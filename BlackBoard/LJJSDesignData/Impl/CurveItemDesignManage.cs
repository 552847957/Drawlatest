using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.BlackBoard.LJJSDesignData.Interface;

using LJJSCAD.Model.Drawing;
using LJJSCAD.Model;
using System.Collections;
using LJJSCAD.Util;

namespace LJJSCAD.BlackBoard.LJJSDesignData.Impl
{
    class CurveItemDesignManage : DesignDataManage<LineItemStruct>
    {
        /// <summary>
        /// 保存所有曲线项的设计数据；
        /// </summary>
        public static Hashtable CurveItemDesignHt = new Hashtable();
        public override LineItemStruct GetItemDrawStrucByID(string ItemID)
        {
            CurveItemModel curveItemModel = (CurveItemModel)HashUtil.FindObjByKey(ItemID,CurveItemDesignHt);
            return new LineItemStruct(curveItemModel);
            
        }
    

     
    }
}
