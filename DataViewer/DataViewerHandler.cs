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
using System.Windows.Forms;

public class DataViewerHandler
{
	public delegate void RequestDataSourceUpdateEventHandler(int page, string sortingColumn, ListSortDirection sortingColumnDirection, string searchTerm, string searchColumn, string where, string whereActive);
	public event RequestDataSourceUpdateEventHandler RequestDataSourceUpdateEvent;

	public delegate void RequestDataSourceAllPagesEventHandler(int page, string sortingColumn, ListSortDirection sortingColumnDirection, string searchTerm, string searchColumn, string where, string whereActive);
	public event RequestDataSourceAllPagesEventHandler RequestDataSourceAllPagesEvent;

	public delegate void ShowOutputEventHandler(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
	public event ShowOutputEventHandler ShowOutputEvent;

	private string _sortingColumn;
	private ListSortDirection _sortingColumnDirection;
	private int _page = -1;
	private int _totalRows = -1;
	private int _previousTotalRows = -1;
	private string _previousSearchTerm = "";
	private string _searchTerm;
	private string _searchColumn;
	private string _where;
	private string _whereActive;
	private readonly string _isDataModifiedText;
	private readonly string _isDataModifiedCaptionText;
	private Dictionary<string, string[]> _searchColumns;

	private readonly DataViewerMain _dataViewerMain;
	private readonly DataViewerPager _dataViewerPager;
	private readonly DataViewerSearcher _dataViewerSearcher;
	private readonly SortColumnsHandler _sortColumnsHandler;
	private readonly OutputHandler _outputHandler;

	public DataViewerHandler(DataGridView dataGridView, PagerPanel PagerPanel, string sortingColumn, ListSortDirection sortingColumnDirection, SearcherPanel SearcherPanel, Dictionary<string, string[]> searchColumns, int itemsPerPage, string isDataModifiedText, string isDataModifiedCaptionText, string outOfText, string totalText, int numberOfSortingColumns, Dictionary<string, string> iconDictionary, Dictionary<string, Icon> iconList, ListSortDirection firstSortColumnDirection)
	{
		_isDataModifiedText = isDataModifiedText;
		_isDataModifiedCaptionText = isDataModifiedCaptionText;
		_searchColumns = searchColumns;

		_dataViewerMain = new DataViewerMain(dataGridView, sortingColumn, sortingColumnDirection, iconDictionary, iconList, firstSortColumnDirection);
		_dataViewerPager = new DataViewerPager(PagerPanel, itemsPerPage, outOfText, totalText);
		_dataViewerSearcher = new DataViewerSearcher(SearcherPanel, searchColumns);
		_sortColumnsHandler = new SortColumnsHandler(numberOfSortingColumns, GetSortingColumnPrefix(sortingColumn, searchColumns), sortingColumn, sortingColumnDirection);
		_outputHandler = new OutputHandler();

		_dataViewerMain.SortChangedEvent += DataViewerHandler_SortChangedEvent;
		_dataViewerPager.PageChangedEvent += DataViewerHandler_PageChangedEvent;
		_dataViewerSearcher.SearchEvent += DataViewerHandler_SearchEvent;
		_outputHandler.ShowOutputEvent += OutputHandler_ShowOutputEvent;
		_dataViewerSearcher.ShowOutputEvent += OutputHandler_ShowOutputEvent;
	}

	public void ReInitializeDataGrid()
	{
		_dataViewerMain.ReInitializeDataGrid();
	}

	public void UpdateColumnOrder(Dictionary<string, string[]> visibleColumns)
	{
		_dataViewerMain.UpdateColumnOrder(visibleColumns);
	}

	public void UpdateColumnWidth(Dictionary<string, string[]> visibleColumns)
	{
		_dataViewerMain.UpdateColumnWidth(visibleColumns);
	}

	public void UpdateSearchColumns(Dictionary<string, string[]> searchColumns)
	{
		_searchColumns = searchColumns;
		_dataViewerSearcher.UpdateSearchColumns(searchColumns);
	}

	public void Search()
	{
		_dataViewerSearcher.Search();
	}

	public List<string[]> GetSortMemory()
	{
		return _sortColumnsHandler.Get();
	}

	public void UpdateSearchControl(Control newSearchControl)
	{
		_dataViewerSearcher.UpdateSearchControl(newSearchControl);
	}

	public void GoToNextPage()
	{
		_dataViewerPager.GoToNextPage();
	}

	public void GoToPreviousPage()
	{
		_dataViewerPager.GoToPreviousPage();
	}

	public void SetMultiSelect(bool value)
	{
		_dataViewerMain.SetMultiSelect(value);
	}

	public void SetFocus()
	{
		_dataViewerMain.SetFocus();
	}

	public DataTable GetVisibleDataTable()
	{
		return _dataViewerMain.GetVisibleDataTable();
	}

	public string GetSortingColumn()
	{
		return _sortingColumn;
	}

	public string GetSearchTerm()
	{
		return _searchTerm;
	}

	public int GetPage()
	{
		return _page;
	}

	public string GetSearchColumn()
	{
		return _searchColumn;
	}

	public string GetWhere()
	{
		return _where;
	}

	public string GetWhereActive()
	{
		return _whereActive;
	}

	public ListSortDirection GetSortingColumnDirection()
	{
		return _sortingColumnDirection;
	}

	public void SetEventsEnabled(bool value)
	{
		_dataViewerMain.SetEventsEnabled(value);
		_dataViewerPager.SetEventsEnabled(value);
		_dataViewerSearcher.SetEventsEnabled(value);
	}

	public void DisableSearch()
	{
		_dataViewerSearcher.DisableSearch();
	}

	public List<DataRow> GetSelectedRows()
	{
		return _dataViewerMain.GetSelectedRows();
	}

	public DataRow GetSelectedRow()
	{
		return _dataViewerMain.GetSelectedRow();
	}

	public int GetNumberOfRows()
	{
		return _dataViewerMain.GetNumberOfRows();
	}

	public int GetNumberOfSelectedRows()
	{
		return _dataViewerMain.GetNumberOfSelectedRows();
	}

	public void Reset()
	{
		_previousTotalRows = -1;
		_totalRows = -1;
		_page = -1;
		_previousSearchTerm = "";
		SetSelectedRow(1);
	}

	public void RequestDataSourceAllPages()
	{
		FireRequestDataSourceAllPagesEvent(1, _sortingColumn, _sortingColumnDirection, _searchTerm, _searchColumn, _where, _whereActive);
	}

	public void UpdateGrid()
	{
		_previousTotalRows = -1;
		_totalRows = -1;
		SetSelectedRow(1);
		FireRequestDataSourceUpdateEvent(1, _sortingColumn, _sortingColumnDirection, _searchTerm, _searchColumn, _where, _whereActive);
	}

	public void UpdateGrid(int page, int index)
	{
		_previousTotalRows = -1;
		_totalRows = -1;
		SetSelectedRow(index);
		FireRequestDataSourceUpdateEvent(page, _sortingColumn, _sortingColumnDirection, "", _searchColumn, _where, _whereActive);
	}

	public void UpdateGrid(string searchTerm, string searchColumn)
	{
		SetSelectedRow(1);
		FireRequestDataSourceUpdateEvent(1, _sortingColumn, _sortingColumnDirection, searchTerm, searchColumn, _where, _whereActive);
	}

	public void UpdateGrid(string where)
	{
		SetSelectedRow(1);
		FireRequestDataSourceUpdateEvent(1, _sortingColumn, _sortingColumnDirection, _searchTerm, _searchColumn, where, _whereActive);
	}

	public void SetSelectedRow(int index)
	{
		_dataViewerMain.SetSelectedRow(index);
	}

	public void SelectRow(int index)
	{
		_dataViewerMain.SelectRow(index);
	}

	public void SetDataSource(int page, string sortingColumn, ListSortDirection sortingColumnDirection, DataSet dataSource, string searchTerm, string searchColumn, Dictionary<string, string[]> visibleColumns, string where, string whereActive)
	{
		int totalRows = Convert.ToInt32(dataSource.Tables[1].Columns["TotalRows"].Table.Rows[0][0]);

		if (page == 1 && (searchTerm != "" && _previousSearchTerm == "") || (searchTerm == "" && _previousSearchTerm != ""))
		{
			_previousSearchTerm = searchTerm;
			_totalRows = -1;
			_previousTotalRows = -1;
		}
		else
		{
			_previousTotalRows = _totalRows;
			_totalRows = totalRows;
		}

		if (IsDataModified())
		{
			SetSelectedRow(1);
			FireRequestDataSourceUpdateEvent(1, sortingColumn, sortingColumnDirection, searchTerm, searchColumn, where, whereActive);
		}
		else
		{
			_where = where;
			_whereActive = whereActive;
			_page = page;
			_sortingColumn = sortingColumn;
			_sortingColumnDirection = sortingColumnDirection;
			_searchTerm = searchTerm;
			_searchColumn = searchColumn;

			_dataViewerMain.UpdateDataSource(dataSource.Tables[0], visibleColumns);
			_dataViewerPager.UpdatePagingInfo(page, totalRows);
			_dataViewerSearcher.SetSearchTermText(_searchTerm);
			_dataViewerSearcher.SetSearchComboBox(_searchColumn);
		}
	}

	public void SetItemsPerPage(int itemsPerPage)
	{
		_dataViewerPager.SetItemsPerPage(itemsPerPage);
	}

	public int GetTotalPages()
	{
		return _dataViewerPager.GetTotalPages();
	}

	private bool IsDataModified()
	{
		bool modified = false;

		if (_previousTotalRows != _totalRows && _previousTotalRows != -1)
		{
			modified = true;
			_outputHandler.Show(_isDataModifiedText, _isDataModifiedCaptionText, MessageBoxButtons.OK, MessageBoxIcon.Information);
			Application.DoEvents();
		}

		return modified;
	}

	private void OutputHandler_ShowOutputEvent(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
	{
		if (ShowOutputEvent != null)
		{
			ShowOutputEvent(text, caption, buttons, icon);
		}
	}

	private void FireRequestDataSourceUpdateEvent(int page, string sortingColumn, ListSortDirection sortingColumnDirection, string searchTerm, string searchColumn, string where, string whereActive)
	{
		if (RequestDataSourceUpdateEvent != null)
		{
			RequestDataSourceUpdateEvent(page, sortingColumn, sortingColumnDirection, searchTerm, searchColumn, where, whereActive);
		}
	}

	private void FireRequestDataSourceAllPagesEvent(int page, string sortingColumn, ListSortDirection sortingColumnDirection, string searchTerm, string searchColumn, string where, string whereActive)
	{
		if (RequestDataSourceAllPagesEvent != null)
		{
			RequestDataSourceAllPagesEvent(page, sortingColumn, sortingColumnDirection, searchTerm, searchColumn, where, whereActive);
		}
	}

	private void DataViewerHandler_SortChangedEvent(string sortingColumn, ListSortDirection sortingColumnDirection)
	{
		_sortingColumn = sortingColumn;
		_sortingColumnDirection = sortingColumnDirection;

		string direction = "";

		if (sortingColumnDirection == ListSortDirection.Descending)
		{
			direction = " desc";
		}

		_sortColumnsHandler.Add(GetSortingColumnPrefix(sortingColumn, _searchColumns), sortingColumn, direction);

		FireRequestDataSourceUpdateEvent(_page, _sortingColumn, _sortingColumnDirection, _searchTerm, _searchColumn, _where, _whereActive);
	}

	private void DataViewerHandler_PageChangedEvent(int page)
	{
		_page = page;
		SetSelectedRow(1);
		FireRequestDataSourceUpdateEvent(_page, _sortingColumn, _sortingColumnDirection, _searchTerm, _searchColumn, _where, _whereActive);
	}

	private void DataViewerHandler_SearchEvent(string searchTerm, string searchColumn)
	{
		_searchTerm = searchTerm;
		_searchColumn = searchColumn;
		Reset();
		_page = 1;

		FireRequestDataSourceUpdateEvent(_page, _sortingColumn, _sortingColumnDirection, _searchTerm, _searchColumn, _where, _whereActive);
	}

	private static string GetSortingColumnPrefix(string sortingColumn, Dictionary<string, string[]> searchColumns)
	{
		return searchColumns[sortingColumn][1];
	}
}
