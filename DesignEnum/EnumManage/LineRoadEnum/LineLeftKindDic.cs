using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DesignEnum;

namespace EnumManage
{
    public class LineLeftKindDic:DicOper
    {
        public override void CreateDic()
        {
            Dic.Add(LineLeftKind.enline.ToString(), "左侧直线");
            Dic.Add(LineLeftKind.unline.ToString(), "左侧无线");
            Dic.Add(LineLeftKind.arrowline.ToString(), "左侧箭头");
        }
    }
}