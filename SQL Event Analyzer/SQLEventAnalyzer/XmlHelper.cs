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
using System.Xml;

public static class XmlHelper
{
	public static string ReadXmlFromFile(string fileName)
	{
		if (AnyUpdatesPending(fileName))
		{
			return ApplyCustomColumnsUpdate(fileName);
		}
		else
		{
			try
			{
				if (File.Exists(fileName))
				{
					return File.ReadAllText(fileName, Encoding.UTF8);
				}
			}
			catch (Exception ex)
			{
				OutputHandler.Show(string.Format("Error reading Xml from file.\r\n\r\n{0}", ex.Message), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

		}

		return null;
	}

	public static bool WriteXmlToFile(string xml, string fileName)
	{
		try
		{
			if (File.Exists(fileName))
			{
				GenericHelper.DeleteFile(fileName);
			}

			XmlDocument xmlDocument = FormatXml(xml);
			xmlDocument.LoadXml(xml);

			XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, Encoding.UTF8);
			xmlTextWriter.IndentChar = '\t';
			xmlTextWriter.Indentation = 1;
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlDocument.WriteContentTo(xmlTextWriter);

			xmlTextWriter.Flush();
			xmlTextWriter.Close();

			return true;
		}
		catch (Exception ex)
		{
			MessageBox.Show(string.Format("Error writing Xml to file.\r\n\r\n{0}", ex.Message), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return false;
		}
	}

	private static XmlDocument FormatXml(string xml)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(xml);

		XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//*[. = '' and count(*) = 0]");

		if (xmlNodeList != null)
		{
			foreach (XmlElement xmlElement in xmlNodeList)
			{
				xmlElement.IsEmpty = true;
			}
		}

		return xmlDocument;
	}

	private static bool AnyUpdatesPending(string fileName)
	{
		string tempFilenameAndPath = string.Format(@"{0}\{1}", GenericHelper.TempPath, Path.GetFileName(fileName));

		if (File.Exists(fileName) && File.Exists(tempFilenameAndPath))
		{
			return true;
		}

		return false;
	}

	private static string ApplyCustomColumnsUpdate(string fileName)
	{
		string updatedXml = null;
		string tempFilenameAndPath = string.Format(@"{0}\{1}", GenericHelper.TempPath, Path.GetFileName(fileName));

		try
		{
			string oldXml = File.ReadAllText(fileName, Encoding.UTF8);
			ColumnCollection oldColumns = ColumnHelper.XmlToColumnCollection(oldXml);

			string newXml = File.ReadAllText(tempFilenameAndPath, Encoding.UTF8);
			ColumnCollection newColumns = ColumnHelper.XmlToColumnCollection(newXml);

			foreach (Parameter oldParameter in oldColumns.Parameters)
			{
				foreach (Parameter newParameter in newColumns.Parameters)
				{
					if (oldParameter.Name == newParameter.Name)
					{
						newParameter.Value = oldParameter.Value;
						break;
					}
				}
			}

			updatedXml = ColumnHelper.ColumnCollectionToXml(newColumns);

			WriteXmlToFile(ColumnHelper.ColumnCollectionToXml(newColumns), fileName);
			GenericHelper.DeleteFile(tempFilenameAndPath);
		}
		catch (Exception ex)
		{
			OutputHandler.Show(string.Format("Error applying Custom Column update.\r\n\r\n{0}", ex.Message), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		return updatedXml;
	}
}
