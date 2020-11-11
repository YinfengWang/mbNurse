using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Configuration;
using System.Data;
using System.Web.UI;
using SQL = HISPlus.SqlManager;
using System.Diagnostics;


/// <summary>
///UserWebSrv 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class UserWebSrv : System.Web.Services.WebService
{
 
    private HISPlus.OracleAccess oraAccess = new HISPlus.OracleAccess();

    public UserWebSrv()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
        oraAccess.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString;
    }


    /// <summary>
    /// 获取系统日期
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public DateTime GetSysDate()
    {
        //DataSet ds = new DataSet();
        //ds.Tables.Add("dfdf");
        //ds.Tables[0].Columns.Add("test", typeof(string));

        //Exception ex = new Exception("测试");

        //outputError(ex, ds, "D_");

        return oraAccess.GetSysDate();
    }


    /// <summary>
    /// 检查用户名/密码
    /// </summary>
    /// <returns>TRUE: 成功; FALSE: 失败</returns>
    [WebMethod]
    public bool ChkOracleUserPwd_Db(string userId, string pwd)
    {
        HISPlus.OracleAccess ora = new HISPlus.OracleAccess();
        bool _rflag;
        try
        {
            userId = userId.ToUpper().Trim();
            pwd = pwd.ToUpper().Trim();

            // 组织连接字符串
            // user id=system;password=manager            
            string connStr = oraAccess.ConnectionString.ToUpper();
            string[] arrParts = connStr.Split(";".ToCharArray());

            for (int i = 0; i < arrParts.Length; i++)
            {
                string[] arrSubParts = arrParts[i].Split("=".ToCharArray());

                arrSubParts[0] = arrSubParts[0].Trim();
                if (arrSubParts[0].ToUpper().Trim().IndexOf("USER") == 0)
                {
                    arrSubParts[0] = "USER ID";
                    if (arrSubParts.Length > 1) { arrSubParts[1] = userId; }
                }

                if (arrSubParts[0].ToUpper().Trim().IndexOf("PASSWORD") == 0)
                {
                    if (arrSubParts.Length > 1) { arrSubParts[1] = pwd; }
                }

                arrParts[i] = arrSubParts[0];
                for (int j = 1; j < arrSubParts.Length; j++)
                {
                    arrParts[i] += "=" + arrSubParts[j];
                }
            }

            connStr = arrParts[0];
            for (int i = 1; i < arrParts.Length; i++)
            {
                connStr += ";" + arrParts[i];
            }

            // 连接测试
            ora.Connect(connStr);
            _rflag = true;
        }
        catch (Exception ex)
        {
            _rflag = false;
        }
        finally
        {
            ora.DisConnect();
        }

        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(), "userId:" + userId + " " + "pwd:" + pwd, _rflag.ToString());
        return _rflag;
    }


    /// <summary>
    /// 检查用户名/密码
    /// </summary>
    /// <returns>TRUE: 成功; FALSE: 失败</returns>
    [WebMethod]
    public bool ChkOracleUserPwd(string userId, string pwd)
    {
        bool _rflag;
        try
        {
            userId = userId.ToUpper().Trim();
            pwd = pwd.ToUpper().Trim();

            //string strSel = "SELECT PASSWORD FROM STAFF_DICT ";
            //strSel += "WHERE UPPER(USER_NAME) = " + SQL.SqlConvert(userId.ToUpper());

            //if (oraAccess.SelectValue(strSel) == true)
            //{0
            //    pwd = HISPlus.EnDecrypt.Encrypt_JW(pwd);

            //    return pwd.Equals(oraAccess.GetResult(0).ToString());
            //}

            //return false;
            string sql = "SELECT ID FROM STAFF_DICT ";
            sql += "WHERE UPPER(USER_NAME) = " + SQL.SqlConvert(userId);
            sql += "AND PASSWORD = " + SQL.SqlConvert(HISPlus.EnDecrypt.Encrypt_JW(pwd));

            //if (oraAccess.Connection.State!= ConnectionState.Open)
            //{
            //    oraAccess.Connection.Open();
            //}

            if (oraAccess.SelectValue(sql) == false)
            {
                _rflag = false;
            }
            else
            {
                _rflag = true;
            }
        }
        catch (Exception ex)
        {
            _rflag = false;
        }

        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(), "userId:" + userId + " " + "pwd:" + pwd, _rflag.ToString());
        return _rflag;
    }


    /// <summary>
    /// 修改用户密码
    /// </summary>
    /// <param name="userId">用户名</param>
    /// <param name="newPwd">新密码</param>
    /// <returns></returns>
    [WebMethod]
    public bool ChangePwd_Db(string userId, string newPwd)
    {
        bool _rflag;
        try
        {

            userId = userId.ToUpper().Trim();
            newPwd = newPwd.ToUpper().Trim();

            string strCmd = "ALTER USER " + userId + " IDENTIFIED BY " + newPwd;
            oraAccess.ExecuteNoQuery(strCmd);

            _rflag = true;
        }
        catch (Exception ex)
        {
            _rflag = false;
        }

        //日志
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(), "userId:" + userId + " " + "newPwd:" + newPwd, _rflag.ToString());
        return _rflag;
    }


    /// <summary>
    /// 修改用户密码
    /// </summary>
    /// <param name="userId">用户名</param>
    /// <param name="newPwd">新密码</param>
    /// <returns></returns>
    [WebMethod]
    public bool ChangePwd(string userId, string newPwd)
    {
        bool _rflag;
        try
        {
            userId = userId.ToUpper().Trim();
            newPwd = HISPlus.EnDecrypt.Encrypt_JW(newPwd.ToUpper().Trim());

            string sql = "UPDATE STAFF_DICT SET PASSWORD = " + SQL.SqlConvert(newPwd);
            sql += " WHERE USER_NAME = " + SQL.SqlConvert(userId.ToUpper());

            oraAccess.ExecuteNoQuery(sql);

            _rflag = true;
        }
        catch (Exception ex)
        {
            _rflag = false;
        }
        //日志
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(), "userId:" + userId + " " + "newPwd:" + newPwd, _rflag.ToString());
        return _rflag;
    }


    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="pwd"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetUserInfo(string userName, string pwd)
    {
        userName = userName.ToUpper().Trim();
        pwd = pwd.ToUpper().Trim();

        string strSel = "SELECT DB_USER, USER_ID, USER_NAME, USER_DEPT FROM USERS WHERE UPPER(DB_USER) = " + SQL.SqlConvert(userName.ToUpper());
        DataSet dsUserInfo = oraAccess.SelectData(strSel, "USER_INFO");

        //日志
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(), "userName:" + userName + " " + "pwd:" + pwd, dsUserInfo.Tables[0].Rows.Count.ToString());

        return dsUserInfo;

    }
}

