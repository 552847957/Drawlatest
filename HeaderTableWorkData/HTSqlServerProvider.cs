using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using LJJSCAD.DAL;
using System.Data;
using System.Collections;

namespace LJJSCAD.HeaderTableWorkData
{
    class HTSqlServerProvider:IHTWorkDataProvider
    {
        public override void InitHtWorkDataDic()
        {
            this.htWorkDataDic.Clear();
            List<string> tmpFieldLst = new List<string>();
            DbDataReader dr = HeaderTableDAL.GetHeaderTableDataReader();
            DataTable columnTable = dr.GetSchemaTable();
          
            if (null != dr)
            {
                foreach (DataRow drf in columnTable.Rows)
                {
                    tmpFieldLst.Add(drf["ColumnName"].ToString());
                }
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        for (int i = 0; i < tmpFieldLst.Count(); i++)
                        {
                            string tmpfield = tmpFieldLst[i];
                            string tmpvalue = dr[tmpfield].ToString();
                            tmpfield = tmpfield.ToLower();
                            if (!htWorkDataDic.ContainsKey(tmpfield))
                                htWorkDataDic.Add(tmpfield, tmpvalue);

                        }

                    }
                }
            }
               
            

        }
    }
}
