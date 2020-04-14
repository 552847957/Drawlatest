using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Model;

namespace LJJSCAD.Util
{
    class IndexNums
    {
        LJJSEntities db = new LJJSEntities();
        public string createIndexNum(string str)
        {
            string sql = "EXECUTE dbo.GetSeqNum @p0";
            Type t = typeof(string);
            var s = db.Database.SqlQuery(t, sql, new object[] { str }).Cast<string>().First();
            return s;
        }
    }
}
