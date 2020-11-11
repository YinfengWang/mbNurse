using System;
using System.Collections.Generic;
using System.Text;

namespace HISPlus.UserControls
{
    /// <summary>
    /// 文本框基类。禁止其他项目引用。
    /// </summary>
    internal interface IBaseTextBox
    {
        bool Multiline { get; set; }

        int MaxLength { get; set; }

        char PasswordChar { get; set; }

        bool ReadOnly { get; set; }

        string Text { get; set; }
    }
}
