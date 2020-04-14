using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.DrawingOper;
using LJJSCAD.DataProcess.DrawControlData;
using LJJSCAD.ItemStyleOper;
using LJJSCAD.BlackBoard;

namespace LJJSCAD.WorkDataManage.Interface
{
    abstract class IWorkDataProvider
    {
        private List<JDTopAndBottom> jdBianJieLst = new List<JDTopAndBottom>();

        public void BuildWorkDataHt()
        {

            WorkDataManage.WorkDataDictionary.Clear();


            BuildJDBianJieLst();  //给jdBianJieLst赋值，里面装的是井深的上界和下界
            if (this.jdBianJieLst.Count() > 0)
            { 
              List<DrawItemName> tmp;
              for (int i = 0; i < LineRoadDesign.LineRoadDesginLst.Count(); i++)
              {
                  tmp = LineRoadDesign.LineRoadDesginLst[i].Drawingitems;
                  if (null != tmp && tmp.Count() > 0)
                      for (int j = 0; j < tmp.Count(); j++)
                      {
                          DrawItemName tmpDrawItemName = tmp[j];
                          BuildItemWorkDataTable(jdBianJieLst, tmpDrawItemName);
                      }
              }
            }

        }
        private void BuildJDBianJieLst()
        {
            this.jdBianJieLst.Clear();
            for (int i = 0; i < FrameDesign.JdStrLst.Count(); i++)
            {
                JDTopAndBottom tmp = JDOper.GetJDTopAndBottom(FrameDesign.JdStrLst[i].Trim());
                this.jdBianJieLst.Add(tmp);
            }

        }
        public abstract void BuildItemWorkDataTable(List<JDTopAndBottom> jdBianJieLst,DrawItemName drawItemName);
    


    }
}
