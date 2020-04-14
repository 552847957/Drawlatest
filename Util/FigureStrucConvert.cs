using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VectorDraw.Geometry;
using LJJSCAD.DrawingElement;

namespace LJJSCAD.Util
{
    class FigureStrucConvert
    {
        public static gPoint ConvertLJJSPointToGPoint(LJJSPoint ljjsPt)
        {
            return new gPoint(ljjsPt.XValue,ljjsPt.YValue);
        }
        public static List<gPoint> ConverLJJSPtLstTogPtLst(List<LJJSPoint> ljjsPtLst)
        {
            List<gPoint> gptlst = new List<gPoint>();
            if (null != ljjsPtLst && ljjsPtLst.Count() > 0)
            {
                for (int i = 0; i < ljjsPtLst.Count(); i++)
                {
                    gptlst.Add(new gPoint(ljjsPtLst[i].XValue, ljjsPtLst[i].YValue));

                }
                return gptlst;
            }
            else
            {
                return null;
            }
        }
    }
}
