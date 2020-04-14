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
    class SymbolItemDesignManage : DesignDataManage<SymbolItemDesignStruct>
    {
        public static Hashtable SymbolItemDesignManageHt = new Hashtable();
        public override SymbolItemDesignStruct GetItemDrawStrucByID(string ItemID)
        {
            SymbolItemModel symbolItemModel = (SymbolItemModel)HashUtil.FindObjByKey(ItemID, SymbolItemDesignManageHt);
            return new SymbolItemDesignStruct(symbolItemModel);
        }
    }
}
