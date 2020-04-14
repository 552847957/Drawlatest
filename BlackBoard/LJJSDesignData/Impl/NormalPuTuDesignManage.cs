using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Model.Drawing;
using LJJSCAD.BlackBoard.LJJSDesignData.Interface;
using System.Collections;
using LJJSCAD.Model;
using LJJSCAD.Util;

namespace LJJSCAD.BlackBoard.LJJSDesignData.Impl
{
    class NormalPuTuDesignManage : DesignDataManage<NormalPuTuDesignClass>
    {
        public static Hashtable NormalPuTuDesignManageHt = new Hashtable();
        public override NormalPuTuDesignClass GetItemDrawStrucByID(string ItemID)
        {
            NormalPuTuItemModel mHCurveItemModel = (NormalPuTuItemModel)HashUtil.FindObjByKey(ItemID, NormalPuTuDesignManageHt);
            return new NormalPuTuDesignClass(mHCurveItemModel);
        }
    }
}
