using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Model;
using System.Collections;
using LJJSCAD.DrawingElement;
using LJJSCAD.CommonData;
using LJJSCAD.LJJSDrawing.Impl;
using LJJSCAD.BlackBoard;

namespace LJJSCAD.DrawingOper
{
    class LineRoadOper 
    {


     
        /// <summary>
        /// 获得线道控制点数据，包括每条线道的井段Lst，线道的宽度等信息。
        /// </summary>
        /// <param name="lineRoadModelLst"></param>
        /// <returns></returns>
        public static List<LineRoadControlData> getLineRoadControlDataLst(List<LineRoadDesignClass> lineRoadModelLst)
        {
            double xLrPtStart=DrawCommonData.xStart;
            double yLrptStart=DrawCommonData.yStart;
            List<LineRoadControlData> lrRoadContorlDataLst = new List<LineRoadControlData>();
            for (int i = 0; i < lineRoadModelLst.Count; i++)
            {
                LJJSPoint lPtStart=new LJJSPoint(xLrPtStart,yLrptStart);
                LineRoadDesignClass tmpLrModel = lineRoadModelLst[i];
                LineRoadControlData tmpLrControlData = new LineRoadControlData();
                tmpLrControlData.LineRoadId = tmpLrModel.LineRoadId;
                tmpLrControlData.LineRoadWidth = tmpLrModel.LineRoadWidth;
                tmpLrControlData.LineRoadJDStructLst = JDOper.GetLineRoadJDLst(lPtStart,tmpLrModel.LineRoadWidth);     //获得2560-2590，2660-2690          
                lrRoadContorlDataLst.Add(tmpLrControlData);
                xLrPtStart = xLrPtStart + tmpLrModel.LineRoadWidth;
            }
            return lrRoadContorlDataLst;   //only depth -> 2560-2590 2660-2690
        }

        public static LineRoadDrawingModel BuildLineRoadDrawingModel(LineRoadDesignClass lineRoadModel, LineRoadControlData lineRoadControlData)
        {
            LineRoadDrawingModel lineRoadDrawingModel = new LineRoadDrawingModel();
            lineRoadDrawingModel.LineRoadStruc = lineRoadModel;
            lineRoadDrawingModel.LineRoadJdLst = lineRoadControlData.LineRoadJDStructLst;
            if (lineRoadControlData.LineRoadJDStructLst.Count() > 0)
            {
                if (null != lineRoadControlData.LineRoadJDStructLst && lineRoadControlData.LineRoadJDStructLst.Count() > 0)

                    lineRoadDrawingModel.PtStart = lineRoadControlData.LineRoadJDStructLst[0].JDPtStart;

            }
            return lineRoadDrawingModel;

        }
               
    }
}
