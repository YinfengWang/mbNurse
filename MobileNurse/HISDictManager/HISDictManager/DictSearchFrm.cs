using System;
using System.Data;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class DictSearchFrm : FormDo
    {
        #region 变量
        private HISDictDbI _dictDbI;

        private DataSet _dsDict;                         // 字典表

        private string _dictId = string.Empty;
        private string _dictName = string.Empty;
        #endregion


        #region 属性
        /// <summary>
        /// 属性 [字典ID]
        /// </summary>
        public string DictId
        {
            get { return _dictId; }
        }


        /// <summary>
        /// 属性 [字典名称]
        /// </summary>
        public string DictName
        {
            get { return _dictName; }
        }
        #endregion


        public DictSearchFrm()
        {
            InitializeComponent();

            _id = "00045";
            _guid = "71F1B4CE-A15A-44ad-A66C-5730AB75ED0B";
        }


        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DictSearchFrm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
               
                // 获取字典表
                _dictDbI = new HISDictDbI(GVars.OracleAccess);
                _dsDict = _dictDbI.GetDictList();

                ucGridView1.Add("字典ID", "DICT_ID",10);
                ucGridView1.Add("字典名称", "DESCRIPTIONS",20);
                ucGridView1.DoubleClick += ucGridView1_DoubleClick;
                ucGridView1.ShowFindPanel = true;
                ucGridView1.Init();

                ucGridView1.DataSource = _dsDict.Tables[0].DefaultView;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        void ucGridView1_DoubleClick(object sender, EventArgs e)
        {
            CloseForm();
        }


        private void CloseForm()
        {
            _dictId = ucGridView1.SelectedRow["DICT_ID"].ToString();
            _dictName = ucGridView1.SelectedRow["DESCRIPTIONS"].ToString();

            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 按钮[取消]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CloseForm();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
    }
}
