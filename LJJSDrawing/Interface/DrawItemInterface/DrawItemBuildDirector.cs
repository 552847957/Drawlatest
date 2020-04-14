using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Model;

namespace LJJSCAD.LJJSDrawing.Interface.DrawItemInterface
{
    abstract class DrawItemBuildManage
    {
        public abstract DrawItemBuilder createDrawItemBuilder(string type,string itemID);
        public List<ulong> DrawItemBuild(DrawItemName drawItemName,LineRoadEnvironment lineRoadEnvironment)
        {
            DrawItemBuilder drawItemBuilder = createDrawItemBuilder(drawItemName.ItemSubStyle,drawItemName.DrawItemID);

            drawItemBuilder.SetLineRoadEnvironment(lineRoadEnvironment);
            drawItemBuilder.InitData(drawItemName);
            drawItemBuilder.SetItemStruct();
            drawItemBuilder.InitOtherItemDesign();//各项特有结构的初始化；
            drawItemBuilder.AddItemTitle();
            drawItemBuilder.DrawItemBody();
            return new List<ulong>();

        }

    }
}
