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

partial class ViewStatisticsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewStatisticsForm));
			this.okButton = new System.Windows.Forms.Button();
			this.statisticsGroupBox = new System.Windows.Forms.GroupBox();
			this.dataViewer1 = new DataViewer();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.columnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.minDurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.maxDurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.avgDurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.devDurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.varDurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sumDurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.minReadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.maxReadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.avgReadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.devReadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.varReadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sumReadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.minWritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.maxWritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.avgWritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.devWritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.varWritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sumWritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.minCpuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.maxCpuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.avgCpuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.devCpuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.varCpuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sumCpuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.minRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.maxRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.avgRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.devRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.varRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sumRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filterLabel = new System.Windows.Forms.Label();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.viewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.statisticsGroupBox.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.okButton.Location = new System.Drawing.Point(798, 610);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 24);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// statisticsGroupBox
			// 
			this.statisticsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.statisticsGroupBox.Controls.Add(this.dataViewer1);
			this.statisticsGroupBox.Location = new System.Drawing.Point(12, 27);
			this.statisticsGroupBox.Name = "statisticsGroupBox";
			this.statisticsGroupBox.Size = new System.Drawing.Size(860, 575);
			this.statisticsGroupBox.TabIndex = 0;
			this.statisticsGroupBox.TabStop = false;
			this.statisticsGroupBox.Text = "Statistics";
			// 
			// dataViewer1
			// 
			this.dataViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataViewer1.Location = new System.Drawing.Point(6, 19);
			this.dataViewer1.MinimumSize = new System.Drawing.Size(367, 182);
			this.dataViewer1.Name = "dataViewer1";
			this.dataViewer1.Size = new System.Drawing.Size(848, 550);
			this.dataViewer1.TabIndex = 0;
			this.dataViewer1.Enter += new System.EventHandler(this.DataViewer1_Enter);
			this.dataViewer1.Leave += new System.EventHandler(this.DataViewer1_Leave);
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
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// exportToolStripMenuItem
			// 
			this.exportToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.table_export_small;
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.exportToolStripMenuItem.Text = "&Export...";
			this.exportToolStripMenuItem.Click += new System.EventHandler(this.ExportToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.closeToolStripMenuItem.Text = "&Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.columnsToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "&View";
			// 
			// columnsToolStripMenuItem
			// 
			this.columnsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minDurationToolStripMenuItem,
            this.maxDurationToolStripMenuItem,
            this.avgDurationToolStripMenuItem,
            this.devDurationToolStripMenuItem,
            this.varDurationToolStripMenuItem,
            this.sumDurationToolStripMenuItem,
            this.minReadsToolStripMenuItem,
            this.maxReadsToolStripMenuItem,
            this.avgReadsToolStripMenuItem,
            this.devReadsToolStripMenuItem,
            this.varReadsToolStripMenuItem,
            this.sumReadsToolStripMenuItem,
            this.minWritesToolStripMenuItem,
            this.maxWritesToolStripMenuItem,
            this.avgWritesToolStripMenuItem,
            this.devWritesToolStripMenuItem,
            this.varWritesToolStripMenuItem,
            this.sumWritesToolStripMenuItem,
            this.minCpuToolStripMenuItem,
            this.maxCpuToolStripMenuItem,
            this.avgCpuToolStripMenuItem,
            this.devCpuToolStripMenuItem,
            this.varCpuToolStripMenuItem,
            this.sumCpuToolStripMenuItem,
            this.minRowsToolStripMenuItem,
            this.maxRowsToolStripMenuItem,
            this.avgRowsToolStripMenuItem,
            this.devRowsToolStripMenuItem,
            this.varRowsToolStripMenuItem,
            this.sumRowsToolStripMenuItem});
			this.columnsToolStripMenuItem.Name = "columnsToolStripMenuItem";
			this.columnsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.columnsToolStripMenuItem.Text = "Columns";
			// 
			// minDurationToolStripMenuItem
			// 
			this.minDurationToolStripMenuItem.Checked = true;
			this.minDurationToolStripMenuItem.CheckOnClick = true;
			this.minDurationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.minDurationToolStripMenuItem.Name = "minDurationToolStripMenuItem";
			this.minDurationToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.minDurationToolStripMenuItem.Text = "MinDuration";
			this.minDurationToolStripMenuItem.Click += new System.EventHandler(this.MinDurationToolStripMenuItem_Click);
			// 
			// maxDurationToolStripMenuItem
			// 
			this.maxDurationToolStripMenuItem.Checked = true;
			this.maxDurationToolStripMenuItem.CheckOnClick = true;
			this.maxDurationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.maxDurationToolStripMenuItem.Name = "maxDurationToolStripMenuItem";
			this.maxDurationToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.maxDurationToolStripMenuItem.Text = "MaxDuration";
			this.maxDurationToolStripMenuItem.Click += new System.EventHandler(this.MaxDurationToolStripMenuItem_Click);
			// 
			// avgDurationToolStripMenuItem
			// 
			this.avgDurationToolStripMenuItem.Checked = true;
			this.avgDurationToolStripMenuItem.CheckOnClick = true;
			this.avgDurationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.avgDurationToolStripMenuItem.Name = "avgDurationToolStripMenuItem";
			this.avgDurationToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.avgDurationToolStripMenuItem.Text = "AvgDuration";
			this.avgDurationToolStripMenuItem.Click += new System.EventHandler(this.AvgDurationToolStripMenuItem_Click);
			// 
			// devDurationToolStripMenuItem
			// 
			this.devDurationToolStripMenuItem.CheckOnClick = true;
			this.devDurationToolStripMenuItem.Name = "devDurationToolStripMenuItem";
			this.devDurationToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.devDurationToolStripMenuItem.Text = "DevDuration";
			this.devDurationToolStripMenuItem.Click += new System.EventHandler(this.DevDurationToolStripMenuItem_Click);
			// 
			// varDurationToolStripMenuItem
			// 
			this.varDurationToolStripMenuItem.CheckOnClick = true;
			this.varDurationToolStripMenuItem.Name = "varDurationToolStripMenuItem";
			this.varDurationToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.varDurationToolStripMenuItem.Text = "VarDuration";
			this.varDurationToolStripMenuItem.Click += new System.EventHandler(this.VarDurationToolStripMenuItem_Click);
			// 
			// sumDurationToolStripMenuItem
			// 
			this.sumDurationToolStripMenuItem.CheckOnClick = true;
			this.sumDurationToolStripMenuItem.Name = "sumDurationToolStripMenuItem";
			this.sumDurationToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.sumDurationToolStripMenuItem.Text = "SumDuration";
			this.sumDurationToolStripMenuItem.Click += new System.EventHandler(this.SumDurationToolStripMenuItem_Click);
			// 
			// minReadsToolStripMenuItem
			// 
			this.minReadsToolStripMenuItem.Checked = true;
			this.minReadsToolStripMenuItem.CheckOnClick = true;
			this.minReadsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.minReadsToolStripMenuItem.Name = "minReadsToolStripMenuItem";
			this.minReadsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.minReadsToolStripMenuItem.Text = "MinReads";
			this.minReadsToolStripMenuItem.Click += new System.EventHandler(this.MinReadsToolStripMenuItem_Click);
			// 
			// maxReadsToolStripMenuItem
			// 
			this.maxReadsToolStripMenuItem.Checked = true;
			this.maxReadsToolStripMenuItem.CheckOnClick = true;
			this.maxReadsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.maxReadsToolStripMenuItem.Name = "maxReadsToolStripMenuItem";
			this.maxReadsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.maxReadsToolStripMenuItem.Text = "MaxReads";
			this.maxReadsToolStripMenuItem.Click += new System.EventHandler(this.MaxReadsToolStripMenuItem_Click);
			// 
			// avgReadsToolStripMenuItem
			// 
			this.avgReadsToolStripMenuItem.Checked = true;
			this.avgReadsToolStripMenuItem.CheckOnClick = true;
			this.avgReadsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.avgReadsToolStripMenuItem.Name = "avgReadsToolStripMenuItem";
			this.avgReadsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.avgReadsToolStripMenuItem.Text = "AvgReads";
			this.avgReadsToolStripMenuItem.Click += new System.EventHandler(this.AvgReadsToolStripMenuItem_Click);
			// 
			// devReadsToolStripMenuItem
			// 
			this.devReadsToolStripMenuItem.CheckOnClick = true;
			this.devReadsToolStripMenuItem.Name = "devReadsToolStripMenuItem";
			this.devReadsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.devReadsToolStripMenuItem.Text = "DevReads";
			this.devReadsToolStripMenuItem.Click += new System.EventHandler(this.DevReadsToolStripMenuItem_Click);
			// 
			// varReadsToolStripMenuItem
			// 
			this.varReadsToolStripMenuItem.CheckOnClick = true;
			this.varReadsToolStripMenuItem.Name = "varReadsToolStripMenuItem";
			this.varReadsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.varReadsToolStripMenuItem.Text = "VarReads";
			this.varReadsToolStripMenuItem.Click += new System.EventHandler(this.VarReadsToolStripMenuItem_Click);
			// 
			// sumReadsToolStripMenuItem
			// 
			this.sumReadsToolStripMenuItem.CheckOnClick = true;
			this.sumReadsToolStripMenuItem.Name = "sumReadsToolStripMenuItem";
			this.sumReadsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.sumReadsToolStripMenuItem.Text = "SumReads";
			this.sumReadsToolStripMenuItem.Click += new System.EventHandler(this.SumReadsToolStripMenuItem_Click);
			// 
			// minWritesToolStripMenuItem
			// 
			this.minWritesToolStripMenuItem.Checked = true;
			this.minWritesToolStripMenuItem.CheckOnClick = true;
			this.minWritesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.minWritesToolStripMenuItem.Name = "minWritesToolStripMenuItem";
			this.minWritesToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.minWritesToolStripMenuItem.Text = "MinWrites";
			this.minWritesToolStripMenuItem.Click += new System.EventHandler(this.MinWritesToolStripMenuItem_Click);
			// 
			// maxWritesToolStripMenuItem
			// 
			this.maxWritesToolStripMenuItem.Checked = true;
			this.maxWritesToolStripMenuItem.CheckOnClick = true;
			this.maxWritesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.maxWritesToolStripMenuItem.Name = "maxWritesToolStripMenuItem";
			this.maxWritesToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.maxWritesToolStripMenuItem.Text = "MaxWrites";
			this.maxWritesToolStripMenuItem.Click += new System.EventHandler(this.MaxWritesToolStripMenuItem_Click);
			// 
			// avgWritesToolStripMenuItem
			// 
			this.avgWritesToolStripMenuItem.Checked = true;
			this.avgWritesToolStripMenuItem.CheckOnClick = true;
			this.avgWritesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.avgWritesToolStripMenuItem.Name = "avgWritesToolStripMenuItem";
			this.avgWritesToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.avgWritesToolStripMenuItem.Text = "AvgWrites";
			this.avgWritesToolStripMenuItem.Click += new System.EventHandler(this.AvgWritesToolStripMenuItem_Click);
			// 
			// devWritesToolStripMenuItem
			// 
			this.devWritesToolStripMenuItem.CheckOnClick = true;
			this.devWritesToolStripMenuItem.Name = "devWritesToolStripMenuItem";
			this.devWritesToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.devWritesToolStripMenuItem.Text = "DevWrites";
			this.devWritesToolStripMenuItem.Click += new System.EventHandler(this.DevWritesToolStripMenuItem_Click);
			// 
			// varWritesToolStripMenuItem
			// 
			this.varWritesToolStripMenuItem.CheckOnClick = true;
			this.varWritesToolStripMenuItem.Name = "varWritesToolStripMenuItem";
			this.varWritesToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.varWritesToolStripMenuItem.Text = "VarWrites";
			this.varWritesToolStripMenuItem.Click += new System.EventHandler(this.VarWritesToolStripMenuItem_Click);
			// 
			// sumWritesToolStripMenuItem
			// 
			this.sumWritesToolStripMenuItem.CheckOnClick = true;
			this.sumWritesToolStripMenuItem.Name = "sumWritesToolStripMenuItem";
			this.sumWritesToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.sumWritesToolStripMenuItem.Text = "SumWrites";
			this.sumWritesToolStripMenuItem.Click += new System.EventHandler(this.SumWritesToolStripMenuItem_Click);
			// 
			// minCpuToolStripMenuItem
			// 
			this.minCpuToolStripMenuItem.Checked = true;
			this.minCpuToolStripMenuItem.CheckOnClick = true;
			this.minCpuToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.minCpuToolStripMenuItem.Name = "minCpuToolStripMenuItem";
			this.minCpuToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.minCpuToolStripMenuItem.Text = "MinCpu";
			this.minCpuToolStripMenuItem.Click += new System.EventHandler(this.MinCpuToolStripMenuItem_Click);
			// 
			// maxCpuToolStripMenuItem
			// 
			this.maxCpuToolStripMenuItem.Checked = true;
			this.maxCpuToolStripMenuItem.CheckOnClick = true;
			this.maxCpuToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.maxCpuToolStripMenuItem.Name = "maxCpuToolStripMenuItem";
			this.maxCpuToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.maxCpuToolStripMenuItem.Text = "MaxCpu";
			this.maxCpuToolStripMenuItem.Click += new System.EventHandler(this.MaxCpuToolStripMenuItem_Click);
			// 
			// avgCpuToolStripMenuItem
			// 
			this.avgCpuToolStripMenuItem.Checked = true;
			this.avgCpuToolStripMenuItem.CheckOnClick = true;
			this.avgCpuToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.avgCpuToolStripMenuItem.Name = "avgCpuToolStripMenuItem";
			this.avgCpuToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.avgCpuToolStripMenuItem.Text = "AvgCpu";
			this.avgCpuToolStripMenuItem.Click += new System.EventHandler(this.AvgCpuToolStripMenuItem_Click);
			// 
			// devCpuToolStripMenuItem
			// 
			this.devCpuToolStripMenuItem.CheckOnClick = true;
			this.devCpuToolStripMenuItem.Name = "devCpuToolStripMenuItem";
			this.devCpuToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.devCpuToolStripMenuItem.Text = "DevCpu";
			this.devCpuToolStripMenuItem.Click += new System.EventHandler(this.DevCpuToolStripMenuItem_Click);
			// 
			// varCpuToolStripMenuItem
			// 
			this.varCpuToolStripMenuItem.CheckOnClick = true;
			this.varCpuToolStripMenuItem.Name = "varCpuToolStripMenuItem";
			this.varCpuToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.varCpuToolStripMenuItem.Text = "VarCpu";
			this.varCpuToolStripMenuItem.Click += new System.EventHandler(this.VarCpuToolStripMenuItem_Click);
			// 
			// sumCpuToolStripMenuItem
			// 
			this.sumCpuToolStripMenuItem.CheckOnClick = true;
			this.sumCpuToolStripMenuItem.Name = "sumCpuToolStripMenuItem";
			this.sumCpuToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.sumCpuToolStripMenuItem.Text = "SumCpu";
			this.sumCpuToolStripMenuItem.Click += new System.EventHandler(this.SumCpuToolStripMenuItem_Click);
			// 
			// minRowsToolStripMenuItem
			// 
			this.minRowsToolStripMenuItem.Checked = true;
			this.minRowsToolStripMenuItem.CheckOnClick = true;
			this.minRowsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.minRowsToolStripMenuItem.Name = "minRowsToolStripMenuItem";
			this.minRowsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.minRowsToolStripMenuItem.Text = "MinRows";
			this.minRowsToolStripMenuItem.Click += new System.EventHandler(this.MinRowsToolStripMenuItem_Click);
			// 
			// maxRowsToolStripMenuItem
			// 
			this.maxRowsToolStripMenuItem.Checked = true;
			this.maxRowsToolStripMenuItem.CheckOnClick = true;
			this.maxRowsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.maxRowsToolStripMenuItem.Name = "maxRowsToolStripMenuItem";
			this.maxRowsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.maxRowsToolStripMenuItem.Text = "MaxRows";
			this.maxRowsToolStripMenuItem.Click += new System.EventHandler(this.MaxRowsToolStripMenuItem_Click);
			// 
			// avgRowsToolStripMenuItem
			// 
			this.avgRowsToolStripMenuItem.Checked = true;
			this.avgRowsToolStripMenuItem.CheckOnClick = true;
			this.avgRowsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.avgRowsToolStripMenuItem.Name = "avgRowsToolStripMenuItem";
			this.avgRowsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.avgRowsToolStripMenuItem.Text = "AvgRows";
			this.avgRowsToolStripMenuItem.Click += new System.EventHandler(this.AvgRowsToolStripMenuItem_Click);
			// 
			// devRowsToolStripMenuItem
			// 
			this.devRowsToolStripMenuItem.CheckOnClick = true;
			this.devRowsToolStripMenuItem.Name = "devRowsToolStripMenuItem";
			this.devRowsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.devRowsToolStripMenuItem.Text = "DevRows";
			this.devRowsToolStripMenuItem.Click += new System.EventHandler(this.DevRowsToolStripMenuItem_Click);
			// 
			// varRowsToolStripMenuItem
			// 
			this.varRowsToolStripMenuItem.CheckOnClick = true;
			this.varRowsToolStripMenuItem.Name = "varRowsToolStripMenuItem";
			this.varRowsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.varRowsToolStripMenuItem.Text = "VarRows";
			this.varRowsToolStripMenuItem.Click += new System.EventHandler(this.VarRowsToolStripMenuItem_Click);
			// 
			// sumRowsToolStripMenuItem
			// 
			this.sumRowsToolStripMenuItem.CheckOnClick = true;
			this.sumRowsToolStripMenuItem.Name = "sumRowsToolStripMenuItem";
			this.sumRowsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.sumRowsToolStripMenuItem.Text = "SumRows";
			this.sumRowsToolStripMenuItem.Click += new System.EventHandler(this.SumRowsToolStripMenuItem_Click);
			// 
			// filterLabel
			// 
			this.filterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.filterLabel.AutoSize = true;
			this.filterLabel.Location = new System.Drawing.Point(12, 616);
			this.filterLabel.Name = "filterLabel";
			this.filterLabel.Size = new System.Drawing.Size(0, 13);
			this.filterLabel.TabIndex = 3;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem1});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(112, 26);
			// 
			// viewToolStripMenuItem1
			// 
			this.viewToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.viewToolStripMenuItem1.Name = "viewToolStripMenuItem1";
			this.viewToolStripMenuItem1.Size = new System.Drawing.Size(111, 22);
			this.viewToolStripMenuItem1.Text = "View...";
			this.viewToolStripMenuItem1.Click += new System.EventHandler(this.ViewToolStripMenuItem1_Click);
			// 
			// ViewStatisticsForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.okButton;
			this.ClientSize = new System.Drawing.Size(884, 641);
			this.Controls.Add(this.filterLabel);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.statisticsGroupBox);
			this.Controls.Add(this.okButton);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(666, 394);
			this.Name = "ViewStatisticsForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Title";
			this.Resize += new System.EventHandler(this.ViewStatisticsForm_Resize);
			this.statisticsGroupBox.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Button okButton;
	private System.Windows.Forms.GroupBox statisticsGroupBox;
	private System.Windows.Forms.MenuStrip menuStrip1;
	private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
	private DataViewer dataViewer1;
	private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	private System.Windows.Forms.Label filterLabel;
	private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem columnsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem minDurationToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem maxDurationToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem avgDurationToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem devDurationToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem varDurationToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem sumDurationToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem minReadsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem maxReadsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem avgReadsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem devReadsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem varReadsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem sumReadsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem minWritesToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem maxWritesToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem avgWritesToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem devWritesToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem varWritesToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem sumWritesToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem minCpuToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem maxCpuToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem avgCpuToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem devCpuToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem varCpuToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem sumCpuToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem minRowsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem maxRowsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem avgRowsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem devRowsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem varRowsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem sumRowsToolStripMenuItem;
	private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
	private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem1;
}
