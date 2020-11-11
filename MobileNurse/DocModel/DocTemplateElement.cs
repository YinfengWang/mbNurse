using System;
using System.Collections;

/* ------------------------------------
	
    Name: DocTemplateElement.cs
    Author: HCC
    Description: 
    Date: 2014-12-05
  
------------------------------------ */

namespace HISPlus
{
    #region DocTemplateElement

    /// <summary>
    /// DocTemplateElement object for NHibernate mapped table 'DOC_TEMPLATE_ELEMENT'.
    /// </summary>
    public sealed class DocTemplateElement : CommonEntity.IEntity
    {
        #region 成员变量

        /// <summary>
        /// 
        /// </summary>		
        private decimal _id;

        /// <summary>
        /// 父级元素ID
        /// </summary>
        private decimal _parentId;
        /// <summary>
        /// 序号列
        /// </summary>
        private decimal _sortId;
        /// <summary>
        /// 元素名称
        /// </summary>
        private string _elementName;
        /// <summary>
        /// 显示名
        /// </summary>
        private string _displayName;
        /// <summary>
        /// 名称字体
        /// </summary>
        private string _nameFont;
        /// <summary>
        /// 状态ID
        /// </summary>
        private decimal _controlStatusId;
        /// <summary>
        /// 控件字体
        /// </summary>
        private string _controlFont;
        /// <summary>
        /// 控件宽度
        /// </summary>
        private decimal _controlWidth;
        /// <summary>
        /// 控件高度
        /// </summary>
        private decimal _controlHeight;
        /// <summary>
        /// 控件偏移量
        /// </summary>
        private decimal _controlOffset;
        /// <summary>
        /// 控件前缀
        /// </summary>
        private string _controlPrefix;
        /// <summary>
        /// 控件后缀
        /// </summary>
        private string _controlSuffix;
        /// <summary>
        /// 是否换行，1为是，0为否，默认为0
        /// </summary>
        private byte _newLine;
        /// <summary>
        /// 行间距
        /// </summary>
        private decimal _rowSpacing;
        /// <summary>
        /// 子项缩进
        /// </summary>
        private decimal _childrenIndent;
        /// <summary>
        /// 数据类型
        /// </summary>
        private byte _dataType;
        /// <summary>
        /// 分值
        /// </summary>
        private float _score;
        /// <summary>
        /// 关联类型
        /// </summary>
        private decimal _relationType;
        /// <summary>
        /// 关联项
        /// </summary>
        private decimal _relationCode;
        /// <summary>
        /// 达标分数下限
        /// </summary>
        private decimal _minScore;

        public decimal MinScore
        {
            get { return _minScore; }
            set { _minScore = value; }
        }

        /// <summary>
        /// 达标分数上限
        /// </summary>
        private decimal _maxScore;

        public decimal MaxScore
        {
            get { return _maxScore; }
            set { _maxScore = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        private DateTime _createTimestamp;
        /// <summary>
        /// 更新时间
        /// </summary>
        private DateTime _updateTimestamp;
        /// <summary>
        /// 
        /// </summary>
        private DocControlTemplate _docControlTemplate;
        /// <summary>
        /// 
        /// </summary>
        private DocTemplate _docTemplate;

        #endregion

        #region 构造器

        /// <summary>
        /// 
        /// </summary>
        public DocTemplateElement() { }

        /// <summary>
        /// 
        /// </summary>
        public DocTemplateElement(decimal parentId, decimal sortId, string elementName, string displayName, string nameFont, decimal controlStatusId, string controlFont, decimal controlWidth, decimal controlHeight, decimal controlOffset, string controlPrefix, string controlSuffix, byte newLine, decimal rowSpacing, decimal childrenIndent, byte dataType, float score, decimal relationType, decimal relationCode, DateTime createTimestamp, DateTime updateTimestamp, DocControlTemplate docControlTemplate, DocTemplate docTemplate)
        {
            this._parentId = parentId;
            this._sortId = sortId;
            this._elementName = elementName;
            this._displayName = displayName;
            this._nameFont = nameFont;
            this._controlStatusId = controlStatusId;
            this._controlFont = controlFont;
            this._controlWidth = controlWidth;
            this._controlHeight = controlHeight;
            this._controlOffset = controlOffset;
            this._controlPrefix = controlPrefix;
            this._controlSuffix = controlSuffix;
            this._newLine = newLine;
            this._rowSpacing = rowSpacing;
            this._childrenIndent = childrenIndent;
            this._dataType = dataType;
            this._score = score;
            this._relationType = relationType;
            this._relationCode = relationCode;
            this._createTimestamp = createTimestamp;
            this._updateTimestamp = updateTimestamp;
            this._docControlTemplate = docControlTemplate;
            this._docTemplate = docTemplate;
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 
        /// </summary>		    
        public decimal Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 父级元素ID
        /// </summary>	
        public decimal ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        /// <summary>
        /// 序号列
        /// </summary>	
        public decimal SortId
        {
            get { return _sortId; }
            set { _sortId = value; }
        }

        /// <summary>
        /// 元素名称
        /// </summary>	
        public string ElementName
        {
            get { return _elementName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ElementName", value, value.ToString());
                _elementName = value;
            }
        }

        /// <summary>
        /// 显示名
        /// </summary>	
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for DisplayName", value, value.ToString());
                _displayName = value;
            }
        }

        /// <summary>
        /// 名称字体
        /// </summary>	
        public string NameFont
        {
            get { return _nameFont; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for NameFont", value, value.ToString());
                _nameFont = value;
            }
        }

        /// <summary>
        /// 状态ID
        /// </summary>	
        public decimal ControlStatusId
        {
            get { return _controlStatusId; }
            set { _controlStatusId = value; }
        }

        /// <summary>
        /// 控件字体
        /// </summary>	
        public string ControlFont
        {
            get { return _controlFont; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for ControlFont", value, value.ToString());
                _controlFont = value;
            }
        }

        /// <summary>
        /// 控件宽度
        /// </summary>	
        public decimal ControlWidth
        {
            get { return _controlWidth; }
            set { _controlWidth = value; }
        }

        /// <summary>
        /// 控件高度
        /// </summary>	
        public decimal ControlHeight
        {
            get { return _controlHeight; }
            set { _controlHeight = value; }
        }

        /// <summary>
        /// 控件偏移量
        /// </summary>	
        public decimal ControlOffset
        {
            get { return _controlOffset; }
            set { _controlOffset = value; }
        }

        /// <summary>
        /// 控件前缀
        /// </summary>	
        public string ControlPrefix
        {
            get { return _controlPrefix; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for ControlPrefix", value, value.ToString());
                _controlPrefix = value;
            }
        }

        /// <summary>
        /// 控件后缀
        /// </summary>	
        public string ControlSuffix
        {
            get { return _controlSuffix; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for ControlSuffix", value, value.ToString());
                _controlSuffix = value;
            }
        }

        /// <summary>
        /// 是否换行，1为是，0为否，默认为0
        /// </summary>	
        public byte NewLine
        {
            get { return _newLine; }
            set { _newLine = value; }
        }

        /// <summary>
        /// 行间距
        /// </summary>	
        public decimal RowSpacing
        {
            get { return _rowSpacing; }
            set { _rowSpacing = value; }
        }

        /// <summary>
        /// 子项缩进
        /// </summary>	
        public decimal ChildrenIndent
        {
            get { return _childrenIndent; }
            set { _childrenIndent = value; }
        }

        /// <summary>
        /// 数据类型
        /// </summary>	
        public byte DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        /// <summary>
        /// 分值
        /// </summary>	
        public float Score
        {
            get { return _score; }
            set { _score = value; }
        }

        /// <summary>
        /// 关联类型
        /// </summary>	
        public decimal RelationType
        {
            get { return _relationType; }
            set { _relationType = value; }
        }

        /// <summary>
        /// 关联项
        /// </summary>	
        public decimal RelationCode
        {
            get { return _relationCode; }
            set { _relationCode = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>	
        public DateTime CreateTimestamp
        {
            get { return _createTimestamp; }
            set { _createTimestamp = value; }
        }

        /// <summary>
        /// 更新时间
        /// </summary>	
        public DateTime UpdateTimestamp
        {
            get { return _updateTimestamp; }
            set { _updateTimestamp = value; }
        }

        /// <summary>
        /// 
        /// </summary>	
        public DocControlTemplate DocControlTemplate
        {
            get { return _docControlTemplate; }
            set { _docControlTemplate = value; }
        }

        /// <summary>
        /// 
        /// </summary>	
        public DocTemplate DocTemplate
        {
            get { return _docTemplate; }
            set { _docTemplate = value; }
        }


        /// <summary>
        /// 元素ID+名称
        /// </summary>	
        public string ElementIdName
        {
            get { return string.Format("[{0}] {1}", _id, _elementName); }
        }

        #endregion

        public DocTemplateElement Clone()
        {
            DocTemplateElement newModel = new DocTemplateElement();
            newModel.NameFont = _nameFont;
            newModel.ParentId = this._parentId;
            newModel.SortId = this._sortId;
            newModel.DisplayName = this._displayName;
            newModel.ElementName = this._elementName;
            newModel.NameFont = this._nameFont;
            newModel.ControlStatusId = this._controlStatusId;
            newModel.ControlFont = this._controlFont;
            newModel.ControlWidth = this._controlWidth;
            newModel.ControlHeight = this._controlHeight;
            newModel.ControlOffset = this._controlOffset;
            newModel.ControlPrefix = this._controlPrefix;
            newModel.ControlSuffix = this._controlSuffix;
            newModel.NewLine = this._newLine;
            newModel.RowSpacing = this._rowSpacing;
            newModel.ChildrenIndent = this._childrenIndent;
            newModel.DataType = this._dataType;
            newModel.Score = this._score;
            newModel.RelationType = this._relationType;
            newModel.RelationCode = this._relationCode;
            newModel.CreateTimestamp = this._createTimestamp;
            newModel.UpdateTimestamp = this._updateTimestamp;
            newModel.DocControlTemplate = this._docControlTemplate;
            newModel.DocTemplate = this._docTemplate;
            //newModel.Id = this._id;
            return newModel;
        }
    }
    #endregion
}