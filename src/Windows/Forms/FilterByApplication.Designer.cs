namespace Harvester.Forms
{
    partial class FilterByApplication
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterByApplication));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.mainPandel = new System.Windows.Forms.Panel();
            this.applicationsGroup = new System.Windows.Forms.GroupBox();
            this.applications = new System.Windows.Forms.CheckedListBox();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.resetButton = new System.Windows.Forms.Button();
            this.mainPandel.SuspendLayout();
            this.applicationsGroup.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(246, 7);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(327, 7);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // mainPandel
            // 
            this.mainPandel.Controls.Add(this.applicationsGroup);
            this.mainPandel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPandel.Location = new System.Drawing.Point(0, 0);
            this.mainPandel.Name = "mainPandel";
            this.mainPandel.Size = new System.Drawing.Size(414, 234);
            this.mainPandel.TabIndex = 0;
            // 
            // applicationsGroup
            // 
            this.applicationsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.applicationsGroup.Controls.Add(this.applications);
            this.applicationsGroup.Location = new System.Drawing.Point(12, 12);
            this.applicationsGroup.Name = "applicationsGroup";
            this.applicationsGroup.Padding = new System.Windows.Forms.Padding(6);
            this.applicationsGroup.Size = new System.Drawing.Size(390, 216);
            this.applicationsGroup.TabIndex = 0;
            this.applicationsGroup.TabStop = false;
            this.applicationsGroup.Text = "Applications";
            // 
            // applications
            // 
            this.applications.CheckOnClick = true;
            this.applications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.applications.FormattingEnabled = true;
            this.applications.IntegralHeight = false;
            this.applications.Location = new System.Drawing.Point(6, 19);
            this.applications.Name = "applications";
            this.applications.ScrollAlwaysVisible = true;
            this.applications.Size = new System.Drawing.Size(378, 191);
            this.applications.Sorted = true;
            this.applications.TabIndex = 0;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.resetButton);
            this.buttonPanel.Controls.Add(this.cancelButton);
            this.buttonPanel.Controls.Add(this.okButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(0, 234);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(414, 42);
            this.buttonPanel.TabIndex = 1;
            // 
            // resetButton
            // 
            this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resetButton.Location = new System.Drawing.Point(12, 7);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 0;
            this.resetButton.Text = "&Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            // 
            // FilterByApplication
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(414, 276);
            this.Controls.Add(this.mainPandel);
            this.Controls.Add(this.buttonPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 300);
            this.Name = "FilterByApplication";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filter By Application";
            this.mainPandel.ResumeLayout(false);
            this.applicationsGroup.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPandel;
        private System.Windows.Forms.GroupBox applicationsGroup;
        private System.Windows.Forms.CheckedListBox applications;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;

    }
}