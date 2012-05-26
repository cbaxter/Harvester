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
    internal class ContainsFilter : StringComparisonFilterBase
    {
        public override String FriendlyName { get { return "Contains"; } }

        public ContainsFilter(IHaveExtendedProperties extendedProperties, IEnumerable<IFilterMessages> children)
            : base(extendedProperties, children, Contains)
        { }

        private static Boolean Contains(String member, String value)
        {
            return (member ?? String.Empty).IndexOf(value ?? String.Empty, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }

    internal class EndsWithFilter : StringComparisonFilterBase
    {
        public override String FriendlyName { get { return "Ends With"; } }

        public EndsWithFilter(IHaveExtendedProperties extendedProperties, IEnumerable<IFilterMessages> children)
            : base(extendedProperties, children, EndsWith)
        { }

        private static Boolean EndsWith(String member, String value)
        {
            return (member ?? String.Empty).EndsWith(value ?? String.Empty, StringComparison.OrdinalIgnoreCase);
        }
    }

    internal class StartsWithFilter : StringComparisonFilterBase
    {
        public override String FriendlyName { get { return "Starts With"; } }

        public StartsWithFilter(IHaveExtendedProperties extendedProperties, IEnumerable<IFilterMessages> children)
            : base(extendedProperties, children, StartsWith)
        { }

        private static Boolean StartsWith(String member, String value)
        {
            return (member ?? String.Empty).StartsWith(value ?? String.Empty, StringComparison.OrdinalIgnoreCase);
        }
    }
}
