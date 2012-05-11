using System;
using System.Collections.Generic;
using System.Globalization;
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
    internal abstract class ComparisonFilterBase : FilterBase
    {
        private readonly Func<Expression, Expression, BinaryExpression> expressionBuilder;

        protected ComparisonFilterBase(IHaveExtendedProperties extendedProperties, IEnumerable<IFilterMessages> children, Func<Expression, Expression, BinaryExpression> expressionBuilder)
            : base(extendedProperties, children)
        {
            Verify.False((children ?? Enumerable.Empty<IFilterMessages>()).Any(), "children", "Comparison filter cannot have any child filters defined.");
            Verify.NotNull(expressionBuilder, "expressionBuilder");

            this.expressionBuilder = expressionBuilder;
        }

        protected override Expression BuildExpression(FilterParameters parameters)
        {
            var systemEvent = parameters.GetParameter(typeof(SystemEvent));
            var propertyName = ExtendedProperties.GetExtendedProperty("property");
            var propertyExpression = Expression.Property(systemEvent, propertyName);

            return expressionBuilder.Invoke(
                       ConvertPropertyValueIfRequired(propertyExpression),
                       GetConvertedValue(propertyExpression.Type)
                   );
        }

        protected Expression ConvertPropertyValueIfRequired(MemberExpression propertyExpression)
        {
            return propertyExpression.Type.IsEnum
                       ? Expression.Convert(propertyExpression, propertyExpression.Type.GetEnumUnderlyingType())
                       : propertyExpression as Expression;
        }

        protected Expression GetConvertedValue(Type targetType)
        {
            var value = ExtendedProperties.GetExtendedProperty("value") ?? String.Empty;

            if (targetType.IsEnum)
                return Expression.Constant(((IConvertible)Enum.Parse(targetType, value, true)).ToType(targetType.GetEnumUnderlyingType(), CultureInfo.InvariantCulture));

            return typeof(IConvertible).IsAssignableFrom(targetType)
                        ? Expression.Constant(((IConvertible)value).ToType(targetType, CultureInfo.InvariantCulture))
                        : Expression.Convert(Expression.Constant(value), targetType) as Expression;
        }
    }
}
