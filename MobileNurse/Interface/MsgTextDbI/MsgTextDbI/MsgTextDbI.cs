using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class MsgTextDbI
    {
        #region ±‰¡ø
        protected DbAccess _connection;
        #endregion

        public MsgTextDbI(DbAccess conn)
        {
            _connection = conn;
        }


        public DataSet GetMsgDict()
        {
            string sql = "SELECT * FROM MSG_DICT";
            
            return _connection.SelectData(sql, "MSG_DICT");
        }

        
        public bool SaveMsgDict(ref DataSet dsMsg)
        {
            string sql = "SELECT * FROM MSG_DICT";
            
            _connection.Update(ref dsMsg, "MSG_DICT", sql);

            return true;
        }


        public DataSet DownloadMsgDict()
        {
            string sql = "SELECT CONCAT(MSG_TYPE, MSG_ID) MSG_ID, MSG_TEXT MSG_CONTENT FROM MSG_DICT";
            
            return _connection.SelectData(sql, "MSG_DICT");
        }
    }
}
