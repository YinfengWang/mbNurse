using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Configuration;
using System.Data;
using System.Text;

using SQL = HISPlus.SqlManager;
using ComConst = HISPlus.ComConst;
using System.IO;

/// <summary>
///PatientWebSrv 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class PatientWebSrv : System.Web.Services.WebService
{

    private HISPlus.OracleAccess oraAccess = new HISPlus.OracleAccess();

    public ArrayList arraylist;


    public PatientWebSrv()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
        oraAccess.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString;
    }


    /// <summary>
    /// 获取过滤病人列表
    /// </summary>
    /// <param name="deptCode"></param>
    /// <param name="filterName"></param>
    /// <param name="certificate"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetPatientInfo_Filter(string deptCode, string filterName)
    {
        DataSet DsPatientInfo_Filter = new DataSet();
        // 预处理
        string[] parts = filterName.Split(":".ToCharArray());
        if (parts.Length == 0 || parts[0].Trim().Length == 0)
        {
            return null;
        }

        string typeName = parts[0].Trim();
        string typeVal = string.Empty;

        if (parts.Length > 1) { typeVal = parts[1].Trim(); }

        // 处理
        switch (typeName)
        {
            case "全部":
                DsPatientInfo_Filter = getPatientList(deptCode, string.Empty);
                break;
            case "病情":
                DsPatientInfo_Filter = getPatientFiltered_Status(deptCode, typeVal);
                break;
            case "护理等级":
                DsPatientInfo_Filter = getPatientFiltered_NursingClass(deptCode, typeVal);
                break;
            case "体温大于":
                DsPatientInfo_Filter = getPatientFitlered_Temperature(deptCode, typeVal);
                break;
            case "新入院":
                string filter = " TO_DATE(PATIENT_INFO.ADMISSION_DATE_TIME) > TO_DATE(SYSDATE) - " + typeVal;
                DsPatientInfo_Filter = getPatientList(deptCode, filter);
                break;
            case "医嘱":
                DsPatientInfo_Filter = getPatientFiltered_Orders(deptCode, typeVal);
                break;
            case "饮食":
                DsPatientInfo_Filter = getPatientFiltered_Orders(deptCode, typeVal);
                break;
            case "执行单":
                DsPatientInfo_Filter = getPatientFiltered_OrdersExecute(deptCode, typeVal);
                break;
            case "手术":
                DsPatientInfo_Filter = getPatientFiltered_Surgery(deptCode, typeVal);
                break;
            case "转入":
                DsPatientInfo_Filter = getPatientFiltered_Into(deptCode, typeVal);
                break;
            case "测血压":
                DsPatientInfo_Filter = getPatientFiltered_Orders(deptCode, typeVal);
                break;
            case "测血糖4":
                DsPatientInfo_Filter = getPatientFiltered_Orders(deptCode, typeVal);
                break;
            case "测血糖6":
                DsPatientInfo_Filter = getPatientFiltered_Orders(deptCode, typeVal);
                break;
            default:
                DsPatientInfo_Filter = null;
                break;
        }


        //日志
        //string logJsonValue = string.Empty;
        //if (DsPatientInfo_Filter != null && DsPatientInfo_Filter.Tables[0].Rows.Count > 0)
        //    logJsonValue = OperationLog.DataTableConvertJson.Dataset2Json(DsPatientInfo_Filter);
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
        //    "deptCode:" + deptCode + "  " + "filterName:" + filterName, logJsonValue);

        return DsPatientInfo_Filter;
    }

    /// <summary>                                         
    /// 手术过滤
    /// </summary>
    /// <param name="deptCode"></param>
    /// <param name="surgery"></param>
    /// <returns></returns>
    private DataSet getPatientFiltered_Surgery(string deptCode, string surgery)
    {
        StringBuilder sql = new StringBuilder();
        sql.Append("SELECT DISTINCT PATIENT_INFO.PATIENT_ID, ");
        sql.Append("PATIENT_INFO.VISIT_ID ");
        sql.Append("FROM VITAL_SIGNS_REC, ");
        sql.Append("PATIENT_INFO ");
        sql.Append("WHERE VITAL_SIGNS_REC.PATIENT_ID = PATIENT_INFO.PATIENT_ID ");
        sql.Append("AND VITAL_SIGNS_REC.VISIT_ID = PATIENT_INFO.VISIT_ID ");
        sql.Append("AND PATIENT_INFO.WARD_CODE=" + SQL.SqlConvert(deptCode));
        sql.Append("AND VITAL_SIGNS_REC.VITAL_SIGNS LIKE '%手术' ");
        sql.Append("AND TO_DATE(VITAL_SIGNS_REC.TIME_POINT)>TO_DATE(SYSDATE)-" + surgery);

        DataSet dsPatientId = oraAccess.SelectData(sql.ToString());

        string filter = string.Empty;
        foreach (DataRow dr in dsPatientId.Tables[0].Rows)
        {
            if (filter.Length > 0)
            {
                filter += "','";
            }

            filter += dr["PATIENT_ID"].ToString();
        }

        if (filter.Length > 0)
        {
            filter = " PATIENT_INFO.PATIENT_ID IN ('" + filter + "') ";
        }
        else
        {
            filter = " (1 = 2) ";
        }

        // 获取病区病人信息
        return getPatientList(deptCode, filter);
    }

    private DataSet getPatientFiltered_Into(string deptCode, string into)
    {
        StringBuilder sql = new StringBuilder();
        sql.Append("SELECT DISTINCT PATIENT_INFO.PATIENT_ID, ");
        sql.Append("PATIENT_INFO.VISIT_ID ");
        sql.Append("FROM VITAL_SIGNS_REC, ");
        sql.Append("PATIENT_INFO ");
        sql.Append("WHERE VITAL_SIGNS_REC.PATIENT_ID = PATIENT_INFO.PATIENT_ID ");
        sql.Append("AND VITAL_SIGNS_REC.VISIT_ID = PATIENT_INFO.VISIT_ID ");
        sql.Append("AND PATIENT_INFO.WARD_CODE=" + SQL.SqlConvert(deptCode));
        sql.Append("AND VITAL_SIGNS_REC.VITAL_SIGNS LIKE '%转入' ");
        sql.Append("AND TO_DATE(VITAL_SIGNS_REC.TIME_POINT)>TO_DATE(SYSDATE)-" + into);

        DataSet dsPatientId = oraAccess.SelectData(sql.ToString());

        string filter = string.Empty;
        foreach (DataRow dr in dsPatientId.Tables[0].Rows)
        {
            if (filter.Length > 0)
            {
                filter += "','";
            }

            filter += dr["PATIENT_ID"].ToString();
        }

        if (filter.Length > 0)
        {
            filter = " PATIENT_INFO.PATIENT_ID IN ('" + filter + "') ";
        }
        else
        {
            filter = " (1 = 2) ";
        }
        // 获取病区病人信息
        return getPatientList(deptCode, filter);
    }

    /// <summary>
    /// 获取某一病区的病人
    /// </summary>
    /// <param name="deptCode"></param>
    /// <returns></returns>
    private DataSet getPatientList(string deptCode, string filter)
    {
        // 获取数据
        string sql = string.Empty;

        sql = "SELECT * FROM PATIENT_INFO ";
//        sql = @"WARD_CODE, PATIENT_ID, VISIT_ID, INP_NO, ADMISSION_DATE_TIME, NAME, SEX, DATE_OF_BIRTH, 
//                    DIAGNOSIS, ALERGY_DRUGS, DOCTOR_IN_CHARGE, BED_NO, BED_LABEL, PATIENT_STATUS_NAME, 
//                    NURSING_CLASS, NURSING_CLASS_NAME, NURSING_CLASS_COLOR, DEPT_NAME, DEPT_CODE, WARD_NAME, ROOM_NO, ADM_WARD_DATE_TIME ";
        sql += "WHERE ";
        sql += "WARD_CODE = " + SQL.SqlConvert(deptCode);

        if (filter.Length > 0)
        {
            sql += " AND " + filter;
        }

        sql += " ORDER BY ";
        sql += "BED_NO ";

        return oraAccess.SelectData(sql, "PATIENT_INF");
    }


    /// <summary>
    /// 病情过滤
    /// </summary>
    /// <returns></returns>
    public DataSet getPatientFiltered_Status(string deptCode, string status)
    {
        // 获取过滤条件
        string filter = string.Empty;

        if (status.Length > 0)
        {
            filter += "PATIENT_INFO.PATIENT_STATUS_NAME = " + SQL.SqlConvert(status);
        }
        else
        {
            filter += "(1 = 2) ";
        }

        // 查询   
        return getPatientList(deptCode, filter);
    }


    /// <summary>
    /// 护理等级过滤
    /// </summary>
    /// <returns></returns>
    public DataSet getPatientFiltered_NursingClass(string deptCode, string nursingClassName)
    {
        // 获取过滤条件
        string filter = string.Empty;

        if (nursingClassName.Length > 0)
        {
            filter += "PATIENT_INFO.NURSING_CLASS_NAME = " + SQL.SqlConvert(nursingClassName);
        }
        else
        {
            filter += "(1 = 2) ";
        }

        // 查询   
        return getPatientList(deptCode, filter);
    }


    /// <summary>
    /// 获取病人的体温过滤条件
    /// </summary>
    /// <returns></returns>
    public DataSet getPatientFitlered_Temperature(string deptCode, string temperature)
    {
        // 获取满足条件的病人ID
        string sql = string.Empty;

        sql += "SELECT DISTINCT PATIENT_INFO.PATIENT_ID, ";                             // 病人标识号
        sql += "PATIENT_INFO.VISIT_ID ";                                            // 病人本次住院标识
        sql += "FROM VITAL_SIGNS_REC, ";                                                    // 病人体症记录
        sql += "PATIENT_INFO ";                                                     // 在院病人记录
        sql += "WHERE PATIENT_INFO.WARD_CODE = " + SQL.SqlConvert(deptCode);            // 所在病房代码
        sql += "AND VITAL_SIGNS_REC.PATIENT_ID = PATIENT_INFO.PATIENT_ID ";         // 病人标识号
        sql += "AND VITAL_SIGNS_REC.VISIT_ID = PATIENT_INFO.VISIT_ID ";             // 病人本次住院标识
        sql += "AND VITAL_SIGNS_REC.VITAL_SIGNS LIKE '%体温' ";
        sql += "AND VITAL_SIGNS_REC.VITAL_SIGNS_CVALUES > " + SQL.SqlConvert(temperature);
        sql += "AND TO_DATE(VITAL_SIGNS_REC.TIME_POINT) > TO_DATE(SYSDATE) - 4 ";

        DataSet dsPatientId = oraAccess.SelectData(sql, "PATIENT_ID");

        string filter = string.Empty;
        foreach (DataRow dr in dsPatientId.Tables[0].Rows)
        {
            if (filter.Length > 0)
            {
                filter += "','";
            }

            filter += dr["PATIENT_ID"].ToString();
        }

        if (filter.Length > 0)
        {
            filter = " PATIENT_INFO.PATIENT_ID IN ('" + filter + "') ";
        }
        else
        {
            filter = " (1 = 2) ";
        }

        // 获取病区病人信息
        return getPatientList(deptCode, filter);
    }


    /// <summary>
    /// 获取医嘱过滤的病人
    /// </summary>
    /// <param name="deptCode"></param>
    /// <param name="orderText"></param>
    /// <returns></returns>
    public DataSet getPatientFiltered_Orders(string deptCode, string orderText)
    {
        string sql = "SELECT DISTINCT ORDERS_M.PATIENT_ID, ORDERS_M.VISIT_ID  ";        // 病人本次住院标识
        sql += "FROM ";
        sql += "ORDERS_M, ";                                                    // 医嘱
        sql += "PATIENT_INFO ";                                           // 在院病人
        sql += "WHERE ";
        sql += "( ";
        sql += "((REPEAT_INDICATOR = '1') AND (ORDERS_M.STOP_DATE_TIME IS NULL OR ORDERS_M.STOP_DATE_TIME > SYSDATE)) ";
        sql += "OR ((REPEAT_INDICATOR = '0') AND (TO_DATE(ORDERS_M.STOP_DATE_TIME) = TO_DATE(SYSDATE))) ";
        sql += ") ";
        sql += "AND PATIENT_INFO.PATIENT_ID = ORDERS_M.PATIENT_ID ";
        sql += "AND PATIENT_INFO.VISIT_ID = ORDERS_M.VISIT_ID ";
        sql += "AND PATIENT_INFO.WARD_CODE = " + SQL.SqlConvert(deptCode);
        sql += "AND ORDERS_M.ORDER_TEXT LIKE '%" + orderText + "%'";

        DataSet dsPatientId = oraAccess.SelectData(sql, "PATIENT_ID");

        string filter = string.Empty;
        foreach (DataRow dr in dsPatientId.Tables[0].Rows)
        {
            if (filter.Length > 0)
            {
                filter += "','";
            }

            filter += dr["PATIENT_ID"].ToString();
        }

        if (filter.Length > 0)
        {
            filter = " PATIENT_INFO.PATIENT_ID IN ('" + filter + "') ";
        }
        else
        {
            filter = " (1 = 2) ";
        }

        // 获取病区病人信息
        return getPatientList(deptCode, filter);

    }


    /// <summary>
    /// 获取医嘱过滤的病人
    /// </summary>
    /// <param name="deptCode"></param>
    /// <param name="orderText"></param>
    /// <returns></returns>
    public DataSet getPatientFiltered_OrdersExecute(string deptCode, string orderType)
    {
        // 获取用药途径 及其它参数
        string sql = "SELECT * FROM APP_CONFIG "
                   + "WHERE APP_CODE = '002' AND "
                   + "(DEPT_CODE IS NULL OR DEPT_CODE = " + SQL.SqlConvert(deptCode) + ") "
                   + "AND PARAMETER_NAME IN ('ADMINISTRATION_INFUSE', "
                   + "'ADMINISTRATION_TRANSFUSE', "
                   + "'ADMINISTRATION_DRUG', "
                   + "'ORDERS_EXECUTE_START') "
                   + "ORDER BY "
                   + "DEPT_CODE DESC";

        DataSet ds = oraAccess.SelectData(sql);

        if (ds == null || ds.Tables.Count == 0)
        {
            return null;
        }

        string injection = string.Empty;
        string transfuse = string.Empty;
        string drug = string.Empty;
        string treat = string.Empty;
        int dayStart = -1;

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string val = dr["PARAMETER_VALUE"].ToString().Trim();

            switch (dr["PARAMETER_NAME"].ToString())
            {
                case "ADMINISTRATION_INFUSE":
                    if (injection.Length == 0) { injection = val; }
                    break;
                case "ADMINISTRATION_TRANSFUSE":
                    if (transfuse.Length == 0) { transfuse = val; }
                    break;
                case "ADMINISTRATION_DRUG":
                    if (drug.Length == 0) { drug = val; }
                    break;
                case "ORDERS_EXECUTE_START":
                    if (dayStart == -1) { dayStart = int.Parse(val); }
                    break;
            }
        }

        injection = injection.Replace(ComConst.STR.BLANK, string.Empty);
        injection = "'" + injection.Replace(",", "','") + "'";

        transfuse = transfuse.Replace(ComConst.STR.BLANK, string.Empty);
        transfuse = "'" + transfuse.Replace(",", "','") + "'";

        drug = drug.Replace(ComConst.STR.BLANK, string.Empty);
        drug = "'" + drug.Replace(",", "','") + "'";

        treat = injection + "," + transfuse + "," + drug;

        if (dayStart == -1) { dayStart = 6; }

        // 生成过滤条件
        string filter = string.Empty;

        switch (orderType)
        {
            case "护理单":
                filter = " AND ORDERS_EXECUTE.ORDER_CLASS = 'H' ";
                break;
            case "服药单":
                filter += " AND ORDERS_EXECUTE.ADMINISTRATION IN (" + drug + ")";
                break;
            case "注射单":
                filter += " AND ORDERS_EXECUTE.ADMINISTRATION IN (" + injection + ")";
                break;
            case "输液单":
                filter += " AND ORDERS_EXECUTE.ADMINISTRATION IN (" + transfuse + ")";
                break;

            case "治疗单":
                //HS  修改于 2011年9月7号 。提供  除 “H” 类 以外的医嘱检索
                // 为治疗单 提供正确数据。但是有一点就是 ，希望能实现 对 具体医嘱类的限制。
                //9月8号 确定 这个 治疗单中的 内容是 按照 order_class 来取得值。 并且它是全院搞的，所以正如之前想的那样
                //在这个的 根目录下添加一个 配置文件 就行了，说明治疗单中 要包含的 order_class 类型。

                filter += "AND ORDERS_EXECUTE.ORDER_CLASS in (" + OrderClass() + ")";

                break;

            //case "治疗单":
            //    filter = " AND ORDERS_EXECUTE.ORDER_CLASS <> 'H' ";
            //    filter+= " AND ORDERS_EXECUTE.ADMINISTRATION NOT IN (" + treat + ")";
            //    break;
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT DISTINCT ");
        sb.Append("PATIENT_INFO.PATIENT_ID, "); // 病人标识号
        sb.Append("PATIENT_INFO.VISIT_ID "); // 病人本次住院标识
        sb.Append("FROM ORDERS_EXECUTE, "); // 医嘱执行表
        sb.Append("PATIENT_INFO "); // 在院病人记录
        sb.Append("WHERE PATIENT_INFO.WARD_CODE = " + SQL.SqlConvert(deptCode)); // 所在病房代码
        sb.Append("AND ORDERS_EXECUTE.PATIENT_ID = PATIENT_INFO.PATIENT_ID "); // 病人标识号
        sb.Append("AND ORDERS_EXECUTE.VISIT_ID = PATIENT_INFO.VISIT_ID "); // 病人本次住院标识
        sb.Append("AND ORDERS_EXECUTE.PERFORM_SCHEDULE >=TO_DATE(SYSDATE) + " + dayStart.ToString() + "/24 ");
        sb.Append("AND ORDERS_EXECUTE.PERFORM_SCHEDULE < TO_DATE(SYSDATE) + 1 + " + dayStart.ToString() + "/24 ");
        sb.Append("AND ORDERS_EXECUTE.IS_EXECUTE <> '1' ");

        DataSet dsPatientId = oraAccess.SelectData(sb.ToString() + filter, "PATIENT_ID");

        filter = string.Empty;
        foreach (DataRow dr in dsPatientId.Tables[0].Rows)
        {
            if (filter.Length > 0)
            {
                filter += "','";
            }

            filter += dr["PATIENT_ID"].ToString();
        }

        if (filter.Length > 0)
        {
            filter = " PATIENT_INFO.PATIENT_ID IN ('" + filter + "') ";
        }
        else
        {
            filter = " (1 = 2) ";
        }

        // 获取病区病人信息
        return getPatientList(deptCode, filter);
    }

    #region  为了治疗单 提供 医嘱类型
    private string OrderClass()
    {

        string path = System.AppDomain.CurrentDomain.BaseDirectory + "治疗单中的包含类型.txt";
        //string path = "E:\\天健科技\\天健内容\\成都三院\\MobileWebSrv\\治疗单中的包含类型.txt";

        StreamReader objreader = new StreamReader(path);
        string strLine = "";
        arraylist = new ArrayList();
        arraylist.Clear();
        while (strLine != null)
        {
            strLine = objreader.ReadLine();
            if (strLine != null)
            {
                arraylist.Add(strLine);
            }
        }
        string orderclass = "";
        for (int i = 0; i < arraylist.Count; i++)
        {
            orderclass = orderclass + "'" + arraylist[i].ToString() + "',";
        }
        return orderclass;
    }


    #endregion

    /// <summary>
    /// 获取PDA客户端超时时间
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public int GetPdaClientTimeout()
    {
        int _resultValue = 10;
        string pdaTimeout = string.Empty;
        try
        {
            pdaTimeout = "SELECT A.PARAMETER_VALUE FROM APP_CONFIG A    WHERE A.PARAMETER_NAME='PDA_TIMEOUT'";
            DataSet DsPdaClientTimeout = oraAccess.SelectData(pdaTimeout, "APP_CONFIG");
            if (DsPdaClientTimeout != null && DsPdaClientTimeout.Tables[0].Rows.Count > 0)
            {
                _resultValue = Convert.ToInt32(DsPdaClientTimeout.Tables[0].Rows[0][0]);
            }
            else
            {
                _resultValue = 10;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            _resultValue = 10;
        }

        return _resultValue;
    }
}

