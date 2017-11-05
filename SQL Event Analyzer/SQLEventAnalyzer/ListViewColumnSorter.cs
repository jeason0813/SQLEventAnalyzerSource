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
using System.Collections;
using System.Windows.Forms;

public class ListViewColumnSorter : IComparer
{
	private int _columnToSort;
	private SortOrder _orderOfSort;
	private readonly CaseInsensitiveComparer _objectCompare;
	private bool _sortByDecimalValue;
	private bool _sortByDateTimeValue;
	private bool _sortByCheckedValue;

	public ListViewColumnSorter()
	{
		_columnToSort = 0;
		_orderOfSort = SortOrder.None;
		_objectCompare = new CaseInsensitiveComparer();
	}

	public int Compare(object x, object y)
	{
		ListViewItem listviewX = (ListViewItem)x;
		ListViewItem listviewY = (ListViewItem)y;

		int compareResult;

		if (_sortByDecimalValue)
		{
			compareResult = _objectCompare.Compare(Convert.ToDecimal(listviewX.SubItems[_columnToSort].Text), Convert.ToDecimal(listviewY.SubItems[_columnToSort].Text));
		}
		else if (_sortByDateTimeValue)
		{
			compareResult = _objectCompare.Compare(Convert.ToDateTime(listviewX.SubItems[_columnToSort].Text), Convert.ToDateTime(listviewY.SubItems[_columnToSort].Text));
		}
		else if (_sortByCheckedValue)
		{
			compareResult = _objectCompare.Compare(listviewX.Checked, listviewY.Checked);
		}
		else
		{
			compareResult = _objectCompare.Compare(listviewX.SubItems[_columnToSort].Text, listviewY.SubItems[_columnToSort].Text);
		}

		if (_orderOfSort == SortOrder.Ascending)
		{
			return compareResult;
		}
		else if (_orderOfSort == SortOrder.Descending)
		{
			return (-compareResult);
		}
		else
		{
			return 0;
		}
	}

	public int SortColumn
	{
		set
		{
			_columnToSort = value;
		}
		get
		{
			return _columnToSort;
		}
	}

	public SortOrder Order
	{
		set
		{
			_orderOfSort = value;
		}
		get
		{
			return _orderOfSort;
		}
	}

	public bool SortByDecimalValue
	{
		set
		{
			_sortByDecimalValue = value;
		}
		get
		{
			return _sortByDecimalValue;
		}
	}

	public bool SortByDateTimeValue
	{
		set
		{
			_sortByDateTimeValue = value;
		}
		get
		{
			return _sortByDateTimeValue;
		}
	}

	public bool SortByCheckedValue
	{
		set
		{
			_sortByCheckedValue = value;
		}
		get
		{
			return _sortByCheckedValue;
		}
	}
}
