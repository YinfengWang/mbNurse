using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BSE.Windows.Forms;

namespace HISPlus
{
    public partial class OutlookBarFrm : Form
    {
        #region 窗体变量
        public MenuStrip                MnuStrip    = null;        
        #endregion
        
        
        public OutlookBarFrm()
        {
            InitializeComponent();
        }

        
        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutlookBarFrm_Load(object sender, EventArgs e)
        {
            try
            {
                initDisp();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }        
        
        
        #region 菜单控制
        void lblSubMenu_Click(object sender, EventArgs e)
        {
            try
            {
                ListView  lvwMenu  = sender as ListView;
                
                if (lvwMenu != null && lvwMenu.SelectedItems.Count > 0)
                {
                    ToolStripItem mnuItem = (ToolStripItem)(lvwMenu.SelectedItems[0].Tag);
                    mnuItem.PerformClick();
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion

        #endregion


        #region  共通函数
        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {
            createlblMenuFromMenu(ref MnuStrip);            
        }
        
        
        /// <summary>
        /// 从Menu菜单中创建OutlookBar的主菜单按钮
        /// </summary>
        /// <param name="mnuStrip"></param>
        private void createlblMenuFromMenu(ref MenuStrip mnuStrip)
        {
            XPanderPanel xpPanel = null;
            
            for (int i = 0; i < mnuStrip.Items.Count; i++)
            {   
                ToolStripItem mnuItem = mnuStrip.Items[i];
                
                if (mnuItem.Enabled == false)
                {
                    continue;
                }
                
                xpPanel = new XPanderPanel();
                xpPanelGroup1.Controls.Add(xpPanel);
                
                xpPanel.BackColor = Color.Transparent;
                xpPanel.CaptionFont = new Font("Arial", 10.5F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                xpPanel.CaptionForeColor = Color.FromArgb(0, 17, 96);
                //xpPanel.ColorCaptionGradientBegin = Color.FromArgb(127, 177, 250);
                //xpPanel.ColorCaptionGradientEnd = Color.FromArgb(0, 53, 145);
                //xpPanel.ColorCaptionGradientMiddle = Color.FromArgb(82, 127, 208);
                
                xpPanel.ColorCaptionGradientBegin = Color.FromArgb(157, 192, 232);
                xpPanel.ColorCaptionGradientEnd = Color.FromArgb(57, 114, 191);
                xpPanel.ColorCaptionGradientMiddle = Color.FromArgb(157, 192, 232);
                xpPanel.ColorScheme = ColorScheme.Custom;
                                
                xpPanel.Font = new Font("宋体", 10.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                xpPanel.ForeColor = Color.Blue;
                xpPanel.Image = mnuItem.Image;
                xpPanel.Padding = new Padding(0, 25, 0, 0);
                xpPanel.TabIndex = 0;
                xpPanel.Text = mnuItem.Text;
                
                // 子菜单
                createlblSubMenuFromMenu(ref xpPanel, ref mnuItem);

                xpPanel.Resize += new EventHandler(xpPanel_Resize);                
            }
            
            if (xpPanelGroup1.Controls.Count > 0)
            {
                xpPanel = xpPanelGroup1.Controls[0] as XPanderPanel;
                if (xpPanel != null)
                {
                    xpPanel.Expand = true;
                }
            }
        }

        void xpPanel_Resize(object sender, EventArgs e)
        {
            XPanderPanel pnl = (XPanderPanel)sender;
            
            if (pnl.Height <= pnl.CaptionHeight + 24 || pnl.Tag != null)
            {
                return;
            }
            
            foreach(Control ctrl in pnl.Controls)
            {
                if (ctrl.GetType().Equals(typeof(ListView)) == true)
                {
                    ListView lv = (ListView)ctrl;
                    lv.Top = pnl.CaptionHeight + 12;
                    lv.Height = pnl.Height - pnl.CaptionHeight - 24;
                    break;
                }
            }
            
            pnl.Tag = "S";
        }
        
        
        private void createlblSubMenuFromMenu2(ref XPanderPanel xpPanel, ref ToolStripItem mnuItem)
        {
            // 创建一个包含的Panel
            System.Windows.Forms.Panel pnl = new System.Windows.Forms.Panel();
            pnl.BackColor = Color.FromArgb(221, 239, 249);
            pnl.Width     = xpPanel.Width;
            pnl.Height    = 200;
            
            // 创建ListView
            ListView lvwMenu = new ListView();
            pnl.Controls.Add(lvwMenu);
            
            lvwMenu.BackColor = Color.FromArgb(242, 247, 253);
            lvwMenu.BorderStyle = BorderStyle.None;
            lvwMenu.FullRowSelect = true;
            lvwMenu.HideSelection = true;
            lvwMenu.LabelWrap = false;
            lvwMenu.LargeImageList = this.imageList1;
            lvwMenu.SmallImageList = this.imageList1;
            lvwMenu.UseCompatibleStateImageBehavior = false;            
            lvwMenu.Click += new EventHandler(lblSubMenu_Click);
            
            // 加载ListViewItem
            lvwMenu.AutoArrange = true;
            lvwMenu.Width = 96;
            lvwMenu.Height = pnl.Height - 24;
            
            ToolStripMenuItem mnuParent = (ToolStripMenuItem)mnuItem;
            ListViewItem item = null;
            
            for(int i = 0; i < mnuParent.DropDownItems.Count; i++)
            {
                ToolStripItem mnuSubItem = mnuParent.DropDownItems[i];
                
                if (mnuSubItem.Enabled == false)
                {
                    continue;
                }
                
                if (mnuSubItem.Image != null)
                {
                    imageList1.Images.Add(mnuSubItem.Image);
                    item = lvwMenu.Items.Add(mnuSubItem.Text, imageList1.Images.Count - 1);
                }
                else
                {
                    item = lvwMenu.Items.Add(mnuSubItem.Text);
                }
                
                item.ForeColor = Color.Blue;
                item.Tag = mnuSubItem;
            }

            lvwMenu.AutoArrange = false;
            lvwMenu.Location = new Point(6, 6);
            lvwMenu.Width    = xpPanel.Width - 12;
            lvwMenu.Anchor   = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            
            // Panel定位
            xpPanel.Controls.Add(pnl);
            pnl.Dock = DockStyle.Fill;
        }
        

        private void createlblSubMenuFromMenu(ref XPanderPanel xpPanel, ref ToolStripItem mnuItem)
        {
            // 创建一个包含的Panel
            xpPanel.BackColor = Color.FromArgb(221, 239, 249);
            
            // 创建ListView
            ListView lvwMenu = new ListView();
            xpPanel.Controls.Add(lvwMenu);
            
            lvwMenu.BackColor = Color.FromArgb(242, 247, 253);
            lvwMenu.BorderStyle = BorderStyle.None;
            lvwMenu.FullRowSelect = true;
            lvwMenu.HideSelection = true;
            lvwMenu.LabelWrap = false;
            lvwMenu.LargeImageList = this.imageList1;
            lvwMenu.SmallImageList = this.imageList1;
            lvwMenu.UseCompatibleStateImageBehavior = false;            
            lvwMenu.Click += new EventHandler(lblSubMenu_Click);
            
            // 加载ListViewItem
            lvwMenu.AutoArrange = true;
            lvwMenu.Width = 96;
            lvwMenu.Height = xpPanel.Height -  xpPanel.CaptionHeight - 24;
            
            ToolStripMenuItem mnuParent = (ToolStripMenuItem)mnuItem;
            ListViewItem item = null;
            
            for(int i = 0; i < mnuParent.DropDownItems.Count; i++)
            {
                ToolStripItem mnuSubItem = mnuParent.DropDownItems[i];
                
                if (mnuSubItem.Enabled == false)
                {
                    continue;
                }
                
                if (mnuSubItem.Image != null)
                {
                    imageList1.Images.Add(mnuSubItem.Image);
                    item = lvwMenu.Items.Add(mnuSubItem.Text, imageList1.Images.Count - 1);
                }
                else
                {
                    item = lvwMenu.Items.Add(mnuSubItem.Text);
                }
                
                item.ForeColor = Color.Blue;
                item.Tag = mnuSubItem;
            }

            lvwMenu.AutoArrange = false;
            lvwMenu.Location = new Point(6, xpPanel.CaptionHeight);
            lvwMenu.Width    = xpPanel.Width - 12;
            lvwMenu.Anchor   = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
        }        
        #endregion


        #region 接口
        public void SelectButton(int index)
        {
        }


        public void Reset()
        {         
        }
        #endregion
    }
}