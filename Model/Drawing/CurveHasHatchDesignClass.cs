using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.CommonData;
using LJJSCAD.LJJSDrawing.Impl.DrawItem;
using LJJSCAD.Util;
using LJJSCAD.DrawingOper;
using LJJSCAD.Drawing.Hatch;
using DesignEnum;

namespace LJJSCAD.Model.Drawing
{
    class CurveHasHatchDesignClass:ItemDesignClass
    {       
       
        public string JSField { get; set; }	//井深对应的字段
        public string XFieldName { get; set; }	//x值对应的字段
        public string CurveFromTableName { get; set; }	//来自的表
        public string CurveUnit { get; set; }	//单位
        public double CurveHeaderStartheigh { get; set; }	//曲线头起始高度，实际为第一个刻度尺高度距离线道头底部的高度
        public double CurveShowNameVSKDCHeigh { get; set; }	//刻度尺和显示名之间的距离
        public bool KDCIfShow { get; set; }	//是否显示刻度尺
        public CJQXUnitPosition UnitPosition { get; set; }	//单位的位置
        public LJJSHatch ItemHatch { get; set; }	//填充设计
        public MyCurveHatchPos HatchPosition { get; set; }	//填充位置，左填充，或者右填充
      
        public CurveHasHatchDesignClass(CurveHasHatchItemModel curveHatchModel)
        {
            ItemConstructor(curveHatchModel.ID, curveHatchModel.CurveShowName, curveHatchModel.ItemOrder, curveHatchModel.ItemSubStyle, curveHatchModel.ItemTitlePosition);

            this.JSField = StandardCurveItemBuilder.GetJSField(curveHatchModel.JSField);
            this.KDCIfShow = curveHatchModel.KDCIfShow;
            this.XFieldName = curveHatchModel.XFieldName.Trim();
            this.CurveFromTableName = curveHatchModel.CurveFromTableName.Trim();
            this.CurveUnit = curveHatchModel.CurveUnit.Trim();
            this.CurveHeaderStartheigh = StrUtil.StrToDouble(curveHatchModel.CurveHeaderStartheigh, 3, "绘图项起始为非数值型");
            this.CurveShowNameVSKDCHeigh = StrUtil.StrToDouble(curveHatchModel.CurveShowNameVSKDCHeigh, 10, "绘图项题目高度设计为非数值型");
           
            this.UnitPosition = EnumUtil.GetEnumByStr(curveHatchModel.UnitPosition, CJQXUnitPosition.RightPos);
            this.ItemHatch = new LJJSHatch(curveHatchModel.FillMode
                , curveHatchModel.IsDrawBoundary, curveHatchModel.HatchPenColor
                , curveHatchModel.HatchPenWidth, curveHatchModel.FillBKColor
                ,curveHatchModel.FillColor, curveHatchModel.HatchPattern
                , curveHatchModel.HatchScale, curveHatchModel.HatchBlk, "");//不需要图片填充；

            this.HatchPosition = EnumUtil.GetEnumByStr(curveHatchModel.HatchPosition, MyCurveHatchPos.Left);
       
 
        }

    }
}
