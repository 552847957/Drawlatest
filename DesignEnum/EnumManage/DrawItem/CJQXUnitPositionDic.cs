using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EnumManage;


namespace DesignEnum
{
    public class CJQXUnitPositionDic:DicOper

    {
        public override void CreateDic()
        {
            Dic.Add(CJQXUnitPosition.AtRight.ToString(), "居右");
            Dic.Add(CJQXUnitPosition.MidBottomPos.ToString(), "底部中间");
            Dic.Add(CJQXUnitPosition.RightPos.ToString(), "右侧");
           

        }
    }
}