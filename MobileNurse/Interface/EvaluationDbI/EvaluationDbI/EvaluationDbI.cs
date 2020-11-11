using System;
using System.Data;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class EvaluationDbI
    {
        #region 变量
        protected DbAccess  _connection     = null;             // 数据库连接
        
        private DataSet     dsItemDict      = null;             // 评估字典
        #endregion        
        
        public EvaluationDbI(DbAccess connection)
        {
            _connection = connection;
        }
        
        
        /// <summary>
        /// 获取评估项目表
        /// </summary>
        /// <param name="dictId"></param>
        /// <returns></returns>
        public  DataSet GetEvaluationDictItem(string dictId)
        {
            string sql = "SELECT * FROM HIS_DICT_ITEM "
                       + "WHERE DICT_ID = " + SqlManager.SqlConvert(dictId)
                       + "ORDER BY ITEM_ID ";
            
            return _connection.SelectData(sql, "HIS_DICT_ITEM");
        }        
        
        /// <summary>
        /// 获取评估实现表
        /// </summary>
        /// <param name="dictId"></param>
        /// <returns></returns>
        public  DataSet GetEvaluationImplement(string dictId)
        {
            string sql = "SELECT * FROM PAT_EVALUATION_IMPLEMENT "
                       + "WHERE DICT_ID = " + SqlManager.SqlConvert(dictId)
                       + "ORDER BY ITEM_ID ";
            
            return _connection.SelectData(sql, "HIS_DICT_ITEM");
        }        
        
        /// <summary>
        /// 获取评估记录
        /// </summary>
        /// <returns></returns>
        public DataSet GetEvaluationRec(string patientId, string visitId, string dictId, DateTime dtEvaluation)
        {
            string sql = "SELECT * FROM PAT_EVALUATION_M "
                       + "WHERE "
                       +    "PATIENT_ID = " + SqlManager.SqlConvert(patientId)
                       +    " AND VISIT_ID = " + SqlManager.SqlConvert(visitId)
                       +    " AND DICT_ID = " + SqlManager.SqlConvert(dictId)
                       +    " AND RECORD_DATE = " + SqlManager.GetOraDbDate(dtEvaluation)
                       + "ORDER BY "
                       +    "ITEM_ID ";
            
            return _connection.SelectData(sql, "PAT_EVALUATION_M");
        }
  
          
        /// <summary>
        /// 获取评估记录
        /// </summary>
        /// <returns></returns>
        public DataSet GetEvaluationRec(string patientId, string visitId, string dictId, DateTime dtStart, DateTime dtEnd)
        {
            dtEnd = dtEnd.AddDays(1);
            
            string sql = "SELECT * FROM PAT_EVALUATION_M "
                       + "WHERE "
                       +    "PATIENT_ID = " + SqlManager.SqlConvert(patientId)
                       +    " AND VISIT_ID = " + SqlManager.SqlConvert(visitId)
                       +    " AND DICT_ID = " + SqlManager.SqlConvert(dictId)
                       +    " AND RECORD_DATE >= " + SqlManager.GetOraDbDate(dtStart.Date)
                       +    " AND RECORD_DATE < " + SqlManager.GetOraDbDate(dtEnd.Date)
                       + "ORDER BY "
                       +    "ITEM_ID ";
            
            string filter = "A.PATIENT_ID = " + SqlManager.SqlConvert(patientId)
                       +    " AND A.VISIT_ID = " + SqlManager.SqlConvert(visitId)
                       +    " AND A.DICT_ID = " + SqlManager.SqlConvert(dictId);
            
            sql = "SELECT A.*, B.EXIST_CHILD ";
            sql += "FROM PAT_EVALUATION_M A, ";
            sql +=     "HIS_DICT_ITEM B ";
            sql += "WHERE " + filter;
            sql +=     "AND RECORD_DATE >= " + SqlManager.GetOraDbDate(dtStart.Date);
            sql +=     "AND RECORD_DATE < " + SqlManager.GetOraDbDate(dtEnd.Date);
            sql +=     "AND A.DICT_ID = B.DICT_ID ";
            sql +=     "AND A.ITEM_ID = B.ITEM_ID ";
            
            DataSet ds = _connection.SelectData(sql, "PAT_EVALUATION_M");
            deleteEmptyParentItem(ref ds);
            return ds;
        }
  
              
        /// <summary>
        /// 保存评估记录
        /// </summary>
        /// <returns></returns>
        public void SaveEvaluationRec(ref DataSet dsChanged, string patientId, string visitId, string dictId, DateTime dtEvaluation)
        {
            string sql = "SELECT * FROM PAT_EVALUATION_M "
                       + "WHERE "
                       +    "PATIENT_ID = " + SqlManager.SqlConvert(patientId)
                       +    " AND VISIT_ID = " + SqlManager.SqlConvert(visitId)
                       +    " AND DICT_ID = " + SqlManager.SqlConvert(dictId)
                       +    " AND RECORD_DATE = " + SqlManager.GetOraDbDate(dtEvaluation);
            
            _connection.Update(ref dsChanged, "PAT_EVALUATION_M", sql);
        }
        
        
        /// <summary>
        /// 获取评估记录
        /// </summary>
        /// <returns></returns>
        public DataSet GetEvaluationRec(string patientId, string visitId, string dictId)
        {
            string sql = "SELECT * FROM PAT_EVALUATION_M "
                       + "WHERE "
                       +    "PATIENT_ID = " + SqlManager.SqlConvert(patientId)
                       +    " AND VISIT_ID = " + SqlManager.SqlConvert(visitId)
                       +    " AND DICT_ID = " + SqlManager.SqlConvert(dictId)
                       + "ORDER BY "
                       +    "ITEM_ID ";
            
            return _connection.SelectData(sql, "PAT_EVALUATION_M");
        }        
        
        
        /// <summary>
        /// 保存评估记录
        /// </summary>
        /// <returns></returns>
        public void SaveEvaluationRec(ref DataSet dsChanged, string patientId, string visitId, string dictId)
        {
            string sql = "SELECT * FROM PAT_EVALUATION_M "
                       + "WHERE "
                       +    "PATIENT_ID = " + SqlManager.SqlConvert(patientId)
                       +    " AND VISIT_ID = " + SqlManager.SqlConvert(visitId)
                       +    " AND DICT_ID = " + SqlManager.SqlConvert(dictId);
            
            _connection.Update(ref dsChanged, "PAT_EVALUATION_M", sql);
        }

        
        #region 评估内容
        /// <summary>
        /// 获取评估记录
        /// </summary>
        /// <returns></returns>
        public string GetLastEvaluationResult(string patientId, string visitId)
        {
            // 获取评估记录 (每日评估优先)
            string dictId = "01";                 // 每日评估
            string filter = "PATIENT_ID = " + SqlManager.SqlConvert(patientId)
                       +    " AND VISIT_ID = " + SqlManager.SqlConvert(visitId)
                       +    " AND DICT_ID = " + SqlManager.SqlConvert(dictId);
            
            string sql = "SELECT MAX(RECORD_DATE) FROM PAT_EVALUATION_M WHERE " + filter;
            if (_connection.SelectValue(sql) == true && _connection.GetResult(0).Length > 0)
            {
                DateTime dtEval = DateTime.Parse(_connection.GetResult(0));
                return GetEvaluationResult(dictId, patientId, visitId, dtEval);
            }
            else
            {
                dictId = "02";                 // 入院评估
                filter = "PATIENT_ID = " + SqlManager.SqlConvert(patientId)
                       +    " AND VISIT_ID = " + SqlManager.SqlConvert(visitId)
                       +    " AND DICT_ID = " + SqlManager.SqlConvert(dictId);
                
                sql = "SELECT RECORD_DATE FROM PAT_EVALUATION_M WHERE " + filter;
                if (_connection.SelectValue(sql) == true && _connection.GetResult(0).Length > 0)
                {
                    DateTime dtEval = DateTime.Parse(_connection.GetResult(0));
                    return GetEvaluationResult(dictId, patientId, visitId, dtEval.Date);
                }
            }
            
            return string.Empty;
        }

        
        /// <summary>
        /// 获取评估记录
        /// </summary>
        /// <returns></returns>
        public string GetLastEvaluationResult(string patientId, string visitId, DateTime dtBegin, DateTime dtEnd)
        {
            // 获取评估记录 (每日评估优先)
            string dictId = "01";                 // 每日评估
            string filter = "PATIENT_ID = " + SqlManager.SqlConvert(patientId)
                       +    " AND VISIT_ID = " + SqlManager.SqlConvert(visitId)
                       +    " AND DICT_ID = " + SqlManager.SqlConvert(dictId)
                       +    "AND RECORD_DATE >= " + SqlManager.GetOraDbDate(dtBegin);
            
            string sql = "SELECT MAX(RECORD_DATE) FROM PAT_EVALUATION_M WHERE " + filter;
            if (_connection.SelectValue(sql) == true && _connection.GetResult(0).Length > 0)
            {
                DateTime dtEval = DateTime.Parse(_connection.GetResult(0));

                return GetEvaluationResult("01", patientId, visitId, dtEval);
            }
            else
            {
                //dictId = "02";                 // 入院评估
                //filter = "PATIENT_ID = " + SqlManager.SqlConvert(patientId)
                //       +    " AND VISIT_ID = " + SqlManager.SqlConvert(visitId)
                //       +    " AND DICT_ID = " + SqlManager.SqlConvert(dictId);
                //sql = "SELECT * FROM PAT_EVALUATION_M WHERE " + filter;

                //dsEvalRec = _connection.SelectData_NoKey(sql);
                return string.Empty;
            }
        }


        /// <summary>
        /// 获取评估记录
        /// </summary>
        /// <returns></returns>
        public string GetEvaluationResult(string dictId, string patientId, string visitId, DateTime dtEval)
        {
            string sql = string.Empty;

            // 获取评估字典
            if (dsItemDict == null)
            {
                sql = "SELECT * FROM HIS_DICT_ITEM WHERE DICT_ID IN ('01', '02')";
                dsItemDict = _connection.SelectData(sql, "HIS_DICT_ITEM");
            }

            // 获取评估记录
            //string dictId = "01";                 // 每日评估
            string filter = "A.PATIENT_ID = " + SqlManager.SqlConvert(patientId)
                       +    " AND A.VISIT_ID = " + SqlManager.SqlConvert(visitId)
                       +    " AND A.DICT_ID = " + SqlManager.SqlConvert(dictId);
            
            //sql = "SELECT * FROM PAT_EVALUATION_M WHERE " + filter + " AND RECORD_DATE = " + SQL.GetOraDbDate(dtEval);
            
            sql = "SELECT A.*, B.EXIST_CHILD ";
            sql += "FROM PAT_EVALUATION_M A, ";
            sql +=     "HIS_DICT_ITEM B ";
            sql += "WHERE " + filter;
            sql +=     "AND B.STANDBYINT IN ('1', '2') ";
            sql +=     "AND A.RECORD_DATE = " + SQL.GetOraDbDate(dtEval);
            sql +=     "AND A.DICT_ID = B.DICT_ID ";
            sql +=     "AND A.ITEM_ID = B.ITEM_ID ";
            
            DataSet dsEvalRec = _connection.SelectData_NoKey(sql);
            
            deleteEmptyParentItem(ref dsEvalRec);
            
            // 获取评估异常内容
            DataRow[] drFind = dsEvalRec.Tables[0].Select(string.Empty, "ITEM_ID");

            string str_tab      = "    ";
            string str_line     = new string('-', 80);
            string recordTag    = string.Empty;                            // 记录人与记录时间
            string content      = string.Empty;                            // 内容
            string itemIdGrpWNL = string.Empty;                            // 分组ID
            string itemIdGrp    = string.Empty;                            // 分组ID
            string itemIdPre    = string.Empty;                            // 前一个ID号
            bool   valPre       = false;                                   // 前一个项目是否有值

            for (int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];

                // 获取内容
                string itemId    = dr["ITEM_ID"].ToString();
                string itemName  = dr["ITEM_NAME"].ToString().ToUpper();
                string itemValue = dr["ITEM_VALUE"].ToString().Trim();

                switch (itemName)
                {
                    case "WNL":                                         // 正常
                        itemIdGrpWNL = itemId.Substring(0, itemId.Length / 2);
                        itemIdGrp    = string.Empty;

                        break;

                    default:                                            // 其它
                        if (itemIdGrpWNL.Length > 0 && itemId.StartsWith(itemIdGrpWNL))
                        {
                            break;
                        }

                        if (itemName.Equals("RECORDER") || itemName.Equals("RECORD_TIME"))
                        {
                            break;
                        }

                        // 如果分组变更
                        if (itemIdGrp.Length == 0 || itemId.StartsWith(itemIdGrp) == false)
                        {
                            valPre    = false;
                            itemIdPre = string.Empty;
                            itemIdGrp = itemId.Substring(0, 2);
                            content += ComConst.STR.CRLF;// + getGroupName(dictId, itemId) + ComConst.STR.CRLF + str_tab;
                        }

                        if (itemName.Equals("EXCEPTION") == false && itemName.Trim().Length > 0)
                        {
                            if (itemId.Length > 2)
                            {
                                if (itemIdPre.Length > 0)
                                {
                                    if (itemIdPre.Length < itemId.Length && valPre == false)
                                    {
                                        if (content.EndsWith(ComConst.STR.CRLF + str_tab) == false)
                                        {
                                            content += ComConst.STR.COLON;
                                        }
                                    }

                                    if (itemIdPre.Length == itemId.Length)
                                    {
                                        content += ComConst.STR.COMMA;
                                    }
                                }

                                content += (content.EndsWith(ComConst.STR.COLON)? string.Empty: ComConst.STR.BLANK) + itemName;

                                if (itemValue.Trim().Length > 0)
                                {
                                    content += (itemName.Length > 0 ? ComConst.STR.COLON : ComConst.STR.BLANK) + itemValue;
                                    valPre = true;
                                }
                                else
                                {
                                    valPre = false;
                                }
                            }
                        }
                        else
                        {
                            content += (itemValue.Trim().Length > 0 ? itemValue + ComConst.STR.CRLF + str_tab: string.Empty);
                        }
                        
                        break;
                }

                if (itemId.Length > 2)
                {
                    itemIdPre = itemId;
                }
                else
                {
                    itemIdPre = string.Empty;
                }
            }

            string blank4 = new string(' ', 4);
            string blank5 = new string(' ', 5);
            content = content.Replace(blank5 + ":", blank4);
            content = content.Replace(blank5, blank4);

            return content;
        }
        
        
        /// <summary>
        /// 删除空的父节点
        /// </summary>
        private void deleteEmptyParentItem(ref DataSet dsEvalRec)
        {
            // 处理规则: 对于非叶子节点, 如果它的子节点没有被选中, 删除
            DataRow[] drFind = dsEvalRec.Tables[0].Select("", "ITEM_ID");

            string filter = "ITEM_ID LIKE '{0}%' AND ITEM_ID <> '{1}' AND (EXIST_CHILD IS NULL OR EXIST_CHILD = '0') ";
            
            DataRow dr = null;
            bool blnParent = false;         // 是否是父节点
            string itemId  = string.Empty;
            for(int i = 0; i < drFind.Length; i++)
            {
                dr          = drFind[i];
                if (dr["EXIST_CHILD"].ToString().Equals("1") == false) continue;

                // 非叶子节点, 如果有值, 不删除
                if (dr["ITEM_VALUE"].ToString().Trim().Length > 0) continue;
                
                // 非叶子节点, 如果子节点没有被选中, 删除
                itemId      = dr["ITEM_ID"].ToString();
                if (dsEvalRec.Tables[0].Select(string.Format(filter, itemId, itemId)).Length == 0)
                {
                    dr.Delete();
                }
            }
            
            dsEvalRec.AcceptChanges();
        }
        
        
        /// <summary>
        /// 获取分组名称
        /// </summary>
        /// <returns></returns>
        private string getGroupName(string dictId, string itemId)
        {
            string filter = "DICT_ID = " + SQL.SqlConvert(dictId) + " AND ITEM_ID = " + SQL.SqlConvert(itemId.Substring(0, 2));
            DataRow[] drFind = dsItemDict.Tables[0].Select(filter);

            if (drFind.Length > 0)
            {
                return drFind[0]["ITEM_NAME"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
