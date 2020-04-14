using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.CommonData;
using LJJSCAD.BlackBoard.LJJSDesignData.Impl;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem
{
    class CurveItemDirector:DrawItemBuildManage
    {
        public override DrawItemBuilder createDrawItemBuilder(string type,string itemID)
        {
            if (type.Equals(DrawCommonData.StandardStyle))
            {
                StandardCurveItemBuilder standardCurveItemBuilder = new StandardCurveItemBuilder();
                
                return  standardCurveItemBuilder;
            }
            else
            {
                return new StandardCurveItemBuilder();
            }
            
        }
    }
}
