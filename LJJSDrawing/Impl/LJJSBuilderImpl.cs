using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface;
using LJJSCAD.DataProcess.DrawControlData;
using LJJSCAD.Model;
using LJJSCAD.LJJSDrawing.Interface.LineRoadInterface;
using LJJSCAD.LJJSDrawing.Impl.LineRoad;
using LJJSCAD.DrawingOper;
using LJJSCAD.LJJSDrawing.Factory;
using LJJSCAD.DrawingElement;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.CommonData;
using LJJSCAD.Util;
using LJJSCAD.LJJSDrawing.Impl.Legend;
using LJJSCAD.Drawing.Text;
using System.Windows.Forms;
using LJJSCAD.BlackBoard;
using DesignEnum;
namespace LJJSCAD.LJJSDrawing.Impl
{
    class LJJSBuilderImpl : LJJSBuilder
    {
        //顶部图例的起点坐标;
        LJJSPoint legendEndPt=null;
        public override List<ulong> BuildFrame()
        {
            IFrameDraw iFrameDraw = new FrameDrawImpl();
            return iFrameDraw.DrawRectFrame();//绘制录井解释图的外框架;
        }

        public override List<ulong> BuildLineRoadArea()
        {

            //1,从黑板取出线道的设计数据;
           List<LineRoadDesignClass> lineRoadModelLst=LineRoadDesign.LineRoadDesginLst;
           List<LineRoadControlData> lineRoadControlData=FrameControlData.LineRoadControlLst; //2560-2590 2660-2690
           //2,绘制线道区
           if (lineRoadModelLst.Count() != lineRoadControlData.Count())
               return null;
           for (int i = 0; i < lineRoadModelLst.Count(); i++)
           {
               LineRoadDesignClass tmplineRoadModel = lineRoadModelLst[i];
               LineRoadControlData tmpLineRoadControlData = lineRoadControlData[i];  //2560-2590 2660-2690
               LineRoadBuilder lineRoadBuilder = LineRoadFactory.CreateLineRoadInstance(tmplineRoadModel.LineRoadStyle, LineRoadOper.BuildLineRoadDrawingModel(tmplineRoadModel, tmpLineRoadControlData));
               LineRoadBuildDirector lineRoadDirector = new LineRoadBuildDirector(lineRoadBuilder); //
               lineRoadDirector.BuildLineRoad();

           }
               return new List<ulong>();
        }

        public override ulong BuildBiLiChi()
        {
            LJJSPoint pt = new LJJSPoint(FrameDrawImpl.FrameWidth * 0.5, 5 + legendEndPt.YValue);//解析该绘图点的起始位置,距离高度仅仅为5
            int headercolor =StrUtil.StrToInt(FrameDesign.CorTxtColor,DrawCommonData.BlackColorRGB,"比例尺颜色设计有误");
            return LJJSText.AddHorCommonText(FrameDesign.CorValue, pt, headercolor, AttachmentPoint.BottomCenter,TextFontManage.BiLiChi);

        }

        public override ulong BuildHeader()
        {
         //   double lrh = FrameDesign.LineRoadTitleBarHeigh;//获取
            LJJSPoint headerposition = new LJJSPoint(FrameDrawImpl.FrameWidth * 0.5, 60 + legendEndPt.YValue);//图头起始点坐标
            string jhname = FrameDesign.JH.Trim();//临时变量,获取井号
            if (jhname != "")
            {
                int headercolor = StrUtil.StrToInt(FrameDesign.HeaderTxtColor, DrawCommonData.BlackColorRGB, "图幅标题颜色设计有误");
               return  LJJSText.AddHorCommonText(jhname+FrameDesign.HeaderContent, headerposition, headercolor, AttachmentPoint.BottomCenter, TextFontManage.MainTitle);
               
            }
            return 0;
 
        }
        //绘制图例；
        public override List<ulong> BuildLegendArea()
        {
            AddLegendArea addLegend = new AddLegendArea();
            legendEndPt= addLegend.LegendAreaBuild();
            return null;
     
        }
    }
}
