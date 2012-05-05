using System;
using System.Text;
using System.Threading;
using Harvester.Core.Messaging.Sources.DbWin;
using Xunit;

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

namespace Harvester.Core.Tests.Messaging.Sources.DbWin.UsingSharedMemoryBuffer
{
    public class WhenReadingFromBuffer : IDisposable
    {
        private readonly ManualResetEvent syncEvent1;
        private readonly ManualResetEvent syncEvent2;
        private readonly ManualResetEvent syncEvent3;
        private readonly SharedMemoryBuffer buffer;

        public WhenReadingFromBuffer()
        {
            var guid = Guid.NewGuid();
            var bufferName = String.Format(@"Local\HRVSTR_{0}", guid);

            buffer = new SharedMemoryBuffer(bufferName, 1024) { Timeout = TimeSpan.FromMilliseconds(250) };
            syncEvent1 = new ManualResetEvent(false);
            syncEvent2 = new ManualResetEvent(false);
            syncEvent3 = new ManualResetEvent(false);
        }

        public void Dispose()
        {
            syncEvent3.Dispose();
            syncEvent2.Dispose();
            syncEvent1.Dispose();
            buffer.Dispose();
        }

        [Fact]
        public void CannotReadFromDisposedObject()
        {
            buffer.Dispose();

            Assert.Throws<ObjectDisposedException>(() => buffer.Read());
        }

        [Fact]
        public void ReadBlocksUntilWrite()
        {
            ThreadPool.QueueUserWorkItem(state =>
                                             {
                                                 syncEvent1.Set();
                                                 
                                                 Assert.Contains("Sample Data", Encoding.ASCII.GetString(buffer.Read()));
                                                 
                                                 syncEvent2.Set();
                                             });

            syncEvent1.WaitOne();

            Assert.False(syncEvent2.WaitOne(TimeSpan.FromMilliseconds(100)));

            buffer.Write(Encoding.ASCII.GetBytes("Sample Data"));

            Assert.True(syncEvent2.WaitOne());
        }

        [Fact]
        public void CanReadImmediatelyIfBufferHasData()
        {
            buffer.Write(Encoding.ASCII.GetBytes("Sample Data"));

            Assert.Contains("Sample Data", Encoding.ASCII.GetString(buffer.Read()));
        }

        [Fact]
        public void CanOnlyReadDataOnce()
        {
            ThreadPool.QueueUserWorkItem(state =>
                                             {
                                                 buffer.Write(Encoding.ASCII.GetBytes("Sample Data 1"));

                                                 syncEvent1.Set();
                                                 syncEvent2.WaitOne();

                                                 Assert.Contains("Sample Data 2", Encoding.ASCII.GetString(buffer.Read()));
                                                  
                                                 syncEvent3.Set();
                                             });

            syncEvent1.WaitOne();

            Assert.Contains("Sample Data 1", Encoding.ASCII.GetString(buffer.Read()));

            syncEvent2.Set();

            Assert.False(syncEvent3.WaitOne(TimeSpan.FromMilliseconds(100)));

            buffer.Write(Encoding.ASCII.GetBytes("Sample Data 2"));

            Assert.True(syncEvent3.WaitOne());
        }

        [Fact]
        public void BlockedReadThrowsObjectDisposedExceptionOnDisposeCall()
        {
            ThreadPool.QueueUserWorkItem(state =>
                                             {
                                                 syncEvent1.WaitOne();

                                                 buffer.Dispose();

                                                 syncEvent2.Set();
                                             });

            syncEvent1.Set();

            Assert.Throws<ObjectDisposedException>(() => buffer.Read());

            syncEvent2.WaitOne();
        }

        [Fact]
        public void NameIsBufferName()
        {
            Assert.Contains(@"Local\HRVSTR_", buffer.Name);
        }
    }
}
