// ***********************************************************************
// Assembly         : Xy.Auxiliary.AutoUpdate
// Author           : LPD
// Created          : 01-16-2016
//
// Last Modified By : LPD
// Last Modified On : 01-16-2016
// ***********************************************************************
// <copyright file="Program.cs" company="心医国际(西安)">
//     Copyright (c) 心医国际(西安). All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Diagnostics;
using Xy.Auxiliary.Bll;
using Xy.Auxiliary.PubCls;
using System.Threading;

/// <summary>
/// The AutoUpdate namespace.
/// </summary>
namespace Xy.Auxiliary.AutoUpdate
{
    /// <summary>
    /// Class Program.
    /// </summary>
    class Program
    {
        
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            try
            {
                /// <summary>
                /// 实例Mobile连接
                /// </summary>
                DBHelper dbMobile = new DBHelper(ConnFlag.mobile.ToString());

                //BodytempAutoupdate_Bll bodyTemp = new BodytempAutoupdate_Bll();

                Console.WriteLine("启动更新程序...");

                //本地更新的配置文件
                string updFileConfigPath = System.Environment.CurrentDirectory + "\\" + "UpdConfig.xml";

                //当前数据库中BodytempAutoupdate表数据
                DataTable dtBodyTemp = GetBodytempAutoupdate();//bodyTemp.GetBodytempAutoupdate();


                foreach (DataRow dtRow in dtBodyTemp.Rows)
                {
                    //下载
                    dbMobile.SaveByteToFlie(System.Environment.CurrentDirectory, dtRow["FILENAME"].ToString());
                }
                

                CreateXmlFile(dtBodyTemp, updFileConfigPath);

                

                Console.WriteLine("更新完成...");


                //执行更新程序
                Process.Start(System.Environment.CurrentDirectory + "\\" + "BodyTemp.exe");

                Console.ReadKey();                
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            
        }

        /// <summary>
        /// 创建XML文件
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileSavePath"></param>
        public static void CreateXmlFile(DataTable dt, string fileSavePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建根节点  
            XmlNode root = xmlDoc.CreateElement("FILES");
            xmlDoc.AppendChild(root);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XmlNode node1 = xmlDoc.CreateNode(XmlNodeType.Element, "file", null);
                CreateNode(xmlDoc, node1, "FILENAME", dt.Rows[i]["FILENAME"].ToString());
                CreateNode(xmlDoc, node1, "VERSION", dt.Rows[i]["VERSION"].ToString());
                root.AppendChild(node1);
            }

            try
            {
                xmlDoc.Save(fileSavePath);
            }
            catch (Exception e)
            {

            }


        }

        /// <summary>    
        /// 创建节点    
        /// </summary>    
        /// <param name="xmldoc"></param>  xml文档  
        /// <param name="parentnode"></param>父节点    
        /// <param name="name"></param>  节点名  
        /// <param name="value"></param>  节点值  
        ///   
        public static void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }

        /// <summary>
        /// 获取BODYTEMP_AUTOUPDATE表数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetBodytempAutoupdate()
        {
            string strSql = "SELECT * FROM MOBILE.BODYTEMP_AUTOUPDATE ";
            return new DBHelper(ConnFlag.mobile.ToString()).GetData(strSql);
        }
    }
}
