using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.BlackBoard.InitData.Interface;
using LJJSCAD.BlackBoard.InitData.Impl;
using DesignEnum;

namespace LJJSCAD.BlackBoard.InitData.Factory
{
    class DesignInitBuilderFactory
    {
        public static DesignInitBuilder CreateDesignInitBuilder(DataProvidePattern dataProvidePattern)
        {
            if (dataProvidePattern.Equals(DataProvidePattern.DataBase))
                return new DataBaseDesignInitBuilder();
            else if (dataProvidePattern.Equals(DataProvidePattern.TextFile))
                return new TextDesignInitBuilder();
            else
                return new DataBaseDesignInitBuilder();   //this one!

        }
    }
}
