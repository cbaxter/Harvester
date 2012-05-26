namespace Harvester.Forms
{
    partial class FilterByLevel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterByLevel));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.mainPandel = new System.Windows.Forms.Panel();
            this.levelsGroup = new System.Windows.Forms.GroupBox();
            this.traceLevel = new System.Windows.Forms.RadioButton();
            this.debugLevel = new System.Windows.Forms.RadioButton();
            this.informationLevel = new System.Windows.Forms.RadioButton();
            this.warningLevel = new System.Windows.Forms.RadioButton();
            this.errorLevel = new System.Windows.Forms.RadioButton();
            this.fatalLevel = new System.Windows.Forms.RadioButton();
            this.buttonPanel.SuspendLayout();
            this.mainPandel.SuspendLayout();
            this.levelsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(126, 7);
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
            this.cancelButton.Location = new System.Drawing.Point(207, 7);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
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
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.resetButton);
            this.buttonPanel.Controls.Add(this.cancelButton);
            this.buttonPanel.Controls.Add(this.okButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(0, 184);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(294, 42);
            this.buttonPanel.TabIndex = 1;
            // 
            // mainPandel
            // 
            this.mainPandel.Controls.Add(this.levelsGroup);
            this.mainPandel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPandel.Location = new System.Drawing.Point(0, 0);
            this.mainPandel.Name = "mainPandel";
            this.mainPandel.Size = new System.Drawing.Size(294, 184);
            this.mainPandel.TabIndex = 0;
            // 
            // levelsGroup
            // 
            this.levelsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.levelsGroup.Controls.Add(this.traceLevel);
            this.levelsGroup.Controls.Add(this.debugLevel);
            this.levelsGroup.Controls.Add(this.informationLevel);
            this.levelsGroup.Controls.Add(this.warningLevel);
            this.levelsGroup.Controls.Add(this.errorLevel);
            this.levelsGroup.Controls.Add(this.fatalLevel);
            this.levelsGroup.Location = new System.Drawing.Point(12, 12);
            this.levelsGroup.Name = "levelsGroup";
            this.levelsGroup.Size = new System.Drawing.Size(270, 166);
            this.levelsGroup.TabIndex = 0;
            this.levelsGroup.TabStop = false;
            this.levelsGroup.Text = "System Event Levels";
            // 
            // traceLevel
            // 
            this.traceLevel.AutoSize = true;
            this.traceLevel.Checked = true;
            this.traceLevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.traceLevel.Location = new System.Drawing.Point(3, 134);
            this.traceLevel.Name = "traceLevel";
            this.traceLevel.Padding = new System.Windows.Forms.Padding(3);
            this.traceLevel.Size = new System.Drawing.Size(264, 23);
            this.traceLevel.TabIndex = 5;
            this.traceLevel.TabStop = true;
            this.traceLevel.Text = "&Trace";
            this.traceLevel.UseVisualStyleBackColor = true;
            // 
            // debugLevel
            // 
            this.debugLevel.AutoSize = true;
            this.debugLevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.debugLevel.Location = new System.Drawing.Point(3, 111);
            this.debugLevel.Name = "debugLevel";
            this.debugLevel.Padding = new System.Windows.Forms.Padding(3);
            this.debugLevel.Size = new System.Drawing.Size(264, 23);
            this.debugLevel.TabIndex = 4;
            this.debugLevel.Text = "&Debug";
            this.debugLevel.UseVisualStyleBackColor = true;
            // 
            // informationLevel
            // 
            this.informationLevel.AutoSize = true;
            this.informationLevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.informationLevel.Location = new System.Drawing.Point(3, 88);
            this.informationLevel.Name = "informationLevel";
            this.informationLevel.Padding = new System.Windows.Forms.Padding(3);
            this.informationLevel.Size = new System.Drawing.Size(264, 23);
            this.informationLevel.TabIndex = 3;
            this.informationLevel.Text = "&Information";
            this.informationLevel.UseVisualStyleBackColor = true;
            // 
            // warningLevel
            // 
            this.warningLevel.AutoSize = true;
            this.warningLevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.warningLevel.Location = new System.Drawing.Point(3, 65);
            this.warningLevel.Name = "warningLevel";
            this.warningLevel.Padding = new System.Windows.Forms.Padding(3);
            this.warningLevel.Size = new System.Drawing.Size(264, 23);
            this.warningLevel.TabIndex = 2;
            this.warningLevel.Text = "&Warning";
            this.warningLevel.UseVisualStyleBackColor = true;
            // 
            // errorLevel
            // 
            this.errorLevel.AutoSize = true;
            this.errorLevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.errorLevel.Location = new System.Drawing.Point(3, 42);
            this.errorLevel.Name = "errorLevel";
            this.errorLevel.Padding = new System.Windows.Forms.Padding(3);
            this.errorLevel.Size = new System.Drawing.Size(264, 23);
            this.errorLevel.TabIndex = 1;
            this.errorLevel.Text = "&Error";
            this.errorLevel.UseVisualStyleBackColor = true;
            // 
            // fatalLevel
            // 
            this.fatalLevel.AutoSize = true;
            this.fatalLevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.fatalLevel.Location = new System.Drawing.Point(3, 16);
            this.fatalLevel.Name = "fatalLevel";
            this.fatalLevel.Padding = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.fatalLevel.Size = new System.Drawing.Size(264, 26);
            this.fatalLevel.TabIndex = 0;
            this.fatalLevel.Text = "&Fatal";
            this.fatalLevel.UseVisualStyleBackColor = true;
            // 
            // FilterByLevel
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(294, 226);
            this.Controls.Add(this.mainPandel);
            this.Controls.Add(this.buttonPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 250);
            this.Name = "FilterByLevel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filter By System Event Level";
            this.buttonPanel.ResumeLayout(false);
            this.mainPandel.ResumeLayout(false);
            this.levelsGroup.ResumeLayout(false);
            this.levelsGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Panel mainPandel;
        private System.Windows.Forms.GroupBox levelsGroup;
        private System.Windows.Forms.RadioButton traceLevel;
        private System.Windows.Forms.RadioButton debugLevel;
        private System.Windows.Forms.RadioButton informationLevel;
        private System.Windows.Forms.RadioButton warningLevel;
        private System.Windows.Forms.RadioButton errorLevel;
        private System.Windows.Forms.RadioButton fatalLevel;
    }
}