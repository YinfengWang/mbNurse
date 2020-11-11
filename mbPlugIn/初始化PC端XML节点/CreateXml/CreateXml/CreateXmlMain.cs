// ***********************************************************************
// Assembly         : CreateXml
// Author           : lpd
// Created          : 05-04-2016
//
// Last Modified By : lpd
// Last Modified On : 05-04-2016
// ***********************************************************************
// <copyright file="CreateXmlMain.cs" company="senyint">
//     Copyright (c) senyint. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

/// <summary>
/// The CreateXml namespace.
/// </summary>
namespace CreateXml
{
    /// <summary>
    /// Class CreateXmlMain.
    /// </summary>
    public partial class CreateXmlMain : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateXmlMain"/> class.
        /// </summary>
        public CreateXmlMain()
        {            
            InitializeComponent();            
        }

        /// <summary>
        /// The file path
        /// </summary>
        string filePath = string.Empty;
        /// <summary>
        /// Handles the Click event of the toolOpen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void toolOpen_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dilog = new FolderBrowserDialog();
                dilog.Description = "请选择文件夹";
                if (dilog.ShowDialog() == DialogResult.OK || dilog.ShowDialog() == DialogResult.Yes)
                {
                    filePath = dilog.SelectedPath;
                }

                if (!string.IsNullOrEmpty(filePath))
                {
                    lstShowData.Items.Clear();
                    getAllFiles(filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }


        }

        /// <summary>
        /// Handles the Click event of the toolCreate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void toolCreate_Click(object sender, EventArgs e)
        {

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lstShowData.Items.Count; i++)
            {
                sb.Append("<UpdFileList>\r\n");
                sb.Append("<FILE_NAME>" + lstShowData.Items[i].ToString() + "</FILE_NAME>\r\n");
                sb.Append("<VERSION>1</VERSION>\r\n");
                sb.Append("<MODIFY_DATE>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T') + "</MODIFY_DATE>\r\n");
                sb.Append("</UpdFileList>\r\n");
            }

            txtContent.Text = sb.ToString();
            MessageBox.Show("记录条数:" + lstShowData.Items.Count.ToString(), "Info");
        }

        /// <summary>
        /// Handles the Click event of the toolClear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void toolClear_Click(object sender, EventArgs e)
        {
            lstShowData.Items.Clear();
            txtContent.Clear();
        }

        /// <summary>
        /// Gets all files.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public void getAllFiles(string directory) //获取指定的目录中的所有文件（包括文件夹）
        {
            getFiles(directory);//获取指定的目录中的所有文件（不包括文件夹）
            getDirectory(directory);//获取指定的目录中的所有目录（文件夹）            
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public void getFiles(string directory) //获取指定的目录中的所有文件（不包括文件夹）
        {
            string[] path = System.IO.Directory.GetFiles(directory);
            for (int i = 0; i < path.Length; i++)
                lstShowData.Items.Add(path[i].Replace(filePath + "\\", ""));
        }

        /// <summary>
        /// Gets the directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public void getDirectory(string directory) //获取指定的目录中的所有目录（文件夹）
        {
            string[] directorys = System.IO.Directory.GetDirectories(directory);
            if (directorys.Length <= 0) //如果该目录总没有其他文件夹
                return;
            else
            {
                for (int i = 0; i < directorys.Length; i++)
                    getAllFiles(directorys[i]);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the txtContent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void txtContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control)
            {
                txtContent.SelectAll();
            }
        }

        /// <summary>
        /// Handles the Load event of the CreateXmlMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CreateXmlMain_Load(object sender, EventArgs e)
        {
            txtContent.ScrollBars = ScrollBars.Vertical;
        }
        /// <summary>
        /// 拖入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstShowData_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstShowData_DragDrop(object sender, DragEventArgs e)
        {
            lstShowData.Items.Clear();
            txtContent.Clear();
            filePath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();

            if (System.IO.Directory.Exists(filePath))
            {
                getAllFiles(filePath);
                toolCreate_Click(null, null);
            }
            else
            {
                MessageBox.Show("请选择文件夹...");
                return;
            }
        }

        private void Toolabout_Click(object sender, EventArgs e)
        {
            //new About().ShowDialog();
        }

        /// <summary>
        /// 气泡提示
        /// </summary>
        private int x, y;
        private void lstShowData_MouseMove(object sender, MouseEventArgs e)
        {
            if (x != e.X || y != e.Y)
            {
                toolTip1.SetToolTip(lstShowData, "请将文件夹托至此区域...");
                x = e.X;
                y = e.Y;
            }
        }


    }
}
