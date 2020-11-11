using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using DXApplication2;
using TemperatureBLL;

namespace HISPlus
{
    public partial class BodyTemperatureForm : FormDo, IBasePatient
    {
        #region 变量

        private BodyTemperatureCom com
        {
            get { return drawPic.Com; }
        }

        //private PatientDbI patientDbI = null;
        private HospitalDbI hospitalDbI = null;
        private DataSet dsPatient;

        private BodyTemper bodyTemper
        {
            get { return drawPic.BodyTemper; }
        }

        private string patientId = string.Empty;
        private string visitId = string.Empty;

        private int totalPage = 0;
        private int pageIndex = 0;

        private DrawPic drawPic;
        #endregion


        public BodyTemperatureForm()
        {
            InitializeComponent();

            _id = "00011";
            _guid = "1F282093-4D54-4811-9664-881B634131AC";

            this.Load += new EventHandler(BodyTemperatureForm_Load);
        }

        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BodyTemperatureForm_Load(object sender, EventArgs e)
        {
            try
            {
                // 初始化窗体变量
                initFrmVal();

                initDisp();

                reloadData();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        void IBasePatient.Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            patientId = e.PatientId;
            visitId = e.VisitId;
            //// 获取病人信息
            //dsPatient = patientDbI.GetInpPatientInfo_FromID(e.PatientId, e.VisitId);
            //if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            //{
            //    dsPatient = patientDbI.GetPatientInfo_FromID(e.PatientId);
            //}
            ////dsPatient = patientDbI.GetPatientInfo_FromID(e.PatientId);

            //if (dsPatient != null && dsPatient.Tables.Count > 0
            //    && dsPatient.Tables[0].Rows.Count > 0)
            //{
            //    DataRow dr = dsPatient.Tables[0].Rows[0];

            //    patientId = dr["PATIENT_ID"].ToString();
            //    visitId = dr["VISIT_ID"].ToString();
            //}
            //else
            //{
            //    MessageBox.Show("没找到病人! 病人ID:" + e.PatientId);
            //}

            reloadData();
        }

        /// <summary>
        /// 按钮[前一页]单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrePage_Click(object sender, System.EventArgs e)
        {
            try
            {
                pageIndex = pageIndex - 1;
                SetBodyTemperButton();
                RedrawBodyTemper();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[后一页]单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextPage_Click(object sender, System.EventArgs e)
        {
            try
            {
                pageIndex = pageIndex + 1;
                SetBodyTemperButton();
                RedrawBodyTemper();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
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
                this.Cursor = Cursors.WaitCursor;

                reloadData();
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
        /// 按钮 [可打印体温单病人]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintablePatient_Click(object sender, EventArgs e)
        {
            try
            {
                BodyTemperaturPrintablePatientFrm showFrm = new BodyTemperaturPrintablePatientFrm();
                if (showFrm.ShowDialog() == DialogResult.OK)
                {
                    MdiFrm.GetInstance().LocatePatient(showFrm.Patient_Id);
                    //if (showFrm.Bed_Label.ToString().Length > 0)
                    //    patientFrm.InputBedlabel(showFrm.Bed_Label);
                    //else
                    //{
                    //    patientFrm.txtBedLabel.Text1 = showFrm.Patient_Id;
                    //    patientFrm.txtBedLabel_KeyDown(null,new KeyEventArgs(Keys.Return));
                    //}
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 确保为半角输入法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void imeCtrl_GotFocus(object sender, EventArgs e)
        {
            IME.ChangeControlIme(this.ActiveControl.Handle);
        }
        #endregion


        #region 共通函数
        /// <summary>
        /// 初始化窗体变量
        /// </summary>
        private void initFrmVal()
        {
            //patientDbI = new PatientDbI(GVars.OracleAccess);
            hospitalDbI = new HospitalDbI(GVars.OracleAccess);
        }


        private void initDisp()
        {
            //patientId,visitId
            patientId = MdiFrm.GetInstance().PatientId;
            visitId = MdiFrm.GetInstance().VisitId;

            try
            {
                drawPic = new DrawPic(GVars.Patient.ID, Convert.ToInt32(GVars.Patient.VisitId),
                    hospitalDbI.GetHospitalName(),
                    Path.Combine(Application.StartupPath, "Data1"));

                totalPage = drawPic.GetWeeks();
                pageIndex = totalPage;

                RedrawBodyTemper();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 重新加载病人的数据
        /// </summary>
        /// <returns></returns>
        private bool reloadData()
        {
            com.AutoGenerateVitalSigns(patientId, visitId);             // 自动生成入转出护理事件

            drawPic = new DrawPic(GVars.Patient.ID, Convert.ToInt32(GVars.Patient.VisitId),
               hospitalDbI.GetHospitalName(),
               Path.Combine(Application.StartupPath, "Data1"));

            totalPage = drawPic.GetWeeks();
            pageIndex = totalPage;
            SetBodyTemperButton();                                      // 设置按钮状态            
            // 重新绘图
            RedrawBodyTemper();

            return true;
        }

        /// <summary>
        /// 设置体温单按扭状态
        /// </summary>
        private void SetBodyTemperButton()
        {
            txtPageIndex.Text = pageIndex.ToString();
            btnFirst.Enabled = pageIndex > 1;
            btnPrePage.Enabled = pageIndex > 1;
            btnNextPage.Enabled = pageIndex < totalPage;
        }

        /// <summary>
        /// 重画体温单
        /// </summary>
        private void RedrawBodyTemper()
        {


            string s = drawPic.GetImage(pageIndex);

            Image img = Image.FromFile(s);
            Image bmp = new Bitmap(img);

            pictureEdit1.Width = img.Width;
            pictureEdit1.Height = img.Height;
            img.Dispose();
            pictureEdit1.Image = bmp;
        }

        #endregion


        #region 接口
        //public void DrawTemperPic()
        //{
        //    dsPatient = patientCom.GetInpPatientInfo_FromID(GVars.Patient.ID, GVars.Patient.VisitId);
        //    patientId = GVars.Patient.ID;    

        //    // 显示病人信息
        //    if (showPatientInfo(ref dsPatient) == false)
        //    {
        //        GVars.Msg.Show("W00005");                           // 该病人不存在!
        //        return;
        //    }

        //    // 入院日期
        //    bodyTemper.DtInp = (DateTime)(dsPatient.Tables[0].Rows[0]["ADMISSION_DATE_TIME"]);

        //    // 作图
        //    if (patientId.Length > 0)
        //    {
        //        reloadData();
        //    }
        //}
        #endregion


        #region 打印
        /// <summary>
        /// 按钮[打印]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //// LB20110627,打印机条件检查
                //string dllName = this.GetType().Module.Name.Replace(".dll", "");
                //string Result = string.Empty;
                //GVars.PrinterInfos.SetDefaultPrinter(dllName, out Result);
                //if (Result.Length > 0)
                //{
                //    GVars.Msg.MsgId = "E";
                //    GVars.Msg.MsgContent.Add(Result);
                //    GVars.Msg.Show();
                //    return;
                //}
                ////LB20110627结束


                PrintPreviewDialog ppd = new PrintPreviewDialog();
                PrintDocument pd = new PrintDocument();

                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                ppd.Document = pd;
                ppd.ShowDialog();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics gc = e.Graphics;

            //Bitmap image = new Bitmap(pictureEdit1.Image);

            //float quotient = 1;
            //float margin = 20;
            //float page_w = e.PageBounds.Width - (2 * margin);
            //float page_h = e.PageBounds.Height - (2 * margin);
            //if (image.Width >= image.Height)
            //{
            //    quotient = page_w / image.Width;
            //}
            //if (image.Width < image.Height)
            //{
            //    quotient = image.Height / page_h;
            //}
            //float w = page_w;
            //float h = image.Height * quotient;

            //e.Graphics.DrawImage(image, margin, margin, w, h);
            //e.HasMorePages = false;
            //return;

            // 打印固定内容
            //printFixed(ref e);
            Image img1 = Image.FromFile(drawPic.GetClearImage(pageIndex));

         
            Image img = new Bitmap(img1);

          
            img1.Dispose();
          
            float zoom = objParams.PrintZoom;  // 6.27F;
            gc.PageUnit = GraphicsUnit.Pixel;

            Point ptStart = objParams.PrintStartPoint;
            Rectangle rectDest = new Rectangle(ptStart.X, ptStart.Y, img.Width, img.Height);
            rectDest.Width = (int)(img.Width * zoom);
            rectDest.Height = (int)(img.Height * zoom);

            gc.CompositingQuality = CompositingQuality.HighQuality;
            gc.SmoothingMode = SmoothingMode.HighQuality;
            gc.InterpolationMode = InterpolationMode.HighQualityBicubic;

            gc.DrawImage(img, rectDest, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);


            //gc.DrawImage(pictureEdit1.Image, gc.VisibleClipBounds);
            e.HasMorePages = false;
        }

        public BodyTemperParams objParams
        {
            get { return drawPic.BodyTemper.Params; }
        }


        /// <summary>
        /// 打印固定内容
        /// </summary>
        /// <param name="e"></param>
        void printFixed(ref PrintPageEventArgs e)
        {
            RectangleF rectClient = e.PageSettings.PrintableArea;
            string text = string.Empty;
            string val = string.Empty;
            Graphics graph = e.Graphics;

            // dsPatient.WriteXml(@"D:\dsPatient.xml", XmlWriteMode.WriteSchema);

            // 获取配置文件
            string configFile = Path.Combine(Application.StartupPath, "Template\\TEMPERATURE.ini");
            if (File.Exists(configFile) == false)
            {
                MessageBox.Show(configFile + " 不存在, 请创建一个.");
                return;
            }

            FileStream fs = new FileStream(configFile, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();

                // 条件检查
                if (string.IsNullOrEmpty(line) || line.StartsWith(@"//") == true)
                {
                    continue;
                }

                string[] parts = line.Split(ComConst.STR.COMMA.ToCharArray());

                // 画文本 格式1: x, y, width, height, fontName, fontsize, bold, underline, center, text
                if (parts.Length == 11)
                {
                    RectangleF rect = getRect(line);                   // 位置
                    if (rect.X < 0) { rect.X += rectClient.Width; }
                    if (rect.Y < 0) { rect.Y += rectClient.Height; }
                    if (rect.Width < 0) { rect.Width += rectClient.Width; }
                    if (rect.Height < 0) { rect.Height += rectClient.Height; }

                    Font font = getFont(line);                          // 字体

                    StringFormat sf = new StringFormat();               // 对齐方式
                    if (parts[8].Trim().ToUpper().Equals("TRUE") == true)
                    {
                        sf.Alignment = StringAlignment.Center;
                    }
                    else
                    {
                        sf.Alignment = StringAlignment.Near;
                    }

                    string color = parts[9].Trim().ToUpper();          // 颜色
                    Brush brush = Brushes.Black;
                    switch (color)
                    {
                        case "BLUE":
                            brush = Brushes.Blue;
                            break;
                        case "RED":
                            brush = Brushes.Red;
                            break;
                        default:
                            brush = Brushes.Black;
                            break;
                    }

                    val = parts[10].Trim();                             // 值
                    val = getVal(val);

                    e.Graphics.DrawString(val, font, brush, rect, sf);

                    sf.Dispose();
                    font.Dispose();

                    continue;
                }

                // 画线 格式2: x, y, width
                if (parts.Length >= 3)
                {
                    RectangleF rect = getRect(line);                   // 位置
                    if (rect.X < 0) { rect.X += rectClient.Width; }
                    if (rect.Y < 0) { rect.Y += rectClient.Height; }
                    if (rect.Width < 0) { rect.Width += rectClient.Width; }
                    if (rect.Height < 0) { rect.Height += rectClient.Height; }

                    e.Graphics.DrawLine(Pens.Black, rect.X, rect.Y, rect.X + rect.Width, rect.Y);

                    continue;
                }
            }

            sr.Close();
            fs.Close();

            fs.Dispose();
        }


        private int getInpWeeks()
        {
            TimeSpan tSpan = bodyTemper.DtStart.Date.Subtract(bodyTemper.DtInp.Date);
            int weeks = tSpan.Days / ComConst.VAL.DAYS_PER_WEEK + 1;

#if DEBUG
            MessageBox.Show("开周开始时间: " + bodyTemper.DtStart.Date.ToString(ComConst.FMT_DATE.SHORT)
                            + ComConst.STR.CRLF
                            + "病人入院日期: " + bodyTemper.DtInp.Date.ToString(ComConst.FMT_DATE.SHORT)
                            + ComConst.STR.CRLF
                            + "周数: " + weeks.ToString());
#endif

            return weeks;
        }


        private RectangleF getRect(string line)
        {
            RectangleF rect = new RectangleF(0, 0, 1, 1);

            string[] parts = line.Split(ComConst.STR.COMMA.ToCharArray());

            if (parts.Length < 3)
            {
                return rect;
            }

            int result = 0;

            string val = parts[0].Trim();
            int.TryParse(val, out result);
            rect.X = result;

            val = parts[1].Trim();
            int.TryParse(val, out result);
            rect.Y = result;

            val = parts[2].Trim();
            int.TryParse(val, out result);
            rect.Width = result;

            if (parts.Length > 3)
            {
                val = parts[3].Trim();
                int.TryParse(val, out result);
                rect.Height = result;
            }

            return rect;
        }


        private Font getFont(string line)
        {
            // fontName, fontsize, bold, underline
            string[] parts = line.Split(ComConst.STR.COMMA.ToCharArray());

            if (parts.Length < 8)
            {
                return new Font("宋体", 10);
            }

            string fontName = string.Empty;
            int fontSize = 10;
            bool bold = false;
            bool underLine = false;

            int i = 4;
            fontName = parts[i++].Trim();

            string val = parts[i++].Trim();
            int.TryParse(val, out fontSize);

            val = parts[i++].Trim().ToUpper();
            if (val.Equals("TRUE") == true) { bold = true; }

            val = parts[i++].Trim().ToUpper();
            if (val.Equals("TRUE") == true) { underLine = true; }

            return new Font(fontName, fontSize,
                (bold ? FontStyle.Bold : FontStyle.Regular) | (underLine ? FontStyle.Underline : FontStyle.Regular));
        }


        private string getVal(string val)
        {
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                return val;
            }

            DataRow dr = dsPatient.Tables[0].Rows[0];

            val = val.Trim().ToUpper();

            switch (val)
            {
                case "NAME":        // 病人姓名
                    return dr["NAME"].ToString();

                case "GENDER":      // 性别
                    return dr["SEX"].ToString();

                case "AGE":         // 年龄 例: 27岁
                    if (dr["DATE_OF_BIRTH"].ToString().Length > 0)
                    {
                        return PersonCls.GetAge((DateTime)dr["DATE_OF_BIRTH"], GVars.OracleAccess.GetSysDate());
                    }
                    else
                    {
                        return string.Empty;
                    }

                case "BEDLABEL":    // 床标
                    return dr["BED_LABEL"].ToString();

                case "BEDNO":       // 床号
                    return dr["BED_NO"].ToString();

                case "WEEKS":       // 住院周数
                    return getInpWeeks().ToString();

                case "INPNO":       // 住院号
                    return dr["INP_NO"].ToString();

                case "WARDNAME":    // 病区名称
                    return GVars.User.DeptName;

                case "DEPTNAME":    // 科室名称
                    return dr["DEPT_NAME"].ToString();

                case "INPDATE":     // 入院日期
                    if (dr["ADMISSION_DATE_TIME"].ToString().Length > 0)
                    {
                        DateTime dtTemp = (DateTime)dr["ADMISSION_DATE_TIME"];
                        return dtTemp.Year.ToString() + "年" + dtTemp.Month.ToString() + "月"
                                + dtTemp.Day.ToString() + "日";
                    }
                    else
                    {
                        return string.Empty;
                    }

                case "ARCHIVENO":   // 病案号
                    return dr["INP_NO"].ToString();

                default:
                    if (dsPatient.Tables[0].Columns.Contains(val) == true)
                    {
                        return dr[val].ToString();
                    }
                    else
                    {
                        return val;
                    }
            }
        }
        #endregion

        private void btnHistory_Click(object sender, EventArgs e)
        {
            try
            {
                BodyTemperHistory showFrm = new BodyTemperHistory();
                showFrm.Patient_Id = MdiFrm.GetInstance().PatientId;
                showFrm.Patient_Name = MdiFrm.GetInstance().PatientName;

                if (showFrm.ShowDialog() == DialogResult.OK)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("ADMISSION_DATE_TIME", showFrm.MIN_TIME_POINT);
                    ht.Add("VISIT_ID", showFrm.Visit_Id);
                    ht.Add("PATIENT_ID", showFrm.Patient_Id);
                    if (this.patientId == showFrm.Patient_Id && this.visitId != showFrm.Visit_Id)
                    {
                        btnHistoryReloadData(ht);
                    }
                    else
                    {
                        //patientFrm.txtBedLabel.Text1 = showFrm.Patient_Id;
                        //patientFrm.txtBedLabel_KeyDown(null, new KeyEventArgs(Keys.Return));
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 重新加载病人的数据
        /// </summary>
        /// <returns></returns>
        private bool btnHistoryReloadData(Hashtable ht)
        {
            //patientFrm.lblInpDate.Text1 = DataType.GetDateTimeShort(ht["ADMISSION_DATE_TIME"].ToString());   // 入院日期

            // 重新获取参数
            if (objParams.ReloadParams == true)
            {
                objParams.Parameters = com.GetParameters();

                objParams.Parse();
            }

            com.AutoGenerateVitalSigns(ht["PATIENT_ID"].ToString(), ht["VISIT_ID"].ToString());             // 自动生成入转出护理事件
            bodyTemper.DsNursing = com.GetVitalSignsRec(ht["PATIENT_ID"].ToString(), ht["VISIT_ID"].ToString());


            SetBodyTemperButton();                                      // 设置按钮状态            

            // 重新绘图
            RedrawBodyTemper();

            return true;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            try
            {
                pageIndex = 1;
                SetBodyTemperButton();
                RedrawBodyTemper();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        public void Patient_ListRefresh(object sender, PatientEventArgs e)
        {
            //MessageBox.Show("病人列表刷新");
        }
    }
}