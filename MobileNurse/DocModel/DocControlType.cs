using System;
using System.Collections;

/* ------------------------------------
	
    Name: DocControlType.cs
    Author: HCC
    Description: 
    Date: 2014-11-12
  
------------------------------------ */

namespace HISPlus
{
	#region DocControlType

	/// <summary>
	/// DocControlType object for NHibernate mapped table 'DOC_CONTROL_TYPE'.
	/// </summary>
    public class DocControlType : CommonEntity.IEntity
	{
		#region 成员变量
		
        /// <summary>
	    /// 
	    /// </summary>		
        protected decimal _id;
        
		/// <summary>
	    /// 控件类型CODE
	    /// </summary>
        protected string _code;
		/// <summary>
	    /// 类型名称
	    /// </summary>
        protected string _name;
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

		#endregion

		#region 构造器

        /// <summary>
	    /// 
	    /// </summary>
		public DocControlType() { }

        /// <summary>
	    /// 
	    /// </summary>
		public DocControlType( string code, string name, string remark, DateTime createTimestamp, DateTime updateTimestamp )
		{
			this._code = code;
			this._name = name;
			this._remark = remark;
			this._createTimestamp = createTimestamp;
			this._updateTimestamp = updateTimestamp;
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
	    /// 控件类型CODE
	    /// </summary>	
        public virtual string Code
		{
			get { return _code; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Code", value, value.ToString());
				_code = value;
			}
		}

		/// <summary>
	    /// 类型名称
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


		#endregion
	}
	#endregion
}