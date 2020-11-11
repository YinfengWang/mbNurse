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
    public partial class OutHospitalGuideFrm : EvaluationEverydayFrm
    {
        public OutHospitalGuideFrm()
        {
            InitializeComponent();
            
            _id         = "00055";
            _guid       = "14603A32-E42D-4151-8CBC-45CF2BCD30DA";
                        
            _dict_id    = "03";                     // 字典ID
            _moreTimes  = false;                    // 是否多次评估
            _template   = "出院指导";               // 模板文件
        }
               
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EvaluationEverydayFrm_Load(object sender, EventArgs e)
        {            
        }
    }
}
