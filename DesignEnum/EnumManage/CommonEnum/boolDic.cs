using System;
using System.Collections.Generic;
using System.Linq;


namespace EnumManage
{
    public class boolDic:DicOper
    {

        public override void  CreateDic()
        {           
            Dic.Add(bool.TrueString, "是");
            Dic.Add(bool.FalseString, "否");        
        }
    }
}