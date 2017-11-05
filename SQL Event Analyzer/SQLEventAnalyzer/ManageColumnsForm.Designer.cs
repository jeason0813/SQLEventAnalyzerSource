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

partial class ManageColumnsForm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageColumnsForm));
			this.okButton = new System.Windows.Forms.Button();
			this.columnsGroupBox = new System.Windows.Forms.GroupBox();
			this.columnsDataGridView = new System.Windows.Forms.DataGridView();
			this.ImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
			this.NumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.InputTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.OutputTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.HiddenColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.editButton = new System.Windows.Forms.Button();
			this.moveDownButton = new System.Windows.Forms.Button();
			this.moveUpButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			this.createButton = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
			this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.loadColumnSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.createMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.editMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
			this.changeMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toggleHiddenMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.hiddenMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.shownMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
			this.toggleActiveMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.activeMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.inactiveMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.isolationLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.readUncommittedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.readCommittedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.repeatableReadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.serializableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.cutMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.copyMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.selectAllMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.moveUpMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.moveDownMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.onlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.searchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
			this.createMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
			this.changeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toggleHiddenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hiddenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.shownMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
			this.toggleActiveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.activeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.inactiveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
			this.isolationLevelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.readUncommittedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.readCommittedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.repeatableReadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.serializableToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.cutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.selectAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.moveUpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveDownMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.parametersGroupBox = new System.Windows.Forms.GroupBox();
			this.parametersDataGridView = new System.Windows.Forms.DataGridView();
			this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.restartToUpdateTextBox = new System.Windows.Forms.TextBox();
			this.columnsGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.columnsDataGridView)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.parametersGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.parametersDataGridView)).BeginInit();
			this.contextMenuStrip2.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.okButton.Location = new System.Drawing.Point(798, 610);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 24);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// columnsGroupBox
			// 
			this.columnsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.columnsGroupBox.Controls.Add(this.columnsDataGridView);
			this.columnsGroupBox.Controls.Add(this.editButton);
			this.columnsGroupBox.Controls.Add(this.moveDownButton);
			this.columnsGroupBox.Controls.Add(this.moveUpButton);
			this.columnsGroupBox.Controls.Add(this.deleteButton);
			this.columnsGroupBox.Controls.Add(this.createButton);
			this.columnsGroupBox.Location = new System.Drawing.Point(12, 27);
			this.columnsGroupBox.Name = "columnsGroupBox";
			this.columnsGroupBox.Size = new System.Drawing.Size(860, 450);
			this.columnsGroupBox.TabIndex = 0;
			this.columnsGroupBox.TabStop = false;
			this.columnsGroupBox.Text = "Columns";
			// 
			// columnsDataGridView
			// 
			this.columnsDataGridView.AllowDrop = true;
			this.columnsDataGridView.AllowUserToAddRows = false;
			this.columnsDataGridView.AllowUserToDeleteRows = false;
			this.columnsDataGridView.AllowUserToResizeRows = false;
			this.columnsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.columnsDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.columnsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			this.columnsDataGridView.ColumnHeadersHeight = 20;
			this.columnsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.columnsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ImageColumn,
            this.NumberColumn,
            this.ItemName,
            this.InputTypeColumn,
            this.OutputTypeColumn,
            this.HiddenColumn});
			this.columnsDataGridView.Location = new System.Drawing.Point(6, 19);
			this.columnsDataGridView.Name = "columnsDataGridView";
			this.columnsDataGridView.ReadOnly = true;
			this.columnsDataGridView.RowHeadersVisible = false;
			this.columnsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.columnsDataGridView.Size = new System.Drawing.Size(767, 425);
			this.columnsDataGridView.StandardTab = true;
			this.columnsDataGridView.TabIndex = 0;
			this.columnsDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ColumnsDataGridView_CellDoubleClick);
			this.columnsDataGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ColumnsDataGridView_CellMouseClick);
			this.columnsDataGridView.SelectionChanged += new System.EventHandler(this.ColumnsDataGridView_SelectionChanged);
			this.columnsDataGridView.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColumnsDataGridView_DragDrop);
			this.columnsDataGridView.DragOver += new System.Windows.Forms.DragEventHandler(this.ColumnsDataGridView_DragOver);
			this.columnsDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ColumnsDataGridView_KeyDown);
			this.columnsDataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColumnsDataGridView_MouseDown);
			this.columnsDataGridView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ColumnsDataGridView_MouseMove);
			// 
			// ImageColumn
			// 
			this.ImageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.NullValue = null;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.ImageColumn.DefaultCellStyle = dataGridViewCellStyle1;
			this.ImageColumn.HeaderText = "";
			this.ImageColumn.Image = global::SQLEventAnalyzer.Properties.Resources.columns_small;
			this.ImageColumn.MinimumWidth = 26;
			this.ImageColumn.Name = "ImageColumn";
			this.ImageColumn.ReadOnly = true;
			this.ImageColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.ImageColumn.Width = 26;
			// 
			// NumberColumn
			// 
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.NumberColumn.DefaultCellStyle = dataGridViewCellStyle2;
			this.NumberColumn.HeaderText = "#";
			this.NumberColumn.MinimumWidth = 70;
			this.NumberColumn.Name = "NumberColumn";
			this.NumberColumn.ReadOnly = true;
			this.NumberColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.NumberColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.NumberColumn.Width = 70;
			// 
			// ItemName
			// 
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.ItemName.DefaultCellStyle = dataGridViewCellStyle3;
			this.ItemName.HeaderText = "Name";
			this.ItemName.MinimumWidth = 50;
			this.ItemName.Name = "ItemName";
			this.ItemName.ReadOnly = true;
			this.ItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ItemName.Width = 265;
			// 
			// InputTypeColumn
			// 
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.InputTypeColumn.DefaultCellStyle = dataGridViewCellStyle4;
			this.InputTypeColumn.HeaderText = "Input Type";
			this.InputTypeColumn.MinimumWidth = 50;
			this.InputTypeColumn.Name = "InputTypeColumn";
			this.InputTypeColumn.ReadOnly = true;
			this.InputTypeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.InputTypeColumn.Width = 170;
			// 
			// OutputTypeColumn
			// 
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.OutputTypeColumn.DefaultCellStyle = dataGridViewCellStyle5;
			this.OutputTypeColumn.HeaderText = "Output Type";
			this.OutputTypeColumn.MinimumWidth = 50;
			this.OutputTypeColumn.Name = "OutputTypeColumn";
			this.OutputTypeColumn.ReadOnly = true;
			this.OutputTypeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.OutputTypeColumn.Width = 170;
			// 
			// HiddenColumn
			// 
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle6.NullValue = false;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.HiddenColumn.DefaultCellStyle = dataGridViewCellStyle6;
			this.HiddenColumn.HeaderText = "Hidden";
			this.HiddenColumn.MinimumWidth = 46;
			this.HiddenColumn.Name = "HiddenColumn";
			this.HiddenColumn.ReadOnly = true;
			this.HiddenColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.HiddenColumn.Width = 46;
			// 
			// editButton
			// 
			this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.editButton.Enabled = false;
			this.editButton.Location = new System.Drawing.Point(779, 48);
			this.editButton.Name = "editButton";
			this.editButton.Size = new System.Drawing.Size(75, 24);
			this.editButton.TabIndex = 2;
			this.editButton.Text = "Edit";
			this.editButton.UseVisualStyleBackColor = true;
			this.editButton.Click += new System.EventHandler(this.EditButton_Click);
			// 
			// moveDownButton
			// 
			this.moveDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.moveDownButton.Enabled = false;
			this.moveDownButton.Location = new System.Drawing.Point(779, 421);
			this.moveDownButton.Name = "moveDownButton";
			this.moveDownButton.Size = new System.Drawing.Size(75, 24);
			this.moveDownButton.TabIndex = 5;
			this.moveDownButton.Text = "Move Down";
			this.moveDownButton.UseVisualStyleBackColor = true;
			this.moveDownButton.Click += new System.EventHandler(this.MoveDownButton_Click);
			// 
			// moveUpButton
			// 
			this.moveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.moveUpButton.Enabled = false;
			this.moveUpButton.Location = new System.Drawing.Point(779, 392);
			this.moveUpButton.Name = "moveUpButton";
			this.moveUpButton.Size = new System.Drawing.Size(75, 24);
			this.moveUpButton.TabIndex = 4;
			this.moveUpButton.Text = "Move Up";
			this.moveUpButton.UseVisualStyleBackColor = true;
			this.moveUpButton.Click += new System.EventHandler(this.MoveUpButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.deleteButton.Enabled = false;
			this.deleteButton.Location = new System.Drawing.Point(779, 77);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(75, 24);
			this.deleteButton.TabIndex = 3;
			this.deleteButton.Text = "Delete";
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// createButton
			// 
			this.createButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.createButton.Location = new System.Drawing.Point(779, 19);
			this.createButton.Name = "createButton";
			this.createButton.Size = new System.Drawing.Size(75, 24);
			this.createButton.TabIndex = 1;
			this.createButton.Text = "Create";
			this.createButton.UseVisualStyleBackColor = true;
			this.createButton.Click += new System.EventHandler(this.CreateButton_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionToolStripMenuItem,
            this.onlineToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.menuStrip1.Size = new System.Drawing.Size(884, 24);
			this.menuStrip1.TabIndex = 7;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSessionToolStripMenuItem,
            this.toolStripSeparator17,
            this.importToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripSeparator9,
            this.loadColumnSetToolStripMenuItem,
            this.toolStripSeparator2,
            this.recentFilesToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// newSessionToolStripMenuItem
			// 
			this.newSessionToolStripMenuItem.Name = "newSessionToolStripMenuItem";
			this.newSessionToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.newSessionToolStripMenuItem.Text = "&New Column Set";
			this.newSessionToolStripMenuItem.Click += new System.EventHandler(this.NewSessionToolStripMenuItem_Click);
			// 
			// toolStripSeparator17
			// 
			this.toolStripSeparator17.Name = "toolStripSeparator17";
			this.toolStripSeparator17.Size = new System.Drawing.Size(200, 6);
			// 
			// importToolStripMenuItem
			// 
			this.importToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.folder;
			this.importToolStripMenuItem.Name = "importToolStripMenuItem";
			this.importToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.importToolStripMenuItem.Text = "&Open Column Set...";
			this.importToolStripMenuItem.Click += new System.EventHandler(this.ImportToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Enabled = false;
			this.saveToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.disk;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.saveToolStripMenuItem.Text = "&Save Column Set";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
			// 
			// exportToolStripMenuItem
			// 
			this.exportToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.disk;
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.exportToolStripMenuItem.Text = "Save Column Set &As...";
			this.exportToolStripMenuItem.Click += new System.EventHandler(this.ExportToolStripMenuItem_Click);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(200, 6);
			// 
			// loadColumnSetToolStripMenuItem
			// 
			this.loadColumnSetToolStripMenuItem.Name = "loadColumnSetToolStripMenuItem";
			this.loadColumnSetToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.loadColumnSetToolStripMenuItem.Text = "&Load Column Set";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(200, 6);
			// 
			// recentFilesToolStripMenuItem
			// 
			this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
			this.recentFilesToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.recentFilesToolStripMenuItem.Text = "&Recent Files";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(200, 6);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.closeToolStripMenuItem.Text = "&Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
			// 
			// actionToolStripMenuItem
			// 
			this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem,
            this.toolStripSeparator11,
            this.createMenuItem1,
            this.editMenuItem1,
            this.deleteMenuItem1,
            this.toolStripSeparator13,
            this.changeMenuItem1,
            this.toolStripSeparator3,
            this.cutMenuItem1,
            this.copyMenuItem1,
            this.pasteMenuItem1,
            this.toolStripSeparator4,
            this.selectAllMenuItem1,
            this.toolStripSeparator5,
            this.moveUpMenuItem1,
            this.moveDownMenuItem1});
			this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
			this.actionToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.actionToolStripMenuItem.Text = "&Action";
			this.actionToolStripMenuItem.DropDownOpening += new System.EventHandler(this.ActionToolStripMenuItem_DropDownOpening);
			// 
			// searchToolStripMenuItem
			// 
			this.searchToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.find_small;
			this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
			this.searchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.searchToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.searchToolStripMenuItem.Text = "Search...";
			this.searchToolStripMenuItem.Click += new System.EventHandler(this.SearchToolStripMenuItem_Click);
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(177, 6);
			// 
			// createMenuItem1
			// 
			this.createMenuItem1.Name = "createMenuItem1";
			this.createMenuItem1.ShortcutKeyDisplayString = "Ctrl+N";
			this.createMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.createMenuItem1.Text = "Create";
			this.createMenuItem1.Click += new System.EventHandler(this.CreateToolStripMenuItem_Click);
			// 
			// editMenuItem1
			// 
			this.editMenuItem1.Image = global::SQLEventAnalyzer.Properties.Resources.columns_small;
			this.editMenuItem1.Name = "editMenuItem1";
			this.editMenuItem1.ShortcutKeyDisplayString = "Enter";
			this.editMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.editMenuItem1.Text = "Edit";
			this.editMenuItem1.Click += new System.EventHandler(this.EditToolStripMenuItem_Click);
			// 
			// deleteMenuItem1
			// 
			this.deleteMenuItem1.Name = "deleteMenuItem1";
			this.deleteMenuItem1.ShortcutKeyDisplayString = "Del";
			this.deleteMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.deleteMenuItem1.Text = "Delete";
			this.deleteMenuItem1.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
			// 
			// toolStripSeparator13
			// 
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(177, 6);
			// 
			// changeMenuItem1
			// 
			this.changeMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleHiddenMenuItem1,
            this.hiddenMenuItem1,
            this.shownMenuItem1,
            this.toolStripSeparator15,
            this.toggleActiveMenuItem1,
            this.activeMenuItem1,
            this.inactiveMenuItem1,
            this.toolStripSeparator7,
            this.isolationLevelToolStripMenuItem});
			this.changeMenuItem1.Name = "changeMenuItem1";
			this.changeMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.changeMenuItem1.Text = "Change";
			// 
			// toggleHiddenMenuItem1
			// 
			this.toggleHiddenMenuItem1.Name = "toggleHiddenMenuItem1";
			this.toggleHiddenMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
			this.toggleHiddenMenuItem1.Size = new System.Drawing.Size(196, 22);
			this.toggleHiddenMenuItem1.Text = "Toggle Hidden";
			this.toggleHiddenMenuItem1.Click += new System.EventHandler(this.ToggleHiddenMenuItem1_Click);
			// 
			// hiddenMenuItem1
			// 
			this.hiddenMenuItem1.Name = "hiddenMenuItem1";
			this.hiddenMenuItem1.Size = new System.Drawing.Size(196, 22);
			this.hiddenMenuItem1.Text = "Hidden";
			this.hiddenMenuItem1.Click += new System.EventHandler(this.HiddenMenuItem1_Click);
			// 
			// shownMenuItem1
			// 
			this.shownMenuItem1.Name = "shownMenuItem1";
			this.shownMenuItem1.Size = new System.Drawing.Size(196, 22);
			this.shownMenuItem1.Text = "Shown";
			this.shownMenuItem1.Click += new System.EventHandler(this.ShownMenuItem1_Click);
			// 
			// toolStripSeparator15
			// 
			this.toolStripSeparator15.Name = "toolStripSeparator15";
			this.toolStripSeparator15.Size = new System.Drawing.Size(193, 6);
			// 
			// toggleActiveMenuItem1
			// 
			this.toggleActiveMenuItem1.Name = "toggleActiveMenuItem1";
			this.toggleActiveMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
			this.toggleActiveMenuItem1.Size = new System.Drawing.Size(196, 22);
			this.toggleActiveMenuItem1.Text = "Toggle Enabled";
			this.toggleActiveMenuItem1.Click += new System.EventHandler(this.ToggleActiveMenuItem1_Click);
			// 
			// activeMenuItem1
			// 
			this.activeMenuItem1.Name = "activeMenuItem1";
			this.activeMenuItem1.Size = new System.Drawing.Size(196, 22);
			this.activeMenuItem1.Text = "Enabled";
			this.activeMenuItem1.Click += new System.EventHandler(this.ActiveMenuItem1_Click);
			// 
			// inactiveMenuItem1
			// 
			this.inactiveMenuItem1.Name = "inactiveMenuItem1";
			this.inactiveMenuItem1.Size = new System.Drawing.Size(196, 22);
			this.inactiveMenuItem1.Text = "Disabled";
			this.inactiveMenuItem1.Click += new System.EventHandler(this.InactiveMenuItem1_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(193, 6);
			// 
			// isolationLevelToolStripMenuItem
			// 
			this.isolationLevelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readUncommittedToolStripMenuItem,
            this.readCommittedToolStripMenuItem,
            this.repeatableReadToolStripMenuItem,
            this.serializableToolStripMenuItem});
			this.isolationLevelToolStripMenuItem.Name = "isolationLevelToolStripMenuItem";
			this.isolationLevelToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.isolationLevelToolStripMenuItem.Text = "Isolation Level";
			// 
			// readUncommittedToolStripMenuItem
			// 
			this.readUncommittedToolStripMenuItem.Name = "readUncommittedToolStripMenuItem";
			this.readUncommittedToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.readUncommittedToolStripMenuItem.Text = "Read Uncommitted";
			this.readUncommittedToolStripMenuItem.Click += new System.EventHandler(this.ReadUncommittedToolStripMenuItem_Click);
			// 
			// readCommittedToolStripMenuItem
			// 
			this.readCommittedToolStripMenuItem.Name = "readCommittedToolStripMenuItem";
			this.readCommittedToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.readCommittedToolStripMenuItem.Text = "Read Committed";
			this.readCommittedToolStripMenuItem.Click += new System.EventHandler(this.ReadCommittedToolStripMenuItem_Click);
			// 
			// repeatableReadToolStripMenuItem
			// 
			this.repeatableReadToolStripMenuItem.Name = "repeatableReadToolStripMenuItem";
			this.repeatableReadToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.repeatableReadToolStripMenuItem.Text = "Repeatable Read";
			this.repeatableReadToolStripMenuItem.Click += new System.EventHandler(this.RepeatableReadToolStripMenuItem_Click);
			// 
			// serializableToolStripMenuItem
			// 
			this.serializableToolStripMenuItem.Name = "serializableToolStripMenuItem";
			this.serializableToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.serializableToolStripMenuItem.Text = "Serializable";
			this.serializableToolStripMenuItem.Click += new System.EventHandler(this.SerializableToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
			// 
			// cutMenuItem1
			// 
			this.cutMenuItem1.Name = "cutMenuItem1";
			this.cutMenuItem1.ShortcutKeyDisplayString = "Ctrl+X";
			this.cutMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.cutMenuItem1.Text = "Cut";
			this.cutMenuItem1.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
			// 
			// copyMenuItem1
			// 
			this.copyMenuItem1.Name = "copyMenuItem1";
			this.copyMenuItem1.ShortcutKeyDisplayString = "Ctrl+C";
			this.copyMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.copyMenuItem1.Text = "Copy";
			this.copyMenuItem1.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
			// 
			// pasteMenuItem1
			// 
			this.pasteMenuItem1.Name = "pasteMenuItem1";
			this.pasteMenuItem1.ShortcutKeyDisplayString = "Ctrl+V";
			this.pasteMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.pasteMenuItem1.Text = "Paste";
			this.pasteMenuItem1.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
			// 
			// selectAllMenuItem1
			// 
			this.selectAllMenuItem1.Name = "selectAllMenuItem1";
			this.selectAllMenuItem1.ShortcutKeyDisplayString = "Ctrl+A";
			this.selectAllMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.selectAllMenuItem1.Text = "Select All";
			this.selectAllMenuItem1.Click += new System.EventHandler(this.SelectAllToolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(177, 6);
			// 
			// moveUpMenuItem1
			// 
			this.moveUpMenuItem1.Name = "moveUpMenuItem1";
			this.moveUpMenuItem1.ShortcutKeyDisplayString = "Ctrl+U";
			this.moveUpMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.moveUpMenuItem1.Text = "Move Up";
			this.moveUpMenuItem1.Click += new System.EventHandler(this.MoveUpToolStripMenuItem_Click);
			// 
			// moveDownMenuItem1
			// 
			this.moveDownMenuItem1.Name = "moveDownMenuItem1";
			this.moveDownMenuItem1.ShortcutKeyDisplayString = "Ctrl+D";
			this.moveDownMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.moveDownMenuItem1.Text = "Move Down";
			this.moveDownMenuItem1.Click += new System.EventHandler(this.MoveDownToolStripMenuItem_Click);
			// 
			// onlineToolStripMenuItem
			// 
			this.onlineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem});
			this.onlineToolStripMenuItem.Name = "onlineToolStripMenuItem";
			this.onlineToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
			this.onlineToolStripMenuItem.Text = "&Subscription";
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.drive_web_small;
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.settingsToolStripMenuItem.Text = "&Settings...";
			this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
			// 
			// checkForUpdatesToolStripMenuItem
			// 
			this.checkForUpdatesToolStripMenuItem.Enabled = false;
			this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
			this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.checkForUpdatesToolStripMenuItem.Text = "Check for &updates...";
			this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.CheckForUpdatesToolStripMenuItem_Click);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "xml";
			this.saveFileDialog1.Filter = "Xml files|*.xml|All files|*.*";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.DefaultExt = "xml";
			this.openFileDialog1.Filter = "Xml files|*.xml|All files|*.*";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem1,
            this.toolStripSeparator12,
            this.createMenuItem,
            this.editMenuItem,
            this.deleteMenuItem,
            this.toolStripSeparator14,
            this.changeMenuItem,
            this.toolStripSeparator8,
            this.cutMenuItem,
            this.copyMenuItem,
            this.pasteMenuItem,
            this.toolStripSeparator10,
            this.selectAllMenuItem,
            this.toolStripSeparator6,
            this.moveUpMenuItem,
            this.moveDownMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(181, 276);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
			// 
			// searchToolStripMenuItem1
			// 
			this.searchToolStripMenuItem1.Image = global::SQLEventAnalyzer.Properties.Resources.find_small;
			this.searchToolStripMenuItem1.Name = "searchToolStripMenuItem1";
			this.searchToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl + F";
			this.searchToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.searchToolStripMenuItem1.Text = "Search...";
			this.searchToolStripMenuItem1.Click += new System.EventHandler(this.SearchToolStripMenuItem1_Click);
			// 
			// toolStripSeparator12
			// 
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(177, 6);
			// 
			// createMenuItem
			// 
			this.createMenuItem.Name = "createMenuItem";
			this.createMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
			this.createMenuItem.Size = new System.Drawing.Size(180, 22);
			this.createMenuItem.Text = "Create";
			this.createMenuItem.Click += new System.EventHandler(this.CreateMenuItem_Click);
			// 
			// editMenuItem
			// 
			this.editMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.columns_small;
			this.editMenuItem.Name = "editMenuItem";
			this.editMenuItem.ShortcutKeyDisplayString = "Enter";
			this.editMenuItem.Size = new System.Drawing.Size(180, 22);
			this.editMenuItem.Text = "Edit";
			this.editMenuItem.Click += new System.EventHandler(this.EditMenuItem_Click);
			// 
			// deleteMenuItem
			// 
			this.deleteMenuItem.Name = "deleteMenuItem";
			this.deleteMenuItem.ShortcutKeyDisplayString = "Del";
			this.deleteMenuItem.Size = new System.Drawing.Size(180, 22);
			this.deleteMenuItem.Text = "Delete";
			this.deleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
			// 
			// toolStripSeparator14
			// 
			this.toolStripSeparator14.Name = "toolStripSeparator14";
			this.toolStripSeparator14.Size = new System.Drawing.Size(177, 6);
			// 
			// changeMenuItem
			// 
			this.changeMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleHiddenMenuItem,
            this.hiddenMenuItem,
            this.shownMenuItem,
            this.toolStripSeparator16,
            this.toggleActiveMenuItem,
            this.activeMenuItem,
            this.inactiveMenuItem,
            this.toolStripSeparator18,
            this.isolationLevelToolStripMenuItem1});
			this.changeMenuItem.Name = "changeMenuItem";
			this.changeMenuItem.Size = new System.Drawing.Size(180, 22);
			this.changeMenuItem.Text = "Change";
			// 
			// toggleHiddenMenuItem
			// 
			this.toggleHiddenMenuItem.Name = "toggleHiddenMenuItem";
			this.toggleHiddenMenuItem.ShortcutKeyDisplayString = "Ctrl+H";
			this.toggleHiddenMenuItem.Size = new System.Drawing.Size(196, 22);
			this.toggleHiddenMenuItem.Text = "Toggle Hidden";
			this.toggleHiddenMenuItem.Click += new System.EventHandler(this.ToggleHiddenMenuItem_Click);
			// 
			// hiddenMenuItem
			// 
			this.hiddenMenuItem.Name = "hiddenMenuItem";
			this.hiddenMenuItem.Size = new System.Drawing.Size(196, 22);
			this.hiddenMenuItem.Text = "Hidden";
			this.hiddenMenuItem.Click += new System.EventHandler(this.HiddenMenuItem_Click);
			// 
			// shownMenuItem
			// 
			this.shownMenuItem.Name = "shownMenuItem";
			this.shownMenuItem.Size = new System.Drawing.Size(196, 22);
			this.shownMenuItem.Text = "Shown";
			this.shownMenuItem.Click += new System.EventHandler(this.ShownMenuItem_Click);
			// 
			// toolStripSeparator16
			// 
			this.toolStripSeparator16.Name = "toolStripSeparator16";
			this.toolStripSeparator16.Size = new System.Drawing.Size(193, 6);
			// 
			// toggleActiveMenuItem
			// 
			this.toggleActiveMenuItem.Name = "toggleActiveMenuItem";
			this.toggleActiveMenuItem.ShortcutKeyDisplayString = "Ctrl+E";
			this.toggleActiveMenuItem.Size = new System.Drawing.Size(196, 22);
			this.toggleActiveMenuItem.Text = "Toggle Enabled";
			this.toggleActiveMenuItem.Click += new System.EventHandler(this.ToggleActiveMenuItem_Click);
			// 
			// activeMenuItem
			// 
			this.activeMenuItem.Name = "activeMenuItem";
			this.activeMenuItem.Size = new System.Drawing.Size(196, 22);
			this.activeMenuItem.Text = "Enabled";
			this.activeMenuItem.Click += new System.EventHandler(this.ActiveMenuItem_Click);
			// 
			// inactiveMenuItem
			// 
			this.inactiveMenuItem.Name = "inactiveMenuItem";
			this.inactiveMenuItem.Size = new System.Drawing.Size(196, 22);
			this.inactiveMenuItem.Text = "Disabled";
			this.inactiveMenuItem.Click += new System.EventHandler(this.InactiveMenuItem_Click);
			// 
			// toolStripSeparator18
			// 
			this.toolStripSeparator18.Name = "toolStripSeparator18";
			this.toolStripSeparator18.Size = new System.Drawing.Size(193, 6);
			// 
			// isolationLevelToolStripMenuItem1
			// 
			this.isolationLevelToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readUncommittedToolStripMenuItem1,
            this.readCommittedToolStripMenuItem1,
            this.repeatableReadToolStripMenuItem1,
            this.serializableToolStripMenuItem1});
			this.isolationLevelToolStripMenuItem1.Name = "isolationLevelToolStripMenuItem1";
			this.isolationLevelToolStripMenuItem1.Size = new System.Drawing.Size(196, 22);
			this.isolationLevelToolStripMenuItem1.Text = "Isolation Level";
			// 
			// readUncommittedToolStripMenuItem1
			// 
			this.readUncommittedToolStripMenuItem1.Name = "readUncommittedToolStripMenuItem1";
			this.readUncommittedToolStripMenuItem1.Size = new System.Drawing.Size(177, 22);
			this.readUncommittedToolStripMenuItem1.Text = "Read Uncommitted";
			this.readUncommittedToolStripMenuItem1.Click += new System.EventHandler(this.ReadUncommittedToolStripMenuItem1_Click);
			// 
			// readCommittedToolStripMenuItem1
			// 
			this.readCommittedToolStripMenuItem1.Name = "readCommittedToolStripMenuItem1";
			this.readCommittedToolStripMenuItem1.Size = new System.Drawing.Size(177, 22);
			this.readCommittedToolStripMenuItem1.Text = "Read Committed";
			this.readCommittedToolStripMenuItem1.Click += new System.EventHandler(this.ReadCommittedToolStripMenuItem1_Click);
			// 
			// repeatableReadToolStripMenuItem1
			// 
			this.repeatableReadToolStripMenuItem1.Name = "repeatableReadToolStripMenuItem1";
			this.repeatableReadToolStripMenuItem1.Size = new System.Drawing.Size(177, 22);
			this.repeatableReadToolStripMenuItem1.Text = "Repeatable Read";
			this.repeatableReadToolStripMenuItem1.Click += new System.EventHandler(this.RepeatableReadToolStripMenuItem1_Click);
			// 
			// serializableToolStripMenuItem1
			// 
			this.serializableToolStripMenuItem1.Name = "serializableToolStripMenuItem1";
			this.serializableToolStripMenuItem1.Size = new System.Drawing.Size(177, 22);
			this.serializableToolStripMenuItem1.Text = "Serializable";
			this.serializableToolStripMenuItem1.Click += new System.EventHandler(this.SerializableToolStripMenuItem1_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(177, 6);
			// 
			// cutMenuItem
			// 
			this.cutMenuItem.Name = "cutMenuItem";
			this.cutMenuItem.ShortcutKeyDisplayString = "Ctrl+X";
			this.cutMenuItem.Size = new System.Drawing.Size(180, 22);
			this.cutMenuItem.Text = "Cut";
			this.cutMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItem1_Click);
			// 
			// copyMenuItem
			// 
			this.copyMenuItem.Name = "copyMenuItem";
			this.copyMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
			this.copyMenuItem.Size = new System.Drawing.Size(180, 22);
			this.copyMenuItem.Text = "Copy";
			this.copyMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem1_Click);
			// 
			// pasteMenuItem
			// 
			this.pasteMenuItem.Name = "pasteMenuItem";
			this.pasteMenuItem.ShortcutKeyDisplayString = "Ctrl+V";
			this.pasteMenuItem.Size = new System.Drawing.Size(180, 22);
			this.pasteMenuItem.Text = "Paste";
			this.pasteMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem1_Click);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(177, 6);
			// 
			// selectAllMenuItem
			// 
			this.selectAllMenuItem.Name = "selectAllMenuItem";
			this.selectAllMenuItem.ShortcutKeyDisplayString = "Ctrl+A";
			this.selectAllMenuItem.Size = new System.Drawing.Size(180, 22);
			this.selectAllMenuItem.Text = "Select All";
			this.selectAllMenuItem.Click += new System.EventHandler(this.SelectAllToolStripMenuItem1_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(177, 6);
			// 
			// moveUpMenuItem
			// 
			this.moveUpMenuItem.Name = "moveUpMenuItem";
			this.moveUpMenuItem.ShortcutKeyDisplayString = "Ctrl+U";
			this.moveUpMenuItem.Size = new System.Drawing.Size(180, 22);
			this.moveUpMenuItem.Text = "Move Up";
			this.moveUpMenuItem.Click += new System.EventHandler(this.MoveUpMenuItem_Click);
			// 
			// moveDownMenuItem
			// 
			this.moveDownMenuItem.Name = "moveDownMenuItem";
			this.moveDownMenuItem.ShortcutKeyDisplayString = "Ctrl+D";
			this.moveDownMenuItem.Size = new System.Drawing.Size(180, 22);
			this.moveDownMenuItem.Text = "Move Down";
			this.moveDownMenuItem.Click += new System.EventHandler(this.MoveDownMenuItem_Click);
			// 
			// parametersGroupBox
			// 
			this.parametersGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.parametersGroupBox.Controls.Add(this.parametersDataGridView);
			this.parametersGroupBox.Location = new System.Drawing.Point(12, 483);
			this.parametersGroupBox.Name = "parametersGroupBox";
			this.parametersGroupBox.Size = new System.Drawing.Size(860, 119);
			this.parametersGroupBox.TabIndex = 1;
			this.parametersGroupBox.TabStop = false;
			this.parametersGroupBox.Text = "Parameters";
			// 
			// parametersDataGridView
			// 
			this.parametersDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.parametersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.parametersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.ValueColumn});
			this.parametersDataGridView.ContextMenuStrip = this.contextMenuStrip2;
			this.parametersDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.parametersDataGridView.Location = new System.Drawing.Point(6, 19);
			this.parametersDataGridView.Name = "parametersDataGridView";
			this.parametersDataGridView.Size = new System.Drawing.Size(848, 94);
			this.parametersDataGridView.TabIndex = 4;
			this.parametersDataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ParametersDataGridView_MouseDown);
			// 
			// NameColumn
			// 
			this.NameColumn.HeaderText = "Name";
			this.NameColumn.Name = "NameColumn";
			// 
			// ValueColumn
			// 
			this.ValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ValueColumn.HeaderText = "Value";
			this.ValueColumn.Name = "ValueColumn";
			// 
			// contextMenuStrip2
			// 
			this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteSelectedToolStripMenuItem});
			this.contextMenuStrip2.Name = "contextMenuStrip2";
			this.contextMenuStrip2.Size = new System.Drawing.Size(154, 26);
			// 
			// deleteSelectedToolStripMenuItem
			// 
			this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
			this.deleteSelectedToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.deleteSelectedToolStripMenuItem.Text = "Delete selected";
			this.deleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.DeleteSelectedToolStripMenuItem_Click);
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
			// ManageColumnsForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.okButton;
			this.ClientSize = new System.Drawing.Size(884, 641);
			this.Controls.Add(this.restartToUpdateTextBox);
			this.Controls.Add(this.parametersGroupBox);
			this.Controls.Add(this.columnsGroupBox);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.menuStrip1);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(666, 394);
			this.Name = "ManageColumnsForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Custom Columns";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManageColumnsForm_FormClosing);
			this.Resize += new System.EventHandler(this.ManageColumnsForm_Resize);
			this.columnsGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.columnsDataGridView)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.parametersGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.parametersDataGridView)).EndInit();
			this.contextMenuStrip2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Button okButton;
	private System.Windows.Forms.GroupBox columnsGroupBox;
	private System.Windows.Forms.Button moveDownButton;
	private System.Windows.Forms.Button moveUpButton;
	private System.Windows.Forms.Button deleteButton;
	private System.Windows.Forms.Button createButton;
	private System.Windows.Forms.Button editButton;
	private System.Windows.Forms.MenuStrip menuStrip1;
	private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
	private System.Windows.Forms.SaveFileDialog saveFileDialog1;
	private System.Windows.Forms.OpenFileDialog openFileDialog1;
	private System.Windows.Forms.DataGridView columnsDataGridView;
	private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem createMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem editMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem deleteMenuItem1;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
	private System.Windows.Forms.ToolStripMenuItem moveUpMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem moveDownMenuItem1;
	private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
	private System.Windows.Forms.ToolStripMenuItem createMenuItem;
	private System.Windows.Forms.ToolStripMenuItem editMenuItem;
	private System.Windows.Forms.ToolStripMenuItem deleteMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
	private System.Windows.Forms.ToolStripMenuItem moveUpMenuItem;
	private System.Windows.Forms.ToolStripMenuItem moveDownMenuItem;
	private System.Windows.Forms.ToolStripMenuItem copyMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem pasteMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem copyMenuItem;
	private System.Windows.Forms.ToolStripMenuItem pasteMenuItem;
	private System.Windows.Forms.ToolStripMenuItem cutMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem cutMenuItem;
	private System.Windows.Forms.ToolStripMenuItem selectAllMenuItem1;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
	private System.Windows.Forms.ToolStripMenuItem selectAllMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
	private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
	private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem1;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
	private System.Windows.Forms.ToolStripMenuItem changeMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem hiddenMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem activeMenuItem1;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
	private System.Windows.Forms.ToolStripMenuItem changeMenuItem;
	private System.Windows.Forms.ToolStripMenuItem hiddenMenuItem;
	private System.Windows.Forms.ToolStripMenuItem activeMenuItem;
	private System.Windows.Forms.ToolStripMenuItem shownMenuItem1;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
	private System.Windows.Forms.ToolStripMenuItem inactiveMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem shownMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
	private System.Windows.Forms.ToolStripMenuItem inactiveMenuItem;
	private System.Windows.Forms.ToolStripMenuItem toggleHiddenMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem toggleActiveMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem toggleHiddenMenuItem;
	private System.Windows.Forms.ToolStripMenuItem toggleActiveMenuItem;
	private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
	private System.Windows.Forms.GroupBox parametersGroupBox;
	private System.Windows.Forms.DataGridView parametersDataGridView;
	private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
	private System.Windows.Forms.DataGridViewTextBoxColumn ValueColumn;
	private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
	private System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem newSessionToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
	private System.Windows.Forms.ToolStripMenuItem loadColumnSetToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	private System.Windows.Forms.DataGridViewImageColumn ImageColumn;
	private System.Windows.Forms.DataGridViewTextBoxColumn NumberColumn;
	private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
	private System.Windows.Forms.DataGridViewTextBoxColumn InputTypeColumn;
	private System.Windows.Forms.DataGridViewTextBoxColumn OutputTypeColumn;
	private System.Windows.Forms.DataGridViewCheckBoxColumn HiddenColumn;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
	private System.Windows.Forms.ToolStripMenuItem isolationLevelToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem readUncommittedToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem readCommittedToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem repeatableReadToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem serializableToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
	private System.Windows.Forms.ToolStripMenuItem isolationLevelToolStripMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem readUncommittedToolStripMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem readCommittedToolStripMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem repeatableReadToolStripMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem serializableToolStripMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem onlineToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
	private System.Windows.Forms.TextBox restartToUpdateTextBox;
}
