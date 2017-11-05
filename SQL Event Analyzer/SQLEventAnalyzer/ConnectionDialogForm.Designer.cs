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

partial class ConnectionDialogForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionDialogForm));
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.serverNameLabel = new System.Windows.Forms.Label();
			this.authenticationLabel = new System.Windows.Forms.Label();
			this.usernameLabel = new System.Windows.Forms.Label();
			this.passwordLabel = new System.Windows.Forms.Label();
			this.userNameTextBox = new System.Windows.Forms.TextBox();
			this.passwordTextBox = new System.Windows.Forms.TextBox();
			this.saveValuesCheckBox = new System.Windows.Forms.CheckBox();
			this.connectToSqlServerGroupBox = new System.Windows.Forms.GroupBox();
			this.deleteLinkLabel = new System.Windows.Forms.LinkLabel();
			this.serverNameComboBox = new System.Windows.Forms.ComboBox();
			this.authenticationComboBox = new ComboBoxCustom();
			this.connectToSqlServerGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.BackColor = System.Drawing.SystemColors.Control;
			this.okButton.Location = new System.Drawing.Point(255, 150);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 24);
			this.okButton.TabIndex = 7;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = false;
			this.okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.BackColor = System.Drawing.SystemColors.Control;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(336, 150);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 24);
			this.cancelButton.TabIndex = 8;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = false;
			this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// serverNameLabel
			// 
			this.serverNameLabel.AutoSize = true;
			this.serverNameLabel.BackColor = System.Drawing.SystemColors.Control;
			this.serverNameLabel.Location = new System.Drawing.Point(6, 22);
			this.serverNameLabel.Name = "serverNameLabel";
			this.serverNameLabel.Size = new System.Drawing.Size(70, 13);
			this.serverNameLabel.TabIndex = 11;
			this.serverNameLabel.Text = "Server name:";
			// 
			// authenticationLabel
			// 
			this.authenticationLabel.AutoSize = true;
			this.authenticationLabel.BackColor = System.Drawing.SystemColors.Control;
			this.authenticationLabel.Location = new System.Drawing.Point(6, 48);
			this.authenticationLabel.Name = "authenticationLabel";
			this.authenticationLabel.Size = new System.Drawing.Size(78, 13);
			this.authenticationLabel.TabIndex = 12;
			this.authenticationLabel.Text = "Authentication:";
			// 
			// usernameLabel
			// 
			this.usernameLabel.AutoSize = true;
			this.usernameLabel.BackColor = System.Drawing.SystemColors.Control;
			this.usernameLabel.Location = new System.Drawing.Point(22, 73);
			this.usernameLabel.Name = "usernameLabel";
			this.usernameLabel.Size = new System.Drawing.Size(61, 13);
			this.usernameLabel.TabIndex = 14;
			this.usernameLabel.Text = "User name:";
			// 
			// passwordLabel
			// 
			this.passwordLabel.AutoSize = true;
			this.passwordLabel.BackColor = System.Drawing.SystemColors.Control;
			this.passwordLabel.Location = new System.Drawing.Point(22, 101);
			this.passwordLabel.Name = "passwordLabel";
			this.passwordLabel.Size = new System.Drawing.Size(56, 13);
			this.passwordLabel.TabIndex = 15;
			this.passwordLabel.Text = "Password:";
			// 
			// userNameTextBox
			// 
			this.userNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.userNameTextBox.BackColor = System.Drawing.Color.White;
			this.userNameTextBox.Location = new System.Drawing.Point(130, 72);
			this.userNameTextBox.Name = "userNameTextBox";
			this.userNameTextBox.Size = new System.Drawing.Size(263, 20);
			this.userNameTextBox.TabIndex = 3;
			// 
			// passwordTextBox
			// 
			this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.passwordTextBox.BackColor = System.Drawing.Color.White;
			this.passwordTextBox.Location = new System.Drawing.Point(130, 98);
			this.passwordTextBox.Name = "passwordTextBox";
			this.passwordTextBox.PasswordChar = '*';
			this.passwordTextBox.Size = new System.Drawing.Size(219, 20);
			this.passwordTextBox.TabIndex = 4;
			// 
			// saveValuesCheckBox
			// 
			this.saveValuesCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.saveValuesCheckBox.AutoSize = true;
			this.saveValuesCheckBox.BackColor = System.Drawing.Color.Transparent;
			this.saveValuesCheckBox.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.saveValuesCheckBox.Location = new System.Drawing.Point(12, 154);
			this.saveValuesCheckBox.Name = "saveValuesCheckBox";
			this.saveValuesCheckBox.Size = new System.Drawing.Size(85, 17);
			this.saveValuesCheckBox.TabIndex = 6;
			this.saveValuesCheckBox.Text = "Save values";
			this.saveValuesCheckBox.UseVisualStyleBackColor = false;
			// 
			// connectToSqlServerGroupBox
			// 
			this.connectToSqlServerGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.connectToSqlServerGroupBox.Controls.Add(this.deleteLinkLabel);
			this.connectToSqlServerGroupBox.Controls.Add(this.serverNameComboBox);
			this.connectToSqlServerGroupBox.Controls.Add(this.passwordLabel);
			this.connectToSqlServerGroupBox.Controls.Add(this.serverNameLabel);
			this.connectToSqlServerGroupBox.Controls.Add(this.userNameTextBox);
			this.connectToSqlServerGroupBox.Controls.Add(this.authenticationLabel);
			this.connectToSqlServerGroupBox.Controls.Add(this.usernameLabel);
			this.connectToSqlServerGroupBox.Controls.Add(this.passwordTextBox);
			this.connectToSqlServerGroupBox.Controls.Add(this.authenticationComboBox);
			this.connectToSqlServerGroupBox.Location = new System.Drawing.Point(12, 11);
			this.connectToSqlServerGroupBox.Name = "connectToSqlServerGroupBox";
			this.connectToSqlServerGroupBox.Size = new System.Drawing.Size(399, 131);
			this.connectToSqlServerGroupBox.TabIndex = 0;
			this.connectToSqlServerGroupBox.TabStop = false;
			this.connectToSqlServerGroupBox.Text = "Connect to SQL Server";
			// 
			// deleteLinkLabel
			// 
			this.deleteLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.deleteLinkLabel.AutoSize = true;
			this.deleteLinkLabel.Location = new System.Drawing.Point(355, 101);
			this.deleteLinkLabel.Name = "deleteLinkLabel";
			this.deleteLinkLabel.Size = new System.Drawing.Size(38, 13);
			this.deleteLinkLabel.TabIndex = 5;
			this.deleteLinkLabel.TabStop = true;
			this.deleteLinkLabel.Text = "Delete";
			this.deleteLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DeleteLinkLabel_LinkClicked);
			// 
			// serverNameComboBox
			// 
			this.serverNameComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.serverNameComboBox.FormattingEnabled = true;
			this.serverNameComboBox.Location = new System.Drawing.Point(107, 19);
			this.serverNameComboBox.Name = "serverNameComboBox";
			this.serverNameComboBox.Size = new System.Drawing.Size(286, 21);
			this.serverNameComboBox.TabIndex = 1;
			// 
			// authenticationComboBox
			// 
			this.authenticationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.authenticationComboBox.BackColor = System.Drawing.Color.White;
			this.authenticationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.authenticationComboBox.FormattingEnabled = true;
			this.authenticationComboBox.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Server Authentication"});
			this.authenticationComboBox.Location = new System.Drawing.Point(107, 45);
			this.authenticationComboBox.MaxDropDownItems = 2;
			this.authenticationComboBox.Name = "authenticationComboBox";
			this.authenticationComboBox.Size = new System.Drawing.Size(286, 21);
			this.authenticationComboBox.TabIndex = 2;
			this.authenticationComboBox.SelectedIndexChanged += new System.EventHandler(this.AuthenticationComboBox_SelectedIndexChanged);
			// 
			// ConnectionDialogForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(423, 181);
			this.Controls.Add(this.connectToSqlServerGroupBox);
			this.Controls.Add(this.saveValuesCheckBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConnectionDialogForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Title";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectionDialogForm_FormClosing);
			this.connectToSqlServerGroupBox.ResumeLayout(false);
			this.connectToSqlServerGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Button okButton;
	private System.Windows.Forms.Button cancelButton;
	private System.Windows.Forms.Label serverNameLabel;
	private System.Windows.Forms.Label authenticationLabel;
	private ComboBoxCustom authenticationComboBox;
	private System.Windows.Forms.Label usernameLabel;
	private System.Windows.Forms.Label passwordLabel;
	private System.Windows.Forms.TextBox userNameTextBox;
	private System.Windows.Forms.TextBox passwordTextBox;
	private System.Windows.Forms.CheckBox saveValuesCheckBox;
	private System.Windows.Forms.GroupBox connectToSqlServerGroupBox;
	private System.Windows.Forms.ComboBox serverNameComboBox;
	private System.Windows.Forms.LinkLabel deleteLinkLabel;
}
