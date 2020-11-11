using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace HISPlus
{
    class EvaluationUserControl
    {
        public string TYPE     = string.Empty;
        public string TEXT     = string.Empty;
        public string ID       = string.Empty;
        public Point  LOCATION = new Point(); 
        public Size   SIZE     = new Size(1, 1);
        public bool   CHILDREN = false;
        public string RNG      = string.Empty;
        public string PARENT   = string.Empty;
        
        public Control EditCtrl = null;
        
        public EvaluationUserControl()
        {
        }
    }
}
