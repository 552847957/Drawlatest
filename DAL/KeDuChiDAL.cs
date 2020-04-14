using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using LJJSCAD.CommonData;
using DBHelper;

namespace LJJSCAD.DAL
{
    class KeDuChiDAL
    {
        private string tableName;
        public KeDuChiDAL(string fromTableName)
        {
            tableName = fromTableName;
        }
        public DbDataReader GetKeDuChiDesignByDB(string sqlCondition)
        {
            string sql = "select * from " + tableName +" "+ sqlCondition;
            var drReader = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSConfigureDb, DBConfigure.configConStr);
            return drReader.ExecuteReader(sql);

        }

    }
}
