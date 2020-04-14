using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Impl;
using DesignEnum;

namespace LJJSCAD.DrawingOper.LineRoadSurfaceManage
{
    class LineRoadSurfaceBuildFactory
    {
        public static LineRoadSurface CreateLRSurfaceInstance(LineRoadDrawingModel lineRoadDrawingModel)
        {
            if (lineRoadDrawingModel.LineRoadStruc.LineRoadStyle.Equals(LineRoadStyle.JingShenLineRoad))
                return new JSLineRoadSurfaceBuild(lineRoadDrawingModel);
            else
                return new StandardLineRoadSurfaceBuild(lineRoadDrawingModel);
        }
    }
}
