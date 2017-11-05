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

partial class ErrorForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorForm));
			this.okButton = new System.Windows.Forms.Button();
			this.aboutTextBox = new System.Windows.Forms.TextBox();
			this.aboutGroupBox = new System.Windows.Forms.GroupBox();
			this.sqlGroupBox = new System.Windows.Forms.GroupBox();
			this.infoTextBox = new ICSharpCode.TextEditor.TextEditorControl();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemUndo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemRedo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemCut = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.programNameLabel = new System.Windows.Forms.Label();
			this.copyButton = new System.Windows.Forms.Button();
			this.errorMessageGroupBox = new System.Windows.Forms.GroupBox();
			this.errorTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.aboutGroupBox.SuspendLayout();
			this.sqlGroupBox.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			this.errorMessageGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(336, 397);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 24);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "Exit";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// aboutTextBox
			// 
			this.aboutTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.aboutTextBox.Location = new System.Drawing.Point(6, 19);
			this.aboutTextBox.Multiline = true;
			this.aboutTextBox.Name = "aboutTextBox";
			this.aboutTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.aboutTextBox.Size = new System.Drawing.Size(387, 51);
			this.aboutTextBox.TabIndex = 3;
			this.aboutTextBox.Enter += new System.EventHandler(this.AboutTextBox_Enter);
			this.aboutTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AboutTextBox_KeyDown);
			// 
			// aboutGroupBox
			// 
			this.aboutGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.aboutGroupBox.Controls.Add(this.aboutTextBox);
			this.aboutGroupBox.Location = new System.Drawing.Point(12, 119);
			this.aboutGroupBox.Name = "aboutGroupBox";
			this.aboutGroupBox.Size = new System.Drawing.Size(399, 76);
			this.aboutGroupBox.TabIndex = 3;
			this.aboutGroupBox.TabStop = false;
			this.aboutGroupBox.Text = "About";
			// 
			// sqlGroupBox
			// 
			this.sqlGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sqlGroupBox.Controls.Add(this.infoTextBox);
			this.sqlGroupBox.Location = new System.Drawing.Point(12, 201);
			this.sqlGroupBox.Name = "sqlGroupBox";
			this.sqlGroupBox.Size = new System.Drawing.Size(399, 188);
			this.sqlGroupBox.TabIndex = 4;
			this.sqlGroupBox.TabStop = false;
			this.sqlGroupBox.Text = "SQL";
			// 
			// infoTextBox
			// 
			this.infoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.infoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.infoTextBox.ContextMenuStrip = this.contextMenuStrip;
			this.infoTextBox.IsReadOnly = false;
			this.infoTextBox.Location = new System.Drawing.Point(6, 19);
			this.infoTextBox.Name = "infoTextBox";
			this.infoTextBox.ShowVRuler = false;
			this.infoTextBox.Size = new System.Drawing.Size(387, 163);
			this.infoTextBox.TabIndex = 4;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
			this.contextMenuStrip.Size = new System.Drawing.Size(165, 170);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
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
			// programNameLabel
			// 
			this.programNameLabel.AutoSize = true;
			this.programNameLabel.Location = new System.Drawing.Point(12, 9);
			this.programNameLabel.Name = "programNameLabel";
			this.programNameLabel.Size = new System.Drawing.Size(106, 13);
			this.programNameLabel.TabIndex = 5;
			this.programNameLabel.Text = "Program Name Label";
			// 
			// copyButton
			// 
			this.copyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.copyButton.Location = new System.Drawing.Point(255, 397);
			this.copyButton.Name = "copyButton";
			this.copyButton.Size = new System.Drawing.Size(75, 24);
			this.copyButton.TabIndex = 5;
			this.copyButton.Text = "Copy";
			this.copyButton.UseVisualStyleBackColor = true;
			this.copyButton.Click += new System.EventHandler(this.CopyButton_Click);
			// 
			// errorMessageGroupBox
			// 
			this.errorMessageGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.errorMessageGroupBox.Controls.Add(this.errorTextBox);
			this.errorMessageGroupBox.Location = new System.Drawing.Point(12, 33);
			this.errorMessageGroupBox.Name = "errorMessageGroupBox";
			this.errorMessageGroupBox.Size = new System.Drawing.Size(399, 80);
			this.errorMessageGroupBox.TabIndex = 2;
			this.errorMessageGroupBox.TabStop = false;
			this.errorMessageGroupBox.Text = "Error message";
			// 
			// errorTextBox
			// 
			this.errorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.errorTextBox.Location = new System.Drawing.Point(6, 19);
			this.errorTextBox.Multiline = true;
			this.errorTextBox.Name = "errorTextBox";
			this.errorTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.errorTextBox.Size = new System.Drawing.Size(387, 55);
			this.errorTextBox.TabIndex = 2;
			this.errorTextBox.WordWrap = false;
			this.errorTextBox.Enter += new System.EventHandler(this.ErrorTextBox_Enter);
			this.errorTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ErrorTextBox_KeyDown);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Location = new System.Drawing.Point(12, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(399, 2);
			this.label1.TabIndex = 4;
			// 
			// ErrorForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(423, 428);
			this.Controls.Add(this.errorMessageGroupBox);
			this.Controls.Add(this.copyButton);
			this.Controls.Add(this.programNameLabel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.sqlGroupBox);
			this.Controls.Add(this.aboutGroupBox);
			this.Controls.Add(this.okButton);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ErrorForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Error occured";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ErrorForm_FormClosing);
			this.aboutGroupBox.ResumeLayout(false);
			this.aboutGroupBox.PerformLayout();
			this.sqlGroupBox.ResumeLayout(false);
			this.contextMenuStrip.ResumeLayout(false);
			this.errorMessageGroupBox.ResumeLayout(false);
			this.errorMessageGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Button okButton;
	private System.Windows.Forms.GroupBox aboutGroupBox;
	private System.Windows.Forms.GroupBox sqlGroupBox;
	private System.Windows.Forms.Label programNameLabel;
	private System.Windows.Forms.Button copyButton;
	private System.Windows.Forms.GroupBox errorMessageGroupBox;
	private System.Windows.Forms.TextBox aboutTextBox;
	private System.Windows.Forms.TextBox errorTextBox;
	private ICSharpCode.TextEditor.TextEditorControl infoTextBox;
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
	private System.Windows.Forms.Label label1;
}
