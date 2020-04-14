using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Util;
using LJJSCAD.DrawingOper;
using LJJSCAD.LJJSDrawing.Impl.DrawItem;
using DesignEnum;


namespace LJJSCAD.Model.Drawing
{
   
    public struct LineItemStruct
    {
        public string LineItemID;
        public string CurveItemShowName;
        public CJQXChaoJie LineItemChaoJie;//测井曲线超界； _CJQXChaoJie = GetChaoJieByText(ht["CJQXChaoJie"].ToString());
        public CJQXLineClass LineItemType;//测井曲线类型:棒线和折线。
        public string LIFromTableName;//测井曲线相关位置等属性来自的表名。
        public string LIFromFieldName;//相关字段的名字；
        public string LineItemUnit;//测井曲线单位。
        public bool KDCIfShow;//是否显示刻度尺;
        public double FirstKDCStartHeigh;//绘图项起始位置的高度，即第一比例尺的位置;
        public double LINameVSKDCHeigh;//曲线项名称距最后一个比例尺的高度；
        public CJQXUnitPosition UnitPosition;//曲线单位的位置;
        public ItemTitlePos LineItemTitlePos;//曲线项名称的位置；
        public string LiSubClass;//曲线项的子类；
        public string JsField;//井深对应的字段;

        public LineItemStruct(CurveItemModel curveItemModel)
        {
            LineItemID = curveItemModel.ID;
            CurveItemShowName=curveItemModel.CJQXShowName;
            LIFromTableName = curveItemModel.CJQXFromTableName.Trim();
            LineItemUnit = curveItemModel.CJQXUnit.Trim();
            LineItemType = EnumUtil.GetEnumByStr(curveItemModel.CJQXlineClass.Trim(),CJQXLineClass.Continus);// CurveItemDesignClass.GetLineClassByText();
        
            KDCIfShow = BoolUtil.GetBoolByBindID(curveItemModel.KDCIfShow, true);
            LIFromFieldName = curveItemModel.CJQXFieldName.Trim();
            JsField = StandardCurveItemBuilder.depth;
            if (!string.IsNullOrEmpty(curveItemModel.JSField.Trim()))
            JsField = curveItemModel.JSField.Trim();

            FirstKDCStartHeigh = StrUtil.StrToDouble(curveItemModel.CJQXHeaderStartheigh.Trim(), "缺少绘图项顶部起始位置数据", "绘图项顶部起始位置数据非数值型");
            LINameVSKDCHeigh = StrUtil.StrToDouble(curveItemModel.QXNameVSKDCHeigh.Trim(), 4, "曲线项名称与宽度尺的距离值为非数值型");


            UnitPosition = EnumUtil.GetEnumByStr(curveItemModel.UnitPosition.Trim(), CJQXUnitPosition.AtRight);
            LineItemTitlePos = EnumUtil.GetEnumByStr(curveItemModel.QXItemTitlePosition, ItemTitlePos.Mid); //ItemOper.GetDrawingItemTitlePos(curveItemModel.QXItemTitlePosition.Trim());
            LineItemChaoJie = EnumUtil.GetEnumByStr(curveItemModel.CJQXChaoJie.Trim(),CJQXChaoJie.BiaoZhu);
            LiSubClass = curveItemModel.LIDISubStyle.Trim();
        }
    }
   
}
