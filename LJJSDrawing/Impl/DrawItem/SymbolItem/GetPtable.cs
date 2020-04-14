using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LJJSCAD.CommonData;
using DBHelper;
using System.Text.RegularExpressions;
using LJJSCAD.DrawingDesign.Frame;
using System.Reflection;
using LJJSCAD;
using System.Windows.Forms;
namespace LJJSCAD.LJJSDrawing.Impl.DrawItem.SymbolItem
{

    class GetPtable
    {


        public static string  MSEJ=null;
        static int startmsej = 0;
        static int endmsej = 0;
        static DataTable GongShiShu = Getgongshi();


        public double CXG = Convert.ToDouble(GongShiShu.Rows[0][1]);
        public double QXG = Convert.ToDouble(GongShiShu.Rows[1][1]);
        public double KXD = Convert.ToDouble(GongShiShu.Rows[2][1]);
        public double CTL1 = Convert.ToDouble(GongShiShu.Rows[3][1]);
        public double CTL2 = Convert.ToDouble(GongShiShu.Rows[4][1]);
        public double YD1 = Convert.ToDouble(GongShiShu.Rows[5][1]);
        public double YD2 = Convert.ToDouble(GongShiShu.Rows[6][1]);
        public double KZX1 = Convert.ToDouble(GongShiShu.Rows[7][1]);
        public double KZX2 = Convert.ToDouble(GongShiShu.Rows[8][1]);
        public double SXXS1 = Convert.ToDouble(GongShiShu.Rows[9][1]);
        public double SXXS2 = Convert.ToDouble(GongShiShu.Rows[10][1]);
        public string cxg = GongShiShu.Rows[0][0].ToString();
        public string qxg = GongShiShu.Rows[1][0].ToString();
        public string kxd = GongShiShu.Rows[2][0].ToString();
        public string ctl1 = GongShiShu.Rows[3][0].ToString();
        public string ctl2 = GongShiShu.Rows[4][0].ToString();
        public string yd1 = GongShiShu.Rows[5][0].ToString();
        public string yd2 = GongShiShu.Rows[6][0].ToString();
        public string kzx1 = GongShiShu.Rows[7][0].ToString();
        public string kzx2 = GongShiShu.Rows[8][0].ToString();
        public string sxxs1 = GongShiShu.Rows[9][0].ToString();
        public string sxxs2 = GongShiShu.Rows[10][0].ToString();





        public DataTable GetMseDesTable(DataTable MseSourceTable)
        {
            bool hasbizsize = false;
            foreach(DataColumn column in MseSourceTable.Columns)
            {
                if(column.ColumnName == "bitsize")
                {
                    hasbizsize = true;   //有bitsize这个列
                    break;
                }
            }
            if(hasbizsize ==false)   //没有bitsize这个列，就加这个列
            {
                DataColumn column = new DataColumn("bitsize");
                MseSourceTable.Columns.Add(column);
            }
            foreach (DataColumn column in MseSourceTable.Columns)
            {
                if (column.ColumnName == "gwjs")
                {
                    column.ColumnName = "depth";
                    break;
                }
            }

            double msejdepthmax = 0;
            double msejdepthmin = 0;
            if (MainForm.MSEJjd != null)
            {
                string[] msejDepthArr = Regex.Split(MainForm.MSEJjd, "-");
                 msejdepthmax = double.Parse(msejDepthArr[1]);  //这是深度的大值
                 msejdepthmin = double.Parse(msejDepthArr[0]);  //这是深度的小值
            }
            GetpGai(MseSourceTable);
           

            DataTable MseDesTable = new DataTable();
            DataColumn msev = new DataColumn();  //垂向功
            DataColumn mseh = new DataColumn(); //切向功
            DataColumn mse = new DataColumn();
            DataColumn mseb = new DataColumn();
            DataColumn msebqyp = new DataColumn();
            DataColumn msebyp = new DataColumn();
            DataColumn maxmin = new DataColumn();
            DataColumn maxmincount = new DataColumn();
            DataColumn average = new DataColumn();
            DataColumn fazhi = new DataColumn();
            DataColumn msej = new DataColumn();
            DataColumn p = new DataColumn();
            DataColumn kxd = new DataColumn();
            DataColumn ctl = new DataColumn();
            DataColumn yd = new DataColumn();
            DataColumn kzx = new DataColumn();
            DataColumn sxxs = new DataColumn();
            DataColumn depth = new DataColumn();

            DataColumn AfterMsev = new DataColumn();
            DataColumn AfterMseh = new DataColumn();

            DataColumn JinGeiLiang = new DataColumn();
            DataColumn AfterJinGeiLiang = new DataColumn();

            DataColumn AfterMseb = new DataColumn();


            MseDesTable.Columns.Add(msev);
            MseDesTable.Columns.Add(mseh);
            MseDesTable.Columns.Add(mse);
            MseDesTable.Columns.Add(mseb);
            MseDesTable.Columns.Add(msebqyp);
            MseDesTable.Columns.Add(msebyp);
            MseDesTable.Columns.Add(maxmin);
            MseDesTable.Columns.Add(maxmincount);
            MseDesTable.Columns.Add(fazhi);
            MseDesTable.Columns.Add(msej);
            MseDesTable.Columns.Add(average);
            MseDesTable.Columns.Add(p);
            MseDesTable.Columns.Add(kxd);
            MseDesTable.Columns.Add(ctl);
            MseDesTable.Columns.Add(yd);
            MseDesTable.Columns.Add(kzx);
            MseDesTable.Columns.Add(sxxs);
            MseDesTable.Columns.Add(depth);

            MseDesTable.Columns.Add(AfterMsev);
            MseDesTable.Columns.Add(AfterMseh);

            MseDesTable.Columns.Add(JinGeiLiang);
            MseDesTable.Columns.Add(AfterJinGeiLiang);

            MseDesTable.Columns.Add(AfterMseb);

            MseDesTable.Columns[0].ColumnName = "MSEV";
            MseDesTable.Columns[1].ColumnName = "MSEH";
            MseDesTable.Columns[2].ColumnName = "MSE";
            MseDesTable.Columns[3].ColumnName = "MSEB";
            MseDesTable.Columns[4].ColumnName = "MSEBQYP";
            MseDesTable.Columns[5].ColumnName = "MSEBYP";
            MseDesTable.Columns[6].ColumnName = "MAXMIN";
            MseDesTable.Columns[7].ColumnName = "MAXMINCOUNT";
            MseDesTable.Columns[8].ColumnName = "AVERAGE";
            MseDesTable.Columns[9].ColumnName = "FAZHI";
            MseDesTable.Columns[10].ColumnName = "MSEJ";
            MseDesTable.Columns[11].ColumnName = "P";
            MseDesTable.Columns[12].ColumnName = "kxd";
            MseDesTable.Columns[13].ColumnName = "ctl";
            MseDesTable.Columns[14].ColumnName = "yd";
            MseDesTable.Columns[15].ColumnName = "kzx";
            MseDesTable.Columns[16].ColumnName = "sxxs";
            MseDesTable.Columns[17].ColumnName = "depth";

            MseDesTable.Columns[18].ColumnName = "AfterMsev";
            MseDesTable.Columns[19].ColumnName = "AfterMseh";
            MseDesTable.Columns[20].ColumnName = "JinGeiLiang";
            MseDesTable.Columns[21].ColumnName = "AfterJinGeiLiang";
            MseDesTable.Columns[22].ColumnName = "AfterMseb";

            MseDesTable.Columns[11].DataType = typeof(System.Double);
            MseDesTable.Columns[12].DataType = typeof(System.Double);
            MseDesTable.Columns[13].DataType = typeof(System.Double);
            MseDesTable.Columns[14].DataType = typeof(System.Double);
            MseDesTable.Columns[15].DataType = typeof(System.Double);
            MseDesTable.Columns[16].DataType = typeof(System.Double);
            MseDesTable.Columns[17].DataType = typeof(System.Double);
            MseDesTable.Columns[18].DataType = typeof(System.Double);
            MseDesTable.Columns[19].DataType = typeof(System.Double);
            MseDesTable.Columns[20].DataType = typeof(System.Double);
            MseDesTable.Columns[21].DataType = typeof(System.Double);
            MseDesTable.Columns[22].DataType = typeof(System.Double);
            //  MseDesTable.Columns[12].ColumnName = "P";
            double WOBB = 80;
            double NB = 70;

            int msebqypcount = 0;
            int msebypcount = 0;
            int a = 0;
            int b = 0;
            foreach (DataRow row in MseSourceTable.Rows)
            {

                //foreach (DataColumn column in MseSourceTable.Columns)
                //{
                //    Console.WriteLine(row[column]); 
                //}
                //   double MSEV = CXG * ((Convert.ToDouble(row["wob"]) / ((3.14 / 4) * Convert.ToDouble(row["bitsize"]) * Convert.ToDouble(row["bitsize"]))) * 1000);
                
                //钻头半径
                
                

                if(DrawCommonData.drill_radius != null)
                {
                    row["bitsize"] = DrawCommonData.drill_radius;
                }
                else
                {
                    row["bitsize"] = 215.9;
                }
                double MSEV = (CXG * ((Convert.ToDouble(row["wob"]) / ((3.14 / 4) * Convert.ToDouble(row["bitsize"]) * Convert.ToDouble(row["bitsize"]))) * 1000)) / Convert.ToDouble(row["rop"]);
                //在此处修改钻时的值，如果是慢井可以考虑让钻时变大
                double MSEH = QXG * (((120 * 3.14 * Convert.ToDouble(row["rpm"]) * Convert.ToDouble(row["torque"])) / (((3.14 / 4) * Math.Pow(Convert.ToDouble(row["bitsize"]) / 1000, 2)) * ((1 / Convert.ToDouble(row["rop"])*10) * 60))) / 1000);
                double MSE = MSEV + MSEH;
                double MSEB = (Math.Pow(WOBB, -0.408) / Math.Pow(Convert.ToDouble(row["wob"]), -0.408)) * (Math.Pow(NB / Convert.ToDouble(row["rpm"]), 0.926)) * MSE;
                double MSEBQYP;
                double MSEBYP;
                double DEPYH = Convert.ToDouble(row["depth"]);
                
                double jinGeiLiang = (1 / Convert.ToDouble(row["rop"])*10) / Convert.ToDouble(row["rpm"]) * 1000;

                //  MseDesTable.Rows.Count
                //求MSEBQYP;
                if (MseDesTable.Rows.Count > 4)
                {
                    double msebqypsum = 0;
                    // int j = 0;  
                    for (int i = 0; i < 5; i++)
                    {
                        msebqypsum += Convert.ToDouble(MseDesTable.Rows[msebqypcount][3]) * (i + 1);
                        msebqypcount++;
                    }
                    MSEBQYP = msebqypsum / 15;
                    a++;
                    msebqypcount = a;
                }
                else
                {
                    MSEBQYP = 0;

                }
                //求MSEBYP;

                int mseyqcount = 100;
                if (MseDesTable.Rows.Count > mseyqcount - 1)//改
                {
                    double msebypsum = 0;
                    // int j = 0;  
                    for (int i = 0; i < mseyqcount; i++)//改
                    {
                        msebypsum += Convert.ToDouble(MseDesTable.Rows[msebypcount][3]);
                        msebypcount++;
                    }
                    MSEBYP = msebypsum / mseyqcount;//改
                    b++;
                    msebypcount = b;
                }
                else
                {
                    MSEBYP = 0;

                }


                DataRow dr = MseDesTable.NewRow();
             
             

                
                   dr[0] = MSEV; 
                if (MSEH > 1500) { dr[1] = 1500; }
                else
                {
                    dr[1] = MSEH;
                }
              //  if (MSEH == 0) { dr[1] = 10; }
                dr[2] = MSE;
               // if (double.IsInfinity(MSEB)) { dr[3] = 100; } else { dr[3] = MSEB; }
                dr[3] = MSEB;
             
                dr[4] = MSEBQYP;
                if (MseSourceTable.Rows.Count > 100)
                {
                    dr[5] = MSEBYP;
                }
                else
                {
                    dr[5] = MSEB;
                }
                dr[17] = DEPYH;
            //    if (double.IsInfinity(jinGeiLiang)) { dr[20] = 5000; }else { dr[20] = jinGeiLiang; }
                dr[20] = jinGeiLiang;
                MseDesTable.Rows.Add(dr);
            }
            // double min=Convert.ToDouble(MseDesTable.Rows[100][5]);
            double min;
            double max = 0;
            int count = 100;
            int aa = 0;
            int bb = 0;
            int maxcount = 0;
            int mincount;
            double averagezhi = 0;
            double averageshu = 0;
            double maxfazhishu = 0;
            double minfazhishu = 0;
            int fenduan = 0;
            int fenduanbl = 0;
            double fenduansum = 0;
            int Fenduancount = 100;





            if (MseDesTable.Rows.Count > 100)//给msej赋值
            {
                // Fenduancount = 100;


                for (int s = 0; s < MseDesTable.Rows.Count / Fenduancount; s++)
                {/**
                try  //significant
                {
                    min = Convert.ToDouble(MseDesTable.Rows[count][5]);
                }
                catch
                {
                    count = count - 1;
                }**/
                    min = Convert.ToDouble(MseDesTable.Rows[count][5]);
                    mincount = count;
                    max = Convert.ToDouble(MseDesTable.Rows[count][5]);
                    maxcount = count;
                    for (int i = 0; i < Fenduancount; i++)
                    {

                        if (count > MseDesTable.Rows.Count - 2) { break; }
                        else
                        {

                            if (min > Convert.ToDouble(MseDesTable.Rows[count][5]))
                            {
                                min = Convert.ToDouble(MseDesTable.Rows[count][5]);
                                mincount = count;

                            }
                            if (max < Convert.ToDouble(MseDesTable.Rows[count][5]))
                            {
                                max = Convert.ToDouble(MseDesTable.Rows[count][5]);
                                maxcount = count;
                            }
                            count++;

                        }
                    }

                    if (maxfazhishu > 0.3 && minfazhishu > 0.3)
                    {
                        averageshu = 0; bb = 0;
                        fenduan = (s + 1) * Fenduancount;
                    }
                    if (maxfazhishu < -0.3 && minfazhishu < -0.3) { averageshu = 0; bb = 0; fenduan = (s + 1) * Fenduancount; }



                    if ((s + 1) == (MseDesTable.Rows.Count / Fenduancount)) { fenduan = MseDesTable.Rows.Count; }
                    for (int qwe = fenduanbl; qwe < fenduan; qwe++)
                    {
                        fenduansum = fenduansum + Convert.ToDouble(MseDesTable.Rows[qwe][5]);

                    }
                    for (int qwe = fenduanbl; qwe < fenduan; qwe++)
                    {

                        MseDesTable.Rows[qwe][10] = fenduansum / (fenduan - fenduanbl);
                    }
                    if (maxfazhishu > 0.3 && minfazhishu > 0.3)
                    {
                        fenduanbl = fenduan;
                        fenduansum = 0;
                    }
                    if (maxfazhishu < -0.3 && minfazhishu < -0.3) { fenduanbl = fenduan; fenduansum = 0; }


                    // if (maxfazhishu < -0.03 && minfazhishu < -0.03) { averageshu = 0; bb = 0; fenduan = (s + 1) * 100;  }
                    averageshu = averageshu + max;
                    averagezhi = averageshu / (bb + 1);
                    MseDesTable.Rows[aa][6] = max;
                    MseDesTable.Rows[aa][7] = maxcount;
                    MseDesTable.Rows[aa][8] = averagezhi;
                    maxfazhishu = (max - averagezhi) / max;
                    MseDesTable.Rows[aa][9] = maxfazhishu;
                    aa++; bb++;
                    averageshu = averageshu + min;
                    averagezhi = averageshu / (bb + 1);
                    MseDesTable.Rows[aa][6] = min;
                    MseDesTable.Rows[aa][7] = mincount;
                    MseDesTable.Rows[aa][8] = averagezhi;
                    minfazhishu = (min - averagezhi) / min;
                    MseDesTable.Rows[aa][9] = minfazhishu;
                    aa++; bb++;

                }
                if (MseDesTable.Rows.Count < Fenduancount)
                {
                    for (int i = 0; i < MseDesTable.Rows.Count; i++)
                    {
                        MseDesTable.Rows[i][11] = 0;
                        MseDesTable.Rows[i][10] = 0;
                    }
                }



            }
            else
            {
                double xiaomsejsum = 0;
                int xiaomesjcount = 0;
                for (int i = 0; i < MseDesTable.Rows.Count; i++)
                {
                    xiaomsejsum = xiaomsejsum + Convert.ToDouble(MseDesTable.Rows[i][5]);
                    xiaomesjcount++;
                }
                for (int i = 0; i < MseDesTable.Rows.Count; i++)
                {

                    MseDesTable.Rows[i][10] = xiaomsejsum / xiaomesjcount;
                }

            }




            if (msejdepthmax != 0 && msejdepthmin !=0)
            {
                MSEJSET(msejdepthmax, msejdepthmin, MseDesTable);
                msejgai(MseDesTable);

            }





            string[] strDepthArr = Regex.Split(FrameDesign.JdStrLst[0], DrawCommonData.jdSplitter.ToString());
            double maxDepth = double.Parse(strDepthArr[1]);  //这是深度的大值
            double minDepth = double.Parse(strDepthArr[0]);  //这是深度的小值


            //msev的最大值最小值

            double minMsev = Convert.ToDouble(MseDesTable.Rows[0][0]);
            double maxMsev = Convert.ToDouble(MseDesTable.Rows[0][0]);
            //??????!!!!

            GetPtable.set_min_and_max(minDepth, maxDepth, ref minMsev, ref maxMsev, MseDesTable, "MSEV");

       

            double minMseh = Convert.ToDouble(MseDesTable.Rows[0][1]);
            double maxMseh = Convert.ToDouble(MseDesTable.Rows[0][1]);


            GetPtable.set_min_and_max(minDepth, maxDepth, ref minMseh, ref maxMseh, MseDesTable, "MSEH");
       
            double minMseb = Convert.ToDouble(MseDesTable.Rows[0][3]); ;
            double maxMseb = 0;


            GetPtable.set_min_and_max(minDepth, maxDepth, ref minMseb, ref maxMseb, MseDesTable, "MSEB");
          
            double minJGL = Convert.ToDouble(MseDesTable.Rows[0][20]); ;
            double maxJGL = 4.5;

            GetPtable.set_min_and_max(minDepth, maxDepth, ref minJGL, ref maxJGL, MseDesTable, "JinGeiLiang");

            for (int i = 0; i < MseDesTable.Rows.Count; i++)
            {


                double pppMsev = 0;
                pppMsev = (Convert.ToDouble(MseDesTable.Rows[i][0]) - minMsev) / (maxMsev - minMsev);
                if (pppMsev >= 1)
                {

                    MseDesTable.Rows[i][18] = 0.9999;
                }
                else
                {
                    MseDesTable.Rows[i][18] = pppMsev;
                }

                if (pppMsev <= 0 || double.IsNaN(pppMsev))
                {

                    MseDesTable.Rows[i][18] = 0.001;
                }

                double pppMseh = 0;
                pppMseh = (Convert.ToDouble(MseDesTable.Rows[i][1]) - minMseh) / (maxMseh - minMseh);
                if (pppMseh >= 1)
                {
                    MseDesTable.Rows[i][19] = 0.9999;
                }
                else
                {
                    MseDesTable.Rows[i][19] = pppMseh;
                }
                if (pppMseh <= 0 || double.IsNaN(pppMseh))
                {

                    MseDesTable.Rows[i][19] = 0.01;
                }



                double pppJGL = 0;
                pppJGL = (Convert.ToDouble(MseDesTable.Rows[i][20]) - minJGL) / (maxJGL - minJGL);
                if (pppJGL >= 1)
                {
                    MseDesTable.Rows[i][21] = 0.9999;
                }
                else
                {
                    MseDesTable.Rows[i][21] = pppJGL;
                }
                if (pppJGL <= 0)
                {
                    MseDesTable.Rows[i][21] = 0.001;
                }
                double pppMSEB = 0;
                pppMSEB = (Convert.ToDouble(MseDesTable.Rows[i][3]) - minMseb) / (maxMseb - minMseb);
                if (pppMSEB >= 1)
                {
                    MseDesTable.Rows[i][22] = 0.9999;
                }
                else
                {
                    MseDesTable.Rows[i][22] = pppMSEB;
                }



            }

            this.change(MseDesTable); 
  


            for (int i = 0; i < MseDesTable.Rows.Count; i++)
            {


                double ppp = 0;
                ppp = Convert.ToDouble(MseDesTable.Rows[i][4]) / Convert.ToDouble(MseDesTable.Rows[i][10]);
                if (ppp > 1)
                {
                    MseDesTable.Rows[i][11] = 0.9999;
                }
                else
                {
                    MseDesTable.Rows[i][11] = ppp;
                    //   MseDesTable.Rows[i][11] = Convert.ToDouble(MseDesTable.Rows[i][4]) / Convert.ToDouble(MseDesTable.Rows[i][10]);
                }


                double kxd_temp = Math.Log(Convert.ToDouble(MseDesTable.Rows[i][11]), 2.7182) * (KXD);
                if (kxd_temp > 10000)
                {
                    kxd_temp = 0;
                }
                MseDesTable.Rows[i][12] = kxd_temp;

                MseDesTable.Rows[i][13] = (Math.Pow(2.7182, CTL2 * Convert.ToDouble(MseDesTable.Rows[i][12]))) * (CTL1);
                MseDesTable.Rows[i][14] = (Math.Pow(Convert.ToDouble(MseDesTable.Rows[i][3]), YD2)) * (YD1);
                MseDesTable.Rows[i][15] = (Math.Pow(Convert.ToDouble(MseDesTable.Rows[i][3]), KZX2)) * (KZX1);
                MseDesTable.Rows[i][16] = 1 / ((Math.Log(Convert.ToDouble(MseDesTable.Rows[i][3]), 2.7182)) * (SXXS1) + SXXS2);


                double pppMsev = Convert.ToDouble(MseDesTable.Rows[i][18]);
                //pppMsev = (Convert.ToDouble(MseDesTable.Rows[i][0]) - minMsev) / (maxMsev - minMsev);
                if (pppMsev >= 1)
                {

                    MseDesTable.Rows[i][18] = 0.9999;
                }
                else
                {
                    MseDesTable.Rows[i][18] = pppMsev;
                }

                if (pppMsev <= 0 || double.IsNaN(pppMsev))
                {

                    MseDesTable.Rows[i][18] = 0.001;
                }

                double pppMseh = Convert.ToDouble(MseDesTable.Rows[i][19]);
                //pppMseh = (Convert.ToDouble(MseDesTable.Rows[i][1]) - minMseh) / (maxMseh - minMseh);
                if (pppMseh >= 1)
                {
                    MseDesTable.Rows[i][19] = 0.9999;
                }
                else
                {
                    MseDesTable.Rows[i][19] = pppMseh;
                }
                if (pppMseh <= 0 || double.IsNaN(pppMseh))
                {

                    MseDesTable.Rows[i][19] = 0.01;
                }



                double pppJGL =Convert.ToDouble(MseDesTable.Rows[i][21]);
                //pppJGL = (Convert.ToDouble(MseDesTable.Rows[i][20]) - minJGL) / (maxJGL - minJGL);
                if (pppJGL >= 1)
                {
                    MseDesTable.Rows[i][21] = 0.9999;
                }
                else
                {
                    MseDesTable.Rows[i][21] = pppJGL;
                }
                if (pppJGL <= 0)
                {
                    MseDesTable.Rows[i][21] = 0.001;
                }
                double pppMSEB = Convert.ToDouble(MseDesTable.Rows[i][22]);
                //pppMSEB = (Convert.ToDouble(MseDesTable.Rows[i][3]) - minMseb) / (maxMseb - minMseb);
                if (pppMSEB >= 1)
                {
                    MseDesTable.Rows[i][22] = 0.9999;
                }
                else
                {
                    MseDesTable.Rows[i][22] = pppMSEB;
                }
            }
          


            DrawCommonData.ptable = MseDesTable;
            pTableContainer.itemDataTable = MseDesTable;

            return MseDesTable;



        }

        public DataRow copy_row(DataTable ori,DataRow row)  //复制一行
        {
            
            //DataRow ret = row.Table.NewRow();
            DataRow ret = ori.NewRow();
            for (int i = 0; i < row.Table.Columns.Count; i++ )
            {
                ret[i] = row[i];
            }
            return ret;
        }


        public DataTable copy(DataTable dt, int startindex, int endindex) //复制一个datatable，行从start到endindex
        {

            DataTable ret = dt.Clone();
            for (int i = startindex; i <= endindex;i++)
            {
                DataRow dr = this.copy_row(ret,dt.Rows[i]);
              
                ret.Rows.Add(dr);
            }
            return ret;
        }

       public List<DataTable> split_one_dt_to_two(DataTable dt,int split_index)
        {  //从splitindex的位置，产生两个datatable，第一个的行是0到splitindex-1，第二个的行是从splitindex到最后。
            List<DataTable> ret = new List<DataTable>();
            ret.Add(this.copy(dt, 0, split_index - 1));
            ret.Add(this.copy(dt, split_index, dt.Rows.Count - 1));


            return ret;

        }

        public List<DataTable> split_dt_by_DEPTH(DataTable dt)  //如果depth之间有不连贯的，就分割为两个datatable
        {
            List<DataTable> ret = new List<DataTable>();
            int prev_index = 0;
            for (int i = 0; i < dt.Rows.Count ; i++ )
            {
                if (i == dt.Rows.Count - 1)
                {
                    //到头了
                    DataTable split_dt = this.copy(dt, prev_index+1, i);
                    ret.Add(split_dt);
                    break;
                }


                double small_depth = double.Parse(dt.Rows[i]["DEPTH"].ToString());
                double big_depth = double.Parse(dt.Rows[i+1]["DEPTH"].ToString());
                
                if(big_depth - small_depth > 0.2)
                {
                    //要分成两个datatable
                    DataTable split_dt = this.copy(dt, prev_index, i);
                    ret.Add(split_dt);
                    prev_index = i;
                }
                
            }

            return ret;
        }

        public void change(DataTable dt)
        {
           
            string sql = string.Format("use DQLREPORTDB select * from P_TABLE where  jh='{0}' order by DEPTH asc",  FrameDesign.JH);
            DataTable newdt = SqlServerDAL.gettable(sql);
            newdt.Columns.Remove("jh");
            List<DataTable> dts = this.split_dt_by_DEPTH(newdt);
            for (int i = 0; i < dts.Count-1;i++ )
            {

                string up = dts[i].Rows[dts[i].Rows.Count - 1]["depth"].ToString();
                string down = dts[i+1].Rows[0]["depth"].ToString();
                if(up == down)
                {
                    dts[i + 1].Rows.RemoveAt(0);
                }
            }


            foreach (DataTable item in dts)
            {
                List<int> index = this.find_depth_index(dt, item);
                for (int i = index[0], j = 0; i <= index[1]; i++, j++)
                {
                    try
                    {
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            dt.Rows[i][k] = item.Rows[j][k];
                        }
                    }
                    catch
                    {
                        break;
                    }

                }
            }   
            //
            int kk = 9;
            

        }

       

        public List<int> find_depth_index(DataTable bigdt, DataTable smdt)
        {   
            List<int> ret = new List<int>();

            double upper_depth = double.Parse(smdt.Rows[0]["DEPTH"].ToString());
            double lower_depth = double.Parse(smdt.Rows[smdt.Rows.Count - 1]["DEPTH"].ToString());

            for(int i =0;i<bigdt.Rows.Count;i++)
            {
                double curr_depth = double.Parse(bigdt.Rows[i]["DEPTH"].ToString());
                if(curr_depth == upper_depth)
                {
                    ret.Add(i);

                }
                if(curr_depth == lower_depth)
                {
                    ret.Add(i);
                    break;
                }
            }
            
            //ret[0] 是索引的上界 【1】是下界
            return ret;
        }



        public DataTable create_new_dt_by_depth(DataTable dt,int startDepth, int endDepth)
        {  //返回新的datatable， 深度介于startDepth 和endDepth之间
            int startIndex = 0, endIndex = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (double.Parse(dt.Rows[i]["depth"].ToString()) >= startDepth)
                {
                    startIndex = i;
                    break;
                }

            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (double.Parse(dt.Rows[i]["depth"].ToString()) >= endDepth)
                {
                    endIndex = i;
                    break;
                }
            }

            if (endIndex == 0)
            {
                endIndex = dt.Rows.Count - 1;
            }

            DataTable ret = new DataTable();
            foreach (DataColumn dc in dt.Columns)
            {

                ret.Columns.Add(dc.ColumnName);
           }

            for(int i = startIndex; i<= endIndex ;i++)
            {
               
                ret.Rows.Add(dt.Rows[i].ItemArray);
            }
            return ret;

        }


        public void insert_table_into_db(DataTable dt,int startDepth, int endDepth,string jh)
        {
            DataTable insert_dt = this.create_new_dt_by_depth(dt, startDepth, endDepth);
            DataColumn jh_column = new DataColumn("jh");
            insert_dt.Columns.Add(jh_column);
            foreach(DataRow row in insert_dt.Rows)
            {
                row["jh"] = jh;
            }
            
            SqlServerDAL.SqlBulkCopyColumnMapping("P_TABLE", insert_dt);


            
        }
        

        public void GetpGai(DataTable ptable) {
            if (Convert.ToDouble(ptable.Rows[0]["wob"]) == 0) { ptable.Rows[0]["wob"]=0.01;}
            if (Convert.ToDouble(ptable.Rows[0]["rpm"]) == 0) { ptable.Rows[0]["rpm"] = 0.01; }
            if (Convert.ToDouble(ptable.Rows[0]["rop"]) == 0) { ptable.Rows[0]["rop"] = 0.01; }
            if (Convert.ToDouble(ptable.Rows[0]["torque"]) == 0) { ptable.Rows[0]["torque"] = 0.01; }

            for (int i = 0; i < ptable.Rows.Count; i++) { 
            
            if(Convert.ToDouble(ptable .Rows [i]["wob"])==0){

                ptable.Rows[i]["wob"] = ptable.Rows[i - 1]["wob"];

            }
          
                
                if (Convert.ToDouble(ptable.Rows[i]["rop"]) == 0)
            {

                ptable.Rows[i]["rop"] = ptable.Rows[i - 1]["rop"];

            }
               if (Convert.ToDouble(ptable.Rows[i]["rpm"]) == 0)
            {

                ptable.Rows[i]["rpm"] = ptable.Rows[i - 1]["rpm"];

            }
               if (Convert.ToDouble(ptable.Rows[i]["torque"]) == 0)
               {

                   ptable.Rows[i]["torque"] = ptable.Rows[i - 1]["torque"];

               }
            
            
            }


            /** 冒泡排序 对depth
            for (int i = 0; i < ptable.Rows.Count - 1; i++)
            {
                for (int j = 0; j < ptable.Rows.Count - 1 - i; j++)
                {
                    if(double.Parse(ptable.Rows[j]["depth"].ToString()) > double.Parse(ptable.Rows[j+1]["depth"].ToString()))
                    {
                        ptable = SwapRow(j, j + 1, ptable);
                    }
                }
            }
            **/
            //QuickSort_Main(ref ptable, "depth");

            DataTable sb = ptable;
        
        }


        


        public static void QuickSort_Main(ref DataTable dt,string column_Name_to_sort)
        {

            int _nLength = dt.Rows.Count;



            QuickSort(ref dt, 0, _nLength - 1, column_Name_to_sort);

            
            
        }
        //获取按枢轴值左右分流后枢轴的位置
        private static int Division(ref DataTable dt, int left, int right, string column_Name_to_sort)
        {
            while (left < right)
            {
                double num = double.Parse(dt.Rows[left][column_Name_to_sort].ToString()); //将首元素作为枢轴
                if (num > double.Parse(dt.Rows[left + 1][column_Name_to_sort].ToString()))
                {

                    dt = SwapRow(left, left + 1, dt);

                    
                    left++;
                }
                else
                {

                    dt = SwapRow(right, left + 1, dt);

                    
                    right--;
                }
                
            }
            
            return left; //指向的此时枢轴的位置
        }
        private static void QuickSort(ref DataTable dt, int left, int right, string column_Name_to_sort)
        {
            if (left < right)
            {
                int i = Division(ref dt, left, right, column_Name_to_sort);
                //对枢轴的左边部分进行排序
                QuickSort(ref dt, i + 1, right, column_Name_to_sort);
                //对枢轴的右边部分进行排序
                QuickSort(ref dt, left, i - 1, column_Name_to_sort);
            }
        }    



        public static DataTable SwapRow(int index1, int index2, DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr.ItemArray = dt.Rows[index1].ItemArray;
            dt.Rows[index1].ItemArray = dt.Rows[index2].ItemArray;
            dt.Rows[index2].ItemArray = dr.ItemArray;
            return dt;
        }
        public static void msejgai(DataTable dt)
        {

            for (int i = endmsej; i < startmsej; i++)
            {
                dt.Rows[i]["MSEJ"] = DrawCommonData.base_value;
             
            }
        
        }


        public static void MSEJSET(double mindepthmsej, double maxdepthmsej, DataTable dt)
        {
           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (double.Parse(dt.Rows[i]["depth"].ToString()) >= mindepthmsej)
                {
                    startmsej = i;
                    break;
                }

            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (double.Parse(dt.Rows[i]["depth"].ToString()) >= maxdepthmsej)
                {
                    endmsej = i;
                    break;
                }
            }

            if (endmsej == 0) { endmsej = dt.Rows.Count - 1; }
               MSEJ=dt.Rows[startmsej]["MSEJ"].ToString();
              // MainForm form = mainform;
                  // MainForm  form = new MainForm () ;
               //MainForm  form = new MainForm ();
            //MainForm.TXTmsej.Text = MSEJ;
               if (DrawCommonData.base_value == null) {
                   DrawCommonData.base_value = MSEJ;
               }
              
       
        }



        public static void set_min_and_max(double minDepth, double maxDepth, ref double minValue, ref double maxValue, DataTable dt, string columnName)
        {
            //寻找最大值和最小值



            int startindex = 0;
            int endindex = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (double.Parse(dt.Rows[i]["depth"].ToString()) >= minDepth)
                {
                    startindex = i;
                    break;
                }
                
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (double.Parse(dt.Rows[i]["depth"].ToString()) >= maxDepth)
                {
                    endindex = i;
                    break;
                }
            }

            if (endindex == 0) { endindex = dt.Rows.Count-1; }
            if (dt.Rows.Count > 500)
            {
                for (int jj = 0; jj < 30; jj++)
                {
                    double maxmax = 0;
                    int maxcount = 0;
                    for (int i = startindex; i <= endindex; i++)
                    {
                        if (maxmax < double.Parse(dt.Rows[i][columnName].ToString()))
                        {
                            maxmax = double.Parse(dt.Rows[i][columnName].ToString());
                            maxcount = i;
                        }
                    }
                    dt.Rows[maxcount][columnName] = dt.Rows[1][columnName].ToString();

                }
            }





            for (int i = startindex; i <= endindex; i++)
            {
                if (maxValue < double.Parse(dt.Rows[i][columnName].ToString()))
                {
                    maxValue = double.Parse(dt.Rows[i][columnName].ToString());

                }

                if (minValue > double.Parse(dt.Rows[i][columnName].ToString()))
                {
                    minValue = double.Parse(dt.Rows[i][columnName].ToString());

                }

            }

            Type draw_common_data = typeof(DrawCommonData);
            FieldInfo[] fields = draw_common_data.GetFields();
            foreach (FieldInfo field in fields)   //如果用户手动设置了最小值最大值，则会进入这个循环，否则不会
            {
                if (columnName.Equals("msev", StringComparison.CurrentCultureIgnoreCase))
                {
                    //如果列是msev，则看静态类中用户设置的msevmax和msevmin是否为null，不为null则赋值给max和minvalue
                    if (field.Name.Equals("msevmax", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (field.GetValue(null) != null)
                        {
                            maxValue = double.Parse(field.GetValue(null).ToString());
                        }

                    }
                    if (field.Name.Equals("msevmin", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (field.GetValue(null) != null)
                        {
                            minValue = double.Parse(field.GetValue(null).ToString());
                        }

                    }

                }

                if (columnName.Equals("mseh", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (field.Name.Equals("msehmax", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (field.GetValue(null) != null)
                        {
                            maxValue = double.Parse(field.GetValue(null).ToString());
                        }

                    }
                    if (field.Name.Equals("msehmin", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (field.GetValue(null) != null)
                        {
                            minValue = double.Parse(field.GetValue(null).ToString());
                        }

                    }

                }
                if (columnName.Equals("mseb", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (field.Name.Equals("msebmax", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (field.GetValue(null) != null)
                        {
                            maxValue = double.Parse(field.GetValue(null).ToString());
                        }

                    }
                    if (field.Name.Equals("msebmin", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (field.GetValue(null) != null)
                        {
                            minValue = double.Parse(field.GetValue(null).ToString());
                        }

                    }

                }
                if (columnName.Equals("jingeiliang", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (field.Name.Equals("jglmax", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (field.GetValue(null) != null)
                        {
                            maxValue = double.Parse(field.GetValue(null).ToString());
                        }

                    }
                    if (field.Name.Equals("jglmin", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (field.GetValue(null) != null)
                        {
                            minValue = double.Parse(field.GetValue(null).ToString());
                        }

                    }

                }
            }

            foreach (FieldInfo field in fields)
            {
                if (columnName.Equals("msev", StringComparison.CurrentCultureIgnoreCase) && field.Name.Contains("Msev"))
                {
                    if (field.Name.Contains("curr") && field.Name.Contains("Max") && field.Name.Contains("depth") == false)
                    {
                        //给静态类中的 现在的最大值 都赋值
                        field.SetValue(null, maxValue);
                    }
                    if (field.Name.Contains("curr") && field.Name.Contains("Min") && field.Name.Contains("depth") == false)
                    {
                        //给静态类中的 现在的最小值 都赋值
                        field.SetValue(null, minValue);
                    }
                }
                if (columnName.Equals("mseh", StringComparison.CurrentCultureIgnoreCase) && field.Name.Contains("Mseh"))
                {
                    if (field.Name.Contains("curr") && field.Name.Contains("Max") && field.Name.Contains("depth") == false)
                    {
                        //给静态类中的 现在的最大值 都赋值
                        field.SetValue(null, maxValue);
                    }
                    if (field.Name.Contains("curr") && field.Name.Contains("Min") && field.Name.Contains("depth") == false)
                    {
                        //给静态类中的 现在的最小值 都赋值
                        field.SetValue(null, minValue);
                    }
                }
                if (columnName.Equals("mseb", StringComparison.CurrentCultureIgnoreCase) && field.Name.Contains("Mseb"))
                {
                    if (field.Name.Contains("curr") && field.Name.Contains("Max") && field.Name.Contains("depth") == false)
                    {
                        //给静态类中的 现在的最大值 都赋值
                        field.SetValue(null, maxValue);
                    }
                    if (field.Name.Contains("curr") && field.Name.Contains("Min") && field.Name.Contains("depth") == false)
                    {
                        //给静态类中的 现在的最小值 都赋值
                        field.SetValue(null, minValue);
                    }
                }
                if (columnName.Equals("jingeiliang", StringComparison.CurrentCultureIgnoreCase) && field.Name.Contains("JGL"))
                {
                    if (field.Name.Contains("curr") && field.Name.Contains("Max") && field.Name.Contains("depth") == false)
                    {
                        //给静态类中的 现在的最大值 都赋值
                        field.SetValue(null, maxValue);
                    }
                    if (field.Name.Contains("curr") && field.Name.Contains("Min") && field.Name.Contains("depth") == false)
                    {
                        //给静态类中的 现在的最小值 都赋值
                        field.SetValue(null, minValue);
                    }
                }
            }
            
        }

        public static double findMax(DataTable dt, string columnName)
        {
            double max = -999;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (max < double.Parse(dt.Rows[i][columnName].ToString()))
                {
                    max = double.Parse(dt.Rows[i][columnName].ToString());
                }
            }
            return max;
        }
        public static double findMin(DataTable dt, string columnName)
        {
            double min = -999;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (min > double.Parse(dt.Rows[i][columnName].ToString()))
                {
                    min = double.Parse(dt.Rows[i][columnName].ToString());
                }
            }
            return min;
        }

        static public DataTable Getgongshi()
        {

            string sqlstr = string.Format("use DQLREPORTDB select * from GongShiCanShu ");
            var wellReader = DataAccessFactory.CreateDataAccess(DBConfigure.LJJSDrawDb, DBConfigure.drawConStr);
            //  return wellReader.ExecuteReader(sqltxt);
            try
            {
                using (DataTable dt = wellReader.GetTable(sqlstr))
                {
                    return dt;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }




    }
}
