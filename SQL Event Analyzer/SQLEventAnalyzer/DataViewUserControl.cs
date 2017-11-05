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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

public partial class DataViewUserControl : UserControl
{
	public delegate void ShowTraceFileSelectorEventHandler();
	public event ShowTraceFileSelectorEventHandler ShowInputSourceEvent;

	public delegate void SetMenuItemCheckBoxEventHandler(string menuItemName, bool value);
	public event SetMenuItemCheckBoxEventHandler SetMenuItemCheckBoxEvent;

	private DatabaseOperation _databaseOperation;
	private SearchTextForm _searchForm;
	private bool _caretPositionChangeFromSearch;
	private List<Filter> _filter1;
	private List<Filter> _filter2;
	private DataViewerParameters _dataViewerParametersDataViewer1;
	private int _totalRows;

	public DataViewUserControl()
	{
		InitializeComponent();
	}

	public void Initialize(DatabaseOperation databaseOperation)
	{
		InitializeDictionary();

		_databaseOperation = databaseOperation;
		_dataViewerParametersDataViewer1 = DalTranslator.GetTraceData();
		ColumnPropertiesHandler.LoadColumnWidthAndOrder(_dataViewerParametersDataViewer1, "DataViewer_TraceData");

		_searchForm = new SearchTextForm();
		_searchForm.Initialize();

		if (ConfigHandler.UseTranslation)
		{
			_searchForm.SetTitle(Translator.GetText("searchInTextData"));
		}
		else
		{
			_searchForm.SetTitle("Search in TextData");
		}

		filter1UserControl.ApplyEvent += Filter1UserControl_ApplyEvent;
		filter1UserControl.ResetEvent += Filter1UserControl_ResetEvent;

		filter2UserControl.ApplyEvent += Filter2UserControl_ApplyEvent;
		filter2UserControl.ResetEvent += Filter2UserControl_ResetEvent;

		textDataTextBox.SetHighlighting("SQL");
		textDataTextBox.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("SQL");

		textDataTextBox.TextEditorProperties.Font = new Font(ConfigHandler.TextDataFontFamily, float.Parse(ConfigHandler.TextDataFontSize));
		textDataTextBox.Font = new Font(ConfigHandler.TextDataFontFamily, float.Parse(ConfigHandler.TextDataFontSize));

		textDataTextBox.ActiveTextAreaControl.Caret.PositionChanged += Caret_PositionChanged;
		_searchForm.SearchEvent += SearchForm_SearchEvent;

		ConfigHandler.ActiveFilters = new List<Filter>();
	}

	public void RefreshData()
	{
		_databaseOperation.SetForceRecalculateTotalRows(true);
		dataViewer1.UpdateGrid(1, 1);

		SetUserInterface();
	}

	public void SetVerboseMode()
	{
		backButton.Visible = false;
		filter2UserControl.SetVerboseMode();
		statisticUserControl1.SetVerboseMode();
	}

	public DataViewerParameters GetDataViewerParameters()
	{
		return _dataViewerParametersDataViewer1;
	}

	public DataViewer GetDataViewer()
	{
		return dataViewer1;
	}

	public void ToggleShowHideColumn(string columnName)
	{
		foreach (KeyValuePair<string, string[]> column in _dataViewerParametersDataViewer1.Columns)
		{
			if (column.Key == columnName)
			{
				if (column.Value[4] == "SearchableShow")
				{
					column.Value[4] = "NonSearchableHide";
				}
				else if (column.Value[4] == "NonSearchableHide")
				{
					column.Value[4] = "SearchableShow";
				}

				break;
			}
		}

		ColumnPropertiesHandler.SaveColumnWidthAndOrder(_dataViewerParametersDataViewer1, "DataViewer_TraceData");
		ReloadDataViewer(false, false, false);
	}

	public void ToggleShowHiddenColumns()
	{
		foreach (Column column in ColumnHelper.EnabledColumns)
		{
			if (column.Hidden)
			{
				if (ConfigHandler.ShowHiddenColumns)
				{
					_dataViewerParametersDataViewer1.Columns[column.Name].SetValue("SearchableShow", 4);
				}
				else
				{
					_dataViewerParametersDataViewer1.Columns[column.Name].SetValue("NonSearchableHide", 4);
				}
			}
			else
			{
				_dataViewerParametersDataViewer1.Columns[column.Name].SetValue("SearchableShow", 4);
			}
		}
	}

	public void ShowTimeLine()
	{
		DataSet graphData = _databaseOperation.GetGraphData(_dataViewerParametersDataViewer1.Columns);

		if (graphData.Tables[0].Rows.Count > 0)
		{
			if (Convert.ToInt32(graphData.Tables[1].Rows[0]["NumberOfRows"].ToString()) > ConfigHandler.MaxGraphRows)
			{
				string text = "Cannot show timeline.\r\nNumber of events exceeds the allowed {0}.\r\n\r\nModify the number of events by using filter 1 and filter 2.";

				if (ConfigHandler.UseTranslation)
				{
					text = Translator.GetText("MaxRows");
				}

				OutputHandler.Show(string.Format(text, ConfigHandler.MaxGraphRows), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else if (Convert.ToDateTime(graphData.Tables[0].Rows[graphData.Tables[0].Rows.Count - 1]["StartTime"]) - Convert.ToDateTime(graphData.Tables[0].Rows[0]["StartTime"]) >= TimeSpan.FromDays(1))
			{
				string text = "Cannot show timeline.\r\nThe time span between of the chosen events can't be greater than 1 day.\r\n\r\nModify the number of events by using filter 1 and filter 2.";

				if (ConfigHandler.UseTranslation)
				{
					text = Translator.GetText("MaxDays");
				}

				OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				DataTable dataTable = _databaseOperation.GetGraphData(_dataViewerParametersDataViewer1.Columns).Tables[0];
				TimelineForm form = new TimelineForm(dataTable);
				form.ShowDialog();
			}
		}
		else
		{
			string text = "No events to display.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("NoRowsToDisplay");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}

	public bool HandleColumns(bool importFromExistingTable)
	{
		ConfigHandler.HandleColumnsStartTime = DateTime.Now;
		ConfigHandler.ErrorNumbersToSkip = new List<int>();

		Visible = false;
		HandleColumnsForm form = new HandleColumnsForm();
		form.Initialize(_databaseOperation, importFromExistingTable);

		if (GenericHelper.IsUserInteractive())
		{
			form.ShowDialog();
		}

		bool success = form.GetSuccess();

		ConfigHandler.ErrorNumbersToSkip = null;
		ConfigHandler.HandleColumnsEndTime = DateTime.Now;

		return success;
	}

	public void SetItemsPerPage()
	{
		dataViewer1.SetItemsPerPage(ConfigHandler.ItemsPerPage);
		dataViewer1.UpdateGrid(1, 1);
	}

	public void UpdateColumnWidthAndOrder()
	{
		dataViewer1.ReInitializeDataGrid();
		dataViewer1.UpdateColumnWidth(DalTranslator.GetTraceData().Columns);
		dataViewer1.UpdateColumnOrder(DalTranslator.GetTraceData().Columns);
	}

	public string GetSortingColumn()
	{
		return dataViewer1.GetSortingColumn();
	}

	public void ReloadDataViewer(bool initializeUserInterface, bool useConnectToSessionTimeAsImportTime, bool resetSortingColumn)
	{
		eventsGroupBox.Controls.Remove(dataViewer1);

		AnchorStyles anchor = dataViewer1.Anchor;
		Color backColor = dataViewer1.BackColor;
		BorderStyle borderStyle = dataViewer1.BorderStyle;
		Point location = dataViewer1.Location;
		Size minimumSize = dataViewer1.MinimumSize;
		string name = dataViewer1.Name;
		Size size = dataViewer1.Size;
		int tabIndex = dataViewer1.TabIndex;

		string sortingColumn = _dataViewerParametersDataViewer1.SortingColumn;
		ListSortDirection sortingColumnDirection = _dataViewerParametersDataViewer1.SortingColumnDirection;

		if (dataViewer1.IsInitialized() && !resetSortingColumn)
		{
			sortingColumn = dataViewer1.GetSortingColumn();
			sortingColumnDirection = dataViewer1.GetSortingColumnDirection();
		}

		dataViewer1 = new DataViewer();

		if (!ConfigHandler.EnableQuickSearch)
		{
			dataViewer1.DisableTopBar();
		}

		dataViewer1.Anchor = anchor;
		dataViewer1.BackColor = backColor;
		dataViewer1.BorderStyle = borderStyle;
		dataViewer1.Location = location;
		dataViewer1.MinimumSize = minimumSize;
		dataViewer1.Name = name;
		dataViewer1.Size = size;
		dataViewer1.TabIndex = tabIndex;

		if (ConfigHandler.UseTranslation)
		{
			dataViewer1.SetText(Translator.GetText("ClearButtonText"), Translator.GetText("SearchButtonText"), Translator.GetText("PageText"), Translator.GetText("IsDataModifiedText"), Translator.GetText("IsDataModifiedCaptionText"), Translator.GetText("OutOf"), Translator.GetText("TotalText"), Translator.GetText("SearchBoxToolTipText"), Translator.GetText("NotValidIntegerText"));
		}

		eventsGroupBox.Controls.Add(dataViewer1);

		InitializeDataViewer(initializeUserInterface, useConnectToSessionTimeAsImportTime, sortingColumn, sortingColumnDirection);
		InitializeShowCheckBoxes();
		ApplyFilters();
		dataViewer1.SetFocus();
	}

	public void CloseSearchForm()
	{
		_searchForm.Hide();
	}

	public void SetSplitter()
	{
		splitContainer1.SplitterDistance = Convert.ToInt32(ConfigHandler.SplitterDistance);
		splitContainer1.Invalidate();
	}

	public void SetFilter1Applied(bool value)
	{
		filter1UserControl.FilterApplied = value;
	}

	public void SetFilter2Applied(bool value)
	{
		filter2UserControl.FilterApplied = value;
	}

	public void RequestDataSourceAllPages()
	{
		dataViewer1.RequestDataSourceAllPages();
	}

	public DataTable GetVisibleDataTable()
	{
		return dataViewer1.GetVisibleDataTable();
	}

	public ErrorFormParams RunPostScriptFile(string postScriptFileName)
	{
		string sql = null;

		if (postScriptFileName != null)
		{
			sql = File.ReadAllText(postScriptFileName);
			OutputHandler.WriteToLog(string.Format("Running Post Script File \"{0}\"", postScriptFileName));
		}

		UnattendedForm form = new UnattendedForm();
		form.Initialize(_databaseOperation, sql);

		if (GenericHelper.IsUserInteractive())
		{
			form.ShowDialog(this);
		}

		ErrorFormParams errorFormParams = form.GetErrorFormParams();
		return errorFormParams;
	}

	public ErrorFormParams RunPostScript(string postScript)
	{
		string sql = null;

		if (postScript != null)
		{
			sql = postScript;
			OutputHandler.WriteToLog("Running Post Script");
		}

		UnattendedForm form = new UnattendedForm();
		form.Initialize(_databaseOperation, sql);

		if (GenericHelper.IsUserInteractive())
		{
			form.ShowDialog(this);
		}

		ErrorFormParams errorFormParams = form.GetErrorFormParams();
		return errorFormParams;
	}

	public bool InititalizeStatisticsGeneration(string statisticsName, string filter1Name, string filter2Name, string tempPath, List<string> sendToWebServiceNames, string tempPathSendToWebService, int nameIterator)
	{
		bool success = true;

		if (!string.IsNullOrEmpty(filter1Name))
		{
			bool filter1NameExists = filter1UserControl.InitializeSavedSearch(filter1Name);

			if (filter1NameExists)
			{
				filter1UserControl.ApplyFilter();
			}
			else
			{
				OutputHandler.WriteToLog(string.Format("The requested Filter1 \"{0}\" for statistic \"{1}\" does not exist", filter1Name, statisticsName));
				success = false;
			}
		}

		if (!string.IsNullOrEmpty(filter2Name))
		{
			bool filter2NameExists = filter2UserControl.InitializeSavedSearch(filter2Name);

			if (filter2NameExists)
			{
				filter2UserControl.ApplyFilter();
			}
			else
			{
				OutputHandler.WriteToLog(string.Format("The requested Filter2 \"{0}\" for statistic \"{1}\" does not exist", filter2Name, statisticsName));
				success = false;
			}
		}

		bool statisticsNameExists = statisticUserControl1.InitializeSavedSearch(statisticsName);

		if (statisticsNameExists)
		{
			if (!Directory.Exists(tempPath))
			{
				Directory.CreateDirectory(tempPath);
			}

			string exportCsvFileName = string.Format(@"{0}\[{1}][{2}][{3}][{4}][{5}][{6}].csv", tempPath, statisticsName, "{0}", "{1}", filter1Name, filter2Name, "{2}");
			statisticUserControl1.ExportStatisticsUnattended(exportCsvFileName);

			string periodBegin = "";

			if (statisticUserControl1.GetMinStartTime() != DateTime.MinValue)
			{
				periodBegin = GenericHelper.FormatFileNameDate(statisticUserControl1.GetMinStartTime());
			}

			string periodEnd = "";

			if (statisticUserControl1.GetMaxStartTime() != DateTime.MinValue)
			{
				periodEnd = GenericHelper.FormatFileNameDate(statisticUserControl1.GetMaxStartTime());
			}

			if (sendToWebServiceNames.Count > 0)
			{
				if (!Directory.Exists(tempPathSendToWebService))
				{
					Directory.CreateDirectory(tempPathSendToWebService);
				}

				for (int i = 0; i < sendToWebServiceNames.Count; i++)
				{
					if (i == nameIterator && sendToWebServiceNames[i] == statisticsName)
					{
						File.Copy(string.Format(exportCsvFileName, periodBegin, periodEnd, _totalRows), string.Format(@"{0}\[{1}][{2}][{3}][{4}][{5}][{6}].csv", tempPathSendToWebService, statisticsName, periodBegin, periodEnd, filter1Name, filter2Name, _totalRows));
					}
				}
			}
		}
		else
		{
			OutputHandler.WriteToLog(string.Format("The requested statistic \"{0}\" does not exist", statisticsName));
			success = false;
		}

		return success;
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (textDataTextBox.ActiveTextAreaControl.TextArea.Focused)
		{
			if ((int)keyData == 131142) // Keys.Control && Keys.F
			{
				Search();
				return true;
			}
		}

		if ((int)keyData == 116) // F5
		{
			RefreshData();
			return true;
		}

		return base.ProcessCmdKey(ref msg, keyData);
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

		SetSplitter();

		splitContainer1.SplitterMoved += SplitContainer1_SplitterMoved;
		Resize += DataViewUserControl_Resize;
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			backButton.Text = Translator.GetText("backButton");
			numberOfEventsLabel.Text = Translator.GetText("numberOfEventsLabel");
			findToolStripButton.Text = Translator.GetText("findToolStripButton");
			fontToolStripButton.Text = Translator.GetText("fontToolStripButton");
			toolStripMenuItemUndo.Text = Translator.GetText("toolStripMenuItemUndo");
			toolStripMenuItemRedo.Text = Translator.GetText("toolStripMenuItemRedo");
			toolStripMenuItemCut.Text = Translator.GetText("toolStripMenuItemCut");
			toolStripMenuItemCopy.Text = Translator.GetText("toolStripMenuItemCopy");
			toolStripMenuItemPaste.Text = Translator.GetText("toolStripMenuItemPaste");
			toolStripMenuItemDelete.Text = Translator.GetText("toolStripMenuItemDelete");
			toolStripMenuItemSelectAll.Text = Translator.GetText("toolStripMenuItemSelectAll1");
			findToolStripMenuItem.Text = Translator.GetText("findToolStripMenuItem");
			dataAndFiltersGroupBox.Text = Translator.GetText("dataAndFiltersGroupBox");
			viewToolStripMenuItem1.Text = Translator.GetText("viewToolStripMenuItem1");
			eventsGroupBox.Text = Translator.GetText("eventsGroupBox");
			statisticsTabPage.Text = Translator.GetText("statisticsTabPage");
		}
	}

	private void ApplyFilters()
	{
		if (filter1UserControl.FilterApplied)
		{
			filter1UserControl.ApplyFilter();
		}

		if (filter2UserControl.FilterApplied)
		{
			filter2UserControl.ApplyFilter();

			if (ConfigHandler.AutoPopulateFilter2)
			{
				filter2UserControl.ReloadFilterValues();
			}
		}
	}

	private void InitializeShowCheckBoxes()
	{
		foreach (KeyValuePair<string, string[]> column in _dataViewerParametersDataViewer1.Columns)
		{
			if (column.Key == "FileName" || column.Key == "Type" || column.Key == "SPID" || column.Key == "Duration" || column.Key == "Reads" || column.Key == "Writes" || column.Key == "CPU" || column.Key == "Rows")
			{
				bool value = false;

				if (column.Value[4] == "SearchableShow")
				{
					value = true;
				}

				FireSetMenuItemCheckBoxEvent(column.Key, value);
			}
		}
	}

	private void InitializeDataViewer(bool inititalizeUserInterface, bool useConnectToSessionTimeAsImportTime, string sortingColumn, ListSortDirection sortingColumnDirection)
	{
		if (inititalizeUserInterface)
		{
			_dataViewerParametersDataViewer1 = DalTranslator.GetTraceData();

			ColumnPropertiesHandler.LoadColumnWidthAndOrder(_dataViewerParametersDataViewer1, "DataViewer_TraceData");
			ToggleShowHiddenColumns();

			if (useConnectToSessionTimeAsImportTime)
			{
				ConfigHandler.ImportStartTime = DateTime.Now;
			}

			SetUserInterface();

			if (useConnectToSessionTimeAsImportTime)
			{
				ConfigHandler.ImportEndTime = DateTime.Now;
			}
		}

		_dataViewerParametersDataViewer1.SortingColumn = sortingColumn;
		_dataViewerParametersDataViewer1.SortingColumnDirection = sortingColumnDirection;

		dataViewer1.UpdateDataSourceEvent += _databaseOperation.UpdateDataSource;
		dataViewer1.RequestDataSourceAllPagesEvent += _databaseOperation.RequestDataSourceAllPages;
		dataViewer1.SelectionChangedEvent += DataViewer1_SelectionChangedEvent;
		dataViewer1.DoubleClickEvent += DataViewer1_DoubleClickEvent;
		dataViewer1.RightClickEvent += DataViewer1_RightClickEvent;

		dataViewer1.ColumnWidthChangedEvent += DataViewer1_ColumnWidthChangedEvent;
		dataViewer1.ColumnDisplayIndexChangedEvent += DataViewer1_ColumnDisplayIndexChangedEvent;

		dataViewer1.ShowOutputEvent += DataViewer1_ShowOutputEvent;

		dataViewer1.SetDateFormatShow(ConfigHandler.DateTimeLongFormat);
		dataViewer1.SetDateFormatSearch(ConfigHandler.DateFormatSearch);
		dataViewer1.SetUseDateSelector(ConfigHandler.UseDateSelectorInQuickSearch);

		dataViewer1.LoadData(_dataViewerParametersDataViewer1);

		Visible = true;
	}

	private void DataViewer1_ColumnWidthChangedEvent(string columnName, int width)
	{
		ColumnPropertiesHandler.ChangeWidth(_dataViewerParametersDataViewer1, columnName, width);
		ColumnPropertiesHandler.SaveColumnWidthAndOrder(_dataViewerParametersDataViewer1, "DataViewer_TraceData");
	}

	private void DataViewer1_ColumnDisplayIndexChangedEvent(string columnName, int displayIndex)
	{
		Dictionary<string, string[]> newColumns = ColumnPropertiesHandler.ChangeColumnOrder(_dataViewerParametersDataViewer1, columnName, displayIndex);
		_dataViewerParametersDataViewer1.Columns = newColumns;
		dataViewer1.UpdateVisibleColumns(newColumns);
		ColumnPropertiesHandler.SaveColumnWidthAndOrder(_dataViewerParametersDataViewer1, "DataViewer_TraceData");
	}

	private void DataViewer1_RightClickEvent()
	{
		contextMenuStrip1.Show(Cursor.Position);
	}

	private void DataViewer1_DoubleClickEvent()
	{
		ViewItem();
	}

	private void SetUserInterface()
	{
		DataSet traceDataInfo = _databaseOperation.GetTraceDataInfo();
		DateTime lastEventStartTime = _databaseOperation.GetLastEventStartTime();

		if (ConfigHandler.UseTranslation)
		{
			numberOfEventsLabel.Text = string.Format("{1} {0}", GenericHelper.FormatWithThousandSeparator(Convert.ToInt32(traceDataInfo.Tables[0].Rows[0]["TotalRows"])), Translator.GetText("numberOfEventsLabel"));
		}
		else
		{
			numberOfEventsLabel.Text = string.Format("Total Events: {0}", GenericHelper.FormatWithThousandSeparator(Convert.ToInt32(traceDataInfo.Tables[0].Rows[0]["TotalRows"])));
		}

		if (dataViewer1.GetSelectedRow() == null || Convert.ToInt32(traceDataInfo.Tables[0].Rows[0]["TotalRows"]) == 0)
		{
			textDataTextBox.Text = "";
		}
		else
		{
			textDataTextBox.Text = dataViewer1.GetSelectedRow()["TextData"].ToString();
		}

		_totalRows = Convert.ToInt32(traceDataInfo.Tables[0].Rows[0]["TotalRows"]);
		statisticUserControl1.SetTotalRows(_totalRows);

		textDataTextBox.ActiveTextAreaControl.Refresh();

		ResetFilters(traceDataInfo, lastEventStartTime);
		InitializeStatistics();
	}

	private void InitializeStatistics()
	{
		statisticUserControl1.Initialize(_databaseOperation, filter1UserControl, filter2UserControl);
	}

	private Font GetTextDataFont()
	{
		return textDataTextBox.TextEditorProperties.Font;
	}

	private void SetTextDataFont(Font font)
	{
		textDataTextBox.TextEditorProperties.Font = font;
		textDataTextBox.Font = font;
		textDataTextBox.ActiveTextAreaControl.TextArea.Refresh();
	}

	private void Search()
	{
		if (_searchForm.IsShown())
		{
			_searchForm.Activate();
			_searchForm.Show();
		}
		else
		{
			if (textDataTextBox.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
			{
				if (!textDataTextBox.ActiveTextAreaControl.SelectionManager.SelectedText.Contains("\r"))
				{
					_searchForm.SetSearchTerm(textDataTextBox.ActiveTextAreaControl.SelectionManager.SelectedText);
				}
			}

			_searchForm.Hide();
			_searchForm.Show(this);
		}
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

			textData += string.Format("{0}\r\n", selectedRows[i]["TextData"]);

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

	private void DataViewer1_SelectionChangedEvent(int numberOfSelectedRows)
	{
		if (numberOfSelectedRows == 0)
		{
			textDataTextBox.Text = "";
		}
		else
		{
			textDataTextBox.Text = GetTextData();
		}

		textDataTextBox.ActiveTextAreaControl.Refresh();
	}

	private void SearchForm_SearchEvent(int foundIndex, string searchTerm)
	{
		_caretPositionChangeFromSearch = true;

		TextLocation startPosition = textDataTextBox.Document.OffsetToPosition(foundIndex);
		TextLocation endPosition = textDataTextBox.Document.OffsetToPosition(foundIndex + searchTerm.Length);

		textDataTextBox.ActiveTextAreaControl.SelectionManager.SetSelection(startPosition, endPosition);
		textDataTextBox.ActiveTextAreaControl.CenterViewOn(textDataTextBox.Document.GetLineNumberForOffset(foundIndex), 0);

		textDataTextBox.ActiveTextAreaControl.Caret.Line = endPosition.Line;
		textDataTextBox.ActiveTextAreaControl.Caret.Column = endPosition.Column;

		_caretPositionChangeFromSearch = false;

		tabControl1.SelectedTab = textDataTabPage;
	}

	private void Caret_PositionChanged(object sender, EventArgs e)
	{
		Caret caret = (Caret)sender;

		if (!_caretPositionChangeFromSearch)
		{
			_searchForm.Reset(caret.Offset);
		}
	}

	private void ResetFilters(DataSet traceDataInfo, DateTime lastEventStartTime)
	{
		if (!filter1UserControl.FilterApplied)
		{
			ResetFilter1(traceDataInfo, lastEventStartTime);
		}

		if (!filter2UserControl.FilterApplied)
		{
			ResetFilter2();
		}
	}

	private void ResetFilter1(DataSet traceDataInfo, DateTime lastEventStartTime)
	{
		filter1UserControl.Initialize(traceDataInfo, lastEventStartTime);

		filter1TabPage.Text = "Filter 1";

		if (_filter1 != null)
		{
			_filter1.Clear();
		}

		if (ConfigHandler.ActiveFilters != null)
		{
			ConfigHandler.ActiveFilters.Clear();
		}

		if (_filter2 != null)
		{
			foreach (Filter filter in _filter2)
			{
				ConfigHandler.ActiveFilters.Add(filter);
			}
		}

		filter1UserControl.FilterApplied = false;
	}

	private void ResetFilter2()
	{
		filter2UserControl.Initialize(_databaseOperation);

		filter2TabPage.Text = "Filter 2";

		if (_filter2 != null)
		{
			_filter2.Clear();
		}

		if (ConfigHandler.ActiveFilters != null)
		{
			ConfigHandler.ActiveFilters.Clear();
		}

		if (_filter1 != null)
		{
			foreach (Filter filter in _filter1)
			{
				ConfigHandler.ActiveFilters.Add(filter);
			}
		}

		filter2UserControl.FilterApplied = false;
	}

	private void FireShowInputSourceEvent()
	{
		if (ShowInputSourceEvent != null)
		{
			ShowInputSourceEvent();
		}
	}

	private void FireSetMenuItemCheckBoxEvent(string menuItemName, bool value)
	{
		if (SetMenuItemCheckBoxEvent != null)
		{
			SetMenuItemCheckBoxEvent(menuItemName, value);
		}
	}

	private void BackButton_Click(object sender, EventArgs e)
	{
		_searchForm.Hide();
		FireShowInputSourceEvent();
	}

	private void InvokeFilter()
	{
		dataViewer1.Reset();
		dataViewer1.SetReadyForInput(false);
		dataViewer1.Search();
		dataViewer1.SetReadyForInput(true);

		DataSet dataSet = dataViewer1.GetDataSource();
		_totalRows = Convert.ToInt32(dataSet.Tables[1].Rows[0]["TotalRows"]);
		statisticUserControl1.SetTotalRows(_totalRows);

		if (dataViewer1.GetNumberOfRows() > 0)
		{
			textDataTextBox.Text = dataViewer1.GetSelectedRow()["TextData"].ToString();
		}
		else
		{
			textDataTextBox.Text = "";
		}

		textDataTextBox.ActiveTextAreaControl.Refresh();
	}

	private void Filter1UserControl_ApplyEvent(List<Filter> filters)
	{
		filter1TabPage.Text = "Filter 1 *";
		_filter1 = filters;

		if (ConfigHandler.ActiveFilters != null)
		{
			ConfigHandler.ActiveFilters.Clear();
		}

		foreach (Filter filter in _filter1)
		{
			ConfigHandler.ActiveFilters.Add(filter);
		}

		if (_filter2 != null)
		{
			foreach (Filter filter in _filter2)
			{
				ConfigHandler.ActiveFilters.Add(filter);
			}
		}

		InvokeFilter();
	}

	private void Filter1UserControl_ResetEvent()
	{
		filter1TabPage.Text = "Filter 1";

		if (_filter1 != null)
		{
			_filter1.Clear();
		}

		if (ConfigHandler.ActiveFilters != null)
		{
			ConfigHandler.ActiveFilters.Clear();
		}

		if (_filter2 != null)
		{
			foreach (Filter filter in _filter2)
			{
				ConfigHandler.ActiveFilters.Add(filter);
			}
		}

		InvokeFilter();
	}

	private void Filter2UserControl_ApplyEvent(List<Filter> filters)
	{
		filter2TabPage.Text = "Filter 2 *";
		_filter2 = filters;

		if (ConfigHandler.ActiveFilters != null)
		{
			ConfigHandler.ActiveFilters.Clear();
		}

		foreach (Filter filter in _filter2)
		{
			ConfigHandler.ActiveFilters.Add(filter);
		}

		if (_filter1 != null)
		{
			foreach (Filter filter in _filter1)
			{
				ConfigHandler.ActiveFilters.Add(filter);
			}
		}

		InvokeFilter();
	}

	private void Filter2UserControl_ResetEvent()
	{
		filter2TabPage.Text = "Filter 2";

		if (_filter2 != null)
		{
			_filter2.Clear();
		}

		if (ConfigHandler.ActiveFilters != null)
		{
			ConfigHandler.ActiveFilters.Clear();
		}

		if (_filter1 != null)
		{
			foreach (Filter filter in _filter1)
			{
				ConfigHandler.ActiveFilters.Add(filter);
			}
		}

		InvokeFilter();
	}

	private void SplitContainer1_Paint(object sender, PaintEventArgs e)
	{
		SplitContainerGrip.PaintGrip(sender, e);
	}

	private void SplitContainer1_MouseUp(object sender, MouseEventArgs e)
	{
		if (splitContainer1.CanFocus)
		{
			ActiveControl = tabControl1;
		}
	}

	private void SplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
	{
		tabControl1.Refresh();

		ConfigHandler.SplitterDistance = splitContainer1.SplitterDistance.ToString();
		ConfigHandler.SaveConfig();
	}

	private void TextDataTextBox_TextChanged(object sender, EventArgs e)
	{
		_searchForm.SetSearchText(textDataTextBox.Text);
		_searchForm.Reset(textDataTextBox.ActiveTextAreaControl.Caret.Offset);
	}

	private void FindToolStripButton_Click(object sender, EventArgs e)
	{
		Search();
	}

	private void FontToolStripButton_Click(object sender, EventArgs e)
	{
		try
		{
			fontDialog1.Font = GetTextDataFont();
			fontDialog1.ShowDialog();

			if (fontDialog1.Font.Bold || fontDialog1.Font.Italic)
			{
				if (ConfigHandler.UseTranslation)
				{
					OutputHandler.Show(Translator.GetText("fontsNotSupported"), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					OutputHandler.Show("Bold and Italic fonts are not supported.", GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

				return;
			}

			SetTextDataFont(fontDialog1.Font);

			string familyName = fontDialog1.Font.FontFamily.Name;
			string size = fontDialog1.Font.Size.ToString();

			ConfigHandler.TextDataFontFamily = familyName;
			ConfigHandler.TextDataFontSize = size;
			ConfigHandler.SaveConfig();
		}
		catch
		{
			if (ConfigHandler.UseTranslation)
			{
				OutputHandler.Show(string.Format(Translator.GetText("newFonts"), GenericHelper.ApplicationName), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				OutputHandler.Show(string.Format("New fonts added to Windows while {0} is running can't be added due to an error in Windows.\r\n\r\nTo add this font, restart {0}.", GenericHelper.ApplicationName), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}

	private void DataViewUserControl_Resize(object sender, EventArgs e)
	{
		if (Width < MinimumSize.Width || Width == 0)
		{
			return;
		}

		splitContainer1.SplitterDistance = Convert.ToInt32(ConfigHandler.SplitterDistance);

		splitContainer1.SplitterDistance++;
		splitContainer1.SplitterDistance--;
		splitContainer1.Invalidate();

		ConfigHandler.WindowSize = string.Format("{0}; {1}", Size.Width, Size.Height);
		ConfigHandler.SaveConfig();
	}

	private void ToolStripMenuItemUndo_Click(object sender, EventArgs e)
	{
		textDataTextBox.Undo();
	}

	private void ToolStripMenuItemRedo_Click(object sender, EventArgs e)
	{
		textDataTextBox.Redo();
	}

	private void ToolStripMenuItemCut_Click(object sender, EventArgs e)
	{
		textDataTextBox.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(sender, e);
	}

	private void ToolStripMenuItemCopy_Click(object sender, EventArgs e)
	{
		textDataTextBox.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(sender, e);
	}

	private void ToolStripMenuItemPaste_Click(object sender, EventArgs e)
	{
		textDataTextBox.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(sender, e);
	}

	private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
	{
		textDataTextBox.ActiveTextAreaControl.TextArea.ClipboardHandler.Delete(sender, e);
	}

	private void ToolStripMenuItemSelectAll_Click(object sender, EventArgs e)
	{
		SelectAll();
	}

	private void SelectAll()
	{
		TextLocation startPosition = new TextLocation(0, 0);

		int textLength = textDataTextBox.ActiveTextAreaControl.Document.TextLength;
		TextLocation endPosition = new TextLocation();
		endPosition.Column = textDataTextBox.Document.OffsetToPosition(textLength).Column;
		endPosition.Line = textDataTextBox.Document.OffsetToPosition(textLength).Line;

		textDataTextBox.ActiveTextAreaControl.SelectionManager.SetSelection(startPosition, endPosition);
		textDataTextBox.ActiveTextAreaControl.Caret.Position = endPosition;
	}

	private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
	{
		if (textDataTextBox.Document.UndoStack.CanUndo)
		{
			toolStripMenuItemUndo.Enabled = true;
		}
		else
		{
			toolStripMenuItemUndo.Enabled = false;
		}

		if (textDataTextBox.Document.UndoStack.CanRedo)
		{
			toolStripMenuItemRedo.Enabled = true;
		}
		else
		{
			toolStripMenuItemRedo.Enabled = false;
		}
	}

	private void FindToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Search();
	}

	private void ViewToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		ViewItem();
	}

	private void ViewItem()
	{
		ViewRowForm form = new ViewRowForm();
		form.SetValues(GetTextData());
		form.ShowDialog();
	}

	private static void DataViewer1_ShowOutputEvent(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
	{
		OutputHandler.Show(text, caption, buttons, icon);
	}
}
