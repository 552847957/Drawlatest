using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJJSCAD.Model;

namespace LJJSCAD.DrawingDesign.Frame
{
    public delegate void set_jdStrList_DELE(List<string> inputList);
    class FrameDesign
    {

       


        //绘图数据库名称
        private static string _drawDBName = "";

        public static string DrawDBName
        {
            get { return FrameDesign._drawDBName; }
            set { FrameDesign._drawDBName = value; }
        }

        ///绘图井设计
        private static string _jH="";//井号

        public static string JH
        {
            get { return FrameDesign._jH; }
            set { FrameDesign._jH = value; }
        }
        private static List<string> _jdStrLst=new List<string>();//存放由界面读入的井段设计；

        public static List<string> JdStrLst
        {
            get { return FrameDesign._jdStrLst; }
            set { FrameDesign._jdStrLst = value; }
        }


        private static double _jdSpace=20;//分段间距

        public static double JdSpace
        {
            get { return FrameDesign._jdSpace; }
            set { FrameDesign._jdSpace = value; }
        }

        //绘图外框设计
        private static double _pictureFrameLineWidth=0.5;//外框线宽

        public static double PictureFrameLineWidth
        {
            get { return FrameDesign._pictureFrameLineWidth; }
            set { FrameDesign._pictureFrameLineWidth = value; }
        }
        private static double _lineRoadTitleBarHeigh=40;//线道标题栏高度

        public static double LineRoadTitleBarHeigh
        {
            get { return FrameDesign._lineRoadTitleBarHeigh; }
            set { FrameDesign._lineRoadTitleBarHeigh = value; }
        }

        //比例尺设计

        //设定比例尺
        public static bool set_Bi_Li_Chi(double xcoord,double ycoord) //i wrote
        {
            if(xcoord <= ycoord)
            {
                FrameDesign.XCoordinate = xcoord;
                FrameDesign.YCoordinate = ycoord;
                return true;  //返回true则设定新比例尺成功，否则失败
            }
            else
            {
                return false;
            }
        }
        //回复到默认的比例尺
        public static void return_to_default_BiLiChi()
        {
            FrameDesign.XCoordinate = 1;
            FrameDesign.YCoordinate = 100;
        }

        private static double _xCoordinate=1;//X轴比例尺

        public static double XCoordinate
        {
            get { return FrameDesign._xCoordinate; }
            set { FrameDesign._xCoordinate = value; }
        }
        private static double _yCoordinate=100;//Y轴比例尺

        public static double YCoordinate
        {
            get { return FrameDesign._yCoordinate; }
            set { FrameDesign._yCoordinate = value; }
        }
        private static double _valueCoordinate=0.01;//比例尺计算结果

        public static double ValueCoordinate
        {
            get { return FrameDesign._valueCoordinate; }
            set { FrameDesign._valueCoordinate = value; }
        }
        private static string _corTxtFont="Times New Roman";//比例尺字体

        public static string CorTxtFont
        {
            get { return FrameDesign._corTxtFont; }
            set { FrameDesign._corTxtFont = value; }
        }
        private static double _corTxtHeit=4;//比例尺高度

        public static double CorTxtHeit
        {
            get { return FrameDesign._corTxtHeit; }
            set { FrameDesign._corTxtHeit = value; }
        }
        private static string _corTxtColor = "-16777216";//比例尺颜色

        public static string CorTxtColor
        {
            get { return FrameDesign._corTxtColor; }
            set { FrameDesign._corTxtColor = value; }
        }
        private static string _corValue="1:100";//比例尺从数据库的值

        public static string CorValue
        {
            get { return FrameDesign._corValue; }
            set { FrameDesign._corValue = value; }
        }

        //绘图项文字
        private static string _pictureItemFont="宋体";//绘图项文字字体

        public static string PictureItemFont
        {
            get { return FrameDesign._pictureItemFont; }
            set { FrameDesign._pictureItemFont = value; }
        }
        private static double _pictureItemTxtHeight = 4;//绘图项文字高度，仅仅是文字的字体高度

        public static double PictureItemTxtHeight
        {
            get { return FrameDesign._pictureItemTxtHeight; }
            set { FrameDesign._pictureItemTxtHeight = value; }
        }

        //刻度尺文字
        private static string _scaleLabelTxtFont="宋体";//刻度尺文字字体

        public static string ScaleLabelTxtFont
        {
            get { return FrameDesign._scaleLabelTxtFont; }
            set { FrameDesign._scaleLabelTxtFont = value; }
        }
        private static string _scaleLabelTxtHeight="2";//刻度尺文字高度

        public static string ScaleLabelTxtHeight
        {
            get { return FrameDesign._scaleLabelTxtHeight; }
            set { FrameDesign._scaleLabelTxtHeight = value; }
        }

        //图头设计
        private static string _headerContent="";//图头文字内容

        public static string HeaderContent
        {
            get { return FrameDesign._headerContent; }
            set { FrameDesign._headerContent = value; }
        }
        private static string _pictureHeaderTXTStyle = "隶书";//图头文字字体

        public static string PictureHeaderTXTStyle
        {
            get { return FrameDesign._pictureHeaderTXTStyle; }
            set { FrameDesign._pictureHeaderTXTStyle = value; }
        }
        private static double _headBigTxtHeight=18;//图头文字高度

        public static double HeadBigTxtHeight
        {
            get { return FrameDesign._headBigTxtHeight; }
            set { FrameDesign._headBigTxtHeight = value; }
        }
        private static string _headerTxtColor = "-4582375"; //图头文字颜色

        public static string HeaderTxtColor
        {
            get { return FrameDesign._headerTxtColor; }
            set { FrameDesign._headerTxtColor = value; }
        }  

        //图例设计
        private static string _legendPos;//图例设计位置

        public static string LegendPos
        {
            get { return FrameDesign._legendPos; }
            set { FrameDesign._legendPos = value; }
        }
        private static string _legendStyle;//图例设置类型

        public static string LegendStyle
        {
            get { return FrameDesign._legendStyle; }
            set { FrameDesign._legendStyle = value; }
        }
        private static int _legendColumnNum;//图例设置列数

        public static int LegendColumnNum
        {
            get { return FrameDesign._legendColumnNum; }
            set { FrameDesign._legendColumnNum = value; }
        }
        private static double _legendUnitHeigh;//图例设计单位高度

        public static double LegendUnitHeigh
        {
            get { return FrameDesign._legendUnitHeigh; }
            set { FrameDesign._legendUnitHeigh = value; }
        }
        private static string _legendTbAndField;//图例查询涉及的表和字段;

        public static string LegendTbAndField
        {
            get { return FrameDesign._legendTbAndField; }
            set { FrameDesign._legendTbAndField = value; }
        }
        private static bool _ifAddLegend;//是否绘制图例;

        public static bool IfAddLegend
        {
            get { return FrameDesign._ifAddLegend; }
            set { FrameDesign._ifAddLegend = value; }
        }
        public static void SetFrameDesignByFrameModel(FrameModel frameModel)
        {
           // IfAddLegend=frameModel.
 
        }

        public static set_jdStrList_DELE set_MainForm_jdStrList_dele;

        public static Boolean Validate(SuiZuanForm mf)
        {
            Boolean res = true;
            DrawDBName = "Sql";
            //    JH = "tt";
            if (DrawDBName.Equals(""))
            {
                MessageBox.Show("请选择绘图数据库");
                res = false;
            }
            else if (string.IsNullOrEmpty(JH))
            {
                MessageBox.Show("请选择绘图井号");
                res = false;
            }
            else if (_jdStrLst.Count < 1)
            {
                MessageBox.Show("请设计绘图井段");
                res = false;
            }
            
            return res;
        }


        public static Boolean Validate(MainForm mf)
        {
            Boolean res = true;
            DrawDBName = "Sql";
        //    JH = "tt";
            if (DrawDBName.Equals(""))
            {
                MessageBox.Show("请选择绘图数据库");
                res = false;
            }
            else if (string.IsNullOrEmpty(JH))
            {
                MessageBox.Show("请选择绘图井号");
                res = false ;
            }
            else if (_jdStrLst.Count < 1)
            {
                MessageBox.Show("请设计绘图井段");
                res = false;
            }
            FrameDesign.set_MainForm_jdStrList_dele = mf.set_jdStrList;
            FrameDesign.set_MainForm_jdStrList_dele(_jdStrLst);  //2560-2590 2660-2690
            return res;
        }






   
    }
}
