using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class HealthEduFrm : Form
    {
        #region 变量
        private const string     STR_EDU      = "教  育";
        private const string     STR_CANCEL   = "撤销教育";
        private readonly string  _dictId      = "07";                   // 字典ID
        
        private PatientNavigator patNavigator = new PatientNavigator(); // 病人导航
        
        private HealthEduDbI     healthRecDbI = new HealthEduDbI();
        
        private DataSet          dsItems      = null;                   // 健康教育项目
        private DataRow[]        drItems      = null;
        private DataSet          dsRec        = null;                   // 健康教育记录
        private DataRow          drRec        = null;                   // 当前健康教育记录
        
        private ArrayList        arrNodes     = new ArrayList();
        private TreeNode         nodeSelected = null;
        #endregion
        
        
        public HealthEduFrm()
        {
            InitializeComponent();
        }
        
        
        #region 扫描器
        /// <summary>
        /// 扫描器 读取通知 事件的委托程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ScanReader_ReadNotify(object sender, EventArgs e)
        {
            #if SCANNER
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                // 获取病人ID号
                string barcode = GVars.ScanReaderBuffer.Text.Trim();
                
                // 如果不包含空格, 表示是病人的腕带
                if (barcode.IndexOf( ComConst.STR.BLANK) < 0 && barcode.IndexOf("T") < 0)
                {
                    if (patNavigator.ScanedPatient(barcode) == false) GVars.Msg.Show("W00005");   // 该病人不存在!
                    
                    return;
                }                
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                GVars.ScanReader.Actions.Read(GVars.ScanReaderBuffer);  // 再次开始等待扫描
            }
            #endif
        }
        #endregion        
        
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HealthEduFrm_Load(object sender, EventArgs e)
        {
            try
            {
                patNavigator.BtnPrePatient = this.btnPrePatient;
                patNavigator.BtnCurrentPatient = this.btnCurrPatient;
                patNavigator.BtnNextPatient = this.btnNextPatient;
                patNavigator.BtnPatientList = this.btnListPatient;
                
                patNavigator.MenuItemPatient = mnuPatient;
                
                patNavigator.PatientChanged = new DataChanged(patientChanged);
                
                this.timer1.Enabled = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 菜单[返回]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        
        
        /// <summary>
        /// 时钟消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                GVars.App.UserInput     = false;
                timer1.Enabled          = false;
                
                // 设置编辑按钮
                btnEducation.Enabled    = false;
                btnContent.Enabled      = false;
                
                // 设置病人导航按钮
                patNavigator.SetPatientButtons();
                
                // 获取数据
                dsItems = healthRecDbI.GetDictItem(_dictId);
                
                if (GVars.Patient.ID.Length > 0)
                {
                    dsRec = healthRecDbI.GetHealthEduRec(GVars.User.DeptCode, GVars.Patient.ID, GVars.Patient.VisitId);
                }
                
                // 显示数据
                showHealthEduRec();

                // 菜单控制
                foreach (MenuItem mnu in mnuNavigator.MenuItems)
                {
                    if (mnu.Text.IndexOf(this.Text) >= 0)
                    {
                        mnu.Enabled = false;
                    }
                    else
                    {
                        mnu.Click += new EventHandler(mnuNavigator_Func_Click);
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;                
                Cursor.Current = Cursors.Default;
            }
        }


        /// <summary>
        /// 菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuNavigator_Func_Click(object sender, EventArgs e)
        {
            try
            {
                MenuItem mnu = sender as MenuItem;
                if (mnu == null) return;

                this.Tag = mnu.Text;

                this.Close();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 节点Check事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvEduRec_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                //if (GVars.App.UserInput == true)
                //{
                //    e.Node.Checked = false;
                //}
                trvEduRec.SelectedNode = e.Node;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 节点选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void trvEduRec_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                // 记录当前节点
                if (nodeSelected != null && nodeSelected.Equals(e.Node) == false) 
                {
                    nodeSelected.ForeColor = Color.Black;
                }
                                
                e.Node.ForeColor = Color.Blue;
                nodeSelected = e.Node;
                
                // 条件检查
                if (e.Node.Nodes.Count > 0)
                {
                    btnContent.Enabled = false;
                    btnViewEdu.Enabled = false;
                    btnEducation.Enabled = false;
                    return;
                }
                
                // 正常处理
                drRec = null;
                
                DataRow dr = drItems[(int)(e.Node.Tag)];
                
                // 项目内容
                btnContent.Enabled = (e.Node.Nodes.Count == 0);
                btnViewEdu.Enabled = false;
                
                // 是否可以进行教育
                string itemId = dr["ITEM_ID"].ToString();
                string filter = "ITEM_ID = " + SqlManager.SqlConvert(itemId);
                
                DataRow[] drFind = dsRec.Tables[0].Select(filter);
                if (drFind.Length == 0)
                {
                    btnEducation.Enabled = true;
                    btnEducation.Text    = STR_EDU;
                }
                else
                {
                    drRec = drFind[0];
                    
                    btnEducation.Enabled = drRec["EDU_NURSE"].ToString().Equals(GVars.User.Name);
                    //rdoFamily.Checked    = (drRec["EDU_OBJECT"].ToString().Equals(rdoFamily.Text));
                    //rdoPatient.Checked   = (drRec["EDU_OBJECT"].ToString().Equals(rdoPatient.Text));
                    btnEducation.Text    = STR_CANCEL;
                    btnViewEdu.Enabled   = true;
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[查看/修改]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewEdu_Click(object sender, EventArgs e)
        {
            try
            {
                // 条件检查
                trvEduRec.Focus();
                if (trvEduRec.SelectedNode == null)
                {
                    GVars.Msg.Show("E00060", "教育项目");               // "E00060", "请选择{0}!");
                    return;
                }
                
                DataRow drItem = drItems[(int)(trvEduRec.SelectedNode.Tag)];
                string itemId  = drItem["ITEM_ID"].ToString();
                
                DataRow[] drFind = dsRec.Tables[0].Select("ITEM_ID = " + SqlManager.SqlConvert(itemId));
                if (drFind.Length == 0)
                {
                    return;
                }
                
                // 获取数据
                drRec = drFind[0];
                
                HealthEduDoFrm doFrm = new HealthEduDoFrm();
                
                doFrm.EduObject         = drRec["EDU_OBJECT"].ToString();
                doFrm.EduPrecondition   = drRec["PRECONDITION"].ToString();
                doFrm.EduMethod         = drRec["EDU_METHOD"].ToString();
                doFrm.EduMasterDegree   = drRec["MASTERED_DEGREE"].ToString();
                doFrm.EduClog           = drRec["EDU_CLOG"].ToString();
                doFrm.Memo              = drRec["MEMO"].ToString();
                doFrm.OnlyView          = (drRec["EDU_NURSE"].ToString().Equals(GVars.User.Name) == false);
                
                // 显示
                if (doFrm.ShowDialog() == DialogResult.OK)                
                {
                    DateTime dtNow = GVars.GetDateNow();
                    
                    drRec["EDU_OBJECT"]     = doFrm.EduObject;
                    drRec["PRECONDITION"]   = doFrm.EduPrecondition;
                    drRec["EDU_METHOD"]     = doFrm.EduMethod;
                    drRec["MASTERED_DEGREE"]= doFrm.EduMasterDegree;
                    drRec["EDU_CLOG"]       = doFrm.EduClog;
                    drRec["MEMO"]           = doFrm.Memo;
                    
                    drRec["EDU_DATE"]       = dtNow;
                    drRec["EDU_NURSE"]      = GVars.User.Name;
                    
                    drRec["UPD_DATE_TIME"]  = dtNow;
                    
                    healthRecDbI.SaveHealthEduRec(dsRec.GetChanges());
                    
                    dsRec.AcceptChanges();
                    
                    btnEducation.Text    = STR_CANCEL;          // 撤销教育
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[教育内容]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContent_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow drItem = drItems[(int)(trvEduRec.SelectedNode.Tag)];
                string itemId  = drItem["ITEM_ID"].ToString();
                string content = healthRecDbI.GetHealthEduItemContent(itemId, GVars.User.DeptCode);
                
                ContentShowFrm frm = new ContentShowFrm();
                frm.Content = content;
                frm.ShowDialog();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[教育]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEducation_Click(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;
                
                // 条件检查
                trvEduRec.Focus();
                if (trvEduRec.SelectedNode == null)
                {
                    GVars.Msg.Show("E00060", "教育项目");                                       // "E00060", "请选择{0}!");
                    return;
                }
                                
                // 执行教育
                DataRow drItem = drItems[(int)(trvEduRec.SelectedNode.Tag)];
                string itemId  = drItem["ITEM_ID"].ToString();
                
                DateTime dtNow = GVars.GetDateNow();
                DataSet dsDel  = null;
                
                // 如果是教育
                if (btnEducation.Text.Equals(STR_EDU) == true)
                {
                    HealthEduDoFrm doFrm = new HealthEduDoFrm();
                    doFrm.OnlyView  = false;
                    
                    if (doFrm.ShowDialog() == DialogResult.Cancel)
                    {
                        return;
                    }
                    
                    if (drRec == null)
                    {
                        drRec = dsRec.Tables[0].NewRow();
                        
                        drRec["DEPT_CODE"]      = GVars.User.DeptCode;
                        drRec["PATIENT_ID"]     = GVars.Patient.ID;
                        drRec["VISIT_ID"]       = GVars.Patient.VisitId;
                        drRec["ITEM_ID"]        = itemId;
                        drRec["ITEM_TYPE"]      = trvEduRec.SelectedNode.Parent.Text;
                        drRec["EDU_ITEM"]       = trvEduRec.SelectedNode.Text;
                        
                        drRec["EDU_OBJECT"]     = doFrm.EduObject;
                        drRec["PRECONDITION"]   = doFrm.EduPrecondition;
                        drRec["EDU_METHOD"]     = doFrm.EduMethod;
                        drRec["MASTERED_DEGREE"]= doFrm.EduMasterDegree;
                        drRec["EDU_CLOG"]       = doFrm.EduClog;
                        drRec["MEMO"]           = doFrm.Memo;
                        
                        drRec["EDU_DATE"]       = dtNow;
                        drRec["EDU_NURSE"]      = GVars.User.Name;
                        
                        dsRec.Tables[0].Rows.Add(drRec);
                    }
                    else
                    {
                        drRec["EDU_OBJECT"]     = doFrm.EduObject;
                        drRec["PRECONDITION"]   = doFrm.EduPrecondition;
                        drRec["EDU_METHOD"]     = doFrm.EduMethod;
                        drRec["MASTERED_DEGREE"]= doFrm.EduMasterDegree;
                        drRec["EDU_CLOG"]       = doFrm.EduClog;
                        drRec["MEMO"]           = doFrm.Memo;
                        
                        drRec["EDU_DATE"]       = dtNow;
                        drRec["EDU_NURSE"]      = GVars.User.Name;
                    }
                    
                    drRec["UPD_DATE_TIME"]  = dtNow;
                    
                    trvEduRec.SelectedNode.Checked = true;
                }
                else
                {                    
                    if (GVars.Msg.Show("Q00004", btnEducation.Text) != DialogResult.Yes)        // "Q00004","您确认{0}吗?");
                    {
                        return;
                    }
                    
                    if (drRec != null)
                    {
                        drRec.Delete();
                    }
                    
                    dsDel = ComFunctionApp.GetDataSetDeleted(ref dsRec, dtNow);
                    
                    trvEduRec.SelectedNode.Checked = false;
                }
                
                healthRecDbI.SaveHealthEduRec(dsRec.GetChanges());
                if (dsDel != null)
                {
                    healthRecDbI.SaveHealthEduRecDel(ref dsDel);
                }
                
                dsRec.AcceptChanges();
                btnEducation.Enabled = false;
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
        #endregion
        
        
        #region 共通函数
        /// <summary>
        /// 病人变更
        /// </summary>
        private void patientChanged()
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                GVars.App.UserInput     = false;
                
                // 设置编辑按钮
                btnEducation.Enabled    = false;
                btnContent.Enabled      = false;
                
                // 设置病人导航按钮
                patNavigator.SetPatientButtons();
                
                // 获取数据
                dsItems = healthRecDbI.GetDictItem(_dictId);
                
                if (GVars.Patient.ID.Length > 0)
                {
                    dsRec = healthRecDbI.GetHealthEduRec(GVars.User.DeptCode, GVars.Patient.ID, GVars.Patient.VisitId);
                }
                
                // 显示数据
                showHealthEduRec();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;                
                Cursor.Current = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// 显示评估数据
        /// </summary>
        private void showHealthEduRec()
        {
            trvEduRec.Nodes.Clear();
            arrNodes.Clear();
                        
            if (dsItems == null || dsRec == null)
            {
                return;
            }
            
            drItems = dsItems.Tables[0].Select(string.Empty, "ITEM_ID");
            
            DataRow[] drRecs  = dsRec.Tables[0].Select(string.Empty, "ITEM_ID");
            int recIndex = 0;
            
            TreeNode nodePre        = null;
            string   itemIdPre      = string.Empty;
            
            for(int i = 0; i < drItems.Length; i++)
            {
                // 创建节点
                string itemId = drItems[i]["ITEM_ID"].ToString();
                string itemName = drItems[i]["ITEM_NAME"].ToString();
                
                TreeNode node = null;
                
                #region 增加节点
                // 如果是第一个节点
                if (nodePre == null)
                {
                    node = trvEduRec.Nodes.Add(itemName);
                    
                    arrNodes.Add(node);
                }
                else
                {
                    // 如果ID长度相同, 表示是同一层
                    if (itemIdPre.Length == itemId.Length)
                    {
                        node = nodePre.Parent.Nodes.Add(itemName);
                        arrNodes.Add(node);
                    }
                    // 如果ID长度大于前一个ID, 表示是子项目
                    else if (itemIdPre.Length < itemId.Length)
                    {
                        node = nodePre.Nodes.Add(itemName);
                        arrNodes.Add(node);
                    }
                    // 如果ID小于于前一个ID, 表示父项目的兄弟
                    else
                    {
                        do
                        {
                            nodePre = nodePre.Parent;
                            
                            if (nodePre != null)
                            {
                                itemIdPre = drItems[(int)(nodePre.Tag)]["ITEM_ID"].ToString();
                            }
                            else
                            {
                                itemIdPre = string.Empty;
                            }
                            
                        } while (nodePre != null && itemId.Length < itemIdPre.Length);
                        
                        if (nodePre == null || nodePre.Parent == null)
                        {
                            node = trvEduRec.Nodes.Add(itemName);
                            arrNodes.Add(node);
                        }
                        else
                        {
                            node = nodePre.Parent.Nodes.Add(itemName);
                            arrNodes.Add(node);
                        }
                    }
                }
                #endregion
                
                node.Tag  = i;
                
                nodePre   = node;
                itemIdPre = itemId;
                
                // 设置节点的值
                if (getItemValue(ref drRecs, ref recIndex, itemId) == true)
                {
                    node.Checked = true;
                }
            }
        }
        
        
        /// <summary>
        /// 判断有没有教育记录
        /// </summary>
        /// <param name="drRecs"></param>
        /// <param name="recIndex"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        private bool getItemValue(ref DataRow[] drRecs, ref int recIndex, string itemId)
        {
            string itemIdRec = string.Empty;
            
            if (recIndex < drRecs.Length)
            {
                itemIdRec = drRecs[recIndex]["ITEM_ID"].ToString();
                
                while(itemId.CompareTo(itemIdRec) > 0)
                {
                    recIndex++;
                    
                    if (recIndex >= drRecs.Length)
                    {
                        return false;
                    }
                    
                    itemIdRec = drRecs[recIndex]["ITEM_ID"].ToString();
                }
                
                if(itemId.CompareTo(itemIdRec) == 0)
                {
                    return true;
                }
            }
            
            return false;            
        }
        #endregion        
    }
}