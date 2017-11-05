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
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

public partial class GetDataForm : Form
{
	private DatabaseOperation _databaseOperation;
	private BackgroundWorker _worker;
	private DataSet _dataSet;
	private Timer _timer;
	private readonly Stopwatch _sw = new Stopwatch();

	public GetDataForm()
	{
		InitializeComponent();
	}

	public void Initialize(DatabaseOperation databaseOperation, string sql)
	{
		ConfigHandler.GetDataFormShown = true;

		InitializeDictionary();

		timeTextBox.GotFocus += TimeTextBox_GotFocus;

		Opacity = 0;

		elapsedTimeTimer.Start();
		_sw.Reset();
		_sw.Start();

		_timer = new Timer();
		_timer.Interval = 500;
		_timer.Tick += Timer_Tick;
		_timer.Start();

		_databaseOperation = databaseOperation;

		ConfigHandler.GetDataStartTime = DateTime.Now;

		if (GenericHelper.IsUserInteractive())
		{
			InitializeWorker();
			_worker.RunWorkerAsync(sql);
		}
		else
		{
			RunWorkerCompleted(DoWork(sql));
		}
	}

	public DataSet GetDataSet()
	{
		return _dataSet;
	}

	protected override bool ShowWithoutActivation
	{
		get { return true; }
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			infoLabel.Text = Translator.GetText("FetchingData");
			Text = Translator.GetText("Working");
		}
	}

	private void InitializeWorker()
	{
		_worker = new BackgroundWorker();
		_worker.DoWork += Worker_DoWork;
		_worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
	}

	private void Timer_Tick(object sender, EventArgs e)
	{
		_timer.Stop();
		Opacity = 100;
	}

	private void Worker_DoWork(object sender, DoWorkEventArgs e)
	{
		e.Result = DoWork(e.Argument.ToString());
	}

	private DataSet DoWork(string sql)
	{
		DataSet ds = _databaseOperation.ExecuteDataSet(sql);
		return ds;
	}

	private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		RunWorkerCompleted((DataSet)e.Result);
	}

	private void RunWorkerCompleted(DataSet dataSet)
	{
		_dataSet = dataSet;
		ConfigHandler.GetDataEndTime = DateTime.Now;
		ConfigHandler.GetDataFormShown = false;
		Close();
	}

	private void ElapsedTimeTimer_Tick(object sender, EventArgs e)
	{
		string days = _sw.Elapsed.Days.ToString();
		string hours = _sw.Elapsed.Hours.ToString();
		string minutes = _sw.Elapsed.Minutes.ToString();
		string seconds = _sw.Elapsed.Seconds.ToString();

		if (days.Length == 1)
		{
			days = string.Format("0{0}", days);
		}

		if (hours.Length == 1)
		{
			hours = string.Format("0{0}", hours);
		}

		if (minutes.Length == 1)
		{
			minutes = string.Format("0{0}", minutes);
		}

		if (seconds.Length == 1)
		{
			seconds = string.Format("0{0}", seconds);
		}

		timeTextBox.Text = string.Format("{0}:{1}:{2}:{3}", days, hours, minutes, seconds);
	}

	private void TimeTextBox_Enter(object sender, EventArgs e)
	{
		timeTextBox.SelectionStart = 0;
		timeTextBox.SelectionLength = 0;
		GenericHelper.HideCaret(timeTextBox);
	}

	private void TimeTextBox_GotFocus(object sender, EventArgs e)
	{
		timeTextBox.SelectionStart = 0;
		timeTextBox.SelectionLength = 0;
		GenericHelper.HideCaret(timeTextBox);
	}
}
