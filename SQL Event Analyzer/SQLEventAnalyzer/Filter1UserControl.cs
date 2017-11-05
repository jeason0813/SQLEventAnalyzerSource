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
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using SQLEventAnalyzer.Properties;

public partial class Filter1UserControl : UserControl
{
	public delegate void ResetEventHandler();
	public event ResetEventHandler ResetEvent;

	public delegate void ApplyEventHandler(List<Filter> filters);
	public event ApplyEventHandler ApplyEvent;

	public bool FilterApplied;

	private int _selectedSavedSearchIndex;
	private string _defaultSavedSearch;
	private const string _registryName = "Filter1";

	public Filter1UserControl()
	{
		InitializeComponent();
	}

	public void Initialize(DataSet traceDataInfo, DateTime lastEventStartTime)
	{
		InitializeDictionary();

		filter1ContentUserControl1.Initialize(traceDataInfo, lastEventStartTime);
		filter1ContentUserControl1.EnterKeyEvent += Filter1ContentUserControl1_EnterKeyEvent;

		_defaultSavedSearch = RegistryHandler.ReadFromRegistry(string.Format("DefaultSavedSearch_{0}", _registryName));
		InitializeSavedSearch(_defaultSavedSearch);
	}

	public void ApplyFilter()
	{
		if (filter1ContentUserControl1.IsDataValid())
		{
			FilterApplied = true;
			deactivateButton.Enabled = true;
			FireApplyEvent(filter1ContentUserControl1.GetFilters());
		}
	}

	public bool InitializeSavedSearch(string name)
	{
		PopulateSavedSearchesToolStrip();

		bool nameExist = false;

		for (int i = 2; i < toolStripDropDownButton1.DropDown.Items.Count; i++)
		{
			if (toolStripDropDownButton1.DropDown.Items[i].Text == name)
			{
				nameExist = true;
				break;
			}
		}

		if (!nameExist)
		{
			return false;
		}

		if (name != "")
		{
			toolStripDropDownButton1.Text = name;

			for (int i = 2; i < toolStripDropDownButton1.DropDown.Items.Count; i++)
			{
				if (toolStripDropDownButton1.DropDown.Items[i].Text == toolStripDropDownButton1.Text)
				{
					_selectedSavedSearchIndex = i;
					break;
				}
			}

			string xml = SavedSearchesHandler.Load(name, _registryName);
			filter1ContentUserControl1.LoadSavedSearch(xml);

			if (xml != "")
			{
				deleteToolStripButton.Enabled = true;
			}
		}

		if (toolStripDropDownButton1.Text == _defaultSavedSearch)
		{
			defaultToolStripButton.Image = Resources.star;
		}
		else
		{
			defaultToolStripButton.Image = Resources.star1;
		}

		return true;
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			deactivateButton.Text = Translator.GetText("deactivateButton");
			activateButton.Text = Translator.GetText("activateButton");
			resetButton.Text = Translator.GetText("resetButton");
			filterGroupBox.Text = Translator.GetText("filter1GroupBox");
			deleteToolStripButton.Text = Translator.GetText("deleteToolStripButton");
			saveToolStripButton.Text = Translator.GetText("saveToolStripButton");
			defaultToolStripButton.ToolTipText = Translator.GetText("defaultToolStripButton");
		}
	}

	private void FireResetEvent()
	{
		if (ResetEvent != null)
		{
			ResetEvent();
		}
	}

	private void FireApplyEvent(List<Filter> filters)
	{
		if (ApplyEvent != null)
		{
			ApplyEvent(filters);
		}
	}

	private void ResetButton_Click(object sender, EventArgs e)
	{
		ResetFilter();
	}

	private void ResetFilter()
	{
		FilterApplied = false;
		deactivateButton.Enabled = false;
		FireResetEvent();
	}

	private void ApplyButton_Click(object sender, EventArgs e)
	{
		ApplyFilter();
	}

	private void ResetButton_Click_1(object sender, EventArgs e)
	{
		filter1ContentUserControl1.FillDefaultTraceDataValues();
		ResetFilter();
	}

	private void SaveToolStripButton_Click(object sender, EventArgs e)
	{
		if (!ConfigHandler.RegistryModifyAccess)
		{
			if (ConfigHandler.UseTranslation)
			{
				MessageBox.Show(Translator.GetText("SaveSearchNoAccess"));
			}
			else
			{
				MessageBox.Show("Logged in user does not have necessary rights to save the search.\r\n\r\nUser needs modify access to HKLM in RegEdit.", GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			return;
		}

		if (!filter1ContentUserControl1.IsDataValid())
		{
			return;
		}

		string savedSearchName = toolStripDropDownButton1.Text;

		if (_selectedSavedSearchIndex == 0)
		{
			savedSearchName = "";
		}

		string savedSearchesXml = filter1ContentUserControl1.GetSavedSearchesXml();
		string returnedSavedSearchName = SavedSearchesHandler.SaveAs(savedSearchesXml, _registryName, savedSearchName);

		if (returnedSavedSearchName != null && returnedSavedSearchName != toolStripDropDownButton1.Text)
		{
			PopulateSavedSearchesToolStrip();
		}

		if (returnedSavedSearchName != null)
		{
			deleteToolStripButton.Enabled = true;
			toolStripDropDownButton1.Text = returnedSavedSearchName;

			if (toolStripDropDownButton1.Text == _defaultSavedSearch)
			{
				defaultToolStripButton.Image = Resources.star;
			}
			else
			{
				defaultToolStripButton.Image = Resources.star1;
			}

			for (int i = 2; i < toolStripDropDownButton1.DropDown.Items.Count; i++)
			{
				if (toolStripDropDownButton1.DropDown.Items[i].Text == toolStripDropDownButton1.Text)
				{
					_selectedSavedSearchIndex = i;
					break;
				}
			}
		}
	}

	private void DeleteToolStripButton_Click(object sender, EventArgs e)
	{
		if (!ConfigHandler.RegistryModifyAccess)
		{
			if (ConfigHandler.UseTranslation)
			{
				MessageBox.Show(Translator.GetText("DeleteSearchNoAccess"));
			}
			else
			{
				MessageBox.Show("Logged in user does not have necessary rights to delete the saved search.\r\n\r\nUser needs modify access to HKLM in RegEdit.", GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			return;
		}

		string savedSearchName = toolStripDropDownButton1.Text;

		if (SavedSearchesHandler.IsSystemObject(savedSearchName, _registryName))
		{
			string text1 = "The selected search is a system object and can not be deleted.";

			if (ConfigHandler.UseTranslation)
			{
				text1 = Translator.GetText("CanNotDeleteSystemObject");
			}

			OutputHandler.Show(text1, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);

			return;
		}

		string text = "Delete search \"{0}\"?";

		if (ConfigHandler.UseTranslation)
		{
			text = Translator.GetText("DeleteSearch");
		}

		DialogResult result = OutputHandler.Show(string.Format(text, savedSearchName), GenericHelper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

		if (result == DialogResult.Yes)
		{
			SavedSearchesHandler.Delete(savedSearchName, _registryName);
			deleteToolStripButton.Enabled = false;
			PopulateSavedSearchesToolStrip();

			if (savedSearchName == _defaultSavedSearch)
			{
				RegistryHandler.Delete(string.Format("DefaultSavedSearch_{0}", _registryName));
				defaultToolStripButton.Image = Resources.star1;
				_defaultSavedSearch = "";
			}
		}
	}

	private void ToolStripDropDownButton1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
	{
		if (e.ClickedItem is ToolStripSeparator)
		{
			return;
		}

		CustomToolStripMenuItem toolStripItem = ((CustomToolStripMenuItem)e.ClickedItem);
		toolStripDropDownButton1.Text = toolStripItem.Text;

		if (toolStripItem.Index == 0)
		{
			filter1ContentUserControl1.FillDefaultTraceDataValues();
			filter1ContentUserControl1.DisableAllCheckBoxes();
			deleteToolStripButton.Enabled = false;
		}
		else
		{
			string xml = SavedSearchesHandler.Load(toolStripItem.Text, _registryName);
			filter1ContentUserControl1.LoadSavedSearch(xml);

			if (xml != "")
			{
				deleteToolStripButton.Enabled = true;
			}
		}

		_selectedSavedSearchIndex = toolStripItem.Index;

		if (toolStripItem.Text == _defaultSavedSearch)
		{
			defaultToolStripButton.Image = Resources.star;
		}
		else
		{
			defaultToolStripButton.Image = Resources.star1;
		}
	}

	private void DefaultToolStripButton_Click(object sender, EventArgs e)
	{
		if (!ConfigHandler.RegistryModifyAccess)
		{
			if (ConfigHandler.UseTranslation)
			{
				MessageBox.Show(Translator.GetText("ModifyDefaultSearchNoAccess"));
			}
			else
			{
				MessageBox.Show("Logged in user does not have necessary rights to modify the default search.\r\n\r\nUser needs modify access to HKLM in RegEdit.", GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			return;
		}

		if (toolStripDropDownButton1.Text == _defaultSavedSearch)
		{
			RegistryHandler.Delete(string.Format("DefaultSavedSearch_{0}", _registryName));
			defaultToolStripButton.Image = Resources.star1;
			_defaultSavedSearch = "";
		}
		else
		{
			RegistryHandler.SaveToRegistry(string.Format("DefaultSavedSearch_{0}", _registryName), toolStripDropDownButton1.Text);
			defaultToolStripButton.Image = Resources.star;
			_defaultSavedSearch = toolStripDropDownButton1.Text;
		}

		string selectedSavedSearch = toolStripDropDownButton1.Text;
		PopulateSavedSearchesToolStrip();
		toolStripDropDownButton1.Text = selectedSavedSearch;
	}

	private void PopulateSavedSearchesToolStrip()
	{
		toolStripDropDownButton1.DropDown.Items.Clear();

		string emptyText = "Empty";

		if (ConfigHandler.UseTranslation)
		{
			emptyText = Translator.GetText("Empty");
		}

		toolStripDropDownButton1.Text = emptyText;

		CustomToolStripMenuItem toolStripItem = new CustomToolStripMenuItem();
		toolStripItem.Text = emptyText;
		toolStripItem.Index = 0;

		if (toolStripItem.Text == _defaultSavedSearch)
		{
			toolStripItem.Image = Resources.star;
		}

		toolStripDropDownButton1.DropDownItems.Add(toolStripItem);

		List<string> savedSearchesNames = SavedSearchesHandler.GetNames(_registryName);

		if (savedSearchesNames.Count > 0)
		{
			toolStripDropDownButton1.DropDown.Items.Add(new ToolStripSeparator());
		}

		for (int i = 0; i < savedSearchesNames.Count; i++)
		{
			toolStripItem = new CustomToolStripMenuItem();
			toolStripItem.Text = savedSearchesNames[i];
			toolStripItem.Index = i + 1;

			if (toolStripItem.Text == _defaultSavedSearch)
			{
				toolStripItem.Image = Resources.star;
			}

			toolStripDropDownButton1.DropDownItems.Add(toolStripItem);
		}
	}

	private void Filter1ContentUserControl1_EnterKeyEvent()
	{
		ApplyFilter();
	}

	private class CustomToolStripMenuItem : ToolStripMenuItem
	{
		public int Index;
	}
}
