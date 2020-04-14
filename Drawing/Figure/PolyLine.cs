using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Util;
using LJJSCAD.DrawingElement;
using VectorDraw.Geometry;
using LJJSCAD.CommonData;
using System.Drawing;

namespace LJJSCAD.Drawing.Figure
{
    class PolyLine
    {
        public static ulong BuildCommonPolyLine(List<LJJSPoint> ptcol, double penWidth, Color PenColor)
        { 
            List<gPoint> ljjsgPtLst=FigureStrucConvert.ConverLJJSPtLstTogPtLst(ptcol);
            return VectorDrawHelper.CommonPolyLine(DrawCommonData.activeDocument, ljjsgPtLst, penWidth, PenColor);
        }
    }
}
