using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using LJJSCAD.CommonData;
using DBHelper;

namespace LJJSCAD.DAL
{
    class SymbolCodeDAL
    {
        public static DbDataReader GetAllSymbolCode()
        {
            string sqltxt = "select * from " + MyTableManage.SymbolCodeTbName;
            IDataAccess data = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSConfigureDb, DBConfigure.configConStr);
            DbDataReader symbolCodeReader = data.ExecuteReader(sqltxt);
            return symbolCodeReader;
        }
    }
}
