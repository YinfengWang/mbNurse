using System;
using System.Collections;

/* ------------------------------------
	
    Name: UserCardMenu.cs
    Author: HCC
    Description: 
    Date: 2014-12-29
  
------------------------------------ */

namespace HISPlus
{
    #region UserCardMenu

    /// <summary>
    /// UserCardMenu object for NHibernate mapped table 'USER_CARD_MENU'.
    /// </summary>
    public class UserCardMenu : CommonEntity.IEntity
    {
        #region 成员变量

        /// <summary>
        /// 
        /// </summary>		
        protected decimal _id;

        /// <summary>
        /// 用户编号
        /// </summary>
        protected string _userId;
        /// <summary>
        /// 科室/病区
        /// </summary>
        protected string _deptCode;
        /// <summary>
        /// 菜单ID
        /// </summary>
        protected string _menuId;
        /// <summary>
        /// 菜单父级ID
        /// </summary>
        protected string _parentMenuId;
        /// <summary>
        /// 排序
        /// </summary>
        protected decimal _sortId;

        #endregion

        #region 构造器

        /// <summary>
        /// 
        /// </summary>
        public UserCardMenu() { }

        /// <summary>
        /// 
        /// </summary>
        public UserCardMenu(string userId, string deptCode, string menuId, string parentMenuId, decimal sortId)
        {
            this._userId = userId;
            this._deptCode = deptCode;
            this._menuId = menuId;
            this._parentMenuId = parentMenuId;
            this._sortId = sortId;
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
        /// 用户编号
        /// </summary>	
        public virtual string UserId
        {
            get { return _userId; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for UserId", value, value.ToString());
                _userId = value;
            }
        }

        /// <summary>
        /// 科室/病区
        /// </summary>	
        public virtual string DeptCode
        {
            get { return _deptCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for DeptCode", value, value.ToString());
                _deptCode = value;
            }
        }

        /// <summary>
        /// 菜单ID
        /// </summary>	
        public virtual string MenuId
        {
            get { return _menuId; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for MenuId", value, value.ToString());
                _menuId = value;
            }
        }

        /// <summary>
        /// 菜单父级ID
        /// </summary>	
        public virtual string ParentMenuId
        {
            get { return _parentMenuId; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ParentMenuId", value, value.ToString());
                _parentMenuId = value;
            }
        }

        /// <summary>
        /// 排序
        /// </summary>	
        public virtual decimal SortId
        {
            get { return _sortId; }
            set { _sortId = value; }
        }

        /// <summary>
        /// 菜单名称,该属性不与数据库进行关联
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图标路径,该属性不与数据库进行关联
        /// </summary>
        public string IconPath { get; set; }

        #endregion
    }
    #endregion
}