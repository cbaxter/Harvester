using System;
using System.Collections.Generic;
using System.Linq;
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
    public class WhenGettingSupportedFilterNames : WithStaticFilterDefinition
    {
        public static IEnumerable<String> CompositeFilterNames;
        public static IEnumerable<Object[]> BuiltInTypes { get { return Types.BuiltIn.Select(type => new Object[] { type }); } }

        static WhenGettingSupportedFilterNames()
        {
            CompositeFilterNames = CoreAssembly.Reference
                                               .GetTypes()
                                               .Where(type => !type.IsAbstract && type.IsClass && typeof(ICreateFilterExpressions).IsAssignableFrom(type))
                                               .Select(type => (ICreateFilterExpressions)Activator.CreateInstance(type, FilterDefinition.Empty, Enumerable.Empty<ICreateFilterExpressions>()))
                                               .Where(filter => filter.CompositeExpression)
                                               .Select(filter => filter.FriendlyName)
                                               .ToArray();
        }

        [Theory, PropertyData("BuiltInTypes")]
        public void DoNotIncludeCompositeFilterTypes(Object type)
        {
            var dynamicFilter = new DynamicFilterExpression(StaticFilter);
            var friendlyNames = dynamicFilter.GetSupportedFilterNames((Type)type);

            Assert.Equal(0, friendlyNames.Intersect(CompositeFilterNames).Count());
        }
    }
}
