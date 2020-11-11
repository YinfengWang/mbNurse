//------------------------------------------------------------------------------------
//  ����            : PersonCls.cs
//  ���ܸ�Ҫ        : ��Ȼ����
//  ������          : ����
//  ������          : 2008-04-10
//  �汾            : 1.0.0.0
// 
//------------------< �����ʷ >------------------------------------------------------
//  �������        : 
//  �����          : 
//  �������        : 
//  �汾            : 
//------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace HISPlus
{
    public class PersonCls
    {
        #region ��������
        protected string        _name           = string.Empty;                 // ����
        protected string        _gender         = string.Empty;                 // �Ա�: ��/Ů/δ֪
        protected DateTime      _birthday       = DataType.DateTime_Null();     // ��������

        protected string        _body_Height    = string.Empty;                 // ���
        protected string        _body_Weight    = string.Empty;                 // ����
        #endregion

        public PersonCls()
        {
            _birthday = DataType.DateTime_Null();
        }

        #region ����
        public string Name
        {
            get { return _name;}
            set { _name = value;}
        }


        public string Gender
        {
            get { return _gender;}
            set { _gender = value;}
        }


        public DateTime Birthday
        {
            get { return _birthday;}
            set { _birthday = value;}
        }


        public string Body_Height
        {
            get { return _body_Height;}
            set { _body_Height = value;}
        }


        public string Body_Weight
        {
            get { return _body_Weight;}
            set { _body_Weight = value;}
        }
        #endregion


        #region ����
        /// <summary>
        /// ��ȡ�����ַ���
        /// </summary>
        /// <returns></returns>
        public string GetAge(DateTime dtNow)
        {
            return GetAge(_birthday, dtNow);
        }


        public static string GetAge(DateTime dtBirthday, DateTime dtNow)
        {
            string  strAge      = string.Empty;                         // ������ַ�����ʾ
            int     intYear     = 0;                                    // ��
            int     intMonth    = 0;                                    // ��
            int     intDay      = 0;                                    // ��

            // ���û���趨��������, ���ؿ�
            if (DataType.DateTime_IsNull(ref dtBirthday) == true)
            {
                return string.Empty;
            }
            
            // ��������
            intDay = dtNow.Day - dtBirthday.Day;
            if (intDay < 0)
            {
                dtNow = dtNow.AddMonths(-1);
                intDay += DateTime.DaysInMonth(dtNow.Year, dtNow.Month);
            }
            
            // ��������
            intMonth = dtNow.Month - dtBirthday.Month;
            if (intMonth < 0)
            {
                intMonth += 12;
                dtNow = dtNow.AddYears(-1);
            }
            
            // ��������
            intYear = dtNow.Year - dtBirthday.Year;
            
            // ��ʽ���������
            if (intYear >= 1)                                            // ������
            {
                strAge = intYear.ToString() + "��";
            }
            
            if (intMonth > 0 && intYear <= 5)                           // �������¿����������
            {
                strAge += intMonth.ToString() + "��";
            }
            
            if (intDay >= 0 && intYear < 1)                              // һ�����¿����������
            {
                if (strAge.Length == 0 || intDay > 0)
                {
                    strAge += intDay.ToString() + "��";
                }
            }
            
            return strAge;
        }
        #endregion
    }
}
