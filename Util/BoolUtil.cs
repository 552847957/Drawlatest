using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.BlackBoard.DBItemDesign;

namespace LJJSCAD.Util
{
    class BoolUtil
    {
        public static bool GetBoolByBindID(string idStr, bool nullValue)
        {
            bool resultvalue = false;
            if (string.IsNullOrEmpty(idStr))
                return nullValue;
            string resultstr = idStr.Trim();
            if (resultstr.Equals(bool.TrueString))
            {
                resultvalue = true;
            }
            else if (resultstr.Equals(bool.FalseString))
            {
                resultvalue = false;
            }
            else
                resultvalue = false;
            return resultvalue;
 
        }
     
    }
}
