using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using System.Collections;
using LJJSCAD.Model.Drawing;
using LJJSCAD.Drawing.Text;
using LJJSCAD.CommonData;
using LJJSCAD.DrawingOper;
using LJJSCAD.BlackBoard.LJJSDesignData.Impl;
using LJJSCAD.Drawing.Figure;
using DesignEnum;

namespace LJJSCAD.LJJSDrawing.Impl.Legend
{
    class LegendDrawing
    {
        private LJJSPoint _ptstart;
        private List<string> _legendlist;
        private int _legendcolumn;//图例列数
        private int legendrownum;//图例行数
        private double _legendunitheigh = 10;
        private double noframetxtvslegspace = 1;//对于无框图例，图例名字和图例符号之间的横向间距
        private double noframelegtxtwidth = 20;//对于无框图例，图例名字的宽度；
        private double noframecolspace = 10;//对于无框图例，列之间的间距;
        private LegendPosStyle _legendpos;
        private LegendDrawStyle _legenddrawstyle;
        private double _outframewidth = 0;//图框宽度；对应绘图区域的宽度
        private double _NoframeLegendVSTitleHeigh = 10;
        private Hashtable symbolOperHt;
        private double legendunitwidth = 0;//有框图例的单元格宽度；
        private double legendtxtheigh =3;//中文名字的字高；
        private string legendCNtxtstyle = "宋体";//中文名字的字体
        private double legmaxwidth = 0;

        public LJJSPoint ptStart
        {
            set { _ptstart = value; }
            get { return _ptstart; }
        }
        public LegendDrawing(LJJSPoint ptStart, List<string> legendList, int legendColumn, double legendUnitHeigh, LegendPosStyle legendPos, LegendDrawStyle legendDrawStyle, double outFrameWidth)
        {
            _ptstart = ptStart;
            _legendlist = legendList;
            _legendpos = legendPos;
            if (legendUnitHeigh > 0)
                _legendunitheigh = legendUnitHeigh;
            _legendcolumn = legendColumn;
            _legenddrawstyle = legendDrawStyle;
            _outframewidth = outFrameWidth;
            symbolOperHt = GetSymbolOperHt(_legendlist);
            legendrownum = GetLegendRowsCount(_legendcolumn);
            if (_legendcolumn > 0)
                legendunitwidth = _outframewidth / _legendcolumn;
        }
        private int GetLegendRowsCount(int legendColnumsCount)
        {
            int legendcount = _legendlist.Count;
            int rowcount = 0;
            if (legendcount > 0 && legendColnumsCount > 0)
            {
                rowcount = ((int)((legendcount - 1) / legendColnumsCount)) + 1;
            }
            return rowcount;
        }
        /// <summary>
        /// 根据左基点和符号内容，添加符号或颜色文本;
        /// </summary>
        private void AddLegendHaveLeftTopBaseAndWH(LJJSPoint ptBase, SymbolCodeClass symboloper)
        {

            symboloper.InserLegendSymbol(new LJJSPoint(ptBase.XValue, ptBase.YValue - _legendunitheigh * 0.5), legendunitwidth, _legendunitheigh);
            LJJSText.AddHorCommonText(symboloper.symbolChineseName, new LJJSPoint(ptBase.XValue + legendunitwidth * 0.5, ptBase.YValue - _legendunitheigh * 1.6), DrawCommonData.BlackColorRGB,AttachmentPoint.MiddleCenter, legendtxtheigh, legendCNtxtstyle);
                

        }
        public List<LJJSPoint> AddLegendFrameToFigure()
        {
            List<LJJSPoint> ptbasecol = new List<LJJSPoint>();
            if (_legendunitheigh > 0.01 && legendunitwidth > 0.01)
            {
                for (int i = 0; i < legendrownum; i++)
                {
                    for (int j = 0; j < _legendcolumn; j++)
                    {
                        LJJSPoint ptbase = new LJJSPoint(_ptstart.XValue+ j * legendunitwidth, _ptstart.YValue - i * _legendunitheigh*2);
                        LegendOper.AddSunStyleRect(ptbase, -2*_legendunitheigh, legendunitwidth, 0);
                        ptbasecol.Add(ptbase);
                    }
                }
            }
            return ptbasecol;
        }
        private void AddHaveFrameLegend(ArrayList al)
        {
            if (legendunitwidth < 0.01)
                return;
          List<LJJSPoint> ptbasecol = AddLegendFrameToFigure();
            for (int i = 0; i < al.Count; i++)
            {
                int keyvalue = (int)al[i];
                SymbolCodeClass symoper = (SymbolCodeClass)symbolOperHt[keyvalue];
                if (null != symoper)
                {
                    if (i < ptbasecol.Count)
                        AddLegendHaveLeftTopBaseAndWH(ptbasecol[i], symoper);
                }
            }
        }
        private List<LJJSPoint> GetNoFramePtbaseCol()
        {
            List<LJJSPoint> ptbasecol = new List<LJJSPoint>();
            legendunitwidth = legmaxwidth + noframecolspace + noframelegtxtwidth + noframetxtvslegspace;
            if (_legendunitheigh > 0.01 && legendunitwidth > 0.01)
            {
                for (int i = 0; i < _legendcolumn; i++)
                {
                    for (int j = 0; j < legendrownum; j++)
                    {
                        LJJSPoint ptbase = new LJJSPoint(_ptstart.XValue + i * legendunitwidth, _ptstart.YValue- j * _legendunitheigh);
                        ptbasecol.Add(ptbase);
                    }
                }
            }
            return ptbasecol;

        }
        private void AddNoFrameLegend(ArrayList al)
        {
            List<LJJSPoint> ptbasecol = GetNoFramePtbaseCol();
            for (int i = 0; i < al.Count; i++)
            {
                int keyvalue = (int)al[i];
                SymbolCodeClass symoper = (SymbolCodeClass)symbolOperHt[keyvalue];
                if (i < ptbasecol.Count)
                    AddPerLegendNoFrame(ptbasecol[i], symoper);
            }
        }
        private void AddPerLegendNoFrame(LJJSPoint ptbase, SymbolCodeClass symoper)
        {
            if (symoper == null)
                return;
            double legwidth = symoper.legendWidth;
            Rect.AddBlackRect(ptbase, -_legendunitheigh,legwidth , 0,new DrawDirection(1,1));
            symoper.InserLegendSymbol(new LJJSPoint(ptbase.XValue, ptbase.YValue- _legendunitheigh * 0.5), legwidth, _legendunitheigh);
            LJJSText.AddHorCommonText(symoper.symbolChineseName, new LJJSPoint(ptbase.XValue + legwidth + noframetxtvslegspace, ptbase.YValue - _legendunitheigh * 0.5), DrawCommonData.BlackColorRGB, AttachmentPoint.MiddleLeft, legendtxtheigh, legendCNtxtstyle);
                //(symoper.symbolChineseName, legendtxtheigh, new LJJSPoint(ptbase.XValue+ legwidth + noframetxtvslegspace, ptbase.YValue - _legendunitheigh * 0.5), AttachmentPoint.MiddleLeft, legendCNtxtstyle);
        }
        public void AddLegendToFigure()
        {
            ArrayList al = new ArrayList(symbolOperHt.Keys);
            al.Sort();
            if (_legendunitheigh < 0.01)
                return;
            _ptstart = GetLegendptStart(_ptstart.XValue, _ptstart.YValue, _legendpos, _legenddrawstyle);
            if (_legenddrawstyle == LegendDrawStyle.HaveOutFrame)
            {
                AddHaveFrameLegend(al);
            }
            else
            {
                AddNoFrameLegend(al);
            }

        }
        private LJJSPoint GetLegendptStart(double xstart, double ystart, LegendPosStyle legendPos, LegendDrawStyle legendDrawStyle)
        {
            LJJSPoint ptstart;

            if (legendDrawStyle == LegendDrawStyle.HaveOutFrame)
            {
                if (legendPos == LegendPosStyle.BottomPos)
                    ptstart = new LJJSPoint(xstart, ystart);
                else
                    ptstart = new LJJSPoint(xstart, ystart + (legendrownum * _legendunitheigh) * 2);

            }
            else
            {
                legmaxwidth = GetLegendMaxWidth(symbolOperHt);
                double legoutframewidth = (noframecolspace + legmaxwidth + noframelegtxtwidth + noframetxtvslegspace) * _legendcolumn - noframecolspace;
                double ptstartX = xstart + (_outframewidth - legoutframewidth) / 2;
                if (legendPos == LegendPosStyle.TopPos)
                    ptstart = new LJJSPoint(ptstartX, ystart + legendrownum * _legendunitheigh + _NoframeLegendVSTitleHeigh);
                else
                    ptstart = new LJJSPoint(ptstartX, ystart - _NoframeLegendVSTitleHeigh);
            }
            return ptstart;
        }
        private double GetLegendMaxWidth(Hashtable syoperHt)
        {
            double maxwidth = 0;
            foreach (DictionaryEntry de in syoperHt)
            {
                SymbolCodeClass symoper = (SymbolCodeClass)de.Value;
                if (symoper.legendWidth > maxwidth)
                    maxwidth = symoper.legendWidth;
            }
            return maxwidth;
        }
        private Hashtable GetSymbolOperHt(List<string> symbolCodeList)
        {
            Hashtable ht = new Hashtable();
            foreach (string symcodestr in symbolCodeList)
            {
                SymbolCodeClass symboloper = (SymbolCodeClass)FillSymbolCode.SymbolCodeClassHt[symcodestr];
                if (!ht.Contains(symboloper.legendSequence) && symboloper != null)
                    ht.Add(symboloper.legendSequence, symboloper);
            }
            return ht;
        }
    }
}
