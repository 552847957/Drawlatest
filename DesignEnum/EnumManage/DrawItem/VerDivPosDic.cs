using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnumManage;

namespace DesignEnum.EnumManage.DrawItem
{
   public class VerDivPosDic:DicOper
    {
        public override void CreateDic()
        {
            Dic.Add(VerDivPos.LeftPos.ToString(),"左侧");
            Dic.Add(VerDivPos.CenterPos.ToString(), "中部");
            Dic.Add(VerDivPos.RightPos.ToString(), "右侧");

        }
    }
}
