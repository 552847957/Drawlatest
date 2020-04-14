using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DesignEnum;

namespace LJJSCAD.ItemStyleOper
{
    /// <summary>
    /// 读取各个绘图项的工作数据，也就是绘制曲线的生产数据；
    /// </summary>
    class ItemWorkDataTableRead:ItemStyleOperFrameHasReturn
    {
        private string _itemID;

        public string ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }
        public ItemWorkDataTableRead(string itemID)
        {
            this.ItemID = itemID;
        }
        public override object LineItemOper()
        {
            //1,根据ID获得需要操作的数据表；
            ItemFromTableRead itemFromTable = new ItemFromTableRead(ItemID);
            string tablename=itemFromTable.ReturnItemInstance(DrawItemStyle.LineItem).ToString();
            //2,根据表名从workdataManage获取相应的表；
            return WorkDataManage.WorkDataManage.FindWorkDataTableByName(tablename);
         
        }

        public override object TextItemOper()
        {
            //1,根据ID获得需要操作的数据表；
            ItemFromTableRead itemFromTable = new ItemFromTableRead(ItemID);
            string tablename = itemFromTable.ReturnItemInstance(DrawItemStyle.TextItem).ToString();
            //2,根据表名从workdataManage获取相应的表；
            return WorkDataManage.WorkDataManage.FindWorkDataTableByName(tablename);
        }

        public override object SymbolItemOper()
        {
            //1,根据ID获得需要操作的数据表；
            ItemFromTableRead itemFromTable = new ItemFromTableRead(ItemID);
            string tablename = itemFromTable.ReturnItemInstance(DrawItemStyle.SymbolItem).ToString();
            //2,根据表名从workdataManage获取相应的表；
            return WorkDataManage.WorkDataManage.FindWorkDataTableByName(tablename);
        }

        public override object ImageItemOper()
        {
            //1,根据ID获得需要操作的数据表；
            ItemFromTableRead itemFromTable = new ItemFromTableRead(ItemID);
            string tablename = itemFromTable.ReturnItemInstance(DrawItemStyle.ImageItem).ToString();
            //2,根据表名从workdataManage获取相应的表；
            return WorkDataManage.WorkDataManage.FindWorkDataTableByName(tablename);
        }

        public override object HCGZItemOper()
        {
            Dictionary<string, DataTable> hcgzWorkDataDic=new Dictionary<string,DataTable>();
            //1,根据ID获得需要操作的数据表；
            ItemFromTableRead itemFromTable = new ItemFromTableRead(ItemID);
            Dictionary<string,string> tablenamedic =(Dictionary<string,string>)itemFromTable.ReturnItemInstance(DrawItemStyle.HCGZItem);
            hcgzWorkDataDic.Add("mainTable", WorkDataManage.WorkDataManage.FindWorkDataTableByName(tablenamedic["mainTable"]));
            hcgzWorkDataDic.Add("closedAreaTable", WorkDataManage.WorkDataManage.FindWorkDataTableByName(tablenamedic["closedAreaTable"]));
            return hcgzWorkDataDic;
        }

        public override object MultiHatchCurveItemOper()
        {
            return GetItemTable(DrawItemStyle.MultiHatchCurveItem);
        }

        public override object HatchRectItemOper()
        {
            return GetItemTable(DrawItemStyle.HatchRectItem);
        }

        private DataTable GetItemTable( DrawItemStyle itemStyle)
        {         
            ItemFromTableRead itemFrom = new ItemFromTableRead(ItemID);
            string tablename = itemFrom.ReturnItemInstance(itemStyle).ToString();
            return WorkDataManage.WorkDataManage.FindWorkDataTableByName(tablename);
        }

        public override object NormalPuTuItemOper()
        {
            Dictionary<string, DataTable> WorkDataDic = new Dictionary<string, DataTable>();
            //1,根据ID获得需要操作的数据表；
            ItemFromTableRead itemFromTable = new ItemFromTableRead(ItemID);
            Dictionary<string, string> tablenamedic = (Dictionary<string, string>)itemFromTable.ReturnItemInstance(DrawItemStyle.NormalPuTuItem);
            WorkDataDic.Add("mainTable", WorkDataManage.WorkDataManage.FindWorkDataTableByName(tablenamedic["mainTable"]));
            WorkDataDic.Add("closedAreaTable", WorkDataManage.WorkDataManage.FindWorkDataTableByName(tablenamedic["closedAreaTable"]));
            return WorkDataDic;
        }

        public override object CurveHasHatchItemOper()
        {
            Dictionary<string, DataTable> curHasHatchDataDic = new Dictionary<string, DataTable>();
            ItemFromTableRead itemFrom = new ItemFromTableRead(ItemID);
            string tablename = itemFrom.ReturnItemInstance(DrawItemStyle.CurveHasHatchItem).ToString();
            return WorkDataManage.WorkDataManage.FindWorkDataTableByName(tablename);

        }
    }
}
