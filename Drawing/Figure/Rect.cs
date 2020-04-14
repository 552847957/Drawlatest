using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Geometry;
using LJJSCAD.CommonData;
using LJJSCAD.Util;
using LJJSCAD.DrawingElement;

namespace LJJSCAD.Drawing.Figure
{
    class Rect
    {
        public static ulong AddBlackRect(LJJSPoint insertPoint, double rectHeight, double rectWidth,  double penWidth,DrawDirection drawDirection)
        {
            return VectorDrawHelper.AddRect(DrawCommonData.activeDocument,FigureStrucConvert.ConvertLJJSPointToGPoint(insertPoint), rectHeight, rectWidth, 0, penWidth, drawDirection);
        }
    
    }
}
