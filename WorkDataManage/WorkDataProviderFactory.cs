using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.WorkDataManage.Interface;
using LJJSCAD.BlackBoard;
using LJJSCAD.WorkDataManage.Impl;
using DesignEnum;

namespace LJJSCAD.WorkDataManage
{
    class WorkDataProviderFactory
    {
       public static IWorkDataProvider GetWorkDataProvider()
        {
            if (DataControl.workDataProvidePattern.Equals(DataProvidePattern.SQLServerDataBase))
                return new SQLServerWorkDataProvider();
            else
                return new SQLServerWorkDataProvider();

        }
    }
}
