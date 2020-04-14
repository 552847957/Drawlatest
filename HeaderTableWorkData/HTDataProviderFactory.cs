using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.BlackBoard;
using DesignEnum;

namespace LJJSCAD.HeaderTableWorkData
{
    class HTDataProviderFactory
    {
      public static  IHTWorkDataProvider getHTWorkDataProvider ()
      {
          if (DataControl.workDataProvidePattern.Equals(DataProvidePattern.SQLServerDataBase))
              return new HTSqlServerProvider();//sqlserver数据库为数据来源；
            else
                return new HTTxtWorkDataProvider();//文本文件为数据来源；
      }
    }
}
