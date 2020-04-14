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
    class CurveHasHatchDesignManage : DesignDataManage<CurveHasHatchDesignClass>
    {
        public static Hashtable CurveHasHatchDesignManageHt = new Hashtable();
        public override CurveHasHatchDesignClass GetItemDrawStrucByID(string ItemID)
        {
            CurveHasHatchItemModel ItemModel = (CurveHasHatchItemModel)HashUtil.FindObjByKey(ItemID, CurveHasHatchDesignManageHt);
            return new CurveHasHatchDesignClass(ItemModel);
        }
    }
}
