using System;
using System.Collections.Generic;
using System.Text;

namespace LJJSCAD.Model
{
    /// <summary>
    /// 绘图框架类，定义绘图一些整体的特性；
    /// </summary>
    class FrameModel
    {
         public string ID{set;get;}
         private  string _pictureHeaderName;
        /// <summary>
        /// 图头大字名称
        /// </summary>
        public  string PictureHeaderName
        {
            get { return _pictureHeaderName; }
            set { _pictureHeaderName = value; }
        }
        private  string _pictureHeaderTXTStyle;
        /// <summary>
        /// 图头大字字体
        /// </summary>
        public  string PictureHeaderTXTStyle
        {
            get { return _pictureHeaderTXTStyle; }
            set { _pictureHeaderTXTStyle = value; }
        }
        private  string _headBigTxtHeight;
        /// <summary>
        /// 图头大字高度
        /// </summary>
        public  string HeadBigTxtHeight
        {
            get { return _headBigTxtHeight; }
            set { _headBigTxtHeight = value; }
        }
        private  string _headBigTxtLayout;
        /// <summary>
        /// 图头大字排列方式
        /// </summary>
        public  string HeadBigTxtLayout
        {
            get { return _headBigTxtLayout; }
            set { _headBigTxtLayout = value; }
        }
        private  string _headBigTxtColor;
        /// <summary>
        /// 图头大字颜色
        /// </summary>
        public  string HeadBigTxtColor
        {
            get { return _headBigTxtColor; }
            set { _headBigTxtColor = value; }
        }
        private  string _pictureItemFont;

        /// <summary>
        /// 绘图项文字字体
        /// </summary>
        public  string PictureItemFont
        {
            get { return _pictureItemFont; }
            set { _pictureItemFont = value; }
        }
        private  string _pictureItemTxtHeight;
        /// <summary>
        /// 绘图项文字高度
        /// </summary>
        public  string PictureItemTxtHeight
        {
            get { return _pictureItemTxtHeight; }
            set { _pictureItemTxtHeight = value; }
        }
        private  string _scaleLabelTxtFont;
        /// <summary>
        /// 刻度尺标注项字体
        /// </summary>
        public  string ScaleLabelTxtFont
        {
            get { return _scaleLabelTxtFont; }
            set { _scaleLabelTxtFont = value; }
        }
        private  string _scaleLabelTxtHeight;
        /// <summary>
        /// 刻度尺标注项文字高度
        /// </summary>
        public  string ScaleLabelTxtHeight
        {
            get { return _scaleLabelTxtHeight; }
            set { _scaleLabelTxtHeight = value; }
        }
        private  string _modelName;
        /// <summary>
        /// 模板名称
        /// </summary>
        public  string ModelName
        {
            get { return _modelName; }
            set { _modelName = value; }
        }
        private  string _pictureHeaderStartX;
        /// <summary>
        /// 图头起始X
        /// </summary>
        public  string PictureHeaderStartX
        {
            get { return _pictureHeaderStartX; }
            set { _pictureHeaderStartX = value; }
        }
        private  string _pictureHeaderStartY;
        /// <summary>
        /// 图头起始Y
        /// </summary>
        public  string PictureHeaderStartY
        {
            get { return _pictureHeaderStartY; }
            set { _pictureHeaderStartY = value; }
        }
        private  string _scaleValue;
        /// <summary>
        /// 比例尺的值
        /// </summary>
        public  string ScaleValue
        {
            get { return _scaleValue; }
            set { _scaleValue = value; }
        }
        private  string _scaleValueFont;
        /// <summary>
        /// 比例尺文字的字体
        /// </summary>
        public  string ScaleValueFont
        {
            get { return _scaleValueFont; }
            set { _scaleValueFont = value; }
        }
        private  string _scaleValueHeight;
        /// <summary>
        /// 比例尺文字的高度
        /// </summary>
        public  string ScaleValueHeight
        {
            get { return _scaleValueHeight; }
            set { _scaleValueHeight = value; }
        }
        private  string _scaleValueColor;
        /// <summary>
        /// 比例尺文字的颜色
        /// </summary>
        public  string ScaleValueColor
        {
            get { return _scaleValueColor; }
            set { _scaleValueColor = value; }
        }
        private  string _ifDefaultModel;
        /// <summary>
        /// 是不是默认的模板，0为否，1为是
        /// </summary>
        public  string IfDefaultModel
        {
            get { return _ifDefaultModel; }
            set { _ifDefaultModel = value; }
        }
        private  string _jDSpace;
        /// <summary>
        /// 井段距离
        /// </summary>
        public  string JDSpace
        {
            get { return _jDSpace; }
            set { _jDSpace = value; }
        }
        private  string _pictureFrameLineWidth;
        /// <summary>
        /// 解释图外框线宽
        /// </summary>
        public  string PictureFrameLineWidth
        {
            get { return _pictureFrameLineWidth; }
            set { _pictureFrameLineWidth = value; }
        }
        private  string _lineRoadTitleBarHeigh;
        /// <summary>
        /// 线道标题栏高度
        /// </summary>
        public  string LineRoadTitleBarHeigh
        {
            get { return _lineRoadTitleBarHeigh; }
            set { _lineRoadTitleBarHeigh = value; }
        }
        private  string _legendPos;
        /// <summary>
        /// 图例位置:上\下
        /// </summary>
        public  string LegendPos
        {
            get { return _legendPos; }
            set { _legendPos = value; }
        }
        private  string _legendStyle;
        /// <summary>
        /// 图例类型,有框\无框
        /// </summary>
        public  string LegendStyle
        {
            get { return _legendStyle; }
            set { _legendStyle = value; }
        }

        private  string _legendColumnNum;
        /// <summary>
        /// 图例列数,行数依靠图例的总数和列数进行计算。
        /// </summary>
        public  string LegendColumnNum
        {
            get { return _legendColumnNum; }
            set { _legendColumnNum = value; }
        }
        private  string _legendUnitHeigh;
        /// <summary>
        /// 图例高度
        /// </summary>
        public  string LegendUnitHeigh
        {
            get { return _legendUnitHeigh; }
            set { _legendUnitHeigh = value; }
        }
        private  string _legendTbAndField;
        /// <summary>
        /// 图例来自的字段和表;格式:表名:字段名;
        /// </summary>
        public  string LegendTbAndField
        {
            get { return _legendTbAndField; }
            set { _legendTbAndField = value; }
        }
        private  string _ifAddLegend;

        public  string IfAddLegend
        {
            get { return _ifAddLegend; }
            set { _ifAddLegend = value; }
        }
    }          
}

