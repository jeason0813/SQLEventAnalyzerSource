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

partial class EncryptPasswordForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EncryptPasswordForm));
			this.okButton = new System.Windows.Forms.Button();
			this.passwordGroupBox = new System.Windows.Forms.GroupBox();
			this.copyLinkLabel = new System.Windows.Forms.LinkLabel();
			this.encryptedLabel = new System.Windows.Forms.Label();
			this.decryptedLabel = new System.Windows.Forms.Label();
			this.encryptedTextBox = new System.Windows.Forms.TextBox();
			this.decryptedTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.infoLabel = new System.Windows.Forms.LinkLabel();
			this.passwordGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(232, 125);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 24);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// passwordGroupBox
			// 
			this.passwordGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.passwordGroupBox.Controls.Add(this.copyLinkLabel);
			this.passwordGroupBox.Controls.Add(this.encryptedLabel);
			this.passwordGroupBox.Controls.Add(this.decryptedLabel);
			this.passwordGroupBox.Controls.Add(this.encryptedTextBox);
			this.passwordGroupBox.Controls.Add(this.decryptedTextBox);
			this.passwordGroupBox.Location = new System.Drawing.Point(12, 12);
			this.passwordGroupBox.Name = "passwordGroupBox";
			this.passwordGroupBox.Size = new System.Drawing.Size(295, 71);
			this.passwordGroupBox.TabIndex = 0;
			this.passwordGroupBox.TabStop = false;
			this.passwordGroupBox.Text = "Password";
			// 
			// copyLinkLabel
			// 
			this.copyLinkLabel.AutoSize = true;
			this.copyLinkLabel.Enabled = false;
			this.copyLinkLabel.Location = new System.Drawing.Point(259, 48);
			this.copyLinkLabel.Name = "copyLinkLabel";
			this.copyLinkLabel.Size = new System.Drawing.Size(30, 13);
			this.copyLinkLabel.TabIndex = 1;
			this.copyLinkLabel.TabStop = true;
			this.copyLinkLabel.Text = "copy";
			this.copyLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CopyLinkLabel_LinkClicked);
			// 
			// encryptedLabel
			// 
			this.encryptedLabel.AutoSize = true;
			this.encryptedLabel.Location = new System.Drawing.Point(6, 48);
			this.encryptedLabel.Name = "encryptedLabel";
			this.encryptedLabel.Size = new System.Drawing.Size(58, 13);
			this.encryptedLabel.TabIndex = 3;
			this.encryptedLabel.Text = "Encrypted:";
			// 
			// decryptedLabel
			// 
			this.decryptedLabel.AutoSize = true;
			this.decryptedLabel.Location = new System.Drawing.Point(6, 22);
			this.decryptedLabel.Name = "decryptedLabel";
			this.decryptedLabel.Size = new System.Drawing.Size(59, 13);
			this.decryptedLabel.TabIndex = 2;
			this.decryptedLabel.Text = "Decrypted:";
			// 
			// encryptedTextBox
			// 
			this.encryptedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.encryptedTextBox.Location = new System.Drawing.Point(71, 45);
			this.encryptedTextBox.Name = "encryptedTextBox";
			this.encryptedTextBox.ReadOnly = true;
			this.encryptedTextBox.Size = new System.Drawing.Size(182, 20);
			this.encryptedTextBox.TabIndex = 1;
			// 
			// decryptedTextBox
			// 
			this.decryptedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.decryptedTextBox.Location = new System.Drawing.Point(71, 19);
			this.decryptedTextBox.Name = "decryptedTextBox";
			this.decryptedTextBox.Size = new System.Drawing.Size(218, 20);
			this.decryptedTextBox.TabIndex = 0;
			this.decryptedTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DecryptedTextBox_KeyUp);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label3.Location = new System.Drawing.Point(12, 117);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(295, 2);
			this.label3.TabIndex = 5;
			// 
			// infoLabel
			// 
			this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.infoLabel.BackColor = System.Drawing.SystemColors.Control;
			this.infoLabel.Enabled = false;
			this.infoLabel.Location = new System.Drawing.Point(12, 86);
			this.infoLabel.Name = "infoLabel";
			this.infoLabel.Size = new System.Drawing.Size(295, 31);
			this.infoLabel.TabIndex = 28;
			this.infoLabel.TabStop = true;
			this.infoLabel.Text = "See Command Line Parameters for encrypted password usage.";
			// 
			// EncryptPasswordForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.okButton;
			this.ClientSize = new System.Drawing.Size(319, 157);
			this.ControlBox = false;
			this.Controls.Add(this.infoLabel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.passwordGroupBox);
			this.Controls.Add(this.okButton);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "EncryptPasswordForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Encrypt password";
			this.passwordGroupBox.ResumeLayout(false);
			this.passwordGroupBox.PerformLayout();
			this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.Button okButton;
	private System.Windows.Forms.GroupBox passwordGroupBox;
	private System.Windows.Forms.TextBox encryptedTextBox;
	private System.Windows.Forms.TextBox decryptedTextBox;
	private System.Windows.Forms.Label encryptedLabel;
	private System.Windows.Forms.Label decryptedLabel;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.LinkLabel infoLabel;
	private System.Windows.Forms.LinkLabel copyLinkLabel;

}
