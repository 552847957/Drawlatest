using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesignEnum;

namespace LJJSCAD.BlackBoard
{

    class DataControl
    {
        public static DataProvidePattern designDataProvidePattern = DataProvidePattern.SQLServerDataBase;
        public static DataProvidePattern workDataProvidePattern = DataProvidePattern.SQLServerDataBase;
        public static DataProvidePattern headerTableWorkDataProvidePattern = DataProvidePattern.SQLServerDataBase;
        public static string TuTouBiaoBlockName = "";


    }
}
