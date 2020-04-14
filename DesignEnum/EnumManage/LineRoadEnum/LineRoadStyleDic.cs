
using DesignEnum;

namespace EnumManage
{
    public class LineRoadStyleDic:DicOper
    {
        public override void CreateDic()
        {
            Dic.Add(LineRoadStyle.JingShenLineRoad.ToString(),"井深线道");
            Dic.Add(LineRoadStyle.StandardLineRoad.ToString(), "标准线道");
            Dic.Add(LineRoadStyle.NullLineRoad.ToString(), "空线道");
        }
    }
}