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
    class TextItemDesignManage : DesignDataManage<TxtItemClass>
    {
        public static Hashtable TextItemDesignManageHt = new Hashtable();
        public override TxtItemClass GetItemDrawStrucByID(string ItemID)
        {
            TextItemModel txtItemModel = (TextItemModel)HashUtil.FindObjByKey(ItemID, TextItemDesignManageHt);
            return new TxtItemClass(txtItemModel);
        }
    }
}
