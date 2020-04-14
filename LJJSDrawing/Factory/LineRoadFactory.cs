using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.LineRoadInterface;
using LJJSCAD.LJJSDrawing.Impl.LineRoad;
using LJJSCAD.LJJSDrawing.Impl;
using DesignEnum;

namespace LJJSCAD.LJJSDrawing.Factory
{
    class LineRoadFactory
    {
        public static LineRoadBuilder CreateLineRoadInstance(LineRoadStyle lineRoadStyle,LineRoadDrawingModel lineRoadDrawingModel )
        {
            //画线道在这里，包括画井深，普通的，还有刻度
            if (lineRoadStyle.Equals(LineRoadStyle.StandardLineRoad))
                return new StandardLineRoadBuilderImpl(lineRoadDrawingModel);
            else if (lineRoadStyle.Equals(LineRoadStyle.JingShenLineRoad))
                return new JingShenLineRoadBuilderImpl(lineRoadDrawingModel);
            else
                return new StandardLineRoadBuilderImpl(lineRoadDrawingModel);
        }


    }
}
