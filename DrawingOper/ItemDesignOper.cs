using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.BlackBoard.LJJSDesignData.Impl;

namespace LJJSCAD.DrawingOper
{
    class ItemDesignBlackBoardRead:ItemStyleOper.ItemStyleOperFrameHasReturn
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public ItemDesignBlackBoardRead(string iD)
        {
            this._id = iD;

        } 

        public override object LineItemOper()
        {
            CurveItemDesignManage curveItemDesignManage = new CurveItemDesignManage();
            return curveItemDesignManage.GetItemDrawStrucByID(_id);
        }

        public override object TextItemOper()
        {
            TextItemDesignManage txtItemDesignManage = new TextItemDesignManage();
            return txtItemDesignManage.GetItemDrawStrucByID(_id);
        }

        public override object SymbolItemOper()
        {
            SymbolItemDesignManage symbolItemDesignManage = new SymbolItemDesignManage();
            return symbolItemDesignManage.GetItemDrawStrucByID(_id);
        }

        public override object ImageItemOper()
        {
            ImageItemDesignManage imageItemDesignManage = new ImageItemDesignManage();
            return imageItemDesignManage.GetItemDrawStrucByID(_id);
        }

        public override object HCGZItemOper()
        {
            HCGZItemDesignManage hcgzItemDesignManage = new HCGZItemDesignManage();
            return hcgzItemDesignManage.GetItemDrawStrucByID(_id);
        }

        public override object MultiHatchCurveItemOper()
        {
            MultiHatchCurveDesignManage itemDesignManage = new MultiHatchCurveDesignManage();
            return itemDesignManage.GetItemDrawStrucByID(_id);
        }

        public override object HatchRectItemOper()
        {
            HatchRectDesignManage itemDesignManage = new HatchRectDesignManage();
            return itemDesignManage.GetItemDrawStrucByID(_id);
        }

        public override object NormalPuTuItemOper()
        {
            NormalPuTuDesignManage itemDesignManage = new NormalPuTuDesignManage();
            return itemDesignManage.GetItemDrawStrucByID(_id);
        }

        public override object CurveHasHatchItemOper()
        {
            CurveHasHatchDesignManage itemDesignManage = new CurveHasHatchDesignManage();
            return itemDesignManage.GetItemDrawStrucByID(_id);
        }
    }
}
