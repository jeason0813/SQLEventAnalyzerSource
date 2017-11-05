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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
public partial class CheckForUpdatesForm : Form
{
	public delegate void UpdateCheckCompleteEventHandler(string errorMessage, Version version, bool anyUpdates, bool anyErrors);
	public event UpdateCheckCompleteEventHandler UpdateCheckCompleteEvent;

	private bool _initialCheckForUpdatesOnStart;
	private const string _localFileName = "SQLEventAnalyzer.msi";
	private bool _downloading;
	private BackgroundWorker _changelogWorker;
	private bool _forceQuit;

	public CheckForUpdatesForm()
	{
		InitializeComponent();
		Initialize();
	}

	public void CheckForUpdates()
	{
		_changelogWorker.RunWorkerAsync();
	}

	public bool GetForceQuit()
	{
		return _forceQuit;
	}

	private void Initialize()
	{
		InitializeDictionary();

		FormBorderStyle = FormBorderStyle.FixedDialog;
		Size = new Size(439, 160);

		checkOnStartCheckBox.Checked = Convert.ToBoolean(ConfigHandler.CheckForUpdatesOnStart);
		_initialCheckForUpdatesOnStart = checkOnStartCheckBox.Checked;

		string currentVersionText = "Current version";

		if (ConfigHandler.UseTranslation)
		{
			currentVersionText = Translator.GetText("currentVersionText");
		}

		infoTextBox.Text = string.Format("{0}\r\n{2} {1}", GenericHelper.ApplicationName, GenericHelper.Version, currentVersionText);

		InitializeChangelogWorker();
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			changelogGroupBox.Text = Translator.GetText("Changelog");
			checkOnStartCheckBox.Text = Translator.GetText("checkOnStartCheckBox");
			updateButton.Text = Translator.GetText("updateButtonCheck");
			closeButton.Text = Translator.GetText("Close");

			Text = Translator.GetText("CheckForUpdatesTitle");
		}
	}

	private void InitializeChangelogWorker()
	{
		_changelogWorker = new BackgroundWorker();
		_changelogWorker.DoWork += ChangelogWorker_DoWork;
		_changelogWorker.RunWorkerCompleted += ChangelogWorker_RunWorkerCompleted;
	}

	private void FireUpdateCheckCompleteEvent(string errorMessage, Version version, bool anyUpdates, bool anyErrors)
	{
		if (UpdateCheckCompleteEvent != null)
		{
			UpdateCheckCompleteEvent(errorMessage, version, anyUpdates, anyErrors);
		}
	}

	private static void ChangelogWorker_DoWork(object sender, DoWorkEventArgs e)
	{
		ChangelogWorkerResult result = new ChangelogWorkerResult();

		try
		{
			Version currentVersion = new Version(GenericHelper.Version);
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

	private void ChangelogWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		ChangelogWorkerResult result = (ChangelogWorkerResult)e.Result;

		updateButton.Enabled = true;
		closeButton.Enabled = true;
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

			infoTextBox.Text = string.Format("{0}\r\n{3} {1}\r\n\r\n{2}", GenericHelper.ApplicationName, GenericHelper.Version, result.ErrorMessage, currentVersionText);
		}
		else
		{
			string newVersionInfo;

			if (result.AnyUpdates)
			{
				FormBorderStyle = FormBorderStyle.Sizable;
				Size = new Size(439, 300);
				MinimumSize = new Size(439, 300); // error in .NET

				changelogGroupBox.Visible = true;
				changelogTextBox.Visible = true;
				changelogTextBox.Text = result.Changelog;

				updateButton.Text = "Update";

				if (ConfigHandler.UseTranslation)
				{
					updateButton.Text = Translator.GetText("updateButtonUpdate");
				}

				string newVersionText = "New version {0} available.";

				if (ConfigHandler.UseTranslation)
				{
					newVersionText = Translator.GetText("newVersionText");
				}

				newVersionInfo = string.Format(newVersionText, result.Version);
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

			infoTextBox.Text = string.Format("{0}\r\n{3} {1}\r\n\r\n{2}", GenericHelper.ApplicationName, GenericHelper.Version, newVersionInfo, currentVersionText);
		}

		FireUpdateCheckCompleteEvent(result.ErrorMessage, result.Version, result.AnyUpdates, result.AnyErrors);
	}

	private class ChangelogWorkerResult
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
			closeButton.Enabled = false;
			_downloading = true;

			_changelogWorker.RunWorkerAsync();
		}
		else
		{
			updateButton.Text = "Downloading...";

			if (ConfigHandler.UseTranslation)
			{
				updateButton.Text = Translator.GetText("updateButtonDownloading");
			}

			updateButton.Enabled = false;
			closeButton.Enabled = false;
			_downloading = true;

			PerformDownload();
		}
	}

	private void CloseButton_Click(object sender, EventArgs e)
	{
		if (checkOnStartCheckBox.Checked != _initialCheckForUpdatesOnStart)
		{
			ConfigHandler.CheckForUpdatesOnStart = checkOnStartCheckBox.Checked.ToString();
			ConfigHandler.SaveConfig();
		}
	}

	private void PerformDownload()
	{
		WebClient webClient = new WebClient();
		webClient.DownloadFileCompleted += DownloadFileCompleted;

		string downloadUrl = GetDownloadUrl();

		if (downloadUrl != null)
		{
			webClient.DownloadFileAsync(new Uri(downloadUrl), string.Format(@"{0}\{1}", GenericHelper.TempPath, _localFileName));
		}
	}

	private static string GetDownloadUrl()
	{
		ServiceHandler service = new ServiceHandler(new Uri(ConfigHandler.UpdateServiceUrl));
		object[] args = { GenericHelper.ApplicationName };
		return service.InvokeMethod<string>("VirtcoreService", "GetDownloadUrl", args);
	}

	private static string GetChangelogUrl()
	{
		ServiceHandler service = new ServiceHandler(new Uri(ConfigHandler.UpdateServiceUrl));
		object[] args = { GenericHelper.ApplicationName };
		return service.InvokeMethod<string>("VirtcoreService", "GetChangelogUrl", args);
	}

	private static string GetLatestVersion()
	{
		ServiceHandler service = new ServiceHandler(new Uri(ConfigHandler.UpdateServiceUrl));
		object[] args = { GenericHelper.ApplicationName };
		return service.InvokeMethod<string>("VirtcoreService", "GetLatestVersion", args);
	}

	private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
	{
		Exception ex = e.Error;

		if (ex == null)
		{
			updateButton.Text = "Downloaded";

			if (ConfigHandler.UseTranslation)
			{
				updateButton.Text = Translator.GetText("updateButtonDownloaded");
			}

			Process.Start(string.Format(@"{0}\{1}", GenericHelper.TempPath, _localFileName));

			_downloading = false;
			_forceQuit = true;
			Close();
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

			updateButton.Enabled = true;
			closeButton.Enabled = true;
		}
	}

	private void CheckForUpdatesForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (_downloading)
		{
			e.Cancel = true;
		}
	}
}
