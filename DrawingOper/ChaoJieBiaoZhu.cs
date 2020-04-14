using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using LJJSCAD.Drawing.Curve;
using System.Drawing;
using DesignEnum;

namespace LJJSCAD.DrawingOper
{
    class ChaoJieBZ
    {
        private double _xValue;
        private double _jS;
        private LJJSPoint _chaoJiePt;
        public LJJSPoint chaoJiePt
        {
            set { _chaoJiePt = value; }
            get { return _chaoJiePt; }
        }
        public double jS
        {
            set { _jS = value; }
            get { return _jS; }
        }
        public double xValue
        {
            set { _xValue = value; }
            get { return _xValue; }
        }
        public ChaoJieBZ(double bzxvalue, LJJSPoint bzPt)
        {
            _xValue = bzxvalue;
            _chaoJiePt = bzPt;
        }
        public void DrawChaoJieBiaoZhu(int KDir)
        {
            if (Math.Abs(_xValue) < 0.00001)
                return;
            AttachmentPoint txtlayout;
            if (KDir.Equals(1))
            {
                txtlayout = AttachmentPoint.MiddleRight;
            }
            else
            {
                txtlayout = AttachmentPoint.MiddleLeft;
            }

            Line.BuildHorToRightBlackSolidLine(_chaoJiePt, -2 * KDir,0,"超界");
            ulong objid = LJJSCAD.Drawing.Text.LJJSText.AddHorCommonText(_xValue.ToString("F1"), new LJJSPoint(_chaoJiePt.XValue - 2.5 * KDir, _chaoJiePt.YValue), Color.Black.ToArgb(),txtlayout,0.5,"Times New Roman");


        }
    }
}
