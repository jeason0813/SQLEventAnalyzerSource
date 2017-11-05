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

partial class StatisticUserControl
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
			this.statisticsGroupBox = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.defaultToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.sortAlphabeticallyCheckBox = new System.Windows.Forms.CheckBox();
			this.removeButton = new System.Windows.Forms.Button();
			this.addButton = new System.Windows.Forms.Button();
			this.showHiddenCheckBox = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.viewButton = new System.Windows.Forms.Button();
			this.comboBoxGroupStatisticsUserControl1 = new ComboBoxGroupStatisticsUserControl();
			this.statisticsGroupBox.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statisticsGroupBox
			// 
			this.statisticsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.statisticsGroupBox.Controls.Add(this.flowLayoutPanel1);
			this.statisticsGroupBox.Controls.Add(this.toolStrip1);
			this.statisticsGroupBox.Controls.Add(this.sortAlphabeticallyCheckBox);
			this.statisticsGroupBox.Controls.Add(this.removeButton);
			this.statisticsGroupBox.Controls.Add(this.addButton);
			this.statisticsGroupBox.Controls.Add(this.showHiddenCheckBox);
			this.statisticsGroupBox.Controls.Add(this.label3);
			this.statisticsGroupBox.Controls.Add(this.viewButton);
			this.statisticsGroupBox.Location = new System.Drawing.Point(3, 3);
			this.statisticsGroupBox.Name = "statisticsGroupBox";
			this.statisticsGroupBox.Size = new System.Drawing.Size(906, 209);
			this.statisticsGroupBox.TabIndex = 0;
			this.statisticsGroupBox.TabStop = false;
			this.statisticsGroupBox.Text = "Statistics";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.AutoScroll = true;
			this.flowLayoutPanel1.Controls.Add(this.comboBoxGroupStatisticsUserControl1);
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 42);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(894, 130);
			this.flowLayoutPanel1.TabIndex = 0;
			this.flowLayoutPanel1.WrapContents = false;
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
			this.toolStrip1.TabIndex = 17;
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
			// viewButton
			// 
			this.viewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.viewButton.Location = new System.Drawing.Point(825, 180);
			this.viewButton.Name = "viewButton";
			this.viewButton.Size = new System.Drawing.Size(75, 24);
			this.viewButton.TabIndex = 1;
			this.viewButton.Text = "View";
			this.viewButton.UseVisualStyleBackColor = false;
			this.viewButton.Click += new System.EventHandler(this.ViewButton_Click);
			// 
			// comboBoxGroupStatisticsUserControl1
			// 
			this.comboBoxGroupStatisticsUserControl1.Location = new System.Drawing.Point(0, 0);
			this.comboBoxGroupStatisticsUserControl1.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxGroupStatisticsUserControl1.Name = "comboBoxGroupStatisticsUserControl1";
			this.comboBoxGroupStatisticsUserControl1.Size = new System.Drawing.Size(781, 27);
			this.comboBoxGroupStatisticsUserControl1.TabIndex = 0;
			// 
			// StatisticUserControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.statisticsGroupBox);
			this.DoubleBuffered = true;
			this.MinimumSize = new System.Drawing.Size(430, 215);
			this.Name = "StatisticUserControl";
			this.Size = new System.Drawing.Size(912, 215);
			this.statisticsGroupBox.ResumeLayout(false);
			this.statisticsGroupBox.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.GroupBox statisticsGroupBox;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.Button viewButton;
	private System.Windows.Forms.CheckBox showHiddenCheckBox;
	private System.Windows.Forms.Button addButton;
	private System.Windows.Forms.Button removeButton;
	private System.Windows.Forms.CheckBox sortAlphabeticallyCheckBox;
	private System.Windows.Forms.ToolStrip toolStrip1;
	private System.Windows.Forms.ToolStripButton saveToolStripButton;
	private System.Windows.Forms.ToolStripButton deleteToolStripButton;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
	private System.Windows.Forms.ToolStripButton defaultToolStripButton;
	private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
	private ComboBoxGroupStatisticsUserControl comboBoxGroupStatisticsUserControl1;
}
