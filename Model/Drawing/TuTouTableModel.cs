using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using LJJSCAD.Util;
using VectorDraw.Geometry;

namespace LJJSCAD.Model.Drawing
{
   public class TuTouTableModel
    {
        public string path { set; get; }
        public bool isSelectInsertPt { set; get; }
        public object insertPoint { set; get; }
        public double xScale { set; get; }
        public double yScale { set; get; }
        public TuTouTableModel(string filePath, string xInserPt, string yInserPt, string xScaleStr, string yScaleStr,bool isSelectInsertPt)
        {
            this.path = filePath.Trim();
            double xinserPt=StrUtil.StrToDouble(xInserPt,0,"x为非数值型");
            double yinserPt=StrUtil.StrToDouble(yInserPt,0,"y为非数值型");
            if (isSelectInsertPt)
                this.insertPoint = "user";
            else
            {
                gPoint pt = new gPoint(xinserPt, yinserPt);
                this.insertPoint = pt;
            }
            this.xScale = StrUtil.StrToDouble(xScaleStr, 0, "xscal为非数值型");
            this.yScale = StrUtil.StrToDouble(yScaleStr, 0, "yScaleStr为非数值型");
            this.isSelectInsertPt = isSelectInsertPt;



        }

    }
}
