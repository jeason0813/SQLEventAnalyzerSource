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
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

public class Dal
{
	public delegate void InfoMessageEventHandler(string infoMessage);
	public event InfoMessageEventHandler InfoMessageEvent;

	public bool Connected;
	private SqlConnectionStringBuilder _connString;
	private SqlConnection _conn;
	private SqlCommand _cmd;
	private bool _throwError;
	private bool _exitOnError;
	private bool _success = true;
	private bool _showErrorForm = true;
	private ErrorFormParams _errorFormParams;

	public void InitializeDal(string connectionString)
	{
		Connected = OpenConnection(connectionString, false);
	}

	public ErrorFormParams GetErrorFormParams()
	{
		return _errorFormParams;
	}

	public string GetConnectionString()
	{
		return _connString.ToString();
	}

	public int GetSqlServerVersion()
	{
		if (_conn.ServerVersion != null)
		{
			return Convert.ToInt32(_conn.ServerVersion.Substring(0, _conn.ServerVersion.IndexOf(".")));
		}

		return -1;
	}

	public void SetShowErrorForm(bool value)
	{
		_showErrorForm = value;
	}

	public string GetSqlServerName()
	{
		return _connString.DataSource;
	}

	public void ConnInfoMessage(object sender, SqlInfoMessageEventArgs e)
	{
		if (_throwError)
		{
			if (e.Errors[0].Number == 5701) // "Changed database context to '...'."
			{
				return;
			}
			else if (e.Errors[0].Number == 3621) // "The statement has been terminated."
			{
				return;
			}
			else if (e.Errors[0].Number == 5703) // "Changed language setting to '...'."
			{
				return;
			}
			else if (e.Errors[0].Number == 8153) // "Warning: Null value is eliminated by an aggregate or other SET operation."
			{
				return;
			}
			else if (e.Errors[0].Number == 4035) // "Processed ... pages for database ..." from Backup and Restore
			{
				return;
			}
			else if (e.Errors[0].Number == 3014) // "BACKUP DATABASE successfully processed" and "RESTORE DATABASE successfully processed"
			{
				return;
			}
			else if (e.Errors[0].Number == 5060) // "Nonqualified transactions are being rolled back. Estimated rollback completion: 100%."
			{
				return;
			}
			else if (e.Errors[0].Number == 2528) // "DBCC execution completed. If DBCC printed error messages, contact your system administrator."
			{
				return;
			}
			else if (e.Errors[0].Number == 7998) // "Checking identity information: current identity value '...', current column value '...'."
			{
				return;
			}
			else if (e.Errors[0].Number == 9927) // "The full-text search condition contained noise word(s)."
			{
				return;
			}
			else if (e.Errors[0].Number == 15457) // "Configuration option 'show advanced options' changed from 1 to 1. Run the RECONFIGURE statement to install."
			{
				return;
			}
			else if (e.Errors[0].Number == 8152) // "String or binary data would be truncated."
			{
				return;
			}
			else if (e.Errors[0].Number == 229) // "The SELECT permission was denied on the object 'sysjobsteps', database 'msdb', schema 'dbo'."
			{
				return;
			}
			else if (e.Errors[0].Number == 15477) // "Caution: Changing any part of an object name could break scripts and stored procedures."
			{
				return;
			}
			else if (e.Errors[0].Number == 7673) // "Warning: Full-text change tracking is currently disabled for table or indexed view '...'."
			{
				return;
			}
			else if (e.Errors[0].Number == 7640) // "Warning: Request to stop tracking changes on table or indexed view '...' will not stop population currently in progress on the table or indexed view."
			{
				return;
			}
			else if (e.Errors[0].Number == 7638) // "Warning: Request to stop change tracking has deleted all changes tracked on table or indexed view '...'."
			{
				return;
			}
			else if (e.Errors[0].Number == 7676) // "Warning: Full-text auto propagation is on. Stop crawl request is ignored."
			{
				return;
			}
			else if (e.Errors[0].Number == 7636) // "Warning: Request to start a full-text index population on table or indexed view '...' is ignored because a population is currently active for this table or indexed view."
			{
				return;
			}
			else if (e.Errors[0].Number != 0 && e.Message == "Incorrect syntax near 'go'.")
			{
				_success = false;
				OutputHandler.Show(string.Format("{0}\r\n\r\n'GO' is not a valid T-SQL statement.", e.Message), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			HandleError(e.Message, _cmd.CommandText, e.Errors[0].Number);
		}
		else
		{
			FireSendMessageEvent(e.Message);
		}
	}

	public string GetDatabaseName()
	{
		return _connString.InitialCatalog;
	}

	public string GetUserName()
	{
		return _connString.UserID;
	}

	public string GetPassword()
	{
		return _connString.Password;
	}

	public bool GetIntegratedSecurity()
	{
		return _connString.IntegratedSecurity;
	}

	public DataTable ExecuteDataTable(string sql)
	{
		_throwError = true;
		_exitOnError = false;

		DataSet ds = DoExecuteDataSet(sql, true);

		if (ds != null && ds.Tables.Count > 0)
		{
			return ds.Tables[0];
		}
		else
		{
			return null;
		}
	}

	public DataSet ExecuteDataSet(string sql)
	{
		_throwError = true;
		_exitOnError = false;

		return DoExecuteDataSet(sql, true);
	}

	public bool Execute(string sql, bool showErrorForm, bool exitOnError)
	{
		_showErrorForm = showErrorForm;
		_exitOnError = exitOnError;

		DoExecuteDataSet(sql, false);

		return _success;
	}

	public bool Execute(string sql, bool exitOnError)
	{
		_throwError = true;
		_exitOnError = exitOnError;

		DoExecuteDataSet(sql, false);

		return _success;
	}

	public bool Execute(string sql)
	{
		_throwError = true;
		_exitOnError = false;

		DoExecuteDataSet(sql, false);

		return _success;
	}

	public void Dispose()
	{
		if (_cmd != null)
		{
			_cmd.Cancel();
			_cmd.Dispose();
		}

		if (_conn != null)
		{
			_conn.Close();
			_conn.Dispose();
		}
	}

	public bool ChangeConnection(string connectionString)
	{
		Connected = OpenConnection(connectionString, false);
		return Connected;
	}

	private bool OpenConnection(string connectionString, bool exitOnError)
	{
		_exitOnError = exitOnError;
		SqlConnection conn = new SqlConnection(connectionString);

		try
		{
			conn.Open();

			bool isSqlServerVersionSupported = IsSqlServerVersionSupported(conn.ServerVersion);

			if (!isSqlServerVersionSupported)
			{
				conn.Close();

				string text = "SQL Server version not supported.";

				if (ConfigHandler.UseTranslation)
				{
					text = Translator.GetText("SqlServerNotSupported");
				}

				HandleError(text, "", -1);
			}
			else
			{
				if (_conn != null)
				{
					_conn.Close();
				}

				_conn = conn;
				_conn.InfoMessage += ConnInfoMessage;
				_conn.FireInfoMessageEventOnUserErrors = true;

				_connString = new SqlConnectionStringBuilder(connectionString);

				return true;
			}
		}
		catch (SqlException ex)
		{
			HandleError(ex.Message, string.Format("Connection string:\r\n\r\n{0}", conn.ConnectionString), -1);
		}
		catch (InvalidOperationException ex)
		{
			HandleError(ex.Message, "Please check the connection settings.", -1);
		}

		return false;
	}

	private static bool IsSqlServerVersionSupported(string serverVersion)
	{
		int version = Convert.ToInt32(serverVersion.Substring(0, serverVersion.IndexOf(".")));

		if (version < 9 || version > 13)
		{
			return false;
		}

		return true;
	}

	private void FireSendMessageEvent(string message)
	{
		if (InfoMessageEvent != null)
		{
			InfoMessageEvent(message);
		}
	}

	private DataSet DoExecuteDataSet(string sql, bool returnDataSet)
	{
		_success = true;

		DataSet dataSet = new DataSet();

		using (_cmd = new SqlCommand())
		{
			_cmd.CommandTimeout = 0;
			_cmd.CommandType = CommandType.Text;

			try
			{
				dataSet = ExecuteCommand(sql, returnDataSet);
			}
			catch (SqlException ex)
			{
				if (ex.Message == "The query processor could not start the necessary thread resources for parallel query execution.")
				{
					HandleError(string.Format("{0}\r\n\r\nTry lowering the number of connections.", ex.Message), "", -1);
				}
				else if (ex.Message.StartsWith("A transport-level error has occurred when sending the request to the server."))
				{
					dataSet = ExecuteCommand(sql, returnDataSet); // will be caught if connection is lost, but _conn.State has not noticed it yet. The execution will be retried once, so the connection can be opened again
				}
				else if (ex.Message == "Thread was being aborted.")
				{
				}
				else
				{
					HandleError(string.Format("Error in database communication.\r\n\r\n{0}", ex.Message), "", -1);
				}
			}
			catch (Exception ex)
			{
				if (ex.Message == "Thread was being aborted.")
				{
				}
				else
				{
					HandleError(string.Format("Error in database communication.\r\n\r\n{0}", ex.Message), "", -1);
				}
			}
		}

		return dataSet;
	}

	private DataSet ExecuteCommand(string sql, bool returnDataSet)
	{
		if (_conn == null)
		{
			return null;
		}

		DataSet dataSet = new DataSet();

		if (_conn.State == ConnectionState.Closed)
		{
			try
			{
				_conn.Open();
			}
			catch (Exception ex)
			{
				HandleError(ex.Message, "", -1);
				return null;
			}
		}

		_cmd.Connection = _conn;
		_cmd.CommandText = sql;

		if (returnDataSet)
		{
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter(_cmd))
			{
				dataAdapter.Fill(dataSet);
			}
		}
		else
		{
			_cmd.ExecuteNonQuery();
		}

		return dataSet;
	}

	private void HandleError(string message, string sql, int errorNumber)
	{
		_success = false;

		string okButtonText = "Close";

		if (ConfigHandler.UseTranslation)
		{
			okButtonText = Translator.GetText("Close");
		}

		if (_exitOnError)
		{
			okButtonText = "Exit";

			if (ConfigHandler.UseTranslation)
			{
				okButtonText = Translator.GetText("exit");
			}
		}

		_errorFormParams = new ErrorFormParams(okButtonText, message, sql);

		if (_showErrorForm && (ConfigHandler.ErrorNumbersToSkip == null || (ConfigHandler.ErrorNumbersToSkip != null && !ConfigHandler.ErrorNumbersToSkip.Contains(errorNumber))))
		{
			ErrorFormHandler errorFormHandler = new ErrorFormHandler();

			if (!ConfigHandler.ErrorFormShown)
			{
				ConfigHandler.ErrorFormShown = true;
				errorFormHandler.ErrorOccuredEvent(okButtonText, message, sql);
			}

			if (errorNumber != -1 && ConfigHandler.ErrorNumbersToSkip != null)
			{
				ConfigHandler.ErrorNumbersToSkip.Add(errorNumber);
			}
		}

		if (_exitOnError)
		{
			Dispose();
			Environment.Exit(-1);
		}
	}
}
