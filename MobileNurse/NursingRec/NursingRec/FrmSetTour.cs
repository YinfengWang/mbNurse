using System;
using System.Data;
using System.Windows.Forms;

using SQL = HISPlus.SqlManager;


namespace HISPlus
{
    public partial class FrmSetTour : FormDo
    {


        #region 变量

        DataSet dsCounts = null;

        #endregion

        #region 源石化
        public FrmSetTour()
        {
            InitializeComponent();
        }
        #endregion

        #region  窗体初始化
        private void FrmSetTour_Load(object sender, EventArgs e)
        {
            iniDept();

        }

        /// <summary>
        /// 初始化显示
        /// </summary>
        private void iniDept()
        {
            dsCounts = Getcustomcontent(GVars.User.DeptCode);

            dataGridVCou.DataSource = dsCounts.Tables[0].DefaultView;
            dataGridVCou.Columns[0].HeaderText = "科室";
            dataGridVCou.Columns[1].HeaderText = "巡视内容";

            dataGridVCou.AllowUserToAddRows = false;
            dataGridVCou.AllowUserToDeleteRows = false;
        }
        #endregion

        #region 保存按钮
        private void bntServe_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (string.IsNullOrWhiteSpace(txtCount.Text))
                    MessageBox.Show("巡视内容不允许为空。");

                // 保存数据
                if (saveData())
                {
                    SaveCustomContent(ref  dsCounts);
                    return;
                }
                iniDept();
                txtCount.Text = "";
                this.bntServe.Enabled = false;

            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region  保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        private bool saveData()
        {
            bool aa = true;
            string xunshiContent = txtCount.Text.Trim();
            DataRow[] drFind = dsCounts.Tables[0].Select("xunshiContent =" + SQL.SqlConvert(xunshiContent));
            DataRow drEdit = null;
            if (drFind.Length == 0)
            {
                drEdit = dsCounts.Tables[0].NewRow();
            }
            else
            {
                drEdit = drFind[0];
            }
            if (txtCount.Text.Length > 0)
            {
                drEdit["WARD_CODE"] = GVars.User.DeptCode;
                drEdit["xunshiContent"] = txtCount.Text.Trim();
            }
            if (drFind.Length == 0)
            {
                dsCounts.Tables[0].Rows.Add(drEdit);
            }
            else
            {
                MessageBox.Show("对不起，已经存在相同巡视内容 请认真检查！");
                aa = false;
            }

            return aa;
        }

        #endregion

        #region 退出 按钮
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region  数据层操作

        /// <summary>
        /// 查询巡视内容
        /// </summary>
        /// <returns></returns>
        public DataSet Getcustomcontent(string wordCode)
        {
            string filter = "select * from  CustomContent  where  ward_code =" + SQL.SqlConvert(wordCode);

            return GVars.OracleAccess.SelectData(filter, "CustomContent");
        }

        /// <summary>
        /// 保存巡视内容
        /// </summary>
        /// <param name="dsNursingEvents"></param>
        /// <returns></returns>
        public bool SaveCustomContent(ref DataSet dsCustomContent)
        {
            string filter = "select * from  CustomContent";
            GVars.OracleAccess.Update(ref dsCustomContent, "CustomContent", filter);
            return true;
        }

        #endregion

        #region   繁盛边哈u
        private void txtCount_TextChanged(object sender, EventArgs e)
        {
            bntServe.Enabled = true;

        }
        #endregion

        private void dataGridVCou_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridVCou.SelectedRows.Count > 0)
            {
                btnDelete.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string ward_code = dataGridVCou.SelectedRows[0].Cells[0].Value.ToString();
            string xunShiContent = dataGridVCou.SelectedRows[0].Cells[1].Value.ToString();
            string strDql = " DELETE CUSTOMCONTENT A WHERE A.WARD_CODE='" + ward_code + "' AND A.XUNSHICONTENT='" + xunShiContent + "'";
            GVars.OracleAccess.ExecuteNoQuery(strDql);
            iniDept();
        }
    }
}
