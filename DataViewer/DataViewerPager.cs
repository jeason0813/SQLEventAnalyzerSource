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

using System;
using System.Globalization;
using System.Windows.Forms;

public class DataViewerPager
{
	public delegate void PageChangedEventHandler(int page);
	public event PageChangedEventHandler PageChangedEvent;

	private readonly PagerPanel _pagerPanel;
	private int _page;
	private int _totalRows;
	private int _itemsPerPage;
	private bool _eventsEnabled = true;
	private readonly string _outOfText;
	private readonly string _totalText;

	public DataViewerPager(PagerPanel PagerPanel, int itemsPerPage, string outOfText, string totalText)
	{
		_outOfText = outOfText;
		_totalText = totalText;

		_itemsPerPage = itemsPerPage;
		_pagerPanel = PagerPanel;

		InitializeEvents();
	}

	public void SetEventsEnabled(bool value)
	{
		_eventsEnabled = value;
	}

	public void UpdatePagingInfo(int page, int totalRows)
	{
		_totalRows = totalRows;
		_page = page;

		HandlePageButtons();
	}

	public void SetItemsPerPage(int itemsPerPage)
	{
		_itemsPerPage = itemsPerPage;
	}

	public void ChangePage(int page)
	{
		_pagerPanel.PageTextBox.Text = page.ToString();
		SetPage(page);
		HandlePageButtons();
	}

	public void GoToNextPage()
	{
		if (_eventsEnabled)
		{
			NextPage();
			HandlePageButtons();
		}
	}

	public void GoToPreviousPage()
	{
		if (_eventsEnabled)
		{
			PreviousPage();
			HandlePageButtons();
		}
	}

	public int GetTotalPages()
	{
		double items = Convert.ToDouble(_totalRows) / Convert.ToDouble(_itemsPerPage);
		return Convert.ToInt32(Math.Ceiling(items));
	}

	private void InitializeEvents()
	{
		_pagerPanel.FirstPageButton.Click += FirstPageButton_Click;
		_pagerPanel.LastPageButton.Click += LastPageButton_Click;
		_pagerPanel.NextPageButton.Click += NextPageButton_Click;
		_pagerPanel.PreviousPageButton.Click += PreviousPageButton_Click;
		_pagerPanel.PageTextBox.KeyDown += PageTextBox_KeyDown;
	}

	private void SetPage(int page)
	{
		_page = page;
		FirePageChangedEvent();
	}

	private void NextPage()
	{
		if (_page < GetTotalPages())
		{
			_page++;
			FirePageChangedEvent();
		}
	}

	private void PreviousPage()
	{
		if (_page > 1)
		{
			_page--;
			FirePageChangedEvent();
		}
	}

	private void FirePageChangedEvent()
	{
		if (PageChangedEvent != null)
		{
			PageChangedEvent(_page);
		}
	}

	private void NextPageButton_Click(object sender, EventArgs e)
	{
		GoToNextPage();
	}

	private void PreviousPageButton_Click(object sender, EventArgs e)
	{
		GoToPreviousPage();
	}

	private void FirstPageButton_Click(object sender, EventArgs e)
	{
		if (_eventsEnabled)
		{
			SetPage(1);
			HandlePageButtons();
		}
	}

	private void LastPageButton_Click(object sender, EventArgs e)
	{
		if (_eventsEnabled)
		{
			SetPage(GetTotalPages());
			HandlePageButtons();
		}
	}

	private void PageTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (_eventsEnabled)
		{
			if (e.KeyCode == Keys.Enter)
			{
				int currentPage = _page;

				try
				{
					int page = Convert.ToInt32(_pagerPanel.PageTextBox.Text);

					if (page < 1)
					{
						page = 1;
						_pagerPanel.PageTextBox.Text = "1";
					}
					else if (page > GetTotalPages())
					{
						page = GetTotalPages();
						_pagerPanel.PageTextBox.Text = GetTotalPages().ToString();
					}

					SetPage(page);
					HandlePageButtons();
				}
				catch
				{
					_pagerPanel.PageTextBox.Text = currentPage.ToString();
				}
			}
		}
	}

	private static string FormatWithThousandSeparator(int input)
	{
		return input.ToString("N0", CultureInfo.CurrentCulture);
	}

	private void HandlePageButtons()
	{
		_pagerPanel.PageTextBox.Text = _page.ToString();
		_pagerPanel.TotalPagesLabel.Text = string.Format("{1} {0}", FormatWithThousandSeparator(GetTotalPages()), _outOfText);
		_pagerPanel.TotalRowsTextBox.Text = string.Format("{1}: {0}", FormatWithThousandSeparator(_totalRows), _totalText);

		if (GetTotalPages() <= 1)
		{
			_pagerPanel.PreviousPageButton.Enabled = false;
			_pagerPanel.FirstPageButton.Enabled = false;
			_pagerPanel.NextPageButton.Enabled = false;
			_pagerPanel.LastPageButton.Enabled = false;
		}
		else if (_page == 1)
		{
			_pagerPanel.PreviousPageButton.Enabled = false;
			_pagerPanel.FirstPageButton.Enabled = false;
			_pagerPanel.NextPageButton.Enabled = true;
			_pagerPanel.LastPageButton.Enabled = true;

			if (_pagerPanel.PageTextBox.Focused)
			{
				_pagerPanel.NextPageButton.Focus();
			}
		}
		else if (_page == GetTotalPages())
		{
			_pagerPanel.NextPageButton.Enabled = false;
			_pagerPanel.LastPageButton.Enabled = false;
			_pagerPanel.PreviousPageButton.Enabled = true;
			_pagerPanel.FirstPageButton.Enabled = true;

			if (_pagerPanel.PageTextBox.Focused)
			{
				_pagerPanel.PreviousPageButton.Focus();
			}
		}
		else
		{
			_pagerPanel.NextPageButton.Enabled = true;
			_pagerPanel.PreviousPageButton.Enabled = true;
			_pagerPanel.FirstPageButton.Enabled = true;
			_pagerPanel.LastPageButton.Enabled = true;
		}
	}
}
