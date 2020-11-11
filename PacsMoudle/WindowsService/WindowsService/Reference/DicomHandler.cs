using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dicom;
using Dicom.Imaging;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Threading;
using Dicom.IO;
using System.Windows.Forms;

namespace PACSMonitor
{
    /// <summary>
    /// 生成单个病人的PACS影像图片
    /// </summary>
    public class Dicom
    {
        /// <summary>
        /// PACS文件网络映射目录
        /// </summary>
        private string pacsRemotePath = Common.GetConfigValue("pacsRemotePath");
        DicomHandler handler;
        string fileName = "";
        BinaryReader dicomFile;//dicom文件流

        /// <summary>
        /// 医院名称
        /// </summary>
        private string hospitalName = string.Empty;

        /// <summary>
        /// 图片格式
        /// </summary>
        private ImageFormat imageFormat = ImageFormat.Png;

        private DataSet dataSet = new DataSet();

        private DataTable dataTable = new DataTable();

        //        private string SelectString =
        //            @"SELECT *
        //                  FROM MOBILEHIS.PACS_FILE
        //                 WHERE DECODE(STATUS, '1', '1') = '1'   
        //                   FOR UPDATE SKIP LOCKED";

        /// <summary>
        /// 查询状态是1的前100条记录
        /// </summary>
        private string SelectString =
            @"SELECT *
                  FROM MOBILEHIS.PACS_FILE
                 WHERE DECODE(STATUS, '1', '1') = '1' AND ROWNUM<=100";
        /// <summary>
        /// 查询状态是1的所有记录
        /// </summary>
        private string SelectString1 =
       @"SELECT *
                  FROM MOBILEHIS.PACS_FILE
                 WHERE DECODE(STATUS, '1', '1') = '1' ";

        /// <summary>
        /// 初始化Dicom实例
        /// </summary>
        /// <param name="tempdataSet">Dicom文件/文件夹路径的数据行集合</param>
        public Dicom()
        {
            int count = 0;
            while (true)
            {
                try
                {
                    dataSet = DB.OracleHelper.ExecuteDataset(SelectString);
                    if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {
                        dataTable = dataSet.Tables[0];
                        
                        Init();

                        DB.OracleHelper.UpdateDataset(SelectString1, dataTable);

                        Common.WriteLog("操作记录数：" + dataTable.Rows.Count);

                        count = 0;
                    }
                    else
                    {
                        Common.WriteLog("暂无数据。");

                        // 如果没有数据，就休息count个10秒，一直累加。
                        count++;
                        // 如果现在是8点到18点之间，则最大休眠时间为60个10秒，即10分钟。
                        if (DateTime.Now.Hour > 8 && DateTime.Now.Hour < 18)
                        {
                            count = Math.Min(6 * 10, count);
                        }
                        Thread.Sleep(1000 * 10 * count);
                    }
                }
                catch (Exception ex)
                {
                    Common.WriteLog(ex.Message);
                }
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            //int _drNumber = -1;  //记录当前执行到的行数

            try
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    //_drNumber++;
                    // 生成开始
                    dr["Generate_Start_date"] = DateTime.Now;
                    dr["status"] = 2;
                    string patientId = dr["PATIENT_ID"].ToString();
                    string examNo = dr["EXAM_NO"].ToString();
                    string filePath = dr["FILE_PATH"].ToString();
                    string SeriesNo = dr["Series_NO"].ToString();
                    string ImageNo = dr["Image_No"].ToString();

                    //DICOM原文件路径
                    string dicomPath = fileName = pacsRemotePath + dr["FILE_PATH"].ToString();
                    //PACS图片保存路径
                    string saveRootPath = Common.PacsSavePath + @"\" + patientId + @"\" + examNo + @"\" + SeriesNo;

                    string saveFullPath = saveRootPath + @"\" + ImageNo + "." + imageFormat.ToString();

                    //PACS图片对应的HTTP地址
                    string dicomFolder = Common.PacsSavePath.Substring(Common.PacsSavePath.LastIndexOf(@"\") + 1);
                    string httpPath = patientId + @"/" + examNo + @"/" + SeriesNo + @"/" + ImageNo + "." + imageFormat.ToString();



                    dr["Url"] = httpPath;
                    if (fileName == string.Empty)
                        return;
                    dicomFile = new BinaryReader(File.OpenRead(fileName));

                    //跳过128字节导言部分
                    dicomFile.BaseStream.Seek(128, SeekOrigin.Begin);
                    if (new string(dicomFile.ReadChars(4)) != "DICM")
                    {
                        Common.WriteLog("没有dicom标识头，文件格式错误");
                        break;
                    }

                    try
                    {
                        // 不存在时创建
                        if (!Directory.Exists(saveRootPath))
                        {
                            Directory.CreateDirectory(saveRootPath);
                            Common.WriteLog("INFO：病人ID：" + patientId + "  检查号：" + examNo + Environment.NewLine
                                                + "分组号：" + SeriesNo + " 生成中。。。");
                        }
                        if (!File.Exists(saveFullPath))
                        {
                            if (!File.Exists(dicomPath))
                            {
                                dicomPath = GetLikeFilePath(dicomPath);

                                if (string.IsNullOrEmpty(dicomPath))
                                {
                                    Common.WriteLog("ERROR：DICOM文件未找到：" + dicomPath + Environment.NewLine
                                                        + "病人ID：" + patientId + "  检查号：" + examNo + Environment.NewLine
                                                        + "分组号：" + SeriesNo + "  图片编号：" + ImageNo);
                                    dr["status"] = 3;
                                    dr["errormsg"] = "ERROR：DICOM文件未找到：" + dicomPath;
                                    continue;
                                }
                            }

                            handler = new DicomHandler(fileName);
                            handler.readAndShow();

                            if (handler == null || handler.gdiImg == null)
                                return;
                            handler.saveAs(saveFullPath);

                            Object imageLock = new Object();
                            lock (imageLock)
                            {
                                #region test

                                //DicomFile dicomFile = DicomFile.Open(dicomPath);
                                //DicomDataset dcmDataSet = dicomFile.Dataset;
                                //String patientName = dcmDataSet.Get<String>(DicomTag.PatientName);
                                //var dcmImage = new DicomImage(dcmDataSet);//可以增加第二个参数来指定获取第几帧
                                //DicomImage dcmImage = new DicomImage(dcmDataSet);
                                #endregion

                                DicomImage dcmImage = new DicomImage(dicomPath);
                                using (Image image = dcmImage.RenderImage())
                                {
                                    //2048x1536 
                                    if (!File.Exists(saveFullPath))
                                    {
                                        if (image.Width > 1024)
                                        {
                                            #region test
                                            //Bitmap bmp = new Bitmap(image);
                                            //Bitmap bmp2 = new Bitmap(1024, 768, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                                            //Graphics draw = Graphics.FromImage(bmp2);
                                            //draw.DrawImage(bmp, 0, 0);
                                            //bmp2.Save(saveFullPath, imageFormat);
                                            //draw.Dispose();
                                            //bmp.Dispose();
                                            #endregion

                                            Bitmap originBmp = new Bitmap(image);
                                            int w = 1024;
                                            int h = 768;
                                            Bitmap resizedBmp = new Bitmap(w, h);
                                            Graphics g = Graphics.FromImage(resizedBmp);
                                            //设置高质量插值法   
                                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                                            //设置高质量,低速度呈现平滑程度   
                                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                            //消除锯齿 
                                            g.SmoothingMode = SmoothingMode.AntiAlias;
                                            g.DrawImage(originBmp, new Rectangle(0, 0, w, h),
                                                new Rectangle(0, 0, originBmp.Width, originBmp.Height), GraphicsUnit.Pixel);
                                            resizedBmp.Save(saveFullPath, imageFormat);
                                            g.Dispose();
                                            resizedBmp.Dispose();
                                            originBmp.Dispose();
                                        }
                                        else
                                            image.Save(saveFullPath, imageFormat);
                                    }
                                    image.Dispose();
                                }
                            }
                        }
                    }
                    catch (DicomIoException ex)
                    {
                        //dr["status"] = 3;
                        //dr["errormsg"] = ex.Message.Substring(0, Math.Min(ex.Message.Length, 2000));
                        //dr["Generate_end_date"] = DateTime.Now;

                        //日子
                        Common.WriteLog(saveFullPath + "图片保存失败." + Environment.NewLine +
                                                "病人ID：" + patientId + "  检查号：" + examNo + Environment.NewLine
                                                + "分组号：" + SeriesNo + "  图片编号：" + ImageNo + Environment.NewLine
                                                + ex.Message);

                    }

                }
            }
            catch (Exception ex)
            {
                //DataRow dr = dataTable.Rows[_drNumber];
                //dr["status"] = "3";
                //dr["errormsg"] = ex.Message.Substring(0, Math.Min(ex.Message.Length, 2000));
                Common.WriteLog(ex.Message);
            }
        }

        /// <summary>
        /// 当文件路径中包含有特殊字符时，可能无法精确定位，此时，尝试模糊查找该文件。
        /// </summary>
        /// <returns></returns>
        private string GetLikeFilePath(string filePath)
        {
            int Last1 = filePath.LastIndexOf(@"\");
            int Last2 = filePath.Substring(0, Last1).LastIndexOf(@"\");
            int Last3 = filePath.Substring(0, Last2).LastIndexOf(@"\");
            string[] files = Directory.GetFileSystemEntries(filePath.Substring(0, Last3),
                filePath.Substring(Last1 + 1), SearchOption.AllDirectories);

            if (files.Length == 0)
            {
                return string.Empty;
            }
            else
                return files[0];
        }

        /// <summary>
        /// 初始化
        /// </summary>
        //private void Init()
        //{
        //    dataTable.Columns["PID"].ColumnName = "Patient_ID";

        //    dataTable.Columns.AddRange(new DataColumn[] {                                               
        //        new DataColumn("Url"),                              
        //        new DataColumn("hospital_Name"),                
        //    });

        //    string firstExamNo =
        //        dataTable.Columns.Contains("aid") ? dataTable.Rows[0]["aid"].ToString()
        //        : dataTable.Columns.Contains("EXAM_NO") ? dataTable.Rows[0]["EXAM_NO"].ToString()
        //        : dataTable.Rows[0]["EXAMNO"].ToString();
        //    string firstPatientId = dataTable.Rows[0]["PATIENT_ID"].ToString();

        //    try
        //    {
        //        bool IsCreating = Common.Static.PacsExamBackgroundAdd(firstExamNo);

        //        //盘符没有冒号时补上
        //        if (!pacsRemoteDisk.EndsWith(":"))
        //            pacsRemoteDisk = pacsRemoteDisk + ":";

        //        string startPath = string.Empty;
        //        startPath = pacsPointDir.StartsWith(@"\\") ? (pacsRemoteDisk + pacsRemotePath) : pacsPointDir;
        //        startPath += startPath.EndsWith(@"\") ? string.Empty : @"\";

        //        string tempAbsSavePath = Common.Static.HttpCurrent.Server.MapPath(savePath);

        //        foreach (DataRow dr in dataTable.Rows)
        //        {
        //            string filePath = dr["FILENAME"].ToString();
        //            string SeriesId = dr["Series_Id"].ToString();

        //            // 复制PACS的目录,依此生成目录
        //            string tempEndPath = firstPatientId + @"\" + firstExamNo + @"\" + SeriesId;

        //            saveAbsPath = tempAbsSavePath + tempEndPath;

        //            // 不存在时创建
        //            if (!Directory.Exists(saveAbsPath))
        //                Directory.CreateDirectory(saveAbsPath);

        //            //// 文件名
        //            //string fileName = string.Empty;
        //            //if (filePath.Contains(@"\"))
        //            //    fileName = filePath.Substring(filePath.LastIndexOf(@"\") + 1);
        //            //else
        //            //    fileName = filePath.Substring(filePath.LastIndexOf(@"/") + 1);

        //            // 文件名
        //            string fileName = dr["Image_No"].ToString() + "." + imageFormat.ToString();

        //            // 图片的保存路径
        //            string saveImagePath = string.Empty;
        //            // 图片的URL地址
        //            string httpImagePath = string.Empty;
        //            // DICOM文件地址
        //            string tempFilePath = string.Empty;

        //            saveImagePath = saveAbsPath + "\\" + fileName;
        //            httpImagePath = Common.Static.GetImageUrl(savePath + tempEndPath + @"/" + fileName);

        //            if (string.IsNullOrEmpty(pacsDir))
        //            {
        //                tempFilePath = startPath + filePath;
        //            }
        //            else
        //                tempFilePath = filePath.Replace(pacsDir, startPath);

        //            // 绝对转相对
        //            tempFilePath = tempFilePath.Replace(@"/", @"\").Replace(@"\\", @"\");

        //            //DICOM文件存在
        //            if (File.Exists(tempFilePath))
        //            {
        //                //new FileInfo(tempFilePath).c
        //                FileStream sFile = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);
        //                //Stream s = new MemoryStream();
        //                //sFile.CopyTo(s);
        //                // 如果图片不存在,或者hospitalName为空,则新建
        //                // 如果图片存在,且hospitalName不为空,则直接赋值
        //                if (!File.Exists(saveImagePath) || string.IsNullOrEmpty(hospitalName))
        //                {
        //                    try
        //                    {
        //                        //DicomFile dicomFile = DicomFile.Open(tempFilePath);
        //                        DicomFile dicomFile = DicomFile.Open(sFile);
        //                        DicomDataset dcmDataSet = dicomFile.Dataset;

        //                        var dcmImage = new DicomImage(dcmDataSet);//可以增加第二个参数来指定获取第几帧

        //                        //// 检查ID                
        //                        //dr["Study_Id"] = dcmDataSet.Get<String>(DicomTag.StudyID);
        //                        // 检查类别 (如CT或DR等)
        //                        //dr["Study_Type"] = dcmDataSet.Get<String>(DicomTag.Modality);
        //                        //// 检查日期
        //                        //dr["Study_Date"] = dcmDataSet.Get<String>(DicomTag.StudyDate);
        //                        //// 影像编号
        //                        //dr["Instance_Number"] = dcmDataSet.Get<String>(DicomTag.InstanceNumber);            

        //                        hospitalName = dcmDataSet.Get<String>(DicomTag.InstitutionName);

        //                        dr["hospital_Name"] = hospitalName;
        //                        dr["Url"] = httpImagePath;

        //                        ////创建一个bitmap类型的bmp变量来读取文件。
        //                        //Bitmap bmp = new Bitmap(image);
        //                        ////将第一个bmp拷贝到bmp2中
        //                        //Graphics draw = Graphics.FromImage(bmp);
        //                        //draw.DrawImage(bmp, 0, 0);

        //                        //draw.Dispose();
        //                        //bmp.Dispose();//释放bmp文件资源
        //                        if (IsCreating)
        //                            using (Image image = dcmImage.RenderImage())
        //                            {
        //                                if (!File.Exists(saveImagePath))
        //                                    image.Save(saveImagePath, imageFormat);
        //                                else
        //                                    image.Dispose();
        //                            }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        //if (File.Exists(saveImagePath))
        //                        //    throw new Exception(saveImagePath + "图片保存失败." + ex.Message +
        //                        //        Environment.NewLine +
        //                        //        "文件是否存在:" + File.Exists(saveImagePath).ToString() +
        //                        //        " 文件大小: " + new FileInfo(saveImagePath).Length);
        //                        //else
        //                        //    throw new Exception(saveImagePath + "图片保存失败." + ex.Message +
        //                        //        Environment.NewLine +
        //                        //        "文件是否存在:" + File.Exists(saveImagePath).ToString() +
        //                        //        " 文件大小: 0 . ");

        //                        throw new Exception(saveImagePath + "图片保存失败." + ex.Message);
        //                    }
        //                }
        //                else
        //                {
        //                    dr["hospital_Name"] = hospitalName;
        //                    dr["Url"] = httpImagePath;
        //                }
        //            }
        //            //DICOM文件不存在
        //            else
        //            {
        //                if (ShowError)
        //                    throw new Exception(" DICOM文件: " + tempFilePath + " 存在! ");
        //            }
        //        }

        //        if (IsCreating)
        //            Common.Static.PacsExamBackgroundDelete(firstExamNo);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Static.PacsExamBackgroundDelete(firstExamNo);

        //        throw new Exception(ex.TargetSite.Name + ex.Message);
        //    }
        //}

        /// <summary>
        /// 初始化
        /// </summary>
        //private void Init()
        //{
        //    dataTable.Columns["PID"].ColumnName = "Patient_ID";

        //    dataTable.Columns.AddRange(new DataColumn[] {                                               
        //        new DataColumn("Url"),                              
        //        new DataColumn("hospital_Name"),                
        //    });
        //    //dataTable.Columns.Remove("FILENAME");          

        //    string firstFileName = dataTable.Rows[0]["FILENAME"].ToString();
        //    string firstExamNo =
        //        dataTable.Columns.Contains("aid") ? dataTable.Rows[0]["aid"].ToString()
        //        : dataTable.Columns.Contains("EXAM_NO") ? dataTable.Rows[0]["EXAM_NO"].ToString()
        //        : dataTable.Rows[0]["EXAMNO"].ToString();
        //    string firstPatientId = dataTable.Rows[0]["PATIENT_ID"].ToString();

        //    try
        //    {
        //        if (Common.Static.PacsExamBackgroundAdd(firstExamNo))
        //        {
        //            //盘符没有冒号时补上
        //            if (!pacsRemoteDisk.EndsWith(":"))
        //                pacsRemoteDisk = pacsRemoteDisk + ":";

        //            string startPath = string.Empty;
        //            startPath = pacsPointDir.StartsWith(@"\\") ? (pacsRemoteDisk + pacsRemotePath) : pacsPointDir;
        //            startPath += startPath.EndsWith(@"\") ? string.Empty : @"\";

        //            string tempAbsSavePath = Common.Static.HttpCurrent.Server.MapPath(savePath);

        //            foreach (DataRow dr in dataTable.Rows)
        //            {
        //                string filePath = dr["FILENAME"].ToString();
        //                string SeriesId = dr["Series_Id"].ToString();

        //                // 复制PACS的目录,依此生成目录
        //                string tempEndPath = firstPatientId + @"\" + firstExamNo + @"\" + SeriesId;

        //                saveAbsPath = tempAbsSavePath + tempEndPath;

        //                // 不存在时创建
        //                if (!Directory.Exists(saveAbsPath))
        //                    Directory.CreateDirectory(saveAbsPath);

        //                //// 文件名
        //                //string fileName = string.Empty;
        //                //if (filePath.Contains(@"\"))
        //                //    fileName = filePath.Substring(filePath.LastIndexOf(@"\") + 1);
        //                //else
        //                //    fileName = filePath.Substring(filePath.LastIndexOf(@"/") + 1);

        //                // 文件名
        //                string fileName = dr["Image_No"].ToString() + "." + imageFormat.ToString();

        //                // 图片的保存路径
        //                string saveImagePath = string.Empty;
        //                // 图片的URL地址
        //                string httpImagePath = string.Empty;
        //                // DICOM文件地址
        //                string tempFilePath = string.Empty;

        //                saveImagePath = saveAbsPath + "\\" + fileName;
        //                httpImagePath = Common.Static.GetImageUrl(savePath + tempEndPath + @"/" + fileName);

        //                if (string.IsNullOrEmpty(pacsDir))
        //                {
        //                    tempFilePath = startPath + filePath;
        //                }
        //                else
        //                    tempFilePath = filePath.Replace(pacsDir, startPath);

        //                // 绝对转相对
        //                tempFilePath = tempFilePath.Replace(@"/", @"\").Replace(@"\\", @"\");

        //                //DICOM文件存在
        //                if (File.Exists(tempFilePath))
        //                {
        //                    // 如果图片不存在,或者hospitalName为空,则新建
        //                    // 如果图片存在,且hospitalName不为空,则直接赋值
        //                    if (!File.Exists(saveImagePath) || string.IsNullOrEmpty(hospitalName))
        //                    {
        //                        try
        //                        {
        //                            DicomFile dicomFile = DicomFile.Open(tempFilePath);
        //                            DicomDataset dcmDataSet = dicomFile.Dataset;

        //                            var dcmImage = new DicomImage(dcmDataSet);//可以增加第二个参数来指定获取第几帧

        //                            //// 检查ID                
        //                            //dr["Study_Id"] = dcmDataSet.Get<String>(DicomTag.StudyID);
        //                            // 检查类别 (如CT或DR等)
        //                            //dr["Study_Type"] = dcmDataSet.Get<String>(DicomTag.Modality);
        //                            //// 检查日期
        //                            //dr["Study_Date"] = dcmDataSet.Get<String>(DicomTag.StudyDate);
        //                            //// 影像编号
        //                            //dr["Instance_Number"] = dcmDataSet.Get<String>(DicomTag.InstanceNumber);            

        //                            hospitalName = dcmDataSet.Get<String>(DicomTag.InstitutionName);

        //                            dr["hospital_Name"] = hospitalName;
        //                            dr["Url"] = httpImagePath;

        //                            ////创建一个bitmap类型的bmp变量来读取文件。
        //                            //Bitmap bmp = new Bitmap(image);
        //                            ////将第一个bmp拷贝到bmp2中
        //                            //Graphics draw = Graphics.FromImage(bmp);
        //                            //draw.DrawImage(bmp, 0, 0);

        //                            //draw.Dispose();
        //                            //bmp.Dispose();//释放bmp文件资源

        //                            using (Image image = dcmImage.RenderImage())
        //                            {
        //                                if (!File.Exists(saveImagePath))
        //                                    image.Save(saveImagePath, imageFormat);
        //                                else
        //                                    image.Dispose();
        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            throw new Exception(saveImagePath + "图片保存失败." + ex.Message +
        //                                Environment.NewLine + " 文件大小: " +
        //                                new FileInfo(saveImagePath).Length / 1024);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        dr["hospital_Name"] = hospitalName;
        //                        dr["Url"] = httpImagePath;
        //                    }
        //                }
        //                //DICOM文件不存在
        //                else
        //                {
        //                    if (ShowError)
        //                        throw new Exception(tempFilePath + "不存在! ");

        //                    dr["Url"] = null;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        Common.Static.PacsExamBackgroundDelete(firstExamNo);
        //    }
        //}

        /// <summary>
        /// 返回从pacs读取的数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataTable()
        {
            return dataSet;
        }



    }

    class DicomHandler
    {
        string fileName = "";
        Dictionary<string, string> tags = new Dictionary<string, string>();//dicom文件中的标签
        BinaryReader dicomFile;//dicom文件流

        //文件元信息
        public Bitmap gdiImg;//转换后的gdi图像
        UInt32 fileHeadLen;//文件头长度
        long fileHeadOffset;//文件数据开始位置
        UInt32 pixDatalen;//像素数据长度
        long pixDataOffset = 0;//像素数据开始位置
        bool isLitteEndian = true;//是否小字节序（小端在前 、大端在前）
        bool isExplicitVR = true;//有无VR

        //像素信息
        int colors;//颜色数 RGB为3 黑白为1
        public int windowWith = 2048, windowCenter = 2048 / 2;//窗宽窗位
        int rows, cols;
        public void readAndShow()
        {
            if (fileName == string.Empty)
                return;
            dicomFile = new BinaryReader(File.OpenRead(fileName));

            //跳过128字节导言部分
            dicomFile.BaseStream.Seek(128, SeekOrigin.Begin);

            if (new string(dicomFile.ReadChars(4)) != "DICM")
            {
                MessageBox.Show("没有dicom标识头，文件格式错误");
                return;
            }


            tagRead();

            IDictionaryEnumerator enor = tags.GetEnumerator();
            //while (enor.MoveNext())
            //{
            //    if (enor.Key.ToString().Length > 9)
            //    {
            //        textBox1.Text += enor.Key.ToString() + "\r\n";
            //        textBox1.Text += enor.Value.ToString().Replace('\0', ' ');
            //    }
            //    else
            //        textBox1.Text += enor.Key.ToString() + enor.Value.ToString().Replace('\0', ' ') + "\r\n";
            //}
            dicomFile.Close();
        }
        public DicomHandler(string _filename)
        {
            fileName = _filename;
        }

        public void saveAs(string filename)
        {
            switch (filename.Substring(filename.LastIndexOf('.')))
            {
                case ".jpg":
                case ".Jpeg":
                    gdiImg.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case ".bmp":
                case ".Bmp":
                    gdiImg.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case ".png":
                case ".Png":
                    gdiImg.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
                    break;
                default:
                    break;
            }
        }

        public bool getImg()//获取图像 在图像数据偏移量已经确定的情况下
        {
            if (fileName == string.Empty)
                return false;

            int dataLen, validLen;//数据长度 有效位
            int imgNum;//帧数

            rows = int.Parse(tags["0028,0010"].Substring(5));
            cols = int.Parse(tags["0028,0011"].Substring(5));

            colors = int.Parse(tags["0028,0002"].Substring(5));
            dataLen = int.Parse(tags["0028,0100"].Substring(5));
            validLen = int.Parse(tags["0028,0101"].Substring(5));

            gdiImg = new Bitmap(cols, rows);

            BinaryReader dicomFile = new BinaryReader(File.OpenRead(fileName));

            dicomFile.BaseStream.Seek(pixDataOffset, SeekOrigin.Begin);

            long reads = 0;
            for (int i = 0; i < gdiImg.Height; i++)
            {
                for (int j = 0; j < gdiImg.Width; j++)
                {
                    if (reads >= pixDatalen)
                        break;
                    byte[] pixData = dicomFile.ReadBytes(dataLen / 8 * colors);
                    reads += pixData.Length;

                    Color c = Color.Empty;
                    if (colors == 1)
                    {
                        int grayGDI;

                        double gray = BitConverter.ToUInt16(pixData, 0);
                        //调窗代码，就这么几句而已 
                        //1先确定窗口范围 2映射到8位灰度
                        int grayStart = (windowCenter - windowWith / 2);
                        int grayEnd = (windowCenter + windowWith / 2);

                        if (gray < grayStart)
                            grayGDI = 0;
                        else if (gray > grayEnd)
                            grayGDI = 255;
                        else
                        {
                            grayGDI = (int)((gray - grayStart) * 255 / windowWith);
                        }

                        if (grayGDI > 255)
                            grayGDI = 255;
                        else if (grayGDI < 0)
                            grayGDI = 0;
                        c = Color.FromArgb(grayGDI, grayGDI, grayGDI);
                    }
                    else if (colors == 3)
                    {
                        c = Color.FromArgb(pixData[0], pixData[1], pixData[2]);
                    }

                    gdiImg.SetPixel(j, i, c);
                }
            }

            dicomFile.Close();
            return true;
        }
        void tagRead()//不断读取所有tag 及其值 直到碰到图像数据 (7fe0 0010 )
        {
            bool enDir = false;
            int leve = 0;
            StringBuilder folderData = new StringBuilder();//该死的文件夹标签
            string folderTag = "";
            while (dicomFile.BaseStream.Position + 6 < dicomFile.BaseStream.Length)
            {
                //读取tag
                string tag = dicomFile.ReadUInt16().ToString("x4") + "," +
                dicomFile.ReadUInt16().ToString("x4");

                string VR = string.Empty;
                UInt32 Len = 0;
                //读取VR跟Len
                //对OB OW SQ 要做特殊处理 先置两个字节0 然后4字节值长度
                //------------------------------------------------------这些都是在读取VR一步被阻断的情况
                if (tag.Substring(0, 4) == "0002")//文件头 特殊情况
                {
                    VR = new string(dicomFile.ReadChars(2));

                    if (VR == "OB" || VR == "OW" || VR == "SQ" || VR == "OF" || VR == "UT" || VR == "UN")
                    {
                        dicomFile.BaseStream.Seek(2, SeekOrigin.Current);
                        Len = dicomFile.ReadUInt32();
                    }
                    else
                        Len = dicomFile.ReadUInt16();
                }
                else if (tag == "fffe,e000" || tag == "fffe,e00d" || tag == "fffe,e0dd")//文件夹标签
                {
                    VR = "**";
                    Len = dicomFile.ReadUInt32();
                }
                else if (isExplicitVR == true)//有无VR的情况
                {
                    VR = new string(dicomFile.ReadChars(2));

                    if (VR == "OB" || VR == "OW" || VR == "SQ" || VR == "OF" || VR == "UT" || VR == "UN")
                    {
                        dicomFile.BaseStream.Seek(2, SeekOrigin.Current);
                        Len = dicomFile.ReadUInt32();
                    }
                    else
                        Len = dicomFile.ReadUInt16();
                }
                else if (isExplicitVR == false)
                {
                    VR = getVR(tag);//无显示VR时根据tag一个一个去找 真tm烦啊。
                    Len = dicomFile.ReadUInt32();
                }
                //判断是否应该读取VF 以何种方式读取VF
                //-------------------------------------------------------这些都是在读取VF一步被阻断的情况
                byte[] VF = { 0x00 };

                if (tag == "7fe0,0010")//图像数据开始了
                {
                    pixDatalen = Len;
                    pixDataOffset = dicomFile.BaseStream.Position;
                    dicomFile.BaseStream.Seek(Len, SeekOrigin.Current);
                    VR = "UL";
                    VF = BitConverter.GetBytes(Len);
                }
                else if ((VR == "SQ" && Len == UInt32.MaxValue) || (tag == "fffe,e000" && Len == UInt32.MaxValue))//靠 遇到文件夹开始标签了
                {
                    if (enDir == false)
                    {
                        enDir = true;
                        folderData.Remove(0, folderData.Length);
                        folderTag = tag;
                    }
                    else
                    {
                        leve++;//VF不赋值
                    }
                }
                else if ((tag == "fffe,e00d" && Len == UInt32.MinValue) || (tag == "fffe,e0dd" && Len == UInt32.MinValue))//文件夹结束标签
                {
                    if (enDir == true)
                    {
                        enDir = false;
                    }
                    else
                    {
                        leve--;
                    }
                }
                else
                    VF = dicomFile.ReadBytes((int)Len);

                string VFStr;

                VFStr = getVF(VR, VF);

                //----------------------------------------------------------------针对特殊的tag的值的处理
                //特别针对文件头信息处理
                if (tag == "0002,0000")
                {
                    fileHeadLen = Len;
                    fileHeadOffset = dicomFile.BaseStream.Position;
                }
                else if (tag == "0002,0010")//传输语法 关系到后面的数据读取
                {
                    switch (VFStr)
                    {
                        case "1.2.840.10008.1.2.1\0"://显示little
                            isLitteEndian = true;
                            isExplicitVR = true;
                            break;
                        case "1.2.840.10008.1.2.2\0"://显示big
                            isLitteEndian = false;
                            isExplicitVR = true;
                            break;
                        case "1.2.840.10008.1.2\0"://隐式little
                            isLitteEndian = true;
                            isExplicitVR = false;
                            break;
                        default:
                            break;
                    }
                }
                for (int i = 1; i <= leve; i++)
                    tag = "--" + tag;
                //------------------------------------数据搜集代码
                if ((VR == "SQ" && Len == UInt32.MaxValue) || (tag == "fffe,e000" && Len == UInt32.MaxValue) || leve > 0)//文件夹标签代码
                {
                    folderData.AppendLine(tag + "(" + VR + ")：" + VFStr);
                }
                else if (((tag == "fffe,e00d" && Len == UInt32.MinValue) || (tag == "fffe,e0dd" && Len == UInt32.MinValue)) && leve == 0)//文件夹结束标签
                {
                    folderData.AppendLine(tag + "(" + VR + ")：" + VFStr);
                    tags.Add(folderTag + "SQ", folderData.ToString());
                }
                else
                    tags.Add(tag, "(" + VR + "):" + VFStr);
            }
        }

        string getVR(string tag)
        {
            switch (tag)
            {
                case "0002,0000"://文件元信息长度
                    return "UL";
                    break;
                case "0002,0010"://传输语法
                    return "UI";
                    break;
                case "0002,0013"://文件生成程序的标题
                    return "SH";
                    break;
                case "0008,0005"://文本编码
                    return "CS";
                    break;
                case "0008,0008":
                    return "CS";
                    break;
                case "0008,1032"://成像时间
                    return "SQ";
                    break;
                case "0008,1111":
                    return "SQ";
                    break;
                case "0008,0020"://检查日期
                    return "DA";
                    break;
                case "0008,0060"://成像仪器
                    return "CS";
                    break;
                case "0008,0070"://成像仪厂商
                    return "LO";
                    break;
                case "0008,0080":
                    return "LO";
                    break;
                case "0010,0010"://病人姓名
                    return "PN";
                    break;
                case "0010,0020"://病人id
                    return "LO";
                    break;
                case "0010,0030"://病人生日
                    return "DA";
                    break;
                case "0018,0060"://电压
                    return "DS";
                    break;
                case "0018,1030"://协议名
                    return "LO";
                    break;
                case "0018,1151":
                    return "IS";
                    break;
                case "0020,0010"://检查ID
                    return "SH";
                    break;
                case "0020,0011"://序列
                    return "IS";
                    break;
                case "0020,0012"://成像编号
                    return "IS";
                    break;
                case "0020,0013"://影像编号
                    return "IS";
                    break;
                case "0028,0002"://像素采样1为灰度3为彩色
                    return "US";
                    break;
                case "0028,0004"://图像模式MONOCHROME2为灰度
                    return "CS";
                    break;
                case "0028,0010"://row高
                    return "US";
                    break;
                case "0028,0011"://col宽
                    return "US";
                    break;
                case "0028,0100"://单个采样数据长度
                    return "US";
                    break;
                case "0028,0101"://实际长度
                    return "US";
                    break;
                case "0028,0102"://采样最大值
                    return "US";
                    break;
                case "0028,1050"://窗位
                    return "DS";
                    break;
                case "0028,1051"://窗宽
                    return "DS";
                    break;
                case "0028,1052":
                    return "DS";
                    break;
                case "0028,1053":
                    return "DS";
                    break;
                case "0040,0008"://文件夹标签
                    return "SQ";
                    break;
                case "0040,0260"://文件夹标签
                    return "SQ";
                    break;
                case "0040,0275"://文件夹标签
                    return "SQ";
                    break;
                case "7fe0,0010"://像素数据开始处
                    return "OW";
                    break;
                default:
                    return "UN";
                    break;
            }
        }

        string getVF(string VR, byte[] VF)
        {
            string VFStr = string.Empty;
            if (isLitteEndian == false)//如果是big字节序 先把数据翻转一下
                Array.Reverse(VF);
            switch (VR)
            {
                case "SS":
                    VFStr = BitConverter.ToInt16(VF, 0).ToString();
                    break;
                case "US":
                    VFStr = BitConverter.ToUInt16(VF, 0).ToString();

                    break;
                case "SL":
                    VFStr = BitConverter.ToInt32(VF, 0).ToString();

                    break;
                case "UL":
                    VFStr = BitConverter.ToUInt32(VF, 0).ToString();

                    break;
                case "AT":
                    VFStr = BitConverter.ToUInt16(VF, 0).ToString();

                    break;
                case "FL":
                    VFStr = BitConverter.ToSingle(VF, 0).ToString();

                    break;
                case "FD":
                    VFStr = BitConverter.ToDouble(VF, 0).ToString();

                    break;
                case "OB":
                    VFStr = BitConverter.ToString(VF, 0);
                    break;
                case "OW":
                    VFStr = BitConverter.ToString(VF, 0);
                    break;
                case "SQ":
                    VFStr = BitConverter.ToString(VF, 0);
                    break;
                case "OF":
                    VFStr = BitConverter.ToString(VF, 0);
                    break;
                case "UT":
                    VFStr = BitConverter.ToString(VF, 0);
                    break;
                case "UN":
                    VFStr = Encoding.Default.GetString(VF);
                    break;
                default:
                    VFStr = Encoding.Default.GetString(VF);
                    break;
            }
            return VFStr;
        }
    }
}
