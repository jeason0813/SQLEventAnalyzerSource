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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

public partial class MainForm : Form
{
	private delegate void CustomColumnsChangedEventHandler();
	private event CustomColumnsChangedEventHandler CustomColumnsChangedEvent;

	private delegate void CustomColumnsUpdatePendingEventHandler();
	private event CustomColumnsUpdatePendingEventHandler CustomColumnsUpdatePendingEvent;

	[DllImport("User32.dll")]
	private static extern IntPtr SetForegroundWindow(int hWnd);

	private DatabaseOperation _databaseOperation;
	private bool _customColumnsChanged;
	private bool _unattended;
	private readonly string _postScriptFileName;
	private readonly string _postScript;
	private readonly bool _recordingMode;
	private int _customColumnsWindowHandle;
	private readonly string _unattendedConnectToSessionId;
	private string _lastImportFile;
	private readonly int _numberOfImportFiles;
	private readonly int _compressTraceFilesAfterImport;
	private readonly bool _deleteTraceFilesAfterImport;
	private readonly string _unattendedImportPath;
	private readonly string _useSessionId;
	private int _unattendedExitCode;
	private readonly bool _verboseMode;
	private readonly bool _forceDelete;
	private readonly bool _unattendedUseSessionParameter;
	private readonly string _unattendedConnectionStringFromCmdLine;
	private CheckForUpdatesForm _checkForUpdatesForm;
	private CustomColumnsCheckForUpdatesForm _customColumnsCheckForUpdatesForm;
	private readonly List<string> _unattendedGenerateStatisticsNames;
	private readonly List<string> _unattendedGenerateStatisticsFilter1Names;
	private readonly List<string> _unattendedGenerateStatisticsFilter2Names;
	private readonly string _unattendedGenerateStatisticsSavePath;
	private readonly List<string> _unattendedGenerateStatisticsSaveWebService;

	public MainForm()
	{
		DynamicAssembly.EnableDynamicLoadingForDlls(Assembly.GetExecutingAssembly(), "SQLEventAnalyzer.Resources.Assemblies");
		PreInitialize();
		InitializeDatabaseOperation(false);
		Initialize();
		CheckForCustomColumnsUpdate();
	}

	public MainForm(string columnsXmlFileName, string connectionStringFromCmdLine, string sqlEventAnalyzerDatabaseName)
	{
		_unattendedConnectionStringFromCmdLine = connectionStringFromCmdLine;

		if (sqlEventAnalyzerDatabaseName != null)
		{
			ConfigHandler.DatabaseName = sqlEventAnalyzerDatabaseName;
		}

		DynamicAssembly.EnableDynamicLoadingForDlls(Assembly.GetExecutingAssembly(), "SQLEventAnalyzer.Resources.Assemblies");
		PreInitialize();
		InitializeDatabaseOperation(false);
		Initialize();

		bool success = true;

		if (columnsXmlFileName != null)
		{
			ManageColumnsForm form = new ManageColumnsForm();
			form.Initialize();
			success = form.LoadColumnXmlFile(columnsXmlFileName);
		}

		if (!success)
		{
			_unattendedExitCode = -1;
			Close();
		}
	}

	public MainForm(string postScriptFileName, string postScript, int numberOfImportFiles, string lastImportFile, string importNewerThanSessionId, string sessionId, int compressTraceFilesAfterImport, bool deleteTraceFilesAfterImport, string importPath, string columnsXmlFileName, string connectionStringFromCmdLine, bool forceDelete, string sqlEventAnalyzerDatabaseName, List<string> generateStatisticsNames, List<string> generateStatisticsFilter1Names, List<string> generateStatisticsFilter2Names, string generateStatisticsSavePath, List<string> generateStatisticsSaveWebService)
	{
		_unattended = true;
		_unattendedImportPath = importPath;
		_unattendedConnectionStringFromCmdLine = connectionStringFromCmdLine;
		_forceDelete = forceDelete;
		_unattendedGenerateStatisticsNames = generateStatisticsNames;
		_unattendedGenerateStatisticsFilter1Names = generateStatisticsFilter1Names;
		_unattendedGenerateStatisticsFilter2Names = generateStatisticsFilter2Names;
		_unattendedGenerateStatisticsSavePath = generateStatisticsSavePath;
		_unattendedGenerateStatisticsSaveWebService = generateStatisticsSaveWebService;

		if (sqlEventAnalyzerDatabaseName != null)
		{
			ConfigHandler.DatabaseName = sqlEventAnalyzerDatabaseName;
		}

		if (sessionId != null)
		{
			_unattendedUseSessionParameter = true;
		}

		bool success = true;

		if (!Directory.Exists(_unattendedImportPath))
		{
			OutputHandler.WriteToLog(string.Format("Error: Import Path \"{0}\" does not exist.", _unattendedImportPath));
			success = false;
		}

		if (success)
		{
			DynamicAssembly.EnableDynamicLoadingForDlls(Assembly.GetExecutingAssembly(), "SQLEventAnalyzer.Resources.Assemblies");
			PreInitialize();
			InitializeDatabaseOperation(true);
			Initialize();

			_postScriptFileName = postScriptFileName;
			_postScript = postScript;
			_numberOfImportFiles = numberOfImportFiles;
			_compressTraceFilesAfterImport = compressTraceFilesAfterImport;
			_deleteTraceFilesAfterImport = deleteTraceFilesAfterImport;
			_unattendedConnectToSessionId = null;

			if (columnsXmlFileName != null)
			{
				ManageColumnsForm form = new ManageColumnsForm();
				form.Initialize();
				success = form.LoadColumnXmlFile(columnsXmlFileName);
			}
		}

		if (success)
		{
			if (importNewerThanSessionId != null)
			{
				_unattendedConnectToSessionId = importNewerThanSessionId;

				success = UnattendedCheckIfTempTableExists();

				if (success)
				{
					success = UnattendedCheckLastImportedFileName(importNewerThanSessionId);

					if (!success) // TraceData table does not contain any information on previously imported trace files = the TraceData table is empty
					{
						_numberOfImportFiles = 0; // import all files found instead
						numberOfImportFiles = 0;
						importNewerThanSessionId = null;
						success = true;
					}
				}
			}
			else if (sessionId != null)
			{
				_unattendedConnectToSessionId = sessionId;
			}
		}

		if (!success || (numberOfImportFiles == -1 && lastImportFile == null && importNewerThanSessionId == null))
		{
			_unattendedExitCode = -1;
			Close();
		}
	}

	public MainForm(bool recordingMode, string columnsXmlFileName, string connectionStringFromCmdLine, string sqlEventAnalyzerDatabaseName)
	{
		_unattendedConnectionStringFromCmdLine = connectionStringFromCmdLine;

		if (sqlEventAnalyzerDatabaseName != null)
		{
			ConfigHandler.DatabaseName = sqlEventAnalyzerDatabaseName;
		}

		bool success = true;

		DynamicAssembly.EnableDynamicLoadingForDlls(Assembly.GetExecutingAssembly(), "SQLEventAnalyzer.Resources.Assemblies");
		PreInitialize();
		InitializeDatabaseOperation(recordingMode);
		Initialize();

		if (columnsXmlFileName != null)
		{
			ManageColumnsForm form = new ManageColumnsForm();
			form.Initialize();
			success = form.LoadColumnXmlFile(columnsXmlFileName);
		}

		if (success)
		{
			_recordingMode = recordingMode;
		}
		else
		{
			_unattendedExitCode = -2;
			Close();
		}
	}

	public MainForm(string useSessionId, string columnsXmlFileName, bool verboseMode, string connectionStringFromCmdLine, bool forceDelete, string applicationName, string sqlEventAnalyzerDatabaseName)
	{
		_unattendedConnectionStringFromCmdLine = connectionStringFromCmdLine;
		_forceDelete = forceDelete;
		_unattendedUseSessionParameter = true;

		if (sqlEventAnalyzerDatabaseName != null)
		{
			ConfigHandler.DatabaseName = sqlEventAnalyzerDatabaseName;
		}

		bool success = true;

		DynamicAssembly.EnableDynamicLoadingForDlls(Assembly.GetExecutingAssembly(), "SQLEventAnalyzer.Resources.Assemblies");
		PreInitialize();

		if (verboseMode)
		{
			dataViewUserControl1.SetVerboseMode();
			customColumnsToolStripMenuItem.Visible = false;
			recordToolStripMenuItem.Visible = false;
			changeConnectionToolStripMenuItem.Visible = false;
			toolStripSeparator2.Visible = false;
			toolStripSeparator3.Visible = false;
			recordToolStripMenuItem.ShortcutKeys = Keys.None;
			customColumnsToolStripMenuItem.ShortcutKeys = Keys.None;
			showHiddenColumnsToolStripMenuItem.Visible = false;

			if (applicationName != null)
			{
				GenericHelper.ApplicationName = applicationName;
			}
		}

		InitializeDatabaseOperation(false);
		Initialize();

		if (columnsXmlFileName != null)
		{
			ManageColumnsForm form = new ManageColumnsForm();
			form.Initialize();
			success = form.LoadColumnXmlFile(columnsXmlFileName);
		}

		if (success)
		{
			_useSessionId = useSessionId;
			_verboseMode = verboseMode;
		}
		else
		{
			_unattendedExitCode = -3;
			Close();
		}
	}

	public int GetUnattendedExitCode()
	{
		return _unattendedExitCode;
	}

	public void HandleUnattended()
	{
		OutputHandler.WriteToLog(string.Format("Custom Columns version: {0}", ColumnHelper.GetVersion()));

		bool success = UnattendedImportFromTraceFiles();

		if (!success)
		{
			_unattendedExitCode = -6;
		}

		if (success)
		{
			success = UnattendedRunPostScript();

			if (!success)
			{
				_unattendedExitCode = -4;
			}

			if (success)
			{
				if (_unattendedConnectToSessionId != null)
				{
					UnattendedConnectToExistingSession();
				}

				string sevenZipFileName = string.Format(@"{0}\7za{1}.exe", GenericHelper.TempPath, Guid.NewGuid());

				if (_unattendedGenerateStatisticsNames.Count != 0)
				{
					success = GenerateStatistics(sevenZipFileName);

					if (!success)
					{
						_unattendedExitCode = -9;
					}
				}

				if (success && _compressTraceFilesAfterImport != -1)
				{
					success = UnattendedCompressTraceFilesAfterImport(_compressTraceFilesAfterImport, sevenZipFileName);

					if (!success)
					{
						_unattendedExitCode = -7;
					}
				}

				if (success)
				{
					if (_deleteTraceFilesAfterImport)
					{
						success = UnattendedDeleteTraceFilesAfterImport();

						if (!success)
						{
							_unattendedExitCode = -5;
						}
					}
				}

				if (_compressTraceFilesAfterImport != -1 || _deleteTraceFilesAfterImport)
				{
					inputSourceUserControl1.RefreshListView();
				}
			}
		}

		OutputHandler.WriteToLog(string.Format("Success: {0}", success));

		if (!success)
		{
			_unattended = false;
		}
		else
		{
			if (GenericHelper.IsUserInteractive())
			{
				Close();
			}
			else
			{
				Cleanup();
				Close();
			}
		}
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
			StartRecordingMode();
		}
		else if (_useSessionId != null)
		{
			StartUseSessionMode();
		}
		else if (_unattended)
		{
			HandleUnattended();
		}
		else
		{
			inputSourceUserControl1.SelectControl();
		}
	}

	private void PreInitialize()
	{
		ConfigHandler.LoadConfig();
		Translator.GenerateDictionary();

		InitializeComponent();

		CheckForUpdates();

		SplashForm splashForm = new SplashForm();
		ShowInTaskbar = false;
		splashForm.Begin(this);

		SessionHelper.LoadLastSession();

		ShowInTaskbar = true;
		splashForm.End();
	}

	private void Initialize()
	{
		InitializeDictionary();

		Text = GenericHelper.ApplicationName;

		SetSize();

		MinimumSize = new Size(900, 680); // error in .NET

		showHiddenColumnsToolStripMenuItem.Checked = ConfigHandler.ShowHiddenColumns;

		inputSourceUserControl1.Initialize(_databaseOperation);
		inputSourceUserControl1.HandleListView(_unattended, _unattendedImportPath);

		dataViewUserControl1.Initialize(_databaseOperation);

		inputSourceUserControl1.ShowDataViewEvent += InputSourceUserControl1_ShowDataViewEvent;
		dataViewUserControl1.ShowInputSourceEvent += DataViewUserControl1_ShowInputSourceEvent;
		dataViewUserControl1.SetMenuItemCheckBoxEvent += DataViewUserControl1_SetMenuItemCheckBoxEvent;
		CustomColumnsChangedEvent += MainForm_CustomColumnsChangedEvent;
		CustomColumnsUpdatePendingEvent += MainForm_CustomColumnsUpdatePendingEvent;

		SetTracingFunctionality();
	}

	private void DataViewUserControl1_SetMenuItemCheckBoxEvent(string menuItemName, bool value)
	{
		if (menuItemName == "FileName")
		{
			fileNameToolStripMenuItem.Checked = value;
		}
		else if (menuItemName == "Type")
		{
			typeToolStripMenuItem.Checked = value;
		}
		else if (menuItemName == "SPID")
		{
			sPIDToolStripMenuItem.Checked = value;
		}
		else if (menuItemName == "Duration")
		{
			durationToolStripMenuItem.Checked = value;
		}
		else if (menuItemName == "Reads")
		{
			readsToolStripMenuItem.Checked = value;
		}
		else if (menuItemName == "Writes")
		{
			writesToolStripMenuItem.Checked = value;
		}
		else if (menuItemName == "CPU")
		{
			cpuToolStripMenuItem.Checked = value;
		}
		else if (menuItemName == "Rows")
		{
			rowsToolStripMenuItem.Checked = value;
		}
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			fileToolStripMenuItem.Text = Translator.GetText("fileToolStripMenuItem");
			toolsToolStripMenuItem.Text = Translator.GetText("toolsToolStripMenuItem");
			viewToolStripMenuItem.Text = Translator.GetText("viewToolStripMenuItem");
			helpToolStripMenuItem.Text = Translator.GetText("helpToolStripMenuItem");
			helpToolStripMenuItem1.Text = Translator.GetText("helpToolStripMenuItem1");
			changeConnectionToolStripMenuItem.Text = Translator.GetText("changeConnectionToolStripMenuItem");
			exitToolStripMenuItem.Text = Translator.GetText("exitToolStripMenuItem");
			optionsToolStripMenuItem.Text = Translator.GetText("optionsToolStripMenuItem");
			showHiddenColumnsToolStripMenuItem.Text = Translator.GetText("showHiddenColumnsToolStripMenuItem");
			aboutToolStripMenuItem.Text = Translator.GetText("aboutToolStripMenuItem");
			infoToolStripMenuItem.Text = Translator.GetText("infoToolStripMenuItem");
			customColumnsToolStripMenuItem.Text = Translator.GetText("customColumnsToolStripMenuItem");
			exportToolStripMenuItem.Text = Translator.GetText("exportToolStripButton1");
			standardColumnsToolStripMenuItem.Text = Translator.GetText("standardColumnsToolStripMenuItem");
			recordToolStripMenuItem.Text = Translator.GetText("recordToolStripMenuItem");
			commandLineParametersToolStripMenuItem.Text = Translator.GetText("commandLineParametersToolStripMenuItem");
			timelineToolStripMenuItem.Text = Translator.GetText("Timeline");
			refreshToolStripMenuItem.Text = Translator.GetText("Refresh");
			checkForupdatesToolStripMenuItem.Text = Translator.GetText("CheckForUpdates");
		}
	}

	private void SetSize()
	{
		int x = Convert.ToInt32(ConfigHandler.WindowSize.Split(';')[0]);
		int y = Convert.ToInt32(ConfigHandler.WindowSize.Split(';')[1]);

		if (x > Screen.PrimaryScreen.Bounds.Width || y > Screen.PrimaryScreen.Bounds.Height)
		{
			WindowState = FormWindowState.Maximized;
			return;
		}

		if (x >= MinimumSize.Width && y >= MinimumSize.Height)
		{
			Size = new Size(x, y);
		}
	}

	private void DataViewUserControl1_ShowInputSourceEvent()
	{
		inputSourceUserControl1.Visible = true;
		dataViewUserControl1.Visible = false;
		showHiddenColumnsToolStripMenuItem.Enabled = false;
		standardColumnsToolStripMenuItem.Enabled = false;
		exportToolStripMenuItem.Enabled = false;
		timelineToolStripMenuItem.Enabled = false;
		inputSourceUserControl1.RefreshSessionListView();
		inputSourceUserControl1.SelectControl();
	}

	private bool InputSourceUserControl1_ShowDataViewEvent(bool handleColumns, bool importFromExistingTable, bool connectToExistingSession, bool connectToCurrentSession)
	{
		bool success = true;

		if (handleColumns || _customColumnsChanged)
		{
			success = dataViewUserControl1.HandleColumns(importFromExistingTable);
		}

		if (importFromExistingTable)
		{
			if (handleColumns)
			{
				ConfigHandler.ImportStartTime = DateTime.Now;

				ImportSessionForm form = new ImportSessionForm();
				form.Initialize(_databaseOperation, inputSourceUserControl1.GetImportSessionInfoList());

				if (GenericHelper.IsUserInteractive())
				{
					form.ShowDialog();
				}

				ConfigHandler.ImportEndTime = DateTime.Now;
			}
		}

		if (connectToExistingSession || (_customColumnsChanged && connectToCurrentSession))
		{
			_customColumnsChanged = false;
			dataViewUserControl1.SetFilter1Applied(false);
			dataViewUserControl1.SetFilter2Applied(false);
			dataViewUserControl1.ReloadDataViewer(true, true, !IsSortingColumnStillActive());
		}

		if (_customColumnsChanged)
		{
			_customColumnsChanged = false;
			dataViewUserControl1.SetFilter2Applied(false);
			inputSourceUserControl1.SetImportNew(true);
		}

		if (handleColumns)
		{
			dataViewUserControl1.SetFilter1Applied(false);
			dataViewUserControl1.ReloadDataViewer(true, false, !IsSortingColumnStillActive());
		}

		inputSourceUserControl1.Visible = false;
		dataViewUserControl1.Visible = true;
		showHiddenColumnsToolStripMenuItem.Enabled = true;
		standardColumnsToolStripMenuItem.Enabled = true;
		exportToolStripMenuItem.Enabled = true;
		timelineToolStripMenuItem.Enabled = true;

		return success;
	}

	private bool UnattendedCompressTraceFilesAfterImport(int filesToKeep, string sevenZipFileName)
	{
		return inputSourceUserControl1.CompressCheckedTraceFiles(filesToKeep, sevenZipFileName);
	}

	private bool UnattendedDeleteTraceFilesAfterImport()
	{
		return inputSourceUserControl1.DeleteCheckedTraceFiles();
	}

	private bool UnattendedCheckIfTempTableExists()
	{
		bool success = _databaseOperation.DoesTempTableExist(_unattendedConnectToSessionId);

		if (!success)
		{
			OutputHandler.Show(string.Format("Session \"{0}\" does not exist.", _unattendedConnectToSessionId), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		return success;
	}

	private bool UnattendedCheckLastImportedFileName(string importNewerThanSessionId)
	{
		bool success = false;

		_lastImportFile = _databaseOperation.GetLastImportedFileName(importNewerThanSessionId);

		if (_lastImportFile != null)
		{
			success = true;
		}

		if (!success)
		{
			OutputHandler.WriteToLog("Session does not contain any information on previous imported Trace Files.\r\n\r\nAll Trace Files found will be imported.");
		}

		return success;
	}

	private bool UnattendedImportFromTraceFiles()
	{
		bool success = inputSourceUserControl1.HandleUnattended(_numberOfImportFiles, _lastImportFile);
		return success;
	}

	private bool UnattendedRunPostScript()
	{
		bool success = true;

		ErrorFormParams errorFormParams = dataViewUserControl1.RunPostScriptFile(_postScriptFileName);

		if (errorFormParams != null)
		{
			success = false;
		}

		if (success)
		{
			errorFormParams = dataViewUserControl1.RunPostScript(_postScript);

			if (errorFormParams != null)
			{
				success = false;
			}
		}

		if (!success)
		{
			ErrorFormHandler errorFormHandler = new ErrorFormHandler();

			if (!ConfigHandler.ErrorFormShown)
			{
				ConfigHandler.ErrorFormShown = true;
				errorFormHandler.ErrorOccuredEvent(errorFormParams.OkButtonText, errorFormParams.Message, errorFormParams.Sql);
			}
		}

		return success;
	}

	private void UnattendedConnectToExistingSession()
	{
		string sessionIdImported = GenericHelper.GetSessionIdFromTableName();

		OutputHandler.WriteToLog(string.Format("Events imported to Session Id \"{0}\" via temporary Session Id \"{1}\"", _unattendedConnectToSessionId, sessionIdImported));

		DataViewUserControl1_ShowInputSourceEvent();
		inputSourceUserControl1.ConnectToSessionId(_unattendedConnectToSessionId);
		inputSourceUserControl1.ImportSessionToCurrentSession(sessionIdImported);

		_databaseOperation.DropTempTable(string.Format("TraceData_{0}", sessionIdImported.Replace("'", "''")));
	}

	private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
	{
		AboutForm form = new AboutForm();
		form.ShowDialog();
	}

	private void InfoToolStripMenuItem_Click(object sender, EventArgs e)
	{
		InfoForm form = new InfoForm();
		form.SetValues();
		form.ShowDialog();
	}

	private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		Cleanup();
	}

	private void Cleanup()
	{
		Hide();

		if (ConfigHandler.EnableCLRTemporary)
		{
			_databaseOperation.DisableCLR();
		}

		if ((ModifierKeys != Keys.Shift && !ConfigHandler.KeepSessionOnExit && !_unattendedUseSessionParameter) || _forceDelete)
		{
			if (!inputSourceUserControl1.GetManuallyUseSession() || (inputSourceUserControl1.GetManuallyUseSession() && GenericHelper.ConfirmDropTempTable(GenericHelper.GetSessionIdFromTableName())))
			{
				_databaseOperation.DropTempTable(GenericHelper.TempTableName.Replace("'", "''"));
			}
		}

		_databaseOperation.Dispose();

		if (!_unattended && ConfigHandler.RegistryModifyAccess)
		{
			SessionHelper.SaveSession();
		}
	}

	private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		bool previousUseTranslationValue = ConfigHandler.UseTranslation;

		SettingsForm form = new SettingsForm();
		form.Initialize(_databaseOperation);
		form.ShowDialog();

		Application.DoEvents();

		if (form.RestartRequired)
		{
			string text = "Application restart required for changes to take effect.\r\n\r\nApplication will now exit.";

			if (ConfigHandler.UseTranslation)
			{
				if (!previousUseTranslationValue)
				{
					Translator.GenerateDictionary();
				}

				text = Translator.GetText("RestartRequired");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			Application.DoEvents();
			Close();
		}

		if (form.ResetLayout)
		{
			WindowState = FormWindowState.Normal;
			SetSize();
			dataViewUserControl1.SetSplitter();

			if (dataViewUserControl1.Visible)
			{
				dataViewUserControl1.UpdateColumnWidthAndOrder();
			}
		}

		if (form.ItemsPerPageChanged || form.EnableQuickSearchChanged)
		{
			if (ConfigHandler.TempTableCreated)
			{
				dataViewUserControl1.ReloadDataViewer(false, false, !IsSortingColumnStillActive());
			}
		}

		if (form.ItemsPerPageChanged && ConfigHandler.TempTableCreated)
		{
			dataViewUserControl1.SetItemsPerPage();
		}
	}

	private void ShowHiddenColumnsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ConfigHandler.ShowHiddenColumns = showHiddenColumnsToolStripMenuItem.Checked;
		ConfigHandler.SaveConfig();

		dataViewUserControl1.ToggleShowHiddenColumns();
		dataViewUserControl1.ReloadDataViewer(false, false, !IsSortingColumnStillActive());
	}

	private void MainForm_Resize(object sender, EventArgs e)
	{
		if (Width < MinimumSize.Width || Width == 0)
		{
			return;
		}

		ConfigHandler.WindowSize = string.Format("{0}; {1}", Size.Width, Size.Height);
		ConfigHandler.SaveConfig();
	}

	private void InitializeDatabaseOperation(bool unattended)
	{
		ConnectionDialogForm form = new ConnectionDialogForm();
		form.Initialize(_databaseOperation, unattended, _unattendedConnectionStringFromCmdLine);

		if (GenericHelper.IsUserInteractive())
		{
			form.ShowDialog();
		}
		else
		{
			form.OkButtonClick();
		}

		Application.DoEvents();

		if (form.ConnectionChanged)
		{
			_databaseOperation = form.GetDatabaseOperation();

			ChangeConnectionForm form1 = new ChangeConnectionForm();
			form1.Initialize(_databaseOperation, this);
		}
		else
		{
			if (_databaseOperation == null)
			{
				Environment.Exit(-1);
			}
		}
	}

	private void ChangeConnectionToolStripMenuItem_Click(object sender, EventArgs e)
	{
		dataViewUserControl1.CloseSearchForm();

		DatabaseOperation previousConnection = new DatabaseOperation();
		previousConnection.ChangeConnection(_databaseOperation.GetConnectionString());
		string previousSessionId = GenericHelper.GetSessionIdFromTableName();

		ConnectionDialogForm form = new ConnectionDialogForm();
		form.ShowInTaskbar = false;
		form.Initialize(_databaseOperation, false, null);

		if (GenericHelper.IsUserInteractive())
		{
			form.ShowDialog();
		}
		else
		{
			form.OkButtonClick();
		}

		Application.DoEvents();

		if (form.ConnectionChanged)
		{
			Enabled = false;

			dataViewUserControl1.SetFilter1Applied(false);
			dataViewUserControl1.SetFilter2Applied(false);

			ChangeConnectionForm form1 = new ChangeConnectionForm();
			form1.ShowInTaskbar = false;
			form1.Initialize(_databaseOperation, this, previousConnection, form.ShiftPressed, inputSourceUserControl1.GetManuallyUseSession(), previousSessionId);

			GenericHelper.KillPreviousConnection(previousConnection, previousSessionId);

			Enabled = true;

			inputSourceUserControl1.SetManuallyUseSession(false);
			inputSourceUserControl1.HandleListView(false, null);
			inputSourceUserControl1.SetImportNew(true);
			DataViewUserControl1_ShowInputSourceEvent();

			SetTracingFunctionality();
		}

		previousConnection.Dispose();
	}

	private void CustomColumnsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!ConfigHandler.CustomColumnsFormShown)
		{
			ConfigHandler.CustomColumnsFormShown = true;

			Thread thread = new Thread(ShowColumnsForm);
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
		}
		else
		{
			SetForegroundWindow(_customColumnsWindowHandle);
		}
	}

	private void ShowColumnsForm()
	{
		ManageColumnsForm form = new ManageColumnsForm();
		_customColumnsWindowHandle = (int)form.Handle;
		form.Initialize();
		Application.Run(form);

		bool anyChanges = form.AnyChanges();

		if (anyChanges && !ConfigHandler.GetDataFormShown)
		{
			if (dataViewUserControl1.Visible)
			{
				if (Visible)
				{
					BeginInvoke(CustomColumnsChangedEvent);
				}
			}
			else
			{
				_customColumnsChanged = true;
			}
		}

		inputSourceUserControl1.SetImportNew(_customColumnsChanged);
		ConfigHandler.CustomColumnsFormShown = false;

		if (Visible)
		{
			BeginInvoke(CustomColumnsUpdatePendingEvent);
		}
	}

	private void HandleDataViewerAfterCustomColumnsChange()
	{
		dataViewUserControl1.SetFilter2Applied(false);
		dataViewUserControl1.ReloadDataViewer(true, false, !IsSortingColumnStillActive());
	}

	private bool IsSortingColumnStillActive()
	{
		string sortingColumn = dataViewUserControl1.GetSortingColumn();

		foreach (Column column in ColumnHelper.EnabledColumns)
		{
			if (column.Name == sortingColumn)
			{
				return true;
			}
		}

		return false;
	}

	private void MainForm_CustomColumnsChangedEvent()
	{
		if (WindowState == FormWindowState.Minimized)
		{
			WindowState = FormWindowState.Normal;
		}

		bool success = FillParameterForm.CheckForUniqueParameterNames();

		if (success)
		{
			success = FillParameterForm.FillMissingParameters();
		}

		if (success)
		{
			SetForegroundWindow((int)Handle);

			dataViewUserControl1.HandleColumns(false);
			HandleDataViewerAfterCustomColumnsChange();
		}
	}

	private void MainForm_CustomColumnsUpdatePendingEvent()
	{
		SetRestartToUpdate();
	}

	private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ExportForm form = new ExportForm();
		form.Initialize(_databaseOperation, dataViewUserControl1.GetDataViewer(), dataViewUserControl1.GetDataViewerParameters());
		form.ShowDialog();
	}

	private void FileNameToolStripMenuItem_Click(object sender, EventArgs e)
	{
		dataViewUserControl1.ToggleShowHideColumn("FileName");
	}

	private void TypeToolStripMenuItem_Click(object sender, EventArgs e)
	{
		dataViewUserControl1.ToggleShowHideColumn("Type");
	}

	private void SPIDToolStripMenuItem_Click(object sender, EventArgs e)
	{
		dataViewUserControl1.ToggleShowHideColumn("SPID");
	}

	private void DurationToolStripMenuItem_Click(object sender, EventArgs e)
	{
		dataViewUserControl1.ToggleShowHideColumn("Duration");
	}

	private void ReadsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		dataViewUserControl1.ToggleShowHideColumn("Reads");
	}

	private void WritesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		dataViewUserControl1.ToggleShowHideColumn("Writes");
	}

	private void CpuToolStripMenuItem_Click(object sender, EventArgs e)
	{
		dataViewUserControl1.ToggleShowHideColumn("CPU");
	}

	private void RowsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		dataViewUserControl1.ToggleShowHideColumn("Rows");
	}

	private void RecordToolStripMenuItem_Click(object sender, EventArgs e)
	{
		StartRecordingMode();
	}

	private void CommandLineParametersToolStripMenuItem_Click(object sender, EventArgs e)
	{
		CommandLineParametersForm form = new CommandLineParametersForm();
		form.Initialize();
		form.ShowDialog();
	}

	private void HelpToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		const string helpFile = "SQLEventAnalyzer.pdf";

		if (File.Exists(helpFile))
		{
			try
			{
				Process.Start(helpFile);
			}
			catch (Exception ex)
			{
				OutputHandler.Show(ex.Message, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
		else
		{
			string text = "Help file not found.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("helpFileNotFound");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}

	private void TimelineToolStripMenuItem_Click(object sender, EventArgs e)
	{
		dataViewUserControl1.ShowTimeLine();
	}

	private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (dataViewUserControl1.Visible)
		{
			dataViewUserControl1.RefreshData();
		}
		else
		{
			inputSourceUserControl1.RefreshListView();
		}
	}

	private void SetTracingFunctionality()
	{
		if (ConfigHandler.UseExtendedEvents == "")
		{
			if (_databaseOperation.GetSqlServerVersion() >= 11)
			{
				ConfigHandler.UseExtendedEvents = "True";
			}
			else
			{
				ConfigHandler.UseExtendedEvents = "False";
			}
		}
		else
		{
			if (_databaseOperation.GetSqlServerVersion() < 11)
			{
				ConfigHandler.UseExtendedEvents = "False";
			}
		}
	}

	private void StartRecordingMode()
	{
		bool success = FillParameterForm.CheckForUniqueParameterNames();

		if (success)
		{
			success = FillParameterForm.FillMissingParameters();
		}

		if (success)
		{
			TraceRecordingForm form = new TraceRecordingForm();
			form.Initialize(_databaseOperation, _recordingMode);
			form.ShowDialog();

			success = form.GetSuccess();

			if (success)
			{
				inputSourceUserControl1.SetImportNew(true);
				_databaseOperation.SetForceRecalculateTotalRows(true);
				InputSourceUserControl1_ShowDataViewEvent(true, false, false, false);
			}
		}
	}

	private void StartUseSessionMode()
	{
		inputSourceUserControl1.UseSession(_useSessionId, _verboseMode);
	}

	private void CheckForupdatesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		CheckForUpdatesForm form = new CheckForUpdatesForm();
		form.ShowDialog();
	}

	private void CheckForUpdates()
	{
		if (GenericHelper.IsUserInteractive() && !_unattended && Convert.ToBoolean(ConfigHandler.CheckForUpdatesOnStart))
		{
			_checkForUpdatesForm = new CheckForUpdatesForm();
			_checkForUpdatesForm.UpdateCheckCompleteEvent += UpdateCheckCompleteEvent;
			_checkForUpdatesForm.CheckForUpdates();
		}
	}

	private void UpdateCheckCompleteEvent(string errorMessage, Version version, bool anyUpdates, bool anyErrors)
	{
		if (!anyErrors && anyUpdates)
		{
			_checkForUpdatesForm.ShowDialog();

			if (_checkForUpdatesForm.GetForceQuit())
			{
				Environment.Exit(0);
			}
		}
	}

	private void CheckForCustomColumnsUpdate()
	{
		if (ColumnHelper.GetAutomaticUpdateEnabled())
		{
			_customColumnsCheckForUpdatesForm = new CustomColumnsCheckForUpdatesForm(ColumnHelper.ColumnCollectionFileName);
			_customColumnsCheckForUpdatesForm.UpdateCheckCompleteEvent += CustomColumnsUpdateCheckCompleteEvent;
			_customColumnsCheckForUpdatesForm.CheckForUpdates();
		}
	}

	private void CustomColumnsUpdateCheckCompleteEvent(string errorMessage, Version version, bool anyUpdates, bool anyErrors)
	{
		if (!anyErrors && anyUpdates)
		{
			GenericHelper.CustomColumnsUpdatePending = true;
			SetRestartToUpdate();
		}
	}

	private void SetRestartToUpdate()
	{
		if (GenericHelper.CustomColumnsUpdatePending)
		{
			if (ConfigHandler.UseTranslation)
			{
				restartToUpdateTextBox.Text = Translator.GetText("restartToUpdateLabel");
			}

			restartToUpdateTextBox.Visible = true;
		}
	}

	private void RestartToUpdateTextBox_MouseDown(object sender, MouseEventArgs e)
	{
		GenericHelper.HideCaret(restartToUpdateTextBox);
	}

	private bool GenerateStatistics(string sevenZipFileName)
	{
		bool success = true;

		string tempPath = string.Format(@"{0}\{1}", GenericHelper.TempPath, Guid.NewGuid());
		string tempPathSendToWebService = string.Format(@"{0}\{1}", GenericHelper.TempPath, Guid.NewGuid());

		for (int i = 0; i < _unattendedGenerateStatisticsNames.Count; i++)
		{
			string statisticsName = _unattendedGenerateStatisticsNames[i];

			string filter1Name = null;

			if (_unattendedGenerateStatisticsFilter1Names.Count > 0)
			{
				filter1Name = _unattendedGenerateStatisticsFilter1Names[i];
			}

			string filter2Name = null;

			if (_unattendedGenerateStatisticsFilter2Names.Count > 0)
			{
				filter2Name = _unattendedGenerateStatisticsFilter2Names[i];
			}

			WriteGenerateStatisticsLogInfo(statisticsName, filter1Name, filter2Name);
			success = dataViewUserControl1.InititalizeStatisticsGeneration(statisticsName, filter1Name, filter2Name, tempPath, _unattendedGenerateStatisticsSaveWebService, tempPathSendToWebService, i);

			if (success)
			{
				OutputHandler.WriteToLog(string.Format("Generating statistics using \"{0}\": Completed", statisticsName));
			}
			else
			{
				OutputHandler.WriteToLog(string.Format("Generating statistics using \"{0}\": Failed", statisticsName));
			}
		}

		if (success)
		{
			success = CompressStatistics(_unattendedGenerateStatisticsSavePath, tempPath, tempPathSendToWebService, _unattendedGenerateStatisticsSaveWebService, sevenZipFileName);
		}

		return success;
	}

	private static bool CompressStatistics(string saveToPath, string tempPath, string tempPathSendToWebService, List<string> sendToWebServiceNames, string sevenZipFileName)
	{
		string archiveName = string.Format(@"{0}\Statistics.zip", GenericHelper.TempPath);

		bool success = CompressStatisticsDirectory(tempPath, archiveName, sevenZipFileName);

		if (saveToPath != null)
		{
			if (Directory.Exists(saveToPath))
			{
				try
				{
					File.Copy(archiveName, string.Format(@"{0}\Statistics.zip", saveToPath), true);
				}
				catch (Exception ex)
				{
					OutputHandler.WriteToLog(string.Format("Error copying Statistics.zip to \"{0}\". Error: {1}", saveToPath, ex.Message));
					success = false;
				}
			}
			else
			{
				OutputHandler.WriteToLog(string.Format("Error: Directory \"{0}\" does not exist", saveToPath));
				success = false;
			}
		}

		if (success)
		{
			try
			{
				foreach (string file in Directory.GetFiles(tempPath))
				{
					GenericHelper.DeleteFile(file);
				}

				Directory.Delete(tempPath);
			}
			catch
			{
			}
		}

		if (success)
		{
			try
			{
				GenericHelper.DeleteFile(archiveName);
			}
			catch
			{
			}
		}

		if (sendToWebServiceNames.Count > 0)
		{
			if (success)
			{
				success = CompressStatisticsDirectory(tempPathSendToWebService, archiveName, sevenZipFileName);
			}

			if (success)
			{
				try
				{
					foreach (string file in Directory.GetFiles(tempPathSendToWebService))
					{
						GenericHelper.DeleteFile(file);
					}

					Directory.Delete(tempPathSendToWebService);
				}
				catch
				{
				}
			}

			if (success)
			{
				ConfigHandler.StatisticsArchive = archiveName;
			}
		}

		return success;
	}

	private static bool CompressStatisticsDirectory(string tempPath, string archiveName, string sevenZipFileName)
	{
		bool success = true;

		try
		{
			GenericHelper.WriteFile(sevenZipFileName, SQLEventAnalyzer.Properties.Resources._7za);
		}
		catch (Exception ex)
		{
			OutputHandler.WriteToLog(string.Format("Error writing 7za.exe in CompressStatisticsDirectory: \"{0}\"", ex.Message));
			success = false;
		}

		if (success)
		{
			try
			{
				CompressionHandler.CompressDirectory(tempPath, archiveName, sevenZipFileName);
			}
			catch (Exception ex)
			{
				OutputHandler.WriteToLog(string.Format("Error creating statistics archive: \"{0}\"", ex.Message));
				success = false;
			}
		}

		if (success)
		{
			try
			{
				GenericHelper.DeleteFile(sevenZipFileName);
			}
			catch
			{
			}
		}

		return success;
	}

	private static void WriteGenerateStatisticsLogInfo(string statisticsName, string filter1Name, string filter2Name)
	{
		string filter1 = "";
		string filter2 = "";

		if (!string.IsNullOrEmpty(filter1Name))
		{
			filter1 = string.Format("Filter 1 \"{0}\" applied", filter1Name);
		}

		if (!string.IsNullOrEmpty(filter2Name))
		{
			filter2 = string.Format("Filter 2 \"{0}\" applied", filter2Name);
		}

		string filters = "";

		if (filter1 != "" && filter2 == "")
		{
			filters = string.Format(" ({0})", filter1);
		}
		else if (filter2 != "" && filter1 == "")
		{
			filters = string.Format(" ({0})", filter2);
		}
		if (filter1 != "" && filter2 != "")
		{
			filters = string.Format(" ({0}, {1})", filter1, filter2);
		}

		OutputHandler.WriteToLog(string.Format("Generating statistics using \"{0}\"{1}", statisticsName, filters));
	}
}
