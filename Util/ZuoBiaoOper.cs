using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using LJJSCAD.DrawingOper;
using DesignEnum;

namespace LJJSCAD.Util
{
    class ZuoBiaoOper
    {
        /// <summary>
        /// 方法:获取井深坐标点
        /// </summary>
        /// <param name="jdStartPt">参数:井段坐标点</param>
        /// <param name="JS">参数:井深</param>
        /// <param name="jdStartJs"></param>
        /// <param name="biLiChiValue">参数:比例尺值</param>
        /// <returns></returns>
        public static LJJSPoint GetJSZuoBiaoPt(LJJSPoint jdStartPt, double JS, double jdStartJs, double biLiChiValue)
        {
            double xvalue = jdStartPt.XValue;
            double yvalue = jdStartPt.YValue;
            double jsspace = Math.Abs(JS - jdStartJs);
            if (jdStartJs < JS)//所求点在基点下方；
            {
                yvalue = yvalue - jsspace * 1000 * biLiChiValue;//计算y轴坐标
            }
            else
            {
                yvalue = yvalue + jsspace * 1000 * biLiChiValue;//计算y轴坐标
            }
            return new LJJSPoint(xvalue, yvalue);

        }
        public static LJJSPoint GetMidPtBetweenTwoPt(LJJSPoint ptStart, LJJSPoint ptEnd)
        {
            return new LJJSPoint(0.5 * (ptStart.XValue + ptEnd.XValue), 0.5 * (ptStart.YValue + ptEnd.YValue));
        }
        public static DepthFieldStyle GetDepthFieldStyle(string depthFieldSytleStr)
        {
            if (string.IsNullOrEmpty(depthFieldSytleStr))
                return DepthFieldStyle.ErrorFieldStyle;
            else
            {
                if (depthFieldSytleStr.Equals("BottomANDHeigh"))
                    return DepthFieldStyle.BottomAndHeigh;
                else if (depthFieldSytleStr.Equals("TopAndBottom"))
                    return DepthFieldStyle.TopAndBottom;
                else if (depthFieldSytleStr.Equals("TopAndHeigh"))
                    return DepthFieldStyle.TopAndHeigh;
                else if (depthFieldSytleStr.Equals("Top"))
                    return DepthFieldStyle.Top;
                    
            }
            return DepthFieldStyle.ErrorFieldStyle;
        }

        /// <summary>
        /// 方法:返回深度给出方式
        /// </summary>
        /// <param name="strTop"></param>
        /// <param name="strBottom"></param>
        /// <param name="strHeigh"></param>
        /// <returns></returns>
        public static DepthFieldStyle GetDepthFieldStyle(string strTop, string strBottom, string strHeigh)
        {
            string strtop = strTop.Trim();
            string strbottom = strBottom.Trim();
            string strheigh = strHeigh.Trim();
            DepthFieldStyle revl = DepthFieldStyle.ErrorFieldStyle;
            if (strtop != "")
            {
                if (strBottom != "")
                {
                    revl = DepthFieldStyle.TopAndBottom;
                }
                else if (strHeigh != "")
                {
                    revl = DepthFieldStyle.TopAndHeigh;
                }
                else
                {
                    revl = DepthFieldStyle.Top;
                }
            }
            else if (!string.IsNullOrEmpty(strBottom))
            {
                if (!string.IsNullOrEmpty(strHeigh))
                    revl = DepthFieldStyle.BottomAndHeigh;
           

            }



            return revl;

        }
        public static LJJSPoint GetTiaoBianIntersectionPoint(LJJSPoint pt1, LJJSPoint pt2, double xValue)
        {
            if (Math.Abs(pt1.YValue - pt2.YValue) < 0.00001)
                return new LJJSPoint(xValue, (pt1.YValue+ pt2.YValue) / 2);
            if (Math.Abs(pt1.XValue- pt2.XValue) < 0.00001)
                return new LJJSPoint(xValue, pt1.YValue);

            double kvalue = (pt1.YValue - pt2.YValue) / (pt1.XValue - pt2.XValue);
            double yvalue = kvalue * (xValue - pt1.XValue) + pt1.YValue;
            return new LJJSPoint(xValue, yvalue);
        }
        public static bool IfInKeDuChi(double xvalue, KeDuChiItem kdc)
        {
            bool revalue = false;
            if (kdc == null)
                return revalue;
            if ((xvalue >= kdc.KMin) && (xvalue <= kdc.KMax))
            {
                revalue = true;
            }
            return revalue;
        }
        private static double _bilichivalue;
        /// <summary>
        /// 构造函数:将比例尺值传入到坐标操作类中
        /// </summary>
        /// <param name="blcval"></param>
        public ZuoBiaoOper(double blcval)
        {
            _bilichivalue = blcval;

        }
        public static LJJSPoint UpdateLRStartPt(int kdcDir, LJJSPoint LRStartPt, double LRWidth)
        {
            if (kdcDir.Equals(-1))
                return new LJJSPoint(LRStartPt.XValue + LRWidth, LRStartPt.YValue);
            return LRStartPt;

        }
        public static double GetJSZongZBValue(double yStart, double JS, double jdStartJs, double biLiChiValue)
        {
            double yvalue = yStart;
            double jsspace = Math.Abs(JS - jdStartJs);
            if (jdStartJs < JS)//所求点在基点下方；
            {
                yvalue = yvalue - jsspace * 1000 * biLiChiValue;
            }
            else
            {
                yvalue = yvalue + jsspace * 1000 * biLiChiValue;
            }
            return yvalue;


        }
        public static double XGetZuoBiaoValue(double xStartPt, KeDuChiItem kdc, double xVal,  double lRWidth)
        {
            double xtmpval;
            double xvalue=xStartPt;
            if (Math.Abs(kdc.KMax - kdc.KMin) > 0)
            {
                xtmpval = (xVal - kdc.KMin) / (kdc.KMax - kdc.KMin) * lRWidth;
                xvalue = xvalue + kdc.KDir * xtmpval;
            }
            return xvalue;
        }
    
        /// <summary>
        /// 根据绘图的基点井深、基点坐标、刻度尺等内容获得绘制点在图幅上的坐标。
        /// </summary>
        /// <param name="jdStartPt">井段起始点坐标</param>
        /// <param name="kdc">绘制点所属的刻度尺</param>
        /// <param name="xVal">绘制点的X值（数据库中）</param>
        /// <param name="yVal">绘制点的Y值即井深（数据库中）</param>
        /// <param name="jdStartJS">基点对应的井深值</param>
        /// <returns></returns>
        public LJJSPoint GetDrawingZuoBiaoPt(LJJSPoint jdStartPt, KeDuChiItem kdc, double xVal, double yVal, double jdStartJS, double lRWidth)
        {
            double xvalue = jdStartPt.XValue;
            double yvalue = jdStartPt.YValue;
            double jsspace = Math.Abs(yVal - jdStartJS);
            if (jdStartJS < yVal)//所求点在基点下方；
            {
                yvalue = yvalue - jsspace * 1000 * _bilichivalue;
            }
            else
            {
                yvalue = yvalue + jsspace * 1000 * _bilichivalue;
            }
            double xtmpval;
            if (Math.Abs(kdc.KMax - kdc.KMin) > 0)
            {


                xtmpval = (xVal - kdc.KMin) / (kdc.KMax - kdc.KMin) * lRWidth;
                xvalue = xvalue + kdc.KDir * xtmpval;
            }
            /**if(xvalue > 5)
            {
                xvalue = 0;
            }**/

            LJJSPoint pt = new LJJSPoint(xvalue, yvalue);
            return pt;
        }

        /// <summary>
        /// 方法:获取纵向等分点的坐标集合
        /// </summary>
        /// <param name="ptStart">起始点坐标</param>
        /// <param name="ZXDirection">参数:等分方向，向下时，ZXDirection为-1，向上时为1</param>
        /// <param name="ZXHeigh">参数:纵向的深度</param>
        /// <param name="dengFenLength">参数:等分长度</param>
        /// <returns></returns>
        public static List<LJJSPoint> GetZongXiangDengFenPtArr(LJJSPoint ptStart, int ZXDirection, double ZXHeigh, double dengFenLength)
        {
            List<LJJSPoint> ptarr = new List<LJJSPoint>();//定义新的点坐标集合
            //起始点赋值为坐标原点
            double xvalue = ptStart.XValue;
            double yvalue = ptStart.YValue;

            double tmpyvalue = ptStart.YValue + ZXDirection * dengFenLength;//临时变量:赋值为第一个等分点的坐标,其点纵坐标为+上第一个等分距离
            double endyvalue = ptStart.YValue + ZXDirection * ZXHeigh;//设置等分线的最大值为起始点坐标+纵向的深度

            //eques方法提示：equals可以比较所有的对象和方法
            if (ZXDirection.Equals(1))//向上等分；
            {
                while (tmpyvalue < endyvalue)
                {
                    LJJSPoint tmppt = new LJJSPoint(xvalue, tmpyvalue);//设置点横坐标为x初始值不变
                    ptarr.Add(tmppt);//坐标集合内添加坐标点
                    tmpyvalue = tmpyvalue + ZXDirection * dengFenLength;//纵坐标继续加上长度集合
                }
            }
            else if (ZXDirection.Equals(-1))//向下等分,过程类似向上等分
            {
                while (tmpyvalue >= endyvalue )
                {
                    LJJSPoint tmppt = new LJJSPoint(xvalue, tmpyvalue);
                    ptarr.Add(tmppt);
                    tmpyvalue = tmpyvalue + ZXDirection * dengFenLength;
                }
            }
            return ptarr;
        }
    }
}
