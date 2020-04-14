using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LJJSCAD.LJJSDrawing.Impl.SQLStr
{
    class SQLBuilder
    {
        public static string GetSingleDepthSubSql(double jdTop, double jdBottom,string depthFeildStr)
        {
            string tmpdepth = depthFeildStr.Trim();
           
            string reval = "";
            if (jdTop < jdBottom)
            {
                if (!string.IsNullOrEmpty(tmpdepth))
                    reval = depthFeildStr + ">" + jdTop + " and " + depthFeildStr + "<" + jdBottom;
                else
                    MessageBox.Show("绘图项缺少深度字段设计");
            }
            else
                MessageBox.Show("井段设计有误，井段顶部需要小于底部");

            return reval;
        }
    }
}
