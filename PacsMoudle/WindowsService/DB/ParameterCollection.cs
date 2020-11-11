using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;

namespace DB
{
    /// <summary>
    /// 参数集合
    /// </summary>
    public class ParameterCollection
    {
        /// <summary>
        /// Oracle参数集合
        /// </summary>
        private OracleParameterCollection collection;

        /// <summary>
        /// 参数:返回结果标识代码
        /// </summary>
        private const string PARAM_RESULT_CODE = "o_resultcode";

        /// <summary>
        /// 参数:返回异常信息
        /// </summary>
        private const string PARAM_RESULT_MSG = "o_errormsg";

        /// <summary>
        /// 初始化 System.Data.OracleClient.OracleParameterCollection 类的新实例。
        /// </summary>
        public ParameterCollection()
        {
            collection = new OracleParameterCollection();
        }

        /// <summary>
        /// 初始化 System.Data.OracleClient.OracleParameterCollection 类的新实例。
        /// </summary>
        /// <param name="values">要添加的 System.Data.OracleClient.OracleParameter 值。</param>
        public ParameterCollection(OracleParameter[] values)
        {
            collection = new OracleParameterCollection();
            collection.AddRange(values);
        }

        /// <summary>
        /// 获取或设置 System.Data.OracleClient.OracleParameterCollection 集合
        /// </summary>
        public OracleParameterCollection Collection
        {
            get
            {
                return collection;
            }
            set { collection = value; }
        }

        private OracleParameter[] parameters;
        /// <summary>
        /// 获取或设置 System.Data.OracleClient.OracleParameter[] 集合
        /// </summary>
        public OracleParameter[] Parameters
        {
            get
            {
                if (parameters == null || parameters.Length != this.collection.Count)
                {
                    OracleParameter[] pms = new OracleParameter[this.collection.Count];

                    int pmIndex = 0;
                    // 深拷贝
                    foreach (var p in this.collection)
                    {
                        pms[pmIndex] = (OracleParameter)((ICloneable)p).Clone();
                        pmIndex++;
                    }
                    return pms;
                }
                else
                    return parameters;
            }
            set { parameters = value; }
        }

        /// <summary>
        /// 获取或设置具有指定名称的 System.Data.OracleClient.OracleParameter 的值。
        /// </summary>
        /// <param name="parameterName"> 要检索的参数的名称。</param>
        /// <returns>具有指定名称的 System.Data.OracleClient.OracleParameter 的值。</returns>
        public string this[string parameterName]
        {
            get
            {
                if (collection.Contains(parameterName))
                    return collection[parameterName].Value == null ? string.Empty : collection[parameterName].Value.ToString();
                else
                    return string.Empty;
            }
            set
            {
                collection[parameterName].Value = value;
            }
        }

        /// <summary>
        /// 将 System.Data.OracleClient.OracleParameter 添加到 System.Data.OracleClient.OracleParameterCollection的末尾。
        /// </summary>
        /// <param name="values">要添加的 System.Data.OracleClient.OracleParameter 值。</param>
        public ParameterCollection Add(OracleParameter value)
        {
            collection.Add(value);
            return this;
        }

        /// <summary>
        /// 将 System.Data.OracleClient.OracleParameter 值的数组添加到 System.Data.OracleClient.OracleParameterCollection的末尾。
        /// </summary>
        /// <param name="values">要添加的 System.Data.OracleClient.OracleParameter 值。</param>
        public ParameterCollection AddRange(OracleParameter[] values)
        {
            collection.AddRange(values);
            return this;
        }

        /// <summary>
        /// 获取返回代码
        /// </summary>
        /// <returns></returns>
        public string GetResultCode()
        {
            return collection[PARAM_RESULT_CODE].Value.ToString();
        }

        ///// <summary>
        ///// 获取数据库操作是否成功的状态
        ///// </summary>
        //public bool IsSuccess
        //{
        //    get
        //    {
        //        return this.GetResultCode() == Result.RESULT_SUCCESS;
        //    }
        //}

        /// <summary>
        /// 获取返回信息
        /// </summary>
        /// <returns></returns>
        public string GetErrorMsg()
        {
            return collection[PARAM_RESULT_MSG].Value.ToString();
        }

        #region 输入参数

        /// <summary>
        /// 初始化使用参数名称、数据类型和值的OracleParameter类的新实例 [参数类型为输入参数]
        /// </summary>
        /// <param name="parName">参数名称</param>
        /// <param name="oracleType">参数类型</param>              
        /// <param name="value">参数值</param>
        /// <returns>返回一个OracleParameter实例</returns>
        public ParameterCollection CreateParameter(string parName, OracleType oracleType, object value)
        {
            OracleParameter par = new OracleParameter(parName, oracleType);
            par.Direction = ParameterDirection.Input;
            par.Value = value;
            this.Add(par);
            return this;
        }

        /// <summary>
        /// 初始化使用参数名称、数据类型、长度和值的OracleParameter类的新实例 [参数类型为输入参数]
        /// </summary>
        /// <param name="parName">参数名称</param>
        /// <param name="oracleType">参数类型</param>
        /// <param name="size">参数长度</param>        
        /// <param name="value">参数值</param>
        /// <returns>返回一个OracleParameter实例</returns>
        public ParameterCollection CreateParameter(string parName, OracleType oracleType, int size, object value)
        {
            OracleParameter par = new OracleParameter(parName, oracleType, size);           
            par.Direction = ParameterDirection.Input;
            par.Value = value;
            this.Add(par);
            return this;
        }
        #endregion 输入参数

        #region 输出参数

        /// <summary>
        /// 初始化使用参数名称和数据类型的OracleParameter类的新实例 [参数类型为输出参数]
        /// </summary>
        /// <param name="parName">参数名称</param>
        /// <param name="oracleType">参数类型</param>
        /// <param name="size">参数长度</param>                
        /// <returns>返回一个OracleParameter实例</returns>
        public  ParameterCollection CreateOutParameter(string parName, OracleType oracleType)
        {
            OracleParameter par = new OracleParameter(parName, oracleType);
            par.Direction = ParameterDirection.Output;
            this.Add(par);
            return this;
        }

        /// <summary>
        /// 初始化使用参数名称、数据类型和长度的OracleParameter类的新实例 [参数类型为输出参数]
        /// </summary>
        /// <param name="parName">参数名称</param>
        /// <param name="oracleType">参数类型</param>
        /// <param name="size">参数长度</param>                
        /// <returns>返回一个OracleParameter实例</returns>
        public ParameterCollection CreateOutParameter(string parName, OracleType oracleType, int size)
        {
            OracleParameter par = new OracleParameter(parName, oracleType, size);
            par.Direction = ParameterDirection.Output;
            this.Add(par);
            return this;
        }
        #endregion 输出参数

    }
}
