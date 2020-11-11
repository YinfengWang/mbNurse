//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ҽԺ��Ϣϵͳ
//  ��ϵͳ����      : ͨ��ģ��
//  ��������        : 
//  ����            : ComFunction.cs
//  ���ܸ�Ҫ        : ��ͨ����
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
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using Path	= System.IO.Path;
using Dir	= System.IO.Directory;
using Env	= System.Environment;

namespace HISPlus
{    
	/// <summary>
	/// ComFunctions ��ժҪ˵����
	/// </summary>
	public class DataType
	{
		public DataType()
		{
		}
        

        #region ��������
        /// <summary>
        /// ��ȡDateTime��Nullֵ
        /// </summary>
        /// <remarks>�ٶ�һ��"1-1-1 12:00:00"ֵ��ΪNullֵ</remarks>
        /// <returns>DateTime</returns>
        static public DateTime DateTime_Null()
        {
            return new DateTime(1700, 1, 1);
        }
        
        
        /// <summary>
        /// �ж�DateTime�Ƿ�ΪNull
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <returns>TRUE: ��ΪNull; FALSE: ��ΪNull</returns>
        static public bool DateTime_IsNull(ref DateTime dt)
        {
            return DateTime_Null().Date.Equals(dt.Date);
        }


        /// <summary>
        /// ���ַ���ת����Ϊ����
        /// </summary>
        /// <param name="strDateTime">�����ַ���</param>
        /// <returns>����</returns>
        static public DateTime DateTimeFromString(string strDateTime)
        {
            if (strDateTime.Trim().Length == 0)
            {
                return DateTime_Null();
            }
            else
            {
                return DateTime.Parse(strDateTime);
            }
        }


        /// <summary>
        /// ��һ���ַ����о�����ȡ����ʱ���ַ���
        /// </summary>
        /// <remarks>
        ///  1: ������ʱ������Ա�����������ַ��ָ�
        ///  2: ������ʱ������Բ����κηָ����ָ�,��ȡʱ������λ������, ����������λ����
        ///  3: 1��2��������Ľ����Ҳ����ʶ��, �� 200012-2 20:12-43, ������ȷʶ��
        /// </remarks>
        /// <param name="dtSrc">����ʱ���ַ���</param>
        /// <returns>TRUE: �ɹ�; FALSE: ʧ��</returns>
        static public bool GetDateTime(ref string dtSrc)
        {
            string strChar   = string.Empty;
            string strSect   = string.Empty;
            string strSep    = string.Empty;
            int    pos       = 0;
            int    val       = 0;
            int    year      = 0;
            int    month     = 0;
            int    maxDays   = 0;
            bool   blnNum    = false;
            int    len       = 4;
            
            string[] arrNum  = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            string[] arrPart = {"20", "00", "00", "00", "00", "00"};
            
            // �ַ������
            dtSrc += " ";                       // " "Ϊ����������ַ�, Ŀ�����ڷ�����µĵ�forѭ�����Դ����������ַ���
            
            for(int i = 0; i < dtSrc.Length; i++)
            {
                strChar = dtSrc.Substring(i, 1);
                
                // �ж��Ƿ�������
                blnNum = false;
                for(int j = 0; j < arrNum.Length; j++)
                {
                    if (strChar.Equals(arrNum[j]) == true)
                    {
                        blnNum = true;
                        break;
                    }
                }
                
                // ���������
                if (blnNum == true)
                {
                    strSect += strChar;
                    
                    if (strSect.Length > len && pos < arrPart.Length)
                    {
                        arrPart[pos++] = strSect.Substring(0, len);
                        len = 2;
                        
                        strSect = strChar;
                    }
                }
                else
                {
                    if (strSect.Length > 0 && pos < arrPart.Length)
                    {
                        arrPart[pos++] = strSect;
                        len = 2;

                        strSect = string.Empty;
                    }
                }
            }
            
            if (pos < 3)            
            {
                return false;
            }
            
            // ��ȷ���ж�
            for(int i = 0; i < arrPart.Length; i++)
            {
                val = int.Parse(arrPart[i]);
                
                switch(i)
                {
                    case 0:     // ��
                        if (val < 50) {val += 2000; }
                        if (50 <= val && val < 100) {val += 1900; }
                        
                        if (val > 9999) { return false; }
                        
                        year = val;
                        break;
                        
                    case 1:     // ��
                        if (val < 1 || val > 12) { return false; } 
                        
                        month = val;
                        break;
                        
                    case 2:     // ��
                        maxDays = 31;
                        
                        if (month == 4 || month == 6 || month == 9 || month == 11)
                        {
                            maxDays = 30;
                        }
                        
                        if (month == 2)
                        {
                            maxDays = 28;

                            if (DateTime.IsLeapYear(year) == true)
                            {
                                maxDays = 29;
                            }
                        }
                        
                        if (val < 1 || val > maxDays) { return false; } 
                        break;
                    
                    case 3:     // ʱ
                        if (val < 0 || 23 < val) { return false; }
                        break;

                    case 4:     // ��
                    case 5:     // ��
                        if (val < 0 || 59 < val) { return false; }
                        break;
                }
            }
            
            // ��ϳ�����ʱ���ַ���
            dtSrc = arrPart[0] + "-" + arrPart[1] + "-" + arrPart[2] + " " + arrPart[3] + ":" + arrPart[4] + ":" + arrPart[5];
            return true;
        }


        /// <summary>
        /// ��һ����ʾ���ڵ��ַ���ת���ɶ����ڸ�ʽ
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        static public string GetDateTimeShort(string dt)
        {
            if (dt.Trim().Length > 0)
            {
                return DateTime.Parse(dt).ToString(ComConst.FMT_DATE.SHORT);
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// ��һ����ʾ���ڵ��ַ���ת���ɳ����ڸ�ʽ
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        static public string GetDateTimeLong(string dt)
        {
            if (dt.Trim().Length > 0)
            {
                return DateTime.Parse(dt).ToString(ComConst.FMT_DATE.LONG);
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// ��ȡ��ʱ���ʽ
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        static public string GetShortTime(string dt)
        {
            if (dt.Trim().Length > 0)
            {
                return DateTime.Parse(dt).ToString(ComConst.FMT_DATE.TIME_SHORT);
            }
            else
            {
                return string.Empty;
            }
        }
        
        
        /// <summary>
        /// ��ȡ����(��8.2)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        static public string GetMD(string dt)
        {
            if (dt.Trim().Length > 0)
            {
                DateTime dtValue = DateTime.Parse(dt);
                int month = dtValue.Month;
                int day = dtValue.Day;
                return month.ToString() + "." + day.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        
        
        /// <summary>
        /// ��ȡ������, ��2007��12��6��
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        static public string GetYMD(string dt)
        {
            if (dt.Trim().Length > 0)
            {
                DateTime dtValue = DateTime.Parse(dt);

                int year    = dtValue.Year;
                int month   = dtValue.Month;
                int day     = dtValue.Day;

                return year.ToString() + ComConst.STR.YEAR 
                    + month.ToString() + ComConst.STR.MONTH 
                    + day.ToString() + ComConst.STR.DAY;
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// ��ȡ���ֵ������ʾ
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        static public string GetRomaNum(int num)
        {
            if (num <= 0) { return string.Empty; }
            
            string[] arrNum = {"��","��","��","��","��","��","��","��","��","��"};
            string romaNum = string.Empty;
            
            string numStr = num.ToString();
            
            for(int i = 0; i < numStr.Length; i++)
            {
                romaNum += arrNum[int.Parse(numStr.Substring(i, 1))];
            }
            
            return romaNum;
        }
        #endregion
        

		#region �жϺ���
		/// <summary>
		/// �ж�һ���ַ����ǲ���������
		/// </summary>
		/// <remarks>����������</remarks>
		/// <param name="text">����֤���ַ���</param>
		/// <returns>TRUE: ����Ҫ��; FALSE: ������Ҫ��</returns>
		public static bool IsNumber(string text)
		{
			return Regex.IsMatch(text, @"^\d+$");
		}


        /// <summary>
        /// ���ĳ�����ǲ�������(����0)
        /// </summary>
        /// <param name="text">Ҫ�����ַ���</param>
        /// <returns>TRUE: �ɹ�; FALSE: ʧ��</returns>
        static public bool IsPositive(String text)
        {
            Regex objNotPositivePattern=new Regex("[^0-9.]");
            Regex objPositivePattern=new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern=new Regex("[0-9]*[.][0-9]*[.][0-9]*");

            return !objNotPositivePattern.IsMatch(text) &&
                objPositivePattern.IsMatch(text)  &&
                !objTwoDotPattern.IsMatch(text);
        }


        /// <summary>
        /// ���һ���ַ����ǲ��ǺϷ���ʱ���ʽ
        /// �����: �������ַ���ת���ɱ�׼�����ڸ�ʽHH:MM:SS
        /// </summary>
        /// <param name="timeStr">��ʾʱ����ַ�</param>
        /// <returns>TRUE: ��; FALSE: ����</returns>
        static public bool IsTime(ref string timeStr)
        {
            const int MAX_TIME_ITEM_COUNT   = 3;
            
            int intValue                    = 0;
            
            string[] astrItem = timeStr.Split(ComConst.STR.COLON.ToCharArray());
            
            if (astrItem.Length < 1) return false;
            
            for(int i = 0; i < astrItem.Length && i < MAX_TIME_ITEM_COUNT; i++)
            {
                if (astrItem[i].Trim().Length == 0)
                {
                    astrItem[i] = ComConst.FMT_DATE.TIME_ITEM;
                }
                
                // ������������ַ���, �˳�
                if (IsNumber(astrItem[i]) == false)
                {
                    return false;
                }
                
                intValue = int.Parse(astrItem[i]);
                
                // Сʱ
                if (i == 0)
                {
                    // Сʱ��Χ 0 <= X < 24
                    if (intValue < 0 || intValue > 23)
                    {
                        return false;
                    }
                    
                    timeStr = intValue.ToString(ComConst.FMT_DATE.TIME_ITEM);
                }
                    // ���ӻ���
                else
                {
                    // ���ӻ��뷶Χ 0 <= X < 60
                    if (intValue < 0 || intValue > 60)
                    {
                        return false;
                    }
                    
                    timeStr += ComConst.STR.COLON + intValue.ToString(ComConst.FMT_DATE.TIME_ITEM);
                }
            }
            
            // ���㲻���Ĳ���
            for(int i = astrItem.Length; i < MAX_TIME_ITEM_COUNT; i++)
            {
                timeStr += ComConst.STR.COLON + ComConst.FMT_DATE.TIME_ITEM;
            }

            return true;
        }


        /// <summary>
        /// ���һ���������Ƿ�����ȷ�����ڸ�ʽ
        /// </summary>
        /// <remarks>YYYY-MM-DD��YYYYMMDD</remarks>
        /// <param name="dtStr"></param>
        /// <returns></returns>
        static public bool IsShortDate(string dtStr)
        { 
            // ȥ�����пո�
            dtStr = dtStr.Replace(ComConst.STR.BLANK, string.Empty);

            if (dtStr.Length < 8) { return false;}

            dtStr = dtStr.Replace(ComConst.STR.SLASH,       ComConst.STR.LINEATION);
            dtStr = dtStr.Replace(ComConst.STR.BACKSLASH,   ComConst.STR.LINEATION);

            // ���û�ָ���
            if (dtStr.IndexOf(ComConst.STR.LINEATION) < 0)
            {
                dtStr = dtStr.Substring(0, 4) + ComConst.STR.LINEATION 
                      + dtStr.Substring(4, 2) + ComConst.STR.LINEATION 
                      + dtStr.Substring(6, 2);
            }

            // ��ȡ�����յ�ÿһ������
            string[] arrPart = dtStr.Split(ComConst.STR.LINEATION.ToCharArray());

            if (arrPart.Length != 3) { return false; }
            
            if (IsPositive(arrPart[0]) == false
                || IsPositive(arrPart[1]) == false
                || IsPositive(arrPart[2]) == false)
            {
                return false;
            }

            // ��������Ƿ���ȷ            
            return true;
        }
		#endregion
	}
}
