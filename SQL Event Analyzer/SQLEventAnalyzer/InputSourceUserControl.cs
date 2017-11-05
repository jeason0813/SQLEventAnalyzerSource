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
using System.Windows.Forms;

public partial class InputSourceUserControl : UserControl
{
	public delegate bool ShowDataViewEventHandler(bool handleColumns, bool importFromExistingTable, bool connectToExistingSession, bool connectToCurrentSession);
	public event ShowDataViewEventHandler ShowDataViewEvent;

	private string _previousInputSource;

	public InputSourceUserControl()
	{
		InitializeComponent();
	}

	public void Initialize(DatabaseOperation databaseOperation)
	{
		InitializeDictionary();

		traceFileSelectorUserControl1.Initialize(databaseOperation);
		sessionSelectorUserControl1.Initialize(databaseOperation);

		traceFileSelectorUserControl1.ShowDataViewEvent += TraceFileSelectorUserControl1_ShowDataViewEvent;
		sessionSelectorUserControl1.ShowDataViewEvent += SessionSelectorUserControl1_ShowDataViewEvent;
		traceFileSelectorUserControl1.InputHandlingBeginEvent += TraceFileSelectorUserControl1_InputHandlingBeginEvent;
		sessionSelectorUserControl1.InputHandlingBeginEvent += SessionSelectorUserControl1_InputHandlingBeginEvent;
		traceFileSelectorUserControl1.InputHandlingEndEvent += TraceFileSelectorUserControl1_InputHandlingEndEvent;
		sessionSelectorUserControl1.InputHandlingEndEvent += SessionSelectorUserControl1_InputHandlingEndEvent;
		sessionSelectorUserControl1.NewSessionEvent += SessionSelectorUserControl1_NewSessionEvent;
	}

	public bool GetManuallyUseSession()
	{
		return sessionSelectorUserControl1.GetManuallyUseSession();
	}

	public void SetManuallyUseSession(bool value)
	{
		sessionSelectorUserControl1.SetManuallyUseSession(value);
	}

	public void SetImportNew(bool value)
	{
		traceFileSelectorUserControl1.SetImportNew(value);
		sessionSelectorUserControl1.SetImportNew(value);
	}

	public List<ImportSessionInfo> GetImportSessionInfoList()
	{
		return sessionSelectorUserControl1.GetImportSessionInfoList();
	}

	public void RefreshSessionListView()
	{
		sessionSelectorUserControl1.RefreshSessionListView();
	}

	public void SelectControl()
	{
		if (traceFileSelectorUserControl1.Visible)
		{
			traceFileSelectorUserControl1.SelectControl();
		}
		else if (sessionSelectorUserControl1.Visible)
		{
			sessionSelectorUserControl1.SelectControl();
		}
	}

	public bool CompressCheckedTraceFiles(int filesToKeep, string sevenZipFileName)
	{
		return traceFileSelectorUserControl1.CompressCheckedTraceFiles(filesToKeep, sevenZipFileName);
	}

	public bool DeleteCheckedTraceFiles()
	{
		return traceFileSelectorUserControl1.DeleteCheckedTraceFiles();
	}

	public void RefreshListView()
	{
		if (traceFileSelectorUserControl1.Visible)
		{
			traceFileSelectorUserControl1.RefreshTraceFileListView();
		}
		else if (sessionSelectorUserControl1.Visible)
		{
			sessionSelectorUserControl1.RefreshSessionListView();
		}
	}

	public void HandleListView(bool unattended, string unattendedImportPath)
	{
		traceFileSelectorUserControl1.HandleListView(unattended, unattendedImportPath);
		sessionSelectorUserControl1.HandleListView();
	}

	public bool HandleUnattended(int numberOfImportFiles, string lastImportFile)
	{
		bool success = traceFileSelectorUserControl1.HandleUnattended(numberOfImportFiles, lastImportFile);
		return success;
	}

	public void ConnectToSessionId(string sessionId)
	{
		sessionSelectorUserControl1.ConnectToSession(sessionId);
	}

	public void ImportSessionToCurrentSession(string sessionIdToImport)
	{
		sessionSelectorUserControl1.ImportSessionToCurrentSession(sessionIdToImport);
	}

	public void UseSession(string useSessionId, bool verboseMode)
	{
		sessionSelectorUserControl1.UseSession(useSessionId, verboseMode);
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			switchInputButton.Text = Translator.GetText("sessionsTabPage");
		}
	}

	private void SessionSelectorUserControl1_InputHandlingBeginEvent()
	{
		if (_previousInputSource != "Session")
		{
			sessionSelectorUserControl1.SetImportNew(true);
		}

		switchInputButton.Enabled = false;
		_previousInputSource = "Session";
	}

	private void TraceFileSelectorUserControl1_InputHandlingBeginEvent()
	{
		if (_previousInputSource != "TraceFile")
		{
			traceFileSelectorUserControl1.SetImportNew(true);
		}

		switchInputButton.Enabled = false;
		_previousInputSource = "TraceFile";
	}

	private void SessionSelectorUserControl1_InputHandlingEndEvent()
	{
		sessionSelectorUserControl1.SetImportNew(false);
		switchInputButton.Enabled = true;
	}

	private void TraceFileSelectorUserControl1_InputHandlingEndEvent()
	{
		traceFileSelectorUserControl1.SetImportNew(false);
		switchInputButton.Enabled = true;
	}

	private void SessionSelectorUserControl1_ShowDataViewEvent(bool handleColumns, bool connectToExistingSession, bool connectToCurrentSession)
	{
		FireShowDataViewEvent(handleColumns, true, connectToExistingSession, connectToCurrentSession);
	}

	private bool TraceFileSelectorUserControl1_ShowDataViewEvent(bool handleColumns)
	{
		return FireShowDataViewEvent(handleColumns, false, false, false);
	}

	private bool FireShowDataViewEvent(bool handleColumns, bool importFromExistingTable, bool connectToExistingSession, bool connectToCurrentSession)
	{
		bool success = true;

		if (ShowDataViewEvent != null)
		{
			success = ShowDataViewEvent(handleColumns, importFromExistingTable, connectToExistingSession, connectToCurrentSession);
		}

		return success;
	}

	private void SwitchToSessionSelector()
	{
		traceFileSelectorUserControl1.Visible = false;
		sessionSelectorUserControl1.Visible = true;
		traceFileSelectorUserControl1.TabStop = false;
		sessionSelectorUserControl1.TabStop = true;

		if (ConfigHandler.UseTranslation)
		{
			switchInputButton.Text = Translator.GetText("traceFilesTabPage");
		}
		else
		{
			switchInputButton.Text = "View Trace Files";
		}

		sessionSelectorUserControl1.SelectControl();
	}

	private void SwitchToTracefileSelector()
	{
		sessionSelectorUserControl1.Visible = false;
		traceFileSelectorUserControl1.Visible = true;
		traceFileSelectorUserControl1.TabStop = true;
		sessionSelectorUserControl1.TabStop = false;

		if (ConfigHandler.UseTranslation)
		{
			switchInputButton.Text = Translator.GetText("sessionsTabPage");
		}
		else
		{
			switchInputButton.Text = "View Sessions";
		}

		traceFileSelectorUserControl1.SelectControl();
	}

	private void SwitchInputButton_Click(object sender, System.EventArgs e)
	{
		if (traceFileSelectorUserControl1.Visible)
		{
			SwitchToSessionSelector();
		}
		else
		{
			SwitchToTracefileSelector();
		}
	}

	private void SessionSelectorUserControl1_NewSessionEvent()
	{
		traceFileSelectorUserControl1.SetImportNew(true);
	}
}
