using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.Model
{
    /// <summary>
    /// 普通的谱图绘图项；
    /// </summary>
    class NormalPuTuItemModel
    {
        public string ID { set; get; }
        public string ItemOrder { set; get; }
        public string ItemShowName { set; get; }
        public string Main_GWJSField { set; get; }
        public string Main_YYWZField { set; get; }      
        public string Main_TableName { set; get; }
        public string ClosedArea_Field_X { set; get; }
        public string ClosedArea_Feild_Y { set; get; }
        public string ClosedArea_Table { set; get; }
        public string ClosedArea_Field_YYWZ { set; get; }
        public string TitleUint { set; get; }
        public string TitleUnitPosition { set; get; }
        public string HatchColorSet { set; get; }
        public string FenJieValueSet { set; get; }       
        public string ItemSubStyle { set; get; }
        public string ItemNameVSKDCHeigh { set; get; }
        public string FirstKDCStartHeigh { set; get; }
        public string TitlePosition { set; get; }
    }
}
