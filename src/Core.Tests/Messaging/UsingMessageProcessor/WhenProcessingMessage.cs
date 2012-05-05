using System;
using System.Diagnostics;
using System.Threading;
using Harvester.Core.Messaging;
using Moq;
using Xunit;

namespace Harvester.Core.Tests.Messaging.UsingMessageProcessor
{
    public class WhenProcessingMessage : IDisposable
    {
        private readonly Mock<IRenderEvents> renderer = new Mock<IRenderEvents>();
        private readonly MessageProcessor processor;

        public WhenProcessingMessage()
        {
            processor = new MessageProcessor(renderer.Object);
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
        public void ProcessWillNotBlockOnCurrentThread()
        {
            using(var blockingMessage = new BlockingMessage())
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
