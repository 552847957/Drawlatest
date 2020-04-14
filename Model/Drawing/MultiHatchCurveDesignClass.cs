using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Drawing.Hatch;
using LJJSCAD.Util;
using LJJSCAD.LJJSDrawing.Impl.DrawItem;
using DesignEnum;

namespace LJJSCAD.Model.Drawing
{
    class MultiHatchCurveDesignClass:ItemDesignClass
    {
       
        public string ItemFromTable { set; get; }	//绘图项X值和y值来自的表
        public string Yfield { set; get; }	//绘图项y值来自的字段
        public string Xfield { set; get; }	//绘图项X值来自的字段
        public double FenJieValueOne { set; get; }	//分界值1，为较小的分界值，一共两个分界值，将曲线分为三个部分进行填充
        public double FenJieValueTwo { set; get; }	//分界值2
        public string ItemUnit { set; get; }	//绘图项单位
        public CJQXUnitPosition UnitPosition { set; get; }	//单位在图头标题的位置
        public bool KDCIfShow { set; get; }	//是否显示刻度尺
        public double ShowNameVSKDCHeigh { set; get; }	//显示名和刻度尺之间的距离
        public double ItemHeaderStartheigh { set; get; }	//图头起始高度，即刻度尺起始高度；
        public int HatchCount { set; get; }//填充级数，取3以内的数
        public LJJSHatch HatchOne { set; get; }//一级填充颜色，全面的填充，如果有1,2级则露出最远的部分；
        public LJJSHatch HatchTwo { set; get; }//二级填充颜色，中间的填充
        public LJJSHatch HatchThree { set; get; }//三级填充颜色，最靠线道一侧的填充
        public MultiHatchCurveDesignClass(MultiHatchCurveItemModel mHatchItem)
        {
            this.ItemConstructor(mHatchItem.ID
                , mHatchItem.ItemShowName, mHatchItem.ItemOrder
                , "", mHatchItem.ItemShowNamePosition);
            this.ItemFromTable = StrUtil.GetNoNullStr(mHatchItem.ItemFromTable);
            this.Xfield = StrUtil.GetNoNullStr(mHatchItem.Xfield);
            this.Yfield = StandardCurveItemBuilder.GetJSField(mHatchItem.Yfield);
            this.FenJieValueOne = StrUtil.StrToDouble(mHatchItem.FenJieValueOne,-1,"分界值1为非数值型");
            this.FenJieValueTwo = StrUtil.StrToDouble(mHatchItem.FenJieValueTwo, -1, "分界值2为非数值型");
            this.ItemUnit = StrUtil.GetNoNullStr(mHatchItem.ItemUnit);
            this.UnitPosition = EnumUtil.GetEnumByStr(mHatchItem.UnitPosition, CJQXUnitPosition.MidBottomPos);
            this.KDCIfShow = BoolUtil.GetBoolByBindID(mHatchItem.KDCIfShow, true);
            this.ShowNameVSKDCHeigh = StrUtil.StrToDouble(mHatchItem.ShowNameVSKDCHeigh,5,"绘图项题目与刻度尺距离非数值型");
            this.ItemHeaderStartheigh = StrUtil.StrToDouble(mHatchItem.ItemHeaderStartheigh,3,"非数值型");
            this.HatchCount = StrUtil.StrToInt(mHatchItem.HatchCount,1,"填充级数非数值型");
            this.HatchOne = new LJJSHatch(mHatchItem.HatchColorOne, bool.FalseString);
            this.HatchTwo = new LJJSHatch(mHatchItem.HatchColorTwo,bool.FalseString);
            this.HatchThree = new LJJSHatch(mHatchItem.HatchColorThree, bool.FalseString);

                

        }
    }
}
