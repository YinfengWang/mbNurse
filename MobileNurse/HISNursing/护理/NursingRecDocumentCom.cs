using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    class NursingRecDocumentCom
    {
        public DataSet  DsShow      = new DataSet();
        public DateTime Now;                                            // 现在的时间
        
        private DataSet dsItemDict  = null;
        private DateTime dtStart;                                       // 所取数据的时间范围
        private DateTime dtEnd;                                         // 所取数据的时间范围
        
        private TextBox txtEdit = new TextBox();                        // 用于计算分行
        
        public NursingRecDocumentCom()
		{
		    txtEdit.Multiline   = true;
		    txtEdit.ScrollBars  = ScrollBars.Vertical;
		}
		
        
		/// <summary>
		/// 创建缓存表
		/// </summary>
		/// <returns></returns>
		public bool CreateDataTable()
		{
		    // 获取表结构
            string sql = "SELECT * FROM NURSING_RECORD WHERE (1 = 2)";
            
            DsShow = GVars.SqlserverAccess.SelectData(sql);
            
            // 补充缺的字段
            DataTable dt = DsShow.Tables[0];
            
            dt.Columns.Add("SHOW_ORDER",        Type.GetType("System.Int32"));
		    dt.Columns.Add("TEMPERATURE",       Type.GetType("System.String"));
		    dt.Columns.Add("PULSE",             Type.GetType("System.String"));
		    dt.Columns.Add("BREATH",            Type.GetType("System.String"));
		    dt.Columns.Add("BLOOD_PRESSURE",    Type.GetType("System.String"));
		    dt.Columns.Add("COL1",              Type.GetType("System.String"));
		    dt.Columns.Add("COL2",              Type.GetType("System.String"));
		    dt.Columns.Add("ITEM_OUT",          Type.GetType("System.String"));
		    dt.Columns.Add("ITEM_OUT_AMOUNT",   Type.GetType("System.String"));
		    dt.Columns.Add("ITEM_IN",           Type.GetType("System.String"));
		    dt.Columns.Add("ITEM_IN_AMOUNT",    Type.GetType("System.String"));
		    
		    // 设置主健
		    DataColumn[] dcPrimary = new DataColumn[DsShow.Tables[0].PrimaryKey.Length + 1];
		    int i = 0;
		    for(i = 0; i < DsShow.Tables[0].PrimaryKey.Length; i++)
		    {
		        dcPrimary[i] = DsShow.Tables[0].PrimaryKey[i];
		    }
		    
		    dcPrimary[i] = DsShow.Tables[0].Columns["SHOW_ORDER"];
		    
		    DsShow.Tables[0].PrimaryKey = dcPrimary;
		    
		    return true;
		}
		
		
		/// <summary>
		/// 获取数据
		/// </summary>
		/// <param name="patientId"></param>
		/// <param name="visitId"></param>
		/// <param name="dtStart"></param>
		/// <param name="dtEnd"></param>
		/// <returns></returns>
		public bool SelectData(string patientId, string visitId, DateTime dtStart, DateTime dtEnd)
		{
		    // 把时间限定在实际工作的班次中
		    dtStart = dtStart.Date.AddHours(7);
		    dtEnd   = dtEnd.Date.AddDays(1).AddHours(7);
		    
		    this.dtStart = dtStart;
		    this.dtEnd   = dtEnd;
		    
		    // 清空原来的数据
		    DsShow.Tables[0].Rows.Clear();
		    
		    dsItemDict = getItemDict();
		    
		    // 首先获取病情记录
		    string sql = string.Empty;
            
            sql += "SELECT ";
            sql +=     "PATIENT_ID, VISIT_ID, TIME_POINT, SUM_FLG, 0 SHOW_ORDER, OBSERVE_NURSE, ";
            sql +=     "ROWS_COUNT_PRE, ROWS_COUNT, ROWS_DESC, PRINT_FLG, NURSE ";
            sql += "FROM ";
            sql +=     "NURSING_RECORD ";
            sql += "WHERE ";
            sql +=     "PATIENT_ID = " + SQL.SqlConvert(patientId);
            sql +=     "AND VISIT_ID = " + SQL.SqlConvert(visitId);
            sql +=     "AND TIME_POINT >= CONVERT(DATETIME, " + SQL.SqlConvert(dtStart.ToString(ComConst.FMT_DATE.LONG)) + ", 102) ";
            sql +=     "AND TIME_POINT < CONVERT(DATETIME, " + SQL.SqlConvert(dtEnd.ToString(ComConst.FMT_DATE.LONG)) + ", 102) ";
            sql += "ORDER BY ";
            sql += " TIME_POINT ASC";
            
            DataSet ds = GVars.SqlserverAccess.SelectData(sql);
            
            // 设置主健
		    DataColumn[] dcPrimary = new DataColumn[ds.Tables[0].PrimaryKey.Length + 1];
		    int i = 0;
		    for(i = 0; i < ds.Tables[0].PrimaryKey.Length; i++)
		    {
		        dcPrimary[i] = ds.Tables[0].PrimaryKey[i];
		    }
		    
		    dcPrimary[i] = ds.Tables[0].Columns["SHOW_ORDER"];
		    
		    ds.Tables[0].PrimaryKey = dcPrimary;
		    
            // 合并数据
            DsShow.Tables[0].Merge(ds.Tables[0]);            
            DsShow.AcceptChanges();
            
            // 获取护理项目数据
            sql = "SELECT * ";
            sql += "FROM VITAL_SIGNS_REC ";
            sql += "WHERE ";
            sql +=     "PATIENT_ID = " + SQL.SqlConvert(patientId);
            sql +=     "AND VISIT_ID = " + SQL.SqlConvert(visitId);
            sql +=     "AND TIME_POINT >= " + SQL.GetOraDbDate(dtStart.ToString(ComConst.FMT_DATE.LONG));
            sql +=     "AND TIME_POINT < " + SQL.GetOraDbDate(dtEnd.ToString(ComConst.FMT_DATE.LONG));
            sql += "ORDER BY ";
            sql += " TIME_POINT ASC";
            
            ds = GVars.OracleAccess.SelectData(sql);
            
            // 合并数据
            mergeNurseItemRec(ref ds, patientId, visitId);
            
            // 合计数据
            // sumNurseRec(patientId, visitId);
            
            return true;
		}
		
		
		/// <summary>
		/// 获取数据
		/// </summary>
		/// <param name="patientId"></param>
		/// <param name="visitId"></param>
		/// <param name="dtStart"></param>
		/// <param name="dtEnd"></param>
		/// <returns></returns>
		public bool SelectData(string patientId, string visitId)
		{	    
		    // 清空原来的数据
		    DsShow.Tables[0].Rows.Clear();
		    
		    dsItemDict = getItemDict();
		    
		    // 首先获取病情记录
		    string sql = string.Empty;
            
            sql += "SELECT ";
            sql +=     "PATIENT_ID, VISIT_ID, TIME_POINT, SUM_FLG, 0 SHOW_ORDER, OBSERVE_NURSE, ";
            sql +=     "ROWS_COUNT_PRE, ROWS_COUNT, ROWS_DESC, PRINT_FLG, NURSE ";
            sql += "FROM ";
            sql +=     "NURSING_RECORD ";
            sql += "WHERE ";
            sql +=     "PATIENT_ID = " + SQL.SqlConvert(patientId);
            sql +=     "AND VISIT_ID = " + SQL.SqlConvert(visitId);
            sql += "ORDER BY ";
            sql += " TIME_POINT ASC";
            
            DataSet ds = GVars.SqlserverAccess.SelectData(sql);
            
            // 设置主健
		    DataColumn[] dcPrimary = new DataColumn[ds.Tables[0].PrimaryKey.Length + 1];
		    int i = 0;
		    for(i = 0; i < ds.Tables[0].PrimaryKey.Length; i++)
		    {
		        dcPrimary[i] = ds.Tables[0].PrimaryKey[i];
		    }
		    
		    dcPrimary[i] = ds.Tables[0].Columns["SHOW_ORDER"];
		    
		    ds.Tables[0].PrimaryKey = dcPrimary;
		    
            // 合并数据
            DsShow.Tables[0].Merge(ds.Tables[0]);            
            DsShow.AcceptChanges();
            
            // 获取护理项目数据
            sql = "SELECT * ";
            sql += "FROM VITAL_SIGNS_REC ";
            sql += "WHERE ";
            sql +=     "PATIENT_ID = " + SQL.SqlConvert(patientId);
            sql +=     "AND VISIT_ID = " + SQL.SqlConvert(visitId);
            sql += "ORDER BY ";
            sql += " TIME_POINT ASC";
            
            ds = GVars.OracleAccess.SelectData(sql);
            
            // 合并数据
            mergeNurseItemRec(ref ds, patientId, visitId);
            
            return true;
		}

		
		/// <summary>
		/// 保存数据
		/// </summary>
		/// <returns></returns>
		public bool SaveData()
		{
		    ArrayList   arrSql      = new ArrayList();
		    string      sql         = string.Empty;
		    
		    DateTime    dtTimePoint;
		    string      timePoint0  = string.Empty;
		    string      timePoint1  = string.Empty;
		    
		    // 删除
		    DataRow[] drFind = DsShow.Tables[0].Select(string.Empty, string.Empty, DataViewRowState.Deleted);
            
		    for(int i = 0; i < drFind.Length; i++)
		    {
		        drFind[i].RejectChanges();
		        
		        dtTimePoint = DateTime.Parse(drFind[i]["TIME_POINT"].ToString());
		        timePoint0  = dtTimePoint.ToString(ComConst.FMT_DATE.LONG_MINUTE);
		        timePoint1  = timePoint0 + ":59";
		        
		        sql = "DELETE FROM NURSING_RECORD WHERE ";
		        sql += "PATIENT_ID = " + SQL.SqlConvert(drFind[i]["PATIENT_ID"].ToString());
		        sql += "AND VISIT_ID = " + SQL.SqlConvert(drFind[i]["VISIT_ID"].ToString());
		        sql += "AND TIME_POINT >= " + SQL.SqlConvert(timePoint0);
		        sql += "AND TIME_POINT <= " + SQL.SqlConvert(timePoint1);
		        
		        arrSql.Add(sql);
		        
		        drFind[i].Delete();
		    }
		    
		    // 修改
		    drFind = DsShow.Tables[0].Select(string.Empty, string.Empty, DataViewRowState.ModifiedCurrent);
            
		    for(int i = 0; i < drFind.Length; i++)
		    {
		        dtTimePoint = DateTime.Parse(drFind[i]["TIME_POINT"].ToString());
		        timePoint0  = dtTimePoint.ToString(ComConst.FMT_DATE.LONG_MINUTE);
		        timePoint1  = timePoint0 + ":59";
		        
		        sql = "UPDATE NURSING_RECORD ";
		        sql += "SET ";
		        sql +=     "SUM_FLG = " + SQL.SqlConvert(drFind[i]["SUM_FLG"].ToString());
		        sql +=     ",OBSERVE_NURSE = " + SQL.SqlConvert(drFind[i]["OBSERVE_NURSE"].ToString());
		        // sql +=     ",ROWS_COUNT_PRE = " + SQL.SqlConvert(drFind[i]["ROWS_COUNT_PRE"].ToString());
		        sql +=     ",ROWS_COUNT = " + SQL.SqlConvert(drFind[i]["ROWS_COUNT"].ToString());
		        sql +=     ",ROWS_DESC = " + SQL.SqlConvert(drFind[i]["ROWS_DESC"].ToString());
		        sql +=     ",PRINT_FLG = " + SQL.SqlConvert(drFind[i]["PRINT_FLG"].ToString());
		        sql += "WHERE ";
		        sql += "PATIENT_ID = " + SQL.SqlConvert(drFind[i]["PATIENT_ID"].ToString());
		        sql += "AND VISIT_ID = " + SQL.SqlConvert(drFind[i]["VISIT_ID"].ToString());
		        sql += "AND TIME_POINT >= " + SQL.SqlConvert(timePoint0);
		        sql += "AND TIME_POINT <= " + SQL.SqlConvert(timePoint1);
                		        
		        arrSql.Add(sql);
		    }
		    
		    // 新增
		    SQL.DbField[] arrF = new SQL.DbField[10];
		    int field = 0;
		    
		    drFind = DsShow.Tables[0].Select(string.Empty, string.Empty, DataViewRowState.Added);
            
		    for(int i = 0; i < drFind.Length; i++)
		    {
		        field = 0;
		        
                arrF[field++] = SQL.GetDbField_Sql("PATIENT_ID",     drFind[i]["PATIENT_ID"].ToString());
                arrF[field++] = SQL.GetDbField_Sql("VISIT_ID",       drFind[i]["VISIT_ID"].ToString());
                arrF[field++] = SQL.GetDbField_Sql("TIME_POINT",     drFind[i]["TIME_POINT"].ToString(), SqlManager.FIELD_TYPE.DATE);
                arrF[field++] = SQL.GetDbField_Sql("SUM_FLG",        drFind[i]["SUM_FLG"].ToString());
                arrF[field++] = SQL.GetDbField_Sql("OBSERVE_NURSE",  drFind[i]["OBSERVE_NURSE"].ToString());
                // arrF[field++] = SQL.GetDbField_Sql("ROWS_COUNT_PRE", drFind[i]["ROWS_COUNT_PRE"].ToString());
                arrF[field++] = SQL.GetDbField_Sql("ROWS_COUNT",     drFind[i]["ROWS_COUNT"].ToString());
                arrF[field++] = SQL.GetDbField_Sql("ROWS_DESC",      drFind[i]["ROWS_DESC"].ToString());
                arrF[field++] = SQL.GetDbField_Sql("PRINT_FLG",      drFind[i]["PRINT_FLG"].ToString());
                arrF[field++] = SQL.GetDbField_Sql("NURSE",          drFind[i]["NURSE"].ToString());
                
                sql = SQL.GetSqlInsert("NURSING_RECORD", arrF, field);
		        arrSql.Add(sql);
		    }
		    
		    // 执行保存
		    GVars.SqlserverAccess.ExecuteNoQuery(ref arrSql);
		    
		    DsShow.AcceptChanges();
		    
		    return true;
		}
		
		
		/// <summary>
		/// 设置病情描述的列宽
		/// </summary>
		public void SetDescWidth(int width)
		{
		    txtEdit.Width = width;
		}
		
		
		/// <summary>
		/// 获取护理项目字典
		/// </summary>
		/// <returns></returns>
		private DataSet getItemDict()
		{
		    string sql = "SELECT * FROM NURSE_TEMPERATURE_ITEM_DICT ";
		    sql += "WHERE WARD_CODE = " + SQL.SqlConvert(GVars.User.DeptCode);
		    
		    return GVars.SqlserverAccess.SelectData(sql);
		}
		
		
		/// <summary>
		/// 合并护理测量数据
		/// </summary>
		private void mergeNurseItemRec(ref DataSet ds, string patientId, string visitId)
		{
            string filterTime   = "(TIME_POINT >= '{0}' AND TIME_POINT <= '{1}')";    // 查询条件
            string timePoint    = string.Empty;                         // 时间点
            string timePointEnd = string.Empty;
            string vitalSigns   = string.Empty;                         // 项目名称
            string vitalCode    = string.Empty;                         // 项目代码
            
            DataRow[] drFind    = null;
            string filterType   = "(ATTRIBUTE >= '0' AND ATTRIBUTE < '3') AND VITAL_CODE = '{0}'";
            int    type         = 0;
            
            DataRow drRec       = null;
            int    rowCount     = 0;
            string cValue       = string.Empty;
            
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                vitalCode   = dr["VITAL_CODE"].ToString();
                vitalSigns  = dr["VITAL_SIGNS"].ToString();
                
                timePoint   = DateTime.Parse(dr["TIME_POINT"].ToString()).ToString(ComConst.FMT_DATE.LONG_MINUTE);
                timePointEnd= timePoint + ":59";
                
                // 条件检查
                drFind = dsItemDict.Tables[0].Select(string.Format(filterType, vitalCode));
                if (drFind.Length == 0) { continue; }
                
                type = int.Parse(drFind[0]["ATTRIBUTE"].ToString());
                
                if (type == 0)
                {
                    if (vitalSigns.IndexOf("体温") < 0
                      && vitalSigns.Equals("脉搏") == false
                      && vitalSigns.Equals("呼吸") == false
                      && vitalSigns.Equals("血压") == false)
                    {
                        continue;
                    }
                }
                
                // 判断对应时间点记录是否存在
                string filterItem = string.Format(filterTime, timePoint, timePointEnd);
                
                drFind = DsShow.Tables[0].Select(filterItem);
                rowCount = drFind.Length;
                
                if (type == 1 || type == 2)
                {
                    if (type == 1) { filterItem += " AND (ITEM_IN IS NULL OR ITEM_IN = '')"; }
                    if (type == 2) { filterItem += " AND (ITEM_OUT IS NULL OR ITEM_OUT = '')"; }
                    
                    drFind = DsShow.Tables[0].Select(filterItem);
                }
                
                if (drFind.Length == 0)
                {
                    drRec = DsShow.Tables[0].NewRow();
                    drRec["PATIENT_ID"]     = patientId;
                    drRec["VISIT_ID"]       = visitId;
                    drRec["TIME_POINT"]     = timePoint;
                    drRec["SUM_FLG"]        = "0";
                    drRec["SHOW_ORDER"]     = rowCount++;
                    drRec["ROWS_COUNT_PRE"] = "0";
                    drRec["ROWS_COUNT"]     = "1";
                    drRec["ROWS_DESC"]      = "1";
                    
                    DsShow.Tables[0].Rows.Add(drRec);
                }
                else
                {
                    drRec = drFind[0];
                }
                
                // 赋值
                string fieldName = string.Empty;
                
                if (type == 1)
                {
                    fieldName = "ITEM_IN_AMOUNT";
                    drRec["ITEM_IN"] = vitalSigns;
                }
                
                if (type == 2)
                {
                    fieldName = "ITEM_OUT_AMOUNT";
                    drRec["ITEM_OUT"] = vitalSigns;
                }
                
                if (type == 0)
                {
                    if (vitalSigns.IndexOf("体温") >= 0) { fieldName = "TEMPERATURE"; }
                    if (vitalSigns.Equals("脉搏")) { fieldName = "PULSE"; }
                    if (vitalSigns.Equals("呼吸")) { fieldName = "BREATH"; }
                    if (vitalSigns.Equals("血压")) { fieldName = "BLOOD_PRESSURE"; }
                }
                
                cValue = dr["VITAL_SIGNS_CVALUES"].ToString();
                
                if (DataType.IsPositive(cValue) == true)
                {
                    cValue = (float.Parse(cValue)).ToString();
                }
                
                drRec[fieldName] = cValue;
                
                // 记录状态控制 (drFind.Length > 0 表示非新增, rowCount > 0表示该时间点有对应记录)
                if (drFind.Length > 0 || rowCount > 1)
                {
                    drRec.AcceptChanges();
                }
                
                continue;
            }            
		}
		
		
		/// <summary>
		/// 对护理数据进行24小时小结, 7:00 - 7:00
		/// </summary>
		private void sumNurseRec(string patientId, string visitId)
		{
		    DateTime dtTime0 = dtStart;
		    DateTime dtTime1 = dtStart.AddHours(ComConst.VAL.HOURS_PER_DAY / 2);
		    
		    string filterFmt = "SUM_FLG = '0' AND TIME_POINT >= '{0}' AND TIME_POINT < '{1}'";
		    string filter    = string.Empty;
		    DataRow[] drFind = null;
		    DataRow drSum    = null;
            
            string desc      = string.Empty;
            
		    while(dtTime1.CompareTo(dtEnd) < 0)
		    {
		        // 生成总结描述
		        filter = string.Format(filterFmt, dtTime0.ToString(ComConst.FMT_DATE.LONG), dtTime1.ToString(ComConst.FMT_DATE.LONG));
		        drFind = DsShow.Tables[0].Select(filter);
		        
		        desc = getSumDesc(ref drFind, true) + ComConst.STR.CRLF + getSumDesc(ref drFind, false);

                // 如果有总结
                if (desc.Trim().Length > 0)
                {
                    // 分行
                    txtEdit.Text = desc;

                    int lines = Win32API.SendMessageA(txtEdit.Handle.ToInt32(), Win32API.EM_GETLINECOUNT, 0, 0);
                    int pos1 = 0;
                    int pos0 = Win32API.SendMessageA(txtEdit.Handle.ToInt32(), Win32API.EM_LINEINDEX, 0, 0);
                    int row = 0;

                    desc = string.Empty;
                    for (row = 1; row < lines; row++)
                    {
                        pos1 = Win32API.SendMessageA(txtEdit.Handle.ToInt32(), Win32API.EM_LINEINDEX, row, 0);

                        desc += txtEdit.Text.Substring(pos0, pos1 - pos0) + ComConst.STR.TAB;

                        pos0 = pos1;
                    }

                    desc += txtEdit.Text.Substring(pos0, txtEdit.Text.Length - pos0);

                    // 查找总结记录
                    filter = "SUM_FLG = '1' AND TIME_POINT = '" + dtTime1.ToString(ComConst.FMT_DATE.LONG) + "'";
                    drFind = DsShow.Tables[0].Select(filter);

                    if (drFind.Length == 0)
                    {
                        drSum = DsShow.Tables[0].NewRow();

                        drSum["PATIENT_ID"] = patientId;
                        drSum["VISIT_ID"] = visitId;
                        drSum["TIME_POINT"] = dtTime1.ToString(ComConst.FMT_DATE.LONG);
                        drSum["SUM_FLG"] = "1";
                        drSum["SHOW_ORDER"] = "0";
                        drSum["ROWS_COUNT_PRE"] = "0";
                        drSum["ROWS_COUNT"] = "2";
                        drSum["ROWS_DESC"] = "2";

                        DsShow.Tables[0].Rows.Add(drSum);
                    }
                    else
                    {
                        drSum = drFind[0];
                    }

                    // 保存数据
                    drSum["ROWS_COUNT"] = (lines > 2 ? lines : 2).ToString();
                    drSum["ROWS_DESC"] = lines.ToString();
                    drSum["OBSERVE_NURSE"] = desc;
                }

                // 下一个合计
                if (dtTime1.Hour == 7)          // 如果是24小时合计
                {
                    dtTime0 = dtTime1;
                }
                
                dtTime1 = dtTime1.AddHours(ComConst.VAL.HOURS_PER_DAY / 2);
		    }
		}
		
		
		/// <summary>
		/// 获取总结描述
		/// </summary>
		/// <param name="drFind"></param>
		/// <returns></returns>
		private string getSumDesc(ref DataRow[] drFind, bool blnIn)
		{
            string descItem  = string.Empty;                            // 已描述项目名称列表, 以Tab分隔
		    string desc      = string.Empty;                            // 总结描述
            
            string item      = string.Empty;
            string itemValue = string.Empty;
            int    pos       = 0;
            float  sum       = 0;
            
            string field_Name   = "ITEM_IN";
            string field_Value  = "ITEM_IN_AMOUNT";
            
            if (blnIn == false)
            {
                field_Name   = "ITEM_OUT";
                field_Value  = "ITEM_OUT_AMOUNT";
            }
            
		    // 入量
		    while(pos < drFind.Length)
		    {
		        // 获取项目名称
		        item        = drFind[pos][field_Name].ToString();
		        itemValue   = drFind[pos][field_Value].ToString();
		        
		        if (item.Length == 0 || descItem.IndexOf(item) >= 0)
		        {
		            pos++;
		            continue;
		        }
                
                // 获取该类项目的值的和		        
		        sum = 0;
		        if (DataType.IsPositive(itemValue) == true)
		        {
		            sum = float.Parse(itemValue);
		        }
		        
		        for(int i = pos + 1; i < drFind.Length; i++)
		        {
		            if (item.Equals(drFind[i][field_Name].ToString()) == false)
		            {
		                continue;
		            }
		            
		            itemValue = drFind[i][field_Value].ToString();
		            
		            if (DataType.IsPositive(itemValue) == true)
		            {
		                sum += float.Parse(itemValue);
		            }   
		        }
		        
		        // 输出
		        descItem += item + ComConst.STR.TAB;                        // 已描述项目
		        desc += item + ":" + sum.ToString() + "; ";		        
		        
		        // 下一项目
		        pos++;
		    }
		    
		    if (desc.EndsWith("; ") == true)
		    {
		        desc = desc.Substring(0, desc.Length - 2);
		    }
		    
		    return desc;
		}
    }
}
