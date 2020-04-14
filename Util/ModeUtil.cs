using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.Util
{
    class ModeUtil
    {
        /// <summary>
        /// 返回一个数字，要求是BeiShu的整数倍，并且是最小的大于inDoubleValue的数，即离inDoubleValue最近的可以被BeiShu整除的整数
        /// </summary>
        /// <param name="inDoubleValue"></param>
        /// <param name="BeiShu"></param>
        /// <returns></returns>
        public static int GetMinBeiShu(double inDoubleValue, int BeiShu)
        {
            double modevalue = inDoubleValue % BeiShu;
            int returnvalue = (int)(inDoubleValue + BeiShu - modevalue);
            return returnvalue;
        }
    }
}
