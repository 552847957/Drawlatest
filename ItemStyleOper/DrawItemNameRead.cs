using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Model;
using LJJSCAD.BlackBoard.LJJSDesignData.Impl;
using DesignEnum;

namespace LJJSCAD.ItemStyleOper
{
    class DrawItemNameRead:ItemStyleOperFrameHasReturn
    {
         private string _iD;

        public string ID
        {
            get { return _iD; }
            set { _iD = value; }
        }
        public DrawItemNameRead(string drawItemID)
        {
            this._iD = drawItemID.Trim();
        }

        public override object LineItemOper()
        {
            CurveItemModel cur = (CurveItemModel)CurveItemDesignManage.CurveItemDesignHt[_iD];
            DrawItemName drawItemName = new DrawItemName();
            drawItemName.ItemStyle = DrawItemStyle.LineItem;
            drawItemName.DrawItemShowName = cur.CJQXShowName.Trim();
            drawItemName.ItemSubStyle = cur.LIDISubStyle.Trim();
            drawItemName.DrawItemID = this._iD;

            return drawItemName;
        }

        public override object TextItemOper()
        {
            TextItemModel txt = (TextItemModel)TextItemDesignManage.TextItemDesignManageHt[_iD];
            DrawItemName drawItemName = new DrawItemName();
            drawItemName.ItemStyle = DrawItemStyle.TextItem;
            drawItemName.DrawItemShowName = txt.TxtItemName.Trim();
            drawItemName.ItemSubStyle = txt.TxtDiSubStyle.Trim();
            drawItemName.DrawItemID = this._iD;
            return drawItemName;

        }

        public override object SymbolItemOper()
        {
            SymbolItemModel sym = (SymbolItemModel)SymbolItemDesignManage.SymbolItemDesignManageHt[_iD];
            DrawItemName drawItemName = new DrawItemName();
            drawItemName.ItemStyle = DrawItemStyle.SymbolItem;
            drawItemName.DrawItemShowName = sym.ItemName.Trim();
            drawItemName.ItemSubStyle = sym.SymDISubStyle.Trim();
            drawItemName.DrawItemID = this._iD;

            return drawItemName;
        }

        public override object ImageItemOper()
        {
            ImageItemModel imag = (ImageItemModel)ImageItemDesignManage.ImageItemDesignManageHt[_iD];
            DrawItemName drawItemName = new DrawItemName();
            drawItemName.ItemStyle = DrawItemStyle.ImageItem;
            drawItemName.DrawItemShowName = imag.ItemName.Trim();
            drawItemName.ItemSubStyle = imag.ImageItemSubStyle;
            drawItemName.DrawItemID = this._iD;
            return drawItemName;
        }

        public override object HCGZItemOper()
        {
            HCGZItemModel hcitem = (HCGZItemModel)HCGZItemDesignManage.hcgzItemManageHt[_iD];
            DrawItemName drawItemName = new DrawItemName();
            drawItemName.ItemStyle = DrawItemStyle.HCGZItem;
            drawItemName.DrawItemShowName = hcitem.ItemShowName;
            drawItemName.ItemSubStyle = hcitem.ItemSubStyle;
            drawItemName.DrawItemID = this._iD;
            return drawItemName;
        }

        public override object MultiHatchCurveItemOper()
        {
            MultiHatchCurveItemModel curItem = (MultiHatchCurveItemModel)MultiHatchCurveDesignManage.MultiHatchCurveDesignManageHt[_iD];
            DrawItemName drawItemName = new DrawItemName();
            drawItemName.ItemStyle = DrawItemStyle.MultiHatchCurveItem;
            drawItemName.DrawItemShowName = curItem.ItemShowName;
            drawItemName.ItemSubStyle = CommonData.DrawCommonData.StandardStyle;
            drawItemName.DrawItemID = this._iD;
            return drawItemName;
        }

        public override object HatchRectItemOper()
        {
            HatchRectItemModel curItem = (HatchRectItemModel)HatchRectDesignManage.HatchRectDesignManageHt[_iD];
            DrawItemName drawItemName = new DrawItemName();
            drawItemName.ItemStyle = DrawItemStyle.HatchRectItem;
            drawItemName.DrawItemShowName = curItem.ItemShowName;
            drawItemName.ItemSubStyle =curItem.ItemSubStyle;
            drawItemName.DrawItemID = this._iD;
            return drawItemName;
        }

        public override object NormalPuTuItemOper()
        {
            NormalPuTuItemModel curItem = (NormalPuTuItemModel)NormalPuTuDesignManage.NormalPuTuDesignManageHt[_iD];
            DrawItemName drawItemName = new DrawItemName();
            drawItemName.ItemStyle = DrawItemStyle.NormalPuTuItem;
            drawItemName.DrawItemShowName = curItem.ItemShowName;
            drawItemName.ItemSubStyle = curItem.ItemSubStyle;
            drawItemName.DrawItemID = this._iD;
            return drawItemName;
        }

        public override object CurveHasHatchItemOper()
        {
            CurveHasHatchItemModel curItem = (CurveHasHatchItemModel)CurveHasHatchDesignManage.CurveHasHatchDesignManageHt[_iD];
            DrawItemName drawItemName = new DrawItemName();
            drawItemName.ItemStyle = DrawItemStyle.CurveHasHatchItem;
            drawItemName.DrawItemShowName = curItem.CurveShowName;
            drawItemName.ItemSubStyle = curItem.ItemSubStyle;
            drawItemName.DrawItemID = this._iD;
            return drawItemName;
        }
    }
}
