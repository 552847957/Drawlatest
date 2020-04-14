using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.Model.Drawing;
using LJJSCAD.DrawingOper;
using LJJSCAD.BlackBoard.LJJSDesignData.Impl;
using LJJSCAD.ItemStyleOper;
using System.Data;
using LJJSCAD.Model;
using System.Windows.Forms;
using LJJSCAD.Util;
using LJJSCAD.DrawingElement;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.Drawing.Hatch;
using LJJSCAD.CommonData;
using DesignEnum;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem.HatchRectItem
{
    class StandardHatchRectBuilder : DrawItemBuilder
    {
        HatchRectDesignClass itemDesignStruc;
        private KeDuChiItem drawkdc;
        Dictionary<string, string> colorDic;
        public override void SetItemStruct()
        {
            ItemDesignBlackBoardRead itemDesignBlackBoardRead = new ItemDesignBlackBoardRead(this.ID);
            itemDesignStruc = (HatchRectDesignClass)itemDesignBlackBoardRead.ReturnItemInstance(DrawItemStyle.HatchRectItem);
        
        }

        public override void InitOtherItemDesign()
        {
            colorDic = ColorCodeDic.GetColorDescKeyDic();
            List<KeDuChiItem> m_KDCList = (List<KeDuChiItem>)KeDuChiManage.LineItemKDCHt[ItemName];
            if (null != m_KDCList && m_KDCList.Count > 0)
            {
                drawkdc = m_KDCList[0];
            }
            ////图层设计；
            //if(null !=drawkdc)
            //{
            //    string layername =DrawItemStyle.HatchRectItem.ToString()+ drawkdc.KName;

            //}

        }
        private string GetItemSqlTxt(string jTop, string jBottom)
        {
            string restr = "";
            if (!string.IsNullOrWhiteSpace(itemDesignStruc.Y_TopField) && !string.IsNullOrWhiteSpace(itemDesignStruc.Y_BottomField))
            {
                restr = restr + itemDesignStruc.Y_BottomField + ">" + jTop + " and " + itemDesignStruc.Y_TopField + "<" + jBottom;

            }
            else
            {
                MessageBox.Show("直方图填充设计有误！缺少顶底字段");
            }

            return restr;

        }

        public override void AddPerJDDrawItem(JDStruc jdStruc, int index)
        {
          List<HatchRectDrawStrut> drawStructList=GetPerJDDrawStructLst( jdStruc);
          if (null != drawStructList && drawStructList.Count > 0)
          {
              for(int i=0;i<drawStructList.Count;i++)
              {
                  MyHatch.AddAreaHatch(DrawCommonData.activeDocument, drawStructList[i].FillArea, drawStructList[i].LjjsHatch);
              }
          }



        }

        private List<HatchRectDrawStrut> GetPerJDDrawStructLst(JDStruc jdStruc)
        {
            LJJSPoint lrptstart = ZuoBiaoOper.UpdateLRStartPt(drawkdc.KDir, jdStruc.JDPtStart, lineRoadEnvironment.LineRoadWidth);
            double jdTop = jdStruc.JDtop;
            double jdBottom = jdStruc.JDBottom;


            List<HatchRectDrawStrut> hrdsLst = new List<HatchRectDrawStrut>();
            if (null == itemDataTable || itemDataTable.Rows.Count < 1)
                return hrdsLst;
            string sql = GetItemSqlTxt(jdTop.ToString(), jdBottom.ToString());
            DataRow[] drs = itemDataTable.Select(sql, itemDesignStruc.Y_TopField + " ASC");
            if (itemDesignStruc.hrItemSubStyle.Equals(HatchRectItemSubStyle.ColorField))
            {
                foreach (DataRow dr in drs)
                {
                    List<LJJSPoint> areaLst = GetFillAreaPtLst(lrptstart, jdTop, jdBottom, dr);
                    string color = dr[itemDesignStruc.Color_FromFiled].ToString();
                    if (colorDic.ContainsKey(color))
                    {
                        string colorvalue = colorDic[color];
                        LJJSHatch ljjsHatch = new LJJSHatch(colorvalue, bool.FalseString);
                        HatchRectDrawStrut hrds = new HatchRectDrawStrut(areaLst, ljjsHatch);
                        hrdsLst.Add(hrds);
                    }

                }
            }
            else
            {
                foreach (DataRow dr in drs)
                {
                    List<LJJSPoint> areaLst = GetFillAreaPtLst(lrptstart, jdTop, jdBottom, dr);

                    LJJSHatch ljjsHatch = itemDesignStruc.ItemHatch;
                    HatchRectDrawStrut hrds = new HatchRectDrawStrut(areaLst, ljjsHatch);
                    hrdsLst.Add(hrds);

                }

            }
            return hrdsLst;
        }

        private List<LJJSPoint> GetFillAreaPtLst(LJJSPoint lrptstart, double jdTop, double jdBottom, DataRow dr)
        {
            double depthtop=jdTop, depthbottom = jdBottom;
            depthtop = StrUtil.StrToDouble(dr[itemDesignStruc.Y_TopField].ToString(), "绘图数据缺少顶部数据", "顶部数据为非数值型");
            if (depthtop < jdTop)
                depthtop = jdTop;
            depthbottom = StrUtil.StrToDouble(dr[itemDesignStruc.Y_BottomField].ToString(), "绘图数据缺少底部数据", "底数据为非数值型");
            if (depthbottom > jdBottom)
            {
                depthbottom = jdBottom;
            }
            if (Math.Abs(depthbottom - depthtop) < 0.000001)
            {
                depthbottom = depthbottom + 0.01;
                depthtop = depthtop - 0.01;
            }           
            double xvalue = StrUtil.StrToDouble(dr[itemDesignStruc.Xfield].ToString(), "缺少X数据", "X数据为非数值型");
            List<LJJSPoint> areaLst = GetFillAreaPtLst(lrptstart, jdTop, depthtop, depthbottom, xvalue);
            return areaLst;
        }

        private List<LJJSPoint> GetFillAreaPtLst(LJJSPoint lrptstart, double jdTop, double depthtop, double depthbottom, double xvalue)
        {
            List<LJJSPoint> ptlist = new List<LJJSPoint>();
            ZuoBiaoOper zbopp = new ZuoBiaoOper(FrameDesign.ValueCoordinate);
            LJJSPoint ptTopend = zbopp.GetDrawingZuoBiaoPt(lrptstart, drawkdc, xvalue, depthtop, jdTop, lineRoadEnvironment.LineRoadWidth);
            LJJSPoint ptTopstart = new LJJSPoint(lrptstart.XValue, ptTopend.YValue);
            LJJSPoint ptBottomEnd = zbopp.GetDrawingZuoBiaoPt(lrptstart, drawkdc, xvalue, depthbottom, jdTop, lineRoadEnvironment.LineRoadWidth);
            LJJSPoint ptBottomStart = new LJJSPoint(lrptstart.XValue, ptBottomEnd.YValue);
            ptlist.Add(ptTopstart);
            ptlist.Add(ptTopend);
            ptlist.Add(ptBottomEnd);
            ptlist.Add(ptBottomStart);
            return ptlist;
        }

        public override void AddItemTitle()
        {
            CurveItemTitleClass title = new CurveItemTitleClass();
            title.showName = this.itemDesignStruc.ItemShowName;
            title.isKDCShow = this.itemDesignStruc.KDCIfShow;
            title.itemTitlePos = this.itemDesignStruc.ItemShowNamePosition;
            title.firstKDCStartHeigh = this.itemDesignStruc.ItemHeaderStartheigh;
            title.curveItemUnit = this.itemDesignStruc.ItemUnit;
            title.curveUnitPos = this.itemDesignStruc.UnitPosition;
            title.showNameVSKDCHeigh = this.itemDesignStruc.ShowNameVSKDCHeigh;

            //和曲线项添加绘图项头部一样；
            LJJSCAD.DrawingOper.AddItemTitle.AddLineItemTitleToFig(LineRoadStartPt, lineRoadEnvironment.LineRoadWidth, title);           
        }

        public override void BuildItemDrawData(List<JDStruc> jdStruc)
        {
            ItemWorkDataTableRead itemWorkDataTableRead = new ItemWorkDataTableRead(itemDesignStruc.ID);
            ItemDataTable = (DataTable)itemWorkDataTableRead.ReturnItemInstance(DrawItemStyle.HatchRectItem);
        }
    }
}
