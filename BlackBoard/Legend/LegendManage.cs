using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingDesign.Frame;

namespace LJJSCAD.BlackBoard.Legend
{
    class LegendManage
    {
        //绘图需要作为符号类型图例的符号名称表，也就是符号图例名称；
        public static List<string> SymLegendNameLst = new List<string>();
        //绘图需要作为文本类型图例的符号名称表，也就是文本类型图例名称；
        public static List<string> TxtLegendNameLst = new List<string>();
        //绘图项中作为图例的符号需要来自的表；
        public static List<string> dilegendTblist = new List<string>();
        //绘图项中作为图例的符号需要来自的字段;
        public static List<string> dilegendfieldlist = new List<string>();
        public static void UpdateTxtLegendLst(string fieldstr, string SymbolItemFromTableName, string legendCode)
        {
            if (isLegendField(fieldstr, SymbolItemFromTableName))
                if (!TxtLegendNameLst.Contains(legendCode))
                    TxtLegendNameLst.Add(legendCode);
        }
        public static void UpdateSymLegendLst(string fieldstr, string SymbolItemFromTableName, string legendCode)
        {
            if (isLegendField(fieldstr, SymbolItemFromTableName))
                if (!SymLegendNameLst.Contains(legendCode))
                    SymLegendNameLst.Add(legendCode);
        }
        private static bool isLegendField(string fieldstr, string SymbolItemFromTableName)
        {
            bool revel = false;
            string tbname = SymbolItemFromTableName.ToLower().Trim();
            string fieldname = fieldstr.ToLower().Trim();

            if (dilegendTblist.Contains(tbname) && dilegendfieldlist.Contains(fieldname))
                revel = true;
            return revel;
        }
        public static void SetLegendTbAndFieldList()
        {
            string tmpstr = FrameDesign.LegendTbAndField.Trim();
            string[] tbandfieldarr = tmpstr.Split(';');
            for (int i = 0; i < tbandfieldarr.Length; i++)
            {
                string tbandfieldstr = tbandfieldarr[i].Trim();
                string[] tborfieldarr = tbandfieldstr.Split(':');
                if (tborfieldarr.Length == 2)
                {
                    string tbname = tborfieldarr[0].ToLower().Trim();
                    if (!dilegendTblist.Contains(tbname))
                        dilegendTblist.Add(tbname);
                    string fieldname = tborfieldarr[1].ToLower().Trim();
                    if (!dilegendfieldlist.Contains(fieldname))
                        dilegendfieldlist.Add(fieldname);
                }
            }
        }
    }
}
