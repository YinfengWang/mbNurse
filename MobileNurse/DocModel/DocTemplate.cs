using System;
using System.Collections;

/* ------------------------------------
	
    Name: DocTemplate.cs
    Author: HCC
    Description: 
    Date: 2014-11-12
  
------------------------------------ */

namespace HISPlus
{
	#region DocTemplate

	/// <summary>
	/// DocTemplate object for NHibernate mapped table 'DOC_TEMPLATE'.
	/// </summary>
    public class DocTemplate : CommonEntity.IEntity
	{
		#region 成员变量
		
        /// <summary>
	    /// 
	    /// </summary>		
        protected decimal _id;
        
		/// <summary>
	    /// 模板类型ID
	    /// </summary>
        protected decimal _templateTypeId;
		/// <summary>
	    /// 模板名称
	    /// </summary>
        protected string _templateName;
		/// <summary>
	    /// 显示名
	    /// </summary>
        protected string _displayName;
		/// <summary>
	    /// 是否为全院模板
	    /// </summary>
        protected byte _isGlobal;
		/// <summary>
	    /// 创建时间
	    /// </summary>
        protected DateTime _createTimestamp;
		/// <summary>
	    /// 创建用户
	    /// </summary>
        protected string _createUser;
		/// <summary>
	    /// 更新时间
	    /// </summary>
        protected DateTime _updateTimestamp;
		/// <summary>
	    /// 更新用户
	    /// </summary>
        protected string _updateUser;
		/// <summary>
	    /// 是否可用
	    /// </summary>
        protected byte _isEnabled;
		/// <summary>
	    /// 书写频次
	    /// </summary>
        protected string _freq;
		/// <summary>
	    /// 备注
	    /// </summary>
        protected string _remark;
		/// <summary>
	    /// 报表类型ID
	    /// </summary>
        protected decimal _reportTypeId;
		/// <summary>
	    /// 出入量统计
	    /// </summary>
        protected byte _hasInoutStat;
		/// <summary>
	    /// 腕带扫描
	    /// </summary>
        protected string _wristScan;
        ///<summary>
        ///序号
        ///</summary>
        private decimal _serialNo;

        public virtual decimal SerialNo
        {
            get { return _serialNo; }
            set { _serialNo = value; }
        }

        /// <summary>
        /// 达标分数下限
        /// </summary>
        private decimal _minScore;

        public virtual decimal MinScore
        {
            get { return _minScore; }
            set { _minScore = value; }
        }

		/// <summary>
	    /// 达标分数上限
	    /// </summary>
        private decimal _maxScore;

        public virtual decimal MaxScore
        {
            get { return _maxScore; }
            set { _maxScore = value; }
        }

        protected DocTemplateClass _docTemplateClass;
		/// <summary>
	    /// 
	    /// </summary>
        protected IList _docTemplateElements;
		/// <summary>
	    /// 
	    /// </summary>
        protected IList _docTemplateDepts;

		#endregion

		#region 构造器

        /// <summary>
	    /// 
	    /// </summary>
		public DocTemplate() { }

        /// <summary>
	    /// 
	    /// </summary>
		public DocTemplate( decimal templateTypeId, string templateName, string displayName, byte isGlobal, DateTime createTimestamp, string createUser, DateTime updateTimestamp, string updateUser, byte isEnabled, string freq, string remark, decimal reportTypeId, byte hasInoutStat, string wristScan, decimal serialNo,DocTemplateClass docTemplateClass )
		{
			this._templateTypeId = templateTypeId;
			this._templateName = templateName;
			this._displayName = displayName;
			this._isGlobal = isGlobal;
			this._createTimestamp = createTimestamp;
			this._createUser = createUser;
			this._updateTimestamp = updateTimestamp;
			this._updateUser = updateUser;
			this._isEnabled = isEnabled;
			this._freq = freq;
			this._remark = remark;
			this._reportTypeId = reportTypeId;
			this._hasInoutStat = hasInoutStat;
			this._wristScan = wristScan;
			this._docTemplateClass = docTemplateClass;
            this._serialNo = serialNo;
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
	    /// 模板类型ID
	    /// </summary>	
        public virtual decimal TemplateTypeId
		{
			get { return _templateTypeId; }
			set { _templateTypeId = value; }
		}

		/// <summary>
	    /// 模板名称
	    /// </summary>	
        public virtual string TemplateName
		{
			get { return _templateName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for TemplateName", value, value.ToString());
				_templateName = value;
			}
		}

		/// <summary>
	    /// 显示名
	    /// </summary>	
        public virtual string DisplayName
		{
			get { return _displayName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for DisplayName", value, value.ToString());
				_displayName = value;
			}
		}

		/// <summary>
	    /// 是否为全院模板
	    /// </summary>	
        public virtual byte IsGlobal
		{
			get { return _isGlobal; }
			set { _isGlobal = value; }
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
	    /// 更新时间
	    /// </summary>	
        public virtual DateTime UpdateTimestamp
		{
			get { return _updateTimestamp; }
			set { _updateTimestamp = value; }
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
	    /// 是否可用
	    /// </summary>	
        public virtual byte IsEnabled
		{
			get { return _isEnabled; }
			set { _isEnabled = value; }
		}

		/// <summary>
	    /// 书写频次
	    /// </summary>	
        public virtual string Freq
		{
			get { return _freq; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Freq", value, value.ToString());
				_freq = value;
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
	    /// 报表类型ID
	    /// </summary>	
        public virtual decimal ReportTypeId
		{
			get { return _reportTypeId; }
			set { _reportTypeId = value; }
		}

		/// <summary>
	    /// 出入量统计
	    /// </summary>	
        public virtual byte HasInoutStat
		{
			get { return _hasInoutStat; }
			set { _hasInoutStat = value; }
		}

		/// <summary>
	    /// 腕带扫描
	    /// </summary>	
        public virtual string WristScan
		{
			get { return _wristScan; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for WristScan", value, value.ToString());
				_wristScan = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual DocTemplateClass DocTemplateClass
		{
			get { return _docTemplateClass; }
			set { _docTemplateClass = value; }
		}        

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual IList DocTemplateElements
		{
			get
			{
				if (_docTemplateElements==null)
				{
					_docTemplateElements = new ArrayList();
				}
				return _docTemplateElements;
			}
			set { _docTemplateElements = value; }
		}
		/// <summary>
	    /// 
	    /// </summary>	
        public virtual IList DocTemplateDepts
		{
			get
			{
				if (_docTemplateDepts==null)
				{
					_docTemplateDepts = new ArrayList();
				}
				return _docTemplateDepts;
			}
			set { _docTemplateDepts = value; }
		}

		#endregion
	}
	#endregion
}