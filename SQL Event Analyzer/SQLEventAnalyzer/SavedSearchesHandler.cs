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

using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

public static class SavedSearchesHandler
{
	public static string SaveAs(string savedSearchesXml, string registryName, string savedSearchName)
	{
		string newName = GetName(savedSearchName, registryName, savedSearchesXml);

		if (newName != null)
		{
			Save(savedSearchesXml, registryName, newName);
		}

		return newName;
	}

	public static string Load(string savedSearchName, string registryName)
	{
		RegistryKey rk = Registry.LocalMachine;
		RegistryKey sk = rk.OpenSubKey(RegistryHandler.RegistryKey);

		string returnValue = "";

		if (sk != null)
		{
			string[] values = sk.GetValueNames();

			foreach (string value in values)
			{
				if (value == string.Format("SavedSearch_{0}_{1}", registryName, savedSearchName))
				{
					returnValue = sk.GetValue(value).ToString();
				}
			}
		}

		return returnValue;
	}

	public static void Delete(string savedSearchName, string registryName)
	{
		RegistryKey rk = Registry.LocalMachine;
		RegistryKey sk = rk.OpenSubKey(RegistryHandler.RegistryKey, true);

		if (sk != null)
		{
			string[] values = sk.GetValueNames();

			foreach (string value in values)
			{
				if (value == string.Format("SavedSearch_{0}_{1}", registryName, savedSearchName))
				{
					sk.DeleteValue(value);
				}
			}
		}
	}

	public static List<string> GetNames(string registryName)
	{
		List<string> names = new List<string>();

		RegistryKey rk = Registry.LocalMachine;
		RegistryKey sk = rk.OpenSubKey(RegistryHandler.RegistryKey);

		if (sk != null)
		{
			string[] values = sk.GetValueNames();

			foreach (string value in values)
			{
				if (value.StartsWith(string.Format("SavedSearch_{0}", registryName)))
				{
					names.Add(value.Substring(13 + registryName.Length, value.Length - (13 + registryName.Length)));
				}
			}
		}

		return names;
	}

	public static bool IsSystemObject(string savedSearchName, string registryName)
	{
		string[] systemObjectNamesArray = null;

		RegistryKey rk = Registry.LocalMachine;
		RegistryKey sk = rk.OpenSubKey(RegistryHandler.RegistryKey);

		if (sk != null)
		{
			string[] values = sk.GetValueNames();

			foreach (string value in values)
			{
				if (value == string.Format("SystemObjects_{0}", registryName))
				{
					systemObjectNamesArray = sk.GetValue(value).ToString().Split('|');
				}
			}
		}

		if (systemObjectNamesArray != null)
		{
			List<string> systemObjectNames = new List<string>(systemObjectNamesArray);

			foreach (string systemObjectName in systemObjectNames)
			{
				if (systemObjectName.ToLower() == savedSearchName.ToLower())
				{
					return true;
				}
			}
		}

		return false;
	}

	private static bool CheckForUniqueName(string savedSearchName, string registryName)
	{
		RegistryKey rk = Registry.LocalMachine;
		RegistryKey sk = rk.OpenSubKey(RegistryHandler.RegistryKey);

		if (sk != null)
		{
			string[] values = sk.GetValueNames();

			foreach (string value in values)
			{
				if (value == string.Format("SavedSearch_{0}_{1}", registryName, savedSearchName))
				{
					return false;
				}
			}
		}

		return true;
	}

	private static bool CheckForValidName(string savedSearchName)
	{
		if (ConfigHandler.UseTranslation && savedSearchName == Translator.GetText("Empty"))
		{
			return false;
		}

		if (savedSearchName == "Empty" || savedSearchName.Contains(",") || savedSearchName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 || savedSearchName.Contains("'") || savedSearchName.Contains("&") || savedSearchName.Contains("[") || savedSearchName.Contains("]"))
		{
			return false;
		}

		if (savedSearchName.Length > ConfigHandler.SavedSearchesMaxLength)
		{
			return false;
		}

		return true;
	}

	private static string GetName(string savedSearchName, string registryName, string savedSearchesXml)
	{
		string titleText = "Search name";

		if (ConfigHandler.UseTranslation)
		{
			titleText = Translator.GetText("SavedSearchName");
		}

		string groupBoxText = "Search";

		if (ConfigHandler.UseTranslation)
		{
			groupBoxText = Translator.GetText("SavedSearch");
		}

		string valueLabelText = "Name:";

		if (ConfigHandler.UseTranslation)
		{
			valueLabelText = Translator.GetText("Name1");
		}

		GetTextForm form = new GetTextForm(titleText, groupBoxText, valueLabelText, savedSearchName, ConfigHandler.SavedSearchesMaxLength);
		form.ShowDialog();

		string newName = form.GetText();

		if (newName == null)
		{
			return null;
		}

		if (IsSystemObject(newName, registryName))
		{
			string text1 = "The selected search is a system object and can not be overwritten.";

			if (ConfigHandler.UseTranslation)
			{
				text1 = Translator.GetText("CanNotOverwriteSystemObject");
			}

			OutputHandler.Show(text1, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);

			newName = GetName(form.GetText(), registryName, savedSearchesXml);
		}
		else
		{
			if (form.SaveChanges())
			{
				bool valid = CheckForValidName(form.GetText());

				if (!valid)
				{
					string text = "Name not valid.";

					if (ConfigHandler.UseTranslation)
					{
						text = Translator.GetText("NameNotValid");
					}

					OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);

					newName = GetName(form.GetText(), registryName, savedSearchesXml);
				}
				else
				{
					bool unique = CheckForUniqueName(form.GetText(), registryName);

					if (!unique)
					{
						string text = "Replace search \"{0}\"?";

						if (ConfigHandler.UseTranslation)
						{
							text = Translator.GetText("SaveSearch");
						}

						DialogResult result = OutputHandler.Show(string.Format(text, form.GetText()), GenericHelper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

						if (result == DialogResult.Yes)
						{
							Save(savedSearchesXml, registryName, form.GetText());
						}
						else
						{
							newName = GetName(form.GetText(), registryName, savedSearchesXml);
						}
					}
				}
			}
		}

		return newName;
	}

	private static void Save(string savedSearchesXml, string registryName, string savedSearchName)
	{
		RegistryHandler.SaveToRegistry(string.Format("SavedSearch_{0}_{1}", registryName, savedSearchName), savedSearchesXml);
	}
}
