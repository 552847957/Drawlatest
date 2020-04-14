using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.DrawingOper;
using LJJSCAD.Model.Drawing;
using LJJSCAD.LJJSDrawing.Impl.DrawItem.ItemBuildExec;
using LJJSCAD.ItemStyleOper;
using System.Data;
using LJJSCAD.DrawingElement;
using LJJSCAD.LJJSDrawing.Impl.SQLStr;
using LJJSCAD.Util;
using LJJSCAD.DrawingDesign.Frame;
using DesignEnum;
namespace LJJSCAD.LJJSDrawing.Impl.DrawItem.NormalPuTuItem
{
    class StandardNormalPuTuBuilder : DrawItemBuilder
    {
        private DataTable MainDataTb;
        private DataTable ClosedAreaDataTb;
        private NormalPuTuDesignClass itemDesignStruc;
        private double starty;
        private double endy;
        private KeDuChiItem kdcItem;
     
        public override void SetItemStruct()
        {
            ItemDesignBlackBoardRead itemDesignBlackBoardRead = new ItemDesignBlackBoardRead(this.ID);
            itemDesignStruc = (NormalPuTuDesignClass)itemDesignBlackBoardRead.ReturnItemInstance(DrawItemStyle.NormalPuTuItem);
        
        }

        public override void InitOtherItemDesign()
        {
            kdcItem = KDCBuildExec.GetFirstKDCItemByItemName(this.ItemName);        
        }

        public override void AddPerJDDrawItem(JDStruc jdStruc, int index)
        {
            LJJSPoint ptStart;
            double jdtop = jdStruc.JDtop;
            double jdbottom = jdStruc.JDBottom;
            ptStart = jdStruc.JDPtStart;
            string subsql = SQLBuilder.GetSingleDepthSubSql(jdtop, jdbottom, this.itemDesignStruc.Main_GWJSField);
            DataRow[] drs = MainDataTb.Select(subsql, itemDesignStruc.Main_GWJSField + " ASC");
            if (null != drs && drs.Length > 0)
            {
                foreach (DataRow dr in drs)
                {
                    string yywz = dr[itemDesignStruc.Main_YYWZField].ToString();
                    double gwjs = (double)dr[itemDesignStruc.Main_GWJSField];                    
                    AddPerYYWZHatch(yywz, gwjs, ptStart,jdStruc.JDtop);
                }
            }
        }
        private List<LJJSPoint> GetClosedAreaDRS(KeDuChiItem kdc, string subsql, double gwjsYZuoBiao, LJJSPoint ptStart)
        {
            List<LJJSPoint> rev = new List<LJJSPoint>();          
            DataRow[] drs = ClosedAreaDataTb.Select(subsql, itemDesignStruc.ClosedArea_Field_X + " ASC");
            if (null != drs && drs.Count() > 0)
            {
                
                foreach (DataRow dr in drs)
                {
                    double x = (double)dr[itemDesignStruc.ClosedArea_Field_X];
                    double y = gwjsYZuoBiao + (double)dr[itemDesignStruc.ClosedArea_Feild_Y] * StandardHCGZItemBuilder.HCGZLTXiShu;
                    double xzb = DuiShuOper.XGetDSZuoBiaoValue(ptStart.XValue, kdc.KDir, kdc.KParm, x, kdc.KMin);
                    rev.Add(new LJJSPoint(xzb, y));
                }

            }
            return rev;
        }

        private void AddPerYYWZHatch(string yYWZ, double gWJS, LJJSPoint ptStart, double jdTop)
        {
            double gwjsZuoBiaoY = ZuoBiaoOper.GetJSZongZBValue(ptStart.YValue, gWJS, jdTop, FrameDesign.ValueCoordinate);
            string subsql = this.itemDesignStruc.ClosedArea_Field_YYWZ + @"='" + yYWZ + @"'";
            if (null != kdcItem)
            {
                List<LJJSPoint> sfslst = GetClosedAreaDRS(kdcItem, subsql, gwjsZuoBiaoY,ptStart);
                List<LJJSPoint> HorBoundaryLineLst = new List<LJJSPoint>();
                HorBoundaryLineLst.Add(new LJJSPoint(ptStart.XValue,gwjsZuoBiaoY));
                HorBoundaryLineLst.Add(new LJJSPoint(ptStart.XValue+this.lineRoadEnvironment.LineRoadWidth, gwjsZuoBiaoY));
                CurveHatch curveHatch = new CurveHatch(kdcItem, this.lineRoadEnvironment.LineRoadWidth);
                curveHatch.CurvesBuildExec(sfslst, itemDesignStruc.HatchDic[0], HorBoundaryLineLst);

            }
            
        }

        public override void AddItemTitle()
        {
            CurveItemTitleClass title = new CurveItemTitleClass();
            title.showName = this.itemDesignStruc.ItemShowName;
            title.isKDCShow = true;
            title.itemTitlePos = this.itemDesignStruc.ItemShowNamePosition;
            title.firstKDCStartHeigh = this.itemDesignStruc.FirstKDCStartHeigh;
            title.curveItemUnit = this.itemDesignStruc.TitleUint;
            title.curveUnitPos = this.itemDesignStruc.TitleUnitPosition;
            title.showNameVSKDCHeigh = this.itemDesignStruc.ItemNameVSKDCHeigh;
            //和曲线项添加绘图项头部一样；
            LJJSCAD.DrawingOper.AddItemTitle.AddLineItemTitleToFig(LineRoadStartPt, lineRoadEnvironment.LineRoadWidth, title);     
        }

        public override void BuildItemDrawData(List<JDStruc> jdStruc)
        {
            ItemWorkDataTableRead itemWorkDataTableRead = new ItemWorkDataTableRead(this.itemDesignStruc.ID);
            Dictionary<string, DataTable> dtDic = (Dictionary<string, DataTable>)itemWorkDataTableRead.ReturnItemInstance(DrawItemStyle.NormalPuTuItem);
            MainDataTb = dtDic["mainTable"];
            ClosedAreaDataTb = dtDic["closedAreaTable"];
            
        }

   
    }
}
