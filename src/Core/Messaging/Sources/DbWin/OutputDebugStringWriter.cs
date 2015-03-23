using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

/* Copyright (c) 2012-2015 CBaxter
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
    /// <summary>
    /// Writes out debug messages to the output debug string shared memory buffer.
    /// </summary>
    public sealed class OutputDebugStringWriter : IWriteMessages
    {
        private readonly MessageBuffer messageBuffer;
        private readonly String mutexName;
        private readonly Byte[] buffer;

        /// <summary>
        /// Initializes a new instance of <see cref="OutputDebugStringWriter"/>.
        /// </summary>
        /// <param name="mutexName">The global mutex name.</param>
        /// <param name="messageBuffer">The underlying message buffer implementation.</param>
        public OutputDebugStringWriter(String mutexName, MessageBuffer messageBuffer)
        {
            Verify.NotWhitespace(mutexName, "mutexName");
            Verify.NotNull(messageBuffer, "messageBuffer");

            this.mutexName = mutexName;
            this.messageBuffer = messageBuffer;
            this.buffer = new Byte[OutputDebugString.BufferSize];

            PrepareBufferPreamble();
        }

        /// <summary>
        /// Prepare the underlying message buffer by pre-writing the current process identifier to the start of the buffer.
        /// </summary>
        private void PrepareBufferPreamble()
        {
            using (var process = Process.GetCurrentProcess())
            {
                var preamble = BitConverter.GetBytes(process.Id);

                Buffer.BlockCopy(preamble, 0, buffer, 0, preamble.Length);
            }
        }

        /// <summary>
        /// Write out the specified <paramref name="message"/> to the underlying message buffer.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void Write(String message)
        {
            Mutex mutex;

            if (Mutex.TryOpenExisting(mutexName, out mutex))
            {
                var captured = false;

                try
                {
                    captured = mutex.WaitOne(messageBuffer.Timeout);
                    if (captured) WriteInternal(message);
                }
                catch (AbandonedMutexException)
                {
                    captured = true;
                }
                finally
                {
                    if (captured) mutex.ReleaseMutex();
                    mutex.Dispose();
                }
            }
        }

        /// <summary>
        /// Write out the specified <paramref name="message"/> to the underlying message buffer.
        /// </summary>
        /// <param name="message">The message to write.</param>
        private void WriteInternal(String message)
        {
            if (String.IsNullOrEmpty(message))
                WriteEmptyMessage();
            else
                WriteChunkedMessage(message);
        }

        /// <summary>
        /// Write an empty message to the underlying message buffer.
        /// </summary>
        private void WriteEmptyMessage()
        {
            buffer[OutputDebugString.PreambleSize] = 0;
            messageBuffer.Write(buffer);
        }

        /// <summary>
        /// Write a chunked message to the underlying message buffer.
        /// </summary>
        /// <param name="message">The message to write.</param>
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
