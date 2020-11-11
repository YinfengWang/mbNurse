//------------------------------------------------------------------------------------
//
//  系统名称        : 通用模块
//  子系统名称      : 
//  对象类型        : 
//  类名            : Error.cs
//  功能概要        : 错误处理
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
using System.Collections;
using System.Windows.Forms;

namespace HISPlus
{
	/// <summary>
	/// Error 的摘要说明。
	/// </summary>
	public class Message
	{	
	    public static readonly int      MSG_FLG_LEN				= 1;						// 消息ID中, 标识消息类型的字符长度
		public const string	            MSG_ID_PREFIX_ERROR		= "E";						// 错误类型的消息ID以"E"开头
		public const string	            MSG_ID_PREFIX_WARNING	= "W";						// 警告类型的消息ID以"W"开头
		public const string	            MSG_ID_PREFIX_INFO		= "I";						// 提示类型的消息ID以"I"开头
		public const string	            MSG_ID_PREFIX_QUESTION	= "Q";						// 询问类型的消息ID以"Q"开头

		public ArrayList                MsgContent		        = new ArrayList();          // 需要填充的内容
		
        protected Hashtable             _msgDict                = new Hashtable();		    // 文本字典
        protected string                caption                 = string.Empty;             // 窗口标题
		protected string                msgId                   = string.Empty;             // 错误ID
        protected Control               objSrc                  = null;                     // 错误源
		

        /// <summary>
        /// 构造函数
        /// </summary>
        public Message()
        {
        }
        
		
		/// <summary>
		/// 析构函数
		/// </summary>
        ~Message()
        {
            MsgContent.Clear();
            MsgContent = null;
        }

		
		#region 属性
		/// <summary>
		/// 属性 [MsgId]
		/// </summary>
		public string MsgId
		{
			get
			{
				return msgId;
			}
			set
			{
				MsgContent.Clear();
				msgId = value;
			}
		}


		/// <summary>
		/// 属性 [错误源]
		/// </summary>
		public Control ErrorSrc
		{
			get
			{
				return objSrc;
			}
			set
			{
				objSrc = value;
			}
		}


        /// <summary>
        /// 窗口标题
        /// </summary>
        public string Caption
        {
            get
            {
                return caption;
            }
            set
            {
                caption = value;
            }
        }
		#endregion


        #region 接口
        public void AddMsg(object key, object value)
        {
            _msgDict.Add(key, value);
        }

        
        /// <summary>
        /// 获取消息文本
        /// </summary>
        /// <returns>消息文本</returns>
        public string GetMsg()
        {
            // 特例处理
            if (IsPrefixChar(msgId) == true)
            {
                if (MsgContent.Count > 0)
                {
                    return MsgContent[0].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }

            // 检查msgId是否存在
            if (_msgDict == null || _msgDict.Contains(msgId) == false)
            {
                return msgId;
            }

            // 正常情况处理
			string		msg			= _msgDict[msgId].ToString();		// 消息文本
			string[]	astrParams  = new string[MsgContent.Count];     // 消息中需要的参数数组
		
			// 生成消息内容
			for (int i = 0; i < MsgContent.Count; i++)
			{
				astrParams[i] = (string)MsgContent[i];
			}
        
			if (MsgContent.Count > 0) 
			{
				msg = string.Format(msg, astrParams);
			}

            return msg;
        }


        /// <summary>
        /// 获取消息文本
        /// </summary>
        /// <param name="msgId">消息ID</param>
        /// <returns>消息文本</returns>
        public string GetMsg(string msgId)
        { 
            // 特例处理
            if (IsPrefixChar(msgId) == true)
            {
                if (MsgContent.Count > 0)
                {
                    return MsgContent[0].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }

            // 检查msgId是否存在
            if (_msgDict == null || _msgDict.Contains(msgId) == false)
            {
                return msgId;
            }
            
            return _msgDict[msgId].ToString();		// 消息文本
        }

        
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <returns>确认结果</returns>
        public DialogResult Show()
        {
            string          strMsg = GetMsg();
            DialogResult    Result;                                                             // 结果
                        
            // 确定消息框样式, 并显示消息
            string preFix = msgId.Substring(0, Message.MSG_FLG_LEN);
            
            Clear();                                                                            // 防止多次显示消息
            
            switch(preFix)
            {
                // 错误消息
                case Message.MSG_ID_PREFIX_ERROR:
                    Result = MessageBox.Show(strMsg, caption);
                    break;
                
                // 警告消息
                case Message.MSG_ID_PREFIX_WARNING:
                    Result = MessageBox.Show(strMsg, caption);
                    break;

                // 提示消息
                case Message.MSG_ID_PREFIX_INFO:
                    Result = MessageBox.Show(strMsg, caption);
                    break;
                    
                // 询问消息
                case Message.MSG_ID_PREFIX_QUESTION:
                    Result = MessageBox.Show(strMsg, caption, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    break;
                    
                // 其它
                default:
                    Result = DialogResult.None;
                    break;
            }
			            
            // 把焦点放在错误发生的位置
            if (objSrc != null)
            {
                objSrc.Focus();
            }
            
            // 返回用户的确认结果
            return Result;
        }
		
		
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="strMsgId">消息ID</param>
        /// <returns>确认结果</returns>
        public DialogResult Show(string msgId)
        {
            this.msgId = msgId;
			
            return Show();
        }

        
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="strMsgId">消息ID</param>
        /// <param name="strMsgContent">消息替换内容</param>
        /// <returns>确认结果</returns>
        public DialogResult Show(string msgId, string msgContent)
        {
            this.msgId = msgId;
            MsgContent.Add(msgContent);
            
            return Show();
        }

		
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="strMsgId">消息ID</param>
        /// <param name="strMsgContent1">消息替换内容1</param>
        /// <param name="strMsgContent2">消息替换内容2</param>
        /// <returns>确认结果</returns>
        public DialogResult Show(string msgId, string msgContent1, string msgContent2)
        {
            this.msgId = msgId;
            MsgContent.Add(msgContent1);
            MsgContent.Add(msgContent2);
            
            return Show();
        }
        #endregion


        #region 共通函数
        /// <summary>
        /// 清除错误, 确保错误只显示一次
        /// </summary>
        public void Clear()
        {
            msgId = string.Empty;
            MsgContent.Clear();
        }


        /// <summary>
        /// 检查是否是消息ID前缀
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsPrefixChar(string id)
        {
            switch (id)
            {
                // 错误消息
                case Message.MSG_ID_PREFIX_ERROR:
                    return true;

                // 警告消息
                case Message.MSG_ID_PREFIX_WARNING:
                    return true;

                // 提示消息
                case Message.MSG_ID_PREFIX_INFO:
                    return true;

                // 询问消息
                case Message.MSG_ID_PREFIX_QUESTION:
                    return true;

                default:
                    return false;
            }
                    
        }
        #endregion
    }
}
