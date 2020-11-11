using System;
using System.Collections.Generic;
using System.Text;
using Net = System.Net;

namespace HISPlus
{
    public class UrlIp
    {
        public UrlIp()
        { 
        }

        /// <summary>
        /// ��ȡ����IP��ַ
        /// </summary>
        /// <remarks>
        /// ���IP��ַ�������ֻȡ��һ��
        /// </remarks>
        /// <returns>���ػ�������IP��ַ</returns>
        static public string GetLocalIp()
        {
            string strIp = string.Empty;
            string strHostName = Net.Dns.GetHostName();
            
            Net.IPAddress[] astrIp = Net.Dns.GetHostEntry(strHostName).AddressList;

            if (astrIp != null && astrIp.Length > 0)
            {
                return astrIp[0].ToString();
            }
            
            return string.Empty;
        }


        /// <summary>
        /// ��ȡ���ػ�������IP��ַ
        /// </summary>
        /// <remarks>
        /// ����֮����Tab���ָ�
        /// </remarks>
        /// <returns>���ػ�������IP��ַ</returns>
        static public string GetLocalNameIp()
        {
            string strIp = string.Empty;
            string strHostName = Net.Dns.GetHostName();
            
            Net.IPAddress[] astrIp = Net.Dns.GetHostEntry(strHostName).AddressList;
            
            for(int i = 0; i < astrIp.Length; i++) 
            { 
                strIp += ComConst.STR.TAB + astrIp[i].ToString();
            } 

            return strHostName + strIp;
        }


        /// <summary>
        /// ��ȡURL�е�IP��ַ
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        static public string GetUrlIp(string url)
        { 
            int pos = url.IndexOf("//");
            int pos1 = url.IndexOf("/", pos + 2);

            if (pos1 > pos && pos >= 0)
            {
                return url.Substring(pos, pos1);
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// �ı�Url�е�������ַ
        /// </summary>
        /// <returns>�µ�URL</returns>
        static public string ChangeIpInUrl(string url, string newIp)
        {
            int pos = url.IndexOf("//");
            string header = url.Substring(0, pos + 2);

            pos = url.IndexOf("/", pos + 2);
            string tail = url.Substring(pos + 1);

            return header + newIp + "/" + tail;
        }
    }
}
