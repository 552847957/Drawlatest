using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnumManage;

namespace DesignEnum.EnumManage.DrawItem
{
   public class TxtItemOutFrameDic:DicOper
    {
        public override void CreateDic()
        {
            Dic.Add(TxtItemOutFrame.DoubleParallel.ToString(), "双平行线外框");
            Dic.Add(TxtItemOutFrame.DouPelAndVerDivide.ToString(), "双平行线纵分外框");
            Dic.Add(TxtItemOutFrame.NoFrame.ToString(), "无外框");
        }
    }
}
