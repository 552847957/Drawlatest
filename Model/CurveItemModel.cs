using System;
using System.Collections.Generic;
using System.Text;

namespace LJJSCAD.Model
{
    public class CurveItemModel
    {
        private string _iD;

        public string ID
        {
            get { return _iD; }
            set { _iD = value; }
        }
        private string _cJQXShowName;
        /// <summary>
        /// 中文名字
        /// </summary>
        public string CJQXShowName
        {
            get { return _cJQXShowName; }
            set { _cJQXShowName = value; }
        }
        private string _jsField;
        /// <summary>
        /// 井深对应的字段
        /// </summary>
        public string JSField
        {
            get { return _jsField; }
            set { _jsField = value; }
        }
        private string _cJQXFieldName;
        /// <summary>
        /// 字段的名称
        /// </summary>
        public string CJQXFieldName
        {
            get { return _cJQXFieldName; }
            set { _cJQXFieldName = value; }
        }
        private string _cJQXlineClass;
        /// <summary>
        /// 测井曲线类型:包括连线,离散点,连线和离散点,棒图。
        /// </summary>
        public string CJQXlineClass
        {
            get { return _cJQXlineClass; }
            set { _cJQXlineClass = value; }
        }
        private string _cJQXFromTableName;
        /// <summary>
        /// 测井曲线相关位置等属性来自的表名
        /// </summary>
        public string CJQXFromTableName
        {
            get { return _cJQXFromTableName; }
            set { _cJQXFromTableName = value; }
        }
        private string _cJQXUnit;
        /// <summary>
        /// 测井曲线单位
        /// </summary>
        public string CJQXUnit
        {
            get { return _cJQXUnit; }
            set { _cJQXUnit = value; }
        }
        private string _cJQXChaoJie;
        /// <summary>
        /// 超界
        /// </summary>
        public string CJQXChaoJie
        {
            get { return _cJQXChaoJie; }
            set { _cJQXChaoJie = value; }
        }
        private string _cJQXHeaderStartheigh;
        /// <summary>
        /// 图头部分起始高度
        /// </summary>
        public string CJQXHeaderStartheigh
        {
            get { return _cJQXHeaderStartheigh; }
            set { _cJQXHeaderStartheigh = value; }
        }
        private string _qXNameVSKDCHeigh;
        /// <summary>
        /// 曲线名称距比例尺高度
        /// </summary>
        public string QXNameVSKDCHeigh
        {
            get { return _qXNameVSKDCHeigh; }
            set { _qXNameVSKDCHeigh = value; }
        }
        private string _kDCIfShow;
        /// <summary>
        /// 是否显示刻度尺
        /// </summary>
        public string KDCIfShow
        {
            get { return _kDCIfShow; }
            set { _kDCIfShow = value; }
        }
        private string _unitPosition;
        /// <summary>
        /// 单位排列位置:右;中下
        /// </summary>
        public string UnitPosition
        {
            get { return _unitPosition; }
            set { _unitPosition = value; }
        }
        private string _lineItemOrder;
        /// <summary>
        /// 排列序号
        /// </summary>
        public string LineItemOrder
        {
            get { return _lineItemOrder; }
            set { _lineItemOrder = value; }
        }
        private string _qXItemTitlePosition;
        /// <summary>
        /// 测井曲线项标题得位置:左＼中＼右
        /// </summary>
        public string QXItemTitlePosition
        {
            get { return _qXItemTitlePosition; }
            set { _qXItemTitlePosition = value; }
        }
        private string _liDISubStyle;
        /// <summary>
        /// 绘图项的子类别，例如和某个已存在绘图项绘制方法相同，则与那个绘图项同名；
        /// </summary>
        public string LIDISubStyle
        {
            get { return _liDISubStyle; }
            set { _liDISubStyle = value; }
        }

    }
}
