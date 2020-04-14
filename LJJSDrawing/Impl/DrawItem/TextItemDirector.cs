using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.CommonData;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem
{
    class TextItemDirector:DrawItemBuildManage
    {

        public override DrawItemBuilder createDrawItemBuilder(string type, string itemID)
        {
            if (type.Equals(DrawCommonData.StandardStyle))
            {
                StandardTextItemBuilder standardCurveItemBuilder = new StandardTextItemBuilder();
                return standardCurveItemBuilder;
            }
            else
            {
                return new StandardTextItemBuilder();
            }
            
        }
    }
}
