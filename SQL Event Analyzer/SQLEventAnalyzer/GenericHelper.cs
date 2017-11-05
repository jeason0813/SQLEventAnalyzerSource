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
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public static class GenericHelper
{
	public static string ApplicationName = "SQL Event Analyzer";
	public const int NumberOfSearchHistoryItems = 10;

	public static string ExecPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
	public static string SqlServerName;
	public static int SqlServerVersion;
	public static bool CustomColumnsUpdatePending;

	[DllImport("user32")]
	private static extern bool HideCaret(IntPtr hWnd);
	public static void HideCaret(TextBox textBox)
	{
		HideCaret(textBox.Handle);
	}

	public static void WriteFile(string fileName, byte[] content)
	{
		const int numberOfRetries = 5;
		const int timeBetweenRetriesInSeconds = 30;

		int retryCounter = 0;

		while (retryCounter <= numberOfRetries)
		{
			try
			{
				using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None, 65536, FileOptions.WriteThrough))
				{
					fileStream.Write(content, 0, content.Length);
					fileStream.Flush();

					break;
				}
			}
			catch
			{
				if (retryCounter == numberOfRetries)
				{
					throw;
				}

				retryCounter++;

				System.Threading.Thread.Sleep(timeBetweenRetriesInSeconds * 1000);
			}
		}
	}

	public static void DeleteFile(string fileName)
	{
		const int retryCount = 10;
		int retryNumber = 0;

		while (retryNumber <= retryCount)
		{
			if (File.Exists(fileName))
			{
				try
				{
					File.Delete(fileName);
				}
				catch
				{
					System.Threading.Thread.Sleep(500);
				}

				retryNumber++;
			}
			else
			{
				break;
			}
		}
	}

	public static string TempTableName
	{ get; set; }

	public static bool IsUserInteractive()
	{
		if (SystemInformation.UserInteractive)
		{
			return true;
		}

		return false;
	}

	public static string FormatWithThousandSeparator(int input)
	{
		return input.ToString("N0", CultureInfo.CurrentCulture);
	}

	public static string GetSessionIdFromTableName()
	{
		return TempTableName.Substring("TraceData_".Length);
	}

	public static string TempPath
	{
		get
		{
			string tempPath = Path.GetTempPath();

			if (!Directory.Exists(tempPath))
			{
				Directory.CreateDirectory(tempPath);
			}

			if (tempPath.Substring(tempPath.Length - 1, 1) == @"\")
			{
				tempPath = tempPath.Substring(0, tempPath.Length - 1);
			}

			string appTempPath = string.Format(@"{0}\{1}", tempPath, ApplicationName);

			if (!Directory.Exists(appTempPath))
			{
				Directory.CreateDirectory(appTempPath);
			}

			return appTempPath;
		}
	}

	public static string GetServerName(string sqlServerNameAndInstanceName)
	{
		if (sqlServerNameAndInstanceName.Contains("\\"))
		{
			int pos = sqlServerNameAndInstanceName.IndexOf("\\");
			return sqlServerNameAndInstanceName.Substring(0, pos);
		}
		else
		{
			return sqlServerNameAndInstanceName;
		}
	}

	public static string GetSql(string fileName)
	{
		ResourceManager rm = new ResourceManager("SQLEventAnalyzer.Properties.Resources", Assembly.GetExecutingAssembly());

		string sql = rm.GetObject(fileName.Substring(0, fileName.Length - 4)) as string;
		rm.ReleaseAllResources();

		return sql;
	}

	public static bool CheckDynamicDateTimeValue(string valueToCheck, string name)
	{
		bool success = false;
		bool validInteger = true;

		if (valueToCheck.Trim().StartsWith("dd ") || valueToCheck.Trim().StartsWith("led ") || valueToCheck.Trim().StartsWith("dd-") || valueToCheck.Trim().StartsWith("led-") || valueToCheck.Trim().StartsWith("dd+") || valueToCheck.Trim().StartsWith("led+"))
		{
			int numberOfMinus = valueToCheck.Length - valueToCheck.Replace("-", "").Length;
			int numberOfPlus = valueToCheck.Length - valueToCheck.Replace("+", "").Length;

			if (numberOfMinus == 1 && numberOfPlus == 0)
			{
				int indexOfMinus = valueToCheck.IndexOf("-");
				string numberValue = valueToCheck.Substring(indexOfMinus + 1).Trim();

				validInteger = CheckIntegerValue(true, numberValue, name, false);

				if (validInteger)
				{
					success = true;
				}
			}
			else if (numberOfPlus == 1 && numberOfMinus == 0)
			{
				int indexOfPlus = valueToCheck.IndexOf("+");
				string numberValue = valueToCheck.Substring(indexOfPlus + 1).Trim();

				validInteger = CheckIntegerValue(true, numberValue, name, false);

				if (validInteger)
				{
					success = true;
				}
			}
		}

		if (!success && validInteger)
		{
			string text = "The dynamic value for \"{0}\" is not correct.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("WrongDynamicDateTimeValue");
			}

			OutputHandler.Show(string.Format(text, name), ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		return success;
	}

	public static string GetDynamicDateTimeText(string value, DateTime lastEventStartTime)
	{
		if (value.Trim().StartsWith("dd"))
		{
			return "getdate()";
		}
		else if (value.Trim().StartsWith("led"))
		{
			return DateTimeToSqlDateTimeWithMilliseconds(lastEventStartTime);
		}

		return null;
	}

	public static string GetDynamicDateTimeValue(string value)
	{
		string operatorValue = "-";
		int indexOfOperator = value.IndexOf("-");

		if (indexOfOperator == -1)
		{
			indexOfOperator = value.IndexOf("+");
			operatorValue = "+";
		}

		string numberValue = value.Substring(indexOfOperator + 1).Trim();

		return string.Format("{0} {1}", operatorValue, numberValue);
	}

	public static bool CheckIntegerValue(bool allowZero, string valueToCheck, string name, bool allowMinusOne)
	{
		try
		{
			int newValue = Convert.ToInt32(valueToCheck);

			if (allowMinusOne)
			{
				if (newValue < -1)
				{
					string text = "\"{0}\" must be equal to or greater than -1.";

					if (ConfigHandler.UseTranslation)
					{
						text = Translator.GetText("GreaterThanMinusOneEqual");
					}

					OutputHandler.Show(string.Format(text, name), ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return false;
				}
			}
			else
			{
				if (allowZero)
				{
					if (newValue < 0)
					{
						string text = "\"{0}\" must be equal to or greater than 0.";

						if (ConfigHandler.UseTranslation)
						{
							text = Translator.GetText("GreaterThanZeroEqual");
						}

						OutputHandler.Show(string.Format(text, name), ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
						return false;
					}
				}
				else
				{
					if (newValue <= 0)
					{
						string text = "\"{0}\" must be greater than 0.";

						if (ConfigHandler.UseTranslation)
						{
							text = Translator.GetText("GreaterThanZero");
						}

						OutputHandler.Show(string.Format(text, name), ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
						return false;
					}
				}
			}
		}
		catch
		{
			string text = "\"{0}\" is not a valid number.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("NotValidNumber");
			}

			OutputHandler.Show(string.Format(text, name), ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			return false;
		}

		return true;
	}

	public static string Version
	{
		get
		{
			Assembly asm = Assembly.GetExecutingAssembly();

			if (asm.GetName().Version.Revision > 0)
			{
				return string.Format("{0}.{1}.{2}.{3}", asm.GetName().Version.Major, asm.GetName().Version.Minor, asm.GetName().Version.Build, asm.GetName().Version.Revision);
			}
			else
			{
				return string.Format("{0}.{1}.{2}", asm.GetName().Version.Major, asm.GetName().Version.Minor, asm.GetName().Version.Build);
			}
		}
	}

	public static string InfoText
	{
		get
		{
			string text = "Application version: \t\t{0}\r\nSQL Server version: \t{1}\r\nSQL Server: \t\t{2}\r\nSession Id: \t\t{3}\r\nLast import time: \t\t{4}\r\nLast columns handling time: \t{5}\r\nLast data retrieval time: \t{6}";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("infoText");
			}

			TimeSpan importTime = ConfigHandler.ImportEndTime - ConfigHandler.ImportStartTime;
			TimeSpan handleColumnsTime = ConfigHandler.HandleColumnsEndTime - ConfigHandler.HandleColumnsStartTime;
			TimeSpan getDataTime = ConfigHandler.GetDataEndTime - ConfigHandler.GetDataStartTime;

			string tempTableName = "";

			if (TempTableName != null)
			{
				tempTableName = GetSessionIdFromTableName();
			}

			return string.Format(text, Version, SqlServerVersion, SqlServerName, tempTableName, FormatTimeSpan(importTime), FormatTimeSpan(handleColumnsTime), FormatTimeSpan(getDataTime));
		}
	}

	public class DateCompareFileInfo : IComparer<FileInfo>
	{
		public int Compare(FileInfo fi1, FileInfo fi2)
		{
			int result;

			if (fi1.CreationTime == fi2.CreationTime)
			{
				result = 0;
			}
			else if (fi1.CreationTime < fi2.CreationTime)
			{
				result = 1;
			}
			else
			{
				result = -1;
			}

			return result;
		}
	}

	public static string FormatFileNameDate(DateTime dateTime)
	{
		return dateTime.ToString(ConfigHandler.DateTimeFileFormat);
	}

	public static string FormatDate(DateTime dateTime)
	{
		return dateTime.ToString(ConfigHandler.DateTimeFormat);
	}

	public static string FormatLongDate(DateTime dateTime)
	{
		return dateTime.ToString(ConfigHandler.DateTimeLongFormat);
	}

	public static string DateTimeToSqlDateTime(DateTime dateTime)
	{
		return string.Format("convert(datetime, '{0}-{1}-{2} {3}:{4}:{5}', 120)", dateTime.Year, AddZeroToSingleDigit(dateTime.Month), AddZeroToSingleDigit(dateTime.Day), AddZeroToSingleDigit(dateTime.Hour), AddZeroToSingleDigit(dateTime.Minute), AddZeroToSingleDigit(dateTime.Second));
	}

	public static string DateTimeToSqlDateTimeWithMilliseconds(DateTime dateTime)
	{
		return string.Format("convert(datetime, '{0}-{1}-{2} {3}:{4}:{5}.{6}', 121)", dateTime.Year, AddZeroToSingleDigit(dateTime.Month), AddZeroToSingleDigit(dateTime.Day), AddZeroToSingleDigit(dateTime.Hour), AddZeroToSingleDigit(dateTime.Minute), AddZeroToSingleDigit(dateTime.Second), AddZerosToTripleDigit(dateTime.Millisecond));
	}

	public static string AddZeroToSingleDigit(int number)
	{
		if (number.ToString().Length == 1)
		{
			return string.Format("0{0}", number);
		}

		return number.ToString();
	}

	public static string AddZerosToTripleDigit(int number)
	{
		if (number.ToString().Length == 1)
		{
			return string.Format("00{0}", number);
		}
		else if (number.ToString().Length == 2)
		{
			return string.Format("0{0}", number);
		}

		return number.ToString();
	}

	public static string FormatTimeSpan(TimeSpan timeSpan)
	{
		string days = timeSpan.Days.ToString();
		string hours = timeSpan.Hours.ToString();
		string minutes = timeSpan.Minutes.ToString();
		string seconds = timeSpan.Seconds.ToString();

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

		return string.Format("{0}:{1}:{2}:{3}", days, hours, minutes, seconds);
	}

	public static void KillPreviousConnection(DatabaseOperation connection, string sessionId)
	{
		string connectionString = connection.GetConnectionString();

		SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder(connectionString);
		connString.ApplicationName = ApplicationName;

		DatabaseOperation killConnection = new DatabaseOperation();
		killConnection.ChangeConnection(connString.ToString());
		killConnection.KillConnections(sessionId);
		killConnection.Dispose();
	}

	public static bool UniqueElements(string[] elements)
	{
		List<string> elementList = new List<string>();

		foreach (string element in elements)
		{
			if (!elementList.Contains(element))
			{
				elementList.Add(element);
			}
			else
			{
				return false;
			}
		}

		return true;
	}

	public static string GetYesText()
	{
		if (ConfigHandler.UseTranslation)
		{
			return Translator.GetText("Yes");
		}
		else
		{
			return "Yes";
		}
	}

	public static string GetNoText()
	{
		if (ConfigHandler.UseTranslation)
		{
			return Translator.GetText("No");
		}
		else
		{
			return "No";
		}
	}

	public static string GetAndText()
	{
		if (ConfigHandler.UseTranslation)
		{
			return Translator.GetText("and");
		}
		else
		{
			return "and";
		}
	}

	public static string GetOrText()
	{
		if (ConfigHandler.UseTranslation)
		{
			return Translator.GetText("or");
		}
		else
		{
			return "or";
		}
	}

	public static string GetEqualText()
	{
		if (ConfigHandler.UseTranslation)
		{
			return Translator.GetText("equal");
		}
		else
		{
			return "Equal";
		}
	}

	public static string GetNotEqualText()
	{
		if (ConfigHandler.UseTranslation)
		{
			return Translator.GetText("notEqual");
		}
		else
		{
			return "Not equal";
		}
	}

	public static string GetInText()
	{
		if (ConfigHandler.UseTranslation)
		{
			return Translator.GetText("in");
		}
		else
		{
			return "In";
		}
	}

	public static string GetNotInText()
	{
		if (ConfigHandler.UseTranslation)
		{
			return Translator.GetText("notin");
		}
		else
		{
			return "Not in";
		}
	}

	public static string GetGreaterThanText()
	{
		if (ConfigHandler.UseTranslation)
		{
			return Translator.GetText("greaterThan");
		}
		else
		{
			return "Greater than";
		}
	}

	public static string GetLessThanText()
	{
		if (ConfigHandler.UseTranslation)
		{
			return Translator.GetText("lessThan");
		}
		else
		{
			return "Less than";
		}
	}

	public static bool ConfirmDropTempTable(string sessionId)
	{
		string text = "You have manually created or connected to Session \"{0}\". It is chosen not to keep Sessions on exit (can be configured in Options).\r\n\r\nPermanently delete Session \"{0}\"?";

		if (ConfigHandler.UseTranslation)
		{
			text = Translator.GetText("PermanentlyDeleteSession");
		}

		DialogResult result = OutputHandler.Show(string.Format(text, sessionId), ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

		if (result.ToString() == "Yes")
		{
			return true;
		}

		return false;
	}
}
