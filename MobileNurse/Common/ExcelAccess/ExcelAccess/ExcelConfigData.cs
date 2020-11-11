using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HISPlus
{
    public class ExcelPageInfo
    {
        /// <summary>
        /// 开始行
        /// </summary>
        public int Start_Row;

        /// <summary>
        /// 开始列
        /// </summary>
        public int Start_Col;

        /// <summary>
        /// 行数
        /// </summary>
        public int Unit_Rows;

        /// <summary>
        /// 列数
        /// </summary>
        public int Unit_Cols;

        /// <summary>
        /// 单元格重复数
        /// </summary>
        public int Unit_Count;

        /// <summary>
        /// 每页行数
        /// </summary>
        public int Page_Rows;
        
        public ExcelPageInfo()
        {
        }
        
        public void Clear()
        {
            Start_Row = 0;
            Start_Col = 0;
            Unit_Rows = 0;
            Unit_Cols = 0;
            Unit_Count = 0;
        }
    }


    public class ExcelItem
    {
        public int      Row;
        public int      Col;
        public string   ItemId;
        public string   CheckValue;
    }
    
    public class ExcelConfigSection
    {
        public bool     MultiRows;
        public string   TableName;
        public int      StartRow;
        public int      StartCol;
        public int      Max_Rows;
        
        public List<ExcelItem> ConfigItems;
    }
    
    public class ExcelConfigData
    {
        public static string STR_PAGE_CONFIG    = "PAGE_CONFIG";
        public static string STR_MULTI_ROWS     = "MULTI_ROWS";
        public static string STR_BARCODE        = "BARCODE";
        
        public ExcelPageInfo  PageInfo          = new ExcelPageInfo();
        public List<ExcelConfigSection>    ConfigSections    = new List<ExcelConfigSection>();
        
        public ExcelConfigData()
        {
        }
        
        
        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            PageInfo.Clear();
            ConfigSections.Clear();
        }
        
        
        /// <summary>
        /// 分析配置信息
        /// </summary>
        /// <returns></returns>
        public bool ParseConfigInfo(string configInfo)
        {
            string[] lines = configInfo.Split('\r');
            string line     = string.Empty;
            
            bool isPageInfo = false;
            
            ExcelConfigSection configSection = null;
            
            for(int i = 0; i < lines.Length; i++)
            {
                line = lines[i].Trim().ToUpper();
                
                // 如果是空白行
                if (line.Length == 0) continue;
                
                // 如果是注释行
                if (line.StartsWith(@"\\") || line.StartsWith(@"//"))
                {
                    continue;
                }
                
                // 如果是页面配置信息
                if (line.Equals("[PAGE_CONFIG]") == true)
                {
                    isPageInfo = true;
                    continue;
                }
                
                // 获取页面配置信息
                if (isPageInfo == true)
                {
                    getPageInfo(ref PageInfo, line);
                    isPageInfo = false;
                    continue;
                }
                
                // 如果是分段配置信息
                if (line.StartsWith("[") == true)
                {
                    isPageInfo = false;
                    
                    if (configSection != null)
                    {
                        ConfigSections.Add(configSection);
                    }
                    
                    configSection = new ExcelConfigSection();
                    getConfigSectionInfo(ref configSection, line);
                    continue;
                }
                
                getConfigSectionInfo(ref configSection, line);
            }

            if (configSection != null)
            {
                ConfigSections.Add(configSection);
            }
            
            return true;
        }
        
        
        /// <summary>
        /// 获取页面配置信息
        /// </summary>
        /// <returns></returns>
        private bool getPageInfo(ref ExcelPageInfo pageInfo, string configLine)
        {
            string[] parts = configLine.Split(",".ToCharArray());
            int val = 0;
            int index = 0;
            
            // 开始行
            if (parts.Length > index)
            {
                int.TryParse(parts[index], out val);
                pageInfo.Start_Row = val;
                index++;
            }
            
            // 开始列
            if (parts.Length > index)
            {
                int.TryParse(parts[index], out val);
                pageInfo.Start_Col = val;
                index++;
            }
                        
            // 行数
            if (parts.Length > index)
            {
                int.TryParse(parts[index], out val);
                pageInfo.Unit_Rows = val;
                index++;
            }
            
            // 列数
            if (parts.Length > index)
            {
                int.TryParse(parts[index], out val);
                pageInfo.Unit_Cols = val;
                index++;
            }
            
            // 单元格重复数
            if (parts.Length > index)
            {
                int.TryParse(parts[index], out val);
                pageInfo.Unit_Count = val;
                index++;
            }
            
            // 每页行数
            if (parts.Length > index)
            {
                int.TryParse(parts[index], out val);
                pageInfo.Page_Rows = val;
                index++;
            }
            
            return (index >= 2);
        }
        
        
        /// <summary>
        /// 获取分配置信息
        /// </summary>
        /// <param name="configSection"></param>
        /// <param name="configLine"></param>
        /// <returns></returns>
        private bool getConfigSectionInfo(ref ExcelConfigSection configSection, string configLine)
        {
            string[] arrParts = null;
            
            // 表名
            if (configLine.StartsWith("[") == true)
            {
                int pos1 = configLine.IndexOf("]");
                
                if (pos1 > 1)
                {
                    configSection.TableName = configLine.Substring(1, pos1 - 1);
                }
                
                return true;
            }

            // 起始位置 定义
            if (configLine.IndexOf(STR_MULTI_ROWS) >= 0)
            {
                configSection.MultiRows = true;
                
                configSection.Max_Rows  = -1;
                arrParts = configLine.Split(",".ToCharArray());
                if (arrParts.Length > 1)
                {
                    int maxRow = 0;
                    if (int.TryParse(arrParts[1], out maxRow) == true)
                    {
                        configSection.Max_Rows = maxRow;
                    }
                }
                
                return true;
            }
            
            // 节点定义
            arrParts = configLine.Split(",".ToCharArray());
            if (arrParts.Length < 3) return false;

            ExcelItem excelItem = new ExcelItem();
            excelItem.ItemId    = arrParts[1];
            excelItem.CheckValue = arrParts[2];
            
            // 获取行列
            arrParts = arrParts[0].Split(":".ToCharArray());
            if (arrParts.Length <= 1)
            {
                return false;
            }

            // 行号
            int row = 0;
            if (int.TryParse(arrParts[0], out row) == true)
            {
                excelItem.Row = row;
            }

            // 列号
            int col = ExcelAccess.GetCol(arrParts[1]);
            if (col <= 0)
            {
                return false;
            }
            
            excelItem.Col = col;
            
            // 把节点加入集合中
            if (configSection.ConfigItems == null)
            {
                configSection.ConfigItems = new List<ExcelItem>();
            }
            
            configSection.ConfigItems.Add(excelItem);
            
            return true;
        }
    }
}
