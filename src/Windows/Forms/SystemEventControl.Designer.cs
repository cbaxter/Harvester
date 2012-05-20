namespace Harvester.Forms
{
    partial class SystemEventControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.messageTabPage = new System.Windows.Forms.TabPage();
            this.detailsTabPage = new System.Windows.Forms.TabPage();
            this.rawTabPage = new System.Windows.Forms.TabPage();
            this.rawText = new System.Windows.Forms.RichTextBox();
            this.messageText = new System.Windows.Forms.RichTextBox();
            this.detailsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.messageIdHeader = new System.Windows.Forms.Label();
            this.levelHeader = new System.Windows.Forms.Label();
            this.sourceHeader = new System.Windows.Forms.Label();
            this.messageId = new System.Windows.Forms.TextBox();
            this.level = new System.Windows.Forms.TextBox();
            this.source = new System.Windows.Forms.TextBox();
            this.timestampHeader = new System.Windows.Forms.Label();
            this.processHeader = new System.Windows.Forms.Label();
            this.threadHeader = new System.Windows.Forms.Label();
            this.usernameHeader = new System.Windows.Forms.Label();
            this.timestamp = new System.Windows.Forms.TextBox();
            this.process = new System.Windows.Forms.TextBox();
            this.thread = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.messageHeader = new System.Windows.Forms.Label();
            this.message = new System.Windows.Forms.RichTextBox();
            this.tabControl.SuspendLayout();
            this.messageTabPage.SuspendLayout();
            this.detailsTabPage.SuspendLayout();
            this.rawTabPage.SuspendLayout();
            this.detailsLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.messageTabPage);
            this.tabControl.Controls.Add(this.detailsTabPage);
            this.tabControl.Controls.Add(this.rawTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.HotTrack = true;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(922, 450);
            this.tabControl.TabIndex = 0;
            // 
            // messageTabPage
            // 
            this.messageTabPage.Controls.Add(this.messageText);
            this.messageTabPage.Location = new System.Drawing.Point(4, 22);
            this.messageTabPage.Name = "messageTabPage";
            this.messageTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.messageTabPage.Size = new System.Drawing.Size(914, 424);
            this.messageTabPage.TabIndex = 0;
            this.messageTabPage.Text = "Message";
            this.messageTabPage.UseVisualStyleBackColor = true;
            // 
            // detailsTabPage
            // 
            this.detailsTabPage.Controls.Add(this.detailsLayout);
            this.detailsTabPage.Location = new System.Drawing.Point(4, 22);
            this.detailsTabPage.Name = "detailsTabPage";
            this.detailsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.detailsTabPage.Size = new System.Drawing.Size(914, 424);
            this.detailsTabPage.TabIndex = 1;
            this.detailsTabPage.Text = "Details";
            this.detailsTabPage.UseVisualStyleBackColor = true;
            // 
            // rawTabPage
            // 
            this.rawTabPage.Controls.Add(this.rawText);
            this.rawTabPage.Location = new System.Drawing.Point(4, 22);
            this.rawTabPage.Name = "rawTabPage";
            this.rawTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.rawTabPage.Size = new System.Drawing.Size(914, 424);
            this.rawTabPage.TabIndex = 2;
            this.rawTabPage.Text = "Raw";
            this.rawTabPage.UseVisualStyleBackColor = true;
            // 
            // rawText
            // 
            this.rawText.CausesValidation = false;
            this.rawText.DetectUrls = false;
            this.rawText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rawText.Location = new System.Drawing.Point(3, 3);
            this.rawText.Name = "rawText";
            this.rawText.ReadOnly = true;
            this.rawText.Size = new System.Drawing.Size(908, 418);
            this.rawText.TabIndex = 1;
            this.rawText.Text = "";
            // 
            // messageText
            // 
            this.messageText.CausesValidation = false;
            this.messageText.DetectUrls = false;
            this.messageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageText.Location = new System.Drawing.Point(3, 3);
            this.messageText.Name = "messageText";
            this.messageText.ReadOnly = true;
            this.messageText.Size = new System.Drawing.Size(908, 418);
            this.messageText.TabIndex = 2;
            this.messageText.Text = "";
            // 
            // detailsLayout
            // 
            this.detailsLayout.ColumnCount = 4;
            this.detailsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.detailsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.detailsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.detailsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.detailsLayout.Controls.Add(this.message, 0, 6);
            this.detailsLayout.Controls.Add(this.messageIdHeader, 0, 0);
            this.detailsLayout.Controls.Add(this.levelHeader, 1, 0);
            this.detailsLayout.Controls.Add(this.sourceHeader, 2, 0);
            this.detailsLayout.Controls.Add(this.messageId, 0, 1);
            this.detailsLayout.Controls.Add(this.level, 1, 1);
            this.detailsLayout.Controls.Add(this.source, 2, 1);
            this.detailsLayout.Controls.Add(this.timestampHeader, 0, 2);
            this.detailsLayout.Controls.Add(this.processHeader, 1, 2);
            this.detailsLayout.Controls.Add(this.threadHeader, 2, 2);
            this.detailsLayout.Controls.Add(this.usernameHeader, 3, 2);
            this.detailsLayout.Controls.Add(this.timestamp, 0, 3);
            this.detailsLayout.Controls.Add(this.process, 1, 3);
            this.detailsLayout.Controls.Add(this.thread, 2, 3);
            this.detailsLayout.Controls.Add(this.username, 3, 3);
            this.detailsLayout.Controls.Add(this.messageHeader, 0, 5);
            this.detailsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailsLayout.Location = new System.Drawing.Point(3, 3);
            this.detailsLayout.Name = "detailsLayout";
            this.detailsLayout.RowCount = 7;
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.detailsLayout.Size = new System.Drawing.Size(908, 418);
            this.detailsLayout.TabIndex = 3;
            // 
            // messageIdHeader
            // 
            this.messageIdHeader.AutoSize = true;
            this.messageIdHeader.Location = new System.Drawing.Point(3, 0);
            this.messageIdHeader.Name = "messageIdHeader";
            this.messageIdHeader.Size = new System.Drawing.Size(21, 13);
            this.messageIdHeader.TabIndex = 1;
            this.messageIdHeader.Text = "ID:";
            // 
            // levelHeader
            // 
            this.levelHeader.AutoSize = true;
            this.levelHeader.Location = new System.Drawing.Point(230, 0);
            this.levelHeader.Name = "levelHeader";
            this.levelHeader.Size = new System.Drawing.Size(36, 13);
            this.levelHeader.TabIndex = 3;
            this.levelHeader.Text = "Level:";
            // 
            // sourceHeader
            // 
            this.sourceHeader.AutoSize = true;
            this.detailsLayout.SetColumnSpan(this.sourceHeader, 2);
            this.sourceHeader.Location = new System.Drawing.Point(457, 0);
            this.sourceHeader.Name = "sourceHeader";
            this.sourceHeader.Size = new System.Drawing.Size(44, 13);
            this.sourceHeader.TabIndex = 5;
            this.sourceHeader.Text = "Source:";
            // 
            // messageId
            // 
            this.messageId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageId.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageId.Location = new System.Drawing.Point(3, 16);
            this.messageId.Name = "messageId";
            this.messageId.ReadOnly = true;
            this.messageId.Size = new System.Drawing.Size(221, 20);
            this.messageId.TabIndex = 6;
            // 
            // level
            // 
            this.level.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.level.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.level.Location = new System.Drawing.Point(230, 16);
            this.level.Name = "level";
            this.level.ReadOnly = true;
            this.level.Size = new System.Drawing.Size(221, 20);
            this.level.TabIndex = 7;
            // 
            // source
            // 
            this.source.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailsLayout.SetColumnSpan(this.source, 2);
            this.source.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.source.Location = new System.Drawing.Point(457, 16);
            this.source.Name = "source";
            this.source.ReadOnly = true;
            this.source.Size = new System.Drawing.Size(448, 20);
            this.source.TabIndex = 8;
            // 
            // timestampHeader
            // 
            this.timestampHeader.AutoSize = true;
            this.timestampHeader.Location = new System.Drawing.Point(3, 39);
            this.timestampHeader.Name = "timestampHeader";
            this.timestampHeader.Size = new System.Drawing.Size(58, 13);
            this.timestampHeader.TabIndex = 9;
            this.timestampHeader.Text = "Timestamp";
            // 
            // processHeader
            // 
            this.processHeader.AutoSize = true;
            this.processHeader.Location = new System.Drawing.Point(230, 39);
            this.processHeader.Name = "processHeader";
            this.processHeader.Size = new System.Drawing.Size(48, 13);
            this.processHeader.TabIndex = 10;
            this.processHeader.Text = "Process:";
            // 
            // threadHeader
            // 
            this.threadHeader.AutoSize = true;
            this.threadHeader.Location = new System.Drawing.Point(457, 39);
            this.threadHeader.Name = "threadHeader";
            this.threadHeader.Size = new System.Drawing.Size(44, 13);
            this.threadHeader.TabIndex = 11;
            this.threadHeader.Text = "Thread:";
            // 
            // usernameHeader
            // 
            this.usernameHeader.AutoSize = true;
            this.usernameHeader.Location = new System.Drawing.Point(684, 39);
            this.usernameHeader.Name = "usernameHeader";
            this.usernameHeader.Size = new System.Drawing.Size(58, 13);
            this.usernameHeader.TabIndex = 13;
            this.usernameHeader.Text = "Username:";
            // 
            // timestamp
            // 
            this.timestamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timestamp.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timestamp.Location = new System.Drawing.Point(3, 55);
            this.timestamp.Name = "timestamp";
            this.timestamp.ReadOnly = true;
            this.timestamp.Size = new System.Drawing.Size(221, 20);
            this.timestamp.TabIndex = 14;
            // 
            // process
            // 
            this.process.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.process.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.process.Location = new System.Drawing.Point(230, 55);
            this.process.Name = "process";
            this.process.ReadOnly = true;
            this.process.Size = new System.Drawing.Size(221, 20);
            this.process.TabIndex = 15;
            // 
            // thread
            // 
            this.thread.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.thread.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thread.Location = new System.Drawing.Point(457, 55);
            this.thread.Name = "thread";
            this.thread.ReadOnly = true;
            this.thread.Size = new System.Drawing.Size(221, 20);
            this.thread.TabIndex = 16;
            // 
            // username
            // 
            this.username.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.username.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username.Location = new System.Drawing.Point(684, 55);
            this.username.Name = "username";
            this.username.ReadOnly = true;
            this.username.Size = new System.Drawing.Size(221, 20);
            this.username.TabIndex = 17;
            // 
            // messageHeader
            // 
            this.messageHeader.AutoSize = true;
            this.detailsLayout.SetColumnSpan(this.messageHeader, 4);
            this.messageHeader.Location = new System.Drawing.Point(3, 78);
            this.messageHeader.Name = "messageHeader";
            this.messageHeader.Size = new System.Drawing.Size(53, 13);
            this.messageHeader.TabIndex = 18;
            this.messageHeader.Text = "Message:";
            // 
            // message
            // 
            this.message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailsLayout.SetColumnSpan(this.message, 4);
            this.message.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.message.Location = new System.Drawing.Point(3, 94);
            this.message.Name = "message";
            this.message.ReadOnly = true;
            this.message.Size = new System.Drawing.Size(902, 321);
            this.message.TabIndex = 19;
            this.message.Text = "";
            this.message.WordWrap = false;
            // 
            // SystemEventControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "SystemEventControl";
            this.Size = new System.Drawing.Size(922, 450);
            this.tabControl.ResumeLayout(false);
            this.messageTabPage.ResumeLayout(false);
            this.detailsTabPage.ResumeLayout(false);
            this.rawTabPage.ResumeLayout(false);
            this.detailsLayout.ResumeLayout(false);
            this.detailsLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage messageTabPage;
        private System.Windows.Forms.TabPage detailsTabPage;
        private System.Windows.Forms.TabPage rawTabPage;
        private System.Windows.Forms.RichTextBox rawText;
        private System.Windows.Forms.RichTextBox messageText;
        private System.Windows.Forms.TableLayoutPanel detailsLayout;
        private System.Windows.Forms.RichTextBox message;
        private System.Windows.Forms.Label messageIdHeader;
        private System.Windows.Forms.Label levelHeader;
        private System.Windows.Forms.Label sourceHeader;
        private System.Windows.Forms.TextBox messageId;
        private System.Windows.Forms.TextBox level;
        private System.Windows.Forms.TextBox source;
        private System.Windows.Forms.Label timestampHeader;
        private System.Windows.Forms.Label processHeader;
        private System.Windows.Forms.Label threadHeader;
        private System.Windows.Forms.Label usernameHeader;
        private System.Windows.Forms.TextBox timestamp;
        private System.Windows.Forms.TextBox process;
        private System.Windows.Forms.TextBox thread;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Label messageHeader;
    }
}
