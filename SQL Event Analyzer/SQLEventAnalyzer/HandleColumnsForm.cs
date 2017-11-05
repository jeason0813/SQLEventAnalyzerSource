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
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

public partial class HandleColumnsForm : Form
{
	private readonly Stopwatch _sw = new Stopwatch();
	private DatabaseOperation _databaseOperation;
	private BackgroundWorker _worker;
	private bool _importFromExistingTable;
	private bool _success;

	public HandleColumnsForm()
	{
		InitializeComponent();
	}

	public void Initialize(DatabaseOperation databaseOperation, bool importFromExistingTable)
	{
		InitializeDictionary();

		timeTextBox.GotFocus += TimeTextBox_GotFocus;

		_databaseOperation = databaseOperation;
		_importFromExistingTable = importFromExistingTable;

		elapsedTimeTimer.Start();
		_sw.Reset();
		_sw.Start();

		if (GenericHelper.IsUserInteractive())
		{
			InitializeWorker();
			_worker.RunWorkerAsync();
		}
		else
		{
			DoWorkEventArgs e = new DoWorkEventArgs(null);
			DoWork(e);
			RunWorkerCompleted(e.Result);
		}
	}

	public bool GetSuccess()
	{
		return _success;
	}

	public static string HandleParameters(string text)
	{
		foreach (Parameter parameter in ColumnHelper.ColumnCollection.Parameters)
		{
			text = text.Replace(parameter.Name, parameter.Value);
		}

		string sessionId = GenericHelper.GetSessionIdFromTableName();
		text = text.Replace("{SessionId}", sessionId.Replace("'", "''"));

		return text;
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			infoLabel1.Text = Translator.GetText("preparingCustomColumns");
			infoLabel2.Text = Translator.GetText("preparingLabel");
			Text = Translator.GetText("Working");
		}
	}

	private void InitializeWorker()
	{
		_worker = new BackgroundWorker();
		_worker.WorkerReportsProgress = true;
		_worker.DoWork += Worker_DoWork;
		_worker.ProgressChanged += Worker_ProgressChanged;
		_worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
	}

	private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		if (e.ProgressPercentage == -1)
		{
			ProgressObject progressObject = (ProgressObject)e.UserState;

			progressBar1.Maximum = progressObject.Total;
			progressBar1.Value = progressObject.Step;

			if (ConfigHandler.UseTranslation)
			{
				infoLabel1.Text = string.Format("{2} ({0}/{1}): {3}", progressObject.Step, progressObject.Total, Translator.GetText("preparingCustomColumns"), progressObject.Message);
			}
			else
			{
				infoLabel1.Text = string.Format("Preparing Custom Column ({0}/{1}): {2}", progressObject.Step, progressObject.Total, progressObject.Message);
			}
		}
		else if (e.ProgressPercentage == -2)
		{
			ProgressObject progressObject = (ProgressObject)e.UserState;

			progressBar2.Maximum = progressObject.Total;
			progressBar2.Value = progressObject.Step;

			infoLabel2.Text = string.Format("{2} ({0}/{1})", progressObject.Step, progressObject.Total, progressObject.Message);
		}
	}

	private void Worker_DoWork(object sender, DoWorkEventArgs e)
	{
		DoWork(e);
	}

	private void DoWork(DoWorkEventArgs e)
	{
		bool success = true;

		_databaseOperation.DisableColumnStoreIndex();
		_databaseOperation.StopFullTextPopulation();

		StringBuilder sb = new StringBuilder();

		if (_importFromExistingTable)
		{
			OutputHandler.WriteToLog("Handling Custom Columns from session");
		}
		else
		{
			OutputHandler.WriteToLog("Handling Custom Columns from Trace File");
		}

		if (ColumnHelper.EnabledColumns.Count > 0)
		{
			for (int i = 0; i < ColumnHelper.EnabledColumns.Count; i++)
			{
				ConfigHandler.ActiveCustomColumn = ColumnHelper.EnabledColumns[i].Name;

				if (GenericHelper.IsUserInteractive())
				{
					_worker.ReportProgress(-1, new ProgressObject(i + 1, ColumnHelper.EnabledColumns.Count, ColumnHelper.EnabledColumns[i].Name));

					if (ConfigHandler.UseTranslation)
					{
						_worker.ReportProgress(-2, new ProgressObject(1, 4, Translator.GetText("handlingCustomColumns")));
					}
					else
					{
						_worker.ReportProgress(-2, new ProgressObject(1, 4, "Handling Custom Columns"));
					}
				}

				OutputHandler.WriteToLog(string.Format("{0}/{1} ({2})", i + 1, ColumnHelper.EnabledColumns.Count, ColumnHelper.EnabledColumns[i].Name));

				sb.Append(string.Format("'{0}', ", ColumnHelper.EnabledColumns[i].Name.Replace("'", "''")));

				_databaseOperation.Execute(string.Format("use [{0}] if not exists (select c.* from sys.columns c where c.name = '{1}' and object_id = object_id('dbo.[{4}]')) begin alter table dbo.[{5}] add [{2}] varchar({3}) not null constraint [DF_{5}_{2}] default ('') end", ConfigHandler.DatabaseName, ColumnHelper.EnabledColumns[i].Name.Replace("'", "''"), ColumnHelper.EnabledColumns[i].Name, ConfigHandler.ColumnMaxCharacters, GenericHelper.TempTableName.Replace("'", "''"), GenericHelper.TempTableName));

				if (!_importFromExistingTable)
				{
					_databaseOperation.Execute(string.Format(SQLEventAnalyzer.Properties.Resources.EmptyColumns, ConfigHandler.DatabaseName, ColumnHelper.EnabledColumns[i].Name, GenericHelper.TempTableName));

					if (GenericHelper.IsUserInteractive())
					{
						if (ConfigHandler.UseTranslation)
						{
							_worker.ReportProgress(-2, new ProgressObject(2, 4, Translator.GetText("handlingCustomColumnData")));
						}
						else
						{
							_worker.ReportProgress(-2, new ProgressObject(2, 4, "Handling Custom Column Data"));
						}
					}

					string isolationLevel = ColumnHelper.IsolationLevelTypeToString(ColumnHelper.EnabledColumns[i].IsolationLevel);
					string updateSql = GetColumnUpdateSql(ColumnHelper.EnabledColumns[i]);
					string whereSql = GetColumnWhereSql(ColumnHelper.EnabledColumns[i]);

					bool currentSuccess = _databaseOperation.Execute(string.Format(SQLEventAnalyzer.Properties.Resources.HandleColumns, ConfigHandler.DatabaseName, updateSql, whereSql, GenericHelper.TempTableName, isolationLevel));

					if (!currentSuccess && success)
					{
						success = false;
					}
				}

				if (!ColumnHelper.EnabledColumns[i].Hidden)
				{
					if (GenericHelper.IsUserInteractive())
					{
						if (ConfigHandler.UseTranslation)
						{
							_worker.ReportProgress(-2, new ProgressObject(3, 4, Translator.GetText("creatingIndexes")));
						}
						else
						{
							_worker.ReportProgress(-2, new ProgressObject(3, 4, "Creating indexes"));
						}
					}

					_databaseOperation.Execute(string.Format("use [{3}] if exists (select i.* from sys.indexes i where i.object_id = object_id(N'dbo.[{2}]') and i.name = N'ix_{1}_asc') drop index [ix_{0}_asc] on dbo.[{4}]", ColumnHelper.EnabledColumns[i].Name, ColumnHelper.EnabledColumns[i].Name.Replace("'", "''"), GenericHelper.TempTableName.Replace("'", "''"), ConfigHandler.DatabaseName, GenericHelper.TempTableName));
					_databaseOperation.Execute(string.Format("use [{3}] if exists (select i.* from sys.indexes i where i.object_id = object_id(N'dbo.[{2}]') and i.name = N'ix_{1}_desc') drop index [ix_{0}_desc] on dbo.[{4}]", ColumnHelper.EnabledColumns[i].Name, ColumnHelper.EnabledColumns[i].Name.Replace("'", "''"), GenericHelper.TempTableName.Replace("'", "''"), ConfigHandler.DatabaseName, GenericHelper.TempTableName));
					_databaseOperation.Execute(string.Format("use [{2}] create nonclustered index [ix_{0}_asc] on dbo.[{1}] ([{0}] asc, StartTime) with (fillfactor = 100)", ColumnHelper.EnabledColumns[i].Name, GenericHelper.TempTableName, ConfigHandler.DatabaseName));

					if (GenericHelper.IsUserInteractive())
					{
						if (ConfigHandler.UseTranslation)
						{
							_worker.ReportProgress(-2, new ProgressObject(4, 4, Translator.GetText("creatingIndexes")));
						}
						else
						{
							_worker.ReportProgress(-2, new ProgressObject(4, 4, "Creating indexes"));
						}
					}

					_databaseOperation.Execute(string.Format("use [{2}] create nonclustered index [ix_{0}_desc] on dbo.[{1}] ([{0}] desc, StartTime) with (fillfactor = 100)", ColumnHelper.EnabledColumns[i].Name, GenericHelper.TempTableName, ConfigHandler.DatabaseName));
				}
			}

			ConfigHandler.ActiveCustomColumn = "";
		}
		else
		{
			sb.Append("''");
		}

		string columnNames = sb.ToString();

		if (ColumnHelper.EnabledColumns.Count > 0)
		{
			columnNames = columnNames.Substring(0, columnNames.Length - 2);
		}

		DataTable dt = _databaseOperation.ExecuteDataTable(string.Format("use [{0}] select i.column_name Name from information_schema.columns i where i.table_name = 'dbo.[{2}]' and i.column_name not in ({1})", ConfigHandler.DatabaseName, columnNames, GenericHelper.TempTableName.Replace("'", "''")));

		foreach (DataRow dr in dt.Rows)
		{
			_databaseOperation.Execute(string.Format("use [{0}] alter table dbo.[{2}] drop column [{1}]", ConfigHandler.DatabaseName, dr["Name"], GenericHelper.TempTableName));
		}

		if (_databaseOperation.GetSqlServerVersion() >= 11 && _databaseOperation.IsColumnStoreSupported())
		{
			OutputHandler.WriteToLog("Creating Column Store Index...");

			_databaseOperation.EnableColumnStoreIndex();

			OutputHandler.WriteToLog("Creating Column Store Index: Completed");
		}

		OutputHandler.WriteToLog("Populating full text catalog...");

		_databaseOperation.StartFullTextPopulation();

		e.Result = success;
	}

	private static string GetColumnWhereSql(Column column)
	{
		if (column.InputType == Column.ColumnType.RegEx)
		{
			return string.Format("where dbo.MatchRegEx(TextData, '{0}') = 1", HandleParameters(column.Input).Replace("'", "''"));
		}
		else if (column.InputType == Column.ColumnType.SQL)
		{
			return string.Format("{0}", HandleParameters(column.Input));
		}
		else if (column.InputType == Column.ColumnType.StoredProcedureName)
		{
			return string.Format("where dbo.MatchStoredProcedureName(TextData, {0}) = 1", HandleParameters(column.Input));
		}
		else if (column.InputType == Column.ColumnType.Constant)
		{
			return string.Format("where TextData = '{0}'", HandleParameters(column.Input));
		}

		return null;
	}

	private static string GetColumnUpdateSql(Column column)
	{
		if (column.OutputType == Column.ColumnType.RegEx)
		{
			return string.Format("[{0}] = convert(varchar({2}), dbo.GetRegEx(TextData, '{1}'))", column.Name, HandleParameters(column.Output).Replace("'", "''"), ConfigHandler.ColumnMaxCharacters);
		}
		else if (column.OutputType == Column.ColumnType.SQL)
		{
			return string.Format("[{0}] =\r\n(\r\n{1})", column.Name, AddLineTabs(HandleParameters(column.Output)));
		}
		else if (column.OutputType == Column.ColumnType.StoredProcedureName)
		{
			return string.Format("[{0}] = convert(varchar({2}), dbo.GetStoredProcedureName(TextData, {1}))", column.Name, HandleParameters(column.Output), ConfigHandler.ColumnMaxCharacters);
		}
		else if (column.OutputType == Column.ColumnType.StoredProcedureParameter)
		{
			return string.Format("[{0}] = convert(varchar({2}), dbo.GetStoredProcedureParameter(TextData, {1}))", column.Name, HandleParameters(column.Output), ConfigHandler.ColumnMaxCharacters);
		}
		else if (column.OutputType == Column.ColumnType.LogParameter)
		{
			return string.Format("[{0}] = convert(varchar({2}), dbo.GetLogParameter(TextData, {1}))", column.Name, HandleParameters(column.Output), ConfigHandler.ColumnMaxCharacters);
		}
		else if (column.OutputType == Column.ColumnType.Constant)
		{
			return string.Format("[{0}] = convert(varchar({2}), '{1}')", column.Name, HandleParameters(column.Output), ConfigHandler.ColumnMaxCharacters);
		}

		return null;
	}

	private static string AddLineTabs(string inputText)
	{
		StringBuilder sb = new StringBuilder();

		string[] lines = inputText.Split('\n');

		for (int i = 0; i < lines.Length; i++)
		{
			sb.AppendLine(string.Format("\t{0}", lines[i]));
		}

		return sb.ToString();
	}

	private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		RunWorkerCompleted(e.Result);
	}

	private void RunWorkerCompleted(object result)
	{
		_success = Convert.ToBoolean(result);
		Close();
	}

	private void TimeTextBox_MouseDown(object sender, MouseEventArgs e)
	{
		GenericHelper.HideCaret(timeTextBox);
	}

	private void ElapsedTimeTimer_Tick(object sender, EventArgs e)
	{
		string days = _sw.Elapsed.Days.ToString();
		string hours = _sw.Elapsed.Hours.ToString();
		string minutes = _sw.Elapsed.Minutes.ToString();
		string seconds = _sw.Elapsed.Seconds.ToString();

		if (days.Length == 1)
		{
			days = string.Format("0{0}", days);
		}

		if (hours.Length == 1)
		{
			hours = string.Format("0{0}", hours);
		}

		if (minutes.Length == 1)
		{
			minutes = string.Format("0{0}", minutes);
		}

		if (seconds.Length == 1)
		{
			seconds = string.Format("0{0}", seconds);
		}

		timeTextBox.Text = string.Format("{0}:{1}:{2}:{3}", days, hours, minutes, seconds);
	}

	private class ProgressObject
	{
		public readonly int Step;
		public readonly int Total;
		public readonly string Message;

		public ProgressObject(int step, int total, string message)
		{
			Step = step;
			Total = total;
			Message = message;
		}
	}

	private void TimeTextBox_Enter(object sender, EventArgs e)
	{
		timeTextBox.SelectionStart = 0;
		timeTextBox.SelectionLength = 0;
		GenericHelper.HideCaret(timeTextBox);
	}

	private void TimeTextBox_GotFocus(object sender, EventArgs e)
	{
		timeTextBox.SelectionStart = 0;
		timeTextBox.SelectionLength = 0;
		GenericHelper.HideCaret(timeTextBox);
	}
}
