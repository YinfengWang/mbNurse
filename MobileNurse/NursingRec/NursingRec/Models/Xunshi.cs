using System;
/* ------------------------------------
	
    Name: Xunshi.cs
    Author: HCC
    Description: 
    Date: 2015-04-22
  
------------------------------------ */

namespace HISPlus.Models
{
	#region Xunshi

	/// <summary>
	/// Xunshi object for NHibernate mapped table 'XUNSHI'.
	/// </summary>
	public class Xunshi :CommonEntity.IEntity
	{
		#region 成员变量
		
         /// <summary>
	    /// 
	    /// </summary>		
        protected XunshiId _id;        
        
		/// <summary>
	    /// ?????
	    /// </summary>
        private string _nurse;
		/// <summary>
	    /// 
	    /// </summary>
        private string _content;
		/// <summary>
	    /// 
	    /// </summary>
        private DateTime _createTimestamp;
		/// <summary>
	    /// 
	    /// </summary>
        private DateTime _updateTimestamp;
		/// <summary>
	    /// 
	    /// </summary>
        private long _xunshiId;

        private DateTime _execute_date;


		#endregion

		#region 构造器

        /// <summary>
	    /// 
	    /// </summary>
		public Xunshi() { }

        /// <summary>
	    /// 
	    /// </summary>
		public Xunshi( string nurse, string content, DateTime createTimestamp, DateTime updateTimestamp, long xunshiId,DateTime executeDate )
		{
			this._nurse = nurse;
			this._content = content;
			this._createTimestamp = createTimestamp;
			this._updateTimestamp = updateTimestamp;
			this._xunshiId = xunshiId;
            this._execute_date = executeDate;
		}

		#endregion

		#region 公共属性

         /// <summary>
	    /// 
	    /// </summary>		    
        public virtual XunshiId Id
		{
			get {return _id;}			
			set {_id = value;}			
		}
        

		/// <summary>
	    /// ?????
	    /// </summary>	
        public virtual string Nurse
		{
			get { return _nurse; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for Nurse", value, value);
				_nurse = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string Content
		{
			get { return _content; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Content", value, value);
				_content = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual DateTime CreateTimestamp
		{
			get { return _createTimestamp; }
			set { _createTimestamp = value; }
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual DateTime UpdateTimestamp
		{
			get { return _updateTimestamp; }
			set { _updateTimestamp = value; }
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual long XunshiId
		{
			get { return _xunshiId; }
			set { _xunshiId = value; }
		}

        public virtual DateTime Execute_date
        {
            get { return _execute_date; }
            set { _execute_date = value; }
        }

		#endregion
     }   
   
    /// <summary>
    /// 
    /// </summary>		
    public class XunshiId
    {
		/// <summary>
	    /// ??????
	    /// </summary>        
        public virtual string WardCode { get; set; }
		/// <summary>
	    /// ??
	    /// </summary>        
        public virtual string PatientId { get; set; }
		/// <summary>
	    /// ??
	    /// </summary>        
        public virtual long VisitId { get; set; }
		/// <summary>
	    /// ????
	    /// </summary>        
        public virtual DateTime ExecuteDate { get; set; }
                
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
        
	#endregion
}