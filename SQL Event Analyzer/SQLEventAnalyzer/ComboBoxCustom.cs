﻿/*
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

public class ComboBoxCustom : ComboBox
{
	private int _previousComboBoxIndex = -1;

	protected override void OnSelectedIndexChanged(EventArgs e)
	{
		if (_previousComboBoxIndex == SelectedIndex)
		{
			return;
		}
		else
		{
			_previousComboBoxIndex = SelectedIndex;
		}

		base.OnSelectedIndexChanged(e);
	}
}
