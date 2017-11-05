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

partial class InputSourceUserControl
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

	#region Component Designer generated code

	/// <summary> 
	/// Required method for Designer support - do not modify 
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
			this.switchInputButton = new System.Windows.Forms.Button();
			this.traceFileSelectorUserControl1 = new TraceFileSelectorUserControl();
			this.sessionSelectorUserControl1 = new SessionSelectorUserControl();
			this.SuspendLayout();
			// 
			// switchInputButton
			// 
			this.switchInputButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.switchInputButton.Location = new System.Drawing.Point(307, 252);
			this.switchInputButton.Name = "switchInputButton";
			this.switchInputButton.Size = new System.Drawing.Size(102, 24);
			this.switchInputButton.TabIndex = 2;
			this.switchInputButton.Text = "View Sessions";
			this.switchInputButton.UseVisualStyleBackColor = true;
			this.switchInputButton.Click += new System.EventHandler(this.SwitchInputButton_Click);
			// 
			// traceFileSelectorUserControl1
			// 
			this.traceFileSelectorUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.traceFileSelectorUserControl1.Location = new System.Drawing.Point(0, 0);
			this.traceFileSelectorUserControl1.Margin = new System.Windows.Forms.Padding(0);
			this.traceFileSelectorUserControl1.Name = "traceFileSelectorUserControl1";
			this.traceFileSelectorUserControl1.Size = new System.Drawing.Size(501, 283);
			this.traceFileSelectorUserControl1.TabIndex = 0;
			// 
			// sessionSelectorUserControl1
			// 
			this.sessionSelectorUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sessionSelectorUserControl1.Location = new System.Drawing.Point(0, 0);
			this.sessionSelectorUserControl1.Margin = new System.Windows.Forms.Padding(0);
			this.sessionSelectorUserControl1.MinimumSize = new System.Drawing.Size(501, 283);
			this.sessionSelectorUserControl1.Name = "sessionSelectorUserControl1";
			this.sessionSelectorUserControl1.Size = new System.Drawing.Size(501, 283);
			this.sessionSelectorUserControl1.TabIndex = 1;
			this.sessionSelectorUserControl1.TabStop = false;
			// 
			// InputSourceUserControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.switchInputButton);
			this.Controls.Add(this.traceFileSelectorUserControl1);
			this.Controls.Add(this.sessionSelectorUserControl1);
			this.DoubleBuffered = true;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "InputSourceUserControl";
			this.Size = new System.Drawing.Size(501, 283);
			this.ResumeLayout(false);

	}

	#endregion

	private TraceFileSelectorUserControl traceFileSelectorUserControl1;
	private SessionSelectorUserControl sessionSelectorUserControl1;
	private System.Windows.Forms.Button switchInputButton;


}
