using System;
using System.ComponentModel;
using Harvester.Core;
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
    internal partial class FilterByLevel : FormBase
    {
        private SystemEventLevel Level { get; set; }
        public Boolean FilterEnabled { get { return Level > SystemEventLevel.Trace; } }

        public FilterByLevel()
        {
            InitializeComponent();

            resetButton.Click += (sender, e) => HandleEvent(() => traceLevel.Checked = true);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Font = SystemEventProperties.Default.Font;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (fatalLevel.Checked)
                Level = SystemEventLevel.Fatal;
            else if (errorLevel.Checked)
                Level = SystemEventLevel.Error;
            else if (warningLevel.Checked)
                Level = SystemEventLevel.Warning;
            else if (informationLevel.Checked)
                Level = SystemEventLevel.Information;
            else if (debugLevel.Checked)
                Level = SystemEventLevel.Debug;
            else
                Level = SystemEventLevel.Trace;
        }
    }
}
