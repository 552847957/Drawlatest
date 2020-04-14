using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using LJJSCAD.CommonData;
using LJJSCAD.BlackBoard.DrawingHeaderTable;
using DBHelper;
using LJJSCAD.DrawingDesign.Frame;

namespace LJJSCAD.DAL
{
    class HeaderTableDAL
    {
        public static DbDataReader GetHeaderTableDataReader()
        {            
            string sqltxt = "select distinct * from " + HeaderTableDataManage.DrawingHeaderTbName + @" where jh = '" + FrameDesign.JH + @"'";
            var htReader = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSDrawDb, DBConfigure.drawConStr);
            return htReader.ExecuteReader(sqltxt);
        }
    }
}
