using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Geometry;
using LJJSCAD.Util;
using VectorDraw.Professional.vdObjects;

namespace LJJSCAD.Drawing.Hatch
{
    class MyHatch
    {
        public static void AddAreaHatch(vdDocument activeDocument, List<LJJSPoint> fillArea, LJJSHatch ljjsHatch)
        {
            Vertexes vts = new Vertexes();
            if (null != fillArea && fillArea.Count>2)
            {
                for (int i = 0; i < fillArea.Count; i++)
                {
                    vts.Add(new gPoint(fillArea[i].XValue, fillArea[i].YValue));
                }
                VectorDrawHatchHelper.AddAreaHatch(activeDocument, ljjsHatch, vts);
            }
           
 
        }
        public static void AddPolyHatch(vdDocument activeDocument,List<List<LJJSPoint>> hatchBoundary,LJJSHatch ljjsHatch)
        {
            vdCurves curves = new vdCurves();
            List<vdCurves> curvelst = new List<vdCurves>();
            for(int i=0;i<hatchBoundary.Count;i++)
            {
                
               
                List<LJJSPoint> tmpLst = hatchBoundary[i];
                VectorDraw.Professional.vdFigures.vdPolyline tmppoly = new VectorDraw.Professional.vdFigures.vdPolyline();
                for (int j = 0; j < tmpLst.Count; j++)
                {                   
                    tmppoly.VertexList.Add(new gPoint(tmpLst[j].XValue,tmpLst[j].YValue));
                }


                curves.AddItem(tmppoly);
            }
            curvelst.Add(curves);
            if (curves.Count > 0)
                VectorDrawHatchHelper.AddPolyHatch(activeDocument, ljjsHatch, curvelst);
        }
    }
}
