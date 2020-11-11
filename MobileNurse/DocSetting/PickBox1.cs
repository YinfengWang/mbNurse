using System;
using System.Windows.Forms;
using System.Drawing;

namespace HISPlus
{
    /// <summary>
    /// This class implements sizing and moving functions for
    ///	runtime editing of graphic controls
    /// </summary>
    public class PickBox1
    {
        //////////////////////////////////////////////////////////////////
        // PRIVATE CONSTANTS AND VARIABLES
        //////////////////////////////////////////////////////////////////

        private const int BoxSize = 8;
        private readonly Color _boxColor = Color.White;
        private Control _mControl;
        private readonly Label[] _lbl = new Label[8];
        private int _startl;
        private int _startt;
        private int _startw;
        private int _starth;
        private int _startx;
        private int _starty;
        private bool _dragging;
        private Cursor[] arrArrow =
	{Cursors.SizeNWSE, Cursors.SizeNS,
	    Cursors.SizeNESW, Cursors.SizeWE, Cursors.SizeNWSE, Cursors.SizeNS,
	    Cursors.SizeNESW, Cursors.SizeWE};
        private Cursor _oldCursor;

        private const int MinSize = 20;

        public PickBox1()
        {
            for (int i = 0; i < 8; i++)
            {
                _lbl[i] = new Label
                {
                    TabIndex = i,
                    FlatStyle = 0,
                    Width = 0,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = _boxColor,
                    Cursor = arrArrow[i],
                    Text = ""
                };
                _lbl[i].BringToFront();
                _lbl[i].MouseDown += this.lbl_MouseDown;
                _lbl[i].MouseMove += this.lbl_MouseMove;
                _lbl[i].MouseUp += this.lbl_MouseUp;
            }
        }

        private ContainerControl MContainer { get; set; }


        public void WireControl(Control ctl)
        {
            //ctl.BringToFront();
            ctl.Click += this.SelectControl;
        }


        /////////////////////////////////////////////////////////////////
        // PRIVATE METHODS
        /////////////////////////////////////////////////////////////////
        /// 
        OpaqueCommand cmd = new OpaqueCommand();

        //
        // Attaches a pick box to the sender Control
        //
        private void SelectControl(object sender, EventArgs e)
        {
            if (_mControl != null)
            {
                //_mControl.Visible = true;                
                _mControl.Cursor = _oldCursor;

                //Remove event any pre-existing event handlers appended by this class
                _mControl.MouseDown -= this.ctl_MouseDown;
                _mControl.MouseMove -= this.ctl_MouseMove;
                _mControl.MouseUp -= this.ctl_MouseUp;
                //_mControl.KeyDown -= _mControl_KeyDown;
                //_mControl.FindForm().PreviewKeyDown -= PickBox_PreviewKeyDown;                              

                _mControl = null;
                cmd.HideOpaqueLayer();
            }

            _mControl = (Control)sender;
            //_mControl.Focus();
            //Add event handlers for moving the selected control around
            _mControl.MouseDown += this.ctl_MouseDown;
            //_mControl.KeyDown += _mControl_KeyDown;
            _mControl.MouseMove += this.ctl_MouseMove;
            _mControl.MouseUp += this.ctl_MouseUp;
            //_mControl.FindForm().PreviewKeyDown+=PickBox_PreviewKeyDown;
            //_mControl.FindForm().KeyDown+=PickBox_KeyDown;
           
            //Add sizing handles to Control's container (Form or PictureBox)
            
            for (int i = 0; i < 8; i++)
            {
                _mControl.Parent.Controls.Add(_lbl[i]);
                _lbl[i].BringToFront();
            }
            

            //Position sizing handles around Control
            MoveHandles();

            //Display sizing handles
            ShowHandles();

            cmd.ShowOpaqueLayer(_mControl, 125);
            _oldCursor = _mControl.Cursor;
            _mControl.Cursor = Cursors.SizeAll;

        }

        //void PickBox_KeyPress(object sender, KeyPressEventArgs e)
        //{            
        //    if (_mControl == null) return;
        //    switch (e.KeyChar)
        //    {
        //        case Keys.Left:
        //            _mControl.Left -= 10;
        //            break;
        //        case Keys.Right:
        //            _mControl.Left += 10;
        //            break;
        //        case Keys.Up:
        //            _mControl.Top -= 10;
        //            break;
        //        case Keys.Down:
        //            _mControl.Top += 10;
        //            break;
        //    }
        //}

        void PickBox_KeyDown(object sender, KeyEventArgs e)
        {           
            if (_mControl == null) return;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    _mControl.Left -= 10;
                    break;
                case Keys.Right:
                    _mControl.Left += 10;
                    break;
                case Keys.Up:
                    _mControl.Top -= 10;
                    break;
                case Keys.Down:
                    _mControl.Top += 10;
                    break;
            }
            //_mControl.Focus();     
        }

        void PickBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    _mControl.Left -= 10;
                    break;
                case Keys.Right:
                    _mControl.Left += 10;
                    break;
                case Keys.Up:
                    _mControl.Top -= 10;
                    break;
                case Keys.Down:
                    _mControl.Top += 10;
                    break;
            }
            _mControl.Focus();
        }

        void _mControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    _mControl.Left -= 10;
                    break;
                case Keys.Right:
                    _mControl.Left += 10;
                    break;
                case Keys.Up:
                    _mControl.Top -= 10;
                    break;
                case Keys.Down:
                    _mControl.Top += 10;
                    break;
            }
            // if (e.KeyCode == Keys.Up)
            //     _mControl.Top -= 10;
            //else if (e.KeyCode == Keys.Down)
            //     _mControl.Top += 10;
            // else if (e.KeyCode == Keys.Left)
            //     _mControl.Left -= 10;
            // else if (e.KeyCode == Keys.Right)
            //     _mControl.Left += 10;
        }

        public void Remove()
        {
            HideHandles();
            _mControl.Cursor = _oldCursor;
        }

        private void ShowHandles()
        {
            if (_mControl != null)
            {
                for (int i = 0; i < 8; i++)
                {
                    _lbl[i].Visible = true;
                }
            }
        }

        private void HideHandles()
        {
            for (int i = 0; i < 8; i++)
            {
                _lbl[i].Visible = false;
            }
        }

        private void MoveHandles()
        {
            int sX = _mControl.Left - BoxSize;
            int sY = _mControl.Top - BoxSize;
            int sW = _mControl.Width + BoxSize;
            int sH = _mControl.Height + BoxSize;
            const int hB = BoxSize / 2;
            int[] arrPosX =
		{sX+hB, sX + sW / 2, sX + sW-hB, sX + sW-hB,
		    sX + sW-hB, sX + sW / 2, sX+hB, sX+hB};
            int[] arrPosY =
		{sY+hB, sY+hB, sY+hB, sY + sH / 2, sY + sH-hB,
		    sY + sH-hB, sY + sH-hB, sY + sH / 2};
            for (int i = 0; i < 8; i++)
                _lbl[i].SetBounds(arrPosX[i], arrPosY[i], BoxSize, BoxSize);
        }
        private void lbl_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _startl = _mControl.Left;
            _startt = _mControl.Top;
            _startw = _mControl.Width;
            _starth = _mControl.Height;
            HideHandles();
        }
        private void lbl_MouseMove(object sender, MouseEventArgs e)
        {
            int l = _mControl.Left;
            int w = _mControl.Width;
            int t = _mControl.Top;
            int h = _mControl.Height;
            if (_dragging)
            {
                switch (((Label)sender).TabIndex)
                {
                    case 0: // Dragging top-left sizing box
                        l = _startl + e.X < _startl + _startw - MinSize ? _startl + e.X : _startl + _startw - MinSize;
                        t = _startt + e.Y < _startt + _starth - MinSize ? _startt + e.Y : _startt + _starth - MinSize;
                        w = _startl + _startw - _mControl.Left;
                        h = _startt + _starth - _mControl.Top;
                        break;
                    case 1: // Dragging top-center sizing box
                        t = _startt + e.Y < _startt + _starth - MinSize ? _startt + e.Y : _startt + _starth - MinSize;
                        h = _startt + _starth - _mControl.Top;
                        break;
                    case 2: // Dragging top-right sizing box
                        w = _startw + e.X > MinSize ? _startw + e.X : MinSize;
                        t = _startt + e.Y < _startt + _starth - MinSize ? _startt + e.Y : _startt + _starth - MinSize;
                        h = _startt + _starth - _mControl.Top;
                        break;
                    case 3: // Dragging right-middle sizing box
                        w = _startw + e.X > MinSize ? _startw + e.X : MinSize;
                        break;
                    case 4: // Dragging right-bottom sizing box
                        w = _startw + e.X > MinSize ? _startw + e.X : MinSize;
                        h = _starth + e.Y > MinSize ? _starth + e.Y : MinSize;
                        break;
                    case 5: // Dragging center-bottom sizing box
                        h = _starth + e.Y > MinSize ? _starth + e.Y : MinSize;
                        break;
                    case 6: // Dragging left-bottom sizing box
                        l = _startl + e.X < _startl + _startw - MinSize ? _startl + e.X : _startl + _startw - MinSize;
                        w = _startl + _startw - _mControl.Left;
                        h = _starth + e.Y > MinSize ? _starth + e.Y : MinSize;
                        break;
                    case 7: // Dragging left-middle sizing box
                        l = _startl + e.X < _startl + _startw - MinSize ? _startl + e.X : _startl + _startw - MinSize;
                        w = _startl + _startw - _mControl.Left;
                        break;
                }
                l = (l < 0) ? 0 : l;
                t = (t < 0) ? 0 : t;
                _mControl.SetBounds(l, t, w, h);
            }
        }
        private void lbl_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
            MoveHandles();
            ShowHandles();
        }

        private void ctl_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _startx = e.X;
            _starty = e.Y;
            HideHandles();
        }
        private void ctl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                int l = _mControl.Left + e.X - _startx;
                int t = _mControl.Top + e.Y - _starty;
                int w = _mControl.Width;
                int h = _mControl.Height;
                l = (l < 0) ? 0 : ((l + w > _mControl.Parent.ClientRectangle.Width) ?
                  _mControl.Parent.ClientRectangle.Width - w : l);
                t = (t < 0) ? 0 : ((t + h > _mControl.Parent.ClientRectangle.Height) ?
                _mControl.Parent.ClientRectangle.Height - h : t);
                _mControl.Left = l;
                _mControl.Top = t;
            }
        }
        private void ctl_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
            MoveHandles();
            ShowHandles();
        }

    }
}

