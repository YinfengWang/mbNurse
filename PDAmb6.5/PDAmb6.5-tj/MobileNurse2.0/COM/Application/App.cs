//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ҽ������վ
//  ��ϵͳ����      : ͨ��ģ��
//  ��������        : 
//  ����            : UniversalVarApp.cs
//  ���ܸ�Ҫ        : Ӧ�ó�������
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
using System.Data;
using System.Diagnostics;

namespace HISPlus
{
	/// <summary>
	/// UniversalVarApp ��ժҪ˵����
	/// </summary>
	public class App
    {
        protected string    _name           = string.Empty;             // Ӧ�ó��������
        protected string    _title          = string.Empty;             // Ӧ�ó���ı���
        protected string    _copyRight      = string.Empty;             // ��Ȩ
        protected string    _version        = string.Empty;             // �汾
        
        protected DateTime  _dtNow          = DataType.DateTime_Null();

        protected string    _right          = string.Empty;             // Ȩ��
        protected bool      _verified       = false;                    // ͨ����֤
        
        protected bool      _questionExit   = true;                     // �˳�ϵͳǰ�Ƿ�ѯ��
        
        protected bool      _userInput      = true;                     // �Ƿ������������Ϊ�û�����
        
        public App()
		{
        }


        #region ����
        public string Name
        {
            get { return _name;}
            set { _name = value;}
        }


        public string Title
        {
            get { return _title;}
            set { _title = value;}
        }


        public string CopyRight
        {
            get { return _copyRight;}
            set { _copyRight = value;}
        }


        public string Version
        {
            get { return _version;}
            set { _version = value;}
        }


        public DateTime Now
        {
            get { return _dtNow;}
            set { _dtNow = value;}
        }


        public string Right
        {
            get { return _right;}
            set { _right = value;}
        }


        public bool Verified
        {
            get { return _verified;}
            set { _verified = value;}
        }


        public bool QuestionExit
        {
            get { return _questionExit;}
            set { _questionExit = value;}
        }


        public bool UserInput
        {
            get { return _userInput;}
            set { _userInput = value;}
        }
        #endregion
    }
}
