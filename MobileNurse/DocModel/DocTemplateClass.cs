using System;
using System.Collections;

/* ------------------------------------
	
    Name: DocTemplateClass.cs
    Author: HCC
    Description: 
    Date: 2015-01-22
  
------------------------------------ */

namespace HISPlus
{
	#region DocTemplateClass

	/// <summary>
	/// DocTemplateClass object for NHibernate mapped table 'DOC_TEMPLATE_CLASS'.
	/// </summary>
    public class DocTemplateClass : CommonEntity.IEntity
	{
		#region 成员变量
		
        /// <summary>
	    /// 
	    /// </summary>		
        protected decimal _id;
        
		/// <summary>
	    /// 模板类别名称
	    /// </summary>
        protected string _name;
		/// <summary>
	    /// 父级类别ID，为类别分级，可多级。
	    /// </summary>
        protected decimal _parentId;
		/// <summary>
	    /// 序号列，为类别排序，排序规则为同一父级项数据由1开始递增排序。
	    /// </summary>
        protected decimal _sortId;
		/// <summary>
	    /// 创建时间
	    /// </summary>
        protected DateTime _createTimestamp;
		/// <summary>
	    /// 更新时间
	    /// </summary>
        protected DateTime _updateTimestamp;
		/// <summary>
	    /// 
	    /// </summary>
        protected byte _enabled;

		#endregion

		#region 构造器

        /// <summary>
	    /// 
	    /// </summary>
		public DocTemplateClass() { }

        /// <summary>
	    /// 
	    /// </summary>
		public DocTemplateClass( string name, decimal parentId, decimal sortId, DateTime createTimestamp, DateTime updateTimestamp, byte enabled )
		{
			this._name = name;
			this._parentId = parentId;
			this._sortId = sortId;
			this._createTimestamp = createTimestamp;
			this._updateTimestamp = updateTimestamp;
			this._enabled = enabled;
		}

		#endregion

		#region 公共属性

        /// <summary>
	    /// 
	    /// </summary>		    
        public virtual decimal Id
		{
			get {return _id;}
			set {_id = value;}
		}

		/// <summary>
	    /// 模板类别名称
	    /// </summary>	
        public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

		/// <summary>
	    /// 父级类别ID，为类别分级，可多级。
	    /// </summary>	
        public virtual decimal ParentId
		{
			get { return _parentId; }
			set { _parentId = value; }
		}

		/// <summary>
	    /// 序号列，为类别排序，排序规则为同一父级项数据由1开始递增排序。
	    /// </summary>	
        public virtual decimal SortId
		{
			get { return _sortId; }
			set { _sortId = value; }
		}

		/// <summary>
	    /// 创建时间
	    /// </summary>	
        public virtual DateTime CreateTimestamp
		{
			get { return _createTimestamp; }
			set { _createTimestamp = value; }
		}

		/// <summary>
	    /// 更新时间
	    /// </summary>	
        public virtual DateTime UpdateTimestamp
		{
			get { return _updateTimestamp; }
			set { _updateTimestamp = value; }
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