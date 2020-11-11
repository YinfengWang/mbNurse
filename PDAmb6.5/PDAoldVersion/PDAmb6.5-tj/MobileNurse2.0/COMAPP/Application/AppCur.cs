using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace HISPlus
{
    public class AppCur : App
    {
        protected string    _serverIp   = string.Empty;
        protected string    _deptCode   = string.Empty;
        
        protected bool      _wlan       = false;                        // 无线网络连接是否可用

        public AppCur()
		{
        }


        #region 属性
        public string ServerIp
        {
            get { return _serverIp;}
            set { _serverIp = value;}
        }


        public string DeptCode
        {
            get { return _deptCode;}
            set { _deptCode = value;}
        }

        
        public bool WLanReachable
        {
            get 
            {
                return true;
                return _wlan;
            }
            set 
            {
                _wlan = value;
            }
        }
        #endregion


        #region 接口
        /// <summary>
        /// 加载本地配置文件设置
        /// </summary>
        /// <returns></returns>
        public bool LoadLocalSetting()
        { 
            // 初始化
            _serverIp = string.Empty;
            _deptCode = string.Empty;

            // 获取值
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
            string configFile = Path.Combine(appPath, "Config.xml");
            
            DataSet dsConfig = new DataSet("Config");
            
            if (System.IO.File.Exists(configFile) == false)
            {
                // 创建表
                DataTable dt = new DataTable("Config");
                DataColumn dc = new DataColumn("SERVER_IP", System.Type.GetType("System.String"));
                dt.Columns.Add(dc);
                
                dc = new DataColumn("WARD_CODE", System.Type.GetType("System.String"));
                dt.Columns.Add(dc);
                
                dc = new DataColumn("VERSION", System.Type.GetType("System.String"));
                dt.Columns.Add(dc);
                
                dsConfig.Tables.Add(dt);
                
                // 向表中增加数据
                DataRow dr = dt.NewRow();
                dr["SERVER_IP"] = string.Empty;
                dr["WARD_CODE"] = string.Empty;
                
                dt.Rows.Add(dr);
                
                // 保存到本地
                dsConfig.WriteXml(configFile, XmlWriteMode.WriteSchema);
            }
            else
            {
                dsConfig.ReadXml(configFile, XmlReadMode.ReadSchema);
            }

            if (dsConfig == null || dsConfig.Tables.Count == 0 || dsConfig.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                DataRow dr = dsConfig.Tables[0].Rows[0];

                _serverIp = dr["SERVER_IP"].ToString();
                _deptCode = dr["WARD_CODE"].ToString();
                _version  = dr["VERSION"].ToString();
            }
            
            return true;
        }
        #endregion
    }
}
