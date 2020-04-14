using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.Model
{
    /// <summary>
    /// 带T2截止值的谱图；
    /// </summary>
    class HCGZItemModel
    {
        public string ID { set; get; }
        public string ItemShowName { set; get; }
        public string Main_GWJSField { set; get; }
        public string Main_YYWZField { set; get; }
        public string Main_T2JZZField { set; get; }
        public string Main_Table { set; get; }
        public string ClosedArea_Field_X { set; get; }
        public string ClosedArea_Feild_Y { set; get; }
        public string ClosedArea_Table { set; get; }
        public string ClosedArea_Field_YYWZ { set; get; }
        public string TitleUint { set; get; }
        public string TitleUnitPosition { set; get; }
        public string SFLTColor { set; get; }
        public string KDLTColor { set; get; }
        public string ItemSubStyle { set; get; }
        public string ItemNameVSKDCHeigh { set; get; }
        public string FirstKDCStartHeigh { set; get; }
        public string TitlePosition { set; get; }
    }
}
