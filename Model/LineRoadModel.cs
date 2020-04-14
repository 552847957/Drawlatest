using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.Model
{
    class LineRoadModel
    {
    public string LineRoadDesignID {set;get;}
    public string LineRoadDesignDetailID {set;get;}
    public string LineRoadStyle  {set;get;}
    public string LineRoadWidth {set;get;}
    public string CwLineType {set;get;}
    public bool IfzhengMiLine {set;get;}
    public bool IfLeftSecondKD { set; get; }
    public bool IfRightSecondKD { set; get; }
    public bool TitleLeftFrameLineChecked { set; get; }
    public string LeftLineStyle {set;get;}
    public string LineRoadName {set;get;}
    public string LineroadTitleHeight {set;get;}

    public bool ifAddCeWang { set; get; }

    public string cewangstyle { set; get; }
    public string LeftLineLength { set; get; }

    public string duishuminvalue { set; get; }
    public string duishuParam { set; get; }
    public string cewangsepnum { set; get; }
    public string cewangfixlen { set; get; }
    public bool IfCeWangHeng { set; get; }
    public bool IfCeWangZong { set; get; }



    public string CurveItemCollections { set; get; }
    public string TextItemCollections { set; get; }
    public string SymbolItemCollections { set; get; }
    public string ImageItemCollections { set; get; }
    public string HcgzItemCollections { set; get; }
    public string MultiHatchCurveItemCollections { set; get; }
    public string HatchRectItemCollections { set; get; }
    public string NormalPuTuItemCollections { set; get; }
    public string CurveHasHatchItemCollections { set; get; }




    }
}
