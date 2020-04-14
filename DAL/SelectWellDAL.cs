using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Windows.Forms;
using LJJSCAD.CommonData;
using DBHelper;
using System.Data;

namespace LJJSCAD.DAL
{
    class SelectWellDAL
    {
        public static DbDataReader GetSelectWellDataReader()
        {
            string sqltxt = "select distinct jh ,JB,HTDW from " + MyTableManage.DrawingJHTbName;
            var wellReader = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSDrawDb, DBConfigure.drawConStr);
            return wellReader.ExecuteReader(sqltxt);
        }
        public static DataTable GetSelectWellDataTable()
        {
            string sqltxt = "select distinct jh as 井号,JB as 井别,HTDW as 单位 from " + MyTableManage.DrawingJHTbName;
            var wellReader = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSDrawDb, DBConfigure.drawConStr);
            DataTable ret;
            try
            {
               ret = wellReader.GetTable(sqltxt);
            }
            catch(Exception e)
            {
                return null;
            }
            
            return ret;
        }
    }
}
