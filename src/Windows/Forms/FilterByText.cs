using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Harvester.Core;
using Harvester.Core.Filters;
using Harvester.Properties;

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

namespace Harvester.Forms
{
    internal partial class FilterByText : FormBase
    {
        private static readonly IDictionary<String, Type> KnownFilters;

        private readonly String propertyName;

        public Boolean FilterEnabled
        {
            get { return false; }
        }

        static FilterByText()
        {
            KnownFilters = CoreAssembly.Reference
                .GetTypes()
                .Where(type => !type.IsAbstract && type.IsClass && typeof (IFilterMessages).IsAssignableFrom(type))
                .Select(type => (IFilterMessages) FormatterServices.GetUninitializedObject(type))
                .Where(filter => !filter.CompositeFilter)
                .ToDictionary(filter => filter.FriendlyName, filter => filter.GetType());
        }

        public FilterByText()
        {
            InitializeComponent();
        }

        public FilterByText(String propertyName)
            : this()
        {
            Verify.NotWhitespace(propertyName, "propertyName");

            this.propertyName = propertyName;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Font = SystemEventProperties.Default.Font;
            Text = String.Format("Filter By {0}", propertyName);

            PopulateFilterOptions();
        }

        private void PopulateFilterOptions()
        {
            foreach (var knownFilter in KnownFilters.Keys)
                filterType.Items.Add(knownFilter);
        }
    }
}
