using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Model;
using LJJSCAD.LJJSDrawing.Impl;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.DrawingOper;
using LJJSCAD.Drawing.Curve;
using LJJSCAD.DrawingElement;
using System.Drawing;
using LJJSCAD.CommonData;
using LJJSCAD.DrawingOper.LineRoadSurfaceManage;

namespace LJJSCAD.LJJSDrawing.Interface.LineRoadInterface
{
   abstract class LineRoadBuilder
    {
      protected LineRoadDrawingModel lineRoadDrawingModel;
      protected static char perWordSplitter='_';
      public LineRoadBuilder(LineRoadDrawingModel lineRoadDrawingModel)
      {
          this.lineRoadDrawingModel = lineRoadDrawingModel;
      }

      public LineRoadBuilder()
      {
          // TODO: Complete member initialization
      }
      public ulong BuildLineRoadHeaderLeftLine()
      {
          if (lineRoadDrawingModel.LineRoadStruc.TitleLeftFrameLineChecked)
          {

              LJJSPoint ptstart = lineRoadDrawingModel.PtStart;
              //
              return Line.BuildVerLine(ptstart, FrameDesign.LineRoadTitleBarHeigh, FrameDesign.PictureFrameLineWidth, Color.Black.ToArgb(), DrawCommonData.SolidLineTypeName, "", DrawCommonData.DirectionUp);
          }
          else
              return 0;
          
      }
      public void InitLineRoadDrawingModel()
      {
          lineRoadDrawingModel.LineRoadStruc.ZmLineSpace = (int)((LineRoadSurface._ZMLdengfenspace* FrameDesign.YCoordinate) / (FrameDesign.XCoordinate * 1000));//整米线及单位的赋值
      }
      public void BuildLineRoadSurface()
      {

          LineRoadSurface lineRoadSurface =LineRoadSurfaceBuildFactory.CreateLRSurfaceInstance(lineRoadDrawingModel); //2560-2590 2660-2690
          lineRoadSurface.Build();      
      }
      public void BuildLineRoadDrawItem()
      {
          if (null != lineRoadDrawingModel.LineRoadStruc.Drawingitems && lineRoadDrawingModel.LineRoadStruc.Drawingitems.Count() > 0)
          {
              for (int i = 0; i < lineRoadDrawingModel.LineRoadStruc.Drawingitems.Count(); i++)
              {
                  BuildLineRoadPerDrawItem(lineRoadDrawingModel.LineRoadStruc.Drawingitems[i]);
              }
          }
      }
      public abstract List<ulong> BuildLineRoadPerDrawItem(DrawItemName drawItemName);
      public abstract List<ulong> BuildLineRoadHeader();
      public abstract List<ulong> BuildLineRoadTitleName();

    }
}
