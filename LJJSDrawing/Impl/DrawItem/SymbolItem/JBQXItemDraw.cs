using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.Model.Drawing;
using LJJSCAD.DrawingOper;
using LJJSCAD.Util;
using LJJSCAD.DrawingElement;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.Drawing.Symbol;
using LJJSCAD.BlackBoard.LJJSDesignData.Impl;
using LJJSCAD.ItemStyleOper;
using System.Data;
using DesignEnum;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem.SymbolItem
{
    class JBQXItemDraw : DrawItemBuilder
    {
        private SymbolItemDesignStruct symItemDesignStruc;
        private double jbqx_SymbolWidth = 20;//井壁取心符号的宽度；
        private static double jbqx_yxhoroffset = 4;//井壁取心的岩性符号平移量;
        private static double jbqx_czhoroffset = 4;//井壁取心的产状符号平移量;
        private static double jbqx_yshoroffset = 16;//井壁取心的颜色内容平移量；
        private static string jbqx_bzFont = "complex";
        private static double jbqx_bzFont_Xcale = 1;
        private static double jbqx_ysCodeTxt_Height = 1.2;
        public override void SetItemStruct()
        {
            ItemDesignBlackBoardRead itemDesignBlackBoardRead = new ItemDesignBlackBoardRead(this.ID);
            symItemDesignStruc = (SymbolItemDesignStruct)itemDesignBlackBoardRead.ReturnItemInstance(DrawItemStyle.SymbolItem);
        }

        public override void InitOtherItemDesign()
        {
            throw new NotImplementedException();
        }

        public override void AddPerJDDrawItem(JDStruc jdStruc, int index)
        {
            SymbolItemBuilderOper symbolItemBuilderOper = new SymbolItemBuilderOper();
            symbolItemBuilderOper.SymItemDesignStruc = symItemDesignStruc;
            List<SymbolItemStruc> yxpmstruclist = symbolItemBuilderOper.GetSymbolItemPerJDDrawData(jdStruc, ItemDataTable);
            AddJBQXItemToFigure(jdStruc, yxpmstruclist);
        }
        private void AddJBQXItemToFigure(JDStruc jdstruc, List<SymbolItemStruc> syDrawingCol)
        {
            double adjacentVerZbdifference = 10;
            double lastVerZBHor = 0;
            double lastverZBVerHeigh = 0;
            double lastadjacentVerZbdifference = 0;
            for (int i = 0; i < syDrawingCol.Count; i++)
            {
                SymbolItemStruc symbldrawing = syDrawingCol[i];
                LJJSPoint sybltopZuoBiaoPt = ZuoBiaoOper.GetJSZuoBiaoPt(jdstruc.JDPtStart, symbldrawing.depthtop, jdstruc.JDtop, FrameDesign.ValueCoordinate);
                adjacentVerZbdifference = lastadjacentVerZbdifference + Math.Abs(lastverZBVerHeigh - sybltopZuoBiaoPt.YValue);

                if (adjacentVerZbdifference < 4)
                {
                    lastVerZBHor = lastVerZBHor +jbqx_SymbolWidth;
                    lastadjacentVerZbdifference = lastadjacentVerZbdifference + Math.Abs(lastverZBVerHeigh - sybltopZuoBiaoPt.YValue);

                }
                else
                {
                    lastVerZBHor = 0;
                    lastadjacentVerZbdifference = 0;
                }
                LJJSPoint syblpos = new LJJSPoint(sybltopZuoBiaoPt.XValue + lastVerZBHor, sybltopZuoBiaoPt.YValue);
                SymbolAdd.InsertBlock("JBQX", 1, 1, syblpos);
              
                int sybolcodecount = symbldrawing.sybolcodelist.Count;

                if (sybolcodecount == 3)
                {
                    string yxcode = symbldrawing.sybolcodelist[0].Trim();
                    if (yxcode != "n" && yxcode != "N" && yxcode != "")
                    {
                        SymbolCodeClass sybloper = (SymbolCodeClass)HashUtil.FindObjByKey(yxcode, FillSymbolCode.SymbolCodeClassHt);
                        if (null != sybloper)
                       // SymbolCodeClass sybloper = (SymbolCodeClass)FillSymbolCode.SymbolCodeClassHt[yxcode];
                        AddJBQXYXSymbolToFigure(syblpos, sybloper);
                    }
                    string czcode = symbldrawing.sybolcodelist[1].Trim();
                    if (czcode != "n" && czcode != "N" && czcode != "")
                    {
                        SymbolCodeClass sybloper = (SymbolCodeClass)HashUtil.FindObjByKey(czcode, FillSymbolCode.SymbolCodeClassHt);
                      //  SymbolCodeClass sybloper = (SymbolCodeClass)FillSymbolCode.SymbolCodeClassHt[czcode];
                        if (null != sybloper)
                        AddJBQXCZSymbolToFigure(syblpos, sybloper);

                    }
                    string yscode = symbldrawing.sybolcodelist[2].Trim();
                    LJJSCAD.Drawing.Text.LJJSText.AddHorCommonTextByLayer(yscode, new LJJSPoint(syblpos.XValue + jbqx_yshoroffset, syblpos.YValue), AttachmentPoint.MiddleLeft, jbqx_ysCodeTxt_Height, jbqx_bzFont);
                   // ArxCSHelper.AddMText(yscode, 1.2, new LJJSPoint(syblpos.XValue + jbqx_yshoroffset, syblpos.YValue), AttachmentPoint.MiddleLeft, bztxtstyleid);
                }
                lastverZBVerHeigh = syblpos.YValue;
            }
        }
        private void AddJBQXCZSymbolToFigure(LJJSPoint insertPos, SymbolCodeClass sybloper)
        {
            LJJSPoint insertpt = new LJJSPoint(insertPos.XValue + jbqx_czhoroffset, insertPos.YValue - 1);
            string tmpsymbol = sybloper.symbolcode.Trim();
            int yenlarge = 2;
            if (tmpsymbol.Equals("yj") || tmpsymbol.Equals("yb") || tmpsymbol.Equals("yg"))
            {
                yenlarge = 1;
            }

            double enlargefactor = 1;
            if (sybloper.symbolWidth > 0)
            {
                if (sybloper.symbolWidth > 8)
                {
                    enlargefactor = 8 / sybloper.symbolWidth;
                }
            }
            SymbolAdd.InsertBlock(sybloper.symbolcode, enlargefactor, yenlarge, insertpt);
        }
        private void AddJBQXYXSymbolToFigure(LJJSPoint insertPos, SymbolCodeClass sybloper)
        {
            LJJSPoint insertpt = new LJJSPoint(insertPos.XValue + jbqx_yxhoroffset, insertPos.YValue + 1);

            double enlargefactor = 1;
            string tmpsymbol = sybloper.symbolcode.Trim();
            int yenlarge = 1;
            if (sybloper.symbolWidth > 0)
            {
                if (sybloper.symbolWidth > 8)
                {
                    enlargefactor = 8 / sybloper.symbolWidth;
                }
            }
            SymbolAdd.InsertBlock(sybloper.symbolcode, enlargefactor, yenlarge, insertpt);
          //  ArxCSHelper.InsertBlock(sybloper.symbolcode, enlargefactor, yenlarge, insertpt);
        }

        public override void AddItemTitle()
        {
            
        }

        public override void BuildItemDrawData(List<JDStruc> jdStruc)
        {
            //DrawItemKey drawItemKey = new DrawItemKey(this.ID, DrawItemStyle.SymbolItem);
            //ItemDataTable = WorkDataManage.WorkDataManage.GetItemWorkDataTable(drawItemKey);

            ItemWorkDataTableRead itemWorkDataTableRead = new ItemWorkDataTableRead(this.ID);
            ItemDataTable = (DataTable)itemWorkDataTableRead.ReturnItemInstance(DrawItemStyle.SymbolItem);
        }
    }
}
