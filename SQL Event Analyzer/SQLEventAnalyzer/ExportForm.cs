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
using System.Data;
using System.Windows.Forms;

public partial class ExportForm : Form
{
	private DatabaseOperation _databaseOperation;
	private DataViewer _dataViewer;
	private string _fileName;
	private DataViewerParameters _dataViewerParameters;
	private bool _unattended;
	private DateTime _minStartTime;
	private DateTime _maxStartTime;
	private int _totalRows;

	public ExportForm()
	{
		InitializeComponent();
	}

	public void Initialize(DatabaseOperation databaseOperation, DataViewer dataViewer, DataViewerParameters dataViewerParameters)
	{
		InitializeDictionary();

		_dataViewer = dataViewer;
		_databaseOperation = databaseOperation;
		_dataViewerParameters = dataViewerParameters;
	}

	public DateTime GetMinStartTime()
	{
		return _minStartTime;
	}

	public DateTime GetMaxStartTime()
	{
		return _maxStartTime;
	}

	public void DisableExportSql()
	{
		sqlRadioButton.Enabled = false;
	}

	public void DisableExportPtt()
	{
		pttRadioButton.Enabled = false;
		databaseNameLabel.Enabled = false;
	}

	public void DisableExportSession()
	{
		sessionRadioButton.Enabled = false;
		sessionNameLabel.Enabled = false;
	}

	public void DoExportCsvAllPagesUnattended(string fileName, int totalRows)
	{
		_fileName = fileName;
		_totalRows = totalRows;
		_unattended = true;

		_databaseOperation.DataSourceAllPagesReadyEvent += DatabaseOperation_DataSourceAllPagesReadyEventUnattended;
		_dataViewer.RequestDataSourceAllPages();
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			okButton.Text = Translator.GetText("okButton");
			cancelButton.Text = Translator.GetText("cancelButton");
			Text = Translator.GetText("Export");
			optionsGroupBox.Text = Translator.GetText("Options");
			formatGroupBox.Text = Translator.GetText("Format");
			allPagesRadioButton.Text = Translator.GetText("allPagesRadioButton");
			currentPageRadioButton.Text = Translator.GetText("currentPageRadioButton");
			databaseNameLabel.Text = Translator.GetText("databaseNameLabel");
			sessionNameLabel.Text = Translator.GetText("sessionNameLabel");
			sessionRadioButton.Text = Translator.GetText("Session");
		}
	}

	private void DatabaseOperation_DataSourceAllPagesReadyEvent(DataSet dataSet)
	{
		_databaseOperation.DataSourceAllPagesReadyEvent -= DatabaseOperation_DataSourceAllPagesReadyEvent;
		Export(dataSet.Tables[0]);
	}

	private void DatabaseOperation_DataSourceAllPagesReadyEventUnattended(DataSet dataSet)
	{
		if (_unattended)
		{
			DataTable statisticsInfoDataTable = dataSet.Tables[1];

			string periodBegin = "";

			if (statisticsInfoDataTable.Rows[0]["MinStartTime"] != DBNull.Value)
			{
				DateTime minStartTime = Convert.ToDateTime(statisticsInfoDataTable.Rows[0]["MinStartTime"]);
				_minStartTime = minStartTime;

				string formattedMinStartTime = GenericHelper.FormatFileNameDate(minStartTime);

				periodBegin = formattedMinStartTime;
			}

			string periodEnd = "";

			if (statisticsInfoDataTable.Rows[0]["MaxStartTime"] != DBNull.Value)
			{
				DateTime maxStartTime = Convert.ToDateTime(statisticsInfoDataTable.Rows[0]["MaxStartTime"]);
				_maxStartTime = maxStartTime;

				string formattedMaxStartTime = GenericHelper.FormatFileNameDate(maxStartTime);

				periodEnd = formattedMaxStartTime;
			}

			_fileName = string.Format(_fileName, periodBegin, periodEnd, _totalRows);
		}

		_databaseOperation.DataSourceAllPagesReadyEvent -= DatabaseOperation_DataSourceAllPagesReadyEventUnattended;
		ExportToCsv.DoExport(dataSet.Tables[0], _fileName, _dataViewerParameters);
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		if (pttRadioButton.Checked && databaseNameTextBox.Text.Trim() == "")
		{
			string text = "Database name is missing.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("DatabaseNameMissing");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			databaseNameTextBox.Focus();
			return;
		}

		if (sessionRadioButton.Checked && sessionNameTextBox.Text.Trim() == "")
		{
			string text = "Session name is missing.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("SessionNameMissing");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			sessionNameTextBox.Focus();
			return;
		}

		if (sessionRadioButton.Checked)
		{
			ExportSessionToSession();
		}
		else
		{
			SaveFileDialog exportFileDialog = new SaveFileDialog();

			if (csvRadioButton.Checked)
			{
				if (ConfigHandler.UseTranslation)
				{
					exportFileDialog.Filter = Translator.GetText("exportFileDialogFilterCsv");
				}
				else
				{
					exportFileDialog.Filter = "Csv files (*.csv)|*.csv|All files (*.*)|*.*";
				}
			}
			else if (sqlRadioButton.Checked)
			{
				if (ConfigHandler.UseTranslation)
				{
					exportFileDialog.Filter = Translator.GetText("exportFileDialogFilterSql");
				}
				else
				{
					exportFileDialog.Filter = "Sql files (*.sql)|*.sql|All files (*.*)|*.*";
				}
			}
			else if (pttRadioButton.Checked)
			{
				if (ConfigHandler.UseTranslation)
				{
					exportFileDialog.Filter = Translator.GetText("exportFileDialogFilterPtt");
				}
				else
				{
					exportFileDialog.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
				}
			}

			DialogResult result = exportFileDialog.ShowDialog();
			Application.DoEvents();

			if (result.ToString() == "OK")
			{
				_fileName = exportFileDialog.FileName;

				if (allPagesRadioButton.Checked)
				{
					_databaseOperation.DataSourceAllPagesReadyEvent += DatabaseOperation_DataSourceAllPagesReadyEvent;
					_dataViewer.RequestDataSourceAllPages();
				}
				else
				{
					Export(_dataViewer.GetVisibleDataTable());
				}

				Close();
			}
		}
	}

	private void Export(DataTable dataTable)
	{
		if (csvRadioButton.Checked)
		{
			ExportToCsv.Export(dataTable, _fileName, _dataViewerParameters);
		}
		else if (sqlRadioButton.Checked)
		{
			ExportToSql.Export(dataTable, _fileName, _dataViewerParameters);
		}
		else
		{
			ExportToPtt.Export(dataTable, databaseNameTextBox.Text.Trim(), _fileName);
		}
	}

	private void PttRadioButton_CheckedChanged(object sender, EventArgs e)
	{
		databaseNameTextBox.Enabled = pttRadioButton.Checked;
		databaseNameLabel.Enabled = pttRadioButton.Checked;

		if (pttRadioButton.Checked)
		{
			databaseNameTextBox.Focus();
		}
	}

	private void SessionRadioButton_CheckedChanged(object sender, EventArgs e)
	{
		sessionNameTextBox.Enabled = sessionRadioButton.Checked;
		sessionNameLabel.Enabled = sessionRadioButton.Checked;

		if (sessionRadioButton.Checked)
		{
			sessionNameTextBox.Focus();
		}
	}

	private bool ValidateSessionName(string name)
	{
		bool unique = CheckForUniqueName(name);

		if (!unique)
		{
			string text = "Names must be unique.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("NameMustBeUnique");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			sessionNameTextBox.Focus();
		}

		return unique;
	}

	private bool CheckForUniqueName(string name)
	{
		bool exists = _databaseOperation.DoesTempTableExist(name);

		if (exists)
		{
			return false;
		}

		return true;
	}

	private void ExportSessionToSession()
	{
		string newSessionName = sessionNameTextBox.Text.Trim();
		bool validName = ValidateSessionName(newSessionName);

		if (validName)
		{
			_databaseOperation.ExportSessionToSessionReadyEvent += DatabaseOperation_ExportSessionToSessionReadyEvent;

			int page = _dataViewer.GetPage();

			if (allPagesRadioButton.Checked)
			{
				page = -1;
			}

			_databaseOperation.ExportSessionToSession(newSessionName, _dataViewer, "ExportSessionToSession.sql", page, ConfigHandler.ItemsPerPage, _dataViewer.GetSortingColumn(), _dataViewer.GetSortingColumnDirection(), _dataViewer.GetSearchTerm(), _dataViewer.GetSearchColumn(), DalTranslator.GetTraceData().Columns, _dataViewer.GetWhere(), _dataViewer.GetWhereActive());
		}
	}

	private void DatabaseOperation_ExportSessionToSessionReadyEvent(bool success)
	{
		_databaseOperation.ExportSessionToSessionReadyEvent -= DatabaseOperation_ExportSessionToSessionReadyEvent;

		if (success)
		{
			string text = "Export successful.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("ExportSuccessful");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		else
		{
			string text = "Export failed.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("ExportFailed");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		Close();
	}
}
