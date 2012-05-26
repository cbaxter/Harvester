namespace Harvester.Forms
{
    partial class FilterByText
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterByText));
            this.filterText = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.addFilter = new System.Windows.Forms.Button();
            this.filters = new System.Windows.Forms.ListBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.newFilterGroup = new System.Windows.Forms.GroupBox();
            this.clearFilter = new System.Windows.Forms.Button();
            this.negateFilter = new System.Windows.Forms.CheckBox();
            this.filterType = new System.Windows.Forms.ComboBox();
            this.filtersGroup = new System.Windows.Forms.GroupBox();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.newFilterPanel = new System.Windows.Forms.Panel();
            this.filtersPanel = new System.Windows.Forms.Panel();
            this.newFilterGroup.SuspendLayout();
            this.filtersGroup.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.newFilterPanel.SuspendLayout();
            this.filtersPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // filterText
            // 
            this.filterText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterText.Location = new System.Drawing.Point(192, 19);
            this.filterText.Name = "filterText";
            this.filterText.Size = new System.Drawing.Size(252, 20);
            this.filterText.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(387, 7);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(306, 7);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // addFilter
            // 
            this.addFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addFilter.Location = new System.Drawing.Point(288, 45);
            this.addFilter.Name = "addFilter";
            this.addFilter.Size = new System.Drawing.Size(75, 23);
            this.addFilter.TabIndex = 3;
            this.addFilter.Text = "Add";
            this.addFilter.UseVisualStyleBackColor = true;
            // 
            // filters
            // 
            this.filters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filters.FormattingEnabled = true;
            this.filters.IntegralHeight = false;
            this.filters.Location = new System.Drawing.Point(6, 19);
            this.filters.Name = "filters";
            this.filters.Size = new System.Drawing.Size(438, 163);
            this.filters.TabIndex = 0;
            // 
            // resetButton
            // 
            this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resetButton.Location = new System.Drawing.Point(18, 6);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 0;
            this.resetButton.Text = "&Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            // 
            // newFilterGroup
            // 
            this.newFilterGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newFilterGroup.Controls.Add(this.clearFilter);
            this.newFilterGroup.Controls.Add(this.negateFilter);
            this.newFilterGroup.Controls.Add(this.filterType);
            this.newFilterGroup.Controls.Add(this.filterText);
            this.newFilterGroup.Controls.Add(this.addFilter);
            this.newFilterGroup.Location = new System.Drawing.Point(12, 6);
            this.newFilterGroup.Name = "newFilterGroup";
            this.newFilterGroup.Size = new System.Drawing.Size(450, 76);
            this.newFilterGroup.TabIndex = 0;
            this.newFilterGroup.TabStop = false;
            this.newFilterGroup.Text = "Create New Filter";
            // 
            // clearFilter
            // 
            this.clearFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearFilter.Location = new System.Drawing.Point(369, 45);
            this.clearFilter.Name = "clearFilter";
            this.clearFilter.Size = new System.Drawing.Size(75, 23);
            this.clearFilter.TabIndex = 4;
            this.clearFilter.Text = "Clear";
            this.clearFilter.UseVisualStyleBackColor = true;
            // 
            // negateFilter
            // 
            this.negateFilter.AutoSize = true;
            this.negateFilter.Location = new System.Drawing.Point(6, 51);
            this.negateFilter.Name = "negateFilter";
            this.negateFilter.Size = new System.Drawing.Size(86, 17);
            this.negateFilter.TabIndex = 2;
            this.negateFilter.Text = "Negate Filter";
            this.negateFilter.UseVisualStyleBackColor = true;
            // 
            // filterType
            // 
            this.filterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterType.FormattingEnabled = true;
            this.filterType.Location = new System.Drawing.Point(6, 19);
            this.filterType.Name = "filterType";
            this.filterType.Size = new System.Drawing.Size(180, 21);
            this.filterType.Sorted = true;
            this.filterType.TabIndex = 0;
            // 
            // filtersGroup
            // 
            this.filtersGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filtersGroup.Controls.Add(this.filters);
            this.filtersGroup.Location = new System.Drawing.Point(12, 12);
            this.filtersGroup.Name = "filtersGroup";
            this.filtersGroup.Size = new System.Drawing.Size(450, 188);
            this.filtersGroup.TabIndex = 0;
            this.filtersGroup.TabStop = false;
            this.filtersGroup.Text = "Filters";
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.okButton);
            this.buttonPanel.Controls.Add(this.cancelButton);
            this.buttonPanel.Controls.Add(this.resetButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(0, 294);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(474, 42);
            this.buttonPanel.TabIndex = 2;
            // 
            // newFilterPanel
            // 
            this.newFilterPanel.Controls.Add(this.newFilterGroup);
            this.newFilterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.newFilterPanel.Location = new System.Drawing.Point(0, 0);
            this.newFilterPanel.Name = "newFilterPanel";
            this.newFilterPanel.Size = new System.Drawing.Size(474, 88);
            this.newFilterPanel.TabIndex = 0;
            // 
            // filtersPanel
            // 
            this.filtersPanel.Controls.Add(this.filtersGroup);
            this.filtersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filtersPanel.Location = new System.Drawing.Point(0, 88);
            this.filtersPanel.Name = "filtersPanel";
            this.filtersPanel.Size = new System.Drawing.Size(474, 206);
            this.filtersPanel.TabIndex = 1;
            // 
            // FilterByText
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(474, 336);
            this.Controls.Add(this.filtersPanel);
            this.Controls.Add(this.newFilterPanel);
            this.Controls.Add(this.buttonPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 360);
            this.Name = "FilterByText";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FilterBySource";
            this.newFilterGroup.ResumeLayout(false);
            this.newFilterGroup.PerformLayout();
            this.filtersGroup.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.newFilterPanel.ResumeLayout(false);
            this.filtersPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox filterText;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button addFilter;
        private System.Windows.Forms.ListBox filters;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.GroupBox newFilterGroup;
        private System.Windows.Forms.GroupBox filtersGroup;
        private System.Windows.Forms.ComboBox filterType;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Panel newFilterPanel;
        private System.Windows.Forms.Panel filtersPanel;
        private System.Windows.Forms.CheckBox negateFilter;
        private System.Windows.Forms.Button clearFilter;
    }
}