/*
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

public class PagerPanel
{
	public Button FirstPageButton;
	public Button LastPageButton;
	public Button NextPageButton;
	public Button PreviousPageButton;
	public TextBox PageTextBox;
	public TextBox TotalRowsTextBox;
	public Label TotalPagesLabel;
}
