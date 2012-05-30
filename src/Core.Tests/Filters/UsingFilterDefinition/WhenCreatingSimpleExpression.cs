using System;
using Harvester.Core.Filters;
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

namespace Harvester.Core.Tests.Filters.UsingFilterDefinition
{
    public class WhenCreatingSimpleExpression
    {
        [Fact]
        public void DoNotNegateExpression()
        {
            var definition = FilterDefinition.Create("Level", SystemEventLevel.Error);

            Assert.False(definition.Negate);
        }

        [Fact]
        public void FriendlyNameEmpty()
        {
            var definition = FilterDefinition.Create("Level", SystemEventLevel.Error);

            Assert.Equal(String.Empty, definition.FriendlyName);
        }

        [Fact]
        public void PropertyMappedCorrectly()
        {
            var definition = FilterDefinition.Create("Level", SystemEventLevel.Error);

            Assert.Equal("Level", definition.Property);
        }

        [Fact]
        public void PropertyExtendedPropertyDefined()
        {
            var definition = FilterDefinition.Create("Level", SystemEventLevel.Error);

            Assert.True(definition.HasExtendedProperty("property"));
            Assert.Equal(definition.Property, definition.GetExtendedProperty("property"));
        }

        [Fact]
        public void ValueExtendedPropertyDefined()
        {
            var definition = FilterDefinition.Create("Level", SystemEventLevel.Error);

            Assert.True(definition.HasExtendedProperty("value"));
            Assert.Equal("Error", definition.GetExtendedProperty("value"));
        }

        [Fact]
        public void ThrowExceptionIfPropertyNotDefined()
        {
            var definition = FilterDefinition.Create("Level", SystemEventLevel.Error);

            Assert.Throws<ArgumentException>(() => definition.GetExtendedProperty("DoesNotexist"));
        }

        [Fact]
        public void ToStringDescribesDefinition()
        {
            var definition = FilterDefinition.Create("Level", SystemEventLevel.Error);

            Assert.Equal("\"Error\" is True", definition.ToString());
        }
    }
}
