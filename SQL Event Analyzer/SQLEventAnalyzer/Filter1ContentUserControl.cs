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
using System.Xml;

public partial class Filter1ContentUserControl : UserControl
{
	public delegate void EnterKeyEventHandler();
	public event EnterKeyEventHandler EnterKeyEvent;

	private int _startId = -2;
	private int _endId = -2;
	private int _startSpid = -2;
	private int _endSpid = -2;
	private int _startDuration = -2;
	private int _endDuration = -2;
	private string _startStartTime = "";
	private string _endStartTime = "";
	private int _startReads = -2;
	private int _endReads = -2;
	private int _startWrites = -2;
	private int _endWrites = -2;
	private int _startCpu = -2;
	private int _endCpu = -2;
	private int _startRows = -2;
	private int _endRows = -2;

	private DataSet _traceDataInfo;
	private DateTime _lastEventStartTime;

	public Filter1ContentUserControl()
	{
		InitializeComponent();

		endDateTimePicker.CustomFormat = ConfigHandler.DateTimeFormat;
		startDateTimePicker.CustomFormat = ConfigHandler.DateTimeFormat;
	}

	public void Initialize(DataSet traceDataInfo, DateTime lastEventStartTime)
	{
		InitializeDictionary();

		_traceDataInfo = traceDataInfo;
		_lastEventStartTime = lastEventStartTime;

		if (ConfigHandler.EnableFileNameAndType)
		{
			InitializeTypeComboBox();
			InitializeFileNameComboBox();

			enableFileNameCheckBox.Enabled = true;
			enableTypeCheckBox.Enabled = true;
		}
		else
		{
			typeComboBox.Enabled = false;
			fileNameComboBox.Enabled = false;
			enableFileNameCheckBox.Enabled = false;
			enableTypeCheckBox.Enabled = false;
		}

		FillDefaultTraceDataValues();
	}

	public string GetSavedSearchesXml()
	{
		string fileName = "";
		string type = "";

		if (ConfigHandler.EnableFileNameAndType)
		{
			if (fileNameComboBox.SelectedIndex > 0)
			{
				fileName = fileNameComboBox.SelectedItem.ToString();
			}

			if (typeComboBox.SelectedIndex > 0)
			{
				type = typeComboBox.SelectedItem.ToString();
			}
		}

		string startId = "";
		string endId = "";
		string startSpid = "";
		string endSpid = "";
		string startDuration = "";
		string endDuration = "";
		string startStartTime = "";
		string endStartTime = "";
		string startReads = "";
		string endReads = "";
		string startWrites = "";
		string endWrites = "";
		string startCpu = "";
		string endCpu = "";
		string startRows = "";
		string endRows = "";

		if (_startId != -2)
		{
			startId = _startId.ToString();
		}

		if (_endId != -2)
		{
			endId = _endId.ToString();
		}

		if (_startSpid != -2)
		{
			startSpid = _startSpid.ToString();
		}

		if (_endSpid != -2)
		{
			endSpid = _endSpid.ToString();
		}

		if (_startDuration != -2)
		{
			startDuration = _startDuration.ToString();
		}

		if (_endDuration != -2)
		{
			endDuration = _endDuration.ToString();
		}

		if (_startStartTime != "")
		{
			startStartTime = _startStartTime;
		}

		if (_endStartTime != "")
		{
			endStartTime = _endStartTime;
		}

		if (_startReads != -2)
		{
			startReads = _startReads.ToString();
		}

		if (_endReads != -2)
		{
			endReads = _endReads.ToString();
		}

		if (_startWrites != -2)
		{
			startWrites = _startWrites.ToString();
		}

		if (_endWrites != -2)
		{
			endWrites = _endWrites.ToString();
		}

		if (_startCpu != -2)
		{
			startCpu = _startCpu.ToString();
		}

		if (_endCpu != -2)
		{
			endCpu = _endCpu.ToString();
		}

		if (_startRows != -2)
		{
			startRows = _startRows.ToString();
		}

		if (_endRows != -2)
		{
			endRows = _endRows.ToString();
		}

		StringBuilder sb = new StringBuilder();

		sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
		sb.Append(string.Format("<values startId=\"{0}\" endId=\"{1}\" enableId=\"{2}\" startDuration=\"{3}\" endDuration=\"{4}\" enableDuration=\"{5}\" startDateTime=\"{6}\" endDateTime=\"{7}\" enableDateTime=\"{8}\" startReads=\"{9}\" endReads=\"{10}\" enableReads=\"{11}\" startWrites=\"{12}\" endWrites=\"{13}\" enableWrites=\"{14}\" startCpu=\"{15}\" endCpu=\"{16}\" enableCpu=\"{17}\" startRows=\"{18}\" endRows=\"{19}\" enableRows=\"{20}\" startSpid=\"{21}\" endSpid=\"{22}\" enableSpid=\"{23}\" fileName=\"{24}\" enableFileName=\"{25}\" type=\"{26}\" enableType=\"{27}\" dynamicDateTimeStart=\"{28}\" dynamicDateTimeEnd=\"{29}\" />", startId, endId, enableIdCheckBox.Checked, startDuration, endDuration, enableDurationCheckBox.Checked, startStartTime, endStartTime, enableStartTimeCheckBox.Checked, startReads, endReads, enableReadsCheckBox.Checked, startWrites, endWrites, enableWritesCheckBox.Checked, startCpu, endCpu, enableCpuCheckBox.Checked, startRows, endRows, enableRowsCheckBox.Checked, startSpid, endSpid, enableSpidCheckBox.Checked, System.Security.SecurityElement.Escape(fileName), enableFileNameCheckBox.Checked, System.Security.SecurityElement.Escape(type), enableTypeCheckBox.Checked, dynamicStartTimeStartCheckBox.Checked, dynamicStartTimeEndCheckBox.Checked));

		return sb.ToString();
	}

	public void LoadSavedSearch(string xml)
	{
		if (xml == "")
		{
			return;
		}

		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(xml);

		startIdTextBox.Text = xmlDocument.SelectSingleNode("values/@startId").Value;
		endIdTextBox.Text = xmlDocument.SelectSingleNode("values/@endId").Value;
		enableIdCheckBox.Checked = Convert.ToBoolean(xmlDocument.SelectSingleNode("values/@enableId").Value);

		startDurationTextBox.Text = xmlDocument.SelectSingleNode("values/@startDuration").Value;
		endDurationTextBox.Text = xmlDocument.SelectSingleNode("values/@endDuration").Value;
		enableDurationCheckBox.Checked = Convert.ToBoolean(xmlDocument.SelectSingleNode("values/@enableDuration").Value);

		bool dynamicDateTimeStart = Convert.ToBoolean(xmlDocument.SelectSingleNode("values/@dynamicDateTimeStart").Value);
		bool dynamicDateTimeEnd = Convert.ToBoolean(xmlDocument.SelectSingleNode("values/@dynamicDateTimeEnd").Value);

		dynamicStartTimeStartCheckBox.Checked = dynamicDateTimeStart;
		dynamicStartTimeEndCheckBox.Checked = dynamicDateTimeEnd;

		if (dynamicDateTimeStart)
		{
			startDateTimeTextBox.Text = xmlDocument.SelectSingleNode("values/@startDateTime").Value;
		}
		else
		{
			if (xmlDocument.SelectSingleNode("values/@startDateTime").Value != "")
			{
				startDateTimePicker.Value = Convert.ToDateTime(xmlDocument.SelectSingleNode("values/@startDateTime").Value);
			}
		}

		if (dynamicDateTimeEnd)
		{
			endDateTimeTextBox.Text = xmlDocument.SelectSingleNode("values/@endDateTime").Value;
		}
		else
		{
			if (xmlDocument.SelectSingleNode("values/@endDateTime").Value != "")
			{
				endDateTimePicker.Value = Convert.ToDateTime(xmlDocument.SelectSingleNode("values/@endDateTime").Value);
			}
		}

		enableStartTimeCheckBox.Checked = Convert.ToBoolean(xmlDocument.SelectSingleNode("values/@enableDateTime").Value);


		startReadsTextBox.Text = xmlDocument.SelectSingleNode("values/@startReads").Value;
		endReadsTextBox.Text = xmlDocument.SelectSingleNode("values/@endReads").Value;
		enableReadsCheckBox.Checked = Convert.ToBoolean(xmlDocument.SelectSingleNode("values/@enableReads").Value);

		startWritesTextBox.Text = xmlDocument.SelectSingleNode("values/@startWrites").Value;
		endWritesTextBox.Text = xmlDocument.SelectSingleNode("values/@endWrites").Value;
		enableWritesCheckBox.Checked = Convert.ToBoolean(xmlDocument.SelectSingleNode("values/@enableWrites").Value);

		startCpuTextBox.Text = xmlDocument.SelectSingleNode("values/@startCpu").Value;
		endCpuTextBox.Text = xmlDocument.SelectSingleNode("values/@endCpu").Value;
		enableCpuCheckBox.Checked = Convert.ToBoolean(xmlDocument.SelectSingleNode("values/@enableCpu").Value);

		startRowsTextBox.Text = xmlDocument.SelectSingleNode("values/@startRows").Value;
		endRowsTextBox.Text = xmlDocument.SelectSingleNode("values/@endRows").Value;
		enableRowsCheckBox.Checked = Convert.ToBoolean(xmlDocument.SelectSingleNode("values/@enableRows").Value);

		startSpidTextBox.Text = xmlDocument.SelectSingleNode("values/@startSpid").Value;
		endSpidTextBox.Text = xmlDocument.SelectSingleNode("values/@endSpid").Value;
		enableSpidCheckBox.Checked = Convert.ToBoolean(xmlDocument.SelectSingleNode("values/@enableSpid").Value);

		if (ConfigHandler.EnableFileNameAndType)
		{
			string fileName = xmlDocument.SelectSingleNode("values/@fileName").Value;

			if (fileName != null)
			{
				if (fileName == "")
				{
					fileNameComboBox.SelectedIndex = 0;
				}
				else
				{
					foreach (ComboBoxItem comboBoxItem in fileNameComboBox.Items)
					{
						if (comboBoxItem.ToString() == fileName)
						{
							fileNameComboBox.SelectedItem = comboBoxItem;
							break;
						}
					}
				}
			}

			enableFileNameCheckBox.Checked = Convert.ToBoolean(xmlDocument.SelectSingleNode("values/@enableFileName").Value);

			string type = xmlDocument.SelectSingleNode("values/@type").Value;

			if (type != null)
			{
				if (type == "")
				{
					typeComboBox.SelectedIndex = 0;
				}
				else
				{
					foreach (ComboBoxItem comboBoxItem in typeComboBox.Items)
					{
						if (comboBoxItem.ToString() == type)
						{
							typeComboBox.SelectedItem = comboBoxItem;
							break;
						}
					}
				}
			}

			enableTypeCheckBox.Checked = Convert.ToBoolean(xmlDocument.SelectSingleNode("values/@enableType").Value);
		}
	}

	public List<Filter> GetFilters()
	{
		List<Filter> dataViewFilter = new List<Filter>();

		if (enableIdCheckBox.Checked && _startId.ToString() != _traceDataInfo.Tables[0].Rows[0]["MinID"].ToString())
		{
			dataViewFilter.Add(GetFilter("ID", _startId.ToString(), ">="));
		}

		if (enableIdCheckBox.Checked && _endId.ToString() != _traceDataInfo.Tables[0].Rows[0]["MaxID"].ToString())
		{
			dataViewFilter.Add(GetFilter("ID", _endId.ToString(), "<="));
		}

		if (enableDurationCheckBox.Checked && _startDuration.ToString() != _traceDataInfo.Tables[0].Rows[0]["MinDuration"].ToString())
		{
			dataViewFilter.Add(GetFilter("Duration", _startDuration.ToString(), ">="));
		}

		if (enableDurationCheckBox.Checked && _endDuration.ToString() != _traceDataInfo.Tables[0].Rows[0]["MaxDuration"].ToString())
		{
			dataViewFilter.Add(GetFilter("Duration", _endDuration.ToString(), "<="));
		}

		if (enableStartTimeCheckBox.Checked)
		{
			if (dynamicStartTimeStartCheckBox.Checked)
			{
				bool addStartTimeFilter = true;

				if (_lastEventStartTime == DateTime.MinValue && _startStartTime.Trim().StartsWith("led"))
				{
					addStartTimeFilter = false;
				}

				if (addStartTimeFilter)
				{
					string dynamicText = GenericHelper.GetDynamicDateTimeText(_startStartTime, _lastEventStartTime);
					string dynamicValue = GenericHelper.GetDynamicDateTimeValue(_startStartTime);

					dataViewFilter.Add(GetFilter("StartTime", string.Format("{0} {1}", dynamicText, dynamicValue), ">="));
				}
			}
			else
			{
				if (enableStartTimeCheckBox.Checked && _traceDataInfo.Tables[0].Rows[0]["MinStartTime"].ToString() != "" && startDateTimePicker.Value != RoundDateTimeDown(Convert.ToDateTime(_traceDataInfo.Tables[0].Rows[0]["MinStartTime"])))
				{
					dataViewFilter.Add(GetFilter("StartTime", startDateTimePicker.Value.ToString(), ">="));
				}
			}

			if (dynamicStartTimeEndCheckBox.Checked)
			{
				bool addStartTimeFilter = true;

				if (_lastEventStartTime == DateTime.MinValue && _endStartTime.Trim().StartsWith("led"))
				{
					addStartTimeFilter = false;
				}

				if (addStartTimeFilter)
				{
					string dynamicText = GenericHelper.GetDynamicDateTimeText(_endStartTime, _lastEventStartTime);
					string dynamicValue = GenericHelper.GetDynamicDateTimeValue(_endStartTime);

					dataViewFilter.Add(GetFilter("StartTime", string.Format("{0} {1}", dynamicText, dynamicValue), "<="));
				}
			}
			else
			{
				if (enableStartTimeCheckBox.Checked && _traceDataInfo.Tables[0].Rows[0]["MaxStartTime"].ToString() != "" && endDateTimePicker.Value != RoundDateTimeUp(Convert.ToDateTime(_traceDataInfo.Tables[0].Rows[0]["MaxStartTime"])))
				{
					dataViewFilter.Add(GetFilter("StartTime", endDateTimePicker.Value.ToString(), "<="));
				}
			}
		}

		if (enableReadsCheckBox.Checked && _startReads.ToString() != _traceDataInfo.Tables[0].Rows[0]["MinReads"].ToString())
		{
			dataViewFilter.Add(GetFilter("Reads", _startReads.ToString(), ">="));
		}

		if (enableReadsCheckBox.Checked && _endReads.ToString() != _traceDataInfo.Tables[0].Rows[0]["MaxReads"].ToString())
		{
			dataViewFilter.Add(GetFilter("Reads", _endReads.ToString(), "<="));
		}

		if (enableWritesCheckBox.Checked && _startWrites.ToString() != _traceDataInfo.Tables[0].Rows[0]["MinWrites"].ToString())
		{
			dataViewFilter.Add(GetFilter("Writes", _startWrites.ToString(), ">="));
		}

		if (enableWritesCheckBox.Checked && _endWrites.ToString() != _traceDataInfo.Tables[0].Rows[0]["MaxWrites"].ToString())
		{
			dataViewFilter.Add(GetFilter("Writes", _endWrites.ToString(), "<="));
		}

		if (enableCpuCheckBox.Checked && _startCpu.ToString() != _traceDataInfo.Tables[0].Rows[0]["MinCPU"].ToString())
		{
			dataViewFilter.Add(GetFilter("CPU", _startCpu.ToString(), ">="));
		}

		if (enableCpuCheckBox.Checked && _endCpu.ToString() != _traceDataInfo.Tables[0].Rows[0]["MaxCPU"].ToString())
		{
			dataViewFilter.Add(GetFilter("CPU", _endCpu.ToString(), "<="));
		}

		if (enableRowsCheckBox.Checked && _startRows.ToString() != _traceDataInfo.Tables[0].Rows[0]["MinRows"].ToString())
		{
			dataViewFilter.Add(GetFilter("Rows", _startRows.ToString(), ">="));
		}

		if (enableRowsCheckBox.Checked && _endRows.ToString() != _traceDataInfo.Tables[0].Rows[0]["MaxRows"].ToString())
		{
			dataViewFilter.Add(GetFilter("Rows", _endRows.ToString(), "<="));
		}

		if (enableSpidCheckBox.Checked && _startSpid.ToString() != _traceDataInfo.Tables[0].Rows[0]["MinSPID"].ToString())
		{
			dataViewFilter.Add(GetFilter("SPID", _startSpid.ToString(), ">="));
		}

		if (enableSpidCheckBox.Checked && _endSpid.ToString() != _traceDataInfo.Tables[0].Rows[0]["MaxSPID"].ToString())
		{
			dataViewFilter.Add(GetFilter("SPID", _endSpid.ToString(), "<="));
		}

		if (ConfigHandler.EnableFileNameAndType)
		{
			if (enableTypeCheckBox.Checked && typeComboBox.SelectedIndex > 0)
			{
				Filter filter = new Filter();
				filter.AndOr = "and";
				filter.Column = "Type";
				filter.Operator = "=";
				filter.Value = typeComboBox.SelectedItem.ToString();
				filter.ParanthesBegin = "";
				filter.ParanthesEnd = "";
				filter.Filter1Search = true;

				dataViewFilter.Add(filter);
			}

			if (enableFileNameCheckBox.Checked && fileNameComboBox.SelectedIndex > 0)
			{
				Filter filter = new Filter();
				filter.AndOr = "and";
				filter.Column = "FileName";
				filter.Operator = "=";
				filter.Value = fileNameComboBox.SelectedItem.ToString();
				filter.ParanthesBegin = "";
				filter.ParanthesEnd = "";
				filter.Filter1Search = true;

				dataViewFilter.Add(filter);
			}
		}

		return dataViewFilter;
	}

	public bool IsDataValid()
	{
		string name;
		bool valid;

		if (enableIdCheckBox.Checked)
		{
			name = "First Id";

			if (ConfigHandler.UseTranslation)
			{
				name = Translator.GetText("firstId");
			}

			valid = GenericHelper.CheckIntegerValue(false, startIdTextBox.Text, name, false);

			if (!valid)
			{
				startIdTextBox.Focus();
				return false;
			}
			else
			{
				_startId = Convert.ToInt32(startIdTextBox.Text);
			}

			name = "Second Id";

			if (ConfigHandler.UseTranslation)
			{
				name = Translator.GetText("secondId");
			}

			valid = GenericHelper.CheckIntegerValue(false, endIdTextBox.Text, name, false);

			if (!valid)
			{
				endIdTextBox.Focus();
				return false;
			}
			else
			{
				_endId = Convert.ToInt32(endIdTextBox.Text);
			}
		}

		if (enableDurationCheckBox.Checked)
		{
			name = "First Duration";

			if (ConfigHandler.UseTranslation)
			{
				name = Translator.GetText("firstDuration");
			}

			valid = GenericHelper.CheckIntegerValue(true, startDurationTextBox.Text, name, false);

			if (!valid)
			{
				startDurationTextBox.Focus();
				return false;
			}
			else
			{
				_startDuration = Convert.ToInt32(startDurationTextBox.Text);
			}

			name = "Second Duration";

			if (ConfigHandler.UseTranslation)
			{
				name = Translator.GetText("secondDuration");
			}

			valid = GenericHelper.CheckIntegerValue(true, endDurationTextBox.Text, name, false);

			if (!valid)
			{
				endDurationTextBox.Focus();
				return false;
			}
			else
			{
				_endDuration = Convert.ToInt32(endDurationTextBox.Text);
			}
		}

		if (enableStartTimeCheckBox.Checked)
		{
			if (dynamicStartTimeStartCheckBox.Checked)
			{
				name = "First StartTime";

				if (ConfigHandler.UseTranslation)
				{
					name = Translator.GetText("firstStartTime");
				}

				valid = GenericHelper.CheckDynamicDateTimeValue(startDateTimeTextBox.Text, name);

				if (!valid)
				{
					startDateTimeTextBox.Focus();
					return false;
				}
				else
				{
					_startStartTime = startDateTimeTextBox.Text;
				}
			}
			else
			{
				_startStartTime = startDateTimePicker.Value.ToString();
			}

			if (dynamicStartTimeEndCheckBox.Checked)
			{
				name = "Second StartTime";

				if (ConfigHandler.UseTranslation)
				{
					name = Translator.GetText("secondStartTime");
				}

				valid = GenericHelper.CheckDynamicDateTimeValue(endDateTimeTextBox.Text, name);

				if (!valid)
				{
					endDateTimeTextBox.Focus();
					return false;
				}
				else
				{
					_endStartTime = endDateTimeTextBox.Text;
				}
			}
			else
			{
				_endStartTime = endDateTimePicker.Value.ToString();
			}
		}

		if (enableReadsCheckBox.Checked)
		{
			name = "First Reads";

			if (ConfigHandler.UseTranslation)
			{
				name = Translator.GetText("firstReads");
			}

			valid = GenericHelper.CheckIntegerValue(true, startReadsTextBox.Text, name, true);

			if (!valid)
			{
				startReadsTextBox.Focus();
				return false;
			}
			else
			{
				_startReads = Convert.ToInt32(startReadsTextBox.Text);
			}

			name = "Second Reads";

			if (ConfigHandler.UseTranslation)
			{
				name = Translator.GetText("secondReads");
			}

			valid = GenericHelper.CheckIntegerValue(true, endReadsTextBox.Text, name, true);

			if (!valid)
			{
				endReadsTextBox.Focus();
				return false;
			}
			else
			{
				_endReads = Convert.ToInt32(endReadsTextBox.Text);
			}
		}

		if (enableWritesCheckBox.Checked)
		{
			name = "First Writes";

			if (ConfigHandler.UseTranslation)
			{
				name = Translator.GetText("firstWrites");
			}

			valid = GenericHelper.CheckIntegerValue(true, startWritesTextBox.Text, name, true);

			if (!valid)
			{
				startWritesTextBox.Focus();
				return false;
			}
			else
			{
				_startWrites = Convert.ToInt32(startWritesTextBox.Text);
			}

			name = "Second Writes";

			if (ConfigHandler.UseTranslation)
			{
				name = Translator.GetText("secondWrites");
			}

			valid = GenericHelper.CheckIntegerValue(true, endWritesTextBox.Text, name, true);

			if (!valid)
			{
				endWritesTextBox.Focus();
				return false;
			}
			else
			{
				_endWrites = Convert.ToInt32(endWritesTextBox.Text);
			}
		}

		if (enableCpuCheckBox.Checked)
		{
			name = "First Cpu";

			if (ConfigHandler.UseTranslation)
			{
				name = Translator.GetText("firstCpu");
			}

			valid = GenericHelper.CheckIntegerValue(true, startCpuTextBox.Text, name, true);

			if (!valid)
			{
				startCpuTextBox.Focus();
				return false;
			}
			else
			{
				_startCpu = Convert.ToInt32(startCpuTextBox.Text);
			}

			name = "Second Cpu";

			if (ConfigHandler.UseTranslation)
			{
				name = Translator.GetText("secondCpu");
			}

			valid = GenericHelper.CheckIntegerValue(true, endCpuTextBox.Text, name, true);

			if (!valid)
			{
				endCpuTextBox.Focus();
				return false;
			}
			else
			{
				_endCpu = Convert.ToInt32(endCpuTextBox.Text);
			}
		}

		if (enableSpidCheckBox.Checked)
		{
			name = "First SPID";

			if (ConfigHandler.UseTranslation)
			{
				name = Translator.GetText("firstSPID");
			}

			valid = GenericHelper.CheckIntegerValue(true, startSpidTextBox.Text, name, false);

			if (!valid)
			{
				startSpidTextBox.Focus();
				return false;
			}
			else
			{
				_startSpid = Convert.ToInt32(startSpidTextBox.Text);
			}

			name = "Second SPID";

			if (ConfigHandler.UseTranslation)
			{
				name = Translator.GetText("secondSPID");
			}

			valid = GenericHelper.CheckIntegerValue(true, endSpidTextBox.Text, name, false);

			if (!valid)
			{
				endSpidTextBox.Focus();
				return false;
			}
			else
			{
				_endSpid = Convert.ToInt32(endSpidTextBox.Text);
			}
		}

		if (enableRowsCheckBox.Checked)
		{
			name = "First Rows";

			if (ConfigHandler.UseTranslation)
			{
				name = Translator.GetText("firstRows");
			}

			valid = GenericHelper.CheckIntegerValue(true, startRowsTextBox.Text, name, true);

			if (!valid)
			{
				startRowsTextBox.Focus();
				return false;
			}
			else
			{
				_startRows = Convert.ToInt32(startRowsTextBox.Text);
			}

			name = "Second Rows";

			if (ConfigHandler.UseTranslation)
			{
				name = Translator.GetText("secondRows");
			}

			valid = GenericHelper.CheckIntegerValue(true, endRowsTextBox.Text, name, true);

			if (!valid)
			{
				endRowsTextBox.Focus();
				return false;
			}
			else
			{
				_endRows = Convert.ToInt32(endRowsTextBox.Text);
			}
		}

		return true;
	}

	public void FillDefaultTraceDataValues()
	{
		if (Convert.ToInt32(_traceDataInfo.Tables[0].Rows[0]["TotalRows"]) > 0)
		{
			startIdTextBox.Text = _traceDataInfo.Tables[0].Rows[0]["MinID"].ToString();
			endIdTextBox.Text = _traceDataInfo.Tables[0].Rows[0]["MaxID"].ToString();
			enableIdCheckBox.Checked = false;

			startDurationTextBox.Text = _traceDataInfo.Tables[0].Rows[0]["MinDuration"].ToString();
			endDurationTextBox.Text = _traceDataInfo.Tables[0].Rows[0]["MaxDuration"].ToString();
			enableDurationCheckBox.Checked = false;

			dynamicStartTimeStartCheckBox.Checked = false;
			dynamicStartTimeEndCheckBox.Checked = false;

			startDateTimePicker.Value = RoundDateTimeDown(Convert.ToDateTime(_traceDataInfo.Tables[0].Rows[0]["MinStartTime"]));
			endDateTimePicker.Value = RoundDateTimeUp(Convert.ToDateTime(_traceDataInfo.Tables[0].Rows[0]["MaxStartTime"]));
			enableStartTimeCheckBox.Checked = false;

			startReadsTextBox.Text = _traceDataInfo.Tables[0].Rows[0]["MinReads"].ToString();
			endReadsTextBox.Text = _traceDataInfo.Tables[0].Rows[0]["MaxReads"].ToString();
			enableReadsCheckBox.Checked = false;

			startWritesTextBox.Text = _traceDataInfo.Tables[0].Rows[0]["MinWrites"].ToString();
			endWritesTextBox.Text = _traceDataInfo.Tables[0].Rows[0]["MaxWrites"].ToString();
			enableWritesCheckBox.Checked = false;

			startCpuTextBox.Text = _traceDataInfo.Tables[0].Rows[0]["MinCPU"].ToString();
			endCpuTextBox.Text = _traceDataInfo.Tables[0].Rows[0]["MaxCPU"].ToString();
			enableCpuCheckBox.Checked = false;

			startRowsTextBox.Text = _traceDataInfo.Tables[0].Rows[0]["MinRows"].ToString();
			endRowsTextBox.Text = _traceDataInfo.Tables[0].Rows[0]["MaxRows"].ToString();
			enableRowsCheckBox.Checked = false;

			startSpidTextBox.Text = _traceDataInfo.Tables[0].Rows[0]["MinSPID"].ToString();
			endSpidTextBox.Text = _traceDataInfo.Tables[0].Rows[0]["MaxSPID"].ToString();
			enableSpidCheckBox.Checked = false;

			if (ConfigHandler.EnableFileNameAndType)
			{
				SetTypeComboBox(_traceDataInfo.Tables[1]);
				SetFileNameComboBox(_traceDataInfo.Tables[2]);
				enableTypeCheckBox.Checked = false;
				enableFileNameCheckBox.Checked = false;
			}
		}
	}

	public void DisableAllCheckBoxes()
	{
		enableIdCheckBox.Checked = false;
		enableDurationCheckBox.Checked = false;
		enableStartTimeCheckBox.Checked = false;
		enableReadsCheckBox.Checked = false;
		enableWritesCheckBox.Checked = false;
		enableCpuCheckBox.Checked = false;
		enableRowsCheckBox.Checked = false;
		enableSpidCheckBox.Checked = false;
		enableTypeCheckBox.Checked = false;
		enableFileNameCheckBox.Checked = false;
		dynamicStartTimeStartCheckBox.Checked = false;
		dynamicStartTimeEndCheckBox.Checked = false;
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			andLabel1.Text = Translator.GetText("and");
			andLabel2.Text = Translator.GetText("and");
			andLabel3.Text = Translator.GetText("and");
			andLabel4.Text = Translator.GetText("and");
			andLabel5.Text = Translator.GetText("and");
			andLabel6.Text = Translator.GetText("and");
			andLabel7.Text = Translator.GetText("and");
			andLabel8.Text = Translator.GetText("and");
			readsLabel.Text = Translator.GetText("readsLabel");
			writesLabel.Text = Translator.GetText("writesLabel");
			cpuLabel.Text = Translator.GetText("cpuLabel");
			rowsLabel.Text = Translator.GetText("rowsLabel");
			durationLabel.Text = Translator.GetText("durationLabel");
			spidLabel.Text = Translator.GetText("spidLabel");
			idLabel.Text = Translator.GetText("idLabel");
			startTimeLabel.Text = Translator.GetText("startTimeLabel");
			enableIdCheckBox.Text = Translator.GetText("Enabled");
			enableDurationCheckBox.Text = Translator.GetText("Enabled");
			enableStartTimeCheckBox.Text = Translator.GetText("Enabled");
			enableReadsCheckBox.Text = Translator.GetText("Enabled");
			enableWritesCheckBox.Text = Translator.GetText("Enabled");
			enableCpuCheckBox.Text = Translator.GetText("Enabled");
			enableRowsCheckBox.Text = Translator.GetText("Enabled");
			enableSpidCheckBox.Text = Translator.GetText("Enabled");
			enableFileNameCheckBox.Text = Translator.GetText("Enabled");
			enableTypeCheckBox.Text = Translator.GetText("Enabled");
			dynamicStartTimeStartCheckBox.Text = Translator.GetText("SetDynamic");
			dynamicStartTimeEndCheckBox.Text = Translator.GetText("SetDynamic");
		}
	}

	private void InitializeTypeComboBox()
	{
		typeComboBox.Items.Clear();

		if (ConfigHandler.UseTranslation)
		{
			typeComboBox.Items.Add(new ComboBoxItem(Translator.GetText("All")));
		}
		else
		{
			typeComboBox.Items.Add(new ComboBoxItem("All"));
		}

		typeComboBox.SelectedIndex = 0;
	}

	private void InitializeFileNameComboBox()
	{
		fileNameComboBox.Items.Clear();

		if (ConfigHandler.UseTranslation)
		{
			fileNameComboBox.Items.Add(new ComboBoxItem(Translator.GetText("All")));
		}
		else
		{
			fileNameComboBox.Items.Add(new ComboBoxItem("All"));
		}

		fileNameComboBox.SelectedIndex = 0;
	}

	private void SetTypeComboBox(DataTable typesDataTable)
	{
		InitializeTypeComboBox();

		foreach (DataRow type in typesDataTable.Rows)
		{
			typeComboBox.Items.Add(new ComboBoxItem(type["Type"].ToString()));
		}
	}

	private void SetFileNameComboBox(DataTable fileNamesDataTable)
	{
		InitializeFileNameComboBox();

		foreach (DataRow fileName in fileNamesDataTable.Rows)
		{
			fileNameComboBox.Items.Add(new ComboBoxItem(fileName["FileName"].ToString()));
		}
	}

	private void StartIdTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}

	private void EndIdTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}

	private void StartSpidTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}

	private void EndSpidTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}

	private void StartDurationTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}

	private void EndDurationTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}

	private void StartDateTimePicker_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}

	private void EndDateTimePicker_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}

	private void StartReadsTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}

	private void EndReadsTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}

	private void StartWritesTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}

	private void EndWritesTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}

	private void StartCpuTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}

	private void EndCpuTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}

	private void StartRowsTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}

	private void EndRowsTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}

	private static DateTime RoundDateTimeDown(DateTime dateTime)
	{
		return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
	}

	private static DateTime RoundDateTimeUp(DateTime dateTime)
	{
		if (dateTime.Millisecond > 0)
		{
			int diff = 1000 - dateTime.Millisecond;
			return dateTime.AddMilliseconds(diff);
		}
		else
		{
			return dateTime;
		}
	}

	private class ComboBoxItem
	{
		private readonly string _text;

		public ComboBoxItem(string text)
		{
			_text = text;
		}

		public override string ToString()
		{
			return _text;
		}
	}

	private static Filter GetFilter(string name, string value, string operatorValue)
	{
		Filter filter = new Filter();
		filter.AndOr = "and";
		filter.Column = name;
		filter.Operator = operatorValue;
		filter.Value = value;
		filter.ParanthesBegin = "";
		filter.ParanthesEnd = "";
		filter.Filter1Search = true;

		return filter;
	}

	private void FireEnterKeyEvent()
	{
		if (EnterKeyEvent != null)
		{
			EnterKeyEvent();
		}
	}

	private void LeftCopyIdButton_Click(object sender, EventArgs e)
	{
		endIdTextBox.Text = startIdTextBox.Text;
	}

	private void RightCopyIdButton_Click(object sender, EventArgs e)
	{
		startIdTextBox.Text = endIdTextBox.Text;
	}

	private void LeftCopyDurationButton_Click(object sender, EventArgs e)
	{
		endDurationTextBox.Text = startDurationTextBox.Text;
	}

	private void RightCopyDurationButton_Click(object sender, EventArgs e)
	{
		startDurationTextBox.Text = endDurationTextBox.Text;
	}

	private void LeftCopyStartTimeButton_Click(object sender, EventArgs e)
	{
		if (dynamicStartTimeStartCheckBox.Checked && dynamicStartTimeEndCheckBox.Checked)
		{
			endDateTimeTextBox.Text = startDateTimeTextBox.Text;
		}
		else if (!dynamicStartTimeStartCheckBox.Checked && !dynamicStartTimeEndCheckBox.Checked)
		{
			endDateTimePicker.Value = startDateTimePicker.Value;
		}
	}

	private void RightCopyStartTimeButton_Click(object sender, EventArgs e)
	{
		if (dynamicStartTimeStartCheckBox.Checked && dynamicStartTimeEndCheckBox.Checked)
		{
			startDateTimeTextBox.Text = endDateTimeTextBox.Text;
		}
		else if (!dynamicStartTimeStartCheckBox.Checked && !dynamicStartTimeEndCheckBox.Checked)
		{
			startDateTimePicker.Value = endDateTimePicker.Value;
		}
	}

	private void LeftCopyReadsButton_Click(object sender, EventArgs e)
	{
		endReadsTextBox.Text = startReadsTextBox.Text;
	}

	private void RightCopyReadsButton_Click(object sender, EventArgs e)
	{
		startReadsTextBox.Text = endReadsTextBox.Text;
	}

	private void LeftCopyWritesButton_Click(object sender, EventArgs e)
	{
		endWritesTextBox.Text = startWritesTextBox.Text;
	}

	private void RightCopyWritesButton_Click(object sender, EventArgs e)
	{
		startWritesTextBox.Text = endWritesTextBox.Text;
	}

	private void LeftCopyCpuButton_Click(object sender, EventArgs e)
	{
		endCpuTextBox.Text = startCpuTextBox.Text;
	}

	private void RightCopyCpuButton_Click(object sender, EventArgs e)
	{
		startCpuTextBox.Text = endCpuTextBox.Text;
	}

	private void LeftCopyRowsButton_Click(object sender, EventArgs e)
	{
		endRowsTextBox.Text = startRowsTextBox.Text;
	}

	private void RightCopyRowsButton_Click(object sender, EventArgs e)
	{
		startRowsTextBox.Text = endRowsTextBox.Text;
	}

	private void LeftCopySpidButton_Click(object sender, EventArgs e)
	{
		endSpidTextBox.Text = startSpidTextBox.Text;
	}

	private void RightCopySpidButton_Click(object sender, EventArgs e)
	{
		startSpidTextBox.Text = endSpidTextBox.Text;
	}

	private void EnableIdCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		startIdTextBox.Enabled = enableIdCheckBox.Checked;
		endIdTextBox.Enabled = enableIdCheckBox.Checked;
		leftCopyIdButton.Enabled = enableIdCheckBox.Checked;
		rightCopyIdButton.Enabled = enableIdCheckBox.Checked;
	}

	private void EnableDurationCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		startDurationTextBox.Enabled = enableDurationCheckBox.Checked;
		endDurationTextBox.Enabled = enableDurationCheckBox.Checked;
		leftCopyDurationButton.Enabled = enableDurationCheckBox.Checked;
		rightCopyDurationButton.Enabled = enableDurationCheckBox.Checked;
	}

	private void EnableStartTimeCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		startDateTimePicker.Enabled = enableStartTimeCheckBox.Checked;
		endDateTimePicker.Enabled = enableStartTimeCheckBox.Checked;
		startDateTimeTextBox.Enabled = enableStartTimeCheckBox.Checked;
		endDateTimeTextBox.Enabled = enableStartTimeCheckBox.Checked;
		leftCopyStartTimeButton.Enabled = enableStartTimeCheckBox.Checked;
		rightCopyStartTimeButton.Enabled = enableStartTimeCheckBox.Checked;
		dynamicStartTimeStartCheckBox.Enabled = enableStartTimeCheckBox.Checked;
		dynamicStartTimeEndCheckBox.Enabled = enableStartTimeCheckBox.Checked;
		dynamicDateTimeHelpButton1.Enabled = enableStartTimeCheckBox.Checked;
		dynamicDateTimeHelpButton2.Enabled = enableStartTimeCheckBox.Checked;
	}

	private void EnableReadsCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		startReadsTextBox.Enabled = enableReadsCheckBox.Checked;
		endReadsTextBox.Enabled = enableReadsCheckBox.Checked;
		leftCopyReadsButton.Enabled = enableReadsCheckBox.Checked;
		rightCopyReadsButton.Enabled = enableReadsCheckBox.Checked;
	}

	private void EnableWritesCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		startWritesTextBox.Enabled = enableWritesCheckBox.Checked;
		endWritesTextBox.Enabled = enableWritesCheckBox.Checked;
		leftCopyWritesButton.Enabled = enableWritesCheckBox.Checked;
		rightCopyWritesButton.Enabled = enableWritesCheckBox.Checked;
	}

	private void EnableCpuCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		startCpuTextBox.Enabled = enableCpuCheckBox.Checked;
		endCpuTextBox.Enabled = enableCpuCheckBox.Checked;
		leftCopyCpuButton.Enabled = enableCpuCheckBox.Checked;
		rightCopyCpuButton.Enabled = enableCpuCheckBox.Checked;
	}

	private void EnableRowsCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		startRowsTextBox.Enabled = enableRowsCheckBox.Checked;
		endRowsTextBox.Enabled = enableRowsCheckBox.Checked;
		leftCopyRowsButton.Enabled = enableRowsCheckBox.Checked;
		rightCopyRowsButton.Enabled = enableRowsCheckBox.Checked;
	}

	private void EnableSpidCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		startSpidTextBox.Enabled = enableSpidCheckBox.Checked;
		endSpidTextBox.Enabled = enableSpidCheckBox.Checked;
		leftCopySpidButton.Enabled = enableSpidCheckBox.Checked;
		rightCopySpidButton.Enabled = enableSpidCheckBox.Checked;
	}

	private void EnableFileNameCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		if (ConfigHandler.EnableFileNameAndType)
		{
			fileNameComboBox.Enabled = enableFileNameCheckBox.Checked;
		}
	}

	private void EnableTypeCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		if (ConfigHandler.EnableFileNameAndType)
		{
			typeComboBox.Enabled = enableTypeCheckBox.Checked;
		}
	}

	private void DynamicStartTimeStartCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		if (dynamicStartTimeStartCheckBox.Checked)
		{
			startDateTimeTextBox.Visible = true;
			startDateTimePicker.Visible = false;
			startDateTimeTextBox.Focus();
		}
		else
		{
			startDateTimeTextBox.Visible = false;
			startDateTimePicker.Visible = true;
		}
	}

	private void DynamicStartTimeEndCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		if (dynamicStartTimeEndCheckBox.Checked)
		{
			endDateTimeTextBox.Visible = true;
			endDateTimePicker.Visible = false;
			endDateTimeTextBox.Focus();
		}
		else
		{
			endDateTimeTextBox.Visible = false;
			endDateTimePicker.Visible = true;
		}
	}

	private void DynamicDateTimeHelpButton1_Click(object sender, EventArgs e)
	{
		ShowDynamicDateTimeHelp();
	}

	private void DynamicDateTimeHelpButton2_Click(object sender, EventArgs e)
	{
		ShowDynamicDateTimeHelp();
	}

	private static void ShowDynamicDateTimeHelp()
	{
		string text = "Use \"dd\" for current date and time.\r\nUse \"led\" for last event date.\r\n\r\nExample (last seven days from current date and time):\r\ndd - 7\r\n\r\nExample (last seven days from last event date):\r\nled - 7\r\n\r\nAllowed operators are: \"-\" and \"+\".";

		if (ConfigHandler.UseTranslation)
		{
			text = Translator.GetText("DynamicDateTimeHelp");
		}

		OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
	}

	private void StartDateTimeTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}

	private void EndDateTimeTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			FireEnterKeyEvent();
		}
	}
}
