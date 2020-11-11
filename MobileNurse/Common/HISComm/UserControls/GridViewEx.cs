using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.FilterEditor;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace HISPlus.UserControls
{
    public class GridViewEx : GridView
    {
        protected override FilterColumnCollection CreateFilterColumnCollection()
        {
            //FilterColumnCollection f = base.CreateFilterColumnCollection();
            //return f as ViewFilterColumnCollectionEx;

            return base.CreateFilterColumnCollection() as ViewFilterColumnCollectionEx;
        }

    }

    public abstract class ViewFilterColumnCollectionEx : ViewFilterColumnCollection
    {
        protected ViewFilterColumnCollectionEx(ColumnView view)
            : base(view)
        {

        }

        protected override bool IsValidForFilter(GridColumn column)
        {
            return true;
        }
    }
}
