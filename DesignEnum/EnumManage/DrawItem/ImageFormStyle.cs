using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnumManage
{
    public class ImageFormStyle
    {
        public static readonly string YXPicture = "YXPicture";//岩屑图像
        public static readonly string YGXWPicture = "YGXWPicture";//荧光显微图像 
    }
    public class ImageFormStyleDic : DicOper
    {
        public override void CreateDic()
        {
            Dic.Add(ImageFormStyle.YXPicture, "岩屑图像");
            Dic.Add(ImageFormStyle.YGXWPicture, "荧光显微图像");
        }
    }

}