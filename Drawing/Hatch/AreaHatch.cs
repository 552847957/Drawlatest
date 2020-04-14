using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using VectorDraw.Geometry;
using VectorDraw.Professional.vdObjects;
using LJJSCAD.Util;
using VectorDraw.Professional.Constants;

namespace LJJSCAD.Drawing.Hatch
{

    class AreaHatch
    {
        public static ulong AddStandardAreaHatch(vdDocument activeDocument,List<LJJSPoint> hatchPtLst, int areaLineColor,int areaHatchColor)
        {
            return AddAreaHatch(activeDocument, hatchPtLst, areaLineColor, areaHatchColor,VdConstSplineFlag.SFlagSTANDARD);
        }
        public static ulong AddQUADRATICAreaHatch(vdDocument activeDocument, List<LJJSPoint> hatchPtLst, int areaLineColor, int areaHatchColor)
        {
            return AddAreaHatch(activeDocument, hatchPtLst, areaLineColor, areaHatchColor, VdConstSplineFlag.SFlagQUADRATIC);
        }
        public static ulong AddFITTINGAreaHatch(vdDocument activeDocument, List<LJJSPoint> hatchPtLst, int areaLineColor, int areaHatchColor)
        {
            return AddAreaHatch(activeDocument, hatchPtLst, areaLineColor, areaHatchColor, VdConstSplineFlag.SFlagFITTING);
        }
        public static ulong AddCONTROLPOINTSAreaHatch(vdDocument activeDocument, List<LJJSPoint> hatchPtLst, int areaLineColor, int areaHatchColor)
        {
            return AddAreaHatch(activeDocument, hatchPtLst, areaLineColor, areaHatchColor, VdConstSplineFlag.SFlagCONTROLPOINTS);
        }

        private static ulong AddAreaHatch(vdDocument activeDocument, List<LJJSPoint> hatchPtLst, int areaLineColor, int areaHatchColor,VdConstSplineFlag splineFlag)
        {
            Vertexes hatcharea = new Vertexes();
            if (null != hatchPtLst && hatchPtLst.Count() > 0)
            {
                for (int i = 0; i < hatchPtLst.Count(); i++)
                {
                    gPoint gpt = new gPoint(hatchPtLst[i].XValue, hatchPtLst[i].YValue);
                    hatcharea.Add(gpt);
                }
                return VectorDrawHelper.AddHatchToFigure(activeDocument, hatcharea, areaLineColor, areaHatchColor, splineFlag);
            }
            return 0;
        }
    
    }
}
