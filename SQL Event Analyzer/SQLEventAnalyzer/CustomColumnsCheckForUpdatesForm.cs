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
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

public partial class CustomColumnsCheckForUpdatesForm : Form
{
	public delegate void UpdateCheckCompleteEventHandler(string errorMessage, Version version, bool anyUpdates, bool anyErrors);
	public event UpdateCheckCompleteEventHandler UpdateCheckCompleteEvent;

	public bool DoPerformUpdate;

	private readonly string _localFileName;
	private Version _version;
	private string _updateServiceUrl;
	private bool _downloading;
	private BackgroundWorker _worker;
	private readonly string _sqlEventAnalyzerVersion;

	public CustomColumnsCheckForUpdatesForm(string fileName)
	{
		InitializeComponent();

		InitializeOptions();

		_localFileName = Path.GetFileName(fileName);

		Assembly asm = Assembly.GetExecutingAssembly();

		if (asm.GetName().Version.Revision > 0)
		{
			_sqlEventAnalyzerVersion = string.Format("{0}.{1}.{2}.{3}", asm.GetName().Version.Major, asm.GetName().Version.Minor, asm.GetName().Version.Build, asm.GetName().Version.Revision);
		}
		else
		{
			_sqlEventAnalyzerVersion = string.Format("{0}.{1}.{2}", asm.GetName().Version.Major, asm.GetName().Version.Minor, asm.GetName().Version.Build);
		}

		Initialize();
	}

	public void CheckForUpdates()
	{
		_worker.RunWorkerAsync();
	}

	private void Initialize()
	{
		InitializeDictionary();

		FormBorderStyle = FormBorderStyle.FixedDialog;
		Size = new Size(439, 160);

		string currentVersionText = "Current version";

		if (ConfigHandler.UseTranslation)
		{
			currentVersionText = Translator.GetText("currentVersionText");
		}

		infoTextBox.Text = string.Format("{0}\r\n{2} {1}", _localFileName, GetVersion(), currentVersionText);

		InitializeWorker();
	}

	private void InitializeOptions()
	{
		_version = ColumnHelper.GetVersion();
		_updateServiceUrl = ColumnHelper.GetUpdateServer();
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			changelogGroupBox.Text = Translator.GetText("Changelog");
			updateButton.Text = Translator.GetText("updateButtonCheck");
			closebutton.Text = Translator.GetText("Close");

			Text = Translator.GetText("CheckForUpdatesTitle");
		}
	}

	private void InitializeWorker()
	{
		if (!GenericHelper.IsUserInteractive())
		{
			Application.DoEvents(); // due to _worker_RunWorkerCompleted not being called in non-interactive mode since there are no GUI thread.
		}

		_worker = new BackgroundWorker();
		_worker.DoWork += Worker_DoWork;
		_worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
	}

	private void FireUpdateCheckCompleteEvent(string errorMessage, Version version, bool anyUpdates, bool anyErrors)
	{
		if (UpdateCheckCompleteEvent != null)
		{
			UpdateCheckCompleteEvent(errorMessage, version, anyUpdates, anyErrors);
		}
	}

	private void Worker_DoWork(object sender, DoWorkEventArgs e)
	{
		WorkerResult result = new WorkerResult();

		try
		{
			Version currentVersion = new Version(GetVersion());
			Version serverVersion = new Version(GetLatestVersion());

			if (serverVersion > currentVersion)
			{
				string changelogUrl = GetChangelogUrl();

				WebClient webClient = new WebClient();
				webClient.DownloadFile(new Uri(changelogUrl), string.Format(@"{0}\Changelog.txt", GenericHelper.TempPath));

				result.Changelog = ParseChangelog(currentVersion, serverVersion);
				result.Version = serverVersion;
				result.AnyUpdates = true;
			}
		}
		catch
		{
			result.ErrorMessage = "Could not contact update server.";

			if (ConfigHandler.UseTranslation)
			{
				result.ErrorMessage = Translator.GetText("NoUpdateServer");
			}

			result.AnyErrors = true;
		}

		e.Result = result;
	}

	private static string ParseChangelog(Version currentVersion, Version serverVersion)
	{
		string[] changelogLines = File.ReadAllLines(string.Format(@"{0}\Changelog.txt", GenericHelper.TempPath));

		StringBuilder sb = new StringBuilder();

		bool allignedWithServerVersionWithRevision = false;
		bool allignedWithServerVersionWithoutRevision = false;

		foreach (string changelogLine in changelogLines)
		{
			if (changelogLine == string.Format("{0}.{1}.{2}", serverVersion.Major, serverVersion.Minor, serverVersion.Build))
			{
				allignedWithServerVersionWithoutRevision = true;
				allignedWithServerVersionWithRevision = false;
			}

			if (changelogLine == string.Format("{0}.{1}.{2}.{3}", serverVersion.Major, serverVersion.Minor, serverVersion.Build, serverVersion.Revision))
			{
				allignedWithServerVersionWithRevision = true;
				allignedWithServerVersionWithoutRevision = false;
			}

			if (allignedWithServerVersionWithoutRevision)
			{
				if (changelogLine != string.Format("{0}.{1}.{2}", currentVersion.Major, currentVersion.Minor, currentVersion.Build))
				{
					sb.AppendLine(changelogLine);
				}
				else
				{
					break;
				}
			}

			if (allignedWithServerVersionWithRevision)
			{
				if (changelogLine != string.Format("{0}.{1}.{2}.{3}", currentVersion.Major, currentVersion.Minor, currentVersion.Build, currentVersion.Revision))
				{
					sb.AppendLine(changelogLine);
				}
				else
				{
					break;
				}
			}
		}

		return sb.ToString().Substring(0, sb.Length - 2);
	}

	private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		WorkerResult result = (WorkerResult)e.Result;

		updateButton.Enabled = true;
		closebutton.Enabled = true;
		_downloading = false;

		if (result.AnyErrors)
		{
			updateButton.Text = "Check";

			if (ConfigHandler.UseTranslation)
			{
				updateButton.Text = Translator.GetText("updateButtonCheck");
			}

			string currentVersionText = "Current version";

			if (ConfigHandler.UseTranslation)
			{
				currentVersionText = Translator.GetText("currentVersionText");
			}

			infoTextBox.Text = string.Format("{0}\r\n{3} {1}\r\n\r\n{2}", _localFileName, GetVersion(), result.ErrorMessage, currentVersionText);
		}
		else
		{
			string newVersionInfo;

			if (result.AnyUpdates)
			{
				updateButton.Text = "Downloading...";

				if (ConfigHandler.UseTranslation)
				{
					updateButton.Text = Translator.GetText("updateButtonDownloading");
				}

				updateButton.Enabled = false;
				closebutton.Enabled = false;
				_downloading = true;

				PerformUpdate(result);

				string newVersionText = "New version {0} available.";

				if (ConfigHandler.UseTranslation)
				{
					newVersionText = Translator.GetText("newVersionText");
				}

				newVersionInfo = string.Format(newVersionText, result.Version);

				changelogTextBox.Text = result.Changelog;
			}
			else
			{
				updateButton.Text = "Check";

				if (ConfigHandler.UseTranslation)
				{
					updateButton.Text = Translator.GetText("updateButtonCheck");
				}

				newVersionInfo = "Latest version already installed.";

				if (ConfigHandler.UseTranslation)
				{
					newVersionInfo = Translator.GetText("LatestVersionInstalled");
				}
			}

			string currentVersionText = "Current version";

			if (ConfigHandler.UseTranslation)
			{
				currentVersionText = Translator.GetText("currentVersionText");
			}

			infoTextBox.Text = string.Format("{0}\r\n{3} {1}\r\n\r\n{2}", _localFileName, GetVersion(), newVersionInfo, currentVersionText);
		}
	}

	private class WorkerResult
	{
		public string ErrorMessage;
		public Version Version;
		public bool AnyUpdates;
		public bool AnyErrors;
		public string Changelog;
	}

	private void UpdateButton_Click(object sender, EventArgs e)
	{
		string checkText = "Check";

		if (ConfigHandler.UseTranslation)
		{
			checkText = Translator.GetText("updateButtonCheck");
		}

		if (updateButton.Text == checkText)
		{
			updateButton.Text = "Checking...";

			if (ConfigHandler.UseTranslation)
			{
				updateButton.Text = Translator.GetText("updateButtonChecking");
			}

			updateButton.Enabled = false;
			closebutton.Enabled = false;
			_downloading = true;

			_worker.RunWorkerAsync();
		}
		else
		{
			DoPerformUpdate = true;
			Close();
		}
	}

	private void PerformUpdate(WorkerResult workerResult)
	{
		WebClient webClient = new WebClient();
		webClient.DownloadFileCompleted += Completed;

		string downloadUrl = GetDownloadUrl();

		if (downloadUrl != null)
		{
			webClient.DownloadFileAsync(new Uri(downloadUrl), string.Format(@"{0}\{1}", GenericHelper.TempPath, _localFileName), workerResult);
		}
	}

	private string GetDownloadUrl()
	{
		string machineName = Environment.MachineName;
		string userName = Environment.UserName;
		string domainName = Environment.UserDomainName;
		string allowedUpdateCriteria = string.Format("SQLEventAnalyzerVersion={0}", _sqlEventAnalyzerVersion);

		ServiceHandler service = new ServiceHandler(new Uri(_updateServiceUrl));
		object[] args = { _localFileName, machineName, userName, domainName, allowedUpdateCriteria };
		return service.InvokeMethod<string>("VirtcoreService", "GetDownloadUrl", args);
	}

	private string GetChangelogUrl()
	{
		string allowedUpdateCriteria = string.Format("SQLEventAnalyzerVersion={0}", _sqlEventAnalyzerVersion);

		ServiceHandler service = new ServiceHandler(new Uri(_updateServiceUrl));
		object[] args = { _localFileName, allowedUpdateCriteria };
		return service.InvokeMethod<string>("VirtcoreService", "GetChangelogUrl", args);
	}

	private string GetLatestVersion()
	{
		string machineName = Environment.MachineName;
		string userName = Environment.UserName;
		string domainName = Environment.UserDomainName;
		string allowedUpdateCriteria = string.Format("SQLEventAnalyzerVersion={0}", _sqlEventAnalyzerVersion);

		ServiceHandler service = new ServiceHandler(new Uri(_updateServiceUrl));
		object[] args = { _localFileName, GetVersion(), machineName, userName, domainName, allowedUpdateCriteria };
		return service.InvokeMethod<string>("VirtcoreService", "GetLatestVersion", args);
	}

	private void Completed(object sender, AsyncCompletedEventArgs e)
	{
		WorkerResult result = (WorkerResult)e.UserState;
		Exception ex = e.Error;

		if (ex == null)
		{
			_downloading = false;

			FormBorderStyle = FormBorderStyle.Sizable;
			Size = new Size(439, 300);
			MinimumSize = new Size(439, 300); // error in .NET

			changelogGroupBox.Visible = true;
			changelogTextBox.Visible = true;

			updateButton.Text = "Update";

			if (ConfigHandler.UseTranslation)
			{
				updateButton.Text = Translator.GetText("updateButtonUpdate");
			}
		}
		else
		{
			string errorDownloading = "Error downloading update.";

			if (ConfigHandler.UseTranslation)
			{
				errorDownloading = Translator.GetText("errorDownloading");
			}

			MessageBox.Show(string.Format("{1}\r\n\r\n{0}", ex.Message, errorDownloading), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			_downloading = false;

			updateButton.Text = "Update";

			if (ConfigHandler.UseTranslation)
			{
				updateButton.Text = Translator.GetText("updateButtonUpdate");
			}
		}

		updateButton.Enabled = true;
		closebutton.Enabled = true;

		FireUpdateCheckCompleteEvent(result.ErrorMessage, result.Version, result.AnyUpdates, result.AnyErrors);
	}

	private void CheckForUpdatesForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (_downloading)
		{
			e.Cancel = true;
		}
	}

	private string GetVersion()
	{
		string version;

		if (_version.Revision > 0)
		{
			version = string.Format("{0}.{1}.{2}.{3}", _version.Major, _version.Minor, _version.Build, _version.Revision);
		}
		else
		{
			version = string.Format("{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build);
		}

		return version;
	}
}
