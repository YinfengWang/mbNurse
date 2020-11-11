using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TJ.CHSIS
{
    public partial class DecorateFrm : Form
    {
        public DecorateFrm()
        {
            InitializeComponent();
        }
        
        
        public DecorateFrm(Form showFrm)
        {
            InitializeComponent();
            
            showFrm.FormBorderStyle = FormBorderStyle.None;
            showFrm.TopLevel        = false;
            showFrm.Dock            = DockStyle.Fill;
            showFrm.Visible         = true;
            
            pnlHolder.Controls.Add(showFrm);
            showFrm.Dock = DockStyle.Fill;
        }
        
        
        private void FormHolderFrm_Load(object sender, EventArgs e)
        {

        }
        
        
        private void DecorateFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach(Control ctrl in pnlHolder.Controls)
            {
                Form frm = ctrl as Form;
                
                if (frm != null)
                {
                    frm.Close();
                }
            }
        }
    }
}
