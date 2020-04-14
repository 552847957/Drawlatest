using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.ItemStyleOper;
using DesignEnum;
namespace LJJSCAD.BlackBoard.LJJSDesignData.Impl
{
    /// <summary>
    /// 管理所有类型的绘图项名称；
    /// </summary>
    class DrawItemNamesManage
    {
        //存储所有绘图项名字；包括绘图项ID，显示名称，类别等信息；
        public static List<DrawItemName> DrawItemNamesList = new List<DrawItemName>();
        public static void SetDrawItemNamesLstFromHt()
        {
            DrawItemNamesList.Clear();
            AllDrawItemNamesBuild allitemsnames = new AllDrawItemNamesBuild();
            allitemsnames.ExecAllItemBuilder();
        }
        public static List<DrawItemName> GetNoSelectedDrawItemNamesList(List<DrawItemName> selectedDrawItemsNameLst)
        {
            List<DrawItemName> noselect = new List<DrawItemName>();
            for (int i = 0; i < DrawItemNamesList.Count; i++)
            {
                DrawItemName tmp = DrawItemNamesList[i];
                if (!isInSelectedDrawItemNamesLst(tmp, selectedDrawItemsNameLst))
                {
                    noselect.Add(tmp); 
                }
            }
                return noselect;
 
        }
        public static bool isInSelectedDrawItemNamesLst(DrawItemName sourceDrawItemName, List<DrawItemName> selectedDrawItemsNameLst)
        {
            bool result = false;
            for (int i = 0; i < selectedDrawItemsNameLst.Count; i++)
            {
                DrawItemName tmp = selectedDrawItemsNameLst[i];
                string id = tmp.DrawItemID;
                DrawItemStyle dis = tmp.ItemStyle;
                if (sourceDrawItemName.DrawItemID == id && sourceDrawItemName.ItemStyle == dis)
                {
                    result = true;
                    break;
                }

            }
            return result;

        }
    }
}
