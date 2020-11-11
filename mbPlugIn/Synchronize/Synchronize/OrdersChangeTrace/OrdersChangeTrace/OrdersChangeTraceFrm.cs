using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using Microsoft.Win32;
using System.IO;

namespace HISPlus
{
    public partial class OrdersChangeTraceFrm : FormDo
    {
        #region 变量

        private ServerCreateSyncInfo tracer;
        private Thread threadTracer;                             // 加载数据的线程
        #endregion

        private OleDbAccess his = null;
        private OracleAccess mobile = null;

        public OrdersChangeTraceFrm()
        {
            InitializeComponent();

            _id = "00036";
            _guid = "4320767B-15A4-4b75-B329-60D4D36B5242";
            tracer = new ServerCreateSyncInfo();
        }

        public OrdersChangeTraceFrm(string path)
        {
            InitializeComponent();

            _id = "00036";
            _guid = "4320767B-15A4-4b75-B329-60D4D36B5242";
            tracer = new ServerCreateSyncInfo();
            tracer.IniPath = path;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrdersChangeTraceFrm_Load(object sender, EventArgs e)
        {
            try
            {
                tracer.WardCode = GVars.User.DeptCode;
                if (Path.GetExtension(tracer.IniPath) == ".sql")
                {
                    threadTracer = new Thread(tracer.SqlTask);
                }
                else
                {
                    threadTracer = new Thread(tracer.Trace);
                }
                
                threadTracer.IsBackground = true;
                //threadTracer.Name = Guid.NewGuid().ToString();  
                threadTracer.Name = "ChangeTrace";
                threadTracer.Start();

                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 窗体退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrdersChangeTraceFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                tracer.Exit();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[刷新]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                txtTrace.Text = tracer.TraceInfo;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;

                //tracer.Trace();                
                btnRefresh_Click(sender, e);
            }
            catch
            {
            }
            finally
            {
                timer1.Enabled = true;
            }
        }


        #region Oracle字符集处理
        private string oldNlslang = string.Empty;


        /// <summary>
        /// 设置字符集
        /// </summary>
        private void setMobileNlsLang()
        {
            // 不同字符集的处理
            string nlsLangKey = GVars.IniFile.ReadString("DATABASE", "ORA_NLS_LANG", string.Empty);

            if (nlsLangKey.Length > 0)
            {
                //mobile.KeepConnection = true;          // 保持数据库连接

                for (int i = 0; i < 6; i++)              // 针对西京医院 要多次连执接才会成功的情况设计
                {
                    try
                    {
                        oldNlslang = oracleNlsLang_Zh(nlsLangKey);
                        Thread.Sleep(100);
                        mobile.SelectValue("SELECT SYSDATE FROM DUAL");
                        return;
                    }
                    catch (Exception ex)
                    {
                        txtTrace.Text +=Environment.NewLine  + ex.Message;

                        Thread.Sleep(30 * 1000);        // 休息30秒
                    }
                    finally
                    {
                        oracleNlsLang_Restore(nlsLangKey, oldNlslang);
                    }
                }

                throw new Exception("设置字符集失败!");
            }
        }


        /// <summary>
        /// 设置字符集
        /// </summary>
        private void setHisNlsLang()
        {
            // 不同字符集的处理
            string nlsLangKey = GVars.IniFile.ReadString("DATABASE", "ORA_NLS_LANG", string.Empty);

            if (nlsLangKey.Length > 0)
            {
                //his.KeepConnection = true;          // 保持数据库连接

                for (int i = 0; i < 6; i++)              // 针对西京医院 要多次连执接才会成功的情况设计
                {
                    try
                    {
                        oldNlslang = oracleNlsLang_En(nlsLangKey);
                        Thread.Sleep(100);
                        his.SelectValue("SELECT SYSDATE FROM DUAL");
                        return;
                    }
                    catch (Exception ex)
                    {
                        txtTrace.Text +=Environment.NewLine  + ex.Message;

                        Thread.Sleep(30 * 1000);        // 休息30秒
                    }
                    finally
                    {
                        oracleNlsLang_Restore(nlsLangKey, oldNlslang);
                    }
                }

                throw new Exception("设置字符集失败!");
            }
        }


        /// <summary>
        /// 修改Oracle字符集为英文
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string oracleNlsLang_En(string key)
        {
            string preValue = Registry.GetValue(key, "NLS_LANG", null).ToString();

            if (preValue.Length > 0)
            {
                Registry.SetValue(key, "NLS_LANG", "AMERICAN_AMERICA.US7ASCII");
            }

            return preValue;
        }


        /// <summary>
        /// 修改Oracle字符集为中文
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string oracleNlsLang_Zh(string key)
        {
            string preValue = Registry.GetValue(key, "NLS_LANG", null).ToString();

            if (preValue.Length > 0)
            {
                Registry.SetValue(key, "NLS_LANG", "SIMPLIFIED CHINESE_CHINA.ZHS16GBK");
            }

            return preValue;
        }


        /// <summary>
        /// 还原Oracle字符集
        /// </summary>
        /// <param name="key"></param>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        private bool oracleNlsLang_Restore(string key, string oldValue)
        {
            if (oldValue.Length == 0)
            {
                return true;
            }

            Registry.SetValue(key, "NLS_LANG", oldValue);

            return true;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (his == null)
                {
                    his = new HISPlus.OleDbAccess();
                    his.ConnectionString = "Provider=msdaora;Data Source=JingXi;User ID=bjtj;Password=ydyh";
                }

                setHisNlsLang();

                DataSet ds = his.SelectData("SELECT DEPT_NAME FROM DEPT_DICT WHERE ROWNUM = 1");
                txtTrace.Text +=Environment.NewLine  + ds.Tables[0].Rows[0]["DEPT_NAME"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (mobile == null)
                {
                    mobile = new HISPlus.OracleAccess();
                    mobile.ConnectionString = GVars.OracleMobile.ConnectionString;
                }

                setMobileNlsLang();

                DataSet ds = mobile.SelectData("SELECT DEPT_NAME FROM DEPT_DICT  WHERE ROWNUM = 1");
                txtTrace.Text += Environment.NewLine + ds.Tables[0].Rows[0]["DEPT_NAME"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
