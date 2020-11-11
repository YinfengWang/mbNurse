// ***********************************************************************
// Assembly         : Xy.Auxiliary.Dal
// Author           : LPD
// Created          : 12-07-2015
//
// Last Modified By : LPD
// Last Modified On : 12-21-2015
// ***********************************************************************
// <copyright file="OrdersExecute_Dal.cs" company="心医国际(西安)">
//     Copyright (c) 心医国际(西安). All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Xy.Auxiliary.PubCls;

/// <summary>
/// The Dal namespace.
/// </summary>
namespace Xy.Auxiliary.Dal
{
    /// <summary>
    /// Class OrdersExecuteDal.
    /// </summary>
    public class OrdersExecute_Dal
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
        /// 判断是否医嘱是否已经拆分过【Mobile】
        /// </summary>
        /// <param name="patientId">The patient unique identifier.</param>
        /// <param name="visitId">The visit unique identifier.</param>
        /// <param name="orderNo">The order no.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool OrdersExecuteExists(string patientId, decimal visitId, decimal orderNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ORDERS_EXECUTE");
            strSql.Append(" WHERE PATIENT_ID='" + patientId + "' AND VISIT_ID='" + visitId + "' AND ORDER_NO='" + orderNo + "' ");
            DataTable dt = dbMobile.GetData(strSql.ToString());
            int _rowCount = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                _rowCount = dt.Rows.Count;
            }
            return _rowCount > 0;
        }
    }
}
