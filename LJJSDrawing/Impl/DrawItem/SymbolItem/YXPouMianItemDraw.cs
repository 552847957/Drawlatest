using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.Model.Drawing;
using LJJSCAD.DrawingOper;
using LJJSCAD.DrawingElement;
using LJJSCAD.Util;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.BlackBoard.LJJSDesignData.Impl;
using LJJSCAD.Drawing.Figure;
using LJJSCAD.Drawing.Symbol;
using LJJSCAD.ItemStyleOper;
using System.Data;
using DesignEnum;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem.SymbolItem
{
    class YXPouMianItemDraw:DrawItemBuilder
    {
        private SymbolItemDesignStruct symItemDesignStruc;
        public override void SetItemStruct()
        {
            ItemDesignBlackBoardRead itemDesignBlackBoardRead = new ItemDesignBlackBoardRead(this.ID);
            symItemDesignStruc = (SymbolItemDesignStruct)itemDesignBlackBoardRead.ReturnItemInstance(DrawItemStyle.SymbolItem);
            //symItemDesignStruc = (SymbolItemDesignStruct)ItemDesignBlackBoardRead.GetItemFromBlackBoard(this.ID, DrawItemStyle.SymbolItem);
        }

        public override void InitOtherItemDesign()
        {
            
        }

        public override void AddPerJDDrawItem(JDStruc jdStruc, int index)
        {
            SymbolItemBuilderOper symbolItemBuilderOper = new SymbolItemBuilderOper();
            symbolItemBuilderOper.SymItemDesignStruc = symItemDesignStruc;
            List<SymbolItemStruc> yxpmstruclist = symbolItemBuilderOper.GetSymbolItemPerJDDrawData(jdStruc,ItemDataTable);
            AddYXPouMianToFigure(jdStruc, yxpmstruclist);
        }
        private void AddYXPouMianToFigure(JDStruc jdstruc, List<SymbolItemStruc> yxpmStrucLst)
        {

            for (int i = 0; i < yxpmStrucLst.Count; i++)
            {
                SymbolItemStruc symbldrawing = yxpmStrucLst[i];
                LJJSPoint sybltopZuoBiaoPt = ZuoBiaoOper.GetJSZuoBiaoPt(jdstruc.JDPtStart, symbldrawing.depthtop, jdstruc.JDtop, FrameDesign.ValueCoordinate);
                LJJSPoint syblbottomZuoBiaoPt = ZuoBiaoOper.GetJSZuoBiaoPt(jdstruc.JDPtStart, symbldrawing.depthbottom, jdstruc.JDtop, FrameDesign.ValueCoordinate);
                LJJSPoint syblpos = ZuoBiaoOper.GetMidPtBetweenTwoPt(sybltopZuoBiaoPt, syblbottomZuoBiaoPt);

                for (int j = 0; j < symbldrawing.sybolcodelist.Count; j++)
                {
                    double xscale = 1;
                    double yscale = 1;
                    string syblcode = symbldrawing.sybolcodelist[j].Trim();
                    if (syblcode != "n" && syblcode != "")
                    {
                        SymbolCodeClass sybloper = (SymbolCodeClass)FillSymbolCode.SymbolCodeClassHt[syblcode];
                        if (null == sybloper)
                            continue;
                        if (sybloper.ifZXEnlarge)
                        {
                            yscale = Math.Abs(sybltopZuoBiaoPt.YValue - syblbottomZuoBiaoPt.YValue);
                        }
                        else if (sybloper.ifFill)
                        {
                            xscale = sybloper.symbolWidth;
                            yscale = Math.Abs(sybltopZuoBiaoPt.YValue - syblbottomZuoBiaoPt.YValue);
                        }
                        if (sybloper.symbolWidth > 0)
                            Rect.AddBlackRect(syblbottomZuoBiaoPt,Math.Abs(syblbottomZuoBiaoPt.YValue-sybltopZuoBiaoPt.YValue),sybloper.symbolWidth,0,new DrawDirection(1,1));

                        SymbolAdd.InsertBlock(sybloper.symbolcode, xscale, yscale, syblpos);
                    }
                }

            }
        }


        public override void AddItemTitle()
        {
            LJJSCAD.DrawingOper.AddItemTitle.AddNormalItemTitleText(this.LineRoadStartPt, symItemDesignStruc.ItemName, symItemDesignStruc.SymbolItemTitleHorPos, this.lineRoadEnvironment.LineRoadWidth, symItemDesignStruc.SyHeaderStartheigh, symItemDesignStruc.SyItemTxtVerSpace);
        }

        public override void BuildItemDrawData(List<JDStruc> jdStruc)
        {

            ItemWorkDataTableRead itemWorkDataTableRead = new ItemWorkDataTableRead(this.ID);
            ItemDataTable = (DataTable)itemWorkDataTableRead.ReturnItemInstance(DrawItemStyle.SymbolItem);
        }


    }
}
