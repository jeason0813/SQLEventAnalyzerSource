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

public static class SessionHelper
{
	public static void SaveSession()
	{
		if (ColumnHelper.ColumnCollectionFileName != null)
		{
			RegistryHandler.SaveToRegistry("ColumnsFileName", ColumnHelper.ColumnCollectionFileName);
			RegistryHandler.SaveToRegistry("DefaultColumnName", "");
		}
		else
		{
			RegistryHandler.SaveToRegistry("ColumnsFileName", "");

			if (ConfigHandler.DefaultColumnName != null)
			{
				RegistryHandler.SaveToRegistry("DefaultColumnName", ConfigHandler.DefaultColumnName);
			}
		}
	}

	public static void LoadLastSession()
	{
		ColumnHelper.ColumnCollectionFileName = RegistryHandler.ReadFromRegistry("ColumnsFileName");

		if (ColumnHelper.ColumnCollectionFileName == "")
		{
			ColumnHelper.ColumnCollectionFileName = null;

			string defaultColumnName = RegistryHandler.ReadFromRegistry("DefaultColumnName");

			if (defaultColumnName == "TextDataCleaned")
			{
				ColumnHelper.ColumnCollection = ColumnHelper.XmlToColumnCollection(SQLEventAnalyzer.Properties.Resources.TextDataCleaned);
			}
		}
		else
		{
			ColumnCollection temporaryColumns = ColumnHelper.XmlToColumnCollection(XmlHelper.ReadXmlFromFile(ColumnHelper.ColumnCollectionFileName));

			if (ColumnHelper.ColumnCollection == null)
			{
				ColumnHelper.ColumnCollectionFileName = null;
			}
			else
			{
				ColumnHelper.ColumnCollection = temporaryColumns;
			}
		}
	}
}
