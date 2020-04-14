using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesignEnum;

namespace LJJSCAD.ItemStyleOper
{
   abstract class ItemStyleOperFrameHasReturn
    {
       private DrawItemStyle _selfdrawItemStyle;

       public DrawItemStyle SelfdrawItemStyle
        {
            get { return _selfdrawItemStyle; }
            set { _selfdrawItemStyle = value; }
        }
        public abstract object LineItemOper();
        public abstract object TextItemOper();
        public abstract object SymbolItemOper();
        public abstract object ImageItemOper();
        public abstract object HCGZItemOper();

        public abstract object MultiHatchCurveItemOper();
        public abstract object HatchRectItemOper();
        public abstract object NormalPuTuItemOper();
        public abstract object CurveHasHatchItemOper();
       
        public object ReturnItemInstance(DrawItemStyle ItemStyle)
        {
            _selfdrawItemStyle = ItemStyle;
            if (ItemStyle.Equals(DrawItemStyle.LineItem))
                return LineItemOper();
            else if (ItemStyle.Equals(DrawItemStyle.SymbolItem))
                return SymbolItemOper();
            else if (ItemStyle.Equals(DrawItemStyle.TextItem))
                return TextItemOper();
            else if (ItemStyle.Equals(DrawItemStyle.ImageItem))
                return ImageItemOper();
            else if (ItemStyle.Equals(DrawItemStyle.HCGZItem))
                return HCGZItemOper();
            else if (ItemStyle.Equals(DrawItemStyle.MultiHatchCurveItem))
                return MultiHatchCurveItemOper();
            else if (ItemStyle.Equals(DrawItemStyle.HatchRectItem))
                return HatchRectItemOper();
            else if (ItemStyle.Equals(DrawItemStyle.NormalPuTuItem))
                return NormalPuTuItemOper();
            else if (ItemStyle.Equals(DrawItemStyle.CurveHasHatchItem))
                return CurveHasHatchItemOper();
            else
                return null;
        }
    
    }
}
