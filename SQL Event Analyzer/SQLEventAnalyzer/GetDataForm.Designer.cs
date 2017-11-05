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

partial class GetDataForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetDataForm));
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.infoLabel = new System.Windows.Forms.Label();
			this.timeTextBox = new System.Windows.Forms.TextBox();
			this.elapsedTimeTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(12, 12);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(399, 23);
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar1.TabIndex = 0;
			this.progressBar1.Value = 1;
			// 
			// infoLabel
			// 
			this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.infoLabel.AutoSize = true;
			this.infoLabel.Location = new System.Drawing.Point(12, 45);
			this.infoLabel.Name = "infoLabel";
			this.infoLabel.Size = new System.Drawing.Size(83, 13);
			this.infoLabel.TabIndex = 1;
			this.infoLabel.Text = "Fetching Data...";
			// 
			// timeTextBox
			// 
			this.timeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.timeTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.timeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.timeTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.timeTextBox.Location = new System.Drawing.Point(345, 45);
			this.timeTextBox.Name = "timeTextBox";
			this.timeTextBox.ReadOnly = true;
			this.timeTextBox.ShortcutsEnabled = false;
			this.timeTextBox.Size = new System.Drawing.Size(66, 13);
			this.timeTextBox.TabIndex = 7;
			this.timeTextBox.TabStop = false;
			this.timeTextBox.Text = "00:00:00:00";
			this.timeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.timeTextBox.Enter += new System.EventHandler(this.TimeTextBox_Enter);
			// 
			// elapsedTimeTimer
			// 
			this.elapsedTimeTimer.Tick += new System.EventHandler(this.ElapsedTimeTimer_Tick);
			// 
			// GetDataForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(423, 67);
			this.ControlBox = false;
			this.Controls.Add(this.timeTextBox);
			this.Controls.Add(this.infoLabel);
			this.Controls.Add(this.progressBar1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "GetDataForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Working";
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.ProgressBar progressBar1;
	private System.Windows.Forms.Label infoLabel;
	private System.Windows.Forms.TextBox timeTextBox;
	private System.Windows.Forms.Timer elapsedTimeTimer;
}
