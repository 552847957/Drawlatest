using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesignEnum;

namespace LJJSCAD.Model.Drawing
{
    class CurveItemTitleClass
    {
        public string showName { set; get; }
        public double firstKDCStartHeigh { set; get; }
        public double showNameVSKDCHeigh { set; get; }
        public bool isKDCShow { set; get; }
        public ItemTitlePos itemTitlePos { set; get; }

        public string curveItemUnit { set; get; }
        public CJQXUnitPosition curveUnitPos { set; get; }

        public CurveItemTitleClass()
        {
 
        }
        public CurveItemTitleClass(string itemshowName,double itemfirstkdcStartHeigh
            ,double nameVSKDCHeigh,bool IsKDCShow,ItemTitlePos ItemTitlePosition
            ,string ItemUnit,CJQXUnitPosition UnitPos)
        {
            this.showName = itemshowName;
            this.firstKDCStartHeigh = itemfirstkdcStartHeigh;
            this.showNameVSKDCHeigh = nameVSKDCHeigh;
            this.isKDCShow = IsKDCShow;
            this.itemTitlePos = ItemTitlePosition;
            this.curveItemUnit = ItemUnit;
            this.curveUnitPos = UnitPos;
 
        }
    }
}
