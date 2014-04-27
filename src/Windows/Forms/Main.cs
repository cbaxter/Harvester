﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Harvester.Core;
using Harvester.Core.Configuration;
using Harvester.Core.Filters;
using Harvester.Core.Messaging;
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
    internal partial class Main : FormBase, IRenderEvents
    {
        private readonly IDictionary<Keys, Action> accelerators;
        private readonly List<ListViewItem> bufferedItems;
        private readonly DynamicFilterExpression filter;
        private readonly Timer flushBufferedItemsTimer;

        public Main()
        {
            InitializeComponent();

            bufferedItems = new List<ListViewItem>();
            mainToolStrip.Renderer = new CheckedButtonRenderer();
            splitContainer.Panel1MinSize = splitContainer.Panel2MinSize = 180;

            WireUpToolStrip();
            WireUpContextMenu();
            WireUpSystemEvents();

            accelerators = GetAccelerators();
            filter = new DynamicFilterExpression(Settings.GetFilter());
            flushBufferedItemsTimer = new Timer { Enabled = true, Interval = 100 };
            flushBufferedItemsTimer.Tick += (sender, e) => HandleEvent(FlushBufferedItems);
        }

        private void WireUpToolStrip()
        {
            closeButton.Click += (sender, e) => HandleEvent(Application.Exit);
            eraseButton.Click += (sender, e) => HandleEvent(ClearSystemEvents);
            scrollButton.Click += (sender, e) => HandleEvent(ToggleAutoScroll);
            colorButton.Click += (sender, e) => HandleEvent(ShowColorPicker);

            levelFilterButton.Click += (sender, e) => HandleEvent(ShowFilterByLevel);
            processFilterButton.Click += (sender, e) => HandleEvent(ShowFilterByProcess);
            applicationFilterButton.Click += (sender, e) => HandleEvent(ShowFilterByApplication);
            sourceFilterButton.Click += (sender, e) => HandleEvent(ShowFilterBySource);
            userFilterButton.Click += (sender, e) => HandleEvent(ShowFilterByUsername);
            messageFilterButton.Click += (sender, e) => HandleEvent(ShowFilterByMessage);

            saveButton.Click += (sender, e) => HandleEvent(SaveEventItemsToTextFile);

            searchButton.Click += (sender, e) => HandleEvent(StartSearch);
            searchText.KeyDown += (sender, e) => HandleEvent(() =>
                                                                 {
                                                                     if (e.KeyCode != Keys.Enter)
                                                                         return;

                                                                     if (searchText.Items.Count > 0 && Equals(searchText.Text, searchText.Items[0]))
                                                                         SearchNext();
                                                                     else
                                                                         StartSearch();
                                                                 });
        }

        private void WireUpContextMenu()
        {
            contextMenuStrip.Opening += (sender, e) => HandleEvent(() => ShowingContextMenu(e));
            displayIdColumn.Click += (sender, e) => HandleEvent(() => ToggleColumnDisplay(displayIdColumn, messageIdColumn));
            displayLevelColumn.Click += (sender, e) => HandleEvent(() => ToggleColumnDisplay(displayLevelColumn, levelColumn));
            displayTimestampColumn.Click += (sender, e) => HandleEvent(() => ToggleColumnDisplay(displayTimestampColumn, timestampColumn));
            displayProcessIdColumn.Click += (sender, e) => HandleEvent(() => ToggleColumnDisplay(displayProcessIdColumn, processIdColumn));
            displayProcessNameColumn.Click += (sender, e) => HandleEvent(() => ToggleColumnDisplay(displayProcessNameColumn, processNameColumn));
            displayThreadColumn.Click += (sender, e) => HandleEvent(() => ToggleColumnDisplay(displayThreadColumn, threadColumn));
            displaySourceColumn.Click += (sender, e) => HandleEvent(() => ToggleColumnDisplay(displaySourceColumn, sourceColumn));
            displayUserColumn.Click += (sender, e) => HandleEvent(() => ToggleColumnDisplay(displayUserColumn, userColumn));
        }

        private void WireUpSystemEvents()
        {
            systemEvents.SetFillColumn(messageColumn);
            systemEvents.ItemSelectionChanged += (sender, e) => HandleEvent(DisableAutoScroll);
            systemEvents.SelectedIndexChanged += (sender, e) => HandleEvent(DisplaySelectedSystemEvent);
        }

        private IDictionary<Keys, Action> GetAccelerators()
        {
            return new Dictionary<Keys, Action>
                       {
                           {Keys.Control | Keys.Shift | Keys.C, eraseButton.PerformClick}, 
                           {Keys.Control | Keys.Shift | Keys.D, colorButton.PerformClick}, 
                           {Keys.Control | Keys.Shift | Keys.V, scrollButton.PerformClick}, 
                           {Keys.Control | Keys.Shift | Keys.L, levelFilterButton.PerformClick},
                           {Keys.Control | Keys.Shift | Keys.P, processFilterButton.PerformClick},
                           {Keys.Control | Keys.Shift | Keys.A, applicationFilterButton.PerformClick}, 
                           {Keys.Control | Keys.Shift | Keys.S, sourceFilterButton.PerformClick},
                           {Keys.Control | Keys.Shift | Keys.U, userFilterButton.PerformClick}, 
                           {Keys.Control | Keys.Shift | Keys.M, messageFilterButton.PerformClick}, 
                           {Keys.Control | Keys.Shift | Keys.F, searchText.Focus}, 
                           {Keys.Control | Keys.F, searchButton.PerformClick},
                           {Keys.Control | Keys.S, saveButton.PerformClick}
                       };
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
                                ShellProperties.Default.SplitPosition = splitContainer.SplitterDistance;
                                ShellProperties.Default.WindowState = WindowState == FormWindowState.Minimized ? FormWindowState.Normal : WindowState;
                                if (WindowState == FormWindowState.Normal)
                                {
                                    ShellProperties.Default.WindowLocation = Location;
                                    ShellProperties.Default.WindowSize = Size;
                                }

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
                                WindowState = ShellProperties.Default.WindowState;
                                Location = workingArea.Contains(formLocation) ? formLocation : workingArea.Location;
                                splitContainer.SplitterDistance = Math.Min(splitContainer.Height - splitContainer.Panel2MinSize, ShellProperties.Default.SplitPosition);

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

        protected override Boolean ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Action accelerator;
            if (!accelerators.TryGetValue(keyData, out accelerator))
                return base.ProcessCmdKey(ref msg, keyData); ;

            accelerator.Invoke();
            return true;
        }

        #region Context Menu

        private void ShowingContextMenu(CancelEventArgs e)
        {
            var clientPosition = systemEvents.PointToClient(Cursor.Position);

            e.Cancel = clientPosition.Y > systemEvents.GetHeaderHeight();

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

        #region Clear System Events

        private void ClearSystemEvents()
        {
            lock (bufferedItems)
            {
                systemEvents.Items.Clear();
                systemEventControl.Clear();
            }
        }

        #endregion

        #region Save Event Items to Text File
        private void SaveEventItemsToTextFile()
        {
            lock (bufferedItems)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                saveFileDialog1.Title = "Save Events";
                saveFileDialog1.CheckPathExists = true;
                saveFileDialog1.DefaultExt = "txt";
                saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                System.Text.StringBuilder outputBuilder = new System.Text.StringBuilder();

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    foreach (ListViewItem row in systemEvents.Items)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            outputBuilder.Append(row.SubItems[i].Text.Replace(Environment.NewLine, " ") + " ");
                        }
                        outputBuilder.Append("\r\n");
                    }

                    System.IO.StreamWriter outFile = new System.IO.StreamWriter(saveFileDialog1.FileName);
                    outFile.Write(outputBuilder.ToString());
                    outFile.Close();
                } 

            }
        }

        #endregion  

        #region Show Color Picker

        private void ShowColorPicker()
        {
            using (var colorPicker = new ColorPicker())
            {
                if (colorPicker.ShowDialog(this) != DialogResult.OK)
                    return;

                systemEventControl.SetFont(SystemEventProperties.Default.Font);
                systemEvents.BackColor = SystemEventProperties.Default.PrimaryBackColor;
                systemEvents.Font = SystemEventProperties.Default.Font;

            }
        }

        #endregion

        #region Toggle Auto-Scroll

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

            if (systemEvents.Items.Count > 0)
                systemEvents.EnsureVisible(systemEvents.Items.Count - 1);
        }

        private void DisableAutoScroll()
        {
            scrollButton.Checked = false;
            scrollButton.Image = Resources.AutoScrollOff;
        }

        #endregion

        #region Filters

        private void ShowFilterByLevel()
        {
            using (var form = new FilterByLevel(filter))
            {
                form.ShowDialog(this);
                levelFilterButton.Checked = form.FilterEnabled;
            }
        }

        private void ShowFilterByProcess()
        {
            using (var form = new FilterByProcessId(filter))
            {
                form.ShowDialog(this);
                processFilterButton.Checked = form.FilterEnabled;
            }
        }

        private void ShowFilterByApplication()
        {
            using (var form = new FilterByApplication(filter))
            {
                form.ShowDialog(this);
                applicationFilterButton.Checked = form.FilterEnabled;
            }
        }

        private void ShowFilterBySource()
        {
            using (var form = new FilterByText(filter, "Source"))
            {
                form.ShowDialog(this);
                sourceFilterButton.Checked = form.FilterEnabled;
            }
        }

        private void ShowFilterByUsername()
        {
            using (var form = new FilterByText(filter, "Username"))
            {
                form.ShowDialog(this);
                userFilterButton.Checked = form.FilterEnabled;
            }
        }

        private void ShowFilterByMessage()
        {
            using (var form = new FilterByText(filter, "Message"))
            {
                form.ShowDialog(this);
                messageFilterButton.Checked = form.FilterEnabled;
            }
        }

        #endregion

        #region Search

        private void StartSearch()
        {
            var text = searchText.Text;

            if (String.IsNullOrWhiteSpace(text))
                return;

            searchText.Items.Remove(text);
            searchText.Items.Insert(0, text);

            while (searchText.Items.Count > 10)
                searchText.Items.RemoveAt(searchText.Items.Count - 1);

            SearchNext();
        }

        private void SearchNext()
        {
            var selectedIndex = systemEvents.SelectedIndices.Cast<Int32>().FirstOrDefault() + 1;

            if (systemEvents.Items.Count == 0)
                MessageBox.Show(Localization.NoMatchesFound, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                Search(searchText.Text, selectedIndex < systemEvents.Items.Count ? selectedIndex : 0);
        }

        private void Search(String text, Int32 startIndex)
        {
            ListViewItem match;

            while ((match = Find(text, startIndex)) == null)
            {
                var canWrap = startIndex != 0;
                if (canWrap)
                {
                    startIndex = 0;
                }
                else
                {
                    MessageBox.Show(Localization.NoMatchesFound, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            systemEvents.EnsureVisible(match.Index);
            systemEvents.SelectedItems.Clear();
            match.Selected = true;
        }

        private ListViewItem Find(String text, Int32 startIndex)
        {
            for (var i = startIndex; i < systemEvents.Items.Count; i++)
            {
                var item = systemEvents.Items[i];
                var e = item.Tag as SystemEvent;

                if (e != null && e.RawMessage.Value.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    return item;
            }

            return null;
        }

        #endregion

        #region System Event Rendering

        void IRenderEvents.Render(SystemEvent e)
        {
            if (filter.Exclude(e))
                return;

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

        private void DisplaySelectedSystemEvent()
        {
            var selectedItem = systemEvents.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            var e = selectedItem == null ? null : selectedItem.Tag as SystemEvent;

            if (e == null)
                systemEventControl.Clear();
            else
                systemEventControl.Bind(e);
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
