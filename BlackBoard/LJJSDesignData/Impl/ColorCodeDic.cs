using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using LJJSCAD.DAL;

namespace LJJSCAD.BlackBoard.LJJSDesignData.Impl
{
    class ColorCodeDic
    {
        /// <summary>
        /// 获得颜色字典，颜色描述为主键
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetColorDescKeyDic()
        {

            return GetColorDic("ColorDesc", "ColorARGBValue");
 
        }
        /// <summary>
        /// 获得颜色字典，颜色代码为主键
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetColorCodeKeyDic()
        {
            return GetColorDic("ColorCode", "ColorARGBValue");
        }
        private static Dictionary<string, string> GetColorDic(string keyField,string ValueField)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            DbDataReader dr = ColorCodeDAL.GetAllColorCode();
            if (null != dr)
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string key=dr[keyField].ToString().Trim();
                        string keyvalue=dr[ValueField].ToString().Trim();
                        if(!result.ContainsKey(key))
                        result.Add(key, keyvalue);
                    }
                }
            }
            return result;
 
        }
    }
}
