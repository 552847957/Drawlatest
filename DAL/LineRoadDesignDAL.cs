using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using DBHelper;
using LJJSCAD.CommonData;

namespace LJJSCAD.DAL
{
    class LineRoadDesignDAL
    {/** 啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊
        public static DbDataReader GetDefaultLineRoadDesignDR()
        {
            string sql = "select * from LineRoadDesignDetail a,LineRoadDesign b where a.LineRoadDesignID=b.LineRoadDesignID and b.IsDefault=1";
            var lrReader = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSConfigureDb, DBConfigure.configConStr);
            return lrReader.ExecuteReader(sql);
        }**/

        public static DbDataReader GetDefaultLineRoadDesignDR(string myLineRoadDesignName)
        {
            string sql = "select * from LineRoadDesignDetail a,LineRoadDesign b where a.LineRoadDesignID=b.LineRoadDesignID and b.LineRoadDesignName='"+myLineRoadDesignName+"'";
            var lrReader = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSConfigureDb, DBConfigure.configConStr);
            return lrReader.ExecuteReader(sql);
        }

        public static DbDataReader GetLineRoadDesignDRByID(string lineRoadDesginID)
        {
            string sql = @"select * from LineRoadDesignDetail a,LineRoadDesign b where a.LineRoadDesignID=b.LineRoadDesignID and b.LineRoadDesignID='"+lineRoadDesginID+@"'";
            var lrReader = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSConfigureDb, DBConfigure.configConStr);
            return lrReader.ExecuteReader(sql);
        }
        public static DbDataReader GetDefaultJingShenDesignDR()
        {
            string sql = "select * from JingShenDesign where ifDefault=1";
            var lrReader = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSConfigureDb, DBConfigure.configConStr);
            return lrReader.ExecuteReader(sql);
        }
    }
}
