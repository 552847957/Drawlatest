using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Model;
using LJJSCAD.Model.Drawing;

namespace LJJSCAD.LJJSDrawing.Interface.DrawItemInterface
{
    abstract class AddTextItem
    {


        public TxtItemClass textItemStruct { get; set; }


       public  double lineRoadWidth { set; get; }
       
      
      abstract public List<ulong> AddTextItemToFigure(JDStruc jdstruc, List<TextItemDrawStruc> textItemDrawStrucCol);
    }
}
