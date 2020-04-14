using LJJSCAD.LJJSDrawing.Impl.DrawItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.LJJSDrawing.Impl
{
    public static class DrawPointContainer //js
    {
        public static SuiZuanForm szf;
        public static string fromtablename;

        public static List<MsehAndMsevContainer> list;

        public static void initiate()
        {
            DrawPointContainer.list = null;
            DrawPointContainer.szf = null;
            DrawPointContainer.fromtablename = null;
        }
        public static void set_list(int jd_count)   //有几个井段，list里就有几个元素
        {
             DrawPointContainer.list = new List<MsehAndMsevContainer>();
            for(int i =0;i<jd_count;i++)
            {
               
                MsehAndMsevContainer item = new MsehAndMsevContainer();
                item.Initiate();
                DrawPointContainer.list.Add(item);
            }
        }
    }
}
