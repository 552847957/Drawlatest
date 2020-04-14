using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.LineRoadInterface;
using LJJSCAD.Model;
using LJJSCAD.LJJSDrawing.Factory;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.DrawingElement;
using LJJSCAD.CommonData;
using LJJSCAD.Drawing.Text;
using LJJSCAD.DrawingDesign.Frame;
using System.Drawing;
using DesignEnum;
using LJJSCAD.BlackBoard;
namespace LJJSCAD.LJJSDrawing.Impl.LineRoad
{
    class StandardLineRoadBuilderImpl : LineRoadBuilder
    {
        public StandardLineRoadBuilderImpl(LineRoadDrawingModel lineRoadDrawingModel)
            : base(lineRoadDrawingModel)
        { }

        public StandardLineRoadBuilderImpl()
        {
            // TODO: Complete member initialization
        }

  

        public override List<ulong> BuildLineRoadHeader()
        {

            return new List<ulong>();
        }

        public override List<ulong> BuildLineRoadTitleName()
        {


            string lineRoadName = lineRoadDrawingModel.LineRoadStruc.LineRoadName;
            if (string.IsNullOrEmpty(lineRoadName))
                return new List<ulong>();
            lineRoadName = lineRoadName.Trim();

            double PerWordSpace = 5;
            double xvalue = this.lineRoadDrawingModel.PtStart.XValue + 0.5 * this.lineRoadDrawingModel.LineRoadStruc.LineRoadWidth;
            double yend = this.lineRoadDrawingModel.PtStart.YValue + DrawCommonData.DirectionUp * this.lineRoadDrawingModel.LineRoadStruc.LineroadTitleHeight;
            LJJSPoint titleEndPtStart = new LJJSPoint(xvalue, yend);
            string[] lrnamearr = lineRoadName.Split(perWordSplitter);
            if (null != lrnamearr && lrnamearr.Count() > 0)
            {
                for (int i = lrnamearr.Count(); i > 0; i--)
                {
                    LJJSText.AddHorCommonText(lrnamearr[i - 1], new LJJSPoint(xvalue, yend), Color.Black.ToArgb(), AttachmentPoint.BottomCenter, TextFontManage.LineRoadName);

                    yend = yend + FrameDesign.PictureItemTxtHeight * 1.6 + PerWordSpace;
                }
            }

            return new List<ulong>();
        }

        public override List<ulong> BuildLineRoadPerDrawItem(DrawItemName drawItemName)
        {
            LineRoadEnvironment lre=new LineRoadEnvironment();
            lre.JdDrawLst=lineRoadDrawingModel.LineRoadJdLst;
            lre.LineRoadWidth=lineRoadDrawingModel.LineRoadStruc.LineRoadWidth;
            DrawItemDirectorFactory drawItemDirectorFactory = new DrawItemDirectorFactory();
            DrawItemBuildManage diBuilderDir = (DrawItemBuildManage)drawItemDirectorFactory.ReturnItemInstance(drawItemName.ItemStyle);
         
            diBuilderDir.DrawItemBuild(drawItemName, lre);
            return new List<ulong>();
        }
    }
}
