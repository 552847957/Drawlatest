using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Windows.Forms;
using LJJSCAD.CommonData;
using DBHelper;
using System.Data;
using LJJSCAD.DrawingDesign.Frame;
using DesignEnum;
using System.Text.RegularExpressions;
using LJJSCAD.LJJSDrawing.Impl;

namespace LJJSCAD.DAL
{
    class ItemDAL
    {
        private DrawItemStyle _itemstyle;
        public ItemDAL(DrawItemStyle itemstyle)
        {
            _itemstyle = itemstyle;
        }
        //JSPJSJ_YX
        public static DataTable GetItemWorkDataTable(string fromTableName)
        {
            //string sql = "select distinct * from " + fromTableName + @" where jh='" + FrameDesign.JH + "'";
            //order by depth asc

           


            if (fromTableName.Equals("JSPJSJ_QTYSDJJ", StringComparison.CurrentCultureIgnoreCase) || fromTableName.Equals("JSPJSJ_PLYL", StringComparison.CurrentCultureIgnoreCase) || fromTableName.Equals("JSPJSJ_CDLY", StringComparison.CurrentCultureIgnoreCase))
            {
                //JSPJSJ_CDLY
                string sql1 = String.Format("select distinct * from {0} where jh = '{1}' order by gwjs asc", fromTableName, FrameDesign.JH);
                DataTable dt1 = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSDrawDb, DBConfigure.drawConStr).GetTable(sql1);
                return dt1;
            }

            if (fromTableName.Equals("JSPJSJ_PLYL;JSPJSJ_CDLY",StringComparison.CurrentCultureIgnoreCase))
            {
                string sql1 = String.Format("use DQLREPORTDB select distinct JSPJSJ_plyl.rop,JSPJSJ_cdly.rpm,JSPJSJ_cdly.torque,JSPJSJ_cdly.wob,JSPJSJ_cdly.gwjs from JSPJSJ_plyl inner join  JSPJSJ_cdly on  JSPJSJ_plyl.gwjs = JSPJSJ_cdly.gwjs  and JSPJSJ_plyl.jh='{0}'and JSPJSJ_cdly.jh='{1}' order by gwjs", FrameDesign.JH, FrameDesign.JH);
                DataTable dt1 = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSDrawDb, DBConfigure.drawConStr).GetTable(sql1);
                return dt1;

            }
            if(fromTableName=="JSPJSJ_YX"){
                string sql1 = String.Format("select distinct * from {0} where jh = '{1}'", fromTableName, FrameDesign.JH);
                DataTable dt1 = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSDrawDb, DBConfigure.drawConStr).GetTable(sql1);
                return dt1;
            }
            if (fromTableName == "JSPJSJ_LJJSCGB")
            {
                string sql1 = String.Format("select distinct * from {0} where jh = '{1}'", fromTableName, FrameDesign.JH);
                DataTable dt1 = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSDrawDb, DBConfigure.drawConStr).GetTable(sql1);
                return dt1;
            
            }
            string sql = String.Format("select distinct * from {0} where jh = '{1}' order by depth asc", fromTableName, FrameDesign.JH);
            DataTable dt = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSDrawDb, DBConfigure.drawConStr).GetTable(sql);
            return dt;
        }
        //important
        
        public static DataTable SuiZuan_GetItemWorkDataTable(string fromTableName)
        {
            DrawPointContainer.fromtablename = fromTableName;

            //将  "2500-2600"  变为"2500"与"2600" 两个字符串
            string[] strDepthArr = Regex.Split(FrameDesign.JdStrLst[0], DrawCommonData.jdSplitter.ToString());

            double minDepth = double.Parse(strDepthArr[0]);  //这是深度的小值
            double maxDepth = double.Parse(strDepthArr[1]);  //这是深度的大值


            //string sqlstr = String.Format("select distinct * from {0} where jh = '{1}' and depth >={2} and depth<={3}", fromTableName, FrameDesign.JH, minDepth.ToString(), maxDepth.ToString());
            string sqlstr = String.Format("select distinct * from {0} where jh = '{1}' order by depth asc", fromTableName, FrameDesign.JH);
            
           // string sqlstr = String.Format("select distinct * from {0} where jh = '{1}' and depth >={2} and depth<={3} order by depth asc", fromTableName, FrameDesign.JH, minDepth.ToString(), maxDepth.ToString());
            DataTable dt = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSDrawDb, DBConfigure.drawConStr).GetTable(sqlstr);
            return dt;
           // ItemDAL.remove_redundant_depth_rows(dt);
            /**
            bool res = check_if_has_data_after(fromTableName);  //看在这个深度后面还有没有数据
            if (res == true)
            {  //随后有数据，接着随钻
                return dt;
            }
            else
            {  //随后没有数据，停止随钻
                DrawPointContainer.szf.Stop_Suizuan();
                return dt;
            }**/
            
        }

        public static bool check_if_has_data_after(string fromTableName)
        {
            /**
            if (!fromTableName.Equals("SCJSSJB_ZDJG") && DrawPointContainer.szf.timer1.Enabled == true)
            {
                //只查询实时表
                return true;
            }**/
            string[] strDepthArr = Regex.Split(FrameDesign.JdStrLst[0], DrawCommonData.jdSplitter.ToString());
            double maxDepth = double.Parse(strDepthArr[1]);  //这是深度的大值
            double minDepth = double.Parse(strDepthArr[0]);  //这是深度的小值

           // string sqlstr = String.Format("select distinct * from {0} where jh = '{1}' and depth > {2}", fromTableName, FrameDesign.JH, maxDepth.ToString());

            string sqlstr = String.Format("select max(depth) from SCJSSJB_ZDJG where jh = '{0}'", FrameDesign.JH);
            
            DataTable dt = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSDrawDb, DBConfigure.drawConStr).GetTable(sqlstr);
            try
            {
                double curr_maxdepth = double.Parse(dt.Rows[0][0].ToString());





                if (curr_maxdepth >= maxDepth)
                {
                    //出现新的数据
                    return true;
                }
                else
                {
                    //没有新的数据
                    return false;
                }
            }
            
            catch
            {
                return true;
            }

           

        }


        public void stopKedu()
        {
            SuiZuanForm sz = new SuiZuanForm();
            sz.timer1.Enabled = false;
            sz.timer2.Enabled = true;
            
        }



        public static void remove_redundant_depth_rows(DataTable dt)
        {
            List<DataRow> rows_to_be_removed = new List<DataRow>();
            //数据库中有许多行，其depth都是重复的，要把这些重复的depth都去掉，剩下的depth都是不重复的
            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                double depth1 = double.Parse(dt.Rows[i]["depth"].ToString());
                double depth2 = double.Parse(dt.Rows[i+1]["depth"].ToString());
                if (depth1 == depth2)
                {
                    rows_to_be_removed.Add(dt.Rows[i]);
                }
            }
            foreach (DataRow row in rows_to_be_removed)
            {
                dt.Rows.Remove(row);
            }
        }


        public DbDataReader GetDrawItemDataReader()
        {
            string sqltxt = "select * from ";
            DbDataReader itemreader;
            if (_itemstyle == DrawItemStyle.LineItem)
                sqltxt = sqltxt + MyTableManage.LineItemTbName + " order by  LineItemOrder ASC";
            else if (_itemstyle == DrawItemStyle.TextItem)
                sqltxt = sqltxt + MyTableManage.TextItemTbName + " order by  TxtItemOrder ASC";
            else if (_itemstyle == DrawItemStyle.ImageItem)
                sqltxt = sqltxt + MyTableManage.ImageItemTbName + " order by  ImageItemOrder ASC";
            else if (_itemstyle.Equals(DrawItemStyle.HCGZItem))
                sqltxt = sqltxt + MyTableManage.HCGZItemTbName + " order by HCGZItemOrder ASC";
            else if (_itemstyle.Equals(DrawItemStyle.MultiHatchCurveItem))
                sqltxt = sqltxt + MyTableManage.MultiCurveHatchTb + " order by ItemOrder ASC";

            else if (_itemstyle.Equals(DrawItemStyle.HatchRectItem))
                sqltxt = sqltxt + MyTableManage.HatchRectItemModelTb + " order by ItemOrder ASC";

            else if (_itemstyle.Equals(DrawItemStyle.CurveHasHatchItem))
                sqltxt = sqltxt + MyTableManage.CurveHasHatchItemModel + " order by ItemOrder ASC";

            else if (_itemstyle.Equals(DrawItemStyle.NormalPuTuItem))
                sqltxt = sqltxt + MyTableManage.NormalPuTuTb + " order by ItemOrder ASC";

            else if (_itemstyle.Equals(DrawItemStyle.SymbolItem))
                sqltxt = sqltxt + MyTableManage.SymbolItemTbName + " order by SymbolOrder ASC";

            else
            {
                sqltxt = "";
                return null;
            }
            try
            {
                IDataAccess data = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSConfigureDb, DBConfigure.configConStr);
                itemreader = data.ExecuteReader(sqltxt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
            return itemreader;
        }
    }
}
