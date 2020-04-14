using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.Model
{
    enum HatchRectItemSubStyle { DirectFill,ColorField}//直接填充或者是颜色或者是图案，填充颜色来自字段，需要访问颜色表，根据颜色代码获得具体的颜色值；
    /// <summary>
    /// 直方图填充项,以镜下荧光为例，绘制方法与曲线填充相似。
    /// </summary>
    class HatchRectItemModel
    {        
        public string ID { set; get; }//ID
        public string ItemShowName { set; get; }//绘图项显示名称
        public string ItemFromTable { set; get; }//绘图项X值和y值来自的表
        public string Xfield { set; get; }	//绘图项X值来自的字段

        public string Y_TopField { set; get; }	//绘图项Y方向顶部深度值来自的字段
        public string Y_BottomField { set; get; }//绘图项Y方向底部深度值来自的字段
        public string Color_FromField { set; get; }//绘图颜色来自的字段

        public string ItemUnit { set; get; }//绘图项单位
        public string UnitPosition { set; get; }//单位在图头标题的位置
        public string KDCIfShow { set; get; }//是否显示刻度尺
        public string ItemShowNamePosition { set; get; }//绘图项显示名称的位置
        public string ItemOrder { set; get; }//绘图项的序号
        public string ShowNameVSKDCHeigh { set; get; }//显示名和刻度尺之间的距离
        public string ItemHeaderStartheigh { set; get; }//图头起始高度，即刻度尺起始高度；
    
        public string FillMode { get; set; }//填充模式
        public string IsDrawBoundary { set; get; }//是否绘制边界
        public string HatchPenColor { set; get; }//边界颜色
        public string HatchPenWidth { set; get; }//边界线粗
        public string FillBKColor { set; get; }//填充的背景色
        public string FillColor { set; get; }//填充的颜色
        public string HatchPattern { set; get; }//填充的图案
        public string HatchScale { set; get; }//填充率
        public string HatchBlk { set; get; }//填充块名
        public string ItemSubStyle { set; get; }//子类型
      
    }
}
