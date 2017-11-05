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

partial class SessionSelectorUserControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SessionSelectorUserControl));
			this.sessionCountLabel = new System.Windows.Forms.Label();
			this.nextButton = new System.Windows.Forms.Button();
			this.sessionsGroupBox = new System.Windows.Forms.GroupBox();
			this.sessionListView = new System.Windows.Forms.ListView();
			this.imageColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.numberColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.sessionIdColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.sizeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.dateCreatedColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.markSelectedAndContinueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyNameToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.useSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.sessionsInfoGroupBox = new System.Windows.Forms.GroupBox();
			this.newButton = new System.Windows.Forms.Button();
			this.existingSessionsComboBox = new ComboBoxCustom();
			this.okButton = new System.Windows.Forms.Button();
			this.importCheckedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sessionsGroupBox.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.sessionsInfoGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// sessionCountLabel
			// 
			this.sessionCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.sessionCountLabel.AutoSize = true;
			this.sessionCountLabel.Location = new System.Drawing.Point(9, 258);
			this.sessionCountLabel.Name = "sessionCountLabel";
			this.sessionCountLabel.Size = new System.Drawing.Size(104, 13);
			this.sessionCountLabel.TabIndex = 9;
			this.sessionCountLabel.Text = "Number of Sessions:";
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
			// sessionsGroupBox
			// 
			this.sessionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sessionsGroupBox.Controls.Add(this.sessionListView);
			this.sessionsGroupBox.Location = new System.Drawing.Point(12, 57);
			this.sessionsGroupBox.Name = "sessionsGroupBox";
			this.sessionsGroupBox.Size = new System.Drawing.Size(477, 187);
			this.sessionsGroupBox.TabIndex = 4;
			this.sessionsGroupBox.TabStop = false;
			this.sessionsGroupBox.Text = "Import Sessions";
			// 
			// sessionListView
			// 
			this.sessionListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sessionListView.CheckBoxes = true;
			this.sessionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.imageColumnHeader,
            this.numberColumnHeader,
            this.sessionIdColumnHeader,
            this.sizeColumnHeader,
            this.dateCreatedColumnHeader});
			this.sessionListView.ContextMenuStrip = this.contextMenuStrip1;
			this.sessionListView.Font = new System.Drawing.Font("Consolas", 9F);
			this.sessionListView.FullRowSelect = true;
			this.sessionListView.Location = new System.Drawing.Point(6, 19);
			this.sessionListView.Name = "sessionListView";
			this.sessionListView.Size = new System.Drawing.Size(465, 162);
			this.sessionListView.SmallImageList = this.imageList1;
			this.sessionListView.TabIndex = 4;
			this.sessionListView.UseCompatibleStateImageBehavior = false;
			this.sessionListView.View = System.Windows.Forms.View.Details;
			this.sessionListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.SessionListView_ColumnClick);
			this.sessionListView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.SessionListView_ItemCheck);
			this.sessionListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.SessionListView_ItemChecked);
			this.sessionListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SessionListView_KeyDown);
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
			// sessionIdColumnHeader
			// 
			this.sessionIdColumnHeader.Text = "Session Id";
			this.sessionIdColumnHeader.Width = 370;
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
            this.renameToolStripMenuItem,
            this.toolStripSeparator1,
            this.useSessionToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(306, 186);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
			// 
			// markSelectedAndContinueToolStripMenuItem
			// 
			this.markSelectedAndContinueToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.markSelectedAndContinueToolStripMenuItem.Name = "markSelectedAndContinueToolStripMenuItem";
			this.markSelectedAndContinueToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
			this.markSelectedAndContinueToolStripMenuItem.Text = "Import selected (append to current Session)";
			this.markSelectedAndContinueToolStripMenuItem.Click += new System.EventHandler(this.MarkSelectedAndContinueToolStripMenuItem_Click);
			// 
			// deleteSelectedToolStripMenuItem
			// 
			this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
			this.deleteSelectedToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.deleteSelectedToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
			this.deleteSelectedToolStripMenuItem.Text = "Delete selected";
			this.deleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.DeleteSelectedToolStripMenuItem_Click);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+A";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
			this.selectAllToolStripMenuItem.Text = "Select All";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllToolStripMenuItem_Click);
			// 
			// copyNameToClipboardToolStripMenuItem
			// 
			this.copyNameToClipboardToolStripMenuItem.Name = "copyNameToClipboardToolStripMenuItem";
			this.copyNameToClipboardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyNameToClipboardToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
			this.copyNameToClipboardToolStripMenuItem.Text = "Copy name to Clipboard";
			this.copyNameToClipboardToolStripMenuItem.Click += new System.EventHandler(this.CopyNameToClipboardToolStripMenuItem_Click);
			// 
			// renameToolStripMenuItem
			// 
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			this.renameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.renameToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
			this.renameToolStripMenuItem.Text = "Rename";
			this.renameToolStripMenuItem.Click += new System.EventHandler(this.RenameToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(302, 6);
			// 
			// useSessionToolStripMenuItem
			// 
			this.useSessionToolStripMenuItem.Name = "useSessionToolStripMenuItem";
			this.useSessionToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
			this.useSessionToolStripMenuItem.Text = "Use Session";
			this.useSessionToolStripMenuItem.Click += new System.EventHandler(this.UseSessionToolStripMenuItem_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "database.png");
			// 
			// sessionsInfoGroupBox
			// 
			this.sessionsInfoGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sessionsInfoGroupBox.Controls.Add(this.newButton);
			this.sessionsInfoGroupBox.Controls.Add(this.existingSessionsComboBox);
			this.sessionsInfoGroupBox.Controls.Add(this.okButton);
			this.sessionsInfoGroupBox.Location = new System.Drawing.Point(12, 3);
			this.sessionsInfoGroupBox.Name = "sessionsInfoGroupBox";
			this.sessionsInfoGroupBox.Size = new System.Drawing.Size(477, 48);
			this.sessionsInfoGroupBox.TabIndex = 1;
			this.sessionsInfoGroupBox.TabStop = false;
			this.sessionsInfoGroupBox.Text = "Use Session";
			// 
			// newButton
			// 
			this.newButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.newButton.Location = new System.Drawing.Point(430, 19);
			this.newButton.Name = "newButton";
			this.newButton.Size = new System.Drawing.Size(41, 23);
			this.newButton.TabIndex = 3;
			this.newButton.Text = "New";
			this.newButton.UseVisualStyleBackColor = true;
			this.newButton.Click += new System.EventHandler(this.NewButton_Click);
			// 
			// existingSessionsComboBox
			// 
			this.existingSessionsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.existingSessionsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.existingSessionsComboBox.Font = new System.Drawing.Font("Consolas", 8.5F);
			this.existingSessionsComboBox.FormattingEnabled = true;
			this.existingSessionsComboBox.Location = new System.Drawing.Point(6, 20);
			this.existingSessionsComboBox.Name = "existingSessionsComboBox";
			this.existingSessionsComboBox.Size = new System.Drawing.Size(371, 21);
			this.existingSessionsComboBox.TabIndex = 1;
			this.existingSessionsComboBox.SelectedIndexChanged += new System.EventHandler(this.ExistingSessionsComboBox_SelectedIndexChanged);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(383, 19);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(41, 23);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// importCheckedToolStripMenuItem
			// 
			this.importCheckedToolStripMenuItem.Name = "importCheckedToolStripMenuItem";
			this.importCheckedToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
			this.importCheckedToolStripMenuItem.Text = "Import checked (append to current Session)";
			this.importCheckedToolStripMenuItem.Click += new System.EventHandler(this.ImportCheckedToolStripMenuItem_Click);
			// 
			// SessionSelectorUserControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.sessionsInfoGroupBox);
			this.Controls.Add(this.sessionCountLabel);
			this.Controls.Add(this.nextButton);
			this.Controls.Add(this.sessionsGroupBox);
			this.DoubleBuffered = true;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "SessionSelectorUserControl";
			this.Size = new System.Drawing.Size(501, 283);
			this.Resize += new System.EventHandler(this.SessionSelectorUserControl_Resize);
			this.sessionsGroupBox.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.sessionsInfoGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Label sessionCountLabel;
	private System.Windows.Forms.Button nextButton;
	private System.Windows.Forms.GroupBox sessionsGroupBox;
	private System.Windows.Forms.ListView sessionListView;
	private System.Windows.Forms.ColumnHeader sessionIdColumnHeader;
	private System.Windows.Forms.ColumnHeader dateCreatedColumnHeader;
	private System.Windows.Forms.ImageList imageList1;
	private System.Windows.Forms.ColumnHeader sizeColumnHeader;
	private System.Windows.Forms.ColumnHeader numberColumnHeader;
	private System.Windows.Forms.ColumnHeader imageColumnHeader;
	private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
	private System.Windows.Forms.ToolStripMenuItem markSelectedAndContinueToolStripMenuItem;
	private System.Windows.Forms.GroupBox sessionsInfoGroupBox;
	private ComboBoxCustom existingSessionsComboBox;
	private System.Windows.Forms.Button okButton;
	private System.Windows.Forms.ToolStripMenuItem useSessionToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	private System.Windows.Forms.Button newButton;
	private System.Windows.Forms.ToolStripMenuItem copyNameToClipboardToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem importCheckedToolStripMenuItem;
}
