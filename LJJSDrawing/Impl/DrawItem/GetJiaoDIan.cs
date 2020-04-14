using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using System.Windows.Forms;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem
{
    public static class GetJiaoDIan
    {
        public static LJJSPoint getjiaoDian(LJJSPoint A1, LJJSPoint A2, LJJSPoint B1, LJJSPoint B2)
        {
            if (A1.XValue == A2.XValue)
            {
                if (B1.XValue != B2.XValue)
                {
                    double c1 = (B1.YValue - B2.YValue) / (B1.XValue - B2.XValue);
                    double d1 = (B1.XValue * B2.YValue - B2.XValue * B1.YValue) / (B1.XValue - B2.XValue);


                    double x1 = A1.XValue;
                    double y1 = c1 * x1 + d1;
                    return new LJJSPoint(x1, y1);
                }
                else
                {
                    return new LJJSPoint(A1.XValue, A1.YValue);
                }
            }
            if (B1.XValue == B2.XValue)
            {
                if (A1.XValue != A2.XValue)
                {
                    double a1 = (A1.YValue - A2.YValue) / (A1.XValue - A2.XValue);
                    double b1 = (A1.XValue * A2.YValue - A2.XValue * A1.YValue) / (A1.XValue - A2.XValue);


                    double x1 = B1.XValue;
                    double y1 = a1 * x1 + b1;
                    return new LJJSPoint(x1, y1);
                }
                else
                {
                    return new LJJSPoint(A1.XValue, A1.YValue);
                }
            }
            double a = (A1.YValue-A2.YValue)/(A1.XValue-A2.XValue);
            double c = (B1.YValue - B2.YValue) / (B1.XValue - B2.XValue);

            double b = (A1.XValue * A2.YValue - A2.XValue * A1.YValue)/(A1.XValue-A2.XValue);
            double d = (B1.XValue * B2.YValue - B2.XValue * B1.YValue) / (B1.XValue - B2.XValue);

            double x = (d - b) / (a - c);
            double y = (x * a) + b;

            if (double.IsInfinity(x) || double.IsInfinity(y) || double.IsNaN(x) || double.IsNaN(y))
            {
                MessageBox.Show("!!!!!!!!!!!!!");
            }
            return new LJJSPoint(x, y);
        }
    }
}
