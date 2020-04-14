using DesignEnum;
using LJJSCAD.BlackBoard;
using LJJSCAD.BlackBoard.InitData.Factory;
using LJJSCAD.BlackBoard.InitData.Impl;
using LJJSCAD.CommonData;
using LJJSCAD.DataProcess.DrawControlData;
using LJJSCAD.Drawing.Manage;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.DrawingElement;
using LJJSCAD.DrawingOper;
using LJJSCAD.LJJSDrawing.Impl;
using LJJSCAD.LJJSDrawing.Impl.DrawItem;
using LJJSCAD.LJJSDrawing.Interface;
using LJJSCAD.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VectorDraw.Generics;
using VectorDraw.Geometry;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using LJJSCAD.WorkDataManage.Impl;
using LJJSCAD.DAL;
using System.Threading;

namespace LJJSCAD
{
    public partial class SuiZuanForm : Form
    {
        public MainForm mf;
        public bool zheng_zai_Sui_Zuan_Zhong = false;//正在随钻中
        //GetItemDataTableByName()
        void createForm()
        {
            //"new" order
            Childform form = new Childform();
            form.MdiParent = this;
            form.mf = this.mf;
            form.WindowState = FormWindowState.Maximized;

            

            form.Show();
            
        
        }
        void CommandExecute(string commandname, bool isDefaultImplemented, ref bool success)
        {
            if (string.Compare(commandname, "open", true) == 0)
            {
                success = true;
                createForm();
                Childform form = this.ActiveMdiChild as Childform;
                VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
                object ret = doc.GetOpenFileNameDlg(0, "", 0);
                if (ret == null)
                {
                    form.Close();
                    return;
                }

                string fname = (string)ret;
                bool successopen = doc.Open(fname);
                if (!successopen)
                {
                    System.Windows.Forms.MessageBox.Show("Error openning " + fname);
                    form.Close();
                    return;
                }
                doc.Redraw(false);

            }
            else if (string.Compare(commandname, "new", true) == 0)
            {
                createForm();
                success = true;
            }
            else if (string.Compare(commandname, "pan", true) == 0)
            {
                Childform frm = this.ActiveMdiChild as Childform;
                if (frm == null) return;
                frm.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.Pan();
            }

        }
        public SuiZuanForm()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            vdCommandLine1.LoadCommands(VectorDraw.Serialize.Activator.GetAssemplyPath(), "Commands.txt");
            commandLine.CommandExecute += new VectorDraw.Professional.vdCommandLine.CommandExecuteEventHandler(CommandExecute);
            
        }
        private double startDepth1 = -999;
        private double startDepth2 = -999;
        private string tmpjd = "";
        public VectorDraw.Professional.vdCommandLine.vdCommandLine commandLine { get { return vdCommandLine1; } }

        private void InitDrawData(string myLineRoadDesignName)
        {
            //1,选择数据提供方式
            DataControl.designDataProvidePattern = DataProvidePattern.SQLServerDataBase;
            //2,初始化各个结构,框架,绘图项,符号设计;
            DesignInitDirector designInitDirector = new DesignInitDirector(DesignInitBuilderFactory.CreateDesignInitBuilder(DataControl.designDataProvidePattern));
            designInitDirector.ExecDesignInit(myLineRoadDesignName);



        }
        private void ClearAllGrips(vdSelection GripSelection)
        {
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
            foreach (vdFigure fig in GripSelection)
            {
                fig.ShowGrips = false;
            }
            if (GripSelection.Count > 0)
            {
                GripSelection.RemoveAll();
                frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.RefreshGraphicsControl(frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.control);
            }
        }
        private vdSelection GetGripsCollection()
        {
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return null;
            string selsetname = "VDGRIPSET_" + frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Handle.ToStringValue() + (frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveViewPort != null ? frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveViewPort.Handle.ToStringValue() : "");
            vdSelection gripset = frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Document.Selections.FindName(selsetname);
            if (gripset == null) gripset = frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Document.Selections.Add(selsetname);
            return gripset;
        }
        void BaseControl_vdMouseDown(MouseEventArgs e, ref bool cancel)
        {

            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;

            if (e.Button != MouseButtons.Left) return;
            if (frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.OverAllActiveActions == null) return;
            if (frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.OverAllActiveActions.Count > 1) return;

            if (frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveAction is VectorDraw.Professional.CommandActions.ActionLine) return;
            vdSelection GripEntities = GetGripsCollection();
            gPoint p1 = frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.World2Pixelmatrix.Transform(frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.OverAllActiveAction.MouseLocation);
            gPoint p1viewCS = frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.CurrentMatrix.Transform(frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.OverAllActiveAction.MouseLocation);
            Point location = new Point((int)p1.x, (int)p1.y);

            #region Grip Move Code
            if (System.Windows.Forms.Control.ModifierKeys == Keys.None)
            {
                Box box = new Box();
                box.AddPoint(p1viewCS);
                box.AddWidth(frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.GlobalProperties.GripSize * frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.PixelSize / 2.0d);

                vdSelection selset = new vdSelection();
                vdArray<Int32Array> indexesArray = new vdArray<Int32Array>();
                gPoint pt = frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Document.World2UserMatrix.Transform(frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.OverAllActiveAction.MouseLocation);
                foreach (vdFigure fig in GripEntities)
                {
                    Int32Array indexes = frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.getGripIndexes(fig, box);
                    if (indexes.Count != 0)
                    {
                        selset.AddItem(fig, false, vdSelection.AddItemCheck.Nochecking);
                        indexesArray.AddItem(indexes);
                    }
                }
                if (selset.Count > 0)
                {
                    VectorDraw.Professional.ActionUtilities.CmdMoveGripPoints MoveGrips = new VectorDraw.Professional.ActionUtilities.CmdMoveGripPoints(pt, frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut, selset, indexesArray);
                    frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActionAdd(MoveGrips);
                    VectorDraw.Actions.StatusCode ret = MoveGrips.WaitToFinish();
                    cancel = true;
                    return;
                }
            }
            #endregion

            #region One by One implementation
            vdFigure Fig = null;
            Fig = frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.GetEntityFromPoint(location, frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.GlobalProperties.PickSize, false, vdDocument.LockLayerMethodEnum.EnableGetObjectGrip);
            frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Document.LockLayerMethodStack.Push(vdDocument.LockLayerMethodEnum.DisableAll | ((frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Document.LockLayerMethod & vdDocument.LockLayerMethodEnum.EnableGetObjectGrip) != 0 ? vdDocument.LockLayerMethodEnum.EnableAddToSelections : 0));
            bool bShift = ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) == Keys.Shift);
            if (Fig != null)
            {
                ClearAllGrips(GripEntities);
                GripEntities.AddItem(Fig, true, vdSelection.AddItemCheck.RemoveInVisibleEntities);
                if (cancel)
                    Fig.ShowGrips = false;
                else
                    Fig.ShowGrips = true;
                frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.RefreshGraphicsControl(frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.control);
              //  DrawGrips(GripEntities);
            }
            frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Document.LockLayerMethodStack.Pop();
            #endregion


        }
       /* private void DrawGrips(vdSelection GripSelection)
        {
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;

            if (GripSelection.Count == 0) return;
            bool isstarted = frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.Started;
            if (!isstarted) frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.StartDraw(true);
            bool islock = frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.IsLock;
            if (islock) frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.UnLock();
            VectorDraw.Render.GDIDraw.drawingMode mixmode = frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.SetMixMode(VectorDraw.Render.GDIDraw.drawingMode.R2_COPYPEN);
            frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.PushPenstyle(frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.GlobalProperties.GripColor, 0.0d, null);
            frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.PushClipPerigram(new Matrix(), null, VectorDraw.Geometry.GpcWrapper.ClippingOperation.Union);
            int i = 0;
            Process endPro;
            bool gripon = frm.vdScrollableControl1.BaseControl.ActiveDocument.EnableAutoGripOn;

            foreach (vdFigure fig in GripSelection)
            {
                fig.DrawGrips(frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender);
                if (!string.IsNullOrEmpty(fig.URL))
                    try
                    {
                        endPro = System.Diagnostics.Process.Start(fig.URL);
                    }
                    catch
                    {
                        MessageBox.Show("链接有误，文件不存在，请重新设置！");
                    }

                i++;
                if (!VectorDraw.WinMessages.MessageManager.IsMessageQueEmpty(IntPtr.Zero, VectorDraw.WinMessages.MessageManager.BreakMessageMethod.All)) break;
            }
            frm.vdScrollableControl1.BaseControl.ActiveDocument.EnableAutoGripOn = false;

            frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.PopClipPerigram();
            frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.PopPenstyle();
            frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.SetMixMode(mixmode);
            if (islock) frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.Lock();
            if (!isstarted) frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveActionRender.EndDraw();
            frm.vdScrollableControl1.BaseControl.ActiveDocument.EnableAutoGripOn = gripon;
        }*/

        public void check_SuiZuan_is_on_or_not()
        {
           
            //检测随钻是否正在运行
            while(true)
            {
                Thread.Sleep(1000);
                if (this.timer1.Enabled == true)
                {
                    this.label10.Text = "正在随钻";
                    
                }
                else
                {
                    this.label10.Text = "随钻暂停";
                }
            }
            
        }


        private void SuiZuanForm_Load(object sender, EventArgs e)
        {
            menuStrip1.AutoSize = false;
            menuStrip1.Size = new Size(100, 1);
            this.MainMenuStrip = menuStrip1;
            #region    对静态类进行初始化
            DrawPointContainer.initiate();
            DrawCommonData.Initiate();
            pTableContainer.initiate();
            #endregion



            this.IsMdiContainer = true;

            
            DataTable dt = new DBHelper.SqlServerDAL("server=127.0.0.1;database=DQLREPORTDB;integrated security=true;").GetTable("use DQLREPORTDB select * from Draw_JH ");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == dt.Rows.Count - 1)
                {
                    if (dt.Rows[i]["drawjh"].ToString().Length > 0)
                    {
                        this.tbJingHao.Text = dt.Rows[i]["drawjh"].ToString();
                    }

                }
            }

            DataTable mobanTable = new DBHelper.SqlServerDAL("server=127.0.0.1;database=DQLREPORTDB;integrated security=true;").GetTable("use JXBNCAD select LineRoadDesignName from LineRoadDesign");
            //this.cmbMoBan.Items
            foreach(DataRow row in mobanTable.Rows)
            {
                this.cmbMoBan.Items.Add(row[0].ToString());
            }

            commandLine.ExecuteCommand("new");

            


            /**
                        void createForm()
                    {
                        //"new" order
                        Childform form = new Childform();
                        form.MdiParent = this;
                        form.WindowState = FormWindowState.Maximized;
                        form.Show();
        
                    }
            **/
        }


        public void initdrawdata(string myLineRoadDesignName)
        {
            InitDrawData(myLineRoadDesignName);
            LineRoadWinDesign();



            

            Childform frm = this.ActiveMdiChild as Childform;


            if (frm == null) return;
            frm.vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
        }


        private void LineRoadWinDesign()
        {
            this.lb_LineRoadDesign.Items.Clear();
            int lCount = LineRoadDesign.LineRoadDesginLst.Count;
            for (int i = 0; i < lCount; i++)
            {
                //线道ID,对应LineRoadDesignDetail中的LineRoadDesignDetaiID
                string lid = LineRoadDesign.LineRoadDesginLst[i].LineRoadId;

                //线道包含的绘图项；
                string tmpItemNames = "";
                List<DrawItemName> drawItemlst = LineRoadDesign.LineRoadDesginLst[i].Drawingitems;
                if (LineRoadDesign.LineRoadDesginLst[i].LineRoadStyle.Equals(LineRoadStyle.JingShenLineRoad))
                    tmpItemNames = "井深";
                else
                {
                    for (int j = 0; j < drawItemlst.Count; j++)
                    {
                        tmpItemNames = tmpItemNames + drawItemlst[j].DrawItemShowName + ";";
                    }
                }

                string lineRoadShowContent = lid + ":" + tmpItemNames;

                if (this.lb_LineRoadDesign.Items.Contains(lineRoadShowContent) == false)
                {
                    this.lb_LineRoadDesign.Items.Add(lineRoadShowContent);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        public bool start_from_Zero = true;

        public void start_SuiZuan()  //开始随钻
        {

            DrawCommonData.Initiate();
            
            if(this.tbDrillRadius.Text != "")
            {
                try
                {
                    DrawCommonData.drill_radius = double.Parse(this.tbDrillRadius.Text);
                }
                catch
                {
                    DrawCommonData.drill_radius = null;
                }
            }


            if (this.tbJingHao.Text == "" || this.tbStart.Text == "" || this.tbEnd.Text == "" || this.tbStepLength.Text == "" ||this.tbInterval.Text=="")
            {
                MessageBox.Show("填写信息不全,无法进行随钻");
                return;
            }
            try
            {
                this.stepnum = double.Parse(this.tbStepLength.Text.ToString().Trim());
            }

            catch
            {
                MessageBox.Show("步长的格式不对，无法进行随钻");
                return;
            }

            try
            {
                this.timer1.Interval = Convert.ToInt32((double.Parse(this.tbInterval.Text.ToString().Trim()) * 1000));
            }

            catch
            {
                MessageBox.Show("随钻频率的格式不对，无法进行随钻");
                return;
            }
            //aaaa
            DrawCommonData.icount = 0;
            timer1.Enabled = true;
            timer2.Enabled = true;
            timer2.Interval = timer1.Interval+50;


            this.start_from_Zero = false;  //表示此时随钻已经开始

           

            Thread th = new Thread(this.check_SuiZuan_is_on_or_not);
            th.Start(); //此为检测随钻是否正在进行
        }

        public void continue_SUizuan()
        {
            if(timer1.Enabled == false)
            {
                timer1.Enabled = true;
            }
            
        }

        public bool force_Stop_Suizuan()
        {
            if (timer1.Enabled == true)
            {
                timer1.Enabled = false;

                timer2.Enabled = false;


                return true;

            }
            return false;
        }


        public bool Stop_Suizuan()
        {
            if(timer1.Enabled == true)
            {
                timer1.Enabled = false;

               


                return true;

            }
            return false;
        }
        //加入一个暂停方法，在暂停中调用该方法，通过将本来的timer1暂停，暂停后启用timer2刷新，如果井深超出数据库中的范围，就停止，一直刷新到有数据后重新开始timer1
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            

            SQLServerWorkDataProvider.invoke_method = "随钻";
            #region    对部分静态类进行初始化
            DrawPointContainer.initiate();
            
            pTableContainer.initiate();
            #endregion

            DrawPointContainer.szf = this;

            if(DrawPointContainer.list!=null)
            {
                for (int i = 0; i < DrawPointContainer.list.Count; i++)
                {
                    DrawPointContainer.list[i].Initiate();
                }
            }
            
                //suizuan//.Initiate();
                //绘图井，绘图框架等设计信息写入黑板区；

                SetFrameDesign();

            //根据lb_LineRoadDesign重新整理线道设计顺序；
            if (DrawCommonData.icount == 0)
            {
                SetLineRoadDesign();
                
            }
           
            //线道链表，线道框架即各个控制点数据等设计信息写入黑板区；
            FrameControlData.LineRoadControlLst = LineRoadOper.getLineRoadControlDataLst(LineRoadDesign.LineRoadDesginLst);

            WorkDataManage.WorkDataManage.BuildWorkDataHt();//获得绘图工作数据；将数据填充WorkDataManage类的WorkDataHt属性中；
            Entities.ClearAll();
            //绘图
            this.DrawLJJSPicture();

            if(this.maxminform != null)
            {
                pTableMaxAndMinValueManagementForm form = this.maxminform;

                form.tbMinJGL.Text = DrawCommonData.curr_JGLMin.ToString();
                form.tbMinMseb.Text = DrawCommonData.curr_MsebMin.ToString();
                form.tbMinMseh.Text = DrawCommonData.curr_MsehMin.ToString();
                form.tbMinMsev.Text = DrawCommonData.curr_MsevMin.ToString();

                form.tbMaxJGL.Text = DrawCommonData.curr_JGLMax.ToString();
                form.tbMaxMseb.Text = DrawCommonData.curr_MsebMax.ToString();
                form.tbMaxMseh.Text = DrawCommonData.curr_MsehMax.ToString();
                form.tbMaxMsev.Text = DrawCommonData.curr_MsevMax.ToString();
            }
            

        }

        public bool zoomed = false;
        public static LJJSPoint startpoint;
        public static void set_startpoint(LJJSPoint point)
        {
            if(SuiZuanForm.startpoint == null)
            {
                SuiZuanForm.startpoint = point;
            }
        }

        

        /// <summary>
        /// 绘制录井解释图
        /// </summary>
        private void DrawLJJSPicture()
        {
            Childform frm = this.ActiveMdiChild as Childform;

            if (frm == null) return;

            DrawCommonData.activeDocument = frm.vdScrollableControl1.BaseControl.ActiveDocument;
            if (!FrameDesign.Validate(this))//验证； 若!=true 则return
                return;

            
            

            LJJSBuilder ljjsBuilder = new LJJSBuilderImpl();
            LJJSDirector ljjsDirector = new LJJSDirector(ljjsBuilder);
            ljjsDirector.BuildLJJS();

            vdDocument activeDocu = DrawCommonData.activeDocument;
            
            /**
            if (DrawCommonData.icount == 1)
            {
                
                if (SuiZuanForm.startpoint != null && this.zoomed == false)
                {
                    gPoint g1 = new gPoint(SuiZuanForm.startpoint.XValue - 100, SuiZuanForm.startpoint.YValue + 40);
                    gPoint g2 = new gPoint(g1.x + 900.0, g1.y - 450.0);
                    frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ZoomWindow(g1, g2);
                    this.zoomed = true;
                }

            }**/
            Childform cf = this.MdiChildren[0] as Childform;
            //自动缩放到合适的大小
            if (DrawCommonData.icount == 1)
            {
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomE_Ex(cf.vdScrollableControl1.BaseControl.ActiveDocument);
            }
            
            activeDocu.Redraw(true);
            
            //commandLine.ExecuteCommand("ze");
            //frm.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.
            
        }
        


        private void SetLineRoadDesign()
        {
            List<LineRoadDesignClass> tmp = new List<LineRoadDesignClass>();
            for (int i = 0; i < this.mf.lb_LineRoadDesign.Items.Count; i++)
            {
                // List<lineroad>
                string tmpLrId = this.mf.GetLineRoadIdByDesignStr(this.mf.lb_LineRoadDesign.Items[i].ToString());

                LineRoadDesignClass tmpstruc = LineRoadDesign.GetLineRoadDesignStrucById(tmpLrId);
                tmp.Add(tmpstruc);

            }
            LineRoadDesign.LineRoadDesginLst = tmp;

        }

        private double stepnum;
        private void SetFrameDesign()
        {



            if (DrawCommonData.icount == 0)
            {


                
                /**try
                {
                    this.startDepth1 = int.Parse(this.tb__JDStart.Text.ToString().Trim());
                    //this.startDepth1 = int.Parse(this.tb__JDStart.Text.ToString().Trim());
                    this.startDepth2 = int.Parse(this.tb__JDEnd.Text.ToString().Trim());
                }
                catch (Exception e)
                {
                    MessageBox.Show("输入数据有误??????");
                    return;
                }**/
                if (!string.IsNullOrEmpty(this.tbJingHao.Text))
                {
                    FrameDesign.JH = this.tbJingHao.Text;
                }

                //比例尺值计算；           
                FrameDesign.CorValue = FrameDesign.XCoordinate + ":" + FrameDesign.YCoordinate;//比例尺值赋值
                if (Math.Abs(FrameDesign.YCoordinate) > 0.0001)
                    FrameDesign.ValueCoordinate = FrameDesign.XCoordinate / FrameDesign.YCoordinate;
                //井段设计

                FrameDesign.JdStrLst.Clear();

                

                //添加井段信息
                string tmpJdStr = this.tbStart.Text + DrawCommonData.jdSplitter.ToString() + this.tbEnd.Text;
                FrameDesign.JdStrLst.Add(tmpJdStr);

                this.tmpjd = tmpJdStr;
                try
                {
                    //   string currData = this.lb_JD.SelectedItem.ToString().Trim(); //现在选中的记录
                    string[] strStartDepthArr = Regex.Split(this.tmpjd, DrawCommonData.jdSplitter.ToString());

                    this.startDepth1 = double.Parse(strStartDepthArr[0].Trim());
                    this.startDepth2 = double.Parse(strStartDepthArr[1].Trim());
                }
                catch (Exception e)
                {
                    MessageBox.Show("data Error");
                    throw e;
                }


                FrameDesign.Validate(this);//数据验证；
            }
            else
            {
                FrameDesign.JdStrLst.Clear();


                if (this.startDepth1 < 0 || this.startDepth2 < 0)
                {
                    MessageBox.Show("error");
                    throw new Exception("Error startDepth");
                    //  return;
                }

                this.startDepth1 += this.stepnum;
                this.startDepth2 += this.stepnum;
                string jdstr = startDepth1.ToString() + DrawCommonData.jdSplitter + startDepth2.ToString();
                FrameDesign.JdStrLst.Add(jdstr);


            }

            DrawCommonData.icount++;


        }

        private void tbInterval_TextChanged(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void vdCommandLine1_Load(object sender, EventArgs e)
        {

        }

        private void lb_LineRoadDesign_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbMoBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string myLineRoadDesignName = this.cmbMoBan.SelectedItem.ToString().Trim();
                this.initdrawdata(myLineRoadDesignName);
            }
            catch
            {
                this.lb_LineRoadDesign.Items.Clear();
                return;
            }
        }

        private void tbStepLength_TextChanged(object sender, EventArgs e)
        {

        }
        public string GetLineRoadIdByDesignStr(string designStr)
        {
            if (string.IsNullOrEmpty(designStr))
                return null;

            string[] tmpLrStrArr = designStr.Split(':');
            if (tmpLrStrArr.Length == 2)
            {
                return tmpLrStrArr[0];
            }
            else
                return null;

        }
        private void btnRemoveLine_Click(object sender, EventArgs e)
        {
            if (null != lb_LineRoadDesign.SelectedItem)
            {

                if (DialogResult.Yes == MessageBox.Show("确定删除该线道么？", "线道删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {


                    string tmpid = this.GetLineRoadIdByDesignStr(lb_LineRoadDesign.SelectedItem.ToString());
                    //1,从线道管理结构中删除该线道内容；
                    LineRoadDesign.DeleteLRDesignStrucByID(tmpid);
                    //2,从线道设计框中删除该线道内容；
                    this.lb_LineRoadDesign.Items.RemoveAt(lb_LineRoadDesign.SelectedIndex);


                }
            }
            else
            {
                MessageBox.Show("请选择删除线道");
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            bool res = ItemDAL.check_if_has_data_after(DrawPointContainer.fromtablename);
            if(res == true)
            {
               //有数据，接着随钻
                this.continue_SUizuan();
               // this.timer2.Stop();    //timer2停止，即停止检测之后是否还有数据
            }
            else
            {
                //没数据 停止随钻
                this.Stop_Suizuan();
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        public pTableMaxAndMinValueManagementForm maxminform;

        private void btnSetWhole_Click(object sender, EventArgs e)
        {
            pTableMaxAndMinValueManagementForm form = new pTableMaxAndMinValueManagementForm();
            this.maxminform = form;
            form.mf = this;
            form.Show();
        }

        private void menuStrip1_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
             

            if(e.Item.Text.Length == 0)
            {
                e.Item.Visible = false;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnSuiZuan_Click(object sender, EventArgs e)
        {
            if (this.cmbMoBan.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择模板");
                return;
            }
            if (this.start_from_Zero == true)
            {
                this.start_SuiZuan();
                this.zheng_zai_Sui_Zuan_Zhong = true;
            }
            else
            {
                this.continue_SUizuan();
                this.zheng_zai_Sui_Zuan_Zhong = false;
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            bool success = this.force_Stop_Suizuan();
            if (success == false)
            {
                MessageBox.Show("无法停止随钻");
                return;
            }
            this.zheng_zai_Sui_Zuan_Zhong = false;
        }
    }
}
