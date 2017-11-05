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


using DataViewerUserControl.Properties;

partial class DataViewer
{
	/// <summary> 
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary> 
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Component Designer generated code

	/// <summary> 
	/// Required method for Designer support - do not modify 
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.clearSearchButton = new System.Windows.Forms.Button();
			this.searchButton = new System.Windows.Forms.Button();
			this.searchTermTextBox = new System.Windows.Forms.TextBox();
			this.totalPagesLabel = new System.Windows.Forms.Label();
			this.pageTextBox = new System.Windows.Forms.TextBox();
			this.searchPictureBox = new System.Windows.Forms.PictureBox();
			this.firstPageButton = new System.Windows.Forms.Button();
			this.lastPageButton = new System.Windows.Forms.Button();
			this.previousPageButton = new System.Windows.Forms.Button();
			this.nextPageButton = new System.Windows.Forms.Button();
			this.pageLabel = new System.Windows.Forms.Label();
			this.totalRowsTextBox = new TransparentTextBox();
			this.searchComboBox = new ComboBoxCustom();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.searchPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.AllowUserToOrderColumns = true;
			this.dataGridView.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView.Location = new System.Drawing.Point(3, 33);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size(361, 117);
			this.dataGridView.StandardTab = true;
			this.dataGridView.TabIndex = 9;
			this.dataGridView.VirtualMode = true;
			// 
			// clearSearchButton
			// 
			this.clearSearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.clearSearchButton.Enabled = false;
			this.clearSearchButton.Location = new System.Drawing.Point(256, 4);
			this.clearSearchButton.Name = "clearSearchButton";
			this.clearSearchButton.Size = new System.Drawing.Size(51, 23);
			this.clearSearchButton.TabIndex = 7;
			this.clearSearchButton.Text = "Clear";
			this.clearSearchButton.UseVisualStyleBackColor = true;
			// 
			// searchButton
			// 
			this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.searchButton.Location = new System.Drawing.Point(313, 4);
			this.searchButton.Name = "searchButton";
			this.searchButton.Size = new System.Drawing.Size(51, 23);
			this.searchButton.TabIndex = 8;
			this.searchButton.Text = "Search";
			this.searchButton.UseVisualStyleBackColor = true;
			// 
			// searchTermTextBox
			// 
			this.searchTermTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.searchTermTextBox.Location = new System.Drawing.Point(186, 5);
			this.searchTermTextBox.MinimumSize = new System.Drawing.Size(0, 21);
			this.searchTermTextBox.Name = "searchTermTextBox";
			this.searchTermTextBox.Size = new System.Drawing.Size(64, 21);
			this.searchTermTextBox.TabIndex = 6;
			// 
			// totalPagesLabel
			// 
			this.totalPagesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.totalPagesLabel.AutoSize = true;
			this.totalPagesLabel.Location = new System.Drawing.Point(64, 161);
			this.totalPagesLabel.Name = "totalPagesLabel";
			this.totalPagesLabel.Size = new System.Drawing.Size(34, 13);
			this.totalPagesLabel.TabIndex = 14;
			this.totalPagesLabel.Text = "out of";
			// 
			// pageTextBox
			// 
			this.pageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pageTextBox.Location = new System.Drawing.Point(35, 158);
			this.pageTextBox.MinimumSize = new System.Drawing.Size(0, 21);
			this.pageTextBox.Name = "pageTextBox";
			this.pageTextBox.Size = new System.Drawing.Size(29, 21);
			this.pageTextBox.TabIndex = 10;
			// 
			// searchPictureBox
			// 
			this.searchPictureBox.Image = global::DataViewerUserControl.Properties.Resources.search;
			this.searchPictureBox.Location = new System.Drawing.Point(3, 5);
			this.searchPictureBox.Name = "searchPictureBox";
			this.searchPictureBox.Size = new System.Drawing.Size(21, 20);
			this.searchPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.searchPictureBox.TabIndex = 21;
			this.searchPictureBox.TabStop = false;
			// 
			// firstPageButton
			// 
			this.firstPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.firstPageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.firstPageButton.Image = global::DataViewerUserControl.Properties.Resources.page_first;
			this.firstPageButton.Location = new System.Drawing.Point(238, 158);
			this.firstPageButton.Name = "firstPageButton";
			this.firstPageButton.Size = new System.Drawing.Size(27, 21);
			this.firstPageButton.TabIndex = 11;
			this.firstPageButton.UseVisualStyleBackColor = true;
			// 
			// lastPageButton
			// 
			this.lastPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lastPageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lastPageButton.Image = global::DataViewerUserControl.Properties.Resources.page_last;
			this.lastPageButton.Location = new System.Drawing.Point(337, 158);
			this.lastPageButton.Name = "lastPageButton";
			this.lastPageButton.Size = new System.Drawing.Size(27, 21);
			this.lastPageButton.TabIndex = 14;
			this.lastPageButton.UseVisualStyleBackColor = true;
			// 
			// previousPageButton
			// 
			this.previousPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.previousPageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.previousPageButton.Image = global::DataViewerUserControl.Properties.Resources.page_previous;
			this.previousPageButton.Location = new System.Drawing.Point(271, 158);
			this.previousPageButton.Name = "previousPageButton";
			this.previousPageButton.Size = new System.Drawing.Size(27, 21);
			this.previousPageButton.TabIndex = 12;
			this.previousPageButton.UseVisualStyleBackColor = true;
			// 
			// nextPageButton
			// 
			this.nextPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.nextPageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.nextPageButton.Image = global::DataViewerUserControl.Properties.Resources.page_next;
			this.nextPageButton.Location = new System.Drawing.Point(304, 158);
			this.nextPageButton.Name = "nextPageButton";
			this.nextPageButton.Size = new System.Drawing.Size(27, 21);
			this.nextPageButton.TabIndex = 13;
			this.nextPageButton.UseVisualStyleBackColor = true;
			// 
			// pageLabel
			// 
			this.pageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pageLabel.AutoSize = true;
			this.pageLabel.Location = new System.Drawing.Point(3, 161);
			this.pageLabel.Name = "pageLabel";
			this.pageLabel.Size = new System.Drawing.Size(32, 13);
			this.pageLabel.TabIndex = 23;
			this.pageLabel.Text = "Page";
			// 
			// totalRowsTextBox
			// 
			this.totalRowsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.totalRowsTextBox.BackAlpha = 0;
			this.totalRowsTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.totalRowsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.totalRowsTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.totalRowsTextBox.Location = new System.Drawing.Point(135, 161);
			this.totalRowsTextBox.Name = "totalRowsTextBox";
			this.totalRowsTextBox.ReadOnly = true;
			this.totalRowsTextBox.ShortcutsEnabled = false;
			this.totalRowsTextBox.Size = new System.Drawing.Size(97, 13);
			this.totalRowsTextBox.TabIndex = 27;
			this.totalRowsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.totalRowsTextBox.WordWrap = false;
			this.totalRowsTextBox.Enter += new System.EventHandler(this.TotalRowsTextBox_Enter);
			// 
			// searchComboBox
			// 
			this.searchComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.searchComboBox.FormattingEnabled = true;
			this.searchComboBox.Location = new System.Drawing.Point(30, 5);
			this.searchComboBox.Name = "searchComboBox";
			this.searchComboBox.Size = new System.Drawing.Size(150, 21);
			this.searchComboBox.TabIndex = 5;
			this.searchComboBox.SelectedIndexChanged += new System.EventHandler(this.SearchComboBox_SelectedIndexChanged);
			// 
			// DataViewer
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.totalRowsTextBox);
			this.Controls.Add(this.pageLabel);
			this.Controls.Add(this.searchPictureBox);
			this.Controls.Add(this.clearSearchButton);
			this.Controls.Add(this.searchButton);
			this.Controls.Add(this.searchTermTextBox);
			this.Controls.Add(this.searchComboBox);
			this.Controls.Add(this.firstPageButton);
			this.Controls.Add(this.lastPageButton);
			this.Controls.Add(this.totalPagesLabel);
			this.Controls.Add(this.pageTextBox);
			this.Controls.Add(this.previousPageButton);
			this.Controls.Add(this.nextPageButton);
			this.Controls.Add(this.dataGridView);
			this.DoubleBuffered = true;
			this.MinimumSize = new System.Drawing.Size(367, 182);
			this.Name = "DataViewer";
			this.Size = new System.Drawing.Size(367, 182);
			this.EnabledChanged += new System.EventHandler(this.DataViewer_EnabledChanged);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.searchPictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.DataGridView dataGridView;
	private System.Windows.Forms.Button clearSearchButton;
	private System.Windows.Forms.Button searchButton;
	private System.Windows.Forms.TextBox searchTermTextBox;
	private ComboBoxCustom searchComboBox;
	private System.Windows.Forms.Button firstPageButton;
	private System.Windows.Forms.Button lastPageButton;
	private System.Windows.Forms.Label totalPagesLabel;
	private System.Windows.Forms.TextBox pageTextBox;
	private System.Windows.Forms.Button previousPageButton;
	private System.Windows.Forms.Button nextPageButton;
	private System.Windows.Forms.PictureBox searchPictureBox;
	private System.Windows.Forms.Label pageLabel;
	private TransparentTextBox totalRowsTextBox;
}
