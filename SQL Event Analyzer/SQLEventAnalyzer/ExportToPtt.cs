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
using System.Text;
using System.Windows.Forms;

public class ExportToPtt
{
	public static void Export(DataTable dataTable, string databaseName, string fileName)
	{
		bool success = SaveTaskCollection(dataTable, databaseName, fileName);

		if (success)
		{
			string text = "Export successful.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("ExportSuccessful");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}

	private static bool SaveTaskCollection(DataTable traceData, string databaseName, string fileName)
	{
		TaskCollection taskCollection = ImportTrace(traceData, databaseName);
		return XmlHelper.WriteXmlToFile(TaskCollectionToXml(taskCollection), fileName);
	}

	private static string TaskCollectionToXml(TaskCollection taskCollection)
	{
		if (taskCollection.Tasks == null)
		{
			return "";
		}

		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
		stringBuilder.Append(string.Format("<tasks description=\"{0}\" connections=\"{1}\" timeBetweenConnections=\"{2}\" performanceCountersSamplingInterval=\"{3}\" mode=\"{4}\" usePooling=\"{5}\" minPooling=\"{6}\" maxPooling=\"{7}\">", System.Security.SecurityElement.Escape(TaskCollection.Description), TaskCollection.Connections, TaskCollection.TimeBetweenConnections, TaskCollection.PerformanceCountersSamplingInterval, TaskCollection.Mode, TaskCollection.UsePooling, TaskCollection.MinPooling, TaskCollection.MaxPooling));

		foreach (Task task in taskCollection.Tasks)
		{
			stringBuilder.Append(string.Format("<task name=\"{0}\" sql=\"{1}\" type=\"{2}\" description=\"{3}\" enabled=\"{4}\" delayAfterCompletion=\"{5}\" includeInResults=\"{6}\" />", System.Security.SecurityElement.Escape(task.Name), System.Security.SecurityElement.Escape(task.Sql), "Normal", System.Security.SecurityElement.Escape(task.Description), task.Enabled, task.DelayAfterCompletion, task.IncludeInResults));
		}

		stringBuilder.Append("</tasks>");
		return stringBuilder.ToString();
	}

	private static TaskCollection ImportTrace(DataTable traceData, string databaseName)
	{
		List<TraceFileDataPtt> traceFileDataList = GetTraceData(traceData, databaseName);
		return TraceFileDataListToTaskCollection(traceFileDataList);
	}

	private static List<TraceFileDataPtt> GetTraceData(DataTable traceData, string databaseName)
	{
		List<TraceFileDataPtt> traceFileDataList = new List<TraceFileDataPtt>();

		foreach (DataRow row in traceData.Rows)
		{
			traceFileDataList.Add(new TraceFileDataPtt(row["TextData"].ToString(), Convert.ToDateTime(row["StartTime"]), databaseName.Replace("'", "''")));
		}

		return traceFileDataList;
	}

	private class TraceFileDataPtt
	{
		public readonly string TextData;
		public DateTime StartTime;
		public readonly string DatabaseName;

		public TraceFileDataPtt(string textData, DateTime startTime, string databaseName)
		{
			TextData = textData;
			StartTime = startTime;
			DatabaseName = databaseName;
		}
	}

	private class TaskCollection
	{
		public readonly List<Task> Tasks = new List<Task>();
		public static readonly string Description = string.Format("Task Collection created by {0}.", GenericHelper.ApplicationName);
		public const int PerformanceCountersSamplingInterval = 0;
		public const int Connections = 1;
		public const int TimeBetweenConnections = 0;
		public const string Mode = "Parallel";
		public const bool UsePooling = true;
		public const int MinPooling = 0;
		public const int MaxPooling = 100;
	}

	private class Task
	{
		public readonly string Name;
		public readonly string Description;
		public readonly int DelayAfterCompletion;
		public readonly string Sql;
		public readonly bool Enabled;
		public readonly bool IncludeInResults;

		public Task(string name, string description, int delayAfterCompletion, string sql, bool enabled, bool includeInResults)
		{
			Name = name;
			Description = description;
			DelayAfterCompletion = delayAfterCompletion;
			Sql = sql;
			Enabled = enabled;
			IncludeInResults = includeInResults;
		}

		public override string ToString()
		{
			return Name;
		}
	}

	private static TaskCollection TraceFileDataListToTaskCollection(List<TraceFileDataPtt> traceFileDataList)
	{
		TaskCollection taskCollection = new TaskCollection();

		traceFileDataList.Sort
		(
			delegate(TraceFileDataPtt t1, TraceFileDataPtt t2)
			{
				return t1.StartTime.CompareTo(t2.StartTime);
			}
		);

		for (int i = 0; i < traceFileDataList.Count; i++)
		{
			const string taskDescription = "";
			const bool enabled = true;
			const bool includeInResults = true;
			int delayAfterCompletion = 0;

			if (i > 0)
			{
				DateTime currentStartTime = traceFileDataList[i].StartTime;
				DateTime previousStartTime = traceFileDataList[i - 1].StartTime;
				TimeSpan diff = currentStartTime.Subtract(previousStartTime);
				delayAfterCompletion = Convert.ToInt32(diff.TotalMilliseconds);
			}

			string name = string.Format("Task {0} ({1})", i + 1, GetPartOfTaskName(traceFileDataList[i].TextData));
			string sql = string.Format("use [{0}]\r\n\r\n{1}", traceFileDataList[i].DatabaseName, traceFileDataList[i].TextData);

			sql = sql.Replace("\n", "\r\n");
			sql = sql.Replace("\r\r\n", "\r\n");

			Task task = new Task(name, taskDescription, delayAfterCompletion, sql, enabled, includeInResults);
			taskCollection.Tasks.Add(task);
		}

		return taskCollection;
	}

	private static string GetPartOfTaskName(string textData)
	{
		int maxLen = 20;

		if (textData.Length < maxLen)
		{
			maxLen = textData.Length;
		}

		string partOfTaskName = textData.Replace("\t", " ");
		partOfTaskName = partOfTaskName.Replace("\n", "\r\n");
		partOfTaskName = partOfTaskName.Replace("\r\r\n", "\r\n");
		partOfTaskName = partOfTaskName.Replace("\r\n", " ");

		while (partOfTaskName.Contains("  "))
		{
			partOfTaskName = partOfTaskName.Replace("  ", " ");
		}

		if (partOfTaskName.Length > maxLen)
		{
			partOfTaskName = string.Format("{0}...", partOfTaskName.Substring(0, maxLen));
		}
		else
		{
			partOfTaskName = partOfTaskName.Substring(0, partOfTaskName.Length);
		}

		return partOfTaskName;
	}
}
