using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DesignEnum;

namespace EnumManage
{
    public class CurveStyleDic:DicOper
    {
        public override void CreateDic()
        {
            Dic.Add(CJQXLineClass.Continus.ToString(), "折线");
            Dic.Add(CJQXLineClass.StickLine.ToString(), "棒线");
        }
    }
}