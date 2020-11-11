using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HISPlus.COMAPP.Function;

namespace HISPlus
{
    public partial class ItemValueInputFrm : Form
    {
        #region 变量
        protected string _dictId = string.Empty;           // 字典表ID
        protected string _templateId = string.Empty;             // 模板ID
        protected bool _oneTime = false;                  // 是否只有一次 (入院/出院 评估 只有一次)
        protected string _docNursingId = string.Empty;             // 模板ID

        private const string ITEM_RECORDER = "RECORDER";
        private const string ITEM_RECORD_TIME = "RECORD_TIME";
        private const string ITEM_EXCEPTION = "EXCEPTION";

        private const string STR_OK = "确定";
        private const string STR_SAVE = "保存";
        private const string SEP_CHAR = ":";
        private bool _isAddPgd = false;   //是否新增评估单
        private PatientNavigator patNavigator = new PatientNavigator(); // 病人导航

        private DateTime dtRecord = DataType.DateTime_Null();   // 记录时间
        private string recorder = string.Empty;           // 记录护士
        private EvaluationDbI evalDbi = new EvaluationDbI();

        private DataSet dsItems = null;                   // 评估项目
        private DataRow[] drItems = null;
        private DataSet dsRec = null;                   // 评估记录
        private DataSet dsAppConfig = null;                   // 参数设置

        private ArrayList arrNodes = new ArrayList();

        private bool dataChanged = false;
        private TreeNode nodeSelected = null;
        private Color colorSelected = Color.Black;
        #endregion


        public ItemValueInputFrm()
        {
            InitializeComponent();
        }


        #region 属性
        /// <summary>
        /// 字典ID号
        /// </summary>
        public string DictID
        {
            get { return _dictId; }
            set { _dictId = value; }
        }


        /// <summary>
        /// 是否只有一次
        /// </summary>
        public bool OneTime
        {
            get { return _oneTime; }
            set { _oneTime = value; }
        }
        #endregion


        #region 窗体事件
        private void ItemValueInputFrm_Load(object sender, EventArgs e)
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
            catch (Exception ex)
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
        /// 菜单[新增评估]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuNewEval_Click(object sender, EventArgs e)
        {
            try
            {
                _isAddPgd = true;
                dsRec = null;
                if (GVars.Patient.ID.Length > 0)
                {
                    getTemplate();
                }

                showEvalRec2();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
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

                GVars.App.UserInput = false;
                timer1.Enabled = false;

                // 设置编辑按钮
                btnSave.Enabled = false;
                txtValue.Visible = false;
                dtInputDate.Visible = false;
                dtInputTime.Visible = false;

                mnuNewEval.Enabled = (_oneTime == false);       // 新增评估仅对多次评有用 ，屏蔽by 李治鹏，2014-11-24
                //mnuNewEval.Enabled = false;                       //新增按钮失效

                // 设置病人导航按钮
                patNavigator.SetPatientButtons();

                // 获取参数
                string sql = "SELECT * FROM APP_CONFIG WHERE PARAMETER_NAME LIKE 'EI_" + _dictId + "_%'";
                dsAppConfig = GVars.sqlceLocal.SelectData(sql, "APP_CONFIG");

                // 获取数据
                if (GVars.Patient.ID.Length > 0)
                {
                    getTemplate();
                }

                // 显示数据
                //showEvalRec();
                showEvalRec2();

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

                DataRow drItem = null;
                string tmpFilter = "ID = " + e.Node.Tag;
                DataRow[] tmpRows = dsItems.Tables[0].Select(tmpFilter);
                if (tmpRows.Count() <= 0)
                {
                    return;
                }

                drItem = tmpRows[0];

                string attribute = drItem["CONTROL_TEMPLATE_ID"].ToString();

                // 如果是checkbox
                if (attribute.IndexOf(EVAL_ITEM_TYPE.CHECKBOX.ToString()) >= 0)
                {

                    tmpFilter = "ID = " + e.Node.Parent.Tag;
                    tmpRows = null;
                    tmpRows = dsItems.Tables[0].Select(tmpFilter);
                    if (tmpRows.Count() <= 0)
                    {
                        return;
                    }

                    drItem = tmpRows[0];
                    attribute = drItem["CONTROL_TEMPLATE_ID"].ToString();

                    if (attribute.IndexOf(EVAL_ITEM_TYPE.SELECTSINGLE.ToString()) >= 0)
                    {
                        if (e.Node.Checked == true)
                        {
                            foreach (TreeNode node in e.Node.Parent.Nodes)
                            {

                                if (node != e.Node)
                                {
                                    node.Checked = false;
                                    uncheckChild(node);
                                }
                            }
                        }
                    }

                }

                // 波及其它节点
                sprideResult(e.Node);

                // 取消选择
                if (e.Node.Checked == false)
                {
                    colorSelected = Color.Black;
                    uncheckChild(e.Node);
                }
                //修改总分
                changeScore();

                //// 父节点的Check标识修改
                checkParent(e.Node);

                //// 标识异常
                markException(e.Node);

                ////btnSave.Enabled = (recorder.Length == 0 || GVars.User.Name.Equals(recorder));

                btnSave.Enabled = true;

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
                    if (btnSave.Enabled == true && btnSave.Text.Equals(STR_OK) == true)
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

                DataRow drItem = null;
                string tmpFilter = "ID = " + e.Node.Tag;
                DataRow[] tmpRows = dsItems.Tables[0].Select(tmpFilter);
                if (tmpRows.Count() <= 0)
                {
                    return;
                }

                drItem = tmpRows[0];


                string itemValue = getItemValue(e.Node.Text);
                string attribute = drItem["CONTROL_TEMPLATE_ID"].ToString();

                // 如果是文本框
                if (attribute.IndexOf(EVAL_ITEM_TYPE.TEXTBOX.ToString()) >= 0 || attribute.IndexOf(EVAL_ITEM_TYPE.TEXTAREA.ToString()) >= 0)
                {
                    int tmpRelationType = 0;
                    tmpRelationType = Convert.ToInt32(drItem["RELATION_TYPE"].ToString());
                    if (RelationType.签名 == (RelationType)tmpRelationType)
                    {

                        nodeSelected.Checked = true;
                        txtValue.Visible = false;
                    }
                    else
                    {
                        txtValue.Visible = true;
                        txtValue.Text = itemValue;
                    }

                }

                //// 如果是LABEL
                //if (attribute.IndexOf(EVAL_ITEM_TYPE.LABEL.ToString()) >= 0)
                //{

                //}



                // 如果是日期框
                if (attribute.IndexOf(EVAL_ITEM_TYPE.DATE.ToString()) >= 0 || attribute.IndexOf(EVAL_ITEM_TYPE.TIME.ToString()) > 0 || attribute.IndexOf(EVAL_ITEM_TYPE.DATETIME.ToString()) > 0)
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
                    btnSave.Text = STR_OK;
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

                btnSave.Text = STR_OK;
                //btnSave.Enabled = (recorder.Length == 0 || GVars.User.Name.Equals(recorder));
                btnSave.Enabled = true;

                dataChanged = btnSave.Enabled;
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
                Cursor.Current = Cursors.WaitCursor;

                if (btnSave.Text.Equals(STR_OK) == true)
                {
                    if (saveInputValueInNode() == true)
                    {
                        this.btnSave.Text = STR_SAVE;
                    }
                }
                else
                {
                    if (dtRecord.Date.Year < 2000)
                    {
                        dtRecord = GVars.GetDateNow();
                    }

                    DateTime dtNow = GVars.GetDateNow();
                    saveDispTag(dtNow);
                    saveDisp2(dtNow);


                    if (_oneTime == true)
                    {
                        //DataSet dsDel= ComFunctionApp.GetDataSetDeleted(ref dsRec, dtNow);
                        DataSet dsDel = ComFunctionApp.GetDataSetDeleted(ref dsRec, dtRecord);

                        evalDbi.SaveEvalRecOne(ref dsRec, dtRecord, _dictId, GVars.Patient.ID, GVars.Patient.VisitId);
                        evalDbi.SaveEvalRecOneDel(ref dsDel);
                    }
                    else
                    {
                        //DataSet dsDel= ComFunctionApp.GetDataSetDeleted(ref dsRec, dtNow);
                        DataSet dsDel = ComFunctionApp.GetDataSetDeleted(ref dsRec, dtRecord);
                       

                        evalDbi.SaveEvalRec(ref dsRec, dtRecord, _dictId, GVars.Patient.ID, GVars.Patient.VisitId);
                        evalDbi.SaveEvalRecDel(ref dsDel);


                        
                    }

                    dataChanged = false;
                    btnSave.Enabled = false;
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
        #endregion


        #region 共通函数
        /// <summary>
        /// 显示评估数据
        /// </summary>
        private void showEvalRec()
        {
            dataChanged = false;

            trvItems.Nodes.Clear();
            recorder = string.Empty;
            arrNodes.Clear();

            // dtRecord = GVars.GetDateNow();

            if (dsItems == null || dsRec == null)
            {
                return;
            }

            drItems = dsItems.Tables[0].Select(string.Empty, "ITEM_ID");

            DataRow[] drRecs = dsRec.Tables[0].Select(string.Empty, "ITEM_ID");
            int recIndex = 0;

            TreeNode nodePre = null;
            TreeNode nodeParent = null;
            string itemIdPre = string.Empty;

            //if (drRecs.Length > 0)
            //{
            //    dtRecord = (DateTime)(drRecs[0]["RECORD_DATE"]);
            //}

            for (int i = 0; i < drItems.Length; i++)
            {
                // 创建节点
                string itemId = drItems[i]["ITEM_ID"].ToString();
                string itemName = drItems[i]["ITEM_NAME"].ToString();
                string itemValue = string.Empty;

                #region 记录人 记录时间
                if (itemName.Equals(ITEM_RECORDER) == true)
                {
                    getItemValue(ref drRecs, ref recIndex, itemId, ref recorder);
                    continue;
                }

                if (itemName.Equals(ITEM_RECORD_TIME) == true)
                {
                    if (getItemValue(ref drRecs, ref recIndex, itemId, ref itemValue) == true)
                    {
                        dtRecord = DateTime.Parse(itemValue);
                    }
                    continue;
                }
                #endregion

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
                    markException(node);            // 标识异常

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
        /// 显示评估数据  作者：李治鹏
        /// </summary>
        private void showEvalRec2()
        {

            if (dsItems == null)
            {
                return;
            }

            dataChanged = false;

            TreeNode rootNode = null;

            trvItems.Nodes.Clear();
            recorder = string.Empty;
            arrNodes.Clear();

            string tmpFilter = string.Empty;
            tmpFilter = "PARENT_ID = " + "0";
            string tmpSort = "SORT_ID";
            showUI(rootNode, "0", tmpSort);
            initDefaultVaules();
            showUIValue();
            changeScore();

        }
        /// <summary>
        /// 显示模板元素  作者：李治鹏
        /// </summary>
        private void showUI(TreeNode tmpTreeNode, string tmpParentId, string tmpsSort)
        {
            TreeNode tmpNode = null;
            string tmpItemName = string.Empty;
            string tmpItemId = string.Empty;
            string tmpFilter = string.Empty;
            tmpFilter = "PARENT_ID = " + tmpParentId;

            DataRow[] tmpRows = dsItems.Tables[0].Select(tmpFilter, tmpsSort);
            if (tmpRows == null)
            {
                return;
            }
            if (tmpTreeNode == null)
            {
                //创建根节点
                foreach (DataRow tmpRow in tmpRows)
                {
                    tmpNode = new TreeNode();
                    tmpNode.Text = tmpRow["ELEMENT_NAME"].ToString();
                    tmpItemId = tmpRow["ID"].ToString();
                    trvItems.Nodes.Add(tmpNode);
                    tmpNode.Tag = tmpItemId;
                    arrNodes.Add(tmpNode);
                    showUI(tmpNode, tmpItemId, tmpsSort);
                }
            }
            else
            {
                //创建非根节点
                foreach (DataRow tmpRow in tmpRows)
                {
                    tmpNode = new TreeNode();
                    tmpNode.Text = tmpRow["ELEMENT_NAME"].ToString();
                    tmpItemId = tmpRow["ID"].ToString();
                    tmpTreeNode.Nodes.Add(tmpNode);
                    tmpNode.Tag = tmpItemId;
                    arrNodes.Add(tmpNode);
                    showUI(tmpNode, tmpItemId, tmpsSort);

                }

            }
        }
        #region 数值显示
        /// <summary>
        /// 显示评估数据  作者：李治鹏
        /// </summary>
        private void showUIValue()
        {
            if (dsRec == null || dsRec.Tables[0].Rows.Count <= 0)
            {
                return;
            }

            DataRow[] tmpDr = dsRec.Tables[0].Select();

            foreach (DataRow tmpRow in tmpDr)
            {
                string tmpId = string.Empty;
                tmpId = tmpRow["DOC_ELEMENT_ID"].ToString();


                for (int j = 0; j < arrNodes.Count; j++)
                {
                    TreeNode tmpNode = null;
                    tmpNode = (TreeNode)arrNodes[j];
                    string tmpDocEleId = string.Empty;
                    tmpDocEleId = tmpNode.Tag.ToString();

                    if (System.Int32.Parse(tmpDocEleId) == System.Int32.Parse(tmpId))
                    {
                        //默认值清空


                        //shezhi checkbox
                        DataRow drItem = null;
                        string tmpFilter = "ID = " + tmpNode.Tag;
                        DataRow[] tmpRows = dsItems.Tables[0].Select(tmpFilter);
                        if (tmpRows.Count() <= 0)
                        {
                            continue;
                        }

                        drItem = tmpRows[0];
                        string attribute = drItem["CONTROL_TEMPLATE_ID"].ToString();
                        int dataType = Convert.ToInt32(drItem["DATA_TYPE"].ToString());
                        // 如果
                        if (attribute.IndexOf(EVAL_ITEM_TYPE.CHECKBOX.ToString()) >= 0)
                        {
                            string tmpAttribute = string.Empty;
                            if (dataType == (int)DataValueType.zhengshu || dataType == (int)DataValueType.fudian)
                            {
                                tmpAttribute = tmpRow["NUMBER_VALUE"].ToString();
                            }
                            else
                            {
                                tmpAttribute = tmpRow["STRING_VALUE"].ToString();
                            }

                            if ((Convert.ToDouble(tmpAttribute) < 1))
                            {
                                tmpNode.Checked = false;

                            }
                            else
                            {
                                tmpNode.Checked = true;
                                tmpNode.Parent.Checked = true;
                            }

                        }
                        else if (attribute.IndexOf(EVAL_ITEM_TYPE.LABEL.ToString()) >= 0
                            || attribute.IndexOf(EVAL_ITEM_TYPE.SELECTMUL.ToString()) >= 0
                            || attribute.IndexOf(EVAL_ITEM_TYPE.SELECTSINGLE.ToString()) >= 0)
                        {
                            break;
                        }
                        else
                        {
                            if (dataType == (int)DataValueType.zhengshu || dataType == (int)DataValueType.fudian)
                            {
                                tmpNode.Text = drItem["ELEMENT_NAME"].ToString() + ":" + tmpRow["NUMBER_VALUE"].ToString();
                            }
                            else
                            {
                                tmpNode.Text = drItem["ELEMENT_NAME"].ToString() + ":" + tmpRow["STRING_VALUE"].ToString();
                            };
                            tmpNode.Checked = true;

                        }
                        break;

                    }
                }
            }

        }

        /// <summary>
        /// 初始化值，系统某些元素默认值  作者：李治鹏
        /// </summary>
        private void initDefaultVaules()
        {
            DataRow curRow = GVars.DsPatient.Tables[0].Rows[GVars.PatIndex];
            for (int i = 0; i < arrNodes.Count; i++)
            {
                TreeNode tmpNode = (TreeNode)arrNodes[i];
                DataRow drItem = null;
                string tmpFilter = "ID = " + tmpNode.Tag;

                DataRow[] tmpRows = dsItems.Tables[0].Select(tmpFilter);
                if (tmpRows.Count() <= 0)
                {
                    continue;
                }


                drItem = tmpRows[0];
                int tmpRelationType = 0;
                tmpRelationType = Convert.ToInt32(drItem["RELATION_TYPE"].ToString());
                if (RelationType.默认 == (RelationType)tmpRelationType)
                {
                    continue;
                }
                else if (RelationType.签名 == (RelationType)tmpRelationType)
                {
                    tmpNode.Text = drItem["ELEMENT_NAME"].ToString() + ":" + GVars.User.Name;
                    tmpNode.Checked = true;
                }
                else
                {
                    int relationCode = Convert.ToInt32(drItem["RELATION_CODE"].ToString());
                    switch ((RelationPatientItem)relationCode)
                    {

                        case RelationPatientItem.病人Id:
                            tmpNode.Text = drItem["ELEMENT_NAME"].ToString() + ":" + curRow["PATIENT_ID"].ToString();
                            tmpNode.Checked = true;
                            break;
                        case RelationPatientItem.姓名:
                            tmpNode.Text = drItem["ELEMENT_NAME"].ToString() + ":" + curRow["NAME"].ToString();
                            tmpNode.Checked = true;
                            break;
                        case RelationPatientItem.性别:
                            tmpNode.Text = drItem["ELEMENT_NAME"].ToString() + ":" + curRow["SEX"].ToString();
                            tmpNode.Checked = true;
                            break;
                        case RelationPatientItem.住院号:
                            tmpNode.Text = drItem["ELEMENT_NAME"].ToString() + ":" + curRow["INP_NO"].ToString();
                            tmpNode.Checked = true;
                            break;
                        case RelationPatientItem.住院次数:
                            tmpNode.Text = drItem["ELEMENT_NAME"].ToString() + ":" + curRow["VISIT_ID"].ToString();
                            tmpNode.Checked = true;
                            break;
                        case RelationPatientItem.生日:
                            tmpNode.Text = drItem["ELEMENT_NAME"].ToString() + ":" + curRow["DATE_OF_BIRTH"].ToString();
                            tmpNode.Checked = true;
                            break;
                        case RelationPatientItem.诊断:
                            tmpNode.Text = drItem["ELEMENT_NAME"].ToString() + ":" + curRow["DIAGNOSIS"].ToString();
                            tmpNode.Checked = true;
                            break;
                        case RelationPatientItem.护理等级:
                            break;
                        case RelationPatientItem.主治医生:
                            break;
                        case RelationPatientItem.床号:
                            tmpNode.Text = drItem["ELEMENT_NAME"].ToString() + ":" + curRow["BED_LABEL"].ToString();
                            tmpNode.Checked = true;
                            break;
                        case RelationPatientItem.床标号:
                            tmpNode.Text = drItem["ELEMENT_NAME"].ToString() + ":" + curRow["BED_LABEL"].ToString();
                            tmpNode.Checked = true;
                            break;
                        case RelationPatientItem.病情状态:
                            break;
                        case RelationPatientItem.入院时间:
                            if (curRow["ADMISSION_DATE_TIME"].ToString().Length > 0)
                            {
                                tmpNode.Text = drItem["ELEMENT_NAME"].ToString() + ":" + curRow["ADMISSION_DATE_TIME"].ToString(); // 入院日期
                                tmpNode.Checked = true;
                            }

                            break;
                        case RelationPatientItem.所在科室:
                            tmpNode.Text = drItem["ELEMENT_NAME"].ToString() + ":" + curRow["DEPT_NAME"].ToString();
                            tmpNode.Checked = true;
                            break;
                        default:
                            break;
                    }

                }

            }

        }
        #endregion

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
        /// 检查是否属于异常节点
        /// </summary>
        /// <returns></returns>
        private bool checkIsExceptionNode(TreeNode nodeCheck)
        {
            TreeNode nodeParent = nodeCheck;
            while (nodeParent != null)
            {
                if (nodeParent.Text.Equals(ITEM_EXCEPTION) == true)
                {
                    return true;
                }

                nodeParent = nodeParent.Parent;
            }

            return false;
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
        /// 标识异常
        /// </summary>
        private void markException(TreeNode nodeCheck)
        {
            // 如果不是异常节点, 退出
            if (checkIsExceptionNode(nodeCheck) == false)
            {
                return;
            }

            // 如果是异常节点, 标识异常
            bool isException = checkIsChecked(nodeCheck);
            Color color = (isException ? Color.Red : Color.Black);
            if (nodeCheck.Checked == false)
            {
                nodeCheck.ForeColor = Color.Black;
            }
            else
            {
                nodeCheck.ForeColor = color;
            }

            colorSelected = nodeCheck.ForeColor;

            TreeNode nodeParent = nodeCheck.Parent;
            while (nodeParent != null)
            {
                nodeParent.ForeColor = color;
                nodeParent = nodeParent.Parent;
            }
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
        /// 病人变更
        /// </summary>
        private void patientChanged()
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                GVars.App.UserInput = false;

                // 设置编辑按钮
                btnSave.Enabled = false;
                txtValue.Visible = false;
                dtInputDate.Visible = false;
                dtInputTime.Visible = false;

                // 设置病人导航按钮
                patNavigator.SetPatientButtons();

                // 获取数据
                if (GVars.Patient.ID.Length > 0)
                {
                    getTemplate();
                }

                dsRec = evalDbi.GetEvalRecOne2(_docNursingId);
                // 显示数据
                showEvalRec2();
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

            DataRow drItem = null;
            string tmpItemId = string.Empty;
            string tmpFilter = "ID = " + nodeSelected.Tag;
            DataRow[] tmpRows = dsItems.Tables[0].Select(tmpFilter);
            if (tmpRows.Count() <= 0)
            {
                return false;
            }

            drItem = tmpRows[0];
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

            //trvItems.SelectedNode.Text = drItem["ITEM_NAME"].ToString() + SEP_CHAR + itemValue;                
            //trvItems.SelectedNode.Checked = (itemValue.Trim().Length > 0);
            nodeSelected.Text = drItem["ELEMENT_NAME"].ToString() + SEP_CHAR + itemValue;
            nodeSelected.Checked = (itemValue.Trim().Length > 0);

            return true;
        }


        /// <summary>
        /// 显示评估数据
        /// </summary>
        private bool saveDisp(DateTime dtNow)
        {
            DataRow[] drRecs = dsRec.Tables[0].Select(string.Empty, "ITEM_ID");
            int recIndex = 0;
            bool blnMod = false;            // 修改或删除
            bool lnNew = false;

            for (int i = 0; i < arrNodes.Count; i++)
            {
                TreeNode node = (TreeNode)arrNodes[i];

                DataRow drItem = drItems[(int)(node.Tag)];

                string itemId = drItem["ITEM_ID"].ToString();
                string itemText = getItemValue(node.Text);

                string recItemId = string.Empty;
                string itemName = string.Empty;
                while (recIndex < drRecs.Length)
                {
                    recItemId = drRecs[recIndex]["ITEM_ID"].ToString();
                    itemName = drRecs[recIndex]["ITEM_NAME"].ToString();

                    if (ITEM_RECORDER.Equals(itemName) || ITEM_RECORD_TIME.Equals(itemName))
                    {
                        recIndex++;
                    }
                    else
                    {
                        break;
                    }
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
                                    drRec["UPD_DATE_TIME"] = dtNow;
                                }
                            }
                        }
                        // 如果是其它
                        else
                        {
                            if (itemText.Equals(drRec["ITEM_VALUE"].ToString()) == false)
                            {
                                drRec["ITEM_VALUE"] = itemText;
                                drRec["UPD_DATE_TIME"] = dtNow;
                            }
                        }
                    }

                    recIndex++;
                }
                // 如果是新增
                //else if (node.Checked == true)
                if (lnNew && node.Checked == true)
                {
                    DataRow drNew = dsRec.Tables[0].NewRow();

                    drNew["PATIENT_ID"] = GVars.Patient.ID;
                    drNew["VISIT_ID"] = GVars.Patient.VisitId;
                    drNew["DICT_ID"] = _dictId;
                    drNew["ITEM_ID"] = itemId;
                    drNew["ITEM_NAME"] = drItem["ITEM_NAME"];
                    drNew["ITEM_VALUE"] = itemText;
                    drNew["DEPT_CODE"] = GVars.User.DeptCode;
                    drNew["RECORD_DATE"] = dtRecord;    // dtRecord.Date;
                    drNew["UPD_DATE_TIME"] = dtNow;

                    dsRec.Tables[0].Rows.Add(drNew);
                }
            }

            return true;
        }


        /// <summary>
        /// baocun 评估数据
        /// </summary>
        private bool saveDisp2(DateTime dtNow)
        {
            DataRow[] drRecs = dsRec.Tables[0].Select(string.Empty, "ID");
            bool blnMod = false;            // 修改或删除
            bool lnNew = false;
            int datatype = 0;


            for (int i = 0; i < arrNodes.Count; i++)
            {
                TreeNode tmpNode = (TreeNode)arrNodes[i];
                if (tmpNode.Text == null)
                {
                    continue;
                }

                DataRow drItem = null;
                string tmpFilter = "ID = " + tmpNode.Tag.ToString();
                DataRow[] tmpRows = dsItems.Tables[0].Select(tmpFilter);
                if (tmpRows.Count() <= 0)
                {
                    continue;
                }


                drItem = tmpRows[0];
                string itemName = string.Empty;
                itemName = drItem["ELEMENT_NAME"].ToString();
                string itemText = getItemValue(tmpNode.Text);
                string attribute = drItem["CONTROL_TEMPLATE_ID"].ToString();
                datatype = Convert.ToInt32(drItem["DATA_TYPE"]);


                // 如果是label
                if (attribute.IndexOf(EVAL_ITEM_TYPE.LABEL.ToString()) >= 0
                    || attribute.IndexOf(EVAL_ITEM_TYPE.SELECTSINGLE.ToString()) >= 0
                    || attribute.IndexOf(EVAL_ITEM_TYPE.SELECTMUL.ToString()) >= 0)
                {
                    continue;
                }

                drItem = null;
                tmpFilter = "DOC_ELEMENT_ID = " + System.Int32.Parse(tmpNode.Tag.ToString())
                    + " AND DOC_NURSING_ID = " + "'" + _docNursingId + "'";
                tmpRows = dsRec.Tables[0].Select(tmpFilter);


                //判断是新增还是修改
                if (tmpRows.Count() <= 0)
                {
                    blnMod = false;
                    lnNew = true;
                }
                else
                {
                    blnMod = true;
                    lnNew = false;
                }


                if (blnMod)
                {
                    DataRow drRec = tmpRows[0];

                    // 如果是删除
                    if (tmpNode.Checked == false)
                    {
                        drRec.Delete();
                    }
                    else // 修改
                    {
                        // 如果是label
                        if (attribute.IndexOf(EVAL_ITEM_TYPE.CHECKBOX.ToString()) >= 0)
                        {
                            if (tmpNode.Checked == true)
                            {
                                if (datatype == (int)DataValueType.zhengshu || datatype == (int)DataValueType.fudian)
                                    drRec["NUMBER_VALUE"] = 1;
                                else
                                    drRec["STRING_VALUE"] = "1";
                                drRec["UPDATE_TIMESTAMP"] = dtNow;
                                drRec["UPD_DATE_TIME"] = dtNow;
                            }
                            else
                            {
                                if (datatype == (int)DataValueType.zhengshu || datatype == (int)DataValueType.fudian)
                                    drRec["NUMBER_VALUE"] = 0;
                                else
                                    drRec["STRING_VALUE"] = "0";

                                drRec["UPDATE_TIMESTAMP"] = dtNow;
                                drRec["UPD_DATE_TIME"] = dtNow;
                            }
                        }
                        else
                        {

                            string tmpContent = string.Empty;
                            tmpContent = tmpNode.Text.Replace(itemName + ":", "");

                            if (tmpContent.Equals(drRec["STRING_VALUE"].ToString()) == false)
                            {

                                if (datatype == (int)DataValueType.zhengshu || datatype == (int)DataValueType.fudian)
                                    drRec["NUMBER_VALUE"] = Convert.ToInt32(tmpContent);
                                else
                                    drRec["STRING_VALUE"] = tmpContent;

                                drRec["UPDATE_TIMESTAMP"] = dtNow;
                                drRec["UPD_DATE_TIME"] = dtNow;
                            }
                        }

                    }
                }
                // 如果是新增
                if (lnNew && tmpNode.Checked == true)
                {
                    string tmpContent = string.Empty;
                    if (tmpNode.Text.ToString().Contains(":"))
                    {
                        tmpContent = tmpNode.Text.Replace(itemName + ":", "");
                    }
                    else
                    {
                        tmpContent = tmpNode.Text.Replace(itemName, "");
                    }

                    DataRow drNew = dsRec.Tables[0].NewRow();
                    string tmpDocEleId = string.Empty;
                    tmpDocEleId = tmpNode.Tag.ToString();

                    drNew["ID"] = getGUID();
                    drNew["DOC_NURSING_ID"] = _docNursingId;
                    drNew["DOC_ELEMENT_ID"] = System.Int32.Parse(tmpNode.Tag.ToString());
                    drNew["STRING_VALUE"] = tmpContent;
                    drNew["CREATE_TIMESTAMP"] = dtNow;
                    drNew["UPDATE_TIMESTAMP"] = dtNow;
                    drNew["UPD_DATE_TIME"] = dtNow;

                    try
                    {
                        if (attribute.IndexOf(EVAL_ITEM_TYPE.CHECKBOX.ToString()) >= 0)
                        {
                            if (tmpNode.Checked == true)
                            {
                                if (datatype == (int)DataValueType.zhengshu || datatype == (int)DataValueType.fudian)
                                {
                                    drNew["NUMBER_VALUE"] = 1;
                                    drNew["STRING_VALUE"] = " ";
                                }
                                else
                                {
                                    drNew["STRING_VALUE"] = "1";
                                    drNew["NUMBER_VALUE"] = 0;
                                }

                            }
                            else
                            {

                                drNew["NUMBER_VALUE"] = 0;
                                drNew["STRING_VALUE"] = "0";
                            }
                        }
                        else
                        {
                            if (datatype == (int)DataValueType.zhengshu || datatype == (int)DataValueType.fudian)
                            {
                                drNew["NUMBER_VALUE"] = Convert.ToInt32(tmpContent);
                                drNew["STRING_VALUE"] = " ";
                            }
                            else
                            {
                                drNew["STRING_VALUE"] = tmpContent;
                                drNew["NUMBER_VALUE"] = 0;
                            }

                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    dsRec.Tables[0].Rows.Add(drNew);
                }
            }
            return true;
        }


        private bool saveDispTag(DateTime dtNow)
        {
            string filter = string.Empty;
            DataRow[] drFind = null;

            // ---------- 记录人 ------------------------------
            if (recorder.Length == 0)
            {
                // 查找项目
                filter = "ELEMENT_NAME = " + SqlManager.SqlConvert(ITEM_RECORDER);

                DataRow drItemIn = null;
                drFind = dsItems.Tables[0].Select(filter);
                if (drFind.Length > 0)
                {
                    drItemIn = drFind[0];

                    // 查找记录
                    drFind = dsRec.Tables[0].Select(filter);

                    if (drFind.Length == 0)
                    {
                        DataRow drNew = dsRec.Tables[0].NewRow();

                        drNew["PATIENT_ID"] = GVars.Patient.ID;
                        drNew["VISIT_ID"] = GVars.Patient.VisitId;
                        drNew["DICT_ID"] = _dictId;
                        drNew["ITEM_ID"] = drItemIn["ITEM_ID"];
                        drNew["ITEM_NAME"] = drItemIn["ITEM_NAME"];
                        drNew["ITEM_VALUE"] = GVars.User.Name;
                        drNew["DEPT_CODE"] = GVars.User.DeptCode;
                        drNew["RECORD_DATE"] = dtRecord;            // dtRecord.Date;
                        drNew["UPD_DATE_TIME"] = dtNow;

                        dsRec.Tables[0].Rows.Add(drNew);
                    }
                }

                recorder = GVars.User.Name;
            }

            // ---------- 记录时间 ------------------------------
            // 查找项目
            filter = "ELEMENT_NAME = " + SqlManager.SqlConvert(ITEM_RECORD_TIME);

            drFind = dsItems.Tables[0].Select(filter);
            if (drFind.Length > 0)
            {
                DataRow drItemIn = drFind[0];

                // 查找记录
                drFind = dsRec.Tables[0].Select(filter);

                if (drFind.Length == 0)
                {
                    DataRow drNew = dsRec.Tables[0].NewRow();

                    drNew["PATIENT_ID"] = GVars.Patient.ID;
                    drNew["VISIT_ID"] = GVars.Patient.VisitId;
                    drNew["DICT_ID"] = _dictId;
                    drNew["ITEM_ID"] = drItemIn["ITEM_ID"];
                    drNew["ITEM_NAME"] = drItemIn["ITEM_NAME"];
                    drNew["ITEM_VALUE"] = dtNow.ToString(ComConst.FMT_DATE.LONG);
                    drNew["DEPT_CODE"] = GVars.User.DeptCode;
                    drNew["RECORD_DATE"] = dtRecord;        //dtRecord.Date;
                    drNew["UPD_DATE_TIME"] = dtNow;

                    dsRec.Tables[0].Rows.Add(drNew);
                }
            }

            return true;
        }


        /// <summary>
        /// 传播结果
        /// </summary>
        private void sprideResult(TreeNode node)
        {
            if (dsAppConfig.Tables[0].Rows.Count <= 0)
            {
                return;
            }

            if (node.Tag == null || node.Tag.ToString().Length == 0)
            {
                return;
            }

            // 查找节点记录
            int recIdx = (int)(node.Tag);
            if (recIdx < 0 || recIdx >= drItems.Length)
            {
                return;
            }

            DataRow drItem = drItems[recIdx];

            // 判断有无相关内容
            string paraName = "EI_" + _dictId + "_" + drItem["ITEM_ID"].ToString();
            string filter = "PARAMETER_NAME = " + SqlManager.SqlConvert(paraName);
            DataRow[] drFind = dsAppConfig.Tables[0].Select(filter);
            if (drFind.Length == 0)
            {
                return;
            }

            // 派生值
            while (drFind.Length > 0)
            {
                // 源节点的值
                string srvVal = getItemValue(node.Text);

                // 查找目标节点
                TreeNode nodeDest = null;
                string itemId_dest = drFind[0]["PARAMETER_VALUE"].ToString().Trim();
                string resultRng = drFind[0]["PARAMETER_SCOPE"].ToString().Trim();

                for (int i = 0; i < arrNodes.Count; i++)
                {
                    recIdx = (int)(((TreeNode)(arrNodes[i])).Tag);
                    if (drItems[recIdx]["ITEM_ID"].ToString().Trim().Equals(itemId_dest) == true)
                    {
                        drItem = drItems[recIdx];
                        nodeDest = (TreeNode)(arrNodes[i]);
                        break;
                    }
                }

                if (nodeDest == null) return;

                // 目标节点赋值
                switch (drFind[0]["MEMO"].ToString().Trim().ToUpper())
                {
                    case "SUM":         // 合计总分
                        string sumVal = getSumVal(node);
                        nodeDest.Text = drItem["ITEM_NAME"].ToString();
                        if (sumVal.Length > 0)
                        {
                            nodeDest.Checked = true;
                            nodeDest.Text += SEP_CHAR + sumVal;
                        }
                        else
                        {
                            nodeDest.Checked = false;
                        }
                        break;
                    case "EVAL":        // 评估结果
                        nodeDest.Text = drItem["ITEM_NAME"].ToString();
                        if (srvVal.Length > 0)
                        {
                            nodeDest.Checked = true;
                            nodeDest.Text += SEP_CHAR + getValDesc(srvVal, resultRng);
                        }
                        else
                        {
                            nodeDest.Checked = false;
                        }
                        break;
                    case "EV_DATE":     // 评估日期
                        nodeDest.Text = drItem["ITEM_NAME"].ToString();
                        if (srvVal.Length > 0)
                        {
                            nodeDest.Checked = true;
                            nodeDest.Text += SEP_CHAR + GVars.sqlceLocal.GetSysDate().ToString(ComConst.FMT_DATE.LONG);
                        }
                        else
                        {
                            nodeDest.Checked = false;
                        }
                        break;
                    case "EV_RECORDER": // 评估人
                        nodeDest.Text = drItem["ITEM_NAME"].ToString();
                        if (srvVal.Length > 0)
                        {
                            nodeDest.Checked = true;
                            nodeDest.Text += SEP_CHAR + GVars.User.Name;
                        }
                        else
                        {
                            nodeDest.Checked = false;
                        }
                        break;
                    default:
                        break;
                }

                // 查找下一个影响到的节点
                node = nodeDest;

                paraName = "EI_" + _dictId + "_" + drItem["ITEM_ID"].ToString();
                filter = "PARAMETER_NAME = " + SqlManager.SqlConvert(paraName);
                drFind = dsAppConfig.Tables[0].Select(filter);
            }
        }


        /// <summary>
        /// 获取合计值
        /// </summary>
        /// <returns></returns>
        private string getSumVal(TreeNode node)
        {
            TreeNodeCollection searchNodes = null;
            if (node.Parent == null)
            {
                searchNodes = trvItems.Nodes;
            }
            else
            {
                searchNodes = node.Parent.Nodes;
            }

            int sum = 0;

            for (int i = 0; i < searchNodes.Count; i++)
            {
                if (searchNodes[i].Checked == false) continue;

                string text = searchNodes[i].Text;
                sum += getIntValInText(text);
            }

            if (sum > 0) return sum.ToString();
            return string.Empty;
        }


        /// <summary>
        /// 获取字符串中的数值
        /// </summary>
        /// <returns></returns>
        private int getIntValInText(string str)
        {
            int val = 0;
            string valStr = string.Empty;

            for (int i = 0; i < str.Length; i++)
            {
                if ("0123456789".Contains(str.Substring(i, 1)) == true)
                {
                    valStr += str.Substring(i, 1);
                }
                else if (valStr.Length > 0)
                {
                    break;
                }
            }

            if (valStr.Length > 0)
            {
                val = int.Parse(valStr);
            }

            return val;
        }


        /// <summary>
        /// 获取值描述
        /// </summary>
        /// <returns></returns>
        private string getValDesc(string srcVal, string scope)
        {
            if (srcVal.Length == 0)
            {
                return string.Empty;
            }

            int valTemp = int.Parse(srcVal);

            // 特重型(3-5); 重型(6-8);中型(9-12);轻型(13-15)
            string[] parts = scope.Split(")".ToCharArray());
            for (int i = 0; i < parts.Length; i++)
            {
                string[] partsIn = parts[i].Split("(".ToCharArray());
                if (partsIn.Length != 2) continue;

                string[] partsVal = partsIn[1].Split("-".ToCharArray());
                if (partsVal.Length != 2) continue;

                if (int.Parse(partsVal[0]) <= valTemp && valTemp <= int.Parse(partsVal[1]))
                {
                    return partsIn[0].Trim();
                }
            }

            return string.Empty;
        }
        #endregion


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
                if (barcode.IndexOf(ComConst.STR.BLANK) < 0 && barcode.IndexOf("T") < 0)
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



        #region tmpplate
        //
        //获取模板元素，李治鹏，2014-11-19
        //
        private void getTemplate()
        {
            string tmpsql = string.Empty;
            // 多日次评估
            //if (_oneTime == false)
            //{

            //    tmpsql = "SELECT * FROM DOC_TEMPLATE WHERE TEMPLATE_NAME = '每日护理评估单'";
            //}
            //// 入院评估
            //else
            //{
            //    //获取模板id
            //    tmpsql = "SELECT * FROM DOC_TEMPLATE WHERE TEMPLATE_NAME = '住院护理评估单'";

            //}

            tmpsql = "SELECT * FROM DOC_TEMPLATE WHERE TEMPLATE_NAME = '" + this.Text + "'";

            DataSet tmpds = GVars.sqlceLocal.SelectData(tmpsql, "DOC_CONTROL_TEMPLATE");

            if (tmpds == null || tmpds.Tables[0].Rows.Count <= 0)
            {
                return;
            }
            //获取模板内容元素
            for (int i = 0; i < tmpds.Tables[0].Rows.Count; i++)
            {
                _templateId = tmpds.Tables[0].Rows[i]["TEMPLATE_ID"].ToString();
            }

            //元素
            dsItems = evalDbi.GetDictItem2(_templateId);

            tmpsql = string.Empty;
            tmpds = null;
            if (_oneTime == false)
            {
                tmpsql = "SELECT * FROM DOC_NURSING WHERE PATIENT_ID = '" + GVars.Patient.ID
                        + "' AND VISIT_NO = " + GVars.Patient.VisitId
                        + " AND TEMPLATE_ID = " + _templateId
                        + " AND DateDiff(dd ,CREATE_TIMESTAMP,getdate()) = 0   ORDER BY UPDATE_TIMESTAMP DESC";
            }
            else
            {
                tmpsql = "SELECT * FROM DOC_NURSING WHERE PATIENT_ID = '" + GVars.Patient.ID
                        + "' AND VISIT_NO = " + GVars.Patient.VisitId
                        + " AND TEMPLATE_ID = " + _templateId
                        + " ORDER BY UPDATE_TIMESTAMP DESC";
            }

            tmpds = GVars.sqlceLocal.SelectData(tmpsql, "DOC_NURSING");
            if (tmpds.Tables[0].Rows.Count > 0 && _isAddPgd == false)
            {
                _docNursingId = tmpds.Tables[0].Rows[0]["ID"].ToString();
            }
            else//不存在记录，向主记录表添加记录
            {

                DateTime dtNow = GVars.GetDateNow();
                _docNursingId = getGUID();
                DataRow drNew = tmpds.Tables[0].NewRow();
                drNew["ID"] = _docNursingId;   //入院评估
                drNew["PATIENT_ID"] = GVars.Patient.ID;
                drNew["VISIT_NO"] = GVars.Patient.VisitId;
                drNew["WARD_CODE"] = GVars.User.DeptCode;
                drNew["TEMPLATE_ID"] = System.Int32.Parse(_templateId);
                drNew["TOTAL_SCORE"] = 0;
                drNew["CREATE_USER"] = GVars.User.ID;
                drNew["UPDATE_USER"] = GVars.User.ID;
                drNew["CREATE_TIMESTAMP"] = dtNow;
                drNew["UPDATE_TIMESTAMP"] = dtNow;
                drNew["UPD_DATE_TIME"] = dtNow;

                tmpds.Tables[0].Rows.Add(drNew);
                evalDbi.addNewMasterRecord(ref tmpds);

            }
            dsRec = evalDbi.GetEvalRecOne2(_docNursingId);
        }
        #endregion

        #region score
        //
        //修改分数,李治鹏，2014年12月2日
        //
        private void changeScore()
        {
            //计算总分
            double totalScore = 0;
            for (int i = 0; i < arrNodes.Count; i++)
            {
                TreeNode tmpNode = (TreeNode)arrNodes[i];
                DataRow drItem = null;
                string tmpFilter = "ID = " + tmpNode.Tag;

                DataRow[] tmpRows = dsItems.Tables[0].Select(tmpFilter);
                if (tmpRows.Count() <= 0)
                {
                    continue;
                }

                drItem = tmpRows[0];
                string attribute = drItem["CONTROL_TEMPLATE_ID"].ToString();

                if (attribute.IndexOf(EVAL_ITEM_TYPE.CHECKBOX.ToString()) >= 0 && true == tmpNode.Checked)
                {
                    double tmpItemScore = 0;
                    try
                    {
                        tmpItemScore = Convert.ToDouble(drItem["SCORE"].ToString());
                    }
                    catch (Exception ex)
                    {
                        tmpItemScore = 0;
                    }

                    totalScore = totalScore + tmpItemScore;
                }
            }

            //显示总分值
            for (int i = 0; i < arrNodes.Count; i++)
            {
                TreeNode tmpNode = (TreeNode)arrNodes[i];
                DataRow drItem = null;
                string tmpFilter = "ID = " + tmpNode.Tag;

                DataRow[] tmpRows = dsItems.Tables[0].Select(tmpFilter);
                if (tmpRows.Count() <= 0)
                {
                    continue;
                }
                drItem = tmpRows[0];
                int tmpRelationType = 0;
                tmpRelationType = Convert.ToInt32(drItem["RELATION_TYPE"].ToString());
                if (RelationType.总分 != (RelationType)tmpRelationType)
                {
                    continue;
                }
                tmpNode.Text = tmpNode.Text = drItem["ELEMENT_NAME"].ToString() + ":" + ((int)totalScore).ToString();
                tmpNode.Checked = true;
            }

        }
        #endregion


        #region GUID
        private string getGUID()
        {
            System.Guid tmpGuid = new Guid();
            tmpGuid = Guid.NewGuid();
            string tmpStr = string.Empty;
            tmpStr = tmpGuid.ToString().Replace("-", ""); ;
            return tmpStr;
        }
        #endregion



    }



    struct EVAL_ITEM_TYPE
    {
        public static int PARENT = 0;
        public static int TEXTBOX = 1;
        public static int LABEL = 2;
        public static int CHECKBOX = 3;
        public static int SELECTMUL = 4;
        public static int SELECTSINGLE = 5;
        public static int TEXTAREA = 6;
        public static int DATE = 7;
        public static int TIME = 8;
        public static int DATETIME = 9;
    }

    public enum DataValueType
    {
        zifu = 0,
        zhengshu = 1,
        fudian = 2,
    }

    /// <summary>    
    /// 关联类型，李治鹏
    /// </summary>
    public enum RelationType
    {
        默认,
        病人信息,
        签名,
        总分,
    }

    // <summary>
    /// 关联病人信息项，李治鹏
    /// </summary>
    public enum RelationPatientItem
    {
        病人Id,
        住院次数,
        住院号,
        姓名,
        性别,
        //            国籍, 
        //            民族,
        入院时间,
        //            入科时间,
        床号,
        床标号,
        生日,
        诊断,
        护理等级,
        //            年龄, 
        //            医保类别,
        //            病区名称,
        //            病区号,
        所在科室,
        //            身高, 
        //            体重,
        //            职业,
        //            邮政编码,
        //            家庭电话,
        //            联系人姓名,
        //            联系人电话,
        //            身份证号码,
        //            家庭住址,
        //            术后天数,
        //            中医诊断,
        主治医生,
        病情状态,
    }

}