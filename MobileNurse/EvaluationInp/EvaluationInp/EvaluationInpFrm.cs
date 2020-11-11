using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace HISPlus
{
    public partial class EvaluationInpFrm : EvaluationEverydayFrm
    {
        public EvaluationInpFrm()
        {
            InitializeComponent();
            
            _id         = "00037";
            _guid       = "4A3C8122-431B-4db4-B409-D5842CAA818F";
                        
            _dict_id    = "02";                     // 字典ID
            _moreTimes  = false;                    // 是否多次评估
            _template   = "住院评估表";             // 模板文件
        }
               
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EvaluationEverydayFrm_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("这里也到了！");
        }
    }
}
