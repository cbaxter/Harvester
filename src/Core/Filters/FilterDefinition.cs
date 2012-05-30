using System;
using System.Collections.Generic;
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
    public class FilterDefinition : IHaveExtendedProperties
    {
        public static readonly FilterDefinition Empty = new FilterDefinition(String.Empty, String.Empty, String.Empty, false);

        private readonly IDictionary<String, String> extendedProperties;
        private readonly String friendlyDescription;
        private readonly String friendlyName;
        private readonly String property;
        private readonly Boolean negate;

        public Boolean Negate { get { return negate; } }
        public String Property { get { return property; } }
        public String FriendlyName { get { return friendlyName; } }
        
        private FilterDefinition(String propertyName, String friendlyFilterName, String value, Boolean negateFilter)
        {
            negate = negateFilter;
            property = propertyName;
            friendlyName = friendlyFilterName;
            extendedProperties = new Dictionary<String, String> { { "property", propertyName }, { "value", value } };
            friendlyDescription = String.Format("{0} \"{1}\" is {2}", friendlyFilterName, value, negateFilter ? "False" : "True").Trim();
        }

        public String GetExtendedProperty(String propertyName)
        {
            if (!extendedProperties.ContainsKey(propertyName))
                throw new ArgumentException(String.Format(Localization.ExtendedPropertyNotDefined, propertyName), "propertyName");

            return extendedProperties[propertyName];
        }

        public Boolean HasExtendedProperty(String propertyName)
        {
            return extendedProperties.ContainsKey(propertyName);
        }

        public static FilterDefinition Create(String propertyName, Object value)
        {
            Verify.NotWhitespace(propertyName, "propertyName");

            return new FilterDefinition(propertyName, String.Empty, value.ToString(), false);
        }

        public static FilterDefinition ForPositiveExpression(String propertyName, String filterFriendlyName, String value)
        {
            Verify.NotWhitespace(filterFriendlyName, "filterFriendlyName");
            Verify.NotWhitespace(propertyName, "propertyName");

            return new FilterDefinition(propertyName, filterFriendlyName, value, false);
        }

        public static FilterDefinition ForNegativeExpression(String propertyName, String filterFriendlyName, String value)
        {
            Verify.NotWhitespace(filterFriendlyName, "filterFriendlyName");
            Verify.NotWhitespace(propertyName, "propertyName");

            return new FilterDefinition(propertyName, filterFriendlyName, value, true);
        }
        
        public override string ToString()
        {
            return friendlyDescription;
        }
    }
}
