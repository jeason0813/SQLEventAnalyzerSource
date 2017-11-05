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
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using SortOrder = System.Windows.Forms.SortOrder;

public partial class SessionSelectorUserControl : UserControl
{
	public delegate void ShowDataViewEventHandler(bool handleColumns, bool connectToExistingSession, bool connectToCurrentSession);
	public event ShowDataViewEventHandler ShowDataViewEvent;

	public delegate void InputHandlingBeginEventHandler();
	public event InputHandlingBeginEventHandler InputHandlingBeginEvent;

	public delegate void InputHandlingEndEventHandler();
	public event InputHandlingEndEventHandler InputHandlingEndEvent;

	public delegate void NewSessionEventHandler();
	public event NewSessionEventHandler NewSessionEvent;

	private ListViewColumnSorter _listViewColumnSorter;
	private DatabaseOperation _databaseOperation;
	private List<string> _lastImportedSessions;
	private List<ImportSessionInfo> _importSessionInfoList;
	private bool _importNew;
	private bool _manuallyUseSession;

	public SessionSelectorUserControl()
	{
		InitializeComponent();
	}

	public void Initialize(DatabaseOperation databaseOperation)
	{
		InitializeDictionary();

		_databaseOperation = databaseOperation;
		_listViewColumnSorter = new ListViewColumnSorter();
		_lastImportedSessions = new List<string>();
		sessionListView.ListViewItemSorter = _listViewColumnSorter;
	}

	public bool GetManuallyUseSession()
	{
		return _manuallyUseSession;
	}

	public void SetManuallyUseSession(bool value)
	{
		_manuallyUseSession = value;
	}

	public void SetImportNew(bool value)
	{
		_importNew = value;
	}

	public void HandleListView()
	{
		PopulateSessionListView();
	}

	public void SelectControl()
	{
		sessionListView.Select();
		SetOkButton();
	}

	public void RefreshSessionListView()
	{
		PopulateSessionListView();
	}

	public List<ImportSessionInfo> GetImportSessionInfoList()
	{
		return _importSessionInfoList;
	}

	public void ConnectToSession(string sessionId)
	{
		if (_importSessionInfoList != null)
		{
			_importSessionInfoList.Clear();
		}

		CreateNewSession(sessionId, true);
		_lastImportedSessions.Add(sessionId);

		DisableColumnsNotInImportTables();

		ConfigHandler.HandleColumnsStartTime = DateTime.Now;
		ConfigHandler.HandleColumnsEndTime = DateTime.Now;

		_importNew = false;
	}

	public void ImportSessionToCurrentSession(string sessionIdToImport)
	{
		foreach (ListViewItem item in sessionListView.Items)
		{
			if (item.SubItems[2].Text == sessionIdToImport)
			{
				item.Checked = true;
			}
			else
			{
				item.Checked = false;
			}
		}

		Next();
	}

	public void UseSession(string sessionId, bool verboseMode)
	{
		foreach (ComboBoxItem item in existingSessionsComboBox.Items)
		{
			if (item.SessionId.ToLower() == sessionId.ToLower())
			{
				existingSessionsComboBox.SelectedItem = item;
				break;
			}
		}

		UseSession(verboseMode);
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if ((int)keyData == 116) // F5
		{
			PopulateSessionListView();
			return true;
		}

		return base.ProcessCmdKey(ref msg, keyData);
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			sessionCountLabel.Text = Translator.GetText("sessionCountLabel");
			nextButton.Text = Translator.GetText("nextButton");
			markSelectedAndContinueToolStripMenuItem.Text = Translator.GetText("markSelectedAndContinueToolStripMenuItemAppend");
			sessionsGroupBox.Text = Translator.GetText("sessionsGroupBox");
			sessionIdColumnHeader.Text = Translator.GetText("sessionIdColumnHeader");
			sizeColumnHeader.Text = Translator.GetText("sizeColumnHeader");
			dateCreatedColumnHeader.Text = Translator.GetText("dateCreatedColumnHeader");
			sessionsInfoGroupBox.Text = Translator.GetText("sessionsInfoGroupBox");
			okButton.Text = Translator.GetText("okButton");
			useSessionToolStripMenuItem.Text = Translator.GetText("useSessionToolStripMenuItem");
			deleteSelectedToolStripMenuItem.Text = Translator.GetText("deleteSelectedToolStripMenuItem");
			newButton.Text = Translator.GetText("newButton");
			copyNameToClipboardToolStripMenuItem.Text = Translator.GetText("CopyToClipboard");
			selectAllToolStripMenuItem.Text = Translator.GetText("SelectAll");
			renameToolStripMenuItem.Text = Translator.GetText("Rename");
			importCheckedToolStripMenuItem.Text = Translator.GetText("importCheckedToolStripMenuItemAppend");
		}
	}

	private void FillExistingSessionsComboBox(DataTable existingSessions)
	{
		string existingSessionId = GenericHelper.GetSessionIdFromTableName();

		string currentText = "Current";

		if (ConfigHandler.UseTranslation)
		{
			currentText = Translator.GetText("CurrentText");
		}

		string activeText = "Active";

		if (ConfigHandler.UseTranslation)
		{
			activeText = Translator.GetText("ActiveText");
		}

		string existingSessionSize = "0";

		for (int i = 0; i < existingSessions.Rows.Count; i++)
		{
			if (existingSessions.Rows[i]["TableName"].ToString() == existingSessionId)
			{
				int size = Convert.ToInt32(existingSessions.Rows[i]["TableSizeInKB"]) + Convert.ToInt32(existingSessions.Rows[i]["IndexSizeInKB"]);
				existingSessionSize = GenericHelper.FormatWithThousandSeparator(Convert.ToInt32(size));
				break;
			}
		}

		string existingText = string.Format("{2} - {0} ({1}, {3} KB)", existingSessionId, currentText, FormatSessionNumber(0, existingSessions.Rows.Count - 1), existingSessionSize);

		existingSessionsComboBox.Items.Clear();

		existingSessionsComboBox.Items.Add(new ComboBoxItem(existingText, false, true, existingSessionId));

		DataTable existingConnections = _databaseOperation.GetExistingConnections();

		for (int i = 0; i < existingSessions.Rows.Count; i++)
		{
			if (existingSessions.Rows[i]["TableName"].ToString() != existingSessionId)
			{
				string sessionText = existingSessions.Rows[i]["TableName"].ToString();
				bool active = false;

				foreach (DataRow existingConnection in existingConnections.Rows)
				{
					if (existingConnection["sessionid"].ToString() == existingSessions.Rows[i]["TableName"].ToString())
					{
						sessionText = string.Format("{0} ({1})", existingSessions.Rows[i]["TableName"], activeText);
						active = true;
						break;
					}
				}

				sessionText = string.Format("{1} - {0}", sessionText, FormatSessionNumber(i + 1, existingSessions.Rows.Count - 1));

				existingSessionsComboBox.Items.Add(new ComboBoxItem(sessionText, active, false, existingSessions.Rows[i]["TableName"].ToString()));
			}
		}

		existingSessionsComboBox.SelectedIndex = 0;
	}

	private static string FormatSessionNumber(int number, int totalNumberOfRows)
	{
		string sessionNumber = number.ToString();
		sessionNumber = sessionNumber.PadLeft(totalNumberOfRows.ToString().Length);
		return sessionNumber;
	}

	private void FireShowDataViewEvent(bool handleColumns, bool connectToExistingSession, bool connectToCurrentSession)
	{
		if (ShowDataViewEvent != null)
		{
			ShowDataViewEvent(handleColumns, connectToExistingSession, connectToCurrentSession);
		}
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

	private void FireNewSessionEvent()
	{
		if (NewSessionEvent != null)
		{
			NewSessionEvent();
		}
	}

	private void SortSessionListView(int columnIndex)
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

		sessionListView.Sort();
	}

	private void SetCheckedListViewItems(string checkSessionId, ListViewItem sessionListViewItem)
	{
		if (_importSessionInfoList != null)
		{
			foreach (ImportSessionInfo importSessionInfo in _importSessionInfoList)
			{
				if (checkSessionId == importSessionInfo.SessionId)
				{
					sessionListViewItem.Checked = true;
					break;
				}
			}
		}
	}

	private void PopulateSessionListView()
	{
		sessionListView.Items.Clear();

		DataTable existingSessions = _databaseOperation.GetSessionInfo(this);

		string existingSessionId = GenericHelper.GetSessionIdFromTableName();

		for (int i = 0; i < existingSessions.Rows.Count; i++)
		{
			if (existingSessions.Rows[i]["TableName"].ToString() != existingSessionId)
			{
				int size = Convert.ToInt32(existingSessions.Rows[i]["TableSizeInKB"]) + Convert.ToInt32(existingSessions.Rows[i]["IndexSizeInKB"]);
				string[] items = { "", (i + 1).ToString(), existingSessions.Rows[i]["TableName"].ToString(), GenericHelper.FormatWithThousandSeparator(Convert.ToInt32(size)), GenericHelper.FormatDate(Convert.ToDateTime(existingSessions.Rows[i]["CreateDate"])) };
				ListViewItem sessionListViewItem = new ListViewItem(items, "database.png");

				SetCheckedListViewItems(existingSessions.Rows[i]["TableName"].ToString(), sessionListViewItem);

				sessionListView.Items.Add(sessionListViewItem);
			}
		}

		if (ConfigHandler.UseTranslation)
		{
			sessionCountLabel.Text = string.Format("{1} {0}", sessionListView.Items.Count, Translator.GetText("sessionCountLabel"));
		}
		else
		{
			sessionCountLabel.Text = string.Format("Number of Sessions: {0}", sessionListView.Items.Count);
		}

		FillExistingSessionsComboBox(existingSessions);
	}

	private void SetNextButton()
	{
		if (sessionListView.CheckedItems.Count > 0)
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

	private void SessionListView_ColumnClick(object sender, ColumnClickEventArgs e)
	{
		SortSessionListView(e.Column);
	}

	private void SessionListView_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.A)
		{
			foreach (ListViewItem item in sessionListView.Items)
			{
				item.Selected = true;
			}
		}
	}

	private void SessionListView_ItemChecked(object sender, ItemCheckedEventArgs e)
	{
		SetNextButton();
	}

	private void NextButton_Click(object sender, EventArgs e)
	{
		Next();
	}

	private static bool IsCustomColumnsWindowClosed(bool modifyCustomColumns)
	{
		if (ConfigHandler.CustomColumnsFormShown && modifyCustomColumns)
		{
			string text = "\"Custom Columns\" window must be closed before continuing.";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("closeCCWindow");
			}

			OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			return false;
		}

		return true;
	}

	private void Next()
	{
		FireInputHandlingBeginEvent();
		nextButton.Enabled = false;

		bool startImport = ImportNewSessions();

		if (startImport)
		{
			bool success = IsCustomColumnsWindowClosed(true);

			if (success)
			{
				success = FillParameterForm.CheckForUniqueParameterNames();
			}

			if (success)
			{
				success = FillParameterForm.FillMissingParameters();
			}

			if (success)
			{
				_lastImportedSessions.Clear();
				StartImport();

				DisableColumnsNotInImportTables();

				_databaseOperation.SetForceRecalculateTotalRows(true);
				FireShowDataViewEvent(true, false, false);
			}
		}

		nextButton.Enabled = true;
		FireInputHandlingEndEvent();
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

	private bool ImportNewSessions()
	{
		if (_importNew)
		{
			return true;
		}

		List<string> checkedSessions = new List<string>();

		for (int i = 0; i < sessionListView.CheckedItems.Count; i++)
		{
			string sessionId = sessionListView.CheckedItems[i].SubItems[2].Text;
			checkedSessions.Add(sessionId);
		}

		if (IsListTheSame(_lastImportedSessions, checkedSessions))
		{
			string text = "Import the same selection again?";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("ImportSameAgain");
			}

			DialogResult overwrite = OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

			if (overwrite.ToString() == "Yes")
			{
				return true;
			}

			return false;
		}
		else
		{
			return true;
		}
	}

	private void StartImport()
	{
		_importSessionInfoList = new List<ImportSessionInfo>();

		for (int i = 0; i < sessionListView.CheckedItems.Count; i++)
		{
			string sessionId = sessionListView.CheckedItems[i].SubItems[2].Text;
			string sessionSize = sessionListView.CheckedItems[i].SubItems[3].Text;
			_importSessionInfoList.Add(new ImportSessionInfo(sessionId, sessionSize));
			_lastImportedSessions.Add(sessionId);
		}

		if (!ConfigHandler.TempTableCreated)
		{
			_databaseOperation.CreateTempTable();
		}
	}

	private void DisableColumnsNotInImportTables()
	{
		List<int> indexesToDisable = new List<int>();

		for (int i = 0; i < _lastImportedSessions.Count; i++)
		{
			List<string> nonDefaultColumnNames = ImportSessionForm.GetNonDefaultColumns(_databaseOperation, _lastImportedSessions[i]);

			for (int j = 0; j < ColumnHelper.ColumnCollection.Columns.Count; j++)
			{
				if (ColumnHelper.ColumnCollection.Columns[j].Enabled)
				{
					bool found = false;

					foreach (string nonDefaultColumnName in nonDefaultColumnNames)
					{
						if (ColumnHelper.ColumnCollection.Columns[j].Name == nonDefaultColumnName)
						{
							found = true;
							break;
						}
					}

					if (!found)
					{
						indexesToDisable.Add(j);
					}
				}
			}
		}

		foreach (int indexToDisable in indexesToDisable)
		{
			ColumnHelper.ColumnCollection.Columns[indexToDisable].Enabled = false;
		}
	}

	private void SessionSelectorUserControl_Resize(object sender, EventArgs e)
	{
		sessionListView.Columns[2].Width = sessionListView.Width - (sessionListView.Columns[0].Width + sessionListView.Columns[1].Width + sessionListView.Columns[3].Width + sessionListView.Columns[4].Width + 21);
	}

	private void MarkSelectedAndContinueToolStripMenuItem_Click(object sender, EventArgs e)
	{
		foreach (ListViewItem item in sessionListView.Items)
		{
			item.Checked = false;
		}

		foreach (ListViewItem item in sessionListView.SelectedItems)
		{
			item.Checked = true;
		}

		Next();
	}

	private void ImportCheckedToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Next();
	}

	private void SessionListView_ItemCheck(object sender, ItemCheckEventArgs e)
	{
		if (ModifierKeys == Keys.Shift || ModifierKeys == Keys.Control)
		{
			e.NewValue = e.CurrentValue;
		}
	}

	private void ContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
	{
		if (sessionListView.SelectedItems.Count == 0)
		{
			e.Cancel = true;
		}

		if (sessionListView.SelectedItems.Count == 1)
		{
			useSessionToolStripMenuItem.Enabled = true;
			renameToolStripMenuItem.Enabled = true;
			copyNameToClipboardToolStripMenuItem.Enabled = true;
		}
		else
		{
			useSessionToolStripMenuItem.Enabled = false;
			renameToolStripMenuItem.Enabled = false;
			copyNameToClipboardToolStripMenuItem.Enabled = false;
		}
	}

	private class ComboBoxItem
	{
		private readonly string _text;
		public readonly bool Active;
		public readonly bool Current;
		public readonly string SessionId;

		public ComboBoxItem(string text, bool active, bool current, string sessionId)
		{
			_text = text;
			Active = active;
			Current = current;
			SessionId = sessionId;
		}

		public override string ToString()
		{
			return _text;
		}
	}

	private void UseSession(bool verboseMode)
	{
		ComboBoxItem selectedItem = (ComboBoxItem)existingSessionsComboBox.SelectedItem;

		if (!IsCustomColumnsWindowClosed(!selectedItem.Current))
		{
			return;
		}

		bool connectToSession = false;

		if (selectedItem.Active && !verboseMode)
		{
			string text = "The chosen Session is currently active by another instance of {0}.\r\nUse Session?";

			if (ConfigHandler.UseTranslation)
			{
				text = Translator.GetText("OverTakeSession");
			}

			DialogResult result = OutputHandler.Show(string.Format(text, GenericHelper.ApplicationName), GenericHelper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

			if (result.ToString() == "Yes")
			{
				connectToSession = true;
			}
		}
		else
		{
			connectToSession = true;
		}

		if (connectToSession)
		{
			if (!selectedItem.Current)
			{
				ConnectToSession(selectedItem.SessionId);
				_databaseOperation.SetForceRecalculateTotalRows(true);
				FireShowDataViewEvent(false, true, false);
			}
			else
			{
				FireShowDataViewEvent(false, false, true);
			}
		}

		_manuallyUseSession = false;
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		UseSession(false);
		_manuallyUseSession = true;
	}

	private void CreateNewConnectionAndKillOld(string newSessionId)
	{
		string previousConnectionString = _databaseOperation.GetConnectionString();
		SqlConnectionStringBuilder newConnString = new SqlConnectionStringBuilder(previousConnectionString);
		newConnString.ApplicationName = string.Format("{0} - {1}", GenericHelper.ApplicationName, newSessionId);
		_databaseOperation.ChangeConnection(newConnString.ToString());

		DatabaseOperation previousConnection = new DatabaseOperation();
		previousConnection.ChangeConnection(previousConnectionString);

		string previousSessionId = GenericHelper.GetSessionIdFromTableName();
		GenericHelper.KillPreviousConnection(previousConnection, previousSessionId);
	}

	private void UseSessionToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string sessionId = sessionListView.SelectedItems[0].SubItems[2].Text;
		UseSession(sessionId, false);
		_manuallyUseSession = true;
	}

	private void ExistingSessionsComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		SetOkButton();
	}

	private void SetOkButton()
	{
		if (((ComboBoxItem)existingSessionsComboBox.SelectedItem).Current && !ConfigHandler.TempTableCreated)
		{
			okButton.Enabled = false;
		}
		else
		{
			okButton.Enabled = true;
		}
	}

	private void DeleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (sessionListView.SelectedItems.Count == 0)
		{
			return;
		}

		string text1 = "Deleted selected Sessions?";

		if (ConfigHandler.UseTranslation)
		{
			text1 = Translator.GetText("DeleteSelectedSessions");
		}

		DialogResult result = OutputHandler.Show(text1, GenericHelper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

		if (result.ToString() == "Yes")
		{
			for (int i = 0; i < sessionListView.SelectedItems.Count; i++)
			{
				string sessionId = sessionListView.SelectedItems[i].SubItems[2].Text;

				try
				{
					_databaseOperation.DeleteSession(sessionId);
				}
				catch (Exception ex)
				{
					string text = "Error deleting Session.\r\n\r\n{0}";

					if (ConfigHandler.UseTranslation)
					{
						text = Translator.GetText("errorDeletingSession");
					}

					OutputHandler.Show(string.Format(text, ex.Message), GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}

			RefreshSessionListView();
		}
	}

	private void NewButton_Click(object sender, EventArgs e)
	{
		string titleText = "Create new Session";

		if (ConfigHandler.UseTranslation)
		{
			titleText = Translator.GetText("NewSession2");
		}

		string newName = GetName("", "", titleText);

		if (newName != null)
		{
			okButton.Enabled = false;
			CreateNewSession(newName, false);
		}

		_manuallyUseSession = true;
	}

	private void CreateNewSession(string newSessionId, bool connectToExistingSession)
	{
		CreateNewConnectionAndKillOld(newSessionId);

		GenericHelper.TempTableName = string.Format("TraceData_{0}", newSessionId);

		if (connectToExistingSession)
		{
			ConfigHandler.TempTableCreated = true;
		}
		else
		{
			ConfigHandler.TempTableCreated = false;
		}

		PopulateSessionListView();

		_lastImportedSessions.Clear();
		FireNewSessionEvent();
	}

	private void CopyNameToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (sessionListView.SelectedItems.Count == 1)
		{
			Thread newThread = new Thread(ThreadMethod);
			newThread.SetApartmentState(ApartmentState.STA);

			string copy = sessionListView.SelectedItems[0].SubItems[2].Text;
			newThread.Start(copy);
		}
	}

	private static void ThreadMethod(object text)
	{
		Clipboard.SetText(text.ToString());
	}

	private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		foreach (ListViewItem item in sessionListView.Items)
		{
			item.Selected = true;
		}
	}

	private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (sessionListView.SelectedItems.Count == 0)
		{
			return;
		}

		string titleText = "New name";

		if (ConfigHandler.UseTranslation)
		{
			titleText = Translator.GetText("NewName");
		}

		string initialName = sessionListView.SelectedItems[0].SubItems[2].Text;
		string newName = GetName(initialName, initialName, titleText);

		if (newName != initialName && newName != null)
		{
			_databaseOperation.RenameTable(initialName, newName);

			PopulateSessionListView();
		}
	}

	private string GetName(string name, string initialName, string titleText)
	{
		string groupBoxText = "Session";

		if (ConfigHandler.UseTranslation)
		{
			groupBoxText = Translator.GetText("Session");
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
			bool unique = CheckForUniqueName(newName, initialName);

			if (!unique)
			{
				string text = "Names must be unique.";

				if (ConfigHandler.UseTranslation)
				{
					text = Translator.GetText("NameMustBeUnique");
				}

				OutputHandler.Show(text, GenericHelper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);

				newName = GetName(newName, initialName, titleText);
			}
		}

		return newName;
	}

	private bool CheckForUniqueName(string name, string initialName)
	{
		if (name == initialName)
		{
			return true;
		}

		bool exists = _databaseOperation.DoesTempTableExist(name);

		if (exists)
		{
			return false;
		}

		return true;
	}
}
