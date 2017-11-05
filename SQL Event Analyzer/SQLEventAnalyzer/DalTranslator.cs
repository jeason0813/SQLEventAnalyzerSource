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

using System.Collections.Generic;
using System.ComponentModel;

public static class DalTranslator
{
	// 0: Shown name
	// 1: Column prefix
	// 2: Data Type. Can be "Integer", "Text", "DateTime", "FullText"
	// 3: Column width
	// 4: Column type. Value indicating if the column should be shown or not. Can be "SearchableShow", "SearchableHide", "NonSearchableShow", "NonSearchableHide"
	//	  SearchableShow / SearchableHide:			Columns that can be searched even if they are not shown.
	//	  NonSearchableShow / NonSearchableHide:	Columns that can't be searched even if they are shown.

	public static DataViewerParameters GetTraceData()
	{
		Dictionary<string, string[]> columns = new Dictionary<string, string[]>();

		columns.Add("ID", new[] { "Id", "t", "Integer", "54", "SearchableShow" });

		columns.Add("FileName", new[] { "FileName", "t", "Text", "116", "NonSearchableHide" });
		columns.Add("Type", new[] { "Type", "t", "Text", "116", "NonSearchableHide" });
		columns.Add("TextData", new[] { "TextData", "t", "FullText", "250", "SearchableShow" });
		columns.Add("SPID", new[] { "SPID", "t", "Integer", "54", "NonSearchableHide" });
		columns.Add("StartTime", new[] { "StartTime", "t", "DateTime", "131", "SearchableShow" });
		columns.Add("Duration", new[] { "Duration", "t", "Integer", "77", "SearchableShow" });
		columns.Add("Reads", new[] { "Reads", "t", "Integer", "77", "SearchableShow" });
		columns.Add("Writes", new[] { "Writes", "t", "Integer", "77", "NonSearchableHide" });
		columns.Add("CPU", new[] { "CPU", "t", "Integer", "77", "NonSearchableHide" });
		columns.Add("Rows", new[] { "Rows", "t", "Integer", "77", "NonSearchableHide" });

		foreach (Column column in ColumnHelper.EnabledColumns)
		{
			columns.Add(column.Name, new[] { column.Name, "t", "Text", column.Width.ToString(), GetCustomColumnColumnShownText(!column.Hidden) });
		}

		return new DataViewerParameters("GetTraceData.sql", columns, "StartTime", ListSortDirection.Descending, "", "TextData", "and t.ID = {0}", "Id", ConfigHandler.ItemsPerPage, "", ConfigHandler.SortMemory, null, null, ListSortDirection.Descending);
	}

	public static DataViewerParameters GetStatisticsData()
	{
		Dictionary<string, string[]> columns = new Dictionary<string, string[]>();

		DataViewerParameters dataViewerParameters = GetTraceData();

		foreach (KeyValuePair<string, string[]> column in dataViewerParameters.Columns)
		{
			columns.Add(column.Key, new[] { column.Key, column.Value[1], column.Value[2], column.Value[3], "NonSearchableHide" });
		}

		columns.Add("TotalCount", new[] { "TotalCount", "t", "Integer", "100", "NonSearchableShow" });
		columns.Add("MinDuration", new[] { "MinDuration", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("MaxDuration", new[] { "MaxDuration", "t", "Integer", "100", "NonSearchableShow" });
		columns.Add("AvgDuration", new[] { "AvgDuration", "t", "Integer", "100", "NonSearchableShow" });
		columns.Add("DevDuration", new[] { "DevDuration", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("VarDuration", new[] { "VarDuration", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("SumDuration", new[] { "SumDuration", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("MinReads", new[] { "MinReads", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("MaxReads", new[] { "MaxReads", "t", "Integer", "100", "NonSearchableShow" });
		columns.Add("AvgReads", new[] { "AvgReads", "t", "Integer", "100", "NonSearchableShow" });
		columns.Add("DevReads", new[] { "DevReads", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("VarReads", new[] { "VarReads", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("SumReads", new[] { "SumReads", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("MinWrites", new[] { "MinWrites", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("MaxWrites", new[] { "MaxWrites", "t", "Integer", "100", "NonSearchableShow" });
		columns.Add("AvgWrites", new[] { "AvgWrites", "t", "Integer", "100", "NonSearchableShow" });
		columns.Add("DevWrites", new[] { "DevWrites", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("VarWrites", new[] { "VarWrites", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("SumWrites", new[] { "SumWrites", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("MinCPU", new[] { "MinCPU", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("MaxCPU", new[] { "MaxCPU", "t", "Integer", "100", "NonSearchableShow" });
		columns.Add("AvgCPU", new[] { "AvgCPU", "t", "Integer", "100", "NonSearchableShow" });
		columns.Add("DevCPU", new[] { "DevCPU", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("VarCPU", new[] { "VarCPU", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("SumCPU", new[] { "SumCPU", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("MinRows", new[] { "MinRows", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("MaxRows", new[] { "MaxRows", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("AvgRows", new[] { "AvgRows", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("DevRows", new[] { "DevRows", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("VarRows", new[] { "VarRows", "t", "Integer", "100", "NonSearchableHide" });
		columns.Add("SumRows", new[] { "SumRows", "t", "Integer", "100", "NonSearchableHide" });

		string sortingColumn = "TotalCount";
		string searchColumn = "";

		//if (ConfigHandler.GroupByColumns.Count > 0)
		//{
		//	sortingColumn = ConfigHandler.GroupByColumns[0];
		//	searchColumn = ConfigHandler.GroupByColumns[0];
		//}

		return new DataViewerParameters("GetStatistics.sql", columns, sortingColumn, ListSortDirection.Descending, "", searchColumn, "", "", ConfigHandler.ItemsPerPage, "", ConfigHandler.SortMemory, null, null, ListSortDirection.Descending);
	}

	private static string GetCustomColumnColumnShownText(bool show)
	{
		if (show)
		{
			return "SearchableShow";
		}

		return "NonSearchableHide";
	}
}
