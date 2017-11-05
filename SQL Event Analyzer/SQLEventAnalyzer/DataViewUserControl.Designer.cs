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

partial class DataViewUserControl
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
			this.components = new System.ComponentModel.Container();
			this.backButton = new System.Windows.Forms.Button();
			this.numberOfEventsLabel = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.textDataTabPage = new System.Windows.Forms.TabPage();
			this.textDataToolStrip1 = new System.Windows.Forms.ToolStrip();
			this.findToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.fontToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.textDataTextBox = new ICSharpCode.TextEditor.TextEditorControl();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemUndo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemRedo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemCut = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.filter1TabPage = new System.Windows.Forms.TabPage();
			this.filter1UserControl = new Filter1UserControl();
			this.filter2TabPage = new System.Windows.Forms.TabPage();
			this.filter2UserControl = new Filter2UserControl();
			this.statisticsTabPage = new System.Windows.Forms.TabPage();
			this.statisticUserControl1 = new StatisticUserControl();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.eventsGroupBox = new System.Windows.Forms.GroupBox();
			this.dataViewer1 = new DataViewer();
			this.dataAndFiltersGroupBox = new System.Windows.Forms.GroupBox();
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.viewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1.SuspendLayout();
			this.textDataTabPage.SuspendLayout();
			this.textDataToolStrip1.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			this.filter1TabPage.SuspendLayout();
			this.filter2TabPage.SuspendLayout();
			this.statisticsTabPage.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.eventsGroupBox.SuspendLayout();
			this.dataAndFiltersGroupBox.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// backButton
			// 
			this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.backButton.Location = new System.Drawing.Point(798, 586);
			this.backButton.Name = "backButton";
			this.backButton.Size = new System.Drawing.Size(75, 24);
			this.backButton.TabIndex = 2;
			this.backButton.Text = "<- Back";
			this.backButton.UseVisualStyleBackColor = true;
			this.backButton.Click += new System.EventHandler(this.BackButton_Click);
			// 
			// numberOfEventsLabel
			// 
			this.numberOfEventsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.numberOfEventsLabel.AutoSize = true;
			this.numberOfEventsLabel.Location = new System.Drawing.Point(9, 592);
			this.numberOfEventsLabel.Name = "numberOfEventsLabel";
			this.numberOfEventsLabel.Size = new System.Drawing.Size(70, 13);
			this.numberOfEventsLabel.TabIndex = 2;
			this.numberOfEventsLabel.Text = "Total Events:";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.textDataTabPage);
			this.tabControl1.Controls.Add(this.filter1TabPage);
			this.tabControl1.Controls.Add(this.filter2TabPage);
			this.tabControl1.Controls.Add(this.statisticsTabPage);
			this.tabControl1.Location = new System.Drawing.Point(6, 19);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(850, 275);
			this.tabControl1.TabIndex = 1;
			this.tabControl1.TabStop = false;
			// 
			// textDataTabPage
			// 
			this.textDataTabPage.Controls.Add(this.textDataToolStrip1);
			this.textDataTabPage.Controls.Add(this.textDataTextBox);
			this.textDataTabPage.Location = new System.Drawing.Point(4, 22);
			this.textDataTabPage.Name = "textDataTabPage";
			this.textDataTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.textDataTabPage.Size = new System.Drawing.Size(842, 249);
			this.textDataTabPage.TabIndex = 1;
			this.textDataTabPage.Text = "TextData";
			this.textDataTabPage.UseVisualStyleBackColor = true;
			// 
			// textDataToolStrip1
			// 
			this.textDataToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripButton,
            this.fontToolStripButton});
			this.textDataToolStrip1.Location = new System.Drawing.Point(3, 3);
			this.textDataToolStrip1.Name = "textDataToolStrip1";
			this.textDataToolStrip1.Size = new System.Drawing.Size(836, 25);
			this.textDataToolStrip1.TabIndex = 1;
			this.textDataToolStrip1.Text = "textDataToolStrip1";
			// 
			// findToolStripButton
			// 
			this.findToolStripButton.Image = global::SQLEventAnalyzer.Properties.Resources.find_small;
			this.findToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.findToolStripButton.Name = "findToolStripButton";
			this.findToolStripButton.Size = new System.Drawing.Size(62, 22);
			this.findToolStripButton.Text = "Search";
			this.findToolStripButton.Click += new System.EventHandler(this.FindToolStripButton_Click);
			// 
			// fontToolStripButton
			// 
			this.fontToolStripButton.Image = global::SQLEventAnalyzer.Properties.Resources.font;
			this.fontToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.fontToolStripButton.Name = "fontToolStripButton";
			this.fontToolStripButton.Size = new System.Drawing.Size(51, 22);
			this.fontToolStripButton.Text = "Font";
			this.fontToolStripButton.Click += new System.EventHandler(this.FontToolStripButton_Click);
			// 
			// textDataTextBox
			// 
			this.textDataTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textDataTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textDataTextBox.ContextMenuStrip = this.contextMenuStrip;
			this.textDataTextBox.EnableFolding = false;
			this.textDataTextBox.IsReadOnly = false;
			this.textDataTextBox.Location = new System.Drawing.Point(2, 31);
			this.textDataTextBox.Name = "textDataTextBox";
			this.textDataTextBox.ShowVRuler = false;
			this.textDataTextBox.Size = new System.Drawing.Size(836, 215);
			this.textDataTextBox.TabIndex = 1;
			this.textDataTextBox.TextChanged += new System.EventHandler(this.TextDataTextBox_TextChanged);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem,
            this.toolStripSeparator3,
            this.toolStripMenuItemUndo,
            this.toolStripMenuItemRedo,
            this.toolStripSeparator1,
            this.toolStripMenuItemCut,
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemPaste,
            this.toolStripMenuItemDelete,
            this.toolStripSeparator2,
            this.toolStripMenuItemSelectAll});
			this.contextMenuStrip.Name = "contextMenuStrip1";
			this.contextMenuStrip.Size = new System.Drawing.Size(165, 198);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
			// 
			// findToolStripMenuItem
			// 
			this.findToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.find_small;
			this.findToolStripMenuItem.Name = "findToolStripMenuItem";
			this.findToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+F";
			this.findToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.findToolStripMenuItem.Text = "Search...";
			this.findToolStripMenuItem.Click += new System.EventHandler(this.FindToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(161, 6);
			// 
			// toolStripMenuItemUndo
			// 
			this.toolStripMenuItemUndo.Name = "toolStripMenuItemUndo";
			this.toolStripMenuItemUndo.ShortcutKeyDisplayString = "Ctrl+Z";
			this.toolStripMenuItemUndo.Size = new System.Drawing.Size(164, 22);
			this.toolStripMenuItemUndo.Text = "Undo";
			this.toolStripMenuItemUndo.Click += new System.EventHandler(this.ToolStripMenuItemUndo_Click);
			// 
			// toolStripMenuItemRedo
			// 
			this.toolStripMenuItemRedo.Name = "toolStripMenuItemRedo";
			this.toolStripMenuItemRedo.ShortcutKeyDisplayString = "Ctrl+Y";
			this.toolStripMenuItemRedo.Size = new System.Drawing.Size(164, 22);
			this.toolStripMenuItemRedo.Text = "Redo";
			this.toolStripMenuItemRedo.Click += new System.EventHandler(this.ToolStripMenuItemRedo_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
			// 
			// toolStripMenuItemCut
			// 
			this.toolStripMenuItemCut.Name = "toolStripMenuItemCut";
			this.toolStripMenuItemCut.ShortcutKeyDisplayString = "Ctrl+X";
			this.toolStripMenuItemCut.Size = new System.Drawing.Size(164, 22);
			this.toolStripMenuItemCut.Text = "Cut";
			this.toolStripMenuItemCut.Click += new System.EventHandler(this.ToolStripMenuItemCut_Click);
			// 
			// toolStripMenuItemCopy
			// 
			this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
			this.toolStripMenuItemCopy.ShortcutKeyDisplayString = "Ctrl+C";
			this.toolStripMenuItemCopy.Size = new System.Drawing.Size(164, 22);
			this.toolStripMenuItemCopy.Text = "Copy";
			this.toolStripMenuItemCopy.Click += new System.EventHandler(this.ToolStripMenuItemCopy_Click);
			// 
			// toolStripMenuItemPaste
			// 
			this.toolStripMenuItemPaste.Name = "toolStripMenuItemPaste";
			this.toolStripMenuItemPaste.ShortcutKeyDisplayString = "Ctrl+V";
			this.toolStripMenuItemPaste.Size = new System.Drawing.Size(164, 22);
			this.toolStripMenuItemPaste.Text = "Paste";
			this.toolStripMenuItemPaste.Click += new System.EventHandler(this.ToolStripMenuItemPaste_Click);
			// 
			// toolStripMenuItemDelete
			// 
			this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			this.toolStripMenuItemDelete.ShortcutKeyDisplayString = "Del";
			this.toolStripMenuItemDelete.Size = new System.Drawing.Size(164, 22);
			this.toolStripMenuItemDelete.Text = "Delete";
			this.toolStripMenuItemDelete.Click += new System.EventHandler(this.ToolStripMenuItemDelete_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
			// 
			// toolStripMenuItemSelectAll
			// 
			this.toolStripMenuItemSelectAll.Name = "toolStripMenuItemSelectAll";
			this.toolStripMenuItemSelectAll.ShortcutKeyDisplayString = "Ctrl+A";
			this.toolStripMenuItemSelectAll.Size = new System.Drawing.Size(164, 22);
			this.toolStripMenuItemSelectAll.Text = "Select All";
			this.toolStripMenuItemSelectAll.Click += new System.EventHandler(this.ToolStripMenuItemSelectAll_Click);
			// 
			// filter1TabPage
			// 
			this.filter1TabPage.Controls.Add(this.filter1UserControl);
			this.filter1TabPage.Location = new System.Drawing.Point(4, 22);
			this.filter1TabPage.Name = "filter1TabPage";
			this.filter1TabPage.Padding = new System.Windows.Forms.Padding(3);
			this.filter1TabPage.Size = new System.Drawing.Size(842, 249);
			this.filter1TabPage.TabIndex = 0;
			this.filter1TabPage.Text = "Filter 1";
			this.filter1TabPage.UseVisualStyleBackColor = true;
			// 
			// filter1UserControl
			// 
			this.filter1UserControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.filter1UserControl.Location = new System.Drawing.Point(3, 3);
			this.filter1UserControl.MinimumSize = new System.Drawing.Size(430, 215);
			this.filter1UserControl.Name = "filter1UserControl";
			this.filter1UserControl.Size = new System.Drawing.Size(836, 243);
			this.filter1UserControl.TabIndex = 0;
			// 
			// filter2TabPage
			// 
			this.filter2TabPage.Controls.Add(this.filter2UserControl);
			this.filter2TabPage.Location = new System.Drawing.Point(4, 22);
			this.filter2TabPage.Name = "filter2TabPage";
			this.filter2TabPage.Padding = new System.Windows.Forms.Padding(3);
			this.filter2TabPage.Size = new System.Drawing.Size(842, 249);
			this.filter2TabPage.TabIndex = 2;
			this.filter2TabPage.Text = "Filter 2";
			this.filter2TabPage.UseVisualStyleBackColor = true;
			// 
			// filter2UserControl
			// 
			this.filter2UserControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.filter2UserControl.Location = new System.Drawing.Point(3, 3);
			this.filter2UserControl.MinimumSize = new System.Drawing.Size(430, 215);
			this.filter2UserControl.Name = "filter2UserControl";
			this.filter2UserControl.Size = new System.Drawing.Size(836, 243);
			this.filter2UserControl.TabIndex = 0;
			// 
			// statisticsTabPage
			// 
			this.statisticsTabPage.Controls.Add(this.statisticUserControl1);
			this.statisticsTabPage.Location = new System.Drawing.Point(4, 22);
			this.statisticsTabPage.Name = "statisticsTabPage";
			this.statisticsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.statisticsTabPage.Size = new System.Drawing.Size(842, 249);
			this.statisticsTabPage.TabIndex = 3;
			this.statisticsTabPage.Text = "Statistics";
			this.statisticsTabPage.UseVisualStyleBackColor = true;
			// 
			// statisticUserControl1
			// 
			this.statisticUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statisticUserControl1.Location = new System.Drawing.Point(3, 3);
			this.statisticUserControl1.MinimumSize = new System.Drawing.Size(430, 215);
			this.statisticUserControl1.Name = "statisticUserControl1";
			this.statisticUserControl1.Size = new System.Drawing.Size(836, 243);
			this.statisticUserControl1.TabIndex = 0;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(9, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.splitContainer1.Size = new System.Drawing.Size(866, 581);
			this.splitContainer1.SplitterDistance = 271;
			this.splitContainer1.TabIndex = 0;
			this.splitContainer1.TabStop = false;
			this.splitContainer1.Paint += new System.Windows.Forms.PaintEventHandler(this.SplitContainer1_Paint);
			this.splitContainer1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SplitContainer1_MouseUp);
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.eventsGroupBox);
			this.splitContainer1.Panel1MinSize = 223;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.dataAndFiltersGroupBox);
			this.splitContainer1.Panel2MinSize = 306;
			// 
			// eventsGroupBox
			// 
			this.eventsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.eventsGroupBox.Controls.Add(this.dataViewer1);
			this.eventsGroupBox.Location = new System.Drawing.Point(3, 3);
			this.eventsGroupBox.Name = "eventsGroupBox";
			this.eventsGroupBox.Size = new System.Drawing.Size(860, 256);
			this.eventsGroupBox.TabIndex = 1;
			this.eventsGroupBox.TabStop = false;
			this.eventsGroupBox.Text = "Events";
			// 
			// dataViewer1
			// 
			this.dataViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataViewer1.BackColor = System.Drawing.Color.White;
			this.dataViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.dataViewer1.Location = new System.Drawing.Point(6, 19);
			this.dataViewer1.MinimumSize = new System.Drawing.Size(367, 182);
			this.dataViewer1.Name = "dataViewer1";
			this.dataViewer1.Size = new System.Drawing.Size(848, 231);
			this.dataViewer1.TabIndex = 0;
			// 
			// dataAndFiltersGroupBox
			// 
			this.dataAndFiltersGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataAndFiltersGroupBox.Controls.Add(this.tabControl1);
			this.dataAndFiltersGroupBox.Location = new System.Drawing.Point(3, 3);
			this.dataAndFiltersGroupBox.Name = "dataAndFiltersGroupBox";
			this.dataAndFiltersGroupBox.Size = new System.Drawing.Size(860, 300);
			this.dataAndFiltersGroupBox.TabIndex = 2;
			this.dataAndFiltersGroupBox.TabStop = false;
			this.dataAndFiltersGroupBox.Text = "Data and Filters";
			// 
			// fontDialog1
			// 
			this.fontDialog1.AllowScriptChange = false;
			this.fontDialog1.Color = System.Drawing.SystemColors.ControlText;
			this.fontDialog1.FixedPitchOnly = true;
			this.fontDialog1.ShowEffects = false;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem1});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
			// 
			// viewToolStripMenuItem1
			// 
			this.viewToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.viewToolStripMenuItem1.Name = "viewToolStripMenuItem1";
			this.viewToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.viewToolStripMenuItem1.Text = "View...";
			this.viewToolStripMenuItem1.Click += new System.EventHandler(this.ViewToolStripMenuItem1_Click);
			// 
			// DataViewUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.numberOfEventsLabel);
			this.Controls.Add(this.backButton);
			this.DoubleBuffered = true;
			this.Name = "DataViewUserControl";
			this.Size = new System.Drawing.Size(884, 617);
			this.tabControl1.ResumeLayout(false);
			this.textDataTabPage.ResumeLayout(false);
			this.textDataTabPage.PerformLayout();
			this.textDataToolStrip1.ResumeLayout(false);
			this.textDataToolStrip1.PerformLayout();
			this.contextMenuStrip.ResumeLayout(false);
			this.filter1TabPage.ResumeLayout(false);
			this.filter2TabPage.ResumeLayout(false);
			this.statisticsTabPage.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.eventsGroupBox.ResumeLayout(false);
			this.dataAndFiltersGroupBox.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Button backButton;
	private System.Windows.Forms.Label numberOfEventsLabel;
	private DataViewer dataViewer1;
	private System.Windows.Forms.TabControl tabControl1;
	private System.Windows.Forms.TabPage filter1TabPage;
	private Filter1UserControl filter1UserControl;
	private System.Windows.Forms.TabPage textDataTabPage;
	private System.Windows.Forms.SplitContainer splitContainer1;
	private ICSharpCode.TextEditor.TextEditorControl textDataTextBox;
	private System.Windows.Forms.ToolStrip textDataToolStrip1;
	private System.Windows.Forms.ToolStripButton findToolStripButton;
	private System.Windows.Forms.ToolStripButton fontToolStripButton;
	private System.Windows.Forms.FontDialog fontDialog1;
	private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
	private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemUndo;
	private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRedo;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCut;
	private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
	private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPaste;
	private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSelectAll;
	private System.Windows.Forms.TabPage filter2TabPage;
	private Filter2UserControl filter2UserControl;
	private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
	private System.Windows.Forms.GroupBox eventsGroupBox;
	private System.Windows.Forms.GroupBox dataAndFiltersGroupBox;
	private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
	private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem1;
	private System.Windows.Forms.TabPage statisticsTabPage;
	private StatisticUserControl statisticUserControl1;
}
