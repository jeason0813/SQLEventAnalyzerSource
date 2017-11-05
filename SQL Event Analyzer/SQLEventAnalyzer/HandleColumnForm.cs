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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

public partial class HandleColumnForm : Form
{
	private bool _textChanged;
	private string _initialNameValue;
	private string _focusedTextEditorControl;
	private SearchTextForm _inputSearchForm;
	private SearchTextForm _outputSearchForm;
	private bool _caretPositionChangeFromInputSearch;
	private bool _caretPositionChangeFromOutputSearch;

	public HandleColumnForm()
	{
		InitializeComponent();
	}

	public void Initialize()
	{
		InitializeDictionary();
		SetSize();

		InitializeInputSearch();
		InitializeOutputSearch();

		InitializeIsolationLevelComboBox();
		InitializeInputTypeComboBox();
		InitializeOutputTypeComboBox();

		SetInitialValues();

		isolationLevelComboBox.SelectedIndexChanged += IsolationLevelComboBox_SelectedIndexChanged;
		inputTypeComboBox.SelectedIndexChanged += InputTypeComboBox_SelectedIndexChanged;
		outputTypeComboBox.SelectedIndexChanged += OutputTypeComboBox_SelectedIndexChanged;

		inputTextEditorControl.TextEditorProperties.Font = new Font(ConfigHandler.EditorFontFamily, float.Parse(ConfigHandler.EditorFontSize));
		inputTextEditorControl.Font = new Font(ConfigHandler.EditorFontFamily, float.Parse(ConfigHandler.EditorFontSize));
		inputTextEditorControl.ActiveTextAreaControl.TextArea.GotFocus += InputTextEditorControl_GotFocus;
		inputTextEditorControl.ActiveTextAreaControl.TextArea.LostFocus += TextEditorControl_LostFocus;

		outputTextEditorControl.TextEditorProperties.Font = new Font(ConfigHandler.EditorFontFamily, float.Parse(ConfigHandler.EditorFontSize));
		outputTextEditorControl.Font = new Font(ConfigHandler.EditorFontFamily, float.Parse(ConfigHandler.EditorFontSize));
		outputTextEditorControl.ActiveTextAreaControl.TextArea.GotFocus += OutputTextEditorControl_GotFocus;
		outputTextEditorControl.ActiveTextAreaControl.TextArea.LostFocus += TextEditorControl_LostFocus;

		HandleInputComboBoxChanged();
		HandleOutputComboBoxChanged();
	}

	public void SetValues(Column column)
	{
		nameTextBox.Text = column.Name;
		SetIsolationLevelComboBox(column.IsolationLevel);
		inputTextEditorControl.Text = column.Input;
		SetInputTypeComboBox(column.InputType);
		outputTextEditorControl.Text = column.Output;
		SetOutputTypeComboBox(column.OutputType);
		hiddenCheckBox.Checked = column.Hidden;
		enabledCheckBox.Checked = column.Enabled;
		widthTextBox.Text = column.Width.ToString();

		CheckCLR();

		SetTextChanged(false);
		_initialNameValue = column.Name.ToLower();
	}

	public Column GetValue()
	{
		return new Column(nameTextBox.Text, ((IsolationLevelComboBoxItem)isolationLevelComboBox.SelectedItem).IsolationLevel, inputTextEditorControl.Text, ((ComboBoxItem)inputTypeComboBox.SelectedItem).ColumnType, outputTextEditorControl.Text, ((ComboBoxItem)outputTypeComboBox.SelectedItem).ColumnType, hiddenCheckBox.Checked, enabledCheckBox.Checked, Convert.ToInt32(widthTextBox.Text));
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (inputTextEditorControl.ActiveTextAreaControl.TextArea.Focused)
		{
			if ((int)keyData == 131137) // Keys.Control && Keys.A
			{
				SelectAll(inputTextEditorControl);
				return true;
			}

			if ((int)keyData == 131142) // Keys.Control && Keys.F
			{
				InputSearch();
				return true;
			}

			if (keyData == Keys.Escape)
			{
				Close();
			}
		}

		if (outputTextEditorControl.ActiveTextAreaControl.TextArea.Focused)
		{
			if ((int)keyData == 131137) // Keys.Control && Keys.A
			{
				SelectAll(outputTextEditorControl);
				return true;
			}

			if ((int)keyData == 131142) // Keys.Control && Keys.F
			{
				OutputSearch();
				return true;
			}

			if (keyData == Keys.Escape)
			{
				Close();
			}
		}

		return base.ProcessCmdKey(ref msg, keyData);
	}

	protected override void OnLoad(EventArgs args)
	{
		if (Site == null || (Site != null && !Site.DesignMode))
		{
			base.OnLoad(args);
			Application.Idle += OnLoaded;
		}
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			fileToolStripMenuItem.Text = Translator.GetText("fileToolStripMenuItem");
			closeToolStripMenuItem.Text = Translator.GetText("closeToolStripMenuItem");
			nameLabel.Text = Translator.GetText("nameLabel");
			cancelButton.Text = Translator.GetText("cancelButton");
			okButton.Text = Translator.GetText("okButton");
			outputTypeLabel.Text = Translator.GetText("Type");
			outputGroupBox.Text = Translator.GetText("Output");
			fontToolStripMenuItem.Text = Translator.GetText("fontToolStripMenuItem");
			formatToolStripMenuItem.Text = Translator.GetText("formatToolStripMenuItem");
			outputToolStripMenuItemUndo.Text = Translator.GetText("toolStripMenuItemUndo");
			outputToolStripMenuItemRedo.Text = Translator.GetText("toolStripMenuItemRedo");
			outputToolStripMenuItemCut.Text = Translator.GetText("toolStripMenuItemCut");
			outputToolStripMenuItemCopy.Text = Translator.GetText("toolStripMenuItemCopy");
			outputToolStripMenuItemPaste.Text = Translator.GetText("toolStripMenuItemPaste");
			outputToolStripMenuItemDelete.Text = Translator.GetText("toolStripMenuItemDelete");
			outputToolStripMenuItemSelectAll.Text = Translator.GetText("toolStripMenuItemSelectAll1");
			inputToolStripMenuItemUndo.Text = Translator.GetText("toolStripMenuItemUndo");
			inputToolStripMenuItemRedo.Text = Translator.GetText("toolStripMenuItemRedo");
			inputToolStripMenuItemCut.Text = Translator.GetText("toolStripMenuItemCut");
			inputToolStripMenuItemCopy.Text = Translator.GetText("toolStripMenuItemCopy");
			inputToolStripMenuItemPaste.Text = Translator.GetText("toolStripMenuItemPaste");
			inputToolStripMenuItemDelete.Text = Translator.GetText("toolStripMenuItemDelete");
			inputToolStripMenuItemSelectAll.Text = Translator.GetText("toolStripMenuItemSelectAll1");
			inputGroupBox.Text = Translator.GetText("Input");
			inputTypeLabel.Text = Translator.GetText("Type");
			hiddenCheckBox.Text = Translator.GetText("hiddenCheckBox");
			enabledCheckBox.Text = Translator.GetText("enabledCheckBox");
			clrNotAvailableIntputLabel.Text = Translator.GetText("clrNotAvailable");
			clrNotAvailableOutputLabel.Text = Translator.GetText("clrNotAvailable");
			searchToolStripMenuItem.Text = Translator.GetText("findToolStripMenuItem");
			searchToolStripMenuItem1.Text = Translator.GetText("findToolStripMenuItem");
			insertToolStripMenuItem1.Text = Translator.GetText("insertParameter");
			insertToolStripMenuItem2.Text = Translator.GetText("insertParameter");
			widthLabel.Text = Translator.GetText("Width");
			searchToolStripMenuItem2.Text = Translator.GetText("findToolStripMenuItem");
			undoToolStripMenuItem.Text = Translator.GetText("toolStripMenuItemUndo");
			redoToolStripMenuItem.Text = Translator.GetText("toolStripMenuItemRedo");
			cutToolStripMenuItem.Text = Translator.GetText("toolStripMenuItemCut");
			copyToolStripMenuItem.Text = Translator.GetText("toolStripMenuItemCopy");
			pasteToolStripMenuItem.Text = Translator.GetText("toolStripMenuItemPaste");
			deleteToolStripMenuItem.Text = Translator.GetText("toolStripMenuItemDelete");
			selectAllToolStripMenuItem.Text = Translator.GetText("toolStripMenuItemSelectAll1");
			insertParameterToolStripMenuItem.Text = Translator.GetText("insertParameter");
		}
	}

	private void OnLoaded(object sender, EventArgs args)
	{
		Application.Idle -= OnLoaded;

		splitContainer1.SplitterMoved += SplitContainer1_SplitterMoved;
	}

	private void InitializeInputSearch()
	{
		_inputSearchForm = new SearchTextForm();
		_inputSearchForm.Initialize();

		if (ConfigHandler.UseTranslation)
		{
			_inputSearchForm.SetTitle(Translator.GetText("searchInInput"));
		}
		else
		{
			_inputSearchForm.SetTitle("Search in input");
		}

		inputTextEditorControl.ActiveTextAreaControl.Caret.PositionChanged += InputCaret_PositionChanged;
		_inputSearchForm.SearchEvent += InputSearchForm_SearchEvent;
	}

	private void InitializeOutputSearch()
	{
		_outputSearchForm = new SearchTextForm();
		_outputSearchForm.Initialize();

		if (ConfigHandler.UseTranslation)
		{
			_outputSearchForm.SetTitle(Translator.GetText("searchInOutput"));
		}
		else
		{
			_outputSearchForm.SetTitle("Search in output");
		}

		outputTextEditorControl.ActiveTextAreaControl.Caret.PositionChanged += OutputCaret_PositionChanged;
		_outputSearchForm.SearchEvent += OutputSearchForm_SearchEvent;
	}

	private void InputSearch()
	{
		if (_outputSearchForm.IsShown())
		{
			_outputSearchForm.Close();
		}

		_inputSearchForm.ReloadHistory();

		if (_inputSearchForm.IsShown())
		{
			_inputSearchForm.Activate();
			_inputSearchForm.Show();
		}
		else
		{
			if (inputTextEditorControl.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
			{
				if (!inputTextEditorControl.ActiveTextAreaControl.SelectionManager.SelectedText.Contains("\r"))
				{
					_inputSearchForm.SetSearchTerm(inputTextEditorControl.ActiveTextAreaControl.SelectionManager.SelectedText);
				}
			}

			_inputSearchForm.Hide();
			_inputSearchForm.Show(this);
		}
	}

	private void OutputSearch()
	{
		if (_inputSearchForm.IsShown())
		{
			_inputSearchForm.Close();
		}

		_outputSearchForm.ReloadHistory();

		if (_outputSearchForm.IsShown())
		{
			_outputSearchForm.Activate();
			_outputSearchForm.Show();
		}
		else
		{
			if (outputTextEditorControl.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
			{
				if (!outputTextEditorControl.ActiveTextAreaControl.SelectionManager.SelectedText.Contains("\r"))
				{
					_outputSearchForm.SetSearchTerm(outputTextEditorControl.ActiveTextAreaControl.SelectionManager.SelectedText);
				}
			}

			_outputSearchForm.Hide();
			_outputSearchForm.Show(this);
		}
	}

	private void InputSearchForm_SearchEvent(int foundIndex, string searchTerm)
	{
		_caretPositionChangeFromInputSearch = true;

		TextLocation startPosition = inputTextEditorControl.Document.OffsetToPosition(foundIndex);
		TextLocation endPosition = inputTextEditorControl.Document.OffsetToPosition(foundIndex + searchTerm.Length);

		inputTextEditorControl.ActiveTextAreaControl.SelectionManager.SetSelection(startPosition, endPosition);
		inputTextEditorControl.ActiveTextAreaControl.CenterViewOn(inputTextEditorControl.Document.GetLineNumberForOffset(foundIndex), 0);

		inputTextEditorControl.ActiveTextAreaControl.Caret.Line = endPosition.Line;
		inputTextEditorControl.ActiveTextAreaControl.Caret.Column = endPosition.Column;

		_caretPositionChangeFromInputSearch = false;
	}

	private void OutputSearchForm_SearchEvent(int foundIndex, string searchTerm)
	{
		_caretPositionChangeFromOutputSearch = true;

		TextLocation startPosition = outputTextEditorControl.Document.OffsetToPosition(foundIndex);
		TextLocation endPosition = outputTextEditorControl.Document.OffsetToPosition(foundIndex + searchTerm.Length);

		outputTextEditorControl.ActiveTextAreaControl.SelectionManager.SetSelection(startPosition, endPosition);
		outputTextEditorControl.ActiveTextAreaControl.CenterViewOn(outputTextEditorControl.Document.GetLineNumberForOffset(foundIndex), 0);

		outputTextEditorControl.ActiveTextAreaControl.Caret.Line = endPosition.Line;
		outputTextEditorControl.ActiveTextAreaControl.Caret.Column = endPosition.Column;

		_caretPositionChangeFromOutputSearch = false;
	}

	private void InputCaret_PositionChanged(object sender, EventArgs e)
	{
		Caret caret = (Caret)sender;

		if (!_caretPositionChangeFromInputSearch)
		{
			_inputSearchForm.Reset(caret.Offset);
		}
	}

	private void OutputCaret_PositionChanged(object sender, EventArgs e)
	{
		Caret caret = (Caret)sender;

		if (!_caretPositionChangeFromOutputSearch)
		{
			_outputSearchForm.Reset(caret.Offset);
		}
	}

	private void SetInitialValues()
	{
		isolationLevelComboBox.SelectedIndex = 0;
		inputTypeComboBox.SelectedIndex = 1;
		outputTypeComboBox.SelectedIndex = 1;

		inputTextEditorControl.Text = "where 1 = 1";
		widthTextBox.Text = "100";

		SetTextChanged(false);
	}

	private void InitializeIsolationLevelComboBox()
	{
		isolationLevelComboBox.Items.Add(new IsolationLevelComboBoxItem("Read Uncommitted", Column.IsolationLevelType.ReadUncommitted));
		isolationLevelComboBox.Items.Add(new IsolationLevelComboBoxItem("Read Committed", Column.IsolationLevelType.ReadCommitted));
		isolationLevelComboBox.Items.Add(new IsolationLevelComboBoxItem("Repeatable Read", Column.IsolationLevelType.RepeatableRead));
		isolationLevelComboBox.Items.Add(new IsolationLevelComboBoxItem("Serializable", Column.IsolationLevelType.Serializable));
	}

	private void InitializeInputTypeComboBox()
	{
		if (ConfigHandler.UseTranslation)
		{
			inputTypeComboBox.Items.Add(new ComboBoxItem(Translator.GetText("RegEx"), Column.ColumnType.RegEx));
			inputTypeComboBox.Items.Add(new ComboBoxItem(Translator.GetText("SQL"), Column.ColumnType.SQL));
			inputTypeComboBox.Items.Add(new ComboBoxItem(Translator.GetText("Constant"), Column.ColumnType.Constant));
			inputTypeComboBox.Items.Add(new ComboBoxItem(Translator.GetText("StoredProcedureName"), Column.ColumnType.StoredProcedureName));
		}
		else
		{
			inputTypeComboBox.Items.Add(new ComboBoxItem("RegEx", Column.ColumnType.RegEx));
			inputTypeComboBox.Items.Add(new ComboBoxItem("SQL", Column.ColumnType.SQL));
			inputTypeComboBox.Items.Add(new ComboBoxItem("Constant", Column.ColumnType.Constant));
			inputTypeComboBox.Items.Add(new ComboBoxItem("Stored Procedure Name", Column.ColumnType.StoredProcedureName));
		}
	}

	private void InitializeOutputTypeComboBox()
	{
		outputTypeComboBox.Items.Add(new ComboBoxItem(ColumnHelper.GetColumnTypeName(Column.ColumnType.RegEx), Column.ColumnType.RegEx));
		outputTypeComboBox.Items.Add(new ComboBoxItem(ColumnHelper.GetColumnTypeName(Column.ColumnType.SQL), Column.ColumnType.SQL));
		outputTypeComboBox.Items.Add(new ComboBoxItem(ColumnHelper.GetColumnTypeName(Column.ColumnType.Constant), Column.ColumnType.Constant));
		outputTypeComboBox.Items.Add(new ComboBoxItem(ColumnHelper.GetColumnTypeName(Column.ColumnType.StoredProcedureName), Column.ColumnType.StoredProcedureName));
		outputTypeComboBox.Items.Add(new ComboBoxItem(ColumnHelper.GetColumnTypeName(Column.ColumnType.StoredProcedureParameter), Column.ColumnType.StoredProcedureParameter));
		outputTypeComboBox.Items.Add(new ComboBoxItem(ColumnHelper.GetColumnTypeName(Column.ColumnType.LogParameter), Column.ColumnType.LogParameter));
	}

	private void SetIsolationLevelComboBox(Column.IsolationLevelType isolationLevel)
	{
		foreach (IsolationLevelComboBoxItem item in isolationLevelComboBox.Items)
		{
			if (item.IsolationLevel == isolationLevel)
			{
				isolationLevelComboBox.SelectedItem = item;
				break;
			}
		}
	}

	private void SetOutputTypeComboBox(Column.ColumnType outputType)
	{
		foreach (ComboBoxItem item in outputTypeComboBox.Items)
		{
			if (item.ColumnType == outputType)
			{
				outputTypeComboBox.SelectedItem = item;
				break;
			}
		}
	}

	private void SetInputTypeComboBox(Column.ColumnType inputType)
	{
		foreach (ComboBoxItem item in inputTypeComboBox.Items)
		{
			if (item.ColumnType == inputType)
			{
				inputTypeComboBox.SelectedItem = item;
				break;
			}
		}
	}

	private void SetTextChanged(bool value)
	{
		_textChanged = value;

		string title = "Custom Column";

		if (ConfigHandler.UseTranslation)
		{
			title = Translator.GetText("CustomColumn");
		}

		if (value)
		{
			Text = string.Format("{0} *", title);
		}
		else
		{
			Text = title;
		}
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		Save();
	}

	private bool Save()
	{
		if (UniqueName(nameTextBox.Text) && ValidName(nameTextBox.Text.ToLower()) && ValidWidth(widthTextBox.Text) && ValidInput(inputTextEditorControl.Text) && ValidOutput(outputTextEditorControl.Text))
		{
			if (_textChanged)
			{
				CheckCLR();
				DialogResult = DialogResult.OK;
			}
			else
			{
				DialogResult = DialogResult.Cancel;
			}

			SetTextChanged(false);
			return true;
		}

		return false;
	}

	private void CheckCLR()
	{
		if (!ConfigHandler.ClrDeployed && (ConfigHandler.ColumnTypeClrDependant(((ComboBoxItem)inputTypeComboBox.SelectedItem).ColumnType) || ConfigHandler.ColumnTypeClrDependant(((ComboBoxItem)outputTypeComboBox.SelectedItem).ColumnType)))
		{
			enabledCheckBox.Checked = false;
		}
	}

	private bool UniqueName(string keyName)
	{
		foreach (Column item in ColumnHelper.ColumnCollection.Columns)
		{
			if (keyName.ToLower() == item.Name.ToLower() && _initialNameValue != keyName.ToLower())
			{
				string text = "Another Column with the same name already exists.\r\n\r\nColumn names must be unique.";

				if (ConfigHandler.UseTranslation)
				{
					text = Translator.GetText("uniqueColumn");
				}

				OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				nameTextBox.Focus();
				return false;
			}
		}

		return true;
	}

	private bool ValidName(string keyName)
	{
		if (keyName == "")
		{
			string text = "\"Name\" can't be empty.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("emptyName");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			nameTextBox.Focus();
			return false;
		}

		if (keyName == "id" || keyName == "filename" || keyName == "type" || keyName == "textdata" || keyName == "spid" || keyName == "duration" || keyName == "starttime" || keyName == "reads" || keyName == "writes" || keyName == "cpu" || keyName == "rows" || IsNameStatisticsName(keyName))
		{
			string text = "Invalid \"Name\".";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("invalidName");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			nameTextBox.Focus();
			return false;
		}

		return true;
	}

	private static bool IsNameStatisticsName(string name)
	{
		List<string> stringList = new List<string>();
		stringList.InsertRange(stringList.Count, new[] { "totalcount", "minduration", "maxduration", "avgduration", "devduration", "varduration", "sumduration", "minreads", "maxreads", "avgreads", "devreads", "varreads", "sumreads", "minwrites", "maxwrites", "avgwrites", "devwrites", "varwrites", "sumwrites", "mincpu", "maxcpu", "avgcpu", "devcpu", "varcpu", "sumcpu", "minrows", "maxrows", "avgrows", "devrows", "varrows", "sumrows" });

		return stringList.Contains(name);
	}

	private bool ValidWidth(string width)
	{
		if (width == "")
		{
			string text = "\"Width\" can't be empty.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("emptyWidth");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			widthTextBox.Focus();
			return false;
		}

		int check;

		try
		{
			check = Convert.ToInt32(width);
		}
		catch
		{
			string text = "\"Width\" value is not a valid number.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("widthNotValidNumber");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			widthTextBox.Focus();
			return false;
		}

		if (check < 20 || check > 1000)
		{
			string text = "\"Width\" value must be between 20 and 1000.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("widthBetween");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			widthTextBox.Focus();
			return false;
		}

		return true;
	}

	private bool ValidInput(string input)
	{
		if (input == "" && ((ComboBoxItem)inputTypeComboBox.SelectedItem).ColumnType != Column.ColumnType.Constant)
		{
			string text = "Input can't be empty.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("emptyInput");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			inputTextEditorControl.Focus();
			return false;
		}

		if (input.Length > 4000)
		{
			string text = "Input length exceeds 4000 characters.\r\nMaximum length allowed is 4000 characters.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("inputLength");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			inputTextEditorControl.Focus();
			return false;
		}

		return true;
	}

	private bool ValidOutput(string output)
	{
		if (output == "" && ((ComboBoxItem)outputTypeComboBox.SelectedItem).ColumnType != Column.ColumnType.StoredProcedureName && ((ComboBoxItem)outputTypeComboBox.SelectedItem).ColumnType != Column.ColumnType.Constant)
		{
			string text = "Output can't be empty.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("emptyOutput");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			outputTextEditorControl.Focus();
			return false;
		}

		if (output.Length > 4000)
		{
			string text = "Output length exceeds 4000 characters.\r\nMaximum length allowed is 4000 characters.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("outputLength");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			outputTextEditorControl.Focus();
			return false;
		}

		return true;
	}

	private void NameTextBox_TextChanged(object sender, EventArgs e)
	{
		SetTextChanged(true);
	}

	private void HandleColumnForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		_inputSearchForm.Hide();
		_outputSearchForm.Hide();

		if (_textChanged)
		{
			string text = "Save changes?";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("saveChanges");
			}

			DialogResult result = OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

			if (result.ToString() == "Yes")
			{
				bool closeWindow = Save();

				if (!closeWindow)
				{
					e.Cancel = true;
				}
			}
			else if (result.ToString() == "Cancel")
			{
				e.Cancel = true;
			}
		}
	}

	private void HandleInputComboBoxChanged()
	{
		if (((ComboBoxItem)inputTypeComboBox.SelectedItem).ColumnType == Column.ColumnType.SQL)
		{
			inputTextEditorControl.SetHighlighting("SQL");
			inputTextEditorControl.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("SQL");
		}
		else
		{
			inputTextEditorControl.SetHighlighting("Default");
			inputTextEditorControl.Document.HighlightingStrategy = HighlightingManager.Manager.DefaultHighlighting;
		}

		if (!ConfigHandler.ClrDeployed && (ConfigHandler.ColumnTypeClrDependant(((ComboBoxItem)inputTypeComboBox.SelectedItem).ColumnType)))
		{
			clrNotAvailableIntputLabel.Visible = true;
		}
		else
		{
			clrNotAvailableIntputLabel.Visible = false;
		}
	}

	private void HandleOutputComboBoxChanged()
	{
		if (((ComboBoxItem)outputTypeComboBox.SelectedItem).ColumnType == Column.ColumnType.SQL)
		{
			outputTextEditorControl.SetHighlighting("SQL");
			outputTextEditorControl.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("SQL");
		}
		else
		{
			outputTextEditorControl.SetHighlighting("Default");
			outputTextEditorControl.Document.HighlightingStrategy = HighlightingManager.Manager.DefaultHighlighting;
		}

		if (!ConfigHandler.ClrDeployed && (ConfigHandler.ColumnTypeClrDependant(((ComboBoxItem)outputTypeComboBox.SelectedItem).ColumnType)))
		{
			clrNotAvailableOutputLabel.Visible = true;
		}
		else
		{
			clrNotAvailableOutputLabel.Visible = false;
		}
	}

	private void InputTextEditorControl_TextChanged(object sender, EventArgs e)
	{
		SetTextChanged(true);

		_inputSearchForm.SetSearchText(inputTextEditorControl.Text);
		_inputSearchForm.Reset(inputTextEditorControl.ActiveTextAreaControl.Caret.Offset);
	}

	private void OutputTextEditorControl_TextChanged(object sender, EventArgs e)
	{
		SetTextChanged(true);

		_outputSearchForm.SetSearchText(outputTextEditorControl.Text);
		_outputSearchForm.Reset(outputTextEditorControl.ActiveTextAreaControl.Caret.Offset);
	}

	private void InputToolStripMenuItemUndo_Click(object sender, EventArgs e)
	{
		inputTextEditorControl.Undo();
	}

	private void InputToolStripMenuItemRedo_Click(object sender, EventArgs e)
	{
		inputTextEditorControl.Redo();
	}

	private void InputToolStripMenuItemCut_Click(object sender, EventArgs e)
	{
		inputTextEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(sender, e);
	}

	private void InputToolStripMenuItemCopy_Click(object sender, EventArgs e)
	{
		inputTextEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(sender, e);
	}

	private void InputToolStripMenuItemPaste_Click(object sender, EventArgs e)
	{
		inputTextEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(sender, e);
	}

	private void InputToolStripMenuItemDelete_Click(object sender, EventArgs e)
	{
		inputTextEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Delete(sender, e);
	}

	private void InputToolStripMenuItemSelectAll_Click(object sender, EventArgs e)
	{
		SelectAll(inputTextEditorControl);
	}

	private void OutputToolStripMenuItemUndo_Click(object sender, EventArgs e)
	{
		outputTextEditorControl.Undo();
	}

	private void OutputToolStripMenuItemRedo_Click(object sender, EventArgs e)
	{
		outputTextEditorControl.Redo();
	}

	private void OutputToolStripMenuItemCut_Click(object sender, EventArgs e)
	{
		outputTextEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(sender, e);
	}

	private void OutputToolStripMenuItemCopy_Click(object sender, EventArgs e)
	{
		outputTextEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(sender, e);
	}

	private void OutputToolStripMenuItemPaste_Click(object sender, EventArgs e)
	{
		outputTextEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(sender, e);
	}

	private void OutputToolStripMenuItemDelete_Click(object sender, EventArgs e)
	{
		outputTextEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Delete(sender, e);
	}

	private void OutputToolStripMenuItemSelectAll_Click(object sender, EventArgs e)
	{
		SelectAll(outputTextEditorControl);
	}

	private static void SelectAll(TextEditorControl textEditorControl)
	{
		TextLocation startPosition = new TextLocation(0, 0);

		int textLength = textEditorControl.ActiveTextAreaControl.Document.TextLength;
		TextLocation endPosition = new TextLocation();
		endPosition.Column = textEditorControl.Document.OffsetToPosition(textLength).Column;
		endPosition.Line = textEditorControl.Document.OffsetToPosition(textLength).Line;

		textEditorControl.ActiveTextAreaControl.SelectionManager.SetSelection(startPosition, endPosition);
		textEditorControl.ActiveTextAreaControl.Caret.Position = endPosition;
	}

	private void InputContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
	{
		if (inputTextEditorControl.Document.UndoStack.CanUndo)
		{
			inputToolStripMenuItemUndo.Enabled = true;
		}
		else
		{
			inputToolStripMenuItemUndo.Enabled = false;
		}

		if (inputTextEditorControl.Document.UndoStack.CanRedo)
		{
			inputToolStripMenuItemRedo.Enabled = true;
		}
		else
		{
			inputToolStripMenuItemRedo.Enabled = false;
		}

		FillParameterMenu(inputTextEditorControl, insertToolStripMenuItem1);
	}

	private void OutputContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
	{
		if (outputTextEditorControl.Document.UndoStack.CanUndo)
		{
			outputToolStripMenuItemUndo.Enabled = true;
		}
		else
		{
			outputToolStripMenuItemUndo.Enabled = false;
		}

		if (outputTextEditorControl.Document.UndoStack.CanRedo)
		{
			outputToolStripMenuItemRedo.Enabled = true;
		}
		else
		{
			outputToolStripMenuItemRedo.Enabled = false;
		}

		FillParameterMenu(outputTextEditorControl, insertToolStripMenuItem2);
	}

	private void SplitContainer1_Paint(object sender, PaintEventArgs e)
	{
		SplitContainerGrip.PaintGrip(sender, e);
	}

	private void SplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
	{
		ConfigHandler.ColumnEditorSplitterDistance = splitContainer1.SplitterDistance.ToString();
		ConfigHandler.SaveConfig();
	}

	private void SplitContainer1_MouseUp(object sender, MouseEventArgs e)
	{
		if (splitContainer1.CanFocus)
		{
			ActiveControl = nameLabel;
		}
	}

	private void HiddenCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		SetTextChanged(true);
	}

	private static void SetFont(Font font, TextEditorControl textBox)
	{
		textBox.TextEditorProperties.Font = font;
		textBox.Font = font;
		textBox.ActiveTextAreaControl.TextArea.Refresh();
	}

	private void SetSize()
	{
		int x = Convert.ToInt32(ConfigHandler.ColumnEditorWindowSize.Split(';')[0]);
		int y = Convert.ToInt32(ConfigHandler.ColumnEditorWindowSize.Split(';')[1]);

		if (x > Screen.PrimaryScreen.Bounds.Width || y > Screen.PrimaryScreen.Bounds.Height)
		{
			WindowState = FormWindowState.Maximized;
			return;
		}

		if (x >= MinimumSize.Width && y >= MinimumSize.Height)
		{
			Size = new Size(x, y);
		}

		splitContainer1.SplitterDistance = Convert.ToInt32(ConfigHandler.ColumnEditorSplitterDistance);
	}

	private void HandleColumnForm_Resize(object sender, EventArgs e)
	{
		if (Width < MinimumSize.Width || Width == 0)
		{
			return;
		}

		if (WindowState == FormWindowState.Maximized)
		{
			splitContainer1.SplitterDistance = Convert.ToInt32(ConfigHandler.ColumnEditorSplitterDistance);
		}

		splitContainer1.SplitterDistance++;
		splitContainer1.SplitterDistance--;
		splitContainer1.Invalidate();

		ConfigHandler.ColumnEditorWindowSize = string.Format("{0}; {1}", Size.Width, Size.Height);
		ConfigHandler.SaveConfig();
	}

	private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void FontToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			fontDialog1.Font = inputTextEditorControl.TextEditorProperties.Font;
			fontDialog1.ShowDialog();

			if (fontDialog1.Font.Bold || fontDialog1.Font.Italic)
			{
				if (ConfigHandler.UseTranslation)
				{
					OutputHandler.Show(Translator.GetText("fontsNotSupported"), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					OutputHandler.Show("Bold and Italic fonts are not supported.", GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

				return;
			}

			SetFont(fontDialog1.Font, inputTextEditorControl);
			SetFont(fontDialog1.Font, outputTextEditorControl);

			string familyName = fontDialog1.Font.FontFamily.Name;
			string size = fontDialog1.Font.Size.ToString();

			ConfigHandler.EditorFontFamily = familyName;
			ConfigHandler.EditorFontSize = size;
			ConfigHandler.SaveConfig();
		}
		catch
		{
			if (ConfigHandler.UseTranslation)
			{
				OutputHandler.Show(string.Format(Translator.GetText("newFonts"), GenericHelper.ApplicationName), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				OutputHandler.Show(string.Format("New fonts added to Windows while {0} is running can't be added due to an error in Windows.\r\n\r\nTo add this font, restart {0}.", GenericHelper.ApplicationName), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}

	private void EnabledCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		SetTextChanged(true);
	}

	private void IsolationLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		SetTextChanged(true);
	}

	private void InputTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		SetTextChanged(true);
		HandleInputComboBoxChanged();
	}

	private void OutputTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		SetTextChanged(true);
		HandleOutputComboBoxChanged();
	}

	private class IsolationLevelComboBoxItem
	{
		private readonly string Text;
		public readonly Column.IsolationLevelType IsolationLevel;

		public IsolationLevelComboBoxItem(string text, Column.IsolationLevelType isolationLevel)
		{
			Text = text;
			IsolationLevel = isolationLevel;
		}

		public override string ToString()
		{
			return Text;
		}
	}

	private class ComboBoxItem
	{
		private readonly string Text;
		public readonly Column.ColumnType ColumnType;

		public ComboBoxItem(string text, Column.ColumnType columnType)
		{
			Text = text;
			ColumnType = columnType;
		}

		public override string ToString()
		{
			return Text;
		}
	}

	private void SearchToolStripMenuItem_Click(object sender, EventArgs e)
	{
		OutputSearch();
	}

	private void SearchToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		InputSearch();
	}

	private static void FillParameterMenu(TextEditorControl textBox, ToolStripMenuItem parameterToolStripMenuItem)
	{
		parameterToolStripMenuItem.DropDownItems.Clear();

		CreateParameterMenuItem(textBox, parameterToolStripMenuItem, "{SessionId}");

		if (ColumnHelper.ColumnCollection.Parameters.Count > 0)
		{
			parameterToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
		}

		foreach (Parameter parameter in ColumnHelper.ColumnCollection.Parameters)
		{
			CreateParameterMenuItem(textBox, parameterToolStripMenuItem, parameter.Name);
		}
	}

	private static void CreateParameterMenuItem(TextEditorControl textBox, ToolStripMenuItem parameterToolStripMenuItem, string name)
	{
		ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
		toolStripMenuItem.Text = name;
		toolStripMenuItem.Click += delegate
		{
			ParameterMenuItem_Click(textBox, name);
		};

		parameterToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
	}

	private static void ParameterMenuItem_Click(TextEditorControl textBox, string name)
	{
		string selectedText = textBox.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;

		if (textBox.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected)
		{
			textBox.ActiveTextAreaControl.Caret.Position = textBox.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].StartPosition;
		}

		textBox.Document.Replace(textBox.ActiveTextAreaControl.TextArea.Caret.Offset, selectedText.Length, name);
		textBox.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();
		textBox.ActiveTextAreaControl.Caret.Position = textBox.Document.OffsetToPosition(textBox.ActiveTextAreaControl.TextArea.Caret.Offset + name.Length);
		textBox.ActiveTextAreaControl.TextArea.Refresh();
	}

	private void WidthTextBox_TextChanged(object sender, EventArgs e)
	{
		SetTextChanged(true);
	}

	private void SearchToolStripMenuItem2_Click(object sender, EventArgs e)
	{
		if (_focusedTextEditorControl == "input")
		{
			InputSearch();
		}
		else
		{
			OutputSearch();
		}
	}

	private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
	{
		GetFocusedTextEditorControl().Undo();
	}

	private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
	{
		GetFocusedTextEditorControl().Redo();
	}

	private void CutToolStripMenuItem_Click(object sender, EventArgs e)
	{
		GetFocusedTextEditorControl().ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(sender, e);
	}

	private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
	{
		GetFocusedTextEditorControl().ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(sender, e);
	}

	private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
	{
		GetFocusedTextEditorControl().ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(sender, e);
	}

	private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
	{
		GetFocusedTextEditorControl().ActiveTextAreaControl.TextArea.ClipboardHandler.Delete(sender, e);
	}

	private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SelectAll(GetFocusedTextEditorControl());
	}

	private void InputTextEditorControl_GotFocus(object sender, EventArgs e)
	{
		_focusedTextEditorControl = "input";
		editToolStripMenuItem.Enabled = true;
	}

	private void OutputTextEditorControl_GotFocus(object sender, EventArgs e)
	{
		_focusedTextEditorControl = "output";
		editToolStripMenuItem.Enabled = true;
	}

	private void TextEditorControl_LostFocus(object sender, EventArgs e)
	{
		_focusedTextEditorControl = "";
		editToolStripMenuItem.Enabled = false;
	}

	private TextEditorControl GetFocusedTextEditorControl()
	{
		if (_focusedTextEditorControl == "input")
		{
			return inputTextEditorControl;
		}
		else
		{
			return outputTextEditorControl;
		}
	}

	private void EditToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
	{
		if (GetFocusedTextEditorControl().Document.UndoStack.CanUndo)
		{
			undoToolStripMenuItem.Enabled = true;
		}
		else
		{
			undoToolStripMenuItem.Enabled = false;
		}

		if (GetFocusedTextEditorControl().Document.UndoStack.CanRedo)
		{
			redoToolStripMenuItem.Enabled = true;
		}
		else
		{
			redoToolStripMenuItem.Enabled = false;
		}

		FillParameterMenu(GetFocusedTextEditorControl(), insertParameterToolStripMenuItem);
	}
}
