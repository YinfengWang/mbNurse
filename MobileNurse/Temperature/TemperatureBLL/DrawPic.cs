using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using CommonEntity;
using HISPlus;
using TemperatureDAL;

namespace TemperatureBLL
{
    public class DrawPic
    {
        #region 变量

        public readonly BodyTemper BodyTemper = new BodyTemper(); // 体温图
        public readonly BodyTemperatureCom Com = new BodyTemperatureCom();
        private readonly ArrayList _arrBreakCols = new ArrayList(); // 断开点

        /// <summary>
        ///     图片模板路径
        /// </summary>
        private readonly string _imageTemplate;

        /// <summary>
        ///     所有信息分两部分.
        ///     只画固定部分或者只画数据
        /// [true]表示画固定部分,[false]表示画数据
        /// </summary>
        private readonly bool _isDrawFix = true;

        private readonly ITemperatureDal _temperatureDal;
        public ImageFormat ImageFormat = ImageFormat.Png;

        /// <summary>
        ///     住院日期及时间
        /// </summary>
        private DateTime _admissionDate;

        public Bitmap _bmp;

        /// <summary>
        ///     填充图形形状的内部对象.黑色
        /// </summary>
        private Brush _brushBlack = new SolidBrush(Color.Black);

        /// <summary>
        ///     填充图形形状的内部对象.蓝色
        /// </summary>
        private Brush _brushBlue = new SolidBrush(Color.Blue);

        /// <summary>
        ///     填充图形形状的内部对象.红色
        /// </summary>
        private Brush _brushRed = new SolidBrush(Color.Red);

        /// <summary>
        ///     体温单显示的结束日期
        /// </summary>
        private DateTime _endDate;

        /// <summary>
        ///     GDI绘图图面
        /// </summary>
        private Graphics _gc;

        /// <summary>
        ///     绘制特殊值集合
        /// </summary>
        private List<DrawSpec> _list;

        /// <summary>
        ///     矩形的位置和大小
        /// </summary>
        private RectangleF _myRa;

        /// <summary>
        /// 病人信息
        /// </summary>
        private DataSet _patientInfo;

        /// <summary>
        ///     病人生命体征数据
        /// </summary>
        private DataTable _patientVitals;

        /// <summary>
        ///     绘制列数.[1-7]
        /// </summary>
        private int _showDays;

        /// <summary>
        ///     体温单显示的起始日期
        /// </summary>
        private DateTime _startDate;

        private Shows Shows
        {
            get { return BodyTemper.Params.Shows; }
        }

        #endregion 变量

        #region 画笔

        /// <summary>
        ///     画笔颜色:黑色.宽度:2.5
        /// </summary>
        private readonly Pen _boldBPen = new Pen(Color.Black, 2.5f);

        /// <summary>
        ///     画笔颜色:蓝色.宽度:2.5
        /// </summary>
        private readonly Pen _boldBluePen = new Pen(Color.Blue, 2.5f);

        /// <summary>
        ///     画笔颜色:红色.宽度:2.5
        /// </summary>
        private readonly Pen _boldRedDashPen = new Pen(Color.Red, 2.5f);

        /// <summary>
        ///     画笔颜色:红色.宽度:2.5
        /// </summary>
        private readonly Pen _boldRedPen = new Pen(Color.Red, 2.5f);

        /// <summary>
        ///     画笔颜色:红色.宽度:1.5
        /// </summary>
        private readonly Pen _redPen = new Pen(Color.Red, 1.5f);

        /// <summary>
        ///     画笔颜色:黑色.宽度:1.5
        /// </summary>
        private readonly Pen _thinBPen = new Pen(Color.Black, 1.5f);

        /// <summary>
        ///     画笔颜色:暗灰色.宽度:1.5
        /// </summary>
        private readonly Pen _thinBPen1 = new Pen(Color.DimGray, 1.5f);

        #endregion 画笔

        #region 字体

        /// <summary>
        ///     宋体.大小:12
        /// </summary>
        private readonly Font _fontChn12 = new Font("宋体", 12f, FontStyle.Bold);

        /// <summary>
        ///     宋体.大小:14
        /// </summary>
        private readonly Font _fontChn14 = new Font("宋体", 14f, FontStyle.Bold);

        /// <summary>
        ///     Times New Roman.大小:7
        /// </summary>
        private readonly Font _fontEng7 = new Font("Times New Roman", 7f);

        /// <summary>
        ///     Times New Roman.大小:9
        /// </summary>
        private readonly Font _fontEng9 = new Font("Times New Roman", 9f);

        /// <summary>
        ///     宋体.大小:10
        /// </summary>
        private readonly Font _fontChn10 = new Font("宋体", 10f, FontStyle.Bold);

        /// <summary>
        ///     宋体.大小:8.加粗
        /// </summary>
        private readonly Font _fontChn8 = new Font("宋体", 8f, FontStyle.Bold);

        /// <summary>
        ///     宋体.大小:9
        /// </summary>
        private readonly Font _fontChn9 = new Font("宋体", 9f);

        /// <summary>
        ///     宋体.大小:16
        /// </summary>
        private readonly Font _fontChnH16 = new Font("宋体", 16f, FontStyle.Bold);

        #endregion 字体

        #region 边框

        /// <summary>
        ///     最底部行集单元格高度
        /// </summary>
        private int _bottomRowHeight;

        /// <summary>
        ///     总宽度.x轴结束点
        /// </summary>
        private float _endX;

        /// <summary>
        ///     总高度.y轴结束点
        /// </summary>
        private float _endY;

        /// <summary>
        ///     左边框+左边第一列的宽度
        /// </summary>
        private float _left;

        /// <summary>
        ///     第一列单元格宽度(住院天数,日期,时间的列)
        /// </summary>
        private float _leftWidth;

        /// <summary>
        ///     体温单中部每天的单元格宽度
        /// </summary>
        private float _middleDayWidth;

        /// <summary>
        ///     体温单中部每4小时的单元格宽度.值为 每天的单元格宽度/6
        /// </summary>
        private float _middleHourWidth;

        /// <summary>
        ///     最中部43行单元格的单元格高度
        /// </summary>
        private float _middleRowHeight;

        /// <summary>
        ///     图片高度
        /// </summary>
        private int _picHeight;

        /// <summary>
        ///     图片宽度
        /// </summary>
        private int _picWidth;

        /// <summary>
        ///     最后一列单元格宽度(标识心率,脉搏等说明的列)
        /// </summary>
        private int _rightWidth;

        /// <summary>
        ///     X轴起始坐标.等于左边框
        /// </summary>
        private int _startX;

        /// <summary>
        ///     上边框+Head标题+前几行(住院天数,日期,时间)
        /// </summary>
        private int _top;

        /// <summary>
        ///     标题下的行数
        /// </summary>
        private const int TopRowCount = 4;

        /// <summary>
        ///     标题下的单元格高度
        /// </summary>
        private int _topRowHeight;

        /// <summary>
        ///     头部高度(标题)
        /// </summary>
        private int _headHeight;

        /*
                /// <summary>
                ///     Y轴起始坐标.等于上边框+头部标题高度
                /// </summary>
                private int _startY;
        */

        /// <summary>
        ///     左边框
        /// </summary>
        private int _leftMargin;

        /// <summary>
        ///     右边框
        /// </summary>
        private int _rightMargin;

        /// <summary>
        ///     上边框
        /// </summary>
        private int _topMargin;

        /// <summary>
        ///     总页数
        /// </summary>
        private int _totalPage;

        #endregion 边框

        #region 常量

        /// <summary>
        ///     体温单显示7天的信息
        /// </summary>
        private const int ShowDaysColumns = 7;

        /// <summary>
        ///     在最中间的部分,总共有43行,即由TEMPERATURE_MIN到TEMPERATURE_MAX的8个刻度差*5再加上3.
        /// </summary>
        private const int ShowAxisRows = 43;

        /// <summary>
        ///     起始时间刻度
        /// </summary>
        private const int HourStart = 0;

        /// <summary>
        ///     时间刻度间隔
        /// </summary>
        private const int HourInterval = 4;

        /// <summary>
        ///     时间刻度总数
        /// </summary>
        private const int HourColumnCount = 6;

        /// <summary>
        ///     最高体温刻度
        /// </summary>
        private const int TemperatureMax = 42;

        /// <summary>
        ///     显示的最低体温刻度
        /// </summary>
        private const int TemperatureMinShow = 35;

        /// <summary>
        ///     最底部自定义行数.现为14
        ///     长庆油田修改为11
        /// </summary>
        private const int ShowBottonRows = 11;

        /// <summary>
        ///     图标: 实心圆。默认。红色。
        /// </summary>
        private const string IconCircleSolid = "●";

        /// <summary>
        ///     图标: 实心圆。蓝色
        /// </summary>
        private const string IconCircleSolidBlue = "●1";

        /// <summary>
        ///     图标: 空心圆
        /// </summary>
        private const string IconCircleHollow = "○";

        /// <summary>
        ///     图标: 叉
        /// </summary>
        private const string IconPick = "Х";
        private const string PhysicalCoolT = "RedХ";

        /// <summary>
        ///     图标: 方框
        /// </summary>
        private const string IconBox = "□";

        /// <summary>
        /// 图标：呼吸机
        /// </summary>
        private const string IconBreathMachine = "®";

        /// <summary>
        ///     文本布局: 水平居中对齐,垂直居中对齐
        /// </summary>
        private readonly StringFormat _formatCenterCenter = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        /// <summary>
        ///     文本布局: 水平居中对齐,垂直靠近布局
        /// </summary>
        private readonly StringFormat _formatCenterLeft = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Near
        };

        /// <summary>
        ///     文本布局: 水平靠近布局,垂直居中对齐
        /// </summary>
        private readonly StringFormat _formatLeftCenter = new StringFormat
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };

        /// <summary>
        ///     体温单特殊值XML路径
        /// </summary>
        public string SpecXmlPath = @"Template\XML\T.xml";

        //= HttpContext.Current.Server.MapPath(Common.Static.GetWebConfigValueWLog("physicalSpec"));

        #endregion 常量

        #region 属性

        /// <summary>
        ///     病人编号
        /// </summary>
        private readonly string _patientId;

        /// <summary>
        ///     住院次数
        /// </summary>
        private readonly int _visitId;

        /// <summary>
        ///     页面索引.显示第几页
        /// </summary>
        private int _pageIndex = -1;

        /// <summary>
        ///     当前Http请求。
        /// </summary>
        private readonly HttpContext _context = HttpContext.Current;

        private DateTime _measureDateEnd;
        private DateTime _measureDateStart;

        ///// <summary>
        ///// 数据库连接字符串
        ///// </summary>
        //public string ConnectionString { get; set; }

        /// <summary>
        ///     图片保存路径。完整路径。
        /// </summary>
        private string SavePath { get; set; }

        /// <summary>
        ///     医院名称
        /// </summary>
        private string HospitalName { get; set; }

        /// <summary>
        ///     头部高度(标题)
        /// </summary>
        private int HeadHeight
        {
            get { return _headHeight; }
            set { _headHeight = value; }
        }

        /// <summary>
        ///     Y轴起始坐标.等于上边框+头部标题高度
        /// </summary>
        private int StartY
        {
            get { return _topMargin + HeadHeight; }
        }

        /// <summary>
        ///     属性[测量开始时间]
        /// </summary>
        public DateTime MeasureDateStart
        {
            get { return _measureDateStart; }
        }


        /// <summary>
        ///     属性[测量结束时间]
        /// </summary>
        public DateTime MeasureDateEnd
        {
            get { return _measureDateEnd; }
        }

        #endregion 属性
        /// <summary>
        /// 用于存储脉搏、心率的坐标
        /// </summary>
        private ArrayList arrayMB = new ArrayList(), arrayXL = new ArrayList(), arrayTW = new ArrayList();
        #region 实例化

        /// <summary>
        ///     实例化
        /// </summary>
        /// <param name="patientId">病人ID</param>
        /// <param name="visitId">住院编号</param>
        /// <param name="hospitalName"></param>
        /// <param name="savePath">在Web Service中，仅指定虚拟目录即可,在Winform中指定</param>
        public DrawPic(string patientId, int visitId, string hospitalName, string savePath)
        {
            this._patientId = patientId;
            this._visitId = visitId;
            arrayMB = new ArrayList();
            arrayXL = new ArrayList();
            arrayTW = new ArrayList();
            _temperatureDal = new TemperatureMobileDal(GVars.OracleAccess);

            //图片保存路径
            SavePath = savePath;
            //医院名称
            HospitalName = hospitalName;

            if (string.IsNullOrEmpty(SavePath))
                throw new Exception("请设置图片保存路径SavePath属性值.");

            if (string.IsNullOrEmpty(HospitalName))
                throw new Exception("请设置医院名称HospitolName属性值.");

            // 初始化
            Init();

            BodyTemper.Params.SzUnit = new SizeF(_middleHourWidth, _middleRowHeight);

            //取模板文件
            string fileName = DateTime.Now.ToString(ComConst.FMT_DATE.SHORT_COMPACT) + "." + ImageFormat;
            // string fileName = patientId + visitId.ToString("_00") + pageIndex.ToString("_00") + ".jpg";

            string tmpPath = SavePath;
            if (!SavePath.Contains(":") && _context != null)
                tmpPath = _context.Server.MapPath(SavePath);
            if (!Directory.Exists(tmpPath))
            {
                Directory.CreateDirectory(tmpPath);
            }
            string strpath = Path.Combine(tmpPath, fileName);

            _imageTemplate = strpath;
            if (File.Exists(strpath))
            {
                _isDrawFix = false;
            }
            if (_isDrawFix)
            {
                Reload(-1);
                _isDrawFix = false;
            }
            //Reload(pageIndex);
        }

        /// <summary>
        ///     实例化
        /// </summary>
        /// <param name="patientId">病人ID</param>
        /// <param name="visitId">住院编号</param>
        /// <param name="hospitalName"></param>
        /// <param name="savePath">在Web Service中，仅指定虚拟目录即可,在Winform中指定</param>
        public DrawPic(string patientId, int visitId, string hospitalName, string savePath, DbAccess oracleAccess)
        {
            this._patientId = patientId;
            this._visitId = visitId;
            GVars.Patient.ID = patientId;
            GVars.Patient.VisitId = visitId.ToString();
            arrayMB = new ArrayList();
            arrayXL = new ArrayList();
            arrayTW = new ArrayList();
            _temperatureDal = new TemperatureMobileDal(oracleAccess);

            //图片保存路径
            SavePath = savePath;
            //医院名称
            HospitalName = hospitalName;

            if (string.IsNullOrEmpty(SavePath))
                throw new Exception("请设置图片保存路径SavePath属性值.");

            if (string.IsNullOrEmpty(HospitalName))
                throw new Exception("请设置医院名称HospitolName属性值.");

            // 初始化
            Init();

            BodyTemper.Params.SzUnit = new SizeF(_middleHourWidth, _middleRowHeight);

            //取模板文件
            string fileName = DateTime.Now.ToString(ComConst.FMT_DATE.SHORT_COMPACT) + "." + ImageFormat;
            // string fileName = patientId + visitId.ToString("_00") + pageIndex.ToString("_00") + ".jpg";

            string tmpPath = SavePath;
            if (!SavePath.Contains(":") && _context != null)
                tmpPath = _context.Server.MapPath(SavePath);
            if (!Directory.Exists(tmpPath))
            {
                Directory.CreateDirectory(tmpPath);
            }
            string strpath = Path.Combine(tmpPath, fileName);

            _imageTemplate = strpath;
            if (File.Exists(strpath))
            {
                _isDrawFix = false;
            }
            if (_isDrawFix)
            {
                Reload(-1);
                _isDrawFix = false;
            }
            //Reload(pageIndex);
        }

        public void Reload(int pageIndex)
        {
            if (!_isDrawFix && _pageIndex == pageIndex) return;

            if (!_isDrawFix)
                _pageIndex = pageIndex;

            // 开始
            _bmp = DrawImageStart();

            // 绘制Title标题
            DrawTitle();

            if (_isDrawFix)
                // 绘制边框
                DrawBorder();
            else
                InitPatient();

            // 绘制头部
            PaintTop();
            // 绘制左边
            PaintLeft();

            if (_isDrawFix)
            {
                PaintCenter();
            }
            else
            {
                PaintCenterData();
            }

            // 绘制中心
            PaintCenter();

            //// 绘制右边
            //this.PaintRight();

            // 绘制底部
            PaintBottom();

            if (_isDrawFix)
            {
                if (File.Exists(_imageTemplate))
                {
                    return;
                }
                _bmp.Save(_imageTemplate, ImageFormat);
            }
        }
        #endregion

        /// <summary>
        ///     获取图片路径. 分辨率为1024*768
        /// </summary>
        /// <returns>体温单URL</returns>
        public string GetImage(int pageIndex)
        {
            arrayMB = new ArrayList();
            arrayXL = new ArrayList();
            arrayTW = new ArrayList();
            Reload(pageIndex);
            // 结束
            return DrawImageEnd(_bmp, false);
        }

        /// <summary>
        ///     获取图片路径. 高清.
        /// </summary>
        /// <returns>体温单URL</returns>
        public string GetClearImage(int pageIndex)
        {
            Reload(pageIndex);
            // 结束
            return DrawImageEnd(_bmp, true);
        }

        #region 初始化

        /// <summary>
        ///     初始化
        /// </summary>
        private void Init()
        {
            // 获取AppConfig表参数
            var objParams = new BodyTemperParams { Parameters = Com.GetParameters() };

            //
            objParams.Parse();

            // 共通
            Com.Params = objParams;

            // 体温单
            BodyTemper.Params = objParams;

            if (File.Exists(SpecXmlPath))
            {
                DataTable dt = XmlToDataTable(SpecXmlPath);

                _list = ConvertToList<DrawSpec>(dt);
            }

            _picWidth = _A(210);
            _picHeight = _A(297);
            _leftMargin = _A(9);//9
            _topMargin = _A(21);//_(1)======================================
            _rightMargin = _A(0);
            _A(1);
            HeadHeight = _A(21);//21 方框

            _topRowHeight = _A(5);
            _leftWidth = _A(22);
            _middleDayWidth = _A(24);
            _middleHourWidth = _middleDayWidth / 6;
            _rightWidth = _A(0);
            _middleRowHeight = _A(4);//4
            _bottomRowHeight = _A(5);//5

            _boldRedDashPen.DashStyle = DashStyle.Dash;


            //画刷
            _brushBlack = new SolidBrush(Color.Black);
            _brushRed = new SolidBrush(Color.Red);
            _brushBlue = new SolidBrush(Color.Blue);

            _startX = _leftMargin;
            _left = _leftMargin + _leftWidth;
            _endX = ((_leftMargin + _leftWidth) + (ShowDaysColumns * _middleDayWidth)) + _rightWidth;

            _top = StartY + _topRowHeight * TopRowCount;

            _endY = (_top +
                    (ShowAxisRows * _middleRowHeight)) + (ShowBottonRows * _bottomRowHeight);

            Com.GetVitalSignsRecDate(_patientId, _visitId, ref _measureDateStart, ref _measureDateEnd);

            // 病人信息
            _patientInfo = _temperatureDal.GetPatientInfo(_patientId, _visitId);


            if (_patientInfo != null && _patientInfo.Tables.Count > 0 && _patientInfo.Tables[0].Rows.Count > 0)
            {
                //住院日期及时间
                _admissionDate = Convert.ToDateTime(_patientInfo.Tables[0].Rows[0]["ADMISSION_DATE_TIME"]);
            }


            TimeSpan daySpan = MeasureDateEnd.AddDays(1).Date - _admissionDate.Date;

            // 体温单总页面数
            _totalPage = (int)Math.Ceiling((double)daySpan.Days / ShowDaysColumns);

            if (_totalPage < 1)//2016.03.09
                _totalPage = 1;
        }


        /// <summary>
        /// 返回体温图总周数
        /// </summary>
        /// <returns></returns>
        public int GetWeeks()
        {
            return _totalPage;
        }


        private void InitPatient()
        {
            TimeSpan daySpan = MeasureDateEnd.AddDays(1).Date - _admissionDate.Date;
            int daySum = daySpan.Days;

            if (_pageIndex < _totalPage)
                _showDays = ShowDaysColumns;
            else
            {
                _pageIndex = _totalPage;
                _showDays = daySum % ShowDaysColumns > 0 ? daySum % ShowDaysColumns : ShowDaysColumns;
            }

            // 体温单开始和结束时间
            _startDate = _admissionDate.AddDays((_pageIndex - 1) * ShowDaysColumns).Date;

            //_endDate = _admissionDate.AddDays((double)((_pageIndex - 1) * ShowDaysColumns) + _showDays - 1).Date;
            //由于_temperatureDal.GetPatientVitalSignal1条件为<结束时间，造成_endDate不包括最后数据的一天
            _endDate = _admissionDate.AddDays((double)((_pageIndex - 1) * ShowDaysColumns) + _showDays).Date;

            //throw new Exception("无效页面参数!体温单总页面数为:" + totalPage + ".");

            // 病人体征信息
            _patientVitals = _temperatureDal.GetPatientVitalSignal1(_patientId, _visitId, _startDate.AddHours(HourStart),
                _endDate.AddHours(HourStart));

            BodyTemper.DsNursing = _patientVitals.DataSet;

            BodyTemper.ParsePara();
        }

        #endregion

        #region 绘制

        #region 绘制开始

        /// <summary>
        ///     绘制体温单--开始
        /// </summary>
        /// <returns></returns>
        private Bitmap DrawImageStart()
        {
            Bitmap bmp;
            if (_isDrawFix)
            {
                bmp = new Bitmap(_picWidth, _picHeight + 10, PixelFormat.Format32bppPArgb);
                bmp.SetResolution(200f, 200f);
            }
            else
            {
                bmp = new Bitmap(_imageTemplate);
            }

            //bmp.SetResolution(1024f, 768f);
            //bmp.SetResolution(1024f, 1586f);
            //bmp.SetResolution(300f, 300f);
            _gc = Graphics.FromImage(bmp);
            if (_isDrawFix)
                _gc.Clear(Color.White);
            _gc.CompositingQuality = CompositingQuality.HighQuality;
            _gc.SmoothingMode = SmoothingMode.HighQuality;
            _gc.InterpolationMode = InterpolationMode.HighQualityBicubic;

            return bmp;
        }

        #endregion

        #region 绘制标题

        /// <summary>
        ///     绘制体温单Title标题
        /// </summary>
        private void DrawTitle()
        {
            //bodyTemper
            BodyTemperParams bodyTemperParams = BodyTemper.Params;

            // 获取楣栏的项目
            string topItems = bodyTemperParams.TitleItems;
            string[] items = topItems.Split(ComConst.STR.COMMA.ToCharArray());

            PaintMiddleCenter(HospitalName, _A(1), _A(40), _fontChn14, _brushBlack);//7 顶部高

            PaintMiddleCenter("体 温 单", _A(7), _A(42), _fontChnH16, _brushBlack); //9

            // 基本信息总行数为1行. 如果为2行或3行,则再加上行高.
            int rowCount = 1;
            float height = _A(6);

            //间隔
            float columnInterval = _A(3);//1

            foreach (string itemName in items)
            {
                // 获取项目属性
                float ptStart = 0;
                int width = 0;
                string itemValue = string.Empty;
                int rowNumber = 0;

                //格式：
                //列名,X轴坐标(以百分比设置范围是0-100),宽度,第几行(最小为1,默认为1)
                //示例：
                //DEPT_NAME,0,30,1
                if (bodyTemperParams.GetTopItemProperty(itemName, ref itemValue, ref ptStart, ref width,
                    ref rowNumber) == false)
                {
                    continue;
                }

                if (rowNumber > rowCount)
                {
                    _headHeight = _headHeight + (rowNumber - rowCount) * (int)height;
                    rowCount = rowNumber;
                }

                ptStart = _endX * ptStart / 82;

                float tempX = _leftMargin + ptStart + _A(Shows.Font.Size) *
                              Regex.Replace(itemName, "[^\x00-\xff]", "zz").Length
                              * 0.00018f * _endX;
                if (_isDrawFix)
                {
                    // 姓名等小标题
                    _myRa = new RectangleF(_leftMargin + ptStart,
                        _topMargin + (float)_A(8) + height * rowNumber,
                        _A(46), height);
                    _gc.DrawString(itemName, Shows.Font, Shows.Brush, _myRa,
                        _formatLeftCenter);

                    // 画下划线
                    //if (!string.IsNullOrEmpty(itemName))
                    //    DrawLine_Horizontal(_thinBPen, tempX,
                    //        (tempX + _A(width)) > _endX ? _endX : (tempX + _A(width)),
                    //        _topMargin + (float)_A(6) + height * (rowNumber + 1) + columnInterval);
                }
                else
                {
                    // 病人基本信息
                    _myRa = new RectangleF(tempX,
                        _topMargin + (float)_A(8) + height * rowNumber, _A(46), height);
                    _gc.DrawString(itemValue, Shows.Font, Shows.Brush, _myRa,
                        _formatLeftCenter);
                }
            }
        }

        #endregion

        #region 绘制边框线

        /// <summary>
        ///     绘制边框线
        /// </summary>
        private void DrawBorder()
        {
            // 画四个边框
            DrawLine_Horizontal(_boldBPen, _startX, _endX, StartY);

            DrawLine_Horizontal(_boldBPen, _startX, _endX, _endY);

            DrawLine_Vertical(_boldBPen, _startX, StartY, _endY);

            DrawLine_Vertical(_boldBPen, _endX, StartY, _endY);
        }

        #endregion

        /// <summary>
        ///     绘制左边
        /// </summary>
        private void PaintLeft()
        {
            if (!_isDrawFix) return;

            #region 脉搏和体温中间的竖线

            // 画脉搏和体温中间的竖线
            DrawLine_Vertical(_thinBPen1, _startX + (_leftWidth / 2), _top, _top + (ShowAxisRows * _middleRowHeight));

            #endregion

            #region 刻度数据

            const int offset = 6;

            // 第一个脉搏
            int firstPulse = 180;
            // 相邻脉搏刻度差
            const int pulseDiff = -20;

            // 第一个体温
            int temperature = TemperatureMax;
            // 相邻体温刻度差
            const int tempDiff = -1;

            // 体温单位
            const string tempUnit = "\x00b0";

            // 第一个刻度的位置(在几个单元格后)
            int firstLocation = 2;

            while (temperature >= TemperatureMinShow)
            {
                _myRa = new RectangleF(_startX, (_top + (firstLocation * _middleRowHeight)) + offset, _leftWidth,
                    2 * _middleRowHeight);
                _gc.DrawString(
                    firstPulse.ToString().PadRight(10) + temperature + tempUnit
                    , _fontEng9, _brushBlack, _myRa, _formatCenterLeft);

                temperature += tempDiff;
                firstPulse += pulseDiff;
                //if (temperature == TEMPERATURE_MIN)
                //    FirstLocation += 4;
                //else
                //    FirstLocation += 5;

                firstLocation += 5;
            }

            string[] showRemark =
            {
                "脉搏", "体温"
            };

            for (int tempRemarkIndex = 0; tempRemarkIndex < showRemark.Length; tempRemarkIndex++)
            {
                _myRa = new RectangleF(_startX + (_leftWidth / 2) * tempRemarkIndex,
                    _top + _middleRowHeight / 2,
                    _leftWidth / 2, _middleRowHeight);
                _gc.DrawString(showRemark[tempRemarkIndex], _fontChn8,
                    _brushBlack, _myRa, _formatCenterLeft);
            }

            string[] showRemark2 =
            {
                "●", "Х"
            };

            for (int tempRemarkIndex = 0; tempRemarkIndex < showRemark.Length; tempRemarkIndex++)
            {
                _myRa = new RectangleF(_startX + (_leftWidth / 2) * tempRemarkIndex,
                    _top + _middleRowHeight + _middleRowHeight / 2,
                    (_leftWidth / 2), _middleRowHeight);
                _gc.DrawString(showRemark2[tempRemarkIndex], _fontChn8,
                    tempRemarkIndex == 0 ? _brushRed : _brushBlue, _myRa, _formatCenterLeft);
            }

            #endregion 刻度数据
        }

        /// <summary>
        ///     绘制呼吸.[在右下角位置]
        /// </summary>
        private void PaintBreath()
        {
            #region 刻度数据

            string[] showRemark =
            {
                "呼", "吸", "●"
            };

            for (int tempRemarkIndex = 0; tempRemarkIndex < showRemark.Length; tempRemarkIndex++)
            {
                _myRa = new RectangleF(_endX,
                    (float)(_top + _middleRowHeight * (5 * 5 - 0.5 + tempRemarkIndex)),
                    _leftWidth / 2 - 15, _middleRowHeight);
                if (tempRemarkIndex == showRemark.Length - 1)
                {
                    _gc.DrawString(showRemark[tempRemarkIndex], _fontChn8,
                        _brushBlue, _myRa, _formatCenterLeft);
                    continue;
                }
                _gc.DrawString(showRemark[tempRemarkIndex], _fontChn8,
                    _brushBlack, _myRa, _formatCenterLeft);
            }

            // 相邻刻度差
            const int pulseDiff = -10;

            // 第一个体温
            int temperature = 40;

            // 第一个刻度的位置(在几个单元格后)
            int firstLocation = 5 * 5 + 2;

            while (temperature >= 10)
            {
                _myRa = new RectangleF(_endX, _top + (int)((firstLocation + 0.5) * _middleRowHeight),
                    _leftWidth / 2 - 15, _middleRowHeight);
                _gc.DrawString(
                    temperature.ToString()
                    , _fontEng9, _brushBlack, _myRa, _formatCenterLeft);

                temperature += pulseDiff;

                firstLocation += 5;
            }

            #endregion 刻度数据
        }

        /// <summary>
        ///     绘制中心
        /// </summary>
        private void PaintCenter()
        {
            #region 表格线

            // y起点.为上边框+头部标题+最上面的三行(住院天数,日期,时间行)-第三行的时间
            float starty = _top - _topRowHeight;

            // y坐标终点到体温刻度最底部再加上呼吸行
            float endy = _top + (ShowAxisRows * _middleRowHeight); // + this.BottomRowHeight;

            // 画竖线
            for (int i = 0, j = 1; i <= ShowDaysColumns; i++, j = 1)
            {
                if (i != ShowDaysColumns && i != 0)
                {
                    DrawLine_Vertical(_boldRedPen, _startX + _leftWidth + _middleDayWidth * i,
                        StartY, _endY);
                }
                else
                {
                    DrawLine_Vertical(_boldBPen, _startX + _leftWidth + _middleDayWidth * i,
                        StartY, _endY);
                }

                while (j < 6 && i < ShowDaysColumns)
                {
                    float tempStartx = _startX + _leftWidth + (_middleDayWidth * i) +
                                       (_middleHourWidth * j);

                    DrawLine_Vertical(_thinBPen1, tempStartx, starty, endy);

                    j++;
                }
            }

            // 体温刻度右边的竖线
            float startx = _startX + _leftWidth;
            float endx = _endX - _rightWidth;

            starty = _top;

            // 画10*5的横线(第一行,仅三小行)
            for (int i = 0; i < TemperatureMax - TemperatureMinShow + 2; i++)
            {
                starty += _middleRowHeight;

                if (i > 0)
                    DrawLine_Horizontal(i != 6 ? _boldBPen : _boldRedPen, startx, endx, starty);
                else
                    DrawLine_Horizontal(_thinBPen1, startx, endx, starty);

                // 42刻度上(第一行)仅有两小行
                for (int j = 0; j < (i == 0 ? 1 : 4); j++)
                {
                    starty += _middleRowHeight;
                    DrawLine_Horizontal(_thinBPen1, startx, endx, starty);
                }
            }

            #endregion
        }

        private void PaintCenterData()
        {
            // 获取主图的项目(liubo20110107 此处应为或得画点的项目，从数据库中读取的)

            //APPCONFIG - GRAPH_POINT_ITEMS字段
            string topItems = BodyTemper.Params.GraphPointItems;
            string[] items = topItems.Split(ComConst.STR.COMMA.ToCharArray());

            // 脉搏短绌
            //if (BodyTemper.Params.PulseShort.Length > 0)
            //{
            //    DrawPulseShort(new Size(_picWidth, _picHeight));
            //}

            float valStart = 0F;
            float valStep = 1F;
            string condition = string.Empty;

            float valStartT = 0F; // 体温的开始值
            float valStepT = 1F; // 体温的步长

            foreach (string itemName in items)
            {
                // 获取项目属性
                int type = 0;
                int color = 0;

                if (BodyTemper.Params.GetGraphItemProperty(itemName, ref type,
                    ref valStart, ref valStep, ref condition, ref color) == false)
                {
                    continue;
                }

                // 获取数据                
                BodyTemperParams.GraphPointType itemType = BodyTemperParams.GetGraphPointType(type);

                Pen pen = BodyTemperParams.GetPenFromColor(color);

                PaintCenterPart(itemType, valStart, valStep, condition, pen);

                // 对于体温大于单独记录其设置信息
                if (itemName.Trim().EndsWith("体温"))
                {
                    valStartT = valStart;
                    valStepT = valStep;
                }
            }

            //// 首次体温大于39, 加复试符号 泾河园无此要求
           // DrawHighTemperData(valStartT, valStepT);

            // 降温
            //string itemCode = string.Empty;
            //if (BodyTemper.Params.GetDownTemperProperty(ref condition, ref itemCode, ref valStart, ref valStep) == false)
            //{
            //    return;
            //}

            //绘制房颤心率---脉搏短绌
            if (BodyTemper.Params.PulseShort.ToUpper().Trim() == "TRUE")
                DrawShadow(_gc);
            ////画降温数据
            //BodyTemper.getDownTemperData(condition, itemCode);
            //DrawDownTemper(valStart, valStep);

            BodyTemper.DtStart = _startDate;

            // 护理事件
            DrawOtherNursingEventText();
        }
        /// <summary>
        /// 画阴影
        /// </summary>
        /// <param name="g"></param>
        private void DrawShadow(Graphics g)
        {
            try
            {
                if (arrayMB != null && arrayXL != null && arrayMB.Count != 0 && arrayXL.Count != 0)
                {
                    PointF[] pointFs = new PointF[arrayMB.Count + arrayXL.Count];
                    int m = 0;
                    foreach (object obj in arrayXL)
                    {
                        PointF pxl = (PointF)obj;
                        pointFs[m] = pxl;
                        m++;
                    }
                    for (int i = arrayMB.Count - 1; i >= 0; i--)
                    {
                        PointF pmb = (PointF)arrayMB[i];
                        pointFs[m] = pmb;
                        m++;
                    }
                    HatchBrush brush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Red, Color.Transparent);
                    if ((arrayMB.Count + arrayXL.Count) > 0)
                    {
                        g.FillPolygon(brush, pointFs, FillMode.Alternate);
                    }
                    for (int i = 0; i < arrayXL.Count; i++)
                    {
                        if (i == arrayMB.Count - 1)
                        {
                            g.DrawLine(_boldRedPen, (PointF)arrayXL[arrayXL.Count - 1], (PointF)arrayMB[i]);
                            break;
                        }
                        else if (i == 0 || i == arrayXL.Count - 1)
                        {
                            g.DrawLine(_boldRedPen, (PointF)arrayXL[i], (PointF)arrayMB[i]);
                        }
                        //else
                        //{
                        //    if (i < arrayXL.Count - 1)
                        //    {
                        //        if (
                        //            ((PointF)arrayMB[i]) != ((PointF)arrayXL[i])
                        //            &&
                        //            ((PointF)arrayMB[i + 1]) == ((PointF)arrayXL[i + 1])
                        //           )
                        //        {
                        //            g.DrawLine(_boldRedPen, (PointF)arrayXL[i], (PointF)arrayMB[i + 1]);
                        //        }
                        //        else
                        //        {
                        //            if (
                        //                ((PointF)arrayMB[i]) != ((PointF)arrayXL[i])
                        //                &&
                        //                ((PointF)arrayMB[i - 1]) == ((PointF)arrayXL[i - 1])
                        //              )
                        //            {
                        //                g.DrawLine(_boldRedPen, (PointF)arrayMB[i - 1], (PointF)arrayXL[i]);
                        //            }
                        //            else
                        //            {
                        //                if (
                        //               ((PointF)arrayMB[i - 1]) != ((PointF)arrayXL[i - 1])
                        //               &&
                        //               ((PointF)arrayMB[i + 1]) != ((PointF)arrayXL[i + 1])
                        //             )
                        //                {
                        //                    g.DrawLine(_boldRedPen, (PointF)arrayMB[i - 1], (PointF)arrayMB[i]);
                        //                    g.DrawLine(_boldRedPen, (PointF)arrayMB[i], (PointF)arrayMB[i + 1]);
                        //                }
                        //            }
                        //        }
                        //    }
                        //}

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        /// <summary>
        ///     绘制头部
        /// </summary>
        private void PaintTop()
        {
            // 获取楣栏的项目
            string topItems = BodyTemper.Params.TopItems;
            string[] items = topItems.Split(ComConst.STR.COMMA.ToCharArray());

            if (_isDrawFix)
            {
                #region 表格线

                for (int i = 0; i <= items.Length; i++)
                {
                    DrawLine_Horizontal(_boldBPen, _startX, _endX - _rightWidth,
                        StartY + _topRowHeight * i);
                }

                #endregion 表格线
            }

            for (int i = 0; i < items.Length; i++)
            {
                string itemName = items[i];

                // 获取项目属性              
                string showName = string.Empty;
                if (BodyTemper.Params.GetTopItemProperty(itemName, ref showName) == false)
                {
                    continue;
                }

                if (_isDrawFix)
                {
                    _myRa = new RectangleF(_startX,
                        (float)StartY + _topRowHeight * i + _A(0.5f),
                        _leftWidth, _topRowHeight);

                    _gc.DrawString(showName,
                        BodyTemper.Params.TopItemsShows.Font,
                        BodyTemper.Params.TopItemsShows.Brush, _myRa, _formatCenterCenter);
                }
                else
                    switch (itemName)
                    {
                        case "日期":
                            DrawDateNow(i);
                            break;

                        case "住院日数":
                            DrawInpDays(i);
                            break;

                        case "术后日数":
                            DrawOperatedDays(i);
                            break;

                        case "时间":
                            DrawTimeNow(i);
                            break;
                    }
            }
        }

        /// <summary>
        ///     画楣栏的当前日期
        /// </summary>
        private void DrawTimeNow(int rowNumber)
        {
            #region 时间刻度

            for (int i = 0; i < ShowDaysColumns; i++)
            {
                for (int colIndex = 1; colIndex <= HourColumnCount; colIndex++)
                {
                    _myRa = new RectangleF((_left + (i * _middleDayWidth) +
                                            ((colIndex - 1) * _middleHourWidth)) - _A(1),
                        StartY + _topRowHeight * rowNumber,
                        (_middleHourWidth) + _A(2), _topRowHeight);
                    _gc.DrawString((HourStart + HourInterval * colIndex).ToString(), _fontEng7,
                        (colIndex > 0 && colIndex < 4) ? _brushBlack : _brushRed,
                        _myRa, _formatCenterCenter);
                }
            }

            #endregion
        }

        /// <summary>
        ///     画楣栏的入院日数
        /// </summary>
        private void DrawInpDays(int rowNumber)
        {
            // 从作图日期开始到 周末
            for (int i = 0; i < _showDays; i++)
            {
                _myRa = new RectangleF(_startX + _leftWidth + _middleDayWidth * i,
                    StartY + _topRowHeight * rowNumber,
                    _middleDayWidth, _topRowHeight);
                // 获取术后日数
                DateTime dtCurrent = _startDate.AddDays(i);

                if (dtCurrent.Date > MeasureDateEnd.Date) return;

                TimeSpan tspan = dtCurrent.Date.Subtract(_admissionDate.Date);

                // 作图
                _gc.DrawString((tspan.Days + 1).ToString(), Shows.Font, Shows.Brush, _myRa, _formatCenterCenter);
            }
        }

        /// <summary>
        ///     画楣栏的当前日期
        /// </summary>
        private void DrawDateNow(int rowNumber)
        {
            DateTime dtPre = DataType.DateTime_Null();

            // 从作图日期开始到 周末
            for (int i = 0; i < ShowDaysColumns; i++)
            {
                // 获取作图区大小, 并清除                
                _myRa = new RectangleF(_startX + _leftWidth + _middleDayWidth * i,
                    StartY + _topRowHeight * rowNumber,
                    _middleDayWidth, _topRowHeight);
                // 获取当前日期
                DateTime dtCurrent = _startDate.AddDays(i);

                // 获取当前日期的字符串
                string curDate;

                if (i == 0 || dtCurrent.Year != dtPre.Year)
                {
                    curDate = dtCurrent.ToString(ComConst.FMT_DATE.SHORT);
                }
                else
                {
                    curDate = dtCurrent.ToString(dtCurrent.Month != dtPre.Month ? "MM-dd" : "dd");
                }

                dtPre = dtCurrent;

                // 作图                
                _gc.DrawString(curDate, Shows.Font, Shows.Brush, _myRa, _formatCenterCenter);
            }
        }

        /// <summary>
        ///     画楣栏的术后日数
        /// </summary>
        private void DrawOperatedDays(int rowNumber)
        {
            // 从作图日期开始到 周末
            for (int i = 0; i < 7/*_showDays + 1*/; i++)
            {
                _myRa = new RectangleF(_startX + _leftWidth + _middleDayWidth * i,
                    StartY + _topRowHeight * rowNumber,
                    _middleDayWidth, _topRowHeight);

                // 获取术后日数
                DateTime dtCurrent = _startDate.AddDays(i);

                string operedDays = BodyTemper.getOperedDays(dtCurrent);
                //string operedDays = BodyTemper.getOperedDays(dtCurrent, BodyTemper.Params.OperateFmt);

                // 作图 
                _gc.DrawString(operedDays, Shows.Font, Shows.Brush, _myRa, _formatCenterCenter);
            }
        }

        private float GetX(DateTime timePoint)
        {
            return _startX + _leftWidth +
                   ((int)(timePoint.Subtract(_startDate.AddHours(HourStart)).TotalHours / HourInterval)) *
                   _middleHourWidth;
        }

        private float GetY(string value, float minStandard, float stepLength)
        {
            float fvalue;
            if (float.TryParse(value, out fvalue))
                return GetY(fvalue, minStandard, stepLength);
            return 0f;
        }

        private float GetY(float value, float minStandard, float stepLength)
        {
            return _endY - (ShowBottonRows * _bottomRowHeight) - ((value - minStandard) / stepLength) * _middleRowHeight;
        }

        private float GetY(double value, float minStandard, float stepLength)
        {
            return
                (float)(_endY - (ShowBottonRows * _bottomRowHeight) - ((value - minStandard) / stepLength) * _middleRowHeight);
        }

        private PointF GetPoint(DateTime timePoint, string value, float minStandard, float stepLength)
        {
            return new PointF { X = GetX(timePoint), Y = GetY(value, minStandard, stepLength) };
        }

        /// <summary>
        /// 获取画点位置
        /// </summary>
        /// <returns></returns>
        private BodyTemper.GraphPointStatus getGraphPoint(DateTime dtMeasure, string valStr, float valStart, float valStep, BodyTemperParams.GraphPointType itemType, ref PointF pt)
        {
            float col;
            float valFloat = 0;
            SizeF szUnit = BodyTemper.Params.SzUnit;
            // 获取时间坐标位置           
            col = GetX(dtMeasure);
            pt.X = (int)((col + 0.5) * szUnit.Width);
            //pt.X = ((float)tspan.TotalHours / STEP_HOURS) * szUnit.Width;

            // 获取值坐标位置
            string val = valStr;
            valFloat = 0F;

            pt.Y = -1;
            if (float.TryParse(val, out valFloat) == true)
            {
                pt.Y = (int)(((valFloat - valStart) / valStep) * szUnit.Height);
            }

            float.TryParse(val, out valFloat);

            // 体温
            if (itemType == BodyTemperParams.GraphPointType.Anus || itemType == BodyTemperParams.GraphPointType.Armpit || itemType == BodyTemperParams.GraphPointType.Mouse)
            {
                // 大于40度
                if (valFloat >= 40)
                {
                    pt.Y = (int)(((40 - valStart) / valStep) * szUnit.Height);
                    return TemperatureBLL.BodyTemper.GraphPointStatus.TEMPERATURE_H;
                }

                // 小于35
                if (valFloat <= 35)
                {
                    pt.Y = (int)(((35 - valStart) / valStep) * szUnit.Height);
                    return BodyTemper.GraphPointStatus.TEMPERATURE_L;
                }
            }

            // 脉搏
            if (itemType == BodyTemperParams.GraphPointType.Pulse)
            {
                // 大于140
                if (valFloat >= 140)
                {
                    pt.Y = (int)(((140 - valStart) / valStep) * szUnit.Height);
                    return BodyTemper.GraphPointStatus.PULSE_H;
                }

                // 脉搏不清情况
                if (valStr.Equals("不清"))
                {
                    pt.Y = (int)(((35 - valStart) / valStep) * szUnit.Height);
                    //LB添加上去的
                    pt.Y = -1;
                    return BodyTemper.GraphPointStatus.PULSE_NO;
                }
            }

            return BodyTemper.GraphPointStatus.NORMAL;
        }


        /// <summary>
        /// 画异常点
        /// </summary>
        private int drawAbnormalPoint(BodyTemper.GraphPointStatus ptStatus, PointF pt, double valFloat)
        {
            SizeF szUnit = BodyTemper.Params.SzUnit;
            int temperPos = BodyTemper.Params.TemperEventPos;

            // 体温事件 体温大于40
            if (ptStatus == BodyTemper.GraphPointStatus.TEMPERATURE_H)
            {
                RectangleF rect = new RectangleF(pt.X - szUnit.Width / 2, pt.Y - 1,
                                             szUnit.Width, szUnit.Height * temperPos);

                drawStringNurseEvent(rect, valFloat.ToString());
            }

            // 脉搏事件 脉搏大于140
            if (ptStatus == BodyTemper.GraphPointStatus.PULSE_H)
            {
                //RectangleF rect = new RectangleF(pt.X - szUnit.Width / 2, pt.Y + 35,
                //                             szUnit.Width, szUnit.Height * temperPos);
                RectangleF rect = new RectangleF(pt.X - szUnit.Width / 2, pt.Y + 45,
                                            szUnit.Width, szUnit.Height * temperPos);
                //CJ20110812 设置脉搏大于140的文本字体
                drawStringNurseEvent(rect, valFloat.ToString());
            }

            // 体温小于35度事件
            if (ptStatus == BodyTemper.GraphPointStatus.TEMPERATURE_L)
            {
                DrawLegend(BodyTemperParams.GraphPointType.Arrow, (int)(pt.X), (int)(pt.Y - 2 * szUnit.Height));
                //LB20110524新增
                return 0;
            }

            // 脉搏不清情况
            if (ptStatus == BodyTemper.GraphPointStatus.PULSE_NO)
            {
                RectangleF rect = new RectangleF(pt.X - szUnit.Width / 2, pt.Y + 10,
                                             szUnit.Width, szUnit.Height * temperPos);
                //画文本
                _gc.DrawString("不清", BodyTemper.Params.GraphTextFont, Brushes.Blue, rect);

                //画符号
                DrawLegend(BodyTemperParams.GraphPointType.Symbols, (int)(pt.X), (int)(pt.Y + szUnit.Height * 1.5));
                DrawLegend(BodyTemperParams.GraphPointType.Arrow, (int)(pt.X), (int)(pt.Y - 3 * szUnit.Height));

                return 0;
            }

            return 1;
        }

        /// <summary>
        /// 在画板上输出护理事件文本
        /// </summary>
        /// <param name="gc"></param>
        /// <param name="rect"></param>
        /// <param name="text"></param>
        private void drawStringNurseEvent(RectangleF rect, string text)
        {
            RectangleF rectDraw = new RectangleF(rect.X, rect.Y,
                                                 BodyTemper.Params.SzUnit.Width,
                                                 BodyTemper.Params.SzUnit.Height);

            // 不升符号显示
            if (text.Equals("不升") == true && BodyTemper.Params.TempNotAscendSymbol.Equals("1") == true)
            {
                int x = (int)(rectDraw.X + rectDraw.Width / 2);
                _gc.DrawLine(Pens.Red, x, rectDraw.Y + 2, x, rectDraw.Y + BodyTemper.Params.SzUnit.Height * 2 - 2);

                rectDraw.X += 2;
                rectDraw.Y += BodyTemper.Params.SzUnit.Height + 2;
                rectDraw.Height -= 4;

                _gc.DrawLine(Pens.Red, rectDraw.X, rectDraw.Y, x, rectDraw.Y + rectDraw.Height);

                int x1 = (int)(x + (x - rectDraw.X - 1) * 2);
                _gc.DrawLine(Pens.Red, x1, rectDraw.Y, x, rectDraw.Y + rectDraw.Height);
                return;
            }

            for (int i = 0; i < text.Length; i++)
            {
                if (i > 0)
                {
                    rectDraw.Y += BodyTemper.Params.SzUnit.Height;
                }

                string charText = text.Substring(i, 1);

                if (charText.Equals("|") == false)
                {
                    _gc.DrawString(charText, BodyTemper.Params.GraphTextFont, Brushes.Red, rectDraw);
                }
                else
                {
                    rectDraw.X += (int)(rectDraw.Width / 2);
                    _gc.DrawLine(Pens.Red, new Point((int)(rectDraw.X), (int)(rectDraw.Y) + 2),
                                            new Point((int)(rectDraw.X), (int)(rectDraw.Y + rectDraw.Height) - 2));
                    rectDraw.X -= (int)(rectDraw.Width / 2);
                }
            }
        }


        /// <summary>
        ///     画降温数据
        /// </summary>
        private void DrawDownTemper(float valStart, float valStep)
        {
            var pen = new Pen(Color.Red, 1) { DashStyle = DashStyle.Dash };

            var pt0 = new PointF();
            var pt1 = new PointF();

            foreach (DataRow dr in BodyTemper.dtDownTemper.Rows)
            {
                // 获取时间坐标位置
                var dtMeasure = (DateTime)dr["TIME_POINT"];
                pt1.X = pt0.X;

                // 获取值坐标位置                
                string val = dr["PRE_TEMPER"].ToString();
                pt0 = GetPoint(dtMeasure, val, valStart, valStep);

                val = dr["DOWN_TEMPER"].ToString();
                pt1 = GetPoint(dtMeasure, val, valStart, valStep);

                // 画图
                DrawLegend(BodyTemperParams.GraphPointType.Heartrate, pt1);

                _gc.DrawLine(pen, pt0, pt1);
            }
        }

        /// <summary>
        ///画脉脉搏短绌
        /// </summary>
        private void DrawPulseShort(Size szRng)
        {
            #region 获取参数

            //获取App_Config表脉搏短绌值 1002,1001,20,4
            string[] parts = BodyTemper.Params.PulseShort.Split(ComConst.STR.COMMA.ToCharArray());
            string vitalCode0 = parts[0].Trim(); //心率代码
            string vitalCode1 = parts[1].Trim(); //脉搏代码

            float valStart;
            float valStep;
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
                string temp = vitalCode0;//vitalCode0 = 1001
                vitalCode0 = vitalCode1;//vitalCode1 = 1002
                vitalCode1 = temp;
            }

            #endregion

            #region 获取数据

            string condition = "VITAL_CODE = " + SqlManager.SqlConvert(vitalCode0)
                               + "OR VITAL_CODE = " + SqlManager.SqlConvert(vitalCode1);//VITAL_CODE = '1001' OR VITAL_CODE = '1002' 

            // 获取数据           
            string filter = " ( " + condition + ")";

            DataRow[] drFind = _patientVitals.Select(filter, "TIME_POINT ASC, VITAL_CODE ASC");//找出VITAL_CODE=1001.1002的数据

            #endregion

            #region 定义变量

            var arr0 = new ArrayList();
            var arr1 = new ArrayList();
            var arrC = new ArrayList();

            float colPre = -1;

            bool exist = false;

            bool pair = true;
            int row = -1;

            var path = new GraphicsPath();

            #endregion

            while (row < drFind.Length)
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
                            _gc.SetClip(path);
                        }
                        else
                        {
                            _gc.SetClip(path, CombineMode.Union);
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
                var dtMeasure = (DateTime)dr["TIME_POINT"];

                var pt0 = new PointF();
                var pt1 = new PointF();

                float valFloat = 0;

                // 第一点
                if (dr["VITAL_CODE"].ToString().Equals(vitalCode0) == false)
                {
                    pair = false;
                    continue;
                }

                float col = (float)((dtMeasure.Subtract(_startDate).TotalDays % ShowDaysColumns) * _middleDayWidth +
                                     (GetCellIndex(dtMeasure.Hour) + 0.5f) * _middleHourWidth);

                if (IsBreakPoint(colPre, col))
                {
                    colPre = -1;
                    row--;

                    pair = false;
                    continue;
                }

                string val = dr["VITAL_SIGNS_CVALUES"].ToString();
                if (float.TryParse(val, out valFloat) == false)
                {
                    pair = false;
                    continue;
                }

                pt0 = GetPoint(dtMeasure, val, valStart, valStep);

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

                pt1 = GetPoint(dtMeasure, val, valStart, valStep);

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
                    _gc.SetClip(path);
                }
                else
                {
                    _gc.SetClip(path, CombineMode.Union);
                }

                _gc.DrawPath(_boldRedPen, path);

                exist = true;

                path.Dispose();
                arr0.Clear();
                arr1.Clear();
            }

            #endregion

            #region 画图

            float cx = -1 * (float)szRng.Width / 2;
            var ptStart = new Point(0, 0);
            var ptEnd = new Point(0, szRng.Height);

            while (exist && cx < szRng.Width)
            {
                // 两个坐标点的X轴交换即可改变画的斜线倾斜方向(撇或捺)
                ptStart.X = (int)(cx + (float)szRng.Width / 2);
                ptEnd.X = (int)(cx);

                _gc.DrawLine(_redPen, ptStart, ptEnd);

                cx += _middleHourWidth / 2;
            }

            _gc.ResetClip();

            // 画闭合线
            int c = 0;
            while (c < arrC.Count - 1)
            {
                _gc.DrawLine(_boldRedPen, (PointF)arrC[c++], (PointF)arrC[c++]);
            }

            #endregion
        }

        private GraphicsPath getGraphicsPath(ref ArrayList arr0, ref ArrayList arr1, ref ArrayList arrC)
        {
            PointF pt;

            var arrPoint = new PointF[arr0.Count + arr1.Count + 1];

            int c = 0;
            for (; c < arr0.Count; c++)
            {
                pt = (PointF)arr0[c];
                arrPoint[c] = new PointF(pt.X, pt.Y);
            }

            for (int k = arr0.Count + arr1.Count - 1; k >= arr0.Count; k--)
            {
                pt = (PointF)arr1[k - arr0.Count];
                arrPoint[c++] = new PointF(pt.X, pt.Y);
            }

            pt = (PointF)arr0[0];
            arrPoint[c] = new PointF(pt.X, pt.Y);

            var path = new GraphicsPath(FillMode.Winding);
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
        ///     检查前一个点是不是断点
        /// </summary>
        /// <returns></returns>
        private bool IsBreakPoint(float colPre, float colCur)
        {
            if (colPre == -1)
            {
                return false;
            }

            return (from object t in _arrBreakCols select (int)(t)).Any(colVal => colPre <= colVal && colVal <= colCur);
        }

        /// <summary>
        ///     画其它护理事件的文本显示 记录体温首次入院高于39的病人
        /// </summary>
        private void DrawHighTemperData(float valStart, float valStep)
        {
            // 获取数据		               
            //-------------------------
            // 判断 是否是第一天
            if (_startDate.Date.Equals(_admissionDate.Date) == false)
            {
                return;
            }

            // 获取数据
            const string filter = "VITAL_SIGNS LIKE '%体温' ";

            DataRow[] drFind = _patientVitals.Select(filter, "TIME_POINT ASC");
            if (drFind.Length == 0) return;

            var dtMeasure = (DateTime)drFind[0]["TIME_POINT"];
            string val = drFind[0]["VITAL_SIGNS_CVALUES"].ToString();

            // 判断体温是否大于39
            float valFloat;
            float.TryParse(val, out valFloat);
            if (valFloat < 39) return;

            // 画图
            DrawLegend(BodyTemperParams.GraphPointType.Verify,
                GetX(dtMeasure) + _middleHourWidth * 0.5f
                , GetY(valFloat, valStart, valStep));
        }


        /// <summary>
        ///     画图例
        /// </summary>
        /// <param name="gc"></param>
        /// <param name="eLegend"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void DrawLegend(BodyTemperParams.GraphPointType eLegend, PointF p)
        {
            DrawLegend(eLegend, p.X, p.Y);
        }

        /// <summary>
        ///     画图例
        /// </summary>
        /// <param name="_gc"></param>
        /// <param name="eLegend"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void DrawLegend(BodyTemperParams.GraphPointType eLegend, float x, float y)
        {
            var szUnit = new Size
            {
                Width = (int)BodyTemper.Params.SzUnit.Width / 2,
                Height = (int)BodyTemper.Params.SzUnit.Height / 2
            };

            y = y - this._middleRowHeight / 2;
            var rect = new RectangleF(x, y, this._middleHourWidth,
                this._middleRowHeight);

            switch (eLegend)
            {
                case BodyTemperParams.GraphPointType.Pulse: // 脉搏
                    _gc.DrawEllipse(Pens.Red, rect);
                    _gc.FillEllipse(Brushes.Red, rect);
                    break;

                case BodyTemperParams.GraphPointType.Heartrate: // 心率
                    rect.X -= 2;
                    rect.Width += 4;
                    rect.Y -= 2;
                    rect.Height += 4;

                    _gc.DrawEllipse(Pens.Red, rect);
                    break;

                case BodyTemperParams.GraphPointType.Mouse: // 口表
                    if (BodyTemper.Params.TempMouseSymbol.Equals("+"))
                    {
                        _gc.DrawLine(Pens.Blue, rect.X, rect.Y + rect.Height / 2, rect.X + rect.Width,
                            rect.Y + rect.Height / 2);
                        _gc.DrawLine(Pens.Blue, rect.X + rect.Width / 2, rect.Y, rect.X + rect.Width / 2,
                            rect.Y + rect.Height);
                    }
                    else
                    {
                        _gc.DrawEllipse(Pens.Blue, rect);
                        _gc.FillEllipse(Brushes.Blue, rect);
                    }

                    break;

                case BodyTemperParams.GraphPointType.Anus: // 肛表
                    _gc.DrawEllipse(Pens.Blue, rect);
                    break;

                case BodyTemperParams.GraphPointType.Armpit: // 腋表
                    //szUnit.Width = (int)(szUnit.Width * 1.2);
                    //szUnit.Height = (int)(szUnit.Height * 1.2);
                    rect = new Rectangle((int)(x - szUnit.Width / 2), (int)(y - szUnit.Height / 2), szUnit.Width,
                        szUnit.Height);

                    _gc.DrawLine(Pens.Blue, rect.Left, rect.Top, rect.Left + rect.Width, rect.Top + rect.Height);
                    _gc.DrawLine(Pens.Blue, rect.Left, rect.Top + rect.Height, rect.Left + rect.Width, rect.Top);
                    break;

                case BodyTemperParams.GraphPointType.Breath: // 呼吸
                    _gc.DrawEllipse(Pens.Blue, rect);
                    _gc.FillEllipse(Brushes.Blue, rect);
                    break;

                //√
                case BodyTemperParams.GraphPointType.Verify: // 确认
                    var pt0 = new PointF(x, y);

                    pt0.X -= (float)(szUnit.Width * 0.4);
                    pt0.Y -= (float)(szUnit.Height * 1.1);

                    var pt1 = new PointF(x, y);

                    var pt2 = new PointF(x, y);
                    pt2.X += (float)(szUnit.Width * 0.7);
                    pt2.Y -= (float)(szUnit.Height * 1.5);

                    _gc.DrawLine(_boldRedPen, pt0, pt1);
                    _gc.DrawLine(_boldRedPen, pt2, pt1);

                    break;

                case BodyTemperParams.GraphPointType.Arrow: // 箭头
                    var pt3 = new PointF(x, y);
                    pt3.X += (float)(szUnit.Width * 0.25 - 1);
                    pt3.Y += szUnit.Height * 4;

                    var pt6 = new PointF(x, y);
                    pt6.X += (float)(szUnit.Width * 0.25 - 1);
                    pt6.Y += szUnit.Height;

                    var pt4 = new PointF(x, y);
                    pt4.X += szUnit.Width - 10;
                    pt4.Y += szUnit.Height * 2;

                    var pt5 = new PointF(x, y);
                    pt5.X += (float)(szUnit.Width * 0.7);
                    pt5.Y += szUnit.Height * 2;

                    _gc.DrawLine(Pens.Blue, pt3, pt6);
                    _gc.DrawLine(Pens.Blue, pt4, pt6);
                    _gc.DrawLine(Pens.Blue, pt5, pt6);

                    break;
                case BodyTemperParams.GraphPointType.Symbols: // 圈中再叉符号
                    rect = new Rectangle((int)(x - szUnit.Width), (int)(y - szUnit.Height), szUnit.Width * 2,
                        szUnit.Height * 2);
                    _gc.DrawEllipse(Pens.Blue, rect);

                    _gc.DrawLine(Pens.Blue, rect.Left + 2, rect.Top + 2, (rect.Left + rect.Width) - 2,
                        (rect.Top + rect.Height) - 2);
                    _gc.DrawLine(Pens.Blue, rect.Left + 2, (rect.Top + rect.Height) - 2, (rect.Left + rect.Width) - 2,
                        rect.Top + 2);
                    break;
                case BodyTemperParams.GraphPointType.BreathMachine:
                    _gc.DrawEllipse(Pens.Blue, rect);
                    _gc.FillEllipse(Brushes.Blue, rect);
                    break;
            }
        }

        /// <summary>
        ///     画其它护理事件的文本显示
        /// </summary>
        private void DrawOtherNursingEventText()
        {
            // 获取数据		    

            string filter = "(" + BodyTemper.Params.NursingEventFilter + ")";

            DataRow[] drFind = _patientVitals.Select(filter, "TIME_POINT ASC");

            for (int i = 0; i < drFind.Length; i++)
            {
                // 预处理
                string eventName = drFind[i]["VITAL_SIGNS"].ToString();
                string vitalCode = drFind[i]["VITAL_CODE"].ToString();
                if (BodyTemper.Params.ValueOutEvent.IndexOf(vitalCode) >= 0)
                {
                    eventName += drFind[i]["VITAL_SIGNS_CVALUES"].ToString();
                }

                // 正常处理
                DateTime dtMeasure = DateTime.Parse(drFind[i]["TIME_POINT"].ToString());

                // 护理事件名称
                if (BodyTemper.Params.TimeOutEvent.IndexOf(vitalCode) >= 0)
                {
                    eventName += "|" + BodyTemper.getTimeText(dtMeasure.ToString(ComConst.FMT_DATE.TIME_SHORT));
                }

                // 输出
                var rectDraw = new RectangleF(GetX(dtMeasure), _top + _middleRowHeight * BodyTemper.Params.NurseEventPos,
                    _middleHourWidth,
                    _middleRowHeight * eventName.Length);



                // 不升符号显示
                if (eventName.Equals("不升") && BodyTemper.Params.TempNotAscendSymbol.Equals("1"))
                {
                    var x = (int)(rectDraw.X + rectDraw.Width / 2);
                    _gc.DrawLine(Pens.Red, x, rectDraw.Y + 2, x, rectDraw.Y + BodyTemper.Params.SzUnit.Height * 2 - 2);

                    rectDraw.X += 2;
                    rectDraw.Y += BodyTemper.Params.SzUnit.Height + 2;
                    rectDraw.Height -= 4;

                    _gc.DrawLine(Pens.Red, rectDraw.X, rectDraw.Y, x, rectDraw.Y + rectDraw.Height);

                    var x1 = (int)(x + (x - rectDraw.X - 1) * 2);
                    _gc.DrawLine(Pens.Red, x1, rectDraw.Y, x, rectDraw.Y + rectDraw.Height);
                    return;
                }

                for (int j = 0; j < eventName.Length; j++)
                {
                    if (!eventName.Contains("不升") && !eventName.Contains("®"))//eventName.Contains("®") || 
                    {
                        rectDraw = new RectangleF(GetX(dtMeasure),
                        _top + _middleRowHeight * (BodyTemper.Params.NurseEventPos + j),
                        _middleHourWidth,
                        _middleRowHeight);

                        string charText = eventName.Substring(j, 1);

                        if (charText.Equals("|") == false)
                        {
                            _gc.DrawString(charText, BodyTemper.Params.GraphTextFont, Brushes.Red, rectDraw, _formatCenterLeft);
                        }
                        else
                        {
                            rectDraw.X += (int)(rectDraw.Width / 2);
                            _gc.DrawLine(Pens.Red, new Point((int)(rectDraw.X), (int)(rectDraw.Y) + 2),
                                new Point((int)(rectDraw.X), (int)(rectDraw.Y + rectDraw.Height) - 2));
                            rectDraw.X -= (int)(rectDraw.Width / 2);
                        }
                    }

                }


                //////测试代码
                // 输出
                var rectDrawTwbs = new RectangleF(GetX(dtMeasure), _top + _middleRowHeight * BodyTemper.Params.NurseEventPosTwbs,
                    _middleHourWidth,
                    _middleRowHeight * eventName.Length);

                //呼吸机
                var rectDrawHxj = new RectangleF(GetX(dtMeasure), _top + _middleRowHeight * BodyTemper.Params.NurseEventPosHxj,
                    _middleHourWidth,
                    _middleRowHeight * eventName.Length);



                //// 不升符号显示
                //if (eventName.Equals("不升") && BodyTemper.Params.TempNotAscendSymbol.Equals("1"))
                //{
                //    var x = (int)(rectDrawTwbs.X + rectDrawTwbs.Width / 2);
                //    _gc.DrawLine(Pens.Red, x, rectDrawTwbs.Y + 2, x, rectDrawTwbs.Y + BodyTemper.Params.SzUnit.Height * 2 - 2);

                //    rectDrawTwbs.X += 2;
                //    rectDrawTwbs.Y += BodyTemper.Params.SzUnit.Height + 2;
                //    rectDrawTwbs.Height -= 4;

                //    _gc.DrawLine(Pens.Red, rectDrawTwbs.X, rectDrawTwbs.Y, x, rectDrawTwbs.Y + rectDrawTwbs.Height);

                //    var x1 = (int)(x + (x - rectDrawTwbs.X - 1) * 2);
                //    _gc.DrawLine(Pens.Red, x1, rectDrawTwbs.Y, x, rectDrawTwbs.Y + rectDrawTwbs.Height);
                //    return;
                //}

                for (int j = 0; j < eventName.Length; j++)
                {
                    if (eventName.Contains("不升"))
                    {
                        rectDrawTwbs = new RectangleF(GetX(dtMeasure),
                           _top + _middleRowHeight * (BodyTemper.Params.NurseEventPosTwbs + j),
                           _middleHourWidth,
                           _middleRowHeight);

                        string charText = eventName.Substring(j, 1);

                        if (charText.Equals("|") == false)
                        {
                            _gc.DrawString(charText, BodyTemper.Params.GraphTextFont, Brushes.Red, rectDrawTwbs, _formatCenterLeft);
                        }
                        else
                        {
                            rectDrawTwbs.X += (int)(rectDrawTwbs.Width / 2);
                            _gc.DrawLine(Pens.Red, new Point((int)(rectDrawTwbs.X), (int)(rectDrawTwbs.Y) + 2),
                                new Point((int)(rectDrawTwbs.X), (int)(rectDrawTwbs.Y + rectDrawTwbs.Height) - 2));
                            rectDrawTwbs.X -= (int)(rectDrawTwbs.Width / 2);
                        }
                    }
                }
                for (int j = 0; j < eventName.Length; j++)
                {
                    if (eventName.Contains("®"))
                    {
                        rectDrawHxj = new RectangleF(GetX(dtMeasure),
                           _top + _middleRowHeight * (BodyTemper.Params.NurseEventPosHxj + j),
                           _middleHourWidth,
                           _middleRowHeight);

                        string charText = eventName.Substring(j, 1);

                        if (charText.Equals("|") == false)
                        {
                            Font drawFont = new Font("宋体", 14);
                            _gc.DrawString(charText, drawFont, Brushes.Black, rectDrawHxj, _formatCenterLeft);
                        }
                        else
                        {
                            rectDrawHxj.X += (int)(rectDrawHxj.Width / 2);
                            _gc.DrawLine(Pens.Black, new Point((int)(rectDrawHxj.X), (int)(rectDrawHxj.Y) + 2),
                                new Point((int)(rectDrawHxj.X), (int)(rectDrawHxj.Y + rectDrawHxj.Height) - 2));
                            rectDrawHxj.X -= (int)(rectDrawHxj.Width / 2);
                        }
                    }
                }


            }
        }


        /// <summary>
        ///     绘制中心部分
        /// </summary>
        private void PaintCenterPart(BodyTemperParams.GraphPointType type, float minStandard, float stepLength,
            string condition, Pen pen)
        {
            if (type == BodyTemperParams.GraphPointType.Breath)
            {
                PaintBreath();
            }


            //bool verifyItem = BodyTemper.isVerifyItem(condition);          // 是否是需要确认的项目

            var p = new Pulse();

            string filter = "( " + condition + ")";

            DataRow[] drFind = _patientVitals.Select(filter, "TIME_POINT ASC");
            double preValue;
            double dItemValue = 0;
            foreach (DataRow drPatientVital in drFind)
            {
                int i = 0;
                DateTime timePoint = Convert.ToDateTime(drPatientVital["TIME_POINT"]);

                string itemValue = drPatientVital["VITAL_SIGNS_CVALUES"].ToString();


                preValue = dItemValue;
                if (!double.TryParse(itemValue, out dItemValue))
                {
                    continue;
                }

                float pointXWrite = GetX(timePoint);

                PointF pt = new PointF(-1, -1);

                BodyTemper.GraphPointStatus ptStatus = getGraphPoint(timePoint, itemValue, minStandard, stepLength, type, ref pt);

                if (drawAbnormalPoint(ptStatus, pt, dItemValue) == 0)
                {
                    //arrBreakCols.Add(col);                      // 记录断点
                    continue;
                }

                switch (type)
                {
                    case BodyTemperParams.GraphPointType.Armpit:
                    case BodyTemperParams.GraphPointType.Mouse:
                        DrawSpec("体温", pointXWrite, 0f, ref dItemValue);
                        break;
                    case BodyTemperParams.GraphPointType.Pulse:
                        DrawSpec("脉搏", pointXWrite, 0f, ref dItemValue);
                        break;
                    case BodyTemperParams.GraphPointType.Heartrate:
                        DrawSpec("心率", pointXWrite, 0f, ref dItemValue);
                        break;
                    case BodyTemperParams.GraphPointType.BreathMachine:
                        DrawSpec("呼吸机", pointXWrite, 0f, ref dItemValue);
                        break;
                }


                p.PrevP = p.CurrentP;
                p.IsP = true;
                p.CurrentP = new PointF(
                    pointXWrite + _middleHourWidth * 0.5f,
                    GetY(dItemValue, minStandard, stepLength)
                    );

                //if (!p.PrevP.IsEmpty)
                //    _gc.DrawLine(pen, p.PrevP, p.CurrentP);
                if (type == BodyTemperParams.GraphPointType.PhysicalCoolT)
                {
                    if (arrayTW.Count > 0)
                        foreach (PointF point in arrayTW)
                            if (point.X == p.CurrentP.X)
                            {
                                DashStyle ds = pen.DashStyle;
                                pen.DashStyle = DashStyle.Dash;
                                _gc.DrawLine(pen, point, p.CurrentP);
                                pen.DashStyle = ds;
                                break;
                            }
                }
                else if (!p.PrevP.IsEmpty)
                    _gc.DrawLine(pen, p.PrevP, p.CurrentP);

                switch (type)
                {
                    case BodyTemperParams.GraphPointType.Armpit:
                    case BodyTemperParams.GraphPointType.Mouse:
                        Draw(p.CurrentP, IconPick);
                        this.arrayTW.Add(p.CurrentP);
                        break;
                    case BodyTemperParams.GraphPointType.Pulse:
                        {
                            bool flag = false;
                            foreach (PointF point in arrayTW)
                                if (point == p.CurrentP)
                                    flag = true;
                            if (flag)//体温和脉搏重合点绘制
                                Draw(p.CurrentP, IconCircleHollow);
                            else
                                Draw(p.CurrentP, IconCircleSolid);

                            foreach (PointF point in arrayXL)
                            {
                                if (p.CurrentP.X == point.X)
                                    this.arrayMB.Add(p.CurrentP);//缓存与心率点相匹配的脉搏点
                            }
                        }
                        break;
                    case BodyTemperParams.GraphPointType.Breath:
                        Draw(p.CurrentP, IconCircleSolidBlue);
                        break;
                    case BodyTemperParams.GraphPointType.Heartrate:
                        Draw(p.CurrentP, IconCircleHollow);//根据配置首先缓存心率点
                        this.arrayXL.Add(p.CurrentP);
                        break;
                    case BodyTemperParams.GraphPointType.BreathMachine:
                        Draw(p.CurrentP, IconBreathMachine);
                        break;
                    case BodyTemperParams.GraphPointType.PhysicalCoolT://物理降温
                        Draw(p.CurrentP, PhysicalCoolT);
                        break;
                }

                // 是否确认
                //if (verifyItem)
                //{
                //    if (BodyTemper.chkVerifyPoint(preValue, dItemValue))
                //    {
                //        DrawLegend(BodyTemperParams.GraphPointType.Verify, p.CurrentP);
                //    }
                //}
            }
        }

        /// <summary>
        ///     画底部.底部的行集和.第[pageIndex]页
        /// </summary>
        private void PaintBottom()
        {
            BodyTemper.DtNow = GVars.OracleAccess.GetSysDate();

            // 呼吸的行号。呼吸占两行。
            int breathNo = -1;

            // 获取尾栏的项目
            string topItems = BodyTemper.Params.TailItems;
            string[] items = topItems.Split(ComConst.STR.COMMA.ToCharArray());

            int rowNumber = 0;

            //遍历尾栏项目
            foreach (string itemName in items)
            {
                // 获取项目属性
                int type = 0;
                string condition = string.Empty;
                int dayStart = 0;


                if (BodyTemper.Params.GetTailItemProperty(itemName, ref type, ref condition, ref dayStart) == false)
                {
                    continue;
                }

                if (itemName.Contains("呼吸"))
                {
                    breathNo = rowNumber + 1;

                    // 画个白线把之前的黑线盖住...
                    //DrawLine_Horizontal(new Pen(Color.White, 1.5f), StartX, EndX,
                    //    (this.EndY - (SHOW_BOTTON_ROWS * this.BottomRowHeight))
                    //    + (rowNumber + 1) * this.BottomRowHeight);

                    _myRa = new RectangleF(_startX, (_endY - (ShowBottonRows * _bottomRowHeight))
                                                    + rowNumber * _bottomRowHeight + _A(1),
                        _leftWidth, (float)_bottomRowHeight * 2);

                    if (_isDrawFix)
                        _gc.DrawString(itemName, _fontChn12,
                            Shows.Brush, _myRa, _formatCenterCenter);
                    else
                        DrawTailSixTimePerday(condition, dayStart, rowNumber);

                    // 呼吸占两行
                    rowNumber += 2;

                    continue;
                }

                if (_isDrawFix)
                {
                    _myRa = new RectangleF(_startX, (_endY - (ShowBottonRows * _bottomRowHeight))
                                                    + rowNumber * _bottomRowHeight + _A(1),
                        _leftWidth, _bottomRowHeight);
                    _gc.DrawString(itemName,
                        BodyTemper.Params.TailItemsShows.Font,
                        BodyTemper.Params.TailItemsShows.Brush, _myRa, _formatCenterLeft);
                }
                else
                {
                    BodyTemperParams.TailItemType itemType = BodyTemperParams.GetTailItemType(type);
                    switch (itemType)
                    {
                        case BodyTemperParams.TailItemType.ONE: // 类型: 单项
                            DrawTailOneTimePerday(condition, dayStart, rowNumber);
                            break;
                        case BodyTemperParams.TailItemType.SUM: // 合计
                            DrawTailSumPerday(condition, dayStart, rowNumber);
                            break;

                        case BodyTemperParams.TailItemType.TWO: // 两项
                            drawTailTwoTimePerday_Border(condition, rowNumber);
                            break;

                        case BodyTemperParams.TailItemType.CLYSTER: // 灌肠
                            DrawTailStoolPerday(condition, dayStart, rowNumber);
                            break;
                    }
                }
                rowNumber++;
            }

            if (_isDrawFix)
            {
                #region 表格线

                for (int i = 0; i < ShowBottonRows; i++)
                {
                    if (i == breathNo) continue;

                    DrawLine_Horizontal(i == 0 ? _boldBPen : _thinBPen, _startX,
                        _endX - _rightMargin - _rightWidth,
                        _top + (ShowAxisRows * _middleRowHeight) + _bottomRowHeight * i
                        );
                }

                #endregion
            }
            else
            {
                #region 第几页

                _myRa = new RectangleF(_startX,
                    _endY + _A(2),
                    _endX - _startX,
                    2 * _bottomRowHeight);

                // iTmp = (this.toltalPage + 1) - this.pageIndex;
                // string Tempp = "第" + iTmp.ToString() + "页";

                string tempp = "第" + _pageIndex + "页";

                PaintMiddleCenter(tempp, _endY + _A(2), 2 * _bottomRowHeight, _fontChn9, _brushBlack);

                _myRa = new RectangleF(_startX, _endY + _A(1), _endX, _bottomRowHeight);

                #endregion
            }
        }

        /// <summary>
        ///     画尾栏的一日合计项目
        /// </summary>
        private void DrawTailSumPerday(string condition, int dayStart, int rowNumber)
        {
            // 从作图日期开始到 周末
            for (int i = 0; i < _showDays; i++)
            {
                // 获取作图区大小, 并清除
                var rectRng = new RectangleF(_startX + _leftWidth + _middleDayWidth * i,
                    _endY - _bottomRowHeight * (11 - rowNumber),
                    _middleDayWidth, _bottomRowHeight);

                // 条件检查
                DateTime dtCurrent = _startDate.AddDays(i);

                dtCurrent = dtCurrent.Date.AddHours(dayStart);

                string filter = "TIME_POINT >= " + SqlManager.SqlConvert(dtCurrent.ToString(ComConst.FMT_DATE.LONG));
                filter += " AND TIME_POINT < " +
                          SqlManager.SqlConvert(dtCurrent.AddDays(1).ToString(ComConst.FMT_DATE.LONG));
                filter += " AND ( " + condition + ")";

                DataRow[] drFind = _patientVitals.Select(filter);

                float val =
                    drFind.Select(t => t["VITAL_SIGNS_CVALUES"].ToString())
                        .Select(content => BodyTemper.GetSumValueFromString(content))
                        .Sum();

                // 作图
                if (val.ToString() != "0")
                    _gc.DrawString(val.ToString(), Shows.Font, Shows.Brush, rectRng, _formatCenterCenter);
            }
        }


        /// <summary>
        ///     画尾栏的一日两项的项目
        /// </summary>
        private void DrawTailTwoTimePerday(ref Graphics gc, Point ptStart, Size szUnitRng, string condition,
            int rowNumber)
        {
            int hours = ComConst.VAL.HOURS_PER_DAY / 2;

            // 从作图日期开始到 周末
            for (int i = 0; i < _showDays * 2; i++)
            {
                // 获取作图区大小, 并清除
                var rectRng = new RectangleF(_startX + _leftWidth + _middleDayWidth * i,
                    _endY - _bottomRowHeight * (11 - rowNumber),
                    _middleDayWidth, _bottomRowHeight);


                // 条件检查
                DateTime dtCurrent = _startDate.Date.AddHours(hours * i);
                if (dtCurrent.Date.CompareTo(BodyTemper.DtNow.Date) > 0)
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
                filter += " AND TIME_POINT < " +
                          SqlManager.SqlConvert(dtCurrent.AddHours(hours).ToString(ComConst.FMT_DATE.LONG));
                filter += " AND ( " + condition + ")";

                DataRow[] drFind = _patientVitals.Select(filter, "TIME_POINT");
                if (drFind.Length >= 1)
                {
                    string val = drFind[0]["VITAL_SIGNS_CVALUES"].ToString();

                    // 作图
                    _gc.DrawString(val, Shows.Font, Shows.Brush, rectRng, _formatCenterCenter);
                }

                ptStart.X += szUnitRng.Width;
            }
        }


        /// <summary>
        ///     画尾栏的一日两项的项目 (如果有多项, 最开始与最后两项)
        /// </summary>
        private void drawTailTwoTimePerday_Border(string condition, int rowNumber)
        {
            // 从作图日期开始到 周末
            for (int i = 0; i < _showDays; i++)
            {
                DrawLine_Vertical(_thinBPen1, _startX + _leftWidth + _middleDayWidth * i
                                              + _middleHourWidth * 3, _endY - _bottomRowHeight * (11 - rowNumber),
                    _endY - _bottomRowHeight * (11 - rowNumber - 1));

                // 条件检查
                DateTime dtCurrent = _startDate.Date.AddDays(i);

                // 获取值
                string filter = "TIME_POINT >= " + SqlManager.SqlConvert(dtCurrent.ToString(ComConst.FMT_DATE.LONG));
                filter += " AND TIME_POINT < " +
                          SqlManager.SqlConvert(dtCurrent.AddDays(1).ToString(ComConst.FMT_DATE.LONG));
                filter += " AND ( " + condition + ")";

                DataRow[] drFind = _patientVitals.Select(filter, "TIME_POINT");
                if (drFind.Length == 1)
                {
                    DateTime timePoint = Convert.ToDateTime(drFind[0]["TIME_POINT"]);

                    _myRa = new RectangleF(_startX + _leftWidth + _middleDayWidth * i
                                           + _middleHourWidth * (GetCellIndex(timePoint.Hour > 12 ? 12 : 0) - 1),
                        _endY - _bottomRowHeight * (11 - rowNumber),
                        _middleDayWidth / 2 + _middleHourWidth * 2, _bottomRowHeight);

                    //CJ20110812 modify end 血压分上午和下午，上午显示在左边，下午显示在右边
                    string val = drFind[0]["VITAL_SIGNS_CVALUES"].ToString();
                    _gc.DrawString(val, Shows.Font, Shows.Brush, _myRa, _formatCenterCenter);
                }
                else if (drFind.Length > 1)
                {
                    for (int c = 0; c < 2; c++)
                    {
                        int recIndex = 0;
                        if (c > 0) recIndex = drFind.Length - 1;

                        _myRa = new RectangleF(_startX + _leftWidth + _middleDayWidth * i
                                               + _middleHourWidth * (GetCellIndex(c > 0 ? 12 : 0) - 1),
                            _endY - _bottomRowHeight * (11 - rowNumber),
                            _middleDayWidth / 2 + _middleHourWidth * 2, _bottomRowHeight);

                        string val = drFind[recIndex]["VITAL_SIGNS_CVALUES"].ToString();
                        _gc.DrawString(val, Shows.Font, Shows.Brush, _myRa, _formatCenterCenter);
                    }
                }
            }
        }

        /// <summary>
        ///     画尾栏的呼吸项目
        /// </summary>
        private void DrawTailSixTimePerday(string condition, int dayStart, int rowNumber)
        {
            bool blnUp = (dayStart != 0); // 上下错格

            // 画竖线
            for (int i = 0, j = 1; i <= _showDays; i++, j = 1)
            {
                while (j < 6 && i < ShowDaysColumns)
                {
                    float tempStartx = _startX + _leftWidth + (_middleDayWidth * i) + (_middleHourWidth * j);

                    DrawLine_Vertical(_thinBPen1, tempStartx, _endY - _bottomRowHeight * (11 - rowNumber),
                        _endY - _bottomRowHeight * (11 - rowNumber - 2));

                    j++;
                }
            }

            // 获取值
            string filter = " ( " + condition + ")";

            DataRow[] drFind = _patientVitals.Select(filter, "TIME_POINT");

            foreach (DataRow dr in drFind)
            {
                string val = dr["VITAL_SIGNS_CVALUES"].ToString();

                var dtMeasure = (DateTime)dr["TIME_POINT"];

                if (blnUp)
                    // 获取作图区大小, 并清除
                    _myRa = new RectangleF(GetX(dtMeasure) - _middleHourWidth,
                        _endY - _bottomRowHeight * (11 - rowNumber),
                        _middleHourWidth * 3, _bottomRowHeight);
                else
                    // 获取作图区大小, 并清除
                    _myRa = new RectangleF(GetX(dtMeasure) - _middleHourWidth,
                        _endY - _bottomRowHeight * (11 - rowNumber - 1),
                        _middleHourWidth * 3, _bottomRowHeight);

                // 记录断点
                int temp;
                if (val.Length > 0 && int.TryParse(val, out temp) == false)
                {
                    _arrBreakCols.Add(temp);
                }

                blnUp = !blnUp;

                _gc.DrawString(val, Shows.Font, Shows.Brush, _myRa, _formatCenterCenter);
            }
        }


        /// <summary>
        ///     画尾栏的一日一次项目
        /// </summary>
        private void DrawTailStoolPerday(string condition, int dayStart, int rowNumber)
        {
            SizeF szPart = _gc.MeasureString("2E", Shows.Font);

            // 从作图日期开始到 周末
            for (int i = 0; i < _showDays; i++)
            {
                // 获取作图区大小
                _myRa = new RectangleF(_startX + _leftWidth + _middleDayWidth * i
                                       + _middleHourWidth * GetCellIndex(dayStart),
                    _endY - _bottomRowHeight * (11 - rowNumber),
                    _middleDayWidth, _bottomRowHeight);

                // 条件检查
                DateTime dtCurrent = _startDate.AddDays(i);


                // 获取值
                string val;
                dtCurrent = dtCurrent.Date.AddHours(dayStart);

                string filter = "TIME_POINT >= " + SqlManager.SqlConvert(dtCurrent.ToString(ComConst.FMT_DATE.LONG));
                filter += " AND TIME_POINT < " +
                          SqlManager.SqlConvert(dtCurrent.AddDays(1).ToString(ComConst.FMT_DATE.LONG));
                filter += " AND ( " + condition + ")";

                DataRow[] drFind = _patientVitals.Select(filter);
                if (drFind.Length > 0)
                {
                    val = drFind[0]["VITAL_SIGNS_CVALUES"].ToString();
                }
                else
                {
                    continue;
                }

                // 分析数据, 并作图
                if (val.IndexOf(ComConst.STR.COMMA) > 0)
                {
                    string[] parts = val.Split(ComConst.STR.COMMA.ToCharArray());

                    string preCount = string.Empty; // 灌肠前大便次数
                    string stoolCount = string.Empty; // 灌肠次数
                    string nextCount = string.Empty; // 灌肠后大便次数

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

                    if (stoolCount.Equals("1"))
                    {
                        stoolCount = string.Empty;
                    }

                    stoolCount += "E";

                    if (preCount.Length > 0)
                    {
                        var rectRngPre = new RectangleF(_startX + _leftWidth + _middleDayWidth * i
                                                        + _middleHourWidth * GetCellIndex(dayStart),
                            _endY - _bottomRowHeight * (11 - rowNumber),
                            _middleDayWidth, _bottomRowHeight);


                        // 灌肠前大便次数
                        rectRngPre.Y += 2;
                        _gc.DrawString(preCount, Shows.Font, Shows.Brush, rectRngPre, _formatCenterCenter);

                        // 灌肠后大便次数
                        var rectNext = new RectangleF
                        {
                            X = rectRngPre.X + rectRngPre.Width,
                            Y = rectRngPre.Y - 2,
                            Width = szPart.Width,
                            Height = (int)(rectRngPre.Height / 2)
                        };

                        rectNext.Y -= 2;
                        _gc.DrawString(nextCount, Shows.Font, Shows.Brush, rectNext, _formatCenterCenter);

                        // 横线
                        _gc.DrawLine(Pens.Black, new Point((int)(rectNext.X), (int)(rectNext.Y + rectNext.Height)),
                            new Point((int)(rectNext.X + szPart.Width), (int)(rectNext.Y + rectNext.Height)));

                        // 灌肠次数
                        rectNext.Y += rectNext.Height - 1;
                        _gc.DrawString(stoolCount, Shows.Font, Shows.Brush, rectNext, _formatCenterCenter);
                    }
                }
                else
                {
                    //rectRng.Y += 2;
                    _gc.DrawString(val, Shows.Font, Shows.Brush, _myRa, _formatCenterCenter);
                }
            }
        }


        /// <summary>
        ///     对于阳性红色显示
        /// </summary>
        /// <param name="gc"></param>
        /// <param name="rect"></param>
        /// <param name="text"></param>
        private void DrawStringColored(Graphics gc, RectangleF rect, string text)
        {
            string coloredText = string.Empty;

            text = text.Trim();
            text = text.Replace("（", "(");
            text = text.Replace("）", ")");

            int pos = text.IndexOf("(阳性)");
            if (pos >= 0)
            {
                coloredText = "(阳性)";
            }
            else if ((pos = text.IndexOf("(+)")) >= 0)
            {
                coloredText = "(+)";
            }

            if (pos >= 0)
            {
                // 准备
                SizeF sz = gc.MeasureString(text, Shows.Font);
                if (rect.Width < sz.Width)
                {
                    rect.Width = sz.Width;
                }
                else
                {
                    rect.X += (rect.Width - sz.Width) / 2;
                    rect.Width = sz.Width;
                }

                var sf = new StringFormat { Alignment = StringAlignment.Near };

                // 开始
                string pre = text.Substring(0, pos);
                sz = gc.MeasureString(pre, Shows.Font);
                _gc.DrawString(pre, Shows.Font, Shows.Brush, rect, sf);

                // 本体
                rect.X += sz.Width - 4;
                _gc.DrawString(coloredText, Shows.Font, Brushes.Red, rect, sf);
            }
            else
            {
                _gc.DrawString(text, Shows.Font, Shows.Brush, rect, _formatCenterCenter);
            }
        }

        /// <summary>
        ///     画尾栏的一日一次项目
        /// </summary>
        private void DrawTailOneTimePerday(string condition, int dayStart, int rowNumber)
        {
            // 从作图日期开始到 周末
            for (int i = 0; i < _showDays; i++)
            {
                // 获取作图区大小, 并清除
                // RectangleF rectRng = new RectangleF(ptStart, szUnitRng);

                // 条件检查
                DateTime dtCurrent = _startDate.AddDays(i);

                _myRa = new RectangleF(_startX + _leftWidth + _middleDayWidth * i
                                         + _middleHourWidth * GetCellIndex(dayStart),
                   _endY - _bottomRowHeight * (11 - rowNumber),
                   _middleDayWidth, _bottomRowHeight);

                // 获取值
                string val = string.Empty;
                dtCurrent = dtCurrent.Date.AddHours(dayStart);

                string filter = "TIME_POINT >= " + SqlManager.SqlConvert(dtCurrent.ToString(ComConst.FMT_DATE.LONG));
                filter += " AND TIME_POINT < " +
                          SqlManager.SqlConvert(dtCurrent.AddDays(1).ToString(ComConst.FMT_DATE.LONG));
                filter += " AND ( " + condition + ")";

                DataRow[] drFind = _patientVitals.Select(filter);

                for (int c = 0; c < drFind.Length; c++)
                {
                    if (c > 0) val += ComConst.STR.CRLF;
                    val += drFind[c]["VITAL_SIGNS_CVALUES"].ToString();
                }

                if (drFind.Length > 0)
                {
                    // val = drFind[0]["VITAL_SIGNS_CVALUES"].ToString();

                    // 作图
                    DrawStringColored(_gc, _myRa, val);
                    // gc.DrawString(val, fontDraw, brushDraw, rectRng, drawFormat);
                }
            }
        }


        /// <summary>
        ///     居中绘制
        /// </summary>
        /// <param name="s"></param>
        /// <param name="y"></param>
        /// <param name="height"></param>
        /// <param name="brush"></param>
        /// <param name="font"></param>
        private void PaintMiddleCenter(string s, float y, float height, Font font, Brush brush)
        {
            var r = new RectangleF(_startX, y,
                _endX - _startX, height);

            _gc.DrawString(s, font, brush, r, _formatCenterCenter);
        }

        #region 绘制结束

        /// <summary>
        ///     绘制体温单--结束
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="isClear">是否返回高清图片</param>
        /// <returns></returns>
        public string DrawImageEnd(Bitmap bmp, bool isClear)
        {
            ImageFormat imageFormat = ImageFormat.Png;
            string fileName = string.Concat(
                _visitId.ToString("00"), _pageIndex.ToString("_00")) +
                              (isClear ? "C" : string.Empty) + "." + imageFormat;
            // string fileName = patientId + visitId.ToString("_00") + pageIndex.ToString("_00") + ".jpg";

            string mapFilePath = Path.Combine(SavePath, _patientId);
            if (!mapFilePath.Contains(":") && _context != null)
                mapFilePath = _context.Server.MapPath(mapFilePath);
            if (!Directory.Exists(mapFilePath))
            {
                Directory.CreateDirectory(mapFilePath);
            }
            string strpath = Path.Combine(mapFilePath, fileName);
            int ow = bmp.Width;
            int oh = bmp.Height;

            if (isClear)
            {
                try
                {
                    bmp.Save(strpath, imageFormat);
                }
                catch (Exception ex)
                {
                    throw new Exception(strpath + "无法写入", ex);
                }
                //bmp.Dispose();
            }
            else
            {
                var smallBmp = new Bitmap(ow / 2, oh / 2);

                Graphics g = Graphics.FromImage(smallBmp);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.DrawImage(bmp, new Rectangle(0, 0, ow / 2, oh / 2), new Rectangle(0, 0, ow, oh), GraphicsUnit.Pixel);

                smallBmp.SetResolution(1024f, 768f);

                try
                {
                    smallBmp.Save(strpath, imageFormat);
                }
                catch (Exception ex)
                {
                    throw new Exception(strpath + "无法写入", ex);
                }
                smallBmp.Dispose();
                g.Dispose();
            }
            fileName = Path.Combine(_patientId, fileName);
            if (_context == null)
                return Path.Combine(SavePath, fileName);
            return GetImageUrl(Path.Combine(SavePath, fileName));
        }

        #endregion

        #endregion

        #region 公共方法

        /// <summary>
        ///     按比例绘制
        /// </summary>
        /// <param name="l">数值</param>
        /// <returns></returns>
        private int _A(int l)
        {
            double ratio = 3.3;
            ratio *= 2.0;
            return (int)(l * ratio);
        }

        /// <summary>
        ///     按比例绘制
        /// </summary>
        /// <param name="l">数值</param>
        /// <returns></returns>
        private float _A(float l)
        {
            double ratio = 3.3;
            ratio *= 2.0;
            return (float)(l * ratio);
        }

        /// <summary>
        ///     10进制转16进制
        /// </summary>
        /// <param name="i">10进制数</param>
        /// <returns></returns>
        private Int32 Hex10To16(Int32 i)
        {
            ////十进制转二进制
            //Console.WriteLine("十进制166的二进制表示: " + Convert.ToString(166, 2));
            ////十进制转八进制
            //Console.WriteLine("十进制166的八进制表示: " + Convert.ToString(166, 8));
            ////十进制转十六进制
            //Console.WriteLine("十进制166的十六进制表示: " + Convert.ToString(166, 16));
            return Convert.ToInt32(Convert.ToString(i, 16));
        }

        /// <summary>
        ///     16进制转10进制
        /// </summary>
        /// <param name="i">16进制数</param>
        /// <returns></returns>
        private Int32 Hex16To10(Int32 i)
        {
            ////二进制转十进制
            //Console.WriteLine("二进制 111101 的十进制表示: " + Convert.ToInt32("111101", 2));
            ////八进制转十进制
            //Console.WriteLine("八进制 44 的十进制表示: " + Convert.ToInt32("44", 8));
            ////十六进制转十进制
            //Console.WriteLine("十六进制 CC的十进制表示: " + Convert.ToInt32("CC", 16));
            return Convert.ToInt32(i.ToString(), 16);
        }

        /// <summary>
        ///     分隔字符串.(按字符分隔).如 "请假"==> "请","假"
        /// </summary>
        /// <param name="str">入参.字符串</param>
        /// <returns></returns>
        private string[] Split(string str)
        {
            var tempStrings = new string[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                tempStrings[i] = str[i].ToString();
            }
            return tempStrings;
        }

        /// <summary>
        ///     分隔字符串.(按字符分隔).如 "请假"==> "请","假"
        /// </summary>
        /// <param name="str">入参.字符串</param>
        /// <param name="firstDefault">首项默认值</param>
        /// <returns></returns>
        private string[] Split(string str, string firstDefault)
        {
            if (string.IsNullOrEmpty(firstDefault))
                return Split(str);

            var tempStrings = new string[str.Length + 1];

            tempStrings[0] = firstDefault;

            for (int i = 1; i < tempStrings.Length; i++)
            {
                tempStrings[i] = str[i - 1].ToString();
            }
            return tempStrings;
        }


        /// <summary>
        ///     绘制多文本
        /// </summary>
        /// <param name="stringArray">文本集合</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="format">文本布局</param>
        private void DrawString(string[] stringArray, float width, float height, StringFormat format)
        {
            int tempIndex = 0;
            foreach (string str in stringArray)
            {
                if (tempIndex == 0)
                    height = _top + _middleRowHeight;
                else
                    height += tempIndex * _middleRowHeight;

                _myRa = new RectangleF(width, height,
                    (_middleHourWidth) * (tempIndex == 0 ? 2 : 1),
                    _middleRowHeight);
                _gc.DrawString(str, _fontChn8, _brushBlack, _myRa, format);

                tempIndex += 2;
            }
        }

        /// <summary>
        ///     绘制多文本
        /// </summary>
        /// <param name="stringArray">文本集合</param>
        /// <param name="intArray">高度增加的数值</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="format">文本布局</param>
        private void DrawString(string[] stringArray, int[] intArray, float width, float height, StringFormat format)
        {
            height = _top + _middleRowHeight;

            int j = 0;

            for (int i = 0; i < stringArray.Length; i++)
            {
                if (i > 0 || stringArray.Length == intArray.Length)
                {
                    height += intArray[j] * _middleRowHeight;
                    j++;
                }
                _myRa = new RectangleF(width, height,
                    _middleHourWidth,
                    _middleRowHeight);
                _gc.DrawString(stringArray[i], _fontChn8, _brushBlack, _myRa, format);
            }
        }

        /// <summary>
        ///     根据当前时间获取操作的单元格(结果值[0~5])
        /// </summary>
        /// <param name="myHour"></param>
        /// <returns></returns>
        private int GetCellIndex(int myHour)
        {
            return (myHour - HourStart) / HourInterval;
        }

        /**/

        /// <summary>
        ///     读取Xml文件信息,并转换成DataTable对象
        /// </summary>
        /// <param name="xmlFilePath">xml文江路径</param>
        /// <returns>DataTable对象</returns>
        public static DataTable XmlToDataTable(string xmlFilePath)
        {
            if (!string.IsNullOrEmpty(xmlFilePath))
            {
                string path = Path.GetFullPath(xmlFilePath);

                var dt = new DataTable();

                if (File.Exists(path + ".Schema"))
                {
                    dt.ReadXmlSchema(path + ".Schema");
                }
                dt.ReadXml(path);
                return dt;
            }
            return null;
        }

        /// <summary>
        ///     传入文件所在服务器相对路径,获取文件URL路径
        /// </summary>
        /// <param name="filePath">文件相对服务器路径</param>
        /// <returns>文件URL</returns>
        private string GetImageUrl(string filePath)
        {
            Uri url = _context.Request.Url;
            //string strUrl = url.AbsoluteUri.Replace(url.AbsolutePath, @"/");
            string strUrl = url.AbsoluteUri.Remove(
                url.AbsoluteUri.IndexOf(
                    _context.Request.AppRelativeCurrentExecutionFilePath.Replace(@"~/", string.Empty)));

            if (!strUrl.StartsWith("http"))
            {
                url = _context.Request.Url;
                strUrl = url.AbsoluteUri.Replace(url.AbsolutePath, @"/");
            }
            return strUrl + filePath.Replace(@"\", @"/");
        }

        /// <summary>
        ///     将DataTable转换为List..T
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="dt">表名</param>
        /// <returns>实体列表</returns>
        public static List<T> ConvertToList<T>(DataTable dt)
        {
            if (dt == null)
                return null;
            var list = new List<T>();
            //使用default关键字，T为空时返回：
            //1、如果T为引用类型，则t=null
            //2、如果T为值类型，则t=0
            //3、如果T为结构类型，则返回所有成员为0或空的结构
            //定义属性集
            List<PropertyInfo> properies = null;
            string strColumnName = string.Empty;

            foreach (DataRow dr in dt.Rows)
            {
                var t = Activator.CreateInstance<T>();
                //取得类中的所有属性
                properies = t.GetType().GetProperties().ToList();

                foreach (DataColumn dc in dt.Columns)
                {
                    string columnName = Regex.Replace(dc.ColumnName.ToLower(), @"(^[a-zA-Z0-9])|([_](\w))",
                        m => m.Value.ToUpper().Replace("_", ""));

                    PropertyInfo pro = properies.Find(p => p.Name.ToUpper() == columnName.ToUpper());

                    if (pro != null && !(dr[dc.ColumnName] is DBNull))
                        pro.SetValue(t, dr[dc.ColumnName], null);
                }
                list.Add(t);
            }

            return list;
        }

        #endregion

        #region 画图标.画线.

        /// <summary>
        ///     以点画图标.
        /// </summary>
        /// <param name="center">图标中心点</param>
        /// <param name="flag">图标类别</param>
        private void Draw(PointF center, string flag)
        {
            switch (flag)
            {
                case IconCircleSolid:
                    DrawO_Solid(center);
                    break;
                case IconCircleSolidBlue:
                    DrawO_Solid_Blue(center);
                    break;
                case IconCircleHollow:
                    DrawO_Hollow(center);
                    break;
                case IconPick:
                    DrawX(center);
                    break;
                case PhysicalCoolT:
                    DrawRedX(center);
                    break;
                case IconBox:
                    Draw_Box(center);
                    break;
                case IconBreathMachine:
                    DrawR_BreathMachine(center);
                    break;
            }
        }

        /// <summary>
        ///     画圆.实心圆.
        /// </summary>
        /// <param name="Center">圆心</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        private void DrawO_Solid(PointF Center)
        {
            Center.X = Center.X - _middleHourWidth / 2;
            Center.Y = Center.Y - _middleRowHeight / 2;
            var width = (float)(((double)_middleDayWidth) / 12);
            float height = 3 * _middleRowHeight;
            var newWidth = (float)(((double)_middleDayWidth) / 8);
            float newHeight = 3 * _middleRowHeight;

            _myRa = new RectangleF(
                Center.X - (width / 2) + (newWidth / 2),
                Center.Y - (height / 2) + (newHeight / 2),
                newWidth, newHeight);

            //this.myRA = new RectangleF(
            // Center.X, Center.Y, MiddleHourWidth, MiddleRowHeight);
            _gc.DrawString("●", _fontChn9, _brushRed, _myRa, _formatCenterLeft);
        }

        /// <summary>
        ///  画呼吸机
        /// </summary>
        /// <param name="Center"></param>
        private void DrawR_BreathMachine(PointF center)
        {
            center.X = center.X - _middleHourWidth / 2;
            center.Y = center.Y - _middleRowHeight / 2;

            var width = (float)(((double)_middleDayWidth) / 12);
            float height = 3 * _middleRowHeight;
            var newWidth = (float)(((double)_middleDayWidth) / 8);
            float newHeight = 3 * _middleRowHeight;

            _myRa = new RectangleF(
                center.X - (width / 2) + (newWidth / 2),
                center.Y - (height / 2) + (newHeight / 2),
                newWidth, newHeight);

            //this.myRA = new RectangleF(
            //  Center.X, Center.Y, MiddleHourWidth, MiddleRowHeight);
            _gc.DrawString("®", _fontChn9, _brushBlue, _myRa, _formatCenterLeft);
        }

        /// <summary>
        ///     画圆.实心圆.
        /// </summary>
        /// <param name="center">圆心</param>
        private void DrawO_Solid_Blue(PointF center)
        {
            center.X = center.X - _middleHourWidth / 2;
            center.Y = center.Y - _middleRowHeight / 2;

            var width = (float)(((double)_middleDayWidth) / 12);
            float height = 3 * _middleRowHeight;
            var newWidth = (float)(((double)_middleDayWidth) / 8);
            float newHeight = 3 * _middleRowHeight;

            _myRa = new RectangleF(
                center.X - (width / 2) + (newWidth / 2),
                center.Y - (height / 2) + (newHeight / 2),
                newWidth, newHeight);

            //this.myRA = new RectangleF(
            //  Center.X, Center.Y, MiddleHourWidth, MiddleRowHeight);
            _gc.DrawString("●", _fontChn9, _brushBlue, _myRa, _formatCenterLeft);
        }

        /// <summary>
        ///     画方框.
        /// </summary>
        /// <param name="center">圆心</param>
        private void Draw_Box(PointF center)
        {
            var width = (float)(((double)_middleDayWidth) / 12);
            float height = 3 * _middleRowHeight;
            var newWidth = (float)(((double)_middleDayWidth) / 8);
            float newHeight = 3 * _middleRowHeight;

            _myRa = new RectangleF(
                center.X - (width / 2) + (newWidth / 2),
                center.Y - (height / 2) + (newHeight / 2),
                newWidth, newHeight);
            // this.myRA = new RectangleF(Center.X, Center.Y, (float)(((double)this.MiddleDayWidth) / 12), (float)(3 * this.MiddleRowHeight));          
            _gc.DrawString("■", _fontChn9, _brushRed, _myRa, _formatCenterLeft);
        }

        /// <summary>
        ///     画圆○.空心圆
        /// </summary>
        /// <param name="center">圆心</param>
        private void DrawO_Hollow(PointF center)
        {
            center.X = center.X - _middleHourWidth / 2;
            center.Y = center.Y - _middleRowHeight / 2;
            // float l = this._A((float)1.5f);            
            float l = _A(0.8f);
            var cirRec = new RectangleF(center.X + l, center.Y + l, 3f * l, 3f * l);
            _gc.DrawEllipse(_boldRedPen, cirRec);
        }

        /// <summary>
        ///     画叉.
        /// </summary>
        /// <param name="center">中心</param>
        private void DrawX(PointF center)
        {
            float l = _A(1f);
            var p1 = new PointF();
            var p2 = new PointF();
            p1.X = center.X - l;
            p1.Y = center.Y - l;
            p2.X = center.X + l;
            p2.Y = center.Y + l;
            _gc.DrawLine(_boldBluePen, p1, p2);
            p1.X = center.X - l;
            p1.Y = center.Y + l;
            p2.X = center.X + l;
            p2.Y = center.Y - l;
            _gc.DrawLine(_boldBluePen, p1, p2);
        }

        /// <summary>
        ///     画叉.
        /// </summary>
        /// <param name="center">中心</param>
        private void DrawRedX(PointF center)
        {
            float l = _A(1f);
            var p1 = new PointF();
            var p2 = new PointF();
            p1.X = center.X - l;
            p1.Y = center.Y - l;
            p2.X = center.X + l;
            p2.Y = center.Y + l;
            _gc.DrawLine(_boldRedPen, p1, p2);
            p1.X = center.X - l;
            p1.Y = center.Y + l;
            p2.X = center.X + l;
            p2.Y = center.Y - l;
            _gc.DrawLine(_boldRedPen, p1, p2);
        }

        /// <summary>
        ///     画横线
        /// </summary>
        /// <param name="pen">画笔</param>
        /// <param name="x1">第一个点的x坐标</param>
        /// <param name="x2">第二个点的x坐标</param>
        /// <param name="y">两点的y坐标</param>
        private void DrawLine_Horizontal(Pen pen, float x1, float x2, float y)
        {
            _gc.DrawLine(pen, x1, y, x2, y);
        }

        /// <summary>
        ///     画竖线
        /// </summary>
        /// <param name="pen">画笔</param>
        /// <param name="x">两点的x坐标</param>
        /// <param name="y1">第一个点的y坐标</param>
        /// <param name="y2">第二个点的y坐标</param>
        private void DrawLine_Vertical(Pen pen, float x, float y1, float y2)
        {
            _gc.DrawLine(pen, x, y1, x, y2);
        }

        #endregion 画图标.画线.

        #region 绘制特殊情况下的提示信息

        /// <summary>
        ///     绘制特殊情况下的提示信息
        /// </summary>
        /// <param name="item">项目</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="value">值</param>
        private bool DrawSpec(string item, float width, float height, ref double value)
        {
            if (_list == null || _list.Count == 0) return false;

            var iTmp = (int)value;
            double d = value;

            DrawSpec entity = _list.Find(p => p.Item == item && p.Value == d);

            // DrawSpec entity = list.Find(p => p.Item == "脉搏");

            if (entity != null)
            {
                char[] charArray = entity.Tips.ToCharArray();

                charArray.ToString();

                // string[] tempStrings = Split(entity.Tips, iTmp.ToString());

                string[] tempStrings = Split(entity.Tips);

                value = entity.DefaultValue;

                if (string.IsNullOrEmpty(entity.Position) || entity.Position == "2,4")

                    DrawString(tempStrings, width, height, _formatLeftCenter);
                else
                {
                    string[] strArray = entity.Position.Split(new[] { ',' });
                    var intArray = new int[strArray.Length];
                    int i = 0;
                    foreach (string str in strArray)
                    {
                        int.TryParse(str, out intArray[i]);
                        i++;
                    }
                    DrawString(tempStrings, intArray, width, height, _formatLeftCenter);
                }
                return true;
            }
            return false;
        }

        #endregion
    }

    #region 辅助类

    #region 特殊信息类

    /// <summary>
    ///绘制时的特殊值信息
    /// </summary>
    public class DrawSpec
    {
        public DrawSpec()
        {
        }

        /// <summary>
        /// 绘制时的特殊值信息
        /// </summary>
        /// <param name="item">项目</param>
        /// <param name="value">项目值</param>
        /// <param name="tips">提示信息</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="position">位置.以逗号分隔的坐标增量</param>
        public DrawSpec(string item, double value, string tips, double defaultValue, string position)
        {
            Item = item;
            Value = value;
            Tips = tips;
            DefaultValue = defaultValue;
            Position = position;
        }

        /// <summary>
        ///     项目
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        ///     项目值
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        ///     提示信息
        /// </summary>
        public string Tips { get; set; }

        /// <summary>
        ///     默认值
        /// </summary>
        public double DefaultValue { get; set; }

        /// <summary>
        ///     位置.以逗号分隔的坐标增量
        /// </summary>
        public string Position { get; set; }
    }

    #endregion

    #region 体温类

    /// <summary>
    ///     体温类
    /// </summary>
    public class Temperature
    {
        /// <summary>
        ///     当前体温
        /// </summary>
        public PointF CurrentT { get; set; }

        /// <summary>
        ///     前一体温
        /// </summary>
        public PointF PrevT { get; set; }

        /// <summary>
        ///     是否为体温列
        /// </summary>
        public bool IsT { get; set; }
    }

    #endregion

    #region 心率类

    /// <summary>
    ///     心率类
    /// </summary>
    public class HeartRate
    {
        /// <summary>
        ///     当前心率
        /// </summary>
        public PointF CurrentHR { get; set; }

        /// <summary>
        ///     前一心率
        /// </summary>
        public PointF PrevHR { get; set; }

        /// <summary>
        ///     是否为心率列
        /// </summary>
        public bool IsHR { get; set; }
    }

    #endregion

    #region 脉搏类

    /// <summary>
    ///     脉搏类
    /// </summary>
    public class Pulse
    {
        /// <summary>
        ///     当前脉搏
        /// </summary>
        public PointF CurrentP { get; set; }

        /// <summary>
        ///     前一脉搏
        /// </summary>
        public PointF PrevP { get; set; }

        /// <summary>
        ///     是否为脉搏列
        /// </summary>
        public bool IsP { get; set; }
    }

    #endregion

    /// <summary>
    ///     显示类. 提供显示的字体及其大小和颜色处理
    /// </summary>
    public class Shows
    {
        /// <summary>
        ///     字体
        /// </summary>
        public Font Font { get; private set; }

        /// <summary>
        ///     颜色
        /// </summary>
        public Brush Brush { get; private set; }

        public void Init(string fontName, string fontSize, string brushColor)
        {
            float size;
            int color;
            if ((float.TryParse(fontSize, out size) == false) ||
                (int.TryParse(brushColor, out color) == false)) throw new Exception("无效字体设置.请检查后重试.");

            if (fontName != null) Font = new Font(fontName, size);
            Brush = GetBrushFromColor(color);
        }

        /// <summary>
        ///     获取画刷
        /// </summary>
        /// <param name="color">画刷代码: 0:黑, 1:红 2: 蓝</param>
        /// <returns></returns>
        private static Brush GetBrushFromColor(int color)
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
    }

    #endregion
}