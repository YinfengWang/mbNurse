using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data;
using System.Text;

using SQL = HISPlus.SqlManager;
using System.Data.SqlClient;
/*
 2015-05-26   lpd
 修改方法：GetSpeciment
 添加： A.SPCM_RECEIVED_DATE_TIME, 
 */
/// <summary>
///HISDataWebSrv 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class HISDataWebSrv : System.Web.Services.WebService
{

    private HISPlus.OracleAccess oraAccess = new HISPlus.OracleAccess();
    private HISPlus.OracleAccess mobileAccesss = new HISPlus.OracleAccess();
    private HISPlus.SqlserverAccess lisAccesss = new HISPlus.SqlserverAccess();
    private SqlConnection con = new SqlConnection();

    public HISDataWebSrv()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
        mobileAccesss.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString;
        oraAccess.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnectionHis"].ConnectionString;
        con.ConnectionString = ConfigurationManager.ConnectionStrings["SqlServerConnectionLis"].ConnectionString;
    }


    [WebMethod]
    public DataSet GetData(string sql, string tableName)
    {
        DataSet DsData = oraAccess.SelectData(sql, tableName);

        #region 日志

        //string logJsonValue = string.Empty;
        //if (DsData != null && DsData.Tables[0].Rows.Count > 0)
        //    logJsonValue = OperationLog.DataTableConvertJson.Dataset2Json(DsData);
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
        //    "tableName:" + tableName + "  " + "sql:" + sql + "  " + "tableName:" + tableName, logJsonValue);
        #endregion

        return DsData;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="patientId">病人ID</param>
    /// <param name="visitId"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetExamList(string patientId, string visitId)
    {
        string sql = "SELECT EXAM_NO, EXAM_CLASS, EXAM_SUB_CLASS, EXAM_DATE_TIME "
                            + "FROM EXAM_MASTER "
                            + "WHERE PATIENT_ID = " + SQL.SqlConvert(patientId)
                            + " AND VISIT_ID = " + SQL.SqlConvert(visitId);

        DataSet DsExamList = oraAccess.SelectData(sql, "EXAM_MASTER");

        #region 日志

        //日志
        //string logJsonValue = string.Empty;
        //if (DsExamList != null && DsExamList.Tables[0].Rows.Count > 0)
        //    logJsonValue = OperationLog.DataTableConvertJson.Dataset2Json(DsExamList);
        //logJsonValue = logJsonValue.Length <= 0 ? "空" : logJsonValue;
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
        //    "patientId:" + patientId + "  " + "visitId:" + visitId, logJsonValue);

        #endregion

        return DsExamList;


    }


    [WebMethod]
    public DataSet GetExamResult(string examNo)
    {
        string sql = "SELECT * FROM EXAM_REPORT WHERE EXAM_NO = " + SQL.SqlConvert(examNo);

        DataSet DsExamResult = oraAccess.SelectData(sql, "EXAM_REPORT");

        #region 日志

        //日志
        //string logJsonValue = string.Empty;
        //if (DsExamResult != null && DsExamResult.Tables[0].Rows.Count > 0)
        //    logJsonValue = OperationLog.DataTableConvertJson.Dataset2Json(DsExamResult);
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
        //    "examNo:" + examNo, logJsonValue);
        #endregion

        return DsExamResult;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="patientId">病人ID</param>
    /// <param name="visitId">住院次数</param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetLabTestList(string patientId, string visitId)
    {
        string sql = "SELECT RELEVANT_CLINIC_DIAG, SPECIMEN,EXECUTE_DATE,TEST_NO "
                            + "FROM LAB_TEST_MASTER "
                            + "WHERE PATIENT_ID = " + SQL.SqlConvert(patientId)
                            + " AND VISIT_ID = " + SQL.SqlConvert(visitId);

        DataSet DsLabTestList = oraAccess.SelectData(sql, "LAB_TEST_MASTER");

        #region 日志
        //日志
        //string JsonValue = string.Empty;
        //if (DsLabTestList != null && DsLabTestList.Tables[0].Rows.Count > 0)
        //    JsonValue = OperationLog.DataTableConvertJson.DataTableToJson(DsLabTestList.Tables[0]);
        //JsonValue = JsonValue.Length <= 0 ? "空" : JsonValue;
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
        //    "patientId:" + patientId + "  " + "visitId:" + visitId, JsonValue);

        #endregion

        return DsLabTestList;
    }


    [WebMethod]
    public DataSet GetLabTestResult(string testNo)
    {
        string sql = "SELECT * FROM LAB_RESULT WHERE TEST_NO = " + SQL.SqlConvert(testNo);

        DataSet DsLabTestResult = oraAccess.SelectData(sql, "LAB_RESULT");

        #region 日志
        //日志
        //string JsonValue = string.Empty;
        //if (DsLabTestResult != null && DsLabTestResult.Tables[0].Rows.Count > 0)
        //    JsonValue = OperationLog.DataTableConvertJson.DataTableToJson(DsLabTestResult.Tables[0]);
        //JsonValue = JsonValue.Length <= 0 ? "空" : JsonValue;
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
        //    "testNo:" + testNo, JsonValue);

        #endregion

        return DsLabTestResult;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="patientId">病人ID</param>
    /// <param name="visitId">住院次数</param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetSpeciment(string patientId, string visitId)
    {
        string sql = @"SELECT A.PATIENT_ID,
                       A.VISIT_ID,
                       A.TEST_NO,
                       A.RECEIVED_OPERATOR as SAMPLE_OPERATOR,----//20180601
                       A.SPCM_RECEIVED_DATE_TIME, 
                       A.SPCM_SAMPLE_DATE_TIME,
                       A.SPECIMEN,
                       B.SPECIMEN_ID, B.GATHER_TIME, B.GATHER_NURSE, B.CONSIGN_TIME, B.CONSIGN_NURSE, B.RECEIVE_TIME, 
                       B.RECIPIENT, B.REPORT_TIME, B.REPORTOR, B.CONSIGN_SHIPPER
                  FROM LAB_TEST_MASTER A, SPECIMENT_FLOW_REC B ";
        sql += " WHERE A.PATIENT_ID = " + SQL.SqlConvert(patientId) + " AND A.VISIT_ID = " + SQL.SqlConvert(visitId)
                    + " AND A.TEST_NO = B.TEST_NO(+) ";


        DataSet DsSpeciment = oraAccess.SelectData(sql, "SPECIMENT_FLOW_REC");

        #region 日志
        //日志
        //string logJsonValue = string.Empty;
        //if (DsSpeciment != null && DsSpeciment.Tables[0].Rows.Count > 0)
        //    logJsonValue = OperationLog.DataTableConvertJson.Dataset2Json(DsSpeciment);
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
        //    "patientId:" + patientId + "  " + "visitId:" + visitId, logJsonValue);

        #endregion

        return DsSpeciment;
    }

    [WebMethod]
    public bool SaveSpecimentTest(string testNo)
    {
        //////////////////////////////////
        string str = string.Format(@"SELECT  A.PATIENT_ID,
                               A.VISIT_ID,
                               A.TEST_NO,
                               A.RECEIVED_OPERATOR as SAMPLE_OPERATOR,----//20180601
                               A.SPCM_RECEIVED_DATE_TIME,
                               A.SPCM_SAMPLE_DATE_TIME,
                               A.SPECIMEN,
                               B.SPECIMEN_ID,
                               B.GATHER_TIME,
                               B.GATHER_NURSE,
                               B.CONSIGN_TIME,
                               B.CONSIGN_NURSE,
                               B.RECEIVE_TIME,
                               B.RECIPIENT,
                               B.REPORT_TIME,
                               B.REPORTOR,
                               B.CONSIGN_SHIPPER
                          FROM LAB_TEST_MASTER A, SPECIMENT_FLOW_REC B
                         WHERE A.TEST_NO = B.TEST_NO(+) and A.TEST_NO= '{0}'", testNo.Trim());
        DataSet dsChanged = oraAccess.SelectData(str, "SPECIMENT_FLOW_REC");

        if (dsChanged.Tables[0].Rows[0]["SAMPLE_OPERATOR"].ToString() == string.Empty)
        {
            dsChanged.Tables[0].Rows[0]["GATHER_TIME"] = "2018/6/5 15:43:16";
            dsChanged.Tables[0].Rows[0]["GATHER_NURSE"] = "WYF";
        }
        else
        {
            dsChanged.Tables[0].Rows[0]["GATHER_TIME"] = DBNull.Value;
            dsChanged.Tables[0].Rows[0]["GATHER_NURSE"] = DBNull.Value;
        }
        

        return SaveSpeciment(dsChanged.GetChanges());
        //////////////////////////////
    }
    /// <summary>
    /// 保存到HIS的SPECIMENT_FLOW_REC表
    /// </summary>
    /// <param name="dsChanged"></param>
    /// <returns></returns>
    [WebMethod]
    public bool SaveSpeciment(DataSet dsChanged)
    {


        OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
            "dsChanged:" + OperationLog.DataTableConvertJson.Dataset2Json(dsChanged), true.ToString());
        //oraAccess.BeginTrans();
        try
        {
            if (dsChanged == null || dsChanged.Tables.Count == 0 || dsChanged.Tables[0].Rows.Count == 0)
            {
                //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
                //    "dsChanged:" + OperationLog.DataTableConvertJson.Dataset2Json(dsChanged), true.ToString());
                return true;
            }

            // 删除
            DataRow[] drFind = dsChanged.Tables[0].Select(string.Empty, string.Empty, DataViewRowState.ModifiedCurrent | DataViewRowState.ModifiedOriginal);
            for (int i = 0; i < drFind.Length; i++)
            {
                string sql = "DELETE FROM SPECIMENT_FLOW_REC WHERE TEST_NO = " + SQL.SqlConvert(drFind[i]["TEST_NO"].ToString());
                oraAccess.ExecuteNoQuery(sql);
            }

            //if (drFind.Length > 0)
            //{
            //    //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
            //    //    "dsChanged:" + OperationLog.DataTableConvertJson.Dataset2Json(dsChanged), true.ToString());
            //    return true;
            //}

            if (UpdateLisSpeciment(dsChanged.Tables[0].Rows[0]["GATHER_TIME"].ToString().Trim() == string.Empty ? "-2" : "2", dsChanged.Tables[0].Rows[0]["TEST_NO"].ToString(), dsChanged.Tables[0].Rows[0]["SAMPLE_OPERATOR"].ToString(), dsChanged.Tables[0].Rows[0]["SPCM_SAMPLE_DATE_TIME"].ToString(), ""))
            {
                string sql;
                if (dsChanged.Tables[0].Rows[0]["GATHER_TIME"].ToString().Trim() != string.Empty)
                {
                    // 插入HIS
                    //oraAccess.Update(ref dsChanged, "SPECIMENT_FLOW_REC", "SELECT * FROM SPECIMENT_FLOW_REC");
                    sql = string.Format(@"INSERT INTO SPECIMENT_FLOW_REC
                                              (TEST_NO, SPECIMEN_ID, GATHER_TIME, GATHER_NURSE)
                                            values
                                              ('{0}',
                                               '{1}',
                                               to_date('{2}', 'yyyy-mm-dd hh24:mi:ss'),
                                               '{3}')", dsChanged.Tables[0].Rows[0]["TEST_NO"].ToString(), dsChanged.Tables[0].Rows[0]["SPECIMEN_ID"].ToString() == string.Empty ? "1" : dsChanged.Tables[0].Rows[0]["SPECIMEN_ID"].ToString(), dsChanged.Tables[0].Rows[0]["GATHER_TIME"].ToString(), dsChanged.Tables[0].Rows[0]["GATHER_NURSE"].ToString());
                    oraAccess.ExecuteNoQuery(sql);
                }
                sql = @"UPDATE LAB_TEST_MASTER SET SPCM_RECEIVED_DATE_TIME =
                                            TO_DATE('" + dsChanged.Tables[0].Rows[0]["GATHER_TIME"].ToString() + "','YYYY-MM-DD HH24:MI:SS'),RECEIVED_OPERATOR='" + dsChanged.Tables[0].Rows[0]["GATHER_NURSE"].ToString() + "' WHERE TEST_NO='" + dsChanged.Tables[0].Rows[0]["TEST_NO"].ToString() + "'";
                //            string updLabTestMasterSql = @"UPDATE LAB_TEST_MASTER SET SPCM_SAMPLE_DATE_TIME=
                //                                            TO_DATE('" + dsChanged.Tables[0].Rows[0]["SPCM_SAMPLE_DATE_TIME"].ToString() + "','YYYY-MM-DD HH24:MI:SS'),SAMPLE_OPERATOR='" + dsChanged.Tables[0].Rows[0]["SAMPLE_OPERATOR"].ToString() + "' WHERE TEST_NO='" + dsChanged.Tables[0].Rows[0]["TEST_NO"].ToString() + "'";
                oraAccess.ExecuteNoQuery(sql);
                //oraAccess.Commit();
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
            "异常:" + ex.Message, false.ToString());
            return false;
        }
        return true;
    }

    /// <summary>
    /// 更新LIS库中标本采样人和采样时间
    /// </summary>
    /// <param name="actionFlag"></param>
    /// <param name="barcode"></param>
    /// <param name="userNo"></param>
    /// <param name="actiontiem"></param>
    /// <param name="cationInfo"></param>
    /// <param name="flag"></param>
    /// <returns></returns>
    [WebMethod]
    public bool UpdateLisSpeciment(string actionFlag, string barcode, string userNo, string actionTime, string cationInfo)
    {
        SqlDataReader reader = null;
        try
        {

            con.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Connection = con;
            com.CommandText = "sp_pda_collect_barcode";


            IDataParameter[] parameters = {  
                new SqlParameter("@actionflag",SqlDbType.VarChar),  
                new SqlParameter("@barcode",SqlDbType.VarChar),  
                new SqlParameter("@userno",SqlDbType.VarChar),  
                new SqlParameter("@actiontime",SqlDbType.DateTime),  
                new SqlParameter("@actioninfo",SqlDbType.VarChar)
            };
            parameters[0].Value = actionFlag;
            parameters[1].Value = barcode;
            parameters[2].Value = userNo.Trim() == string.Empty ? "0000" : userNo;
            parameters[3].Value = actionTime.Trim() == string.Empty ? Convert.ToDateTime("1900/01/01 01:01:01") : Convert.ToDateTime((actionTime.Replace('-', '/')));
            parameters[4].Value = cationInfo;
            com.Parameters.AddRange(parameters);


            OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
            "WYF:" + actionFlag + barcode, userNo + "--" + actionTime);

            // @actionflag	varchar(20),	-- 2=采样，3=送出, -2=取消采样，-3取消送出
            //@barcode	varchar(20),
            //@userno	varchar(32),
            //@actiontime	datetime,
            //@actioninfo	varchar(512)	--相关信息，如送出时的交接人

            reader = com.ExecuteReader();
            if (reader.Read())
            {
                if (String.Format("{0}", reader[0]) == "1")
                    return true;
                else
                {
                    // throw new Exception(String.Format("{0}", reader[1]));
                    OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
                             "异常:" + String.Format("{0}", reader[1]), false.ToString());
                    return false;
                }
            }
            else
            {
                //throw new Exception(String.Format("{0}", "存储过程没有返回状态"));
                OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
                             "异常:" + "存储过程没有返回状态", false.ToString());
                return false;
            }
        }
        catch (Exception ex)
        {
            OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
            "异常:" + ex.Message, false.ToString());
            return false;
        }
        finally
        {
            if (reader != null)
                reader.Close();
            con.Close();
        }

    }

    //    /// <summary>
    //    /// 保存到HIS的SPECIMENT_FLOW_REC表
    //    /// </summary>
    //    /// <param name="dsChanged"></param>
    //    /// <returns></returns>
    //    [WebMethod]
    //    public bool SaveSpeciment(DataSet dsChanged)
    //    {
    //        OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
    //            "dsChanged:" + OperationLog.DataTableConvertJson.Dataset2Json(dsChanged), true.ToString());

    //        try
    //        {
    //            if (dsChanged == null || dsChanged.Tables.Count == 0 || dsChanged.Tables[0].Rows.Count == 0)
    //            {
    //                //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
    //                //    "dsChanged:" + OperationLog.DataTableConvertJson.Dataset2Json(dsChanged), true.ToString());
    //                return true;
    //            }

    //            // 删除
    //            DataRow[] drFind = dsChanged.Tables[0].Select(string.Empty, string.Empty, DataViewRowState.ModifiedCurrent | DataViewRowState.ModifiedOriginal);
    //            for (int i = 0; i < drFind.Length; i++)
    //            {
    //                string sql = "DELETE FROM SPECIMENT_FLOW_REC WHERE TEST_NO = " + SQL.SqlConvert(drFind[i]["TEST_NO"].ToString());
    //                oraAccess.ExecuteNoQuery(sql);
    //            }

    //            if (drFind.Length > 0)
    //            {
    //                //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
    //                //    "dsChanged:" + OperationLog.DataTableConvertJson.Dataset2Json(dsChanged), true.ToString());
    //                return true;
    //            }

    //            // 插入HIS
    //            oraAccess.Update(ref dsChanged, "SPECIMENT_FLOW_REC", "SELECT * FROM SPECIMENT_FLOW_REC");

    //            string updLabTestMasterSql = @"UPDATE LAB_TEST_MASTER SET SPCM_RECEIVED_DATE_TIME =
    //                                            TO_DATE('" + dsChanged.Tables[0].Rows[0]["SPCM_SAMPLE_DATE_TIME"].ToString() + "','YYYY-MM-DD HH24:MI:SS'),RECEIVED_OPERATOR='" + dsChanged.Tables[0].Rows[0]["SAMPLE_OPERATOR"].ToString() + "' WHERE TEST_NO='" + dsChanged.Tables[0].Rows[0]["TEST_NO"].ToString() + "'";
    //            //            string updLabTestMasterSql = @"UPDATE LAB_TEST_MASTER SET SPCM_SAMPLE_DATE_TIME=
    //            //                                            TO_DATE('" + dsChanged.Tables[0].Rows[0]["SPCM_SAMPLE_DATE_TIME"].ToString() + "','YYYY-MM-DD HH24:MI:SS'),SAMPLE_OPERATOR='" + dsChanged.Tables[0].Rows[0]["SAMPLE_OPERATOR"].ToString() + "' WHERE TEST_NO='" + dsChanged.Tables[0].Rows[0]["TEST_NO"].ToString() + "'";
    //            oraAccess.ExecuteNoQuery(updLabTestMasterSql);


    //        }
    //        catch (Exception ex)
    //        {
    //            OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
    //            "异常:" + ex.Message, false.ToString());
    //            return false;
    //        }




    //        return true;
    //    }

    /// <summary>
    /// 检查是否有新开医嘱
    /// </summary>
    /// <param name="deptCode">科室ID</param>
    /// <param name="preChkDate"></param>
    /// <returns></returns>
    [WebMethod]
    public string ChkNewOrder(string deptCode, string preChkDate)
    {
        string _returnValue = string.Empty;
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT MAX(ORDERS.ENTER_DATE_TIME)  ");
        sb.Append("FROM ORDERS, ");
        sb.Append("PATS_IN_HOSPITAL ");
        sb.Append("WHERE PATS_IN_HOSPITAL.WARD_CODE=" + SQL.SqlConvert(deptCode));
        sb.Append(" AND ORDERS.ENTER_DATE_TIME>" + SQL.GetOraDbDate(preChkDate));
        sb.Append(" AND ORDERS.ORDER_STATUS='6' ");
        sb.Append(" AND ((ORDERS.REPEAT_INDICATOR=0 AND ORDERS.STOP_DATE_TIME IS NOT NULL) OR (ORDERS.REPEAT_INDICATOR=1 AND ORDERS.STOP_DATE_TIME IS NULL)) ");
        sb.Append(" AND ORDERS.PATIENT_ID=PATS_IN_HOSPITAL.PATIENT_ID ");
        sb.Append(" AND ORDERS.VISIT_ID=PATS_IN_HOSPITAL.VISIT_ID ");
        if (oraAccess.SelectValue(sb.ToString()) == true)
        {
            string result = oraAccess.GetResult(0);

            if (result.Length == 0)
            {
                _returnValue = preChkDate;
            }
            else
            {
                _returnValue = result;
            }
        }
        else
        {
            _returnValue = preChkDate;
        }

        //日志
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(), "deptCode:" + deptCode + " " + "preChkDate:" + preChkDate, _returnValue);

        return _returnValue;

    }

    /// <summary>
    /// 检查是否有新开停的医嘱
    /// </summary>
    /// <param name="deptCode">科室ID</param>
    /// <param name="preChkDate"></param>
    /// <returns></returns>
    [WebMethod]
    public int ChkNewStopOrder(string deptCode)
    {
        int _chkNewOrderCount = 0;
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT COUNT(*) ");
        sb.Append("FROM ORDERS, ");
        sb.Append("PATS_IN_HOSPITAL ");
        sb.Append("WHERE PATS_IN_HOSPITAL.WARD_CODE=" + SQL.SqlConvert(deptCode));
        sb.Append(" AND ORDERS.ORDER_STATUS='6' ");
        sb.Append(" AND ORDERS.REPEAT_INDICATOR=1 ");
        sb.Append(" AND ORDERS.STOP_DATE_TIME IS NOT NULL ");
        sb.Append(" AND ORDERS.STOP_DOCTOR IS NOT NULL ");
        sb.Append(" AND ORDERS.NURSE IS NOT NULL ");
        sb.Append(" AND ORDERS.STOP_DATE_TIME>=TO_DATE(SYSDATE) ");
        sb.Append(" AND ORDERS.STOP_DATE_TIME<TO_DATE(SYSDATE+1) ");
        sb.Append(" AND ORDERS.PATIENT_ID=PATS_IN_HOSPITAL.PATIENT_ID ");
        sb.Append(" AND ORDERS.VISIT_ID=PATS_IN_HOSPITAL.VISIT_ID ");
        if (oraAccess.SelectValue(sb.ToString()) == true)
        {
            int result = int.Parse(oraAccess.GetResult(0));
            if (result == 0)
            {
                _chkNewOrderCount = 0;
            }
            else
            {
                _chkNewOrderCount = result;
            }
        }
        else
        {
            _chkNewOrderCount = 0;
        }

        //日志
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(), "deptCode:" + deptCode, _chkNewOrderCount.ToString());

        return _chkNewOrderCount;
    }

    /// <summary>
    /// 获取医嘱提醒的患者床标
    /// </summary>
    /// <param name="deptCode">科室ID</param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetPatientBedLabel(string deptCode)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT DISTINCT BED_REC.BED_LABEL ");//床号
        sb.Append("FROM ORDERS, ");//医嘱
        sb.Append("PATS_IN_HOSPITAL, ");//在院病人记录
        sb.Append("BED_REC ");
        sb.Append("WHERE ((ORDERS.ORDER_STATUS='6' AND ORDERS.ENTER_DATE_TIME>=TO_DATE(SYSDATE)  AND ((ORDERS.REPEAT_INDICATOR=0 AND ORDERS.STOP_DATE_TIME IS NOT NULL) OR (ORDERS.REPEAT_INDICATOR=1 AND ORDERS.STOP_DATE_TIME IS NULL))) ");//新开医嘱
        sb.Append("OR (ORDERS.ORDER_STATUS='2' AND ORDERS.ENTER_DATE_TIME>=TO_DATE(SYSDATE) AND ((ORDERS.REPEAT_INDICATOR=0 AND ORDERS.STOP_DATE_TIME IS NOT NULL AND ORDERS.NURSE IS NOT NULL) OR (ORDERS.REPEAT_INDICATOR=1 AND ORDERS.STOP_DATE_TIME IS NULL AND ORDERS.NURSE IS NOT NULL))) ");
        sb.Append("OR (ORDERS.ORDER_STATUS='6' AND ORDERS.REPEAT_INDICATOR=1 AND ORDERS.STOP_DATE_TIME IS NOT NULL AND ORDERS.STOP_DOCTOR IS NOT NULL AND ORDERS.STOP_DATE_TIME>=TO_DATE(SYSDATE) AND ORDERS.STOP_DATE_TIME<TO_DATE(SYSDATE+1)) ");
        sb.Append("OR (ORDERS.ORDER_STATUS='3' AND ORDERS.REPEAT_INDICATOR=1 AND ORDERS.STOP_DATE_TIME IS NOT NULL AND ORDERS.STOP_DOCTOR IS NOT NULL AND ORDERS.STOP_DATE_TIME>=TO_DATE(SYSDATE) AND ORDERS.STOP_DATE_TIME<TO_DATE(SYSDATE+1))) ");
        sb.Append("AND ORDERS.PATIENT_ID=PATS_IN_HOSPITAL.PATIENT_ID ");//病人ID号
        sb.Append("AND ORDERS.VISIT_ID=PATS_IN_HOSPITAL.VISIT_ID ");
        sb.Append("AND BED_REC.WARD_CODE=PATS_IN_HOSPITAL.WARD_CODE ");
        sb.Append("AND BED_REC.BED_NO=PATS_IN_HOSPITAL.BED_NO ");
        sb.Append("AND PATS_IN_HOSPITAL.WARD_CODE=" + SQL.SqlConvert(deptCode));
        sb.Append("ORDER BY BED_REC.BED_LABEL");
        DataSet DsPatientBedLabel = oraAccess.SelectData(sb.ToString());

        #region 日志

        //日志
        //string logJsonValue = string.Empty;
        //if (DsPatientBedLabel != null && DsPatientBedLabel.Tables[0].Rows.Count > 0)
        //    logJsonValue = OperationLog.DataTableConvertJson.Dataset2Json(DsPatientBedLabel);
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
        //    "deptCode:" + deptCode, logJsonValue);

        #endregion

        return DsPatientBedLabel;
    }

    /// <summary>
    /// 获取科室所以医嘱提醒患者的医嘱
    /// </summary>
    /// <param name="deptCode">科室ID</param>
    /// <returns>DataSet</returns>
    [WebMethod]
    public DataSet GetOrderRemindList(string deptCode)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT DISTINCT PAT_MASTER_INDEX.NAME, "); //姓名
        sb.Append("BED_REC.BED_LABEL, ");//床标
        sb.Append("ORDERS.REPEAT_INDICATOR, ");//医嘱类型
        sb.Append("ORDERS.ORDER_NO, ");
        sb.Append("ORDERS.ORDER_SUB_NO, ");
        sb.Append("ORDERS.DOCTOR, ");//医生
        sb.Append("ORDERS.ORDER_TEXT, ");//医嘱正文
        sb.Append("ORDERS.DOSAGE||ORDERS.DOSAGE_UNITS DOSAGE, ");//剂量
        sb.Append("ORDERS.ADMINISTRATION, ");//途径
        sb.Append("ORDERS.ORDER_STATUS, ");
        sb.Append("ORDERS.STOP_DATE_TIME, ");
        sb.Append("ORDERS.NURSE, ");
        sb.Append("ORDERS.STOP_NURSE, ");
        sb.Append("ORDERS.STOP_DOCTOR, ");
        sb.Append("ORDERS.ENTER_DATE_TIME, ");
        sb.Append("ORDERS.STOP_DATE_TIME ");
        sb.Append("FROM ORDERS, ");//医嘱
        sb.Append("PATS_IN_HOSPITAL, ");//在院病人记录
        sb.Append("PAT_MASTER_INDEX, ");//病人主索引
        sb.Append("BED_REC ");
        sb.Append("WHERE ((ORDERS.ORDER_STATUS='6' AND ORDERS.ENTER_DATE_TIME>=TO_DATE(SYSDATE)  AND ((ORDERS.REPEAT_INDICATOR=0 AND ORDERS.STOP_DATE_TIME IS NOT NULL) OR (ORDERS.REPEAT_INDICATOR=1 AND ORDERS.STOP_DATE_TIME IS NULL))) ");//新开医嘱
        sb.Append("OR (ORDERS.ORDER_STATUS='2' AND ORDERS.ENTER_DATE_TIME>=TO_DATE(SYSDATE) AND ((ORDERS.REPEAT_INDICATOR=0 AND ORDERS.STOP_DATE_TIME IS NOT NULL AND ORDERS.NURSE IS NOT NULL) OR (ORDERS.REPEAT_INDICATOR=1 AND ORDERS.STOP_DATE_TIME IS NULL AND ORDERS.NURSE IS NOT NULL))) ");
        sb.Append("OR (ORDERS.ORDER_STATUS='6' AND ORDERS.REPEAT_INDICATOR=1 AND ORDERS.STOP_DATE_TIME IS NOT NULL AND ORDERS.STOP_DOCTOR IS NOT NULL AND ORDERS.STOP_DATE_TIME>=TO_DATE(SYSDATE) AND ORDERS.STOP_DATE_TIME<TO_DATE(SYSDATE+1)) ");
        sb.Append("OR (ORDERS.ORDER_STATUS='3' AND ORDERS.REPEAT_INDICATOR=1 AND ORDERS.STOP_DATE_TIME IS NOT NULL AND ORDERS.STOP_DOCTOR IS NOT NULL AND ORDERS.STOP_DATE_TIME>=TO_DATE(SYSDATE) AND ORDERS.STOP_DATE_TIME<TO_DATE(SYSDATE+1))) ");
        sb.Append("AND ORDERS.PATIENT_ID=PATS_IN_HOSPITAL.PATIENT_ID ");//病人ID号
        sb.Append("AND ORDERS.VISIT_ID=PATS_IN_HOSPITAL.VISIT_ID ");
        sb.Append("AND PAT_MASTER_INDEX.PATIENT_ID=PATS_IN_HOSPITAL.PATIENT_ID ");
        sb.Append("AND BED_REC.WARD_CODE=PATS_IN_HOSPITAL.WARD_CODE ");
        sb.Append("AND BED_REC.BED_NO=PATS_IN_HOSPITAL.BED_NO ");
        sb.Append("AND PATS_IN_HOSPITAL.WARD_CODE=" + SQL.SqlConvert(deptCode));//在院病人所在科室

        DataSet DsOrderRemindList = oraAccess.SelectData(sb.ToString());

        #region 日志
        //日志
        //string logJsonValue = string.Empty;
        //if (DsOrderRemindList != null && DsOrderRemindList.Tables[0].Rows.Count > 0)
        //    logJsonValue = OperationLog.DataTableConvertJson.Dataset2Json(DsOrderRemindList);
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
        //    "deptCode:" + deptCode, logJsonValue);

        #endregion

        return DsOrderRemindList;
    }


    /// <summary>
    /// 获取医嘱提醒患者的医嘱
    /// </summary>
    /// <param name="deptCode">科室ID</param>
    /// <param name="bedLabel"></param>
    /// <returns>dataSet</returns>
    [WebMethod]
    public DataSet GetOrderRemindInfo(string deptCode, string bedLabel)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT DISTINCT PAT_MASTER_INDEX.NAME, "); //姓名
        sb.Append("BED_REC.BED_LABEL, ");//床标
        sb.Append("ORDERS.REPEAT_INDICATOR, ");//医嘱类型
        sb.Append("ORDERS.ORDER_NO, ");
        sb.Append("ORDERS.ORDER_SUB_NO, ");
        sb.Append("ORDERS.DOCTOR, ");//医生
        sb.Append("ORDERS.ORDER_TEXT, ");//医嘱正文
        sb.Append("ORDERS.DOSAGE||ORDERS.DOSAGE_UNITS DOSAGE, ");//剂量
        sb.Append("ORDERS.ADMINISTRATION, ");//途径
        sb.Append("ORDERS.ORDER_STATUS, ");
        sb.Append("ORDERS.STOP_DATE_TIME, ");
        sb.Append("ORDERS.NURSE, ");
        sb.Append("ORDERS.STOP_NURSE, ");
        sb.Append("ORDERS.STOP_DOCTOR, ");
        sb.Append("ORDERS.ENTER_DATE_TIME, ");
        sb.Append("ORDERS.STOP_DATE_TIME ");
        sb.Append("FROM ORDERS, ");//医嘱
        sb.Append("PATS_IN_HOSPITAL, ");//在院病人记录
        sb.Append("PAT_MASTER_INDEX, ");//病人主索引
        sb.Append("BED_REC ");
        sb.Append("WHERE ((ORDERS.ORDER_STATUS='6' AND ORDERS.ENTER_DATE_TIME>=TO_DATE(SYSDATE)  AND ((ORDERS.REPEAT_INDICATOR=0 AND ORDERS.STOP_DATE_TIME IS NOT NULL) OR (ORDERS.REPEAT_INDICATOR=1 AND ORDERS.STOP_DATE_TIME IS NULL))) ");//新开医嘱
        sb.Append("OR (ORDERS.ORDER_STATUS='2' AND ORDERS.ENTER_DATE_TIME>=TO_DATE(SYSDATE) AND ((ORDERS.REPEAT_INDICATOR=0 AND ORDERS.STOP_DATE_TIME IS NOT NULL AND ORDERS.NURSE IS NOT NULL) OR (ORDERS.REPEAT_INDICATOR=1 AND ORDERS.STOP_DATE_TIME IS NULL AND ORDERS.NURSE IS NOT NULL))) ");
        sb.Append("OR (ORDERS.ORDER_STATUS='6' AND ORDERS.REPEAT_INDICATOR=1 AND ORDERS.STOP_DATE_TIME IS NOT NULL AND ORDERS.STOP_DOCTOR IS NOT NULL AND ORDERS.STOP_DATE_TIME>=TO_DATE(SYSDATE) AND ORDERS.STOP_DATE_TIME<TO_DATE(SYSDATE+1)) ");
        sb.Append("OR (ORDERS.ORDER_STATUS='3' AND ORDERS.REPEAT_INDICATOR=1 AND ORDERS.STOP_DATE_TIME IS NOT NULL AND ORDERS.STOP_DOCTOR IS NOT NULL AND ORDERS.STOP_DATE_TIME>=TO_DATE(SYSDATE) AND ORDERS.STOP_DATE_TIME<TO_DATE(SYSDATE+1))) ");
        sb.Append("AND ORDERS.PATIENT_ID=PATS_IN_HOSPITAL.PATIENT_ID ");//病人ID号
        sb.Append("AND ORDERS.VISIT_ID=PATS_IN_HOSPITAL.VISIT_ID ");
        sb.Append("AND PAT_MASTER_INDEX.PATIENT_ID=PATS_IN_HOSPITAL.PATIENT_ID ");
        sb.Append("AND BED_REC.WARD_CODE=PATS_IN_HOSPITAL.WARD_CODE ");
        sb.Append("AND BED_REC.BED_NO=PATS_IN_HOSPITAL.BED_NO ");
        sb.Append("AND BED_REC.BED_LABEL=" + SQL.SqlConvert(bedLabel));
        sb.Append("AND PATS_IN_HOSPITAL.WARD_CODE=" + SQL.SqlConvert(deptCode));//在院病人所在科室
        DataSet DsOrderRemindInfo = oraAccess.SelectData(sb.ToString());

        #region 日志
        //日志
        //string logJsonValue = string.Empty;
        //if (DsOrderRemindInfo != null && DsOrderRemindInfo.Tables[0].Rows.Count > 0)
        //    logJsonValue = OperationLog.DataTableConvertJson.Dataset2Json(DsOrderRemindInfo);
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
        //    "deptCode:" + deptCode + "  " + "bedLabel:" + bedLabel, logJsonValue);

        #endregion

        return DsOrderRemindInfo;
    }

    /// <summary>
    /// 获取当前科室医嘱提醒的患者列表
    /// </summary>
    /// <param name="deptCode">科室ID</param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetOrderRemindPatientList(string deptCode)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT DISTINCT BED_REC.BED_LABEL, ");//床标
        sb.Append("PAT_MASTER_INDEX.NAME, "); //姓名
        sb.Append("PAT_MASTER_INDEX.SEX, ");
        sb.Append("PAT_MASTER_INDEX.DATE_OF_BIRTH ");
        sb.Append("FROM ORDERS, ");//医嘱
        sb.Append("PATS_IN_HOSPITAL, ");//在院病人记录
        sb.Append("PAT_MASTER_INDEX, ");//病人主索引
        sb.Append("BED_REC ");
        sb.Append("WHERE ((ORDERS.ORDER_STATUS='6' AND ORDERS.ENTER_DATE_TIME>=TO_DATE(SYSDATE)  AND ((ORDERS.REPEAT_INDICATOR=0 AND ORDERS.STOP_DATE_TIME IS NOT NULL) OR (ORDERS.REPEAT_INDICATOR=1 AND ORDERS.STOP_DATE_TIME IS NULL))) ");//新开医嘱
        sb.Append("OR (ORDERS.ORDER_STATUS='2' AND ORDERS.ENTER_DATE_TIME>=TO_DATE(SYSDATE) AND ((ORDERS.REPEAT_INDICATOR=0 AND ORDERS.STOP_DATE_TIME IS NOT NULL AND ORDERS.NURSE IS NOT NULL) OR (ORDERS.REPEAT_INDICATOR=1 AND ORDERS.STOP_DATE_TIME IS NULL AND ORDERS.NURSE IS NOT NULL))) ");
        sb.Append("OR (ORDERS.ORDER_STATUS='6' AND ORDERS.REPEAT_INDICATOR=1 AND ORDERS.STOP_DATE_TIME IS NOT NULL AND ORDERS.STOP_DOCTOR IS NOT NULL AND ORDERS.STOP_DATE_TIME>=TO_DATE(SYSDATE) AND ORDERS.STOP_DATE_TIME<TO_DATE(SYSDATE+1)) ");
        sb.Append("OR (ORDERS.ORDER_STATUS='3' AND ORDERS.REPEAT_INDICATOR=1 AND ORDERS.STOP_DATE_TIME IS NOT NULL AND ORDERS.STOP_DOCTOR IS NOT NULL AND ORDERS.STOP_DATE_TIME>=TO_DATE(SYSDATE) AND ORDERS.STOP_DATE_TIME<TO_DATE(SYSDATE+1))) ");
        sb.Append("AND ORDERS.PATIENT_ID=PATS_IN_HOSPITAL.PATIENT_ID ");//病人ID号
        sb.Append("AND ORDERS.VISIT_ID=PATS_IN_HOSPITAL.VISIT_ID ");
        sb.Append("AND PAT_MASTER_INDEX.PATIENT_ID=PATS_IN_HOSPITAL.PATIENT_ID ");
        sb.Append("AND BED_REC.WARD_CODE=PATS_IN_HOSPITAL.WARD_CODE ");
        sb.Append("AND BED_REC.BED_NO=PATS_IN_HOSPITAL.BED_NO ");
        sb.Append("AND PATS_IN_HOSPITAL.WARD_CODE=" + SQL.SqlConvert(deptCode));//在院病人所在科室
        sb.Append("ORDER BY BED_REC.BED_LABEL");

        DataSet DsOrderRemindPatientList = oraAccess.SelectData(sb.ToString());

        #region 日志
        //日志
        //string logJsonValue = string.Empty;
        //if (DsOrderRemindPatientList != null && DsOrderRemindPatientList.Tables[0].Rows.Count > 0)
        //    logJsonValue = OperationLog.DataTableConvertJson.Dataset2Json(DsOrderRemindPatientList);
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
        //    "deptCode:" + deptCode, logJsonValue);

        #endregion

        return DsOrderRemindPatientList;
    }
}

