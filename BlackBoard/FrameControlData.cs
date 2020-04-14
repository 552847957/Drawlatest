using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using LJJSCAD.Model;

namespace LJJSCAD.DataProcess.DrawControlData
{
    /// <summary>
    /// 绘图控制数据,包括线道的各个控制点;
    /// </summary>
    class FrameControlData
    {
        private static List<LineRoadControlData> _lineRoadControlLst;

        public static List<LineRoadControlData> LineRoadControlLst
        {
            get { return FrameControlData._lineRoadControlLst; }
            set { FrameControlData._lineRoadControlLst = value; }
        }
        private static List<LineRoadDesignClass> _lineRoadModelLst;

        public static List<LineRoadDesignClass> LineRoadModelLst
        {
            get { return FrameControlData._lineRoadModelLst; }
            set { FrameControlData._lineRoadModelLst = value; }
        }
        /// <summary>
        /// 获得主框架的高度;
        /// </summary>
        /// <returns></returns>
        public static double GetMainFrameHeight()
        {
            double mainFrameHeight = 0;
            int lineRoadCount=_lineRoadControlLst.Count();
            if (_lineRoadControlLst != null && lineRoadCount > 0)
            {
                JDStruc lastJdStruc = LineRoadControlLst[lineRoadCount-1].LineRoadJDStructLst.Last();
                mainFrameHeight = Math.Abs(lastJdStruc.JDPtStart.YValue) + lastJdStruc.JDHeight;
            }
            return mainFrameHeight;
        }
        /// <summary>
        /// 获得主框架的宽度
        /// </summary>
        /// <returns></returns>
        public static double GetMainFrameWidth()
        {
            double mainFrameWidth = 0;
            if (_lineRoadControlLst != null && _lineRoadControlLst.Count() > 0)
            {
                for (int i = 0; i < _lineRoadControlLst.Count(); i++)
                {
                    mainFrameWidth = mainFrameWidth + _lineRoadControlLst[i].LineRoadWidth;
                }
            }
            return mainFrameWidth;
        }

    }
}
