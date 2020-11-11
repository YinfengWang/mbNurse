using System;
using System.Collections;

/* ------------------------------------
	
    Name: DocNursingRecord.cs
    Author: HCC
    Description: 
    Date: 2014-11-12
  
------------------------------------ */

namespace HISPlus
{
	#region DocNursingRecord

	/// <summary>
	/// DocNursingRecord object for NHibernate mapped table 'DOC_NURSING_RECORD'.
	/// </summary>
    public class DocNursingRecord : CommonEntity.IEntity
	{
		#region 成员变量
		
        /// <summary>
	    /// 
	    /// </summary>		
        protected string _id;
        
		/// <summary>
	    /// 元素ID
	    /// </summary>
        protected decimal _docElementId;
		/// <summary>
	    /// 整型浮点型值
	    /// </summary>
        protected decimal _numberValue;
		/// <summary>
	    /// 字符串类型值
	    /// </summary>
        protected string _stringValue;
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
        protected DocNursing _docNursing;

		#endregion

		#region 构造器

        /// <summary>
	    /// 
	    /// </summary>
		public DocNursingRecord() { }

        /// <summary>
	    /// 
	    /// </summary>
		public DocNursingRecord( decimal docElementId, decimal numberValue, string stringValue, DateTime createTimestamp, DateTime updateTimestamp, DocNursing docNursing )
		{
			this._docElementId = docElementId;
			this._numberValue = numberValue;
			this._stringValue = stringValue;
			this._createTimestamp = createTimestamp;
			this._updateTimestamp = updateTimestamp;
			this._docNursing = docNursing;
		}

		#endregion

		#region 公共属性

        /// <summary>
	    /// 
	    /// </summary>		    
        public virtual string Id
		{
			get {return _id;}
			set {_id = value;}
		}

		/// <summary>
	    /// 元素ID
	    /// </summary>	
        public virtual decimal DocElementId
		{
			get { return _docElementId; }
			set { _docElementId = value; }
		}

		/// <summary>
	    /// 整型浮点型值
	    /// </summary>	
        public virtual decimal NumberValue
		{
			get { return _numberValue; }
			set { _numberValue = value; }
		}

		/// <summary>
	    /// 字符串类型值
	    /// </summary>	
        public virtual string StringValue
		{
			get { return _stringValue; }
			set
			{
				if ( value != null && value.Length > 4000)
					throw new ArgumentOutOfRangeException("Invalid value for StringValue", value, value.ToString());
				_stringValue = value;
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
        public virtual DocNursing DocNursing
		{
			get { return _docNursing; }
			set { _docNursing = value; }
		}        


		#endregion
	}
	#endregion
}