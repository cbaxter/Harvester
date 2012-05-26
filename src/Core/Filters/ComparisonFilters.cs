using System;
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
    internal class EqualFilter : ComparisonFilterBase
    {
        public override String FriendlyName { get { return "Equal To"; } }

        public EqualFilter(IHaveExtendedProperties extendedProperties, IEnumerable<ICreateFilterExpressions> children)
            : base(extendedProperties, children, Expression.Equal)
        { }
    }

    internal class GreaterThanFilter : ComparisonFilterBase
    {
        public override String FriendlyName { get { return "Greater Than"; } }

        public GreaterThanFilter(IHaveExtendedProperties extendedProperties, IEnumerable<ICreateFilterExpressions> children)
            : base(extendedProperties, children, Expression.GreaterThan)
        { }
    }

    internal class GreaterThanOrEqualFilter : ComparisonFilterBase
    {
        public override String FriendlyName { get { return "Greater Than Or Equal To"; } }

        public GreaterThanOrEqualFilter(IHaveExtendedProperties extendedProperties, IEnumerable<ICreateFilterExpressions> children)
            : base(extendedProperties, children, Expression.GreaterThanOrEqual)
        { }
    }

    internal class LessThanFilter : ComparisonFilterBase
    {
        public override String FriendlyName { get { return "Less Than"; } }

        public LessThanFilter(IHaveExtendedProperties extendedProperties, IEnumerable<ICreateFilterExpressions> children)
            : base(extendedProperties, children, Expression.LessThan)
        { }
    }

    internal class LessThanOrEqualFilter : ComparisonFilterBase
    {
        public override String FriendlyName { get { return "Less Than Or Equal To"; } }

        public LessThanOrEqualFilter(IHaveExtendedProperties extendedProperties, IEnumerable<ICreateFilterExpressions> children)
            : base(extendedProperties, children, Expression.LessThanOrEqual)
        { }
    }

    internal class NotEqualFilter : ComparisonFilterBase
    {
        public override String FriendlyName { get { return "Not Equal To"; } }

        public NotEqualFilter(IHaveExtendedProperties extendedProperties, IEnumerable<ICreateFilterExpressions> children)
            : base(extendedProperties, children, Expression.NotEqual)
        { }
    }
}
