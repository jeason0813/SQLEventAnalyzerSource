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

partial class ImportSessionForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportSessionForm));
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.timeTextBox = new System.Windows.Forms.TextBox();
			this.elapsedTimeTimer = new System.Windows.Forms.Timer(this.components);
			this.sessionLabel = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.sizeLabel = new System.Windows.Forms.Label();
			this.nameValueLabel = new System.Windows.Forms.Label();
			this.sessionValueLabel = new System.Windows.Forms.Label();
			this.sizeValueLabel = new System.Windows.Forms.Label();
			this.statusValueLabel = new System.Windows.Forms.Label();
			this.statusLabel = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(12, 12);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(399, 23);
			this.progressBar1.TabIndex = 1;
			// 
			// timeTextBox
			// 
			this.timeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.timeTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.timeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.timeTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.timeTextBox.Location = new System.Drawing.Point(345, 45);
			this.timeTextBox.Name = "timeTextBox";
			this.timeTextBox.ReadOnly = true;
			this.timeTextBox.ShortcutsEnabled = false;
			this.timeTextBox.Size = new System.Drawing.Size(66, 13);
			this.timeTextBox.TabIndex = 5;
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
			// sessionLabel
			// 
			this.sessionLabel.AutoSize = true;
			this.sessionLabel.Location = new System.Drawing.Point(12, 72);
			this.sessionLabel.Name = "sessionLabel";
			this.sessionLabel.Size = new System.Drawing.Size(26, 13);
			this.sessionLabel.TabIndex = 6;
			this.sessionLabel.Text = "Session:";
			// 
			// nameLabel
			// 
			this.nameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(12, 91);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(38, 13);
			this.nameLabel.TabIndex = 7;
			this.nameLabel.Text = "Name:";
			// 
			// sizeLabel
			// 
			this.sizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.sizeLabel.AutoSize = true;
			this.sizeLabel.Location = new System.Drawing.Point(12, 110);
			this.sizeLabel.Name = "sizeLabel";
			this.sizeLabel.Size = new System.Drawing.Size(30, 13);
			this.sizeLabel.TabIndex = 8;
			this.sizeLabel.Text = "Size:";
			// 
			// nameValueLabel
			// 
			this.nameValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nameValueLabel.Location = new System.Drawing.Point(61, 91);
			this.nameValueLabel.Name = "nameValueLabel";
			this.nameValueLabel.Size = new System.Drawing.Size(350, 13);
			this.nameValueLabel.TabIndex = 9;
			// 
			// sessionValueLabel
			// 
			this.sessionValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sessionValueLabel.Location = new System.Drawing.Point(61, 72);
			this.sessionValueLabel.Name = "sessionValueLabel";
			this.sessionValueLabel.Size = new System.Drawing.Size(350, 13);
			this.sessionValueLabel.TabIndex = 10;
			// 
			// sizeValueLabel
			// 
			this.sizeValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sizeValueLabel.Location = new System.Drawing.Point(61, 110);
			this.sizeValueLabel.Name = "sizeValueLabel";
			this.sizeValueLabel.Size = new System.Drawing.Size(350, 13);
			this.sizeValueLabel.TabIndex = 11;
			// 
			// statusValueLabel
			// 
			this.statusValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.statusValueLabel.Location = new System.Drawing.Point(61, 45);
			this.statusValueLabel.Name = "statusValueLabel";
			this.statusValueLabel.Size = new System.Drawing.Size(278, 13);
			this.statusValueLabel.TabIndex = 13;
			// 
			// statusLabel
			// 
			this.statusLabel.AutoSize = true;
			this.statusLabel.Location = new System.Drawing.Point(12, 45);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(40, 13);
			this.statusLabel.TabIndex = 12;
			this.statusLabel.Text = "Status:";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label3.Location = new System.Drawing.Point(15, 65);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(396, 2);
			this.label3.TabIndex = 14;
			// 
			// ImportSessionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(423, 130);
			this.ControlBox = false;
			this.Controls.Add(this.label3);
			this.Controls.Add(this.statusValueLabel);
			this.Controls.Add(this.statusLabel);
			this.Controls.Add(this.sizeValueLabel);
			this.Controls.Add(this.sessionValueLabel);
			this.Controls.Add(this.nameValueLabel);
			this.Controls.Add(this.sizeLabel);
			this.Controls.Add(this.nameLabel);
			this.Controls.Add(this.sessionLabel);
			this.Controls.Add(this.timeTextBox);
			this.Controls.Add(this.progressBar1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImportSessionForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Analysing Trace Data";
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.ProgressBar progressBar1;
	private System.Windows.Forms.TextBox timeTextBox;
	private System.Windows.Forms.Timer elapsedTimeTimer;
	private System.Windows.Forms.Label sessionLabel;
	private System.Windows.Forms.Label nameLabel;
	private System.Windows.Forms.Label sizeLabel;
	private System.Windows.Forms.Label nameValueLabel;
	private System.Windows.Forms.Label sessionValueLabel;
	private System.Windows.Forms.Label sizeValueLabel;
	private System.Windows.Forms.Label statusValueLabel;
	private System.Windows.Forms.Label statusLabel;
	private System.Windows.Forms.Label label3;
}
