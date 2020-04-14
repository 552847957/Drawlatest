using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.LineRoadInterface;
using LJJSCAD.Model;
using LJJSCAD.DrawingElement;
using LJJSCAD.CommonData;
using LJJSCAD.Drawing.Text;
using System.Drawing;
using LJJSCAD.DrawingDesign.Frame;
using DesignEnum;
using LJJSCAD.BlackBoard;
namespace LJJSCAD.LJJSDrawing.Impl.LineRoad
{
    class JingShenLineRoadBuilderImpl : LineRoadBuilder
    {
        public JingShenLineRoadBuilderImpl(LineRoadDrawingModel lineRoadDrawingModel)
            : base(lineRoadDrawingModel)
        { }

        public override List<ulong> BuildLineRoadHeader()
        {
            return new List<ulong>();
        }


       
        public override List<ulong> BuildLineRoadTitleName()
        {
            string lineRoadName = lineRoadDrawingModel.LineRoadStruc.LineRoadName.Trim();
            double PerWordSpace = 5;
            double xvalue=this.lineRoadDrawingModel.PtStart.XValue + 0.5 * this.lineRoadDrawingModel.LineRoadStruc.LineRoadWidth;
            double yend = this.lineRoadDrawingModel.PtStart.YValue + DrawCommonData.DirectionUp * this.lineRoadDrawingModel.LineRoadStruc.LineroadTitleHeight;
            LJJSPoint titleEndPtStart = new LJJSPoint(xvalue, yend);
            string[] lrnamearr = lineRoadName.Split(perWordSplitter);
            if (null != lrnamearr && lrnamearr.Count() > 0)
            {
                //写字， 与刻度无关
                for (int i = lrnamearr.Count(); i > 0; i--)
                {
                    LJJSText.AddHorCommonText(lrnamearr[i - 1], new LJJSPoint(xvalue, yend), Color.Black.ToArgb(), AttachmentPoint.BottomCenter, TextFontManage.ItemName);                   
                    yend = yend + FrameDesign.PictureItemTxtHeight * 1.6 + PerWordSpace;
                }
            }
            return new List<ulong>();

        }

        public override List<ulong> BuildLineRoadPerDrawItem(DrawItemName drawItemName)
        {
            return null;

        }
    }
}
