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

partial class Filter1ContentUserControl
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
			this.rightCopyStartTimeButton = new System.Windows.Forms.Button();
			this.leftCopyStartTimeButton = new System.Windows.Forms.Button();
			this.fileNameComboBox = new System.Windows.Forms.ComboBox();
			this.fileNameLabel = new System.Windows.Forms.Label();
			this.endReadsTextBox = new System.Windows.Forms.TextBox();
			this.andLabel5 = new System.Windows.Forms.Label();
			this.startReadsTextBox = new System.Windows.Forms.TextBox();
			this.readsLabel = new System.Windows.Forms.Label();
			this.endWritesTextBox = new System.Windows.Forms.TextBox();
			this.andLabel6 = new System.Windows.Forms.Label();
			this.startWritesTextBox = new System.Windows.Forms.TextBox();
			this.writesLabel = new System.Windows.Forms.Label();
			this.endCpuTextBox = new System.Windows.Forms.TextBox();
			this.andLabel7 = new System.Windows.Forms.Label();
			this.startCpuTextBox = new System.Windows.Forms.TextBox();
			this.cpuLabel = new System.Windows.Forms.Label();
			this.endRowsTextBox = new System.Windows.Forms.TextBox();
			this.andLabel8 = new System.Windows.Forms.Label();
			this.startRowsTextBox = new System.Windows.Forms.TextBox();
			this.rowsLabel = new System.Windows.Forms.Label();
			this.endDurationTextBox = new System.Windows.Forms.TextBox();
			this.andLabel3 = new System.Windows.Forms.Label();
			this.startDurationTextBox = new System.Windows.Forms.TextBox();
			this.durationLabel = new System.Windows.Forms.Label();
			this.endSpidTextBox = new System.Windows.Forms.TextBox();
			this.andLabel2 = new System.Windows.Forms.Label();
			this.startSpidTextBox = new System.Windows.Forms.TextBox();
			this.spidLabel = new System.Windows.Forms.Label();
			this.typeComboBox = new System.Windows.Forms.ComboBox();
			this.typeLabel = new System.Windows.Forms.Label();
			this.endIdTextBox = new System.Windows.Forms.TextBox();
			this.andLabel1 = new System.Windows.Forms.Label();
			this.startIdTextBox = new System.Windows.Forms.TextBox();
			this.idLabel = new System.Windows.Forms.Label();
			this.andLabel4 = new System.Windows.Forms.Label();
			this.startTimeLabel = new System.Windows.Forms.Label();
			this.endDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.startDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.enableIdCheckBox = new System.Windows.Forms.CheckBox();
			this.enableStartTimeCheckBox = new System.Windows.Forms.CheckBox();
			this.enableDurationCheckBox = new System.Windows.Forms.CheckBox();
			this.enableReadsCheckBox = new System.Windows.Forms.CheckBox();
			this.enableWritesCheckBox = new System.Windows.Forms.CheckBox();
			this.enableCpuCheckBox = new System.Windows.Forms.CheckBox();
			this.enableRowsCheckBox = new System.Windows.Forms.CheckBox();
			this.enableSpidCheckBox = new System.Windows.Forms.CheckBox();
			this.enableFileNameCheckBox = new System.Windows.Forms.CheckBox();
			this.enableTypeCheckBox = new System.Windows.Forms.CheckBox();
			this.rightCopyIdButton = new System.Windows.Forms.Button();
			this.leftCopyIdButton = new System.Windows.Forms.Button();
			this.rightCopyDurationButton = new System.Windows.Forms.Button();
			this.leftCopyDurationButton = new System.Windows.Forms.Button();
			this.rightCopyReadsButton = new System.Windows.Forms.Button();
			this.leftCopyReadsButton = new System.Windows.Forms.Button();
			this.rightCopyWritesButton = new System.Windows.Forms.Button();
			this.leftCopyWritesButton = new System.Windows.Forms.Button();
			this.rightCopyCpuButton = new System.Windows.Forms.Button();
			this.leftCopyCpuButton = new System.Windows.Forms.Button();
			this.rightCopyRowsButton = new System.Windows.Forms.Button();
			this.leftCopyRowsButton = new System.Windows.Forms.Button();
			this.rightCopySpidButton = new System.Windows.Forms.Button();
			this.leftCopySpidButton = new System.Windows.Forms.Button();
			this.dynamicStartTimeStartCheckBox = new System.Windows.Forms.CheckBox();
			this.dynamicStartTimeEndCheckBox = new System.Windows.Forms.CheckBox();
			this.startDateTimeTextBox = new System.Windows.Forms.TextBox();
			this.endDateTimeTextBox = new System.Windows.Forms.TextBox();
			this.dynamicDateTimeHelpButton1 = new System.Windows.Forms.Button();
			this.dynamicDateTimeHelpButton2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// rightCopyStartTimeButton
			// 
			this.rightCopyStartTimeButton.Enabled = false;
			this.rightCopyStartTimeButton.Image = global::SQLEventAnalyzer.Properties.Resources.arrow_left;
			this.rightCopyStartTimeButton.Location = new System.Drawing.Point(451, 51);
			this.rightCopyStartTimeButton.Name = "rightCopyStartTimeButton";
			this.rightCopyStartTimeButton.Size = new System.Drawing.Size(24, 23);
			this.rightCopyStartTimeButton.TabIndex = 13;
			this.rightCopyStartTimeButton.UseVisualStyleBackColor = true;
			this.rightCopyStartTimeButton.Click += new System.EventHandler(this.RightCopyStartTimeButton_Click);
			// 
			// leftCopyStartTimeButton
			// 
			this.leftCopyStartTimeButton.Enabled = false;
			this.leftCopyStartTimeButton.Image = global::SQLEventAnalyzer.Properties.Resources.arrow_right;
			this.leftCopyStartTimeButton.Location = new System.Drawing.Point(421, 51);
			this.leftCopyStartTimeButton.Name = "leftCopyStartTimeButton";
			this.leftCopyStartTimeButton.Size = new System.Drawing.Size(24, 23);
			this.leftCopyStartTimeButton.TabIndex = 12;
			this.leftCopyStartTimeButton.UseVisualStyleBackColor = true;
			this.leftCopyStartTimeButton.Click += new System.EventHandler(this.LeftCopyStartTimeButton_Click);
			// 
			// fileNameComboBox
			// 
			this.fileNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.fileNameComboBox.Enabled = false;
			this.fileNameComboBox.FormattingEnabled = true;
			this.fileNameComboBox.Location = new System.Drawing.Point(101, 235);
			this.fileNameComboBox.Name = "fileNameComboBox";
			this.fileNameComboBox.Size = new System.Drawing.Size(309, 21);
			this.fileNameComboBox.TabIndex = 44;
			// 
			// fileNameLabel
			// 
			this.fileNameLabel.AutoSize = true;
			this.fileNameLabel.Location = new System.Drawing.Point(-1, 238);
			this.fileNameLabel.Name = "fileNameLabel";
			this.fileNameLabel.Size = new System.Drawing.Size(51, 13);
			this.fileNameLabel.TabIndex = 65;
			this.fileNameLabel.Text = "FileName";
			// 
			// endReadsTextBox
			// 
			this.endReadsTextBox.Enabled = false;
			this.endReadsTextBox.Location = new System.Drawing.Point(272, 105);
			this.endReadsTextBox.Name = "endReadsTextBox";
			this.endReadsTextBox.Size = new System.Drawing.Size(138, 20);
			this.endReadsTextBox.TabIndex = 20;
			this.endReadsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EndReadsTextBox_KeyDown);
			// 
			// andLabel5
			// 
			this.andLabel5.AutoSize = true;
			this.andLabel5.Location = new System.Drawing.Point(244, 108);
			this.andLabel5.Name = "andLabel5";
			this.andLabel5.Size = new System.Drawing.Size(25, 13);
			this.andLabel5.TabIndex = 63;
			this.andLabel5.Text = "and";
			// 
			// startReadsTextBox
			// 
			this.startReadsTextBox.Enabled = false;
			this.startReadsTextBox.Location = new System.Drawing.Point(101, 105);
			this.startReadsTextBox.Name = "startReadsTextBox";
			this.startReadsTextBox.Size = new System.Drawing.Size(138, 20);
			this.startReadsTextBox.TabIndex = 19;
			this.startReadsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StartReadsTextBox_KeyDown);
			// 
			// readsLabel
			// 
			this.readsLabel.AutoSize = true;
			this.readsLabel.Location = new System.Drawing.Point(-1, 108);
			this.readsLabel.Name = "readsLabel";
			this.readsLabel.Size = new System.Drawing.Size(82, 13);
			this.readsLabel.TabIndex = 58;
			this.readsLabel.Text = "Reads between";
			// 
			// endWritesTextBox
			// 
			this.endWritesTextBox.Enabled = false;
			this.endWritesTextBox.Location = new System.Drawing.Point(272, 131);
			this.endWritesTextBox.Name = "endWritesTextBox";
			this.endWritesTextBox.Size = new System.Drawing.Size(138, 20);
			this.endWritesTextBox.TabIndex = 25;
			this.endWritesTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EndWritesTextBox_KeyDown);
			// 
			// andLabel6
			// 
			this.andLabel6.AutoSize = true;
			this.andLabel6.Location = new System.Drawing.Point(244, 134);
			this.andLabel6.Name = "andLabel6";
			this.andLabel6.Size = new System.Drawing.Size(25, 13);
			this.andLabel6.TabIndex = 61;
			this.andLabel6.Text = "and";
			// 
			// startWritesTextBox
			// 
			this.startWritesTextBox.Enabled = false;
			this.startWritesTextBox.Location = new System.Drawing.Point(101, 131);
			this.startWritesTextBox.Name = "startWritesTextBox";
			this.startWritesTextBox.Size = new System.Drawing.Size(138, 20);
			this.startWritesTextBox.TabIndex = 24;
			this.startWritesTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StartWritesTextBox_KeyDown);
			// 
			// writesLabel
			// 
			this.writesLabel.AutoSize = true;
			this.writesLabel.Location = new System.Drawing.Point(0, 134);
			this.writesLabel.Name = "writesLabel";
			this.writesLabel.Size = new System.Drawing.Size(81, 13);
			this.writesLabel.TabIndex = 60;
			this.writesLabel.Text = "Writes between";
			// 
			// endCpuTextBox
			// 
			this.endCpuTextBox.Enabled = false;
			this.endCpuTextBox.Location = new System.Drawing.Point(272, 157);
			this.endCpuTextBox.Name = "endCpuTextBox";
			this.endCpuTextBox.Size = new System.Drawing.Size(138, 20);
			this.endCpuTextBox.TabIndex = 30;
			this.endCpuTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EndCpuTextBox_KeyDown);
			// 
			// andLabel7
			// 
			this.andLabel7.AutoSize = true;
			this.andLabel7.Location = new System.Drawing.Point(244, 160);
			this.andLabel7.Name = "andLabel7";
			this.andLabel7.Size = new System.Drawing.Size(25, 13);
			this.andLabel7.TabIndex = 62;
			this.andLabel7.Text = "and";
			// 
			// startCpuTextBox
			// 
			this.startCpuTextBox.Enabled = false;
			this.startCpuTextBox.Location = new System.Drawing.Point(101, 157);
			this.startCpuTextBox.Name = "startCpuTextBox";
			this.startCpuTextBox.Size = new System.Drawing.Size(138, 20);
			this.startCpuTextBox.TabIndex = 29;
			this.startCpuTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StartCpuTextBox_KeyDown);
			// 
			// cpuLabel
			// 
			this.cpuLabel.AutoSize = true;
			this.cpuLabel.Location = new System.Drawing.Point(0, 160);
			this.cpuLabel.Name = "cpuLabel";
			this.cpuLabel.Size = new System.Drawing.Size(73, 13);
			this.cpuLabel.TabIndex = 57;
			this.cpuLabel.Text = "CPU between";
			// 
			// endRowsTextBox
			// 
			this.endRowsTextBox.Enabled = false;
			this.endRowsTextBox.Location = new System.Drawing.Point(272, 183);
			this.endRowsTextBox.Name = "endRowsTextBox";
			this.endRowsTextBox.Size = new System.Drawing.Size(138, 20);
			this.endRowsTextBox.TabIndex = 35;
			this.endRowsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EndRowsTextBox_KeyDown);
			// 
			// andLabel8
			// 
			this.andLabel8.AutoSize = true;
			this.andLabel8.Location = new System.Drawing.Point(244, 186);
			this.andLabel8.Name = "andLabel8";
			this.andLabel8.Size = new System.Drawing.Size(25, 13);
			this.andLabel8.TabIndex = 64;
			this.andLabel8.Text = "and";
			// 
			// startRowsTextBox
			// 
			this.startRowsTextBox.Enabled = false;
			this.startRowsTextBox.Location = new System.Drawing.Point(101, 183);
			this.startRowsTextBox.Name = "startRowsTextBox";
			this.startRowsTextBox.Size = new System.Drawing.Size(138, 20);
			this.startRowsTextBox.TabIndex = 34;
			this.startRowsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StartRowsTextBox_KeyDown);
			// 
			// rowsLabel
			// 
			this.rowsLabel.AutoSize = true;
			this.rowsLabel.Location = new System.Drawing.Point(0, 186);
			this.rowsLabel.Name = "rowsLabel";
			this.rowsLabel.Size = new System.Drawing.Size(78, 13);
			this.rowsLabel.TabIndex = 59;
			this.rowsLabel.Text = "Rows between";
			// 
			// endDurationTextBox
			// 
			this.endDurationTextBox.Enabled = false;
			this.endDurationTextBox.Location = new System.Drawing.Point(272, 27);
			this.endDurationTextBox.Name = "endDurationTextBox";
			this.endDurationTextBox.Size = new System.Drawing.Size(138, 20);
			this.endDurationTextBox.TabIndex = 6;
			this.endDurationTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EndDurationTextBox_KeyDown);
			// 
			// andLabel3
			// 
			this.andLabel3.AutoSize = true;
			this.andLabel3.Location = new System.Drawing.Point(244, 30);
			this.andLabel3.Name = "andLabel3";
			this.andLabel3.Size = new System.Drawing.Size(25, 13);
			this.andLabel3.TabIndex = 56;
			this.andLabel3.Text = "and";
			// 
			// startDurationTextBox
			// 
			this.startDurationTextBox.Enabled = false;
			this.startDurationTextBox.Location = new System.Drawing.Point(101, 27);
			this.startDurationTextBox.Name = "startDurationTextBox";
			this.startDurationTextBox.Size = new System.Drawing.Size(138, 20);
			this.startDurationTextBox.TabIndex = 5;
			this.startDurationTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StartDurationTextBox_KeyDown);
			// 
			// durationLabel
			// 
			this.durationLabel.AutoSize = true;
			this.durationLabel.Location = new System.Drawing.Point(-1, 30);
			this.durationLabel.Name = "durationLabel";
			this.durationLabel.Size = new System.Drawing.Size(91, 13);
			this.durationLabel.TabIndex = 54;
			this.durationLabel.Text = "Duration between";
			// 
			// endSpidTextBox
			// 
			this.endSpidTextBox.Enabled = false;
			this.endSpidTextBox.Location = new System.Drawing.Point(272, 209);
			this.endSpidTextBox.Name = "endSpidTextBox";
			this.endSpidTextBox.Size = new System.Drawing.Size(138, 20);
			this.endSpidTextBox.TabIndex = 40;
			this.endSpidTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EndSpidTextBox_KeyDown);
			// 
			// andLabel2
			// 
			this.andLabel2.AutoSize = true;
			this.andLabel2.Location = new System.Drawing.Point(244, 212);
			this.andLabel2.Name = "andLabel2";
			this.andLabel2.Size = new System.Drawing.Size(25, 13);
			this.andLabel2.TabIndex = 51;
			this.andLabel2.Text = "and";
			// 
			// startSpidTextBox
			// 
			this.startSpidTextBox.Enabled = false;
			this.startSpidTextBox.Location = new System.Drawing.Point(101, 209);
			this.startSpidTextBox.Name = "startSpidTextBox";
			this.startSpidTextBox.Size = new System.Drawing.Size(138, 20);
			this.startSpidTextBox.TabIndex = 39;
			this.startSpidTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StartSpidTextBox_KeyDown);
			// 
			// spidLabel
			// 
			this.spidLabel.AutoSize = true;
			this.spidLabel.Location = new System.Drawing.Point(-1, 212);
			this.spidLabel.Name = "spidLabel";
			this.spidLabel.Size = new System.Drawing.Size(76, 13);
			this.spidLabel.TabIndex = 48;
			this.spidLabel.Text = "SPID between";
			// 
			// typeComboBox
			// 
			this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.typeComboBox.Enabled = false;
			this.typeComboBox.FormattingEnabled = true;
			this.typeComboBox.Location = new System.Drawing.Point(101, 262);
			this.typeComboBox.Name = "typeComboBox";
			this.typeComboBox.Size = new System.Drawing.Size(309, 21);
			this.typeComboBox.TabIndex = 46;
			// 
			// typeLabel
			// 
			this.typeLabel.AutoSize = true;
			this.typeLabel.Location = new System.Drawing.Point(-1, 266);
			this.typeLabel.Name = "typeLabel";
			this.typeLabel.Size = new System.Drawing.Size(31, 13);
			this.typeLabel.TabIndex = 45;
			this.typeLabel.Text = "Type";
			// 
			// endIdTextBox
			// 
			this.endIdTextBox.Enabled = false;
			this.endIdTextBox.Location = new System.Drawing.Point(272, 1);
			this.endIdTextBox.Name = "endIdTextBox";
			this.endIdTextBox.Size = new System.Drawing.Size(138, 20);
			this.endIdTextBox.TabIndex = 1;
			this.endIdTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EndIdTextBox_KeyDown);
			// 
			// andLabel1
			// 
			this.andLabel1.AutoSize = true;
			this.andLabel1.Location = new System.Drawing.Point(244, 4);
			this.andLabel1.Name = "andLabel1";
			this.andLabel1.Size = new System.Drawing.Size(25, 13);
			this.andLabel1.TabIndex = 42;
			this.andLabel1.Text = "and";
			// 
			// startIdTextBox
			// 
			this.startIdTextBox.Enabled = false;
			this.startIdTextBox.Location = new System.Drawing.Point(101, 1);
			this.startIdTextBox.Name = "startIdTextBox";
			this.startIdTextBox.Size = new System.Drawing.Size(138, 20);
			this.startIdTextBox.TabIndex = 0;
			this.startIdTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StartIdTextBox_KeyDown);
			// 
			// idLabel
			// 
			this.idLabel.AutoSize = true;
			this.idLabel.Location = new System.Drawing.Point(-1, 4);
			this.idLabel.Name = "idLabel";
			this.idLabel.Size = new System.Drawing.Size(60, 13);
			this.idLabel.TabIndex = 39;
			this.idLabel.Text = "Id between";
			// 
			// andLabel4
			// 
			this.andLabel4.AutoSize = true;
			this.andLabel4.Location = new System.Drawing.Point(244, 56);
			this.andLabel4.Name = "andLabel4";
			this.andLabel4.Size = new System.Drawing.Size(25, 13);
			this.andLabel4.TabIndex = 32;
			this.andLabel4.Text = "and";
			// 
			// startTimeLabel
			// 
			this.startTimeLabel.AutoSize = true;
			this.startTimeLabel.Location = new System.Drawing.Point(-1, 56);
			this.startTimeLabel.Name = "startTimeLabel";
			this.startTimeLabel.Size = new System.Drawing.Size(96, 13);
			this.startTimeLabel.TabIndex = 29;
			this.startTimeLabel.Text = "StartTime between";
			// 
			// endDateTimePicker
			// 
			this.endDateTimePicker.CustomFormat = "";
			this.endDateTimePicker.Enabled = false;
			this.endDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.endDateTimePicker.Location = new System.Drawing.Point(272, 53);
			this.endDateTimePicker.Name = "endDateTimePicker";
			this.endDateTimePicker.Size = new System.Drawing.Size(138, 20);
			this.endDateTimePicker.TabIndex = 11;
			this.endDateTimePicker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EndDateTimePicker_KeyDown);
			// 
			// startDateTimePicker
			// 
			this.startDateTimePicker.CustomFormat = "";
			this.startDateTimePicker.Enabled = false;
			this.startDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.startDateTimePicker.Location = new System.Drawing.Point(101, 53);
			this.startDateTimePicker.Name = "startDateTimePicker";
			this.startDateTimePicker.Size = new System.Drawing.Size(138, 20);
			this.startDateTimePicker.TabIndex = 10;
			this.startDateTimePicker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StartDateTimePicker_KeyDown);
			// 
			// enableIdCheckBox
			// 
			this.enableIdCheckBox.AutoSize = true;
			this.enableIdCheckBox.Location = new System.Drawing.Point(483, 3);
			this.enableIdCheckBox.Name = "enableIdCheckBox";
			this.enableIdCheckBox.Size = new System.Drawing.Size(65, 17);
			this.enableIdCheckBox.TabIndex = 4;
			this.enableIdCheckBox.Text = "Enabled";
			this.enableIdCheckBox.UseVisualStyleBackColor = true;
			this.enableIdCheckBox.CheckedChanged += new System.EventHandler(this.EnableIdCheckBox_CheckedChanged);
			// 
			// enableStartTimeCheckBox
			// 
			this.enableStartTimeCheckBox.AutoSize = true;
			this.enableStartTimeCheckBox.Location = new System.Drawing.Point(483, 55);
			this.enableStartTimeCheckBox.Name = "enableStartTimeCheckBox";
			this.enableStartTimeCheckBox.Size = new System.Drawing.Size(65, 17);
			this.enableStartTimeCheckBox.TabIndex = 14;
			this.enableStartTimeCheckBox.Text = "Enabled";
			this.enableStartTimeCheckBox.UseVisualStyleBackColor = true;
			this.enableStartTimeCheckBox.CheckedChanged += new System.EventHandler(this.EnableStartTimeCheckBox_CheckedChanged);
			// 
			// enableDurationCheckBox
			// 
			this.enableDurationCheckBox.AutoSize = true;
			this.enableDurationCheckBox.Location = new System.Drawing.Point(483, 29);
			this.enableDurationCheckBox.Name = "enableDurationCheckBox";
			this.enableDurationCheckBox.Size = new System.Drawing.Size(65, 17);
			this.enableDurationCheckBox.TabIndex = 9;
			this.enableDurationCheckBox.Text = "Enabled";
			this.enableDurationCheckBox.UseVisualStyleBackColor = true;
			this.enableDurationCheckBox.CheckedChanged += new System.EventHandler(this.EnableDurationCheckBox_CheckedChanged);
			// 
			// enableReadsCheckBox
			// 
			this.enableReadsCheckBox.AutoSize = true;
			this.enableReadsCheckBox.Location = new System.Drawing.Point(483, 107);
			this.enableReadsCheckBox.Name = "enableReadsCheckBox";
			this.enableReadsCheckBox.Size = new System.Drawing.Size(65, 17);
			this.enableReadsCheckBox.TabIndex = 23;
			this.enableReadsCheckBox.Text = "Enabled";
			this.enableReadsCheckBox.UseVisualStyleBackColor = true;
			this.enableReadsCheckBox.CheckedChanged += new System.EventHandler(this.EnableReadsCheckBox_CheckedChanged);
			// 
			// enableWritesCheckBox
			// 
			this.enableWritesCheckBox.AutoSize = true;
			this.enableWritesCheckBox.Location = new System.Drawing.Point(483, 133);
			this.enableWritesCheckBox.Name = "enableWritesCheckBox";
			this.enableWritesCheckBox.Size = new System.Drawing.Size(65, 17);
			this.enableWritesCheckBox.TabIndex = 28;
			this.enableWritesCheckBox.Text = "Enabled";
			this.enableWritesCheckBox.UseVisualStyleBackColor = true;
			this.enableWritesCheckBox.CheckedChanged += new System.EventHandler(this.EnableWritesCheckBox_CheckedChanged);
			// 
			// enableCpuCheckBox
			// 
			this.enableCpuCheckBox.AutoSize = true;
			this.enableCpuCheckBox.Location = new System.Drawing.Point(483, 159);
			this.enableCpuCheckBox.Name = "enableCpuCheckBox";
			this.enableCpuCheckBox.Size = new System.Drawing.Size(65, 17);
			this.enableCpuCheckBox.TabIndex = 33;
			this.enableCpuCheckBox.Text = "Enabled";
			this.enableCpuCheckBox.UseVisualStyleBackColor = true;
			this.enableCpuCheckBox.CheckedChanged += new System.EventHandler(this.EnableCpuCheckBox_CheckedChanged);
			// 
			// enableRowsCheckBox
			// 
			this.enableRowsCheckBox.AutoSize = true;
			this.enableRowsCheckBox.Location = new System.Drawing.Point(483, 185);
			this.enableRowsCheckBox.Name = "enableRowsCheckBox";
			this.enableRowsCheckBox.Size = new System.Drawing.Size(65, 17);
			this.enableRowsCheckBox.TabIndex = 38;
			this.enableRowsCheckBox.Text = "Enabled";
			this.enableRowsCheckBox.UseVisualStyleBackColor = true;
			this.enableRowsCheckBox.CheckedChanged += new System.EventHandler(this.EnableRowsCheckBox_CheckedChanged);
			// 
			// enableSpidCheckBox
			// 
			this.enableSpidCheckBox.AutoSize = true;
			this.enableSpidCheckBox.Location = new System.Drawing.Point(483, 211);
			this.enableSpidCheckBox.Name = "enableSpidCheckBox";
			this.enableSpidCheckBox.Size = new System.Drawing.Size(65, 17);
			this.enableSpidCheckBox.TabIndex = 43;
			this.enableSpidCheckBox.Text = "Enabled";
			this.enableSpidCheckBox.UseVisualStyleBackColor = true;
			this.enableSpidCheckBox.CheckedChanged += new System.EventHandler(this.EnableSpidCheckBox_CheckedChanged);
			// 
			// enableFileNameCheckBox
			// 
			this.enableFileNameCheckBox.AutoSize = true;
			this.enableFileNameCheckBox.Location = new System.Drawing.Point(483, 237);
			this.enableFileNameCheckBox.Name = "enableFileNameCheckBox";
			this.enableFileNameCheckBox.Size = new System.Drawing.Size(65, 17);
			this.enableFileNameCheckBox.TabIndex = 45;
			this.enableFileNameCheckBox.Text = "Enabled";
			this.enableFileNameCheckBox.UseVisualStyleBackColor = true;
			this.enableFileNameCheckBox.CheckedChanged += new System.EventHandler(this.EnableFileNameCheckBox_CheckedChanged);
			// 
			// enableTypeCheckBox
			// 
			this.enableTypeCheckBox.AutoSize = true;
			this.enableTypeCheckBox.Location = new System.Drawing.Point(483, 264);
			this.enableTypeCheckBox.Name = "enableTypeCheckBox";
			this.enableTypeCheckBox.Size = new System.Drawing.Size(65, 17);
			this.enableTypeCheckBox.TabIndex = 47;
			this.enableTypeCheckBox.Text = "Enabled";
			this.enableTypeCheckBox.UseVisualStyleBackColor = true;
			this.enableTypeCheckBox.CheckedChanged += new System.EventHandler(this.EnableTypeCheckBox_CheckedChanged);
			// 
			// rightCopyIdButton
			// 
			this.rightCopyIdButton.Enabled = false;
			this.rightCopyIdButton.Image = global::SQLEventAnalyzer.Properties.Resources.arrow_left;
			this.rightCopyIdButton.Location = new System.Drawing.Point(451, -1);
			this.rightCopyIdButton.Name = "rightCopyIdButton";
			this.rightCopyIdButton.Size = new System.Drawing.Size(24, 23);
			this.rightCopyIdButton.TabIndex = 3;
			this.rightCopyIdButton.UseVisualStyleBackColor = true;
			this.rightCopyIdButton.Click += new System.EventHandler(this.RightCopyIdButton_Click);
			// 
			// leftCopyIdButton
			// 
			this.leftCopyIdButton.Enabled = false;
			this.leftCopyIdButton.Image = global::SQLEventAnalyzer.Properties.Resources.arrow_right;
			this.leftCopyIdButton.Location = new System.Drawing.Point(421, -1);
			this.leftCopyIdButton.Name = "leftCopyIdButton";
			this.leftCopyIdButton.Size = new System.Drawing.Size(24, 23);
			this.leftCopyIdButton.TabIndex = 2;
			this.leftCopyIdButton.UseVisualStyleBackColor = true;
			this.leftCopyIdButton.Click += new System.EventHandler(this.LeftCopyIdButton_Click);
			// 
			// rightCopyDurationButton
			// 
			this.rightCopyDurationButton.Enabled = false;
			this.rightCopyDurationButton.Image = global::SQLEventAnalyzer.Properties.Resources.arrow_left;
			this.rightCopyDurationButton.Location = new System.Drawing.Point(451, 25);
			this.rightCopyDurationButton.Name = "rightCopyDurationButton";
			this.rightCopyDurationButton.Size = new System.Drawing.Size(24, 23);
			this.rightCopyDurationButton.TabIndex = 8;
			this.rightCopyDurationButton.UseVisualStyleBackColor = true;
			this.rightCopyDurationButton.Click += new System.EventHandler(this.RightCopyDurationButton_Click);
			// 
			// leftCopyDurationButton
			// 
			this.leftCopyDurationButton.Enabled = false;
			this.leftCopyDurationButton.Image = global::SQLEventAnalyzer.Properties.Resources.arrow_right;
			this.leftCopyDurationButton.Location = new System.Drawing.Point(421, 25);
			this.leftCopyDurationButton.Name = "leftCopyDurationButton";
			this.leftCopyDurationButton.Size = new System.Drawing.Size(24, 23);
			this.leftCopyDurationButton.TabIndex = 7;
			this.leftCopyDurationButton.UseVisualStyleBackColor = true;
			this.leftCopyDurationButton.Click += new System.EventHandler(this.LeftCopyDurationButton_Click);
			// 
			// rightCopyReadsButton
			// 
			this.rightCopyReadsButton.Enabled = false;
			this.rightCopyReadsButton.Image = global::SQLEventAnalyzer.Properties.Resources.arrow_left;
			this.rightCopyReadsButton.Location = new System.Drawing.Point(451, 103);
			this.rightCopyReadsButton.Name = "rightCopyReadsButton";
			this.rightCopyReadsButton.Size = new System.Drawing.Size(24, 23);
			this.rightCopyReadsButton.TabIndex = 22;
			this.rightCopyReadsButton.UseVisualStyleBackColor = true;
			this.rightCopyReadsButton.Click += new System.EventHandler(this.RightCopyReadsButton_Click);
			// 
			// leftCopyReadsButton
			// 
			this.leftCopyReadsButton.Enabled = false;
			this.leftCopyReadsButton.Image = global::SQLEventAnalyzer.Properties.Resources.arrow_right;
			this.leftCopyReadsButton.Location = new System.Drawing.Point(421, 103);
			this.leftCopyReadsButton.Name = "leftCopyReadsButton";
			this.leftCopyReadsButton.Size = new System.Drawing.Size(24, 23);
			this.leftCopyReadsButton.TabIndex = 21;
			this.leftCopyReadsButton.UseVisualStyleBackColor = true;
			this.leftCopyReadsButton.Click += new System.EventHandler(this.LeftCopyReadsButton_Click);
			// 
			// rightCopyWritesButton
			// 
			this.rightCopyWritesButton.Enabled = false;
			this.rightCopyWritesButton.Image = global::SQLEventAnalyzer.Properties.Resources.arrow_left;
			this.rightCopyWritesButton.Location = new System.Drawing.Point(451, 129);
			this.rightCopyWritesButton.Name = "rightCopyWritesButton";
			this.rightCopyWritesButton.Size = new System.Drawing.Size(24, 23);
			this.rightCopyWritesButton.TabIndex = 27;
			this.rightCopyWritesButton.UseVisualStyleBackColor = true;
			this.rightCopyWritesButton.Click += new System.EventHandler(this.RightCopyWritesButton_Click);
			// 
			// leftCopyWritesButton
			// 
			this.leftCopyWritesButton.Enabled = false;
			this.leftCopyWritesButton.Image = global::SQLEventAnalyzer.Properties.Resources.arrow_right;
			this.leftCopyWritesButton.Location = new System.Drawing.Point(421, 129);
			this.leftCopyWritesButton.Name = "leftCopyWritesButton";
			this.leftCopyWritesButton.Size = new System.Drawing.Size(24, 23);
			this.leftCopyWritesButton.TabIndex = 26;
			this.leftCopyWritesButton.UseVisualStyleBackColor = true;
			this.leftCopyWritesButton.Click += new System.EventHandler(this.LeftCopyWritesButton_Click);
			// 
			// rightCopyCpuButton
			// 
			this.rightCopyCpuButton.Enabled = false;
			this.rightCopyCpuButton.Image = global::SQLEventAnalyzer.Properties.Resources.arrow_left;
			this.rightCopyCpuButton.Location = new System.Drawing.Point(451, 155);
			this.rightCopyCpuButton.Name = "rightCopyCpuButton";
			this.rightCopyCpuButton.Size = new System.Drawing.Size(24, 23);
			this.rightCopyCpuButton.TabIndex = 32;
			this.rightCopyCpuButton.UseVisualStyleBackColor = true;
			this.rightCopyCpuButton.Click += new System.EventHandler(this.RightCopyCpuButton_Click);
			// 
			// leftCopyCpuButton
			// 
			this.leftCopyCpuButton.Enabled = false;
			this.leftCopyCpuButton.Image = global::SQLEventAnalyzer.Properties.Resources.arrow_right;
			this.leftCopyCpuButton.Location = new System.Drawing.Point(421, 155);
			this.leftCopyCpuButton.Name = "leftCopyCpuButton";
			this.leftCopyCpuButton.Size = new System.Drawing.Size(24, 23);
			this.leftCopyCpuButton.TabIndex = 31;
			this.leftCopyCpuButton.UseVisualStyleBackColor = true;
			this.leftCopyCpuButton.Click += new System.EventHandler(this.LeftCopyCpuButton_Click);
			// 
			// rightCopyRowsButton
			// 
			this.rightCopyRowsButton.Enabled = false;
			this.rightCopyRowsButton.Image = global::SQLEventAnalyzer.Properties.Resources.arrow_left;
			this.rightCopyRowsButton.Location = new System.Drawing.Point(451, 181);
			this.rightCopyRowsButton.Name = "rightCopyRowsButton";
			this.rightCopyRowsButton.Size = new System.Drawing.Size(24, 23);
			this.rightCopyRowsButton.TabIndex = 37;
			this.rightCopyRowsButton.UseVisualStyleBackColor = true;
			this.rightCopyRowsButton.Click += new System.EventHandler(this.RightCopyRowsButton_Click);
			// 
			// leftCopyRowsButton
			// 
			this.leftCopyRowsButton.Enabled = false;
			this.leftCopyRowsButton.Image = global::SQLEventAnalyzer.Properties.Resources.arrow_right;
			this.leftCopyRowsButton.Location = new System.Drawing.Point(421, 181);
			this.leftCopyRowsButton.Name = "leftCopyRowsButton";
			this.leftCopyRowsButton.Size = new System.Drawing.Size(24, 23);
			this.leftCopyRowsButton.TabIndex = 36;
			this.leftCopyRowsButton.UseVisualStyleBackColor = true;
			this.leftCopyRowsButton.Click += new System.EventHandler(this.LeftCopyRowsButton_Click);
			// 
			// rightCopySpidButton
			// 
			this.rightCopySpidButton.Enabled = false;
			this.rightCopySpidButton.Image = global::SQLEventAnalyzer.Properties.Resources.arrow_left;
			this.rightCopySpidButton.Location = new System.Drawing.Point(451, 207);
			this.rightCopySpidButton.Name = "rightCopySpidButton";
			this.rightCopySpidButton.Size = new System.Drawing.Size(24, 23);
			this.rightCopySpidButton.TabIndex = 42;
			this.rightCopySpidButton.UseVisualStyleBackColor = true;
			this.rightCopySpidButton.Click += new System.EventHandler(this.RightCopySpidButton_Click);
			// 
			// leftCopySpidButton
			// 
			this.leftCopySpidButton.Enabled = false;
			this.leftCopySpidButton.Image = global::SQLEventAnalyzer.Properties.Resources.arrow_right;
			this.leftCopySpidButton.Location = new System.Drawing.Point(421, 207);
			this.leftCopySpidButton.Name = "leftCopySpidButton";
			this.leftCopySpidButton.Size = new System.Drawing.Size(24, 23);
			this.leftCopySpidButton.TabIndex = 41;
			this.leftCopySpidButton.UseVisualStyleBackColor = true;
			this.leftCopySpidButton.Click += new System.EventHandler(this.LeftCopySpidButton_Click);
			// 
			// dynamicStartTimeStartCheckBox
			// 
			this.dynamicStartTimeStartCheckBox.AutoSize = true;
			this.dynamicStartTimeStartCheckBox.Enabled = false;
			this.dynamicStartTimeStartCheckBox.Location = new System.Drawing.Point(101, 79);
			this.dynamicStartTimeStartCheckBox.Name = "dynamicStartTimeStartCheckBox";
			this.dynamicStartTimeStartCheckBox.Size = new System.Drawing.Size(84, 17);
			this.dynamicStartTimeStartCheckBox.TabIndex = 15;
			this.dynamicStartTimeStartCheckBox.Text = "Set dynamic";
			this.dynamicStartTimeStartCheckBox.UseVisualStyleBackColor = true;
			this.dynamicStartTimeStartCheckBox.CheckedChanged += new System.EventHandler(this.DynamicStartTimeStartCheckBox_CheckedChanged);
			// 
			// dynamicStartTimeEndCheckBox
			// 
			this.dynamicStartTimeEndCheckBox.AutoSize = true;
			this.dynamicStartTimeEndCheckBox.Enabled = false;
			this.dynamicStartTimeEndCheckBox.Location = new System.Drawing.Point(272, 79);
			this.dynamicStartTimeEndCheckBox.Name = "dynamicStartTimeEndCheckBox";
			this.dynamicStartTimeEndCheckBox.Size = new System.Drawing.Size(84, 17);
			this.dynamicStartTimeEndCheckBox.TabIndex = 17;
			this.dynamicStartTimeEndCheckBox.Text = "Set dynamic";
			this.dynamicStartTimeEndCheckBox.UseVisualStyleBackColor = true;
			this.dynamicStartTimeEndCheckBox.CheckedChanged += new System.EventHandler(this.DynamicStartTimeEndCheckBox_CheckedChanged);
			// 
			// startDateTimeTextBox
			// 
			this.startDateTimeTextBox.Enabled = false;
			this.startDateTimeTextBox.Location = new System.Drawing.Point(101, 53);
			this.startDateTimeTextBox.Name = "startDateTimeTextBox";
			this.startDateTimeTextBox.Size = new System.Drawing.Size(138, 20);
			this.startDateTimeTextBox.TabIndex = 10;
			this.startDateTimeTextBox.Visible = false;
			this.startDateTimeTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StartDateTimeTextBox_KeyDown);
			// 
			// endDateTimeTextBox
			// 
			this.endDateTimeTextBox.Enabled = false;
			this.endDateTimeTextBox.Location = new System.Drawing.Point(272, 53);
			this.endDateTimeTextBox.Name = "endDateTimeTextBox";
			this.endDateTimeTextBox.Size = new System.Drawing.Size(138, 20);
			this.endDateTimeTextBox.TabIndex = 11;
			this.endDateTimeTextBox.Visible = false;
			this.endDateTimeTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EndDateTimeTextBox_KeyDown);
			// 
			// dynamicDateTimeHelpButton1
			// 
			this.dynamicDateTimeHelpButton1.Enabled = false;
			this.dynamicDateTimeHelpButton1.Image = global::SQLEventAnalyzer.Properties.Resources.help_small;
			this.dynamicDateTimeHelpButton1.Location = new System.Drawing.Point(191, 75);
			this.dynamicDateTimeHelpButton1.Name = "dynamicDateTimeHelpButton1";
			this.dynamicDateTimeHelpButton1.Size = new System.Drawing.Size(24, 23);
			this.dynamicDateTimeHelpButton1.TabIndex = 16;
			this.dynamicDateTimeHelpButton1.UseVisualStyleBackColor = true;
			this.dynamicDateTimeHelpButton1.Click += new System.EventHandler(this.DynamicDateTimeHelpButton1_Click);
			// 
			// dynamicDateTimeHelpButton2
			// 
			this.dynamicDateTimeHelpButton2.Enabled = false;
			this.dynamicDateTimeHelpButton2.Image = global::SQLEventAnalyzer.Properties.Resources.help_small;
			this.dynamicDateTimeHelpButton2.Location = new System.Drawing.Point(362, 75);
			this.dynamicDateTimeHelpButton2.Name = "dynamicDateTimeHelpButton2";
			this.dynamicDateTimeHelpButton2.Size = new System.Drawing.Size(24, 23);
			this.dynamicDateTimeHelpButton2.TabIndex = 18;
			this.dynamicDateTimeHelpButton2.UseVisualStyleBackColor = true;
			this.dynamicDateTimeHelpButton2.Click += new System.EventHandler(this.DynamicDateTimeHelpButton2_Click);
			// 
			// Filter1ContentUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dynamicDateTimeHelpButton2);
			this.Controls.Add(this.dynamicDateTimeHelpButton1);
			this.Controls.Add(this.endDateTimeTextBox);
			this.Controls.Add(this.startDateTimeTextBox);
			this.Controls.Add(this.dynamicStartTimeEndCheckBox);
			this.Controls.Add(this.dynamicStartTimeStartCheckBox);
			this.Controls.Add(this.rightCopySpidButton);
			this.Controls.Add(this.leftCopySpidButton);
			this.Controls.Add(this.rightCopyRowsButton);
			this.Controls.Add(this.leftCopyRowsButton);
			this.Controls.Add(this.rightCopyCpuButton);
			this.Controls.Add(this.leftCopyCpuButton);
			this.Controls.Add(this.rightCopyWritesButton);
			this.Controls.Add(this.leftCopyWritesButton);
			this.Controls.Add(this.rightCopyReadsButton);
			this.Controls.Add(this.leftCopyReadsButton);
			this.Controls.Add(this.rightCopyDurationButton);
			this.Controls.Add(this.leftCopyDurationButton);
			this.Controls.Add(this.rightCopyIdButton);
			this.Controls.Add(this.leftCopyIdButton);
			this.Controls.Add(this.enableTypeCheckBox);
			this.Controls.Add(this.enableFileNameCheckBox);
			this.Controls.Add(this.enableSpidCheckBox);
			this.Controls.Add(this.enableRowsCheckBox);
			this.Controls.Add(this.enableCpuCheckBox);
			this.Controls.Add(this.enableWritesCheckBox);
			this.Controls.Add(this.enableReadsCheckBox);
			this.Controls.Add(this.enableDurationCheckBox);
			this.Controls.Add(this.enableStartTimeCheckBox);
			this.Controls.Add(this.enableIdCheckBox);
			this.Controls.Add(this.rightCopyStartTimeButton);
			this.Controls.Add(this.leftCopyStartTimeButton);
			this.Controls.Add(this.fileNameComboBox);
			this.Controls.Add(this.fileNameLabel);
			this.Controls.Add(this.endReadsTextBox);
			this.Controls.Add(this.andLabel5);
			this.Controls.Add(this.startReadsTextBox);
			this.Controls.Add(this.readsLabel);
			this.Controls.Add(this.endWritesTextBox);
			this.Controls.Add(this.andLabel6);
			this.Controls.Add(this.startWritesTextBox);
			this.Controls.Add(this.writesLabel);
			this.Controls.Add(this.endCpuTextBox);
			this.Controls.Add(this.andLabel7);
			this.Controls.Add(this.startCpuTextBox);
			this.Controls.Add(this.cpuLabel);
			this.Controls.Add(this.endRowsTextBox);
			this.Controls.Add(this.andLabel8);
			this.Controls.Add(this.startRowsTextBox);
			this.Controls.Add(this.rowsLabel);
			this.Controls.Add(this.endDurationTextBox);
			this.Controls.Add(this.andLabel3);
			this.Controls.Add(this.startDurationTextBox);
			this.Controls.Add(this.durationLabel);
			this.Controls.Add(this.endSpidTextBox);
			this.Controls.Add(this.andLabel2);
			this.Controls.Add(this.startSpidTextBox);
			this.Controls.Add(this.spidLabel);
			this.Controls.Add(this.typeComboBox);
			this.Controls.Add(this.typeLabel);
			this.Controls.Add(this.endIdTextBox);
			this.Controls.Add(this.andLabel1);
			this.Controls.Add(this.startIdTextBox);
			this.Controls.Add(this.idLabel);
			this.Controls.Add(this.andLabel4);
			this.Controls.Add(this.startTimeLabel);
			this.Controls.Add(this.endDateTimePicker);
			this.Controls.Add(this.startDateTimePicker);
			this.Name = "Filter1ContentUserControl";
			this.Size = new System.Drawing.Size(564, 291);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Button rightCopyStartTimeButton;
	private System.Windows.Forms.Button leftCopyStartTimeButton;
	private System.Windows.Forms.ComboBox fileNameComboBox;
	private System.Windows.Forms.Label fileNameLabel;
	private System.Windows.Forms.TextBox endReadsTextBox;
	private System.Windows.Forms.Label andLabel5;
	private System.Windows.Forms.TextBox startReadsTextBox;
	private System.Windows.Forms.Label readsLabel;
	private System.Windows.Forms.TextBox endWritesTextBox;
	private System.Windows.Forms.Label andLabel6;
	private System.Windows.Forms.TextBox startWritesTextBox;
	private System.Windows.Forms.Label writesLabel;
	private System.Windows.Forms.TextBox endCpuTextBox;
	private System.Windows.Forms.Label andLabel7;
	private System.Windows.Forms.TextBox startCpuTextBox;
	private System.Windows.Forms.Label cpuLabel;
	private System.Windows.Forms.TextBox endRowsTextBox;
	private System.Windows.Forms.Label andLabel8;
	private System.Windows.Forms.TextBox startRowsTextBox;
	private System.Windows.Forms.Label rowsLabel;
	private System.Windows.Forms.TextBox endDurationTextBox;
	private System.Windows.Forms.Label andLabel3;
	private System.Windows.Forms.TextBox startDurationTextBox;
	private System.Windows.Forms.Label durationLabel;
	private System.Windows.Forms.TextBox endSpidTextBox;
	private System.Windows.Forms.Label andLabel2;
	private System.Windows.Forms.TextBox startSpidTextBox;
	private System.Windows.Forms.Label spidLabel;
	private System.Windows.Forms.ComboBox typeComboBox;
	private System.Windows.Forms.Label typeLabel;
	private System.Windows.Forms.TextBox endIdTextBox;
	private System.Windows.Forms.Label andLabel1;
	private System.Windows.Forms.TextBox startIdTextBox;
	private System.Windows.Forms.Label idLabel;
	private System.Windows.Forms.Label andLabel4;
	private System.Windows.Forms.Label startTimeLabel;
	private System.Windows.Forms.DateTimePicker endDateTimePicker;
	private System.Windows.Forms.DateTimePicker startDateTimePicker;
	private System.Windows.Forms.CheckBox enableIdCheckBox;
	private System.Windows.Forms.CheckBox enableStartTimeCheckBox;
	private System.Windows.Forms.CheckBox enableDurationCheckBox;
	private System.Windows.Forms.CheckBox enableReadsCheckBox;
	private System.Windows.Forms.CheckBox enableWritesCheckBox;
	private System.Windows.Forms.CheckBox enableCpuCheckBox;
	private System.Windows.Forms.CheckBox enableRowsCheckBox;
	private System.Windows.Forms.CheckBox enableSpidCheckBox;
	private System.Windows.Forms.CheckBox enableFileNameCheckBox;
	private System.Windows.Forms.CheckBox enableTypeCheckBox;
	private System.Windows.Forms.Button rightCopyIdButton;
	private System.Windows.Forms.Button leftCopyIdButton;
	private System.Windows.Forms.Button rightCopyDurationButton;
	private System.Windows.Forms.Button leftCopyDurationButton;
	private System.Windows.Forms.Button rightCopyReadsButton;
	private System.Windows.Forms.Button leftCopyReadsButton;
	private System.Windows.Forms.Button rightCopyWritesButton;
	private System.Windows.Forms.Button leftCopyWritesButton;
	private System.Windows.Forms.Button rightCopyCpuButton;
	private System.Windows.Forms.Button leftCopyCpuButton;
	private System.Windows.Forms.Button rightCopyRowsButton;
	private System.Windows.Forms.Button leftCopyRowsButton;
	private System.Windows.Forms.Button rightCopySpidButton;
	private System.Windows.Forms.Button leftCopySpidButton;
	private System.Windows.Forms.CheckBox dynamicStartTimeStartCheckBox;
	private System.Windows.Forms.CheckBox dynamicStartTimeEndCheckBox;
	private System.Windows.Forms.TextBox startDateTimeTextBox;
	private System.Windows.Forms.TextBox endDateTimeTextBox;
	private System.Windows.Forms.Button dynamicDateTimeHelpButton1;
	private System.Windows.Forms.Button dynamicDateTimeHelpButton2;
}
