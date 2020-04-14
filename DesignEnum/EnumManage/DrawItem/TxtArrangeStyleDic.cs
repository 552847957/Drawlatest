using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnumManage;

namespace DesignEnum.EnumManage.DrawItem
{
  public  class TxtArrangeStyleDic:DicOper
    {
        public override void CreateDic()
        {
            Dic.Add(TxtArrangeStyle.TxtHorArrange.ToString(),"横向排列");
            Dic.Add(TxtArrangeStyle.TxtVerArrange.ToString(), "纵向排列");


        }
    }
}
