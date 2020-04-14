using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.CommonData;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem
{
    class ImageItemDirector:DrawItemBuildManage
    {
        public override DrawItemBuilder createDrawItemBuilder(string type, string itemID)
        {
            if (string.IsNullOrEmpty(type))
                return new StandardImageItemBuilder();
            string tmp = type.ToLower().Trim();
            if (tmp.Equals(DrawCommonData.StandardStyle))
            {
                StandardImageItemBuilder standardCurveItemBuilder = new StandardImageItemBuilder();
                return standardCurveItemBuilder;
            }
            else
            {
                return new StandardImageItemBuilder();
            }
        }
    }
}
