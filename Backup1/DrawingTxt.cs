using System;
using System.Collections.Generic;
using System.Text;

namespace DrawingModelDBStruc
{
    public class DrawingTxt
    {
        private string _txtItemName;
        /// <summary>
        /// 
        /// </summary>
        public string TxtItemName
        {
            get { return _txtItemName; }
            set { _txtItemName = value; }
        }
        private string _txtItemField;
        /// <summary>
        /// 
        /// </summary>
        public string TxtItemField
        {
            get { return _txtItemField; }
            set { _txtItemField = value; }
        }
        private string _txtLayOut;
        /// <summary>
        /// 文本项的布局,是分散还是集中
        /// </summary>
        public string TxtLayOut
        {
            get { return _txtLayOut; }
            set { _txtLayOut = value; }
        }
        private string _txtSize;
        /// <summary>
        /// 
        /// </summary>
        public string TxtSize
        {
            get { return _txtSize; }
            set { _txtSize = value; }
        }
        private string _txtColor;
        /// <summary>
        /// 
        /// </summary>
        public string TxtColor
        {
            get { return _txtColor; }
            set { _txtColor = value; }
        }
        private string _txtFont;
        /// <summary>
        /// 
        /// </summary>
        public string TxtFont
        {
            get { return _txtFont; }
            set { _txtFont = value; }
        }
        private string _txtJDTop;
        /// <summary>
        /// 井顶高度
        /// </summary>
        public string TxtJDTop
        {
            get { return _txtJDTop; }
            set { _txtJDTop = value; }
        }
        private string _txtJDBottom;
        /// <summary>
        /// 井底高度
        /// </summary>
        public string TxtJDBottom
        {
            get { return _txtJDBottom; }
            set { _txtJDBottom = value; }
        }
        private string _txtJDHeigh;
        /// <summary>
        /// 井段厚度
        /// </summary>
        public string TxtJDHeigh
        {
            get { return _txtJDHeigh; }
            set { _txtJDHeigh = value; }
        }
        private string _txtHeaderStartheigh;
        /// <summary>
        /// 图头部分起始高度
        /// </summary>
        public string TxtHeaderStartheigh
        {
            get { return _txtHeaderStartheigh; }
            set { _txtHeaderStartheigh = value; }
        }
        private string _txtItemTitleSpace;
        /// <summary>
        /// 
        /// </summary>
        public string TxtItemTitleSpace
        {
            get { return _txtItemTitleSpace; }
            set { _txtItemTitleSpace = value; }
        }
        private string _txtPaiLie;
        /// <summary>
        /// 文本项文本的排列方式,横排还是纵排
        /// </summary>
        public string TxtPaiLie
        {
            get { return _txtPaiLie; }
            set { _txtPaiLie = value; }
        }
        private string _txtPosition;
        /// <summary>
        /// 文本项文本的方位,左中右
        /// </summary>
        public string TxtPosition
        {
            get { return _txtPosition; }
            set { _txtPosition = value; }
        }
        private string _textOutFrame;
        /// <summary>
        /// 文本项外框;双平行线;平行纵分
        /// </summary>
        public string TextOutFrame
        {
            get { return _textOutFrame; }
            set { _textOutFrame = value; }
        }
        private string _txtItemFromTbName;
        /// <summary>
        /// 文本项来自的表名
        /// </summary>
        public string TxtItemFromTbName
        {
            get { return _txtItemFromTbName; }
            set { _txtItemFromTbName = value; }
        }
        private string _txtOutFrameWidth;
        /// <summary>
        /// 文本外框宽度
        /// </summary>
        public string TxtOutFrameWidth
        {
            get { return _txtOutFrameWidth; }
            set { _txtOutFrameWidth = value; }
        }
        private string _txtOutFrameVerDivPos;
        /// <summary>
        /// 文本外框纵分线位置
        /// </summary>
        public string TxtOutFrameVerDivPos
        {
            get { return _txtOutFrameVerDivPos; }
            set { _txtOutFrameVerDivPos = value; }
        }
        private string _tIOffset;
        /// <summary>
        /// 文本项相对平移距离(和上一项窜了?  双平行线右偏移值)
        /// </summary>
        public string TIOffset
        {
            get { return _tIOffset; }
            set { _tIOffset = value; }
        }
        private string _txtItemStyle;
        /// <summary>
        /// 文本项类型
        /// </summary>
        public string TxtItemStyle
        {
            get { return _txtItemStyle; }
            set { _txtItemStyle = value; }
        }
        private string _thinBZpos;
        /// <summary>
        /// 超薄标注位置
        /// </summary>
        public string ThinBZpos
        {
            get { return _thinBZpos; }
            set { _thinBZpos = value; }
        }
        private string _txtItemOrder;
        /// <summary>
        /// 文本项的排列序号
        /// </summary>
        public string TxtItemOrder
        {
            get { return _txtItemOrder; }
            set { _txtItemOrder = value; }
        }
        private string _txtItemTitleHorPos;
        /// <summary>
        /// 文本项标题水平位置
        /// </summary>
        public string TxtItemTitleHorPos
        {
            get { return _txtItemTitleHorPos; }
            set { _txtItemTitleHorPos = value; }
        }

        private string _txtDiSubStyle;
        /// <summary>
        /// 绘图项的子类别，例如和某个已存在绘图项绘制方法相同，则与那个绘图项同名；
        /// </summary>
        public string TxtDiSubStyle
        {
            get { return _txtDiSubStyle; }
            set { _txtDiSubStyle = value; }
        }
    }
}
