using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Model;


namespace LJJSCAD.BlackBoard.LJJSDesignData.Interface
{
    class DrawItemFactory:ItemStyleOper.ItemStyleOperFrameHasReturn
    {

        public override object LineItemOper()
        {
            return new CurveItemModel();
        }

        public override object TextItemOper()
        {
            return new TextItemModel();
        }

        public override object SymbolItemOper()
        {
            return new SymbolItemModel();
        }

        public override object ImageItemOper()
        {
            return new ImageItemModel();
        }
        public override object HCGZItemOper()
        {
            return  new HCGZItemModel();
        }

        public override object MultiHatchCurveItemOper()
        {
            return new MultiHatchCurveItemModel();
        }

        public override object HatchRectItemOper()
        {
            return new HatchRectItemModel();
        }

        public override object NormalPuTuItemOper()
        {
            return new NormalPuTuItemModel();
        }

        public override object CurveHasHatchItemOper()
        {
            return new CurveHasHatchItemModel();
        }
    }
}
