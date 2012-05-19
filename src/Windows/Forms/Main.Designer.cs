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
            this.systemEvents = new System.Windows.Forms.ListView();
            this.messageIdColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timestampColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.levelColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.processIdColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.processNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.threadColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sourceColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.userColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.messageColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.feedbackLabel = new System.Windows.Forms.Label();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // systemEvents
            // 
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
            this.systemEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.systemEvents.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.systemEvents.FullRowSelect = true;
            this.systemEvents.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.systemEvents.Location = new System.Drawing.Point(0, 0);
            this.systemEvents.MultiSelect = false;
            this.systemEvents.Name = "systemEvents";
            this.systemEvents.Size = new System.Drawing.Size(1008, 705);
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
            this.toolStripContainer.ContentPanel.Controls.Add(this.systemEvents);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(1008, 705);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(1008, 730);
            this.toolStripContainer.TabIndex = 2;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.toolStripContainer);
            this.Controls.Add(this.feedbackLabel);
            this.Name = "Main";
            this.Text = "Main";
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView systemEvents;
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
    }
}