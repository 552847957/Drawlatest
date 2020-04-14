using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using LJJSCAD.CommonData;
using DesignEnum;

namespace LJJSCAD.DAL
{
    class TextItemDAL
    {
        public static DbDataReader GetAllTextItemConfig()
        {
            ItemDAL itemDal = new ItemDAL(DrawItemStyle.TextItem);
            return itemDal.GetDrawItemDataReader();
        }
    }
}
