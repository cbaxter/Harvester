using System;
using System.Text;
using System.Threading;
using Harvester.Core.Messaging.Sources.DbWin;
using Xunit;

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

namespace Harvester.Core.Tests.Messaging.Sources.DbWin.UsingSharedMemoryBuffer
{
    public class WhenWrittingData : IDisposable
    {
        private readonly ManualResetEvent syncEvent1;
        private readonly ManualResetEvent syncEvent2;
        private readonly SharedMemoryBuffer buffer;

        public WhenWrittingData()
        {
            var guid = Guid.NewGuid();
            var bufferName = String.Format(@"Local\HRVSTR_{0}", guid);

            buffer = new SharedMemoryBuffer(bufferName, 1024) { Timeout = TimeSpan.FromMilliseconds(250) };
            syncEvent1 = new ManualResetEvent(false);
            syncEvent2 = new ManualResetEvent(false);
        }

        public void Dispose()
        {
            syncEvent2.Dispose();
            syncEvent1.Dispose();
            buffer.Dispose();
        }

        [Fact]
        public void CannotWriteToDisposedObject()
        {
            buffer.Dispose();

            Assert.Throws<ObjectDisposedException>(() => buffer.Write(new Byte[0]));
        }

        [Fact]
        public void CannotOverwriteData()
        {
            ThreadPool.QueueUserWorkItem(state =>
                                                {
                                                    syncEvent1.WaitOne();

                                                    buffer.Write(Encoding.ASCII.GetBytes("Sample Data 2"));

                                                    syncEvent2.Set();
                                                });

            buffer.Write(Encoding.ASCII.GetBytes("Sample Data 1"));

            syncEvent1.Set();

            Assert.False(syncEvent2.WaitOne(TimeSpan.FromMilliseconds(100)));
            Assert.Contains("Sample Data 1", Encoding.ASCII.GetString(buffer.Read()));

            syncEvent2.WaitOne();
        }

        [Fact]
        public void CannotExceedBufferLength()
        {
            var largeMessage = '^' + string.Empty.PadLeft(1024) + '$';

            buffer.Write(Encoding.ASCII.GetBytes(largeMessage));

            Assert.Contains(largeMessage.Substring(0, 1024), Encoding.ASCII.GetString(buffer.Read()));
        }

        [Fact]
        public void IgnoreIfBufferReadyEventNotReceived()
        {
            var message1 = Encoding.ASCII.GetBytes("My First Message");
            var message2 = Encoding.ASCII.GetBytes("My Second Message");

            buffer.Write(message1);
            buffer.Write(message2); // Second write will be ignored without an intermediate read.

            Assert.Contains("My First Message", Encoding.ASCII.GetString(buffer.Read()));
        }

        [Fact]
        public void IgnoreIfBufferIsNull()
        {
            var message = Encoding.ASCII.GetBytes("My Message");

            buffer.Write(null);
            buffer.Write(message); // Second write will not be ignored.

            Assert.Contains("My Message", Encoding.ASCII.GetString(buffer.Read()));
        }

        [Fact]
        public void CanWriteEmptyBuffer()
        {
            var message = Encoding.ASCII.GetBytes("My Message");

            buffer.Write(new Byte[0]);
            buffer.Write(message); // Second write will be ignored without an intermediate read.

            Assert.DoesNotContain("My Message", Encoding.ASCII.GetString(buffer.Read()));
        }
    }
}
