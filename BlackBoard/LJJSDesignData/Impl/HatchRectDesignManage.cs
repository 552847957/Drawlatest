using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Model.Drawing;
using LJJSCAD.BlackBoard.LJJSDesignData.Interface;
using System.Collections;
using LJJSCAD.Util;
using LJJSCAD.Model;

namespace LJJSCAD.BlackBoard.LJJSDesignData.Impl
{
    class HatchRectDesignManage : DesignDataManage<HatchRectDesignClass>
    {
        public static Hashtable HatchRectDesignManageHt = new Hashtable();
        public override HatchRectDesignClass GetItemDrawStrucByID(string ItemID)
        {
            HatchRectItemModel ItemModel = (HatchRectItemModel)HashUtil.FindObjByKey(ItemID, HatchRectDesignManageHt);
            return new HatchRectDesignClass(ItemModel);
       
        }
    }
}
