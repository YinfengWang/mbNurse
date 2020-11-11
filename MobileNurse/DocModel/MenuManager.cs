using System;
using System.Collections;

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
    public class MenuManager : CommonEntity.IEntity
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
	    /// 模块编码
	    /// </summary>
        protected string _moduleCode;
		/// <summary>
	    /// 程序集dll
	    /// </summary>
        protected string _assembly;
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
	    /// 关联病人列表(0不关联,1关联且为1级菜单,2关联且为2级菜单)
	    /// </summary>
        protected byte _connectionPatient;
		/// <summary>
	    /// 序号列
	    /// </summary>
        protected decimal _sortId;
		/// <summary>
	    /// 
	    /// </summary>
        protected string _rights;
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
			this._moduleCode = moduleCode;
			this._assembly = assembly;
			this._formName = formName;
			this._openMode = openMode;
			this._iconPath = iconPath;
			this._connectionPatient = connectionPatient;
			this._sortId = sortId;
			this._rights = rights;
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
			get {return _id;}
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Id", value, value.ToString());
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
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

		/// <summary>
	    /// 模块编码
	    /// </summary>	
        public virtual string ModuleCode
		{
			get { return _moduleCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ModuleCode", value, value.ToString());
				_moduleCode = value;
			}
		}

		/// <summary>
	    /// 程序集dll
	    /// </summary>	
        public virtual string Assembly
		{
			get { return _assembly; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Assembly", value, value.ToString());
				_assembly = value;
			}
		}

		/// <summary>
	    /// 窗体名称
	    /// </summary>	
        public virtual string FormName
		{
			get { return _formName; }
			set
			{
				if ( value != null && value.Length > 100)
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
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for OpenMode", value, value.ToString());
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
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for IconPath", value, value.ToString());
				_iconPath = value;
			}
		}

		/// <summary>
	    /// 关联病人列表(0不关联,1关联且为1级菜单,2关联且为2级菜单)
	    /// </summary>	
        public virtual byte ConnectionPatient
		{
			get { return _connectionPatient; }
			set { _connectionPatient = value; }
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
        public virtual string Rights
		{
			get { return _rights; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Rights", value, value.ToString());
				_rights = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string AppCode
		{
			get { return _appCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for AppCode", value, value.ToString());
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
				if ( value != null && value.Length > 2000)
					throw new ArgumentOutOfRangeException("Invalid value for Remark", value, value.ToString());
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

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string Version
		{
			get { return _version; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Version", value, value.ToString());
				_version = value;
			}
		}


		#endregion
	}
	#endregion
}