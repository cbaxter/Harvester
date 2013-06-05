using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class WhenBuildingUsernameFilters : WithStaticFilterDefinition
    {
        [Fact]
        public void ThrowExceptionIfUnknownFriendlyFilterName()
        {
            var dynamicFilter = new DynamicFilterExpression(StaticFilter) { TextFilters = new[] { FilterDefinition.ForPositiveExpression("Username", "Unknown", "Some Value") } };

            Assert.Throws<ArgumentException>(() => dynamicFilter.Update());
        }

        [Fact]
        public void ExcludeIfNegativeFilterDefinition()
        {
            var dynamicFilter = new DynamicFilterExpression(StaticFilter) { TextFilters = new[] { FilterDefinition.ForNegativeExpression("Username", "Equal To", "Some Value") } };
            dynamicFilter.Update();

            Assert.True(dynamicFilter.Exclude(new SystemEvent { Username = "Some Value" }));
        }

        [Fact]
        public void IncludeIfPositiveFilterDefinition()
        {
            var dynamicFilter = new DynamicFilterExpression(StaticFilter) { TextFilters = new[] { FilterDefinition.ForPositiveExpression("Username", "Equal To", "Some Value") } };
            dynamicFilter.Update();

            Assert.False(dynamicFilter.Exclude(new SystemEvent { Username = "Some Value" }));
        }

        [Fact]
        public void FiltersDefinitionsBySourceProperty()
        {
            var textFilters = new[] { FilterDefinition.ForPositiveExpression("Username", "Equal To", "Some Value"), FilterDefinition.ForPositiveExpression("Message", "Equal To", "Another Value") };
            var dynamicFilter = new DynamicFilterExpression(StaticFilter) { TextFilters = textFilters };
            dynamicFilter.Update();

            Assert.False(dynamicFilter.Exclude(new SystemEvent { Username = "Some Value", Message = "Another Value" }));
        }

        [Theory, InlineData("Equal To"), InlineData("Not Equal To"), InlineData("Starts With"), InlineData("Ends With"), InlineData("Contains")]
        public void SupportedFilterTypes(String friendlyName)
        {
            var dynamicFilter = new DynamicFilterExpression(StaticFilter) { TextFilters = new[] { FilterDefinition.ForPositiveExpression("Username", friendlyName, "Some Value") } };

            Assert.DoesNotThrow(dynamicFilter.Update);
        }
    }
}
