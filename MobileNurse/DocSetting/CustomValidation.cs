using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;

namespace HISPlus
{
    public class CustomValidation
    {
        private readonly DXValidationProvider provider;

        /// <summary>
        /// 条件列表
        /// </summary>
        public List<ControlRule> RuleList { get; set; }

        public CustomValidation()
        {
            provider = new DXValidationProvider();
        }

        /// <summary>
        /// 执行验证
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            bool flag = true;
            if (provider != null)
            {
                provider.Dispose();
            }
            if (RuleList != null)
            {
                foreach (ControlRule item in RuleList)
                {
                    provider.SetIconAlignment(item.control, ErrorIconAlignment.MiddleRight);
                    provider.SetValidationRule(item.control, item.rule);
                    //if (!provider.Validate() && flag)
                    //{
                    //    flag = false;
                    //}
                }
                flag = provider.Validate();
            }
            else
            {
                flag = false;
            }
            return flag;
        }
    }

    public class CustomRuleByRegex : ValidationRule
    {
        string regex;
        bool isnotblank;//是否为空
        /// <summary>
        /// 是否为数字
        /// </summary>
        public static string strIsNumeric = "^[\\+\\-]?[0-9]*\\.?[0-9]+$";
        /// <summary>
        /// 电话号码
        /// </summary>
        public static string strPhone = @"(^(\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$|(1(([35][0-9])|(47)|[8][01236789]))\d{8}$";

        /// <summary>
        /// 使用正则表达式验证
        /// </summary>
        /// <param name="regex">正则表达式</param>
        /// <param name="isnotblank">是否允许为空</param>
        public CustomRuleByRegex(string regex, bool isnotblank)
        {
            this.regex = regex;
            this.isnotblank = isnotblank;
        }

        public override bool Validate(Control control, object value)
        {
            bool flag;
            try
            {
                if (value == null || value.ToString().Trim() == string.Empty)
                {
                    if (isnotblank)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                        this.ErrorText = "该值不允许为空！";
                    }
                }
                else
                {
                    flag = Regex.IsMatch((string)value, regex);
                }
            }
            catch (Exception ex)
            {
                this.ErrorText = ex.Message;
                flag = false;
            }
            return flag;
        }
    }

    public class ControlRule
    {
        public Control control;
        public ValidationRule rule;

        public ControlRule(Control control, ValidationRule rule)
        {
            this.control = control;
            this.rule = rule;
        }

        /// <summary>
        /// 判断控件是否为空
        /// </summary>
        /// <returns></returns>
        public static ConditionValidationRule NotEmpty()
        {
            ConditionValidationRule rule = new ConditionValidationRule();
            rule.ConditionOperator = ConditionOperator.IsNotBlank;
            rule.ErrorText = "该值不允许为空！";
            return rule;
        }

        /// <summary>
        /// 判断是否大于等于某个数
        /// </summary>
        /// <param name="number">数值</param>
        public static ConditionValidationRule NoLessNumber(decimal number)
        {
            ConditionValidationRule rule = new ConditionValidationRule();
            rule.ConditionOperator = ConditionOperator.GreaterOrEqual;
            rule.ErrorText = string.Format("该值不能小于{0}！", number);
            rule.Value1 = number;
            return rule;
        }

        /// <summary>
        /// 判断是否大于某个数
        /// </summary>
        /// <param name="number">数值</param>
        public static ConditionValidationRule GreaterNumber(decimal number)
        {
            ConditionValidationRule rule = new ConditionValidationRule();
            rule.ConditionOperator = ConditionOperator.Greater;
            rule.ErrorText = string.Format("该值必须大于{0}！", number);
            rule.Value1 = number;
            return rule;
        }

        /// <summary>
        /// 判断是否在指定数（包含）之间
        /// </summary>
        /// <param name="small">小数</param>
        /// <param name="big">大数</param>
        /// <returns></returns>
        public static ConditionValidationRule BetweenNumbers(decimal small, decimal big)
        {
            ConditionValidationRule rule = new ConditionValidationRule();
            rule.ConditionOperator = ConditionOperator.Between;
            rule.ErrorText = string.Format("该值不能小于{0}且不能大于{1}！", small, big);
            rule.Value1 = small;
            rule.Value2 = big;
            return rule;
        }

        /// <summary>
        /// 判断结束时间是否不小于指定时间
        /// </summary>
        /// <param name="startTime">指定时间</param>
        public static ConditionValidationRule NoLessDate(DateTime startTime)
        {
            ConditionValidationRule rule = new ConditionValidationRule();
            rule.ErrorText = string.Format("结束时间不能小于开始时间！");
            rule.ConditionOperator = ConditionOperator.GreaterOrEqual;
            rule.Value1 = startTime;
            return rule;
        }

        /// <summary>
        /// 调用正则表达式判断输入格式
        /// </summary>
        /// <param name="strRex">正则表达式</param>
        /// <param name="isnotblank">是否允许为空</param>
        public static CustomRuleByRegex AddValueRex(string strRex, bool isnotblank)
        {
            CustomRuleByRegex rule = new CustomRuleByRegex(strRex, isnotblank);
            rule.ErrorText = "输入格式不正确，请重新输入！";
            return rule;
        }
    }
}
