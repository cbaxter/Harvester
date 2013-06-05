using System;
using Harvester.Core.Filters;
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

namespace Harvester.Core.Tests.Filters.UsingDynamicFilterExpression
{
    public class WhenBuildingApplicationFilters : WithStaticFilterDefinition
    {
        [Fact]
        public void NeverReturnNull()
        {
            var dynamicFilter = new DynamicFilterExpression(StaticFilter) { ApplicationFilters = null };

            Assert.NotNull(dynamicFilter.ApplicationFilters);
        }

        [Theory, InlineData("Abd"), InlineData("Wyz"), InlineData("135")]
        public void ExcludeIfEventProcessNameNotEqualToAnyApplicationFilter(String processName)
        {
            var dynamicFilter = new DynamicFilterExpression(StaticFilter) { ApplicationFilters = new[] { "Abc", "Xyz", "123" } };
            dynamicFilter.Update();

            Assert.True(dynamicFilter.Exclude(new SystemEvent { ProcessName = processName }));
        }

        [Theory, InlineData("Abc"), InlineData("Xyz"), InlineData("123")]
        public void IncludeIfEventProcessNameEqualToAnyApplicationFilter(String processName)
        {
            var dynamicFilter = new DynamicFilterExpression(StaticFilter) { ApplicationFilters = new[] { "Abc", "Xyz", "123" } };
            dynamicFilter.Update();

            Assert.False(dynamicFilter.Exclude(new SystemEvent { ProcessName = processName }));
        }

        [Theory, InlineData("ABC"), InlineData("abc"), InlineData("AbC")]
        public void ApplicationNameFilterIsCaseSensitive(String processName)
        {
            var dynamicFilter = new DynamicFilterExpression(StaticFilter) { ApplicationFilters = new[] { "Abc" } };
            dynamicFilter.Update();

            Assert.True(dynamicFilter.Exclude(new SystemEvent { ProcessName = processName }));
        }
    }
}
