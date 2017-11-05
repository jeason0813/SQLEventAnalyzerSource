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
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using System.Drawing.Imaging;

public class TransparentTextBox : TextBox
{
	private readonly uPictureBox myPictureBox;
	private bool myUpToDate;
	private bool myCaretUpToDate;
	private Bitmap myBitmap;
	private Bitmap myAlphaBitmap;
	private bool myPaintedFirstTime;
	private Color myBackColor = Color.White;
	private int myBackAlpha = 10;
	private Container components;

	public TransparentTextBox()
	{
		InitializeComponent();

		BackColor = myBackColor;

		SetStyle(ControlStyles.UserPaint, false);
		SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		SetStyle(ControlStyles.DoubleBuffer, true);

		myPictureBox = new uPictureBox();
		Controls.Add(myPictureBox);
		myPictureBox.Dock = DockStyle.Fill;
	}

	[Category("Appearance"), Description("The alpha value used to blend the control's background. Valid values are 0 through 255."), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	public int BackAlpha
	{
		get
		{
			return myBackAlpha;
		}
		set
		{
			int v = value;

			if (v > 255)
			{
				v = 255;
			}

			myBackAlpha = v;
			myUpToDate = false;
			Invalidate();
		}
	}

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
		myBitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
		myAlphaBitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
		myUpToDate = false;
		Invalidate();
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown(e);
		myUpToDate = false;
		Invalidate();
	}

	protected override void OnKeyUp(KeyEventArgs e)
	{
		base.OnKeyUp(e);
		myUpToDate = false;
		Invalidate();
	}

	protected override void OnKeyPress(KeyPressEventArgs e)
	{
		base.OnKeyPress(e);
		myUpToDate = false;
		Invalidate();
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		Invalidate();
	}

	protected override void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
	{
		base.OnGiveFeedback(gfbevent);
		myUpToDate = false;
		Invalidate();
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		Point ptCursor = Cursor.Position;
		Form f = FindForm();

		if (f != null)
		{
			ptCursor = f.PointToClient(ptCursor);
		}

		if (!Bounds.Contains(ptCursor))
		{
			base.OnMouseLeave(e);
		}
	}

	protected override void OnChangeUICues(UICuesEventArgs e)
	{
		base.OnChangeUICues(e);
		myUpToDate = false;
		Invalidate();
	}

	protected override void OnGotFocus(EventArgs e)
	{
		base.OnGotFocus(e);
		myCaretUpToDate = false;
		myUpToDate = false;
		Invalidate();
	}

	protected override void OnLostFocus(EventArgs e)
	{
		base.OnLostFocus(e);
		myCaretUpToDate = false;
		myUpToDate = false;
		Invalidate();
	}

	protected override void OnFontChanged(EventArgs e)
	{
		if (myPaintedFirstTime)
		{
			SetStyle(ControlStyles.UserPaint, false);
		}

		base.OnFontChanged(e);

		if (myPaintedFirstTime)
		{
			SetStyle(ControlStyles.UserPaint, true);
		}

		myUpToDate = false;
		Invalidate();
	}

	protected override void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		myUpToDate = false;
		Invalidate();
	}

	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);

		switch (m.Msg)
		{
			case win32.WM_PAINT:
				myPaintedFirstTime = true;
				if (!myUpToDate || !myCaretUpToDate)
					GetBitmaps();
				myUpToDate = true;
				myCaretUpToDate = true;
				if (myPictureBox.Image != null) myPictureBox.Image.Dispose();
				myPictureBox.Image = (Image)myAlphaBitmap.Clone();
				break;
			case win32.WM_VSCROLL:
			case win32.WM_HSCROLL:
				myUpToDate = false;
				Invalidate();
				break;
			case win32.WM_LBUTTONDBLCLK:
			case win32.WM_RBUTTONDOWN:
			case win32.WM_LBUTTONDOWN:
				myUpToDate = false;
				Invalidate();
				break;
			case win32.WM_MOUSEMOVE:
				if (m.WParam.ToInt32() != 0)
				{
					myUpToDate = false;
					Invalidate();
				}
				break;
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			if (components != null)
			{
				components.Dispose();
			}
		}

		base.Dispose(disposing);
	}

	public new BorderStyle BorderStyle
	{
		get
		{
			return base.BorderStyle;
		}
		set
		{
			if (myPaintedFirstTime)
			{
				SetStyle(ControlStyles.UserPaint, false);
			}

			base.BorderStyle = value;

			if (myPaintedFirstTime)
			{
				SetStyle(ControlStyles.UserPaint, true);
			}

			myBitmap = null;
			myAlphaBitmap = null;
			myUpToDate = false;
			Invalidate();
		}
	}

	public new Color BackColor
	{
		get
		{
			return Color.FromArgb(base.BackColor.R, base.BackColor.G, base.BackColor.B);
		}
		set
		{
			myBackColor = value;
			base.BackColor = value;
			myUpToDate = false;
		}
	}

	public override bool Multiline
	{
		get
		{
			return base.Multiline;
		}
		set
		{
			if (myPaintedFirstTime)
			{
				SetStyle(ControlStyles.UserPaint, false);
			}

			base.Multiline = value;

			if (myPaintedFirstTime)
			{
				SetStyle(ControlStyles.UserPaint, true);
			}

			myBitmap = null;
			myAlphaBitmap = null;
			myUpToDate = false;
			Invalidate();
		}
	}

	private void GetBitmaps()
	{
		if (myBitmap == null || myAlphaBitmap == null || myBitmap.Width != Width || myBitmap.Height != Height || myAlphaBitmap.Width != Width || myAlphaBitmap.Height != Height)
		{
			myBitmap = null;
			myAlphaBitmap = null;
		}

		if (myBitmap == null)
		{
			myBitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
			myUpToDate = false;
		}

		if (!myUpToDate)
		{
			SetStyle(ControlStyles.UserPaint, false);

			win32.CaptureWindow(this, ref myBitmap);

			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			BackColor = Color.FromArgb(myBackAlpha, myBackColor);
		}

		Rectangle r2 = new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height);
		ImageAttributes tempImageAttr = new ImageAttributes();

		ColorMap[] tempColorMap = new ColorMap[1];
		tempColorMap[0] = new ColorMap();
		tempColorMap[0].OldColor = Color.FromArgb(255, myBackColor);
		tempColorMap[0].NewColor = Color.FromArgb(myBackAlpha, myBackColor);

		tempImageAttr.SetRemapTable(tempColorMap);

		if (myAlphaBitmap != null)
		{
			myAlphaBitmap.Dispose();
		}

		myAlphaBitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);

		Graphics tempGraphics1 = Graphics.FromImage(myAlphaBitmap);

		tempGraphics1.DrawImage(myBitmap, r2, 0, 0, ClientRectangle.Width, ClientRectangle.Height, GraphicsUnit.Pixel, tempImageAttr);
		tempGraphics1.Dispose();
	}

	private sealed class uPictureBox : PictureBox
	{
		public uPictureBox()
		{
			SetStyle(ControlStyles.Selectable, false);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);

			Cursor = null;
			Enabled = true;
			SizeMode = PictureBoxSizeMode.Normal;
		}

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case win32.WM_MOUSEMOVE:
				case win32.WM_MOUSELEAVE:
				case win32.WM_LBUTTONDBLCLK:
				case win32.WM_RBUTTONDOWN:
				case win32.WM_LBUTTONDOWN:
					win32.PostMessage(Parent.Handle, (uint)m.Msg, m.WParam, m.LParam);
					break;
				case win32.WM_LBUTTONUP:
					Parent.Invalidate();
					break;
			}

			base.WndProc(ref m);
		}
	}

	private void InitializeComponent()
	{
		components = new Container();
	}
}

public class win32
{
	public const int WM_MOUSEMOVE = 0x0200;
	public const int WM_LBUTTONDOWN = 0x0201;
	public const int WM_LBUTTONUP = 0x0202;
	public const int WM_RBUTTONDOWN = 0x0204;
	public const int WM_LBUTTONDBLCLK = 0x0203;
	public const int WM_MOUSELEAVE = 0x02A3;
	public const int WM_PAINT = 0x000F;
	public const int WM_ERASEBKGND = 0x0014;
	public const int WM_PRINT = 0x0317;
	public const int WM_HSCROLL = 0x0114;
	public const int WM_VSCROLL = 0x0115;
	public const int EM_GETSEL = 0x00B0;
	public const int EM_LINEINDEX = 0x00BB;
	public const int EM_LINEFROMCHAR = 0x00C9;
	public const int EM_POSFROMCHAR = 0x00D6;

	[DllImport("USER32.DLL", EntryPoint = "PostMessage")]
	public static extern bool PostMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

	[DllImport("USER32.DLL", EntryPoint = "SendMessage")]
	public static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

	[DllImport("USER32.DLL", EntryPoint = "GetCaretBlinkTime")]
	public static extern uint GetCaretBlinkTime();

	private const long PRF_CLIENT = 0x00000004L;
	private const long PRF_ERASEBKGND = 0x00000008L;

	public static bool CaptureWindow(Control control, ref Bitmap bitmap)
	{
		Graphics g2 = Graphics.FromImage(bitmap);
		const int meint = (int)(PRF_CLIENT | PRF_ERASEBKGND);
		IntPtr meptr = new IntPtr(meint);

		IntPtr hdc = g2.GetHdc();
		SendMessage(control.Handle, WM_PRINT, hdc, meptr);

		g2.ReleaseHdc(hdc);
		g2.Dispose();

		return true;
	}
}
