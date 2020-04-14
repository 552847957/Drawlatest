using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Model;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.Util;
using LJJSCAD.DrawingOper;

namespace LJJSCAD.BlackBoard.LJJSDesignData.Impl
{
    class FrameDesignManage
    {
       
        public static void SetFrameDesginByFrameModel(FrameModel frameModel)
        {
            //比例尺；
            FrameDesign.CorTxtColor = frameModel.ScaleValueColor;
            FrameDesign.CorTxtFont = frameModel.ScaleValueFont;
            FrameDesign.CorTxtHeit =StrUtil.StrToDouble(frameModel.ScaleValueHeight,8,"比例尺文本高度为非数值型");
            FrameDesign.CorValue = frameModel.ScaleValue;

            FrameDesign.XCoordinate =StrUtil.StrToDouble( BiLiChiOper.GetXValueStr(frameModel.ScaleValue),"比例尺X值为空","比例尺X值为非数值型");
            FrameDesign.YCoordinate = StrUtil.StrToDouble(BiLiChiOper.GetYValueStr(frameModel.ScaleValue), "比例尺X值为空", "比例尺X值为非数值型");



            //图例设计
            FrameDesign.LegendPos =frameModel.LegendPos;//图例设计位置
            FrameDesign.LegendStyle = frameModel.LegendStyle;//图例设计类型
            FrameDesign.LegendColumnNum = StrUtil.StrToInt(frameModel.LegendColumnNum,5,"");//图例设计列数
            FrameDesign.LegendUnitHeigh = StrUtil.StrToDouble(frameModel.LegendUnitHeigh,10,"图例设计单位高度为非数值型");//图例设计单位高度
            FrameDesign.LegendTbAndField = frameModel.LegendTbAndField;//获得图例查询所涉及的表和字段；
            FrameDesign.IfAddLegend = BoolUtil.GetBoolByBindID(frameModel.IfAddLegend,true);
             //   BoolUtil.GetBoolBySFStr(frameModel.IfAddLegend,true);



            //绘图项文字；
            if (!string.IsNullOrEmpty(frameModel.PictureItemFont))
                FrameDesign.PictureItemFont = frameModel.PictureItemFont;
            if (!string.IsNullOrEmpty(frameModel.PictureItemTxtHeight))
                FrameDesign.PictureItemTxtHeight = StrUtil.StrToDouble(frameModel.PictureItemTxtHeight,4,"绘图项文字的高度为非数值型");
        


            //图幅设计
            FrameDesign.JdSpace = StrUtil.StrToDouble(frameModel.JDSpace, 20, "井段间距为非数值型");
            FrameDesign.PictureFrameLineWidth = StrUtil.StrToDouble(frameModel.PictureFrameLineWidth, "缺少外框线宽", "外框线宽为非数值型");//外框线宽
            FrameDesign.LineRoadTitleBarHeigh = StrUtil.StrToDouble(frameModel.LineRoadTitleBarHeigh, "缺少线道标题栏高度", "线道标题栏高度为非数值型");//线道标题栏高度

            //刻度尺
            if (!string.IsNullOrEmpty(frameModel.ScaleLabelTxtFont))
                FrameDesign.ScaleLabelTxtFont= frameModel.ScaleLabelTxtFont;
            if (!string.IsNullOrEmpty(frameModel.ScaleLabelTxtHeight))
                FrameDesign.ScaleLabelTxtHeight = frameModel.ScaleLabelTxtHeight;
        
            //图头文字
            FrameDesign.HeaderContent=frameModel.PictureHeaderName;
            FrameDesign.PictureHeaderTXTStyle=frameModel.PictureHeaderTXTStyle;
            FrameDesign.HeadBigTxtHeight=StrUtil.StrToDouble(frameModel.HeadBigTxtHeight,18,"图头字高为非数值型");
            FrameDesign.HeaderTxtColor=frameModel.HeadBigTxtColor;

             
        }
    }
}
