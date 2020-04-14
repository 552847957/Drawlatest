using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.BlackBoard.LJJSDesignData.Interface;
using LJJSCAD.Model.Drawing;
using System.Collections;
using LJJSCAD.Util;
using LJJSCAD.Model;

namespace LJJSCAD.BlackBoard.LJJSDesignData.Impl
{
    class ImageItemDesignManage : DesignDataManage<ImageItemDesignClass>
    {
        public static Hashtable ImageItemDesignManageHt = new Hashtable();
        public override ImageItemDesignClass GetItemDrawStrucByID(string ItemID)
        {
            ImageItemModel imageItemModel = (ImageItemModel)HashUtil.FindObjByKey(ItemID, ImageItemDesignManageHt);
            return new ImageItemDesignClass(imageItemModel);
        }
    }
}
