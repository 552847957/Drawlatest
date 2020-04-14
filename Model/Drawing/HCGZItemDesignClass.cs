using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Util;
using LJJSCAD.DrawingOper;
using DesignEnum;

namespace LJJSCAD.Model.Drawing
{
    class HCGZItemDesignClass
    {
        public string ID { set; get; }
        public string ItemShowName { set; get; }
        public string Main_GWJSField { set; get; }
        public string Main_YYWZField { set; get; }
        public string Main_T2JZZField { set; get; }
        public string ClosedArea_Field_X { set; get; }
        public string ClosedArea_Feild_Y { set; get; }
        public string ClosedArea_Table { set; get; }
        public string ClosedArea_Field_YYWZ { set; get; }
        public string TitleUint { set; get; }
        public CJQXUnitPosition TitleUnitPosition { set; get; }
        public int SFLTColor { set; get; }
        public int KDLTColor { set; get; }
        public string ItemSubStyle { set; get; }
        public string Main_Table { set; get; }
        public double  ItemNameVSKDCHeigh { set; get; }
        public double  FirstKDCStartHeigh { set; get; }
        public ItemTitlePos TitlePosition{set;get;}

        public HCGZItemDesignClass(HCGZItemModel hcgzItemModel)
        {
            this.ID = hcgzItemModel.ID;
            this.ItemShowName = hcgzItemModel.ItemShowName;
            this.KDLTColor = StrUtil.StrToInt(hcgzItemModel.KDLTColor, 0, "可动流体颜色为非整数型");
            this.SFLTColor = StrUtil.StrToInt(hcgzItemModel.SFLTColor, 0, "束缚流体颜色为非整数型");
            this.TitleUnitPosition = EnumUtil.GetEnumByStr(hcgzItemModel.TitleUnitPosition.Trim(),CJQXUnitPosition.AtRight);
            this.TitleUint = hcgzItemModel.TitleUint;
            this.Main_GWJSField = hcgzItemModel.Main_GWJSField.Trim();
            this.Main_T2JZZField = StrUtil.StrTestNULL( hcgzItemModel.Main_T2JZZField.Trim(), "T2截止值未指定映射字段");
            this.Main_YYWZField = StrUtil.StrTestNULL(hcgzItemModel.Main_YYWZField, "岩样位置未指定映射字段");
            this.ClosedArea_Feild_Y = StrUtil.StrTestNULL(hcgzItemModel.ClosedArea_Feild_Y, "封闭区域纵向未设置映射字段");
            this.ClosedArea_Field_X = StrUtil.StrTestNULL(hcgzItemModel.ClosedArea_Field_X, "时间");
            this.ClosedArea_Field_YYWZ = StrUtil.StrTestNULL(hcgzItemModel.ClosedArea_Field_YYWZ, "封闭区域岩样位置未设置映射字段");
            this.ClosedArea_Table = StrUtil.StrTestNULL(hcgzItemModel.ClosedArea_Table, "封闭区域未设置映射表");
            this.Main_Table = StrUtil.StrTestNULL(hcgzItemModel.Main_Table, "主表未设置映射表");

            ItemNameVSKDCHeigh = StrUtil.StrToDouble(hcgzItemModel.ItemNameVSKDCHeigh, 8,"刻度尺与绘图项名称间距为非数值型");
            FirstKDCStartHeigh = StrUtil.StrToDouble(hcgzItemModel.FirstKDCStartHeigh, 2, "刻度尺起始高度为非数值型");
            TitlePosition = EnumUtil.GetEnumByStr(hcgzItemModel.TitlePosition, ItemTitlePos.Mid); //ItemOper.GetDrawingItemTitlePos(hcgzItemModel.TitlePosition);
        }
    
    }
}
