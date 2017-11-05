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

using System.Windows.Forms;

public partial class ComboBoxGroupStatisticsUserControl : UserControl
{
	public ComboBoxGroupStatisticsUserControl()
	{
		InitializeComponent();
	}

	public void InitializeFirst()
	{
		InitializeDictionary();

		enabledCheckBox1.Checked = true;

		if (columnComboBox1.Items.Count > 0)
		{
			columnComboBox1.SelectedIndex = 0;
		}
	}

	public void InitializeNotFirst()
	{
		InitializeDictionary();

		if (ConfigHandler.UseTranslation)
		{
			groupByLabel.Text = Translator.GetText("and");
		}
		else
		{
			groupByLabel.Text = "and";
		}
	}

	public ComboBoxCustom GetColumnComboBox()
	{
		return columnComboBox1;
	}

	public CheckBox GetEnabledCheckBox()
	{
		return enabledCheckBox1;
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			enabledCheckBox1.Text = Translator.GetText("Enabled");
			groupByLabel.Text = Translator.GetText("groupBy");
		}
	}
}
