using System;
using System.Collections.Generic;
using System.Text;

namespace DrawingModelDBStruc
{
    public class DrawingKedu
    {
        private string _kName;
        /// <summary>
        /// 绘图项刻度尺的名字:绘图项的名字+刻度尺序号
        /// </summary>
        public string KName
        {
            get { return _kName; }
            set { _kName = value; }
        }
        private string _kNum;
        /// <summary>
        /// 刻度尺序号
        /// </summary>
        public string KNum
        {
            get { return _kNum; }
            set { _kNum = value; }
        }
        private string _kStyle;
        /// <summary>
        /// 刻度尺类型:对数、等分、等差
        /// </summary>
        public string KStyle
        {
            get { return _kStyle; }
            set { _kStyle = value; }
        }
        private string _kDir;
        /// <summary>
        /// 刻度尺方向
        /// </summary>
        public string KDir
        {
            get { return _kDir; }
            set { _kDir = value; }
        }
        private string _kSepNum;
        /// <summary>
        /// 刻度尺等分数
        /// </summary>
        public string KSepNum
        {
            get { return _kSepNum; }
            set { _kSepNum = value; }
        }
        private string _kFixedLen;
        /// <summary>
        /// 刻度尺每一份的固定长度
        /// </summary>
        public string KFixedLen
        {
            get { return _kFixedLen; }
            set { _kFixedLen = value; }
        }
        private string _kMin;
        /// <summary>
        /// 刻度尺最小值
        /// </summary>
        public string KMin
        {
            get { return _kMin; }
            set { _kMin = value; }
        }
        private string _kMax;
        /// <summary>
        /// 刻度尺最大值
        /// </summary>
        public string KMax
        {
            get { return _kMax; }
            set { _kMax = value; }
        }
        private string _kUnit;
        /// <summary>
        /// 刻度尺标注单位
        /// </summary>
        public string KUnit
        {
            get { return _kUnit; }
            set { _kUnit = value; }
        }
        private string _kCol;
        /// <summary>
        /// 刻度尺颜色
        /// </summary>
        public string KCol
        {
            get { return _kCol; }
            set { _kCol = value; }
        }
        private string _kParm;
        /// <summary>
        /// 刻度尺对数系数
        /// </summary>
        public string KParm
        {
            get { return _kParm; }
            set { _kParm = value; }
        }
        private string _kDrawItem;
        /// <summary>
        /// 绘图项的名字
        /// </summary>
        public string KDrawItem
        {
            get { return _kDrawItem; }
            set { _kDrawItem = value; }
        }
        private string _kLineStyle;
        /// <summary>
        /// 线型
        /// </summary>
        public string KLineStyle
        {
            get { return _kLineStyle; }
            set { _kLineStyle = value; }
        }
        private string _kIfTwoEndBZ;
        /// <summary>
        /// 两端是否标注
        /// </summary>
        public string KIfTwoEndBZ
        {
            get { return _kIfTwoEndBZ; }
            set { _kIfTwoEndBZ = value; }
        }
        private string _kIfCenterBZ;
        /// <summary>
        /// 中间是否标注
        /// </summary>
        public string KIfCenterBZ
        {
            get { return _kIfCenterBZ; }
            set { _kIfCenterBZ = value; }
        }
        private string _kLineWidth;
        /// <summary>
        /// 曲线线宽
        /// </summary>
        public string KLineWidth
        {
            get { return _kLineWidth; }
            set { _kLineWidth = value; }
        }
    }
}
