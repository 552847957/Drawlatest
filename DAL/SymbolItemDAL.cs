using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using LJJSCAD.CommonData;
using DesignEnum;

namespace LJJSCAD.DAL
{
    class SymbolItemDAL
    {
        public static DbDataReader GetAllSymbolItemConfig()
        {
            ItemDAL itemDal = new ItemDAL(DrawItemStyle.SymbolItem);
            return itemDal.GetDrawItemDataReader();
        }
    }
}
