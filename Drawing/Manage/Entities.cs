using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Util;
using LJJSCAD.CommonData;

namespace LJJSCAD.Drawing.Manage
{
    class Entities
    {
        public static void ClearAll()
        {
            VectorDrawHelper.ClearAllEntities(DrawCommonData.activeDocument);
        }
    }
}
