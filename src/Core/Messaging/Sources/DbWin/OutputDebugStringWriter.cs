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

namespace Harvester.Core.Messaging.Sources.DbWin
{
    public sealed class OutputDebugStringWriter : IWriteMessages
    {
        private readonly MessageBuffer messageBuffer;
        private readonly String mutexName;
        private readonly Byte[] buffer;

        public OutputDebugStringWriter(String mutexName, MessageBuffer messageBuffer)
            : this(mutexName, messageBuffer, new Byte[OutputDebugString.BufferSize])
        { }

        public OutputDebugStringWriter(String mutexName, MessageBuffer messageBuffer, Byte[] buffer)
        {
            Verify.NotNull(buffer, "buffer");
            Verify.NotWhitespace(mutexName, "mutexName");
            Verify.NotNull(messageBuffer, "messageBuffer");

            this.buffer = buffer;
            this.mutexName = mutexName;
            this.messageBuffer = messageBuffer;

            PrepareBufferPreamble();
        }

        private void PrepareBufferPreamble()
        {
            using (var process = Process.GetCurrentProcess())
            {
                var preamble = BitConverter.GetBytes(process.Id);

                Buffer.BlockCopy(preamble, 0, buffer, 0, preamble.Length);
            }
        }

        public void Write(String message)
        {
            Boolean createdNew;

            using (var mutex = new Mutex(false, mutexName, out createdNew))
            {
                if (createdNew || !mutex.WaitOne(messageBuffer.Timeout))
                    return;

                try
                {
                    if (String.IsNullOrEmpty(message))
                        WriteEmptyMessage();
                    else
                        WriteChunkedMessage(message);
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }

        private void WriteEmptyMessage()
        {
            buffer[OutputDebugString.PreambleSize] = 0;
            messageBuffer.Write(buffer);
        }

        private void WriteChunkedMessage(String message)
        {
            const Int32 maxBlockSize = OutputDebugString.BufferSize - OutputDebugString.PreambleSize - 1;
            var position = 0;

            while (position < message.Length)
            {
                var length = Math.Min(maxBlockSize, message.Length - position);
                var messageBytes = Encoding.UTF8.GetBytes(message.Substring(position, length));

                Buffer.BlockCopy(messageBytes, 0, buffer, OutputDebugString.PreambleSize, messageBytes.Length);
                buffer[OutputDebugString.PreambleSize + messageBytes.Length] = 0;

                messageBuffer.Write(buffer);

                position += length;
            }
        }
    }
}
