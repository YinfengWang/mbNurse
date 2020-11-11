using System.Drawing.Drawing2D;
using System.IO;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace HISPlus
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// 表格
    /// </summary>
    internal sealed class GridNode : BaseNode
    {
        public readonly PictureBox _ctl;

        public GridNode(DesignTemplate container, DocTemplateElement nursingDocNode)
            : base(container, nursingDocNode)
        {
            base.HasValue = false;
            _ctl = new PictureBox { Tag = this };
            string filePath = "Template\\" + nursingDocNode.DisplayName;
            if (!File.Exists(filePath)) return;
            //读取文件流
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            int byteLength = (int)fileStream.Length;
            byte[] fileBytes = new byte[byteLength];
            fileStream.Read(fileBytes, 0, byteLength);

            //文件流关闭,文件解除锁定
            fileStream.Close();

            Image img = Image.FromStream(new MemoryStream(fileBytes));
            _ctl.Image = img;

            _ctl.Width = img.Width;
            _ctl.Height = img.Height;

            _ctl.BorderStyle = BorderStyle.None;
            // （zoom:缩放；clip剪短；stretchHorizontal:纵向拉伸；stretchVertical:横向拉伸；squeeze:压缩）
            //_ctl.Properties.SizeMode = PictureSizeMode.Zoom;

            if (ControlWidth > 0)
            {
                _ctl.Width = ControlWidth;
            }
            if (ControlHeight > 0)
            {
                _ctl.Height = ControlHeight;
            }

            ControlList.Add(this._ctl);
        }

        protected override void LayoutBeforeChildNodes(ref Point location)
        {
            //LayoutPrefix(ref location);
            //Container.AddControl(this._ctl, new Point(location.X, location.Y - 6 + (int)NursingDocNode.RowSpacing));
            Container.AddControl(this._ctl, new Point(location.X, location.Y + 3 + (int)NursingDocNode.RowSpacing));
            location.X += this._ctl.Width;
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
                return null;
            }
            set
            {
            }
        }
    }
}

