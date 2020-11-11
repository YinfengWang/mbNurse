using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class AppManagerDbI
    {
        #region 变量
        protected DbAccess _connection;
        #endregion

        public AppManagerDbI(DbAccess conn)
        {
            _connection = conn;
        }


        #region 应用程序列表
        public DataSet GetApplicationList()
        {
            string sql = "SELECT * FROM APP_DICT";
            
            return _connection.SelectData(sql, "APP_DICT");
        }
        
        
        public bool SaveApplicationList(ref DataSet dsApplication)
        {
            string sql = "SELECT * FROM APP_DICT";
            
            _connection.Update(ref dsApplication, "APP_DICT", sql);

            return true;
        }
        #endregion


        #region 应用程序
        /// <summary>
        /// 获取应用程序名称
        /// </summary>
        /// <returns></returns>
        public string GetAppName(string appCode)
        {
            string sql = "SELECT APP_NAME FROM APP_DICT WHERE APP_CODE = " + SQL.SqlConvert(appCode);
            if (_connection.SelectValue(sql) == true)
            {
                return _connection.GetResult(0);
            }
            else
            {
                return string.Empty;
            }
        }
        
        
        /// <summary>
        /// 获取应用程序的授权码
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public string GetAppAuthorizeCode(string appName)
        {
            string sql = "SELECT KEY FROM KEY_DICT WHERE PRODUCT_NAME = " + SQL.SqlConvert(appName);
            if (_connection.SelectValue(sql) == true)
            {
                return _connection.GetResult(0);
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
        

        #region 应用程序参数
        public string GetNewParamId()
        {
            string sql = "SELECT NVL(MAX(PARAMETER_ID), 0) + 1 FROM APP_CONFIG ";

            _connection.SelectValue(sql);
            return _connection.GetResult(0);
        }


        public DataSet GetApplicationParams(string appCode)
        {
             string sql = "SELECT * FROM APP_CONFIG "
                        + "WHERE APP_CODE IS NULL "
                        +       "OR APP_CODE = " + SqlManager.SqlConvert(appCode);
            
            return _connection.SelectData(sql, "APP_CONFIG");
        }


        public bool SaveApplicationParams(ref DataSet dsAppParams, string appCode)
        {
             string sql = "SELECT * FROM APP_CONFIG "
                        + "WHERE APP_CODE IS NULL "
                        +       "OR APP_CODE = " + SqlManager.SqlConvert(appCode);
            
            _connection.Update(ref dsAppParams);
            // _connection.Update(ref dsAppParams, "APP_CONFIG", sql);
            return true;
        }
        #endregion


        #region 应用程序模块
        public DataSet GetApplicationModules(string appCode)
        { 
            //string sql = "SELECT * FROM APP_VS_MODULE WHERE APP_CODE = " + SqlManager.SqlConvert(appCode);
            string sql = "SELECT * FROM MENU_MANAGER WHERE APP_CODE = " + SqlManager.SqlConvert(appCode);

            return _connection.SelectData(sql, "APP_VS_MODULE");            
        }

        public bool SaveApplicationModules(ref DataSet dsAppModules, string appCode)
        {
            //string sql = "SELECT * FROM APP_VS_MODULE WHERE APP_CODE = " + SqlManager.SqlConvert(appCode);        
            string sql = "SELECT * FROM MENU_MANAGER WHERE APP_CODE = " + SqlManager.SqlConvert(appCode);
            
            _connection.Update(ref dsAppModules, "APP_VS_MODULE", sql);

            return true;
        }


        public DataSet GetApplicationMenu(string appCode)
        { 
            string filter = "APP_VS_MODULE.APP_CODE = " + SQL.SqlConvert(appCode);
            StringBuilder sb = new StringBuilder(); 
            //sb.Append("SELECT NVL(APP_VS_MODULE.MODULE_CODE, APP_VS_MODULE.NODE_ID) NODEID, "    );
            //sb.Append(    "NVL(APP_VS_MODULE.PARENT_NODE_ID, '0') PARENTID, "                    );
            //sb.Append(    "APP_VS_MODULE.NODE_TITLE NODENAME, "                                  );
            //sb.Append(    "NVL(APP_VS_MODULE.MODULE_CODE, '0') MODULE_CODE, "                    );
            //sb.Append(    "(SELECT MODULE_DICT.DLL_NAME FROM MODULE_DICT "                       );
            //sb.Append(     "WHERE MODULE_DICT.FORM_GUID = APP_VS_MODULE.FORM_GUID) ADDDLL, "     );
            
            //sb.Append(    "(SELECT MODULE_DICT.FORM_TYPE FROM MODULE_DICT "                      );
            //sb.Append(     "WHERE MODULE_DICT.FORM_GUID = APP_VS_MODULE.FORM_GUID) ADDRESS,"     );
            
            //sb.Append(    "0 SERIAL_NO, "                                                        );
            //sb.Append(    "0 WIN_OPEN_MODE, "                                                    );
            
            //sb.Append(    "APP_VS_MODULE.ICON_FILE ICON, "                                       );
            //sb.Append(    "APP_VS_MODULE.BACKGROUND_RUN BACKGROUND "                             );
            //sb.Append("FROM APP_VS_MODULE "                                                      );
            //sb.Append("WHERE APP_VS_MODULE.APP_CODE = " + SqlManager.SqlConvert(appCode)         );
            
            //sb.Append("SELECT APP_VS_MODULE.NODE_ID NODEID, "                                    );
            //sb.Append(    "NVL(APP_VS_MODULE.PARENT_NODE_ID, '0') PARENTID, "                    );
            //sb.Append(    "APP_VS_MODULE.NODE_TITLE NODENAME, "                                  );
            //sb.Append(    "NVL(APP_VS_MODULE.MODULE_CODE, '0') MODULE_CODE, "                    );
            
            sb.Append("SELECT NVL(APP_VS_MODULE.MODULE_CODE, APP_VS_MODULE.NODE_ID) NODEID, "    );
            sb.Append(    "NVL(APP_VS_MODULE.PARENT_NODE_ID, '0') PARENTID, "                    );
            sb.Append(    "APP_VS_MODULE.NODE_TITLE NODENAME, "                                  );
            sb.Append(    "NVL(APP_VS_MODULE.MODULE_CODE, '0') MODULE_CODE, "                    );            
            sb.Append(    "(SELECT MODULE_DICT.DLL_NAME FROM MODULE_DICT "                       );
            sb.Append(     "WHERE MODULE_DICT.MODULE_CODE = APP_VS_MODULE.MODULE_CODE "          );
            sb.Append(           " AND " + filter + " AND ROWNUM = 1) ADDDLL, " );
            
            sb.Append(    "(SELECT MODULE_DICT.FORM_TYPE FROM MODULE_DICT "                      );
            sb.Append(     "WHERE MODULE_DICT.MODULE_CODE = APP_VS_MODULE.MODULE_CODE "          );
            sb.Append(           " AND " + filter + " AND ROWNUM = 1) ADDRESS," );
            
            sb.Append(    "(SELECT MODULE_DICT.PARAMETER FROM MODULE_DICT "                      );
            sb.Append(     "WHERE MODULE_DICT.MODULE_CODE = APP_VS_MODULE.MODULE_CODE"           );
            sb.Append(           " AND " + filter + " AND ROWNUM = 1) PARAMETER, " );
            
            sb.Append(    "APP_VS_MODULE.SERIAL_NO, "                                            );
            sb.Append(    "0 WIN_OPEN_MODE, "                                                    );
            
            sb.Append(    "APP_VS_MODULE.ICON_FILE ICON, "                                       );
            sb.Append(    "APP_VS_MODULE.BACKGROUND_RUN BACKGROUND "                             );
            sb.Append("FROM APP_VS_MODULE "                                                      );
            sb.Append("WHERE " + filter                                                          );
            
            string sql = sb.ToString();
            return _connection.SelectData(sql, "Menu");
        }
        #endregion


        #region  模块
        public DataSet GetModuleList()
        {
            string sql = "SELECT * FROM MODULE_DICT";
            
            return _connection.SelectData(sql, "MODULE_DICT");
        }


        public DataSet GetModulesInApp(string appCode)
        {
            StringBuilder sb = new StringBuilder(); 
            sb.Append("SELECT MODULE_DICT.* "                                                    );
            sb.Append("FROM MODULE_DICT, "                                                       );
            sb.Append(    "APP_VS_MODULE "                                                       );
            sb.Append("WHERE MODULE_DICT.FORM_GUID = APP_VS_MODULE.FORM_GUID "                   );
            sb.Append(    "AND APP_VS_MODULE.APP_CODE = " + SqlManager.SqlConvert(appCode)       );

            string sql = sb.ToString();
            
            return _connection.SelectData_NoKey(sql, "APP_MODULES");
        }


        public bool SaveModuleList(ref DataSet dsModule)
        {
            string sql = "SELECT * FROM MODULE_DICT";
            
            _connection.Update(ref dsModule, "MODULE_DICT", sql);

            return true;
        }
        #endregion


        #region 角色
        public DataSet GetRoleDict()
        { 
            string sql = "SELECT * FROM ROLE_DICT";

            return _connection.SelectData(sql, "ROLE_DICT");
        }


        public bool SaveRoleDict(ref DataSet dsRole)
        {
             string sql = "SELECT * FROM ROLE_DICT";
            
            _connection.Update(ref dsRole, "ROLE_DICT", sql);

            return true;
        }


        public DataSet GetRoleRights(string roleId)
        {
            string sql = "SELECT * FROM ROLE_RIGHTS WHERE ROLE_ID = " + SqlManager.SqlConvert(roleId);

            return _connection.SelectData(sql, "ROLE_RIGHTS");
        }


        public bool SaveRoleRights(ref DataSet dsRoleRights, string roleId)
        {
             string sql = "SELECT * FROM ROLE_RIGHTS WHERE ROLE_ID = " + SqlManager.SqlConvert(roleId);
            
            _connection.Update(ref dsRoleRights, "ROLE_RIGHTS", sql);

            return true;
        }


        public DataSet GetRoleApplications(string roleId)
        {
            StringBuilder sb = new StringBuilder(); 
            sb.Append("SELECT DISTINCT "                                                         );
            sb.Append(    "APP_VS_MODULE.APP_CODE "                                              );
            sb.Append("FROM ROLE_RIGHTS, "                                                       );
            sb.Append(    "APP_VS_MODULE "                                                       );
            sb.Append("WHERE ROLE_RIGHTS.ROLE_ID = " + SqlManager.SqlConvert(roleId)             );
            sb.Append(    "AND ROLE_RIGHTS.MODULE_CODE = APP_VS_MODULE.MODULE_CODE "             );

            string sql = sb.ToString();

            return _connection.SelectData(sql, "ROLE_APPS");
        }
        #endregion
    }
}
