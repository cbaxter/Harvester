using System;
using System.Diagnostics;
using System.Threading;
using Harvester.Core.Messaging;
using Harvester.Core.Messaging.Parsers;
using Harvester.Core.Messaging.Sources.DbWin;
using Moq;
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

namespace Harvester.Core.Tests.Messaging.UsingMessageProcessor
{
    public class WhenProcessingMessage : IDisposable
    {
        private readonly Mock<IParseMessages> parser = new Mock<IParseMessages>();
        private readonly Mock<IRenderEvents> renderer = new Mock<IRenderEvents>();
        private readonly MessageProcessor processor;

        public WhenProcessingMessage()
        {
            processor = new MessageProcessor(renderer.Object, new[] { parser.Object });
        }

        public void Dispose()
        {
            processor.Dispose();
        }

        [Fact]
        public void DoNotThrowOnDisposedProcessor()
        {
            processor.Dispose();
            processor.Process(null);
        }

        [Fact]
        public void RenderMessageIfParserFound()
        {
            var e = new SystemEvent();
            var message = new OutputDebugString("MySource", 123, "My Message");
            var resetEvent = new ManualResetEvent(false);

            renderer.Setup(mock => mock.Render(It.IsAny<SystemEvent>())).Callback(() => resetEvent.Set());
            parser.Setup(mock => mock.CanParseMessage("My Message")).Returns(true);
            parser.Setup(mock => mock.Parse(message)).Returns(e);

            processor.Process(message);

            Assert.True(resetEvent.WaitOne(TimeSpan.FromSeconds(1)), "ManualResetEvent not signalled within expected time.");

            renderer.Verify(mock => mock.Render(e), Times.Once());
        }

        [Fact]
        public void RenderMessageIfNoParserFound()
        {
            var message = new OutputDebugString("MySource", 123, "My Message");
            var resetEvent = new ManualResetEvent(false);

            renderer.Setup(mock => mock.Render(It.IsAny<SystemEvent>())).Callback(() => resetEvent.Set());
            parser.Setup(mock => mock.CanParseMessage("My Message")).Returns(false);

            processor.Process(message);

            Assert.True(resetEvent.WaitOne(TimeSpan.FromSeconds(1)), "ManualResetEvent not signalled within expected time.");

            renderer.Verify(mock => mock.Render(It.Is((SystemEvent e) => e.ProcessName == "Process #123")), Times.Once());
            parser.Verify(mock => mock.Parse(message), Times.Never());
        }

        [Fact]
        public void ProcessWillNotBlockOnCurrentThread()
        {
            using (var blockingMessage = new BlockingMessage())
            {
                var stopwatch = Stopwatch.StartNew();
                processor.Process(blockingMessage);
                stopwatch.Stop();

                Assert.InRange(stopwatch.ElapsedMilliseconds, 0, 10);
            }
        }

        private class BlockingMessage : IMessage, IDisposable
        {
            private readonly ManualResetEvent resetEvent = new ManualResetEvent(false);

            public DateTime Timestamp { get { resetEvent.WaitOne(); return DateTime.Now; } }
            public Int32 ProcessId { get { resetEvent.WaitOne(); return DateTime.Now.Millisecond; } }
            public String Message { get { resetEvent.WaitOne(); return DateTime.Now.ToLongTimeString(); } }
            public String Source { get { resetEvent.WaitOne(); return DateTime.Now.ToLongTimeString(); } }

            public void Dispose()
            {
                resetEvent.Set();
            }
        }
    }
}
