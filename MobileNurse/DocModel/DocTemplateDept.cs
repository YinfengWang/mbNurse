using System;
using System.Collections;

/* ------------------------------------
	
    Name: DocTemplateDept.cs
    Author: HCC
    Description: 
    Date: 2014-12-05
  
------------------------------------ */

namespace HISPlus
{
    #region DocTemplateDept
  
    /// <summary>
    /// DocTemplateDept object for NHibernate mapped table 'DOC_TEMPLATE_DEPT'.
    /// </summary>
    public class DocTemplateDept : CommonEntity.IEntity
    {
        #region 成员变量

        /// <summary>
        /// 
        /// </summary>		
        protected decimal _id;

        /// <summary>
        /// 病区/科室CODE
        /// </summary>
        protected string _deptCode;
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
        protected DocTemplate _docTemplate;

        #endregion

        #region 构造器

        /// <summary>
        /// 
        /// </summary>
        public DocTemplateDept() { }

        /// <summary>
        /// 
        /// </summary>
        public DocTemplateDept(string deptCode, DateTime createTimestamp, DateTime updateTimestamp, DocTemplate docTemplate)
        {
            this._deptCode = deptCode;
            this._createTimestamp = createTimestamp;
            this._updateTimestamp = updateTimestamp;
            this._docTemplate = docTemplate;
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 
        /// </summary>		    
        public virtual decimal Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 病区/科室CODE
        /// </summary>	
        public virtual string DeptCode
        {
            get { return _deptCode; }
            set
            {
                if (value != null && value.Length > 8)
                    throw new ArgumentOutOfRangeException("Invalid value for DeptCode", value, value.ToString());
                _deptCode = value;
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
        public virtual DocTemplate DocTemplate
        {
            get { return _docTemplate; }
            set { _docTemplate = value; }
        }


        #endregion
    }
    #endregion
}