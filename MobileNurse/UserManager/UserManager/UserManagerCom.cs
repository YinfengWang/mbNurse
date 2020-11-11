using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HISPlus
{
    class UserManagerCom
    {
        // 局部变量
        private  HospitalDbI    hospitalDbI     = null;
        private  UserDbI        userDbI         = null;
        private  AppManagerDbI  appManagerDbI   = null;

        public UserManagerCom()
        {
            hospitalDbI     = new HospitalDbI(GVars.OracleAccess);
            userDbI         = new UserDbI(GVars.OracleAccess);
            appManagerDbI   = new AppManagerDbI(GVars.OracleAccess);
        }


        public DataSet GetNursingUnits()
        {
            return hospitalDbI.Get_WardList_Nurse();
        }


        public DataSet GetNursesInDept(string deptCode)
        {
            return hospitalDbI.GetNursesInDept(deptCode);
        }


        public DataSet GetRoleDict()
        {
            return appManagerDbI.GetRoleDict();
        }


        public DataSet GetUserRoles(string userId)
        {
            return userDbI.GetUserRoles(userId);
        }


        public bool SaveUserRoles(ref DataSet dsUserRoles, string userId)
        {
            return userDbI.SaveUserRoles(ref dsUserRoles, userId);
        }


        public bool SaveUserInfo(string userId, string workNo, string userName, string pwd, string inputNo)
        {
            return userDbI.SaveUserInfo(userId, workNo, userName, pwd, inputNo);
        }
        
        
        public bool ResetPwd(string userId)
        {
            return userDbI.ChangePwd_Table(userId, string.Empty);
        }
    }
}
