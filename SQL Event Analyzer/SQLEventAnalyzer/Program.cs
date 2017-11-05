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
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

public static class Program
{
	private static string _sendLogToWebService;
	private static string _message;
	private static string _messageSql;
	private static string _connectionStringFromCmdLine;

	[STAThread]
	public static int Main(string[] args)
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);

		CheckPermissions();

		int returnCode = 0;

		if (args.Length == 0)
		{
			Application.Run(new MainForm());
		}
		else
		{
			ConfigHandler.UnattendedRunStartTime = DateTime.Now;

			OutputHandler.InitializeLog();

			string versionInfo = string.Format("{0} {1}", GenericHelper.ApplicationName, GenericHelper.Version);

			OutputHandler.WriteToLog(string.Format("Unattended service context run started at {0}", ConfigHandler.UnattendedRunStartTime));
			OutputHandler.WriteToLog(string.Format("{0}", versionInfo));
			OutputHandler.WriteToLog(string.Format("Executed on: {0}", Environment.MachineName));
			OutputHandler.WriteToLog(string.Format("Executed by: {0}", Environment.UserName));

			returnCode = HandleCommandLineArgs(args);

			if (_message != null)
			{
				OutputHandler.WriteToLog(string.Format("Custom Message:\r\n[BEGIN Custom Message]\r\n{0}\r\n[END Custom Message]", _message));
			}

			if (_messageSql != null)
			{
				DatabaseMessageObject databaseMessageObject = GetDatabaseMessage(_connectionStringFromCmdLine, _messageSql);
				OutputHandler.WriteToLog(string.Format("Custom SQL Message:\r\n[BEGIN Custom SQL Message]\r\n{0}\r\n[END Custom SQL Message]", databaseMessageObject.Message));

				if (!databaseMessageObject.Success)
				{
					returnCode = -10;
				}
			}

			DateTime endTime = DateTime.Now;

			OutputHandler.WriteToLog(string.Format("Run ended at {0}", endTime));
			OutputHandler.WriteToLog(string.Format("Execution time: {0}", GenericHelper.FormatTimeSpan(endTime - ConfigHandler.UnattendedRunStartTime)));
			OutputHandler.WriteToLog(string.Format("Exit code: {0}", returnCode));

			if (_sendLogToWebService != null)
			{
				bool success = SendLogtoWebService(returnCode);

				if (!success)
				{
					returnCode = -8;
				}
			}
		}

		Environment.ExitCode = returnCode;
		return returnCode;
	}

	private static bool SendLogtoWebService(int returnCode)
	{
		bool success = false;

		string machineName = Environment.MachineName;
		string userName = Environment.UserName;
		string domainName = Environment.UserDomainName;

		try
		{
			ServiceHandler service = new ServiceHandler(new Uri(_sendLogToWebService));

			if (ConfigHandler.StatisticsArchive == null)
			{
				object[] args = { "SQL Event Analyzer", GenericHelper.Version, machineName, userName, domainName, returnCode, OutputHandler.GetLog() };
				success = service.InvokeMethod<bool>("LogService", "RegisterLogEvent", args);
			}
			else
			{
				byte[] attachment = GetDocument(ConfigHandler.StatisticsArchive);
				object[] args = { "SQL Event Analyzer", GenericHelper.Version, machineName, userName, domainName, returnCode, OutputHandler.GetLog(), attachment };
				success = service.InvokeMethod<bool>("LogService", "RegisterLogEventWithAttachment", args);

				GenericHelper.DeleteFile(ConfigHandler.StatisticsArchive);
			}
		}
		catch
		{
		}

		return success;
	}

	private static byte[] GetDocument(string documentPathAndName)
	{
		FileStream filestream = new FileStream(documentPathAndName, FileMode.Open, FileAccess.Read);
		int len = (int)filestream.Length;
		byte[] documentContent = new byte[len];
		filestream.Read(documentContent, 0, len);
		filestream.Close();

		return documentContent;
	}

	private static int HandleCommandLineArgs(string[] args)
	{
		bool unattended = false;
		int numberOfImportFiles = -1;
		string lastImportFile = null;
		string importNewerThanSessionId = null;
		string sessionId = null;
		string postScriptFileName = null;
		string postScript = null;
		bool deleteTraceFilesAfterImport = false;
		string importPath = null;
		string columnsXmlFileName = null;
		bool recordMode = false;
		string useSessionId = null;
		bool verboseMode = false;
		string applicationName = null;
		string sqlEventAnalyzerDatabaseName = null;
		bool forceDelete = false;
		bool saveOutputToLogFile = false;
		int compressTraceFilesAfterImport = -1;
		List<string> generateStatisticsNames = new List<string>();
		List<string> generateStatisticsFilter1Names = new List<string>();
		List<string> generateStatisticsFilter2Names = new List<string>();
		string generateStatisticsSavePath = null;
		List<string> generateStatisticsSaveWebService = new List<string>();

		int correctArgsCount = 0;
		int optionalGlobalArgs = 0; // -c -t -o -w -b -m
		int optionalConnectionStringArgs = 0; // -ms

		try
		{
			if (args.Length >= 1 && args.Length <= 20) // -n -f -i -p -e -s -d -z -l -c -r -u -v -t -o -x -w -a -b -g -f1 -f2 -sp -sw -m -ms
			{
				if (GenericHelper.UniqueElements(args))
				{
					int mandatoryInputSourceArgsCount = 0; // -n -f
					int importNewerThanArgsCount = 0; // -i
					int optionalSection1ArgsCount = 0; // -p -e -d -z
					int optionalInputSourceArgsCount = 0; // -s
					int mandatoryImportPathArgsCount = 0; // -l
					int mandatoryRecordingModeArgsCount = 0; // -r
					int mandatoryModeArgsCount = 0; // -u
					int optionalModeArgsCount = 0; // -v
					int optionalForceDeleteArgsCount = 0; // -x
					int optionalVerboseArgsCount = 0; // -a
					int optionalGenerateStatisticsArgsCount = 0; // -g
					int optionalGenerateStatisticsSavePathArgsCount = 0; // -sp
					int optionalGenerateStatisticsSaveWebServiceArgsCount = 0; // -sw
					int optionalGenerateStatisticsFilterArgsCount = 0; // -f1 -f2
					int optionalConnectionStringFromCmdLine = 0; // -t

					foreach (string arg in args)
					{
						if (arg.Length >= 3)
						{
							if (arg.ToLower().Substring(0, 3) == "-u:")
							{
								useSessionId = arg.Substring(3, arg.Length - 3);

								if (useSessionId.Length > 0)
								{
									correctArgsCount++;
									mandatoryModeArgsCount++;
								}
							}

							if (arg.ToLower().Substring(0, 3) == "-n:")
							{
								numberOfImportFiles = Convert.ToInt32(arg.Substring(3, arg.Length - 3));

								if (numberOfImportFiles >= 0)
								{
									correctArgsCount++;
									mandatoryInputSourceArgsCount++;
								}
							}

							if (arg.ToLower().Substring(0, 3) == "-f:")
							{
								lastImportFile = arg.Substring(3, arg.Length - 3);

								if (lastImportFile.Length > 0)
								{
									correctArgsCount++;
									mandatoryInputSourceArgsCount++;
								}
							}

							if (arg.ToLower().Substring(0, 3) == "-i:")
							{
								importNewerThanSessionId = arg.Substring(3, arg.Length - 3);
								sessionId = importNewerThanSessionId;

								if (importNewerThanSessionId.Length > 0)
								{
									correctArgsCount++;
									importNewerThanArgsCount++;
								}
							}

							if (arg.ToLower().Substring(0, 3) == "-s:")
							{
								sessionId = arg.Substring(3, arg.Length - 3);

								if (sessionId.Length > 0)
								{
									correctArgsCount++;
									optionalInputSourceArgsCount++;
								}
							}

							if (arg.ToLower().Substring(0, 3) == "-p:")
							{
								postScriptFileName = arg.Substring(3, arg.Length - 3);

								if (postScriptFileName.Length > 0)
								{
									correctArgsCount++;
									optionalSection1ArgsCount++;
								}
							}

							if (arg.ToLower().Substring(0, 3) == "-e:")
							{
								postScript = arg.Substring(3, arg.Length - 3);

								correctArgsCount++;
								optionalSection1ArgsCount++;
							}

							if (arg.ToLower().Substring(0, 3) == "-l:")
							{
								importPath = arg.Substring(3, arg.Length - 3);

								if (importPath.Length > 0)
								{
									if (importPath.EndsWith("\""))
									{
										importPath = importPath.Substring(0, importPath.Length - 1);
									}

									correctArgsCount++;
									mandatoryImportPathArgsCount++;
								}
							}

							if (arg.ToLower().Substring(0, 3) == "-c:")
							{
								columnsXmlFileName = arg.Substring(3, arg.Length - 3);

								if (columnsXmlFileName.Length >= 0)
								{
									correctArgsCount++;
									optionalGlobalArgs++;
								}
							}

							if (arg.ToLower().Substring(0, 3) == "-t:")
							{
								string connectionStringFromCmdLine = arg.Substring(3, arg.Length - 3);

								if (connectionStringFromCmdLine.Length >= 0)
								{
									_connectionStringFromCmdLine = connectionStringFromCmdLine;
									optionalConnectionStringFromCmdLine++;
									correctArgsCount++;
									optionalGlobalArgs++;
								}
							}

							if (arg.ToLower().Substring(0, 3) == "-w:")
							{
								_sendLogToWebService = arg.Substring(3, arg.Length - 3);

								if (_sendLogToWebService.Length >= 0)
								{
									correctArgsCount++;
									optionalGlobalArgs++;
								}
							}

							if (arg.ToLower().Substring(0, 3) == "-b:")
							{
								sqlEventAnalyzerDatabaseName = arg.Substring(3, arg.Length - 3);

								if (sqlEventAnalyzerDatabaseName.Length >= 0)
								{
									correctArgsCount++;
									optionalGlobalArgs++;
								}
							}

							if (arg.ToLower().Substring(0, 3) == "-m:")
							{
								string message = arg.Substring(3, arg.Length - 3);

								if (message.Length >= 0)
								{
									_message = message;
									correctArgsCount++;
									optionalGlobalArgs++;
								}
							}

							if (arg.ToLower().Substring(0, 4) == "-ms:")
							{
								string messageSql = arg.Substring(4, arg.Length - 4);

								if (messageSql.Length >= 0)
								{
									_messageSql = messageSql;
									correctArgsCount++;
									optionalConnectionStringArgs++;
								}
							}

							if (arg.ToLower().Substring(0, 3) == "-a:")
							{
								applicationName = arg.Substring(3, arg.Length - 3);

								if (applicationName.Length >= 0)
								{
									correctArgsCount++;
									optionalVerboseArgsCount++;
								}
							}

							if (arg.ToLower().Substring(0, 3) == "-z:")
							{
								compressTraceFilesAfterImport = Convert.ToInt32(arg.Substring(3, arg.Length - 3));

								if (compressTraceFilesAfterImport >= 0)
								{
									correctArgsCount++;
									optionalSection1ArgsCount++;
								}
							}

							if (arg.ToLower().Substring(0, 3) == "-g:")
							{
								string[] inputGenerateStatisticsNames = arg.Substring(3, arg.Length - 3).Split(',');

								if (inputGenerateStatisticsNames.Length >= 0)
								{
									foreach (string inputGenerateStatisticsName in inputGenerateStatisticsNames)
									{
										generateStatisticsNames.Add(inputGenerateStatisticsName.Trim());
									}

									correctArgsCount++;
									optionalGenerateStatisticsArgsCount++;
								}
							}

							if (arg.ToLower().Substring(0, 4) == "-f1:")
							{
								string[] inputGenerateStatisticsFilter1Names = arg.Substring(4, arg.Length - 4).Split(',');

								if (inputGenerateStatisticsFilter1Names.Length >= 0)
								{
									foreach (string inputGenerateStatisticsFilter1Name in inputGenerateStatisticsFilter1Names)
									{
										generateStatisticsFilter1Names.Add(inputGenerateStatisticsFilter1Name.Trim());
									}

									correctArgsCount++;
									optionalGenerateStatisticsFilterArgsCount++;
								}
							}

							if (arg.ToLower().Substring(0, 4) == "-f2:")
							{
								string[] inputGenerateStatisticsFilter2Names = arg.Substring(4, arg.Length - 4).Split(',');

								if (inputGenerateStatisticsFilter2Names.Length >= 0)
								{
									foreach (string inputGenerateStatisticsFilter2Name in inputGenerateStatisticsFilter2Names)
									{
										generateStatisticsFilter2Names.Add(inputGenerateStatisticsFilter2Name.Trim());
									}

									correctArgsCount++;
									optionalGenerateStatisticsFilterArgsCount++;
								}
							}

							if (arg.ToLower().Substring(0, 4) == "-sp:")
							{
								generateStatisticsSavePath = arg.Substring(4, arg.Length - 4);

								if (generateStatisticsSavePath.Length >= 0)
								{
									if (generateStatisticsSavePath.EndsWith("\""))
									{
										generateStatisticsSavePath = generateStatisticsSavePath.Substring(0, generateStatisticsSavePath.Length - 1);
									}

									correctArgsCount++;
									optionalGenerateStatisticsSavePathArgsCount++;
								}
							}

							if (arg.ToLower().Substring(0, 4) == "-sw:")
							{
								string[] inputGenerateStatisticsSaveWebServiceNames = arg.Substring(4, arg.Length - 4).Split(',');

								if (inputGenerateStatisticsSaveWebServiceNames.Length >= 0)
								{
									foreach (string inputGenerateStatisticsSaveWebServiceName in inputGenerateStatisticsSaveWebServiceNames)
									{
										generateStatisticsSaveWebService.Add(inputGenerateStatisticsSaveWebServiceName.Trim());
									}

									correctArgsCount++;
									optionalGenerateStatisticsSaveWebServiceArgsCount++;
								}
							}
						}
						else if (arg.Length == 2)
						{
							if (arg.ToLower() == "-d")
							{
								deleteTraceFilesAfterImport = true;

								correctArgsCount++;
								optionalSection1ArgsCount++;
							}

							if (arg.ToLower() == "-r")
							{
								recordMode = true;

								correctArgsCount++;
								mandatoryRecordingModeArgsCount++;
							}

							if (arg.ToLower() == "-v")
							{
								verboseMode = true;

								correctArgsCount++;
								optionalModeArgsCount++;
							}

							if (arg.ToLower() == "-x")
							{
								forceDelete = true;

								correctArgsCount++;
								optionalForceDeleteArgsCount++;
							}

							if (arg.ToLower() == "-o")
							{
								correctArgsCount++;
								optionalGlobalArgs++;

								if (!GenericHelper.IsUserInteractive())
								{
									continue;
								}

								saveOutputToLogFile = true;
							}
						}
					}

					if
					(
						correctArgsCount == args.Length
						&&
						(
							(
								(
									(
										(
											mandatoryInputSourceArgsCount == 1 // -n -f
											&&
											(
												optionalInputSourceArgsCount == 0
												||
												(
													optionalInputSourceArgsCount == 1 // -s
													&&
													(
														optionalForceDeleteArgsCount == 0 || optionalForceDeleteArgsCount == 1 // -x
													)
												)
											)
										)
										||
										(
											importNewerThanArgsCount == 1 // -i
											&&
											(
												optionalForceDeleteArgsCount == 0 || optionalForceDeleteArgsCount == 1 // -x
											)
										)
									)
									&&
									mandatoryImportPathArgsCount == 1 // -l
								)
								&&
								(
									optionalSection1ArgsCount >= 0 && optionalSection1ArgsCount <= 4 // -p -e -d -z
								)
								&&
								(
									optionalGenerateStatisticsArgsCount == 0
									||
									(
										optionalGenerateStatisticsArgsCount == 1 // -g
										&&
										(
											optionalGenerateStatisticsFilterArgsCount >= 0 && optionalGenerateStatisticsFilterArgsCount <= 2 // -f1 -f2
											&&
											(
												generateStatisticsFilter1Names.Count <= generateStatisticsNames.Count && generateStatisticsFilter2Names.Count <= generateStatisticsNames.Count && generateStatisticsSaveWebService.Count <= generateStatisticsNames.Count
											)
											&&
											(
												optionalGenerateStatisticsSavePathArgsCount == 0 && optionalGenerateStatisticsSaveWebServiceArgsCount == 1
											)
											|| optionalGenerateStatisticsSavePathArgsCount == 1 // -sp
											&&
											(
												optionalGenerateStatisticsSaveWebServiceArgsCount == 0 && optionalGenerateStatisticsSavePathArgsCount == 1
											)
											||
											(
												optionalGenerateStatisticsSaveWebServiceArgsCount == 1 && _sendLogToWebService != null // -sw
											)
										)
									)
								)
							)
							||
							(
								mandatoryModeArgsCount == 1 // -u
								&&
								(
									optionalModeArgsCount == 0
									||
									(
										optionalModeArgsCount == 1 // -v
										&&
										(
											optionalVerboseArgsCount == 0 || optionalVerboseArgsCount == 1 // -a
										)
									)
								)
								&&
								(
									optionalForceDeleteArgsCount == 0 || optionalForceDeleteArgsCount == 1 // -x
								)
							)
							||
							(
								mandatoryRecordingModeArgsCount == 1 // -r
							)
						)
						&&
						(
							(
								optionalConnectionStringFromCmdLine == 0 && optionalConnectionStringArgs == 0
							)
							||
							(
								optionalConnectionStringFromCmdLine == 1 // -t
								&&
								(
									optionalConnectionStringArgs == 0 || optionalConnectionStringArgs == 1 // -ms
								)
							)
						)
					)
					{
						unattended = true;
					}
				}
			}
		}
		catch
		{
			WriteCommandSyntax();
			return 1;
		}

		GetSqlEventAnalyzerServerInfoText(_connectionStringFromCmdLine, sqlEventAnalyzerDatabaseName);

		if (unattended)
		{
			if (recordMode)
			{
				return HandleRecordMode(columnsXmlFileName, _connectionStringFromCmdLine, saveOutputToLogFile, sqlEventAnalyzerDatabaseName);
			}
			else if (useSessionId != null)
			{
				return HandleUseSessionMode(useSessionId, columnsXmlFileName, verboseMode, _connectionStringFromCmdLine, saveOutputToLogFile, forceDelete, applicationName, sqlEventAnalyzerDatabaseName);
			}
			else
			{
				return HandleUnattended(numberOfImportFiles, lastImportFile, importNewerThanSessionId, sessionId, postScriptFileName, postScript, compressTraceFilesAfterImport, deleteTraceFilesAfterImport, importPath, columnsXmlFileName, _connectionStringFromCmdLine, saveOutputToLogFile, forceDelete, sqlEventAnalyzerDatabaseName, generateStatisticsNames, generateStatisticsFilter1Names, generateStatisticsFilter2Names, generateStatisticsSavePath, generateStatisticsSaveWebService);
			}
		}
		else if (correctArgsCount == args.Length && optionalGlobalArgs >= 1 && optionalGlobalArgs <= 6) // -c -t -o -w -b -m
		{
			if (saveOutputToLogFile)
			{
				StartInteractiveRunLog();
			}

			MainForm form = new MainForm(columnsXmlFileName, _connectionStringFromCmdLine, sqlEventAnalyzerDatabaseName);

			int returnCode = form.GetUnattendedExitCode();

			if (returnCode == 0)
			{
				Application.Run(form);
				return form.GetUnattendedExitCode();
			}
			else
			{
				return returnCode;
			}
		}
		else
		{
			WriteCommandSyntax();
			return 1;
		}
	}

	private static void GetSqlEventAnalyzerServerInfoText(string connectionStringFromCmdLine, string sqlEventAnalyzerDatabaseName)
	{
		string databaseName = sqlEventAnalyzerDatabaseName;

		if (sqlEventAnalyzerDatabaseName == null)
		{
			databaseName = RegistryHandler.ReadFromRegistry("DatabaseName");
		}

		OutputHandler.WriteToLog(string.Format("SQL Event Analyzer database name: {0}", databaseName));

		string connectionString = connectionStringFromCmdLine;

		if (connectionStringFromCmdLine == null)
		{
			connectionString = RegistryHandler.ReadFromRegistry("ConnectionString");
		}

		string sqlEventAnalyzerDatabaseSqlServerName = GenericHelper.GetServerName(new SqlConnectionStringBuilder(connectionString).DataSource);

		OutputHandler.WriteToLog(string.Format("Server containing the SQL Event Analyzer database: {0}", sqlEventAnalyzerDatabaseSqlServerName));
	}

	private static int HandleRecordMode(string columnsXmlFileName, string connectionStringFromCmdLine, bool saveOutputToLogFile, string sqlEventAnalyzerDatabaseName)
	{
		OutputHandler.WriteToLog("\"-r\" not available when running in a service context.");

		if (!GenericHelper.IsUserInteractive())
		{
			return 1;
		}

		if (saveOutputToLogFile)
		{
			StartInteractiveRunLog();
		}

		MainForm form = new MainForm(true, columnsXmlFileName, connectionStringFromCmdLine, sqlEventAnalyzerDatabaseName);

		int returnCode = form.GetUnattendedExitCode();

		if (returnCode == 0)
		{
			Application.Run(form);
			return form.GetUnattendedExitCode();
		}
		else
		{
			return returnCode;
		}
	}

	private static int HandleUseSessionMode(string useSessionId, string columnsXmlFileName, bool verboseMode, string connectionStringFromCmdLine, bool saveOutputToLogFile, bool forceDelete, string applicationName, string sqlEventAnalyzerDatabaseName)
	{
		OutputHandler.WriteToLog("\"-u\" not available when running in a service context.");

		if (!GenericHelper.IsUserInteractive())
		{
			return 1;
		}

		if (saveOutputToLogFile)
		{
			StartInteractiveRunLog();
		}

		MainForm form = new MainForm(useSessionId, columnsXmlFileName, verboseMode, connectionStringFromCmdLine, forceDelete, applicationName, sqlEventAnalyzerDatabaseName);

		int returnCode = form.GetUnattendedExitCode();

		if (returnCode == 0)
		{
			Application.Run(form);
			return form.GetUnattendedExitCode();
		}
		else
		{
			return returnCode;
		}
	}

	private static int HandleUnattended(int numberOfImportFiles, string lastImportFile, string importNewerThanSessionId, string sessionId, string postScriptFileName, string postScript, int compressTraceFilesAfterImport, bool deleteTraceFilesAfterImport, string importPath, string columnsXmlFileName, string connectionStringFromCmdLine, bool saveOutputToLogFile, bool forceDelete, string sqlEventAnalyzerDatabaseName, List<string> generateStatisticsNames, List<string> generateStatisticsFilter1Names, List<string> generateStatisticsFilter2Names, string generateStatisticsSavePath, List<string> generateStatisticsSaveWebService)
	{
		if (saveOutputToLogFile)
		{
			StartInteractiveRunLog();
		}

		if (postScriptFileName == null || File.Exists(postScriptFileName))
		{
			return StartUnattended(postScriptFileName, postScript, numberOfImportFiles, lastImportFile, importNewerThanSessionId, sessionId, compressTraceFilesAfterImport, deleteTraceFilesAfterImport, importPath, columnsXmlFileName, connectionStringFromCmdLine, forceDelete, sqlEventAnalyzerDatabaseName, generateStatisticsNames, generateStatisticsFilter1Names, generateStatisticsFilter2Names, generateStatisticsSavePath, generateStatisticsSaveWebService);
		}
		else
		{
			ConfigHandler.LoadConfig();
			Translator.GenerateDictionary();

			string text = "Post Script File not found.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("postScriptFileNotFound");
			}

			ConfigHandler.SaveOutputToLogFile = saveOutputToLogFile;

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			return 1;
		}
	}

	private static int StartUnattended(string postScriptFileName, string postScript, int numberOfImportFiles, string lastImportFile, string importNewerThanSessionId, string sessionId, int compressTraceFilesAfterImport, bool deleteTraceFilesAfterImport, string importPath, string columnsXmlFileName, string connectionStringFromCmdLine, bool forceDelete, string sqlEventAnalyzerDatabaseName, List<string> generateStatisticsNames, List<string> generateStatisticsFilter1Names, List<string> generateStatisticsFilter2Names, string generateStatisticsSavePath, List<string> generateStatisticsSaveWebService)
	{
		MainForm form = new MainForm(postScriptFileName, postScript, numberOfImportFiles, lastImportFile, importNewerThanSessionId, sessionId, compressTraceFilesAfterImport, deleteTraceFilesAfterImport, importPath, columnsXmlFileName, connectionStringFromCmdLine, forceDelete, sqlEventAnalyzerDatabaseName, generateStatisticsNames, generateStatisticsFilter1Names, generateStatisticsFilter2Names, generateStatisticsSavePath, generateStatisticsSaveWebService);

		if (form.GetUnattendedExitCode() == 0)
		{
			if (!GenericHelper.IsUserInteractive())
			{
				form.HandleUnattended();
			}
			else
			{
				Application.Run(form);
			}
		}

		return form.GetUnattendedExitCode();
	}

	private static void WriteCommandSyntax()
	{
		ConfigHandler.LoadConfig();
		Translator.GenerateDictionary();

		CommandLineParametersForm form = new CommandLineParametersForm();
		form.Initialize();
		form.SetCommandSyntaxOptions();

		if (GenericHelper.IsUserInteractive())
		{
			form.ShowDialog();
		}

		OutputHandler.WriteToLog("Wrong unattended Parameters.");
	}

	private static void StartInteractiveRunLog()
	{
		ConfigHandler.SaveOutputToLogFile = true;
		OutputHandler.InitializeLog();
		OutputHandler.WriteToLog(string.Format("Interactive run started at {0}", ConfigHandler.UnattendedRunStartTime));
	}

	private static void CheckPermissions()
	{
		bool success = RegistryHandler.CheckRegistryAccess();
		ConfigHandler.RegistryModifyAccess = success;
	}

	private static DatabaseMessageObject GetDatabaseMessage(string connectionString, string sql)
	{
		string message = "";
		bool success = false;

		DatabaseOperation databaseOperation = new DatabaseOperation();

		SqlConnectionStringBuilder tempConnString = new SqlConnectionStringBuilder(connectionString);
		tempConnString.Password = ConnectionStringSecurity.Decode(tempConnString.Password);

		databaseOperation.InitializeDal(tempConnString.ToString());

		ErrorFormParams errorFormParams = databaseOperation.GetErrorFormParams();

		if (errorFormParams != null)
		{
			message = errorFormParams.Message;
		}
		else
		{
			try
			{
				DataTable dataTable = databaseOperation.ExecuteDataTable(sql);

				if (dataTable != null)
				{
					message = dataTable.Rows[0]["Message"].ToString();
					success = true;
				}
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}
		}

		return new DatabaseMessageObject(success, message);
	}

	private class DatabaseMessageObject
	{
		public readonly bool Success;
		public readonly string Message;

		public DatabaseMessageObject(bool success, string message)
		{
			Success = success;
			Message = message;
		}
	}
}
