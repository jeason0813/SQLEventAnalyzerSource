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

partial class ExportForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportForm));
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.optionsGroupBox = new System.Windows.Forms.GroupBox();
			this.currentPageRadioButton = new System.Windows.Forms.RadioButton();
			this.allPagesRadioButton = new System.Windows.Forms.RadioButton();
			this.formatGroupBox = new System.Windows.Forms.GroupBox();
			this.sqlRadioButton = new System.Windows.Forms.RadioButton();
			this.databaseNameTextBox = new System.Windows.Forms.TextBox();
			this.databaseNameLabel = new System.Windows.Forms.Label();
			this.pttRadioButton = new System.Windows.Forms.RadioButton();
			this.csvRadioButton = new System.Windows.Forms.RadioButton();
			this.sessionNameTextBox = new System.Windows.Forms.TextBox();
			this.sessionNameLabel = new System.Windows.Forms.Label();
			this.sessionRadioButton = new System.Windows.Forms.RadioButton();
			this.optionsGroupBox.SuspendLayout();
			this.formatGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(336, 185);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 24);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(255, 185);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 24);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// optionsGroupBox
			// 
			this.optionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.optionsGroupBox.Controls.Add(this.currentPageRadioButton);
			this.optionsGroupBox.Controls.Add(this.allPagesRadioButton);
			this.optionsGroupBox.Location = new System.Drawing.Point(12, 12);
			this.optionsGroupBox.Name = "optionsGroupBox";
			this.optionsGroupBox.Size = new System.Drawing.Size(132, 166);
			this.optionsGroupBox.TabIndex = 2;
			this.optionsGroupBox.TabStop = false;
			this.optionsGroupBox.Text = "Options";
			// 
			// currentPageRadioButton
			// 
			this.currentPageRadioButton.AutoSize = true;
			this.currentPageRadioButton.Location = new System.Drawing.Point(6, 42);
			this.currentPageRadioButton.Name = "currentPageRadioButton";
			this.currentPageRadioButton.Size = new System.Drawing.Size(86, 17);
			this.currentPageRadioButton.TabIndex = 1;
			this.currentPageRadioButton.Text = "Current page";
			this.currentPageRadioButton.UseVisualStyleBackColor = true;
			// 
			// allPagesRadioButton
			// 
			this.allPagesRadioButton.AutoSize = true;
			this.allPagesRadioButton.Checked = true;
			this.allPagesRadioButton.Location = new System.Drawing.Point(6, 19);
			this.allPagesRadioButton.Name = "allPagesRadioButton";
			this.allPagesRadioButton.Size = new System.Drawing.Size(68, 17);
			this.allPagesRadioButton.TabIndex = 0;
			this.allPagesRadioButton.TabStop = true;
			this.allPagesRadioButton.Text = "All pages";
			this.allPagesRadioButton.UseVisualStyleBackColor = true;
			// 
			// formatGroupBox
			// 
			this.formatGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.formatGroupBox.Controls.Add(this.sessionNameTextBox);
			this.formatGroupBox.Controls.Add(this.sessionNameLabel);
			this.formatGroupBox.Controls.Add(this.sessionRadioButton);
			this.formatGroupBox.Controls.Add(this.sqlRadioButton);
			this.formatGroupBox.Controls.Add(this.databaseNameTextBox);
			this.formatGroupBox.Controls.Add(this.databaseNameLabel);
			this.formatGroupBox.Controls.Add(this.pttRadioButton);
			this.formatGroupBox.Controls.Add(this.csvRadioButton);
			this.formatGroupBox.Location = new System.Drawing.Point(150, 12);
			this.formatGroupBox.Name = "formatGroupBox";
			this.formatGroupBox.Size = new System.Drawing.Size(261, 166);
			this.formatGroupBox.TabIndex = 3;
			this.formatGroupBox.TabStop = false;
			this.formatGroupBox.Text = "Format";
			// 
			// sqlRadioButton
			// 
			this.sqlRadioButton.AutoSize = true;
			this.sqlRadioButton.Location = new System.Drawing.Point(6, 42);
			this.sqlRadioButton.Name = "sqlRadioButton";
			this.sqlRadioButton.Size = new System.Drawing.Size(46, 17);
			this.sqlRadioButton.TabIndex = 1;
			this.sqlRadioButton.Text = "SQL";
			this.sqlRadioButton.UseVisualStyleBackColor = true;
			// 
			// databaseNameTextBox
			// 
			this.databaseNameTextBox.Enabled = false;
			this.databaseNameTextBox.Location = new System.Drawing.Point(97, 88);
			this.databaseNameTextBox.Name = "databaseNameTextBox";
			this.databaseNameTextBox.Size = new System.Drawing.Size(155, 20);
			this.databaseNameTextBox.TabIndex = 3;
			// 
			// databaseNameLabel
			// 
			this.databaseNameLabel.AutoSize = true;
			this.databaseNameLabel.Enabled = false;
			this.databaseNameLabel.Location = new System.Drawing.Point(6, 91);
			this.databaseNameLabel.Name = "databaseNameLabel";
			this.databaseNameLabel.Size = new System.Drawing.Size(85, 13);
			this.databaseNameLabel.TabIndex = 2;
			this.databaseNameLabel.Text = "Database name:";
			// 
			// pttRadioButton
			// 
			this.pttRadioButton.AutoSize = true;
			this.pttRadioButton.Location = new System.Drawing.Point(6, 65);
			this.pttRadioButton.Name = "pttRadioButton";
			this.pttRadioButton.Size = new System.Drawing.Size(209, 17);
			this.pttRadioButton.TabIndex = 2;
			this.pttRadioButton.Text = "Performance Test Tool Task Collection";
			this.pttRadioButton.UseVisualStyleBackColor = true;
			this.pttRadioButton.CheckedChanged += new System.EventHandler(this.PttRadioButton_CheckedChanged);
			// 
			// csvRadioButton
			// 
			this.csvRadioButton.AutoSize = true;
			this.csvRadioButton.Checked = true;
			this.csvRadioButton.Location = new System.Drawing.Point(6, 19);
			this.csvRadioButton.Name = "csvRadioButton";
			this.csvRadioButton.Size = new System.Drawing.Size(46, 17);
			this.csvRadioButton.TabIndex = 0;
			this.csvRadioButton.TabStop = true;
			this.csvRadioButton.Text = "CSV";
			this.csvRadioButton.UseVisualStyleBackColor = true;
			// 
			// sessionNameTextBox
			// 
			this.sessionNameTextBox.Enabled = false;
			this.sessionNameTextBox.Location = new System.Drawing.Point(97, 137);
			this.sessionNameTextBox.Name = "sessionNameTextBox";
			this.sessionNameTextBox.Size = new System.Drawing.Size(155, 20);
			this.sessionNameTextBox.TabIndex = 5;
			// 
			// sessionNameLabel
			// 
			this.sessionNameLabel.AutoSize = true;
			this.sessionNameLabel.Enabled = false;
			this.sessionNameLabel.Location = new System.Drawing.Point(6, 140);
			this.sessionNameLabel.Name = "sessionNameLabel";
			this.sessionNameLabel.Size = new System.Drawing.Size(76, 13);
			this.sessionNameLabel.TabIndex = 4;
			this.sessionNameLabel.Text = "Session name:";
			// 
			// sessionRadioButton
			// 
			this.sessionRadioButton.AutoSize = true;
			this.sessionRadioButton.Location = new System.Drawing.Point(6, 114);
			this.sessionRadioButton.Name = "sessionRadioButton";
			this.sessionRadioButton.Size = new System.Drawing.Size(62, 17);
			this.sessionRadioButton.TabIndex = 4;
			this.sessionRadioButton.Text = "Session";
			this.sessionRadioButton.UseVisualStyleBackColor = true;
			this.sessionRadioButton.CheckedChanged += new System.EventHandler(this.SessionRadioButton_CheckedChanged);
			// 
			// ExportForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(423, 216);
			this.Controls.Add(this.formatGroupBox);
			this.Controls.Add(this.optionsGroupBox);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExportForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Export";
			this.optionsGroupBox.ResumeLayout(false);
			this.optionsGroupBox.PerformLayout();
			this.formatGroupBox.ResumeLayout(false);
			this.formatGroupBox.PerformLayout();
			this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.Button cancelButton;
	private System.Windows.Forms.Button okButton;
	private System.Windows.Forms.GroupBox optionsGroupBox;
	private System.Windows.Forms.RadioButton currentPageRadioButton;
	private System.Windows.Forms.RadioButton allPagesRadioButton;
	private System.Windows.Forms.GroupBox formatGroupBox;
	private System.Windows.Forms.RadioButton pttRadioButton;
	private System.Windows.Forms.RadioButton csvRadioButton;
	private System.Windows.Forms.TextBox databaseNameTextBox;
	private System.Windows.Forms.Label databaseNameLabel;
	private System.Windows.Forms.RadioButton sqlRadioButton;
	private System.Windows.Forms.TextBox sessionNameTextBox;
	private System.Windows.Forms.Label sessionNameLabel;
	private System.Windows.Forms.RadioButton sessionRadioButton;
}
