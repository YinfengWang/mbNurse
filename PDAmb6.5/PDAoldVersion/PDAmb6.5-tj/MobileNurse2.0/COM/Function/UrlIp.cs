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
        /// 获取本地IP地址
        /// </summary>
        /// <remarks>
        /// 多个IP地址的情况下只取第一个
        /// </remarks>
        /// <returns>本地机器名与IP地址</returns>
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
        /// 获取本地机器名与IP地址
        /// </summary>
        /// <remarks>
        /// 各项之间用Tab键分隔
        /// </remarks>
        /// <returns>本地机器名与IP地址</returns>
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
        /// 获取URL中的IP地址
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
        /// 改变Url中的主机地址
        /// </summary>
        /// <returns>新的URL</returns>
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
