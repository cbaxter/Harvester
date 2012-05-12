using Harvester.Core.Configuration;
using Xunit;

// ReSharper disable AccessToDisposedClosure
namespace Harvester.Core.Tests.Configuration.UsingParsers
{
    public class WhenGettingParsers
    {
        [Fact]
        public void CanParseValidParsersSection()
        {
            using (var configration = TestConfigFile.Open())
            {
                Assert.DoesNotThrow(() => configration.GetSection<ParsersSection>());
            }
        }
    }
}
// ReSharper restore AccessToDisposedClosure
