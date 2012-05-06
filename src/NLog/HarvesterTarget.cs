using System;
using Harvester.Core;
using Harvester.Core.Messaging.Sources;
using Harvester.Core.Messaging.Sources.DbWin;
using Harvester.Core.Messaging.Sources.NamedPipe;
using NLog;
using NLog.Layouts;
using NLog.Targets;

/* Copyright (c) 2012 CBaxter
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

namespace Harvester.Integration.NLog
{
    [Target("Harvester")]
    public sealed class HarvesterTarget : Target
    {
        private IWriteMessages MessageWriter { get; set; }
        private IMessageBuffer MessageBuffer { get; set; }
        private Layout Layout { get; set; }

        public String Binding { get; set; }
        public String Buffer { get; set; }
        public String Mutex { get; set; }

        public HarvesterTarget()
        {
            Layout = new Log4JXmlEventLayout();
            Binding = @"\\.\pipe\Harvester";
            Buffer = @"NamedPipeBuffer";
            Mutex = @"HarvesterMutex";
        }

        protected override void InitializeTarget()
        {
            base.InitializeTarget();

            switch (Buffer)
            {
                case "SharedMemoryBuffer":
                    SetupSharedMemoryBuffer(Binding, Mutex);
                    break;
                case "NamedPipeBuffer":
                    SetupNamedPipeBuffer(Binding, Mutex);
                    break;
                default:
                    throw new NotSupportedException(String.Format("Unknown buffer type specified '{0}'; supported buffers are 'SharedMemoryBuffer' and 'NamedPipeBuffer'.", Buffer));
            }
        }

        private void SetupSharedMemoryBuffer(String buffer, String mutex)
        {
            if (String.IsNullOrWhiteSpace(Binding) || !(Binding.StartsWith(@"Local\") || Binding.StartsWith(@"Global\")))
                throw new ArgumentException(Localization.InvalidSharedMemoryBinding, "buffer");

            if (String.IsNullOrWhiteSpace(Mutex))
                throw new ArgumentException(Localization.InvalidSharedMemoryMutex, "mutex");

            MessageBuffer = new SharedMemoryBuffer(buffer, OutputDebugString.BufferSize);
            MessageWriter = new OutputDebugStringWriter(mutex, MessageBuffer);
        }

        private void SetupNamedPipeBuffer(String buffer, String mutex)
        {
            if (String.IsNullOrWhiteSpace(Binding) || !Binding.StartsWith(@"\\.\pipe\"))
                throw new ArgumentException(Localization.InvalidNamedPipeBinding, "buffer");

            if (String.IsNullOrWhiteSpace(Mutex))
                throw new ArgumentException(Localization.InvalidNamedPipeMutex, "mutex");

            MessageBuffer = new NamedPipeClientBuffer(".", buffer);
            MessageWriter = new PipeMessageWriter(mutex, MessageBuffer);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (!disposing || MessageBuffer == null)
                return;

            MessageBuffer.Dispose();
            MessageBuffer = null;
        }

        protected override void Write(LogEventInfo logEvent)
        {
            MessageWriter.Write(Layout.Render(logEvent));
        }
    }
}
