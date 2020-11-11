using System;
using System.Collections.Generic;
using System.Text;

namespace HISPlus
{
    public class Coding
    {
        #region 变量
        static private Encoding encoding    = Encoding.GetEncoding("GB18030");
        #endregion

            
        public Coding()
        {
        }
        
        
        /// <summary>
        /// 获取汉字首字母
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        static public string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            
            for(int i=0;i<len;i++)
            {
                myStr += GetInitial(strText.Substring(i,1));
            }
            
            return myStr;
        }

        
        /// <summary>
        /// 获取汉学首字母
        /// </summary>
        /// <param name="myChar"></param>
        /// <returns></returns>
        static public string GetInitial(string myChar)
        {
            byte[] arrCN = System.Text.Encoding.Default.GetBytes(myChar);
            if(arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area<<8) + pos;
                
                int[] areacode = {45217,45253,45761,46318,46826,47010,
                                  47297,47614,48119,48119,49062,49324,
                                  49896,50371,50614,50622,50906,51387,
                                  51446,52218,52698,52698,52698,52980,
                                  53689,54481};
                
                for(int i=0;i<26;i++)
                {
                    int max = 55290;
                    
                    if(i != 25) max = areacode[i+1];
                    
                    if(areacode[i]<=code && code<max)
                    {
                        return System.Text.Encoding.Default.GetString(new byte[]{(byte)(65+i)});
                    }
                }
                
                return "*";
            }
            else 
            {
                return myChar;
            }
        }
        
        
        /// <summary>
        /// 获取字节数
        /// </summary>
        /// <returns></returns>
        static public int GetStrByteLen(string text)
        {
            return encoding.GetByteCount(text);
        }            
    }            
}
