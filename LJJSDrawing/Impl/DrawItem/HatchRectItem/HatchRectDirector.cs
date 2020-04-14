using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.CommonData;
using LJJSCAD.Util;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem.HatchRectItem
{
    class HatchRectDirector : DrawItemBuildManage
    {
        public override DrawItemBuilder createDrawItemBuilder(string type, string itemID)
        {
            if (string.IsNullOrEmpty(type))
            {
                return new StandardHatchRectBuilder();
            }
            else
            {
                return new StandardHatchRectBuilder();
            }
        }
    }
}
