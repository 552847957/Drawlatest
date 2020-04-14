using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnumManage
{
    enum HatchRectItemSubStyle { DirectFill, ColorField }//直接填充或者是颜色或者是图案，填充颜色来自字段，需要访问颜色表，根据颜色代码获得具体的颜色值；
    public class HatchRectSubStyleDic:DicOper
    {
        public override void CreateDic()
        {
            Dic.Add(HatchRectItemSubStyle.ColorField.ToString(),"颜色字段");
            Dic.Add(HatchRectItemSubStyle.DirectFill.ToString(),"直接填充");
        }
    }
}