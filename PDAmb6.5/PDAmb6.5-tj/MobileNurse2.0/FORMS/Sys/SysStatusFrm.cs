using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HISPlus
{
    public partial class SysStatusFrm : Form
    {
        public SysStatusFrm()
        {
            InitializeComponent();
        }

        
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SysStatusFrm_Load(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;
                
                mnuRefresh_Click(null, null);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }
        
        
        /// <summary>
        /// 菜单 [返回]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        /// <summary>
        /// 按钮[刷新]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuRefresh_Click(object sender, EventArgs e)
        {

        }

        private void mnuLog_Down_Click(object sender, EventArgs e)
        {
            refreshLog("Down");
        }

        private void mnuLog_Up_Click(object sender, EventArgs e)
        {
            refreshLog("Up");
        }

        private void mnuLog_Normal_Click(object sender, EventArgs e)
        {
            refreshLog("Normal");
        }
        
        
        private void refreshLog(string tableName)
        {
            Cursor.Current = Cursors.WaitCursor;
            
            try
            {
                txtResult.Text = string.Empty;

                if (System.IO.File.Exists("Download.xml") == false)
                {
                    txtResult.Text = "Download.xml不存在";
                    return;
                }

                DataSet dsLog = new DataSet();
                dsLog.ReadXml("Download.xml");
                
                if (dsLog.Tables.Contains(tableName) == false) 
                {
                    txtResult.Text = tableName + " 日志 不存在";
                    return;
                }
                
                StringBuilder sb = new StringBuilder();
                for (int i = dsLog.Tables[tableName].Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dsLog.Tables[tableName].Rows[i];
                    sb.AppendLine(dr[0].ToString());
                }
                txtResult.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                txtResult.Text = ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}