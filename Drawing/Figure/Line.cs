using System;
using System.Collections.Generic;
using System.Text;
using VectorDraw.Geometry;
using VectorDraw.Professional.vdObjects;
using LJJSCAD.CommonData;
using LJJSCAD.Util;
using LJJSCAD.DrawingElement;

namespace LJJSCAD.Drawing.Curve
{
    class Line
    {
        public static ulong BuildCommonSoldLine(LJJSPoint ptStart, LJJSPoint ptEnd, int lineColor, double penWidth)
        {
            gPoint pStart = FigureStrucConvert.ConvertLJJSPointToGPoint(ptStart);
            gPoint pEnd = FigureStrucConvert.ConvertLJJSPointToGPoint(ptEnd);
            return VectorDrawHelper.CommonLine(DrawCommonData.activeDocument, pStart, pEnd, penWidth, lineColor, DrawCommonData.SolidLineTypeName, "");

        }
        public static ulong BuildCommonLineByLayer(LJJSPoint ptStart, LJJSPoint ptEnd, double penWidth)
        {
            gPoint pStart = FigureStrucConvert.ConvertLJJSPointToGPoint(ptStart);
            gPoint pEnd = FigureStrucConvert.ConvertLJJSPointToGPoint(ptEnd);
            return VectorDrawHelper.CommonLineByLayer(DrawCommonData.activeDocument, pStart, pEnd, penWidth, "","");
        }
        public static ulong BuildCommonHorLineByLayer(LJJSPoint pStart, double lineWidth, double penWidth,int horDirection)
        {
            gPoint pEnd = new gPoint(pStart.XValue +horDirection* lineWidth, pStart.YValue);
            gPoint pstart = new gPoint(pStart.XValue, pStart.YValue);

            return VectorDrawHelper.CommonLineByLayer(DrawCommonData.activeDocument, pstart, pEnd, penWidth, "","");
        }

        public static ulong BuildCommonHorSolidLineByLayer(LJJSPoint pStart, double lineWidth, double penWidth, int horDirection)
        {
            gPoint pEnd = new gPoint(pStart.XValue + horDirection * lineWidth, pStart.YValue);
            gPoint pstart = new gPoint(pStart.XValue, pStart.YValue);

            return VectorDrawHelper.CommonLineByLayer(DrawCommonData.activeDocument, pstart, pEnd, penWidth, "",DrawCommonData.SolidLineTypeName);
        }


       

        public static ulong BuildHorToRightSolidLine(LJJSPoint pStart, double lineWidth, double penWidth, int pColor, string pToolTip)
        {
            gPoint pEnd=new gPoint(pStart.XValue+lineWidth,pStart.YValue);
            gPoint pstart = new gPoint(pStart.XValue,pStart.YValue);
            return VectorDrawHelper.CommonLine(DrawCommonData.activeDocument, pstart, pEnd, penWidth, pColor, DrawCommonData.SolidLineTypeName,pToolTip);
        }

        public static ulong BuildHorToRightBlackSolidLine(LJJSPoint pStart, double lineWidth, double penWidth, string pToolTip)
        {
            gPoint pEnd = new gPoint(pStart.XValue + lineWidth, pStart.YValue);
            return VectorDrawHelper.CommonLine(DrawCommonData.activeDocument, FigureStrucConvert.ConvertLJJSPointToGPoint(pStart), pEnd, penWidth, DrawCommonData.BlackColorRGB, DrawCommonData.SolidLineTypeName, pToolTip);
        }
        /// <summary>
        /// 根据起始点，添加垂直线;
        /// </summary>
        /// <param name="ptStart">起始点</param>
        /// <param name="lineLen">线长</param>
        /// <param name="penWidth">线粗</param>
        /// <param name="pToolTip">提示语</param>
        /// <returns>线的ID</returns>
        public static ulong BuildVerLine(LJJSPoint ptStart, double lineLen, double penWidth,int lineColor,string lineTypeName, string pToolTip,int direction)
        {
            gPoint ptend = new gPoint(ptStart.XValue, ptStart.YValue + direction*lineLen);
            return VectorDrawHelper.CommonLine(DrawCommonData.activeDocument, FigureStrucConvert.ConvertLJJSPointToGPoint(ptStart), ptend, penWidth, lineColor, lineTypeName,pToolTip);

          
        }
        public static ulong BuildToRightHorLine(LJJSPoint ptStart, double lineLen, double penWidth, int lineColor, string lineTypeName, string pToolTip)
        {
            gPoint ptend = new gPoint(ptStart.XValue + lineLen, ptStart.YValue);
            return VectorDrawHelper.CommonLine(DrawCommonData.activeDocument, FigureStrucConvert.ConvertLJJSPointToGPoint(ptStart), ptend, penWidth, lineColor, lineTypeName, pToolTip);
        }
  
    }
}
