﻿/*
Copyright (C) 2017 Lars Hove Christiansen
http://virtcore.com

This file is a part of DataViewer

	DataViewer is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	DataViewer is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with DataViewer. If not, see <http://www.gnu.org/licenses/>.
*/

using System.Windows.Forms;

internal class OutputHandler
{
	public delegate void ShowOutputEventHandler(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
	public event ShowOutputEventHandler ShowOutputEvent;

	internal void Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
	{
		FireShowOutputEvent(text, caption, buttons, icon);
	}

	private void FireShowOutputEvent(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
	{
		if (ShowOutputEvent != null)
		{
			ShowOutputEvent(text, caption, buttons, icon);
		}
	}
}
