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
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public class DatabaseOperation : Dal
{
	public delegate void DataSourceAllPagesReadyEventHandler(DataSet dataSet);
	public event DataSourceAllPagesReadyEventHandler DataSourceAllPagesReadyEvent;

	public delegate void ExportSessionToSessionReadyEventHandler(bool success);
	public event ExportSessionToSessionReadyEventHandler ExportSessionToSessionReadyEvent;

	private string _previousWhereIncludingSearch;
	private int _previousTotalRows = -1;
	private bool _forceRecalculateTotalRows;

	public void SetForceRecalculateTotalRows(bool value)
	{
		_forceRecalculateTotalRows = value;
	}

	public void UpdateDataSource(DataViewer dataViewer, string sqlFile, int page, int itemsPerPage, string sortingColumn, ListSortDirection sortingColumnDirection, string searchTerm, string searchColumn, Dictionary<string, string[]> visibleColumns, string where, string whereActive)
	{
		DataObject dataObject = GetDataObject(sqlFile, page, itemsPerPage, sortingColumn, sortingColumnDirection, dataViewer.GetSortMemory(), searchTerm, searchColumn, visibleColumns, where, whereActive);
		dataViewer.SetDataSource(dataObject.Page, dataObject.SortingColumn, dataObject.SortingColumnDirection, dataObject.DataSource, dataObject.SearchTerm, dataObject.SearchColumn, dataObject.Where, dataObject.WhereActive);
	}

	public void RequestDataSourceAllPages(DataViewer dataViewer, string sqlFile, int page, int itemsPerPage, string sortingColumn, ListSortDirection sortingColumnDirection, string searchTerm, string searchColumn, Dictionary<string, string[]> visibleColumns, string where, string whereActive)
	{
		DataObject dataObject = GetDataObject(sqlFile, page, itemsPerPage, sortingColumn, sortingColumnDirection, dataViewer.GetSortMemory(), searchTerm, searchColumn, visibleColumns, where, whereActive);
		FireDataSourceAllPagesReadyEvent(dataObject.DataSource);
	}

	public void ExportSessionToSession(string newSessionName, DataViewer dataViewer, string sqlFile, int page, int itemsPerPage, string sortingColumn, ListSortDirection sortingColumnDirection, string searchTerm, string searchColumn, Dictionary<string, string[]> visibleColumns, string where, string whereActive)
	{
		DataObject dataObject = GetDataObjectExportSessionToSession(newSessionName, sqlFile, page, itemsPerPage, sortingColumn, sortingColumnDirection, dataViewer.GetSortMemory(), searchTerm, searchColumn, visibleColumns, where, whereActive);

		bool success = false;

		DataTable dataTable = new DataTable();

		if (dataObject.DataSource != null && dataObject.DataSource.Tables.Count > 0)
		{
			dataTable = dataObject.DataSource.Tables[0];
		}

		if (dataTable != null && dataTable.Rows.Count > 0 && dataTable.Rows[0]["Completed"].ToString() == "1")
		{
			success = true;
		}

		if (!success)
		{
			DropTempTable(string.Format("TraceData_{0}", newSessionName));
		}

		FireExportSessionToSessionReadyEvent(success);
	}

	public void UpdateDataSourceStatistics(DataViewer dataViewer, string sqlFile, int page, int itemsPerPage, string sortingColumn, ListSortDirection sortingColumnDirection, string searchTerm, string searchColumn, Dictionary<string, string[]> visibleColumns, string where, string whereActive)
	{
		DataObject dataObject = GetDataObjectStatistics(sqlFile, page, itemsPerPage, sortingColumn, sortingColumnDirection, dataViewer.GetSortMemory(), searchTerm, searchColumn, visibleColumns, where, whereActive);
		dataViewer.SetDataSource(dataObject.Page, dataObject.SortingColumn, dataObject.SortingColumnDirection, dataObject.DataSource, dataObject.SearchTerm, dataObject.SearchColumn, dataObject.Where, dataObject.WhereActive);
	}

	public void RequestDataSourceAllPagesStatistics(DataViewer dataViewer, string sqlFile, int page, int itemsPerPage, string sortingColumn, ListSortDirection sortingColumnDirection, string searchTerm, string searchColumn, Dictionary<string, string[]> visibleColumns, string where, string whereActive)
	{
		DataObject dataObject = GetDataObjectStatistics(sqlFile, page, itemsPerPage, sortingColumn, sortingColumnDirection, dataViewer.GetSortMemory(), searchTerm, searchColumn, visibleColumns, where, whereActive);
		FireDataSourceAllPagesReadyEvent(dataObject.DataSource);
	}

	public string GetSqlServerDataDir()
	{
		DataTable dataTable = ExecuteDataTable("select substring(f.physical_name, 1, charindex(N'master.mdf', lower(f.physical_name)) - 1) DataDir from master.sys.master_files f where f.database_id = 1 and f.file_id = 1");

		if (dataTable.Rows.Count == 0)
		{
			return null;
		}
		else
		{
			return dataTable.Rows[0]["DataDir"].ToString();
		}
	}

	public bool StartTraceRecording(int traceId)
	{
		if (ConfigHandler.UseExtendedEvents == "False" || GetSqlServerVersion() < 11)
		{
			string sql = string.Format(SQLEventAnalyzer.Properties.Resources.StartTraceRecording, traceId, ConfigHandler.RecordingPrefix);
			return Execute(sql);
		}
		else
		{
			string sql = string.Format(SQLEventAnalyzer.Properties.Resources.StartEventSessionRecording, ConfigHandler.TraceFileName, ConfigHandler.RecordingPrefix);
			return Execute(sql);
		}
	}

	public bool StopDeleteTrace()
	{
		if (ConfigHandler.UseExtendedEvents == "False" || GetSqlServerVersion() < 11)
		{
			string traceFile = string.Format(@"{0}\{1}", ConfigHandler.RecordTraceFileDir.Replace("'", "''"), ConfigHandler.TraceFileName);
			string sql = string.Format(SQLEventAnalyzer.Properties.Resources.StopDeleteTrace, traceFile);
			return Execute(sql);
		}
		else
		{
			string sql = string.Format(SQLEventAnalyzer.Properties.Resources.StopEventSession, ConfigHandler.TraceFileName);
			return Execute(sql);
		}
	}

	public bool DropEventSession()
	{
		if (ConfigHandler.UseExtendedEvents == "True" || GetSqlServerVersion() >= 11)
		{
			string sql = string.Format(SQLEventAnalyzer.Properties.Resources.DropEventSession, ConfigHandler.TraceFileName);
			return Execute(sql);
		}

		return true;
	}

	public int CreateTrace()
	{
		string traceFile = string.Format(@"{0}\{1}", ConfigHandler.RecordTraceFileDir.Replace("'", "''"), ConfigHandler.TraceFileName);

		if (ConfigHandler.UseExtendedEvents == "False" || GetSqlServerVersion() < 11)
		{
			string sql = string.Format(SQLEventAnalyzer.Properties.Resources.CreateTrace, traceFile);

			DataTable dataTable = ExecuteDataTable(sql);

			int traceId = Convert.ToInt32(dataTable.Rows[0]["TraceID"].ToString());
			int returnCode = Convert.ToInt32(dataTable.Rows[0]["ReturnCode"].ToString());

			if (returnCode == 13) // Out of memory
			{
				traceId = -1;
			}

			return traceId;
		}
		else
		{
			string sql = string.Format(SQLEventAnalyzer.Properties.Resources.CreateEventSession, ConfigHandler.TraceFileName, traceFile, ConfigHandler.RecordingPrefix);

			bool success = Execute(sql);

			return Convert.ToInt32(success);
		}
	}

	public bool SetTraceStatus(int traceId, int status)
	{
		if (ConfigHandler.UseExtendedEvents == "False" || GetSqlServerVersion() < 11)
		{
			string sql = string.Format("{2}\r\nexec sp_trace_setstatus {0}, {1}", traceId, status, ConfigHandler.RecordingPrefix);
			return Execute(sql);
		}
		else
		{
			string state = "";

			if (status == 0)
			{
				state = "stop";
			}
			else if (status == 1)
			{
				state = "start";
			}

			string sql = string.Format("{2}\r\nalter event session [{0}] on server state = {1}", ConfigHandler.TraceFileName, state, ConfigHandler.RecordingPrefix);
			return Execute(sql);
		}
	}

	public void InitializeDatabaseActions()
	{
		CreateTempDatabase();
		EnableFullText();
	}

	public bool IsCLREnabled()
	{
		DataTable dt = ExecuteDataTable("select c.value from sys.configurations c where c.name = 'clr enabled'");

		if (dt.Rows[0]["value"].ToString() == "1")
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool DoesTempTableExist(string sessionId)
	{
		DataTable dt = ExecuteDataTable(string.Format(SQLEventAnalyzer.Properties.Resources.DoesTempTableExist, ConfigHandler.DatabaseName, sessionId.Replace("'", "''")));

		if (dt.Rows[0]["Result"].ToString() == "1")
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void RenameTable(string source, string dest)
	{
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.RenameTable, ConfigHandler.DatabaseName, source.Replace("'", "''"), dest.Replace("'", "''")));
	}

	public void CreateTempTable()
	{
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.CreateTempTable, ConfigHandler.DatabaseName, GenericHelper.TempTableName.Replace("'", "''"), GenericHelper.TempTableName));
	}

	public void DeleteSession(string sessionId)
	{
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.DeleteSession, sessionId, ConfigHandler.DatabaseName, sessionId.Replace("'", "''")));
	}

	public void CreateIndexes()
	{
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.CreateIndexes, ConfigHandler.DatabaseName, GenericHelper.TempTableName));
	}

	public void EnableColumnStoreIndex()
	{
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.EnableColumnStoreIndex, ConfigHandler.DatabaseName, GenericHelper.TempTableName.Replace("'", "''"), GenericHelper.TempTableName));
	}

	public void DisableColumnStoreIndex()
	{
		if (GetSqlServerVersion() >= 11 && IsColumnStoreSupported())
		{
			Execute(string.Format(SQLEventAnalyzer.Properties.Resources.DisableColumnStoreIndex, ConfigHandler.DatabaseName, GenericHelper.TempTableName.Replace("'", "''"), GenericHelper.TempTableName));
		}
	}

	public bool IsColumnStoreSupported()
	{
		DataTable dataTable = ExecuteDataTable("select serverproperty('Edition') Edition");

		if (dataTable.Rows.Count == 1)
		{
			string edition = dataTable.Rows[0]["Edition"].ToString();

			if (edition.StartsWith("Enterprise") || edition.StartsWith("Developer") || edition.StartsWith("Evaluation"))
			{
				return true;
			}
		}

		return false;
	}

	public void StopFullTextPopulation()
	{
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.StopFullTextPopulation, ConfigHandler.DatabaseName, GenericHelper.TempTableName));
	}

	public void StartFullTextPopulation()
	{
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.StartFullTextPopulation, ConfigHandler.DatabaseName, GenericHelper.TempTableName));
	}

	public void DropTempTable(string tempTableName)
	{
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.DropTempTable, ConfigHandler.DatabaseName, tempTableName.Replace("'", "''"), tempTableName));
	}

	public void KillConnections(string sessionIdToKill)
	{
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.KillConnections, sessionIdToKill.Replace("'", "''"), GenericHelper.ApplicationName));
	}

	public DataTable GetExistingConnections()
	{
		return ExecuteDataTable(string.Format(SQLEventAnalyzer.Properties.Resources.GetExistingConnections, ConfigHandler.DatabaseName, GenericHelper.ApplicationName));
	}

	public DataSet GetTraceDataInfo()
	{
		string enableFileNameAndType = "0";

		if (ConfigHandler.EnableFileNameAndType)
		{
			enableFileNameAndType = "1";
		}

		string sql = string.Format(SQLEventAnalyzer.Properties.Resources.GetTraceDataInfo, ConfigHandler.DatabaseName, GenericHelper.TempTableName, enableFileNameAndType);

		GetDataForm form = new GetDataForm();
		form.Initialize(this, sql);

		if (GenericHelper.IsUserInteractive())
		{
			form.ShowDialog();
		}

		DataSet ds = form.GetDataSet();

		return ds;
	}

	public DataTable GetSessionInfo(IWin32Window owner)
	{
		string sql = string.Format(SQLEventAnalyzer.Properties.Resources.GetSessionInfo, ConfigHandler.DatabaseName);

		GetDataForm form = new GetDataForm();
		form.Initialize(this, sql);

		if (GenericHelper.IsUserInteractive())
		{
			form.ShowDialog(owner);
		}

		return form.GetDataSet().Tables[0];
	}

	public DataTable GetNonDefaultColumnNames(string tableName)
	{
		string sql = string.Format(SQLEventAnalyzer.Properties.Resources.GetNonDefaultColumnNames, ConfigHandler.DatabaseName, tableName.Replace("'", "''"));
		return ExecuteDataTable(sql);
	}

	public string GetLastImportedFileName(string sessionId)
	{
		string sql = string.Format(SQLEventAnalyzer.Properties.Resources.GetLastImportedFileName, ConfigHandler.DatabaseName, sessionId.Replace("'", "''"));
		DataTable dataTable = ExecuteDataTable(sql);

		if (dataTable != null && dataTable.Rows.Count == 1)
		{
			return dataTable.Rows[0]["FileName"].ToString();
		}

		return null;
	}

	public DateTime GetLastEventStartTime()
	{
		string sql = string.Format(SQLEventAnalyzer.Properties.Resources.GetLastEventStartTime, ConfigHandler.DatabaseName, GenericHelper.TempTableName);
		DataTable dataTable = ExecuteDataTable(sql);

		if (dataTable != null && dataTable.Rows.Count == 1)
		{
			return Convert.ToDateTime(dataTable.Rows[0]["StartTime"]);
		}

		return new DateTime();
	}

	public bool ImportTraceFile(string fileName)
	{
		string fileNameExtension = Path.GetExtension(fileName);
		bool success = false;

		if (fileNameExtension == ".trc")
		{
			success = ImportSqlTraceFile(fileName);
		}
		else if (fileNameExtension == ".xel")
		{
			success = ImportExtendedEventsFile(fileName);
		}

		return success;
	}

	public bool ImportSession(string sessionId, List<string> nonDefaultColumns)
	{
		bool success = Execute(string.Format(SQLEventAnalyzer.Properties.Resources.ImportSession, ConfigHandler.DatabaseName, sessionId, GenericHelper.TempTableName, PrefixFormat(nonDefaultColumns, ""), PrefixFormat(nonDefaultColumns, "t.")));
		return success;
	}

	public void HandleCLR()
	{
		bool clrEnabled = IsCLREnabled();

		if (!clrEnabled)
		{
			EnableCLR();
		}
	}

	public void DeployCLR()
	{
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.HandleCLR, ConfigHandler.DatabaseName));
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.CreateGetRegEx));
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.CreateMatchRegEx));
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.CreateGetStoredProcedureName));
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.CreateMatchStoredProcedureName));
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.CreateGetStoredProcedureParameter));
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.CreateMatchStoredProcedureParameter));
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.CreateGetLogParameter));
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.CreateMatchLogParameter));
	}

	public void DisableCLR()
	{
		Execute("sp_configure 'show advanced options', 1");
		Execute("reconfigure");
		Execute("sp_configure 'clr enabled', 0");
		Execute("reconfigure");
	}

	public DataSet GetGraphData(Dictionary<string, string[]> visibleColumns)
	{
		string whereIncludingSearch = "";

		string combinedWhere = GetWhere(visibleColumns);

		if (combinedWhere.Length > 0)
		{
			whereIncludingSearch = string.Format("and ({0})", combinedWhere);
		}

		string sql = string.Format(SQLEventAnalyzer.Properties.Resources.GetGraphData
			, ConfigHandler.DatabaseName                                                    // {0}
			, whereIncludingSearch                                                          // {1}
			, GetGraphColumnNames("t")                                                      // {2}
			, GenericHelper.TempTableName                                                   // {3}
			, ConfigHandler.MaxGraphRows                                                    // {4}
			, GetContains(visibleColumns)                                                   // {5}
			, GetNotContains(visibleColumns)                                                // {6}
			, GetContainsDropTable(visibleColumns)                                          // {7}
			, GetNotContainsDropTable(visibleColumns)                                       // {8}
		);

		GetDataForm form = new GetDataForm();
		form.Initialize(this, sql);
		form.ShowDialog();

		DataSet ds = form.GetDataSet();

		return ds;
	}

	private bool ImportSqlTraceFile(string fileName)
	{
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName).Replace("'", "''");
		string fileNameWithExtension = Path.GetFileName(fileName).Replace("'", "''");
		string traceFile = string.Format(@"{0}\{1}", Path.GetDirectoryName(fileName).Replace("'", "''"), fileNameWithoutExtension);
		bool success = Execute(string.Format(SQLEventAnalyzer.Properties.Resources.ImportTraceFile, ConfigHandler.DatabaseName, traceFile, fileNameWithExtension, GenericHelper.TempTableName));

		return success;
	}

	private bool ImportExtendedEventsFile(string fileName)
	{
		string fileNameWithExtension = Path.GetFileName(fileName).Replace("'", "''");
		string traceFile = string.Format(@"{0}\{1}", Path.GetDirectoryName(fileName).Replace("'", "''"), fileNameWithExtension);
		bool success = Execute(string.Format(SQLEventAnalyzer.Properties.Resources.ImportExtendedEventsFile, ConfigHandler.DatabaseName, traceFile, fileNameWithExtension, GenericHelper.TempTableName));

		return success;
	}

	private void EnableCLR()
	{
		Execute("sp_configure 'show advanced options', 1");
		Execute("reconfigure");
		Execute("sp_configure 'clr enabled', 1");
		Execute("reconfigure");
	}

	private void CreateTempDatabase()
	{
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.CreateDatabase, ConfigHandler.DatabaseName));
	}

	private void EnableFullText()
	{
		Execute(string.Format(SQLEventAnalyzer.Properties.Resources.EnableFullText, ConfigHandler.DatabaseName), true);
	}

	private DataSet GetData(string fileName, int page, int pageSize, List<string[]> sortMemory, Dictionary<string, string[]> visibleColumns, string whereIncludingSearch)
	{
		string contains = GetContains(visibleColumns);
		string notContains = GetNotContains(visibleColumns);

		string recalculateTotalRows;

		if (string.Format("{0}{1}{2}", whereIncludingSearch, contains, notContains) == _previousWhereIncludingSearch && !_forceRecalculateTotalRows)
		{
			recalculateTotalRows = "0";
		}
		else
		{
			recalculateTotalRows = "1";
			_previousWhereIncludingSearch = string.Format("{0}{1}{2}", whereIncludingSearch, contains, notContains);
		}

		string sql = string.Format(GenericHelper.GetSql(fileName)
			, page                                                                          // {0}
			, pageSize                                                                      // {1}
			, GetOrderBy(sortMemory)                                                        // {2}
			, whereIncludingSearch                                                          // {3}
			, contains                                                                      // {4}
			, notContains                                                                   // {5}
			, GetContainsDropTable(visibleColumns)                                          // {6}
			, GetNotContainsDropTable(visibleColumns)                                       // {7}
			, ConfigHandler.DatabaseName                                                    // {8}
			, GenericHelper.TempTableName                                                   // {9}
			, GetSortingColumns(sortMemory)                                                 // {10}
			, GetColumnsToReturn(visibleColumns)                                            // {11}
			, recalculateTotalRows                                                          // {12}
			, _previousTotalRows                                                            // {13}
		);

		GetDataForm form = new GetDataForm();
		form.Initialize(this, sql);

		if (GenericHelper.IsUserInteractive())
		{
			form.ShowDialog();
		}

		DataSet ds = form.GetDataSet();

		if (recalculateTotalRows == "1")
		{
			_previousTotalRows = Convert.ToInt32(ds.Tables[1].Rows[0]["TotalRows"]);
			_forceRecalculateTotalRows = false;
		}

		return ds;
	}

	private DataSet GetDataExportSessionToSession(string newSessionName, string fileName, int page, int pageSize, List<string[]> sortMemory, Dictionary<string, string[]> visibleColumns, string whereIncludingSearch)
	{
		string contains = GetContains(visibleColumns);
		string notContains = GetNotContains(visibleColumns);

		string allPages = "";

		if (page == -1)
		{
			allPages = "--";
		}

		string objectScriptConstraints = GetObjectScriptConstraints(newSessionName);
		string objectScriptIndexes = GetObjectScriptIndexes(newSessionName);

		string sql = string.Format(GenericHelper.GetSql(fileName)
			, page                                                                          // {0}
			, pageSize                                                                      // {1}
			, GetOrderBy(sortMemory)                                                        // {2}
			, whereIncludingSearch                                                          // {3}
			, contains                                                                      // {4}
			, notContains                                                                   // {5}
			, GetContainsDropTable(visibleColumns)                                          // {6}
			, GetNotContainsDropTable(visibleColumns)                                       // {7}
			, ConfigHandler.DatabaseName                                                    // {8}
			, GenericHelper.TempTableName                                                   // {9}
			, GetSortingColumns(sortMemory)                                                 // {10}
			, allPages                                                                      // {11}
			, newSessionName                                                                // {12}
			, newSessionName.Replace("'", "''")                                             // {13}
			, objectScriptConstraints                                                       // {14}
			, objectScriptIndexes                                                           // {15}
		);

		GetDataForm form = new GetDataForm();
		form.Initialize(this, sql);

		if (GenericHelper.IsUserInteractive())
		{
			form.ShowDialog();
		}

		DataSet ds = form.GetDataSet();

		return ds;
	}

	private DataSet GetDataStatistics(string fileName, int page, int pageSize, List<string[]> sortMemory, Dictionary<string, string[]> visibleColumns, string whereIncludingSearch)
	{
		string sql = string.Format(GenericHelper.GetSql(fileName)
			, page                                                                          // {0}
			, pageSize                                                                      // {1}
			, GetOrderBy(sortMemory)                                                        // {2}
			, whereIncludingSearch                                                          // {3}
			, GetContains(visibleColumns)                                                   // {4}
			, GetNotContains(visibleColumns)                                                // {5}
			, GetContainsDropTable(visibleColumns)                                          // {6}
			, GetNotContainsDropTable(visibleColumns)                                       // {7}
			, ConfigHandler.DatabaseName                                                    // {8}
			, GenericHelper.TempTableName                                                   // {9}
			, GetSortingColumns(sortMemory)                                                 // {10}
			, GetGroupColumnNamesStatistics("t")                                            // {11}
			, GetColumnsToReturnStatistics(visibleColumns, "v")                             // {12}
			, GetColumnsToReturnStatistics(visibleColumns, "r")                             // {13}
		);

		GetDataForm form = new GetDataForm();
		form.Initialize(this, sql);

		if (GenericHelper.IsUserInteractive())
		{
			form.ShowDialog();
		}

		DataSet ds = form.GetDataSet();

		return ds;
	}

	private static string GetObjectScriptConstraints(string newSessionName)
	{
		StringBuilder sb = new StringBuilder();

		if (ColumnHelper.EnabledColumns.Count > 0)
		{
			for (int i = 0; i < ColumnHelper.EnabledColumns.Count; i++)
			{
				sb.AppendLine(string.Format("alter table dbo.[TraceData_{1}] add constraint [DF_TraceData_{1}_{0}] default ('') for [{0}]", ColumnHelper.EnabledColumns[i].Name, newSessionName));
			}
		}

		return sb.ToString();
	}

	private string GetObjectScriptIndexes(string newSessionName)
	{
		string targetName = string.Format("TraceData_{0}", newSessionName);

		DataTable dt = ExecuteDataTable(string.Format(GenericHelper.GetSql("GetObjectScript_IX.sql"), ConfigHandler.DatabaseName, GenericHelper.TempTableName.Replace("'", "''"), targetName.Replace("'", "''")));

		if (dt != null && dt.Rows.Count > 0)
		{
			return dt.Rows[0]["ObjectScript"].ToString();
		}

		return "";
	}

	private static string GetColumnsToReturn(Dictionary<string, string[]> visibleColumns)
	{
		StringBuilder sb = new StringBuilder();

		foreach (KeyValuePair<string, string[]> column in visibleColumns)
		{
			if (column.Value[4] == "SearchableShow" || column.Value[4] == "NonSearchableShow" || (column.Value[4] == "SearchableHide" && ConfigHandler.ShowHiddenColumns))
			{
				string prefix = column.Value[1];
				string prefixAndColumn = string.Format(", {0}.[{1}]", prefix, column.Key);

				sb.Append(prefixAndColumn);
			}
		}

		return sb.ToString().Substring(2, sb.ToString().Length - 2);
	}

	private static string GetColumnsToReturnStatistics(Dictionary<string, string[]> visibleColumns, string customPrefix)
	{
		StringBuilder sb = new StringBuilder();

		foreach (KeyValuePair<string, string[]> column in visibleColumns)
		{
			if (column.Value[4] == "SearchableShow" || column.Value[4] == "NonSearchableShow")
			{
				string prefix = customPrefix;

				if (customPrefix == null)
				{
					prefix = column.Value[1];
				}

				string prefixAndColumn = string.Format(", {0}.[{1}]", prefix, column.Key);

				sb.Append(prefixAndColumn);
			}
		}

		return sb.ToString().Substring(2, sb.ToString().Length - 2);
	}

	private static string GetOrderBy(List<string[]> sortMemory)
	{
		StringBuilder sb = new StringBuilder();

		for (int i = 0; i <= sortMemory.Count - 1; i++)
		{
			if (i > 0)
			{
				sb.Append(", ");
			}

			sb.Append(string.Format("v.[{0}]{1}", sortMemory[i][1], sortMemory[i][2]));
		}

		return sb.ToString();
	}

	private static string GetGraphColumnNames(string prefix)
	{
		StringBuilder sb = new StringBuilder();

		foreach (Column column in ColumnHelper.EnabledColumns)
		{
			sb.Append(string.Format(", {0}.[{1}]", prefix, column.Name));
		}

		return sb.ToString();
	}

	private static string GetGroupColumnNamesStatistics(string prefix)
	{
		StringBuilder sb = new StringBuilder();

		foreach (string column in ConfigHandler.GroupByColumns)
		{
			sb.Append(string.Format(", {0}.[{1}]", prefix, column));
		}

		return sb.ToString();
	}

	private void FireDataSourceAllPagesReadyEvent(DataSet dataSet)
	{
		if (DataSourceAllPagesReadyEvent != null)
		{
			DataSourceAllPagesReadyEvent(dataSet);
		}
	}

	private void FireExportSessionToSessionReadyEvent(bool success)
	{
		if (ExportSessionToSessionReadyEvent != null)
		{
			ExportSessionToSessionReadyEvent(success);
		}
	}

	private static string PrefixFormat(List<string> columns, string prefix)
	{
		StringBuilder sb = new StringBuilder();

		foreach (string column in columns)
		{
			sb.Append(string.Format(", {0}[{1}]", prefix, column));
		}

		return sb.ToString();
	}

	private DataObject GetDataObject(string sqlFile, int page, int itemsPerPage, string sortingColumn, ListSortDirection sortingColumnDirection, List<string[]> sortMemory, string searchTerm, string searchColumn, Dictionary<string, string[]> visibleColumns, string where, string whereActive)
	{
		AddDataGridSearchToFilter(searchTerm, searchColumn);

		string whereIncludingSearch = GetWhereIncludingSearch(visibleColumns, where, whereActive);

		DataSet dataSource = GetData(sqlFile, page, itemsPerPage, sortMemory, visibleColumns, whereIncludingSearch);

		DataObject dataObject = new DataObject();

		dataObject.Page = page;
		dataObject.SortingColumn = sortingColumn;
		dataObject.SortingColumnDirection = sortingColumnDirection;
		dataObject.DataSource = dataSource;
		dataObject.SearchTerm = searchTerm;
		dataObject.SearchColumn = searchColumn;
		dataObject.Where = where;
		dataObject.WhereActive = whereActive;

		return dataObject;
	}

	private DataObject GetDataObjectExportSessionToSession(string newSessionName, string sqlFile, int page, int itemsPerPage, string sortingColumn, ListSortDirection sortingColumnDirection, List<string[]> sortMemory, string searchTerm, string searchColumn, Dictionary<string, string[]> visibleColumns, string where, string whereActive)
	{
		AddDataGridSearchToFilter(searchTerm, searchColumn);

		string whereIncludingSearch = GetWhereIncludingSearch(visibleColumns, where, whereActive);

		DataSet dataSource = GetDataExportSessionToSession(newSessionName, sqlFile, page, itemsPerPage, sortMemory, visibleColumns, whereIncludingSearch);

		DataObject dataObject = new DataObject();

		dataObject.Page = page;
		dataObject.SortingColumn = sortingColumn;
		dataObject.SortingColumnDirection = sortingColumnDirection;
		dataObject.DataSource = dataSource;
		dataObject.SearchTerm = searchTerm;
		dataObject.SearchColumn = searchColumn;
		dataObject.Where = where;
		dataObject.WhereActive = whereActive;

		return dataObject;
	}

	private DataObject GetDataObjectStatistics(string sqlFile, int page, int itemsPerPage, string sortingColumn, ListSortDirection sortingColumnDirection, List<string[]> sortMemory, string searchTerm, string searchColumn, Dictionary<string, string[]> visibleColumns, string where, string whereActive)
	{
		AddDataGridSearchToFilter(searchTerm, searchColumn);

		string whereIncludingSearch = GetWhereIncludingSearch(visibleColumns, where, whereActive);

		DataSet dataSource = GetDataStatistics(sqlFile, page, itemsPerPage, sortMemory, visibleColumns, whereIncludingSearch);

		DataObject dataObject = new DataObject();

		dataObject.Page = page;
		dataObject.SortingColumn = sortingColumn;
		dataObject.SortingColumnDirection = sortingColumnDirection;
		dataObject.DataSource = dataSource;
		dataObject.SearchTerm = searchTerm;
		dataObject.SearchColumn = searchColumn;
		dataObject.Where = where;
		dataObject.WhereActive = whereActive;

		return dataObject;
	}

	private class DataObject
	{
		public int Page;
		public string SortingColumn;
		public ListSortDirection SortingColumnDirection;
		public DataSet DataSource;
		public string SearchTerm;
		public string SearchColumn;
		public string Where;
		public string WhereActive;
	}

	private class FullTextObject
	{
		public string Contains;
		public string ContainsFullTextJoin;
		public string NotContains;
		public string NotContainsFullTextJoin;
	}

	private static void AddDataGridSearchToFilter(string searchTerm, string searchColumn)
	{
		int existingFilter = -1;

		for (int i = 0; i < ConfigHandler.ActiveFilters.Count; i++)
		{
			if (ConfigHandler.ActiveFilters[i].DataGridSearch)
			{
				existingFilter = i;
				break;
			}
		}

		if (existingFilter != -1)
		{
			ConfigHandler.ActiveFilters.RemoveAt(existingFilter);
		}

		if (searchTerm != "")
		{
			Filter filter = new Filter();
			filter.Column = searchColumn;
			filter.Value = searchTerm;
			filter.AndOr = "and";
			filter.DataGridSearch = true;
			filter.Operator = "=";
			filter.ParanthesBegin = "(";
			filter.ParanthesEnd = ")";

			ConfigHandler.ActiveFilters.Add(filter);
		}
	}

	private static string GetWhereIncludingSearch(Dictionary<string, string[]> visibleColumns, string where, string whereActive)
	{
		string combinedWhere = GetWhere(visibleColumns);

		string whereIncludingSearch = where;

		if (combinedWhere.Length > 0)
		{
			if (where.Length == 0)
			{
				whereIncludingSearch = string.Format("and ({0})", combinedWhere);
			}
			else
			{
				whereIncludingSearch = string.Format("{0} and ({1})", where, combinedWhere);
			}
		}

		if (whereActive != "")
		{
			if (whereIncludingSearch != "")
			{
				whereIncludingSearch = string.Format("{0} {1}", whereIncludingSearch, whereActive);
			}
			else
			{
				whereIncludingSearch = whereActive;
			}
		}

		return whereIncludingSearch;
	}

	private static string GetSortingColumns(List<string[]> sortMemory)
	{
		StringBuilder sb = new StringBuilder();

		for (int i = 0; i <= sortMemory.Count - 1; i++)
		{
			if (i > 0)
			{
				sb.Append(", ");
			}

			sb.Append(string.Format("{0}.[{1}]", sortMemory[i][0], sortMemory[i][1]));
		}

		return sb.ToString();
	}

	private static string GetFullTextJoinType(Dictionary<string, string[]> visibleColumns, string filterOperator)
	{
		if (filterOperator == "!=" || AnyFullTextOrOperators(visibleColumns))
		{
			return "left";
		}

		return "inner";
	}

	private static bool AnyFullTextOrOperators(Dictionary<string, string[]> visibleColumns)
	{
		int numberOfFullTextSearches = 0;
		int numberOfOrOperators = 0;

		foreach (Filter filter in ConfigHandler.ActiveFilters)
		{
			List<string> filterValues = SplitFilterValue(filter.Value, filter.Operator);

			foreach (string filterValueUntrimmed in filterValues)
			{
				string filterValue = filterValueUntrimmed.Trim();

				foreach (KeyValuePair<string, string[]> column in visibleColumns)
				{
					if (filter.Column == column.Key)
					{
						string dataType = visibleColumns[filter.Column][2];

						if (dataType == "FullText")
						{
							if (UseFullText(filterValue, filter) && (filter.Operator == "=" || filter.Operator == "!="))
							{
								numberOfFullTextSearches++;

								if (filter.AndOr == "or" && numberOfFullTextSearches > 1)
								{
									numberOfOrOperators++;
								}
							}
						}
					}
				}
			}
		}

		if (numberOfFullTextSearches > 1 && numberOfOrOperators > 0)
		{
			return true;
		}

		return false;
	}

	private static string GetFullTextJoin(string joinType, int filterNumber, string dataType, string filterColumn, string filterValue)
	{
		string join = "";

		if (dataType == "FullText")
		{
			join = string.Format("\r\n{3} join containstable(dbo.[{4}], [{0}], '\"{1}\"') ft{2} on ft{2}.[key] = t.ID", filterColumn, filterValue, filterNumber, joinType, GenericHelper.TempTableName.Replace("'", "''"));
		}

		return join;
	}

	private static FullTextObject GetFullTextObject(Dictionary<string, string[]> visibleColumns)
	{
		StringBuilder contains = new StringBuilder();
		StringBuilder containsFullTextJoin = new StringBuilder();
		StringBuilder notContains = new StringBuilder();
		StringBuilder notContainsFullTextJoin = new StringBuilder();

		int filterNumber = 0;

		foreach (Filter filter in ConfigHandler.ActiveFilters)
		{
			List<string> filterValues = SplitFilterValue(filter.Value, filter.Operator);

			foreach (string filterValueUntrimmed in filterValues)
			{
				string filterValue = filterValueUntrimmed.Trim();

				bool useFullText = false;

				string filterValueFullText = filterValue;

				if (filterValueFullText != "")
				{
					filterNumber++;
				}

				string dataType = visibleColumns[filter.Column][2];

				if (dataType == "FullText")
				{
					if (UseFullText(filterValue, filter) && (filter.Operator == "=" || filter.Operator == "!="))
					{
						useFullText = true;
						string joinType = GetFullTextJoinType(visibleColumns, filter.Operator);

						if (filter.Operator == "!=")
						{
							if (filterValueFullText != "")
							{
								notContainsFullTextJoin.Append(GetFullTextJoin(joinType, filterNumber, dataType, filter.Column, filterValueFullText.Replace("'", "''").Replace("%", "*")));
							}
						}
						else
						{
							if (filterValueFullText != "")
							{
								containsFullTextJoin.Append(GetFullTextJoin(joinType, filterNumber, dataType, filter.Column, filterValueFullText.Replace("'", "''").Replace("%", "*")));
							}
						}
					}
				}

				if (useFullText)
				{
					if (filter.Operator == "!=")
					{
						if (notContains.Length > 0)
						{
							notContains.Append(" or ");
						}

						if (notContainsFullTextJoin.Length > 0)
						{
							notContains.Append(string.Format("ft{0}.[key] is not null", filterNumber));
						}
					}
					else
					{
						if (contains.Length > 0)
						{
							contains.Append(string.Format(" {0} ", filter.AndOr));
						}

						if (containsFullTextJoin.Length > 0)
						{
							contains.Append(string.Format("ft{0}.[key] is not null", filterNumber));
						}
					}
				}
			}
		}

		if (containsFullTextJoin.Length > 0)
		{
			containsFullTextJoin.Remove(0, 2);
		}

		if (notContainsFullTextJoin.Length > 0)
		{
			notContainsFullTextJoin.Remove(0, 2);
		}

		FullTextObject fullTextObject = new FullTextObject();
		fullTextObject.Contains = contains.ToString();
		fullTextObject.ContainsFullTextJoin = containsFullTextJoin.ToString();
		fullTextObject.NotContains = notContains.ToString();
		fullTextObject.NotContainsFullTextJoin = notContainsFullTextJoin.ToString();

		return fullTextObject;
	}

	private static string GetContains(Dictionary<string, string[]> visibleColumns)
	{
		FullTextObject fullTextObject = GetFullTextObject(visibleColumns);

		string sql = "";

		if (fullTextObject.Contains != "")
		{
			sql = string.Format("\r\nselect t.ID\r\ninto dbo.#contains\r\nfrom dbo.[{2}] t\r\n{1}\r\nwhere {0}\r\n", fullTextObject.Contains, fullTextObject.ContainsFullTextJoin, GenericHelper.TempTableName.Replace("'", "''"));
		}

		return sql;
	}

	private static string GetNotContains(Dictionary<string, string[]> visibleColumns)
	{
		FullTextObject fullTextObject = GetFullTextObject(visibleColumns);

		string sql = "";

		if (fullTextObject.NotContains != "")
		{
			sql = string.Format("\r\nselect t.ID\r\ninto dbo.#notcontains\r\nfrom dbo.[{2}] t\r\n{1}\r\nwhere {0}\r\n", fullTextObject.NotContains, fullTextObject.NotContainsFullTextJoin, GenericHelper.TempTableName.Replace("'", "''"));
		}

		return sql;
	}

	private static string GetContainsDropTable(Dictionary<string, string[]> visibleColumns)
	{
		FullTextObject fullTextObject = GetFullTextObject(visibleColumns);

		StringBuilder sb = new StringBuilder();

		if (fullTextObject.Contains != "")
		{
			sb.Append("\r\ndrop table dbo.#contains");
		}

		return sb.ToString();
	}

	private static string GetNotContainsDropTable(Dictionary<string, string[]> visibleColumns)
	{
		FullTextObject fullTextObject = GetFullTextObject(visibleColumns);

		StringBuilder sb = new StringBuilder();

		if (fullTextObject.NotContains != "")
		{
			sb.Append("\r\ndrop table dbo.#notcontains");
		}

		return sb.ToString();
	}

	private static string FormatListValue(string inputValue)
	{
		StringBuilder sb = new StringBuilder();

		string[] values = inputValue.Split(',');

		for (int i = 0; i < values.Length; i++)
		{
			if (i > 0)
			{
				sb.Append(",");
			}

			string value = string.Format("'{0}'", values[i].Trim());
			sb.Append(value);
		}

		return sb.ToString();
	}

	private static string TypeCastIntegerList(string values)
	{
		StringBuilder sb = new StringBuilder();

		for (int i = 0; i < values.Split(',').Length; i++)
		{
			string value = values.Split(',')[i];

			if (value.Length > 0)
			{
				sb.AppendFormat("convert(int, {0})", value.Trim());
			}
			else
			{
				sb.Append("null");
			}

			if (i < values.Split(',').Length - 1)
			{
				sb.Append(",");
			}
		}

		return sb.ToString();
	}

	private static string CleanEmptyIntegerListValue(string value)
	{
		bool c = true;

		while (c)
		{
			if (value.StartsWith(","))
			{
				value = value.Substring(1, value.Length - 1);
				continue;
			}

			if (value.EndsWith(","))
			{
				value = value.Substring(0, value.Length - 1);
				continue;
			}

			if (value.Contains(",,"))
			{
				value = value.Replace(",,", ",");
				continue;
			}

			c = false;
		}

		return value;
	}

	private static string GetWhere(Dictionary<string, string[]> visibleColumns)
	{
		StringBuilder combinedWhere = new StringBuilder();
		bool containsAdded = false;
		bool notContainsAdded = false;
		bool filter1StartParanthesAdded = false;
		bool filter2StartParanthesAdded = false;
		bool filter1EndParanthesAdded = false;
		bool filter2EndParanthesAdded = false;

		foreach (Filter filter in ConfigHandler.ActiveFilters)
		{
			List<string> filterValues = SplitFilterValue(filter.Value, filter.Operator);

			foreach (string filterValueUntrimmed in filterValues)
			{
				string filterValue = filterValueUntrimmed.Trim();

				bool useFullText = false;
				bool searchForDate = false;
				bool searchForInteger = false;
				string operatorText = filter.Operator;

				string searchColumnPrefix = visibleColumns[filter.Column][1];
				string searchDatabaseColumn = string.Format("{0}.[{1}]", searchColumnPrefix, filter.Column);

				string dataType = visibleColumns[filter.Column][2];

				if (dataType == "Integer")
				{
					searchForInteger = true;
				}
				else if (dataType == "DateTime")
				{
					searchForDate = true;
				}
				else if (dataType == "FullText")
				{
					if (UseFullText(filterValue, filter) && (filter.Operator == "=" || filter.Operator == "!="))
					{
						useFullText = true;
					}
				}

				if (!useFullText)
				{
					if ((filterValue.Contains("*") || filterValue.Contains("%")) && (filter.Operator == "=" || filter.Operator == "!="))
					{
						if (filter.Operator == "!=")
						{
							operatorText = "not like";
						}
						else
						{
							operatorText = "like";
						}
					}
				}

				if (combinedWhere.Length > 0 && !useFullText)
				{
					if (filter.Filter1Search && filter2StartParanthesAdded && !filter2EndParanthesAdded)
					{
						combinedWhere.Append(")");
						filter2EndParanthesAdded = true;
					}
					else if (filter.Filter2Search && filter1StartParanthesAdded && !filter1EndParanthesAdded)
					{
						combinedWhere.Append(")");
						filter1EndParanthesAdded = true;
					}

					combinedWhere.Append(string.Format(" {0} ", filter.AndOr));
				}

				if (!useFullText)
				{
					if (filter.Filter1Search && !filter1StartParanthesAdded)
					{
						combinedWhere.Append("(");
						filter1StartParanthesAdded = true;
					}
					else if (filter.Filter2Search && !filter2StartParanthesAdded)
					{
						combinedWhere.Append("(");
						filter2StartParanthesAdded = true;
					}

					combinedWhere.Append(string.Format("{0}", filter.ParanthesBegin));
				}

				if (useFullText)
				{
					string filterValueFullText = filterValue;

					if (filter.Operator == "!=")
					{
						if (!containsAdded)
						{
							if (combinedWhere.Length > 0)
							{
								combinedWhere.Append(string.Format(" {0} ", filter.AndOr));
							}

							if (filterValueFullText != "")
							{
								combinedWhere.Append(string.Format("{0}.Id not in (select nc.Id from dbo.#notcontains nc)", searchColumnPrefix));
							}

							containsAdded = true;
						}
					}
					else
					{
						if (!notContainsAdded)
						{
							if (combinedWhere.Length > 0)
							{
								combinedWhere.Append(string.Format(" {0} ", filter.AndOr));
							}

							if (filterValueFullText != "")
							{
								combinedWhere.Append(string.Format("{0}.Id in (select c.Id from dbo.#contains c)", searchColumnPrefix));
							}

							notContainsAdded = true;
						}
					}
				}
				else if (searchForDate)
				{
					if (filter.DataGridSearch && !ConfigHandler.UseDateSelectorInQuickSearch)
					{
						combinedWhere.Append(string.Format("convert(varchar({4}), {0}, {3}) {2} '{1}'", searchDatabaseColumn, filterValue.Replace("'", "''").Replace("*", "%"), operatorText, ConfigHandler.DateTimeLongSqlFormat, ConfigHandler.DateTimeLongFormat.Length));
					}
					else
					{
						string granularity = "ss";

						if (filter.DataGridSearch)
						{
							granularity = ConfigHandler.DateFormatSearchGranularity;
						}

						string dateTimeSearchValue;

						try
						{
							dateTimeSearchValue = GenericHelper.DateTimeToSqlDateTime(Convert.ToDateTime(filterValue)); // normal DateTime value
						}
						catch
						{
							dateTimeSearchValue = filterValue; // dynamic DateTime value
						}

						if (filter.Operator == "!=")
						{
							combinedWhere.Append(string.Format("isnull(datediff({3}, {1}, {0}), -1) {2} 0", searchDatabaseColumn, dateTimeSearchValue, operatorText, granularity));
						}
						else
						{
							combinedWhere.Append(string.Format("datediff({3}, {1}, {0}) {2} 0", searchDatabaseColumn, dateTimeSearchValue, operatorText, granularity));
						}
					}
				}
				else if (searchForInteger && !filterValue.Contains("*") && !filterValue.Contains("%"))
				{
					if (filter.Operator == "in")
					{
						if (filterValue.Contains(",,") || filterValue.Length == 0 || filterValue.StartsWith(",") || filterValue.EndsWith(","))
						{
							combinedWhere.Append(string.Format("({0} {2} ({1}) or {0} is null)", searchDatabaseColumn, TypeCastIntegerList(CleanEmptyIntegerListValue(filterValue)), operatorText));
						}
						else
						{
							combinedWhere.Append(string.Format("{0} {2} ({1})", searchDatabaseColumn, TypeCastIntegerList(filterValue), operatorText));
						}
					}
					else if (filter.Operator == "not in")
					{
						if (filterValue.Contains(",,") || filterValue.Length == 0 || filterValue.StartsWith(",") || filterValue.EndsWith(","))
						{
							combinedWhere.Append(string.Format("({0} {2} ({1}) and {0} is not null)", searchDatabaseColumn, TypeCastIntegerList(CleanEmptyIntegerListValue(filterValue)), operatorText));
						}
						else
						{
							combinedWhere.Append(string.Format("{0} {2} ({1})", searchDatabaseColumn, TypeCastIntegerList(filterValue), operatorText));
						}
					}
					else if (filter.Operator == "!=")
					{
						combinedWhere.Append(string.Format("isnull({0}, '') {2} convert(int, {1})", searchDatabaseColumn, filterValue, operatorText));
					}
					else
					{
						if (filterValue.Length == 0)
						{
							combinedWhere.Append(string.Format("isnull({0}, '') {1} ''", searchDatabaseColumn, operatorText));
						}
						else
						{
							combinedWhere.Append(string.Format("{0} {2} convert(int, {1})", searchDatabaseColumn, filterValue, operatorText));
						}
					}
				}
				else
				{
					if (filter.Operator == "in")
					{
						if (filterValue.Contains(",,") || filterValue.Length == 0 || filterValue.StartsWith(",") || filterValue.EndsWith(","))
						{
							combinedWhere.Append(string.Format("({0} {2} ({1}) or {0} is null)", searchDatabaseColumn, FormatListValue(filterValue.Replace("'", "''").Replace("*", "%")), operatorText));
						}
						else
						{
							combinedWhere.Append(string.Format("{0} {2} ({1})", searchDatabaseColumn, FormatListValue(filterValue.Replace("'", "''").Replace("*", "%")), operatorText));
						}
					}
					else if (filter.Operator == "not in")
					{
						if (filterValue.Contains(",,") || filterValue.Length == 0 || filterValue.StartsWith(",") || filterValue.EndsWith(","))
						{
							combinedWhere.Append(string.Format("({0} {2} ({1}) and {0} is not null)", searchDatabaseColumn, FormatListValue(filterValue.Replace("'", "''").Replace("*", "%")), operatorText));
						}
						else
						{
							combinedWhere.Append(string.Format("{0} {2} ({1})", searchDatabaseColumn, FormatListValue(filterValue.Replace("'", "''").Replace("*", "%")), operatorText));
						}
					}
					else if (filter.Operator == "!=")
					{
						combinedWhere.Append(string.Format("isnull({0}, '') {2} '{1}'", searchDatabaseColumn, filterValue.Replace("'", "''").Replace("*", "%"), operatorText));
					}
					else
					{
						if (filterValue.Length == 0)
						{
							combinedWhere.Append(string.Format("isnull({0}, '') {1} ''", searchDatabaseColumn, operatorText));
						}
						else
						{
							combinedWhere.Append(string.Format("{0} {2} '{1}'", searchDatabaseColumn, filterValue.Replace("'", "''").Replace("*", "%"), operatorText));
						}
					}
				}

				if (!useFullText)
				{
					combinedWhere.Append(string.Format("{0}", filter.ParanthesEnd));
				}
			}
		}

		if ((filter1StartParanthesAdded && !filter1EndParanthesAdded) || (filter2StartParanthesAdded && !filter2EndParanthesAdded))
		{
			combinedWhere.Append(")");
		}

		return combinedWhere.ToString();
	}

	private static List<string> SplitFilterValue(string filterValue, string filterOperator)
	{
		List<string> valueList = new List<string>();

		if (filterOperator != "=" && filterOperator != "!")
		{
			valueList.Add(filterValue);
			return valueList;
		}

		foreach (Match match in Regex.Matches(filterValue, "\"([^\"]*)\""))
		{
			if (match.Value != "")
			{
				if (!valueList.Contains(match.Value))
				{
					valueList.Add(match.Value);
				}
			}
		}

		for (int i = 0; i < valueList.Count; i++)
		{
			string value = valueList[i];
			filterValue = filterValue.Replace(value, "");
			valueList[i] = value.Replace("\"", "");
		}

		//string[] values = filterValue.Split(' ');
		string[] values = { filterValue };

		foreach (string value in values)
		{
			if (value != "")
			{
				if (!valueList.Contains(value))
				{
					valueList.Add(value);
				}
			}
		}

		return valueList;
	}

	private static bool UseFullText(string filterValue, Filter filter)
	{
		bool useFullText = false;

		if (filterValue.Contains("\""))
		{
			return false;
		}

		if (filter.ParanthesBegin != "" || filter.ParanthesEnd != "")
		{
			return false;
		}

		//string[] values = filterValue.Split(' ');
		string[] values = { filterValue };

		foreach (string value in values)
		{
			if ((value.EndsWith("*") || value.EndsWith("%")) && !value.StartsWith("*") && !value.StartsWith("%"))
			{
				useFullText = true;
			}

			if (filterValue.Length >= 2)
			{
				string filterValueMiddle = filterValue.Substring(1, filterValue.Length - 2);

				if (filterValueMiddle.Contains("*") || filterValueMiddle.Contains("%"))
				{
					useFullText = false;
					break;
				}
			}
		}

		return useFullText;
	}
}
