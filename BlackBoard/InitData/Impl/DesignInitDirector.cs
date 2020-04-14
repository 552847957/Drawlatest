using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.BlackBoard.InitData.Interface;

namespace LJJSCAD.BlackBoard.InitData.Impl
{
    class DesignInitDirector
    {
        private DesignInitBuilder _designInitBuilder;
        public DesignInitDirector(DesignInitBuilder designInitBuilder)
        {
            _designInitBuilder = designInitBuilder;

        }
        public void ExecDesignInit(string myLineRoadDesignName)
        {
            _designInitBuilder.InitFrameDesign();
            _designInitBuilder.InitItemDesign();
            _designInitBuilder.InitItemSymbolDesign();           
            _designInitBuilder.InitKeDuChi();
            _designInitBuilder.InitLineRoadDesgin(myLineRoadDesignName);
            _designInitBuilder.InitOtherDesign();
        }
    }
}
