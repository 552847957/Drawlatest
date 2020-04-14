using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.Model.Drawing;
using LJJSCAD.DrawingOper;
using LJJSCAD.DrawingElement;
using System.Windows.Forms;
using LJJSCAD.CommonData;
using LJJSCAD.Util;
using LJJSCAD.ItemStyleOper;
using System.Data;
using System.Collections;
using LJJSCAD.DAL;
using LJJSCAD.LJJSDrawing.Impl.DrawItem.ItemBuildExec;
using DesignEnum;
namespace LJJSCAD.LJJSDrawing.Impl.DrawItem
{
    class MultiHatchCurveItemBuilder : DrawItemBuilder
    {
        MultiHatchCurveDesignClass itemDesignStruc;
        private List<LJJSPoint> firstVerBoundaryLineLst=new List<LJJSPoint>();//左侧或者右侧的第一条垂直边界线，也叫补充线;
        private List<LJJSPoint> secondVerBoundaryLineLst=new List<LJJSPoint>();//左侧或者右侧的第一条垂直边界线，也叫补充线;
        private List<LJJSPoint> thirdVerBoundaryLineLst=new List<LJJSPoint>();//左侧或者右侧的第一条垂直边界线，也叫补充线;
       
        KeDuChiItem kdcItem;
        public override void SetItemStruct()
        {
            ItemDesignBlackBoardRead itemDesignBlackBoardRead = new ItemDesignBlackBoardRead(this.ID);
            itemDesignStruc = (MultiHatchCurveDesignClass)itemDesignBlackBoardRead.ReturnItemInstance(DrawItemStyle.MultiHatchCurveItem);
        
        }

        public override void InitOtherItemDesign()
        {
             List<KeDuChiItem> m_KDCList=(List<KeDuChiItem>)KeDuChiManage.LineItemKDCHt[ItemName];
            //设置刻度尺列表；
             if (null != m_KDCList && m_KDCList.Count > 0)
                 kdcItem = m_KDCList.First();
             else
             {
                 MessageBox.Show("缺少刻度尺设计");
                 return;
             }   
            //设置补充线1，从线道头开始，至线道末的垂直线；
             firstVerBoundaryLineLst = new List<LJJSPoint>();
             double xstart;
            if(kdcItem.KDir.Equals(1))//正向
            {
                xstart = this.LineRoadStartPt.XValue; 
        
            }
            else
            {
                xstart = this.LineRoadStartPt.XValue + lineRoadEnvironment.LineRoadWidth;
            }
           LJJSPoint startone=GetVerBoundaryStartPt(xstart,kdcItem.KMin);
           firstVerBoundaryLineLst.Add(startone);
           firstVerBoundaryLineLst.Add(GetVerBoundaryEndPt(startone.XValue));

            if (itemDesignStruc.FenJieValueOne > 0)
            {
                LJJSPoint starttwo = GetVerBoundaryStartPt(xstart, itemDesignStruc.FenJieValueOne);
                secondVerBoundaryLineLst.Add(starttwo);
                secondVerBoundaryLineLst.Add(GetVerBoundaryEndPt(starttwo.XValue));       
            }
            if(itemDesignStruc.FenJieValueTwo>0)
            {
                LJJSPoint startthree = GetVerBoundaryStartPt(xstart, itemDesignStruc.FenJieValueTwo);
                thirdVerBoundaryLineLst.Add(startthree);
                thirdVerBoundaryLineLst.Add(GetVerBoundaryEndPt(startthree.XValue));
            }   
         

        }

        private LJJSPoint GetVerBoundaryStartPt(double xstart,double fenjieValue)
        {
            double xvalue = xstart;
            if (kdcItem.KStyle.Equals(KDCStyle.DuiShu))
            {
                xvalue = DuiShuOper.XGetDSZuoBiaoValue(xstart, kdcItem.KDir, kdcItem.KParm, fenjieValue, kdcItem.KMin);

            }
            else
            {
                xvalue = ZuoBiaoOper.XGetZuoBiaoValue(xstart, kdcItem, fenjieValue, this.lineRoadEnvironment.LineRoadWidth);
               
            }
            LJJSPoint startPt = new LJJSPoint(xvalue, this.LineRoadStartPt.YValue);
            return startPt;
        }

        private LJJSPoint GetVerBoundaryEndPt(double XValue)
        {
            int lastjdcount = this.lineRoadEnvironment.JdDrawLst.Count();
            if (lastjdcount > 0)
            {
                JDStruc jd = this.lineRoadEnvironment.JdDrawLst[lastjdcount - 1];
                LJJSPoint endPt = new LJJSPoint(XValue, jd.JDPtStart.YValue + DrawCommonData.DirectionDown * jd.JDHeight);
               
                return endPt;
            }
            else
                return null;
        }

        public override void AddPerJDDrawItem(JDStruc jdStruc,int index)
        {
       
            if (null == firstVerBoundaryLineLst && firstVerBoundaryLineLst.Count < 2)
                return;

            double jdtop = jdStruc.JDtop;
            double jdbottom = jdStruc.JDBottom;

            Hashtable drawht;

            if (jdtop < jdbottom)
            {
                try
                {
                    drawht = CurveItemDAL.GetCurveItemPointHt(ItemDataTable, jdtop.ToString(), jdbottom.ToString(), itemDesignStruc.Xfield, itemDesignStruc.Yfield);
                }
                catch
                {
                    MessageBox.Show("请检查数据表字段是否正确111");

                    return;
                }
                if (null!=drawht&&drawht.Count > 1)
                {
                    AddMultiHatchCurve(jdStruc, drawht);
                }
            }
        }

        private void AddMultiHatchCurve(JDStruc jdStruc, Hashtable drawht)
        {
            if (null == kdcItem )
                return;
            if (null == drawht || drawht.Count < 1)
                return;
            CurveHatch curveHatch = new CurveHatch(jdStruc, kdcItem, this.lineRoadEnvironment.LineRoadWidth);
            if (null == firstVerBoundaryLineLst || firstVerBoundaryLineLst.Count < 2)
                return;
            List<LJJSPoint> drawPtCol = curveHatch.GetDrawZuoPtCol(drawht, firstVerBoundaryLineLst[0].XValue);

            if (null != itemDesignStruc.HatchOne && CurveHatch.TestBoundaryCurve(firstVerBoundaryLineLst))
            {
                if(null!=drawPtCol&&drawPtCol.Count>0)
                curveHatch.CurvesBuildExec(drawPtCol, itemDesignStruc.HatchOne, firstVerBoundaryLineLst);

            

            }
          
            if (null != itemDesignStruc.HatchTwo&& itemDesignStruc.FenJieValueOne>0&&CurveHatch.TestBoundaryCurve(secondVerBoundaryLineLst))
            {
                
                 List<LJJSPoint> drawPXcol = curveHatch.GetInterSectCol(drawPtCol, secondVerBoundaryLineLst[0], secondVerBoundaryLineLst[1],kdcItem.KDir);
                 if(null!=drawPXcol&&drawPXcol.Count>0)
                 curveHatch.CurvesBuildExec(drawPXcol, itemDesignStruc.HatchTwo, secondVerBoundaryLineLst);

            }
            if (null != itemDesignStruc.HatchThree && itemDesignStruc.FenJieValueTwo > 0 && CurveHatch.TestBoundaryCurve(thirdVerBoundaryLineLst))
            {
              
                List<LJJSPoint> drawPXcol = curveHatch.GetInterSectCol(drawPtCol, thirdVerBoundaryLineLst[0], thirdVerBoundaryLineLst[1], kdcItem.KDir);
                if(null!=drawPXcol&&drawPXcol.Count>0)
                curveHatch.CurvesBuildExec(drawPXcol, itemDesignStruc.HatchThree, thirdVerBoundaryLineLst);

            }


        }

        public override void AddItemTitle()
        {
            CurveItemTitleClass title=new CurveItemTitleClass();
            title.showName=this.itemDesignStruc.ItemShowName;
            title.isKDCShow=this.itemDesignStruc.KDCIfShow;
            title.itemTitlePos=this.itemDesignStruc.ItemShowNamePosition;
            title.firstKDCStartHeigh=this.itemDesignStruc.ItemHeaderStartheigh;
            title.curveItemUnit=this.itemDesignStruc.ItemUnit;
            title.curveUnitPos=this.itemDesignStruc.UnitPosition;
            title.showNameVSKDCHeigh = this.itemDesignStruc.ShowNameVSKDCHeigh;

            //和曲线项添加绘图项头部一样；
            LJJSCAD.DrawingOper.AddItemTitle.AddLineItemTitleToFig(LineRoadStartPt, lineRoadEnvironment.LineRoadWidth, title);           
        }

        public override void BuildItemDrawData(List<JDStruc> jdStruc)
        {
            ItemWorkDataTableRead itemWorkDataTableRead = new ItemWorkDataTableRead(itemDesignStruc.ID);
            ItemDataTable = (DataTable)itemWorkDataTableRead.ReturnItemInstance(DrawItemStyle.MultiHatchCurveItem);
        }
    }
}
