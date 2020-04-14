using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnumManage
{
    public class LineTypeDic:DicOper
    {
        public override void CreateDic()
        {
            Dic.Add("CENTER", "CENTER");
            Dic.Add("DASHDOT", "DASHDOT");
            Dic.Add("DASHED", "DASHED");
            Dic.Add("HIDDEN", "HIDDEN");
            Dic.Add("HIDDEN2", "HIDDEN2");
            Dic.Add("BORDER", "BORDER");
            Dic.Add("BORDER2", "BORDER2");
            Dic.Add("BORDERX2", "BORDERX2");
            Dic.Add("CENTER2", "CENTER2");
            Dic.Add("DASHDOT2", "DASHDOT2");
            Dic.Add("DASHDOTX2", "DASHDOTX2");
            Dic.Add("DASHED2", "DASHED2");
            Dic.Add("DOTX2", "DOTX2");
            Dic.Add("HIDDENX2", "HIDDENX2");
            Dic.Add("PHANTOM", "PHANTOM");
            Dic.Add("PHANTOM2", "PHANTOM2");
            Dic.Add("PHANTOMX2", "PHANTOMX2");
            Dic.Add("SOLID", "SOLID");
            Dic.Add("BYLAYER", "BYLAYER");
            Dic.Add("BYBLOCK", "BYBLOCK");
            Dic.Add("DASHED0", "DASHED0");
            Dic.Add("DOT0", "DOT0");
            Dic.Add("DASHDOT0", "DASHDOT0");
            Dic.Add("DASHDOTDOT0", "DASHDOTDOT0");
            Dic.Add("INVISIBLE", "INVISIBLE");
            
        }
    }
}