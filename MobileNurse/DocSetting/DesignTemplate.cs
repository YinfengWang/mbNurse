using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DocDAL;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
namespace HISPlus
{
    public partial class DesignTemplate : FormDo, IDocNodeParent,IBasePatient
    {
        private const string RIGHT_EDIT = "1";                      //  录入权限
        private const string RIGHT_MODIFY = "2";                      //  编辑别人录入记录的权限

        ///<summary>
        /// 病人评估单
        ///</summary>
        private DocNursingDbi _docNursingDbi;
        /// <summary>
        /// 模板ID
        /// </summary>
        private decimal _templateId;

        /// <summary>
        /// 列宽
        /// </summary>
        public int ColumnWidth = 60;
        /// <summary>
        /// 边框
        /// </summary>
        public int Border = 12;

        /// <summary>
        /// 界面总宽度.已减去左右边框
        /// </summary>
        public int LayoutWidth = 803 - 12 * 2;

        private int _pageIndex;

        private Rectangle _rectPrintBound;
        private int _pageCount;

        float ZoomValue = 1f;

        private const int SpaceBetweenRows = 5;

        /// <summary>
        /// 名称和控件之前的间距
        /// </summary>
        public const int SpaceNameControl = 30;/*2015.10.09原为3*/

        /// <summary>
        /// 模板元素List
        /// </summary>
        public List<DocTemplateElement> ListTemplateElements;

        public DataSet docNursingRecordDs = new DataSet();

        /// <summary>
        /// 文书护理信息
        /// </summary>
        private DocNursing _nursing;

        /// <summary>
        /// 父级容器
        /// </summary>
        public Control ParentControl;

        /// <summary>
        /// 是否加载元素数据
        /// </summary>
        private readonly bool _reloadData;

        public string PatientId;

        public string VisitId;

        /// <summary>
        /// 表格展示界面
        /// </summary>
        private bool _isGrid = false;

        [Browsable(false)]
        public DocNodeCollection1 ChildNodes { get; private set; }

        /// <summary>
        /// 默认行高
        /// </summary>
        public const int DefaultRowHeight = 28;

        /// <summary>
        /// 护理数据集合
        /// </summary>
        private List<DocNursing> _listNursing;

        public DesignTemplate()
        {
            InitializeComponent();
        }

        public DesignTemplate(decimal templateId, string formText = null, bool reloadData = true)
            : this(templateId, formText, reloadData, isGrid: false)
        {
        }

        public DesignTemplate(decimal templateId, string formText = null, bool reloadData = true, bool isGrid = false)
        {
            InitializeComponent();

            this._templateId = templateId;
            this._reloadData = reloadData;
            this._isGrid = isGrid;
            if (formText == null)
            {
                groupControl1.Visible = false;
                groupControl2.Visible = false;
                this.Text = @"文书预览";
                this.Width = xtraScrollableControl1.DisplayRectangle.Width + 50;
                xtraScrollableControl1.Dock = DockStyle.Fill;
            }
            else
            {
                this.Text = formText;
            }

            ucGridView1.RowStyle += gvDefault_RowStyle; //2015.12.18
        }

        private void DesignTemplate_Load(object sender, EventArgs e)
        {
            try
            {
                _id = "00100";
                _guid = "CC118166-670D-4cec-84E4-EDE789DD611E";
                _right = @"0:查看
                        1:录入
                        2:修改";

                this.ChildNodes = new DocNodeCollection1(this);
                _docNursingDbi = new DocNursingDbi(GVars.OracleAccess);
                this.PatientId = GVars.Patient.ID;
                this.VisitId = GVars.Patient.VisitId;
                
                //2015.12.04 add 2016.01.19 del
                //if (GVars.Patient.STATE == HISPlus.PAT_INHOS_STATE.OUT)
                //{
                //    btnNew.Enabled = false;
                //    btnSave.Enabled = false;
                //}
                ParentControl = panelControl1;
                //ParentControl.Width = 827;
                //ParentControl.Width = 803;

                //_layoutWidth = ParentControl.Width - ParentControl.Padding.Right - ParentControl.Padding.Left;
                if (_reloadData)
                {
                    _userRight = GVars.User.GetUserFrmRight(_id);

                    ucGridView1.Add("Id", "Id", false);
                    ucGridView1.Add("创建日期", "CreateTimestamp", ComConst.FMT_DATE.LONG_MINUTE,140);
                    ucGridView1.Add("总分", "TotalScore");
                    ucGridView1.Add("达标", "Standard", false);
                    //ucGridView1.ShowHeaders = false;
                    //ucGridView1.ShowFindPanel = true;//2015.12.07 true->false --->del
                    ucGridView1.SelectionChanged += ucGridView1_SelectionChanged;
                    ucGridView1.Init();

                    InitData();

                    if (ucGridView1.SelectRowsCount == 0)
                    {
                        this.NewForm(_templateId);
                    }
                }
                else
                {
                    if (_isGrid && ListTemplateElements != null && ListTemplateElements.Count > 0)
                    {
                        this.LayoutForm(ListTemplateElements);
                        PickBox pb = new PickBox();
                        foreach (Control ctl in ParentControl.Controls)
                        {
                            if (ctl is PictureBox)
                            {
                                continue;
                            }

                            BaseNode node = ctl.Tag as BaseNode;
                            if (node == null) continue;

                            if (node is TextBoxNode)
                            {
                                pb.WireControl(node._lblName, (node as TextBoxNode).TxtControl);
                                continue;
                            }

                            pb.WireControl(ctl);
                        }

                        pnlBottom.Visible = true;

                        foreach (Control ctl in ParentControl.Controls)/*2015.10.05 del*/
                        {
                            if (ctl is WebBrowser)
                                continue;
                            ctl.BringToFront();
                        }
                    }
                    else
                        this.NewForm(_templateId);
                }

                //foreach (Control ctl in ParentControl.Controls)/*2015.10.05 del*/
                //{
                //    if (ctl is WebBrowser)
                //        continue;
                //    ctl.BringToFront();
                //}

                panelControl2.MinimumSize = new Size(800, Math.Max(660, ParentControl.Height));

                SetButtonEnabled();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 根据权限设置按钮是否可用
        /// </summary>
        private void SetButtonEnabled()
        {
            // 怎么简单怎么来。
            return;
            if (!_reloadData) return;
            // 保存权限
            btnSave.Enabled = (PatientId.Length > 0) && (_userRight.IndexOf(RIGHT_EDIT) >= 0 || _userRight.IndexOf(RIGHT_MODIFY) >= 0);

            // 删除权限取自编辑权限
            btnDelete.Enabled = _nursing.CreateUser.Equals(GVars.User.Name) || _right.IndexOf(RIGHT_MODIFY) >= 0;

            // 录入权限
            btnNew.Enabled = _userRight.IndexOf(RIGHT_EDIT) >= 0;
        }

        private void InitData()
        {
            string id = null;
            if (this._nursing != null)
                id = _nursing.Id;


            _listNursing = EntityOper.GetInstance().FindByProperty<DocNursing>(new[] { "PatientId", "VisitNo", "TemplateId" },
                        new object[] { PatientId, VisitId, _templateId }).OrderBy(p => p.CreateTimestamp).ToList();

            _nursing = null;
            docNursingRecordDs = null;

            getDocNotStandard();//2015.12.18
            ucGridView1.DataSource = _listNursing;

            if (id != null)
                ucGridView1.SelectRow("Id", id);

        }

        void ucGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                base.LoadingShow();

                this.Cursor = Cursors.WaitCursor;
                DocNursing model = ucGridView1.GetSelectRow() as DocNursing;
                if (model == null) return;


                this._nursing = model;

                this.docNursingRecordDs = _docNursingDbi.GetDocNursingRecordDs(_nursing.Id);

                this.NewForm(_templateId);
                SetButtonEnabled();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                base.LoadingClose();
                this.Cursor = Cursors.Default;
            }
        }

        public void AddControl(Control control, Point location)
        {
            //if (!_parentControl.Controls.Contains(control))
            //if (control is PictureBox || control is WebBrowser)
            //    control.SendToBack();
            //else
            //    control.BringToFront();

            control.Location = location;
            ParentControl.Controls.Add(control);
        }

        internal BaseNode FindNode(int id)
        {
            return this.FindNode(id, this.ChildNodes);
        }

        private BaseNode FindNode(int id, DocNodeCollection1 nodes)
        {
            //foreach (BaseNode node in nodes)
            //{
            //    if (node.i == id)
            //    {
            //        return node;
            //    }
            //    if (node.ChildNodes.Count > 0)
            //    {
            //        BaseNode node2 = this.FindNode(id, node.ChildNodes);
            //        if (node2 != null)
            //        {
            //            return node2;
            //        }
            //    }
            //}
            return null;
        }


        public void AddGrid()
        {
            WebBrowser webBrowser1 = new WebBrowser();
            this.AddControl(webBrowser1, new Point(20, 1000));
            //.Controls.Add(webBrowser1);
            webBrowser1.Width = 800;
            webBrowser1.Height = 800;

            //设置一个对COM可见的对象(上面已将该类设置对COM可见)
            //this.webBrowser1.ObjectForScripting = this;
            //禁止显示拖放到webBrowser上的文件
            webBrowser1.AllowWebBrowserDrop = false;
            //禁止使用IE浏览器快捷键
            webBrowser1.WebBrowserShortcutsEnabled = false;
            //禁止显示右键菜单
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.ScrollBarsEnabled = false;

            //禁止显示脚本错误
            //this.webBrowser1.ScriptErrorsSuppressed = true;

            //webBrowser1.Url = new Uri(@"E:\SVN\mobilenurse\MobileNurse\Html\1.html"); 
            webBrowser1.Url = new Uri(@"D:\文件\635624599784816800.files\sheet002.html");
            //webBrowser1.Url = new Uri("E:\\PICC护理记录表.xls");
            //webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
            webBrowser1.Show();
        }

        public void LayoutForm(IEnumerable<DocTemplateElement> ilist)
        {
            List<DocTemplateElement> list = ilist.OrderBy(p => p.ParentId).ThenBy(p => p.SortId).ToList();

            this.ListTemplateElements = list;

            foreach (DocTemplateElement node in list)
            {
                //if (node.ParentId != 0) continue;

                // 如果节点的父节点存在,则跳过
                DocTemplateElement node1 = node;
                if (list.Any(p => p.Id == node1.ParentId))
                    continue;

                BaseNode item = BaseNode.NewDocNode(this, node);

                if (item != null)
                {
                    this.ChildNodes.Add(item);

                    item.Layout();
                }
            }

            SetChecked(this.ChildNodes);

            //Dictionary<int, BaseNode> dict = new Dictionary<int, BaseNode>();
            ////this.SetNodeDict(this._docNodes, dict);
            //Cursor.Current = Cursors.WaitCursor;
            //try
            //{
            //    base.SuspendLayout();
            //    base.VerticalScroll.Value = 0;
            //    base.HorizontalScroll.Value = 0;
            //    foreach (Control control in base.Controls)
            //    {
            //        control.Dispose();
            //    }
            //    base.Controls.Clear();
            //    Point point = new Point(parentControl.Padding.Left, parentControl.Padding.Top);
            //    int y = point.Y;
            //    foreach (BaseNode node3 in this.DocNodes)
            //    {
            //        if ((node3 is TotalScoreTextBoxNode) && dict.ContainsKey(node3.NursingDocNode.referenceCode))
            //        {
            //            (node3 as TotalScoreTextBoxNode).SetReferenceNode(dict[node3.NursingDocNode.referenceCode]);
            //        }
            //        node3.Layout();
            //    }
            //}
            //finally
            //{
            //    base.ResumeLayout();
            //    Cursor.Current = Cursors.Default;
            //}
        }

        private void SetChecked(IEnumerable<BaseNode> nodes)
        {

            foreach (BaseNode node in nodes)
            {
                if ((node is TotalScoreTextBoxNode))
                {
                    (node as TotalScoreTextBoxNode).SetReferenceNode(node.ChildNodes);//2015.11.06 this.ChildNodes ->
                }
                SetChecked(node.ChildNodes);
            }
        }

        private void NewForm(decimal templateId)
        {
            Cursor.Current = Cursors.Default;
            try
            {
                this.ChildNodes.Clear();
                ParentControl.Controls.Clear();

                this._templateId = templateId;
                
                IList<DocTemplateElement> ilist =
                    EntityOper.GetInstance().FindByProperty<DocTemplateElement>("DocTemplate.Id", templateId);
                this.LayoutForm(ilist);

                foreach (Control ctl in ParentControl.Controls)/*2015.10.05 add*/
                {
                    if (ctl is WebBrowser)
                        continue;
                    ctl.BringToFront();
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }



        private void Save(IEnumerable<BaseNode> nodes, ref double totalScore)
        {
            foreach (BaseNode node in nodes)
            {
                BaseNode node1 = node;
                if (docNursingRecordDs == null || docNursingRecordDs.Tables.Count == 0)
                    docNursingRecordDs = _docNursingDbi.GetDocNursingRecordDs(this._nursing.Id);
                DataRow[] rows = docNursingRecordDs.Tables[0].Select("DOC_ELEMENT_ID='" + node1.NursingDocNode.Id + "'");
                DataRow recordRow = null;
                if (rows.Length > 0)
                    recordRow = rows[0];

                if (((node is CheckBoxNode) && (node.Value != null)) && (node.Value.ToString() == "1"))
                {
                    totalScore += node.NursingDocNode.Score;
                }

                // 文本框,值不为空,分值>0的节点
                if ((node is TextBoxNode) && (node.Value != null) && node.NursingDocNode.Score > 0)
                {
                    float f;
                    float.TryParse(node.Value.ToString(), out f);
                    totalScore += f;
                }

                if (node.Value != null)
                {
                    if (recordRow == null)
                    {
                        DataRow newRow = docNursingRecordDs.Tables[0].NewRow();
                        string id = Guid.NewGuid().ToString().Replace("-", "");
                        newRow["ID"] = id;
                        newRow["DOC_NURSING_ID"] = this._nursing.Id;
                        newRow["DOC_ELEMENT_ID"] = node.NursingDocNode.Id;
                        newRow["CREATE_TIMESTAMP"] = now;
                        newRow["UPDATE_TIMESTAMP"] = now;
                        if (node.NursingDocNode.DataType == (int)Enums.DataType.字符串)
                        {
                            newRow["STRING_VALUE"] = node.Value.ToString();
                            newRow["NUMBER_VALUE"] = 0;
                        }
                        else
                        {
                            newRow["STRING_VALUE"] = null;
                            newRow["NUMBER_VALUE"] = Convert.ToDecimal(node.Value);
                        }
                        docNursingRecordDs.Tables[0].Rows.Add(newRow);
                    }
                    else   //库里有值，界面有值，修改库里的值。
                    {
                        if (node.NursingDocNode.DataType == (int)Enums.DataType.字符串)
                        {
                            if (recordRow["STRING_VALUE"].ToString() != node.Value.ToString())
                            {
                                recordRow["STRING_VALUE"] = node.Value.ToString();
                                recordRow["UPDATE_TIMESTAMP"] = now;
                            }
                        }
                        else
                        {
                            if (recordRow["NUMBER_VALUE"].ToString() != node.Value.ToString())
                            {
                                recordRow["NUMBER_VALUE"] = Convert.ToDecimal(node.Value);
                                recordRow["UPDATE_TIMESTAMP"] = now;
                            }
                        }

                    }
                }
                else if (recordRow != null)
                {                    
                    recordRow.Delete();
                }
                this.Save(node.ChildNodes, ref totalScore);
            }
        }

        private DateTime now;
        private void SaveNursing()
        {
            now = GVars.OracleAccess.GetSysDate();
            if (this._nursing == null)
            {
                this._nursing = new DocNursing
                {
                    PatientId = PatientId,
                    VisitNo = VisitId,
                    WardCode = GVars.User.DeptCode,
                    TemplateId = this._templateId,
                    CreateTimestamp = now,
                    UpdateTimestamp = now,
                    CreateUser = GVars.User.UserName,
                    UpdateUser = GVars.User.UserName
                };

                _listNursing.Add(_nursing);
            }
            else
            {
                this._nursing.UpdateTimestamp = now;
                this._nursing.UpdateUser = GVars.User.UserName;
            }

            double totalScore = 0.0;
            Save(this.ChildNodes, ref totalScore);
            _nursing.TotalScore = totalScore;

            EntityOper.GetInstance().SaveOrUpdate(_nursing);

            DataRow[] addedRows = docNursingRecordDs.Tables[0].Select("", "", DataViewRowState.Added);
            foreach (DataRow row in addedRows)
            {
                row["DOC_NURSING_ID"] = _nursing.Id;
            }

            _docNursingDbi.SaveDocNursingRecordDs(docNursingRecordDs);

            XtraMessageBox.Show("保存成功!");
        }

        private void Print(string printerName, bool isPreview)
        {
            this._pageIndex = 0;
            PrintDocument document = new PrintDocument
            {
                DocumentName = printerName,
                PrintController = new StandardPrintController()
            };
            document.DefaultPageSettings.Margins = new Margins(ParentControl.Padding.Left, ParentControl.Padding.Right + 12, ParentControl.Padding.Top, ParentControl.Padding.Bottom);
            document.DefaultPageSettings.PrinterSettings.Duplex = Duplex.Default;// Duplex.Vertical;
            //2016.12.26 判断纵向、横向，该条件有待确认
            if (document.DefaultPageSettings.PaperSize.Width < ParentControl.Width)
                document.DefaultPageSettings.Landscape = true;
            else
                document.DefaultPageSettings.Landscape = false;
            document.PrintPage += this.printDoc_PrintPage;
            if (!isPreview)
            {
                document.Print();
            }
            else
            {
                PrintPreviewDialog dialog = new PrintPreviewDialog
                {
                    Document = document,
                    ShowIcon = false
                };
                dialog.PrintPreviewControl.Zoom = 1.0;
                dialog.ShowDialog();
            }
        }

        /// <summary>
        /// 标题控件.【注：标题控件必须为文书根据节点的第一项!】
        /// </summary>
        private Control _titleControl;
        private int _buttomHeight = 16;
        Rectangle _titleRect;
        //private int _topHeight;

        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            #region
            this._rectPrintBound = e.MarginBounds;//2015.10.18 add 将所有e.MarginBounds改为_rectPrintBound
            _rectPrintBound.Width -= 30;
            _rectPrintBound.Height -= 50;
            
            #endregion
            //ZoomValue = (float)ParentControl.Width / (float)e.MarginBounds.Width;//2015.10.18 add      
            ZoomValue = 1f;//2015.10.18 del mod

            #region 2015.10.13 计算页数，因将标题高度、页码高度都减去（标题高度从控件中取出，页码高度固定为16f）
            //this._pageCount = (int)(Math.Ceiling(((ParentControl.Height / ZoomValue) - 15) / e.MarginBounds.Height));
            if (ParentControl.Controls.Count > 0)
            {
                _titleControl = ParentControl.Controls[ParentControl.Controls.Count - 1];
            }
            //页数 = 容器正文部分高度 / 打印页面正文部分高度，即：
            //[（容器高度 - 标题高度 - 底部高度 - 上下页边距）/ 缩放比例] / [ 打印页面边距内高度 - (标题高度 + 底部高度）/ 缩放比例 ]
            //-----------------------------------------------------
            //2016.05.19 del
            //this._pageCount = (int)(Math.Ceiling(
            //                            ((ParentControl.Height - (_titleControl.Height + _titleControl.Top )- _buttomHeight - ParentControl.Padding.Top - ParentControl.Padding.Bottom) / ZoomValue)
            //                             /
            //                             (_rectPrintBound.Height - (_titleControl.Height + _buttomHeight) / ZoomValue)//2015.10.18 add 将所有e.MarginBounds改为_rectPrintBound
            //                         ));
            //-----------------------------------------------------
            //2016.05.19 add
            if (String.IsNullOrEmpty(_titleControl.Text))
            {
                this._pageCount = (int)(Math.Ceiling(
                                            ((ParentControl.Height - /*(_titleControl.Height + _titleControl.Top) -*/ _buttomHeight - ParentControl.Padding.Top - ParentControl.Padding.Bottom) / ZoomValue)
                                             /
                                             (_rectPrintBound.Height - /*(_titleControl.Height +*/ _buttomHeight) / ZoomValue)//2015.10.18 add 将所有e.MarginBounds改为_rectPrintBound
                                         );
            }
            else
            {
                this._pageCount = (int)(Math.Ceiling(
                                        ((ParentControl.Height - (_titleControl.Height + _titleControl.Top) - _buttomHeight - ParentControl.Padding.Top - ParentControl.Padding.Bottom) / ZoomValue)
                                         /
                                         (_rectPrintBound.Height - (_titleControl.Height + _buttomHeight) / ZoomValue)//2015.10.18 add 将所有e.MarginBounds改为_rectPrintBound
                                     ));
            }
            //-----------------------------------------------------
            #endregion

            this.PrintPage(e.Graphics, this._pageIndex, 1f);
            this.PrintButtom(e.Graphics);
            //if (((int)(((ParentControl.Height / ZoomValue) - 15) / _rectPrintBound.Height)) > this._pageIndex)//2015.10.18 mod 将所有e.MarginBounds改为_rectPrintBound//2015.10.19del
            if (this._pageCount - 1 > this._pageIndex)
            {
                e.HasMorePages = true;
                this._pageIndex++;
            }
            else
            {
                this._pageIndex = 0;
            }
        }

        private void PrintButtom(Graphics g)
        {
            using (StringFormat format = new StringFormat())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Far;
                RectangleF layoutRectangle = new RectangleF(this._rectPrintBound.Left, this._rectPrintBound.Height - 6, this._rectPrintBound.Width, _buttomHeight);//2015.10.13 16f);//2015.10.18 
                //g.DrawRectangle(new Pen(Color.Green, 1), layoutRectangle.X, layoutRectangle.Y, layoutRectangle.Width, layoutRectangle.Height);//2015.10.17 add debug
                g.DrawString("第" + (this._pageIndex + 1) + "页 共" + this._pageCount + "页", new Font("宋体", 9f), Brushes.Black, layoutRectangle, format);
            }
        }

        private void PrintPage(Graphics g, int pageIndex, float verticalScaleFactor)
        {
            #region 测试debug，修正名称控件的宽高 2015.10.10
            int iFixHeight = 5, iFixWidth = 50;
            //g.DrawRectangle(new Pen(Color.Black, 1), _rectPrintBound.X, _rectPrintBound.Y, _rectPrintBound.Width, _rectPrintBound.Height);//2015.10.17 add debug
            
            #endregion

            if (ParentControl.Controls.Count > 0 && pageIndex == 0)
            {
                _titleControl = ParentControl.Controls[ParentControl.Controls.Count - 1];
                //TitleControl.Text = TitleControl.Text + "(续)";
            }

            if (_titleControl == null) return;

            {
                #region 2015.10.13 标题y坐标 = 打印页面边距内
                #endregion
                int y = (((int)(((_titleControl.Top + VerticalScroll.Value) / ZoomValue) * verticalScaleFactor))) +
                        _rectPrintBound.Top;
                int height = (int)Math.Ceiling(_titleControl.Height / ZoomValue) + iFixHeight;

                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                if (_titleControl is LabelControl)
                {
                    LabelControl label = _titleControl as LabelControl;

                    Rectangle layoutRectangle = new Rectangle(this._rectPrintBound.Left, y,//2015.10.18 RectangleF -> Rectangle
                        this._rectPrintBound.Width, height);
                    
                   g.DrawString(label.Text + (pageIndex == 0 ? "" : "(续)"), label.Font, Brushes.Black, layoutRectangle,
                        format);
                    _titleRect = layoutRectangle;//2015.10.14 add
                    //g.DrawRectangle(new Pen(Color.Red, 1), _titleRect);//2015.10.17 add debug
                    //g.DrawRectangle(new Pen(Color.Blue, 1), _rectPrintBound);
                    //g.DrawString(label.Text + "(续)", label.Font, new SolidBrush(label.ForeColor), new Rectangle(x, y, width + 60, height), format);
                }
            }

            /*
             * 打印逻辑
             * 1. 在每页都打印标题
             * 2. 因为每页都打印标题,所以需要把总高度减去标题高度
             * 3. 需要避免控件无法打印的情况,即高度大于当前页,却小于下一页
             * */

            // 标题高度,因为标题本身的高度并不足够,所以又加了一些,以保证打印效果
            //int topHeight = _titleControl.Height + Border + 10 + iFixHeight;//2015.10.14 //2015.10.18 del
            //int topHeight = _titleRect.Height;//2015.10.18 add
            int topBottom = _titleRect.Bottom - _rectPrintBound.Top;//2015.10.18 标题的底部和可打印区域的顶部高度差
            int topTop = _titleRect.Top - _rectPrintBound.Top;//2016.05.19 add
            // 16是页面底部的【第几页,共几页】的高度
            //int totalHeight = _rectPrintBound.Height - 16 - topHeight - _rectPrintBound.Top; //2015.10.14 del
            //-------------------------
            //2016.05.19 del
            //int totalHeight = _rectPrintBound.Height - (_buttomHeight /*+ 6*/ ) - topBottom;//2015.10.14 add//2015.10.18 topHeight
            int totalHeight;
            //---------------------------
            if (String.IsNullOrEmpty(_titleControl.Text))
            {
                totalHeight = _rectPrintBound.Height ; // (_buttomHeight /*+ 6*/ )- topBottom//2015.10.14 add//2015.10.18 topHeight
            }
            else
            {
                totalHeight = _rectPrintBound.Height - (_buttomHeight /*+ 6*/ ) - topBottom;//2015.10.14 add//2015.10.18 topHeight
            }
            //g.DrawRectangle(new Pen(Color.Blue, 1), _rectPrintBound.X, _titleRect.Bottom, _rectPrintBound.Width, totalHeight);//2015.10.17 add debug
            //for (int i = 1; i < ParentControl.Controls.Count; i++)
            int iTemp = 50;//2016.05.19
            for (int i = ParentControl.Controls.Count - 2; i >= 0; i--)
            {
                Control control = ParentControl.Controls[i];
                if (!control.Visible)
                    continue;

                //if (control.Text.Contains("手术安全核查"))
                //{
                //}

                if (control.Text.Contains("现状"))
                {
                }


                int x = ((int)((control.Left + HorizontalScroll.Value) / ZoomValue)) + _rectPrintBound.Left;

                /*
                 * Y坐标 减去标题高度并在判断坐标是否在打印页面之内后加上此高度,目的是:                 
                 * 使界面整体下移标题高度.
                 * 因为界面高度已减去标题高度,所以下移不会出界                 
                 * */
                //int y = ((int)(((control.Top + VerticalScroll.Value) / ZoomValue) * verticalScaleFactor)) -   //2015.10.18 del
                //        (totalHeight * pageIndex)
                //        - topHeight + (int)(Border * 1.5);
                //int y = (control.Top - topHeight ) - (totalHeight * pageIndex); //2015.10.14
                int y = (control.Top - _titleControl.Bottom) - ( totalHeight * pageIndex); //2015.10.18 add - _titleControl.Top
                //------------------------------
                //2016.05.19
                //if (String.IsNullOrEmpty(_titleControl.Text))
                //{
                //    y = (control.Top ) - (totalHeight * pageIndex);
                //}
                //else
                //{
                //    y = (control.Top - _titleControl.Bottom) - (totalHeight * pageIndex);
                //}
                //--------------------------------
                int width = (int)Math.Ceiling(control.Width / ZoomValue);
                int height = (int)Math.Ceiling(control.Height / ZoomValue);

                //if (pageIndex == 0)
                //{
                //    if (y + Border * 3 < 0 || (y >= totalHeight)) continue;
                //}
                //else
                {
                    if (y < 0 || ((y /*+ height*/) >= totalHeight)) continue;
                }

                //y += topHeight;//2015.10.18 del
                //-------------------------------
                //2016.05.19 del
                //y = y + _titleRect.Bottom;//2015.10.18 y之前是偏移量,加_titleRect.bottom后为实际坐标 
                //-------------------------------

                //y += topBottom;

                //-----------------------------
                //2016.05.19 add
                if (String.IsNullOrEmpty(_titleControl.Text))
                {
                    y = y + _titleRect.Top;// _titleControl;// _titleRect.Top;
                    if (i == ParentControl.Controls.Count - 2)
                    {
                        //MessageBox.Show("标题为空", "坐标" + y.ToString());
                        if (y < iTemp) iTemp = y;
                    }
                    y = y - iTemp;
                }
                else
                {
                    y = y + _titleRect.Bottom; //topBottom;// _titleRect.Bottom;
                    //if (i == ParentControl.Controls.Count - 2) MessageBox.Show("标题非空", "坐标" + y.ToString());
                }
                //------------------------------
                //if (pageIndex > 0)
                //{
                //    y += (TitleControl == null ? 0 : TitleControl.Height) + Border * 2;

                //    if ((y < _rectPrintBound.Top) || (y + 16 >= _rectPrintBound.Height)) continue;
                //}

                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center
                };
                if (control is PictureBox)
                {
                    PictureBox box3 = control as PictureBox;

                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Center;

                    g.DrawImage(box3.Image, x, y, width, height);
                }
                if (control is LabelControl)
                {
                    if (control == _titleControl)
                        continue;

                    LabelControl label = control as LabelControl;

                    if (label.Width == 0 || string.IsNullOrEmpty(label.Text))
                        continue;

                    if (label.Name == "_lblPostFix" && label.Text.Contains("分"))
                    {
                        if (ParentControl.Controls[i - 1] is CheckEdit)
                        {
                            CheckEdit c = ParentControl.Controls[i - 1] as CheckEdit;
                            if (c == null)
                                continue;

                            int cx = ((int)((c.Left + HorizontalScroll.Value) / ZoomValue)) + _rectPrintBound.Left;

                            g.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor),
                                new Rectangle(cx + (int)g.MeasureString(c.Text, c.Font).Width + 10, y - 1, width,
                                    height), format);
                            continue;
                        }
                        if (ParentControl.Controls[i - 1] is SpinEdit)
                        {
                            g.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor),
                                new Rectangle(x - 14, y - 1, width, height), format);
                            continue;
                        }
                    }
                    #region 2015.10.10 debug
                    if (label.Name == "_lblName")
                    {
                        width += iFixWidth + 20;
                        height += iFixHeight + 5;
                        //width = (int)g.MeasureString(label.Text, label.Font).Width;
                        //height = (int)g.MeasureString(label.Text, label.Font).Height;
                    }
                    #endregion
                    //if (label.Text.Contains("评定标准")||label.Text.Contains("备注"))
                    //    g.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor), new Rectangle(x, y, 600, height), format);
                    //else
                        g.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor), new Rectangle(x, y-3, width, height), format);
                    continue;
                }
                if (control is PanelControl)
                {
                    PanelControl box = control as PanelControl;
                    if (box.Height < 3)
                    {
                        //g.DrawLine(new Pen(new SolidBrush(Color.Black)), new Point(x, y + height - 7), new Point(width, y + height - 7));
                        g.DrawLine(new Pen(Color.Black), new Point(x, y + height - 7),
                            new Point(803, y + height - 7));
                    }
                    continue;
                }
                if (control is MemoEdit)
                {
                    MemoEdit box = control as MemoEdit;
                    g.DrawString(box.Text, box.Font, new SolidBrush(box.ForeColor),
                        new Rectangle(x, y, width, height),
                        format);
                    //g.DrawLine(new Pen(new SolidBrush(Color.Black)), new Point(x, y + height - 7), new Point(x + width, y + height - 7));
                    g.DrawRectangle(new Pen(Color.Black), new Rectangle(x, y, width, height - 7));
                    continue;
                }
                if (control is TextEdit)
                {
                    TextEdit box = control as TextEdit;

                    format.Alignment = StringAlignment.Near;

                    format.LineAlignment = StringAlignment.Center;
                    //2016.01.27 临时解决文本行打印折行问题width -> width + 20。最终应调整format属性。
                    g.DrawString(box.Text, box.Font, new SolidBrush(box.ForeColor),
                        new Rectangle(x, y - 1, width+20, height), format);
                    //g.DrawRectangle(new Pen(Color.Green, 1), new Rectangle(x, y - 1, width, height));debug
                    BaseNode node1 = control.Tag as BaseNode;
                    if (node1 != null && node1.IsInGrid)
                    {
                        continue;
                    }

                    if (box is SpinEdit)
                    {
                        g.DrawLine(new Pen(Color.Black), new Point(x, y + height - 7),
                            new Point(x + width - 14, y + height - 7));
                        continue;
                    }

                    g.DrawLine(new Pen(Color.Black), new Point(x, y + height - 7),
                        new Point(x + width, y + height - 7));
                    continue;
                }
                if (control is UserControls.UcComboBox)
                {
                    UserControls.UcComboBox box2 = control as UserControls.UcComboBox;
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Center;
                    g.DrawString(box2.Text, box2.Font, new SolidBrush(box2.ForeColor),
                        new Rectangle(x, y, width, height), format);
                    continue;
                }
                if (control is CheckEdit)
                {
                    CheckEdit box3 = control as CheckEdit;

                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Center;

                    g.DrawString("□" + box3.Text, box3.Font, new SolidBrush(Color.Black),
                        new Rectangle(x + 6, y + 5, (int)g.MeasureString("□" + box3.Text, box3.Font).Width + 14, height), format);//2016.03.30 box3.ForeColor -> Color.Black
                    if (box3.Checked)
                    {
                        g.DrawString("√", new Font("楷体_GB2312", 9f, FontStyle.Bold), new SolidBrush(box3.ForeColor),
                            new Rectangle(x + 8, y + 5, width, height), format);
                    }
                }
            }
        }

        public void RelocateChildNodes(int startChildIndex)
        {
            base.SuspendLayout();
            try
            {
                for (int i = startChildIndex; i < this.ChildNodes.Count; i++)
                {
                    this.ChildNodes[i].Layout(true);
                }
            }
            finally
            {
                base.ResumeLayout();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                base.LoadingShow();

                SaveNursing();

                _listNursing = _listNursing.OrderBy(p => p.CreateTimestamp).ToList();
                //ucGridView1.DataSource = _listNursing.OrderBy(p => p.CreateTimestamp);
                int preFocusHandle = ucGridView1.FocusedRowHandle;
                ucGridView1.DataSource = _listNursing;
                if (preFocusHandle > 0 && preFocusHandle != ucGridView1.FocusedRowHandle)
                    ucGridView1.FocusedRowHandle = preFocusHandle;
                ucGridView1.RefreshData();

                if (this._nursing != null)
                    ucGridView1.SelectRow("Id", _nursing.Id);

                //InitData();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                base.LoadingClose();
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                Print(this.Text, true);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Print(this.Text, false);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void xtraScrollableControl1_MouseEnter(object sender, EventArgs e)
        {
            xtraScrollableControl1.Focus();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                base.LoadingShow();

                _nursing = null;
                docNursingRecordDs = null;

                this.NewForm(_templateId);

                SetButtonEnabled();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                base.LoadingClose();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show("是否确定删除？", "提示", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                return;
            try
            {
                base.LoadingShow();

                if (_nursing != null)
                {
                    //ucGridView1.DeleteSelectRow();
                    if (docNursingRecordDs != null && docNursingRecordDs.Tables.Count > 0 && docNursingRecordDs.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in docNursingRecordDs.Tables[0].Rows)
                        {
                            row.Delete();
                        }
                        _docNursingDbi.SaveDocNursingRecordDs(docNursingRecordDs);
                    }
                    
                    EntityOper.GetInstance().Delete(_nursing);
                    XtraMessageBox.Show("删除成功！");

                    InitData();

                    ucGridView1.SelectFirstRow();

                    DocNursing model = ucGridView1.GetSelectRow() as DocNursing;
                    if (model == null)
                    {
                        this.NewForm(_templateId);
                        SetButtonEnabled();
                        return;
                    }

                    this._nursing = model;

                    docNursingRecordDs = _docNursingDbi.GetDocNursingRecordDs(_nursing.Id);

                    this.NewForm(_templateId);
                    SetButtonEnabled();
                }
                else
                    XtraMessageBox.Show("删除失败！没有要删除的记录。");
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                base.LoadingClose();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                base.LoadingShow();

                InitData();

                //this.NewForm(_templateId);

                SetButtonEnabled();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                base.LoadingClose();
            }

        }

        private void btnSaveGrid_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ParentControl.Controls.Count; i++)
            {
                Control ctl = ParentControl.Controls[i];
                if (!ctl.Visible)
                    continue;

                if (ctl is PictureBox)
                {
                    BaseNode node1 = ctl.Tag as BaseNode;
                    if (node1 == null) continue;
                    DocTemplateElement model1 = node1.NursingDocNode;
                    if (model1.ControlHeight == model1.ControlWidth && model1.ControlWidth == 0)
                    {
                        model1.ControlWidth = ctl.Width;
                        model1.ControlHeight = ctl.Height;
                        model1.NewLine = 1;
                    }
                    continue;
                }

                if (!(ctl is CheckEdit))
                {
                    if (ctl.Tag == null || ctl.Name != "_lblName") continue;
                }
                BaseNode node = ctl.Tag as BaseNode;
                if (node == null) continue;
                DocTemplateElement model = node.NursingDocNode;

                model.ControlOffset = ctl.Left;
                model.RowSpacing = ctl.Top;
                model.NewLine = 1;
            }

            EntityOper.GetInstance().SaveOrUpdate<DocTemplateElement>(ListTemplateElements);

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 设置未达标问卷标志
        /// </summary>
        private void getDocNotStandard()
        {
            string docNursingId = string.Empty;
            int index;

            for (int i = 0; i < _listNursing.Count; i++)
            {
                _listNursing[i].Standard = "";
            }
            ucGridView1.ColumnsEvenOldRowColor = "Standard";

            DataRow dr = null;
            DataSet ds = null;

            try
            {
                /*2016.01.11 暂时屏蔽，原因：效率过低，简化流程
                ds = _docNursingDbi.GetDocItemNursingNotStard(GVars.Patient.ID, GVars.Patient.VisitId, _templateId);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    docNursingId = dr["DOC_NURSING_ID"].ToString();
                    index = _listNursing.FindIndex(p => p.Id.Equals(docNursingId));
                    if (index >= 0)
                    {
                        DocNursing docNursing = _listNursing[index];
                        docNursing.Standard = "1";//节点总分不达标
                    }
                }
                */
                ds = _docNursingDbi.GetDocNursingNotStard(GVars.Patient.ID, GVars.Patient.VisitId, _templateId);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    docNursingId = dr["ID"].ToString();
                    index = _listNursing.FindIndex(p => p.Id.Equals(docNursingId));
                    if (index >= 0)
                    {
                        DocNursing docNursing = _listNursing[index];
                        docNursing.Standard = "2";//评估单总分不达标，优先显示(同时可能节点总分也不达标)
                    }
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
                return;
            }

        }

        private void gvDefault_RowStyle(object sender, RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.RowHandle >= 0)
            {
                e.Appearance.BackColor = Color.White;
                string standard = view.GetRowCellDisplayText(e.RowHandle, view.Columns["Standard"]);
                switch (standard)
                {
                    case "1":
                        e.Appearance.ForeColor = Color.Orange;
                        break;
                    case "2":
                        e.Appearance.ForeColor = Color.Red;
                        break;
                }
            }
        }

        public void Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            //2015.12.19
            try
            {
                base.LoadingShow();
                this.PatientId = GVars.Patient.ID;
                this.VisitId = GVars.Patient.VisitId;
                //2015.12.04 add 2016.01.19 del
                //if (GVars.Patient.STATE == HISPlus.PAT_INHOS_STATE.OUT)
                //{
                //    btnNew.Enabled = false;
                //    btnSave.Enabled = false;
                //}

                InitData();

                this._nursing = ucGridView1.GetSelectRow() as DocNursing;
                //当列表为空时，即病人没有评估单记录，新建空评估单（_nursing为空即可）
                if (this._nursing != null)
                    docNursingRecordDs = _docNursingDbi.GetDocNursingRecordDs(_nursing.Id);
                else
                    docNursingRecordDs = null;

                this.NewForm(_templateId);

            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                base.LoadingClose();
            }
            
        }

        public void Patient_ListRefresh(object sender, PatientEventArgs e)
        {
           // throw new NotImplementedException();
        }

        //保存评估单为图片
        public bool SaveAsPic(string nursingId, string fullpath)
        {
            try
            {
                base.LoadingShow();
                _nursing = EntityOper.GetInstance().Load<DocNursing>(nursingId);

                this.PatientId = _nursing.PatientId;
                this.VisitId = _nursing.VisitNo;
                this._templateId = _nursing.TemplateId;
                this._rectPrintBound = new Rectangle(0, 0, ParentControl.DisplayRectangle.Width, ParentControl.DisplayRectangle.Height);

                if (_nursing == null)
                    docNursingRecordDs = null;
                else
                    docNursingRecordDs = _docNursingDbi.GetDocNursingRecordDs(_nursing.Id);
                //---------
                this.NewForm(_templateId);
                //---------

                Image myimage = new Bitmap(ParentControl.Width, ParentControl.Height);
                Graphics graphics = Graphics.FromImage(myimage);
                graphics.Clear(Color.White);
                this.PrintPage(graphics, 0, 1f);
                myimage.Save(fullpath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            catch //(Exception ex)
            {
                //Error.ErrProc(ex);
                return false;
            }
            finally
            {
                base.LoadingClose();
            }

            return true;
        }
    }
}

