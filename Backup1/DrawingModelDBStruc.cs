using System;
using System.Collections.Generic;
using System.Text;

namespace DrawingModelDBStruc
{
    public class DrawingFrame
    {

        private static string _pictureHeaderName;
        /// <summary>
        /// 图头大字名称
        /// </summary>
        public static string PictureHeaderName
        {
            get { return DrawingFrame._pictureHeaderName; }
            set { DrawingFrame._pictureHeaderName = value; }
        }
        private static string _pictureHeaderTXTStyle;
        /// <summary>
        /// 图头大字字体
        /// </summary>
        public static string PictureHeaderTXTStyle
        {
            get { return DrawingFrame._pictureHeaderTXTStyle; }
            set { DrawingFrame._pictureHeaderTXTStyle = value; }
        }
        private static string _headBigTxtHeight;
        /// <summary>
        /// 图头大字高度
        /// </summary>
        public static string HeadBigTxtHeight
        {
            get { return DrawingFrame._headBigTxtHeight; }
            set { DrawingFrame._headBigTxtHeight = value; }
        }
        private static string _headBigTxtLayout;
        /// <summary>
        /// 图头大字排列方式
        /// </summary>
        public static string HeadBigTxtLayout
        {
            get { return DrawingFrame._headBigTxtLayout; }
            set { DrawingFrame._headBigTxtLayout = value; }
        }
        private static string _headBigTxtColor;
        /// <summary>
        /// 图头大字颜色
        /// </summary>
        public static string HeadBigTxtColor
        {
            get { return DrawingFrame._headBigTxtColor; }
            set { DrawingFrame._headBigTxtColor = value; }
        }
        private static string _pictureItemFont;

        /// <summary>
        /// 绘图项文字字体
        /// </summary>
        public static string PictureItemFont
        {
            get { return DrawingFrame._pictureItemFont; }
            set { DrawingFrame._pictureItemFont = value; }
        }
        private static string _pictureItemTxtHeight;
        /// <summary>
        /// 绘图项文字高度
        /// </summary>
        public static string PictureItemTxtHeight
        {
            get { return DrawingFrame._pictureItemTxtHeight; }
            set { DrawingFrame._pictureItemTxtHeight = value; }
        }
        private static string _scaleLabelTxtFont;
        /// <summary>
        /// 刻度尺标注项字体
        /// </summary>
        public static string ScaleLabelTxtFont
        {
            get { return DrawingFrame._scaleLabelTxtFont; }
            set { DrawingFrame._scaleLabelTxtFont = value; }
        }
        private static string _scaleLabelTxtHeight;
        /// <summary>
        /// 刻度尺标注项文字高度
        /// </summary>
        public static string ScaleLabelTxtHeight
        {
            get { return DrawingFrame._scaleLabelTxtHeight; }
            set { DrawingFrame._scaleLabelTxtHeight = value; }
        }
        private static string _modelName;
        /// <summary>
        /// 模板名称
        /// </summary>
        public static string ModelName
        {
            get { return DrawingFrame._modelName; }
            set { DrawingFrame._modelName = value; }
        }
        private static string _pictureHeaderStartX;
        /// <summary>
        /// 图头起始X
        /// </summary>
        public static string PictureHeaderStartX
        {
            get { return DrawingFrame._pictureHeaderStartX; }
            set { DrawingFrame._pictureHeaderStartX = value; }
        }
        private static string _pictureHeaderStartY;
        /// <summary>
        /// 图头起始Y
        /// </summary>
        public static string PictureHeaderStartY
        {
            get { return DrawingFrame._pictureHeaderStartY; }
            set { DrawingFrame._pictureHeaderStartY = value; }
        }
        private static string _scaleValue;
        /// <summary>
        /// 比例尺的值
        /// </summary>
        public static string ScaleValue
        {
            get { return DrawingFrame._scaleValue; }
            set { DrawingFrame._scaleValue = value; }
        }
        private static string _scaleValueFont;
        /// <summary>
        /// 比例尺文字的字体
        /// </summary>
        public static string ScaleValueFont
        {
            get { return DrawingFrame._scaleValueFont; }
            set { DrawingFrame._scaleValueFont = value; }
        }
        private static string _scaleValueHeight;
        /// <summary>
        /// 比例尺文字的高度
        /// </summary>
        public static string ScaleValueHeight
        {
            get { return DrawingFrame._scaleValueHeight; }
            set { DrawingFrame._scaleValueHeight = value; }
        }
        private static string _scaleValueColor;
        /// <summary>
        /// 比例尺文字的颜色
        /// </summary>
        public static string ScaleValueColor
        {
            get { return DrawingFrame._scaleValueColor; }
            set { DrawingFrame._scaleValueColor = value; }
        }
        private static string _ifDefaultModel;
        /// <summary>
        /// 是不是默认的模板，0为否，1为是
        /// </summary>
        public static string IfDefaultModel
        {
            get { return DrawingFrame._ifDefaultModel; }
            set { DrawingFrame._ifDefaultModel = value; }
        }
        private static string _jDSpace;
        /// <summary>
        /// 井段距离
        /// </summary>
        public static string JDSpace
        {
            get { return DrawingFrame._jDSpace; }
            set { DrawingFrame._jDSpace = value; }
        }
        private static string _pictureFrameLineWidth;
        /// <summary>
        /// 解释图外框线宽
        /// </summary>
        public static string PictureFrameLineWidth
        {
            get { return DrawingFrame._pictureFrameLineWidth; }
            set { DrawingFrame._pictureFrameLineWidth = value; }
        }
        private static string _lineRoadTitleBarHeigh;
        /// <summary>
        /// 线道标题栏高度
        /// </summary>
        public static string LineRoadTitleBarHeigh
        {
            get { return DrawingFrame._lineRoadTitleBarHeigh; }
            set { DrawingFrame._lineRoadTitleBarHeigh = value; }
        }
        private static string _legendPos;
        /// <summary>
        /// 图例位置:上\下
        /// </summary>
        public static string LegendPos
        {
            get { return DrawingFrame._legendPos; }
            set { DrawingFrame._legendPos = value; }
        }
        private static string _legendStyle;
        /// <summary>
        /// 图例类型,有框\无框
        /// </summary>
        public static string LegendStyle
        {
            get { return DrawingFrame._legendStyle; }
            set { DrawingFrame._legendStyle = value; }
        }
        private static string _legendRowNum;
        /// <summary>
        /// 图例行数
        /// </summary>
        public static string LegendRowNum
        {
            get { return DrawingFrame._legendRowNum; }
            set { DrawingFrame._legendRowNum = value; }
        }
        private static string _legendColumnNum;
        /// <summary>
        /// 图例列数
        /// </summary>
        public static string LegendColumnNum
        {
            get { return DrawingFrame._legendColumnNum; }
            set { DrawingFrame._legendColumnNum = value; }
        }
        private static string _legendUnitHeigh;
        /// <summary>
        /// 图例高度
        /// </summary>
        public static string LegendUnitHeigh
        {
            get { return DrawingFrame._legendUnitHeigh; }
            set { DrawingFrame._legendUnitHeigh = value; }
        }
        private static string _legendTbAndField;
        /// <summary>
        /// 图例来自的字段和表;格式:表名:字段名;
        /// </summary>
        public static string LegendTbAndField
        {
            get { return DrawingFrame._legendTbAndField; }
            set { DrawingFrame._legendTbAndField = value; }
        }
        private static string _ifAddLegend;

        public static string IfAddLegend
        {
            get { return DrawingFrame._ifAddLegend; }
            set { DrawingFrame._ifAddLegend = value; }
        }
    }             

 
    public class LineItem
    {
    }
    public class TextItem
    {
    }
    public class SymbolItem
    {
    }
}
