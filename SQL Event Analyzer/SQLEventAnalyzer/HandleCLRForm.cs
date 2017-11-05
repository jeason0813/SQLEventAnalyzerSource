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

public partial class HandleCLRForm : Form
{
	private bool _enableCLR;
	private bool _enableCLRTemporary;
	private bool _allowClose;

	public HandleCLRForm()
	{
		InitializeComponent();
	}

	public void Initialize()
	{
		InitializeDictionary();

		Text = GenericHelper.ApplicationName;
	}

	public bool GetEnableCLR()
	{
		return _enableCLR;
	}

	public bool GetEnableCLRTemporary()
	{
		return _enableCLRTemporary;
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			clrLabel.Text = Translator.GetText("clrEnabled");
			enableCLRButton.Text = Translator.GetText("enableCLRButton");
			noRegExButton.Text = Translator.GetText("noRegExButton");
		}
	}

	private void ExitButton_Click(object sender, EventArgs e)
	{
		_allowClose = true;
		Hide();
		Environment.Exit(-1);
	}

	private void EnableCLRButton_Click(object sender, EventArgs e)
	{
		if (ModifierKeys == Keys.Shift)
		{
			_enableCLRTemporary = true;
		}

		_enableCLR = true;
		_allowClose = true;
		Close();
	}

	private void NoRegExButton_Click(object sender, EventArgs e)
	{
		_enableCLR = false;
		_allowClose = true;
		Close();
	}

	private void HandleCLRForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (!_allowClose)
		{
			Hide();
			Environment.Exit(-1);
		}
	}
}
