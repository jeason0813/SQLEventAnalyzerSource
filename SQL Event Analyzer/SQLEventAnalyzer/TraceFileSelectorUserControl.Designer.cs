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

partial class TraceFileSelectorUserControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TraceFileSelectorUserControl));
			this.traceFileCountLabel = new System.Windows.Forms.Label();
			this.nextButton = new System.Windows.Forms.Button();
			this.traceFilesGroupBox = new System.Windows.Forms.GroupBox();
			this.traceFileListView = new System.Windows.Forms.ListView();
			this.imageColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.numberColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.fileNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.sizeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.dateCreatedColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.importCheckedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.markSelectedAndContinueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyNameToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.traceFileDirectoryGroupBox = new System.Windows.Forms.GroupBox();
			this.loadButton = new System.Windows.Forms.Button();
			this.selectTraceFileDirButton = new System.Windows.Forms.Button();
			this.traceFileDirDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.traceFileDirComboBox = new ComboBoxCustom();
			this.traceFilesGroupBox.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.traceFileDirectoryGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// traceFileCountLabel
			// 
			this.traceFileCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.traceFileCountLabel.AutoSize = true;
			this.traceFileCountLabel.Location = new System.Drawing.Point(9, 258);
			this.traceFileCountLabel.Name = "traceFileCountLabel";
			this.traceFileCountLabel.Size = new System.Drawing.Size(114, 13);
			this.traceFileCountLabel.TabIndex = 9;
			this.traceFileCountLabel.Text = "Number of Trace Files:";
			// 
			// nextButton
			// 
			this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.nextButton.Enabled = false;
			this.nextButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.nextButton.Location = new System.Drawing.Point(415, 252);
			this.nextButton.Name = "nextButton";
			this.nextButton.Size = new System.Drawing.Size(75, 24);
			this.nextButton.TabIndex = 5;
			this.nextButton.Text = "Import ->";
			this.nextButton.UseVisualStyleBackColor = true;
			this.nextButton.Click += new System.EventHandler(this.NextButton_Click);
			// 
			// traceFilesGroupBox
			// 
			this.traceFilesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.traceFilesGroupBox.Controls.Add(this.traceFileListView);
			this.traceFilesGroupBox.Location = new System.Drawing.Point(12, 57);
			this.traceFilesGroupBox.Name = "traceFilesGroupBox";
			this.traceFilesGroupBox.Size = new System.Drawing.Size(477, 187);
			this.traceFilesGroupBox.TabIndex = 4;
			this.traceFilesGroupBox.TabStop = false;
			this.traceFilesGroupBox.Text = "Import Trace Files";
			// 
			// traceFileListView
			// 
			this.traceFileListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.traceFileListView.CheckBoxes = true;
			this.traceFileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.imageColumnHeader,
            this.numberColumnHeader,
            this.fileNameColumnHeader,
            this.sizeColumnHeader,
            this.dateCreatedColumnHeader});
			this.traceFileListView.ContextMenuStrip = this.contextMenuStrip1;
			this.traceFileListView.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.traceFileListView.FullRowSelect = true;
			this.traceFileListView.Location = new System.Drawing.Point(6, 19);
			this.traceFileListView.Name = "traceFileListView";
			this.traceFileListView.Size = new System.Drawing.Size(465, 162);
			this.traceFileListView.SmallImageList = this.imageList1;
			this.traceFileListView.TabIndex = 4;
			this.traceFileListView.UseCompatibleStateImageBehavior = false;
			this.traceFileListView.View = System.Windows.Forms.View.Details;
			this.traceFileListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.TraceFileListView_ColumnClick);
			this.traceFileListView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.TraceFileListView_ItemCheck);
			this.traceFileListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.TraceFileListView_ItemChecked);
			this.traceFileListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TraceFileListView_KeyDown);
			// 
			// imageColumnHeader
			// 
			this.imageColumnHeader.Text = "";
			this.imageColumnHeader.Width = 40;
			// 
			// numberColumnHeader
			// 
			this.numberColumnHeader.Text = "#";
			this.numberColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numberColumnHeader.Width = 70;
			// 
			// fileNameColumnHeader
			// 
			this.fileNameColumnHeader.Text = "File Name";
			this.fileNameColumnHeader.Width = 370;
			// 
			// sizeColumnHeader
			// 
			this.sizeColumnHeader.Text = "Size (KB)";
			this.sizeColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.sizeColumnHeader.Width = 158;
			// 
			// dateCreatedColumnHeader
			// 
			this.dateCreatedColumnHeader.Text = "Date Created";
			this.dateCreatedColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.dateCreatedColumnHeader.Width = 160;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importCheckedToolStripMenuItem,
            this.markSelectedAndContinueToolStripMenuItem,
            this.deleteSelectedToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.copyNameToClipboardToolStripMenuItem,
            this.renameToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(284, 136);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
			// 
			// importCheckedToolStripMenuItem
			// 
			this.importCheckedToolStripMenuItem.Name = "importCheckedToolStripMenuItem";
			this.importCheckedToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
			this.importCheckedToolStripMenuItem.Text = "Import checked (delete current Session)";
			this.importCheckedToolStripMenuItem.Click += new System.EventHandler(this.ImportCheckedToolStripMenuItem_Click);
			// 
			// markSelectedAndContinueToolStripMenuItem
			// 
			this.markSelectedAndContinueToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.markSelectedAndContinueToolStripMenuItem.Name = "markSelectedAndContinueToolStripMenuItem";
			this.markSelectedAndContinueToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
			this.markSelectedAndContinueToolStripMenuItem.Text = "Import selected (delete current Session)";
			this.markSelectedAndContinueToolStripMenuItem.Click += new System.EventHandler(this.MarkSelectedAndContinueToolStripMenuItem_Click);
			// 
			// deleteSelectedToolStripMenuItem
			// 
			this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
			this.deleteSelectedToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.deleteSelectedToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
			this.deleteSelectedToolStripMenuItem.Text = "Delete selected";
			this.deleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.DeleteSelectedToolStripMenuItem_Click);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+A";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
			this.selectAllToolStripMenuItem.Text = "Select All";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllToolStripMenuItem_Click);
			// 
			// copyNameToClipboardToolStripMenuItem
			// 
			this.copyNameToClipboardToolStripMenuItem.Name = "copyNameToClipboardToolStripMenuItem";
			this.copyNameToClipboardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyNameToClipboardToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
			this.copyNameToClipboardToolStripMenuItem.Text = "Copy name to Clipboard";
			this.copyNameToClipboardToolStripMenuItem.Click += new System.EventHandler(this.CopyNameToClipboardToolStripMenuItem_Click);
			// 
			// renameToolStripMenuItem
			// 
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			this.renameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.renameToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
			this.renameToolStripMenuItem.Text = "Rename";
			this.renameToolStripMenuItem.Click += new System.EventHandler(this.RenameToolStripMenuItem_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "Profiler.gif");
			this.imageList1.Images.SetKeyName(1, "XEvent.png");
			// 
			// traceFileDirectoryGroupBox
			// 
			this.traceFileDirectoryGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.traceFileDirectoryGroupBox.Controls.Add(this.traceFileDirComboBox);
			this.traceFileDirectoryGroupBox.Controls.Add(this.loadButton);
			this.traceFileDirectoryGroupBox.Controls.Add(this.selectTraceFileDirButton);
			this.traceFileDirectoryGroupBox.Location = new System.Drawing.Point(12, 3);
			this.traceFileDirectoryGroupBox.Name = "traceFileDirectoryGroupBox";
			this.traceFileDirectoryGroupBox.Size = new System.Drawing.Size(477, 48);
			this.traceFileDirectoryGroupBox.TabIndex = 1;
			this.traceFileDirectoryGroupBox.TabStop = false;
			this.traceFileDirectoryGroupBox.Text = "Import Path";
			// 
			// loadButton
			// 
			this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.loadButton.Location = new System.Drawing.Point(400, 19);
			this.loadButton.Name = "loadButton";
			this.loadButton.Size = new System.Drawing.Size(41, 23);
			this.loadButton.TabIndex = 2;
			this.loadButton.Text = "Load";
			this.loadButton.UseVisualStyleBackColor = true;
			this.loadButton.Click += new System.EventHandler(this.LoadButton_Click);
			// 
			// selectTraceFileDirButton
			// 
			this.selectTraceFileDirButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.selectTraceFileDirButton.Location = new System.Drawing.Point(447, 19);
			this.selectTraceFileDirButton.Name = "selectTraceFileDirButton";
			this.selectTraceFileDirButton.Size = new System.Drawing.Size(24, 23);
			this.selectTraceFileDirButton.TabIndex = 3;
			this.selectTraceFileDirButton.Text = "...";
			this.selectTraceFileDirButton.UseVisualStyleBackColor = true;
			this.selectTraceFileDirButton.Click += new System.EventHandler(this.SelectTraceFileDirButton_Click);
			// 
			// traceFileDirDialog
			// 
			this.traceFileDirDialog.ShowNewFolderButton = false;
			// 
			// traceFileDirComboBox
			// 
			this.traceFileDirComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.traceFileDirComboBox.FormattingEnabled = true;
			this.traceFileDirComboBox.Location = new System.Drawing.Point(6, 20);
			this.traceFileDirComboBox.Name = "traceFileDirComboBox";
			this.traceFileDirComboBox.Size = new System.Drawing.Size(388, 21);
			this.traceFileDirComboBox.TabIndex = 1;
			this.traceFileDirComboBox.Enter += new System.EventHandler(this.TraceFileDirComboBox_Enter);
			this.traceFileDirComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TraceFileDirComboBox_KeyDown);
			// 
			// TraceFileSelectorUserControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.traceFileCountLabel);
			this.Controls.Add(this.nextButton);
			this.Controls.Add(this.traceFilesGroupBox);
			this.Controls.Add(this.traceFileDirectoryGroupBox);
			this.DoubleBuffered = true;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "TraceFileSelectorUserControl";
			this.Size = new System.Drawing.Size(501, 283);
			this.Resize += new System.EventHandler(this.TraceFileSelectorUserControl_Resize);
			this.traceFilesGroupBox.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.traceFileDirectoryGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Label traceFileCountLabel;
	private System.Windows.Forms.Button nextButton;
	private System.Windows.Forms.GroupBox traceFilesGroupBox;
	private System.Windows.Forms.ListView traceFileListView;
	private System.Windows.Forms.ColumnHeader fileNameColumnHeader;
	private System.Windows.Forms.ColumnHeader dateCreatedColumnHeader;
	private System.Windows.Forms.GroupBox traceFileDirectoryGroupBox;
	private System.Windows.Forms.Button selectTraceFileDirButton;
	private System.Windows.Forms.FolderBrowserDialog traceFileDirDialog;
	private System.Windows.Forms.ImageList imageList1;
	private System.Windows.Forms.Button loadButton;
	private System.Windows.Forms.ColumnHeader sizeColumnHeader;
	private System.Windows.Forms.ColumnHeader numberColumnHeader;
	private System.Windows.Forms.ColumnHeader imageColumnHeader;
	private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
	private System.Windows.Forms.ToolStripMenuItem markSelectedAndContinueToolStripMenuItem;
	private ComboBoxCustom traceFileDirComboBox;
	private System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem copyNameToClipboardToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem importCheckedToolStripMenuItem;
}
