using System;
using System.Globalization;
using System.Windows.Forms;
using Harvester.Core;
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

namespace Harvester.Forms
{
    public partial class Main : Form, IRenderEvents
    {
        private readonly SystemMonitor systemMonitor;

        public Main()
        {
            InitializeComponent();

            systemMonitor = new SystemMonitor(this);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            //systemMonitor.Dispose();
        }

        public void Render(SystemEvent e)
        {
            //TODO: enqueue messages for display, setup timer to update list view every 100ms

            var item = CreateListViewItem(e);

            if (systemEvents.InvokeRequired)
            {
                systemEvents.Invoke((Action) (() => systemEvents.Items.Add(item)));
            }
            else
            {
                systemEvents.Items.Add(item);
            }
        }

        private ListViewItem CreateListViewItem(SystemEvent e)
        {
            return new ListViewItem(new[]
                       {
                           e.MessageId.ToString(CultureInfo.InvariantCulture),
                           e.Timestamp.ToString("yyyy-MM-dd HH:mm:ss,fff"),
                           e.Level.ToString(),
                           e.ProcessId.ToString(CultureInfo.InvariantCulture),
                           e.ProcessName,
                           e.Thread,
                           e.Source,
                           e.Username,
                           e.Message
                       }) {Tag = e};
        }
    }
}
