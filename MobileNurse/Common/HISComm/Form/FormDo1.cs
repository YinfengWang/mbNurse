using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace HISPlus
{
    //public delegate void DataChanged();

    public class FormDo1:Form
    {
        #region 变量
        protected string _id;                   // 窗体ID
        protected string _version;              // 版本号
        protected string _guid;                 // 窗体的GUID
        protected string _right;                // 支持的权限
        protected string _userRight;            // 当前用户具有权限
        #endregion


        /// <summary>
        /// FormDo 摘要说明
        /// </summary>
        public FormDo1()
        {
            this.KeyPreview = true;

            this.KeyDown += new KeyEventHandler(FormDo_KeyDown);
            this.MouseUp += new MouseEventHandler(FormDo_MouseUp);

            this.Load += new EventHandler(FormDo_Load2);
        }


        void FormDo_Load2(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(244, 249, 253);
            SetWinBackColor();
        }


        #region 属性
        /// <summary>
        /// 功能划分ID
        /// </summary>
        public string ID
        {
            get
            {
                return _id;
            }
        }


        /// <summary>
        /// 版本号
        /// </summary>
        public string Version
        {
            get
            {
                return _version;
            }
        }


        /// <summary>
        /// 窗体GUID
        /// </summary>
        public string GUID
        {
            get
            {
                return _guid;
            }
        }


        /// <summary>
        /// 窗体支持的权限列表
        /// </summary>
        public new string Right
        {
            get
            {
                return _right;
            }
        }


        /// <summary>
        /// 当前用户的权限
        /// </summary>
        public string UserRight
        {
            get
            {
                return _userRight;
            }
            set
            {
                _userRight = value;
            }
        }
        #endregion


        #region 方法
        void FormDo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F11)
                {
                    FormDoInfo infoFrm = new FormDoInfo();
                    infoFrm.ShowFormInfo(_id, _version, _guid, _right, _userRight);

                    infoFrm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        void FormDo_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Keys keyMode = Control.ModifierKeys;

                if (e.Button == MouseButtons.Right && keyMode == (Keys.Control))
                {
                    FormDoInfo infoFrm = new FormDoInfo();
                    infoFrm.ShowFormInfo(_id, _version, _guid, _right, _userRight);

                    infoFrm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion


        #region 共通方法
        /// <summary>
        /// 设置窗体背景色
        /// </summary>
        public void SetWinBackColor()
        {
            foreach (Control ctrl in this.Controls)
            {
                setControlBackColor(ctrl, this.BackColor);
            }
        }


        /// <summary>
        /// 设置控件背景色
        /// </summary>
        /// <param name="ctrl"></param>
        private void setControlBackColor(Control ctrl, Color bkColor)
        {
            // 字体控制
            if (ctrl.Font.Size < 9)
            {
                ctrl.Font = new Font(ctrl.Font.Name, 9, ctrl.Font.Style);
            }

            // 类型 DataGridView
            if (ctrl.GetType().Equals(typeof(DataGridView)) == true)
            {
                DataGridView dgv = ((DataGridView)ctrl);

                // 背景色
                dgv.BackgroundColor = bkColor;

                // 列标题
                dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dgv.ColumnHeadersHeight = 20;
                dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

                // 交替行
                dgv.AlternatingRowsDefaultCellStyle.BackColor = bkColor;

                // 行标题
                dgv.RowHeadersWidth = 24;
                dgv.RowsDefaultCellStyle.BackColor = Color.White;

                return;
            }

            // 类型 GroupBox
            if (ctrl.GetType().Equals(typeof(GroupBox)) == true)
            {
                GroupBox grp = ((GroupBox)(ctrl));
                grp.BackColor = bkColor;
                grp.ForeColor = Color.Black;

                foreach (Control ctrlIn in grp.Controls)
                {
                    setControlBackColor(ctrlIn, bkColor);
                }

                return;
            }

            // 类型 TextBox
            if (ctrl.GetType().Equals(typeof(TextBox)) == true)
            {
                //((TextBox)(ctrl)).BackColor = bkColor;
                return;
            }

            // 类型 ListVie
            if (ctrl.GetType().Equals(typeof(ListView)) == true)
            {
                ((ListView)(ctrl)).BackColor = bkColor;
                return;
            }

            // 类型 TabPageControl
            if (ctrl.GetType().Equals(typeof(TabControl)) == true)
            {
                TabControl tabCtrl = (TabControl)ctrl;
                foreach (TabPage tabPage in tabCtrl.TabPages)
                {
                    setControlBackColor(tabPage, bkColor);
                }

                return;
            }

            // 类型TabPage
            if (ctrl.GetType().Equals(typeof(TabPage)) == true)
            {
                TabPage tabPage = (TabPage)ctrl;
                foreach (Control ctrlIn in tabPage.Controls)
                {
                    setControlBackColor(ctrlIn, bkColor);
                }

                return;
            }

            // 类型 Form
            Form frm = ctrl as Form;
            if (frm != null)
            {
                frm.BackColor = bkColor;
                return;
            }

            // 用户控件
            UserControl userCtrl = ctrl as UserControl;
            if (userCtrl != null)
            {
                ctrl.BackColor = bkColor;

                foreach (Control ctrlIn in userCtrl.Controls)
                {
                    setControlBackColor(ctrlIn, bkColor);
                }

                return;
            }

            // 其它内容控件
            if (ctrl.Controls.Count > 0)
            {
                foreach (Control ctrlIn in ctrl.Controls)
                {
                    setControlBackColor(ctrlIn, bkColor);
                }

                return;
            }
        }
        #endregion
    }
}
