using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid.Columns;

namespace HISPlus.UserControls
{
    /// <summary>
    /// 表格控件数据校验
    /// </summary>
    public interface IGridCellChecked
    {
        /// <summary>
        /// 单元格单击事件.校验是否允许更新状态列图标
        /// </summary>
        /// <returns></returns>
        bool GridCellChecked(int rowHandle, GridColumn column);
    }
}
