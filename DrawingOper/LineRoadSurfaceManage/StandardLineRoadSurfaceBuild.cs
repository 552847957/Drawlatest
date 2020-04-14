using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Impl;
using LJJSCAD.Drawing.Curve;
using LJJSCAD.DrawingElement;
using LJJSCAD.Util;
using LJJSCAD.DrawingDesign.Frame;

namespace LJJSCAD.DrawingOper.LineRoadSurfaceManage
{
    class StandardLineRoadSurfaceBuild:LineRoadSurface
    {
        public StandardLineRoadSurfaceBuild(LineRoadDrawingModel lineRoadDrawingModel)
            : base(lineRoadDrawingModel)
        { 
        }
        public override void DrawPerJDSecondKD(DrawingElement.LJJSPoint ptstart, double jdHeigh, int DirParam)
        {
            int secondkdcount = (int)Math.Abs(Math.Ceiling(jdHeigh) / _secondKDSpace);
              //  int secondkdcount = Math.Abs((int)(jdHeigh / _secondKDSpace));
                for (int i = 1; i < secondkdcount; i++)
                {
                    Line.BuildHorToRightBlackSolidLine(new LJJSPoint(ptstart.XValue, ptstart.YValue - _secondKDSpace * i), DirParam * _secondKDLineLen, 0, "");

                }
         
        }

        public override void DrawPerJDZMLine(DrawingElement.LJJSPoint ptstart, JDStruc jdStruc)
        {
            LJJSPoint tmpptstart = ptstart;
            double tmplineroadwidth = _lineroadwidth;
 
            if (_lineRoadModel.ZmLineSpace < Math.Abs(jdStruc.JDBottom - jdStruc.JDtop))
            {
                int minZhengMiPtJs = ModeUtil.GetMinBeiShu(jdStruc.JDtop, _lineRoadModel.ZmLineSpace);
                LJJSPoint minZhengMiPt = ZuoBiaoOper.GetJSZuoBiaoPt(tmpptstart, minZhengMiPtJs, jdStruc.JDtop, FrameDesign.ValueCoordinate);
                List<LJJSPoint> dengfenptarr = ZuoBiaoOper.GetZongXiangDengFenPtArr(minZhengMiPt, -1, jdStruc.JDHeight - Math.Abs(minZhengMiPt.YValue - tmpptstart.YValue), _ZMLdengfenspace);
                dengfenptarr.Add(minZhengMiPt);
                foreach (LJJSPoint pt in dengfenptarr)
                {
                    Line.BuildHorToRightBlackSolidLine(pt, _lineRoadModel.LineRoadWidth, _lineRoadModel.ZhengMiLineWidth, "");
                    
                }
            }
        }

        public override void DrawPerJDJSZMLineBiaoZhu(DrawingElement.LJJSPoint biaoZhuPosition, double BiaoZhuContent)
        {
            

        }
    }
}
