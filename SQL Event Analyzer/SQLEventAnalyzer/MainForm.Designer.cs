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

partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.changeConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.customColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.recordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.standardColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.typeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sPIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.durationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.readsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.writesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cpuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showHiddenColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.timelineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.commandLineParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkForupdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dataViewUserControl1 = new DataViewUserControl();
			this.inputSourceUserControl1 = new InputSourceUserControl();
			this.restartToUpdateTextBox = new System.Windows.Forms.TextBox();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.menuStrip1.Size = new System.Drawing.Size(884, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeConnectionToolStripMenuItem,
            this.toolStripSeparator3,
            this.exportToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// changeConnectionToolStripMenuItem
			// 
			this.changeConnectionToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.database_key_small;
			this.changeConnectionToolStripMenuItem.Name = "changeConnectionToolStripMenuItem";
			this.changeConnectionToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.changeConnectionToolStripMenuItem.Text = "&Change Connection...";
			this.changeConnectionToolStripMenuItem.Click += new System.EventHandler(this.ChangeConnectionToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(186, 6);
			// 
			// exportToolStripMenuItem
			// 
			this.exportToolStripMenuItem.Enabled = false;
			this.exportToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.table_export_small;
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.exportToolStripMenuItem.Text = "&Export...";
			this.exportToolStripMenuItem.Click += new System.EventHandler(this.ExportToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customColumnsToolStripMenuItem,
            this.recordToolStripMenuItem,
            this.toolStripSeparator2,
            this.optionsToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
			this.toolsToolStripMenuItem.Text = "&Tools";
			// 
			// customColumnsToolStripMenuItem
			// 
			this.customColumnsToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.report_edit_small;
			this.customColumnsToolStripMenuItem.Name = "customColumnsToolStripMenuItem";
			this.customColumnsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D1)));
			this.customColumnsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
			this.customColumnsToolStripMenuItem.Text = "&Custom Columns...";
			this.customColumnsToolStripMenuItem.Click += new System.EventHandler(this.CustomColumnsToolStripMenuItem_Click);
			// 
			// recordToolStripMenuItem
			// 
			this.recordToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.stop_small;
			this.recordToolStripMenuItem.Name = "recordToolStripMenuItem";
			this.recordToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D2)));
			this.recordToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
			this.recordToolStripMenuItem.Text = "&Record...";
			this.recordToolStripMenuItem.Click += new System.EventHandler(this.RecordToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(209, 6);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.cog_small;
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
			this.optionsToolStripMenuItem.Text = "&Options...";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItem_Click);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.standardColumnsToolStripMenuItem,
            this.showHiddenColumnsToolStripMenuItem,
            this.toolStripSeparator4,
            this.timelineToolStripMenuItem,
            this.toolStripSeparator5,
            this.refreshToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "&View";
			// 
			// standardColumnsToolStripMenuItem
			// 
			this.standardColumnsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileNameToolStripMenuItem,
            this.typeToolStripMenuItem,
            this.sPIDToolStripMenuItem,
            this.durationToolStripMenuItem,
            this.readsToolStripMenuItem,
            this.writesToolStripMenuItem,
            this.cpuToolStripMenuItem,
            this.rowsToolStripMenuItem});
			this.standardColumnsToolStripMenuItem.Enabled = false;
			this.standardColumnsToolStripMenuItem.Name = "standardColumnsToolStripMenuItem";
			this.standardColumnsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.standardColumnsToolStripMenuItem.Text = "Standard Columns";
			// 
			// fileNameToolStripMenuItem
			// 
			this.fileNameToolStripMenuItem.CheckOnClick = true;
			this.fileNameToolStripMenuItem.Name = "fileNameToolStripMenuItem";
			this.fileNameToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.fileNameToolStripMenuItem.Text = "FileName";
			this.fileNameToolStripMenuItem.Click += new System.EventHandler(this.FileNameToolStripMenuItem_Click);
			// 
			// typeToolStripMenuItem
			// 
			this.typeToolStripMenuItem.CheckOnClick = true;
			this.typeToolStripMenuItem.Name = "typeToolStripMenuItem";
			this.typeToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.typeToolStripMenuItem.Text = "Type";
			this.typeToolStripMenuItem.Click += new System.EventHandler(this.TypeToolStripMenuItem_Click);
			// 
			// sPIDToolStripMenuItem
			// 
			this.sPIDToolStripMenuItem.CheckOnClick = true;
			this.sPIDToolStripMenuItem.Name = "sPIDToolStripMenuItem";
			this.sPIDToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.sPIDToolStripMenuItem.Text = "SPID";
			this.sPIDToolStripMenuItem.Click += new System.EventHandler(this.SPIDToolStripMenuItem_Click);
			// 
			// durationToolStripMenuItem
			// 
			this.durationToolStripMenuItem.Checked = true;
			this.durationToolStripMenuItem.CheckOnClick = true;
			this.durationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.durationToolStripMenuItem.Name = "durationToolStripMenuItem";
			this.durationToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.durationToolStripMenuItem.Text = "Duration";
			this.durationToolStripMenuItem.Click += new System.EventHandler(this.DurationToolStripMenuItem_Click);
			// 
			// readsToolStripMenuItem
			// 
			this.readsToolStripMenuItem.Checked = true;
			this.readsToolStripMenuItem.CheckOnClick = true;
			this.readsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.readsToolStripMenuItem.Name = "readsToolStripMenuItem";
			this.readsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.readsToolStripMenuItem.Text = "Reads";
			this.readsToolStripMenuItem.Click += new System.EventHandler(this.ReadsToolStripMenuItem_Click);
			// 
			// writesToolStripMenuItem
			// 
			this.writesToolStripMenuItem.Checked = true;
			this.writesToolStripMenuItem.CheckOnClick = true;
			this.writesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.writesToolStripMenuItem.Name = "writesToolStripMenuItem";
			this.writesToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.writesToolStripMenuItem.Text = "Writes";
			this.writesToolStripMenuItem.Click += new System.EventHandler(this.WritesToolStripMenuItem_Click);
			// 
			// cpuToolStripMenuItem
			// 
			this.cpuToolStripMenuItem.Checked = true;
			this.cpuToolStripMenuItem.CheckOnClick = true;
			this.cpuToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cpuToolStripMenuItem.Name = "cpuToolStripMenuItem";
			this.cpuToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.cpuToolStripMenuItem.Text = "CPU";
			this.cpuToolStripMenuItem.Click += new System.EventHandler(this.CpuToolStripMenuItem_Click);
			// 
			// rowsToolStripMenuItem
			// 
			this.rowsToolStripMenuItem.Checked = true;
			this.rowsToolStripMenuItem.CheckOnClick = true;
			this.rowsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.rowsToolStripMenuItem.Name = "rowsToolStripMenuItem";
			this.rowsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.rowsToolStripMenuItem.Text = "Rows";
			this.rowsToolStripMenuItem.Click += new System.EventHandler(this.RowsToolStripMenuItem_Click);
			// 
			// showHiddenColumnsToolStripMenuItem
			// 
			this.showHiddenColumnsToolStripMenuItem.CheckOnClick = true;
			this.showHiddenColumnsToolStripMenuItem.Enabled = false;
			this.showHiddenColumnsToolStripMenuItem.Name = "showHiddenColumnsToolStripMenuItem";
			this.showHiddenColumnsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.showHiddenColumnsToolStripMenuItem.Text = "Show &Hidden Columns";
			this.showHiddenColumnsToolStripMenuItem.Click += new System.EventHandler(this.ShowHiddenColumnsToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(193, 6);
			// 
			// timelineToolStripMenuItem
			// 
			this.timelineToolStripMenuItem.Enabled = false;
			this.timelineToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.clock_small;
			this.timelineToolStripMenuItem.Name = "timelineToolStripMenuItem";
			this.timelineToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.timelineToolStripMenuItem.Text = "&Timeline...";
			this.timelineToolStripMenuItem.Click += new System.EventHandler(this.TimelineToolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(193, 6);
			// 
			// refreshToolStripMenuItem
			// 
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.ShortcutKeyDisplayString = "F5";
			this.refreshToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.refreshToolStripMenuItem.Text = "Refresh";
			this.refreshToolStripMenuItem.Click += new System.EventHandler(this.RefreshToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.commandLineParametersToolStripMenuItem,
            this.checkForupdatesToolStripMenuItem,
            this.toolStripSeparator6,
            this.aboutToolStripMenuItem,
            this.infoToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// helpToolStripMenuItem1
			// 
			this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
			this.helpToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.helpToolStripMenuItem1.Size = new System.Drawing.Size(227, 22);
			this.helpToolStripMenuItem1.Text = "&Help...";
			this.helpToolStripMenuItem1.Click += new System.EventHandler(this.HelpToolStripMenuItem1_Click);
			// 
			// commandLineParametersToolStripMenuItem
			// 
			this.commandLineParametersToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.cmd_small;
			this.commandLineParametersToolStripMenuItem.Name = "commandLineParametersToolStripMenuItem";
			this.commandLineParametersToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
			this.commandLineParametersToolStripMenuItem.Text = "&Command Line Parameters...";
			this.commandLineParametersToolStripMenuItem.Click += new System.EventHandler(this.CommandLineParametersToolStripMenuItem_Click);
			// 
			// checkForupdatesToolStripMenuItem
			// 
			this.checkForupdatesToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.update;
			this.checkForupdatesToolStripMenuItem.Name = "checkForupdatesToolStripMenuItem";
			this.checkForupdatesToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
			this.checkForupdatesToolStripMenuItem.Text = "Check for &updates...";
			this.checkForupdatesToolStripMenuItem.Click += new System.EventHandler(this.CheckForupdatesToolStripMenuItem_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(224, 6);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
			this.aboutToolStripMenuItem.Text = "&About...";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
			// 
			// infoToolStripMenuItem
			// 
			this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
			this.infoToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
			this.infoToolStripMenuItem.Text = "&Info...";
			this.infoToolStripMenuItem.Click += new System.EventHandler(this.InfoToolStripMenuItem_Click);
			// 
			// dataViewUserControl1
			// 
			this.dataViewUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataViewUserControl1.Location = new System.Drawing.Point(0, 24);
			this.dataViewUserControl1.Name = "dataViewUserControl1";
			this.dataViewUserControl1.Size = new System.Drawing.Size(884, 617);
			this.dataViewUserControl1.TabIndex = 2;
			this.dataViewUserControl1.Visible = false;
			// 
			// inputSourceUserControl1
			// 
			this.inputSourceUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inputSourceUserControl1.Location = new System.Drawing.Point(0, 24);
			this.inputSourceUserControl1.Margin = new System.Windows.Forms.Padding(0);
			this.inputSourceUserControl1.MinimumSize = new System.Drawing.Size(501, 283);
			this.inputSourceUserControl1.Name = "inputSourceUserControl1";
			this.inputSourceUserControl1.Size = new System.Drawing.Size(884, 617);
			this.inputSourceUserControl1.TabIndex = 0;
			// 
			// restartToUpdateTextBox
			// 
			this.restartToUpdateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.restartToUpdateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.restartToUpdateTextBox.Cursor = System.Windows.Forms.Cursors.Default;
			this.restartToUpdateTextBox.Location = new System.Drawing.Point(421, 5);
			this.restartToUpdateTextBox.Name = "restartToUpdateTextBox";
			this.restartToUpdateTextBox.ReadOnly = true;
			this.restartToUpdateTextBox.Size = new System.Drawing.Size(453, 13);
			this.restartToUpdateTextBox.TabIndex = 9;
			this.restartToUpdateTextBox.TabStop = false;
			this.restartToUpdateTextBox.Text = "Custom Columns updated. Restart application to apply.";
			this.restartToUpdateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.restartToUpdateTextBox.Visible = false;
			this.restartToUpdateTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RestartToUpdateTextBox_MouseDown);
			// 
			// MainForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(884, 641);
			this.Controls.Add(this.dataViewUserControl1);
			this.Controls.Add(this.inputSourceUserControl1);
			this.Controls.Add(this.restartToUpdateTextBox);
			this.Controls.Add(this.menuStrip1);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(900, 680);
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MainForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.MenuStrip menuStrip1;
	private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
	private DataViewUserControl dataViewUserControl1;
	private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem showHiddenColumnsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem changeConnectionToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	private System.Windows.Forms.ToolStripMenuItem customColumnsToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
	private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem standardColumnsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem fileNameToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem typeToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem sPIDToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem durationToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem readsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem writesToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem cpuToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem rowsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem recordToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem commandLineParametersToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem timelineToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
	private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
	private InputSourceUserControl inputSourceUserControl1;
	private System.Windows.Forms.ToolStripMenuItem checkForupdatesToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
	private System.Windows.Forms.TextBox restartToUpdateTextBox;
	private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
}
