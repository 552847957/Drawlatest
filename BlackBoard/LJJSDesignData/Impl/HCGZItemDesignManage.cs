using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.BlackBoard.LJJSDesignData.Interface;
using LJJSCAD.Model.Drawing;
using System.Collections;
using LJJSCAD.Model;
using LJJSCAD.Util;

namespace LJJSCAD.BlackBoard.LJJSDesignData.Impl
{
    class HCGZItemDesignManage : DesignDataManage<HCGZItemDesignClass>
    {
        public static Hashtable hcgzItemManageHt = new Hashtable();

        public override HCGZItemDesignClass GetItemDrawStrucByID(string ItemID)
        {
            HCGZItemModel hcgzItemModel = (HCGZItemModel)HashUtil.FindObjByKey(ItemID, hcgzItemManageHt);
            return new HCGZItemDesignClass(hcgzItemModel);
        }
    }
}
