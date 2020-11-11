using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

using SqlConvert = HISPlus.SqlManager;
using DbFieldType = HISPlus.SqlManager.FIELD_TYPE;

namespace HISPlus
{
    public partial class InpNursingEvaluation : Form
    {
        #region ����
        private const string   CHECK        = "��";
        
        private DataSet        dsPatient    = null;                     // ������Ϣ
        private string         patientId    = string.Empty;             // ���˱�ʶ��
        private string         visitId      = string.Empty;             // סԺ��ʶ

        private DataSet        dsEvaluation = null;                     // ����������Ϣ
        private DataTable      dtShow       = null;
        private DataRow        drShow       = null;
        private DataSet        dsEduRec     = null;                     // ����������¼
        
        private ExcelAccess    excelAccess  = new ExcelAccess();
        private DataTable       dtPrint     = null;
        #endregion
        
        
        public InpNursingEvaluation()
        {
            InitializeComponent();
                        
            this.txtBedLabel.KeyDown += new KeyEventHandler( txtBedLabel_KeyDown );
            this.txtBedLabel.GotFocus += new EventHandler(imeCtrl_GotFocus);

            this.vsbInpEval.ValueChanged += new EventHandler( vsbInpEval_ValueChanged );
            this.vsbInpEval.Scroll += new ScrollEventHandler(vsbInpEval_ValueChanged);
            this.ResizeEnd += new EventHandler( InpNursingEvaluation_ResizeEnd );
            
            this.txtTemperature.LostFocus += new EventHandler( txtTemperature_LostFocus );
            this.txtTemperature.GotFocus += new EventHandler(imeCtrl_GotFocus);

            this.txtPulse.LostFocus += new EventHandler( txtPulse_LostFocus );
            this.txtPulse.GotFocus += new EventHandler(imeCtrl_GotFocus);

            this.txtHeartRate.LostFocus += new EventHandler( txtHeartRate_LostFocus );
            this.txtHeartRate.GotFocus += new EventHandler(imeCtrl_GotFocus);

            this.txtHeight.LostFocus += new EventHandler( chkPositive );
            this.txtHeight.GotFocus += new EventHandler(imeCtrl_GotFocus);

            this.txtWeight.LostFocus += new EventHandler( chkPositive );
            this.txtWeight.GotFocus += new EventHandler(imeCtrl_GotFocus);

            this.txtBloodPressureH.LostFocus += new EventHandler( chkPositive );
            this.txtBloodPressureH.GotFocus += new EventHandler(imeCtrl_GotFocus);

            this.txtBloodPressureL.LostFocus += new EventHandler( chkPositive );
            this.txtBloodPressureL.GotFocus += new EventHandler(imeCtrl_GotFocus);

            this.txtBedSoreLen.LostFocus += new EventHandler( chkPositive );
            this.txtBedSoreLen.GotFocus += new EventHandler(imeCtrl_GotFocus);

            this.txtBedSoreWidth.LostFocus += new EventHandler( chkPositive );
            this.txtBedSoreWidth.GotFocus += new EventHandler(imeCtrl_GotFocus);

            this.txtBedSorePart.GotFocus += new EventHandler(imeCtrl_GotFocus);

            this.chkIllHis.CheckedChanged += new EventHandler( chkIllHis_CheckedChanged );
            this.chkIllHis_No.CheckedChanged += new EventHandler( chkIllHis_No_CheckedChanged );

            this.chkAllergyHis_No.CheckedChanged += new EventHandler( chkAllergyHis_No_CheckedChanged );
            this.chkAllergyHis.CheckedChanged += new EventHandler( chkAllergyHis_CheckedChanged );
            
            this.chkConscious_1.CheckedChanged += new EventHandler(chkConscious_CheckedChanged);
            this.chkConscious_2.CheckedChanged += new EventHandler(chkConscious_CheckedChanged);
            this.chkConscious_3.CheckedChanged += new EventHandler(chkConscious_CheckedChanged);
            this.chkConscious_4.CheckedChanged += new EventHandler(chkConscious_CheckedChanged);
            this.chkConscious_5.CheckedChanged += new EventHandler(chkConscious_CheckedChanged);

            this.chkSighBug_No.CheckedChanged += new EventHandler( chkSighBug_No_CheckedChanged );
            this.chkSighBug.CheckedChanged += new EventHandler( chkSighBug_CheckedChanged );

            this.chkAuditionBug_No.CheckedChanged += new EventHandler( chkAuditionBug_No_CheckedChanged );
            this.chkAuditionBug.CheckedChanged += new EventHandler( chkAuditionBug_CheckedChanged );
            
            this.chkAche_No.CheckedChanged += new EventHandler( chkAche_No_CheckedChanged );
            this.chkAche.CheckedChanged += new EventHandler( chkAche_CheckedChanged );

            this.chkDiet_1.CheckedChanged += new EventHandler( chkDiet_CheckedChanged );
            this.chkDiet_2.CheckedChanged += new EventHandler(chkDiet_CheckedChanged);
            this.chkDiet_3.CheckedChanged += new EventHandler(chkDiet_CheckedChanged);
            this.chkDiet_4.CheckedChanged += new EventHandler(chkDiet_CheckedChanged);
            this.chkDiet_5.CheckedChanged += new EventHandler(chkDiet_CheckedChanged);

            this.chkSleep_0.CheckedChanged += new EventHandler( chkSleep_CheckedChanged );
            this.chkSleep_1.CheckedChanged += new EventHandler( chkSleep_CheckedChanged );
            this.chkSleep_2.CheckedChanged += new EventHandler( chkSleep_CheckedChanged );
            this.chkSleep_3.CheckedChanged += new EventHandler( chkSleep_CheckedChanged );
            this.chkSleep_4.CheckedChanged += new EventHandler( chkSleep_CheckedChanged );
            this.chkSleep_5.CheckedChanged += new EventHandler( chkSleep_CheckedChanged );

            this.chkStool_0.CheckedChanged += new EventHandler( chkStool_CheckedChanged );
            this.chkStool_1.CheckedChanged += new EventHandler( chkStool_CheckedChanged );
            this.chkStool_2.CheckedChanged += new EventHandler( chkStool_CheckedChanged );
            this.chkStool_3.CheckedChanged += new EventHandler( chkStool_CheckedChanged );

            this.chkPee_0.CheckedChanged += new EventHandler( chkPee_CheckedChanged );
            this.chkPee_1.CheckedChanged += new EventHandler( chkPee_CheckedChanged );
            this.chkPee_2.CheckedChanged += new EventHandler( chkPee_CheckedChanged );
            this.chkPee_3.CheckedChanged += new EventHandler( chkPee_CheckedChanged );
            this.chkPee_Other.CheckedChanged += new EventHandler( chkPee_CheckedChanged );

            this.chkSkin_0.CheckedChanged += new EventHandler( chkSkin_CheckedChanged );
            this.chkSkin_1.CheckedChanged += new EventHandler( chkSkin_CheckedChanged );
            this.chkSkin_2.CheckedChanged += new EventHandler( chkSkin_CheckedChanged );
            this.chkSkin_3.CheckedChanged += new EventHandler( chkSkin_CheckedChanged );
            this.chkSkin_4.CheckedChanged += new EventHandler( chkSkin_CheckedChanged );
            this.chkSkin_5.CheckedChanged += new EventHandler( chkSkin_CheckedChanged );
            this.chkSkin_6.CheckedChanged += new EventHandler( chkSkin_CheckedChanged );

            this.chkBedSore_No.CheckedChanged += new EventHandler( chkBedSore_No_CheckedChanged );
            this.chkBedSore.CheckedChanged += new EventHandler( chkBedSore_CheckedChanged );

            this.chkSelfDepend.CheckedChanged += new EventHandler( chkSelfDepend_CheckedChanged );
            this.chkSelfDepend_1.CheckedChanged += new EventHandler(chkSelfDepend_CheckedChanged);
            this.chkSelfDepend_2.CheckedChanged += new EventHandler(chkSelfDepend_CheckedChanged);

            this.chkIllCognition.CheckedChanged += new EventHandler( chkIllCognition_CheckedChanged );
            this.chkIllCognition_1.CheckedChanged += new EventHandler( chkIllCognition_CheckedChanged );
            this.chkIllCognition_2.CheckedChanged += new EventHandler( chkIllCognition_CheckedChanged );

            this.chkInpMode_Foot.CheckedChanged += new EventHandler( chkInpMode_CheckedChanged );
        }
        
        
        #region �����¼�
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InpNursingEvaluation_Load( object sender, EventArgs e )
        {
            try
            {
                initFrmVal();
                initDisp();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// ����ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InpNursingEvaluation_ResizeEnd( object sender, EventArgs e )
        {
            try
            {
                this.vsbInpEval.Maximum = (panelHolder.Height - panelLayout.Height) / 10 + 20;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// ����Żس��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtBedLabel_KeyDown(object sender, KeyEventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                // �������
                if (e.KeyCode != Keys.Return)
                {
                    return;
                }
                
                // ��ȡ��ѯ����
                if (txtBedLabel.Text.Trim().Length == 0)
                {
                    return;
                }
                
                GVars.App.UserInput = false;
                
                // ��ȡ������Ϣ
                dsPatient = getPatientInfo(txtBedLabel.Text.Trim(), GVars.User.DeptCode);
                if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
                {
                    GVars.Msg.Show("W00005");                           // �ò��˲�����!	
                    return;
                }
                
                // ��ʾ������Ϣ
                showPatientInfo();
                
                // ��ʾ���˻�����Ϣ
                initPatientComInfo();
                showPatientCommInfo();
                
                // ��ȡ��Ժ��������
                dtShow.Rows.Clear();
                drShow = dtShow.NewRow();
                
                getEvaluationInfo();
                
                formRecsInOne();
                
                // ��ʾ��Ժ��ƽ����
                initEvaluationDisp();
                showNursingEvalRec();
                
                // ��ȡ����������¼
                dsEduRec = getPatientEduRec(patientId, visitId);
                showPatientEduRec();
                
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
                this.Cursor = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// ��������������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void vsbInpEval_ValueChanged( object sender, EventArgs e )
        {
            try
            {
                int val = (-1 * this.vsbInpEval.Value) * 10;
                                
                this.panelHolder.Top = val;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// ��ť[��ѯ]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>      
        private void btnSave_Click( object sender, EventArgs e )
        {
            try
            {
                // ��������
                getDisp_NursingEvalRec();
                
                DataTable dtRec = disperseOneInRecs();
                DataSet dsResult = new DataSet();
                dsResult.Tables.Add(dtRec);
                
                saveNursingEvalRec(dsResult, patientId, visitId);
                
                // ��������
                savePatientEduRec();
                
                GVars.Msg.Show("I00004", "����");             // {0}�ɹ�!
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// ��ť[��ӡ]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click( object sender, EventArgs e )
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                // Ϊ��ӡ׼������
                prepareDataForPrint();
                
                // ��ӡ
                ExcelTemplatePrint();
                
                this.Cursor = Cursors.Default;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// ��ť[�ر�]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click( object sender, EventArgs e )
        {
            try
            {
                this.Close();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        

        /// <summary>
        /// ȷ��Ϊ������뷨
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void imeCtrl_GotFocus(object sender, EventArgs e)
        {
            IME.ChangeControlIme(this.ActiveControl.Handle);
        }

        
        #region �������
        void chkInpMode_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                CheckBox chkCur = (CheckBox)this.ActiveControl;                
                if (chkCur.Checked == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                
                chkInpMode_Foot.Checked = false;
                chkInpMode_Car.Checked = false;
                chkInpMode_Wheel.Checked = false;
                chkInpMode_Other.Checked = false;
                                
                chkCur.Checked = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }  
        }
                
        void chkIllCognition_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                CheckBox chkCur = (CheckBox)this.ActiveControl;                
                if (chkCur.Checked == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                
                chkIllCognition.Checked = false;
                chkIllCognition_1.Checked = false;
                chkIllCognition_2.Checked = false;
                
                chkCur.Checked = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }  
        }
        
        void chkSelfDepend_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                CheckBox chkCur = (CheckBox)this.ActiveControl;                
                if (chkCur.Checked == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                
                chkSelfDepend.Checked = false;
                chkSelfDepend_1.Checked = false;
                chkSelfDepend_2.Checked = false;
                
                chkCur.Checked = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }  
        }

        void chkBedSore_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                if (chkBedSore.Checked == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                
                chkBedSore_No.Checked = false;
                txtBedSorePart.Enabled = true;
                cmbBedSore.Enabled = true;
                txtBedSoreLen.Enabled = true;
                txtBedSoreWidth.Enabled = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }  
        }

        void chkBedSore_No_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                if (chkBedSore_No.Checked == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                
                chkBedSore.Checked = false;
                txtBedSorePart.Enabled = false;
                cmbBedSore.Enabled = false;
                txtBedSoreLen.Enabled = false;
                txtBedSoreWidth.Enabled = false;
                
                txtBedSorePart.Text = string.Empty;
                cmbBedSore.SelectedIndex = -1;
                txtBedSoreLen.Text = string.Empty;
                txtBedSoreWidth.Text = string.Empty;                
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }  
        }

        void chkSkin_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                CheckBox chkCur = (CheckBox)this.ActiveControl;                
                if (chkCur.Checked == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                
                this.chkSkin_0.Checked = false;
                this.chkSkin_1.Checked = false;
                this.chkSkin_2.Checked = false;
                this.chkSkin_3.Checked = false;
                this.chkSkin_4.Checked = false;
                this.chkSkin_5.Checked = false;
                this.chkSkin_6.Checked = false;
                                
                chkCur.Checked = true;
                if (chkCur.Text.Equals(chkSkin_6.Text) == true)
                {
                    txtSkin.Enabled = true;
                }
                else
                {
                    txtSkin.Enabled = false;
                    txtSkin.Text = string.Empty;
                }                
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            } 
        }

        void chkPee_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                CheckBox chkCur = (CheckBox)this.ActiveControl;                
                if (chkCur.Checked == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                
                this.chkPee_0.Checked = false;
                this.chkPee_1.Checked = false;
                this.chkPee_2.Checked = false;
                this.chkPee_3.Checked = false;
                this.chkPee_Other.Checked = false;
                
                chkCur.Checked = true;
                if (chkCur.Text.Equals(chkPee_Other.Text) == true)
                {
                    txtPee.Enabled = true;
                }
                else
                {
                    txtPee.Enabled = false;
                    txtPee.Text = string.Empty;
                }
                
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            } 
        }

        void chkStool_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                CheckBox chkCur = (CheckBox)this.ActiveControl;                
                if (chkCur.Checked == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                
                this.chkStool_0.Checked = false;
                this.chkStool_1.Checked = false;
                this.chkStool_2.Checked = false;
                this.chkStool_3.Checked = false;
                
                chkCur.Checked = true;
                if (chkCur.Text.Equals(chkStool_3.Text) == true)
                {
                    txtStool.Enabled = true;
                }
                else
                {
                    txtStool.Enabled = false;
                    txtStool.Text = string.Empty;
                }
                
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            } 
        }

        void chkSleep_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                CheckBox chkCur = (CheckBox)this.ActiveControl;                
                if (chkCur.Checked == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                
                this.chkSleep_0.Checked = false;
                this.chkSleep_1.Checked = false;
                this.chkSleep_2.Checked = false;
                this.chkSleep_3.Checked = false;
                this.chkSleep_4.Checked = false;
                this.chkSleep_5.Checked = false;
                
                chkCur.Checked = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            } 
        }

        void chkDiet_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                CheckBox chkCur = (CheckBox)this.ActiveControl;                
                if (chkCur.Checked == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                
                this.chkDiet_1.Checked = false;
                this.chkDiet_2.Checked = false;
                this.chkDiet_3.Checked = false;
                this.chkDiet_4.Checked = false;
                this.chkDiet_5.Checked = false;
                
                chkCur.Checked = true;
                
                if (chkCur.Text.Equals(chkDiet_5.Text) == false)
                {
                    txtDiet.Text = string.Empty;
                    txtDiet.Enabled = false;
                }
                else
                {
                    txtDiet.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }

        void chkAche_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                if (chkAche.Checked == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                
                chkAche_No.Checked = false;
                txtAchePart.Enabled = true;
                txtAcheClass.Enabled = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }

        void chkAche_No_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                if (chkAche_No.Checked == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                
                chkAche.Checked = false;
                txtAchePart.Enabled = false;
                txtAcheClass.Enabled = false;
                
                txtAchePart.Text = string.Empty;
                txtAcheClass.Text = string.Empty;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }
        
        void chkAuditionBug_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                if (chkAuditionBug.Checked == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                
                chkAuditionBug_No.Checked = false;
                cmbAuditionBug.Enabled = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }  
        }

        void chkAuditionBug_No_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                if (chkAuditionBug_No.Checked == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                
                chkAuditionBug.Checked = false;
                cmbAuditionBug.Enabled = false;
                cmbAuditionBug.SelectedIndex = -1;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }  
        }        
        
        void chkSighBug_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                if (chkSighBug.Checked == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                
                chkSighBug_No.Checked = false;
                cmbSighBug.Enabled = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }

        void chkSighBug_No_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                if (chkSighBug_No.Checked == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                
                chkSighBug.Checked = false;
                cmbSighBug.SelectedIndex = -1;
                cmbSighBug.Enabled = false;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }

        void chkConscious_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                CheckBox chkCur = (CheckBox)this.ActiveControl;
                if (chkCur.Checked == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                
                chkConscious_1.Checked = false;
                chkConscious_2.Checked = false;
                chkConscious_3.Checked = false;
                chkConscious_4.Checked = false;
                chkConscious_5.Checked = false;
                
                chkCur.Checked = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }

        void chkAllergyHis_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                if (chkAllergyHis.Checked == true)
                {
                    chkAllergyHis_No.Checked = false;
                    txtAllergyHis.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }

        void chkAllergyHis_No_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                if (chkAllergyHis_No.Checked == true)
                {
                    chkAllergyHis.Checked = false;
                    txtAllergyHis.Text = string.Empty;
                    txtAllergyHis.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }

        void chkIllHis_No_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                if (chkIllHis_No.Checked == true)
                {
                    chkIllHis.Checked = false;
                    txtIllHis.Text = string.Empty;
                    txtIllHis.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }        
        
        void chkIllHis_CheckedChanged( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;        
                if (chkIllHis.Checked == true)
                {
                    chkIllHis_No.Checked = false;
                    txtIllHis.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }

        #endregion
        
        
        #region ������
        void chkPositive( object sender, EventArgs e )
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;
                
                if (this.ActiveControl.Text.Trim().Length == 0)
                {
                    return;
                }
                
                if (DataType.IsPositive(this.ActiveControl.Text) == false)
                {
                    GVars.Msg.ErrorSrc = ActiveControl;
                    GVars.Msg.Show("E00012");   // ����������!
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }
        
        void txtTemperature_LostFocus( object sender, EventArgs e )
        {
            try
            {
                if (this.txtTemperature.Text.Trim().Length == 0)
                {
                    return;
                }
                
                if (DataType.IsPositive(this.txtTemperature.Text) == false)
                {
                    GVars.Msg.ErrorSrc = txtTemperature;
                    GVars.Msg.Show("E00012");   // ����������!
                    return;
                }
                
                if (chkBodyTemperature(float.Parse(txtTemperature.Text)) == false)
                {
                    GVars.Msg.ErrorSrc = txtTemperature;
                    GVars.Msg.Show();
                    return;
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        void txtPulse_LostFocus( object sender, EventArgs e )
        {
            try
            {
                if (this.txtPulse.Text.Trim().Length == 0)
                {
                    return;
                }
                
                if (DataType.IsPositive(this.txtPulse.Text) == false)
                {
                    GVars.Msg.ErrorSrc = txtPulse;
                    GVars.Msg.Show("E00012");   // ����������!
                    return;
                }
                
                if (chkPulse(float.Parse(txtPulse.Text)) == false)
                {
                    GVars.Msg.ErrorSrc = txtPulse;
                    GVars.Msg.Show();
                    return;
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }               
        }
        
        void txtHeartRate_LostFocus( object sender, EventArgs e )
        {
            try
            {
                if (this.txtHeartRate.Text.Trim().Length == 0)
                {
                    return;
                }
                
                if (DataType.IsPositive(this.txtHeartRate.Text) == false)
                {
                    GVars.Msg.ErrorSrc = txtHeartRate;
                    GVars.Msg.Show("E00012");   // ����������!
                    return;
                }
                
                if (chkHeartRate(float.Parse(txtHeartRate.Text)) == false)
                {
                    GVars.Msg.ErrorSrc = txtHeartRate;
                    GVars.Msg.Show();
                    return;
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }   
        }
        #endregion
        #endregion
        
        
        #region ��ͨ����
        /// <summary>
        /// ��ʼ���������
        /// </summary>
        private void initFrmVal()
        {
            // ������Ӧ�ڽ���ı�ṹ
            createNursingEvaluationTable();
            
            // ������Ӧ�ڴ�ӡ�ı�ṹ
            createTableForPrint();
            
            // ��ʼ������������Ŀ
            initEduItem();
        }
        
        
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void initDisp()
        {
            // ���ý���
            this.vsbInpEval.Maximum = (this.panelHolder.Height - this.panelLayout.Height) / 10 + 20;
            
            initPatientComInfo();
        }        
        
        
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="bedLabel">�����</param>
        /// <returns></returns>
        private DataSet getPatientInfo(string bedLabel, string wardCode)
        {
            string sql = string.Empty;

            sql = "SELECT ";
            sql+=    "PAT_MASTER_INDEX.NAME, ";                                      // ����
            sql+=    "PAT_MASTER_INDEX.SEX, ";                                       // �Ա�
            sql+=    "PAT_MASTER_INDEX.DATE_OF_BIRTH, ";                             // ��������
            sql+=    "PATS_IN_HOSPITAL.DIAGNOSIS, ";                                 // ��Ҫ���
                        
            sql+=    "PATS_IN_HOSPITAL.VISIT_ID, ";                                  // ����סԺ��ʶ
            sql+=    "PAT_MASTER_INDEX.PATIENT_ID, ";                                // ���˱�ʶ��
            sql+=    "PAT_MASTER_INDEX.INP_NO, ";                                    // סԺ��
            sql+=    "PAT_MASTER_INDEX.CHARGE_TYPE, ";                               // �ѱ�
            
            sql+=    "PAT_MASTER_INDEX.NATION ,";                                    // ����
            //sql+=    "PAT_MASTER_INDEX.DEGREE, ";                                  // ѧ��
            sql+=    "PAT_VISIT.OCCUPATION, ";                                       // ְҵ
            
            sql+=    "(SELECT PATIENT_CLASS_NAME FROM PATIENT_CLASS_DICT ";
            sql+=    "WHERE PATIENT_CLASS_CODE = PAT_VISIT.PATIENT_CLASS ";
            sql+=    ") INP_APPROACH, ";                                             // ��Ժ��ʽ
            
            sql+=	 "BED_REC.BED_NO, ";								             // ����
            sql+=    "BED_REC.BED_LABEL, ";                                          // �����
            
            sql+=    "PATS_IN_HOSPITAL.ADMISSION_DATE_TIME, ";                       // ��Ժ���ڼ�ʱ��

            sql+=    "(SELECT DEPT_NAME ";
            sql+=    "FROM DEPT_DICT ";
            sql+=    "WHERE DEPT_CODE = ";
            sql+=        "(CASE WHEN PATS_IN_HOSPITAL.LEND_INDICATOR = 1 ";
            sql+=         "THEN PATS_IN_HOSPITAL.DEPT_CODE_LEND ";
            sql+=         "ELSE PATS_IN_HOSPITAL.DEPT_CODE END)) ";
            sql+=    "DEPT_NAME ";                                                   // ���ڿ���
            
            sql+= "FROM ";
            sql+=    "PATS_IN_HOSPITAL, ";                                           // ��Ժ���˼�¼
            sql+=    "PAT_VISIT, ";                                                  // ����סԺ����¼
            sql+=    "PAT_MASTER_INDEX, ";                                           // ����������
            sql+=    "BED_REC ";                                                     // ��λ��¼

            sql+= "WHERE ";
            sql+=    "(BED_REC.BED_NO = PATS_IN_HOSPITAL.BED_NO ";
            sql+=    "AND BED_REC.WARD_CODE = PATS_IN_HOSPITAL.WARD_CODE) ";
            
            // ��λ��
            sql+=    "AND BED_REC.BED_LABEL = " + SqlConvert.SqlConvert(bedLabel);
            sql+=    "AND BED_REC.WARD_CODE = " + SqlConvert.SqlConvert(wardCode);
            sql+=   " AND PATS_IN_HOSPITAL.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID ";
            
            sql+=   " AND PAT_VISIT.PATIENT_ID = PATS_IN_HOSPITAL.PATIENT_ID ";
            sql+=   " AND PAT_VISIT.VISIT_ID = PATS_IN_HOSPITAL.VISIT_ID ";

            return GVars.OracleAccess.SelectData(sql);
        }        
        
        
        /// <summary>
        /// ��ʾ���˵Ļ�����Ϣ
        /// </summary>
        private void showPatientInfo()
        { 
            // ��ս���
            this.txtBedLabel.Text = string.Empty;                       // ���˴����
            this.lblPatientID.Text = string.Empty;                      // ���˱�ʶ��
            this.lblVisitID.Text = string.Empty;                        // ����סԺ��ʶ
            this.lblPatientName.Text = string.Empty;                    // ��������
            this.lblGender.Text = string.Empty;                         // �����Ա�
            this.lblDeptName.Text = string.Empty;                       // �Ʊ�
            this.lblInpNo.Text = string.Empty;                          // סԺ��
            this.lblInpDate.Text = string.Empty;                        // ��Ժ����
            
            patientId   = string.Empty;
            visitId     = string.Empty;
            
            // ���û�������˳�
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                return;
            }
            
            // ��ʾ���˵Ļ�����Ϣ
            DataRow dr = dsPatient.Tables[0].Rows[0];
            
            this.txtBedLabel.Text = dr["BED_LABEL"].ToString();         // ���˴����
            this.lblPatientID.Text = dr["PATIENT_ID"].ToString();       // ���˱�ʶ��
            this.lblVisitID.Text = dr["VISIT_ID"].ToString();           // ����סԺ��ʶ
            this.lblPatientName.Text = dr["NAME"].ToString();           // ��������
            this.lblGender.Text = dr["SEX"].ToString();                 // �����Ա�
            this.lblDeptName.Text = dr["DEPT_NAME"].ToString();         // �Ʊ�
            this.lblInpNo.Text = dr["INP_NO"].ToString();               // סԺ��
            this.lblInpDate.Text = DataType.GetDateTimeShort(dr["ADMISSION_DATE_TIME"].ToString());   // ��Ժ����
            
            patientId   = dr["PATIENT_ID"].ToString();
            visitId     = dr["VISIT_ID"].ToString();
        }        
        
        
        /// <summary>
        /// ��ղ���һ������
        /// </summary>
        private void initPatientComInfo()
        {
            txtPatientName.Text              = string.Empty;            // ����
            txtPatientGender.Text            = string.Empty;            // �Ա�
            txtPatientAge.Text               = string.Empty;            // ����
            txtDeptName.Text                 = string.Empty;            // �Ʊ�
            txtPatientBedLabel.Text          = string.Empty;            // ����
            txtCareer.Text                   = string.Empty;            // ְҵ
            txtNation.Text                   = string.Empty;            // ����
            txtPatientEducation.Text         = string.Empty;            // �Ļ��̶�
            txtInfoProvider.Text             = string.Empty;            // ��ʷ������
            txtInpDiagnose.Text              = string.Empty;            // ��Ժ���
            txtAssertDiagnose.Text           = string.Empty;            // ȷ�����

            lblInpDate_Year.Text             = string.Empty;            // ��Ժʱ��(��)
            lblInpDate_Month.Text            = string.Empty;            // ��Ժʱ��(��)
            lblInpDate_Day.Text              = string.Empty;            // ��Ժʱ��(��)
            lblInpDate_Hour.Text             = string.Empty;            // ��Ժʱ��(ʱ)
            lblInpDate_Minute.Text           = string.Empty;            // ��Ժʱ��(��)

            chkInpApproach_Outp.Checked      = false;                   // ��Ժ;��(����)
            chkInpApproach_Emergency.Checked = false;                   // ��Ժ;��(����)
            chkInpApproach_Shift.Checked     = false;                   // ��Ժ;��(ת��)

            chkChargeType_All.Checked        = false;                   // ����֧��(����ҽ��)
            chkChargeType_Big.Checked        = false;                   // ��ͳ��
            chkChargeType_Insur.Checked      = false;                   // ҽ�Ʊ���
            chkChargeType_Self.Checked       = false;                   // �Է�
            chkChargeType_Other.Checked      = false;                   // ����

            chkInpMode_Foot.Checked          = false;                   // ����
            chkInpMode_Wheel.Checked         = false;                   // ����
            chkInpMode_Car.Checked           = false;                   // ƽ��
            chkInpMode_Other.Checked         = false;                   // ����
        }
        
        
        /// <summary>
        /// ��ʾ����һ������
        /// </summary>
        private void showPatientCommInfo()
        {
            // ���û�������˳�
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                return;
            }
            
            DateTime Now = GVars.OracleAccess.GetSysDate();

            DataRow dr = dsPatient.Tables[0].Rows[0];
            string  val = string.Empty;
            
            txtPatientName.Text     = dr["NAME"].ToString();                                    // ����
            txtPatientGender.Text   = dr["SEX"].ToString();                                     // �Ա�
            
            val = dr["DATE_OF_BIRTH"].ToString();                                               // ����
            PersonCls person = new PersonCls();

            if (val.Length > 0)
            {
                txtPatientAge.Text = PersonCls.GetAge(DateTime.Parse(val), Now);
            }
            else
            {
                txtPatientAge.Text = string.Empty;
            }
            
            txtDeptName.Text         = dr["DEPT_NAME"].ToString();                              // �Ʊ�
            txtPatientBedLabel.Text  = dr["BED_LABEL"].ToString();                              // ����            
            txtCareer.Text           = dr["OCCUPATION"].ToString();                             // ְҵ
            txtNation.Text           = dr["NATION"].ToString();                                 // ����
            //txtPatientEducation.Text1 = dr["DEGREE"].ToString();                               // �Ļ��̶�
            txtInfoProvider.Text     = string.Empty;                                            // ��ʷ������
            txtInpDiagnose.Text      = string.Empty;                                            // ��Ժ���
            txtAssertDiagnose.Text   = dr["DIAGNOSIS"].ToString();                              // ȷ�����
            
            // ��Ժ����
            if (dr["ADMISSION_DATE_TIME"].ToString().Length > 0)
            {
                DateTime dt = (DateTime)dr["ADMISSION_DATE_TIME"];
                
                lblInpDate_Year.Text    = dt.Year.ToString();                                   // ��
                lblInpDate_Month.Text   = dt.Month.ToString();                                  // ��
                lblInpDate_Day.Text     = dt.Day.ToString();                                    // ��
                lblInpDate_Hour.Text    = dt.Hour.ToString();                                   // Сʱ
                lblInpDate_Minute.Text  = dt.Minute.ToString();                                 // ����
            }
            
            // ��Ժ;��
            val = dr["INP_APPROACH"].ToString();   // ��Ժ;��
            chkInpApproach_Outp.Checked         = (val.Equals(chkInpApproach_Outp.Text));       // ����
            chkInpApproach_Emergency.Checked    = (val.Equals(chkInpApproach_Emergency.Text));  // ����
            chkInpApproach_Shift.Checked        = (val.Equals(chkInpApproach_Shift.Text));      // ת��
            
            // ����֧��
            val = dr["CHARGE_TYPE"].ToString();    // ����֧��
            chkChargeType_All.Checked   = (val.Equals(chkChargeType_All.Text));                 // ��������            
            chkChargeType_Big.Checked   = (val.Equals(chkChargeType_Big.Text));                 // ��ͳ��
            chkChargeType_Insur.Checked = (val.Equals(chkChargeType_Insur.Text));               // ҽ�Ʊ���            
            chkChargeType_Self.Checked  = (val.Equals(chkChargeType_Self.Text));                // �Է�
            
            chkChargeType_Other.Checked = (chkChargeType_All.Checked == false 
                                            && chkChargeType_Big.Checked == false
                                            && chkChargeType_Insur.Checked == false
                                            && chkChargeType_Self.Checked == false);            // ����
            
            //txtInpMode.Text1          = string.Empty;                                          // ��Ժ��ʽ
        }
        
        
        #region ��������
        /// <summary>
        /// ������Ӧ�ڽ�����ʾ�ı�
        /// </summary>
        private void createNursingEvaluationTable()
        {
            dtShow = new DataTable();

            dtShow.Columns.Add("Temperature",       Type.GetType("System.String"));             // ����
            dtShow.Columns.Add("Pulse",             Type.GetType("System.String"));             // ����
            dtShow.Columns.Add("HeartRate",         Type.GetType("System.String"));             // ����
            dtShow.Columns.Add("Height",            Type.GetType("System.String"));             // ���
            dtShow.Columns.Add("Weight",            Type.GetType("System.String"));             // ����
            dtShow.Columns.Add("BloodPressureH",    Type.GetType("System.String"));             // Ѫѹ(��)
            dtShow.Columns.Add("BloodPressureL",    Type.GetType("System.String"));             // Ѫѹ(��)
            dtShow.Columns.Add("InpReason",         Type.GetType("System.String"));             // ��Ժԭ��
            dtShow.Columns.Add("IllHis",            Type.GetType("System.String"));             // ����ʷ
            dtShow.Columns.Add("AllergyHis",        Type.GetType("System.String"));             // ����ʷ
            dtShow.Columns.Add("Consciou",          Type.GetType("System.String"));             // ��ʶ״̬
            dtShow.Columns.Add("SighBug",           Type.GetType("System.String"));             // ����
            dtShow.Columns.Add("AuditionBug",       Type.GetType("System.String"));             // �����ϰ�
            dtShow.Columns.Add("AchePart",          Type.GetType("System.String"));             // ��ʹ��λ
            dtShow.Columns.Add("AcheClass",         Type.GetType("System.String"));             // ��ʹ����
            dtShow.Columns.Add("Diet",              Type.GetType("System.String"));             // ��ʳ
            dtShow.Columns.Add("DietCure",          Type.GetType("System.String"));             // ������ʳ
            dtShow.Columns.Add("Sleep",             Type.GetType("System.String"));             // ˯��
            dtShow.Columns.Add("Stool",             Type.GetType("System.String"));             // ���
            dtShow.Columns.Add("StoolOther",        Type.GetType("System.String"));             // �������
            dtShow.Columns.Add("Pee",               Type.GetType("System.String"));             // С��
            dtShow.Columns.Add("PeeOther",          Type.GetType("System.String"));             // С������
            dtShow.Columns.Add("Skin",              Type.GetType("System.String"));             // Ƥ�����
            dtShow.Columns.Add("SkinPart",          Type.GetType("System.String"));             // Ƥ�λ
            dtShow.Columns.Add("BedSorePart",       Type.GetType("System.String"));             // �촯��λ
            dtShow.Columns.Add("BedSoreDegree",     Type.GetType("System.String"));             // �촯�̶�
            dtShow.Columns.Add("BedSoreLen",        Type.GetType("System.String"));             // �촯���
            dtShow.Columns.Add("BedSoreWidth",      Type.GetType("System.String"));             // �촯���
            dtShow.Columns.Add("SelfDependDegree",  Type.GetType("System.String"));             // ����̶�
            dtShow.Columns.Add("IllCognition",      Type.GetType("System.String"));             // ������ʶ
            dtShow.Columns.Add("Note",              Type.GetType("System.String"));             // ��ע
            dtShow.Columns.Add("Career",            Type.GetType("System.String"));             // ְҵ
            dtShow.Columns.Add("Degree",            Type.GetType("System.String"));             // �Ļ��̶�
            dtShow.Columns.Add("Provider",          Type.GetType("System.String"));             // ��ʷ������
            dtShow.Columns.Add("InpDiag",           Type.GetType("System.String"));             // ��Ժ���
            dtShow.Columns.Add("InpMode",           Type.GetType("System.String"));             // ��Ժ��ʽ
            dtShow.Columns.Add("Nurse",             Type.GetType("System.String"));             // ��ʿ
            dtShow.Columns.Add("RecDate",           Type.GetType("System.String"));             // ��¼����
        }        
        
        
        /// <summary>
        /// ��ȡ����������Ϣ
        /// </summary>        
        private void getEvaluationInfo()
        {
            string where = "PATIENT_ID = " + SqlConvert.SqlConvert(patientId)
                        + " AND VISIT_ID = " + SqlConvert.SqlConvert(visitId);
            
            // ��ȡ���˵Ĺ���ʷ(��HISΪ��)
            string allergyDrug = string.Empty;
            string sql = "SELECT ALERGY_DRUGS FROM PAT_VISIT WHERE " + where;
            if (GVars.OracleAccess.SelectValue(sql) == true)
            {
                allergyDrug = GVars.OracleAccess.GetResult(0);
            }
            
            // ����Sqlserver�е�����
            sql = "UPDATE INP_EVALUATION_REC SET ITEM_VALUE = " + SqlConvert.SqlConvert(allergyDrug);
            sql += " WHERE ITEM_NAME = 'AllergyHis' AND " + where;
            GVars.SqlserverAccess.ExecuteNoQuery(sql);
            
            // ȡ����
            sql = "SELECT * FROM INP_EVALUATION_REC WHERE " + where;
            dsEvaluation = GVars.SqlserverAccess.SelectData(sql, "INP_EVALUATION_REC");
            dsEvaluation.AcceptChanges();
        }
        
        
        /// <summary>
        /// ��ʼ������������Ϣ
        /// </summary>
        private void initEvaluationDisp()
        {
            txtTemperature.Text               = string.Empty;           // ����
            txtPulse.Text                     = string.Empty;           // ����
            txtHeartRate.Text                 = string.Empty;           // ����
            txtBloodPressureH.Text            = string.Empty;           // Ѫѹ��
            txtBloodPressureL.Text            = string.Empty;           // Ѫѹ��
            txtHeight.Text                    = string.Empty;           // ���
            txtWeight.Text                    = string.Empty;           // ����
            txtInpReason.Text                 = string.Empty;           // ��Ժԭ��
            
            chkIllHis_No.Checked              = false;                  // ����ʷ(��)
            chkIllHis.Checked                 = false;                  // ����ʷ            
            txtIllHis.Text                    = string.Empty;
            
            chkAllergyHis_No.Checked          = false;                  // ����ʷ(��)
            chkAllergyHis.Checked             = false;                  // ����ʷ
            txtAllergyHis.Text                = string.Empty;           // ����ʷ
            
            chkConscious_1.Checked            = false;                  // ��ʶ״̬1
            chkConscious_2.Checked            = false;                  // ��ʶ״̬2
            chkConscious_3.Checked            = false;                  // ��ʶ״̬3
            chkConscious_4.Checked            = false;                  // ��ʶ״̬4
            chkConscious_5.Checked            = false;                  // ��ʶ״̬5
            
            chkSighBug_No.Checked             = false;                  // �����ϰ�(��)
            chkSighBug.Checked                = false;                  // �����ϰ�
            cmbSighBug.SelectedIndex          = -1;
            
            chkAuditionBug_No.Checked         = false;                  // �����ϰ�(��)
            chkAuditionBug.Checked            = false;                  // �����ϰ�
            cmbAuditionBug.SelectedIndex      = -1;                     // �����ϰ�
            
            chkAche_No.Checked                = false;                  // ��ʹ(��)
            chkAche.Checked                   = false;                  // ��ʹ            
            txtAchePart.Text                  = string.Empty;           // ��λ
            txtAcheClass.Text                 = string.Empty;           // ����
            
            chkDiet_1.Checked                 = false;                  // ��ʳ1
            chkDiet_2.Checked                 = false;                  // ��ʳ2
            chkDiet_3.Checked                 = false;                  // ��ʳ3
            chkDiet_4.Checked                 = false;                  // ��ʳ4
            chkDiet_5.Checked                 = false;                  // ��ʳ5
            txtDiet.Text                      = string.Empty;
            
            chkSleep_0.Checked                = false;                  // ˯��״̬1
            chkSleep_1.Checked                = false;                  // ˯��״̬2
            chkSleep_2.Checked                = false;                  // ˯��״̬3
            chkSleep_3.Checked                = false;                  // ˯��״̬4
            chkSleep_4.Checked                = false;                  // ˯��״̬5
            chkSleep_5.Checked                = false;                  // ˯��״̬6
            
            chkStool_0.Checked                = false;                  // ���״̬1
            chkStool_1.Checked                = false;                  // ���״̬2
            chkStool_2.Checked                = false;                  // ���״̬3
            chkStool_3.Checked                = false;                  // ���״̬4
            txtStool.Text                     = string.Empty;           // ���
            
            chkPee_0.Checked                  = false;                  // С��
            chkPee_1.Checked                  = false;                  // С��״̬1
            chkPee_2.Checked                  = false;                  // С��״̬2
            chkPee_3.Checked                  = false;                  // С��״̬3
            chkPee_Other.Checked              = false;
            
            chkSkin_0.Checked                 = false;                  // Ƥ�����1
            chkSkin_1.Checked                 = false;                  // Ƥ�����2
            chkSkin_2.Checked                 = false;                  // Ƥ�����3
            chkSkin_3.Checked                 = false;                  // Ƥ�����4
            chkSkin_4.Checked                 = false;                  // Ƥ�����5
            chkSkin_5.Checked                 = false;                  // Ƥ�����6
            chkSkin_6.Checked                 = false;                  // Ƥ�����7
            txtSkin.Text                      = string.Empty;           // Ƥ��
            
            chkBedSore_No.Checked             = false;                  // �촯
            chkBedSore.Checked                = false;
            txtBedSorePart.Text               = string.Empty;           // ��λ
            cmbBedSore.SelectedIndex          = -1;                     // �̶�
            txtBedSoreLen.Text                = string.Empty;
            txtBedSoreWidth.Text              = string.Empty;
            
            chkSelfDepend.Checked             = false;                  // ����̶�1
            chkSelfDepend_1.Checked           = false;                  // ����̶�2
            chkSelfDepend_2.Checked           = false;                  // ����̶�3
            
            chkIllCognition.Checked           = false;                  // �Լ�������ʶ1
            chkIllCognition_1.Checked         = false;                  // �Լ�������ʶ2
            chkIllCognition_2.Checked         = false;                  // �Լ�������ʶ3
        }
        
        
        /// <summary>
        /// ��ʾ���˵���Ժ����������¼
        /// </summary>
        private void showNursingEvalRec()
        {
            string val = string.Empty;
            
            txtTemperature.Text      = drShow["Temperature"].ToString();                        // ����
            txtPulse.Text            = drShow["Pulse"].ToString();                              // ����
            txtHeartRate.Text        = drShow["HeartRate"].ToString();                          // ����
            txtHeight.Text           = drShow["Height"].ToString();                             // ���
            txtWeight.Text           = drShow["Weight"].ToString();                             // ����
            txtBloodPressureH.Text   = drShow["BloodPressureH"].ToString();                     // Ѫѹ(��)
            txtBloodPressureL.Text   = drShow["BloodPressureL"].ToString();                     // Ѫѹ(��)
            txtInpReason.Text        = drShow["InpReason"].ToString();                          // ��Ժԭ��
            
            val = drShow["IllHis"].ToString().Trim();                                           // ����ʷ
            chkIllHis_No.Checked     = (val.Length == 0);
            chkIllHis.Checked        = (val.Length > 0);
            txtIllHis.Text           = val;
            
            val = drShow["AllergyHis"].ToString().Trim();                                       // ����ʷ
            chkAllergyHis_No.Checked = (val.Length == 0);
            chkAllergyHis.Checked    = (val.Length > 0);
            txtAllergyHis.Text       = val;
            
            val = drShow["Consciou"].ToString();                                                // ��ʶ״̬
            chkConscious_1.Checked   = (val.Equals(chkConscious_1.Text));
            chkConscious_2.Checked   = (val.Equals(chkConscious_2.Text));
            chkConscious_3.Checked   = (val.Equals(chkConscious_3.Text));
            chkConscious_4.Checked   = (val.Equals(chkConscious_4.Text));
            chkConscious_5.Checked   = (val.Equals(chkConscious_5.Text));
            
            val = drShow["SighBug"].ToString().Trim();                                          // ����
            chkSighBug_No.Checked    = (val.Length == 0);
            chkSighBug.Checked       = (val.Length > 0);
            cmbSighBug.SelectedValue = val;
            
            val = drShow["AuditionBug"].ToString().Trim();                                      // �����ϰ�
            chkAuditionBug_No.Checked= (val.Length == 0);
            chkAuditionBug.Checked   = (val.Length > 0);
            cmbAuditionBug.SelectedValue = val;
            
            val = drShow["AchePart"].ToString().Trim();                                         // ��ʹ��λ
            chkAche_No.Checked       = (val.Length == 0);
            chkAche.Checked          = (val.Length > 0);
            txtAchePart.Text         = val;
            txtAcheClass.Text        = drShow["AcheClass"].ToString();                          // ��ʹ����
            
            val = drShow["Diet"].ToString().Trim();                                             // ��ʳ
            chkDiet_1.Checked        = (val.Equals(chkDiet_1.Text));
            chkDiet_2.Checked        = (val.Equals(chkDiet_2.Text));
            chkDiet_3.Checked        = (val.Equals(chkDiet_3.Text));
            chkDiet_4.Checked        = (val.Equals(chkDiet_4.Text));
            chkDiet_5.Checked        = (val.Equals(chkDiet_5.Text));
            txtDiet.Text             = drShow["DietCure"].ToString();                           // ������ʳ
            
            val = drShow["Sleep"].ToString().Trim();                                            // ˯��
            chkSleep_0.Checked       = (val.Length == 0 || val.Equals(chkSleep_0.Text));
            chkSleep_1.Checked       = (val.Equals(chkSleep_1.Text));
            chkSleep_2.Checked       = (val.Equals(chkSleep_2.Text));
            chkSleep_3.Checked       = (val.Equals(chkSleep_3.Text));
            chkSleep_4.Checked       = (val.Equals(chkSleep_4.Text));
            chkSleep_5.Checked       = (val.Equals(chkSleep_5.Text));
            
            val = drShow["Stool"].ToString().Trim();                                            // ���
            chkStool_0.Checked       = (val.Length == 0 || val.Equals(chkStool_0.Text));
            chkStool_1.Checked       = (val.Equals(chkStool_1.Text));
            chkStool_2.Checked       = (val.Equals(chkStool_2.Text));
            chkStool_3.Checked       = (val.Equals(chkStool_3.Text));
            txtStool.Text            = drShow["StoolOther"].ToString();                         // �������
            
            val = drShow["Pee"].ToString().Trim();                                              // С��
            chkPee_0.Checked         = (val.Length == 0 || val.Equals(chkPee_0.Text));
            chkPee_1.Checked         = (val.Equals(chkPee_1.Text));
            chkPee_2.Checked         = (val.Equals(chkPee_2.Text));
            chkPee_3.Checked         = (val.Equals(chkPee_3.Text));
            chkPee_Other.Checked     = (val.Equals(chkPee_Other.Text));
            txtPee.Text              = drShow["PeeOther"].ToString();                           // С������
            
            val = drShow["Skin"].ToString().Trim();                                             // Ƥ�����
            chkSkin_0.Checked        = (val.Length == 0 || val.Equals(chkSkin_0.Text));
            chkSkin_1.Checked        = (val.Equals(chkSkin_1.Text));
            chkSkin_2.Checked        = (val.Equals(chkSkin_2.Text));
            chkSkin_3.Checked        = (val.Equals(chkSkin_3.Text));
            chkSkin_4.Checked        = (val.Equals(chkSkin_4.Text));
            chkSkin_5.Checked        = (val.Equals(chkSkin_5.Text));
            chkSkin_6.Checked        = (val.Equals(chkSkin_6.Text));
            txtSkin.Text             = drShow["SkinPart"].ToString();                           // Ƥ�λ
            
            val = drShow["BedSorePart"].ToString().Trim();                                      // �촯��λ
            chkBedSore_No.Checked    = (val.Length == 0);
            chkBedSore.Checked       = (val.Length > 0);
            txtBedSorePart.Text      = val;
            cmbBedSore.SelectedValue = drShow["BedSoreDegree"].ToString();                      // �촯�̶�
            txtBedSoreLen.Text       = drShow["BedSoreLen"].ToString();                         // �촯���
            txtBedSoreWidth.Text     = drShow["BedSoreWidth"].ToString();                       // �촯���
            
            val = drShow["SelfDependDegree"].ToString().Trim();                                 // ����̶�
            chkSelfDepend.Checked    = (val.Equals(chkSelfDepend.Text));
            chkSelfDepend_1.Checked    = (val.Equals(chkSelfDepend_1.Text));
            chkSelfDepend_2.Checked    = (val.Equals(chkSelfDepend_2.Text));
            
            val = drShow["IllCognition"].ToString().Trim();                                     // ������ʶ
            chkIllCognition.Checked  = (val.Equals(chkIllCognition.Text));
            chkIllCognition_1.Checked  = (val.Equals(chkIllCognition_1.Text));
            chkIllCognition_2.Checked  = (val.Equals(chkIllCognition_2.Text));
            
            txtMemo.Text                = drShow["Note"].ToString();                            // ��ע
            
            txtCareer.Text              = drShow["Career"].ToString();                          // ְҵ            
            txtPatientEducation.Text    = drShow["Degree"].ToString();                          // �Ļ��̶�
            txtInfoProvider.Text        = drShow["Provider"].ToString();                        // ��ʷ������
            txtInpDiagnose.Text         = drShow["InpDiag"].ToString();                         // ��Ժ���
            
            val = drShow["InpMode"].ToString();                                                 // ��Ժ���
            chkInpMode_Foot.Checked     = (val.Equals(chkInpMode_Foot.Text));
            chkInpMode_Wheel.Checked    = (val.Equals(chkInpMode_Wheel.Text));
            chkInpMode_Car.Checked      = (val.Equals(chkInpMode_Car.Text));
            chkInpMode_Other.Checked    = (val.Equals(chkInpMode_Other.Text));
        }                
        
        
        /// <summary>
        /// �Ѽ�¼����ϳ�һ����¼
        /// </summary>
        private void formRecsInOne()
        { 
            // ת������
            if (dsEvaluation == null || dsEvaluation.Tables.Count == 0)
            {
                return;
            }

            string itemName = string.Empty;
            
            DataTable dt = dsEvaluation.Tables[0];
                        
            foreach(DataRow dr in dt.Rows)
            {
                itemName = dr["ITEM_NAME"].ToString();

                if (dtShow.Columns.IndexOf(itemName) >= 0)
                {
                    drShow[itemName]    = dr["ITEM_VALUE"].ToString();
                }
            }
            
            dtShow.Rows.Add(drShow);
        }               
        
        
        /// <summary>
        /// ���没�˵���Ժ����������¼
        /// </summary>
        private void getDisp_NursingEvalRec()
        {
            string val = string.Empty;
            drShow["Temperature"]      = (txtTemperature.Enabled ?      txtTemperature.Text:        string.Empty); // ����
            drShow["Pulse"]            = (txtPulse.Enabled ?            txtPulse.Text:              string.Empty); // ����
            drShow["HeartRate"]        = (txtHeartRate.Enabled ?        txtHeartRate.Text:          string.Empty); // ����
            drShow["Height"]           = (txtHeight.Enabled ?           txtHeight.Text:             string.Empty); // ���
            drShow["Weight"]           = (txtWeight.Enabled ?           txtWeight.Text:             string.Empty); // ����
            drShow["BloodPressureH"]   = (txtBloodPressureH.Enabled ?   txtBloodPressureH.Text:     string.Empty); // Ѫѹ(��)
            drShow["BloodPressureL"]   = (txtBloodPressureL.Enabled ?   txtBloodPressureL.Text:     string.Empty); // Ѫѹ(��)
            drShow["InpReason"]        = (txtInpReason.Enabled ?        txtInpReason.Text:          string.Empty); // ��Ժԭ��
            drShow["IllHis"]           = (txtIllHis.Enabled ?           txtIllHis.Text:             string.Empty); // ����ʷ
            drShow["AllergyHis"]       = (txtAllergyHis.Enabled ?       txtAllergyHis.Text:         string.Empty); // ����ʷ
            
            val = string.Empty;                                                                                     // ��ʶ״̬
            if (chkConscious_1.Checked == true) { val = chkConscious_1.Text; }
            if (chkConscious_2.Checked == true) { val = chkConscious_2.Text; }
            if (chkConscious_3.Checked == true) { val = chkConscious_3.Text; }
            if (chkConscious_4.Checked == true) { val = chkConscious_4.Text; }
            if (chkConscious_5.Checked == true) { val = chkConscious_5.Text; }            
            drShow["Consciou"]         = val;
            
            drShow["SighBug"]          = (cmbSighBug.Enabled ?          cmbSighBug.Text:            string.Empty); // ����
            drShow["AuditionBug"]      = (cmbAuditionBug.Enabled ?      cmbAuditionBug.Text:        string.Empty); // �����ϰ�
            drShow["AchePart"]         = (txtAchePart.Enabled ?         txtAchePart.Text:           string.Empty); // ��ʹ��λ
            drShow["AcheClass"]        = (txtAcheClass.Enabled ?        txtAcheClass.Text:          string.Empty); // ��ʹ����
            
            val = string.Empty;                                                                                     // ��ʳ
            if (chkDiet_1.Checked == true) { val = chkDiet_1.Text; }
            if (chkDiet_2.Checked == true) { val = chkDiet_2.Text; }
            if (chkDiet_3.Checked == true) { val = chkDiet_3.Text; }
            if (chkDiet_4.Checked == true) { val = chkDiet_4.Text; }
            if (chkDiet_5.Checked == true) { val = chkDiet_5.Text; }
            drShow["Diet"]              = val;
            
            drShow["DietCure"]          = (txtDiet.Enabled ?             txtDiet.Text:               string.Empty); // ������ʳ
            
            val = string.Empty;                                                                                     // ˯��
            if (chkSleep_0.Checked == true) { val = chkSleep_0.Text; }
            if (chkSleep_1.Checked == true) { val = chkSleep_1.Text; }
            if (chkSleep_2.Checked == true) { val = chkSleep_2.Text; }
            if (chkSleep_3.Checked == true) { val = chkSleep_3.Text; }
            if (chkSleep_4.Checked == true) { val = chkSleep_4.Text; }
            if (chkSleep_5.Checked == true) { val = chkSleep_5.Text; }            
            drShow["Sleep"]             = val; 
            
            val = string.Empty;                                                                                     // ���
            if (chkStool_0.Checked == true) { val = chkStool_0.Text; }
            if (chkStool_1.Checked == true) { val = chkStool_1.Text; }
            if (chkStool_2.Checked == true) { val = chkStool_2.Text; }
            if (chkStool_3.Checked == true) { val = chkStool_3.Text; }
            drShow["Stool"]             = val;
            
            drShow["StoolOther"]        = (txtStool.Enabled ?            txtStool.Text:              string.Empty); // �������
            
            val = string.Empty;                                                                                     // С��
            if (chkPee_0.Checked == true) { val = chkPee_0.Text; }
            if (chkPee_1.Checked == true) { val = chkPee_1.Text; }
            if (chkPee_2.Checked == true) { val = chkPee_2.Text; }
            if (chkPee_3.Checked == true) { val = chkPee_3.Text; }
            drShow["Pee"]               = val; 
            
            drShow["PeeOther"]          = (txtPee.Enabled ?              txtPee.Text:                string.Empty); // С������
            
            val = string.Empty;                                                                                     // Ƥ�����
            if (chkSkin_0.Checked == true) { val = chkSkin_0.Text; }
            if (chkSkin_1.Checked == true) { val = chkSkin_1.Text; }
            if (chkSkin_2.Checked == true) { val = chkSkin_2.Text; }
            if (chkSkin_3.Checked == true) { val = chkSkin_3.Text; }
            if (chkSkin_4.Checked == true) { val = chkSkin_4.Text; }
            if (chkSkin_5.Checked == true) { val = chkSkin_5.Text; }
            if (chkSkin_6.Checked == true) { val = chkSkin_6.Text; }            
            drShow["Skin"]              = val; 
            
            drShow["SkinPart"]         = (txtSkin.Enabled ?             txtSkin.Text:               string.Empty); // Ƥ�λ
            
            drShow["BedSorePart"]      = (txtBedSorePart.Enabled ?      txtBedSorePart.Text:        string.Empty); // �촯��λ
            drShow["BedSoreDegree"]    = (cmbBedSore.Enabled?           cmbBedSore.Text:            string.Empty); // �촯�̶�
            drShow["BedSoreLen"]       = (txtBedSoreLen.Enabled ?       txtBedSoreLen.Text:         string.Empty); // �촯���
            drShow["BedSoreWidth"]     = (txtBedSoreWidth.Enabled ?     txtBedSoreWidth.Text:       string.Empty); // �촯���
            
            val = string.Empty;                                                                                     // ����̶�
            if (chkSelfDepend.Checked == true) { val = chkSelfDepend.Text; }
            if (chkSelfDepend_1.Checked == true) { val = chkSelfDepend_1.Text; }
            if (chkSelfDepend_2.Checked == true) { val = chkSelfDepend_2.Text; }                        
            drShow["SelfDependDegree"] = val; 
            
            val = string.Empty;                                                                                     // ������ʶ
            if (chkIllCognition.Checked == true) { val = chkIllCognition.Text; }
            if (chkIllCognition_1.Checked == true) { val = chkIllCognition_1.Text; }
            if (chkIllCognition_2.Checked == true) { val = chkIllCognition_2.Text; }
            
            drShow["IllCognition"]      = val; 
            
            drShow["Note"]              = txtMemo.Text.Trim();                                                      // ��ע
            drShow["Career"]            = txtCareer.Text.Trim();                                                    // ְҵ
            drShow["Degree"]            = txtPatientEducation.Text.Trim();                                          // �Ļ��̶�
            drShow["Provider"]          = txtInfoProvider.Text.Trim();                                              // ��ʷ������
            drShow["InpDiag"]           = txtInpDiagnose.Text.Trim();                                               // ��Ժ���
            
            val = string.Empty;                                                                                     // ��Ժ��ʽ
            if (chkInpMode_Foot.Checked == true) { val = chkInpMode_Foot.Text; } 
            if (chkInpMode_Car.Checked == true) { val = chkInpMode_Car.Text; } 
            if (chkInpMode_Wheel.Checked == true) { val = chkInpMode_Wheel.Text; } 
            if (chkInpMode_Other.Checked == true) { val = chkInpMode_Other.Text; }             
            drShow["InpMode"] = val;
            
            if (drShow["Nurse"].ToString().Length == 0)
            {
                drShow["Nurse"] = GVars.User.Name;
            }
            
            if (drShow["RecDate"].ToString().Length == 0)
            {
                drShow["RecDate"] = GVars.OracleAccess.GetSysDate().ToString(ComConst.FMT_DATE.LONG);
            }
        }
        
        
        /// <summary>
        /// ��һ����¼��ɢ�ɼ�¼��
        /// </summary>
        private DataTable disperseOneInRecs()
        {
            DataTable dtTemp    = dsEvaluation.Tables[0].Clone();
            string    val       = string.Empty;
            
            val = drShow["Temperature"].ToString();                                             // ����
            addNewRec(ref dtTemp, "Temperature", val, "��");
            
            val = drShow["Pulse"].ToString();                                                   // ����
            addNewRec(ref dtTemp, "Pulse", val, "��/��");
            
            val = drShow["HeartRate"].ToString();                                               // ����
            addNewRec(ref dtTemp, "HeartRate", val, "��/��");
            
            val = drShow["Height"].ToString();                                                  // ���
            addNewRec(ref dtTemp, "Height", val, "cm");
            
            val = drShow["Weight"].ToString();                                                  // ����
            addNewRec(ref dtTemp, "Weight", val, "kg");

            val = drShow["BloodPressureH"].ToString();                                          // Ѫѹ(��)
            addNewRec(ref dtTemp, "BloodPressureH", val, "mmHg");

            val = drShow["BloodPressureL"].ToString();                                          // Ѫѹ(��)
            addNewRec(ref dtTemp, "BloodPressureL", val, "mmHg");

            val = drShow["InpReason"].ToString();                                               // ��Ժԭ��
            addNewRec(ref dtTemp, "InpReason", val, string.Empty);

            val = drShow["IllHis"].ToString();                                                  // ����ʷ
            addNewRec(ref dtTemp, "IllHis", val, string.Empty);

            val = drShow["AllergyHis"].ToString();                                              // ����ʷ
            addNewRec(ref dtTemp, "AllergyHis", val, string.Empty);

            val = drShow["Consciou"].ToString();                                                // ��ʶ״̬
            addNewRec(ref dtTemp, "Consciou", val, string.Empty);

            val = drShow["SighBug"].ToString();                                                 // ����
            addNewRec(ref dtTemp, "SighBug", val, string.Empty);

            val = drShow["AuditionBug"].ToString();                                             // �����ϰ�
            addNewRec(ref dtTemp, "AuditionBug", val, string.Empty);

            val = drShow["AchePart"].ToString();                                                // ��ʹ��λ
            addNewRec(ref dtTemp, "AchePart", val, string.Empty);

            val = drShow["AcheClass"].ToString();                                               // ��ʹ����
            addNewRec(ref dtTemp, "AcheClass", val, string.Empty);

            val = drShow["Diet"].ToString();                                                    // ��ʳ
            addNewRec(ref dtTemp, "Diet", val, string.Empty);

            val = drShow["DietCure"].ToString();                                                // ������ʳ
            addNewRec(ref dtTemp, "DietCure", val, string.Empty);
            
            val = drShow["Sleep"].ToString();                                                   // ˯��
            addNewRec(ref dtTemp, "Sleep", val, string.Empty);
            
            val = drShow["Stool"].ToString();                                                   // ���
            addNewRec(ref dtTemp, "Stool", val, string.Empty);
            
            val = drShow["StoolOther"].ToString();                                              // �������
            addNewRec(ref dtTemp, "StoolOther", val, string.Empty);
            
            val = drShow["Pee"].ToString();                                                     // С��
            addNewRec(ref dtTemp, "Pee", val, string.Empty);
            
            val = drShow["PeeOther"].ToString();                                                // С������
            addNewRec(ref dtTemp, "PeeOther", val, string.Empty);
            
            val = drShow["Skin"].ToString();                                                    // Ƥ�����
            addNewRec(ref dtTemp, "Skin", val, string.Empty);
            
            val = drShow["SkinPart"].ToString();                                                // Ƥ�λ
            addNewRec(ref dtTemp, "SkinPart", val, string.Empty);

            val = drShow["BedSorePart"].ToString();                                             // �촯��λ
            addNewRec(ref dtTemp, "BedSorePart", val, string.Empty);

            val = drShow["BedSoreDegree"].ToString();                                           // �촯�̶�
            addNewRec(ref dtTemp, "BedSoreDegree", val, string.Empty);

            val = drShow["BedSoreLen"].ToString();                                              // �촯���
            addNewRec(ref dtTemp, "BedSoreLen", val, "cm");

            val = drShow["BedSoreWidth"].ToString();                                            // �촯���
            addNewRec(ref dtTemp, "BedSoreWidth", val, "cm");

            val = drShow["SelfDependDegree"].ToString();                                        // ����̶�
            addNewRec(ref dtTemp, "SelfDependDegree", val, string.Empty);
            
            val = drShow["IllCognition"].ToString();                                            // ������ʶ
            addNewRec(ref dtTemp, "IllCognition", val, string.Empty);
            
            val = drShow["Note"].ToString();                                                    // ��ע
            addNewRec(ref dtTemp, "Note", val, string.Empty);
            
            val = drShow["Career"].ToString();                                                  // ְҵ
            addNewRec(ref dtTemp, "Career", val, string.Empty);
            
            val = drShow["Degree"].ToString();                                                  // �Ļ��̶�
            addNewRec(ref dtTemp, "Degree", val, string.Empty);
            
            val = drShow["Provider"].ToString();                                                // ��ʷ������
            addNewRec(ref dtTemp, "Provider", val, string.Empty);
            
            val = drShow["InpDiag"].ToString();                                                 // ��Ժ���
            addNewRec(ref dtTemp, "InpDiag", val, string.Empty);
            
            val = drShow["InpMode"].ToString();                                                 // ��Ժ���
            addNewRec(ref dtTemp, "InpMode", val, string.Empty);
            
            val = drShow["Nurse"].ToString();                                                   // ��ʿ
            addNewRec(ref dtTemp, "Nurse", val, string.Empty);
            
            val = drShow["RecDate"].ToString();                                                 // ��¼ʱ��
            addNewRec(ref dtTemp, "RecDate", val, string.Empty);
            
            return dtTemp;
        }        
        
        
        private void addNewRec(ref DataTable dt, string itemName, string itemValue, string itemUnit)
        { 
            DataRow drNew = dt.NewRow();
            
            drNew["WARD_CODE"]  = GVars.User.DeptCode;
            drNew["PATIENT_ID"] = patientId;
            drNew["VISIT_ID"]   = visitId;
            drNew["ITEM_NAME"]  = itemName;
            drNew["ITEM_UNIT"]  = itemUnit;
            drNew["ITEM_VALUE"] = itemValue;

            dt.Rows.Add(drNew);
        }                
        
        
        /// <summary>
        /// ���没�˵���Ժ����������¼
        /// </summary>
        /// <param name="dsEduRec"></param>
        /// <returns></returns>
        public bool saveNursingEvalRec(DataSet dsNursingEvalRec, string patientId, string visitId)
        { 
            // �������
            if (dsNursingEvalRec == null || dsNursingEvalRec.Tables.Count == 0)
            {
                return true;
            }
            
            // ���浽����
            string sql          = string.Empty;
            string where        = string.Empty;
            string tableName    = "INP_EVALUATION_REC";
            string val          = string.Empty;
            
            SqlConvert.DbField[] arrF    = new SqlConvert.DbField[6];
            ArrayList               arrSql  = new ArrayList();
            
            foreach (DataRow dr in dsNursingEvalRec.Tables[0].Rows)
            {
                int i = -1;
                
                arrF[++i] = SqlConvert.GetDbField_Sql("PATIENT_ID", patientId, DbFieldType.STR);
                arrF[++i] = SqlConvert.GetDbField_Sql("VISIT_ID", visitId, DbFieldType.STR);
                arrF[++i] = SqlConvert.GetDbField_Sql("ITEM_NAME", dr["ITEM_NAME"].ToString(), DbFieldType.STR);
                
                arrF[++i] = SqlConvert.GetDbField_Sql("WARD_CODE", dr["WARD_CODE"].ToString(), DbFieldType.STR);                
                arrF[++i] = SqlConvert.GetDbField_Sql("ITEM_UNIT", dr["ITEM_UNIT"].ToString(), DbFieldType.STR);
                arrF[++i] = SqlConvert.GetDbField_Sql("ITEM_VALUE", dr["ITEM_VALUE"].ToString(), DbFieldType.STR);
                                
                where = SqlConvert.getFieldValuePairAssert(arrF, 3);
                
                // �жϼ�¼�Ƿ����
                sql = "SELECT PATIENT_ID FROM " + tableName + " WHERE " + where;
                
                bool blnExist = GVars.SqlserverAccess.SelectValue(sql);
                
                if (blnExist == true)
                {
                    sql = "UPDATE " + tableName + " SET ";
                    sql += "WARD_CODE = " + SqlConvert.SqlConvert(dr["WARD_CODE"].ToString());
                    sql += ", ITEM_UNIT = " + SqlConvert.SqlConvert(dr["ITEM_UNIT"].ToString());
                    sql += ", ITEM_VALUE = " + SqlConvert.SqlConvert(dr["ITEM_VALUE"].ToString());
                    sql += " WHERE " + where;
                    
                    arrSql.Add(sql);
                }
                else
                {
                    sql = SqlConvert.GetSqlInsert(tableName, arrF, 6);
                    
                    arrSql.Add(sql);
                }
            }

            GVars.SqlserverAccess.ExecuteNoQuery(ref arrSql);
            
            // �������ʷ��HIS��
            sql = "UPDATE PAT_VISIT SET ALERGY_DRUGS = " + SqlConvert.SqlConvert(txtAllergyHis.Text.Trim())
               + " WHERE PATIENT_ID = " + SqlConvert.SqlConvert(patientId)
               + " AND VISIT_ID = " + SqlConvert.SqlConvert(visitId);
            
            GVars.OracleAccess.ExecuteNoQuery(sql);
            
            return true;
        }        
        #endregion
        
        
        #region ��Ժ����
        /// <summary>
        /// ��ȡ���˵Ľ���������Ŀ
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        private DataSet getPatientEduRec(string patientId, string visitId)
        {
            string sql = string.Empty;
            
            sql += "SELECT PAT_EDUCATION_ITEMS.EDU_ITEM_NAME ";
            sql += "FROM PAT_EDUCATION_REC INNER JOIN ";
            sql +=     "PAT_EDUCATION_ITEMS ON ";
            sql +=     "PAT_EDUCATION_REC.EDU_ITEM_CODE = PAT_EDUCATION_ITEMS.EDU_ITEM_CODE ";
            sql += "WHERE PAT_EDUCATION_REC.PATIENT_ID = " + SqlConvert.SqlConvert(patientId);
            sql +=     " AND PAT_EDUCATION_REC.VISIT_ID = " + SqlConvert.SqlConvert(visitId);
            
            return GVars.SqlserverAccess.SelectData(sql, "PAT_EDUCATION_REC");
        }
        
        
        /// <summary>
        /// ��ʾ���˵Ľ���������Ŀ
        /// </summary>
        private void showPatientEduRec()
        {
            chkIntroDuct_1.Checked = false;
            chkIntroDuct_2.Checked = false;
            chkIntroDuct_3.Checked = false;
            chkIntroDuct_4.Checked = false;
            chkIntroDuct_5.Checked = false;
            chkIntroDuct_6.Checked = false;
            chkIntroDuct_7.Checked = false;
            chkIntroDuct_8.Checked = false;
            
            if (dsEduRec == null || dsEduRec.Tables.Count == 0)
            {
                return;
            }
            
            foreach(DataRow dr in dsEduRec.Tables[0].Rows)
            {
                string val = dr["EDU_ITEM_NAME"].ToString();
                
                if (val.Equals(chkIntroDuct_1.Text) == true) { chkIntroDuct_1.Checked = true; }
                if (val.Equals(chkIntroDuct_2.Text) == true) { chkIntroDuct_2.Checked = true; }
                if (val.Equals(chkIntroDuct_3.Text) == true) { chkIntroDuct_3.Checked = true; }
                if (val.Equals(chkIntroDuct_4.Text) == true) { chkIntroDuct_4.Checked = true; }
                if (val.Equals(chkIntroDuct_5.Text) == true) { chkIntroDuct_5.Checked = true; }
                if (val.Equals(chkIntroDuct_6.Text) == true) { chkIntroDuct_6.Checked = true; }
                if (val.Equals(chkIntroDuct_7.Text) == true) { chkIntroDuct_7.Checked = true; }
                if (val.Equals(chkIntroDuct_8.Text) == true) { chkIntroDuct_8.Checked = true; }
            }
        }
        
        
        /// <summary>
        /// ���没�˵Ľ���������¼
        /// </summary>
        private void savePatientEduRec()
        {
            // �жϼ�¼�Ƿ����
            string sqlSel = "SELECT EDU_ITEM_CODE FROM PAT_EDUCATION_REC WHERE EDU_ITEM_CODE = '{0}'";
            string sqlDel = "DELETE FROM PAT_EDUCATION_REC WHERE EDU_ITEM_CODE = '{0}'";
            string sqlIns = "INSERT INTO PAT_EDUCATION_REC VALUES("  
                + SqlConvert.SqlConvert(patientId) + ", "
                + SqlConvert.SqlConvert(visitId) + ", "
                + "'{0}', "
                + SqlConvert.SqlConvert(GVars.User.Name) + ", "
                + "GETDATE(), "
                + "GETDATE() "
                + ")";
            ArrayList arrSql = new ArrayList(8);
            
            string sql = string.Empty;
            
            sql = saveOneEduRec(chkIntroDuct_1.Tag.ToString(), chkIntroDuct_1.Checked, ref sqlSel, ref sqlDel, ref sqlIns);
            if (sql.Length > 0) { arrSql.Add(sql); }
            
            sql = saveOneEduRec(chkIntroDuct_2.Tag.ToString(), chkIntroDuct_2.Checked, ref sqlSel, ref sqlDel, ref sqlIns);
            if (sql.Length > 0) { arrSql.Add(sql); }
            
            sql = saveOneEduRec(chkIntroDuct_3.Tag.ToString(), chkIntroDuct_3.Checked, ref sqlSel, ref sqlDel, ref sqlIns);
            if (sql.Length > 0) { arrSql.Add(sql); }
            
            sql = saveOneEduRec(chkIntroDuct_4.Tag.ToString(), chkIntroDuct_4.Checked, ref sqlSel, ref sqlDel, ref sqlIns);
            if (sql.Length > 0) { arrSql.Add(sql); }
            
            sql = saveOneEduRec(chkIntroDuct_5.Tag.ToString(), chkIntroDuct_5.Checked, ref sqlSel, ref sqlDel, ref sqlIns);
            if (sql.Length > 0) { arrSql.Add(sql); }
            
            sql = saveOneEduRec(chkIntroDuct_6.Tag.ToString(), chkIntroDuct_6.Checked, ref sqlSel, ref sqlDel, ref sqlIns);
            if (sql.Length > 0) { arrSql.Add(sql); }
            
            sql = saveOneEduRec(chkIntroDuct_7.Tag.ToString(), chkIntroDuct_7.Checked, ref sqlSel, ref sqlDel, ref sqlIns);
            if (sql.Length > 0) { arrSql.Add(sql); }
            
            sql = saveOneEduRec(chkIntroDuct_8.Tag.ToString(), chkIntroDuct_8.Checked, ref sqlSel, ref sqlDel, ref sqlIns);
            if (sql.Length > 0) { arrSql.Add(sql); }
            
            GVars.SqlserverAccess.ExecuteNoQuery(ref arrSql);
        }
        
        
        /// <summary>
        /// ����һ������������¼
        /// </summary>
        /// <param name="itemCode">��Ŀ����</param>
        /// <param name="blnDone">�Ƿ����</param>
        /// <returns></returns>
        private string saveOneEduRec(string itemCode, bool blnDone, ref string sqlSel, ref string sqlDel, ref string sqlIns)
        {
            if (itemCode.Length == 0)
            {
                return string.Empty;
            }
            
            string sql = string.Format(sqlSel, itemCode);
            
            // �����ɾ��
            if (GVars.SqlserverAccess.SelectValue(sql) == true)
            {
                if (blnDone == false)
                {
                    return string.Format(sqlDel, itemCode);
                }
            }
            else
            {
                if (blnDone == true)
                {
                    return string.Format(sqlIns, itemCode);
                }
            }
            
            return string.Empty;
        }
        
        
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        private void initEduItem()
        {
            chkIntroDuct_1.Tag = string.Empty;
            chkIntroDuct_2.Tag = string.Empty;
            chkIntroDuct_3.Tag = string.Empty;
            chkIntroDuct_4.Tag = string.Empty;
            chkIntroDuct_5.Tag = string.Empty;
            chkIntroDuct_6.Tag = string.Empty;
            chkIntroDuct_7.Tag = string.Empty;
            chkIntroDuct_8.Tag = string.Empty;
            
            // ��ȡ����������Ŀ����
            string sql = "SELECT * FROM PAT_EDUCATION_ITEMS WHERE FLG = '1'";
            DataSet dsItem = GVars.SqlserverAccess.SelectData(sql);
            
            if (dsItem == null || dsItem.Tables.Count == 0)
            {
                return;
            }
            
            foreach(DataRow dr in dsItem.Tables[0].Rows)
            {
                string itemName = dr["EDU_ITEM_NAME"].ToString();
                string itemCode = dr["EDU_ITEM_CODE"].ToString();
                
                if (itemName.Equals(chkIntroDuct_1.Text) == true) { chkIntroDuct_1.Tag = itemCode; }
                if (itemName.Equals(chkIntroDuct_2.Text) == true) { chkIntroDuct_2.Tag = itemCode; }
                if (itemName.Equals(chkIntroDuct_3.Text) == true) { chkIntroDuct_3.Tag = itemCode; }
                if (itemName.Equals(chkIntroDuct_4.Text) == true) { chkIntroDuct_4.Tag = itemCode; }
                if (itemName.Equals(chkIntroDuct_5.Text) == true) { chkIntroDuct_5.Tag = itemCode; }
                if (itemName.Equals(chkIntroDuct_6.Text) == true) { chkIntroDuct_6.Tag = itemCode; }
                if (itemName.Equals(chkIntroDuct_7.Text) == true) { chkIntroDuct_7.Tag = itemCode; }
                if (itemName.Equals(chkIntroDuct_8.Text) == true) { chkIntroDuct_8.Tag = itemCode; }
            }
        }
        #endregion
        
        
        #region ���ݼ��
        /// <summary>
        /// ���¼��
        /// </summary>
        /// <remarks>���µ���Ч��Χ: 33 �C 42.4</remarks>
        /// <param name="temperature">����</param>
        /// <returns>TRUE: ͨ�����; FALSE: û��ͨ�����</returns>
        private bool chkBodyTemperature(float temperature)
        {
            if (temperature < 33 || temperature > 42.4)
            {
                GVars.Msg.MsgId = "E00016";                             // {0}����ȷ, ��Ч��ΧΪ {1} - {2}
                
                GVars.Msg.MsgContent.Add("����");
                GVars.Msg.MsgContent.Add("33");
                GVars.Msg.MsgContent.Add("42.4");

                return false;
            }

            return true;
        }
        
        
        /// <summary>
        /// ���ʼ��
        /// </summary>
        /// <remarks>���ʵ�Ч��Χ: 0 �C 188</remarks>
        /// <param name="rate">����</param>
        /// <returns>TRUE: ͨ�����; FALSE: û��ͨ�����</returns>
        private bool chkHeartRate(float rate)
        {
            if (rate < 0 || rate > 188)
            {
                GVars.Msg.MsgId = "E00016";                             // {0}����ȷ, ��Ч��ΧΪ {1} - {2}

                GVars.Msg.MsgContent.Add("����");
                GVars.Msg.MsgContent.Add("0");
                GVars.Msg.MsgContent.Add("188");

                return false;
            }

            return true;
        }
        
        
        /// <summary>
        /// �������
        /// </summary>
        /// <remarks>������Ч��Χ: 0 �C 188</remarks>
        /// <param name="rate">����</param>
        /// <returns>TRUE: ͨ�����; FALSE: û��ͨ�����</returns>
        private bool chkPulse(float pulse)
        {
            if (pulse < 0 || pulse > 188)
            {
                GVars.Msg.MsgId = "E00016";                             // {0}����ȷ, ��Ч��ΧΪ {1} - {2}

                GVars.Msg.MsgContent.Add("����");
                GVars.Msg.MsgContent.Add("0");
                GVars.Msg.MsgContent.Add("188");
                
                return false;
            }

            return true;
        }
        
        
        /// <summary>
        /// �������
        /// </summary>
        /// <remarks>������Ч��Χ: 0 �C 188</remarks>
        /// <param name="rate">����</param>
        /// <returns>TRUE: ͨ�����; FALSE: û��ͨ�����</returns>
        private bool chkBreath(float breath)
        {
            if (breath < 0 || breath > 188)
            {
                GVars.Msg.MsgId = "E00016";                             // {0}����ȷ, ��Ч��ΧΪ {1} - {2}
                
                GVars.Msg.MsgContent.Add("����");
                GVars.Msg.MsgContent.Add("0");
                GVars.Msg.MsgContent.Add("188");
                
                return false;
            }
            
            return true;
        }                
        #endregion
        
        
        #region ��ӡ
		/// <summary>
		/// ��Excelģ���ӡ���Ƚ��ʺ��״򡢸�ʽ��ͳ�Ʒ�������ͼ�η������Զ����ӡ
		/// </summary>
		/// <remarks>��Excel��ӡ������Ϊ���򿪡�д���ݡ���ӡԤ�����ر�</remarks>
		private void ExcelTemplatePrint()
		{
			string strExcelTemplateFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Template\\��Ժ����������.xls");
            
			excelAccess.Open(strExcelTemplateFile);				//��ģ���ļ�
			excelAccess.IsVisibledExcel = true;
			excelAccess.FormCaption = string.Empty;	
		    
		    // ��ȡ�����ļ�
		    string iniFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Template\\��Ժ����������.ini");
		    if (System.IO.File.Exists(iniFile) == true)
		    {
		        StreamReader sr = new StreamReader(iniFile);
		        string line = string.Empty;
		        int row = 0;
		        int col = 0;
		        string fieldName = string.Empty;
		        
		        DataRow drPrint = dtPrint.Rows[0];
		        
		        while((line = sr.ReadLine()) != null)
		        {
		            // ��ȡ����
                    fieldName = getParts(line, ref row, ref col);
		            
		            if (fieldName.Length > 0)
		            {
		                excelAccess.SetCellText(row, col, drPrint[fieldName].ToString());
		            }
		        }
		    }
		    
			//excel.Print();				           //��ӡ
			excelAccess.PrintPreview();			       //Ԥ��
            
			excelAccess.Close(false);				   //�رղ��ͷ�			
		}
		
		
		/// <summary>
		/// ������ӡ���
		/// </summary>
		private void createTableForPrint()
		{
		    dtPrint = new DataTable();
		    
            dtPrint.Columns.Add("INP_NO",               Type.GetType("System.String"));             //סԺ��
            dtPrint.Columns.Add("PATIENT_NAME",         Type.GetType("System.String"));             //����
            dtPrint.Columns.Add("PATIENT_SEX",          Type.GetType("System.String"));             //�Ա�
            dtPrint.Columns.Add("PATIENT_AGE",          Type.GetType("System.String"));             //����
            dtPrint.Columns.Add("PATIENT_DEPT",         Type.GetType("System.String"));             //�Ʊ�
            dtPrint.Columns.Add("PATIENT_BED_LABEL",    Type.GetType("System.String"));             //����
            dtPrint.Columns.Add("PATIENT_CAREER",       Type.GetType("System.String"));             //ְҵ
            dtPrint.Columns.Add("PATIENT_NATION",       Type.GetType("System.String"));             //����
            dtPrint.Columns.Add("PATIENT_DEGREE",       Type.GetType("System.String"));             //�Ļ��̶�
            dtPrint.Columns.Add("INPPROVIDER",          Type.GetType("System.String"));             //��ʷ������
            dtPrint.Columns.Add("INPDIAG",              Type.GetType("System.String"));             //��Ժ���
            dtPrint.Columns.Add("ASSERTDIAG",           Type.GetType("System.String"));             // ȷ�����
            dtPrint.Columns.Add("INP_DATE_YEAR",        Type.GetType("System.String"));             //��
            dtPrint.Columns.Add("INP_DATE_MONTH",       Type.GetType("System.String"));             //��
            dtPrint.Columns.Add("INP_DATE_DAY",         Type.GetType("System.String"));             //��
            dtPrint.Columns.Add("INP_DATE_HOUR",        Type.GetType("System.String"));             // ʱ
            dtPrint.Columns.Add("INP_DATE_MINUTE",      Type.GetType("System.String"));             //��
            dtPrint.Columns.Add("APPROACH_CLINIC",      Type.GetType("System.String"));             //��Ժ;��-����
            dtPrint.Columns.Add("APPROACH_EMERGENCY",   Type.GetType("System.String"));             //��Ժ;��-����
            dtPrint.Columns.Add("APPROACH_SHIFT",       Type.GetType("System.String"));             //��Ժ;��-ת��
            dtPrint.Columns.Add("CHARGE0",              Type.GetType("System.String"));             //����֧��-����ҽ��
            dtPrint.Columns.Add("CHARGE1",              Type.GetType("System.String"));             //����֧��-��ͳ��
            dtPrint.Columns.Add("CHARGE2",              Type.GetType("System.String"));             //����֧��-ҽ�Ʊ���
            dtPrint.Columns.Add("CHARGE3",              Type.GetType("System.String"));             //����֧��-�Է�
            dtPrint.Columns.Add("CHARGE4",              Type.GetType("System.String"));             //����֧��-����
            dtPrint.Columns.Add("INP_MODE0",            Type.GetType("System.String"));             //��Ժ��ʽ-����
            dtPrint.Columns.Add("INP_MODE1",            Type.GetType("System.String"));             //��Ժ��ʽ-����
            dtPrint.Columns.Add("INP_MODE2",            Type.GetType("System.String"));             //��Ժ��ʽ-ƽ��
            dtPrint.Columns.Add("INP_MODE3",            Type.GetType("System.String"));             //��Ժ��ʽ-����
            dtPrint.Columns.Add("TEMPERATURE",          Type.GetType("System.String"));             //����
            dtPrint.Columns.Add("PULSE",                Type.GetType("System.String"));             //����
            dtPrint.Columns.Add("HEAR_RATE",            Type.GetType("System.String"));             //����
            dtPrint.Columns.Add("BLOOD_PRESSURE",       Type.GetType("System.String"));             //Ѫѹ
            dtPrint.Columns.Add("HEIGHT",               Type.GetType("System.String"));             //���
            dtPrint.Columns.Add("WEIGHT",               Type.GetType("System.String"));             //����
            dtPrint.Columns.Add("INP_REASON",           Type.GetType("System.String"));             //��Ժԭ��
            dtPrint.Columns.Add("ILLHIS_N",             Type.GetType("System.String"));             //����ʷ-��
            dtPrint.Columns.Add("ILLHIS_Y",             Type.GetType("System.String"));             //��סʷ-��
            dtPrint.Columns.Add("ILLHIS",               Type.GetType("System.String"));             //��סʷ
            dtPrint.Columns.Add("ALLERGYHIS_N",         Type.GetType("System.String"));             //����ʷ-��
            dtPrint.Columns.Add("ALLERGYHIS_Y",         Type.GetType("System.String"));             //����ʷ-��
            dtPrint.Columns.Add("ALLERGYHIS",           Type.GetType("System.String"));             //����ʷ
            dtPrint.Columns.Add("CONSCIOUS0",           Type.GetType("System.String"));             //��ʶ״̬-����
            dtPrint.Columns.Add("CONSCIOUS1",           Type.GetType("System.String"));             //��ʶ״̬-����
            dtPrint.Columns.Add("CONSCIOUS2",           Type.GetType("System.String"));             //��ʶ״̬-��˯
            dtPrint.Columns.Add("CONSCIOUS3",           Type.GetType("System.String"));             //��ʶ״̬-��˯
            dtPrint.Columns.Add("CONSCIOUS4",           Type.GetType("System.String"));             //��ʶ״̬-����
            dtPrint.Columns.Add("SIGHBUG_N",            Type.GetType("System.String"));             //�����ϰ�-��
            dtPrint.Columns.Add("SIGHBUG_Y",            Type.GetType("System.String"));             //�����ϰ�-��
            dtPrint.Columns.Add("SIGHBUG",              Type.GetType("System.String"));             //�����ϰ�-��/��
            dtPrint.Columns.Add("AUDITION_N",           Type.GetType("System.String"));             //�����ϰ�-��
            dtPrint.Columns.Add("AUDITION_Y",           Type.GetType("System.String"));             //�����ϰ�-��
            dtPrint.Columns.Add("AUDITION",             Type.GetType("System.String"));             //�����ϰ�-��/��
            dtPrint.Columns.Add("ACHE_N",               Type.GetType("System.String"));             //��ʹ-��
            dtPrint.Columns.Add("ACHE_Y",               Type.GetType("System.String"));             //��ʹ-��
            dtPrint.Columns.Add("ACHE_PART",            Type.GetType("System.String"));             //��ʹ-��λ
            dtPrint.Columns.Add("ACHE_CLASS",           Type.GetType("System.String"));             //��ʹ-����
            dtPrint.Columns.Add("DIET0",                Type.GetType("System.String"));             //��ʳ-��ʳ
            dtPrint.Columns.Add("DIET1",                Type.GetType("System.String"));             //��ʳ-����ʳ
            dtPrint.Columns.Add("DIET2",                Type.GetType("System.String"));             //��ʳ-��ʳ
            dtPrint.Columns.Add("DIET3",                Type.GetType("System.String"));             //��ʳ-��ʳˮ
            dtPrint.Columns.Add("DIET4",                Type.GetType("System.String"));             //������ʳ
            dtPrint.Columns.Add("SLEEP0",               Type.GetType("System.String"));             //˯��-����
            dtPrint.Columns.Add("SLEEP1",               Type.GetType("System.String"));             //˯��-��˯����
            dtPrint.Columns.Add("SLEEP2",               Type.GetType("System.String"));             //˯��-����
            dtPrint.Columns.Add("SLEEP3",               Type.GetType("System.String"));             //˯��-����
            dtPrint.Columns.Add("SLEEP4",               Type.GetType("System.String"));             //˯��-ʧ��
            dtPrint.Columns.Add("SLEEP05",              Type.GetType("System.String"));             //˯��-ҩ�︨��˯��
            dtPrint.Columns.Add("STOOL0",               Type.GetType("System.String"));             //���-����
            dtPrint.Columns.Add("STOOL1",               Type.GetType("System.String"));             //���-����
            dtPrint.Columns.Add("STOOL2",               Type.GetType("System.String"));             //���-��к
            dtPrint.Columns.Add("STOOL3",               Type.GetType("System.String"));             //���-����
            dtPrint.Columns.Add("PEE0",                 Type.GetType("System.String"));             //С��-����
            dtPrint.Columns.Add("PEE1",                 Type.GetType("System.String"));             //С��-��ʧ��
            dtPrint.Columns.Add("PEE2",                 Type.GetType("System.String"));             //С��-������
            dtPrint.Columns.Add("PEE3",                 Type.GetType("System.String"));             //С��-�������
            dtPrint.Columns.Add("PEE4",                 Type.GetType("System.String"));             //С��-����
            dtPrint.Columns.Add("SKIN0",                Type.GetType("System.String"));             //Ƥ�����-����
            dtPrint.Columns.Add("SKIN1",                Type.GetType("System.String"));             //Ƥ�����-�԰�
            dtPrint.Columns.Add("SKIN2",                Type.GetType("System.String"));             //Ƥ�����-���
            dtPrint.Columns.Add("SKIN3",                Type.GetType("System.String"));             //Ƥ�����-����
            dtPrint.Columns.Add("SKIN4",                Type.GetType("System.String"));             //Ƥ�����-ˮ��
            dtPrint.Columns.Add("SKIN5",                Type.GetType("System.String"));             //Ƥ�����-��Ƥ
            dtPrint.Columns.Add("SKIN6",                Type.GetType("System.String"));             //Ƥ�����-Ƥ��
            dtPrint.Columns.Add("SKIN7",                Type.GetType("System.String"));             //Ƥ�����-Ƥ�λ
            dtPrint.Columns.Add("BEDSORE_N",            Type.GetType("System.String"));             //�촯-��
            dtPrint.Columns.Add("BEDSORE_Y",            Type.GetType("System.String"));             //�촯-��
            dtPrint.Columns.Add("BEDSORE_PART",         Type.GetType("System.String"));             //�촯-��λ
            dtPrint.Columns.Add("BEDSORE_DEGREE",       Type.GetType("System.String"));             //�촯-�̶�
            dtPrint.Columns.Add("BEDSORE_LEN",          Type.GetType("System.String"));             //�촯-��
            dtPrint.Columns.Add("BEDSORE_WIDTH",        Type.GetType("System.String"));             //�촯-��
            dtPrint.Columns.Add("SELF_DEPEND0",         Type.GetType("System.String"));             //����-����
            dtPrint.Columns.Add("SELF_DEPEND1",         Type.GetType("System.String"));             //����-��������
            dtPrint.Columns.Add("SELF_DEPEND2",         Type.GetType("System.String"));             //����-��ȫ��������
            dtPrint.Columns.Add("ILLCONG0",             Type.GetType("System.String"));             //��ʶ-��ȫ�˽�
            dtPrint.Columns.Add("ILLCONG1",             Type.GetType("System.String"));             //��ʶ-�����˽�
            dtPrint.Columns.Add("ILLCONG2",             Type.GetType("System.String"));             //��ʶ-���˽�
            dtPrint.Columns.Add("INTRO0",               Type.GetType("System.String"));             //����-���һ���
            dtPrint.Columns.Add("INTRO1",               Type.GetType("System.String"));             //����-��Ϣʱ��
            dtPrint.Columns.Add("INTRO2",               Type.GetType("System.String"));             //����-̽���ƶ�
            dtPrint.Columns.Add("INTRO3",               Type.GetType("System.String"));             //����-��ס�ƶ�
            dtPrint.Columns.Add("INTRO4",               Type.GetType("System.String"));             //����-������Ʒ�����ƶ�
            dtPrint.Columns.Add("INTRO5",               Type.GetType("System.String"));             //����-��ʳ
            dtPrint.Columns.Add("INTRO6",               Type.GetType("System.String"));             //����-����ҽ��
            dtPrint.Columns.Add("INTRO7",               Type.GetType("System.String"));             //����-���ܻ�ʿ
            dtPrint.Columns.Add("NOTE",                 Type.GetType("System.String"));             //��ע
            dtPrint.Columns.Add("NURSE",                Type.GetType("System.String"));             //��ʿ
            dtPrint.Columns.Add("RECDATE",              Type.GetType("System.String"));             //��¼ʱ��
            
            DataRow dr = dtPrint.NewRow();
            dtPrint.Rows.Add(dr);
		}
		
		
		/// <summary>
		/// Ϊ��ӡ׼������
		/// </summary>
		private void prepareDataForPrint()
		{
		    string val      = string.Empty;
		    bool blnHave    = false;
		    
		    // ��������
		    dtPrint.Rows.Clear();
		    DataRow dr = dtPrint.NewRow();
		    dtPrint.Rows.Add(dr);
            
            // ������Ϣ
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0
             || drShow == null)
            {
                return ;
            }
             
		    DataRow drSrc = dsPatient.Tables[0].Rows[0];
            
            dr["INP_NO"]            = drSrc["INP_NO"].ToString();                               // סԺ��
		    dr["PATIENT_NAME"]      = drSrc["NAME"].ToString();                                 // ����
            dr["PATIENT_SEX"]       = drSrc["SEX"].ToString();                                  // �Ա�
            dr["PATIENT_AGE"]       = txtPatientAge.Text;                                       // ����
            dr["PATIENT_DEPT"]      = drSrc["DEPT_NAME"].ToString();                            // �Ʊ�
            dr["PATIENT_BED_LABEL"] = drSrc["BED_LABEL"].ToString();                            // ����            
            dr["PATIENT_CAREER"]    = drShow["Career"].ToString();                              // ְҵ
            dr["PATIENT_NATION"]    = drSrc["NATION"].ToString();;                              // ����
            
            dr["PATIENT_DEGREE"]    = drShow["Degree"].ToString();                              // �Ļ��̶�
            dr["INPPROVIDER"]       = drShow["Provider"].ToString();                            // ��ʷ������
            dr["INPDIAG"]           = drShow["InpDiag"].ToString();                             // ��Ժ���
            dr["ASSERTDIAG"]        = drSrc["DIAGNOSIS"].ToString();                            // ȷ�����

            if (drSrc["ADMISSION_DATE_TIME"].ToString().Length > 0)
            {
                DateTime dt = (DateTime)drSrc["ADMISSION_DATE_TIME"];
                
                dr["INP_DATE_YEAR"] = dt.Year.ToString();               // ��
                dr["INP_DATE_MONTH"] = dt.Month.ToString();             // ��
                dr["INP_DATE_DAY"]  = dt.Day.ToString();                // ��
                dr["INP_DATE_HOUR"] = dt.Hour.ToString();               // ʱ                
                dr["INP_DATE_MINUTE"] = dt.Minute.ToString();           // ��
            }
            
            val = drSrc["INP_APPROACH"].ToString();
            if (val.Equals(chkInpApproach_Outp.Text) == true) { dr["APPROACH_CLINIC"] = CHECK;}     //��Ժ;��-����
            if (val.Equals(chkInpApproach_Emergency.Text) == true) { dr["APPROACH_EMERGENCY"] = CHECK;}     //��Ժ;��-����
            if (val.Equals(chkInpApproach_Shift.Text) == true) { dr["APPROACH_SHIFT"] = CHECK;}     //��Ժ;��-ת��
            
            val = drSrc["CHARGE_TYPE"].ToString();                          // ����֧��
            blnHave = false;
            if (val.Equals(chkChargeType_All.Text) == true) { dr["CHARGE0"] = CHECK; blnHave = true;}     //����֧��-����ҽ��
            if (val.Equals(chkChargeType_Big.Text) == true) { dr["CHARGE1"] = CHECK; blnHave = true;}     //����֧��-��ͳ��
            if (val.Equals(chkChargeType_Insur.Text) == true) { dr["CHARGE2"] = CHECK; blnHave = true;}     //����֧��-ҽ�Ʊ���
            if (val.Equals(chkChargeType_Self.Text) == true) { dr["CHARGE3"] = CHECK; blnHave = true;}     //����֧��-�Է�
            if (blnHave == false) {dr["CHARGE4"] = CHECK;};             // ����֧��-����
            
            val = drShow["InpMode"].ToString();                                                 // ��Ժ���
            if (val.Equals(chkInpMode_Foot.Text) == true) { dr["INP_MODE0"] = CHECK; }          //��Ժ��ʽ-����
            if (val.Equals(chkInpMode_Wheel.Text) == true) {dr["INP_MODE1"] = CHECK; }          //��Ժ��ʽ-����
            if (val.Equals(chkInpMode_Car.Text) == true) {dr["INP_MODE2"] = CHECK; }            //��Ժ��ʽ-ƽ��
            if (val.Equals(chkInpMode_Other.Text) == true) {dr["INP_MODE3"] = CHECK; }          //��Ժ��ʽ-����
                        
            dr["TEMPERATURE"]       = drShow["Temperature"].ToString();                         // ����            
            dr["PULSE"]             = drShow["Pulse"].ToString();                               //����            
            dr["HEAR_RATE"]         = drShow["HeartRate"].ToString();                           //����
            dr["BLOOD_PRESSURE"]    = drShow["BloodPressureH"].ToString() + "/" + drShow["BloodPressureL"].ToString(); // Ѫѹ
            
            dr["HEIGHT"]            = drShow["Height"].ToString();                              //���
            dr["WEIGHT"]            = drShow["Weight"].ToString();                              //����
            dr["INP_REASON"]        = drShow["InpReason"].ToString();                           //��Ժԭ��
            
            val = drShow["IllHis"].ToString().Trim();                                           // ����ʷ            
            if (val.Length == 0) { dr["ILLHIS_N"] = CHECK; }                // ����ʷ-��            
            if (val.Length > 0) { dr["ILLHIS_Y"] = CHECK; }                 // ��סʷ-��
            dr["ILLHIS"] = val;                                             // ��סʷ
            
            val = drShow["AllergyHis"].ToString().Trim();                   // ����ʷ
            if (val.Length == 0) { dr["ALLERGYHIS_N"] = CHECK; }            // ����ʷ-��
            if (val.Length > 0) { dr["ALLERGYHIS_Y"] = CHECK; }             // ����ʷ-��
            dr["ALLERGYHIS"] = val;                                         // ����ʷ
            
            val = drShow["Consciou"].ToString();                                                // ��ʶ״̬
            if (val.Equals(chkConscious_1.Text) == true) { dr["CONSCIOUS0"] = CHECK; }          // ��ʶ״̬-����
            if (val.Equals(chkConscious_2.Text) == true) { dr["CONSCIOUS1"] = CHECK; }          // ��ʶ״̬-����
            if (val.Equals(chkConscious_3.Text) == true) { dr["CONSCIOUS2"] = CHECK; }          // ��ʶ״̬-��˯
            if (val.Equals(chkConscious_4.Text) == true) { dr["CONSCIOUS3"] = CHECK; }          // ��ʶ״̬-��˯
            if (val.Equals(chkConscious_5.Text) == true) { dr["CONSCIOUS4"] = CHECK; }          // ��ʶ״̬-����
            
            val = drShow["SighBug"].ToString().Trim();                      // ����
            if (val.Length == 0) { dr["SIGHBUG_N"] = CHECK; }               // �����ϰ�-��
            if (val.Length > 0) { dr["SIGHBUG_Y"] = CHECK; }                // �����ϰ�-��
            if (val.Length > 0) { dr["SIGHBUG"] = "(" + val + ")"; }        // �����ϰ�-��/��
            
            val = drShow["AuditionBug"].ToString().Trim();                  // �����ϰ�
            if (val.Length == 0) { dr["AUDITION_N"] = CHECK; }              // �����ϰ�-��
            if (val.Length > 0) { dr["AUDITION_Y"] = CHECK; }               // �����ϰ�-��
            if (val.Length > 0) { dr["AUDITION"] = "(" + val + ")"; }       // �����ϰ�-��/��
            
            val = drShow["AchePart"].ToString().Trim();                     // ��ʹ��λ
            if (val.Length == 0) { dr["ACHE_N"] = CHECK; }                  // ��ʹ-��
            if (val.Length > 0) { dr["ACHE_Y"] = CHECK; }                   // ��ʹ-��
            
            dr["ACHE_PART"] = val;                                          // ��ʹ-��λ
            dr["ACHE_CLASS"] = drShow["AcheClass"].ToString();              // ��ʹ-����
            
            val = drShow["Diet"].ToString().Trim();                                             // ��ʳ
            if (val.Equals(chkDiet_1.Text) == true) { dr["DIET0"] = CHECK; }                    // ��ʳ-��ʳ
            if (val.Equals(chkDiet_2.Text) == true) { dr["DIET1"] = CHECK; }                    // ��ʳ-����ʳ
            if (val.Equals(chkDiet_3.Text) == true) { dr["DIET2"] = CHECK; }                    // ��ʳ-��ʳ
            if (val.Equals(chkDiet_4.Text) == true) { dr["DIET3"] = CHECK; }                    // ��ʳ-��ʳˮ
            dr["DIET4"] = drShow["DietCure"].ToString();
            
            val = drShow["Sleep"].ToString().Trim();                                            // ˯��
            if (val.Equals(chkSleep_0.Text) == true) {dr["SLEEP0"] = CHECK;}                    // ˯��-����
            if (val.Equals(chkSleep_1.Text) == true) {dr["SLEEP1"] = CHECK;}                    // ˯��-��˯����
            if (val.Equals(chkSleep_2.Text) == true) {dr["SLEEP2"] = CHECK;}                    // ˯��-����
            if (val.Equals(chkSleep_3.Text) == true) {dr["SLEEP3"] = CHECK;}                    // ˯��-����
            if (val.Equals(chkSleep_4.Text) == true) {dr["SLEEP4"] = CHECK;}                    // ˯��-ʧ��
            if (val.Equals(chkSleep_5.Text) == true) {dr["SLEEP5"] = CHECK;}                    // ˯��-ҩ�︨��˯��
            
            val = drShow["Stool"].ToString().Trim();                                            // ���
            if (val.Length == 0 || val.Equals(chkStool_0.Text) == true) { dr["STOOL0"] = CHECK; } //���-����
            if (val.Equals(chkStool_1.Text) == true) { dr["STOOL1"] = CHECK;}                   // ���-����
            if (val.Equals(chkStool_2.Text) == true) { dr["STOOL2"] = CHECK;}                   // ���-��к
            dr["STOOL3"] = drShow["StoolOther"].ToString();                                     // ���-����
            
            val = drShow["Pee"].ToString().Trim();                                              // С��
            if (val.Length == 0 || val.Equals(chkPee_0.Text) == true) { dr["PEE0"] = CHECK; }   // С��-����
            if (val.Equals(chkPee_1.Text) == true) { dr["PEE1"] = CHECK; }                      // С��-��ʧ��
            if (val.Equals(chkPee_2.Text) == true) { dr["PEE2"] = CHECK; }                      // С��-������
            if (val.Equals(chkPee_3.Text) == true) { dr["PEE3"] = CHECK; }                      // С��-�������
            dr["PEE4"] = drShow["PeeOther"].ToString();                                         // С��-����
            
            val = drShow["Skin"].ToString().Trim();                                             // Ƥ�����
            if (val.Length == 0 || val.Equals(chkSkin_0.Text) == true) { dr["SKIN0"] = CHECK; } // Ƥ�����-����
            if (val.Equals(chkSkin_1.Text) == true) { dr["SKIN1"] = CHECK;}                     // Ƥ�����-�԰�
            if (val.Equals(chkSkin_2.Text) == true) { dr["SKIN2"] = CHECK;}                     // Ƥ�����-���
            if (val.Equals(chkSkin_3.Text) == true) { dr["SKIN3"] = CHECK;}                     // Ƥ�����-����
            if (val.Equals(chkSkin_4.Text) == true) { dr["SKIN4"] = CHECK;}                     // Ƥ�����-ˮ��
            if (val.Equals(chkSkin_5.Text) == true) { dr["SKIN5"] = CHECK;}                     // Ƥ�����-��Ƥ
            if (val.Equals(chkSkin_6.Text) == true) { dr["SKIN6"] = CHECK;}                     // Ƥ�����-Ƥ��
            dr["SKIN7"] = drShow["SkinPart"].ToString();                                        // Ƥ�����-Ƥ�λ
            
            val = drShow["BedSorePart"].ToString().Trim();                                      // �촯��λ
            if (val.Length == 0) { dr["BEDSORE_N"] = CHECK; }                                   // �촯-��
            if (val.Length > 0) { dr["BEDSORE_Y"] = CHECK; }                                    // �촯-��            
            dr["BEDSORE_PART"] = val;                                                           // �촯-��λ
            
            dr["BEDSORE_DEGREE"] = drShow["BedSoreDegree"].ToString() + "��";                   // �촯-�̶�
            dr["BEDSORE_LEN"] = drShow["BedSoreLen"].ToString();                                // �촯-��
            dr["BEDSORE_WIDTH"] = drShow["BedSoreWidth"];                                       // �촯-��
            
            val = drShow["SelfDependDegree"].ToString().Trim();                                 // ����̶�
            if (val.Equals(chkSelfDepend.Text) == true) { dr["SELF_DEPEND0"] = CHECK; }         // ����-����
            if (val.Equals(chkSelfDepend_1.Text) == true) { dr["SELF_DEPEND1"] = CHECK; }       //����-��������
            if (val.Equals(chkSelfDepend_2.Text) == true) { dr["SELF_DEPEND2"] = CHECK; }
            
            val = drShow["IllCognition"].ToString().Trim();                                     // ������ʶ
            if (val.Equals(chkIllCognition.Text) == true) { dr["ILLCONG0"] = CHECK; }           // ��ʶ-��ȫ�˽�
            if (val.Equals(chkIllCognition_1.Text) == true) { dr["ILLCONG1"] = CHECK; }         // ��ʶ-�����˽�
            if (val.Equals(chkIllCognition_2.Text) == true) { dr["ILLCONG2"] = CHECK; }         // ��ʶ-���˽�
            
            dr["NOTE"] = drShow["Note"].ToString();                                             //��ע
            
            dr["NURSE"]     = drShow["Nurse"].ToString();                                       // ��ʿ
            dr["RECDATE"]   = drShow["RecDate"].ToString();                                     // ��¼ʱ��
            
            // ��Ժ����
            if (dsEduRec != null && dsEduRec.Tables.Count > 0)
            {
                foreach(DataRow drIn in dsEduRec.Tables[0].Rows)
                {
                    val = drIn["EDU_ITEM_NAME"].ToString();
                    
                    if (val.Equals(chkIntroDuct_1.Text) == true) { dr["INTRO0"] = CHECK; }      //����-���һ���
                    if (val.Equals(chkIntroDuct_2.Text) == true) { dr["INTRO1"] = CHECK; }      //����-��Ϣʱ��
                    if (val.Equals(chkIntroDuct_3.Text) == true) { dr["INTRO2"] = CHECK; }      //����-̽���ƶ�
                    if (val.Equals(chkIntroDuct_4.Text) == true) { dr["INTRO3"] = CHECK; }      //����-��ס�ƶ�
                    if (val.Equals(chkIntroDuct_5.Text) == true) { dr["INTRO4"] = CHECK; }      //����-������Ʒ�����ƶ�
                    if (val.Equals(chkIntroDuct_6.Text) == true) { dr["INTRO5"] = CHECK; }      //����-��ʳ
                    if (val.Equals(chkIntroDuct_7.Text) == true) { dr["INTRO6"] = CHECK; }      //����-����ҽ��
                    if (val.Equals(chkIntroDuct_8.Text) == true) { dr["INTRO7"] = CHECK; }      //����-���ܻ�ʿ
                }
            }
		}
		
		
		private string getParts(string line, ref int row, ref int col)
		{
		    string[] arrParts = line.Split(ComConst.STR.BLANK.ToCharArray());
		    
		    string pos = string.Empty;
		    string fieldName = string.Empty;
		    
		    row = 0;
		    col = 0;
		    for(int i = 0; i < arrParts.Length; i++)
		    {
		        if (arrParts[i].Trim().Length > 0)
		        {
		            if (pos.Length == 0) 
		            { 
		                pos = arrParts[i]; 
                    }
                    else
                    {
                        fieldName = arrParts[i];
                        break;
                    }
		        }    
		    }
		    
		    // ��ȡ����
		    arrParts = pos.Split(":".ToCharArray());
		    if (arrParts.Length <= 1)
		    {
		        return string.Empty;
		    }
		    
		    row = int.Parse(arrParts[0]);   // �к�
		    col = getCol(arrParts[1]);      // �к�
		    
		    return fieldName;
		}
		
		
		private int getCol(string colString)
		{
            int col = 0;
		    for(int i = 0; i < colString.Length; i++)
		    {
                switch(colString.Substring(i, 1).ToUpper())
                {
                    case "A": col += 1; break;
                    case "B": col += 2; break;
                    case "C": col += 3; break;
                    case "D": col += 4; break;
                    case "E": col += 5; break;
                    case "F": col += 6; break;
                    case "G": col += 7; break;
                    case "H": col += 8; break;
                    case "I": col += 9; break;
                    case "J": col += 10; break;
                    case "K": col += 11; break;
                    case "L": col += 12; break;
                    case "M": col += 13; break;
                    case "N": col += 14; break;
                    case "O": col += 15; break;
                    case "P": col += 16; break;
                    case "Q": col += 17; break;
                    case "R": col += 18; break;
                    case "S": col += 19; break;
                    case "T": col += 20; break;
                    case "U": col += 21; break;
                    case "V": col += 22; break;
                    case "W": col += 23; break;
                    case "X": col += 24; break;
                    case "Y": col += 25; break;
                    case "Z": col += 26; break;
                }
                
                if (colString.Length - 1 > i)
                {
                    col *= (colString.Length - 1 - i) * 26;
                }
		    }	
		    
		    return col;	    
		}
        #endregion
        #endregion
    }
}