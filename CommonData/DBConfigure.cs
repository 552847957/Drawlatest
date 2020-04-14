using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBHelper;
using System.Configuration;

namespace LJJSCAD.CommonData
{
    class DBConfigure
    {
        public static EnumDatabaseType LJJSConfigureDb = EnumDatabaseType.SqlServer;
        public static EnumDatabaseType LJJSDrawDb = EnumDatabaseType.SqlServer;
        public static EnumDatabaseType LJJSOracleDrawDb = EnumDatabaseType.Oracle;
        public static string configConStr = ConfigurationManager.ConnectionStrings["configConStr"].ToString();//读连接字符串 
        public static string drawConStr = ConfigurationManager.ConnectionStrings["drawConStr"].ToString();//读连接字符串 
   //     public static string oracConstr = ConfigurationManager.ConnectionStrings["oradrawConStr"].ToString();
    }
}
