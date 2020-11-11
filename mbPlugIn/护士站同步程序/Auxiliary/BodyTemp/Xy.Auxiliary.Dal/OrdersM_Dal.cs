// ***********************************************************************
// Assembly         : Xy.Auxiliary.Dal
// Author           : LPD
// Created          : 12-07-2015
//
// Last Modified By : LPD
// Last Modified On : 01-16-2016
// ***********************************************************************
// <copyright file="OrdersM_Dal.cs" company="心医国际(西安)">
//     Copyright (c) 心医国际(西安). All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using Xy.Auxiliary.PubCls;
using Xy.Auxiliary.Entity;
using System.Collections;
using System;

/// <summary>
/// The Dal namespace.
/// </summary>
namespace Xy.Auxiliary.Dal
{
    /// <summary>
    /// Class OrdersMDal.
    /// </summary>
    public class OrdersM_Dal
    {
        /// <summary>
        /// 实例Mobile连接
        /// </summary>
        DBHelper dbMobile = new DBHelper(ConnFlag.mobile.ToString());

        /// <summary>
        /// 实例His连接
        /// </summary>
        DBHelper dbHis = new DBHelper(ConnFlag.his.ToString());

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersM_Dal"/> class.
        /// </summary>
        public OrdersM_Dal()
        { }

        /// <summary>
        /// 判断是否医嘱是否已经同步过【Mobile】
        /// </summary>
        /// <param name="patientId">The patient unique identifier.</param>
        /// <param name="visitId">The visit unique identifier.</param>
        /// <param name="orderNo">The order no.</param>
        /// <param name="orderSubNo">医嘱子序号</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Exists(string patientId, decimal visitId, decimal orderNo, decimal orderSubNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM ORDERS_M");
            strSql.Append(" WHERE PATIENT_ID='" + patientId + "' AND VISIT_ID='" + visitId + "' AND ORDER_NO='" + orderNo + "' AND ORDER_SUB_NO='" + orderSubNo + "'");
            DataTable dt = dbMobile.GetData(strSql.ToString());
            int _rowCount = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                _rowCount = Convert.ToInt32(dt.Rows[0][0]);
                
            }
            return _rowCount > 0;
        }

        /// <summary>
        /// 获取HIS医嘱信息【HIS】
        /// </summary>
        /// <param name="patientId">The patient unique identifier.</param>
        /// <param name="visitId">The visit unique identifier.</param>
        /// <param name="orderNo">The order no.</param>
        /// <returns>DataTable.</returns>
        public DataTable GetHisOrders(string patientId, decimal visitId, decimal orderNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  SELECT * FROM  ORDERS A,ORDER_CLASS_DICT B,ORDER_STATUS_DICT C WHERE A.ORDER_CLASS=B.ORDER_CLASS_CODE AND A.ORDER_STATUS = C.ORDER_STATUS_CODE");
            strSql.Append(" AND A.PATIENT_ID='" + patientId + "' AND A.VISIT_ID=" + visitId + " AND A.ORDER_NO=" + orderNo + "");
            DataTable dtHisOrders = dbHis.GetData(strSql.ToString());
            return dtHisOrders;
        }

        /// <summary>
        /// 获取Mobile遗嘱信息
        /// </summary>
        /// <param name="patientId">病人ID</param>
        /// <param name="visitId">住院次数</param>
        /// <param name="orderNo">医嘱号</param>
        /// <returns>DataTable.</returns>
        public DataTable GetMobileOrders_M(string patientId, decimal visitId, decimal orderNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  SELECT * FROM  ORDERS_M A");
            strSql.Append(" WHERE A.PATIENT_ID='" + patientId + "' AND A.VISIT_ID=" + visitId + " AND A.ORDER_NO=" + orderNo + " ");
            DataTable dtMobileOrders = dbMobile.GetData(strSql.ToString());
            return dtMobileOrders;
        }


        /// <summary>
        /// 将HIS的医嘱同步至MOBILE库
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool SyncHisOrdersToMobile(DataRow dr)
        {
            //病人所属病区
            string patWardCode = GetWardCode(dr["PATIENT_ID"].ToString(), dr["VISIT_ID"].ToString());
            ArrayList alst = new ArrayList();
            //if (dt != null && dt.Rows.Count > 0)
            //{

            //    for (int m = 0; m < dt.Rows.Count; m++)
            //    {
            string insSql = string.Empty; //插入数据库的SQL
            insSql += " INSERT INTO ORDERS_M( ";
            insSql += " WARD_CODE,PATIENT_ID,VISIT_ID,ORDER_NO,ORDER_SUB_NO,REPEAT_INDICATOR,ORDER_CLASS,ORDER_CLASS_NAME,ORDER_STATUS,ORDER_STATUS_NAME,ORDER_TEXT,ORDER_CODE,DOSAGE,DOSAGE_UNITS,ADMINISTRATION,DURATION,DURATION_UNITS,START_DATE_TIME,STOP_DATE_TIME,FREQUENCY,FREQ_COUNTER,FREQ_INTERVAL,FREQ_INTERVAL_UNIT,FREQ_DETAIL,DEGREE,PERFORM_SCHEDULE,PERFORM_RESULT,ORDERING_DEPT,DOCTOR,STOP_DOCTOR,NURSE,STOP_NURSE,CREATE_TIMESTAMP,PROCESSING_NURSE,REMARK)";
            insSql += " VALUES ( ";
            insSql += " '" + patWardCode + "',   " +
                          "  '" + dr["PATIENT_ID"] + "',   " +
                          "  '" + dr["VISIT_ID"] + "',   " +
                          "  '" + dr["ORDER_NO"] + "',   " +
                          "  '" + dr["ORDER_SUB_NO"] + "',   " +
                          "  '" + dr["REPEAT_INDICATOR"] + "',   " +
                          "  '" + dr["ORDER_CLASS"] + "',   " +
                          "  '" + dr["ORDER_CLASS_NAME"] + "',   " +
                          "  '" + dr["ORDER_STATUS"] + "',   " +
                          "  '" + dr["ORDER_STATUS_NAME"] + "',   " +
                          "  '" + dr["ORDER_TEXT"] + "',   " +
                          "  '" + dr["ORDER_CODE"] + "',   " +
                          "  '" + dr["DOSAGE"] + "',   " +
                          "  '" + dr["DOSAGE_UNITS"] + "',   " +
                          "  '" + dr["ADMINISTRATION"] + "',   " +
                          "  '" + dr["DURATION"] + "',   " +
                          "  '" + dr["DURATION_UNITS"] + "',   " +
                          "  TO_DATE('" + dr["START_DATE_TIME"] + "','YYYY-MM-DD HH24:MI:SS'),   " +
                          "  TO_DATE('" + dr["STOP_DATE_TIME"] + "','YYYY-MM-DD HH24:MI:SS'),   " +
                          "  '" + dr["FREQUENCY"] + "',   " +
                          "  '" + dr["FREQ_COUNTER"] + "',   " +
                          "  '" + dr["FREQ_INTERVAL"] + "',   " +
                          "  '" + dr["FREQ_INTERVAL_UNIT"] + "',   " +
                          "  '" + dr["FREQ_DETAIL"] + "',   " +
                          "  '" + dr["DEGREE"] + "',   " +
                          "  '" + dr["PERFORM_SCHEDULE"] + "',   " +
                          "  '" + dr["PERFORM_RESULT"] + "',   " +
                          "  '" + dr["ORDERING_DEPT"] + "',   " +
                          "  '" + dr["DOCTOR"] + "',   " +
                          "  '" + dr["STOP_DOCTOR"] + "',   " +
                          "  '" + dr["NURSE"] + "',   " +
                          "  '" + dr["STOP_NURSE"] + "',   " +
                          "  TO_DATE('" + DateTime.Now.ToString() + "','YYYY-MM-DD HH24:MI:SS'),   " +
                          "  '" + dr["PROCESSING_NURSE"] + "',   " +
                          "  'Nurse_Client_Sync'";
            insSql += " ) ";

            alst.Add(insSql);
            //    }
            //}

            return dbMobile.Transaction(alst);
        }

        /// <summary>
        /// 发现HIS中医嘱状态和停止执行时间与MOBILE不一致，则更新MOBILE的医嘱与HIS一致。
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool UpdMobileOrdersFromHis(DataRow dr)
        {
            ArrayList alst = new ArrayList();
            string strUpd = "UPDATE ORDERS_M   SET " +
                            "       REPEAT_INDICATOR ='" + dr["REPEAT_INDICATOR"] + "',     " +
                            "       ORDER_CLASS = '" + dr["ORDER_CLASS"] + "',     " +
                            "       ORDER_CLASS_NAME = '" + dr["ORDER_CLASS_NAME"] + "',     " +
                            "       ORDER_STATUS = '" + dr["ORDER_STATUS"] + "',     " +
                            "       ORDER_STATUS_NAME = '" + dr["ORDER_STATUS_NAME"] + "',     " +
                            "       ORDER_TEXT = '" + dr["ORDER_TEXT"] + "',     " +
                            "       ORDER_CODE = '" + dr["ORDER_CODE"] + "',     " +
                            "       DOSAGE = '" + dr["DOSAGE"] + "',     " +
                            "       DOSAGE_UNITS = '" + dr["DOSAGE_UNITS"] + "',     " +
                            "       ADMINISTRATION = '" + dr["ADMINISTRATION"] + "',     " +
                            "       DURATION = '" + dr["DURATION"] + "',     " +
                            "       DURATION_UNITS = '" + dr["DURATION_UNITS"] + "',     " +
                            "       START_DATE_TIME=TO_DATE('" + dr["START_DATE_TIME"] + "','YYYY-MM-DD HH24:MI:SS'),   " +
                            "       STOP_DATE_TIME=TO_DATE('" + dr["STOP_DATE_TIME"] + "','YYYY-MM-DD HH24:MI:SS'),   " +
                            "       FREQUENCY = '" + dr["FREQUENCY"] + "',     " +
                            "       FREQ_COUNTER = '" + dr["FREQ_COUNTER"] + "',     " +
                            "       FREQ_INTERVAL = '" + dr["FREQ_INTERVAL"] + "',     " +
                            "       FREQ_INTERVAL_UNIT = '" + dr["FREQ_INTERVAL_UNIT"] + "',     " +
                            "       FREQ_DETAIL = '" + dr["FREQ_DETAIL"] + "',     " +
                            "       DEGREE = '" + dr["DEGREE"] + "',     " +
                            "       PERFORM_SCHEDULE = '" + dr["PERFORM_SCHEDULE"] + "',     " +
                            "       PERFORM_RESULT = '" + dr["PERFORM_RESULT"] + "',     " +
                            "       ORDERING_DEPT = '" + dr["ORDERING_DEPT"] + "',     " +
                            "       DOCTOR = '" + dr["DOCTOR"] + "',     " +
                            "       STOP_DOCTOR = '" + dr["STOP_DOCTOR"] + "',     " +
                            "       NURSE = '" + dr["NURSE"] + "',     " +
                            "       STOP_NURSE = '" + dr["STOP_NURSE"] + "',     " +
                            "       PROCESSING_NURSE = '" + dr["PROCESSING_NURSE"] + "'     " +
                            "       WHERE PATIENT_ID = '" + dr["PATIENT_ID"] + "'     " +
                            "   AND VISIT_ID = '" + dr["VISIT_ID"] + "'     " +
                            "   AND ORDER_NO = '" + dr["ORDER_NO"] + "'    " +
                            "   AND ORDER_SUB_NO ='" + dr["ORDER_SUB_NO"] + "' ";
            alst.Add(strUpd);
            return dbMobile.Transaction(alst);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="patientId">The patient unique identifier.</param>
        /// <param name="visitId">The visit unique identifier.</param>
        /// <param name="orderNo">The order no.</param>
        /// <param name="orderSubNo">The order sub no.</param>
        /// <returns>OrdersM_Entity.</returns>
        public OrdersM_Entity GetOrdersModel(string patientId, decimal visitId, decimal orderNo, decimal orderSubNo)
        {
            OrdersM_Entity orderEntity = new OrdersM_Entity();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ORDERS_M");
            strSql.Append(" WHERE PATIENT_ID='" + patientId + "' AND VISIT_ID='" + visitId + "' AND ORDER_NO='" + orderNo + "' AND ORDER_SUB_NO='" + orderSubNo + "'");
            DataTable dt = dbMobile.GetData(strSql.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    orderEntity.WARD_CODE = dt.Rows[m]["WARD_CODE"].ToString();
                    orderEntity.PATIENT_ID = dt.Rows[m]["PATIENT_ID"].ToString();
                    orderEntity.VISIT_ID = Convert.ToDecimal(dt.Rows[m]["VISIT_ID"].ToString());
                    orderEntity.ORDER_NO = Convert.ToDecimal(dt.Rows[m]["ORDER_NO"].ToString());
                    orderEntity.ORDER_SUB_NO = Convert.ToDecimal(dt.Rows[m]["ORDER_SUB_NO"].ToString());
                    orderEntity.REPEAT_INDICATOR = Convert.ToDecimal(dt.Rows[m]["REPEAT_INDICATOR"].ToString());
                    orderEntity.ORDER_CLASS = dt.Rows[m]["ORDER_CLASS"].ToString();
                    orderEntity.ORDER_CLASS_NAME = dt.Rows[m]["ORDER_CLASS_NAME"].ToString();
                    orderEntity.ORDER_STATUS = dt.Rows[m]["ORDER_STATUS"].ToString();
                    orderEntity.ORDER_STATUS_NAME = dt.Rows[m]["ORDER_STATUS_NAME"].ToString();
                    orderEntity.ORDER_TEXT = dt.Rows[m]["ORDER_TEXT"].ToString();
                    orderEntity.ORDER_CODE = dt.Rows[m]["ORDER_CODE"].ToString();
                    orderEntity.DOSAGE = Convert.ToDecimal(dt.Rows[m]["DOSAGE"].ToString());
                    orderEntity.DOSAGE_UNITS = dt.Rows[m]["DOSAGE_UNITS"].ToString();
                    orderEntity.ADMINISTRATION = dt.Rows[m]["ADMINISTRATION"].ToString();
                    //orderEntity.DURATION = Convert.ToDecimal(dt.Rows[m]["DURATION"].ToString());
                    //orderEntity.DURATION_UNITS = dt.Rows[m]["DURATION_UNITS"].ToString();
                    orderEntity.START_DATE_TIME = Convert.ToDateTime(dt.Rows[m]["START_DATE_TIME"]);

                    if (!string.IsNullOrEmpty(dt.Rows[m]["STOP_DATE_TIME"].ToString()))
                    {
                        orderEntity.STOP_DATE_TIME = Convert.ToDateTime(dt.Rows[m]["STOP_DATE_TIME"].ToString());
                    }
                    orderEntity.FREQUENCY = dt.Rows[m]["FREQUENCY"].ToString();
                    orderEntity.FREQ_COUNTER = Convert.ToDecimal(dt.Rows[m]["FREQ_COUNTER"].ToString());
                    orderEntity.FREQ_INTERVAL = Convert.ToDecimal(dt.Rows[m]["FREQ_INTERVAL"].ToString());
                    orderEntity.FREQ_INTERVAL_UNIT = dt.Rows[m]["FREQ_INTERVAL_UNIT"].ToString();
                    orderEntity.FREQ_DETAIL = dt.Rows[m]["FREQ_DETAIL"].ToString();
                    //orderEntity.DEGREE = Convert.ToDecimal(dt.Rows[m]["DEGREE"].ToString());
                    orderEntity.PERFORM_SCHEDULE = dt.Rows[m]["PERFORM_SCHEDULE"].ToString();
                    orderEntity.PERFORM_RESULT = dt.Rows[m]["PERFORM_RESULT"].ToString();
                    orderEntity.ORDERING_DEPT = dt.Rows[m]["ORDERING_DEPT"].ToString();
                    orderEntity.DOCTOR = dt.Rows[m]["DOCTOR"].ToString();
                    orderEntity.STOP_DOCTOR = dt.Rows[m]["STOP_DOCTOR"].ToString();
                    orderEntity.NURSE = dt.Rows[m]["NURSE"].ToString();
                    orderEntity.STOP_NURSE = dt.Rows[m]["STOP_NURSE"].ToString();
                    //orderEntity.CREATE_TIMESTAMP = Convert.ToDateTime(dt.Rows[m]["CREATE_TIMESTAMP"].ToString());
                    //orderEntity.UPDATE_TIMESTAMP = Convert.ToDateTime(dt.Rows[m]["UPDATE_TIMESTAMP"].ToString());
                    orderEntity.PROCESSING_NURSE = dt.Rows[m]["PROCESSING_NURSE"].ToString();
                }
            }
            return orderEntity;
        }

        /// <summary>
        /// 根据ini文件中的医嘱在orders_m表找到医嘱内容
        /// </summary>
        /// <param name="newOrders">The new orders.</param>
        /// <returns>DataTable.</returns>
        public DataTable GetIniFileOrdersDt(string newOrders)
        {
            string[] arrOrders = newOrders.Split("|".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            string strSql = string.Empty;
            if (arrOrders.Length > 0)
            {

                for (int m = 0; m < arrOrders.Length; m++)
                {
                    string patientID = arrOrders[m].Split(',')[0];    //病人ID
                    string visitID = arrOrders[m].Split(',')[1];      //住院次数
                    string orderNo = arrOrders[m].Split(',')[2];      //医嘱序号
                    strSql += "SELECT * FROM ORDERS_M A  WHERE A.PATIENT_ID='" + patientID + "' AND A.VISIT_ID=" + visitID + " AND A.ORDER_NO=" + orderNo + " ";

                    if (arrOrders.Length > 1 && m != arrOrders.Length - 1)
                        strSql += " UNION ";
                }
            }

            return dbMobile.GetData(strSql);
        }


        /// <summary>
        /// 根据病人ID，和住院次数，获取到病区代码
        /// </summary>
        /// <param name="patientId">病人ID</param>
        /// <param name="visitId">住院次数</param>
        /// <returns>System.String.</returns>
        public string GetWardCode(string patientId, string visitId)
        {
            string strSql = " SELECT A.WARD_CODE FROM PATIENT_INFO A WHERE A.PATIENT_ID='" + patientId + "' AND A.VISIT_ID='" + visitId + "'";
            object objWardCode = dbMobile.ExecuteScalar(strSql);
            return (objWardCode != null) ? objWardCode.ToString() : "";
        }
    }
}