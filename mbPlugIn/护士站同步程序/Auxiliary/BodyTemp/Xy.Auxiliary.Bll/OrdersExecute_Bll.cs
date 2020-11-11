// ***********************************************************************
// Assembly         : Xy.Auxiliary.Bll
// Author           : LPD
// Created          : 12-14-2015
//
// Last Modified By : LPD
// Last Modified On : 12-21-2015
// ***********************************************************************
// <copyright file="OrdersExecute_Bll.cs" company="心医国际(西安)">
//     Copyright (c) 心医国际(西安). All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xy.Auxiliary.Dal;

/// <summary>
/// The Bll namespace.
/// </summary>
namespace Xy.Auxiliary.Bll
{

    /// <summary>
    /// Class OrdersExecute_Bll.
    /// </summary>
    public class OrdersExecute_Bll
    {
        /// <summary>
        /// The order executable dal
        /// </summary>
        OrdersExecute_Dal orderExeDal = new Dal.OrdersExecute_Dal();

        /// <summary>
        /// 判断医嘱是否已经拆分过【Mobile】
        /// </summary>
        /// <param name="patientId">The patient unique identifier.</param>
        /// <param name="visitId">The visit unique identifier.</param>
        /// <param name="orderNo">The order no.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool OrdersExecuteExists(string patientId, decimal visitId, decimal orderNo)
        {
            return orderExeDal.OrdersExecuteExists(patientId, visitId, orderNo);
        }
    }
}
