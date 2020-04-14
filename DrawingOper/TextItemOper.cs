using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using LJJSCAD.Drawing.Curve;
using LJJSCAD.CommonData;

namespace LJJSCAD.DrawingOper
{
    class TextItemOper
    {
        /// <summary>
        /// 绘制双平行线
        /// </summary>
        /// <param name="topZbPt"></param>
        /// <param name="bottomZbPt"></param>
        public static void AddTiOutFrameParelLine(LJJSPoint topZbPt, LJJSPoint bottomZbPt,double textItemOffset,double txtOutFrameWidth)
        {
            //根据坐标顶部和外框线框画出一个外框
            Line.BuildCommonHorSolidLineByLayer(new LJJSPoint(topZbPt.XValue + textItemOffset, topZbPt.YValue), txtOutFrameWidth, 0,DrawCommonData.DirectionRight);
           // CommonDrawing.AddContinusHorLine(new LJJSPoint(topZbPt.XValue + textItemOffset, topZbPt.YValue), txtOutFrameWidth, 0);
            //根据坐标底部点和外框线框画出一个外框
            Line.BuildCommonHorSolidLineByLayer(new LJJSPoint(bottomZbPt.XValue + textItemOffset, bottomZbPt.YValue), txtOutFrameWidth, 0, DrawCommonData.DirectionRight);
         //   CommonDrawing.AddContinusHorLine(new LJJSPoint(bottomZbPt.XValue + textItemOffset, bottomZbPt.YValue), txtOutFrameWidth, 0);
        }
    }
}
