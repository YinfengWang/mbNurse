namespace HISPlus
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class NursingRecNormal_A : FormDo
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private IContainer components = null;
        private DataSet dsPatient = null;
        private System.Windows.Forms.GroupBox GroupBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox GroupTree;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label lblAge;
        private Label lblDeptName;
        private Label lblDiagnose;
        private Label lblDocNo;
        private Label lblGender;
        private Label lblName;
        private DataSet myDs;
        private string NodeName = string.Empty;
        private Panel panel1;
        private DataSet patienDs;
        private DataTable patienDt;
        private PatientDbI patientCom;
        private string patientId = string.Empty;
        private TreeView treeView1;
        private TextBox txtBedLabel;
        private string visitId = string.Empty;

        public NursingRecNormal_A()
        {
            base._id = "00035";
            base._guid = "C37AB0C7-5551-87d6-B390-0834E1FDCCD1";
            this.InitializeComponent();
        }

        private void Auto_Scrolls(DataTable dt1, DataTable dt2)
        {
            if (dt1.Rows.Count > 0)
            {
                DataRow[] rowArray;
                int num;
                TextBox box;
                switch (dt1.Rows[0]["FLAG"].ToString())
                {
                    case "1":
                        for (num = 0; num < dt1.Rows.Count; num++)
                        {
                            RadioButton button = new RadioButton();
                            box = new TextBox();
                            button.Name = dt1.Rows[num]["ITEM_ID"].ToString();
                            button.Text = dt1.Rows[num]["ITEM_NAME"].ToString();
                            rowArray = this.patienDt.Select("ITEM_ID = '" + button.Name + "'");
                            if (rowArray.Length > 0)
                            {
                                button.Checked = true;
                                box.Text = rowArray[0]["ITEM_VALUE"].ToString();
                            }
                            this.panel1.Controls.Add(button);
                            button.Location = new Point(20, 5 + (num * 0x19));
                            box.Name = dt1.Rows[num]["ITEM_ID"].ToString();
                            box.Width = 120;
                            this.panel1.Controls.Add(box);
                            box.Location = new Point(140, 5 + (num * 0x19));
                        }
                        break;

                    case "2":
                        for (num = 0; num < dt1.Rows.Count; num++)
                        {
                            CheckBox box2 = new CheckBox();
                            box = new TextBox();
                            box2.Name = dt1.Rows[num]["ITEM_ID"].ToString();
                            box2.Text = dt1.Rows[num]["ITEM_NAME"].ToString();
                            rowArray = this.patienDt.Select("ITEM_ID = '" + box2.Name + "'");
                            if (rowArray.Length > 0)
                            {
                                box2.Checked = true;
                                box.Text = rowArray[0]["ITEM_VALUE"].ToString();
                            }
                            this.panel1.Controls.Add(box2);
                            box2.Location = new Point(20, 5 + (num * 0x19));
                            box.Name = dt1.Rows[num]["ITEM_ID"].ToString();
                            box.Width = 120;
                            this.panel1.Controls.Add(box);
                            box.Location = new Point(140, 5 + (num * 0x19));
                        }
                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count = this.panel1.Controls.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (this.panel1.Controls[i].GetType().ToString() == "System.Windows.Forms.TextBox")
                    {
                        this.SaveInfo(this.panel1.Controls[i].Name, this.panel1.Controls[i].Text);
                    }
                    if (this.panel1.Controls[i].GetType().ToString() == "System.Windows.Forms.DateTimePicker")
                    {
                        this.SaveInfo(this.panel1.Controls[i].Name, this.panel1.Controls[i].Text);
                    }
                }
            }
        }

        private void CreateTree(TreeNodeCollection TreeNodeCollection, int parentLength, string parentId)
        {
            DataView view = new DataView {
                Table = this.myDs.Tables[0],
                RowFilter = "FLAG = 0 or FLAG = 3 or FLAG = 4"
            };
            foreach (DataRowView view2 in view)
            {
                TreeNode node;
                string str = view2.Row["ITEM_ID"].ToString();
                int num = parentLength + 2;
                if (parentLength == 0)
                {
                    if (str.Length == num)
                    {
                        node = new TreeNode {
                            Name = view2.Row["ITEM_ID"].ToString(),
                            Text = view2.Row["ITEM_NAME"].ToString().Trim()
                        };
                        TreeNodeCollection.Add(node);
                        this.CreateTree(TreeNodeCollection[TreeNodeCollection.Count - 1].Nodes, parentLength + 2, node.Name);
                    }
                }
                else if ((str.Length == num) && (str.Remove(parentLength) == parentId))
                {
                    node = new TreeNode {
                        Name = view2.Row["ITEM_ID"].ToString(),
                        Text = view2.Row["ITEM_NAME"].ToString().Trim()
                    };
                    TreeNodeCollection.Add(node);
                    this.CreateTree(TreeNodeCollection[TreeNodeCollection.Count - 1].Nodes, parentLength + 2, node.Name);
                }
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

        private DataSet GetDictData()
        {
            string sqlSel = ((string.Empty + "SELECT *") + "  FROM " + "his_dict_item ") + "WHERE " + "Dict_Id = 'YD'";
            return GVars.OracleAccess.SelectData(sqlSel);
        }

        private DataSet GetDictData(string itemiId)
        {
            string sqlSel = string.Empty;
            int num = itemiId.Length + 2;
            sqlSel = ((((sqlSel + "SELECT *") + "  FROM " + "his_dict_item ") + "WHERE " + "Dict_Id = 'YD'") + " AND ITEM_ID like '" + itemiId + "%'") + " AND length(ITEM_ID) = " + num;
            return GVars.OracleAccess.SelectData(sqlSel);
        }

        private DataSet GetPatientData(string patientId, string visitId)
        {
            string sqlSel = (((((string.Empty + "SELECT *") + "  FROM " + "PAT_EVALUATION_M ") + "WHERE " + "DICT_ID = 'YD'") + " AND PATIENT_ID = " + SqlManager.SqlConvert(patientId)) + " AND VISIT_ID = " + SqlManager.SqlConvert(visitId)) + " AND DEPT_CODE = " + SqlManager.SqlConvert(GVars.User.DeptCode);
            return GVars.OracleAccess.SelectData(sqlSel);
        }

        private DataSet GetPatientData(string patientId, string visitId, string itemiId)
        {
            string sqlSel = ((((((string.Empty + "SELECT *") + "  FROM " + "PAT_EVALUATION_M ") + "WHERE " + "DICT_ID = 'YD'") + " AND PATIENT_ID = " + SqlManager.SqlConvert(patientId)) + " AND VISIT_ID = " + SqlManager.SqlConvert(visitId)) + " AND DEPT_CODE = " + SqlManager.SqlConvert(GVars.User.DeptCode)) + " AND ITEM_ID = '" + itemiId + "'";
            return GVars.OracleAccess.SelectData(sqlSel);
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.button3 = new Button();
            this.button2 = new Button();
            this.button1 = new Button();
            this.GroupTree = new System.Windows.Forms.GroupBox();
            this.treeView1 = new TreeView();
            this.GroupBox = new System.Windows.Forms.GroupBox();
            this.panel1 = new Panel();
            this.groupBox1.SuspendLayout();
            this.GroupTree.SuspendLayout();
            this.GroupBox.SuspendLayout();
            base.SuspendLayout();
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
            this.groupBox1.Location = new Point(2, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x2d3, 0x2b);
            this.groupBox1.TabIndex = 1;
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
            this.button3.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.button3.Location = new Point(0x282, 0x1b0);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x4b, 0x17);
            this.button3.TabIndex = 5;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = true;
            this.button2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.button2.Location = new Point(0x91, 0x160);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 4;
            this.button2.Text = "确定";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.button1.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.button1.Location = new Point(0x1a7, 0x1af);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 3;
            this.button1.Text = "打印";
            this.button1.UseVisualStyleBackColor = true;
            this.GroupTree.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.GroupTree.Controls.Add(this.treeView1);
            this.GroupTree.Location = new Point(10, 0x2b);
            this.GroupTree.Name = "GroupTree";
            this.GroupTree.Size = new Size(350, 0x19b);
            this.GroupTree.TabIndex = 6;
            this.GroupTree.TabStop = false;
            this.GroupTree.Text = "项目树";
            this.treeView1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.treeView1.Location = new Point(10, 0x11);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new Size(0x149, 0x181);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.GroupBox.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.GroupBox.Controls.Add(this.panel1);
            this.GroupBox.Controls.Add(this.button2);
            this.GroupBox.Location = new Point(0x16e, 0x2c);
            this.GroupBox.Name = "GroupBox";
            this.GroupBox.Size = new Size(0x15f, 0x17d);
            this.GroupBox.TabIndex = 7;
            this.GroupBox.TabStop = false;
            this.GroupBox.Text = "输入区";
            this.panel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.panel1.Location = new Point(0x11, 0x10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x148, 330);
            this.panel1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2d7, 0x1d2);
            base.Controls.Add(this.GroupBox);
            base.Controls.Add(this.GroupTree);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.groupBox1);
            base.Name = "NursingRecNormal_A";
            this.Text = "护理评估单";
            base.Load += new EventHandler(this.NursingRecDocument_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GroupTree.ResumeLayout(false);
            this.GroupBox.ResumeLayout(false);
            base.ResumeLayout(false);
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

        private void SaveInfo(string itemiId, string itemiValue)
        {
            DataTable table = this.GetPatientData(this.patientId, this.visitId, itemiId).Tables[0];
            string sql = string.Empty;
            if (table.Rows.Count == 0)
            {
                sql = "insert into PAT_EVALUATION_M(DEPT_CODE,PATIENT_ID,VISIT_ID,RECORD_DATE,DICT_ID,ITEM_ID,ITEM_VALUE)";
                sql = ((((((((sql + "values( ") + "'" + GVars.User.DeptCode + "'") + ",'" + this.patientId + "'") + ",'" + this.visitId + "'") + "," + SqlManager.GetOraDbDate_Short(DateTime.Now.ToShortDateString())) + ",'YD'") + ",'" + itemiId + "'") + ",'" + itemiValue + "'") + ")";
                GVars.OracleAccess.ExecuteNoQuery(sql);
            }
            else
            {
                sql = "UPDATE PAT_EVALUATION_M ";
                sql = (((((((sql + "SET ") + "ITEM_VALUE = '" + itemiValue + "'") + "WHERE ") + "PATIENT_ID = '" + this.patientId + "'") + "AND VISIT_ID = '" + this.visitId + "'") + "AND DICT_ID = 'YD'") + "AND DEPT_CODE = '" + GVars.User.DeptCode + "'") + "AND ITEM_ID = '" + itemiId + "'";
                GVars.OracleAccess.ExecuteNoQuery(sql);
            }
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
                this.patientId = row["PATIENT_ID"].ToString();
                this.visitId = row["VISIT_ID"].ToString();
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.patienDs = this.GetPatientData(this.patientId, this.visitId);
            this.patienDt = this.patienDs.Tables[0];
            this.NodeName = e.Node.Name;
            this.panel1.Controls.Clear();
            string str = this.myDs.Tables[0].Select("ITEM_ID = '" + e.Node.Name + "'")[0]["FLAG"].ToString();
            if ((str == "3") || (str == "4"))
            {
                DataRow[] rowArray;
                Label label = new Label {
                    Name = "label1",
                    Text = e.Node.Text
                };
                this.panel1.Controls.Add(label);
                label.Location = new Point(20, 30);
                switch (str)
                {
                    case "3":
                    {
                        TextBox box = new TextBox {
                            Name = e.Node.Name
                        };
                        rowArray = this.patienDt.Select("ITEM_ID = '" + e.Node.Name + "'");
                        if (rowArray.Length > 0)
                        {
                            box.Text = rowArray[0]["ITEM_VALUE"].ToString();
                        }
                        else
                        {
                            box.Text = "";
                        }
                        box.Width = 200;
                        this.panel1.Controls.Add(box);
                        box.Location = new Point(20, 0x37);
                        break;
                    }
                    case "4":
                    {
                        DateTimePicker picker = new DateTimePicker {
                            Name = e.Node.Name
                        };
                        rowArray = this.patienDt.Select("ITEM_ID = '" + e.Node.Name + "'");
                        if (rowArray.Length > 0)
                        {
                            picker.Text = rowArray[0]["ITEM_VALUE"].ToString();
                        }
                        picker.Width = 120;
                        this.panel1.Controls.Add(picker);
                        picker.Location = new Point(20, 0x37);
                        break;
                    }
                }
            }
            else if (str == "0")
            {
                DataTable table = this.GetDictData(e.Node.Name).Tables[0];
                this.Auto_Scrolls(table, this.patienDt);
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
                        GVars.Msg.Show("W00005");
                    }
                    else
                    {
                        this.showPatientInfo();
                        this.myDs = this.GetDictData();
                        this.patienDs = this.GetPatientData(this.patientId, this.visitId);
                        this.patienDt = this.patienDs.Tables[0];
                        this.treeView1.Nodes.Clear();
                        this.CreateTree(this.treeView1.Nodes, 0, "");
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
}

