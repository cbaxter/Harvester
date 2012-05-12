using System;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using Harvester.Core.Filters;
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

namespace Harvester.Core.Configuration
{
    public class FiltersSection : ConfigurationSection, IHaveExtendedProperties
    {
        [ConfigurationProperty(null, Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public FilterElementCollection Filters { get { return (FilterElementCollection)base[""] ?? new FilterElementCollection(); } }

        public Func<SystemEvent, Boolean> CompileFilter()
        {
            var systemEvent = Expression.Parameter(typeof(SystemEvent), "e");
            var filterParameters = new FilterParameters { systemEvent };
            var filters = Filters.Cast<FilterElement>().Select(element => element.GetFilter(filterParameters));

            return Expression.Lambda<Func<SystemEvent, Boolean>>(
                       new AndAlsoFilter(this, filters).CreateExpression(filterParameters),
                       systemEvent
                   ).Compile();
        }

        public String GetExtendedProperty(String property)
        {
            throw new ArgumentException(String.Format(Localization.ExtendedPropertyNotDefined, property), "property");
        }

        public Boolean HasExtendedProperty(String property)
        {
            return false;
        }
    }

    [ConfigurationCollection(typeof(FilterElement), AddItemName = "filter", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class FilterElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FilterElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return element.GetHashCode();
        }
    }

    public class FilterElement : ExtendableConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true)]
        public String TypeName { get { return (String)base["type"]; } }

        [ConfigurationProperty("", IsRequired = false, IsDefaultCollection = true)]
        public FilterElementCollection Filters { get { return this[""] as FilterElementCollection ?? new FilterElementCollection(); } }

        public IFilterMessages GetFilter(FilterParameters parameters)
        {
            var filterType = Type.GetType(String.Format("{0}.{1}Filter", typeof(FilterParameters).Namespace, TypeName), false) ?? Type.GetType(TypeName, true);
            var filters = Filters.Cast<FilterElement>().Select(element => element.GetFilter(parameters));

            return (IFilterMessages)Activator.CreateInstance(filterType, this, filters);
        }
    }
}
