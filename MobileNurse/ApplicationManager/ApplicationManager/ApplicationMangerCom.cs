using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CommonEntity;

namespace HISPlus
{
    public class ApplicationMangerCom
    {
        // 局部变量
        private AppManagerDbI appManagerDbI = null;


        public ApplicationMangerCom()
        {
            appManagerDbI = new AppManagerDbI(GVars.OracleAccess);
        }


        #region 应用程序列表
        public DataSet GetApplicationList()
        {
            return appManagerDbI.GetApplicationList();
        }


        public bool SaveApplicationList(ref DataSet dsApplication)
        {
            return appManagerDbI.SaveApplicationList(ref dsApplication);
        }
        #endregion


        #region 应用程序参数
        public string GetNewParamId()
        {
            return appManagerDbI.GetNewParamId();
        }


        public DataSet GetApplicationParams(string appCode)
        {
            return appManagerDbI.GetApplicationParams(appCode);
        }


        public bool SaveApplicationParams(ref DataSet dsAppParams, string appCode)
        {
            return appManagerDbI.SaveApplicationParams(ref dsAppParams, appCode);
        }
        #endregion


        #region 模块列表
        public DataSet GetModuleList()
        {
            return appManagerDbI.GetModuleList();
        }


        public bool SaveModuleList(ref DataSet dsModule)
        {
            return appManagerDbI.SaveModuleList(ref dsModule);
        }
        #endregion


        #region 应用程序模块
        public DataSet GetApplicationModules(string appCode)
        {
            return appManagerDbI.GetApplicationModules(appCode);
        }

        /// <summary>
        /// 根据应用程序编码查询菜单列表
        /// </summary>
        /// <param name="appCode"></param>
        /// <returns></returns>
        public IList<MenuManager> GetMenus(string appCode)
        {
            return EntityOper.GetInstance().FindByProperty<MenuManager>("AppCode", appCode);
        }

        public bool SaveApplicationModules(ref DataSet dsAppModules, string appCode)
        {
            return appManagerDbI.SaveApplicationModules(ref dsAppModules, appCode);
        }


        public DataSet GetModulesInApp(string appCode)
        {
            return appManagerDbI.GetModulesInApp(appCode);
        }


        public DataSet GetApplicationMenu(string appCode)
        {
            return appManagerDbI.GetApplicationMenu(appCode);
        }
        #endregion


        #region 角色
        public DataSet GetRoleDict()
        {
            return appManagerDbI.GetRoleDict();
        }


        public bool SaveRoleDict(ref DataSet dsRole)
        {
            return appManagerDbI.SaveRoleDict(ref dsRole);
        }


        public DataSet GetRoleRights(string roleId)
        {
            return appManagerDbI.GetRoleRights(roleId);
        }


        public bool SaveRoleRights(ref DataSet dsRoleRights, string roleId)
        {
            return appManagerDbI.SaveRoleRights(ref dsRoleRights, roleId);
        }


        public DataSet GetRoleApplications(string roleId)
        {
            return appManagerDbI.GetRoleApplications(roleId);
        }
        #endregion
    }
}
