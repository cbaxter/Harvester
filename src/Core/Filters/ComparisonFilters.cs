using System.Collections.Generic;
using System.Linq.Expressions;
using Harvester.Core.Messaging;

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

namespace Harvester.Core.Filters
{
    internal class Equal : ComparisonFilterBase
    {
        public Equal(IHaveExtendedProperties extendedProperties, IEnumerable<IFilterMessages> children)
            : base(extendedProperties, children, Expression.Equal)
        { }
    }

    internal class GreaterThan : ComparisonFilterBase
    {
        public GreaterThan(IHaveExtendedProperties extendedProperties, IEnumerable<IFilterMessages> children)
            : base(extendedProperties, children, Expression.GreaterThan)
        { }
    }

    internal class GreaterThanOrEqual : ComparisonFilterBase
    {
        public GreaterThanOrEqual(IHaveExtendedProperties extendedProperties, IEnumerable<IFilterMessages> children)
            : base(extendedProperties, children, Expression.GreaterThanOrEqual)
        { }
    }

    internal class LessThan : ComparisonFilterBase
    {
        public LessThan(IHaveExtendedProperties extendedProperties, IEnumerable<IFilterMessages> children)
            : base(extendedProperties, children, Expression.LessThan)
        { }
    }

    internal class LessThanOrEqual : ComparisonFilterBase
    {
        public LessThanOrEqual(IHaveExtendedProperties extendedProperties, IEnumerable<IFilterMessages> children)
            : base(extendedProperties, children, Expression.LessThanOrEqual)
        { }
    }

    internal class NotEqual : ComparisonFilterBase
    {
        public NotEqual(IHaveExtendedProperties extendedProperties, IEnumerable<IFilterMessages> children)
            : base(extendedProperties, children, Expression.NotEqual)
        { }
    }
}
