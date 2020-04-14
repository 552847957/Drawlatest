using DBHelper;
using LJJSCAD.BlackBoard;
using LJJSCAD.CommonData;
using LJJSCAD.DataProcess.DrawControlData;
using LJJSCAD.Drawing.Manage;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.DrawingOper;
using LJJSCAD.LJJSDrawing.Impl;
using LJJSCAD.LJJSDrawing.Impl.DrawItem;
using LJJSCAD.LJJSDrawing.Interface;
using LJJSCAD.Util;
using LJJSCAD.WorkDataManage.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;

namespace LJJSCAD
{
    public partial class BaseLineForm : Form
    {
        public static int depthmax = 0;
        public static int depthmin = 0;
        public static int startcount = 0;
        public static int endcount = 0;
        double mesj3
            =0;


        public MainForm mf;
        public BaseLineForm()
        {
            InitializeComponent();

        }




        public void Fdraw_pic(bool redraw)
        {
            

            SQLServerWorkDataProvider.invoke_method = "绘图";  //用绘图的方式读取数据库，即一次读完所有数据，不用depth

            if(redraw == false)  
            {
                #region    对静态类进行初始化
                DrawPointContainer.initiate();
                DrawCommonData.Initiate();
                pTableContainer.initiate();
                #endregion
            }
            else  
            {
                DrawPointContainer.initiate();
                DrawCommonData.Initiate();
                
            }

            DrawCommonData.icount = 0;
            DrawCommonData.baseline = true;
            //绘图井，绘图框架等设计信息写入黑板区；
            this.mf.SetFrameDesign();
            //根据lb_LineRoadDesign重新整理线道设计顺序；
            //this.mf.SetLineRoadDesign();
            this.mf.ProperSetLineRoadDesign();


            //线道链表，线道框架即各个控制点数据等设计信息写入黑板区；
            FrameControlData.LineRoadControlLst = LineRoadOper.getLineRoadControlDataLst(LineRoadDesign.LineRoadDesginLst);
            WorkDataManage.WorkDataManage.BuildWorkDataHt();//获得绘图工作数据；将数据填充WorkDataManage类的WorkDataHt属性中；

            Entities.ClearAll();



            

            //绘图
            this.DrawLJJSPicture();

            
        }

        public void DrawLJJSPicture()
        {
            Childform frm = this.ActiveMdiChild as Childform;

            if (frm == null) return;

            DrawCommonData.activeDocument = frm.vdScrollableControl1.BaseControl.ActiveDocument;
           
            //   frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ZoomWindow(new VectorDraw.Geometry.gPoint(-170, 0), new VectorDraw.Geometry.gPoint(170.0, 200));

            LJJSBuilder ljjsBuilder = new LJJSBuilderImpl();
            LJJSDirector ljjsDirector = new LJJSDirector(ljjsBuilder);
            ljjsDirector.BuildLJJS();

            vdDocument activeDocu = this.cf.vdScrollableControl1.BaseControl.ActiveDocument;

            activeDocu.Redraw(true);

          

        }


        public Childform cf;
        private void BaseLineForm_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
           // this.mf.cf.MdiParent = this;
            Childform cf = new Childform();
            this.cf = cf;
            cf.MdiParent = this;
           
            cf.WindowState = FormWindowState.Maximized;
            cf.Show();
            /**
            vdEntities arr = this.mf.cf.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Entities;//获取主窗体中vd画的所有实体
            foreach(vdFigure figure in arr)
            {
                vdFigure copyfigure = (vdFigure)figure.Clone(cf.vdScrollableControl1.BaseControl.ActiveDocument);
                cf.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Entities.Add(copyfigure);
            }**/
            this.Fdraw_pic(true);   //没有点绘图按钮
            cf.vdScrollableControl1.BaseControl.ActiveDocument.ZoomExtents();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vdEntities arr = ((Childform)this.ActiveMdiChild).vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Entities;


            vdLine beforelastline = arr[arr.Count - 2] as vdLine;

            vdLine lastline = arr[arr.Count - 1] as vdLine;

            if(beforelastline!=null && lastline!=null)
            {
                MessageBox.Show("无法画线");
                return;
            }




            ((Childform)this.ActiveMdiChild).vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.CmdLine(null);



        }

        private void find_depth_gap()
        {
            vdEntities arr = ((Childform)this.ActiveMdiChild).vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Entities;
            List<vdLine> list = new List<vdLine>();
            //arr[arr.Count - 2], arr[arr.Count - 1]
            vdLine line1;
            vdLine line2;
            try
            {
                line1 = (vdLine)arr[arr.Count - 2];
                line2 = (vdLine)arr[arr.Count - 1];
            }
            catch
            {
                MessageBox.Show("还没有画线或者线的数目不够2个");
                return;
            }
            if (line1.StartPoint.y >= line2.StartPoint.y)
            {
                //line1在上面
                list.Add(line1);
                list.Add(line2);
            }
            else
            {
                list.Add(line2);
                list.Add(line1);
            }
            // DrawCommonData.startDepth1
            List<vdText> depthList = new List<vdText>();

            foreach (vdFigure figure in arr)
            {


                if (figure is vdText)
                {
                    vdText text = figure as vdText;
                    try
                    {
                        int depth = int.Parse(text.TextString);
                        if (depth >= DrawCommonData.upperdepth && depth <= DrawCommonData.lowerdepth)
                        {
                            depthList.Add(text);
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            int upper = this.find_nearest_depth(depthList, list[0]);  //深度上界

            int lower = this.find_nearest_depth(depthList, list[1]);  //深度下界
            if (upper < 0)
            {
                upper = lower - 2;
            }
            //MessageBox.Show("上界:" + upper.ToString());
            //MessageBox.Show("下界:" + lower.ToString());
            MessageBox.Show(upper.ToString() + "-" + lower.ToString());
            BaseLineForm.depthmin = upper;
            BaseLineForm.depthmax = lower;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.find_depth_gap();

        }
        public void xiugaijia(string lie, DataTable dt ) {
            SETcount(depthmin, depthmax, dt);
            for (int i = startcount; i < endcount; i++)
            {
               dt.Rows[i][lie]= double.Parse(dt.Rows[i][lie].ToString()) + 0.1;

            }
        }
        public void zuoyougai(string lie, string shu, DataTable dt)
        {
            bool isContainjia = shu.Contains("+");
            if (isContainjia) {
                SETcount(depthmin, depthmax, dt);
                shu = shu.Replace("+", string.Empty);
                for (int i = startcount; i < endcount; i++)
                {
                    dt.Rows[i][lie] = double.Parse(dt.Rows[i][lie].ToString()) + double.Parse(shu);

                }
            
            }
            bool isContainjian = shu.Contains("-");
            if (isContainjian) {
                shu = shu.Replace("-", string.Empty);
                SETcount(depthmin, depthmax, dt);
                for (int i = startcount; i < endcount; i++)
                {
                    dt.Rows[i][lie] = double.Parse(dt.Rows[i][lie].ToString()) - double.Parse(shu);

                }
            
            }

            
        }
        //public void zuoyougaijian(string lie, string shu, DataTable dt)
        //{
        //    shu = shu.Replace("-", string.Empty);
        //    SETcount(depthmin, depthmax, dt);
        //    for (int i = startcount; i < endcount; i++)
        //    {
        //        dt.Rows[i][lie] = double.Parse(dt.Rows[i][lie].ToString()) - double.Parse(shu);

        //    }
        //}
        public void xiugaijian(string lie, DataTable dt)
        {
            SETcount(depthmin, depthmax, dt);
            for (int i = startcount; i < endcount; i++)
            {
                dt.Rows[i][lie] = double.Parse(dt.Rows[i][lie].ToString()) - 0.1;

            }
        }

  
        public  void msej(DataTable dt)
        {
            SETcount(depthmin, depthmax, dt);
            for (int i = startcount; i < endcount; i++)
            {
                dt.Rows[i]["MSEJ"] = double.Parse(toolStripTextBox2.Text.ToString().Trim());

            }

        }

        public static void sort(DataTable dt, string sortname)
        {
            
        }

        public static void SETcount(double mindepthmsej, double maxdepthmsej, DataTable dt)
        {

        


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                if (double.Parse(dt.Rows[i]["depth"].ToString()) >= mindepthmsej)
                {
                    startcount = i;
                    break;
                }

            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (double.Parse(dt.Rows[i]["depth"].ToString()) >= maxdepthmsej)
                {
                    endcount = i;
                    break;
                }
            }

            if (endcount == 0) { endcount = dt.Rows.Count - 1; }
   


        }



        public int find_nearest_depth(List<vdText> depthList, vdLine line)
        {
            //public delegate int Comparison<in T>(T x, T y);
            Comparison<vdText> cmp = (vdText x, vdText y) =>
            {
                if (x.InsertionPoint.y > y.InsertionPoint.y)
                {
                    return -1;
                }
                else if (x.InsertionPoint.y == y.InsertionPoint.y)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            };
            depthList.Sort(cmp);  //将深度的文字从小到大排序

            for (int i = 0; i < depthList.Count; i++)
            {
                /**
                if(line.StartPoint.y<= depthList[i].InsertionPoint.y  && line.StartPoint.y>= depthList[i+1].InsertionPoint.y )
                {
                    return int.Parse(depthList[i + 1].TextString);
                }**/
                if (Math.Abs(line.StartPoint.y - depthList[i].InsertionPoint.y) <= 5)
                {
                    return int.Parse(depthList[i].TextString);
                }
            }
            return -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            xiugaijia("AfterMsev", pTableContainer.itemDataTable);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            xiugaijian("AfterMseh", pTableContainer.itemDataTable);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            msej(pTableContainer.itemDataTable);
        }

        async private void button6_Click(object sender, EventArgs e)
        {

            //if (toolStripTextBox1.Text.ToString().Trim() != null) {
            //    msej(pTableContainer.itemDataTable);
            
            //}
         
            delete_ptable(depthmin, depthmax);

            insert_table_into_db(pTableContainer.itemDataTable, depthmin, depthmax, FrameDesign.JH);
                //绘图
            // vdEntities arr = ((Childform)this.ActiveMdiChild).vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Entities;

            this.clear_previous_pic();//清空之前画的图

            await Task.Run(() => { this.Fdraw_pic(false); });
            //this.Fdraw_pic(false);  //点了绘图按钮



            cf.vdScrollableControl1.BaseControl.ActiveDocument.ZoomExtents();
        }
        public void delete_ptable(int depthmin,int depthmax)
        {
            string sqlstr = string.Format("USE DQLREPORTDB delete from P_TABLE where depth>={0} and depth<={1} and jh='{2}' ", depthmin, depthmax, FrameDesign.JH);

            int i = SqlServerDAL.ExecuteNonQueryy(sqlstr);
        }
        
        public void insert_table_into_db(DataTable dt, int startDepth, int endDepth, string jh)
        {  //把dt的一部分放入数据库里
            DataTable insert_dt = this.create_new_dt_by_depth(dt, startDepth, endDepth);
            DataColumn jh_column = new DataColumn("jh");
            insert_dt.Columns.Add(jh_column);
            foreach (DataRow row in insert_dt.Rows)
            {
                row["jh"] = jh;
            }

            SqlServerDAL.SqlBulkCopyColumnMapping("P_TABLE", insert_dt);



        }
        public DataTable create_new_dt_by_depth(DataTable dt, int startDepth, int endDepth)
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

            for (int i = startIndex; i <= endIndex; i++)
            {

                ret.Rows.Add(dt.Rows[i].ItemArray);
            }
            return ret;

        }

        public void clear_previous_pic()
        {
            vdEntities arr = this.cf.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Entities;
            arr.RemoveAll();

            vdDocument activeDocu = this.cf.vdScrollableControl1.BaseControl.ActiveDocument;
            activeDocu.Redraw(true);

            cf.vdScrollableControl1.BaseControl.ActiveDocument.ZoomExtents();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            xiugaijian("AfterMsev", pTableContainer.itemDataTable);
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            xiugaijia("AfterMsev", pTableContainer.itemDataTable);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            xiugaijian("AfterMseh", pTableContainer.itemDataTable);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            xiugaijia("AfterMseh", pTableContainer.itemDataTable);
        }


        public bool found_depth = false;
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if(this.found_depth == false)
            {
                MessageBox.Show("还没找深度");
                return;
            } if (cxgtxt.Text != "")
            {
                zuoyougai("AfterMsev", cxgtxt.Text.ToString(), pTableContainer.itemDataTable);
                MessageBox.Show("垂向功移动了" + cxgtxt.Text.ToString());
            } if (qxgtxt.Text != "")
            {
                zuoyougai("AfterMseh", qxgtxt.Text.ToString(), pTableContainer.itemDataTable);
                MessageBox.Show("切向功移动了" + qxgtxt.Text.ToString());
            } if (jgltxt.Text != "")
            {
                zuoyougai("AfterJinGeiLiang", jgltxt.Text.ToString(), pTableContainer.itemDataTable);
                MessageBox.Show("进给量移动了" + jgltxt.Text.ToString());
            } if (jxbntxt.Text != "")
            {
                zuoyougai("AfterMseb", jxbntxt.Text.ToString(), pTableContainer.itemDataTable);
                MessageBox.Show("机械比能移动了" + jxbntxt.Text.ToString());
            }
            /**
            try
            {
                this.find_depth_gap();
            }
            catch
            {
                //没有找到深度，直接画图
                this.clear_previous_pic();//清空之前画的图

                this.Fdraw_pic(false); 
                cf.vdScrollableControl1.BaseControl.ActiveDocument.ZoomExtents();
                return;
            }
            **/
            if (toolStripTextBox2.Text.ToString().Trim() != "")
            {
                msej(pTableContainer.itemDataTable);

            }

            delete_ptable(depthmin, depthmax);

            insert_table_into_db(pTableContainer.itemDataTable, depthmin, depthmax, FrameDesign.JH);
           
            //绘图
            // vdEntities arr = ((Childform)this.ActiveMdiChild).vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Entities;

            this.clear_previous_pic();//清空之前画的图

            this.Fdraw_pic(false); 
            cf.vdScrollableControl1.BaseControl.ActiveDocument.ZoomExtents();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            xiugaijian("AfterJinGeiLiang", pTableContainer.itemDataTable);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            xiugaijia("AfterJinGeiLiang", pTableContainer.itemDataTable);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            xiugaijian("AfterMseb", pTableContainer.itemDataTable);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            xiugaijia("AfterMseb", pTableContainer.itemDataTable);
        
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {

           


            ((Childform)this.ActiveMdiChild).vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.CmdLine(null);


        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            this.found_depth = true;
            this.find_depth_gap();
            this.showMSEJ();
        }
        private void showMSEJ()
        {//BaseLineForm.depthmax
            //pTableContainer.itemDataTable[]
            foreach(DataRow row in pTableContainer.itemDataTable.Rows)
            {
                if(row["depth"].ToString() == BaseLineForm.depthmax.ToString())
                {
                    this.toolStripTextBox2.Text = row["MSEJ"].ToString();
                }
            }
        }
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (cxgtxt.Text != "") { 
                zuoyougai("AfterMsev", cxgtxt.Text.ToString(), pTableContainer.itemDataTable);
                MessageBox.Show("垂向功移动了" + cxgtxt.Text.ToString());
            } if (qxgtxt.Text != "")
            {
                zuoyougai("AfterMseh", qxgtxt.Text.ToString(), pTableContainer.itemDataTable);
                MessageBox.Show("切向功移动了" + qxgtxt.Text.ToString());
            } if (jgltxt.Text != "")
            {
                zuoyougai("AfterJinGeiLiang", jgltxt.Text.ToString(), pTableContainer.itemDataTable);
                MessageBox.Show("进给量移动了" + jgltxt.Text.ToString());
            } if (jxbntxt.Text != "")
            {
                zuoyougai("AfterMseb", jxbntxt.Text.ToString(), pTableContainer.itemDataTable);
                MessageBox.Show("机械比能移动了" + jxbntxt.Text.ToString());
            }
         
        }
    }
}
