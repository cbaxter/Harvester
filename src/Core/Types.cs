using System;
using System.Collections.Generic;
using System.Linq;

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

namespace Harvester.Core
{
    internal static class Types
    {
        public static readonly IEnumerable<Type> Comparable;
        public static readonly IEnumerable<Type> BuiltIn;
        public static readonly IEnumerable<Type> Simple;
        public static readonly IEnumerable<Type> Empty;

        static Types()
        {
            BuiltIn = new[]
                          {
                              typeof (Boolean),
                              typeof (Byte),
                              typeof (SByte),
                              typeof (Char),
                              typeof (Single),
                              typeof (Double),
                              typeof (Decimal),
                              typeof (Int16),
                              typeof (Int32),
                              typeof (Int64),
                              typeof (UInt16),
                              typeof (UInt32),
                              typeof (UInt64),
                              typeof (String),
                              typeof (Object)
                          };

            Simple = BuiltIn.Except(new[] { typeof(String), typeof(Object) }).ToArray();
            Comparable = Simple.Except(new[] { typeof(Boolean) }).ToArray();
            Empty = Enumerable.Empty<Type>();
        }
    }
}
