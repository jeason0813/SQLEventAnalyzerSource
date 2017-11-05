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

partial class CustomColumnsCheckForUpdatesForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomColumnsCheckForUpdatesForm));
			this.closebutton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.infoTextBox = new System.Windows.Forms.TextBox();
			this.updateButton = new System.Windows.Forms.Button();
			this.changelogGroupBox = new System.Windows.Forms.GroupBox();
			this.changelogTextBox = new System.Windows.Forms.TextBox();
			this.changelogGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// closebutton
			// 
			this.closebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.closebutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.closebutton.Location = new System.Drawing.Point(336, 230);
			this.closebutton.Name = "closebutton";
			this.closebutton.Size = new System.Drawing.Size(75, 24);
			this.closebutton.TabIndex = 2;
			this.closebutton.Text = "Close";
			this.closebutton.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label3.Location = new System.Drawing.Point(12, 220);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(399, 2);
			this.label3.TabIndex = 6;
			// 
			// infoTextBox
			// 
			this.infoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.infoTextBox.Location = new System.Drawing.Point(12, 12);
			this.infoTextBox.Multiline = true;
			this.infoTextBox.Name = "infoTextBox";
			this.infoTextBox.ReadOnly = true;
			this.infoTextBox.Size = new System.Drawing.Size(399, 59);
			this.infoTextBox.TabIndex = 22;
			this.infoTextBox.TabStop = false;
			this.infoTextBox.WordWrap = false;
			// 
			// updateButton
			// 
			this.updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.updateButton.Location = new System.Drawing.Point(228, 230);
			this.updateButton.Name = "updateButton";
			this.updateButton.Size = new System.Drawing.Size(102, 24);
			this.updateButton.TabIndex = 1;
			this.updateButton.Text = "Check";
			this.updateButton.UseVisualStyleBackColor = true;
			this.updateButton.Click += new System.EventHandler(this.UpdateButton_Click);
			// 
			// changelogGroupBox
			// 
			this.changelogGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.changelogGroupBox.Controls.Add(this.changelogTextBox);
			this.changelogGroupBox.Location = new System.Drawing.Point(12, 77);
			this.changelogGroupBox.Name = "changelogGroupBox";
			this.changelogGroupBox.Size = new System.Drawing.Size(399, 140);
			this.changelogGroupBox.TabIndex = 23;
			this.changelogGroupBox.TabStop = false;
			this.changelogGroupBox.Text = "Changelog";
			this.changelogGroupBox.Visible = false;
			// 
			// changelogTextBox
			// 
			this.changelogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.changelogTextBox.Location = new System.Drawing.Point(6, 19);
			this.changelogTextBox.Multiline = true;
			this.changelogTextBox.Name = "changelogTextBox";
			this.changelogTextBox.ReadOnly = true;
			this.changelogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.changelogTextBox.Size = new System.Drawing.Size(387, 115);
			this.changelogTextBox.TabIndex = 24;
			this.changelogTextBox.TabStop = false;
			this.changelogTextBox.Visible = false;
			this.changelogTextBox.WordWrap = false;
			// 
			// CustomColumnsCheckForUpdatesForm
			// 
			this.AcceptButton = this.updateButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.closebutton;
			this.ClientSize = new System.Drawing.Size(423, 261);
			this.ControlBox = false;
			this.Controls.Add(this.changelogGroupBox);
			this.Controls.Add(this.updateButton);
			this.Controls.Add(this.infoTextBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.closebutton);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CustomColumnsCheckForUpdatesForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Check for updates";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CheckForUpdatesForm_FormClosing);
			this.changelogGroupBox.ResumeLayout(false);
			this.changelogGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Button closebutton;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.TextBox infoTextBox;
	private System.Windows.Forms.Button updateButton;
	private System.Windows.Forms.GroupBox changelogGroupBox;
	private System.Windows.Forms.TextBox changelogTextBox;
}
