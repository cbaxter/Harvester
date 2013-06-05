using System.Linq;
using Harvester.Core.Filters;
using Xunit;

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

namespace Harvester.Core.Tests.Filters.UsingCompositeFilter
{
    public class WhenAndAlsoFilter
    {
        [Fact]
        public void AlwaysCompositeExpression()
        {
            var filter = new AndAlsoFilter(new FakeExtendedProperties(), Enumerable.Empty<ICreateFilterExpressions>());

            Assert.True(filter.CompositeExpression);
        }

        [Fact]
        public void ReturnTrueIfNoChildFilters()
        {
            var e = new SystemEvent();
            var extendedProperties = new FakeExtendedProperties { { "property", "Level" }, { "value", "Warning" } };
            var filter = new AndAlsoFilter(extendedProperties, Enumerable.Empty<ICreateFilterExpressions>());

            Assert.True(Filter.Compile(filter).Invoke(e));
        }

        [Fact]
        public void ReturnTrueIfSingleChildFilterTrue()
        {
            var e = new SystemEvent { Level = SystemEventLevel.Warning };
            var extendedProperties = new FakeExtendedProperties { { "property", "Level" }, { "value", "Warning" } };
            var filter = new AndAlsoFilter(new FakeExtendedProperties(), new[] { new EqualFilter(extendedProperties, Enumerable.Empty<ICreateFilterExpressions>()) });

            Assert.True(Filter.Compile(filter).Invoke(e));
        }

        [Fact]
        public void ReturnFalseIfSingleChildFilterFalse()
        {
            var e = new SystemEvent { Level = SystemEventLevel.Error };
            var extendedProperties = new FakeExtendedProperties { { "property", "Level" }, { "value", "Warning" } };
            var filter = new AndAlsoFilter(new FakeExtendedProperties(), new[] { new EqualFilter(extendedProperties, Enumerable.Empty<ICreateFilterExpressions>()) });

            Assert.False(Filter.Compile(filter).Invoke(e));
        }

        [Fact]
        public void ReturnTrueIfAllChildFiltersTrue()
        {
            var e = new SystemEvent { Level = SystemEventLevel.Error, ProcessId = 1234, Thread = "Unknown" };
            var filter = new AndAlsoFilter(
                             new FakeExtendedProperties(),
                             new[]
                                 {
                                     new EqualFilter(new FakeExtendedProperties { { "property", "ProcessId" }, { "value", "1234" } }, Enumerable.Empty<ICreateFilterExpressions>()),
                                     new EqualFilter(new FakeExtendedProperties { { "property", "Thread" }, { "value", "Unknown" } }, Enumerable.Empty<ICreateFilterExpressions>()),
                                     new EqualFilter(new FakeExtendedProperties { { "property", "Level" }, { "value", "Error" } }, Enumerable.Empty<ICreateFilterExpressions>())
                                 });

            Assert.True(Filter.Compile(filter).Invoke(e));
        }

        [Fact]
        public void ReturnFalseIfAnyChildFilterFalse()
        {
            var e = new SystemEvent { Level = SystemEventLevel.Error, ProcessId = 123, Thread = "Unknown" };
            var filter = new AndAlsoFilter(
                             new FakeExtendedProperties(),
                             new[]
                                 {
                                     new EqualFilter(new FakeExtendedProperties { { "property", "ProcessId" }, { "value", "1234" } }, Enumerable.Empty<ICreateFilterExpressions>()),
                                     new EqualFilter(new FakeExtendedProperties { { "property", "Thread" }, { "value", "Unknown" } }, Enumerable.Empty<ICreateFilterExpressions>()),
                                     new EqualFilter(new FakeExtendedProperties { { "property", "Level" }, { "value", "Error" } }, Enumerable.Empty<ICreateFilterExpressions>())
                                 });

            Assert.False(Filter.Compile(filter).Invoke(e));
        }
    }
}
