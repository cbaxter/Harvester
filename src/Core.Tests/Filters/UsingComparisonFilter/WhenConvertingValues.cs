using System;
using System.Globalization;
using System.Linq;
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

namespace Harvester.Core.Tests.Filters.UsingComparisonFilter
{
    public class WhenConvertingValues
    {
        [Fact]
        public void ValueCanEnum()
        {
            var e = new SystemEvent { Level = SystemEventLevel.Warning };
            var extendedProperties = new FakeExtendedProperties { { "property", "Level" }, { "value", "Warning" } };
            var filter = new EqualFilter(extendedProperties, Enumerable.Empty<IFilterMessages>());

            Assert.True(Filter.Compile(filter).Invoke(e));
        }

        [Fact]
        public void ValueCanBeUInt32()
        {
            var e = new SystemEvent();
            var extendedProperties = new FakeExtendedProperties { { "property", "MessageId" }, { "value", e.MessageId.ToString(CultureInfo.InvariantCulture) } };
            var filter = new EqualFilter(extendedProperties, Enumerable.Empty<IFilterMessages>());

            Assert.True(Filter.Compile(filter).Invoke(e));
        }

        [Fact]
        public void ValueCanBeInt32()
        {
            var e = new SystemEvent { ProcessId = 123 };
            var extendedProperties = new FakeExtendedProperties { { "property", "ProcessId" }, { "value", "123" } };
            var filter = new EqualFilter(extendedProperties, Enumerable.Empty<IFilterMessages>());

            Assert.True(Filter.Compile(filter).Invoke(e));
        }

        [Fact]
        public void ValueCanBeDateTime()
        {
            var now = DateTime.Now;
            var e = new SystemEvent { Timestamp = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second) };
            var extendedProperties = new FakeExtendedProperties { { "property", "Timestamp" }, { "value", now.ToString("yyyy-MM-dd HH:mm:ss") } };
            var filter = new EqualFilter(extendedProperties, Enumerable.Empty<IFilterMessages>());

            Assert.True(Filter.Compile(filter).Invoke(e));
        }

        [Fact]
        public void ValueCanBeString()
        {
            var e = new SystemEvent { Message = "My Message" };
            var extendedProperties = new FakeExtendedProperties { { "property", "Message" }, { "value", "My Message" } };
            var filter = new EqualFilter(extendedProperties, Enumerable.Empty<IFilterMessages>());

            Assert.True(Filter.Compile(filter).Invoke(e));
        }
    }
}
