using System;
using System.Collections.Generic;
using System.Text;

namespace LJJSCAD.Model
{
    class SymbolItemModel
    {
        private string _iD;

        public string ID
        {
            get { return _iD; }
            set { _iD = value; }
        }
        private string _itemName;
        /// <summary>
        /// 绘图项的名字
        /// </summary>
        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }
        private string _ItemField;
        /// <summary>
        /// 绘图项对应的字段
        /// </summary>
        public string ItemField
        {
            get { return _ItemField; }
            set { _ItemField = value; }
        }
        private string _itemTable;
        /// <summary>
        /// 绘图项字段所在表
        /// </summary>
        public string ItemTable
        {
            get { return _itemTable; }
            set { _itemTable = value; }
        }
        private string _syJDTop;
        /// <summary>
        /// 井顶高度对应的字段
        /// </summary>
        public string SyJDTop
        {
            get { return _syJDTop; }
            set { _syJDTop = value; }
        }
        private string _syJDBottom;
        /// <summary>
        /// 井底高度对应的字段
        /// </summary>
        public string SyJDBottom
        {
            get { return _syJDBottom; }
            set { _syJDBottom = value; }
        }
        private string _syJDHeigh;
        /// <summary>
        /// 井段厚度对应的字段
        /// </summary>
        public string SyJDHeigh
        {
            get { return _syJDHeigh; }
            set { _syJDHeigh = value; }
        }
        private string _syHeaderStartheigh;
        /// <summary>
        /// 图头部分起始高度
        /// </summary>
        public string SyHeaderStartheigh
        {
            get { return _syHeaderStartheigh; }
            set { _syHeaderStartheigh = value; }
        }
        private string _syItemTxtVerSpace;
        /// <summary>
        /// 绘图项名字的纵向间距
        /// </summary>
        public string SyItemTxtVerSpace
        {
            get { return _syItemTxtVerSpace; }
            set { _syItemTxtVerSpace = value; }
        }
        private string _syblIfHorFill;
        /// <summary>
        /// 绘图符号是否横向充满线道
        /// </summary>
        public string SyblIfHorFill
        {
            get { return _syblIfHorFill; }
            set { _syblIfHorFill = value; }
        }
        private string _symbolFrame;
        /// <summary>
        /// 绘图外框
        /// </summary>
        public string SymbolFrame
        {
            get { return _symbolFrame; }
            set { _symbolFrame = value; }
        }
        private string _symbolItemColor;
        /// <summary>
        /// 符号项颜色
        /// </summary>
        public string SymbolItemColor
        {
            get { return _symbolItemColor; }
            set { _symbolItemColor = value; }
        }
        private string _syBolOffset;
        /// <summary>
        /// 符号项相对线道起点，平移距离
        /// </summary>
        public string SyBolOffset
        {
            get { return _syBolOffset; }
            set { _syBolOffset = value; }
        }
        private string _symbolOrder;
        /// <summary>
        /// 符号项的排列序号
        /// </summary>
        public string SymbolOrder
        {
            get { return _symbolOrder; }
            set { _symbolOrder = value; }
        }
        private string _symbolItemTitleHorPos;
        /// <summary>
        /// 符号项标题名称水平位置
        /// </summary>
        public string SymbolItemTitleHorPos
        {
            get { return _symbolItemTitleHorPos; }
            set { _symbolItemTitleHorPos = value; }
        }

        private string _symDISubStyle;
        /// <summary>
        /// 绘图项的子类别，例如和某个已存在绘图项绘制方法相同，则与那个绘图项同名；
        /// </summary>
        public string SymDISubStyle
        {
            get { return _symDISubStyle; }
            set { _symDISubStyle = value; }
        }
    }
}
