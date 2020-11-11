using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HISPlus
{
    public partial class VitalSignRec_Ward : FormDo
    {
        #region ����
        private VitalSignRec_WardCom com = new VitalSignRec_WardCom();
        private ExcelAccess excelAccess  = new ExcelAccess();
        #endregion

        public VitalSignRec_Ward()
        {
            _id     = "00016";
            _guid   = "21BAA944-5E3D-40f3-8583-BAD29FFF734E";
            
            InitializeComponent();
        }

        private void InitControl()
        {

            ucGridView1.Add("����", "BED_LABEL");
            ucGridView1.Add("����", "NAME");
            ucGridView1.Add("", "FREQUENCY");
            ucGridView1.Add("T2", "T2");
            ucGridView1.Add("P2", "P2");
            ucGridView1.Add("T6", "T6");
            ucGridView1.Add("P6", "P6");
            ucGridView1.Add("T10", "T10");
            ucGridView1.Add("P10", "P10");
            ucGridView1.Add("T14", "T14");
            ucGridView1.Add("P14", "P14");
            ucGridView1.Add("T18", "T18");
            ucGridView1.Add("P18", "P18");
            ucGridView1.Add("T22", "T22");
            ucGridView1.Add("P22", "P22");
            ucGridView1.Add("���", "STOOL");
            ucGridView1.Add("С��", "PEE");

            ucGridView1.Init();
        }

        /// <summary>
        /// ��ť[��ѯ]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // ��ȡ���
                com.GetVitalSignRec_Ward(dtRngStart.Value, GVars.User.DeptCode);

                ucGridView1.DataSource = com.DsVitalSignRec.Tables[0].DefaultView;
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


        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VitalSignRec_Ward_Load(object sender, EventArgs e)
        {
            try
            {
                //this.btnPrint.Visible = false;
                //this.btnPrintPreview.Visible = false;

                InitControl();

                btnQuery.PerformClick();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// ��ť[��ӡԤ��]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                // �������
                if (com.DsVitalSignRec == null
                    || com.DsVitalSignRec.Tables.Count == 0
                    || com.DsVitalSignRec.Tables[0].Rows.Count == 0)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                // ��ӡ
                ExcelTemplatePrint(true);
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


        /// <summary>
        /// ��ť[��ӡԤ��]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // �������
                if (com.DsVitalSignRec == null
                    || com.DsVitalSignRec.Tables.Count == 0
                    || com.DsVitalSignRec.Tables[0].Rows.Count == 0)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                // ��ӡ
                ExcelTemplatePrint(false);
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


        /// <summary>
		/// ��Excelģ���ӡ���Ƚ��ʺ��״򡢸�ʽ��ͳ�Ʒ�������ͼ�η������Զ����ӡ
		/// </summary>
		/// <remarks>��Excel��ӡ������Ϊ���򿪡�д���ݡ���ӡԤ�����ر�</remarks>
		private void ExcelTemplatePrint(bool blnPreview)
		{
			string strExcelTemplateFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Template\\���������۲쵥.xls");
            
			excelAccess.Open(strExcelTemplateFile);				//��ģ���ļ�
			excelAccess.IsVisibledExcel = true;
			excelAccess.FormCaption = string.Empty;

            // ��ȡ�����ļ�
            int startRow = 0;
            int startCol = 0;

		    string iniFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Template\\���������۲쵥.ini");
		    if (System.IO.File.Exists(iniFile) == true)
		    {
		        StreamReader sr = new StreamReader(iniFile);
		        string line = string.Empty;
		        int row = 0;
		        int col = 0;
		        string fieldName = string.Empty;
                
		        while((line = sr.ReadLine()) != null)
		        {
		            // ��ȡ����
                    fieldName = getParts(line, ref row, ref col);

                    switch (fieldName)
                    {
                        case "START_POS":
                            startRow = row;
                            startCol = col;
                            continue;

                        case "WARD_NAME":
                            if (GVars.User.DeptName == null)
                            {
                                GVars.User.DeptName = string.Empty;
                            }

                            excelAccess.SetCellText(row,col, GVars.User.DeptName);
                            break;

                        case "REC_DATE":
                            excelAccess.SetCellText(row, col, dtRngStart.Value.ToString(ComConst.FMT_DATE.SHORT));
                            break;
                    }
		        }
		    }

            if (startRow > 0 && startCol > 0)
            {
                int row = startRow;
                int col = startCol;
                foreach (DataRow dr in com.DsVitalSignRec.Tables[0].Rows)
                { 
                    col = startCol;

                    excelAccess.SetCellText(row, col++, dr["BED_LABEL"].ToString());
                    excelAccess.SetCellText(row, col++, dr["NAME"].ToString());
                    excelAccess.SetCellText(row, col++, dr["FREQUENCY"].ToString());
                    excelAccess.SetCellText(row, col++, dr["T6"].ToString());
                    excelAccess.SetCellText(row, col++, dr["P6"].ToString());
                    excelAccess.SetCellText(row, col++, dr["T10"].ToString());
                    excelAccess.SetCellText(row, col++, dr["P10"].ToString());
                    excelAccess.SetCellText(row, col++, dr["T14"].ToString());
                    excelAccess.SetCellText(row, col++, dr["P14"].ToString());
                    excelAccess.SetCellText(row, col++, dr["T18"].ToString());
                    excelAccess.SetCellText(row, col++, dr["P18"].ToString());
                    excelAccess.SetCellText(row, col++, dr["STOOL"].ToString());
                    excelAccess.SetCellText(row, col++, dr["PEE"].ToString());
                    row++;
                }
            }

            if (blnPreview == true)
            {
                excelAccess.PrintPreview();			       //Ԥ��
            }
            else
            {
                excelAccess.Print();           
            }

			excelAccess.Close(false);				   //�رղ��ͷ�			
		}

        
		private string getParts(string line, ref int row, ref int col)
		{
		    string[] arrParts = line.Split(ComConst.STR.BLANK.ToCharArray());
		    
		    string pos = string.Empty;
		    string fieldName = string.Empty;
		    
		    row = 0;
		    col = 0;
		    for(int i = 0; i < arrParts.Length; i++)
		    {
		        if (arrParts[i].Trim().Length > 0)
		        {
		            if (pos.Length == 0) 
		            { 
		                pos = arrParts[i]; 
                    }
                    else
                    {
                        fieldName = arrParts[i];
                        break;
                    }
		        }    
		    }
		    
		    // ��ȡ����
		    arrParts = pos.Split(":".ToCharArray());
		    if (arrParts.Length <= 1)
		    {
		        return string.Empty;
		    }
		    
		    row = int.Parse(arrParts[0]);   // �к�
		    col = getCol(arrParts[1]);      // �к�
		    
		    return fieldName;
		}

        
		private int getCol(string colString)
		{
            int col = 0;
		    for(int i = 0; i < colString.Length; i++)
		    {
                switch(colString.Substring(i, 1).ToUpper())
                {
                    case "A": col += 1; break;
                    case "B": col += 2; break;
                    case "C": col += 3; break;
                    case "D": col += 4; break;
                    case "E": col += 5; break;
                    case "F": col += 6; break;
                    case "G": col += 7; break;
                    case "H": col += 8; break;
                    case "I": col += 9; break;
                    case "J": col += 10; break;
                    case "K": col += 11; break;
                    case "L": col += 12; break;
                    case "M": col += 13; break;
                    case "N": col += 14; break;
                    case "O": col += 15; break;
                    case "P": col += 16; break;
                    case "Q": col += 17; break;
                    case "R": col += 18; break;
                    case "S": col += 19; break;
                    case "T": col += 20; break;
                    case "U": col += 21; break;
                    case "V": col += 22; break;
                    case "W": col += 23; break;
                    case "X": col += 24; break;
                    case "Y": col += 25; break;
                    case "Z": col += 26; break;
                }
                
                if (colString.Length - 1 > i)
                {
                    col *= (colString.Length - 1 - i) * 26;
                }
		    }	
		    
		    return col;	    
		}


        /// <summary>
        /// ��ť[�ر�]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }        
    }
}