using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class WristBandPrintFrm1 : FormDo
    {
        #region 窗体变量
        private PatientListWardFrm  patientListFrm  = null;                     // 病人列表
        
        private DataSet             dsPatient       = null;                     // 病人信息
        private string              patientId       = string.Empty;             // 病人ID号
        private string              visitId         = string.Empty;             // 本次就诊序号
        
        private ExcelConfigData     excelConfig     = new ExcelConfigData();    // Excel 打印配置
        private ExcelAccess         excelAccess     = new ExcelAccess();        // Excel 接口
        private string              _template       = "腕带";                   // 模板文件
        
        private DotNetBarcode       qrBarcode       = null;
        private List<ExcelItem>     arrPics         = null;                     // 待插入图片列表
        #endregion
        
        
        public WristBandPrintFrm1()
        {
            InitializeComponent();

            _id     = "00061";
            _guid   = "55972058-E5D0-4a30-BD94-99D42388101D";
        }
        
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WristBandPrintFrm_Load(object sender, EventArgs e)
        {
            try
            {
                initFrmVal();
                initDisp();
                
                if (patientListFrm.PatientId.Length > 0)
                {
                    patientListFrm_PatientChanged(null, new PatientEventArgs(patientListFrm.PatientId, patientListFrm.VisitId));
                }
                
                deleteOldBarcodeBmpFile();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        void patientListFrm_PatientChanged(object sender, PatientEventArgs e)
        {
            GVars.Patient.ID = e.PatientId;
            GVars.Patient.VisitId = e.VisitId;
            
            patientId = GVars.Patient.ID;
            visitId = GVars.Patient.VisitId;
            
            btnPrint.Enabled = getPatientInfo();

            dsPatient.Tables[0].DefaultView.Sort = "ORDER_NO";
            dgvPatient.DataSource = dsPatient.Tables[0].DefaultView;
        }

        
        /// <summary>
        /// 按钮[刷新]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                patientListFrm.ReloadPatientList();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[打印]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // LB20110627,打印机条件检查
                string dllName = this.GetType().Module.Name.Replace(".dll", "");
                string Result = string.Empty;
                GVars.PrinterInfos.SetDefaultPrinter(dllName, out Result);
                if (Result.Length > 0)
                {
                    GVars.Msg.MsgId = "E";
                    GVars.Msg.MsgContent.Add(Result);
                    GVars.Msg.Show();
                    return;
                }
                //LB20110627结束

                if (excelConfig.ConfigSections.Count == 0)
                {
                    loadExcelPrintConfig(_template);
                }

                if (excelConfig.ConfigSections.Count > 0)
                {
                    excelTemplatePrint();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[退出]
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
        #endregion
        

        #region 共通函数
        /// <summary>
        /// 初始化窗体变量
        /// </summary>
        private void initFrmVal()
        {
            _userRight  = GVars.User.GetUserFrmRight(_id);
            
            createPatientInfoStruct();
        }


        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {
            // 加载病人列表
            patientListFrm = new PatientListWardFrm();
            patientListFrm.ShowWaitInpPatient = true;
            
            patientListFrm.DeptCode = GVars.User.DeptCode;

            patientListFrm.FormBorderStyle = FormBorderStyle.None;
            patientListFrm.TopLevel = false;
            patientListFrm.Dock = DockStyle.Fill;
            patientListFrm.Visible = true;

            grpPatientList.Controls.Add(patientListFrm);

            patientListFrm.PatientChanged += new PatientChangedEventHandler(patientListFrm_PatientChanged);
            patientListFrm.BackColor = this.BackColor;
            
            // 病人信息
            dgvPatient.AutoGenerateColumns = false;
        }
        
        
        /// <summary>
        /// 获取病人信息
        /// </summary>
        private bool getPatientInfo()
        {
            // 清除记录内容
            foreach (DataRow dr in dsPatient.Tables[0].Rows)
            {
                dr["COL_VALUE"] = DBNull.Value;
            }
            
            // 获取数据
            if (patientListFrm.DsPatient == null || patientListFrm.DsPatient.Tables.Count == 0)
            {
                return false;
            }
            
            // 加载数据
            string filter = "PATIENT_ID = " + SQL.SqlConvert(patientId);
            DataRow[] drFind = patientListFrm.DsPatient.Tables[0].Select(filter);
            if (drFind.Length > 0)
            {
                DataRow drPatient = drFind[0];
                
                foreach(DataRow dr in dsPatient.Tables[0].Rows)
                {
                    if (drPatient[dr["COL_NAME"].ToString()] == DBNull.Value)
                    {
                        dr["COL_VALUE"] = DBNull.Value;
                    }
                    else
                    {
                        dr["COL_VALUE"] = drPatient[dr["COL_NAME"].ToString()].ToString();
                    }
                }
                
                return true;
            }
            else
            {
                return false;
            }
        }
        
        
        /// <summary>
        /// 创建病人信息结构
        /// </summary>
        private void createPatientInfoStruct()
        {
            dsPatient = new DataSet();
            dsPatient.Tables.Add("PATIENT_INFO");
                        
            dsPatient.Tables[0].Columns.Add("COL_NAME", typeof(string));
            dsPatient.Tables[0].Columns.Add("COL_VALUE", typeof(string));
            dsPatient.Tables[0].Columns.Add("COL_NAME_CH", typeof(string));
            dsPatient.Tables[0].Columns.Add("ORDER_NO", typeof(decimal));
            
            DataRow dr = dsPatient.Tables[0].NewRow();
            dr["COL_NAME"]      = "NAME"; 
            dr["COL_NAME_CH"]   = "姓名";
            dr["ORDER_NO"]      = "0";
            dsPatient.Tables[0].Rows.Add(dr);

            dr = dsPatient.Tables[0].NewRow();
            dr["COL_NAME"]      = "SEX";
            dr["COL_NAME_CH"]   = "性别";
            dr["ORDER_NO"]      = "1";
            dsPatient.Tables[0].Rows.Add(dr);
            
            dr = dsPatient.Tables[0].NewRow();
            dr["COL_NAME"]      = "PATIENT_ID";
            dr["COL_NAME_CH"]   = "病人标识号";
            dr["ORDER_NO"]      = "2";
            dsPatient.Tables[0].Rows.Add(dr);

            dr = dsPatient.Tables[0].NewRow();
            dr["COL_NAME"]      = "DEPT_NAME";
            dr["COL_NAME_CH"]   = "所在科室";
            dr["ORDER_NO"]      = "3";
            dsPatient.Tables[0].Rows.Add(dr);

            dr = dsPatient.Tables[0].NewRow();
            dr["COL_NAME"]      = "BED_LABEL";
            dr["COL_NAME_CH"]   = "床号";
            dr["ORDER_NO"]      = "4";
            dsPatient.Tables[0].Rows.Add(dr);

            dr = dsPatient.Tables[0].NewRow();
            dr["COL_NAME"]      = "ADMISSION_DATE_TIME";
            dr["COL_NAME_CH"]   = "入院日期";
            dr["ORDER_NO"]      = "5";
            dsPatient.Tables[0].Rows.Add(dr);
        }
        #endregion           
        
        
        #region 打印有关
        /// <summary>
        /// 加载Excel打印配置文件
        /// </summary>
        /// <param name="templateFileName"></param>
        private void loadExcelPrintConfig(string templateFileName)
        {
            excelConfig.Clear();

            // 读取配置文件
            string iniFile = Path.Combine(Application.StartupPath, "Template\\" + templateFileName + ".ini");
            if (File.Exists(iniFile) == true)
            {
                StreamReader sr = new StreamReader(iniFile);

                try
                {
                    string configInfo = sr.ReadToEnd();
                    excelConfig.ParseConfigInfo(configInfo);
                }
                finally
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }


        /// <summary>
        /// 用Excel模板打印
        /// </summary>
        /// <remarks>比较适合套打、格式、统计分析报表、图形分析、自定义打印</remarks>
        private void excelTemplatePrint()
        {
            string strExcelTemplateFile = Path.Combine(Application.StartupPath, "Template\\" + _template + ".xls");

            excelAccess.Open(strExcelTemplateFile);         // 打开模板文件
            excelAccess.IsVisibledExcel = true;
            excelAccess.FormCaption = string.Empty;

            excelTemplatePrintData();                       // 输出数据

            excelAccess.Print();				                // 打印
            //excelAccess.PrintPreview();			            // 预览

            excelAccess.Close(false);				        // 关闭并释放			
        }

        
        /// <summary>
        /// 向Excel中输出数据
        /// </summary>
        private void excelTemplatePrintData()
        {
            if (arrPics != null) arrPics.Clear();           // 清除待插入图片
            arrPics = new List<ExcelItem>();
            
            //------------------------------------------------------------------
            ExcelConfigSection configSection = excelConfig.ConfigSections[0];
            
            // 确定数据源
            string filter = "PATIENT_ID = " + SQL.SqlConvert(patientId);
            DataRow[] drFind = patientListFrm.DsPatient.Tables[0].Select(filter);
            if (drFind.Length > 0)
            {
                DataRow drPatient = drFind[0];
                
                // 输出数据
                for (int n = 0; n < configSection.ConfigItems.Count; n++)
                {
                    ExcelItem excelItem = configSection.ConfigItems[n];
                    setExcelCellText(drPatient, excelItem.ItemId, excelItem.Row, excelItem.Col, excelItem.CheckValue);
                }
            }
            
            //------------------------------------------------------------------
            for (int i = 0; i < arrPics.Count; i++)
            {
                excelAccess.InsertPicture(arrPics[i].Row, arrPics[i].Col, arrPics[i].ItemId);
                Thread.Sleep(100);
            }
        }


        /// <summary>
        /// 向单元格赋值
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="colName"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void setExcelCellText(DataRow dr, string colName, int row, int col, string tagInfo)
        {
            // 如果是条码
            if (colName.StartsWith(ExcelConfigData.STR_BARCODE) == true)
            {
                if (tagInfo.Trim().Length == 0) return;

                // 获取条码值
                string barCode = getCol_Value(dr, colName, tagInfo);
                if (barCode.Length == 0) return;
                
                // 生成条码
                int pixelSize = 0;
                int.TryParse(colName.Substring(ExcelConfigData.STR_BARCODE.Length), out pixelSize);
                if (pixelSize < 1) pixelSize = 2;
                
                string fileName = createBarcodeBmpFile(barCode, pixelSize);
                
                ExcelItem item = new ExcelItem();
                item.Row = row;
                item.Col = col;
                item.ItemId = fileName;
                arrPics.Add(item);
            }
            else
            {
                excelAccess.SetCellText(row, col, getCol_Value(dr, colName, tagInfo));
            }
        }


        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="colName"></param>
        /// <param name="tagInfo"></param>
        /// <returns></returns>
        private string getCol_Value(DataRow dr, string colName, string tagInfo)
        {            
            if (tagInfo.Trim().Length == 0) 
            {
                return getCol_Value(dr, colName);
            }
            else
            {
                string barCode = string.Empty;
                string[] parts = tagInfo.Split("+".ToCharArray());
                for (int i = 0; i < parts.Length; i++)
                {
                    string colNameIn = parts[i].Trim();
                    
                    if (colNameIn.StartsWith(@"'") && colNameIn.EndsWith(@"'") && colNameIn.Length > 2)
                    {
                        barCode += colNameIn.Substring(1, colNameIn.Length - 2);
                        continue;
                    }
                    
                    if (dr.Table.Columns.Contains(colNameIn) == false) return string.Empty;
                    barCode += getCol_Value(dr, colNameIn);
                }
                
                return barCode;
            }
        }
        
        
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="colName"></param>
        /// <param name="tagInfo"></param>
        /// <returns></returns>
        private string getCol_Value(DataRow dr, string colName)
        {
            if (dr.Table.Columns.Contains(colName) == false)
            {
                return string.Empty;
            }
            
            if (dr[colName] == DBNull.Value)
            {
                return string.Empty;
            }
            else if (dr.Table.Columns[colName].DataType == typeof(DateTime))
            {
                return ((DateTime)(dr[colName])).ToString(ComConst.FMT_DATE.LONG);
            }
            else
            {
                return dr[colName].ToString();
            }
        }
        

        /// <summary>
        /// 创建条码文件
        /// </summary>
        /// <returns></returns>
        private string createBarcodeBmpFile(string barcode, int pixelSize)
        {
            if (qrBarcode == null)
            {
                qrBarcode = new DotNetBarcode(DotNetBarcode.Types.QRCode);
                qrBarcode.QRQuitZone = pixelSize;
            }
            
            string path =  Path.Combine(Application.StartupPath, "Data");
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            
            string fileName = string.Empty;
            
            if (pixelSize == 2) 
            {
                fileName = Path.Combine(path, barcode + ".bmp");
            }
            else
            {
                fileName = Path.Combine(path, barcode + "_" + pixelSize.ToString() + ".bmp");
            }
            
            qrBarcode.QRSave(barcode, fileName, pixelSize);
            
            return fileName;
        }

        
        /// <summary>
        /// 删除以前的条码文件
        /// </summary>
        /// <returns></returns>
        private void deleteOldBarcodeBmpFile()
        {
            string path =  Path.Combine(Application.StartupPath, "Data");
            if (Directory.Exists(path) == false)
            {
                return;
            }
            
            string[] files = Directory.GetFiles(path);
            for (int i = files.Length - 1; i >= 0; i--)
            {
                File.SetAttributes(files[i], FileAttributes.Normal);
                File.Delete(files[i]);
            }
        }        
        #endregion        
    }
}
