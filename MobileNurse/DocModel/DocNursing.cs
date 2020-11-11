using System;
using System.Collections;

/* ------------------------------------
	
    Name: DocNursing.cs
    Author: HCC
    Description: 
    Date: 2014-11-12
  
------------------------------------ */

namespace HISPlus
{
	#region DocNursing

	/// <summary>
	/// DocNursing object for NHibernate mapped table 'DOC_NURSING'.
	/// </summary>
    public class DocNursing : CommonEntity.IEntity
	{
		#region 成员变量
		
        /// <summary>
	    /// 
	    /// </summary>		
        protected string _id;
        
		/// <summary>
	    /// 病人ID
	    /// </summary>
        protected string _patientId;
		/// <summary>
	    /// 住院号
	    /// </summary>
        protected string _visitNo;
		/// <summary>
	    /// 病区号
	    /// </summary>
        protected string _wardCode;
		/// <summary>
	    /// 文书模板ID
	    /// </summary>
        protected decimal _templateId;
		/// <summary>
	    /// 总分
	    /// </summary>
        protected double _totalScore;
		/// <summary>
	    /// 创建用户
	    /// </summary>
        protected string _createUser;
		/// <summary>
	    /// 更新用户
	    /// </summary>
        protected string _updateUser;
		/// <summary>
	    /// 创建时间
	    /// </summary>
        protected DateTime _createTimestamp;
		/// <summary>
	    /// 更新时间
	    /// </summary>
        protected DateTime _updateTimestamp;

        /// <summary>
        /// 是否达标
        /// </summary>
        private string _standard;

		#endregion

		#region 构造器

        /// <summary>
	    /// 
	    /// </summary>
		public DocNursing() { }

        /// <summary>
	    /// 
	    /// </summary>
		public DocNursing( string patientId, string visitNo, string wardCode, decimal templateId, double totalScore, string createUser, string updateUser, DateTime createTimestamp, DateTime updateTimestamp )
		{
			this._patientId = patientId;
			this._visitNo = visitNo;
			this._wardCode = wardCode;
			this._templateId = templateId;
			this._totalScore = totalScore;
			this._createUser = createUser;
			this._updateUser = updateUser;
			this._createTimestamp = createTimestamp;
			this._updateTimestamp = updateTimestamp;
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
	    /// 病人ID
	    /// </summary>	
        public virtual string PatientId
		{
			get { return _patientId; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for PatientId", value, value.ToString());
				_patientId = value;
			}
		}

		/// <summary>
	    /// 住院号
	    /// </summary>	
        public virtual string VisitNo
		{
			get { return _visitNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for VisitNo", value, value.ToString());
				_visitNo = value;
			}
		}

		/// <summary>
	    /// 病区号
	    /// </summary>	
        public virtual string WardCode
		{
			get { return _wardCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for WardCode", value, value.ToString());
				_wardCode = value;
			}
		}

		/// <summary>
	    /// 文书模板ID
	    /// </summary>	
        public virtual decimal TemplateId
		{
			get { return _templateId; }
			set { _templateId = value; }
		}

		/// <summary>
	    /// 总分
	    /// </summary>	
        public virtual double TotalScore
		{
			get { return _totalScore; }
			set { _totalScore = value; }
		}

		/// <summary>
	    /// 创建用户
	    /// </summary>	
        public virtual string CreateUser
		{
			get { return _createUser; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CreateUser", value, value.ToString());
				_createUser = value;
			}
		}

		/// <summary>
	    /// 更新用户
	    /// </summary>	
        public virtual string UpdateUser
		{
			get { return _updateUser; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for UpdateUser", value, value.ToString());
				_updateUser = value;
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
        /// 是否达标
        /// </summary>
        public virtual string Standard
        {
            get { return _standard; }
            set { _standard = value; }
        }
		#endregion
	}
	#endregion
}