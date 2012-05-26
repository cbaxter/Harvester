using System;
using System.Collections.Generic;
using System.Linq;
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
    internal abstract class CompositeFilterBase : FilterBase
    {
        private readonly Func<Expression, Expression, BinaryExpression> expressionBuilder;

        public override bool CompositeFilter { get { return true; } }

        protected CompositeFilterBase(IHaveExtendedProperties extendedProperties, IEnumerable<IFilterMessages> children, Func<Expression, Expression, BinaryExpression> expressionBuilder)
            : base(extendedProperties, children)
        {
            Verify.NotNull(expressionBuilder, "expressionBuilder");

            this.expressionBuilder = expressionBuilder;
        }

        protected override Expression BuildExpression(FilterParameters parameters)
        {
            var filters = Children.ToList();

            switch (filters.Count)
            {
                case 0:
                    return Expression.Constant(true);
                case 1:
                    return filters[0].CreateExpression(parameters);
                default:
                    var expression = filters[0].CreateExpression(parameters);

                    for (var i = 1; i < filters.Count; i++)
                        expression = expressionBuilder.Invoke(expression, filters[i].CreateExpression(parameters));

                    return expression;
            }
        }
    }
}
