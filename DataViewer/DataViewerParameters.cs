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
using System.Drawing;

public class DataViewerParameters
{
	public int Page;
	public int ItemsPerPage;
	public string SqlFile;
	public Dictionary<string, string[]> Columns;
	public string SortingColumn;
	public ListSortDirection SortingColumnDirection;
	public string SearchTerm;
	public string SearchColumn;
	public string WhereSingle;
	public string WhereSingleColumn;
	public string WhereActive;
	public int NumberOfSortingColumns;
	public Dictionary<string, string> IconDictionary;
	public Dictionary<string, Icon> IconList;
	public ListSortDirection FirstSortColumnDirection;

	public DataViewerParameters(string sqlFile, Dictionary<string, string[]> columns, string sortingColumn, ListSortDirection sortingColumnDirection, string searchTerm, string searchColumn, string whereSingle, string whereSingleColumn, int itemsPerPage, string whereActive, int numberOfSortingColumns, Dictionary<string, string> iconDictionary, Dictionary<string, Icon> iconList, ListSortDirection firstSortColumnDirection)
	{
		Page = 1;
		SqlFile = sqlFile;
		Columns = columns;
		SortingColumn = sortingColumn;
		SortingColumnDirection = sortingColumnDirection;
		SearchTerm = searchTerm;
		SearchColumn = searchColumn;
		WhereSingle = whereSingle;
		WhereSingleColumn = whereSingleColumn;
		ItemsPerPage = itemsPerPage;
		WhereActive = whereActive;
		NumberOfSortingColumns = numberOfSortingColumns;
		IconDictionary = iconDictionary;
		IconList = iconList;
		FirstSortColumnDirection = firstSortColumnDirection;
	}
}
