using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Util;
using DesignEnum;

namespace LJJSCAD.Model.Drawing
{
   abstract class ItemDesignClass
    {
        public string ID { set; get; }	//ID
        public string ItemShowName { set; get; }	//绘图项显示名称
        public int ItemOrder { set; get; }	//绘图项的序号
        public string ItemSubStyle { set; get; }//子类型
        public ItemTitlePos ItemShowNamePosition { set; get; }//绘图项显示名的位置
        protected void ItemConstructor(string itemID, string itemShowName, string itemOrder, string itemSubStyle, string itemShowNamePosition)
        {
            this.ID = StrUtil.GetNoNullStr(itemID);
            this.ItemShowName = StrUtil.GetNoNullStr(itemShowName);
            this.ItemOrder = StrUtil.StrToInt(itemOrder,0,"绘图项顺序设计有误，为非数值型");
            this.ItemShowNamePosition = EnumUtil.GetEnumByStr(itemShowNamePosition, ItemTitlePos.Mid);
            this.ItemSubStyle = StrUtil.GetNoNullStr(itemSubStyle);

        }
    }
}
