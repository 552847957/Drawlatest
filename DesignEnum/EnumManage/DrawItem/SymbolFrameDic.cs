using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnumManage;

namespace DesignEnum.EnumManage.DrawItem
{
   public class SymbolFrameDic : DicOper
    {
        public override void CreateDic()
        {
            Dic.Add(SymbolFrame.DoubleParallel.ToString(), "双平行线");
            Dic.Add(SymbolFrame.NoFrame.ToString(), "无外框");
        }
    }
}
