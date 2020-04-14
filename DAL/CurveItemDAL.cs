using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Windows.Forms;
using LJJSCAD.CommonData;
using DBHelper;
using System.Data;
using System.Collections;
using LJJSCAD.Util;
using DesignEnum;

namespace LJJSCAD.DAL
{
    class CurveItemDAL
    {
        public static DbDataReader GetAllCurveItemConfig()
        {
            ItemDAL itemDal = new ItemDAL(DrawItemStyle.LineItem);
            return itemDal.GetDrawItemDataReader();
        }

        public static List<Hashtable> GetCurveItemPointHt(DataTable curveItemDrawDt,string jdTop, string jdBottom,string jsField, params string[] itemFields)
        {
            List<Hashtable> ptcolList = new List<Hashtable>();
            foreach (DataColumn column in curveItemDrawDt.Columns)
            {
                if (column.ColumnName == "gwjs")
                {

                    column.ColumnName = jsField;
                }
            }

            foreach(string itemField in itemFields)
            {
                if (null != curveItemDrawDt)
                {
                    Hashtable ptcol = new Hashtable();
                    string sqltxt = GetCurveItemJDDataSql(jdTop, jdBottom, itemField, jsField);
                    DataRow[] ptcoldrs = curveItemDrawDt.Select(sqltxt);

                    if (ptcoldrs.Length > 0)
                    {
                        foreach (DataRow dr in ptcoldrs)
                        {
                            string xvalstr = dr[itemField].ToString().Trim();
                            string yvalstr = dr[jsField].ToString().Trim();
                            if (xvalstr != "" && yvalstr != "")
                            {
                                double yval = StrUtil.StrToDouble(yvalstr, 0, "曲线项数据有非数值型");
                                double xval = StrUtil.StrToDouble(xvalstr, 0, "曲线项数据有非数值型");
                                if(itemField == "MSEJ")
                                {
                                    xval = xval / 1000;
                                }
                                if (!ptcol.Contains(yval))
                                    ptcol.Add(yval, xval);
                            }
                        }
                    }
                    ptcolList.Add(ptcol);
                }
            }


            return ptcolList;
        }

        public static Hashtable GetCurveItemPointHt(DataTable curveItemDrawDt,string jdTop, string jdBottom, string itemField,string jsField)
        {
            Hashtable ptcol = new Hashtable();
            foreach(DataColumn column in curveItemDrawDt.Columns)
            {
                if(column.ColumnName=="gwjs")
                {
                 
                    column.ColumnName = jsField;
                }
            }
            if (null != curveItemDrawDt)
            {
                string sqltxt = GetCurveItemJDDataSql(jdTop, jdBottom, itemField, jsField);
                DataRow[] ptcoldrs = curveItemDrawDt.Select(sqltxt);

                if (ptcoldrs.Length > 0)
                {
                    foreach (DataRow dr in ptcoldrs)
                    {
                        string xvalstr = dr[itemField].ToString().Trim();
                        string yvalstr = dr[jsField].ToString().Trim();
                        if (xvalstr != "" && yvalstr != "")
                        {
                            double yval = StrUtil.StrToDouble(yvalstr, 0, "曲线项数据有非数值型");
                            double xval = StrUtil.StrToDouble(xvalstr, 0, "曲线项数据有非数值型");
                            if (!ptcol.Contains(yval))
                                ptcol.Add(yval, xval);
                        }
                    }
                }
            }

            return ptcol;

        }
      
        private static string GetCurveItemJDDataSql(string jdTop, string jdBottom, string itemField, string jsField)
        {

            string restr = "";
            if (!string.IsNullOrEmpty(itemField))
            {

                restr = jsField + ">" + jdTop + " and " + jsField + "<" + jdBottom + @" and " + itemField + @" is not null";
            }
            else
            {
                MessageBox.Show("缺少设计项，请检查设计内容");

            }
            return restr;
        }   
 
    }
}
