//------------------------------------------------------------------------------------
//
//  系统名称        : 通用
//  子系统名称      : 通用模块
//  对象类型        : 
//  类名            : ZebraPrinter.cs
//  功能概要        : 斑马打印机的接口
//  作成者          : 付军
//  作成日          : 2007-01-19
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace HISPlus
{
    /// <summary>
	/// ZebraPrinter 的摘要说明。
	/// </summary>
    public class ZebraPrinter
    {
        // 控制类
        static int PRINT_NORMAL             = 1000;                     // 正常打印
        static int PRINT_ERR_PARA           = 1001;                     // 参数错误
        static int PRINT_ERR_OVERFLOW       = 1002;                     // 数据信息量过大

        // 通讯类
        static int COMM_ERR_PORT_OPEN       = 2001;                     // 打开通讯端口错或者未联接打印机
        static int COMM_ERR_PORT_WRITE      = 2002;                     // 写端口错误
        static int COMM_ERR_PORT_CLOSE      = 2003;                     // 关闭通讯端口错

        // 文件错误
        static int FILE_ERR_OPEN            = 3001;                     // 文件打开错误
        static int FILE_ERR_READ            = 3002;                     // 读文件错误
        static int FILE_ERR_WRITE           = 3003;                     // 写文件错误
        static int FILE_ERR_FORMAT          = 3004;                     // 文件类型或格式不对

        // 打印机错误
        static int PRINTER_ERR_NO_PAPER     = 4001;                     // 打印机缺纸
        static int PRINTER_ERR_NO_CARBON    = 4002;                     // 打印机缺碳带
        static int PRINTER_ERR_HEAD_UP      = 4003;                     // 打印头抬起
        static int PRINTER_ERR_CUTTER       = 4004;                     // 切刀错误

        
        // 打开通讯端口函数
        // 参数:	
        //      Long Port       打开通讯端口（1-并口一;2-并口二;3-串口一;4-串口二）
        //      Long BaudSpeed  串口速率(9600,19200,38400) 
        // 返回值:  
        //      0   正常返回
        //      -1  打开端口出错
        // 说明	用异步方式打开端口端口，设置延迟，当通讯端口为并口方式时，通讯速率不起作用. 
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PortOpen(Int32 Port, Int32 BaudSpeed);


        // 关闭通讯端口函数
        // 返回值
        //      0   正常返回
        //     -1   关闭端口出错
        // 说明	打印机参数设置类
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PortClose();


        // 设置打印机串口速率函数
        // 参数：	Long BaudSpeed		设置串口的数率值(9600,19200,38400) 
        // 返回值	0 成功
        //    -1 失败
        // 说明	只能通过并口来设置打印机的串口速率，设置值可为：9600,19200,38400 
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetPrinterComSpeed(Int32 BaudSpeed);


        // 设置打印机精度函数
        // 参数：	Long Dots	设置打印机精度（203 或300）
        // 返回值	0 成功
        //    -1 失败
        // 说明	203 是指203.2dpi 精度的打印机（8dots/mm），300 是指300dpi精度的打印机（11.81dots/mm）;如果不设置打印机精度，则默
        // 认为203.2dpi 打印机。
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetPrinterDots(Int32 Dots);


        // 设置打印机打印黑度函数
        // 参数：	Long DarkValue	设置打印的黑度值
        // 返回值	0 成功
        //    -1 失败
        // 说明	打印黑度为1-15 的整型值，打印机默认为10 
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetDarkness(Int32 DarkValue);


        // 设置打印机的打印速率函数
        // 参数	Long PrintRate	打印速率
        // 返回值	0 成功
        //    -1 失败
        // 说明	打印速率的参数值为1-10 的整型值，C4，Pd4 打印机默认为2 最大为3 
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetPrintSpeed(Int32 PrintRate);


        // 设置打印机的坐标圆点函数
        // 参数	Long iX	打印区域水平起始位置
        //    Long iY	打印区域垂直起始位置
        // 返回值	0 成功
        //    -1 失败
        // 说明	打印起始位置要根据实际标签尺寸来确定，坐标以毫米为单位，
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetPrintXY(Int32 iX, Int32 iY);


        // 设置打印机打印方向旋转180 度函数
        // 参数	 Long iDirect旋转标志（0-正常打印，1-旋转180 度）
        // 返回值	0 成功
        //    -1 失败
        // 说明	通过设置可使打印信息旋转１８０度的方向。
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetPrintOrientation(Int32 iDirect);


        // 设置打印机每次打印是否都要回退函数
        // 参数	Long iBack	回退标志（0-回退（默认），1-不回退）
        // 返回值	0 成功
        //    -1 失败
        // 说明	该函数主要用于设置切刀时使用
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetPrintBack(Int32 iBack);


        // 设置打印机打印每张标签完后的吐纸尺寸函数
        // 参数	Long iFeed吐纸长度（取值范围0-30） 
        // 返回值	0 成功
        //    -1 失败
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetPrintFeedDim(Int32 iFeed);


        // 设置标签纸的尺寸函数
        // 参数	 Long iflag	纸张类型(0-缝隙纸,1-黑标纸) 
        //     Long iWidth	设置标签的宽度(0mm-150mm，若为-1 则忽略宽度让置) 
        //     Long iHigh	设置标签的高度（0-150, 若为-1 则忽略高度设置和缝隙设置）
        //     Long iGap      设置标签的缝隙宽度（或黑标的高度）
        // 返回值	0 成功
        //    -1 失败
        // 说明	可用于设置缝隙纸，黑标纸，连续纸，以毫米为单位,iWidth 和iHigh 不能同时为-1
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetPaperSize(Int32 iflag, Int32 iWidth, Int32 iHigh, Int32 iGap);


        // 清除打印机缓存函数
        // 返回值
        //      0 成功
        //      -1 失败	
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetClear();


        // 初始化打印机，即可设置打印机黑度，打印速率，出纸模式函数
        // 参数	Long PrintRate	打印速率
        //    Long DarkNess	打印黑度
        //    Long PrintMode	出纸模式(1,回卷;0, 不回卷) 
        // 返回值	0 成功
        //    -1 失败
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetInitialize(Int32 PrintRate, Int32 DarkNess, Int32 PrintMode);


        // 错误信息函数
        // 返回值	返回错误号及错误提示信息
        [DllImport("zebracom.dll")]
        public static extern string VS_GetLastError();


        // 下载图片到打印机内存中函数
        // 参数		char PicturePath	预下载的图片路径
        //        Char AimName		下载到打印中目标文件名
        // 返回值	0 成功
        //    -1 失败
        // 说明	Intermec C4,Intermec Pd4 打印机只能下载PCX 文件
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_DownPicture(string PicturePath, string AimName);


        //参数	Long oXprint	输出字符的水平位置（X 轴坐标）
        //    Long oYprint	输出字符的垂直位置（Y 轴坐标）
        //    Long YNRotation	字符是否旋转（0－不;1－90;2-180;3-270;）
        //    Long FontStyle	字体类型（1—7 代表不同的数字，字母;8 为亚州字符） 
        //    Long oXMult	字符的水平宽度（1-8）
        //    Long oYMult	字符的垂直宽度（1-10） 
        //    Long YNImage	字符是否有背景(0—无，1—有) 
        //    Char Text	输出字符信息
        //返回值	0 成功
        //    -1 失败
        //说明	可以是数字字符，阿拉伯字母或中文字符
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PrintCharacter(Int32 oXprint, Int32 oYprint, Int32 YNRotation, Int32 FontStyle, Int32 oXMult, Int32 oYMult, Int32 YNImage, string Text);


        //参数	Long oXprint	输出字符的水平位置（Ｘ轴坐标）
        //    Long oYprint	输出字符的垂直位置（Ｙ轴坐标）
        //    Long FontName	字体名称（0-黑体;1-宋体,2-楷体,3隶书） 
        //    Long FontStyle	字体类型（0-正常，1-倾斜，2-加粗，3-倾斜加粗，4-带下划线，5-下划线倾斜，6-下划线加粗，7-下划线加粗
        //倾斜） 
        //    Long FontSize	字体大小（0-100）
        //    Long YNRotation	字符是否旋转（0－不旋转;1－180 度）
        //    Char Text	输出字符信息
        //返回值	0 成功
        //    -1 失败
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PrintChineseStr(Int32 oXprint, Int32 oYprint, Int32 FontName, Int32 FontStyle, Int32 FontSize, Int32 YNRotation, string Text);


        //打印条形码函数
        //参数	Long oXprint	输出字符的水平位置（X 轴坐标）
        //    Long oYprint	输出字符的垂直位置（Y 轴坐标）
        //    Long YNRotation	字符是否旋转（0－不;1－90;2-180;3-270） 
        //    Long BarStyle	条码类型（1-- Code128;2--Interleaved 2 of 5;3--Code 39 std. or extended 3;4--ean13;5-Code93;6--ucc/ean128;7--upcA8---upcE;)） 
        //    Long BarNarrow	条码精度(1-10) 
        //    Long BarWidth	条码宽度(1-15) 
        //    Long BarHigh	条码高度(1-100) 
        //    Long YNHuman	是否打印可识读信息(1-打印;0-不打印) 
        //    Char BarStr	条码信息
        //返回值	0 成功
        //    -1 失败
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PrintBarcode1(Int32 oXprint, Int32 oYprint, Int32 YNRotation, Int32 BarStyle, Int32 BarNarrow, Int32 BarWidth, Int32 BarHigh, Int32 YNHuman, string BarStr);


        // 打印直线函数
        // 参数	Long oXStart	起始点X 轴坐标
        //    Long oYStart	起始点Y 轴坐标
        //    Long HoriWidth	水平宽度(0mm---150mm) 
        //    Long VertWidth	垂直宽度(0mm---150mm) 
        // 返回值	0 成功
        //    -1 失败
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PrintLine(Int32 oXStart, Int32 oYStart, Int32 HoriWidth, Int32 VertWidth);


        //画一个长方形图框函数
        //参数	Long oXStart	起始点X 轴坐标
        //    Long oYStart	起始点Y 轴坐标
        //    Long oXEnd	终止点X 轴坐标
        //    Long oYEnd	终止点Y 轴坐标
        //    Long LineDots	方框线的粗细 
        //返回值	0 成功
        //    -1 失败
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PrintBox(Int32 oXStart, Int32 oYStart, Int32 oXEnd, Int32 oYEnd, Int32 LineDots);


        // 打印存在打印机内存中的图片信息函数
        // 参数	Long oXprint	输出字符的水平位置（X 轴坐标）
        //    Long oYprint	输出字符的垂直位置（Y 轴坐标）
        //    Char GrapName	图片名称
        // 返回值	0 成功
        //    -1 失败
        // 说明图片必须预先下载到打印机内存中，C4 打印机只能打印PCX 格式的图片 
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PrintMemGraphics(Int32 oXprint, Int32 oYprint, string GrapName);


        //打印二进制字符信息函数
        //参数	Long oXprint	输出字符的水平位置（X 轴坐标）
        //    Long oYprint	输出字符的垂直位置（Y 轴坐标）
        //    Long XWidth	字符信息的水平宽度（以字节为单位）
        //    Long YHigh	字符信息的垂直高度（以点为单位）
        //    Char Text	二进制字符信息
        //返回值	0 成功
        //    -1 失败
        //说明	如二进制图片信息;如果Text 为“##”，则表示不写入字符信息,只写入命令信息; 
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PrintBinary(Int32 oXprint, Int32 oYprint, Int32 XWidth, Int32 YHigh, string Text);


        //打印Bmp 格式的图片信息函数
        //参数	Long oXprint	输出字符的水平位置（X 轴坐标）
        //    Long oYprint	输出字符的垂直位置（Y 轴坐标）
        //    Char GrapName	图片名称 
        //返回值	0 成功
        //    -1 失败
        //说明	图片（.BMP 位图文件）不需要预先下载到计算机中,图片名称为存在于计算机中的绝对路径。
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PrintBmp(Int32 oXprint, Int32 oYprint, string GrapName);


        //驱动打印机打印数据函数
        //参数	Long sumLabel	要打印的标签份数（值为1-5000） 
        //返回值	0 成功
        //    -1 失败
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_Print(Int32 sumLabel);


        /// <summary>
        /// 通过端口号获取端口名称
        /// </summary>
        /// <param name="portNo"></param>
        /// <returns></returns>
        public static string GetPortName(int portNo)
        {
            switch (portNo)
            {
                case 1: 
                    return "LPT1"; 
                
                case 2: 
                    return "LPT2"; 
                
                case 3:                     
                    return "COM1"; 
                
                case 4: 
                    return "COM2"; 
                
                default: 
                    throw new Exception("错误的打印端口号!");
            }
        }


        /// <summary>
        /// 获取MaxiCode条码打印命令
        /// </summary>
        /// <returns></returns>
        public static string GetMaxicodePrintCmd(int x, int y, string barcode)
        { 
            char    charDouble  = '"';
            char    charBlank   = ' ';
            string  strDouble   = new string(charDouble, 1);
            string  strBlank    = new string(charBlank, 50);

            return "b" + x.ToString() + "," + y.ToString() 
                 + ",M,m4," + strDouble + barcode + strDouble + ComConst.STR.CRLF
                 + "P1" + ComConst.STR.CRLF;
        }
    }
}
