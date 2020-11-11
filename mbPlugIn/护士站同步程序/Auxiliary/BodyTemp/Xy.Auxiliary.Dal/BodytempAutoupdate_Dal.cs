// ***********************************************************************
// Assembly         : Xy.Auxiliary.Dal
// Author           : LPD
// Created          : 01-14-2016
//
// Last Modified By : LPD
// Last Modified On : 01-14-2016
// ***********************************************************************
// <copyright file="BodytempAutoupdate_Dal.cs" company="心医国际(西安)">
//     Copyright (c) 心医国际(西安). All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xy.Auxiliary.PubCls;
using System.Data;

/// <summary>
/// The Dal namespace.
/// </summary>
namespace Xy.Auxiliary.Dal
{
    /// <summary>
    /// Class BodytempAutoupdate_Dal.
    /// </summary>
    public class BodytempAutoupdate_Dal
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
        /// 验证上传的文件是否已经存在数据库  true:存在  false:不存在
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool fileIsExit(string fileName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM BODYTEMP_AUTOUPDATE");
            strSql.Append(" WHERE FILENAME='" + fileName + "'");
            DataTable dt = dbMobile.GetData(strSql.ToString());
            int _rowCount = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                _rowCount = Convert.ToInt32(dt.Rows[0][0]);
            }
            return _rowCount > 0;
        }

        /// <summary>
        /// 获取当前文件的版本号
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public int GetNowVersion(string fileName)
        {
            int _version =0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT VERSION FROM BODYTEMP_AUTOUPDATE");
            strSql.Append(" WHERE FILENAME='" + fileName + "'");
            DataTable dt = dbMobile.GetData(strSql.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                _version = Convert.ToInt32(dt.Rows[0][0]);
            }
            return _version;
        }

        /// <summary>
        /// 获取BODYTEMP_AUTOUPDATE表数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetBodytempAutoupdate()
        {
            string strSql = "SELECT * FROM MOBILE.BODYTEMP_AUTOUPDATE ";
            return dbMobile.GetData(strSql);
        }
    }
}
