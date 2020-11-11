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
    public partial class LabTestFrm : Form
    {
        public LabTestFrm()
        {
            InitializeComponent();
        }
        
        
        private void LabTestFrm_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    timer1.Enabled = true;
                }
                catch (Exception ex)
                {
                    Error.ErrProc(ex);
                }
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
                DataSet ds = GVars.HISDataSrv.GetLabTestList(GVars.Patient.ID, GVars.Patient.VisitId);

                // 显示检查记录
                DataRow[] drFind = ds.Tables[0].Select("", "EXECUTE_DATE DESC");
                DataRow   dr     = null;
                for (int i = 0; i < drFind.Length; i++)
                {
                    dr = drFind[i];

                    ListViewItem item = new ListViewItem(dr["SPECIMEN"].ToString());
                    item.SubItems.Add(dr["RELEVANT_CLINIC_DIAG"].ToString());

                    if (dr["EXECUTE_DATE"] != DBNull.Value && dr["EXECUTE_DATE"].ToString().Length > 0)
                    {
                        item.SubItems.Add(((DateTime)(dr["EXECUTE_DATE"])).ToString(ComConst.FMT_DATE.LONG_MINUTE));
                    }

                    item.Tag = dr["TEST_NO"].ToString();

                    lvwLab.Items.Add(item);
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
        
        
        private void lvwLab_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                // 清除结果
                lvwResult.Items.Clear();

                if (lvwLab.SelectedIndices.Count == 0)
                {
                    return;
                }

                string testNo = lvwLab.Items[lvwLab.SelectedIndices[0]].Tag.ToString();
                
                // 查询检查结果
                DataSet ds = GVars.HISDataSrv.GetLabTestResult(testNo);
                
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return;
                }

                DataRow[] drFind = ds.Tables[0].Select("", "ITEM_NO ASC");
                DataRow   dr     = null;
                for(int i = 0; i < drFind.Length; i++)
                {
                    dr = drFind[i];
                    
                    ListViewItem item = new ListViewItem(dr["REPORT_ITEM_NAME"].ToString());
                    item.SubItems.Add(dr["RESULT"].ToString() + dr["UNITS"].ToString());
                    item.SubItems.Add(dr["PRINT_CONTEXT"].ToString());
                    
                    if (dr["ABNORMAL_INDICATOR"] != DBNull.Value)
                    {
                        if (dr["ABNORMAL_INDICATOR"].ToString().Equals("H") == true)
                        {
                            item.ForeColor = Color.Red;
                        }
                        else if (dr["ABNORMAL_INDICATOR"].ToString().Equals("H") == true)
                        {
                            item.ForeColor = Color.Green;
                        }
                    }
                    
                    lvwResult.Items.Add(item);
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
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
    }
}