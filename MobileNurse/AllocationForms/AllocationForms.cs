using DevExpress.XtraEditors;

namespace HISPlus
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public class AllocationForms : FormDo
    {
        private HISPlus.UserControls.UcButton bntColer;
        private HISPlus.UserControls.UcButton bntScan;
        private HISPlus.UserControls.UcButton btnSave;
        private HISPlus.UserControls.UcButton btnSaveSql;
        private string cellValue = string.Empty;
        private HISPlus.UserControls.UcComboBox cmbDept;
        private HISPlus.UserControls.UcComboBox cmbSql;
        private const string COL_ROW_INDEX = "NURSING_NO";
        private IContainer components = null;
        private ContextMenuStrip contextMnu_VitalSigns;
        private DbInfo dbAccess = null;
        private string deptCode = string.Empty;
        private DataSet dsConfig = null;
        private DataSet dsRecordContent = null;
        private DataSet dsWardList = null;
        private DateTime dtpBegin = DateTime.Now;
        private DateTime dtpEnd = DateTime.Now.AddDays(1.0);
        private PanelControl PanelControl3;
        private PanelControl grpSeach;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ToolStripMenuItem mnuVitalSigns_AddRec;
        private ToolStripMenuItem mnuVitalSigns_Delete;
        private RichTextBox RichTxtSql;
        private const string RIGHT_EDIT = "2";
        private const int SCROLL_STEP = 20;
        private UserControls.UcGridView ucGridView1;
        private UserControls.UcGridView dataSql;
        private const string WARD_CODE = "DEPT_CODE";

        public AllocationForms()
        {
            this.InitializeComponent();
            base._id = "00999";
            base._guid = "64C06336-670E-4f99-hsjy-817696C00999";

            this.cmbSql.DisplayMember = "CONFIGNAME";
            this.cmbSql.ValueMember = "CONFIGPARAMETERS";
        }

        private bool addNewLineInRec(int dgvRowBased, int rowIndexInRec, string colName, string newValue)
        {
            DataColumn column = null;
            DataRow row2 = this.dsRecordContent.Tables[0].NewRow();
            for (int i = 0; i < this.dsRecordContent.Tables[0].PrimaryKey.Length; i++)
            {
                column = this.dsRecordContent.Tables[0].PrimaryKey[i];
                row2[column.ColumnName] = ucGridView1.GetRowCellValue(dgvRowBased, column.ColumnName);
            }
            if (colName.Length > 0)
            {
                row2[colName] = newValue;
            }
            row2["NURSING_NO"] = rowIndexInRec;
            if (this.deptCode.Length > 0)
            {
                row2["DEPT_CODE"] = this.deptCode;
            }
            else
            {
                MessageBox.Show("科室为空呀！");
                return false;
            }
            this.dsRecordContent.Tables[0].Rows.Add(row2);
            return true;
        }

        private void bntColer_Click(object sender, EventArgs e)
        {
            this.bntColer.Visible = false;
            this.btnSaveSql.Visible = true;
            this.dataSql.Visible = false;
            this.RichTxtSql.Visible = true;
        }

        private void bntScan_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSql.SelectedValue == null) return;

                this.dataSql.Visible = true;
                this.RichTxtSql.Visible = false;
                this.btnSaveSql.Visible = false;
                this.bntColer.Visible = true;
                this.dataSql.Enabled = true;
                DataSet set = null;

                string str = this.cmbSql.SelectedValue.ToString();
                if (str.IndexOf(".") < 0)
                {
                    str = str + ".sql";
                }
                StreamReader reader = new StreamReader(Path.Combine(Path.Combine(Application.StartupPath, "SQL"), str), Encoding.GetEncoding("gb2312"));
                string sqlSel = reader.ReadToEnd();
                reader.Close();
                sqlSel = sqlSel.Replace("\r", string.Empty).Replace("\n", ComConst.STR.BLANK).Replace("{DATE_BEGIN}", SqlManager.GetOraDbDate_Short(this.dtpBegin)).Replace("{DATE_END}", SqlManager.GetOraDbDate_Short(this.dtpEnd));
                try
                {
                    set = GVars.OracleAccess.SelectData_NoKey(sqlSel);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message.ToString());
                }
                if (set != null)
                {
                    this.dataSql.DataSource = set.Tables[0].DefaultView;
                }
                else
                {
                    this.dataSql.DataSource = 0;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucGridView1.SelectRowsCount > 0)
                {
                    //if (this.dgvData.CurrentRow.ReadOnly)
                    //{
                    //    GVars.Msg.Show("I0013", "删除");
                    //}
                    //else
                    if (GVars.Msg.Show("Q0005") == DialogResult.Yes)
                    {
                        ucGridView1.DeleteSelectRow();
                        this.btnSave.Enabled = true;
                    }
                }
            }
            catch (Exception exception)
            {
                Error.ErrProc(exception);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string result = this.dbAccess.GetValueBySql("select max(nursing_no) from configuration");
                int nursingNo = int.Parse(string.IsNullOrEmpty(result) ? "0" : result);
                foreach (DataRow row in dsRecordContent.Tables[0].Rows)
                {
                    if (row.RowState == DataRowState.Added)
                    {
                        row["nursing_no"] = ++nursingNo;
                    }
                }
                this.dbAccess.SaveTableData(this.dsRecordContent);
                this.btnSave.Enabled = false;
                this.GetConfigMent();
                this.Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(exception);
            }
        }

        private void btnSaveSql_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Path.Combine(Application.StartupPath, "SQL"), this.cmbSql.SelectedValue.ToString() + ".SQL");
            this.RichTxtSql.SaveFile(path, RichTextBoxStreamType.PlainText);
            this.btnSaveSql.Enabled = false;
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.deptCode = this.cmbDept.SelectedValue.ToString();
                this.Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(exception);
            }
        }

        private void cmbSql_SelectedIndexChanged(object sender, EventArgs e)
        {
            Exception exception;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataSet set = null;
                string str = this.cmbSql.SelectedValue.ToString();
                if (str.IndexOf(".") < 0)
                {
                    str = str + ".sql";
                }
                str = Path.Combine(Path.Combine(Application.StartupPath, "SQL"), str);
                if (!File.Exists(str))
                {
                    MessageBox.Show(str + "   不存在!");
                    this.RichTxtSql.Text = string.Empty;
                }
                else
                {
                    StreamReader reader = new StreamReader(str, Encoding.GetEncoding("gb2312"));
                    string sqlSel = reader.ReadToEnd();
                    reader.Close();
                    this.RichTxtSql.Text = sqlSel;
                    if (this.dataSql.Visible)
                    {
                        sqlSel = sqlSel.Replace("\r", string.Empty).Replace("\n", ComConst.STR.BLANK).Replace("{DATE_BEGIN}", SqlManager.GetOraDbDate_Short(this.dtpBegin)).Replace("{DATE_END}", SqlManager.GetOraDbDate_Short(this.dtpEnd));
                        try
                        {
                            set = GVars.OracleAccess.SelectData_NoKey(sqlSel);
                        }
                        catch (Exception exception1)
                        {
                            exception = exception1;
                            MessageBox.Show(exception.Message.ToString());
                        }
                        if (set != null)
                        {
                            this.dataSql.DataSource = set.Tables[0].DefaultView;
                        }
                        else
                        {
                            this.dataSql.DataSource = null;
                        }
                    }
                }
                this.btnSaveSql.Enabled = false;
                this.Cursor = Cursors.Default;
            }
            catch (Exception exception2)
            {
                exception = exception2;
                this.Cursor = Cursors.Default;
                Error.ErrProc(exception);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void GetConfigMent()
        {
            string sqlSel = "select  * from  configuration  where dept_code =" + SqlManager.SqlConvert(GVars.User.DeptCode);
            this.dsConfig = GVars.OracleAccess.SelectData(sqlSel, "configuration");

            this.cmbSql.DataSource = this.dsConfig.Tables[0].DefaultView;
        }

        private string getFilterFromDgvRow(int dgvRowIndex, ref int rowIndexInRec)
        {
            rowIndexInRec = -1;
            DataRowView row = ucGridView1.GetRow(dgvRowIndex) as DataRowView;
            if (row == null) throw new ArgumentNullException("row");

            DataColumn column = null;
            string str = string.Empty;
            for (int i = 0; i < this.dsRecordContent.Tables[0].PrimaryKey.Length; i++)
            {
                column = this.dsRecordContent.Tables[0].PrimaryKey[i];
                if (column.ColumnName.ToUpper().Equals("NURSING_NO"))
                {
                    rowIndexInRec = int.Parse(ucGridView1.GetRowCellValue(dgvRowIndex,"NURSING_NO").ToString());
                }
                else
                {
                    if (str.Length > 0)
                    {
                        str = str + " AND ";
                    }
                    if (column.DataType == typeof(DateTime))
                    {
                        DateTime time = (DateTime)row[column.ColumnName];
                        string str3 = str;
                        str3 = str3 + "(" + column.ColumnName + " >= " + SqlManager.SqlConvert(time.ToString(ComConst.FMT_DATE.LONG));
                        str = (str3 + "AND " + column.ColumnName + " < " + SqlManager.SqlConvert(time.AddSeconds(1.0).ToString(ComConst.FMT_DATE.LONG))) + ") ";
                    }
                    else
                    {
                        str = (str + column.ColumnName + " = ") + SqlManager.SqlConvert(row[column.ColumnName].ToString());
                    }
                }
            }
            return str;
        }

        private DataSet getNursingRecord(string deptCode)
        {
            string filter = "DEPT_CODE=" + SqlManager.SqlConvert(deptCode);
            return this.dbAccess.GetTableData("CONFIGURATION", filter);
        }

        private void initdisp()
        {
            this.dbAccess = new DbInfo(GVars.OracleAccess);
            base._userRight = GVars.User.GetUserFrmRight(base._id);
        }

        private void initFrmVal()
        {
            this.deptCode = GVars.User.DeptCode;
            this.dsRecordContent = this.getNursingRecord(this.deptCode);
            
            ucGridView1.AllowEdit = true;
            ucGridView1.Add("NURSING_NO", "NURSING_NO", false);
            ucGridView1.Add("DEPT_CODE", "DEPT_CODE", false);
            ucGridView1.Add("统计名称和模版名称", "CONFIGNAME");
            ucGridView1.Add("统计对应SQL文件名(.sql)", "CONFIGPARAMETERS");

            ucGridView1.CellValueChanged += ucGridView1_CellValueChanged;

            ucGridView1.Init();
            ucGridView1.DataSource = this.dsRecordContent.Tables[0].DefaultView;
             
            this.btnSave.Enabled = false;
            string sqlSel = "SELECT * FROM DEPT_DICT WHERE CLINIC_ATTR = '2'";
            this.dsWardList = GVars.OracleAccess.SelectData(sqlSel, "DEPT_DICT");
            this.cmbDept.DisplayMember = "DEPT_NAME";
            this.cmbDept.ValueMember = "DEPT_CODE";
            this.dsWardList.Tables[0].DefaultView.Sort = "DEPT_CODE";
            this.cmbDept.DataSource = this.dsWardList.Tables[0].DefaultView;
            if (GVars.User.DeptCode.Length == 0)
            {
                this.cmbDept.Enabled = true;
            }
            else
            {
                this.cmbDept.SelectedValue = GVars.User.DeptCode;
                this.cmbDept.Enabled = !this.cmbDept.SelectedValue.ToString().Equals(GVars.User.DeptCode);
            }
            this.GetConfigMent();
            base._userRight = GVars.User.GetUserFrmRight(base._id);
        }

        void ucGridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllocationForms));
            this.contextMnu_VitalSigns = new System.Windows.Forms.ContextMenuStrip();
            this.mnuVitalSigns_AddRec = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVitalSigns_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.ucGridView1 = new HISPlus.UserControls.UcGridView();
            this.grpSeach = new DevExpress.XtraEditors.PanelControl();
            this.cmbDept = new HISPlus.UserControls.UcComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.PanelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.dataSql = new HISPlus.UserControls.UcGridView();
            this.bntColer = new HISPlus.UserControls.UcButton();
            this.RichTxtSql = new System.Windows.Forms.RichTextBox();
            this.bntScan = new HISPlus.UserControls.UcButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSql = new HISPlus.UserControls.UcComboBox();
            this.btnSaveSql = new HISPlus.UserControls.UcButton();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMnu_VitalSigns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpSeach)).BeginInit();
            this.grpSeach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PanelControl3)).BeginInit();
            this.PanelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMnu_VitalSigns
            // 
            this.contextMnu_VitalSigns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuVitalSigns_AddRec,
            this.mnuVitalSigns_Delete});
            this.contextMnu_VitalSigns.Name = "contextMnu_VitalSigns";
            this.contextMnu_VitalSigns.Size = new System.Drawing.Size(154, 52);
            // 
            // mnuVitalSigns_AddRec
            // 
            this.mnuVitalSigns_AddRec.Name = "mnuVitalSigns_AddRec";
            this.mnuVitalSigns_AddRec.Size = new System.Drawing.Size(153, 24);
            this.mnuVitalSigns_AddRec.Text = "插入新纪录";
            this.mnuVitalSigns_AddRec.Click += new System.EventHandler(this.mnuVitalSigns_AddRec_Click);
            // 
            // mnuVitalSigns_Delete
            // 
            this.mnuVitalSigns_Delete.Name = "mnuVitalSigns_Delete";
            this.mnuVitalSigns_Delete.Size = new System.Drawing.Size(153, 24);
            this.mnuVitalSigns_Delete.Text = "删除";
            this.mnuVitalSigns_Delete.Click += new System.EventHandler(this.mnuVitalSigns_Delete_Click);
            // 
            // ucGridView1
            // 
            this.ucGridView1.AllowAddRows = false;
            this.ucGridView1.AllowDeleteRows = false;
            this.ucGridView1.AllowEdit = false;
            this.ucGridView1.AllowSort = false;
            this.ucGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ucGridView1.ColumnAutoWidth = true;
            this.ucGridView1.ColumnsEvenOldRowColor = null;
            this.ucGridView1.ContextMenuStrip = this.contextMnu_VitalSigns;
            this.ucGridView1.DataSource = null;
            this.ucGridView1.Location = new System.Drawing.Point(12, 12);
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(544, 449);
            this.ucGridView1.TabIndex = 2;
            // 
            // grpSeach
            // 
            this.grpSeach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpSeach.Controls.Add(this.cmbDept);
            this.grpSeach.Controls.Add(this.label2);
            this.grpSeach.Controls.Add(this.btnSave);
            this.grpSeach.Location = new System.Drawing.Point(12, 467);
            this.grpSeach.Name = "grpSeach";
            this.grpSeach.Size = new System.Drawing.Size(544, 68);
            this.grpSeach.TabIndex = 40;
            // 
            // cmbDept
            // 
            this.cmbDept.DataSource = null;
            this.cmbDept.DisplayMember = null;
            this.cmbDept.DropDownHeight = 0;
            this.cmbDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDept.DropDownWidth = 0;
            this.cmbDept.DroppedDown = false;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.IntegralHeight = true;
            this.cmbDept.Location = new System.Drawing.Point(64, 22);
            this.cmbDept.MaxDropDownItems = 0;
            this.cmbDept.MaximumSize = new System.Drawing.Size(1000, 24);
            this.cmbDept.MinimumSize = new System.Drawing.Size(40, 24);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.SelectedIndex = -1;
            this.cmbDept.SelectedValue = null;
            this.cmbDept.Size = new System.Drawing.Size(133, 24);
            this.cmbDept.TabIndex = 1;
            this.cmbDept.ValueMember = null;
            this.cmbDept.SelectedIndexChanged += cmbDept_SelectedIndexChanged;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "科室";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageRight = false;
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(432, 22);
            this.btnSave.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 6;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // PanelControl3
            // 
            this.PanelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelControl3.Controls.Add(this.dataSql);
            this.PanelControl3.Controls.Add(this.bntColer);
            this.PanelControl3.Controls.Add(this.RichTxtSql);
            this.PanelControl3.Controls.Add(this.bntScan);
            this.PanelControl3.Controls.Add(this.label5);
            this.PanelControl3.Controls.Add(this.label4);
            this.PanelControl3.Controls.Add(this.label3);
            this.PanelControl3.Controls.Add(this.cmbSql);
            this.PanelControl3.Controls.Add(this.btnSaveSql);
            this.PanelControl3.Controls.Add(this.label1);
            this.PanelControl3.Location = new System.Drawing.Point(562, 12);
            this.PanelControl3.Name = "PanelControl3";
            this.PanelControl3.Size = new System.Drawing.Size(667, 523);
            this.PanelControl3.TabIndex = 42;
            // 
            // dataSql
            // 
            this.dataSql.AllowAddRows = false;
            this.dataSql.AllowDeleteRows = false;
            this.dataSql.AllowEdit = false;
            this.dataSql.AllowSort = false;
            this.dataSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataSql.ColumnAutoWidth = true;
            this.dataSql.ColumnsEvenOldRowColor = null;
            this.dataSql.DataSource = null;
            this.dataSql.Location = new System.Drawing.Point(15, 64);
            this.dataSql.Name = "dataSql";
            this.dataSql.ShowRowIndicator = false;
            this.dataSql.Size = new System.Drawing.Size(629, 355);
            this.dataSql.TabIndex = 27;
            this.dataSql.Visible = false;
            // 
            // bntColer
            // 
            this.bntColer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bntColer.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bntColer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntColer.Image = ((System.Drawing.Image)(resources.GetObject("bntColer.Image")));
            this.bntColer.ImageRight = false;
            this.bntColer.ImageStyle = HISPlus.UserControls.ImageStyle.Close;
            this.bntColer.Location = new System.Drawing.Point(519, 22);
            this.bntColer.MaximumSize = new System.Drawing.Size(120, 30);
            this.bntColer.MinimumSize = new System.Drawing.Size(60, 30);
            this.bntColer.Name = "bntColer";
            this.bntColer.Size = new System.Drawing.Size(100, 30);
            this.bntColer.TabIndex = 26;
            this.bntColer.TextValue = "关闭预览";
            this.bntColer.UseVisualStyleBackColor = true;
            this.bntColer.Visible = false;
            this.bntColer.Click += new System.EventHandler(this.bntColer_Click);
            // 
            // RichTxtSql
            // 
            this.RichTxtSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTxtSql.Location = new System.Drawing.Point(14, 64);
            this.RichTxtSql.Name = "RichTxtSql";
            this.RichTxtSql.Size = new System.Drawing.Size(630, 355);
            this.RichTxtSql.TabIndex = 0;
            this.RichTxtSql.Text = "";
            this.RichTxtSql.TextChanged += new System.EventHandler(this.RichTxtSql_TextChanged);
            // 
            // bntScan
            // 
            this.bntScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bntScan.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bntScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntScan.Image = ((System.Drawing.Image)(resources.GetObject("bntScan.Image")));
            this.bntScan.ImageRight = false;
            this.bntScan.ImageStyle = HISPlus.UserControls.ImageStyle.Preview;
            this.bntScan.Location = new System.Drawing.Point(396, 22);
            this.bntScan.MaximumSize = new System.Drawing.Size(100, 30);
            this.bntScan.MinimumSize = new System.Drawing.Size(60, 30);
            this.bntScan.Name = "bntScan";
            this.bntScan.Size = new System.Drawing.Size(89, 30);
            this.bntScan.TabIndex = 24;
            this.bntScan.TextValue = "预览SQL";
            this.bntScan.UseVisualStyleBackColor = true;
            this.bntScan.Click += new System.EventHandler(this.bntScan_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(84, 479);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(189, 20);
            this.label5.TabIndex = 23;
            this.label5.Text = "结束时间{DATE_END}";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(84, 456);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(209, 20);
            this.label4.TabIndex = 22;
            this.label4.Text = "开始时间{DATE_BEGIN}";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(11, 436);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 20);
            this.label3.TabIndex = 21;
            this.label3.Text = "SQL查询时间规范：";
            // 
            // cmbSql
            // 
            this.cmbSql.DataSource = null;
            this.cmbSql.DisplayMember = null;
            this.cmbSql.DropDownHeight = 0;
            this.cmbSql.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSql.DropDownWidth = 0;
            this.cmbSql.DroppedDown = false;
            this.cmbSql.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSql.FormattingEnabled = true;
            this.cmbSql.IntegralHeight = true;
            this.cmbSql.Location = new System.Drawing.Point(142, 28);
            this.cmbSql.MaxDropDownItems = 0;
            this.cmbSql.MaximumSize = new System.Drawing.Size(1000, 24);
            this.cmbSql.MinimumSize = new System.Drawing.Size(40, 24);
            this.cmbSql.Name = "cmbSql";
            this.cmbSql.SelectedIndex = -1;
            this.cmbSql.SelectedValue = null;
            this.cmbSql.Size = new System.Drawing.Size(132, 24);
            this.cmbSql.TabIndex = 20;
            this.cmbSql.ValueMember = null;
            this.cmbSql.SelectedIndexChanged += new System.EventHandler(this.cmbSql_SelectedIndexChanged);
            // 
            // btnSaveSql
            // 
            this.btnSaveSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSql.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSaveSql.Enabled = false;
            this.btnSaveSql.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSql.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveSql.Image")));
            this.btnSaveSql.ImageRight = false;
            this.btnSaveSql.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSaveSql.Location = new System.Drawing.Point(519, 446);
            this.btnSaveSql.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnSaveSql.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnSaveSql.Name = "btnSaveSql";
            this.btnSaveSql.Size = new System.Drawing.Size(100, 30);
            this.btnSaveSql.TabIndex = 12;
            this.btnSaveSql.TextValue = "保存SQL";
            this.btnSaveSql.UseVisualStyleBackColor = true;
            this.btnSaveSql.Click += new System.EventHandler(this.btnSaveSql_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(11, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "SQL参数名称:";
            // 
            // AllocationForms
            // 
            this.ClientSize = new System.Drawing.Size(1241, 547);
            this.Controls.Add(this.PanelControl3);
            this.Controls.Add(this.grpSeach);
            this.Controls.Add(this.ucGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AllocationForms";
            this.Text = "配置窗体";
            this.Load += new System.EventHandler(this.NursingClassName_Load);
            this.contextMnu_VitalSigns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpSeach)).EndInit();
            this.grpSeach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelControl3)).EndInit();
            this.PanelControl3.ResumeLayout(false);
            this.PanelControl3.PerformLayout();
            this.ResumeLayout(false);

        }

        private void mnuVitalSigns_AddRec_Click(object sender, EventArgs e)
        {
            Exception exception;
            if (this.dsRecordContent.Tables[0].Rows.Count == 0)
            {
                try
                {
                    DataRow row = this.dsRecordContent.Tables[0].NewRow();
                    row["NURSING_NO"] = 1;
                    if (this.deptCode.Length > 0)
                    {
                        row["DEPT_CODE"] = this.deptCode;
                    }
                    else
                    {
                        MessageBox.Show("科室为空呀！");
                    }
                    this.dsRecordContent.Tables[0].Rows.Add(row);
                    this.btnSave.Enabled = true;
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    Error.ErrProc(exception);
                }
            }
            else
            {
                try
                {
                    int rowIndexInRec = -1;
                    string filterExpression = this.getFilterFromDgvRow(0, ref rowIndexInRec);
                    DataRow[] rowArray = this.dsRecordContent.Tables[0].Select(filterExpression, "NURSING_NO");
                    if (rowArray.Length != 0)
                    {
                        for (int i = rowArray.Length - 1; i >= 0; i--)
                        {
                            if (int.Parse(rowArray[i]["NURSING_NO"].ToString()) <= rowIndexInRec)
                            {
                                break;
                            }
                            rowArray[i]["NURSING_NO"] = int.Parse(rowArray[i]["NURSING_NO"].ToString()) + 1;
                        }
                        this.addNewLineInRec(0, rowIndexInRec + 1, string.Empty, string.Empty);
                        this.btnSave.Enabled = true;
                    }
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    Error.ErrProc(exception);
                }
            }
        }

        private void mnuVitalSigns_Delete_Click(object sender, EventArgs e)
        {
            this.btnDelete_Click(sender, e);
        }

        private void NursingClassName_Load(object sender, EventArgs e)
        {
            bool userInput = GVars.App.UserInput;
            try
            {
                GVars.App.UserInput = false;
                this.cmbSql.SelectedIndexChanged -= new EventHandler(this.cmbSql_SelectedIndexChanged);
                this.initdisp();
                this.initFrmVal();
                this.cmbSql.SelectedIndexChanged += new EventHandler(this.cmbSql_SelectedIndexChanged);
            }
            catch (Exception exception)
            {
                Error.ErrProc(exception);
            }
            finally
            {
                GVars.App.UserInput = userInput;
            }
        }

        private void RichTxtSql_TextChanged(object sender, EventArgs e)
        {
            this.btnSaveSql.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.dsRecordContent.Tables[0].Rows.Count == 0)
            {
            }
        }
    }
}

