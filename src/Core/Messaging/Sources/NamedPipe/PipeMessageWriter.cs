using System;
using System.Diagnostics;
using System.Text;
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

namespace Harvester.Core.Messaging.Sources.NamedPipe
{
    public sealed class PipeMessageWriter : IWriteMessages
    {
        private readonly MessageBuffer memoryBuffer;
        private readonly String mutexName;
        private readonly Byte[] preamble;

        public PipeMessageWriter(String mutexName, MessageBuffer memoryBuffer)
        {
            Verify.NotWhitespace(mutexName, "mutexName");
            Verify.NotNull(memoryBuffer, "memoryBuffer");

            this.mutexName = mutexName;
            this.memoryBuffer = memoryBuffer;

            using (var process = Process.GetCurrentProcess())
                preamble = BitConverter.GetBytes(process.Id);
        }

        public void Write(String message)
        {
            Boolean createdNew;

            using (var mutex = new Mutex(false, mutexName, out createdNew))
            {
                if (createdNew || !mutex.WaitOne(memoryBuffer.Timeout))
                    return;

                try
                {
                    var messageBytes = Encoding.UTF8.GetBytes(message ?? String.Empty);
                    var data = new Byte[preamble.Length + messageBytes.Length];

                    Buffer.BlockCopy(preamble, 0, data, 0, preamble.Length);
                    Buffer.BlockCopy(messageBytes, 0, data, preamble.Length, messageBytes.Length);

                    memoryBuffer.Write(data);
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }
    }
}
