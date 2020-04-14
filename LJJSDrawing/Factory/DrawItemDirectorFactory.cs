using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.LJJSDrawing.Impl.DrawItem;
using LJJSCAD.LJJSDrawing.Impl;
using LJJSCAD.LJJSDrawing.Impl.DrawItem.HatchRectItem;
using LJJSCAD.LJJSDrawing.Impl.DrawItem.NormalPuTuItem;
using LJJSCAD.LJJSDrawing.Impl.DrawItem.CurveHasHatchItem;

namespace LJJSCAD.LJJSDrawing.Factory
{
    class DrawItemDirectorFactory:ItemStyleOper.ItemStyleOperFrameHasReturn
    { 

        public override object LineItemOper()
        {
            return new CurveItemDirector();
        }

        public override object TextItemOper()
        {
            return new TextItemDirector();
        }

        public override object SymbolItemOper()
        {
            return new SymbolItemDirector();
        }

        public override object ImageItemOper()
        {
            return new ImageItemDirector();
        }

        public override object HCGZItemOper()
        {
            return new HCGZItemDirector();
        }

        public override object MultiHatchCurveItemOper()
        {
            return new MultiHatchCurveItemDirector();
        }

        public override object HatchRectItemOper()
        {
            return new HatchRectDirector();
        }

        public override object NormalPuTuItemOper()
        {
            return new NormalPuTuDirector();
        }

        public override object CurveHasHatchItemOper()
        {
            return new CurveHasHatchDirector();
        }
    }
}
