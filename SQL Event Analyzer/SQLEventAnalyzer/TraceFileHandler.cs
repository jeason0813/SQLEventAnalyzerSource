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
using System.Windows.Forms;

public static class TraceFileHandler
{
	public static bool ImportTraceFile(DatabaseOperation databaseOperation, string fileName)
	{
		bool success = databaseOperation.ImportTraceFile(fileName);
		return success;
	}

	public static bool DeleteTraceFile(bool showError, DatabaseOperation databaseOperation)
	{
		databaseOperation.DropEventSession();

		if (Directory.Exists(ConfigHandler.RecordTraceFileDir))
		{
			foreach (string file in Directory.GetFiles(ConfigHandler.RecordTraceFileDir, string.Format("{0}*.*", ConfigHandler.TraceFileName)))
			{
				try
				{
					GenericHelper.DeleteFile(file);
				}
				catch (Exception ex)
				{
					if (showError)
					{
						string text = "Error deleting Trace File.\r\n\r\n{0}";

						if (ConfigHandler.UseTranslation)
						{
							text = Translator.GetText("errorDeletingTrace");
						}

						OutputHandler.Show(string.Format(text, ex.Message), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}

					return false;
				}
			}
		}
		else
		{
			if (showError)
			{
				string text = "The Trace File Directory \"{0}\" does not exist.\r\n\r\nPlease create the directory manually.";

				if (ConfigHandler.UseTranslation)
				{
					text = Translator.GetText("createTraceDirManually");
				}

				OutputHandler.Show(string.Format(text, ConfigHandler.RecordTraceFileDir), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			return false;
		}

		return true;
	}
}
