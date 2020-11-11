using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using DXApplication2;

namespace HISPlus
{
    public partial class BodyTemperatureFormBak : FormDo, IBasePatient
    {
        #region 变量
        private readonly float SCROLL_PIXELS = 40F;                  // 一次滚动为10个象素

        private BodyTemperParams objParams = null;

        private PatientDbI patientCom;                                 // 病人共通
        private BodyTemperatureCom com = new BodyTemperatureCom();

        private PatientDbI patientDbI = null;
        private DataSet dsPatient;

        private BodyTemper bodyTemper = new BodyTemper();     // 体温图

        private string patientId = string.Empty;
        private string visitId = string.Empty;
        private string diagnose = string.Empty;
        #endregion


        public BodyTemperatureFormBak()
        {
            InitializeComponent();

            _id = "00011";
            _guid = "1F282093-4D54-4811-9664-881B634131AC";

            this.Load += new EventHandler(BodyTemperatureForm_Load);
            this.Closed += new EventHandler(BodyTemperatureForm_Closed);

            this.picMain.MouseWheel += picMain_MouseWheel;


            //注册事件
            this.MouseWheel += BodyTemperatureForm_MouseWheel;
        }

        void picMain_MouseWheel(object sender, MouseEventArgs e)
        {
            //获取光标位置
            Point mousePoint = new Point(e.X, e.Y);
            //换算成相对本窗体的位置
            mousePoint.Offset(this.Location.X, this.Location.Y);
            //判断是否在panel内
            if (panelBodyTemper.RectangleToScreen(
              panelBodyTemper.DisplayRectangle).Contains(mousePoint))
            {
                //滚动
                panelBodyTemper.AutoScrollPosition = new Point(panelBodyTemper.VerticalScroll.Value - e.Delta);
                //sbVertical.AutoScrollOffset = new Point(panelBodyTemper.VerticalScroll.Value - e.Delta);
            }

            //if (e.Delta > 0)
            //{
            //    this.Text = "Mouse Wheeled Up";
            //}
            //else
            //{
            //    this.Text = "Mouse Wheeled Down";
            //}
        }


        public string PatientId
        {
            get
            {
                return patientId;
            }
            set
            {
                patientId = value;
            }
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

        void BodyTemperatureForm_MouseWheel(object sender, MouseEventArgs e)
        {
            //获取光标位置
            Point mousePoint = new Point(e.X, e.Y);
            //换算成相对本窗体的位置
            mousePoint.Offset(this.Location.X, this.Location.Y);
            //判断是否在panel内
            if (panelBodyTemper.RectangleToScreen(
              panelBodyTemper.DisplayRectangle).Contains(mousePoint))
            {
                //滚动
                panelBodyTemper.AutoScrollPosition = new Point(panelBodyTemper.VerticalScroll.Value - e.Delta);
                //sbVertical.AutoScrollOffset = new Point(panelBodyTemper.VerticalScroll.Value - e.Delta);
            }
        }


        /// <summary>
        /// 窗体伸缩事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BodyTemperatureForm_Resize(object sender, EventArgs e)
        {
            try
            {
                layoutBodyTemper(true);

                float max = (picLeftTop.Height * 2 + picLeft.Height + picBottom.Height - panelBodyTemper.Height) / SCROLL_PIXELS;

                if (max >= 0)
                {
                    sbVertical.Maximum = (int)max + 2;
                }
                else
                {
                    sbVertical.Maximum = sbVertical.Minimum;
                }

                this.sbVertical.Visible = (max >= 0);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        void IBasePatient.Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            // 入院日期                
            bodyTemper.DtInp = MdiFrm.GetInstance().InpDateTime;

            // 获取病人信息
            dsPatient = patientDbI.GetInpPatientInfo_FromID(e.PatientId, e.VisitId);
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                dsPatient = patientDbI.GetPatientInfo_FromID(e.PatientId);
            }
            //dsPatient = patientDbI.GetPatientInfo_FromID(e.PatientId);

            if (dsPatient != null && dsPatient.Tables.Count > 0
                && dsPatient.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsPatient.Tables[0].Rows[0];

                patientId = dr["PATIENT_ID"].ToString();
                visitId = dr["VISIT_ID"].ToString();
            }
            else
            {
                MessageBox.Show("没找到病人! 病人ID:" + e.PatientId);
            }

            reloadData();
        }


        void patientFrm_PatientChanged(object sender, PatientEventArgs e)
        {
            ////LB20110621解决历史体温图打印问题
            //if (sender.GetType().FullName == "System.Collections.Hashtable")
            //{
            //    // 入院日期                
            //    bodyTemper.DtInp =DateTime.Parse(DataType.GetDateTimeShort(((Hashtable)sender)["ADMISSION_DATE_TIME"].ToString()));
            //    patientFrm.DtInp = bodyTemper.DtInp;
            //    patientId = patientFrm.PatientId;
            //    visitId = ((Hashtable)sender)["VISIT_ID"].ToString();
            //    reloadData();
            //    return;
            //}
            ////LB20110621结束


            MdiFrm.GetInstance().LocatePatient(e.PatientId);

        }


        /// <summary>
        /// 体温单垂直滚动条滚动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbVertical_ValueChanged(object sender, EventArgs e)
        {
            if (GVars.App.UserInput == false)
            {
                return;
            }

            layoutBodyTemper(false);
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
                bodyTemper.DtStart = bodyTemper.DtStart.AddDays(-1 * ComConst.VAL.DAYS_PER_WEEK).Date;

                redrawBodyTemper();

                setBodyTemperButton();
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
                bodyTemper.DtStart = bodyTemper.DtStart.AddDays(ComConst.VAL.DAYS_PER_WEEK).Date;

                redrawBodyTemper();

                setBodyTemperButton();
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
        /// 按钮[退出]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BodyTemperatureForm_Closed(object sender, EventArgs e)
        {
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
            patientDbI = new PatientDbI(GVars.OracleAccess);
            patientCom = new PatientDbI(GVars.OracleAccess);

            // 获取参数
            objParams = new BodyTemperParams();
            objParams.Parameters = com.GetParameters();
            objParams.Parse();

            // 共通
            com.Params = objParams;

            // 体温单
            bodyTemper.Params = objParams;
        }


        private void initDisp()
        {
            patientId = MdiFrm.GetInstance().PatientId;
            visitId = MdiFrm.GetInstance().VisitId;

            // 布局
            loadBackgroudPic();
        }


        /// <summary>
        /// 重新加载病人的数据
        /// </summary>
        /// <returns></returns>
        private bool reloadData()
        {
            // 重新获取参数
            if (objParams.ReloadParams == true)
            {
                objParams.Parameters = com.GetParameters();

                objParams.Parse();
            }

            bodyTemper.DtNow = GVars.OracleAccess.GetSysDate();
            bodyTemper.DtInp = MdiFrm.GetInstance().InpDateTime;

            com.AutoGenerateVitalSigns(patientId, visitId);             // 自动生成入转出护理事件
            bodyTemper.DsNursing = com.GetVitalSignsRec(patientId, visitId);

            bodyTemper.ParsePara();

            // 布局各控件
            layoutBodyTemper(true);

            setBodyTemperButton();                                      // 设置按钮状态            

            // 重新绘图
            redrawBodyTemper();

            return true;
        }


        /// <summary>
        /// 加载背景图
        /// </summary>
        /// <returns></returns>
        private bool loadBackgroudPic()
        {
            // 左上角            
            loadBackgroundPic(ref picLeftTop);
            loadBackgroundPic(ref picTop);
            loadBackgroundPic(ref picLeft);
            loadBackgroundPic(ref picMain);
            loadBackgroundPic(ref picBottom);

            // 排列
            layoutBodyTemper(false);

            return true;
        }


        private void loadBackgroundPic(ref PictureBox pic)
        {
            string fileName = Path.Combine(Application.StartupPath, pic.Tag.ToString());

            if (File.Exists(fileName))
            {
                pic.Image = Image.FromFile(fileName);

                pic.Width = pic.Image.Width;
                pic.Height = pic.Image.Height;
            }
        }


        /// <summary>
        /// 设置体温单按扭状态
        /// </summary>
        private void setBodyTemperButton()
        {
            TimeSpan tSpan = bodyTemper.DtStart.Subtract(bodyTemper.MeasureDateStart);
            btnPrePage.Enabled = (tSpan.TotalDays > 0);

            tSpan = bodyTemper.MeasureDateEnd.Subtract(bodyTemper.DtStart.AddDays(ComConst.VAL.DAYS_PER_WEEK));
            btnNextPage.Enabled = (tSpan.TotalDays >= 0);
        }


        /// <summary>
        /// 重画体温单
        /// </summary>
        private void redrawBodyTemper()
        {
            // 重新加载背景图片
            loadBackgroudPic();

            // 顶部
            Graphics grfx = Graphics.FromImage(picTop.Image);
            bodyTemper.DrawHeader(ref grfx);
            picTop.Invalidate();

            // 底部
            grfx = Graphics.FromImage(picBottom.Image);
            bodyTemper.DrawTailParts(ref grfx);

            // 中部
            grfx = Graphics.FromImage(picMain.Image);
            bodyTemper.DrawCenterParts(ref grfx, new Size(picMain.Width, picMain.Height));

        }


        /// <summary>
        /// 重布局体温单
        /// </summary>
        private void layoutBodyTemper(bool blnInit)
        {
            // 上部份固定不动
            picTop.Left = picLeftTop.Left + picLeftTop.Width - 1;

            // 中间部份
            picLeft.Left = picLeftTop.Left;

            if (blnInit)
            {
                picLeft.Top = picLeftTop.Top + picLeftTop.Height;
            }
            else
            {
                picLeft.Top = (int)(picLeftTop.Top + picLeftTop.Height - sbVertical.Value * SCROLL_PIXELS);
            }

            // 作图区
            picMain.Left = picTop.Left;
            picMain.Top = picLeft.Top;
            // picMain.Height      = picLeft.Height;

            // 下半部份
            picBottom.Left = picLeft.Left;
            picBottom.Top = picLeft.Top + picLeft.Height;

            // 滚动栏
            if (blnInit)
            {
                sbVertical.Top = picLeft.Top;
                sbVertical.Left = picBottom.Left + picBottom.Width;
                sbVertical.Height = panelBodyTemper.Height - sbVertical.Top;
            }
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
            Graphics grfx = e.Graphics;

            // 打印固定内容
            printFixed(ref e);

            float zoom = objParams.PrintZoom;  // 6.27F;
            grfx.PageUnit = GraphicsUnit.Pixel;

            // 左上角
            Point ptStart = objParams.PrintStartPoint;
            Rectangle rectDest = new Rectangle(ptStart.X, ptStart.Y, picLeftTop.Image.Width, picLeftTop.Image.Height);
            rectDest.Width = (int)(rectDest.Width * zoom);
            rectDest.Height = (int)(rectDest.Height * zoom);

            grfx.DrawImage(picLeftTop.Image, rectDest, 0, 0, picLeftTop.Width, picLeftTop.Height, GraphicsUnit.Pixel);

            Size szLeftTop = new Size(rectDest.Width, rectDest.Height);

            // 左部份
            ptStart.X = objParams.PrintStartPoint.X;
            ptStart.Y = objParams.PrintStartPoint.Y + szLeftTop.Height;

            rectDest = new Rectangle(ptStart.X, ptStart.Y, picLeft.Image.Width, picLeft.Image.Height);
            rectDest.Width = (int)(picLeft.Width * zoom);
            rectDest.Height = (int)(picLeft.Height * zoom);

            grfx.DrawImage(picLeft.Image, rectDest, 0, 0, picLeft.Width, picLeft.Height, GraphicsUnit.Pixel);

            // 上部份
            ptStart.X = objParams.PrintStartPoint.X + szLeftTop.Width - (int)(zoom);
            ptStart.Y = objParams.PrintStartPoint.Y;

            rectDest = new Rectangle(ptStart.X, ptStart.Y, picTop.Image.Width, picTop.Image.Height);
            rectDest.Width = (int)(picTop.Width * zoom);
            rectDest.Height = (int)(picTop.Height * zoom);

            grfx.DrawImage(picTop.Image, rectDest, 0, 0, picTop.Width, picTop.Height, GraphicsUnit.Pixel);

            // 中间部份
            ptStart.X = objParams.PrintStartPoint.X + szLeftTop.Width;
            ptStart.Y = objParams.PrintStartPoint.Y + szLeftTop.Height;

            rectDest = new Rectangle(ptStart.X, ptStart.Y, picMain.Image.Width, picMain.Image.Height);
            rectDest.Width = (int)(picMain.Width * zoom);
            rectDest.Height = (int)(picMain.Height * zoom);

            grfx.DrawImage(picMain.Image, rectDest, 0, 0, picMain.Width, picMain.Height, GraphicsUnit.Pixel);

            // 下部份
            ptStart.X = objParams.PrintStartPoint.X;
            ptStart.Y += rectDest.Height;

            rectDest = new Rectangle(ptStart.X, ptStart.Y, picBottom.Image.Width, picBottom.Image.Height);
            rectDest.Width = (int)(picBottom.Width * zoom);
            rectDest.Height = (int)(picBottom.Height * zoom);

            grfx.DrawImage(picBottom.Image, rectDest, 0, 0, picBottom.Width, picBottom.Height, GraphicsUnit.Pixel);

            e.HasMorePages = false;
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

            bodyTemper.DtNow = GVars.OracleAccess.GetSysDate();
            bodyTemper.DtInp = DateTime.Parse(ht["ADMISSION_DATE_TIME"].ToString());

            com.AutoGenerateVitalSigns(ht["PATIENT_ID"].ToString(), ht["VISIT_ID"].ToString());             // 自动生成入转出护理事件
            bodyTemper.DsNursing = com.GetVitalSignsRec(ht["PATIENT_ID"].ToString(), ht["VISIT_ID"].ToString());

            bodyTemper.ParsePara();

            // 布局各控件
            layoutBodyTemper(true);

            setBodyTemperButton();                                      // 设置按钮状态            

            // 重新绘图
            redrawBodyTemper();

            return true;
        }

        private void picMain_MouseDown(object sender, MouseEventArgs e)
        {
            picMain.Focus();
        }
    }
}