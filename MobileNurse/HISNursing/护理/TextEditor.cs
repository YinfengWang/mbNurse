using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HISPlus
{
    public partial class TextEditor : Form
    {
        #region 声明变量
        private string  topDir      = string.Empty;                     // 模板所在目录
        
        public string   TimePoint   = string.Empty;                     // 时间点
        public string   Desc        = string.Empty;                     // 病情描述
        
        public string[] ArrDesc     = null;                             // 每一行的内容

        protected int   _lines      = 0;                                // 总行数
        #endregion
        
        public TextEditor()
        {
            InitializeComponent();
            
            this.trvDirectory.BeforeExpand += new TreeViewCancelEventHandler( trvDirectory_BeforeExpand );
            this.trvDirectory.AfterSelect += new TreeViewEventHandler( trvDirectory_AfterSelect );

            this.txtEdit.GotFocus += new EventHandler(imeCtrl_GotFocus);
        }


        #region 属性
        public int Lines
        {
            get { return _lines;}
        }
        #endregion


        #region 窗体事件处理
        private void TextEditor_Load(object sender, EventArgs e)
        {
            try
            {
                trvDirectory.ImageList  = imageList1;
                refreshTree();
                
                // 编辑控件属性设置
                txtEdit.AllowDrop = true;
                                
                txtEdit.Text    = Desc;
                dtPicker.Value  = DateTime.Parse(TimePoint);
                
                txtPath.ReadOnly        = true;
                btnSaveTemplate.Enabled = false;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[编辑模板]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditTemplate_Click(object sender, EventArgs e)
        {
            if (txtPath.Text.Length > 0 && txtPath.Text.EndsWith(".txt") == true)
            {
                txtPath.ReadOnly        = false;
                txtTemplate.ReadOnly    = false;
                btnSaveTemplate.Enabled = true;
                btnEditTemplate.Enabled = false;
            }
        }
        
        
        /// <summary>
        /// 按钮[保存模板]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                bool blnNewFile = false;
                
                // 判断文件是否正确
                string fileName = Path.Combine(topDir, txtPath.Text).Trim();
                
                if (fileName.EndsWith(".txt") == false)
                {
                    MessageBox.Show("文件名不正确!");
                    txtPath.Focus();
                    return;
                }
                
                this.Cursor = Cursors.WaitCursor;
                
                // 判断文件是否存在
                if (File.Exists(fileName) == false)
                {
                    FileStream fs = File.Create(fileName);
                    fs.Close();
                    
                    blnNewFile = true;
                }
                
                // 保存文件
                StreamWriter sw = new StreamWriter(fileName, false, Encoding.Default);
                sw.WriteLine(txtTemplate.Text);
                sw.Close();
                
                MessageBox.Show("保存模板文件成功");
                
                // 界面状态
                if (blnNewFile == true)
                {
                    refreshTree();
                }
                
                txtTemplate.ReadOnly    = true;
                txtPath.ReadOnly        = true;
                btnEditTemplate.Enabled = true;
                btnSaveTemplate.Enabled = false;
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
        /// 按钮[=>]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEdit.Text.Length == 0)
                {
                    txtEdit.Text = txtTemplate.Text;
                }
                else
                {
                    txtEdit.Text += ComConst.STR.CRLF + txtTemplate.Text;
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[确定]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click( object sender, EventArgs e )
        {
            try
            {
                TimePoint = this.dtPicker.Value.ToString(ComConst.FMT_DATE.LONG);
                
                getLines();
                
                this.Close();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
                
        /// <summary>
        /// 按钮[退出]
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
        #endregion
        
        
        #region 共通函数
        /// <summary>
        /// 获取编辑框的行数以及每一行的内容
        /// </summary>
        private void getLines()
        {
            // 如果没有内容
            if (txtEdit.Text.Trim().Length == 0)
            {
                _lines   = 1;
                ArrDesc = new string[_lines];
                
                return;
            }
            
            // 如果有内容
            _lines = Win32API.SendMessageA(txtEdit.Handle.ToInt32(), Win32API.EM_GETLINECOUNT, 0, 0);
            ArrDesc = new string[_lines];
            
            int pos1 = 0;            
            int pos0 = Win32API.SendMessageA(txtEdit.Handle.ToInt32(), Win32API.EM_LINEINDEX, 0, 0);
            int row  = 0;
            for(row = 1; row < _lines; row++)
            {
                pos1 = Win32API.SendMessageA(txtEdit.Handle.ToInt32(), Win32API.EM_LINEINDEX, row ,0);
                
                ArrDesc[row - 1] = txtEdit.Text.Substring(pos0, pos1 - pos0);
                
                pos0 = pos1;
            }
            
            ArrDesc[row - 1] = txtEdit.Text.Substring(pos0, txtEdit.Text.Length - pos0);
        }
        #endregion
        
        
        #region 树的处理
        /// <summary>
        /// 重新加载树
        /// </summary>
        private void refreshTree()
        {
            // 清空树
            try
            {
                trvDirectory.BeginUpdate();
                trvDirectory.Nodes.Clear();
                
                // 获取子目录                    
                topDir = Path.Combine(System.Environment.CurrentDirectory, @"Template\护理记录模板");
                String[] arrDir = Directory.GetDirectories(topDir);
                
                string  nodeText = string.Empty;
                int     pos      = 0;
                
                foreach(string str in arrDir)
                {
                    nodeText = str;
                    pos      = nodeText.IndexOf(topDir);
                    if (pos >= 0)
                    {
                        nodeText = str.Substring(pos + topDir.Length + 1);
                    }
                    
                    TreeNode trvNode = new TreeNode(nodeText, 0, 0);
                                        
                    trvDirectory.Nodes.Add(trvNode);
                    addDirectories(trvNode);
                }
            }
            finally
            {
                trvDirectory.EndUpdate();
            }
        }
        
        
        /// <summary>
        /// 添加目录的子节点
        /// </summary>
        /// <param name="tn"></param>
        private void addDirectories(TreeNode trvNode)
        {
            
            trvNode.Nodes.Clear();
            
            // 获取目录名称
            string strPath = Path.Combine(topDir, trvNode.FullPath);
            
            // 获取子目录
            DirectoryInfo dirInfo = new DirectoryInfo(strPath);
            DirectoryInfo[] arrDirInfo;
            
            try
            {
                arrDirInfo = dirInfo.GetDirectories();
            }
            catch
            {
                return;
            }
            
            // 显示子目录
            foreach(DirectoryInfo di in arrDirInfo)
            {
                TreeNode trvNodeSub = new TreeNode(di.Name, 1, 2);
                
                trvNodeSub.ImageIndex = 0;
                trvNode.Nodes.Add(trvNodeSub);
            }
            
            // 获取该目录下的文件
            FileInfo[] arrFiInfo;
            try
            {
                arrFiInfo = dirInfo.GetFiles();
            }
            catch
            {
                return;
            }
            
            foreach (FileInfo fi in arrFiInfo)
            {
                TreeNode trvNodeSub = new TreeNode(fi.Name, 1, 2);
                if (fi.Extension.Equals(".txt"))
                {
                    trvNode.Nodes.Add(trvNodeSub);
                    trvNodeSub.ImageIndex = 2;
                }
            }
        }
        
        
        /// <summary>
        /// 节点展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void trvDirectory_BeforeExpand( object sender, TreeViewCancelEventArgs e )
        {
            try
            {
                trvDirectory.BeginUpdate();
                
                if (trvDirectory.Tag == null || (int)(trvDirectory.Tag) == 0)
                {
                    e.Node.ImageIndex = 1;
                }
                
                foreach (TreeNode tn in e.Node.Nodes)
                {
                    addDirectories(tn);
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                trvDirectory.EndUpdate();
            }
        }
        
        
        /// <summary>
        /// 选中一个节点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void trvDirectory_AfterSelect( object sender, TreeViewEventArgs e )
        {
            try
            {
                string strFile  = Path.Combine(topDir, e.Node.FullPath);
                
                if (e.Node.Text.EndsWith(".txt") == false)
                {
                    e.Node.SelectedImageIndex = (e.Node.IsExpanded? 1: 0);
                }
                
                txtPath.Text    = e.Node.FullPath;
                txtTemplate.Text= string.Empty;
                
                if (File.Exists(strFile) == true)
                {
                    StreamReader st = new StreamReader(strFile, Encoding.Default);
                    txtTemplate.Text = st.ReadToEnd();
                    txtTemplate.Text = txtTemplate.Text.TrimEnd();
                    st.Close();
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion        
    }
}