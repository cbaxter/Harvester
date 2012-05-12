using System;
using JetBrains.Annotations;

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

namespace Harvester.Core
{
    public static class Verify
    {
        [AssertionMethod]
        public static void Null<T>([AssertionCondition(AssertionConditionType.IsNull)]T value, [InvokerParameterName]String paramName)
          where T : class
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
        }

        [AssertionMethod]
        public static void NotNull<T>([AssertionCondition(AssertionConditionType.IsNotNull)]T value, [InvokerParameterName]String paramName)
          where T : class
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
        }

        [AssertionMethod]
        public static void NotWhitespace([AssertionCondition(AssertionConditionType.IsNotNull)]String value, [InvokerParameterName]String paramName)
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(paramName);
        }

        [AssertionMethod]
        public static void True([AssertionCondition(AssertionConditionType.IsTrue)]Boolean condition, [InvokerParameterName]String paramName, String message)
        {
            if (!condition)
                throw new ArgumentException(message, paramName);
        }

        [AssertionMethod]
        public static void False([AssertionCondition(AssertionConditionType.IsFalse)]Boolean condition, [InvokerParameterName]String paramName, String message)
        {
            if (condition)
                throw new ArgumentException(message, paramName);
        }
    }
}

#region JetBrains.Annotations
/*
 * Copyright 2007-2011 JetBrains s.r.o.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace JetBrains.Annotations
{
    /// <summary>
    /// Indicates that the function argument should be string literal and match one  of the parameters of the caller function.
    /// For example, <see cref="ArgumentNullException"/> has such parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class InvokerParameterNameAttribute : Attribute { }

    /// <summary>
    /// Indicates that the marked method is assertion method, i.e. it halts control flow if one of the conditions is satisfied. 
    /// To set the condition, mark one of the parameters with <see cref="AssertionConditionAttribute"/> attribute
    /// </summary>
    /// <seealso cref="AssertionConditionAttribute"/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AssertionMethodAttribute : Attribute { }

    /// <summary>
    /// Indicates the condition parameter of the assertion method. 
    /// The method itself should be marked by <see cref="AssertionMethodAttribute"/> attribute.
    /// The mandatory argument of the attribute is the assertion type.
    /// </summary>
    /// <seealso cref="AssertionConditionType"/>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class AssertionConditionAttribute : Attribute
    {
        /// <summary>
        /// Initializes new instance of AssertionConditionAttribute
        /// </summary>
        /// <param name="conditionType">Specifies condition type</param>
        public AssertionConditionAttribute(AssertionConditionType conditionType)
        {
            ConditionType = conditionType;
        }

        /// <summary>
        /// Gets condition type
        /// </summary>
        public AssertionConditionType ConditionType { get; private set; }
    }

    /// <summary>
    /// Specifies assertion type. If the assertion method argument satisifes the condition, then the execution continues. 
    /// Otherwise, execution is assumed to be halted
    /// </summary>
    public enum AssertionConditionType
    {
        /// <summary>
        /// Indicates that the marked parameter should be evaluated to true
        /// </summary>
        IsTrue = 0,

        /// <summary>
        /// Indicates that the marked parameter should be evaluated to false
        /// </summary>
        IsFalse = 1,

        /// <summary>
        /// Indicates that the marked parameter should be evaluated to null value
        /// </summary>
        IsNull = 2,

        /// <summary>
        /// Indicates that the marked parameter should be evaluated to not null value
        /// </summary>
        IsNotNull = 3,
    }

    /// <summary>
    /// Indicates that the value of marked element could be <c>null</c> sometimes, so the check for <c>null</c> is necessary before its usage
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class CanBeNullAttribute : Attribute { }

    /// <summary>
    /// Indicates that the value of marked element could never be <c>null</c>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class NotNullAttribute : Attribute { }
}
#endregion
