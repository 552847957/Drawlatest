using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LJJSCAD.CommonData
{
    class MyTableManage
    {
        public static readonly string CJJSDrawingDBName = "CJJSDrawingDB.mdb";//测井解释图绘制参数管理数据库
        public static readonly string DrawingFrameTbName = "DrawingFrameTb";//解释图图幅模板设置表
        public static readonly string LineItemTbName = "LineItemTb";//测井曲线项管理表；
        public static readonly string SymbolItemTbName = "SymbolItemTb";//测井曲线符号项管理表
        public static readonly string TextItemTbName = "TextItemTb";//文本项管理表;
        public static readonly string ImageItemTbName = "ImageItemTb";//图片绘图项管理表，需要引入图片的绘图项;
        public static readonly string HCGZItemTbName = "HCGZItemTb";//核磁共振类设计，即带T2截止值得谱图
        public static readonly string CurveHasHatchItemModel = "CurveHasHatchItemModel";//带填充的曲线项的设计表；

        public static readonly string MultiCurveHatchTb = "MultiHatchCurveItemModel";//多级填充曲线项
        public static readonly string NormalPuTuTb = "NormalPuTuItemModel";//普通谱图
        public static readonly string HatchRectItemModelTb = "HatchRectItemModel";//带填充的直方图绘图项；例如镜下荧光的油脂沥青发光面积，胶质沥青发光面积；水溶烃发光面积


        public static readonly string DrawingJHTbName = "kf_table";//绘图井号所在的表;
        public static readonly string DrawingHeaderTbName = "kf_table";//图头表数据所对应的表;
        public static readonly string XMLFileNameLineRoadManage = "XMLLineRoadManage.xml";//对线道设置的管理得XMl；
        public static readonly string XMLFileNameDrawingItemManage = "XMLDrawingItemManage.xml";//对绘图项管理的XML;
        public static readonly string KeDuChiTbName = "KeDuChiTb";
        public static readonly string XMLFileNameKeDuChiManage = "XMLKeDuChi.xml";
        public static readonly string XMLFileNameDrawingFrame = "DrawingFrame.xml";
        public static readonly string XMLFileNameLegend = "LegendCode.xml";
        public static readonly string XMLFileNameTxtlegend = "TxtLegendcode.xml";
        public static readonly string SymbolCodeTbName = "SymbolCodeTb";
        public static readonly string SpecialItemParTbName = "SpecialParm";//保存特殊线道，固定参数的表；

        public static readonly string BHDPZTable = "BHD";//饱和度配置表名
        public static readonly string JSPZTable = "JSini";//井深配置表名

        public static readonly string ColorCodeTable="ColorCodeTb";


        #region<查看数据SQL>
        /// <summary>
        /// 查看数据表的SQl
        /// </summary>
        /// <returns></returns>
        public static string get_sql_wxcsfx_all()
        {
            return "select jh as 井号,js as 井深,qsjs as 起始井深,yph as 样品号,jdwz as 距顶位置,ypcd as 样品长度,bzw as 样品类别,kxd as 空隙度,stl as 渗透率,hybhd as 含油饱和度,sbhd as 水饱和度,qbhd as 气饱和度,lhy as 氯化盐,nzhl as 泥质含量,hzhl as 灰质含量,ysmd as 岩石密度,ds as 滴水,szj as 示踪剂,sytj as 使用图件,cs as 筒次 from " + wxcsfxTB;
        }

        public static string get_sql_az13_all()
        {
            return "select qxcs as 取芯次数,djsd1 as 起始井深,djsd2 as 终止井深,jc as 进尺,yxcd as 心长,shl1 as 收获率,continuous as 连续设计 from " + az_13;
        }

        public static string get_sql_yxgwjms_all()
        {
            return "select cs as 筒次,qsjs as 起始井深,zzjs as 终止井深,yxdkyxds1 as 单块岩心顶部深度,yxdkyxds2 as 单块岩心底部深度,yxmsqk as 岩心磨损情况,yxdkcd as 岩心单块长度,yxljcd as 岩心累计长度,ysdh as 颜色,yxdm as 岩心,ypmgm as 样品磨光面位置,hyjbmc as 含油级别,hyw as 含有物 from " + yxpmDataPreTb;
        }
        public static string get_sql_yxgwjmsDesign_all()//设计表
        {
            return "select  SubmitTbField as 提交表字段,SubmitTbFieldName as 提交表字段名,SourceTbField as 源表字段,FieldId as 字段ID,MyDataType as 数据类型,DealClass as 处理类型,SourceTbName as 源表名,SubmitTbName as 提交表名 from " + yxgwjmsDesignTb;
        }
        #endregion

        public static readonly string yxpmDataPreTb = "yxgwjms";
        public static readonly string yxpmTb = "JSPJSJ_yx";
        public static readonly string wxcsfxsource = "az13_3";
        public static readonly string wxcsfxTB = "wxcsfx";//wxcsfx表  
        public static readonly string az_13 = "az13";//az13表  
        public static readonly string yxjbsj = "yxjbsj";//yxjbsj表  

        public static readonly string yxgwjmsDesignTb = "yxgwjmsDesign";
        public static readonly string yxgwDtName = "yxgwjms";//岩心归位表在DataSet中的DataTable表名；
        public static readonly string wxcsfxDtName = "wxcsfx";//物性参数分析表在DataSet中的DataTable表名；

        public static string DrawDBConnstr;
        public static string PreDataTbConnStr
        {
            get
            {
                string dbpath = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source =" + Application.StartupPath + "\\" + "PreDataOperManage.mdb";//获取当前目录;
                return dbpath;
            }
        }



        //public static string GetCJJSDrawingDBCurPath()//包括数据库文件名称；
        //{
        //    string dbpath = LJJSReg.LJJSRegInfo.readLJJSInstall() + "\\" + CJJSDrawingDBName;//获取当前目录;
        //    return dbpath;
        //}
        public static string GetWorkPath()
        {
            return Application.StartupPath;
        }
        public static string GetLegendCodeXMLFilePath()
        {
            string path = GetWorkPath() + "\\" + XMLFileNameLegend;//获取图例目录;
            return path;
        }
        public static string GetTxtLegendcodeXMLFilePath()
        {
            string path = GetWorkPath() + "\\" + XMLFileNameTxtlegend;//获取图例目录;
            return path;
        }
        //public static string GetCJJSDrawingDBConnStr()
        //{
        //    string dbconnStr = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source =" + GetCJJSDrawingDBCurPath();
        //    return dbconnStr;
        //}

        /// <summary>
        /// 方法:返回一个连接字符串，其连接直接连接到绘图数据库
        /// </summary>
        /// <param name="DBPath">绘图数据库路径,DrawingFrameData.DrawDBName</param>
        /// <returns></returns>
        public static void GetDBConnStr(string DBPath)
        {
            string dbpath = DBPath.Trim();//去括号处理
            DrawDBConnstr = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source =" + dbpath;

        }
        public static string GetExcelDBConnStr(string DBPath)
        {
            string dbpath = DBPath.Trim();
            string restr = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source =" + dbpath;
            return restr;
        }

        public static string GetDrawingItemXMLFilePath()
        {
            string drawingitemxmlpath = GetWorkPath() + "\\" + XMLFileNameDrawingItemManage;
            return drawingitemxmlpath;
        }
        public static string GetLineRoadXMLFilePath()
        {
            string lineroadxmlpath = GetWorkPath() + "\\" + XMLFileNameLineRoadManage;
            return lineroadxmlpath;
        }
        public static string GetKeDuChiXMLFilePath()
        {
            string keduchixmlpath = GetWorkPath() + "\\" + XMLFileNameKeDuChiManage;
            return keduchixmlpath;

        }
        public static string GetDrawingFramXMLFilePath()
        {
            string xmlpath = GetWorkPath() + "\\" + XMLFileNameDrawingFrame;
            return xmlpath;
        }

    }
}
