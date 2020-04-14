using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Model;
using LJJSCAD.BlackBoard.LJJSDesignData.Impl;

namespace LJJSCAD.ItemStyleOper
{
    class ItemFromTableRead:ItemStyleOperFrameHasReturn
    {
        private string _iD;

        public string ID
        {
            get { return _iD; }
            set { _iD = value; }
        }
        public ItemFromTableRead(string iD)
        {
            this._iD = iD;
        }
        public override object LineItemOper()
        {
            CurveItemModel cur = (CurveItemModel)CurveItemDesignManage.CurveItemDesignHt[_iD];
            return cur.CJQXFromTableName.Trim();
        }

        public override object TextItemOper()
        {
            TextItemModel txt = (TextItemModel)TextItemDesignManage.TextItemDesignManageHt[_iD];
            return txt.TxtItemFromTbName.Trim();
        }

        public override object SymbolItemOper()
        {
            SymbolItemModel sym = (SymbolItemModel)SymbolItemDesignManage.SymbolItemDesignManageHt[_iD];
            return sym.ItemTable.Trim();
        }

        public override object ImageItemOper()
        {
            ImageItemModel imag = (ImageItemModel)ImageItemDesignManage.ImageItemDesignManageHt[_iD];
            return imag.ItemFromTable.Trim();
        }

        public override object HCGZItemOper()
        {
            HCGZItemModel hcgzItem = (HCGZItemModel)HCGZItemDesignManage.hcgzItemManageHt[_iD];
            Dictionary<string, string> tableDic = new Dictionary<string, string>();
            tableDic.Add("mainTable", hcgzItem.Main_Table);
            tableDic.Add("closedAreaTable",hcgzItem.ClosedArea_Table);
            return tableDic;
        }

        public override object MultiHatchCurveItemOper()
        {

            MultiHatchCurveItemModel model = (MultiHatchCurveItemModel)MultiHatchCurveDesignManage.MultiHatchCurveDesignManageHt[_iD];
            return model.ItemFromTable.Trim();
        }

        public override object HatchRectItemOper()
        {
            HatchRectItemModel model = (HatchRectItemModel)HatchRectDesignManage.HatchRectDesignManageHt[_iD];
            return model.ItemFromTable.Trim();
        }

        public override object NormalPuTuItemOper()
        {
            NormalPuTuItemModel model = (NormalPuTuItemModel)NormalPuTuDesignManage.NormalPuTuDesignManageHt[_iD];
            Dictionary<string, string> tableDic = new Dictionary<string, string>();
            tableDic.Add("mainTable", model.Main_TableName);
            tableDic.Add("closedAreaTable", model.ClosedArea_Table);
            return tableDic;
        }

        public override object CurveHasHatchItemOper()
        {
            CurveHasHatchItemModel model = (CurveHasHatchItemModel)CurveHasHatchDesignManage.CurveHasHatchDesignManageHt[_iD];
            return model.CurveFromTableName.Trim();
        }
    }
}
