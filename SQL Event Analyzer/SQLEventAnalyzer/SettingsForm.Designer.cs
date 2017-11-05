/*
Copyright (C) 2017 Lars Hove Christiansen
http://virtcore.com

This file is a part of SQL Event Analyzer

	SQL Event Analyzer is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	SQL Event Analyzer is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with SQL Event Analyzer. If not, see <http://www.gnu.org/licenses/>.
*/

partial class SettingsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.layoutGroupBox = new System.Windows.Forms.GroupBox();
			this.itemsPerPageLabel = new System.Windows.Forms.Label();
			this.itemsPerPageTextBox = new System.Windows.Forms.TextBox();
			this.otherGroupBox = new System.Windows.Forms.GroupBox();
			this.autoPopulateFilter2Label = new System.Windows.Forms.Label();
			this.autoPopulateFilter2CheckBox = new System.Windows.Forms.CheckBox();
			this.enableFileNameAndTypeLabel = new System.Windows.Forms.Label();
			this.enableFileNameAndTypeCheckBox = new System.Windows.Forms.CheckBox();
			this.keepSessionLabel = new System.Windows.Forms.Label();
			this.keepSessionCheckBox = new System.Windows.Forms.CheckBox();
			this.languageComboBox = new System.Windows.Forms.ComboBox();
			this.languageLabel = new System.Windows.Forms.Label();
			this.resetLayoutButton = new System.Windows.Forms.Button();
			this.traceFileGroupBox = new System.Windows.Forms.GroupBox();
			this.traceFileDirComboBox = new ComboBoxCustom();
			this.tracingFunctionalityComboBox = new System.Windows.Forms.ComboBox();
			this.tracingFunctionalityLabel = new System.Windows.Forms.Label();
			this.chooseDirectoryButton = new System.Windows.Forms.Button();
			this.traceFileInfoLinkLabel = new System.Windows.Forms.LinkLabel();
			this.traceFileDirLabel = new System.Windows.Forms.Label();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.enableQuickSearchLabel = new System.Windows.Forms.Label();
			this.enableQuickSearchCheckBox = new System.Windows.Forms.CheckBox();
			this.layoutGroupBox.SuspendLayout();
			this.otherGroupBox.SuspendLayout();
			this.traceFileGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(432, 325);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 24);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(351, 325);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 24);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// layoutGroupBox
			// 
			this.layoutGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.layoutGroupBox.Controls.Add(this.itemsPerPageLabel);
			this.layoutGroupBox.Controls.Add(this.itemsPerPageTextBox);
			this.layoutGroupBox.Location = new System.Drawing.Point(12, 12);
			this.layoutGroupBox.Name = "layoutGroupBox";
			this.layoutGroupBox.Size = new System.Drawing.Size(495, 46);
			this.layoutGroupBox.TabIndex = 2;
			this.layoutGroupBox.TabStop = false;
			this.layoutGroupBox.Text = "Layout";
			// 
			// itemsPerPageLabel
			// 
			this.itemsPerPageLabel.AutoSize = true;
			this.itemsPerPageLabel.Location = new System.Drawing.Point(6, 22);
			this.itemsPerPageLabel.Name = "itemsPerPageLabel";
			this.itemsPerPageLabel.Size = new System.Drawing.Size(77, 13);
			this.itemsPerPageLabel.TabIndex = 1;
			this.itemsPerPageLabel.Text = "Items per page";
			// 
			// itemsPerPageTextBox
			// 
			this.itemsPerPageTextBox.Location = new System.Drawing.Point(180, 19);
			this.itemsPerPageTextBox.Name = "itemsPerPageTextBox";
			this.itemsPerPageTextBox.Size = new System.Drawing.Size(69, 20);
			this.itemsPerPageTextBox.TabIndex = 0;
			// 
			// otherGroupBox
			// 
			this.otherGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.otherGroupBox.Controls.Add(this.enableQuickSearchCheckBox);
			this.otherGroupBox.Controls.Add(this.enableQuickSearchLabel);
			this.otherGroupBox.Controls.Add(this.autoPopulateFilter2Label);
			this.otherGroupBox.Controls.Add(this.autoPopulateFilter2CheckBox);
			this.otherGroupBox.Controls.Add(this.enableFileNameAndTypeLabel);
			this.otherGroupBox.Controls.Add(this.enableFileNameAndTypeCheckBox);
			this.otherGroupBox.Controls.Add(this.keepSessionLabel);
			this.otherGroupBox.Controls.Add(this.keepSessionCheckBox);
			this.otherGroupBox.Controls.Add(this.languageComboBox);
			this.otherGroupBox.Controls.Add(this.languageLabel);
			this.otherGroupBox.Location = new System.Drawing.Point(12, 64);
			this.otherGroupBox.Name = "otherGroupBox";
			this.otherGroupBox.Size = new System.Drawing.Size(495, 127);
			this.otherGroupBox.TabIndex = 3;
			this.otherGroupBox.TabStop = false;
			this.otherGroupBox.Text = "Application";
			// 
			// autoPopulateFilter2Label
			// 
			this.autoPopulateFilter2Label.AutoSize = true;
			this.autoPopulateFilter2Label.Location = new System.Drawing.Point(30, 86);
			this.autoPopulateFilter2Label.Name = "autoPopulateFilter2Label";
			this.autoPopulateFilter2Label.Size = new System.Drawing.Size(234, 13);
			this.autoPopulateFilter2Label.TabIndex = 8;
			this.autoPopulateFilter2Label.Text = "Automatically populate Filter 2 drop down values";
			this.autoPopulateFilter2Label.Click += new System.EventHandler(this.AutoPopulateFilter2Label_Click);
			// 
			// autoPopulateFilter2CheckBox
			// 
			this.autoPopulateFilter2CheckBox.AutoSize = true;
			this.autoPopulateFilter2CheckBox.Location = new System.Drawing.Point(9, 86);
			this.autoPopulateFilter2CheckBox.Name = "autoPopulateFilter2CheckBox";
			this.autoPopulateFilter2CheckBox.Size = new System.Drawing.Size(15, 14);
			this.autoPopulateFilter2CheckBox.TabIndex = 3;
			this.autoPopulateFilter2CheckBox.UseVisualStyleBackColor = true;
			// 
			// enableFileNameAndTypeLabel
			// 
			this.enableFileNameAndTypeLabel.AutoSize = true;
			this.enableFileNameAndTypeLabel.Location = new System.Drawing.Point(30, 66);
			this.enableFileNameAndTypeLabel.Name = "enableFileNameAndTypeLabel";
			this.enableFileNameAndTypeLabel.Size = new System.Drawing.Size(155, 13);
			this.enableFileNameAndTypeLabel.TabIndex = 6;
			this.enableFileNameAndTypeLabel.Text = "Enable \"FileName\" and \"Type\"";
			this.enableFileNameAndTypeLabel.Click += new System.EventHandler(this.EnableFileNameAndTypeLabel_Click);
			// 
			// enableFileNameAndTypeCheckBox
			// 
			this.enableFileNameAndTypeCheckBox.AutoSize = true;
			this.enableFileNameAndTypeCheckBox.Location = new System.Drawing.Point(9, 66);
			this.enableFileNameAndTypeCheckBox.Name = "enableFileNameAndTypeCheckBox";
			this.enableFileNameAndTypeCheckBox.Size = new System.Drawing.Size(15, 14);
			this.enableFileNameAndTypeCheckBox.TabIndex = 2;
			this.enableFileNameAndTypeCheckBox.UseVisualStyleBackColor = true;
			// 
			// keepSessionLabel
			// 
			this.keepSessionLabel.AutoSize = true;
			this.keepSessionLabel.Location = new System.Drawing.Point(30, 46);
			this.keepSessionLabel.Name = "keepSessionLabel";
			this.keepSessionLabel.Size = new System.Drawing.Size(106, 13);
			this.keepSessionLabel.TabIndex = 4;
			this.keepSessionLabel.Text = "Keep Session on exit";
			this.keepSessionLabel.Click += new System.EventHandler(this.KeepSessionLabel_Click);
			// 
			// keepSessionCheckBox
			// 
			this.keepSessionCheckBox.AutoSize = true;
			this.keepSessionCheckBox.Location = new System.Drawing.Point(9, 46);
			this.keepSessionCheckBox.Name = "keepSessionCheckBox";
			this.keepSessionCheckBox.Size = new System.Drawing.Size(15, 14);
			this.keepSessionCheckBox.TabIndex = 1;
			this.keepSessionCheckBox.UseVisualStyleBackColor = true;
			// 
			// languageComboBox
			// 
			this.languageComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.languageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.languageComboBox.FormattingEnabled = true;
			this.languageComboBox.Items.AddRange(new object[] {
            "Dansk",
            "English"});
			this.languageComboBox.Location = new System.Drawing.Point(180, 19);
			this.languageComboBox.MaxDropDownItems = 2;
			this.languageComboBox.Name = "languageComboBox";
			this.languageComboBox.Size = new System.Drawing.Size(309, 21);
			this.languageComboBox.TabIndex = 0;
			// 
			// languageLabel
			// 
			this.languageLabel.AutoSize = true;
			this.languageLabel.Location = new System.Drawing.Point(6, 22);
			this.languageLabel.Name = "languageLabel";
			this.languageLabel.Size = new System.Drawing.Size(55, 13);
			this.languageLabel.TabIndex = 3;
			this.languageLabel.Text = "Language";
			// 
			// resetLayoutButton
			// 
			this.resetLayoutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.resetLayoutButton.Location = new System.Drawing.Point(12, 325);
			this.resetLayoutButton.Name = "resetLayoutButton";
			this.resetLayoutButton.Size = new System.Drawing.Size(83, 24);
			this.resetLayoutButton.TabIndex = 5;
			this.resetLayoutButton.Text = "Reset layout";
			this.resetLayoutButton.UseVisualStyleBackColor = true;
			this.resetLayoutButton.Click += new System.EventHandler(this.ResetLayoutButton_Click);
			// 
			// traceFileGroupBox
			// 
			this.traceFileGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.traceFileGroupBox.Controls.Add(this.traceFileDirComboBox);
			this.traceFileGroupBox.Controls.Add(this.tracingFunctionalityComboBox);
			this.traceFileGroupBox.Controls.Add(this.tracingFunctionalityLabel);
			this.traceFileGroupBox.Controls.Add(this.chooseDirectoryButton);
			this.traceFileGroupBox.Controls.Add(this.traceFileInfoLinkLabel);
			this.traceFileGroupBox.Controls.Add(this.traceFileDirLabel);
			this.traceFileGroupBox.Location = new System.Drawing.Point(12, 197);
			this.traceFileGroupBox.Name = "traceFileGroupBox";
			this.traceFileGroupBox.Size = new System.Drawing.Size(495, 120);
			this.traceFileGroupBox.TabIndex = 4;
			this.traceFileGroupBox.TabStop = false;
			this.traceFileGroupBox.Text = "Tracing (for recording)";
			// 
			// traceFileDirComboBox
			// 
			this.traceFileDirComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.traceFileDirComboBox.FormattingEnabled = true;
			this.traceFileDirComboBox.Location = new System.Drawing.Point(180, 18);
			this.traceFileDirComboBox.Name = "traceFileDirComboBox";
			this.traceFileDirComboBox.Size = new System.Drawing.Size(268, 21);
			this.traceFileDirComboBox.TabIndex = 0;
			// 
			// tracingFunctionalityComboBox
			// 
			this.tracingFunctionalityComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tracingFunctionalityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tracingFunctionalityComboBox.FormattingEnabled = true;
			this.tracingFunctionalityComboBox.Location = new System.Drawing.Point(180, 92);
			this.tracingFunctionalityComboBox.MaxDropDownItems = 2;
			this.tracingFunctionalityComboBox.Name = "tracingFunctionalityComboBox";
			this.tracingFunctionalityComboBox.Size = new System.Drawing.Size(309, 21);
			this.tracingFunctionalityComboBox.TabIndex = 2;
			// 
			// tracingFunctionalityLabel
			// 
			this.tracingFunctionalityLabel.AutoSize = true;
			this.tracingFunctionalityLabel.Location = new System.Drawing.Point(6, 95);
			this.tracingFunctionalityLabel.Name = "tracingFunctionalityLabel";
			this.tracingFunctionalityLabel.Size = new System.Drawing.Size(105, 13);
			this.tracingFunctionalityLabel.TabIndex = 5;
			this.tracingFunctionalityLabel.Text = "Tracing Functionality";
			// 
			// chooseDirectoryButton
			// 
			this.chooseDirectoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chooseDirectoryButton.Location = new System.Drawing.Point(454, 17);
			this.chooseDirectoryButton.Name = "chooseDirectoryButton";
			this.chooseDirectoryButton.Size = new System.Drawing.Size(35, 23);
			this.chooseDirectoryButton.TabIndex = 1;
			this.chooseDirectoryButton.Text = "...";
			this.chooseDirectoryButton.UseVisualStyleBackColor = true;
			this.chooseDirectoryButton.Click += new System.EventHandler(this.ChooseDirectoryButton_Click);
			// 
			// traceFileInfoLinkLabel
			// 
			this.traceFileInfoLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.traceFileInfoLinkLabel.BackColor = System.Drawing.SystemColors.Control;
			this.traceFileInfoLinkLabel.Enabled = false;
			this.traceFileInfoLinkLabel.Location = new System.Drawing.Point(180, 42);
			this.traceFileInfoLinkLabel.Name = "traceFileInfoLinkLabel";
			this.traceFileInfoLinkLabel.Size = new System.Drawing.Size(309, 47);
			this.traceFileInfoLinkLabel.TabIndex = 27;
			this.traceFileInfoLinkLabel.TabStop = true;
			this.traceFileInfoLinkLabel.Text = "If you are writing from a remote server to a local drive, please use a UNC path a" +
    "nd make sure the server has write access to your network share.";
			// 
			// traceFileDirLabel
			// 
			this.traceFileDirLabel.AutoSize = true;
			this.traceFileDirLabel.Location = new System.Drawing.Point(6, 22);
			this.traceFileDirLabel.Name = "traceFileDirLabel";
			this.traceFileDirLabel.Size = new System.Drawing.Size(99, 13);
			this.traceFileDirLabel.TabIndex = 4;
			this.traceFileDirLabel.Text = "Trace File Directory";
			// 
			// enableQuickSearchLabel
			// 
			this.enableQuickSearchLabel.AutoSize = true;
			this.enableQuickSearchLabel.Location = new System.Drawing.Point(30, 106);
			this.enableQuickSearchLabel.Name = "enableQuickSearchLabel";
			this.enableQuickSearchLabel.Size = new System.Drawing.Size(104, 13);
			this.enableQuickSearchLabel.TabIndex = 9;
			this.enableQuickSearchLabel.Text = "Enable quick search";
			this.enableQuickSearchLabel.Click += new System.EventHandler(this.EnableQuickSearchLabel_Click);
			// 
			// enableQuickSearchCheckBox
			// 
			this.enableQuickSearchCheckBox.AutoSize = true;
			this.enableQuickSearchCheckBox.Location = new System.Drawing.Point(9, 106);
			this.enableQuickSearchCheckBox.Name = "enableQuickSearchCheckBox";
			this.enableQuickSearchCheckBox.Size = new System.Drawing.Size(15, 14);
			this.enableQuickSearchCheckBox.TabIndex = 4;
			this.enableQuickSearchCheckBox.UseVisualStyleBackColor = true;
			// 
			// SettingsForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(519, 356);
			this.Controls.Add(this.traceFileGroupBox);
			this.Controls.Add(this.resetLayoutButton);
			this.Controls.Add(this.otherGroupBox);
			this.Controls.Add(this.layoutGroupBox);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			this.layoutGroupBox.ResumeLayout(false);
			this.layoutGroupBox.PerformLayout();
			this.otherGroupBox.ResumeLayout(false);
			this.otherGroupBox.PerformLayout();
			this.traceFileGroupBox.ResumeLayout(false);
			this.traceFileGroupBox.PerformLayout();
			this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.Button cancelButton;
	private System.Windows.Forms.Button okButton;
	private System.Windows.Forms.GroupBox layoutGroupBox;
	private System.Windows.Forms.Label itemsPerPageLabel;
    private System.Windows.Forms.TextBox itemsPerPageTextBox;
	private System.Windows.Forms.GroupBox otherGroupBox;
	private System.Windows.Forms.ComboBox languageComboBox;
	private System.Windows.Forms.Label languageLabel;
	private System.Windows.Forms.Button resetLayoutButton;
	private System.Windows.Forms.GroupBox traceFileGroupBox;
	private System.Windows.Forms.Label traceFileDirLabel;
	private System.Windows.Forms.LinkLabel traceFileInfoLinkLabel;
	private System.Windows.Forms.Button chooseDirectoryButton;
	private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
	private System.Windows.Forms.ComboBox tracingFunctionalityComboBox;
	private System.Windows.Forms.Label tracingFunctionalityLabel;
	private ComboBoxCustom traceFileDirComboBox;
	private System.Windows.Forms.Label keepSessionLabel;
	private System.Windows.Forms.CheckBox keepSessionCheckBox;
	private System.Windows.Forms.Label enableFileNameAndTypeLabel;
	private System.Windows.Forms.CheckBox enableFileNameAndTypeCheckBox;
	private System.Windows.Forms.Label autoPopulateFilter2Label;
	private System.Windows.Forms.CheckBox autoPopulateFilter2CheckBox;
	private System.Windows.Forms.CheckBox enableQuickSearchCheckBox;
	private System.Windows.Forms.Label enableQuickSearchLabel;
}
