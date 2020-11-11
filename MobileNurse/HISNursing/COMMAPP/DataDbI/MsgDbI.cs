//------------------------------------------------------------------------------------
//
//  系统名称        : 医生工作站
//  子系统名称      : 通用模块
//  对象类型        : 
//  类名            : UniversalVarApp.cs
//  功能概要        : 应用程序配置
//  作成者          : 付军
//  作成日          : 2007-01-19
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------
using System;
using System.Data;

namespace HISPlus
{
	/// <summary>
	/// UniversalVarApp 的摘要说明。
	/// </summary>
	public class MsgDbI
	{
        protected DbAccess  _connection;


        public MsgDbI(DbAccess connection)
		{
            _connection = connection;
		}
        
        
        /// <summary>
        /// 加载默认消息
        /// </summary>
        public void LoadMsg_Default(ref Message msg)
        {
            // 警告消息
            msg.AddMsg("WD001", "{0} 已经启动!");
            
            // 错误消息
            msg.AddMsg("ED001", "找不到初始化文件。请确认{0}位于路径下");
            msg.AddMsg("ED002", "摆药药局为空，请重新设置！");
            msg.AddMsg("ED003", "{0}文件中没有找到数据库连接字符串！");
            msg.AddMsg("ED004", "连接字符串解密失败！");
            msg.AddMsg("ED005", "系统错误");
            msg.AddMsg("ED006", "您还没有使用本系统的权限!");
            msg.AddMsg("ED008", "该项目已经有护理记录了,不能删除!");
            
            // 提示消息
            msg.AddMsg("ID001", "用户名为空。" + ComConst.STR.CRLF + "请输入用户名后点取确认按钮。");
            msg.AddMsg("ID002", "口令为空。" + ComConst.STR.CRLF + "请输入口令后点取确认按钮。");
            msg.AddMsg("ID003", "错误的用户名/密码。请重新输入！");
            msg.AddMsg("ID004", "三次登录失败，退出登录");
            msg.AddMsg("ID005", "登录系统");
            msg.AddMsg("ID006", "登录失败");
            msg.AddMsg("ID007", "正常退出");
            msg.AddMsg("ID008", "异常退出");
            
            msg.AddMsg("ID012", "请选择护理单元!");
        }
        
        
        /// <summary>
        /// 加载保存在数据库中的消息
        /// </summary>
        public void LoadMsg_Db(ref Message msg)
        {
            DataSet ds = _connection.SelectData("SELECT * FROM SYSTEM_TEXT");

            // 组织数据
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow drRow in ds.Tables[0].Rows)
                {
                    msg.AddMsg(drRow["TEXT_ID"].ToString(), drRow["TEXT_CONTENT"].ToString());
                }
            }
        }
	}
}
