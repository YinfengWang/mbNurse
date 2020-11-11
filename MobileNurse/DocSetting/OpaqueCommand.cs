using System;
using System.Windows.Forms;

namespace HISPlus
{
    class OpaqueCommand
    {
        private MyOpaqueLayer m_OpaqueLayer = null;//°ëÍ¸Ã÷ÃÉ°å²ã

        private Control ctl;

        /// <summary>
        /// ÏÔÊ¾ÕÚÕÖ²ã
        /// </summary>
        /// <param name="control">¿Ø¼þ</param>
        /// <param name="alpha">Í¸Ã÷¶È</param>
        public void ShowOpaqueLayer(Control control, int alpha)
        {
            try
            {
                ctl = control;
                if (this.m_OpaqueLayer == null)
                {
                    this.m_OpaqueLayer = new MyOpaqueLayer(alpha);
                }
                m_OpaqueLayer.Location = new System.Drawing.Point(ctl.Left - 3, ctl.Top - 3);
                m_OpaqueLayer.Width = ctl.Width + 6;
                m_OpaqueLayer.Height = ctl.Height + 6;
                control.Parent.Controls.Add(this.m_OpaqueLayer);
                //this.m_OpaqueLayer.Dock = DockStyle.Fill;
                this.m_OpaqueLayer.BringToFront();
                this.m_OpaqueLayer.Enabled = true;
                this.m_OpaqueLayer.Visible = true;
            }
            catch { }
        }

        /// <summary>
        /// Òþ²ØÕÚÕÖ²ã
        /// </summary>
        public void HideOpaqueLayer()
        {
            try
            {
                if (this.m_OpaqueLayer != null)
                {
                    this.m_OpaqueLayer.Visible = false;
                    this.m_OpaqueLayer.Enabled = false;
                    if (ctl != null)
                        ctl.Controls.Remove(m_OpaqueLayer);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

    }
}
