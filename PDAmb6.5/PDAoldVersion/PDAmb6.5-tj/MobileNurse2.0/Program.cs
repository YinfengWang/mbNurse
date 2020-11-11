using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Threading;

namespace HISPlus
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [MTAThread]
        static void Main()
        {
            try
            {
                // 设置应用程序属性
                setAppProperty();
                
                // 加载消息文本
                MsgDbI msgDbI = new MsgDbI();
                GVars.Msg.Caption = GVars.App.Title;
                msgDbI.LoadMsg(ref GVars.Msg);
                
                // 获取本地设置
                GVars.App.LoadLocalSetting();
                
                // 加载主窗体
                Application.Run(new MainFrm());
            }
            catch
            {
                
            }
        }
        
        
        /// <summary>
        /// 设置应用程序属性
        /// </summary>
        static private bool setAppProperty()
        {
            // 应用程序设置
            GVars.App.Title             = "移动护理";
            GVars.App.Name              = "MOBILE_NURSING";
            GVars.App.Version           = "版本 1.0";
            GVars.App.CopyRight         = "Copyright(c) 2014 陕西天健源达信息科技有限公司。All Rights Reserved";
            GVars.App.QuestionExit      = true;
            
            GVars.App.Right             = "002";
            
            return true;
        }        
    }
}