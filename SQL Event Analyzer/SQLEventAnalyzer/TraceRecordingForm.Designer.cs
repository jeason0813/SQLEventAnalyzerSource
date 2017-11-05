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

partial class TraceRecordingForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TraceRecordingForm));
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.startButton = new System.Windows.Forms.Button();
			this.pauseButton = new System.Windows.Forms.Button();
			this.stopButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.statusLabel = new System.Windows.Forms.Label();
			this.recordingPictureBox = new System.Windows.Forms.PictureBox();
			this.recordGroupBox = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.recordingPictureBox)).BeginInit();
			this.recordGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.BackColor = System.Drawing.SystemColors.Control;
			this.progressBar1.Location = new System.Drawing.Point(6, 19);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(306, 23);
			this.progressBar1.TabIndex = 0;
			// 
			// startButton
			// 
			this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.startButton.BackColor = System.Drawing.Color.Transparent;
			this.startButton.Image = global::SQLEventAnalyzer.Properties.Resources.stop_small;
			this.startButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.startButton.Location = new System.Drawing.Point(12, 87);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(75, 24);
			this.startButton.TabIndex = 1;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = false;
			this.startButton.Click += new System.EventHandler(this.StartButton_Click);
			// 
			// pauseButton
			// 
			this.pauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pauseButton.BackColor = System.Drawing.Color.Transparent;
			this.pauseButton.Enabled = false;
			this.pauseButton.Image = global::SQLEventAnalyzer.Properties.Resources.control_pause;
			this.pauseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.pauseButton.Location = new System.Drawing.Point(93, 87);
			this.pauseButton.Name = "pauseButton";
			this.pauseButton.Size = new System.Drawing.Size(75, 24);
			this.pauseButton.TabIndex = 2;
			this.pauseButton.Text = "Pause";
			this.pauseButton.UseVisualStyleBackColor = false;
			this.pauseButton.Click += new System.EventHandler(this.PauseButton_Click);
			// 
			// stopButton
			// 
			this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.stopButton.BackColor = System.Drawing.Color.Transparent;
			this.stopButton.Enabled = false;
			this.stopButton.Image = global::SQLEventAnalyzer.Properties.Resources.control_stop;
			this.stopButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.stopButton.Location = new System.Drawing.Point(174, 87);
			this.stopButton.Name = "stopButton";
			this.stopButton.Size = new System.Drawing.Size(75, 24);
			this.stopButton.TabIndex = 3;
			this.stopButton.Text = "Stop";
			this.stopButton.UseVisualStyleBackColor = false;
			this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cancelButton.BackColor = System.Drawing.Color.Transparent;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(255, 87);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 24);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = false;
			// 
			// statusLabel
			// 
			this.statusLabel.AutoSize = true;
			this.statusLabel.BackColor = System.Drawing.Color.Transparent;
			this.statusLabel.Location = new System.Drawing.Point(8, 48);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(60, 13);
			this.statusLabel.TabIndex = 5;
			this.statusLabel.Text = "Status: Idle";
			// 
			// recordingPictureBox
			// 
			this.recordingPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.recordingPictureBox.Image = global::SQLEventAnalyzer.Properties.Resources.stop_small;
			this.recordingPictureBox.Location = new System.Drawing.Point(297, 46);
			this.recordingPictureBox.Name = "recordingPictureBox";
			this.recordingPictureBox.Size = new System.Drawing.Size(16, 16);
			this.recordingPictureBox.TabIndex = 7;
			this.recordingPictureBox.TabStop = false;
			this.recordingPictureBox.Visible = false;
			// 
			// recordGroupBox
			// 
			this.recordGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.recordGroupBox.Controls.Add(this.progressBar1);
			this.recordGroupBox.Controls.Add(this.recordingPictureBox);
			this.recordGroupBox.Controls.Add(this.statusLabel);
			this.recordGroupBox.Location = new System.Drawing.Point(12, 12);
			this.recordGroupBox.Name = "recordGroupBox";
			this.recordGroupBox.Size = new System.Drawing.Size(318, 67);
			this.recordGroupBox.TabIndex = 8;
			this.recordGroupBox.TabStop = false;
			this.recordGroupBox.Text = "Record";
			// 
			// TraceRecordingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(342, 118);
			this.Controls.Add(this.recordGroupBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.stopButton);
			this.Controls.Add(this.pauseButton);
			this.Controls.Add(this.startButton);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TraceRecordingForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Record";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TraceRecordingForm_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.recordingPictureBox)).EndInit();
			this.recordGroupBox.ResumeLayout(false);
			this.recordGroupBox.PerformLayout();
			this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.ProgressBar progressBar1;
	private System.Windows.Forms.Button startButton;
	private System.Windows.Forms.Button pauseButton;
	private System.Windows.Forms.Button stopButton;
	private System.Windows.Forms.Button cancelButton;
	private System.Windows.Forms.Label statusLabel;
	private System.Windows.Forms.PictureBox recordingPictureBox;
	private System.Windows.Forms.GroupBox recordGroupBox;
}
