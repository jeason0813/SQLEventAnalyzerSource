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
using Microsoft.Win32;

public partial class SettingsForm : Form
{
	public bool ItemsPerPageChanged;
	public bool RestartRequired;
	public bool ResetLayout;
	public bool EnableQuickSearchChanged;

	public SettingsForm()
	{
		InitializeComponent();
	}

	public void Initialize(DatabaseOperation databaseOperation)
	{
		InitializeDictionary();

		itemsPerPageTextBox.Text = ConfigHandler.ItemsPerPage.ToString();
		traceFileDirComboBox.Text = ConfigHandler.RecordTraceFileDir;
		SearchHistoryHandler.LoadItems(traceFileDirComboBox, "RecentListTraceFileDir_Settings");

		SetLanguageDropDown();
		SetTracingFunctionality(databaseOperation);

		keepSessionCheckBox.Checked = ConfigHandler.KeepSessionOnExit;
		enableFileNameAndTypeCheckBox.Checked = ConfigHandler.EnableFileNameAndType;
		autoPopulateFilter2CheckBox.Checked = ConfigHandler.AutoPopulateFilter2;
		enableQuickSearchCheckBox.Checked = ConfigHandler.EnableQuickSearch;
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			cancelButton.Text = Translator.GetText("cancelButton");
			okButton.Text = Translator.GetText("okButton");
			layoutGroupBox.Text = Translator.GetText("layoutGroupBox");
			itemsPerPageLabel.Text = Translator.GetText("itemsPerPageLabel");
			otherGroupBox.Text = Translator.GetText("otherGroupBox");
			languageLabel.Text = Translator.GetText("languageLabel");
			Text = Translator.GetText("Settings");
			resetLayoutButton.Text = Translator.GetText("ResetLayout");
			traceFileGroupBox.Text = Translator.GetText("traceFileGroupBox");
			traceFileInfoLinkLabel.Text = Translator.GetText("traceFileInfoLinkLabel");
			traceFileDirLabel.Text = Translator.GetText("traceFileDirLabel");
			tracingFunctionalityLabel.Text = Translator.GetText("tracingFunctionalityLabel");
			keepSessionLabel.Text = Translator.GetText("keepSessionLabel");
			enableFileNameAndTypeLabel.Text = Translator.GetText("EnableFileNameAndType");
			autoPopulateFilter2Label.Text = Translator.GetText("AutoPopulateFilter2");
			enableQuickSearchLabel.Text = Translator.GetText("EnableQuickSearch");
		}
	}

	private void SetLanguageDropDown()
	{
		foreach (string item in languageComboBox.Items)
		{
			if (item.ToLower() == ConfigHandler.Language.ToLower())
			{
				languageComboBox.SelectedItem = item;
			}
		}
	}

	private void SetTracingFunctionality(DatabaseOperation databaseOperation)
	{
		tracingFunctionalityComboBox.Items.Add("SQL Server Trace");
		tracingFunctionalityComboBox.SelectedIndex = 0;

		if (databaseOperation.GetSqlServerVersion() >= 11)
		{
			tracingFunctionalityComboBox.Items.Add("Extended Events");

			if (ConfigHandler.UseExtendedEvents == "True")
			{
				tracingFunctionalityComboBox.SelectedIndex = 1;
			}
		}
	}

	private void CancelButton_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		if (ValidateItemsPerPage() && ValidTraceFileDir())
		{
			int newItemsPerPage = Convert.ToInt32(itemsPerPageTextBox.Text);

			if (ConfigHandler.ItemsPerPage != newItemsPerPage)
			{
				ConfigHandler.ItemsPerPage = newItemsPerPage;
				ItemsPerPageChanged = true;
			}

			if (languageComboBox.SelectedItem.ToString().ToLower() != ConfigHandler.Language.ToLower())
			{
				ConfigHandler.Language = languageComboBox.SelectedItem.ToString().ToLower();
				RestartRequired = true;
			}

			ConfigHandler.RecordTraceFileDir = traceFileDirComboBox.Text.Trim();

			if (tracingFunctionalityComboBox.SelectedIndex == 0)
			{
				ConfigHandler.UseExtendedEvents = "False";
			}
			else if (tracingFunctionalityComboBox.SelectedIndex == 1)
			{
				ConfigHandler.UseExtendedEvents = "True";
			}

			ConfigHandler.KeepSessionOnExit = keepSessionCheckBox.Checked;

			if (ConfigHandler.EnableFileNameAndType != enableFileNameAndTypeCheckBox.Checked)
			{
				ConfigHandler.EnableFileNameAndType = enableFileNameAndTypeCheckBox.Checked;
				RestartRequired = true;
			}

			ConfigHandler.AutoPopulateFilter2 = autoPopulateFilter2CheckBox.Checked;

			if (ConfigHandler.EnableQuickSearch != enableQuickSearchCheckBox.Checked)
			{
				ConfigHandler.EnableQuickSearch = enableQuickSearchCheckBox.Checked;
				EnableQuickSearchChanged = true;
			}

			ConfigHandler.SaveConfig();
			SearchHistoryHandler.AddItem(traceFileDirComboBox, traceFileDirComboBox.Text, "RecentListTraceFileDir_Settings");
			Close();
		}
	}

	private bool ValidateItemsPerPage()
	{
		try
		{
			int newItemsPerPage = Convert.ToInt32(itemsPerPageTextBox.Text);

			if (newItemsPerPage <= 0)
			{
				string caption = "Settings";

				if (ConfigHandler.UseTranslation)
				{
					caption = Translator.GetText("Settings");
				}

				string text = "Items per page must be greater than 0.";

				if (ConfigHandler.UseTranslation)
				{
					text = Translator.GetText("ItemsPerPageZero");
				}

				OutputHandler.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
				itemsPerPageTextBox.Focus();
				return false;
			}

			return true;
		}
		catch
		{
			string caption = "Settings";

			if (ConfigHandler.UseTranslation)
			{
				caption = Translator.GetText("Settings");
			}

			string text = "Items per page is not a valid number.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("ItemsPerPageInvalid");
			}

			OutputHandler.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
			itemsPerPageTextBox.Focus();
			return false;
		}
	}

	private bool ValidTraceFileDir()
	{
		if (traceFileDirComboBox.Text.Trim() == "")
		{
			string text = "Trace File Directory can't be empty.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("traceDirEmpty");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			traceFileDirComboBox.Focus();
			return false;
		}

		return true;
	}

	private void ResetLayoutButton_Click(object sender, EventArgs e)
	{
		if (itemsPerPageTextBox.Text != "50")
		{
			ItemsPerPageChanged = true;
		}

		ConfigHandler.ItemsPerPage = 50;

		itemsPerPageTextBox.Text = ConfigHandler.ItemsPerPage.ToString();

		ConfigHandler.ColumnEditorSplitterDistance = "271";
		ConfigHandler.ColumnEditorWindowSize = "900; 680";
		ConfigHandler.ColumnsWindowSize = "900; 680";
		ConfigHandler.ViewStatisticsWindowSize = "900; 680";
		ConfigHandler.ViewRowWindowSize = "900; 680";
		ConfigHandler.TimelineWindowSize = "900; 680";
		ConfigHandler.SplitterDistance = "271";
		ConfigHandler.WindowSize = "900; 680";

		RegistryKey rk = Registry.LocalMachine;
		RegistryKey sk = rk.OpenSubKey(RegistryHandler.RegistryKey, true);

		if (sk != null)
		{
			string[] values = sk.GetValueNames();

			foreach (string value in values)
			{
				if (value.StartsWith("DataViewer_"))
				{
					sk.DeleteValue(value);
				}
			}
		}

		resetLayoutButton.Enabled = false;

		ResetLayout = true;
		ConfigHandler.SaveConfig();
	}

	private void ChooseDirectoryButton_Click(object sender, EventArgs e)
	{
		folderBrowserDialog1.SelectedPath = traceFileDirComboBox.Text;

		DialogResult result = folderBrowserDialog1.ShowDialog();

		if (result == DialogResult.OK)
		{
			traceFileDirComboBox.Text = folderBrowserDialog1.SelectedPath;
		}
	}

	private void KeepSessionLabel_Click(object sender, EventArgs e)
	{
		keepSessionCheckBox.Checked = !keepSessionCheckBox.Checked;
	}

	private void EnableFileNameAndTypeLabel_Click(object sender, EventArgs e)
	{
		enableFileNameAndTypeCheckBox.Checked = !enableFileNameAndTypeCheckBox.Checked;
	}

	private void AutoPopulateFilter2Label_Click(object sender, EventArgs e)
	{
		autoPopulateFilter2CheckBox.Checked = !autoPopulateFilter2CheckBox.Checked;
	}

	private void EnableQuickSearchLabel_Click(object sender, EventArgs e)
	{
		enableQuickSearchCheckBox.Checked = !enableQuickSearchCheckBox.Checked;
	}
}
