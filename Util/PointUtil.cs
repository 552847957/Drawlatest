using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;

namespace LJJSCAD.Util
{
    class PointUtil
    {
        /// <summary>
        /// 方法:返回一个右侧坐标点
        /// </summary>
        /// <param name="ptTop">参数:井顶坐标</param>
        /// <param name="ptBottom">参数:井底坐标</param>
        /// <param name="offsetlen">参数:差距长度??</param>
        /// <param name="lRWidth">参数:线道宽度</param>
        /// <returns></returns>
        public static LJJSPoint GetRightInsertTxtPt(LJJSPoint ptTop, LJJSPoint ptBottom, double offsetlen, double lineRoadWidth)
        {
            return new LJJSPoint(ptTop.XValue + lineRoadWidth - offsetlen, (ptTop.YValue + ptBottom.YValue) * 0.5);//右侧需要加一个线道宽度
        }
        /// <summary>
        /// 方法:返回一个左侧坐标点
        /// </summary>
        /// <param name="ptTop">参数:井顶坐标</param>
        /// <param name="ptBottom">参数:井底坐标</param>
        /// <param name="offSetLen">参数:差距长度?</param>
        /// <returns></returns>
        public static LJJSPoint GetLeftInsertTxtPt(LJJSPoint ptTop, LJJSPoint ptBottom, double offSetLen)
        {
            return new LJJSPoint(ptTop.XValue + offSetLen, (ptTop.YValue + ptBottom.YValue) * 0.5);//左侧不需要加一个线道宽度
        }
    }
}
