using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HISPlus
{
    public partial class SettingFrm : Form
    {

        public bool Saved = false;              // 配置文件保存


        public SettingFrm()
        {
            InitializeComponent();
        }

        
        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                save();
                
                Saved = true;

                this.Close();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        private void SettingFrm_Load(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;

                Saved = false;

                initDisp();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }


        private void initDisp()
        {
            txtServerIp.Text = string.Empty;
            txtWardCode.Text = string.Empty;
            lblVersion.Text  = string.Empty;
            txtVersion.Text  = string.Empty;
            
            // 获取值
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
            string configFile = Path.Combine(appPath, "Config.xml");
            
            DataSet dsConfig = new DataSet("Config");
            
            if (System.IO.File.Exists(configFile) == false)
            {
                // 创建表
                DataTable dt = new DataTable("Config");
                DataColumn dc = new DataColumn("SERVER_IP", System.Type.GetType("System.String"));
                dt.Columns.Add(dc);
                
                dc = new DataColumn("WARD_CODE", System.Type.GetType("System.String"));
                dt.Columns.Add(dc);
                
                dsConfig.Tables.Add(dt);
                
                // 向表中增加数据
                DataRow dr = dt.NewRow();
                dr["SERVER_IP"] = string.Empty;
                dr["WARD_CODE"] = string.Empty;
                
                dt.Rows.Add(dr);
                
                // 保存到本地
                dsConfig.WriteXml(configFile, XmlWriteMode.WriteSchema);
            }
            else
            {
                dsConfig.ReadXml(configFile, XmlReadMode.ReadSchema);
            }

            if (dsConfig == null || dsConfig.Tables.Count == 0 || dsConfig.Tables[0].Rows.Count == 0)
            {
                return;
            }
            else
            {
                DataRow dr = dsConfig.Tables[0].Rows[0];

                txtServerIp.Text = dr["SERVER_IP"].ToString();
                txtWardCode.Text = dr["WARD_CODE"].ToString();
                
                if (dsConfig.Tables[0].Columns.Contains("VERSION") == true)
                {
                    lblVersion.Text = dr["VERSION"].ToString();
                    txtVersion.Text = dr["VERSION"].ToString();
                }
            }
        }
        
        
        private void save()
        {
            // 获取值
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
            string configFile = Path.Combine(appPath, "Config.xml");
            
            DataSet dsConfig = new DataSet("Config");
            DataRow dr       = null;

            if (System.IO.File.Exists(configFile) == false)
            {
                // 创建表
                DataTable dt = new DataTable("Config");
                DataColumn dc = new DataColumn("SERVER_IP", System.Type.GetType("System.String"));
                dt.Columns.Add(dc);
                
                dc = new DataColumn("WARD_CODE", System.Type.GetType("System.String"));
                dt.Columns.Add(dc);

                dc = new DataColumn("VERSION", System.Type.GetType("System.String"));
                dt.Columns.Add(dc);
                
                dsConfig.Tables.Add(dt);
                
                // 向表中增加数据
                dr = dt.NewRow();
                
                dr["SERVER_IP"] = txtServerIp.Text.Trim();
                dr["WARD_CODE"] = txtWardCode.Text.Trim();
                dr["VERSION"]   = txtVersion.Text.Trim();
                
                dt.Rows.Add(dr);
            }
            else
            {
                dsConfig.ReadXml(configFile, XmlReadMode.ReadSchema);

                dr = dsConfig.Tables[0].Rows[0];
                
                dr["SERVER_IP"] = txtServerIp.Text.Trim();
                dr["WARD_CODE"] = txtWardCode.Text.Trim();
                dr["VERSION"]   = txtVersion.Text.Trim();
            }

            dsConfig.WriteXml(configFile, XmlWriteMode.WriteSchema);
        }
        
        
        /// <summary>
        /// 菜单 [返回]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        private void txtServerIp_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                btnSet.Enabled = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[清除本地数据]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearLocalDb_Click(object sender, EventArgs e)
        {
            try
            {
                GVars.Msg.MsgId = "Q00004";
                GVars.Msg.MsgContent.Add(btnClearLocalDb.Text);
                
                if (GVars.Msg.Show() == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    
                    clearLocalDb();
                    
                    GVars.Msg.Show("I00004", btnClearLocalDb.Text);   // ,"{0}成功!");
                }
            }
            catch(Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// 清除本地数据
        /// </summary>
        /// <returns></returns>
        private bool clearLocalDb()
        {
            DataSet dsTables = GVars.sqlceLocal.SelectData("SELECT * FROM PDA_TABLE_LIST");
            
            string sql = string.Empty;
            
            foreach(DataRow dr in dsTables.Tables[0].Rows)
            {
                if (dr["TABLE_NAME"].ToString().Equals("PDA_TABLE_LIST") == true)
                {
                    continue;
                }
                
                sql = "DELETE FROM " + dr["TABLE_NAME"].ToString();
                GVars.sqlceLocal.ExecuteNoQuery(sql);
                
                if (dr["SYNC_MODE"].ToString().Equals("2") == true)
                {
                    sql = "DELETE FROM " + dr["TABLE_NAME"].ToString() + "_TOMBSTONE";
                    GVars.sqlceLocal.ExecuteNoQuery(sql);
                }
            }
            
            sql = "UPDATE PDA_TABLE_LIST SET LAST_DOWN_SCN = NULL";
            GVars.sqlceLocal.ExecuteNoQuery(sql);
            
            return true;
        }

        
        /// <summary>
        /// 按钮[更新本地表结构]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdDbStruct_Click(object sender, EventArgs e)
        {
            try
            {
                GVars.Msg.MsgId = "Q00004";
                GVars.Msg.MsgContent.Add(btnUpdDbStruct.Text);
                
                if (GVars.Msg.Show() == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    
                    GVars.sqlceLocal.CheckDatabase();
                    
                    GVars.Msg.Show("I00004", btnUpdDbStruct.Text);   // ,"{0}成功!");
                }
            }
            catch(Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}