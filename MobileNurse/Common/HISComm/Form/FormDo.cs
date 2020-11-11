using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;

namespace HISPlus
{
    public delegate void DataChanged();

    public class FormDo : XtraForm//RibbonForm
    {
        #region 变量
        protected string _id;                   // 窗体ID
        protected string _version;              // 版本号
        protected string _guid;                 // 窗体的GUID
        protected string _right;                // 支持的权限
        protected string _userRight;            // 当前用户具有权限        
        #endregion

        //protected System.Windows.Forms.Cursor cursor;
        /// <summary>
        /// FormDo 摘要说明
        /// </summary>
        public FormDo()
        {
            this.KeyPreview = true;

            this.KeyDown += new KeyEventHandler(FormDo_KeyDown);
            this.MouseUp += new MouseEventHandler(FormDo_MouseUp);

            //this.Icon=
            this.Load += new EventHandler(FormDo_Load2);

            //设定按字体来缩放控件  
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ////设定字体大小为12px       
            //this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));  
        }

        //public override sealed Font Font
        //{
        //    get { return base.Font; }
        //    set { base.Font = value; }
        //}


        void FormDo_Load2(object sender, EventArgs e)
        {
            if (!File.Exists(@"Resource\ICON\logo.ico")) return;

            Icon applicationLogo;
            using (Bitmap b = new Bitmap(Image.FromFile(@"Resource\ICON\logo.ico")))
            {
                applicationLogo = Icon.FromHandle(b.GetHicon());
            }
            if (applicationLogo != null)
            {
                this.Icon = applicationLogo;
            }

            return;

            foreach (Control ctl in this.Controls)
            {
                if (ctl is DataGridView)
                {
                    DataGridView dgvModule = ctl as DataGridView;
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    DataGridViewCellStyle style2 = new DataGridViewCellStyle();
                    DataGridViewCellStyle style3 = new DataGridViewCellStyle();
                    style.BackColor = Color.FromArgb(0xf3, 0xf9, 0xff);
                    //style.BackColor = Color.Red;
                    dgvModule.AlternatingRowsDefaultCellStyle = style;
                    //dgvModule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvModule.BackgroundColor = Color.White;
                    dgvModule.BorderStyle = BorderStyle.Fixed3D;
                    style2.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    style2.BackColor = SystemColors.Control;
                    style2.Font = new Font("宋体", 9f);
                    style2.ForeColor = SystemColors.WindowText;
                    style2.SelectionBackColor = SystemColors.Highlight;
                    style2.SelectionForeColor = SystemColors.HighlightText;
                    //style2.WrapMode = DataGridViewTriState.True;
                    dgvModule.ColumnHeadersDefaultCellStyle = style2;
                    dgvModule.ColumnHeadersHeight = 0x18;
                    dgvModule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                    style3.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    style3.BackColor = SystemColors.Window;
                    style3.Font = new Font("宋体", 9f);
                    style3.ForeColor = Color.FromArgb(0x37, 0x47, 0x60);
                    style3.SelectionBackColor = SystemColors.Highlight;
                    style3.SelectionForeColor = SystemColors.HighlightText;
                    style3.WrapMode = DataGridViewTriState.False;
                    dgvModule.DefaultCellStyle = style3;
                    dgvModule.EnableHeadersVisualStyles = false;
                    dgvModule.RowHeadersWidth = 15;
                    dgvModule.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                    dgvModule.RowTemplate.Height = 20;
                    //dgvModule.ScrollBars = ScrollBars.Vertical;
                    dgvModule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
            }

            //this.BackColor = Color.FromArgb(244, 249, 253);
            //this.BackColor = SystemColors.Control;
            //SetWinBackColor();
        }

        /// <summary>
        /// 开始等待
        /// </summary>
        protected void LoadingShow()
        {
            if (SplashScreenManager.Default == null)
                SplashScreenManager.ShowForm(this.FindForm(), typeof(WaitForm1), false, true);
        }

        /// <summary>
        /// 结束等待
        /// </summary>
        protected void LoadingClose()
        {
            if (SplashScreenManager.Default != null)
                if (SplashScreenManager.Default.IsSplashFormVisible)
                    SplashScreenManager.CloseForm();
        }


        /// <summary>
        /// 开始等待
        /// </summary>
        protected void LoadingShowScreen()
        {
            SplashScreenManager.ShowForm(this.FindForm(), typeof(SplashScreen1), false, true);
        }

        /// <summary>
        /// 结束等待
        /// </summary>
        protected void LoadingCloseScreen()
        {
            SplashScreenManager.CloseForm();
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
                    infoFrm.ShowFormInfo(_id, _version + Environment.NewLine + this.Name, _guid, _right, _userRight);

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
        /// 将绑定数据源和控件绑定
        /// </summary>
        /// <param name="ctl">控件</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="BindingSource">数据源</param>
        /// <param name="fieldName">绑定字段</param>
        public void SetBinding(Control ctl, string propertyName, object BindingSource, string fieldName)
        {
            if (ctl.DataBindings[propertyName] != null) ctl.DataBindings.Remove(ctl.DataBindings[propertyName]);
            //ctl.DataBindings.Add(propertyName, BindingSource, fieldName, true, DataSourceUpdateMode.OnPropertyChanged);
            ctl.DataBindings.Add(propertyName, BindingSource, fieldName);
        }

        /// <summary>
        /// 将绑定数据源和控件绑定,默认绑定Text属性
        /// </summary>
        /// <param name="ctl">控件</param>
        /// <param name="bindingSource">数据源</param>
        /// <param name="fieldName">绑定字段</param>
        public void SetBinding(Control ctl, object bindingSource, string fieldName)
        {
            SetBinding(ctl, "Text", bindingSource, fieldName);
        }

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
