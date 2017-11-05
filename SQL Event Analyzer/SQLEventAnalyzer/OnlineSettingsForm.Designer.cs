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

partial class OnlineSettingsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OnlineSettingsForm));
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.automaticUpdateGroupBox = new System.Windows.Forms.GroupBox();
			this.fileVersionLabel = new System.Windows.Forms.Label();
			this.fileVersionTextBox = new System.Windows.Forms.TextBox();
			this.fileNameLabel = new System.Windows.Forms.Label();
			this.fileNameTextBox = new System.Windows.Forms.TextBox();
			this.infoLinkLabel = new System.Windows.Forms.LinkLabel();
			this.updateServerLabel = new System.Windows.Forms.Label();
			this.enableCheckBox = new System.Windows.Forms.CheckBox();
			this.updateServerTextBox = new System.Windows.Forms.TextBox();
			this.automaticUpdateGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(321, 190);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 24);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(402, 190);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 24);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// automaticUpdateGroupBox
			// 
			this.automaticUpdateGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.automaticUpdateGroupBox.Controls.Add(this.fileVersionLabel);
			this.automaticUpdateGroupBox.Controls.Add(this.fileVersionTextBox);
			this.automaticUpdateGroupBox.Controls.Add(this.fileNameLabel);
			this.automaticUpdateGroupBox.Controls.Add(this.fileNameTextBox);
			this.automaticUpdateGroupBox.Controls.Add(this.infoLinkLabel);
			this.automaticUpdateGroupBox.Controls.Add(this.updateServerLabel);
			this.automaticUpdateGroupBox.Controls.Add(this.enableCheckBox);
			this.automaticUpdateGroupBox.Controls.Add(this.updateServerTextBox);
			this.automaticUpdateGroupBox.Location = new System.Drawing.Point(12, 12);
			this.automaticUpdateGroupBox.Name = "automaticUpdateGroupBox";
			this.automaticUpdateGroupBox.Size = new System.Drawing.Size(465, 170);
			this.automaticUpdateGroupBox.TabIndex = 0;
			this.automaticUpdateGroupBox.TabStop = false;
			this.automaticUpdateGroupBox.Text = "Subscription";
			// 
			// fileVersionLabel
			// 
			this.fileVersionLabel.AutoSize = true;
			this.fileVersionLabel.Location = new System.Drawing.Point(6, 144);
			this.fileVersionLabel.Name = "fileVersionLabel";
			this.fileVersionLabel.Size = new System.Drawing.Size(63, 13);
			this.fileVersionLabel.TabIndex = 32;
			this.fileVersionLabel.Text = "File version:";
			// 
			// fileVersionTextBox
			// 
			this.fileVersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fileVersionTextBox.Enabled = false;
			this.fileVersionTextBox.Location = new System.Drawing.Point(108, 141);
			this.fileVersionTextBox.Name = "fileVersionTextBox";
			this.fileVersionTextBox.Size = new System.Drawing.Size(351, 20);
			this.fileVersionTextBox.TabIndex = 31;
			this.fileVersionTextBox.TabStop = false;
			// 
			// fileNameLabel
			// 
			this.fileNameLabel.AutoSize = true;
			this.fileNameLabel.Location = new System.Drawing.Point(6, 118);
			this.fileNameLabel.Name = "fileNameLabel";
			this.fileNameLabel.Size = new System.Drawing.Size(55, 13);
			this.fileNameLabel.TabIndex = 30;
			this.fileNameLabel.Text = "File name:";
			// 
			// fileNameTextBox
			// 
			this.fileNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fileNameTextBox.Enabled = false;
			this.fileNameTextBox.Location = new System.Drawing.Point(108, 115);
			this.fileNameTextBox.Name = "fileNameTextBox";
			this.fileNameTextBox.Size = new System.Drawing.Size(351, 20);
			this.fileNameTextBox.TabIndex = 29;
			this.fileNameTextBox.TabStop = false;
			// 
			// infoLinkLabel
			// 
			this.infoLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.infoLinkLabel.BackColor = System.Drawing.SystemColors.Control;
			this.infoLinkLabel.Enabled = false;
			this.infoLinkLabel.Location = new System.Drawing.Point(6, 16);
			this.infoLinkLabel.Name = "infoLinkLabel";
			this.infoLinkLabel.Size = new System.Drawing.Size(453, 47);
			this.infoLinkLabel.TabIndex = 28;
			this.infoLinkLabel.TabStop = true;
			this.infoLinkLabel.Text = "If enabled, a check for updated Custom Columns will be made at program start. If " +
    "new Custom Columns are found, they will automatically be updated, and existing C" +
    "ustom Columns will be overwritten.";
			// 
			// updateServerLabel
			// 
			this.updateServerLabel.AutoSize = true;
			this.updateServerLabel.Location = new System.Drawing.Point(6, 92);
			this.updateServerLabel.Name = "updateServerLabel";
			this.updateServerLabel.Size = new System.Drawing.Size(77, 13);
			this.updateServerLabel.TabIndex = 2;
			this.updateServerLabel.Text = "Update server:";
			// 
			// enableCheckBox
			// 
			this.enableCheckBox.AutoSize = true;
			this.enableCheckBox.Location = new System.Drawing.Point(9, 66);
			this.enableCheckBox.Name = "enableCheckBox";
			this.enableCheckBox.Size = new System.Drawing.Size(144, 17);
			this.enableCheckBox.TabIndex = 1;
			this.enableCheckBox.Text = "Enable automatic update";
			this.enableCheckBox.UseVisualStyleBackColor = true;
			this.enableCheckBox.CheckedChanged += new System.EventHandler(this.EnableCheckBox_CheckedChanged);
			// 
			// updateServerTextBox
			// 
			this.updateServerTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.updateServerTextBox.Enabled = false;
			this.updateServerTextBox.Location = new System.Drawing.Point(108, 89);
			this.updateServerTextBox.Name = "updateServerTextBox";
			this.updateServerTextBox.Size = new System.Drawing.Size(351, 20);
			this.updateServerTextBox.TabIndex = 2;
			// 
			// OnlineSettingsForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(489, 222);
			this.Controls.Add(this.automaticUpdateGroupBox);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OnlineSettingsForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Subscription Settings";
			this.automaticUpdateGroupBox.ResumeLayout(false);
			this.automaticUpdateGroupBox.PerformLayout();
			this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.Button okButton;
	private System.Windows.Forms.Button cancelButton;
	private System.Windows.Forms.GroupBox automaticUpdateGroupBox;
	private System.Windows.Forms.CheckBox enableCheckBox;
	private System.Windows.Forms.TextBox updateServerTextBox;
	private System.Windows.Forms.Label updateServerLabel;
	private System.Windows.Forms.LinkLabel infoLinkLabel;
	private System.Windows.Forms.Label fileVersionLabel;
	private System.Windows.Forms.TextBox fileVersionTextBox;
	private System.Windows.Forms.Label fileNameLabel;
	private System.Windows.Forms.TextBox fileNameTextBox;
}
