using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Util;
using LJJSCAD.Drawing.Hatch;
using DesignEnum;

namespace LJJSCAD.Model.Drawing
{
    class HatchRectDesignClass:ItemDesignClass
    {      
       
        public string ItemFromTable { set; get; }	//绘图项X值和y值来自的表
        public string Xfield { set; get; }	//绘图项X值来自的字段
        public string Y_TopField { set; get; }	//绘图项Y方向顶部深度值来自的字段
        public string Y_BottomField { set; get; }	//绘图项Y方向底部深度值来自的字段
        public string ItemUnit { set; get; }	//绘图项单位
        public string Color_FromFiled { set; get; }//填充颜色来自的字段；
        public CJQXUnitPosition UnitPosition { set; get; }	//单位在图头标题的位置
        public bool KDCIfShow { set; get; }	//是否显示刻度尺
        public double ShowNameVSKDCHeigh { set; get; }	//显示名和刻度尺之间的距离
        public double ItemHeaderStartheigh { set; get; }	//图头起始高度，即刻度尺起始高度；
        public HatchRectItemSubStyle hrItemSubStyle { set; get; }
     
        public LJJSHatch ItemHatch { get; set; }//填充设计

        public HatchRectDesignClass(HatchRectItemModel hatchRectItemModel)
        {
            this.ItemConstructor(hatchRectItemModel.ID, hatchRectItemModel.ItemShowName
                , hatchRectItemModel.ItemOrder, hatchRectItemModel.ItemSubStyle
                , hatchRectItemModel.ItemShowNamePosition);
            this.Color_FromFiled = StrUtil.GetNoNullStr(hatchRectItemModel.Color_FromField);
            this.ItemFromTable = StrUtil.GetNoNullStr(hatchRectItemModel.ItemFromTable);
            this.Xfield = StrUtil.GetNoNullStr(hatchRectItemModel.Xfield);
            this.Y_TopField = StrUtil.GetNoNullStr(hatchRectItemModel.Y_TopField);
            this.Y_BottomField = StrUtil.GetNoNullStr(hatchRectItemModel.Y_BottomField);
            this.ItemUnit = StrUtil.GetNoNullStr(hatchRectItemModel.ItemUnit);
            this.UnitPosition = EnumUtil.GetEnumByStr(hatchRectItemModel.UnitPosition, CJQXUnitPosition.RightPos);
            this.KDCIfShow = BoolUtil.GetBoolByBindID(hatchRectItemModel.KDCIfShow, true);
            this.ShowNameVSKDCHeigh = StrUtil.StrToDouble(hatchRectItemModel.ShowNameVSKDCHeigh, 5, "起始位置设计有误，为非数值型");
            this.ItemHeaderStartheigh = StrUtil.StrToDouble(hatchRectItemModel.ItemHeaderStartheigh,3,"起始位置非数值型");
            ItemHatch = new LJJSHatch(hatchRectItemModel.FillMode
                , hatchRectItemModel.IsDrawBoundary
                , hatchRectItemModel.HatchPenColor
                , hatchRectItemModel.HatchPenWidth
                , hatchRectItemModel.FillBKColor
                , hatchRectItemModel.FillColor
                , hatchRectItemModel.HatchPattern
                , hatchRectItemModel.HatchScale
                , hatchRectItemModel.HatchBlk, "");
            hrItemSubStyle = EnumUtil.GetEnumByStr(this.ItemSubStyle, HatchRectItemSubStyle.DirectFill);

        }
    }
}
