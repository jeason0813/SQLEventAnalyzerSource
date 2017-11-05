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

partial class HandleCLRForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HandleCLRForm));
			this.clrLabel = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.enableCLRButton = new System.Windows.Forms.Button();
			this.noRegExButton = new System.Windows.Forms.Button();
			this.exitButton = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// clrLabel
			// 
			this.clrLabel.AutoSize = true;
			this.clrLabel.Location = new System.Drawing.Point(55, 12);
			this.clrLabel.Name = "clrLabel";
			this.clrLabel.Size = new System.Drawing.Size(297, 39);
			this.clrLabel.TabIndex = 0;
			this.clrLabel.Text = "CLR support is not enabled on the SQL Server.\r\n\r\nFor all features to be available" +
    ", CLR support must be enabled.";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label3.Location = new System.Drawing.Point(12, 71);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(454, 2);
			this.label3.TabIndex = 5;
			// 
			// enableCLRButton
			// 
			this.enableCLRButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.enableCLRButton.Location = new System.Drawing.Point(12, 80);
			this.enableCLRButton.Name = "enableCLRButton";
			this.enableCLRButton.Size = new System.Drawing.Size(183, 24);
			this.enableCLRButton.TabIndex = 0;
			this.enableCLRButton.Text = "Enable CLR support";
			this.enableCLRButton.UseVisualStyleBackColor = true;
			this.enableCLRButton.Click += new System.EventHandler(this.EnableCLRButton_Click);
			// 
			// noRegExButton
			// 
			this.noRegExButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.noRegExButton.Location = new System.Drawing.Point(201, 80);
			this.noRegExButton.Name = "noRegExButton";
			this.noRegExButton.Size = new System.Drawing.Size(183, 24);
			this.noRegExButton.TabIndex = 1;
			this.noRegExButton.Text = "Continue without CLR support";
			this.noRegExButton.UseVisualStyleBackColor = true;
			this.noRegExButton.Click += new System.EventHandler(this.NoRegExButton_Click);
			// 
			// exitButton
			// 
			this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.exitButton.Location = new System.Drawing.Point(390, 80);
			this.exitButton.Name = "exitButton";
			this.exitButton.Size = new System.Drawing.Size(75, 24);
			this.exitButton.TabIndex = 2;
			this.exitButton.Text = "Exit";
			this.exitButton.UseVisualStyleBackColor = true;
			this.exitButton.Click += new System.EventHandler(this.ExitButton_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::SQLEventAnalyzer.Properties.Resources.information;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.TabIndex = 6;
			this.pictureBox1.TabStop = false;
			// 
			// HandleCLRForm
			// 
			this.AcceptButton = this.enableCLRButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.exitButton;
			this.ClientSize = new System.Drawing.Size(478, 111);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.exitButton);
			this.Controls.Add(this.noRegExButton);
			this.Controls.Add(this.enableCLRButton);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.clrLabel);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HandleCLRForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Title";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HandleCLRForm_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Label clrLabel;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.Button enableCLRButton;
	private System.Windows.Forms.Button noRegExButton;
	private System.Windows.Forms.Button exitButton;
	private System.Windows.Forms.PictureBox pictureBox1;
}
