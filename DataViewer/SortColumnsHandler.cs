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

using System.Collections.Generic;
using System.ComponentModel;

public class SortColumnsHandler
{
	private readonly int _numberOfSortColumns;
	private readonly List<string[]> _sortMemory;

	public SortColumnsHandler(int numberOfSortColumns, string initialSortingColumnPrefix, string initialSortingColumn, ListSortDirection initialSortingColumnDirection)
	{
		_numberOfSortColumns = numberOfSortColumns;
		_sortMemory = new List<string[]>(_numberOfSortColumns);

		string direction = "";

		if (initialSortingColumnDirection == ListSortDirection.Descending)
		{
			direction = " desc";
		}

		Add(initialSortingColumnPrefix, initialSortingColumn, direction);
	}

	public void Add(string sortingColumnPrefix, string sortingColumn, string direction)
	{
		int indexOfExistingItem = -1;

		for (int i = 0; i <= _sortMemory.Count - 1; i++)
		{
			if (_sortMemory[i][0] == sortingColumnPrefix && _sortMemory[i][1] == sortingColumn)
			{
				indexOfExistingItem = i;
			}
		}

		if (indexOfExistingItem >= 0)
		{
			_sortMemory.RemoveAt(indexOfExistingItem);
		}

		if (_sortMemory.Count == _numberOfSortColumns)
		{
			_sortMemory.RemoveAt(_numberOfSortColumns - 1);
		}

		_sortMemory.Insert(0, new[] { sortingColumnPrefix, sortingColumn, direction });
	}

	public List<string[]> Get()
	{
		return _sortMemory;
	}
}
