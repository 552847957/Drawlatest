using System;
using System.Collections.Generic;



namespace EnumManage
{
    public class SystemFontDic:DicOper
    {
        public override void CreateDic()
        {
            //如何获得系统字体列表            
            System.Drawing.Text.InstalledFontCollection fonts = new System.Drawing.Text.InstalledFontCollection();
            foreach (System.Drawing.FontFamily family in fonts.Families)
            {
                Dic.Add(family.Name, family.Name);
            }
          
        }
    }
}