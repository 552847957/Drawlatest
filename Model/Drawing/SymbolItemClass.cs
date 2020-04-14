using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Util;
using LJJSCAD.CommonData;
using LJJSCAD.DrawingElement;
using LJJSCAD.Drawing.Symbol;
using LJJSCAD.Drawing.Text;
using LJJSCAD.DrawingOper;
using DesignEnum;
namespace LJJSCAD.Model.Drawing
{
    struct SymbolItemDesignStruct
    {
        public string ItemField;

        public string ItemName;
        public string ItemTable;
        public bool SyblIfHorFill ;
        public double SyBolOffset ;
        public double SyHeaderStartheigh ;
        public double SyItemTxtVerSpace ;
        public string SyJDBottom ;
        public string SyJDHeigh ;
        public string SyJDTop ;
        public SymbolFrame SymbolFramePro;
        public int SymbolItemColor ;
        public ItemTitlePos SymbolItemTitleHorPos;
        public int SymbolOrder ;
        public SymbolItemSubStyle SymDISubStyle ;
        public DepthFieldStyle SymDepthFieldStyle;
        public SymbolItemDesignStruct(SymbolItemModel symbolItemModel)
        {
            ItemField = symbolItemModel.ItemField.Trim();
            ItemName = symbolItemModel.ItemName.Trim();
            ItemTable = symbolItemModel.ItemTable.Trim();
            this.SyblIfHorFill = BoolUtil.GetBoolByBindID(symbolItemModel.SyblIfHorFill.Trim(),false);
            this.SyBolOffset =StrUtil.StrToDouble(symbolItemModel.SyBolOffset.Trim(),0,"符号项设置有误");
            this.SyHeaderStartheigh = StrUtil.StrToDouble(symbolItemModel.SyHeaderStartheigh.Trim(), 0, "符号项设置有误");
            this.SyItemTxtVerSpace = StrUtil.StrToDouble(symbolItemModel.SyItemTxtVerSpace.Trim(), 0, "符号项设置有误");
            this.SyJDBottom = symbolItemModel.SyJDBottom.Trim();
            this.SyJDHeigh = symbolItemModel.SyJDHeigh.Trim();
            this.SyJDTop = symbolItemModel.SyJDTop.Trim();
            this.SymbolFramePro = EnumUtil.GetEnumByStr(symbolItemModel.SymbolFrame, SymbolFrame.NoFrame); //SymbolItemClass.GetSymbolFrameStyle(symbolItemModel.SymbolFrame.Trim());
            this.SymbolItemColor = StrUtil.StrToInt(symbolItemModel.SymbolItemColor.Trim(), DrawCommonData.BlackColorRGB, "符号项颜色设置有误");
            this.SymbolItemTitleHorPos = EnumUtil.GetEnumByStr(symbolItemModel.SymbolItemTitleHorPos, ItemTitlePos.Mid);//ItemOper.GetDrawingItemTitlePos(symbolItemModel.SymbolItemTitleHorPos.Trim()); 
            this.SymbolOrder = StrUtil.StrToInt(symbolItemModel.SymbolOrder, 0, "符号项设置有误");
            this.SymDISubStyle = SymbolItemClass.GetSymDISubStyleByStr(symbolItemModel.SymDISubStyle.Trim());
            this.SymDepthFieldStyle = ZuoBiaoOper.GetDepthFieldStyle(symbolItemModel.SyJDTop,symbolItemModel.SyJDBottom,symbolItemModel.SyJDHeigh);
       
 
        }
    }
    /// <summary>
    /// 符号项绘制结构
    /// </summary>
    class SymbolItemStruc
    {
        public double depthtop;
        public double depthbottom;
        public List<string> sybolcodelist;
        public SymbolItemStruc(double depthTop, double depthBottom, List<string> sybolCodeList)
        {
            depthtop = depthTop;
            depthbottom = depthBottom;
            sybolcodelist = sybolCodeList;
        }

    }

    class SymbolItemClass
    {
        //public static SymbolFrame GetSymbolFrameStyle(string frameStr)
        //{
        //    string tmpstr = frameStr.Trim();
        //    SymbolFrame reval = SymbolFrame.NoFrame;
        //    if (tmpstr.Equals("双平行线"))
        //    {
        //        reval = SymbolFrame.DoubleParallel;
        //    }
        //    return reval;
        //}
        public static SymbolItemSubStyle GetSymDISubStyleByStr(string symDISubStyleStr)
        {
            if (string.IsNullOrEmpty(symDISubStyleStr))
                return SymbolItemSubStyle.None;
            string tmp=symDISubStyleStr.ToLower();
            if (tmp.Equals("yxpm"))
                return SymbolItemSubStyle.Yxpm;
            else if (tmp.Equals("jbqx"))
                return SymbolItemSubStyle.Jbqx;
            else if (tmp.Equals("jc"))
                return SymbolItemSubStyle.Jc;
            else if (tmp.Equals("yxwz"))
                return SymbolItemSubStyle.Yxwz;
            else if (tmp.Equals("jsjg"))
                return SymbolItemSubStyle.Jsjg;
            else
                return SymbolItemSubStyle.None;
            
        }
    }
    class SymbolCodeClass
    {
        private string _symbolcode;
        private double _symbolWidth;
        private bool _ifFill;
        private bool _ifZXEnlarge;
        private double _symbolHeigh;
        private string _symbolChineseName;
        private double ystxtheigh = 2.5;
        private LegendStyle _legendstyle;
        private int _legendSequence;//图例的序号;
        private double _legendWidth;//图例框宽度；
        public LegendStyle legendstyle
        {
            set { _legendstyle = value; }
            get { return _legendstyle; }
        }
        public string symbolChineseName
        {
            set { _symbolChineseName = value; }
            get { return _symbolChineseName; }
        }
        public int legendSequence
        {
            set { _legendSequence = value; }
            get { return _legendSequence; }
        }
        public double legendWidth
        {
            set { _legendWidth = value; }
            get { return _legendWidth; }
        }
        public double symbolWidth
        {
            set { _symbolWidth = value; }
            get { return _symbolWidth; }
        }
        public double symbolHeigh
        {
            set { _symbolHeigh = value; }
            get { return _symbolHeigh; }
        }
        public bool ifFill
        {
            set { _ifFill = value; }
            get { return _ifFill; }
        }
        public bool ifZXEnlarge
        {
            set { _ifZXEnlarge = value; }
            get { return _ifZXEnlarge; }
        }
        public string symbolcode
        {
            set { _symbolcode = value; }
            get { return _symbolcode; }
        }

        public void InserLegendSymbol(LJJSPoint ptBase, double legendUnitWith, double legendUnitHeigh)
        {
            double xscale = 1;
            double yscale = 1;
            double xoffset = 0;
            if (_legendstyle == LegendStyle.SymbolStyle)
            {
                if (_symbolWidth > legendUnitWith)
                    xscale = legendUnitWith / _symbolWidth;
                if ((_symbolHeigh > legendUnitHeigh) || (_ifZXEnlarge))
                    yscale = legendUnitHeigh / _symbolHeigh;
                if (_symbolWidth + 0.05 < legendUnitWith)
                    xoffset = (legendUnitWith - _symbolWidth) / 2;
                LJJSPoint inserpos = new LJJSPoint(ptBase.XValue + xoffset, ptBase.YValue);
                SymbolAdd.InsertBlock(_symbolcode, xscale, yscale, inserpos);
            }
            else if (_legendstyle == LegendStyle.YSTxtStyle)
            {
                LJJSText.AddHorCommonText(_symbolcode, new LJJSPoint(ptBase.XValue + legendUnitWith * 0.5, ptBase.YValue), 0,AttachmentPoint.MiddleCenter, ystxtheigh, "宋体");
                   

            }

        }
    }
}
