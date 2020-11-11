using System;
using System.Collections;

/* ------------------------------------
	
    Name: PatientInfo.cs
    Author: HCC
    Description: 
    Date: 2015-04-21
  
------------------------------------ */

namespace CommonEntity
{
	#region PatientInfo

	/// <summary>
	/// PatientInfo object for NHibernate mapped table 'PATIENT_INFO'.
	/// </summary>
	public class PatientInfo : IEntity
	{
		#region 成员变量
		
         /// <summary>
	    /// 
	    /// </summary>		
        protected PatientInfoId _id;        
        
		/// <summary>
	    /// 
	    /// </summary>
        private string _wardCode;
		/// <summary>
	    /// 
	    /// </summary>
        private string _inpNo;
		/// <summary>
	    /// 
	    /// </summary>
        private DateTime _admissionDateTime;
		/// <summary>
	    /// 
	    /// </summary>
        private string _name;
		/// <summary>
	    /// 
	    /// </summary>
        private string _sex;
		/// <summary>
	    /// 
	    /// </summary>
        private DateTime _dateOfBirth;
		/// <summary>
	    /// 
	    /// </summary>
        private string _diagnosis;
		/// <summary>
	    /// 
	    /// </summary>
        private string _alergyDrugs;
		/// <summary>
	    /// 
	    /// </summary>
        private string _doctorInCharge;
		/// <summary>
	    /// 
	    /// </summary>
        private short _bedNo;
		/// <summary>
	    /// 
	    /// </summary>
        private string _bedLabel;
		/// <summary>
	    /// 
	    /// </summary>
        private string _patientStatusName;
		/// <summary>
	    /// 
	    /// </summary>
        private string _nursingClass;
		/// <summary>
	    /// 
	    /// </summary>
        private string _nursingClassName;
		/// <summary>
	    /// 
	    /// </summary>
        private string _nursingClassColor;
		/// <summary>
	    /// 
	    /// </summary>
        private string _deptName;
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
        private string _deptCode;
		/// <summary>
	    /// 
	    /// </summary>
        private string _wardName;
		/// <summary>
	    /// 
	    /// </summary>
        private string _roomNo;

		#endregion

		#region 构造器

        /// <summary>
	    /// 
	    /// </summary>
		public PatientInfo() { }

        /// <summary>
	    /// 
	    /// </summary>
		public PatientInfo( string wardCode, string inpNo, DateTime admissionDateTime, string name, string sex, DateTime dateOfBirth, string diagnosis, string alergyDrugs, string doctorInCharge, short bedNo, string bedLabel, string patientStatusName, string nursingClass, string nursingClassName, string nursingClassColor, string deptName, DateTime createTimestamp, DateTime updateTimestamp, string deptCode, string wardName, string roomNo )
		{
			this._wardCode = wardCode;
			this._inpNo = inpNo;
			this._admissionDateTime = admissionDateTime;
			this._name = name;
			this._sex = sex;
			this._dateOfBirth = dateOfBirth;
			this._diagnosis = diagnosis;
			this._alergyDrugs = alergyDrugs;
			this._doctorInCharge = doctorInCharge;
			this._bedNo = bedNo;
			this._bedLabel = bedLabel;
			this._patientStatusName = patientStatusName;
			this._nursingClass = nursingClass;
			this._nursingClassName = nursingClassName;
			this._nursingClassColor = nursingClassColor;
			this._deptName = deptName;
			this._createTimestamp = createTimestamp;
			this._updateTimestamp = updateTimestamp;
			this._deptCode = deptCode;
			this._wardName = wardName;
			this._roomNo = roomNo;
		}

		#endregion

		#region 公共属性

         /// <summary>
	    /// 
	    /// </summary>		    
        public virtual PatientInfoId Id
		{
			get {return _id;}			
			set {_id = value;}			
		}
        

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string WardCode
		{
			get { return _wardCode; }
			set
			{
				if ( value != null && value.Length > 8)
					throw new ArgumentOutOfRangeException("Invalid value for WardCode", value, value);
				_wardCode = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string InpNo
		{
			get { return _inpNo; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for InpNo", value, value);
				_inpNo = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual DateTime AdmissionDateTime
		{
			get { return _admissionDateTime; }
			set { _admissionDateTime = value; }
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value);
				_name = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string Sex
		{
			get { return _sex; }
			set
			{
				if ( value != null && value.Length > 4)
					throw new ArgumentOutOfRangeException("Invalid value for Sex", value, value);
				_sex = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual DateTime DateOfBirth
		{
			get { return _dateOfBirth; }
			set { _dateOfBirth = value; }
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string Diagnosis
		{
			get { return _diagnosis; }
			set
			{
				if ( value != null && value.Length > 80)
					throw new ArgumentOutOfRangeException("Invalid value for Diagnosis", value, value);
				_diagnosis = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string AlergyDrugs
		{
			get { return _alergyDrugs; }
			set
			{
				if ( value != null && value.Length > 80)
					throw new ArgumentOutOfRangeException("Invalid value for AlergyDrugs", value, value);
				_alergyDrugs = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string DoctorInCharge
		{
			get { return _doctorInCharge; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for DoctorInCharge", value, value);
				_doctorInCharge = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual short BedNo
		{
			get { return _bedNo; }
			set { _bedNo = value; }
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string BedLabel
		{
			get { return _bedLabel; }
			set
			{
				if ( value != null && value.Length > 8)
					throw new ArgumentOutOfRangeException("Invalid value for BedLabel", value, value);
				_bedLabel = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string PatientStatusName
		{
			get { return _patientStatusName; }
			set
			{
				if ( value != null && value.Length > 4)
					throw new ArgumentOutOfRangeException("Invalid value for PatientStatusName", value, value);
				_patientStatusName = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string NursingClass
		{
			get { return _nursingClass; }
			set
			{
				if ( value != null && value.Length > 1)
					throw new ArgumentOutOfRangeException("Invalid value for NursingClass", value, value);
				_nursingClass = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string NursingClassName
		{
			get { return _nursingClassName; }
			set
			{
				if ( value != null && value.Length > 8)
					throw new ArgumentOutOfRangeException("Invalid value for NursingClassName", value, value);
				_nursingClassName = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string NursingClassColor
		{
			get { return _nursingClassColor; }
			set
			{
				if ( value != null && value.Length > 15)
					throw new ArgumentOutOfRangeException("Invalid value for NursingClassColor", value, value);
				_nursingClassColor = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string DeptName
		{
			get { return _deptName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for DeptName", value, value);
				_deptName = value;
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
        public virtual string DeptCode
		{
			get { return _deptCode; }
			set
			{
				if ( value != null && value.Length > 8)
					throw new ArgumentOutOfRangeException("Invalid value for DeptCode", value, value);
				_deptCode = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string WardName
		{
			get { return _wardName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for WardName", value, value);
				_wardName = value;
			}
		}

		/// <summary>
	    /// 
	    /// </summary>	
        public virtual string RoomNo
		{
			get { return _roomNo; }
			set
			{
				if ( value != null && value.Length > 8)
					throw new ArgumentOutOfRangeException("Invalid value for RoomNo", value, value);
				_roomNo = value;
			}
		}


		#endregion
     }   
        /// <summary>
	    /// 
	    /// </summary>		
        public class PatientInfoId
        {
    		/// <summary>
    	    /// 
    	    /// </summary>        
            public virtual string PatientId { get; set; }
    		/// <summary>
    	    /// 
    	    /// </summary>        
            public virtual byte VisitId { get; set; }
                    
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