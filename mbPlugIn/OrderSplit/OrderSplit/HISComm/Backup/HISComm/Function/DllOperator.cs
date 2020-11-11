using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace HISPlus
{
    /// <summary>
    /// 动态库操作类
    /// </summary>
    public class DllOperator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DllOperator()
        {
        }


        /// <summary>
        /// 从动态库中获取窗体
        /// </summary>
        /// <returns></returns>
        public static Form GetFormInDll(string dllName, string typeName)
        { 
            try
            {
                Assembly asmAssembly    = Assembly.LoadFrom(dllName);
                Type     typeToLoad     = asmAssembly.GetType(typeName);
                
                object GenericInstance  = Activator.CreateInstance(typeToLoad);

                Form formToLoad = new Form();
                formToLoad = (Form)(GenericInstance);
                
                return formToLoad;
            }
            catch(Exception ex)
            {
                string msg = "DLL名称: " + dllName + " 类名:" + typeName + " 没被发现! ";
                throw new Exception(msg + ComConst.STR.CRLF + ex.Message);
            }
        }
    }
}
