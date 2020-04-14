using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesignEnum;

namespace LJJSCAD.ItemStyleOper
{
   abstract class ItemStyleOperFrame
    {
       protected DrawItemName drawItemName;
       public abstract void LineItemOperNoReturn();
       public abstract void TextItemOperNoReturn();
       public abstract void SymbolItemOperNoReturn();
       public abstract void ImageItemOperNoReturn();
       public abstract void HCGZItemOperNoReturn();

       
       public void ItemStyleOperNoReturnTemplate(DrawItemName ItemName)
       {
           drawItemName = ItemName;
           if (ItemName.ItemStyle.Equals(DrawItemStyle.LineItem))
               LineItemOperNoReturn();
           else if (ItemName.ItemStyle.Equals(DrawItemStyle.SymbolItem))
               SymbolItemOperNoReturn();
           else if (ItemName.ItemStyle.Equals(DrawItemStyle.TextItem))
               TextItemOperNoReturn();
           else if (ItemName.ItemStyle.Equals(DrawItemStyle.ImageItem))
               ImageItemOperNoReturn();
           else if (ItemName.ItemStyle.Equals(DrawItemStyle.HCGZItem))
               HCGZItemOperNoReturn();

       }
      
    }
}
