using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.LineRoadInterface;

namespace LJJSCAD.LJJSDrawing.Impl.LineRoad
{
    class LineRoadBuildDirector
    {
        private LineRoadBuilder lineRoadBuilder;
        public LineRoadBuildDirector(LineRoadBuilder lineRoadBuilder)
        {
            this.lineRoadBuilder = lineRoadBuilder;
        }
        public void BuildLineRoad()
        {
            this.lineRoadBuilder.InitLineRoadDrawingModel();
            this.lineRoadBuilder.BuildLineRoadSurface();
            this.lineRoadBuilder.BuildLineRoadDrawItem();
            this.lineRoadBuilder.BuildLineRoadHeaderLeftLine();
            this.lineRoadBuilder.BuildLineRoadHeader();
            this.lineRoadBuilder.BuildLineRoadTitleName();

        }
    }
}
