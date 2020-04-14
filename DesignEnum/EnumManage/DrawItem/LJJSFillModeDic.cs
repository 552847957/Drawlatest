using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnumManage
{
    enum LJJSFillMode { ModeNone, ModeSolid, ModeHatchBdiagonal, ModeHatchCross, ModeDiagross, ModeHatchFDiagonal, ModeHatchHorizontal, ModeHatchVertical, ModeHatchBlock, ModeHatchPattern, ModeHatchImage };
    public class LJJSFillModeDic : DicOper
    {
        public override void CreateDic()
        {
            Dic.Add(LJJSFillMode.ModeDiagross.ToString(), LJJSFillMode.ModeDiagross.ToString());
            Dic.Add(LJJSFillMode.ModeHatchBdiagonal.ToString(), LJJSFillMode.ModeHatchBdiagonal.ToString());
            Dic.Add(LJJSFillMode.ModeHatchBlock.ToString(),"块填充");
            Dic.Add(LJJSFillMode.ModeHatchCross.ToString(), LJJSFillMode.ModeHatchCross.ToString());

            Dic.Add(LJJSFillMode.ModeHatchFDiagonal.ToString(), LJJSFillMode.ModeHatchFDiagonal.ToString());
            Dic.Add(LJJSFillMode.ModeHatchHorizontal.ToString(), LJJSFillMode.ModeHatchHorizontal.ToString());
            Dic.Add(LJJSFillMode.ModeHatchImage.ToString(), "图片填充");
            Dic.Add(LJJSFillMode.ModeHatchPattern.ToString(), "图案填充");
            Dic.Add(LJJSFillMode.ModeHatchVertical.ToString(), LJJSFillMode.ModeHatchVertical.ToString());
            Dic.Add(LJJSFillMode.ModeNone.ToString(), "无填充");
            Dic.Add(LJJSFillMode.ModeSolid.ToString(), "颜色填充");
          
        }
    }
}