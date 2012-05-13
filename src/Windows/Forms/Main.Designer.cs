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
            this.messageId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.processId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.processName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.thread = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.source = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.user = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // systemEvents
            // 
            this.systemEvents.CausesValidation = false;
            this.systemEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.messageId,
            this.timestamp,
            this.level,
            this.processId,
            this.processName,
            this.thread,
            this.source,
            this.user,
            this.message});
            this.systemEvents.FullRowSelect = true;
            this.systemEvents.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.systemEvents.Location = new System.Drawing.Point(12, 85);
            this.systemEvents.MultiSelect = false;
            this.systemEvents.Name = "systemEvents";
            this.systemEvents.Size = new System.Drawing.Size(984, 430);
            this.systemEvents.TabIndex = 0;
            this.systemEvents.UseCompatibleStateImageBehavior = false;
            this.systemEvents.View = System.Windows.Forms.View.Details;
            // 
            // messageId
            // 
            this.messageId.Text = "Id";
            // 
            // timestamp
            // 
            this.timestamp.Text = "Timestamp";
            this.timestamp.Width = 100;
            // 
            // level
            // 
            this.level.Text = "Level";
            this.level.Width = 100;
            // 
            // processId
            // 
            this.processId.Text = "PID";
            this.processId.Width = 100;
            // 
            // processName
            // 
            this.processName.Text = "Process Name";
            this.processName.Width = 100;
            // 
            // thread
            // 
            this.thread.Text = "Thread";
            this.thread.Width = 100;
            // 
            // source
            // 
            this.source.Text = "Source";
            this.source.Width = 100;
            // 
            // user
            // 
            this.user.Text = "User";
            this.user.Width = 100;
            // 
            // message
            // 
            this.message.Text = "Message";
            this.message.Width = 100;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.systemEvents);
            this.Name = "Main";
            this.Text = "Main";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView systemEvents;
        private System.Windows.Forms.ColumnHeader messageId;
        private System.Windows.Forms.ColumnHeader timestamp;
        private System.Windows.Forms.ColumnHeader level;
        private System.Windows.Forms.ColumnHeader processId;
        private System.Windows.Forms.ColumnHeader processName;
        private System.Windows.Forms.ColumnHeader thread;
        private System.Windows.Forms.ColumnHeader source;
        private System.Windows.Forms.ColumnHeader user;
        private System.Windows.Forms.ColumnHeader message;
    }
}