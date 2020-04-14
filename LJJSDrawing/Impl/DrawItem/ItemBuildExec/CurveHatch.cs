using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using System.Collections;
using LJJSCAD.Util;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.DrawingOper;
using LJJSCAD.Drawing.Manage;
using LJJSCAD.Drawing.Hatch;
using LJJSCAD.CommonData;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem.ItemBuildExec
{
    class CurveHatch
    {
    
        JDStruc _jdstruc;//井段

        internal JDStruc Jdstruc
        {
            get { return _jdstruc; }
            set { _jdstruc = value; }
        }
        KeDuChiItem _keDuChiItem;//刻度尺

        internal KeDuChiItem KeDuChiItem
        {
            get { return _keDuChiItem; }
            set { _keDuChiItem = value; }
        }
     
        LJJSPoint _jdptstart;//井段起点，和刻度尺有关；正向刻度尺起点在左侧，反向刻度尺起点在右侧；

        public LJJSPoint JDptstart
        {
            get { return _jdptstart; }
            set { _jdptstart = value; }
        }
     
        double _lineRoadWidth;

        public double LineRoadWidth
        {
            get { return _lineRoadWidth; }
            set { _lineRoadWidth = value; }
        }


        public static bool TestBoundaryCurve(List<LJJSPoint> boundaryCurve)
        {
            if (null == boundaryCurve)
                return false;

            List<LJJSPoint> tmplst=new List<LJJSPoint>();
            for (int i = 0; i < boundaryCurve.Count; i++)
            {
                LJJSPoint tmp = boundaryCurve[i];
                if (null != tmp)
                    tmplst.Add(tmp);
            }
            if (tmplst.Count < 2)
                return false;
            else
            {
                boundaryCurve = tmplst;
            }

                return true;
          

        }
        /// <summary>
        /// 已完成排序的带有焦点的绘制点集合；
        /// </summary>
        /// <param name="sourceLst"></param>
        /// <param name="AInterSect"></param>
        /// <param name="BInterSect"></param>
        /// <returns></returns>
        public List<LJJSPoint> GetInterSectCol(List<LJJSPoint> sourceLst, LJJSPoint AInterSect, LJJSPoint BInterSect,int kDir)
        {
            List<LJJSPoint> result = new List<LJJSPoint>();
            if (null == sourceLst || sourceLst.Count < 2)
                return sourceLst;
            double xfenjie=AInterSect.XValue;
            for (int i = 0; i < sourceLst.Count-1; i++)
            {
                if (kDir.Equals(1))
                {
                    if (sourceLst[i].XValue > xfenjie)
                        result.Add(sourceLst[i]);

                }
                else
                {
                    if (sourceLst[i].XValue < xfenjie)
                        result.Add(sourceLst[i]);
                }                
                LJJSPoint intersectPt=new LJJSPoint();
                LJJSPoint C = sourceLst[i];
                LJJSPoint D = sourceLst[i + 1];
               
               int tmp= CurveItemUtil.segIntersect(AInterSect, BInterSect, C, D, ref intersectPt);
               if (tmp.Equals(1))//线段有交点
               {
                   result.Add(intersectPt); 
               }

            }
            if (kDir.Equals(1))
            {
                if (sourceLst[sourceLst.Count - 1].XValue > xfenjie)
                    result.Add(sourceLst[sourceLst.Count - 1]);

            }
            else
            {
                if (sourceLst[sourceLst.Count - 1].XValue < xfenjie)
                    result.Add(sourceLst[sourceLst.Count - 1]);
            }                
        
            result.OrderBy(m => m.YValue);
            return result;
 
        }
        /// <summary>
        /// 获得x值大于分界值的哈希表点集合；
        /// </summary>
        /// <param name="sourceHt"></param>
        /// <param name="fenjieValue"></param>
        /// <returns></returns>
        public static Hashtable GetDrawHtMoreFenJie(Hashtable sourceHt ,double fenjieValue)
        {
            Hashtable result = new Hashtable();
            foreach(DictionaryEntry de in sourceHt)
            {
                double tmp =(double)de.Value;
                if (tmp > fenjieValue)
                    result.Add(de.Key,de.Value);
            }
            return result;
 
        }
        public CurveHatch(JDStruc jdstruc, KeDuChiItem keDuChiItem,  double lineRoadWidth)
        {
            this._jdstruc = jdstruc;
            this._keDuChiItem = keDuChiItem;
            this._jdptstart = ZuoBiaoOper.UpdateLRStartPt(keDuChiItem.KDir, jdstruc.JDPtStart, lineRoadWidth);//根据刻度尺方向获得起点；
            this._lineRoadWidth = lineRoadWidth;
 
        }
        public CurveHatch(KeDuChiItem keDuChiItem, double lineRoadWidth)
        {
           
            this._keDuChiItem = keDuChiItem;          
            this._lineRoadWidth = lineRoadWidth;

        }
    
        public List<LJJSPoint> GetDrawZuoPtCol(Hashtable drawht, double addPtXvalue)
        {
            List<LJJSPoint> drawptcol = new List<LJJSPoint>();
            ArrayList al;//完成排序的绘图点;
            if (null == drawht)
                return drawptcol;
            if (_keDuChiItem.KStyle == KDCStyle.DuiShu)//对数项；
            {
                drawht = HashUtil.MoveHashTableZeroValue(drawht);//去掉数据中的0值；              
            }  

            al = HashUtil.GetHastablePaiXuList(drawht);//完成排序的绘图点;
            if (null != al && al.Count > 1)
            {

                int last = al.Count - 1;
                double firstptJS = (double)al[0];
                double lastptJS = (double)al[last];

                double firstPtY = ZuoBiaoOper.GetJSZongZBValue(_jdptstart.YValue, firstptJS, _jdstruc.JDtop, FrameDesign.ValueCoordinate);
                double lastPtY = ZuoBiaoOper.GetJSZongZBValue(_jdptstart.YValue, lastptJS, _jdstruc.JDtop, FrameDesign.ValueCoordinate);

                drawptcol.Add(new LJJSPoint(addPtXvalue, firstPtY + 0.0001));
               
                for (int i = 0; i < al.Count; i++)
                {
                    double jsvalue = (double)al[i];
                    double xvalue = (double)drawht[jsvalue];
                    if (_keDuChiItem.KStyle == KDCStyle.DuiShu)//对数项；
                    {
                        drawptcol.Add(GetDuiShuPointZB(_keDuChiItem, new LJJSPoint(xvalue, jsvalue), _jdstruc, _jdptstart));
                    }
                    else
                    {
                        drawptcol.Add(GetPtZBByKDC(_keDuChiItem, _jdptstart, new LJJSPoint(xvalue, jsvalue), _jdstruc.JDtop));
                    }
                }
                drawptcol.Add(new LJJSPoint(addPtXvalue, lastPtY - 0.0001));
             
               
            }
         
            return drawptcol;
 
        }
        private LJJSPoint GetPtZBByKDC(KeDuChiItem drawkdc, LJJSPoint lrptstart, LJJSPoint converPt, double jdtop)
        {
            ZuoBiaoOper zbopp = new ZuoBiaoOper(FrameDesign.ValueCoordinate);
            return zbopp.GetDrawingZuoBiaoPt(lrptstart, drawkdc, converPt.XValue, converPt.YValue, jdtop, _lineRoadWidth);

        }
        private static LJJSPoint GetDuiShuPointZB(KeDuChiItem duiShuKDC, LJJSPoint convertPt, JDStruc jdstruc, LJJSPoint lrptstart)
        {

            double Xpt = DuiShuOper.XGetDSZuoBiaoValue(lrptstart.XValue, duiShuKDC.KDir, duiShuKDC.KParm, convertPt.XValue, duiShuKDC.KMin);
            double Ypt = ZuoBiaoOper.GetJSZongZBValue(jdstruc.JDPtStart.YValue, convertPt.YValue, jdstruc.JDtop, FrameDesign.ValueCoordinate);
            return new LJJSPoint(Xpt, Ypt);
        }
        public void CurvesBuildExec(List<LJJSPoint> drawptcol,LJJSHatch itemHatch,List<LJJSPoint>boundaryLinePtList)
        {
            List<List<LJJSPoint>> hatchBoundary = new List<List<LJJSPoint>>();
            hatchBoundary.Add(boundaryLinePtList);
            hatchBoundary.Add(drawptcol);
            Layer.Layer_SetToCurrent(_keDuChiItem.KName);
            MyHatch.AddPolyHatch(DrawCommonData.activeDocument, hatchBoundary, itemHatch);
        }
    }
}
