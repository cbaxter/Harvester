using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class ListenersSection : ConfigurationSection
    {
        [ConfigurationProperty(null, Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public ListenerElementCollection Listeners { get { return (ListenerElementCollection)base[""] ?? new ListenerElementCollection(); } }
    }

    [ConfigurationCollection(typeof(ListenerElement), AddItemName = "listener", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class ListenerElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ListenerElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((ListenerElement)element).Name;
        }
    }

    public class ListenerElement : ConfigurationElement, IConfigureListeners
    {
        private readonly IDictionary<String, String> extendedProperties = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);

        [ConfigurationProperty("type", IsRequired = true)]
        public String TypeName { get { return (String)base["type"]; }  }

        [ConfigurationProperty("name", IsRequired = true)]
        public String Name { get { return (String)base["name"]; }  }

        [ConfigurationProperty("mutex", IsRequired = true)]
        public String Mutex { get { return (String)base["mutex"]; } }

        [ConfigurationProperty("elevatedOnly", IsRequired = false, DefaultValue = false)]
        public Boolean ElevatedOnly { get { return (Boolean)base["elevatedOnly"]; } }

        public String GetExtendedProperty(String property)
        {
            if (!extendedProperties.ContainsKey(property))
                throw new ArgumentException(String.Format(Localization.ExtendedPropertyNotDefined, property), "property");

            return extendedProperties[property] ?? String.Empty;
        }

        public Boolean HasExtendedProperty(String property)
        {
            return extendedProperties.ContainsKey(property);
        }

        protected override Boolean OnDeserializeUnrecognizedAttribute(String name, String value)
        {
            extendedProperties.Add(name, value);
            
            return true;
        }
    }
}
