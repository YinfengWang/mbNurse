using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace PACSMonitor
{
    public class BaseForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// 系统图标
        /// </summary>
        public Icon CommonIcon;

        /// <summary>
        /// 页面基类
        /// </summary>
        public BaseForm()
        {
            this.Load += new EventHandler(BaseForm_Load);
        }

        /// <summary>
        /// 添加页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_Load(object sender, System.EventArgs e)
        {
            //获取目标窗体
            Form form = this.FindForm();

            //获取组件源
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClient));

            CommonIcon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));

            //设置窗体图标
            form.Icon = CommonIcon;

            //设置文本
            form.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            //设置文本颜色
            form.ForeColor = System.Drawing.SystemColors.WindowText;

            //设置Esc取消按钮,用来关闭窗体
            SetEscButton(form);

            form.Focus();
        }

        /// <summary>
        /// 设置Esc取消按钮,用来关闭窗体
        /// </summary>
        /// <param name="form">窗体</param>
        private void SetEscButton(Form form)
        {
            //如果不是父页面 ,就添加Button作Esc关闭事件
            //if (form.Name != "frmClient")
            {
                //创建Button控件,用以关闭当前窗体
                System.Windows.Forms.Button tmp_btnClose = new System.Windows.Forms.Button();

                //为Button添加Esc事件
                tmp_btnClose.Click += new System.EventHandler(tmp_btnClose_Click);

                //设置Button的大小,使其不可见,但可用
                tmp_btnClose.Size = new System.Drawing.Size(0, 0);

                //添加控件
                form.Controls.Add(tmp_btnClose);
                //设置Esc响应Button
                form.CancelButton = tmp_btnClose;
                //激活控件
                //tmp_btnClose.TabIndex = 111;
            }
        }

        /// <summary>
        /// 虚方法,由子类实现   
        /// 关闭打开的子窗口(由 Esc 执行) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void tmp_btnClose_Click(object sender, EventArgs e)
        {
            //this.FindForm().Hide();
        }
    }
}
