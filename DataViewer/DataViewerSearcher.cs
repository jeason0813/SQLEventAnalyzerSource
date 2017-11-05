/*
Copyright (C) 2017 Lars Hove Christiansen
http://virtcore.com

This file is a part of DataViewer

	DataViewer is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	DataViewer is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with DataViewer. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class DataViewerSearcher
{
	public delegate void SearchEventHandler(string searchTerm, string searchColumn);
	public event SearchEventHandler SearchEvent;

	public delegate void ShowOutputEventHandler(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
	public event ShowOutputEventHandler ShowOutputEvent;

	private readonly SearcherPanel _searcherPanel;
	private readonly OutputHandler _outputHandler;
	private Dictionary<string, string[]> _searchColumns;
	private bool _disableSearch;
	private bool _eventsEnabled = true;

	public DataViewerSearcher(SearcherPanel SearcherPanel, Dictionary<string, string[]> searchColumns)
	{
		_searcherPanel = SearcherPanel;
		_searchColumns = searchColumns;
		_outputHandler = new OutputHandler();

		foreach (KeyValuePair<string, string[]> item in searchColumns)
		{
			bool showField = true;

			if (item.Value.Length >= 4)
			{
				string type = item.Value[4];

				if (type == "NonSearchableShow" || type == "NonSearchableHide" || type == "Active")
				{
					showField = false;
				}
			}

			if (showField)
			{
				KeyValuePair<string, string> comboBoxItem = new KeyValuePair<string, string>(item.Key, item.Value[0]);
				_searcherPanel.SearchComboBox.Items.Add(comboBoxItem);
				_searcherPanel.SearchComboBox.ValueMember = "Value";
				_searcherPanel.SearchComboBox.DisplayMember = "Value";
			}
		}

		InitializeEvents();
	}

	public void UpdateSearchColumns(Dictionary<string, string[]> searchColumns)
	{
		_searchColumns = searchColumns;
	}

	public void Search()
	{
		DoSearch();
	}

	public void UpdateSearchControl(Control newSearchControl)
	{
		_searcherPanel.SearchTermTextBox = newSearchControl;
		_searcherPanel.SearchTermTextBox.KeyDown += SearchTermTextBox_KeyDown;
	}

	public void SetEventsEnabled(bool value)
	{
		_eventsEnabled = value;
	}

	public void DisableSearch()
	{
		_disableSearch = true;
		_searcherPanel.SearchComboBox.Enabled = false;
		_searcherPanel.SearchTermTextBox.Enabled = false;
		_searcherPanel.SearchButton.Enabled = false;
		_searcherPanel.ClearSearchButton.Enabled = false;
	}

	public void SetSearchTermText(string searchTerm)
	{
		_searcherPanel.SearchTermTextBox.Text = searchTerm;
	}

	public void SetSearchComboBox(string searchColumn)
	{
		if (searchColumn != "")
		{
			Dictionary<string, string[]> databaseSearchColumns = _searchColumns;
			string searchDatabaseColumn = databaseSearchColumns[searchColumn][0];

			KeyValuePair<string, string> comboBoxItem = new KeyValuePair<string, string>(searchColumn, searchDatabaseColumn);
			_searcherPanel.SearchComboBox.SelectedItem = comboBoxItem;

			SetClearButton();
		}
	}

	private void InitializeEvents()
	{
		_searcherPanel.SearchButton.Click += SearchButton_Click;
		_searcherPanel.ClearSearchButton.Click += ClearSearchButton_Click;
		_searcherPanel.SearchTermTextBox.KeyDown += SearchTermTextBox_KeyDown;
		_outputHandler.ShowOutputEvent += OutputHandler_ShowOutputEvent;
	}

	private void SetClearButton()
	{
		if (_searcherPanel.SearchTermTextBox.Text == "")
		{
			_searcherPanel.ClearSearchButton.Enabled = false;
		}
		else
		{
			if (!_disableSearch)
			{
				_searcherPanel.ClearSearchButton.Enabled = true;
			}
		}
	}

	private void FireSearchEvent()
	{
		if (SearchEvent != null)
		{
			SetClearButton();

			string searchColumn = ((KeyValuePair<string, string>)_searcherPanel.SearchComboBox.SelectedItem).Key;
			SearchEvent(_searcherPanel.SearchTermTextBox.Text, searchColumn);
		}
	}

	private void SearchTermTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (_eventsEnabled)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (IsValidData())
				{
					FireSearchEvent();
				}
			}
		}
	}

	private void SearchButton_Click(object sender, EventArgs e)
	{
		DoSearch();
	}

	private void DoSearch()
	{
		if (_eventsEnabled)
		{
			if (IsValidData())
			{
				FireSearchEvent();
			}
		}
	}

	private void ClearSearchButton_Click(object sender, EventArgs e)
	{
		if (_eventsEnabled)
		{
			_searcherPanel.SearchTermTextBox.Text = "";
			_searcherPanel.ClearSearchButton.Enabled = false;
			FireSearchEvent();
		}
	}

	private bool IsValidData()
	{
		if (_searcherPanel.SearchComboBox.SelectedItem == null)
		{
			return false;
		}

		bool success = true;

		string searchColumn = ((KeyValuePair<string, string>)_searcherPanel.SearchComboBox.SelectedItem).Key;

		if (_searchColumns[searchColumn].Length >= 3)
		{
			string fieldName = _searchColumns[searchColumn][0];
			string dataType = _searchColumns[searchColumn][2];
			string searchTerm = _searcherPanel.SearchTermTextBox.Text;

			if (dataType == "Integer" && !searchTerm.Contains("*") && !searchTerm.Contains("%") && searchTerm != "")
			{
				success = CheckValidInteger(searchTerm, fieldName);
			}
		}

		return success;
	}

	private bool CheckValidInteger(string value, string fieldName)
	{
		int outValue;
		bool success = int.TryParse(value, out outValue);

		if (!success)
		{
			string text = string.Format("\"{0}\" {1}.", fieldName, _searcherPanel.NotValidIntegerText);
			_outputHandler.Show(text, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		return success;
	}

	private void OutputHandler_ShowOutputEvent(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
	{
		if (ShowOutputEvent != null)
		{
			ShowOutputEvent(text, caption, buttons, icon);
		}
	}
}
