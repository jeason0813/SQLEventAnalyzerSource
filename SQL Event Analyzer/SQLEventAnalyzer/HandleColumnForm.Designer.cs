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

partial class HandleColumnForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HandleColumnForm));
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.nameLabel = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.outputGroupBox = new System.Windows.Forms.GroupBox();
			this.clrNotAvailableOutputLabel = new System.Windows.Forms.Label();
			this.outputTypeLabel = new System.Windows.Forms.Label();
			this.outputTypeComboBox = new ComboBoxCustom();
			this.outputTextEditorControl = new ICSharpCode.TextEditor.TextEditorControl();
			this.outputContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.insertToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.outputToolStripMenuItemUndo = new System.Windows.Forms.ToolStripMenuItem();
			this.outputToolStripMenuItemRedo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.outputToolStripMenuItemCut = new System.Windows.Forms.ToolStripMenuItem();
			this.outputToolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.outputToolStripMenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.outputToolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.outputToolStripMenuItemSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.inputContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.searchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.insertToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.inputToolStripMenuItemUndo = new System.Windows.Forms.ToolStripMenuItem();
			this.inputToolStripMenuItemRedo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.inputToolStripMenuItemCut = new System.Windows.Forms.ToolStripMenuItem();
			this.inputToolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.inputToolStripMenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.inputToolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.inputToolStripMenuItemSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.inputGroupBox = new System.Windows.Forms.GroupBox();
			this.clrNotAvailableIntputLabel = new System.Windows.Forms.Label();
			this.inputTypeLabel = new System.Windows.Forms.Label();
			this.inputTypeComboBox = new ComboBoxCustom();
			this.inputTextEditorControl = new ICSharpCode.TextEditor.TextEditorControl();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.hiddenCheckBox = new System.Windows.Forms.CheckBox();
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.searchToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.insertParameterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.formatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enabledCheckBox = new System.Windows.Forms.CheckBox();
			this.widthLabel = new System.Windows.Forms.Label();
			this.widthTextBox = new System.Windows.Forms.TextBox();
			this.isolationLevelComboBox = new ComboBoxCustom();
			this.isolationLevelLabel = new System.Windows.Forms.Label();
			this.outputGroupBox.SuspendLayout();
			this.outputContextMenuStrip.SuspendLayout();
			this.inputContextMenuStrip.SuspendLayout();
			this.inputGroupBox.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// nameTextBox
			// 
			this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nameTextBox.Location = new System.Drawing.Point(56, 31);
			this.nameTextBox.MaxLength = 120;
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(344, 20);
			this.nameTextBox.TabIndex = 0;
			this.nameTextBox.TextChanged += new System.EventHandler(this.NameTextBox_TextChanged);
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(12, 34);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(38, 13);
			this.nameLabel.TabIndex = 1;
			this.nameLabel.Text = "Name:";
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(798, 610);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 24);
			this.cancelButton.TabIndex = 7;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(717, 610);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 24);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// outputGroupBox
			// 
			this.outputGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.outputGroupBox.Controls.Add(this.clrNotAvailableOutputLabel);
			this.outputGroupBox.Controls.Add(this.outputTypeLabel);
			this.outputGroupBox.Controls.Add(this.outputTypeComboBox);
			this.outputGroupBox.Controls.Add(this.outputTextEditorControl);
			this.outputGroupBox.Location = new System.Drawing.Point(3, 3);
			this.outputGroupBox.Name = "outputGroupBox";
			this.outputGroupBox.Size = new System.Drawing.Size(860, 267);
			this.outputGroupBox.TabIndex = 1;
			this.outputGroupBox.TabStop = false;
			this.outputGroupBox.Text = "Output";
			// 
			// clrNotAvailableOutputLabel
			// 
			this.clrNotAvailableOutputLabel.AutoSize = true;
			this.clrNotAvailableOutputLabel.ForeColor = System.Drawing.Color.Red;
			this.clrNotAvailableOutputLabel.Location = new System.Drawing.Point(258, 22);
			this.clrNotAvailableOutputLabel.Name = "clrNotAvailableOutputLabel";
			this.clrNotAvailableOutputLabel.Size = new System.Drawing.Size(125, 13);
			this.clrNotAvailableOutputLabel.TabIndex = 4;
			this.clrNotAvailableOutputLabel.Text = "CLR support not enabled";
			this.clrNotAvailableOutputLabel.Visible = false;
			// 
			// outputTypeLabel
			// 
			this.outputTypeLabel.AutoSize = true;
			this.outputTypeLabel.Location = new System.Drawing.Point(6, 22);
			this.outputTypeLabel.Name = "outputTypeLabel";
			this.outputTypeLabel.Size = new System.Drawing.Size(34, 13);
			this.outputTypeLabel.TabIndex = 3;
			this.outputTypeLabel.Text = "Type:";
			// 
			// outputTypeComboBox
			// 
			this.outputTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.outputTypeComboBox.FormattingEnabled = true;
			this.outputTypeComboBox.Location = new System.Drawing.Point(44, 19);
			this.outputTypeComboBox.Name = "outputTypeComboBox";
			this.outputTypeComboBox.Size = new System.Drawing.Size(208, 21);
			this.outputTypeComboBox.TabIndex = 0;
			// 
			// outputTextEditorControl
			// 
			this.outputTextEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.outputTextEditorControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.outputTextEditorControl.ContextMenuStrip = this.outputContextMenuStrip;
			this.outputTextEditorControl.IsReadOnly = false;
			this.outputTextEditorControl.Location = new System.Drawing.Point(6, 46);
			this.outputTextEditorControl.Name = "outputTextEditorControl";
			this.outputTextEditorControl.ShowVRuler = false;
			this.outputTextEditorControl.Size = new System.Drawing.Size(848, 215);
			this.outputTextEditorControl.TabIndex = 1;
			this.outputTextEditorControl.TextChanged += new System.EventHandler(this.OutputTextEditorControl_TextChanged);
			// 
			// outputContextMenuStrip
			// 
			this.outputContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem,
            this.toolStripSeparator5,
            this.insertToolStripMenuItem2,
            this.toolStripSeparator8,
            this.outputToolStripMenuItemUndo,
            this.outputToolStripMenuItemRedo,
            this.toolStripSeparator3,
            this.outputToolStripMenuItemCut,
            this.outputToolStripMenuItemCopy,
            this.outputToolStripMenuItemPaste,
            this.outputToolStripMenuItemDelete,
            this.toolStripSeparator4,
            this.outputToolStripMenuItemSelectAll});
			this.outputContextMenuStrip.Name = "contextMenuStrip1";
			this.outputContextMenuStrip.Size = new System.Drawing.Size(165, 226);
			this.outputContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.OutputContextMenuStrip_Opening);
			// 
			// searchToolStripMenuItem
			// 
			this.searchToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.find_small;
			this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
			this.searchToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+F";
			this.searchToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.searchToolStripMenuItem.Text = "Search...";
			this.searchToolStripMenuItem.Click += new System.EventHandler(this.SearchToolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(161, 6);
			// 
			// insertToolStripMenuItem2
			// 
			this.insertToolStripMenuItem2.Name = "insertToolStripMenuItem2";
			this.insertToolStripMenuItem2.Size = new System.Drawing.Size(164, 22);
			this.insertToolStripMenuItem2.Text = "Insert Parameter";
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(161, 6);
			// 
			// outputToolStripMenuItemUndo
			// 
			this.outputToolStripMenuItemUndo.Name = "outputToolStripMenuItemUndo";
			this.outputToolStripMenuItemUndo.ShortcutKeyDisplayString = "Ctrl+Z";
			this.outputToolStripMenuItemUndo.Size = new System.Drawing.Size(164, 22);
			this.outputToolStripMenuItemUndo.Text = "Undo";
			this.outputToolStripMenuItemUndo.Click += new System.EventHandler(this.OutputToolStripMenuItemUndo_Click);
			// 
			// outputToolStripMenuItemRedo
			// 
			this.outputToolStripMenuItemRedo.Name = "outputToolStripMenuItemRedo";
			this.outputToolStripMenuItemRedo.ShortcutKeyDisplayString = "Ctrl+Y";
			this.outputToolStripMenuItemRedo.Size = new System.Drawing.Size(164, 22);
			this.outputToolStripMenuItemRedo.Text = "Redo";
			this.outputToolStripMenuItemRedo.Click += new System.EventHandler(this.OutputToolStripMenuItemRedo_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(161, 6);
			// 
			// outputToolStripMenuItemCut
			// 
			this.outputToolStripMenuItemCut.Name = "outputToolStripMenuItemCut";
			this.outputToolStripMenuItemCut.ShortcutKeyDisplayString = "Ctrl+X";
			this.outputToolStripMenuItemCut.Size = new System.Drawing.Size(164, 22);
			this.outputToolStripMenuItemCut.Text = "Cut";
			this.outputToolStripMenuItemCut.Click += new System.EventHandler(this.OutputToolStripMenuItemCut_Click);
			// 
			// outputToolStripMenuItemCopy
			// 
			this.outputToolStripMenuItemCopy.Name = "outputToolStripMenuItemCopy";
			this.outputToolStripMenuItemCopy.ShortcutKeyDisplayString = "Ctrl+C";
			this.outputToolStripMenuItemCopy.Size = new System.Drawing.Size(164, 22);
			this.outputToolStripMenuItemCopy.Text = "Copy";
			this.outputToolStripMenuItemCopy.Click += new System.EventHandler(this.OutputToolStripMenuItemCopy_Click);
			// 
			// outputToolStripMenuItemPaste
			// 
			this.outputToolStripMenuItemPaste.Name = "outputToolStripMenuItemPaste";
			this.outputToolStripMenuItemPaste.ShortcutKeyDisplayString = "Ctrl+V";
			this.outputToolStripMenuItemPaste.Size = new System.Drawing.Size(164, 22);
			this.outputToolStripMenuItemPaste.Text = "Paste";
			this.outputToolStripMenuItemPaste.Click += new System.EventHandler(this.OutputToolStripMenuItemPaste_Click);
			// 
			// outputToolStripMenuItemDelete
			// 
			this.outputToolStripMenuItemDelete.Name = "outputToolStripMenuItemDelete";
			this.outputToolStripMenuItemDelete.ShortcutKeyDisplayString = "Del";
			this.outputToolStripMenuItemDelete.Size = new System.Drawing.Size(164, 22);
			this.outputToolStripMenuItemDelete.Text = "Delete";
			this.outputToolStripMenuItemDelete.Click += new System.EventHandler(this.OutputToolStripMenuItemDelete_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(161, 6);
			// 
			// outputToolStripMenuItemSelectAll
			// 
			this.outputToolStripMenuItemSelectAll.Name = "outputToolStripMenuItemSelectAll";
			this.outputToolStripMenuItemSelectAll.ShortcutKeyDisplayString = "Ctrl+A";
			this.outputToolStripMenuItemSelectAll.Size = new System.Drawing.Size(164, 22);
			this.outputToolStripMenuItemSelectAll.Text = "Select All";
			this.outputToolStripMenuItemSelectAll.Click += new System.EventHandler(this.OutputToolStripMenuItemSelectAll_Click);
			// 
			// inputContextMenuStrip
			// 
			this.inputContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem1,
            this.toolStripSeparator6,
            this.insertToolStripMenuItem1,
            this.toolStripSeparator7,
            this.inputToolStripMenuItemUndo,
            this.inputToolStripMenuItemRedo,
            this.toolStripSeparator1,
            this.inputToolStripMenuItemCut,
            this.inputToolStripMenuItemCopy,
            this.inputToolStripMenuItemPaste,
            this.inputToolStripMenuItemDelete,
            this.toolStripSeparator2,
            this.inputToolStripMenuItemSelectAll});
			this.inputContextMenuStrip.Name = "contextMenuStrip1";
			this.inputContextMenuStrip.Size = new System.Drawing.Size(165, 226);
			this.inputContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.InputContextMenuStrip_Opening);
			// 
			// searchToolStripMenuItem1
			// 
			this.searchToolStripMenuItem1.Image = global::SQLEventAnalyzer.Properties.Resources.find_small;
			this.searchToolStripMenuItem1.Name = "searchToolStripMenuItem1";
			this.searchToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+F";
			this.searchToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
			this.searchToolStripMenuItem1.Text = "Search...";
			this.searchToolStripMenuItem1.Click += new System.EventHandler(this.SearchToolStripMenuItem1_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(161, 6);
			// 
			// insertToolStripMenuItem1
			// 
			this.insertToolStripMenuItem1.Name = "insertToolStripMenuItem1";
			this.insertToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
			this.insertToolStripMenuItem1.Text = "Insert Parameter";
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(161, 6);
			// 
			// inputToolStripMenuItemUndo
			// 
			this.inputToolStripMenuItemUndo.Name = "inputToolStripMenuItemUndo";
			this.inputToolStripMenuItemUndo.ShortcutKeyDisplayString = "Ctrl+Z";
			this.inputToolStripMenuItemUndo.Size = new System.Drawing.Size(164, 22);
			this.inputToolStripMenuItemUndo.Text = "Undo";
			this.inputToolStripMenuItemUndo.Click += new System.EventHandler(this.InputToolStripMenuItemUndo_Click);
			// 
			// inputToolStripMenuItemRedo
			// 
			this.inputToolStripMenuItemRedo.Name = "inputToolStripMenuItemRedo";
			this.inputToolStripMenuItemRedo.ShortcutKeyDisplayString = "Ctrl+Y";
			this.inputToolStripMenuItemRedo.Size = new System.Drawing.Size(164, 22);
			this.inputToolStripMenuItemRedo.Text = "Redo";
			this.inputToolStripMenuItemRedo.Click += new System.EventHandler(this.InputToolStripMenuItemRedo_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
			// 
			// inputToolStripMenuItemCut
			// 
			this.inputToolStripMenuItemCut.Name = "inputToolStripMenuItemCut";
			this.inputToolStripMenuItemCut.ShortcutKeyDisplayString = "Ctrl+X";
			this.inputToolStripMenuItemCut.Size = new System.Drawing.Size(164, 22);
			this.inputToolStripMenuItemCut.Text = "Cut";
			this.inputToolStripMenuItemCut.Click += new System.EventHandler(this.InputToolStripMenuItemCut_Click);
			// 
			// inputToolStripMenuItemCopy
			// 
			this.inputToolStripMenuItemCopy.Name = "inputToolStripMenuItemCopy";
			this.inputToolStripMenuItemCopy.ShortcutKeyDisplayString = "Ctrl+C";
			this.inputToolStripMenuItemCopy.Size = new System.Drawing.Size(164, 22);
			this.inputToolStripMenuItemCopy.Text = "Copy";
			this.inputToolStripMenuItemCopy.Click += new System.EventHandler(this.InputToolStripMenuItemCopy_Click);
			// 
			// inputToolStripMenuItemPaste
			// 
			this.inputToolStripMenuItemPaste.Name = "inputToolStripMenuItemPaste";
			this.inputToolStripMenuItemPaste.ShortcutKeyDisplayString = "Ctrl+V";
			this.inputToolStripMenuItemPaste.Size = new System.Drawing.Size(164, 22);
			this.inputToolStripMenuItemPaste.Text = "Paste";
			this.inputToolStripMenuItemPaste.Click += new System.EventHandler(this.InputToolStripMenuItemPaste_Click);
			// 
			// inputToolStripMenuItemDelete
			// 
			this.inputToolStripMenuItemDelete.Name = "inputToolStripMenuItemDelete";
			this.inputToolStripMenuItemDelete.ShortcutKeyDisplayString = "Del";
			this.inputToolStripMenuItemDelete.Size = new System.Drawing.Size(164, 22);
			this.inputToolStripMenuItemDelete.Text = "Delete";
			this.inputToolStripMenuItemDelete.Click += new System.EventHandler(this.InputToolStripMenuItemDelete_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
			// 
			// inputToolStripMenuItemSelectAll
			// 
			this.inputToolStripMenuItemSelectAll.Name = "inputToolStripMenuItemSelectAll";
			this.inputToolStripMenuItemSelectAll.ShortcutKeyDisplayString = "Ctrl+A";
			this.inputToolStripMenuItemSelectAll.Size = new System.Drawing.Size(164, 22);
			this.inputToolStripMenuItemSelectAll.Text = "Select All";
			this.inputToolStripMenuItemSelectAll.Click += new System.EventHandler(this.InputToolStripMenuItemSelectAll_Click);
			// 
			// inputGroupBox
			// 
			this.inputGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.inputGroupBox.Controls.Add(this.clrNotAvailableIntputLabel);
			this.inputGroupBox.Controls.Add(this.inputTypeLabel);
			this.inputGroupBox.Controls.Add(this.inputTypeComboBox);
			this.inputGroupBox.Controls.Add(this.inputTextEditorControl);
			this.inputGroupBox.Location = new System.Drawing.Point(3, 3);
			this.inputGroupBox.Name = "inputGroupBox";
			this.inputGroupBox.Size = new System.Drawing.Size(860, 258);
			this.inputGroupBox.TabIndex = 0;
			this.inputGroupBox.TabStop = false;
			this.inputGroupBox.Text = "Input";
			// 
			// clrNotAvailableIntputLabel
			// 
			this.clrNotAvailableIntputLabel.AutoSize = true;
			this.clrNotAvailableIntputLabel.ForeColor = System.Drawing.Color.Red;
			this.clrNotAvailableIntputLabel.Location = new System.Drawing.Point(258, 22);
			this.clrNotAvailableIntputLabel.Name = "clrNotAvailableIntputLabel";
			this.clrNotAvailableIntputLabel.Size = new System.Drawing.Size(125, 13);
			this.clrNotAvailableIntputLabel.TabIndex = 5;
			this.clrNotAvailableIntputLabel.Text = "CLR support not enabled";
			this.clrNotAvailableIntputLabel.Visible = false;
			// 
			// inputTypeLabel
			// 
			this.inputTypeLabel.AutoSize = true;
			this.inputTypeLabel.Location = new System.Drawing.Point(6, 22);
			this.inputTypeLabel.Name = "inputTypeLabel";
			this.inputTypeLabel.Size = new System.Drawing.Size(34, 13);
			this.inputTypeLabel.TabIndex = 2;
			this.inputTypeLabel.Text = "Type:";
			// 
			// inputTypeComboBox
			// 
			this.inputTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.inputTypeComboBox.FormattingEnabled = true;
			this.inputTypeComboBox.Location = new System.Drawing.Point(44, 19);
			this.inputTypeComboBox.Name = "inputTypeComboBox";
			this.inputTypeComboBox.Size = new System.Drawing.Size(208, 21);
			this.inputTypeComboBox.TabIndex = 0;
			// 
			// inputTextEditorControl
			// 
			this.inputTextEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.inputTextEditorControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.inputTextEditorControl.ContextMenuStrip = this.inputContextMenuStrip;
			this.inputTextEditorControl.IsReadOnly = false;
			this.inputTextEditorControl.Location = new System.Drawing.Point(6, 46);
			this.inputTextEditorControl.Name = "inputTextEditorControl";
			this.inputTextEditorControl.ShowVRuler = false;
			this.inputTextEditorControl.Size = new System.Drawing.Size(848, 206);
			this.inputTextEditorControl.TabIndex = 1;
			this.inputTextEditorControl.TextChanged += new System.EventHandler(this.InputTextEditorControl_TextChanged);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(9, 57);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.inputGroupBox);
			this.splitContainer1.Panel1MinSize = 127;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.outputGroupBox);
			this.splitContainer1.Panel2MinSize = 120;
			this.splitContainer1.Size = new System.Drawing.Size(866, 548);
			this.splitContainer1.SplitterDistance = 271;
			this.splitContainer1.TabIndex = 5;
			this.splitContainer1.TabStop = false;
			this.splitContainer1.Paint += new System.Windows.Forms.PaintEventHandler(this.SplitContainer1_Paint);
			this.splitContainer1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SplitContainer1_MouseUp);
			// 
			// hiddenCheckBox
			// 
			this.hiddenCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.hiddenCheckBox.AutoSize = true;
			this.hiddenCheckBox.Location = new System.Drawing.Point(732, 33);
			this.hiddenCheckBox.Name = "hiddenCheckBox";
			this.hiddenCheckBox.Size = new System.Drawing.Size(60, 17);
			this.hiddenCheckBox.TabIndex = 3;
			this.hiddenCheckBox.Text = "Hidden";
			this.hiddenCheckBox.UseVisualStyleBackColor = true;
			this.hiddenCheckBox.CheckedChanged += new System.EventHandler(this.HiddenCheckBox_CheckedChanged);
			// 
			// fontDialog1
			// 
			this.fontDialog1.AllowScriptChange = false;
			this.fontDialog1.Color = System.Drawing.SystemColors.ControlText;
			this.fontDialog1.FixedPitchOnly = true;
			this.fontDialog1.ShowEffects = false;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.formatToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.menuStrip1.Size = new System.Drawing.Size(884, 24);
			this.menuStrip1.TabIndex = 6;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.closeToolStripMenuItem.Text = "&Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem2,
            this.toolStripSeparator9,
            this.insertParameterToolStripMenuItem,
            this.toolStripSeparator10,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator11,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator12,
            this.selectAllToolStripMenuItem});
			this.editToolStripMenuItem.Enabled = false;
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			this.editToolStripMenuItem.DropDownOpening += new System.EventHandler(this.EditToolStripMenuItem_DropDownOpening);
			// 
			// searchToolStripMenuItem2
			// 
			this.searchToolStripMenuItem2.Image = global::SQLEventAnalyzer.Properties.Resources.find_small;
			this.searchToolStripMenuItem2.Name = "searchToolStripMenuItem2";
			this.searchToolStripMenuItem2.ShortcutKeyDisplayString = "Ctrl+F";
			this.searchToolStripMenuItem2.Size = new System.Drawing.Size(164, 22);
			this.searchToolStripMenuItem2.Text = "Search...";
			this.searchToolStripMenuItem2.Click += new System.EventHandler(this.SearchToolStripMenuItem2_Click);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(161, 6);
			// 
			// insertParameterToolStripMenuItem
			// 
			this.insertParameterToolStripMenuItem.Name = "insertParameterToolStripMenuItem";
			this.insertParameterToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.insertParameterToolStripMenuItem.Text = "Insert Parameter";
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(161, 6);
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Z";
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.undoToolStripMenuItem.Text = "Undo";
			this.undoToolStripMenuItem.Click += new System.EventHandler(this.UndoToolStripMenuItem_Click);
			// 
			// redoToolStripMenuItem
			// 
			this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
			this.redoToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Y";
			this.redoToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.redoToolStripMenuItem.Text = "Redo";
			this.redoToolStripMenuItem.Click += new System.EventHandler(this.RedoToolStripMenuItem_Click);
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(161, 6);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+X";
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.cutToolStripMenuItem.Text = "Cut";
			this.cutToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+V";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.ShortcutKeyDisplayString = "Del";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
			// 
			// toolStripSeparator12
			// 
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(161, 6);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+A";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.selectAllToolStripMenuItem.Text = "Select All";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllToolStripMenuItem_Click);
			// 
			// formatToolStripMenuItem
			// 
			this.formatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontToolStripMenuItem});
			this.formatToolStripMenuItem.Name = "formatToolStripMenuItem";
			this.formatToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
			this.formatToolStripMenuItem.Text = "F&ormat";
			// 
			// fontToolStripMenuItem
			// 
			this.fontToolStripMenuItem.Image = global::SQLEventAnalyzer.Properties.Resources.font;
			this.fontToolStripMenuItem.Name = "fontToolStripMenuItem";
			this.fontToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.fontToolStripMenuItem.Text = "&Font...";
			this.fontToolStripMenuItem.Click += new System.EventHandler(this.FontToolStripMenuItem_Click);
			// 
			// enabledCheckBox
			// 
			this.enabledCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.enabledCheckBox.AutoSize = true;
			this.enabledCheckBox.Checked = true;
			this.enabledCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.enabledCheckBox.Location = new System.Drawing.Point(801, 33);
			this.enabledCheckBox.Name = "enabledCheckBox";
			this.enabledCheckBox.Size = new System.Drawing.Size(65, 17);
			this.enabledCheckBox.TabIndex = 4;
			this.enabledCheckBox.Text = "Enabled";
			this.enabledCheckBox.UseVisualStyleBackColor = true;
			this.enabledCheckBox.CheckedChanged += new System.EventHandler(this.EnabledCheckBox_CheckedChanged);
			// 
			// widthLabel
			// 
			this.widthLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.widthLabel.AutoSize = true;
			this.widthLabel.Location = new System.Drawing.Point(617, 34);
			this.widthLabel.Name = "widthLabel";
			this.widthLabel.Size = new System.Drawing.Size(38, 13);
			this.widthLabel.TabIndex = 8;
			this.widthLabel.Text = "Width:";
			// 
			// widthTextBox
			// 
			this.widthTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.widthTextBox.Location = new System.Drawing.Point(661, 31);
			this.widthTextBox.Name = "widthTextBox";
			this.widthTextBox.Size = new System.Drawing.Size(65, 20);
			this.widthTextBox.TabIndex = 2;
			this.widthTextBox.TextChanged += new System.EventHandler(this.WidthTextBox_TextChanged);
			// 
			// isolationLevelComboBox
			// 
			this.isolationLevelComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.isolationLevelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.isolationLevelComboBox.FormattingEnabled = true;
			this.isolationLevelComboBox.Location = new System.Drawing.Point(490, 31);
			this.isolationLevelComboBox.Name = "isolationLevelComboBox";
			this.isolationLevelComboBox.Size = new System.Drawing.Size(121, 21);
			this.isolationLevelComboBox.TabIndex = 1;
			// 
			// isolationLevelLabel
			// 
			this.isolationLevelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.isolationLevelLabel.AutoSize = true;
			this.isolationLevelLabel.Location = new System.Drawing.Point(406, 34);
			this.isolationLevelLabel.Name = "isolationLevelLabel";
			this.isolationLevelLabel.Size = new System.Drawing.Size(78, 13);
			this.isolationLevelLabel.TabIndex = 9;
			this.isolationLevelLabel.Text = "Isolation Level:";
			// 
			// HandleColumnForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(884, 641);
			this.Controls.Add(this.isolationLevelLabel);
			this.Controls.Add(this.isolationLevelComboBox);
			this.Controls.Add(this.widthLabel);
			this.Controls.Add(this.widthTextBox);
			this.Controls.Add(this.enabledCheckBox);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.hiddenCheckBox);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.nameLabel);
			this.Controls.Add(this.nameTextBox);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(666, 394);
			this.Name = "HandleColumnForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Custom Column";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HandleColumnForm_FormClosing);
			this.Resize += new System.EventHandler(this.HandleColumnForm_Resize);
			this.outputGroupBox.ResumeLayout(false);
			this.outputGroupBox.PerformLayout();
			this.outputContextMenuStrip.ResumeLayout(false);
			this.inputContextMenuStrip.ResumeLayout(false);
			this.inputGroupBox.ResumeLayout(false);
			this.inputGroupBox.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.TextBox nameTextBox;
	private System.Windows.Forms.Label nameLabel;
	private System.Windows.Forms.Button cancelButton;
	private System.Windows.Forms.Button okButton;
	private System.Windows.Forms.GroupBox outputGroupBox;
	private System.Windows.Forms.GroupBox inputGroupBox;
	private ICSharpCode.TextEditor.TextEditorControl inputTextEditorControl;
	private ICSharpCode.TextEditor.TextEditorControl outputTextEditorControl;
	private System.Windows.Forms.ContextMenuStrip inputContextMenuStrip;
	private System.Windows.Forms.ToolStripMenuItem inputToolStripMenuItemUndo;
	private System.Windows.Forms.ToolStripMenuItem inputToolStripMenuItemRedo;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	private System.Windows.Forms.ToolStripMenuItem inputToolStripMenuItemCut;
	private System.Windows.Forms.ToolStripMenuItem inputToolStripMenuItemCopy;
	private System.Windows.Forms.ToolStripMenuItem inputToolStripMenuItemPaste;
	private System.Windows.Forms.ToolStripMenuItem inputToolStripMenuItemDelete;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	private System.Windows.Forms.ToolStripMenuItem inputToolStripMenuItemSelectAll;
	private System.Windows.Forms.ContextMenuStrip outputContextMenuStrip;
	private System.Windows.Forms.ToolStripMenuItem outputToolStripMenuItemUndo;
	private System.Windows.Forms.ToolStripMenuItem outputToolStripMenuItemRedo;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
	private System.Windows.Forms.ToolStripMenuItem outputToolStripMenuItemCut;
	private System.Windows.Forms.ToolStripMenuItem outputToolStripMenuItemCopy;
	private System.Windows.Forms.ToolStripMenuItem outputToolStripMenuItemPaste;
	private System.Windows.Forms.ToolStripMenuItem outputToolStripMenuItemDelete;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
	private System.Windows.Forms.ToolStripMenuItem outputToolStripMenuItemSelectAll;
	private System.Windows.Forms.SplitContainer splitContainer1;
	private System.Windows.Forms.CheckBox hiddenCheckBox;
	private System.Windows.Forms.FontDialog fontDialog1;
	private System.Windows.Forms.MenuStrip menuStrip1;
	private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem formatToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem fontToolStripMenuItem;
	private System.Windows.Forms.CheckBox enabledCheckBox;
	private ComboBoxCustom outputTypeComboBox;
	private ComboBoxCustom inputTypeComboBox;
	private System.Windows.Forms.Label outputTypeLabel;
	private System.Windows.Forms.Label inputTypeLabel;
	private System.Windows.Forms.Label clrNotAvailableOutputLabel;
	private System.Windows.Forms.Label clrNotAvailableIntputLabel;
	private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
	private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem1;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
	private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem2;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
	private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem1;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
	private System.Windows.Forms.Label widthLabel;
	private System.Windows.Forms.TextBox widthTextBox;
	private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem2;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
	private System.Windows.Forms.ToolStripMenuItem insertParameterToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
	private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
	private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
	private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
	private ComboBoxCustom isolationLevelComboBox;
	private System.Windows.Forms.Label isolationLevelLabel;
}
