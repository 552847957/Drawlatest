using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnumManage;

namespace DesignEnum.EnumManage.DrawItem
{
  public  class TxtdistributionDic:DicOper
    {

        public override void CreateDic()
        {
            Dic.Add(Txtdistribution.Txtfocus.ToString(), "集中");
            Dic.Add(Txtdistribution.TxtSpread.ToString(), "分散");
        }
    }
}
