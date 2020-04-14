using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VectorDraw.Professional.vdObjects;
using System.Drawing;
using System.Data;

namespace LJJSCAD.CommonData
{
    public class DrawCommonData
    {
        public static bool baseline = false;
    
        public static  DataTable ptable;
        public static int upperdepth;
        public static int lowerdepth;



        public static double? drill_radius;

        public static double curr_max_depth = -999; //现在在数据库中查到的最大的depth


        public static readonly double xStart=0;//线道最左侧起点的X
        public static readonly double yStart=0;
        public static readonly char jdSplitter='-';
        public static vdDocument activeDocument=null;

        //
        public static readonly int BlackColorRGB = Color.Gray.ToArgb();
        public static readonly string CeWangLineStyle = "DOTX2";
        public static readonly string SolidLineTypeName = "SOLID";
        public static readonly string StandardStyle = "standardstyle";
        public static readonly int DirectionUp = 1;
        public static readonly int DirectionDown = -1;
        public static readonly int DirectionLeft = -1;
        public static readonly int DirectionRight = 1;
        public static readonly double HatchScale = 0.1;
        public static int icount = 0;
        public static double startjd;
        public static double endjd;
        public static int startDepth1 = -999;
        public static int startDepth2 = -999;

        //用于归一化   是用户给定的
        public static double? MsevMax;    //垂向功最大值
        public static double? MsehMax;    //切向功最大值
        public static double? MsebMax;     //标准化机械比能最大值
        public static double? JGLMax;   //进给量最大值

        public static double? MsevMin;    
        public static double? MsehMin;   
        public static double? MsebMin;   
        public static double? JGLMin;  


        //实时显示的最大值最小值
        public static double curr_MsevMax;
        public static double curr_MsehMax;
        public static double curr_MsebMax;
        public static double curr_JGLMax;

        public static double curr_MsevMin;
        public static double curr_MsehMin;
        public static double curr_MsebMin;
        public static double curr_JGLMin;

        //基值

        public static string base_value;

        public static void Initiate()
        {
            DrawCommonData.baseline = false;


            DrawCommonData.base_value = null;

            DrawCommonData.drill_radius = null;
            //DrawCommonData.activeDocument = null;
            DrawCommonData.icount = 0;
            DrawCommonData.startjd = 0;
            DrawCommonData.endjd = 0;
            DrawCommonData.startDepth1 = -999;
            DrawCommonData.startDepth2 = -999;
            DrawCommonData.curr_max_depth = -999;

             DrawCommonData.MsehMax = null;
             DrawCommonData.MsevMax = null;
             DrawCommonData.MsebMax = null;
             DrawCommonData.JGLMax = null;

             DrawCommonData.MsehMin = null;
             DrawCommonData.MsevMin = null;
             DrawCommonData.MsebMin = null;
             DrawCommonData.JGLMin = null;


             DrawCommonData.curr_MsevMax = 0;
             DrawCommonData.curr_MsehMax = 0;
             DrawCommonData.curr_JGLMax = 0;
             DrawCommonData.curr_MsebMax = 0;

             DrawCommonData.curr_MsevMin = 0;
             DrawCommonData.curr_MsehMin = 0;
             DrawCommonData.curr_JGLMin = 0;
             DrawCommonData.curr_MsebMin = 0;
        }
    }
}
