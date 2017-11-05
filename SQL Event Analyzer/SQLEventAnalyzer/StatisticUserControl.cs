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
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SQLEventAnalyzer.Properties;

public partial class StatisticUserControl : UserControl
{
	private DatabaseOperation _databaseOperation;
	private readonly List<string> _groupBys = new List<string>();
	private readonly List<ComboBoxGroupStatisticsUserControl> _comboBoxGroups = new List<ComboBoxGroupStatisticsUserControl>();
	private Filter1UserControl _filter1UserControl;
	private Filter2UserControl _filter2UserControl;
	private DateTime _minStartTime;
	private DateTime _maxStartTime;
	private int _totalRows;

	private int _selectedSavedSearchIndex;
	private string _defaultSavedSearch;
	private const string _registryName = "Statistics";

	public StatisticUserControl()
	{
		InitializeComponent();
	}

	public void Initialize(DatabaseOperation databaseOperation, Filter1UserControl filter1UserControl, Filter2UserControl filter2UserControl)
	{
		InitializeDictionary();
		_databaseOperation = databaseOperation;
		_filter1UserControl = filter1UserControl;
		_filter2UserControl = filter2UserControl;

		Reset();

		InitializeComboBoxGroups();
		AddComboBoxEventHandlers();
		InitializeColumnsComboBox();

		_defaultSavedSearch = RegistryHandler.ReadFromRegistry(string.Format("DefaultSavedSearch_{0}", _registryName));
		InitializeSavedSearch(_defaultSavedSearch);
	}

	public void SetVerboseMode()
	{
		showHiddenCheckBox.Visible = false;
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
			ResetAll();
			LoadSavedSearch(xml);

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

	public void ExportStatisticsUnattended(string fileName)
	{
		SetGroupBys();

		ViewStatisticsForm form = new ViewStatisticsForm();
		form.Initialize(_databaseOperation, _groupBys, _filter1UserControl.FilterApplied, _filter2UserControl.FilterApplied, true, _totalRows);
		form.ExportDataUnattended(fileName);

		_minStartTime = form.GetMinStartTime();
		_maxStartTime = form.GetMaxStartTime();
	}

	public DateTime GetMinStartTime()
	{
		return _minStartTime;
	}

	public DateTime GetMaxStartTime()
	{
		return _maxStartTime;
	}

	public void SetTotalRows(int totalRows)
	{
		_totalRows = totalRows;
	}

	private void InitializeColumnsComboBox()
	{
		foreach (ComboBoxGroupStatisticsUserControl comboBoxGroup in _comboBoxGroups)
		{
			comboBoxGroup.GetColumnComboBox().SelectedIndexChanged -= ColumnComboBox_SelectedIndexChanged;

			if (comboBoxGroup.GetColumnComboBox().SelectedIndex > 0)
			{
				string selectedName = comboBoxGroup.GetColumnComboBox().Text;

				InitializeColumnComboBox(comboBoxGroup.GetColumnComboBox());

				foreach (ComboBoxItem item in comboBoxGroup.GetColumnComboBox().Items)
				{
					if (item.ToString() == selectedName)
					{
						comboBoxGroup.GetColumnComboBox().SelectedItem = item;
					}
				}
			}
			else
			{
				InitializeColumnComboBox(comboBoxGroup.GetColumnComboBox());
			}

			comboBoxGroup.GetColumnComboBox().SelectedIndexChanged += ColumnComboBox_SelectedIndexChanged;
		}
	}

	private void Reset()
	{
		int totalGroups = _comboBoxGroups.Count;

		for (int i = 2; i <= totalGroups; i++)
		{
			RemoveComboBoxGroup(i);
		}

		_comboBoxGroups.Clear();
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			removeButton.Text = Translator.GetText("removeButton");
			addButton.Text = Translator.GetText("addButton");
			showHiddenCheckBox.Text = Translator.GetText("showHiddenCheckBox");
			viewButton.Text = Translator.GetText("View");
			sortAlphabeticallyCheckBox.Text = Translator.GetText("sortAlphabeticallyCheckBox");
			statisticsGroupBox.Text = Translator.GetText("statisticsTabPage");
			deleteToolStripButton.Text = Translator.GetText("deleteToolStripButton");
			saveToolStripButton.Text = Translator.GetText("saveToolStripButton");
			defaultToolStripButton.ToolTipText = Translator.GetText("defaultToolStripButton");
		}
	}

	private void InitializeComboBoxGroups()
	{
		comboBoxGroupStatisticsUserControl1.InitializeFirst();
		comboBoxGroupStatisticsUserControl1.GetEnabledCheckBox().CheckedChanged += EnabledCheckBox1_CheckedChanged;
		_comboBoxGroups.Add(comboBoxGroupStatisticsUserControl1);
	}

	private void InitializeColumnComboBox(ComboBox columnsComboBox)
	{
		columnsComboBox.Items.Clear();

		if (ConfigHandler.UseTranslation)
		{
			columnsComboBox.Items.Add(new ComboBoxItem(Translator.GetText("None")));
		}
		else
		{
			columnsComboBox.Items.Add(new ComboBoxItem("None"));
		}

		columnsComboBox.Items.Add(new ComboBoxItem("TextData"));

		columnsComboBox.SelectedIndex = 0;

		foreach (Column column in GetColumnNames())
		{
			if (showHiddenCheckBox.Checked || (!showHiddenCheckBox.Checked && !column.Hidden))
			{
				columnsComboBox.Items.Add(new ComboBoxItem(column.Name));
			}
		}
	}

	private void AddComboBoxEventHandlers()
	{
		foreach (ComboBoxGroupStatisticsUserControl comboBoxGroup in _comboBoxGroups)
		{
			comboBoxGroup.GetColumnComboBox().SelectedIndexChanged += ColumnComboBox_SelectedIndexChanged;
		}
	}

	private void ColumnComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		SetViewButton();
	}

	private void SetViewButton()
	{
		bool nameExist = false;
		bool anyEnabled = false;

		for (int i = 0; i < _comboBoxGroups.Count; i++)
		{
			string columnName = _comboBoxGroups[i].GetColumnComboBox().SelectedItem.ToString();
			bool enabled = _comboBoxGroups[i].GetEnabledCheckBox().Checked;

			if (enabled)
			{
				anyEnabled = true;

				nameExist = DoesNameExistInOtherComboBoxes(columnName, i + 1);

				if (nameExist)
				{
					break;
				}
			}
		}

		if (!nameExist && anyEnabled)
		{
			viewButton.Enabled = true;
		}
		else
		{
			viewButton.Enabled = false;
		}
	}

	private bool DoesNameExistInOtherComboBoxes(string columnName, int startIterator)
	{
		bool nameExist = false;

		for (int i = startIterator; i < _comboBoxGroups.Count; i++)
		{
			string checkColumnName = _comboBoxGroups[i].GetColumnComboBox().SelectedItem.ToString();
			bool checkEnabled = _comboBoxGroups[i].GetEnabledCheckBox().Checked;

			if (checkEnabled)
			{
				if (checkColumnName == columnName)
				{
					nameExist = true;
					break;
				}
			}
		}

		return nameExist;
	}

	private List<Column> GetColumnNames()
	{
		List<Column> returnList = new List<Column>();

		foreach (Column column in ColumnHelper.EnabledColumns)
		{
			returnList.Add(column);
		}

		if (sortAlphabeticallyCheckBox.Checked)
		{
			returnList.Sort(delegate (Column c1, Column c2)
			{
				return c1.Name.CompareTo(c2.Name);
			});
		}

		return returnList;
	}

	private class ComboBoxItem
	{
		private readonly string _text;

		public ComboBoxItem(string text)
		{
			_text = text;
		}

		public override string ToString()
		{
			return _text;
		}
	}

	private void ShowHiddenCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		InitializeColumnsComboBox();
	}

	private void AddGroup()
	{
		int numberOfGroups = _comboBoxGroups.Count;

		ComboBoxGroupStatisticsUserControl newComboBoxGroupUserControl = new ComboBoxGroupStatisticsUserControl();
		newComboBoxGroupUserControl.InitializeNotFirst();
		newComboBoxGroupUserControl.TabIndex = 3 + numberOfGroups;
		newComboBoxGroupUserControl.Name = string.Format("comboBoxGroupUserControl{0}", numberOfGroups + 1);

		flowLayoutPanel1.Controls.Add(newComboBoxGroupUserControl);

		InitializeColumnComboBox(newComboBoxGroupUserControl.GetColumnComboBox());

		newComboBoxGroupUserControl.GetColumnComboBox().SelectedIndexChanged += ColumnComboBox_SelectedIndexChanged;
		newComboBoxGroupUserControl.GetEnabledCheckBox().CheckedChanged += EnabledCheckBox1_CheckedChanged;

		_comboBoxGroups.Add(newComboBoxGroupUserControl);

		flowLayoutPanel1.ScrollControlIntoView(newComboBoxGroupUserControl);

		removeButton.Enabled = true;

		SetViewButton();
	}

	private void AddButton_Click(object sender, EventArgs e)
	{
		AddGroup();
	}

	private void RemoveButton_Click(object sender, EventArgs e)
	{
		int lastComboBoxGroupId = _comboBoxGroups.Count;
		RemoveComboBoxGroup(lastComboBoxGroupId);

		SetViewButton();
	}

	private void RemoveComboBoxGroup(int comboBoxGroupId)
	{
		ComboBoxGroupStatisticsUserControl comboBoxGroupUserControlToRemove = new ComboBoxGroupStatisticsUserControl();

		foreach (Control control in flowLayoutPanel1.Controls)
		{
			if (control is ComboBoxGroupStatisticsUserControl)
			{
				ComboBoxGroupStatisticsUserControl comboBoxGroupUserControl = (ComboBoxGroupStatisticsUserControl)control;

				if (comboBoxGroupUserControl.Name == string.Format("comboBoxGroupUserControl{0}", comboBoxGroupId))
				{
					comboBoxGroupUserControlToRemove = comboBoxGroupUserControl;
					_comboBoxGroups.Remove(comboBoxGroupUserControl);
					break;
				}
			}
		}

		flowLayoutPanel1.Controls.Remove(comboBoxGroupUserControlToRemove);
		comboBoxGroupUserControlToRemove.Dispose();

		if (comboBoxGroupId == 2)
		{
			removeButton.Enabled = false;
		}
	}

	private void SortAlphabeticallyCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		InitializeColumnsComboBox();
	}

	private void ViewButton_Click(object sender, EventArgs e)
	{
		SetGroupBys();

		ViewStatisticsForm form = new ViewStatisticsForm();
		form.Initialize(_databaseOperation, _groupBys, _filter1UserControl.FilterApplied, _filter2UserControl.FilterApplied, false, _totalRows);
		form.ShowDialog();
	}

	private void SetGroupBys()
	{
		_groupBys.Clear();

		foreach (ComboBoxGroupStatisticsUserControl comboBoxGroup in _comboBoxGroups)
		{
			bool enabled = comboBoxGroup.GetEnabledCheckBox().Checked;

			if (enabled)
			{
				SetGroupBy(comboBoxGroup.GetColumnComboBox());
			}
		}
	}

	private void SetGroupBy(ComboBox columnComboBox)
	{
		if (columnComboBox.SelectedIndex > 0)
		{
			string column = columnComboBox.SelectedItem.ToString();
			_groupBys.Add(column);
		}
	}

	private void EnabledCheckBoxChanged(CheckBox checkBox)
	{
		ComboBoxGroupStatisticsUserControl comboBoxGroupUserControl = (ComboBoxGroupStatisticsUserControl)checkBox.Parent;
		comboBoxGroupUserControl.GetColumnComboBox().Enabled = checkBox.Checked;

		SetViewButton();
	}

	private void EnabledCheckBox1_CheckedChanged(object sender, EventArgs e)
	{
		EnabledCheckBoxChanged((CheckBox)sender);
	}

	private static string GetSavedSearchesXml(List<ComboBoxGroupStatisticsUserControl> comboBoxGroups, bool showHidden, bool sortAlphabetically)
	{
		StringBuilder sb = new StringBuilder();

		sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
		sb.Append(string.Format("<groups showHidden=\"{0}\" sortAlphabetically=\"{1}\">", showHidden, sortAlphabetically));

		foreach (ComboBoxGroupStatisticsUserControl comboBoxGroup in comboBoxGroups)
		{
			ComboBox columnComboBox = comboBoxGroup.GetColumnComboBox();
			CheckBox enabledCheckBox = comboBoxGroup.GetEnabledCheckBox();

			sb.Append(string.Format("<group column=\"{0}\" enabled=\"{1}\" />", System.Security.SecurityElement.Escape(columnComboBox.Text), enabledCheckBox.Checked));
		}

		sb.Append("</groups>");

		return sb.ToString();
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

		string savedSearchName = toolStripDropDownButton1.Text;

		if (_selectedSavedSearchIndex == 0)
		{
			savedSearchName = "";
		}

		string savedSearchesXml = GetSavedSearchesXml(_comboBoxGroups, showHiddenCheckBox.Checked, sortAlphabeticallyCheckBox.Checked);
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

		ResetAll();

		if (toolStripItem.Index == 0)
		{
			SetLastGroupValues("", true);
			deleteToolStripButton.Enabled = false;
		}
		else
		{
			string xml = SavedSearchesHandler.Load(toolStripItem.Text, _registryName);
			LoadSavedSearch(xml);
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

	private void LoadSavedSearch(string xml)
	{
		if (xml == "")
		{
			return;
		}

		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(xml);

		bool showHiddenValue = Convert.ToBoolean(xmlDocument.SelectSingleNode("groups/@showHidden").Value);
		SetShowHidden(showHiddenValue);

		bool sortAlphabeticallyValue = Convert.ToBoolean(xmlDocument.SelectSingleNode("groups/@sortAlphabetically").Value);
		SetSortAlphabetically(sortAlphabeticallyValue);

		XmlNodeList xmlGroupList = xmlDocument.SelectNodes("groups/group");

		if (xmlGroupList != null)
		{
			int i = 0;

			foreach (XmlElement xmlElement in xmlGroupList)
			{
				string columnValue = xmlElement.GetAttribute("column");
				bool enabledValue = Convert.ToBoolean(xmlElement.GetAttribute("enabled"));

				SetShowHidden(showHiddenValue);
				SetSortAlphabetically(sortAlphabeticallyValue);

				if (i > 0)
				{
					AddGroup();
				}

				SetLastGroupValues(columnValue, enabledValue);

				if (i == 0)
				{
					InitializeFirstGroup();
				}

				i++;
			}
		}

		deleteToolStripButton.Enabled = true;
		ScrollFirstGroupIntoView();
	}

	private void SetShowHidden(bool value)
	{
		showHiddenCheckBox.Checked = value;
	}

	private void SetSortAlphabetically(bool value)
	{
		sortAlphabeticallyCheckBox.Checked = value;
	}

	private void ResetAll()
	{
		int lastComboBoxGroupId = _comboBoxGroups.Count;

		while (lastComboBoxGroupId != 1)
		{
			RemoveComboBoxGroup(lastComboBoxGroupId);
			lastComboBoxGroupId = _comboBoxGroups.Count;
		}

		ComboBox columnComboBox = _comboBoxGroups[0].GetColumnComboBox();
		columnComboBox.SelectedIndex = 0;
	}

	private void ScrollFirstGroupIntoView()
	{
		flowLayoutPanel1.PerformLayout();

		ComboBoxGroupStatisticsUserControl firstComboBoxGroupUserControl = _comboBoxGroups[0];
		flowLayoutPanel1.ScrollControlIntoView(firstComboBoxGroupUserControl);
	}

	private void InitializeFirstGroup()
	{
		EnabledCheckBoxChanged(_comboBoxGroups[0].GetEnabledCheckBox());
	}

	private void SetLastGroupValues(string columnValue, bool enabledValue)
	{
		int lastComboBoxGroupId = _comboBoxGroups.Count - 1;

		_comboBoxGroups[lastComboBoxGroupId].GetColumnComboBox().Text = columnValue;
		_comboBoxGroups[lastComboBoxGroupId].GetEnabledCheckBox().Checked = enabledValue;
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

	private class CustomToolStripMenuItem : ToolStripMenuItem
	{
		public int Index;
	}
}
