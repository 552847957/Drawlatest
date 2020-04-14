using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem.CurveHasHatchItem
{
    class CurveHasHatchDirector : DrawItemBuildManage
    {
        public override DrawItemBuilder createDrawItemBuilder(string type, string itemID)
        {
            if (string.IsNullOrEmpty(type))
            {
                return new StandardCurveHasHatchBuilder();
            }
            else
            {
                return new StandardCurveHasHatchBuilder();
            }
        }
    }
}
