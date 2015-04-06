using System;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using Harvester.Core.Messaging.Sources;
using Harvester.Core.Messaging.Sources.NamedPipe;
using Xunit;
using Xunit.Extensions;

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

namespace Harvester.Core.Tests.Messaging.Sources.NamedPipe.UsingNamedPipeServerBuffer
{
    public class WhenReadingFromBuffer : IDisposable
    {
        private readonly NamedPipeClientStream clientPipeStream;
        private readonly MessageBuffer buffer;
        private readonly String pipeName;

        public WhenReadingFromBuffer()
        {
            pipeName = @"\\.\pipe\" + Guid.NewGuid();
            buffer = new NamedPipeServerBuffer(pipeName);
            clientPipeStream = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut);
        }

        public void Dispose()
        {
            buffer.Dispose();
            clientPipeStream.Dispose();
        }

        [Fact]
        public void ReadAsSingleMessageIfMessageSmallerThanBuffer()
        {
            var message = new Byte[1024];
            var client = Task.Factory.StartNew(() => SendMessage(message));

            Assert.Equal(message.Length, buffer.Read().Length);

            client.Wait();
        }

        [Fact]
        public void ReadAsSingleMessageIfMessageLargerThanBuffer()
        {
            var message = new Byte[32768];
            var client = Task.Factory.StartNew(() => SendMessage(message));

            Assert.Equal(message.Length, buffer.Read().Length);

            client.Wait();
        }

        private void SendMessage(Byte[] message)
        {
            clientPipeStream.Connect();
            clientPipeStream.Write(message, 0, message.Length);
        }

        [Fact]
        public void CanHandleLargeReadVolume()
        {
            const Int32 iterations = 10000;
            var clients = new[]
                              {
                                  Task.Factory.StartNew(() => RunClient(iterations), TaskCreationOptions.PreferFairness),
                                  Task.Factory.StartNew(() => RunClient(iterations), TaskCreationOptions.PreferFairness),
                                  Task.Factory.StartNew(() => RunClient(iterations), TaskCreationOptions.PreferFairness),
                                  Task.Factory.StartNew(() => RunClient(iterations), TaskCreationOptions.PreferFairness),
                                  Task.Factory.StartNew(() => RunClient(iterations), TaskCreationOptions.PreferFairness)
                              };

            for (var i = 0; i < iterations * clients.Length; i++)
            {
                var bytes = buffer.Read();
                var message = Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                Assert.True(message.StartsWith("START->"));
                Assert.True(message.EndsWith("<-END"));
            }

            Task.WaitAll(clients);
        }

        private void RunClient(Int32 iterations)
        {
            var randomizer = new Random();
            for (var i = 0; i < iterations; i++)
            {
                var message = Encoding.UTF8.GetBytes("START->" + String.Empty.PadLeft(randomizer.Next(0, 32768)) + "<-END");

                using (var client = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut))
                {
                    client.Connect();
                    client.Write(message, 0, message.Length);
                }
            }
        }

        [Fact]
        public void NameIsBufferName()
        {
            Assert.Contains(@"\\.\pipe\", buffer.Name);
        }
    }
}
