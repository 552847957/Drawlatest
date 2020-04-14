using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignEnum
{
   public class EnumUtil
    {
        /// <summary>
        /// 根据输入的枚举值，获得对应的枚举类型；
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumStr"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static T GetEnumByStr<T>(string enumStr, T nullValue)
        {
            if (string.IsNullOrEmpty(enumStr))
                return nullValue;
            enumStr = enumStr.Trim();
            Array enmuData = Enum.GetValues(nullValue.GetType());
            for (int i = 0; i < enmuData.Length; i++)
            {
                T tmp = (T)enmuData.GetValue(i);
                string nn = tmp.ToString();
                if (nn == enumStr)
                    return (T)tmp;

            }
            return nullValue;
        }
    }
}
