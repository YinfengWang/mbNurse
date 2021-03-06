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
using System.Drawing.Drawing2D;
using System.Collections;
using System.Text;

using Label = DevExpress.XtraEditors.LabelControl;

using GraphPointType = HISPlus.BodyTemperParams.GraphPointType;

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
        
        
        #region 结构体
        /// <summary>
        /// 画点状态
        /// </summary>
        public enum GraphPointStatus
        {
            NORMAL          = 0,                // 正常
            TEMPERATURE_H   = 1,                // 体温过高
            TEMPERATURE_L   = 2,                // 体温过低
            PULSE_H         = 3,                // 脉搏过高
            PULSE_NO        = 4                 // 脉搏不清
        }
        #endregion
        
        
        #region 变量
        private const int           STEP_HOURS          = 4;                                    // 每四个小时一个单元格
        
        protected BodyTemperParams  _bodyTemperParams   = null;                                 // 体温单参数
        
        protected DateTime          _dtStart            = DataType.DateTime_Null();             // 作图起始日期
        protected DateTime          _dtNow              = DataType.DateTime_Null();             // 当前的日期
        protected DateTime          _dtInp              = DataType.DateTime_Null();             // 入院日期               
        
        private DateTime            _dtRngStart         = DataType.DateTime_Null();             // 数据起始日期
        private DateTime            _dtRngEnd           = DataType.DateTime_Null();             // 数据结束日期
        
        protected DataSet           _dsNursing          = null;                                 // 护理数据
                                                                                                // 如 -4 表示从前一天的20点开到 第二天的20点算一天
        protected int               _graphStarTime      = 0;                                    // 体温开始时间点
        
        // 作图工具
        private StringFormat        drawFormat          = new StringFormat();                   // 文本布局
        public  Font                fontText            = new Font("宋体", 11);                 // 字体
        
        // 临时缓存
        private DataTable           dtDownTemper        = null;                                 // 降温数据
        private ArrayList           arrBreakCols        = new ArrayList();                      // 断开点
        #endregion
        
        
        #region 属性
        /// <summary>
        /// 体温单参数
        /// </summary>
        public BodyTemperParams Params
        {
            get { return _bodyTemperParams; }
            set { _bodyTemperParams = value;}
        }              
        
        
        /// <summary>
        /// 属性[入院日期]
        /// </summary>
        public DateTime DtInp
        {
            get
            {
                return _dtInp;
            }
            set
            {
                _dtInp = value;
            }
        }
        
        
        /// <summary>
        /// 当前日期
        /// </summary>
        public DateTime DtNow
        {
            get {return _dtNow;}
            set {_dtNow = value;}
        }
        
        
        /// <summary>
        /// 属性[作图起始日期]
        /// </summary>
        public DateTime DtStart
        {
            get
            {
                return _dtStart;
            }
            set
            {
                _dtStart = value;
            }
        }    
               
        
        /// <summary>
        /// 护理数据
        /// </summary>
        public DataSet DsNursing
        {
            get {return _dsNursing; }
            set {_dsNursing = value;}
        }                                             
        
        
        /// <summary>
        /// 属性[测量开始时间]
        /// </summary>
        public DateTime MeasureDateStart
        {
            get
            {
                return _dtRngStart;
            }
        }


        /// <summary>
        /// 属性[测量结束时间]
        /// </summary>
        public DateTime MeasureDateEnd
        {
            get
            {
                return _dtRngEnd;
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
        public void DrawLegend(ref Graphics grfx, GraphPointType eLegend, int x, int y)
        {
            Size szUnit = new Size();
            szUnit.Width = (int)_bodyTemperParams.SzUnit.Width / 2;
            szUnit.Height = (int)_bodyTemperParams.SzUnit.Height / 2;
            
            Rectangle rect = new Rectangle(x - szUnit.Width / 2, y - szUnit.Height / 2, szUnit.Width, szUnit.Height);
            
            switch(eLegend)
            {
                case GraphPointType.PULSE:       // 脉搏
                    grfx.DrawEllipse(Pens.Red, rect );
                    grfx.FillEllipse(Brushes.Red, rect);
                    break;
                
                case GraphPointType.HEARTRATE:   // 心率
                    rect.X -= 2;
                    rect.Width += 4;
                    rect.Y -= 2;
                    rect.Height += 4;
                    
                    grfx.DrawEllipse(Pens.Red, rect);
                    break;
                
                case GraphPointType.MOUSE:       // 口表
                    if (_bodyTemperParams.TempMouseSymbol.Equals("+") == true)
                    {
                        grfx.DrawLine(Pens.Blue, rect.X, rect.Y + rect.Height / 2, rect.X + rect.Width, rect.Y + rect.Height / 2);
                        grfx.DrawLine(Pens.Blue, rect.X + rect.Width / 2, rect.Y, rect.X + rect.Width / 2, rect.Y + rect.Height);
                    }
                    else
                    {
                        grfx.DrawEllipse(Pens.Blue, rect);
                        grfx.FillEllipse(Brushes.Blue, rect);
                    }
                    
                    break;
                
                case GraphPointType.ANUS:        // 肛表
                    grfx.DrawEllipse(Pens.Blue, rect);
                    break;
                
                case GraphPointType.ARMPIT:      // 腋表
                    //szUnit.Width = (int)(szUnit.Width * 1.2);
                    //szUnit.Height = (int)(szUnit.Height * 1.2);
                    rect = new Rectangle(x - szUnit.Width / 2, y - szUnit.Height / 2, szUnit.Width, szUnit.Height);
                    
                    grfx.DrawLine(Pens.Blue, rect.Left, rect.Top, rect.Left + rect.Width, rect.Top + rect.Height);
                    grfx.DrawLine(Pens.Blue, rect.Left, rect.Top + rect.Height, rect.Left + rect.Width, rect.Top);
                    break;

                case GraphPointType.BREATH:     // 呼吸
                    grfx.DrawEllipse(Pens.Blue, rect);
                    grfx.FillEllipse(Brushes.Blue, rect);
                    break;
                
                case GraphPointType.VERIFY:     // 确认
                    PointF pt0 = new PointF(x, y);
                    
                    pt0.X -= (float)(szUnit.Width * 0.4);
                    pt0.Y += (float)(szUnit.Height * 1.1);
                    
                    PointF pt1 = new PointF(x, y);
                    
                    PointF pt2 = new PointF(x, y);
                    pt2.X += (float)(szUnit.Width * 0.7);
                    pt2.Y += (float)(szUnit.Height * 1.5);
                    
                    grfx.DrawLine(Pens.Red, pt0, pt1);
                    grfx.DrawLine(Pens.Red, pt2, pt1);
                                        
                    break;

                case GraphPointType.Arrow:     // 箭头
                    PointF pt3 = new PointF(x, y);
                    pt3.X += (float)(szUnit.Width * 0.25 - 1);
                    pt3.Y += (float)(szUnit.Height * 4);

                    PointF pt6 = new PointF(x, y);
                    pt6.X += (float)(szUnit.Width * 0.25 - 1);
                    pt6.Y += (float)(szUnit.Height);

                    PointF pt4 = new PointF(x, y);
                    pt4.X += (float)(szUnit.Width - 10);
                    pt4.Y += (float)(szUnit.Height * 2);

                    PointF pt5 = new PointF(x, y);
                    pt5.X += (float)(szUnit.Width * 0.7);
                    pt5.Y += (float)(szUnit.Height * 2);

                    grfx.DrawLine(Pens.Blue, pt3, pt6);
                    grfx.DrawLine(Pens.Blue, pt4, pt6);
                    grfx.DrawLine(Pens.Blue, pt5, pt6);

                    break;
                case GraphPointType.Symbols:    // 圈中再叉符号
                    rect = new Rectangle(x - szUnit.Width, y - szUnit.Height, szUnit.Width * 2, szUnit.Height * 2);
                    grfx.DrawEllipse(Pens.Blue, rect);

                    grfx.DrawLine(Pens.Blue, rect.Left + 2, rect.Top + 2, (rect.Left + rect.Width) - 2, (rect.Top + rect.Height) - 2);
                    grfx.DrawLine(Pens.Blue, rect.Left + 2, (rect.Top + rect.Height) - 2, (rect.Left + rect.Width) - 2, rect.Top + 2);
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
        public void DrawLegend(ref Bitmap bmp, GraphPointType eLegend, int x, int y)
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
        
        
        #region 数据处理
        /// <summary>
        /// 获取测量数据的起始日期与结束日期
        /// </summary>
        private void getDateRng()
        {
            if (_dsNursing == null || _dsNursing.Tables.Count == 0 || _dsNursing.Tables[0].Rows.Count == 0)
            {
                return;
            }
            
            DataRow[] drFind = _dsNursing.Tables[0].Select(string.Empty, "TIME_POINT");
            
            _dtRngStart = (DateTime)drFind[0]["TIME_POINT"];
            _dtRngEnd   = (DateTime)drFind[drFind.Length - 1]["TIME_POINT"];
            
            TimeSpan tspan = _dtRngEnd.Date.Subtract(_dtRngStart.Date);
            
            int count = (int)(tspan.TotalDays / ComConst.VAL.DAYS_PER_WEEK);
            
            _dtStart  = _dtRngStart.Date.AddDays(count * ComConst.VAL.DAYS_PER_WEEK);            
        }
        
        
        /// <summary>
        /// 获取术后日数
        /// </summary>
        /// <param name="dtCurrent"></param>
        /// <returns></returns>
        private string getOperedDays(DateTime dtCurrent)
        {
            switch(_bodyTemperParams.OperateFmt)
            {
                case 0:
                    return getOperedDays0(dtCurrent);
                case 1:
                    return getOperedDays1(dtCurrent);
                default:
                    return "未指定格式";
            }
        }
        
        
        /// <summary>
        /// 获取术后天数
        /// </summary>
        /// <remarks>手术记录按日期倒排序</remarks>
        /// <param name="dtCurrent">要计算的日期</param>
        /// <returns></returns>
        private string getOperedDays0(DateTime dtCurrent)
        {
            // 获取最近手术时间
            string filter = "VITAL_CODE = " + SqlManager.SqlConvert(_bodyTemperParams.OperationVitalCode)
				+ " AND TIME_POINT > " + SqlManager.SqlConvert(dtCurrent.AddDays(-1* _bodyTemperParams.OperatedDaysShow).ToString(ComConst.FMT_DATE.SHORT))
                + " AND TIME_POINT < " + SqlManager.SqlConvert(dtCurrent.AddDays(1).ToString(ComConst.FMT_DATE.SHORT));
            
			DataRow[] drFind    = _dsNursing.Tables[0].Select(filter, "TIME_POINT DESC");
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
                        operedDays = tSpan.Days.ToString() + ComConst.STR.COMMA + operedDays;
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
                        operedDays = tSpan.Days.ToString() + ComConst.STR.COMMA + operedDays;
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
                    operedDays = tSpan.Days.ToString() + ComConst.STR.COMMA + operedDays;
                }
                else
                { 
                    operedDays = tSpan.Days.ToString();
                }
            }
            
            // 格式化输出
            string[] parts = operedDays.Split(ComConst.STR.COMMA.ToCharArray());
            
            operedDays = string.Empty;
            for(int i = 0; i < _bodyTemperParams.OperatedMaxCount && i < parts.Length; i++)
            {
                if (i > 0) operedDays += @"/";
                operedDays += parts[i];
            }
            
            return operedDays;
        }        
                
        
        /// <summary>
        /// 获取术后天数
        /// </summary>
        /// <remarks>手术记录按日期倒排序</remarks>
        /// <param name="dtCurrent">要计算的日期</param>
        /// <returns></returns>
        private string getOperedDays1(DateTime dtCurrent)
        {
            // 获取最近手术时间
            string filter = "VITAL_CODE = " + SqlManager.SqlConvert(_bodyTemperParams.OperationVitalCode)
                + " AND TIME_POINT < " + SqlManager.SqlConvert(dtCurrent.AddDays(1).ToString(ComConst.FMT_DATE.SHORT));
            
			DataRow[] drFind    = _dsNursing.Tables[0].Select(filter, "TIME_POINT DESC");
            string preOperDays  = string.Empty;                         // 距倒数第二次手术的天数
            string operedDays   = string.Empty;                         // 术后天数
            
            // 如果没有手术记录, 返回空
            if (drFind.Length == 0)
            {
                return string.Empty;
            }
            
            DateTime dtOper = DataType.DateTime_Null();
            DateTime dtNext = dtCurrent;
            string   tag    = string.Empty;
            
            for(int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];
                
                dtOper = ((DateTime)dr["TIME_POINT"]).Date;
                TimeSpan tSpan = dtNext.Date.Subtract(dtOper.Date);
                
                // 末次手术超过10天 不显示
                if (tSpan.TotalDays > _bodyTemperParams.OperatedDaysShow)
                {
                    break;
                }
                
                if (i == 0)
                {
                    if (tSpan.TotalDays == 0)
                    {
                        if (drFind.Length == 1)
                        {
                            return "0";
                        }
                        else
                        {
                            tag = "(" + drFind.Length + ")";
                        }
                    }
                    else
                    {
                        operedDays = tSpan.TotalDays.ToString() + "/";
                    }
                }
                else
                {
                    operedDays += dtCurrent.Date.Subtract(dtOper.Date).TotalDays.ToString() + "/";
                }
                
                dtNext = dtOper;
            }
            
            if (operedDays.EndsWith("/"))
            {
                operedDays = operedDays.Substring(0, operedDays.Length - 1);
            }
            
            return operedDays + tag;
        }        
        
        
        /// <summary>
        /// 重新设置大便项目
        /// </summary>
        private void resetStoolData()
        {
            ArrayList arrTime   = new ArrayList();
            ArrayList arrValue  = new ArrayList();
            
            // 获取各个项目的代码
            string preCountItem     = string.Empty;                     // 灌肠前次数代码
            string nextCountItem    = string.Empty;                     // 灌肠后次数代码
            string clysterItem      = string.Empty;                     // 灌肠次数代码
            string stoolItem        = string.Empty;                     // 大便次数代码
            
            string[] parts = _bodyTemperParams.StoolComponent.Split(ComConst.STR.COMMA.ToCharArray());
            if (parts.Length < 4)
            {
                return;
            }
            
            stoolItem       = parts[0];
            preCountItem    = parts[1];
            clysterItem     = parts[2];
            nextCountItem   = parts[3];
            
            // 查找相应的项目
            if (_dsNursing == null || _dsNursing.Tables.Count == 0 || _dsNursing.Tables[0].Rows.Count == 0)
            {
                return;
            }
            
            DataRow[] drFind = _dsNursing.Tables[0].Select(string.Empty, "TIME_POINT");
            
            DateTime dtStool = DataType.DateTime_Null();
            DateTime dtStart = DataType.DateTime_Null();
            DateTime dtEnd   = DataType.DateTime_Null(); 
            DateTime dtCurrent;
            
            string preCount     = string.Empty;                         // 灌肠前次数
            string nextCount    = string.Empty;                         // 灌肠后次数
            string clyster      = string.Empty;                         // 灌肠次数
            string stoolCount   = string.Empty;                         // 大便次数
            
            for(int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];
                
                string vitalCode  = dr["VITAL_CODE"].ToString();                    // 项目代码
                string vitalValue = dr["VITAL_SIGNS_CVALUES"].ToString().Trim();    // 值
                
                if (vitalValue.Length == 0)
                {
                    continue;
                }
                
                dtCurrent = (DateTime)dr["TIME_POINT"];
                
                // 判断时间是否一天
                if (DataType.DateTime_IsNull(ref dtEnd) == false 
                    && dtCurrent.Date.Subtract(dtEnd.Date).TotalDays >= 1
                    && dtStool.Date.Equals(dtEnd.Date) == false)
                {
                    if (clyster.Length > 0 && nextCount.Length > 0)
                    {   
                        if (preCount.Length > 0)
                        {
                            stoolCount = preCount + ComConst.STR.COMMA;                                     
                        }
                        
                        stoolCount += clyster + ComConst.STR.COMMA + nextCount;
                        
                        // 插入该数据
                        arrTime.Add(dtEnd);
                        arrValue.Add(stoolCount);
                        
                        preCount     = string.Empty;                         // 灌肠前次数
                        nextCount    = string.Empty;                         // 灌肠后次数
                        clyster      = string.Empty;                         // 灌肠次数
                        stoolCount   = string.Empty;                         // 大便次数
                        
                        dtStart      = DataType.DateTime_Null();
                        dtEnd        = DataType.DateTime_Null();
                    }
                }
                
                // 获取数据
                if (vitalCode.Equals(stoolItem))             // 大便
                {
                    dtStool     = dtCurrent;
                    stoolCount  = vitalValue;
                }
                else if (vitalCode.Equals(preCountItem))     // 灌肠前次数
                {
                    setRngTime(dtCurrent, ref dtStart, ref dtEnd);
                    preCount    = vitalValue;                        
                }
                else if (vitalCode.Equals(clysterItem))      // 灌肠次数
                {
                    setRngTime(dtCurrent, ref dtStart, ref dtEnd);
                    clyster     = vitalValue;                        
                }
                else if (vitalCode.Equals(nextCountItem))    // 灌肠后次数
                {
                    setRngTime(dtCurrent, ref dtStart, ref dtEnd);
                    nextCount   = vitalValue;
                }
            }
            
            if (clyster.Length > 0 && nextCount.Length > 0)
            {   
                if (preCount.Length > 0)
                {
                    stoolCount = preCount + ComConst.STR.COMMA;                                     
                }
                
                stoolCount += clyster + ComConst.STR.COMMA + nextCount;
                
                // 插入该数据
                arrTime.Add(dtEnd);
                arrValue.Add(stoolCount);
                
                preCount     = string.Empty;                         // 灌肠前次数
                nextCount    = string.Empty;                         // 灌肠后次数
                clyster      = string.Empty;                         // 灌肠次数
                stoolCount   = string.Empty;                         // 大便次数
                
                dtStart      = DataType.DateTime_Null();
                dtEnd        = DataType.DateTime_Null();
            }
            
            // 插入数据
            for(int j = 0; j < arrTime.Count; j++)
            {
                DataRow drFirst = _dsNursing.Tables[0].Rows[0];
                
                DataRow drNew =_dsNursing.Tables[0].NewRow();
                drNew["ATTRIBUTE"]          = "3";                
                drNew["PATIENT_ID"]         = drFirst["PATIENT_ID"];
                drNew["VISIT_ID"]           = drFirst["VISIT_ID"];
                drNew["RECORDING_DATE"]     = (DateTime)arrTime[j];
                drNew["TIME_POINT"]         = (DateTime)arrTime[j];
                drNew["VITAL_SIGNS"]        = "大便";
                drNew["UNITS"]              = string.Empty;
                drNew["CLASS_CODE"]         = "B";
                drNew["VITAL_CODE"]         = stoolItem;
                drNew["VITAL_SIGNS_CVALUES"] = (string)arrValue[j];
                drNew["WARD_CODE"]          = drFirst["WARD_CODE"];
                
                _dsNursing.Tables[0].Rows.Add(drNew);
            }
        }
        
        
        private void setRngTime(DateTime dtCurrent, ref DateTime dtStart, ref DateTime dtEnd)
        {
            if (DataType.DateTime_IsNull(ref dtStart) == true)
            {
                dtStart = dtCurrent;
                dtEnd   = dtCurrent;
            }
            else
            {
                dtEnd   = dtCurrent;
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
            
            // 设置字体
            fontText = _bodyTemperParams.GraphTextFont;
            
            // 用灌肠数据更新大便数据
            resetStoolData();
        }
        
        
        /// <summary>
        /// 释放所占资源
        /// </summary>
        public void Dispose()
        {
        }        
        #endregion
        
        
        #region 作图第二版
        /// <summary>
        /// 画楣栏
        /// </summary>
        public void DrawHeader(ref Graphics grfx)
        {
            // 如果没起始时间点
            if (DataType.DateTime_IsNull(ref _dtRngStart) == true)
            {
                return;
            }
            
            // 获取楣栏的项目
            string topItems = _bodyTemperParams.TopItems;            
            string[] items = topItems.Split(ComConst.STR.COMMA.ToCharArray());
            
            for(int i = 0; i < items.Length; i++)
            {
                string itemName = items[i];
                
                // 获取项目属性
                Point   ptStart     = new Point();                
                Size    szUnitRng   = new Size();
                string  fontName    = string.Empty;
                float   fontSize    = 0;
                int     color       = 0;
                
                if (_bodyTemperParams.GetTopItemProperty(itemName, ref ptStart, ref szUnitRng, 
                                                         ref fontName, ref fontSize, ref color) == false)
                {
                    continue;
                }
                
                switch(itemName)
                {
                    case "日期":
                        drawDateNow(ref grfx, ptStart, szUnitRng, fontName, fontSize, color);
                        break;
                    
                    case "住院日数":
                        drawInpDays(ref grfx, ptStart, szUnitRng, fontName, fontSize, color);
                        break;
                    
                    case "术后日数":
                        drawOperatedDays(ref grfx, ptStart, szUnitRng, fontName, fontSize, color);
                        break;
                        
                    default:
                        break;
                }
            }
        }
        
        
        /// <summary>
        /// 画楣栏的当前日期
        /// </summary>
        private void drawDateNow(ref Graphics grfx, Point ptStart, Size szUnitRng, 
                                 string fontName, float fontSize, int color)
        {
            Font        fontDraw    = new Font(fontName, fontSize);            
            DateTime    dtCurrent   = DataType.DateTime_Null();
            DateTime    dtPre       = DataType.DateTime_Null();
            Brush       brushDraw   = BodyTemperParams.GetBrushFromColor(color);
            
            drawFormat.Alignment = StringAlignment.Center;
            
            // 从作图日期开始到 周末
            for(int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {
                // 获取作图区大小, 并清除
                RectangleF rectRng = new RectangleF(ptStart, szUnitRng);
                
                // 获取当前日期
                dtCurrent = _dtStart.AddDays(i);
                
                if (dtCurrent.Date.CompareTo(_dtNow.Date) > 0 && dtCurrent.Date.CompareTo(_dtRngEnd.Date) > 0)
                {
                    ptStart.X += szUnitRng.Width;
                    continue;
                }
                
                // 获取当前日期的字符串
                string  curDate = string.Empty;
                
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
                        curDate = dtCurrent.ToString("dd");
                    }
                }
                
                dtPre = dtCurrent;
                
                // 作图                
                grfx.DrawString(curDate, fontDraw, brushDraw, rectRng, drawFormat);
                
                ptStart.X += szUnitRng.Width;
            }
        }
        
        
        /// <summary>
        /// 画楣栏的入院日数
        /// </summary>
        private void drawInpDays(ref Graphics grfx, Point ptStart, Size szUnitRng, 
                                 string fontName, float fontSize, int color)
        {
            Font        fontDraw    = new Font(fontName, fontSize);            
            DateTime    dtCurrent   = DataType.DateTime_Null();
            DateTime    dtPre       = DataType.DateTime_Null();
            Brush       brushDraw   = BodyTemperParams.GetBrushFromColor(color);
            
            drawFormat.Alignment = StringAlignment.Center;
            
            // 从作图日期开始到 周末
            for(int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {
                // 获取作图区大小, 并清除
                RectangleF rectRng = new RectangleF(ptStart, szUnitRng);
                
                // 获取术后日数
                dtCurrent = _dtStart.AddDays(i);
                TimeSpan tspan = dtCurrent.Date.Subtract(_dtInp.Date);
                
                if (dtCurrent.Date.CompareTo(_dtNow.Date) > 0 && dtCurrent.Date.CompareTo(_dtRngEnd.Date) > 0)
                {
                    ptStart.X += szUnitRng.Width;
                    continue;
                }
                
                // 作图
                grfx.DrawString((tspan.Days + 1).ToString(), fontDraw, brushDraw, rectRng, drawFormat);
                
                ptStart.X += szUnitRng.Width;
            }
        }        
         
        
        /// <summary>
        /// 画楣栏的术后日数
        /// </summary>
        private void drawOperatedDays(ref Graphics grfx, Point ptStart, Size szUnitRng, 
                                 string fontName, float fontSize, int color)
        {
            Font        fontDraw    = new Font(fontName, fontSize);            
            DateTime    dtCurrent   = DataType.DateTime_Null();
            DateTime    dtPre       = DataType.DateTime_Null();
            Brush       brushDraw   = BodyTemperParams.GetBrushFromColor(color);
            
            drawFormat.Alignment = StringAlignment.Center;
            
            // 从作图日期开始到 周末
            for(int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {
                // 获取作图区大小, 并清除
                RectangleF rectRng = new RectangleF(ptStart, szUnitRng);
                
                // 获取术后日数
                dtCurrent = _dtStart.AddDays(i);
                TimeSpan tspan = dtCurrent.Subtract(_dtInp);
                
                if (dtCurrent.Date.CompareTo(_dtNow.Date) > 0 && dtCurrent.Date.CompareTo(_dtRngEnd.Date) > 0)
                {
                    ptStart.X += szUnitRng.Width;
                    continue;
                }
                
                string operedDays = getOperedDays(dtCurrent);
                
                // 作图
                grfx.DrawString(operedDays, fontDraw, brushDraw, rectRng, drawFormat);
                
                ptStart.X += szUnitRng.Width;
            }
        } 
        
        
        /// <summary>
        /// 画尾栏
        /// </summary>
        public void DrawTailParts(ref Graphics grfx)
        {
            // 如果没起始时间点
            if (DataType.DateTime_IsNull(ref _dtRngStart) == true)
            {
                return;
            }
            
            arrBreakCols.Clear();
            
            // 获取尾栏的项目
            string topItems = _bodyTemperParams.TailItems;
            string[] items = topItems.Split(ComConst.STR.COMMA.ToCharArray());
            
            for(int i = 0; i < items.Length; i++)
            {
                string itemName = items[i];
                
                // 获取项目属性
                int     type        = 0;
                string  condition   = string.Empty;
                Point   ptStart     = new Point();                
                Size    szUnitRng   = new Size();
                string  fontName    = string.Empty;
                float   fontSize    = 0;
                int     color       = 0;
                int     dayStart    = 0;
                
                if (_bodyTemperParams.GetTailItemProperty(itemName, ref type, ref condition, ref ptStart, ref szUnitRng, 
                                                         ref fontName, ref fontSize, ref color, ref dayStart) == false)
                {
                    continue;
                }
                
                BodyTemperParams.TailItemType itemType = BodyTemperParams.GetTailItemType(type);
                
                switch(itemType)
                {
                    case BodyTemperParams.TailItemType.ONE:             // 类型: 单项
                        drawTailOneTimePerday(ref grfx, ptStart, szUnitRng, fontName, fontSize, color, condition, dayStart);
                        break;
                    
                    case BodyTemperParams.TailItemType.SUM:             // 合计
                        drawTailSumPerday(ref grfx, ptStart, szUnitRng, fontName, fontSize, color, condition, dayStart);
                        break;
                    
                    case BodyTemperParams.TailItemType.TWO:             // 两项
                        drawTailTwoTimePerday_Border(ref grfx, ptStart, szUnitRng, fontName, fontSize, color, condition);
                        break;
                    
                    case BodyTemperParams.TailItemType.BREATH:          // 呼吸
                        drawTailSixTimePerday(ref grfx, ptStart, szUnitRng, fontName, fontSize, color, condition, dayStart);
                        break;
                    
                    case BodyTemperParams.TailItemType.CLYSTER:         // 灌肠
                        drawTailStoolPerday(ref grfx, ptStart, szUnitRng, fontName, fontSize, color, condition, dayStart);
                        break;
                    
                    default:
                        break;
                }                
            }
	    }
        
        
        /// <summary>
        /// 画尾栏的一日一次项目
        /// </summary>
        private void drawTailOneTimePerday(ref Graphics grfx, Point ptStart, Size szUnitRng, 
                                 string fontName, float fontSize, int color, string condition, int dayStart)
        {
            Font        fontDraw    = new Font(fontName, fontSize);            
            DateTime    dtCurrent   = DataType.DateTime_Null();
            DateTime    dtPre       = DataType.DateTime_Null();
            Brush       brushDraw   = BodyTemperParams.GetBrushFromColor(color);
            
            drawFormat.Alignment = StringAlignment.Center;
            
            // 从作图日期开始到 周末
            for(int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {
                // 获取作图区大小, 并清除
                RectangleF rectRng = new RectangleF(ptStart, szUnitRng);
                
                // 条件检查
                dtCurrent = _dtStart.AddDays(i);
                if (dtCurrent.Date.CompareTo(_dtNow.Date) > 0)
                {
                    ptStart.X += szUnitRng.Width;
                    continue;
                }
                
                // 获取值
                string val = string.Empty;
                dtCurrent = dtCurrent.Date.AddHours(dayStart);
                
                string filter = "TIME_POINT >= " + SqlManager.SqlConvert(dtCurrent.ToString(ComConst.FMT_DATE.LONG));
                filter += " AND TIME_POINT < " + SqlManager.SqlConvert(dtCurrent.AddDays(1).ToString(ComConst.FMT_DATE.LONG));
                filter += " AND ( " + condition + ")";
                
                DataRow[] drFind = _dsNursing.Tables[0].Select(filter);
                
                for(int c = 0; c < drFind.Length; c++)
                {
                    if (c > 0) val += ComConst.STR.CRLF;
                    val += drFind[c]["VITAL_SIGNS_CVALUES"].ToString();
                }
                
                if (drFind.Length > 0)
                {
                    // val = drFind[0]["VITAL_SIGNS_CVALUES"].ToString();
                    
                    // 作图
                    drawStringColored(ref grfx, rectRng, val, fontName, fontSize, color);
                    // grfx.DrawString(val, fontDraw, brushDraw, rectRng, drawFormat);
                }
                
                ptStart.X += szUnitRng.Width;
            }
        }
        
        
        /// <summary>
        /// 画尾栏的一日合计项目
        /// </summary>
        private void drawTailSumPerday(ref Graphics g, Point ptStart, Size szUnitRng, 
                                 string fontName, float fontSize, int color, string condition, int dayStart)
        {
            Font        fontDraw    = new Font(fontName, fontSize);            
            DateTime    dtCurrent   = DataType.DateTime_Null();
            DateTime    dtPre       = DataType.DateTime_Null();
            Brush       brushDraw   = BodyTemperParams.GetBrushFromColor(color);
            
            drawFormat.Alignment = StringAlignment.Center;
            
            // 从作图日期开始到 周末
            for(int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {
                // 获取作图区大小, 并清除
                RectangleF rectRng = new RectangleF(ptStart, szUnitRng);
                
                // 条件检查
                dtCurrent = _dtStart.AddDays(i);
                if (dtCurrent.Date.CompareTo(_dtNow.Date) > 0)
                {
                    ptStart.X += szUnitRng.Width;
                    continue;
                }
                
                // 获取值
                float   val     = 0F;
                bool    blnHave = false;   // 是否有值
                
                dtCurrent = dtCurrent.Date.AddHours(dayStart);
                
                string filter = "TIME_POINT >= " + SqlManager.SqlConvert(dtCurrent.ToString(ComConst.FMT_DATE.LONG));
                filter += " AND TIME_POINT < " + SqlManager.SqlConvert(dtCurrent.AddDays(1).ToString(ComConst.FMT_DATE.LONG));
                filter += " AND ( " + condition + ")";
                
                DataRow[] drFind = _dsNursing.Tables[0].Select(filter);
                
                for(int k = 0; k < drFind.Length; k++)
                {
                    string content = drFind[k]["VITAL_SIGNS_CVALUES"].ToString();
                    val += GetSumValueFromString(content);
                    //float valOneRec = 0F;
                    
                    //if (float.TryParse(content, out valOneRec) == true)
                    //{
                    //    blnHave = true;
                    //    val += valOneRec;
                    //}
                }
                
                if (val != 0)
                {
                    // 作图
                    g.DrawString(val.ToString(), fontDraw, brushDraw, rectRng, drawFormat);
                }
                else if (_bodyTemperParams.ShowSumZero && blnHave)
                {
                    g.DrawString(val.ToString(), fontDraw, brushDraw, rectRng, drawFormat);
                }
                
                ptStart.X += szUnitRng.Width;
            }
        }
        
        
        /// <summary>
        /// 画尾栏的一日两项的项目
        /// </summary>
        private void drawTailTwoTimePerday(ref Graphics grfx, Point ptStart, Size szUnitRng, 
                                 string fontName, float fontSize, int color, string condition)
        {
            Font        fontDraw    = new Font(fontName, fontSize);            
            DateTime    dtCurrent   = DataType.DateTime_Null();
            DateTime    dtPre       = DataType.DateTime_Null();
            Brush       brushDraw   = BodyTemperParams.GetBrushFromColor(color);
            
            drawFormat.Alignment = StringAlignment.Center;
            
            int         hours       = (int)(ComConst.VAL.HOURS_PER_DAY / 2);
            
            // 从作图日期开始到 周末
            for(int i = 0; i < ComConst.VAL.DAYS_PER_WEEK * 2; i++)
            {
                // 获取作图区大小, 并清除
                RectangleF rectRng = new RectangleF(ptStart, szUnitRng);
                
                // 条件检查
                dtCurrent = _dtStart.Date.AddHours(hours * i);
                if (dtCurrent.Date.CompareTo(_dtNow.Date) > 0)
                {
                    ptStart.X += szUnitRng.Width;
                    break;
                }
                
                // 如果一天有三次, 不输出
                //string filter = "TIME_POINT >= " + SqlManager.SqlConvert(dtCurrent.ToString(ComConst.FMT_DATE.SHORT));
                //filter += " AND TIME_POINT < " + SqlManager.SqlConvert(dtCurrent.AddDays(1).ToString(ComConst.FMT_DATE.SHORT));
                //filter += " AND ( " + condition + ")";
                
                //DataRow[] drFind = _dsNursing.Tables[0].Select(filter);
                //if (drFind.Length > 2) continue;
                
                // 获取值
                // dtCurrent = dtCurrent.AddHours(_dayStartTime);                
                string filter = "TIME_POINT >= " + SqlManager.SqlConvert(dtCurrent.ToString(ComConst.FMT_DATE.LONG));
                filter += " AND TIME_POINT < " + SqlManager.SqlConvert(dtCurrent.AddHours(hours).ToString(ComConst.FMT_DATE.LONG));
                filter += " AND ( " + condition + ")";
                
                DataRow[] drFind = _dsNursing.Tables[0].Select(filter, "TIME_POINT");
                if (drFind.Length >= 1)
                {
                    string val = drFind[0]["VITAL_SIGNS_CVALUES"].ToString();
                    
                    // 作图
                    grfx.DrawString(val, fontDraw, brushDraw, rectRng, drawFormat);
                }
                
                ptStart.X += szUnitRng.Width;
            }
        }


        /// <summary>
        /// 画尾栏的一日两项的项目 (如果有多项, 最开始与最后两项)
        /// </summary>
        private void drawTailTwoTimePerday_Border(ref Graphics grfx, Point ptStart, Size szUnitRng,
                                 string fontName, float fontSize, int color, string condition)
        {
            Font        fontDraw    = new Font(fontName, fontSize);
            DateTime    dtCurrent   = DataType.DateTime_Null();
            DateTime    dtPre       = DataType.DateTime_Null();
            Brush       brushDraw   = BodyTemperParams.GetBrushFromColor(color);

            drawFormat.Alignment = StringAlignment.Center;

            int         hours       = (int)(ComConst.VAL.HOURS_PER_DAY);
            RectangleF rectRng      = new RectangleF(ptStart, szUnitRng);

            // 从作图日期开始到 周末
            for (int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {
                // 条件检查
                dtCurrent = _dtStart.Date.AddHours(hours * i);
                if (dtCurrent.Date.CompareTo(_dtNow.Date) > 0)
                {
                    ptStart.X += szUnitRng.Width;
                    break;
                }

                // 获取值
                string filter = "TIME_POINT >= " + SqlManager.SqlConvert(dtCurrent.ToString(ComConst.FMT_DATE.LONG));
                filter += " AND TIME_POINT < " + SqlManager.SqlConvert(dtCurrent.AddHours(hours).ToString(ComConst.FMT_DATE.LONG));
                filter += " AND ( " + condition + ")";

                DataRow[] drFind = _dsNursing.Tables[0].Select(filter, "TIME_POINT");
                if (drFind.Length == 1)
                {
                    //CJ20110812 modify begin
                    DateTime timePoint = Convert.ToDateTime(drFind[0]["TIME_POINT"]);
                    DateTime tmpTimePoint = timePoint.Date.AddHours(12d);
                    TimeSpan ts = timePoint - tmpTimePoint;
                    if (ts.TotalHours <= 0)
                    {
                        rectRng = new RectangleF(ptStart.X, ptStart.Y, szUnitRng.Width / 2, szUnitRng.Height);  
                    }
                    else
                    {
                        rectRng = new RectangleF(ptStart.X + szUnitRng.Width / 2, ptStart.Y, szUnitRng.Width / 2, szUnitRng.Height);
                    }
                    //rectRng      = new RectangleF(ptStart, szUnitRng);
                    //CJ20110812 modify end 血压分上午和下午，上午显示在左边，下午显示在右边
                    string val = drFind[0]["VITAL_SIGNS_CVALUES"].ToString();
                    grfx.DrawString(val, fontDraw, brushDraw, rectRng, drawFormat);
                }
                else if (drFind.Length > 1)
                {
                    for (int c = 0; c < 2; c++)
                    {
                        int rec_index = 0;
                        if (c > 0) rec_index = drFind.Length - 1;

                        rectRng      = new RectangleF(ptStart.X + c * szUnitRng.Width / 2, ptStart.Y, szUnitRng.Width / 2, szUnitRng.Height);
                        string val = drFind[rec_index]["VITAL_SIGNS_CVALUES"].ToString();
                        grfx.DrawString(val, fontDraw, brushDraw, rectRng, drawFormat);
                    }
                }

                ptStart.X += szUnitRng.Width;
            }
        }
        
        /// <summary>
        /// 画尾栏的呼吸项目
        /// </summary>
        private void drawTailSixTimePerday(ref Graphics grfx, Point ptStart, Size szUnitRng,
                                 string fontName, float fontSize, int color, string condition, int dayStart)
        {
            Font fontDraw = new Font(fontName, fontSize);
            DateTime dtCurrent = DataType.DateTime_Null();
            DateTime dtPre = DataType.DateTime_Null();
            Brush brushDraw = BodyTemperParams.GetBrushFromColor(color);

            drawFormat.Alignment = StringAlignment.Center;

            int hours = (int)(ComConst.VAL.HOURS_PER_DAY / 6);
            bool blnUp = (dayStart != 0);                                          // 上下错格

            // 从作图日期开始到 周末
            for (int i = 0; i < ComConst.VAL.DAYS_PER_WEEK * 6; i++)
            {
                // 获取作图区大小, 并清除
                RectangleF rectRng = new RectangleF(ptStart.X, ptStart.Y, szUnitRng.Width + 2, szUnitRng.Height);

                // 条件检查
                dtCurrent = _dtStart.Date.AddHours((hours) * i + _bodyTemperParams.StartTime);
                if (dtCurrent.Date.CompareTo(_dtNow.Date) > 0)
                {
                    ptStart.X += szUnitRng.Width;
                    continue;
                }

                // 获取值
                string filter = "TIME_POINT >= " + SqlManager.SqlConvert(dtCurrent.ToString(ComConst.FMT_DATE.LONG));
                filter += " AND TIME_POINT < " + SqlManager.SqlConvert(dtCurrent.AddHours(hours).ToString(ComConst.FMT_DATE.LONG));
                filter += " AND ( " + condition + ")";

                DataRow[] drFind = _dsNursing.Tables[0].Select(filter);
                if (drFind.Length >= 1)
                {
                    string val = drFind[0]["VITAL_SIGNS_CVALUES"].ToString();

                    DateTime dtMeasure = (DateTime)drFind[0]["TIME_POINT"];
                    float valFloat = 0;
                    string valH = dtMeasure.Hour.ToString();                        //取录入的时间点 例如 2010-8-10 12：50 取12
                    float.TryParse(valH, out valFloat);
                    // 记录断点
                    int temp = 0;
                    if (val.Length > 0 && int.TryParse(val, out temp) == false)
                    {
                        arrBreakCols.Add(i);
                    }

                    // 作图
                    if (valFloat > 11 && valFloat < 24)
                    {

                        rectRng.Y += szUnitRng.Height / 3;
                    }

                    blnUp = !blnUp;

                    grfx.DrawString(val, fontDraw, brushDraw, rectRng, drawFormat);
                }

                ptStart.X += szUnitRng.Width;
            }
        }       
        
        
        /// <summary>
        /// 画尾栏的一日一次项目
        /// </summary>
        private void drawTailStoolPerday(ref Graphics grfx, Point ptStart, Size szUnitRng, 
                                 string fontName, float fontSize, int color, string condition, int dayStart)
        {
            Font        fontDraw    = new Font(fontName, fontSize);
            Font        fontDrawS   = new Font(fontName, fontSize - 1);
            
            DateTime    dtCurrent   = DataType.DateTime_Null();
            DateTime    dtPre       = DataType.DateTime_Null();
            Brush       brushDraw   = BodyTemperParams.GetBrushFromColor(color);
            
            drawFormat.Alignment = StringAlignment.Center;
            
            SizeF szPart = grfx.MeasureString("2E", fontDrawS);
            
            // 从作图日期开始到 周末
            for(int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {
                // 获取作图区大小
                RectangleF rectRng = new RectangleF(ptStart, szUnitRng);
                
                // 条件检查
                dtCurrent = _dtStart.AddDays(i);
                if (dtCurrent.Date.CompareTo(_dtNow.Date) > 0)
                {
                    ptStart.X += szUnitRng.Width;
                    continue;
                }
                
                // 获取值
                string val = string.Empty;
                dtCurrent = dtCurrent.Date.AddHours(dayStart);
                
                string filter = "TIME_POINT >= " + SqlManager.SqlConvert(dtCurrent.ToString(ComConst.FMT_DATE.LONG));
                filter += " AND TIME_POINT < " + SqlManager.SqlConvert(dtCurrent.AddDays(1).ToString(ComConst.FMT_DATE.LONG));
                filter += " AND ( " + condition + ")";
                
                DataRow[] drFind = _dsNursing.Tables[0].Select(filter);
                if (drFind.Length > 0)
                {
                    val = drFind[0]["VITAL_SIGNS_CVALUES"].ToString();
                }
                else
                {
                    ptStart.X += szUnitRng.Width;
                    continue;
                }
                
                // 分析数据, 并作图
                if (val.IndexOf(ComConst.STR.COMMA) > 0)
                {
                    string[] parts = val.Split(ComConst.STR.COMMA.ToCharArray());
                    
                    string preCount     = string.Empty;         // 灌肠前大便次数
                    string stoolCount   = string.Empty;         // 灌肠次数
                    string nextCount    = string.Empty;         // 灌肠后大便次数
                    
                    if (parts.Length == 3)
                    {
                        preCount = parts[0].Trim();
                        stoolCount = parts[1].Trim();
                        nextCount = parts[2].Trim();
                    }
                    else if (parts.Length == 2)
                    {
                        stoolCount = parts[0].Trim();
                        nextCount = parts[1].Trim();   
                    }
                    
                    if (stoolCount.Equals("1") == true)
                    {
                        stoolCount = string.Empty;
                    }
                    
                    stoolCount += "E";
                    
                    if (preCount.Length > 0)
                    {
                        RectangleF rectRngPre = new RectangleF(ptStart.X, ptStart.Y, szUnitRng.Width / 2, szUnitRng.Height);
                        
                        // 灌肠前大便次数
                        drawFormat.Alignment = StringAlignment.Far;
                        rectRngPre.Y += 2;
                        grfx.DrawString(preCount, fontDraw, brushDraw, rectRngPre, drawFormat);
                        
                        // 灌肠后大便次数
                        RectangleF rectNext = new RectangleF();
                        rectNext.X  = rectRngPre.X + rectRngPre.Width;
                        rectNext.Y  = rectRngPre.Y - 2;
                        rectNext.Width = szPart.Width;
                        rectNext.Height = (int)(rectRngPre.Height / 2);
                        
                        drawFormat.Alignment = StringAlignment.Center;
                        rectNext.Y -= 2;
                        grfx.DrawString(nextCount, fontDrawS, brushDraw, rectNext, drawFormat);
                        
                        // 横线
                        grfx.DrawLine(Pens.Black, new Point((int)(rectNext.X), (int)(rectNext.Y + rectNext.Height)), 
                                      new Point((int)(rectNext.X + szPart.Width), (int)(rectNext.Y + rectNext.Height)));
                        
                        // 灌肠次数
                        rectNext.Y += rectNext.Height - 1;
                        grfx.DrawString(stoolCount, fontDrawS, brushDraw, rectNext, drawFormat);
                    }
                    else
                    {
                        drawFormat.Alignment = StringAlignment.Center;
                        
                        // 灌肠后大便次数
                        RectangleF rectNext = new RectangleF();
                        rectNext.X = (int)(ptStart.X + (szUnitRng.Width - szPart.Width) / 2);
                        rectNext.Y = ptStart.Y - 2;
                        rectNext.Width = szPart.Width;
                        rectNext.Height = (int)(szUnitRng.Height / 2);
                        
                        drawFormat.Alignment = StringAlignment.Center;
                        grfx.DrawString(nextCount, fontDrawS, brushDraw, rectNext, drawFormat);
                        
                        // 横线
                        grfx.DrawLine(Pens.Black, new Point((int)(rectNext.X), (int)(rectNext.Y + rectNext.Height)), 
                                      new Point((int)(rectNext.X + rectNext.Width), (int)(rectNext.Y + rectNext.Height)));
                        
                        // 灌肠次数
                        rectNext.Y += rectNext.Height - 1;
                        grfx.DrawString(stoolCount, fontDrawS, brushDraw, rectNext, drawFormat);
                    }
                }
                else
                {
                    drawFormat.Alignment = StringAlignment.Center;
                    rectRng.Y += 2;
                    grfx.DrawString(val, fontDraw, brushDraw, rectRng, drawFormat);
                }
                
                ptStart.X += szUnitRng.Width;
            }
        }        
        
        
        /// <summary>
        /// 对于阳性红色显示
        /// </summary>
        /// <param name="grfx"></param>
        /// <param name="rect"></param>
        /// <param name="text"></param>
        public void drawStringColored(ref Graphics grfx, RectangleF rect, string text, 
                                      string fontName, float fontSize, int color)
        {
            Font   fontDraw     = new Font(fontName, fontSize);
            Brush  brushDraw    = BodyTemperParams.GetBrushFromColor(color);
            
            // 测量大小
            if (grfx.MeasureString(text, fontDraw).Width > rect.Width)
            {
                fontDraw.Dispose();
                fontDraw = new Font(fontName, fontSize - 1);
                
                rect.Y -= 6;
                drawFormat.Alignment = StringAlignment.Near;
            }
            
            string COLORED_TEXT = string.Empty;
            
            text = text.Trim();
            text = text.Replace("（", "(");
            text = text.Replace("）", ")");
            
            int pos = text.IndexOf("(阳性)");
            if (pos >= 0)
            {
                COLORED_TEXT = "(阳性)";
            }
            else if ((pos = text.IndexOf("(+)")) >= 0)
            {
                COLORED_TEXT = "(+)";
            }
            
            if (pos >= 0)
            {
                // 准备
                SizeF sz = grfx.MeasureString(text, fontDraw);
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
                sz = grfx.MeasureString(pre, fontDraw);
                grfx.DrawString(pre, fontDraw, brushDraw, rect, sf);
                
                // 本体
                rect.X += sz.Width - 4;
                grfx.DrawString(COLORED_TEXT, fontDraw, Brushes.Red, rect, sf);
            }
            else
            {
                grfx.DrawString(text, fontDraw, brushDraw, rect, drawFormat);
            }
        }        
        
        
        /// <summary>
        /// 画测量点(脉搏,心率,体温,降温)
        /// </summary>
        public void DrawCenterParts(ref Graphics grfx, Size szRng)
        {
            // 如果没起始时间点
            if (DataType.DateTime_IsNull(ref _dtRngStart) == true)
            {
                return;
            }
            
            // 获取主图的项目(liubo20110107 此处应为或得画点的项目，从数据库中读取的)
            string topItems = _bodyTemperParams.GraphPointItems;
            string[] items = topItems.Split(ComConst.STR.COMMA.ToCharArray());
            
            // 脉搏短绌
            if (_bodyTemperParams.PulseShort.Length > 0)
            {
                drawPulseShort(ref grfx, szRng);
            }
            
            // 重新设置坐标
            grfx.ResetTransform();
            grfx.TranslateTransform(0, szRng.Height);
            grfx.ScaleTransform(1, -1);
            
            float   valStart  = 0F;
            float   valStep   = 1F;
            string condition  = string.Empty;

            float   valStartT = 0F;                 // 体温的开始值
            float   valStepT  = 1F;                 // 体温的步长

            for(int i = 0; i < items.Length; i++)
            {
                string itemName = items[i];
                
                // 获取项目属性
                int     type        = 0;
                int     color       = 0;
                
                if (_bodyTemperParams.GetGraphItemProperty(itemName, ref type, 
                                                           ref valStart, ref valStep, ref condition, ref color) == false)
                {
                    continue;
                }
                
                drawGraphPoint(ref grfx, type, valStart, valStep, condition, color);      
                
                // 对于体温大于单独记录其设置信息
                if (itemName.Trim().EndsWith("体温") == true)
                {
                    valStartT = valStart;
                    valStepT = valStep;
                }
            }

            // 首次体温大于39, 加复试符号
            drawHighTemperData(ref grfx, valStartT, valStepT);

            // 降温
            string itemCode = string.Empty;                        
            if (_bodyTemperParams.GetDownTemperProperty(ref condition, ref itemCode, ref valStart, ref valStep) == false)
            {
                return;
            }
            
            getDownTemperData(condition, itemCode);
            drawDownTemper(ref grfx, valStart, valStep);
            
            // 护理事件
            drawOtherNursingEventText(ref grfx);   
        }
        
        
        /// <summary>
        /// 画正图点及连线
        /// </summary>
        private void drawGraphPoint(ref Graphics grfx, int type, float valStart, float valStep, string condition, int color)
        {
            BodyTemperParams.GraphPointType itemType = BodyTemperParams.GetGraphPointType(type);
            Brush       brushDraw   = BodyTemperParams.GetBrushFromColor(color);
            Pen         penDraw     = BodyTemperParams.GetPenFromColor(color);
            SizeF       szUnit      = _bodyTemperParams.SzUnit;
            
            int         temperPos   = _bodyTemperParams.TemperEventPos;
            
            bool        verifyItem  = isVerifyItem(condition);          // 是否是需要确认的项目
            
            // 获取数据
            DateTime dtStart = _dtStart.AddHours(_bodyTemperParams.StartTime);
            DateTime dtEnd   = _dtStart.AddDays(ComConst.VAL.DAYS_PER_WEEK).AddHours(_bodyTemperParams.StartTime);
            
            string filter = "TIME_POINT >= " + SqlManager.SqlConvert(dtStart.ToString(ComConst.FMT_DATE.LONG));
            filter += " AND TIME_POINT < " + SqlManager.SqlConvert(dtEnd.ToString(ComConst.FMT_DATE.LONG));
            filter += " AND ( " + condition + ")";
            
            DataRow[] drFind = _dsNursing.Tables[0].Select(filter, "TIME_POINT ASC");
            
            // 画图
            PointF pt2          = new PointF(-1, -1);
            PointF pt1          = new PointF(-1, -1);
            PointF pt           = new PointF(-1, -1);
            
            int    col2         = -1;
            int    col1         = -1;
            int    col          = -1;
            
            string valText2     = string.Empty;                         // 值文本(不能解释成数字型的)
            string valText1     = string.Empty;                         // 值文本(不能解释成数字型的)
            string valText      = string.Empty;                         // 值文本(不能解释成数字型的)
            
            float  val0         = 0F;                                   // 值
            float  val1         = 0F;                                   // 值
            float  val2         = 0F;                                   // 值
            
            GraphPointStatus  ptStatus = GraphPointStatus.NORMAL;
            //LB添加，清除断点，保证比如体温断点，不连线，不会导致后来画脉搏也不连
            //arrBreakCols.Clear();
            
            for(int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];
                
                // 获取时间坐标位置
                DateTime dtMeasure = (DateTime)dr["TIME_POINT"];
                string   val       = dr["VITAL_SIGNS_CVALUES"].ToString();
                float    valFloat  = 0F;
                
                ptStatus = getGraphPoint(dtMeasure, val, szUnit, valStart, valStep, itemType, ref col, ref pt, ref valFloat);
                
                // 获取值
                valText = string.Empty;
                val0    = 0F;
                if (pt.Y == -1)
                {
                    valText = val;
                }
                else
                {
                    val0 = valFloat;
                }

                // 如果是同一栏
                if (col == col1)
                {
                    // 如果同一栏的前一是数字, 重设置该栏的值
                    if (valText1.Length == 0)
                    {
                        pt1      = pt;
                        valText1 = valText;
                        
                        val1     = val0;
                    }
                }
                // 如果跨栏, 画前一个数据点
                else 
                {
                    // 异常情况作图
                    //LB理解异常点就是数值，过高或者过低的点，并不是 “不升、不清等”
                    if (drawAbnormalPoint(ref grfx, ptStatus, pt, szUnit, valFloat) == 0)
                    {
                        //arrBreakCols.Add(col1);                      // 记录断点(原来的)
                        arrBreakCols.Add(col);                      // 记录断点
                        continue;
                    }
                    
                    // 如果前一栏有值
                    if (col1 > -1)
                    {
                        // 如果前一栏是数字
                        if (valText1.Length == 0)
                        {
                            // 画点
                            DrawLegend(ref grfx, itemType, (int)(pt1.X), (int)(pt1.Y));
                            
                            // 如果更前一点是数字, 连线
                            if (col2 > -1 && valText2.Length == 0)
                            {
                                if (isBreakPoint(col2, col1) == false)
                                {
                                    grfx.DrawLine(penDraw, pt2, pt1);
                                }
                            }
                            
                            // 是否确认
                            if (verifyItem)
                            {
                                if (chkVerifyPoint(val1, val0) == 1)
                                {
                                    DrawLegend(ref grfx, GraphPointType.VERIFY, (int)(pt.X), (int)(pt.Y + szUnit.Height/3));
                                }
                                else if (chkVerifyPoint(val1, val0) == -1)
                                {
                                    DrawLegend(ref grfx, GraphPointType.VERIFY, (int)(pt.X), (int)(pt.Y - szUnit.Height));                                    
                                }
                            }
                        }
                        // 如果前一栏不是数字, 原文输出字符串
                        else
                        {
                            RectangleF rect = new RectangleF(pt1.X - szUnit.Width / 2 , szUnit.Height * temperPos - 1, 
                                                             szUnit.Width, szUnit.Height * temperPos);
                            
                            grfx.ScaleTransform(1, -1);
                            rect.Y *= -1;
                            
                            arrBreakCols.Add(col1);                      // 记录断点
                            
                            drawStringNurseEvent(ref grfx, rect, valText1);
                            
                            grfx.ScaleTransform(1, -1);
                        }
                    }
                    
                    col2        = col1;
                    pt2         = pt1;
                    valText2    = valText1;
                    val2        = val1;
                    
                    col1        = col;
                    pt1         = pt;
                    valText1    = valText;
                    val1        = val0;
                }
            }
            
            // 输出最后一个点
            if (col1 > -1)
            {
                // 如果前一栏是数字
                if (valText1.Length == 0)
                {
                    // 画点
                    DrawLegend(ref grfx, itemType, (int)(pt1.X), (int)(pt1.Y));
                    
                    // 如果更前一点是数字, 连线
                    if (col2 > -1 && valText2.Length == 0)
                    {
                        if (isBreakPoint(col2, col1) == false)
                        {
                            grfx.DrawLine(penDraw, pt2, pt1);
                        }
                    }
                    
                    // 是否确认
                    if (verifyItem)
                    {
                        if (chkVerifyPoint(val1, val0) == 1)
                        {
                            DrawLegend(ref grfx, GraphPointType.VERIFY, (int)(pt.X), (int)(pt.Y + szUnit.Height/3));
                        }
                        else if (chkVerifyPoint(val1, val0) == -1)
                        {
                            DrawLegend(ref grfx, GraphPointType.VERIFY, (int)(pt.X), (int)(pt.Y - szUnit.Height));                                    
                        }
                    }
                }
                // 如果前一栏不是数字, 原文输出字符串
                else
                {
                    RectangleF rect = new RectangleF(pt1.X - szUnit.Width / 2, szUnit.Height * temperPos - 1, 
                                                     szUnit.Width, szUnit.Height * temperPos);
                    
                    grfx.ScaleTransform(1, -1);
                    rect.Y *= -1;
                    
                    arrBreakCols.Add(col1);                      // 记录断点
                    
                    drawStringNurseEvent(ref grfx, rect, valText1);
                    
                    grfx.ScaleTransform(1, -1);
                }
            }
        }
        
        
        /// <summary>
        /// 获取画点位置
        /// </summary>
        /// <returns></returns>
        private GraphPointStatus getGraphPoint(DateTime dtMeasure, string valStr, SizeF szUnit, float valStart, float valStep, GraphPointType itemType, ref int col, ref PointF pt, ref float valFloat)
        {
            // 获取时间坐标位置
            DateTime dtStart = _dtStart.AddHours(_bodyTemperParams.StartTime);
            TimeSpan tspan = dtMeasure.Subtract(dtStart);
            
            col = (int)(tspan.TotalHours / STEP_HOURS);
            pt.X = (int)((col + 0.5) * szUnit.Width);
            //pt.X = ((float)tspan.TotalHours / STEP_HOURS) * szUnit.Width;
            
            // 获取值坐标位置
            string val = valStr;
            valFloat   = 0F;
            
            pt.Y = -1;
            if (float.TryParse(val, out valFloat) == true)
            {
                pt.Y = (int)(((valFloat - valStart) / valStep) * szUnit.Height);
            }
            
            float.TryParse(val, out valFloat);
            
            // 体温
            if (itemType == GraphPointType.ANUS || itemType == GraphPointType.ARMPIT || itemType == GraphPointType.MOUSE)
            {
                // 大于40度
                if (valFloat >= 40)
                {
                    pt.Y = (int)(((40 - valStart) / valStep) * szUnit.Height);
                    return GraphPointStatus.TEMPERATURE_H;
                }

                // 小于35
                if (valFloat <= 35)
                {
                    pt.Y = (int)(((35 - valStart) / valStep) * szUnit.Height);
                    return GraphPointStatus.TEMPERATURE_L;
                }
            }

            // 脉搏
            if (itemType == GraphPointType.PULSE)
            {
                // 大于140
                if (valFloat >= 140)
                {
                    pt.Y = (int)(((140 - valStart) / valStep) * szUnit.Height);
                    return GraphPointStatus.PULSE_H;
                }

                // 脉搏不清情况
                if (valStr.Equals("不清"))
                {
                    pt.Y = (int)(((35 - valStart) / valStep) * szUnit.Height);
                    //LB添加上去的
                    pt.Y = -1;
                    return GraphPointStatus.PULSE_NO;
                }
            }
            
            return GraphPointStatus.NORMAL;
        }
        
        
        /// <summary>
        /// 画异常点
        /// </summary>
        private int drawAbnormalPoint(ref Graphics grfx, GraphPointStatus ptStatus, PointF pt, SizeF szUnit, float valFloat)
        {
            int temperPos = _bodyTemperParams.TemperEventPos;
            
            // 体温事件 体温大于40
            if (ptStatus == GraphPointStatus.TEMPERATURE_H)
            {
                RectangleF rect = new RectangleF(pt.X - szUnit.Width / 2, pt.Y - 1,
                                             szUnit.Width, szUnit.Height * temperPos);

                grfx.ScaleTransform(1, -1);
                rect.Y *= -1;
                fontText = new Font("宋体", 15, FontStyle.Bold);
                drawStringNurseEvent(ref grfx, rect, valFloat.ToString());
                fontText = new Font("宋体", 11);
                grfx.ScaleTransform(1, -1);
            }

            // 脉搏事件 脉搏大于140
            if (ptStatus == GraphPointStatus.PULSE_H)
            {
                //RectangleF rect = new RectangleF(pt.X - szUnit.Width / 2, pt.Y + 35,
                //                             szUnit.Width, szUnit.Height * temperPos);
                RectangleF rect = new RectangleF(pt.X - szUnit.Width / 2, pt.Y + 45,
                                            szUnit.Width, szUnit.Height * temperPos);
                //CJ20110812 改变文本的位置
                grfx.ScaleTransform(1, -1);
                rect.Y *= -1;
                fontText = new Font("宋体", 15, FontStyle.Bold);
                //CJ20110812 设置脉搏大于140的文本字体
                drawStringNurseEvent(ref grfx, rect, valFloat.ToString());
                fontText = new Font("宋体", 11);
                //CJ20110812 恢复系统默认字体
                grfx.ScaleTransform(1, -1);
            }

            // 体温小于35度事件
            if (ptStatus == GraphPointStatus.TEMPERATURE_L)
            {
                DrawLegend(ref grfx, GraphPointType.Arrow, (int)(pt.X), (int)(pt.Y - 2 * szUnit.Height));
                //LB20110524新增
                return 0;
            }

            // 脉搏不清情况
            if (ptStatus == GraphPointStatus.PULSE_NO)
            {
                RectangleF rect = new RectangleF(pt.X - szUnit.Width / 2, pt.Y + 10,
                                             szUnit.Width, szUnit.Height * temperPos);
                //画文本
                grfx.ScaleTransform(1, -1);
                rect.Y *= -1;
                
                grfx.DrawString("不清", fontText, Brushes.Blue, rect);
                grfx.ScaleTransform(1, -1);

                //画符号
                DrawLegend(ref grfx, GraphPointType.Symbols, (int)(pt.X), (int)(pt.Y + szUnit.Height * 1.5));
                DrawLegend(ref grfx, GraphPointType.Arrow, (int)(pt.X), (int)(pt.Y - 3 * szUnit.Height));
                
                return 0;
            }
            
            return 1;
        }
        
        
        /// <summary>
        /// 画降温数据
        /// </summary>
        /// <param name="bmp"></param>
        public void drawDownTemper(ref Graphics grfx, float valStart, float valStep)
        {
            SizeF       szUnit      = _bodyTemperParams.SzUnit;
            
            Pen         pen         = new Pen(Color.Red, 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            
            PointF      pt0         = new PointF();
            PointF      pt1         = new PointF();
            
            int         col         = 0;
            
            DateTime    dtStart     = _dtStart.AddHours(_bodyTemperParams.StartTime);
            
            foreach(DataRow dr in dtDownTemper.Rows)
            {            
                // 获取时间坐标位置
                DateTime dtMeasure = (DateTime)dr["TIME_POINT"];
                TimeSpan tspan = dtMeasure.Subtract(dtStart);
                
                col = (int)(tspan.TotalHours / STEP_HOURS); 
                pt0.X = (int)((col + 0.5) * szUnit.Width);
                pt1.X = pt0.X;
                
                // 获取值坐标位置
                float   valFloat    = 0;
                string  val         = dr["PRE_TEMPER"].ToString();
                
                if (float.TryParse(val, out valFloat) == true)
                {
                    pt0.Y = (int)(((valFloat - valStart) / valStep) * szUnit.Height);
                }
                
                val = dr["DOWN_TEMPER"].ToString();
                if (float.TryParse(val, out valFloat) == true)
                {
                    pt1.Y = (int)(((valFloat - valStart) / valStep) * szUnit.Height);
                }
                
                // 画图
                DrawLegend(ref grfx, GraphPointType.HEARTRATE, (int)pt1.X, (int)pt1.Y);
                grfx.DrawLine(pen, pt0, pt1);
            }
        }
        
        
        /// <summary>
        /// 画脉脉搏短绌
        /// </summary>
        /// <param name="grfx"></param>
        /// <param name="valStart"></param>
        /// <param name="valStep"></param>
        private void drawPulseShort(ref Graphics grfx, Size szRng)
        {
            grfx.ResetTransform();
            grfx.TranslateTransform(0, szRng.Height);
            grfx.ScaleTransform(1, -1);
            
            #region 获取参数
            SizeF szUnit = _bodyTemperParams.SzUnit;
            
            string[] parts = _bodyTemperParams.PulseShort.Split(ComConst.STR.COMMA.ToCharArray());
            string vitalCode0 = parts[0].Trim();
            string vitalCode1 = parts[1].Trim();
            
            float valStart = 0;
            float valStep = 0;
            if (float.TryParse(parts[2].Trim(), out valStart) == false)
            {
                return;
            }
            
            if (float.TryParse(parts[3].Trim(), out valStep) == false)
            {
                return;
            }
            
            if (vitalCode0.CompareTo(vitalCode1) > 0)
            {
                string temp = vitalCode0;
                vitalCode0 = vitalCode1;
                vitalCode1 = temp;
            }
            #endregion
            
            #region 获取数据
            string condition = "VITAL_CODE = " + SqlManager.SqlConvert(vitalCode0)
                             + "OR VITAL_CODE = " + SqlManager.SqlConvert(vitalCode1);
            
            // 获取数据
            DateTime dtStart = _dtStart.AddHours(_bodyTemperParams.StartTime);
            DateTime dtEnd   = _dtStart.AddDays(ComConst.VAL.DAYS_PER_WEEK).AddHours(_bodyTemperParams.StartTime);
            
            string filter = "TIME_POINT >= " + SqlManager.SqlConvert(dtStart.ToString(ComConst.FMT_DATE.LONG));
            filter += " AND TIME_POINT < " + SqlManager.SqlConvert(dtEnd.ToString(ComConst.FMT_DATE.LONG));
            filter += " AND ( " + condition + ")";
            
            DataRow[] drFind = _dsNursing.Tables[0].Select(filter, "TIME_POINT ASC, VITAL_CODE ASC");
            #endregion
            
            #region 定义变量
            ArrayList arr0 = new ArrayList();
            ArrayList arr1 = new ArrayList();
            ArrayList arrC = new ArrayList();
            
            int     col     = 0;
            int     colPre  = -1;
            
            bool    exist   = false;
            
            bool    pair    = true;
            int     row     = -1;
            
            GraphicsPath path = new GraphicsPath();
            #endregion
            
            while(row < drFind.Length)
            {
                #region 如果找到不匹配的点, 获取路径
                if (pair == false)
                {
                    pair = true;
                    
                    if (arr0.Count > 1 && arr1.Count > 1)
                    {
                        path = getGraphicsPath(ref arr0, ref arr1, ref arrC);
                        
                        if (exist == false)
                        {
                            grfx.SetClip(path);
                        }
                        else
                        {
                            grfx.SetClip(path, CombineMode.Union);
                        }
                        
                        exist = true;                                                
                    }
                    
                    arr0.Clear();
                    arr1.Clear();
                }
                #endregion
                
                #region 获取匹配的数据点
                row++;
                if (row >= drFind.Length)
                {
                    pair = false;
                    continue;
                }
                
                DataRow dr = drFind[row];
                DateTime dtMeasure = (DateTime)dr["TIME_POINT"];
                TimeSpan tspan = dtMeasure.Subtract(dtStart);
                
                PointF  pt0 = new PointF();
                PointF  pt1 = new PointF();
                
                float   valFloat = 0;
                
                // 第一点
                if (dr["VITAL_CODE"].ToString().Equals(vitalCode0) == false)
                {
                    pair = false;
                    continue;
                }
                
                col = (int)(tspan.TotalHours / STEP_HOURS);
                if (isBreakPoint(colPre, col) == true)
                {
                    colPre = -1;
                    row--;
                    
                    pair = false;
                    continue;
                }                
                
                pt0.X = (int)((col + 0.5) * szUnit.Width);
                
                string val = dr["VITAL_SIGNS_CVALUES"].ToString();
                if (float.TryParse(val, out valFloat) == false)
                {
                    pair = false;
                    continue;
                }
                
                pt0.Y = (int)(((valFloat - valStart) / valStep) * szUnit.Height);
                
                // 第二个点
                row++;
                if (row >= drFind.Length)
                {
                    pair = false;
                    continue;
                }
                
                dr = drFind[row];
                
                // 条件检查
                val = dr["VITAL_SIGNS_CVALUES"].ToString();
                if (dtMeasure.CompareTo((DateTime)dr["TIME_POINT"]) != 0
                    || dr["VITAL_CODE"].ToString().Equals(vitalCode1) == false
                    || float.TryParse(val, out valFloat) == false)
                {
                    row--;
                    pair = false;
                    continue;
                }
                
                pt1.X = pt0.X;
                pt1.Y = (int)(((valFloat - valStart) / valStep) * szUnit.Height);
                
                if (colPre != col)
                {
                    colPre = col;
                }
                
                arr0.Add(pt0);
                arr1.Add(pt1);
                #endregion
            }
            
            #region 如果找到不匹配的点, 获取路径
            if (pair == false && arr0.Count > 1 && arr1.Count > 1)
            {
                path = getGraphicsPath(ref arr0, ref arr1, ref arrC);
                
                if (exist == false)
                {
                    grfx.SetClip(path);
                }
                else
                {
                    grfx.SetClip(path, CombineMode.Union);
                }
                
                exist = true;
                
                path.Dispose();
                arr0.Clear();
                arr1.Clear();
            }
            #endregion
            
            #region 画图
            float cx = -1 * szRng.Width / 2;
            Point ptStart = new Point(0, 0);
            Point ptEnd   = new Point(0, szRng.Height);
            
            while(exist && cx < szRng.Width)
            {
                ptStart.X = (int)(cx);
                ptEnd.X   = (int)(cx + szRng.Width / 2);
                
                grfx.DrawLine(Pens.Red, ptStart, ptEnd);
                
                cx += 5;
            }
            
            grfx.ResetClip();
            
            // 画闭合线
            PointF ptStore = new PointF();
            int c = 0;
            while(c < arrC.Count - 1)
            {
                ptStore = (PointF)arrC[c++];
                ptStart = new Point((int)(ptStore.X), (int)(ptStore.Y));
                
                ptStore = (PointF)arrC[c++];
                ptEnd   = new Point((int)(ptStore.X), (int)(ptStore.Y));
                
                grfx.DrawLine(Pens.Red, ptStart, ptEnd);
            }
            #endregion
        }
        
        
        private GraphicsPath getGraphicsPath(ref ArrayList arr0, ref ArrayList arr1, ref ArrayList arrC)
        {
            PointF pt = new PointF();
            
            PointF[] arrPoint = new PointF[arr0.Count + arr1.Count + 1];
            
            int c = 0;
            for(; c < arr0.Count; c++)
            {
                pt = (PointF)arr0[c];
                arrPoint[c] = new PointF(pt.X, pt.Y);
            }
            
            for(int k = arr0.Count + arr1.Count - 1; k >= arr0.Count; k--)
            {
                pt = (PointF)arr1[k - arr0.Count];
                arrPoint[c++] = new PointF(pt.X, pt.Y);
            }
            
            pt = (PointF)arr0[0];
            arrPoint[c] = new PointF(pt.X, pt.Y);
            
            GraphicsPath path = new GraphicsPath(FillMode.Winding);
            path.AddLines(arrPoint);
            
            // 获取闭合点
            if (arr0.Count > 1)
            {
                pt = (PointF)arr0[0];
                arrC.Add(new PointF(pt.X, pt.Y));
                
                pt = (PointF)arr1[0];
                arrC.Add(new PointF(pt.X, pt.Y));
                
                pt = (PointF)arr0[arr0.Count - 1];
                arrC.Add(new PointF(pt.X, pt.Y));
                
                pt = (PointF)arr1[arr1.Count - 1];
                arrC.Add(new PointF(pt.X, pt.Y));                
            }
            
            return path;
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
        private void getDownTemperData(string condition, string itemCode)
        {
            // 数据结构准备
            if (dtDownTemper == null)
            {
                createDownTemper();
            }
            
            // 查询数据
            DateTime dtStart = _dtStart.AddHours(_bodyTemperParams.StartTime);
            DateTime dtEnd   = _dtStart.AddDays(ComConst.VAL.DAYS_PER_WEEK + 1).AddHours(_bodyTemperParams.StartTime);
            
            string filter = "TIME_POINT >= " + SqlManager.SqlConvert(dtStart.ToString(ComConst.FMT_DATE.LONG));
            filter += " AND TIME_POINT < " + SqlManager.SqlConvert(dtEnd.ToString(ComConst.FMT_DATE.LONG));
            filter += " AND ( " + condition + ")";
            
            DataRow[] drFind = _dsNursing.Tables[0].Select(filter, "TIME_POINT ASC");
            
            dtDownTemper.Rows.Clear();
            
            DataRow drNew = dtDownTemper.NewRow();
            
            float preTemper     = 0F;
            float downTemper    = 0F;
            float val           = 0F;
            
            for(int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];
                
                // 获取值
                if (float.TryParse(dr["VITAL_SIGNS_CVALUES"].ToString(), out val) == false)
                {
                    continue;
                }
                
                // 如果是降温, 获取降温后的值
                if ((dr["VITAL_CODE"].ToString()).Equals(itemCode) == true)
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
                    // 如果有降温记录, 保存
                    if (downTemper > 0)
                    {
                        drNew["PRE_TEMPER"]     = preTemper.ToString();
                        drNew["DOWN_TEMPER"]    = downTemper.ToString();
                        dtDownTemper.Rows.Add(drNew);
                        
                        drNew = dtDownTemper.NewRow();
                    }
                    
                    // 保存正常体温记录
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
            
        }
        
        
		/// <summary>
		/// 画其它护理事件的文本显示
		/// </summary>
		/// <param name="bmp"></param>
		private void drawOtherNursingEventText(ref Graphics grfx)
		{
		    int tmStart        = _bodyTemperParams.StartTime;               // 每天测量的开始时间点
            float tmInterval   = (float)STEP_HOURS;                         // 测量时间间隔
            int timesPerDay    = ComConst.VAL.HOURS_PER_DAY / STEP_HOURS;   // 每天测量的次数
            
		    // 获取数据		    
		    DateTime dtStart = _dtStart.AddHours(_bodyTemperParams.StartTime);		    
            DateTime dtEnd   = _dtStart.AddDays(ComConst.VAL.DAYS_PER_WEEK).AddHours(_bodyTemperParams.StartTime);
            
            string filter = "(" + _bodyTemperParams.NursingEventFilter + ")";
            
            // 如果是第一页体温图
            if (dtStart.Date.Equals(_dtRngStart.Date) == true)
            {
                filter += " AND TIME_POINT >= " + HISPlus.SqlManager.SqlConvert(dtStart.ToString(ComConst.FMT_DATE.SHORT))
				        + " AND TIME_POINT < " + HISPlus.SqlManager.SqlConvert(dtEnd.ToString(ComConst.FMT_DATE.LONG));
            }    
			else
			{
			    filter += " AND TIME_POINT >= " + HISPlus.SqlManager.SqlConvert(dtStart.ToString(ComConst.FMT_DATE.LONG))
				        + " AND TIME_POINT < " + HISPlus.SqlManager.SqlConvert(dtEnd.ToString(ComConst.FMT_DATE.LONG));
			}
            
			DataRow[] drFind = _dsNursing.Tables[0].Select(filter, "TIME_POINT ASC");
            
            // 数据处理
			drawFormat.Alignment = StringAlignment.Near;
            
            SizeF szUnit = _bodyTemperParams.SzUnit;
            
            RectangleF rect = new RectangleF(0, szUnit.Height * _bodyTemperParams.NurseEventPos,
                                             szUnit.Width, szUnit.Height * 10);
            
			int  col        = -1;   
			int  sect       = 0;    
			int  hour       = 0;    
            string eventName= string.Empty;
            
            int colPre      = -1;
            int count       = 0;                                                            // 最多两个事件放在一列上

            grfx.ResetTransform();
			for(int i = 0; i < drFind.Length; i++)
			{
                // 预处理
                eventName = drFind[i]["VITAL_SIGNS"].ToString();                             // 事件名称
                string vitalCode = drFind[i]["VITAL_CODE"].ToString();
                if (_bodyTemperParams.ValueOutEvent.IndexOf(vitalCode) >= 0)
                {
                    eventName += drFind[i]["VITAL_SIGNS_CVALUES"].ToString();
                }
                
                // 正常处理
				DateTime dtMeasure  = DateTime.Parse(drFind[i]["TIME_POINT"].ToString());
				TimeSpan tSpan      = dtMeasure.Subtract(_dtStart.Date);
                
				sect = (int)(tSpan.TotalHours / ComConst.VAL.HOURS_PER_DAY);
				hour = (int)(tSpan.TotalHours % ComConst.VAL.HOURS_PER_DAY);
                
				col  = sect * timesPerDay + (int)(Math.Floor((float)((hour - tmStart) / tmInterval)));
				col  = (col < 0? 0: col);
				
                if (col <= colPre)
                {
                    col = colPre + 1;
                    //colPre  = col;
                    //count   = 0;
                }
                
                if (col > timesPerDay * ComConst.VAL.DAYS_PER_WEEK - 1) return;
                
				// 护理事件名称
				if (_bodyTemperParams.TimeOutEvent.IndexOf(vitalCode) >= 0)
				{
                    eventName += "|" + getTimeText(dtMeasure.ToString(ComConst.FMT_DATE.TIME_SHORT));
                }
                
 
                rect.X  = col * szUnit.Width;
                   
                // 输出
                drawStringNurseEvent(ref grfx, rect, eventName);
                colPre = col;
			}
		}                
		
		
		/// <summary>
		/// 在画板上输出护理事件文本
		/// </summary>
		/// <param name="grfx"></param>
		/// <param name="rect"></param>
		/// <param name="text"></param>
		private void drawStringNurseEvent(ref Graphics grfx, RectangleF rect, string text)
		{
		    RectangleF rectDraw = new RectangleF(rect.X, rect.Y, 
		                                         _bodyTemperParams.SzUnit.Width, 
		                                         _bodyTemperParams.SzUnit.Height);
            
            // 不升符号显示
            if (text.Equals("不升") == true && _bodyTemperParams.TempNotAscendSymbol.Equals("1") == true)
            {
                int x = (int)(rectDraw.X + rectDraw.Width / 2);
                grfx.DrawLine(Pens.Red, x, rectDraw.Y + 2, x, rectDraw.Y + _bodyTemperParams.SzUnit.Height * 2 - 2);
                
                rectDraw.X += 2;
                rectDraw.Y += _bodyTemperParams.SzUnit.Height + 2;
                rectDraw.Height -= 4;
                
                grfx.DrawLine(Pens.Red, rectDraw.X, rectDraw.Y, x, rectDraw.Y + rectDraw.Height);
                
                int x1 = (int)(x + (x - rectDraw.X - 1) * 2);
                grfx.DrawLine(Pens.Red, x1, rectDraw.Y, x, rectDraw.Y + rectDraw.Height);
                return;
            }
            
		    for(int i = 0; i < text.Length; i++)
		    {
		        if (i > 0)
		        {
		            rectDraw.Y += _bodyTemperParams.SzUnit.Height;
		        }
		        
		        string charText = text.Substring(i, 1);
		         
		        if (charText.Equals("|") == false)
		        {
                   
		            grfx.DrawString(charText, fontText, Brushes.Red, rectDraw);
		        }
		        else
		        {
		            rectDraw.X += (int)(rectDraw.Width / 2);
		            grfx.DrawLine(Pens.Red, new Point((int)(rectDraw.X), (int)(rectDraw.Y) + 2),
		                                    new Point((int)(rectDraw.X), (int)(rectDraw.Y + rectDraw.Height) - 2));
                    rectDraw.X -= (int)(rectDraw.Width / 2);
		        }
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
        
        
        /// <summary>
        /// 检查前一个点是不是断点
        /// </summary>
        /// <returns></returns>
        private bool isBreakPoint(int colPre, int colCur)
        {
            if (colPre == -1)
            {
                return false;
            }
            
            for(int i = 0; i < arrBreakCols.Count; i++)
            {
                int colVal = (int)(arrBreakCols[i]);
                
                if (colPre <= colVal && colVal <= colCur)
                {
                    return true;
                }                
            }
            
            return false;
        }


        /// <summary>
        /// 画其它护理事件的文本显示 记录体温首次入院高于39的病人
        /// </summary>
        /// <param name="bmp"></param>
        private void  drawHighTemperData(ref Graphics grfx, float valStart, float valStep)
        {

            int   tmStart     = _bodyTemperParams.StartTime;               // 每天测量的开始时间点
            float tmInterval  = (float)STEP_HOURS;                         // 测量时间间隔
            int   timesPerDay = ComConst.VAL.HOURS_PER_DAY / STEP_HOURS;   // 每天测量的次数

            // 获取数据		    
            DateTime dtStart = _dtStart.AddHours(_bodyTemperParams.StartTime);
            DateTime dtEnd   = _dtStart.AddDays(ComConst.VAL.DAYS_PER_WEEK).AddHours(_bodyTemperParams.StartTime);

            //-------------------------
            // 判断 是否是第一天
            if (dtStart.Date.Equals(_dtRngStart.Date) == false)
            {
                return;
            }

            // 获取数据
            string filter = "VITAL_SIGNS LIKE '%体温' "
                        + " AND TIME_POINT >= " + HISPlus.SqlManager.SqlConvert(dtStart.ToString(ComConst.FMT_DATE.SHORT))
                        + " AND TIME_POINT < " + HISPlus.SqlManager.SqlConvert(dtEnd.ToString(ComConst.FMT_DATE.LONG));
            
            DataRow[] drFind = _dsNursing.Tables[0].Select(filter, "TIME_POINT ASC");
            if (drFind.Length == 0) return;

            DateTime dtMeasure = (DateTime)drFind[0]["TIME_POINT"];
            string val = drFind[0]["VITAL_SIGNS_CVALUES"].ToString();

            // 判断体温是否大于39
            float valFloat = 0;
            float.TryParse(val, out valFloat);
            if (valFloat < 39) return;

            // 画复试符号
            SizeF szUnit = _bodyTemperParams.SzUnit;
            
            PointF pt = new PointF(-1, -1);
            int sect  = 0;
            int hour  = 0;  
            int col   = -1;
         
            // X位置
            TimeSpan tspan = dtMeasure.Subtract(dtStart);

            sect = (int)(tspan.TotalHours / ComConst.VAL.HOURS_PER_DAY);
            hour = (int)(tspan.TotalHours % ComConst.VAL.HOURS_PER_DAY);

            col  = sect * timesPerDay + (int)(Math.Floor((float)((hour - tmStart) / tmInterval)));
            col  = (col < 0 ? 0 : col);

            //col = (int)(tspan.TotalHours / STEP_HOURS); 
            pt.X = (int)((col + 0.5) * szUnit.Width);

            // Y位置
            pt.Y = (int)(((valFloat - valStart) / valStep + 0.5) * szUnit.Height);

            // 画图
            DrawLegend (ref grfx,GraphPointType.VERIFY,(int)(pt.X),(int)(pt.Y));
        }                
		
        #endregion
        
        
        #region 共通函数
        /// <summary>
        /// 是否是需要确认的项目
        /// </summary>
        /// <returns></returns>
        private bool isVerifyItem(string condition)
        {
            // 没有没置确认项目属性
            if (_bodyTemperParams.VerifyVitalCode.Length == 0
                || ( _bodyTemperParams.VerifyDown == 0 && _bodyTemperParams.VerifyUp == 0 ))
            {
                return false;
            }
            
            
            string pattern = "VITAL_CODE=" + SqlManager.SqlConvert(_bodyTemperParams.VerifyVitalCode).Trim();
            condition = condition.Replace(ComConst.STR.BLANK, string.Empty);
            
            if (condition.IndexOf(pattern) >= 0)
            {
                return true;
            }
            
            return false;
        }
        
        
        /// <summary>
        /// 判断是不是确认点
        /// </summary>
        /// <param name="valPre"></param>
        /// <param name="valCur"></param>
        /// <returns></returns>
        private int chkVerifyPoint(float valPre, float valCur)
        {
            if (valPre == 0 || valCur == 0)
            {
                return 0;
            }
            
            if (valCur > valPre)
            {
                if((valCur - valPre) >= _bodyTemperParams.VerifyUp)
                {
                    return 1;
                }
            }
            
            if (valCur < valPre)
            {
                if ((valPre - valCur) >= _bodyTemperParams.VerifyDown)
                {
                    return -1;
                }
            }
            
            return 0;
        }


        /// <summary>
        /// 从字符串中获取合计值
        /// </summary>
        /// <returns></returns>
        public float GetSumValueFromString(string str)
        {
            // 获取数字
            string strNum = "0123456789.";

            // 把所有非数字字符变成空格
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (strNum.IndexOf(str.Substring(i, 1)) < 0)
                {
                    sb.Append(" ");
                }
                else
                {
                    sb.Append(str.Substring(i, 1));
                }
            }

            str = sb.ToString();

            // 合计数值
            string[] parts = str.Split(" ".ToCharArray());
            float sum = 0F;
            float val = 0F;
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].Trim().Length == 0)
                {
                    continue;
                }

                if (float.TryParse(parts[i], out val) == true)
                {
                    sum += val;
                }
            }

            return sum;
        }        
        #endregion
    }
}
