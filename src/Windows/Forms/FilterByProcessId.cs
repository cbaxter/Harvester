using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    internal partial class FilterByProcessId : FormBase
    {
        public Boolean FilterEnabled { get { return processesList.CheckedItems.Count > 0; } }

        public FilterByProcessId()
        {
            InitializeComponent();

            resetButton.Click += (sender, e) => HandleEvent(ClearChecked);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Font = SystemEventProperties.Default.Font;
            PopulateProcessList();
        }

        private void PopulateProcessList()
        {
            var processes = new List<ProcessInfo>();

            foreach (var process in Process.GetProcesses())
            {
                using (process)
                    processes.Add(new ProcessInfo(process));
            }

            processesList.Items.AddRange(processes.OrderBy(info => info).Cast<Object>().ToArray());
        }

        private void ClearChecked()
        {
            processesList.ClearSelected();
            foreach (Int32 index in processesList.CheckedIndices)
                processesList.SetItemChecked(index, false);
        }

        private struct ProcessInfo : IComparable<ProcessInfo>
        {
            private readonly Int32 id;
            private readonly String name;

            public ProcessInfo(Process process)
            {
                id = process.Id;
                name = process.ProcessName;
            }

            public Int32 CompareTo(ProcessInfo other)
            {
                var nameCompare = String.Compare(name, other.name, StringComparison.OrdinalIgnoreCase);

                return nameCompare == 0 ? id.CompareTo(other.id) : nameCompare;
            }

            public override String ToString()
            {
                return String.Format("{0} ({1})", name, id);
            }
        }
    }
}
