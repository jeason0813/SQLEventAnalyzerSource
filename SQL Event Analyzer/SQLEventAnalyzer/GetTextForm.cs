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

public partial class GetTextForm : Form
{
	private bool _saveChanges;
	private string _text;

	public GetTextForm(string titleText, string groupBoxText, string valueLabelText, string initialValue, int maxLength)
	{
		InitializeComponent();

		if (maxLength != -1)
		{
			textTextBox.MaxLength = maxLength;
		}

		Initialize(titleText, groupBoxText, valueLabelText, initialValue);
	}

	public bool SaveChanges()
	{
		return _saveChanges;
	}

	public string GetText()
	{
		if (_text != null)
		{
			return _text.Trim();
		}

		return null;
	}

	private void Initialize(string titleText, string groupBoxText, string valueLabelText, string initialValue)
	{
		InitializeDictionary();

		Text = titleText;
		textGroupBox.Text = groupBoxText;
		valueLabel.Text = valueLabelText;
		textTextBox.Text = initialValue;

		SetOkButton();
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			okButton.Text = Translator.GetText("okButton");
			cancelButton.Text = Translator.GetText("cancelButton");
		}
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		bool success = Save();

		if (success)
		{
			Close();
		}
	}

	private bool Save()
	{
		_saveChanges = true;
		_text = textTextBox.Text;

		return true;
	}

	private void TextTextBox_TextChanged(object sender, EventArgs e)
	{
		SetOkButton();
	}

	private void SetOkButton()
	{
		if (textTextBox.Text.Trim().Length > 0)
		{
			okButton.Enabled = true;
		}
		else
		{
			okButton.Enabled = false;
		}
	}
}
