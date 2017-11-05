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

using System.Windows.Forms;

public partial class SplashForm : Form
{
	public SplashForm()
	{
		InitializeComponent();
		InitializeDictionary();
		Initialize();
	}

	public void SetText(string text)
	{
		initializingLabel.Text = text;
		initializingLabel.Refresh();
		backgroundLabel.Refresh();
	}

	public void Begin(IWin32Window owner)
	{
		if (GenericHelper.IsUserInteractive())
		{
			Show(owner);
		}

		Refresh();
	}

	public void End()
	{
		Hide();
	}

	private void Initialize()
	{
		Text = GenericHelper.ApplicationName;
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			initializingLabel.Text = Translator.GetText("Initializing");
		}

		initializingLabel.Refresh();
	}

	private void SplashForm_Load(object sender, System.EventArgs e)
	{
		if (Owner != null)
		{
			Location = new System.Drawing.Point(Owner.Location.X + (Owner.Width - Width) / 2, Owner.Location.Y + (Owner.Height - Height) / 2);
		}
	}
}
