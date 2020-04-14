using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using DBHelper;
using LJJSCAD.CommonData;
using System.Windows.Forms;

namespace LJJSCAD.DAL
{
    class FrameModelDAL
    {
        private string frameModelTbName;
        public FrameModelDAL(string tableName)
        {
            frameModelTbName = tableName;
        }
        public  DbDataReader GetDefaultFrameModel(string conditionStr)
        {
            DbDataReader frameDr=null;
            string sql = "select * from " + frameModelTbName + " " + conditionStr;
            try
            {
                IDataAccess data = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSConfigureDb, DBConfigure.configConStr);
                frameDr = data.ExecuteReader(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
            return frameDr;

        }
    }
}
