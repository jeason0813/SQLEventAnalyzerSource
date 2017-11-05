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
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public partial class ManageColumnsForm : Form
{
	private Rectangle _dragBoxFromMouseDown;
	private int _rowIndexFromMouseDown;
	private int _rowIndexOfItemUnderMouseToDrop;
	private readonly List<Column> _copiedItems = new List<Column>();
	private bool _cutActivated;
	private bool _changesMade;
	private bool _reloadDataViewer;
	private readonly ColumnCollection _initialColumnCollection = new ColumnCollection();
	private SearchListForm _searchForm;
	private List<string> _searchList;
	private bool _selectionChangeFromSearch;
	private bool _searchInName = true;
	private bool _searchInInput = true;
	private bool _searchInOutput = true;
	private CustomColumnsCheckForUpdatesForm _customColumnsCheckForUpdatesForm;
	private int _parametersDataGridViewMouseRowHit;

	public ManageColumnsForm()
	{
		InitializeComponent();
	}

	public void Initialize()
	{
		SetTitle();
		SetCheckForUpdatesEnabled();
		InitializeDictionary();
		SetSize();
		CopyColumnCollectionToInitialColumnCollection();
		InitializeSearchForm();
		FillRecentFilesMenu();
		FillList();
		FillParametersGrid();
		SelectFirstItem();
		FillCustomColumnsDropDown();
		SetRestartToUpdate();
	}

	public bool AnyChanges()
	{
		return _reloadDataViewer;
	}

	public bool LoadColumnXmlFile(string fileName)
	{
		bool success = false;

		if (File.Exists(fileName))
		{
			string xml = XmlHelper.ReadXmlFromFile(fileName);

			if (xml != null)
			{
				success = Import(xml);

				if (success)
				{
					CheckForCustomColumnsUpdate(fileName);
					SetFileName(fileName);
					SetTitle();
					SetCheckForUpdatesEnabled();
				}
			}
		}
		else
		{
			string text = "Column Set File \"{0}\" not found.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("fileNotFound");
			}

			OutputHandler.Show(string.Format(text, fileName), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		return success;
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			okButton.Text = Translator.GetText("okButton");
			columnsGroupBox.Text = Translator.GetText("Columns");
			ItemName.HeaderText = Translator.GetText("Name");
			HiddenColumn.HeaderText = Translator.GetText("hiddenCheckBox");
			editButton.Text = Translator.GetText("editButton");
			moveDownButton.Text = Translator.GetText("moveDownButton");
			moveUpButton.Text = Translator.GetText("moveUpButton");
			deleteButton.Text = Translator.GetText("toolStripMenuItemDelete");
			createButton.Text = Translator.GetText("createButton");
			fileToolStripMenuItem.Text = Translator.GetText("fileToolStripMenuItem");
			importToolStripMenuItem.Text = Translator.GetText("importToolStripMenuItem");
			exportToolStripMenuItem.Text = Translator.GetText("SaveSessionAs");
			recentFilesToolStripMenuItem.Text = Translator.GetText("recentFilesToolStripMenuItem");
			closeToolStripMenuItem.Text = Translator.GetText("closeToolStripMenuItem");
			actionToolStripMenuItem.Text = Translator.GetText("actionToolStripMenuItem");
			createMenuItem1.Text = Translator.GetText("createButton");
			editMenuItem1.Text = Translator.GetText("editButton");
			deleteMenuItem1.Text = Translator.GetText("toolStripMenuItemDelete");
			cutMenuItem1.Text = Translator.GetText("toolStripMenuItemCut");
			copyMenuItem1.Text = Translator.GetText("toolStripMenuItemCopy");
			pasteMenuItem1.Text = Translator.GetText("toolStripMenuItemPaste");
			selectAllMenuItem1.Text = Translator.GetText("toolStripMenuItemSelectAll");
			moveUpMenuItem1.Text = Translator.GetText("moveUpButton");
			moveDownMenuItem1.Text = Translator.GetText("moveDownButton");
			saveFileDialog1.Filter = Translator.GetText("xmlFilter");
			openFileDialog1.Filter = Translator.GetText("xmlFilter");
			createMenuItem.Text = Translator.GetText("createButton");
			editMenuItem.Text = Translator.GetText("editButton");
			deleteMenuItem.Text = Translator.GetText("toolStripMenuItemDelete");
			cutMenuItem.Text = Translator.GetText("toolStripMenuItemCut");
			copyMenuItem.Text = Translator.GetText("toolStripMenuItemCopy");
			pasteMenuItem.Text = Translator.GetText("toolStripMenuItemPaste");
			selectAllMenuItem.Text = Translator.GetText("toolStripMenuItemSelectAll");
			moveUpMenuItem.Text = Translator.GetText("moveUpButton");
			moveDownMenuItem.Text = Translator.GetText("moveDownButton");
			searchToolStripMenuItem.Text = Translator.GetText("findToolStripMenuItem");
			searchToolStripMenuItem1.Text = Translator.GetText("findToolStripMenuItem");
			hiddenMenuItem.Text = Translator.GetText("hiddenCheckBox");
			hiddenMenuItem1.Text = Translator.GetText("hiddenCheckBox");
			shownMenuItem.Text = Translator.GetText("shown");
			shownMenuItem1.Text = Translator.GetText("shown");
			activeMenuItem.Text = Translator.GetText("enabledCheckBox");
			activeMenuItem1.Text = Translator.GetText("enabledCheckBox");
			inactiveMenuItem.Text = Translator.GetText("inactive");
			inactiveMenuItem1.Text = Translator.GetText("inactive");
			changeMenuItem.Text = Translator.GetText("change");
			changeMenuItem1.Text = Translator.GetText("change");
			toggleHiddenMenuItem.Text = Translator.GetText("toggleHidden");
			toggleHiddenMenuItem1.Text = Translator.GetText("toggleHidden");
			toggleActiveMenuItem.Text = Translator.GetText("toggleActive");
			toggleActiveMenuItem1.Text = Translator.GetText("toggleActive");
			saveToolStripMenuItem.Text = Translator.GetText("saveToolStripMenuItem");
			parametersGroupBox.Text = Translator.GetText("Parameters");
			NameColumn.HeaderText = Translator.GetText("NameColumn");
			ValueColumn.HeaderText = Translator.GetText("ValueColumn");
			deleteSelectedToolStripMenuItem.Text = Translator.GetText("DeleteSelected");
			newSessionToolStripMenuItem.Text = Translator.GetText("NewSession");
			loadColumnSetToolStripMenuItem.Text = Translator.GetText("loadColumnSetToolStripMenuItem");
			onlineToolStripMenuItem.Text = Translator.GetText("onlineToolStripMenuItem");
			settingsToolStripMenuItem.Text = Translator.GetText("settingsToolStripMenuItem");
			checkForUpdatesToolStripMenuItem.Text = Translator.GetText("CheckForUpdates");
		}
	}

	private void CopyColumnCollectionToInitialColumnCollection()
	{
		_initialColumnCollection.Columns.Clear();

		foreach (Column column in ColumnHelper.ColumnCollection.Columns)
		{
			Column newColumn = new Column(column.Name, column.IsolationLevel, column.Input, column.InputType, column.Output, column.OutputType, column.Hidden, column.Enabled, column.Width);
			_initialColumnCollection.Columns.Add(newColumn);
		}

		foreach (Parameter parameter in ColumnHelper.ColumnCollection.Parameters)
		{
			Parameter newParameter = new Parameter(parameter.Name, parameter.Value);
			_initialColumnCollection.Parameters.Add(newParameter);
		}

		foreach (Option option in ColumnHelper.ColumnCollection.Options)
		{
			Option newOption = new Option(option.Name, option.Value);
			_initialColumnCollection.Options.Add(newOption);
		}
	}

	private void InitializeSearchForm()
	{
		_searchForm = new SearchListForm();
		_searchForm.Initialize();

		_searchForm.SearchEvent += SearchForm_SearchEvent;
		_searchForm.RequestUpdateListEvent += SearchForm_RequestUpdateListEvent;
	}

	private void SearchForm_SearchEvent(int foundIndex, string searchTerm)
	{
		_selectionChangeFromSearch = true;

		columnsDataGridView.FirstDisplayedScrollingRowIndex = foundIndex;
		columnsDataGridView.CurrentCell = columnsDataGridView["ItemName", foundIndex];
		columnsDataGridView.Rows[foundIndex].Selected = true;

		_selectionChangeFromSearch = false;
	}

	private void SearchForm_RequestUpdateListEvent(bool name, bool input, bool output)
	{
		_searchInName = name;
		_searchInInput = input;
		_searchInOutput = output;

		PopulateSearchList();
	}

	private void PopulateSearchList()
	{
		_searchList = new List<string>();

		foreach (DataGridViewRow row in columnsDataGridView.Rows)
		{
			foreach (Column item in ColumnHelper.ColumnCollection.Columns)
			{
				if (row.Cells["ItemName"].Value.ToString() == item.Name)
				{
					string searchText = "";

					if (_searchInName)
					{
						searchText += row.Cells["ItemName"].Value;
					}

					if (_searchInInput)
					{
						if (_searchInName)
						{
							searchText += "\r\n";
						}

						searchText += item.Input;
					}

					if (_searchInOutput)
					{
						if (_searchInInput || _searchInName)
						{
							searchText += "\r\n";
						}

						searchText += item.Output;
					}

					_searchList.Add(searchText);
				}
			}
		}

		_searchForm.SetSearchList(_searchList);

		if (columnsDataGridView.SelectedRows.Count > 0)
		{
			_searchForm.Reset(columnsDataGridView.SelectedRows[0].Index);
		}
	}

	private void FillRecentFilesMenu()
	{
		RecentFilesHandler.LoadMenuItems(recentFilesToolStripMenuItem, "RecentColumnFiles");
		AddEventHandlersToRecentFiles();
	}

	private void SetFileName(string fileName)
	{
		ColumnHelper.ColumnCollectionFileName = fileName;
		SetDefaultColumnName("");
		RecentFilesHandler.AddFileName(recentFilesToolStripMenuItem, fileName, "RecentColumnFiles");
		AddEventHandlersToRecentFiles();
	}

	private void AddEventHandlersToRecentFiles()
	{
		foreach (ToolStripMenuItem existingFileName in recentFilesToolStripMenuItem.DropDownItems)
		{
			if (existingFileName.Text != "-")
			{
				existingFileName.Click += ExistingFileNameMenuItem_Click;
			}
		}
	}

	private bool Import(string xml)
	{
		ColumnCollection temporaryColumns = ColumnHelper.XmlToColumnCollection(xml);

		if (temporaryColumns != null)
		{
			ColumnHelper.ColumnCollection = temporaryColumns;
			FillList();
			FillParametersGrid();
			SelectFirstItem();

			SetChangesMade(false);

			return true;
		}

		return false;
	}

	private void ExistingFileNameMenuItem_Click(object sender, EventArgs e)
	{
		bool success = SaveChanges();

		if (success)
		{
			string fileName = ((ToolStripMenuItem)sender).Text;
			LoadColumnXmlFile(fileName);
		}
	}

	private void FillList()
	{
		columnsDataGridView.Rows.Clear();

		for (int i = 0; i < ColumnHelper.ColumnCollection.Columns.Count; i++)
		{
			int index = columnsDataGridView.Rows.Add();
			DataGridViewRow row = columnsDataGridView.Rows[index];
			row.Cells["ItemName"].Value = ColumnHelper.ColumnCollection.Columns[i].Name;
			row.Cells["ItemName"].ToolTipText = ColumnHelper.ColumnCollection.Columns[i].Name;
			row.Cells["InputTypeColumn"].Value = ColumnHelper.GetColumnTypeName(ColumnHelper.ColumnCollection.Columns[i].InputType);
			row.Cells["OutputTypeColumn"].Value = ColumnHelper.GetColumnTypeName(ColumnHelper.ColumnCollection.Columns[i].OutputType);
			row.Cells["HiddenColumn"].Value = ColumnHelper.ColumnCollection.Columns[i].Hidden;

			if (ColumnHelper.ColumnCollection.Columns[i].Hidden)
			{
				row.DefaultCellStyle.ForeColor = Color.Gray;
				row.DefaultCellStyle.SelectionForeColor = Color.Gray;
			}
			else
			{
				row.DefaultCellStyle.ForeColor = SystemColors.WindowText;
				row.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
			}

			if (ColumnHelper.ColumnCollection.Columns[i].Enabled)
			{
				row.DefaultCellStyle.Font = new Font(Font, FontStyle.Regular);
			}
			else
			{
				row.DefaultCellStyle.Font = new Font(Font, FontStyle.Strikeout);
			}

			if (!ConfigHandler.ClrDeployed && (ConfigHandler.ColumnTypeClrDependant(ColumnHelper.ColumnCollection.Columns[i].InputType) || ConfigHandler.ColumnTypeClrDependant(ColumnHelper.ColumnCollection.Columns[i].OutputType)))
			{
				row.DefaultCellStyle.Font = new Font(Font, FontStyle.Strikeout);
			}
		}

		FillNumberColumn();
		PopulateSearchList();
		EnableItems();
	}

	private void FillParametersGrid()
	{
		bool reloadDataViewer = _reloadDataViewer;
		RemoveParameterGridEvents();

		parametersDataGridView.Rows.Clear();

		foreach (Parameter parameter in ColumnHelper.ColumnCollection.Parameters)
		{
			parametersDataGridView.Rows.Add(parameter.Name, parameter.Value);
		}

		SetChangesMade(false);

		AddParameterGridEvents();
		_reloadDataViewer = reloadDataViewer;
	}

	private void FillNumberColumn()
	{
		for (int i = 0; i < ColumnHelper.ColumnCollection.Columns.Count; i++)
		{
			columnsDataGridView.Rows[i].Cells["NumberColumn"].Value = (i + 1).ToString();
		}
	}

	private void SelectFirstItem()
	{
		if (columnsDataGridView.Rows.Count > 0)
		{
			columnsDataGridView.Rows[0].Selected = true;
			EnableItems();
		}
	}

	private void MoveItem(int currentIndex, int newIndex)
	{
		DataGridViewRow row = columnsDataGridView.Rows[currentIndex];

		columnsDataGridView.Rows.RemoveAt(currentIndex);
		columnsDataGridView.Rows.Insert(newIndex, row);

		columnsDataGridView.CurrentCell = columnsDataGridView.Rows[newIndex].Cells["ItemName"];
		columnsDataGridView.Focus();

		SaveAfterMoveItem();

		FillNumberColumn();
		PopulateSearchList();
		EnableItems();
	}

	private void MoveUpButton_Click(object sender, EventArgs e)
	{
		MoveUp();
	}

	private void MoveUp()
	{
		int currentIndex = columnsDataGridView.SelectedRows[0].Index;
		int newIndex = columnsDataGridView.SelectedRows[0].Index - 1;
		MoveItem(currentIndex, newIndex);
	}

	private void MoveDownButton_Click(object sender, EventArgs e)
	{
		MoveDown();
	}

	private void MoveDown()
	{
		int currentIndex = columnsDataGridView.SelectedRows[0].Index;
		int newIndex = columnsDataGridView.SelectedRows[0].Index + 1;
		MoveItem(currentIndex, newIndex);
	}

	private void SaveAfterMoveItem()
	{
		List<Column> newList = new List<Column>();

		foreach (DataGridViewRow row in columnsDataGridView.Rows)
		{
			foreach (Column item in ColumnHelper.ColumnCollection.Columns)
			{
				if (row.Cells["ItemName"].Value.ToString() == item.Name)
				{
					newList.Add(item);
				}
			}
		}

		ColumnHelper.ColumnCollection.Columns = newList;
		SetChangesMade(true);
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void EnableItems()
	{
		if (columnsDataGridView.SelectedRows.Count == 0)
		{
			DisableItems();
		}
		else if (columnsDataGridView.SelectedRows.Count == 1)
		{
			createButton.Enabled = true;
			editButton.Enabled = true;
			deleteButton.Enabled = true;

			createMenuItem1.Enabled = true;
			editMenuItem1.Enabled = true;
			deleteMenuItem1.Enabled = true;
			cutMenuItem1.Enabled = true;
			copyMenuItem1.Enabled = true;

			createMenuItem.Enabled = true;
			editMenuItem.Enabled = true;
			deleteMenuItem.Enabled = true;
			cutMenuItem.Enabled = true;
			copyMenuItem.Enabled = true;

			changeMenuItem.Enabled = true;
			changeMenuItem1.Enabled = true;
		}
		else if (columnsDataGridView.SelectedRows.Count > 1)
		{
			createButton.Enabled = true;
			editButton.Enabled = false;
			deleteButton.Enabled = true;

			createMenuItem1.Enabled = true;
			editMenuItem1.Enabled = false;
			deleteMenuItem1.Enabled = true;
			cutMenuItem1.Enabled = true;
			copyMenuItem1.Enabled = true;

			createMenuItem.Enabled = true;
			editMenuItem.Enabled = false;
			deleteMenuItem.Enabled = true;
			cutMenuItem.Enabled = true;
			copyMenuItem.Enabled = true;

			changeMenuItem.Enabled = true;
			changeMenuItem1.Enabled = true;
		}

		if (columnsDataGridView.Rows.Count == 0)
		{
			selectAllMenuItem1.Enabled = false;
		}
		else
		{
			selectAllMenuItem1.Enabled = true;
		}

		if (columnsDataGridView.Rows.Count <= 1)
		{
			moveUpButton.Enabled = false;
			moveUpMenuItem1.Enabled = false;
			moveUpMenuItem.Enabled = false;
		}
		else
		{
			if (columnsDataGridView.Rows[0].Selected || columnsDataGridView.SelectedRows.Count == 0 || columnsDataGridView.SelectedRows.Count > 1)
			{
				moveUpButton.Enabled = false;
				moveUpMenuItem1.Enabled = false;
				moveUpMenuItem.Enabled = false;
			}
			else
			{
				moveUpButton.Enabled = true;
				moveUpMenuItem1.Enabled = true;
				moveUpMenuItem.Enabled = true;
			}
		}

		if (columnsDataGridView.Rows.Count <= 1)
		{
			moveDownButton.Enabled = false;
			moveDownMenuItem1.Enabled = false;
			moveDownMenuItem.Enabled = false;
		}
		else
		{
			if (columnsDataGridView.Rows[columnsDataGridView.Rows.Count - 1].Selected || columnsDataGridView.SelectedRows.Count == 0 || columnsDataGridView.SelectedRows.Count > 1)
			{
				moveDownButton.Enabled = false;
				moveDownMenuItem1.Enabled = false;
				moveDownMenuItem.Enabled = false;
			}
			else
			{
				moveDownButton.Enabled = true;
				moveDownMenuItem1.Enabled = true;
				moveDownMenuItem.Enabled = true;
			}
		}
	}

	private void DisableItems()
	{
		createButton.Enabled = true;
		editButton.Enabled = false;
		deleteButton.Enabled = false;

		createMenuItem1.Enabled = true;
		editMenuItem1.Enabled = false;
		deleteMenuItem1.Enabled = false;
		cutMenuItem1.Enabled = false;
		copyMenuItem1.Enabled = false;

		createMenuItem.Enabled = true;
		editMenuItem.Enabled = false;
		deleteMenuItem.Enabled = false;
		cutMenuItem.Enabled = false;
		copyMenuItem.Enabled = false;

		changeMenuItem.Enabled = false;
		changeMenuItem1.Enabled = false;
	}

	private void CreateButton_Click(object sender, EventArgs e)
	{
		Create();
	}

	private void Create()
	{
		HandleColumnForm form = new HandleColumnForm();
		form.Initialize();
		DialogResult result = form.ShowDialog();

		if (result.ToString() == "OK")
		{
			Column newItem = form.GetValue();

			int insertNewRowAt = 0;

			if (columnsDataGridView.SelectedRows.Count > 0)
			{
				insertNewRowAt = columnsDataGridView.SelectedRows[0].Index;
				insertNewRowAt++;
			}

			CreateNewItem(newItem, insertNewRowAt);
			FillNumberColumn();
			PopulateSearchList();

			columnsDataGridView.FirstDisplayedScrollingRowIndex = insertNewRowAt;
			columnsDataGridView.CurrentCell = columnsDataGridView["ItemName", insertNewRowAt];
			columnsDataGridView.Rows[insertNewRowAt].Selected = true;

			SetChangesMade(true);
		}

		columnsDataGridView.Focus();
	}

	private void CreateNewItem(Column newItem, int insertNewRowAt)
	{
		ColumnHelper.ColumnCollection.Columns.Insert(insertNewRowAt, newItem);

		int index = columnsDataGridView.Rows.Add();
		DataGridViewRow row = columnsDataGridView.Rows[index];
		row.Cells["ItemName"].Value = newItem.Name;
		row.Cells["ItemName"].ToolTipText = newItem.Name;
		row.Cells["InputTypeColumn"].Value = ColumnHelper.GetColumnTypeName(newItem.InputType);
		row.Cells["OutputTypeColumn"].Value = ColumnHelper.GetColumnTypeName(newItem.OutputType);
		row.Cells["HiddenColumn"].Value = newItem.Hidden;

		if (newItem.Hidden)
		{
			row.DefaultCellStyle.ForeColor = Color.Gray;
			row.DefaultCellStyle.SelectionForeColor = Color.Gray;
		}
		else
		{
			row.DefaultCellStyle.ForeColor = SystemColors.WindowText;
			row.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
		}

		if (newItem.Enabled)
		{
			row.DefaultCellStyle.Font = new Font(Font, FontStyle.Regular);
		}
		else
		{
			row.DefaultCellStyle.Font = new Font(Font, FontStyle.Strikeout);
		}

		if (!ConfigHandler.ClrDeployed && (ConfigHandler.ColumnTypeClrDependant(newItem.InputType) || ConfigHandler.ColumnTypeClrDependant(newItem.OutputType)))
		{
			row.DefaultCellStyle.Font = new Font(Font, FontStyle.Strikeout);
		}

		columnsDataGridView.Rows.RemoveAt(index);
		columnsDataGridView.Rows.Insert(insertNewRowAt, row);

		SetChangesMade(true);
	}

	private void EditButton_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void Edit()
	{
		List<Column> newItems = ColumnHelper.ColumnCollection.Columns;

		bool save = false;

		foreach (Column item in newItems)
		{
			if (columnsDataGridView.SelectedRows[0].Cells["ItemName"].Value.ToString() == item.Name)
			{
				HandleColumnForm form = new HandleColumnForm();
				form.Initialize();
				form.SetValues(item);
				DialogResult result = form.ShowDialog();

				if (result.ToString() == "OK")
				{
					Column newItem = form.GetValue();
					item.Name = newItem.Name;
					item.IsolationLevel = newItem.IsolationLevel;
					item.Input = newItem.Input;
					item.InputType = newItem.InputType;
					item.Output = newItem.Output;
					item.OutputType = newItem.OutputType;
					item.Hidden = newItem.Hidden;
					item.Enabled = newItem.Enabled;
					item.Width = newItem.Width;

					columnsDataGridView.SelectedRows[0].Cells["ItemName"].Value = newItem.Name;
					columnsDataGridView.SelectedRows[0].Cells["InputTypeColumn"].Value = ColumnHelper.GetColumnTypeName(newItem.InputType);
					columnsDataGridView.SelectedRows[0].Cells["OutputTypeColumn"].Value = ColumnHelper.GetColumnTypeName(newItem.OutputType);
					columnsDataGridView.SelectedRows[0].Cells["HiddenColumn"].Value = newItem.Hidden;

					if (newItem.Hidden)
					{
						columnsDataGridView.SelectedRows[0].DefaultCellStyle.ForeColor = Color.Gray;
						columnsDataGridView.SelectedRows[0].DefaultCellStyle.SelectionForeColor = Color.Gray;
					}
					else
					{
						columnsDataGridView.SelectedRows[0].DefaultCellStyle.ForeColor = SystemColors.WindowText;
						columnsDataGridView.SelectedRows[0].DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
					}

					if (newItem.Enabled)
					{
						columnsDataGridView.SelectedRows[0].DefaultCellStyle.Font = new Font(Font, FontStyle.Regular);
					}
					else
					{
						columnsDataGridView.SelectedRows[0].DefaultCellStyle.Font = new Font(Font, FontStyle.Strikeout);
					}

					if (!ConfigHandler.ClrDeployed && (ConfigHandler.ColumnTypeClrDependant(newItem.InputType) || ConfigHandler.ColumnTypeClrDependant(newItem.OutputType)))
					{
						columnsDataGridView.SelectedRows[0].DefaultCellStyle.Font = new Font(Font, FontStyle.Strikeout);
					}

					save = true;
				}

				break;
			}
		}

		if (save)
		{
			ColumnHelper.ColumnCollection.Columns = newItems;
			FillNumberColumn();
			PopulateSearchList();
			SetChangesMade(true);
		}

		columnsDataGridView.Focus();
	}

	private void SetChangesMade(bool value)
	{
		_reloadDataViewer = true;

		bool setTitle = false;

		if (value != _changesMade)
		{
			setTitle = true;
		}

		_changesMade = value;

		if (_changesMade)
		{
			saveToolStripMenuItem.Enabled = true;
		}
		else
		{
			saveToolStripMenuItem.Enabled = false;
		}

		if (setTitle)
		{
			SetTitle();
		}
	}

	private void SetTitle()
	{
		string fileName = "";

		if (ColumnHelper.ColumnCollectionFileName != null)
		{
			fileName = string.Format(" - {0}", Path.GetFileName(ColumnHelper.ColumnCollectionFileName));
		}

		string changesMade = "";

		if (_changesMade)
		{
			changesMade = " *";
		}

		string title = "Custom Columns";

		if (ConfigHandler.UseTranslation)
		{
			title = Translator.GetText("columnsToolStripButton");
		}

		Text = string.Format("{0}{1}{2}", title, fileName, changesMade);
	}

	private void DeleteButton_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void Delete()
	{
		string text = "Delete selected Columns?";

		if (ConfigHandler.UseTranslation)
		{
			text = Translator.GetText("deleteColumns");
		}

		DialogResult result = OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

		if (result.ToString() == "Yes")
		{
			List<Column> itemsToBeDeleted = new List<Column>();

			foreach (DataGridViewRow row in columnsDataGridView.SelectedRows)
			{
				foreach (Column item in ColumnHelper.ColumnCollection.Columns)
				{
					if (row.Cells["ItemName"].Value.ToString() == item.Name)
					{
						itemsToBeDeleted.Add(item);
					}
				}
			}

			DoDelete(itemsToBeDeleted);
			FillNumberColumn();
			PopulateSearchList();
			EnableItems();
			columnsDataGridView.Focus();
		}
	}

	private void DoDelete(List<Column> itemsToBeDeleted)
	{
		List<int> indexesToBeRemoved = new List<int>();

		for (int r = 0; r < columnsDataGridView.Rows.Count; r++)
		{
			for (int i = 0; i < itemsToBeDeleted.Count; i++)
			{
				if (columnsDataGridView.Rows[r].Cells["ItemName"].Value.ToString() == itemsToBeDeleted[i].Name)
				{
					indexesToBeRemoved.Add(columnsDataGridView.Rows[r].Index);
				}
			}
		}

		indexesToBeRemoved.Sort(new SortIntDescending());

		for (int i = 0; i < indexesToBeRemoved.Count; i++)
		{
			columnsDataGridView.Rows.RemoveAt(indexesToBeRemoved[i]);
			ColumnHelper.ColumnCollection.Columns.RemoveAt(indexesToBeRemoved[i]);
		}

		SetChangesMade(true);
	}

	private class SortIntDescending : IComparer<int>
	{
		int IComparer<int>.Compare(int a, int b)
		{
			if (a > b)
			{
				return -1;
			}
			if (a < b)
			{
				return 1;
			}
			else
			{
				return 0;
			}
		}
	}

	private bool SaveChanges()
	{
		if (_changesMade)
		{
			string text = "Save changes?";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("saveChanges");
			}

			DialogResult result = OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

			if (result.ToString() == "Yes")
			{
				bool success = SaveCollection();

				if (!success)
				{
					return false;
				}
			}
			else if (result.ToString() == "No")
			{
				ColumnHelper.ColumnCollection = _initialColumnCollection;
				_reloadDataViewer = false;
			}
			else if (result.ToString() == "Cancel")
			{
				return false;
			}
		}

		return true;
	}

	private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
	{
		bool success = SaveChanges();

		if (success)
		{
			DialogResult result = openFileDialog1.ShowDialog();
			Application.DoEvents();

			if (result.ToString() == "OK")
			{
				string xml = XmlHelper.ReadXmlFromFile(openFileDialog1.FileName);

				if (xml != null)
				{
					success = Import(xml);

					if (success)
					{
						CheckForCustomColumnsUpdate(openFileDialog1.FileName);
						SetFileName(openFileDialog1.FileName);
						SetTitle();
						SetCheckForUpdatesEnabled();
					}
				}
			}
		}
	}

	private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
	{
		bool reloadDataViewer = _reloadDataViewer;

		string currentFileName = ColumnHelper.ColumnCollectionFileName;
		ColumnHelper.ColumnCollectionFileName = null;

		bool success = SaveCollection();

		if (!success)
		{
			ColumnHelper.ColumnCollectionFileName = currentFileName;
			SetDefaultColumnName("");
		}

		_reloadDataViewer = reloadDataViewer;
	}

	private bool SaveCollection()
	{
		string fileName = GetFileName();

		if (fileName != null)
		{
			ColumnHelper.SaveColumnCollection(fileName);
			CopyColumnCollectionToInitialColumnCollection();
			SetTitle();
			SetChangesMade(false);
			return true;
		}

		return false;
	}

	private void RemoveReservedParameterNames()
	{
		foreach (DataGridViewRow row in parametersDataGridView.Rows)
		{
			string name = "";

			if (row.Cells[0].Value != null)
			{
				name = row.Cells[0].Value.ToString();
			}

			if (name == "{SessionId}")
			{
				string text = "\"{SessionId}\" is a reserved name and can not be used.";

				if (ConfigHandler.UseTranslation)
				{
					text = Translator.GetText("sessionreserved");
				}

				OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}

	private void SaveAfterChangeParameter()
	{
		RemoveReservedParameterNames();

		List<Parameter> newList = new List<Parameter>();

		foreach (DataGridViewRow row in parametersDataGridView.Rows)
		{
			string name = "";
			string value = "";

			if (row.Cells[0].Value != null)
			{
				name = row.Cells[0].Value.ToString();
			}

			if (row.Cells[1].Value != null)
			{
				value = row.Cells[1].Value.ToString();
			}

			if (name != "" && name != "{SessionId}")
			{
				Parameter parameter = new Parameter(name, value);
				newList.Add(parameter);
			}
		}

		ColumnHelper.ColumnCollection.Parameters = newList;
		SetChangesMade(true);
	}

	private string GetFileName()
	{
		if (ColumnHelper.ColumnCollectionFileName == null)
		{
			DialogResult result = saveFileDialog1.ShowDialog();
			Application.DoEvents();

			if (result.ToString() == "OK")
			{
				SetFileName(saveFileDialog1.FileName);
			}
			else
			{
				return null;
			}
		}

		return ColumnHelper.ColumnCollectionFileName;
	}

	private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Close();
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (columnsDataGridView.Focused)
		{
			if (msg.WParam.ToInt32() == (int)Keys.Enter)
			{
				if (columnsDataGridView.SelectedRows.Count == 1)
				{
					Edit();
				}

				return true;
			}

			if ((int)keyData == 196644) // Keys.Shift && Keys.Control && Keys.Home
			{
				SendKeys.Send("+^{UP}");
				return true;
			}
		}
		else if (ActiveControl is DataGridViewTextBoxEditingControl)
		{
			if ((int)keyData == 131155) // CTRL + S
			{
				parametersDataGridView.EndEdit();
				SaveCollection();
				return true;
			}
		}

		return base.ProcessCmdKey(ref msg, keyData);
	}

	private void ColumnsDataGridView_SelectionChanged(object sender, EventArgs e)
	{
		EnableItems();

		if (!_selectionChangeFromSearch)
		{
			if (columnsDataGridView.SelectedRows.Count > 0)
			{
				_searchForm.Reset(columnsDataGridView.SelectedRows[0].Index);
			}
		}
	}

	private void ColumnsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex == -1)
		{
			return;
		}

		if (columnsDataGridView.SelectedRows.Count == 0)
		{
			DisableItems();
		}
		else if (columnsDataGridView.SelectedRows.Count == 1)
		{
			Edit();
		}
	}

	private void ColumnsDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (columnsDataGridView.SelectedRows.Count == 0)
		{
			DisableItems();
		}

		if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
		{
			if (!columnsDataGridView.Rows[e.RowIndex].Selected)
			{
				columnsDataGridView.CurrentCell = columnsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
			}

			Rectangle r = columnsDataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
			contextMenuStrip1.Show((Control)sender, r.Left + e.X, r.Top + e.Y);
		}
	}

	private void ColumnsDataGridView_DragDrop(object sender, DragEventArgs e)
	{
		string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

		if (files != null)
		{
			if (files.Length == 1)
			{
				bool success = SaveChanges();

				if (success)
				{
					string xml = XmlHelper.ReadXmlFromFile(files[0]);

					if (xml != null)
					{
						success = Import(xml);

						if (success)
						{
							CheckForCustomColumnsUpdate(files[0]);
							SetFileName(files[0]);
							SetTitle();
							SetCheckForUpdatesEnabled();
						}
					}
				}
			}
		}
		else
		{
			Point clientPoint = columnsDataGridView.PointToClient(new Point(e.X, e.Y));
			_rowIndexOfItemUnderMouseToDrop = columnsDataGridView.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

			if (e.Effect == DragDropEffects.Move)
			{
				if (_rowIndexOfItemUnderMouseToDrop != -1)
				{
					if (_rowIndexFromMouseDown != _rowIndexOfItemUnderMouseToDrop)
					{
						MoveItem(_rowIndexFromMouseDown, _rowIndexOfItemUnderMouseToDrop);
					}
				}
			}
		}
	}

	private void ColumnsDataGridView_DragOver(object sender, DragEventArgs e)
	{
		e.Effect = DragDropEffects.Move;
	}

	private void ColumnsDataGridView_KeyDown(object sender, KeyEventArgs e)
	{
		if (columnsDataGridView.Rows.Count >= 1)
		{
			EnableItems();
		}

		if (e.KeyData == Keys.Delete)
		{
			if (columnsDataGridView.SelectedRows.Count >= 1)
			{
				Delete();
			}
		}
		else if (e.KeyCode == Keys.N && e.Modifiers == Keys.Control)
		{
			Create();
		}
		else if (e.KeyCode == Keys.X && e.Modifiers == Keys.Control)
		{
			if (columnsDataGridView.SelectedRows.Count >= 1)
			{
				Cut();
			}
		}
		else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
		{
			if (columnsDataGridView.SelectedRows.Count >= 1)
			{
				Copy();
			}
		}
		else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
		{
			if (_copiedItems.Count > 0)
			{
				Paste();
			}
		}
		else if (e.KeyCode == Keys.U && e.Modifiers == Keys.Control)
		{
			if (columnsDataGridView.SelectedRows.Count == 1)
			{
				if (columnsDataGridView.Rows[0].Selected || columnsDataGridView.SelectedRows.Count == 0)
				{
					return;
				}

				MoveUp();
			}
		}
		else if (e.KeyCode == Keys.D && e.Modifiers == Keys.Control)
		{
			if (columnsDataGridView.SelectedRows.Count == 1)
			{
				if (columnsDataGridView.Rows[columnsDataGridView.Rows.Count - 1].Selected || columnsDataGridView.SelectedRows.Count == 0)
				{
					return;
				}

				MoveDown();
			}
		}
	}

	private void ColumnsDataGridView_MouseDown(object sender, MouseEventArgs e)
	{
		_rowIndexFromMouseDown = columnsDataGridView.HitTest(e.X, e.Y).RowIndex;

		if (_rowIndexFromMouseDown != -1)
		{
			Size dragSize = SystemInformation.DragSize;
			_dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
		}
		else
		{
			_dragBoxFromMouseDown = Rectangle.Empty;
		}
	}

	private void ColumnsDataGridView_MouseMove(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			if (_dragBoxFromMouseDown != Rectangle.Empty && !_dragBoxFromMouseDown.Contains(e.X, e.Y))
			{
				columnsDataGridView.DoDragDrop(columnsDataGridView.Rows[_rowIndexFromMouseDown], DragDropEffects.Move);
			}
		}
	}

	private int GetCorrectIndexOfSelectedRow(DataGridViewRow selectedRow)
	{
		foreach (DataGridViewRow row in columnsDataGridView.Rows)
		{
			if (row == selectedRow)
			{
				return row.Index;
			}
		}

		return -1;
	}

	private string FirstNameOfSelectedRows()
	{
		int firstIndex = columnsDataGridView.Rows.Count + 1;
		string firstName = null;

		foreach (Column item in ColumnHelper.ColumnCollection.Columns)
		{
			foreach (DataGridViewRow selectedRow in columnsDataGridView.SelectedRows)
			{
				if (selectedRow.Cells["ItemName"].Value.ToString() == item.Name)
				{
					if (GetCorrectIndexOfSelectedRow(selectedRow) < firstIndex)
					{
						firstIndex = GetCorrectIndexOfSelectedRow(selectedRow);
						firstName = item.Name;
					}
				}
			}
		}

		return firstName;
	}

	private string LastNameOfSelectedRows()
	{
		int lastIndex = 0;
		string lastName = null;

		foreach (Column item in ColumnHelper.ColumnCollection.Columns)
		{
			foreach (DataGridViewRow selectedRow in columnsDataGridView.SelectedRows)
			{
				if (selectedRow.Cells["ItemName"].Value.ToString() == item.Name)
				{
					if (GetCorrectIndexOfSelectedRow(selectedRow) >= lastIndex)
					{
						lastIndex = GetCorrectIndexOfSelectedRow(selectedRow);
						lastName = item.Name;
					}
				}
			}
		}

		return lastName;
	}

	private void Cut()
	{
		_cutActivated = true;
		DoCopy();
	}

	private void Copy()
	{
		_cutActivated = false;
		DoCopy();
	}

	private void DoCopy()
	{
		_copiedItems.Clear();

		for (int i = 0; i < ColumnHelper.ColumnCollection.Columns.Count; i++)
		{
			for (int r = 0; r < columnsDataGridView.SelectedRows.Count; r++)
			{
				if (columnsDataGridView.SelectedRows[r].Cells["ItemName"].Value.ToString() == ColumnHelper.ColumnCollection.Columns[i].Name)
				{
					_copiedItems.Add(ColumnHelper.ColumnCollection.Columns[i]);
				}
			}
		}
	}

	private int GetIndexOfRowFromName(string name)
	{
		foreach (DataGridViewRow row in columnsDataGridView.Rows)
		{
			if (row.Cells["ItemName"].Value.ToString() == name)
			{
				return row.Index;
			}
		}

		return -1;
	}

	private bool ValidNameLength()
	{
		bool valid = true;

		foreach (Column itemToBeCopied in _copiedItems)
		{
			string name = GetNewItemName(itemToBeCopied.Name);

			if (name.Length > 120)
			{
				valid = false;
				break;
			}
		}

		return valid;
	}

	private void Paste()
	{
		if (!ValidNameLength())
		{
			string text = "Could not paste Columns since one or more of the Column Name lengths is more than the allowed 120 characters.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("couldNotPaste");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			return;
		}

		int insertNewRowAt;

		if (_cutActivated)
		{
			string firstNameOfSelectedRows = FirstNameOfSelectedRows();
			insertNewRowAt = GetIndexOfRowFromName(firstNameOfSelectedRows);

			int totalRows = columnsDataGridView.Rows.Count;
			int spaceLeft = totalRows - insertNewRowAt;

			if (spaceLeft < _copiedItems.Count)
			{
				insertNewRowAt = totalRows - _copiedItems.Count;
			}

			if (insertNewRowAt < 0)
			{
				insertNewRowAt = 0;
			}
		}
		else
		{
			string lastNameOfSelectedRows = LastNameOfSelectedRows();
			insertNewRowAt = GetIndexOfRowFromName(lastNameOfSelectedRows) + 1;
		}

		int insertNewRowAtOriginal = insertNewRowAt;

		if (_cutActivated)
		{
			DoDelete(_copiedItems);
		}

		List<string> nameList = new List<string>();

		foreach (Column itemToBeCopied in _copiedItems)
		{
			string name = GetNewItemName(itemToBeCopied.Name);

			Column newItem = new Column(name, itemToBeCopied.IsolationLevel, itemToBeCopied.Input, itemToBeCopied.InputType, itemToBeCopied.Output, itemToBeCopied.OutputType, itemToBeCopied.Hidden, itemToBeCopied.Enabled, itemToBeCopied.Width);
			CreateNewItem(newItem, insertNewRowAt);
			insertNewRowAt++;

			nameList.Add(name);
		}

		foreach (DataGridViewRow row in columnsDataGridView.Rows)
		{
			row.Selected = false;
		}

		columnsDataGridView.CurrentCell = columnsDataGridView["ItemName", insertNewRowAtOriginal];
		SelectRows(nameList);
		FillNumberColumn();
		PopulateSearchList();

		SetChangesMade(true);
		columnsDataGridView.Focus();
	}

	private void SelectRows(List<string> nameList)
	{
		foreach (DataGridViewRow row in columnsDataGridView.Rows)
		{
			foreach (string name in nameList)
			{
				if (row.Cells["ItemName"].Value.ToString() == name)
				{
					row.Selected = true;
				}
			}
		}
	}

	private static string GetNewItemName(string name)
	{
		bool uniqueName;

		do
		{
			uniqueName = true;

			foreach (Column item in ColumnHelper.ColumnCollection.Columns)
			{
				if (item.Name == name)
				{
					uniqueName = false;
					break;
				}
			}

			if (!uniqueName)
			{
				if (ConfigHandler.UseTranslation)
				{
					name = string.Format("{0} ({1})", name, Translator.GetText("copy1"));
				}
				else
				{
					name = string.Format("{0} (copy)", name);
				}
			}
		} while (!uniqueName);

		return name;
	}

	private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Create();
	}

	private void EditToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void MoveUpToolStripMenuItem_Click(object sender, EventArgs e)
	{
		MoveUp();
	}

	private void MoveDownToolStripMenuItem_Click(object sender, EventArgs e)
	{
		MoveDown();
	}

	private void CreateMenuItem_Click(object sender, EventArgs e)
	{
		Create();
	}

	private void EditMenuItem_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void DeleteMenuItem_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void MoveUpMenuItem_Click(object sender, EventArgs e)
	{
		MoveUp();
	}

	private void MoveDownMenuItem_Click(object sender, EventArgs e)
	{
		MoveDown();
	}

	private void ContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
	{
		HandleToolStripOpening();
	}

	private void ActionToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
	{
		HandleToolStripOpening();
	}

	private void HandleToolStripOpening()
	{
		if (_copiedItems.Count > 0)
		{
			pasteMenuItem.Enabled = true;
			pasteMenuItem1.Enabled = true;
		}
		else
		{
			pasteMenuItem.Enabled = false;
			pasteMenuItem1.Enabled = false;
		}
	}

	private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Copy();
	}

	private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Paste();
	}

	private void CopyToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		Copy();
	}

	private void PasteToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		Paste();
	}

	private void CutToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		Cut();
	}

	private void CutToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Cut();
	}

	private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		columnsDataGridView.SelectAll();
	}

	private void SelectAllToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		columnsDataGridView.SelectAll();
	}

	private void SetSize()
	{
		int x = Convert.ToInt32(ConfigHandler.ColumnsWindowSize.Split(';')[0]);
		int y = Convert.ToInt32(ConfigHandler.ColumnsWindowSize.Split(';')[1]);

		if (x > Screen.PrimaryScreen.Bounds.Width || y > Screen.PrimaryScreen.Bounds.Height)
		{
			WindowState = FormWindowState.Maximized;
			return;
		}

		if (x >= MinimumSize.Width && y >= MinimumSize.Height)
		{
			Size = new Size(x, y);
		}
	}

	private void ManageColumnsForm_Resize(object sender, EventArgs e)
	{
		if (Width < MinimumSize.Width || Width == 0)
		{
			return;
		}

		ConfigHandler.ColumnsWindowSize = string.Format("{0}; {1}", Size.Width, Size.Height);
		ConfigHandler.SaveConfig();

		ItemName.Width = columnsDataGridView.Width - (columnsDataGridView.Columns[0].Width + columnsDataGridView.Columns[1].Width + columnsDataGridView.Columns[3].Width + columnsDataGridView.Columns[4].Width + columnsDataGridView.Columns[5].Width + 20);
	}

	private void ManageColumnsForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		bool success = SaveChanges();

		if (!success)
		{
			e.Cancel = true;
		}
		else
		{
			_searchForm.Close();
			e.Cancel = false;
		}
	}

	private void SearchToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		Search();
	}

	private void SearchToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Search();
	}

	private void Search()
	{
		if (_searchForm.IsShown())
		{
			_searchForm.Activate();
		}
		else
		{
			_searchForm.Hide();
			_searchForm.Show(this);
		}
	}

	private void HiddenMenuItem_Click(object sender, EventArgs e)
	{
		SetHidden(true);
	}

	private void ShownMenuItem_Click(object sender, EventArgs e)
	{
		SetHidden(false);
	}

	private void ActiveMenuItem_Click(object sender, EventArgs e)
	{
		SetActive(true);
	}

	private void InactiveMenuItem_Click(object sender, EventArgs e)
	{
		SetActive(false);
	}

	private void HiddenMenuItem1_Click(object sender, EventArgs e)
	{
		SetHidden(true);
	}

	private void ShownMenuItem1_Click(object sender, EventArgs e)
	{
		SetHidden(false);
	}

	private void ActiveMenuItem1_Click(object sender, EventArgs e)
	{
		SetActive(true);
	}

	private void InactiveMenuItem1_Click(object sender, EventArgs e)
	{
		SetActive(false);
	}

	private void SetIsolationLevel(Column.IsolationLevelType isolationLevel)
	{
		foreach (DataGridViewRow row in columnsDataGridView.SelectedRows)
		{
			foreach (Column item in ColumnHelper.ColumnCollection.Columns)
			{
				if (row.Cells["ItemName"].Value.ToString() == item.Name)
				{
					item.IsolationLevel = isolationLevel;
				}
			}
		}

		SetChangesMade(true);
		columnsDataGridView.Focus();
	}

	private void SetHidden(bool value)
	{
		foreach (DataGridViewRow row in columnsDataGridView.SelectedRows)
		{
			foreach (Column item in ColumnHelper.ColumnCollection.Columns)
			{
				if (row.Cells["ItemName"].Value.ToString() == item.Name)
				{
					if (value)
					{
						row.DefaultCellStyle.ForeColor = Color.Gray;
						row.DefaultCellStyle.SelectionForeColor = Color.Gray;
					}
					else
					{
						row.DefaultCellStyle.ForeColor = SystemColors.WindowText;
						row.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
					}

					item.Hidden = value;
					row.Cells["HiddenColumn"].Value = value;
				}
			}
		}

		SetChangesMade(true);
		columnsDataGridView.Focus();
	}

	private void SetActive(bool value)
	{
		foreach (DataGridViewRow row in columnsDataGridView.SelectedRows)
		{
			foreach (Column item in ColumnHelper.ColumnCollection.Columns)
			{
				if (row.Cells["ItemName"].Value.ToString() == item.Name)
				{
					if (value)
					{
						row.DefaultCellStyle.Font = new Font(Font, FontStyle.Regular);
					}
					else
					{
						row.DefaultCellStyle.Font = new Font(Font, FontStyle.Strikeout);
					}

					item.Enabled = value;
				}
			}
		}

		SetChangesMade(true);
		columnsDataGridView.Focus();
	}

	private void ToggleHiddenMenuItem_Click(object sender, EventArgs e)
	{
		ToggleHidden();
	}

	private void ToggleActiveMenuItem_Click(object sender, EventArgs e)
	{
		ToggleActive();
	}

	private void ToggleHiddenMenuItem1_Click(object sender, EventArgs e)
	{
		ToggleHidden();
	}

	private void ToggleActiveMenuItem1_Click(object sender, EventArgs e)
	{
		ToggleActive();
	}

	private void ToggleHidden()
	{
		foreach (DataGridViewRow row in columnsDataGridView.SelectedRows)
		{
			foreach (Column item in ColumnHelper.ColumnCollection.Columns)
			{
				if (row.Cells["ItemName"].Value.ToString() == item.Name)
				{
					if (item.Hidden)
					{
						item.Hidden = false;

						row.DefaultCellStyle.ForeColor = SystemColors.WindowText;
						row.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
						row.Cells["HiddenColumn"].Value = false;
					}
					else
					{
						item.Hidden = true;

						row.DefaultCellStyle.ForeColor = Color.Gray;
						row.DefaultCellStyle.SelectionForeColor = Color.Gray;
						row.Cells["HiddenColumn"].Value = true;
					}
				}
			}
		}

		SetChangesMade(true);
		columnsDataGridView.Focus();
	}

	private void ToggleActive()
	{
		foreach (DataGridViewRow row in columnsDataGridView.SelectedRows)
		{
			foreach (Column item in ColumnHelper.ColumnCollection.Columns)
			{
				if (row.Cells["ItemName"].Value.ToString() == item.Name)
				{
					if (item.Enabled)
					{
						item.Enabled = false;

						row.DefaultCellStyle.Font = new Font(Font, FontStyle.Strikeout);
					}
					else
					{
						item.Enabled = true;

						row.DefaultCellStyle.Font = new Font(Font, FontStyle.Regular);
					}
				}
			}
		}

		SetChangesMade(true);
		columnsDataGridView.Focus();
	}

	private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SaveCollection();
	}

	private void ParametersDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		SaveAfterChangeParameter();
	}

	private void ParametersDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		SaveAfterChangeParameter();
	}

	private void ParametersDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
		SaveAfterChangeParameter();
	}

	private void AddParameterGridEvents()
	{
		parametersDataGridView.CellValueChanged += ParametersDataGridView_CellValueChanged;
		parametersDataGridView.RowsAdded += ParametersDataGridView_RowsAdded;
		parametersDataGridView.RowsRemoved += ParametersDataGridView_RowsRemoved;
	}

	private void RemoveParameterGridEvents()
	{
		parametersDataGridView.CellValueChanged -= ParametersDataGridView_CellValueChanged;
		parametersDataGridView.RowsAdded -= ParametersDataGridView_RowsAdded;
		parametersDataGridView.RowsRemoved -= ParametersDataGridView_RowsRemoved;
	}

	private void DeleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string text = "Delete selected Parameters?";

		if (ConfigHandler.UseTranslation)
		{
			text = Translator.GetText("deleteParameters");
		}

		DialogResult result = OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

		if (result.ToString() == "Yes")
		{
			List<DataGridViewRow> rowCollection = new List<DataGridViewRow>();

			if (_parametersDataGridViewMouseRowHit >= 0)
			{
				rowCollection.Add(parametersDataGridView.Rows[_parametersDataGridViewMouseRowHit]);
			}

			foreach (DataGridViewCell cell in parametersDataGridView.SelectedCells)
			{
				DataGridViewRow row = cell.OwningRow;

				if (!row.IsNewRow && !rowCollection.Contains(row) && row.Selected)
				{
					rowCollection.Add(row);
				}
			}

			foreach (DataGridViewRow row in rowCollection)
			{
				if (!row.IsNewRow)
				{
					parametersDataGridView.Rows.Remove(row);
				}
			}
		}
	}

	private void NewSessionToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DeleteAll();
	}

	private void DeleteAll()
	{
		string text = "Create new Column Set?";

		if (ConfigHandler.UseTranslation)
		{
			text = Translator.GetText("CreateNewSession");
		}

		DialogResult overwrite = OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

		if (overwrite.ToString() == "Yes")
		{
			bool success = SaveChanges();

			if (success)
			{
				ColumnHelper.ColumnCollection.Columns.Clear();
				ColumnHelper.ColumnCollection.Parameters.Clear();
				ColumnHelper.ColumnCollection.Options.Clear();

				parametersDataGridView.Rows.Clear();

				UnloadFileName();
				SetDefaultColumnName("");

				SetChangesMade(false);
				FillList();
			}
		}
	}

	private void UnloadFileName()
	{
		ColumnHelper.ColumnCollectionFileName = null;
		SetTitle();
		SetCheckForUpdatesEnabled();
	}

	private static void SetDefaultColumnName(string name)
	{
		if (name != "")
		{
			ConfigHandler.DefaultColumnName = name;
		}
		else
		{
			ConfigHandler.DefaultColumnName = null;
		}
	}

	private void LoadTextDataCleanedCustomColumnSet()
	{
		bool success = SaveChanges();

		if (success)
		{
			Import(SQLEventAnalyzer.Properties.Resources.TextDataCleaned);
			UnloadFileName();
			SetDefaultColumnName("TextDataCleaned");
		}
	}

	private void FillCustomColumnsDropDown()
	{
		string textDataCleanedText = "TextData Cleaned";

		if (ConfigHandler.UseTranslation)
		{
			textDataCleanedText = Translator.GetText("TextDataCleaned");
		}

		loadColumnSetToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(textDataCleanedText, null, delegate
		{
			LoadTextDataCleanedCustomColumnSet();
		}));
	}

	private void ReadUncommittedToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string menuItem = ((ToolStripMenuItem)sender).Text;
		SetIsolationLevel(ColumnHelper.StringToIsolationLevelType(menuItem));
	}

	private void ReadCommittedToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string menuItem = ((ToolStripMenuItem)sender).Text;
		SetIsolationLevel(ColumnHelper.StringToIsolationLevelType(menuItem));
	}

	private void RepeatableReadToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string menuItem = ((ToolStripMenuItem)sender).Text;
		SetIsolationLevel(ColumnHelper.StringToIsolationLevelType(menuItem));
	}

	private void SerializableToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string menuItem = ((ToolStripMenuItem)sender).Text;
		SetIsolationLevel(ColumnHelper.StringToIsolationLevelType(menuItem));
	}

	private void ReadUncommittedToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		string menuItem = ((ToolStripMenuItem)sender).Text;
		SetIsolationLevel(ColumnHelper.StringToIsolationLevelType(menuItem));
	}

	private void ReadCommittedToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		string menuItem = ((ToolStripMenuItem)sender).Text;
		SetIsolationLevel(ColumnHelper.StringToIsolationLevelType(menuItem));
	}

	private void RepeatableReadToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		string menuItem = ((ToolStripMenuItem)sender).Text;
		SetIsolationLevel(ColumnHelper.StringToIsolationLevelType(menuItem));
	}

	private void SerializableToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		string menuItem = ((ToolStripMenuItem)sender).Text;
		SetIsolationLevel(ColumnHelper.StringToIsolationLevelType(menuItem));
	}

	private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		OnlineSettingsForm form = new OnlineSettingsForm();
		form.Initialize();

		form.ShowDialog();

		if (form.ChangesMade)
		{
			SetChangesMade(true);
			SetCheckForUpdatesEnabled();
		}
	}

	private void CheckForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		CustomColumnsCheckForUpdatesForm form = new CustomColumnsCheckForUpdatesForm(ColumnHelper.ColumnCollectionFileName);
		form.ShowDialog();

		if (form.DoPerformUpdate)
		{
			DoUpdateCustomColumns();
		}
	}

	private void SetCheckForUpdatesEnabled()
	{
		if (ColumnHelper.ColumnCollectionFileName == null)
		{
			checkForUpdatesToolStripMenuItem.Enabled = false;
			return;
		}

		checkForUpdatesToolStripMenuItem.Enabled = ColumnHelper.GetAutomaticUpdateEnabled();
	}

	private void CheckForCustomColumnsUpdate(string fileName)
	{
		if (ColumnHelper.GetAutomaticUpdateEnabled())
		{
			_customColumnsCheckForUpdatesForm = new CustomColumnsCheckForUpdatesForm(fileName);
			_customColumnsCheckForUpdatesForm.UpdateCheckCompleteEvent += CustomColumnsUpdateCheckCompleteEvent;
			_customColumnsCheckForUpdatesForm.CheckForUpdates();
		}
	}

	private void CustomColumnsUpdateCheckCompleteEvent(string errorMessage, Version version, bool anyUpdates, bool anyErrors)
	{
		if (!anyErrors && anyUpdates)
		{
			DoUpdateCustomColumns();
		}
	}

	private void DoUpdateCustomColumns()
	{
		GenericHelper.CustomColumnsUpdatePending = true;
		SetRestartToUpdate();
	}

	private void SetRestartToUpdate()
	{
		if (GenericHelper.CustomColumnsUpdatePending)
		{
			if (ConfigHandler.UseTranslation)
			{
				restartToUpdateTextBox.Text = Translator.GetText("restartToUpdateLabel");
			}

			restartToUpdateTextBox.Visible = true;
		}
	}

	private void RestartToUpdateTextBox_MouseDown(object sender, MouseEventArgs e)
	{
		GenericHelper.HideCaret(restartToUpdateTextBox);
	}

	private void ParametersDataGridView_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right)
		{
			DataGridView.HitTestInfo hit = parametersDataGridView.HitTest(e.X, e.Y);

			if (hit.Type == DataGridViewHitTestType.Cell || hit.Type == DataGridViewHitTestType.RowHeader)
			{
				_parametersDataGridViewMouseRowHit = hit.RowIndex;
			}
			else
			{
				_parametersDataGridViewMouseRowHit = -1;
			}
		}
	}
}
