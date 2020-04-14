using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DesignEnum;

namespace EnumManage
{
    public class ItemTitlePosDic:DicOper
    {
        public override void CreateDic()
        {
            Dic.Add(ItemTitlePos.Left.ToString(),"左侧");
            Dic.Add(ItemTitlePos.Mid.ToString(), "中间");
            Dic.Add(ItemTitlePos.Right.ToString(), "右侧");
        }
    }
}