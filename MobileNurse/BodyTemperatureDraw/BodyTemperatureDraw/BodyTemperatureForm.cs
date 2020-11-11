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
        #region ����
        private readonly float SCROLL_PIXELS    = 40F;                  // һ�ι���Ϊ10������
        
        private PatientDbI  patientCom;                                 // ���˹�ͨ
        private BodyTemperatureCom com          = new BodyTemperatureCom(); 
        
        private DataSet     dsPatient;

        private BodyTemper  bodyTemper          = new BodyTemper();     // ����ͼ
        private int         tmInterval          = 4;                    // ����ʱ����
        
        private Bitmap      bmpPicTitle0        = null;                 // ͼ�񻺴�: ����ı���
        private Bitmap      bmpPicTitle         = null;                 // ͼ�񻺴�: ����
        private Bitmap      bmpPicLegend        = null;                 // ͼ�񻺴�: ͼ��
        private Bitmap      bmpPicMain          = null;                 // ͼ�񻺴�: ��ͼ
        private Bitmap      bmpPicAppend        = null;                 // ͼ�񻺴�: ������Ϣ
        private Bitmap      bmpPicAppend0       = null;                 // ͼ�񻺴�: ������Ϣ����
        
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

        
        #region �����¼�
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BodyTemperatureForm_Load( object sender, EventArgs e )
        {
            try
            {
                initFrmVal();                   // ��ʼ���������
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// ���������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BodyTemperatureForm_Resize( object sender, EventArgs e )
        {
            try
            {
                // ���ù�����
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
        /// ����Żس��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtBedLabel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // �������
                if (e.KeyCode != Keys.Return)
                {
                    return;
                }
                
                // ��ȡ��ѯ����
                if (txtBedLabel.Text.Trim().Length == 0)
                {
                    return;
                }

                // ��ȡ������Ϣ
                dsPatient = patientCom.GetInpPatientInfo_FromBedLabel(txtBedLabel.Text.Trim(), GVars.User.DeptCode);

                if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
                {
                    dsPatient = patientCom.GetPatientInfo_FromID(txtBedLabel.Text.Trim());
                }

                // TEST 20080902
                dsPatient.WriteXml("dsPatient.xml", XmlWriteMode.WriteSchema);

                // ��ʾ������Ϣ
                if (showPatientInfo(ref dsPatient) == false)
                {
                    GVars.Msg.Show("W00005");                           // �ò��˲�����!
                    return;
                }
                
                // ��Ժ����
                bodyTemper.DtInp = (DateTime)(dsPatient.Tables[0].Rows[0]["ADMISSION_DATE_TIME"]);

                // ��ͼ
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
        /// ���µ�ˮƽ�����������¼�
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
        /// ���µ���ֱ�����������¼�
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
        /// ��ť[ǰһҳ]�����¼�
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
        /// ��ť[��һҳ]�����¼�
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
		/// ��ť[ˢ��]
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
        /// ��ť[�˳�]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void btnExit_Click( object sender, EventArgs e )
        {
            this.Close();
        }
        
        
        /// <summary>
        /// ����ر��¼�
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
        /// ȷ��Ϊ������뷨
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void imeCtrl_GotFocus(object sender, EventArgs e)
        {
            IME.ChangeControlIme(this.ActiveControl.Handle);
        }
        #endregion
        
        
        #region ��ͨ����
        /// <summary>
        /// ��ʼ���������
        /// </summary>
        private void initFrmVal()
        {
            patientCom = new PatientDbI(GVars.OracleAccess);

            bodyTemper.MeasureStartTime = 2;                            // ����ʼʱ��

            // ��ȡ������Ŀ
            string fileName = System.IO.Path.GetDirectoryName(GVars.IniFile.FileName);
            fileName = System.IO.Path.Combine(fileName, "TEMPERATURE.xml");
            bodyTemper.DsAppendItem.ReadXml(fileName);
        }
        
        
        /// <summary>
        /// ���¼��ز��˵�����
        /// </summary>
        /// <returns></returns>
        private bool reloadData()
        {
            com.GetVitalSignsRec(patientId, visitId);                   // ��ȡ����������Ϣ
            com.GetOperationRec(patientId, visitId);                    // ��ȡ������Ϣ
            // com.GetNursingItemClass();                                  // ������Ŀ�����Ϣ
            com.GetNursingItem();
            com.ReTreatNursingRec();                                    // �������ݴ���(����һ��������, ���������и�ֵ)
            
            // TEST 20080902
            com.DsData.WriteXml("DsData.xml", XmlWriteMode.WriteSchema);

            bodyTemper.DataMeasure = com.DsData.Tables[BodyTemperatureCom.T_VITAL_SIGNS_REC];
            bodyTemper.RefineData();
            
            bodyTemper.DataOper    = com.DsData.Tables[BodyTemperatureCom.T_OPERATION_REC];
			// bodyTemper.DataNursing = com.DsData.Tables[BodyTemperatureCom.T_NURSING_CLASS_DICT];
            bodyTemper.ParsePara();
            
            initDisp_Temper();
            
            // �ػ����
            panelDrawPic.Invalidate();
            panelTitle.Invalidate();
            panelAppend.Invalidate();
            panelLegend.Invalidate();
            panelAppend0.Invalidate();
            panelTitle0.Invalidate();
            
            return true;
        }
        
        
        /// <summary>
        /// ��ʼ�����½���
        /// </summary>
        /// <returns></returns>
        private bool initDisp_Temper()
        {
            #region ��ͼ��������
            bodyTemper.MeasureInterval = tmInterval;                        // ����ʱ����
            bodyTemper.lblDate         = this.lblDate;                      // ����
            bodyTemper.lblOperedDays   = this.lblOperedDays;                // ��������
            bodyTemper.lblDateInterval = this.lblDateInterval;              // ʱ����
            bodyTemper.lblInpDays      = this.lblInpDays;                   // סԺ����            
            bodyTemper.lblBreath       = this.lblBreath;                    // ����
            #endregion
            
            #region ��ʼ�����岼��
            layoutBodyTemper(true);
            
            panelTitle0.Visible = true;
            panelTitle.Visible = true;
            panelLegend.Visible = true;
            panelDrawPic.Visible = true;
            panelBreathRuler.Visible = true;
            panelAppend0.Visible = true;
            panelAppend.Visible = true;
            
            #endregion
            
            #region ��ʼ����ͼ������
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
            
            drawPicCache();                                             // ��ͼ���뻺����            
            setBodyTemperButton();                                      // ���ð�ť״̬
            
            return true;
        }
        
        
        /// <summary>
        /// ��ͼ���뻺����
        /// </summary>
        private void drawPicCache()
        {
            Graphics grfx = null;
            
            // ������
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
            
            // ����
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
            
            // �����
            try
            {
                grfx = Graphics.FromImage(bmpPicMain);
                
                bodyTemper.DrawPicGrid(ref grfx, new Point(0, 0), new Size(bmpPicMain.Width, bmpPicMain.Height));
            }
            finally
            {
                grfx.Dispose();
            }
            
            // ��ɢ������ͼ
            try
            {
                grfx = Graphics.FromImage(bmpPicMain);
                
                bodyTemper.DrawMeasurePoints(ref grfx, new Point(0, 0), new Size(bmpPicMain.Width, bmpPicMain.Height));
            }
            finally
            {
                grfx.Dispose();
            }
            
            // �����ӱ�����
            try
            {
                grfx = Graphics.FromImage(bmpPicAppend0);
                                                
                bodyTemper.DrawAppendGrid0(ref grfx, new Point(0, 0), new Size(bmpPicAppend0.Width, bmpPicAppend0.Height));
            }
            finally
            {
                grfx.Dispose();
            }
                        
            // �����ӱ�
            try
            {
                grfx = Graphics.FromImage(bmpPicAppend);
                                                                
                bodyTemper.DrawAppendGrid(ref grfx, new Point(0, 0), new Size(bmpPicAppend.Width, bmpPicAppend.Height));
            }
            finally
            {
                grfx.Dispose();
            }
            
            
            // ��������Ϣ
            try
            {
                grfx = Graphics.FromImage(bmpPicAppend);
                                
                bodyTemper.DrawMeasurePointsAppend(ref grfx, new Point(0, 0), new Size(bmpPicAppend.Width, bmpPicAppend.Height));
            }
            finally
            {
                grfx.Dispose();
            }
            
            // �������¼��ı�
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
        /// �������µ���Ť״̬
        /// </summary>
        private void setBodyTemperButton()
        {
            TimeSpan tSpan = bodyTemper.DrawPicStartDate.Subtract(bodyTemper.MeasureDateStart);
            btnPrePage.Enabled = (tSpan.TotalDays > 0);
            
            tSpan = bodyTemper.MeasureDateEnd.Subtract(bodyTemper.DrawPicStartDate.AddDays(ComConst.VAL.DAYS_PER_WEEK));
            btnNextPage.Enabled = (tSpan.TotalDays >= 0);
        }        
        
        
        /// <summary>
        /// �ػ����µ�
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
            
            drawPicCache();                                             // ��ͼ���뻺����
                        
            this.Invalidate(true);
            layoutBodyTemper(false);
        }
        
        
        /// <summary>
        /// �ز������µ�
        /// </summary>
        private void layoutBodyTemper(bool blnInit)
        {
            if (blnInit == true)
            {
                //this.grpBodyTemper.Top      = 0;
                //this.panelBodyTemper.Top    = 18;

                // ��ҪԤ��ȷ����
                this.panelTitle.Width       = bodyTemper.SzUnit.Width * bodyTemper.MeasureTimesPerDay * ComConst.VAL.DAYS_PER_WEEK + 2;      // 2Ϊ��ʱƫ����
                
                // ����(��)
                this.panelTitle.Top         = panelTitle0.Top;
                this.panelTitle.Left        = panelTitle0.Left + panelTitle0.Width - 2;         // 2Ϊ��ʱƫ����
                
                // ��ͼ��
                this.panelLegend.Top        = panelTitle0.Top + panelTitle0.Height;
                this.panelLegend.Left       = panelTitle0.Left;
                this.panelLegend.Width      = panelTitle0.Width;
                this.panelLegend.Height     = bodyTemper.SzUnit.Height * 43;                    // 43Ϊ�̶�ֵ
                
                this.panelDrawPic.Top       = panelLegend.Top;
                this.panelDrawPic.Left      = panelTitle.Left;                
                this.panelDrawPic.Width     = panelTitle.Width;                                 // 20Ϊ�������̶�
                this.panelDrawPic.Height    = panelLegend.Height;
                
                // ���±��
                this.panelBreathRuler.Top   = panelDrawPic.Top;
                this.panelBreathRuler.Left  = panelDrawPic.Left + panelDrawPic.Width;
                this.panelBreathRuler.Height= panelDrawPic.Height;

                // ������
                this.panelAppend0.Top       = panelLegend.Top + panelLegend.Height;
                this.panelAppend0.Left      = panelTitle0.Left;
                this.panelAppend0.Width     = panelTitle0.Width;
                this.panelAppend0.Height    = bodyTemper.BaseCellHeight * getPanelAppendRows();
                
                this.panelAppend.Top        = panelAppend0.Top;
                this.panelAppend.Left       = panelDrawPic.Left;
                this.panelAppend.Width      = panelDrawPic.Width;
                this.panelAppend.Height     = panelAppend0.Height;
                
                // ���ù�����
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
        /// �����µ���ͼ��
        /// </summary>
        private void drawLegend(ref Graphics grfx, Point ptLeftRight, Size szRng, int rulerPos)
        {        
            // �����
            bodyTemper.DrawRuler(ref grfx, ptLeftRight, szRng, rulerPos);
            
            // ��ͼ��
            //drawLegend(ref lblPulse,        HISPlus.BodyTemper.enmMeasure.PULSE);
            //drawLegend(ref lblHeartRate,    HISPlus.BodyTemper.enmMeasure.HEARTRATE);
            //drawLegend(ref lblMouseTemper,  HISPlus.BodyTemper.enmMeasure.MOUSE);
            //drawLegend(ref lblAnusTemper,   HISPlus.BodyTemper.enmMeasure.ANUS);
            //drawLegend(ref lblArmpitTemper, HISPlus.BodyTemper.enmMeasure.ARMPIT);
        }
        
        
        /// <summary>
        /// �����µ���ͼ��
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
        /// ��ʾ���˵Ļ�����Ϣ
        /// </summary>
        private bool showPatientInfo(ref DataSet dsPatient)
        { 
            // ��ս���
            this.lblPatientName.Text = string.Empty;                    // ��������
            this.lblGender.Text     = string.Empty;                     // �����Ա�
            this.lblAge.Text        = string.Empty;                     // ����
            this.lblInpDate.Text    = string.Empty;                     // ��Ժ����
            this.lblBedRoom.Text    = string.Empty;                     // ����
            this.lblArchiveNo.Text  = string.Empty;                     // ������
            
            patientId   = string.Empty;
            visitId     = string.Empty;
            
            // ���û�������˳�
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            
            // ��ʾ���˵Ļ�����Ϣ
            DataRow dr = dsPatient.Tables[0].Rows[0];
            
            this.lblPatientName.Text = dr["NAME"].ToString();           // ��������
            this.lblGender.Text = dr["SEX"].ToString();                 // �����Ա�

            if (dr["DATE_OF_BIRTH"].ToString().Length > 0)
            {
                this.lblAge.Text = PersonCls.GetAge((DateTime)dr["DATE_OF_BIRTH"], com.GetSysDate());
            }
            else
            {
                this.lblAge.Text = string.Empty;
            }
            
            this.lblInpDate.Text = DataType.GetDateTimeShort(dr["ADMISSION_DATE_TIME"].ToString());   // ��Ժ����
            this.lblArchiveNo.Text  = dr["INP_NO"].ToString();          // ������
            this.lblBedRoom.Text    = dr["ROOM_NO"].ToString();         // �����

            bodyTemper.DtInp = (DateTime)dr["ADMISSION_DATE_TIME"];
            patientId   = dr["PATIENT_ID"].ToString();
            visitId     = dr["VISIT_ID"].ToString();
            
            // diagnose    = dr["DIAGNOSIS"].ToString();                   // ���

            return true;
        }


        /// <summary>
        /// ��ȡ���ӱ��ĸ߶�
        /// </summary>
        /// <returns></returns>
        private int getPanelAppendRows()
        {
            if (bodyTemper.DsAppendItem == null || bodyTemper.DsAppendItem.Tables.Count == 0)
            {
                return 11;
            }

            int rows = 0;                                               // ������һ��û���������ļ��ж���

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


        #region �ӿ�
        //public void DrawTemperPic()
        //{
        //    dsPatient = patientCom.GetInpPatientInfo_FromID(GVars.Patient.ID, GVars.Patient.VisitId);
        //    patientId = GVars.Patient.ID;    
            
        //    // ��ʾ������Ϣ
        //    if (showPatientInfo(ref dsPatient) == false)
        //    {
        //        GVars.Msg.Show("W00005");                           // �ò��˲�����!
        //        return;
        //    }
            
        //    // ��Ժ����
        //    bodyTemper.DtInp = (DateTime)(dsPatient.Tables[0].Rows[0]["ADMISSION_DATE_TIME"]);

        //    // ��ͼ
        //    if (patientId.Length > 0)
        //    {
        //        reloadData();
        //    }
        //}
        #endregion


        #region ��ӡ
        /// <summary>
        /// ��ť[��ӡ]
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
            
            // ��ӡ�̶�����
            printFixed(ref e);
            
            // ����
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
            
            // ��������̶���
            rect.Y += bmpPicTitle0.Height + 1;
            rect.X = rect0.X;
            
            ptStart.X       = (int)(rect.X);
            ptStart.Y       = (int)(rect.Y);
            szRng.Width     = bmpPicLegend.Width;
            szRng.Height    = bmpPicLegend.Height;
            drawLegend(ref grfx, ptStart, szRng, (lblTemperTop.Left + lblPulseTop.Left + lblPulseTop.Width) / 2);
            
            grfx.DrawRectangle(Pens.Black, ptStart.X, ptStart.Y, szRng.Width, szRng.Height);
            
            // ��ͼ��
            rect.X += szRng.Width;
            
            ptStart.X       = (int)(rect.X);
            ptStart.Y       = (int)(rect.Y);
            szRng.Width     = bmpPicMain.Width;
            szRng.Height    = bmpPicMain.Height;
            
            bodyTemper.DrawPicGrid(ref grfx, ptStart, szRng);                               // �������
            bodyTemper.DrawOtherNursingEventText(ref grfx, ptStart, szRng);                 // �����¼�
            bodyTemper.DrawMeasurePoints(ref grfx, ptStart, szRng);                         // ɢ��
            
            grfx.ResetTransform();
            grfx.DrawRectangle(Pens.Black, ptStart.X, ptStart.Y, szRng.Width, szRng.Height);
            
            // �ײ�
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
        /// ��ӡ�̶�����
        /// </summary>
        /// <param name="e"></param>
        void printFixed(ref PrintPageEventArgs e)
        {
            RectangleF  rectClient  = e.PageSettings.PrintableArea;
            string      text        = string.Empty;
            string      val         = string.Empty;
            Graphics    graph       = e.Graphics;

            // ��ȡ�����ļ�
            string configFile = Path.Combine(Application.StartupPath, "TEMPERATURE.ini");
            if (File.Exists(configFile) == false)
            {
                MessageBox.Show(configFile + " ������, �봴��һ��.");
                return;
            }

            FileStream fs = new FileStream(configFile, FileMode.Open, FileAccess.Read);   
            StreamReader sr = new StreamReader(fs, Encoding.Default);   
            string line;   
          
            while((line = sr.ReadLine()) != null)   
            {   
                line = line.Trim();

                // �������
                if (string.IsNullOrEmpty(line) || line.StartsWith(@"//") == true)
                {
                    continue;
                }
                
                string[] parts = line.Split(",".ToCharArray());
                
                // ���ı� ��ʽ1: x, y, width, height, fontName, fontsize, bold, underline, center, text
                if (parts.Length == 11)
                {
                    RectangleF  rect = getRect(line);                   // λ��
                    if (rect.X < 0)     { rect.X      += rectClient.Width;  }
                    if (rect.Y < 0)     { rect.Y      += rectClient.Height; }
                    if (rect.Width < 0) { rect.Width  += rectClient.Width;  }
                    if (rect.Height < 0){ rect.Height += rectClient.Height; }

                    Font font = getFont(line);                          // ����

                    StringFormat sf = new StringFormat();               // ���뷽ʽ
                    if (parts[8].Trim().ToUpper().Equals("TRUE") == true)
                    {
                        sf.Alignment = StringAlignment.Center;
                    }
                    else
                    {
                        sf.Alignment = StringAlignment.Near;
                    }

                    string color = parts[9].Trim().ToUpper();          // ��ɫ
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

                    val = parts[10].Trim();                             // ֵ
                    val = getVal(val);

                    e.Graphics.DrawString(val, font, brush, rect, sf);

                    sf.Dispose();
                    font.Dispose();
                    
                    continue;
                }
                
                // ���� ��ʽ2: x, y, width
                if (parts.Length >= 3)
                {
                    RectangleF  rect = getRect(line);                   // λ��
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
                return new Font("����", 10);
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
                case "NAME":        // ��������
                    return dr["NAME"].ToString();

                case "GENDER":      // �Ա�
                    return dr["SEX"].ToString();

                case "AGE":         // ���� ��: 27��
                    if (dr["DATE_OF_BIRTH"].ToString().Length > 0)
                    {
                        return PersonCls.GetAge((DateTime)dr["DATE_OF_BIRTH"], com.GetSysDate());
                    }
                    else
                    {
                        return string.Empty;
                    }

                case "BEDLABEL":    // ����
                    return dr["BED_LABEL"].ToString();

                case "BEDNO":       // ����
                    return dr["BED_NO"].ToString();

                case "WEEKS":       // סԺ����
                    return getInpWeeks().ToString();

                case "INPNO":       // סԺ��
                    return dr["INP_NO"].ToString();

                case "WARDNAME":    // ��������
                    return GVars.User.DeptName;

                case "DEPTNAME":    // ��������
                    return dr["DEPT_NAME"].ToString();

                case "INPDATE":     // ��Ժ����
                    if (dr["ADMISSION_DATE_TIME"].ToString().Length > 0)
                    {
                        DateTime dtTemp = (DateTime)dr["ADMISSION_DATE_TIME"];
                        return dtTemp.Year.ToString() + "��" + dtTemp.Month.ToString() + "��" 
                                + dtTemp.Day.ToString() + "��";
                    }
                    else
                    {
                        return string.Empty;
                    }
                    
                case "ARCHIVENO":   // ������
                    return dr["INP_NO"].ToString();

                default:
                    return val;
            }
        }
        #endregion
    }
}