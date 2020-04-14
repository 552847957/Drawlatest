using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingOper;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem.ItemBuildExec
{
    class KDCBuildExec
    {
        public static KeDuChiItem GetFirstKDCItemByItemName(string itemName)
        {
            KeDuChiItem result = null;
            List<KeDuChiItem> m_KDCList;
            if (null != KeDuChiManage.LineItemKDCHt)
            {
                m_KDCList = (List<KeDuChiItem>)KeDuChiManage.LineItemKDCHt[itemName];
                if (null != m_KDCList && m_KDCList.Count > 0)
                    result = m_KDCList[0];

            }
            return result;
        }
    }
}
