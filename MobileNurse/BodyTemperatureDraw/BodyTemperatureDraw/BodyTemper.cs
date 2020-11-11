//------------------------------------------------------------------------------------
//
//  系统名称        : 医院系统
//  子系统名称      : 共通模块
//  对象类型        : 
//  类名            : BodyTemper.cs
//  功能概要        : 对体温单
//  作成者          : 付军
//  作成日          : 2007-03-20
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        : 
//  变更者          : 
//  变更内容        : 
//  版本            : 
//------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Drawing;
using System.Collections;
using Label = System.Windows.Forms.Label;

namespace HISPlus
{
	/// <summary>
	/// BodyTemper 的摘要说明。
	/// </summary>
	public class BodyTemper
	{
		public BodyTemper()
		{
            drawFormat.Alignment = StringAlignment.Center;                                      // 文本布局
		}

        
        #region 结构体定义
        public enum enmMeasure
        {
            PULSE,                              // 脉搏
            HEARTRATE,                          // 心率
            MOUSE,                              // 口表
            ANUS,                               // 肛表
            ARMPIT,                             // 腋表
            BREATH                              // 呼吸
        }
        
        #endregion
        
        
        #region 变量
        public readonly string MEASURE_REC = "VITAL_SIGNS_REC";                                 // 表名: 测量记录
        public readonly string OPER_REC    = "OPERATION";                                       // 表名: 手术记录
		public readonly string NURSING_DICT= "NURSE_TEMPERATURE_CLASS_DICT";                    // 表名: 护理类型字典
        
        // 外部控件
        public Label         lblDate        = null;                                             // 标签: 日期
        public Label         lblOperedDays  = null;                                             // 标签: 术后日数
        public Label         lblDateInterval= null;                                             // 标签: 时间间隔
        public Label         lblInpDays     = null;                                             // 标签: 住院日数
        
        public Label         lblBreath      = null;                                             // 标签: 呼吸
        
        // 作图工具
        private Pen          penBlack       = new Pen(Color.Black);                             // 画笔: 黑色 + 宽度1
        private Pen          penBlackBold   = new Pen(Color.Black, 2F);                         // 画笔: 黑色 + 宽度2
        private Pen          penRedBold     = new Pen(Color.Red, 2F);                           // 画笔: 红色 + 宽度2
        private Pen          penRed         = new Pen(Color.Red);                               // 画笔: 红色 + 宽度1
        private Pen          penBlueBold    = new Pen(Color.Blue, 2F);                          // 画笔: 蓝色 + 宽度2
        private Pen          penBlue        = new Pen(Color.Blue);                              // 画笔: 蓝色 + 宽度1
        
        private SolidBrush   brushDraw      = new SolidBrush(Color.Black);                      // 画刷: 黑色
        private SolidBrush   brushRed       = new SolidBrush(Color.Red);                        // 画刷: 红色
        private SolidBrush   brushBlue      = new SolidBrush(Color.Blue);                       // 画刷: 蓝色
        private StringFormat drawFormat     = new StringFormat();                               // 文本布局
        
        public  Color        BackColor      = Color.White;                                      // 背景色
        public  Font         Font           = new Font("宋体", 9);                              // 字体
        
        // 大小设置
        public Size          SzLegend       = new Size(8, 8);                                   // 图例大小
        public Size          SzUnit         = new Size(14, 12);                                 // 体温表格单小单元格矩阵形
        public int           BaseCellHeight = 20;                                               // 最底表格单元格的高度        
        public int           TwoLineHeight  = 30;

        private int          OFF_SET        = 4;
        
        private int          BLANK_WIDTH    = 25;                                               // 排出量 的宽度

        // 业务数据
        public DataSet       DsAppendItem   = new DataSet();                                    // 体温单下面的附加项目
        private string       hospitalName   = string.Empty;                                     // 医院名称
        private string       patientId      = string.Empty;                                     // 病人ID号
        private string       patientName    = string.Empty;                                     // 病人姓名
        private string       deptName       = string.Empty;                                     // 科室名称
        private string       bedLabel       = string.Empty;                                     // 床标
        private string       inpNo          = string.Empty;                                     // 住院号
        private string       inDeptDate     = string.Empty;                                     // 入科日期
        private DateTime     dtInp          = DateTime.Now;                                     // 入院日期
        
        private DataSet      data           = new DataSet();                                    // 数据DataSet 表结构同[VITAL_SIGNS_REC]
        private DataTable    dtDownTemper   = null;                                             // 降温数据        
        
        private int          tmStart        = 1;                                                // 每天测量的开始时间点
        private int          tmInterval     = 4;                                                // 测量时间间隔
        private int          timesPerDay    = 6;                                                // 每天测量的次数
        
        private int          temperBase     = 34;                                               // 体温从34度开始画    
        private float        temperStep     = 0.2F;                                             // 体温每0.2作为一个刻度
        private int          pulseBase      = 20;                                               // 脉搏从20开始画
        private int          pulseStep      = 4;                                                // 脉搏每4作为一个刻度
        
        private int          breathBase     = 10;                                               // 呼吸从10开始画
        private int          breathStep     = 2;                                                // 呼吸每2作为一个刻度

        private DateTime     dtStart        = DateTime.Now;                                     // 作图起始日期
        private DateTime     dtRngStart     = DateTime.Now;                                     // 数据起始日期
        private DateTime     dtRngEnd       = DateTime.Now;                                     // 数据结束日期
        
        // 状态变量
        private bool         blnDataRefined = false;                                            // 输入的数据是否整合了
        #endregion
        
        
        #region 属性
        /// <summary>
        /// 测量开始时间点
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
        /// 测量时间间隔(小时)
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
        /// 一天测量次数
        /// </summary>
        public int MeasureTimesPerDay
        {
            get
            {
                return timesPerDay;
            }
        }


        /// <summary>
        /// 属性[入院日期]
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
        /// 属性[测量数据]
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
        /// 属性[手术信息]
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
		/// 属性[护理类型]
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
        /// 属性[测量开始时间]
        /// </summary>
        public DateTime MeasureDateStart
        {
            get            
            {
                return dtRngStart;
            }
        }


        /// <summary>
        /// 属性[测量结束时间]
        /// </summary>
        public DateTime MeasureDateEnd
        {
            get
            {
                return dtRngEnd;
            }
        }

        
        /// <summary>
        /// 属性[作图起始日期]
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
        
        
        #region 作图函数
        #region 画背景
        /// <summary>
        /// 画标题 [姓名 性别 年龄  入院日期 病室 病案号]
        /// </summary>
        public void DrawTitle(ref Graphics grfx, Point ptLeftRight, Size szRng)
        {
            // 画行分隔线
            DrawTitleRowSepLines(ref grfx, ptLeftRight, szRng, false);
            
            // 标题PANEL填写内容
            int         left        = ptLeftRight.X + 1;
            DateTime    dtCurrent   = dtStart;
            DateTime    dtPre       = dtStart;
            string      curDate     = dtStart.ToString(ComConst.FMT_DATE.SHORT);
            string      inpDays     = string.Empty;                 // 住院天数
            string      operedDays  = string.Empty;                 // 术后天数
            
            string      dateFmt     = string.Empty;

            dateFmt = GVars.IniFile.ReadString("TEMPERATURE", "DATE", string.Empty);

            for(int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {
                Rectangle rect = new Rectangle(left, ptLeftRight.Y, SzUnit.Width * timesPerDay, szRng.Height);
                
                // 获取当前日期
                dtCurrent = dtStart.AddDays(i);
                
                // 获取当前日期的字符串
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
                        if (dateFmt.Length > 0)
                        {
                            // curDate = dtCurrent.ToString(dateFmt);
                            curDate = dtCurrent.Day.ToString();
                            if (curDate.Length == 1) curDate = "0" + curDate;
                        }
                        else
                        {
                            curDate = dtCurrent.ToString("dd");
                        }
                    }
                }
                
                dtPre = dtCurrent;

                // 获取患病日数
                dtStart = DateTime.Parse(dtStart.ToString(ComConst.FMT_DATE.SHORT));
                dtInp   = DateTime.Parse(dtInp.ToString(ComConst.FMT_DATE.SHORT));
                
                TimeSpan tSpan = (dtStart.AddDays(i)).Subtract(dtInp);
                inpDays = string.Empty;
                if (tSpan.Days >= 0)
                {
                    inpDays = (tSpan.Days + 1).ToString();
                }

                // 术后天数
                operedDays = getOperedDays(dtStart.AddDays(i));
                
                // 画标题
                drawOneTitle(ref grfx, rect, curDate, inpDays, operedDays, tmStart, tmInterval, timesPerDay, i == ComConst.VAL.DAYS_PER_WEEK - 1);
                
                left += SzUnit.Width * timesPerDay;
            }
        }
        
        
        /// <summary>
        /// 画标题行分隔线
        /// </summary>
        /// <param name="bmp">作图缓存</param>
        /// <param name="lblDate">日期</param>
        /// <param name="lblInpDays">住院日数</param>
        /// <param name="lblOperedDays">术后日数</param>
        public void DrawTitleRowSepLines(ref Graphics grfx, Point ptLeftRight, Size szRng, bool blnTitle)
        {
            int width   = szRng.Width;
            int height  = szRng.Height;
            
            // 最顶加粗线
            Point ptStart = new Point(ptLeftRight.X,            ptLeftRight.Y);
            Point ptEnd   = new Point(ptLeftRight.X + width,    ptStart.Y);
            
            grfx.DrawLine(this.penBlack, ptStart, ptEnd);
            
            drawFormat.Alignment = StringAlignment.Center;
            
            // 日期 与 住院日数 分隔线
            ptStart = new Point(ptLeftRight.X,          ptLeftRight.Y + lblDate.Top + lblDate.Height);
            ptEnd   = new Point(ptLeftRight.X + width,  ptStart.Y);
            
            grfx.DrawLine(this.penBlack, ptStart, ptEnd);
            
            if (blnTitle)
            {
                grfx.DrawString(lblDate.Text, Font, brushDraw, 
                    new RectangleF(ptLeftRight.X + lblDate.Left, ptLeftRight.Y + lblDate.Top, lblDate.Width, lblDate.Height), 
                    drawFormat);
            }
            
            // 住院日数 与 术后日数 分隔线
            ptStart = new Point(ptLeftRight.X,          ptLeftRight.Y + lblInpDays.Top + lblInpDays.Height);
            ptEnd   = new Point(ptLeftRight.X + width,  ptStart.Y);
            
            if (blnTitle)
            {
                grfx.DrawString(lblInpDays.Text, Font, brushDraw, 
                    new RectangleF(ptLeftRight.X + lblInpDays.Left, ptLeftRight.Y + lblInpDays.Top, lblInpDays.Width, lblInpDays.Height), 
                    drawFormat);
            }
            
            grfx.DrawLine(this.penBlack, ptStart, ptEnd);
            
            // 术后日数 与 时间间隔 分隔线
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
            
            // 时间间隔分隔线
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
        /// 写一个标题
        /// </summary>
        /// <param name="grfx">作图上下文</param>
        /// <param name="rectUnit">作图范围</param>
        /// <param name="curDate">日期</param>
        /// <param name="inpDays">入院日数</param>
        /// <param name="operedDays">术后日数</param>
        /// <param name="start">天始时间</param>
        /// <param name="interval">时间间隔</param>
        /// <param name="times">次数</param>
        private void drawOneTitle(ref Graphics grfx, Rectangle rectUnit, string curDate, 
            string inpDays, string operedDays, int start, int interval, int times, bool blnLast)
        {
            RectangleF  rect    = new RectangleF(0, 0, 1, 1);
                        
            drawFormat.Alignment = StringAlignment.Center;
            
            // 画左边的红竖 (第一天不画)
            Point ptStart = new Point(rectUnit.Left + rectUnit.Width, rectUnit.Top);
            Point ptEnd   = new Point(ptStart.X,                      rectUnit.Top + rectUnit.Height);
            grfx.DrawLine((blnLast ? penBlackBold: penRedBold), ptStart, ptEnd);
            
            // 日期
            rect = new RectangleF(rectUnit.Left, rectUnit.Top + lblDate.Top, rectUnit.Width, lblDate.Height);
            grfx.DrawString(curDate, Font, brushBlue, rect, drawFormat);
            
            // 住院日数 (蓝色)
            rect = new RectangleF(rectUnit.Left, rectUnit.Top + lblInpDays.Top, rectUnit.Width, lblInpDays.Height);
            grfx.DrawString(inpDays, Font, brushBlue, rect, drawFormat);
            
            // 术后日数
            rect = new RectangleF(rectUnit.Left, rectUnit.Top + lblOperedDays.Top, rectUnit.Width, lblOperedDays.Height);
            grfx.DrawString(operedDays, Font, brushRed, rect, drawFormat);
            
            // 时间间隔
            int textTop             = rectUnit.Top + lblDateInterval.Top + 6;
            
            int timeSepLineTop      = rectUnit.Top + lblOperedDays.Top + lblOperedDays.Height;
            int timeSepLineBottom   = rectUnit.Top + rectUnit.Height;
            
            int left                = 0;
            int timePoint           = 0;
                        
            // 时间刻度
            SolidBrush brushUse = brushDraw;
            
            for(int i = 0; i < times; i++)
            {
                // 确定时间刻度颜色
                if (i == 0 || i == 1 || i == times - 1)
                {
                    brushUse = brushRed;
                }
                else
                {
                    brushUse = brushDraw;
                }
                
                // 写时间刻度
                rect = new RectangleF(rectUnit.Left + SzUnit.Width * i - 1, textTop, SzUnit.Width + 3, SzUnit.Height + 1);
                
                timePoint = start + interval * i;
                
                rect.X -= 1;
                rect.Width += 1;
                
                grfx.DrawString(timePoint.ToString(), Font, brushUse, rect, drawFormat);
                
                left = rectUnit.Left + SzUnit.Width * (i + 1);
                
                // 画时间刻度线
                if (i < times - 1)
                {
                    grfx.DrawLine(penBlack, new Point(left , timeSepLineTop), new Point(left, timeSepLineBottom));
                }
            }
        }
        
        
        /// <summary>
        /// 画标尺(左边)
        /// </summary>
        /// <param name="bmp">作图缓冲区</param>
        /// <param name="offset_Right">尺子离右边界的距离</param>
        public void DrawRuler(ref Graphics grfx, Point ptLeftRight, Size szRng, int pos)
        {
            int OFF_RULTER = 2;                 // 刻度标注与标尺的距离
            
            SizeF sz = grfx.MeasureString("180 ", Font);
            
            // 画纵线
            Point ptStart = new Point(ptLeftRight.X + pos,  ptLeftRight.Y);
            Point ptEnd   = new Point(ptLeftRight.X + pos,  ptLeftRight.Y + szRng.Height);
            
            grfx.DrawLine(penBlack, ptStart, ptEnd);
            
            // 写标题
            grfx.DrawString("脉搏", Font, brushRed, ptLeftRight.X + 15, ptLeftRight.Y + 10);
            grfx.DrawString("体温", Font, brushBlue, ptLeftRight.X + 55, ptLeftRight.Y + 10);
            
            // 坐标变换, 画刻度
            Point ptZero = new Point(ptStart.X, ptStart.Y + szRng.Height);      // 从左下起
            drawFormat.Alignment = StringAlignment.Near;
            
            // 体温
            int val = temperBase + 1;
            while(val <= 42)
            {
                RectangleF rect = new RectangleF(ptZero.X + OFF_RULTER, 
                    ptZero.Y - SzUnit.Height * 5 * (val - temperBase) - SzUnit.Height / 2, 
                    szRng.Width - pos, 
                    sz.Height);
                
                grfx.DrawString(val.ToString() + "℃", Font, brushBlue, rect, drawFormat);
                
                val++;
            }
            
            // 脉搏
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

            // 画图例
            int y = 230 - ptLeftRight.Y;
            int sep = 10;
            int step = 28;
            int legend = 15 + ptLeftRight.X;

            DrawLegend(ref grfx, enmMeasure.MOUSE, legend, szRng.Height - y);
            y -= sep;
            grfx.DrawString("口表", Font, brushBlue, new PointF(ptLeftRight.X, szRng.Height - y));

            y -= step;
            DrawLegend(ref grfx, enmMeasure.ARMPIT, legend, szRng.Height - y);
            y -= sep;
            grfx.DrawString("腋表", Font, brushBlue, new PointF(ptLeftRight.X, szRng.Height - y));

            y -= step;
            DrawLegend(ref grfx, enmMeasure.ANUS, legend, szRng.Height - y);
            y -= sep;
            grfx.DrawString("肛表", Font, brushBlue, new PointF(ptLeftRight.X, szRng.Height - y));

            y -= step;
            DrawLegend(ref grfx, enmMeasure.PULSE, legend, szRng.Height - y);
            y -= sep;
            grfx.DrawString("脉搏", Font, brushBlue, new PointF(ptLeftRight.X, szRng.Height - y));

            y -= step;
            DrawLegend(ref grfx, enmMeasure.HEARTRATE, legend, szRng.Height - y);
            y -= sep;
            grfx.DrawString("心率", Font, brushBlue, new PointF(ptLeftRight.X, szRng.Height - y));

            y -= step;
            DrawLegend(ref grfx, enmMeasure.BREATH, legend, szRng.Height - y);
            y -= sep;
            grfx.DrawString("呼吸", Font, brushBlue, new PointF(ptLeftRight.X, szRng.Height - y));
        }
        
        
        /// <summary>
        /// 画华氏度刻度标尺
        /// </summary>
        /// <param name="bmp"></param>
        public void DrawRulerF(ref Graphics grfx, Point ptLeftRight, Size szRng, int pos)
        {
            // 改变坐标系
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
            grfx.DrawString("华 氏", Font, brushDraw, rect, drawFormat);
            
            // 画边框
            Rectangle rect0 = new Rectangle(0, -1 * szRng.Height,  szRng.Width - 3, szRng.Height - 3);
            //grfx.DrawRectangle(penBlack, rect0);
            
            f = fEnd;
            while (f >= fStart)
            {
                ptStart.Y   = (int)(pos + (szRng.Height * -1) + (fEnd - f) * SzUnit.Height * 5 / 9 * 5);
                ptEnd.Y     = ptStart.Y;
                
                // 刻度值
                if ((count++ - 2) % 5 == 0)
                {
                    // 刻度线
                    ptEnd.X = 15;
                    grfx.DrawLine(penBlack, ptStart, ptEnd);
                    
                    rect = new RectangleF(ptEnd.X, ptStart.Y - sz.Height / 2, sz.Width, sz.Height);
                    
                    text = ((int)f).ToString();
                    grfx.DrawString(text, Font, brushDraw, rect, drawFormat);
                    
                    rect.X += grfx.MeasureString(text, Font).Width - 4;
                    rect.Y -= 6;
                    grfx.DrawString("。", Font, brushDraw, rect, drawFormat);
                }
                else
                {
                    // 刻度线
                    ptEnd.X = 10;
                    grfx.DrawLine(penBlack, ptStart, ptEnd);
                }
                
                f -= step;
            }
        }
        
                
        /// <summary>
        /// 作图区域打表格
        /// </summary>
        public void DrawPicGrid(ref Graphics grfx, Point ptLeftRight, Size szRng)
        {
            // 改变坐标系
            grfx.ResetTransform();
            // grfx.TranslateTransform(ptLeftRight.X, ptLeftRight.Y + szRng.Height);
            grfx.TranslateTransform(0, szRng.Height + 2 * ptLeftRight.Y);
            grfx.ScaleTransform(1, -1);
            
            Pen penUse = this.penBlack;
            
            // 竖线
            int x       = 1 + SzUnit.Width;     // 单元格右边的坐标
            int count   = 1;                    // 计数是否该画单元分隔线
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
            
            // 横线
            int y = SzUnit.Height + 1;
            count = 1;
            while(y < szRng.Height)
            {
                penUse = this.penBlack;
                
                if (count == 15)            // 此为37度
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
        /// 画附加信息标题表格
        /// </summary>
        public void DrawAppendGrid0(ref Graphics grfx, Point ptLeftRight, Size szRng)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            
            int width       = szRng.Width;
            int bottom      = ptLeftRight.Y;
            int left        = ptLeftRight.X + 2;
            int row         = -1;

            //grfx.DrawString("呼吸(次/分)", Font, brushDraw, new PointF(left, ptLeftRight.Y + 4));
            
            //bottom += BaseCellHeight;
            //grfx.DrawLine(this.penBlack, new Point(ptLeftRight.X + 0, bottom), new Point(ptLeftRight.X + width, bottom));

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
                
                int    rows     = 1;
                if (dr["ROW_HEIGHT"].ToString().Trim().Length > 0)
                {
                    rows = int.Parse(dr["ROW_HEIGHT"].ToString());
                }
                
                if (itemUnit.Length > 0)
                {
                    itemName = itemName + "(" + itemUnit + ")";
                }
                
                row++;

                // 写标题
                if (2 <= count && count <= 4)
                {
                    string tempText = "排";
                    if (count == 3) { tempText = "出"; }
                    if (count == 4) { tempText = "量"; }

                    grfx.DrawString(tempText, Font, brushDraw, new PointF(left, ptLeftRight.Y + BaseCellHeight * row + 4));

                    grfx.DrawString(itemName, Font, brushDraw, new PointF(left + BLANK_WIDTH, ptLeftRight.Y + BaseCellHeight * row + 4));
                }
                else if (rows == 1)
                {
                    grfx.DrawString(itemName, Font, brushDraw, new PointF(left, ptLeftRight.Y + BaseCellHeight * row + 4));
                }
                else
                {
                    PointF point = new PointF();
                    point.X = left;
                    point.Y = (float)(ptLeftRight.Y + BaseCellHeight * (row + ((float)(rows - 1) / 2.0 )) + 4);

                    grfx.DrawString(itemName, Font, brushDraw, point);
                    
                    row += (rows - 1);
                }

                // 画底线
                bottom += BaseCellHeight * rows;
                
                if (count != DsAppendItem.Tables[0].Rows.Count)
                {
                    if (count == 2 || count == 3)
                    {
                        grfx.DrawLine(this.penBlack, new Point(ptLeftRight.X + BLANK_WIDTH, bottom), new Point(ptLeftRight.X + width, bottom));
                        grfx.DrawLine(this.penBlack, new Point(ptLeftRight.X + BLANK_WIDTH, bottom - BaseCellHeight), new Point(ptLeftRight.X + BLANK_WIDTH, bottom));
                    }
                    else if (count == 4)
                    { 
                        grfx.DrawLine(this.penBlack, new Point(ptLeftRight.X + 0, bottom), new Point(ptLeftRight.X + width, bottom));
                        grfx.DrawLine(this.penBlack, new Point(ptLeftRight.X + BLANK_WIDTH, bottom - BaseCellHeight), new Point(ptLeftRight.X + BLANK_WIDTH, bottom));
                    }
                    else
                    {
                        grfx.DrawLine(this.penBlack, new Point(ptLeftRight.X + 0, bottom), new Point(ptLeftRight.X + width, bottom));
                    }
                }
            }
        }
        
        
        /// <summary>
        /// 画附加信息表格
        /// </summary>
        public void DrawAppendGrid(ref Graphics grfx, Point ptLeftRight, Size szRng)
        {
            int width       = szRng.Width;
            int bottom      = ptLeftRight.Y;
            
            // 横向
            for(int i = 0; i < DsAppendItem.Tables[0].Rows.Count; i++)
            {
                bottom += BaseCellHeight;
                grfx.DrawLine(this.penBlack, new Point(ptLeftRight.X + 0, bottom), new Point(ptLeftRight.X + width, bottom));
            }
            
            // 纵向
            int x       = ptLeftRight.X + 1 + SzUnit.Width;
            int count   = 1;
            
            while(x < ptLeftRight.X + width)
            {
                if (count % timesPerDay == 0)
                {
                    //if (count == timesPerDay * ComConst.VAL.DAYS_PER_WEEK)
                    //{
                    //    grfx.DrawLine(this.penBlackBold, x, ptLeftRight.Y, x, ptLeftRight.Y + BaseCellHeight);  // 呼吸大分隔线最后为 黑色
                    //}
                    //else
                    //{
                    //    grfx.DrawLine(this.penRedBold, x, ptLeftRight.Y, x, ptLeftRight.Y + BaseCellHeight);    // 呼吸大分隔线为 红色
                    //}
                    
                    // grfx.DrawLine(this.penBlackBold, x, ptLeftRight.Y + BaseCellHeight, x, ptLeftRight.Y + szRng.Height); // 其它的大分隔线为 黑色
                    grfx.DrawLine(this.penBlackBold, x, ptLeftRight.Y, x, ptLeftRight.Y + szRng.Height); // 其它的大分隔线为 黑色
                }
                else
                {
                    //grfx.DrawLine(this.penBlack, x, ptLeftRight.Y, x, ptLeftRight.Y + BaseCellHeight);          // 呼吸小分隔线
                }
                
                x += SzUnit.Width;
                count++;
            }
        }
        #endregion
        
        
        /// <summary>
        /// 画测量点(脉搏,心率,体温,降温)
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
            
            // ----------------------- 脉搏 --------------------------
            drFind = data.Tables[MEASURE_REC].Select(string.Format(filter, "脉搏"), "TIME_POINT ASC");
            ptArrAdd = drawMeasurePoints(ref grfx, enmMeasure.PULSE, ref drFind);                
            
            // 画折线图
            if (ptArrAdd.Length > 1)
            {
                grfx.DrawLines(this.penRed, ptArrAdd);
            }
            
            // ----------------------- 心率 --------------------------
            drFind = data.Tables[MEASURE_REC].Select(string.Format(filter, "心率"), "TIME_POINT ASC");
            ptArrAdd = drawMeasurePoints(ref grfx, enmMeasure.HEARTRATE, ref drFind);
            
            // 画折线图
            if (ptArrAdd.Length > 1)
            {
                grfx.DrawLines(this.penRed, ptArrAdd);
            }
            

            // ----------------------- 呼吸 --------------------------
            drFind = data.Tables[MEASURE_REC].Select(string.Format(filter, "呼吸"), "TIME_POINT ASC");
            ptArrAdd = drawMeasurePoints(ref grfx, enmMeasure.BREATH, ref drFind);
            
            // 画折线图
            if (ptArrAdd.Length > 1)
            {
                grfx.DrawLines(this.penBlue, ptArrAdd);
            }
            
            // ----------------------- 体温 --------------------------
            filter += " AND VITAL_SIGNS_CVALUES > '35'";

            // 口内体温
            drFind = data.Tables[MEASURE_REC].Select(string.Format(filter, "口内体温"), "TIME_POINT ASC");
            ptArrAdd = drawMeasurePoints(ref grfx, enmMeasure.MOUSE, ref drFind);
            
            mergePointArr(ref ptArrAll, ref ptArrAdd, ref valRng);
            
            // 腋下体温
            drFind = data.Tables[MEASURE_REC].Select(string.Format(filter, "腋下体温"), "TIME_POINT ASC");
            ptArrAdd = drawMeasurePoints(ref grfx, enmMeasure.ARMPIT, ref drFind);
            
            mergePointArr(ref ptArrAll, ref ptArrAdd, ref valRng);
            
            // 直肠体温
            drFind = data.Tables[MEASURE_REC].Select(string.Format(filter, "直肠体温"), "TIME_POINT ASC");
            ptArrAdd = drawMeasurePoints(ref grfx, enmMeasure.ANUS, ref drFind);
            
            mergePointArr(ref ptArrAll, ref ptArrAdd, ref valRng);
            
            // 作折线图
            ptArrDraw = new PointF[valRng];
            
            if (ptArrDraw.Length > 1)
            {
                for(int i = 0; i < valRng; i++)
                {
                    ptArrDraw[i].X = ptArrAll[i].X;
                    ptArrDraw[i].Y = ptArrAll[i].Y;
                }
                
                grfx.DrawLines(this.penBlue, ptArrDraw);
            }
            
            // ----------------------- 降温 --------------------------
            DrawDownTemper(ref grfx, ptLeftRight, szRng);
        }
        
        
        /// <summary>
        /// 画降温数据
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
                    
                    // 画图
                    DrawLegend(ref grfx, enmMeasure.HEARTRATE, (int)ptEnd.X, (int)ptEnd.Y);
                    grfx.DrawLine(pen, ptStart, ptEnd);
                }
            }
        }
        
        
		/// <summary>
		/// 画其它护理事件的文本显示
		/// </summary>
		/// <param name="bmp"></param>
		public void DrawOtherNursingEventText(ref Graphics grfx, Point ptLeftRight, Size szRng)
		{
		    // 获取数据
			DateTime dtEnd = dtStart.AddDays(ComConst.VAL.DAYS_PER_WEEK);
            
			string filter = "ATTRIBUTE = '4'"
			    + " AND TIME_POINT >= " + HISPlus.SqlManager.SqlConvert(dtStart.ToString(ComConst.FMT_DATE.SHORT))
				+ " AND TIME_POINT < " + HISPlus.SqlManager.SqlConvert(dtEnd.ToString(ComConst.FMT_DATE.SHORT));
            
			DataRow[] drFind    = data.Tables[MEASURE_REC].Select(filter, "TIME_POINT ASC");
            
            // 数据处理
			drawFormat.Alignment = StringAlignment.Near;
            
            RectangleF rect = new RectangleF(0, 0, SzUnit.Width, SzUnit.Height * 18);

			int  col        = -1;   
			int  sect       = 0;    
			int  hour       = 0;    
            string strValue = string.Empty;
            
            int colPre      = -1;
            int count       = 0;                                        // 最多两个事件放在一列上

            grfx.ResetTransform();
			for(int i = 0; i < drFind.Length; i++)
			{
                // 预处理
                strValue = drFind[i]["VITAL_SIGNS"].ToString();                             // 事件名称

                if (strValue.EndsWith("降温") == true)
                {
                    continue;
                }
                
                // 正常处理
				DateTime dtMeasure  = DateTime.Parse(drFind[i]["TIME_POINT"].ToString());
				TimeSpan tSpan      = dtMeasure.Subtract(dtStart);
                
				sect     = (int)(tSpan.TotalHours / ComConst.VAL.HOURS_PER_DAY);
				hour     = (int)(tSpan.TotalHours % ComConst.VAL.HOURS_PER_DAY);
                
				if (((hour - tmStart) % tmInterval == 0 || (hour - tmStart) % tmInterval == 1) 
				    && hour >= tmStart)                                                     // 在当前时间列上有对应的显示
				{
					col =  sect * timesPerDay + (int)((hour - tmStart) / tmInterval);       // 定时间列
				}
				else
				{   
					col =  sect * timesPerDay + (int)((hour - tmStart) / tmInterval) + 1;   // 定时间列
				}
                
                if (col != colPre)
                {
                    colPre  = col;
                    count   = 0;
                }
                
                // 预处理
                //if (strValue.Trim().Equals("入院") == true)
                //{
                //    strValue = "今日" + strValue;
                //}
                
				// 转科 格式 转**科
                if (strValue.IndexOf("转科") >= 0)
                {
                    strValue = drFind[i]["VITAL_SIGNS_CVALUES"].ToString();
                }
                else if (strValue.Equals("不升") == true)
                {
                    strValue = drFind[i]["VITAL_SIGNS_CVALUES"].ToString();
                }
                // 其它护理事件的名称 + 时间
                else
                {
                    strValue += getTimeText(dtMeasure.ToString(ComConst.FMT_DATE.TIME_SHORT));
                }

                // 定位
                if (strValue.Equals("不升") == true)
                {
                    rect.X  = col * SzUnit.Width + 1;
                    rect.Y = SzUnit.Height * (7 * 5 + 3);
                }
                else
                {
                    count++;
                    if (count > 2) { continue;}

                    if (count == 2 && col < timesPerDay * ComConst.VAL.DAYS_PER_WEEK - 1)
                    {
                        col++;
                    }

                    rect.X  = col * SzUnit.Width + 1;
                    rect.Y = SzUnit.Height * 3;
                }

                rect.X += ptLeftRight.X;
			    rect.Y += ptLeftRight.Y;
                
                // 输出
                if (strValue.Equals("不升") == true)
                {
                    grfx.DrawString(strValue, Font, brushBlue, rect, this.drawFormat);
                }
                else
                { 
                    grfx.DrawString(strValue, Font, brushRed, rect, this.drawFormat);
                }
			}
		}
        
        
        /// <summary>
        /// 画测量点
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

            // 呼吸是必须的
            ptLeftRight.Y += OFF_SET;
            //drFind = data.Tables[MEASURE_REC].Select(string.Format(filter, "呼吸"), "TIME_POINT ASC");
            //drawBreathText(ref drFind, ref grfx, ptLeftRight);
            
            // 灌肠
            DrawMeasurePointsAppend_Stool(ref grfx, ptLeftRight);

            foreach (DataRow dr in DsAppendItem.Tables[0].Rows)
            {
                string itemName     = dr["ITEM_NAME"].ToString().Trim();
                string filterOut    = dr["FILTER"].ToString().Trim();                           // 在外部指定的过滤条件
                string type         = dr["TYPE"].ToString();

                int    rows         = 1;
                if (dr["ROW_HEIGHT"].ToString().Length > 0)
                {
                    rows = int.Parse(dr["ROW_HEIGHT"].ToString());
                }

                // 对皮试单独处理
                if (itemName.Equals("皮试") == true)
                { 
                    string filterTemp = "VITAL_SIGNS LIKE '皮试%' AND " + filterTime;

                    drFind = data.Tables[MEASURE_REC].Select(filterTemp, "TIME_POINT ASC");
                    drawTwoTimesPerDayText_TwoRows(ref drFind, ref grfx, ptLeftRight);
                    
                    ptLeftRight.Y += BaseCellHeight * rows;
                    continue;
                }
                
                // 其它正常情况的处理
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
                    case "0":                   // 一天一次
                        drawOneDayOneTimeText(ref drFind, ref grfx, ptLeftRight);
                        break;

                    case "1":                   // 合计
                        drawSumText(ref drFind, ref grfx, ptLeftRight, false);
                        break;

                    case "2":                   // 一天两次
                        drawTwoTimesPerDayText_Optional(ref drFind, ref grfx, ptLeftRight);
                        break;

                    default:
                        break;
                }

                ptLeftRight.Y += BaseCellHeight * rows;
            }
        }
        
                
        /// <summary>
        /// 画测量点
        /// </summary>
        public void DrawMeasurePointsAppend_Stool(ref Graphics grfx, Point ptLeftRight)
        {
            DateTime dtEnd = dtStart.AddDays(ComConst.VAL.DAYS_PER_WEEK);
            
            string filterTime = "TIME_POINT >= " + HISPlus.SqlManager.SqlConvert(dtStart.ToString(ComConst.FMT_DATE.SHORT))
                      + " AND TIME_POINT < " + HISPlus.SqlManager.SqlConvert(dtEnd.ToString(ComConst.FMT_DATE.SHORT));
            
            string filter = "(VITAL_SIGNS LIKE '%前大便' OR VITAL_SIGNS LIKE '%后大便' OR VITAL_SIGNS = '灌肠次数') AND " + filterTime;           
            DataRow[] drFind = data.Tables[MEASURE_REC].Select(filter, "TIME_POINT ASC");
            
            grfx.ResetTransform();
            
            ptLeftRight.Y += BaseCellHeight;

            drawStool(ref drFind, ref grfx, ptLeftRight);   
        }
        
        
        /// <summary>
        /// 画某一种测量值
        /// </summary>
        /// <param name="grfx">作图上下文</param>
        /// <param name="measureType">点的类型</param>
        /// <param name="drFind">数据记录</param>
        /// <returns>有效的作图点坐标</returns>
        private PointF[] drawMeasurePoints(ref Graphics grfx, enmMeasure measureType, ref DataRow[] drFind)
        {
            PointF[]    apt         = new PointF[drFind.Length];
            
            DateTime    dtMeasure   = dtStart;
            Pen         pen         = Pens.Red;
            
            int         count       = 0;
            int         hour        = 0;
            int         sect        = 0;

            // 数据处理            
            switch(measureType)
            {
                case enmMeasure.PULSE:      // 脉搏
                case enmMeasure.HEARTRATE:  // 心率
                    for(int i = 0; i < drFind.Length; i++)
                    {
                        if (DataType.IsPositive(drFind[i]["VITAL_SIGNS_CVALUES"].ToString()) == false)
                        {
                            continue;
                        }

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

                case enmMeasure.ANUS:       // 肛表
                case enmMeasure.MOUSE:      // 口表
                case enmMeasure.ARMPIT:     // 腋表
                    for(int i = 0; i < drFind.Length; i++)
                    {
                        dtMeasure = DateTime.Parse(drFind[i]["TIME_POINT"].ToString());
                        TimeSpan tSpan = dtMeasure.Subtract(dtStart);
                        
                        if (DataType.IsPositive(drFind[i]["VITAL_SIGNS_CVALUES"].ToString()) == false)
                        {
                            continue;
                        }

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

                case enmMeasure.BREATH:  // 呼吸
                    for(int i = 0; i < drFind.Length; i++)
                    {
                        if (DataType.IsPositive(drFind[i]["VITAL_SIGNS_CVALUES"].ToString()) == false)
                        {
                            continue;
                        }

                        dtMeasure = DateTime.Parse(drFind[i]["TIME_POINT"].ToString());
                        TimeSpan tSpan = dtMeasure.Subtract(dtStart);
                        
                        sect = (int)(tSpan.TotalHours / ComConst.VAL.HOURS_PER_DAY);
                        hour = (int)(tSpan.TotalHours % ComConst.VAL.HOURS_PER_DAY);
                        
                        if ((hour - tmStart) % tmInterval == 0 && hour >= tmStart)
                        {
                            count++;
                            
                            apt[i].X = (sect * timesPerDay  + (hour - tmStart) / tmInterval) * SzUnit.Width;
                            apt[i].Y = (float.Parse(drFind[i]["VITAL_SIGNS_CVALUES"].ToString()) - breathBase) / breathStep * SzUnit.Height;
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
            
            // 选择有效数据
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
                        
            // 画图例
            for(int i = 0; i < aptVal.Length; i++)
            {
                DrawLegend(ref grfx, measureType, (int)aptVal[i].X, (int)aptVal[i].Y);
            }
            
            // 返回有效数据
            return aptVal;
        }
        
        
        /// <summary>
        /// 画呼吸数据点
        /// </summary>
        /// <param name="drFind"></param>
        private void drawBreathText(ref DataRow[] drFind, ref Graphics grfx, Point ptLeftRight)
        {
            // 数据处理
            RectangleF rect = new RectangleF(0, 0, SzUnit.Width * 2, BaseCellHeight);
            
            drawFormat.Alignment = StringAlignment.Near;
            
            int  col     = -1;   
            int  sect    = 0;    
            int  hour    = 0;    
            bool topShow = true;            // 是否显示 偏上
            
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
        /// 画总入量数据点
        /// </summary>
        /// <param name="drFind"></param>
        private void drawSumText(ref DataRow[] drFind, ref Graphics grfx, Point ptLeftRight, bool blnTimes)
        {
            // 数据处理
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

                if (DataType.IsPositive(drFind[i]["VITAL_SIGNS_CVALUES"].ToString()) == false)
                {
                    continue;
                }

                if (dtPre.Day != dtMeasure.Day && sum > 0)
                {
                    tSpan = dtPre.Subtract(dtStart);
                    
                    sect = (int)(tSpan.TotalHours / ComConst.VAL.HOURS_PER_DAY);                        
                    col  =  (int)(sect * timesPerDay);
                    
                    rect.X  = ptLeftRight.X + col * SzUnit.Width + 1;
                    
                    if (blnTimes == true)
                    {
                        grfx.DrawString(times.ToString() + "/" + sum.ToString(), Font, brushBlue, rect, this.drawFormat);
                    }
                    else
                    {
                        grfx.DrawString(sum.ToString(), Font, brushBlue, rect, this.drawFormat);
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
            
            // 最后一项
            if (sum > 0)
            {
                tSpan = dtPre.Subtract(dtStart);
                                        
                sect = (int)(tSpan.TotalHours / ComConst.VAL.HOURS_PER_DAY);
                                    
                col  =  (int)(sect * timesPerDay);
                rect.X  = ptLeftRight.X + col * SzUnit.Width + 1;
                
                if (blnTimes == true)
                {
                    grfx.DrawString(times.ToString() + "/" + sum.ToString(), Font, brushBlue, rect, this.drawFormat);
                }
                else
                {
                    grfx.DrawString(sum.ToString(), Font, brushBlue, rect, this.drawFormat);
                }
            }
        }                        
        
        
        /// <summary>
        /// 画严格一天两次值的情况
        /// </summary>
        /// <param name="drFind"></param>
        private void drawTwoTimesPerDayText_Restrict(ref DataRow[] drFind, ref Graphics grfx, Point ptLeftRight)
        {
            string TWO_VALUE_FLG = "O";

            // 数据处理 (一天最多两次, 如果超过两次, 就不记录)
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

            // 如果上午或下午超过两次, 不输出
            for (int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {                
                if (arrValue[i * 2].Equals(TWO_VALUE_FLG) == true
                    || arrValue[i * 2 + 1].Equals(TWO_VALUE_FLG) == true)
                {
                    arrValue[i * 2]     = string.Empty;
                    arrValue[i * 2 + 1] = string.Empty;
                }
            }
            
            // 画纵向分隔线(一天两次的分隔线)
            for (int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {
                int x = (i * timesPerDay + timesPerDay / 2) * SzUnit.Width;
                grfx.DrawLine(penBlack, x, ptLeftRight.Y, x, ptLeftRight.Y + BaseCellHeight);
            }

            // 画测量值
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
        /// 画一天两次值的情况, 可能一天就一次
        /// </summary>
        /// <param name="drFind"></param>
        private void drawTwoTimesPerDayText_Optional(ref DataRow[] drFind, ref Graphics grfx, Point ptLeftRight)
        {
            string TWO_VALUE_FLG = "O";

            // 数据处理 (一天最多两次, 如果超过两次, 就不记录)
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

            // 如果上午或下午超过两次, 不输出
            for (int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {                
                if (arrValue[i * 2].Equals(TWO_VALUE_FLG) == true
                    || arrValue[i * 2 + 1].Equals(TWO_VALUE_FLG) == true)
                {
                    arrValue[i * 2]     = string.Empty;
                    arrValue[i * 2 + 1] = string.Empty;
                }
            }
            
            // 画测量值
            RectangleF rectPart = new RectangleF(0, ptLeftRight.Y + 2, SzUnit.Width * (timesPerDay / 2) + 2, (float)(TwoLineHeight));
            RectangleF rectDay = new RectangleF(0, ptLeftRight.Y + 2, SzUnit.Width * timesPerDay, (float)(TwoLineHeight));

            drawFormat.Alignment = StringAlignment.Center;

            for(int day = 0; day < ComConst.VAL.DAYS_PER_WEEK; day++)
            {
                int pos = day * 2;              // arrValue中的位置

                // 如果一天有两个值, 中间画分隔线
                if (arrValue[pos].Length > 0 && arrValue[pos + 1].Length > 0)
                { 
                    int x = ptLeftRight.X + (day * timesPerDay + timesPerDay / 2) * SzUnit.Width;
                    grfx.DrawLine(penBlack, x + 1, ptLeftRight.Y - OFF_SET, x + 1, ptLeftRight.Y - OFF_SET + BaseCellHeight);

                    // 上午
                    rectPart.X  = ptLeftRight.X + (day * timesPerDay) * SzUnit.Width;                    
                    grfx.DrawString(arrValue[pos], Font, brushBlue, rectPart, this.drawFormat);

                    // 下午
                    rectPart.X  += (timesPerDay / 2) * SzUnit.Width;                    
                    grfx.DrawString(arrValue[pos + 1], Font, brushBlue, rectPart, this.drawFormat);

                    continue;
                }

                // 如果一天中只有一个值
                if (arrValue[pos].Length > 0 || arrValue[pos + 1].Length > 0)
                {
                    int valPos = (arrValue[pos].Length > 0? pos: pos + 1);

                    rectDay.X  = ptLeftRight.X + (day * timesPerDay) * SzUnit.Width;                    
                    grfx.DrawString(arrValue[valPos], Font, brushBlue, rectDay, this.drawFormat);

                    continue;
                }
            }
        }
        
                
        /// <summary>
        /// 画一天两次值的情况, 可能一天就一次
        /// </summary>
        /// <param name="drFind"></param>
        private void drawTwoTimesPerDayText_TwoRows(ref DataRow[] drFind, ref Graphics grfx, Point ptLeftRight)
        {
            // 数据处理 (一天最多两次, 如果超过两次, 就不记录)
            string[] arrValue = new string[ComConst.VAL.DAYS_PER_WEEK * 2];
            for (int i = 0; i < ComConst.VAL.DAYS_PER_WEEK * 2; i++)
            {
                arrValue[i] = string.Empty;
            }

            for (int i = 0; i < drFind.Length; i++)
            {
                DateTime dtMeasure = DateTime.Parse(drFind[i]["TIME_POINT"].ToString());
                TimeSpan tSpan = dtMeasure.Subtract(dtStart);

                int pos = (int)(tSpan.TotalHours / (ComConst.VAL.HOURS_PER_DAY)) * 2;

                if (arrValue[pos].Length == 0)
                {
                    arrValue[pos] = drFind[i]["VITAL_SIGNS_CVALUES"].ToString();
                }
                else if (arrValue[pos + 1].Length == 0)
                { 
                    arrValue[pos + 1] = drFind[i]["VITAL_SIGNS_CVALUES"].ToString();
                }
            }
            
            // 
            RectangleF rectDay = new RectangleF(0, ptLeftRight.Y + 2, SzUnit.Width * timesPerDay, (float)(TwoLineHeight));

            drawFormat.Alignment = StringAlignment.Center;

            for(int day = 0; day < ComConst.VAL.DAYS_PER_WEEK; day++)
            {
                int pos = day * 2;              // arrValue中的位置

                // 如果一天有两个值, 分上下两行写
                if (arrValue[pos].Length > 0)
                {
                    rectDay.X  = ptLeftRight.X + (day * timesPerDay) * SzUnit.Width;
                    // grfx.DrawString(arrValue[pos], Font, brushBlue, rectDay, this.drawFormat);
                    drawStringColored(ref grfx, rectDay, arrValue[pos]);
                }

                if (arrValue[pos + 1].Length > 0)
                { 
                    rectDay.X  = ptLeftRight.X + (day * timesPerDay) * SzUnit.Width;
                    rectDay.Y += BaseCellHeight;

                    // grfx.DrawString(arrValue[pos + 1], Font, brushBlue, rectDay, this.drawFormat);
                    drawStringColored(ref grfx, rectDay, arrValue[pos + 1]);

                    rectDay.Y += BaseCellHeight;
                }
            }
        }
        

        /// <summary>
        /// 画大便
        /// </summary>
        /// <param name="drFind"></param>
        private void drawStool(ref DataRow[] drFind, ref Graphics grfx, Point ptLeftRight)
        {
            string preVal      = "0";                           // 灌肠前次数
            string nextVal     = "0";                           // 灌肠后次数
            string infuseCount = "0";                           // 灌肠次数

            Font font = new Font("宋体", 8);                    // 字体

            StringFormat sfCenter = new StringFormat();         // 文本布局
            sfCenter.Alignment = StringAlignment.Center;

            DateTime dtPre = DateTime.Now;

            for (int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];

                if (i == 0) { dtPre = (DateTime)dr["TIME_POINT"];}
                DateTime dtNow = (DateTime)dr["TIME_POINT"];

                if (dtNow.Year != dtPre.Year || dtNow.Month != dtPre.Month || dtNow.Day != dtPre.Day)
                {
                    // 输出
                    if (preVal.Equals("0") == false || nextVal.Equals("0") == false)
                    {
                        TimeSpan tSpan = dtPre.Subtract(dtStart);
                        int pos = (int)(tSpan.TotalHours / (ComConst.VAL.HOURS_PER_DAY));

                        // 前大便
                        PointF point = ptLeftRight;
                        point.X += SzUnit.Width * (pos * 6 + 2);

                        grfx.DrawString(preVal, font, brushBlue, point);

                        // 后大便
                        SizeF sz = grfx.MeasureString(preVal, font);
                        point.X += sz.Width + 1;              
                        point.Y -= 5;
                        
                        RectangleF rect = new RectangleF();
                        rect.X = point.X - 4;
                        rect.Y = point.Y;
                        rect.Width = 16;
                        rect.Height = 10;

                        grfx.DrawString(nextVal, font, brushBlue, rect, sfCenter);

                        // E
                        rect.Y += 11;
                        if (infuseCount.Equals("1") == true || infuseCount.Equals("0") == true)
                        {
                            grfx.DrawString("E", font, brushBlue, rect, sfCenter);
                        }
                        else
                        {
                            grfx.DrawString(infuseCount + "E", font, brushBlue, rect, sfCenter);
                        }

                        // 横线
                        point.X -= 2;
                        point.Y = rect.Top;
                        grfx.DrawLine(penBlack, point, new PointF(point.X + 14, point.Y));
                    }

                    // 中间变量
                    preVal = "0";
                    nextVal = "0";

                    dtPre = dtNow;
                }

                string itemName = dr["VITAL_SIGNS"].ToString();

                if (itemName.EndsWith("前大便") == true)
                { 
                    preVal = dr["VITAL_SIGNS_CVALUES"].ToString();
                }
                
                if (itemName.EndsWith("后大便") == true)
                { 
                    nextVal = dr["VITAL_SIGNS_CVALUES"].ToString();
                }

                if (itemName.Equals("灌肠次数") == true)
                {
                    infuseCount = dr["VITAL_SIGNS_CVALUES"].ToString();
                }
            }

            // 如果上午或下午超过两次, 不输出
            // 输出
            if (preVal.Equals("0") == false || nextVal.Equals("0") == false)
            {
                TimeSpan tSpan = dtPre.Subtract(dtStart);
                int pos = (int)(tSpan.TotalHours / (ComConst.VAL.HOURS_PER_DAY));

                // 前大便
                PointF point = ptLeftRight;
                point.X += SzUnit.Width * (pos * 6 + 2);

                grfx.DrawString(preVal, font, brushBlue, point);

                // 后大便
                SizeF sz = grfx.MeasureString(preVal, font);
                point.X += sz.Width + 1;              
                point.Y -= 5;
                
                RectangleF rect = new RectangleF();
                rect.X = point.X - 4;
                rect.Y = point.Y;
                rect.Width = 16;
                rect.Height = 10;

                grfx.DrawString(nextVal, font, brushBlue, rect, sfCenter);

                // E
                rect.Y += 11;
                if (infuseCount.Equals("1") == true || infuseCount.Equals("0") == true)
                {
                    grfx.DrawString("E", font, brushBlue, rect, sfCenter);
                }
                else
                {
                    grfx.DrawString(infuseCount + "E", font, brushBlue, rect, sfCenter);
                }
                
                // 横线
                point.X -= 2;
                point.Y = rect.Top;
                grfx.DrawLine(penBlack, point, new PointF(point.X + 14, point.Y));
            }

            sfCenter.Dispose();
        }
        

        /// <summary>
        /// 画血压数据点
        /// </summary>
        /// <param name="drFind"></param>
        private void drawOneDayOneTimeText(ref DataRow[] drFind, ref Graphics grfx, Point ptLeftRight)
        {
            // 数据处理
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
                
                // grfx.DrawString(drFind[i]["VITAL_SIGNS_CVALUES"].ToString(), Font, brushBlue, rect, this.drawFormat);
                drawStringColored(ref grfx, rect, drFind[i]["VITAL_SIGNS_CVALUES"].ToString());
            }
        }
        #endregion
        
        
        #region 作图函数 共通
        /// <summary>
        /// 画图例
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
                case enmMeasure.PULSE:       // 脉搏
                    grfx.DrawEllipse(this.penRed, rect);
                    grfx.FillEllipse(this.brushRed, rect);
                    break;
                
                case enmMeasure.HEARTRATE:   // 心率
                    grfx.DrawEllipse(this.penRed, rect);
                    break;
                
                case enmMeasure.MOUSE:       // 口表
                    grfx.DrawEllipse(this.penBlue, rect);
                    grfx.FillEllipse(this.brushBlue, rect);
                    break;
                
                case enmMeasure.ANUS:        // 肛表
                    grfx.DrawEllipse(this.penBlue, rect);
                    break;
                
                case enmMeasure.ARMPIT:      // 腋表
                    grfx.DrawLine(this.penBlue, rect.Left, rect.Top, rect.Left + rect.Width, rect.Top + rect.Height);
                    grfx.DrawLine(this.penBlue, rect.Left, rect.Top + rect.Height, rect.Left + rect.Width, rect.Top);
                    break;

                case enmMeasure.BREATH:     // 呼吸
                    grfx.DrawEllipse(this.penBlue, rect);
                    grfx.FillEllipse(this.brushBlue, rect);
                    break;

                default:
                    break;
            }
        }
        
        
        /// <summary>
        /// 画图例
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


        public void drawStringColored(ref Graphics grfx, RectangleF rect, string text)
        {
            string COLORED_TEXT = "(阳性)";

            text = text.Trim();
            text = text.Replace("（", "(");
            text = text.Replace("）", ")");

            int pos = text.IndexOf(COLORED_TEXT);

            if (pos >= 0)
            {
                // 准备
                SizeF sz = grfx.MeasureString(text, Font);
                if (rect.Width < sz.Width)
                {
                    rect.Width = sz.Width;
                }
                else
                {
                    rect.X += (rect.Width - sz.Width) / 2;
                    rect.Width = sz.Width;
                }

                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;

                // 开始
                string pre = text.Substring(0, pos);
                sz = grfx.MeasureString(pre, Font);
                grfx.DrawString(pre, Font, brushBlue, rect, sf);
                
                // 本体
                rect.X += sz.Width - 4;
                grfx.DrawString(COLORED_TEXT, Font, brushRed, rect, sf);
            }
            else
            {
                grfx.DrawString(text, Font, brushBlue, rect, this.drawFormat);
            }
        }
        #endregion
        
        
        #region 数据处理
        /// <summary>
        /// 获取测量数据的起始日期与结束日期
        /// </summary>
        private void getDateRng()
        {
            // 获取日期最大值与最小值
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
            
            // 改变最大值, 让(最大值 - 最小值 + 1) 可以被7整除
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
        /// 获取术后天数
        /// </summary>
        /// <remarks>手术记录按日期倒排序</remarks>
        /// <param name="dtCurrent">要计算的日期</param>
        /// <returns></returns>
        private string getOperedDays(DateTime dtCurrent)
        {
            // 获取最近手术时间
            string filter = "ATTRIBUTE = '4' AND VITAL_SIGNS = '手术'"
				+ " AND TIME_POINT > " + HISPlus.SqlManager.SqlConvert(dtCurrent.AddDays(-14).ToString(ComConst.FMT_DATE.SHORT))
                + " AND TIME_POINT < " + HISPlus.SqlManager.SqlConvert(dtCurrent.AddDays(1).ToString(ComConst.FMT_DATE.SHORT));
            
			DataRow[] drFind    = data.Tables[MEASURE_REC].Select(filter, "TIME_POINT DESC");
            DateTime dtOper     = dtCurrent;
            TimeSpan tSpan;
            string preOperDays  = string.Empty;                         // 距倒数第二次手术的天数
            string operedDays   = string.Empty;                         // 术后天数

            // 如果没有手术记录, 返回空
            if (drFind.Length == 0)
            {
                return string.Empty;
            }
            
            // 获取倒数第三次手术天数
            if (drFind.Length > 2)
            {
                DateTime dtOperPre = DateTime.Parse(drFind[2]["TIME_POINT"].ToString());
                tSpan = dtCurrent.Date.Subtract(dtOperPre.Date);
                
                if (tSpan.Days >= 0 && tSpan.Days <= ComConst.VAL.DAYS_PER_WEEK * 2)
                {
                    if (operedDays.Length > 0)
                    {
                        operedDays = tSpan.Days.ToString() + @"/" + operedDays;
                    }
                    else
                    { 
                        operedDays = tSpan.Days.ToString();
                    }
                }
            }

            // 获取倒数第二次手术天数
            if (drFind.Length > 1)
            {
                DateTime dtOperPre = DateTime.Parse(drFind[1]["TIME_POINT"].ToString());
                tSpan = dtCurrent.Date.Subtract(dtOperPre.Date);
                
                if (tSpan.Days >= 0 && tSpan.Days <= ComConst.VAL.DAYS_PER_WEEK * 2)
                {
                    if (operedDays.Length > 0)
                    {
                        operedDays = tSpan.Days.ToString() + @"/" + operedDays;
                    }
                    else
                    { 
                        operedDays = tSpan.Days.ToString();
                    }
                }
            }
            
            // 获取最近一次手术天数
            dtOper = DateTime.Parse(drFind[0]["TIME_POINT"].ToString());
            tSpan = dtCurrent.Date.Subtract(dtOper.Date);
            
            if (tSpan.Days >= 0)
            {
                if (operedDays.Length > 0)
                {
                    operedDays = tSpan.Days.ToString() + @"/" + operedDays;
                }
                else
                { 
                    operedDays = tSpan.Days.ToString();
                }
            }
            
            return operedDays;
        }
        
        
        /// <summary>
        /// 数据处理
        /// </summary>
        /// <returns></returns>
        public bool RefineData()
        {
            if (blnDataRefined == true)
            {
                return true;
            }
            
            // 获取降温数据
            getDownTemperData();
            
            // 归整记录时间
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
        /// 创建降温数据表
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
        /// 获取降温数据
        /// </summary>
        private void getDownTemperData()
        {
            float preTemper     = 0;
            float downTemper    = 0;
            float val           = 0;
                        
            // 数据结构准备
            if (dtDownTemper == null)
            {
                createDownTemper();
            }
            
            // string filter = "VITAL_CODE = '1003' OR VITAL_CODE = '1004' OR VITAL_CODE = '1005' OR VITAL_CODE = '1006'";
            string filter = "VITAL_SIGNS LIKE '%体温' OR VITAL_SIGNS LIKE '%降温'";

            DataRow[] drFind = data.Tables[MEASURE_REC].Select(filter, "TIME_POINT ASC");
            
            dtDownTemper.Rows.Clear();
            
            DataRow drNew = dtDownTemper.NewRow();
            
            for(int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];

                if (dr["VITAL_SIGNS_CVALUES"].ToString().Length == 0)
                {
                    continue;
                }

                val = float.Parse(dr["VITAL_SIGNS_CVALUES"].ToString());        // 降温后的温度
                
                // 如果是降温, 获取降温后的值
                // if ("1006".Equals(dr["VITAL_CODE"].ToString()))
                if ((dr["VITAL_SIGNS"].ToString()).EndsWith("降温") == true)
                {
                    if (preTemper > 0)
                    {
                        if (downTemper == 0)                                    // 如果是初始值
                        {
                            downTemper = val;
                        }
                        else
                        {
                            if (preTemper > val)                                // 如果降温, 取最低温度
                            {
                                downTemper = (downTemper > val ? val : downTemper);
                            }
                            else                                                // 如果升温, 取最高温度
                            {
                                downTemper = (downTemper < val ? val : downTemper);
                            }
                        }
                    }
                }
                // 如果是正常量体温
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
            
            // 归整时间
            int hour = 0;
            foreach(DataRow dr in dtDownTemper.Rows)
            {
                DateTime timePoint = (DateTime)dr["TIME_POINT"];                
                hour = tmStart + ((int)(timePoint.Hour / tmInterval)) * tmInterval;
                
                dr["TIME_POINT"] = timePoint.ToString(ComConst.FMT_DATE.SHORT) + " " + hour.ToString() + ":00:00";
            }
        }


        /// <summary>
        /// 有序合并数组
        /// </summary>
        /// <param name="ptArr">存放结果的数组</param>
        /// <param name="ptArrSrc">源数组</param>
        /// <param name="destRng">存放结果的数组的有效数据个数</param>
        private void mergePointArr(ref PointF[] ptArr, ref PointF[] ptArrSrc, ref int valRng)
        {
            // 如果存放结果的数组开始没有数据
            if (valRng == 0)
            {
                ptArrSrc.CopyTo(ptArr, 0);

                valRng += ptArrSrc.Length;
                
                return;
            }

            // 存放结果的数组整体向后移动新增数组的长度
            for(int i = valRng - 1; i >= 0; i--)
            {
                ptArr[i + ptArrSrc.Length].X = ptArr[i].X;
                ptArr[i + ptArrSrc.Length].Y = ptArr[i].Y;
            }
            
            valRng += ptArrSrc.Length;

            // 进行比较
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
        
        
        #region 方法
        /// <summary>
        /// 分析参数
        /// </summary>
        public void ParsePara()
        {
            // 获取测量数据的起始日期与结束日期
            getDateRng();
        }
        
        
        /// <summary>
        /// 释放所占资源
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
		/// 通过护理类型代码获取护理类型名称
		/// </summary>
		/// <param name="strNursingItemClassCode">护理类型代码</param>
		/// <returns>护理类型名称</returns>
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
		/// 将数字的时间字符串变为文字的时间字符串
		/// </summary>
		/// <param name="timeNum"></param>
		/// <returns></returns>
		private string getTimeText(string timeNum)
		{
            string timeText = string.Empty;
            string hour     = string.Empty;
            string minute   = string.Empty;

            // HH:mm
            for (int i = 0; i < 5; i++)
            {
                string val = timeNum.Substring(i, 1);

                switch (i)
                {
                    case 0:
                        if ("0".Equals(val) == false)
                        {
                            if ("1".Equals(val) == false)
                            {
                                hour = getCaptionNum(val);
                            }

                            hour += "十";
                        }
                        break;
                    case 1:
                        if ("0".Equals(val) == true)
                        {
                            if (hour.Length == 0)
                            {
                                hour = "零";
                            }
                        }
                        else
                        {
                            hour += getCaptionNum(val);
                        }

                        hour += "时";

                        break;
                    case 2:
                        break;
                    case 3:
                        if ("0".Equals(val) == false)
                        {
                            if ("1".Equals(val) == false)
                            {
                                minute = getCaptionNum(val);
                            }

                            minute += "十";
                        }
                        break;
                    case 4:
                        if ("0".Equals(val) != true)
                        {
                            minute += getCaptionNum(val);
                        }

                        if (minute.Length > 0)
                        {
                            minute += "分";
                        }

                        break;
                    default:
                        break;
                }
            }
            
            return hour + minute;
		}


        private string getCaptionNum(string smallNum)
        {
            switch (smallNum)
            {
                case "0": return "零";
                case "1": return "一";
                case "2": return "二";
                case "3": return "三";
                case "4": return "四";
                case "5": return "五";
                case "6": return "六";
                case "7": return "七";
                case "8": return "八";
                case "9": return "九";
                default: return smallNum;
            }
        }
        #endregion
	}
}
