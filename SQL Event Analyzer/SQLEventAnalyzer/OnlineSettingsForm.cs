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
using System.IO;
using System.Windows.Forms;

public partial class OnlineSettingsForm : Form
{
	public bool ChangesMade;

	private bool _initialAutomaticUpdateEnabled;
	private string _initialUpdateServer;

	public OnlineSettingsForm()
	{
		InitializeComponent();
	}

	public void Initialize()
	{
		InitializeDictionary();

		string fileName = null;

		if (ColumnHelper.ColumnCollectionFileName != null)
		{
			fileName = Path.GetFileName(ColumnHelper.ColumnCollectionFileName);
		}

		fileNameTextBox.Text = fileName;
		fileVersionTextBox.Text = ColumnHelper.GetVersion().ToString();

		SetInitialValues();
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			cancelButton.Text = Translator.GetText("cancelButton");
			okButton.Text = Translator.GetText("okButton");
			Text = Translator.GetText("OnlineSettingsTitle");
			infoLinkLabel.Text = Translator.GetText("OnlineUpdateInfo");
			enableCheckBox.Text = Translator.GetText("OnlineEnableAutoUpdate");
			updateServerLabel.Text = Translator.GetText("OnlineUpdateServerLabel");
			fileNameLabel.Text = Translator.GetText("OnlineFileNameLabel");
			fileVersionLabel.Text = Translator.GetText("OnlineFileVersionLabel");
			automaticUpdateGroupBox.Text = Translator.GetText("OnlineAutomaticUpdate");
		}
	}

	private void SetInitialValues()
	{
		enableCheckBox.Checked = ColumnHelper.GetAutomaticUpdateEnabled();
		updateServerTextBox.Text = ColumnHelper.GetUpdateServer();
		_initialAutomaticUpdateEnabled = enableCheckBox.Checked;
		_initialUpdateServer = updateServerTextBox.Text;
	}

	private void CancelButton_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		if (enableCheckBox.Checked && updateServerTextBox.Text.Trim().Length == 0)
		{
			string caption = "Online Settings";

			if (ConfigHandler.UseTranslation)
			{
				caption = Translator.GetText("OnlineSettingsTitle");
			}

			string text = "Update server is missing.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("MissingUpdateServer");
			}

			OutputHandler.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
			updateServerTextBox.Focus();
			return;
		}
		else
		{
			if (enableCheckBox.Checked != _initialAutomaticUpdateEnabled || updateServerTextBox.Text != _initialUpdateServer)
			{
				SaveOptions();
				ChangesMade = true;
			}
		}

		Close();
	}

	private void SaveOptions()
	{
		bool automaticUpdateOptionFound = false;

		foreach (Option option in ColumnHelper.ColumnCollection.Options)
		{
			if (option.Name == "AutomaticUpdateEnabled")
			{
				option.Value = enableCheckBox.Checked.ToString();
				automaticUpdateOptionFound = true;
				break;
			}
		}

		if (!automaticUpdateOptionFound)
		{
			ColumnHelper.ColumnCollection.Options.Add(new Option("AutomaticUpdateEnabled", enableCheckBox.Checked.ToString()));
		}

		bool updateServerOptionFound = false;

		foreach (Option option in ColumnHelper.ColumnCollection.Options)
		{
			if (option.Name == "UpdateServer")
			{
				option.Value = updateServerTextBox.Text;
				updateServerOptionFound = true;
				break;
			}
		}

		if (!updateServerOptionFound)
		{
			ColumnHelper.ColumnCollection.Options.Add(new Option("UpdateServer", updateServerTextBox.Text));
		}
	}

	private void EnableCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		updateServerTextBox.Enabled = enableCheckBox.Checked;
	}
}
