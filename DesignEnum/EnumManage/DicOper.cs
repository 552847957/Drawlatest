using System;
using System.Collections.Generic;
using System.Linq;


namespace EnumManage
{
    public abstract class DicOper
    {
        private Dictionary<string, string> dic;

        public Dictionary<string, string> Dic
        {
            get { return dic; }
           
        }
        public DicOper()
        {
            dic = new Dictionary<string, string>();
            CreateDic(); 
        }
        public abstract void CreateDic();
       
        public string GetDicValueByKey(string keyContent)
        {

            if (string.IsNullOrEmpty(keyContent))
                return "";
            keyContent = keyContent.Trim();
            if (dic.ContainsKey(keyContent))
            {
                return dic[keyContent];
            }
            else
            {
                return "";
            }

        }

    }
}