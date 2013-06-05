using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using Harvester.Core;

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
    internal class DoubleBufferedListView : ListView
    {
        private static readonly Int32 BufferTrimSize;
        private static readonly Int32 BufferSize;
        private ColumnHeader fillColumn;

        static DoubleBufferedListView()
        {
            var configuredBufferSize = ConfigurationManager.AppSettings["buffer-size"];

            BufferSize = String.IsNullOrWhiteSpace(configuredBufferSize) ? 32000 : Int32.Parse(configuredBufferSize);
            BufferTrimSize = Convert.ToInt32(BufferSize * 1.25);
        }

        public DoubleBufferedListView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            ColumnWidthChanged += OnResize;
            Resize += OnResize;
        }
        
        public Int32 GetHeaderHeight()
        {
            return NativeMethods.GetHeaderHeight(this);
        }

        public void OnResize(Object sender, EventArgs e)
        {
            try
            {
                UseWaitCursor = true;

                if (fillColumn == null)
                    return;

                SuspendLayout();

                var totalWidth = Columns.Cast<ColumnHeader>().Sum(column => column.Width);
                var messageColumnWidth = Width - (totalWidth - fillColumn.Width) - SystemInformation.VerticalScrollBarWidth - 4;
                var computedWidth = Math.Max(60, messageColumnWidth);

                if (fillColumn.Width == computedWidth)
                    return;

                fillColumn.Width = computedWidth;

                ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        public void SetFillColumn(ColumnHeader columnHeader)
        {
            Verify.True(Columns.Contains(columnHeader), "columnHeader", "Column must belong to this ListView instance.");

            fillColumn = columnHeader;

            OnResize(this, EventArgs.Empty);
        }

        public void AddItems(IEnumerable<ListViewItem> items)
        {
            if (items == null)
                return;

            BeginUpdate();
            SuspendLayout();

            if (Items.Count > BufferTrimSize)
            {
                while (Items.Count > BufferSize)
                    Items.RemoveAt(0);
            }

            foreach (var item in items)
                Items.Add(item);

            ResumeLayout();
            EndUpdate();
        }
    }
}
