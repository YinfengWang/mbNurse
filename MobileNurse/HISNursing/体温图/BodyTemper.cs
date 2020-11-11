//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ҽԺϵͳ
//  ��ϵͳ����      : ��ͨģ��
//  ��������        : 
//  ����            : BodyTemper.cs
//  ���ܸ�Ҫ        : �����µ�
//  ������          : ����
//  ������          : 2007-03-20
//  �汾            : 1.0.0.0
// 
//------------------< �����ʷ >------------------------------------------------------
//  �������        : 
//  �����          : 
//  �������        : 
//  �汾            : 
//------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Drawing;
using System.Collections;
using Label = System.Windows.Forms.Label;

namespace HISPlus
{
	/// <summary>
	/// BodyTemper ��ժҪ˵����
	/// </summary>
	public class BodyTemper
	{
		public BodyTemper()
		{
            drawFormat.Alignment = StringAlignment.Center;                                      // �ı�����
		}

        
        #region �ṹ�嶨��
        public enum enmMeasure
        {
            PULSE,                              // ����
            HEARTRATE,                          // ����
            MOUSE,                              // �ڱ�
            ANUS,                               // �ر�
            ARMPIT                              // Ҹ��
        }
        
        #endregion
        
        
        #region ����
        public readonly string MEASURE_REC = "VITAL_SIGNS_REC";                                 // ����: ������¼
        public readonly string OPER_REC    = "OPERATION";                                       // ����: ������¼
		public readonly string NURSING_DICT= "NURSE_TEMPERATURE_CLASS_DICT";                    // ����: ���������ֵ�
        
        // �ⲿ�ؼ�
        public Label         lblDate        = null;                                             // ��ǩ: ����
        public Label         lblOperedDays  = null;                                             // ��ǩ: ��������
        public Label         lblDateInterval= null;                                             // ��ǩ: ʱ����
        public Label         lblInpDays     = null;                                             // ��ǩ: סԺ����
        
        public Label         lblBreath      = null;                                             // ��ǩ: ����
        
        // ��ͼ����
        private Pen          penBlack       = new Pen(Color.Black);                             // ����: ��ɫ + ���1
        private Pen          penBlackBold   = new Pen(Color.Black, 2F);                         // ����: ��ɫ + ���2
        private Pen          penRedBold     = new Pen(Color.Red, 2F);                           // ����: ��ɫ + ���2
        private Pen          penRed         = new Pen(Color.Red);                               // ����: ��ɫ + ���1
        private Pen          penBlueBold    = new Pen(Color.Blue, 2F);                          // ����: ��ɫ + ���2
        private Pen          penBlue        = new Pen(Color.Blue);                              // ����: ��ɫ + ���1
        
        private SolidBrush   brushDraw      = new SolidBrush(Color.Black);                      // ��ˢ: ��ɫ
        private SolidBrush   brushRed       = new SolidBrush(Color.Red);                        // ��ˢ: ��ɫ
        private SolidBrush   brushBlue      = new SolidBrush(Color.Blue);                       // ��ˢ: ��ɫ
        private StringFormat drawFormat     = new StringFormat();                               // �ı�����
        
        public  Color        BackColor      = Color.White;                                      // ����ɫ
        public  Font         Font           = new Font("����", 9);                              // ����
        
        // ��С����
        public Size          SzLegend       = new Size(8, 8);                                   // ͼ����С
        public Size          SzUnit         = new Size(14, 12);                                 // ���±��С��Ԫ�������
        public int           BaseCellHeight = 20;                                               // ��ױ��Ԫ��ĸ߶�        
        public int           TwoLineHeight  = 30;

        private int          OFF_SET        = 4;
        
        // ҵ������
        public DataSet       DsAppendItem   = new DataSet();                                    // ���µ�����ĸ�����Ŀ
        private string       hospitalName   = string.Empty;                                     // ҽԺ����
        private string       patientId      = string.Empty;                                     // ����ID��
        private string       patientName    = string.Empty;                                     // ��������
        private string       deptName       = string.Empty;                                     // ��������
        private string       bedLabel       = string.Empty;                                     // ����
        private string       inpNo          = string.Empty;                                     // סԺ��
        private string       inDeptDate     = string.Empty;                                     // �������
        private DateTime     dtInp          = DateTime.Now;                                     // ��Ժ����
        
        private DataSet      data           = new DataSet();                                    // ����DataSet ��ṹͬ[VITAL_SIGNS_REC]
        private DataTable    dtDownTemper   = null;                                             // ��������        
        
        private int          tmStart        = 1;                                                // ÿ������Ŀ�ʼʱ���
        private int          tmInterval     = 4;                                                // ����ʱ����
        private int          timesPerDay    = 6;                                                // ÿ������Ĵ���
        
        private int          temperBase     = 34;                                               // ���´�34�ȿ�ʼ��    
        private float        temperStep     = 0.2F;                                             // ����ÿ0.2��Ϊһ���̶�
        private int          pulseBase      = 20;                                               // ������20��ʼ��
        private int          pulseStep      = 4;                                                // ����ÿ4��Ϊһ���̶�
        
        private DateTime     dtStart        = DateTime.Now;                                     // ��ͼ��ʼ����
        private DateTime     dtRngStart     = DateTime.Now;                                     // ������ʼ����
        private DateTime     dtRngEnd       = DateTime.Now;                                     // ���ݽ�������
        
        // ״̬����
        private bool         blnDataRefined = false;                                            // ����������Ƿ�������
        #endregion
        
        
        #region ����
        /// <summary>
        /// ������ʼʱ���
        /// </summary>
        public int MeasureStartTime
        {
            get
            {
                return tmStart;
            }
            set
            {
                if (value >= 0 && value < ComConst.VAL.HOURS_PER_DAY)
                {
                    tmStart = value;

                    timesPerDay = ComConst.VAL.HOURS_PER_DAY / tmInterval - tmStart / tmInterval;
                }
            }
        }
        
        
        /// <summary>
        /// ����ʱ����(Сʱ)
        /// </summary>
        public int MeasureInterval
        {
            get
            {
                return tmInterval;
            }
            set
            {
                if (value >= 0 && value < ComConst.VAL.HOURS_PER_DAY)
                {
                    tmInterval = value;
                                        
                    timesPerDay = ComConst.VAL.HOURS_PER_DAY / tmInterval - tmStart / tmInterval;
                }
            }
        }
        
        
        /// <summary>
        /// һ���������
        /// </summary>
        public int MeasureTimesPerDay
        {
            get
            {
                return timesPerDay;
            }
        }


        /// <summary>
        /// ����[��Ժ����]
        /// </summary>
        public DateTime DtInp
        {
            get
            {
                return dtInp;
            }
            set
            {
                dtInp = value;
            }
        }

        
        /// <summary>
        /// ����[��������]
        /// </summary>
        public DataTable DataMeasure
        {
            get
            {
                foreach(DataTable dt in data.Tables)
                {
                    if (OPER_REC.Equals(dt.TableName) == true)
                    {
                        return data.Tables[MEASURE_REC].Copy();
                    }
                }
                
                return null;
            }
            set
            {
                if (data != null && data.Tables.IndexOf(MEASURE_REC) >= 0)
                {
                    data.Tables.Remove(MEASURE_REC);
                }
                
                DataTable dtMeasure = value.Copy();
                dtMeasure.TableName = MEASURE_REC;
                
                data.Tables.Add(dtMeasure);
                
                blnDataRefined = false;
            }
        }


        /// <summary>
        /// ����[������Ϣ]
        /// </summary>
        public DataTable DataOper
        {
            get
            {
                foreach(DataTable dt in data.Tables)
                {
                    if (OPER_REC.Equals(dt.TableName) == true)
                    {
                        return data.Tables[OPER_REC].Copy();
                    }
                }
                
                return null;
            }

            set
            {
                if (data != null && data.Tables.IndexOf(OPER_REC) >= 0)
                {
                    data.Tables.Remove(OPER_REC);
                }
                
                DataTable dtMeasure = value.Copy();
                dtMeasure.TableName = OPER_REC;
                
                data.Tables.Add(dtMeasure);
            }
        }
        
        
		/// <summary>
		/// ����[��������]
		/// </summary>
		public DataTable DataNursing
		{
			get
			{
				foreach(DataTable dt in data.Tables)
				{
					if (NURSING_DICT.Equals(dt.TableName) == true)
					{
						return data.Tables[NURSING_DICT].Copy();
					}
				}
                
				return null;
			}
			set
			{
				if (data != null && data.Tables.IndexOf(NURSING_DICT) >= 0)
				{
					data.Tables.Remove(NURSING_DICT);
				}
                
				DataTable dtNursing = value.Copy();
				dtNursing.TableName = NURSING_DICT;
                
				data.Tables.Add(dtNursing);
			}
		}


        /// <summary>
        /// ����[������ʼʱ��]
        /// </summary>
        public DateTime MeasureDateStart
        {
            get            
            {
                return dtRngStart;
            }
        }


        /// <summary>
        /// ����[��������ʱ��]
        /// </summary>
        public DateTime MeasureDateEnd
        {
            get
            {
                return dtRngEnd;
            }
        }

        
        /// <summary>
        /// ����[��ͼ��ʼ����]
        /// </summary>
        public DateTime DrawPicStartDate
        {
            get
            {
                return dtStart;
            }
            set
            {
                dtStart = value;
            }
        }        
        #endregion
        
        
        #region ��ͼ����
        #region ������
        /// <summary>
        /// ������ [���� �Ա� ����  ��Ժ���� ���� ������]
        /// </summary>
        public void DrawTitle(ref Graphics grfx, Point ptLeftRight, Size szRng)
        {
            // ���зָ���
            DrawTitleRowSepLines(ref grfx, ptLeftRight, szRng, false);
            
            // ����PANEL��д����
            int         left        = ptLeftRight.X + 1;
            DateTime    dtCurrent   = dtStart;
            DateTime    dtPre       = dtStart;
            string      curDate     = dtStart.ToString(ComConst.FMT_DATE.SHORT);
            string      inpDays     = string.Empty;                 // סԺ����
            string      operedDays  = string.Empty;                 // ��������
            
            for(int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {
                Rectangle rect = new Rectangle(left, ptLeftRight.Y, SzUnit.Width * timesPerDay, szRng.Height);
                
                // ��ȡ��ǰ����
                dtCurrent = dtStart.AddDays(i);
                
                // ��ȡ��ǰ���ڵ��ַ���
                if (i == 0 || dtCurrent.Year != dtPre.Year)
                {
                    curDate = dtCurrent.ToString(ComConst.FMT_DATE.SHORT);
                }
                else
                {
                    if (dtCurrent.Month != dtPre.Month)
                    {
                        curDate = dtCurrent.ToString("MM-dd");
                    }
                    else
                    {
                        curDate = int.Parse(dtCurrent.ToString("dd")).ToString();
                    }
                }
                
                dtPre = dtCurrent;

                // ��ȡ��������
                dtStart = DateTime.Parse(dtStart.ToString(ComConst.FMT_DATE.SHORT));
                dtInp   = DateTime.Parse(dtInp.ToString(ComConst.FMT_DATE.SHORT));
                
                TimeSpan tSpan = (dtStart.AddDays(i)).Subtract(dtInp);
                inpDays = string.Empty;
                if (tSpan.Days >= 0)
                {
                    inpDays = tSpan.Days.ToString();
                }

                // ��������
                operedDays = getOperedDays(dtStart.AddDays(i));
                
                // ������
                drawOneTitle(ref grfx, rect, curDate, inpDays, operedDays, tmStart, tmInterval, timesPerDay, i == ComConst.VAL.DAYS_PER_WEEK - 1);
                
                left += SzUnit.Width * timesPerDay;
            }
        }
        
        
        /// <summary>
        /// �������зָ���
        /// </summary>
        /// <param name="bmp">��ͼ����</param>
        /// <param name="lblDate">����</param>
        /// <param name="lblInpDays">סԺ����</param>
        /// <param name="lblOperedDays">��������</param>
        public void DrawTitleRowSepLines(ref Graphics grfx, Point ptLeftRight, Size szRng, bool blnTitle)
        {
            int width   = szRng.Width;
            int height  = szRng.Height;
            
            // ��Ӵ���
            Point ptStart = new Point(ptLeftRight.X,            ptLeftRight.Y);
            Point ptEnd   = new Point(ptLeftRight.X + width,    ptStart.Y);
            
            grfx.DrawLine(this.penBlack, ptStart, ptEnd);
            
            drawFormat.Alignment = StringAlignment.Center;
            
            // ���� �� סԺ���� �ָ���
            ptStart = new Point(ptLeftRight.X,          ptLeftRight.Y + lblDate.Top + lblDate.Height);
            ptEnd   = new Point(ptLeftRight.X + width,  ptStart.Y);
            
            grfx.DrawLine(this.penBlack, ptStart, ptEnd);
            
            if (blnTitle)
            {
                grfx.DrawString(lblDate.Text, Font, brushDraw, 
                    new RectangleF(ptLeftRight.X + lblDate.Left, ptLeftRight.Y + lblDate.Top, lblDate.Width, lblDate.Height), 
                    drawFormat);
            }
            
            // סԺ���� �� �������� �ָ���
            ptStart = new Point(ptLeftRight.X,          ptLeftRight.Y + lblInpDays.Top + lblInpDays.Height);
            ptEnd   = new Point(ptLeftRight.X + width,  ptStart.Y);
            
            if (blnTitle)
            {
                grfx.DrawString(lblInpDays.Text, Font, brushDraw, 
                    new RectangleF(ptLeftRight.X + lblInpDays.Left, ptLeftRight.Y + lblInpDays.Top, lblInpDays.Width, lblInpDays.Height), 
                    drawFormat);
            }
            
            grfx.DrawLine(this.penBlack, ptStart, ptEnd);
            
            // �������� �� ʱ���� �ָ���
            ptStart = new Point(ptLeftRight.X,          ptLeftRight.Y + lblOperedDays.Top + lblOperedDays.Height);
            ptEnd   = new Point(ptLeftRight.X + width,  ptStart.Y);
            
            if (blnTitle)
            {
                grfx.DrawString(lblOperedDays.Text, Font, brushDraw, 
                    new RectangleF(ptLeftRight.X + lblOperedDays.Left, ptLeftRight.Y + lblOperedDays.Top, 
                                   lblOperedDays.Width, lblOperedDays.Height),
                    drawFormat);
            }
            
            grfx.DrawLine(this.penBlack, ptStart, ptEnd);
            
            // ʱ�����ָ���
            ptStart = new Point(ptLeftRight.X,          ptLeftRight.Y + lblDateInterval.Top + lblDateInterval.Height / 2 - 2);
            ptEnd   = new Point(ptLeftRight.X + width,  ptStart.Y);
            
            if (blnTitle)
            {
                grfx.DrawString(lblDateInterval.Text, Font, brushDraw, 
                    new RectangleF(ptLeftRight.X + lblDateInterval.Left, ptLeftRight.Y + lblDateInterval.Top + lblDateInterval.Height / 3, 
                                   lblDateInterval.Width, lblDateInterval.Height),
                    drawFormat);
            }            
        }
        
        
        /// <summary>
        /// дһ������
        /// </summary>
        /// <param name="grfx">��ͼ������</param>
        /// <param name="rectUnit">��ͼ��Χ</param>
        /// <param name="curDate">����</param>
        /// <param name="inpDays">��Ժ����</param>
        /// <param name="operedDays">��������</param>
        /// <param name="start">��ʼʱ��</param>
        /// <param name="interval">ʱ����</param>
        /// <param name="times">����</param>
        private void drawOneTitle(ref Graphics grfx, Rectangle rectUnit, string curDate, 
            string inpDays, string operedDays, int start, int interval, int times, bool blnLast)
        {
            RectangleF  rect    = new RectangleF(0, 0, 1, 1);
                        
            drawFormat.Alignment = StringAlignment.Center;
            
            // ����ߵĺ��� (��һ�첻��)
            Point ptStart = new Point(rectUnit.Left + rectUnit.Width, rectUnit.Top);
            Point ptEnd   = new Point(ptStart.X,                      rectUnit.Top + rectUnit.Height);
            grfx.DrawLine((blnLast ? penBlackBold: penRedBold), ptStart, ptEnd);
            
            // ����
            rect = new RectangleF(rectUnit.Left, rectUnit.Top + lblDate.Top, rectUnit.Width, lblDate.Height);
            grfx.DrawString(curDate, Font, brushBlue, rect, drawFormat);
            
            // סԺ���� (��ɫ)
            rect = new RectangleF(rectUnit.Left, rectUnit.Top + lblInpDays.Top, rectUnit.Width, lblInpDays.Height);
            grfx.DrawString(inpDays, Font, brushBlue, rect, drawFormat);
            
            // ��������
            rect = new RectangleF(rectUnit.Left, rectUnit.Top + lblOperedDays.Top, rectUnit.Width, lblOperedDays.Height);
            grfx.DrawString(operedDays, Font, brushBlue, rect, drawFormat);
            
            // ʱ����
            int textTop             = rectUnit.Top + lblDateInterval.Top + 6;
            
            int timeSepLineTop      = rectUnit.Top + lblOperedDays.Top + lblOperedDays.Height;
            int timeSepLineBottom   = rectUnit.Top + rectUnit.Height;
            
            int left                = 0;
            int timePoint           = 0;
                        
            // ʱ��̶�
            SolidBrush brushUse = brushDraw;
            
            for(int i = 0; i < times; i++)
            {
                // ȷ��ʱ��̶���ɫ
                if (i == 0 || i == times - 1 || i == times - 2)
                {
                    brushUse = brushRed;
                }
                else
                {
                    brushUse = brushDraw;
                }
                
                // дʱ��̶�
                rect = new RectangleF(rectUnit.Left + SzUnit.Width * i - 1, textTop, SzUnit.Width + 3, SzUnit.Height + 1);
                
                timePoint = start + interval * i;
                
                rect.X -= 1;
                rect.Width += 1;
                
                grfx.DrawString(timePoint.ToString(), Font, brushUse, rect, drawFormat);
                
                left = rectUnit.Left + SzUnit.Width * (i + 1);
                
                // ��ʱ��̶���
                if (i < times - 1)
                {
                    grfx.DrawLine(penBlack, new Point(left , timeSepLineTop), new Point(left, timeSepLineBottom));
                }
            }
        }
        
        
        /// <summary>
        /// �����(���)
        /// </summary>
        /// <param name="bmp">��ͼ������</param>
        /// <param name="offset_Right">�������ұ߽�ľ���</param>
        public void DrawRuler(ref Graphics grfx, Point ptLeftRight, Size szRng, int pos)
        {
            int OFF_RULTER = 2;                 // �̶ȱ�ע���ߵľ���
                        
            SizeF sz = grfx.MeasureString("180 ", Font);
            
            // ������
            Point ptStart = new Point(ptLeftRight.X + pos,  ptLeftRight.Y);
            Point ptEnd   = new Point(ptLeftRight.X + pos,  ptLeftRight.Y + szRng.Height);
            
            grfx.DrawLine(penBlack, ptStart, ptEnd);
            
            // д����
            grfx.DrawString("����", Font, brushRed, ptLeftRight.X + 15, ptLeftRight.Y + 10);
            grfx.DrawString("����", Font, brushBlue, ptLeftRight.X + 55, ptLeftRight.Y + 10);
            
            // ����任, ���̶�
            Point ptZero = new Point(ptStart.X, ptStart.Y + szRng.Height);      // ��������
            drawFormat.Alignment = StringAlignment.Near;
            
            // ����
            int val = temperBase + 1;
            while(val <= 42)
            {
                RectangleF rect = new RectangleF(ptZero.X + OFF_RULTER, 
                    ptZero.Y - SzUnit.Height * 5 * (val - temperBase) - SzUnit.Height / 2, 
                    szRng.Width - pos, 
                    sz.Height);
                
                grfx.DrawString(val.ToString() + "��", Font, brushBlue, rect, drawFormat);
                
                val++;
            }
            
            // ����
            val = pulseBase + 20;
            drawFormat.Alignment = StringAlignment.Center;
            
            while(val <= 180)
            {
                RectangleF rect = new RectangleF(ptStart.X - sz.Width - OFF_RULTER, 
                                                 ptZero.Y - SzUnit.Height * 5 * (val / pulseBase - 1) - SzUnit.Height / 2, 
                                                 sz.Width, sz.Height);
                
                grfx.DrawString(val.ToString(), Font, brushRed, rect, drawFormat);
                
                val += pulseBase;
            }
        }
        
        
        /// <summary>
        /// �����϶ȿ̶ȱ��
        /// </summary>
        /// <param name="bmp"></param>
        public void DrawRulerF(ref Graphics grfx, Point ptLeftRight, Size szRng, int pos)
        {
            // �ı�����ϵ
            grfx.ResetTransform();
            grfx.TranslateTransform(ptLeftRight.X, ptLeftRight.Y + szRng.Height);
            // grfx.ScaleTransform(1, -1);
            
            drawFormat.Alignment = StringAlignment.Near;
                        
            float fStart    = 94;
            float fEnd      = 107.4F;
            float f         = 0;
            float step      = 0.2F;
            string text     = string.Empty;
            
            Point ptStart   = new Point(0, pos);
            Point ptEnd     = new Point(10, pos);
            
            SizeF sz = grfx.MeasureString("18000", Font);
            
            int count = 0;
            RectangleF rect = new RectangleF(10, pos + szRng.Height * -1 - 20, szRng.Width, sz.Height);
            grfx.DrawString("�� ��", Font, brushDraw, rect, drawFormat);
            
            // ���߿�
            Rectangle rect0 = new Rectangle(0, -1 * szRng.Height,  szRng.Width - 3, szRng.Height - 3);
            //grfx.DrawRectangle(penBlack, rect0);
            
            f = fEnd;
            while (f >= fStart)
            {
                ptStart.Y   = (int)(pos + (szRng.Height * -1) + (fEnd - f) * SzUnit.Height * 5 / 9 * 5);
                ptEnd.Y     = ptStart.Y;
                
                // �̶�ֵ
                if ((count++ - 2) % 5 == 0)
                {
                    // �̶���
                    ptEnd.X = 15;
                    grfx.DrawLine(penBlack, ptStart, ptEnd);
                    
                    rect = new RectangleF(ptEnd.X, ptStart.Y - sz.Height / 2, sz.Width, sz.Height);
                    
                    text = ((int)f).ToString();
                    grfx.DrawString(text, Font, brushDraw, rect, drawFormat);
                    
                    rect.X += grfx.MeasureString(text, Font).Width - 4;
                    rect.Y -= 6;
                    grfx.DrawString("��", Font, brushDraw, rect, drawFormat);
                }
                else
                {
                    // �̶���
                    ptEnd.X = 10;
                    grfx.DrawLine(penBlack, ptStart, ptEnd);
                }
                
                f -= step;
            }
        }
        
                
        /// <summary>
        /// ��ͼ�������
        /// </summary>
        public void DrawPicGrid(ref Graphics grfx, Point ptLeftRight, Size szRng)
        {
            // �ı�����ϵ
            grfx.ResetTransform();
            // grfx.TranslateTransform(ptLeftRight.X, ptLeftRight.Y + szRng.Height);
            grfx.TranslateTransform(0, szRng.Height + 2 * ptLeftRight.Y);
            grfx.ScaleTransform(1, -1);
            
            Pen penUse = this.penBlack;
            
            // ����
            int x       = 1 + SzUnit.Width;     // ��Ԫ���ұߵ�����
            int count   = 1;                    // �����Ƿ�û���Ԫ�ָ���
            while(x < szRng.Width)
            {
                penUse = this.penBlack;
                
                if (count % timesPerDay == 0)
                {
                    if (x > szRng.Width - SzUnit.Width)
                    {
                        penUse = this.penBlackBold;
                    }
                    else
                    {
                        penUse = this.penRedBold;
                    }
                }
                
                grfx.DrawLine(penUse, x + ptLeftRight.X, ptLeftRight.Y, x + ptLeftRight.X, ptLeftRight.Y + szRng.Height);
                
                x += SzUnit.Width;
                count++;
            }
            
            // ����
            int y = SzUnit.Height + 1;
            count = 1;
            while(y < szRng.Height)
            {
                penUse = this.penBlack;
                
                if (count == 15)            // ��Ϊ37��
                {
                    penUse = this.penRedBold;
                }
                else if (count % 5 == 0)
                {
                    penUse = this.penBlackBold;
                }
                                
                grfx.DrawLine(penUse, ptLeftRight.X + 0, ptLeftRight.Y + y, ptLeftRight.X + szRng.Width, ptLeftRight.Y + y);
                
                y += SzUnit.Height;
                count++;
            }
        }

        
        /// <summary>
        /// ��������Ϣ������
        /// </summary>
        public void DrawAppendGrid0(ref Graphics grfx, Point ptLeftRight, Size szRng)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            
            int width       = szRng.Width;
            int bottom      = ptLeftRight.Y;
            int left        = ptLeftRight.X + 2;
            int row         = 0;

            grfx.DrawString("����(��/��)", Font, brushDraw, new PointF(left, ptLeftRight.Y + 4));
            
            bottom += BaseCellHeight;
            grfx.DrawLine(this.penBlack, new Point(ptLeftRight.X + 0, bottom), new Point(ptLeftRight.X + width, bottom));

            if (DsAppendItem == null || DsAppendItem.Tables.Count == 0)
            {
                return;
            }

            int count = 0;
            foreach (DataRow dr in DsAppendItem.Tables[0].Rows)
            {
                count++;

                string itemName = dr["ITEM_NAME"].ToString().Trim();
                string itemUnit = dr["ITEM_UNIT"].ToString().Trim();
                
                if (itemUnit.Length > 0)
                {
                    itemName = itemName + "(" + itemUnit + ")";
                }

                row++;
                grfx.DrawString(itemName, Font, brushDraw, new PointF(left, ptLeftRight.Y + BaseCellHeight * row + 4));
                bottom += BaseCellHeight;

                if (count != DsAppendItem.Tables[0].Rows.Count)
                {
                    grfx.DrawLine(this.penBlack, new Point(ptLeftRight.X + 0, bottom), new Point(ptLeftRight.X + width, bottom));
                }
            }
        }
        
        
        /// <summary>
        /// ��������Ϣ���
        /// </summary>
        public void DrawAppendGrid(ref Graphics grfx, Point ptLeftRight, Size szRng)
        {
            int width       = szRng.Width;
            int bottom      = ptLeftRight.Y;
            
            // ����
            for(int i = 0; i < 9; i++)
            {                    
                bottom += BaseCellHeight;
                grfx.DrawLine(this.penBlack, new Point(ptLeftRight.X + 0, bottom), new Point(ptLeftRight.X + width, bottom));
            }
            
            // ����
            int x       = ptLeftRight.X + 1 + SzUnit.Width;
            int count   = 1;
            
            while(x < ptLeftRight.X + width)
            {
                if (count % timesPerDay == 0)
                {
                    if (count == timesPerDay * ComConst.VAL.DAYS_PER_WEEK)
                    {
                        grfx.DrawLine(this.penBlackBold, x, ptLeftRight.Y, x, ptLeftRight.Y + BaseCellHeight);  // ������ָ������Ϊ ��ɫ
                    }
                    else
                    {
                        grfx.DrawLine(this.penRedBold, x, ptLeftRight.Y, x, ptLeftRight.Y + BaseCellHeight);    // ������ָ���Ϊ ��ɫ
                    }
                    
                    grfx.DrawLine(this.penBlackBold, x, ptLeftRight.Y + BaseCellHeight, x, ptLeftRight.Y + szRng.Height); // �����Ĵ�ָ���Ϊ ��ɫ
                }
                else
                {
                    grfx.DrawLine(this.penBlack, x, ptLeftRight.Y, x, ptLeftRight.Y + BaseCellHeight);          // ����С�ָ���
                }
                
                x += SzUnit.Width;
                count++;
            }
        }
        #endregion
        
        
        /// <summary>
        /// ��������(����,����,����,����)
        /// </summary>
        public void DrawMeasurePoints(ref Graphics grfx, Point ptLeftRight, Size szRng)
        {
            DateTime dtEnd = dtStart.AddDays(ComConst.VAL.DAYS_PER_WEEK);
            
            string filter = "VITAL_SIGNS = '{0}' "
                            + "AND TIME_POINT >= " + HISPlus.SqlManager.SqlConvert(dtStart.ToString(ComConst.FMT_DATE.SHORT))
                            + " AND TIME_POINT < " + HISPlus.SqlManager.SqlConvert(dtEnd.ToString(ComConst.FMT_DATE.SHORT));
            
            DataRow[] drFind    = null;
            
            PointF[] ptArrAll   = new PointF[data.Tables[MEASURE_REC].Rows.Count];
            PointF[] ptArrAdd   = null; 
            PointF[] ptArrDraw  = null;
            int      valRng     = 0;
            
            grfx.ResetTransform();
            grfx.TranslateTransform(ptLeftRight.X, ptLeftRight.Y + szRng.Height);
            grfx.ScaleTransform(1, -1);
            
            // ----------------------- ���� --------------------------
            drFind = data.Tables[MEASURE_REC].Select(string.Format(filter, "����"), "TIME_POINT ASC");
            ptArrAdd = drawMeasurePoints(ref grfx, enmMeasure.PULSE, ref drFind);                
            
            // ������ͼ
            if (ptArrAdd.Length > 1)
            {
                grfx.DrawLines(this.penRedBold, ptArrAdd);
            }
            
            // ----------------------- ���� --------------------------
            drFind = data.Tables[MEASURE_REC].Select(string.Format(filter, "����"), "TIME_POINT ASC");
            ptArrAdd = drawMeasurePoints(ref grfx, enmMeasure.HEARTRATE, ref drFind);
            
            // ������ͼ
            if (ptArrAdd.Length > 1)
            {
                grfx.DrawLines(this.penRedBold, ptArrAdd);
            }
            
            // ----------------------- ���� --------------------------
            // ��������
            drFind = data.Tables[MEASURE_REC].Select(string.Format(filter, "��������"), "TIME_POINT ASC");
            ptArrAdd = drawMeasurePoints(ref grfx, enmMeasure.MOUSE, ref drFind);
            
            mergePointArr(ref ptArrAll, ref ptArrAdd, ref valRng);
            
            // Ҹ������
            drFind = data.Tables[MEASURE_REC].Select(string.Format(filter, "Ҹ������"), "TIME_POINT ASC");
            ptArrAdd = drawMeasurePoints(ref grfx, enmMeasure.ARMPIT, ref drFind);
            
            mergePointArr(ref ptArrAll, ref ptArrAdd, ref valRng);
            
            // ֱ������
            drFind = data.Tables[MEASURE_REC].Select(string.Format(filter, "ֱ������"), "TIME_POINT ASC");
            ptArrAdd = drawMeasurePoints(ref grfx, enmMeasure.ANUS, ref drFind);
            
            mergePointArr(ref ptArrAll, ref ptArrAdd, ref valRng);
            
            // ������ͼ
            ptArrDraw = new PointF[valRng];
            
            if (ptArrDraw.Length > 1)
            {
                for(int i = 0; i < valRng; i++)
                {
                    ptArrDraw[i].X = ptArrAll[i].X;
                    ptArrDraw[i].Y = ptArrAll[i].Y;
                }
                
                grfx.DrawLines(this.penBlueBold, ptArrDraw);
            }
            
            // ----------------------- ���� --------------------------
            DrawDownTemper(ref grfx, ptLeftRight, szRng);
        }
        
        
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="bmp"></param>
        public void DrawDownTemper(ref Graphics grfx, Point ptLeftRight, Size szRng)
        {
            grfx.ResetTransform();
            grfx.TranslateTransform(ptLeftRight.X, ptLeftRight.Y + szRng.Height);
            grfx.ScaleTransform(1, -1);
            
            DateTime    dtMeasure   = dtStart;
            Pen         pen         = new Pen(Color.Red, 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            
            PointF      ptStart     = new PointF();
            PointF      ptEnd       = new PointF();
                            
            int         hour        = 0;
            int         sect        = 0;
            
            foreach(DataRow dr in dtDownTemper.Rows)
            {
                dtMeasure = DateTime.Parse(dr["TIME_POINT"].ToString());
                TimeSpan tSpan = dtMeasure.Subtract(dtStart);
                
                sect = (int)(tSpan.TotalHours / ComConst.VAL.HOURS_PER_DAY);
                hour = (int)(tSpan.TotalHours % ComConst.VAL.HOURS_PER_DAY);
                
                if ((hour - tmStart) % tmInterval == 0 && hour >= tmStart)
                {
                    ptStart.X = (sect * timesPerDay  + (hour - tmStart) / tmInterval + 0.5F) * SzUnit.Width;
                    ptEnd.X   = ptStart.X;
                    
                    ptStart.Y = (float.Parse(dr["PRE_TEMPER"].ToString()) - temperBase) / temperStep * SzUnit.Height;
                    ptEnd.Y   = (float.Parse(dr["DOWN_TEMPER"].ToString()) - temperBase) / temperStep * SzUnit.Height;
                    
                    // ��ͼ
                    DrawLegend(ref grfx, enmMeasure.HEARTRATE, (int)ptEnd.X, (int)ptEnd.Y);
                    grfx.DrawLine(pen, ptStart, ptEnd);
                }
            }
        }
        
        
		/// <summary>
		/// �����������¼����ı���ʾ
		/// </summary>
		/// <param name="bmp"></param>
		public void DrawOtherNursingEventText(ref Graphics grfx, Point ptLeftRight, Size szRng)
		{
		    // ��ȡ����
			DateTime dtEnd = dtStart.AddDays(ComConst.VAL.DAYS_PER_WEEK);
            
			string filter = "ATTRIBUTE = '4'"
			    + " AND TIME_POINT >= " + HISPlus.SqlManager.SqlConvert(dtStart.ToString(ComConst.FMT_DATE.SHORT))
				+ " AND TIME_POINT < " + HISPlus.SqlManager.SqlConvert(dtEnd.ToString(ComConst.FMT_DATE.SHORT));
            
			DataRow[] drFind    = data.Tables[MEASURE_REC].Select(filter, "TIME_POINT ASC");
                        
            // ���ݴ���
			drawFormat.Alignment = StringAlignment.Near;
            
			int  col        = -1;   
			int  sect       = 0;    
			int  hour       = 0;    
            string strValue = string.Empty;
            
            grfx.ResetTransform();
			for(int i = 0; i < drFind.Length; i++)
			{
				DateTime dtMeasure  = DateTime.Parse(drFind[i]["TIME_POINT"].ToString());
				TimeSpan tSpan      = dtMeasure.Subtract(dtStart);
                RectangleF rect     = new RectangleF(0, 0, SzUnit.Width, SzUnit.Height * 18);
                
                strValue = drFind[i]["VITAL_SIGNS"].ToString();                             // �¼�����
				sect     = (int)(tSpan.TotalHours / ComConst.VAL.HOURS_PER_DAY);
				hour     = (int)(tSpan.TotalHours % ComConst.VAL.HOURS_PER_DAY);
                
				if (((hour - tmStart) % tmInterval == 0 || (hour - tmStart) % tmInterval == 1) 
				    && hour >= tmStart)                                                     // �ڵ�ǰʱ�������ж�Ӧ����ʾ
				{
					col =  sect * timesPerDay + (int)((hour - tmStart) / tmInterval);       // ��ʱ����
				}
				else
				{   
					col =  sect * timesPerDay + (int)((hour - tmStart) / tmInterval) + 1;   // ��ʱ����
				}
                
				rect.X  = col * SzUnit.Width + 1;
                rect.Y  = 0;

                rect.X += ptLeftRight.X;
				rect.Y += ptLeftRight.Y;
                
                // Ԥ����
                if (strValue.Trim().Equals("��Ժ") == true)
                {
                    strValue = "����" + strValue;
                }

                // ���������¼������� + ʱ��
                if (strValue.IndexOf("��Ժ") >= 0 
				    || strValue.IndexOf("����") >= 0
				    || strValue.IndexOf("����ֹͣ") >= 0)
		        {			        
				    strValue += ComConst.STR.BLANK + getTimeText(dtMeasure.ToShortTimeString());
				    grfx.DrawString(strValue, Font, brushRed, rect, this.drawFormat);
				}
				
				// ת�� ��ʽ ת**��
                if (strValue.IndexOf("ת��") >= 0 )
		        {			        
				    strValue = "ת " + drFind[i]["VITAL_SIGNS_CVALUES"].ToString();
				    grfx.DrawString(strValue, Font, brushRed, rect, this.drawFormat);
				}
                
				// ���������¼�������
                if (strValue.IndexOf("����") >= 0 
				    || strValue.IndexOf("��Ժ") >= 0
				    || strValue.IndexOf("����") >= 0)
		        {
                    if (strValue.Trim().Equals("����") == true)
                    {
                        strValue = "��  ��";
                    }
                    
				    grfx.DrawString(strValue, Font, brushRed, rect, this.drawFormat);
				}
			}			
		}
        
        
        /// <summary>
        /// ��������
        /// </summary>
        public void DrawMeasurePointsAppend(ref Graphics grfx, Point ptLeftRight, Size szRng)
        {
            DateTime dtEnd = dtStart.AddDays(ComConst.VAL.DAYS_PER_WEEK);
            
            string filterTime = "TIME_POINT >= " + HISPlus.SqlManager.SqlConvert(dtStart.ToString(ComConst.FMT_DATE.SHORT))
                      + " AND TIME_POINT < " + HISPlus.SqlManager.SqlConvert(dtEnd.ToString(ComConst.FMT_DATE.SHORT));
            
            string filter = "VITAL_SIGNS = '{0}' AND " + filterTime;
            
            DataRow[] drFind = null;
            
            grfx.ResetTransform();
            //grfx.TranslateTransform(ptLeftRight.X, ptLeftRight.Y + szRng.Height);
            //grfx.ScaleTransform(1, -1);

            if (DsAppendItem == null || DsAppendItem.Tables.Count == 0)
            {
                return;
            }

            // �����Ǳ����
            ptLeftRight.Y += OFF_SET;
            drFind = data.Tables[MEASURE_REC].Select(string.Format(filter, "����"), "TIME_POINT ASC");
            drawBreathText(ref drFind, ref grfx, ptLeftRight);
            
            foreach (DataRow dr in DsAppendItem.Tables[0].Rows)
            {
                string itemName     = dr["ITEM_NAME"].ToString().Trim();
                string filterOut    = dr["FILTER"].ToString().Trim();                           // ���ⲿָ���Ĺ�������
                string type         = dr["TYPE"].ToString();

                ptLeftRight.Y += BaseCellHeight;

                if (filterOut.Length > 0)
                {
                    drFind = data.Tables[MEASURE_REC].Select(filterOut, "TIME_POINT ASC");
                }
                else
                { 
                    drFind = data.Tables[MEASURE_REC].Select(string.Format(filter, itemName), "TIME_POINT ASC");
                }

                switch (type)
                {
                    case "0":                   // һ��һ��
                        drawOneDayOneTimeText(ref drFind, ref grfx, ptLeftRight);
                        break;

                    case "1":                   // �ϼ�
                        drawSumText(ref drFind, ref grfx, ptLeftRight, false);
                        break;

                    case "2":                   // һ������
                        drawTwoTimesPerDayText_Optional(ref drFind, ref grfx, ptLeftRight);
                        break;

                    default:
                        break;
                }
            }
        }
        
        
        /// <summary>
        /// ��ĳһ�ֲ���ֵ
        /// </summary>
        /// <param name="grfx">��ͼ������</param>
        /// <param name="measureType">�������</param>
        /// <param name="drFind">���ݼ�¼</param>
        /// <returns>��Ч����ͼ������</returns>
        private PointF[] drawMeasurePoints(ref Graphics grfx, enmMeasure measureType, ref DataRow[] drFind)
        {
            PointF[]    apt         = new PointF[drFind.Length];
            
            DateTime    dtMeasure   = dtStart;
            Pen         pen         = Pens.Red;
            
            int         count       = 0;
            int         hour        = 0;
            int         sect        = 0;

            // ���ݴ���            
            switch(measureType)
            {
                case enmMeasure.PULSE:      // ����
                case enmMeasure.HEARTRATE:  // ����
                    for(int i = 0; i < drFind.Length; i++)
                    {
                        dtMeasure = DateTime.Parse(drFind[i]["TIME_POINT"].ToString());
                        TimeSpan tSpan = dtMeasure.Subtract(dtStart);
                        
                        sect = (int)(tSpan.TotalHours / ComConst.VAL.HOURS_PER_DAY);
                        hour = (int)(tSpan.TotalHours % ComConst.VAL.HOURS_PER_DAY);
                        
                        if ((hour - tmStart) % tmInterval == 0 && hour >= tmStart)
                        {
                            count++;
                            
                            apt[i].X = (sect * timesPerDay  + (hour - tmStart) / tmInterval) * SzUnit.Width;
                            apt[i].Y = (float.Parse(drFind[i]["VITAL_SIGNS_CVALUES"].ToString()) - pulseBase) / pulseStep * SzUnit.Height;
                        }
                        else
                        {
                            apt[i].X = -1;
                        }
                    }
                    
                    pen = this.penRedBold;
                    
                    break;

                case enmMeasure.ANUS:       // �ر�
                case enmMeasure.MOUSE:      // �ڱ�
                case enmMeasure.ARMPIT:     // Ҹ��
                    for(int i = 0; i < drFind.Length; i++)
                    {
                        dtMeasure = DateTime.Parse(drFind[i]["TIME_POINT"].ToString());
                        TimeSpan tSpan = dtMeasure.Subtract(dtStart);
                        
                        sect = (int)(tSpan.TotalHours / ComConst.VAL.HOURS_PER_DAY);
                        hour = (int)(tSpan.TotalHours % ComConst.VAL.HOURS_PER_DAY);
                        if ((hour - tmStart) % tmInterval == 0 && hour >= tmStart)
                        {
                            count++;

                            apt[i].X = (sect * timesPerDay  + (hour - tmStart) / tmInterval) * SzUnit.Width;                            
                            apt[i].Y = (float.Parse(drFind[i]["VITAL_SIGNS_CVALUES"].ToString()) - temperBase) / temperStep * SzUnit.Height;
                        }
                        else
                        {
                            apt[i].X = -1;
                        }
                    }
                    
                    pen = this.penBlueBold;
                    break;

                default:
                    return null;
            }
            
            // ѡ����Ч����
            PointF[] aptVal = new PointF[count];
            count = 0;
            for(int i = 0; i < drFind.Length; i++)
            {
                if (apt[i].X > -1)
                {
                    aptVal[count].X = apt[i].X + 1 + SzUnit.Width / 2;
                    aptVal[count].Y = apt[i].Y + 1;
                    count++;
                }
            }
                        
            // ��ͼ��
            for(int i = 0; i < aptVal.Length; i++)
            {
                DrawLegend(ref grfx, measureType, (int)aptVal[i].X, (int)aptVal[i].Y);
            }
            
            // ������Ч����
            return aptVal;
        }
        
        
        /// <summary>
        /// ���������ݵ�
        /// </summary>
        /// <param name="drFind"></param>
        private void drawBreathText(ref DataRow[] drFind, ref Graphics grfx, Point ptLeftRight)
        {
            // ���ݴ���
            RectangleF rect = new RectangleF(0, 0, SzUnit.Width * 2, BaseCellHeight);
            
            drawFormat.Alignment = StringAlignment.Near;
            
            int  col     = -1;   
            int  sect    = 0;    
            int  hour    = 0;    
            bool topShow = true;            // �Ƿ���ʾ ƫ��
            
            for(int i = 0; i < drFind.Length; i++)
            {
                DateTime dtMeasure  = DateTime.Parse(drFind[i]["TIME_POINT"].ToString());
                TimeSpan tSpan      = dtMeasure.Subtract(dtStart);
                
                sect = (int)(tSpan.TotalHours / ComConst.VAL.HOURS_PER_DAY);
                hour = (int)(tSpan.TotalHours % ComConst.VAL.HOURS_PER_DAY);
                                    
                //if ((hour - tmStart) % tmInterval == 0 && hour >= tmStart)
                if (hour >= tmStart)
                {
                    col     =  sect * timesPerDay + (int)(hour / tmInterval);
                    rect.X  = ptLeftRight.X + col * SzUnit.Width + 1;
                    rect.Y  = ptLeftRight.Y + (topShow ? -3 : SzUnit.Height / 2);
                    
                    grfx.DrawString(drFind[i]["VITAL_SIGNS_CVALUES"].ToString(), Font, brushRed, rect, this.drawFormat);
                    
                    topShow = !topShow;
                }
            }
        }
        
                
        /// <summary>
        /// �����������ݵ�
        /// </summary>
        /// <param name="drFind"></param>
        private void drawSumText(ref DataRow[] drFind, ref Graphics grfx, Point ptLeftRight, bool blnTimes)
        {
            // ���ݴ���
            RectangleF rect = new RectangleF(0, ptLeftRight.Y, SzUnit.Width * timesPerDay, (float)(TwoLineHeight));
            
            drawFormat.Alignment = StringAlignment.Center;
            
            int  col     = -1;   
            int  sect    = 0;    
            float sum    = 0F;
            int times    = 0;
            
            TimeSpan tSpan;
            DateTime dtPre      = dtStart;
            
            for(int i = 0; i < drFind.Length; i++)
            {
                DateTime dtMeasure  = DateTime.Parse(drFind[i]["TIME_POINT"].ToString());
                
                if (dtPre.Day != dtMeasure.Day && sum > 0)
                {
                    tSpan = dtPre.Subtract(dtStart);
                    
                    sect = (int)(tSpan.TotalHours / ComConst.VAL.HOURS_PER_DAY);                        
                    col  =  (int)(sect * timesPerDay);
                    
                    rect.X  = ptLeftRight.X + col * SzUnit.Width + 1;
                    
                    if (blnTimes == true)
                    {
                        grfx.DrawString(times.ToString() + "/" + sum.ToString(), Font, brushRed, rect, this.drawFormat);
                    }
                    else
                    {
                        grfx.DrawString(sum.ToString(), Font, brushRed, rect, this.drawFormat);
                    }
                    
                    times   = 1;    
                    sum     = float.Parse(drFind[i]["VITAL_SIGNS_CVALUES"].ToString());
                }
                else
                {
                    times++;
                    sum += float.Parse(drFind[i]["VITAL_SIGNS_CVALUES"].ToString());
                }
                
                dtPre = dtMeasure;
            }
            
            // ���һ��
            if (sum > 0)
            {
                tSpan = dtPre.Subtract(dtStart);
                                        
                sect = (int)(tSpan.TotalHours / ComConst.VAL.HOURS_PER_DAY);
                                    
                col  =  (int)(sect * timesPerDay);
                rect.X  = ptLeftRight.X + col * SzUnit.Width + 1;
                
                if (blnTimes == true)
                {
                    grfx.DrawString(times.ToString() + "/" + sum.ToString(), Font, brushRed, rect, this.drawFormat);
                }
                else
                {
                    grfx.DrawString(sum.ToString(), Font, brushRed, rect, this.drawFormat);
                }
            }
        }                        
        
        
        /// <summary>
        /// ���ϸ�һ������ֵ�����
        /// </summary>
        /// <param name="drFind"></param>
        private void drawTwoTimesPerDayText_Restrict(ref DataRow[] drFind, ref Graphics grfx, Point ptLeftRight)
        {
            string TWO_VALUE_FLG = "O";

            // ���ݴ��� (һ���������, �����������, �Ͳ���¼)
            string[] arrValue = new string[ComConst.VAL.DAYS_PER_WEEK * 2];
            for (int i = 0; i < ComConst.VAL.DAYS_PER_WEEK * 2; i++)
            {
                arrValue[i] = string.Empty;
            }

            for (int i = 0; i < drFind.Length; i++)
            {
                DateTime dtMeasure = DateTime.Parse(drFind[i]["TIME_POINT"].ToString());
                TimeSpan tSpan = dtMeasure.Subtract(dtStart);

                int pos = (int)(tSpan.TotalHours / (ComConst.VAL.HOURS_PER_DAY / 2));
                if (arrValue[pos].Length == 0)
                {
                    arrValue[pos] = drFind[i]["VITAL_SIGNS_CVALUES"].ToString();
                }
                else
                {
                    arrValue[pos] = TWO_VALUE_FLG;
                }
            }

            // �����������糬������, �����
            for (int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {                
                if (arrValue[i * 2].Equals(TWO_VALUE_FLG) == true
                    || arrValue[i * 2 + 1].Equals(TWO_VALUE_FLG) == true)
                {
                    arrValue[i * 2]     = string.Empty;
                    arrValue[i * 2 + 1] = string.Empty;
                }
            }
            
            // ������ָ���(һ�����εķָ���)
            for (int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {
                int x = (i * timesPerDay + timesPerDay / 2) * SzUnit.Width;
                grfx.DrawLine(penBlack, x, ptLeftRight.Y, x, ptLeftRight.Y + BaseCellHeight);
            }

            // ������ֵ
            RectangleF rect = new RectangleF(0, ptLeftRight.Y + 2, SzUnit.Width * timesPerDay / 2, (float)(TwoLineHeight));
            drawFormat.Alignment = StringAlignment.Center;

            for(int i = 0; i < ComConst.VAL.DAYS_PER_WEEK * 2; i++)
            {
                if (arrValue[i].Length == 0) { continue; }

                rect.X  = ptLeftRight.X + (i * 3) * SzUnit.Width;
                
                grfx.DrawString(arrValue[i], Font, brushRed, rect, this.drawFormat);
            }
        }
        

        /// <summary>
        /// ��һ������ֵ�����, ����һ���һ��
        /// </summary>
        /// <param name="drFind"></param>
        private void drawTwoTimesPerDayText_Optional(ref DataRow[] drFind, ref Graphics grfx, Point ptLeftRight)
        {
            string TWO_VALUE_FLG = "O";

            // ���ݴ��� (һ���������, �����������, �Ͳ���¼)
            string[] arrValue = new string[ComConst.VAL.DAYS_PER_WEEK * 2];
            for (int i = 0; i < ComConst.VAL.DAYS_PER_WEEK * 2; i++)
            {
                arrValue[i] = string.Empty;
            }

            for (int i = 0; i < drFind.Length; i++)
            {
                DateTime dtMeasure = DateTime.Parse(drFind[i]["TIME_POINT"].ToString());
                TimeSpan tSpan = dtMeasure.Subtract(dtStart);

                int pos = (int)(tSpan.TotalHours / (ComConst.VAL.HOURS_PER_DAY / 2));
                if (arrValue[pos].Length == 0)
                {
                    arrValue[pos] = drFind[i]["VITAL_SIGNS_CVALUES"].ToString();
                }
                else
                {
                    arrValue[pos] = TWO_VALUE_FLG;
                }
            }

            // �����������糬������, �����
            for (int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {                
                if (arrValue[i * 2].Equals(TWO_VALUE_FLG) == true
                    || arrValue[i * 2 + 1].Equals(TWO_VALUE_FLG) == true)
                {
                    arrValue[i * 2]     = string.Empty;
                    arrValue[i * 2 + 1] = string.Empty;
                }
            }
            
            // ������ֵ
            RectangleF rectPart = new RectangleF(0, ptLeftRight.Y + 2, SzUnit.Width * (timesPerDay / 2) + 2, (float)(TwoLineHeight));
            RectangleF rectDay = new RectangleF(0, ptLeftRight.Y + 2, SzUnit.Width * timesPerDay, (float)(TwoLineHeight));

            drawFormat.Alignment = StringAlignment.Center;

            for(int day = 0; day < ComConst.VAL.DAYS_PER_WEEK; day++)
            {
                int pos = day * 2;              // arrValue�е�λ��

                // ���һ��������ֵ, �м仭�ָ���
                if (arrValue[pos].Length > 0 && arrValue[pos + 1].Length > 0)
                { 
                    int x = ptLeftRight.X + (day * timesPerDay + timesPerDay / 2) * SzUnit.Width;
                    grfx.DrawLine(penBlack, x + 1, ptLeftRight.Y - OFF_SET, x + 1, ptLeftRight.Y - OFF_SET + BaseCellHeight);

                    // ����
                    rectPart.X  = ptLeftRight.X + (day * timesPerDay) * SzUnit.Width;                    
                    grfx.DrawString(arrValue[pos], Font, brushRed, rectPart, this.drawFormat);

                    // ����
                    rectPart.X  += (timesPerDay / 2) * SzUnit.Width;                    
                    grfx.DrawString(arrValue[pos + 1], Font, brushRed, rectPart, this.drawFormat);

                    continue;
                }

                // ���һ����ֻ��һ��ֵ
                if (arrValue[pos].Length > 0 || arrValue[pos + 1].Length > 0)
                {
                    int valPos = (arrValue[pos].Length > 0? pos: pos + 1);

                    rectDay.X  = ptLeftRight.X + (day * timesPerDay) * SzUnit.Width;                    
                    grfx.DrawString(arrValue[valPos], Font, brushRed, rectDay, this.drawFormat);

                    continue;
                }
            }
        }
        

        /// <summary>
        /// ��Ѫѹ���ݵ�
        /// </summary>
        /// <param name="drFind"></param>
        private void drawOneDayOneTimeText(ref DataRow[] drFind, ref Graphics grfx, Point ptLeftRight)
        {
            // ���ݴ���
            RectangleF rect = new RectangleF(0, ptLeftRight.Y, SzUnit.Width * timesPerDay, (float)(TwoLineHeight));
            
            drawFormat.Alignment = StringAlignment.Center;
            
            int  col     = -1;   
            int  sect    = 0;    
            
            for(int i = 0; i < drFind.Length; i++)
            {
                DateTime dtMeasure  = DateTime.Parse(drFind[i]["TIME_POINT"].ToString());
                TimeSpan tSpan      = dtMeasure.Subtract(dtStart);
                
                sect = (int)(tSpan.TotalHours / ComConst.VAL.HOURS_PER_DAY);
                
                col = (int)(sect * timesPerDay);
                
                rect.X  = ptLeftRight.X + col * SzUnit.Width + 1;
                
                grfx.DrawString(drFind[i]["VITAL_SIGNS_CVALUES"].ToString(), Font, brushRed, rect, this.drawFormat);
            }
        }
        #endregion
        
        
        #region ��ͼ���� ��ͨ
        /// <summary>
        /// ��ͼ��
        /// </summary>
        /// <param name="grfx"></param>
        /// <param name="eLegend"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void DrawLegend(ref Graphics grfx, enmMeasure eLegend, int x, int y)
        {
            Rectangle rect = new Rectangle(x - SzLegend.Width / 2, y - SzLegend.Height / 2, SzLegend.Width, SzLegend.Height);
            
            switch(eLegend)
            {
                case enmMeasure.PULSE:       // ����
                    grfx.DrawEllipse(this.penRed, rect);
                    grfx.FillEllipse(this.brushRed, rect);
                    break;
                
                case enmMeasure.HEARTRATE:   // ����
                    grfx.DrawEllipse(this.penRed, rect);
                    break;
                
                case enmMeasure.MOUSE:       // �ڱ�
                    grfx.DrawEllipse(this.penBlue, rect);
                    grfx.FillEllipse(this.brushBlue, rect);
                    break;
                
                case enmMeasure.ANUS:        // �ر�
                    grfx.DrawEllipse(this.penBlue, rect);
                    break;
                
                case enmMeasure.ARMPIT:      // Ҹ��
                    grfx.DrawLine(this.penBlue, rect.Left, rect.Top, rect.Left + rect.Width, rect.Top + rect.Height);
                    grfx.DrawLine(this.penBlue, rect.Left, rect.Top + rect.Height, rect.Left + rect.Width, rect.Top);
                    break;

                default:
                    break;
            }
        }
        
        
        /// <summary>
        /// ��ͼ��
        /// </summary>
        /// <param name="grfx"></param>
        /// <param name="eLegend"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void DrawLegend(ref Bitmap bmp, enmMeasure eLegend, int x, int y)
        {
            if (bmp == null)
            {
                return;
            }
            
            Graphics grfx = Graphics.FromImage(bmp);
            
            try
            {
                DrawLegend(ref grfx, eLegend, x, y);
            }
            finally
            {
                grfx.Dispose();
            }
        }
        #endregion
        
        
        #region ���ݴ���
        /// <summary>
        /// ��ȡ�������ݵ���ʼ�������������
        /// </summary>
        private void getDateRng()
        {
            // ��ȡ�������ֵ����Сֵ
            if (data.Tables[MEASURE_REC].Rows.Count > 0)
            {
                DataRow dr  = data.Tables[MEASURE_REC].Rows[0];

                dtRngStart     = DateTime.Parse(dr["RECORDING_DATE"].ToString());
                dtRngEnd       = dtRngStart;
            }
            
            foreach(DataRow dr in data.Tables[MEASURE_REC].Rows)
            {
                DateTime dtMeasure = DateTime.Parse(dr["RECORDING_DATE"].ToString());
                
                if (dtRngStart.CompareTo(dtMeasure) > 0)
                {
                    dtRngStart = dtMeasure;
                }

                if (dtRngEnd.CompareTo(dtMeasure) < 0)
                {
                    dtRngEnd = dtMeasure;
                }
            }
            
            // �ı����ֵ, ��(���ֵ - ��Сֵ + 1) ���Ա�7����
            int weekSpan = ComConst.VAL.DAYS_PER_WEEK - 1;

            dtRngStart = dtRngStart.Date;                               // 1
            dtRngEnd   = dtRngEnd.Date;                                 // 5

            TimeSpan tsp = dtRngEnd.Subtract(dtRngStart);
            int residue  = tsp.Days % ComConst.VAL.DAYS_PER_WEEK;           // 4

            if (tsp.Days != weekSpan)
            {
                dtRngEnd = dtRngEnd.AddDays(weekSpan - residue);        // 5 + (7 - 4 - 1) = 7
            }

            dtStart = dtRngEnd.AddDays(-1 * weekSpan);                  // 7 - 7 + 1 = 1
        }

        
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <remarks>������¼�����ڵ�����</remarks>
        /// <param name="dtCurrent">Ҫ���������</param>
        /// <returns></returns>
        private string getOperedDays(DateTime dtCurrent)
        {
            // ��ȡ�������ʱ��
            string filter = "ATTRIBUTE = '4' AND VITAL_SIGNS = '����'"
				+ " AND TIME_POINT < " + HISPlus.SqlManager.SqlConvert(dtCurrent.AddDays(1).ToString(ComConst.FMT_DATE.SHORT));
            
			DataRow[] drFind    = data.Tables[MEASURE_REC].Select(filter, "TIME_POINT DESC");
            DateTime dtOper     = dtCurrent;
            TimeSpan tSpan;
            string preOperDays  = string.Empty;                         // �൹���ڶ�������������
            
            // ���û��������¼, ���ؿ�
            if (drFind.Length == 0)
            {
                return string.Empty;
            }
            
            // ��ȡ�����ڶ�����������
            if (drFind.Length > 1)
            {
                DateTime dtOperPre = DateTime.Parse(drFind[1]["TIME_POINT"].ToString());
                tSpan = dtCurrent.Date.Subtract(dtOperPre.Date);
                
                if (tSpan.Days <= ComConst.VAL.DAYS_PER_WEEK * 2)
                {
                    preOperDays = tSpan.Days.ToString();
                }
            }
            
            // ��ȡ���һ����������
            dtOper = DateTime.Parse(drFind[0]["TIME_POINT"].ToString());
            tSpan = dtCurrent.Date.Subtract(dtOper.Date);
            
            if (tSpan.Days < 0)
            {
                return preOperDays;
            }
            
            if (tSpan.Days > ComConst.VAL.DAYS_PER_WEEK * 2)
            {
                return string.Empty;
            }
            
            if (preOperDays.Length > 0)
            {
                return tSpan.Days.ToString() + "/" + preOperDays;
            }
            else
            {
                return tSpan.Days.ToString();
            }
        }
        
        
        /// <summary>
        /// ���ݴ���
        /// </summary>
        /// <returns></returns>
        public bool RefineData()
        {
            if (blnDataRefined == true)
            {
                return true;
            }
            
            // ��ȡ��������
            getDownTemperData();
            
            // ������¼ʱ��
            DataTable dtMeasure = data.Tables[MEASURE_REC];
            DataTable dtRefine  = dtMeasure.Copy();
            dtRefine.PrimaryKey = null;
            
            DataRow[] drFind = dtRefine.Select("ATTRIBUTE <> '4'", "TIME_POINT");
            
            int hour = 0;
            for(int i = 0; i < drFind.Length; i++)
            {
                DateTime timePoint = (DateTime)drFind[i]["TIME_POINT"];                
                hour = tmStart + ((int)(timePoint.Hour / tmInterval)) * tmInterval;
                
                drFind[i]["TIME_POINT"] = timePoint.ToString(ComConst.FMT_DATE.SHORT) + " " + hour.ToString() + ":00:00";
            }
            
            data.Tables.Remove(MEASURE_REC);
            data.Tables.Add(dtRefine);
                        
            blnDataRefined = true;
            
            return true;
        }
        
        
        /// <summary>
        /// �����������ݱ�
        /// </summary>
        /// <returns></returns>
        private bool createDownTemper()
        {
            dtDownTemper = new DataTable();
            
            DataColumn dc = new DataColumn("TIME_POINT", Type.GetType("System.DateTime"));
            dtDownTemper.Columns.Add(dc);
            
            dc = new DataColumn("PRE_TEMPER", Type.GetType("System.String"));
            dtDownTemper.Columns.Add(dc);
            
            dc = new DataColumn("DOWN_TEMPER", Type.GetType("System.String"));
            dtDownTemper.Columns.Add(dc);
            
            return true;
        }
        
        
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        private void getDownTemperData()
        {
            float preTemper     = 0;
            float downTemper    = 0;
            float val           = 0;
                        
            // ���ݽṹ׼��
            if (dtDownTemper == null)
            {
                createDownTemper();
            }
            
            string filter = "VITAL_CODE = '1003' OR VITAL_CODE = '1004' OR VITAL_CODE = '1005' OR VITAL_CODE = '1006'";
            DataRow[] drFind = data.Tables[MEASURE_REC].Select(filter, "TIME_POINT ASC");
            
            dtDownTemper.Rows.Clear();
            
            DataRow drNew = dtDownTemper.NewRow();
            
            for(int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];
                
                val = float.Parse(dr["VITAL_SIGNS_CVALUES"].ToString());        // ���º���¶�
                
                // ����ǽ���, ��ȡ���º��ֵ
                if ("1006".Equals(dr["VITAL_CODE"].ToString()))
                {
                    if (preTemper > 0)
                    {
                        if (downTemper == 0)                                    // ����ǳ�ʼֵ
                        {
                            downTemper = val;
                        }
                        else
                        {
                            if (preTemper > val)                                // �������, ȡ����¶�
                            {
                                downTemper = (downTemper > val ? val : downTemper);
                            }
                            else                                                // �������, ȡ����¶�
                            {
                                downTemper = (downTemper < val ? val : downTemper);
                            }
                        }
                    }
                }
                // ���������������
                else
                {
                    if (downTemper > 0)
                    {
                        drNew["PRE_TEMPER"]     = preTemper.ToString();
                        drNew["DOWN_TEMPER"]    = downTemper.ToString();
                        dtDownTemper.Rows.Add(drNew);
                        
                        drNew = dtDownTemper.NewRow();
                    }
                    
                    drNew["TIME_POINT"] = dr["TIME_POINT"];
                    downTemper          = 0;
                    preTemper           = val;
                }
            }
            
            if (downTemper > 0)
            {
                drNew["PRE_TEMPER"]     = preTemper.ToString();
                drNew["DOWN_TEMPER"]    = downTemper.ToString();
                dtDownTemper.Rows.Add(drNew);
            }        
            
            // ����ʱ��
            int hour = 0;
            foreach(DataRow dr in dtDownTemper.Rows)
            {
                DateTime timePoint = (DateTime)dr["TIME_POINT"];                
                hour = tmStart + ((int)(timePoint.Hour / tmInterval)) * tmInterval;
                
                dr["TIME_POINT"] = timePoint.ToString(ComConst.FMT_DATE.SHORT) + " " + hour.ToString() + ":00:00";
            }
        }


        /// <summary>
        /// ����ϲ�����
        /// </summary>
        /// <param name="ptArr">��Ž��������</param>
        /// <param name="ptArrSrc">Դ����</param>
        /// <param name="destRng">��Ž�����������Ч���ݸ���</param>
        private void mergePointArr(ref PointF[] ptArr, ref PointF[] ptArrSrc, ref int valRng)
        {
            // �����Ž�������鿪ʼû������
            if (valRng == 0)
            {
                ptArrSrc.CopyTo(ptArr, 0);

                valRng += ptArrSrc.Length;
                
                return;
            }

            // ��Ž����������������ƶ���������ĳ���
            for(int i = valRng - 1; i >= 0; i--)
            {
                ptArr[i + ptArrSrc.Length].X = ptArr[i].X;
                ptArr[i + ptArrSrc.Length].Y = ptArr[i].Y;
            }
            
            valRng += ptArrSrc.Length;

            // ���бȽ�
            int indexDest   = ptArrSrc.Length;
            int indexSrc    = 0;
            int index       = 0;
            
            while(indexDest < valRng && indexSrc < ptArrSrc.Length)
            {
                if (indexDest >= valRng)
                {
                    ptArr[index].X = ptArrSrc[indexSrc].X;
                    ptArr[index].Y = ptArrSrc[indexSrc].Y;

                    indexSrc++;
                    index++;

                    continue;
                }
                
                if (indexSrc >= ptArrSrc.Length)
                {
                    ptArr[index].X = ptArr[indexDest].X;
                    ptArr[index].Y = ptArr[indexDest].Y;

                    indexDest++;
                    index++;
                    
                    continue;
                }
                
                if (ptArr[indexDest].X > ptArrSrc[indexSrc].X)
                {
                    ptArr[index].X = ptArrSrc[indexSrc].X;
                    ptArr[index].Y = ptArrSrc[indexSrc].Y;
                    indexSrc++;
                }
                else
                {
                    ptArr[index].X = ptArr[indexDest].X;
                    ptArr[index].Y = ptArr[indexDest].Y;

                    indexDest++;
                }
                
                index++;
            }
        }        
        #endregion
        
        
        #region ����
        /// <summary>
        /// ��������
        /// </summary>
        public void ParsePara()
        {
            // ��ȡ�������ݵ���ʼ�������������
            getDateRng();
        }
        
        
        /// <summary>
        /// �ͷ���ռ��Դ
        /// </summary>
        public void Dispose()
        {
            penBlack.Dispose();
            penBlackBold.Dispose();
            penRedBold.Dispose();
            penRed.Dispose();
            penBlueBold.Dispose();
            penBlue.Dispose();

            brushRed.Dispose();
            brushDraw.Dispose();
            brushBlue.Dispose();
        }
        
        
		/// <summary>
		/// ͨ���������ʹ����ȡ������������
		/// </summary>
		/// <param name="strNursingItemClassCode">�������ʹ���</param>
		/// <returns>������������</returns>
		private string getNursingItemClassName(string strNursingItemClassCode)
		{
			string strFilter = "CLASS_CODE = " + HISPlus.SqlManager.SqlConvert(strNursingItemClassCode);
            
			DataRow[] dr = data.Tables[NURSING_DICT].Select(strFilter);

			if (dr.Length > 0)
			{
				return dr[0]["CLASS_NAME"].ToString();
			}
			else
			{
				return string.Empty;
			}
		}
        
        
		/// <summary>
		/// �����ֵ�ʱ���ַ�����Ϊ���ֵ�ʱ���ַ���
		/// </summary>
		/// <param name="timeNum"></param>
		/// <returns></returns>
		private string getTimeText(string timeNum)
		{
			string timeValue = string.Empty;
			string[] arrStr = timeNum.Split(Convert.ToChar(ComConst.STR.COLON));
			string hour0 = string.Empty;
			string hour1 = string.Empty;

			// Сʱ
			if(arrStr[0].Length == 1)
			{
				hour0 = "0";
				hour1 = arrStr[0][0].ToString();

			}
			else
			{
                hour0 = arrStr[0][0].ToString();
				hour1 = arrStr[0][1].ToString();
			}

			if(hour0 == "1")
			{
				timeValue += "һʮ";
			}
			if(hour0 == "2")
			{
				timeValue += "��ʮ";
			}
			      
			switch(hour1)
			{
				case "0":
					timeValue += "��";
					break;
				case "1":
					timeValue += "һ";
					break;
				case "2":
					timeValue += "��";
					break;
				case "3":
					timeValue += "��";
					break;
				case "4":
					timeValue += "��";
					break;
				case "5":
					timeValue += "��";
					break;
				case "6":
					timeValue += "��";
					break;
				case "7":
					timeValue += "��";
					break;
				case "8":
					timeValue += "��";
					break;
				case "9":
					timeValue += "��";
					break;
				default:
					break;
			}

			timeValue += "ʱ";

			// ����
			string minute0 = arrStr[1][0].ToString();
			string minute1 = arrStr[1][1].ToString();

			if(minute0 == "0" && minute1 == "0")
			{
				return timeValue;
			}

			switch(minute0)
			{
				case "0":
					timeValue += "��";
					break;
				case "1":
					timeValue += "һʮ";
					break;
				case "2":
					timeValue += "��ʮ";
					break;
				case "3":
					timeValue += "��ʮ";
					break;
				case "4":
					timeValue += "��ʮ";
					break;
				case "5":
					timeValue += "��ʮ";
					break;
				default:
					break;
			}

			switch(minute1)
			{
				case "1":
					timeValue += "һ";
					break;
				case "2":
					timeValue += "��";
					break;
				case "3":
					timeValue += "��";
					break;
				case "4":
					timeValue += "��";
					break;
				case "5":
					timeValue += "��";
					break;
				case "6":
					timeValue += "��";
					break;
				case "7":
					timeValue += "��";
					break;
				case "8":
					timeValue += "��";
					break;
				case "9":
					timeValue += "��";
					break;
				default:
					break;
			}

			timeValue += "��";

			return timeValue;
		}
        #endregion
	}
}
