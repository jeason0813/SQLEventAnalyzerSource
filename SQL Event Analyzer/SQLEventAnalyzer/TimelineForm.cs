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
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

public partial class TimelineForm : Form
{
	private DateTime _firstStartTime;
	private readonly DataTable _dataTable;
	private int _graphWidth;
	private int _barHeight;
	private int _chartLengthInUnits;
	private int _previousHorizontalZoomBarValue;
	private int _previousVerticalZoomBarValue;
	private int _unitsBetweenDividers;
	private float _pixelsPerUnit;
	private float _zoomHorizontalValue;
	private float _idStringWidth;
	private float _fontHeight;
	private string _textData;
	private Bitmap _bmp;
	private PictureBox _pictureBox;
	private PictureBox _pictureBoxSpacing;

	private const int _distanceBetweenDividers = 50;  // 50 is the minimum size before the text labels covers eachother

	public TimelineForm(DataTable dataTable)
	{
		InitializeComponent();
		_dataTable = dataTable; // input data must be sorted by StartTime
		Initialize();
	}

	protected override void OnLoad(EventArgs args)
	{
		if (Site == null || (Site != null && !Site.DesignMode))
		{
			base.OnLoad(args);
			Application.Idle += OnLoaded;
		}
	}

	private void OnLoaded(object sender, EventArgs args)
	{
		Application.Idle -= OnLoaded;
		SetFocus();
	}

	private void Initialize()
	{
		InitializeDictionary();
		Text = GenericHelper.ApplicationName;
		SetSize();

		MinimumSize = new Size(689, 394); // error in .NET

		ClearValueLabels();

		if (_dataTable.Rows.Count > 0)
		{
			InitializeColumnComboBox();
			Start();
			barLabelsComboBox.SelectedIndexChanged += BarLabelsComboBox_SelectedIndexChanged;
		}
		else
		{
			saveToolStripMenuItem.Enabled = false;
			fitToScreenToolStripMenuItem.Enabled = false;
			zoomHorizontalTrackBar.Enabled = false;
			zoomVerticalTrackBar.Enabled = false;
			viewBarLabelsToolStripMenuItem.Enabled = false;
			barLabelsComboBox.Enabled = false;
			showHiddenCheckBox.Enabled = false;
			sortAlphabeticallyCheckBox.Enabled = false;
		}
	}

	private void Start()
	{
		_previousHorizontalZoomBarValue = zoomHorizontalTrackBar.Value;
		_previousVerticalZoomBarValue = zoomVerticalTrackBar.Value;

		AnalyseData(_dataTable);
		SetHorizontalValues();
		DrawGraph();
	}

	private void InitializeDictionary()
	{
		if (ConfigHandler.UseTranslation)
		{
			dataGroupBox.Text = Translator.GetText("DataMouse");
			viewToolStripMenuItem.Text = Translator.GetText("viewToolStripMenuItem");
			fileToolStripMenuItem.Text = Translator.GetText("fileToolStripMenuItem");
			closeToolStripMenuItem.Text = Translator.GetText("closeToolStripMenuItem");
			relativeStartTimeLabel.Text = Translator.GetText("relativeStartTimeLabel");
			fitToScreenToolStripMenuItem.Text = Translator.GetText("fitToScreenToolStripMenuItem");
			saveToolStripMenuItem.Text = Translator.GetText("Save");
			sortAlphabeticallyCheckBox.Text = Translator.GetText("sortAlphabeticallyCheckBox");
			showHiddenCheckBox.Text = Translator.GetText("showHiddenCheckBox");
			barLabelLabel.Text = Translator.GetText("barLabelsLabel");
			viewBarLabelsToolStripMenuItem.Text = Translator.GetText("viewBarLabelsToolStripMenuItem");
			viewTextDataToolStripMenuItem.Text = Translator.GetText("ViewTextData");
		}
	}

	private void SetInfo(int id, string textData, long duration, DateTime startTime, long reads, long writes, long cpu, long rows)
	{
		idValueLabel.Text = id.ToString();
		textDataValueLabel.Text = textData;
		durationValueLabel.Text = GetTimeStringText(Convert.ToInt32(duration));
		relativeStartTimeValueLabel.Text = GetTimeStringText(GetRelativeStartTime(startTime));
		startTimeValueLabel.Text = GenericHelper.FormatLongDate(startTime);
		readsValueLabel.Text = reads.ToString();
		writesValueLabel.Text = writes.ToString();
		cpuValueLabel.Text = cpu.ToString();
		rowsValueLabel.Text = rows.ToString();
		_textData = textData;
	}

	private int GetRelativeStartTime(DateTime startTime)
	{
		int relativeStartTime = Convert.ToInt32((startTime - _firstStartTime).TotalMilliseconds);
		return relativeStartTime;
	}

	private void ClearValueLabels()
	{
		idValueLabel.Text = "";
		textDataValueLabel.Text = "";
		durationValueLabel.Text = "";
		relativeStartTimeValueLabel.Text = "";
		startTimeValueLabel.Text = "";
		readsValueLabel.Text = "";
		writesValueLabel.Text = "";
		cpuValueLabel.Text = "";
		rowsValueLabel.Text = "";
	}

	private bool IsMouseOnRow(int height, int y)
	{
		if (y > (int)_fontHeight + 2 && y < (height - ((int)_fontHeight + 6)))
		{
			return true;
		}

		return false;
	}

	private void PictureBox_MouseMove(object sender, MouseEventArgs e)
	{
		int y = e.Y;

		if (y < 0)
		{
			y = y + 32768 + 32768;
		}

		if (IsMouseOnRow(((PictureBox)sender).Height, y))
		{
			int row = (int)Math.Ceiling((float)(y - ((int)_fontHeight + 2)) / (_barHeight - 1)) - 1;

			int id = Convert.ToInt32(_dataTable.Rows[row][0]);
			string textData = _dataTable.Rows[row][1].ToString();
			long duration = long.Parse(_dataTable.Rows[row][2].ToString());
			DateTime startTime = Convert.ToDateTime(_dataTable.Rows[row][3]);
			long reads = long.Parse(_dataTable.Rows[row][4].ToString());
			long writes = long.Parse(_dataTable.Rows[row][5].ToString());
			long cpu = long.Parse(_dataTable.Rows[row][6].ToString());
			long rows = long.Parse(_dataTable.Rows[row][7].ToString());

			SetInfo(id, textData, duration, startTime, reads, writes, cpu, rows);
		}
	}

	private void PictureBox_MouseUp(object sender, MouseEventArgs e)
	{
		int y = e.Y;

		if (y < 0)
		{
			y = y + 32768 + 32768;
		}

		if (e.Button == MouseButtons.Right && IsMouseOnRow(((PictureBox)sender).Height, y))
		{
			contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
		}
	}

	private void PictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		int y = e.Y;

		if (y < 0)
		{
			y = y + 32768 + 32768;
		}

		if (e.Button == MouseButtons.Left && IsMouseOnRow(((PictureBox)sender).Height, y))
		{
			ViewRowForm form = new ViewRowForm();
			form.SetValues(_textData);
			form.ShowDialog();
		}
	}

	private void SetFocus()
	{
		foreach (Control control in graphPanel.Controls)
		{
			if (control is PictureBox)
			{
				control.Focus();
			}
		}
	}

	private void DrawGraph()
	{
		int numberOfGraphs = _dataTable.Rows.Count;

		int sizeX = Convert.ToInt32((_chartLengthInUnits * _pixelsPerUnit) + 2 + (int)_idStringWidth);
		int sizeY = ((_barHeight - 1) * numberOfGraphs) + (int)_fontHeight + 7 + (int)_fontHeight + 2;

		if (_bmp != null)
		{
			_bmp.Dispose();
		}

		_bmp = new Bitmap(sizeX, sizeY);

		using (Graphics gfx = Graphics.FromImage(_bmp))
		{
			gfx.Clear(Color.DarkGray);
		}

		for (int i = 0; i < numberOfGraphs; i++)
		{
			AddBackground(_bmp, i);
		}

		AddDivider(_bmp, numberOfGraphs);

		for (int i = 0; i < numberOfGraphs; i++)
		{
			long duration = long.Parse(_dataTable.Rows[i][2].ToString());
			DateTime startTime = Convert.ToDateTime(_dataTable.Rows[i][3]);

			AddBar(_bmp, i, duration, startTime);

			if (_barHeight >= 16)
			{
				AddBarLabels(i);
			}
		}

		AddTime(_bmp, numberOfGraphs);

		CreateBitmap(_bmp, sizeX, sizeY);
		CreateSpacingAfterGraph(sizeX, sizeY);
	}

	private void AddBarLabels(int i)
	{
		int id = Convert.ToInt32(_dataTable.Rows[i][0]);

		string barLabelText;

		if (barLabelsComboBox.SelectedItem.ToString() == "Duration")
		{
			barLabelText = GetTimeStringText(Convert.ToInt32(_dataTable.Rows[i][barLabelsComboBox.SelectedItem.ToString()]));
		}
		else if (barLabelsComboBox.SelectedItem.ToString() == "StartTime")
		{
			DateTime startTime = Convert.ToDateTime(_dataTable.Rows[i][barLabelsComboBox.SelectedItem.ToString()]);
			barLabelText = GenericHelper.FormatLongDate(startTime);
		}
		else
		{
			barLabelText = _dataTable.Rows[i][barLabelsComboBox.SelectedItem.ToString()].ToString();
		}

		AddText(_bmp, i, id, barLabelText);
	}

	private void CreateBitmap(Bitmap bmp, int sizeX, int sizeY)
	{
		const int locationX = 10;
		const int locationY = 10;

		if (_pictureBox != null)
		{
			_pictureBox.Dispose();
		}

		_pictureBox = new PictureBox();
		_pictureBox.MouseMove += PictureBox_MouseMove;
		_pictureBox.MouseUp += PictureBox_MouseUp;
		_pictureBox.MouseDoubleClick += PictureBox_MouseDoubleClick;
		_pictureBox.Location = new Point(locationX, locationY);
		_pictureBox.Size = new Size(sizeX + 2, sizeY);
		_pictureBox.Image = bmp;
		_pictureBox.BorderStyle = BorderStyle.FixedSingle;
		_pictureBox.BackColor = Color.LightGray;
		_pictureBox.Click += PictureBox_Click;
		graphPanel.Controls.Add(_pictureBox);
	}

	private void CreateSpacingAfterGraph(int sizeX, int sizeY)
	{
		int locationX = sizeX + 10;
		const int locationY = 10;
		const int spacingSize = 10;

		if (_pictureBoxSpacing != null)
		{
			_pictureBoxSpacing.Dispose();
		}

		_pictureBoxSpacing = new PictureBox();
		_pictureBoxSpacing.Location = new Point(locationX, locationY);
		_pictureBoxSpacing.Size = new Size(spacingSize, sizeY + spacingSize);
		_pictureBoxSpacing.Image = new Bitmap(spacingSize, spacingSize);
		graphPanel.Controls.Add(_pictureBoxSpacing);
	}

	private void PictureBox_Click(object sender, EventArgs e)
	{
		SetFocus();
	}

	private static int GetDuration(int duration)
	{
		float returnDuration = duration;
		returnDuration = (float)Math.Round(returnDuration, 0, MidpointRounding.AwayFromZero);
		return Convert.ToInt32(returnDuration);
	}

	private void AnalyseData(DataTable dataTable)
	{
		_firstStartTime = Convert.ToDateTime(dataTable.Rows[0][3]);

		SetVerticalValues();
		CalculateChartLength(dataTable);

		int graphHeight = (dataTable.Rows.Count * (_barHeight - 1)) + 4 + (int)_fontHeight + 2 + (int)_fontHeight + 2 + 10 + 10 + 5;

		if (graphHeight > graphPanel.Height)
		{
			_graphWidth = graphPanel.Width - (45 + (int)_idStringWidth);
		}
		else
		{
			_graphWidth = graphPanel.Width - (29 + (int)_idStringWidth);
		}

		_zoomHorizontalValue = (float)(_graphWidth) / _chartLengthInUnits;
	}

	private void CalculateChartLength(DataTable dataTable)
	{
		int maxLength = -1;
		int maxId = -1;

		foreach (DataRow row in dataTable.Rows)
		{
			DateTime startTime = Convert.ToDateTime(row[3]);
			int duration = GetDuration(Convert.ToInt32(row[2]));

			if (duration <= 0)
			{
				duration = 1;
			}

			int length = duration + GetRelativeStartTime(startTime);

			if (length > maxLength)
			{
				maxLength = length;
			}

			int id = Convert.ToInt32(row[0]);

			if (id > maxId)
			{
				maxId = id;
			}
		}

		using (Graphics gfx = Graphics.FromImage(new Bitmap(1, 1)))
		{
			string text = string.Format("Id {0}", maxId);
			SizeF size = gfx.MeasureString(text, new Label().Font);
			_idStringWidth = size.Width + 5;
			_fontHeight = size.Height;
		}

		_chartLengthInUnits = maxLength;
	}

	private int GetBarLength(int duration)
	{
		int returnDuration = Convert.ToInt32(GetDuration(Convert.ToInt32(duration)) * _pixelsPerUnit) - 1;

		if (returnDuration <= 0)
		{
			return 1;
		}

		return returnDuration;
	}

	private void AddBackground(Bitmap bmp, int number)
	{
		int locationX = (int)_idStringWidth;
		int locationY = (_barHeight * number) - number + 1 + (int)_fontHeight + 2;
		int sizeX = Convert.ToInt32(_chartLengthInUnits * _pixelsPerUnit + 2);
		int sizeY = _barHeight - 2;

		using (Graphics gfx = Graphics.FromImage(bmp))
		{
			using (SolidBrush brush = new SolidBrush(Color.WhiteSmoke))
			{
				gfx.FillRectangle(brush, new Rectangle(locationX, locationY, sizeX, sizeY));
			}
		}
	}

	private void AddBar(Bitmap bmp, int number, long duration, DateTime startTime)
	{
		int locationX = Convert.ToInt32(GetRelativeStartTime(startTime) * _pixelsPerUnit) + 1 + (int)_idStringWidth;
		int locationY = (_barHeight * number) - number + 1 + (int)_fontHeight + 2;
		int sizeX = GetBarLength(Convert.ToInt32(duration));
		int sizeY = _barHeight - 2;

		using (Graphics gfx = Graphics.FromImage(bmp))
		{
			using (SolidBrush brush = new SolidBrush(Color.SteelBlue))
			{
				gfx.FillRectangle(brush, new Rectangle(locationX, locationY, sizeX, sizeY));
			}
		}
	}

	private void AddDivider(Bitmap bmp, int numberOfGraphs)
	{
		int numberOfDividers = _chartLengthInUnits / _unitsBetweenDividers;

		for (int i = 1; i <= numberOfDividers; i++)
		{
			int locationX = Convert.ToInt32(_unitsBetweenDividers * _pixelsPerUnit * i) + (int)_idStringWidth;
			int locationY = 1 + (int)_fontHeight + 2;
			int sizeY = ((_barHeight - 1) * numberOfGraphs) - 1;

			using (Graphics gfx = Graphics.FromImage(bmp))
			{
				using (Pen pen = new Pen(Color.LightGray, 1))
				{
					gfx.DrawLine(pen, locationX, locationY, locationX, locationY + sizeY);
				}
			}
		}
	}

	private void AddText(Bitmap bmp, int number, int id, string barLabelText)
	{
		const int locationX = 0;
		int locationY = 1 + (_barHeight * number) - number + (int)_fontHeight + 2;
		int sizeX = (int)_idStringWidth;
		int sizeY = (int)_fontHeight;

		using (Graphics gfx = Graphics.FromImage(bmp))
		{
			gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			gfx.DrawString(string.Format("Id {0}", id), new Label().Font, Brushes.Black, new Rectangle(locationX, locationY, sizeX, sizeY));

			if (viewBarLabelsToolStripMenuItem.Checked)
			{
				SizeF size = gfx.MeasureString(barLabelText, new Label().Font);
				int actionStringWidth = (int)size.Width + 5;

				if (actionStringWidth > _graphWidth)
				{
					actionStringWidth = _graphWidth;
				}

				gfx.DrawString(barLabelText, new Label().Font, Brushes.DarkGray, new Rectangle(locationX + sizeX, locationY, actionStringWidth, sizeY));
			}
		}
	}

	private void AddTime(Bitmap bmp, int numberOfGraphs)
	{
		int numberOfDividers = _chartLengthInUnits / _unitsBetweenDividers;

		for (int i = 0; i <= numberOfDividers; i++)
		{
			int locationX = Convert.ToInt32((int)_idStringWidth + (_unitsBetweenDividers * _pixelsPerUnit * i));
			int locationY = (numberOfGraphs * (_barHeight - 1)) + 4 + (int)_fontHeight + 2;

			int timeInUnits = _unitsBetweenDividers * i;

			using (Graphics gfx = Graphics.FromImage(bmp))
			{
				string text = GetTimeString(timeInUnits);
				SizeF size = gfx.MeasureString(text, new Label().Font);
				int textStringWidth = (int)size.Width + 5;

				gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
				gfx.DrawString(text, new Label().Font, Brushes.Black, new Rectangle(locationX - ((textStringWidth - 5) / 2), locationY, textStringWidth, (int)_fontHeight));
				gfx.DrawString(text, new Label().Font, Brushes.Black, new Rectangle(locationX - ((textStringWidth - 5) / 2), 1, textStringWidth, (int)_fontHeight));
			}
		}
	}

	private static string GetTimeString(int timeInMilliSeconds)
	{
		string unit = "ms";
		float time = timeInMilliSeconds;

		TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, timeInMilliSeconds);

		if (timeSpan.Days != 0)
		{
			unit = "d";
			time = time / 1000 / 60 / 60 / 24;
		}
		else if (timeSpan.Hours != 0)
		{
			unit = "h";
			time = time / 1000 / 60 / 60;
		}
		else if (timeSpan.Minutes != 0)
		{
			unit = "m";
			time = time / 1000 / 60;
		}
		else if (timeSpan.Seconds != 0)
		{
			unit = "s";
			time = time / 1000;
		}

		return string.Format("{0} {1}", Math.Round(time, 2, MidpointRounding.AwayFromZero), unit);
	}

	private static string GetTimeStringText(int timeInMilliSeconds)
	{
		TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, timeInMilliSeconds);

		string returnText;

		if (Math.Floor(timeSpan.TotalHours) > 0)
		{
			returnText = string.Format("{0} h, {1} m, {2} s, {3} ms", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
		}
		else if (Math.Floor(timeSpan.TotalMinutes) > 0)
		{
			returnText = string.Format("{0} m, {1} s, {2} ms", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
		}
		else if (Math.Floor(timeSpan.TotalSeconds) > 0)
		{
			returnText = string.Format("{0} s, {1} ms", timeSpan.Seconds, timeSpan.Milliseconds);
		}
		else
		{
			returnText = string.Format("{0} ms", timeSpan.Milliseconds);
		}

		return returnText;
	}

	private void ZoomHorizontalTrackBar_Scroll(object sender, EventArgs e)
	{
		if (zoomHorizontalTrackBar.Value != _previousHorizontalZoomBarValue)
		{
			SetHorizontalValues();

			graphPanel.Controls.Clear();
			DrawGraph();

			_previousHorizontalZoomBarValue = zoomHorizontalTrackBar.Value;
		}

		graphPanel.Focus();
	}

	private void SetHorizontalValues()
	{
		_pixelsPerUnit = zoomHorizontalTrackBar.Value * _zoomHorizontalValue;
		_unitsBetweenDividers = Convert.ToInt32(_distanceBetweenDividers / _zoomHorizontalValue / zoomHorizontalTrackBar.Value);

		if (_unitsBetweenDividers == 0)
		{
			_unitsBetweenDividers = 1;
		}
	}

	private void ZoomVerticalTrackBar_Scroll(object sender, EventArgs e)
	{
		if (zoomVerticalTrackBar.Value != _previousVerticalZoomBarValue)
		{
			SetVerticalValues();

			graphPanel.Controls.Clear();
			DrawGraph();

			_previousVerticalZoomBarValue = zoomVerticalTrackBar.Value;
		}

		graphPanel.Focus();
	}

	private void SetVerticalValues()
	{
		_barHeight = (zoomVerticalTrackBar.Value * 2) + 2;
	}

	private void GraphPanel_Click(object sender, EventArgs e)
	{
		SetFocus();
	}

	private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void ViewTextDataToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ViewRowForm form = new ViewRowForm();
		form.SetValues(_textData);
		form.ShowDialog();
	}

	private void TimelineForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (_bmp != null)
		{
			_bmp.Dispose();
		}

		if (_pictureBox != null)
		{
			_pictureBox.Dispose();
		}

		if (_pictureBoxSpacing != null)
		{
			_pictureBoxSpacing.Dispose();
		}
	}

	private void SetSize()
	{
		int x = Convert.ToInt32(ConfigHandler.TimelineWindowSize.Split(';')[0]);
		int y = Convert.ToInt32(ConfigHandler.TimelineWindowSize.Split(';')[1]);

		if (x > Screen.PrimaryScreen.Bounds.Width || y > Screen.PrimaryScreen.Bounds.Height)
		{
			Rectangle workingRectangle = Screen.PrimaryScreen.WorkingArea;
			Size = new Size(workingRectangle.Width, workingRectangle.Height);
			return;
		}

		if (x >= MinimumSize.Width && y >= MinimumSize.Height)
		{
			Size = new Size(x, y);
		}
	}

	private void TimelineForm_Resize(object sender, EventArgs e)
	{
		if (Width < MinimumSize.Width || Width == 0)
		{
			return;
		}

		ConfigHandler.TimelineWindowSize = string.Format("{0}; {1}", Size.Width, Size.Height);
		ConfigHandler.SaveConfig();
	}

	private void FitToScreenToolStripMenuItem_Click(object sender, EventArgs e)
	{
		zoomHorizontalTrackBar.Value = 1;
		graphPanel.AutoScrollPosition = new Point(0, 0);
		Start();
	}

	private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DialogResult result = saveFileDialog1.ShowDialog();
		Application.DoEvents();

		if (result.ToString() == "OK")
		{
			switch (saveFileDialog1.FilterIndex)
			{
				case 1:
					_bmp.Save(saveFileDialog1.FileName, ImageFormat.Png);
					break;
				case 2:
					_bmp.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
					break;
				case 3:
					_bmp.Save(saveFileDialog1.FileName, ImageFormat.Tiff);
					break;
			}
		}
	}

	private void ViewBarLabelsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		graphPanel.Controls.Clear();
		DrawGraph();
	}

	private void InitializeColumnComboBox()
	{
		string selectedName = null;

		if (barLabelsComboBox.SelectedIndex >= 0)
		{
			selectedName = barLabelsComboBox.Text;
		}

		barLabelsComboBox.Items.Clear();

		foreach (string columnName in GetColumnNames())
		{
			barLabelsComboBox.Items.Add(new ComboBoxItem(columnName));
		}

		if (selectedName != null)
		{
			foreach (ComboBoxItem item in barLabelsComboBox.Items)
			{
				if (item.ToString() == selectedName)
				{
					barLabelsComboBox.SelectedItem = item;
				}
			}
		}
		else
		{
			barLabelsComboBox.SelectedIndex = 0;
		}
	}

	private List<string> GetColumnNames()
	{
		List<string> returnList = new List<string>();

		returnList.Add("TextData");
		returnList.Add("Duration");
		returnList.Add("StartTime");
		returnList.Add("Reads");
		returnList.Add("Writes");
		returnList.Add("CPU");
		returnList.Add("Rows");

		foreach (Column column in ColumnHelper.EnabledColumns)
		{
			if (showHiddenCheckBox.Checked || (!showHiddenCheckBox.Checked && !column.Hidden))
			{
				returnList.Add(column.Name);
			}
		}

		if (sortAlphabeticallyCheckBox.Checked)
		{
			returnList.Sort(delegate (string c1, string c2)
			{
				return c1.CompareTo(c2);
			});
		}

		return returnList;
	}

	private class ComboBoxItem
	{
		private readonly string _text;

		public ComboBoxItem(string text)
		{
			_text = text;
		}

		public override string ToString()
		{
			return _text;
		}
	}

	private void ShowHiddenCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		InitializeColumnComboBox();
	}

	private void SortAlphabeticallyCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		InitializeColumnComboBox();
	}

	private void BarLabelsComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		graphPanel.Controls.Clear();
		DrawGraph();
	}
}
