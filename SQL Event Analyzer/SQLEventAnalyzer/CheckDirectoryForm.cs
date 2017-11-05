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
using System.IO;
using System.Windows.Forms;

public partial class CheckDirectoryForm : Form
{
	private BackgroundWorker _worker;
	private Timer _timer;
	private bool _directoryExist;

	public CheckDirectoryForm()
	{
		InitializeComponent();
	}

	public void Initialize(string directory)
	{
		InitializeDictionary();
		Text = GenericHelper.ApplicationName;

		Opacity = 0;

		_timer = new Timer();
		_timer.Interval = 500;
		_timer.Tick += Timer_Tick;
		_timer.Start();

		if (GenericHelper.IsUserInteractive())
		{
			InitializeWorker();
			_worker.RunWorkerAsync(directory);
		}
		else
		{
			RunWorkerCompleted(DoWork(directory));
		}
	}

	public bool GetDirectoryExist()
	{
		return _directoryExist;
	}

	protected override bool ShowWithoutActivation
	{
		get
		{
			return true;
		}
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			preparingLabel.Text = Translator.GetText("checkingImportDirectory");
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

	private static void Worker_DoWork(object sender, DoWorkEventArgs e)
	{
		string directory = e.Argument.ToString();
		e.Result = DoWork(directory);

		if (Directory.Exists(directory))
		{
			e.Result = true;
		}
		else
		{
			e.Result = false;
		}
	}

	private static bool DoWork(string directory)
	{
		if (Directory.Exists(directory))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private void RunWorkerCompleted(bool directoryExists)
	{
		_directoryExist = directoryExists;
		Close();
	}

	private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		RunWorkerCompleted(Convert.ToBoolean(e.Result));
	}
}
