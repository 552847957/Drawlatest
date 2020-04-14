using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Model.Drawing;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Geometry;
using LJJSCAD.Util;
using LJJSCAD.BlackBoard;
using LJJSCAD.CommonData;
using LJJSCAD.BlackBoard.DrawingHeaderTable;


namespace LJJSCAD.LJJSDrawing.Impl.DrawItem
{
    class TuTouTableBuilder
    {       
        public static List<string> GetHeaderTableAllAttri()
        {
           string headerTableBlckName = GetHeaderTableBlockName();
           return VDAttributeHelper.getAllAttrName(DrawCommonData.activeDocument, headerTableBlckName);
        }

        private static string GetHeaderTableBlockName()
        {
            string pth = DataControl.TuTouBiaoBlockName;
            string[] arr = pth.Split('.');
            if (arr.Length > 0)
            {
                pth = arr[0];
            }
            return pth;
        }
/// <summary>
        /// 操作HeaderTableDataManage.HeaderTableData,构造图头表,headerTableData中的key格式为“#Field”；
/// </summary>
/// <param name="allAttriLst">未经处理的图头表定义中的属性tag数据；格式为“#Field”</param>
        public static void BuildHeaderTable(List<string> allAttriLst)
        {

            for (int i = 0; i < allAttriLst.Count(); i++)
            {
                string tmpArrti = allAttriLst[i];
                if (HeaderTableDataManage.HeaderTableData.ContainsKey(tmpArrti))
                {
                    string tmpAttriValue = HeaderTableDataManage.HeaderTableData[tmpArrti];
                    string headerTableBlckName = GetHeaderTableBlockName();
                    VDAttributeHelper.setAttrValue(DrawCommonData.activeDocument, headerTableBlckName, tmpArrti, tmpAttriValue);
                    DrawCommonData.activeDocument.Redraw(true);
                }

            }

        }


    }
}
