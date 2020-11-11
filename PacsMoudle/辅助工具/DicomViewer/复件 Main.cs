using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Threading;

namespace DicomViewer
{
    public partial class Main : Form
    {
        bool showTag =true;//tag/ 影像切换
        bool zoomYes = true;//原图/适合窗口
        public Main()
        {
            InitializeComponent();
            UIswitch();
        }

        string fileName = "";
        Dictionary<string, string> tags = new Dictionary<string, string>();//dicom文件中的标签
        BinaryReader dicomFile;//dicom文件流

        //文件元信息
        Bitmap gdiImg;//转换后的gdi图像
        UInt32 fileHeadLen;//文件头长度
        long fileHeadOffset;//文件数据开始位置
        UInt32 pixDatalen;//像素数据长度
        long pixDataOffset=0;//像素数据开始位置
        bool isLitteEndian=true;//是否小字节序（小端在前 、大端在前）
        bool isExplicitVR=true;//有无VR

        //像素信息
        int colors;//颜色数 RGB为3 黑白为1
        int windowWith=2048, windowCenter=2048/2;//窗宽窗位
        int rows, cols;

        void DataInit()
        {
            tags.Clear();
            //dicomFile;
            gdiImg = null;
            fileHeadLen = 0;
            fileHeadOffset = 0;
            pixDatalen = 0;
            pixDataOffset = 0;

            isLitteEndian = true;
            isExplicitVR = true;

            textBox1.Text="";
            fileName = "";

            windowWith = 2048;
            windowCenter = 2048 / 2;
        }
        bool loading;
       

        void getImg()//获取图像 在图像数据偏移量已经确定的情况下
        {
            
            if (fileName == string.Empty)
                return;

            loading = true;
            int dataLen, validLen;//数据长度 有效位
            int imgNum;//帧数
            //int windowCenter, windowWidth;//窗位 窗宽

            rows = int.Parse(tags["0028,0010"].Substring(5));
            cols = int.Parse(tags["0028,0011"].Substring(5));

            colors = int.Parse(tags["0028,0002"].Substring(5));
            dataLen = int.Parse(tags["0028,0100"].Substring(5));
            validLen = int.Parse(tags["0028,0101"].Substring(5));

            //windowCenter = int.Parse(tags["0028,1050"].Substring(5));
            //windowWidth = int.Parse(tags["0028,1051"].Substring(5));

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
                    byte[] pixData = dicomFile.ReadBytes(dataLen/8*colors);
                    reads += pixData.Length;

                    Color c = Color.Empty;
                    if (colors == 1)
                    {
                        int grayGDI ;

                        double gray = BitConverter.ToUInt16(pixData, 0);

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

                        //grayGDI= (int)(255.00D * gray / 2048.00D);
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
            pictureBox1.Image = gdiImg;
            //Graphics g = Graphics.FromHwnd(this.Handle);
            //g.DrawImage(gdiImg, new Point(0, 0));
            dicomFile.Close();

            loading = false;
            UIswitch();
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
                    tags.Add(folderTag+"SQ", folderData.ToString());
                }
                else
                    tags.Add(tag , "(" + VR + "):"+ VFStr);
            }
        }

        string getVR(string tag)
        {
            switch (tag)
            {
                case "0002,0000":
                    return "UL";
                    break;
                case "0002,0010":
                    return "UI";
                    break;
                case "0002,0013":
                    return "SH";
                    break;
                case "0008,0005":
                    return "CS";
                    break;
                case "0008,0008":
                    return "CS";
                    break;
                case "0008,1032":
                    return "SQ";
                    break;
                case "0008,1111":
                    return "SQ";
                    break;
                case "0008,0020":
                    return "DA";
                    break;
                case "0008,0060":
                    return "CS";
                    break;
                case "0008,0070":
                    return "LO";
                    break;
                case "0008,0080":
                    return "LO";
                    break;
                case "0010,0010":
                    return "PN";
                    break;
                case "0010,0020":
                    return "LO";
                    break;
                case "0010,0030":
                    return "DA";
                    break;
                case "0018,0060":
                    return "DS";
                    break;
                case "0018,1030":
                    return "LO";
                    break;
                case "0018,1151":
                    return "IS";
                    break;
                case "0020,0010":
                    return "SH";
                    break;
                case "0020,0011":
                    return "IS";
                    break;
                case "0020,0012":
                    return "IS";
                    break;
                case "0020,0013":
                    return "IS";
                    break;
                case "0028,0002":
                    return "US";
                    break;
                case "0028,0004":
                    return "CS";
                    break;
                case "0028,0010":
                    return "US";
                    break;
                case "0028,0011":
                    return "US";
                    break;
                case "0028,0100":
                    return "US";
                    break;
                case "0028,0101":
                    return "US";
                    break;
                case "0028,0102":
                    return "US";
                    break;
                case "0028,1050":
                    return "DS";
                    break;
                case "0028,1051":
                    return "DS";
                    break;
                case "0028,1052":
                    return "DS";
                    break;
                case "0028,1053":
                    return "DS";
                    break;
                case "0040,0008":
                    return "SQ";
                    break;
                case "0040,0260":
                    return "SQ";
                    break;
                case "0040,0275":
                    return "SQ";
                    break;
                case "7fe0,0010":
                    return "OW";
                    break;
                default:
                    return "UN";
                    break;
            }
        }

        string getVF(string VR ,byte[] VF)
        {
            string VFStr = string.Empty;
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

     

        void UIswitch()
        {
            windowWidthTxt.Text = windowWith.ToString();
            windowCenterTxt.Text = windowCenter.ToString();
            
            if (showTag)
            {
                textBox1.Visible = true;
                pictureBox1.Visible = false;
                toolStripButton2.Text = "[切换到影像　]";
            }
            else
            {
                textBox1.Visible = false;
                pictureBox1.Visible = true;


                toolStripButton2.Text = "[切换到ｔａｇ]";

                
            }


            if (zoomYes)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                toolStripButton3.Text = "[切换到原始尺寸　　]";
            }
            else
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                toolStripButton3.Text = "[切换到适合窗口尺寸]";
            }

            if (loading)
            {
                Bitmap loadImg = new Bitmap(200, 100);
                Graphics g = Graphics.FromImage(loadImg);
                g.DrawString("载入中，请稍后...", new Font(new FontFamily("Arial"), 15.0f), Brushes.Black, new PointF(0, 0));
                pictureBox1.Image = loadImg;
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            }
        }

        

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if (loading)
            {
                MessageBox.Show("影像载入中，请稍后...");
                return;
            }

            DataInit();
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            fileName = openFileDialog1.FileName;
            dicomFile = new BinaryReader(File.OpenRead(fileName));

            //跳过128字节导言部分
            dicomFile.BaseStream.Seek(128, SeekOrigin.Begin);

            if (new string(dicomFile.ReadChars(4)) != "DICM")
            {
                MessageBox.Show("没有dicom标识头，文件格式错误");
                return;
            }


            tagRead();
            //textBox1.Text = tags.ToString().Replace('\0','/');

            IDictionaryEnumerator enor = tags.GetEnumerator();
            while (enor.MoveNext())
            {
                if (enor.Key.ToString().Length > 9)
                {
                    textBox1.Text += enor.Key.ToString() + "\r\n";
                    textBox1.Text += enor.Value.ToString().Replace('\0', ' ');
                }
                else
                    textBox1.Text += enor.Key.ToString() + enor.Value.ToString().Replace('\0', ' ') + "\r\n";
            }

            this.Text = "DicomViewer-" + openFileDialog1.FileName;
            dicomFile.Close();

            if (gdiImg == null)
            {
                Thread t = new Thread(new ThreadStart(getImg));
                t.Start();
                Thread.Sleep(100);
            }


            UIswitch();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (loading)
            {
                MessageBox.Show("影像载入中，请稍后...");
                return;
            }

            if (saveFileDialog1.ShowDialog() != DialogResult.OK || gdiImg == null)
                return;

            switch (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.LastIndexOf('.')))
            {
                case ".jpg":
                    gdiImg.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case ".bmp":
                    gdiImg.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case ".png":
                    gdiImg.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    break;
                default:
                    break;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            showTag = !showTag;
            UIswitch();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            zoomYes = !zoomYes;
            UIswitch();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (loading)
            {
                MessageBox.Show("影像载入中，请稍后...");
                return;
            }
            int.TryParse(windowWidthTxt.Text, out windowWith);
            int.TryParse(windowCenterTxt.Text, out windowCenter);

            Thread t = new Thread(new ThreadStart(getImg));
            t.Start();
            Thread.Sleep(100);

            UIswitch();
        }

      

      
    }
}
