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
using System.IO;

public static class ConfigHandler
{
	private static readonly Guid _uniqueTestGuid = Guid.NewGuid();

	public static int SortMemory = 1;
	public static string RecordTraceFileDir;
	public static string TraceFileName = string.Format("SQLEventAnalyzer-{0}", _uniqueTestGuid);
	public static string RecordingPrefix = string.Format("-- Recording ({0})", _uniqueTestGuid);
	public static string ConnectionString;
	public static string ConnectionStringToSave;
	public static string SaveConnectionString;
	public static string TextDataFontFamily;
	public static string TextDataFontSize;
	public static string EditorFontFamily;
	public static string EditorFontSize;
	public static string WindowSize;
	public static string SplitterDistance;
	public static string ColumnEditorWindowSize;
	public static string ColumnEditorSplitterDistance;
	public static string ColumnsWindowSize;
	public static string ViewStatisticsWindowSize;
	public static string ViewRowWindowSize;
	public static string TimelineWindowSize;
	public static string Language;
	public static string ActiveCustomColumn = "";
	public static string ColumnsFileName;
	public static string DefaultColumnName;
	public static int MaxGraphRows;
	public static int ItemsPerPage = 50;
	public static bool ClrDeployed;
	public static bool ShowHiddenColumns;
	public static bool ErrorFormShown;
	public static bool GetDataFormShown;
	public static bool EnableCLRTemporary;
	public static bool TempTableCreated;
	public static bool CustomColumnsFormShown;
	public static bool KeepSessionOnExit;
	public static bool EnableFileNameAndType;
	public static bool AutoPopulateFilter2;
	public static bool EnableQuickSearch;
	public static DateTime GetDataStartTime;
	public static DateTime GetDataEndTime;
	public static DateTime ImportStartTime;
	public static DateTime ImportEndTime;
	public static DateTime HandleColumnsStartTime;
	public static DateTime HandleColumnsEndTime;
	public static List<string> GroupByColumns;
	public static List<int> ErrorNumbersToSkip;
	public static string UseExtendedEvents;
	public static int ColumnMaxCharacters = 50;
	public const int NumberOfRecentFiles = 10;
	public const int NumberOfServiceContextLogFiles = 10;
	public static List<Filter> ActiveFilters;
	public static bool SaveOutputToLogFile = false;
	public static DateTime UnattendedRunStartTime;
	public static string CheckForUpdatesOnStart;
	public static string DatabaseName;
	public static string UpdateServiceUrl;
	public static string StatisticsArchive;
	public static string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
	public static string DateTimeLongFormat = "yyyy-MM-dd HH:mm:ss.fff"; // must correspond to DateTimeLongSqlFormat
	public static int DateTimeLongSqlFormat = 121; // must correspond to DateTimeLongFormat
	public static string DateFormatSearch = "yyyy-MM-dd";
	public static bool UseDateSelectorInQuickSearch = false;
	public static string DateFormatSearchGranularity = "dd";
	public static string DateTimeFileFormat = "yyyyMMdd-HHmmss";
	public static int SavedSearchesMaxLength = 30;
	public static bool RegistryModifyAccess = false;

	public static void SetRecordTraceFileDirectory(DatabaseOperation databaseOperation)
	{
		RecordTraceFileDir = RegistryHandler.ReadFromRegistry("RecordTraceFileDir");

		if (RecordTraceFileDir == "")
		{
			string traceFileDir = databaseOperation.GetSqlServerDataDir();

			if (traceFileDir == null || !Directory.Exists(traceFileDir))
			{
				traceFileDir = GenericHelper.TempPath;
			}

			RecordTraceFileDir = traceFileDir;
		}
	}

	public static bool UseTranslation
	{
		get
		{
			if (Language.ToLower() != "english")
			{
				return true;
			}

			return false;
		}
	}

	public static void SaveConnection()
	{
		if (RegistryModifyAccess)
		{
			RegistryHandler.SaveToRegistry("ConnectionString", ConnectionStringSecurity.EncodeConnectionString(ConnectionStringToSave, "ConnectionString"));
		}
	}

	public static void LoadConfig()
	{
		ConnectionString = ConnectionStringSecurity.DecodeConnectionString(RegistryHandler.ReadFromRegistry("ConnectionString"), "ConnectionString");

		if (ConnectionString == "")
		{
			ConnectionString = @"Data Source=SQLServerName\SQLServerInstance;Initial Catalog=master;Integrated Security=True";
		}

		ConnectionStringToSave = ConnectionStringSecurity.DecodeConnectionString(RegistryHandler.ReadFromRegistry("ConnectionString"), "ConnectionString");

		if (ConnectionStringToSave == "")
		{
			ConnectionStringToSave = @"Data Source=SQLServerName\SQLServerInstance;Initial Catalog=master;Integrated Security=True";
		}

		SaveConnectionString = RegistryHandler.ReadFromRegistry("SaveConnectionString");

		if (SaveConnectionString == "")
		{
			SaveConnectionString = "True";
		}

		TextDataFontFamily = RegistryHandler.ReadFromRegistry("TextDataFontFamily");

		if (TextDataFontFamily == "")
		{
			TextDataFontFamily = "Courier New";
		}

		TextDataFontSize = RegistryHandler.ReadFromRegistry("TextDataFontSize");

		if (TextDataFontSize == "")
		{
			TextDataFontSize = "10";
		}

		EditorFontFamily = RegistryHandler.ReadFromRegistry("EditorFontFamily");

		if (EditorFontFamily == "")
		{
			EditorFontFamily = "Courier New";
		}

		EditorFontSize = RegistryHandler.ReadFromRegistry("EditorFontSize");

		if (EditorFontSize == "")
		{
			EditorFontSize = "10";
		}

		WindowSize = RegistryHandler.ReadFromRegistry("WindowSize");

		if (WindowSize == "")
		{
			WindowSize = "900; 680";
		}

		SplitterDistance = RegistryHandler.ReadFromRegistry("SplitterDistance");

		if (SplitterDistance == "")
		{
			SplitterDistance = "271";
		}

		ColumnEditorWindowSize = RegistryHandler.ReadFromRegistry("ColumnEditorWindowSize");

		if (ColumnEditorWindowSize == "")
		{
			ColumnEditorWindowSize = "900; 680";
		}

		ColumnEditorSplitterDistance = RegistryHandler.ReadFromRegistry("ColumnEditorSplitterDistance");

		if (ColumnEditorSplitterDistance == "")
		{
			ColumnEditorSplitterDistance = "271";
		}

		ColumnsWindowSize = RegistryHandler.ReadFromRegistry("ColumnsWindowSize");

		if (ColumnsWindowSize == "")
		{
			ColumnsWindowSize = "900; 680";
		}

		ViewStatisticsWindowSize = RegistryHandler.ReadFromRegistry("ViewStatisticsWindowSize");

		if (ViewStatisticsWindowSize == "")
		{
			ViewStatisticsWindowSize = "900; 680";
		}

		ViewRowWindowSize = RegistryHandler.ReadFromRegistry("ViewRowWindowSize");

		if (ViewRowWindowSize == "")
		{
			ViewRowWindowSize = "900; 680";
		}

		TimelineWindowSize = RegistryHandler.ReadFromRegistry("TimelineWindowSize");

		if (TimelineWindowSize == "")
		{
			TimelineWindowSize = "900; 680";
		}

		if (RegistryHandler.ReadFromRegistry("ItemsPerPage") == "")
		{
			ItemsPerPage = 50;
		}
		else
		{
			ItemsPerPage = Convert.ToInt32(RegistryHandler.ReadFromRegistry("ItemsPerPage"));
		}

		Language = RegistryHandler.ReadFromRegistry("Language");

		if (Language == "")
		{
			Language = "english";
		}

		if (RegistryHandler.ReadFromRegistry("ShowHiddenColumns") == "")
		{
			ShowHiddenColumns = false;
		}
		else
		{
			ShowHiddenColumns = Convert.ToBoolean(RegistryHandler.ReadFromRegistry("ShowHiddenColumns"));
		}

		if (RecordTraceFileDir == null)
		{
			RecordTraceFileDir = "";
		}

		if (RegistryHandler.ReadFromRegistry("KeepSessionOnExit") == "")
		{
			KeepSessionOnExit = false;
		}
		else
		{
			KeepSessionOnExit = Convert.ToBoolean(RegistryHandler.ReadFromRegistry("KeepSessionOnExit"));
		}

		if (RegistryHandler.ReadFromRegistry("EnableFileNameAndType") == "")
		{
			EnableFileNameAndType = false;
		}
		else
		{
			EnableFileNameAndType = Convert.ToBoolean(RegistryHandler.ReadFromRegistry("EnableFileNameAndType"));
		}

		if (RegistryHandler.ReadFromRegistry("EnableQuickSearch") == "")
		{
			EnableQuickSearch = false;
		}
		else
		{
			EnableQuickSearch = Convert.ToBoolean(RegistryHandler.ReadFromRegistry("EnableQuickSearch"));
		}

		if (RegistryHandler.ReadFromRegistry("AutoPopulateFilter2") == "")
		{
			AutoPopulateFilter2 = false;
		}
		else
		{
			AutoPopulateFilter2 = Convert.ToBoolean(RegistryHandler.ReadFromRegistry("AutoPopulateFilter2"));
		}

		if (RegistryHandler.ReadFromRegistry("MaxGraphRows") == "")
		{
			MaxGraphRows = 1000;
		}
		else
		{
			MaxGraphRows = Convert.ToInt32(RegistryHandler.ReadFromRegistry("MaxGraphRows"));
		}

		UseExtendedEvents = RegistryHandler.ReadFromRegistry("UseExtendedEvents");

		if (UseExtendedEvents == "")
		{
			UseExtendedEvents = "";
		}

		CheckForUpdatesOnStart = RegistryHandler.ReadFromRegistry("CheckForUpdatesOnStart");

		if (CheckForUpdatesOnStart == "")
		{
			CheckForUpdatesOnStart = "True";
		}

		DatabaseName = RegistryHandler.ReadFromRegistry("DatabaseName");

		if (DatabaseName == "")
		{
			DatabaseName = "SQLEventAnalyzer";
		}

		UpdateServiceUrl = RegistryHandler.ReadFromRegistry("UpdateServiceUrl");

		if (UpdateServiceUrl == "")
		{
			UpdateServiceUrl = "http://virtcore.com/VirtcoreService.asmx";

			if (RegistryModifyAccess)
			{
				RegistryHandler.SaveToRegistry("UpdateServiceUrl", UpdateServiceUrl);
			}
		}
	}

	public static void SaveConfig()
	{
		if (RegistryModifyAccess)
		{
			RegistryHandler.SaveToRegistry("TextDataFontFamily", TextDataFontFamily);
			RegistryHandler.SaveToRegistry("TextDataFontSize", TextDataFontSize);
			RegistryHandler.SaveToRegistry("EditorFontFamily", EditorFontFamily);
			RegistryHandler.SaveToRegistry("EditorFontSize", EditorFontSize);
			RegistryHandler.SaveToRegistry("WindowSize", WindowSize);
			RegistryHandler.SaveToRegistry("SplitterDistance", SplitterDistance);
			RegistryHandler.SaveToRegistry("ColumnEditorWindowSize", ColumnEditorWindowSize);
			RegistryHandler.SaveToRegistry("ColumnEditorSplitterDistance", ColumnEditorSplitterDistance);
			RegistryHandler.SaveToRegistry("ColumnsWindowSize", ColumnsWindowSize);
			RegistryHandler.SaveToRegistry("ViewStatisticsWindowSize", ViewStatisticsWindowSize);
			RegistryHandler.SaveToRegistry("ViewRowWindowSize", ViewRowWindowSize);
			RegistryHandler.SaveToRegistry("TimelineWindowSize", TimelineWindowSize);
			RegistryHandler.SaveToRegistry("ItemsPerPage", ItemsPerPage.ToString());
			RegistryHandler.SaveToRegistry("Language", Language);
			RegistryHandler.SaveToRegistry("ShowHiddenColumns", ShowHiddenColumns.ToString());
			RegistryHandler.SaveToRegistry("RecordTraceFileDir", RecordTraceFileDir);
			RegistryHandler.SaveToRegistry("KeepSessionOnExit", KeepSessionOnExit.ToString());
			RegistryHandler.SaveToRegistry("EnableFileNameAndType", EnableFileNameAndType.ToString());
			RegistryHandler.SaveToRegistry("EnableQuickSearch", EnableQuickSearch.ToString());
			RegistryHandler.SaveToRegistry("AutoPopulateFilter2", AutoPopulateFilter2.ToString());
			RegistryHandler.SaveToRegistry("MaxGraphRows", MaxGraphRows.ToString());
			RegistryHandler.SaveToRegistry("UseExtendedEvents", UseExtendedEvents);
			RegistryHandler.SaveToRegistry("CheckForUpdatesOnStart", CheckForUpdatesOnStart);
			RegistryHandler.SaveToRegistry("DatabaseName", DatabaseName);
		}
	}

	public static bool ColumnTypeClrDependant(Column.ColumnType columnType)
	{
		if (columnType == Column.ColumnType.RegEx || columnType == Column.ColumnType.StoredProcedureName || columnType == Column.ColumnType.StoredProcedureParameter || columnType == Column.ColumnType.LogParameter)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
