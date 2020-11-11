using System.Drawing.Drawing2D;
using DevExpress.XtraEditors;

namespace HISPlus
{
    using System;
    using System.Configuration;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// 复选框
    /// </summary>
    internal sealed class CheckBoxNode : BaseNode
    {
        private readonly CheckEdit _checkEdit;
        public CheckListNode ParentNode;
        /// <summary>
        /// 后缀(Label)
        /// </summary>
        private readonly LabelControl _lblPostFix;

        public CheckBoxNode(DesignTemplate container, DocTemplateElement nursingDocNode)
            : base(container, nursingDocNode)
        {
            string foreColor = ConfigurationManager.AppSettings["DocCheckBoxForeColor"] ?? "Blue";
            _checkEdit = new CheckEdit { Font = ContentFont, Text = nursingDocNode.DisplayName, ForeColor = Color.FromName(foreColor) };

            this._checkEdit.Size = new Size(Math.Max(this._checkEdit.PreferredSize.Width, ControlWidth), this.ItemHeight);

            _checkEdit.Tag = this;

            //_checkEdit.ClientRectangle.Width
            ControlList.Add(this._checkEdit);

            if (ShowScore)
                if (MaxScore > 0)
                {
                    _lblPostFix = new LabelControl();
                    _lblPostFix.Name = "_lblPostFix";
                    _lblPostFix.Font = ContentFont;
                    _lblPostFix.Text = string.Format("({0}分)", nursingDocNode.Score);//2015.11.09 del
                    _lblPostFix.Appearance.TextOptions.HAlignment
                 = DevExpress.Utils.HorzAlignment.Near;
                    _lblPostFix.Appearance.TextOptions.VAlignment
                        = DevExpress.Utils.VertAlignment.Bottom;
                    _lblPostFix.Size = new Size(this._lblPostFix.PreferredSize.Width, this.ItemHeight);
                    //_lblPostFix.Visible = false;//2015.11.09 debug add
                    //_lblPostFix.Text = string.Empty;//2015.11.09 debug add
                    ControlList.Add(this._lblPostFix);//2015.11.09 del
                }

            this._checkEdit.CheckedChanged += this._checkbox_CheckedChanged;
            this._checkEdit.KeyPress += this._checkbox_KeyPress;

            if (ParentNode == null) return;
            if (ParentNode.MultiChecked)
            {
                this._checkEdit.Paint += _checkEdit_Paint;
            }
        }

        void _checkEdit_Paint(object sender, PaintEventArgs e)
        {
            CheckEdit rButton = (CheckEdit)sender;
            Graphics g = e.Graphics;
            Rectangle radioButtonrect = new Rectangle(0, 0, 12, 12);

            g.SmoothingMode = SmoothingMode.AntiAlias;//抗锯齿处理 

            //圆饼背景 
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                g.FillEllipse(brush, radioButtonrect);
            }


            if (rButton.Checked)
            {
                radioButtonrect.Inflate(-2, -2);//矩形内缩2单位 
                g.FillEllipse(Brushes.Red, radioButtonrect);
                radioButtonrect.Inflate(2, 2);//还原 
            }

            //圆形边框 
            using (Pen pen = new Pen(Color.Red))
            {
                g.DrawEllipse(pen, radioButtonrect);
            }
        }

        private void _checkbox_CheckedChanged(object sender, EventArgs e)
        {
            OnValueChanged(this, e);
        }

        private void _checkbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Container.SelectNextControl(sender as Control, true, true, true, true);
                e.Handled = true;
            }
        }

        protected override void LayoutBeforeChildNodes(ref Point location)
        {
            LayoutPrefix(ref location);
            if (IsInGrid)
            {
                this.Container.AddControl(this._checkEdit,
                    new Point(GridLocation.X + (int) this.NursingDocNode.ControlOffset,
                        GridLocation.Y + (int) NursingDocNode.RowSpacing));
            }
            else
            {
                Container.AddControl(this._checkEdit,
                    new Point(location.X, location.Y + (int) NursingDocNode.RowSpacing));
                location.X += this._checkEdit.Width;
                //location.Y += (int)NursingDocNode.RowSpacing;
            }

           
            if(!IsInGrid) //2015.11.09 debug 表格内暂不绘制后缀
            if (_lblPostFix != null && _lblPostFix.Visible)//2015.11.09 add && _lblPostFix.Visible 无效，是不是因为BaseNode？
            {
                Container.AddControl(this._lblPostFix, new Point(location.X, location.Y + 3 + (int)NursingDocNode.RowSpacing));
                location.X += this._lblPostFix.Width;
            }
        }

        public CheckEdit CheckBox
        {
            get
            {
                return this._checkEdit;
            }
        }

        private bool HasFormattedValue
        {
            get
            {
                return this._checkEdit.Checked;
            }
        }

        protected override sealed int ItemHeight
        {
            get
            {
                return Math.Max(base.ItemHeight, this._checkEdit.PreferredSize.Height);
            }
        }

        protected override int PartWidthBeforeChildNodes
        {
            get
            {
                return (PostfixWidth + this._checkEdit.Width + (this._lblPostFix == null ? 0 : this._lblPostFix.Width));
            }
        }

        public override string SelfFormattedValue
        {
            get
            {
                return string.Empty;
            }
        }

        public override object Value
        {
            get
            {
                if (this._checkEdit.Checked)
                {
                    return 1;
                }
                return null;
            }
            set
            {
                this._checkEdit.Checked = (value != null) && value.ToString().Equals("1");
            }
        }
    }
}

