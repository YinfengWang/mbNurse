using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RDLCReport
{
    public partial class frmPageSettings : Form
    {
        public frmPageSettings(string ReportName)
        {
            InitializeComponent();
            this.m_ReportName = ReportName;
        }
        
        /// <summary>
        /// 报表名称，配置文件中一个报表所使用的配置所保存的表名
        /// </summary>
        private string m_ReportName = string.Empty;

        /// <summary>
        /// 用于读取和写入XML配置文件的DataSet
        /// </summary>
        private System.Data.DataSet dsReportSetting = new DataSet();

        /// <summary>
        /// 当前m_ReportName报表配置保存的表
        /// </summary>
        private System.Data.DataTable dtReportSetting = new DataTable();

        /// <summary>
        /// 添加新配置时配置信息保存的行
        /// </summary>
        private System.Data.DataRow drReportSetting = null;


        #region 自定义函数


        /// <summary>
        /// 获取并设置打印机列表
        /// </summary>
        private void GetPrinters()
        {
            System.Collections.ArrayList alPrinters = Printer.GetPrinterList();
            for (int i = 0; i < alPrinters.Count; i++)
                this.cmb打印机.Items.Add(alPrinters[i].ToString());
            alPrinters.Clear();
            alPrinters = null;
        }

        /// <summary>
        /// 通过选中的表单设置页面的高度和宽度
        /// </summary>
        /// <param name="FormName">选中的表单</param>
        private void GetForm(string FormName)
        {
            System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();

            pd.PrinterSettings.PrinterName = this.cmb打印机.Text;

            foreach (System.Drawing.Printing.PaperSize ps in pd.PrinterSettings.PaperSizes)
            {
                if (ps.PaperName == FormName)
                {
                    //in转换为厘米
                    this.txt表单高度.Text = Printer.FromInchToCM(System.Convert.ToDecimal(ps.Height.ToString())).ToString("#.00");
                    this.txt表单宽度.Text = Printer.FromInchToCM(System.Convert.ToDecimal(ps.Width.ToString())).ToString("#.00");
                }
            }            

            pd.Dispose();
        }
        
        /// <summary>
        /// 获取指定打印机的表单列表
        /// </summary>
        private void GetForms()
        {
            this.cmb表单.Items.Clear();

            if (this.cmb打印机.Text.Trim() == string.Empty)
                return;

            System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();

            pd.PrinterSettings.PrinterName = this.cmb打印机.Text;

            foreach (System.Drawing.Printing.PaperSize ps in pd.PrinterSettings.PaperSizes)
                this.cmb表单.Items.Add(ps.PaperName);

            pd.Dispose();
        }

        /// <summary>
        /// 读配置文件，判断配置文件与系统中的配置是否一致
        /// </summary>
        private void ReadSetting()
        {

            this.dsReportSetting.ReadXml(System.Windows.Forms.Application.StartupPath + @"\ReportSettings.xml");

            this.GetPrinters();

            foreach( System.Data.DataTable dt in this.dsReportSetting.Tables )
            {
                if (dt.TableName == this.m_ReportName)
                {
                    this.dtReportSetting = dt;
                    break;
                }
            }

            if (this.dtReportSetting.Rows.Count != 0)
            {
                this.drReportSetting = this.dtReportSetting.Rows[0];

                if (! Printer.PrinterInList(this.drReportSetting["PrinterName"].ToString()))
                {
                    this.epError.SetError(this.cmb打印机, "系统中不存在已经保存在配置文件中的打印机，请重新设置！");
                    return;
                }
                else
                {
                    this.cmb打印机.Text = this.drReportSetting["PrinterName"].ToString();
                    this.GetForms();

                    if (!Printer.FormInPrinter(this.cmb打印机.Text.Trim(), this.drReportSetting["PaperName"].ToString()))
                    {
                        this.epError.SetError(this.cmb表单, "当前打印机中不存在保存在配置文件中的表单，请重新设置！");
                        return;
                    }
                    else
                    {
                        this.cmb表单.Text = this.drReportSetting["PaperName"].ToString();
                        this.GetForm(this.cmb表单.Text);

                        if (!Printer.FormSameSize(this.cmb打印机.Text.Trim(), this.cmb表单.Text.Trim(), System.Convert.ToDecimal(this.txt表单宽度.Text.Trim()), System.Convert.ToDecimal(this.txt表单高度.Text.Trim())))
                        {
                            this.epError.SetError(this.txt表单高度, "保存报表设置时，所填写的报表高度应该和当前所选择的表单高度一致！");
                            this.epError.SetError(this.txt表单宽度, "保存报表设置时，所填写的报表宽度应该和当前所选择的表单宽度一致！");
                        }
                    }

                }

                if (dtReportSetting.Rows[0]["Orientation"].ToString() == "横向")
                    this.rad横向.Checked = true;
                else if (dtReportSetting.Rows[0]["Orientation"].ToString() == "纵向")
                    this.rad纵向.Checked = true;

                this.txt左边距.Text = this.drReportSetting["MarginLeft"].ToString();
                this.txt右边距.Text = this.drReportSetting["MarginRight"].ToString();
                this.txt顶部边距.Text = this.drReportSetting["MarginTop"].ToString();
                this.txt底部边距.Text = this.drReportSetting["MarginBottom"].ToString();

            }
            else
            {
                this.dtReportSetting.TableName = this.m_ReportName;
                this.dtReportSetting.Columns.Add("ReportName", System.Type.GetType("System.String"));
                this.dtReportSetting.Columns.Add("PrinterName", System.Type.GetType("System.String"));
                this.dtReportSetting.Columns.Add("PaperName", System.Type.GetType("System.String"));
                this.dtReportSetting.Columns.Add("PageWidth", System.Type.GetType("System.String"));
                this.dtReportSetting.Columns.Add("PageHeight", System.Type.GetType("System.String"));
                this.dtReportSetting.Columns.Add("MarginTop", System.Type.GetType("System.String"));
                this.dtReportSetting.Columns.Add("MarginBottom", System.Type.GetType("System.String"));
                this.dtReportSetting.Columns.Add("MarginLeft", System.Type.GetType("System.String"));
                this.dtReportSetting.Columns.Add("MarginRight", System.Type.GetType("System.String"));
                this.dtReportSetting.Columns.Add("Orientation", System.Type.GetType("System.String"));
                this.drReportSetting = this.dtReportSetting.NewRow();                
            }

        }

        /// <summary>
        /// 写配置文件，在此之前判断数据的有效性
        /// </summary>
        private bool WriteSetting()
        {

            bool bolRet = false;

            if (this.cmb打印机.Text.Trim() == string.Empty)
            {
                this.epError.SetError(this.cmb打印机, "保存报表设置时，请选择一个打印机！");
                return false;
            }

            if (!Printer.PrinterInList(this.cmb打印机.Text.Trim()))
            {
                this.epError.SetError(this.cmb打印机, "保存报表设置时，所选打印机应该在可用打印机列表中！");
                return false;
            }

            if (this.cmb表单.Text.Trim() == string.Empty)
            {
                this.epError.SetError(this.cmb表单, "保存报表设置时，请选择一个表单！");
                return false;
            }

            if (!Printer.FormInPrinter(this.cmb打印机.Text.Trim(), this.cmb表单.Text.Trim()))
            {
                this.epError.SetError(this.cmb表单, "保存报表设置时，所选表单应该在当前打印机可用的表单列表中！");
                return false;
            }

            decimal decOut = 0;

            if (this.txt表单宽度.Text.Trim() != string.Empty)
            {
                if (!decimal.TryParse(this.txt表单宽度.Text, out decOut))
                {
                    this.epError.SetError(this.txt表单宽度, "保存表单设置时，所填写的表单宽度应该是一个带两位小数的数字！");
                    return false;
                }
            }
            else
            {
                this.epError.SetError(this.txt表单宽度, "保存表单设置时，所填写的表单宽度不能为空！");
                return false;
            }

            if (this.txt表单高度.Text.Trim() != string.Empty)
            {
                if (!decimal.TryParse(this.txt表单高度.Text, out decOut))
                {
                    this.epError.SetError(this.txt表单高度, "保存表单设置时，所填写的表单高度应该是一个带两位小数的数字！");
                    return false;
                }
            }
            else
            {
                this.epError.SetError(this.txt表单高度, "保存表单设置时，所填写的表单高度不能为空！");
                return false;
            }

            if (!Printer.FormSameSize(this.cmb打印机.Text.Trim(), this.cmb表单.Text.Trim(), System.Convert.ToDecimal(this.txt表单宽度.Text.Trim()), System.Convert.ToDecimal(this.txt表单高度.Text.Trim())))
            {
                this.epError.SetError(this.txt表单高度, "保存报表设置时，所填写的报表高度应该和当前所选择的表单高度一致！");
                this.epError.SetError(this.txt表单宽度, "保存报表设置时，所填写的报表宽度应该和当前所选择的表单宽度一致！");
                return false;
            }

            if (!decimal.TryParse(this.txt左边距.Text, out decOut))
            {
                this.epError.SetError(this.txt左边距, "保存表单设置时，所填写的左边距应该是一个带两位小数的数字！");
                return false;
            }

            if (!decimal.TryParse(this.txt右边距.Text, out decOut))
            {
                this.epError.SetError(this.txt右边距, "保存表单设置时，所填写的右边距应该是一个带两位小数的数字！");
                return false;
            }

            if (!decimal.TryParse(this.txt顶部边距.Text, out decOut))
            {
                this.epError.SetError(this.txt顶部边距, "保存表单设置时，所填写的顶部边距应该是一个带两位小数的数字！");
                return false;
            }

            if (!decimal.TryParse(this.txt底部边距.Text, out decOut))
            {
                this.epError.SetError(this.txt底部边距, "保存表单设置时，所填写的底部边距应该是一个带两位小数的数字！");
                return false;
            }
            
            this.drReportSetting["ReportName"] = this.m_ReportName;
            this.drReportSetting["PrinterName"] = this.cmb打印机.Text.Trim();
            this.drReportSetting["PaperName"] = this.cmb表单.Text.Trim();
            this.drReportSetting["PageWidth"] = this.txt表单宽度.Text.Trim();
            this.drReportSetting["PageHeight"] = this.txt表单高度.Text.Trim();

            if (this.txt顶部边距.Text.Trim() != string.Empty)
                this.drReportSetting["MarginTop"] = this.txt顶部边距.Text.Trim();
            else
                this.drReportSetting["MarginTop"] = "0";

            if (this.txt底部边距.Text.Trim() != string.Empty)
                this.drReportSetting["MarginBottom"] = this.txt底部边距.Text.Trim();
            else
                this.drReportSetting["MarginBottom"] = "0";

            if (this.txt左边距.Text.Trim() != string.Empty)
                this.drReportSetting["MarginLeft"] = this.txt左边距.Text.Trim();
            else
                this.drReportSetting["MarginLeft"] = "0";

            if (this.txt右边距.Text.Trim() != string.Empty)
                this.drReportSetting["MarginRight"] = this.txt右边距.Text.Trim();
            else
                this.drReportSetting["MarginRight"] = "0";

            if (this.rad横向.Checked)
                this.drReportSetting["Orientation"] = "横向";
            else
                this.drReportSetting["Orientation"] = "纵向";

            if (this.dtReportSetting.Rows.IndexOf(this.drReportSetting) == -1)
                this.dtReportSetting.Rows.Add(this.drReportSetting);

            if (this.dsReportSetting.Tables.IndexOf(this.dtReportSetting) == -1)
                this.dsReportSetting.Tables.Add(this.dtReportSetting);

            this.dsReportSetting.WriteXml(System.Windows.Forms.Application.StartupPath + @"\ReportSettings.xml");

            bolRet = true;

            return bolRet;

        }
        

        #endregion


        #region 系统事件


        private void frmPageSettings_Load(object sender, EventArgs e)
        {
            this.ReadSetting();
        }
        
        private void cmb打印机_SelectedValueChanged(object sender, EventArgs e)
        {
            //Pub.WinForm.Msg.Information(this.cmb打印机.Text);
            this.GetForms();
        }

        private void btn状态_Click(object sender, EventArgs e)
        {
            //Pub.WinForm.Msg.Information("打印机“" + this.cmb打印机.Text + "”当前状态： \r\n    " + Printer.GetPrinterStatus(this.cmb打印机.Text));
        }

        private void btn默认打印机_Click(object sender, EventArgs e)
        {
            this.cmb打印机.Text = Printer.GetDeaultPrinterName();
            this.GetForms();
            this.cmb表单.Text = string.Empty;
            this.txt表单宽度.Text = string.Empty;
            this.txt表单高度.Text = string.Empty;
        }

        private void btn添加表单_Click(object sender, EventArgs e)
        {
            if (this.cmb打印机.Text.Trim() != string.Empty && this.txt表单.Text.Trim() != string.Empty && this.txt表单宽度.Text.Trim() != string.Empty && this.txt表单高度.Text.Trim() != string.Empty)
            {

                System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();

                pd.PrinterSettings.PrinterName = this.cmb打印机.Text.Trim();

                bool bolExist = false;

                foreach (System.Drawing.Printing.PaperSize ps in pd.PrinterSettings.PaperSizes)
                {
                    if (ps.PaperName == this.txt表单.Text.Trim())
                    {
                        bolExist = true;
                        break;
                    }
                }

                pd.Dispose();

                if (bolExist)
                {
                    Pub.WinForm.Msg.Warning("要添加的表单已经存在，请删除以后再添加！");
                    return;
                }

                Printer.AddCustomPaperSize(this.cmb打印机.Text, this.txt表单.Text.Trim(), System.Convert.ToSingle(this.txt表单宽度.Text.Trim()) * 10, System.Convert.ToSingle(this.txt表单高度.Text.Trim()) * 10);
                this.GetForms();
                this.cmb表单.Text = this.txt表单.Text.Trim();
            }
            else
            {
                decimal decOut = 0;
                if (this.cmb打印机.Text.Trim() == string.Empty)
                    this.epError.SetError(this.cmb打印机, "添加自定义表单时，请选择一个可用的打印机！");
                if (this.txt表单.Text.Trim() == string.Empty)
                    this.epError.SetError(this.txt表单, "添加自定义表单时，请填写一个合适的表单名！");
                if (this.txt表单高度.Text.Trim() == string.Empty || !System.Decimal.TryParse(this.txt表单高度.Text.Trim(), out decOut))
                    this.epError.SetError(this.txt表单高度, "添加自定义表单时，请指定一个数字型的表单高度！");
                if (this.txt表单宽度.Text.Trim() == string.Empty || !System.Decimal.TryParse(this.txt表单宽度.Text.Trim(), out decOut))
                    this.epError.SetError(this.txt表单宽度, "添加自定义表单时，请指定一个数字型的表单宽度！");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.cmb表单.Text.Trim() != string.Empty && this.cmb打印机.Text.Trim() != string.Empty)
            {
                if (Pub.WinForm.Msg.Question("确定删除选中的表单吗？") == DialogResult.Yes)
                {
                    Printer.DeleteCustomPaperSize(this.cmb打印机.Text.Trim(), this.cmb表单.Text.Trim());
                    this.GetForms();
                    this.cmb表单.Text = string.Empty;
                }
            }
            else
            {
                if (this.cmb表单.Text.Trim() == string.Empty)
                    this.epError.SetError(this.cmb表单, "删除自定义表单时，请选择一个自定义表单！");
                if (this.cmb打印机.Text.Trim() == string.Empty)
                    this.epError.SetError(this.cmb打印机, "删除自定义表单时，请选择一个打印机！");
            }
        }

        private void cmb表单_SelectedValueChanged(object sender, EventArgs e)
        {
            this.GetForm(this.cmb表单.Text);
        }

        private void btn确定_Click(object sender, EventArgs e)
        {
            if (!this.WriteSetting())
                return;
            
            //清除使用的对象变量所占用的资源
            this.dtReportSetting.Dispose();

            this.dsReportSetting.Dispose();

            this.Close();

        }

        private void btn取消_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion

    }
}