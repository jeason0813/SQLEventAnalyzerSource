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
using System.Data;
using System.IO;
using System.Windows.Forms;

public class ExportToSql
{
	public static void Export(DataTable dataTable, string fileName, DataViewerParameters dataViewerParameters)
	{
		bool success = DoExport(dataTable, fileName, dataViewerParameters);

		if (success)
		{
			string text = "Export successful.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("ExportSuccessful");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}

	private static bool DoExport(DataTable dataTable, string fileName, DataViewerParameters dataViewerParameters)
	{
		bool success = true;

		try
		{
			StreamWriter sw = new StreamWriter(fileName, false, System.Text.Encoding.GetEncoding(1252));

			int iColCount = dataTable.Columns.Count;

			foreach (DataRow dr in dataTable.Rows)
			{
				for (int i = 0; i < iColCount; i++)
				{
					if (ShouldExportColumn(dataViewerParameters, dataTable.Columns[i].ToString()))
					{
						if (!Convert.IsDBNull(dr[i]))
						{
							string data = dr[i].ToString();
							sw.Write(data);
						}
					}
				}

				sw.Write(sw.NewLine);
				sw.Write("go");
				sw.Write(sw.NewLine);
				sw.Write(sw.NewLine);
			}

			sw.Close();
		}
		catch (Exception ex)
		{
			OutputHandler.Show(ex.Message, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			success = false;
		}

		return success;
	}

	private static bool ShouldExportColumn(DataViewerParameters dataViewerParameters, string columnName)
	{
		foreach (KeyValuePair<string, string[]> column in dataViewerParameters.Columns)
		{
			if (column.Key == columnName && columnName == "TextData")
			{
				return true;
			}
		}

		return false;
	}
}
