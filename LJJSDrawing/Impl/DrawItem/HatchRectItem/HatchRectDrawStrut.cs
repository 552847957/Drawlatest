using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using LJJSCAD.Drawing.Hatch;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem.HatchRectItem
{
    class HatchRectDrawStrut
    {
        public List<LJJSPoint> FillArea { set; get; }
        public LJJSHatch LjjsHatch { set; get; }
        public HatchRectDrawStrut(List<LJJSPoint> fillArea, LJJSHatch ljjsHatch)
        {
            this.FillArea = fillArea;
            this.LjjsHatch = ljjsHatch; 
        }
    }
}
