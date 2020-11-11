using System;

/* ------------------------------------
	
    Name: MenuManager.cs
    Author: HCC
    Description: 
    Date: 2014-12-03
  
------------------------------------ */

namespace HISPlus
{
    #region MenuManager

    /// <summary>
    /// MenuManager object for NHibernate mapped table 'MENU_MANAGER'.
    /// </summary>
    public class MenuManager
    {
        #region 成员变量

        /// <summary>
        /// 
        /// </summary>		
        protected string _id;

        /// <summary>
        /// 父级菜单
        /// </summary>
        protected string _parentId;
        /// <summary>
        /// 菜单名称
        /// </summary>
        protected string _name;

        /// <summary>
        /// 窗体名称
        /// </summary>
        protected string _formName;
        /// <summary>
        /// 打开模式
        /// </summary>
        protected string _openMode;
        /// <summary>
        /// 图标路径
        /// </summary>
        protected string _iconPath;

        /// <summary>
        /// 序号列
        /// </summary>
        protected decimal _sortId;
        /// <summary>
        /// 
        /// </summary>
        protected string _appCode;
        /// <summary>
        /// 
        /// </summary>
        protected string _remark;
        /// <summary>
        /// 
        /// </summary>
        protected byte _enabled;
        /// <summary>
        /// 
        /// </summary>
        protected string _version;

        #endregion

        #region 构造器

        /// <summary>
        /// 
        /// </summary>
        public MenuManager() { }

        /// <summary>
        /// 
        /// </summary>
        public MenuManager(string parentId, string name, string moduleCode, string assembly, string formName, string openMode, string iconPath, byte connectionPatient, decimal sortId, string rights, string appCode, string remark, byte enabled, string version)
        {
            this._parentId = parentId;
            this._name = name;
            this._formName = formName;
            this._openMode = openMode;
            this._iconPath = iconPath;
            this._sortId = sortId;
            this._appCode = appCode;
            this._remark = remark;
            this._enabled = enabled;
            this._version = version;
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 
        /// </summary>		    
        public virtual string Id
        {
            get { return _id; }
            set
            {               
                _id = value;
            }
        }

        /// <summary>
        /// 父级菜单
        /// </summary>	
        public virtual string ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        /// <summary>
        /// 菜单名称
        /// </summary>	
        public virtual string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// 配置文件路径
        /// </summary>	
        public virtual string IniPath
        {
            get { return _formName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for FormName", value, value.ToString());
                _formName = value;
            }
        }

        /// <summary>
        /// 打开模式
        /// </summary>	
        public virtual string OpenMode
        {
            get { return _openMode; }
            set
            {
                _openMode = value;
            }
        }

        /// <summary>
        /// 图标路径
        /// </summary>	
        public virtual string IconPath
        {
            get { return _iconPath; }
            set
            {
                _iconPath = value;
            }
        }

        /// <summary>
        /// 序号列
        /// </summary>	
        public virtual decimal SortId
        {
            get { return _sortId; }
            set { _sortId = value; }
        }

        /// <summary>
        /// 
        /// </summary>	
        public virtual string AppCode
        {
            get { return _appCode; }
            set
            {              
                _appCode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>	
        public virtual string Remark
        {
            get { return _remark; }
            set
            {               
                _remark = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>	
        public virtual byte Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        #endregion
    }
    #endregion
}