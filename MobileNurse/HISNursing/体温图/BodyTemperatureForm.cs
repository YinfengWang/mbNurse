using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class BodyTemperatureForm : Form
    {
        #region 变量
        private readonly float SCROLL_PIXELS    = 40F;                  // 一次滚动为10个象素
        
        private PatientDbI  patientCom;                                 // 病人共通
        private BodyTemperatureCom com          = new BodyTemperatureCom(); 
        
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
                DataSet dsPatient = patientCom.GetInpPatientInfo_FromBedLabel(txtBedLabel.Text.Trim(), GVars.User.DeptCode);
                
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

            bodyTemper.MeasureStartTime = Business.Nurse.EventStartTime;    // 护理开始时间

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
                this.panelDrawPic.Width     = panelTitle.Width;
                this.panelDrawPic.Height    = panelLegend.Height;
                
                // 补充区
                this.panelAppend0.Top       = panelLegend.Top + panelLegend.Height;
                this.panelAppend0.Left      = panelTitle0.Left;
                this.panelAppend0.Width     = panelTitle0.Width;
                this.panelAppend0.Height    = bodyTemper.BaseCellHeight * 11;                    // 11为固定值
                
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
            
            diagnose    = dr["DIAGNOSIS"].ToString();                   // 诊断

            return true;
        }                
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
            
            rect0.X = 30;
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
            Font        font        = new Font("宋体", 26, FontStyle.Bold);
            RectangleF  rect        = e.PageSettings.PrintableArea;
            int         OFF_SET_Y   = 10;            
            string      text        = string.Empty;
            
            rect.Y      = 40 + OFF_SET_Y;
            rect.X      = 25;
            rect.Height = 80;
            
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            
            // 标题
            e.Graphics.DrawString("体  温  记  录", font,  Brushes.Black, rect, sf);
            
            // 单位名称
            font = new Font("宋体", 16, FontStyle.Bold);
            rect.Y -= 10;
            rect.Width = 260;
            
            sf.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("哈尔滨医科大学", font, Brushes.Black, rect, sf);
            
            rect.Y += font.Height;
            e.Graphics.DrawString(" 附属肿瘤医院 ", font, Brushes.Black, rect, sf);
            
            // 床标
            text = "床号:";
            SizeF sz = new SizeF();
            font = new Font("宋体", 10);
            
            sz = e.Graphics.MeasureString(text, font);
            rect.Y += sz.Height * 2;
            e.Graphics.DrawString(text, font, Brushes.Black, rect, sf);
            
            Font fontUnderLine = new Font("宋体", 10, FontStyle.Underline);
            rect.X += sz.Width;
            e.Graphics.DrawString(txtBedLabel.Text, fontUnderLine, Brushes.Black, rect, sf);
            
            // 姓名
            rect.X = 120;
            text = "姓名:";
            e.Graphics.DrawString(text, font, Brushes.Black, rect, sf);
            
            rect.X += sz.Width;
            e.Graphics.DrawString(lblPatientName.Text, fontUnderLine, Brushes.Black, rect, sf);

            /*
            // 性别
            rect.X = 140;
            e.Graphics.DrawString(lblGender.Text, font, Brushes.Black, rect, sf);
            
            // 年龄
            text = "年龄";
            rect.X = 180;
            e.Graphics.DrawString(text, font, Brushes.Black, rect, sf);
            
            sz = e.Graphics.MeasureString(text, font);
            rect.X += sz.Width;
            
            text = lblAge.Text;
            e.Graphics.DrawString(text, fontUnderLine, Brushes.Black, rect, sf);
            */

            // 入院日期
            text = "入院日期:";
            rect.X = 220;
            e.Graphics.DrawString(text, font, Brushes.Black, rect, sf);
            
            sz = e.Graphics.MeasureString(text, font);
            rect.X += sz.Width;
            
            if (lblInpDate.Text.Length > 0)
            {
                DateTime dtInp = DateTime.Parse(lblInpDate.Text);
                
                text = dtInp.Year.ToString() + "年" + dtInp.Month.ToString() + "月" + dtInp.Day.ToString() + "日";
                e.Graphics.DrawString(text, fontUnderLine, Brushes.Black, rect, sf);

                sz = e.Graphics.MeasureString(text, font);
                rect.X += sz.Width;
            }
            
            // 病室
            text = "科室:";
            rect.X = 410;
            e.Graphics.DrawString(text, font, Brushes.Black, rect, sf);
            
            sz = e.Graphics.MeasureString(text, font);
            rect.X += sz.Width;
            
            text = GVars.User.DeptName;
            e.Graphics.DrawString(text, fontUnderLine, Brushes.Black, rect, sf);
            
            // 病案号
            text = "病案号";
            rect.X = 570;
            e.Graphics.DrawString(text, font, Brushes.Black, rect, sf);
            
            sz = e.Graphics.MeasureString(text, font);
            rect.X += sz.Width;
            
            text = lblArchiveNo.Text;
            e.Graphics.DrawString(text, fontUnderLine, Brushes.Black, rect, sf);
            
            // 诊断
            rect.Y += 20;
            rect.X  = 25;
            
            text = "诊断:";
            e.Graphics.DrawString(text, font, Brushes.Black, rect, sf);
            sz = e.Graphics.MeasureString(text, font);
            
            rect.X += sz.Width;
            e.Graphics.DrawString(diagnose , fontUnderLine, Brushes.Black, rect, sf);
            
            // 第几周
            rect.Y = e.PageSettings.PaperSize.Height - 40; // 60
            rect.Width = e.PageSettings.PaperSize.Width;
            rect.X = 0;
            rect.Height = 20;
            
            sf.Alignment = StringAlignment.Center;
            text = "第         周";
            e.Graphics.DrawString(text, font, Brushes.Black, rect, sf);
            sz = e.Graphics.MeasureString(text, font);
            
            TimeSpan tSpan = bodyTemper.DrawPicStartDate.Subtract(bodyTemper.DtInp);
            int weeks = tSpan.Days / ComConst.VAL.DAYS_PER_WEEK + 1;
            e.Graphics.DrawString(weeks.ToString(), font, Brushes.Black, rect, sf);
            
            rect.X = e.PageSettings.PaperSize.Width / 2 - sz.Width / 2;
            rect.Y += sz.Height;
            SizeF sz2 = e.Graphics.MeasureString("第周", font);
            rect.X += sz2.Width / 2;
            rect.Width = sz.Width - sz2.Width;
            
            // 下划线            
            e.Graphics.DrawLine(Pens.Black, rect.X, rect.Y, rect.X + rect.Width, rect.Y);
        }
        #endregion
    }
}