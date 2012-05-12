using Harvester.Core.Configuration;
using Xunit;

// ReSharper disable AccessToDisposedClosure
namespace Harvester.Core.Tests.Configuration.UsingLevels
{
    public class WhenGettingLevels
    {
        [Fact]
        public void CanParseValidLevelsSection()
        {
            using (var configration = TestConfigFile.Open())
            {
                Assert.DoesNotThrow(() => configration.GetSection<LevelsSection>());
            }
        }
    }
}
// ReSharper restore AccessToDisposedClosure
