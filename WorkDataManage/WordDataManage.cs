using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace LJJSCAD.WorkDataManage
{
    public class WorkDataManage
    {
        private static Dictionary<string,DataTable> _workDataDictionary=new Dictionary<string,DataTable>();
        //此字典中加入的key是 “SCJSSJB_ZDJG”字符串，即表名，value是查询此表获得的dataTable

        public static Dictionary<string, DataTable> WorkDataDictionary
        {
            get { return WorkDataManage._workDataDictionary; }
            set { WorkDataManage._workDataDictionary = value; }
        }

        public static void BuildWorkDataHt()
        {
            WorkDataProviderFactory.GetWorkDataProvider().BuildWorkDataHt();
            

        }
        public static DataTable FindWorkDataTableByName(string tableName)
        {
            string tmp = tableName.Trim().ToUpper();
            if (WorkDataDictionary.Keys.Contains(tmp))
                return WorkDataDictionary[tmp];
            else
                return null;
        }
    

    }
}
