using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.Model.Drawing;
using LJJSCAD.DrawingOper;
using System.Data;
using LJJSCAD.ItemStyleOper;
using LJJSCAD.LJJSDrawing.Impl.SQLStr;
using LJJSCAD.DrawingElement;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.Util;
using LJJSCAD.Drawing.Hatch;
using LJJSCAD.CommonData;
using System.Drawing;
using LJJSCAD.LJJSDrawing.Impl.DrawItem.ItemBuildExec;
using DesignEnum;
namespace LJJSCAD.LJJSDrawing.Impl.DrawItem
{
    class StandardHCGZItemBuilder : DrawItemBuilder
    {
        private HCGZItemDesignClass hcgzItemDesignStruc;
        private DataTable hcgzMainDataTb;
        private DataTable hcgzClosedAreaDataTb;
        public static readonly double HCGZLTXiShu = 0.15;    
        private KeDuChiItem kdcItem;
        private LJJSPoint ptStart;
        private double starty;
        private double endy;
        public override void SetItemStruct()
        {
            ItemDesignBlackBoardRead itemDesignBlackBoardRead = new ItemDesignBlackBoardRead(this.ID);
            hcgzItemDesignStruc = (HCGZItemDesignClass)itemDesignBlackBoardRead.ReturnItemInstance(DrawItemStyle.HCGZItem);
        
        }

        public override void InitOtherItemDesign()
        {
            kdcItem = KDCBuildExec.GetFirstKDCItemByItemName(this.ItemName);         
        }

        public override void AddPerJDDrawItem(JDStruc jdStruc, int index)
        {
            double jdtop = jdStruc.JDtop;
            double jdbottom = jdStruc.JDBottom;
            ptStart = jdStruc.JDPtStart;
            string subsql = SQLBuilder.GetSingleDepthSubSql(jdtop, jdbottom, this.hcgzItemDesignStruc.Main_GWJSField);
            DataRow[] drs = hcgzMainDataTb.Select(subsql, hcgzItemDesignStruc.Main_GWJSField + " ASC");
            if (null!=drs && drs.Length > 0)
            {

                foreach (DataRow dr in drs)
                {
                    string yywz = dr[hcgzItemDesignStruc.Main_YYWZField].ToString();
                    double gwjs=(double)dr[hcgzItemDesignStruc.Main_GWJSField];
                    double t2jzz = (double)dr[hcgzItemDesignStruc.Main_T2JZZField];
                    AddPerYYWZHatch(yywz, gwjs, t2jzz,jdStruc.JDtop);
                }
            }

        }
        private  void AddPerYYWZHatch(string yYWZ,double gWJS,double T2jzz,double jdTop)
        {
            double gwjsZuoBiaoY = ZuoBiaoOper.GetJSZongZBValue(ptStart.YValue, gWJS, jdTop, FrameDesign.ValueCoordinate);
            string subsql = this.hcgzItemDesignStruc.ClosedArea_Field_YYWZ + @"='" + yYWZ + @"'";
            if (null!=kdcItem)
            {
                
                double sfsStartX = kdcItem.KMin;
                double sfsEndX = T2jzz;
                double kdsStartX = T2jzz;
                double kdsEndX = kdcItem.KMax;
                List<LJJSPoint> sfslst = GetClosedAreaDRS(sfsStartX, sfsEndX, kdcItem, subsql, gwjsZuoBiaoY);
                double xt2 = DuiShuOper.XGetDSZuoBiaoValue(ptStart.XValue, 1, kdcItem.KParm, T2jzz, kdcItem.KMin);
                double ytmp = endy;
                List<LJJSPoint> kdslst = GetClosedAreaDRS(kdsStartX, kdsEndX, kdcItem, subsql, gwjsZuoBiaoY);
            
                kdslst.Add(new LJJSPoint(xt2, gwjsZuoBiaoY));
                double ytmpkd = starty;

                sfslst.Add(new LJJSPoint(xt2, (ytmp + ytmpkd) * 0.5 * HCGZLTXiShu + gwjsZuoBiaoY));
                sfslst.Add(new LJJSPoint(xt2, gwjsZuoBiaoY)); 
                kdslst.Add(new LJJSPoint(xt2, (ytmp + ytmpkd) * 0.5 * HCGZLTXiShu + gwjsZuoBiaoY));

                AreaHatch.AddStandardAreaHatch(DrawCommonData.activeDocument, sfslst, this.hcgzItemDesignStruc.SFLTColor, this.hcgzItemDesignStruc.SFLTColor);
                AreaHatch.AddStandardAreaHatch(DrawCommonData.activeDocument, kdslst, hcgzItemDesignStruc.KDLTColor, hcgzItemDesignStruc.KDLTColor);
 
            }
   
        }
        private List<LJJSPoint> GetClosedAreaDRS(double startX,double endX,KeDuChiItem kdc,string subsql,double gwjsYZuoBiao)
        {
            List<LJJSPoint> rev = new List<LJJSPoint>();
            if (endX > startX)
            {
                subsql = subsql + " and " + hcgzItemDesignStruc.ClosedArea_Field_X + ">" + startX + " and " + hcgzItemDesignStruc.ClosedArea_Field_X + "<" + endX;
            }
            DataRow[] drs = hcgzClosedAreaDataTb.Select(subsql, hcgzItemDesignStruc.ClosedArea_Field_X + " ASC");
            if (null != drs && drs.Count() > 0)
            {
                starty = (double)drs[0][hcgzItemDesignStruc.ClosedArea_Feild_Y];
                endy = (double)drs[drs.Length - 1][hcgzItemDesignStruc.ClosedArea_Feild_Y];
                foreach (DataRow dr in drs)
                {
                    double x =(double)dr[hcgzItemDesignStruc.ClosedArea_Field_X];
                    double y = gwjsYZuoBiao + (double)dr[hcgzItemDesignStruc.ClosedArea_Feild_Y] * HCGZLTXiShu;
                    double xzb =DuiShuOper.XGetDSZuoBiaoValue(ptStart.XValue, 1, kdc.KParm, x, kdc.KMin);
                    rev.Add(new LJJSPoint(xzb,y));
                }

            }
            return rev;

        }

        public override void AddItemTitle()
        {

            CurveItemTitleClass title = new CurveItemTitleClass();
            title.showName = this.hcgzItemDesignStruc.ItemShowName;
            title.isKDCShow = true;
            title.itemTitlePos = this.hcgzItemDesignStruc.TitlePosition;
            title.firstKDCStartHeigh = this.hcgzItemDesignStruc.FirstKDCStartHeigh;
            title.curveItemUnit = this.hcgzItemDesignStruc.TitleUint;
            title.curveUnitPos = this.hcgzItemDesignStruc.TitleUnitPosition;
            title.showNameVSKDCHeigh = this.hcgzItemDesignStruc.ItemNameVSKDCHeigh;
            //和曲线项添加绘图项头部一样；
            LJJSCAD.DrawingOper.AddItemTitle.AddLineItemTitleToFig(LineRoadStartPt, lineRoadEnvironment.LineRoadWidth, title);     
        }

        public override void BuildItemDrawData(List<JDStruc> jdStruc)
        {
            ItemWorkDataTableRead itemWorkDataTableRead = new ItemWorkDataTableRead(this.hcgzItemDesignStruc.ID);
            Dictionary<string, DataTable> dtDic = (Dictionary<string, DataTable>)itemWorkDataTableRead.ReturnItemInstance(DrawItemStyle.HCGZItem);
            hcgzMainDataTb = dtDic["mainTable"];
            hcgzClosedAreaDataTb = dtDic["closedAreaTable"];
            
        }
    }
}
