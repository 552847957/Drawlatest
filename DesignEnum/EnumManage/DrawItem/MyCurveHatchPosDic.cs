using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnumManage
{
    enum MyCurveHatchPos { Left, Right, None }//曲线填充方向，左右；
    public class MyCurveHatchPosDic:DicOper
    {
        
        public override void CreateDic()
        {
            Dic.Add(MyCurveHatchPos.Left.ToString(),"左填充");
            Dic.Add(MyCurveHatchPos.Right.ToString(), "右填充");
            Dic.Add(MyCurveHatchPos.None.ToString(),"无填充");
        }
    }
}