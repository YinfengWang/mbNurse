using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using TemperatureBLL;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class BodyTemperatureCom
    {
        #region ����
        protected BodyTemperParams _bodyTemperParams = null;         // ���µ�����

        private DataSet dsAppConfig = null;         // Ӧ�ó������
        private DataSet dsNursingItemDict = null;         // �����¼�����

        private string inpVitalCode = string.Empty; // ��Ժ����
        private string outpVitalCode = string.Empty; // ��Ժ����
        private string shiftVitalCode = string.Empty; // ת�ƴ���

        private string inpVitalName = string.Empty; // ��Ժ����
        private string outpVitalName = string.Empty; // ��Ժ����
        private string shiftVitalName = string.Empty; // ת�ƴ���
        #endregion


        #region ����
        /// <summary>
        /// ���µ�����
        /// </summary>
        public BodyTemperParams Params
        {
            get { return _bodyTemperParams; }
            set { _bodyTemperParams = value; }
        }
        #endregion


        /// <summary>
        /// ��ȡ�������¼�¼
        /// </summary>
        /// <param name="patientId">���˱�ʶ��</param>
        /// <param name="visitId">���ξ������</param>
        /// <returns>TRUE: �ɹ�; FALSE: ʧ��</returns>
        public DataSet GetVitalSignsRec(string patientId, string visitId)
        {
            // ��ȡ�����¼�
            string sql = string.Empty;

            sql = "SELECT VITAL_SIGNS_REC.*, NURSING_ITEM_DICT.ATTRIBUTE ";
            sql += "FROM VITAL_SIGNS_REC, NURSING_ITEM_DICT ";
            sql += "WHERE VITAL_SIGNS_REC.PATIENT_ID = " + SQL.SqlConvert(patientId);
            sql += "AND VITAL_SIGNS_REC.VISIT_ID = " + SQL.SqlConvert(visitId);
            sql += "AND VITAL_SIGNS_REC.WARD_CODE = NURSING_ITEM_DICT.WARD_CODE ";
            sql += "AND VITAL_SIGNS_REC.VITAL_CODE = NURSING_ITEM_DICT.VITAL_CODE ";

            DataSet ds = GVars.OracleAccess.SelectData(sql, "VITAL_SIGNS_REC");

            return ds;
        }

        /// <summary>
        /// ��ȡӦ�ó������
        /// </summary>
        /// <returns></returns>
        public DataSet GetParameters()
        {
            string sql = "SELECT * FROM APP_CONFIG WHERE APP_CODE = " + SQL.SqlConvert(GVars.App.Right);

            dsAppConfig = GVars.OracleAccess.SelectData(sql, "APP_CONFIG");
            return dsAppConfig;
        }


        /// <summary>
        /// �Զ����ɻ����¼�
        /// </summary>
        /// <param name="patientId">���˱�ʶ��</param>
        /// <param name="visitId">���ξ������</param>
        /// <returns>TRUE: �ɹ�; FALSE: ʧ��</returns>
        public bool AutoGenerateVitalSigns(string patientId, string visitId)
        {
            string sql = string.Empty;

            // ��ȡ������Ŀ�ֵ�
            if (dsNursingItemDict == null)
            {
                sql = "SELECT * FROM NURSING_ITEM_DICT WHERE WARD_CODE = " + SQL.SqlConvert(GVars.User.DeptCode);
                dsNursingItemDict = GVars.OracleAccess.SelectData(sql, "NURSING_ITEM_DICT");
            }

            // ��ȡ�����¼�����
            getVitalCode();

            // ��ȡҽ������
            string filter = "PATIENT_ID = " + SQL.SqlConvert(patientId) + "AND VISIT_ID = " + SQL.SqlConvert(visitId);
            sql = "SELECT * FROM ORDERS WHERE ORDER_STATUS IN ('2', '3', '6', '7') AND START_DATE_TIME > SYSDATE - 7 AND " + filter;

            DataSet dsOrders = GVars.OracleAccess.SelectData(sql, "ORDERS");

            // ��ȡת�Ƽ�¼
            sql = "SELECT * FROM ADT_LOG WHERE ACTION IN ('C', 'D') AND LOG_DATE_TIME > SYSDATE - 7 AND " + filter;
            DataSet dsAdtLog = GVars.OracleAccess.SelectData(sql, "ADT_LOG");

            // ��ȡ�����¼
            sql = "SELECT * FROM VITAL_SIGNS_REC WHERE TIME_POINT > SYSDATE - 7 AND " + filter;
            DataSet dsNursingRecord = GVars.OracleAccess.SelectData(sql, "VITAL_SIGNS_REC");
            dsNursingRecord.AcceptChanges();

            // ��Ժ�¼�
            DateTime dtTime = DateTime.MinValue;
            filter = "ORDER_TEXT LIKE '%��Ժ%'";
            DataRow[] drFind = dsOrders.Tables[0].Select(filter);
            if (drFind.Length > 0)
            {
                dtTime = (DateTime)(drFind[0]["START_DATE_TIME"]);
                addNursingEvent(patientId, visitId, outpVitalCode, outpVitalName, dtTime, ref dsNursingRecord, true);
            }

            // ��Ժ�¼�
            filter = "ACTION = 'C'";
            drFind = dsAdtLog.Tables[0].Select(filter);
            if (drFind.Length > 0)
            {
                dtTime = (DateTime)(drFind[0]["LOG_DATE_TIME"]);
                filter = "START_DATE_TIME >= " + SQL.SqlConvert(dtTime.ToString(ComConst.FMT_DATE.LONG));
                drFind = dsOrders.Tables[0].Select(filter, "START_DATE_TIME");
                if (drFind.Length > 0)
                {
                    dtTime = (DateTime)(drFind[0]["START_DATE_TIME"]);

                    addNursingEvent(patientId, visitId, inpVitalCode, inpVitalName, dtTime, ref dsNursingRecord, true);
                }
            }

            // ת���¼�
            filter = "ACTION = 'D'";
            drFind = dsAdtLog.Tables[0].Select(filter, "LOG_DATE_TIME");
            for (int i = 0; i < drFind.Length; i++)
            {
                dtTime = (DateTime)(drFind[0]["LOG_DATE_TIME"]);
                filter = "START_DATE_TIME >= " + SQL.SqlConvert(dtTime.ToString(ComConst.FMT_DATE.LONG));
                DataRow[] drFind2 = dsOrders.Tables[0].Select(filter, "START_DATE_TIME");
                if (drFind2.Length > 0)
                {
                    dtTime = (DateTime)(drFind2[0]["START_DATE_TIME"]);

                    addNursingEvent(patientId, visitId, shiftVitalCode, shiftVitalName, dtTime, ref dsNursingRecord, false);
                }
            }

            if (dsNursingRecord.HasChanges() == true)
            {
                GVars.OracleAccess.Update(ref dsNursingRecord);
            }

            return true;
        }


        /// <summary>
        /// ��ȡ�¼�����
        /// </summary>
        /// <returns></returns>
        private bool getVitalCode()
        {
            if (inpVitalCode.Length > 0 || outpVitalCode.Length > 0 || shiftVitalCode.Length > 0)
            {
                return true;
            }

            // ��ȡ����
            string filter = "PARAMETER_NAME = 'TRANSFER_DEPT_VITAL_CODE'";
            DataRow[] drFind = dsAppConfig.Tables[0].Select(filter);
            if (drFind.Length > 0)
            {
                shiftVitalCode = drFind[0]["PARAMETER_VALUE"].ToString();

                if (chkNursingItemCode(shiftVitalCode, ref shiftVitalName) == false)
                {
                    shiftVitalCode = string.Empty;
                }
            }

            filter = "PARAMETER_NAME = 'INP_VITAL_CODE'";
            drFind = dsAppConfig.Tables[0].Select(filter);
            if (drFind.Length > 0)
            {
                inpVitalCode = drFind[0]["PARAMETER_VALUE"].ToString();

                if (chkNursingItemCode(inpVitalCode, ref inpVitalName) == false)
                {
                    inpVitalCode = string.Empty;
                }
            }

            filter = "PARAMETER_NAME = 'OUTP_VITAL_CODE'";
            drFind = dsAppConfig.Tables[0].Select(filter);
            if (drFind.Length > 0)
            {
                outpVitalCode = drFind[0]["PARAMETER_VALUE"].ToString();

                if (chkNursingItemCode(outpVitalCode, ref outpVitalName) == false)
                {
                    outpVitalCode = string.Empty;
                }
            }

            return true;
        }


        /// <summary>
        /// �������Ƿ���ȷ
        /// </summary>
        /// <returns></returns>
        private bool chkNursingItemCode(string vitalCode, ref string vitalName)
        {
            string filter = "VITAL_CODE = " + SQL.SqlConvert(vitalCode);
            DataRow[] drFind = dsNursingItemDict.Tables[0].Select(filter);
            if (drFind.Length > 0)
            {
                vitalName = drFind[0]["VITAL_SIGNS"].ToString();
                return true;
            }

            return false;
        }


        /// <summary>
        /// ���ӻ����¼�
        /// </summary>
        /// <returns></returns>
        private bool addNursingEvent(string patientId, string visitId, string vitalCode, string vitalSigns, DateTime dtRecord, ref DataSet dsNursingRec, bool onlyOne)
        {
            if (vitalCode.Length == 0) return true;

            DateTime dtBegin = dtRecord.Date.AddHours(dtRecord.Hour - 1);
            DateTime dtEnd = dtRecord.Date.AddHours(dtRecord.Hour + 1);

            // ���Ҹ��¼��Ƿ����
            string filter = "VITAL_CODE = " + SQL.SqlConvert(vitalCode)
                            + "AND TIME_POINT >= " + SQL.SqlConvert(dtBegin.ToString(ComConst.FMT_DATE.LONG))
                            + "AND TIME_POINT <= " + SQL.SqlConvert(dtEnd.ToString(ComConst.FMT_DATE.LONG));
            if (onlyOne == true)
            {
                filter = "VITAL_CODE = " + SQL.SqlConvert(vitalCode);
            }

            DataRow[] drFind = dsNursingRec.Tables[0].Select(filter);
            if (drFind.Length > 0)
            {
                return true;
            }

            // ���������, ����
            DataRow drNew = dsNursingRec.Tables[0].NewRow();
            drNew["PATIENT_ID"] = patientId;
            drNew["VISIT_ID"] = visitId;
            drNew["RECORDING_DATE"] = dtRecord.Date;
            drNew["TIME_POINT"] = dtRecord;
            drNew["VITAL_SIGNS"] = vitalSigns;
            drNew["CLASS_CODE"] = "D";
            drNew["VITAL_CODE"] = vitalCode;
            drNew["WARD_CODE"] = GVars.User.DeptCode;
            drNew["NURSE"] = GVars.User.Name;

            dsNursingRec.Tables[0].Rows.Add(drNew);

            return true;
        }
    }
}
