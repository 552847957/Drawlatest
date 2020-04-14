using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Model.Drawing;
using System.Data;
using LJJSCAD.Util;
using LJJSCAD.BlackBoard.Legend;
using DesignEnum;
namespace LJJSCAD.DrawingOper
{
    class SymbolItemBuilderOper
    {
        private SymbolItemDesignStruct symItemDesignStruc;

        public SymbolItemDesignStruct SymItemDesignStruc
        {
            get { return symItemDesignStruc; }
            set { symItemDesignStruc = value; }
        }
        public List<SymbolItemStruc> GetSymbolItemPerJDDrawData(JDStruc jdStruc, DataTable symDt)
        {
            double jdTop=jdStruc.JDtop;
            double jdBottom = jdStruc.JDBottom;
            List<SymbolItemStruc> symboldrawinglist = new List<SymbolItemStruc>();
            double depthtop, depthbottom = jdStruc.JDBottom;
            if (symDt == null || symDt.Rows.Count < 1)
                return symboldrawinglist;

            string sqlTxt = GetJoinAdjustSymbolItemSqlTxt(jdTop.ToString(), jdBottom.ToString());

            DataRow[] drs = symDt.Select(sqlTxt, symItemDesignStruc.SyJDTop + " ASC");
            //sqltxt = "gwjs2>2500 and gwjs1<3000"    sb = "gwjs1 ASC"
            if (drs.Length < 1)
                return symboldrawinglist;
            string[] syfieldarr = symItemDesignStruc.ItemField.Split(';');
            foreach (DataRow dr in drs)
            {
                List<string> symbolcodelist = new List<string>();
                depthtop = StrUtil.StrToDouble(dr[symItemDesignStruc.SyJDTop].ToString(), "绘图数据缺少顶部数据", "顶部数据为非数值型");
                if (depthtop < jdTop)
                    depthtop = jdTop;

                if (symItemDesignStruc.SymDepthFieldStyle == DepthFieldStyle.TopAndBottom)
                {
                    depthbottom = StrUtil.StrToDouble(dr[symItemDesignStruc.SyJDBottom].ToString(), "绘图数据缺少底部数据", "底数据为非数值型");
                }
                else if (symItemDesignStruc.SymDepthFieldStyle == DepthFieldStyle.TopAndHeigh)
                {
                    depthbottom = depthtop + StrUtil.StrToDouble(dr[symItemDesignStruc.SyJDHeigh].ToString(), "绘图数据缺少厚度数据", "厚度数据为非数值型");
                }
                else if (symItemDesignStruc.SymDepthFieldStyle == DepthFieldStyle.Top)
                {
                    depthbottom = depthtop;
                }

                if (depthbottom > jdBottom)
                {
                    depthbottom = jdBottom;
                }
                for (int i = 0; i < syfieldarr.Length; i++)
                {
                    string tmpstr = syfieldarr[i].Trim();
                    if (tmpstr != "")
                    {
                        string symbolcode = dr[tmpstr].ToString().Trim();
                        if (symbolcode != "n" && symbolcode != "N" && symbolcode != "")
                        {
                            symbolcodelist.Add(symbolcode);
                            LegendManage.UpdateSymLegendLst(tmpstr, symItemDesignStruc.ItemTable, symbolcode);
                        }
                    }
                }

                symboldrawinglist.Add(new SymbolItemStruc(depthtop, depthbottom, symbolcodelist));

            }
            return symboldrawinglist;
        }
        private string GetJoinAdjustSymbolItemSqlTxt(string jTop, string jBottom)
        {
            string restr = "";
            if (symItemDesignStruc.SyJDTop != "")
            {
                if (symItemDesignStruc.SyJDBottom != "")
                {
                    restr = restr + symItemDesignStruc.SyJDBottom + ">" + jTop + " and " + symItemDesignStruc.SyJDTop + "<" + jBottom;
                }
                else if (symItemDesignStruc.SyJDHeigh != "")
                {
                    restr = restr + "(" + symItemDesignStruc.SyJDTop + @"+" + symItemDesignStruc.SyJDHeigh + ")" + ">" + jTop + " and " + symItemDesignStruc.SyJDTop + "<" + jBottom;
                }
                else
                {
                    restr = restr + symItemDesignStruc.SyJDTop + ">" + jTop + " and " + symItemDesignStruc.SyJDTop + "<" + jBottom;
                }
            }
           
            return restr;

        }


    }
}
