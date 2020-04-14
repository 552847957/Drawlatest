using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface;
using LJJSCAD.DataProcess.DrawControlData;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.Drawing.Figure;
using LJJSCAD.CommonData;
using VectorDraw.Geometry;
using LJJSCAD.Drawing.Curve;
using LJJSCAD.DrawingElement;

namespace LJJSCAD.LJJSDrawing.Impl
{
    class FrameDrawImpl:IFrameDraw
    {
        private static double _frameHeight = 0;

        public static double FrameHeight
        {
            get { return FrameDrawImpl._frameHeight; }
          
        }
        private static double _frameWidth = 0;

        public static double FrameWidth
        {
            get { return FrameDrawImpl._frameWidth; }
           
        }

        public List<ulong> DrawRectFrame()
        {
            List<ulong> rectFrameLst = new List<ulong>();
            double frameHeight = FrameControlData.GetMainFrameHeight();
            double frameWidth = FrameControlData.GetMainFrameWidth();
            _frameHeight = frameHeight;
            _frameWidth = frameWidth;
            LJJSPoint startPt = new LJJSPoint(DrawCommonData.xStart,DrawCommonData.yStart);
            DrawDirection ddMainFrame = new DrawDirection();
            ddMainFrame.HorDirection = 1;
            ddMainFrame.VerDirection = -1;

            DrawDirection ddTitleFrame = new DrawDirection();
            ddTitleFrame.HorDirection = 1;
            ddTitleFrame.VerDirection = 1;

            Rect.AddBlackRect(startPt, frameHeight, frameWidth, FrameDesign.PictureFrameLineWidth, ddMainFrame);
            Rect.AddBlackRect(startPt, FrameDesign.LineRoadTitleBarHeigh, frameWidth, FrameDesign.PictureFrameLineWidth, ddTitleFrame);

            if (FrameDesign.JdStrLst.Count() >1&& FrameControlData.LineRoadControlLst.Count() > 0)
            {
               List<JDStruc> firstLineRoadJDStrucLst = FrameControlData.LineRoadControlLst[0].LineRoadJDStructLst;
                //fff
               for (int i = 0; i < firstLineRoadJDStrucLst.Count(); i++) //2560-2590 2660-2690
               {
                   JDStruc tmpJDStruc = firstLineRoadJDStrucLst[i];
                   double xStart=tmpJDStruc.JDPtStart.XValue;
                   double yStart=tmpJDStruc.JDPtStart.YValue+DrawCommonData.DirectionDown*tmpJDStruc.JDHeight;
                   LJJSPoint tmpstartPt = new LJJSPoint(xStart,yStart);
                   Line.BuildHorToRightBlackSolidLine(tmpstartPt, frameWidth, FrameDesign.PictureFrameLineWidth, "");

                   tmpstartPt = new LJJSPoint(xStart,yStart+DrawCommonData.DirectionDown*FrameDesign.JdSpace);
                   Line.BuildHorToRightBlackSolidLine(tmpstartPt, frameWidth, FrameDesign.PictureFrameLineWidth, "");


               }

            }

           return rectFrameLst;
        }
    }
}
