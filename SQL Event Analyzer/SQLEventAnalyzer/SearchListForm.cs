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
using System.Text.RegularExpressions;
using System.Windows.Forms;

public partial class SearchListForm : Form
{
	public delegate void SearchEventHandler(int foundIndex, string searchTerm);
	public event SearchEventHandler SearchEvent;

	public delegate void RequestUpdateListEventHandler(bool name, bool input, bool output);
	public event RequestUpdateListEventHandler RequestUpdateListEvent;

	private int _currentSearchIndex;
	private int _previousSearchIndex;
	private int _originalSearchIndex;
	private bool _shown;
	private List<string> _searchList;
	private string _previousSearchTerm;
	private bool _requestUpdateListEventPending;

	public SearchListForm()
	{
		InitializeComponent();
	}

	public void Initialize()
	{
		InitializeDictionary();

		if (ConfigHandler.UseTranslation)
		{
			Text = string.Format("{0} - {1}", GenericHelper.ApplicationName, Translator.GetText("findToolStripButton"));
		}
		else
		{
			Text = string.Format("{0} - Search", GenericHelper.ApplicationName);
		}

		downRadioButton.Checked = true;
		showNoMoreMatchesMessageCheckBox.Checked = true;
		wrapAroundCheckBox.Checked = true;

		nameCheckBox.Checked = true;
		inputCheckBox.Checked = true;
		outputCheckBox.Checked = true;

		SearchHistoryHandler.LoadItems(searchTermComboBox, "RecentListSearchHistory");
	}

	public void SetSearchList(List<string> searchList)
	{
		_searchList = searchList;
	}

	public bool IsShown()
	{
		return _shown;
	}

	public void ShowNotFoundMessage()
	{
		string text = "Not found.";

		if (ConfigHandler.UseTranslation)
		{
			text = Translator.GetText("NotFound");
		}

		OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		searchTermComboBox.Focus();
	}

	public void ShowNoMoreMatchesMessage()
	{
		string text = "No more matches.";

		if (ConfigHandler.UseTranslation)
		{
			text = Translator.GetText("NoMoreMatches");
		}

		OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		searchTermComboBox.Focus();
	}

	public void Reset(int startIndex)
	{
		_currentSearchIndex = startIndex;
		_originalSearchIndex = -1;
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			cancelButton.Text = Translator.GetText("cancelButton");
			okButton.Text = Translator.GetText("FindNext");
			searchForLabel.Text = Translator.GetText("searchForLabel");
			matchCaseCheckBox.Text = Translator.GetText("matchCaseCheckBox");
			matchWholeWordCheckBox.Text = Translator.GetText("matchWholeWordCheckBox");
			wrapAroundCheckBox.Text = Translator.GetText("wrapAroundCheckBox");
			showNoMoreMatchesMessageCheckBox.Text = Translator.GetText("showNoMoreMatchesMessageCheckBox");
			directionGroupBox.Text = Translator.GetText("directionGroupBox");
			searchInGroupBox.Text = Translator.GetText("searchInGroupBox");
			downRadioButton.Text = Translator.GetText("down");
			upRadioButton.Text = Translator.GetText("up");
			useRegExCheckBox.Text = Translator.GetText("useRegExCheckBox");
			optionsGroupBox.Text = Translator.GetText("optionsGroupBox");
			searchGroupBox.Text = Translator.GetText("searchGroupBox");
			nameCheckBox.Text = Translator.GetText("Name");
			inputCheckBox.Text = Translator.GetText("Input");
			outputCheckBox.Text = Translator.GetText("Output");
		}
	}

	private enum SearchDirection
	{
		Up,
		Down
	}

	private SearchDirection GetSearchDirection()
	{
		if (upRadioButton.Checked)
		{
			return SearchDirection.Up;
		}
		else
		{
			return SearchDirection.Down;
		}
	}

	private void FireSearchEvent(int foundIndex, string searchTerm)
	{
		if (SearchEvent != null)
		{
			SearchEvent(foundIndex, searchTerm);
		}
	}

	private void FireRequestUpdateListEvent(bool name, bool input, bool output)
	{
		if (RequestUpdateListEvent != null)
		{
			RequestUpdateListEvent(name, input, output);
		}
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		if (_requestUpdateListEventPending)
		{
			_requestUpdateListEventPending = false;
			FireRequestUpdateListEvent(nameCheckBox.Checked, inputCheckBox.Checked, outputCheckBox.Checked);
		}

		if (searchTermComboBox.Text != _previousSearchTerm)
		{
			_previousSearchTerm = searchTermComboBox.Text;
			Reset(_currentSearchIndex);
			SearchHistoryHandler.AddItem(searchTermComboBox, searchTermComboBox.Text, "RecentListSearchHistory");
		}

		SearchInList();
	}

	private void SearchInList()
	{
		int foundIndex = SearchFromIndexToEdge();

		if (foundIndex == -1)
		{
			if (wrapAroundCheckBox.Checked)
			{
				foundIndex = SearchFromEdgeToIndex();
			}

			if (foundIndex == -1)
			{
				ShowNotFoundMessage();
				Reset(_currentSearchIndex);
			}
		}
	}

	private int SearchFromIndexToEdge()
	{
		int foundIndex;

		if (GetSearchDirection() == SearchDirection.Up)
		{
			if (_currentSearchIndex == 0 && wrapAroundCheckBox.Checked)
			{
				_currentSearchIndex = _searchList.Count;
			}

			foundIndex = Search(searchTermComboBox.Text, _currentSearchIndex - 1, 0);
		}
		else
		{
			if (_currentSearchIndex > _searchList.Count - 1 && wrapAroundCheckBox.Checked)
			{
				_currentSearchIndex = 0;
			}

			foundIndex = Search(searchTermComboBox.Text, _currentSearchIndex + 1, _searchList.Count - 1);
		}

		return foundIndex;
	}

	private int SearchFromEdgeToIndex()
	{
		int foundIndex;

		if (GetSearchDirection() == SearchDirection.Up)
		{
			foundIndex = Search(searchTermComboBox.Text, _searchList.Count - 1, _currentSearchIndex);
		}
		else
		{
			foundIndex = Search(searchTermComboBox.Text, 0, _currentSearchIndex);
		}

		return foundIndex;
	}

	private int Search(string searchTerm, int rangeBegin, int rangeEnd)
	{
		int foundIndex = -1;

		if (GetSearchDirection() == SearchDirection.Up)
		{
			for (int i = rangeBegin; i >= rangeEnd; i--)
			{
				foundIndex = DoSearch(searchTerm, i);

				if (foundIndex == -2)
				{
					break;
				}

				if (foundIndex >= 0)
				{
					return foundIndex;
				}
			}
		}
		else
		{
			for (int i = rangeBegin; i <= rangeEnd; i++)
			{
				foundIndex = DoSearch(searchTerm, i);

				if (foundIndex == -2)
				{
					break;
				}

				if (foundIndex >= 0)
				{
					return foundIndex;
				}
			}
		}

		return foundIndex;
	}

	private int DoSearch(string searchTerm, int index)
	{
		if (_searchList.Count == 0)
		{
			return -1;
		}

		int foundIndex = GetSearchTermInText(searchTerm, _searchList[index]);

		if (foundIndex >= 0)
		{
			foundIndex = index;

			if (foundIndex == _originalSearchIndex && showNoMoreMatchesMessageCheckBox.Checked)
			{
				ShowNoMoreMatchesMessage();
				Reset(_previousSearchIndex);
				foundIndex = _previousSearchIndex;
			}
			else
			{
				FoundMatch(foundIndex);
			}
		}

		return foundIndex;
	}

	private void FoundMatch(int foundIndex)
	{
		FireSearchEvent(foundIndex, searchTermComboBox.Text);

		_previousSearchIndex = foundIndex;

		if (_originalSearchIndex == -1)
		{
			_originalSearchIndex = foundIndex;
		}

		_currentSearchIndex = foundIndex;
	}

	private int GetSearchTermInText(string searchTerm, string searchText)
	{
		string checkSearchTerm;
		string checkSearchItem;

		if (matchCaseCheckBox.Checked)
		{
			checkSearchTerm = searchTerm;
			checkSearchItem = searchText;
		}
		else
		{
			checkSearchTerm = searchTerm.ToLower();
			checkSearchItem = searchText.ToLower();
		}

		if (useRegExCheckBox.Checked)
		{
			return SearchRegEx(checkSearchItem, checkSearchTerm);
		}
		else
		{
			return SearchNormal(checkSearchItem, checkSearchTerm);
		}
	}

	private int SearchRegEx(string checkSearchItem, string checkSearchTerm)
	{
		string pattern = checkSearchTerm;

		if (matchWholeWordCheckBox.Checked)
		{
			pattern = string.Format(@"\b({0})\b", checkSearchTerm);
		}

		Match match;

		try
		{
			match = Regex.Match(checkSearchItem, pattern);
		}
		catch (Exception ex)
		{
			OutputHandler.Show(ex.Message, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			return -2;
		}

		if (match.Success)
		{
			return match.Index;
		}
		else
		{
			return -1;
		}
	}

	private int SearchNormal(string checkSearchItem, string checkSearchTerm)
	{
		if (matchWholeWordCheckBox.Checked)
		{
			checkSearchItem = string.Format(" {0} ", checkSearchItem);
			checkSearchTerm = string.Format(" {0} ", checkSearchTerm);
		}

		return checkSearchItem.IndexOf(checkSearchTerm);
	}

	private void CancelButton_Click(object sender, EventArgs e)
	{
		_shown = false;
		Owner.Activate();
		Hide();
	}

	private void SearchForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		_shown = false;
		Owner.Activate();
		Hide();
		e.Cancel = true;
	}

	private void SearchForm_Load(object sender, EventArgs e)
	{
		if (Owner != null)
		{
			Location = new System.Drawing.Point(Owner.Location.X + (Owner.Width - Width) / 2, Owner.Location.Y + (Owner.Height - Height) / 2);
		}
	}

	private void UpRadioButton_CheckedChanged(object sender, EventArgs e)
	{
		RadioButton radioButton = (RadioButton)sender;

		if (radioButton.Checked)
		{
			Reset(_currentSearchIndex);
			searchTermComboBox.Focus();
		}
	}

	private void DownRadioButton_CheckedChanged(object sender, EventArgs e)
	{
		RadioButton radioButton = (RadioButton)sender;

		if (radioButton.Checked)
		{
			Reset(_currentSearchIndex);
			searchTermComboBox.Focus();
		}
	}

	private void ShowNoMoreMatchesMessageCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		searchTermComboBox.Focus();
	}

	private void MatchWholeWordCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		Reset(_currentSearchIndex);
		searchTermComboBox.Focus();
	}

	private void MatchCaseCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		Reset(_currentSearchIndex);
		searchTermComboBox.Focus();
	}

	private void WrapAroundCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		searchTermComboBox.Focus();
	}

	private void SearchForm_Activated(object sender, EventArgs e)
	{
		_shown = true;
		searchTermComboBox.SelectAll();
		searchTermComboBox.Focus();
	}

	private void NameCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		SearchInCheckBoxChanged();
	}

	private void InputCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		SearchInCheckBoxChanged();
	}

	private void OutputCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		SearchInCheckBoxChanged();
	}

	private void SearchInCheckBoxChanged()
	{
		if (searchTermComboBox.Text.Length > 0 && AnySearchInCheckBoxChecked())
		{
			okButton.Enabled = true;
			_requestUpdateListEventPending = true;
		}
		else
		{
			okButton.Enabled = false;
		}

		searchTermComboBox.Focus();
	}

	private bool AnySearchInCheckBoxChecked()
	{
		if (!nameCheckBox.Checked && !inputCheckBox.Checked && !outputCheckBox.Checked)
		{
			return false;
		}

		return true;
	}

	private void UseRegExCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		Reset(_currentSearchIndex);
		searchTermComboBox.Focus();
	}

	private void ComboBox1_TextChanged(object sender, EventArgs e)
	{
		if (searchTermComboBox.Text.Length > 0 && AnySearchInCheckBoxChecked())
		{
			okButton.Enabled = true;
		}
		else
		{
			okButton.Enabled = false;
		}
	}
}
