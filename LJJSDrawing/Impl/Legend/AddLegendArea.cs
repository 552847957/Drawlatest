using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using LJJSCAD.CommonData;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.DrawingOper;
using LJJSCAD.BlackBoard.Legend;
using LJJSCAD.BlackBoard.LJJSDesignData.Impl;
using LJJSCAD.Util;
using DesignEnum;

namespace LJJSCAD.LJJSDrawing.Impl.Legend
{
    class AddLegendArea
    {
    
        private List<string> legendLst;

        public AddLegendArea()
        {
        
        }
        public LJJSPoint LegendAreaBuild()
        {
            LJJSPoint ptStart;
            List<string> tmplengendlist = new List<string>();

            double xstart = DrawCommonData.xStart;
            double ystart = DrawCommonData.yStart;
            LJJSPoint returnpt = new LJJSPoint(xstart, ystart + DrawCommonData.DirectionUp * FrameDesign.LineRoadTitleBarHeigh);
            if (FrameDesign.IfAddLegend)
            {
                LegendDrawStyle drawstyle = EnumUtil.GetEnumByStr(FrameDesign.LegendStyle, LegendDrawStyle.NoOutFrame);
                LegendPosStyle drawpos = EnumUtil.GetEnumByStr(FrameDesign.LegendPos, LegendPosStyle.BottomPos);
                    
                int colcount = FrameDesign.LegendColumnNum;
                double legendunitheigh = FrameDesign.LegendUnitHeigh;
                if (drawpos == LegendPosStyle.TopPos)
                {
                    ptStart = new LJJSPoint(xstart, ystart + DrawCommonData.DirectionUp * FrameDesign.LineRoadTitleBarHeigh);
                }
                else
                {
                    ptStart = new LJJSPoint(xstart, ystart + DrawCommonData.DirectionDown * FrameDrawImpl.FrameHeight);

                }
                SetLegendList();

                foreach (string symbolstr in legendLst)
                {
                    if (FillSymbolCode.SymbolCodeClassHt.Contains(symbolstr))
                        tmplengendlist.Add(symbolstr);
                }
                legendLst.Clear();
                foreach (string tmpsymbolstr in tmplengendlist)
                {
                    legendLst.Add(tmpsymbolstr);

                }

                if (legendLst.Count > 0)
                {
                    LegendDrawing legenddraw = new LegendDrawing(ptStart, legendLst, colcount, legendunitheigh, drawpos, drawstyle, FrameDrawImpl.FrameWidth);
                    legenddraw.AddLegendToFigure();
                    if (drawpos == LegendPosStyle.TopPos)
                        returnpt = legenddraw.ptStart;
                }
            }



            return returnpt; 
        }
        private void SetLegendList()
        {
            List<string> txtlist = LegendOper.AyalyseTxtLegendLst();

            legendLst = new List<string>();
            foreach (string txtstr in txtlist)
            {
                string tmpstr = txtstr.Trim();
                if (!legendLst.Contains(tmpstr))
                    legendLst.Add(tmpstr);
            }
            foreach (string legstr in LegendManage.SymLegendNameLst)
            {
                string tmplegstr = legstr.Trim();
                if (!legendLst.Contains(tmplegstr))
                    legendLst.Add(tmplegstr);
            }
        }
    }
}
