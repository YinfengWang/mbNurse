using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace HISPlus
{
    /// <summary>
    /// This class implements sizing and moving functions for
    ///	runtime editing of graphic controls
    /// </summary>
    public class PickBox
    {
        //////////////////////////////////////////////////////////////////
        // PRIVATE CONSTANTS AND VARIABLES
        //////////////////////////////////////////////////////////////////

        private const int BoxSize = 1;
        private ControlAssist _mControls;
        //private List<Control> _mControls;
        private int _startl;
        private int _startt;
        private int _startw;
        private int _starth;
        private int _startx;
        private int _starty;
        private Size startPoint;
        private bool _dragging;
        private Cursor _oldCursor;
        private List<ControlAssist> listAssist = new List<ControlAssist>();

        private const int MinSize = 20;
        private List<MyOpaqueLayer> listPanel;

        //public void WireControl(Control ctl)
        //{
        //    //ctl.BringToFront();
        //    //ctl.Enabled = false;           
        //    ctl.Click += this.SelectControl;
        //}

        public void WireControl(params Control[] ctls)
        {
            List<Control> list = ctls.ToList();
            if (listAssist.Exists(p => p.ListControls == list))
                return;
            foreach (Control ctl in ctls)
            {
                listAssist.Add(new ControlAssist { Control = ctl, ListControls = list });

                ctl.Click += this.SelectControl;
            }
        }

        //
        // Attaches a pick box to the sender Control
        //
        private void SelectControl(object sender, EventArgs e)
        {
            if (_mControls == listAssist.FirstOrDefault(p => p.Control == (Control)sender))
                return;

            if (_mControls != null && listPanel != null)
            {
                _mControls.Control.FindForm().KeyDown -= PickBox_KeyDown;
                foreach (MyOpaqueLayer panel in listPanel)
                {
                    panel.MouseDown -= this.ctl_MouseDown;
                    panel.MouseMove -= this.ctl_MouseMove;
                    panel.MouseUp -= this.ctl_MouseUp;
                    panel.Visible = false;
                    panel.Enabled = false;
                }
                //_mControls = null;
                listPanel = null;
            }

            _mControls = listAssist.FirstOrDefault(p => p.Control == (Control)sender);

            listPanel = new List<MyOpaqueLayer>(_mControls.ListControls.Count);

            _mControls.Control.FindForm().KeyDown += PickBox_KeyDown;

            foreach (Control ctl in _mControls.ListControls)
            {
                MyOpaqueLayer panel = new MyOpaqueLayer();

                ctl.Parent.Controls.Add(panel);

                panel.MouseDown += this.ctl_MouseDown;
                panel.MouseMove += this.ctl_MouseMove;
                panel.MouseUp += this.ctl_MouseUp;
                //_mControl.FindForm().PreviewKeyDown+=PickBox_PreviewKeyDown;

                MoveHandles(ctl, panel);
                panel.Cursor = Cursors.SizeAll;
                listPanel.Add(panel);
            }
            ShowHandles();
        }

        void PickBox_KeyDown(object sender, KeyEventArgs e)
        {
            return;
            if (_mControls == null) return;

            switch (e.KeyCode)
            {
                case Keys.Left:
                    MoveControl(-10, 0);
                    break;
                case Keys.Right:
                    MoveControl(10, 0);
                    break;
                case Keys.Up:
                    MoveControl(0, -10);
                    break;
                case Keys.Down:
                    MoveControl(0, 10);
                    break;
            }
        }

        public void Remove()
        {
            HideHandles();
        }

        private void ShowHandles()
        {
            if (_mControls != null)
            {
                foreach (MyOpaqueLayer myOpaqueLayer in listPanel)
                {
                    myOpaqueLayer.Visible = true;
                    myOpaqueLayer.BringToFront();
                }
            }
        }

        private void HideHandles()
        {
            foreach (MyOpaqueLayer myOpaqueLayer in listPanel)
                myOpaqueLayer.Visible = false;
        }

        private void MoveHandles(int index)
        {
            MoveHandles(_mControls.ListControls[index], listPanel[index]);
        }

        private void MoveHandles(Control mControl, MyOpaqueLayer panel)
        {
            int sX = mControl.Left - BoxSize;
            int sY = mControl.Top - BoxSize;
            int sW = mControl.Width + BoxSize * 2;
            int sH = mControl.Height + BoxSize * 2;
            panel.Location = new Point(sX, sY);
            panel.Width = sW;
            panel.Height = sH;
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
                MyOpaqueLayer panel = sender as MyOpaqueLayer;
                int index = listPanel.IndexOf(panel);
                Control ctl = _mControls.ListControls[index];

                if (panel == null) return;

                int l = panel.Left + e.X - _startx + BoxSize;
                int t = panel.Top + e.Y - _starty + BoxSize;

                ctl.Left = l;
                ctl.Top = t;
            }
        }

        //private void 

        private void ctl_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;

            MyOpaqueLayer panel = sender as MyOpaqueLayer;
            MoveControl(e.X - _startx, e.Y - _starty, panel);

        }

        private void MoveControl(int x, int y, MyOpaqueLayer panel = null)
        {
            //Control ctl = _mControls[listPanel.IndexOf(panel)];
            foreach (Control ctl in _mControls.ListControls)
            {
                int l = ctl.Left + x;
                int t = ctl.Top + y;
                if (panel != null)
                    if (ctl == _mControls.ListControls[listPanel.IndexOf(panel)])
                    {
                        //continue;
                        l = panel.Left + x + BoxSize;
                        t = panel.Top + y + BoxSize;
                    }

                ctl.Location = new Point(l, t);
                ctl.BringToFront();
                //ctl.Left = l;
                //ctl.Top = t;
            }


            if (panel != null)
            {
                for (int i = 0; i < listPanel.Count; i++)
                    MoveHandles(i);
                ShowHandles();
            }
            else
            {
                foreach (MyOpaqueLayer myOpaqueLayer in listPanel)
                {
                    myOpaqueLayer.Left = myOpaqueLayer.Left + x;
                    myOpaqueLayer.Top = myOpaqueLayer.Top + y;
                    myOpaqueLayer.Visible = true;
                    myOpaqueLayer.BringToFront();
                }
            }
        }
    }

    /// <summary>
    /// 辅助类.因为数据没有地方放,所以创建一个统一的类,来存放控件,控件的集合,控件是否在拖动中
    /// </summary>
    public class ControlAssist
    {
        public Control Control { get; set; }

        /// <summary>
        /// 控件集合
        /// </summary>
        public List<Control> ListControls { get; set; }

        ///// <summary>
        ///// 是否在拖动中
        ///// </summary>
        //public bool IsShow { get; set; }
    }
}

