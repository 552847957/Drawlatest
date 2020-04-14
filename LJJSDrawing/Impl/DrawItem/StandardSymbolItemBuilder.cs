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
using LJJSCAD.Drawing.Curve;
using LJJSCAD.CommonData;
using LJJSCAD.Drawing.Symbol;
using LJJSCAD.ItemStyleOper;
using System.Data;
using DesignEnum;
namespace LJJSCAD.LJJSDrawing.Impl.DrawItem
{
    class StandardSymbolItemBuilder : DrawItemBuilder
    {
        private SymbolItemDesignStruct symItemDesignStruc;
        public override void AddPerJDDrawItem(JDStruc jdStruc, int index)
        {
            SymbolItemBuilderOper symbolItemBuilderOper = new SymbolItemBuilderOper();
            symbolItemBuilderOper.SymItemDesignStruc = symItemDesignStruc;
            List<SymbolItemStruc> yxpmstruclist = symbolItemBuilderOper.GetSymbolItemPerJDDrawData(jdStruc, ItemDataTable);
            AddStandardSymbolItemToFigure(jdStruc, yxpmstruclist);
        }
        private void AddStandardSymbolItemToFigure(JDStruc jdstruc, List<SymbolItemStruc> syDrawingCol)
        {

            for (int i = 0; i < syDrawingCol.Count; i++)
            {
                SymbolItemStruc symbldrawing = syDrawingCol[i];
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
                        SymbolCodeClass sybloper = (SymbolCodeClass)HashUtil.FindObjByKey(syblcode, FillSymbolCode.SymbolCodeClassHt);
                        if (null == sybloper)
                            continue;
                         
                        if (Math.Abs(sybloper.symbolHeigh) > 0.01 && sybloper.ifZXEnlarge)

                            yscale = Math.Abs(sybltopZuoBiaoPt.YValue - syblbottomZuoBiaoPt.YValue) / sybloper.symbolHeigh;

                        if ((Math.Abs(sybloper.symbolWidth) > 0.01) &&symItemDesignStruc.SyblIfHorFill )

                            xscale =this.lineRoadEnvironment.LineRoadWidth / sybloper.symbolWidth;
                        if (symItemDesignStruc.SymbolFramePro  == SymbolFrame.DoubleParallel)
                        {
                            Line.BuildCommonHorLineByLayer(sybltopZuoBiaoPt, lineRoadEnvironment.LineRoadWidth, 0, DrawCommonData.DirectionRight);
                                //.AddContinusHorLine(sybltopZuoBiaoPt, lineRoadEnvironment.LineRoadWidth, 0);
                            Line.BuildCommonHorLineByLayer(syblbottomZuoBiaoPt, lineRoadEnvironment.LineRoadWidth, 0, DrawCommonData.DirectionRight);
                        }
                    //    AddJoinAdjustItemObjId(CommonDrawing.InsertSymbolItem(sybloper.symbolcode, xscale, yscale, syblpos));
                        SymbolAdd.InsertBlock(sybloper.symbolcode, xscale, yscale, syblpos);
                    }
                }
            }

        }

        public override void AddItemTitle()
        {
            LJJSCAD.DrawingOper.AddItemTitle.AddNormalItemTitleText(this.LineRoadStartPt,symItemDesignStruc.ItemName,symItemDesignStruc.SymbolItemTitleHorPos, this.lineRoadEnvironment.LineRoadWidth,symItemDesignStruc.SyHeaderStartheigh,5);
        }

        public override void SetItemStruct()
        {
            ItemDesignBlackBoardRead itemDesignBlackBoardRead = new ItemDesignBlackBoardRead(this.ID);
            symItemDesignStruc = (SymbolItemDesignStruct)itemDesignBlackBoardRead.ReturnItemInstance(DrawItemStyle.SymbolItem);
           // symItemDesignStruc = (SymbolItemDesignStruct)ItemDesignBlackBoardRead.GetItemFromBlackBoard(this.ID, DrawItemStyle.SymbolItem);
        }

        public override void BuildItemDrawData(List<JDStruc> jdStruc)
        {
            //DrawItemKey drawItemKey = new DrawItemKey(this.ID, DrawItemStyle.SymbolItem);
            //ItemDataTable = WorkDataManage.WorkDataManage.GetItemWorkDataTable(drawItemKey);
            ItemWorkDataTableRead itemWorkDataTableRead = new ItemWorkDataTableRead(this.ID);
            ItemDataTable = (DataTable)itemWorkDataTableRead.ReturnItemInstance(DrawItemStyle.SymbolItem);
        }

        public override void InitOtherItemDesign()
        {
            
        }
    }
}
