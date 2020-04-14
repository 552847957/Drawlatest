using LJJSCAD.CommonData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VectorDraw.Actions;
using VectorDraw.Geometry;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdPrimaries;


namespace LJJSCAD
{
    public partial class Childform : Form  //BaseControl_MouseMoveAfter
    {
        
        public Childform()
        {
            InitializeComponent();

            


            this.vdScrollableControl1.BaseControl.vdKeyPress += new VectorDraw.Professional.Control.KeyPressEventHandler(BaseControl_vdKeyPress);
            this.vdScrollableControl1.BaseControl.vdKeyDown += new VectorDraw.Professional.Control.KeyDownEventHandler(BaseControl_vdKeyDown);
            this.vdScrollableControl1.BaseControl.GripSelectionModified += new VectorDraw.Professional.Control.GripSelectionModifiedEventHandler(BaseControl_GripSelectionModified);
            this.vdScrollableControl1.BaseControl.ActionLayoutActivated += new VectorDraw.Professional.Control.ActionLayoutActivatedEventHandler(BaseControl_ActionLayoutActivated);
            this.vdScrollableControl1.BaseControl.AfterNewDocument += new VectorDraw.Professional.Control.AfterNewDocumentEventHandler(BaseControl_AfterNewDocument);
            this.vdScrollableControl1.BaseControl.AfterOpenDocument += new VectorDraw.Professional.Control.AfterOpenDocumentEventHandler(BaseControl_AfterOpenDocument);
            this.vdScrollableControl1.BaseControl.Progress += new VectorDraw.Professional.Control.ProgressEventHandler(BaseControl_Progress);
            //this.vdScrollableControl1.BaseControl.MouseMoveAfter += new VectorDraw.Professional.Control.MouseMoveAfterEventHandler(BaseControl_MouseMoveAfter);



            
        }





        //test模板
        protected override void OnClosing(CancelEventArgs e)
        {
            if (vdScrollableControl1.BaseControl.ActiveDocument.IsModified)
            {
                DialogResult res = MessageBox.Show("Save changes in Drawing \n" + this.Text + " ?", this.MdiParent.Text, MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    string version = "";
                    string fname = vdScrollableControl1.BaseControl.ActiveDocument.GetSaveFileNameDlg(this.Text, out version);
                    if (fname != null)
                    {
                        bool success = vdScrollableControl1.BaseControl.ActiveDocument.SaveAs(fname, null, version);
                        if (success == false)
                        {
                            MessageBox.Show("Error saving \n" + fname, this.MdiParent.Text);
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
           
            base.OnClosing(e);
        }
        protected override void OnClosed(EventArgs e)
        {
            this.vdScrollableControl1.BaseControl.vdKeyPress -= new VectorDraw.Professional.Control.KeyPressEventHandler(BaseControl_vdKeyPress);
            this.vdScrollableControl1.BaseControl.vdKeyDown -= new VectorDraw.Professional.Control.KeyDownEventHandler(BaseControl_vdKeyDown);
            this.vdScrollableControl1.BaseControl.GripSelectionModified -= new VectorDraw.Professional.Control.GripSelectionModifiedEventHandler(BaseControl_GripSelectionModified);
            this.vdScrollableControl1.BaseControl.ActionLayoutActivated -= new VectorDraw.Professional.Control.ActionLayoutActivatedEventHandler(BaseControl_ActionLayoutActivated);
            this.vdScrollableControl1.BaseControl.AfterNewDocument -= new VectorDraw.Professional.Control.AfterNewDocumentEventHandler(BaseControl_AfterNewDocument);
            this.vdScrollableControl1.BaseControl.AfterOpenDocument -= new VectorDraw.Professional.Control.AfterOpenDocumentEventHandler(BaseControl_AfterOpenDocument);
            this.vdScrollableControl1.BaseControl.Progress -= new VectorDraw.Professional.Control.ProgressEventHandler(BaseControl_Progress);
            
            base.OnClosed(e);
            MainForm parent = this.MdiParent as MainForm;
            if (parent != null) parent.commandLine.SelectDocument(null);
            FillPropertyGrid(null);
            vdScrollableControl1.BaseControl.ActiveDocument.Dispose();
            vdScrollableControl1.BaseControl.Dispose();
            if (parent != null) parent.UpdateMenu(parent.MdiChildren.Length == 1);
        }

        public List<VectorDraw.Geometry.gPoint> gPointList = new List<gPoint>();
        private void BaseControl_MouseMoveAfter(MouseEventArgs e)
        {
            MainForm parent = this.MdiParent as MainForm;
            VectorDraw.Geometry.gPoint ccspt = vdScrollableControl1.BaseControl.ActiveDocument.CCS_CursorPos();
            

            double x = ccspt.x;
            double y = ccspt.y;
            double z = ccspt.z;
            if (Math.Abs(x) > 1 && Math.Abs(y) > 1 && z == 0)
            {
                this.gPointList.Add(ccspt);
                
            }
            string str = vdScrollableControl1.BaseControl.ActiveDocument.lunits.FormatLength(x) + " , " + vdScrollableControl1.BaseControl.ActiveDocument.lunits.FormatLength(y) + " , " + vdScrollableControl1.BaseControl.ActiveDocument.lunits.FormatLength(z);
            if (parent!= null && parent.mDisplayPolarCoord)
            {
                //if active action is user waiting reference point and mDisplayPolarCoord == true
                //then polar coord string
                if (vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.OverAllActiveActions.Count > 1)
                {
                    BaseAction action = vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.OverAllActiveAction;
                    
                    if (action.WaitingPoint && action.ReferencePoint != null)
                    {
                        if ((action.ValueTypeProp & BaseAction.valueType.REFPOINT) == BaseAction.valueType.REFPOINT)
                        {
                            gPoint refpt = vdScrollableControl1.BaseControl.ActiveDocument.World2UserMatrix.Transform(action.ReferencePoint);
                            double angle = Globals.GetAngle(refpt, ccspt);
                            double dist = ccspt.Distance3D(refpt);
                            x = dist;
                            y = Globals.RadiansToDegrees(angle);
                            z = 0.0d;
                            str = vdScrollableControl1.BaseControl.ActiveDocument.lunits.FormatLength(dist) + "<" + vdScrollableControl1.BaseControl.ActiveDocument.aunits.FormatAngle(angle);
                        }
                    }
                }
            }
            if (parent != null) parent.CoordDisplay.Text = str;
        }
        private void BaseControl_Progress(object sender, long percent, string jobDescription)
        {
            MainForm parent = this.MdiParent as MainForm;
            if (parent == null) return;
            if (percent == 0 || percent == 100) parent.status.Text = "";
            else parent.status.Text = jobDescription;
            parent.ProgressBar.Value = (int)percent;
        }
        private void BaseControl_AfterOpenDocument(object sender)
        {
            MainForm parent = this.MdiParent as MainForm;
            if (parent == null) return;
            VectorDraw.Professional.vdCommandLine.vdCommandLine cl = parent.commandLine;
            FillPropertyGrid(vdScrollableControl1.BaseControl.ActiveDocument);
            cl.SelectDocument(vdScrollableControl1.BaseControl.ActiveDocument);
            vdScrollableControl1.BaseControl.Focus();
            if(vdScrollableControl1.BaseControl.ActiveDocument.FileName != "")
                this.Text = vdScrollableControl1.BaseControl.ActiveDocument.FileName;
        }
        private bool IsNewFileNameExist(string fname)
        {
            foreach (Childform frm in this.MdiParent.MdiChildren)
            {
                if (string.Compare(frm.Text, fname, true) == 0) return true;
            }
            return false;
        }
        private void BaseControl_AfterNewDocument(object sender)
        {
            MainForm parent = this.MdiParent as MainForm;
            if (parent == null) return;
            VectorDraw.Professional.vdCommandLine.vdCommandLine cl = parent.commandLine;
            FillPropertyGrid(vdScrollableControl1.BaseControl.ActiveDocument);
            cl.SelectDocument(vdScrollableControl1.BaseControl.ActiveDocument);
            
            vdScrollableControl1.BaseControl.Focus();
            int i = 1;
            string fname = i.ToString() +".vdml";
            while (IsNewFileNameExist(fname))
            {
                i++;
                fname = i.ToString() + ".vdml";
            }
            vdScrollableControl1.BaseControl.ActiveDocument.FileName = fname;
            this.Text = fname;
            
        }
        private void BaseControl_ActionLayoutActivated(object sender, VectorDraw.Professional.vdPrimaries.vdLayout deactivated, VectorDraw.Professional.vdPrimaries.vdLayout activated)
        {
            VectorDraw.Professional.vdCollections.vdSelection gripset = vdScrollableControl1.BaseControl.ActiveDocument.Selections.FindName("VDGRIPSET_" + activated.Handle.ToStringValue());
            if (gripset != null)
            {
                foreach (VectorDraw.Professional.vdPrimaries.vdFigure var in gripset)
                {
                    var.ShowGrips = false;
                }
            }
            FillPropertyGrid(vdScrollableControl1.BaseControl.ActiveDocument);
            
            vdScrollableControl1.BaseControl.ReFresh();

        }
        private void BaseControl_GripSelectionModified(object sender, VectorDraw.Professional.vdPrimaries.vdLayout layout, VectorDraw.Professional.vdCollections.vdSelection gripSelection)
        {
            if (gripSelection.Count == 0) FillPropertyGrid(vdScrollableControl1.BaseControl.ActiveDocument);
            else FillPropertyGrid(gripSelection);
        }
        private void FillPropertyGrid(object obj)
        {
            MainForm parent = this.MdiParent as MainForm;
            if (parent == null) return;
            parent.vdgrid.SelectedObject = obj;
        }

        void BaseControl_vdKeyPress(KeyPressEventArgs e, ref bool cancel)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        private void BaseControl_vdKeyDown(KeyEventArgs e, ref bool cancel)
        {
            BaseAction action = vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.OverAllActiveAction;
            if (!action.SendKeyEvents) return;

            MainForm parent = this.MdiParent as MainForm;
            if (parent == null) return;
            VectorDraw.Professional.vdCommandLine.vdCommandLine cl = parent.commandLine;
            if (cl.Visible == false) return;
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) return;
            Message msg = new Message();
            msg.HWnd = cl.Handle;
            msg.Msg = (int)VectorDraw.WinMessages.MessageManager.Messages.WM_KEYDOWN;
            msg.WParam = (IntPtr)e.KeyCode;
            cl.vdProcessKeyMessage(ref msg);
        }
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            MainForm parent = this.MdiParent as MainForm;
            if (parent == null) return;
            parent.commandLine.SelectDocument(this.vdScrollableControl1.BaseControl.ActiveDocument);
            VectorDraw.Professional.vdCollections.vdSelection gripset = vdScrollableControl1.BaseControl.ActiveDocument.Selections.FindName("VDGRIPSET_" + vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Handle.ToStringValue());
            if (gripset != null && gripset.Count > 0) FillPropertyGrid(gripset);
            else FillPropertyGrid(this.vdScrollableControl1.BaseControl.ActiveDocument);
        }
        protected override void OnDeactivate(EventArgs e)
        {
            MainForm parent = this.MdiParent as MainForm;
            if (parent == null) return;
            parent.commandLine.SelectDocument(null);
            FillPropertyGrid(null);
            base.OnDeactivate(e);
            
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (vdScrollableControl1.BaseControl.Focused) return false;//new change
            MainForm parent = this.MdiParent as MainForm;
            if (parent == null) return false;
            VectorDraw.Professional.vdCommandLine.vdCommandLine cl = parent.commandLine;
            if (cl.Visible == false) return false;

            if (keyData == Keys.Up || keyData == Keys.Down) return false;
            Message nmsg = new Message();
            nmsg.HWnd = cl.Handle;
            nmsg.Msg = msg.Msg;
            nmsg.WParam = msg.WParam;
            nmsg.LParam = msg.LParam;
            cl.vdProcessKeyMessage(ref nmsg);
            return false;
        }

        private void Childform_Load(object sender, EventArgs e)
        {
            
               // vectorDrawBaseControl1.ActiveDocument.ShowUCSAxis = false ;
            MainForm parent = this.MdiParent as MainForm;
            
            if (parent == null) return;
            parent.UpdateMenu(false);
            this.vdScrollableControl1.BaseControl.ActiveDocument.ShowUCSAxis = false;//不显示坐标xyz图像

            this.vdScrollableControl1.BaseControl.SetCustomMousePointer(System.Windows.Forms.Cursors.Arrow);//设置鼠标指针的形状
            //啊啊啊啊啊啊啊啊啊啊啊
            this.vdScrollableControl1.BaseControl.ActiveDocument.ShowUCSAxis = false;//不显示坐标xyz图像
            this.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.BkColorEx = Color.White;
            this.vdScrollableControl1.RulerObject.BackColor = Color.Ivory;
            this.vdScrollableControl1.RulerObject.CursorColor = Color.DeepSkyBlue;
            this.vdScrollableControl1.RulerObject.Width = 20;

            this.vdScrollableControl1.RulerObject.TextSize = vdScrollableControl.vdScrollableControl.vdRuler.SizePercentage.MediumLarge;
            this.vdScrollableControl1.RulerObject.TextFontFile = "Times New Roman";

            this.vdScrollableControl1.RulerObject.Visible = false;


            string filename = VectorDraw.Professional.Utilities.vdGlobals.GetDirectoryName(Application.ExecutablePath) + "\\LJJSModel.vdml";
            this.vdScrollableControl1.BaseControl.ActiveDocument.Open(filename);
            this.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.BkColorEx = Color.White;
            this.vdScrollableControl1.RulerObject.BackColor = Color.Ivory;
            this.vdScrollableControl1.BaseControl.ActiveDocument.Model.UCS(new gPoint(0, 0), new gPoint(1, 0), new gPoint(0, -1));//控制坐标系
            this.vdScrollableControl1.LayoutTab.Visible = false;


            if (this.MdiParent is SuiZuanForm)
            {
                this.contextMenuStrip1.Items.Clear();
                this.contextMenuStrip1.Items.Add("随钻");
                this.contextMenuStrip1.Items.Add("暂停随钻");
                this.contextMenuStrip1.Items.Add("设定绘图比例尺");
                //this.contextMenuStrip1.Items.Add("停止随钻");

            }

            if(this.MdiParent is MainForm)
            {
                this.contextMenuStrip1.Items.Add("画线");
                this.contextMenuStrip1.Items.Add("获得两条线之间的深度差");
                this.contextMenuStrip1.Items.Add("设定绘图比例尺");
                this.contextMenuStrip1.Items.Add("选择模板");
            }
            
        }

        private void vdScrollableControl1_Load(object sender, EventArgs e)
        {
            
            
            
        }

        private void Childform_SizeChanged(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }


        public MainForm mf;

        //右键菜单点击事件
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
            string clicked_menu_str = e.ClickedItem.ToString();
            if(clicked_menu_str.Equals("随钻"))
            {
                SuiZuanForm szf = this.MdiParent as SuiZuanForm;
                if(szf!=null)
                {
                    if(szf.cmbMoBan.SelectedIndex < 0)
                    {
                        MessageBox.Show("请先选择模板");
                        return;
                    }
                    if(szf.start_from_Zero == true)
                    {
                        szf.start_SuiZuan();
                        szf.zheng_zai_Sui_Zuan_Zhong = true;
                    }
                    else
                    {
                        szf.continue_SUizuan();
                        szf.zheng_zai_Sui_Zuan_Zhong = false;
                    }
                }
            }

            if (clicked_menu_str.Equals("暂停随钻"))
            {
                SuiZuanForm szf = this.MdiParent as SuiZuanForm;
                if (szf != null)
                {
                    bool success = szf.force_Stop_Suizuan();
                    if(success == false)
                    {
                        MessageBox.Show("无法停止随钻");
                        return;
                    }
                    szf.zheng_zai_Sui_Zuan_Zhong = false;
                }
            }
            /**
            if (clicked_menu_str.Equals("停止随钻"))
            {
                SuiZuanForm szf = new SuiZuanForm();
                szf.mf = this.mf;

                SuiZuanForm father = this.MdiParent as SuiZuanForm;
                father.Hide();
                this.Hide();
                szf.Show();

                //
                SuiZuanForm szf = this.MdiParent as SuiZuanForm; 
                if (szf != null)
                {
                    

                    this.Close();
                    DrawCommonData.icount = 0;
                    szf.timer1.Enabled = false;
                    szf.cmbMoBan.SelectedIndex = -1;
                    SuiZuanForm.zoomed = false;
                    Childform cf = new Childform();
                   
                    cf.MdiParent = szf;
                    cf.WindowState = FormWindowState.Maximized;
                    
                    szf.zheng_zai_Sui_Zuan_Zhong = false;   //将计时器归零，返回到随钻初始状态
                    cf.Show();  //重新打开一个childform
                    //SuiZuanForm.startpoint != null && SuiZuanForm.zoomed == false
                    //
                }
            }**/
            if(clicked_menu_str.IndexOf("比例尺")>= 0 )
            {
                BiLiChiChangeForm bcf = new BiLiChiChangeForm();
                bcf.Show();
            }

            if(clicked_menu_str.IndexOf("模板")>=0)
            {
                SheDingMoBanForm sbmbf = new SheDingMoBanForm();
                sbmbf.cf = this;
               
                sbmbf.Show();
            }

            if(clicked_menu_str.IndexOf("深度")>=0)
            {   //vdFramedControl.BaseControl.ActiveDocument.ActiveLayOut.Entities.AddItem(oneline);
                if(this.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Entities.Count ==0)
                {
                    MessageBox.Show("错误，还没有画图");
                    return;
                }
                //this.find_depth();
                Thread thread = new Thread(this.find_depth);
                thread.Start();
            }
            if(clicked_menu_str.IndexOf("画线")>=0)
            {
                if (this.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Entities.Count == 0)
                {
                    MessageBox.Show("错误，还没有画图");
                    return;
                }
                this.draw_line();
                //this.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.CmdLine(null);
                
            }
        }
        public void draw_line()
        {
            this.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.CmdLine(null);
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
        public void find_depth()
        {
            vdEntities arr = this.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Entities;
            List<vdLine> list = new List<vdLine>();
            //arr[arr.Count - 2], arr[arr.Count - 1]
            vdLine line1 = (vdLine)arr[arr.Count - 2];
            vdLine line2 = (vdLine)arr[arr.Count - 1];
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

            MessageBox.Show("上界:" + upper.ToString());
            MessageBox.Show("下界:" + lower.ToString());
        }

        private void vdScrollableControl1_DragEnter(object sender, DragEventArgs e)
        {
            this.vdScrollableControl1.BaseControl.SetCustomMousePointer(System.Windows.Forms.Cursors.Arrow);
        }

        private void vdScrollableControl1_DragEnter_1(object sender, DragEventArgs e)
        {

        }

        private void vdScrollableControl1_MouseEnter(object sender, EventArgs e)
        {
            this.vdScrollableControl1.BaseControl.SetCustomMousePointer(System.Windows.Forms.Cursors.Arrow);
        }

        private void vdScrollableControl1_Load_1(object sender, EventArgs e)
        {

            this.vdScrollableControl1.BaseControl.SetCustomMousePointer(System.Windows.Forms.Cursors.Arrow);//设置鼠标指针的形状
            //啊啊啊啊啊啊啊啊啊啊啊
            this.vdScrollableControl1.BaseControl.ActiveDocument.ShowUCSAxis = false;//不显示坐标xyz图像
            this.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.BkColorEx = Color.White;
            this.vdScrollableControl1.RulerObject.BackColor = Color.Ivory;
            this.vdScrollableControl1.RulerObject.CursorColor = Color.DeepSkyBlue;
            this.vdScrollableControl1.RulerObject.Width = 20;

            this.vdScrollableControl1.RulerObject.TextSize = vdScrollableControl.vdScrollableControl.vdRuler.SizePercentage.MediumLarge;
            this.vdScrollableControl1.RulerObject.TextFontFile = "Times New Roman";

            this.vdScrollableControl1.RulerObject.Visible = false;


            string filename = VectorDraw.Professional.Utilities.vdGlobals.GetDirectoryName(Application.ExecutablePath) + "\\LJJSModel.vdml";
            this.vdScrollableControl1.BaseControl.ActiveDocument.Open(filename);
            this.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.BkColorEx = Color.White;
            this.vdScrollableControl1.RulerObject.BackColor = Color.Ivory;
            this.vdScrollableControl1.BaseControl.ActiveDocument.Model.UCS(new gPoint(0, 0), new gPoint(1, 0), new gPoint(0, -1));//控制坐标系
            this.vdScrollableControl1.LayoutTab.Visible = false;


            if (this.MdiParent is SuiZuanForm)
            {
                this.contextMenuStrip1.Items.Clear();
                this.contextMenuStrip1.Items.Add("随钻");
                this.contextMenuStrip1.Items.Add("暂停随钻");
                this.contextMenuStrip1.Items.Add("设定绘图比例尺");
                //this.contextMenuStrip1.Items.Add("停止随钻");

            }

            if (this.MdiParent is BaseLineForm)
            {
                this.contextMenuStrip1.Items.Add("画线");
                this.contextMenuStrip1.Items.Add("获得两条线之间的深度差");
            }

        }

        
        
    }
}