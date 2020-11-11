using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class OrderRemindFrm : Form
    {

        #region   变量
        private DataSet dsPatientInfoList = null;
        private OrderRemindList comData = new OrderRemindList();       //调用新开医嘱
        private int patientIndex = -1;
        string grpChar = new string((char)128, 1);
        #endregion
        public OrderRemindFrm()
        {
            InitializeComponent();
            this.Click += new EventHandler(Close);

        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderRemindfrm_Load(object sender, EventArgs e)
        {
            try
            {
                initDisp();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 单击窗体时退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void initDisp()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dsPatientInfoList = comData.GetPatientInfoList(GVars.App.DeptCode);
                SetPatientButtons();
                if (btnCurrPatient.Text.Length > 0 && btnCurrPatient.Text != "空")
                {
                    string bedNo = btnCurrPatient.Text.Substring(0, btnCurrPatient.Text.IndexOf("床"));
                    showOrderRemind(bedNo);
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

        public void SetPatientButtons()
        {
            btnCurrPatient.Text = "空";
            btnPrePatient.Text = " <";
            btnNextPatient.Text = "> ";
            btnCurrPatient.Enabled = false;
            btnPrePatient.Enabled = false;
            btnNextPatient.Enabled = false;

            if (dsPatientInfoList.Tables.Count == 0 || dsPatientInfoList.Tables[0].Rows.Count == 0)
            {
                return;
            }
            if (patientIndex < 0)
            {
                patientIndex = 0;
            }
            //当前病人
            if (patientIndex >= 0 && patientIndex < dsPatientInfoList.Tables[0].Rows.Count)
            {
                btnCurrPatient.Enabled = true;
                btnCurrPatient.Text = dsPatientInfoList.Tables[0].Rows[patientIndex]["BED_LABEL"].ToString() + "床";
            }

            // 如果有后面的病人
            if (patientIndex < dsPatientInfoList.Tables[0].Rows.Count - 1)
            {
                btnNextPatient.Enabled = true;
                btnNextPatient.Text += dsPatientInfoList.Tables[0].Rows[patientIndex + 1]["BED_LABEL"].ToString() + "床";
            }

            // 如果有前面的病人
            if (patientIndex > 0)
            {
                btnPrePatient.Enabled = true;
                btnPrePatient.Text = dsPatientInfoList.Tables[0].Rows[patientIndex - 1]["BED_LABEL"].ToString() + "床" + btnPrePatient.Text;
            }
        }


        //显示医嘱提醒列表
        private void showOrderRemind(string bedLabel)
        {
            try
            {
                lvwOrderRemindList.BeginUpdate();

                lvwOrderRemindList.Items.Clear();
                DataSet dsOrderRemind = comData.GetOrderRemindInfo(GVars.App.DeptCode, bedLabel);
                if (dsOrderRemind == null || dsOrderRemind.Tables.Count == 0)
                {
                    return;
                }
                string orderNo = string.Empty;
                string sort = "ORDER_NO,ORDER_SUB_NO,NAME";
                DataRow[] dataRow = dsOrderRemind.Tables[0].Select(string.Empty, sort);
                foreach (DataRow dr in dataRow)
                {
                    ListViewItem item = null;
                    if (!dr["ORDER_NO"].ToString().Equals(orderNo))
                    {
                        item = new ListViewItem(grpChar);
                    }
                    else
                    {
                        item = new ListViewItem();
                    }
                    if ((dr["ORDER_STATUS"].ToString().Equals("3") && dr["STOP_DATE_TIME"].ToString() != "" && dr["NURSE"].ToString() != "" && dr["STOP_NURSE"].ToString() != "")
                        || (dr["ORDER_STATUS"].ToString().Equals("6") && dr["STOP_DATE_TIME"].ToString() != "" && dr["NURSE"].ToString() != "" && dr["STOP_DOCTOR"].ToString() != ""))
                    {
                        item.BackColor = Color.FromArgb(183, 0, 0);
                    }
                    orderNo = dr["ORDER_NO"].ToString();
                    item.SubItems.Add(dr["ORDER_TEXT"].ToString());                  //医嘱内容
                    item.SubItems.Add(dr["DOSAGE"].ToString());                      //剂量
                    item.SubItems.Add(dr["ADMINISTRATION"].ToString());              //途径 
                    item.SubItems.Add(dr["ENTER_DATE_TIME"].ToString());
                    item.SubItems.Add(dr["STOP_DATE_TIME"].ToString());
                    item.SubItems.Add(dr["DOCTOR"].ToString());                      //医生
                    lvwOrderRemindList.Items.Add(item);
                    mnuPatient.Text = dr["NAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                lvwOrderRemindList.EndUpdate();
            }
        }

        private void mnuCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void btnPrePatient_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                patientIndex--;
                string bedLabel = btnPrePatient.Text.Substring(0, btnPrePatient.Text.IndexOf("床"));
                showOrderRemind(bedLabel.Trim());
                SetPatientButtons();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnNextPatient_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                patientIndex++;
                string bedLabel = btnNextPatient.Text.Substring(2, btnNextPatient.Text.IndexOf("床") - 2);
                showOrderRemind(bedLabel.Trim());
                SetPatientButtons();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnListPatient_Click(object sender, EventArgs e)
        {
            OrderRemindPatientListFrm patinetListFrm = new OrderRemindPatientListFrm();
            patinetListFrm.DsPatientInfoList = dsPatientInfoList;
            if (patinetListFrm.ShowDialog() == DialogResult.OK)
            {
                string bedLabel = patinetListFrm.PatientBedLabel;
                showOrderRemind(bedLabel.Trim());
                ResetPatIndex(bedLabel);
                SetPatientButtons();

            }
        }

        /// <summary>
        /// 获取病人的位置
        /// </summary>
        public bool ResetPatIndex(string bedLabel)
        {
            if (dsPatientInfoList == null || dsPatientInfoList.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            for (int i = 0; i < dsPatientInfoList.Tables[0].Rows.Count; i++)
            {
                if (dsPatientInfoList.Tables[0].Rows[i]["BED_LABEL"].ToString().Trim().Equals(bedLabel))
                {
                    patientIndex = i;
                    return true;
                }
            }
            return false;
        }
    }
}