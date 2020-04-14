using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.CommonData;
using LJJSCAD.LJJSDrawing.Impl.DrawItem.SymbolItem;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem
{
    class SymbolItemDirector : DrawItemBuildManage
    {

        public override DrawItemBuilder createDrawItemBuilder(string type, string itemID)
        {
            string tmp = type.ToLower();
            // 标准符号项、岩性剖面、岩心位置、井壁取心；
            if (tmp.Equals(DrawCommonData.StandardStyle))
            {
                StandardSymbolItemBuilder standardCurveItemBuilder = new StandardSymbolItemBuilder();
                return standardCurveItemBuilder;
            }
            else if (tmp.Equals("yxpm"))
            {
                YXPouMianItemDraw yxpmItemDraw=new YXPouMianItemDraw();
                return yxpmItemDraw;
 
            }
            else if (tmp.Equals("jbqx"))
            {
                JBQXItemDraw yxpmItemDraw = new JBQXItemDraw();
                return yxpmItemDraw;

            }
            else
            {
                return new StandardSymbolItemBuilder();
            }
        }
    }
}
