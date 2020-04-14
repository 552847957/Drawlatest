using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnumManage;

namespace DesignEnum.EnumManage.DrawItem
{
  public  class TxtItemStyleDic:DicOper
    {
        public override void CreateDic()
        {
            Dic.Add(TxtItemStyle.YsStyle.ToString(),"颜色型");
            Dic.Add(TxtItemStyle.TxtStyle.ToString(), "文本型");
            Dic.Add(TxtItemStyle.NumberStyle.ToString(), "数字型");
        }
    }
}
