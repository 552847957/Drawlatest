using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.LJJSDrawing.Interface
{
   abstract class LJJSBuilder
    {
       public abstract List<ulong> BuildFrame();//绘制外框
       public abstract List<ulong> BuildLineRoadArea();//绘制线道
       public abstract ulong BuildBiLiChi();//绘制比例尺;
       public abstract ulong BuildHeader();//绘制图头文字;
       public abstract List<ulong> BuildLegendArea();//绘制图例;

    }
}
