using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnumManage;

namespace DesignEnum.EnumManage.DrawItem
{
  public  class ThinBZPosStyleDic:DicOper
    {
        public override void CreateDic()
        {
            Dic.Add(ThinBZPosStyle.RightPos.ToString(),"右侧");
            Dic.Add(ThinBZPosStyle.TopPos.ToString(), "上侧");
        }
    }
}
