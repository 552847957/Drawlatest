using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.CommonData;
using LJJSCAD.LJJSDrawing.Impl.DrawItem;

namespace LJJSCAD.LJJSDrawing.Impl
{
    class HCGZItemDirector:DrawItemBuildManage
    {
        public override DrawItemBuilder createDrawItemBuilder(string type, string itemID)
        {

            string tmp = type.ToLower().Trim();
            if (tmp.Equals(DrawCommonData.StandardStyle))
            {
                StandardHCGZItemBuilder standhcgzBuilder = new StandardHCGZItemBuilder();
                return standhcgzBuilder;
            }
            else
            {
                return new StandardHCGZItemBuilder();
            }
        }
    }
}
