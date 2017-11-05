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
using System.Drawing;
using System.Windows.Forms;

public partial class CommandLineParametersForm : Form
{
	public CommandLineParametersForm()
	{
		InitializeComponent();
	}

	public void Initialize()
	{
		InitializeDictionary();
		MinimumSize = new Size(900, 680); // error in .NET
		infoTextBox.GotFocus += InfoTextBox_GotFocus;
		infoTextBox.Select();
	}

	public void SetCommandSyntaxOptions()
	{
		StartPosition = FormStartPosition.CenterScreen;
		ShowInTaskbar = true;

		if (ConfigHandler.UseTranslation)
		{
			closeToolStripMenuItem.Text = Translator.GetText("exitToolStripMenuItem");
		}
		else
		{
			closeToolStripMenuItem.Text = "&Exit";
		}

		CancelButton = null;
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			okButton.Text = Translator.GetText("okButton");
			fileToolStripMenuItem.Text = Translator.GetText("fileToolStripMenuItem");
			closeToolStripMenuItem.Text = Translator.GetText("closeToolStripMenuItem");
			Text = Translator.GetText("cmdTitle");
			infoTextBox.Text = Translator.GetText("unattended");
			toolsToolStripMenuItem.Text = Translator.GetText("toolsToolStripMenuItem");
			generateEncryptedPasswordToolStripMenuItem.Text = Translator.GetText("generateEncryptedPasswordToolStripMenuItem");
		}
	}

	private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void InfoTextBox_Enter(object sender, EventArgs e)
	{
		infoTextBox.SelectionStart = 0;
		infoTextBox.SelectionLength = 0;
		GenericHelper.HideCaret(infoTextBox);
	}

	private void InfoTextBox_GotFocus(object sender, EventArgs e)
	{
		infoTextBox.SelectionStart = 0;
		infoTextBox.SelectionLength = 0;
		GenericHelper.HideCaret(infoTextBox);
	}

	private void InfoTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.A)
		{
			infoTextBox.SelectAll();
		}
	}

	private void GenerateEncryptedPasswordToolStripMenuItem_Click(object sender, EventArgs e)
	{
		EncryptPasswordForm form = new EncryptPasswordForm();
		form.ShowDialog();
	}
}
