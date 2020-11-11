using System;
using System.Collections;
/* ------------------------------------
	
    Name: DocControlTemplate.cs
    Author: HCC
    Description: 
    Date: 2014-11-12
  
------------------------------------ */
using CommonEntity;

namespace HISPlus
{
	#region DocControlTemplate

	/// <summary>
	/// DocControlTemplate object for NHibernate mapped table 'DOC_CONTROL_TEMPLATE'.
	/// </summary>
	public class DocControlTemplate:IEntity
	{
		#region 成员变量
		
        /// <summary>
	    /// 
	    /// </summary>		
        protected decimal _id;
        
		/// <summary>
	    /// 控件模板名称
	    /// </summary>
        protected string _name;
		/// <summary>
	    /// 控件字体
	    /// </summary>
        protected string _controlFont;
		/// <summary>
	    /// 控件宽度
	    /// </summary>
        protected decimal _controlWidth;
		/// <summary>
	    /// 控件高度
	    /// </summary>
        protected decimal _controlHeight;
		/// <summary>
	    /// 控件偏移量
	    /// </summary>
        protected decimal _controlOffset;
		/// <summary>
	    /// 是否可用
	    /// </summary>
        protected byte _isEnabled;
		/// <summary>
	    /// 备注
	    /// </summary>
        protected string _remark;
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
        protected DocControlType _docControlType;

		#endregion

		#region 构造器

        /// <summary>
	    /// 
	    /// </summary>
		public DocControlTemplate() { }

        /// <summary>
	    /// 
	    /// </summary>
		public DocControlTemplate( string name, string controlFont, decimal controlWidth, decimal controlHeight, decimal controlOffset, byte isEnabled, string remark, DateTime createTimestamp, DateTime updateTimestamp, DocControlType docControlType )
		{
			this._name = name;
			this._controlFont = controlFont;
			this._controlWidth = controlWidth;
			this._controlHeight = controlHeight;
			this._controlOffset = controlOffset;
			this._isEnabled = isEnabled;
			this._remark = remark;
			this._createTimestamp = createTimestamp;
			this._updateTimestamp = updateTimestamp;
			this._docControlType = docControlType;
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
	    /// 控件模板名称
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
	    /// 控件字体
	    /// </summary>	
        public virtual string ControlFont
		{
			get { return _controlFont; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for ControlFont", value, value.ToString());
				_controlFont = value;
			}
		}

		/// <summary>
	    /// 控件宽度
	    /// </summary>	
        public virtual decimal ControlWidth
		{
			get { return _controlWidth; }
			set { _controlWidth = value; }
		}

		/// <summary>
	    /// 控件高度
	    /// </summary>	
        public virtual decimal ControlHeight
		{
			get { return _controlHeight; }
			set { _controlHeight = value; }
		}

		/// <summary>
	    /// 控件偏移量
	    /// </summary>	
        public virtual decimal ControlOffset
		{
			get { return _controlOffset; }
			set { _controlOffset = value; }
		}

		/// <summary>
	    /// 是否可用
	    /// </summary>	
        public virtual byte IsEnabled
		{
			get { return _isEnabled; }
			set { _isEnabled = value; }
		}

		/// <summary>
	    /// 备注
	    /// </summary>	
        public virtual string Remark
		{
			get { return _remark; }
			set
			{
				if ( value != null && value.Length > 800)
					throw new ArgumentOutOfRangeException("Invalid value for Remark", value, value.ToString());
				_remark = value;
			}
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
        public virtual DocControlType DocControlType
		{
			get { return _docControlType; }
			set { _docControlType = value; }
		}        


		#endregion
	}
	#endregion
}