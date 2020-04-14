using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DesignEnum;

namespace EnumManage
{
    public class CJQXChaoJieDic:DicOper
    {
        public override void CreateDic()
        {
            Dic.Add(CJQXChaoJie.BiaoZhu.ToString(), "标注");
            Dic.Add(CJQXChaoJie.JianCai.ToString(),"剪裁");
            Dic.Add(CJQXChaoJie.PingYi.ToString(), "平移");
            Dic.Add(CJQXChaoJie.ZheHui.ToString(), "折回");

        }
    }
}