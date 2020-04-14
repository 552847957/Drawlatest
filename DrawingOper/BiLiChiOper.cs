using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.DrawingOper
{
    class BiLiChiOper
    {
        public static readonly char  blcSplitter=':';
        public static string GetXValueStr(string blcStr)
        {
            if (string.IsNullOrEmpty(blcStr))
                return "";
            string[] blcarr = blcStr.Split(blcSplitter);
            if (blcarr.Length == 2)
            {
                return blcarr[0];
            }
            return "";

        }
        public static string GetYValueStr(string blcStr)
        {
            if (string.IsNullOrEmpty(blcStr))
                return "";
            string[] blcarr = blcStr.Split(blcSplitter);
            if (blcarr.Length == 2)
            {
                return blcarr[1];
            }
            return "";

        }
    }
}
