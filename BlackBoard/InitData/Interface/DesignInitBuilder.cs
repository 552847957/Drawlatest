using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.BlackBoard.InitData.Interface
{
    abstract class DesignInitBuilder
    {

       public abstract void InitFrameDesign();
       public abstract void InitItemDesign();
       public abstract void InitItemSymbolDesign();
       public abstract void InitOtherDesign();
       public abstract void InitKeDuChi();
       public abstract void InitLineRoadDesgin(string myLineRoadDesignName);
    }
}
