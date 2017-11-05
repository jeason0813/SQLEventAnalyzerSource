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
using System.Data.SqlClient;
using System.Windows.Forms;

public partial class ConnectionDialogForm : Form
{
	public bool ConnectionChanged;
	public bool ShiftPressed;

	private DatabaseOperation _databaseOperation;
	private BackgroundWorker _worker;
	private bool _runWorkerActive;
	private bool _unattended;

	public ConnectionDialogForm()
	{
		InitializeComponent();
	}

	public void Initialize(DatabaseOperation databaseOperation, bool unattended, string connectionStringFromCmdLine)
	{
		InitializeDictionary();
		SetApplicationName();

		_unattended = unattended;
		_databaseOperation = databaseOperation;

		authenticationComboBox.SelectedIndex = 0;

		SetDefaultValues(connectionStringFromCmdLine);
		SearchHistoryHandler.LoadItems(serverNameComboBox, "RecentListServerName");

		if (ConfigHandler.SaveConnectionString == "True")
		{
			saveValuesCheckBox.Checked = true;
		}
		else
		{
			saveValuesCheckBox.Checked = false;
		}

		if (GenericHelper.IsUserInteractive())
		{
			InitializeWorker();
		}
	}

	public DatabaseOperation GetDatabaseOperation()
	{
		return _databaseOperation;
	}

	public void OkButtonClick()
	{
		if (ModifierKeys == Keys.Shift)
		{
			ShiftPressed = true;
		}

		if (saveValuesCheckBox.Checked)
		{
			ConfigHandler.SaveConnection();
		}

		if (saveValuesCheckBox.Checked)
		{
			ConfigHandler.SaveConnectionString = "True";

		}
		else
		{
			ConfigHandler.SaveConnectionString = "False";
		}

		if (ConfigHandler.RegistryModifyAccess)
		{
			RegistryHandler.SaveToRegistry("SaveConnectionString", ConfigHandler.SaveConnectionString);
		}

		if (!VerifyFields())
		{
			return;
		}

		BeginConnect();

		RunWorkerArgument arg = new RunWorkerArgument();
		arg.ConnectionString = GetConnectionString();

		_runWorkerActive = true;

		if (GenericHelper.IsUserInteractive())
		{
			_worker.RunWorkerAsync(arg);
		}
		else
		{
			DoWork(arg);
			RunWorkerCompleted();
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

		SetFocus();

		if (_unattended)
		{
			OkButtonClick();
		}
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			okButton.Text = Translator.GetText("okButton");
			cancelButton.Text = Translator.GetText("cancelButton");
			serverNameLabel.Text = Translator.GetText("serverNameLabel");
			authenticationLabel.Text = Translator.GetText("authenticationLabel");
			usernameLabel.Text = Translator.GetText("usernameLabel");
			passwordLabel.Text = Translator.GetText("passwordLabel");
			saveValuesCheckBox.Text = Translator.GetText("saveValuesCheckBox");
			connectToSqlServerGroupBox.Text = Translator.GetText("connectToSqlServerGroupBox");
			deleteLinkLabel.Text = Translator.GetText("DeleteThisValue");
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
		string connectionString = arg.ConnectionString;

		if (_databaseOperation == null) // new connection, no connection has been made
		{
			_databaseOperation = new DatabaseOperation();
			_databaseOperation.InitializeDal(connectionString);
		}
		else // a connection has already been made, but should be changed
		{
			ConnectionChanged = _databaseOperation.ChangeConnection(connectionString);
		}

		if (_databaseOperation.Connected)
		{
			ConnectionChanged = true;
		}
		else
		{
			ConnectionChanged = false;
		}

		if (ConnectionChanged)
		{
			ConfigHandler.ConnectionString = connectionString;
		}

		if (saveValuesCheckBox.Checked && ConnectionChanged)
		{
			ConfigHandler.ConnectionStringToSave = connectionString;
			ConfigHandler.SaveConnection();
		}
	}

	private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		RunWorkerCompleted();
	}

	private void RunWorkerCompleted()
	{
		_runWorkerActive = false;
		EndConnect();

		if (ConnectionChanged)
		{
			if (saveValuesCheckBox.Checked)
			{
				SearchHistoryHandler.AddItem(serverNameComboBox, serverNameComboBox.Text, "RecentListServerName");
			}

			Close();
		}
	}

	private void SetApplicationName()
	{
		Text = GenericHelper.ApplicationName;
	}

	private void SetDefaultValues(string connectionStringFromCmdLine)
	{
		SqlConnectionStringBuilder tempConnString;

		if (_databaseOperation == null) // new connection, no connection has been made
		{
			tempConnString = new SqlConnectionStringBuilder(ConfigHandler.ConnectionString);

			if (connectionStringFromCmdLine != null)
			{
				SqlConnectionStringBuilder connStringFromCmdLine = new SqlConnectionStringBuilder(connectionStringFromCmdLine);

				if (connStringFromCmdLine.IntegratedSecurity)
				{
					tempConnString.IntegratedSecurity = connStringFromCmdLine.IntegratedSecurity;
				}
				else
				{
					if (connStringFromCmdLine.UserID != "")
					{
						tempConnString.UserID = connStringFromCmdLine.UserID;
					}

					if (connStringFromCmdLine.Password != "")
					{
						tempConnString.Password = DecodeConnectionStringPassword(connStringFromCmdLine.Password);
					}
				}

				if (connStringFromCmdLine.DataSource != "")
				{
					tempConnString.DataSource = connStringFromCmdLine.DataSource;
				}
			}
		}
		else // a connection has already been made, but should be changed
		{
			if (ConfigHandler.SaveConnectionString != "True")
			{
				tempConnString = new SqlConnectionStringBuilder(ConfigHandler.ConnectionString);
			}
			else
			{
				tempConnString = new SqlConnectionStringBuilder(ConfigHandler.ConnectionStringToSave);
			}
		}

		serverNameComboBox.Text = tempConnString.DataSource;

		if (!tempConnString.IntegratedSecurity)
		{
			authenticationComboBox.SelectedIndex = 1;

			userNameTextBox.Text = tempConnString.UserID;
			passwordTextBox.Text = tempConnString.Password;
		}
	}

	private static string DecodeConnectionStringPassword(string encryptedPassword)
	{
		return ConnectionStringSecurity.Decode(encryptedPassword);
	}

	private void SetFocus()
	{
		serverNameComboBox.Focus();
	}

	private void CancelButton_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void AuthenticationComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		SetUserNameAndPasswordBoxes();
	}

	private void SetUserNameAndPasswordBoxes()
	{
		if (authenticationComboBox.SelectedIndex == 1)
		{
			userNameTextBox.Enabled = true;
			passwordTextBox.Enabled = true;
			deleteLinkLabel.Enabled = true;
			usernameLabel.Enabled = true;
			passwordLabel.Enabled = true;
			userNameTextBox.BackColor = System.Drawing.Color.White;
			passwordTextBox.BackColor = System.Drawing.Color.White;
		}
		else
		{
			userNameTextBox.Enabled = false;
			passwordTextBox.Enabled = false;
			deleteLinkLabel.Enabled = false;
			usernameLabel.Enabled = false;
			passwordLabel.Enabled = false;
			userNameTextBox.BackColor = System.Drawing.Color.Gainsboro;
			passwordTextBox.BackColor = System.Drawing.Color.Gainsboro;
		}
	}

	private void BeginConnect()
	{
		serverNameComboBox.Enabled = false;
		authenticationComboBox.Enabled = false;
		userNameTextBox.Enabled = false;
		passwordTextBox.Enabled = false;
		deleteLinkLabel.Enabled = false;
		cancelButton.Enabled = false;
		okButton.Enabled = false;
		saveValuesCheckBox.Enabled = false;

		Application.DoEvents();
	}

	private void EndConnect()
	{
		serverNameComboBox.Enabled = true;
		authenticationComboBox.Enabled = true;
		SetUserNameAndPasswordBoxes();
		cancelButton.Enabled = true;
		okButton.Enabled = true;
		saveValuesCheckBox.Enabled = true;
	}

	private bool VerifyFields()
	{
		if (serverNameComboBox.Text.Trim() == "")
		{
			string text = "Please enter server name.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("servernameMissing");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			serverNameComboBox.Focus();
			return false;
		}

		if (authenticationComboBox.SelectedIndex == 1)
		{
			if (userNameTextBox.Text.Trim() == "")
			{
				string text = "Please enter user name.";

				if (ConfigHandler.UseTranslation)
				{
					text = Translator.GetText("usernameMissing");
				}

				OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				userNameTextBox.Focus();
				return false;
			}
		}

		return true;
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		OkButtonClick();
	}

	private string GetConnectionString()
	{
		SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();

		if (authenticationComboBox.SelectedIndex == 1)
		{
			connectionString.IntegratedSecurity = false;
			connectionString.UserID = userNameTextBox.Text;
			connectionString.Password = passwordTextBox.Text;
		}
		else
		{
			connectionString.IntegratedSecurity = true;
		}

		connectionString.DataSource = serverNameComboBox.Text;

		string sessionId = Guid.NewGuid().ToString();
		GenericHelper.TempTableName = string.Format("TraceData_{0}", sessionId.Replace("'", "''"));
		connectionString.ApplicationName = string.Format("{0} - {1}", GenericHelper.ApplicationName, sessionId);

		return connectionString.ToString();
	}

	private class RunWorkerArgument
	{
		public string ConnectionString;
	}

	private void ConnectionDialogForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (_runWorkerActive)
		{
			e.Cancel = true;
		}
	}

	private void DeleteLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		string text = "Delete the saved password?";

		if (ConfigHandler.UseTranslation)
		{
			text = Translator.GetText("DeleteSavedValue");
		}

		DialogResult result = OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

		if (result == DialogResult.Yes)
		{
			if (ConfigHandler.RegistryModifyAccess)
			{
				RegistryHandler.Delete("Password_ConnectionString");
				passwordTextBox.Text = "";
			}
			else
			{
				if (ConfigHandler.UseTranslation)
				{
					MessageBox.Show(Translator.GetText("DeletePasswordNoAccess"));
				}
				else
				{
					MessageBox.Show("Logged in user does not have necessary rights to delete the saved password.\r\n\r\nUser needs modify access to HKLM in RegEdit.", GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}
	}
}
