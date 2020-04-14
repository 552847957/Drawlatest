using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using LJJSCAD.DrawingDesign.Frame;
using System.Drawing;
using System.Windows.Forms;
using LJJSCAD.Model.Drawing;
using LJJSCAD.Drawing.Text;
using LJJSCAD.BlackBoard.LJJSDesignData.Impl;
using LJJSCAD.Drawing.Manage;
using DesignEnum;
using LJJSCAD.BlackBoard;

namespace LJJSCAD.DrawingOper
{
    class AddItemTitle
    {
        private static double _kdcspace = 4;// 刻度尺之间的距离;
        private static double jbqx_yxhoroffset = 4;//井壁取心的岩性符号平移量;
        private static double jbqx_czhoroffset = 4;//井壁取心的产状符号平移量;
        private static double jbqx_yshoroffset = 16;//井壁取心的颜色内容平移量；
        private static double jbqx_SymbolWidth = 20;//井壁取心符号的宽度；
        private static double SyItemTxtVerSpace = 0;
        private LJJSPoint _lrPtStart;

        public LJJSPoint LrPtStart
        {
            get { return _lrPtStart; }
            set { _lrPtStart = value; }
        }
        private double _lrWidth;

        public double LrWidth
        {
            get { return _lrWidth; }
            set { _lrWidth = value; }
        }
        private string _titleContent;

        public string TitleContent
        {
            get { return _titleContent; }
            set { _titleContent = value; }
        }
        private ItemTitlePos _titlePos;

        public ItemTitlePos TitlePos
        {
            get { return _titlePos; }
            set { _titlePos = value; }
        }
        private string _isShow;

        public string IsShow
        {
            get { return _isShow; }
            set { _isShow = value; }
        }



        public AddItemTitle(LJJSPoint lrPtStart, double lrWidth, string TitleContent, ItemTitlePos ItemTitlePos, string isShow)
        {
            this._lrPtStart = lrPtStart;
            this._lrWidth = lrWidth;
            this._titleContent = TitleContent;
            this._titlePos = ItemTitlePos;
            this._isShow = isShow;
 
        }
 

        public static void AddNormalItemTitleText(LJJSPoint LineRoadptStart, string TitleContent, ItemTitlePos ItemTitlePos, double LrWidth, double HeaderStartheigh, double TxtSpace)
        {
            double yend = LineRoadptStart.YValue + HeaderStartheigh;
            AttachmentPoint poslayout = AttachmentPoint.BottomCenter;//排列方式。居左中右。.默认在中间

            double xvalue;
            //分析线道字的位置
            if (ItemTitlePos.Equals(ItemTitlePos.Left))
            {
                xvalue = LineRoadptStart.XValue + 2;
                poslayout = AttachmentPoint.BottomLeft;
            }
            else if (ItemTitlePos.Equals(ItemTitlePos.Right))
            {
                xvalue = LineRoadptStart.XValue + LrWidth - 2;
                poslayout = AttachmentPoint.BottomRight;
            }
            else//常用为中间
            {
                xvalue = LineRoadptStart.XValue + LrWidth * 0.5;//如果位于中间则有起始点坐标为传入坐标+该线道宽度的一半
            }

            //分解文字的ID并获取填充在题头上的绘图项文字内容
            string[] strarr = TitleContent.Split('_');
            //循环写上字（要将字的下划线去掉
            if (strarr.Length > 0)
            {
                for (int i = strarr.Length; i > 0; i--)
                {
                    LJJSText.AddHorCommonText(strarr[i - 1], new LJJSPoint(xvalue, yend), Color.Black.ToArgb(), poslayout, TextFontManage.ItemName);
                
                    yend = yend + FrameDesign.PictureItemTxtHeight * 1.6 + TxtSpace;
                }
            }
        }

        public static void AddLineItemTitleToFig(LJJSPoint lrPtStart, double lrWidth, CurveItemTitleClass cureveItemTitle)
        {
            AttachmentPoint inserPos = AttachmentPoint.BottomCenter;
            int kdcdirec = 0;
            List<KeDuChiItem> kdcitemlist = (List<KeDuChiItem>)KeDuChiManage.LineItemKDCHt[cureveItemTitle.showName];
          
            double xstart = lrPtStart.XValue;
            double ystart = lrPtStart.YValue +cureveItemTitle.firstKDCStartHeigh;
            double xend = lrPtStart.XValue + lrWidth;
            double yend = ystart;
            double xvalue;
            

            string lineitemid = cureveItemTitle.showName.Replace("_", "");
            lineitemid = lineitemid.Replace(" ", "");
            if (cureveItemTitle.isKDCShow)
            {
                for (int i = 0; i < kdcitemlist.Count; i++)
                {
                    KeDuChiItem kdc = kdcitemlist[i];
                    kdcdirec = kdc.KDir;
                    Layer.CreateLayer(kdc.KName, kdc.KCol, kdc.KLineStyle);
                    Layer.Layer_SetToCurrent(kdc.KName);


                    ystart = ystart + _kdcspace * i;
                    yend = yend + _kdcspace * i;
                    xstart = lrPtStart.XValue;
                    xend = lrPtStart.XValue + lrWidth;
                    if (kdc.KDir.Equals(-1))
                    {
                        xend = lrPtStart.XValue;
                        xstart = lrPtStart.XValue + lrWidth;
                    }
                    LJJSPoint ptstart = new LJJSPoint(xstart, ystart);
                    LJJSPoint ptend = new LJJSPoint(xend, yend);
                    if ((kdc.KStyle == KDCStyle.DengCha) || (kdc.KStyle == KDCStyle.DengFen))
                    {
                        kdc.AddNormalKDCToFigure(ptstart, ptend);
                    }
                    else if (kdc.KStyle == KDCStyle.DuiShu)//对数刻度尺;
                    {
                        kdc.AddDuiShuKDCToFigure(ptstart, ptend);
                    }

                }
            }
            yend = yend +cureveItemTitle.showNameVSKDCHeigh;
            LJJSPoint ptlinameposition;
            if (cureveItemTitle.itemTitlePos.Equals(ItemTitlePos.Left))
            {
                xvalue = lrPtStart.XValue + 0.5;
                inserPos = AttachmentPoint.BottomLeft;
            }
            else if (cureveItemTitle.itemTitlePos.Equals(ItemTitlePos.Right))
            {
                xvalue = lrPtStart.XValue + lrWidth-0.5;
                inserPos = AttachmentPoint.BottomRight;
            }
            else
                xvalue = lrPtStart.XValue + lrWidth * 0.5;
            ptlinameposition = new LJJSPoint(xvalue, yend);

            //单位绘制(绘制该线道的单位)
            if (!string.IsNullOrEmpty(cureveItemTitle.curveItemUnit))                
            {
                LJJSPoint unitpospt;
                CJQXUnitPosition qxunitpos = cureveItemTitle.curveUnitPos;
                if (qxunitpos == CJQXUnitPosition.RightPos)//右侧，绘图项标题右侧
                {
                    int namelen = cureveItemTitle.showName.Length;
                    unitpospt = new LJJSPoint(lrPtStart.XValue + lrWidth * 0.5 + namelen * FrameDesign.PictureItemTxtHeight * 0.6 + 0.8, yend);
                    LJJSText.AddHorCommonText(cureveItemTitle.curveItemUnit, unitpospt, Color.Black.ToArgb(), AttachmentPoint.BottomLeft, FrameDesign.PictureItemTxtHeight * 0.6, FrameDesign.PictureItemFont);

                }
                else if (qxunitpos == CJQXUnitPosition.AtRight)//居右，线道右侧
                {
                    unitpospt = new LJJSPoint(lrPtStart.XValue + lrWidth, yend);
                    LJJSText.AddHorCommonText(cureveItemTitle.curveItemUnit, unitpospt, Color.Black.ToArgb(), AttachmentPoint.BottomRight, FrameDesign.PictureItemTxtHeight * 0.6, FrameDesign.PictureItemFont);

                }
                else
                {
                    unitpospt = new LJJSPoint(lrPtStart.XValue + lrWidth * 0.5, yend - FrameDesign.PictureItemTxtHeight * 1.4);
                    LJJSText.AddHorCommonText(cureveItemTitle.curveItemUnit, unitpospt, Color.Black.ToArgb(), AttachmentPoint.BottomCenter, FrameDesign.PictureItemTxtHeight * 0.6, FrameDesign.PictureItemFont);

                }
            }
            LJJSText.AddHorCommonText(cureveItemTitle.showName, ptlinameposition, Color.Black.ToArgb(), inserPos, TextFontManage.ItemName);
            //  ArxCSHelper.AddMText(liShowName, FrameDesign.PictureItemTxtHeight, ptlinameposition, Color.Black.ToArgb(), AttachmentPoint.BottomCenter, DrawingFrameData._drawitemTextStyle);

            /*     if (lis.LiSubClass.Equals("yqsbhd"))
                 {
                     YSBHDItemDraw._ysbhdirection = 1;
                     if (kdcdirec != 0)
                     {
                         YSBHDItemDraw._ysbhdirection = kdcdirec;
                     }
                     //加载油水饱和度
                     YSBHDItemDraw.LoadYSBHDLt();
                     //完成返回
                     LJJSPoint ysbhdtemp = new LJJSPoint(ptlinameposition.XValue, lis.LINameVSKDCHeigh - 2 * DrawingFrameData._drawitemTextHeigh, 0);//坐标中点
                     double measure = YSBHDItemDraw._lt_YSLineRoad.Count * YSBHDItemDraw.YSBHDTitleHeigh;//总宽度是measure
                     LJJSPoint ysbhdstart = new LJJSPoint(ysbhdtemp.XValue - 0.5 * measure + 0.5 * YSBHDItemDraw.YSBHDTitleHeigh, ysbhdtemp.YValue, 0);//起始点就是最前面那个

                     for (int i = 0; i < YSBHDItemDraw._lt_YSLineRoad.Count; i++)
                     {
                         //绘制名称
                         ArxCSHelper.AddMText(YSBHDItemDraw._lt_YSLineRoad[i].YSBHDName, DrawingFrameData._drawitemTextHeigh
                             , ysbhdstart, AttachmentPoint.MiddleCenter, DrawingFrameData._drawitemTextStyle);
                         //绘制下面的代表线
                         LJJSPoint ptstart = new LJJSPoint(ysbhdstart.XValue - 0.5 * DrawingFrameData._drawitemTextHeigh, ysbhdstart.YValue - DrawingFrameData._drawitemTextHeigh);
                         LJJSPoint ptend = new LJJSPoint(ysbhdstart.XValue + 0.5 * DrawingFrameData._drawitemTextHeigh, ysbhdstart.YValue - DrawingFrameData._drawitemTextHeigh);
                         ArxCSHelper.AddLWPolyline(ptstart, ptend, 0.03, AutoCADColor.Black.ToArgb(), YSBHDItemDraw._lt_YSLineRoad[i].YSBHDLineKind);

                         ysbhdstart = new LJJSPoint(ysbhdstart.XValue + YSBHDItemDraw.YSBHDTitleHeigh, ysbhdstart.YValue, 0);
                     }
                     return;
                 }*/

        }





    }
}
