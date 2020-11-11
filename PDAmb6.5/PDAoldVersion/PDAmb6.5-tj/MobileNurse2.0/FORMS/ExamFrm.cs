using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class ExamFrm : Form
    {
        public ExamFrm()
        {
            InitializeComponent();
        }
        
        private void ExamFrm_Load(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
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
                this.Close();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 时钟消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                
                Cursor.Current = Cursors.WaitCursor;
                mnuPatient.Enabled = false;
                GVars.HISDataSrv.Url = UrlIp.ChangeIpInUrl(GVars.HISDataSrv.Url, GVars.App.ServerIp);
                
                // 获取检查记录
                if (GVars.Patient.ID.Length == 0)
                {
                    return;
                }
                
                // 当前病人
                mnuPatient.Enabled = true;
                mnuPatient.Text = GVars.DsPatient.Tables[0].Rows[GVars.PatIndex]["NAME"].ToString();
                
                // 获取数据
                DataSet ds = GVars.HISDataSrv.GetExamList(GVars.Patient.ID, GVars.Patient.VisitId);
                
                // 显示检查记录
                DataRow[] drFind = ds.Tables[0].Select("", "EXAM_DATE_TIME DESC");
                DataRow   dr     = null;
                for(int i = 0; i < drFind.Length; i++)
                {
                    dr = drFind[i];
                    
                    ListViewItem item = new ListViewItem(dr["EXAM_CLASS"].ToString());
                    item.SubItems.Add(dr["EXAM_SUB_CLASS"].ToString());
                    
                    if (dr["EXAM_DATE_TIME"] != DBNull.Value && dr["EXAM_DATE_TIME"].ToString().Length > 0)
                    {
                        item.SubItems.Add(((DateTime)(dr["EXAM_DATE_TIME"])).ToString(ComConst.FMT_DATE.LONG_MINUTE));
                    }

                    item.Tag = dr["EXAM_NO"].ToString();
                    
                    lvwExam.Items.Add(item);
                }

                // 菜单控制
                foreach (MenuItem mnu in mnuNavigator.MenuItems)
                {
                    if (mnu.Text.IndexOf(this.Text) >= 0)
                    {
                        mnu.Enabled = false;
                    }
                    else
                    {
                        mnu.Click += new EventHandler(mnuNavigator_Func_Click);
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        /// <summary>
        /// 菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuNavigator_Func_Click(object sender, EventArgs e)
        {
            try
            {
                MenuItem mnu = sender as MenuItem;
                if (mnu == null) return;

                this.Tag = mnu.Text;

                this.Close();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 检查列表选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                // 清除结果
                txtContent.Text = string.Empty;
                txtContent.ForeColor = Color.Black;
                
                if (lvwExam.SelectedIndices.Count == 0)
                {
                    return;
                }
                
                string examNo = lvwExam.Items[lvwExam.SelectedIndices[0]].Tag.ToString(); ;
                
                // 查询检查结果
                DataSet ds = GVars.HISDataSrv.GetExamResult(examNo);
                
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                
                // 显示检查结果
                DataRow dr = ds.Tables[0].Rows[0];

                // 检查参数
                if (dr["EXAM_PARA"] != DBNull.Value)
                {
                    if (txtContent.Text.Length > 0)
                    {
                        txtContent.Text += ComConst.STR.CRLF;
                    }
                    
                    txtContent.Text += "[检查参数]" + ComConst.STR.CRLF;
                    txtContent.Text += dr["EXAM_PARA"].ToString();
                }

                // 检查所见
                if (dr["DESCRIPTION"] != DBNull.Value)
                {
                    if (txtContent.Text.Length > 0)
                    {
                        txtContent.Text += ComConst.STR.CRLF;
                    }

                    txtContent.Text += "[检查所见]" + ComConst.STR.CRLF;
                    txtContent.Text += dr["DESCRIPTION"].ToString();
                }

                // 印象
                if (dr["IMPRESSION"] != DBNull.Value)
                {
                    if (txtContent.Text.Length > 0)
                    {
                        txtContent.Text += ComConst.STR.CRLF;
                    }

                    txtContent.Text += "[印象]" + ComConst.STR.CRLF;
                    txtContent.Text += dr["IMPRESSION"].ToString();
                }

                // 建议
                if (dr["RECOMMENDATION"] != DBNull.Value)
                {
                    if (txtContent.Text.Length > 0)
                    {
                        txtContent.Text += ComConst.STR.CRLF;
                    }

                    txtContent.Text += "[建议]" + ComConst.STR.CRLF;
                    txtContent.Text += dr["RECOMMENDATION"].ToString();
                }

                if (dr["IS_ABNORMAL"] != DBNull.Value && dr["IS_ABNORMAL"].ToString().Equals("1"))
                {
                    txtContent.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// 菜单[当前病人]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPatient_Click(object sender, EventArgs e)
        {
            try
            {
                PatientDetailForm patDetail = new PatientDetailForm();
                patDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
    }
}