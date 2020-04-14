using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.LJJSDrawing.Impl.DrawItem;
using DesignEnum;

namespace LJJSCAD.LJJSDrawing.Factory
{
    class AddPerJDTextItemFactory
    {
        public static AddTextItem CreateAddTextItemToFigureInstance(TxtItemStyle txtItemStyle)
        {
            if (txtItemStyle.Equals(TxtItemStyle.YsStyle))
                return new AddYSTextItemToFigure();
            else if (txtItemStyle.Equals(TxtItemStyle.NumberStyle))
                return new AddNumberTextItemToFigure();
            else
                return new AddNormalTextItemToFigure();

        }
    }
}
