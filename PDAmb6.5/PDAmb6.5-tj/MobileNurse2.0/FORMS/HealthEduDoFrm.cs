using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class HealthEduDoFrm : Form
    {
        #region 变量
        private readonly string DICT_MASTER_DEGREE = "10";              // 掌握情况
        private readonly string DICT_EDU_METHOD    = "11";              // 教育方法
        private readonly string DICT_PRECONDITION  = "12";              // 准备情况
        private readonly string DICT_EDU_CLOG      = "13";              // 学习障碍
        
        protected string _eduObject         = string.Empty;             // 教育对象
        protected string _eduPreCondition   = string.Empty;             // 准备情况
        protected string _eduMethod         = string.Empty;             // 教育方法
        protected string _masterDegree      = string.Empty;             // 掌握情况
        protected string _eduClog           = string.Empty;             // 学习障碍
        protected string _memo              = string.Empty;             // 备注
        protected bool   _blnView           = true;                     // 仅查看
        
        private DataSet dsDictItem          = null;                     // 字典项目
        #endregion
        
        public HealthEduDoFrm()
        {
            InitializeComponent();
        }
        
        
        #region  属性
        /// <summary>
        /// 教育对象
        /// </summary>
        public string EduObject
        {
            get { return _eduObject; }
            set { _eduObject = value; }
        }
        
        
        /// <summary>
        /// 教育方法
        /// </summary>
        public string EduMethod
        {
            get { return _eduMethod; }
            set { _eduMethod = value; }
        }
        
        
        /// <summary>
        /// 准备情况
        /// </summary>
        public string EduPrecondition
        {
            get { return _eduPreCondition; }
            set { _eduPreCondition = value; }
        }
        
        
        /// <summary>
        /// 掌握情况
        /// </summary>
        public string EduMasterDegree
        {
            get { return _masterDegree; }
            set { _masterDegree = value; }
        }
        
        
        /// <summary>
        /// 学习障碍
        /// </summary>
        public string EduClog
        {
            get { return _eduClog; }
            set { _eduClog = value; }
        }
        
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
        
        
        /// <summary>
        /// 是否仅查看
        /// </summary>
        public bool OnlyView
        {
            get { return _blnView; }
            set { _blnView = value; }
        }
        #endregion
        
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HealthEduDoFrm_Load(object sender, EventArgs e)
        {   
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;
                
                // 获取数据
                string sql = "SELECT * FROM HIS_DICT_ITEM WHERE DICT_ID IN ('10','11','12','13')";
                dsDictItem = GVars.sqlceLocal.SelectData(sql, "HIS_DICT_ITEM");
                
                // 初始化显示
                initDisp();
                
                // 权限设置
                mnuOk.Enabled = !_blnView;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }
        
        
        /// <summary>
        /// 初始化显示
        /// </summary>
        private void initDisp()
        {
            if (dsDictItem == null && dsDictItem.Tables.Count == 0)
            {
                return;
            }
            
            string filterFmt = "DICT_ID = '{0}'";
            
            // 教育方法
            DataRow[] drFind = dsDictItem.Tables[0].Select(string.Format(filterFmt, DICT_EDU_METHOD), "ITEM_ID");
            
            cmbEduMethod.Items.Clear();
            for(int i = 0; i < drFind.Length; i++)
            {
                cmbEduMethod.Items.Add(drFind[i]["ITEM_NAME"].ToString());
            }
            
            cmbEduMethod.Text = _eduMethod;
            
            // 准备情况
            drFind = dsDictItem.Tables[0].Select(string.Format(filterFmt, DICT_PRECONDITION), "ITEM_ID");
            
            cmbPrecondition.Items.Clear();
            for(int i = 0; i < drFind.Length; i++)
            {
                cmbPrecondition.Items.Add(drFind[i]["ITEM_NAME"].ToString());
            }
            
            cmbPrecondition.Text = _eduPreCondition;
            
            // 掌握情况
            drFind = dsDictItem.Tables[0].Select(string.Format(filterFmt, DICT_MASTER_DEGREE), "ITEM_ID");
            
            cmbMasterDegree.Items.Clear();
            for(int i = 0; i < drFind.Length; i++)
            {
                cmbMasterDegree.Items.Add(drFind[i]["ITEM_NAME"].ToString());
            }
            
            cmbMasterDegree.Text = _masterDegree;
            
            // 学习障碍
            drFind = dsDictItem.Tables[0].Select(string.Format(filterFmt, DICT_EDU_CLOG), "ITEM_ID");
                        
            cmbEduClog.Items.Clear();
            for(int i = 0; i < drFind.Length; i++)
            {
                cmbEduClog.Items.Add(drFind[i]["ITEM_NAME"].ToString());
            }
            
            cmbEduClog.Text = _eduClog;
            
            // 教育对明
            cmbEduObject.Text   = _eduObject;            
            
            // 备注
            txtMemo.Text = _memo;
        }
        
        
        /// <summary>
        /// 菜单[返回]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 菜单[确定]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuOk_Click(object sender, EventArgs e)
        {
            try
            {
                // 条件检查
                // 教育对象
                if (cmbEduObject.Text.Trim().Length == 0)
                {
                    cmbEduObject.Focus();
                    return;
                }
                
                // 教育方法
                //if (cmbEduMethod.Text.Trim().Length == 0)
                //{
                //    cmbEduMethod.Focus();
                //    return;
                //}
                
                // 获取数据
                _eduClog        = cmbEduClog.Text;                      // 学习障碍
                _eduMethod      = cmbEduMethod.Text;                    // 教育方法
                _eduObject      = cmbEduObject.Text;                    // 教育对象
                _eduPreCondition= cmbPrecondition.Text;                 // 准备情况
                _masterDegree   = cmbMasterDegree.Text;                 // 掌握情况
                _memo           = txtMemo.Text.Trim();                  // 备注
                
                // 返回
                DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 记录改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void content_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                mnuOk.Enabled = !_blnView;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
    }
}