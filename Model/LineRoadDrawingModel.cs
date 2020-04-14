using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using LJJSCAD.Model;
using LJJSCAD.DrawingElement;

namespace LJJSCAD.LJJSDrawing.Impl
{
    class LineRoadDrawingModel
    {
        //该条线道的起始点坐标
        private LJJSPoint ptStart;
        public LJJSPoint PtStart
        {
            get { return ptStart; }
            set { ptStart = value; }
        }
        //保存了井段信息的list;
        private List<JDStruc> _lineRoadJdLst;
        public List<JDStruc> LineRoadJdLst
        {
            get { return _lineRoadJdLst; }
            set { _lineRoadJdLst = value; }
        }
        //绘制线道的设置；
        private LineRoadDesignClass _lineRoadStruc;
        internal LineRoadDesignClass LineRoadStruc
        {
            get { return _lineRoadStruc; }
            set { _lineRoadStruc = value; }
        }
   


    }
}
