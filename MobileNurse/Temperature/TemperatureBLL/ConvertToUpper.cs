using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemperatureBLL
{
    /// <summary>
    /// 公共类: 小写数字/金额转换成大写
    /// </summary>
    public class ConvertToUpper
    {
        private static readonly char[] chnDigit = new char[] { '十', '百', '千', '万', '亿' };
        private static readonly char[] chnText = new char[] { '零', '一', '二', '三', '四', '五', '六', '七', '八', '九' };

        /// <summary>
        /// 转换成大写
        /// </summary>
        /// <param name="o">入参.不能为空</param>
        /// <returns></returns>
        public static string ConvertToUpp(object o)
        {
            string s = Convert.ToString(o);

            char[] integral = s.ToCharArray();
            StringBuilder strInt = new StringBuilder();
            int digit = integral.Length - 1;
            int i = 0;
            while (i < (integral.Length - 1))
            {
                strInt.Append(chnText[integral[i] - '0']);
                if (0 == (digit % 4))
                {
                    if ((4 == digit) || (12 == digit))
                    {
                        strInt.Append(chnDigit[3]);
                    }
                    else if (8 == digit)
                    {
                        strInt.Append(chnDigit[4]);
                    }
                }
                else
                {
                    strInt.Append(chnDigit[(digit % 4) - 1]);
                }
                digit--;
                i++;
            }
            if (('0' != integral[integral.Length - 1]) || (1 == integral.Length))
            {
                strInt.Append(chnText[integral[i] - '0']);
            }
            i = 0;
            while (i < strInt.Length)
            {
                int j = i;
                bool bDoSomething = false;
                while ((j < (strInt.Length - 1)) && ("零" == strInt.ToString().Substring(j, 1)))
                {
                    string strTemp = strInt.ToString().Substring(j + 1, 1);
                    if (("万" == strTemp) || ("亿" == strTemp))
                    {
                        bDoSomething = true;
                        break;
                    }
                    j += 2;
                }
                if (j != i)
                {
                    strInt = strInt.Remove(i, j - i);
                    if (!((i > (strInt.Length - 1)) || bDoSomething))
                    {
                        strInt = strInt.Insert(i, 0x96f6);
                        i++;
                    }
                }
                if (bDoSomething)
                {
                    strInt = strInt.Remove(i, 1);
                    i++;
                }
                else
                {
                    i += 2;
                }
            }
            int index = strInt.ToString().IndexOf("亿万");
            if (-1 != index)
            {
                if (((strInt.Length - 2) != index) && (((index + 2) < strInt.Length) && ("零" != strInt.ToString().Substring(index + 2, 1))))
                {
                    strInt = strInt.Replace("亿万", "亿零", index, 2);
                }
                else
                {
                    strInt = strInt.Replace("亿万", "亿", index, 2);
                }
            }
            if ((strInt.Length > 1) && ("一十" == strInt.ToString().Substring(0, 2)))
            {
                strInt = strInt.Remove(0, 1);
            }
            return strInt.ToString();
        }
    }
}
