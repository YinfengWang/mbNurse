using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    struct EVAL_ITEM_TYPE
    {
        public static int PARENT = 0;
        public static int SINGLE = 1;
        public static int MULTI = 2;
        public static int TEXT = 3;
        public static int DATE = 4;
    }
    
    
    public partial class OptionSelectTreeFrm : FormDo
    {
        #region 变量
        public string       SelectItems     = string.Empty;
        
        private const string STR_OK         = "确定";
        private const string STR_SAVE       = "保存";
        private const string SEP_CHAR       = ":";
        
        protected string    _patientId      = string.Empty;             
        protected string    _visitId        = string.Empty;             

        protected string    _dictId         = string.Empty;                // 字典ID
        protected string    _parentItemId   = string.Empty;                // 父节点ID

        protected DataSet   _dsItemDict     = null;                        // 评估项目
        protected DataSet   _dsEvalRec      = null;                        // 评估记录
        protected DateTime  _dtRec          = DataType.DateTime_Null();    // 记录日期
        
        private bool        multiSelect     = true;

        private DataRow[]   drItems         = null;
        private ArrayList   arrNodes        = new ArrayList();
        private bool        dataChanged     = false;
        private TreeNode    nodeSelected    = null;
        private Color       colorSelected   = Color.Black;
        #endregion


        public OptionSelectTreeFrm()
        {
            InitializeComponent();
        }
        
        
        #region 属性
        public string PatientId
        {
            get { return _patientId; }
            set { _patientId = value; }
        }
        
        
        public string VisitId
        {
            get { return _visitId; }
            set { _visitId = value; }
        }
        
        
        public string DictId
        {
            get { return _dictId; }
            set { _dictId = value; }
        }
        
        
        public DateTime DateTimeRec
        {
            get { return _dtRec; }
            set { _dtRec = value; }
        }
        
        
        public string ParentItemId
        {
            get { return _parentItemId;}
            set { _parentItemId = value;}
        }
        
        
        public DataSet DsItemDict
        {
            get { return  _dsItemDict; }
            set { _dsItemDict = value; }
        }
        
        
        public DataSet DsEvalRec
        {
            get { return _dsEvalRec; }
            set { _dsEvalRec = value;}
        }
        #endregion
        
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionSelectFrm_Load(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = true;
                
                //showItemList();
                //showEvaluationRec();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        /// <summary>
        /// 加载时钟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                GVars.App.UserInput = false;
                timer1.Enabled = false;

                // 显示数据
                showEvalRec();

                // 设置编辑按钮
                btnSave.Enabled     = false;
                txtValue.Visible    = false;
                dtInputDate.Visible = false;
                dtInputTime.Visible = false;
                
                btnOk.Enabled       = false;
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
        /// 树节点Check事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvItems_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (GVars.App.UserInput == false)
            {
                return;
            }
            trvItems.SelectedNode = e.Node;

            bool blnStore = GVars.App.UserInput;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                GVars.App.UserInput = false;

                DataRow drItem = drItems[(int)(e.Node.Tag)];

                string attribute = drItem["FLAG"].ToString();

                // 如果是单选
                if (attribute.IndexOf(EVAL_ITEM_TYPE.SINGLE.ToString()) >= 0)
                {
                    if (e.Node.Checked == true && e.Node.Parent != null)
                    {
                        foreach (TreeNode node in e.Node.Parent.Nodes)
                        {
                            if (e.Node != node)
                            {
                                node.Checked = false;

                                uncheckChild(node);
                            }
                        }
                    }
                }

                // 取消选择
                if (e.Node.Checked == false)
                {
                    colorSelected = Color.Black;
                    uncheckChild(e.Node);
                }

                // 父节点的Check标识修改
                checkParent(e.Node);

                //btnSave.Enabled = (recorder.Length == 0 || GVars.User.Name.Equals(recorder));

                //btnSave.Enabled = true;

                dataChanged = btnSave.Enabled;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                GVars.App.UserInput = blnStore;
            }
        }


        private void trvItems_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            btnOk.Enabled = true;
        }


        /// <summary>
        /// 树节点选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvItems_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                if (nodeSelected != null && nodeSelected.Equals(e.Node) == false)
                {
                    //nodeSelected.ForeColor = Color.Black;
                    nodeSelected.ForeColor = colorSelected;

                    // 自动保存
                    if (btnSave.Enabled == true && btnSave.TextValue.Equals(STR_OK) == true)
                    {
                        btnSave_Click(null, null);
                    }
                }

                nodeSelected = e.Node;
                colorSelected = nodeSelected.ForeColor;
                e.Node.ForeColor = Color.Blue;

                txtValue.Visible = false;
                dtInputDate.Visible = false;
                dtInputTime.Visible = false;

                DataRow drItem = drItems[(int)(e.Node.Tag)];
                string itemValue = getItemValue(e.Node.Text);
                string attribute = drItem["FLAG"].ToString();

                // 如果是文本框
                if (attribute.IndexOf(EVAL_ITEM_TYPE.TEXT.ToString()) >= 0)
                {
                    txtValue.Visible = true;
                    txtValue.Text = itemValue;
                }

                // 如果是日期框
                if (attribute.IndexOf(EVAL_ITEM_TYPE.DATE.ToString()) >= 0)
                {
                    dtInputDate.Visible = true;
                    dtInputTime.Visible = true;

                    dtInputDate.Value = DateTime.Now;
                    dtInputTime.Value = DateTime.Now;

                    if (itemValue.Trim().Length > 0)
                    {
                        DateTime dt = DateTime.Parse(itemValue);
                        dtInputDate.Value = dt;
                        dtInputTime.Value = dt;
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
        /// 值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                btnSave.Enabled = true;
                
                dataChanged = btnSave.Enabled;
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
                Cursor.Current = Cursors.WaitCursor;
                
                if (saveInputValueInNode() == true)
                {
                    btnOk.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// 按钮[确定]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                SelectItems = string.Empty;

                saveDisp(_dtRec);
                
                bool blnFirst = true;
                foreach(TreeNode node in trvItems.Nodes)
                {
                    if (node.Checked == true)
                    {
                        if (blnFirst == false) SelectItems += ",";
                        SelectItems += node.Text;
                        blnFirst = false;
                    }
                }
                                                        
                DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
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
            try
            {
                DialogResult = DialogResult.Cancel;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion
        
        
        #region 共通函数
        /*
        /// <summary>
        /// 显示项目
        /// </summary>
        private void showItemList()
        {
            multiSelect = true;
            
            if (_dsItemDict == null || _dsItemDict.Tables.Count == 0)
            {
                return;
            }
            
            string filter = "ITEM_ID LIKE '" + _parentItemId + "%' AND ITEM_ID <> " + SqlManager.SqlConvert(_parentItemId);
            DataRow[] drFind = _dsItemDict.Tables[0].Select(filter, "ITEM_ID");
            
            lvwItems.Items.Clear();
            for(int i = 0; i < drFind.Length; i++)
            {
                ListViewItem item = lvwItems.Items.Add((i + 1).ToString());
                item.SubItems.Add(drFind[i]["ITEM_NAME"].ToString());
                
                if (drFind[i]["FLAG"].ToString().Equals("1") == true)
                {
                    multiSelect = false;
                }
                
                item.Tag = drFind[i]["ITEM_ID"].ToString();
            }
        }
        
        
        /// <summary>
        /// 显示病人评估
        /// </summary>
        private void showEvaluationRec()
        {
            if (_dsEvalRec == null || _dsEvalRec.Tables.Count == 0)
            {
                return;
            }
            
            string filter = "ITEM_ID LIKE '" + _parentItemId + "%' AND ITEM_ID <> " + SqlManager.SqlConvert(_parentItemId);
            DataRow[] drFind = _dsEvalRec.Tables[0].Select(filter, "ITEM_ID");
            
            int idx_rec = 0;
            
            for(int i = 0; i < lvwItems.Items.Count; i++)
            {
                ListViewItem item = lvwItems.Items[i];
                string itemId = item.Tag.ToString();
                
                if (idx_rec >= drFind.Length)
                {
                    break;
                }
                
                int compare = itemId.CompareTo(drFind[idx_rec]["ITEM_ID"].ToString());
                
                while(compare > 0 && idx_rec < drFind.Length - 1)
                {
                    idx_rec++;
                    compare = itemId.CompareTo(drFind[idx_rec]["ITEM_ID"].ToString());
                }
                
                if (compare == 0)
                {
                    item.Checked = true;
                    idx_rec++;
                }
            }
        }        
        
        
        /// <summary>
        /// 保存护理评估结果
        /// </summary>
        private bool saveEvaluationRec()
        {
            if (_dsEvalRec == null || _dsEvalRec.Tables.Count == 0)
            {
                return false;
            }
            
            DataRow[] drFind = null;
            
            for(int i = 0; i < lvwItems.Items.Count; i++)
            {
                ListViewItem item = lvwItems.Items[i];
                string itemId = item.Tag.ToString();
                string itemName = item.SubItems[1].Text1;
                
                drFind = _dsEvalRec.Tables[0].Select("ITEM_ID = " + SqlManager.SqlConvert(itemId));
                DataRow drEdit = null;
                
                if (item.Checked == true)
                {
                    if (drFind.Length == 0)
                    {
                        drEdit = _dsEvalRec.Tables[0].NewRow();
                        
                        drEdit["PATIENT_ID"]    = _patientId;
                        drEdit["VISIT_ID"]      = _visitId;
                        drEdit["DICT_ID"]       = _dictId; 
                        drEdit["ITEM_ID"]       = itemId;
                        drEdit["ITEM_NAME"]     = itemName;
                        drEdit["DEPT_CODE"]     = GVars.User.DeptCode; 
                        drEdit["RECORD_DATE"]   = _dtRec;
                        
                        _dsEvalRec.Tables[0].Rows.Add(drEdit);
                    }
                }
                else
                {
                    if (drFind.Length > 0)
                    {
                        drFind[0].Delete();
                    }
                }
            }
            
            return true;
        }
        */
        
        /// <summary>
        /// 显示评估数据
        /// </summary>
        private void showEvalRec()
        {
            dataChanged = false;

            trvItems.Nodes.Clear();
            arrNodes.Clear();

            // dtRecord = GVars.GetDateNow();

            if (_dsItemDict == null || _dsEvalRec == null)
            {
                return;
            }

            string filter = "ITEM_ID LIKE '" + _parentItemId + "%' AND ITEM_ID <> " + SqlManager.SqlConvert(_parentItemId);
            drItems = _dsItemDict.Tables[0].Select(filter, "ITEM_ID");
            
            DataRow[] drRecs = _dsEvalRec.Tables[0].Select(filter, "ITEM_ID");
            int recIndex = 0;
            
            TreeNode nodePre = null;
            TreeNode nodeParent = null;
            string itemIdPre = string.Empty;

            for (int i = 0; i < drItems.Length; i++)
            {
                // 创建节点
                string itemId = drItems[i]["ITEM_ID"].ToString();
                string itemName = drItems[i]["ITEM_NAME"].ToString();
                string itemValue = string.Empty;

                TreeNode node = null;

                #region 增加节点
                // 如果是第一个节点
                if (nodePre == null)
                {
                    node = trvItems.Nodes.Add(itemName);

                    arrNodes.Add(node);
                }
                else
                {
                    // 如果ID长度相同, 表示是同一层
                    if (itemIdPre.Length == itemId.Length)
                    {
                        if (nodePre.Parent == null)
                        {
                            node = trvItems.Nodes.Add(itemName);
                        }
                        else
                        {
                            node = nodePre.Parent.Nodes.Add(itemName);
                        }

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
                            node = trvItems.Nodes.Add(itemName);
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

                node.Tag = i;

                nodePre = node;
                itemIdPre = itemId;

                // 设置节点的值
                if (getItemValue(ref drRecs, ref recIndex, itemId, ref itemValue) == true)
                {
                    node.Checked = true;
                    nodeParent = node.Parent;
                    while (nodeParent != null)
                    {
                        nodeParent.Checked = true;
                        nodeParent = nodeParent.Parent;
                    }

                    if (itemValue.Length > 0)
                    {
                        node.Text += ":" + itemValue;
                    }
                }
            }
        }
        

        /// <summary>
        /// 获取节点的值
        /// </summary>
        /// <param name="drRecs"></param>
        /// <param name="recIndex"></param>
        /// <param name="itemId"></param>
        /// <param name="itemValue"></param>
        /// <returns></returns>
        private bool getItemValue(ref DataRow[] drRecs, ref int recIndex, string itemId, ref string itemValue)
        {
            itemValue = string.Empty;

            while (recIndex < drRecs.Length && itemId.CompareTo(drRecs[recIndex]["ITEM_ID"].ToString()) > 0)
            {
                recIndex++;
            }

            if (recIndex < drRecs.Length && itemId.CompareTo(drRecs[recIndex]["ITEM_ID"].ToString()) == 0)
            {
                itemValue = drRecs[recIndex]["ITEM_VALUE"].ToString().Trim();
                return true;
            }

            return false;
        }


        /// <summary>
        /// 获取项目的值
        /// </summary>
        /// <param name="nodeText"></param>
        /// <returns></returns>
        private string getItemValue(string nodeText)
        {
            int pos = nodeText.IndexOf(SEP_CHAR);
            string itemValue = string.Empty;
            if (pos > 0)
            {
                itemValue = nodeText.Substring(pos + 1, nodeText.Length - pos - 1);
            }

            return itemValue;
        }


        /// <summary>
        /// Check所有父节点
        /// </summary>
        /// <param name="node"></param>
        private void checkParent(TreeNode node)
        {
            if (node.Checked == true)
            {
                while (node.Parent != null)
                {
                    node = node.Parent;
                    node.Checked = true;
                }
            }
            else
            {
                while (node.Parent != null)
                {
                    if (checkIsChecked(node) == false)
                    {
                        node.Parent.Checked = false;
                    }
                    else
                    {
                        return;
                    }

                    node = node.Parent;
                }
            }
        }


        /// <summary>
        /// 取消所有子节点的选中状态
        /// </summary>
        /// <param name="?"></param>
        private void uncheckChild(TreeNode nodeCheck)
        {
            nodeCheck.ForeColor = Color.Black;
            foreach (TreeNode node in nodeCheck.Nodes)
            {
                node.Checked = false;
                nodeCheck.ForeColor = Color.Black;

                uncheckChild(node);
            }
        }


        /// <summary>
        /// 检查是否属于异常
        /// </summary>
        /// <param name="nodeCheck"></param>
        /// <returns></returns>
        private bool checkIsChecked(TreeNode nodeCheck)
        {
            if (nodeCheck == null) return false;

            // 如果是选中
            if (nodeCheck.Checked == true) return true;

            if (nodeCheck.Parent == null) return false;

            // 如果是取消选中 查找同级节点中是否还有其它节点被选中
            foreach (TreeNode node in nodeCheck.Parent.Nodes)
            {
                if (node.Equals(nodeCheck) == false)
                {
                    if (node.Checked == true)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        
        /// <summary>
        /// 保存界面输入到缓存中
        /// </summary>
        private bool saveInputValueInNode()
        {
            //if (trvItems.SelectedNode == null)
            if (nodeSelected == null)
            {
                return false;
            }

            //DataRow drItem = drItems[(int)(trvItems.SelectedNode.Tag)];
            DataRow drItem = drItems[(int)(nodeSelected.Tag)];
            string itemValue = string.Empty;

            if (txtValue.Visible == true)
            {
                itemValue = txtValue.Text.Trim();

            }
            else if (dtInputDate.Visible == true)
            {
                DateTime dtSet = dtInputDate.Value.Date;
                dtSet = dtSet.AddHours(dtInputTime.Value.Hour);
                dtSet = dtSet.AddMinutes(dtInputTime.Value.Minute);
                dtSet = dtSet.AddSeconds(dtInputTime.Value.Second);

                itemValue = dtSet.ToString(ComConst.FMT_DATE.LONG);
            }

            //trvItems.SelectedNode.Text1 = drItem["ITEM_NAME"].ToString() + SEP_CHAR + itemValue;                
            //trvItems.SelectedNode.Checked = (itemValue.Trim().Length > 0);
            nodeSelected.Text = drItem["ITEM_NAME"].ToString() + (itemValue.Length > 0? SEP_CHAR: "") + itemValue;
            if (itemValue.Trim().Length > 0)
            {
                nodeSelected.Checked = true;
            }
            
            return true;
        }


        /// <summary>
        /// 显示评估数据
        /// </summary>
        private bool saveDisp(DateTime dtNow)
        {
            string filter = "ITEM_ID LIKE '" + _parentItemId + "%' AND ITEM_ID <> " + SqlManager.SqlConvert(_parentItemId);
            DataRow[] drRecs = _dsEvalRec.Tables[0].Select(filter, "ITEM_ID");
            int recIndex = 0;
            bool blnMod = false;            // 修改或删除
            bool lnNew = false;

            for (int i = 0; i < arrNodes.Count; i++)
            {
                TreeNode node    = (TreeNode)arrNodes[i];

                DataRow drItem   = drItems[(int)(node.Tag)];

                string itemId    = drItem["ITEM_ID"].ToString();
                string itemText  = getItemValue(node.Text);

                string itemName  = drItem["ITEM_NAME"].ToString();

                string recItemId = string.Empty;
                if (recIndex < drRecs.Length)
                {
                    recItemId = drRecs[recIndex]["ITEM_ID"].ToString();
                }
                
                blnMod = (recIndex < drRecs.Length && itemId.Equals(recItemId) == true);
                lnNew = (recIndex >= drRecs.Length || itemId.CompareTo(recItemId) < 0);

                if (blnMod)
                //if (recIndex < drRecs.Length && itemId.Equals(drRecs[recIndex]["ITEM_ID"].ToString()) == true)
                {
                    DataRow drRec = drRecs[recIndex];

                    // 如果是删除
                    if (node.Checked == false)
                    {
                        drRec.Delete();
                    }
                    // 修改
                    else
                    {
                        // 如果是日期
                        if (drItem["FLAG"].ToString().IndexOf(EVAL_ITEM_TYPE.DATE.ToString()) >= 0)
                        {
                            // 如果日期不相等
                            if (itemText.Trim().Length > 0 && drRec["ITEM_VALUE"].ToString().Trim().Length > 0
                                && itemText.Trim().Equals(drRec["ITEM_VALUE"].ToString().Trim()) == false)
                            {
                                DateTime dt0 = DateTime.Parse(itemText);
                                DateTime dt1 = DateTime.Parse(drRec["ITEM_VALUE"].ToString().Trim());

                                if (dt0.CompareTo(dt1) != 0)
                                {
                                    drRec["ITEM_VALUE"] = itemText;
                                }
                            }
                        }
                        // 如果是其它
                        else
                        {
                            if (itemText.Equals(drRec["ITEM_VALUE"].ToString()) == false)
                            {
                                drRec["ITEM_VALUE"] = itemText;
                            }
                        }
                    }

                    recIndex++;
                }
                // 如果是新增
                //else if (node.Checked == true)
                if (lnNew && node.Checked == true)
                {
                    DataRow drNew = _dsEvalRec.Tables[0].NewRow();

                    drNew["PATIENT_ID"]  = GVars.Patient.ID;
                    drNew["VISIT_ID"]    = GVars.Patient.VisitId;
                    drNew["DICT_ID"]     = _dictId;
                    drNew["ITEM_ID"]     = itemId;
                    drNew["ITEM_NAME"]   = drItem["ITEM_NAME"];
                    drNew["ITEM_VALUE"]  = itemText;
                    drNew["DEPT_CODE"]   = GVars.User.DeptCode;
                    drNew["RECORD_DATE"] = dtNow;    // dtRecord.Date;

                    _dsEvalRec.Tables[0].Rows.Add(drNew);
                }
            }

            return true;
        }
        #endregion
    }
}
