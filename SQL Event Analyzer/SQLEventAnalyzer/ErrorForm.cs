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
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

public partial class ErrorForm : Form
{
	public ErrorForm()
	{
		InitializeComponent();
	}

	public void SetValues(string okButtonText, string message, string sql, string aboutText)
	{
		InitializeDictionary();

		errorTextBox.GotFocus += ErrorTextBox_GotFocus;
		aboutTextBox.GotFocus += AboutTextBox_GotFocus;

		infoTextBox.SetHighlighting("SQL");
		infoTextBox.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("SQL");

		infoTextBox.TextEditorProperties.Font = new Font(ConfigHandler.EditorFontFamily, float.Parse(ConfigHandler.EditorFontSize));
		infoTextBox.Font = new Font(ConfigHandler.EditorFontFamily, float.Parse(ConfigHandler.EditorFontSize));

		programNameLabel.Text = GenericHelper.ApplicationName;
		aboutTextBox.Text = FormatAboutText(aboutText);
		okButton.Text = okButtonText;
		errorTextBox.Text = message;
		infoTextBox.Text = sql;

		MinimumSize = new Size(439, 467); // error in .NET
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
		}

		return base.ProcessCmdKey(ref msg, keyData);
	}

	private static string FormatAboutText(string aboutText)
	{
		string[] lines = aboutText.Split('\n');

		StringBuilder sb = new StringBuilder();

		for (int i = 0; i < 4; i++)
		{
			sb.AppendLine(lines[i]);
		}

		return sb.ToString();
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
			aboutGroupBox.Text = Translator.GetText("About");
			copyButton.Text = Translator.GetText("Copy");
			errorMessageGroupBox.Text = Translator.GetText("ErrorMessage");
			Text = Translator.GetText("ErrorOccured");
			okButton.Text = Translator.GetText("exit");
			toolStripMenuItemUndo.Text = Translator.GetText("toolStripMenuItemUndo");
			toolStripMenuItemRedo.Text = Translator.GetText("toolStripMenuItemRedo");
			toolStripMenuItemCut.Text = Translator.GetText("toolStripMenuItemCut");
			toolStripMenuItemCopy.Text = Translator.GetText("toolStripMenuItemCopy");
			toolStripMenuItemPaste.Text = Translator.GetText("toolStripMenuItemPaste");
			toolStripMenuItemDelete.Text = Translator.GetText("toolStripMenuItemDelete");
			toolStripMenuItemSelectAll.Text = Translator.GetText("toolStripMenuItemSelectAll1");
		}
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CopyButton_Click(object sender, EventArgs e)
	{
		Thread newThread = new Thread(ThreadMethod);
		newThread.SetApartmentState(ApartmentState.STA);

		string copy = string.Format("/*\r\nError:\r\n\r\n{0}\r\n\r\nAbout:\r\n\r\n{1}*/\r\n\r\n{2}", errorTextBox.Text, aboutTextBox.Text.Replace("\t", ""), infoTextBox.Text);
		newThread.Start(copy);
	}

	private static void ThreadMethod(object text)
	{
		Clipboard.SetText(text.ToString());
	}

	private void AboutTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.A)
		{
			aboutTextBox.SelectAll();
		}
	}

	private void ErrorTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.A)
		{
			errorTextBox.SelectAll();
		}
	}

	private void ErrorTextBox_Enter(object sender, EventArgs e)
	{
		errorTextBox.SelectionStart = 0;
		errorTextBox.SelectionLength = 0;
	}

	private void AboutTextBox_Enter(object sender, EventArgs e)
	{
		aboutTextBox.SelectionStart = 0;
		aboutTextBox.SelectionLength = 0;
	}

	private void ErrorTextBox_GotFocus(object sender, EventArgs e)
	{
		errorTextBox.SelectionStart = 0;
		errorTextBox.SelectionLength = 0;
	}

	private void AboutTextBox_GotFocus(object sender, EventArgs e)
	{
		aboutTextBox.SelectionStart = 0;
		aboutTextBox.SelectionLength = 0;
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

	private void ErrorForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		ConfigHandler.ErrorFormShown = false;
	}
}
