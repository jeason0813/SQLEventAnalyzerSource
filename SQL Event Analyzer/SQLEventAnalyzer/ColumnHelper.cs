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
using System.Text;
using System.Windows.Forms;
using System.Xml;

public static class ColumnHelper
{
	public static string ColumnCollectionFileName;
	private static ColumnCollection _columnCollection;

	public static void SaveColumnCollection(string fileName)
	{
		XmlHelper.WriteXmlToFile(ColumnCollectionToXml(ColumnCollection), fileName);
	}

	public static List<Column> EnabledColumns
	{
		get
		{
			return ColumnCollection.Columns.FindAll(delegate(Column c)
			{
				if (!ConfigHandler.ClrDeployed && (ConfigHandler.ColumnTypeClrDependant(c.InputType) || ConfigHandler.ColumnTypeClrDependant(c.OutputType)))
				{
					return false;
				}

				return c.Enabled;
			});
		}
	}

	public static ColumnCollection ColumnCollection
	{
		get
		{
			if (_columnCollection == null)
			{
				_columnCollection = new ColumnCollection();
			}

			return _columnCollection;
		}
		set
		{
			_columnCollection = value;
		}
	}

	public static string ColumnCollectionToXml(ColumnCollection columnCollection)
	{
		if (columnCollection.Columns == null)
		{
			return "";
		}

		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?><root>");

		stringBuilder.Append("<options>");

		foreach (Option option in columnCollection.Options)
		{
			stringBuilder.Append(string.Format("<option name=\"{0}\" value=\"{1}\" />", System.Security.SecurityElement.Escape(option.Name), System.Security.SecurityElement.Escape(option.Value)));
		}

		stringBuilder.Append("</options>");

		stringBuilder.Append("<parameters>");

		foreach (Parameter parameter in columnCollection.Parameters)
		{
			stringBuilder.Append(string.Format("<parameter name=\"{0}\" value=\"{1}\" />", System.Security.SecurityElement.Escape(parameter.Name), System.Security.SecurityElement.Escape(parameter.Value)));
		}

		stringBuilder.Append("</parameters>");

		stringBuilder.Append("<columns>");

		foreach (Column column in columnCollection.Columns)
		{
			stringBuilder.Append(string.Format("<column name=\"{0}\" isolationLevel=\"{1}\" input=\"{2}\" inputType=\"{3}\" output=\"{4}\" outputType=\"{5}\" hidden=\"{6}\" enabled=\"{7}\" width=\"{8}\" />", System.Security.SecurityElement.Escape(column.Name), IsolationLevelTypeToString(column.IsolationLevel), System.Security.SecurityElement.Escape(column.Input), ColumnTypeToString(column.InputType), System.Security.SecurityElement.Escape(column.Output), ColumnTypeToString(column.OutputType), column.Hidden, column.Enabled, column.Width));
		}

		stringBuilder.Append("</columns>");
		stringBuilder.Append("</root>");

		return stringBuilder.ToString();
	}

	public static ColumnCollection XmlToColumnCollection(string xml)
	{
		if (string.IsNullOrEmpty(xml))
		{
			return null;
		}

		ColumnCollection columnCollection = new ColumnCollection();

		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);

			XmlNodeList columnNodes = xmlDocument.SelectNodes("root/columns/column");

			foreach (XmlElement columnNode in columnNodes)
			{
				Column column = new Column(columnNode.GetAttribute("name"), StringToIsolationLevelType(columnNode.GetAttribute("isolationLevel")), columnNode.GetAttribute("input"), StringToColumnType(columnNode.GetAttribute("inputType")), columnNode.GetAttribute("output"), StringToColumnType(columnNode.GetAttribute("outputType")), Convert.ToBoolean(columnNode.GetAttribute("hidden")), Convert.ToBoolean(columnNode.GetAttribute("enabled")), Convert.ToInt32(columnNode.GetAttribute("width")));
				columnCollection.Columns.Add(column);
			}

			XmlNodeList parameterNodes = xmlDocument.SelectNodes("root/parameters/parameter");

			foreach (XmlElement parameterNode in parameterNodes)
			{
				Parameter parameter = new Parameter(parameterNode.GetAttribute("name"), parameterNode.GetAttribute("value"));
				columnCollection.Parameters.Add(parameter);
			}

			XmlNodeList optionNodes = xmlDocument.SelectNodes("root/options/option");

			foreach (XmlElement optionNode in optionNodes)
			{
				Option option = new Option(optionNode.GetAttribute("name"), optionNode.GetAttribute("value"));
				columnCollection.Options.Add(option);
			}
		}
		catch (Exception ex)
		{
			if (ex.Message == "Object reference not set to an instance of an object.")
			{
				string text = "Column import file is missing one or more column elements.";

				if (ConfigHandler.UseTranslation)
				{
					text = Translator.GetText("ImportFileMissing");
				}

				OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else
			{
				OutputHandler.Show(ex.Message, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		return columnCollection;
	}

	public static string GetColumnTypeName(Column.ColumnType columnType)
	{
		if (ConfigHandler.UseTranslation)
		{
			switch (columnType)
			{
				case Column.ColumnType.RegEx:
					return Translator.GetText("RegEx");
				case Column.ColumnType.SQL:
					return Translator.GetText("SQL");
				case Column.ColumnType.Constant:
					return Translator.GetText("Constant");
				case Column.ColumnType.StoredProcedureName:
					return Translator.GetText("StoredProcedureName");
				case Column.ColumnType.StoredProcedureParameter:
					return Translator.GetText("StoredProcedureParameter");
				case Column.ColumnType.LogParameter:
					return Translator.GetText("LogParameter");
			}
		}
		else
		{
			switch (columnType)
			{
				case Column.ColumnType.RegEx:
					return "RegEx";
				case Column.ColumnType.SQL:
					return "SQL";
				case Column.ColumnType.Constant:
					return "Constant";
				case Column.ColumnType.StoredProcedureName:
					return "Stored Procedure Name";
				case Column.ColumnType.StoredProcedureParameter:
					return "Stored Procedure Parameter";
				case Column.ColumnType.LogParameter:
					return "Log Parameter";
			}
		}

		return null;
	}

	public static Column.IsolationLevelType StringToIsolationLevelType(string isolationLevel)
	{
		switch (isolationLevel)
		{
			case "Read Committed":
				return Column.IsolationLevelType.ReadCommitted;
			case "Repeatable Read":
				return Column.IsolationLevelType.RepeatableRead;
			case "Serializable":
				return Column.IsolationLevelType.Serializable;
		}

		return Column.IsolationLevelType.ReadUncommitted;
	}

	public static string IsolationLevelTypeToString(Column.IsolationLevelType isolationLevel)
	{
		switch (isolationLevel)
		{
			case Column.IsolationLevelType.ReadCommitted:
				return "Read Committed";
			case Column.IsolationLevelType.RepeatableRead:
				return "Repeatable Read";
			case Column.IsolationLevelType.Serializable:
				return "Serializable";
		}

		return "Read Uncommitted";
	}

	public static Version GetVersion()
	{
		string versionString = null;

		foreach (Option option in ColumnCollection.Options)
		{
			if (option.Name == "Version")
			{
				versionString = option.Value;
				break;
			}
		}

		Version version;

		if (versionString == null)
		{
			version = new Version("1.0.0");
		}
		else
		{
			version = new Version(versionString);
		}

		return version;
	}

	public static string GetUpdateServer()
	{
		string updateServiceUrl = null;

		foreach (Option option in ColumnCollection.Options)
		{
			if (option.Name == "UpdateServer")
			{
				updateServiceUrl = option.Value;
				break;
			}
		}

		return updateServiceUrl;
	}

	public static bool GetAutomaticUpdateEnabled()
	{
		bool automaticUpdateEnabled = false;

		foreach (Option option in ColumnCollection.Options)
		{
			if (option.Name == "AutomaticUpdateEnabled")
			{
				automaticUpdateEnabled = Convert.ToBoolean(option.Value);
				break;
			}
		}

		return automaticUpdateEnabled;
	}

	private static Column.ColumnType StringToColumnType(string columnType)
	{
		switch (columnType)
		{
			case "RegEx":
				return Column.ColumnType.RegEx;
			case "SQL":
				return Column.ColumnType.SQL;
			case "StoredProcedureName":
				return Column.ColumnType.StoredProcedureName;
			case "StoredProcedureParameter":
				return Column.ColumnType.StoredProcedureParameter;
			case "LogParameter":
				return Column.ColumnType.LogParameter;
		}

		return Column.ColumnType.Constant;
	}

	private static string ColumnTypeToString(Column.ColumnType columnType)
	{
		switch (columnType)
		{
			case Column.ColumnType.RegEx:
				return "RegEx";
			case Column.ColumnType.SQL:
				return "SQL";
			case Column.ColumnType.StoredProcedureName:
				return "StoredProcedureName";
			case Column.ColumnType.StoredProcedureParameter:
				return "StoredProcedureParameter";
			case Column.ColumnType.LogParameter:
				return "LogParameter";
		}

		return "Constant";
	}
}
