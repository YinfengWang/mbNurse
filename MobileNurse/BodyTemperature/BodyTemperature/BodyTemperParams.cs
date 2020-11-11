using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;

namespace HISPlus
{
    public class BodyTemperParams
    {

        #region 结构体定义
        /// <summary>
        /// 画图点的类型
        /// </summary>
        public enum GraphPointType
        {
            MOUSE = 0,                    // 口表            
            ARMPIT = 1,                    // 腋表
            ANUS = 2,                    // 肛表
            PULSE = 3,                    // 脉搏
            HEARTRATE = 4,                    // 心率
            BREATH = 5,                    // 呼吸
            VERIFY = 6,                    // 确认
            Arrow = 7,                    // 箭头
            Symbols = 8                     // 符号
        }


        /// <summary>
        /// 体温图上尾部项目类型
        /// </summary>
        public enum TailItemType
        {
            SUM = 0,                            // 合计
            ONE = 1,                            // 单项
            TWO = 2,                            // 两项
            BREATH = 3,                         // 呼吸
            CLYSTER = 4                         // 灌肠
        }
        #endregion


        #region 变量
        protected DataSet _dsParams = null;                     // 参数集合

        protected bool _reloadParams = true;                     // 是否重新加载参数

        protected int _startTime = 0;                        // 开始时间 2
        protected int _dayStartTime = 0;                        // 一开的开始时间 (暂不用)

        protected SizeF _szUnit = new SizeF(12, 12);        // 主区域单元格大小
        protected string _topItems = string.Empty;             // 项头项目

        protected string _operatCode = string.Empty;             // 手术代码
        protected int _operateDaysShow = 10;                       // 手术显示天数
        protected int _operatMaxCount = 2;                        // 最多手次次数(体温图上的术后日数)
        protected int _operatFmt = 0;                        // 术后日数格式

        protected string _graphPointItems = string.Empty;             // 主区域作图项目
        protected int _temperEventPos = 5;                        // 主区域量体温事件位置
        protected int _nurseEventPos = 3;                        // 主区域事件位置
        protected string _nurseEventFilter = string.Empty;             // 护理事件过滤条件(不是所有护理事件都要在图上表示)

        protected string _valueOutNurseEvent = string.Empty;           // 要输出值的护理事件
        protected string _timeOutNurseEvent = string.Empty;             // 要输出时间的护理事件代码

        protected Font _fontCenterText = new Font("宋体", 11);     // 作图主区域字体

        protected string _tailItems = string.Empty;             // 尾部项目
        protected string _stoolComponent = string.Empty;             // 大便项目的组成

        // 降温确认
        protected string _verifyVitalCode = string.Empty;             // 体温代码
        protected float _verifyDown = 0F;                       // 降多少度要确认
        protected float _verifyUP = 0F;                       // 升多少度要确认

        protected string _tempNotAscendSymbol = string.Empty;          // 体温不升符号显示
        protected string _tempMouseSymbol = string.Empty;             // 口表体温显示符号

        // 脉搏短绌
        protected string _pulseShort = string.Empty;             // 脉搏短绌

        // 打印控制
        protected Point _printStartPoint = new Point(0, 0);          // 打印时左上角的位置
        protected float _printZoom = 6.27F;                    // 打印缩放比例

        // 显示控制
        private bool _showSumZero = false;                    // 显示合计项是0的        
        #endregion

        public BodyTemperParams()
        {

        }


        #region 属性
        /// <summary>
        /// 参数数据集
        /// </summary>
        public DataSet Parameters
        {
            get { return _dsParams; }
            set { _dsParams = value; }
        }


        /// <summary>
        /// 是否重新加载参数
        /// </summary>
        public bool ReloadParams
        {
            get { return _reloadParams; }
        }


        /// <summary>
        /// 一天的开始时间
        /// </summary>
        public int DayStartTime
        {
            get { return _dayStartTime; }
        }


        /// <summary>
        /// 开始时间
        /// </summary>
        public int StartTime
        {
            get { return _startTime; }
        }


        /// <summary>
        /// 最小单元格大小
        /// </summary>
        public SizeF SzUnit
        {
            get { return _szUnit; }
        }


        /// <summary>
        /// 体温单楣栏项目列表
        /// </summary>
        public string TopItems
        {
            get { return _topItems; }
        }

        private string _titleItems;
        /// <summary>
        /// 体温单标题项目列表
        /// </summary>
        public string TitleItems
        {
            get { return _titleItems; }
        }


        /// <summary>
        /// 手术事件在护理事件中的代码
        /// </summary>
        public string OperationVitalCode
        {
            get { return _operatCode; }
        }


        /// <summary>
        /// 手术后连续显示多少天
        /// </summary>
        public int OperatedDaysShow
        {
            get { return _operateDaysShow; }
        }


        /// <summary>
        /// 显示的最多手术次数
        /// </summary>
        public int OperatedMaxCount
        {
            get { return _operatMaxCount; }
            set { _operatMaxCount = value; }
        }


        /// <summary>
        /// 手术日数格式
        /// </summary>
        public int OperateFmt
        {
            get { return _operatFmt; }
            set { _operatFmt = value; }
        }


        /// <summary>
        /// 主图区画点项目
        /// </summary>
        public string GraphPointItems
        {
            get { return _graphPointItems; }
        }


        /// <summary>
        /// 写体温事件的文本高度
        /// </summary>
        public int TemperEventPos
        {
            get { return _temperEventPos; }
        }


        /// <summary>
        /// 护理事件离体温图最高点的位置
        /// </summary>
        public int NurseEventPos
        {
            get { return _nurseEventPos; }
        }


        /// <summary>
        /// 要输出值的护理事件
        /// </summary>
        public string ValueOutEvent
        {
            get { return _valueOutNurseEvent; }
        }


        /// <summary>
        /// 要输出时间的护理事件代码
        /// </summary>
        public string TimeOutEvent
        {
            get { return _timeOutNurseEvent; }
            set { _timeOutNurseEvent = value; }
        }


        /// <summary>
        /// 体温图上护理事件的过滤条件
        /// </summary>
        public string NursingEventFilter
        {
            get { return _nurseEventFilter; }
        }


        /// <summary>
        /// 作图区文本字体
        /// </summary>
        public Font GraphTextFont
        {
            get { return _fontCenterText; }
        }


        /// <summary>
        /// 体温单尾栏项目
        /// </summary>
        public string TailItems
        {
            get { return _tailItems; }
        }


        /// <summary>
        /// 大便项目的组成
        /// </summary>
        public string StoolComponent
        {
            get { return _stoolComponent; }
        }


        /// <summary>
        /// 要确认的代码
        /// </summary>
        public string VerifyVitalCode
        {
            get { return _verifyVitalCode; }

        }


        /// <summary>
        /// 降多少度要确认
        /// </summary>
        public float VerifyDown
        {
            get { return _verifyDown; }
        }


        /// <summary>
        /// 升多少度要确认
        /// </summary>
        public float VerifyUp
        {
            get { return _verifyUP; }
        }


        /// <summary>
        /// 属性[体温不升符号显示]
        /// </summary>
        public string TempNotAscendSymbol
        {
            get
            {
                return _tempNotAscendSymbol;
            }
            set
            {
                _tempNotAscendSymbol = value;
            }
        }


        /// <summary>
        /// 属性[口表体温显示符号]
        /// </summary>
        public string TempMouseSymbol
        {
            get
            {
                return _tempMouseSymbol;
            }
            set
            {
                _tempMouseSymbol = value;
            }
        }


        /// <summary>
        /// 脉搏短绌代码
        /// </summary>
        public string PulseShort
        {
            get { return _pulseShort; }
        }


        /// <summary>
        /// 体温图打印开始位置
        /// </summary>
        public Point PrintStartPoint
        {
            get { return _printStartPoint; }
        }


        /// <summary>
        /// 打印缩放比例
        /// </summary>
        public float PrintZoom
        {
            get { return _printZoom; }
        }


        /// <summary>
        /// 是否显示合计项为0的
        /// </summary>
        public bool ShowSumZero
        {
            get { return _showSumZero; }
            set { _showSumZero = value; }
        }
        #endregion


        #region 方法
        /// <summary>
        /// 分析参数
        /// </summary>
        public void Parse()
        {
            // 条件检查
            if (_dsParams == null || _dsParams.Tables.Count == 0)
            {
                return;
            }

            string[] parts = null;
            int val = 0;

            foreach (DataRow dr in _dsParams.Tables[0].Rows)
            {
                string paramName = dr["PARAMETER_NAME"].ToString().Trim();
                string paramVal = dr["PARAMETER_VALUE"].ToString().Trim();

                switch (paramName)
                {
                    case "TEMPERATURE_RELOAD":
                        _reloadParams = "TRUE".Equals(paramVal.ToUpper());
                        break;
                    // 标题项目
                    case "TITLE":
                        _titleItems = paramVal;
                        break;

                    case "TEMPERATURE_START_TIME":                      // 体温单开始时间
                        int.TryParse(paramVal, out _startTime);
                        break;

                    case "DAY_START_TIME":                              // 一天开始的时间
                        int.TryParse(paramVal, out _dayStartTime);
                        break;

                    case "GRID_SIZE":                                   // 体温格大小
                        parts = paramVal.Split(ComConst.STR.COMMA.ToCharArray());
                        if (parts.Length > 1)
                        {
                            float valFloat = 0F;

                            if (float.TryParse(parts[0], out valFloat) == true) { _szUnit.Width = valFloat; }
                            if (float.TryParse(parts[1], out valFloat) == true) { _szUnit.Height = valFloat; }
                        }
                        break;

                    case "TEMPERATURE_HEADER_ITEMS":                    // 楣栏项目
                        _topItems = paramVal;
                        break;

                    case "OPERATION_VITAL_CODE":                        // 手术事件代码
                        _operatCode = paramVal;
                        break;

                    case "OPERATION_SHOW_DAYS":                         // 术后显示天数
                        int.TryParse(paramVal, out _operateDaysShow);
                        break;

                    case "OPER_DAYS_FMT":                               // 术后日数显示格式
                        parts = paramVal.Split(ComConst.STR.COMMA.ToCharArray());
                        if (parts.Length > 0)
                        {
                            int.TryParse(parts[0], out _operatMaxCount);
                            int.TryParse(parts[1], out _operatFmt);
                        }
                        break;

                    case "GRAPH_POINT_ITEMS":                           // 主图区画点项目
                        _graphPointItems = paramVal;
                        break;

                    case "GRAPH_POINT_EVENT_POS":                       // 护理事件离图顶的距离
                        int.TryParse(paramVal, out _nurseEventPos);
                        break;

                    case "GRAPH_VALUE_EVENT":                           // 要输出值的护理事件
                        _valueOutNurseEvent = paramVal;
                        break;

                    case "GRAPH_TIME_EVENT":                            // 输出时间的护理事件代码
                        _timeOutNurseEvent = paramVal;
                        break;

                    case "GRAPH_POINT_TEMPER_EVENT_POS":                // 体温事件高度
                        int.TryParse(paramVal, out _temperEventPos);
                        break;

                    case "GRAPH_NURSE_EVENT_FILTER":                    // 体温图上护理事件的过滤条件
                        _nurseEventFilter = paramVal;
                        break;

                    case "GRAPH_TEXT_FONT":                             // 作图区文字的字体
                        parts = paramVal.Split(ComConst.STR.COMMA.ToCharArray());
                        if (parts.Length > 1)
                        {
                            float fontSize = 0F;
                            string fontName = parts[0];

                            if (float.TryParse(parts[1], out fontSize) == true)
                            {
                                _fontCenterText = new Font(fontName, fontSize);
                            }
                        }
                        break;

                    case "TEMPERATURE_TAIL_ITEMS":                      // 尾栏项目
                        _tailItems = paramVal;
                        break;

                    case "ITEM_STOOL_COMPONENT":                        // 大便项目的组成
                        _stoolComponent = paramVal;
                        break;

                    case "TEMPERATURE_PRINT_POS":                       // 体温图打印的位置
                        parts = paramVal.Split(ComConst.STR.COMMA.ToCharArray());
                        if (parts.Length > 1)
                        {
                            if (int.TryParse(parts[0], out val) == true)
                            {
                                _printStartPoint.X = val;
                            }

                            if (int.TryParse(parts[1], out val) == true)
                            {
                                _printStartPoint.Y = val;
                            }
                        }

                        break;
                    case "PRINT_ZOOM":                                  // 打印缩放比例
                        float.TryParse(paramVal, out _printZoom);
                        break;

                    case "SHOW_SUM_ITEM_ZERO":                          // 是否显示合计项目为0
                        _showSumZero = paramVal.Equals("1");
                        break;

                    case "TEMPERATURE_VERIFY":                          // 降温确认参数
                        parts = paramVal.Split(ComConst.STR.COMMA.ToCharArray());
                        if (parts.Length > 2)
                        {
                            _verifyVitalCode = parts[0].Trim();
                            float.TryParse(parts[1].Trim(), out _verifyDown);
                            float.TryParse(parts[2].Trim(), out _verifyUP);
                        }
                        break;

                    case "TEMP_NOT_ASCEND_SYMBOL":
                        _tempNotAscendSymbol = paramVal;
                        break;

                    case "TEMP_MOUSE_SYMBOL":
                        _tempMouseSymbol = paramVal;
                        break;

                    case "PULSE_SHORT":                                 // 脉搏短绌
                        parts = paramVal.Split(ComConst.STR.COMMA.ToCharArray());
                        if (parts.Length > 3)
                        {
                            _pulseShort = paramVal;
                        }
                        break;

                    default:
                        break;
                }
            }
        }


        public bool GetTopItemProperty(string itemName, ref Point ptStart, ref Size szUnitRng,
                                       ref string fontName, ref float fontSize, ref int color)
        {
            // 条件检查
            if (_dsParams == null || _dsParams.Tables.Count == 0)
            {
                return false;
            }

            // 查找
            string filter = "PARAMETER_CLASS = '体温图' AND PARAMETER_NAME_CN = " + SqlManager.SqlConvert(itemName);
            DataRow[] drFind = _dsParams.Tables[0].Select(filter);

            if (drFind.Length == 0)
            {
                return false;
            }

            // 格式: start_pos(x, y), grid_size(width, height),    fontname, fontsize
            // 例子: 20,0,30,20,宋体,10
            string paramVal = drFind[0]["PARAMETER_VALUE"].ToString();
            string[] parts = paramVal.Split(ComConst.STR.COMMA.ToCharArray());
            if (parts.Length < 7)
            {
                return false;
            }

            int i = 0;
            int intVal = 0;

            // 起始位置
            ptStart = new Point();
            if (int.TryParse(parts[i++], out intVal) == true)
            {
                ptStart.X = intVal;
            }
            else
            {
                return false;
            }


            if (int.TryParse(parts[i++], out intVal) == true)
            {
                ptStart.Y = intVal;
            }
            else
            {
                return false;
            }

            // 大小
            szUnitRng = new Size();
            if (int.TryParse(parts[i++], out intVal) == true)
            {
                szUnitRng.Width = intVal;
            }
            else
            {
                return false;
            }

            if (int.TryParse(parts[i++], out intVal) == true)
            {
                szUnitRng.Height = intVal;
            }
            else
            {
                return false;
            }

            // 字体
            fontName = parts[i++];
            if (float.TryParse(parts[i++], out fontSize) == false) { return false; }
            if (int.TryParse(parts[i++], out color) == false) { return false; }

            return true;
        }


        public bool GetTailItemProperty(string itemName, ref int type, ref string condition,
                                        ref Point ptStart, ref Size szUnitRng,
                                        ref string fontName, ref float fontSize, ref int color, ref int dayStart)
        {
            // 条件检查
            if (_dsParams == null || _dsParams.Tables.Count == 0)
            {
                return false;
            }

            // 查找
            string filter = "PARAMETER_CLASS = '体温图' AND PARAMETER_NAME_CN = " + SqlManager.SqlConvert(itemName);
            DataRow[] drFind = _dsParams.Tables[0].Select(filter);

            if (drFind.Length == 0)
            {
                return false;
            }

            // 格式: 类型, 条件, start_pos, grid_size, fontname, fontsize,color
            // 例子: 3, VITAL_CODE = '201', 20,20, 20, 20, 宋体, 8
            string paramVal = drFind[0]["PARAMETER_VALUE"].ToString();
            string[] parts = paramVal.Split(ComConst.STR.COMMA.ToCharArray());
            if (parts.Length < 9)
            {
                return false;
            }

            int i = 0;
            int intVal = 0;

            // 类型
            if (int.TryParse(parts[i++], out type) == false)
            {
                return false;
            }

            // 条件
            condition = parts[i++].Trim();

            // 起始位置
            ptStart = new Point();
            if (int.TryParse(parts[i++], out intVal) == true)
            {
                ptStart.X = intVal;
            }
            else
            {
                return false;
            }


            if (int.TryParse(parts[i++], out intVal) == true)
            {
                ptStart.Y = intVal;
            }
            else
            {
                return false;
            }

            // 大小
            szUnitRng = new Size();
            if (int.TryParse(parts[i++], out intVal) == true)
            {
                szUnitRng.Width = intVal;
            }
            else
            {
                return false;
            }

            if (int.TryParse(parts[i++], out intVal) == true)
            {
                szUnitRng.Height = intVal;
            }
            else
            {
                return false;
            }

            // 字体
            fontName = parts[i++];
            if (float.TryParse(parts[i++], out fontSize) == false) { return false; }
            if (int.TryParse(parts[i++], out color) == false) { return false; }

            // 开始时间
            if (i < parts.Length)
            {
                int.TryParse(parts[i++], out dayStart);
            }

            return true;
        }


        public bool GetGraphItemProperty(string itemName, ref int type, ref float valStart,
                                        ref float valStep, ref string condition, ref int color)
        {
            // 条件检查
            if (_dsParams == null || _dsParams.Tables.Count == 0)
            {
                return false;
            }

            // 查找
            string filter = "PARAMETER_CLASS = '体温图' AND PARAMETER_NAME_CN = " + SqlManager.SqlConvert(itemName);
            DataRow[] drFind = _dsParams.Tables[0].Select(filter);

            if (drFind.Length == 0)
            {
                return false;
            }

            // 格式: 类型,最低值,步值,条件
            // 例子: 1,33,0.2,VITAL_CODE='1004'
            string paramVal = drFind[0]["PARAMETER_VALUE"].ToString();
            string[] parts = paramVal.Split(ComConst.STR.COMMA.ToCharArray());
            if (parts.Length < 5)
            {
                return false;
            }

            int i = 0;
            if (int.TryParse(parts[i++], out type) == false)
            {
                return false;
            }

            if (float.TryParse(parts[i++], out valStart) == false)
            {
                return false;
            }

            if (float.TryParse(parts[i++], out valStep) == false)
            {
                return false;
            }

            condition = parts[i++];

            if (condition.Trim().Length == 0)
            {
                return false;
            }

            if (int.TryParse(parts[i++], out color) == false)
            {
                return false;
            }

            return true;
        }


        public bool GetDownTemperProperty(ref string condition, ref string itemCode,
                                        ref float valStart, ref float valStep)
        {
            // 条件检查
            if (_dsParams == null || _dsParams.Tables.Count == 0)
            {
                return false;
            }

            // 查找
            string filter = "PARAMETER_CLASS = '体温图' AND PARAMETER_NAME = 'GRAPH_POINT_DOWN_TEMPER'";
            DataRow[] drFind = _dsParams.Tables[0].Select(filter);

            if (drFind.Length == 0)
            {
                return false;
            }

            // 格式: 参照项目代码,项目代码,最低值,步值
            string paramVal = drFind[0]["PARAMETER_VALUE"].ToString();
            string[] parts = paramVal.Split(ComConst.STR.COMMA.ToCharArray());
            if (parts.Length < 4)
            {
                return false;
            }

            int i = 0;
            condition = parts[i++].Trim();
            itemCode = parts[i++].Trim();

            if (float.TryParse(parts[i++], out valStart) == false)
            {
                return false;
            }

            if (float.TryParse(parts[i++], out valStep) == false)
            {
                return false;
            }

            if (condition.Length == 0 || itemCode.Length == 0)
            {
                return false;
            }

            return true;
        }
        #endregion


        #region 静态方法
        /// <summary>
        /// 获取画刷
        /// </summary>
        /// <param name="color">画刷代码: 0:黑, 1:红 2: 蓝</param>
        /// <returns></returns>
        public static Brush GetBrushFromColor(int color)
        {
            switch (color)
            {
                case 1:
                    return Brushes.Red;
                case 2:
                    return Brushes.Blue;
                default:
                    return Brushes.Black;
            }
        }


        /// <summary>
        /// 获取画笔
        /// </summary>
        /// <param name="color">画笔代码: 0:黑, 1:红 2: 蓝</param>
        /// <returns></returns>
        public static Pen GetPenFromColor(int color)
        {
            switch (color)
            {
                case 1:
                    return Pens.Red;
                case 2:
                    return Pens.Blue;
                default:
                    return Pens.Black;
            }
        }


        /// <summary>
        /// 获取体温图上尾部项目类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TailItemType GetTailItemType(int type)
        {
            if (0 <= type && type <= 4)
            {
                return (TailItemType)type;
            }
            else
            {
                return TailItemType.ONE;
            }
        }


        /// <summary>
        /// 获取体温图上正图项目类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static GraphPointType GetGraphPointType(int type)
        {
            if (0 <= type && type <= 5)
            {
                return (GraphPointType)type;
            }
            else
            {
                return GraphPointType.ARMPIT;
            }
        }
        #endregion
    }
}
