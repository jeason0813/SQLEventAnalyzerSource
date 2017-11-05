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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public partial class DataViewer : UserControl
{
	public delegate void UpdateDataSourceEventHandler(DataViewer dataViewer, string sqlFile, int page, int itemsPerPage, string sortingColumn, ListSortDirection sortingColumnDirection, string searchTerm, string searchColumn, Dictionary<string, string[]> visibleColumns, string where, string whereActive);
	public event UpdateDataSourceEventHandler UpdateDataSourceEvent;

	public delegate void RequestDataSourceAllPagesEventHandler(DataViewer dataViewer, string sqlFile, int page, int itemsPerPage, string sortingColumn, ListSortDirection sortingColumnDirection, string searchTerm, string searchColumn, Dictionary<string, string[]> visibleColumns, string where, string whereActive);
	public event RequestDataSourceAllPagesEventHandler RequestDataSourceAllPagesEvent;

	public delegate void GetRowEventHandler(DataRow selectedRow);
	public event GetRowEventHandler GetRowEvent;

	public delegate void RightClickEventHandler();
	public event RightClickEventHandler RightClickEvent;

	public delegate void DoubleClickEventHandler();
	public event DoubleClickEventHandler DoubleClickEvent;

	public delegate void DeleteEventHandler();
	public event DeleteEventHandler DeleteEvent;

	public delegate void SelectionChangedEventHandler(int numberOfSelectedRows);
	public event SelectionChangedEventHandler SelectionChangedEvent;

	public delegate void ColumnWidthChangedEventHandler(string columnName, int width);
	public event ColumnWidthChangedEventHandler ColumnWidthChangedEvent;

	public delegate void ColumnDisplayIndexChangedEventHandler(string columnName, int displayIndex);
	public event ColumnDisplayIndexChangedEventHandler ColumnDisplayIndexChangedEvent;

	public delegate void ShowOutputEventHandler(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
	public event ShowOutputEventHandler ShowOutputEvent;

	[DllImport("user32")]
	private static extern bool HideCaret(IntPtr hWnd);

	private int _page;
	private int _itemsPerPage;
	private int _previousSelectedRowIndex;
	private int _numberOfSortingColumns;
	private string _sortingColumn;
	private string _searchTerm;
	private string _searchColumn;
	private string _sqlFile;
	private string _whereActive;
	private string _isDataModifiedText = "Data has been modified since last retrieval. Data will be reloaded.";
	private string _isDataModifiedCaptionText = "Data modified";
	private string _outOfText = "out of";
	private string _totalText = "Total";
	private string _searchBoxToolTipText = "Use * as wildcard";
	private string _notValidIntegerText = "is not a valid number";
	private string _columnDragName = "";
	private string _dateFormatShow = "";
	private string _dateFormatSearch = "";
	private bool _useDateSelector;
	private ListSortDirection _sortingColumnDirection;
	private Dictionary<string, string[]> _visibleColumns;
	private bool _readyForInput = true;
	private bool _isInitialized;
	private bool _keyDown;
	private bool _fireColumnWidthChangedEvent = true;
	private bool _fireSelectionChangedEvent = true;
	private readonly ToolTip _toolTip = new ToolTip();
	private DataSet _dataSource;

	private DataViewerHandler _dataViewerHandler;

	public DataViewer()
	{
		InitializeComponent();
		totalRowsTextBox.GotFocus += TotalRowsTextBox_GotFocus;
	}

	public void SetDateFormatShow(string format)
	{
		_dateFormatShow = format;
	}

	public void SetDateFormatSearch(string format)
	{
		_dateFormatSearch = format;
	}

	public void SetUseDateSelector(bool value)
	{
		_useDateSelector = value;
	}

	public void ReInitializeDataGrid()
	{
		_dataViewerHandler.ReInitializeDataGrid();
	}

	public void UpdateColumnOrder(Dictionary<string, string[]> visibleColumns)
	{
		_dataViewerHandler.UpdateColumnOrder(visibleColumns);
	}

	public void SetFireColumnWidthChangedEvent(bool value)
	{
		_fireColumnWidthChangedEvent = value;
	}

	public void UpdateColumnWidthAndOrder(Dictionary<string, string[]> visibleColumns)
	{
		_fireColumnWidthChangedEvent = false;
		_dataViewerHandler.UpdateColumnWidth(visibleColumns);
		_dataViewerHandler.UpdateColumnOrder(visibleColumns);
		_fireColumnWidthChangedEvent = true;
	}

	public void UpdateColumnWidth(Dictionary<string, string[]> visibleColumns)
	{
		_fireColumnWidthChangedEvent = false;
		_dataViewerHandler.UpdateColumnWidth(visibleColumns);
		_fireColumnWidthChangedEvent = true;
	}

	public void DisableTopBar()
	{
		searchPictureBox.Visible = false;
		searchComboBox.Visible = false;
		searchTermTextBox.Visible = false;
		clearSearchButton.Visible = false;
		searchButton.Visible = false;
		dataGridView.Location = new Point(dataGridView.Location.X, dataGridView.Location.Y - 28);
		dataGridView.Height = dataGridView.Height + 28;
	}

	public void Search()
	{
		_dataViewerHandler.Search();
	}

	public List<string[]> GetSortMemory()
	{
		return _dataViewerHandler.GetSortMemory();
	}

	public void SetTotalTextBoxBackgroundColor(Color color)
	{
		totalRowsTextBox.BackColor = color;
	}

	public void SetMultiSelect(bool value)
	{
		if (_dataViewerHandler != null)
		{
			_dataViewerHandler.SetMultiSelect(value);
		}
	}

	public void SetFocus()
	{
		if (_dataViewerHandler != null)
		{
			_dataViewerHandler.SetFocus();
		}
	}

	public void RequestDataSourceAllPages()
	{
		_dataViewerHandler.RequestDataSourceAllPages();
	}

	public DataTable GetVisibleDataTable()
	{
		return _dataViewerHandler.GetVisibleDataTable();
	}

	public string GetSortingColumn()
	{
		if (_dataViewerHandler == null)
		{
			return null;
		}

		return _dataViewerHandler.GetSortingColumn();
	}

	public int GetPage()
	{
		if (_dataViewerHandler == null)
		{
			return -1;
		}

		return _dataViewerHandler.GetPage();
	}

	public string GetSearchTerm()
	{
		if (_dataViewerHandler == null)
		{
			return null;
		}

		return _dataViewerHandler.GetSearchTerm();
	}

	public string GetSearchColumn()
	{
		if (_dataViewerHandler == null)
		{
			return null;
		}

		return _dataViewerHandler.GetSearchColumn();
	}

	public string GetWhere()
	{
		if (_dataViewerHandler == null)
		{
			return null;
		}

		return _dataViewerHandler.GetWhere();
	}

	public string GetWhereActive()
	{
		if (_dataViewerHandler == null)
		{
			return null;
		}

		return _dataViewerHandler.GetWhereActive();
	}

	public ListSortDirection GetSortingColumnDirection()
	{
		return _dataViewerHandler.GetSortingColumnDirection();
	}

	public void SetFireSelectionChangedEvent(bool value)
	{
		_fireSelectionChangedEvent = value;
	}

	public void SetEventsEnabled(bool value)
	{
		if (_dataViewerHandler != null)
		{
			_dataViewerHandler.SetEventsEnabled(value);
		}
	}

	public void LoadData(DataViewerParameters dataViewerParameters)
	{
		DoLoadData(dataViewerParameters, null, 1);
	}

	public void LoadData(DataViewerParameters dataViewerParameters, DataRow selectedRow)
	{
		DoLoadData(dataViewerParameters, selectedRow, 1);
	}

	public void LoadData(DataViewerParameters dataViewerParameters, DataRow selectedRow, int selectedIndex)
	{
		DoLoadData(dataViewerParameters, selectedRow, selectedIndex);
	}

	public void LoadData(DataViewerParameters dataViewerParameters, int selectedIndex)
	{
		DoLoadData(dataViewerParameters, null, selectedIndex);
	}

	public void DisableSearch()
	{
		if (_dataViewerHandler != null)
		{
			_dataViewerHandler.DisableSearch();
		}
	}

	public List<DataRow> GetSelectedRows()
	{
		if (_dataViewerHandler != null)
		{
			return _dataViewerHandler.GetSelectedRows();
		}
		else
		{
			return null;
		}
	}

	public DataRow GetSelectedRow()
	{
		if (_dataViewerHandler != null)
		{
			return _dataViewerHandler.GetSelectedRow();
		}
		else
		{
			return null;
		}
	}

	public int GetNumberOfRows()
	{
		if (_dataViewerHandler != null)
		{
			return _dataViewerHandler.GetNumberOfRows();
		}
		else
		{
			return -1;
		}
	}

	public int GetNumberOfSelectedRows()
	{
		if (_dataViewerHandler != null)
		{
			return _dataViewerHandler.GetNumberOfSelectedRows();
		}
		else
		{
			return -1;
		}
	}

	public void Reset()
	{
		if (_dataViewerHandler != null)
		{
			_dataViewerHandler.Reset();
		}
	}

	public void SetDataSource(int page, string sortingColumn, ListSortDirection sortingColumnDirection, DataSet dataSource, string searchTerm, string searchColumn, string where, string whereActive)
	{
		if (dataSource == null || dataSource.Tables.Count == 0) // Connection lost
		{
			return;
		}

		if (dataSource.Tables[0].Rows.Count == 0)
		{
			page = 0;
		}

		_dataSource = dataSource;

		_dataViewerHandler.SetDataSource(page, sortingColumn, sortingColumnDirection, dataSource, searchTerm, searchColumn, _visibleColumns, where, whereActive);
	}

	public DataSet GetDataSource()
	{
		return _dataSource;
	}

	public void UpdateGrid()
	{
		if (_dataViewerHandler != null)
		{
			_dataViewerHandler.UpdateGrid();
		}
	}

	public void UpdateGrid(int page, int index)
	{
		if (_dataViewerHandler != null)
		{
			_dataViewerHandler.UpdateGrid(page, index);
		}
	}

	public void UpdateGrid(string searchTerm, string searchColumn)
	{
		if (_dataViewerHandler != null)
		{
			_dataViewerHandler.UpdateGrid(searchTerm, searchColumn);
		}
	}

	public void UpdateGrid(string where)
	{
		if (_dataViewerHandler != null)
		{
			_dataViewerHandler.UpdateGrid(where);
		}
	}

	public void SetItemsPerPage(int itemsPerPage)
	{
		if (_dataViewerHandler != null)
		{
			_itemsPerPage = itemsPerPage;
			_dataViewerHandler.SetItemsPerPage(itemsPerPage);
		}
	}

	public void SetReadyForInput(bool value)
	{
		_readyForInput = value;
	}

	public void SetText(string clearButtonText, string searchButtonText, string pageText, string isDataModifiedText, string isDataModifiedCaptionText, string outOfText, string totalText, string searchBoxToolTipText, string notValidIntegerText)
	{
		clearSearchButton.Text = clearButtonText;
		searchButton.Text = searchButtonText;
		totalPagesLabel.Text = outOfText;
		pageLabel.Text = pageText;
		_searchBoxToolTipText = searchBoxToolTipText;
		_isDataModifiedText = isDataModifiedText;
		_isDataModifiedCaptionText = isDataModifiedCaptionText;
		_outOfText = outOfText;
		_totalText = totalText;
		_notValidIntegerText = notValidIntegerText;
	}

	public void SetInitialized(bool value)
	{
		_isInitialized = value;
	}

	public bool IsInitialized()
	{
		return _isInitialized;
	}

	public void UpdateVisibleColumns(Dictionary<string, string[]> visibleColumns)
	{
		_visibleColumns = visibleColumns;
		_dataViewerHandler.UpdateSearchColumns(visibleColumns);
	}

	private void DoLoadData(DataViewerParameters dataViewerParameters, DataRow selectedRow, int selectedIndex)
	{
		_sqlFile = dataViewerParameters.SqlFile;
		_page = dataViewerParameters.Page;
		_itemsPerPage = dataViewerParameters.ItemsPerPage;
		_sortingColumn = dataViewerParameters.SortingColumn;
		_sortingColumnDirection = dataViewerParameters.SortingColumnDirection;
		_searchTerm = dataViewerParameters.SearchTerm;
		_searchColumn = dataViewerParameters.SearchColumn;
		_visibleColumns = dataViewerParameters.Columns;
		_whereActive = dataViewerParameters.WhereActive;
		_numberOfSortingColumns = dataViewerParameters.NumberOfSortingColumns;
		Dictionary<string, string> iconDictionary = dataViewerParameters.IconDictionary;
		Dictionary<string, Icon> iconList = dataViewerParameters.IconList;

		_dataViewerHandler = new DataViewerHandler(dataGridView, GetPagerPanel(), _sortingColumn, _sortingColumnDirection, GetSearcherPanel(), _visibleColumns, _itemsPerPage, _isDataModifiedText, _isDataModifiedCaptionText, _outOfText, _totalText, _numberOfSortingColumns, iconDictionary, iconList, dataViewerParameters.FirstSortColumnDirection);
		_dataViewerHandler.RequestDataSourceUpdateEvent += DataViewerHandler_RequestDataSourceUpdateEvent;
		_dataViewerHandler.RequestDataSourceAllPagesEvent += DataViewerHandler_RequestDataSourceAllPagesEvent;
		_dataViewerHandler.ShowOutputEvent += DataViewerHandler_ShowOutputEvent;

		string where = "";

		if (selectedRow != null)
		{
			if (selectedRow[dataViewerParameters.WhereSingleColumn] == DBNull.Value)
			{
				where = string.Format(dataViewerParameters.WhereSingle, "null");
			}
			else
			{
				where = string.Format(dataViewerParameters.WhereSingle, selectedRow[dataViewerParameters.WhereSingleColumn].ToString().Replace("'", "''"));
			}
		}

		_previousSelectedRowIndex = -1;
		_dataViewerHandler.SetSelectedRow(selectedIndex);

		FireUpdateDataSourceEvent(_page, _itemsPerPage, _sortingColumn, _sortingColumnDirection, _searchTerm, _searchColumn, where, _whereActive);

		dataGridView.MouseClick += DataGridView_MouseClick;
		dataGridView.SelectionChanged += DataGridView_SelectionChanged;
		dataGridView.CellMouseDoubleClick += DataGridView_CellMouseDoubleClick;
		dataGridView.KeyDown += DataGridView_KeyDown;
		dataGridView.KeyUp += DataGridView_KeyUp;
		dataGridView.ColumnWidthChanged += DataGridView_ColumnWidthChanged;
		dataGridView.MouseDown += DataGridView_MouseDown;
		dataGridView.CellFormatting += DataGridView_CellFormatting;

		InitializeToolTip(searchTermTextBox, _searchBoxToolTipText);

		_isInitialized = true;
	}

	private void DataViewerHandler_ShowOutputEvent(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
	{
		if (ShowOutputEvent != null)
		{
			ShowOutputEvent(text, caption, buttons, icon);
		}
	}

	private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
	{
		foreach (KeyValuePair<string, string[]> column in _visibleColumns)
		{
			if (column.Key == dataGridView.Columns[e.ColumnIndex].Name && column.Value[2] == "DateTime")
			{
				FormatDate(e);
			}
			else if (column.Value[2] == "Text" || column.Value[2] == "FullText")
			{
				if (e.Value.ToString().Length > 1000) // Bug in DataGridView (can maximum show 43679 characters)
				{
					e.Value = e.Value.ToString().Substring(0, 1000);
				}
			}
		}
	}

	private void FormatDate(DataGridViewCellFormattingEventArgs formatting)
	{
		if (formatting.Value != null && formatting.Value.ToString() != "" && _dateFormatShow != "")
		{
			DateTime dateTime = Convert.ToDateTime(formatting.Value);
			string dateString = dateTime.ToString(_dateFormatShow);
			formatting.Value = dateString;
			formatting.FormattingApplied = true;
		}
	}

	private void DataGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
	{
		if (_fireColumnWidthChangedEvent)
		{
			string name = dataGridView.Columns[e.Column.Index].Name;
			FireColumnWidthChangedEvent(name, e.Column.Width);
		}
	}

	private void DataGridView_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyData == Keys.Down || e.KeyData == Keys.Up)
		{
			if (dataGridView.SelectedRows.Count == 1)
			{
				int rowIndex = dataGridView.SelectedCells[0].RowIndex;

				if (e.KeyData == Keys.Up)
				{
					if (rowIndex == 0 && (_previousSelectedRowIndex == rowIndex || _previousSelectedRowIndex == -1))
					{
						dataGridView.ClearSelection();
						dataGridView.CurrentCell = dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[GetFirstVisibleCell(dataGridView.Rows.Count - 1)];
						dataGridView.Rows[dataGridView.Rows.Count - 1].Selected = true;
					}
				}
				else if (e.KeyData == Keys.Down)
				{
					if (rowIndex == dataGridView.Rows.Count - 1 && _previousSelectedRowIndex == rowIndex)
					{
						dataGridView.ClearSelection();
						dataGridView.CurrentCell = dataGridView.Rows[0].Cells[GetFirstVisibleCell(0)];
						dataGridView.Rows[0].Selected = true;
					}
				}
			}

			_keyDown = false;
			SelectionChanged();
		}
	}

	private void DataGridView_SelectionChanged(object sender, EventArgs e)
	{
		if (!_fireSelectionChangedEvent)
		{
			return;
		}

		if (_keyDown)
		{
			return;
		}

		SelectionChanged();
	}

	private void SelectionChanged()
	{
		if (dataGridView.SelectedRows.Count == 1)
		{
			int rowIndex = dataGridView.SelectedCells[0].RowIndex;

			if (rowIndex >= 0 && rowIndex != _previousSelectedRowIndex)
			{
				_previousSelectedRowIndex = rowIndex;
				FireGetRowEvent(_dataViewerHandler.GetSelectedRow());
			}
		}
		else if (dataGridView.SelectedRows.Count == 0)
		{
			if (dataGridView.Rows.Count > 0)
			{
				if (_previousSelectedRowIndex == -1)
				{
					dataGridView.Rows[0].Selected = true;
				}
				else
				{
					dataGridView.Rows[_previousSelectedRowIndex].Selected = true;
				}
			}
		}

		FireSelectionChangedEvent(dataGridView.SelectedRows.Count);
	}

	private static int GetDropColumnCorrection(int cursorPositionX, int colStart, int columnWidth)
	{
		int columnMiddle = columnWidth / 2;

		if (cursorPositionX <= colStart + columnMiddle)
		{
			return -1;
		}

		return 0;
	}

	private bool GetShouldMove(int cursorPositionX, int colStart, int columnWidth, int columnIndex)
	{
		int columnMiddle = columnWidth / 2;

		DataGridViewColumn nextColumn = dataGridView.Columns.GetNextColumn(dataGridView.Columns[columnIndex], DataGridViewElementStates.Displayed, DataGridViewElementStates.None);
		DataGridViewColumn previousColumn = dataGridView.Columns.GetPreviousColumn(dataGridView.Columns[columnIndex], DataGridViewElementStates.Displayed, DataGridViewElementStates.None);

		if (nextColumn != null && nextColumn.Name == _columnDragName)   // Immediate left
		{
			if (cursorPositionX > colStart + columnMiddle)
			{
				return false;
			}
		}
		else if (previousColumn != null && previousColumn.Name == _columnDragName)  // Immediate right
		{
			if (cursorPositionX <= colStart + columnMiddle)
			{
				return false;
			}
		}

		return true;
	}

	private int GetDisplayIndex(string columnName)
	{
		int i = 0;

		foreach (KeyValuePair<string, string[]> column in _visibleColumns)
		{
			if (column.Key == columnName)
			{
				return i;
			}

			i++;
		}

		return -1;
	}

	private void DataGridView_MouseClick(object sender, MouseEventArgs e)
	{
		DataGridView.HitTestInfo info = dataGridView.HitTest(e.X, e.Y);

		if (e.Button == MouseButtons.Left && info.RowIndex == -1 && _columnDragName != "" && info.ColumnIndex != -1)
		{
			int columnIndex = info.ColumnIndex;
			string columnDropName = dataGridView.Columns[columnIndex].Name;

			if (_columnDragName != columnDropName)
			{
				bool shouldMove = GetShouldMove(e.X, info.ColumnX, dataGridView.Columns[columnIndex].Width, columnIndex);

				if (shouldMove)
				{
					int displayIndex = GetDisplayIndex(columnDropName);
					displayIndex = displayIndex + GetDropColumnCorrection(e.X, info.ColumnX, dataGridView.Columns[columnIndex].Width);
					FireColumnDisplayIndexChangedEvent(_columnDragName, displayIndex);
				}
			}
		}

		int rowIndex = info.RowIndex;

		if (rowIndex >= 0 && e.Button == MouseButtons.Right)
		{
			if (!dataGridView.Rows[rowIndex].Selected)
			{
				dataGridView.ClearSelection();
				dataGridView.CurrentCell = dataGridView.Rows[rowIndex].Cells[GetFirstVisibleCell(rowIndex)];
			}

			dataGridView.Rows[rowIndex].Selected = true;

			FireRightClickEvent();
		}
	}

	private void DataGridView_MouseDown(object sender, MouseEventArgs e)
	{
		DataGridView.HitTestInfo info = dataGridView.HitTest(e.X, e.Y);
		FieldInfo fieldInfo = info.GetType().GetField("typeInternal", BindingFlags.Instance | BindingFlags.NonPublic);
		string columnAction = fieldInfo.GetValue(info).ToString();

		if (e.Button == MouseButtons.Left && info.RowIndex == -1 && columnAction == "ColumnHeader")
		{
			int columnIndex = info.ColumnIndex;
			_columnDragName = dataGridView.Columns[columnIndex].Name;
		}
		else
		{
			_columnDragName = "";
		}
	}

	private int GetFirstVisibleCell(int rowIndex)
	{
		for (int i = 0; i < dataGridView.Rows[rowIndex].Cells.Count; i++)
		{
			if (dataGridView.Rows[rowIndex].Cells[i].Visible)
			{
				return i;
			}
		}

		return -1;
	}

	private void DataGridView_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyData == Keys.Down || e.KeyData == Keys.Up)
		{
			_keyDown = true;
		}

		if (GetNumberOfRows() > 0)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					FireDoubleClickEvent();
					e.SuppressKeyPress = true;
					break;
				case Keys.Delete:
					FireDeleteEvent();
					e.SuppressKeyPress = true;
					break;
			}
		}
	}

	private void DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (e.RowIndex >= 0 && e.Button == MouseButtons.Left)
		{
			FireDoubleClickEvent();
		}
	}

	private void DataViewerHandler_RequestDataSourceUpdateEvent(int page, string sortingColumn, ListSortDirection sortingColumnDirection, string searchTerm, string searchColumn, string where, string whereActive)
	{
		int horizontalScrollingOffset = dataGridView.HorizontalScrollingOffset;

		FireUpdateDataSourceEvent(page, _itemsPerPage, sortingColumn, sortingColumnDirection, searchTerm, searchColumn, where, whereActive);
		FireGetRowEvent(GetSelectedRow());
		_previousSelectedRowIndex = -1;

		if (horizontalScrollingOffset == 0 && dataGridView.Rows.Count > 0)
		{
			dataGridView.HorizontalScrollingOffset = 1;
			dataGridView.HorizontalScrollingOffset = 0;
		}
		else
		{
			dataGridView.HorizontalScrollingOffset = horizontalScrollingOffset;
		}
	}

	private void DataViewerHandler_RequestDataSourceAllPagesEvent(int page, string sortingColumn, ListSortDirection sortingColumnDirection, string searchTerm, string searchColumn, string where, string whereActive)
	{
		int itemsPerPage = _dataViewerHandler.GetTotalPages() * _itemsPerPage;
		FireRequestDataSourceAllPagesEvent(page, itemsPerPage, sortingColumn, sortingColumnDirection, searchTerm, searchColumn, where, whereActive);
	}

	private void FireUpdateDataSourceEvent(int page, int itemsPerPage, string sortingColumn, ListSortDirection sortingColumnDirection, string searchTerm, string searchColumn, string where, string whereActive)
	{
		if (UpdateDataSourceEvent != null)
		{
			UpdateDataSourceEvent(this, _sqlFile, page, itemsPerPage, sortingColumn, sortingColumnDirection, searchTerm, searchColumn, _visibleColumns, where, whereActive);
			SetClearButton();
		}
	}

	private void FireRequestDataSourceAllPagesEvent(int page, int itemsPerPage, string sortingColumn, ListSortDirection sortingColumnDirection, string searchTerm, string searchColumn, string where, string whereActive)
	{
		if (RequestDataSourceAllPagesEvent != null)
		{
			RequestDataSourceAllPagesEvent(this, _sqlFile, page, itemsPerPage, sortingColumn, sortingColumnDirection, searchTerm, searchColumn, _visibleColumns, where, whereActive);
		}
	}

	private void FireGetRowEvent(DataRow selectedRow)
	{
		if (GetRowEvent != null)
		{
			if (_readyForInput)
			{
				GetRowEvent(selectedRow);
			}
		}
	}

	private void FireColumnDisplayIndexChangedEvent(string columnName, int displayIndex)
	{
		if (ColumnDisplayIndexChangedEvent != null)
		{
			ColumnDisplayIndexChangedEvent(columnName, displayIndex);
		}
	}

	private void FireColumnWidthChangedEvent(string columnName, int width)
	{
		if (ColumnWidthChangedEvent != null)
		{
			ColumnWidthChangedEvent(columnName, width);
		}
	}

	private void FireSelectionChangedEvent(int numberOfSelectedRows)
	{
		if (SelectionChangedEvent != null)
		{
			SelectionChangedEvent(numberOfSelectedRows);
		}
	}

	private void FireRightClickEvent()
	{
		if (RightClickEvent != null)
		{
			if (_readyForInput)
			{
				RightClickEvent();
			}
		}
	}

	private void FireDoubleClickEvent()
	{
		if (DoubleClickEvent != null)
		{
			if (_readyForInput)
			{
				DoubleClickEvent();
			}
		}
	}

	private void FireDeleteEvent()
	{
		if (DeleteEvent != null)
		{
			if (_readyForInput)
			{
				DeleteEvent();
			}
		}
	}

	private PagerPanel GetPagerPanel()
	{
		PagerPanel PagerPanel = new PagerPanel();
		PagerPanel.FirstPageButton = firstPageButton;
		PagerPanel.LastPageButton = lastPageButton;
		PagerPanel.NextPageButton = nextPageButton;
		PagerPanel.PreviousPageButton = previousPageButton;
		PagerPanel.PageTextBox = pageTextBox;
		PagerPanel.TotalPagesLabel = totalPagesLabel;
		PagerPanel.TotalRowsTextBox = totalRowsTextBox;

		return PagerPanel;
	}

	private SearcherPanel GetSearcherPanel()
	{
		SearcherPanel SearcherPanel = new SearcherPanel();
		SearcherPanel.SearchComboBox = searchComboBox;
		SearcherPanel.SearchButton = searchButton;
		SearcherPanel.ClearSearchButton = clearSearchButton;
		SearcherPanel.SearchTermTextBox = searchTermTextBox;
		SearcherPanel.NotValidIntegerText = _notValidIntegerText;

		return SearcherPanel;
	}

	private void DataViewer_EnabledChanged(object sender, EventArgs e)
	{
		string tempTotalRowsText = totalRowsTextBox.Text;

		if (dataGridView.Enabled)
		{
			dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
			totalRowsTextBox.Text = "";
		}
		else
		{
			dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
			totalRowsTextBox.Text = "";
		}

		totalRowsTextBox.Text = tempTotalRowsText;
	}

	private void TotalRowsTextBox_GotFocus(object sender, EventArgs e)
	{
		totalRowsTextBox.SelectionStart = 0;
		totalRowsTextBox.SelectionLength = 0;
		HideCaret(totalRowsTextBox.Handle);
	}

	private void TotalRowsTextBox_Enter(object sender, EventArgs e)
	{
		totalRowsTextBox.SelectionStart = 0;
		totalRowsTextBox.SelectionLength = 0;
		HideCaret(totalRowsTextBox.Handle);
	}

	private void SearchComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (searchComboBox.SelectedItem != null)
		{
			KeyValuePair<string, string> selectedItem = (KeyValuePair<string, string>)searchComboBox.SelectedItem;

			bool isDate = false;
			bool isBoolean = false;

			if (_visibleColumns[selectedItem.Key].Length >= 3)
			{
				if (_visibleColumns[selectedItem.Key][2] == "DateTime" && _useDateSelector)
				{
					isDate = true;
				}
				else if (_visibleColumns[selectedItem.Key][2] == "Boolean")
				{
					isBoolean = true;
				}
			}

			Control existingValueControl = new Control();

			foreach (Control control in Controls)
			{
				if (control.Name == "searchTermTextBox")
				{
					existingValueControl = control;
					break;
				}
			}

			if (isDate && !(existingValueControl is DateTimePicker))
			{
				DateTimePicker dateTimePicker = new DateTimePicker();

				dateTimePicker.MinimumSize = new Size(0, 21);
				dateTimePicker.Format = DateTimePickerFormat.Custom;
				dateTimePicker.CustomFormat = _dateFormatSearch;
				dateTimePicker.Location = existingValueControl.Location;
				dateTimePicker.Name = existingValueControl.Name;
				dateTimePicker.Size = existingValueControl.Size;
				dateTimePicker.TabIndex = existingValueControl.TabIndex;
				dateTimePicker.Anchor = existingValueControl.Anchor;

				Controls.Remove(existingValueControl);
				existingValueControl.Dispose();

				Controls.Add(dateTimePicker);

				_dataViewerHandler.UpdateSearchControl(dateTimePicker);
			}
			else if (isBoolean && !(existingValueControl is ComboBox))
			{
				ComboBox comboBox = new ComboBox();

				comboBox.MinimumSize = new Size(0, 21);
				comboBox.Location = existingValueControl.Location;
				comboBox.Name = existingValueControl.Name;
				comboBox.Size = existingValueControl.Size;
				comboBox.TabIndex = existingValueControl.TabIndex;
				comboBox.Anchor = existingValueControl.Anchor;

				comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

				comboBox.Items.Add("No");
				comboBox.Items.Add("Yes");

				comboBox.SelectedIndex = 0;

				Controls.Remove(existingValueControl);
				existingValueControl.Dispose();

				Controls.Add(comboBox);

				_dataViewerHandler.UpdateSearchControl(comboBox);
			}
			else if (!isBoolean && !isDate && !(existingValueControl is TextBox))
			{
				TextBox textBox = new TextBox();

				textBox.MinimumSize = new Size(0, 21);
				textBox.Location = existingValueControl.Location;
				textBox.Name = existingValueControl.Name;
				textBox.Size = existingValueControl.Size;
				textBox.TabIndex = existingValueControl.TabIndex;
				textBox.Anchor = existingValueControl.Anchor;

				InitializeToolTip(textBox, _searchBoxToolTipText);

				Controls.Remove(existingValueControl);
				existingValueControl.Dispose();

				Controls.Add(textBox);

				_dataViewerHandler.UpdateSearchControl(textBox);
			}
		}
	}

	private void SetClearButton()
	{
		if (searchComboBox.SelectedItem != null)
		{
			KeyValuePair<string, string> selectedItem = (KeyValuePair<string, string>)searchComboBox.SelectedItem;

			bool isDate = false;
			bool isBoolean = false;

			if (_visibleColumns[selectedItem.Key].Length >= 3)
			{
				if (_visibleColumns[selectedItem.Key][2] == "DateTime" && _useDateSelector)
				{
					isDate = true;
				}
				else if (_visibleColumns[selectedItem.Key][2] == "Boolean")
				{
					isBoolean = true;
				}
			}

			if (isDate)
			{
				clearSearchButton.Enabled = false;
			}
			else if (isBoolean)
			{
				clearSearchButton.Enabled = false;
			}
			else
			{
				clearSearchButton.Enabled = true;
			}
		}
	}

	private void InitializeToolTip(Control control, string toolTipText)
	{
		_toolTip.SetToolTip(control, toolTipText);
		_toolTip.AutomaticDelay = 500;

		control.MouseEnter += ToolTipReset;
	}

	private void ToolTipReset(object sender, EventArgs e)
	{
		_toolTip.Active = false;
		_toolTip.Active = true;
	}
}
