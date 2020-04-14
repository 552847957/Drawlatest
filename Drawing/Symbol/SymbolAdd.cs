using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using LJJSCAD.Util;
using VectorDraw.Geometry;
using LJJSCAD.CommonData;

namespace LJJSCAD.Drawing.Symbol
{
    class SymbolAdd
    {
        public static ulong InsertBlock(string blockName, double XScale, double YScale, LJJSPoint position)
        {
            string blockna = blockName.Trim();

          
            return VectorDrawHelper.InsertBolck(DrawCommonData.activeDocument, blockna, XScale, YScale, new gPoint(position.XValue, position.YValue));
           
        }
        public static ulong InsertBlock(string blockName, string sourceFile, LJJSPoint ins)
        {
            return 0;
        }
    }
}
