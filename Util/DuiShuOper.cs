using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using LJJSCAD.DrawingOper;
using LJJSCAD.DrawingDesign.Frame;

namespace LJJSCAD.Util
{
    class DuiShuOper
    {
        public static LJJSPoint GetDuiShuPointZB(KeDuChiItem duiShuKDC, LJJSPoint convertPt, JDStruc jdstruc, LJJSPoint lrptstart)
        {

            double Xpt = DuiShuOper.XGetDSZuoBiaoValue(lrptstart.XValue, duiShuKDC.KDir, duiShuKDC.KParm, convertPt.XValue, duiShuKDC.KMin);
            double Ypt = ZuoBiaoOper.GetJSZongZBValue(jdstruc.JDPtStart.YValue, convertPt.YValue, jdstruc.JDtop, FrameDesign.ValueCoordinate);
            return new LJJSPoint(Xpt, Ypt);
        }
        public static double XGetDSZuoBiaoValue(double xStart, int KDir, double duiShuParam, double duiShuXvalue, double duiShuMinValue)
        {
            return xStart + XGetDuiShuVsLen(KDir, duiShuParam, duiShuXvalue, duiShuMinValue);
        }
        public static double XGetDuiShuVsLen(int KDir, double duiShuParam, double duiShuXvalue, double duiShuMinValue)
        {
            double vslen = KDir * duiShuParam * (Math.Log10(duiShuXvalue) - Math.Log10(duiShuMinValue));
            return vslen;
        }
        public static int GetTenCount(double inDoubleValue)
        {

            double tmpvalue = inDoubleValue;

            int i = 0;
            while (tmpvalue > 1.0001)
            {
                tmpvalue = tmpvalue / 10;
                if (tmpvalue < 1)
                {
                    return i;
                }
                i = i + 1;
            }
            while (tmpvalue < 0.9999)
            {
                i = i - 1;
                tmpvalue = tmpvalue * 10;
                if (tmpvalue > 1)
                {
                    return i;
                }
            }
            return i;
        }
    }
}
