using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Common
{

    /// <summary>
    /// 返回结果类
    /// </summary>
    public class Result
    {
        #region 公共参数

        /// <summary>
        /// 处理结果 1：成功
        /// </summary>
        public const string RESULT_SUCCESS = "1";

        /// <summary>
        /// 处理结果 2：失败
        /// </summary>
        public const string RESULT_ERROR = "0";

        /// <summary>
        /// 是否包含元素  默认为false,当进行 [添加返回的xml节点] 操作时,自动置true
        /// </summary>
        public bool HasElement = false;

        #endregion 公共参数

        #region 私有参数

        /// <summary>
        /// WebService调用的处理结果状态
        /// </summary>
        private string resultCode = string.Empty;

        /// <summary>
        /// 失败情况的具体错误代码，结果状态成功时，该信息为空
        /// </summary>
        private string resultMsg = string.Empty;

        /// <summary>
        /// 返回的数据(仅数据,不包含处理状态和失败信息)
        /// </summary>
        private string returnXml = string.Empty;

        /// <summary>
        /// 创建返回的xml文件
        /// </summary>
        private XmlDocument xdoc = new XmlDocument();

        /// <summary>
        /// 根节点
        /// </summary>
        private XmlNode root;

        /// <summary>
        /// 是否显示ResultInfo节点
        /// </summary>
        private bool ShowResultInfo = true;

        #endregion 私有参数

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        public Result()
        {
            // 加入XML的声明部分<?xml version="1.0" encoding="gb2312" ?>
            // XmlDeclaration xdecl = xdoc.CreateXmlDeclaration("1.0", "gb2312", null);
            // xdoc.AppendChild(xdecl);

            // 加入根元素<Employees></Employees>
            XmlElement xelem = xdoc.CreateElement("Response");
            xdoc.AppendChild(xelem);

            // 取出根节点
            root = xdoc.SelectSingleNode("/Response");
            if (ShowResultInfo)
            {
                XmlElement xmlRow = this.CreateElement("ResponseInfo");

                xmlRow.AppendChild(this.CreateElement("resultCode"));
                xmlRow.AppendChild(this.CreateElement("resultMsg"));

                // 创建一个<ResultCode>节点           
                this.AppendNode(xmlRow);
            }
            else
            {
                // 创建一个<ResultCode>节点           
                this.AppendNode("ResultCode");
                // 创建一个<resultMsg>节点
                this.AppendNode("ResultMsg");
            }
            // 设置返回状态的默认值为成功
            resultCode = RESULT_SUCCESS;
        }
        #endregion 初始化

        #region 设定返回结果状态及状态信息

        /// <summary>
        /// 设定结果状态为 1失败
        /// </summary>        
        public void setResultError()
        {
            this.resultCode = RESULT_ERROR;
        }

        /// <summary>
        /// 获取操作是否成功的状态
        /// </summary>
        public bool IsSuccess
        {
            get { return this.resultCode != RESULT_ERROR; }
        }

        /// <summary>
        /// 设定结果状态为 2成功(异常信息自动清空)
        /// </summary>        
        public void setResultSuccess()
        {
            this.resultMsg = string.Empty;
            this.resultCode = RESULT_SUCCESS;
        }

        /// <summary>
        /// 设定结果状态
        /// </summary>
        /// <param name="resutStatus">结果状态 成功or失败</param>
        public void setResultStatus(string resutStatus)
        {
            this.resultCode = resutStatus;
        }

        /// <summary>
        /// 设定结果状态和状态信息
        /// </summary>
        /// <param name="resutStatus">结果状态 成功or失败</param>
        /// <param name="resultMsg">返回消息</param>
        public void setResultStatus(string resutStatus, string resultMsg)
        {
            this.resultCode = resutStatus;
            this.resultMsg = resultMsg;
        }

        /// <summary>
        /// 设定返回信息 [此时默认设置返回状态为失败]
        /// </summary>
        /// <param name="errorCode"></param>
        public void setErrorMsg(String resultMsg)
        {
            this.resultCode = RESULT_ERROR;
            this.resultMsg = resultMsg;
        }
        #endregion 设定返回结果状态及状态信息

        #region 获取返回的xml字符串

        /// <summary>
        /// 获取返回的xml字符串
        /// </summary>
        /// <returns></returns>
        public string getReturnXml()
        {
            try
            {
                if (ShowResultInfo)
                {
                    xdoc.SelectSingleNode("/Response/ResponseInfo/resultCode").InnerText = resultCode;
                    xdoc.SelectSingleNode("/Response/ResponseInfo/resultMsg").InnerText = resultMsg;
                }
                else
                {
                    xdoc.SelectSingleNode("/Response/ResultCode").InnerText = resultCode;
                    xdoc.SelectSingleNode("/Response/ResultMsg").InnerText = resultMsg;
                }
                return xdoc.InnerXml;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion 获取返回的xml字符串

        #region XML操作

        /// <summary>
        /// 创建具有指定名称的元素
        /// </summary>
        /// <param name="name">元素名称</param>
        /// <returns></returns>
        public XmlElement CreateElement(string name)
        {
            return xdoc.CreateElement(name);
        }

        /// <summary>
        /// 创建具有指定名称的元素
        /// </summary>
        /// <param name="name">元素名称</param>
        /// <param name="value">元素值</param>
        /// <returns></returns>
        public XmlElement CreateElement(string name, string value)
        {
            XmlElement xml = xdoc.CreateElement(name);
            xml.InnerText = value;
            return xml;
        }

        /// <summary>
        /// 添加返回的xml节点(节点值默认为空)
        /// </summary>
        /// <param name="nodeName">节点名称</param>        
        public void AppendNode(string nodeName)
        {
            XmlElement xml = xdoc.CreateElement(nodeName);
            xml.InnerText = string.Empty;
            root.AppendChild(xml);
        }

        /// <summary>
        /// 添加返回的xml节点(节点值默认为空)
        /// </summary>
        /// <param name="nodeName">节点名称</param>        
        public void AppendNode(XmlElement xml)
        {
            root.AppendChild(xml);
        }

        /// <summary>
        /// 添加返回的xml节点
        /// </summary>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeValue">节点Value</param>
        public void AppendNode(string nodeName, string nodeValue)
        {
            HasElement = true;

            if (this.resultCode == RESULT_SUCCESS)
            {
                XmlElement xml = xdoc.CreateElement(nodeName);
                xml.InnerText = nodeValue;
                root.AppendChild(xml);
            }
        }

        /// <summary>
        /// 添加返回的xml节点
        /// </summary>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeValue">节点Value</param>
        public void AppendNode(string nodeName, object nodeValue)
        {
            HasElement = true;

            if (this.resultCode == RESULT_SUCCESS)
            {
                XmlElement xml = xdoc.CreateElement(nodeName);
                xml.InnerText = nodeValue == null ? string.Empty : nodeValue.ToString();
                root.AppendChild(xml);
            }
        }
        #endregion XML操作
    }
}
