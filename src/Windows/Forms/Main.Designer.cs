namespace Harvester.Forms
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.systemEvents = new Harvester.Forms.DoubleBufferedListView();
            this.messageIdColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timestampColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.levelColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.processIdColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.processNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.threadColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sourceColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.userColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.messageColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.displayIdColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.displayTimestampColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.displayLevelColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.displayProcessIdColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.displayProcessNameColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.displayThreadColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.displaySourceColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.displayUserColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.feedbackLabel = new System.Windows.Forms.Label();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.systemEventControl = new Harvester.Forms.SystemEventControl();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.closeButton = new System.Windows.Forms.ToolStripButton();
            this.separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.eraseButton = new System.Windows.Forms.ToolStripButton();
            this.colorButton = new System.Windows.Forms.ToolStripButton();
            this.scrollButton = new System.Windows.Forms.ToolStripButton();
            this.separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.levelFilterButton = new System.Windows.Forms.ToolStripButton();
            this.processFilterButton = new System.Windows.Forms.ToolStripButton();
            this.applicationFilterButton = new System.Windows.Forms.ToolStripButton();
            this.sourceFilterButton = new System.Windows.Forms.ToolStripButton();
            this.userFilterButton = new System.Windows.Forms.ToolStripButton();
            this.messageFilterButton = new System.Windows.Forms.ToolStripButton();
            this.separator3 = new System.Windows.Forms.ToolStripSeparator();
            this.searchText = new System.Windows.Forms.ToolStripComboBox();
            this.searchButton = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // systemEvents
            // 
            this.systemEvents.BackColor = System.Drawing.Color.Black;
            this.systemEvents.CausesValidation = false;
            this.systemEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.messageIdColumn,
            this.timestampColumn,
            this.levelColumn,
            this.processIdColumn,
            this.processNameColumn,
            this.threadColumn,
            this.sourceColumn,
            this.userColumn,
            this.messageColumn});
            this.systemEvents.ContextMenuStrip = this.contextMenuStrip;
            this.systemEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.systemEvents.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.systemEvents.ForeColor = System.Drawing.Color.DarkCyan;
            this.systemEvents.FullRowSelect = true;
            this.systemEvents.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.systemEvents.HideSelection = false;
            this.systemEvents.Location = new System.Drawing.Point(0, 0);
            this.systemEvents.MultiSelect = false;
            this.systemEvents.Name = "systemEvents";
            this.systemEvents.ShowItemToolTips = true;
            this.systemEvents.Size = new System.Drawing.Size(1008, 500);
            this.systemEvents.TabIndex = 0;
            this.systemEvents.UseCompatibleStateImageBehavior = false;
            this.systemEvents.View = System.Windows.Forms.View.Details;
            // 
            // messageIdColumn
            // 
            this.messageIdColumn.Text = "Id";
            // 
            // timestampColumn
            // 
            this.timestampColumn.Text = "Timestamp";
            this.timestampColumn.Width = 100;
            // 
            // levelColumn
            // 
            this.levelColumn.Text = "Level";
            this.levelColumn.Width = 100;
            // 
            // processIdColumn
            // 
            this.processIdColumn.Text = "PID";
            this.processIdColumn.Width = 100;
            // 
            // processNameColumn
            // 
            this.processNameColumn.Text = "Process Name";
            this.processNameColumn.Width = 100;
            // 
            // threadColumn
            // 
            this.threadColumn.Text = "Thread";
            this.threadColumn.Width = 100;
            // 
            // sourceColumn
            // 
            this.sourceColumn.Text = "Source";
            this.sourceColumn.Width = 100;
            // 
            // userColumn
            // 
            this.userColumn.Text = "User";
            this.userColumn.Width = 100;
            // 
            // messageColumn
            // 
            this.messageColumn.Text = "Message";
            this.messageColumn.Width = 100;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayIdColumn,
            this.displayTimestampColumn,
            this.displayLevelColumn,
            this.displayProcessIdColumn,
            this.displayProcessNameColumn,
            this.displayThreadColumn,
            this.displaySourceColumn,
            this.displayUserColumn});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(150, 180);
            // 
            // displayIdColumn
            // 
            this.displayIdColumn.Name = "displayIdColumn";
            this.displayIdColumn.Size = new System.Drawing.Size(149, 22);
            this.displayIdColumn.Text = "Id";
            // 
            // displayTimestampColumn
            // 
            this.displayTimestampColumn.Name = "displayTimestampColumn";
            this.displayTimestampColumn.Size = new System.Drawing.Size(149, 22);
            this.displayTimestampColumn.Text = "Timestamp";
            // 
            // displayLevelColumn
            // 
            this.displayLevelColumn.Name = "displayLevelColumn";
            this.displayLevelColumn.Size = new System.Drawing.Size(149, 22);
            this.displayLevelColumn.Text = "Level";
            // 
            // displayProcessIdColumn
            // 
            this.displayProcessIdColumn.Name = "displayProcessIdColumn";
            this.displayProcessIdColumn.Size = new System.Drawing.Size(149, 22);
            this.displayProcessIdColumn.Text = "Process Id";
            // 
            // displayProcessNameColumn
            // 
            this.displayProcessNameColumn.Name = "displayProcessNameColumn";
            this.displayProcessNameColumn.Size = new System.Drawing.Size(149, 22);
            this.displayProcessNameColumn.Text = "Process Name";
            // 
            // displayThreadColumn
            // 
            this.displayThreadColumn.Name = "displayThreadColumn";
            this.displayThreadColumn.Size = new System.Drawing.Size(149, 22);
            this.displayThreadColumn.Text = "Thread";
            // 
            // displaySourceColumn
            // 
            this.displaySourceColumn.Name = "displaySourceColumn";
            this.displaySourceColumn.Size = new System.Drawing.Size(149, 22);
            this.displaySourceColumn.Text = "Source";
            // 
            // displayUserColumn
            // 
            this.displayUserColumn.Name = "displayUserColumn";
            this.displayUserColumn.Size = new System.Drawing.Size(149, 22);
            this.displayUserColumn.Text = "User";
            // 
            // feedbackLabel
            // 
            this.feedbackLabel.AutoSize = true;
            this.feedbackLabel.Location = new System.Drawing.Point(12, 708);
            this.feedbackLabel.Name = "feedbackLabel";
            this.feedbackLabel.Size = new System.Drawing.Size(0, 13);
            this.feedbackLabel.TabIndex = 1;
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.splitContainer);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(1008, 705);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(1008, 730);
            this.toolStripContainer.TabIndex = 2;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.mainToolStrip);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.systemEvents);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.systemEventControl);
            this.splitContainer.Size = new System.Drawing.Size(1008, 705);
            this.splitContainer.SplitterDistance = 500;
            this.splitContainer.TabIndex = 1;
            // 
            // systemEventControl
            // 
            this.systemEventControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.systemEventControl.Location = new System.Drawing.Point(0, 0);
            this.systemEventControl.Name = "systemEventControl";
            this.systemEventControl.Padding = new System.Windows.Forms.Padding(3);
            this.systemEventControl.Size = new System.Drawing.Size(1008, 201);
            this.systemEventControl.TabIndex = 0;
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeButton,
            this.separator1,
            this.eraseButton,
            this.colorButton,
            this.scrollButton,
            this.separator2,
            this.levelFilterButton,
            this.processFilterButton,
            this.applicationFilterButton,
            this.sourceFilterButton,
            this.userFilterButton,
            this.messageFilterButton,
            this.separator3,
            this.searchText,
            this.searchButton});
            this.mainToolStrip.Location = new System.Drawing.Point(3, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(507, 25);
            this.mainToolStrip.TabIndex = 0;
            // 
            // closeButton
            // 
            this.closeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.closeButton.Image = global::Harvester.Properties.Resources.Close;
            this.closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(23, 22);
            this.closeButton.Text = "Close Application";
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(6, 25);
            // 
            // eraseButton
            // 
            this.eraseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.eraseButton.Image = global::Harvester.Properties.Resources.Erase;
            this.eraseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eraseButton.Name = "eraseButton";
            this.eraseButton.Size = new System.Drawing.Size(23, 22);
            this.eraseButton.Text = "Clear System Events";
            // 
            // colorButton
            // 
            this.colorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.colorButton.Image = global::Harvester.Properties.Resources.ColorPicker;
            this.colorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(23, 22);
            this.colorButton.Text = "Customize System Event Display";
            // 
            // scrollButton
            // 
            this.scrollButton.BackColor = System.Drawing.SystemColors.Control;
            this.scrollButton.Checked = true;
            this.scrollButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.scrollButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.scrollButton.Image = global::Harvester.Properties.Resources.AutoScrollOn;
            this.scrollButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.scrollButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.scrollButton.Name = "scrollButton";
            this.scrollButton.Size = new System.Drawing.Size(23, 22);
            this.scrollButton.Text = "Toggle Auto-Scroll";
            // 
            // separator2
            // 
            this.separator2.Name = "separator2";
            this.separator2.Size = new System.Drawing.Size(6, 25);
            // 
            // levelFilterButton
            // 
            this.levelFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.levelFilterButton.Image = global::Harvester.Properties.Resources.FilterLogLevels;
            this.levelFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.levelFilterButton.Name = "levelFilterButton";
            this.levelFilterButton.Size = new System.Drawing.Size(23, 22);
            this.levelFilterButton.Text = "Filter by Level";
            // 
            // processFilterButton
            // 
            this.processFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.processFilterButton.Image = global::Harvester.Properties.Resources.FilterProcesses;
            this.processFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.processFilterButton.Name = "processFilterButton";
            this.processFilterButton.Size = new System.Drawing.Size(23, 22);
            this.processFilterButton.Text = "Filter by Process";
            // 
            // applicationFilterButton
            // 
            this.applicationFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.applicationFilterButton.Image = global::Harvester.Properties.Resources.FilterApplications;
            this.applicationFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.applicationFilterButton.Name = "applicationFilterButton";
            this.applicationFilterButton.Size = new System.Drawing.Size(23, 22);
            this.applicationFilterButton.Text = "Filter by Application";
            // 
            // sourceFilterButton
            // 
            this.sourceFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sourceFilterButton.Image = global::Harvester.Properties.Resources.FilterSources;
            this.sourceFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sourceFilterButton.Name = "sourceFilterButton";
            this.sourceFilterButton.Size = new System.Drawing.Size(23, 22);
            this.sourceFilterButton.Text = "Filter by Source";
            // 
            // userFilterButton
            // 
            this.userFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.userFilterButton.Image = global::Harvester.Properties.Resources.FilterUsers;
            this.userFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.userFilterButton.Name = "userFilterButton";
            this.userFilterButton.Size = new System.Drawing.Size(23, 22);
            this.userFilterButton.Text = "Filter by Username";
            // 
            // messageFilterButton
            // 
            this.messageFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.messageFilterButton.Image = global::Harvester.Properties.Resources.FilterMessages;
            this.messageFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.messageFilterButton.Name = "messageFilterButton";
            this.messageFilterButton.Size = new System.Drawing.Size(23, 22);
            this.messageFilterButton.Text = "Filter by Message";
            // 
            // separator3
            // 
            this.separator3.Name = "separator3";
            this.separator3.Size = new System.Drawing.Size(6, 25);
            // 
            // searchText
            // 
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(200, 25);
            // 
            // searchButton
            // 
            this.searchButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchButton.Image = global::Harvester.Properties.Resources.Search;
            this.searchButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(23, 22);
            this.searchButton.Text = "Search";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.toolStripContainer);
            this.Controls.Add(this.feedbackLabel);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Main";
            this.Text = "Main";
            this.contextMenuStrip.ResumeLayout(false);
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Harvester.Forms.DoubleBufferedListView systemEvents;
        private System.Windows.Forms.ColumnHeader messageIdColumn;
        private System.Windows.Forms.ColumnHeader timestampColumn;
        private System.Windows.Forms.ColumnHeader levelColumn;
        private System.Windows.Forms.ColumnHeader processIdColumn;
        private System.Windows.Forms.ColumnHeader processNameColumn;
        private System.Windows.Forms.ColumnHeader threadColumn;
        private System.Windows.Forms.ColumnHeader sourceColumn;
        private System.Windows.Forms.ColumnHeader userColumn;
        private System.Windows.Forms.ColumnHeader messageColumn;
        private System.Windows.Forms.Label feedbackLabel;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton closeButton;
        private System.Windows.Forms.ToolStripSeparator separator1;
        private System.Windows.Forms.ToolStripButton eraseButton;
        private System.Windows.Forms.ToolStripButton colorButton;
        private System.Windows.Forms.ToolStripButton scrollButton;
        private System.Windows.Forms.ToolStripSeparator separator2;
        private System.Windows.Forms.ToolStripButton levelFilterButton;
        private System.Windows.Forms.ToolStripButton processFilterButton;
        private System.Windows.Forms.ToolStripButton applicationFilterButton;
        private System.Windows.Forms.ToolStripButton sourceFilterButton;
        private System.Windows.Forms.ToolStripButton userFilterButton;
        private System.Windows.Forms.ToolStripButton messageFilterButton;
        private System.Windows.Forms.ToolStripSeparator separator3;
        private System.Windows.Forms.ToolStripButton searchButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem displayIdColumn;
        private System.Windows.Forms.ToolStripMenuItem displayTimestampColumn;
        private System.Windows.Forms.ToolStripMenuItem displayLevelColumn;
        private System.Windows.Forms.ToolStripMenuItem displayProcessIdColumn;
        private System.Windows.Forms.ToolStripMenuItem displayProcessNameColumn;
        private System.Windows.Forms.ToolStripMenuItem displayThreadColumn;
        private System.Windows.Forms.ToolStripMenuItem displaySourceColumn;
        private System.Windows.Forms.ToolStripMenuItem displayUserColumn;
        private System.Windows.Forms.SplitContainer splitContainer;
        private SystemEventControl systemEventControl;
        private System.Windows.Forms.ToolStripComboBox searchText;
    }
}