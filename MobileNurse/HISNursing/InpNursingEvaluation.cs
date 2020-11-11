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
        #region 变量
        private const string   CHECK        = "√";
        
        private DataSet        dsPatient    = null;                     // 病人信息
        private string         patientId    = string.Empty;             // 病人标识号
        private string         visitId      = string.Empty;             // 住院标识

        private DataSet        dsEvaluation = null;                     // 护理评估信息
        private DataTable      dtShow       = null;
        private DataRow        drShow       = null;
        private DataSet        dsEduRec     = null;                     // 健康教育记录
        
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
        
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载
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
        /// 窗体改变事件
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
        /// 床标号回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtBedLabel_KeyDown(object sender, KeyEventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                // 条件检查
                if (e.KeyCode != Keys.Return)
                {
                    return;
                }
                
                // 获取查询条件
                if (txtBedLabel.Text.Trim().Length == 0)
                {
                    return;
                }
                
                GVars.App.UserInput = false;
                
                // 获取病人信息
                dsPatient = getPatientInfo(txtBedLabel.Text.Trim(), GVars.User.DeptCode);
                if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
                {
                    GVars.Msg.Show("W00005");                           // 该病人不存在!	
                    return;
                }
                
                // 显示病人信息
                showPatientInfo();
                
                // 显示病人基本信息
                initPatientComInfo();
                showPatientCommInfo();
                
                // 获取入院护理评估
                dtShow.Rows.Clear();
                drShow = dtShow.NewRow();
                
                getEvaluationInfo();
                
                formRecsInOne();
                
                // 显示入院护平评估
                initEvaluationDisp();
                showNursingEvalRec();
                
                // 获取健康教育记录
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
        /// 纵向滚动条滚动事件
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
        /// 按钮[查询]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>      
        private void btnSave_Click( object sender, EventArgs e )
        {
            try
            {
                // 护理评估
                getDisp_NursingEvalRec();
                
                DataTable dtRec = disperseOneInRecs();
                DataSet dsResult = new DataSet();
                dsResult.Tables.Add(dtRec);
                
                saveNursingEvalRec(dsResult, patientId, visitId);
                
                // 健康教育
                savePatientEduRec();
                
                GVars.Msg.Show("I00004", "保存");             // {0}成功!
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[打印]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click( object sender, EventArgs e )
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                // 为打印准备数据
                prepareDataForPrint();
                
                // 打印
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
        /// 按钮[关闭]
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
        /// 确保为半角输入法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void imeCtrl_GotFocus(object sender, EventArgs e)
        {
            IME.ChangeControlIme(this.ActiveControl.Handle);
        }

        
        #region 界面控制
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
        
        
        #region 输入检查
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
                    GVars.Msg.Show("E00012");   // 请输入正数!
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
                    GVars.Msg.Show("E00012");   // 请输入正数!
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
                    GVars.Msg.Show("E00012");   // 请输入正数!
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
                    GVars.Msg.Show("E00012");   // 请输入正数!
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
        
        
        #region 共通函数
        /// <summary>
        /// 初始化窗体变量
        /// </summary>
        private void initFrmVal()
        {
            // 创建对应于界面的表结构
            createNursingEvaluationTable();
            
            // 创建对应于打印的表结构
            createTableForPrint();
            
            // 初始化健康教育项目
            initEduItem();
        }
        
        
        /// <summary>
        /// 初始化界面
        /// </summary>
        private void initDisp()
        {
            // 设置界面
            this.vsbInpEval.Maximum = (this.panelHolder.Height - this.panelLayout.Height) / 10 + 20;
            
            initPatientComInfo();
        }        
        
        
        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="bedLabel">床标号</param>
        /// <returns></returns>
        private DataSet getPatientInfo(string bedLabel, string wardCode)
        {
            string sql = string.Empty;

            sql = "SELECT ";
            sql+=    "PAT_MASTER_INDEX.NAME, ";                                      // 姓名
            sql+=    "PAT_MASTER_INDEX.SEX, ";                                       // 性别
            sql+=    "PAT_MASTER_INDEX.DATE_OF_BIRTH, ";                             // 出生日期
            sql+=    "PATS_IN_HOSPITAL.DIAGNOSIS, ";                                 // 主要诊断
                        
            sql+=    "PATS_IN_HOSPITAL.VISIT_ID, ";                                  // 本次住院标识
            sql+=    "PAT_MASTER_INDEX.PATIENT_ID, ";                                // 病人标识号
            sql+=    "PAT_MASTER_INDEX.INP_NO, ";                                    // 住院号
            sql+=    "PAT_MASTER_INDEX.CHARGE_TYPE, ";                               // 费别
            
            sql+=    "PAT_MASTER_INDEX.NATION ,";                                    // 民族
            //sql+=    "PAT_MASTER_INDEX.DEGREE, ";                                  // 学历
            sql+=    "PAT_VISIT.OCCUPATION, ";                                       // 职业
            
            sql+=    "(SELECT PATIENT_CLASS_NAME FROM PATIENT_CLASS_DICT ";
            sql+=    "WHERE PATIENT_CLASS_CODE = PAT_VISIT.PATIENT_CLASS ";
            sql+=    ") INP_APPROACH, ";                                             // 入院方式
            
            sql+=	 "BED_REC.BED_NO, ";								             // 床号
            sql+=    "BED_REC.BED_LABEL, ";                                          // 床标号
            
            sql+=    "PATS_IN_HOSPITAL.ADMISSION_DATE_TIME, ";                       // 入院日期及时间

            sql+=    "(SELECT DEPT_NAME ";
            sql+=    "FROM DEPT_DICT ";
            sql+=    "WHERE DEPT_CODE = ";
            sql+=        "(CASE WHEN PATS_IN_HOSPITAL.LEND_INDICATOR = 1 ";
            sql+=         "THEN PATS_IN_HOSPITAL.DEPT_CODE_LEND ";
            sql+=         "ELSE PATS_IN_HOSPITAL.DEPT_CODE END)) ";
            sql+=    "DEPT_NAME ";                                                   // 所在科室
            
            sql+= "FROM ";
            sql+=    "PATS_IN_HOSPITAL, ";                                           // 在院病人记录
            sql+=    "PAT_VISIT, ";                                                  // 病人住院主记录
            sql+=    "PAT_MASTER_INDEX, ";                                           // 病人主索引
            sql+=    "BED_REC ";                                                     // 床位记录

            sql+= "WHERE ";
            sql+=    "(BED_REC.BED_NO = PATS_IN_HOSPITAL.BED_NO ";
            sql+=    "AND BED_REC.WARD_CODE = PATS_IN_HOSPITAL.WARD_CODE) ";
            
            // 床位号
            sql+=    "AND BED_REC.BED_LABEL = " + SqlConvert.SqlConvert(bedLabel);
            sql+=    "AND BED_REC.WARD_CODE = " + SqlConvert.SqlConvert(wardCode);
            sql+=   " AND PATS_IN_HOSPITAL.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID ";
            
            sql+=   " AND PAT_VISIT.PATIENT_ID = PATS_IN_HOSPITAL.PATIENT_ID ";
            sql+=   " AND PAT_VISIT.VISIT_ID = PATS_IN_HOSPITAL.VISIT_ID ";

            return GVars.OracleAccess.SelectData(sql);
        }        
        
        
        /// <summary>
        /// 显示病人的基本信息
        /// </summary>
        private void showPatientInfo()
        { 
            // 清空界面
            this.txtBedLabel.Text = string.Empty;                       // 病人床标号
            this.lblPatientID.Text = string.Empty;                      // 病人标识号
            this.lblVisitID.Text = string.Empty;                        // 本次住院标识
            this.lblPatientName.Text = string.Empty;                    // 病人姓名
            this.lblGender.Text = string.Empty;                         // 病人性别
            this.lblDeptName.Text = string.Empty;                       // 科别
            this.lblInpNo.Text = string.Empty;                          // 住院号
            this.lblInpDate.Text = string.Empty;                        // 入院日期
            
            patientId   = string.Empty;
            visitId     = string.Empty;
            
            // 如果没有数据退出
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                return;
            }
            
            // 显示病人的基本信息
            DataRow dr = dsPatient.Tables[0].Rows[0];
            
            this.txtBedLabel.Text = dr["BED_LABEL"].ToString();         // 病人床标号
            this.lblPatientID.Text = dr["PATIENT_ID"].ToString();       // 病人标识号
            this.lblVisitID.Text = dr["VISIT_ID"].ToString();           // 本次住院标识
            this.lblPatientName.Text = dr["NAME"].ToString();           // 病人姓名
            this.lblGender.Text = dr["SEX"].ToString();                 // 病人性别
            this.lblDeptName.Text = dr["DEPT_NAME"].ToString();         // 科别
            this.lblInpNo.Text = dr["INP_NO"].ToString();               // 住院号
            this.lblInpDate.Text = DataType.GetDateTimeShort(dr["ADMISSION_DATE_TIME"].ToString());   // 入院日期
            
            patientId   = dr["PATIENT_ID"].ToString();
            visitId     = dr["VISIT_ID"].ToString();
        }        
        
        
        /// <summary>
        /// 清空病人一般资料
        /// </summary>
        private void initPatientComInfo()
        {
            txtPatientName.Text              = string.Empty;            // 姓名
            txtPatientGender.Text            = string.Empty;            // 性别
            txtPatientAge.Text               = string.Empty;            // 年龄
            txtDeptName.Text                 = string.Empty;            // 科别
            txtPatientBedLabel.Text          = string.Empty;            // 床号
            txtCareer.Text                   = string.Empty;            // 职业
            txtNation.Text                   = string.Empty;            // 民族
            txtPatientEducation.Text         = string.Empty;            // 文化程度
            txtInfoProvider.Text             = string.Empty;            // 病史供述人
            txtInpDiagnose.Text              = string.Empty;            // 入院诊断
            txtAssertDiagnose.Text           = string.Empty;            // 确定诊断

            lblInpDate_Year.Text             = string.Empty;            // 入院时间(年)
            lblInpDate_Month.Text            = string.Empty;            // 入院时间(月)
            lblInpDate_Day.Text              = string.Empty;            // 入院时间(日)
            lblInpDate_Hour.Text             = string.Empty;            // 入院时间(时)
            lblInpDate_Minute.Text           = string.Empty;            // 入院时间(分)

            chkInpApproach_Outp.Checked      = false;                   // 入院途径(门诊)
            chkInpApproach_Emergency.Checked = false;                   // 入院途径(急诊)
            chkInpApproach_Shift.Checked     = false;                   // 入院途径(转入)

            chkChargeType_All.Checked        = false;                   // 费用支付(公费医疗)
            chkChargeType_Big.Checked        = false;                   // 大病统筹
            chkChargeType_Insur.Checked      = false;                   // 医疗保险
            chkChargeType_Self.Checked       = false;                   // 自费
            chkChargeType_Other.Checked      = false;                   // 其它

            chkInpMode_Foot.Checked          = false;                   // 步行
            chkInpMode_Wheel.Checked         = false;                   // 轮椅
            chkInpMode_Car.Checked           = false;                   // 平车
            chkInpMode_Other.Checked         = false;                   // 其它
        }
        
        
        /// <summary>
        /// 显示病人一般资料
        /// </summary>
        private void showPatientCommInfo()
        {
            // 如果没有数据退出
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                return;
            }
            
            DateTime Now = GVars.OracleAccess.GetSysDate();

            DataRow dr = dsPatient.Tables[0].Rows[0];
            string  val = string.Empty;
            
            txtPatientName.Text     = dr["NAME"].ToString();                                    // 姓名
            txtPatientGender.Text   = dr["SEX"].ToString();                                     // 性别
            
            val = dr["DATE_OF_BIRTH"].ToString();                                               // 年龄
            PersonCls person = new PersonCls();

            if (val.Length > 0)
            {
                txtPatientAge.Text = PersonCls.GetAge(DateTime.Parse(val), Now);
            }
            else
            {
                txtPatientAge.Text = string.Empty;
            }
            
            txtDeptName.Text         = dr["DEPT_NAME"].ToString();                              // 科别
            txtPatientBedLabel.Text  = dr["BED_LABEL"].ToString();                              // 床号            
            txtCareer.Text           = dr["OCCUPATION"].ToString();                             // 职业
            txtNation.Text           = dr["NATION"].ToString();                                 // 民族
            //txtPatientEducation.Text1 = dr["DEGREE"].ToString();                               // 文化程度
            txtInfoProvider.Text     = string.Empty;                                            // 病史供述人
            txtInpDiagnose.Text      = string.Empty;                                            // 入院诊断
            txtAssertDiagnose.Text   = dr["DIAGNOSIS"].ToString();                              // 确定诊断
            
            // 入院日期
            if (dr["ADMISSION_DATE_TIME"].ToString().Length > 0)
            {
                DateTime dt = (DateTime)dr["ADMISSION_DATE_TIME"];
                
                lblInpDate_Year.Text    = dt.Year.ToString();                                   // 年
                lblInpDate_Month.Text   = dt.Month.ToString();                                  // 月
                lblInpDate_Day.Text     = dt.Day.ToString();                                    // 日
                lblInpDate_Hour.Text    = dt.Hour.ToString();                                   // 小时
                lblInpDate_Minute.Text  = dt.Minute.ToString();                                 // 分钟
            }
            
            // 入院途径
            val = dr["INP_APPROACH"].ToString();   // 入院途径
            chkInpApproach_Outp.Checked         = (val.Equals(chkInpApproach_Outp.Text));       // 门诊
            chkInpApproach_Emergency.Checked    = (val.Equals(chkInpApproach_Emergency.Text));  // 急诊
            chkInpApproach_Shift.Checked        = (val.Equals(chkInpApproach_Shift.Text));      // 转入
            
            // 费用支付
            val = dr["CHARGE_TYPE"].ToString();    // 费用支付
            chkChargeType_All.Checked   = (val.Equals(chkChargeType_All.Text));                 // 公费治疗            
            chkChargeType_Big.Checked   = (val.Equals(chkChargeType_Big.Text));                 // 大病统筹
            chkChargeType_Insur.Checked = (val.Equals(chkChargeType_Insur.Text));               // 医疗保险            
            chkChargeType_Self.Checked  = (val.Equals(chkChargeType_Self.Text));                // 自费
            
            chkChargeType_Other.Checked = (chkChargeType_All.Checked == false 
                                            && chkChargeType_Big.Checked == false
                                            && chkChargeType_Insur.Checked == false
                                            && chkChargeType_Self.Checked == false);            // 其它
            
            //txtInpMode.Text1          = string.Empty;                                          // 入院方式
        }
        
        
        #region 护理评估
        /// <summary>
        /// 创建对应于界面显示的表
        /// </summary>
        private void createNursingEvaluationTable()
        {
            dtShow = new DataTable();

            dtShow.Columns.Add("Temperature",       Type.GetType("System.String"));             // 体温
            dtShow.Columns.Add("Pulse",             Type.GetType("System.String"));             // 脉搏
            dtShow.Columns.Add("HeartRate",         Type.GetType("System.String"));             // 心率
            dtShow.Columns.Add("Height",            Type.GetType("System.String"));             // 身高
            dtShow.Columns.Add("Weight",            Type.GetType("System.String"));             // 体重
            dtShow.Columns.Add("BloodPressureH",    Type.GetType("System.String"));             // 血压(高)
            dtShow.Columns.Add("BloodPressureL",    Type.GetType("System.String"));             // 血压(低)
            dtShow.Columns.Add("InpReason",         Type.GetType("System.String"));             // 入院原因
            dtShow.Columns.Add("IllHis",            Type.GetType("System.String"));             // 既往史
            dtShow.Columns.Add("AllergyHis",        Type.GetType("System.String"));             // 过敏史
            dtShow.Columns.Add("Consciou",          Type.GetType("System.String"));             // 意识状态
            dtShow.Columns.Add("SighBug",           Type.GetType("System.String"));             // 视障
            dtShow.Columns.Add("AuditionBug",       Type.GetType("System.String"));             // 听力障碍
            dtShow.Columns.Add("AchePart",          Type.GetType("System.String"));             // 疼痛部位
            dtShow.Columns.Add("AcheClass",         Type.GetType("System.String"));             // 疼痛性质
            dtShow.Columns.Add("Diet",              Type.GetType("System.String"));             // 饮食
            dtShow.Columns.Add("DietCure",          Type.GetType("System.String"));             // 治疗饮食
            dtShow.Columns.Add("Sleep",             Type.GetType("System.String"));             // 睡眠
            dtShow.Columns.Add("Stool",             Type.GetType("System.String"));             // 大便
            dtShow.Columns.Add("StoolOther",        Type.GetType("System.String"));             // 大便其它
            dtShow.Columns.Add("Pee",               Type.GetType("System.String"));             // 小便
            dtShow.Columns.Add("PeeOther",          Type.GetType("System.String"));             // 小便其它
            dtShow.Columns.Add("Skin",              Type.GetType("System.String"));             // 皮肤情况
            dtShow.Columns.Add("SkinPart",          Type.GetType("System.String"));             // 皮疹部位
            dtShow.Columns.Add("BedSorePart",       Type.GetType("System.String"));             // 褥疮部位
            dtShow.Columns.Add("BedSoreDegree",     Type.GetType("System.String"));             // 褥疮程度
            dtShow.Columns.Add("BedSoreLen",        Type.GetType("System.String"));             // 褥疮面程
            dtShow.Columns.Add("BedSoreWidth",      Type.GetType("System.String"));             // 褥疮面程
            dtShow.Columns.Add("SelfDependDegree",  Type.GetType("System.String"));             // 自理程度
            dtShow.Columns.Add("IllCognition",      Type.GetType("System.String"));             // 疾病认识
            dtShow.Columns.Add("Note",              Type.GetType("System.String"));             // 备注
            dtShow.Columns.Add("Career",            Type.GetType("System.String"));             // 职业
            dtShow.Columns.Add("Degree",            Type.GetType("System.String"));             // 文化程度
            dtShow.Columns.Add("Provider",          Type.GetType("System.String"));             // 病史供述人
            dtShow.Columns.Add("InpDiag",           Type.GetType("System.String"));             // 入院诊断
            dtShow.Columns.Add("InpMode",           Type.GetType("System.String"));             // 入院方式
            dtShow.Columns.Add("Nurse",             Type.GetType("System.String"));             // 护士
            dtShow.Columns.Add("RecDate",           Type.GetType("System.String"));             // 记录日期
        }        
        
        
        /// <summary>
        /// 获取护理评估信息
        /// </summary>        
        private void getEvaluationInfo()
        {
            string where = "PATIENT_ID = " + SqlConvert.SqlConvert(patientId)
                        + " AND VISIT_ID = " + SqlConvert.SqlConvert(visitId);
            
            // 获取病人的过敏史(以HIS为主)
            string allergyDrug = string.Empty;
            string sql = "SELECT ALERGY_DRUGS FROM PAT_VISIT WHERE " + where;
            if (GVars.OracleAccess.SelectValue(sql) == true)
            {
                allergyDrug = GVars.OracleAccess.GetResult(0);
            }
            
            // 更新Sqlserver中的数据
            sql = "UPDATE INP_EVALUATION_REC SET ITEM_VALUE = " + SqlConvert.SqlConvert(allergyDrug);
            sql += " WHERE ITEM_NAME = 'AllergyHis' AND " + where;
            GVars.SqlserverAccess.ExecuteNoQuery(sql);
            
            // 取数据
            sql = "SELECT * FROM INP_EVALUATION_REC WHERE " + where;
            dsEvaluation = GVars.SqlserverAccess.SelectData(sql, "INP_EVALUATION_REC");
            dsEvaluation.AcceptChanges();
        }
        
        
        /// <summary>
        /// 初始化护理评估信息
        /// </summary>
        private void initEvaluationDisp()
        {
            txtTemperature.Text               = string.Empty;           // 体温
            txtPulse.Text                     = string.Empty;           // 脉博
            txtHeartRate.Text                 = string.Empty;           // 心率
            txtBloodPressureH.Text            = string.Empty;           // 血压高
            txtBloodPressureL.Text            = string.Empty;           // 血压低
            txtHeight.Text                    = string.Empty;           // 身高
            txtWeight.Text                    = string.Empty;           // 体重
            txtInpReason.Text                 = string.Empty;           // 入院原因
            
            chkIllHis_No.Checked              = false;                  // 既往史(无)
            chkIllHis.Checked                 = false;                  // 既往史            
            txtIllHis.Text                    = string.Empty;
            
            chkAllergyHis_No.Checked          = false;                  // 过敏史(无)
            chkAllergyHis.Checked             = false;                  // 过敏史
            txtAllergyHis.Text                = string.Empty;           // 过敏史
            
            chkConscious_1.Checked            = false;                  // 意识状态1
            chkConscious_2.Checked            = false;                  // 意识状态2
            chkConscious_3.Checked            = false;                  // 意识状态3
            chkConscious_4.Checked            = false;                  // 意识状态4
            chkConscious_5.Checked            = false;                  // 意识状态5
            
            chkSighBug_No.Checked             = false;                  // 视力障碍(无)
            chkSighBug.Checked                = false;                  // 视力障碍
            cmbSighBug.SelectedIndex          = -1;
            
            chkAuditionBug_No.Checked         = false;                  // 听力障碍(无)
            chkAuditionBug.Checked            = false;                  // 听力障碍
            cmbAuditionBug.SelectedIndex      = -1;                     // 听力障碍
            
            chkAche_No.Checked                = false;                  // 疼痛(无)
            chkAche.Checked                   = false;                  // 疼痛            
            txtAchePart.Text                  = string.Empty;           // 部位
            txtAcheClass.Text                 = string.Empty;           // 性质
            
            chkDiet_1.Checked                 = false;                  // 饮食1
            chkDiet_2.Checked                 = false;                  // 饮食2
            chkDiet_3.Checked                 = false;                  // 饮食3
            chkDiet_4.Checked                 = false;                  // 饮食4
            chkDiet_5.Checked                 = false;                  // 饮食5
            txtDiet.Text                      = string.Empty;
            
            chkSleep_0.Checked                = false;                  // 睡眠状态1
            chkSleep_1.Checked                = false;                  // 睡眠状态2
            chkSleep_2.Checked                = false;                  // 睡眠状态3
            chkSleep_3.Checked                = false;                  // 睡眠状态4
            chkSleep_4.Checked                = false;                  // 睡眠状态5
            chkSleep_5.Checked                = false;                  // 睡眠状态6
            
            chkStool_0.Checked                = false;                  // 大便状态1
            chkStool_1.Checked                = false;                  // 大便状态2
            chkStool_2.Checked                = false;                  // 大便状态3
            chkStool_3.Checked                = false;                  // 大便状态4
            txtStool.Text                     = string.Empty;           // 大便
            
            chkPee_0.Checked                  = false;                  // 小便
            chkPee_1.Checked                  = false;                  // 小便状态1
            chkPee_2.Checked                  = false;                  // 小便状态2
            chkPee_3.Checked                  = false;                  // 小便状态3
            chkPee_Other.Checked              = false;
            
            chkSkin_0.Checked                 = false;                  // 皮肤情况1
            chkSkin_1.Checked                 = false;                  // 皮肤情况2
            chkSkin_2.Checked                 = false;                  // 皮肤情况3
            chkSkin_3.Checked                 = false;                  // 皮肤情况4
            chkSkin_4.Checked                 = false;                  // 皮肤情况5
            chkSkin_5.Checked                 = false;                  // 皮肤情况6
            chkSkin_6.Checked                 = false;                  // 皮肤情况7
            txtSkin.Text                      = string.Empty;           // 皮肤
            
            chkBedSore_No.Checked             = false;                  // 褥疮
            chkBedSore.Checked                = false;
            txtBedSorePart.Text               = string.Empty;           // 部位
            cmbBedSore.SelectedIndex          = -1;                     // 程度
            txtBedSoreLen.Text                = string.Empty;
            txtBedSoreWidth.Text              = string.Empty;
            
            chkSelfDepend.Checked             = false;                  // 自理程度1
            chkSelfDepend_1.Checked           = false;                  // 自理程度2
            chkSelfDepend_2.Checked           = false;                  // 自理程度3
            
            chkIllCognition.Checked           = false;                  // 对疾病的认识1
            chkIllCognition_1.Checked         = false;                  // 对疾病的认识2
            chkIllCognition_2.Checked         = false;                  // 对疾病的认识3
        }
        
        
        /// <summary>
        /// 显示病人的入院护理评估记录
        /// </summary>
        private void showNursingEvalRec()
        {
            string val = string.Empty;
            
            txtTemperature.Text      = drShow["Temperature"].ToString();                        // 体温
            txtPulse.Text            = drShow["Pulse"].ToString();                              // 脉搏
            txtHeartRate.Text        = drShow["HeartRate"].ToString();                          // 心率
            txtHeight.Text           = drShow["Height"].ToString();                             // 身高
            txtWeight.Text           = drShow["Weight"].ToString();                             // 体重
            txtBloodPressureH.Text   = drShow["BloodPressureH"].ToString();                     // 血压(高)
            txtBloodPressureL.Text   = drShow["BloodPressureL"].ToString();                     // 血压(低)
            txtInpReason.Text        = drShow["InpReason"].ToString();                          // 入院原因
            
            val = drShow["IllHis"].ToString().Trim();                                           // 既往史
            chkIllHis_No.Checked     = (val.Length == 0);
            chkIllHis.Checked        = (val.Length > 0);
            txtIllHis.Text           = val;
            
            val = drShow["AllergyHis"].ToString().Trim();                                       // 过敏史
            chkAllergyHis_No.Checked = (val.Length == 0);
            chkAllergyHis.Checked    = (val.Length > 0);
            txtAllergyHis.Text       = val;
            
            val = drShow["Consciou"].ToString();                                                // 意识状态
            chkConscious_1.Checked   = (val.Equals(chkConscious_1.Text));
            chkConscious_2.Checked   = (val.Equals(chkConscious_2.Text));
            chkConscious_3.Checked   = (val.Equals(chkConscious_3.Text));
            chkConscious_4.Checked   = (val.Equals(chkConscious_4.Text));
            chkConscious_5.Checked   = (val.Equals(chkConscious_5.Text));
            
            val = drShow["SighBug"].ToString().Trim();                                          // 视障
            chkSighBug_No.Checked    = (val.Length == 0);
            chkSighBug.Checked       = (val.Length > 0);
            cmbSighBug.SelectedValue = val;
            
            val = drShow["AuditionBug"].ToString().Trim();                                      // 听力障碍
            chkAuditionBug_No.Checked= (val.Length == 0);
            chkAuditionBug.Checked   = (val.Length > 0);
            cmbAuditionBug.SelectedValue = val;
            
            val = drShow["AchePart"].ToString().Trim();                                         // 疼痛部位
            chkAche_No.Checked       = (val.Length == 0);
            chkAche.Checked          = (val.Length > 0);
            txtAchePart.Text         = val;
            txtAcheClass.Text        = drShow["AcheClass"].ToString();                          // 疼痛性质
            
            val = drShow["Diet"].ToString().Trim();                                             // 饮食
            chkDiet_1.Checked        = (val.Equals(chkDiet_1.Text));
            chkDiet_2.Checked        = (val.Equals(chkDiet_2.Text));
            chkDiet_3.Checked        = (val.Equals(chkDiet_3.Text));
            chkDiet_4.Checked        = (val.Equals(chkDiet_4.Text));
            chkDiet_5.Checked        = (val.Equals(chkDiet_5.Text));
            txtDiet.Text             = drShow["DietCure"].ToString();                           // 治疗饮食
            
            val = drShow["Sleep"].ToString().Trim();                                            // 睡眠
            chkSleep_0.Checked       = (val.Length == 0 || val.Equals(chkSleep_0.Text));
            chkSleep_1.Checked       = (val.Equals(chkSleep_1.Text));
            chkSleep_2.Checked       = (val.Equals(chkSleep_2.Text));
            chkSleep_3.Checked       = (val.Equals(chkSleep_3.Text));
            chkSleep_4.Checked       = (val.Equals(chkSleep_4.Text));
            chkSleep_5.Checked       = (val.Equals(chkSleep_5.Text));
            
            val = drShow["Stool"].ToString().Trim();                                            // 大便
            chkStool_0.Checked       = (val.Length == 0 || val.Equals(chkStool_0.Text));
            chkStool_1.Checked       = (val.Equals(chkStool_1.Text));
            chkStool_2.Checked       = (val.Equals(chkStool_2.Text));
            chkStool_3.Checked       = (val.Equals(chkStool_3.Text));
            txtStool.Text            = drShow["StoolOther"].ToString();                         // 大便其它
            
            val = drShow["Pee"].ToString().Trim();                                              // 小便
            chkPee_0.Checked         = (val.Length == 0 || val.Equals(chkPee_0.Text));
            chkPee_1.Checked         = (val.Equals(chkPee_1.Text));
            chkPee_2.Checked         = (val.Equals(chkPee_2.Text));
            chkPee_3.Checked         = (val.Equals(chkPee_3.Text));
            chkPee_Other.Checked     = (val.Equals(chkPee_Other.Text));
            txtPee.Text              = drShow["PeeOther"].ToString();                           // 小便其它
            
            val = drShow["Skin"].ToString().Trim();                                             // 皮肤情况
            chkSkin_0.Checked        = (val.Length == 0 || val.Equals(chkSkin_0.Text));
            chkSkin_1.Checked        = (val.Equals(chkSkin_1.Text));
            chkSkin_2.Checked        = (val.Equals(chkSkin_2.Text));
            chkSkin_3.Checked        = (val.Equals(chkSkin_3.Text));
            chkSkin_4.Checked        = (val.Equals(chkSkin_4.Text));
            chkSkin_5.Checked        = (val.Equals(chkSkin_5.Text));
            chkSkin_6.Checked        = (val.Equals(chkSkin_6.Text));
            txtSkin.Text             = drShow["SkinPart"].ToString();                           // 皮疹部位
            
            val = drShow["BedSorePart"].ToString().Trim();                                      // 褥疮部位
            chkBedSore_No.Checked    = (val.Length == 0);
            chkBedSore.Checked       = (val.Length > 0);
            txtBedSorePart.Text      = val;
            cmbBedSore.SelectedValue = drShow["BedSoreDegree"].ToString();                      // 褥疮程度
            txtBedSoreLen.Text       = drShow["BedSoreLen"].ToString();                         // 褥疮面程
            txtBedSoreWidth.Text     = drShow["BedSoreWidth"].ToString();                       // 褥疮面程
            
            val = drShow["SelfDependDegree"].ToString().Trim();                                 // 自理程度
            chkSelfDepend.Checked    = (val.Equals(chkSelfDepend.Text));
            chkSelfDepend_1.Checked    = (val.Equals(chkSelfDepend_1.Text));
            chkSelfDepend_2.Checked    = (val.Equals(chkSelfDepend_2.Text));
            
            val = drShow["IllCognition"].ToString().Trim();                                     // 疾病认识
            chkIllCognition.Checked  = (val.Equals(chkIllCognition.Text));
            chkIllCognition_1.Checked  = (val.Equals(chkIllCognition_1.Text));
            chkIllCognition_2.Checked  = (val.Equals(chkIllCognition_2.Text));
            
            txtMemo.Text                = drShow["Note"].ToString();                            // 备注
            
            txtCareer.Text              = drShow["Career"].ToString();                          // 职业            
            txtPatientEducation.Text    = drShow["Degree"].ToString();                          // 文化程度
            txtInfoProvider.Text        = drShow["Provider"].ToString();                        // 病史供述人
            txtInpDiagnose.Text         = drShow["InpDiag"].ToString();                         // 入院诊断
            
            val = drShow["InpMode"].ToString();                                                 // 入院诊断
            chkInpMode_Foot.Checked     = (val.Equals(chkInpMode_Foot.Text));
            chkInpMode_Wheel.Checked    = (val.Equals(chkInpMode_Wheel.Text));
            chkInpMode_Car.Checked      = (val.Equals(chkInpMode_Car.Text));
            chkInpMode_Other.Checked    = (val.Equals(chkInpMode_Other.Text));
        }                
        
        
        /// <summary>
        /// 把记录集组合成一条记录
        /// </summary>
        private void formRecsInOne()
        { 
            // 转换数据
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
        /// 保存病人的入院护理评估记录
        /// </summary>
        private void getDisp_NursingEvalRec()
        {
            string val = string.Empty;
            drShow["Temperature"]      = (txtTemperature.Enabled ?      txtTemperature.Text:        string.Empty); // 体温
            drShow["Pulse"]            = (txtPulse.Enabled ?            txtPulse.Text:              string.Empty); // 脉搏
            drShow["HeartRate"]        = (txtHeartRate.Enabled ?        txtHeartRate.Text:          string.Empty); // 心率
            drShow["Height"]           = (txtHeight.Enabled ?           txtHeight.Text:             string.Empty); // 身高
            drShow["Weight"]           = (txtWeight.Enabled ?           txtWeight.Text:             string.Empty); // 体重
            drShow["BloodPressureH"]   = (txtBloodPressureH.Enabled ?   txtBloodPressureH.Text:     string.Empty); // 血压(高)
            drShow["BloodPressureL"]   = (txtBloodPressureL.Enabled ?   txtBloodPressureL.Text:     string.Empty); // 血压(低)
            drShow["InpReason"]        = (txtInpReason.Enabled ?        txtInpReason.Text:          string.Empty); // 入院原因
            drShow["IllHis"]           = (txtIllHis.Enabled ?           txtIllHis.Text:             string.Empty); // 既往史
            drShow["AllergyHis"]       = (txtAllergyHis.Enabled ?       txtAllergyHis.Text:         string.Empty); // 过敏史
            
            val = string.Empty;                                                                                     // 意识状态
            if (chkConscious_1.Checked == true) { val = chkConscious_1.Text; }
            if (chkConscious_2.Checked == true) { val = chkConscious_2.Text; }
            if (chkConscious_3.Checked == true) { val = chkConscious_3.Text; }
            if (chkConscious_4.Checked == true) { val = chkConscious_4.Text; }
            if (chkConscious_5.Checked == true) { val = chkConscious_5.Text; }            
            drShow["Consciou"]         = val;
            
            drShow["SighBug"]          = (cmbSighBug.Enabled ?          cmbSighBug.Text:            string.Empty); // 视障
            drShow["AuditionBug"]      = (cmbAuditionBug.Enabled ?      cmbAuditionBug.Text:        string.Empty); // 听力障碍
            drShow["AchePart"]         = (txtAchePart.Enabled ?         txtAchePart.Text:           string.Empty); // 疼痛部位
            drShow["AcheClass"]        = (txtAcheClass.Enabled ?        txtAcheClass.Text:          string.Empty); // 疼痛性质
            
            val = string.Empty;                                                                                     // 饮食
            if (chkDiet_1.Checked == true) { val = chkDiet_1.Text; }
            if (chkDiet_2.Checked == true) { val = chkDiet_2.Text; }
            if (chkDiet_3.Checked == true) { val = chkDiet_3.Text; }
            if (chkDiet_4.Checked == true) { val = chkDiet_4.Text; }
            if (chkDiet_5.Checked == true) { val = chkDiet_5.Text; }
            drShow["Diet"]              = val;
            
            drShow["DietCure"]          = (txtDiet.Enabled ?             txtDiet.Text:               string.Empty); // 治疗饮食
            
            val = string.Empty;                                                                                     // 睡眠
            if (chkSleep_0.Checked == true) { val = chkSleep_0.Text; }
            if (chkSleep_1.Checked == true) { val = chkSleep_1.Text; }
            if (chkSleep_2.Checked == true) { val = chkSleep_2.Text; }
            if (chkSleep_3.Checked == true) { val = chkSleep_3.Text; }
            if (chkSleep_4.Checked == true) { val = chkSleep_4.Text; }
            if (chkSleep_5.Checked == true) { val = chkSleep_5.Text; }            
            drShow["Sleep"]             = val; 
            
            val = string.Empty;                                                                                     // 大便
            if (chkStool_0.Checked == true) { val = chkStool_0.Text; }
            if (chkStool_1.Checked == true) { val = chkStool_1.Text; }
            if (chkStool_2.Checked == true) { val = chkStool_2.Text; }
            if (chkStool_3.Checked == true) { val = chkStool_3.Text; }
            drShow["Stool"]             = val;
            
            drShow["StoolOther"]        = (txtStool.Enabled ?            txtStool.Text:              string.Empty); // 大便其它
            
            val = string.Empty;                                                                                     // 小便
            if (chkPee_0.Checked == true) { val = chkPee_0.Text; }
            if (chkPee_1.Checked == true) { val = chkPee_1.Text; }
            if (chkPee_2.Checked == true) { val = chkPee_2.Text; }
            if (chkPee_3.Checked == true) { val = chkPee_3.Text; }
            drShow["Pee"]               = val; 
            
            drShow["PeeOther"]          = (txtPee.Enabled ?              txtPee.Text:                string.Empty); // 小便其它
            
            val = string.Empty;                                                                                     // 皮肤情况
            if (chkSkin_0.Checked == true) { val = chkSkin_0.Text; }
            if (chkSkin_1.Checked == true) { val = chkSkin_1.Text; }
            if (chkSkin_2.Checked == true) { val = chkSkin_2.Text; }
            if (chkSkin_3.Checked == true) { val = chkSkin_3.Text; }
            if (chkSkin_4.Checked == true) { val = chkSkin_4.Text; }
            if (chkSkin_5.Checked == true) { val = chkSkin_5.Text; }
            if (chkSkin_6.Checked == true) { val = chkSkin_6.Text; }            
            drShow["Skin"]              = val; 
            
            drShow["SkinPart"]         = (txtSkin.Enabled ?             txtSkin.Text:               string.Empty); // 皮疹部位
            
            drShow["BedSorePart"]      = (txtBedSorePart.Enabled ?      txtBedSorePart.Text:        string.Empty); // 褥疮部位
            drShow["BedSoreDegree"]    = (cmbBedSore.Enabled?           cmbBedSore.Text:            string.Empty); // 褥疮程度
            drShow["BedSoreLen"]       = (txtBedSoreLen.Enabled ?       txtBedSoreLen.Text:         string.Empty); // 褥疮面程
            drShow["BedSoreWidth"]     = (txtBedSoreWidth.Enabled ?     txtBedSoreWidth.Text:       string.Empty); // 褥疮面程
            
            val = string.Empty;                                                                                     // 自理程度
            if (chkSelfDepend.Checked == true) { val = chkSelfDepend.Text; }
            if (chkSelfDepend_1.Checked == true) { val = chkSelfDepend_1.Text; }
            if (chkSelfDepend_2.Checked == true) { val = chkSelfDepend_2.Text; }                        
            drShow["SelfDependDegree"] = val; 
            
            val = string.Empty;                                                                                     // 疾病认识
            if (chkIllCognition.Checked == true) { val = chkIllCognition.Text; }
            if (chkIllCognition_1.Checked == true) { val = chkIllCognition_1.Text; }
            if (chkIllCognition_2.Checked == true) { val = chkIllCognition_2.Text; }
            
            drShow["IllCognition"]      = val; 
            
            drShow["Note"]              = txtMemo.Text.Trim();                                                      // 备注
            drShow["Career"]            = txtCareer.Text.Trim();                                                    // 职业
            drShow["Degree"]            = txtPatientEducation.Text.Trim();                                          // 文化程度
            drShow["Provider"]          = txtInfoProvider.Text.Trim();                                              // 病史供述人
            drShow["InpDiag"]           = txtInpDiagnose.Text.Trim();                                               // 入院诊断
            
            val = string.Empty;                                                                                     // 入院方式
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
        /// 把一条记录分散成记录集
        /// </summary>
        private DataTable disperseOneInRecs()
        {
            DataTable dtTemp    = dsEvaluation.Tables[0].Clone();
            string    val       = string.Empty;
            
            val = drShow["Temperature"].ToString();                                             // 体温
            addNewRec(ref dtTemp, "Temperature", val, "度");
            
            val = drShow["Pulse"].ToString();                                                   // 脉搏
            addNewRec(ref dtTemp, "Pulse", val, "次/分");
            
            val = drShow["HeartRate"].ToString();                                               // 心率
            addNewRec(ref dtTemp, "HeartRate", val, "次/分");
            
            val = drShow["Height"].ToString();                                                  // 身高
            addNewRec(ref dtTemp, "Height", val, "cm");
            
            val = drShow["Weight"].ToString();                                                  // 体重
            addNewRec(ref dtTemp, "Weight", val, "kg");

            val = drShow["BloodPressureH"].ToString();                                          // 血压(高)
            addNewRec(ref dtTemp, "BloodPressureH", val, "mmHg");

            val = drShow["BloodPressureL"].ToString();                                          // 血压(低)
            addNewRec(ref dtTemp, "BloodPressureL", val, "mmHg");

            val = drShow["InpReason"].ToString();                                               // 入院原因
            addNewRec(ref dtTemp, "InpReason", val, string.Empty);

            val = drShow["IllHis"].ToString();                                                  // 既往史
            addNewRec(ref dtTemp, "IllHis", val, string.Empty);

            val = drShow["AllergyHis"].ToString();                                              // 过敏史
            addNewRec(ref dtTemp, "AllergyHis", val, string.Empty);

            val = drShow["Consciou"].ToString();                                                // 意识状态
            addNewRec(ref dtTemp, "Consciou", val, string.Empty);

            val = drShow["SighBug"].ToString();                                                 // 视障
            addNewRec(ref dtTemp, "SighBug", val, string.Empty);

            val = drShow["AuditionBug"].ToString();                                             // 听力障碍
            addNewRec(ref dtTemp, "AuditionBug", val, string.Empty);

            val = drShow["AchePart"].ToString();                                                // 疼痛部位
            addNewRec(ref dtTemp, "AchePart", val, string.Empty);

            val = drShow["AcheClass"].ToString();                                               // 疼痛性质
            addNewRec(ref dtTemp, "AcheClass", val, string.Empty);

            val = drShow["Diet"].ToString();                                                    // 饮食
            addNewRec(ref dtTemp, "Diet", val, string.Empty);

            val = drShow["DietCure"].ToString();                                                // 治疗饮食
            addNewRec(ref dtTemp, "DietCure", val, string.Empty);
            
            val = drShow["Sleep"].ToString();                                                   // 睡眠
            addNewRec(ref dtTemp, "Sleep", val, string.Empty);
            
            val = drShow["Stool"].ToString();                                                   // 大便
            addNewRec(ref dtTemp, "Stool", val, string.Empty);
            
            val = drShow["StoolOther"].ToString();                                              // 大便其它
            addNewRec(ref dtTemp, "StoolOther", val, string.Empty);
            
            val = drShow["Pee"].ToString();                                                     // 小便
            addNewRec(ref dtTemp, "Pee", val, string.Empty);
            
            val = drShow["PeeOther"].ToString();                                                // 小便其它
            addNewRec(ref dtTemp, "PeeOther", val, string.Empty);
            
            val = drShow["Skin"].ToString();                                                    // 皮肤情况
            addNewRec(ref dtTemp, "Skin", val, string.Empty);
            
            val = drShow["SkinPart"].ToString();                                                // 皮疹部位
            addNewRec(ref dtTemp, "SkinPart", val, string.Empty);

            val = drShow["BedSorePart"].ToString();                                             // 褥疮部位
            addNewRec(ref dtTemp, "BedSorePart", val, string.Empty);

            val = drShow["BedSoreDegree"].ToString();                                           // 褥疮程度
            addNewRec(ref dtTemp, "BedSoreDegree", val, string.Empty);

            val = drShow["BedSoreLen"].ToString();                                              // 褥疮面程
            addNewRec(ref dtTemp, "BedSoreLen", val, "cm");

            val = drShow["BedSoreWidth"].ToString();                                            // 褥疮面程
            addNewRec(ref dtTemp, "BedSoreWidth", val, "cm");

            val = drShow["SelfDependDegree"].ToString();                                        // 自理程度
            addNewRec(ref dtTemp, "SelfDependDegree", val, string.Empty);
            
            val = drShow["IllCognition"].ToString();                                            // 疾病认识
            addNewRec(ref dtTemp, "IllCognition", val, string.Empty);
            
            val = drShow["Note"].ToString();                                                    // 备注
            addNewRec(ref dtTemp, "Note", val, string.Empty);
            
            val = drShow["Career"].ToString();                                                  // 职业
            addNewRec(ref dtTemp, "Career", val, string.Empty);
            
            val = drShow["Degree"].ToString();                                                  // 文化程度
            addNewRec(ref dtTemp, "Degree", val, string.Empty);
            
            val = drShow["Provider"].ToString();                                                // 病史供述人
            addNewRec(ref dtTemp, "Provider", val, string.Empty);
            
            val = drShow["InpDiag"].ToString();                                                 // 入院诊断
            addNewRec(ref dtTemp, "InpDiag", val, string.Empty);
            
            val = drShow["InpMode"].ToString();                                                 // 入院诊断
            addNewRec(ref dtTemp, "InpMode", val, string.Empty);
            
            val = drShow["Nurse"].ToString();                                                   // 护士
            addNewRec(ref dtTemp, "Nurse", val, string.Empty);
            
            val = drShow["RecDate"].ToString();                                                 // 记录时间
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
        /// 保存病人的入院护理评估记录
        /// </summary>
        /// <param name="dsEduRec"></param>
        /// <returns></returns>
        public bool saveNursingEvalRec(DataSet dsNursingEvalRec, string patientId, string visitId)
        { 
            // 条件检查
            if (dsNursingEvalRec == null || dsNursingEvalRec.Tables.Count == 0)
            {
                return true;
            }
            
            // 保存到本地
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
                
                // 判断记录是否存在
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
            
            // 保存过敏史到HIS中
            sql = "UPDATE PAT_VISIT SET ALERGY_DRUGS = " + SqlConvert.SqlConvert(txtAllergyHis.Text.Trim())
               + " WHERE PATIENT_ID = " + SqlConvert.SqlConvert(patientId)
               + " AND VISIT_ID = " + SqlConvert.SqlConvert(visitId);
            
            GVars.OracleAccess.ExecuteNoQuery(sql);
            
            return true;
        }        
        #endregion
        
        
        #region 入院介绍
        /// <summary>
        /// 获取病人的健康教育项目
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
        /// 显示病人的健康教育项目
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
        /// 保存病人的健康教育记录
        /// </summary>
        private void savePatientEduRec()
        {
            // 判断记录是否存在
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
        /// 保存一条健康教育记录
        /// </summary>
        /// <param name="itemCode">项目代码</param>
        /// <param name="blnDone">是否教育</param>
        /// <returns></returns>
        private string saveOneEduRec(string itemCode, bool blnDone, ref string sqlSel, ref string sqlDel, ref string sqlIns)
        {
            if (itemCode.Length == 0)
            {
                return string.Empty;
            }
            
            string sql = string.Format(sqlSel, itemCode);
            
            // 如果是删除
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
        /// 初始健康教育
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
            
            // 获取健康教育项目代码
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
        
        
        #region 数据检查
        /// <summary>
        /// 体温检查
        /// </summary>
        /// <remarks>体温的有效范围: 33 – 42.4</remarks>
        /// <param name="temperature">体温</param>
        /// <returns>TRUE: 通过检查; FALSE: 没有通过检查</returns>
        private bool chkBodyTemperature(float temperature)
        {
            if (temperature < 33 || temperature > 42.4)
            {
                GVars.Msg.MsgId = "E00016";                             // {0}不正确, 有效范围为 {1} - {2}
                
                GVars.Msg.MsgContent.Add("体温");
                GVars.Msg.MsgContent.Add("33");
                GVars.Msg.MsgContent.Add("42.4");

                return false;
            }

            return true;
        }
        
        
        /// <summary>
        /// 心率检查
        /// </summary>
        /// <remarks>心率的效范围: 0 – 188</remarks>
        /// <param name="rate">心率</param>
        /// <returns>TRUE: 通过检查; FALSE: 没有通过检查</returns>
        private bool chkHeartRate(float rate)
        {
            if (rate < 0 || rate > 188)
            {
                GVars.Msg.MsgId = "E00016";                             // {0}不正确, 有效范围为 {1} - {2}

                GVars.Msg.MsgContent.Add("心率");
                GVars.Msg.MsgContent.Add("0");
                GVars.Msg.MsgContent.Add("188");

                return false;
            }

            return true;
        }
        
        
        /// <summary>
        /// 脉搏检查
        /// </summary>
        /// <remarks>脉搏的效范围: 0 – 188</remarks>
        /// <param name="rate">脉搏</param>
        /// <returns>TRUE: 通过检查; FALSE: 没有通过检查</returns>
        private bool chkPulse(float pulse)
        {
            if (pulse < 0 || pulse > 188)
            {
                GVars.Msg.MsgId = "E00016";                             // {0}不正确, 有效范围为 {1} - {2}

                GVars.Msg.MsgContent.Add("脉搏");
                GVars.Msg.MsgContent.Add("0");
                GVars.Msg.MsgContent.Add("188");
                
                return false;
            }

            return true;
        }
        
        
        /// <summary>
        /// 呼吸检查
        /// </summary>
        /// <remarks>呼吸的效范围: 0 – 188</remarks>
        /// <param name="rate">呼吸</param>
        /// <returns>TRUE: 通过检查; FALSE: 没有通过检查</returns>
        private bool chkBreath(float breath)
        {
            if (breath < 0 || breath > 188)
            {
                GVars.Msg.MsgId = "E00016";                             // {0}不正确, 有效范围为 {1} - {2}
                
                GVars.Msg.MsgContent.Add("呼吸");
                GVars.Msg.MsgContent.Add("0");
                GVars.Msg.MsgContent.Add("188");
                
                return false;
            }
            
            return true;
        }                
        #endregion
        
        
        #region 打印
		/// <summary>
		/// 用Excel模板打印，比较适合套打、格式、统计分析报表、图形分析、自定义打印
		/// </summary>
		/// <remarks>用Excel打印，步骤为：打开、写数据、打印预览、关闭</remarks>
		private void ExcelTemplatePrint()
		{
			string strExcelTemplateFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Template\\入院护理评估表.xls");
            
			excelAccess.Open(strExcelTemplateFile);				//用模板文件
			excelAccess.IsVisibledExcel = true;
			excelAccess.FormCaption = string.Empty;	
		    
		    // 读取配置文件
		    string iniFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Template\\入院护理评估表.ini");
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
		            // 获取配置
                    fieldName = getParts(line, ref row, ref col);
		            
		            if (fieldName.Length > 0)
		            {
		                excelAccess.SetCellText(row, col, drPrint[fieldName].ToString());
		            }
		        }
		    }
		    
			//excel.Print();				           //打印
			excelAccess.PrintPreview();			       //预览
            
			excelAccess.Close(false);				   //关闭并释放			
		}
		
		
		/// <summary>
		/// 创建打印表格
		/// </summary>
		private void createTableForPrint()
		{
		    dtPrint = new DataTable();
		    
            dtPrint.Columns.Add("INP_NO",               Type.GetType("System.String"));             //住院号
            dtPrint.Columns.Add("PATIENT_NAME",         Type.GetType("System.String"));             //姓名
            dtPrint.Columns.Add("PATIENT_SEX",          Type.GetType("System.String"));             //性别
            dtPrint.Columns.Add("PATIENT_AGE",          Type.GetType("System.String"));             //年龄
            dtPrint.Columns.Add("PATIENT_DEPT",         Type.GetType("System.String"));             //科别
            dtPrint.Columns.Add("PATIENT_BED_LABEL",    Type.GetType("System.String"));             //床号
            dtPrint.Columns.Add("PATIENT_CAREER",       Type.GetType("System.String"));             //职业
            dtPrint.Columns.Add("PATIENT_NATION",       Type.GetType("System.String"));             //民族
            dtPrint.Columns.Add("PATIENT_DEGREE",       Type.GetType("System.String"));             //文化程度
            dtPrint.Columns.Add("INPPROVIDER",          Type.GetType("System.String"));             //病史陈述人
            dtPrint.Columns.Add("INPDIAG",              Type.GetType("System.String"));             //入院诊断
            dtPrint.Columns.Add("ASSERTDIAG",           Type.GetType("System.String"));             // 确定诊断
            dtPrint.Columns.Add("INP_DATE_YEAR",        Type.GetType("System.String"));             //年
            dtPrint.Columns.Add("INP_DATE_MONTH",       Type.GetType("System.String"));             //月
            dtPrint.Columns.Add("INP_DATE_DAY",         Type.GetType("System.String"));             //日
            dtPrint.Columns.Add("INP_DATE_HOUR",        Type.GetType("System.String"));             // 时
            dtPrint.Columns.Add("INP_DATE_MINUTE",      Type.GetType("System.String"));             //分
            dtPrint.Columns.Add("APPROACH_CLINIC",      Type.GetType("System.String"));             //入院途径-门诊
            dtPrint.Columns.Add("APPROACH_EMERGENCY",   Type.GetType("System.String"));             //入院途径-急诊
            dtPrint.Columns.Add("APPROACH_SHIFT",       Type.GetType("System.String"));             //入院途径-转入
            dtPrint.Columns.Add("CHARGE0",              Type.GetType("System.String"));             //费用支付-公费医疗
            dtPrint.Columns.Add("CHARGE1",              Type.GetType("System.String"));             //费用支付-大病统筹
            dtPrint.Columns.Add("CHARGE2",              Type.GetType("System.String"));             //费用支付-医疗保险
            dtPrint.Columns.Add("CHARGE3",              Type.GetType("System.String"));             //费用支付-自费
            dtPrint.Columns.Add("CHARGE4",              Type.GetType("System.String"));             //费用支付-其它
            dtPrint.Columns.Add("INP_MODE0",            Type.GetType("System.String"));             //入院方式-步行
            dtPrint.Columns.Add("INP_MODE1",            Type.GetType("System.String"));             //入院方式-轮椅
            dtPrint.Columns.Add("INP_MODE2",            Type.GetType("System.String"));             //入院方式-平车
            dtPrint.Columns.Add("INP_MODE3",            Type.GetType("System.String"));             //入院方式-其它
            dtPrint.Columns.Add("TEMPERATURE",          Type.GetType("System.String"));             //体温
            dtPrint.Columns.Add("PULSE",                Type.GetType("System.String"));             //脉搏
            dtPrint.Columns.Add("HEAR_RATE",            Type.GetType("System.String"));             //心率
            dtPrint.Columns.Add("BLOOD_PRESSURE",       Type.GetType("System.String"));             //血压
            dtPrint.Columns.Add("HEIGHT",               Type.GetType("System.String"));             //身高
            dtPrint.Columns.Add("WEIGHT",               Type.GetType("System.String"));             //体重
            dtPrint.Columns.Add("INP_REASON",           Type.GetType("System.String"));             //入院原因
            dtPrint.Columns.Add("ILLHIS_N",             Type.GetType("System.String"));             //既往史-无
            dtPrint.Columns.Add("ILLHIS_Y",             Type.GetType("System.String"));             //既住史-有
            dtPrint.Columns.Add("ILLHIS",               Type.GetType("System.String"));             //既住史
            dtPrint.Columns.Add("ALLERGYHIS_N",         Type.GetType("System.String"));             //过敏史-无
            dtPrint.Columns.Add("ALLERGYHIS_Y",         Type.GetType("System.String"));             //过敏史-有
            dtPrint.Columns.Add("ALLERGYHIS",           Type.GetType("System.String"));             //过敏史
            dtPrint.Columns.Add("CONSCIOUS0",           Type.GetType("System.String"));             //意识状态-清醒
            dtPrint.Columns.Add("CONSCIOUS1",           Type.GetType("System.String"));             //意识状态-朦胧
            dtPrint.Columns.Add("CONSCIOUS2",           Type.GetType("System.String"));             //意识状态-嗜睡
            dtPrint.Columns.Add("CONSCIOUS3",           Type.GetType("System.String"));             //意识状态-昏睡
            dtPrint.Columns.Add("CONSCIOUS4",           Type.GetType("System.String"));             //意识状态-昏迷
            dtPrint.Columns.Add("SIGHBUG_N",            Type.GetType("System.String"));             //视力障碍-无
            dtPrint.Columns.Add("SIGHBUG_Y",            Type.GetType("System.String"));             //视力障碍-有
            dtPrint.Columns.Add("SIGHBUG",              Type.GetType("System.String"));             //视力障碍-左/右
            dtPrint.Columns.Add("AUDITION_N",           Type.GetType("System.String"));             //听力障碍-无
            dtPrint.Columns.Add("AUDITION_Y",           Type.GetType("System.String"));             //听力障碍-有
            dtPrint.Columns.Add("AUDITION",             Type.GetType("System.String"));             //听力障碍-左/右
            dtPrint.Columns.Add("ACHE_N",               Type.GetType("System.String"));             //疼痛-无
            dtPrint.Columns.Add("ACHE_Y",               Type.GetType("System.String"));             //疼痛-有
            dtPrint.Columns.Add("ACHE_PART",            Type.GetType("System.String"));             //疼痛-部位
            dtPrint.Columns.Add("ACHE_CLASS",           Type.GetType("System.String"));             //疼痛-性质
            dtPrint.Columns.Add("DIET0",                Type.GetType("System.String"));             //饮食-普食
            dtPrint.Columns.Add("DIET1",                Type.GetType("System.String"));             //饮食-半流食
            dtPrint.Columns.Add("DIET2",                Type.GetType("System.String"));             //饮食-流食
            dtPrint.Columns.Add("DIET3",                Type.GetType("System.String"));             //饮食-禁食水
            dtPrint.Columns.Add("DIET4",                Type.GetType("System.String"));             //治疗饮食
            dtPrint.Columns.Add("SLEEP0",               Type.GetType("System.String"));             //睡眠-正常
            dtPrint.Columns.Add("SLEEP1",               Type.GetType("System.String"));             //睡眠-入睡困难
            dtPrint.Columns.Add("SLEEP2",               Type.GetType("System.String"));             //睡眠-易醒
            dtPrint.Columns.Add("SLEEP3",               Type.GetType("System.String"));             //睡眠-多梦
            dtPrint.Columns.Add("SLEEP4",               Type.GetType("System.String"));             //睡眠-失眠
            dtPrint.Columns.Add("SLEEP05",              Type.GetType("System.String"));             //睡眠-药物辅助睡眠
            dtPrint.Columns.Add("STOOL0",               Type.GetType("System.String"));             //大便-正常
            dtPrint.Columns.Add("STOOL1",               Type.GetType("System.String"));             //大便-便秘
            dtPrint.Columns.Add("STOOL2",               Type.GetType("System.String"));             //大便-腹泻
            dtPrint.Columns.Add("STOOL3",               Type.GetType("System.String"));             //大便-其它
            dtPrint.Columns.Add("PEE0",                 Type.GetType("System.String"));             //小便-正常
            dtPrint.Columns.Add("PEE1",                 Type.GetType("System.String"));             //小便-尿失禁
            dtPrint.Columns.Add("PEE2",                 Type.GetType("System.String"));             //小便-尿潴留
            dtPrint.Columns.Add("PEE3",                 Type.GetType("System.String"));             //小便-留置尿管
            dtPrint.Columns.Add("PEE4",                 Type.GetType("System.String"));             //小便-其它
            dtPrint.Columns.Add("SKIN0",                Type.GetType("System.String"));             //皮肤情况-正常
            dtPrint.Columns.Add("SKIN1",                Type.GetType("System.String"));             //皮肤情况-苍白
            dtPrint.Columns.Add("SKIN2",                Type.GetType("System.String"));             //皮肤情况-紫绀
            dtPrint.Columns.Add("SKIN3",                Type.GetType("System.String"));             //皮肤情况-黄梁
            dtPrint.Columns.Add("SKIN4",                Type.GetType("System.String"));             //皮肤情况-水肿
            dtPrint.Columns.Add("SKIN5",                Type.GetType("System.String"));             //皮肤情况-脱皮
            dtPrint.Columns.Add("SKIN6",                Type.GetType("System.String"));             //皮肤情况-皮疹
            dtPrint.Columns.Add("SKIN7",                Type.GetType("System.String"));             //皮肤情况-皮疹部位
            dtPrint.Columns.Add("BEDSORE_N",            Type.GetType("System.String"));             //褥疮-无
            dtPrint.Columns.Add("BEDSORE_Y",            Type.GetType("System.String"));             //褥疮-有
            dtPrint.Columns.Add("BEDSORE_PART",         Type.GetType("System.String"));             //褥疮-部位
            dtPrint.Columns.Add("BEDSORE_DEGREE",       Type.GetType("System.String"));             //褥疮-程度
            dtPrint.Columns.Add("BEDSORE_LEN",          Type.GetType("System.String"));             //褥疮-长
            dtPrint.Columns.Add("BEDSORE_WIDTH",        Type.GetType("System.String"));             //褥疮-宽
            dtPrint.Columns.Add("SELF_DEPEND0",         Type.GetType("System.String"));             //生活-自理
            dtPrint.Columns.Add("SELF_DEPEND1",         Type.GetType("System.String"));             //生活-部分自理
            dtPrint.Columns.Add("SELF_DEPEND2",         Type.GetType("System.String"));             //生活-完全不能自理
            dtPrint.Columns.Add("ILLCONG0",             Type.GetType("System.String"));             //认识-完全了解
            dtPrint.Columns.Add("ILLCONG1",             Type.GetType("System.String"));             //认识-部份了解
            dtPrint.Columns.Add("ILLCONG2",             Type.GetType("System.String"));             //认识-不了解
            dtPrint.Columns.Add("INTRO0",               Type.GetType("System.String"));             //介绍-病室环境
            dtPrint.Columns.Add("INTRO1",               Type.GetType("System.String"));             //介绍-作息时间
            dtPrint.Columns.Add("INTRO2",               Type.GetType("System.String"));             //介绍-探视制度
            dtPrint.Columns.Add("INTRO3",               Type.GetType("System.String"));             //介绍-陪住制度
            dtPrint.Columns.Add("INTRO4",               Type.GetType("System.String"));             //介绍-贵重物品管理制度
            dtPrint.Columns.Add("INTRO5",               Type.GetType("System.String"));             //介绍-饮食
            dtPrint.Columns.Add("INTRO6",               Type.GetType("System.String"));             //介绍-主管医生
            dtPrint.Columns.Add("INTRO7",               Type.GetType("System.String"));             //介绍-主管护士
            dtPrint.Columns.Add("NOTE",                 Type.GetType("System.String"));             //备注
            dtPrint.Columns.Add("NURSE",                Type.GetType("System.String"));             //护士
            dtPrint.Columns.Add("RECDATE",              Type.GetType("System.String"));             //记录时间
            
            DataRow dr = dtPrint.NewRow();
            dtPrint.Rows.Add(dr);
		}
		
		
		/// <summary>
		/// 为打印准备数据
		/// </summary>
		private void prepareDataForPrint()
		{
		    string val      = string.Empty;
		    bool blnHave    = false;
		    
		    // 创建新行
		    dtPrint.Rows.Clear();
		    DataRow dr = dtPrint.NewRow();
		    dtPrint.Rows.Add(dr);
            
            // 病人信息
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0
             || drShow == null)
            {
                return ;
            }
             
		    DataRow drSrc = dsPatient.Tables[0].Rows[0];
            
            dr["INP_NO"]            = drSrc["INP_NO"].ToString();                               // 住院号
		    dr["PATIENT_NAME"]      = drSrc["NAME"].ToString();                                 // 姓名
            dr["PATIENT_SEX"]       = drSrc["SEX"].ToString();                                  // 性别
            dr["PATIENT_AGE"]       = txtPatientAge.Text;                                       // 年龄
            dr["PATIENT_DEPT"]      = drSrc["DEPT_NAME"].ToString();                            // 科别
            dr["PATIENT_BED_LABEL"] = drSrc["BED_LABEL"].ToString();                            // 床号            
            dr["PATIENT_CAREER"]    = drShow["Career"].ToString();                              // 职业
            dr["PATIENT_NATION"]    = drSrc["NATION"].ToString();;                              // 民族
            
            dr["PATIENT_DEGREE"]    = drShow["Degree"].ToString();                              // 文化程度
            dr["INPPROVIDER"]       = drShow["Provider"].ToString();                            // 病史陈述人
            dr["INPDIAG"]           = drShow["InpDiag"].ToString();                             // 入院诊断
            dr["ASSERTDIAG"]        = drSrc["DIAGNOSIS"].ToString();                            // 确定诊断

            if (drSrc["ADMISSION_DATE_TIME"].ToString().Length > 0)
            {
                DateTime dt = (DateTime)drSrc["ADMISSION_DATE_TIME"];
                
                dr["INP_DATE_YEAR"] = dt.Year.ToString();               // 年
                dr["INP_DATE_MONTH"] = dt.Month.ToString();             // 月
                dr["INP_DATE_DAY"]  = dt.Day.ToString();                // 日
                dr["INP_DATE_HOUR"] = dt.Hour.ToString();               // 时                
                dr["INP_DATE_MINUTE"] = dt.Minute.ToString();           // 分
            }
            
            val = drSrc["INP_APPROACH"].ToString();
            if (val.Equals(chkInpApproach_Outp.Text) == true) { dr["APPROACH_CLINIC"] = CHECK;}     //入院途径-门诊
            if (val.Equals(chkInpApproach_Emergency.Text) == true) { dr["APPROACH_EMERGENCY"] = CHECK;}     //入院途径-急诊
            if (val.Equals(chkInpApproach_Shift.Text) == true) { dr["APPROACH_SHIFT"] = CHECK;}     //入院途径-转入
            
            val = drSrc["CHARGE_TYPE"].ToString();                          // 费用支付
            blnHave = false;
            if (val.Equals(chkChargeType_All.Text) == true) { dr["CHARGE0"] = CHECK; blnHave = true;}     //费用支付-公费医疗
            if (val.Equals(chkChargeType_Big.Text) == true) { dr["CHARGE1"] = CHECK; blnHave = true;}     //费用支付-大病统筹
            if (val.Equals(chkChargeType_Insur.Text) == true) { dr["CHARGE2"] = CHECK; blnHave = true;}     //费用支付-医疗保险
            if (val.Equals(chkChargeType_Self.Text) == true) { dr["CHARGE3"] = CHECK; blnHave = true;}     //费用支付-自费
            if (blnHave == false) {dr["CHARGE4"] = CHECK;};             // 费用支付-其它
            
            val = drShow["InpMode"].ToString();                                                 // 入院诊断
            if (val.Equals(chkInpMode_Foot.Text) == true) { dr["INP_MODE0"] = CHECK; }          //入院方式-步行
            if (val.Equals(chkInpMode_Wheel.Text) == true) {dr["INP_MODE1"] = CHECK; }          //入院方式-轮椅
            if (val.Equals(chkInpMode_Car.Text) == true) {dr["INP_MODE2"] = CHECK; }            //入院方式-平车
            if (val.Equals(chkInpMode_Other.Text) == true) {dr["INP_MODE3"] = CHECK; }          //入院方式-其它
                        
            dr["TEMPERATURE"]       = drShow["Temperature"].ToString();                         // 体温            
            dr["PULSE"]             = drShow["Pulse"].ToString();                               //脉搏            
            dr["HEAR_RATE"]         = drShow["HeartRate"].ToString();                           //心率
            dr["BLOOD_PRESSURE"]    = drShow["BloodPressureH"].ToString() + "/" + drShow["BloodPressureL"].ToString(); // 血压
            
            dr["HEIGHT"]            = drShow["Height"].ToString();                              //身高
            dr["WEIGHT"]            = drShow["Weight"].ToString();                              //体重
            dr["INP_REASON"]        = drShow["InpReason"].ToString();                           //入院原因
            
            val = drShow["IllHis"].ToString().Trim();                                           // 既往史            
            if (val.Length == 0) { dr["ILLHIS_N"] = CHECK; }                // 既往史-无            
            if (val.Length > 0) { dr["ILLHIS_Y"] = CHECK; }                 // 既住史-有
            dr["ILLHIS"] = val;                                             // 既住史
            
            val = drShow["AllergyHis"].ToString().Trim();                   // 过敏史
            if (val.Length == 0) { dr["ALLERGYHIS_N"] = CHECK; }            // 过敏史-无
            if (val.Length > 0) { dr["ALLERGYHIS_Y"] = CHECK; }             // 过敏史-有
            dr["ALLERGYHIS"] = val;                                         // 过敏史
            
            val = drShow["Consciou"].ToString();                                                // 意识状态
            if (val.Equals(chkConscious_1.Text) == true) { dr["CONSCIOUS0"] = CHECK; }          // 意识状态-清醒
            if (val.Equals(chkConscious_2.Text) == true) { dr["CONSCIOUS1"] = CHECK; }          // 意识状态-朦胧
            if (val.Equals(chkConscious_3.Text) == true) { dr["CONSCIOUS2"] = CHECK; }          // 意识状态-嗜睡
            if (val.Equals(chkConscious_4.Text) == true) { dr["CONSCIOUS3"] = CHECK; }          // 意识状态-昏睡
            if (val.Equals(chkConscious_5.Text) == true) { dr["CONSCIOUS4"] = CHECK; }          // 意识状态-昏迷
            
            val = drShow["SighBug"].ToString().Trim();                      // 视障
            if (val.Length == 0) { dr["SIGHBUG_N"] = CHECK; }               // 视力障碍-无
            if (val.Length > 0) { dr["SIGHBUG_Y"] = CHECK; }                // 视力障碍-有
            if (val.Length > 0) { dr["SIGHBUG"] = "(" + val + ")"; }        // 视力障碍-左/右
            
            val = drShow["AuditionBug"].ToString().Trim();                  // 听力障碍
            if (val.Length == 0) { dr["AUDITION_N"] = CHECK; }              // 听力障碍-无
            if (val.Length > 0) { dr["AUDITION_Y"] = CHECK; }               // 听力障碍-有
            if (val.Length > 0) { dr["AUDITION"] = "(" + val + ")"; }       // 听力障碍-左/右
            
            val = drShow["AchePart"].ToString().Trim();                     // 疼痛部位
            if (val.Length == 0) { dr["ACHE_N"] = CHECK; }                  // 疼痛-无
            if (val.Length > 0) { dr["ACHE_Y"] = CHECK; }                   // 疼痛-有
            
            dr["ACHE_PART"] = val;                                          // 疼痛-部位
            dr["ACHE_CLASS"] = drShow["AcheClass"].ToString();              // 疼痛-性质
            
            val = drShow["Diet"].ToString().Trim();                                             // 饮食
            if (val.Equals(chkDiet_1.Text) == true) { dr["DIET0"] = CHECK; }                    // 饮食-普食
            if (val.Equals(chkDiet_2.Text) == true) { dr["DIET1"] = CHECK; }                    // 饮食-半流食
            if (val.Equals(chkDiet_3.Text) == true) { dr["DIET2"] = CHECK; }                    // 饮食-流食
            if (val.Equals(chkDiet_4.Text) == true) { dr["DIET3"] = CHECK; }                    // 饮食-禁食水
            dr["DIET4"] = drShow["DietCure"].ToString();
            
            val = drShow["Sleep"].ToString().Trim();                                            // 睡眠
            if (val.Equals(chkSleep_0.Text) == true) {dr["SLEEP0"] = CHECK;}                    // 睡眠-正常
            if (val.Equals(chkSleep_1.Text) == true) {dr["SLEEP1"] = CHECK;}                    // 睡眠-入睡困难
            if (val.Equals(chkSleep_2.Text) == true) {dr["SLEEP2"] = CHECK;}                    // 睡眠-易醒
            if (val.Equals(chkSleep_3.Text) == true) {dr["SLEEP3"] = CHECK;}                    // 睡眠-多梦
            if (val.Equals(chkSleep_4.Text) == true) {dr["SLEEP4"] = CHECK;}                    // 睡眠-失眠
            if (val.Equals(chkSleep_5.Text) == true) {dr["SLEEP5"] = CHECK;}                    // 睡眠-药物辅助睡眠
            
            val = drShow["Stool"].ToString().Trim();                                            // 大便
            if (val.Length == 0 || val.Equals(chkStool_0.Text) == true) { dr["STOOL0"] = CHECK; } //大便-正常
            if (val.Equals(chkStool_1.Text) == true) { dr["STOOL1"] = CHECK;}                   // 大便-便秘
            if (val.Equals(chkStool_2.Text) == true) { dr["STOOL2"] = CHECK;}                   // 大便-腹泻
            dr["STOOL3"] = drShow["StoolOther"].ToString();                                     // 大便-其它
            
            val = drShow["Pee"].ToString().Trim();                                              // 小便
            if (val.Length == 0 || val.Equals(chkPee_0.Text) == true) { dr["PEE0"] = CHECK; }   // 小便-正常
            if (val.Equals(chkPee_1.Text) == true) { dr["PEE1"] = CHECK; }                      // 小便-尿失禁
            if (val.Equals(chkPee_2.Text) == true) { dr["PEE2"] = CHECK; }                      // 小便-尿潴留
            if (val.Equals(chkPee_3.Text) == true) { dr["PEE3"] = CHECK; }                      // 小便-留置尿管
            dr["PEE4"] = drShow["PeeOther"].ToString();                                         // 小便-其它
            
            val = drShow["Skin"].ToString().Trim();                                             // 皮肤情况
            if (val.Length == 0 || val.Equals(chkSkin_0.Text) == true) { dr["SKIN0"] = CHECK; } // 皮肤情况-正常
            if (val.Equals(chkSkin_1.Text) == true) { dr["SKIN1"] = CHECK;}                     // 皮肤情况-苍白
            if (val.Equals(chkSkin_2.Text) == true) { dr["SKIN2"] = CHECK;}                     // 皮肤情况-紫绀
            if (val.Equals(chkSkin_3.Text) == true) { dr["SKIN3"] = CHECK;}                     // 皮肤情况-黄梁
            if (val.Equals(chkSkin_4.Text) == true) { dr["SKIN4"] = CHECK;}                     // 皮肤情况-水肿
            if (val.Equals(chkSkin_5.Text) == true) { dr["SKIN5"] = CHECK;}                     // 皮肤情况-脱皮
            if (val.Equals(chkSkin_6.Text) == true) { dr["SKIN6"] = CHECK;}                     // 皮肤情况-皮疹
            dr["SKIN7"] = drShow["SkinPart"].ToString();                                        // 皮肤情况-皮疹部位
            
            val = drShow["BedSorePart"].ToString().Trim();                                      // 褥疮部位
            if (val.Length == 0) { dr["BEDSORE_N"] = CHECK; }                                   // 褥疮-无
            if (val.Length > 0) { dr["BEDSORE_Y"] = CHECK; }                                    // 褥疮-有            
            dr["BEDSORE_PART"] = val;                                                           // 褥疮-部位
            
            dr["BEDSORE_DEGREE"] = drShow["BedSoreDegree"].ToString() + "°";                   // 褥疮-程度
            dr["BEDSORE_LEN"] = drShow["BedSoreLen"].ToString();                                // 褥疮-长
            dr["BEDSORE_WIDTH"] = drShow["BedSoreWidth"];                                       // 褥疮-宽
            
            val = drShow["SelfDependDegree"].ToString().Trim();                                 // 自理程度
            if (val.Equals(chkSelfDepend.Text) == true) { dr["SELF_DEPEND0"] = CHECK; }         // 生活-自理
            if (val.Equals(chkSelfDepend_1.Text) == true) { dr["SELF_DEPEND1"] = CHECK; }       //生活-部分自理
            if (val.Equals(chkSelfDepend_2.Text) == true) { dr["SELF_DEPEND2"] = CHECK; }
            
            val = drShow["IllCognition"].ToString().Trim();                                     // 疾病认识
            if (val.Equals(chkIllCognition.Text) == true) { dr["ILLCONG0"] = CHECK; }           // 认识-完全了解
            if (val.Equals(chkIllCognition_1.Text) == true) { dr["ILLCONG1"] = CHECK; }         // 认识-部份了解
            if (val.Equals(chkIllCognition_2.Text) == true) { dr["ILLCONG2"] = CHECK; }         // 认识-不了解
            
            dr["NOTE"] = drShow["Note"].ToString();                                             //备注
            
            dr["NURSE"]     = drShow["Nurse"].ToString();                                       // 护士
            dr["RECDATE"]   = drShow["RecDate"].ToString();                                     // 记录时间
            
            // 入院介绍
            if (dsEduRec != null && dsEduRec.Tables.Count > 0)
            {
                foreach(DataRow drIn in dsEduRec.Tables[0].Rows)
                {
                    val = drIn["EDU_ITEM_NAME"].ToString();
                    
                    if (val.Equals(chkIntroDuct_1.Text) == true) { dr["INTRO0"] = CHECK; }      //介绍-病室环境
                    if (val.Equals(chkIntroDuct_2.Text) == true) { dr["INTRO1"] = CHECK; }      //介绍-作息时间
                    if (val.Equals(chkIntroDuct_3.Text) == true) { dr["INTRO2"] = CHECK; }      //介绍-探视制度
                    if (val.Equals(chkIntroDuct_4.Text) == true) { dr["INTRO3"] = CHECK; }      //介绍-陪住制度
                    if (val.Equals(chkIntroDuct_5.Text) == true) { dr["INTRO4"] = CHECK; }      //介绍-贵重物品管理制度
                    if (val.Equals(chkIntroDuct_6.Text) == true) { dr["INTRO5"] = CHECK; }      //介绍-饮食
                    if (val.Equals(chkIntroDuct_7.Text) == true) { dr["INTRO6"] = CHECK; }      //介绍-主管医生
                    if (val.Equals(chkIntroDuct_8.Text) == true) { dr["INTRO7"] = CHECK; }      //介绍-主管护士
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
		    
		    // 获取行列
		    arrParts = pos.Split(":".ToCharArray());
		    if (arrParts.Length <= 1)
		    {
		        return string.Empty;
		    }
		    
		    row = int.Parse(arrParts[0]);   // 行号
		    col = getCol(arrParts[1]);      // 列号
		    
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