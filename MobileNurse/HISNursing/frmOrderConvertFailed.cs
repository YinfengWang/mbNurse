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
        
        #region ����
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
        /// ��������¼�
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
        /// ��ť[�ر�]
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
        /// ��ť[ҽ�����]
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
        /// ��ʾ���ʧ�ܵ�ҽ��
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
                        // ���� ���� ҽ���� ��ҽ���� ҽ������ Ƶ������ ������� ��ϸ���� �ƻ�ʱ�� ��������
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
        /// ��ȡ��������
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
        /// ��ȡ���˵Ĵ����
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="vistId"></param>
        /// <returns></returns>
        private string getPatientBedLabel(string patientId, string visitId)
        {
            string sql = string.Empty;

            sql += "SELECT BED_REC.BED_LABEL ";                                 // �����
            sql += "FROM PATS_IN_HOSPITAL, ";                                   // ��Ժ���˼�¼
            sql += "BED_REC ";                                              // ��λ��¼
            sql += "WHERE PATS_IN_HOSPITAL.WARD_CODE = BED_REC.WARD_CODE ";     // ���ڲ�������
            sql += "AND PATS_IN_HOSPITAL.BED_NO = BED_REC.BED_NO ";         // ����
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