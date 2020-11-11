namespace HISPlus
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public class FormExchangeWork : FormDo
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private MaskedTextBox C1;
        private MaskedTextBox C10;
        private MaskedTextBox C11;
        private MaskedTextBox C12;
        private MaskedTextBox C2;
        private MaskedTextBox C3;
        private MaskedTextBox C4;
        private MaskedTextBox C5;
        private MaskedTextBox C6;
        private MaskedTextBox C7;
        private MaskedTextBox C8;
        private MaskedTextBox C9;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private IContainer components = null;
        private DataGridView dataGridView1;
        private DateTimePicker dateTimePicker1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private Label label1;
        private Label label11;
        private Label label13;
        private Label label15;
        private Label label17;
        private Label label19;
        private Label label2;
        private Label label21;
        private Label label23;
        private Label label28;
        private Label label29;
        private Label label3;
        private Label label30;
        private Label label35;
        private Label label36;
        private Label label37;
        private Label label38;
        private Label label4;
        private Label label43;
        private Label label44;
        private Label label45;
        private Label label46;
        private Label label5;
        private Label label51;
        private Label label52;
        private Label label53;
        private Label label58;
        private Label label59;
        private Label label6;
        private Label label60;
        private Label label61;
        private Label label66;
        private Label label67;
        private Label label68;
        private Label label69;
        private Label label7;
        private Label label71;
        private Label label73;
        private Label label75;
        private Label label9;
        private MaskedTextBox maskedTextBox1;
        private MaskedTextBox maskedTextBox2;
        private MaskedTextBox maskedTextBox3;
        private MaskedTextBox maskedTextBox4;
        private MaskedTextBox maskedTextBox5;
        private MaskedTextBox maskedTextBox6;
        private object obj = null;
        private MaskedTextBox W1;
        private MaskedTextBox W10;
        private MaskedTextBox W11;
        private MaskedTextBox W12;
        private MaskedTextBox W2;
        private MaskedTextBox W3;
        private MaskedTextBox W4;
        private MaskedTextBox W5;
        private MaskedTextBox W6;
        private MaskedTextBox W7;
        private MaskedTextBox W8;
        private MaskedTextBox W9;
        private string WardCode = string.Empty;
        private MaskedTextBox Z1;
        private MaskedTextBox Z10;
        private MaskedTextBox Z11;
        private MaskedTextBox Z12;
        private MaskedTextBox Z2;
        private MaskedTextBox Z3;
        private MaskedTextBox Z4;
        private MaskedTextBox Z5;
        private MaskedTextBox Z6;
        private MaskedTextBox Z7;
        private MaskedTextBox Z8;
        private MaskedTextBox Z9;

        public FormExchangeWork()
        {
            base._id = "00021";
            base._guid = "5E041A9A-5CF8-4164-BC6D-3168A685032E";
            this.InitializeComponent();
            this.WardCode = GVars.IniFile.ReadString("APP", "WARD_CODE", string.Empty);
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = this.GetNursingInfo().Tables[0].DefaultView;
            DateTime time = new DateTime();
            TimeSpan span = new TimeSpan(1, 0, 0, 0);
            time = this.dateTimePicker1.Value.Subtract(span);
            if (DateTime.Now.Hour < 8)
            {
                this.dateTimePicker1.Value = time;
            }
            this.ReadStatistics_Z(this.GetNursingFlowInfo(1));
            this.ReadStatistics_W(this.GetNursingFlowInfo(3));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = this.GetNursingInfo().Tables[0].DefaultView;
            this.ReadStatistics_Z(this.GetNursingFlowInfo(1));
            this.ReadStatistics_W(this.GetNursingFlowInfo(3));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Statistics_Z();
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Statistics_W();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.FlowSaveData_Z();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.FlowSaveData_W();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定要删除该记录吗？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    string sql = "DELETE FROM ExchangeWork ";
                    sql = (sql + "WHERE ") + "DEPT_CODE = '" + GVars.User.DeptCode + "'";
                    DateTime date = DateTime.Parse(this.dataGridView1["Column5", this.dataGridView1.CurrentCell.RowIndex].Value.ToString());
                    sql = (sql + "AND Record_Time = " + SqlManager.GetOraDbDate_Short(date)) + "AND ORDER_NO = " + int.Parse(this.dataGridView1["Column7", this.dataGridView1.CurrentCell.RowIndex].Value.ToString());
                    GVars.OracleAccess.ExecuteNoQuery(sql);
                    this.dataGridView1.AutoGenerateColumns = false;
                    this.dataGridView1.DataSource = this.GetNursingInfo().Tables[0].DefaultView;
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

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataRow[] rowArray = null;
                rowArray = this.GetNursingInfo().Tables[0].Select();
                string str = null;
                StreamReader reader = new StreamReader(@"Template\交班记录单.htm", Encoding.Default);
                str = reader.ReadToEnd();
                reader.Close();
                string[] strArray = str.Split(new char[] { '⊙' });
                string str2 = strArray[0].ToString().Replace("[#KS#]", GVars.User.DeptName).Replace("[#DATETIME#]", this.dateTimePicker1.Text).Replace("[#Z1]", this.Z1.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#Z2]", this.Z2.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#Z3]", this.Z3.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#Z4]", this.Z4.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#Z5]", this.Z5.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#Z6]", this.Z6.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#Z7]", this.Z7.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#Z8]", this.Z8.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#Z9]", this.Z9.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#Z10]", this.Z10.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#Z11]", this.Z11.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#Z12]", this.Z12.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#W1]", this.W1.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#W2]", this.W2.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#W3]", this.W3.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#W4]", this.W4.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#W5]", this.W5.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#W6]", this.W6.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#W7]", this.W7.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#W8]", this.W8.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#W9]", this.W9.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#W10]", this.W10.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#W11]", this.W11.Text.Replace("_", "").Replace(" ", "").Trim()).Replace("[#W12]", this.W12.Text.Replace("_", "").Replace(" ", "").Trim());
                string str3 = null;
                for (int i = 0; i < rowArray.Length; i++)
                {
                    string str4 = strArray[1].ToString().Replace("[#MAIN1]", rowArray[i]["INP_DIAGNOSIS_1"].ToString()).Replace("[#MAIN2]", rowArray[i]["CIRCUMSTANCE_1"].ToString()).Replace("[#MAIN3]", rowArray[i]["CIRCUMSTANCE_3"].ToString());
                    str3 = str3 + str4;
                }
                string str5 = strArray[2].ToString();
                new FormPrint(str2 + str3 + str5).ShowDialog();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DateTime date = this.dateTimePicker1.Value;
            try
            {
                string str2;
                string str = "";
                try
                {
                    str = this.dataGridView1.Rows[e.RowIndex].Cells["Column5"].Value.ToString();
                }
                catch
                {
                }
                if ((str.Length < 1) && (this.obj != null))
                {
                    int num = this.GetNursingCount() + 1;
                    str2 = "insert into ExchangeWork (PATIENT_ID,VISIT_ID,DEPT_CODE,Record_Time,ORDER_NO,INP_DIAGNOSIS_2,";
                    str2 = str2 + this.dataGridView1.Columns[e.ColumnIndex].DataPropertyName;
                    if (this.dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "CIRCUMSTANCE_1")
                    {
                        str2 = str2 + ",USER_NAME_1)";
                    }
                    else if (this.dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "CIRCUMSTANCE_3")
                    {
                        str2 = str2 + ",USER_NAME_3)";
                    }
                    str2 = (((((str2 + ")") + "values( '-" + num.ToString() + "',0,") + "'" + GVars.User.DeptCode + "'") + "," + SqlManager.GetOraDbDate_Short(date)) + "," + num) + ",'" + date.ToShortDateString() + "'";
                    str2 = string.Concat(new object[] { str2, ",'", this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, "'" });
                    if ((this.dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "CIRCUMSTANCE_1") || (this.dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "CIRCUMSTANCE_3"))
                    {
                        str2 = str2 + ",'" + GVars.User.Name + "'";
                    }
                    str2 = str2 + ")";
                    GVars.OracleAccess.ExecuteNoQuery(str2);
                    this.dataGridView1.Rows[e.RowIndex].Cells["Column5"].Value = date;
                    this.dataGridView1.Rows[e.RowIndex].Cells["Column7"].Value = num.ToString();
                }
                else
                {
                    str2 = "UPDATE ExchangeWork ";
                    object obj2 = str2 + "SET ";
                    str2 = string.Concat(new object[] { obj2, this.dataGridView1.Columns[e.ColumnIndex].DataPropertyName, " = '", this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, "'" });
                    if (this.dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "CIRCUMSTANCE_1")
                    {
                        str2 = str2 + ",USER_NAME_1 = '" + GVars.User.Name + "'";
                    }
                    else if (this.dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "CIRCUMSTANCE_3")
                    {
                        str2 = str2 + ",USER_NAME_3 = '" + GVars.User.Name + "'";
                    }
                    str2 = (str2 + "WHERE ") + "DEPT_CODE = '" + GVars.User.DeptCode + "'";
                    DateTime time2 = DateTime.Parse(this.dataGridView1.Rows[e.RowIndex].Cells["Column5"].Value.ToString());
                    str2 = (str2 + "AND Record_Time = " + SqlManager.GetOraDbDate_Short(time2)) + "AND ORDER_NO = " + int.Parse(this.dataGridView1.Rows[e.RowIndex].Cells["Column7"].Value.ToString());
                    GVars.OracleAccess.ExecuteNoQuery(str2);
                }
                this.obj = null;
            }
            catch
            {
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            this.obj = e.FormattedValue;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
            {
                row.Cells[0].Value = row.Index + 1;
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

        private void FlowSaveData_W()
        {
            string str;
            DateTime date = this.dateTimePicker1.Value;
            if (this.GetNursingFlowInt(3) < 1)
            {
                str = "insert into PatientFlow(DEPT_CODE,Record_Time,SERIAL_NO,DISCHARGE_Hospital,Enter_hospital,ReTurn,Turn_section,Death,Becriticallyill,Surgical_operation,Givebirthbaby,Infant,Lend_bed,Accompany_bed,Total_number )";
                str = ((((((((((((((((str + "values( ") + "'" + GVars.User.DeptCode + "'") + "," + SqlManager.GetOraDbDate_Short(date)) + ",3") + "," + int.Parse(this.W1.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.W4.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.W2.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.W5.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.W3.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.W6.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.W7.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.W8.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.W9.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.W10.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.W11.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.W12.Text.Replace("_", "").Replace(" ", "").Trim())) + ")";
                GVars.OracleAccess.ExecuteNoQuery(str);
            }
            else
            {
                str = "UPDATE PatientFlow ";
                object obj2 = str + "SET ";
                obj2 = string.Concat(new object[] { obj2, "DISCHARGE_Hospital = '", int.Parse(this.W1.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Enter_hospital = '", int.Parse(this.W4.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "ReTurn = '", int.Parse(this.W2.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Turn_section = '", int.Parse(this.W5.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Death = '", int.Parse(this.W3.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Becriticallyill = '", int.Parse(this.W6.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Surgical_operation = '", int.Parse(this.W7.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Givebirthbaby = '", int.Parse(this.W8.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Infant = '", int.Parse(this.W9.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Lend_bed = '", int.Parse(this.W10.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Accompany_bed = '", int.Parse(this.W11.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                str = ((string.Concat(new object[] { obj2, "Total_number = '", int.Parse(this.W12.Text.Replace("_", "").Replace(" ", "").Trim()), "' " }) + "WHERE " + "SERIAL_NO = 3") + "AND DEPT_CODE = '" + GVars.User.DeptCode + "'") + "AND Record_Time = " + SqlManager.GetOraDbDate_Short(date);
                GVars.OracleAccess.ExecuteNoQuery(str);
            }
        }

        private void FlowSaveData_Z()
        {
            string str;
            DateTime date = this.dateTimePicker1.Value;
            if (this.GetNursingFlowInt(1) < 1)
            {
                str = "insert into PatientFlow(DEPT_CODE,Record_Time,SERIAL_NO,DISCHARGE_Hospital,Enter_hospital,ReTurn,Turn_section,Death,Becriticallyill,Surgical_operation,Givebirthbaby,Infant,Lend_bed,Accompany_bed,Total_number )";
                str = ((((((((((((((((str + "values( ") + "'" + GVars.User.DeptCode + "'") + "," + SqlManager.GetOraDbDate_Short(date)) + ",1") + "," + int.Parse(this.Z1.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.Z4.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.Z2.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.Z5.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.Z3.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.Z6.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.Z7.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.Z8.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.Z9.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.Z10.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.Z11.Text.Replace("_", "").Replace(" ", "").Trim())) + "," + int.Parse(this.Z12.Text.Replace("_", "").Replace(" ", "").Trim())) + ")";
                GVars.OracleAccess.ExecuteNoQuery(str);
            }
            else
            {
                str = "UPDATE PatientFlow ";
                object obj2 = str + "SET ";
                obj2 = string.Concat(new object[] { obj2, "DISCHARGE_Hospital = '", int.Parse(this.Z1.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Enter_hospital = '", int.Parse(this.Z4.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "ReTurn = '", int.Parse(this.Z2.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Turn_section = '", int.Parse(this.Z5.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Death = '", int.Parse(this.Z3.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Becriticallyill = '", int.Parse(this.Z6.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Surgical_operation = '", int.Parse(this.Z7.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Givebirthbaby = '", int.Parse(this.Z8.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Infant = '", int.Parse(this.Z9.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Lend_bed = '", int.Parse(this.Z10.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                obj2 = string.Concat(new object[] { obj2, "Accompany_bed = '", int.Parse(this.Z11.Text.Replace("_", "").Replace(" ", "").Trim()), "'," });
                str = ((string.Concat(new object[] { obj2, "Total_number = '", int.Parse(this.Z12.Text.Replace("_", "").Replace(" ", "").Trim()), "' " }) + "WHERE " + "SERIAL_NO = 1") + "AND DEPT_CODE = '" + GVars.User.DeptCode + "'") + "AND Record_Time = " + SqlManager.GetOraDbDate_Short(date);
                GVars.OracleAccess.ExecuteNoQuery(str);
            }
        }

        private int GetNursingCount()
        {
            string sqlSel = "SELECT MAX(ORDER_NO) FROM ExchangeWork ";
            sqlSel = ((sqlSel + "WHERE ") + "DEPT_CODE = '" + GVars.User.DeptCode + "'") + "AND Record_Time = " + SqlManager.GetOraDbDate_Short(this.dateTimePicker1.Value);
            DataSet set = GVars.OracleAccess.SelectData(sqlSel);
            int num = 0;
            try
            {
                if (set.Tables[0].Rows.Count > 0)
                {
                    num = int.Parse(set.Tables[0].Rows[0][0].ToString());
                }
            }
            catch
            {
            }
            return num;
        }

        private DataTable GetNursingFlowInfo(int SERIAL)
        {
            DateTime date = this.dateTimePicker1.Value;
            string sqlSel = "SELECT DISCHARGE_Hospital,Enter_hospital,ReTurn,Turn_section,Death,Becriticallyill,Surgical_operation,Givebirthbaby,Infant,Lend_bed,Accompany_bed,Total_number  FROM PatientFlow ";
            sqlSel = (((sqlSel + "WHERE ") + "SERIAL_NO = " + SERIAL) + "AND DEPT_CODE = '" + GVars.User.DeptCode + "'") + "AND Record_Time = " + SqlManager.GetOraDbDate_Short(date);
            return GVars.OracleAccess.SelectData(sqlSel).Tables[0];
        }

        private int GetNursingFlowInt(int SERIAL)
        {
            DateTime date = this.dateTimePicker1.Value;
            string sqlSel = "SELECT COUNT(*) FROM PatientFlow ";
            sqlSel = (((sqlSel + "WHERE ") + "SERIAL_NO =" + SERIAL) + "AND DEPT_CODE = '" + GVars.User.DeptCode + "'") + "AND Record_Time = " + SqlManager.GetOraDbDate_Short(date);
            DataTable table = GVars.OracleAccess.SelectData(sqlSel).Tables[0];
            return int.Parse(table.Rows[0][0].ToString());
        }

        private DataSet GetNursingInfo()
        {
            string sqlSel = "SELECT ORDER_NO,INP_DIAGNOSIS_2,CIRCUMSTANCE_1,CIRCUMSTANCE_2,CIRCUMSTANCE_3,INP_DIAGNOSIS_1 FROM ExchangeWork ";
            sqlSel = (((sqlSel + "WHERE ") + "DEPT_CODE = '" + GVars.User.DeptCode + "'") + "AND Record_Time = " + SqlManager.GetOraDbDate_Short(this.dateTimePicker1.Value)) + "ORDER BY " + " ORDER_NO ASC";
            return GVars.OracleAccess.SelectData(sqlSel);
        }

        private int GetNursingInt(string TIME1, string TIME2, string type, bool bl)
        {
            string sqlSel = string.Empty;
            DateTime time = new DateTime();
            DateTime time2 = new DateTime();
            if (bl)
            {
                time = this.dateTimePicker1.Value;
                time2 = this.dateTimePicker1.Value;
            }
            else
            {
                TimeSpan span = new TimeSpan(1, 0, 0, 0);
                time = this.dateTimePicker1.Value;
                time2 = this.dateTimePicker1.Value.Subtract(span);
            }
            string oraDbDate = SqlManager.GetOraDbDate(time2.ToShortDateString() + " " + TIME1);
            string str3 = SqlManager.GetOraDbDate(time.ToShortDateString() + " " + TIME2);
            string str4 = (((sqlSel + "SELECT count(*) " + " FROM ") + "ADT_LOG " + "WHERE ") + "WARD_CODE = '" + GVars.User.DeptCode + "'") + " AND ACTION = '" + type + "'";
            sqlSel = str4 + "AND (LOG_DATE_TIME>=" + oraDbDate + ") and (LOG_DATE_TIME<=" + str3 + ")";
            DataTable table = GVars.OracleAccess.SelectData(sqlSel).Tables[0];
            return int.Parse(table.Rows[0][0].ToString());
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            this.label1 = new Label();
            this.dateTimePicker1 = new DateTimePicker();
            this.button1 = new Button();
            this.groupBox1 = new GroupBox();
            this.groupBox5 = new GroupBox();
            this.button7 = new Button();
            this.maskedTextBox6 = new MaskedTextBox();
            this.W12 = new MaskedTextBox();
            this.W11 = new MaskedTextBox();
            this.W10 = new MaskedTextBox();
            this.label51 = new Label();
            this.label52 = new Label();
            this.label53 = new Label();
            this.label58 = new Label();
            this.W9 = new MaskedTextBox();
            this.W8 = new MaskedTextBox();
            this.W7 = new MaskedTextBox();
            this.label59 = new Label();
            this.label75 = new Label();
            this.maskedTextBox5 = new MaskedTextBox();
            this.label60 = new Label();
            this.label61 = new Label();
            this.label66 = new Label();
            this.label67 = new Label();
            this.W6 = new MaskedTextBox();
            this.W3 = new MaskedTextBox();
            this.W5 = new MaskedTextBox();
            this.W2 = new MaskedTextBox();
            this.W4 = new MaskedTextBox();
            this.W1 = new MaskedTextBox();
            this.label68 = new Label();
            this.label69 = new Label();
            this.button4 = new Button();
            this.label6 = new Label();
            this.groupBox4 = new GroupBox();
            this.button6 = new Button();
            this.maskedTextBox4 = new MaskedTextBox();
            this.C12 = new MaskedTextBox();
            this.C11 = new MaskedTextBox();
            this.C10 = new MaskedTextBox();
            this.label28 = new Label();
            this.C9 = new MaskedTextBox();
            this.C8 = new MaskedTextBox();
            this.C7 = new MaskedTextBox();
            this.label29 = new Label();
            this.label30 = new Label();
            this.label35 = new Label();
            this.C6 = new MaskedTextBox();
            this.C3 = new MaskedTextBox();
            this.maskedTextBox3 = new MaskedTextBox();
            this.C5 = new MaskedTextBox();
            this.C2 = new MaskedTextBox();
            this.C4 = new MaskedTextBox();
            this.C1 = new MaskedTextBox();
            this.label73 = new Label();
            this.label36 = new Label();
            this.label37 = new Label();
            this.label38 = new Label();
            this.label43 = new Label();
            this.label44 = new Label();
            this.label45 = new Label();
            this.label46 = new Label();
            this.label4 = new Label();
            this.button3 = new Button();
            this.groupBox3 = new GroupBox();
            this.button5 = new Button();
            this.maskedTextBox2 = new MaskedTextBox();
            this.Z12 = new MaskedTextBox();
            this.Z11 = new MaskedTextBox();
            this.Z10 = new MaskedTextBox();
            this.Z9 = new MaskedTextBox();
            this.Z8 = new MaskedTextBox();
            this.Z7 = new MaskedTextBox();
            this.Z6 = new MaskedTextBox();
            this.Z3 = new MaskedTextBox();
            this.Z5 = new MaskedTextBox();
            this.Z2 = new MaskedTextBox();
            this.Z4 = new MaskedTextBox();
            this.Z1 = new MaskedTextBox();
            this.maskedTextBox1 = new MaskedTextBox();
            this.label19 = new Label();
            this.label13 = new Label();
            this.label7 = new Label();
            this.label71 = new Label();
            this.label23 = new Label();
            this.label17 = new Label();
            this.label11 = new Label();
            this.label5 = new Label();
            this.label21 = new Label();
            this.label15 = new Label();
            this.label9 = new Label();
            this.label3 = new Label();
            this.button2 = new Button();
            this.label2 = new Label();
            this.groupBox2 = new GroupBox();
            this.dataGridView1 = new DataGridView();
            this.button8 = new Button();
            this.button9 = new Button();
            this.Column8 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            base.SuspendLayout();
            this.label1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(7, 0x1ed);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "记录日期：";
            this.dateTimePicker1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.dateTimePicker1.Location = new Point(0x4a, 0x1e9);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new Size(0x74, 0x15);
            this.dateTimePicker1.TabIndex = 1;
            this.button1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.button1.Location = new Point(0xc4, 0x1e9);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x47, 0x15);
            this.button1.TabIndex = 2;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(700, 0x88);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "病人转动情况";
            this.groupBox5.Controls.Add(this.button7);
            this.groupBox5.Controls.Add(this.maskedTextBox6);
            this.groupBox5.Controls.Add(this.W12);
            this.groupBox5.Controls.Add(this.W11);
            this.groupBox5.Controls.Add(this.W10);
            this.groupBox5.Controls.Add(this.label51);
            this.groupBox5.Controls.Add(this.label52);
            this.groupBox5.Controls.Add(this.label53);
            this.groupBox5.Controls.Add(this.label58);
            this.groupBox5.Controls.Add(this.W9);
            this.groupBox5.Controls.Add(this.W8);
            this.groupBox5.Controls.Add(this.W7);
            this.groupBox5.Controls.Add(this.label59);
            this.groupBox5.Controls.Add(this.label75);
            this.groupBox5.Controls.Add(this.maskedTextBox5);
            this.groupBox5.Controls.Add(this.label60);
            this.groupBox5.Controls.Add(this.label61);
            this.groupBox5.Controls.Add(this.label66);
            this.groupBox5.Controls.Add(this.label67);
            this.groupBox5.Controls.Add(this.W6);
            this.groupBox5.Controls.Add(this.W3);
            this.groupBox5.Controls.Add(this.W5);
            this.groupBox5.Controls.Add(this.W2);
            this.groupBox5.Controls.Add(this.W4);
            this.groupBox5.Controls.Add(this.W1);
            this.groupBox5.Controls.Add(this.label68);
            this.groupBox5.Controls.Add(this.label69);
            this.groupBox5.Controls.Add(this.button4);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Location = new Point(0x1d2, 13);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0xe5, 0x75);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "晚班";
            this.button7.Location = new Point(0xb2, 10);
            this.button7.Name = "button7";
            this.button7.Size = new Size(0x2d, 0x17);
            this.button7.TabIndex = 0x10;
            this.button7.Text = "确定";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new EventHandler(this.button7_Click);
            this.maskedTextBox6.Location = new Point(0x45, 11);
            this.maskedTextBox6.Mask = "90:00";
            this.maskedTextBox6.Name = "maskedTextBox6";
            this.maskedTextBox6.Size = new Size(0x27, 0x15);
            this.maskedTextBox6.TabIndex = 15;
            this.maskedTextBox6.Text = "0800";
            this.maskedTextBox6.ValidatingType = typeof(DateTime);
            this.W12.Location = new Point(0xc6, 0x5d);
            this.W12.Mask = "999";
            this.W12.Name = "W12";
            this.W12.Size = new Size(0x1a, 0x15);
            this.W12.TabIndex = 14;
            this.W12.Text = "0";
            this.W11.Location = new Point(0x76, 0x5d);
            this.W11.Mask = "999";
            this.W11.Name = "W11";
            this.W11.Size = new Size(0x1a, 0x15);
            this.W11.TabIndex = 14;
            this.W11.Text = "0";
            this.W10.Location = new Point(0x2a, 0x5d);
            this.W10.Mask = "999";
            this.W10.Name = "W10";
            this.W10.Size = new Size(0x1a, 0x15);
            this.W10.TabIndex = 14;
            this.W10.Text = "0";
            this.label51.AutoSize = true;
            this.label51.Location = new Point(160, 0x4f);
            this.label51.Name = "label51";
            this.label51.Size = new Size(0x1d, 12);
            this.label51.TabIndex = 9;
            this.label51.Text = "婴儿";
            this.label52.AutoSize = true;
            this.label52.Location = new Point(160, 0x3d);
            this.label52.Name = "label52";
            this.label52.Size = new Size(0x1d, 12);
            this.label52.TabIndex = 9;
            this.label52.Text = "病危";
            this.label53.AutoSize = true;
            this.label53.Location = new Point(160, 0x2a);
            this.label53.Name = "label53";
            this.label53.Size = new Size(0x1d, 12);
            this.label53.TabIndex = 9;
            this.label53.Text = "死亡";
            this.label58.AutoSize = true;
            this.label58.Location = new Point(0x53, 0x60);
            this.label58.Name = "label58";
            this.label58.Size = new Size(0x1d, 12);
            this.label58.TabIndex = 7;
            this.label58.Text = "陪床";
            this.W9.Location = new Point(0xc6, 0x49);
            this.W9.Mask = "999";
            this.W9.Name = "W9";
            this.W9.Size = new Size(0x1a, 0x15);
            this.W9.TabIndex = 14;
            this.W9.Text = "0";
            this.W8.Location = new Point(0x76, 0x49);
            this.W8.Mask = "999";
            this.W8.Name = "W8";
            this.W8.Size = new Size(0x1a, 0x15);
            this.W8.TabIndex = 14;
            this.W8.Text = "0";
            this.W7.Location = new Point(0x2a, 0x49);
            this.W7.Mask = "999";
            this.W7.Name = "W7";
            this.W7.Size = new Size(0x1a, 0x15);
            this.W7.TabIndex = 14;
            this.W7.Text = "0";
            this.label59.AutoSize = true;
            this.label59.Location = new Point(0x53, 0x4f);
            this.label59.Name = "label59";
            this.label59.Size = new Size(0x1d, 12);
            this.label59.TabIndex = 7;
            this.label59.Text = "生产";
            this.label75.AutoSize = true;
            this.label75.Location = new Point(160, 0x60);
            this.label75.Name = "label75";
            this.label75.Size = new Size(0x29, 12);
            this.label75.TabIndex = 7;
            this.label75.Text = "总人数";
            this.maskedTextBox5.Location = new Point(8, 12);
            this.maskedTextBox5.Mask = "90:00";
            this.maskedTextBox5.Name = "maskedTextBox5";
            this.maskedTextBox5.Size = new Size(0x27, 0x15);
            this.maskedTextBox5.TabIndex = 12;
            this.maskedTextBox5.Text = "1800";
            this.maskedTextBox5.ValidatingType = typeof(DateTime);
            this.label60.AutoSize = true;
            this.label60.Location = new Point(0x53, 0x3d);
            this.label60.Name = "label60";
            this.label60.Size = new Size(0x1d, 12);
            this.label60.TabIndex = 7;
            this.label60.Text = "转入";
            this.label61.AutoSize = true;
            this.label61.Location = new Point(0x53, 0x2a);
            this.label61.Name = "label61";
            this.label61.Size = new Size(0x1d, 12);
            this.label61.TabIndex = 7;
            this.label61.Text = "转出";
            this.label66.AutoSize = true;
            this.label66.Location = new Point(7, 0x61);
            this.label66.Name = "label66";
            this.label66.Size = new Size(0x1d, 12);
            this.label66.TabIndex = 5;
            this.label66.Text = "借床";
            this.label67.AutoSize = true;
            this.label67.Location = new Point(7, 80);
            this.label67.Name = "label67";
            this.label67.Size = new Size(0x1d, 12);
            this.label67.TabIndex = 5;
            this.label67.Text = "手术";
            this.W6.Location = new Point(0xc6, 0x35);
            this.W6.Mask = "999";
            this.W6.Name = "W6";
            this.W6.Size = new Size(0x1a, 0x15);
            this.W6.TabIndex = 14;
            this.W6.Text = "0";
            this.W3.Location = new Point(0xc6, 0x23);
            this.W3.Mask = "999";
            this.W3.Name = "W3";
            this.W3.Size = new Size(0x1a, 0x15);
            this.W3.TabIndex = 14;
            this.W3.Text = "0";
            this.W5.Location = new Point(0x76, 0x35);
            this.W5.Mask = "999";
            this.W5.Name = "W5";
            this.W5.Size = new Size(0x1a, 0x15);
            this.W5.TabIndex = 14;
            this.W5.Text = "0";
            this.W2.Location = new Point(0x76, 0x23);
            this.W2.Mask = "999";
            this.W2.Name = "W2";
            this.W2.Size = new Size(0x1a, 0x15);
            this.W2.TabIndex = 14;
            this.W2.Text = "0";
            this.W4.Location = new Point(0x2a, 0x35);
            this.W4.Mask = "999";
            this.W4.Name = "W4";
            this.W4.Size = new Size(0x1a, 0x15);
            this.W4.TabIndex = 14;
            this.W4.Text = "0";
            this.W1.Location = new Point(0x2a, 0x23);
            this.W1.Mask = "999";
            this.W1.Name = "W1";
            this.W1.Size = new Size(0x1a, 0x15);
            this.W1.TabIndex = 14;
            this.W1.Text = "0";
            this.label68.AutoSize = true;
            this.label68.Location = new Point(7, 0x3e);
            this.label68.Name = "label68";
            this.label68.Size = new Size(0x1d, 12);
            this.label68.TabIndex = 5;
            this.label68.Text = "入院";
            this.label69.AutoSize = true;
            this.label69.Location = new Point(7, 0x2b);
            this.label69.Name = "label69";
            this.label69.Size = new Size(0x1d, 12);
            this.label69.TabIndex = 5;
            this.label69.Text = "出院";
            this.button4.Location = new Point(0x6f, 10);
            this.button4.Name = "button4";
            this.button4.Size = new Size(0x2a, 0x17);
            this.button4.TabIndex = 4;
            this.button4.Text = "统计";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new EventHandler(this.button4_Click);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(50, 0x13);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x11, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "至";
            this.groupBox4.Controls.Add(this.button6);
            this.groupBox4.Controls.Add(this.maskedTextBox4);
            this.groupBox4.Controls.Add(this.C12);
            this.groupBox4.Controls.Add(this.C11);
            this.groupBox4.Controls.Add(this.C10);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Controls.Add(this.C9);
            this.groupBox4.Controls.Add(this.C8);
            this.groupBox4.Controls.Add(this.C7);
            this.groupBox4.Controls.Add(this.label29);
            this.groupBox4.Controls.Add(this.label30);
            this.groupBox4.Controls.Add(this.label35);
            this.groupBox4.Controls.Add(this.C6);
            this.groupBox4.Controls.Add(this.C3);
            this.groupBox4.Controls.Add(this.maskedTextBox3);
            this.groupBox4.Controls.Add(this.C5);
            this.groupBox4.Controls.Add(this.C2);
            this.groupBox4.Controls.Add(this.C4);
            this.groupBox4.Controls.Add(this.C1);
            this.groupBox4.Controls.Add(this.label73);
            this.groupBox4.Controls.Add(this.label36);
            this.groupBox4.Controls.Add(this.label37);
            this.groupBox4.Controls.Add(this.label38);
            this.groupBox4.Controls.Add(this.label43);
            this.groupBox4.Controls.Add(this.label44);
            this.groupBox4.Controls.Add(this.label45);
            this.groupBox4.Controls.Add(this.label46);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Location = new Point(0xea, 13);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0xe7, 0x75);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "中班";
            this.button6.Location = new Point(180, 9);
            this.button6.Name = "button6";
            this.button6.Size = new Size(0x2d, 0x17);
            this.button6.TabIndex = 0x10;
            this.button6.Text = "确定";
            this.button6.UseVisualStyleBackColor = true;
            this.maskedTextBox4.Location = new Point(0x45, 11);
            this.maskedTextBox4.Mask = "90:00";
            this.maskedTextBox4.Name = "maskedTextBox4";
            this.maskedTextBox4.Size = new Size(0x27, 0x15);
            this.maskedTextBox4.TabIndex = 15;
            this.maskedTextBox4.Text = "0800";
            this.maskedTextBox4.ValidatingType = typeof(DateTime);
            this.C12.Location = new Point(0xc7, 0x5c);
            this.C12.Mask = "999";
            this.C12.Name = "C12";
            this.C12.Size = new Size(0x1a, 0x15);
            this.C12.TabIndex = 14;
            this.C12.Text = "0";
            this.C11.Location = new Point(0x76, 0x5c);
            this.C11.Mask = "999";
            this.C11.Name = "C11";
            this.C11.Size = new Size(0x1a, 0x15);
            this.C11.TabIndex = 14;
            this.C11.Text = "0";
            this.C10.Location = new Point(0x2a, 0x5c);
            this.C10.Mask = "999";
            this.C10.Name = "C10";
            this.C10.Size = new Size(0x1a, 0x15);
            this.C10.TabIndex = 14;
            this.C10.Text = "0";
            this.label28.AutoSize = true;
            this.label28.Location = new Point(160, 0x4f);
            this.label28.Name = "label28";
            this.label28.Size = new Size(0x1d, 12);
            this.label28.TabIndex = 9;
            this.label28.Text = "婴儿";
            this.C9.Location = new Point(0xc7, 0x48);
            this.C9.Mask = "999";
            this.C9.Name = "C9";
            this.C9.Size = new Size(0x1a, 0x15);
            this.C9.TabIndex = 14;
            this.C9.Text = "0";
            this.C8.Location = new Point(0x76, 0x48);
            this.C8.Mask = "999";
            this.C8.Name = "C8";
            this.C8.Size = new Size(0x1a, 0x15);
            this.C8.TabIndex = 14;
            this.C8.Text = "0";
            this.C7.Location = new Point(0x2a, 0x48);
            this.C7.Mask = "999";
            this.C7.Name = "C7";
            this.C7.Size = new Size(0x1a, 0x15);
            this.C7.TabIndex = 14;
            this.C7.Text = "0";
            this.label29.AutoSize = true;
            this.label29.Location = new Point(160, 0x3d);
            this.label29.Name = "label29";
            this.label29.Size = new Size(0x1d, 12);
            this.label29.TabIndex = 9;
            this.label29.Text = "病危";
            this.label30.AutoSize = true;
            this.label30.Location = new Point(160, 0x2a);
            this.label30.Name = "label30";
            this.label30.Size = new Size(0x1d, 12);
            this.label30.TabIndex = 9;
            this.label30.Text = "死亡";
            this.label35.AutoSize = true;
            this.label35.Location = new Point(0x53, 0x60);
            this.label35.Name = "label35";
            this.label35.Size = new Size(0x1d, 12);
            this.label35.TabIndex = 7;
            this.label35.Text = "陪床";
            this.C6.Location = new Point(0xc7, 0x34);
            this.C6.Mask = "999";
            this.C6.Name = "C6";
            this.C6.Size = new Size(0x1a, 0x15);
            this.C6.TabIndex = 14;
            this.C6.Text = "0";
            this.C3.Location = new Point(0xc7, 0x22);
            this.C3.Mask = "999";
            this.C3.Name = "C3";
            this.C3.Size = new Size(0x1a, 0x15);
            this.C3.TabIndex = 14;
            this.C3.Text = "0";
            this.maskedTextBox3.Location = new Point(8, 12);
            this.maskedTextBox3.Mask = "90:00";
            this.maskedTextBox3.Name = "maskedTextBox3";
            this.maskedTextBox3.Size = new Size(0x27, 0x15);
            this.maskedTextBox3.TabIndex = 12;
            this.maskedTextBox3.Text = "0800";
            this.maskedTextBox3.ValidatingType = typeof(DateTime);
            this.C5.Location = new Point(0x76, 0x34);
            this.C5.Mask = "999";
            this.C5.Name = "C5";
            this.C5.Size = new Size(0x1a, 0x15);
            this.C5.TabIndex = 14;
            this.C5.Text = "0";
            this.C2.Location = new Point(0x76, 0x22);
            this.C2.Mask = "999";
            this.C2.Name = "C2";
            this.C2.Size = new Size(0x1a, 0x15);
            this.C2.TabIndex = 14;
            this.C2.Text = "0";
            this.C4.Location = new Point(0x2a, 0x34);
            this.C4.Mask = "999";
            this.C4.Name = "C4";
            this.C4.Size = new Size(0x1a, 0x15);
            this.C4.TabIndex = 14;
            this.C4.Text = "0";
            this.C1.Location = new Point(0x2a, 0x22);
            this.C1.Mask = "999";
            this.C1.Name = "C1";
            this.C1.Size = new Size(0x1a, 0x15);
            this.C1.TabIndex = 14;
            this.C1.Text = "0";
            this.label73.AutoSize = true;
            this.label73.Location = new Point(160, 0x60);
            this.label73.Name = "label73";
            this.label73.Size = new Size(0x29, 12);
            this.label73.TabIndex = 7;
            this.label73.Text = "总人数";
            this.label36.AutoSize = true;
            this.label36.Location = new Point(0x53, 0x4f);
            this.label36.Name = "label36";
            this.label36.Size = new Size(0x1d, 12);
            this.label36.TabIndex = 7;
            this.label36.Text = "生产";
            this.label37.AutoSize = true;
            this.label37.Location = new Point(0x53, 0x3d);
            this.label37.Name = "label37";
            this.label37.Size = new Size(0x1d, 12);
            this.label37.TabIndex = 7;
            this.label37.Text = "转入";
            this.label38.AutoSize = true;
            this.label38.Location = new Point(0x53, 0x2a);
            this.label38.Name = "label38";
            this.label38.Size = new Size(0x1d, 12);
            this.label38.TabIndex = 7;
            this.label38.Text = "转出";
            this.label43.AutoSize = true;
            this.label43.Location = new Point(7, 0x61);
            this.label43.Name = "label43";
            this.label43.Size = new Size(0x1d, 12);
            this.label43.TabIndex = 5;
            this.label43.Text = "借床";
            this.label44.AutoSize = true;
            this.label44.Location = new Point(7, 80);
            this.label44.Name = "label44";
            this.label44.Size = new Size(0x1d, 12);
            this.label44.TabIndex = 5;
            this.label44.Text = "手术";
            this.label45.AutoSize = true;
            this.label45.Location = new Point(7, 0x3e);
            this.label45.Name = "label45";
            this.label45.Size = new Size(0x1d, 12);
            this.label45.TabIndex = 5;
            this.label45.Text = "入院";
            this.label46.AutoSize = true;
            this.label46.Location = new Point(7, 0x2b);
            this.label46.Name = "label46";
            this.label46.Size = new Size(0x1d, 12);
            this.label46.TabIndex = 5;
            this.label46.Text = "出院";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(50, 0x13);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "至";
            this.button3.Location = new Point(0x6f, 9);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x2a, 0x17);
            this.button3.TabIndex = 4;
            this.button3.Text = "统计";
            this.button3.UseVisualStyleBackColor = true;
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.maskedTextBox2);
            this.groupBox3.Controls.Add(this.Z12);
            this.groupBox3.Controls.Add(this.Z11);
            this.groupBox3.Controls.Add(this.Z10);
            this.groupBox3.Controls.Add(this.Z9);
            this.groupBox3.Controls.Add(this.Z8);
            this.groupBox3.Controls.Add(this.Z7);
            this.groupBox3.Controls.Add(this.Z6);
            this.groupBox3.Controls.Add(this.Z3);
            this.groupBox3.Controls.Add(this.Z5);
            this.groupBox3.Controls.Add(this.Z2);
            this.groupBox3.Controls.Add(this.Z4);
            this.groupBox3.Controls.Add(this.Z1);
            this.groupBox3.Controls.Add(this.maskedTextBox1);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label71);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new Point(6, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0xe3, 0x75);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "早班";
            this.button5.Location = new Point(0xb0, 9);
            this.button5.Name = "button5";
            this.button5.Size = new Size(0x2d, 0x17);
            this.button5.TabIndex = 0x10;
            this.button5.Text = "确定";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new EventHandler(this.button5_Click);
            this.maskedTextBox2.Location = new Point(70, 12);
            this.maskedTextBox2.Mask = "90:00";
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new Size(0x27, 0x15);
            this.maskedTextBox2.TabIndex = 15;
            this.maskedTextBox2.Text = "1800";
            this.maskedTextBox2.ValidatingType = typeof(DateTime);
            this.Z12.Location = new Point(0xc4, 0x5c);
            this.Z12.Mask = "999";
            this.Z12.Name = "Z12";
            this.Z12.Size = new Size(0x1a, 0x15);
            this.Z12.TabIndex = 14;
            this.Z12.Text = "0";
            this.Z11.Location = new Point(0x76, 0x5d);
            this.Z11.Mask = "999";
            this.Z11.Name = "Z11";
            this.Z11.Size = new Size(0x1a, 0x15);
            this.Z11.TabIndex = 14;
            this.Z11.Text = "0";
            this.Z10.Location = new Point(0x25, 0x5d);
            this.Z10.Mask = "999";
            this.Z10.Name = "Z10";
            this.Z10.Size = new Size(0x1a, 0x15);
            this.Z10.TabIndex = 14;
            this.Z10.Text = "0";
            this.Z9.Location = new Point(0xc4, 0x48);
            this.Z9.Mask = "999";
            this.Z9.Name = "Z9";
            this.Z9.Size = new Size(0x1a, 0x15);
            this.Z9.TabIndex = 14;
            this.Z9.Text = "0";
            this.Z8.Location = new Point(0x76, 0x49);
            this.Z8.Mask = "999";
            this.Z8.Name = "Z8";
            this.Z8.Size = new Size(0x1a, 0x15);
            this.Z8.TabIndex = 14;
            this.Z8.Text = "0";
            this.Z7.Location = new Point(0x25, 0x49);
            this.Z7.Mask = "999";
            this.Z7.Name = "Z7";
            this.Z7.Size = new Size(0x1a, 0x15);
            this.Z7.TabIndex = 14;
            this.Z7.Text = "0";
            this.Z6.Location = new Point(0xc4, 0x34);
            this.Z6.Mask = "999";
            this.Z6.Name = "Z6";
            this.Z6.Size = new Size(0x1a, 0x15);
            this.Z6.TabIndex = 14;
            this.Z6.Text = "0";
            this.Z3.Location = new Point(0xc4, 0x22);
            this.Z3.Mask = "999";
            this.Z3.Name = "Z3";
            this.Z3.Size = new Size(0x1a, 0x15);
            this.Z3.TabIndex = 14;
            this.Z3.Text = "0";
            this.Z5.Location = new Point(0x76, 0x35);
            this.Z5.Mask = "999";
            this.Z5.Name = "Z5";
            this.Z5.Size = new Size(0x1a, 0x15);
            this.Z5.TabIndex = 14;
            this.Z5.Text = "0";
            this.Z5.MaskInputRejected += new MaskInputRejectedEventHandler(this.Z5_MaskInputRejected);
            this.Z2.Location = new Point(0x76, 0x23);
            this.Z2.Mask = "999";
            this.Z2.Name = "Z2";
            this.Z2.Size = new Size(0x1a, 0x15);
            this.Z2.TabIndex = 14;
            this.Z2.Text = "0";
            this.Z4.Location = new Point(0x25, 0x35);
            this.Z4.Mask = "999";
            this.Z4.Name = "Z4";
            this.Z4.Size = new Size(0x1a, 0x15);
            this.Z4.TabIndex = 14;
            this.Z4.Text = "0";
            this.Z1.Location = new Point(0x25, 0x23);
            this.Z1.Mask = "999";
            this.Z1.Name = "Z1";
            this.Z1.Size = new Size(0x1a, 0x15);
            this.Z1.TabIndex = 14;
            this.Z1.Text = "0";
            this.maskedTextBox1.Location = new Point(9, 13);
            this.maskedTextBox1.Mask = "90:00";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new Size(0x27, 0x15);
            this.maskedTextBox1.TabIndex = 12;
            this.maskedTextBox1.Text = "0800";
            this.maskedTextBox1.ValidatingType = typeof(DateTime);
            this.label19.AutoSize = true;
            this.label19.Location = new Point(0x9d, 0x4f);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x1d, 12);
            this.label19.TabIndex = 9;
            this.label19.Text = "婴儿";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x9d, 0x3d);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x1d, 12);
            this.label13.TabIndex = 9;
            this.label13.Text = "病危";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x9d, 0x2a);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x1d, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "死亡";
            this.label71.AutoSize = true;
            this.label71.Location = new Point(0x9d, 0x60);
            this.label71.Name = "label71";
            this.label71.Size = new Size(0x29, 12);
            this.label71.TabIndex = 7;
            this.label71.Text = "总人数";
            this.label23.AutoSize = true;
            this.label23.Location = new Point(0x53, 0x60);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x1d, 12);
            this.label23.TabIndex = 7;
            this.label23.Text = "陪床";
            this.label17.AutoSize = true;
            this.label17.Location = new Point(0x53, 0x4f);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x1d, 12);
            this.label17.TabIndex = 7;
            this.label17.Text = "生产";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x53, 0x3d);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x1d, 12);
            this.label11.TabIndex = 7;
            this.label11.Text = "转入";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x53, 0x2a);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "转出";
            this.label21.AutoSize = true;
            this.label21.Location = new Point(7, 0x61);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x1d, 12);
            this.label21.TabIndex = 5;
            this.label21.Text = "借床";
            this.label15.AutoSize = true;
            this.label15.Location = new Point(7, 80);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x1d, 12);
            this.label15.TabIndex = 5;
            this.label15.Text = "手术";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(7, 0x3e);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x1d, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "入院";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(7, 0x2b);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "出院";
            this.button2.Location = new Point(0x73, 10);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x2a, 0x17);
            this.button2.TabIndex = 4;
            this.button2.Text = "统计";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x33, 20);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x11, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "至";
            this.groupBox2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new Point(3, 0x91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(700, 0x152);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "交班详情";
            this.dataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.dataGridView1.BackgroundColor = SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { this.Column6, this.Column1, this.Column2, this.Column3, this.Column4, this.Column5, this.Column7, this.Column8 });
            this.dataGridView1.Location = new Point(10, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.RowHeadersWidth = 0x18;
            this.dataGridView1.RowTemplate.Height = 0x17;
            this.dataGridView1.Size = new Size(0x2ad, 0x138);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            this.dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.button8.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.button8.Location = new Point(0x269, 0x1e7);
            this.button8.Name = "button8";
            this.button8.Size = new Size(0x4b, 0x17);
            this.button8.TabIndex = 5;
            this.button8.Text = "删除记录";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new EventHandler(this.button8_Click);
            this.button9.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.button9.Location = new Point(0x209, 0x1e7);
            this.button9.Name = "button9";
            this.button9.Size = new Size(0x4b, 0x17);
            this.button9.TabIndex = 6;
            this.button9.Text = "打印";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new EventHandler(this.button9_Click);
            this.Column8.DataPropertyName = "PATIENT_ID";
            this.Column8.HeaderText = "Column8";
            this.Column8.Name = "Column8";
            this.Column8.Visible = false;
            this.Column7.DataPropertyName = "ORDER_NO";
            this.Column7.HeaderText = "Column7";
            this.Column7.Name = "Column7";
            this.Column7.Visible = false;
            this.Column5.DataPropertyName = "INP_DIAGNOSIS_2";
            style.NullValue = null;
            this.Column5.DefaultCellStyle = style;
            this.Column5.HeaderText = "";
            this.Column5.Name = "Column5";
            this.Column5.Visible = false;
            this.Column4.DataPropertyName = "CIRCUMSTANCE_3";
            this.Column4.HeaderText = "晚班病情";
            this.Column4.Name = "Column4";
            this.Column4.Width = 250;
            this.Column3.DataPropertyName = "CIRCUMSTANCE_2";
            this.Column3.HeaderText = "中班病情";
            this.Column3.Name = "Column3";
            this.Column3.Visible = false;
            this.Column3.Width = 0xaf;
            this.Column2.DataPropertyName = "CIRCUMSTANCE_1";
            this.Column2.HeaderText = "早班病情";
            this.Column2.Name = "Column2";
            this.Column2.Width = 250;
            this.Column1.DataPropertyName = "INP_DIAGNOSIS_1";
            this.Column1.HeaderText = "床号|姓名|住院号|诊断|标记";
            this.Column1.Name = "Column1";
            this.Column1.Width = 110;
            this.Column6.HeaderText = "序号";
            this.Column6.Name = "Column6";
            this.Column6.Width = 30;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2c2, 0x200);
            base.Controls.Add(this.button9);
            base.Controls.Add(this.button8);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.dateTimePicker1);
            base.Controls.Add(this.label1);
            base.Name = "FormExchangeWork";
            this.Text = "交班记录";
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void ReadStatistics_W(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                this.W1.Text = dt.Rows[0][0].ToString();
                this.W4.Text = dt.Rows[0][1].ToString();
                this.W2.Text = dt.Rows[0][2].ToString();
                this.W5.Text = dt.Rows[0][3].ToString();
                this.W3.Text = dt.Rows[0][4].ToString();
                this.W6.Text = dt.Rows[0][5].ToString();
                this.W7.Text = dt.Rows[0][6].ToString();
                this.W8.Text = dt.Rows[0][7].ToString();
                this.W9.Text = dt.Rows[0][8].ToString();
                this.W10.Text = dt.Rows[0][9].ToString();
                this.W11.Text = dt.Rows[0][10].ToString();
                this.W12.Text = dt.Rows[0][11].ToString();
            }
            else
            {
                this.W1.Text = "0";
                this.W4.Text = "0";
                this.W2.Text = "0";
                this.W5.Text = "0";
                this.W3.Text = "0";
                this.W6.Text = "0";
                this.W7.Text = "0";
                this.W8.Text = "0";
                this.W9.Text = "0";
                this.W10.Text = "0";
                this.W11.Text = "0";
                this.W12.Text = "0";
            }
        }

        private void ReadStatistics_Z(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                this.Z1.Text = dt.Rows[0][0].ToString();
                this.Z4.Text = dt.Rows[0][1].ToString();
                this.Z2.Text = dt.Rows[0][2].ToString();
                this.Z5.Text = dt.Rows[0][3].ToString();
                this.Z3.Text = dt.Rows[0][4].ToString();
                this.Z6.Text = dt.Rows[0][5].ToString();
                this.Z7.Text = dt.Rows[0][6].ToString();
                this.Z8.Text = dt.Rows[0][7].ToString();
                this.Z9.Text = dt.Rows[0][8].ToString();
                this.Z10.Text = dt.Rows[0][9].ToString();
                this.Z11.Text = dt.Rows[0][10].ToString();
                this.Z12.Text = dt.Rows[0][11].ToString();
            }
            else
            {
                this.Z1.Text = "0";
                this.Z4.Text = "0";
                this.Z2.Text = "0";
                this.Z5.Text = "0";
                this.Z3.Text = "0";
                this.Z6.Text = "0";
                this.Z7.Text = "0";
                this.Z8.Text = "0";
                this.Z9.Text = "0";
                this.Z10.Text = "0";
                this.Z11.Text = "0";
                this.Z12.Text = "0";
            }
        }

        private void Statistics_C()
        {
            this.Z1.Text = this.GetNursingInt(this.maskedTextBox3.Text, this.maskedTextBox4.Text, "F", true).ToString();
            this.Z2.Text = this.GetNursingInt(this.maskedTextBox3.Text, this.maskedTextBox4.Text, "E", true).ToString();
            this.Z3.Text = this.GetNursingInt(this.maskedTextBox3.Text, this.maskedTextBox4.Text, "H", true).ToString();
            this.Z4.Text = this.GetNursingInt(this.maskedTextBox3.Text, this.maskedTextBox4.Text, "C", true).ToString();
            this.Z5.Text = this.GetNursingInt(this.maskedTextBox3.Text, this.maskedTextBox4.Text, "D", true).ToString();
            this.Z6.Text = this.GetNursingInt(this.maskedTextBox3.Text, this.maskedTextBox4.Text, "A", true).ToString();
        }

        private void Statistics_W()
        {
            this.W1.Text = this.GetNursingInt(this.maskedTextBox5.Text, this.maskedTextBox6.Text, "F", false).ToString();
            this.W2.Text = this.GetNursingInt(this.maskedTextBox5.Text, this.maskedTextBox6.Text, "E", false).ToString();
            this.W3.Text = this.GetNursingInt(this.maskedTextBox5.Text, this.maskedTextBox6.Text, "H", false).ToString();
            this.W4.Text = this.GetNursingInt(this.maskedTextBox5.Text, this.maskedTextBox6.Text, "C", false).ToString();
            this.W5.Text = this.GetNursingInt(this.maskedTextBox5.Text, this.maskedTextBox6.Text, "D", false).ToString();
            this.W6.Text = this.GetNursingInt(this.maskedTextBox5.Text, this.maskedTextBox6.Text, "A", false).ToString();
        }

        private void Statistics_Z()
        {
            this.Z1.Text = this.GetNursingInt(this.maskedTextBox1.Text, this.maskedTextBox2.Text, "F", true).ToString();
            this.Z2.Text = this.GetNursingInt(this.maskedTextBox1.Text, this.maskedTextBox2.Text, "E", true).ToString();
            this.Z3.Text = this.GetNursingInt(this.maskedTextBox1.Text, this.maskedTextBox2.Text, "H", true).ToString();
            this.Z4.Text = this.GetNursingInt(this.maskedTextBox1.Text, this.maskedTextBox2.Text, "C", true).ToString();
            this.Z5.Text = this.GetNursingInt(this.maskedTextBox1.Text, this.maskedTextBox2.Text, "D", true).ToString();
            this.Z6.Text = this.GetNursingInt(this.maskedTextBox1.Text, this.maskedTextBox2.Text, "A", true).ToString();
        }

        private void Z5_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
        }
    }
}

