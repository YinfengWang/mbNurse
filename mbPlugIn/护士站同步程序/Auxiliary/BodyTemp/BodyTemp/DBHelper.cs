// ***********************************************************************
// Assembly         : BodyTemp
// Author           : LPD
// Created          : 12-04-2015
//
// Last Modified By : LPD
// Last Modified On : 12-04-2015
// ***********************************************************************
// <copyright file="DBHelper.cs" company="心医国际">
//     Copyright (c) 心医国际. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Collections;
using System.IO;

/// <summary>
/// The BodyTemp namespace.
/// </summary>
namespace BodyTemp
{
    /// <summary>
    /// Class DBHelper.
    /// </summary>
    public class DBHelper
    {
        //数据库地址
        /// <summary>
        /// Mobile连接字符串
        /// </summary>
        protected string conStrMobile = new HISPlus.EnDecrypt().Decrypt(System.Configuration.ConfigurationManager.AppSettings["StrConnMobile"].ToString());

        /// <summary>
        /// His连接字符串
        /// </summary>
        protected string connStrHis = new HISPlus.EnDecrypt().Decrypt(System.Configuration.ConfigurationManager.AppSettings["StrConnHis"].ToString());

        /// <summary>
        /// The mysql configuration
        /// </summary>
        private OracleConnection mysqlCon;

        public DBHelper(string strFlag)
        {
            if (strFlag == ConnFlag.mobile.ToString())
            {
                //连接Mobile库
                mysqlCon = new OracleConnection(conStrMobile);
            }
            else if (strFlag == ConnFlag.his.ToString())
            {
                //连接Hid库
                mysqlCon = new OracleConnection(connStrHis);
            }
            else
            {
                //默认连接Mobile库
                mysqlCon = new OracleConnection(conStrMobile);
            }
        }

        /// <summary>
        /// OracleCommand
        /// </summary>
        private OracleCommand mysqlcmd;

        /// <summary>
        /// OracleTransaction
        /// </summary>
        private OracleTransaction mysqltran;

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="mysql">The mysql.</param>
        /// <returns>DataTable.</returns>
        public DataTable GetData(string mysql)
        {
            OracleDataAdapter msda = new OracleDataAdapter(mysql, mysqlCon);
            DataTable dt = new DataTable();
            if (dt != null)
            {
                msda.Fill(dt); return dt;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据mysql语句，查询数据库，返回一个整型的值
        /// </summary>
        /// <param name="mysql">访问数据库的语句</param>
        /// <returns>整型的值</returns>
        public int ExecuteNonQuery(string mysql)
        {
            //try catach finally检测数据库的连接是否打开，如果状态是关闭，则需要打开，
            //在跟数据库交互完之后需要及时关闭
            try
            {
                if (mysqlCon.State == ConnectionState.Closed)
                {
                    mysqlCon.Open();
                }
                mysqlcmd = new OracleCommand(mysql, mysqlCon);
                int res = mysqlcmd.ExecuteNonQuery();
                mysqlCon.Close();
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                mysqlCon.Close();
            }
        }

        /// <summary>
        /// 根据mysql，查询数据库，返回object的一行一列的值
        /// </summary>
        /// <param name="mysql">连接数据库的语句</param>
        /// <returns>object类型的值</returns>
        public object ExecuteScalar(string mysql)
        {
            //try catach finally检测数据库的连接是否打开，如果状态是关闭，则需要打开，
            //在跟数据库交互完之后需要及时关闭
            try
            {
                if (mysqlCon.State == ConnectionState.Closed)
                {
                    mysqlCon.Open();
                }
                mysqlcmd = new OracleCommand(mysql, mysqlCon);
                object o = null;
                if (mysqlcmd.ExecuteScalar() != System.DBNull.Value)
                {
                    o = mysqlcmd.ExecuteScalar();
                }
                mysqlCon.Close();
                return o;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                mysqlCon.Close();
            }
        }

        /// <summary>
        /// Transactions the specified array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Transaction(ArrayList array)
        {
            if (mysqlCon.State == ConnectionState.Closed)
            {
                mysqlCon.Open();
            }
            //开始事务处理。
            mysqltran = mysqlCon.BeginTransaction();
            mysqlcmd = new OracleCommand();
            mysqlcmd.Connection = mysqlCon;
            mysqlcmd.Transaction = mysqltran;
            try
            {
                foreach (string arr in array)
                {
                    mysqlcmd.CommandText = arr.ToString();
                    mysqlcmd.ExecuteNonQuery();
                }
                mysqltran.Commit();
                return true;
            }
            catch
            {
                //如果不成功，则回滚，即之前的操作均无效。
                mysqltran.Rollback();
                return false;
            }
            finally
            {
                mysqlCon.Close();
            }
        }


    }

    /// <summary>
    /// 连接数据库选项
    /// </summary>
    public enum ConnFlag
    {
        mobile,//mobile库
        his    //his库
    }
}
