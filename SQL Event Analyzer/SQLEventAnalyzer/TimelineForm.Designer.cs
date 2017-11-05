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

partial class TimelineForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimelineForm));
			this.idLabel = new System.Windows.Forms.Label();
			this.idValueLabel = new System.Windows.Forms.Label();
			this.durationValueLabel = new System.Windows.Forms.Label();
			this.durationLabel = new System.Windows.Forms.Label();
			this.relativeStartTimeValueLabel = new System.Windows.Forms.Label();
			this.relativeStartTimeLabel = new System.Windows.Forms.Label();
			this.readsValueLabel = new System.Windows.Forms.Label();
			this.writesValueLabel = new System.Windows.Forms.Label();
			this.cpuValueLabel = new System.Windows.Forms.Label();
			this.rowsValueLabel = new System.Windows.Forms.Label();
			this.readsLabel = new System.Windows.Forms.Label();
			this.writesLabel = new System.Windows.Forms.Label();
			this.cpuLabel = new System.Windows.Forms.Label();
			this.rowsLabel = new System.Windows.Forms.Label();
			this.textDataValueLabel = new System.Windows.Forms.Label();
			this.textDataLabel = new System.Windows.Forms.Label();
			this.startTimeValueLabel = new System.Windows.Forms.Label();
			this.startTimeLabel = new System.Windows.Forms.Label();
			this.zoomVerticalTrackBar = new System.Windows.Forms.TrackBar();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fitToScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewBarLabelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dataGroupBox = new System.Windows.Forms.GroupBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.viewTextDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zoomHorizontalTrackBar = new System.Windows.Forms.TrackBar();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.barLabelLabel = new System.Windows.Forms.Label();
			this.sortAlphabeticallyCheckBox = new System.Windows.Forms.CheckBox();
			this.showHiddenCheckBox = new System.Windows.Forms.CheckBox();
			this.barLabelsComboBox = new ComboBoxCustom();
			this.graphPanel = new CustomPanel();
			((System.ComponentModel.ISupportInitialize)(this.zoomVerticalTrackBar)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.dataGroupBox.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.zoomHorizontalTrackBar)).BeginInit();
			this.SuspendLayout();
			// 
			// idLabel
			// 
			this.idLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.idLabel.AutoSize = true;
			this.idLabel.Location = new System.Drawing.Point(6, 55);
			this.idLabel.Name = "idLabel";
			this.idLabel.Size = new System.Drawing.Size(19, 13);
			this.idLabel.TabIndex = 1;
			this.idLabel.Text = "Id:";
			// 
			// idValueLabel
			// 
			this.idValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.idValueLabel.Location = new System.Drawing.Point(190, 55);
			this.idValueLabel.Name = "idValueLabel";
			this.idValueLabel.Size = new System.Drawing.Size(124, 13);
			this.idValueLabel.TabIndex = 2;
			this.idValueLabel.Text = "value";
			// 
			// durationValueLabel
			// 
			this.durationValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.durationValueLabel.Location = new System.Drawing.Point(190, 68);
			this.durationValueLabel.Name = "durationValueLabel";
			this.durationValueLabel.Size = new System.Drawing.Size(124, 13);
			this.durationValueLabel.TabIndex = 4;
			this.durationValueLabel.Text = "value";
			// 
			// durationLabel
			// 
			this.durationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.durationLabel.AutoSize = true;
			this.durationLabel.Location = new System.Drawing.Point(6, 68);
			this.durationLabel.Name = "durationLabel";
			this.durationLabel.Size = new System.Drawing.Size(50, 13);
			this.durationLabel.TabIndex = 3;
			this.durationLabel.Text = "Duration:";
			// 
			// relativeStartTimeValueLabel
			// 
			this.relativeStartTimeValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.relativeStartTimeValueLabel.Location = new System.Drawing.Point(190, 29);
			this.relativeStartTimeValueLabel.Name = "relativeStartTimeValueLabel";
			this.relativeStartTimeValueLabel.Size = new System.Drawing.Size(664, 13);
			this.relativeStartTimeValueLabel.TabIndex = 6;
			this.relativeStartTimeValueLabel.Text = "value";
			// 
			// relativeStartTimeLabel
			// 
			this.relativeStartTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.relativeStartTimeLabel.AutoSize = true;
			this.relativeStartTimeLabel.Location = new System.Drawing.Point(6, 29);
			this.relativeStartTimeLabel.Name = "relativeStartTimeLabel";
			this.relativeStartTimeLabel.Size = new System.Drawing.Size(153, 13);
			this.relativeStartTimeLabel.TabIndex = 5;
			this.relativeStartTimeLabel.Text = "StartTime relative to first event:";
			// 
			// readsValueLabel
			// 
			this.readsValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.readsValueLabel.Location = new System.Drawing.Point(190, 81);
			this.readsValueLabel.Name = "readsValueLabel";
			this.readsValueLabel.Size = new System.Drawing.Size(124, 13);
			this.readsValueLabel.TabIndex = 8;
			this.readsValueLabel.Text = "value";
			// 
			// writesValueLabel
			// 
			this.writesValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.writesValueLabel.Location = new System.Drawing.Point(357, 55);
			this.writesValueLabel.Name = "writesValueLabel";
			this.writesValueLabel.Size = new System.Drawing.Size(124, 13);
			this.writesValueLabel.TabIndex = 8;
			this.writesValueLabel.Text = "value";
			// 
			// cpuValueLabel
			// 
			this.cpuValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cpuValueLabel.Location = new System.Drawing.Point(357, 68);
			this.cpuValueLabel.Name = "cpuValueLabel";
			this.cpuValueLabel.Size = new System.Drawing.Size(124, 13);
			this.cpuValueLabel.TabIndex = 8;
			this.cpuValueLabel.Text = "value";
			// 
			// rowsValueLabel
			// 
			this.rowsValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.rowsValueLabel.Location = new System.Drawing.Point(357, 81);
			this.rowsValueLabel.Name = "rowsValueLabel";
			this.rowsValueLabel.Size = new System.Drawing.Size(124, 13);
			this.rowsValueLabel.TabIndex = 8;
			this.rowsValueLabel.Text = "value";
			// 
			// readsLabel
			// 
			this.readsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.readsLabel.AutoSize = true;
			this.readsLabel.Location = new System.Drawing.Point(6, 81);
			this.readsLabel.Name = "readsLabel";
			this.readsLabel.Size = new System.Drawing.Size(41, 13);
			this.readsLabel.TabIndex = 7;
			this.readsLabel.Text = "Reads:";
			// 
			// writesLabel
			// 
			this.writesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.writesLabel.AutoSize = true;
			this.writesLabel.Location = new System.Drawing.Point(316, 55);
			this.writesLabel.Name = "writesLabel";
			this.writesLabel.Size = new System.Drawing.Size(40, 13);
			this.writesLabel.TabIndex = 7;
			this.writesLabel.Text = "Writes:";
			// 
			// cpuLabel
			// 
			this.cpuLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cpuLabel.AutoSize = true;
			this.cpuLabel.Location = new System.Drawing.Point(316, 68);
			this.cpuLabel.Name = "cpuLabel";
			this.cpuLabel.Size = new System.Drawing.Size(32, 13);
			this.cpuLabel.TabIndex = 7;
			this.cpuLabel.Text = "CPU:";
			// 
			// rowsLabel
			// 
			this.rowsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.rowsLabel.AutoSize = true;
			this.rowsLabel.Location = new System.Drawing.Point(316, 81);
			this.rowsLabel.Name = "rowsLabel";
			this.rowsLabel.Size = new System.Drawing.Size(37, 13);
			this.rowsLabel.TabIndex = 7;
			this.rowsLabel.Text = "Rows:";
			// 
			// textDataValueLabel
			// 
			this.textDataValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textDataValueLabel.Location = new System.Drawing.Point(190, 16);
			this.textDataValueLabel.Name = "textDataValueLabel";
			this.textDataValueLabel.Size = new System.Drawing.Size(664, 13);
			this.textDataValueLabel.TabIndex = 14;
			this.textDataValueLabel.Text = "value";
			// 
			// textDataLabel
			// 
			this.textDataLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textDataLabel.AutoSize = true;
			this.textDataLabel.Location = new System.Drawing.Point(6, 16);
			this.textDataLabel.Name = "textDataLabel";
			this.textDataLabel.Size = new System.Drawing.Size(54, 13);
			this.textDataLabel.TabIndex = 13;
			this.textDataLabel.Text = "TextData:";
			// 
			// startTimeValueLabel
			// 
			this.startTimeValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.startTimeValueLabel.Location = new System.Drawing.Point(190, 42);
			this.startTimeValueLabel.Name = "startTimeValueLabel";
			this.startTimeValueLabel.Size = new System.Drawing.Size(664, 13);
			this.startTimeValueLabel.TabIndex = 16;
			this.startTimeValueLabel.Text = "value";
			// 
			// startTimeLabel
			// 
			this.startTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.startTimeLabel.AutoSize = true;
			this.startTimeLabel.Location = new System.Drawing.Point(6, 42);
			this.startTimeLabel.Name = "startTimeLabel";
			this.startTimeLabel.Size = new System.Drawing.Size(55, 13);
			this.startTimeLabel.TabIndex = 15;
			this.startTimeLabel.Text = "StartTime:";
			// 
			// zoomVerticalTrackBar
			// 
			this.zoomVerticalTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.zoomVerticalTrackBar.LargeChange = 1;
			this.zoomVerticalTrackBar.Location = new System.Drawing.Point(12, 343);
			this.zoomVerticalTrackBar.Maximum = 7;
			this.zoomVerticalTrackBar.Minimum = 1;
			this.zoomVerticalTrackBar.Name = "zoomVerticalTrackBar";
			this.zoomVerticalTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.zoomVerticalTrackBar.Size = new System.Drawing.Size(45, 148);
			this.zoomVerticalTrackBar.TabIndex = 17;
			this.zoomVerticalTrackBar.TabStop = false;
			this.zoomVerticalTrackBar.Value = 7;
			this.zoomVerticalTrackBar.Scroll += new System.EventHandler(this.ZoomVerticalTrackBar_Scroll);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.menuStrip1.Size = new System.Drawing.Size(884, 24);
			this.menuStrip1.TabIndex = 18;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.disk;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.saveToolStripMenuItem.Text = "&Save...";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(144, 6);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.closeToolStripMenuItem.Text = "&Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fitToScreenToolStripMenuItem,
            this.viewBarLabelsToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "&View";
			// 
			// fitToScreenToolStripMenuItem
			// 
			this.fitToScreenToolStripMenuItem.Name = "fitToScreenToolStripMenuItem";
			this.fitToScreenToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.fitToScreenToolStripMenuItem.Text = "&Fit to window";
			this.fitToScreenToolStripMenuItem.Click += new System.EventHandler(this.FitToScreenToolStripMenuItem_Click);
			// 
			// viewBarLabelsToolStripMenuItem
			// 
			this.viewBarLabelsToolStripMenuItem.Checked = true;
			this.viewBarLabelsToolStripMenuItem.CheckOnClick = true;
			this.viewBarLabelsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.viewBarLabelsToolStripMenuItem.Name = "viewBarLabelsToolStripMenuItem";
			this.viewBarLabelsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.viewBarLabelsToolStripMenuItem.Text = "&Bar labels";
			this.viewBarLabelsToolStripMenuItem.Click += new System.EventHandler(this.ViewBarLabelsToolStripMenuItem_Click);
			// 
			// dataGroupBox
			// 
			this.dataGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGroupBox.Controls.Add(this.idLabel);
			this.dataGroupBox.Controls.Add(this.idValueLabel);
			this.dataGroupBox.Controls.Add(this.durationLabel);
			this.dataGroupBox.Controls.Add(this.startTimeValueLabel);
			this.dataGroupBox.Controls.Add(this.durationValueLabel);
			this.dataGroupBox.Controls.Add(this.startTimeLabel);
			this.dataGroupBox.Controls.Add(this.relativeStartTimeLabel);
			this.dataGroupBox.Controls.Add(this.textDataValueLabel);
			this.dataGroupBox.Controls.Add(this.relativeStartTimeValueLabel);
			this.dataGroupBox.Controls.Add(this.textDataLabel);
			this.dataGroupBox.Controls.Add(this.readsLabel);
			this.dataGroupBox.Controls.Add(this.readsValueLabel);
			this.dataGroupBox.Controls.Add(this.writesLabel);
			this.dataGroupBox.Controls.Add(this.writesValueLabel);
			this.dataGroupBox.Controls.Add(this.cpuLabel);
			this.dataGroupBox.Controls.Add(this.cpuValueLabel);
			this.dataGroupBox.Controls.Add(this.rowsLabel);
			this.dataGroupBox.Controls.Add(this.rowsValueLabel);
			this.dataGroupBox.Location = new System.Drawing.Point(12, 531);
			this.dataGroupBox.Name = "dataGroupBox";
			this.dataGroupBox.Size = new System.Drawing.Size(860, 98);
			this.dataGroupBox.TabIndex = 19;
			this.dataGroupBox.TabStop = false;
			this.dataGroupBox.Text = "Data (Move mouse over a row to see data)";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewTextDataToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(166, 48);
			// 
			// viewTextDataToolStripMenuItem
			// 
			this.viewTextDataToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.viewTextDataToolStripMenuItem.Name = "viewTextDataToolStripMenuItem";
			this.viewTextDataToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.viewTextDataToolStripMenuItem.Text = "View TextData...";
			this.viewTextDataToolStripMenuItem.Click += new System.EventHandler(this.ViewTextDataToolStripMenuItem_Click);
			// 
			// zoomHorizontalTrackBar
			// 
			this.zoomHorizontalTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.zoomHorizontalTrackBar.LargeChange = 1;
			this.zoomHorizontalTrackBar.Location = new System.Drawing.Point(48, 497);
			this.zoomHorizontalTrackBar.Maximum = 5;
			this.zoomHorizontalTrackBar.Minimum = 1;
			this.zoomHorizontalTrackBar.Name = "zoomHorizontalTrackBar";
			this.zoomHorizontalTrackBar.Size = new System.Drawing.Size(148, 45);
			this.zoomHorizontalTrackBar.TabIndex = 20;
			this.zoomHorizontalTrackBar.TabStop = false;
			this.zoomHorizontalTrackBar.Value = 1;
			this.zoomHorizontalTrackBar.Scroll += new System.EventHandler(this.ZoomHorizontalTrackBar_Scroll);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "Png";
			this.saveFileDialog1.Filter = "Png|*.png|Bmp|*.bmp|Tiff|*.tiff";
			// 
			// barLabelLabel
			// 
			this.barLabelLabel.AutoSize = true;
			this.barLabelLabel.Location = new System.Drawing.Point(12, 30);
			this.barLabelLabel.Name = "barLabelLabel";
			this.barLabelLabel.Size = new System.Drawing.Size(51, 13);
			this.barLabelLabel.TabIndex = 23;
			this.barLabelLabel.Text = "Bar label:";
			// 
			// sortAlphabeticallyCheckBox
			// 
			this.sortAlphabeticallyCheckBox.AutoSize = true;
			this.sortAlphabeticallyCheckBox.Location = new System.Drawing.Point(371, 29);
			this.sortAlphabeticallyCheckBox.Name = "sortAlphabeticallyCheckBox";
			this.sortAlphabeticallyCheckBox.Size = new System.Drawing.Size(141, 17);
			this.sortAlphabeticallyCheckBox.TabIndex = 3;
			this.sortAlphabeticallyCheckBox.Text = "Sort name alphabetically";
			this.sortAlphabeticallyCheckBox.UseVisualStyleBackColor = true;
			this.sortAlphabeticallyCheckBox.CheckedChanged += new System.EventHandler(this.SortAlphabeticallyCheckBox_CheckedChanged);
			// 
			// showHiddenCheckBox
			// 
			this.showHiddenCheckBox.AutoSize = true;
			this.showHiddenCheckBox.Location = new System.Drawing.Point(275, 29);
			this.showHiddenCheckBox.Name = "showHiddenCheckBox";
			this.showHiddenCheckBox.Size = new System.Drawing.Size(90, 17);
			this.showHiddenCheckBox.TabIndex = 2;
			this.showHiddenCheckBox.Text = "Show Hidden";
			this.showHiddenCheckBox.UseVisualStyleBackColor = true;
			this.showHiddenCheckBox.CheckedChanged += new System.EventHandler(this.ShowHiddenCheckBox_CheckedChanged);
			// 
			// barLabelsComboBox
			// 
			this.barLabelsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.barLabelsComboBox.FormattingEnabled = true;
			this.barLabelsComboBox.Location = new System.Drawing.Point(69, 27);
			this.barLabelsComboBox.Name = "barLabelsComboBox";
			this.barLabelsComboBox.Size = new System.Drawing.Size(200, 21);
			this.barLabelsComboBox.TabIndex = 1;
			// 
			// graphPanel
			// 
			this.graphPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.graphPanel.AutoScroll = true;
			this.graphPanel.BackColor = System.Drawing.Color.White;
			this.graphPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.graphPanel.Location = new System.Drawing.Point(48, 54);
			this.graphPanel.Name = "graphPanel";
			this.graphPanel.Size = new System.Drawing.Size(824, 437);
			this.graphPanel.TabIndex = 0;
			this.graphPanel.Click += new System.EventHandler(this.GraphPanel_Click);
			// 
			// TimelineForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(884, 641);
			this.Controls.Add(this.sortAlphabeticallyCheckBox);
			this.Controls.Add(this.showHiddenCheckBox);
			this.Controls.Add(this.barLabelLabel);
			this.Controls.Add(this.barLabelsComboBox);
			this.Controls.Add(this.dataGroupBox);
			this.Controls.Add(this.zoomHorizontalTrackBar);
			this.Controls.Add(this.graphPanel);
			this.Controls.Add(this.zoomVerticalTrackBar);
			this.Controls.Add(this.menuStrip1);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "TimelineForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Title";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TimelineForm_FormClosing);
			this.Resize += new System.EventHandler(this.TimelineForm_Resize);
			((System.ComponentModel.ISupportInitialize)(this.zoomVerticalTrackBar)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.dataGroupBox.ResumeLayout(false);
			this.dataGroupBox.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.zoomHorizontalTrackBar)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private CustomPanel graphPanel;
	private System.Windows.Forms.Label idLabel;
	private System.Windows.Forms.Label idValueLabel;
	private System.Windows.Forms.Label durationValueLabel;
	private System.Windows.Forms.Label durationLabel;
	private System.Windows.Forms.Label relativeStartTimeValueLabel;
	private System.Windows.Forms.Label relativeStartTimeLabel;
	private System.Windows.Forms.Label readsValueLabel;
	private System.Windows.Forms.Label readsLabel;
	private System.Windows.Forms.Label writesValueLabel;
	private System.Windows.Forms.Label writesLabel;
	private System.Windows.Forms.Label cpuValueLabel;
	private System.Windows.Forms.Label cpuLabel;
	private System.Windows.Forms.Label rowsValueLabel;
	private System.Windows.Forms.Label rowsLabel;
	private System.Windows.Forms.Label textDataValueLabel;
	private System.Windows.Forms.Label textDataLabel;
	private System.Windows.Forms.Label startTimeValueLabel;
	private System.Windows.Forms.Label startTimeLabel;
	private System.Windows.Forms.TrackBar zoomVerticalTrackBar;
	private System.Windows.Forms.MenuStrip menuStrip1;
	private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem viewBarLabelsToolStripMenuItem;
	private System.Windows.Forms.GroupBox dataGroupBox;
	private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
	private System.Windows.Forms.ToolStripMenuItem viewTextDataToolStripMenuItem;
	private System.Windows.Forms.TrackBar zoomHorizontalTrackBar;
	private System.Windows.Forms.ToolStripMenuItem fitToScreenToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	private System.Windows.Forms.SaveFileDialog saveFileDialog1;
	private ComboBoxCustom barLabelsComboBox;
	private System.Windows.Forms.Label barLabelLabel;
	private System.Windows.Forms.CheckBox sortAlphabeticallyCheckBox;
	private System.Windows.Forms.CheckBox showHiddenCheckBox;

}
