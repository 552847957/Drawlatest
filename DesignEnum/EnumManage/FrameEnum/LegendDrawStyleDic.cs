using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DesignEnum;

namespace EnumManage
{
    public class LegendDrawStyleDic:DicOper
    {
        public override void CreateDic()
        {
            Dic.Add(LegendDrawStyle.HaveOutFrame.ToString(), "网格");
            Dic.Add(LegendDrawStyle.NoOutFrame.ToString(), "矩形框");
        }
    }
}