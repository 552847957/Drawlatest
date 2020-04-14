using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesignEnum;

namespace LJJSCAD.DrawingElement
{
    class LegendDesignStruc
    {
        //图例设计
         public   LegendPosStyle  LegendPos{set;get;} //图例设计位置
         public LegendDrawStyle LegendDrawStyle { set; get; } //图例设计类型
         public int LegendColumnNum { set; get; } //图例设计列数
         public double LegendUnitHeigh { set; get; } //图例设计单位高度
         public string LegendTbAndField { set; get; } //获得图例查询所涉及的表和字段；
         public bool IfAddLegend { set; get; } 
  

    }
}
