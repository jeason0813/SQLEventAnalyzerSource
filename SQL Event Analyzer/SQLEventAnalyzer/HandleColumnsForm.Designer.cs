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

partial class HandleColumnsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HandleColumnsForm));
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.infoLabel1 = new System.Windows.Forms.Label();
			this.timeTextBox = new System.Windows.Forms.TextBox();
			this.elapsedTimeTimer = new System.Windows.Forms.Timer(this.components);
			this.progressBar2 = new System.Windows.Forms.ProgressBar();
			this.infoLabel2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(12, 12);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(399, 23);
			this.progressBar1.TabIndex = 0;
			// 
			// infoLabel1
			// 
			this.infoLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.infoLabel1.Location = new System.Drawing.Point(12, 45);
			this.infoLabel1.Name = "infoLabel1";
			this.infoLabel1.Size = new System.Drawing.Size(399, 13);
			this.infoLabel1.TabIndex = 2;
			this.infoLabel1.Text = "Preparing Custom Column";
			// 
			// timeTextBox
			// 
			this.timeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.timeTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.timeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.timeTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.timeTextBox.Location = new System.Drawing.Point(345, 106);
			this.timeTextBox.Name = "timeTextBox";
			this.timeTextBox.ReadOnly = true;
			this.timeTextBox.ShortcutsEnabled = false;
			this.timeTextBox.Size = new System.Drawing.Size(66, 13);
			this.timeTextBox.TabIndex = 6;
			this.timeTextBox.TabStop = false;
			this.timeTextBox.Text = "00:00:00:00";
			this.timeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.timeTextBox.Enter += new System.EventHandler(this.TimeTextBox_Enter);
			this.timeTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TimeTextBox_MouseDown);
			// 
			// elapsedTimeTimer
			// 
			this.elapsedTimeTimer.Tick += new System.EventHandler(this.ElapsedTimeTimer_Tick);
			// 
			// progressBar2
			// 
			this.progressBar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar2.Location = new System.Drawing.Point(12, 73);
			this.progressBar2.Name = "progressBar2";
			this.progressBar2.Size = new System.Drawing.Size(399, 23);
			this.progressBar2.TabIndex = 7;
			// 
			// infoLabel2
			// 
			this.infoLabel2.AutoSize = true;
			this.infoLabel2.Location = new System.Drawing.Point(12, 106);
			this.infoLabel2.Name = "infoLabel2";
			this.infoLabel2.Size = new System.Drawing.Size(61, 13);
			this.infoLabel2.TabIndex = 8;
			this.infoLabel2.Text = "Preparing...";
			// 
			// HandleColumnsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(423, 129);
			this.ControlBox = false;
			this.Controls.Add(this.infoLabel2);
			this.Controls.Add(this.progressBar2);
			this.Controls.Add(this.timeTextBox);
			this.Controls.Add(this.infoLabel1);
			this.Controls.Add(this.progressBar1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "HandleColumnsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Working";
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.ProgressBar progressBar1;
	private System.Windows.Forms.Label infoLabel1;
	private System.Windows.Forms.TextBox timeTextBox;
	private System.Windows.Forms.Timer elapsedTimeTimer;
	private System.Windows.Forms.ProgressBar progressBar2;
	private System.Windows.Forms.Label infoLabel2;
}
