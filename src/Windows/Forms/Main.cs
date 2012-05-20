using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Harvester.Core;
using Harvester.Core.Messaging;
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
    internal partial class Main : FormBase, IRenderEvents
    {

        private readonly List<ListViewItem> bufferedItems;
        private readonly Timer flushBufferedItemsTimer;

        public Main()
        {
            InitializeComponent();

            bufferedItems = new List<ListViewItem>();
            mainToolStrip.Renderer = new CheckedButtonRenderer();

            // Setup render timer to only update list view every 100ms.
            flushBufferedItemsTimer = new Timer { Enabled = true, Interval = 100 };
            flushBufferedItemsTimer.Tick += (sender, e) => HandleEvent(FlushBufferedItems);

            // Setup system events list view to stop auto-scroll if selection changes.
            systemEvents.SetFillColumn(messageColumn);
            systemEvents.ItemSelectionChanged += (sender, e) => HandleEvent(DisableAutoScroll);

            // Wire-up tool strip button click event handlers.
            closeButton.Click += (sender, e) => HandleEvent(Application.Exit);
            eraseButton.Click += (sender, e) => HandleEvent(ClearSystemEvents);
            scrollButton.Click += (sender, e) => HandleEvent(ToggleAutoScroll);
            colorButton.Click += (sender, e) => HandleEvent(ShowColorPicker);

            // Wire-up context menu item click handlers.
            contextMenuStrip.Opening += (sender, e) => HandleEvent(ShowingContextMenu);
            displayIdColumn.Click += (sender, e) => HandleEvent(() => ToggleColumnDisplay(displayIdColumn, messageIdColumn));
            displayLevelColumn.Click += (sender, e) => HandleEvent(() => ToggleColumnDisplay(displayLevelColumn, levelColumn));
            displayTimestampColumn.Click += (sender, e) => HandleEvent(() => ToggleColumnDisplay(displayTimestampColumn, timestampColumn));
            displayProcessIdColumn.Click += (sender, e) => HandleEvent(() => ToggleColumnDisplay(displayProcessIdColumn, processIdColumn));
            displayProcessNameColumn.Click += (sender, e) => HandleEvent(() => ToggleColumnDisplay(displayProcessNameColumn, processNameColumn));
            displayThreadColumn.Click += (sender, e) => HandleEvent(() => ToggleColumnDisplay(displayThreadColumn, threadColumn));
            displaySourceColumn.Click += (sender, e) => HandleEvent(() => ToggleColumnDisplay(displaySourceColumn, sourceColumn));
            displayUserColumn.Click += (sender, e) => HandleEvent(() => ToggleColumnDisplay(displayUserColumn, userColumn));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            HandleEvent(() =>
                            {
                                // Stop the render timer.
                                flushBufferedItemsTimer.Enabled = false;
                                flushBufferedItemsTimer.Dispose();

                                // Save Shell Dimensions
                                ShellProperties.Default.WindowSize = Size;
                                ShellProperties.Default.WindowLocation = Location;
                                ShellProperties.Default.WindowState = WindowState == FormWindowState.Minimized ? FormWindowState.Normal : WindowState;

                                ShellProperties.Default.Save();

                                // Save System Event Display Properties
                                SystemEventProperties.Default.IdWidth = messageIdColumn.Width;
                                SystemEventProperties.Default.TimestampWidth = timestampColumn.Width;
                                SystemEventProperties.Default.LevelWidth = levelColumn.Width;
                                SystemEventProperties.Default.ProcessIdWidth = processIdColumn.Width;
                                SystemEventProperties.Default.ProcessNameWidth = processNameColumn.Width;
                                SystemEventProperties.Default.ThreadWidth = threadColumn.Width;
                                SystemEventProperties.Default.SourceWidth = sourceColumn.Width;
                                SystemEventProperties.Default.UserWidth = userColumn.Width;

                                SystemEventProperties.Default.Save();
                            });
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            HandleEvent(() =>
                            {
                                var version = CoreAssembly.Reference.GetName().Version;

                                Text = String.Format("{0} v{1}.{2}.{3}", Application.ProductName, version.Major, version.Minor, version.Build);
                                Icon = Resources.Harvester;

                                var formSize = ShellProperties.Default.WindowSize;
                                var formLocation = ShellProperties.Default.WindowLocation;
                                var formDimensions = new Rectangle(formLocation, formSize);
                                var workingArea = Screen.GetWorkingArea(formDimensions);

                                //Load Shell Dimensions
                                Size = formSize;
                                Location = workingArea.Contains(formLocation) ? formLocation : workingArea.Location;
                                WindowState = ShellProperties.Default.WindowState;

                                //Load System Event Display Properties
                                messageIdColumn.Width = SystemEventProperties.Default.IdWidth;
                                timestampColumn.Width = SystemEventProperties.Default.TimestampWidth;
                                levelColumn.Width = SystemEventProperties.Default.LevelWidth;
                                processIdColumn.Width = SystemEventProperties.Default.ProcessIdWidth;
                                processNameColumn.Width = SystemEventProperties.Default.ProcessNameWidth;
                                threadColumn.Width = SystemEventProperties.Default.ThreadWidth;
                                sourceColumn.Width = SystemEventProperties.Default.SourceWidth;
                                userColumn.Width = SystemEventProperties.Default.UserWidth;

                                systemEvents.Font = SystemEventProperties.Default.Font;
                                systemEvents.BackColor = SystemEventProperties.Default.PrimaryBackColor;
                            });
        }

        #region Context Menu

        private void ShowingContextMenu()
        {
            displayIdColumn.Checked = messageIdColumn.Width > 0;
            displayTimestampColumn.Checked = timestampColumn.Width > 0;
            displayLevelColumn.Checked = levelColumn.Width > 0;
            displayProcessIdColumn.Checked = processIdColumn.Width > 0;
            displayProcessNameColumn.Checked = processNameColumn.Width > 0;
            displayThreadColumn.Checked = threadColumn.Width > 0;
            displaySourceColumn.Checked = sourceColumn.Width > 0;
            displayUserColumn.Checked = userColumn.Width > 0;
        }

        private void ToggleColumnDisplay(ToolStripMenuItem item, ColumnHeader columnHeader)
        {
            item.Checked = !item.Checked;
            columnHeader.Width = item.Checked ? 120 : 0;
        }

        #endregion

        #region Tool Strip Buttons

        private void ClearSystemEvents()
        {
            lock (bufferedItems)
            {
                systemEvents.Items.Clear();
            }
        }

        private void ShowColorPicker()
        {
            using (var colorPicker = new ColorPicker())
            {
                if (colorPicker.ShowDialog(this) != DialogResult.OK)
                    return;

                systemEvents.BackColor = SystemEventProperties.Default.PrimaryBackColor;
                systemEvents.Font = SystemEventProperties.Default.Font;
            }
        }

        private void ToggleAutoScroll()
        {
            if (scrollButton.Checked)
                DisableAutoScroll();
            else
                EnableAutoScroll();
        }

        private void EnableAutoScroll()
        {
            scrollButton.Checked = true;
            scrollButton.Image = Resources.AutoScrollOn;
        }

        private void DisableAutoScroll()
        {
            scrollButton.Checked = false;
            scrollButton.Image = Resources.AutoScrollOff;
        }

        #endregion

        #region System Event Rendering

        void IRenderEvents.Render(SystemEvent e)
        {
            lock (bufferedItems)
            {
                bufferedItems.Add(CreateListViewItem(e));
            }
        }

        private ListViewItem CreateListViewItem(SystemEvent e)
        {
            return new ListViewItem(new[]
                       {
                           e.MessageId.ToString(CultureInfo.InvariantCulture),
                           e.Timestamp.ToString("HH:mm:ss,fff"),
                           e.Level.ToString(),
                           e.ProcessId.ToString(CultureInfo.InvariantCulture),
                           e.ProcessName,
                           e.Thread,
                           e.Source,
                           e.Username,
                           e.Message
                       }) { Tag = e, ForeColor = GetForegroundColor(e.Level), BackColor = GetBackgroundColor(e.Level) };
        }

        private static Color GetBackgroundColor(SystemEventLevel level)
        {
            switch (level)
            {
                case SystemEventLevel.Fatal: return SystemEventProperties.Default.FatalBackColor;
                case SystemEventLevel.Error: return SystemEventProperties.Default.ErrorBackColor;
                case SystemEventLevel.Warning: return SystemEventProperties.Default.WarningBackColor;
                case SystemEventLevel.Information: return SystemEventProperties.Default.InformationBackColor;
                case SystemEventLevel.Debug: return SystemEventProperties.Default.DebugBackColor;
                default: return SystemEventProperties.Default.TraceBackColor;
            }
        }

        private static Color GetForegroundColor(SystemEventLevel level)
        {
            switch (level)
            {
                case SystemEventLevel.Fatal: return SystemEventProperties.Default.FatalForeColor;
                case SystemEventLevel.Error: return SystemEventProperties.Default.ErrorForeColor;
                case SystemEventLevel.Warning: return SystemEventProperties.Default.WarningForeColor;
                case SystemEventLevel.Information: return SystemEventProperties.Default.InformationForeColor;
                case SystemEventLevel.Debug: return SystemEventProperties.Default.DebugForeColor;
                default: return SystemEventProperties.Default.TraceForeColor;
            }
        }

        private void FlushBufferedItems()
        {
            lock (bufferedItems)
            {
                if (bufferedItems.Count == 0)
                    return;

                systemEvents.AddItems(bufferedItems);
                bufferedItems.Clear();

                if (scrollButton.Checked && systemEvents.Items.Count > 0)
                    systemEvents.EnsureVisible(systemEvents.Items.Count - 1);
            }
        }

        #endregion

    }
}
