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
using System.Windows.Forms;

public partial class ChangeConnectionForm : Form
{
	private BackgroundWorker _worker;
	private bool _showHandleCLRFormInTaskBar;
	private bool _shiftPressed;
	private bool _manuallyUseSession;

	public ChangeConnectionForm()
	{
		InitializeComponent();
	}

	public void Initialize(DatabaseOperation databaseOperation, IWin32Window owner)
	{
		InitializeDictionary();
		Text = GenericHelper.ApplicationName;

		_showHandleCLRFormInTaskBar = true;
		RunWorkerArgument arg = new RunWorkerArgument(databaseOperation, null, GenericHelper.GetSessionIdFromTableName());

		if (GenericHelper.IsUserInteractive())
		{
			InitializeWorker();
			_worker.RunWorkerAsync(arg);
			ShowDialog(owner);
		}
		else
		{
			DoWork(arg);
			RunWorkerCompleted();
		}
	}

	public void Initialize(DatabaseOperation databaseOperation, IWin32Window owner, DatabaseOperation previousConnection, bool shiftPressed, bool manuallyUseSession, string previousSessionId)
	{
		InitializeDictionary();
		Text = GenericHelper.ApplicationName;

		_showHandleCLRFormInTaskBar = false;
		_shiftPressed = shiftPressed;
		_manuallyUseSession = manuallyUseSession;

		RunWorkerArgument arg = new RunWorkerArgument(databaseOperation, previousConnection, previousSessionId);

		InitializeWorker();
		_worker.RunWorkerAsync(arg);
		ShowDialog(owner);
	}

	private bool HandleCLREnabled(DatabaseOperation databaseOperation)
	{
		if (!databaseOperation.IsCLREnabled())
		{
			OutputHandler.WriteToLog("CLR must be enabled when running in a service context.");

			if (!GenericHelper.IsUserInteractive())
			{
				Environment.Exit(-1);
			}

			HandleCLRForm form = new HandleCLRForm();
			form.ShowInTaskbar = _showHandleCLRFormInTaskBar;
			form.Initialize();
			form.TopMost = true;
			form.ShowDialog();

			bool enableCLR = form.GetEnableCLR();

			bool enableCLRTemporary = form.GetEnableCLRTemporary();
			ConfigHandler.EnableCLRTemporary = enableCLRTemporary;

			return enableCLR;
		}
		else
		{
			return true;
		}
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			preparingLabel.Text = Translator.GetText("preparingLabel");
		}
	}

	private void InitializeWorker()
	{
		_worker = new BackgroundWorker();
		_worker.DoWork += Worker_DoWork;
		_worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
	}

	private void Worker_DoWork(object sender, DoWorkEventArgs e)
	{
		RunWorkerArgument arg = (RunWorkerArgument)e.Argument;
		DoWork(arg);
	}

	private void DoWork(RunWorkerArgument arg)
	{
		if (arg.PreviousConnection != null)
		{
			if (ConfigHandler.EnableCLRTemporary)
			{
				arg.PreviousConnection.DisableCLR();
			}

			if (!_shiftPressed && !ConfigHandler.KeepSessionOnExit)
			{
				if (!_manuallyUseSession || (_manuallyUseSession && GenericHelper.ConfirmDropTempTable(arg.SessionnId)))
				{
					arg.PreviousConnection.DropTempTable(string.Format("TraceData_{0}", arg.SessionnId.Replace("'", "''")));
				}
			}
		}

		DatabaseOperation databaseOperation = arg.DatabaseOperation;

		bool enableCLR = HandleCLREnabled(databaseOperation);

		if (enableCLR)
		{
			databaseOperation.HandleCLR();
		}

		GenericHelper.SqlServerName = databaseOperation.GetSqlServerName();
		GenericHelper.SqlServerVersion = databaseOperation.GetSqlServerVersion();

		databaseOperation.InitializeDatabaseActions();

		if (enableCLR)
		{
			databaseOperation.DeployCLR();
		}

		ConfigHandler.ClrDeployed = databaseOperation.IsCLREnabled();
		ConfigHandler.SetRecordTraceFileDirectory(databaseOperation);
	}

	private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		RunWorkerCompleted();
	}

	private void RunWorkerCompleted()
	{
		Close();
	}

	private class RunWorkerArgument
	{
		public readonly DatabaseOperation DatabaseOperation;
		public readonly DatabaseOperation PreviousConnection;
		public readonly string SessionnId;

		public RunWorkerArgument(DatabaseOperation databaseOperation, DatabaseOperation previousConnection, string sessionId)
		{
			DatabaseOperation = databaseOperation;
			PreviousConnection = previousConnection;
			SessionnId = sessionId;
		}
	}
}
