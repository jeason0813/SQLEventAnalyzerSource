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

public partial class EncryptPasswordForm : Form
{
	public EncryptPasswordForm()
	{
		InitializeComponent();
		Initialize();
	}

	public void SetNameValue(string name)
	{
		decryptedTextBox.Text = name;
	}

	protected override void OnLoad(EventArgs args)
	{
		if (Site == null || (Site != null && !Site.DesignMode))
		{
			base.OnLoad(args);
			Application.Idle += OnLoaded;
		}
	}

	private void OnLoaded(object sender, EventArgs args)
	{
		Application.Idle -= OnLoaded;

		decryptedTextBox.Focus();
	}

	private void Initialize()
	{
		InitializeDictionary();
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			Text = Translator.GetText("encryptPassword");
			okButton.Text = Translator.GetText("okButton");
			passwordGroupBox.Text = Translator.GetText("Parameter");
			decryptedLabel.Text = Translator.GetText("decryptedLabel");
			encryptedLabel.Text = Translator.GetText("encryptedLabel");
			infoLabel.Text = Translator.GetText("passwordInfo");
		}
	}

	private void DecryptedTextBox_KeyUp(object sender, KeyEventArgs e)
	{
		string encryptedPassword = ConnectionStringSecurity.Encode(decryptedTextBox.Text);
		encryptedTextBox.Text = encryptedPassword;

		if (encryptedTextBox.Text != "")
		{
			copyLinkLabel.Enabled = true;
		}
		else
		{
			copyLinkLabel.Enabled = false;
		}
	}

	private void CopyLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		if (encryptedTextBox.Text != "")
		{
			Clipboard.SetText(encryptedTextBox.Text);
		}
	}
}
