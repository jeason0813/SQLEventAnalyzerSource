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
using System.IO;
using System.Threading;
using System.Windows.Forms;

public partial class TraceFileSelectorUserControl : UserControl
{
	public delegate bool ShowDataViewEventHandler(bool handleColumns);
	public event ShowDataViewEventHandler ShowDataViewEvent;

	public delegate void InputHandlingBeginEventHandler();
	public event InputHandlingBeginEventHandler InputHandlingBeginEvent;

	public delegate void InputHandlingEndEventHandler();
	public event InputHandlingEndEventHandler InputHandlingEndEvent;

	private ListViewColumnSorter _listViewColumnSorter;
	private DatabaseOperation _databaseOperation;
	private string _traceFileDir;
	private List<string> _lastImportedFiles;
	private bool _importNew;
	private bool _unattended;

	public TraceFileSelectorUserControl()
	{
		InitializeComponent();
	}

	public void Initialize(DatabaseOperation databaseOperation)
	{
		InitializeDictionary();

		_databaseOperation = databaseOperation;
		_listViewColumnSorter = new ListViewColumnSorter();
		_lastImportedFiles = new List<string>();
		traceFileListView.ListViewItemSorter = _listViewColumnSorter;
		traceFileDirComboBox.GotFocus += TraceFileDirComboBox_GotFocus;

		SearchHistoryHandler.LoadItems(traceFileDirComboBox, "RecentListTraceFileDir_Selector");
	}

	public void SetImportNew(bool value)
	{
		_importNew = value;
	}

	public bool HandleUnattended(int numberOfImportFiles, string lastImportFile)
	{
		OutputHandler.WriteToLog("Importing Trace Files");

		if (numberOfImportFiles > 0) // import top N most recent Trace Files
		{
			OutputHandler.WriteToLog(string.Format("Trace Files to import: {0}", numberOfImportFiles));

			for (int i = 0; i < numberOfImportFiles; i++)
			{
				traceFileListView.Items[i].Checked = true;

				if (i == traceFileListView.Items.Count - 1)
				{
					break;
				}
			}
		}
		else if (numberOfImportFiles == 0) // import all
		{
			OutputHandler.WriteToLog("Trace Files to import: all");

			for (int i = 0; i < traceFileListView.Items.Count; i++)
			{
				traceFileListView.Items[i].Checked = true;
			}
		}
		else if (numberOfImportFiles == -1) // import all files newer than lastImportFile
		{
			OutputHandler.WriteToLog(string.Format("Trace Files to import: all newer than \"{0}\"", lastImportFile));

			for (int i = 0; i < traceFileListView.Items.Count; i++)
			{
				if (traceFileListView.Items[i].SubItems[2].Text == lastImportFile)
				{
					break;
				}
				else
				{
					traceFileListView.Items[i].Checked = true;
				}
			}
		}

		bool success = Next();

		return success;
	}

	public void HandleListView(bool unattended, string unattendedImportPath)
	{
		_unattended = unattended;

		if (!unattended)
		{
			LoadTraceFileDir();
		}
		else
		{
			traceFileDirComboBox.Text = unattendedImportPath;
		}

		PopulateTraceFileListView(true);
	}

	public void SelectControl()
	{
		traceFileListView.Select();
	}

	public bool CompressCheckedTraceFiles(int filesToKeep, string sevenZipFileName)
	{
		bool success = true;

		OutputHandler.WriteToLog(string.Format("Compressing Trace Files after import. Number of Trace Files to compress: {0}", traceFileListView.CheckedItems.Count));

		try
		{
			GenericHelper.WriteFile(sevenZipFileName, SQLEventAnalyzer.Properties.Resources._7za);
		}
		catch (Exception ex)
		{
			OutputHandler.WriteToLog(string.Format("Error writing 7za.exe in CompressCheckedTraceFiles: \"{0}\"", ex.Message));
			success = false;
		}

		if (success)
		{
			for (int i = 0; i < traceFileListView.CheckedItems.Count; i++)
			{
				string fileName = string.Format(@"{0}\{1}", _traceFileDir, traceFileListView.CheckedItems[i].SubItems[2].Text);

				try
				{
					CompressionHandler.CompressFile(fileName, string.Format(@"{0}\{1}.7z", _traceFileDir, Path.GetFileNameWithoutExtension(fileName)), sevenZipFileName);
				}
				catch (Exception ex)
				{
					success = false;

					string text = "Error compressing Trace File.\r\n\r\n{0}";

					if (ConfigHandler.UseTranslation)
					{
						text = Translator.GetText("errorCompressingTrace");
					}

					OutputHandler.Show(string.Format(text, ex.Message), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}

			if (filesToKeep > 0)
			{
				DeleteOldCompressedFiles(filesToKeep, _traceFileDir);
			}

			try
			{
				GenericHelper.DeleteFile(sevenZipFileName);
			}
			catch
			{
			}
		}

		return success;
	}

	public bool DeleteCheckedTraceFiles()
	{
		bool success = true;

		OutputHandler.WriteToLog(string.Format("Deleting Trace Files after import. Number of Trace Files to delete: {0}", traceFileListView.CheckedItems.Count));

		for (int i = 0; i < traceFileListView.CheckedItems.Count; i++)
		{
			string fileName = string.Format(@"{0}\{1}", _traceFileDir, traceFileListView.CheckedItems[i].SubItems[2].Text);

			try
			{
				GenericHelper.DeleteFile(fileName);
			}
			catch (Exception ex)
			{
				success = false;

				string text = "Error deleting Trace File.\r\n\r\n{0}";

				if (ConfigHandler.UseTranslation)
				{
					text = Translator.GetText("errorDeletingTrace");
				}

				OutputHandler.Show(string.Format(text, ex.Message), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		return success;
	}

	public void DeleteSelectedTraceFiles()
	{
		for (int i = 0; i < traceFileListView.SelectedItems.Count; i++)
		{
			string fileName = string.Format(@"{0}\{1}", _traceFileDir, traceFileListView.SelectedItems[i].SubItems[2].Text);

			try
			{
				GenericHelper.DeleteFile(fileName);
			}
			catch (Exception ex)
			{
				string text = "Error deleting Trace File.\r\n\r\n{0}";

				if (ConfigHandler.UseTranslation)
				{
					text = Translator.GetText("errorDeletingTrace");
				}

				OutputHandler.Show(string.Format(text, ex.Message), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		RefreshTraceFileListView();
	}

	public void RefreshTraceFileListView()
	{
		PopulateTraceFileListView(false);
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if ((int)keyData == 116) // F5
		{
			PopulateTraceFileListView(false);
			return true;
		}

		return base.ProcessCmdKey(ref msg, keyData);
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			traceFileCountLabel.Text = Translator.GetText("traceFileCountLabel");
			nextButton.Text = Translator.GetText("nextButton");
			markSelectedAndContinueToolStripMenuItem.Text = Translator.GetText("markSelectedAndContinueToolStripMenuItemDelete");
			traceFilesGroupBox.Text = Translator.GetText("traceFilesGroupBox");
			fileNameColumnHeader.Text = Translator.GetText("fileNameColumnHeader");
			sizeColumnHeader.Text = Translator.GetText("sizeColumnHeader");
			dateCreatedColumnHeader.Text = Translator.GetText("dateCreatedColumnHeader");
			traceFileDirectoryGroupBox.Text = Translator.GetText("traceFileDirectoryGroupBox");
			loadButton.Text = Translator.GetText("loadButton");
			deleteSelectedToolStripMenuItem.Text = Translator.GetText("deleteSelectedToolStripMenuItem");
			copyNameToClipboardToolStripMenuItem.Text = Translator.GetText("CopyToClipboard");
			selectAllToolStripMenuItem.Text = Translator.GetText("SelectAll");
			renameToolStripMenuItem.Text = Translator.GetText("Rename");
			importCheckedToolStripMenuItem.Text = Translator.GetText("importCheckedToolStripMenuItemDelete");
		}
	}

	private void LoadTraceFileDir()
	{
		string traceFileDirFromRegistry = RegistryHandler.ReadFromRegistry("ImportTraceFileDir");

		if (traceFileDirFromRegistry != "")
		{
			traceFileDirComboBox.Text = traceFileDirFromRegistry;
		}
	}

	private void SaveTraceFileDir(string traceFileDir)
	{
		RegistryHandler.SaveToRegistry("ImportTraceFileDir", traceFileDir);
		SearchHistoryHandler.AddItem(traceFileDirComboBox, traceFileDirComboBox.Text, "RecentListTraceFileDir_Selector");
	}

	private bool FireShowDataViewEvent(bool handleColumns)
	{
		bool success = true;

		if (ShowDataViewEvent != null)
		{
			success = ShowDataViewEvent(handleColumns);
		}

		return success;
	}

	private void FireInputHandlingBeginEvent()
	{
		if (InputHandlingBeginEvent != null)
		{
			InputHandlingBeginEvent();
		}
	}

	private void FireInputHandlingEndEvent()
	{
		if (InputHandlingEndEvent != null)
		{
			InputHandlingEndEvent();
		}
	}

	private void SortTraceFileListView(int columnIndex)
	{
		if (columnIndex == _listViewColumnSorter.SortColumn)
		{
			if (_listViewColumnSorter.Order == SortOrder.Ascending)
			{
				_listViewColumnSorter.Order = SortOrder.Descending;
			}
			else
			{
				_listViewColumnSorter.Order = SortOrder.Ascending;
			}
		}
		else
		{
			_listViewColumnSorter.SortColumn = columnIndex;
			_listViewColumnSorter.Order = SortOrder.Ascending;
		}

		switch (columnIndex)
		{
			case 1:
			case 3:
				_listViewColumnSorter.SortByCheckedValue = false;
				_listViewColumnSorter.SortByDecimalValue = true;
				_listViewColumnSorter.SortByDateTimeValue = false;
				break;
			case 4:
				_listViewColumnSorter.SortByCheckedValue = false;
				_listViewColumnSorter.SortByDateTimeValue = true;
				_listViewColumnSorter.SortByDecimalValue = false;
				break;
			case 0:
				_listViewColumnSorter.SortByCheckedValue = true;
				_listViewColumnSorter.SortByDecimalValue = false;
				_listViewColumnSorter.SortByDateTimeValue = false;
				break;
			case 2:
				_listViewColumnSorter.SortByCheckedValue = false;
				_listViewColumnSorter.SortByDecimalValue = false;
				_listViewColumnSorter.SortByDateTimeValue = false;
				break;
		}

		traceFileListView.Sort();
	}

	private void SelectTraceFileDirButton_Click(object sender, EventArgs e)
	{
		traceFileDirDialog.SelectedPath = traceFileDirComboBox.Text;
		DialogResult result = traceFileDirDialog.ShowDialog();

		if (result.ToString() == "OK")
		{
			traceFileDirComboBox.Text = traceFileDirDialog.SelectedPath;
			PopulateTraceFileListView(false);
		}
	}

	private static string GetIcon(string fileNameExtension)
	{
		if (fileNameExtension == ".xel")
		{
			return "XEvent.png";
		}

		return "Profiler.gif";
	}

	private void PopulateTraceFileListView(bool firstRun)
	{
		if (CheckTraceFileDir(firstRun, traceFileDirComboBox.Text))
		{
			traceFileListView.Items.Clear();

			DirectoryInfo dir = new DirectoryInfo(_traceFileDir);
			FileInfo[] files = dir.GetFiles("*.*");

			Array.Sort(files, delegate (FileInfo a, FileInfo b)
			{
				return b.CreationTime.CompareTo(a.CreationTime);
			});

			for (int i = 0; i < files.Length; i++)
			{
				if (files[i].Extension == ".trc" || (files[i].Extension == ".xel" && GenericHelper.SqlServerVersion >= 11))
				{
					string[] items = { "", (i + 1).ToString(), files[i].Name, GenericHelper.FormatWithThousandSeparator(Convert.ToInt32(files[i].Length / 1024)), GenericHelper.FormatDate(files[i].CreationTime) };
					ListViewItem traceFileListViewItem = new ListViewItem(items, GetIcon(files[i].Extension));
					traceFileListView.Items.Add(traceFileListViewItem);
				}
			}
		}

		if (ConfigHandler.UseTranslation)
		{
			traceFileCountLabel.Text = string.Format("{1} {0}", traceFileListView.Items.Count, Translator.GetText("traceFileCountLabel"));
		}
		else
		{
			traceFileCountLabel.Text = string.Format("Number of Trace Files: {0}", traceFileListView.Items.Count);
		}
	}

	private bool CheckTraceFileDir(bool firstRun, string dirToCheck)
	{
		CheckDirectoryForm form = new CheckDirectoryForm();
		form.Initialize(dirToCheck);

		if (GenericHelper.IsUserInteractive())
		{
			form.ShowDialog(this);
		}

		bool directoryExist = form.GetDirectoryExist();

		if (directoryExist)
		{
			_traceFileDir = dirToCheck;

			if (!_unattended && ConfigHandler.RegistryModifyAccess)
			{
				SaveTraceFileDir(dirToCheck);
			}

			return true;
		}
		else
		{
			if (!firstRun)
			{
				string text = "Trace File Directory does not exist.\r\n\r\nIf you are reading from a remote server to a local drive, please use a UNC path and make sure the server has read access to your network share and the SQL Server Service Account has read access to the share.";

				if (ConfigHandler.UseTranslation)
				{
					text = Translator.GetText("traceFileSecurityInfo");
				}

				OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			return false;
		}
	}

	private void SetNextButton()
	{
		if (traceFileListView.CheckedItems.Count > 0)
		{
			nextButton.Enabled = true;
			importCheckedToolStripMenuItem.Enabled = true;
		}
		else
		{
			nextButton.Enabled = false;
			importCheckedToolStripMenuItem.Enabled = false;
		}
	}

	private void TraceFileListView_ColumnClick(object sender, ColumnClickEventArgs e)
	{
		SortTraceFileListView(e.Column);
	}

	private void TraceFileListView_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.A)
		{
			foreach (ListViewItem item in traceFileListView.Items)
			{
				item.Selected = true;
			}
		}
	}

	private void TraceFileListView_ItemChecked(object sender, ItemCheckedEventArgs e)
	{
		SetNextButton();
	}

	private void TraceFileDirComboBox_GotFocus(object sender, EventArgs e)
	{
		traceFileDirComboBox.SelectionStart = 0;
		traceFileDirComboBox.SelectionLength = 0;
	}

	private void NextButton_Click(object sender, EventArgs e)
	{
		Next();
	}

	private bool Next()
	{
		bool success = FillParameterForm.CheckForUniqueParameterNames();

		if (success)
		{
			success = FillParameterForm.FillMissingParameters();
		}

		if (success)
		{
			FireInputHandlingBeginEvent();
			nextButton.Enabled = false;

			bool startImport = ImportNewTraceFiles();

			if (startImport)
			{
				_lastImportedFiles.Clear();
				success = StartImport();
			}

			if (success)
			{
				_databaseOperation.SetForceRecalculateTotalRows(true);
				success = FireShowDataViewEvent(startImport);
			}

			nextButton.Enabled = true;
			FireInputHandlingEndEvent();
		}

		return success;
	}

	private static bool IsListTheSame(List<string> list1, List<string> list2)
	{
		if (list1.Count == list2.Count)
		{
			for (int i = 0; i < list1.Count; i++)
			{
				if (list1[i] != list2[i])
				{
					return false;
				}
			}

			return true;
		}

		return false;
	}

	private bool ImportNewTraceFiles()
	{
		if (_importNew)
		{
			return true;
		}

		List<string> checkedFiles = new List<string>();

		for (int i = 0; i < traceFileListView.CheckedItems.Count; i++)
		{
			string fileName = string.Format(@"{0}\{1}", _traceFileDir, traceFileListView.CheckedItems[i].SubItems[2].Text);
			checkedFiles.Add(fileName);
		}

		if (IsListTheSame(_lastImportedFiles, checkedFiles))
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	private bool StartImport()
	{
		OutputHandler.WriteToLog(string.Format("Starting import. Number of Trace Files to import: {0}", traceFileListView.CheckedItems.Count));

		ConfigHandler.ImportStartTime = DateTime.Now;

		List<ImportTraceFileInfo> importTraceFileInfoList = new List<ImportTraceFileInfo>();

		if (_traceFileDir.EndsWith("\\"))
		{
			_traceFileDir = _traceFileDir.Substring(0, _traceFileDir.Length - 1);
		}

		for (int i = traceFileListView.CheckedItems.Count - 1; i >= 0; i--)
		{
			string fileName = string.Format(@"{0}\{1}", _traceFileDir, traceFileListView.CheckedItems[i].SubItems[2].Text);
			string fileSize = traceFileListView.CheckedItems[i].SubItems[3].Text;
			importTraceFileInfoList.Add(new ImportTraceFileInfo(fileName, fileSize));
			_lastImportedFiles.Add(fileName);
		}

		ImportTraceFileForm form = new ImportTraceFileForm();
		form.Initialize(_databaseOperation, importTraceFileInfoList);

		if (GenericHelper.IsUserInteractive())
		{
			form.ShowDialog();
		}

		ConfigHandler.ImportEndTime = DateTime.Now;

		bool success = form.GetSuccess();

		OutputHandler.WriteToLog(string.Format("Import success: {0}", success));

		return success;
	}

	private void LoadButton_Click(object sender, EventArgs e)
	{
		PopulateTraceFileListView(false);
	}

	private void TraceFileSelectorUserControl_Resize(object sender, EventArgs e)
	{
		traceFileListView.Columns[2].Width = traceFileListView.Width - (traceFileListView.Columns[0].Width + traceFileListView.Columns[1].Width + traceFileListView.Columns[3].Width + traceFileListView.Columns[4].Width + 21);
	}

	private void MarkSelectedAndContinueToolStripMenuItem_Click(object sender, EventArgs e)
	{
		foreach (ListViewItem item in traceFileListView.Items)
		{
			item.Checked = false;
		}

		foreach (ListViewItem item in traceFileListView.SelectedItems)
		{
			item.Checked = true;
		}

		Next();
	}

	private void ImportCheckedToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Next();
	}

	private void TraceFileListView_ItemCheck(object sender, ItemCheckEventArgs e)
	{
		if (ModifierKeys == Keys.Shift || ModifierKeys == Keys.Control)
		{
			e.NewValue = e.CurrentValue;
		}
	}

	private void TraceFileDirComboBox_Enter(object sender, EventArgs e)
	{
		traceFileDirComboBox.SelectionStart = 0;
		traceFileDirComboBox.SelectionLength = 0;
	}

	private void TraceFileDirComboBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyData == Keys.Enter)
		{
			PopulateTraceFileListView(false);
		}
	}

	private void ContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
	{
		if (traceFileListView.SelectedItems.Count == 0)
		{
			e.Cancel = true;
		}

		if (traceFileListView.SelectedItems.Count == 1)
		{
			renameToolStripMenuItem.Enabled = true;
			copyNameToClipboardToolStripMenuItem.Enabled = true;
		}
		else
		{
			renameToolStripMenuItem.Enabled = false;
			copyNameToClipboardToolStripMenuItem.Enabled = false;
		}
	}

	private void DeleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (traceFileListView.SelectedItems.Count == 0)
		{
			return;
		}

		string text1 = "Deleted selected Trace Files?";

		if (ConfigHandler.UseTranslation)
		{
			text1 = Translator.GetText("DeleteSelectedTraceFiles");
		}

		DialogResult result = OutputHandler.Show(text1, GenericHelper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

		if (result.ToString() == "Yes")
		{
			DeleteSelectedTraceFiles();
		}
	}

	private void CopyNameToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (traceFileListView.SelectedItems.Count == 1)
		{
			Thread newThread = new Thread(ThreadMethod);
			newThread.SetApartmentState(ApartmentState.STA);

			string copy = traceFileListView.SelectedItems[0].SubItems[2].Text;
			newThread.Start(copy);
		}
	}

	private static void ThreadMethod(object text)
	{
		Clipboard.SetText(text.ToString());
	}

	private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		foreach (ListViewItem item in traceFileListView.Items)
		{
			item.Selected = true;
		}
	}

	private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (traceFileListView.SelectedItems.Count == 0)
		{
			return;
		}

		string initialName = traceFileListView.SelectedItems[0].SubItems[2].Text;
		string newName = GetName(initialName, initialName);

		if (newName != initialName && newName != null)
		{
			string source = string.Format(@"{0}\{1}", _traceFileDir, initialName);
			string dest = string.Format(@"{0}\{1}", _traceFileDir, newName);

			try
			{
				File.Move(source, dest);
			}
			catch (Exception ex)
			{
				OutputHandler.Show(ex.Message, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			PopulateTraceFileListView(false);
		}
	}

	private string GetName(string name, string initialName)
	{
		string titleText = "New name";

		if (ConfigHandler.UseTranslation)
		{
			titleText = Translator.GetText("NewName");
		}

		string groupBoxText = "Trace File";

		if (ConfigHandler.UseTranslation)
		{
			groupBoxText = Translator.GetText("TraceFile");
		}

		string valueLabelText = "Name:";

		if (ConfigHandler.UseTranslation)
		{
			valueLabelText = Translator.GetText("Name1");
		}

		GetTextForm form = new GetTextForm(titleText, groupBoxText, valueLabelText, name, -1);
		form.ShowDialog();

		string newName = form.GetText();

		if (form.SaveChanges())
		{
			bool valid = CheckForValidName(newName);

			if (!valid)
			{
				string text = "Name not valid.";

				if (ConfigHandler.UseTranslation)
				{
					text = Translator.GetText("NameNotValid");
				}

				OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);

				newName = GetName(newName, initialName);
			}
			else
			{
				bool unique = CheckForUniqueName(newName, initialName);

				if (!unique)
				{
					string text = "Names must be unique.";

					if (ConfigHandler.UseTranslation)
					{
						text = Translator.GetText("NameMustBeUnique");
					}

					OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);

					newName = GetName(newName, initialName);
				}
			}
		}

		return newName;
	}

	private static bool CheckForValidName(string name)
	{
		if (name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
		{
			return false;
		}

		return true;
	}

	private bool CheckForUniqueName(string name, string initialName)
	{
		if (name == initialName)
		{
			return true;
		}

		foreach (ListViewItem item in traceFileListView.Items)
		{
			if (item.SubItems[2].Text.ToLower().Trim() == name.ToLower())
			{
				return false;
			}
		}

		return true;
	}

	private static void DeleteOldCompressedFiles(int filesToKeep, string traceFileDir)
	{
		DirectoryInfo di = new DirectoryInfo(traceFileDir);
		FileInfo[] zipFiles = di.GetFiles(@"*.7z");

		GenericHelper.DateCompareFileInfo dayCompareFileInfo = new GenericHelper.DateCompareFileInfo();

		Array.Sort(zipFiles, dayCompareFileInfo);

		for (int i = filesToKeep; i < zipFiles.Length; i++)
		{
			try
			{
				GenericHelper.DeleteFile(zipFiles[i].FullName);
			}
			catch
			{
			}
		}
	}
}
