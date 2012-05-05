using System;
using Harvester.Core.Messaging;
using Moq;
using Xunit;

namespace Harvester.Core.Tests.Messaging.UsingMessageProcessor
{
    public class WhenDisposingProcessor : IDisposable
    {
        private readonly Mock<IRenderEvents> renderer = new Mock<IRenderEvents>();
        private readonly MessageProcessor processor;

        public WhenDisposingProcessor()
        {
            processor = new MessageProcessor(renderer.Object);
        }

        public void Dispose()
        {
            processor.Dispose();
        }

        [Fact]
        public void CanSafelyCallDisposeMultipleTimes()
        {
            processor.Dispose();
            processor.Dispose();
        }
    }
}
