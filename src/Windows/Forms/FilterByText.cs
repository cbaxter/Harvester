using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
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
        private readonly DynamicFilterExpression filter;
        private readonly String property;

        public Boolean FilterEnabled
        {
            get { return filter.TextFilters.Any(placeholder => String.Compare(placeholder.Property, property, StringComparison.OrdinalIgnoreCase) == 0); }
        }

        public FilterByText(DynamicFilterExpression dynamicFilter, String propertyName)
        {
            Verify.NotNull(dynamicFilter, "dynamicFilter");
            Verify.NotWhitespace(propertyName, "propertyName");
            Verify.True(dynamicFilter.HasProperty(propertyName), "propertyName", String.Format("Unknown '{0}' property specified: {1}", typeof(SystemEvent).Name, propertyName));

            InitializeComponent();

            filter = dynamicFilter;
            property = propertyName;

            addFilter.Click += (sender, e) => HandleEvent(AddFilter);
            clearFilter.Click += (sender, e) => HandleEvent(ClearFilter);
            resetButton.Click += (sender, e) => HandleEvent(ResetFilter);
            filters.KeyDown += (sender, e) => HandleEvent(() => { if (e.KeyCode == Keys.Delete) RemoveFilter(); e.Handled = true; });
            filterText.KeyDown += (sender, e) => HandleEvent(() => { if (e.KeyCode == Keys.Enter) addFilter.PerformClick(); e.Handled = true; });
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Font = SystemEventProperties.Default.Font;
            Text = String.Format("Filter By {0}", property);
            HandleEvent(PopulateFilters);
        }

        private void PopulateFilters()
        {
            foreach (var knownFilter in filter.GetFriendlyFilterNames())
                filterType.Items.Add(knownFilter);

            foreach (var placeholder in filter.TextFilters.Where(placeholder => String.Compare(placeholder.Property, property, StringComparison.OrdinalIgnoreCase) == 0))
                filters.Items.Add(placeholder);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (DialogResult == DialogResult.OK)
                HandleEvent(ApplyFilterChanges);
        }

        private void ApplyFilterChanges()
        {
            filter.TextFilters = filter.TextFilters
                                       .Where(placeholder => String.Compare(placeholder.Property, property, StringComparison.OrdinalIgnoreCase) != 0)
                                       .Concat(filters.Items.Cast<FilterDefinition>())
                                       .ToList();
            filter.Update();
        }

        private void AddFilter()
        {
            var friendlyFilterName = filterType.SelectedItem as String;
            var text = filterText.Text;

            if (!String.IsNullOrWhiteSpace(friendlyFilterName) && !String.IsNullOrWhiteSpace(text))
            {
                var filterPlaceholder = negateFilter.Checked
                                            ? FilterDefinition.ForNegativeExpression(property, friendlyFilterName, text)
                                            : FilterDefinition.ForPositiveExpression(property, friendlyFilterName, text);

                filters.Items.Add(filterPlaceholder);
            }

            ClearFilter();
        }

        private void ClearFilter()
        {
            negateFilter.Checked = false;
            filterText.Text = String.Empty;
            filterType.SelectedItem = null;
            filterType.Focus();
        }

        private void RemoveFilter()
        {
            if (filters.SelectedItem == null)
                return;

            filters.Items.Remove(filters.SelectedItem);
        }

        private void ResetFilter()
        {
            filters.Items.Clear();
        }
    }
}
