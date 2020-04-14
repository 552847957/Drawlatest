using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Impl;
using LJJSCAD.Drawing.Curve;
using LJJSCAD.DrawingElement;
using LJJSCAD.BlackBoard;
using LJJSCAD.Util;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.Model.Drawing;
using LJJSCAD.Drawing.Text;
using LJJSCAD.CommonData;
using DesignEnum;
namespace LJJSCAD.DrawingOper.LineRoadSurfaceManage
{
    class JSLineRoadSurfaceBuild : LJJSCAD.DrawingOper.LineRoadSurfaceManage.LineRoadSurface

    {
        JingShenDesignClass jingShenDesignStruc = LineRoadDesign.JingShenDesign;
        private bool ifAddBiaoZhu = false;
        public JSLineRoadSurfaceBuild(LineRoadDrawingModel lineRoadDrawingModel)
            : base(lineRoadDrawingModel)
        { 
        }
        public override void DrawPerJDSecondKD(DrawingElement.LJJSPoint ptstart, double jdHeigh, int DirParam)
        {
            double secondKDLenth = 2;
            int secondkdcount =(int)Math.Abs(Math.Ceiling(jdHeigh)/_secondKDSpace);
            for (int i = 1; i < secondkdcount; i++)
            {
                Line.BuildHorToRightBlackSolidLine(new LJJSPoint(ptstart.XValue, ptstart.YValue - _secondKDSpace * i), DirParam * secondKDLenth, 0, "");

            }
        }

        public override void DrawPerJDZMLine(DrawingElement.LJJSPoint ptstart, JDStruc jdStruc) //lineroaddrawingmodel.lineroadjdStruct -> 2560-2590,2660-2690  jdstruct
        {
            LJJSPoint tmpptstart = ptstart;
            double tmplineroadwidth = _lineroadwidth;
            
            int jstop = (int)jdStruc.JDtop;
            double mainKDSpace=((jingShenDesignStruc.MainKDSpace * FrameDesign.YCoordinate) / (FrameDesign.XCoordinate * 1000));//主刻度间隔的长度，求出对应的实际井深长度；例如，设计5mm，对应实际井深为所求值；

            double minusVal=Math.Abs(jdStruc.JDtop-jstop); 

            //here!!!
            if (jingShenDesignStruc.isLeftMainKDShow)//绘制左侧主刻度
            {
               AddJSMainKDLine( jdStruc, tmpptstart, jingShenDesignStruc, jstop, mainKDSpace, minusVal);
                

            }
            if (jingShenDesignStruc.isRightMainKDShow)//绘制右侧主刻度
            {
                tmpptstart = new LJJSPoint(ptstart.XValue + _lineroadwidth - jingShenDesignStruc.MainKDLength, ptstart.YValue);
                AddJSMainKDLine(jdStruc, tmpptstart, jingShenDesignStruc, jstop, mainKDSpace, minusVal);
         
               
 
            }
              
         
          
        }

        private double AddJSMainKDLine(JDStruc jdStruc, LJJSPoint tmpptstart, JingShenDesignClass jingShenDesignStruc, int jstop, double mainKDSpace, double minusVal)
        {
            double minMainKDPtJs;
            double biaoZhuJS;
            if (minusVal < mainKDSpace)
            {
                minMainKDPtJs = jstop + mainKDSpace;
            }
            else
            {
                minMainKDPtJs = jstop + 1;
            }
            biaoZhuJS = minMainKDPtJs;
            List<LJJSPoint> dengfenptarr=new List<LJJSPoint>();

            LJJSPoint minMainKDPt = ZuoBiaoOper.GetJSZuoBiaoPt(tmpptstart, minMainKDPtJs, jdStruc.JDtop, FrameDesign.ValueCoordinate);
            dengfenptarr.Add(minMainKDPt);
            dengfenptarr.AddRange(ZuoBiaoOper.GetZongXiangDengFenPtArr(minMainKDPt, -1, jdStruc.JDHeight - Math.Abs(minMainKDPt.YValue - tmpptstart.YValue), jingShenDesignStruc.MainKDSpace));
        
            for (int i = 0; i < dengfenptarr.Count();i++ )
            {
                //1,绘制主刻度线；
                Line.BuildHorToRightBlackSolidLine(dengfenptarr[i], jingShenDesignStruc.MainKDLength, _lineRoadModel.ZhengMiLineWidth, "");

                //2,添加井深标注；
                int tmp=i%jingShenDesignStruc.BiaoZhuSpace;
                if(tmp.Equals(0))
                    DrawPerJDJSZMLineBiaoZhu(dengfenptarr[i], biaoZhuJS);   //画右侧刻度的井深depth标注
                biaoZhuJS = biaoZhuJS + mainKDSpace;
            }
            ifAddBiaoZhu = true;
            return minMainKDPtJs;
        }

     

        public override void DrawPerJDJSZMLineBiaoZhu(DrawingElement.LJJSPoint biaoZhuPosition,double BiaoZhuContent)
        {
          //  new LJJSPoint(dengfenptarr[i].XValue+jingShenDesignStruc.MainKDLength,dengfenptarr[i].YValue)
           //f (ifAddBiaoZhu)   ////？？？ nanikole?
                //return
            /**if(ifAddBiaoZhu == true)
            {
                return;     //me ! me! me!
            }**/

            

            if (this._lineRoadModel.IfLeftSecondKD && this._lineRoadModel.IfRightSecondKD)//左右都标有刻度；此时，标注中间位置；
            {
                
                LJJSPoint insPoint = new LJJSPoint(this._ptStart.XValue+0.5*this._lineroadwidth, biaoZhuPosition.YValue);
                //???
                LJJSText.AddHorCommonText(BiaoZhuContent.ToString(), insPoint, DrawCommonData.BlackColorRGB, AttachmentPoint.BottomCenter, jingShenDesignStruc.BZTxtHeigh, jingShenDesignStruc.BZTxtFont);
                
            }
            else if(this._lineRoadModel.IfLeftSecondKD)//左侧有次刻度，此时标注左侧；  //每次断点都走这里
            {
                LJJSPoint insPoint = new LJJSPoint(this._ptStart.XValue + jingShenDesignStruc.MainKDLength, biaoZhuPosition.YValue);
                LJJSText.AddHorCommonText(BiaoZhuContent.ToString(), insPoint, DrawCommonData.BlackColorRGB, AttachmentPoint.BottomLeft, jingShenDesignStruc.BZTxtHeigh, jingShenDesignStruc.BZTxtFont);
            }
            else if (this._lineRoadModel.IfRightSecondKD)//右侧有次刻度，此时标注右侧；
            {
                LJJSPoint insPoint = new LJJSPoint(this._ptStart.XValue +  this._lineroadwidth-jingShenDesignStruc.MainKDLength, biaoZhuPosition.YValue);
                LJJSText.AddHorCommonText(BiaoZhuContent.ToString(), insPoint, DrawCommonData.BlackColorRGB, AttachmentPoint.BottomRight, jingShenDesignStruc.BZTxtHeigh, jingShenDesignStruc.BZTxtFont);
            }

        }
    }
}
