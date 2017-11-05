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
using System.Windows.Forms;

public partial class FillParameterForm : Form
{
	private bool _allowClose;

	public FillParameterForm()
	{
		InitializeComponent();
		Initialize();
	}

	public void SetNameValue(string name)
	{
		nameTextBox.Text = name;
	}

	public static bool CheckForUniqueParameterNames()
	{
		bool stop = false;

		for (int i = 0; i < ColumnHelper.ColumnCollection.Parameters.Count; i++)
		{
			for (int j = 0; j < ColumnHelper.ColumnCollection.Parameters.Count; j++)
			{
				if (ColumnHelper.ColumnCollection.Parameters[i].Name == ColumnHelper.ColumnCollection.Parameters[j].Name && j != i)
				{
					string text = "Can't continue while parameter names are not unique.";

					if (ConfigHandler.UseTranslation)
					{
						text = Translator.GetText("ParameterNamesNotUnique");
					}

					OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					stop = true;
					break;
				}
			}

			if (stop)
			{
				return false;
			}
		}

		return true;
	}

	public static bool FillMissingParameters()
	{
		bool success = true;

		foreach (Parameter parameter in ColumnHelper.ColumnCollection.Parameters)
		{
			if (parameter.Value == "")
			{
				OutputHandler.WriteToLog("Mandatory Parameters not filled.");

				if (!GenericHelper.IsUserInteractive())
				{
					success = false;
					break;
				}

				FillParameterForm form = new FillParameterForm();
				form.SetNameValue(parameter.Name);


				DialogResult result = form.ShowDialog();

				if (result == DialogResult.Cancel)
				{
					success = false;
					break;
				}
			}
		}

		return success;
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

		valueTextBox.Focus();
	}

	private void Initialize()
	{
		InitializeDictionary();
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			Text = Translator.GetText("EnterParameterValue");
			cancelButton.Text = Translator.GetText("cancelButton");
			okButton.Text = Translator.GetText("okButton");
			parameterGroupBox.Text = Translator.GetText("Parameter");
			nameLabel.Text = Translator.GetText("nameLabel");
			valueLabel.Text = Translator.GetText("valueLabel");
			infoLabel.Text = Translator.GetText("paramInfo");
		}
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		if (valueTextBox.Text.Trim().Length == 0)
		{
			string text = "Please enter a value.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("ValueMissing");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			valueTextBox.Focus();

			_allowClose = false;
		}
		else
		{
			foreach (Parameter parameter in ColumnHelper.ColumnCollection.Parameters)
			{
				if (parameter.Name == nameTextBox.Text)
				{
					parameter.Value = valueTextBox.Text;
				}
			}

			_allowClose = true;
		}
	}

	private void FillParameterForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (!_allowClose)
		{
			e.Cancel = true;
		}
	}

	private void CancelButton_Click(object sender, EventArgs e)
	{
		_allowClose = true;
	}
}
