using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using LJJSCAD.DAL;
using System.Data.Common;
using System.Reflection;
using DesignEnum;

namespace LJJSCAD.BlackBoard.LJJSDesignData.Interface
{
    abstract  class DesignDataManage<T>
    {

        public abstract T GetItemDrawStrucByID(string ItemID);
        public Hashtable GetItemStrucHtByDB(DrawItemStyle drawItemStyle)
        {
            Hashtable itemStrucHt = new Hashtable();        
            ItemDAL itemDal = new ItemDAL(drawItemStyle);
            DbDataReader itemDr= itemDal.GetDrawItemDataReader();

            if (itemDr.HasRows)
            {

                while (itemDr.Read())
                {
                    DrawItemFactory drawItemFactory = new DrawItemFactory();
                    object drawItemObj = drawItemFactory.ReturnItemInstance(drawItemStyle);
                    Type type = drawItemObj.GetType();
                    foreach (PropertyInfo p in type.GetProperties())
                    {
                        string proName = p.Name.Trim();
                        Type pt = p.PropertyType;
                        object proValue;
                        if (pt.Equals(typeof(bool)))
                        {
                            proValue = itemDr[proName];
                        }
                        else                        
                        proValue = itemDr[proName].ToString().Trim();
                        type.GetProperty(proName).SetValue(drawItemObj, proValue, null);
                        
                    }

                    try
                    {
                        itemStrucHt.Add(itemDr["ID"], drawItemObj);//将表中的主键作为key值，将对象赋予到哈希表中
                    }
                    catch
                    {
                        continue;
                    }

                }
            }
            itemDr.Close();
            return itemStrucHt;
        }
        
    }
}
