using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.BlackBoard;
using LJJSCAD.Model;
using LJJSCAD.CommonData;
using LJJSCAD.Util;
using LJJSCAD.BlackBoard.DBItemDesign;
using DesignEnum;

namespace LJJSCAD.DrawingOper
{
    class ItemOper
    {
     
        public static List<DrawItemName> GetAllSelectedDrawItemName()
        {
            List<DrawItemName> selectedDINameLst = new List<DrawItemName>();
            for (int i = 0; i < LineRoadDesign.LineRoadDesginLst.Count; i++)
            {
                LineRoadDesignClass tmp = LineRoadDesign.LineRoadDesginLst[i];
                if (null != tmp && null != tmp.Drawingitems && tmp.Drawingitems.Count > 0)
                    selectedDINameLst.AddRange(tmp.Drawingitems);

            }
            return selectedDINameLst;        
        
        }
        public static string GetItemSubStyle(string sourceStr)
        {
            if (!string.IsNullOrEmpty(sourceStr))
            {
                return sourceStr.Trim();
            }
            else
                return DrawCommonData.StandardStyle;
        }
   
   
    }
}
