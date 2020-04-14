using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Collections;
using LJJSCAD.Model;
using System.Reflection;

namespace LJJSCAD.BlackBoard.LJJSDesignData.ModelOper
{
    class LineRoadModelManage
    {
        public static List<LineRoadModel> GetLineRoadDesignModelLst(DbDataReader lineRoadDesignDR)
        {
            List<LineRoadModel> lrmodelht = new List<LineRoadModel>();
            if (null != lineRoadDesignDR && lineRoadDesignDR.HasRows)
            {
                try
                {
                    while (lineRoadDesignDR.Read())
                    {
                        LineRoadModel tmplrmodel = new LineRoadModel();

                        Type type = tmplrmodel.GetType();
                        foreach (PropertyInfo p in type.GetProperties())
                        {

                            string proName = p.Name.Trim();
                            Type pt = p.PropertyType;
                            object proValue;
                            if (pt.Equals(typeof(bool)))
                            {
                                proValue = lineRoadDesignDR[proName];
                            }
                            else
                                proValue = lineRoadDesignDR[proName].ToString().Trim();



                            type.GetProperty(proName).SetValue(tmplrmodel, proValue, null);

                        }
                        lrmodelht.Add(tmplrmodel);//将表中的主键作为key值，将对象赋予到哈希表中
                    }
                }
                catch
                {
                    lineRoadDesignDR.Close();
                }
                lineRoadDesignDR.Close();

            }

            return lrmodelht;
        }
        public static JingShenModel GetJingShenModel(DbDataReader lineRoadDesignDR)        
        {
             JingShenModel jsmodel=new JingShenModel();
            if (null != lineRoadDesignDR && lineRoadDesignDR.HasRows)
            {
                try
                {
                    while (lineRoadDesignDR.Read())
                    {                       

                        Type type = jsmodel.GetType();
                        foreach (PropertyInfo p in type.GetProperties())
                        {

                            string proName = p.Name.Trim();
                            string proValue = lineRoadDesignDR[proName].ToString().Trim();

                            type.GetProperty(proName).SetValue(jsmodel, proValue, null);

                        }
                       
                    }
                }
                catch
                {
                    lineRoadDesignDR.Close();
                }
                lineRoadDesignDR.Close();

            }

            return jsmodel;

        }
        public static List<LineRoadDesignClass> ConvertLRModelLstToLineRoadDesignLst(List<LineRoadModel> lrModelLst)
        {
            List<LineRoadDesignClass> lrstruclst=new List<LineRoadDesignClass>();
            if (null != lrModelLst && lrModelLst.Count() > 0)
            {
                for (int i = 0; i < lrModelLst.Count(); i++)
                {
                    LineRoadDesignClass tmpLineRoadDesignStruc = new LineRoadDesignClass(lrModelLst[i]);
                    lrstruclst.Add(tmpLineRoadDesignStruc);
                }
            }
            return lrstruclst;
        }
      
    }
}
