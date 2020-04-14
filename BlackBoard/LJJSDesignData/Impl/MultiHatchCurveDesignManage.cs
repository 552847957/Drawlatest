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
    class MultiHatchCurveDesignManage : DesignDataManage<MultiHatchCurveDesignClass>
    {
        public static Hashtable MultiHatchCurveDesignManageHt = new Hashtable();
        public override MultiHatchCurveDesignClass GetItemDrawStrucByID(string ItemID)
        {
            MultiHatchCurveItemModel mHCurveItemModel = (MultiHatchCurveItemModel)HashUtil.FindObjByKey(ItemID, MultiHatchCurveDesignManageHt);
            return new MultiHatchCurveDesignClass(mHCurveItemModel);
        }
    }
}
