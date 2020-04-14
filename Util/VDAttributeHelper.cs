using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdFigures;
using System.Windows.Forms;

namespace LJJSCAD.Util
{
    class VDAttributeHelper
    {
        public static void setAttrValue(vdDocument activeDocument, string blockName, string attrName, string value)
        {
            VectorDraw.Professional.vdFigures.vdAttrib attr = null;
            foreach (VectorDraw.Professional.vdPrimaries.vdFigure item in activeDocument.ActiveLayOut.Entities)
            {
                VectorDraw.Professional.vdFigures.vdInsert ins = item as VectorDraw.Professional.vdFigures.vdInsert;
                if (ins == null) continue;
                if (ins.Block.Name.CompareTo(blockName) == 0)
                {
                    //Once we locate the insert with the corresponding Block name we edit the attribute we want, using its Tag name.
                    attr = ins.Attributes.FindTagName(attrName);
                    attr.ValueString = value;
                }
            }
            //Every time we modify an attribute we shoule use the Update method to make sure the modifications are visible.
            attr.Update();
        }
        //This method returns the value string of the corresponding Attribute, from a specific Block.
        public static string getAttrValue(vdDocument activeDocument, string blockName, string attrName)
        {
            foreach (VectorDraw.Professional.vdPrimaries.vdFigure item in activeDocument.ActiveLayOut.Entities)
            {
                VectorDraw.Professional.vdFigures.vdInsert ins = item as VectorDraw.Professional.vdFigures.vdInsert;
                if (ins == null) continue;
                if (ins.Block.Name.CompareTo(blockName) == 0)
                {
                    return ins.Attributes.FindTagName(attrName).ValueString;
                }
            }
            return null;
        }
        public static List<string> getAllAttrName(vdDocument activeDocument, string blockName)
        { 
            List<string> attnamelst=new List<string>();
            foreach (VectorDraw.Professional.vdPrimaries.vdFigure item in activeDocument.ActiveLayOut.Entities)
            {
                VectorDraw.Professional.vdFigures.vdInsert ins = item as VectorDraw.Professional.vdFigures.vdInsert;
                if (ins == null) continue;
                if (ins.Block.Name.CompareTo(blockName) == 0)
                {
                    VectorDraw.Professional.vdCollections.vdAttribs tt = ins.Attributes;
                    for (int i = 0; i < tt.Count; i++)
                    {
                        vdAttrib tvd = tt[i];
                        attnamelst.Add(tvd.TagString.Trim());                      
                    }
                    return attnamelst;
                }
            }
            return null;
        }
    }
}
