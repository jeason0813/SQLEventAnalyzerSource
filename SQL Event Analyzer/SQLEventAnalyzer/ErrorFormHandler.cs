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

using System.Threading;
using System.Windows.Forms;

public class ErrorFormHandler
{
	public void ErrorOccuredEvent(string okButtonText, string message, string sql)
	{
		Thread t = new Thread(ShowErrorForm);
		t.SetApartmentState(ApartmentState.STA);
		t.Start(new ErrorFormParams(okButtonText, message, sql));
		t.Join();
	}

	private static void ShowErrorForm(object arg)
	{
		ErrorFormParams errorFormParams = (ErrorFormParams)arg;
		ErrorForm form = new ErrorForm();

		string message = errorFormParams.Message;

		if (ConfigHandler.ActiveCustomColumn != "")
		{
			string text = "Error in Custom Column";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("errorInCustomColumn");
			}

			message = string.Format("{2}: {1}\r\n{0}", message, ConfigHandler.ActiveCustomColumn, text);
		}

		form.SetValues(errorFormParams.OkButtonText, message, errorFormParams.Sql, GenericHelper.InfoText);

		if (GenericHelper.IsUserInteractive())
		{
			form.ShowDialog();
		}

		OutputHandler.WriteToLog(string.Format("{0}\r\n{1}", message, errorFormParams.Sql));

		Application.DoEvents();
	}
}
