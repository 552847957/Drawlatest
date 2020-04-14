using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.DrawingOper;
using LJJSCAD.Model.Drawing;
using System.Data;
using LJJSCAD.ItemStyleOper;
using LJJSCAD.CommonData;
using LJJSCAD.DrawingElement;
using System.Collections;
using LJJSCAD.DAL;
using System.Windows.Forms;
using LJJSCAD.Util;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.Drawing.Hatch;
using LJJSCAD.Drawing.Manage;
using LJJSCAD.LJJSDrawing.Impl.DrawItem.ItemBuildExec;
using DesignEnum;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem.CurveHasHatchItem
{
    /// <summary>
    /// 绘制带有填充的曲线项；
    /// </summary>
    class StandardCurveHasHatchBuilder : DrawItemBuilder
    {
        CurveHasHatchDesignClass itemDesignStruc;
        private List<LJJSPoint> verBoundaryLineLst;//左侧或者右侧的垂直边界线，也叫补充线;
   
        private KeDuChiItem keDuChiItem=null;
        public override void SetItemStruct()
        {
            ItemDesignBlackBoardRead itemDesignBlackBoardRead = new ItemDesignBlackBoardRead(this.ID);
            itemDesignStruc = (CurveHasHatchDesignClass)itemDesignBlackBoardRead.ReturnItemInstance(DrawItemStyle.CurveHasHatchItem);
        
        }

        public override void InitOtherItemDesign()
        {

            //设置刻度尺；
            List<KeDuChiItem> m_KDCList = (List<KeDuChiItem>)KeDuChiManage.LineItemKDCHt[ItemName];
            if (null != m_KDCList && m_KDCList.Count > 0)
                keDuChiItem = m_KDCList[0];
            else
            {          
                return;
            }

            //设置补充线，从线道头开始，至线道末的垂直线；
            verBoundaryLineLst = new List<LJJSPoint>();
            double xstart;
            if (itemDesignStruc.HatchPosition.Equals(MyCurveHatchPos.Left))
                xstart = this.LineRoadStartPt.XValue;
            else
                xstart = this.LineRoadStartPt.XValue + lineRoadEnvironment.LineRoadWidth;

            LJJSPoint start = new LJJSPoint(xstart, this.LineRoadStartPt.YValue);
            verBoundaryLineLst.Add(start);
            int lastjdcount = this.lineRoadEnvironment.JdDrawLst.Count();
            if (lastjdcount > 0)
            {
                JDStruc jd = this.lineRoadEnvironment.JdDrawLst[lastjdcount - 1];
                LJJSPoint endPt = new LJJSPoint(xstart, jd.JDPtStart.YValue + DrawCommonData.DirectionDown * jd.JDHeight);
                verBoundaryLineLst.Add(endPt);
            }
        }

        public override void AddPerJDDrawItem(JDStruc jdStruc,int index)
        {

          
            if (null == keDuChiItem)
                return;
            if (null == verBoundaryLineLst && verBoundaryLineLst.Count < 2)
                return;
      
            double jdtop = jdStruc.JDtop;
            double jdbottom = jdStruc.JDBottom;

            Hashtable drawht;
            if (jdtop < jdbottom)
            {
                try
                {
                    drawht = CurveItemDAL.GetCurveItemPointHt(ItemDataTable, jdtop.ToString(), jdbottom.ToString(), itemDesignStruc.XFieldName, itemDesignStruc.JSField);//..GetJoinAdjustLineItemPointCol(LineItemDrawDt, sqltxt, m_liStruc.LIFromFieldName, depth);//保存了绘图的点集；key为Y值，value为x值；
                }
                catch
                {
                    MessageBox.Show("请检查数据表字段是否正确222");
                    return;
                }
                if (drawht.Count > 0)
                {                
                    AddCurvesHasHatch(jdStruc, drawht);                  
                }
            }
      
        }
        private void  AddCurvesHasHatch(JDStruc jdstruc,Hashtable  drawht)
        {
           
          
            if (null != verBoundaryLineLst && verBoundaryLineLst.Count > 0)
            {
                CurveHatch curveHatch = new CurveHatch(jdstruc, keDuChiItem, this.lineRoadEnvironment.LineRoadWidth);

                List<LJJSPoint> drawPtCol = curveHatch.GetDrawZuoPtCol(drawht, verBoundaryLineLst[0].XValue);

                curveHatch.CurvesBuildExec(drawPtCol, itemDesignStruc.ItemHatch, verBoundaryLineLst);
            }



        }      

      
        private LJJSPoint GetDuiShuPointZB(KeDuChiItem duiShuKDC, LJJSPoint convertPt, JDStruc jdstruc, LJJSPoint lrptstart)
        {

            double Xpt = DuiShuOper.XGetDSZuoBiaoValue(lrptstart.XValue, duiShuKDC.KDir, duiShuKDC.KParm, convertPt.XValue, duiShuKDC.KMin);
            double Ypt = ZuoBiaoOper.GetJSZongZBValue(jdstruc.JDPtStart.YValue, convertPt.YValue, jdstruc.JDtop, FrameDesign.ValueCoordinate);
            return new LJJSPoint(Xpt, Ypt);
        }
        private LJJSPoint GetPtZBByKDC(KeDuChiItem drawkdc, LJJSPoint lrptstart, LJJSPoint converPt, double jdtop)
        {
            ZuoBiaoOper zbopp = new ZuoBiaoOper(FrameDesign.ValueCoordinate);
            return zbopp.GetDrawingZuoBiaoPt(lrptstart, drawkdc, converPt.XValue, converPt.YValue, jdtop, LineRoadEnvironment.LineRoadWidth);

        }
     
        public override void AddItemTitle()
        {
            CurveItemTitleClass title = new CurveItemTitleClass();
            title.showName = this.itemDesignStruc.ItemShowName;
            title.isKDCShow = this.itemDesignStruc.KDCIfShow;
            title.itemTitlePos = this.itemDesignStruc.ItemShowNamePosition;
            title.firstKDCStartHeigh = this.itemDesignStruc.CurveHeaderStartheigh;
            title.curveItemUnit = this.itemDesignStruc.CurveUnit;
            title.curveUnitPos = this.itemDesignStruc.UnitPosition;
            title.showNameVSKDCHeigh = this.itemDesignStruc.CurveShowNameVSKDCHeigh;

            //和曲线项添加绘图项头部一样；
            LJJSCAD.DrawingOper.AddItemTitle.AddLineItemTitleToFig(LineRoadStartPt, lineRoadEnvironment.LineRoadWidth, title);           
        }

        public override void BuildItemDrawData(List<JDStruc> jdStruc)
        {
            ItemWorkDataTableRead itemWorkDataTableRead = new ItemWorkDataTableRead(itemDesignStruc.ID);
            ItemDataTable = (DataTable)itemWorkDataTableRead.ReturnItemInstance(DrawItemStyle.CurveHasHatchItem);
        }
    }
}
