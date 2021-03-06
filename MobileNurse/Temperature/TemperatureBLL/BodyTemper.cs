using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Text;
using HISPlus;
using GraphPointType = TemperatureBLL.BodyTemperParams.GraphPointType;

namespace TemperatureBLL
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
            NORMAL = 0,                // 正常
            TEMPERATURE_H = 1,                // 体温过高
            TEMPERATURE_L = 2,                // 体温过低
            PULSE_H = 3,                // 脉搏过高
            PULSE_NO = 4                 // 脉搏不清
        }
        #endregion


        #region 变量
        private const int STEP_HOURS = 4;                                    // 每四个小时一个单元格

        protected BodyTemperParams _bodyTemperParams = null;                                 // 体温单参数

        protected DateTime _dtStart = DataType.DateTime_Null();             // 作图起始日期
        protected DateTime _dtNow = DataType.DateTime_Null();             // 当前的日期
        protected DateTime _dtInp = DataType.DateTime_Null();             // 入院日期                       
        protected DataSet _dsNursing = null;                                 // 护理数据
        // 如 -4 表示从前一天的20点开到 第二天的20点算一天
        protected int _graphStarTime = 0;                                    // 体温开始时间点

        // 作图工具
        private StringFormat drawFormat = new StringFormat();                   // 文本布局
        public Font fontText = new Font("宋体", 11);                 // 字体

        // 临时缓存
        public DataTable dtDownTemper = null;                                 // 降温数据
        private ArrayList arrBreakCols = new ArrayList();                      // 断开点
        #endregion


        #region 属性
        /// <summary>
        /// 体温单参数
        /// </summary>
        public BodyTemperParams Params
        {
            get { return _bodyTemperParams; }
            set { _bodyTemperParams = value; }
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
            get { return _dtNow; }
            set { _dtNow = value; }
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
            get { return _dsNursing; }
            set { _dsNursing = value; }
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

            switch (eLegend)
            {
                case GraphPointType.Pulse:       // 脉搏
                    grfx.DrawEllipse(Pens.Red, rect);
                    grfx.FillEllipse(Brushes.Red, rect);
                    break;

                case GraphPointType.Heartrate:   // 心率
                    rect.X -= 2;
                    rect.Width += 4;
                    rect.Y -= 2;
                    rect.Height += 4;

                    grfx.DrawEllipse(Pens.Red, rect);
                    break;

                case GraphPointType.Mouse:       // 口表
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

                case GraphPointType.Anus:        // 肛表
                    grfx.DrawEllipse(Pens.Blue, rect);
                    break;

                case GraphPointType.Armpit:      // 腋表
                    //szUnit.Width = (int)(szUnit.Width * 1.2);
                    //szUnit.Height = (int)(szUnit.Height * 1.2);
                    rect = new Rectangle(x - szUnit.Width / 2, y - szUnit.Height / 2, szUnit.Width, szUnit.Height);

                    grfx.DrawLine(Pens.Blue, rect.Left, rect.Top, rect.Left + rect.Width, rect.Top + rect.Height);
                    grfx.DrawLine(Pens.Blue, rect.Left, rect.Top + rect.Height, rect.Left + rect.Width, rect.Top);
                    break;

                case GraphPointType.Breath:     // 呼吸
                    grfx.DrawEllipse(Pens.Blue, rect);
                    grfx.FillEllipse(Brushes.Blue, rect);
                    break;

                case GraphPointType.Verify:     // 确认
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

        #endregion


        #region 数据处理

        /// <summary>
        /// 获取术后日数
        /// </summary>
        /// <param name="dtCurrent"></param>
        /// <returns></returns>
        public string getOperedDays(DateTime dtCurrent)
        {
            switch (_bodyTemperParams.OperateFmt)
            {
                case 0:
                    return getOperedDays0(dtCurrent);
                case 1:
                    return getOperedDays1(dtCurrent);
                case 2:
                    return getOperedDays2(dtCurrent);
                default:
                    return "未指定格式";
            }
        }
        /// <summary>
        /// 获取术后日数
        /// </summary>
        /// <param name="dtCurrent"></param>
        /// <returns></returns>
        public string getOperedDays(DateTime dtCurrent, int daysFmt)
        {
            // 获取最近手术时间
            string filter = "VITAL_CODE = " + SqlManager.SqlConvert(_bodyTemperParams.OperationVitalCode)
                + " AND TIME_POINT > " + SqlManager.SqlConvert(dtCurrent.AddDays(-1 * _bodyTemperParams.OperatedDaysShow).ToString(ComConst.FMT_DATE.SHORT))
                + " AND TIME_POINT < " + SqlManager.SqlConvert(dtCurrent.AddDays(1).ToString(ComConst.FMT_DATE.SHORT));

            DataRow[] drFind = _dsNursing.Tables[0].Select(filter, "TIME_POINT DESC");
            string operDays = "";
            if (drFind.Length > 0)
            {
                int[] days = new int[drFind.Length];
                for (int i = 0; i < drFind.Length; i++)
                {
                    days[i] = dtCurrent.Date.Subtract(DateTime.Parse(drFind[i]["TIME_POINT"].ToString()).Date).Days;
                }
                operDays = days[0].ToString();

                if (days[0] == 1)
                {
                    for (int j = 1; j < days.Length; j++)
                        operDays += "/" + days[j];

                }
                else if (days[0] == 0)
                {
                    if (days.Length == 1)
                        operDays = days[0].ToString();
                    else
                        operDays = days[1].ToString();
                }
            }
            return operDays;
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
                + " AND TIME_POINT > " + SqlManager.SqlConvert(dtCurrent.AddDays(-1 * _bodyTemperParams.OperatedDaysShow).ToString(ComConst.FMT_DATE.SHORT))
                + " AND TIME_POINT < " + SqlManager.SqlConvert(dtCurrent.AddDays(1).ToString(ComConst.FMT_DATE.SHORT));

            DataRow[] drFind = _dsNursing.Tables[0].Select(filter, "TIME_POINT DESC");
            DateTime dtOper = dtCurrent;
            TimeSpan tSpan;
            string preOperDays = string.Empty;                         // 距倒数第二次手术的天数
            string operedDays = string.Empty;                         // 术后天数

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
            for (int i = 0; i < _bodyTemperParams.OperatedMaxCount && i < parts.Length; i++)
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

            DataRow[] drFind = _dsNursing.Tables[0].Select(filter, "TIME_POINT DESC");
            string preOperDays = string.Empty;                         // 距倒数第二次手术的天数
            string operedDays = string.Empty;                         // 术后天数

            // 如果没有手术记录, 返回空
            if (drFind.Length == 0)
            {
                return string.Empty;
            }

            DateTime dtOper = DataType.DateTime_Null();
            DateTime dtNext = dtCurrent;
            string tag = string.Empty;

            for (int i = 0; i < drFind.Length; i++)
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
                        operedDays = tSpan.TotalDays + "/";
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
        /// 获取术后天数
        /// </summary>
        /// <remarks>手术记录按日期倒排序</remarks>
        /// <param name="dtCurrent">要计算的日期</param>
        /// <returns></returns>
        private string getOperedDays2(DateTime dtCurrent)
        {
            // 获取最近手术时间
            string filter = "( VITAL_CODE = " + SqlManager.SqlConvert(_bodyTemperParams.OperationVitalCode)
                +" or VITAL_CODE = " + SqlManager.SqlConvert(_bodyTemperParams.BirthVitalCode)+")"//分娩天数显示
                + " AND TIME_POINT > " + SqlManager.SqlConvert(dtCurrent.AddDays(-1 * _bodyTemperParams.OperatedDaysShow).ToString(ComConst.FMT_DATE.SHORT))
                + " AND TIME_POINT < " + SqlManager.SqlConvert(dtCurrent.AddDays(1).ToString(ComConst.FMT_DATE.SHORT));

            DataRow[] drFind = _dsNursing.Tables[0].Select(filter, "TIME_POINT DESC");
            DateTime dtOper = dtCurrent;
            TimeSpan tSpan;
            string preOperDays = string.Empty;                         // 距倒数第二次手术的天数
            string operedDays = string.Empty;                         // 术后天数

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
            for (int i = 0; i < _bodyTemperParams.OperatedMaxCount && i < parts.Length; i++)
            {
                if (i > 0) operedDays += @"/";
                operedDays += parts[i];
            }
            if (operedDays.Contains("/"))
            {

                if (operedDays.Split('/')[0].ToString() == "0")
                    return operedDays.Split('/')[1];
                else if (operedDays.Split('/')[0].ToString() != "1")
                    return operedDays.Split('/')[0];
                else
                    return operedDays;
            }
            else
                return operedDays;
        }

        /// <summary>
        /// 重新设置大便项目
        /// </summary>
        private void resetStoolData()
        {
            ArrayList arrTime = new ArrayList();
            ArrayList arrValue = new ArrayList();

            // 获取各个项目的代码
            string preCountItem = string.Empty;                     // 灌肠前次数代码
            string nextCountItem = string.Empty;                     // 灌肠后次数代码
            string clysterItem = string.Empty;                     // 灌肠次数代码
            string stoolItem = string.Empty;                     // 大便次数代码

            string[] parts = _bodyTemperParams.StoolComponent.Split(ComConst.STR.COMMA.ToCharArray());
            if (parts.Length < 4)
            {
                return;
            }

            stoolItem = parts[0];
            preCountItem = parts[1];
            clysterItem = parts[2];
            nextCountItem = parts[3];

            // 查找相应的项目
            if (_dsNursing == null || _dsNursing.Tables.Count == 0 || _dsNursing.Tables[0].Rows.Count == 0)
            {
                return;
            }

            DataRow[] drFind = _dsNursing.Tables[0].Select(string.Empty, "TIME_POINT");

            DateTime dtStool = DataType.DateTime_Null();
            DateTime dtStart = DataType.DateTime_Null();
            DateTime dtEnd = DataType.DateTime_Null();
            DateTime dtCurrent;

            string preCount = string.Empty;                         // 灌肠前次数
            string nextCount = string.Empty;                         // 灌肠后次数
            string clyster = string.Empty;                         // 灌肠次数
            string stoolCount = string.Empty;                         // 大便次数

            for (int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];

                string vitalCode = dr["VITAL_CODE"].ToString();                    // 项目代码
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

                        preCount = string.Empty;                         // 灌肠前次数
                        nextCount = string.Empty;                         // 灌肠后次数
                        clyster = string.Empty;                         // 灌肠次数
                        stoolCount = string.Empty;                         // 大便次数

                        dtStart = DataType.DateTime_Null();
                        dtEnd = DataType.DateTime_Null();
                    }
                }

                // 获取数据
                if (vitalCode.Equals(stoolItem))             // 大便
                {
                    dtStool = dtCurrent;
                    stoolCount = vitalValue;
                }
                else if (vitalCode.Equals(preCountItem))     // 灌肠前次数
                {
                    setRngTime(dtCurrent, ref dtStart, ref dtEnd);
                    preCount = vitalValue;
                }
                else if (vitalCode.Equals(clysterItem))      // 灌肠次数
                {
                    setRngTime(dtCurrent, ref dtStart, ref dtEnd);
                    clyster = vitalValue;
                }
                else if (vitalCode.Equals(nextCountItem))    // 灌肠后次数
                {
                    setRngTime(dtCurrent, ref dtStart, ref dtEnd);
                    nextCount = vitalValue;
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

                preCount = string.Empty;                         // 灌肠前次数
                nextCount = string.Empty;                         // 灌肠后次数
                clyster = string.Empty;                         // 灌肠次数
                stoolCount = string.Empty;                         // 大便次数

                dtStart = DataType.DateTime_Null();
                dtEnd = DataType.DateTime_Null();
            }

            // 插入数据
            for (int j = 0; j < arrTime.Count; j++)
            {
                DataRow drFirst = _dsNursing.Tables[0].Rows[0];

                DataRow drNew = _dsNursing.Tables[0].NewRow();
                drNew["ATTRIBUTE"] = "3";
                drNew["PATIENT_ID"] = drFirst["PATIENT_ID"];
                drNew["VISIT_ID"] = drFirst["VISIT_ID"];
                drNew["RECORDING_DATE"] = (DateTime)arrTime[j];
                drNew["TIME_POINT"] = (DateTime)arrTime[j];
                drNew["VITAL_SIGNS"] = "大便";
                drNew["UNITS"] = string.Empty;
                drNew["CLASS_CODE"] = "B";
                drNew["VITAL_CODE"] = stoolItem;
                drNew["VITAL_SIGNS_CVALUES"] = (string)arrValue[j];
                drNew["WARD_CODE"] = drFirst["WARD_CODE"];

                _dsNursing.Tables[0].Rows.Add(drNew);
            }
        }


        private void setRngTime(DateTime dtCurrent, ref DateTime dtStart, ref DateTime dtEnd)
        {
            if (DataType.DateTime_IsNull(ref dtStart) == true)
            {
                dtStart = dtCurrent;
                dtEnd = dtCurrent;
            }
            else
            {
                dtEnd = dtCurrent;
            }
        }
        #endregion


        #region 方法
        /// <summary>
        /// 分析参数
        /// </summary>
        public void ParsePara()
        {
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
        public void getDownTemperData(string condition, string itemCode)
        {
            // 数据结构准备
            if (dtDownTemper == null)
            {
                createDownTemper();
            }

            // 查询数据
            DateTime dtStart = _dtStart.AddHours(_bodyTemperParams.StartTime);
            DateTime dtEnd = _dtStart.AddDays(ComConst.VAL.DAYS_PER_WEEK + 1).AddHours(_bodyTemperParams.StartTime);

            string filter = "TIME_POINT >= " + SqlManager.SqlConvert(dtStart.ToString(ComConst.FMT_DATE.LONG));
            filter += " AND TIME_POINT < " + SqlManager.SqlConvert(dtEnd.ToString(ComConst.FMT_DATE.LONG));
            filter += " AND ( " + condition + ")";

            DataRow[] drFind = _dsNursing.Tables[0].Select(filter, "TIME_POINT ASC");

            dtDownTemper.Rows.Clear();

            DataRow drNew = dtDownTemper.NewRow();

            float preTemper = 0F;
            float downTemper = 0F;
            float val = 0F;

            for (int i = 0; i < drFind.Length; i++)
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
                        drNew["PRE_TEMPER"] = preTemper.ToString();
                        drNew["DOWN_TEMPER"] = downTemper.ToString();
                        dtDownTemper.Rows.Add(drNew);

                        drNew = dtDownTemper.NewRow();
                    }

                    // 保存正常体温记录
                    drNew["TIME_POINT"] = dr["TIME_POINT"];
                    downTemper = 0;
                    preTemper = val;
                }
            }

            if (downTemper > 0)
            {
                drNew["PRE_TEMPER"] = preTemper.ToString();
                drNew["DOWN_TEMPER"] = downTemper.ToString();
                dtDownTemper.Rows.Add(drNew);
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

            for (int i = 0; i < text.Length; i++)
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
        public string getTimeText(string timeNum)
        {
            string timeText = string.Empty;
            string hour = string.Empty;
            string minute = string.Empty;

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


        #region 共通函数
        /// <summary>
        /// 是否是需要确认的项目
        /// </summary>
        /// <returns></returns>
        public bool isVerifyItem(string condition)
        {
            // 没有没置确认项目属性
            if (_bodyTemperParams.VerifyVitalCode.Length == 0
                || (_bodyTemperParams.VerifyDown == 0 && _bodyTemperParams.VerifyUp == 0))
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
        public bool chkVerifyPoint(double valPre, double valCur)
        {
            if (valPre == 0 || valCur == 0)
            {
                return false;
            }

            if (valCur > valPre)
            {
                if ((valCur - valPre) >= _bodyTemperParams.VerifyUp)
                {
                    return true;
                }
            }

            if (valCur < valPre)
            {
                if ((valPre - valCur) >= _bodyTemperParams.VerifyDown)
                {
                    return true;
                }
            }

            return false;
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
