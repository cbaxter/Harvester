using System;
using System.IO.MemoryMappedFiles;
using System.Threading;

/* Copyright (c) 2012-2013 CBaxter
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
 * IN THE SOFTWARE. 
 */

namespace Harvester.Core.Messaging.Sources.DbWin
{
    public sealed class SharedMemoryBuffer : MessageBuffer
    {
        private readonly MemoryMappedViewAccessor bufferView;
        private readonly EventWaitHandle bufferReadyEvent;
        private readonly EventWaitHandle dataReadyEvent;
        private readonly MemoryMappedFile bufferFile;

        public SharedMemoryBuffer(String baseObjectName, Int64 capacity)
            : base(baseObjectName)
        {
            Verify.NotWhitespace(baseObjectName, "baseObjectName");
            Verify.True(capacity > 0, "capacity", Localization.ValueGreaterThanZeroExpected);
            Verify.True(baseObjectName.StartsWith(@"Local\") || baseObjectName.StartsWith(@"Global\"), "baseObjectName", Localization.InvalidSharedMemoryBufferName);

            dataReadyEvent = new EventWaitHandle(false, EventResetMode.AutoReset, baseObjectName + "_DATA_READY");
            bufferReadyEvent = new EventWaitHandle(true, EventResetMode.AutoReset, baseObjectName + "_BUFFER_READY");
            bufferFile = MemoryMappedFile.CreateOrOpen(baseObjectName + "_BUFFER", capacity, MemoryMappedFileAccess.ReadWrite);
            bufferView = bufferFile.CreateViewAccessor(0, 0, MemoryMappedFileAccess.ReadWrite);

            Timeout = TimeSpan.FromSeconds(10);
        }

        protected override void Dispose(Boolean disposing)
        {
            if (!disposing)
                return;

            dataReadyEvent.SafeSet();

            bufferReadyEvent.Dispose();
            dataReadyEvent.Dispose();
            bufferView.Dispose();
            bufferFile.Dispose();
        }

        protected override Byte[] ReadMessage()
        {
            dataReadyEvent.WaitOne();

            var result = bufferView.ReadBuffer();

            bufferReadyEvent.Set();

            return result;
        }

        protected override void WriteMessage(Byte[] buffer)
        {
            if (buffer == null)
                return;

            if (!bufferReadyEvent.WaitOne(Timeout))
                return;

            bufferView.WriteBuffer(buffer);
            dataReadyEvent.Set();
        }
    }
}
