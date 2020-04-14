using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DesignEnum;

namespace EnumManage
{
    public class LegendPosDic:DicOper
    {

        public override void CreateDic()
        {
            Dic.Add(LegendPosStyle.TopPos.ToString(), "顶部");
            Dic.Add(LegendPosStyle.BottomPos.ToString(), "底部");
        }
    }
}