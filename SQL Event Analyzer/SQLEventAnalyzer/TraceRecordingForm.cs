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
using System.IO;
using System.Windows.Forms;

public partial class TraceRecordingForm : Form
{
	private DatabaseOperation _databaseOperation;
	private int _traceId;
	private bool _traceRunning;
	private bool _success;
	private bool _recordingMode;
	private BackgroundWorker _checkTraceFileDirWorker;
	private bool _checkingAccessRights;

	public TraceRecordingForm()
	{
		InitializeComponent();
	}

	public void Initialize(DatabaseOperation databaseOperation, bool recordingMode)
	{
		InitializeDictionary();

		_databaseOperation = databaseOperation;
		_recordingMode = recordingMode;
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			Text = Translator.GetText("Record");
			recordGroupBox.Text = Translator.GetText("Record");
			statusLabel.Text = Translator.GetText("StatusIdle");
			startButton.Text = Translator.GetText("startButton");
			pauseButton.Text = Translator.GetText("pauseButton");
			stopButton.Text = Translator.GetText("stopButton");
			cancelButton.Text = Translator.GetText("cancelButton");
		}
	}

	public bool GetSuccess()
	{
		return _success;
	}

	protected override void OnLoad(EventArgs args)
	{
		if (Site == null || (Site != null && !Site.DesignMode))
		{
			base.OnLoad(args);
			Application.Idle += OnLoaded;
		}
	}

	private void OnLoaded(object sender, EventArgs args)
	{
		Application.Idle -= OnLoaded;

		if (_recordingMode)
		{
			startButton.PerformClick();
		}
	}

	private bool StartTracing()
	{
		bool success = _databaseOperation.StopDeleteTrace();

		if (!success)
		{
			return false;
		}

		success = TraceFileHandler.DeleteTraceFile(true, _databaseOperation);

		if (!success)
		{
			return false;
		}

		success = BeginTracing();
		return success;
	}

	private bool BeginTracing()
	{
		_traceId = _databaseOperation.CreateTrace();

		if (_traceId == 0)
		{
			_databaseOperation.StopDeleteTrace();

			string text = "Insufficient rights to Trace File Directory.\r\n\r\nIf you are connection to a remote SQL Server, please use UNC path and make sure the server has write access to your network share.\r\n\r\nTrace File Directory can be set in the Preferences menu.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("traceRights");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);

			return false;
		}
		else if (_traceId == -1)
		{
			_databaseOperation.StopDeleteTrace();

			string text = "Out of memory.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("outOfMemory");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return false;
		}
		else
		{
			bool success = _databaseOperation.StartTraceRecording(_traceId);

			if (!success && ConfigHandler.UseExtendedEvents == "True")
			{
				MessageBox.Show("Insufficient rights to Trace File Directory.\r\n\r\nIf you are connecting to a remote SQL Server, please use UNC path and make sure the server has write access to your network share.\r\n\r\nTrace File Directory can be set in the Preferences menu.", GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			return success;
		}
	}

	private void StartButton_Click(object sender, EventArgs e)
	{
		startButton.Enabled = false;
		cancelButton.Enabled = false;

		CheckTraceFileDir();
	}

	private void DoStart()
	{
		bool success = StartTracing();

		if (success)
		{
			if (ConfigHandler.UseTranslation)
			{
				statusLabel.Text = Translator.GetText("statusRecording");
			}
			else
			{
				statusLabel.Text = "Status: Recording...";
			}

			_traceRunning = true;
			pauseButton.Enabled = true;
			stopButton.Enabled = true;
			cancelButton.Enabled = true;
			progressBar1.Style = ProgressBarStyle.Marquee;
			recordingPictureBox.Visible = true;
			stopButton.Focus();
		}
		else
		{
			startButton.Enabled = true;
			cancelButton.Enabled = true;
		}
	}

	private void CheckTraceFileDir()
	{
		_checkingAccessRights = true;

		startButton.Enabled = false;
		cancelButton.Enabled = false;

		if (ConfigHandler.UseTranslation)
		{
			statusLabel.Text = Translator.GetText("CheckingAccessRights");
		}
		else
		{
			statusLabel.Text = "Status: Checking access rights...";
		}

		_checkTraceFileDirWorker = new BackgroundWorker();
		_checkTraceFileDirWorker.DoWork += CheckTraceFileDirWorker_DoWork;
		_checkTraceFileDirWorker.RunWorkerCompleted += CheckTraceFileDirWorker_RunWorkerCompleted;

		_checkTraceFileDirWorker.RunWorkerAsync();
	}

	private void CheckTraceFileDirWorker_DoWork(object sender, DoWorkEventArgs e)
	{
		bool success;

		if (!Directory.Exists(ConfigHandler.RecordTraceFileDir))
		{
			success = false;

			string text = "Trace File Directory does not exist.\r\n\r\nIf you are reading from a remote server to a local drive, please use a UNC path and make sure the server has read access to your network share and the SQL Server Service Account has read access to the share.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("traceFileSecurityInfo");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		else
		{
			_databaseOperation.SetShowErrorForm(false);

			success = BeginTracing();

			if (success)
			{
				string traceFile = null;

				if (Directory.GetFiles(ConfigHandler.RecordTraceFileDir, string.Format("{0}*.*", ConfigHandler.TraceFileName)).Length > 0)
				{
					traceFile = Directory.GetFiles(ConfigHandler.RecordTraceFileDir, string.Format("{0}*.*", ConfigHandler.TraceFileName))[0];
				}

				if (traceFile == null || !File.Exists(traceFile))
				{
					success = false;

					string text = "Insufficient rights to Trace File Directory.\r\n\r\nIf you are connection to a remote SQL Server, please use UNC path and make sure the server has write access to your network share.\r\n\r\nTrace File Directory can be set in the Preferences menu.";

					if (ConfigHandler.UseTranslation)
					{
						text = Translator.GetText("traceRights");
					}

					OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}

			_databaseOperation.StopDeleteTrace();
			TraceFileHandler.DeleteTraceFile(false, _databaseOperation);
		}

		e.Result = success;
	}

	private void CheckTraceFileDirWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		_databaseOperation.SetShowErrorForm(true);
		_checkingAccessRights = false;

		if (ConfigHandler.UseTranslation)
		{
			statusLabel.Text = Translator.GetText("StatusIdle");
		}
		else
		{
			statusLabel.Text = "Status: Idle";
		}

		bool success = Convert.ToBoolean(e.Result);

		if (success)
		{
			DoStart();
		}
		else
		{
			startButton.Enabled = true;
			cancelButton.Enabled = true;
		}
	}

	private void PauseButton_Click(object sender, EventArgs e)
	{
		string pauseButtonText = "Pause";

		if (ConfigHandler.UseTranslation)
		{
			pauseButtonText = Translator.GetText("pauseButton");
		}

		if (pauseButton.Text == pauseButtonText)
		{
			if (ConfigHandler.UseTranslation)
			{
				statusLabel.Text = Translator.GetText("statusPaused");
			}
			else
			{
				statusLabel.Text = "Status: Paused";
			}

			progressBar1.MarqueeAnimationSpeed = 0;

			if (ConfigHandler.UseTranslation)
			{
				pauseButton.Text = Translator.GetText("continueButton");
			}
			else
			{
				pauseButton.Text = "    Continue";
			}

			recordingPictureBox.Visible = false;
			_databaseOperation.SetTraceStatus(_traceId, 0);
		}
		else
		{
			if (ConfigHandler.UseTranslation)
			{
				statusLabel.Text = Translator.GetText("statusRecording");
			}
			else
			{
				statusLabel.Text = "Status: Recording...";
			}

			progressBar1.MarqueeAnimationSpeed = 100;
			pauseButton.Text = pauseButtonText;
			recordingPictureBox.Visible = true;
			_databaseOperation.SetTraceStatus(_traceId, 1);
		}
	}

	private void StopButton_Click(object sender, EventArgs e)
	{
		bool success = _databaseOperation.StopDeleteTrace();
		_traceRunning = false;
		progressBar1.Style = ProgressBarStyle.Blocks;

		if (success)
		{
			success = ImportTraceFile();
			TraceFileHandler.DeleteTraceFile(true, _databaseOperation);
		}

		_success = success;
		Close();
	}

	private string GetFileSize()
	{
		string fileName = null;

		if (ConfigHandler.UseExtendedEvents == "False" || _databaseOperation.GetSqlServerVersion() < 11)
		{
			fileName = string.Format(@"{0}\{1}.trc", ConfigHandler.RecordTraceFileDir.Replace("'", "''"), ConfigHandler.TraceFileName);
		}
		else
		{
			string[] fileNames = Directory.GetFiles(ConfigHandler.RecordTraceFileDir.Replace("'", "''"), string.Format("{0}*.xel", ConfigHandler.TraceFileName));

			if (fileNames.Length != 0)
			{
				fileName = fileNames[0];
			}
		}

		bool fileExists = File.Exists(fileName);

		if (fileExists && fileName != null)
		{
			string fileSize = (new FileInfo(fileName).Length / 1024).ToString();
			return fileSize;
		}

		return null;
	}

	private bool ImportTraceFile()
	{
		List<ImportTraceFileInfo> importTraceFileInfoList = new List<ImportTraceFileInfo>();

		string fileName;

		if (ConfigHandler.UseExtendedEvents == "False" || _databaseOperation.GetSqlServerVersion() < 11)
		{
			fileName = string.Format(@"{0}\{1}.trc", ConfigHandler.RecordTraceFileDir.Replace("'", "''"), ConfigHandler.TraceFileName);
		}
		else
		{
			fileName = string.Format(@"{0}\{1}*.xel", ConfigHandler.RecordTraceFileDir.Replace("'", "''"), ConfigHandler.TraceFileName);
		}

		string fileSize = GetFileSize();
		importTraceFileInfoList.Add(new ImportTraceFileInfo(fileName, fileSize));

		ImportTraceFileForm form = new ImportTraceFileForm();
		form.Initialize(_databaseOperation, importTraceFileInfoList);
		form.ShowDialog();

		bool success = form.GetSuccess();
		return success;
	}

	private void TraceRecordingForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (_checkingAccessRights)
		{
			e.Cancel = true;
			return;
		}

		if (_traceRunning)
		{
			string text = "Recording in progress.\r\n\r\nAbort operation?";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("abortRecording");
			}

			DialogResult result = OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

			if (result.ToString() == "Yes")
			{
				_databaseOperation.StopDeleteTrace();
				TraceFileHandler.DeleteTraceFile(true, _databaseOperation);
			}
			else
			{
				e.Cancel = true;
			}
		}
	}
}
