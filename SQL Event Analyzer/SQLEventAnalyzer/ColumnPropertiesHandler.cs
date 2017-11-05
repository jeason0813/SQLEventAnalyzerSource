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
using System.Text;
using System.Xml;

internal static class ColumnPropertiesHandler
{
	public static void LoadColumnWidthAndOrder(DataViewerParameters dataViewerParameters, string registryName)
	{
		string xml = RegistryHandler.ReadFromRegistry(registryName);

		if (xml == "")
		{
			return;
		}

		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(xml);

		XmlNodeList xmlNodeList = xmlDocument.SelectNodes("columns/column");

		Dictionary<string, string[]> newColumns = new Dictionary<string, string[]>();

		if (xmlNodeList != null)
		{
			foreach (XmlElement xmlElement in xmlNodeList)
			{
				string key = xmlElement.GetAttribute("key");
				string type = xmlElement.GetAttribute("type");
				string width = xmlElement.GetAttribute("width");

				if (dataViewerParameters.Columns.ContainsKey(key))
				{
					string[] values = dataViewerParameters.Columns[key];
					values[4] = type;
					values[3] = width;

					newColumns.Add(key, values);
				}
			}
		}

		foreach (KeyValuePair<string, string[]> column in dataViewerParameters.Columns)
		{
			bool found = false;

			foreach (KeyValuePair<string, string[]> newColumn in newColumns)
			{
				if (column.Key == newColumn.Key)
				{
					found = true;
					break;
				}
			}

			if (!found)
			{
				newColumns.Add(column.Key, column.Value);
			}
		}

		dataViewerParameters.Columns = newColumns;
	}

	public static void ChangeWidth(DataViewerParameters dataViewerParameters, string columnName, int width)
	{
		foreach (KeyValuePair<string, string[]> column in dataViewerParameters.Columns)
		{
			if (column.Key == columnName)
			{
				column.Value[3] = width.ToString();
			}
		}
	}

	public static void SaveColumnWidthAndOrder(DataViewerParameters dataViewerParameters, string registryName)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?><columns>");

		foreach (KeyValuePair<string, string[]> column in dataViewerParameters.Columns)
		{
			string key = column.Key;
			string type = column.Value[4];
			string width = column.Value[3];

			stringBuilder.Append(string.Format("<column key=\"{0}\" type=\"{1}\" width=\"{2}\" />", System.Security.SecurityElement.Escape(key), type, width));
		}

		stringBuilder.Append("</columns>");

		if (ConfigHandler.RegistryModifyAccess)
		{
			RegistryHandler.SaveToRegistry(registryName, stringBuilder.ToString());
		}
	}

	public static Dictionary<string, string[]> ChangeColumnOrder(DataViewerParameters dataViewerParameters, string columnName, int displayIndex)
	{
		List<string> keys = new List<string>();

		Dictionary<string, string[]> newColumns = new Dictionary<string, string[]>();

		foreach (KeyValuePair<string, string[]> column in dataViewerParameters.Columns)
		{
			if (column.Key != columnName)
			{
				keys.Add(column.Key);
			}
		}

		if (MoveRight(columnName, displayIndex, dataViewerParameters))
		{
			keys.Insert(displayIndex - 0, columnName);
		}
		else
		{
			keys.Insert(displayIndex + 1, columnName);
		}

		foreach (string key in keys)
		{
			newColumns.Add(key, GetValueFromKey(key, dataViewerParameters));
		}

		return newColumns;
	}

	private static bool MoveRight(string columnName, int displayIndex, DataViewerParameters dataViewerParameters)
	{
		int beforeColIndex = GetColumnIndexFromKey(columnName, dataViewerParameters);

		if (displayIndex < beforeColIndex)
		{
			return false;
		}

		return true;
	}

	private static string[] GetValueFromKey(string key, DataViewerParameters dataViewerParameters)
	{
		foreach (KeyValuePair<string, string[]> column in dataViewerParameters.Columns)
		{
			if (column.Key == key)
			{
				return column.Value;
			}
		}

		return null;
	}

	private static int GetColumnIndexFromKey(string key, DataViewerParameters dataViewerParameters)
	{
		int i = 0;

		foreach (KeyValuePair<string, string[]> column in dataViewerParameters.Columns)
		{
			if (column.Key == key)
			{
				return i;
			}

			i++;
		}

		return -1;
	}
}
