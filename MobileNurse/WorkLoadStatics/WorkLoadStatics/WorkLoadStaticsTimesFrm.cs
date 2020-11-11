using System;
using System.Collections.Generic;
using System.Text;

namespace HISPlus
{
    public class WorkLoadStaticsTimesFrm : StaticsFrm
    {
        public WorkLoadStaticsTimesFrm()
        {
            _id         = "00071";
            _guid       = "76A02EBF-DD89-4f1a-A8B8-F549AC6DE6FC";
            
            _template   = "护士工作量统计";                         // 模板文件
            _querySql   = "WorkLoadStaticsTimes";
        }
    }
}
