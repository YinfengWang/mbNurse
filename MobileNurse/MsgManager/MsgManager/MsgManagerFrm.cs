using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class MsgManagerFrm : FormDo
    {
        #region 窗体变量
        private MsgTextDbI com = null;
        private DataSet dsMsg = null;

        private string filter = string.Empty;
        #endregion


        public MsgManagerFrm()
        {
            InitializeComponent();

            _id = "00005";
            _guid = "f1d1ef63-3e7a-43b4-9230-3aecdb3e11ad";
        }


        #region 窗体事件
        private void MsgManagerFrm_Load(object sender, EventArgs e)
        {
            try
            {
                initFrmVal();
                initDisp();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void txtItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                this.btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[新增]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                initDisp_MsgDetail(false);

                this.txtMsgId.Focus();

                this.btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[删除]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 条件检查
                if (txtMsgId.Text.Trim().Length == 0)
                {
                    txtMsgId.Focus();
                    return;
                }

                // 询问
                if (GVars.Msg.Show("Q00004", "消息") != DialogResult.Yes)       // 您确认要删除当前{0}吗?
                {
                    return;
                }

                // 删除
                string msgId = txtMsgId.Text.Trim();
                DataRow[] drFind = dsMsg.Tables[0].Select("MSG_ID = " + SqlManager.SqlConvert(msgId));

                if (drFind.Length > 0)
                {
                    drFind[0].Delete();

                    com.SaveMsgDict(ref dsMsg);

                    // 刷新显示
                    dsMsg.Tables[0].DefaultView.Sort = "MSG_TYPE, MODULE_CODE, MSG_ID";
                    dsMsg.Tables[0].DefaultView.RowFilter = filter;
                    ucGridView1.DataSource = dsMsg.Tables[0].DefaultView;
                }

                this.btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[查询]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                filter = string.Empty;

                // 获取查询条件
                if (cmbMsgType.SelectedIndex > -1)
                {
                    if (filter.Length > 0) filter += " AND ";
                    filter += "MSG_TYPE = " + SqlManager.SqlConvert(cmbMsgType.SelectedItem.ToString().Substring(0, 1));
                }

                if (txtMsgId.Text.Trim().Length > 0)
                {
                    if (filter.Length > 0) filter += " AND ";
                    filter += "MSG_ID LIKE '%" + txtMsgId.Text.Trim() + "%'";
                }

                if (txtModuleId.Text.Trim().Length > 0)
                {
                    if (filter.Length > 0) filter += " AND ";
                    filter += "MODULE_CODE = " + SqlManager.SqlConvert(txtModuleId.Text.Trim());
                }

                if (txtMsgText.Text.Trim().Length > 0)
                {
                    if (filter.Length > 0) filter += " AND ";
                    filter += "MSG_TEXT LIKE '%" + txtMsgText.Text.Trim() + "%'";
                }

                // 过滤并显示
                dsMsg.Tables[0].DefaultView.Sort = "MSG_TYPE, MODULE_CODE, MSG_ID";
                dsMsg.Tables[0].DefaultView.RowFilter = filter;
                ucGridView1.DataSource = dsMsg.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[保存]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkDisp() == false)
                {
                    return;
                }

                string msgId = txtMsgId.Text.Trim();

                // 保存数据到缓存
                saveDisp();

                // 保存数扰到DB中
                com.SaveMsgDict(ref dsMsg);

                // 刷新显示
                dsMsg.Tables[0].DefaultView.Sort = "MSG_TYPE, MODULE_CODE, MSG_ID";
                dsMsg.Tables[0].DefaultView.RowFilter = filter;
                ucGridView1.DataSource = dsMsg.Tables[0].DefaultView;

                // 定位当前行
                locate(getMsgType(), msgId);

                this.btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[下载]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsMsg = com.DownloadMsgDict();
                dsMsg.WriteXml("Msg.xml", XmlWriteMode.WriteSchema);

                GVars.Msg.Show("I0004");                                // 下载成功!
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion


        #region 共通函数
        private void initFrmVal()
        {
            com = new MsgTextDbI(GVars.OracleAccess);

            dsMsg = com.GetMsgDict();
        }


        private void initDisp()
        {
            dsMsg.Tables[0].DefaultView.Sort = "MSG_TYPE, MODULE_CODE, MSG_ID";

            ucGridView1.ShowRowIndicator = true;
            ucGridView1.Add("类型", "MSG_TYPE",10);
            ucGridView1.Add("消息ID", "MSG_ID",10);
            ucGridView1.Add("消息文本", "MSG_TEXT",200);
            ucGridView1.Add("窗口ID", "MODULE_CODE",10);

            ucGridView1.SelectionChanged += ucGridView1_SelectionChanged;

            ucGridView1.Init();

            ucGridView1.DataSource = dsMsg.Tables[0].DefaultView;

            this.cmbMsgType.Items.AddRange(new object[] {
            "Q: 询问",
            "I: 提示",
            "E: 错误",
            "W: 警告"});
        }

        void ucGridView1_SelectionChanged(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;    // 为了避免 触发文本框编辑事件

                // 初始化 应用程序详细信息的显示
                initDisp_MsgDetail(true);

                // 显示应用程序详细信息
                string msgId = ucGridView1.SelectedRow["MSG_ID"].ToString();
                string msgType = ucGridView1.SelectedRow["MSG_TYPE"].ToString();
                DataRow[] drFind = dsMsg.Tables[0].Select("MSG_ID = " + SqlManager.SqlConvert(msgId)
                    + " AND MSG_TYPE = " + SqlManager.SqlConvert(msgType));

                if (drFind.Length > 0)
                {
                    DataRow drMsg = drFind[0];
                    showMsgDetail(drMsg);
                }

                this.btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }


        private void initDisp_MsgDetail(bool allReset)
        {
            if (allReset == true)
            {
                cmbMsgType.SelectedIndex = -1;      // 类型
            }

            txtMsgId.Text = string.Empty; // ID
            txtModuleId.Text = string.Empty; // 窗口ID
            txtMsgText.Text = string.Empty; // 消息文本
        }


        private void showMsgDetail(DataRow drMsg)
        {
            setMsgType(drMsg["MSG_TYPE"].ToString());               // 类型

            txtMsgId.Text = drMsg["MSG_ID"].ToString();       // ID
            txtModuleId.Text = drMsg["MODULE_CODE"].ToString();  // 窗口ID
            txtMsgText.Text = drMsg["MSG_TEXT"].ToString();     // 消息文本
        }


        private bool chkDisp()
        {
            if (txtMsgId.Text.Trim().Length == 0)
            {
                txtMsgId.Focus();
                return false;
            }

            if (cmbMsgType.SelectedIndex == -1)
            {
                cmbMsgType.Focus();
                return false;
            }

            return true;
        }


        private bool saveDisp()
        {
            // 获取当前日期
            DateTime dtNow = GVars.OracleAccess.GetSysDate();

            // 查找记录
            string msgId = this.txtMsgId.Text.Trim();
            DataRow[] drFind = dsMsg.Tables[0].Select("MSG_ID = " + SqlManager.SqlConvert(msgId)
                + "AND MSG_TYPE = " + SqlManager.SqlConvert(getMsgType()));

            DataRow drEdit = null;
            if (drFind.Length == 0)
            {
                drEdit = dsMsg.Tables[0].NewRow();
            }
            else
            {
                drEdit = drFind[0];
            }

            // 保存数据
            drEdit["MSG_TYPE"] = this.cmbMsgType.SelectedItem.ToString().Substring(0, 1); // 类型
            drEdit["MSG_ID"] = this.txtMsgId.Text.Trim();                    // ID
            drEdit["MSG_TEXT"] = this.txtMsgText.Text.Trim();                  // 文本
            drEdit["MODULE_CODE"] = this.txtModuleId.Text.Trim();                 // 窗口ID

            if (drFind.Length == 0)
            {
                drEdit["CREATE_DATE"] = dtNow;                                    // 创建时间
                dsMsg.Tables[0].Rows.Add(drEdit);
            }

            drEdit["UPDATE_DATE"] = dtNow;                                    // 更新时间

            return true;
        }


        private void locate(string msgType, string msgId)
        {
            // 定位
            if (msgId.Length > 0)
            {
                for (int rowHandle = 0; rowHandle < ucGridView1.RowCount; rowHandle++)
                {
                    for (int j = 0; j < ucGridView1.Columns.Count; j++)
                    {
                        if (ucGridView1.GetRowCellValue(rowHandle, "MSG_TYPE").Equals(msgType) || ucGridView1.GetRowCellValue(rowHandle, "MSG_ID").Equals(msgId))
                        {
                            ucGridView1.SelectRow(rowHandle);
                            return;
                        }
                    }
                }
            }
        }


        private string getMsgType()
        {
            if (cmbMsgType.SelectedIndex > -1)
            {
                return cmbMsgType.SelectedItem.ToString().Substring(0, 1);
            }

            return string.Empty;
        }


        public void setMsgType(string msgType)
        {
            for (int i = 0; i < cmbMsgType.Items.Count; i++)
            {
                if (cmbMsgType.Items[i].ToString().StartsWith(msgType) == true)
                {
                    cmbMsgType.SelectedIndex = i;
                    break;
                }
            }
        }
        #endregion
    }
}
