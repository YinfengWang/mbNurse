// ***********************************************************************
// Assembly         : NursingTransferRecord
// Author           : LPD
// Created          : 10-06-2015
//
// Last Modified By : LPD
// Last Modified On : 10-06-2015
// ***********************************************************************
// <copyright file="AddNursingRecode.cs" company="陕西天健源达信息科技有限公司">
//     Copyright (c) 陕西天健源达信息科技有限公司. All rights reserved.
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
using System.Collections;

/// <summary>
/// The HISPlus namespace.
/// </summary>
namespace HISPlus
{
    /// <summary>
    /// Class AddNursingRecode.
    /// </summary>
    public partial class AddNursingRecode : DevExpress.XtraEditors.XtraForm
    {
        private bool _isAddRecode = true; //是否是新增的记录
        private string patient_id = string.Empty; //病人号
        private string visit_id = string.Empty;
        DataSet dsPatientInfo = new DataSet(); //病人信息
        private NursingTransferRecordFrm p_f1;
        private NursingTransferEntity netEntity;   //交班记录实体
        /// <summary>
        /// Initializes a new instance of the <see cref="AddNursingRecode"/> class.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <param name="ntrf">The NTRF.</param>
        public AddNursingRecode(string option, NursingTransferRecordFrm ntrf, NursingTransferEntity ntfe)
        {
            InitializeComponent();

            //判断标题显示内容

            if (option == "add")
            {
                this.Text = "添加交班记录";
                _isAddRecode = true;
            }
            else
            {
                this.Text = "编辑交班记录";
                btnAdd.Text = "更新";
                _isAddRecode = false;
            }
            netEntity = ntfe;
            p_f1 = ntrf;

            //默认时间
            deditDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string selectSql = " SELECT * FROM MOBILE.PATIENT_INFO  A WHERE A.WARD_CODE='" + GVars.User.DeptCode + "' ";
            dsPatientInfo = GVars.OracleAccess.SelectData(selectSql);
            cboxEvent.SelectedIndex = 0;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 添加新纪录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBedNo.Text.Trim()))
            {
                MessageBox.Show("床标不能为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("姓名不能为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(txtZd.Text))
            {
                MessageBox.Show("诊断不能为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(txtJbz.Text.Trim()))
            {
                MessageBox.Show("交班者不能为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(txtJieBz.Text.Trim()))
            {
                MessageBox.Show("接班者不能为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string rguid = string.Empty;

                if (_isAddRecode)
                {
                    rguid = System.Guid.NewGuid().ToString("N").ToUpper();
                }
                else
                {
                    rguid = netEntity.RGUID;
                }
                string ward_code = GVars.User.DeptCode;  //病区代码
                string bed_label = txtBedNo.Text.Trim();   //床号
                string name = txtName.Text.Trim();      //姓名
                string diagnosis_name = txtZd.Text.Trim();  //诊断
                //string dynamic_situation = txtDtqk.Text.Trim(); //动态情况
                string transfer_content = txtHzcz.Text.Trim();  //患者处置、病情观察及交班内容
                string transferer = txtJbz.Text.Trim();         //交班者
                string receiveer = txtJieBz.Text.Trim();        //接班者
                string execute_time = deditDate.Text.Trim();    //时间
                string nurse_event = cboxEvent.Text.Trim();     //护理事件


                string pID = dsPatientInfo.Tables[0].Select(" BED_LABEL='" + txtBedNo.Text.Trim() + "'")[0]["PATIENT_ID"].ToString();
                string vID = dsPatientInfo.Tables[0].Select(" BED_LABEL='" + txtBedNo.Text.Trim() + "'")[0]["VISIT_ID"].ToString();
                string strNurseClass = GetPatientNurseClass(pID, vID);
                #region SQL

                string strSql = string.Empty;

                if (_isAddRecode)
                {
                    strSql = @"INSERT INTO NURSING_TRANSFER_RECORD  " +
                               "  (RGUID,WARD_CODE, BED_NO, NAME, DIAGNOSIS_NAME,   " +
                               "   TRANSFER_CONTENT, TRANSFERER, RECEIVEER, EXECUTE_TIME,IS_ABOLISH,PATIENT_ID,VISIT_ID,NURSE_EVENT,NURSE_CLASS)  " +
                               "  VALUES  " +
                               "  ('" + rguid + "','" + ward_code + "', '" + bed_label + "', '" + name + "', '" + diagnosis_name + "',  " +
                               "   '" + transfer_content + "', '" + transferer + "', '" + receiveer + "', TO_DATE('" + execute_time + "','YYYY-MM-DD HH24:MI:SS'),'F','" + patient_id + "','" + visit_id + "','" + nurse_event + "','" + strNurseClass + "')";

                }
                else
                {
                    strSql = @"UPDATE NURSING_TRANSFER_RECORD     " +
                                    " SET  WARD_CODE = '" + ward_code + "',     " +
                                    "   BED_NO = '" + bed_label + "',     " +
                                    "   NAME = '" + name + "',     " +
                                    "   DIAGNOSIS_NAME = '" + diagnosis_name + "',     " +
                                    "   TRANSFER_CONTENT = '" + transfer_content + "',     " +
                                    "   TRANSFERER = '" + transferer + "',     " +
                                    "   RECEIVEER = '" + receiveer + "',     " +
                                    "   EXECUTE_TIME = TO_DATE('" + execute_time + "','YYYY-MM-DD HH24:MI:SS'),     " +
                                    "   IS_ABOLISH = '" + netEntity.IS_ABOLISH + "',     " +
                                    "   NURSE_EVENT = '" + nurse_event + "',     " +
                                    "   NURSE_CLASS='" + strNurseClass + "'     " +
                                    "   WHERE RGUID = '" + rguid + "'";
                }

                GVars.OracleAccess.ExecuteNoQuery(strSql);

                #endregion

                //刷新父窗体
                this.Close();
                p_f1.Refresh_Method();
            }
        }

        /// <summary>
        /// 根据床号显示病人姓名及诊断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBedNo_EditValueChanged(object sender, EventArgs e)
        {
            if (dsPatientInfo != null && dsPatientInfo.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(txtBedNo.Text.Trim()))
                {
                    DataRow[] drArr = dsPatientInfo.Tables[0].Select(" bed_label='" + txtBedNo.Text.Trim() + "'");
                    if (drArr.Length > 0 && !string.IsNullOrEmpty(txtBedNo.Text.Trim()))
                    {
                        //显示病人信息
                        txtName.Text = drArr[0]["NAME"].ToString();//dsPatientInfo.Tables[0].Select(" BED_NO='" + txtBedNo.Text.Trim() + "'")[0]["NAME"].ToString();
                        txtZd.Text = drArr[0]["DIAGNOSIS"].ToString();//dsPatientInfo.Tables[0].Select(" BED_NO='" + txtBedNo.Text.Trim() + "'")[0]["DIAGNOSIS"].ToString();
                        patient_id = drArr[0]["PATIENT_ID"].ToString();//dsPatientInfo.Tables[0].Select(" BED_NO='" + txtBedNo.Text.Trim() + "'")[0]["PATIENT_ID"].ToString();
                        visit_id = drArr[0]["VISIT_ID"].ToString();//dsPatientInfo.Tables[0].Select(" BED_NO='" + txtBedNo.Text.Trim() + "'")[0]["PATIENT_ID"].ToString();
                    }
                    else
                    {
                        CleanTextValue();
                    }
                }
                else
                {
                    CleanTextValue();
                }
            }
            else
            {
                CleanTextValue();
            }
        }

        /// <summary>
        /// 清空Text的值
        /// </summary>
        private void CleanTextValue()
        {
            txtName.Text = ""; txtZd.Text = ""; patient_id = ""; visit_id = "";
        }

        private void AddNursingRecode_Load(object sender, EventArgs e)
        {
            if (!_isAddRecode && netEntity != null)
            {
                this.deditDate.Text = netEntity.EXECUTE_TIME;
                this.txtBedNo.Text = netEntity.BED_NO;
                this.txtName.Text = netEntity.NAME;
                this.txtZd.Text = netEntity.DIAGNOSIS_NAME;
                //this.txtDtqk.Text = netEntity.DYNAMIC_SITUATION;
                this.txtHzcz.Text = netEntity.TRANSFER_CONTENT;
                this.txtJbz.Text = netEntity.TRANSFERER;
                this.txtJieBz.Text = netEntity.RECEIVEER;
                cboxEvent.Text = netEntity.NURSE_EVENT;
            }
        }

        /// <summary>
        /// 获取用户护理级别
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visidID"></param>
        /// <returns></returns>
        public string GetPatientNurseClass(string patientID, string visidID)
        {
            string strResultValue = string.Empty;
            string strSql = "SELECT A.NURSING_CLASS_NAME FROM PATIENT_INFO A WHERE A.WARD_CODE='" + GVars.User.DeptCode + "' AND A.PATIENT_ID='" + patientID + "' AND A.VISIT_ID='" + visidID + "'";
            DataSet dsNurseClass = GVars.OracleAccess.SelectData(strSql);
            if (dsNurseClass != null && dsNurseClass.Tables[0].Rows.Count > 0)
            {
                strResultValue = dsNurseClass.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                string strSqlTombstone = "SELECT NURSING_CLASS_NAME FROM PATIENT_INFO_TOMBSTONE WHERE WARD_CODE='" + GVars.User.DeptCode + "' AND  PATIENT_ID = '" + patientID + "' AND VISIT_ID = '" + visidID + "'  ";
                DataSet dsNurseClassTombstone = GVars.OracleAccess.SelectData(strSqlTombstone);
                strResultValue = dsNurseClassTombstone.Tables[0].Rows[0][0].ToString();
            }
            return strResultValue;
        }


    }
}