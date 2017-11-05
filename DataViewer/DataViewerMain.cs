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

public class DataViewerMain
{
	public delegate void SortChangedEventHandler(string sortingColumn, ListSortDirection sortingColumnDirection);
	public event SortChangedEventHandler SortChangedEvent;

	private DataGridViewColumn _sortingColumn;
	private ListSortDirection _sortingColumnDirection;
	private readonly DataGridView _dataGridView;
	private bool _firstRun;
	private readonly string _initialSortingColumn;
	private readonly ListSortDirection _initialSortingColumnDirection;
	private bool _eventsEnabled = true;
	private int _selectedRow;
	private int _selectedSingleDataRowIndex;
	private bool _activeColumnPresent;
	private bool _initialized;
	private readonly Dictionary<string, string> _iconDictionary;
	private readonly Dictionary<string, Icon> _iconList;
	private bool _fireRowEnterEvent = true;
	private readonly ListSortDirection _firstSortColumnDirection;

	public DataViewerMain(DataGridView dataGridView, string initialSortingColumn, ListSortDirection initialSortingColumnDirection, Dictionary<string, string> iconDictionary, Dictionary<string, Icon> iconList, ListSortDirection firstSortColumnDirection)
	{
		_initialSortingColumn = initialSortingColumn;
		_initialSortingColumnDirection = initialSortingColumnDirection;
		_firstRun = true;
		_dataGridView = dataGridView;
		_dataGridView.CellPainting += DataGridView_CellPainting;
		_dataGridView.RowEnter += DataGridView_RowEnter;
		_iconDictionary = iconDictionary;
		_iconList = iconList;
		_firstSortColumnDirection = firstSortColumnDirection;

		InitializeEvents();
		InitializeNonDefaultValues();
		SetEnabled();
	}

	public void SetMultiSelect(bool value)
	{
		_dataGridView.MultiSelect = value;
	}

	public void SetFocus()
	{
		_dataGridView.Focus();
	}

	public DataTable GetVisibleDataTable()
	{
		DataTable dataTable = ((DataTable)_dataGridView.DataSource).Copy();

		for (int i = dataTable.Columns.Count - 1; i >= 0; i--)
		{
			if (!_dataGridView.Columns[i].Visible)
			{
				dataTable.Columns.RemoveAt(i);
			}
		}

		return dataTable;
	}

	public void SetEventsEnabled(bool value)
	{
		_eventsEnabled = value;
	}

	public int GetNumberOfRows()
	{
		return _dataGridView.Rows.Count;
	}

	public int GetNumberOfSelectedRows()
	{
		return _dataGridView.SelectedRows.Count;
	}

	public void SetSelectedRow(int index)
	{
		_selectedRow = index;
	}

	public void SelectRow(int index)
	{
		_dataGridView.Rows[index].Selected = true;
	}

	public void UpdateColumnWidth(Dictionary<string, string[]> visibleColumns)
	{
		for (int i = _dataGridView.Columns.Count - 1; i >= 0; i--)
		{
			DataGridViewColumn column = _dataGridView.Columns[i];

			if (visibleColumns.ContainsKey(column.Name))
			{
				column.Width = Convert.ToInt32(visibleColumns[column.Name][3]);
			}
		}
	}

	public void UpdateColumnOrder(Dictionary<string, string[]> visibleColumns)
	{
		_fireRowEnterEvent = false;

		List<string> orderedNames = new List<string>();

		foreach (KeyValuePair<string, string[]> visibleColumn in visibleColumns)
		{
			orderedNames.Add(visibleColumn.Key);
		}

		for (int i = 0; i < _dataGridView.Columns.Count; i++)
		{
			DataGridViewColumn column = _dataGridView.Columns[i];

			if (!visibleColumns.ContainsKey(column.Name))
			{
				orderedNames.Insert(i, column.Name);
			}
		}

		List<DataGridViewColumn> orderedColumns = new List<DataGridViewColumn>();

		foreach (string name in orderedNames)
		{
			for (int i = 0; i < _dataGridView.Columns.Count; i++)
			{
				DataGridViewColumn column = _dataGridView.Columns[i];

				if (name == column.Name)
				{
					orderedColumns.Add(column);
					break;
				}
			}
		}

		_dataGridView.Columns.Clear();

		for (int i = 0; i < orderedColumns.Count; i++)
		{
			DataGridViewColumn column = orderedColumns[i];
			column.DisplayIndex = i;
			_dataGridView.Columns.Add(column);
		}

		for (int i = 0; i < _dataGridView.Columns.Count; i++)
		{
			DataGridViewColumn column = _dataGridView.Columns[i];

			if (!visibleColumns.ContainsKey(column.Name) || visibleColumns[column.Name][4] == "SearchableHide" || visibleColumns[column.Name][4] == "NonSearchableHide" || visibleColumns[column.Name][4] == "Hide")
			{
				column.Visible = false;
			}
			else
			{
				column.Visible = true;
			}
		}

		_fireRowEnterEvent = true;
	}

	public void ReInitializeDataGrid()
	{
		_initialized = false;
	}

	public void UpdateDataSource(DataTable dataSource, Dictionary<string, string[]> visibleColumns)
	{
		_dataGridView.DataSource = dataSource;

		if (!_initialized)
		{
			for (int i = 0; i < _dataGridView.Columns.Count; i++)
			{
				DataGridViewColumn column = _dataGridView.Columns[i];

				if (!visibleColumns.ContainsKey(column.Name))
				{
					column.Visible = false;
				}
				else
				{
					column.HeaderText = visibleColumns[column.Name][0];
					column.SortMode = DataGridViewColumnSortMode.Programmatic;
					column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
					column.Width = Convert.ToInt32(visibleColumns[column.Name][3]);
				}

				column.DisplayIndex = i;
			}

			IsActiveColumnPresent(visibleColumns);

			_initialized = true;
		}

		if (_selectedRow > 0 && dataSource.Rows.Count > 0)
		{
			_dataGridView.Rows[_selectedRow - 1].Selected = true;
			_dataGridView.FirstDisplayedScrollingRowIndex = _selectedRow - 1;

			if (_dataGridView.CurrentCell != null)
			{
				_dataGridView.CurrentCell = _dataGridView[_dataGridView.CurrentCell.ColumnIndex, _selectedRow - 1];
			}
		}

		UpdateSortHeader();
		GreyOutInactiveRows();
		SetIconColumns();
		_dataGridView.Focus();
	}

	public List<DataRow> GetSelectedRows()
	{
		List<DataRow> selectedRows = new List<DataRow>();

		for (int i = 0; i < _dataGridView.Rows.Count; i++)
		{
			if (_dataGridView.Rows[i].Selected)
			{
				DataRow row = ((DataTable)_dataGridView.DataSource).Rows[i];
				selectedRows.Add(row);
			}
		}

		return selectedRows;
	}

	public DataRow GetSelectedRow()
	{
		if (_dataGridView.SelectedRows.Count == 1)
		{
			_selectedSingleDataRowIndex = _dataGridView.SelectedCells[0].RowIndex;
		}

		if (((DataTable)_dataGridView.DataSource).Rows.Count > 0)
		{
			return ((DataTable)_dataGridView.DataSource).Rows[_selectedSingleDataRowIndex];
		}

		return null;
	}

	private void SetIconColumns()
	{
		foreach (DataGridViewColumn column in _dataGridView.Columns)
		{
			if (column.CellType == typeof(DataGridViewImageCell))
			{
				column.DefaultCellStyle.NullValue = null;

				foreach (DataGridViewRow row in _dataGridView.Rows)
				{
					DataGridViewImageCell cell = (DataGridViewImageCell)row.Cells[column.Name];
					string iconType = row.Cells[string.Format("{0}_Type", column.Name)].Value.ToString();

					if (_iconDictionary.ContainsKey(iconType))
					{
						cell.Value = GetIcon(_iconDictionary[iconType]);
					}
					else
					{
						cell.Value = null;
					}
				}
			}
		}
	}

	private Icon GetIcon(string iconFileName)
	{
		foreach (KeyValuePair<string, Icon> icon in _iconList)
		{
			if (icon.Key == iconFileName)
			{
				return icon.Value;
			}
		}

		return null;
	}

	private DataGridViewRow GetSelectedDataGridViewRow()
	{
		int index = 0;

		if (_dataGridView.SelectedCells.Count > 0)
		{
			index = _dataGridView.SelectedCells[0].RowIndex;
		}

		if (_dataGridView.Rows.Count > 0)
		{
			return _dataGridView.Rows[index];
		}

		return null;
	}

	private void GreyOutSelectedRow()
	{
		DataGridViewRow selectedRow = GetSelectedDataGridViewRow();

		if (selectedRow != null)
		{
			if (!_dataGridView.Enabled)
			{
				selectedRow.DefaultCellStyle.SelectionForeColor = SystemColors.WindowText;
			}
			else
			{
				selectedRow.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
			}

			if (IsRowInactive(selectedRow))
			{
				selectedRow.DefaultCellStyle.SelectionForeColor = Color.Gray;
			}
		}
	}

	private void DataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
	{
		if (_fireRowEnterEvent)
		{
			GreyOutSelectedRow();
		}
	}

	private void IsActiveColumnPresent(Dictionary<string, string[]> visibleColumns)
	{
		foreach (DataGridViewColumn column in _dataGridView.Columns)
		{
			if (visibleColumns.ContainsKey(column.Name))
			{
				if (visibleColumns[column.Name].Length >= 4)
				{
					string type = visibleColumns[column.Name][4];

					if (type == "Active")
					{
						_activeColumnPresent = true;
					}
				}
			}
		}
	}

	private void GreyOutInactiveRows()
	{
		if (_activeColumnPresent)
		{
			foreach (DataGridViewRow row in _dataGridView.Rows)
			{
				if (IsRowInactive(row))
				{
					row.DefaultCellStyle.ForeColor = Color.Gray;
				}
			}
		}
	}

	private bool IsRowInactive(DataGridViewRow row)
	{
		if (_activeColumnPresent)
		{
			if (row.Cells["Active"].Value.ToString() == "False")
			{
				return true;
			}
		}

		return false;
	}

	private void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
	{
		if (_firstRun)
		{
			SortOrder firstSortOrder;

			if (_initialSortingColumnDirection == ListSortDirection.Ascending)
			{
				firstSortOrder = SortOrder.Ascending;
			}
			else
			{
				firstSortOrder = SortOrder.Descending;
			}

			DataGridViewColumn sortingColumn = _dataGridView.Columns[_initialSortingColumn];

			if (sortingColumn != null)
			{
				sortingColumn.HeaderCell.SortGlyphDirection = firstSortOrder;
			}

			GreyOutInactiveRows();
		}
	}

	private void UpdateSortHeader()
	{
		if (_sortingColumn != null)
		{
			DataGridViewColumn column = _dataGridView.Columns[_sortingColumn.Name];

			if (column != null)
			{
				if (_sortingColumnDirection == ListSortDirection.Ascending)
				{
					column.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
				}
				else
				{
					column.HeaderCell.SortGlyphDirection = SortOrder.Descending;
				}
			}

			_sortingColumn = _dataGridView.Columns[_sortingColumn.Name];
		}
	}

	private void InitializeNonDefaultValues()
	{
		_dataGridView.AllowUserToAddRows = false;
		_dataGridView.AllowUserToDeleteRows = false;
		_dataGridView.AllowUserToOrderColumns = true;
		_dataGridView.ReadOnly = true;
		_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
	}

	private void InitializeEvents()
	{
		_dataGridView.ColumnHeaderMouseClick += DataGridView_ColumnHeaderSorter;
		_dataGridView.ColumnHeaderMouseDoubleClick += DataGridView_ColumnHeaderSorter;
		_dataGridView.EnabledChanged += _dataGridView_EnabledChanged;
	}

	private void _dataGridView_EnabledChanged(object sender, EventArgs e)
	{
		SetEnabled();
	}

	private void SetEnabled()
	{
		if (_dataGridView.Enabled)
		{
			_dataGridView.DefaultCellStyle.BackColor = SystemColors.Window;
			_dataGridView.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
			_dataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
		}
		else
		{
			_dataGridView.DefaultCellStyle.BackColor = SystemColors.Control;
			_dataGridView.DefaultCellStyle.SelectionBackColor = SystemColors.Control;
			_dataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.WindowText;
		}

		GreyOutSelectedRow();
	}

	private void FireSortChangedEvent()
	{
		if (SortChangedEvent != null)
		{
			SortChangedEvent(_sortingColumn.Name, _sortingColumnDirection);
		}
	}

	private ListSortDirection GetFirstSortDirection()
	{
		return _firstSortColumnDirection;
	}

	private ListSortDirection GetSecondSortDirection()
	{
		if (_firstSortColumnDirection == ListSortDirection.Ascending)
		{
			return ListSortDirection.Descending;
		}
		else
		{
			return ListSortDirection.Ascending;
		}
	}

	private void DataGridView_ColumnHeaderSorter(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (!_eventsEnabled)
		{
			return;
		}

		if (_firstRun)
		{
			_firstRun = false;

			_sortingColumn = _dataGridView.Columns[_initialSortingColumn];
			_sortingColumnDirection = _initialSortingColumnDirection;
		}

		DataGridViewColumn newColumn = _dataGridView.Columns[e.ColumnIndex];
		DataGridViewColumn oldColumn = _sortingColumn;
		ListSortDirection direction;

		if (oldColumn == null)
		{
			direction = GetFirstSortDirection();
		}
		else
		{
			if (oldColumn == newColumn && _sortingColumnDirection == GetFirstSortDirection())
			{
				direction = GetSecondSortDirection();
			}
			else
			{
				direction = GetFirstSortDirection();
				oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
			}
		}

		_sortingColumn = newColumn;
		_sortingColumnDirection = direction;

		if (direction == ListSortDirection.Ascending)
		{
			newColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
		}
		else
		{
			newColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending;
		}

		FireSortChangedEvent();
	}
}
