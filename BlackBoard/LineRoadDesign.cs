using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Model;
using LJJSCAD.Model.Drawing;

namespace LJJSCAD.BlackBoard
{
    class LineRoadDesign
    {
        /// <summary>
        /// 整体控制绘图的线道设计，绘图时只读取该变量来控制线道绘制；
        /// </summary>
        private static List<LineRoadDesignClass> lineRoadDesginLst = new List<LineRoadDesignClass>();//线道设计链表，保存了当前作图的线道设计；
        public static List<LineRoadDesignClass> LineRoadDesginLst
        {
            get { return lineRoadDesginLst; }
            set { lineRoadDesginLst = value; }
        }
        /// <summary>
        ///整体控制绘图的井深线道设计，绘图时只读取该变量来控制井深线道绘制； 
        /// </summary>
        private static JingShenDesignClass jingShenDesign = null;
        public static JingShenDesignClass JingShenDesign
        {
            get { return LineRoadDesign.jingShenDesign; }
            set { LineRoadDesign.jingShenDesign = value; }
        }
        public static LineRoadDesignClass GetLineRoadDesignStrucById(string lineRoadId)
        {
            if (string.IsNullOrEmpty(lineRoadId))
                return null;
            string tmpid = lineRoadId.Trim().ToLower();
           // lineRoadDesginLst[3]._lineroadname = "基值线";
            if (lineRoadDesginLst.Count > 0)
            {
                for (int i = 0; i < lineRoadDesginLst.Count; i++)
                {
                    string lrtmpid = lineRoadDesginLst[i].LineRoadId.Trim().ToLower();
                    if (tmpid == lrtmpid)
                    {
                        return lineRoadDesginLst[i];
                    }
                        
                    if(            tmpid.Contains("2020")==true  &&  lrtmpid.Contains("2020") ==true                  )
                    {

                        //基值线 写死
                       return lineRoadDesginLst[i];
                    }
                }
                return null;
            }
            else
                return null;
        }
        public static bool UpdateLineRoadDesignStruc(LineRoadDesignClass updateLineRoadDesignStruc)
        {
            if (null == updateLineRoadDesignStruc)
                return false; 
            if (string.IsNullOrEmpty(updateLineRoadDesignStruc.LineRoadId))
                return false;
            string tmpid = updateLineRoadDesignStruc.LineRoadId.Trim().ToLower();
            if (lineRoadDesginLst.Count > 0)
            {
                for (int i = 0; i < lineRoadDesginLst.Count; i++)
                {
                    string lrtmpid = lineRoadDesginLst[i].LineRoadId.Trim().ToLower();
                    if (tmpid == lrtmpid)
                    {
                        lineRoadDesginLst[i] = updateLineRoadDesignStruc;
                        return true;
                    }
                }
             
              
            }          
           return false;
 
        }
        public static void DeleteLRDesignStrucByID(string id)
        {
            LineRoadDesignClass tmp = GetLineRoadDesignStrucById(id);
            bool rel= LineRoadDesginLst.Remove(tmp);
        }
    }
}
