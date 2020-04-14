using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.WorkDataManage.Interface;
using System.Data;
using LJJSCAD.BlackBoard.LJJSDesignData.Impl;
using LJJSCAD.Model;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.DAL;
using LJJSCAD.ItemStyleOper;
using DesignEnum;

namespace LJJSCAD.WorkDataManage.Impl
{
    class SQLServerWorkDataProvider : IWorkDataProvider
    {

        public override void BuildItemWorkDataTable(List<JDTopAndBottom> jdBianJieLst, DrawItemName drawItemName)
        {
            //1,获得绘图项来自于的表名；
            object fromTableName = GetItemFromTableName(drawItemName);
            //2，获得绘图数据
            if (drawItemName.ItemStyle.Equals(DrawItemStyle.HCGZItem)||drawItemName.ItemStyle.Equals(DrawItemStyle.NormalPuTuItem))
            //if(true)
            {
                Dictionary<string, string> dicTableName = (Dictionary<string, string>)fromTableName;
                string mainTableName = dicTableName["mainTable"];
                string closedAreaname = dicTableName["closedAreaTable"];
                AddTableToItemWorkDataTable(mainTableName);
                AddTableToItemWorkDataTable(closedAreaname);
            }
            else
            {              
                string fromtablename = fromTableName.ToString();
                AddTableToItemWorkDataTable(fromtablename);            
              
            }
          
        }
        private static void AddTableToItemWorkDataTable(string tableName)
        {
            string tmpTableName = tableName.Trim().ToUpper();
            //string tmpTableName = tableName.Trim();
            if (!WorkDataManage.WorkDataDictionary.Keys.Contains(tmpTableName))
                WorkDataManage.WorkDataDictionary.Add(tmpTableName, GetItemDataTableByName(tmpTableName));
            //此字典中加入的key是 “SCJSSJB_ZDJG”字符串，即表名，value是查询此表获得的dataTable
        }


        public static String invoke_method;
        private static DataTable GetItemDataTableByName(string tableName)
        {
            DataTable dt = null;
            string tn = tableName.Trim();
            if (!string.IsNullOrEmpty(tn))
            {

                //这里访问数据库获得数据
                if (invoke_method.Equals("绘图"))
                {
                    dt = ItemDAL.GetItemWorkDataTable(tn);
                }
                else if (invoke_method.Equals("随钻"))
                {
                    dt = ItemDAL.SuiZuan_GetItemWorkDataTable(tn);
                }

                //dt = ItemDAL.GetItemWorkDataTable(tn);
               
            }
            return dt;
 
        }
        public static object GetItemFromTableName(DrawItemName drawItemName)
        {        
          
            ItemFromTableRead itemFromTableRead = new ItemFromTableRead(drawItemName.DrawItemID);
            object fromTable=itemFromTableRead.ReturnItemInstance(drawItemName.ItemStyle);          
            return fromTable;
        }
    }
}
