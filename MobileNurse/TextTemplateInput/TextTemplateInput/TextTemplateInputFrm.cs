using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    //public partial class TextTemplateInputFrm : Form
    public partial class TextTemplateInputFrm : FormDo
    {
        #region 变量
        private const int LEVEL_LEN = 2;                        // 节点分层长度
        protected string _dictId = "16";                     // 字典ID
        private bool dictChanged = false;

        private DbInfo dbAccess = null;
        private DataSet dsDictItem = null;                     // 字典项目
        private DataSet dsDictItemContent = null;                     // 字典项目内容

        public int MaxLength = -1;
        public int ColWidth = -1;                       // 列宽
        public string TextEdit = string.Empty;             // 编辑的文本
        public string[] Lines = null;                     // 返回的结果


        #endregion


        #region 属性
        public string DictId
        {
            get
            {
                return _dictId;
            }
            set
            {
                if (_dictId.Equals(value) == false)
                {
                    _dictId = value;
                    dictChanged = true;
                }
            }
        }
        #endregion


        public TextTemplateInputFrm()
        {
            _id = "00102";
            _guid = "7F029E68-778D-4b6c-A932-B066D5FC5AC5";

            InitializeComponent();

            txtEdit.TextChanged += txtEdit_TextChanged;
        }

        void txtEdit_TextChanged(object sender, EventArgs e)
        {
            var maxChars = txtEdit.MaxLength;

            lblTextCount.Text = Coding.GetStrByteLen(txtEdit.Text.TrimEnd()) + @"/" + maxChars;

            textBox1.Text = txtEdit.Text;
        }


        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextTemplateInputFrm_Load(object sender, EventArgs e)
        {
            try
            {
                this.timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 在时钟事件中守成初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                this.timer1.Enabled = false;
                initFrmVal();
                initDisp();

                txtEdit.MaxLength = MaxLength;
                txtEdit.MaxCharLength = MaxLength;

                txtEdit.Text = TextEdit;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 节点选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvTemplate_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                GVars.App.UserInput = false;

                txtTemplateContent.Text = string.Empty;

                if (trvTemplate.SelectedNode == null)
                {
                    return;
                }

                string nodeId = trvTemplate.SelectedNode.Tag.ToString();

                if (nodeId.Length > 0)
                {
                    string filter = "ITEM_ID = " + SQL.SqlConvert(nodeId);
                    DataRow[] drFind = dsDictItemContent.Tables[0].Select(filter);

                    if (drFind.Length > 0)
                    {
                        txtTemplateContent.Text = ConvertLfToCrlf(drFind[0]["CONTENT"].ToString());
                    }
                }
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


        /// <summary>
        /// 编辑模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                TextTemplateEditFrm templateEditFrm = new TextTemplateEditFrm();
                templateEditFrm.DictId = _dictId;
                templateEditFrm.dsDictItem = dsDictItem;
                templateEditFrm.dsDictItemContent = dsDictItemContent;

                templateEditFrm.ShowDialog();

                if (templateEditFrm.TemplateChanged == true)
                {
                    txtTemplateContent.Text = string.Empty;

                    resetTree();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[插入]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                string textBefore = txtEdit.Text.Substring(0, txtEdit.SelectionStart);
                string textAfter = txtEdit.Text.Substring(txtEdit.SelectionStart + txtEdit.SelectionLength);

                string substring = textBefore
                    + (txtTemplateContent.SelectedText.Length > 0 ? txtTemplateContent.SelectedText : txtTemplateContent.Text)
                    + textAfter;
                //Coding.GetStrByteLen(txtEdit.Text.TrimEnd())
                if (substring.Length > MaxLength)
                    substring = substring.Substring(0, MaxLength);


                int len = MaxLength;
                while (Coding.GetStrByteLen(substring.TrimEnd()) > MaxLength)
                {
                    substring = substring.Substring(0, len--);
                }
                txtEdit.Text = substring;

                txtEdit.SelectionStart = txtEdit.Text.Length - textAfter.Length;

                txtEdit.Focus();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[确定]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            //是否合并
            string strSql = "SELECT A.PARAMETER_VALUE FROM MOBILE.APP_CONFIG A  WHERE A.PARAMETER_NAME='PRINT_PAGE_ROWS_SET'  ";
            DataSet ds = GVars.OracleAccess.SelectData(strSql);

            string _isHb = (ds != null && ds.Tables[0].Rows.Count > 0) ? ds.Tables[0].Rows[0][0].ToString() : "";
            _isHb = string.IsNullOrEmpty(_isHb) ? "1" : _isHb;
            
            try
            {
                // 检查输入字符串长度
                if (MaxLength > 0)
                {
                    if (Coding.GetStrByteLen(txtEdit.Text.TrimEnd()) > MaxLength)
                    {
                        GVars.Msg.Show("E0014", groupBox4.Text);        // {0}输入超长
                        txtEdit.Focus();
                        return;
                    }
                }

                // 返回输入结果
                txtEdit.Text = txtEdit.Text.TrimEnd();
                TextEdit = txtEdit.Text;

                GetLines(_isHb);        //1:分开   2:合并 

                // 获取每一行的内容

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[取消]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion


        #region 共通函数
        /// <summary>
        /// 初始化窗体变量
        /// </summary>
        private void initFrmVal()
        {
            dbAccess = new DbInfo(GVars.OracleAccess);

            // 获取参数
            _userRight = GVars.User.GetUserFrmRight(_id);

            string filter = "DICT_ID = " + SQL.SqlConvert(_dictId);

            if (dictChanged == true || dsDictItem == null)
            {
                dsDictItem = dbAccess.GetTableData("HIS_DICT_ITEM", filter);
                dsDictItemContent = dbAccess.GetTableData("HIS_DICT_ITEM_CONTENT", filter);

                dictChanged = false;
            }
        }


        /// <summary>
        /// 初始化窗体显示
        /// </summary>
        private void initDisp()
        {
            if (_userRight.IndexOf("2") >= 0)
            {
                btnEditTemplate.Enabled = true;
            }
            else
            {
                btnEditTemplate.Enabled = false;
            }

            // 重置树
            resetTree();

            // 设置窗体宽度
            if (ColWidth > -1)
            {
                this.Width -= (txtEdit.Width - ColWidth);
            }
        }


        /// <summary>
        /// 重新树
        /// </summary>
        private void resetTree()
        {
            // 清除所有节点
            trvTemplate.Nodes.Clear();          // 清空所有节点

            // 条件检查
            if (dsDictItem.Tables.Count == 0 || dsDictItem.Tables[0].Rows.Count == 0)
            {
                return;
            }

            // 增加根节点           
            DataRow[] rows = dsDictItem.Tables[0].Select("PARENT_ID IS NULL OR PARENT_ID = ''", "SERIAL_NO");

            foreach (DataRow dr in rows)
            {
                TreeNode nodeNew = new TreeNode(dr["ITEM_NAME"].ToString());
                nodeNew.Tag = dr["ITEM_ID"].ToString();

                trvTemplate.Nodes.Add(nodeNew);

                // 增加子节点
                addTreeSubNodes(nodeNew);
            }
        }


        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="parentNode"></param>
        private void addTreeSubNodes(TreeNode parentNode)
        {
            string parentNodeId = parentNode.Tag.ToString();

            string filter = "PARENT_ID = " + SQL.SqlConvert(parentNodeId);
            DataRow[] rows = dsDictItem.Tables[0].Select(filter, "SERIAL_NO");

            foreach (DataRow dr in rows)
            {
                TreeNode nodeNew = new TreeNode(dr["ITEM_NAME"].ToString());
                nodeNew.Tag = dr["ITEM_ID"].ToString();

                parentNode.Nodes.Add(nodeNew);

                // 增加子节点
                addTreeSubNodes(nodeNew);
            }
        }


        /// <summary>
        /// 获取每一行的内容
        /// </summary>
        private void GetLines(string m)
        {
            if (m == "1")
            {
                int lineCount = Win32API.SendMessageA((int)(textBox1.Handle), Win32API.EM_GETLINECOUNT, 0, 0);

                Lines = new string[lineCount];

                int pos0 = 0;
                int pos1 = 0;
                for (int i = 1; i < lineCount; i++)
                {
                    pos1 = Win32API.SendMessageA((int)(textBox1.Handle), Win32API.EM_LINEINDEX, i, 0);
                    if (pos1 > -1)
                    {
                        Lines[i - 1] = txtEdit.Text.Substring(pos0, pos1 - pos0);
                        pos0 = pos1;
                    }
                }

                Lines[lineCount - 1] = TextEdit.Substring(pos0);
            }
            else
            {
                Lines = new string[1];
                Lines[0] = txtEdit.Text;
            }
        }
        #endregion

        /// <summary>
        /// 功能：换行转回车+换行
        /// 创建：2015.11.19
        /// </summary>
        /// <param name="sSour"></param>
        /// <returns>转化后的字符串</returns>
        private string ConvertLfToCrlf(string sSour)
        {
            
            string sDesc = string.Empty;

            if (!string.IsNullOrEmpty(sSour))
            {

                if (sSour[0] == '\n')
                    sDesc += '\r';
                sDesc += sSour[0];
                for (int i = 1; i < sSour.Length; i++)
                {
                    if (sSour[i] == '\n')
                    {
                        if (sSour[i - 1] != '\r')
                            sDesc += "\r";
                    }
                    sDesc += sSour[i];
                }
                return sDesc;
            }
            else
            {
                return sDesc;
            }
        }
    }
}
