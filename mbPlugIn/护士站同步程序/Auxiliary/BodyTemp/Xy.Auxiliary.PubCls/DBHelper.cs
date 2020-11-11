using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Collections;
using System.IO;
using System.Data.SqlClient;

namespace Xy.Auxiliary.PubCls
{
    public class DBHelper
    {
        //数据库地址
        /// <summary>
        /// Mobile连接字符串
        /// </summary>
        public string conStrMobile = new HISPlus.EnDecrypt().Decrypt(System.Configuration.ConfigurationManager.AppSettings["StrConnMobile"].ToString());

        /// <summary>
        /// His连接字符串
        /// </summary>
        public string connStrHis = new HISPlus.EnDecrypt().Decrypt(System.Configuration.ConfigurationManager.AppSettings["StrConnHis"].ToString());

        /// <summary>
        /// The mysql configuration
        /// </summary>
        private OracleConnection mysqlCon;

        public DBHelper(string strFlag)
        {
            if (strFlag == ConnFlag.mobile.ToString())
            {
                //连接Mobile库
                mysqlCon = new OracleConnection(conStrMobile);
            }
            else if (strFlag == ConnFlag.his.ToString())
            {
                //连接Hid库
                mysqlCon = new OracleConnection(connStrHis);
            }
            else
            {
                //默认连接Mobile库
                mysqlCon = new OracleConnection(conStrMobile);
            }
        }

        /// <summary>
        /// OracleCommand
        /// </summary>
        private OracleCommand mysqlcmd;

        /// <summary>
        /// OracleTransaction
        /// </summary>
        private OracleTransaction mysqltran;

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="mysql">The mysql.</param>
        /// <returns>DataTable.</returns>
        public DataTable GetData(string mysql)
        {
            OracleDataAdapter msda = new OracleDataAdapter(mysql, mysqlCon);
            DataTable dt = new DataTable();
            if (dt != null)
            {
                if (mysqlCon.State == ConnectionState.Closed)
                {
                    mysqlCon.Open();
                }
                msda.Fill(dt); return dt;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据mysql语句，查询数据库，返回一个整型的值
        /// </summary>
        /// <param name="mysql">访问数据库的语句</param>
        /// <returns>整型的值</returns>
        public int ExecuteNonQuery(string mysql)
        {
            //try catach finally检测数据库的连接是否打开，如果状态是关闭，则需要打开，
            //在跟数据库交互完之后需要及时关闭
            try
            {
                if (mysqlCon.State == ConnectionState.Closed)
                {
                    mysqlCon.Open();
                }
                mysqlcmd = new OracleCommand(mysql, mysqlCon);
                int res = mysqlcmd.ExecuteNonQuery();
                mysqlCon.Close();
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                mysqlCon.Close();
            }
        }

        /// <summary>
        /// 根据mysql，查询数据库，返回object的一行一列的值
        /// </summary>
        /// <param name="mysql">连接数据库的语句</param>
        /// <returns>object类型的值</returns>
        public object ExecuteScalar(string mysql)
        {
            //try catach finally检测数据库的连接是否打开，如果状态是关闭，则需要打开，
            //在跟数据库交互完之后需要及时关闭
            try
            {
                if (mysqlCon.State == ConnectionState.Closed)
                {
                    mysqlCon.Open();
                }
                mysqlcmd = new OracleCommand(mysql, mysqlCon);
                object o = null;
                if (mysqlcmd.ExecuteScalar() != System.DBNull.Value)
                {
                    o = mysqlcmd.ExecuteScalar();
                }
                mysqlCon.Close();
                return o;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                mysqlCon.Close();
            }
        }

        /// <summary>
        /// Transactions the specified array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Transaction(ArrayList array)
        {
            if (mysqlCon.State == ConnectionState.Closed)
            {
                mysqlCon.Open();
            }
            //开始事务处理。
            mysqltran = mysqlCon.BeginTransaction();
            mysqlcmd = new OracleCommand();
            mysqlcmd.Connection = mysqlCon;
            mysqlcmd.Transaction = mysqltran;
            try
            {
                foreach (string arr in array)
                {
                    mysqlcmd.CommandText = arr.ToString();
                    mysqlcmd.ExecuteNonQuery();
                }
                mysqltran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                //如果不成功，则回滚，即之前的操作均无效。
                mysqltran.Rollback();
                return false;
            }
            finally
            {
                mysqlCon.Close();
            }
        }

        /// <summary>
        /// 插入新文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="_version">版本号</param>
        /// <returns></returns>
        public bool InsertFile(string filePath, string fileName, string _version)
        {
            bool _isInsFlag = false;

            //将需要存储的图片读取为数据流
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            Byte[] btye2 = new byte[fs.Length];
            fs.Read(btye2, 0, Convert.ToInt32(fs.Length));
            fs.Close();

            //string sqlconnstr = conStrMobile;
            using (OracleConnection conn = new OracleConnection(new DBHelper(ConnFlag.mobile.ToString()).conStrMobile))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO BODYTEMP_AUTOUPDATE (FILENAME, VERSION, UPDATETIME, FILEVALUE) VALUES(:FILENAME, :VERSION, :UPDATETIME, :FILEVALUE)";

                OracleParameter parfileName = new OracleParameter(":FILENAME", OracleType.VarChar, 500);
                parfileName.Value = fileName;
                cmd.Parameters.Add(parfileName);

                OracleParameter parVersion = new OracleParameter(":VERSION", OracleType.VarChar, 3);
                parVersion.Value = _version;
                cmd.Parameters.Add(parVersion);

                OracleParameter parUpdateTime = new OracleParameter(":UPDATETIME", OracleType.VarChar, 50);
                parUpdateTime.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(parUpdateTime);

                OracleParameter parFilevalue = new OracleParameter(":FILEVALUE", OracleType.Blob, btye2.Length);
                parFilevalue.Value = btye2;
                cmd.Parameters.Add(parFilevalue);

                int t = (int)(cmd.ExecuteNonQuery());
                if (t > 0)
                {
                    _isInsFlag = true;
                }
                else
                {
                    _isInsFlag = false;
                }
                conn.Close();
            }
            return _isInsFlag;
        }

        /// <summary>
        /// 更新旧文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="_version">版本号</param>
        /// <returns></returns>
        public bool UpdateFile(string filePath, string fileName, string _version)
        {
            bool _isUpdFlag = false;

            //将需要存储的图片读取为数据流
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            Byte[] btye2 = new byte[fs.Length];
            fs.Read(btye2, 0, Convert.ToInt32(fs.Length));
            fs.Close();

            //string sqlconnstr = conStrMobile;
            using (OracleConnection conn = new OracleConnection(new DBHelper(ConnFlag.mobile.ToString()).conStrMobile))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE BODYTEMP_AUTOUPDATE SET VERSION = :VERSION,FILEVALUE = :FILEVALUE,UPDATETIME = :UPDATETIME WHERE FILENAME = :FILENAME";

                OracleParameter parfileName = new OracleParameter(":FILENAME", OracleType.VarChar, 500);
                parfileName.Value = fileName;
                cmd.Parameters.Add(parfileName);

                OracleParameter parVersion = new OracleParameter(":VERSION", OracleType.VarChar, 3);
                parVersion.Value = _version;
                cmd.Parameters.Add(parVersion);

                OracleParameter parUpdateTime = new OracleParameter(":UPDATETIME", OracleType.VarChar, 50);
                parUpdateTime.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(parUpdateTime);

                OracleParameter parFilevalue = new OracleParameter(":FILEVALUE", OracleType.Blob, btye2.Length);
                parFilevalue.Value = btye2;
                cmd.Parameters.Add(parFilevalue);

                int t = (int)(cmd.ExecuteNonQuery());
                if (t > 0)
                {
                    _isUpdFlag = true;
                }
                else
                {
                    _isUpdFlag = false;
                }
                conn.Close();
            }
            return _isUpdFlag;
        }

        /// <summary>
        /// 将数据库文件流转化为文件
        /// </summary>
        /// <param name="saveFilePath"></param>
        public void SaveByteToFlie(string saveFilePath, string fileName)
        {
            byte[] MyData = new byte[0];
            using (OracleConnection conn = new OracleConnection(new DBHelper(ConnFlag.mobile.ToString()).conStrMobile))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT FILEVALUE FROM BODYTEMP_AUTOUPDATE WHERE FILENAME='" + fileName + "'";
                OracleDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                MyData = (byte[])sdr["FILEVALUE"];//读取第一个图片的位流
                int ArraySize = MyData.GetUpperBound(0);//获得数据库中存储的位流数组的维度上限，用作读取流的上限
                FileStream fs = new FileStream(saveFilePath + "\\" + fileName, FileMode.Create, FileAccess.Write);
                fs.Write(MyData, 0, ArraySize);
                sdr.Close();
                fs.Close();   //--               
                conn.Close();//查看硬盘上的文件           

                
            }
        }
    }

    /// <summary>
    /// 连接数据库选项
    /// </summary>
    public enum ConnFlag
    {
        mobile,//mobile库
        his    //his库
    }
}
