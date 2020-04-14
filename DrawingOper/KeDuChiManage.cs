using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using LJJSCAD.Model;
using LJJSCAD.DrawingElement;
using LJJSCAD.Util;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.Drawing.Curve;
using System.Windows.Forms;
using LJJSCAD.BlackBoard.LJJSDesignData.Impl;
using LJJSCAD.Drawing.Text;
using DesignEnum;

namespace LJJSCAD.DrawingOper
{
    enum KDCStyle { DengFen, DengCha, DuiShu };//等分，等差，对数；
    class KeDuChiManage
    {
        private static Hashtable m_lineitemkdcht = new Hashtable();//包含了能够支持绘图的刻度尺集合；
        public static Hashtable LineItemKDCHt
        {
            set { m_lineitemkdcht = value; }
            get { return m_lineitemkdcht; }
        }
        /// <summary>
        /// 根据刻度尺的List建立刻度尺的绘图管理结构
        /// </summary>
        public static void CreateKDCManageHt()
        {
            m_lineitemkdcht.Clear();
            foreach (string kdcliname in KeDuChiDesignManage.KDCLineItemName)//循环分析包含了刻度尺设计的每一个曲线项
            {
                List<KeDuChiItem> kdchilist = new List<KeDuChiItem>();

                List<DrawingKedu> dklist = KeDuChiDesignManage.GetDrawKDList(kdcliname);
                foreach (DrawingKedu dk in dklist)
                {
                    kdchilist.Add(new KeDuChiItem(dk));
                }

                m_lineitemkdcht.Add(kdcliname, kdchilist);
            }
        }


    }
    class KeDuChiItem
    {

        private double _kdLineLen = 1;//刻度线的长度
        private double _BZTextSize = 2;//标注字的高度；
        private string _drawItemName;
        private int _KNum;
        private string _KName;//刻度尺的名字;
        private string _KIfTwoEndBZ;
        private string _KIfCenterBZ;
        private string _KUnit;
        private string _KLineStyle;
        private double _kLineWidth;//刻度尺线的宽度；即所对应曲线的宽度；
        private int _KCol;
        private double _KMax;
        private double _KMin;
        private double _KFixedLen;
        private double _KParm;//刻度尺对数系数；
        private int _KDir;//刻度尺的方向
        private KDCStyle _KStyle;
        private int _kSepNum;
        private int _kdPosition = 1;//刻度线的位置，主线上方或下方;
        private string _bzTextStyle;

        public double KMax
        {
            set { _KMax = value; }
            get { return _KMax; }
        }
        public string KLineStyle
        {
            set { _KLineStyle = value; }
            get { return _KLineStyle; }
        }
        public int KCol
        {
            set { _KCol = value; }
            get { return _KCol; }
        }

        public string KName
        {
            set { _KName = value; }
            get { return _KName; }
        }

        public int KDir
        {
            set { _KDir = value; }
            get { return _KDir; }
        }
        public KDCStyle KStyle
        {
            set { _KStyle = value; }
            get { return _KStyle; }
        }

        public double kLineWidth
        {
            set { _kLineWidth = value; }
            get { return _kLineWidth; }
        }
        public double KParm
        {
            set { _KParm = value; }
            get { return _KParm; }
        }
        public double KFixedLen
        {
            set { _KFixedLen = value; }
            get { return _KFixedLen; }
        }
        public double KMin
        {
            set { _KMin = value; }
            get { return _KMin; }
        }
        public int KNum
        {
            set { _KNum = value; }
            get { return _KNum; }
        }

        public KeDuChiItem(DrawingKedu drawingKDC)
        {
            _drawItemName = drawingKDC.KDrawItem.Trim();
            _KCol = StrUtil.StrToInt(drawingKDC.KCol,"未设置刻度尺颜色","刻度尺颜色设计有误");
            _KDir = GetKDCDir(drawingKDC.KDir.Trim());
            _kSepNum = GetIntValue(drawingKDC.KSepNum.Trim(), 1, "刻度尺份数为非数值型");
            _KUnit = drawingKDC.KUnit.Trim();
            _KStyle = GetKDCStyle(drawingKDC.KStyle.Trim());
            _KParm = GetDoubleValue(drawingKDC.KParm.Trim(), -1, "对数系数为非数值型");
            _KMin = GetDoubleValue(drawingKDC.KMin.Trim(), 0, "刻度尺最小值为非数值型");
            _KMax = GetDoubleValue(drawingKDC.KMax.Trim(), 0, "刻度尺最大值为非数值型");
            _KIfCenterBZ = drawingKDC.KIfCenterBZ.Trim();
            _KIfTwoEndBZ = drawingKDC.KIfTwoEndBZ.Trim();
            _KFixedLen = GetDoubleValue(drawingKDC.KFixedLen.Trim(), 0, "刻度尺固定长度值为非数值型");
            _KLineStyle = drawingKDC.KLineStyle.Trim();
            _KNum = GetIntValue(drawingKDC.KNum.Trim(), 0, "刻度尺序号为非数值型");
            _kLineWidth = GetDoubleValue(drawingKDC.KLineWidth.Trim(), 0, "刻度尺所对应曲线的宽度为非数值型");
            _KName = drawingKDC.KName.Trim();
            _bzTextStyle = FrameDesign.ScaleLabelTxtFont;
        }
        public void AddNormalKDCToFigure(LJJSPoint ptStart, LJJSPoint ptEnd)
        {
            double kdclen = Math.Abs(ptEnd.XValue - ptStart.XValue);
            InitOpertion(kdclen);
            AddKDCBzLineToFigure(ptStart, ptEnd);
            AddTwoEndBzToFigure(ptStart, ptEnd);
            AddCenterBzToFigure(ptStart, ptEnd);
            AddMainLineToFigure(ptStart, ptEnd);

        }
        private void InitOpertion(double kdcLen)
        {
            if (_KStyle == KDCStyle.DengCha)
            {
                if (Math.Abs(_KFixedLen) > 0.00001)
                    _kSepNum = (int)Math.Abs((kdcLen / _KFixedLen));
            }
            else if (_KStyle == KDCStyle.DengFen)
            {
                if (_kSepNum != 0)
                    _KFixedLen = kdcLen / _kSepNum;
            }
        }
        private void AddKDCBzLineToFigure(LJJSPoint ptStart, LJJSPoint ptEnd)
        {
            LJJSPoint tmpkdstartpt, tmpkdendpt;
            for (int i = 1; i <= _kSepNum; i++)
            {
                double xvalue = ptStart.XValue + _KFixedLen * _KDir * i;
                double yvalue = ptStart.YValue;
                tmpkdstartpt = new LJJSPoint(xvalue, yvalue);
                tmpkdendpt = new LJJSPoint(xvalue, yvalue + _kdPosition * _kdLineLen);
                Line.BuildCommonSoldLine(tmpkdstartpt, tmpkdendpt,_KCol,_kLineWidth);
            //    ArxCSHelper.AddPolyline2d(tmpkdstartpt, tmpkdendpt, _kLineWidth);

            }
        }
        private void AddTwoEndBzToFigure(LJJSPoint ptStart, LJJSPoint ptEnd)
        {
            if (_KIfTwoEndBZ.Equals("是"))
            {
                if (_kdPosition.Equals(-1))//刻度标注线向下；
                {
                    if (_KDir.Equals(1))// 正向；
                    {
                        LJJSText.AddHorCommonText(_KMin.ToString(), ptStart, _KCol, AttachmentPoint.BottomLeft, _BZTextSize, _bzTextStyle);
                      //  ArxCSHelper.AddMText(_KMin.ToString(), _BZTextSize, ptStart, AttachmentPoint.BottomLeft, _bzTextStyle);
                        LJJSText.AddHorCommonText(_KMax.ToString(), ptEnd, _KCol, AttachmentPoint.BottomRight, _BZTextSize, _bzTextStyle);
                      //  ArxCSHelper.AddMText(_KMax.ToString(), _BZTextSize, ptEnd, AttachmentPoint.BottomRight, _bzTextStyle);
                    }
                    else//反向;
                    {
                        LJJSText.AddHorCommonText(_KMin.ToString(), ptStart, _KCol, AttachmentPoint.BottomRight, _BZTextSize, _bzTextStyle);
                     //   ArxCSHelper.AddMText(_KMin.ToString(), _BZTextSize, ptStart, AttachmentPoint.BottomRight, _bzTextStyle);
                        LJJSText.AddHorCommonText(_KMax.ToString(), ptEnd, _KCol, AttachmentPoint.BottomLeft, _BZTextSize, _bzTextStyle);
                     //   ArxCSHelper.AddMText(_KMax.ToString(), _BZTextSize, ptEnd, AttachmentPoint.BottomLeft, _bzTextStyle);
                    }

                }
                else if (_kdPosition.Equals(1))//刻度标注线向上;
                {
                    LJJSPoint bzptstart;
                    LJJSPoint bzptend;
                    if (_KDir.Equals(1))// 正向；
                    {
                        bzptstart = new LJJSPoint(ptStart.XValue + 0.5, ptStart.YValue+ _kdLineLen);
                        bzptend = new LJJSPoint(ptEnd.XValue - 0.5, ptEnd.YValue+ _kdLineLen);
                        LJJSText.AddHorCommonText(_KMin.ToString(), bzptstart, _KCol, AttachmentPoint.BottomLeft, _BZTextSize, _bzTextStyle);
                      //  ArxCSHelper.AddMText(_KMin.ToString(), _BZTextSize, bzptstart, AttachmentPoint.BottomLeft, _bzTextStyle);
                        LJJSText.AddHorCommonText(_KMax.ToString(), bzptend, _KCol, AttachmentPoint.BottomRight, _BZTextSize, _bzTextStyle);
                     //   ArxCSHelper.AddMText(_KMax.ToString(), _BZTextSize, bzptend, AttachmentPoint.BottomRight, _bzTextStyle);
                    }
                    else//反向;
                    {
                        bzptstart = new LJJSPoint(ptStart.XValue - 0.5, ptStart.YValue+ _kdLineLen);
                        bzptend = new LJJSPoint(ptEnd.XValue + 0.5, ptEnd.YValue+ _kdLineLen);
                        LJJSText.AddHorCommonText(_KMin.ToString(), bzptstart, _KCol, AttachmentPoint.BottomRight, _BZTextSize, _bzTextStyle);
                        LJJSText.AddHorCommonText(_KMax.ToString(), bzptend, _KCol, AttachmentPoint.BottomLeft, _BZTextSize, _bzTextStyle);

                        //ArxCSHelper.AddMText(_KMin.ToString(), _BZTextSize, bzptstart, AttachmentPoint.BottomRight, _bzTextStyle);
                        //ArxCSHelper.AddMText(_KMax.ToString(), _BZTextSize, bzptend, AttachmentPoint.BottomLeft, _bzTextStyle);
                    }

                }

            }

        }
        private void AddCenterBzToFigure(LJJSPoint ptStart, LJJSPoint ptEnd)
        {
            double pervalue = Math.Abs(_KMax - _KMin) / _kSepNum;
            double bzvalue;
            if (_KIfCenterBZ.Equals("是"))
            {
                LJJSPoint tmpbzpt;
                for (int i = 1; i <= _kSepNum - 1; i++)
                {
                    double xvalue = ptStart.XValue + _KFixedLen * _KDir * i;
                    double yvalue = ptStart.YValue;
                    if (_kdPosition.Equals(-1))//刻度标注线向下；
                    {
                        tmpbzpt = new LJJSPoint(xvalue, yvalue);

                    }
                    else if (_kdPosition.Equals(1))//刻度标注线向上;
                    {
                        tmpbzpt = new LJJSPoint(xvalue, yvalue + _kdLineLen);
                    }
                    else
                        tmpbzpt = new LJJSPoint(xvalue, yvalue + _kdLineLen);
                    bzvalue = _KMin + pervalue * i;
                    bzvalue = Math.Round(bzvalue, 2);
                    if (_KDir.Equals(1))// 正向；
                    {
                        if (xvalue < ptEnd.XValue)
                        {
                            LJJSText.AddHorCommonText(bzvalue.ToString(), tmpbzpt, _KCol, AttachmentPoint.BottomCenter, _BZTextSize, _bzTextStyle);
                            
                          //  ArxCSHelper.AddMText(bzvalue.ToString(), _BZTextSize, tmpbzpt, AttachmentPoint.BottomCenter, _bzTextStyle);
                        }
                    }
                    else//反向;
                    {
                        if (xvalue > ptEnd.XValue)
                        {
                            LJJSText.AddHorCommonText(bzvalue.ToString(), tmpbzpt, _KCol, AttachmentPoint.BottomCenter, _BZTextSize, _bzTextStyle);
                         //   ArxCSHelper.AddMText(bzvalue.ToString(), _BZTextSize, tmpbzpt, AttachmentPoint.BottomCenter, _bzTextStyle);
                        }
                    }
                }
            }

        }
        private void AddMainLineToFigure(LJJSPoint ptStart, LJJSPoint ptEnd)
        {
            Line.BuildCommonSoldLine(ptStart, ptEnd, _KCol, _kLineWidth);
        
        }

        private double GetDoubleValue(string doubleStr, double nullValue, string errInfo)
        {
            double revalue = nullValue;
            string doublestr = doubleStr.Trim();
            if (doublestr.Equals(""))
            {
                return revalue;
            }
            try
            {
                revalue = double.Parse(doublestr);

            }
            catch
            {
                MessageBox.Show(errInfo);
            }
            return revalue;
        }
        private int GetIntValue(string intStr, int nullValue, string errInfo)
        {
            int revalue = nullValue;
            string doublestr = intStr.Trim();
            if (doublestr.Equals(""))
            {
                return revalue;
            }
            try
            {
                revalue = int.Parse(intStr);

            }
            catch
            {
                MessageBox.Show(errInfo);
            }
            return revalue;
        }
        private KDCStyle GetKDCStyle(string txtStr)
        {
            string tmpstr = txtStr.Trim();
            KDCStyle returnvale = KDCStyle.DengCha;
            if (tmpstr.Equals("等分"))
                returnvale = KDCStyle.DengFen;
            else if (tmpstr.Equals("等差"))
                returnvale = KDCStyle.DengCha;
            else if (tmpstr.Equals("对数"))
                returnvale = KDCStyle.DuiShu;
            else
                returnvale = KDCStyle.DengCha;
            return returnvale;
        }
        private int GetKDCDir(string txtStr)
        {
            string tmpstr = txtStr.Trim();
            int returnvale = 1;
            if (tmpstr.Equals("正向"))
                returnvale = 1;
            else if (tmpstr.Equals("反向"))
                returnvale = -1;
            else
                returnvale = 1;
            return returnvale;
        }
        /// <summary>
        /// 添加对数刻度尺
        /// </summary>
        /// <param name="ptStart"></param>
        /// <param name="ptEnd"></param>
        public void AddDuiShuKDCToFigure(LJJSPoint ptStart, LJJSPoint ptEnd)
        {
            double kdclen = Math.Abs(ptEnd.XValue - ptStart.XValue);
            AddDuiShuiKDCBZLineToFigure(ptStart, kdclen);

            AddMainLineToFigure(ptStart, ptEnd);

        }
        private void AddDuiShuiKDCBZLineToFigure(LJJSPoint ptStart, double kdclen)
        {
            double tmpxstart = ptStart.XValue;
            double tmpystart = ptStart.YValue;
            double duishuX = _KMin;


            LJJSPoint tmpkdstartpt, tmpkdendpt;

            double duibilen = 0;//相对于起点的位移;


            while ((Math.Abs(duibilen)) <= kdclen)
            {
                for (int i = 2; i <= 10; i++)
                {
                    int tencount = DuiShuOper.GetTenCount(duishuX);
                    double AddSpace = Math.Pow(10, tencount);//增加量;
                    duishuX = duishuX + AddSpace;
                    duibilen = _KDir * _KParm * (Math.Log10(duishuX) - Math.Log10(_KMin));
                    if ((Math.Abs(duibilen)) <= kdclen)
                    {
                        tmpxstart = ptStart.XValue + duibilen;
                        tmpkdstartpt = new LJJSPoint(tmpxstart, tmpystart);
                        tmpkdendpt = new LJJSPoint(tmpxstart, tmpystart + _kdPosition * _kdLineLen);
                        Line.BuildCommonSoldLine(tmpkdstartpt, tmpkdendpt, _KCol,_kLineWidth);
                        //ArxCSHelper.AddPolyline2d(tmpkdstartpt, tmpkdendpt, _kLineWidth);
                        if (i == 10)
                        {
                            LJJSText.AddHorCommonText(duishuX.ToString(), tmpkdendpt, _KCol, AttachmentPoint.BottomCenter, _BZTextSize, _bzTextStyle);
                          //  ArxCSHelper.AddMText(duishuX.ToString(), _BZTextSize, tmpkdendpt, AttachmentPoint.BottomCenter, _bzTextStyle);
                        }
                    }
                }
            }

            LJJSPoint bzptstart;

            if (_KDir.Equals(1))// 正向；
            {
                bzptstart = new LJJSPoint(ptStart.XValue + 0.5, ptStart.YValue+ _kdLineLen);

                LJJSText.AddHorCommonText(_KMin.ToString(), bzptstart, _KCol, AttachmentPoint.BottomLeft, _BZTextSize, _bzTextStyle);
              //  ArxCSHelper.AddMText(_KMin.ToString(), _BZTextSize, bzptstart, AttachmentPoint.BottomLeft, _bzTextStyle);
               
            }
            else//反向;
            {
                bzptstart = new LJJSPoint(ptStart.XValue- 0.5, ptStart.YValue+ _kdLineLen);
                LJJSText.AddHorCommonText(_KMin.ToString(), bzptstart, _KCol, AttachmentPoint.BottomRight, _BZTextSize, _bzTextStyle);
             //   ArxCSHelper.AddMText(_KMin.ToString(), _BZTextSize, bzptstart, AttachmentPoint.BottomRight, _bzTextStyle);
               
            }
        }
    }
}
