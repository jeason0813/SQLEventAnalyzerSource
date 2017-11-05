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
using System.IO;
using System.Text;
using System.Windows.Forms;

internal static class OutputHandler
{
	private static string _fileNameDate;
	private static StringBuilder _log;

	public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
	{
		WriteToLog(text);

		if (GenericHelper.IsUserInteractive())
		{
			return MessageBox.Show(text, caption, buttons, icon, MessageBoxDefaultButton.Button1);
		}

		return DialogResult.None;
	}

	public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton messageBoxDefaultButton)
	{
		WriteToLog(text);

		if (GenericHelper.IsUserInteractive())
		{
			return MessageBox.Show(text, caption, buttons, icon, messageBoxDefaultButton);
		}

		return DialogResult.None;
	}

	public static void InitializeLog()
	{
		DirectoryInfo di = new DirectoryInfo(GenericHelper.ExecPath);
		FileInfo[] logFiles = di.GetFiles(string.Format(@"{0}*.log", GenericHelper.ApplicationName));

		GenericHelper.DateCompareFileInfo dateCompareFileInfo = new GenericHelper.DateCompareFileInfo();

		Array.Sort(logFiles, dateCompareFileInfo);

		for (int i = ConfigHandler.NumberOfServiceContextLogFiles - 1; i < logFiles.Length; i++)
		{
			try
			{
				GenericHelper.DeleteFile(logFiles[i].FullName);
			}
			catch
			{
			}
		}

		_fileNameDate = GenericHelper.FormatFileNameDate(DateTime.Now);
		_log = new StringBuilder();
	}

	public static void WriteToLog(string text)
	{
		if (!GenericHelper.IsUserInteractive() || ConfigHandler.SaveOutputToLogFile)
		{
			string fileName = string.Format(@"{0}\{1} {2}.log", GenericHelper.ExecPath, GenericHelper.ApplicationName, _fileNameDate);

			try
			{
				File.AppendAllText(fileName, string.Format("{1}: {0}\r\n", text, FormatDate(DateTime.Now)));
			}
			catch
			{
			}
		}

		if (_log != null)
		{
			_log.Append(string.Format("{1}: {0}\r\n", text, FormatDate(DateTime.Now)));
		}
	}

	public static string GetLog()
	{
		return _log.ToString();
	}

	private static string FormatDate(DateTime dateTime)
	{
		return dateTime.ToString("s");
	}
}
