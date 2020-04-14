using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Util;
using System.Drawing;
using DesignEnum;

namespace LJJSCAD.Drawing.Hatch
{
    public enum LJJSFillMode { ModeNone, ModeSolid, ModeHatchBdiagonal, ModeHatchCross, ModeDiagross, ModeHatchFDiagonal, ModeHatchHorizontal, ModeHatchVertical, ModeHatchBlock,ModeHatchPattern,ModeHatchImage };

    /// <summary>
    /// 填充类设计
    /// </summary>
    public class LJJSHatch
    {
        public LJJSFillMode FillMode { set; get; }
        public bool IsDrawBoundary { set; get; }
        public int PenColor { set; get; }
        public double PenWidth { set; get; }
        public int FillBKColor { set; get; }
        public int FillColor { set; get; }
        public string HatchPattern { set; get; }
        public double HatchScale { set; get; }
        public string HatchBlk { set; get; }
        public string HatchImage { set; get; }
        public LJJSHatch(string fillMode, string isDrawBoundary, string penColor, string penWidth
            , string fillBKColor, string fillColor, string hatchPattern, string hatchScale, string hatchBlk
            , string HatchImage)
        {         

            FillMode = EnumUtil.GetEnumByStr(fillMode,LJJSFillMode.ModeNone);
            IsDrawBoundary = BoolUtil.GetBoolByBindID(isDrawBoundary, false);
            PenColor = StrUtil.StrToInt(penColor, Color.White.ToArgb(), "填充边界颜色设计有误，为非整型") ;
            PenWidth = StrUtil.StrToDouble(penWidth, 0, "填充边界线条宽度设计有误，为非整型");
            this.FillBKColor = StrUtil.StrToInt(fillBKColor, Color.White.ToArgb(), "填充背景颜色设计有误，为非整型");//如果填充类型为solid，背景色没有作用；
            this.FillColor = StrUtil.StrToInt(fillColor, Color.White.ToArgb(), "填充颜色设计有误，为非整型");      
            this.HatchPattern = StrUtil.GetNoNullStr(hatchPattern);
            this.HatchBlk = StrUtil.GetNoNullStr(hatchBlk);
            this.HatchImage = StrUtil.GetNoNullStr(HatchImage);
            this.HatchScale = StrUtil.StrToDouble(hatchScale,1,"填充设计有误，为非数值型");

        }
        public LJJSHatch(string fillColor,string isDrawBoundary)
        {
            this.FillMode = LJJSFillMode.ModeSolid;
            this.FillColor = StrUtil.StrToInt(fillColor, Color.White.ToArgb(), "填充颜色设计有误，为非整型");
            this.FillBKColor = Color.White.ToArgb();
            this.HatchBlk = "";
            this.HatchImage = "";
            this.HatchPattern = "";
            this.HatchScale = 1;
            this.IsDrawBoundary = BoolUtil.GetBoolByBindID(isDrawBoundary, true);
            this.PenWidth = 0;

        }


    }
}
