using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnumManage
{
    public enum CeWangStyleEnum{DengFen,DengCha,DuiShuZhengXiang,DuiShuFanXiang,None};
    public class CeWangStyleDic:DicOper
    {
        public override void CreateDic()
        {
            Dic.Add(CeWangStyleEnum.DengCha.ToString(), "等差");
            Dic.Add(CeWangStyleEnum.DengFen.ToString(), "等分");
            Dic.Add(CeWangStyleEnum.DuiShuZhengXiang.ToString(), "对数正向");
            Dic.Add(CeWangStyleEnum.DuiShuFanXiang.ToString(), "对数反向");
            Dic.Add(CeWangStyleEnum.None.ToString(), "无");
        }
    }
}