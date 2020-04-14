using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Drawing.Hatch;
using LJJSCAD.Util;
using LJJSCAD.BlackBoard.DBItemDesign;
using DesignEnum;

namespace LJJSCAD.Model.Drawing
{
    class NormalPuTuDesignClass:ItemDesignClass
    {
        public string Main_GWJSField { set; get; }
        public string Main_YYWZField { set; get; }
        public string Main_TableName { set; get; }
        public string ClosedArea_Field_X { set; get; }
        public string ClosedArea_Feild_Y { set; get; }
        public string ClosedArea_Table { set; get; }
        public string ClosedArea_Field_YYWZ { set; get; }
        public string TitleUint { set; get; }
        public CJQXUnitPosition TitleUnitPosition { set; get; }
        public Dictionary<int, LJJSHatch> HatchDic { set; get; }
        public Dictionary<int, double> FenJieValueDic{ set; get; }
        public double ItemNameVSKDCHeigh { set; get; }
        public double FirstKDCStartHeigh { set; get; }
        public NormalPuTuDesignClass(NormalPuTuItemModel nPTModel)
        {
            this.ItemConstructor(nPTModel.ID
                ,nPTModel.ItemShowName,nPTModel.ItemOrder
                ,nPTModel.ItemSubStyle,nPTModel.TitlePosition);
            this.Main_GWJSField = StrUtil.GetNoNullStr(nPTModel.Main_GWJSField);
            this.Main_YYWZField = StrUtil.GetNoNullStr(nPTModel.Main_YYWZField);
            this.Main_TableName = StrUtil.GetNoNullStr(nPTModel.Main_TableName);
            this.ClosedArea_Table = StrUtil.GetNoNullStr(nPTModel.ClosedArea_Table);
            this.ClosedArea_Field_X = StrUtil.GetNoNullStr(nPTModel.ClosedArea_Field_X);
            this.ClosedArea_Feild_Y = StrUtil.GetNoNullStr(nPTModel.ClosedArea_Feild_Y);
            this.ClosedArea_Field_YYWZ = StrUtil.GetNoNullStr(nPTModel.ClosedArea_Field_YYWZ);


            this.TitleUint = StrUtil.GetNoNullStr(nPTModel.TitleUint);
            this.TitleUnitPosition = EnumUtil.GetEnumByStr(nPTModel.TitleUnitPosition, CJQXUnitPosition.MidBottomPos);

            this.ItemNameVSKDCHeigh = StrUtil.StrToDouble(nPTModel.ItemNameVSKDCHeigh, 5, "非数值型");
            this.FirstKDCStartHeigh = StrUtil.StrToDouble(nPTModel.FirstKDCStartHeigh,3,"非数值型");
            FenJieValueDic = new Dictionary<int, double>(); 
            if (!string.IsNullOrEmpty(nPTModel.FenJieValueSet))
            {
                string[] strarr = nPTModel.FenJieValueSet.Split(CommonDeignValue.FenJiValueSplitter);
                if (strarr.Length > 0)
                {
                    for (int i = 0; i < strarr.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(strarr[i]))
                        {

                            double fenji = StrUtil.StrToDouble(strarr[i], -1, "分级设计非数值");
                            FenJieValueDic.Add(i, fenji);
                        }

                    }
                }
            }
            HatchDic = new Dictionary<int, LJJSHatch>();
            if (!string.IsNullOrEmpty(nPTModel.HatchColorSet))
            {
                string[] strarr = nPTModel.HatchColorSet.Split(CommonDeignValue.HatchSplitter);
                if (strarr.Length > 0)
                {
                    for (int i = 0; i < strarr.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(strarr[i]))
                        {

                            LJJSHatch hatch = new LJJSHatch(strarr[i], bool.TrueString);
                            HatchDic.Add(i, hatch);
                        }

                    }
                }
            }
            
            
 
        }
      
     
    }
}
