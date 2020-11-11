using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HISPlus
{
    /// <summary>
    /// 公共类
    /// </summary>
    public class DocCommon
    {
        #region 字体操作

        /// <summary>
        /// 从字符串读取字体
        /// </summary>
        /// <param name="str">字体字符串</param>
        /// <returns></returns>
        public static Font FontFromString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return new Font("宋体", 9);

            object obj = null;
            try
            {
                obj = new FontConverter().ConvertFromString(str);
            }
            catch { }

            return obj as Font ?? new Font("宋体", 9);
        }

        /// <summary>
        /// 字体转为字符串
        /// </summary>
        /// <param name="font">字体</param>
        /// <returns></returns>
        public static string FontToString(Font font)
        {
            object obj = null;
            try
            {
                obj = new FontConverter().ConvertToString(font);
            }
            catch { }

            return obj as String;
        }

        /// <summary>
        /// 显示字体对话框
        /// </summary>
        /// <param name="fontString">原字体</param>
        /// <returns>新字体</returns>
        public static string ShowFontDialog(string fontString)
        {
            FontDialog f = new FontDialog
            {
                Font = FontFromString(fontString),
                ShowColor = false,
                ShowApply = false,
                AllowScriptChange = false
            };
            if (f.ShowDialog() == DialogResult.OK)
            {
                return FontToString(f.Font);
            }
            return fontString;
        }

        #endregion

        #region 共通控件

        /// <summary>
        /// 设置数字控件限制
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="isFloatValue"></param>
        /// <param name="isScore"></param>
        public static void SetSpinEditLimit(SpinEdit ctl, bool isFloatValue = false, bool isScore = false)
        {
            if (isScore)
            {
                ctl.Properties.MinValue =0;
                ctl.Properties.MaxValue = 99;
                if (!isFloatValue)
                    ctl.Properties.MaxLength = 2;
            }
            else
            {
                ctl.Properties.MaxValue = 9999;
                ctl.Properties.MinValue = -9999;
            }

            ctl.Properties.IsFloatValue = isFloatValue;
            //上下箭头步长
            ctl.Properties.Increment = 10;
        }
        #endregion
    }
}
