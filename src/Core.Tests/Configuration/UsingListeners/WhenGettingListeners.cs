using Harvester.Core.Configuration;
using Xunit;

// ReSharper disable AccessToDisposedClosure
namespace Harvester.Core.Tests.Configuration.UsingListeners
{
    public class WhenGettingListeners
    {
        [Fact]
        public void CanParseValidListenersSection()
        {
            using (var configration = TestConfigFile.Open())
            {
                Assert.DoesNotThrow(() => configration.GetSection<ListenersSection>());
            }
        }
    }
}
// ReSharper restore AccessToDisposedClosure
