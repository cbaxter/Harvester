using Harvester.Core.Configuration;
using Xunit;

// ReSharper disable AccessToDisposedClosure
namespace Harvester.Core.Tests.Configuration.UsingFilters
{
    public class WhenGettingFilters
    {
        [Fact]
        public void CanParseValidFiltersSection()
        {
            using (var configration = TestConfigFile.Open())
            {
               var section = configration.GetSection<FiltersSection>();

                Assert.DoesNotThrow(() => section.CompileFilter());
            }
        }
    }
}
// ReSharper restore AccessToDisposedClosure
