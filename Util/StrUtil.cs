using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LJJSCAD.Util
{
    class StrUtil
    {
        /// <summary>
        /// 字符串转成double型；
        /// </summary>
        /// <param name="doubleStr">需要转换的字符串</param>
        /// <param name="eNullStr">如果字符串为空串，需要提示的信息</param>
        /// <param name="eStr">如果转换出异常，需要提示的信息</param>
        /// <returns></returns>
        public static double StrToDouble(string doubleStr, string eNullStr, string eStr)
        {
            double rvalue = 0;
           
            if (string.IsNullOrEmpty(doubleStr))
                MessageBox.Show(eNullStr);
            else
            {
               
                string doublestr = doubleStr.Trim();
                try
                {
                    rvalue = double.Parse(doublestr);
                }
                catch
                {
                    MessageBox.Show(eStr);
                }

            }          
           
            return rvalue;

        }
        public static string GetNoNullStr(string sourceStr)
        {
            string tmp="";
            if (!string.IsNullOrEmpty(sourceStr))
                tmp = sourceStr.Trim();
            return tmp;             
 
        }

        public static double StrToDouble(string doubleStr, double nullvalue, string eStr)
        {
            double rvalue = 0;
            if (string.IsNullOrEmpty(doubleStr))
                rvalue = nullvalue;
            else
            {
                string doublestr = doubleStr.Trim();
                try
                {
                    rvalue = double.Parse(doublestr);
                }
                catch
                {
                    MessageBox.Show(eStr);
                }
            }        
            return rvalue;

        }
        public static bool IfTwoStrEqul(string str1, string str2)
        {
            string tmpstr1 = str1.Trim();
            string tmpstr2 = str2.Trim();
            if (tmpstr1.Equals(tmpstr2))
                return true;
            else
                return false;
        }

        public static int StrToInt(string intStr, string enullStr, string eStr)
        {
            string intstr = intStr.Trim();
            int rvalue = 0;
            if (intstr.Equals(""))
            {
                MessageBox.Show(enullStr);
            }
            else
            {
                try
                {
                    rvalue = int.Parse(intstr);
                }
                catch
                {
                    MessageBox.Show(eStr);
                }
            }
            return rvalue;

        }

        public static int StrToInt(string intStr, int nullValue, string eStr)
        {
            string intstr = intStr.Trim();
            int rvalue = 0;
            if (string.IsNullOrEmpty(intStr))
            {
                return nullValue;
            }
            else
            {
                try
                {
                    rvalue = int.Parse(intstr);
                }
                catch
                {
                    MessageBox.Show(eStr);
                }
            }
            return rvalue;

        }
        public static string StrTestNULL(string testStr,string nullWarnInfo)
        {
            string tmp = testStr.Trim();
            if (string.IsNullOrEmpty(tmp))
            {
                MessageBox.Show(nullWarnInfo);
                return "";
            }
            else
                return tmp;
        }
        public static string StrTestNULL(string testStr, string nullWarnInfo,string nullValue)
        {
            string tmp = testStr.Trim();
            if (string.IsNullOrEmpty(tmp))
            {
                MessageBox.Show(nullWarnInfo);
                return nullValue;
            }
            else
                return tmp;
        }
        public static bool StrCompareIngoreCase(string strOne, string strTwo)
        {
            if (string.IsNullOrEmpty(strOne) || string.IsNullOrEmpty(strTwo))
                return false;
            string tmp1 = strOne.Trim().ToLower();
            string tmp2 = strTwo.Trim().ToLower();
            if (tmp1.Equals(tmp2))
                return true;
            else
                return false;

        }
    }
}
