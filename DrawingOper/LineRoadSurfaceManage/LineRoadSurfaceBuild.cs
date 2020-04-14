using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Impl;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.Model;
using LJJSCAD.DrawingElement;
using System.Windows.Forms;
using LJJSCAD.Util;
using LJJSCAD.Drawing.Curve;
using LJJSCAD.CommonData;
using LJJSCAD.Drawing.Figure;
using System.Drawing;
using DesignEnum;
using EnumManage;
namespace LJJSCAD.DrawingOper.LineRoadSurfaceManage
{
    abstract class LineRoadSurface 
    {
        private LineRoadDrawingModel _lineRoadDrawingModel;
        private List<JDStruc> _jdLst;
        private int _jdCount=0;
        protected LJJSPoint _ptStart;
        protected LineRoadDesignClass _lineRoadModel;
        public static int _ZMLdengfenspace = 50;//两条相邻整米线在图像上的间隔数，默认为50mm；
        protected double _secondKDSpace = 1;//次刻度的间距；
        protected double _secondKDLineLen = 2;//次刻度线的长度
        protected double _lineroadwidth = 30;//线道的宽度；
        private float arrowWidth = 2;//线道左侧线为箭头类型时，箭头的宽度；
        private float arrowheight = 4;//线道左侧线为箭头类型时，箭头的高度；

       public abstract void DrawPerJDSecondKD(LJJSPoint ptstart, double jdHeigh, int DirParam);
       public abstract void DrawPerJDZMLine(LJJSPoint ptstart, JDStruc jdStruc);
       public abstract void DrawPerJDJSZMLineBiaoZhu(LJJSPoint biaoZhuPosition, double BiaoZhuContent);

        public LineRoadSurface(LineRoadDrawingModel lineRoadDrawingModel)
        {
            _lineRoadDrawingModel = lineRoadDrawingModel;
            _jdCount = FrameDesign.JdStrLst.Count();
            _ptStart = _lineRoadDrawingModel.PtStart;
            _lineRoadModel = lineRoadDrawingModel.LineRoadStruc;
            _jdLst = lineRoadDrawingModel.LineRoadJdLst;  //2560-2590 2660-2690
            _lineroadwidth = lineRoadDrawingModel.LineRoadStruc.LineRoadWidth;

        }
        public void Build()
        {
            AddDIOutFrame();
            //添加测网；
            if (_lineRoadModel.Cewang.ifAdd)
            {
                AddCeWang();
            }

            //添加次刻度；
            if (_lineRoadModel.IfLeftSecondKD)
            {
                AddLeftSecondKD();
            }
            if (_lineRoadModel.IfRightSecondKD)
            {
                AddRightSecondKD();
            }
            //添加整米线;
            if (_lineRoadModel.IfzhengMiLine)
            {
                AddzhengMiLine();
            }
         

 
        }



        public  void AddDIOutFrame()
        {
         
            double xstart = _ptStart.XValue;
            double ystart = _ptStart.YValue;
            LineRoadDesignClass lineRoadModel = _lineRoadDrawingModel.LineRoadStruc;  //lineroaddrawingmodel.lineroadjdStruct -> 2560-2590,2660-2690

            if (lineRoadModel.LeftLineStyle == LineLeftKind.arrowline)
            {
                Arrow.VerSolidArrowLine(_ptStart, lineRoadModel.LeftLineLength, FrameDesign.PictureFrameLineWidth, DrawCommonData.DirectionDown, Color.Black.ToArgb(),arrowWidth,arrowheight);
             
            }

            for (int i = 0; i < _jdCount; i++) //lineroaddrawingmodel.lineroadjdStruct -> 2560-2590,2660-2690
            {

                LJJSPoint ptstart = new LJJSPoint(xstart, ystart);
                if (lineRoadModel.LeftLineStyle == LineLeftKind.enline)//绘制左侧线
                {
                    Line.BuildVerLine(ptstart, _jdLst[i].JDHeight, FrameDesign.PictureFrameLineWidth, Color.Black.ToArgb(), DrawCommonData.SolidLineTypeName, "", DrawCommonData.DirectionDown);
                  
                }

                ystart = ystart - _jdLst[i].JDHeight - FrameDesign.JdSpace;
            }
        }
        public void AddCeWang()
        {
            double xstart = _ptStart.XValue;
            double ystart = _ptStart.YValue;
            double cespace = GetCeWangSpace();
            for (int i = 0; i < _jdCount; i++)
            {
                LJJSPoint ptstart = new LJJSPoint(xstart, ystart);
                DrawPerJDCewang(ptstart, cespace, _jdLst[i].JDHeight);
                ystart = ystart - _jdLst[i].JDHeight - FrameDesign.JdSpace;
            }
 
        }
        public void AddLeftSecondKD()
        {

            double xstart = _ptStart.XValue; 
            double ystart = _ptStart.YValue;
            for (int i = 0; i < _jdCount; i++)
            {
                LJJSPoint ptstart = new LJJSPoint(xstart, ystart);
                DrawPerJDSecondKD(ptstart, _jdLst[i].JDHeight, 1);
                ystart = ystart - _jdLst[i].JDHeight - FrameDesign.JdSpace;
            }
        }



        public void AddRightSecondKD()
        {
            
            double xstart = _ptStart.XValue + _lineroadwidth;
            double ystart = _ptStart.YValue;
            for (int i = 0; i < _jdCount; i++)
            {
                LJJSPoint ptstart = new LJJSPoint(xstart, ystart);
                DrawPerJDSecondKD(ptstart, _jdLst[i].JDHeight, DrawCommonData.DirectionLeft);
                ystart = ystart - _jdLst[i].JDHeight - FrameDesign.JdSpace;
            }
        }
        public void AddzhengMiLine()
        {

            double xstart = _ptStart.XValue;
            double ystart = _ptStart.YValue;
            for (int i = 0; i < _jdCount; i++)
            {
                LJJSPoint ptstart = new LJJSPoint(xstart, ystart);
                DrawPerJDZMLine(ptstart, _jdLst[i]);   //整米线？ //lineroaddrawingmodel.lineroadjdStruct -> 2560-2590,2660-2690
                ystart = ystart - _jdLst[i].JDHeight - FrameDesign.JdSpace;
            } 
        }  

        private double GetCeWangSpace()
        {
            double cewangspace = 0;
            if (_lineRoadModel.Cewang.cewangstyle.Equals(CeWangStyleEnum.DengCha) && Math.Abs(_lineRoadModel.Cewang.cewangfixlen - 0) > 0.000001)
            {
                cewangspace = _lineRoadModel.Cewang.cewangfixlen;
            }
            else if (_lineRoadModel.Cewang.cewangsepnum != 0 && _lineRoadModel.Cewang.cewangstyle.Equals(CeWangStyleEnum.DengFen))
            {
                cewangspace = _lineRoadModel.LineRoadWidth / _lineRoadModel.Cewang.cewangsepnum;
            }
            else if ( _lineRoadModel.Cewang.ifHeng)//对数测网；
            {
                if (_lineRoadModel.Cewang.cewangstyle.Equals(CeWangStyleEnum.DuiShuFanXiang) || _lineRoadModel.Cewang.cewangstyle.Equals(EnumManage.CeWangStyleEnum.DuiShuZhengXiang))
                {
                    if (_lineRoadModel.Cewang.cewangfixlen > 0)
                        cewangspace = _lineRoadModel.Cewang.cewangfixlen;
                    else
                    {
                        cewangspace = 5;
                        MessageBox.Show("缺少横向测网间距参数，系统给与默认值５");
                    }
                }
            }
            else
            {
                cewangspace = 5;
            }
            return cewangspace;
        }
        /// <summary>
        /// 添加测网的内部线
        /// </summary>
        /// <param name="horSpace">测网横线间距</param>
        private void DrawPerJDCewang(LJJSPoint ptstart, double cewangSpace, double jdHeigh)
        {
            int horlinecount = (int)(jdHeigh / cewangSpace);
            int verlinecount = (int)(_lineRoadModel.LineRoadWidth / cewangSpace);
            LJJSPoint tmpptStart = ptstart;
            LJJSPoint tmpcwstartpt;
            double tmpxstart;
            int dscwDir = 1;
            if (_lineRoadModel.Cewang.cewangstyle.Equals(CeWangStyleEnum.DuiShuZhengXiang) || _lineRoadModel.Cewang.cewangstyle.Equals(CeWangStyleEnum.DuiShuFanXiang))
            {
                if (_lineRoadModel.Cewang.cewangstyle.Equals(CeWangStyleEnum.DuiShuFanXiang))
                {
                    dscwDir = -1;
                    tmpptStart = new LJJSPoint(ptstart.XValue + _lineRoadModel.LineRoadWidth, ptstart.YValue);
                }
                double duishuX = _lineRoadModel.Cewang.duishuminvalue;
                double duibilen = 0;//相对于起点的位移;
                while ((Math.Abs(duibilen)) <= _lineRoadModel.LineRoadWidth)
                {
                    for (int i = 2; i <= 10; i++)
                    {
                        int tencount = DuiShuOper.GetTenCount(duishuX);
                        double AddSpace = Math.Pow(10, tencount);//增加量;
                        duishuX = duishuX + AddSpace;
                        duibilen = dscwDir * _lineRoadModel.Cewang.duishuParam * (Math.Log10(duishuX) - Math.Log10(_lineRoadModel.Cewang.duishuminvalue));
                        if ((Math.Abs(duibilen)) <= _lineRoadModel.LineRoadWidth)
                        {
                            tmpxstart = tmpptStart.XValue + duibilen;
                            tmpcwstartpt = new LJJSPoint(tmpxstart, ptstart.YValue);
                            Line.BuildVerLine(tmpcwstartpt,jdHeigh,FrameDesign.PictureFrameLineWidth,DrawCommonData.BlackColorRGB,DrawCommonData.CeWangLineStyle,"",DrawCommonData.DirectionDown);
                     
                        }
                    }
                }
                if (_lineRoadModel.Cewang.ifHeng)
                {
                    for (int i = 1; i <= horlinecount; i++)
                    {
                        Line.BuildToRightHorLine(new LJJSPoint(ptstart.XValue, ptstart.YValue - cewangSpace * i), _lineRoadModel.LineRoadWidth, FrameDesign.PictureFrameLineWidth, DrawCommonData.BlackColorRGB, DrawCommonData.CeWangLineStyle, "");
                       
                      
                    }
                }
            }
            else
            {
                if (_lineRoadModel.Cewang.ifHeng)
                {
                    for (int i = 1; i <= horlinecount; i++)
                    {
                        Line.BuildToRightHorLine(new LJJSPoint(ptstart.XValue, ptstart.YValue - cewangSpace * i), _lineRoadModel.LineRoadWidth, FrameDesign.PictureFrameLineWidth, DrawCommonData.BlackColorRGB, DrawCommonData.CeWangLineStyle, "");
                    }
                }
                if (_lineRoadModel.Cewang.ifZong)
                {
                    for (int j = 1; j <= verlinecount; j++)
                    {
                        Line.BuildVerLine(new LJJSPoint(ptstart.XValue + cewangSpace * j, ptstart.YValue), jdHeigh, FrameDesign.PictureFrameLineWidth, DrawCommonData.BlackColorRGB, DrawCommonData.CeWangLineStyle, "", DrawCommonData.DirectionDown);
                   
                    }
                }
            }
        }

    }
}
