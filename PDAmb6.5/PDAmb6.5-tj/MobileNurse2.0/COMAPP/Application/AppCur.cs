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
        
        protected bool      _wlan       = false;                        // �������������Ƿ����

        public AppCur()
		{
        }


        #region ����
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


        #region �ӿ�
        /// <summary>
        /// ���ر��������ļ�����
        /// </summary>
        /// <returns></returns>
        public bool LoadLocalSetting()
        { 
            // ��ʼ��
            _serverIp = string.Empty;
            _deptCode = string.Empty;

            // ��ȡֵ
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
            string configFile = Path.Combine(appPath, "Config.xml");
            
            DataSet dsConfig = new DataSet("Config");
            
            if (System.IO.File.Exists(configFile) == false)
            {
                // ������
                DataTable dt = new DataTable("Config");
                DataColumn dc = new DataColumn("SERVER_IP", System.Type.GetType("System.String"));
                dt.Columns.Add(dc);
                
                dc = new DataColumn("WARD_CODE", System.Type.GetType("System.String"));
                dt.Columns.Add(dc);
                
                dc = new DataColumn("VERSION", System.Type.GetType("System.String"));
                dt.Columns.Add(dc);
                
                dsConfig.Tables.Add(dt);
                
                // �������������
                DataRow dr = dt.NewRow();
                dr["SERVER_IP"] = string.Empty;
                dr["WARD_CODE"] = string.Empty;
                
                dt.Rows.Add(dr);
                
                // ���浽����
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
