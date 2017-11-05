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

partial class ComboBoxGroupStatisticsUserControl
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
			this.enabledCheckBox1 = new System.Windows.Forms.CheckBox();
			this.groupByLabel = new System.Windows.Forms.Label();
			this.columnComboBox1 = new ComboBoxCustom();
			this.SuspendLayout();
			// 
			// enabledCheckBox1
			// 
			this.enabledCheckBox1.AutoSize = true;
			this.enabledCheckBox1.Checked = true;
			this.enabledCheckBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.enabledCheckBox1.Location = new System.Drawing.Point(293, 5);
			this.enabledCheckBox1.Name = "enabledCheckBox1";
			this.enabledCheckBox1.Size = new System.Drawing.Size(65, 17);
			this.enabledCheckBox1.TabIndex = 1;
			this.enabledCheckBox1.Text = "Enabled";
			this.enabledCheckBox1.UseVisualStyleBackColor = true;
			// 
			// groupByLabel
			// 
			this.groupByLabel.AutoSize = true;
			this.groupByLabel.Location = new System.Drawing.Point(2, 6);
			this.groupByLabel.Name = "groupByLabel";
			this.groupByLabel.Size = new System.Drawing.Size(51, 13);
			this.groupByLabel.TabIndex = 18;
			this.groupByLabel.Text = "Group By";
			// 
			// columnComboBox1
			// 
			this.columnComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.columnComboBox1.FormattingEnabled = true;
			this.columnComboBox1.Location = new System.Drawing.Point(85, 3);
			this.columnComboBox1.Name = "columnComboBox1";
			this.columnComboBox1.Size = new System.Drawing.Size(200, 21);
			this.columnComboBox1.TabIndex = 0;
			// 
			// ComboBoxGroupStatisticsUserControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.enabledCheckBox1);
			this.Controls.Add(this.columnComboBox1);
			this.Controls.Add(this.groupByLabel);
			this.DoubleBuffered = true;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "ComboBoxGroupStatisticsUserControl";
			this.Size = new System.Drawing.Size(361, 27);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.CheckBox enabledCheckBox1;
	private ComboBoxCustom columnComboBox1;
	private System.Windows.Forms.Label groupByLabel;
}
