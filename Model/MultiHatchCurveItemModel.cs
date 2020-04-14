using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.Model
{
    /// <summary>
    /// 多级填充折线项，例如定量荧光的含油浓度，油性指数绘图项,镜下荧光；
    /// </summary>
    class MultiHatchCurveItemModel
    {
        public string ID { set; get; }	//ID
        public string ItemShowName { set; get; }	//绘图项显示名称
        public string ItemFromTable { set; get; }	//绘图项X值和y值来自的表
        public string Yfield { set; get; }	//绘图项y值来自的字段
        public string Xfield { set; get; }	//绘图项X值来自的字段
        public string FenJieValueOne { set; get; }	//分界值1，为较小的分界值，一共两个分界值，将曲线分为三个部分进行填充
        public string FenJieValueTwo { set; get; }	//分界值2
        public string ItemUnit { set; get; }	//绘图项单位
        public string UnitPosition { set; get; }	//单位在图头标题的位置
        public string KDCIfShow { set; get; }	//是否显示刻度尺
        public string ItemShowNamePosition { set; get; }	//绘图项显示名称的位置
        public string ItemOrder { set; get; }	//绘图项的序号
        public string ShowNameVSKDCHeigh { set; get; }	//显示名和刻度尺之间的距离
        public string ItemHeaderStartheigh { set; get; }	//图头起始高度，即刻度尺起始高度；
        public string HatchColorOne { set;get;}//一级填充颜色
        public string HatchColorTwo { set; get; }//二级填充颜色
        public string HatchColorThree { set; get; }//三级填充颜色
        public string HatchCount { set; get; }//填充的级数，取3以内；

    }
}
