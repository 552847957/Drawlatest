using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.Model
{
    /// <summary>
    /// 带填充的曲线项
    /// </summary>
    class CurveHasHatchItemModel
    {
        public string ID { get; set; }	//
        public string CurveShowName { get; set; }	//曲线名称
        public string JSField { get; set; }	//井深对应的字段
        public string XFieldName { get; set; }	//x值对应的字段
        public string CurveFromTableName { get; set; }	//来自的表
        public string CurveUnit { get; set; }	//单位
        public string CurveHeaderStartheigh { get; set; }	//曲线头起始高度，实际为第一个刻度尺高度距离线道头底部的高度
        public string CurveShowNameVSKDCHeigh { get; set; }	//刻度尺和显示名之间的距离
        public Boolean KDCIfShow { get; set; }	//是否显示刻度尺
        public string UnitPosition { get; set; }	//单位的位置
        public string ItemOrder { get; set; }	//绘图项序号
        public string ItemTitlePosition { get; set; }	//绘图项显示名的位置
        public string ItemSubStyle { get; set; }	//绘图子项
        public string FillMode { get; set; }	//填充模式
        public string IsDrawBoundary { set; get; }//是否绘制边界
        public string HatchPenColor { set; get; }//边界颜色
        public string HatchPenWidth { set; get; }//边界线粗
        public string FillBKColor { set; get; }//填充的背景色
        public string FillColor { set; get; }//填充的颜色
        public string HatchPattern { set; get; }//填充的图案
        public string HatchScale { set; get; }//填充率
        public string HatchBlk { set; get; }//填充块名
        public string HatchPosition { get; set; }	//填充位置，左填充，或者右填充
    


    }
}
