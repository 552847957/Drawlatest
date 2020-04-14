using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using LJJSCAD.CommonData;
using LJJSCAD.Util;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.DrawingElement;
using LJJSCAD.LJJSDrawing.Impl.DrawItem;

namespace LJJSCAD.DrawingOper
{
    class JDOper
    {      
        /// <summary>
        /// 方法:通过比例尺值和井段的上端和下端获得井段的高度,仅仅在添加静态类中用到
        /// </summary>
        /// <param name="_hStart"></param>
        /// <param name="_hEnd"></param>
        /// <returns></returns>
        public static double GetJDDrawingHeight(double _hStart, double _hEnd, double _coordinateValue)
        {
            double jdheight = 0;
            jdheight = Math.Abs(_hStart - _hEnd);//取井段差异的绝对值
            jdheight = jdheight * 1000 * _coordinateValue;//取值井段实际深度
            return jdheight;
        }
        public static List<JDStruc> GetLineRoadJDLst(LJJSPoint lrPtStart,double lrWidth)
        {
            List<JDStruc> tmpJdStrucLst = new List<JDStruc>();
            double xStart=lrPtStart.XValue;
            double yStart=lrPtStart.YValue;            
            if (FrameDesign.JdStrLst != null)
            {
                for (int i = 0; i < FrameDesign.JdStrLst.Count(); i++)
                {
                    LJJSPoint jdPtStart = new LJJSPoint(xStart, yStart);
                   
                    
                    string tmpJdStr = FrameDesign.JdStrLst[i];
                    JDStruc tmpJdStruc = GetJDStruc(jdPtStart,lrWidth,tmpJdStr);
                    tmpJdStrucLst.Add(tmpJdStruc);
                    yStart =DrawCommonData.DirectionDown*(Math.Abs(yStart) + tmpJdStruc.JDHeight + FrameDesign.JdSpace);
                }
            }
            return tmpJdStrucLst; //2560-2590 2660-2690
        }
        public static JDTopAndBottom GetJDTopAndBottom(string jdStr)
        {
            JDTopAndBottom jdtb = new JDTopAndBottom();
            jdtb.JDTop = -1;
            jdtb.JDBottm = -1;
            if (!string.IsNullOrEmpty(jdStr))
            {
                string[] jdarr = jdStr.Split(DrawCommonData.jdSplitter);
                if (jdarr.Count() > 1)
                {
                    jdtb.JDTop = StrUtil.StrToDouble(jdarr[0], "井段起始为空，请检查井段设计", "井段起始为非数值型，请检查井段设计");
                    jdtb.JDBottm = StrUtil.StrToDouble(jdarr[1], "井段终止为空，请检查井段设计", "井段终止为非数值型，请检查井段设计");
                  
                }
            }
            return jdtb;
        }
        public static JDStruc GetJDStruc(LJJSPoint jdPtStart,double lrWidth,string jdStr)
        {
            JDStruc jdStruc = new JDStruc(); 
            if (!string.IsNullOrEmpty(jdStr)&&jdPtStart!=null)
            {
                jdStruc.JDPtStart = jdPtStart;
                string[] jdarr = jdStr.Split(DrawCommonData.jdSplitter);
                if (jdarr.Count() > 1)
                {
                    jdStruc.JDtop = StrUtil.StrToDouble(jdarr[0],"井段起始为空，请检查井段设计","井段起始为非数值型，请检查井段设计");
                    jdStruc.JDBottom = StrUtil.StrToDouble(jdarr[1], "井段终止为空，请检查井段设计", "井段终止为非数值型，请检查井段设计");
                    jdStruc.JDHeight=JDOper.GetJDDrawingHeight(jdStruc.JDtop, jdStruc.JDBottom, FrameDesign.ValueCoordinate);//井深就是差值乘以比例尺
                }
            }
            return jdStruc;
        }
    }
}
