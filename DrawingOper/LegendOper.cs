using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.BlackBoard.Legend;
using LJJSCAD.DrawingElement;
using LJJSCAD.Drawing.Figure;
using LJJSCAD.Drawing.Curve;
using DesignEnum;

namespace LJJSCAD.DrawingOper
{
    class LegendOper
    {
        private static char txtLegendSplitter='.';


      
        public static LegendStyle GetLegendTypeByStr(string legendTypeStr)
        {
            LegendStyle resultvalue = LegendStyle.SymbolStyle;
            string resultstr = legendTypeStr.Trim();
            if (resultstr.Equals("s") || resultstr.Equals("S"))
            {
                resultvalue = LegendStyle.SymbolStyle;
            }
            else if (resultstr.Equals("t") || resultstr.Equals("T"))
            {
                resultvalue = LegendStyle.YSTxtStyle;
            }
            else
                resultvalue = LegendStyle.SymbolStyle;
            return resultvalue;
        }
        public static List<string> AyalyseTxtLegendLst()
        {

            List<string> opertedtxtleg = new List<string>();
            foreach (string txtleg in LegendManage.TxtLegendNameLst)
            {
                string tmptxtleg = txtleg.Trim();
                int count = tmptxtleg.Length;
                if (count > 0)
                {
                    if (!char.IsDigit(tmptxtleg[0]))
                    {
                        string firstr = tmptxtleg[0].ToString();
                        if (!opertedtxtleg.Contains(firstr))
                            opertedtxtleg.Add(firstr);
                        tmptxtleg = tmptxtleg.Remove(0, 1);
                    }
                    string[] ysstrarr = tmptxtleg.Split(txtLegendSplitter);
                    for (int i = 0; i < ysstrarr.Length; i++)
                    {
                        if (!opertedtxtleg.Contains(ysstrarr[i]))
                            opertedtxtleg.Add(ysstrarr[i]);
                    }
                }
            }
            return opertedtxtleg;

        }
        public static void AddSunStyleRect(LJJSPoint ptBase, double SunFrameHeigh, double SunFrameWidth, double lineWidth)
        {

            Rect.AddBlackRect(ptBase, SunFrameHeigh, SunFrameWidth, lineWidth, new DrawDirection(1, 1));
            Line.BuildCommonHorLineByLayer(new LJJSPoint(ptBase.XValue, ptBase.YValue + SunFrameHeigh * 0.5),SunFrameWidth,lineWidth,1);
                //(new LJJSPoint(ptBase.XValue, ptBase.YValue + SunFrameHeigh * 0.5), SunFrameWidth, lineWidth);
        }
    }
}
