using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;

namespace LJJSCAD.Util
{
    class Vector2
    {

        public double x, y;
        public void set(double x, double y) { this.x = x; this.y = y; }
        public void set(LJJSPoint p) { x = p.XValue; y = p.YValue; }
    };
    class CurveItemUtil
    {
        //用四个点表示两条线段，如果线段没有焦点（直线有相交），返回0 ，如果有，则返回1.
        //如果相交，那么焦点的值会存入InterPt.如果父直线不相交，则返回-1
        //如果输入的A，B为同一点，则返回-2；

        public static int segIntersect(LJJSPoint A, LJJSPoint B, LJJSPoint C, LJJSPoint D, ref LJJSPoint InterPtr)
        {
            double t, u;
            int result;
            Vector2 b = new Vector2();
            b.set(B.XValue - A.XValue, B.YValue - A.YValue);
            if (Math.Abs(b.x) < 0.0000000001 && Math.Abs(b.y) < 0.00000000001)
                return -2;
            Vector2 c = new Vector2();
            c.set(C.XValue - A.XValue, C.YValue - A.YValue);
            Vector2 d = new Vector2();
            d.set(D.XValue - C.XValue, D.YValue - C.YValue);
            if (Math.Abs(d.x) < 0.0000000001 && Math.Abs(d.y) < 0.0000000001)
                return -3;
            result = SystemOfLinearEquationOfTwo(b, c, d, out t, out u);
            switch (result)
            {
                case 1:
                    //both t or u are ok,here we use t
                    InterPtr = new LJJSPoint();
                    InterPtr.XValue = A.XValue + t * b.x;
                    InterPtr.YValue = A.YValue + t * b.y;
                    break;
                case 0:
                    //we also calculate the intersect point
                    InterPtr = new LJJSPoint();
                    InterPtr.XValue = A.XValue + t * b.x;
                    InterPtr.YValue = A.YValue + t * b.y;
                    break;
                default:
                    break;
            }

            return result;
        }
        //求解二元一次方程组
        public static int SystemOfLinearEquationOfTwo(Vector2 b, Vector2 c, Vector2 d, out double t, out double u)
        {
            u = -9999;
            t = -9999;
            //判断向量a和d是否平行,先归一化向量
            Vector2 unia = new Vector2();
            Vector2 unid = new Vector2();
            unia.x = b.x / (Math.Sqrt(b.x * b.x + b.y * b.y)); //分母比不为0，因为如果为0就是0向量了，而这在实际情况没有意义
            unia.y = b.y / (Math.Sqrt(b.x * b.x + b.y * b.y));
            unid.x = d.x / (Math.Sqrt(d.x * d.x + d.y * d.y));
            unid.y = d.y / (Math.Sqrt(d.x * d.x + d.y * d.y));
            if (unia.x == unid.x && unia.y == unid.y)
            {

                return -1; //没有交点，线段平行,有无数的解
            }

            //计算u
            //a.x==0和a.y==0不会同时出现
            if (b.x == 0)// 无需判断if(a.x==0 && d.x!=0)，因为如果a.x,d.x都为0，必然是平行的情况 
            {
                u = -c.x / d.x;
            }
            else if (b.y == 0)// && d.y!=0)
            {
                u = -c.y / d.y;
            }
            else if (b.x != 0 && b.y != 0)
            {
                //方程组1两边同时除以a.x
                //方程组2两边同时除以a.y
                //另方程组相等即:c.x/a.x+d.x/a.x *u = c.y/a.y+d.y*u /a.y;
                u = (c.y / b.y - c.x / b.x) / (d.x / b.x - d.y / b.y);
            }

            //计算t
            //d.x==0 && d.y==0是不会同时出现的，因为(0,0)向量在这里没有意义
            if (d.x == 0)// && a.x!=0)
            {
                t = c.x / b.x;
            }
            else if (d.y == 0)//&& a.y!=0)
            {
                t = c.y / b.y;
            }
            else if (d.x != 0 && d.y != 0)
            {
                //计算t
                //方程组1两边同时除以d.x
                //方程组两边同时除以d.y
                // 同上得:a.x/d.x*t-c.x/d.x=a.y/d.y*t-c.y/d.y
                t = (c.x / d.x - c.y / d.y) / (b.x / d.x - b.y / d.y);
            }

            //如果线段有交点，那么t和u必然介于0和1之间
            if (0 <= t && t <= 1 && 0 <= u && u <= 1)
            {
                return 1;//线段有交点
            }
            else if (t < 0 || t > 1 || u < 0 || u > 1)
            {
                return 0;//线段没有交点，但是线段所在的直线有交点
            }
            return -9999;
        }

    }
}
