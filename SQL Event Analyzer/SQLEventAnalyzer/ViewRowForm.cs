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
using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

public partial class ViewRowForm : Form
{
	private SearchTextForm _searchForm;
	private bool _caretPositionChangeFromSearch;

	public ViewRowForm()
	{
		InitializeComponent();
	}

	public void SetValues(string sql)
	{
		InitializeDictionary();
		Text = GenericHelper.ApplicationName;
		SetSize();

		InitializeSearch();

		infoTextBox.SetHighlighting("SQL");
		infoTextBox.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("SQL");

		infoTextBox.TextEditorProperties.Font = new Font(ConfigHandler.TextDataFontFamily, float.Parse(ConfigHandler.TextDataFontSize));
		infoTextBox.Font = new Font(ConfigHandler.TextDataFontFamily, float.Parse(ConfigHandler.TextDataFontSize));

		infoTextBox.Text = sql;

		MinimumSize = new Size(689, 394); // error in .NET

		ActiveControl = infoTextBox;
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (infoTextBox.ActiveTextAreaControl.TextArea.Focused)
		{
			if ((int)keyData == 131137) // Keys.Control && Keys.A
			{
				SelectAll();
				return true;
			}
			if ((int)keyData == 27) // Keys.Escape
			{
				Close();
				return true;
			}
		}

		return base.ProcessCmdKey(ref msg, keyData);
	}

	private void InitializeSearch()
	{
		_searchForm = new SearchTextForm();
		_searchForm.Initialize();

		if (ConfigHandler.UseTranslation)
		{
			_searchForm.SetTitle(Translator.GetText("searchInTextData"));
		}
		else
		{
			_searchForm.SetTitle("Search in TextData");
		}

		infoTextBox.ActiveTextAreaControl.Caret.PositionChanged += Caret_PositionChanged;
		_searchForm.SearchEvent += SearchForm_SearchEvent;
	}

	private void SelectAll()
	{
		TextLocation startPosition = new TextLocation(0, 0);

		int textLength = infoTextBox.ActiveTextAreaControl.Document.TextLength;
		TextLocation endPosition = new TextLocation();
		endPosition.Column = infoTextBox.Document.OffsetToPosition(textLength).Column;
		endPosition.Line = infoTextBox.Document.OffsetToPosition(textLength).Line;

		infoTextBox.ActiveTextAreaControl.SelectionManager.SetSelection(startPosition, endPosition);
		infoTextBox.ActiveTextAreaControl.Caret.Position = endPosition;
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			okButton.Text = Translator.GetText("okButton");
			fileToolStripMenuItem.Text = Translator.GetText("fileToolStripMenuItem");
			closeToolStripMenuItem.Text = Translator.GetText("closeToolStripMenuItem");
			actionToolStripMenuItem.Text = Translator.GetText("actionToolStripMenuItem");
			searchToolStripMenuItem.Text = Translator.GetText("findToolStripMenuItem");
			toolStripMenuItemUndo.Text = Translator.GetText("toolStripMenuItemUndo");
			toolStripMenuItemRedo.Text = Translator.GetText("toolStripMenuItemRedo");
			toolStripMenuItemCut.Text = Translator.GetText("toolStripMenuItemCut");
			toolStripMenuItemCopy.Text = Translator.GetText("toolStripMenuItemCopy");
			toolStripMenuItemPaste.Text = Translator.GetText("toolStripMenuItemPaste");
			toolStripMenuItemDelete.Text = Translator.GetText("toolStripMenuItemDelete");
			toolStripMenuItemSelectAll.Text = Translator.GetText("toolStripMenuItemSelectAll1");
			searchToolStripMenuItem1.Text = Translator.GetText("findToolStripMenuItem");
		}
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
	{
		if (infoTextBox.Document.UndoStack.CanUndo)
		{
			toolStripMenuItemUndo.Enabled = true;
		}
		else
		{
			toolStripMenuItemUndo.Enabled = false;
		}

		if (infoTextBox.Document.UndoStack.CanRedo)
		{
			toolStripMenuItemRedo.Enabled = true;
		}
		else
		{
			toolStripMenuItemRedo.Enabled = false;
		}
	}

	private void ToolStripMenuItemUndo_Click(object sender, EventArgs e)
	{
		infoTextBox.Undo();
	}

	private void ToolStripMenuItemRedo_Click(object sender, EventArgs e)
	{
		infoTextBox.Redo();
	}

	private void ToolStripMenuItemCut_Click(object sender, EventArgs e)
	{
		infoTextBox.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(sender, e);
	}

	private void ToolStripMenuItemCopy_Click(object sender, EventArgs e)
	{
		infoTextBox.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(sender, e);
	}

	private void ToolStripMenuItemPaste_Click(object sender, EventArgs e)
	{
		infoTextBox.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(sender, e);
	}

	private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
	{
		infoTextBox.ActiveTextAreaControl.TextArea.ClipboardHandler.Delete(sender, e);
	}

	private void ToolStripMenuItemSelectAll_Click(object sender, EventArgs e)
	{
		SelectAll();
	}

	private void SearchToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Search();
	}

	private void Search()
	{
		if (_searchForm.IsShown())
		{
			_searchForm.Activate();
			_searchForm.Show();
		}
		else
		{
			if (infoTextBox.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
			{
				if (!infoTextBox.ActiveTextAreaControl.SelectionManager.SelectedText.Contains("\r"))
				{
					_searchForm.SetSearchTerm(infoTextBox.ActiveTextAreaControl.SelectionManager.SelectedText);
				}
			}

			_searchForm.Hide();
			_searchForm.Show(this);
		}
	}

	private void SearchForm_SearchEvent(int foundIndex, string searchTerm)
	{
		_caretPositionChangeFromSearch = true;

		TextLocation startPosition = infoTextBox.Document.OffsetToPosition(foundIndex);
		TextLocation endPosition = infoTextBox.Document.OffsetToPosition(foundIndex + searchTerm.Length);

		infoTextBox.ActiveTextAreaControl.SelectionManager.SetSelection(startPosition, endPosition);
		infoTextBox.ActiveTextAreaControl.CenterViewOn(infoTextBox.Document.GetLineNumberForOffset(foundIndex), 0);

		infoTextBox.ActiveTextAreaControl.Caret.Line = endPosition.Line;
		infoTextBox.ActiveTextAreaControl.Caret.Column = endPosition.Column;

		_caretPositionChangeFromSearch = false;
	}

	private void Caret_PositionChanged(object sender, EventArgs e)
	{
		Caret caret = (Caret)sender;

		if (!_caretPositionChangeFromSearch)
		{
			_searchForm.Reset(caret.Offset);
		}
	}

	private void ViewRowForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		_searchForm.Hide();
	}

	private void InfoTextBox_TextChanged(object sender, EventArgs e)
	{
		_searchForm.SetSearchText(infoTextBox.Text);
		_searchForm.Reset(infoTextBox.ActiveTextAreaControl.Caret.Offset);
	}

	private void SearchToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		Search();
	}

	private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void ViewRowForm_Resize(object sender, EventArgs e)
	{
		if (Width < MinimumSize.Width || Width == 0)
		{
			return;
		}

		ConfigHandler.ViewRowWindowSize = string.Format("{0}; {1}", Size.Width, Size.Height);
		ConfigHandler.SaveConfig();
	}

	private void SetSize()
	{
		int x = Convert.ToInt32(ConfigHandler.ViewRowWindowSize.Split(';')[0]);
		int y = Convert.ToInt32(ConfigHandler.ViewRowWindowSize.Split(';')[1]);

		if (x > Screen.PrimaryScreen.Bounds.Width || y > Screen.PrimaryScreen.Bounds.Height)
		{
			WindowState = FormWindowState.Maximized;
			return;
		}

		if (x >= MinimumSize.Width && y >= MinimumSize.Height)
		{
			Size = new Size(x, y);
		}
	}
}
