// ***********************************************************************
// Assembly         : BodyTemp
// Author           : LPD
// Created          : 01-12-2016
//
// Last Modified By : LPD
// Last Modified On : 01-13-2016
// ***********************************************************************
// <copyright file="SetAutoUpdate.cs" company="心医国际(西安)">
//     Copyright (c) 心医国际(西安). All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using Xy.Auxiliary.Bll;
using System.Collections;
using Xy.Auxiliary.PubCls;

/// <summary>
/// The BodyTemp namespace.
/// </summary>
namespace BodyTemp
{
    /// <summary>
    /// Class SetAutoUpdate.
    /// </summary>
    public partial class SetAutoUpdate : DevExpress.XtraEditors.XtraForm
    {
        BodytempAutoupdate_Bll bodyAutoUpdateBll = new BodytempAutoupdate_Bll();

        /// <summary>
        /// 实例Mobile连接
        /// </summary>
        DBHelper dbMobile = new DBHelper(ConnFlag.mobile.ToString());


        /// <summary>
        /// Initializes a new instance of the <see cref="SetAutoUpdate"/> class.
        /// </summary>
        public SetAutoUpdate()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 开始上传
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            //记录新增的文件个数
            int upLoadIns = 0;

            //记录修改的文件个数
            int upLoadUpd = 0;

            if (lstUpLoadRecode.Items.Count <= 0)
            {
                MessageBox.Show("请添加记录后再上传.", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.lblUpLoadState.Text = "开始上传中...";

            //设置一个最小值
            barSchedule.Properties.Minimum = 0;
            //设置一个最大值
            barSchedule.Properties.Maximum = lstUpLoadRecode.Items.Count;

            //设置步长，即每次增加的数
            barSchedule.Properties.Step = 1;

            //是否显示进度数据
            barSchedule.Properties.ShowTitle = true;

            //设置进度条的样式
            barSchedule.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;

            barSchedule.Position = 0;

            ArrayList alst = new ArrayList();

            //遍历需要上传的文件
            for (int m = 0; m < lstUpLoadRecode.Items.Count; m++)
            {
                //上传文件的文件路径
                string filePath = lstUpLoadRecode.Items[m].ToString();

                //上传文件的文件名
                string fileName = System.IO.Path.GetFileName(filePath);

                //记录SQL
                string strSql = string.Empty;


                //判断该文件名是否存在数据库中，不存在:插入   不存在:更新  
                bool _IsExit = bodyAutoUpdateBll.fileIsExit(fileName);


                if (_IsExit)
                {
                    int _nowVersion = bodyAutoUpdateBll.GetNowVersion(fileName) + 1;
                    //存在，更新，版本号加1
                    dbMobile.UpdateFile(filePath, fileName, _nowVersion.ToString());
                    upLoadUpd++;
                }
                else
                {
                    //不存在，新增，版本号等于1
                    dbMobile.InsertFile(filePath, fileName, "1");
                    upLoadIns++;
                }

                //处理当前消息队列中的所有windows消息
                Application.DoEvents();

                //执行步长
                barSchedule.PerformStep();
            }

            //上传状态
            MessageBox.Show("新增:" + upLoadIns + "条" + "\r\n" + "更新:" + upLoadUpd + "条", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            lblUpLoadState.Text = "上传完成.";
            this.Close();
        }

        /// <summary>
        /// 浏览文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnbrowse_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = System.Environment.CurrentDirectory;
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            //获取选择文件的集合
                            string[] arrFiles = openFileDialog1.FileNames;
                            if (arrFiles != null && arrFiles.Length > 0)
                            {
                                txtPath.Text = Path.GetDirectoryName(openFileDialog1.FileName); ;
                                for (int i = 0; i < arrFiles.Length; i++)
                                {
                                    lstUpLoadRecode.Items.Add(arrFiles[i].ToString());
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void SetAutoUpdate_Load(object sender, EventArgs e)
        {

        }

        
    }
}