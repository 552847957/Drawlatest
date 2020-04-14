using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.CommonData;
using LJJSCAD.Util;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem
{
    class MultiHatchCurveItemDirector : DrawItemBuildManage
    {
        public override DrawItemBuilder createDrawItemBuilder(string type, string itemID)
        {

            return new MultiHatchCurveItemBuilder();
           
        }
    }
}
