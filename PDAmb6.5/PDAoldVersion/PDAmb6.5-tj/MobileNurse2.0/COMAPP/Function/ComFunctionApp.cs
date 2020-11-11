//------------------------------------------------------------------------------------
//
//  系统名称        : 无线移动医疗
//  子系统名称      : 护理工作站
//  对象类型        : 
//  类名            : ComFunctionApp.cs
//  功能概要        : 共通函数
//  作成者          : 付军
//  作成日          : 2006-05-08
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Env = System.Environment;
using Net = System.Net;
using System.IO;
using System.Drawing;
using Path = System.IO.Path;
using Dir = System.IO.Directory;


namespace HISPlus
{    
	/// <summary>
	/// ComFunctionApp 的摘要说明。
	/// </summary>
	public class ComFunctionApp
	{
		public ComFunctionApp()
		{
		}
        
        
        /// <summary>
        /// 获取护理等级的显示颜色
        /// </summary>
        /// <param name="nursingClassCode"></param>
        /// <returns></returns>
        public static Color GetColor(string color, int defaultColor)
        {
            try
            {
                if (color.Length > 0)
                {
                    string[] arrPart = color.Split(ComConst.STR.COMMA.ToCharArray());
                    
                    if (arrPart.Length > 2)
                    {
                        return Color.FromArgb(int.Parse(arrPart[0]), 
                                              int.Parse(arrPart[1]), int.Parse(arrPart[2]));
                    }
                }
            }
            catch
            {                
            }
            
            if (defaultColor == 0)
            {
                return Color.FromArgb(255, 255, 255);
            }
            else
            {
                return Color.FromArgb(0, 0, 0);
            }
        }       
         
         
        #region 共通函数
		/// <summary>
		/// 获取日嘱日期格式的日期
		/// </summary>
		/// <remarks>
		/// 格式: YY-MM-DD hh:mm
		/// </remarks>
		/// <param name="strDate">日期字符串</param>
		/// <returns></returns>
		static public string GetDateFormat_Order(string strDate)
		{
			// 如果为空字符串
			if (strDate.Trim().Length == 0)
			{
				return string.Empty;
			}

			// 获取日期
			DateTime dtOrder = DateTime.Parse(strDate);
		    return dtOrder.ToString("yy-MM-dd HH:mm");
		}

        
        static public string GetDateFormat_Order(DateTime dtOrder)
		{
			// 获取日期
		    return dtOrder.ToString("yy-MM-dd HH:mm");
		}

        
		/// <summary>
		/// 格式化剂量
		/// </summary>
		/// <remarks>
		/// 剂量 + 剂量单位 (注: 剂量单位格式化为占三个字符位置)
		///  </remarks>
		/// <param name="strDosage"></param>
		/// <param name="strDosageUnit"></param>
		/// <returns></returns>
		static public string GetDosageFormat(string strDosage, string strDosageUnit)
		{
			const int DOSAGE_UNIT_LEN = 3;
            
            if (DataType.IsPositive(strDosage) == true)
            {
                strDosage = float.Parse(strDosage).ToString();
            }
            
			if (strDosageUnit.Length < DOSAGE_UNIT_LEN)
			{
				strDosageUnit = new System.String(' ', DOSAGE_UNIT_LEN - strDosageUnit.Length) + strDosageUnit;
			}

			return strDosage + strDosageUnit;
		}


		/// <summary>
		/// 获取执行单类别
		/// </summary>
		/// <returns>
		/// 0: 服药单 
		/// 1: 注射单
		/// 2: 输液单
		/// 3: 治疗单
		/// 4: 护理单
		/// </returns>
		static public int GetOrderExecuteBillClassCode(string strOrderExecuteBillClass)
		{
			strOrderExecuteBillClass = strOrderExecuteBillClass.Trim();

			switch(strOrderExecuteBillClass)
			{
				case "服药单":  return 0;
				case "注射单":  return 1;
				case "输液单":  return 2;
				case "治疗单":  return 3;
				case "护理单":  return 4;
				default:        return -1;
			}
		}
		#endregion 
		
		
		/// <summary>
		/// 获取删除的数据
		/// </summary>
		/// <param name="dsSrc"></param>
		/// <param name="tableName"></param>
		/// <returns></returns>
		static public DataSet GetDataSetDeleted(ref DataSet dsSrc, DateTime dtNow)
		{
		    DataSet ds = dsSrc.Clone();
		    ds.Tables[0].TableName += "_TOMBSTONE";
		    
		    // 缓存主键
		    DataColumn[] dcPrimary = new DataColumn[dsSrc.Tables[0].PrimaryKey.Length];
		    for(int i = 0; i < dcPrimary.Length; i++)
		    {
		        dcPrimary[i] = dsSrc.Tables[0].PrimaryKey[i];
		    }
		    
		    // 清除主键
		    dsSrc.Tables[0].PrimaryKey = null;
		    DataRow[] drFind = dsSrc.Tables[0].Select(string.Empty, string.Empty, DataViewRowState.Deleted);
		    
		    for(int i = 0; i < drFind.Length; i++)
		    {
		        DataRow dr = drFind[i];
		        dr.RejectChanges();
		        
		        DataRow drNew = ds.Tables[0].NewRow();
		        foreach(DataColumn dc in ds.Tables[0].Columns)
		        {
		            drNew[dc.ColumnName] = dr[dc.ColumnName];
		        }
		        
		        drNew["UPD_DATE_TIME"] = dtNow;
		        ds.Tables[0].Rows.Add(drNew);
                
		        dr.Delete();
		    }
		    
		    // 恢复主键
		    dsSrc.Tables[0].PrimaryKey = dcPrimary;
		    
		    return ds;
		}				
	}    
}
