using System;
using System.Collections;
/* ------------------------------------
	
    Name: VitalSignsRec.cs
    Author: HCC
    Description: 
    Date: 2015-06-05
  
------------------------------------ */
using System.ComponentModel;
using HISPlus.Annotations;

namespace HISPlus.Models
{
    #region VitalSignsRec

    /// <summary>
    /// VitalSignsRec object for NHibernate mapped table 'VITAL_SIGNS_REC'.
    /// </summary>
    public class VitalSignsRec : CommonEntity.IEntity, INotifyPropertyChanged
    {
        #region 成员变量

        /// <summary>
        /// 
        /// </summary>		
        protected VitalSignsRecId _id;

        /// <summary>
        /// 
        /// </summary>
        private string _vitalSigns;
        /// <summary>
        /// 
        /// </summary>
        private float _vitalSignsValues;
        /// <summary>
        /// ????
        /// </summary>
        private string _units;
        /// <summary>
        /// 
        /// </summary>
        private string _vitalSignsCvalues;
        /// <summary>
        /// 
        /// </summary>
        private string _wardCode;
        /// <summary>
        /// 
        /// </summary>
        private string _nurse;
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
        private string _memo;

        #endregion

        #region 构造器

        /// <summary>
        /// 
        /// </summary>
        public VitalSignsRec() { }

        /// <summary>
        /// 
        /// </summary>
        public VitalSignsRec(string vitalSigns, float vitalSignsValues, string units, string vitalSignsCvalues, string wardCode, string nurse, DateTime createTimestamp, DateTime updateTimestamp, string memo)
        {
            this._vitalSigns = vitalSigns;
            this._vitalSignsValues = vitalSignsValues;
            this._units = units;
            this._vitalSignsCvalues = vitalSignsCvalues;
            this._wardCode = wardCode;
            this._nurse = nurse;
            this._createTimestamp = createTimestamp;
            this._updateTimestamp = updateTimestamp;
            this._memo = memo;
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 
        /// </summary>		    
        public virtual VitalSignsRecId Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("Id"); }
        }


        /// <summary>
        /// 
        /// </summary>	
        public virtual string VitalSigns
        {
            get { return _vitalSigns; }
            set
            {
                if (value != null && value.Length > 16)
                    throw new ArgumentOutOfRangeException("Invalid value for VitalSigns", value, value);
                _vitalSigns = value; OnPropertyChanged("VitalSigns"); 
            }
        }

        /// <summary>
        /// 
        /// </summary>	
        public virtual float VitalSignsValues
        {
            get { return _vitalSignsValues; }
            set { _vitalSignsValues = value; OnPropertyChanged("VitalSignsValues"); }
        }

        /// <summary>
        /// ????
        /// </summary>	
        public virtual string Units
        {
            get { return _units; }
            set
            {
                if (value != null && value.Length > 8)
                    throw new ArgumentOutOfRangeException("Invalid value for Units", value, value);
                _units = value; OnPropertyChanged("Units");
            }
        }

        /// <summary>
        /// 
        /// </summary>	
        public virtual string VitalSignsCvalues
        {
            get { return _vitalSignsCvalues; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for VitalSignsCvalues", value, value);
                _vitalSignsCvalues = value; OnPropertyChanged("VitalSignsCvalues");
            }
        }

        /// <summary>
        /// 
        /// </summary>	
        public virtual string WardCode
        {
            get { return _wardCode; }
            set
            {
                if (value != null && value.Length > 8)
                    throw new ArgumentOutOfRangeException("Invalid value for WardCode", value, value);
                _wardCode = value; OnPropertyChanged("WardCode");
            }
        }

        /// <summary>
        /// 
        /// </summary>	
        public virtual string Nurse
        {
            get { return _nurse; }
            set
            {
                if (value != null && value.Length > 16)
                    throw new ArgumentOutOfRangeException("Invalid value for Nurse", value, value);
                _nurse = value; OnPropertyChanged("Nurse");
            }
        }

        /// <summary>
        /// 
        /// </summary>	
        public virtual DateTime CreateTimestamp
        {
            get { return _createTimestamp; }
            set { _createTimestamp = value; OnPropertyChanged("CreateTimestamp"); }
        }

        /// <summary>
        /// 
        /// </summary>	
        public virtual DateTime UpdateTimestamp
        {
            get { return _updateTimestamp; }
            set { _updateTimestamp = value; OnPropertyChanged("UpdateTimestamp"); }
        }

        /// <summary>
        /// 
        /// </summary>	
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                if (value != null && value.Length > 80)
                    throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value);
                _memo = value; OnPropertyChanged("Memo");
            }
        }


        #endregion

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }


    /// <summary>
    /// 
    /// </summary>		
    public class VitalSignsRecId
    {
        /// <summary>
        /// 
        /// </summary>        
        public virtual string PatientId { get; set; }
        /// <summary>
        /// 
        /// </summary>        
        public virtual long VisitId { get; set; }
        /// <summary>
        /// 
        /// </summary>        
        public virtual DateTime RecordingDate { get; set; }
        /// <summary>
        /// 
        /// </summary>        
        public virtual DateTime TimePoint { get; set; }
        /// <summary>
        /// ????
        /// </summary>        
        public virtual string ClassCode { get; set; }
        /// <summary>
        /// ????
        /// </summary>        
        public virtual string VitalCode { get; set; }

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