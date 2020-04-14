using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Model.Drawing;
using System.Collections;
using System.Windows.Forms;
using LJJSCAD.CommonData;
using System.Data.Common;
using LJJSCAD.DAL;
using LJJSCAD.Util;
using LJJSCAD.DrawingOper;
using DesignEnum;
namespace LJJSCAD.BlackBoard.LJJSDesignData.Impl
{
    class FillSymbolCode
    {
        public static Hashtable Ht_SymYXAndDmCon = new Hashtable();     
        private static Hashtable _symbolcodeclassht = new Hashtable();
        /// <summary>
        /// 定义一个哈希表用来存储符号代码类
        /// </summary>
        public static Hashtable SymbolCodeClassHt
        {
            get { return _symbolcodeclassht; }
            set { _symbolcodeclassht = value; }
        }
 
    
        public static void SetSymbolCodeClassHt()
        {
            _symbolcodeclassht.Clear();

            DbDataReader dr = SymbolCodeDAL.GetAllSymbolCode();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    SymbolCodeClass sycc = new SymbolCodeClass();
                    sycc.symbolcode = dr["SymbolCode"].ToString().Trim();
                    sycc.symbolHeigh = StrUtil.StrToDouble(dr["SymbolHeigh"].ToString(), 0, "符号高度的数据为非数值型");
                    sycc.ifFill = BoolUtil.GetBoolByBindID(dr["IfFill"].ToString(),false);
                    sycc.ifZXEnlarge = BoolUtil.GetBoolByBindID(dr["IfZXEnlarge"].ToString(),false);
                    sycc.symbolWidth = StrUtil.StrToDouble(dr["SymbolWidth"].ToString(), 0, "符号宽度的数据为非数值型");
                    sycc.legendSequence = StrUtil.StrToInt(dr["xh"].ToString(), sycc.symbolcode + "图例缺少图例序号", sycc.symbolcode + "图例序号为非数值型");
                    sycc.legendstyle = LegendOper.GetLegendTypeByStr(dr["LegendType"].ToString());
                    if (sycc.legendstyle == LegendStyle.errStyle)
                    {
                        sycc.legendstyle = LegendStyle.YSTxtStyle;
                        MessageBox.Show(sycc.symbolcode + "图例类型表述错误，请检查符号设置表，系统默认为颜色文本类型符号");
                    }
                    sycc.symbolChineseName = dr["ysmc"].ToString().Trim();
                    sycc.legendWidth = StrUtil.StrToDouble(dr["LegendlWidth"].ToString(), 20, sycc.symbolcode + "图例框宽度为非数值型");
                    try
                    {
                        _symbolcodeclassht.Add(sycc.symbolcode, sycc);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            dr.Close();
        }
    }
}
