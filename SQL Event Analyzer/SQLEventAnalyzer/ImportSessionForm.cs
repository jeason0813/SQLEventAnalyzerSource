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
using System.Diagnostics;
using System.Windows.Forms;

public partial class ImportSessionForm : Form
{
	private readonly Stopwatch _sw = new Stopwatch();
	private BackgroundWorker _worker;
	private bool _success;

	public ImportSessionForm()
	{
		InitializeComponent();
	}

	public void Initialize(DatabaseOperation databaseOperation, List<ImportSessionInfo> importSessionInfoList)
	{
		InitializeDictionary();

		timeTextBox.GotFocus += TimeTextBox_GotFocus;

		progressBar1.Value = 0;

		elapsedTimeTimer.Start();
		_sw.Reset();
		_sw.Start();

		RunWorkerArgument arg = new RunWorkerArgument();
		arg.DatabaseOperation = databaseOperation;
		arg.ImportSessionInfoList = importSessionInfoList;

		if (GenericHelper.IsUserInteractive())
		{
			InitializeWorker();
			_worker.RunWorkerAsync(arg);
		}
		else
		{
			RunWorkerCompleted(DoWork(arg));
		}
	}

	public bool GetSuccess()
	{
		return _success;
	}

	public static List<string> GetNonDefaultColumns(DatabaseOperation databaseOperation, string sessionId)
	{
		List<string> nonDefaultColumns = new List<string>();

		DataTable importColumnNames = databaseOperation.GetNonDefaultColumnNames(string.Format("TraceData_{0}", sessionId));

		if (ColumnHelper.EnabledColumns.Count > 0)
		{
			for (int i = 0; i < ColumnHelper.EnabledColumns.Count; i++)
			{
				string enabledColumnName = ColumnHelper.EnabledColumns[i].Name.Replace("'", "''");

				bool found = false;

				foreach (DataRow dataRow in importColumnNames.Rows)
				{
					if (dataRow["column_name"].ToString() == enabledColumnName)
					{
						found = true;
						break;
					}
				}

				if (found)
				{
					nonDefaultColumns.Add(enabledColumnName);
				}
			}
		}

		return nonDefaultColumns;
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			sessionLabel.Text = Translator.GetText("sessionLabel");
			nameLabel.Text = Translator.GetText("nameLabel");
			sizeLabel.Text = Translator.GetText("sizeLabel");
			statusLabel.Text = Translator.GetText("statusLabel");
			Text = Translator.GetText("parsingSessions");
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

	private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		RunWorkerCompleted((bool)e.Result);
	}

	private void RunWorkerCompleted(bool success)
	{
		_success = success;
		Close();
	}

	private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		if (e.ProgressPercentage == -1)
		{
			ProgressObject progressObject = (ProgressObject)e.UserState;
			SetSessionLabel(progressObject.Step, progressObject.Total, progressObject.ImportSessionInfo);
		}
		else if (e.ProgressPercentage == -2)
		{
			string statusMessage = e.UserState.ToString();

			sessionValueLabel.Text = "";
			nameValueLabel.Text = "";
			sizeValueLabel.Text = "";

			statusValueLabel.Text = statusMessage;
		}
	}

	private void Worker_DoWork(object sender, DoWorkEventArgs e)
	{
		RunWorkerArgument arg = (RunWorkerArgument)e.Argument;
		e.Result = DoWork(arg);
	}

	private bool DoWork(RunWorkerArgument arg)
	{
		if (GenericHelper.IsUserInteractive())
		{
			if (ConfigHandler.UseTranslation)
			{
				_worker.ReportProgress(-2, Translator.GetText("importingSessions"));
			}
			else
			{
				_worker.ReportProgress(-2, "Importing session(s)...");
			}
		}

		OutputHandler.WriteToLog("Importing session(s)...");

		arg.DatabaseOperation.DisableColumnStoreIndex();
		arg.DatabaseOperation.StopFullTextPopulation();

		bool success = false;

		for (int i = 0; i < arg.ImportSessionInfoList.Count; i++)
		{
			if (GenericHelper.IsUserInteractive())
			{
				_worker.ReportProgress(-1, new ProgressObject(i + 1, arg.ImportSessionInfoList.Count, arg.ImportSessionInfoList[i]));
			}

			List<string> nonDefaultColumns = GetNonDefaultColumns(arg.DatabaseOperation, arg.ImportSessionInfoList[i].SessionId);

			success = SessionHandler.ImportSession(arg.DatabaseOperation, arg.ImportSessionInfoList[i].SessionId, nonDefaultColumns);

			if (!success)
			{
				break;
			}
		}

		if (success)
		{
			OutputHandler.WriteToLog("Importing session(s): Completed");
		}

		if (!ConfigHandler.TempTableCreated)
		{
			if (GenericHelper.IsUserInteractive())
			{
				if (ConfigHandler.UseTranslation)
				{
					_worker.ReportProgress(-2, Translator.GetText("creatingIndexes"));
				}
				else
				{
					_worker.ReportProgress(-2, "Creating indexes...");
				}
			}

			OutputHandler.WriteToLog("Creating indexes...");

			arg.DatabaseOperation.CreateIndexes();

			ConfigHandler.TempTableCreated = true;
		}

		if (arg.DatabaseOperation.GetSqlServerVersion() >= 11 && arg.DatabaseOperation.IsColumnStoreSupported())
		{
			if (GenericHelper.IsUserInteractive())
			{
				if (ConfigHandler.UseTranslation)
				{
					_worker.ReportProgress(-2, Translator.GetText("enableColumnStoreIndex"));
				}
				else
				{
					_worker.ReportProgress(-2, "Creating Column Store Index...");
				}
			}

			OutputHandler.WriteToLog("Creating Column Store Index...");

			arg.DatabaseOperation.EnableColumnStoreIndex();

			OutputHandler.WriteToLog("Creating Column Store Index: Completed");
		}

		if (GenericHelper.IsUserInteractive())
		{
			if (ConfigHandler.UseTranslation)
			{
				_worker.ReportProgress(-2, Translator.GetText("populatingFullText"));
			}
			else
			{
				_worker.ReportProgress(-2, "Populating full text catalog...");
			}
		}

		OutputHandler.WriteToLog("Populating full text catalog...");

		arg.DatabaseOperation.StartFullTextPopulation();

		return success;
	}

	private void SetSessionLabel(int step, int total, ImportSessionInfo importSessionInfo)
	{
		progressBar1.Maximum = total;
		progressBar1.Value = step;

		string sessionId = importSessionInfo.SessionId;
		string sessionSize = importSessionInfo.SessionSize;

		sessionValueLabel.Text = string.Format("{0}/{1}", step, total);
		nameValueLabel.Text = sessionId;
		sizeValueLabel.Text = GetSizeValue(sessionSize);
	}

	private static string GetSizeValue(string sessionSize)
	{
		if (sessionSize == null)
		{
			return "-";
		}

		decimal sessionSizeInKB = Convert.ToDecimal(sessionSize);
		decimal sessionSizeInMB = Math.Round(sessionSizeInKB / 1024, 0);
		decimal sessionSizeInGB = Math.Round(sessionSizeInMB / 1024, 0);

		if (sessionSizeInGB != 0)
		{
			return string.Format("{0} GB", Math.Round(sessionSizeInMB / 1024, 2));
		}

		if (sessionSizeInMB != 0)
		{
			return string.Format("{0} MB", Math.Round(sessionSizeInKB / 1024, 2));
		}

		return string.Format("{0} KB", sessionSizeInKB);
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

	private void TimeTextBox_MouseDown(object sender, MouseEventArgs e)
	{
		GenericHelper.HideCaret(timeTextBox);
	}

	private class RunWorkerArgument
	{
		public DatabaseOperation DatabaseOperation;
		public List<ImportSessionInfo> ImportSessionInfoList;
	}

	private class ProgressObject
	{
		public readonly int Step;
		public readonly int Total;
		public readonly ImportSessionInfo ImportSessionInfo;

		public ProgressObject(int step, int total, ImportSessionInfo importSessionInfo)
		{
			Step = step;
			Total = total;
			ImportSessionInfo = importSessionInfo;
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
