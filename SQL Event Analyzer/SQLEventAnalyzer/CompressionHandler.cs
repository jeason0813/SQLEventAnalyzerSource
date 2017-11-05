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

using System.Diagnostics;

public static class CompressionHandler
{
	public static void CompressFile(string inputFile, string outputFile, string sevenZipFileName)
	{
		Process process = new Process();
		process.StartInfo.FileName = sevenZipFileName;
		process.StartInfo.Arguments = string.Format("a -t7z \"{0}\" \"{1}\"", outputFile, inputFile);
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.CreateNoWindow = true;

		process.Start();
		process.WaitForExit();
	}

	public static void CompressDirectory(string inputDirectory, string outputFile, string sevenZipFileName)
	{
		Process process = new Process();
		process.StartInfo.FileName = sevenZipFileName;
		process.StartInfo.WorkingDirectory = inputDirectory;
		process.StartInfo.Arguments = string.Format("a -tzip \"{0}\"", outputFile);
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.CreateNoWindow = true;

		process.Start();
		process.WaitForExit();
	}
}
