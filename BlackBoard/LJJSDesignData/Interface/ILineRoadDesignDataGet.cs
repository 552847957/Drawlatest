using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using LJJSCAD.Model;
using LJJSCAD.Model.Drawing;

namespace LJJSCAD.BlackBoard.LJJSDesignData.Interface
{
    interface ILineRoadDesignDataGet
    {
        List<LineRoadModel> GetDefaultLineRoadDesignModelLst(string myLineRoadDesignName);
        List<LineRoadModel> GetLineRoadDesignModelLst(string lineRoadDesignID);
        JingShenModel GetDefaultJingShenDesign();
    }
}
