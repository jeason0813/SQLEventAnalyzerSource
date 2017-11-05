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
using System.Drawing;
using System.Text;
using System.Windows.Forms;

public partial class ViewStatisticsForm : Form
{
	private List<string> _groupBys;
	private DatabaseOperation _databaseOperation;
	private DataViewerParameters _dataViewerParametersDataViewer1;
	private DateTime _minStartTime;
	private DateTime _maxStartTime;
	private int _totalRows;
	private ToolTip _toolTip;

	public ViewStatisticsForm()
	{
		InitializeComponent();
	}

	public void Initialize(DatabaseOperation databaseOperation, List<string> groupBys, bool filter1Applied, bool filter2Applied, bool unattended, int totalRows)
	{
		InitializeDictionary();
		Text = GenericHelper.ApplicationName;
		SetSize();

		_toolTip = new ToolTip();
		_totalRows = totalRows;

		_groupBys = groupBys;
		_databaseOperation = databaseOperation;

		string where = GetWhere();
		_dataViewerParametersDataViewer1 = DalTranslator.GetStatisticsData();

		if (!unattended)
		{
			ColumnPropertiesHandler.LoadColumnWidthAndOrder(_dataViewerParametersDataViewer1, "DataViewer_StatisticsData");
		}
		else
		{
			List<KeyValuePair<string, string[]>> columnsToReorder = new List<KeyValuePair<string, string[]>>();

			foreach (KeyValuePair<string, string[]> visibleColumn in _dataViewerParametersDataViewer1.Columns)
			{
				if (groupBys.Contains(visibleColumn.Key))
				{
					columnsToReorder.Add(visibleColumn);
				}
			}

			for (int i = 0; i < groupBys.Count; i++)
			{
				foreach (KeyValuePair<string, string[]> column in columnsToReorder)
				{
					if (column.Key == groupBys[i])
					{
						Dictionary<string, string[]> newColumns = ColumnPropertiesHandler.ChangeColumnOrder(_dataViewerParametersDataViewer1, column.Key, i - 1);
						_dataViewerParametersDataViewer1.Columns = newColumns;
					}
				}
			}
		}

		DataViewerParameters dataViewerParameters = DalTranslator.GetTraceData();

		foreach (KeyValuePair<string, string[]> column in dataViewerParameters.Columns)
		{
			if (ConfigHandler.GroupByColumns.Contains(column.Key))
			{
				_dataViewerParametersDataViewer1.Columns[column.Key].SetValue("SearchableShow", 4);
			}
			else
			{
				_dataViewerParametersDataViewer1.Columns[column.Key].SetValue("NonSearchableHide", 4);
			}
		}

		_dataViewerParametersDataViewer1.WhereActive = where;

		if (!unattended)
		{
			ColumnPropertiesHandler.SaveColumnWidthAndOrder(_dataViewerParametersDataViewer1, "DataViewer_StatisticsData");
		}

		InitializeDataViewer();

		DataSet dataSet = dataViewer1.GetDataSource();

		if (dataSet.Tables[1].Rows[0]["MinStartTime"] != DBNull.Value)
		{
			_minStartTime = Convert.ToDateTime(dataSet.Tables[1].Rows[0]["MinStartTime"]);
		}

		if (dataSet.Tables[1].Rows[0]["MaxStartTime"] != DBNull.Value)
		{
			_maxStartTime = Convert.ToDateTime(dataSet.Tables[1].Rows[0]["MaxStartTime"]);
		}

		SetFilterLabel(filter1Applied, filter2Applied);
		InitializeShowCheckBoxes();

		MinimumSize = new Size(900, 680); // error in .NET
	}

	public DateTime GetMinStartTime()
	{
		return _minStartTime;
	}

	public DateTime GetMaxStartTime()
	{
		return _maxStartTime;
	}

	public void ExportDataUnattended(string fileName)
	{
		string originalMinDuration = _dataViewerParametersDataViewer1.Columns["MinDuration"][4];
		string originalMaxDuration = _dataViewerParametersDataViewer1.Columns["MaxDuration"][4];
		string originalAvgDuration = _dataViewerParametersDataViewer1.Columns["AvgDuration"][4];
		string originalDevDuration = _dataViewerParametersDataViewer1.Columns["DevDuration"][4];
		string originalVarDuration = _dataViewerParametersDataViewer1.Columns["VarDuration"][4];
		string originalSumDuration = _dataViewerParametersDataViewer1.Columns["SumDuration"][4];
		string originalMinReads = _dataViewerParametersDataViewer1.Columns["MinReads"][4];
		string originalMaxReads = _dataViewerParametersDataViewer1.Columns["MaxReads"][4];
		string originalAvgReads = _dataViewerParametersDataViewer1.Columns["AvgReads"][4];
		string originalDevReads = _dataViewerParametersDataViewer1.Columns["DevReads"][4];
		string originalVarReads = _dataViewerParametersDataViewer1.Columns["VarReads"][4];
		string originalSumReads = _dataViewerParametersDataViewer1.Columns["SumReads"][4];
		string originalMinWrites = _dataViewerParametersDataViewer1.Columns["MinWrites"][4];
		string originalMaxWrites = _dataViewerParametersDataViewer1.Columns["MaxWrites"][4];
		string originalAvgWrites = _dataViewerParametersDataViewer1.Columns["AvgWrites"][4];
		string originalDevWrites = _dataViewerParametersDataViewer1.Columns["DevWrites"][4];
		string originalVarWrites = _dataViewerParametersDataViewer1.Columns["VarWrites"][4];
		string originalSumWrites = _dataViewerParametersDataViewer1.Columns["SumWrites"][4];
		string originalMinCPU = _dataViewerParametersDataViewer1.Columns["MinCPU"][4];
		string originalMaxCPU = _dataViewerParametersDataViewer1.Columns["MaxCPU"][4];
		string originalAvgCPU = _dataViewerParametersDataViewer1.Columns["AvgCPU"][4];
		string originalDevCPU = _dataViewerParametersDataViewer1.Columns["DevCPU"][4];
		string originalVarCPU = _dataViewerParametersDataViewer1.Columns["VarCPU"][4];
		string originalSumCPU = _dataViewerParametersDataViewer1.Columns["SumCPU"][4];
		string originalMinRows = _dataViewerParametersDataViewer1.Columns["MinRows"][4];
		string originalMaxRows = _dataViewerParametersDataViewer1.Columns["MaxRows"][4];
		string originalAvgRows = _dataViewerParametersDataViewer1.Columns["AvgRows"][4];
		string originalDevRows = _dataViewerParametersDataViewer1.Columns["DevRows"][4];
		string originalVarRows = _dataViewerParametersDataViewer1.Columns["VarRows"][4];
		string originalSumRows = _dataViewerParametersDataViewer1.Columns["SumRows"][4];

		_dataViewerParametersDataViewer1.Columns["MinDuration"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["MaxDuration"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["AvgDuration"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["DevDuration"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["VarDuration"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["SumDuration"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["MinReads"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["MaxReads"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["AvgReads"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["DevReads"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["VarReads"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["SumReads"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["MinWrites"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["MaxWrites"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["AvgWrites"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["DevWrites"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["VarWrites"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["SumWrites"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["MinCPU"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["MaxCPU"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["AvgCPU"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["DevCPU"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["VarCPU"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["SumCPU"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["MinRows"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["MaxRows"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["AvgRows"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["DevRows"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["VarRows"][4] = "SearchableShow";
		_dataViewerParametersDataViewer1.Columns["SumRows"][4] = "SearchableShow";

		ExportForm form = new ExportForm();
		form.DisableExportSql();
		form.DisableExportPtt();
		form.DisableExportSession();
		form.Initialize(_databaseOperation, dataViewer1, _dataViewerParametersDataViewer1);

		form.DoExportCsvAllPagesUnattended(fileName, _totalRows);

		_minStartTime = form.GetMinStartTime();
		_maxStartTime = form.GetMaxStartTime();

		_dataViewerParametersDataViewer1.Columns["MinDuration"][4] = originalMinDuration;
		_dataViewerParametersDataViewer1.Columns["MaxDuration"][4] = originalMaxDuration;
		_dataViewerParametersDataViewer1.Columns["AvgDuration"][4] = originalAvgDuration;
		_dataViewerParametersDataViewer1.Columns["DevDuration"][4] = originalDevDuration;
		_dataViewerParametersDataViewer1.Columns["VarDuration"][4] = originalVarDuration;
		_dataViewerParametersDataViewer1.Columns["SumDuration"][4] = originalSumDuration;
		_dataViewerParametersDataViewer1.Columns["MinReads"][4] = originalMinReads;
		_dataViewerParametersDataViewer1.Columns["MaxReads"][4] = originalMaxReads;
		_dataViewerParametersDataViewer1.Columns["AvgReads"][4] = originalAvgReads;
		_dataViewerParametersDataViewer1.Columns["DevReads"][4] = originalDevReads;
		_dataViewerParametersDataViewer1.Columns["VarReads"][4] = originalVarReads;
		_dataViewerParametersDataViewer1.Columns["SumReads"][4] = originalSumReads;
		_dataViewerParametersDataViewer1.Columns["MinWrites"][4] = originalMinWrites;
		_dataViewerParametersDataViewer1.Columns["MaxWrites"][4] = originalMaxWrites;
		_dataViewerParametersDataViewer1.Columns["AvgWrites"][4] = originalAvgWrites;
		_dataViewerParametersDataViewer1.Columns["DevWrites"][4] = originalDevWrites;
		_dataViewerParametersDataViewer1.Columns["VarWrites"][4] = originalVarWrites;
		_dataViewerParametersDataViewer1.Columns["SumWrites"][4] = originalSumWrites;
		_dataViewerParametersDataViewer1.Columns["MinCPU"][4] = originalMinCPU;
		_dataViewerParametersDataViewer1.Columns["MaxCPU"][4] = originalMaxCPU;
		_dataViewerParametersDataViewer1.Columns["AvgCPU"][4] = originalAvgCPU;
		_dataViewerParametersDataViewer1.Columns["DevCPU"][4] = originalDevCPU;
		_dataViewerParametersDataViewer1.Columns["VarCPU"][4] = originalVarCPU;
		_dataViewerParametersDataViewer1.Columns["SumCPU"][4] = originalSumCPU;
		_dataViewerParametersDataViewer1.Columns["MinRows"][4] = originalMinRows;
		_dataViewerParametersDataViewer1.Columns["MaxRows"][4] = originalMaxRows;
		_dataViewerParametersDataViewer1.Columns["AvgRows"][4] = originalAvgRows;
		_dataViewerParametersDataViewer1.Columns["DevRows"][4] = originalDevRows;
		_dataViewerParametersDataViewer1.Columns["VarRows"][4] = originalVarRows;
		_dataViewerParametersDataViewer1.Columns["SumRows"][4] = originalSumRows;
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
		dataViewer1.SetFocus();
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			okButton.Text = Translator.GetText("okButton");
			fileToolStripMenuItem.Text = Translator.GetText("fileToolStripMenuItem");
			closeToolStripMenuItem.Text = Translator.GetText("closeToolStripMenuItem");
			statisticsGroupBox.Text = Translator.GetText("statisticsTabPage");
			exportToolStripMenuItem.Text = Translator.GetText("exportToolStripButton1");
			viewToolStripMenuItem.Text = Translator.GetText("viewToolStripMenuItem");
			columnsToolStripMenuItem.Text = Translator.GetText("Columns");
			viewToolStripMenuItem1.Text = Translator.GetText("viewToolStripMenuItem1");

			dataViewer1.SetText(Translator.GetText("ClearButtonText"), Translator.GetText("SearchButtonText"), Translator.GetText("PageText"), Translator.GetText("IsDataModifiedText"), Translator.GetText("IsDataModifiedCaptionText"), Translator.GetText("OutOf"), Translator.GetText("TotalText"), Translator.GetText("SearchBoxToolTipText"), Translator.GetText("NotValidIntegerText"));
		}
	}

	private void InitializeShowCheckBoxes()
	{
		foreach (KeyValuePair<string, string[]> column in _dataViewerParametersDataViewer1.Columns)
		{
			if (column.Key == "MinDuration")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					minDurationToolStripMenuItem.Checked = true;
				}
				else
				{
					minDurationToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "MaxDuration")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					maxDurationToolStripMenuItem.Checked = true;
				}
				else
				{
					maxDurationToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "AvgDuration")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					avgDurationToolStripMenuItem.Checked = true;
				}
				else
				{
					avgDurationToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "DevDuration")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					devDurationToolStripMenuItem.Checked = true;
				}
				else
				{
					devDurationToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "VarDuration")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					varDurationToolStripMenuItem.Checked = true;
				}
				else
				{
					varDurationToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "SumDuration")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					sumDurationToolStripMenuItem.Checked = true;
				}
				else
				{
					sumDurationToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "MinReads")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					minReadsToolStripMenuItem.Checked = true;
				}
				else
				{
					minReadsToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "MaxReads")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					maxReadsToolStripMenuItem.Checked = true;
				}
				else
				{
					maxReadsToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "AvgReads")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					avgReadsToolStripMenuItem.Checked = true;
				}
				else
				{
					avgReadsToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "DevReads")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					devReadsToolStripMenuItem.Checked = true;
				}
				else
				{
					devReadsToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "VarReads")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					varReadsToolStripMenuItem.Checked = true;
				}
				else
				{
					varReadsToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "SumReads")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					sumReadsToolStripMenuItem.Checked = true;
				}
				else
				{
					sumReadsToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "MinWrites")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					minWritesToolStripMenuItem.Checked = true;
				}
				else
				{
					minWritesToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "MaxWrites")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					maxWritesToolStripMenuItem.Checked = true;
				}
				else
				{
					maxWritesToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "AvgWrites")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					avgWritesToolStripMenuItem.Checked = true;
				}
				else
				{
					avgWritesToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "DevWrites")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					devWritesToolStripMenuItem.Checked = true;
				}
				else
				{
					devWritesToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "VarWrites")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					varWritesToolStripMenuItem.Checked = true;
				}
				else
				{
					varWritesToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "SumWrites")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					sumWritesToolStripMenuItem.Checked = true;
				}
				else
				{
					sumWritesToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "MinCPU")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					minCpuToolStripMenuItem.Checked = true;
				}
				else
				{
					minCpuToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "MaxCPU")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					maxCpuToolStripMenuItem.Checked = true;
				}
				else
				{
					maxCpuToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "AvgCPU")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					avgCpuToolStripMenuItem.Checked = true;
				}
				else
				{
					avgCpuToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "DevCPU")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					devCpuToolStripMenuItem.Checked = true;
				}
				else
				{
					devCpuToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "VarCPU")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					varCpuToolStripMenuItem.Checked = true;
				}
				else
				{
					varCpuToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "SumCPU")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					sumCpuToolStripMenuItem.Checked = true;
				}
				else
				{
					sumCpuToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "MinRows")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					minRowsToolStripMenuItem.Checked = true;
				}
				else
				{
					minRowsToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "MaxRows")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					maxRowsToolStripMenuItem.Checked = true;
				}
				else
				{
					maxRowsToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "AvgRows")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					avgRowsToolStripMenuItem.Checked = true;
				}
				else
				{
					avgRowsToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "DevRows")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					devRowsToolStripMenuItem.Checked = true;
				}
				else
				{
					devRowsToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "VarRows")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					varRowsToolStripMenuItem.Checked = true;
				}
				else
				{
					varRowsToolStripMenuItem.Checked = false;
				}
			}
			else if (column.Key == "SumRows")
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					sumRowsToolStripMenuItem.Checked = true;
				}
				else
				{
					sumRowsToolStripMenuItem.Checked = false;
				}
			}
		}
	}

	private void InitializeDataViewer()
	{
		dataViewer1.UpdateDataSourceEvent += _databaseOperation.UpdateDataSourceStatistics;
		dataViewer1.RequestDataSourceAllPagesEvent += _databaseOperation.RequestDataSourceAllPagesStatistics;
		dataViewer1.DoubleClickEvent += DataViewer1_DoubleClickEvent;
		dataViewer1.RightClickEvent += DataViewer1_RightClickEvent;

		dataViewer1.ColumnWidthChangedEvent += DataViewer1_ColumnWidthChangedEvent;
		dataViewer1.ColumnDisplayIndexChangedEvent += DataViewer1_ColumnDisplayIndexChangedEvent;

		dataViewer1.ShowOutputEvent += DataViewer1_ShowOutputEvent;

		dataViewer1.LoadData(_dataViewerParametersDataViewer1);
	}

	private void DataViewer1_ColumnWidthChangedEvent(string columnName, int width)
	{
		ColumnPropertiesHandler.ChangeWidth(_dataViewerParametersDataViewer1, columnName, width);
		ColumnPropertiesHandler.SaveColumnWidthAndOrder(_dataViewerParametersDataViewer1, "DataViewer_StatisticsData");
	}

	private void DataViewer1_ColumnDisplayIndexChangedEvent(string columnName, int displayIndex)
	{
		Dictionary<string, string[]> newColumns = ColumnPropertiesHandler.ChangeColumnOrder(_dataViewerParametersDataViewer1, columnName, displayIndex);
		_dataViewerParametersDataViewer1.Columns = newColumns;
		dataViewer1.UpdateVisibleColumns(newColumns);
		ColumnPropertiesHandler.SaveColumnWidthAndOrder(_dataViewerParametersDataViewer1, "DataViewer_StatisticsData");
	}

	private string GetWhere()
	{
		StringBuilder columns = new StringBuilder();
		List<string> groupByColumns = new List<string>();

		string groupBy = "";

		if (_groupBys.Count > 0)
		{
			foreach (string column in _groupBys)
			{
				columns.Append(string.Format("t.[{0}], ", column));
				groupByColumns.Add(column);
			}

			groupBy = string.Format("group by {0}", columns.ToString().Substring(0, columns.Length - 2));
		}

		ConfigHandler.GroupByColumns = groupByColumns;
		return groupBy;
	}

	private void SetFilterLabel(bool filter1Applied, bool filter2Applied)
	{
		string filter1;

		if (filter1Applied)
		{
			if (ConfigHandler.UseTranslation)
			{
				filter1 = Translator.GetText("filter1Active");
			}
			else
			{
				filter1 = "Filter 1: Active";
			}
		}
		else
		{
			if (ConfigHandler.UseTranslation)
			{
				filter1 = Translator.GetText("filter1NotActive");
			}
			else
			{
				filter1 = "Filter 1: Not active";
			}
		}

		string filter2;

		if (filter2Applied)
		{
			if (ConfigHandler.UseTranslation)
			{
				filter2 = Translator.GetText("filter2Active");
			}
			else
			{
				filter2 = "Filter 2: Active";
			}
		}
		else
		{
			if (ConfigHandler.UseTranslation)
			{
				filter2 = Translator.GetText("filter2NotActive");
			}
			else
			{
				filter2 = "Filter 2: Not active";
			}
		}

		filterLabel.Text = string.Format("{0}, {1}, {2}", filter1, filter2, GetBasedOnText());

		SetFilterLabelToolTip();
	}

	private void SetFilterLabelToolTip()
	{
		TimeSpan timeSpan = _maxStartTime - _minStartTime;

		string toolTipTextFormat = "Days: {0}\r\nHours: {1}\r\nMinutes: {2}\r\nSeconds: {3}\r\nMilliseconds: {4}";

		if (ConfigHandler.UseTranslation)
		{
			toolTipTextFormat = Translator.GetText("PeriodeToolTip");
		}

		string toolTipText = string.Format(toolTipTextFormat, timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

		if (ConfigHandler.UseTranslation)
		{
			_toolTip.ToolTipTitle = Translator.GetText("PeriodTitle");
		}
		else
		{
			_toolTip.ToolTipTitle = "Period";
		}

		_toolTip.SetToolTip(filterLabel, toolTipText);
		_toolTip.AutomaticDelay = 500;
	}

	private string GetBasedOnText()
	{
		string periodText;

		if (ConfigHandler.UseTranslation)
		{
			periodText = Translator.GetText("Period");
		}
		else
		{
			periodText = "Period:";
		}

		string totalEvents;

		if (ConfigHandler.UseTranslation)
		{
			totalEvents = Translator.GetText("TotalEvents");
		}
		else
		{
			totalEvents = "Based on {0} events";
		}

		string period;

		if (_minStartTime == DateTime.MinValue && _maxStartTime == DateTime.MinValue)
		{
			if (ConfigHandler.UseTranslation)
			{
				period = Translator.GetText("None");
			}
			else
			{
				period = "None";
			}
		}
		else
		{
			period = string.Format("{0} - {1}", GenericHelper.FormatLongDate(_minStartTime), GenericHelper.FormatLongDate(_maxStartTime));
		}

		totalEvents = string.Format(totalEvents, GenericHelper.FormatWithThousandSeparator(_totalRows));

		return string.Format("{0} {1}, {2}", periodText, period, totalEvents);
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void DataViewer1_Enter(object sender, EventArgs e)
	{
		AcceptButton = null;
	}

	private void DataViewer1_Leave(object sender, EventArgs e)
	{
		AcceptButton = okButton;
	}

	private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ExportForm form = new ExportForm();
		form.DisableExportSql();
		form.DisableExportPtt();
		form.DisableExportSession();
		form.Initialize(_databaseOperation, dataViewer1, _dataViewerParametersDataViewer1);
		form.ShowDialog();
	}

	private void ViewStatisticsForm_Resize(object sender, EventArgs e)
	{
		if (Width < MinimumSize.Width || Width == 0)
		{
			return;
		}

		ConfigHandler.ViewStatisticsWindowSize = string.Format("{0}; {1}", Size.Width, Size.Height);
		ConfigHandler.SaveConfig();
	}

	private void SetSize()
	{
		int x = Convert.ToInt32(ConfigHandler.ViewStatisticsWindowSize.Split(';')[0]);
		int y = Convert.ToInt32(ConfigHandler.ViewStatisticsWindowSize.Split(';')[1]);

		if (x > Screen.PrimaryScreen.Bounds.Width || y > Screen.PrimaryScreen.Bounds.Height)
		{
			WindowState = FormWindowState.Maximized;
			return;
		}

		if (x >= MinimumSize.Width && y >= MinimumSize.Height)
		{
			Size = new Size(x, y);
		}
	}

	private void ToggleShowHideColumn(string columnName)
	{
		foreach (KeyValuePair<string, string[]> column in _dataViewerParametersDataViewer1.Columns)
		{
			if (column.Key == columnName)
			{
				if (column.Value[4] == "NonSearchableShow")
				{
					column.Value[4] = "NonSearchableHide";
				}
				else if (column.Value[4] == "NonSearchableHide")
				{
					column.Value[4] = "NonSearchableShow";
				}

				break;
			}
		}

		ColumnPropertiesHandler.SaveColumnWidthAndOrder(_dataViewerParametersDataViewer1, "DataViewer_StatisticsData");
		ReloadDataViewer();
	}

	private void MinDurationToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("MinDuration");
	}

	private void MaxDurationToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("MaxDuration");
	}

	private void AvgDurationToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("AvgDuration");
	}

	private void DevDurationToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("DevDuration");
	}

	private void VarDurationToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("VarDuration");
	}

	private void SumDurationToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("SumDuration");
	}

	private void MinReadsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("MinReads");
	}

	private void MaxReadsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("MaxReads");
	}

	private void AvgReadsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("AvgReads");
	}

	private void DevReadsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("DevReads");
	}

	private void VarReadsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("VarReads");
	}

	private void SumReadsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("SumReads");
	}

	private void MinWritesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("MinWrites");
	}

	private void MaxWritesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("MaxWrites");
	}

	private void AvgWritesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("AvgWrites");
	}

	private void DevWritesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("DevWrites");
	}

	private void VarWritesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("VarWrites");
	}

	private void SumWritesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("SumWrites");
	}

	private void MinCpuToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("MinCPU");
	}

	private void MaxCpuToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("MaxCPU");
	}

	private void AvgCpuToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("AvgCPU");
	}

	private void DevCpuToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("DevCPU");
	}

	private void VarCpuToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("VarCPU");
	}

	private void SumCpuToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("SumCPU");
	}

	private void MinRowsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("MinRows");
	}

	private void MaxRowsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("MaxRows");
	}

	private void AvgRowsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("AvgRows");
	}

	private void DevRowsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("DevRows");
	}

	private void VarRowsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("VarRows");
	}

	private void SumRowsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ToggleShowHideColumn("SumRows");
	}

	private void ReloadDataViewer()
	{
		statisticsGroupBox.Controls.Remove(dataViewer1);

		AnchorStyles anchor = dataViewer1.Anchor;
		Color backColor = dataViewer1.BackColor;
		BorderStyle borderStyle = dataViewer1.BorderStyle;
		Point location = dataViewer1.Location;
		Size minimumSize = dataViewer1.MinimumSize;
		string name = dataViewer1.Name;
		Size size = dataViewer1.Size;
		int tabIndex = dataViewer1.TabIndex;

		dataViewer1 = new DataViewer();

		dataViewer1.Anchor = anchor;
		dataViewer1.BackColor = backColor;
		dataViewer1.BorderStyle = borderStyle;
		dataViewer1.Location = location;
		dataViewer1.MinimumSize = minimumSize;
		dataViewer1.Name = name;
		dataViewer1.Size = size;
		dataViewer1.TabIndex = tabIndex;

		dataViewer1.Enter += DataViewer1_Enter;
		dataViewer1.Leave += DataViewer1_Leave;

		if (ConfigHandler.UseTranslation)
		{
			dataViewer1.SetText(Translator.GetText("ClearButtonText"), Translator.GetText("SearchButtonText"), Translator.GetText("PageText"), Translator.GetText("IsDataModifiedText"), Translator.GetText("IsDataModifiedCaptionText"), Translator.GetText("OutOf"), Translator.GetText("TotalText"), Translator.GetText("SearchBoxToolTipText"), Translator.GetText("NotValidIntegerText"));
		}

		statisticsGroupBox.Controls.Add(dataViewer1);

		string where = GetWhere();
		_dataViewerParametersDataViewer1.WhereActive = where;

		InitializeDataViewer();
		InitializeShowCheckBoxes();
		dataViewer1.SetFocus();
	}

	private static void DataViewer1_ShowOutputEvent(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
	{
		OutputHandler.Show(text, caption, buttons, icon);
	}

	private void DataViewer1_RightClickEvent()
	{
		if (_groupBys.Count == 1)
		{
			contextMenuStrip1.Show(Cursor.Position);
		}
	}

	private void DataViewer1_DoubleClickEvent()
	{
		if (_groupBys.Count == 1)
		{
			ViewItem();
		}
	}

	private void ViewToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (_groupBys.Count == 1)
		{
			ViewItem();
		}
	}

	private void ViewItem()
	{
		ViewRowForm form = new ViewRowForm();
		form.SetValues(GetTextData());
		form.ShowDialog();
	}

	private string GetTextData()
	{
		List<DataRow> selectedRows = dataViewer1.GetSelectedRows();

		string textData = "";

		for (int i = 0; i < selectedRows.Count; i++)
		{
			if (i > 0)
			{
				textData += "go\r\n\r\n";
			}

			textData += string.Format("{0}\r\n", selectedRows[i][_groupBys[0]]);

			if (textData.Length >= 4)
			{
				if (textData.Substring(textData.Length - 4, 4) != "\r\n\r\n")
				{
					textData += "\r\n";
				}
			}
		}

		textData = textData.Substring(0, textData.Length - 2);
		return textData;
	}
}
