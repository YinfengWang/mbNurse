//------------------------------------------------------------------------------------
//
//  ϵͳ����        : �����ƶ�ҽ��
//  ��ϵͳ����      : ������վ
//  ��������        : 
//  ����            : ComFunctionApp.cs
//  ���ܸ�Ҫ        : ��ͨ����
//  ������          : ����
//  ������          : 2006-05-08
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
	/// ComFunctionApp ��ժҪ˵����
	/// </summary>
	public class ComFunctionApp
	{
		public ComFunctionApp()
		{
		}
        
        
        /// <summary>
        /// ��ȡ����ȼ�����ʾ��ɫ
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
         
         
        #region ��ͨ����
		/// <summary>
		/// ��ȡ�������ڸ�ʽ������
		/// </summary>
		/// <remarks>
		/// ��ʽ: YY-MM-DD hh:mm
		/// </remarks>
		/// <param name="strDate">�����ַ���</param>
		/// <returns></returns>
		static public string GetDateFormat_Order(string strDate)
		{
			// ���Ϊ���ַ���
			if (strDate.Trim().Length == 0)
			{
				return string.Empty;
			}

			// ��ȡ����
			DateTime dtOrder = DateTime.Parse(strDate);
		    return dtOrder.ToString("yy-MM-dd HH:mm");
		}

        
        static public string GetDateFormat_Order(DateTime dtOrder)
		{
			// ��ȡ����
		    return dtOrder.ToString("yy-MM-dd HH:mm");
		}

        
		/// <summary>
		/// ��ʽ������
		/// </summary>
		/// <remarks>
		/// ���� + ������λ (ע: ������λ��ʽ��Ϊռ�����ַ�λ��)
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
		/// ��ȡִ�е����
		/// </summary>
		/// <returns>
		/// 0: ��ҩ�� 
		/// 1: ע�䵥
		/// 2: ��Һ��
		/// 3: ���Ƶ�
		/// 4: ����
		/// </returns>
		static public int GetOrderExecuteBillClassCode(string strOrderExecuteBillClass)
		{
			strOrderExecuteBillClass = strOrderExecuteBillClass.Trim();

			switch(strOrderExecuteBillClass)
			{
				case "��ҩ��":  return 0;
				case "ע�䵥":  return 1;
				case "��Һ��":  return 2;
				case "���Ƶ�":  return 3;
				case "����":  return 4;
				default:        return -1;
			}
		}
		#endregion 
		
		
		/// <summary>
		/// ��ȡɾ��������
		/// </summary>
		/// <param name="dsSrc"></param>
		/// <param name="tableName"></param>
		/// <returns></returns>
		static public DataSet GetDataSetDeleted(ref DataSet dsSrc, DateTime dtNow)
		{
		    DataSet ds = dsSrc.Clone();
		    ds.Tables[0].TableName += "_TOMBSTONE";
		    
		    // ��������
		    DataColumn[] dcPrimary = new DataColumn[dsSrc.Tables[0].PrimaryKey.Length];
		    for(int i = 0; i < dcPrimary.Length; i++)
		    {
		        dcPrimary[i] = dsSrc.Tables[0].PrimaryKey[i];
		    }
		    
		    // �������
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
		    
		    // �ָ�����
		    dsSrc.Tables[0].PrimaryKey = dcPrimary;
		    
		    return ds;
		}				
	}    
}
