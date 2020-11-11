using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SqlConvert = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class frmOrderConvertFailed : Form
    {
        
        #region 变量
        private DataSet dsOrders = null;
        private DataSet dsErr    = null;
        #endregion
        
        public frmOrderConvertFailed()
        {
            InitializeComponent();

            this.Load += new EventHandler( frmOrderConvertFailed_Load );
            this.FormClosing += new FormClosingEventHandler( frmOrderConvertFailed_FormClosing );
        }
        
        
        void frmOrderConvertFailed_FormClosing( object sender, FormClosingEventArgs e )
        {
            //this.Hide();
            //e.Cancel = true;
        }
        
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmOrderConvertFailed_Load( object sender, EventArgs e )
        {
            try
            {
                showSplitFailedOrder();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[关闭]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click( object sender, EventArgs e )
        {
            try
            {
                this.Hide();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[医嘱拆分]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click( object sender, EventArgs e )
        {
            try
            {
                showSplitFailedOrder();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
                
        /// <summary>
        /// 显示拆分失败的医嘱
        /// </summary>
        private void showSplitFailedOrder()
        {
            try
            {
                lvwResult.BeginUpdate();
                lvwResult.Items.Clear();
                
                string filter = string.Empty;
                DataRow[] drFind = null;
                
                ListViewItem item = null;
                string patientId  = string.Empty;
                
                GVars.OrderConvertor.GetData(ref dsOrders, ref dsErr);
                
                if (dsErr == null || dsErr.Tables.Count == 0)
                {
                    return;
                }
                
                if (dsOrders == null || dsOrders.Tables.Count == 0)
                {
                    return;
                }
                
                foreach(DataRow dr in dsErr.Tables[0].Rows)
                {
                    filter = "PATIENT_ID = " + SqlConvert.SqlConvert(dr["PATIENT_ID"].ToString())
                        + "AND VISIT_ID = " + SqlConvert.SqlConvert(dr["VISIT_ID"].ToString())
                        + "AND ORDER_NO = " + SqlConvert.SqlConvert(dr["ORDER_NO"].ToString())
                        + "AND ORDER_SUB_NO = " + SqlConvert.SqlConvert(dr["ORDER_SUB_NO"].ToString());
                    
                    drFind = dsOrders.Tables[0].Select(filter);
                    
                    if (drFind.Length > 0)
                    {
                        // 床标 姓名 医嘱号 子医嘱号 医嘱内容 频率描述 间隔描述 详细描述 计划时间 错误描述
                        if (patientId.Equals(dr["PATIENT_ID"].ToString()) == false)
                        {
                            patientId = dr["PATIENT_ID"].ToString();

                            item = new ListViewItem(getPatientBedLabel(dr["PATIENT_ID"].ToString(), dr["VISIT_ID"].ToString()));
                            item.SubItems.Add(getPatientName(dr["PATIENT_ID"].ToString()));                                                        
                        }
                        else
                        {
                            item = new ListViewItem(string.Empty);
                            item.SubItems.Add(string.Empty);
                        }
                        
                        item.SubItems.Add(dr["ORDER_NO"].ToString());
                        item.SubItems.Add(dr["ORDER_SUB_NO"].ToString());
                        item.SubItems.Add(drFind[0]["ORDER_TEXT"].ToString());
                        item.SubItems.Add(drFind[0]["FREQUENCY"].ToString());
                        item.SubItems.Add(drFind[0]["FREQ_COUNTER"].ToString() + "/" + drFind[0]["FREQ_INTERVAL"].ToString() 
                                    + drFind[0]["FREQ_INTERVAL_UNIT"].ToString());
                        item.SubItems.Add(drFind[0]["FREQ_DETAIL"].ToString());
                        item.SubItems.Add(drFind[0]["PERFORM_SCHEDULE"].ToString());
                        item.SubItems.Add(dr["ERR_DESC"].ToString());
                        
                        lvwResult.Items.Add(item);
                    }
                }
            }
            finally
            {
                lvwResult.EndUpdate();
            }    
        }
        
        
        /// <summary>
        /// 获取病人名称
        /// </summary>
        /// <returns></returns>
        private string getPatientName(string patientId)
        {
            string sql = "SELECT NAME FROM PAT_MASTER_INDEX WHERE PATIENT_ID = " + SqlConvert.SqlConvert(patientId);
            
            if (GVars.OracleAccess.SelectValue(sql) == true)
            {
                return GVars.OracleAccess.GetResult(0);
            }
            else
            {
                return string.Empty;
            }
        }
        
        
        /// <summary>
        /// 获取病人的床标号
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="vistId"></param>
        /// <returns></returns>
        private string getPatientBedLabel(string patientId, string visitId)
        {
            string sql = string.Empty;

            sql += "SELECT BED_REC.BED_LABEL ";                                 // 床标号
            sql += "FROM PATS_IN_HOSPITAL, ";                                   // 在院病人记录
            sql += "BED_REC ";                                              // 床位记录
            sql += "WHERE PATS_IN_HOSPITAL.WARD_CODE = BED_REC.WARD_CODE ";     // 所在病房代码
            sql += "AND PATS_IN_HOSPITAL.BED_NO = BED_REC.BED_NO ";         // 床号
            sql += "AND PATS_IN_HOSPITAL.PATIENT_ID = " + SqlConvert.SqlConvert(patientId);
            sql += "AND PATS_IN_HOSPITAL.VISIT_ID = " + SqlConvert.SqlConvert(visitId);

            if (GVars.OracleAccess.SelectValue(sql) == true)
            {
                return GVars.OracleAccess.GetResult(0);
            }
            else
            {
                return string.Empty;
            }            
        }
    }
}