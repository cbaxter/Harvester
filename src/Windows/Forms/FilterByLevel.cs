using System;
using System.ComponentModel;
using System.Windows.Forms;
using Harvester.Core;
using Harvester.Core.Filters;
using Harvester.Properties;

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

namespace Harvester.Forms
{
    internal partial class FilterByLevel : FormBase
    {
        private readonly DynamicFilterExpression filter;

        public Boolean FilterEnabled { get { return filter.LevelFilter > SystemEventLevel.Trace; } }

        public FilterByLevel(DynamicFilterExpression dynamicFilter)
        {
            Verify.NotNull(dynamicFilter, "dynamicFilter");

            InitializeComponent();

            filter = dynamicFilter;
            resetButton.Click += (sender, e) => HandleEvent(() => traceLevel.Checked = true);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Font = SystemEventProperties.Default.Font;
            HandleEvent(PopulateLevel);
        }

        private void PopulateLevel()
        {
            switch (filter.LevelFilter)
            {
                case SystemEventLevel.Fatal: fatalLevel.Checked = true; break;
                case SystemEventLevel.Error: errorLevel.Checked = true; break;
                case SystemEventLevel.Warning: warningLevel.Checked = true; break;
                case SystemEventLevel.Information: informationLevel.Checked = true; break;
                case SystemEventLevel.Debug: debugLevel.Checked = true; break;
                default: traceLevel.Checked = true; break;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (DialogResult == DialogResult.OK)
                HandleEvent(ApplyFilterChanges);
        }

        private void ApplyFilterChanges()
        {
            if (fatalLevel.Checked)
                filter.LevelFilter = SystemEventLevel.Fatal;
            else if (errorLevel.Checked)
                filter.LevelFilter = SystemEventLevel.Error;
            else if (warningLevel.Checked)
                filter.LevelFilter = SystemEventLevel.Warning;
            else if (informationLevel.Checked)
                filter.LevelFilter = SystemEventLevel.Information;
            else if (debugLevel.Checked)
                filter.LevelFilter = SystemEventLevel.Debug;
            else
                filter.LevelFilter = SystemEventLevel.Trace;

            filter.Update();
        }
    }
}
