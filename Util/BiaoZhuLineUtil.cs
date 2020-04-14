using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using LJJSCAD.Drawing.Curve;
using LJJSCAD.CommonData;

namespace LJJSCAD.Util
{
    class BiaoZhuLineUtil
    {
        /// <summary>
        /// 添加水平标注线；向右为正，向左为负
        /// </summary>
        /// <param name="ptStart"></param>
        /// <param name="lineLen"></param>
        /// <param name="bzWidth">标注点间的距离</param>
        /// <param name="linewidth"></param>
        public static List<ulong> AddHorBZLine(LJJSPoint ptStart, double lineLen, double bzWidth, double linewidth)
        {
            List<ulong> bzlst = new List<ulong>();
            bzlst.Add(Line.BuildCommonHorSolidLineByLayer(ptStart, lineLen, linewidth, DrawCommonData.DirectionRight));
            LJJSPoint ptother = new LJJSPoint(ptStart.XValue + bzWidth, ptStart.YValue);
            bzlst.Add(Line.BuildCommonHorSolidLineByLayer(ptother, lineLen, linewidth, DrawCommonData.DirectionLeft));
            return bzlst;

        }
    }
}
