// ***********************************************************************
// Assembly         : Xy.Auxiliary.Bll
// Author           : LPD
// Created          : 12-07-2015
//
// Last Modified By : LPD
// Last Modified On : 01-16-2016
// ***********************************************************************
// <copyright file="OrdersM_Bll.cs" company="心医国际(西安)">
//     Copyright (c) 心医国际(西安). All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xy.Auxiliary.Dal;
using Xy.Auxiliary.Entity;
using System.Data;

/// <summary>
/// The Bll namespace.
/// </summary>
namespace Xy.Auxiliary.Bll
{
    /// <summary>
    /// Class OrdersM_Bll.
    /// </summary>
    public class OrdersM_Bll
    {
        /// <summary>
        /// The orderm dal
        /// </summary>
        OrdersM_Dal ordermDal = new Dal.OrdersM_Dal();


        /// <summary>
        /// 判断是否医嘱是否已经同步过
        /// </summary>
        /// <param name="patientId">The patient unique identifier.</param>
        /// <param name="visitId">The visit unique identifier.</param>
        /// <param name="orderNo">The order no.</param>
        /// <param name="orderSubNo">The order sub no.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Exists(string patientId, decimal visitId, decimal orderNo, decimal orderSubNo)
        {
            return ordermDal.Exists(patientId, visitId, orderNo, orderSubNo);
        }

        /// <summary>
        ///  获取HIS医嘱信息【HIS】
        /// </summary>
        /// <param name="patientId">病人ID</param>
        /// <param name="visitId">住院次数</param>
        /// <param name="orderNo">医嘱号</param>
        /// <returns>DataTable.</returns>
        public DataTable GetHisOrders(string patientId, decimal visitId, decimal orderNo)
        {
            return ordermDal.GetHisOrders(patientId, visitId, orderNo);
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
            return ordermDal.GetMobileOrders_M(patientId, visitId, orderNo);
        }

        /// <summary>
        /// 将HIS的医嘱同步至MOBILE库
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool SyncHisOrdersToMobile(DataRow dr)
        {
            return ordermDal.SyncHisOrdersToMobile(dr);
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
            return ordermDal.GetOrdersModel(patientId, visitId, orderNo, orderSubNo);
        }

        /// <summary>
        /// 发现HIS中医嘱状态和停止执行时间与MOBILE不一致，则更新MOBILE的医嘱与HIS一致。
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool UpdMobileOrdersFromHis(DataRow dr)
        {
            return ordermDal.UpdMobileOrdersFromHis(dr);
        }

        /// <summary>
        /// 根据ini文件中的医嘱在orders_m表找到医嘱内容
        /// </summary>
        /// <param name="newOrders">The new orders.</param>
        /// <returns>DataTable.</returns>
        public DataTable GetIniFileOrdersDt(string newOrders)
        {
            return ordermDal.GetIniFileOrdersDt(newOrders);
        }
    }
}
