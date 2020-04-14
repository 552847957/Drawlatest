using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Util;
using LJJSCAD.CommonData;

namespace LJJSCAD.Drawing.Manage
{
    class Layer
    {
        public  static void Layer_SetToCurrent(string layerName)
        {
            VectorDrawHelper.Layer_SetCurrent(DrawCommonData.activeDocument,layerName);
        }
        public static void CreateLayer(string layerName, int layerColor, string lineType)
        {
            VectorDrawHelper.CreateLayer(DrawCommonData.activeDocument, layerName, layerColor, lineType);
        }
        public static void CreateAndSetCurrentLayer(string layerName, int layerColor)
        {
            CreateLayer(layerName, layerColor, "");
            Layer_SetToCurrent(layerName);
        
        }

    }
}
