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

partial class ComboBoxGroupUserControl
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
			this.whereColumnLabel = new System.Windows.Forms.Label();
			this.operatorComboBox1 = new System.Windows.Forms.ComboBox();
			this.andOrComboBox1 = new System.Windows.Forms.ComboBox();
			this.valueComboBox1 = new System.Windows.Forms.ComboBox();
			this.paranthesEndComboBox = new System.Windows.Forms.ComboBox();
			this.paranthesBeginComboBox = new System.Windows.Forms.ComboBox();
			this.reloadValuesCheckBox = new System.Windows.Forms.CheckBox();
			this.columnComboBox1 = new ComboBoxCustom();
			this.SuspendLayout();
			// 
			// enabledCheckBox1
			// 
			this.enabledCheckBox1.AutoSize = true;
			this.enabledCheckBox1.Checked = true;
			this.enabledCheckBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.enabledCheckBox1.Location = new System.Drawing.Point(716, 5);
			this.enabledCheckBox1.Name = "enabledCheckBox1";
			this.enabledCheckBox1.Size = new System.Drawing.Size(65, 17);
			this.enabledCheckBox1.TabIndex = 7;
			this.enabledCheckBox1.Text = "Enabled";
			this.enabledCheckBox1.UseVisualStyleBackColor = true;
			// 
			// whereColumnLabel
			// 
			this.whereColumnLabel.AutoSize = true;
			this.whereColumnLabel.Location = new System.Drawing.Point(2, 6);
			this.whereColumnLabel.Name = "whereColumnLabel";
			this.whereColumnLabel.Size = new System.Drawing.Size(77, 13);
			this.whereColumnLabel.TabIndex = 18;
			this.whereColumnLabel.Text = "Where Column";
			this.whereColumnLabel.Visible = false;
			// 
			// operatorComboBox1
			// 
			this.operatorComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.operatorComboBox1.Enabled = false;
			this.operatorComboBox1.FormattingEnabled = true;
			this.operatorComboBox1.Location = new System.Drawing.Point(344, 3);
			this.operatorComboBox1.Name = "operatorComboBox1";
			this.operatorComboBox1.Size = new System.Drawing.Size(105, 21);
			this.operatorComboBox1.TabIndex = 3;
			// 
			// andOrComboBox1
			// 
			this.andOrComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.andOrComboBox1.FormattingEnabled = true;
			this.andOrComboBox1.Location = new System.Drawing.Point(32, 3);
			this.andOrComboBox1.Name = "andOrComboBox1";
			this.andOrComboBox1.Size = new System.Drawing.Size(47, 21);
			this.andOrComboBox1.TabIndex = 0;
			// 
			// valueComboBox1
			// 
			this.valueComboBox1.Enabled = false;
			this.valueComboBox1.FormattingEnabled = true;
			this.valueComboBox1.Location = new System.Drawing.Point(455, 3);
			this.valueComboBox1.Name = "valueComboBox1";
			this.valueComboBox1.Size = new System.Drawing.Size(176, 21);
			this.valueComboBox1.TabIndex = 4;
			// 
			// paranthesEndComboBox
			// 
			this.paranthesEndComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.paranthesEndComboBox.FormattingEnabled = true;
			this.paranthesEndComboBox.Location = new System.Drawing.Point(661, 3);
			this.paranthesEndComboBox.Name = "paranthesEndComboBox";
			this.paranthesEndComboBox.Size = new System.Drawing.Size(47, 21);
			this.paranthesEndComboBox.TabIndex = 6;
			// 
			// paranthesBeginComboBox
			// 
			this.paranthesBeginComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.paranthesBeginComboBox.FormattingEnabled = true;
			this.paranthesBeginComboBox.Location = new System.Drawing.Point(85, 3);
			this.paranthesBeginComboBox.Name = "paranthesBeginComboBox";
			this.paranthesBeginComboBox.Size = new System.Drawing.Size(47, 21);
			this.paranthesBeginComboBox.TabIndex = 1;
			// 
			// reloadValuesCheckBox
			// 
			this.reloadValuesCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.reloadValuesCheckBox.Image = global::SQLEventAnalyzer.Properties.Resources.arrow_rotate_clockwise_small1;
			this.reloadValuesCheckBox.Location = new System.Drawing.Point(633, 2);
			this.reloadValuesCheckBox.Name = "reloadValuesCheckBox";
			this.reloadValuesCheckBox.Size = new System.Drawing.Size(24, 23);
			this.reloadValuesCheckBox.TabIndex = 5;
			this.reloadValuesCheckBox.UseVisualStyleBackColor = true;
			this.reloadValuesCheckBox.CheckedChanged += new System.EventHandler(this.ReloadValuesCheckBox_CheckedChanged);
			// 
			// columnComboBox1
			// 
			this.columnComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.columnComboBox1.FormattingEnabled = true;
			this.columnComboBox1.Location = new System.Drawing.Point(138, 3);
			this.columnComboBox1.Name = "columnComboBox1";
			this.columnComboBox1.Size = new System.Drawing.Size(200, 21);
			this.columnComboBox1.TabIndex = 2;
			// 
			// ComboBoxGroupUserControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.reloadValuesCheckBox);
			this.Controls.Add(this.paranthesBeginComboBox);
			this.Controls.Add(this.paranthesEndComboBox);
			this.Controls.Add(this.valueComboBox1);
			this.Controls.Add(this.andOrComboBox1);
			this.Controls.Add(this.enabledCheckBox1);
			this.Controls.Add(this.columnComboBox1);
			this.Controls.Add(this.whereColumnLabel);
			this.Controls.Add(this.operatorComboBox1);
			this.DoubleBuffered = true;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "ComboBoxGroupUserControl";
			this.Size = new System.Drawing.Size(781, 27);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.CheckBox enabledCheckBox1;
	private ComboBoxCustom columnComboBox1;
	private System.Windows.Forms.Label whereColumnLabel;
	private System.Windows.Forms.ComboBox operatorComboBox1;
	private System.Windows.Forms.ComboBox andOrComboBox1;
	private System.Windows.Forms.ComboBox valueComboBox1;
	private System.Windows.Forms.ComboBox paranthesEndComboBox;
	private System.Windows.Forms.ComboBox paranthesBeginComboBox;
	private System.Windows.Forms.CheckBox reloadValuesCheckBox;
}
