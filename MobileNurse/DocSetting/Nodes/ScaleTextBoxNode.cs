namespace HISPlus
{
    using System;
    using System.Drawing;

    public class ScaleTextBoxNode : TextBoxNode
    {
        private int _scaleFormCode;

        public ScaleTextBoxNode(DesignTemplate container, DocTemplateElement node, int scaleFormCode)
            : base(container, node)
        {
            _scaleFormCode = scaleFormCode;
            //this._scaleFormCode = node.referenceCode;
            //base.TxtValue.ReadOnly = true;
            base.TxtControl.BackColor = SystemColors.Window;
            base.TxtControl.Enter += new EventHandler(this._txtValue_Enter);
            //if (base._docNode.formRecord != null)
            //{
            //    this.ReferenceID = base._docNode.formRecord.referenceId;
            //}
        }

        private void _txtValue_Enter(object sender, EventArgs e)
        {
            //FormScale scale;
            //Point location = base._container.PointToScreen(base._txtValue.Bounds.Location);
            //Rectangle txtScaleBoundsToScreen = new Rectangle(location, base._txtValue.Size);
            //if (this.ReferenceID == 0)
            //{
            //    scale = new FormScale(base._container.Patient, base._docNode.referenceCode, txtScaleBoundsToScreen);
            //}
            //else
            //{
            //    scale = new FormScale(base._container.Patient, base._docNode.referenceCode, this.ReferenceID, txtScaleBoundsToScreen);
            //}
            //scale.DocFormSaved += new FormScaleEventHandler(this.frmScale_DocFormSaved);
            //scale.Show();
        }

        //private void frmScale_DocFormSaved(object sender, FormScaleEventArgs e)
        //{
        //    base._txtValue.Text1 = e.DocForm.totalScore.ToString();
        //    this.ReferenceID = e.DocForm.id;
        //    base._container.SelectNextControl(base._txtValue, true, true, true, true);
        //}

        public int ReferenceID { get; private set; }
    }
}

