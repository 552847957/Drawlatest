using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnumManage;

namespace DesignEnum.EnumManage.DrawItem
{
   public class AttachmentPointDic:DicOper
    {
        public override void CreateDic()
        {
            Dic.Add(AttachmentPoint.BottomCenter.ToString(), "底部中心");
            Dic.Add(AttachmentPoint.BottomLeft.ToString(), "底部左侧");
            Dic.Add(AttachmentPoint.BottomRight.ToString(), "底部右侧");
            Dic.Add(AttachmentPoint.MiddleCenter.ToString(), "中部中心");
            Dic.Add(AttachmentPoint.MiddleLeft.ToString(), "中部左侧");
            Dic.Add(AttachmentPoint.MiddleRight.ToString(), "中部右侧");
            Dic.Add(AttachmentPoint.TopCenter.ToString(), "顶部中心");
            Dic.Add(AttachmentPoint.TopLeft.ToString(), "顶部左侧");
            Dic.Add(AttachmentPoint.TopRight.ToString(), "顶部右侧");
           
        }
    }
}
