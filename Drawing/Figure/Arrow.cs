using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using VectorDraw.Geometry;
using LJJSCAD.Util;
using LJJSCAD.Drawing.Curve;
using LJJSCAD.CommonData;
using System.Drawing;

namespace LJJSCAD.Drawing.Figure
{
    class Arrow
    {
        public static void VerSolidArrow(LJJSPoint basePoint, double width, double height,int direction)
        {
            Vertexes controlPtCollection = new Vertexes();          
            gPoint pt2 = new gPoint(basePoint.XValue + width, basePoint.YValue);
            gPoint pt3 = new gPoint(basePoint.XValue, basePoint.YValue +direction*height);
            gPoint pt4 = new gPoint(basePoint.XValue - width, basePoint.YValue);
            controlPtCollection.Add(FigureStrucConvert.ConvertLJJSPointToGPoint(basePoint));
            controlPtCollection.Add(pt2);
            controlPtCollection.Add(pt3);
            controlPtCollection.Add(pt4);
            //
            VectorDrawHelper.BuildSpineWithSolidHatch(DrawCommonData.activeDocument, controlPtCollection, VectorDraw.Professional.Constants.VdConstPlineFlag.PlFlagCLOSE, VectorDraw.Professional.Constants.VdConstSplineFlag.SFlagSTANDARD, Color.Black.ToArgb(), "");


        }
        public static void VerSolidArrowLine(LJJSPoint pt1, double length, double pThickness,int direction,int Color,double arrowWidth,double arrowHeight)
        {
            LJJSPoint pt2 = new LJJSPoint(pt1.XValue, pt1.YValue+direction*length);
            Line.BuildVerLine(pt1, length, pThickness, Color, DrawCommonData.SolidLineTypeName, "", direction);
            VerSolidArrow(pt2, arrowWidth, arrowHeight, direction);
            
        }
    }
}
