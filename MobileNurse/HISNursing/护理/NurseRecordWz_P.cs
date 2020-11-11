namespace HISPlus
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class NurseRecordWz_P : FormDo
    {
        protected int _lines = 0;
        private string _userRight = string.Empty;
        public string[] ArrDesc1 = null;
        private string BedLabel = string.Empty;
        private Button btnContinuePrint;
        private Button btnExit;
        private Button btnQuery;
        private Button btnReprint;
        private Button btnSave;
        private Button btUpData;
        private Button button1;
        private Button button10;
        private Button button11;
        private Button button12;
        private Button button13;
        private Button button14;
        private Button button15;
        private Button button16;
        private Button button17;
        private Button button18;
        private Button button19;
        private Button button2;
        private Button button20;
        private Button button21;
        private Button button22;
        private Button button23;
        private Button button24;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private string CellTemp = string.Empty;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column13;
        private DataGridViewTextBoxColumn Column14;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewButtonColumn Column16;
        private DataGridViewTextBoxColumn Column17;
        private DataGridViewTextBoxColumn Column18;
        private DataGridViewTextBoxColumn Column19;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private IContainer components = null;
        public ContextMenuStrip contextMenuStrip1;
        private string datime = string.Empty;
        public string Desc = string.Empty;
        public string desc1 = string.Empty;
        private DataSet ds2;
        private DataSet dsPatient = null;
        private DateTimePicker dtRngEnd;
        private DateTimePicker dtRngStart;
        private DataTable dtTemp;
        private bool enabled = false;
        private string GridViewVstr;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private int GVCindex;
        private int GVRindex;
        private Label label1;
        private Label label10;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label9;
        private Label lblAge;
        private Label lblDeptName;
        private Label lblDiagnose;
        private Label lblDocNo;
        private Label lblGender;
        private Label lblName;
        private int linenumber = 10;
        private string linetime = "";
        public int loger;
        private object obj = null;
        private int pagenum = 1;
        private int pagex = 0;
        private Panel panel1;
        private PatientDbI patientCom = new PatientDbI(GVars.OracleAccess);
        private DataGridView PatientGridView;
        private string patientId = string.Empty;
        private int printrows = 0x19;
        private const string RIGHT_EDIT = "2";
        private int rownum = 0;
        public string strdoce = string.Empty;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private ToolStripMenuItem ToolStripNoRed;
        private ToolStripMenuItem ToolStripRed;
        private ToolStripMenuItem ToolStripTime;
        private ToolStripMenuItem ToolStripXJTJ;
        private ToolStripMenuItem ToolStripZJTJ;
        private bool toselect = true;
        private TextBox txtBedLabel;
        private string visitId = string.Empty;

        public NurseRecordWz_P()
        {
            base._id = "00043";
            base._guid = "237118F0-6093-49d4-B10C-77C7788CC13E";
            this.InitializeComponent();
            this.txtBedLabel.GotFocus += new EventHandler(this.imeCtrl_GotFocus);
        }

        private void btnContinuePrint_Click(object sender, EventArgs e)
        {
            int num = 0;
            num = this.StringtoInt(this.textBox3.Text.Trim());
            if (num >= 1)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    string sqlSel = (((((string.Empty + "SELECT PRINTTYPE,PAGE_INDEX,ROWS_PAGE_INDEX" + " FROM ") + "NURSING_RECORD " + "WHERE ") + "PATIENT_ID = " + SqlManager.SqlConvert(this.patientId)) + " AND VISIT_ID = " + SqlManager.SqlConvert(this.visitId)) + " AND PAGE_INDEX = " + num) + " ORDER BY " + " NURSING_DATE ASC";
                    DataTable table = GVars.OracleAccess.SelectData(sqlSel).Tables[0];
                    if (table.Rows.Count > 0)
                    {
                        int num2 = this.StringtoInt(table.Rows[0]["PRINTTYPE"].ToString());
                        int num3 = this.StringtoInt(table.Rows[0]["PAGE_INDEX"].ToString());
                        int num4 = this.StringtoInt(table.Rows[0]["ROWS_PAGE_INDEX"].ToString());
                        sqlSel = (((((string.Empty + "SELECT *" + " FROM ") + "NURSING_RECORD " + "WHERE ") + "PATIENT_ID = " + SqlManager.SqlConvert(this.patientId)) + " AND VISIT_ID = " + SqlManager.SqlConvert(this.visitId)) + " AND PAGE_INDEX = " + num) + " ORDER BY " + " NURSING_DATE ASC";
                        DataTable table2 = GVars.OracleAccess.SelectData(sqlSel).Tables[0];
                        sqlSel = (((string.Empty + "SELECT *" + " FROM ") + "NURSING_RECORD " + "WHERE ") + "PATIENT_ID = " + SqlManager.SqlConvert(this.patientId)) + " AND VISIT_ID = " + SqlManager.SqlConvert(this.visitId);
                        sqlSel = (sqlSel + " AND PAGE_INDEX = " + (num + 1)) + " ORDER BY " + " NURSING_DATE ASC";
                        DataTable table3 = GVars.OracleAccess.SelectData(sqlSel).Tables[0];
                        if (table2.Rows.Count > 0)
                        {
                            DataRow row;
                            string[] strArray;
                            string[] strArray2;
                            string[] strArray3;
                            string[] strArray4;
                            string[] strArray5;
                            int length;
                            int num9;
                            int num10;
                            int num11;
                            int num12;
                            int num13;
                            int num14;
                            int num15;
                            this.dtTemp = new DataTable();
                            for (int i = 0; i < 13; i++)
                            {
                                DataColumn column = new DataColumn {
                                    DataType = System.Type.GetType("System.String"),
                                    ColumnName = i.ToString()
                                };
                                this.dtTemp.Columns.Add(column);
                            }
                            this.linenumber = int.Parse(GVars.IniFile.ReadString("LINENUMBER", "NurseRecordWz", string.Empty));
                            this.printrows = int.Parse(GVars.IniFile.ReadString("LINENUMBER", "NURSING_RECORD_PRINT", string.Empty));
                            int num7 = 0;
                            if ((num != 1) && ((num4 != 0) && (num2 != 0)))
                            {
                                num7 = 1;
                                row = this.dtTemp.NewRow();
                                strArray = table2.Rows[0]["DRUG_NAME"].ToString().Replace(".\r\n", "|").Split(new char[] { '|' });
                                strArray2 = table2.Rows[0]["DRUG_AMOUNT"].ToString().Replace("\r\n", "|").Split(new char[] { '|' });
                                strArray3 = table2.Rows[0]["FOOD_NAME"].ToString().Replace(".\r\n", "|").Split(new char[] { '|' });
                                strArray4 = table2.Rows[0]["FOOD_AMOUNT"].ToString().Replace("\r\n", "|").Split(new char[] { '|' });
                                strArray5 = this.TextEditDesc1(table2.Rows[0]["NURSING_STATE_ILL"].ToString(), this.linenumber).Replace("\r\n", "⊙").Split(new char[] { '⊙' });
                                length = strArray.Length;
                                num9 = strArray3.Length;
                                num10 = strArray5.Length;
                                if ((length > num9) && (length > num10))
                                {
                                    num11 = length - num2;
                                    for (num12 = num11; num12 < length; num12++)
                                    {
                                        row = this.dtTemp.NewRow();
                                        row[6] = strArray[num12].ToString();
                                        try
                                        {
                                            row[7] = strArray2[num12].ToString();
                                        }
                                        catch
                                        {
                                        }
                                        if (num12 < num9)
                                        {
                                            row[8] = strArray3[num12].ToString();
                                            try
                                            {
                                                row[9] = strArray4[num12].ToString();
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        if (num12 < num10)
                                        {
                                            row[10] = strArray5[num12].ToString();
                                        }
                                        row[12] = table2.Rows[0]["COLOR_FLAG"].ToString();
                                        this.dtTemp.Rows.Add(row);
                                    }
                                }
                                else if ((num9 > length) && (num9 > num10))
                                {
                                    num13 = num9 - num2;
                                    for (num14 = num13; num14 < num9; num14++)
                                    {
                                        row = this.dtTemp.NewRow();
                                        row[8] = strArray3[num14].ToString();
                                        try
                                        {
                                            row[9] = strArray4[num14].ToString();
                                        }
                                        catch
                                        {
                                        }
                                        if (num14 < length)
                                        {
                                            row[6] = strArray[num14].ToString();
                                            try
                                            {
                                                row[7] = strArray2[num14].ToString();
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        if (num14 < num10)
                                        {
                                            row[10] = strArray5[num14].ToString();
                                        }
                                        row[12] = table2.Rows[0]["COLOR_FLAG"].ToString();
                                        this.dtTemp.Rows.Add(row);
                                    }
                                }
                                else if ((length == num9) && (num9 >= num10))
                                {
                                    num15 = length - num2;
                                    for (num12 = 1; num12 < num15; num12++)
                                    {
                                        row = this.dtTemp.NewRow();
                                        if (num12 < num10)
                                        {
                                            row[10] = strArray5[num12].ToString();
                                        }
                                        row[6] = strArray[num12].ToString();
                                        try
                                        {
                                            row[7] = strArray2[num12].ToString();
                                        }
                                        catch
                                        {
                                        }
                                        row[8] = strArray3[num12].ToString();
                                        try
                                        {
                                            row[9] = strArray4[num12].ToString();
                                        }
                                        catch
                                        {
                                        }
                                        row[12] = table2.Rows[0]["COLOR_FLAG"].ToString();
                                        this.dtTemp.Rows.Add(row);
                                    }
                                }
                                else
                                {
                                    num15 = num10 - num2;
                                    num12 = num15;
                                    while (num12 < num10)
                                    {
                                        row = this.dtTemp.NewRow();
                                        row[10] = strArray5[num12].ToString();
                                        if (num12 < length)
                                        {
                                            row[6] = strArray[num12].ToString();
                                            try
                                            {
                                                row[7] = strArray2[num12].ToString();
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        if (num12 < num9)
                                        {
                                            row[8] = strArray3[num12].ToString();
                                            try
                                            {
                                                row[9] = strArray4[num12].ToString();
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        row[12] = table2.Rows[0]["COLOR_FLAG"].ToString();
                                        this.dtTemp.Rows.Add(row);
                                        num12++;
                                    }
                                }
                            }
                            if (num == 1)
                            {
                                num7 = 0;
                            }
                            for (int j = num7; j < table2.Rows.Count; j++)
                            {
                                row = this.dtTemp.NewRow();
                                row[0] = table2.Rows[j]["NURSING_DATE"].ToString();
                                row[1] = table2.Rows[j]["NURSING_TMP"].ToString();
                                row[2] = table2.Rows[j]["NURSING_PULSE"].ToString();
                                row[3] = table2.Rows[j]["NURSING_BREATH"].ToString();
                                row[4] = table2.Rows[j]["NURSING_SATURATION"].ToString();
                                row[5] = table2.Rows[j]["NURSING_BP"].ToString();
                                strArray = table2.Rows[j]["DRUG_NAME"].ToString().Replace(".\r\n", "|").Split(new char[] { '|' });
                                strArray2 = table2.Rows[j]["DRUG_AMOUNT"].ToString().Replace("\r\n", "|").Split(new char[] { '|' });
                                strArray3 = table2.Rows[j]["FOOD_NAME"].ToString().Replace(".\r\n", "|").Split(new char[] { '|' });
                                strArray4 = table2.Rows[j]["FOOD_AMOUNT"].ToString().Replace("\r\n", "|").Split(new char[] { '|' });
                                strArray5 = this.TextEditDesc1(table2.Rows[j]["NURSING_STATE_ILL"].ToString(), this.linenumber).Replace("\r\n", "⊙").Split(new char[] { '⊙' });
                                row[6] = strArray[0].ToString();
                                row[7] = strArray2[0].ToString();
                                row[8] = strArray3[0].ToString();
                                row[9] = strArray4[0].ToString();
                                row[10] = strArray5[0].ToString();
                                if (table2.Rows[j]["USER_NAME"].ToString() == ".")
                                {
                                    row[11] = "";
                                }
                                else
                                {
                                    row[11] = table2.Rows[j]["USER_NAME"].ToString();
                                }
                                row[12] = table2.Rows[j]["COLOR_FLAG"].ToString();
                                this.dtTemp.Rows.Add(row);
                                length = strArray.Length;
                                num9 = strArray3.Length;
                                num10 = strArray5.Length;
                                if ((length > num9) && (length > num10))
                                {
                                    num12 = 1;
                                    while (num12 < length)
                                    {
                                        row = this.dtTemp.NewRow();
                                        row[6] = strArray[num12].ToString();
                                        try
                                        {
                                            row[7] = strArray2[num12].ToString();
                                        }
                                        catch
                                        {
                                        }
                                        if (num12 < num9)
                                        {
                                            row[8] = strArray3[num12].ToString();
                                            try
                                            {
                                                row[9] = strArray4[num12].ToString();
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        if (num12 < num10)
                                        {
                                            row[10] = strArray5[num12].ToString();
                                        }
                                        row[12] = table2.Rows[j]["USER_NAME"].ToString();
                                        this.dtTemp.Rows.Add(row);
                                        num12++;
                                    }
                                }
                                else if ((num9 > length) && (num9 > num10))
                                {
                                    num12 = 1;
                                    while (num12 < num9)
                                    {
                                        row = this.dtTemp.NewRow();
                                        row[8] = strArray3[num12].ToString();
                                        try
                                        {
                                            row[9] = strArray4[num12].ToString();
                                        }
                                        catch
                                        {
                                        }
                                        if (num12 < length)
                                        {
                                            row[6] = strArray[num12].ToString();
                                            try
                                            {
                                                row[7] = strArray2[num12].ToString();
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        if (num12 < num10)
                                        {
                                            row[10] = strArray5[num12].ToString();
                                        }
                                        row[12] = table2.Rows[j]["USER_NAME"].ToString();
                                        this.dtTemp.Rows.Add(row);
                                        num12++;
                                    }
                                }
                                else if ((length == num9) && (num9 >= num10))
                                {
                                    num12 = 1;
                                    while (num12 < length)
                                    {
                                        row = this.dtTemp.NewRow();
                                        if (num12 < num10)
                                        {
                                            row[10] = strArray5[num12].ToString();
                                        }
                                        row[6] = strArray[num12].ToString();
                                        try
                                        {
                                            row[7] = strArray2[num12].ToString();
                                        }
                                        catch
                                        {
                                        }
                                        row[8] = strArray3[num12].ToString();
                                        try
                                        {
                                            row[9] = strArray4[num12].ToString();
                                        }
                                        catch
                                        {
                                        }
                                        row[12] = table2.Rows[j]["COLOR_FLAG"].ToString();
                                        this.dtTemp.Rows.Add(row);
                                        num12++;
                                    }
                                }
                                else
                                {
                                    num12 = 1;
                                    while (num12 < num10)
                                    {
                                        row = this.dtTemp.NewRow();
                                        row[10] = strArray5[num12].ToString();
                                        if (num12 < length)
                                        {
                                            row[6] = strArray[num12].ToString();
                                            try
                                            {
                                                row[7] = strArray2[num12].ToString();
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        if (num12 < num9)
                                        {
                                            row[8] = strArray3[num12].ToString();
                                            try
                                            {
                                                row[9] = strArray4[num12].ToString();
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        row[12] = table2.Rows[j]["COLOR_FLAG"].ToString();
                                        this.dtTemp.Rows.Add(row);
                                        num12++;
                                    }
                                }
                            }
                            int num17 = 0;
                            if (table3.Rows.Count > 0)
                            {
                                num17 = this.StringtoInt(table3.Rows[0]["PRINTTYPE"].ToString());
                                int num18 = this.StringtoInt(table3.Rows[0]["ROWS_NUM"].ToString());
                                if (num17 != num18)
                                {
                                    row = this.dtTemp.NewRow();
                                    row[0] = table2.Rows[0]["NURSING_DATE"].ToString();
                                    row[1] = table2.Rows[0]["NURSING_TMP"].ToString();
                                    row[2] = table2.Rows[0]["NURSING_PULSE"].ToString();
                                    row[3] = table2.Rows[0]["NURSING_BREATH"].ToString();
                                    row[4] = table2.Rows[0]["NURSING_SATURATION"].ToString();
                                    row[5] = table2.Rows[0]["NURSING_BP"].ToString();
                                    strArray = table3.Rows[0]["DRUG_NAME"].ToString().Replace(".\r\n", "|").Split(new char[] { '|' });
                                    strArray2 = table3.Rows[0]["DRUG_AMOUNT"].ToString().Replace("\r\n", "|").Split(new char[] { '|' });
                                    strArray3 = table3.Rows[0]["FOOD_NAME"].ToString().Replace(".\r\n", "|").Split(new char[] { '|' });
                                    strArray4 = table3.Rows[0]["FOOD_AMOUNT"].ToString().Replace("\r\n", "|").Split(new char[] { '|' });
                                    strArray5 = this.TextEditDesc1(table3.Rows[0]["NURSING_STATE_ILL"].ToString(), this.linenumber).Replace("\r\n", "⊙").Split(new char[] { '⊙' });
                                    row[6] = strArray[0].ToString();
                                    row[7] = strArray2[0].ToString();
                                    row[8] = strArray3[0].ToString();
                                    row[9] = strArray4[0].ToString();
                                    row[10] = strArray5[0].ToString();
                                    if (table2.Rows[0]["USER_NAME"].ToString() == "/n")
                                    {
                                        row[11] = "";
                                    }
                                    else
                                    {
                                        row[11] = table2.Rows[0]["USER_NAME"].ToString();
                                    }
                                    row[12] = table2.Rows[0]["COLOR_FLAG"].ToString();
                                    this.dtTemp.Rows.Add(row);
                                    length = strArray.Length;
                                    num9 = strArray3.Length;
                                    num10 = strArray5.Length;
                                    if ((length > num9) && (length > num10))
                                    {
                                        num11 = length - num17;
                                        for (num12 = 1; num12 < num11; num12++)
                                        {
                                            row = this.dtTemp.NewRow();
                                            row[6] = strArray[num12].ToString();
                                            try
                                            {
                                                row[7] = strArray2[num12].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            if (num12 < num9)
                                            {
                                                row[8] = strArray3[num12].ToString();
                                                try
                                                {
                                                    row[9] = strArray4[num12].ToString();
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            if (num12 < num10)
                                            {
                                                row[10] = strArray5[num12].ToString();
                                            }
                                            row[12] = table2.Rows[0]["COLOR_FLAG"].ToString();
                                            this.dtTemp.Rows.Add(row);
                                        }
                                    }
                                    else if ((num9 > length) && (num9 > num10))
                                    {
                                        num13 = num9 - num17;
                                        for (num14 = 1; num14 < num13; num14++)
                                        {
                                            row = this.dtTemp.NewRow();
                                            row[8] = strArray3[num14].ToString();
                                            try
                                            {
                                                row[9] = strArray4[num14].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            if (num14 < length)
                                            {
                                                row[6] = strArray[num14].ToString();
                                                try
                                                {
                                                    row[7] = strArray2[num14].ToString();
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            if (num14 < num10)
                                            {
                                                row[10] = strArray5[num14].ToString();
                                            }
                                            row[12] = table2.Rows[0]["COLOR_FLAG"].ToString();
                                            this.dtTemp.Rows.Add(row);
                                        }
                                    }
                                    else if ((length == num9) && (num9 >= num10))
                                    {
                                        num15 = length - num17;
                                        for (num12 = 1; num12 < num15; num12++)
                                        {
                                            row = this.dtTemp.NewRow();
                                            if (num12 < num10)
                                            {
                                                row[10] = strArray5[num12].ToString();
                                            }
                                            row[6] = strArray[num12].ToString();
                                            try
                                            {
                                                row[7] = strArray2[num12].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            row[8] = strArray3[num12].ToString();
                                            try
                                            {
                                                row[9] = strArray4[num12].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            row[12] = table2.Rows[0]["COLOR_FLAG"].ToString();
                                            this.dtTemp.Rows.Add(row);
                                        }
                                    }
                                    else
                                    {
                                        num15 = num10 - num17;
                                        for (num12 = 1; num12 < num15; num12++)
                                        {
                                            row = this.dtTemp.NewRow();
                                            row[10] = strArray5[num12].ToString();
                                            if (num12 < length)
                                            {
                                                row[6] = strArray[num12].ToString();
                                                try
                                                {
                                                    row[7] = strArray2[num12].ToString();
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            if (num12 < num9)
                                            {
                                                row[8] = strArray3[num12].ToString();
                                                try
                                                {
                                                    row[9] = strArray4[num12].ToString();
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            row[12] = table2.Rows[0]["COLOR_FLAG"].ToString();
                                            this.dtTemp.Rows.Add(row);
                                        }
                                    }
                                }
                            }
                            string str2 = GVars.IniFile.ReadString("LINENUMBER", "NURSING_RECORD_PRINT_MODEN", string.Empty);
                            string str3 = GVars.IniFile.ReadString("LINENUMBER", "NURSING_RECORD_PRINT_SAVETEMP", string.Empty);
                            string str4 = Guid.NewGuid().ToString();
                            string templetFilePath = str2;
                            string outputFilePath = str3 + DateTime.Now.ToString().Replace(" ", "").Replace("-", "").Replace(".", "").Replace(":", "").Replace("/", "").Replace(@"\", "") + "_WZ.xls";
                            PrintExcelHelper helper = new PrintExcelHelper(templetFilePath, outputFilePath);
                            Hashtable ht = new Hashtable();
                            ht.Add("0", "5$1|姓名:" + this.lblName.Text);
                            ht.Add("1", "5$3|性别:" + this.lblGender.Text);
                            ht.Add("2", "5$5|年龄:" + this.lblAge.Text);
                            ht.Add("3", "5$7|科别:" + this.lblDeptName.Text);
                            ht.Add("4", "5$9|床号: " + this.BedLabel);
                            ht.Add("5", "5$11|病案号:" + this.patientId);
                            helper.DataTableToExcel(this.dtTemp, 0x19, 8, 1, this.printrows, 0, "页", ht);
                            helper.InvokeExcelPrint(outputFilePath, 0);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Error.ErrProc(exception);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                base.Close();
            }
            catch (Exception exception)
            {
                Error.ErrProc(exception);
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.patientId.Length == 0)
                {
                    GVars.Msg.Show("无记录！");
                }
                else
                {
                    this.PatientGridView.DataSource = this.PatientData(this.patientId, this.visitId, this.dtRngStart.Value, this.dtRngEnd.Value).Tables[0].DefaultView;
                    this.showNursingRec(string.Empty);
                    this.toselect = true;
                }
            }
            catch (Exception exception)
            {
                Error.ErrProc(exception);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            Exception exception;
            this.rownum = 0;
            this.pagenum = 1;
            this.pagex = 0;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.datime = "1900-01-01 00:00:00";
                string sqlSel = ((((string.Empty + "SELECT MAX(NURSING_DATE)" + " FROM ") + "NURSING_RECORD " + "WHERE ") + "PATIENT_ID = " + SqlManager.SqlConvert(this.patientId)) + " AND VISIT_ID = " + SqlManager.SqlConvert(this.visitId)) + " AND ROWS_COUNT IS NOT NULL";
                DataTable table = GVars.OracleAccess.SelectData(sqlSel).Tables[0];
                if (table.Rows.Count > 0)
                {
                    this.datime = table.Rows[0][0].ToString();
                }
                if (this.datime == "")
                {
                    this.datime = "1900-01-01 00:00:00";
                }
                sqlSel = ((((string.Empty + "SELECT ROWS_COUNT,PAGE_INDEX,ROWS_PAGE_INDEX" + " FROM ") + "NURSING_RECORD " + "WHERE ") + "PATIENT_ID = " + SqlManager.SqlConvert(this.patientId)) + " AND VISIT_ID = " + SqlManager.SqlConvert(this.visitId)) + " AND NURSING_DATE = " + SqlManager.GetOraDbDate(this.datime);
                DataTable table2 = GVars.OracleAccess.SelectData(sqlSel).Tables[0];
                if ((table2.Rows.Count > 0) && (table2.Rows[0][0].ToString() != ""))
                {
                    this.rownum = int.Parse(table2.Rows[0][0].ToString());
                    this.pagenum = int.Parse(table2.Rows[0][1].ToString());
                    if (table2.Rows[0][2].ToString() != "")
                    {
                        this.pagex = int.Parse(table2.Rows[0][2].ToString());
                    }
                }
                sqlSel = (((((string.Empty + "SELECT *" + " FROM ") + "NURSING_RECORD " + "WHERE ") + "PATIENT_ID = " + SqlManager.SqlConvert(this.patientId)) + " AND VISIT_ID = " + SqlManager.SqlConvert(this.visitId)) + " AND NURSING_DATE > " + SqlManager.GetOraDbDate(this.datime)) + " ORDER BY " + " NURSING_DATE ASC";
                DataTable table3 = GVars.OracleAccess.SelectData(sqlSel).Tables[0];
                this.dtTemp = new DataTable();
                for (int i = 0; i < 13; i++)
                {
                    DataColumn column = new DataColumn {
                        DataType = System.Type.GetType("System.String"),
                        ColumnName = i.ToString()
                    };
                    this.dtTemp.Columns.Add(column);
                }
                this.linenumber = int.Parse(GVars.IniFile.ReadString("LINENUMBER", "NurseRecordWz", string.Empty));
                this.printrows = int.Parse(GVars.IniFile.ReadString("LINENUMBER", "NURSING_RECORD_PRINT", string.Empty));
                for (int j = 0; j < table3.Rows.Count; j++)
                {
                    int num6;
                    DataRow row = this.dtTemp.NewRow();
                    row[0] = table3.Rows[j]["NURSING_DATE"].ToString();
                    row[1] = table3.Rows[j]["NURSING_TMP"].ToString();
                    row[2] = table3.Rows[j]["NURSING_PULSE"].ToString();
                    row[3] = table3.Rows[j]["NURSING_BREATH"].ToString();
                    row[4] = table3.Rows[j]["NURSING_SATURATION"].ToString();
                    row[5] = table3.Rows[j]["NURSING_BP"].ToString();
                    string[] strArray = table3.Rows[j]["DRUG_NAME"].ToString().Replace(".\r\n", "|").Split(new char[] { '|' });
                    string[] strArray2 = table3.Rows[j]["DRUG_AMOUNT"].ToString().Replace("\r\n", "|").Split(new char[] { '|' });
                    string[] strArray3 = table3.Rows[j]["FOOD_NAME"].ToString().Replace(".\r\n", "|").Split(new char[] { '|' });
                    string[] strArray4 = table3.Rows[j]["FOOD_AMOUNT"].ToString().Replace("\r\n", "|").Split(new char[] { '|' });
                    string[] strArray5 = this.TextEditDesc1(table3.Rows[j]["NURSING_STATE_ILL"].ToString(), this.linenumber).Replace("\r\n", "⊙").Split(new char[] { '⊙' });
                    row[6] = strArray[0].ToString();
                    row[7] = strArray2[0].ToString();
                    row[8] = strArray3[0].ToString();
                    row[9] = strArray4[0].ToString();
                    row[10] = strArray5[0].ToString();
                    if (table3.Rows[j]["USER_NAME"].ToString() == ".")
                    {
                        row[11] = "";
                    }
                    else
                    {
                        row[11] = table3.Rows[j]["USER_NAME"].ToString();
                    }
                    row[12] = table3.Rows[j]["COLOR_FLAG"].ToString();
                    this.dtTemp.Rows.Add(row);
                    int length = strArray.Length;
                    int num4 = strArray3.Length;
                    int num5 = strArray5.Length;
                    if ((length > num4) && (length > num5))
                    {
                        num6 = 1;
                        while (num6 < length)
                        {
                            row = this.dtTemp.NewRow();
                            row[6] = strArray[num6].ToString();
                            try
                            {
                                row[7] = strArray2[num6].ToString();
                            }
                            catch
                            {
                            }
                            if (num6 < num4)
                            {
                                row[8] = strArray3[num6].ToString();
                                try
                                {
                                    row[9] = strArray4[num6].ToString();
                                }
                                catch
                                {
                                }
                            }
                            if (num6 < num5)
                            {
                                row[10] = strArray5[num6].ToString();
                            }
                            row[12] = table3.Rows[j]["COLOR_FLAG"].ToString();
                            this.dtTemp.Rows.Add(row);
                            num6++;
                        }
                    }
                    else if ((num4 > length) && (num4 > num5))
                    {
                        num6 = 1;
                        while (num6 < num4)
                        {
                            row = this.dtTemp.NewRow();
                            row[8] = strArray3[num6].ToString();
                            try
                            {
                                row[9] = strArray4[num6].ToString();
                            }
                            catch
                            {
                            }
                            if (num6 < length)
                            {
                                row[6] = strArray[num6].ToString();
                                try
                                {
                                    row[7] = strArray2[num6].ToString();
                                }
                                catch
                                {
                                }
                            }
                            if (num6 < num5)
                            {
                                row[10] = strArray5[num6].ToString();
                            }
                            row[12] = table3.Rows[j]["COLOR_FLAG"].ToString();
                            this.dtTemp.Rows.Add(row);
                            num6++;
                        }
                    }
                    else if ((length == num4) && (num4 >= num5))
                    {
                        num6 = 1;
                        while (num6 < length)
                        {
                            row = this.dtTemp.NewRow();
                            if (num6 < num5)
                            {
                                row[10] = strArray5[num6].ToString();
                            }
                            row[6] = strArray[num6].ToString();
                            row[7] = strArray2[num6].ToString();
                            row[8] = strArray3[num6].ToString();
                            row[9] = strArray4[num6].ToString();
                            row[12] = table3.Rows[j]["COLOR_FLAG"].ToString();
                            this.dtTemp.Rows.Add(row);
                            num6++;
                        }
                    }
                    else
                    {
                        for (num6 = 1; num6 < num5; num6++)
                        {
                            row = this.dtTemp.NewRow();
                            row[10] = strArray5[num6].ToString();
                            if (num6 < length)
                            {
                                row[6] = strArray[num6].ToString();
                                try
                                {
                                    row[7] = strArray2[num6].ToString();
                                }
                                catch
                                {
                                }
                            }
                            if (num6 < num4)
                            {
                                row[8] = strArray3[num6].ToString();
                                try
                                {
                                    row[9] = strArray4[num6].ToString();
                                }
                                catch
                                {
                                }
                            }
                            row[12] = table3.Rows[j]["COLOR_FLAG"].ToString();
                            this.dtTemp.Rows.Add(row);
                        }
                    }
                }
                string str2 = GVars.IniFile.ReadString("LINENUMBER", "NURSING_RECORD_PRINT_MODEN", string.Empty);
                string str3 = GVars.IniFile.ReadString("LINENUMBER", "NURSING_RECORD_PRINT_SAVETEMP", string.Empty);
                string str4 = Guid.NewGuid().ToString();
                string templetFilePath = str2;
                string outputFilePath = str3 + DateTime.Now.ToString().Replace(" ", "").Replace("-", "").Replace(".", "").Replace(":", "").Replace("/", "").Replace(@"\", "") + "_YB.xls";
                PrintExcelHelper helper = new PrintExcelHelper(templetFilePath, outputFilePath);
                Hashtable ht = new Hashtable();
                ht.Add("0", "5$1|姓名:" + this.lblName.Text);
                ht.Add("1", "5$3|性别:" + this.lblGender.Text);
                ht.Add("2", "5$5|年龄:" + this.lblAge.Text);
                ht.Add("3", "5$7|科别:" + this.lblDeptName.Text);
                ht.Add("4", "5$9|床号: " + this.BedLabel);
                ht.Add("5", "5$11|病案号:" + this.patientId);
                if (this.dtTemp.Rows.Count > 0)
                {
                    helper.DataTableToExcel(this.dtTemp, 0x19, 8, 1, this.printrows, this.rownum, "页", ht);
                    helper.InvokeExcelPrint(outputFilePath, this.rownum);
                    try
                    {
                        if (MessageBox.Show("确定打印是否成功？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            if ((this.pagenum == 1) && (this.rownum == 0))
                            {
                                this.PrintUpdata2();
                            }
                            else
                            {
                                this.PrintUpdata3();
                            }
                        }
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                Error.ErrProc(exception);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtBedLabel.Text.Trim().Length != 0)
            {
                try
                {
                    string str = ".";
                    try
                    {
                        str = this.PatientGridView["Column19", this.PatientGridView.CurrentCell.RowIndex].Value.ToString();
                    }
                    catch
                    {
                    }
                    bool flag = (this._userRight.IndexOf("2") >= 0) || GVars.User.Name.Equals(str);
                    if (((str == ".") || (str == GVars.User.Name)) || flag)
                    {
                        if (MessageBox.Show("确定要删除该记录吗？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            string sql = "DELETE FROM NURSING_RECORD WHERE ";
                            sql = ((sql + "PATIENT_ID = '" + this.patientId + "'") + "AND VISIT_ID = '" + this.visitId + "'") + "AND NURSING_DATE = " + SqlManager.GetOraDbDate(this.PatientGridView["Column15", this.PatientGridView.CurrentCell.RowIndex].Value.ToString());
                            GVars.OracleAccess.ExecuteNoQuery(sql);
                            if (this.toselect)
                            {
                                this.PatientGridView.DataSource = this.PatientData(this.patientId, this.visitId, this.dtRngStart.Value, this.dtRngEnd.Value).Tables[0].DefaultView;
                            }
                            if (!this.toselect)
                            {
                                this.PatientGridView.DataSource = this.PatientData(this.patientId, this.visitId).Tables[0].DefaultView;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("您没有删除该记录的权限！");
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void btUpData_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.InfoSave();
            if (this.toselect)
            {
                this.PatientGridView.DataSource = this.PatientData(this.patientId, this.visitId, this.dtRngStart.Value, this.dtRngEnd.Value).Tables[0].DefaultView;
            }
            if (!this.toselect)
            {
                this.PatientGridView.DataSource = this.PatientData(this.patientId, this.visitId).Tables[0].DefaultView;
            }
            this.Cursor = Cursors.Default;
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定要清除打印续打标记吗？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    string sql = "UPDATE NURSING_RECORD ";
                    sql = (((((sql + "SET ") + "ROWS_COUNT = NULL" + ",ROWS_PAGE_INDEX = NULL") + ",PAGE_INDEX = NULL" + ",ROWS_NUM = NULL") + ",PRINTTYPE = NULL" + " WHERE ") + "PATIENT_ID = '" + this.patientId + "'") + " AND VISIT_ID = '" + this.visitId + "'";
                    GVars.OracleAccess.ExecuteNoQuery(sql);
                    MessageBox.Show("操作完成！");
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定要清除打印续打标记吗？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    string sql = "UPDATE NURSING_RECORD ";
                    sql = (((((sql + "SET ") + "ROWS_COUNT = NULL" + ",ROWS_PAGE_INDEX = NULL") + ",PAGE_INDEX = NULL" + ",ROWS_NUM = NULL") + ",PRINTTYPE = NULL" + " WHERE ") + "PATIENT_ID = '" + this.patientId + "'") + " AND VISIT_ID = '" + this.visitId + "'";
                    GVars.OracleAccess.ExecuteNoQuery(sql);
                    MessageBox.Show("操作完成！");
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Graphics graphics;
            SizeF ef;
            int width;
            string str = "";
            string[] strArray = this.PatientGridView["Column8", this.PatientGridView.CurrentCell.RowIndex].Value.ToString().Replace("\r\n", "#").Split(new char[] { '#' });
            int num = 0;
            int length = strArray.Length;
            for (int i = 0; i < length; i++)
            {
                graphics = base.CreateGraphics();
                ef = graphics.MeasureString(strArray[i], this.Font);
                graphics.Dispose();
                width = (int) ef.Width;
                if (width > num)
                {
                    num = width;
                }
            }
            for (int j = 0; j < length; j++)
            {
                graphics = base.CreateGraphics();
                ef = graphics.MeasureString(strArray[j], this.Font);
                graphics.Dispose();
                width = (int) ef.Width;
                int num6 = 0;
                string str3 = "";
                if (width <= num)
                {
                    num6 = num - width;
                    if (j == 0)
                    {
                        str3 = "╗.\r\n";
                    }
                    if ((j > 0) && (j < (length - 1)))
                    {
                        str3 = "╢.\r\n";
                    }
                    if (j == (length - 1))
                    {
                        str3 = "╝.";
                    }
                }
                str = str + this.sss(strArray[j], num6) + str3;
            }
            string sql = "UPDATE NURSING_RECORD ";
            sql = (((((sql + "SET ") + "DRUG_NAME = '" + str + "'") + " WHERE ") + "PATIENT_ID = '" + this.patientId + "'") + " AND VISIT_ID = '" + this.visitId + "'") + " AND NURSING_DATE = " + SqlManager.GetOraDbDate(this.PatientGridView["Column15", this.PatientGridView.CurrentCell.RowIndex].Value.ToString());
            GVars.OracleAccess.ExecuteNoQuery(sql);
            this.PatientGridView["Column8", this.PatientGridView.CurrentCell.RowIndex].Value = str;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private string getDatainfo(string itemname, string timestr)
        {
            DataTable table = new DataTable();
            string str = "";
            try
            {
                string sqlSel = ((((((string.Empty + "SELECT VITAL_SIGNS_CVALUES,VITAL_SIGNS" + " FROM ") + "vital_signs_rec " + "WHERE ") + "PATIENT_ID = " + SqlManager.SqlConvert(this.patientId)) + "AND VISIT_ID = " + SqlManager.SqlConvert(this.visitId)) + "AND time_point = " + SqlManager.GetOraDbDate(timestr)) + "AND vital_code in (" + itemname + ")") + "ORDER BY " + " time_point ASC";
                table = GVars.OracleAccess.SelectData(sqlSel).Tables[0];
                string str3 = null;
                string str4 = null;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    str3 = str3 + table.Rows[i]["VITAL_SIGNS"].ToString() + ".\r\n";
                    str4 = str4 + table.Rows[i]["VITAL_SIGNS_CVALUES"].ToString() + "\r\n";
                }
                str = str3.Remove(str3.Length - 2, 2) + "⊙" + str4.Remove(str4.Length - 2, 2);
            }
            catch
            {
            }
            return str;
        }

        private void getLines()
        {
            if (this.textBox1.Text.Trim().Length == 0)
            {
                this._lines = 1;
                this.ArrDesc1 = new string[this._lines];
            }
            else
            {
                this._lines = Win32API.SendMessageA(this.textBox1.Handle.ToInt32(), Win32API.EM_GETLINECOUNT, 0, 0);
                this.ArrDesc1 = new string[this._lines];
                int num = 0;
                int startIndex = Win32API.SendMessageA(this.textBox1.Handle.ToInt32(), Win32API.EM_LINEINDEX, 0, 0);
                int wParam = 0;
                wParam = 1;
                while (wParam < this._lines)
                {
                    num = Win32API.SendMessageA(this.textBox1.Handle.ToInt32(), Win32API.EM_LINEINDEX, wParam, 0);
                    this.ArrDesc1[wParam - 1] = this.textBox1.Text.Substring(startIndex, num - startIndex);
                    startIndex = num;
                    wParam++;
                }
                this.ArrDesc1[wParam - 1] = this.textBox1.Text.Substring(startIndex, this.textBox1.Text.Length - startIndex);
            }
        }

        private DataTable GetNursingTame(string patientId, string visitId)
        {
            string sqlSel = (((string.Empty + "SELECT max(NURSING_DATE) " + " FROM ") + "NURSING_RECORD " + "WHERE ") + "PATIENT_ID = " + SqlManager.SqlConvert(patientId)) + "AND VISIT_ID = " + SqlManager.SqlConvert(visitId);
            return GVars.OracleAccess.SelectData(sqlSel).Tables[0];
        }

        private DataTable GetSignsTame()
        {
            string sqlSel = (((string.Empty + "SELECT distinct(time_point)" + " FROM ") + "vital_signs_rec " + "WHERE ") + "PATIENT_ID = " + SqlManager.SqlConvert(this.patientId)) + "AND VISIT_ID = " + SqlManager.SqlConvert(this.visitId);
            if ((this.GetNursingTame(this.patientId, this.visitId).Rows.Count > 0) && (this.GetNursingTame(this.patientId, this.visitId).Rows[0][0].ToString() != ""))
            {
                sqlSel = sqlSel + "AND time_point > " + SqlManager.GetOraDbDate(this.GetNursingTame(this.patientId, this.visitId).Rows[0][0].ToString());
            }
            sqlSel = sqlSel + "ORDER BY " + " time_point ASC";
            return GVars.OracleAccess.SelectData(sqlSel).Tables[0];
        }

        private void grdTitle_EnterCell(object sender, EventArgs e)
        {
        }

        private string HZdata(DataTable dt)
        {
            int num = 0;
            int num2 = 0;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] strArray = dt.Rows[i][0].ToString().Replace("\r\n", "#").Split(new char[] { '#' });
                    if (strArray.Length > 0)
                    {
                        for (int j = 0; j < strArray.Length; j++)
                        {
                            if ((((strArray[j].ToString() != "") && (strArray[j].ToString() != " ")) && (strArray[j].ToString() != "")) && (strArray[j].ToString() != null))
                            {
                                num += int.Parse(strArray[j].ToString());
                            }
                        }
                    }
                    string[] strArray2 = dt.Rows[i][1].ToString().Replace("\r\n", "#").Split(new char[] { '#' });
                    if (strArray2.Length > 0)
                    {
                        for (int k = 0; k < strArray2.Length; k++)
                        {
                            if ((((strArray2[k].ToString() != "") && (strArray2[k].ToString() != " ")) && (strArray2[k].ToString() != "")) && (strArray2[k].ToString() != null))
                            {
                                num2 += int.Parse(strArray2[k].ToString());
                            }
                        }
                    }
                }
            }
            return (num.ToString() + "*" + num2.ToString());
        }

        private void imeCtrl_GotFocus(object sender, EventArgs e)
        {
            IME.ChangeControlIme(base.ActiveControl.Handle);
        }

        private void InfoSave()
        {
            try
            {
                DataTable signsTame = this.GetSignsTame();
                if (signsTame.Rows.Count > 0)
                {
                    string itemname = GVars.IniFile.ReadString("NURSING_RECORD_CR", "RL", string.Empty).Trim();
                    string str2 = GVars.IniFile.ReadString("NURSING_RECORD_CR", "CL", string.Empty).Trim();
                    for (int i = 0; i < signsTame.Rows.Count; i++)
                    {
                        string str6;
                        DataTable table2 = this.InfoSigns((DateTime) signsTame.Rows[i][0]);
                        string date = string.Empty;
                        for (int j = 0; j < table2.Rows.Count; j++)
                        {
                            string key = table2.Rows[j]["vital_code"].ToString();
                            string str5 = GVars.IniFile.ReadString("NURSING_RECORD", key, string.Empty).Trim();
                            if (((str5 != "") && (str5 != null)) && (str5 != string.Empty))
                            {
                                if (date != table2.Rows[j]["TIME_POINT"].ToString())
                                {
                                    str6 = "insert into NURSING_RECORD (DEPT_CODE,PATIENT_ID,VISIT_ID,NURSING_DATE,";
                                    str6 = ((((((((str6 + str5 + ",") + "DB_USER,USER_NAME)" + "values( ") + "'" + GVars.User.DeptCode + "'") + ",'" + this.patientId + "'") + ",'" + this.visitId + "'") + "," + SqlManager.GetOraDbDate(table2.Rows[j]["TIME_POINT"].ToString())) + ",'" + table2.Rows[j]["VITAL_SIGNS_CVALUES"].ToString() + " '") + ",'.'") + ",'.'" + ")";
                                    GVars.OracleAccess.ExecuteNoQuery(str6);
                                    date = table2.Rows[j]["TIME_POINT"].ToString();
                                }
                                else
                                {
                                    str6 = "UPDATE NURSING_RECORD ";
                                    str6 = ((((((str6 + "SET ") + str5 + " = '") + table2.Rows[j]["VITAL_SIGNS_CVALUES"].ToString() + "'") + " WHERE ") + "PATIENT_ID = '" + this.patientId + "'") + " AND VISIT_ID = '" + this.visitId + "'") + " AND NURSING_DATE = " + SqlManager.GetOraDbDate(date);
                                    GVars.OracleAccess.ExecuteNoQuery(str6);
                                }
                            }
                        }
                        for (int k = 0; k < table2.Rows.Count; k++)
                        {
                            string[] strArray;
                            string str7 = null;
                            string str8 = null;
                            string str9 = null;
                            string str10 = null;
                            string sql = null;
                            string str12 = this.getDatainfo(itemname, table2.Rows[k]["TIME_POINT"].ToString());
                            if (str12.Length > 0)
                            {
                                strArray = str12.Split(new char[] { '⊙' });
                                str7 = strArray[0].ToString();
                                str8 = strArray[1].ToString();
                            }
                            if (this.getDatainfo(str2, table2.Rows[k]["TIME_POINT"].ToString()).Length > 0)
                            {
                                strArray = this.getDatainfo(str2, table2.Rows[k]["TIME_POINT"].ToString()).Split(new char[] { '⊙' });
                                str9 = strArray[0].ToString();
                                str10 = strArray[1].ToString();
                            }
                            string sqlSel = "select count(*) from NURSING_RECORD ";
                            sqlSel = (((sqlSel + " WHERE ") + "PATIENT_ID = '" + this.patientId + "'") + " AND VISIT_ID = '" + this.visitId + "'") + " AND NURSING_DATE = " + SqlManager.GetOraDbDate(table2.Rows[k]["TIME_POINT"].ToString());
                            if (int.Parse(GVars.OracleAccess.SelectData(sqlSel).Tables[0].Rows[0][0].ToString()) == 0)
                            {
                                sql = "insert into NURSING_RECORD (DEPT_CODE,PATIENT_ID,VISIT_ID,NURSING_DATE,";
                                sql = (((((((((((sql + "DRUG_NAME,DRUG_AMOUNT,FOOD_NAME,FOOD_AMOUNT,") + "DB_USER,USER_NAME)" + "values( ") + "'" + GVars.User.DeptCode + "'") + ",'" + this.patientId + "'") + ",'" + this.visitId + "'") + "," + SqlManager.GetOraDbDate(table2.Rows[k]["TIME_POINT"].ToString())) + ",'" + str7 + " '") + ",'" + str8 + " '") + ",'" + str9 + " '") + ",'" + str10 + " '") + ",'.'") + ",'.'" + ")";
                                GVars.OracleAccess.ExecuteNoQuery(sql);
                                date = table2.Rows[k]["TIME_POINT"].ToString();
                            }
                            else
                            {
                                str6 = "UPDATE NURSING_RECORD ";
                                str6 = (((((((((((str6 + "SET " + "DRUG_NAME = '") + str7 + "',") + "DRUG_AMOUNT = '") + str8 + "',") + "FOOD_NAME = '") + str9 + "',") + "FOOD_AMOUNT = '") + str10 + "'") + " WHERE ") + "PATIENT_ID = '" + this.patientId + "'") + " AND VISIT_ID = '" + this.visitId + "'") + " AND NURSING_DATE = " + SqlManager.GetOraDbDate(date);
                                GVars.OracleAccess.ExecuteNoQuery(str6);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private DataTable InfoSigns(DateTime dtm)
        {
            DataTable table = new DataTable();
            try
            {
                string sqlSel = (((((string.Empty + "SELECT VITAL_CODE,time_point,VITAL_SIGNS_CVALUES,VITAL_SIGNS" + " FROM ") + "vital_signs_rec " + "WHERE ") + "PATIENT_ID = " + SqlManager.SqlConvert(this.patientId)) + "AND VISIT_ID = " + SqlManager.SqlConvert(this.visitId)) + "AND time_point = " + SqlManager.GetOraDbDate(dtm)) + "ORDER BY " + " time_point ASC";
                table = GVars.OracleAccess.SelectData(sqlSel).Tables[0];
            }
            catch
            {
                GVars.Msg.Show(this.GetSignsTame().Rows[0][0].ToString() + "至现在时间，该病员无护理记录");
            }
            return table;
        }

        private void initDisp()
        {
            this.lblName.Text = string.Empty;
            this.lblGender.Text = string.Empty;
            this.lblAge.Text = string.Empty;
            this.lblDeptName.Text = string.Empty;
            this.lblDocNo.Text = string.Empty;
            this.lblDiagnose.Text = string.Empty;
        }

        private void initFrmVal()
        {
            this.patientCom = new PatientDbI(GVars.OracleAccess);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(NurseRecordWz_P));
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            DataGridViewCellStyle style5 = new DataGridViewCellStyle();
            DataGridViewCellStyle style6 = new DataGridViewCellStyle();
            DataGridViewCellStyle style7 = new DataGridViewCellStyle();
            DataGridViewCellStyle style8 = new DataGridViewCellStyle();
            DataGridViewCellStyle style9 = new DataGridViewCellStyle();
            this.btUpData = new Button();
            this.button22 = new Button();
            this.button12 = new Button();
            this.textBox3 = new TextBox();
            this.dtRngEnd = new DateTimePicker();
            this.btnReprint = new Button();
            this.dtRngStart = new DateTimePicker();
            this.label9 = new Label();
            this.label10 = new Label();
            this.btnSave = new Button();
            this.btnQuery = new Button();
            this.btnExit = new Button();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.ToolStripRed = new ToolStripMenuItem();
            this.ToolStripNoRed = new ToolStripMenuItem();
            this.ToolStripTime = new ToolStripMenuItem();
            this.ToolStripXJTJ = new ToolStripMenuItem();
            this.ToolStripZJTJ = new ToolStripMenuItem();
            this.btnContinuePrint = new Button();
            this.groupBox1 = new GroupBox();
            this.lblDiagnose = new Label();
            this.label2 = new Label();
            this.lblDocNo = new Label();
            this.label7 = new Label();
            this.lblDeptName = new Label();
            this.label5 = new Label();
            this.lblAge = new Label();
            this.label4 = new Label();
            this.lblGender = new Label();
            this.label3 = new Label();
            this.lblName = new Label();
            this.label6 = new Label();
            this.txtBedLabel = new TextBox();
            this.label1 = new Label();
            this.panel1 = new Panel();
            this.groupBox2 = new GroupBox();
            this.button23 = new Button();
            this.button7 = new Button();
            this.button11 = new Button();
            this.button9 = new Button();
            this.PatientGridView = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.Column8 = new DataGridViewTextBoxColumn();
            this.Column9 = new DataGridViewTextBoxColumn();
            this.Column10 = new DataGridViewTextBoxColumn();
            this.Column11 = new DataGridViewTextBoxColumn();
            this.Column12 = new DataGridViewTextBoxColumn();
            this.Column13 = new DataGridViewTextBoxColumn();
            this.Column14 = new DataGridViewTextBoxColumn();
            this.Column15 = new DataGridViewTextBoxColumn();
            this.Column16 = new DataGridViewButtonColumn();
            this.Column17 = new DataGridViewTextBoxColumn();
            this.Column18 = new DataGridViewTextBoxColumn();
            this.Column19 = new DataGridViewTextBoxColumn();
            this.button10 = new Button();
            this.button21 = new Button();
            this.button20 = new Button();
            this.button19 = new Button();
            this.button18 = new Button();
            this.button17 = new Button();
            this.button15 = new Button();
            this.button16 = new Button();
            this.button13 = new Button();
            this.button14 = new Button();
            this.button8 = new Button();
            this.textBox1 = new TextBox();
            this.button6 = new Button();
            this.button5 = new Button();
            this.button4 = new Button();
            this.button3 = new Button();
            this.button2 = new Button();
            this.button1 = new Button();
            this.textBox2 = new TextBox();
            this.button24 = new Button();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((ISupportInitialize) this.PatientGridView).BeginInit();
            base.SuspendLayout();
            this.btUpData.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btUpData.Location = new Point(0x2b9, 0x1d0);
            this.btUpData.Name = "btUpData";
            this.btUpData.Size = new Size(0x3d, 0x17);
            this.btUpData.TabIndex = 0x25;
            this.btUpData.Text = "导入记录";
            this.btUpData.UseVisualStyleBackColor = true;
            this.btUpData.Click += new EventHandler(this.btUpData_Click);
            this.button22.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.button22.Location = new Point(0x12d, 0x1d0);
            this.button22.Name = "button22";
            this.button22.Size = new Size(0x30, 0x17);
            this.button22.TabIndex = 40;
            this.button22.Text = "回溯";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Visible = false;
            this.button12.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.button12.Location = new Point(350, 0x1d0);
            this.button12.Name = "button12";
            this.button12.Size = new Size(0x41, 0x17);
            this.button12.TabIndex = 0x27;
            this.button12.Text = "清空标记";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new EventHandler(this.button12_Click_1);
            this.textBox3.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.textBox3.Location = new Point(580, 0x1d2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(0x27, 0x15);
            this.textBox3.TabIndex = 0x26;
            this.dtRngEnd.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.dtRngEnd.Format = DateTimePickerFormat.Short;
            this.dtRngEnd.Location = new Point(0x95, 0x1d2);
            this.dtRngEnd.Name = "dtRngEnd";
            this.dtRngEnd.Size = new Size(0x54, 0x15);
            this.dtRngEnd.TabIndex = 0x24;
            this.btnReprint.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnReprint.Location = new Point(0x1f2, 0x1d0);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new Size(0x4b, 0x17);
            this.btnReprint.TabIndex = 30;
            this.btnReprint.Text = "打印/续打";
            this.btnReprint.UseCompatibleTextRendering = true;
            this.btnReprint.UseVisualStyleBackColor = true;
            this.btnReprint.Click += new EventHandler(this.btnReprint_Click);
            this.dtRngStart.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.dtRngStart.Format = DateTimePickerFormat.Short;
            this.dtRngStart.Location = new Point(0x34, 0x1d2);
            this.dtRngStart.Name = "dtRngStart";
            this.dtRngStart.Size = new Size(0x54, 0x15);
            this.dtRngStart.TabIndex = 0x23;
            this.label9.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x8a, 0x1db);
            this.label9.Name = "label9";
            this.label9.Size = new Size(11, 12);
            this.label9.TabIndex = 0x22;
            this.label9.Text = "-";
            this.label10.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x18, 0x1d4);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x1d, 12);
            this.label10.TabIndex = 0x21;
            this.label10.Text = "日期";
            this.btnSave.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnSave.Location = new Point(0x2f7, 0x1d0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(0x3a, 0x17);
            this.btnSave.TabIndex = 0x1f;
            this.btnSave.Text = "删除(&D)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            this.btnQuery.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnQuery.Location = new Point(0xef, 0x1d0);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new Size(0x38, 0x17);
            this.btnQuery.TabIndex = 0x20;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.btnExit.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnExit.Location = new Point(0x2f7, 0x1d0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(0x3a, 0x17);
            this.btnExit.TabIndex = 0x1c;
            this.btnExit.Text = "退出(&X)";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Visible = false;
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.ToolStripRed, this.ToolStripNoRed, this.ToolStripTime, this.ToolStripXJTJ, this.ToolStripZJTJ });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0xb7, 0x72);
            this.ToolStripRed.Name = "ToolStripRed";
            this.ToolStripRed.Size = new Size(0xb6, 0x16);
            this.ToolStripRed.Text = "标红";
            this.ToolStripRed.Click += new EventHandler(this.ToolStripRed_Click);
            this.ToolStripNoRed.Name = "ToolStripNoRed";
            this.ToolStripNoRed.Size = new Size(0xb6, 0x16);
            this.ToolStripNoRed.Text = "撤销标红";
            this.ToolStripNoRed.Click += new EventHandler(this.ToolStripNoRed_Click);
            this.ToolStripTime.Name = "ToolStripTime";
            this.ToolStripTime.Size = new Size(0xb6, 0x16);
            this.ToolStripTime.Text = "插入时间点";
            this.ToolStripTime.Click += new EventHandler(this.ToolStripTime_Click_1);
            this.ToolStripXJTJ.Name = "ToolStripXJTJ";
            this.ToolStripXJTJ.Size = new Size(0xb6, 0x16);
            this.ToolStripXJTJ.Text = "当班日间小结统计";
            this.ToolStripXJTJ.Click += new EventHandler(this.ToolStripXJTJ_Click);
            this.ToolStripZJTJ.Name = "ToolStripZJTJ";
            this.ToolStripZJTJ.Size = new Size(0xb6, 0x16);
            this.ToolStripZJTJ.Text = "当班24小时总结统计";
            this.ToolStripZJTJ.Click += new EventHandler(this.ToolStripZJTJ_Click);
            this.btnContinuePrint.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnContinuePrint.Location = new Point(0x26a, 0x1d0);
            this.btnContinuePrint.Name = "btnContinuePrint";
            this.btnContinuePrint.Size = new Size(0x3f, 0x17);
            this.btnContinuePrint.TabIndex = 0x1d;
            this.btnContinuePrint.Text = "择页补打";
            this.btnContinuePrint.UseVisualStyleBackColor = true;
            this.btnContinuePrint.Click += new EventHandler(this.btnContinuePrint_Click);
            this.groupBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.lblDiagnose);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblDocNo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblDeptName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblAge);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblGender);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtBedLabel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(14, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x330, 0x2b);
            this.groupBox1.TabIndex = 0x1a;
            this.groupBox1.TabStop = false;
            this.lblDiagnose.AutoSize = true;
            this.lblDiagnose.ForeColor = Color.Blue;
            this.lblDiagnose.Location = new Point(0x270, 0x11);
            this.lblDiagnose.Name = "lblDiagnose";
            this.lblDiagnose.Size = new Size(0x35, 12);
            this.lblDiagnose.TabIndex = 0x12;
            this.lblDiagnose.Text = "张三李四";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x24d, 0x11);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 0x11;
            this.label2.Text = "诊断";
            this.lblDocNo.AutoSize = true;
            this.lblDocNo.ForeColor = Color.Blue;
            this.lblDocNo.Location = new Point(0x20d, 0x11);
            this.lblDocNo.Name = "lblDocNo";
            this.lblDocNo.Size = new Size(0x3b, 12);
            this.lblDocNo.TabIndex = 0x10;
            this.lblDocNo.Text = "JX0000001";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x1de, 0x11);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x29, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "病案号";
            this.lblDeptName.AutoSize = true;
            this.lblDeptName.ForeColor = Color.Blue;
            this.lblDeptName.Location = new Point(0x195, 0x11);
            this.lblDeptName.Name = "lblDeptName";
            this.lblDeptName.Size = new Size(0x35, 12);
            this.lblDeptName.TabIndex = 14;
            this.lblDeptName.Text = "消化内科";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(370, 0x11);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "科别";
            this.lblAge.AutoSize = true;
            this.lblAge.ForeColor = Color.Blue;
            this.lblAge.Location = new Point(0x137, 0x11);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new Size(0x2f, 12);
            this.lblAge.TabIndex = 12;
            this.lblAge.Text = "12岁5月";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x114, 0x11);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "年龄";
            this.lblGender.AutoSize = true;
            this.lblGender.ForeColor = Color.Blue;
            this.lblGender.Location = new Point(0xf3, 0x11);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new Size(0x11, 12);
            this.lblGender.TabIndex = 10;
            this.lblGender.Text = "男";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0xd0, 0x11);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "性别";
            this.lblName.AutoSize = true;
            this.lblName.ForeColor = Color.Blue;
            this.lblName.Location = new Point(0x95, 0x11);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(0x35, 12);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "张三李四";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x72, 0x11);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "姓名";
            this.txtBedLabel.Location = new Point(0x29, 14);
            this.txtBedLabel.Name = "txtBedLabel";
            this.txtBedLabel.Size = new Size(0x36, 0x15);
            this.txtBedLabel.TabIndex = 2;
            this.txtBedLabel.KeyDown += new KeyEventHandler(this.txtBedLabel_KeyDown);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 0x11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "床标";
            this.panel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Location = new Point(5, 0x2f);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x342, 0x19b);
            this.panel1.TabIndex = 0x29;
            this.groupBox2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.button23);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.button11);
            this.groupBox2.Controls.Add(this.button9);
            this.groupBox2.Controls.Add(this.PatientGridView);
            this.groupBox2.Controls.Add(this.button10);
            this.groupBox2.Controls.Add(this.button21);
            this.groupBox2.Controls.Add(this.button20);
            this.groupBox2.Controls.Add(this.button19);
            this.groupBox2.Controls.Add(this.button18);
            this.groupBox2.Controls.Add(this.button17);
            this.groupBox2.Controls.Add(this.button15);
            this.groupBox2.Controls.Add(this.button16);
            this.groupBox2.Controls.Add(this.button13);
            this.groupBox2.Controls.Add(this.button14);
            this.groupBox2.Controls.Add(this.button8);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Location = new Point(8, -2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x331, 0x191);
            this.groupBox2.TabIndex = 0x1c;
            this.groupBox2.TabStop = false;
            this.button23.BackgroundImage = (Image) manager.GetObject("button23.BackgroundImage");
            this.button23.BackgroundImageLayout = ImageLayout.Stretch;
            this.button23.FlatStyle = FlatStyle.Popup;
            this.button23.Location = new Point(0x2e6, 11);
            this.button23.Name = "button23";
            this.button23.Size = new Size(0x33, 0x35);
            this.button23.TabIndex = 0x18;
            this.button23.Text = "签名";
            this.button23.UseVisualStyleBackColor = true;
            this.button7.BackgroundImage = (Image) manager.GetObject("button7.BackgroundImage");
            this.button7.BackgroundImageLayout = ImageLayout.Stretch;
            this.button7.FlatStyle = FlatStyle.Popup;
            this.button7.Font = new Font("宋体", 7.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.button7.Location = new Point(0x125, 11);
            this.button7.Name = "button7";
            this.button7.Size = new Size(0x37, 0x35);
            this.button7.TabIndex = 6;
            this.button7.Text = "血压(mmHg)";
            this.button7.UseVisualStyleBackColor = true;
            this.button11.BackgroundImage = (Image) manager.GetObject("button11.BackgroundImage");
            this.button11.BackgroundImageLayout = ImageLayout.Stretch;
            this.button11.FlatStyle = FlatStyle.Popup;
            this.button11.Location = new Point(0x1c9, 0x27);
            this.button11.Name = "button11";
            this.button11.Size = new Size(60, 0x19);
            this.button11.TabIndex = 11;
            this.button11.Text = "量ml";
            this.button11.UseVisualStyleBackColor = true;
            this.button9.BackgroundImage = (Image) manager.GetObject("button9.BackgroundImage");
            this.button9.BackgroundImageLayout = ImageLayout.Stretch;
            this.button9.FlatStyle = FlatStyle.Popup;
            this.button9.Location = new Point(0x15b, 0x27);
            this.button9.Name = "button9";
            this.button9.Size = new Size(0x6f, 0x19);
            this.button9.TabIndex = 8;
            this.button9.Text = "名称";
            this.button9.UseVisualStyleBackColor = true;
            this.PatientGridView.AllowUserToDeleteRows = false;
            this.PatientGridView.AllowUserToResizeColumns = false;
            this.PatientGridView.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.PatientGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.PatientGridView.BackgroundColor = Color.WhiteSmoke;
            this.PatientGridView.BorderStyle = BorderStyle.None;
            this.PatientGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PatientGridView.ColumnHeadersVisible = false;
            this.PatientGridView.Columns.AddRange(new DataGridViewColumn[] { 
                this.Column1, this.Column2, this.Column3, this.Column4, this.Column5, this.Column6, this.Column7, this.Column8, this.Column9, this.Column10, this.Column11, this.Column12, this.Column13, this.Column14, this.Column15, this.Column16, 
                this.Column17, this.Column18, this.Column19
             });
            this.PatientGridView.ContextMenuStrip = this.contextMenuStrip1;
            this.PatientGridView.Location = new Point(8, 0x3f);
            this.PatientGridView.Name = "PatientGridView";
            this.PatientGridView.RowHeadersWidth = 0x18;
            this.PatientGridView.RowTemplate.Height = 0x17;
            this.PatientGridView.ScrollBars = ScrollBars.Vertical;
            this.PatientGridView.Size = new Size(0x322, 0x145);
            this.PatientGridView.TabIndex = 0x15;
            this.PatientGridView.CellBeginEdit += new DataGridViewCellCancelEventHandler(this.PatientGridView_CellBeginEdit);
            this.PatientGridView.RowEnter += new DataGridViewCellEventHandler(this.PatientGridView_RowEnter);
            this.PatientGridView.RowLeave += new DataGridViewCellEventHandler(this.PatientGridView_RowLeave);
            this.PatientGridView.CellValidating += new DataGridViewCellValidatingEventHandler(this.PatientGridView_CellValidating);
            this.PatientGridView.CellEndEdit += new DataGridViewCellEventHandler(this.PatientGridView_CellEndEdit);
            this.PatientGridView.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.PatientGridView_DataBindingComplete);
            this.PatientGridView.CellContentClick += new DataGridViewCellEventHandler(this.PatientGridView_CellContentClick);
            this.Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Column1.DataPropertyName = "RDATE";
            style.BackColor = Color.WhiteSmoke;
            style.Format = "M";
            style.NullValue = null;
            this.Column1.DefaultCellStyle = style;
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = DataGridViewTriState.False;
            this.Column1.Width = 0x39;
            this.Column2.DataPropertyName = "STIME";
            style2.BackColor = Color.WhiteSmoke;
            style2.Format = "t";
            style2.NullValue = null;
            this.Column2.DefaultCellStyle = style2;
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = DataGridViewTriState.False;
            this.Column2.Width = 0x27;
            this.Column3.DataPropertyName = "NURSING_TMP";
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.Resizable = DataGridViewTriState.False;
            this.Column3.Width = 0x27;
            this.Column4.DataPropertyName = "NURSING_PULSE";
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = DataGridViewTriState.False;
            this.Column4.Width = 0x27;
            this.Column5.DataPropertyName = "NURSING_BREATH";
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            this.Column5.Resizable = DataGridViewTriState.False;
            this.Column5.Width = 0x2c;
            this.Column6.DataPropertyName = "NURSING_SATURATION";
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            this.Column6.Resizable = DataGridViewTriState.False;
            this.Column6.Width = 0x2c;
            this.Column7.DataPropertyName = "NURSING_BP";
            this.Column7.HeaderText = "Column7";
            this.Column7.Name = "Column7";
            this.Column7.Resizable = DataGridViewTriState.False;
            this.Column7.Width = 0x36;
            this.Column8.DataPropertyName = "DRUG_NAME";
            style3.WrapMode = DataGridViewTriState.True;
            this.Column8.DefaultCellStyle = style3;
            this.Column8.HeaderText = "Column8";
            this.Column8.Name = "Column8";
            this.Column8.Resizable = DataGridViewTriState.False;
            this.Column8.Width = 110;
            this.Column9.DataPropertyName = "DRUG_AMOUNT";
            style4.WrapMode = DataGridViewTriState.True;
            this.Column9.DefaultCellStyle = style4;
            this.Column9.HeaderText = "Column9";
            this.Column9.Name = "Column9";
            this.Column9.Resizable = DataGridViewTriState.False;
            this.Column9.Width = 0x3b;
            this.Column10.DataPropertyName = "FOOD_NAME";
            style5.WrapMode = DataGridViewTriState.True;
            this.Column10.DefaultCellStyle = style5;
            this.Column10.HeaderText = "Column10";
            this.Column10.Name = "Column10";
            this.Column10.Resizable = DataGridViewTriState.False;
            this.Column10.Width = 0x37;
            this.Column11.DataPropertyName = "FOOD_AMOUNT";
            style6.WrapMode = DataGridViewTriState.True;
            this.Column11.DefaultCellStyle = style6;
            this.Column11.HeaderText = "Column11";
            this.Column11.Name = "Column11";
            this.Column11.Resizable = DataGridViewTriState.False;
            this.Column11.Width = 0x2e;
            this.Column12.DataPropertyName = "DRAINAGE_NAME";
            style7.WrapMode = DataGridViewTriState.True;
            this.Column12.DefaultCellStyle = style7;
            this.Column12.HeaderText = "Column12";
            this.Column12.Name = "Column12";
            this.Column12.Resizable = DataGridViewTriState.False;
            this.Column12.Visible = false;
            this.Column12.Width = 0x33;
            this.Column13.DataPropertyName = "DRAINAGE_AMOUNT";
            style8.WrapMode = DataGridViewTriState.True;
            this.Column13.DefaultCellStyle = style8;
            this.Column13.HeaderText = "Column13";
            this.Column13.Name = "Column13";
            this.Column13.Resizable = DataGridViewTriState.False;
            this.Column13.Visible = false;
            this.Column13.Width = 50;
            this.Column14.DataPropertyName = "NURSING_STATE_ILL";
            style9.WrapMode = DataGridViewTriState.True;
            this.Column14.DefaultCellStyle = style9;
            this.Column14.HeaderText = "Column14";
            this.Column14.Name = "Column14";
            this.Column14.Resizable = DataGridViewTriState.False;
            this.Column14.Width = 0x73;
            this.Column15.DataPropertyName = "NURSING_DATE";
            this.Column15.HeaderText = "Column15";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Visible = false;
            this.Column16.FlatStyle = FlatStyle.Popup;
            this.Column16.HeaderText = "";
            this.Column16.Name = "Column16";
            this.Column16.Resizable = DataGridViewTriState.False;
            this.Column16.Text = ".";
            this.Column16.Width = 10;
            this.Column17.DataPropertyName = "PrintType";
            this.Column17.HeaderText = "";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Visible = false;
            this.Column18.DataPropertyName = "COLOR_FLAG";
            this.Column18.HeaderText = "Column18";
            this.Column18.Name = "Column18";
            this.Column18.Visible = false;
            this.Column19.DataPropertyName = "USER_NAME";
            this.Column19.HeaderText = "Column19";
            this.Column19.Name = "Column19";
            this.Column19.ReadOnly = true;
            this.Column19.Resizable = DataGridViewTriState.False;
            this.Column19.Width = 50;
            this.button10.BackgroundImage = (Image) manager.GetObject("button10.BackgroundImage");
            this.button10.BackgroundImageLayout = ImageLayout.Stretch;
            this.button10.FlatStyle = FlatStyle.Popup;
            this.button10.Location = new Point(8, 11);
            this.button10.Name = "button10";
            this.button10.Size = new Size(0x18, 0x35);
            this.button10.TabIndex = 0x16;
            this.button10.UseVisualStyleBackColor = true;
            this.button21.BackgroundImage = (Image) manager.GetObject("button21.BackgroundImage");
            this.button21.BackgroundImageLayout = ImageLayout.Stretch;
            this.button21.Enabled = false;
            this.button21.FlatStyle = FlatStyle.Popup;
            this.button21.ForeColor = SystemColors.ControlText;
            this.button21.Location = new Point(0x318, 11);
            this.button21.Name = "button21";
            this.button21.Size = new Size(0x12, 0x35);
            this.button21.TabIndex = 20;
            this.button21.UseVisualStyleBackColor = true;
            this.button20.BackgroundImage = (Image) manager.GetObject("button20.BackgroundImage");
            this.button20.BackgroundImageLayout = ImageLayout.Stretch;
            this.button20.FlatStyle = FlatStyle.Popup;
            this.button20.Location = new Point(0x269, 11);
            this.button20.Name = "button20";
            this.button20.Size = new Size(0x7e, 0x35);
            this.button20.TabIndex = 0x13;
            this.button20.Text = "病情";
            this.button20.UseVisualStyleBackColor = true;
            this.button19.BackgroundImage = (Image) manager.GetObject("button19.BackgroundImage");
            this.button19.FlatStyle = FlatStyle.Popup;
            this.button19.Location = new Point(0x23b, 0x27);
            this.button19.Name = "button19";
            this.button19.Size = new Size(0x2f, 0x19);
            this.button19.TabIndex = 0x12;
            this.button19.Text = "量ml";
            this.button19.UseVisualStyleBackColor = true;
            this.button18.BackgroundImage = (Image) manager.GetObject("button18.BackgroundImage");
            this.button18.FlatStyle = FlatStyle.Popup;
            this.button18.Location = new Point(0x204, 0x27);
            this.button18.Name = "button18";
            this.button18.Size = new Size(0x38, 0x19);
            this.button18.TabIndex = 0x11;
            this.button18.Text = "名称";
            this.button18.UseVisualStyleBackColor = true;
            this.button17.BackgroundImage = (Image) manager.GetObject("button17.BackgroundImage");
            this.button17.FlatStyle = FlatStyle.Popup;
            this.button17.Location = new Point(0x204, 11);
            this.button17.Name = "button17";
            this.button17.Size = new Size(0x66, 30);
            this.button17.TabIndex = 0x10;
            this.button17.Text = "排出液量";
            this.button17.UseVisualStyleBackColor = true;
            this.button15.BackgroundImage = (Image) manager.GetObject("button15.BackgroundImage");
            this.button15.BackgroundImageLayout = ImageLayout.Stretch;
            this.button15.FlatStyle = FlatStyle.Popup;
            this.button15.Location = new Point(0x1a8, 0x2d);
            this.button15.Name = "button15";
            this.button15.Size = new Size(0x2a, 0x13);
            this.button15.TabIndex = 15;
            this.button15.Text = "(ml)";
            this.button15.UseVisualStyleBackColor = true;
            this.button16.BackgroundImage = (Image) manager.GetObject("button16.BackgroundImage");
            this.button16.BackgroundImageLayout = ImageLayout.Stretch;
            this.button16.FlatStyle = FlatStyle.Popup;
            this.button16.Location = new Point(0x180, 0x2d);
            this.button16.Name = "button16";
            this.button16.Size = new Size(0x29, 0x13);
            this.button16.TabIndex = 14;
            this.button16.Text = "名称";
            this.button16.UseVisualStyleBackColor = true;
            this.button13.BackgroundImage = (Image) manager.GetObject("button13.BackgroundImage");
            this.button13.BackgroundImageLayout = ImageLayout.Stretch;
            this.button13.FlatStyle = FlatStyle.Popup;
            this.button13.Location = new Point(0x15a, 0x2d);
            this.button13.Name = "button13";
            this.button13.Size = new Size(0x27, 0x13);
            this.button13.TabIndex = 13;
            this.button13.Text = "(ml)";
            this.button13.UseVisualStyleBackColor = true;
            this.button14.BackgroundImage = (Image) manager.GetObject("button14.BackgroundImage");
            this.button14.BackgroundImageLayout = ImageLayout.Stretch;
            this.button14.FlatStyle = FlatStyle.Popup;
            this.button14.Location = new Point(0x132, 0x2d);
            this.button14.Name = "button14";
            this.button14.Size = new Size(0x29, 0x13);
            this.button14.TabIndex = 12;
            this.button14.Text = "名称";
            this.button14.UseVisualStyleBackColor = true;
            this.button8.BackgroundImage = (Image) manager.GetObject("button8.BackgroundImage");
            this.button8.BackgroundImageLayout = ImageLayout.Stretch;
            this.button8.FlatStyle = FlatStyle.Popup;
            this.button8.Location = new Point(0x15a, 11);
            this.button8.Name = "button8";
            this.button8.Size = new Size(0xab, 30);
            this.button8.TabIndex = 7;
            this.button8.Text = "食物及输入液种类";
            this.button8.UseVisualStyleBackColor = true;
            this.textBox1.Location = new Point(0x1a8, 0x97);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(100, 0x15);
            this.textBox1.TabIndex = 0x13;
            this.button6.BackgroundImage = (Image) manager.GetObject("button6.BackgroundImage");
            this.button6.BackgroundImageLayout = ImageLayout.Stretch;
            this.button6.FlatStyle = FlatStyle.Popup;
            this.button6.Font = new Font("宋体", 7.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.button6.Location = new Point(0xf9, 11);
            this.button6.Name = "button6";
            this.button6.Size = new Size(0x2d, 0x35);
            this.button6.TabIndex = 5;
            this.button6.Text = "血养饱和(%)";
            this.button6.UseVisualStyleBackColor = true;
            this.button5.BackgroundImage = (Image) manager.GetObject("button5.BackgroundImage");
            this.button5.BackgroundImageLayout = ImageLayout.Stretch;
            this.button5.FlatStyle = FlatStyle.Popup;
            this.button5.Font = new Font("宋体", 7.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.button5.Location = new Point(0xcd, 11);
            this.button5.Name = "button5";
            this.button5.Size = new Size(0x2d, 0x35);
            this.button5.TabIndex = 4;
            this.button5.Text = "呼吸 次/分";
            this.button5.UseVisualStyleBackColor = true;
            this.button4.BackgroundImage = (Image) manager.GetObject("button4.BackgroundImage");
            this.button4.FlatStyle = FlatStyle.Popup;
            this.button4.Font = new Font("宋体", 7.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.button4.Location = new Point(0xa6, 11);
            this.button4.Name = "button4";
            this.button4.Size = new Size(40, 0x35);
            this.button4.TabIndex = 3;
            this.button4.Text = "脉搏 次/分";
            this.button4.UseVisualStyleBackColor = true;
            this.button3.BackgroundImage = (Image) manager.GetObject("button3.BackgroundImage");
            this.button3.FlatStyle = FlatStyle.Popup;
            this.button3.Font = new Font("宋体", 7.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.button3.Location = new Point(0x7f, 11);
            this.button3.Name = "button3";
            this.button3.Size = new Size(40, 0x35);
            this.button3.TabIndex = 2;
            this.button3.Text = "体温(℃)";
            this.button3.UseVisualStyleBackColor = true;
            this.button2.BackgroundImage = (Image) manager.GetObject("button2.BackgroundImage");
            this.button2.BackgroundImageLayout = ImageLayout.Stretch;
            this.button2.FlatStyle = FlatStyle.Popup;
            this.button2.Location = new Point(0x58, 11);
            this.button2.Name = "button2";
            this.button2.Size = new Size(40, 0x35);
            this.button2.TabIndex = 1;
            this.button2.Text = "时间";
            this.button2.UseVisualStyleBackColor = true;
            this.button1.BackgroundImage = (Image) manager.GetObject("button1.BackgroundImage");
            this.button1.BackgroundImageLayout = ImageLayout.Stretch;
            this.button1.FlatStyle = FlatStyle.Popup;
            this.button1.Location = new Point(30, 11);
            this.button1.Name = "button1";
            this.button1.Size = new Size(60, 0x35);
            this.button1.TabIndex = 0;
            this.button1.Text = "日期";
            this.button1.UseVisualStyleBackColor = true;
            this.textBox2.Location = new Point(0x12a, 0xdd);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(100, 0x15);
            this.textBox2.TabIndex = 0x17;
            this.button24.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.button24.Location = new Point(450, 0x1d0);
            this.button24.Name = "button24";
            this.button24.Size = new Size(0x2a, 0x17);
            this.button24.TabIndex = 0x2a;
            this.button24.Text = "标组";
            this.button24.UseVisualStyleBackColor = true;
            this.button24.Click += new EventHandler(this.button24_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x34a, 490);
            base.Controls.Add(this.button24);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btUpData);
            base.Controls.Add(this.button22);
            base.Controls.Add(this.button12);
            base.Controls.Add(this.textBox3);
            base.Controls.Add(this.dtRngEnd);
            base.Controls.Add(this.btnReprint);
            base.Controls.Add(this.dtRngStart);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.label10);
            base.Controls.Add(this.btnSave);
            base.Controls.Add(this.btnQuery);
            base.Controls.Add(this.btnExit);
            base.Controls.Add(this.btnContinuePrint);
            base.Controls.Add(this.groupBox1);
            base.Name = "NurseRecordWz_P";
            this.Text = "NurseRecordWz_P";
            base.Load += new EventHandler(this.NursingRecDocument_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((ISupportInitialize) this.PatientGridView).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void NursingRecDocument_Load(object sender, EventArgs e)
        {
            try
            {
                this.initFrmVal();
                this.initDisp();
            }
            catch (Exception exception)
            {
                Error.ErrProc(exception);
            }
        }

        private DataSet PatientData(string patientId, string visitId)
        {
            string sqlSel = ((((string.Empty + "SELECT NURSING_DATE as RDATE,NURSING_DATE as STIME,NURSING_TMP,NURSING_PULSE,NURSING_BREATH,NURSING_SATURATION,NURSING_BP,DRUG_NAME,DRUG_AMOUNT,FOOD_NAME,FOOD_AMOUNT,DRAINAGE_NAME,DRAINAGE_AMOUNT,NURSING_STATE_ILL,NURSING_DATE,PrintType,COLOR_FLAG,USER_NAME" + " FROM ") + "NURSING_RECORD " + "WHERE ") + "PATIENT_ID = " + SqlManager.SqlConvert(patientId)) + "AND VISIT_ID = " + SqlManager.SqlConvert(visitId)) + "ORDER BY " + " NURSING_DATE ASC";
            return GVars.OracleAccess.SelectData(sqlSel, "NURSING_RECORD");
        }

        private DataSet PatientData(string patientId, string visitId, DateTime dtStart, DateTime dtEnd)
        {
            string sqlSel = ((((string.Empty + "SELECT NURSING_DATE as RDATE,NURSING_DATE as STIME,NURSING_TMP,NURSING_PULSE,NURSING_BREATH,NURSING_SATURATION,NURSING_BP,DRUG_NAME,DRUG_AMOUNT,FOOD_NAME,FOOD_AMOUNT,DRAINAGE_NAME,DRAINAGE_AMOUNT,NURSING_STATE_ILL,NURSING_DATE,PrintType,COLOR_FLAG,USER_NAME" + "  FROM ") + "NURSING_RECORD " + "WHERE ") + "PATIENT_ID = " + SqlManager.SqlConvert(patientId)) + "AND VISIT_ID = " + SqlManager.SqlConvert(visitId)) + "AND NURSING_DATE >= " + SqlManager.GetOraDbDate_Short(dtStart.ToShortDateString());
            DateTime time = DateTime.Parse(dtEnd.ToString());
            sqlSel = (sqlSel + "AND NURSING_DATE < " + SqlManager.GetOraDbDate_Short(time.Date.AddDays(1.0).ToShortDateString())) + "ORDER BY " + " NURSING_DATE ASC";
            return GVars.OracleAccess.SelectData(sqlSel);
        }

        private void PatientGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.GVCindex = e.ColumnIndex;
            this.GVRindex = e.RowIndex;
            this.GridViewVstr = this.PatientGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void PatientGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string str = this.PatientGridView.Rows[e.RowIndex].Cells["Column14"].Value.ToString();
                if ((this.PatientGridView.Columns[e.ColumnIndex] == this.Column16) && (this.txtBedLabel.Text != ""))
                {
                    string str2;
                    FormTextEditor editor = new FormTextEditor(this.PatientGridView.Rows[e.RowIndex].Cells["Column14"].Value.ToString());
                    editor.ShowDialog();
                    this.PatientGridView.Rows[e.RowIndex].Cells["Column14"].Value = editor.Desc;
                    DateTime now = DateTime.Now;
                    if ((this.PatientGridView.Rows[e.RowIndex].Cells["Column1"].Value.ToString().Length < 1) && (this.obj != null))
                    {
                        str2 = "insert into NURSING_RECORD (DEPT_CODE,PATIENT_ID,VISIT_ID,NURSING_DATE,";
                        str2 = (((((str2 + this.PatientGridView.Columns["Column14"].DataPropertyName + ",") + "DB_USER,USER_NAME)" + "values( ") + "'" + GVars.User.DeptCode + "'") + ",'" + this.patientId + "'") + ",'" + this.visitId + "'") + "," + SqlManager.GetOraDbDate(now);
                        str2 = ((string.Concat(new object[] { str2, ",'", this.PatientGridView.Rows[e.RowIndex].Cells["Column14"].Value, "'" }) + ",'" + GVars.User.ID + "'") + ",'" + GVars.User.Name + "'") + ")";
                        GVars.OracleAccess.ExecuteNoQuery(str2);
                        this.PatientGridView.Rows[e.RowIndex].Cells["Column15"].Value = now;
                        this.PatientGridView.Rows[e.RowIndex].Cells["Column1"].Value = now;
                        this.PatientGridView.Rows[e.RowIndex].Cells["Column2"].Value = now;
                    }
                    else
                    {
                        string str3 = "";
                        try
                        {
                            str3 = this.PatientGridView.Rows[e.RowIndex].Cells["Column19"].Value.ToString();
                        }
                        catch
                        {
                        }
                        bool flag = (this._userRight.IndexOf("2") >= 0) || GVars.User.Name.Equals(str3);
                        if (((str3 == ".") || (str3 == GVars.User.Name)) || flag)
                        {
                            str2 = "UPDATE NURSING_RECORD ";
                            object obj2 = str2 + "SET ";
                            str2 = (((((string.Concat(new object[] { obj2, this.PatientGridView.Columns["Column14"].DataPropertyName, " = '", this.PatientGridView.Rows[e.RowIndex].Cells["Column14"].Value, "'" }) + ",DB_USER = '" + GVars.User.ID + "'") + ",USER_NAME = '" + GVars.User.Name + "'") + " WHERE ") + "PATIENT_ID = '" + this.patientId + "'") + "AND VISIT_ID = '" + this.visitId + "'") + "AND NURSING_DATE = " + SqlManager.GetOraDbDate(this.PatientGridView.Rows[e.RowIndex].Cells["Column15"].Value.ToString());
                            GVars.OracleAccess.ExecuteNoQuery(str2);
                            this.PatientGridView.Rows[e.RowIndex].Cells["Column19"].Value = GVars.User.Name;
                        }
                        else
                        {
                            MessageBox.Show("没有编辑权限！");
                            this.PatientGridView.Rows[e.RowIndex].Cells["Column14"].Value = str;
                        }
                    }
                    this.obj = null;
                }
            }
            catch
            {
            }
        }

        private void PatientGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if ((this.txtBedLabel.Text.Trim().Length != 0) && this.enabled)
            {
                this.PatientGridView.Rows[e.RowIndex].ErrorText = "";
                DateTime now = DateTime.Now;
                try
                {
                    string str;
                    if ((this.PatientGridView.Rows[e.RowIndex].Cells["Column1"].Value.ToString().Length < 1) && (this.obj != null))
                    {
                        str = "insert into NURSING_RECORD (DEPT_CODE,PATIENT_ID,VISIT_ID,NURSING_DATE,";
                        str = (((((str + this.PatientGridView.Columns[e.ColumnIndex].DataPropertyName + ",") + "DB_USER,USER_NAME)" + "values( ") + "'" + GVars.User.DeptCode + "'") + ",'" + this.patientId + "'") + ",'" + this.visitId + "'") + "," + SqlManager.GetOraDbDate(now);
                        str = string.Concat(new object[] { str, ",'", this.PatientGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, "'" });
                        if (this.PatientGridView.Columns[e.ColumnIndex].DataPropertyName == "NURSING_STATE_ILL")
                        {
                            str = (str + ",'" + GVars.User.ID + "'") + ",'" + GVars.User.Name + "'";
                            this.PatientGridView.Rows[e.RowIndex].Cells["Column19"].Value = GVars.User.Name;
                        }
                        else
                        {
                            str = str + ",'.'" + ",'.'";
                        }
                        str = str + ")";
                        GVars.OracleAccess.ExecuteNoQuery(str);
                        this.PatientGridView.Rows[e.RowIndex].Cells["Column15"].Value = now;
                        this.PatientGridView.Rows[e.RowIndex].Cells["Column1"].Value = now;
                        this.PatientGridView.Rows[e.RowIndex].Cells["Column2"].Value = now;
                    }
                    else
                    {
                        object obj2;
                        string str2 = ".";
                        try
                        {
                            str2 = this.PatientGridView.Rows[e.RowIndex].Cells["Column19"].Value.ToString();
                        }
                        catch
                        {
                        }
                        bool flag = (this._userRight.IndexOf("2") >= 0) || GVars.User.Name.Equals(str2);
                        switch (str2)
                        {
                            case ".":
                            case "":
                                str = "UPDATE NURSING_RECORD ";
                                obj2 = str + "SET ";
                                str = string.Concat(new object[] { obj2, this.PatientGridView.Columns[e.ColumnIndex].DataPropertyName, " = '", this.PatientGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, "'" });
                                if (this.PatientGridView.Columns[e.ColumnIndex].DataPropertyName == "NURSING_STATE_ILL")
                                {
                                    str = (str + ",DB_USER = '" + GVars.User.ID + "'") + ",USER_NAME = '" + GVars.User.Name + "'";
                                    this.PatientGridView.Rows[e.RowIndex].Cells["Column19"].Value = GVars.User.Name;
                                }
                                str = (((str + "WHERE ") + "PATIENT_ID = '" + this.patientId + "'") + "AND VISIT_ID = '" + this.visitId + "'") + "AND NURSING_DATE = " + SqlManager.GetOraDbDate(this.PatientGridView.Rows[e.RowIndex].Cells["Column15"].Value.ToString());
                                GVars.OracleAccess.ExecuteNoQuery(str);
                                goto Label_06BC;
                        }
                        if (flag)
                        {
                            str = "UPDATE NURSING_RECORD ";
                            obj2 = str + "SET ";
                            str = (((string.Concat(new object[] { obj2, this.PatientGridView.Columns[e.ColumnIndex].DataPropertyName, " = '", this.PatientGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, "'" }) + "WHERE ") + "PATIENT_ID = '" + this.patientId + "'") + "AND VISIT_ID = '" + this.visitId + "'") + "AND NURSING_DATE = " + SqlManager.GetOraDbDate(this.PatientGridView.Rows[e.RowIndex].Cells["Column15"].Value.ToString());
                            GVars.OracleAccess.ExecuteNoQuery(str);
                        }
                        else
                        {
                            MessageBox.Show("没有编辑权限！");
                            try
                            {
                                this.PatientGridView.Rows[this.GVRindex].Cells[this.GVCindex].Value = this.GridViewVstr;
                            }
                            catch
                            {
                            }
                        }
                    }
                Label_06BC:
                    this.obj = null;
                }
                catch
                {
                }
            }
        }

        private void PatientGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                this.obj = e.FormattedValue;
            }
            catch
            {
            }
        }

        private void PatientGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                string str = "07:00";
                string str2 = "18:59";
                DateTime time2 = Convert.ToDateTime(str);
                DateTime time3 = Convert.ToDateTime(str2);
                foreach (DataGridViewRow row in (IEnumerable) this.PatientGridView.Rows)
                {
                    if (row.Cells[14].Value != null)
                    {
                        DateTime now;
                        try
                        {
                            now = DateTime.Parse(row.Cells["Column15"].Value.ToString());
                        }
                        catch
                        {
                            now = DateTime.Now;
                        }
                        if ((now.Hour > 7) && (now.Hour < 0x13))
                        {
                            row.DefaultCellStyle.ForeColor = Color.Blue;
                        }
                        else
                        {
                            row.DefaultCellStyle.ForeColor = Color.Red;
                        }
                    }
                    if (row.Cells["Column7"].Value != null)
                    {
                        try
                        {
                            if (row.Cells["Column18"].Value.ToString().IndexOf("RED") >= 0)
                            {
                                row.DefaultCellStyle.ForeColor = Color.Blue;
                                row.DefaultCellStyle.BackColor = Color.LightCoral;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void PatientGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void PatientGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void PrintUpdata(DateTime RowTime)
        {
            string sql = "UPDATE NURSING_RECORD ";
            sql = ((((sql + "SET ") + "PrintType = 1" + "WHERE ") + "PATIENT_ID = '" + this.patientId + "'") + "AND VISIT_ID = '" + this.visitId + "'") + "AND NURSING_DATE = " + SqlManager.GetOraDbDate(RowTime);
            GVars.OracleAccess.ExecuteNoQuery(sql);
        }

        private void PrintUpdata2()
        {
            string sqlSel = ((((string.Empty + "SELECT DRAINAGE_AMOUNT,NURSING_STATE_ILL,DRUG_NAME,FOOD_NAME,NURSING_DATE" + " FROM ") + "NURSING_RECORD " + "WHERE ") + "PATIENT_ID = " + SqlManager.SqlConvert(this.patientId)) + " AND VISIT_ID = " + SqlManager.SqlConvert(this.visitId)) + " ORDER BY " + " NURSING_DATE ASC";
            DataTable table = GVars.OracleAccess.SelectData(sqlSel).Tables[0];
            int num = 0;
            int num2 = 1;
            int num3 = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                num3 = 0;
                string[] strArray = table.Rows[i]["DRUG_NAME"].ToString().Replace(".\r\n", "|").Split(new char[] { '|' });
                string[] strArray2 = table.Rows[i]["FOOD_NAME"].ToString().Replace(".\r\n", "|").Split(new char[] { '|' });
                string[] strArray3 = this.TextEditDesc1(table.Rows[i]["NURSING_STATE_ILL"].ToString(), this.linenumber).Replace("\r\n", "⊙").Split(new char[] { '⊙' });
                int length = strArray.Length;
                int num6 = strArray2.Length;
                int num7 = strArray3.Length;
                if ((length > num6) && (length > num7))
                {
                    num3 = length;
                }
                else if ((num6 > length) && (num6 > num7))
                {
                    num3 = num6;
                }
                else if ((length == num6) && (num6 >= num7))
                {
                    num3 = length;
                }
                else
                {
                    num3 = num7;
                }
                if (num3 == 0)
                {
                    num3 = 1;
                }
                num += num3;
                if (num >= (this.printrows + 1))
                {
                    num -= this.printrows;
                    num2++;
                }
                string str2 = "UPDATE NURSING_RECORD ";
                str2 = ((str2 + "SET ") + "PAGE_INDEX = " + num2) + ",ROWS_NUM = " + num3;
                if (num == 0)
                {
                    str2 = str2 + ",PRINTTYPE = " + this.printrows;
                }
                else
                {
                    str2 = str2 + ",PRINTTYPE = " + num;
                }
                if (num3 > num)
                {
                    int num8 = num2 - 1;
                    object obj2 = str2;
                    str2 = string.Concat(new object[] { obj2, ",ROWS_PAGE_INDEX = '", num8, "'" });
                }
                str2 = (((str2 + " WHERE ") + "PATIENT_ID = '" + this.patientId + "'") + " AND VISIT_ID = '" + this.visitId + "'") + " AND NURSING_DATE = " + SqlManager.GetOraDbDate(table.Rows[i]["NURSING_DATE"].ToString());
                GVars.OracleAccess.ExecuteNoQuery(str2);
            }
            string sql = "UPDATE NURSING_RECORD ";
            sql = sql + "SET ";
            num++;
            sql = ((((sql + "ROWS_COUNT = " + num) + " WHERE ") + "PATIENT_ID = '" + this.patientId + "'") + " AND VISIT_ID = '" + this.visitId + "'") + " AND NURSING_DATE = " + SqlManager.GetOraDbDate(table.Rows[table.Rows.Count - 1]["NURSING_DATE"].ToString());
            GVars.OracleAccess.ExecuteNoQuery(sql);
        }

        private void PrintUpdata3()
        {
            string sqlSel = (((((string.Empty + "SELECT DRAINAGE_AMOUNT,NURSING_STATE_ILL,NURSING_DATE,DRUG_NAME,FOOD_NAME,ROWS_PAGE_INDEX" + " FROM ") + "NURSING_RECORD " + "WHERE ") + "PATIENT_ID = " + SqlManager.SqlConvert(this.patientId)) + " AND VISIT_ID = " + SqlManager.SqlConvert(this.visitId)) + " AND NURSING_DATE > " + SqlManager.GetOraDbDate(this.datime)) + " ORDER BY " + " NURSING_DATE ASC";
            DataTable table = GVars.OracleAccess.SelectData(sqlSel).Tables[0];
            int num = this.rownum - 1;
            int pagenum = this.pagenum;
            int num3 = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                num3 = 0;
                string[] strArray = table.Rows[i]["DRUG_NAME"].ToString().Replace(".\r\n", "|").Split(new char[] { '|' });
                string[] strArray2 = table.Rows[i]["FOOD_NAME"].ToString().Replace(".\r\n", "|").Split(new char[] { '|' });
                string[] strArray3 = this.TextEditDesc1(table.Rows[i]["NURSING_STATE_ILL"].ToString(), this.linenumber).Replace("\r\n", "⊙").Split(new char[] { '⊙' });
                int length = strArray.Length;
                int num6 = strArray2.Length;
                int num7 = strArray3.Length;
                if ((length > num6) && (length > num7))
                {
                    num3 = length;
                }
                else if ((num6 > length) && (num6 > num7))
                {
                    num3 = num6;
                }
                else if ((length == num6) && (num6 >= num7))
                {
                    num3 = length;
                }
                else
                {
                    num3 = num7;
                }
                if (num3 == 0)
                {
                    num3 = 1;
                }
                num += num3;
                if (num >= (this.printrows + 1))
                {
                    num -= this.printrows;
                    pagenum++;
                }
                string str2 = "UPDATE NURSING_RECORD ";
                str2 = ((str2 + "SET ") + "PAGE_INDEX = " + pagenum) + ",ROWS_NUM = " + num3;
                if (num == 0)
                {
                    str2 = str2 + ",PRINTTYPE = " + this.printrows;
                }
                else
                {
                    str2 = str2 + ",PRINTTYPE = " + num;
                }
                if (num3 > num)
                {
                    int num8 = pagenum - 1;
                    object obj2 = str2;
                    str2 = string.Concat(new object[] { obj2, ",ROWS_PAGE_INDEX = '", num8, "'" });
                }
                str2 = (((str2 + " WHERE ") + "PATIENT_ID = '" + this.patientId + "'") + " AND VISIT_ID = '" + this.visitId + "'") + " AND NURSING_DATE = " + SqlManager.GetOraDbDate(table.Rows[i]["NURSING_DATE"].ToString());
                GVars.OracleAccess.ExecuteNoQuery(str2);
            }
            string sql = "UPDATE NURSING_RECORD ";
            sql = sql + "SET ";
            num++;
            sql = ((((sql + "ROWS_COUNT = " + num) + " WHERE ") + "PATIENT_ID = '" + this.patientId + "'") + " AND VISIT_ID = '" + this.visitId + "'") + " AND NURSING_DATE = " + SqlManager.GetOraDbDate(table.Rows[table.Rows.Count - 1]["NURSING_DATE"].ToString());
            GVars.OracleAccess.ExecuteNoQuery(sql);
        }

        private void SaveInfo(string Xstr, string dtstr, DateTime tm)
        {
            string sqlSel = ((((string.Empty + "SELECT DEPT_CODE,NURSING_PULSE,PATIENT_ID,VISIT_ID,DB_USER,USER_NAME,NURSING_DATE,NURSING_BREATH,NURSING_SATURATION,NURSING_BP,DRUG_AMOUNT,FOOD_AMOUNT,DRAINAGE_NAME,DRAINAGE_AMOUNT" + " FROM ") + "NURSING_RECORD " + "WHERE ") + "PATIENT_ID = " + SqlManager.SqlConvert(this.patientId)) + "AND VISIT_ID = " + SqlManager.SqlConvert(this.visitId)) + "ORDER BY " + " NURSING_DATE ASC";
            string date = tm.Date.ToShortDateString() + " " + GVars.IniFile.ReadString("Exchange_Work_Time", "EW_Z", string.Empty) + ":00";
            string str3 = tm.Date.ToShortDateString() + " " + GVars.IniFile.ReadString("Exchange_Work_Time", "EW_W", string.Empty) + ":00";
            string str4 = tm.Year.ToString() + "-" + tm.Month.ToString() + "-" + tm.AddDays(-1.0).ToString() + " " + GVars.IniFile.ReadString("Exchange_Work_Time", "EW_Z", string.Empty) + ":00";
            this.ds2 = new DataSet();
            GVars.OracleAccess.SelectData(sqlSel, "NURSING_RECORD", ref this.ds2, true);
            try
            {
                string sql = "DELETE FROM NURSING_RECORD WHERE ";
                sql = (sql + "PATIENT_ID = '" + this.patientId + "'") + "AND VISIT_ID = '" + this.visitId + "'";
                if (Xstr == "1")
                {
                    sql = sql + "AND NURSING_DATE = " + SqlManager.GetOraDbDate(str3);
                }
                else if (Xstr == "2")
                {
                    sql = sql + "AND NURSING_DATE = " + SqlManager.GetOraDbDate(date);
                }
                GVars.OracleAccess.ExecuteNoQuery(sql);
            }
            catch (Exception)
            {
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            DataRow row = this.ds2.Tables["NURSING_RECORD"].NewRow();
            row["DEPT_CODE"] = GVars.User.DeptCode;
            row["PATIENT_ID"] = this.patientId;
            row["VISIT_ID"] = this.visitId;
            row["DB_USER"] = GVars.User.ID;
            row["USER_NAME"] = GVars.User.Name;
            if (Xstr == "1")
            {
                row["NURSING_DATE"] = str3;
                row["NURSING_PULSE"] = "日";
                row["NURSING_BREATH"] = "间";
                row["NURSING_SATURATION"] = "小";
            }
            else if (Xstr == "2")
            {
                row["NURSING_DATE"] = date;
                row["NURSING_PULSE"] = "24";
                row["NURSING_BREATH"] = "小时";
                row["NURSING_SATURATION"] = "总";
            }
            row["NURSING_BP"] = "结";
            string[] strArray = dtstr.Split(new char[] { '*' });
            string str6 = strArray[0].ToString();
            string str7 = strArray[1].ToString();
            if (str6 == "")
            {
                str6 = "0";
            }
            if (str7 == "")
            {
                str7 = "0";
            }
            row["DRUG_AMOUNT"] = str6;
            row["FOOD_AMOUNT"] = str7;
            row["DRAINAGE_NAME"] = " ";
            row["DRAINAGE_AMOUNT"] = " ";
            this.ds2.Tables["NURSING_RECORD"].Rows.Add(row);
            GVars.OracleAccess.Update(ref this.ds2, "NURSING_RECORD", sqlSel);
        }

        private void showNursingRec(string timePoint)
        {
        }

        private void showNursingRec(DataRow[] drArray, int rowStart)
        {
        }

        private void showPatientInfo()
        {
            this.patientId = string.Empty;
            this.visitId = string.Empty;
            if (((this.dsPatient == null) || (this.dsPatient.Tables.Count == 0)) || (this.dsPatient.Tables[0].Rows.Count == 0))
            {
                GVars.Msg.Show("W00005");
            }
            else
            {
                DataRow row = this.dsPatient.Tables[0].Rows[0];
                PersonCls cls = new PersonCls();
                string s = row["DATE_OF_BIRTH"].ToString();
                if (s.Length > 0)
                {
                    s = PersonCls.GetAge(DateTime.Parse(s), GVars.OracleAccess.GetSysDate());
                }
                this.lblName.Text = row["NAME"].ToString();
                this.lblGender.Text = row["SEX"].ToString();
                this.lblAge.Text = s;
                this.lblDeptName.Text = row["DEPT_NAME"].ToString();
                this.lblDocNo.Text = row["INP_NO"].ToString();
                this.lblDiagnose.Text = row["DIAGNOSIS"].ToString();
                this.BedLabel = row["BED_NO"].ToString();
                this.patientId = row["PATIENT_ID"].ToString();
                this.visitId = row["VISIT_ID"].ToString();
            }
        }

        private void showStr(string str, int ii)
        {
            this.textBox1.Width = ii * 15;
            this.textBox1.Text = str;
            this.getLines();
        }

        private string sss(string istr, int length)
        {
            string str = "";
            if (length > 0)
            {
                Graphics graphics = base.CreateGraphics();
                SizeF ef = graphics.MeasureString("11", this.Font);
                SizeF ef2 = graphics.MeasureString("1 1", this.Font);
                graphics.Dispose();
                int num = (int) Math.Round((double) (((float) length) / (ef2.Width - ef.Width)));
                for (int i = 0; i < num; i++)
                {
                    str = str + " ";
                }
                return (istr + str);
            }
            return istr;
        }

        private int StringtoInt(string str)
        {
            int num = 0;
            try
            {
                if (str != "")
                {
                    num = int.Parse(str);
                }
            }
            catch
            {
            }
            return num;
        }

        public string TextEditDesc1(string desc, int TxtWidth)
        {
            this.showStr(desc.Replace("\r\n", string.Empty), TxtWidth);
            if (this.ArrDesc1 != null)
            {
                this.desc1 = string.Empty;
                for (int i = 0; i < this.ArrDesc1.Length; i++)
                {
                    this.desc1 = this.desc1 + this.ArrDesc1[i] + "\r\n";
                }
                this.desc1 = this.desc1.TrimEnd(new char[0]);
            }
            return this.desc1;
        }

        private void ToolStripNoRed_Click(object sender, EventArgs e)
        {
            if (this.PatientGridView.Rows.Count > 1)
            {
                string sql = "UPDATE NURSING_RECORD ";
                sql = ((((sql + "SET ") + "COLOR_FLAG = null" + " WHERE ") + "PATIENT_ID = '" + this.patientId + "'") + " AND VISIT_ID = '" + this.visitId + "'") + " AND NURSING_DATE = " + SqlManager.GetOraDbDate(this.PatientGridView["Column15", this.PatientGridView.CurrentCell.RowIndex].Value.ToString());
                GVars.OracleAccess.ExecuteNoQuery(sql);
                DataSet set = new DataSet();
                if (this.toselect)
                {
                    set = this.PatientData(this.patientId, this.visitId, this.dtRngStart.Value, this.dtRngEnd.Value);
                    this.PatientGridView.DataSource = set.Tables[0].DefaultView;
                }
                if (!this.toselect)
                {
                    set = this.PatientData(this.patientId, this.visitId);
                    this.PatientGridView.DataSource = set.Tables[0].DefaultView;
                }
                DateTime time = DateTime.Parse(this.PatientGridView["Column15", this.PatientGridView.CurrentCell.RowIndex].Value.ToString());
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    if (DateTime.Parse(set.Tables[0].Rows[i]["NURSING_DATE"].ToString()) == time)
                    {
                        this.PatientGridView.FirstDisplayedScrollingRowIndex = i;
                        this.PatientGridView.Rows[i].Selected = true;
                        this.PatientGridView.CurrentCell = this.PatientGridView.Rows[i].Cells[1];
                    }
                }
            }
        }

        private void ToolStripRed_Click(object sender, EventArgs e)
        {
            if (this.PatientGridView.Rows.Count > 1)
            {
                string sql = "UPDATE NURSING_RECORD ";
                sql = ((((sql + "SET ") + "COLOR_FLAG = 'RED'" + " WHERE ") + "PATIENT_ID = '" + this.patientId + "'") + " AND VISIT_ID = '" + this.visitId + "'") + " AND NURSING_DATE = " + SqlManager.GetOraDbDate(this.PatientGridView["Column15", this.PatientGridView.CurrentCell.RowIndex].Value.ToString());
                GVars.OracleAccess.ExecuteNoQuery(sql);
                DataSet set = new DataSet();
                if (this.toselect)
                {
                    set = this.PatientData(this.patientId, this.visitId, this.dtRngStart.Value, this.dtRngEnd.Value);
                    this.PatientGridView.DataSource = set.Tables[0].DefaultView;
                }
                if (!this.toselect)
                {
                    set = this.PatientData(this.patientId, this.visitId);
                    this.PatientGridView.DataSource = set.Tables[0].DefaultView;
                }
                DateTime time = DateTime.Parse(this.PatientGridView["Column15", this.PatientGridView.CurrentCell.RowIndex].Value.ToString());
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    if (DateTime.Parse(set.Tables[0].Rows[i]["NURSING_DATE"].ToString()) == time)
                    {
                        this.PatientGridView.FirstDisplayedScrollingRowIndex = i;
                        this.PatientGridView.Rows[i].Selected = true;
                        this.PatientGridView.CurrentCell = this.PatientGridView.Rows[i].Cells[1];
                    }
                }
            }
        }

        private void ToolStripTime_Click_1(object sender, EventArgs e)
        {
            FormTime time = new FormTime();
            time.ShowDialog();
            DateTime dtime = time.Dtime;
            if (!time.Quit && (this.patientId.Length != 0))
            {
                string sql = "insert into NURSING_RECORD (DEPT_CODE,PATIENT_ID,VISIT_ID,NURSING_DATE,";
                sql = ((((((sql + "DB_USER,USER_NAME)" + "values( ") + "'" + GVars.User.DeptCode + "'") + ",'" + this.patientId + "'") + ",'" + this.visitId + "'") + "," + SqlManager.GetOraDbDate(dtime)) + ",'.'") + ",'.'" + ")";
                GVars.OracleAccess.ExecuteNoQuery(sql);
                DataSet set = new DataSet();
                if (this.toselect)
                {
                    set = this.PatientData(this.patientId, this.visitId, this.dtRngStart.Value, this.dtRngEnd.Value);
                    this.PatientGridView.DataSource = set.Tables[0].DefaultView;
                }
                if (!this.toselect)
                {
                    set = this.PatientData(this.patientId, this.visitId);
                    this.PatientGridView.DataSource = set.Tables[0].DefaultView;
                }
            }
        }

        private void ToolStripXJTJ_Click(object sender, EventArgs e)
        {
            try
            {
                FormTime time = new FormTime {
                    DateType = true
                };
                time.ShowDialog();
                if (!time.Quit)
                {
                    DateTime dtime = time.Dtime;
                    string str = dtime.Date.ToShortDateString() + " " + GVars.IniFile.ReadString("Exchange_Work_Time", "EW_Z", string.Empty);
                    string str2 = dtime.Date.ToShortDateString() + " " + GVars.IniFile.ReadString("Exchange_Work_Time", "EW_W", string.Empty);
                    string str3 = str + ":31";
                    string str4 = str2 + ":30";
                    str = str + ":00";
                    str2 = str2 + ":00";
                    string sqlSel = "select DRUG_AMOUNT,FOOD_AMOUNT from NURSING_RECORD ";
                    string str6 = sqlSel;
                    sqlSel = ((((str6 + " where NURSING_DATE >= to_date('" + str3 + "','yyyy-mm-dd hh24:mi:ss') and NURSING_DATE < to_date('" + str4 + "','yyyy-mm-dd hh24:mi:ss')") + " and PATIENT_ID = " + SqlManager.SqlConvert(this.patientId)) + "AND VISIT_ID = " + SqlManager.SqlConvert(this.visitId)) + " and NURSING_DATE <> to_date('" + str + "','yyyy-mm-dd hh24:mi:ss')") + " and NURSING_DATE <> to_date('" + str2 + "','yyyy-mm-dd hh24:mi:ss')";
                    DataTable dt = GVars.OracleAccess.SelectData(sqlSel).Tables[0];
                    this.SaveInfo("1", this.HZdata(dt), dtime);
                    if (this.toselect)
                    {
                        this.PatientGridView.DataSource = this.PatientData(this.patientId, this.visitId, this.dtRngStart.Value, this.dtRngEnd.Value).Tables[0].DefaultView;
                    }
                    if (!this.toselect)
                    {
                        this.PatientGridView.DataSource = this.PatientData(this.patientId, this.visitId).Tables[0].DefaultView;
                    }
                }
            }
            catch
            {
            }
        }

        private void ToolStripZJTJ_Click(object sender, EventArgs e)
        {
            try
            {
                FormTime time = new FormTime {
                    DateType = true
                };
                time.ShowDialog();
                if (!time.Quit)
                {
                    DateTime dtime = time.Dtime;
                    string str = dtime.Date.ToShortDateString() + " " + GVars.IniFile.ReadString("Exchange_Work_Time", "EW_Z", string.Empty) + ":30";
                    string str2 = dtime.AddDays(-1.0).ToShortDateString() + " " + GVars.IniFile.ReadString("Exchange_Work_Time", "EW_Z", string.Empty) + ":31";
                    string str3 = dtime.Date.ToShortDateString() + " " + GVars.IniFile.ReadString("Exchange_Work_Time", "EW_Z", string.Empty) + ":00";
                    string str4 = dtime.Date.AddDays(-1.0).ToShortDateString() + " " + GVars.IniFile.ReadString("Exchange_Work_Time", "EW_W", string.Empty) + ":00";
                    string sqlSel = "select DRUG_AMOUNT,FOOD_AMOUNT from NURSING_RECORD ";
                    string str6 = sqlSel;
                    sqlSel = ((((str6 + " where NURSING_DATE >= to_date('" + str2 + "','yyyy-mm-dd hh24:mi:ss') and NURSING_DATE < to_date('" + str + "','yyyy-mm-dd hh24:mi:ss')") + " and PATIENT_ID = " + SqlManager.SqlConvert(this.patientId)) + "AND VISIT_ID = " + SqlManager.SqlConvert(this.visitId)) + " and NURSING_DATE <> to_date('" + str3 + "','yyyy-mm-dd hh24:mi:ss')") + " and NURSING_DATE <> to_date('" + str4 + "','yyyy-mm-dd hh24:mi:ss')";
                    DataTable dt = GVars.OracleAccess.SelectData(sqlSel).Tables[0];
                    this.SaveInfo("2", this.HZdata(dt), dtime);
                    if (this.toselect)
                    {
                        this.PatientGridView.DataSource = this.PatientData(this.patientId, this.visitId, this.dtRngStart.Value, this.dtRngEnd.Value).Tables[0].DefaultView;
                    }
                    if (!this.toselect)
                    {
                        this.PatientGridView.DataSource = this.PatientData(this.patientId, this.visitId).Tables[0].DefaultView;
                    }
                }
            }
            catch
            {
            }
        }

        private void txtBedLabel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if ((e.KeyCode == Keys.Enter) && (this.txtBedLabel.Text.Trim().Length != 0))
                {
                    this.initDisp();
                    this.dsPatient = this.patientCom.GetInpPatientInfo_FromBedLabel(this.txtBedLabel.Text.Trim(), GVars.User.DeptCode);
                    if (((this.dsPatient == null) || (this.dsPatient.Tables.Count == 0)) || (this.dsPatient.Tables[0].Rows.Count == 0))
                    {
                        this.dsPatient = this.patientCom.GetPatientInfo_FromID(this.txtBedLabel.Text.Trim());
                        if (((this.dsPatient == null) || (this.dsPatient.Tables.Count == 0)) || (this.dsPatient.Tables[0].Rows.Count == 0))
                        {
                            MessageBox.Show("该病人不存在");
                            return;
                        }
                    }
                    this._userRight = GVars.User.GetUserFrmRight(base._id);
                    this.enabled = (this._userRight.IndexOf("2") >= 0) || GVars.User.Name.Equals(GVars.User.Name);
                    this.showPatientInfo();
                    this.PatientGridView.DataSource = this.PatientData(this.patientId, this.visitId, this.dtRngStart.Value, this.dtRngEnd.Value).Tables[0].DefaultView;
                    this.toselect = true;
                }
            }
            catch (Exception exception)
            {
                Error.ErrProc(exception);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}

