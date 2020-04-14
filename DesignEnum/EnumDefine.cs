using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignEnum
{
   public enum CJQXLineClass { Continus, StickLine };//折线，棒线；
   public enum CJQXChaoJie { JianCai, ZheHui, PingYi, BiaoZhu };//裁减，折回，平抑，标注
   public enum CJQXUnitPosition { RightPos, MidBottomPos, AtRight };//曲线单位的位置：右侧，中下
   public enum TxtArrangeStyle { TxtHorArrange, TxtVerArrange };//文字排列方式：横排；纵排
   public enum Txtdistribution { TxtSpread, Txtfocus };//文字分布方式；分散，集中；
   public enum SymbolFrame { DoubleParallel, NoFrame };//符号外框：双平行线；
   public enum TxtItemOutFrame { DoubleParallel, DouPelAndVerDivide, NoFrame };//双平行线，双平行线和纵分;
   public enum VerDivPos { LeftPos, CenterPos, RightPos };
   public enum TxtItemStyle { NumberStyle, TxtStyle, YsStyle };//数字类型，文字类型，颜色类型
   public enum ThinBZPosStyle { TopPos, RightPos };//超薄标注位置,上侧，右侧;
   public enum LegendPosStyle { TopPos, BottomPos };//图例位置:底部，顶部
   public enum LegendDrawStyle { HaveOutFrame, NoOutFrame };//图例绘制类型:有矩形外框，无矩形外框;
   public enum ItemTitlePos { Left, Mid, Right };//绘图项题目的水平位置；
   public enum LJJSDirection { LeftMid, MidMid, RigtMid, MidTop, MidBottom, LeftTop, LeftBottom, RightTop, RightBottom }
   public enum LineLeftKind { enline, unline, arrowline };//线道左侧线是否存在或者是箭头
   public enum LineLayoutStyle { Horizontal, Vertical };//线道横向排列或是纵向排列;
   public enum LineRoadStyle { JingShenLineRoad, StandardLineRoad, LineRoadGroup, NullLineRoad };//井深线道,标准线道,线道组;
   public enum AttachmentPoint { BottomCenter, BottomRight, BottomLeft, MiddleRight, MiddleLeft, TopLeft, TopRight, TopCenter, MiddleCenter };//文字插入点相对于文字的位置;

    /// <summary>
    /// 枚举:深度给出的方式；
    /// </summary>
   public enum DepthFieldStyle { TopAndBottom, TopAndHeigh, Top, BottomAndHeigh, ErrorFieldStyle };
   public enum SymbolItemSubStyle { Yxwz, Yxpm, Jbqx, Jsjg, Jc, None };

    /// <summary>
    /// 枚举:图例类型，包括符号型、颜色文本型
    /// </summary>
   public enum LegendStyle { SymbolStyle, YSTxtStyle, errStyle };
    /// <summary>
    /// 枚举列出相应的绘图项方案
    /// </summary>
  public  enum DrawItemStyle
    {
        LineItem,//曲线项
        TextItem,//文本项
        SymbolItem,//符号项
        ImageItem,//图片项
        HCGZItem,//带T2截止值得谱图项，核磁共振
        MultiHatchCurveItem,//多级填充曲线项
        HatchRectItem,//直方图填充项
        NormalPuTuItem,//普通谱图项
        CurveHasHatchItem,//带填充的曲线项；
        NoneItem
    };
  public  enum DataProvidePattern
    {
        SQLServerDataBase,
        OracleDataBase,
        DataBase,
        TextFile,
        XMLFile
    };
}
