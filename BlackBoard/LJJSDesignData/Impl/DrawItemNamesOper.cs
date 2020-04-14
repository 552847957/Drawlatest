using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.ItemStyleOper;
using System.Collections;
using LJJSCAD.Model;
using LJJSCAD.CommonData;
using DesignEnum;
namespace LJJSCAD.BlackBoard.LJJSDesignData.Impl
{
    class AllDrawItemNamesBuild : AllItemBuilder
    {


        protected override void CurveItemBuild()
        {
            if (null != CurveItemDesignManage.CurveItemDesignHt && CurveItemDesignManage.CurveItemDesignHt.Count > 0)
            {
                foreach (DictionaryEntry de in CurveItemDesignManage.CurveItemDesignHt)
                {
                    CurveItemModel curmodel = (CurveItemModel)de.Value;
                    DrawItemName drawItemName = new DrawItemName();
                    drawItemName.DrawItemID = curmodel.ID.Trim();
                    drawItemName.DrawItemShowName = curmodel.CJQXShowName;
                    drawItemName.ItemStyle = DrawItemStyle.LineItem;
                    drawItemName.ItemSubStyle = curmodel.LIDISubStyle;
                    DrawItemNamesManage.DrawItemNamesList.Add(drawItemName);

                }
               
            }
        }

        protected override void TextItemBuild()
        {
            if (null != TextItemDesignManage.TextItemDesignManageHt && TextItemDesignManage.TextItemDesignManageHt.Count > 0)
            {
                foreach (DictionaryEntry de in TextItemDesignManage.TextItemDesignManageHt)
                {
                    TextItemModel curmodel = (TextItemModel)de.Value;
                    DrawItemName drawItemName = new DrawItemName();
                    drawItemName.DrawItemID = curmodel.ID.Trim();
                    drawItemName.DrawItemShowName = curmodel.TxtItemName;
                    drawItemName.ItemStyle = DrawItemStyle.TextItem;
                    drawItemName.ItemSubStyle = curmodel.TxtDiSubStyle;
                    DrawItemNamesManage.DrawItemNamesList.Add(drawItemName);

                }

            }

        }

        protected override void SymbolItemBuild()
        {
           
            if (null != SymbolItemDesignManage.SymbolItemDesignManageHt && SymbolItemDesignManage.SymbolItemDesignManageHt.Count > 0)
            {
                foreach (DictionaryEntry de in SymbolItemDesignManage.SymbolItemDesignManageHt)
                {
                    SymbolItemModel curmodel = (SymbolItemModel)de.Value;
                    DrawItemName drawItemName = new DrawItemName();
                    drawItemName.DrawItemID = curmodel.ID.Trim();
                    drawItemName.DrawItemShowName = curmodel.ItemName;
                    drawItemName.ItemStyle = DrawItemStyle.SymbolItem;
                    drawItemName.ItemSubStyle = curmodel.SymDISubStyle;
                    DrawItemNamesManage.DrawItemNamesList.Add(drawItemName);

                }

            }

        }

        protected override void ImageItemBuild()
        {
            //ImageItemModel imag = (ImageItemModel)ImageItemDesignManage.ImageItemDesignManageHt[_iD];
            if (null != ImageItemDesignManage.ImageItemDesignManageHt && ImageItemDesignManage.ImageItemDesignManageHt.Count > 0)
            {
                foreach (DictionaryEntry de in ImageItemDesignManage.ImageItemDesignManageHt)
                {
                    ImageItemModel curmodel = (ImageItemModel)de.Value;
                    DrawItemName drawItemName = new DrawItemName();
                    drawItemName.DrawItemID = curmodel.ID.Trim();
                    drawItemName.DrawItemShowName = curmodel.ItemName;
                    drawItemName.ItemStyle = DrawItemStyle.ImageItem;
                    drawItemName.ItemSubStyle = curmodel.ImageItemSubStyle;
                    DrawItemNamesManage.DrawItemNamesList.Add(drawItemName);

                }

            }

        }

        protected override void HCGZItemBuild()
        {
            //HCGZItemModel hcitem = (HCGZItemModel)HCGZItemDesignManage.hcgzItemManageHt[_iD];
            if (null != HCGZItemDesignManage.hcgzItemManageHt && HCGZItemDesignManage.hcgzItemManageHt.Count > 0)
            {
                foreach (DictionaryEntry de in HCGZItemDesignManage.hcgzItemManageHt)
                {
                    HCGZItemModel curmodel = (HCGZItemModel)de.Value;
                    DrawItemName drawItemName = new DrawItemName();
                    drawItemName.DrawItemID = curmodel.ID.Trim();
                    drawItemName.DrawItemShowName = curmodel.ItemShowName;
                    drawItemName.ItemStyle = DrawItemStyle.HCGZItem;
                    drawItemName.ItemSubStyle = curmodel.ItemSubStyle;
                    DrawItemNamesManage.DrawItemNamesList.Add(drawItemName);

                }

            }

        }

        protected override void MultiHatchCurveItemBuild()
        {
            if (null != MultiHatchCurveDesignManage.MultiHatchCurveDesignManageHt && MultiHatchCurveDesignManage.MultiHatchCurveDesignManageHt.Count > 0)
            {
                foreach (DictionaryEntry de in MultiHatchCurveDesignManage.MultiHatchCurveDesignManageHt)
                {
                    MultiHatchCurveItemModel curmodel = (MultiHatchCurveItemModel)de.Value;
                    DrawItemName drawItemName = new DrawItemName();
                    drawItemName.DrawItemID = curmodel.ID.Trim();
                    drawItemName.DrawItemShowName = curmodel.ItemShowName;
                    drawItemName.ItemStyle = DrawItemStyle.MultiHatchCurveItem;
                    drawItemName.ItemSubStyle = DrawCommonData.StandardStyle;
                    DrawItemNamesManage.DrawItemNamesList.Add(drawItemName);

                }

            }
        }

        protected override void HatchRectItemBuild()
        {
            if (null != HatchRectDesignManage.HatchRectDesignManageHt && HatchRectDesignManage.HatchRectDesignManageHt.Count > 0)
            {
                foreach (DictionaryEntry de in HatchRectDesignManage.HatchRectDesignManageHt)
                {
                    HatchRectItemModel curmodel = (HatchRectItemModel)de.Value;
                    DrawItemName drawItemName = new DrawItemName();
                    drawItemName.DrawItemID = curmodel.ID.Trim();
                    drawItemName.DrawItemShowName = curmodel.ItemShowName;
                    drawItemName.ItemStyle = DrawItemStyle.HatchRectItem;
                    drawItemName.ItemSubStyle = curmodel.ItemSubStyle;
                    DrawItemNamesManage.DrawItemNamesList.Add(drawItemName);

                }

            }
        }

        protected override void NormalPuTuItemBuild()
        {
            if (null != NormalPuTuDesignManage.NormalPuTuDesignManageHt && NormalPuTuDesignManage.NormalPuTuDesignManageHt.Count > 0)
            {
                foreach (DictionaryEntry de in NormalPuTuDesignManage.NormalPuTuDesignManageHt)
                {
                    NormalPuTuItemModel curmodel = (NormalPuTuItemModel)de.Value;
                    DrawItemName drawItemName = new DrawItemName();
                    drawItemName.DrawItemID = curmodel.ID.Trim();
                    drawItemName.DrawItemShowName = curmodel.ItemShowName;
                    drawItemName.ItemStyle = DrawItemStyle.NormalPuTuItem;
                    drawItemName.ItemSubStyle = curmodel.ItemSubStyle;
                    DrawItemNamesManage.DrawItemNamesList.Add(drawItemName);

                }

            }
        }

        protected override void CurveHasHatchItemBuild()
        {
            if (null != CurveHasHatchDesignManage.CurveHasHatchDesignManageHt && CurveHasHatchDesignManage.CurveHasHatchDesignManageHt.Count > 0)
            {
                foreach (DictionaryEntry de in CurveHasHatchDesignManage.CurveHasHatchDesignManageHt)
                {
                    CurveHasHatchItemModel curmodel = (CurveHasHatchItemModel)de.Value;
                    DrawItemName drawItemName = new DrawItemName();
                    drawItemName.DrawItemID = curmodel.ID.Trim();
                    drawItemName.DrawItemShowName = curmodel.CurveShowName;
                    drawItemName.ItemStyle = DrawItemStyle.CurveHasHatchItem;
                    drawItemName.ItemSubStyle = curmodel.ItemSubStyle;
                    DrawItemNamesManage.DrawItemNamesList.Add(drawItemName);

                }

            }
        }
    }
}
