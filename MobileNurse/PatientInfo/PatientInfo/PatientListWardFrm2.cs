using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class PatientListWardFrm2 : Form
    {
        #region 变量
        public event PatientChangedEventHandler PatientChanged;                              // 定义事件

        protected string _deptCode = string.Empty;

        protected string _patientId = string.Empty;
        protected string _visitId = string.Empty;
        protected bool _showWaitInpPatient = false;                                        // 是否显示待入科病人
        protected bool _showOutHospital3Days = false;
        public DataSet DsPatient = null;
        public DataSet DsWardList = null;
        #endregion
        public PatientListWardFrm2()
        {
            InitializeComponent();
        }

        #region 属性
        public string DeptCode
        {
            get { return _deptCode; }
            set { _deptCode = value; }
        }


        public string PatientId
        {
            get { return _patientId; }
        }


        public string VisitId
        {
            get { return _visitId; }
        }


        /// <summary>
        /// 属性[显示待入科病人]
        /// </summary>
        public bool ShowWaitInpPatient
        {
            get
            {
                return _showWaitInpPatient;
            }
            set
            {
                _showWaitInpPatient = value;
            }
        }

        /// <summary>
        /// 属性[显示三天之内的病人]
        /// </summary>
        public bool ShowOutHospital3Days
        {
            get
            {
                return _showOutHospital3Days;
            }
            set
            {
                _showOutHospital3Days = value;
            }
        }

        #endregion

        #region 窗体事件
        private void PatientListWardFrm_Load(object sender, EventArgs e)
        {
            ucPatientList1.DeptCode = this.DeptCode;
            ucPatientList1.PatientChanged += new PatientChangedEventHandler(ucPatientList1_PatientChanged);
            ucPatientList1.ReloadData();
            DsPatient = ucPatientList1.DsPatient;
            DsWardList = ucPatientList1.DsWardList;
        }

        void ucPatientList1_PatientChanged(object sender, PatientEventArgs e)
        {
            _patientId = ucPatientList1.PatientId;
            _visitId = ucPatientList1.VisitId;
            if (PatientChanged != null)
                PatientChanged(this, new PatientEventArgs(_patientId, _visitId));
            DsPatient = ucPatientList1.DsPatient;
            DsWardList = ucPatientList1.DsWardList;
        }

        /// <summary>
        /// 菜单[刷新]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmnuRefresh_Click(object sender, EventArgs e)
        {
            ucPatientList1.Refresh();
        }
        #endregion

        #region 接口
        /// <summary>
        /// 定位病人
        /// </summary>
        public void locatePatient(string patientId)
        {
            ucPatientList1.locatePatient(patientId);
        }
        #endregion


        /// <summary>
        /// 重新加载病人列表
        /// </summary>
        public void ReloadPatientList()
        {
            ucPatientList1.ReloadData();
            DsPatient = ucPatientList1.DsPatient;
            DsWardList = ucPatientList1.DsWardList;
        }
    }
}
