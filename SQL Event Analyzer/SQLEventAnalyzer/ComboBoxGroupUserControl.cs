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

using System;
using System.Windows.Forms;

public partial class ComboBoxGroupUserControl : UserControl
{
	public delegate void ReloadValuesCheckBoxChangedEventHandler(object sender);
	public event ReloadValuesCheckBoxChangedEventHandler ReloadValuesCheckBoxChangedEvent;

	private readonly ToolTip _toolTip = new ToolTip();

	public ComboBoxGroupUserControl()
	{
		InitializeComponent();
	}

	public void InitializeFirst()
	{
		InitializeDictionary();
		InitializeReloadValuesCheckBox();

		valueComboBox1.Text = "";
		andOrComboBox1.Visible = false;
		whereColumnLabel.Visible = true;
		enabledCheckBox1.Checked = true;

		if (columnComboBox1.Items.Count > 0)
		{
			columnComboBox1.SelectedIndex = 0;
		}

		operatorComboBox1.Enabled = false;
		valueComboBox1.Enabled = false;
	}

	public void InitializeNotFirst()
	{
		InitializeDictionary();
		InitializeReloadValuesCheckBox();

		valueComboBox1.Text = "";
	}

	public ComboBoxCustom GetColumnComboBox()
	{
		return columnComboBox1;
	}

	public ComboBox GetOperatorComboBox()
	{
		return operatorComboBox1;
	}

	public ComboBox GetValueComboBox()
	{
		return valueComboBox1;
	}

	public CheckBox GetEnabledCheckBox()
	{
		return enabledCheckBox1;
	}

	public ComboBox GetAndOrComboBox()
	{
		return andOrComboBox1;
	}

	public ComboBox GetParanthesBeginComboBox()
	{
		return paranthesBeginComboBox;
	}

	public ComboBox GetParanthesEndComboBox()
	{
		return paranthesEndComboBox;
	}

	public bool GetReloadValuesChecked()
	{
		return reloadValuesCheckBox.Checked;
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			whereColumnLabel.Text = Translator.GetText("whereColumnLabel");
			enabledCheckBox1.Text = Translator.GetText("Enabled");
		}
	}

	private void ReloadValuesCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		if (reloadValuesCheckBox.Checked)
		{
			reloadValuesCheckBox.Image = SQLEventAnalyzer.Properties.Resources.arrow_rotate_clockwise_small;
		}
		else
		{
			reloadValuesCheckBox.Image = SQLEventAnalyzer.Properties.Resources.arrow_rotate_clockwise_small1;
		}

		FireReloadValuesCheckBoxChangedEvent(this);
	}

	private void FireReloadValuesCheckBoxChangedEvent(object sender)
	{
		if (ReloadValuesCheckBoxChangedEvent != null)
		{
			ReloadValuesCheckBoxChangedEvent(sender);
		}
	}

	private void InitializeReloadValuesCheckBox()
	{
		reloadValuesCheckBox.CheckedChanged -= ReloadValuesCheckBox_CheckedChanged;

		if (ConfigHandler.AutoPopulateFilter2)
		{
			reloadValuesCheckBox.Image = SQLEventAnalyzer.Properties.Resources.arrow_rotate_clockwise_small;
			reloadValuesCheckBox.Checked = true;
		}

		reloadValuesCheckBox.CheckedChanged += ReloadValuesCheckBox_CheckedChanged;

		string toolTipText = "Automatically populate drop down values";

		if (ConfigHandler.UseTranslation)
		{
			toolTipText = Translator.GetText("ReloadValuesCheckBoxToolTip");
		}

		_toolTip.SetToolTip(reloadValuesCheckBox, toolTipText);
		_toolTip.AutomaticDelay = 500;
		reloadValuesCheckBox.MouseEnter += ToolTipReset;
	}

	private void ToolTipReset(object sender, EventArgs e)
	{
		_toolTip.Active = false;
		_toolTip.Active = true;
	}
}
