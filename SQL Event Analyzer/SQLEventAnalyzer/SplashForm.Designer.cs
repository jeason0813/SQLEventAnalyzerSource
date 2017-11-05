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

partial class SplashForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashForm));
			this.initializingLabel = new System.Windows.Forms.Label();
			this.backgroundLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// initializingLabel
			// 
			this.initializingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.initializingLabel.AutoSize = true;
			this.initializingLabel.BackColor = System.Drawing.Color.White;
			this.initializingLabel.ForeColor = System.Drawing.SystemColors.Desktop;
			this.initializingLabel.Location = new System.Drawing.Point(66, 13);
			this.initializingLabel.Name = "initializingLabel";
			this.initializingLabel.Size = new System.Drawing.Size(61, 13);
			this.initializingLabel.TabIndex = 2;
			this.initializingLabel.Text = "Initializing...";
			// 
			// backgroundLabel
			// 
			this.backgroundLabel.BackColor = System.Drawing.Color.White;
			this.backgroundLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.backgroundLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.backgroundLabel.Location = new System.Drawing.Point(0, 0);
			this.backgroundLabel.Name = "backgroundLabel";
			this.backgroundLabel.Size = new System.Drawing.Size(194, 39);
			this.backgroundLabel.TabIndex = 3;
			// 
			// SplashForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(194, 39);
			this.ControlBox = false;
			this.Controls.Add(this.initializingLabel);
			this.Controls.Add(this.backgroundLabel);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SplashForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Title";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.SplashForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Label initializingLabel;
	private System.Windows.Forms.Label backgroundLabel;

}
