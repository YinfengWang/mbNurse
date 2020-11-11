//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ͨ��ģ��
//  ��ϵͳ����      : 
//  ��������        : 
//  ����            : Error.cs
//  ���ܸ�Ҫ        : ������
//  ������          : ����
//  ������          : 2007-01-19
//  �汾            : 1.0.0.0
// 
//------------------< �����ʷ >------------------------------------------------------
//  �������        :
//  �����          :
//  �������        :
//  �汾            :
//------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Windows.Forms;

namespace HISPlus
{
	/// <summary>
	/// Error ��ժҪ˵����
	/// </summary>
	public class Message
	{	
	    public static readonly int      MSG_FLG_LEN				= 1;						// ��ϢID��, ��ʶ��Ϣ���͵��ַ�����
		public const string	            MSG_ID_PREFIX_ERROR		= "E";						// �������͵���ϢID��"E"��ͷ
		public const string	            MSG_ID_PREFIX_WARNING	= "W";						// �������͵���ϢID��"W"��ͷ
		public const string	            MSG_ID_PREFIX_INFO		= "I";						// ��ʾ���͵���ϢID��"I"��ͷ
		public const string	            MSG_ID_PREFIX_QUESTION	= "Q";						// ѯ�����͵���ϢID��"Q"��ͷ

		public ArrayList                MsgContent		        = new ArrayList();          // ��Ҫ��������
		
        protected Hashtable             _msgDict                = new Hashtable();		    // �ı��ֵ�
        protected string                caption                 = string.Empty;             // ���ڱ���
		protected string                msgId                   = string.Empty;             // ����ID
        protected Control               objSrc                  = null;                     // ����Դ
		

        /// <summary>
        /// ���캯��
        /// </summary>
        public Message()
        {
        }
        
		
		/// <summary>
		/// ��������
		/// </summary>
        ~Message()
        {
            MsgContent.Clear();
            MsgContent = null;
        }

		
		#region ����
		/// <summary>
		/// ���� [MsgId]
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
		/// ���� [����Դ]
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
        /// ���ڱ���
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


        #region �ӿ�
        public void AddMsg(object key, object value)
        {
            _msgDict.Add(key, value);
        }

        
        /// <summary>
        /// ��ȡ��Ϣ�ı�
        /// </summary>
        /// <returns>��Ϣ�ı�</returns>
        public string GetMsg()
        {
            // ��������
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

            // ���msgId�Ƿ����
            if (_msgDict == null || _msgDict.Contains(msgId) == false)
            {
                return msgId;
            }

            // �����������
			string		msg			= _msgDict[msgId].ToString();		// ��Ϣ�ı�
			string[]	astrParams  = new string[MsgContent.Count];     // ��Ϣ����Ҫ�Ĳ�������
		
			// ������Ϣ����
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
        /// ��ȡ��Ϣ�ı�
        /// </summary>
        /// <param name="msgId">��ϢID</param>
        /// <returns>��Ϣ�ı�</returns>
        public string GetMsg(string msgId)
        { 
            // ��������
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

            // ���msgId�Ƿ����
            if (_msgDict == null || _msgDict.Contains(msgId) == false)
            {
                return msgId;
            }
            
            return _msgDict[msgId].ToString();		// ��Ϣ�ı�
        }

        
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        /// <returns>ȷ�Ͻ��</returns>
        public DialogResult Show()
        {
            string          strMsg = GetMsg();
            DialogResult    Result;                                                             // ���
                        
            // ȷ����Ϣ����ʽ, ����ʾ��Ϣ
            string preFix = msgId.Substring(0, Message.MSG_FLG_LEN);
            
            Clear();                                                                            // ��ֹ�����ʾ��Ϣ
            
            switch(preFix)
            {
                // ������Ϣ
                case Message.MSG_ID_PREFIX_ERROR:
                    Result = MessageBox.Show(strMsg, caption);
                    break;
                
                // ������Ϣ
                case Message.MSG_ID_PREFIX_WARNING:
                    Result = MessageBox.Show(strMsg, caption);
                    break;

                // ��ʾ��Ϣ
                case Message.MSG_ID_PREFIX_INFO:
                    Result = MessageBox.Show(strMsg, caption);
                    break;
                    
                // ѯ����Ϣ
                case Message.MSG_ID_PREFIX_QUESTION:
                    Result = MessageBox.Show(strMsg, caption, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    break;
                    
                // ����
                default:
                    Result = DialogResult.None;
                    break;
            }
			            
            // �ѽ�����ڴ�������λ��
            if (objSrc != null)
            {
                objSrc.Focus();
            }
            
            // �����û���ȷ�Ͻ��
            return Result;
        }
		
		
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="strMsgId">��ϢID</param>
        /// <returns>ȷ�Ͻ��</returns>
        public DialogResult Show(string msgId)
        {
            this.msgId = msgId;
			
            return Show();
        }

        
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="strMsgId">��ϢID</param>
        /// <param name="strMsgContent">��Ϣ�滻����</param>
        /// <returns>ȷ�Ͻ��</returns>
        public DialogResult Show(string msgId, string msgContent)
        {
            this.msgId = msgId;
            MsgContent.Add(msgContent);
            
            return Show();
        }

		
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="strMsgId">��ϢID</param>
        /// <param name="strMsgContent1">��Ϣ�滻����1</param>
        /// <param name="strMsgContent2">��Ϣ�滻����2</param>
        /// <returns>ȷ�Ͻ��</returns>
        public DialogResult Show(string msgId, string msgContent1, string msgContent2)
        {
            this.msgId = msgId;
            MsgContent.Add(msgContent1);
            MsgContent.Add(msgContent2);
            
            return Show();
        }
        #endregion


        #region ��ͨ����
        /// <summary>
        /// �������, ȷ������ֻ��ʾһ��
        /// </summary>
        public void Clear()
        {
            msgId = string.Empty;
            MsgContent.Clear();
        }


        /// <summary>
        /// ����Ƿ�����ϢIDǰ׺
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsPrefixChar(string id)
        {
            switch (id)
            {
                // ������Ϣ
                case Message.MSG_ID_PREFIX_ERROR:
                    return true;

                // ������Ϣ
                case Message.MSG_ID_PREFIX_WARNING:
                    return true;

                // ��ʾ��Ϣ
                case Message.MSG_ID_PREFIX_INFO:
                    return true;

                // ѯ����Ϣ
                case Message.MSG_ID_PREFIX_QUESTION:
                    return true;

                default:
                    return false;
            }
                    
        }
        #endregion
    }
}
