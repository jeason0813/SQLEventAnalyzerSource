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
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SQLEventAnalyzer.Properties;

public partial class Filter2UserControl : UserControl
{
	public delegate void ResetEventHandler();
	public event ResetEventHandler ResetEvent;

	public delegate void ApplyEventHandler(List<Filter> filters);
	public event ApplyEventHandler ApplyEvent;

	public bool FilterApplied;

	private DatabaseOperation _databaseOperation;
	private readonly List<Filter> _dataViewFilter = new List<Filter>();
	private readonly List<ComboBoxGroupUserControl> _comboBoxGroups = new List<ComboBoxGroupUserControl>();

	private readonly ToolTip _toolTip = new ToolTip();
	private string _searchBoxToolTipText = "Use * as wildcard";
	private string _searchBoxListToolTipText = "Use comma to separate items, e.g. 123,456,789";

	private int _selectedSavedSearchIndex;
	private string _defaultSavedSearch;
	private const string _registryName = "Filter2";

	public Filter2UserControl()
	{
		InitializeComponent();
	}

	public void Initialize(DatabaseOperation databaseOperation)
	{
		InitializeDictionary();
		_databaseOperation = databaseOperation;

		Reset();

		InitializeComboBoxGroups();
		AddComboBoxEventHandlers();
		InitializeColumnsComboBox();
		InitializeOperatorsComboBox();

		_defaultSavedSearch = RegistryHandler.ReadFromRegistry(string.Format("DefaultSavedSearch_{0}", _registryName));
		InitializeSavedSearch(_defaultSavedSearch);
	}

	public void SetVerboseMode()
	{
		showHiddenCheckBox.Visible = false;
	}

	public void ApplyFilter()
	{
		if (!CheckValidParantheses())
		{
			return;
		}

		FilterApplied = true;
		deactivateButton.Enabled = true;
		SetFilters();
		FireApplyEvent(_dataViewFilter);
	}

	public void ReloadFilterValues()
	{
		foreach (ComboBoxGroupUserControl comboBoxGroup in _comboBoxGroups)
		{
			ComboBox columnComboBox = comboBoxGroup.GetColumnComboBox();
			InitializeValueComboBox(columnComboBox);
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

	private void InitializeColumnsComboBox()
	{
		foreach (ComboBoxGroupUserControl comboBoxGroup in _comboBoxGroups)
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
			comboBoxGroup.ReloadValuesCheckBoxChangedEvent += ComboBoxGroup_ReloadValuesCheckBoxChangedEvent;
		}
	}

	private void Reset()
	{
		int totalGroups = _comboBoxGroups.Count;

		for (int i = 2; i <= totalGroups; i++)
		{
			RemoveComboBoxGroup(i);
		}

		try
		{
			_comboBoxGroups[0].GetValueComboBox().KeyDown -= ValueComboBox_KeyDown;
		}
		catch
		{
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
			deactivateButton.Text = Translator.GetText("deactivateButton");
			activateButton.Text = Translator.GetText("activateButton");
			sortAlphabeticallyCheckBox.Text = Translator.GetText("sortAlphabeticallyCheckBox");
			filterGroupBox.Text = Translator.GetText("filter2GroupBox");
			_searchBoxToolTipText = Translator.GetText("SearchBoxToolTipText");
			_searchBoxListToolTipText = Translator.GetText("SearchBoxListToolTipText");
			deleteToolStripButton.Text = Translator.GetText("deleteToolStripButton");
			saveToolStripButton.Text = Translator.GetText("saveToolStripButton");
			defaultToolStripButton.ToolTipText = Translator.GetText("defaultToolStripButton");
		}
	}

	private void InitializeToolTip(Control control, string toolTipText)
	{
		_toolTip.SetToolTip(control, toolTipText);
		_toolTip.AutomaticDelay = 500;

		control.MouseEnter += ToolTipReset;
	}

	private void UpdateToolTipText(Control control, string toolTipText)
	{
		_toolTip.SetToolTip(control, toolTipText);
	}

	private void ToolTipReset(object sender, EventArgs e)
	{
		_toolTip.Active = false;
		_toolTip.Active = true;
	}

	private void InitializeComboBoxGroups()
	{
		comboBoxGroupUserControl1.InitializeFirst();
		InitializeParanthesBeginComboBoxe(comboBoxGroupUserControl1.GetParanthesBeginComboBox());
		InitializeParanthesEndComboBoxe(comboBoxGroupUserControl1.GetParanthesEndComboBox());
		InitializeAndOrComboBox(comboBoxGroupUserControl1.GetAndOrComboBox());
		InitializeToolTip(comboBoxGroupUserControl1.GetValueComboBox(), _searchBoxToolTipText);
		comboBoxGroupUserControl1.GetEnabledCheckBox().CheckedChanged += EnabledCheckBox1_CheckedChanged;
		_comboBoxGroups.Add(comboBoxGroupUserControl1);
	}

	private void InitializeColumnComboBox(ComboBox columnsComboBox)
	{
		columnsComboBox.Items.Clear();

		if (ConfigHandler.UseTranslation)
		{
			columnsComboBox.Items.Add(new ComboBoxItem(Translator.GetText("All")));
		}
		else
		{
			columnsComboBox.Items.Add(new ComboBoxItem("Please choose..."));
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

	private void InitializeValueComboBox(ComboBox columnComboBox)
	{
		ComboBox valueComboBox = new ComboBox();
		bool reloadValuesCheckBoxChecked = false;

		for (int i = 0; i < _comboBoxGroups.Count; i++)
		{
			if (_comboBoxGroups[i].GetColumnComboBox() == columnComboBox)
			{
				valueComboBox = _comboBoxGroups[i].GetValueComboBox();
				reloadValuesCheckBoxChecked = _comboBoxGroups[i].GetReloadValuesChecked();
				break;
			}
		}

		valueComboBox.Items.Clear();

		if (reloadValuesCheckBoxChecked)
		{
			string columnName = columnComboBox.SelectedItem.ToString();

			if (columnComboBox.SelectedIndex > 1)
			{
				DataTable dt = GetCustomColumnInfo(columnName);

				foreach (DataRow dr in dt.Rows)
				{
					valueComboBox.Items.Add(new ComboBoxItem(dr[columnName].ToString()));
				}
			}
		}
	}

	private DataTable GetCustomColumnInfo(string columnName)
	{
		GetDataForm form = new GetDataForm();
		string sql = string.Format(Resources.GetCustomColumnsInfo, ConfigHandler.DatabaseName, columnName, GenericHelper.TempTableName);
		form.Initialize(_databaseOperation, sql);

		if (GenericHelper.IsUserInteractive())
		{
			form.ShowDialog();
		}

		return form.GetDataSet().Tables[0];
	}

	private void InitializeOperatorsComboBox()
	{
		foreach (ComboBoxGroupUserControl comboBoxGroup in _comboBoxGroups)
		{
			InitializeOperatorComboBox(comboBoxGroup.GetOperatorComboBox());
		}
	}

	private void InitializeOperatorComboBox(ComboBox operatorComboBox)
	{
		operatorComboBox.SelectedIndexChanged -= OperatorComboBox_SelectedIndexChanged;

		operatorComboBox.Items.Clear();

		if (ConfigHandler.UseTranslation)
		{
			operatorComboBox.Items.Add(new ComboBoxItem(Translator.GetText("equal")));
			operatorComboBox.Items.Add(new ComboBoxItem(Translator.GetText("notEqual")));
			operatorComboBox.Items.Add(new ComboBoxItem(Translator.GetText("in")));
			operatorComboBox.Items.Add(new ComboBoxItem(Translator.GetText("notin")));
			operatorComboBox.Items.Add(new ComboBoxItem(Translator.GetText("greaterThan")));
			operatorComboBox.Items.Add(new ComboBoxItem(Translator.GetText("lessThan")));
		}
		else
		{
			operatorComboBox.Items.Add(new ComboBoxItem("Equal"));
			operatorComboBox.Items.Add(new ComboBoxItem("Not equal"));
			operatorComboBox.Items.Add(new ComboBoxItem("In"));
			operatorComboBox.Items.Add(new ComboBoxItem("Not in"));
			operatorComboBox.Items.Add(new ComboBoxItem("Greater than"));
			operatorComboBox.Items.Add(new ComboBoxItem("Less than"));
		}

		operatorComboBox.SelectedIndex = 0;

		operatorComboBox.SelectedIndexChanged += OperatorComboBox_SelectedIndexChanged;
	}

	private static void InitializeParanthesBeginComboBoxe(ComboBox paranthesBeginComboBox)
	{
		paranthesBeginComboBox.Items.Clear();

		paranthesBeginComboBox.Items.Add(new ComboBoxItem(""));
		paranthesBeginComboBox.Items.Add(new ComboBoxItem("("));
		paranthesBeginComboBox.Items.Add(new ComboBoxItem("(("));
		paranthesBeginComboBox.Items.Add(new ComboBoxItem("((("));
		paranthesBeginComboBox.Items.Add(new ComboBoxItem("(((("));
		paranthesBeginComboBox.Items.Add(new ComboBoxItem("((((("));
		paranthesBeginComboBox.Items.Add(new ComboBoxItem("(((((("));
		paranthesBeginComboBox.Items.Add(new ComboBoxItem("((((((("));

		paranthesBeginComboBox.SelectedIndex = 0;
	}

	private static void InitializeParanthesEndComboBoxe(ComboBox paranthesEndComboBox)
	{
		paranthesEndComboBox.Items.Clear();

		paranthesEndComboBox.Items.Add(new ComboBoxItem(""));
		paranthesEndComboBox.Items.Add(new ComboBoxItem(")"));
		paranthesEndComboBox.Items.Add(new ComboBoxItem("))"));
		paranthesEndComboBox.Items.Add(new ComboBoxItem(")))"));
		paranthesEndComboBox.Items.Add(new ComboBoxItem("))))"));
		paranthesEndComboBox.Items.Add(new ComboBoxItem(")))))"));
		paranthesEndComboBox.Items.Add(new ComboBoxItem("))))))"));
		paranthesEndComboBox.Items.Add(new ComboBoxItem(")))))))"));

		paranthesEndComboBox.SelectedIndex = 0;
	}

	private static void InitializeAndOrComboBox(ComboBox andOrComboBox)
	{
		andOrComboBox.Items.Clear();

		if (ConfigHandler.UseTranslation)
		{
			andOrComboBox.Items.Add(new ComboBoxItem(Translator.GetText("and")));
		}
		else
		{
			andOrComboBox.Items.Add(new ComboBoxItem("and"));
		}

		if (ConfigHandler.UseTranslation)
		{
			andOrComboBox.Items.Add(new ComboBoxItem(Translator.GetText("or")));
		}
		else
		{
			andOrComboBox.Items.Add(new ComboBoxItem("or"));
		}

		andOrComboBox.SelectedIndex = 0;
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
		FilterApplied = false;
		deactivateButton.Enabled = false;
		FireResetEvent();
	}

	private void ApplyButton_Click(object sender, EventArgs e)
	{
		ApplyFilter();
	}

	private void SetFilters()
	{
		_dataViewFilter.Clear();

		foreach (ComboBoxGroupUserControl comboBoxGroup in _comboBoxGroups)
		{
			if (comboBoxGroup.GetEnabledCheckBox().Checked)
			{
				SetFilter(comboBoxGroup.GetColumnComboBox(), comboBoxGroup.GetOperatorComboBox(), comboBoxGroup.GetValueComboBox(), comboBoxGroup.GetAndOrComboBox(), comboBoxGroup.GetParanthesBeginComboBox(), comboBoxGroup.GetParanthesEndComboBox());
			}
		}
	}

	private void SetFilter(ComboBox columnComboBox, ComboBox operatorComboBox, ComboBox valueComboBox, ComboBox andOrComboBox, ComboBox paranthesBeginComboBox, ComboBox paranthesEndComboBox)
	{
		if (columnComboBox.SelectedIndex > 0)
		{
			Filter filter = new Filter();
			filter.Column = columnComboBox.SelectedItem.ToString();

			filter.Value = valueComboBox.Text;

			if (operatorComboBox.SelectedIndex == 0)
			{
				filter.Operator = "=";
			}
			else if (operatorComboBox.SelectedIndex == 1)
			{
				filter.Operator = "!=";
			}
			else if (operatorComboBox.SelectedIndex == 2)
			{
				if (valueComboBox.Text.Contains(","))
				{
					filter.Operator = "in";
				}
				else
				{
					filter.Operator = "=";
				}
			}
			else if (operatorComboBox.SelectedIndex == 3)
			{
				if (valueComboBox.Text.Contains(","))
				{
					filter.Operator = "not in";
				}
				else
				{
					filter.Operator = "!=";
				}
			}
			else if (operatorComboBox.SelectedIndex == 4)
			{
				filter.Operator = ">";
			}
			else if (operatorComboBox.SelectedIndex == 5)
			{
				filter.Operator = "<";
			}

			if (andOrComboBox.SelectedIndex == 0)
			{
				filter.AndOr = "and";
			}
			else if (andOrComboBox.SelectedIndex == 1)
			{
				filter.AndOr = "or";
			}

			filter.ParanthesBegin = paranthesBeginComboBox.Text;
			filter.ParanthesEnd = paranthesEndComboBox.Text;
			filter.Filter2Search = true;

			_dataViewFilter.Add(filter);
		}
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

	private void AddComboBoxEventHandlers()
	{
		foreach (ComboBoxGroupUserControl comboBoxGroup in _comboBoxGroups)
		{
			comboBoxGroup.GetColumnComboBox().SelectedIndexChanged += ColumnComboBox_SelectedIndexChanged;
			comboBoxGroup.GetValueComboBox().KeyDown += ValueComboBox_KeyDown;
		}
	}

	private void OperatorComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox operatorComboBox = (ComboBox)sender;
		ComboBox columnComboBox = new ComboBox();

		for (int i = 0; i < _comboBoxGroups.Count; i++)
		{
			if (_comboBoxGroups[i].GetOperatorComboBox() == operatorComboBox)
			{
				columnComboBox = _comboBoxGroups[i].GetColumnComboBox();
				break;
			}
		}

		SetValueControlToolTip(columnComboBox);
	}

	private void SetValueControlToolTip(ComboBox columnComboBox)
	{
		Control valueControl = new Control();
		ComboBox operatorComboBox = new ComboBox();

		for (int i = 0; i < _comboBoxGroups.Count; i++)
		{
			if (_comboBoxGroups[i].GetColumnComboBox() == columnComboBox)
			{
				valueControl = _comboBoxGroups[i].GetValueComboBox();
				operatorComboBox = _comboBoxGroups[i].GetOperatorComboBox();
				break;
			}
		}

		UpdateToolTipText(valueControl, null);

		if (operatorComboBox.Text == GenericHelper.GetInText() || operatorComboBox.Text == GenericHelper.GetNotInText())
		{
			UpdateToolTipText(valueControl, _searchBoxListToolTipText);
		}
		else if (operatorComboBox.Text != GenericHelper.GetLessThanText() && operatorComboBox.Text != GenericHelper.GetGreaterThanText())
		{
			UpdateToolTipText(valueControl, _searchBoxToolTipText);
		}
	}

	private void ValueComboBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			ApplyFilter();
		}
	}

	private void ColumnComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox columnComboBox = (ComboBox)sender;

		InitializeValueComboBox(columnComboBox);

		if (columnComboBox.SelectedIndex == 0)
		{
			SetOtherComboBoxes(columnComboBox, false);
		}
		else
		{
			SetOtherComboBoxes(columnComboBox, true);
		}
	}

	private void ComboBoxGroup_ReloadValuesCheckBoxChangedEvent(object sender)
	{
		ComboBoxGroupUserControl comboBoxGroupUserControl = (ComboBoxGroupUserControl)sender;

		ComboBox columnComboBox = comboBoxGroupUserControl.GetColumnComboBox();
		InitializeValueComboBox(columnComboBox);
	}

	private void SetOtherComboBoxes(ComboBox columnComboBox, bool enabled)
	{
		for (int i = 0; i < _comboBoxGroups.Count; i++)
		{
			if (_comboBoxGroups[i].GetColumnComboBox() == columnComboBox)
			{
				_comboBoxGroups[i].GetOperatorComboBox().Enabled = enabled;
				_comboBoxGroups[i].GetValueComboBox().Enabled = enabled;
				break;
			}
		}
	}

	private void AddGroup()
	{
		int numberOfGroups = _comboBoxGroups.Count;

		ComboBoxGroupUserControl newComboBoxGroupUserControl = new ComboBoxGroupUserControl();
		newComboBoxGroupUserControl.InitializeNotFirst();
		newComboBoxGroupUserControl.TabIndex = 9 + numberOfGroups;
		newComboBoxGroupUserControl.Name = string.Format("comboBoxGroupUserControl{0}", numberOfGroups + 1);

		flowLayoutPanel1.Controls.Add(newComboBoxGroupUserControl);

		InitializeParanthesBeginComboBoxe(newComboBoxGroupUserControl.GetParanthesBeginComboBox());
		InitializeParanthesEndComboBoxe(newComboBoxGroupUserControl.GetParanthesEndComboBox());
		InitializeAndOrComboBox(newComboBoxGroupUserControl.GetAndOrComboBox());
		InitializeColumnComboBox(newComboBoxGroupUserControl.GetColumnComboBox());
		InitializeOperatorComboBox(newComboBoxGroupUserControl.GetOperatorComboBox());
		InitializeToolTip(newComboBoxGroupUserControl.GetValueComboBox(), _searchBoxToolTipText);

		newComboBoxGroupUserControl.GetColumnComboBox().SelectedIndexChanged += ColumnComboBox_SelectedIndexChanged;
		newComboBoxGroupUserControl.GetValueComboBox().KeyDown += ValueComboBox_KeyDown;
		newComboBoxGroupUserControl.GetEnabledCheckBox().CheckedChanged += EnabledCheckBox1_CheckedChanged;
		newComboBoxGroupUserControl.ReloadValuesCheckBoxChangedEvent += ComboBoxGroup_ReloadValuesCheckBoxChangedEvent;

		_comboBoxGroups.Add(newComboBoxGroupUserControl);

		flowLayoutPanel1.ScrollControlIntoView(newComboBoxGroupUserControl);

		removeButton.Enabled = true;
	}

	private void AddButton_Click(object sender, EventArgs e)
	{
		AddGroup();
	}

	private void RemoveButton_Click(object sender, EventArgs e)
	{
		int lastComboBoxGroupId = _comboBoxGroups.Count;
		RemoveComboBoxGroup(lastComboBoxGroupId);
	}

	private void RemoveComboBoxGroup(int comboBoxGroupId)
	{
		ComboBoxGroupUserControl comboBoxGroupUserControlToRemove = new ComboBoxGroupUserControl();

		foreach (Control control in flowLayoutPanel1.Controls)
		{
			if (control is ComboBoxGroupUserControl)
			{
				ComboBoxGroupUserControl comboBoxGroupUserControl = (ComboBoxGroupUserControl)control;

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

	private static void EnabledCheckBoxChanged(CheckBox checkBox)
	{
		ComboBoxGroupUserControl comboBoxGroupUserControl = (ComboBoxGroupUserControl)checkBox.Parent;
		comboBoxGroupUserControl.GetAndOrComboBox().Enabled = checkBox.Checked;
		comboBoxGroupUserControl.GetColumnComboBox().Enabled = checkBox.Checked;
		comboBoxGroupUserControl.GetParanthesBeginComboBox().Enabled = checkBox.Checked;
		comboBoxGroupUserControl.GetParanthesEndComboBox().Enabled = checkBox.Checked;

		if (comboBoxGroupUserControl.GetColumnComboBox().SelectedIndex > 0)
		{
			comboBoxGroupUserControl.GetOperatorComboBox().Enabled = checkBox.Checked;
			comboBoxGroupUserControl.GetValueComboBox().Enabled = checkBox.Checked;
		}
	}

	private static void EnabledCheckBox1_CheckedChanged(object sender, EventArgs e)
	{
		EnabledCheckBoxChanged((CheckBox)sender);
	}

	private static string GetSavedSearchesXml(List<ComboBoxGroupUserControl> comboBoxGroups, bool showHidden, bool sortAlphabetically)
	{
		StringBuilder sb = new StringBuilder();

		sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
		sb.Append(string.Format("<groups showHidden=\"{0}\" sortAlphabetically=\"{1}\">", showHidden, sortAlphabetically));

		foreach (ComboBoxGroupUserControl comboBoxGroup in comboBoxGroups)
		{
			ComboBox paranthesBeginComboBox = comboBoxGroup.GetParanthesBeginComboBox();
			ComboBox paranthesEndComboBox = comboBoxGroup.GetParanthesEndComboBox();
			ComboBox andOrComboBox = comboBoxGroup.GetAndOrComboBox();
			ComboBox columnComboBox = comboBoxGroup.GetColumnComboBox();
			ComboBox operatorComboBox = comboBoxGroup.GetOperatorComboBox();
			Control valueControl = comboBoxGroup.GetValueComboBox();
			CheckBox enabledCheckBox = comboBoxGroup.GetEnabledCheckBox();

			sb.Append(string.Format("<group andOr=\"{0}\" paranthesBegin=\"{1}\" column=\"{2}\" operator=\"{3}\" value=\"{4}\" paranthesEnd=\"{5}\" enabled=\"{6}\" />", System.Security.SecurityElement.Escape(GetSaveAndOrText(andOrComboBox.Text)), paranthesBeginComboBox.Text, System.Security.SecurityElement.Escape(columnComboBox.Text), System.Security.SecurityElement.Escape(GetSaveOperatorText(operatorComboBox.Text)), System.Security.SecurityElement.Escape(GetSaveValueText(valueControl.Text, columnComboBox.Text)), paranthesEndComboBox.Text, enabledCheckBox.Checked));
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
			SetLastGroupValues(GetLoadOperatorText("Equal"), "", true);
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
				string paranthesBeginValue = xmlElement.GetAttribute("paranthesBegin");
				string paranthesEndValue = xmlElement.GetAttribute("paranthesEnd");
				string andOrValue = GetLoadAndOrText(xmlElement.GetAttribute("andOr"));
				string columnValue = xmlElement.GetAttribute("column");
				string operatorValue = GetLoadOperatorText(xmlElement.GetAttribute("operator"));
				string valueValue = GetLoadValueText(xmlElement.GetAttribute("value"), columnValue);
				bool enabledValue = Convert.ToBoolean(xmlElement.GetAttribute("enabled"));

				SetShowHidden(showHiddenValue);
				SetSortAlphabetically(sortAlphabeticallyValue);

				if (i > 0)
				{
					AddGroup();
				}

				SetLastGroupValues(andOrValue, columnValue, operatorValue, valueValue, enabledValue, paranthesBeginValue, paranthesEndValue);

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

		ComboBoxGroupUserControl firstComboBoxGroupUserControl = _comboBoxGroups[0];
		flowLayoutPanel1.ScrollControlIntoView(firstComboBoxGroupUserControl);
	}

	private void InitializeFirstGroup()
	{
		EnabledCheckBoxChanged(_comboBoxGroups[0].GetEnabledCheckBox());
	}

	private static string GetColumnDataTypeFromName(string name)
	{
		foreach (KeyValuePair<string, string[]> column in DalTranslator.GetTraceData().Columns)
		{
			if (column.Value[0] == name)
			{
				return column.Value[2];
			}
		}

		return null;
	}

	private void SetLastGroupValues(string operatorValue, string valueValue, bool enabledValue)
	{
		int lastComboBoxGroupId = _comboBoxGroups.Count - 1;

		_comboBoxGroups[lastComboBoxGroupId].GetParanthesBeginComboBox().Text = "";
		_comboBoxGroups[lastComboBoxGroupId].GetParanthesEndComboBox().Text = "";
		_comboBoxGroups[lastComboBoxGroupId].GetOperatorComboBox().Text = operatorValue;
		_comboBoxGroups[lastComboBoxGroupId].GetValueComboBox().Text = valueValue;
		_comboBoxGroups[lastComboBoxGroupId].GetEnabledCheckBox().Checked = enabledValue;
	}

	private void SetLastGroupValues(string andOrValue, string columnValue, string operatorValue, string valueValue, bool enabledValue, string paranthesBeginValue, string paranthesEndValue)
	{
		int lastComboBoxGroupId = _comboBoxGroups.Count - 1;

		_comboBoxGroups[lastComboBoxGroupId].GetParanthesBeginComboBox().Text = paranthesBeginValue;
		_comboBoxGroups[lastComboBoxGroupId].GetParanthesEndComboBox().Text = paranthesEndValue;
		_comboBoxGroups[lastComboBoxGroupId].GetColumnComboBox().Text = columnValue;
		_comboBoxGroups[lastComboBoxGroupId].GetAndOrComboBox().Text = andOrValue;
		_comboBoxGroups[lastComboBoxGroupId].GetOperatorComboBox().Text = operatorValue;
		_comboBoxGroups[lastComboBoxGroupId].GetValueComboBox().Text = valueValue;
		_comboBoxGroups[lastComboBoxGroupId].GetEnabledCheckBox().Checked = enabledValue;
	}

	private static string GetLoadValueText(string input, string columnComboBoxName)
	{
		string dataType = GetColumnDataTypeFromName(columnComboBoxName);

		if (dataType == "Boolean" || dataType == "BooleanList")
		{
			if (ConfigHandler.UseTranslation)
			{
				if ("Yes" == input)
				{
					return GenericHelper.GetYesText();
				}
				else if ("No" == input)
				{
					return GenericHelper.GetNoText();
				}
			}
		}

		return input;
	}

	private static string GetLoadAndOrText(string input)
	{
		if (ConfigHandler.UseTranslation)
		{
			if ("and" == input)
			{
				return GenericHelper.GetAndText();
			}
			else if ("or" == input)
			{
				return GenericHelper.GetOrText();
			}
		}

		return input;
	}

	private static string GetLoadOperatorText(string input)
	{
		if (ConfigHandler.UseTranslation)
		{
			if ("Equal" == input)
			{
				return GenericHelper.GetEqualText();
			}
			else if ("Not equal" == input)
			{
				return GenericHelper.GetNotEqualText();
			}
			else if ("In" == input)
			{
				return GenericHelper.GetInText();
			}
			else if ("Not in" == input)
			{
				return GenericHelper.GetNotInText();
			}
			else if ("Greater than" == input)
			{
				return GenericHelper.GetGreaterThanText();
			}
			else if ("Less than" == input)
			{
				return GenericHelper.GetLessThanText();
			}
		}

		return input;
	}

	private static string GetSaveValueText(string input, string columnComboBoxName)
	{
		string dataType = GetColumnDataTypeFromName(columnComboBoxName);

		if (dataType == "Boolean" || dataType == "BooleanList")
		{
			if (ConfigHandler.UseTranslation)
			{
				if (GenericHelper.GetYesText() == input)
				{
					return "Yes";
				}
				else if (GenericHelper.GetNoText() == input)
				{
					return "No";
				}
			}
		}

		return input;
	}

	private static string GetSaveAndOrText(string input)
	{
		if (ConfigHandler.UseTranslation)
		{
			if (GenericHelper.GetAndText() == input)
			{
				return "and";
			}
			else if (GenericHelper.GetOrText() == input)
			{
				return "or";
			}
		}

		return input;
	}

	private static string GetSaveOperatorText(string input)
	{
		if (ConfigHandler.UseTranslation)
		{
			if (GenericHelper.GetEqualText() == input)
			{
				return "Equal";
			}
			else if (GenericHelper.GetNotEqualText() == input)
			{
				return "Not equal";
			}
			else if (GenericHelper.GetInText() == input)
			{
				return "In";
			}
			else if (GenericHelper.GetNotInText() == input)
			{
				return "Not in";
			}
			else if (GenericHelper.GetGreaterThanText() == input)
			{
				return "Greater than";
			}
			else if (GenericHelper.GetLessThanText() == input)
			{
				return "Less than";
			}
		}

		return input;
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

	private bool CheckValidParantheses()
	{
		int paranthesBeginCount = 0;
		int paranthesEndCount = 0;
		bool paranthesError = false;

		foreach (ComboBoxGroupUserControl comboBoxGroup in _comboBoxGroups)
		{
			if (comboBoxGroup.GetEnabledCheckBox().Checked && comboBoxGroup.GetColumnComboBox().SelectedIndex > 0)
			{
				string paranthesBegin = comboBoxGroup.GetParanthesBeginComboBox().Text;
				string paranthesEnd = comboBoxGroup.GetParanthesEndComboBox().Text;

				paranthesBeginCount = paranthesBeginCount + paranthesBegin.Length;
				paranthesEndCount = paranthesEndCount + paranthesEnd.Length;

				if (paranthesEndCount > paranthesBeginCount)
				{
					paranthesError = true;
					break;
				}
			}
		}

		if (paranthesBeginCount != paranthesEndCount || paranthesError)
		{
			string text = "Not all parantheses are closed.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("NotClosedParanthes");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			return false;
		}

		return true;
	}

	private class CustomToolStripMenuItem : ToolStripMenuItem
	{
		public int Index;
	}
}
