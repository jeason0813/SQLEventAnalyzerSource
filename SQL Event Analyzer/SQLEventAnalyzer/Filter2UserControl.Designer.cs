﻿/*
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

partial class Filter2UserControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Filter2UserControl));
			this.filterGroupBox = new System.Windows.Forms.GroupBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.defaultToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.comboBoxGroupUserControl1 = new ComboBoxGroupUserControl();
			this.sortAlphabeticallyCheckBox = new System.Windows.Forms.CheckBox();
			this.removeButton = new System.Windows.Forms.Button();
			this.addButton = new System.Windows.Forms.Button();
			this.showHiddenCheckBox = new System.Windows.Forms.CheckBox();
			this.deactivateButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.activateButton = new System.Windows.Forms.Button();
			this.filterGroupBox.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// filterGroupBox
			// 
			this.filterGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.filterGroupBox.Controls.Add(this.toolStrip1);
			this.filterGroupBox.Controls.Add(this.flowLayoutPanel1);
			this.filterGroupBox.Controls.Add(this.sortAlphabeticallyCheckBox);
			this.filterGroupBox.Controls.Add(this.removeButton);
			this.filterGroupBox.Controls.Add(this.addButton);
			this.filterGroupBox.Controls.Add(this.showHiddenCheckBox);
			this.filterGroupBox.Controls.Add(this.deactivateButton);
			this.filterGroupBox.Controls.Add(this.label3);
			this.filterGroupBox.Controls.Add(this.activateButton);
			this.filterGroupBox.Location = new System.Drawing.Point(3, 3);
			this.filterGroupBox.Name = "filterGroupBox";
			this.filterGroupBox.Size = new System.Drawing.Size(906, 209);
			this.filterGroupBox.TabIndex = 0;
			this.filterGroupBox.TabStop = false;
			this.filterGroupBox.Text = "Filter 2 (for Custom Columns)";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripButton,
            this.deleteToolStripButton,
            this.toolStripSeparator1,
            this.toolStripDropDownButton1,
            this.defaultToolStripButton});
			this.toolStrip1.Location = new System.Drawing.Point(669, 13);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(231, 25);
			this.toolStrip1.TabIndex = 8;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// saveToolStripButton
			// 
			this.saveToolStripButton.Image = global::SQLEventAnalyzer.Properties.Resources.application_form_add;
			this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripButton.Name = "saveToolStripButton";
			this.saveToolStripButton.Size = new System.Drawing.Size(76, 22);
			this.saveToolStripButton.Text = "Save As...";
			this.saveToolStripButton.Click += new System.EventHandler(this.SaveToolStripButton_Click);
			// 
			// deleteToolStripButton
			// 
			this.deleteToolStripButton.Enabled = false;
			this.deleteToolStripButton.Image = global::SQLEventAnalyzer.Properties.Resources.application_form_delete;
			this.deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.deleteToolStripButton.Name = "deleteToolStripButton";
			this.deleteToolStripButton.Size = new System.Drawing.Size(60, 22);
			this.deleteToolStripButton.Text = "Delete";
			this.deleteToolStripButton.Click += new System.EventHandler(this.DeleteToolStripButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(54, 22);
			this.toolStripDropDownButton1.Text = "Empty";
			this.toolStripDropDownButton1.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ToolStripDropDownButton1_DropDownItemClicked);
			// 
			// defaultToolStripButton
			// 
			this.defaultToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.defaultToolStripButton.Image = global::SQLEventAnalyzer.Properties.Resources.star1;
			this.defaultToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.defaultToolStripButton.Name = "defaultToolStripButton";
			this.defaultToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.defaultToolStripButton.ToolTipText = "Mark selected as the default.";
			this.defaultToolStripButton.Click += new System.EventHandler(this.DefaultToolStripButton_Click);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.AutoScroll = true;
			this.flowLayoutPanel1.Controls.Add(this.comboBoxGroupUserControl1);
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 42);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(894, 130);
			this.flowLayoutPanel1.TabIndex = 0;
			this.flowLayoutPanel1.WrapContents = false;
			// 
			// comboBoxGroupUserControl1
			// 
			this.comboBoxGroupUserControl1.Location = new System.Drawing.Point(0, 0);
			this.comboBoxGroupUserControl1.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxGroupUserControl1.Name = "comboBoxGroupUserControl1";
			this.comboBoxGroupUserControl1.Size = new System.Drawing.Size(781, 27);
			this.comboBoxGroupUserControl1.TabIndex = 0;
			// 
			// sortAlphabeticallyCheckBox
			// 
			this.sortAlphabeticallyCheckBox.AutoSize = true;
			this.sortAlphabeticallyCheckBox.Location = new System.Drawing.Point(168, 19);
			this.sortAlphabeticallyCheckBox.Name = "sortAlphabeticallyCheckBox";
			this.sortAlphabeticallyCheckBox.Size = new System.Drawing.Size(141, 17);
			this.sortAlphabeticallyCheckBox.TabIndex = 4;
			this.sortAlphabeticallyCheckBox.Text = "Sort name alphabetically";
			this.sortAlphabeticallyCheckBox.UseVisualStyleBackColor = true;
			this.sortAlphabeticallyCheckBox.CheckedChanged += new System.EventHandler(this.SortAlphabeticallyCheckBox_CheckedChanged);
			// 
			// removeButton
			// 
			this.removeButton.Enabled = false;
			this.removeButton.Location = new System.Drawing.Point(87, 15);
			this.removeButton.Name = "removeButton";
			this.removeButton.Size = new System.Drawing.Size(75, 23);
			this.removeButton.TabIndex = 3;
			this.removeButton.Text = "Remove";
			this.removeButton.UseVisualStyleBackColor = false;
			this.removeButton.Click += new System.EventHandler(this.RemoveButton_Click);
			// 
			// addButton
			// 
			this.addButton.Location = new System.Drawing.Point(6, 15);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(75, 23);
			this.addButton.TabIndex = 2;
			this.addButton.Text = "Add";
			this.addButton.UseVisualStyleBackColor = false;
			this.addButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// showHiddenCheckBox
			// 
			this.showHiddenCheckBox.AutoSize = true;
			this.showHiddenCheckBox.Location = new System.Drawing.Point(326, 19);
			this.showHiddenCheckBox.Name = "showHiddenCheckBox";
			this.showHiddenCheckBox.Size = new System.Drawing.Size(90, 17);
			this.showHiddenCheckBox.TabIndex = 5;
			this.showHiddenCheckBox.Text = "Show Hidden";
			this.showHiddenCheckBox.UseVisualStyleBackColor = true;
			this.showHiddenCheckBox.CheckedChanged += new System.EventHandler(this.ShowHiddenCheckBox_CheckedChanged);
			// 
			// deactivateButton
			// 
			this.deactivateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.deactivateButton.Enabled = false;
			this.deactivateButton.Location = new System.Drawing.Point(744, 180);
			this.deactivateButton.Name = "deactivateButton";
			this.deactivateButton.Size = new System.Drawing.Size(75, 24);
			this.deactivateButton.TabIndex = 0;
			this.deactivateButton.Text = "Deactivate";
			this.deactivateButton.UseVisualStyleBackColor = false;
			this.deactivateButton.Click += new System.EventHandler(this.ResetButton_Click);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label3.Location = new System.Drawing.Point(6, 175);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(894, 2);
			this.label3.TabIndex = 7;
			// 
			// activateButton
			// 
			this.activateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.activateButton.Location = new System.Drawing.Point(825, 180);
			this.activateButton.Name = "activateButton";
			this.activateButton.Size = new System.Drawing.Size(75, 24);
			this.activateButton.TabIndex = 1;
			this.activateButton.Text = "Activate";
			this.activateButton.UseVisualStyleBackColor = false;
			this.activateButton.Click += new System.EventHandler(this.ApplyButton_Click);
			// 
			// Filter2UserControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.filterGroupBox);
			this.DoubleBuffered = true;
			this.MinimumSize = new System.Drawing.Size(430, 215);
			this.Name = "Filter2UserControl";
			this.Size = new System.Drawing.Size(912, 215);
			this.filterGroupBox.ResumeLayout(false);
			this.filterGroupBox.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.GroupBox filterGroupBox;
	private System.Windows.Forms.Button deactivateButton;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.Button activateButton;
	private System.Windows.Forms.CheckBox showHiddenCheckBox;
	private System.Windows.Forms.Button addButton;
	private System.Windows.Forms.Button removeButton;
	private System.Windows.Forms.CheckBox sortAlphabeticallyCheckBox;
	private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
	private ComboBoxGroupUserControl comboBoxGroupUserControl1;
	private System.Windows.Forms.ToolStrip toolStrip1;
	private System.Windows.Forms.ToolStripButton saveToolStripButton;
	private System.Windows.Forms.ToolStripButton deleteToolStripButton;
	private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
	private System.Windows.Forms.ToolStripButton defaultToolStripButton;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
}
