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

namespace Harvester.Core.Messaging.Sources.NamedPipe
{
    /// <summary>
    /// Writes out debug messages to the underlying named pipe.
    /// </summary>
    public sealed class PipeMessageWriter : IWriteMessages
    {
        private readonly MessageBuffer messageBuffer;
        private readonly String mutexName;
        private readonly Byte[] preamble;

        /// <summary>
        /// Initializes a new instance of <see cref="PipeMessageWriter"/>.
        /// </summary>
        /// <param name="mutexName">The global mutex name.</param>
        /// <param name="messageBuffer">The underlying message buffer implementation.</param>
        public PipeMessageWriter(String mutexName, MessageBuffer messageBuffer)
        {
            Verify.NotWhitespace(mutexName, "mutexName");
            Verify.NotNull(messageBuffer, "messageBuffer");

            this.mutexName = mutexName;
            this.messageBuffer = messageBuffer;
            this.preamble = GetPremable();
        }

        /// <summary>
        /// Gets the message preamble (i.e., current process identifier).
        /// </summary>
        private static Byte[] GetPremable()
        {
            using (var process = Process.GetCurrentProcess())
                return BitConverter.GetBytes(process.Id);
        }

        /// <summary>
        /// Write out the specified <paramref name="message"/> to the underlying message buffer.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void Write(String message)
        {
            //Boolean createdNew;
            Mutex mutex;
            Boolean captured = false;

            if (Mutex.TryOpenExisting(mutexName, out mutex))
            {
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
            var messageBytes = Encoding.UTF8.GetBytes(message ?? String.Empty);
            var data = new Byte[preamble.Length + messageBytes.Length];

            Buffer.BlockCopy(preamble, 0, data, 0, preamble.Length);
            Buffer.BlockCopy(messageBytes, 0, data, preamble.Length, messageBytes.Length);

            messageBuffer.Write(data);
        }
    }
}
