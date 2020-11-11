using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HISPlus
{
    public partial class BodyTemperatureForm : FormDo1
    {
        #region 变量
        private readonly float SCROLL_PIXELS    = 40F;                  // 一次滚动为10个象素
        
        private PatientDbI  patientCom;                                 // 病人共通
        private BodyTemperatureCom com          = new BodyTemperatureCom(); 
        
        private DataSet     dsPatient;

        private BodyTemper  bodyTemper          = new BodyTemper();     // 体温图
        private int         tmInterval          = 4;                    // 测量时间间隔
        
        private Bitmap      bmpPicTitle0        = null;                 // 图像缓存: 标题的标题
        private Bitmap      bmpPicTitle         = null;                 // 图像缓存: 标题
        private Bitmap      bmpPicLegend        = null;                 // 图像缓存: 图例
        private Bitmap      bmpPicMain          = null;                 // 图像缓存: 主图
        private Bitmap      bmpPicAppend        = null;                 // 图像缓存: 附加信息
        private Bitmap      bmpPicAppend0       = null;                 // 图像缓存: 附加信息标题
        
        private string      patientId           = string.Empty;
        private string      visitId             = string.Empty;
        private string      diagnose            = string.Empty;
        #endregion
        
        public BodyTemperatureForm()
        {
            _id = "00011";
            _guid = "9B802788-4967-4ab2-B0FB-A89F39E4B001";
            
            InitializeComponent();

            this.Load += new EventHandler( BodyTemperatureForm_Load );
            this.Closed += new EventHandler( BodyTemperatureForm_Closed );
            this.Resize += new EventHandler( BodyTemperatureForm_Resize );
            
            this.txtBedLabel.KeyDown += new KeyEventHandler(txtBedLabel_KeyDown);
            this.txtBedLabel.GotFocus += new EventHandler(imeCtrl_GotFocus);

            this.sbHorizontal.ValueChanged += new EventHandler(sbHorizontal_ValueChanged);
            this.sbVertical.ValueChanged += new EventHandler(sbVertical_ValueChanged);
            this.panelTitle0.Paint += new PaintEventHandler(panelTitle0_Paint);
            this.panelTitle.Paint += new PaintEventHandler(panelTitle_Paint);
            this.panelLegend.Paint += new PaintEventHandler(panelLegend_Paint);
            this.panelDrawPic.Paint += new PaintEventHandler(panelDrawPic_Paint);
            this.panelAppend0.Paint += new PaintEventHandler(panelAppend0_Paint);
            this.panelAppend.Paint += new PaintEventHandler(panelAppend_Paint);
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
        void BodyTemperatureForm_Load( object sender, EventArgs e )
        {
            try
            {
                initFrmVal();                   // 初始化窗体变量
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 窗体伸缩事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BodyTemperatureForm_Resize( object sender, EventArgs e )
        {
            try
            {
                // 设置滚动条
                float max = ((panelTitle0.Width * 2 + panelTitle.Width - panelBodyTemper.Width) / SCROLL_PIXELS);
                if (max >= 0)
                {
                    this.sbHorizontal.Maximum   = (int)max + 1;
                }
                
                this.sbHorizontal.Visible = (max >= 0);
                
                max = ((panelTitle0.Height * 3 + panelDrawPic.Height + panelAppend0.Height - panelBodyTemper.Height) / SCROLL_PIXELS);
                if (max >= 0)
                {
                    this.sbVertical.Maximum     = (int)max + 1;
                }
                
                this.sbVertical.Visible = (max >= 0);
            }    
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 床标号回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtBedLabel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // 条件检查
                if (e.KeyCode != Keys.Return)
                {
                    return;
                }
                
                // 获取查询条件
                if (txtBedLabel.Text.Trim().Length == 0)
                {
                    return;
                }

                // 获取病人信息
                dsPatient = patientCom.GetInpPatientInfo_FromBedLabel(txtBedLabel.Text.Trim(), GVars.User.DeptCode);

                if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
                {
                    dsPatient = patientCom.GetPatientInfo_FromID(txtBedLabel.Text.Trim());
                }

                // TEST 20080902
                dsPatient.WriteXml("dsPatient.xml", XmlWriteMode.WriteSchema);

                // 显示病人信息
                if (showPatientInfo(ref dsPatient) == false)
                {
                    GVars.Msg.Show("W00005");                           // 该病人不存在!
                    return;
                }
                
                // 入院日期
                bodyTemper.DtInp = (DateTime)(dsPatient.Tables[0].Rows[0]["ADMISSION_DATE_TIME"]);

                // 作图
                if (patientId.Length > 0)
                {
                    reloadData();
                }
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
        /// 体温单水平滚动条滚动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbHorizontal_ValueChanged(object sender, EventArgs e)
        {
            if (GVars.App.UserInput == false)
            {
                return;
            }
            
            layoutBodyTemper(false);
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

        
        private void panelTitle0_Paint(object sender, PaintEventArgs e)
        {
            if (bmpPicTitle0 != null)
            {
                e.Graphics.DrawImage(bmpPicTitle0, 0, 0);                
            }

            base.OnPaint( e );
        }

        private void panelTitle_Paint(object sender, PaintEventArgs e)
        {
            if (bmpPicTitle != null)
            {
                e.Graphics.DrawImage(bmpPicTitle, 0, 0);
            }

            base.OnPaint( e );
        }

        private void panelLegend_Paint(object sender, PaintEventArgs e)
        {
            if (bmpPicLegend != null)
            {
                e.Graphics.DrawImage(bmpPicLegend, 0, 0);
            }

            base.OnPaint( e );
        }

        private void panelDrawPic_Paint(object sender, PaintEventArgs e)
        {
            if (bmpPicMain != null)
            {
                e.Graphics.DrawImage(bmpPicMain, 0, 0);
            }

            base.OnPaint( e );
        }

        private void panelAppend0_Paint(object sender, PaintEventArgs e)
        {
            if (bmpPicAppend0 != null)
            {
                e.Graphics.DrawImage(bmpPicAppend0, 0, 0);
            }

            base.OnPaint( e );
        }

        private void panelAppend_Paint(object sender, PaintEventArgs e)
        {
            if (bmpPicAppend != null)
            {
                e.Graphics.DrawImage(bmpPicAppend, 0, 0);
            }

            base.OnPaint( e );
        }
        
        private void rdoOneHour_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rdoOneHour.Checked == false)
            {
                return;
            }
            
            tmInterval = 1;
                        
            initDisp_Temper();
            
            this.Invalidate(true);
        }
        
        private void rdoTwoHour_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rdoTwoHour.Checked == false)
            {
                return;
            }
            
            tmInterval = 2;
                        
            initDisp_Temper();
            
            this.Invalidate(true);
        }

        private void rdoFourHour_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rdoFourHour.Checked == false)
            {
                return;
            }

            tmInterval = 4;
                        
            initDisp_Temper();

            this.Invalidate(true);
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
                bodyTemper.DrawPicStartDate = bodyTemper.DrawPicStartDate.AddDays(-1 * ComConst.VAL.DAYS_PER_WEEK);
                
                redrawBodyTemper();

                setBodyTemperButton();
            }
            catch(Exception ex)
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
                bodyTemper.DrawPicStartDate = bodyTemper.DrawPicStartDate.AddDays(ComConst.VAL.DAYS_PER_WEEK);

                redrawBodyTemper();

                setBodyTemperButton();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
		/// <summary>
		/// 按钮[刷新]
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRefresh_Click( object sender, EventArgs e )
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                reloadData();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// 按钮[退出]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void btnExit_Click( object sender, EventArgs e )
        {
            this.Close();
        }
        
        
        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BodyTemperatureForm_Closed( object sender, EventArgs e )
        {
            try
            {
                if (bmpPicMain != null)     { bmpPicMain.Dispose(); }
                if (bmpPicTitle != null)    { bmpPicTitle.Dispose(); }
                if (bmpPicAppend != null)   { bmpPicAppend.Dispose(); }
                if (bmpPicAppend0 != null)  { bmpPicAppend0.Dispose(); }
            }
            catch(Exception ex)
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
            patientCom = new PatientDbI(GVars.OracleAccess);

            bodyTemper.MeasureStartTime = 2;                            // 护理开始时间

            // 获取附加项目
            string fileName = System.IO.Path.GetDirectoryName(GVars.IniFile.FileName);
            fileName = System.IO.Path.Combine(fileName, "TEMPERATURE.xml");
            bodyTemper.DsAppendItem.ReadXml(fileName);
        }
        
        
        /// <summary>
        /// 重新加载病人的数据
        /// </summary>
        /// <returns></returns>
        private bool reloadData()
        {
            com.GetVitalSignsRec(patientId, visitId);                   // 获取生命体征信息
            com.GetOperationRec(patientId, visitId);                    // 获取手术信息
            // com.GetNursingItemClass();                                  // 护理项目类别信息
            com.GetNursingItem();
            com.ReTreatNursingRec();                                    // 护理数据处理(增加一个属性列, 并对它进行赋值)
            
            // TEST 20080902
            com.DsData.WriteXml("DsData.xml", XmlWriteMode.WriteSchema);

            bodyTemper.DataMeasure = com.DsData.Tables[BodyTemperatureCom.T_VITAL_SIGNS_REC];
            bodyTemper.RefineData();
            
            bodyTemper.DataOper    = com.DsData.Tables[BodyTemperatureCom.T_OPERATION_REC];
			// bodyTemper.DataNursing = com.DsData.Tables[BodyTemperatureCom.T_NURSING_CLASS_DICT];
            bodyTemper.ParsePara();
            
            initDisp_Temper();
            
            // 重绘界面
            panelDrawPic.Invalidate();
            panelTitle.Invalidate();
            panelAppend.Invalidate();
            panelLegend.Invalidate();
            panelAppend0.Invalidate();
            panelTitle0.Invalidate();
            
            return true;
        }
        
        
        /// <summary>
        /// 初始化体温界面
        /// </summary>
        /// <returns></returns>
        private bool initDisp_Temper()
        {
            #region 作图参数设置
            bodyTemper.MeasureInterval = tmInterval;                        // 测量时间间隔
            bodyTemper.lblDate         = this.lblDate;                      // 日期
            bodyTemper.lblOperedDays   = this.lblOperedDays;                // 术后日数
            bodyTemper.lblDateInterval = this.lblDateInterval;              // 时间间隔
            bodyTemper.lblInpDays      = this.lblInpDays;                   // 住院日数            
            bodyTemper.lblBreath       = this.lblBreath;                    // 呼吸
            #endregion
            
            #region 初始化窗体布局
            layoutBodyTemper(true);
            
            panelTitle0.Visible = true;
            panelTitle.Visible = true;
            panelLegend.Visible = true;
            panelDrawPic.Visible = true;
            panelBreathRuler.Visible = true;
            panelAppend0.Visible = true;
            panelAppend.Visible = true;
            
            #endregion
            
            #region 初始化作图缓冲区
            if (bmpPicMain != null)     { bmpPicMain.Dispose(); }
            if (bmpPicTitle != null)    { bmpPicTitle.Dispose(); }
            if (bmpPicAppend != null)   { bmpPicAppend.Dispose(); }
            if (bmpPicLegend != null)   { bmpPicLegend.Dispose(); }
            if (bmpPicAppend0 != null)  { bmpPicAppend0.Dispose(); }
            if (bmpPicTitle0 != null)   { bmpPicTitle0.Dispose(); }
            
            bmpPicMain       = new Bitmap(panelDrawPic.Width, panelDrawPic.Height);
            bmpPicTitle      = new Bitmap(panelTitle.Width, panelTitle.Height);
            bmpPicAppend     = new Bitmap(panelAppend.Width, panelAppend.Height);
            bmpPicLegend     = new Bitmap(panelLegend.Width, panelLegend.Height);
            bmpPicAppend0    = new Bitmap(panelAppend0.Width, panelAppend0.Height);
            bmpPicTitle0     = new Bitmap(panelTitle0.Width, panelTitle0.Height);
            #endregion
            
            drawPicCache();                                             // 把图画入缓冲区            
            setBodyTemperButton();                                      // 设置按钮状态
            
            return true;
        }
        
        
        /// <summary>
        /// 把图画入缓冲区
        /// </summary>
        private void drawPicCache()
        {
            Graphics grfx = null;
            
            // 画标题
            try
            {
                grfx = Graphics.FromImage(bmpPicTitle0);            
                bodyTemper.DrawTitleRowSepLines(ref grfx, new Point(0, 0), new Size(bmpPicTitle0.Width, bmpPicTitle0.Height) , true);
            }
            finally
            {
                grfx.Dispose();
            }
            
            try
            {
                grfx = Graphics.FromImage(bmpPicTitle);            
                bodyTemper.DrawTitle(ref grfx, new Point(0, 0), new Size(bmpPicTitle.Width, bmpPicTitle.Height));
            }
            finally
            {
                grfx.Dispose();
            }
            
            // 画例
            try
            {
                grfx = Graphics.FromImage(bmpPicLegend);
                                
                drawLegend(ref grfx, new Point(0, 0), new Size(bmpPicLegend.Width, bmpPicLegend.Height), 
                    (lblTemperTop.Left + lblPulseTop.Left + lblPulseTop.Width) / 2);
            }
            finally
            {
                grfx.Dispose();
            }
            
            // 画表格
            try
            {
                grfx = Graphics.FromImage(bmpPicMain);
                
                bodyTemper.DrawPicGrid(ref grfx, new Point(0, 0), new Size(bmpPicMain.Width, bmpPicMain.Height));
            }
            finally
            {
                grfx.Dispose();
            }
            
            // 画散点折线图
            try
            {
                grfx = Graphics.FromImage(bmpPicMain);
                
                bodyTemper.DrawMeasurePoints(ref grfx, new Point(0, 0), new Size(bmpPicMain.Width, bmpPicMain.Height));
            }
            finally
            {
                grfx.Dispose();
            }
            
            // 画附加表格标题
            try
            {
                grfx = Graphics.FromImage(bmpPicAppend0);
                                                
                bodyTemper.DrawAppendGrid0(ref grfx, new Point(0, 0), new Size(bmpPicAppend0.Width, bmpPicAppend0.Height));
            }
            finally
            {
                grfx.Dispose();
            }
                        
            // 画附加表
            try
            {
                grfx = Graphics.FromImage(bmpPicAppend);
                                                                
                bodyTemper.DrawAppendGrid(ref grfx, new Point(0, 0), new Size(bmpPicAppend.Width, bmpPicAppend.Height));
            }
            finally
            {
                grfx.Dispose();
            }
            
            
            // 画附加信息
            try
            {
                grfx = Graphics.FromImage(bmpPicAppend);
                                
                bodyTemper.DrawMeasurePointsAppend(ref grfx, new Point(0, 0), new Size(bmpPicAppend.Width, bmpPicAppend.Height));
            }
            finally
            {
                grfx.Dispose();
            }
            
            // 画护理事件文本
            try
            {
                grfx = Graphics.FromImage(bmpPicMain);
                                
                bodyTemper.DrawOtherNursingEventText(ref grfx, new Point(0, 0), new Size(bmpPicMain.Width, bmpPicMain.Height));
            }
            finally
            {
                grfx.Dispose();
            }            
        }
        
        
        /// <summary>
        /// 设置体温单按扭状态
        /// </summary>
        private void setBodyTemperButton()
        {
            TimeSpan tSpan = bodyTemper.DrawPicStartDate.Subtract(bodyTemper.MeasureDateStart);
            btnPrePage.Enabled = (tSpan.TotalDays > 0);
            
            tSpan = bodyTemper.MeasureDateEnd.Subtract(bodyTemper.DrawPicStartDate.AddDays(ComConst.VAL.DAYS_PER_WEEK));
            btnNextPage.Enabled = (tSpan.TotalDays >= 0);
        }        
        
        
        /// <summary>
        /// 重画体温单
        /// </summary>
        private void redrawBodyTemper()
        {
            if (bmpPicMain != null)     { bmpPicMain.Dispose(); }
            if (bmpPicTitle != null)    { bmpPicTitle.Dispose(); }
            if (bmpPicAppend != null)   { bmpPicAppend.Dispose(); }
            if (bmpPicAppend0 != null)  { bmpPicAppend0.Dispose(); }

            bmpPicMain      = new Bitmap(panelDrawPic.Width, panelDrawPic.Height);
            bmpPicTitle     = new Bitmap(panelTitle.Width, panelTitle.Height);
            bmpPicAppend    = new Bitmap(panelAppend.Width, panelAppend.Height);
            bmpPicAppend0   = new Bitmap(panelAppend0.Width, panelAppend0.Height);
            
            drawPicCache();                                             // 把图画入缓冲区
                        
            this.Invalidate(true);
            layoutBodyTemper(false);
        }
        
        
        /// <summary>
        /// 重布局体温单
        /// </summary>
        private void layoutBodyTemper(bool blnInit)
        {
            if (blnInit == true)
            {
                //this.grpBodyTemper.Top      = 0;
                //this.panelBodyTemper.Top    = 18;

                // 需要预先确定的
                this.panelTitle.Width       = bodyTemper.SzUnit.Width * bodyTemper.MeasureTimesPerDay * ComConst.VAL.DAYS_PER_WEEK + 2;      // 2为临时偏移量
                
                // 标题(右)
                this.panelTitle.Top         = panelTitle0.Top;
                this.panelTitle.Left        = panelTitle0.Left + panelTitle0.Width - 2;         // 2为临时偏移量
                
                // 画图区
                this.panelLegend.Top        = panelTitle0.Top + panelTitle0.Height;
                this.panelLegend.Left       = panelTitle0.Left;
                this.panelLegend.Width      = panelTitle0.Width;
                this.panelLegend.Height     = bodyTemper.SzUnit.Height * 43;                    // 43为固定值
                
                this.panelDrawPic.Top       = panelLegend.Top;
                this.panelDrawPic.Left      = panelTitle.Left;                
                this.panelDrawPic.Width     = panelTitle.Width;                                 // 20为画呼吸刻度
                this.panelDrawPic.Height    = panelLegend.Height;
                
                // 体温标尺
                this.panelBreathRuler.Top   = panelDrawPic.Top;
                this.panelBreathRuler.Left  = panelDrawPic.Left + panelDrawPic.Width;
                this.panelBreathRuler.Height= panelDrawPic.Height;

                // 补充区
                this.panelAppend0.Top       = panelLegend.Top + panelLegend.Height;
                this.panelAppend0.Left      = panelTitle0.Left;
                this.panelAppend0.Width     = panelTitle0.Width;
                this.panelAppend0.Height    = bodyTemper.BaseCellHeight * getPanelAppendRows();
                
                this.panelAppend.Top        = panelAppend0.Top;
                this.panelAppend.Left       = panelDrawPic.Left;
                this.panelAppend.Width      = panelDrawPic.Width;
                this.panelAppend.Height     = panelAppend0.Height;
                
                // 设置滚动条
                float max = ((panelTitle0.Width * 2 + panelTitle.Width - panelBodyTemper.Width) / SCROLL_PIXELS);
                if (max >= 0)
                {
                    this.sbHorizontal.Maximum   = (int)max + 1;
                    this.sbHorizontal.Value     = 0;
                }
                
                this.sbHorizontal.Visible = (max >= 0);
                
                max = ((panelTitle0.Height * 3 + panelDrawPic.Height + panelAppend0.Height - panelBodyTemper.Height) / SCROLL_PIXELS);
                if (max >= 0)
                {
                    this.sbVertical.Maximum     = (int)max + 1;
                    this.sbVertical.Value       = 0;
                }
                this.sbVertical.Visible = (max >= 0);
            }
            else
            {
                if (sbHorizontal.Visible == false)
                {
                    sbHorizontal.Value = 0;
                }
                
                this.panelTitle.Left    = (int)(panelTitle0.Left + panelTitle0.Width - sbHorizontal.Value * SCROLL_PIXELS);
                
                this.panelLegend.Top    = (int)(panelTitle.Top + panelTitle.Height - sbVertical.Value * SCROLL_PIXELS);
                
                this.panelDrawPic.Top   = panelLegend.Top;            
                this.panelDrawPic.Left  = panelTitle.Left;
                
                this.panelBreathRuler.Top = panelDrawPic.Top;
                this.panelBreathRuler.Left= panelDrawPic.Left + panelDrawPic.Width;

                this.panelAppend0.Top   = panelLegend.Top + panelLegend.Height;
                this.panelAppend.Top    = panelAppend0.Top;
                this.panelAppend.Left   = panelTitle.Left;
            }
        }        
        
        
        /// <summary>
        /// 画体温单的图例
        /// </summary>
        private void drawLegend(ref Graphics grfx, Point ptLeftRight, Size szRng, int rulerPos)
        {        
            // 画标尺
            bodyTemper.DrawRuler(ref grfx, ptLeftRight, szRng, rulerPos);
            
            // 画图例
            //drawLegend(ref lblPulse,        HISPlus.BodyTemper.enmMeasure.PULSE);
            //drawLegend(ref lblHeartRate,    HISPlus.BodyTemper.enmMeasure.HEARTRATE);
            //drawLegend(ref lblMouseTemper,  HISPlus.BodyTemper.enmMeasure.MOUSE);
            //drawLegend(ref lblAnusTemper,   HISPlus.BodyTemper.enmMeasure.ANUS);
            //drawLegend(ref lblArmpitTemper, HISPlus.BodyTemper.enmMeasure.ARMPIT);
        }
        
        
        /// <summary>
        /// 画体温单的图例
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="eLegend"></param>
        private void drawLegend(ref Label lbl, HISPlus.BodyTemper.enmMeasure eLegend)
        {
            int OFF_SET_V = 6;
            int OFF_SET_H = 4;
            
            bodyTemper.DrawLegend(ref bmpPicLegend, eLegend, lbl.Left + lbl.Width + OFF_SET_H, lbl.Top + OFF_SET_V);
        }        
        
        
        /// <summary>
        /// 显示病人的基本信息
        /// </summary>
        private bool showPatientInfo(ref DataSet dsPatient)
        { 
            // 清空界面
            this.lblPatientName.Text = string.Empty;                    // 病人姓名
            this.lblGender.Text     = string.Empty;                     // 病人性别
            this.lblAge.Text        = string.Empty;                     // 年龄
            this.lblInpDate.Text    = string.Empty;                     // 入院日期
            this.lblBedRoom.Text    = string.Empty;                     // 病室
            this.lblArchiveNo.Text  = string.Empty;                     // 病案号
            
            patientId   = string.Empty;
            visitId     = string.Empty;
            
            // 如果没有数据退出
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            
            // 显示病人的基本信息
            DataRow dr = dsPatient.Tables[0].Rows[0];
            
            this.lblPatientName.Text = dr["NAME"].ToString();           // 病人姓名
            this.lblGender.Text = dr["SEX"].ToString();                 // 病人性别

            if (dr["DATE_OF_BIRTH"].ToString().Length > 0)
            {
                this.lblAge.Text = PersonCls.GetAge((DateTime)dr["DATE_OF_BIRTH"], com.GetSysDate());
            }
            else
            {
                this.lblAge.Text = string.Empty;
            }
            
            this.lblInpDate.Text = DataType.GetDateTimeShort(dr["ADMISSION_DATE_TIME"].ToString());   // 入院日期
            this.lblArchiveNo.Text  = dr["INP_NO"].ToString();          // 病案号
            this.lblBedRoom.Text    = dr["ROOM_NO"].ToString();         // 房间号

            bodyTemper.DtInp = (DateTime)dr["ADMISSION_DATE_TIME"];
            patientId   = dr["PATIENT_ID"].ToString();
            visitId     = dr["VISIT_ID"].ToString();
            
            // diagnose    = dr["DIAGNOSIS"].ToString();                   // 诊断

            return true;
        }


        /// <summary>
        /// 获取附加表格的高度
        /// </summary>
        /// <returns></returns>
        private int getPanelAppendRows()
        {
            if (bodyTemper.DsAppendItem == null || bodyTemper.DsAppendItem.Tables.Count == 0)
            {
                return 11;
            }

            int rows = 0;                                               // 呼吸这一行没有在配置文件中定义

            foreach (DataRow dr in bodyTemper.DsAppendItem.Tables[0].Rows)
            {
                if (dr["ROW_HEIGHT"].ToString().Length == 0)
                {
                    rows++;
                }
                else
                {
                    rows += int.Parse(dr["ROW_HEIGHT"].ToString());
                }
            }
            
            return rows;
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
        private void btnPrint_Click( object sender, EventArgs e )
        {
            try
            {
                PrintPreviewDialog ppd = new PrintPreviewDialog();
                PrintDocument pd = new PrintDocument();
                
                pd.PrintPage += new PrintPageEventHandler( pd_PrintPage );
                ppd.Document = pd;
                ppd.ShowDialog();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        void pd_PrintPage( object sender, PrintPageEventArgs e )
        {
            Graphics    grfx    = e.Graphics;
            
            RectangleF  rect    = new RectangleF();
            RectangleF  rect0   = new RectangleF();
            
            Point       ptStart = new Point(0, 0);
            Size        szRng   = new Size(1, 1);
            
            rect0.X = 20;
            rect0.Y = 70;
            
            rect.X = rect0.X;
            rect.Y = rect0.Y;
            
            // 打印固定内容
            printFixed(ref e);
            
            // 标题
            rect.Y = 125 + 15;
            
            ptStart.X       = (int)(rect.X);
            ptStart.Y       = (int)(rect.Y);
            szRng.Width     = bmpPicTitle0.Width;
            szRng.Height    = bmpPicTitle0.Height;
            bodyTemper.DrawTitleRowSepLines(ref grfx, ptStart, szRng , true);
            
            grfx.DrawRectangle(Pens.Black, ptStart.X, ptStart.Y, szRng.Width, szRng.Height);
            
            rect.X += szRng.Width;
            
            ptStart.X       = (int)(rect.X);
            ptStart.Y       = (int)(rect.Y);
            szRng.Width     = bmpPicTitle.Width;
            szRng.Height    = bmpPicTitle.Height;
            bodyTemper.DrawTitle(ref grfx, ptStart, szRng);
            
            grfx.DrawRectangle(Pens.Black, ptStart.X, ptStart.Y, szRng.Width, szRng.Height);
            
            // 画例及左刻度线
            rect.Y += bmpPicTitle0.Height + 1;
            rect.X = rect0.X;
            
            ptStart.X       = (int)(rect.X);
            ptStart.Y       = (int)(rect.Y);
            szRng.Width     = bmpPicLegend.Width;
            szRng.Height    = bmpPicLegend.Height;
            drawLegend(ref grfx, ptStart, szRng, (lblTemperTop.Left + lblPulseTop.Left + lblPulseTop.Width) / 2);
            
            grfx.DrawRectangle(Pens.Black, ptStart.X, ptStart.Y, szRng.Width, szRng.Height);
            
            // 作图区
            rect.X += szRng.Width;
            
            ptStart.X       = (int)(rect.X);
            ptStart.Y       = (int)(rect.Y);
            szRng.Width     = bmpPicMain.Width;
            szRng.Height    = bmpPicMain.Height;
            
            bodyTemper.DrawPicGrid(ref grfx, ptStart, szRng);                               // 背景表格
            bodyTemper.DrawOtherNursingEventText(ref grfx, ptStart, szRng);                 // 护理事件
            bodyTemper.DrawMeasurePoints(ref grfx, ptStart, szRng);                         // 散点
            
            grfx.ResetTransform();
            grfx.DrawRectangle(Pens.Black, ptStart.X, ptStart.Y, szRng.Width, szRng.Height);
            
            // 底部
            rect.Y += bmpPicLegend.Height;
            rect.X = rect0.X;
            
            ptStart.X       = (int)(rect.X);
            ptStart.Y       = (int)(rect.Y);
            szRng.Width     = bmpPicAppend0.Width;
            szRng.Height    = bmpPicAppend0.Height;
            bodyTemper.DrawAppendGrid0(ref grfx, ptStart, szRng);
            
            grfx.DrawRectangle(Pens.Black, ptStart.X, ptStart.Y, szRng.Width, szRng.Height);
            
            rect.X += bmpPicAppend0.Width;
            
            ptStart.X       = (int)(rect.X);
            ptStart.Y       = (int)(rect.Y);
            szRng.Width     = bmpPicAppend.Width;
            szRng.Height    = bmpPicAppend.Height;
            
            bodyTemper.DrawAppendGrid(ref grfx, ptStart, szRng);
            bodyTemper.DrawMeasurePointsAppend(ref grfx, ptStart, szRng);
            
            grfx.DrawRectangle(Pens.Black, ptStart.X, ptStart.Y, szRng.Width, szRng.Height);
            
            e.HasMorePages = false;
        }
        
        
        /// <summary>
        /// 打印固定内容
        /// </summary>
        /// <param name="e"></param>
        void printFixed(ref PrintPageEventArgs e)
        {
            RectangleF  rectClient  = e.PageSettings.PrintableArea;
            string      text        = string.Empty;
            string      val         = string.Empty;
            Graphics    graph       = e.Graphics;

            // 获取配置文件
            string configFile = Path.Combine(Application.StartupPath, "TEMPERATURE.ini");
            if (File.Exists(configFile) == false)
            {
                MessageBox.Show(configFile + " 不存在, 请创建一个.");
                return;
            }

            FileStream fs = new FileStream(configFile, FileMode.Open, FileAccess.Read);   
            StreamReader sr = new StreamReader(fs, Encoding.Default);   
            string line;   
          
            while((line = sr.ReadLine()) != null)   
            {   
                line = line.Trim();

                // 条件检查
                if (string.IsNullOrEmpty(line) || line.StartsWith(@"//") == true)
                {
                    continue;
                }
                
                string[] parts = line.Split(",".ToCharArray());
                
                // 画文本 格式1: x, y, width, height, fontName, fontsize, bold, underline, center, text
                if (parts.Length == 11)
                {
                    RectangleF  rect = getRect(line);                   // 位置
                    if (rect.X < 0)     { rect.X      += rectClient.Width;  }
                    if (rect.Y < 0)     { rect.Y      += rectClient.Height; }
                    if (rect.Width < 0) { rect.Width  += rectClient.Width;  }
                    if (rect.Height < 0){ rect.Height += rectClient.Height; }

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
                    RectangleF  rect = getRect(line);                   // 位置
                    if (rect.X < 0)     { rect.X      += rectClient.Width;  }
                    if (rect.Y < 0)     { rect.Y      += rectClient.Height; }
                    if (rect.Width < 0) { rect.Width  += rectClient.Width;  }
                    if (rect.Height < 0){ rect.Height += rectClient.Height; }

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
            TimeSpan tSpan = bodyTemper.DrawPicStartDate.Subtract(bodyTemper.DtInp);
            return tSpan.Days / ComConst.VAL.DAYS_PER_WEEK + 1;
        }

        
        private RectangleF getRect(string line)
        {
            RectangleF rect = new RectangleF(0, 0, 1, 1);
            
            string[] parts = line.Split(",".ToCharArray());

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
            string[] parts = line.Split(",".ToCharArray());

            if (parts.Length < 8)
            {
                return new Font("宋体", 10);
            }

            string fontName     = string.Empty;
            int    fontSize     = 10;
            bool   bold         = false;
            bool   underLine    = false;
            
            int i = 4;
            fontName = parts[i++].Trim();

            string val = parts[i++].Trim();
            int.TryParse(val, out fontSize);

            val = parts[i++].Trim().ToUpper();
            if (val.Equals("TRUE") == true) { bold = true;}

            val = parts[i++].Trim().ToUpper();
            if (val.Equals("TRUE") == true) { underLine = true;}
            
            return new Font(fontName, fontSize,
                (bold? FontStyle.Bold: FontStyle.Regular) | (underLine? FontStyle.Underline: FontStyle.Regular));
        }


        private string getVal(string val)
        {
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                return val;
            }

            DataRow dr = dsPatient.Tables[0].Rows[0];

            switch (val.ToUpper())
            {
                case "NAME":        // 病人姓名
                    return dr["NAME"].ToString();

                case "GENDER":      // 性别
                    return dr["SEX"].ToString();

                case "AGE":         // 年龄 例: 27岁
                    if (dr["DATE_OF_BIRTH"].ToString().Length > 0)
                    {
                        return PersonCls.GetAge((DateTime)dr["DATE_OF_BIRTH"], com.GetSysDate());
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
                    return val;
            }
        }
        #endregion
    }
}