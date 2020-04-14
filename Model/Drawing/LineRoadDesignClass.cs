using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using LJJSCAD.Util;
using LJJSCAD.ItemStyleOper;
using DesignEnum;
using EnumManage;

namespace LJJSCAD.Model
{
    class LineRoadDesignClass
    {
        private char itemCollectionSplitter = ';';
        private LineRoadStyle _lineRoadStyle;

        public LineRoadStyle LineRoadStyle
        {
            get { return _lineRoadStyle; }
            set { _lineRoadStyle = value; }
        }
        private double JSTxtVSTop = 8;
        private string _lineRoadId;

        public string LineRoadId
        {
            get { return _lineRoadId; }
            set { _lineRoadId = value; }
        }

        private double _lineroadwidth;
        /// <summary>
        /// 线道宽度@
        /// </summary>
        public double LineRoadWidth
        {
            get { return _lineroadwidth; }
            set { _lineroadwidth = value; }
        }

        //测网线的线型;
        private string _cwLineType = "DOTX2";
        public string CwLineType
        {
            get { return _cwLineType; }
            set { _cwLineType = value; }
        }
        private bool _ifZhengMiLine;
        /// <summary>
        /// 是否有整米线
        /// </summary>
        public bool IfzhengMiLine
        {
            get { return _ifZhengMiLine; }
            set { _ifZhengMiLine = value; }
        }
        //整米线的线粗；
        private double _zhengMiLineWidth = 0.2;

        public double ZhengMiLineWidth
        {
            get { return _zhengMiLineWidth; }
            set { _zhengMiLineWidth = value; }
        }
        //整米线距离,单位为米,即相隔多少米画一条整米线，为计算值，求在绘图时50mm代表实际井深中多少m；
        private int _zmLineSpace;
        public int ZmLineSpace
        {
            get { return _zmLineSpace; }
            set { _zmLineSpace = value; }
        }

        private bool _ifLeftSecondKD;
        /// <summary>
        /// 左侧次刻度@
        /// </summary>
        public bool IfLeftSecondKD
        {
            get { return _ifLeftSecondKD; }
            set { _ifLeftSecondKD = value; }
        }
        private bool _ifRightSecondKD;
        /// <summary>
        /// 右侧次刻度@
        /// </summary>
        public bool IfRightSecondKD
        {
            get { return _ifRightSecondKD; }
            set { _ifRightSecondKD = value; }
        }

        //次刻度线的颜色
        private int _secondKDLineColor = 8;
        public int SecondKDLineColor
        {
            get { return _secondKDLineColor; }
            set { _secondKDLineColor = value; }
        }
        //次刻度线的长度
        private double _secondKDLineLen = 2;
        public double SecondKDLineLen
        {
            get { return _secondKDLineLen; }
            set { _secondKDLineLen = value; }
        }
        //次刻度的间距；
        private double _secondKDSpace = 1;

        public double SecondKDSpace
        {
            get { return _secondKDSpace; }
            set { _secondKDSpace = value; }
        }
        //是否绘制题头左侧线
        private bool _titleLeftFrameLineChecked;
        public bool TitleLeftFrameLineChecked
        {
            get { return _titleLeftFrameLineChecked; }
            set { _titleLeftFrameLineChecked = value; }
        }
        //绘制线道体框架左侧线类型；
        private LineLeftKind _leftLineStyle;
        internal LineLeftKind LeftLineStyle
        {
            get { return _leftLineStyle; }
            set { _leftLineStyle = value; }
        }
        //如果为箭头线的话画出箭头线的长度
        private double _leftLineLength;
        public double LeftLineLength
        {
            get { return _leftLineLength; }
            set { _leftLineLength = value; }
        }
        private double _HorLRTitleHeigh = 10;
        /// <summary>
        ///线道组设计，横排线道时的线道头高度
        /// </summary>
        public double HorLRTitleHeigh
        {
            get { return _HorLRTitleHeigh; }
            set { _HorLRTitleHeigh = value; }
        }
       public string _lineroadname;
        /// <summary>
        /// 线组名称@
        /// </summary>
        public string LineRoadName
        {
            get { return _lineroadname; }
            set { _lineroadname = value; }
        }
        private List<DrawItemName> _drawingitems;
        /// <summary>
        /// 绘制线道包含的绘图项，泛型类型（DrawItemName）;@
        /// </summary>
        public List<DrawItemName> Drawingitems
        {
            get { return _drawingitems; }
            set { _drawingitems = value; }
        }

        /// <summary>
        /// 线道标题高度，线道组设计时使用；
        /// </summary>
        private double _lineroadTitleHeight;
        public double LineroadTitleHeight
        {
            get { return _lineroadTitleHeight; }
            set { _lineroadTitleHeight = value; }
        }

        //线道测网设置；
        private LineRoadCeWang _cewang = new LineRoadCeWang();
        /// <summary>
        /// 属性：线道层网设置@
        /// </summary>
        public LineRoadCeWang Cewang
        {
            get { return _cewang; }
            set { _cewang = value; }
        }
        public LineRoadDesignClass(LineRoadModel lineRoadModel)
        {
            this.LineRoadId = lineRoadModel.LineRoadDesignDetailID.Trim();
            this.LineRoadName = lineRoadModel.LineRoadName;
            this.LineRoadStyle = GetLineRoadStyleByStr(lineRoadModel.LineRoadStyle);
            this.LineRoadWidth = StrUtil.StrToDouble(lineRoadModel.LineRoadWidth, 40, LineRoadName + "线道宽度为非数值型");
            this.LineroadTitleHeight = StrUtil.StrToDouble(lineRoadModel.LineroadTitleHeight, 20, "线道标题高度为非数值型");
            this.IfLeftSecondKD = lineRoadModel.IfLeftSecondKD;
            this.IfRightSecondKD = lineRoadModel.IfRightSecondKD;
          
            this.IfzhengMiLine = lineRoadModel.IfzhengMiLine;
            this.TitleLeftFrameLineChecked = lineRoadModel.TitleLeftFrameLineChecked;

            this.LeftLineStyle = GetLineLeftKindByStr(lineRoadModel.LeftLineStyle);
            this.LeftLineLength = StrUtil.StrToDouble(lineRoadModel.LeftLineLength, 40, "线道左侧箭头线长度为非数值型");


            //绘图项设置；
            List<DrawItemName> drawItemNames = new List<DrawItemName>();
            AddDrawItems(ref drawItemNames, lineRoadModel.CurveItemCollections, DrawItemStyle.LineItem);
            AddDrawItems(ref drawItemNames, lineRoadModel.TextItemCollections, DrawItemStyle.TextItem);
            AddDrawItems(ref drawItemNames, lineRoadModel.SymbolItemCollections, DrawItemStyle.SymbolItem);
            AddDrawItems(ref drawItemNames, lineRoadModel.ImageItemCollections, DrawItemStyle.ImageItem);
            AddDrawItems(ref drawItemNames, lineRoadModel.HcgzItemCollections, DrawItemStyle.HCGZItem);
            AddDrawItems(ref drawItemNames, lineRoadModel.CurveHasHatchItemCollections, DrawItemStyle.CurveHasHatchItem);
            AddDrawItems(ref drawItemNames, lineRoadModel.MultiHatchCurveItemCollections, DrawItemStyle.MultiHatchCurveItem);
            AddDrawItems(ref drawItemNames, lineRoadModel.NormalPuTuItemCollections, DrawItemStyle.NormalPuTuItem);
            AddDrawItems(ref drawItemNames, lineRoadModel.HatchRectItemCollections, DrawItemStyle.HatchRectItem);

            this.Drawingitems = drawItemNames;

            //测网设置；
            bool ifAddCeWang = lineRoadModel.ifAddCeWang;
            LineRoadCeWang lineroadcw = new LineRoadCeWang(ifAddCeWang);
            if (ifAddCeWang)
            {
                lineroadcw.ifHeng =lineRoadModel.IfCeWangHeng;
                lineroadcw.ifZong = lineRoadModel.IfCeWangZong;
                lineroadcw.cewangfixlen = StrUtil.StrToDouble(lineRoadModel.cewangfixlen, 0, "测网固定间距为非数值型");
                lineroadcw.cewangsepnum = StrUtil.StrToInt(lineRoadModel.cewangsepnum, 0, "测网分数为非数值型");
                if (null != lineRoadModel.cewangstyle)
                {
                    lineroadcw.cewangstyle =EnumUtil.GetEnumByStr(lineRoadModel.cewangstyle.Trim(),CeWangStyleEnum.None);
                }
                lineroadcw.duishuminvalue = StrUtil.StrToDouble(lineRoadModel.duishuminvalue, 1, "对数最小值为非数值型");
                lineroadcw.duishuParam = StrUtil.StrToDouble(lineRoadModel.duishuParam, 35, "对数系数为非数值型");
            }
            this._cewang = lineroadcw;
        }
        private void AddDrawItems(ref List<DrawItemName> drawItems, string itemCollectionsStr, DrawItemStyle drawItemStyle)
        {
            
            if (string.IsNullOrEmpty(itemCollectionsStr))
                return;
            else
            {
                string tmp = itemCollectionsStr.Trim();
                string[] items = tmp.Split(itemCollectionSplitter);
                if (items.Count() > 0)
                {
                    for (int i = 0; i < items.Count(); i++)
                    {
                        string tmpitemId = items[i];
                        if (!string.IsNullOrEmpty(tmpitemId))
                        {
                            DrawItemNameRead drawItemNameRead = new DrawItemNameRead(tmpitemId);
                            drawItems.Add((DrawItemName)drawItemNameRead.ReturnItemInstance(drawItemStyle));
                        }
                    }
                }
            }

        }
        private LineLeftKind GetLineLeftKindByStr(string lineLeftKind)
        {
            if (!string.IsNullOrEmpty(lineLeftKind))
            {
                string tmp = lineLeftKind.Trim().ToLower();
                if (tmp.Equals("unline"))
                    return LineLeftKind.unline;
                else if (tmp.Equals("arrowline"))
                    return LineLeftKind.arrowline;
            }
            return LineLeftKind.enline;
        }
        private LineRoadStyle GetLineRoadStyleByStr(string lineRoadStyleStr)
        {
            if (!string.IsNullOrEmpty(lineRoadStyleStr))
            {
               
                if (StrUtil.StrCompareIngoreCase(lineRoadStyleStr,LineRoadStyle.JingShenLineRoad.ToString()))
                    return LineRoadStyle.JingShenLineRoad;
                else if (StrUtil.StrCompareIngoreCase(lineRoadStyleStr,LineRoadStyle.LineRoadGroup.ToString()))
                    return LineRoadStyle.LineRoadGroup;

            }
            return LineRoadStyle.StandardLineRoad;


        }

    }
}
