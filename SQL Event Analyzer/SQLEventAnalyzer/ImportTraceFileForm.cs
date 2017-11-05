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
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

public partial class ImportTraceFileForm : Form
{
	private readonly Stopwatch _sw = new Stopwatch();
	private BackgroundWorker _worker;
	private bool _success;

	public ImportTraceFileForm()
	{
		InitializeComponent();
	}

	public void Initialize(DatabaseOperation databaseOperation, List<ImportTraceFileInfo> importTraceFileInfoList)
	{
		InitializeDictionary();

		timeTextBox.GotFocus += TimeTextBox_GotFocus;

		progressBar1.Value = 0;

		elapsedTimeTimer.Start();
		_sw.Reset();
		_sw.Start();

		RunWorkerArgument arg = new RunWorkerArgument();
		arg.DatabaseOperation = databaseOperation;
		arg.ImportTraceFileInfoList = importTraceFileInfoList;

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

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			fileLabel.Text = Translator.GetText("fileLabel");
			nameLabel.Text = Translator.GetText("nameLabel");
			sizeLabel.Text = Translator.GetText("sizeLabel");
			statusLabel.Text = Translator.GetText("statusLabel");
			Text = Translator.GetText("parsingTraceFiles");
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
			SetFileLabel(progressObject.Step, progressObject.Total, progressObject.ImportTraceFileInfo);
		}
		else if (e.ProgressPercentage == -2)
		{
			string statusMessage = e.UserState.ToString();

			fileValueLabel.Text = "";
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
				_worker.ReportProgress(-2, Translator.GetText("preparing"));
			}
			else
			{
				_worker.ReportProgress(-2, "Preparing...");
			}
		}

		arg.DatabaseOperation.CreateTempTable();
		ConfigHandler.TempTableCreated = true;

		if (GenericHelper.IsUserInteractive())
		{
			if (ConfigHandler.UseTranslation)
			{
				_worker.ReportProgress(-2, Translator.GetText("importingFiles"));
			}
			else
			{
				_worker.ReportProgress(-2, "Importing file(s)...");
			}
		}

		OutputHandler.WriteToLog("Importing file(s)...");

		arg.DatabaseOperation.DisableColumnStoreIndex();
		arg.DatabaseOperation.StopFullTextPopulation();

		bool success = true;

		for (int i = 0; i < arg.ImportTraceFileInfoList.Count; i++)
		{
			if (GenericHelper.IsUserInteractive())
			{
				_worker.ReportProgress(-1, new ProgressObject(i + 1, arg.ImportTraceFileInfoList.Count, arg.ImportTraceFileInfoList[i]));
			}

			OutputHandler.WriteToLog(string.Format("{0}/{1} ({2} - {3})", i + 1, arg.ImportTraceFileInfoList.Count, Path.GetFileName(arg.ImportTraceFileInfoList[i].FileName), GetSizeValue(arg.ImportTraceFileInfoList[i].FileSize)));

			success = TraceFileHandler.ImportTraceFile(arg.DatabaseOperation, arg.ImportTraceFileInfoList[i].FileName);

			if (!success)
			{
				break;
			}
		}

		if (success)
		{
			OutputHandler.WriteToLog("Importing file(s): Completed");
		}

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

	private void SetFileLabel(int step, int total, ImportTraceFileInfo importTraceFileInfo)
	{
		progressBar1.Maximum = total;
		progressBar1.Value = step;

		string fileName = Path.GetFileName(importTraceFileInfo.FileName);
		string fileSize = importTraceFileInfo.FileSize;

		fileValueLabel.Text = string.Format("{0}/{1}", step, total);
		nameValueLabel.Text = fileName;
		sizeValueLabel.Text = GetSizeValue(fileSize);
	}

	private static string GetSizeValue(string fileSize)
	{
		if (fileSize == null)
		{
			return "-";
		}

		decimal fileSizeInKB = Convert.ToDecimal(fileSize);
		decimal fileSizeInMB = Math.Round(fileSizeInKB / 1024, 0);
		decimal fileSizeInGB = Math.Round(fileSizeInMB / 1024, 0);

		if (fileSizeInGB != 0)
		{
			return string.Format("{0} GB", Math.Round(fileSizeInMB / 1024, 2));
		}

		if (fileSizeInMB != 0)
		{
			return string.Format("{0} MB", Math.Round(fileSizeInKB / 1024, 2));
		}

		return string.Format("{0} KB", fileSizeInKB);
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
		public List<ImportTraceFileInfo> ImportTraceFileInfoList;
	}

	private class ProgressObject
	{
		public readonly int Step;
		public readonly int Total;
		public readonly ImportTraceFileInfo ImportTraceFileInfo;

		public ProgressObject(int step, int total, ImportTraceFileInfo importTraceFileInfo)
		{
			Step = step;
			Total = total;
			ImportTraceFileInfo = importTraceFileInfo;
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
