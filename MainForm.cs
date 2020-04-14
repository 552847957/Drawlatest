using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VectorDraw.Geometry;
using LJJSCAD.UI;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.Util;
using LJJSCAD.DrawingOper;
using LJJSCAD.LJJSDrawing.Interface;
using LJJSCAD.DataProcess.DrawControlData;
using LJJSCAD.CommonData;
using LJJSCAD.LJJSDrawing.Impl;
using LJJSCAD.BlackBoard;
using LJJSCAD.BlackBoard.InitData.Impl;
using LJJSCAD.BlackBoard.InitData.Factory;
using LJJSCAD.Drawing.Manage;
using LJJSCAD.Util.UI;
using LJJSCAD.BlackBoard.Legend;
using System.IO;
using LJJSCAD.LJJSDrawing.Impl.DrawItem;
using LJJSCAD.HeaderTableWorkData;
using LJJSCAD.Model;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdObjects;
using LJJSCAD.BlackBoard.PropertiesManage;
using VectorDraw.Actions;
using LJJSCAD.URLManage;
using VectorDraw.Generics;
using System.Diagnostics;
using DesignEnum;
using LJJSCAD.DrawingOper.LineRoadSurfaceManage;
using LJJSCAD.Drawing.Figure;
using LJJSCAD.DrawingElement;
using System.Text.RegularExpressions;
using System.Threading;
using System.Data;
using LJJSCAD.WorkDataManage.Impl;
using LJJSCAD.LJJSDrawing.Impl.DrawItem.SymbolItem;
using VectorDraw.Professional.vdFigures;
using System.Threading.Tasks;
namespace LJJSCAD
{
    public partial class MainForm : Form
    {   

       


        internal bool mDisplayPolarCoord = false;
        public static bool cansee = false;//�ֲ���������ѡ���ߵ���ƻ��߻�ͼ�����
        Label l = new Label();
        ToolTip tt = new ToolTip();
       public static string MSEJjd = null;
        public MainForm()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            vdCommandLine1.LoadCommands(VectorDraw.Serialize.Activator.GetAssemplyPath(), "Commands.txt");
            commandLine.CommandExecute += new VectorDraw.Professional.vdCommandLine.CommandExecuteEventHandler(CommandExecute);
            

        }
        internal void UpdateMenu(bool noChilds)
        {
            //disable unusable menu commands
        }
        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void She_Ding_Mo_Ban()//�趨ģ��
        {
            DataTable mobanTable = new DBHelper.SqlServerDAL("server=127.0.0.1;database=DQLREPORTDB;integrated security=true;").GetTable("use JXBNCAD select LineRoadDesignName from LineRoadDesign");
            //this.cmbMoBan.Items
            foreach (DataRow row in mobanTable.Rows)
            {
                this.cmbMoban.Items.Add(row[0].ToString());
            }
        }




        private void MainForm_Load(object sender, EventArgs e)
        {
            
            DataTable dt = new DBHelper.SqlServerDAL("server=127.0.0.1;database=DQLREPORTDB;integrated security=true;").GetTable("use DQLREPORTDB select * from Draw_JH ");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == dt.Rows.Count - 1)
                {
                    if (dt.Rows[i]["drawjh"].ToString().Length > 0)
                    {
                        this.tb_JH.Text = dt.Rows[i]["drawjh"].ToString();
                    }
                    
                }
            }   //i wrote
             
    


            commandLine.ExecuteCommand("new");
            commandLine.ExecuteCommand("ze");
            CreateMySplitControls();

            She_Ding_Mo_Ban();
            //mydefault
            
         
            //��������������������
            /**InitDrawData("��������");
            
             * 
             * 
            this.p_DrawItemDesign.Dock = DockStyle.Fill;
            this.p_LineRoadDesign.Dock = DockStyle.Fill;
            this.p_DrawItemDesign.Visible = cansee;
            this.p_LineRoadDesign.Visible = !cansee;
            
            Childform frm = this.ActiveMdiChild as Childform;
            
           
            if (frm == null) return;
           frm.vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);

            **/
            
        }

        

        public string myLineRoadDesignName;

        public void initdrawdata(string myLineRoadDesignName)
        {
            InitDrawData(myLineRoadDesignName);
            LineRoadWinDesign();
            


                this.p_DrawItemDesign.Dock = DockStyle.Fill;
            this.p_LineRoadDesign.Dock = DockStyle.Fill;
            this.p_DrawItemDesign.Visible = cansee;
            this.p_LineRoadDesign.Visible = !cansee;

            Childform frm = this.ActiveMdiChild as Childform;


            if (frm == null) return;
            frm.vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
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
      /*  private void DrawGrips(vdSelection GripSelection)
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
                if(!string.IsNullOrEmpty(fig.URL))
                    try
                    {
                        endPro = System.Diagnostics.Process.Start(fig.URL);
                    }
                    catch
                    {
                        MessageBox.Show("���������ļ������ڣ����������ã�");
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
        public void InitDrawData(string myLineRoadDesignName)
        {
            //1,ѡ�������ṩ��ʽ
            DataControl.designDataProvidePattern = DataProvidePattern.SQLServerDataBase;
            //2,��ʼ�������ṹ,���,��ͼ��,�������;
            DesignInitDirector designInitDirector = new DesignInitDirector(DesignInitBuilderFactory.CreateDesignInitBuilder(DataControl.designDataProvidePattern));
            designInitDirector.ExecDesignInit(myLineRoadDesignName);



        }
        private void CreateMySplitControls()
        {
            l.Text = "3";
            l.AutoSize = true;
            l.Location = new Point(-6, splitter1.Height / 2 - l.Height);
            l.Font = new System.Drawing.Font("Marlett", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)2);
            l.ForeColor = System.Drawing.Color.Red;
            l.Cursor = Cursors.Hand;
            splitter1.Controls.Add(l);
            tt.SetToolTip(l, "���ػ�ͼ���");
            l.Click += new EventHandler(ShowHide);
            splitter1.Resize += new EventHandler(splitter_Resize);


        }

        private void ShowHide(object sender, EventArgs e)
        {
            if (l.Text == "3")
            {
                l.Text = "4";
                tt.SetToolTip(l, "��ʾ��ͼ���");
                this.panel2.Visible = false;
            }
            else
            {
                l.Text = "3";
                tt.SetToolTip(l, "���ػ�ͼ���");
                this.panel2.Visible = true;
            }
        }
        private void splitter_Resize(object o, EventArgs e)
        {
            l.Location = new Point(-6, splitter1.Height / 2 - l.Height);
        }

        public Childform cf;
        void createForm()
        {

            Childform form = new Childform();
            this.cf = form;
            form.MdiParent = this;
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
        public VectorDraw.Professional.vdCommandLine.vdCommandLine commandLine { get { return vdCommandLine1; } }
        public vdPropertyGrid.vdPropertyGrid vdgrid { get { return vdPropertyGrid1; } }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return false;
        }
        private void CoordDisplay_Click(object sender, EventArgs e)
        {
            mDisplayPolarCoord = !mDisplayPolarCoord;
        }

        private void New_Click(object sender, EventArgs e)
        {
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
            DrawCommonData.activeDocument = frm.vdScrollableControl1.BaseControl.ActiveDocument;
            commandLine.ExecuteCommand("new");
        }

        private void open_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("open");
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
            DrawCommonData.activeDocument = frm.vdScrollableControl1.BaseControl.ActiveDocument;
            frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.BkColorEx = System.Drawing.Color.White;
            frm.vdScrollableControl1.BaseControl.ActiveDocument.Model.UCS(new gPoint(0, 0), new gPoint(1, 0), new gPoint(0, -1));//��������ϵ

            frm.vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            frm.vdScrollableControl1.LayoutTab.Visible = false;
        }

        private void save_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("save");
        }

        private void print_Click(object sender, EventArgs e)
        {
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
            frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Printer.InitializePreviewFormProperties(true, true, false, false);
            frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Printer.DialogPreview();

        }

        private void saveas_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("saveas");
        }
        
        private void redraw_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("redraw");
        }

        private void undo_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("undo");
        }

        private void redo_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("redo");
        }

        private void zoomall_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("za");
        }

        private void zoomextends_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("ze");
        }

        private void zoomprevious_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("zp");
        }

        private void zoomwindow_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("zw");
        }

        private void zoomin_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("zi");
        }

        private void zoomout_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("zo");
        }

        private void line_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("line");
        }

        private void arc_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("arc");
        }

        private void circle_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("circle");
        }

        private void text_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("text");
        }

        private void point_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("point");
        }

        private void ellipse_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("ellipse");
        }

        private void rectangle_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("rect");
        }

        private void polyline_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("pl");
        }

        private void image_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("image");
        }

        private void clipcut_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("clipcut");
        }

        private void clipcopy_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("clipcopy");
        }

        private void clippaste_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("clippaste");
        }

        private void bHatch_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("bhatch");
        }

        private void box_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("box");
        }

        private void cone_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("cone");
        }

        private void sphere_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("sphere");
        }

        private void mesh_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("mesh");
        }

        private void face_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("face");
        }

        private void plineToMesh_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("PlineToMesh");
        }

        private void dimvertical_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("DimRotatedVer");
        }

        private void dimhorizontal_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("DimRotatedHor");
        }

        private void dimaligned_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("DimAligned");

        }

        private void dimangular_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("DimAngular");
        }

        private void dimdiameter_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("DimDiameter");
        }

        private void dimradial_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("DimRadial");
        }

        private void rotate3D_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("Rotate3D");
        }

        private void rotate_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("Rotate");
        }

        private void copy_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("Copy");
        }

        private void erase_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("Erase");
        }

        private void move_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("Move");
        }

        private void explode_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("Explode");
        }

        private void mirror_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("Mirror");
        }

        private void breakEntity_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("Break");
        }

        private void offset_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("Offset");
        }

        private void extend_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("Extend");
        }

        private void trim_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("Trim");
        }

        private void fillet_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("Fillet");
        }

        private void stretch_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("Stretch");
        }

        private void rotateDynamic_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("View3DVrot");
        }

        private void render_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("View3DRender");
        }

        private void shade_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("View3DShade");
        }

        private void shadeOn_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("View3DShadeOn");
        }

        private void hide_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("View3DHide");
        }

        private void wire3d_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("View3DWire");
        }

        private void wire2d_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("View3DWire2d");
        }

        private void viewtop_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("View3DVTop");
        }

        private void viewbottom_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("View3DVBottom");
        }

        private void viewleft_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("View3DVLeft");
        }

        private void viewright_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("View3DVRight");
        }

        private void viewfront_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("View3DVFront");
        }

        private void viewback_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("View3DVBack");
        }

        private void viewnorthEast_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("View3DVINE");

        }

        private void viewnorthWest_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("View3DVINW");
        }

        private void viewsouthEast_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("View3DVISE");
        }

        private void viewsouthWest_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("View3DVISW");
        }

        private void layers_Click(object sender, EventArgs e)
        {
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
            //  VectorDraw.Professional.Dialogs.LayersDialog
            VectorDraw.Professional.Dialogs.LayersDialog.Show(frm.vdScrollableControl1.BaseControl.ActiveDocument);

        }

        private void textStyles_Click(object sender, EventArgs e)
        {
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
            VectorDraw.Professional.Dialogs.frmTextStyle.Show(frm.vdScrollableControl1.BaseControl.ActiveDocument);
        }

        private void dimensionStyles_Click(object sender, EventArgs e)
        {
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
            VectorDraw.Professional.Dialogs.frmDimStyle.Show(frm.vdScrollableControl1.BaseControl.ActiveDocument);
        }

        private void pointStyles_Click(object sender, EventArgs e)
        {
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
            VectorDraw.Professional.Dialogs.frmPointStyleDialog.Show(frm.vdScrollableControl1.BaseControl.ActiveDocument);
        }

        private void externalReferences_Click(object sender, EventArgs e)
        {
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
            VectorDraw.Professional.Dialogs.frmXrefManager.Show(frm.vdScrollableControl1.BaseControl.ActiveDocument);
        }

        private void imageDefinitions_Click(object sender, EventArgs e)
        {
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
            VectorDraw.Professional.Dialogs.FrmImageDefs.Show(frm.vdScrollableControl1.BaseControl.ActiveDocument);
        }

        private void lights_Click(object sender, EventArgs e)
        {
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
            VectorDraw.Professional.Dialogs.frmLightManager.Show(frm.vdScrollableControl1.BaseControl.ActiveDocument);
        }

        private void WindowCascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void tileHorizontal_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVertical_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void arrangeIcons_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void closeAllWindows_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
                frm.Close();
        }

        private void about_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "¼��������ͼϵͳ","Version1.0", MessageBoxButtons.OK);
        }

        private void writeBlock_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("writeblock");
        }

        private void makeBlock_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("makeblock");
        }

        private void insertBlock_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("insert");
        }

        private void editAttribute_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("EditAttrib");
        }

        private void addAttribute_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("addattribute");
        }

        private void arrayrectangle_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("arrayrectangular");
        }

        private void arraypolar_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("arraypolar");
        }

        private void panToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("pan");
        }

        private void osnapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;

            VectorDraw.Professional.vdObjects.vdDocument doc = frm.vdScrollableControl1.BaseControl.ActiveDocument;
            VectorDraw.Professional.Dialogs.OSnapDialog.Show(doc, doc.ActionControl);
        }

        private void mtextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("mtext");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
            VectorDraw.Geometry.gPoint retptWorld = VectorDraw.Professional.ActionUtility.ActionMagnifier.getUserMagnifierPoint(frm.vdScrollableControl1.BaseControl.ActiveDocument, 3, 210, (int)Keys.ShiftKey);
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
      
        private int startDepth1 = -999;
        private int startDepth2 = -999;
        private string tmpjd = "";
        public void SetFrameDesign()
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
                    MessageBox.Show("������������??????");
                    return;
                }**/
                if (!string.IsNullOrEmpty(this.tb_JH.Text))
                {
                    FrameDesign.JH = this.tb_JH.Text;
                }

                //������ֵ���㣻  
                //TO BE DONE
                FrameDesign.CorValue = FrameDesign.XCoordinate + ":" + FrameDesign.YCoordinate;//������ֵ��ֵ
                if (Math.Abs(FrameDesign.YCoordinate) > 0.0001)
                    FrameDesign.ValueCoordinate = FrameDesign.XCoordinate / FrameDesign.YCoordinate;
                //�������

                FrameDesign.JdStrLst.Clear();

                for (int i = 0; i < this.lb_JD.Items.Count; i++)//ѭ����ӽ�������Ϣ
                {
                    string tmpJdStr = this.lb_JD.Items[i].ToString().Trim();//�������һ����¼
                    tmpjd = tmpJdStr;
                    
                    FrameDesign.JdStrLst.Add(tmpJdStr);
                }
               
                try
                {
                 //   string currData = this.lb_JD.SelectedItem.ToString().Trim(); //����ѡ�еļ�¼
                    string[] strStartDepthArr = Regex.Split(tmpjd, DrawCommonData.jdSplitter.ToString());
                    
                    this.startDepth1 = int.Parse(strStartDepthArr[0].Trim());
                    this.startDepth2 = int.Parse(strStartDepthArr[1].Trim());

                    DrawCommonData.upperdepth = this.startDepth1;
                    DrawCommonData.lowerdepth = this.startDepth2;

                    
                }
                catch (Exception e)
                {
                    MessageBox.Show("data Error");
                    return;
                }
                FrameDesign.Validate(this);//������֤��
            }
            else  //DrawCommonData.icount != 0
            {
                //change this
               FrameDesign.JdStrLst.Clear();
           

                if (this.startDepth1 < 0 || this.startDepth2 < 0)
                {
                    MessageBox.Show("error");
                    throw new Exception("Error startDepth");
                  //  return;
                }

                this.startDepth1 += stepnum;
                this.startDepth2 += stepnum;
                string jdstr = startDepth1.ToString() + DrawCommonData.jdSplitter + startDepth2.ToString();
                FrameDesign.JdStrLst.Add(jdstr);
                    

            }

            DrawCommonData.icount++;


        }
        /// <summary>
        /// ����¼������ͼ
        /// </summary>
        public void DrawLJJSPicture()
        {
            Childform frm = this.ActiveMdiChild as Childform;
            
            if (frm == null) return;

            DrawCommonData.activeDocument = frm.vdScrollableControl1.BaseControl.ActiveDocument;
            if (!FrameDesign.Validate(this))//��֤�� ��!=true ��return
                return;
         //   frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ZoomWindow(new VectorDraw.Geometry.gPoint(-170, 0), new VectorDraw.Geometry.gPoint(170.0, 200));

            LJJSBuilder ljjsBuilder = new LJJSBuilderImpl();
            LJJSDirector ljjsDirector = new LJJSDirector(ljjsBuilder);
            ljjsDirector.BuildLJJS();

            vdDocument activeDocu = DrawCommonData.activeDocument;
            
            activeDocu.Redraw(true);
            
            commandLine.ExecuteCommand("ze");

        }

        //����
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            /**
            DrawCommonData.icount = 0;
            timer1.Enabled = true;
            **/

            SuiZuanForm szf = new SuiZuanForm();
            szf.mf = this;
            szf.Show();


            ////��ͼ������ͼ��ܵ������Ϣд��ڰ�����toolStripButton2
            //SetFrameDesign();
            ////����lb_LineRoadDesign���������ߵ����˳��
            //SetLineRoadDesign();

            ////�ߵ������ߵ���ܼ��������Ƶ����ݵ������Ϣд��ڰ�����
            //FrameControlData.LineRoadControlLst = LineRoadOper.getLineRoadControlDataLst(LineRoadDesign.LineRoadDesginLst);
            //WorkDataManage.WorkDataManage.BuildWorkDataHt();//��û�ͼ�������ݣ����������WorkDataManage���WorkDataHt�����У�
            //Entities.ClearAll();


            ////��ͼ
            //DrawLJJSPicture();


        }
        public void SetLineRoadDesign()
        {
            List<LineRoadDesignClass> tmp=new List<LineRoadDesignClass>();
            for (int i = 0; i < this.lb_LineRoadDesign.Items.Count; i++)
            {
               // List<lineroad>
                string tmpLrId=GetLineRoadIdByDesignStr(this.lb_LineRoadDesign.Items[i].ToString());
              
              LineRoadDesignClass tmpstruc=  LineRoadDesign.GetLineRoadDesignStrucById(tmpLrId);
              tmp.Add(tmpstruc);
 
            }
            LineRoadDesign.LineRoadDesginLst = tmp;
 
        }

        public void ProperSetLineRoadDesign()
        {
            
            List<LineRoadDesignClass> tmp = new List<LineRoadDesignClass>();
            this.lb_LineRoadDesign.Items.Clear();

            this.lb_LineRoadDesign.Items.Add("LRD20131217000030:����");
            //"LRD20190916000004:����ָ��;"
            this.lb_LineRoadDesign.Items.Add("LRD20190916000004:����ָ��;");
            this.lb_LineRoadDesign.Items.Add("LRD20190916000005:����;����;");
           
            this.lb_LineRoadDesign.Items.Add("LRD20191006000004:������;��е����;");
            this.lb_LineRoadDesign.Items.Add("LRD20200307000027:��ֵ��;");

            for (int i = 0; i < this.lb_LineRoadDesign.Items.Count; i++)
            {
                // List<lineroad>
                string tmpLrId = GetLineRoadIdByDesignStr(this.lb_LineRoadDesign.Items[i].ToString());

                LineRoadDesignClass tmpstruc = LineRoadDesign.GetLineRoadDesignStrucById(tmpLrId);
                tmp.Add(tmpstruc);

            }
            LineRoadDesign.LineRoadDesginLst = tmp;
            /**
            string tmpLrId = GetLineRoadIdByDesignStr("LRD20131217000030:����");
            LineRoadDesignClass tmpstruc = LineRoadDesign.GetLineRoadDesignStrucById(tmpLrId);
            tmp.Add(tmpstruc);

            tmpLrId = GetLineRoadIdByDesignStr("LRD20190916000005:����;����;");
            tmpstruc = LineRoadDesign.GetLineRoadDesignStrucById(tmpLrId);
            tmp.Add(tmpstruc);

            tmpLrId = GetLineRoadIdByDesignStr("LRD20191006000004:������;��е����;");
            tmpstruc = LineRoadDesign.GetLineRoadDesignStrucById(tmpLrId);
            tmp.Add(tmpstruc);

            tmpLrId = GetLineRoadIdByDesignStr("LRD20200307000005:��ֵ��;");
            tmpstruc = LineRoadDesign.GetLineRoadDesignStrucById(tmpLrId);
            tmp.Add(tmpstruc);

            LineRoadDesign.LineRoadDesginLst = tmp;**/
        }



        private void SetItemDesign()
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SelectWell selectWell = new SelectWell();
            selectWell.ShowDialog();
            if (!string.IsNullOrEmpty(FrameDesign.JH))
            {
                this.tb_JH.Text = FrameDesign.JH;
            }

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Add_JD();
        }
        /// <summary>
        /// ����:�ھ����������Ӿ���
        /// </summary>
        
        private void Add_JD()
        {
            string temp_jdstr = "";
            if (tb__JDStart.Text.Trim() != "" && tb__JDEnd.Text.Trim() != "")
            {
                temp_jdstr = tb__JDStart.Text + DrawCommonData.jdSplitter + tb__JDEnd.Text;
                lb_JD.Items.Add(temp_jdstr);//��Ӿ���
                //��ʼ�����ֹ���������
                tb__JDStart.Text = null;
                tb__JDEnd.Text = null;
            }
        }
        
        private void btn_Del_Click(object sender, EventArgs e)
        {
            CommonControlOper.DeleteFromList(this.lb_JD);

        }

        private void drawToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ���ݿ�ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ���͹���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;

            VectorDraw.Professional.Dialogs.GetLineTypeDialog.Show(frm.vdScrollableControl1.BaseControl.ActiveDocument, null, "Solid", false);
            //  .Show(frm.vdScrollableControl1.BaseControl.ActiveDocument);
        }

        private void ͼ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LegendDesign legendfrm = new LegendDesign();
            legendfrm.ShowDialog();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("ze");
        }

        private void HeaderTableModelSelect()
        {
            TuTouTableOper oper = new TuTouTableOper();
            if (oper.ShowDialog().Equals(DialogResult.OK))
            {
                Childform frm = this.ActiveMdiChild as Childform;
                if (frm == null) return;
                frm.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.CmdInsert(TuTouTableOper.tm.path, TuTouTableOper.tm.insertPoint, TuTouTableOper.tm.xScale, TuTouTableOper.tm.yScale, 0.0);
                frm.vdScrollableControl1.LayoutTab.Visible = false;
                DataControl.TuTouBiaoBlockName = Path.GetFileName(TuTouTableOper.tm.path);
            }
        }

        private void blocksToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            //1,���û�ͼ�Ļdocument��
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
            DrawCommonData.activeDocument = frm.vdScrollableControl1.BaseControl.ActiveDocument;
            //2,���ݳ�ʼ����������ʼ�������������ƣ�
            //2-1�������ƿ��ƽṹΪFrameDesign,��FrameDesignForm�����������ƣ�
            //2-2,�ߵ���ƿ��ƽṹΪLineRoadDesign.lineRoadDesginLst�����������������ƣ�


            LineRoadWinDesign();



            //2-3,������ƿ��ƽṹΪLineRoadDesign.jingShenDesign

            //2-4,ͼ����ƿ��ƽṹΪFrameDesign��û�е�����������ṹ��






        }

        private void LineRoadWinDesign()
        {
            this.lb_LineRoadDesign.Items.Clear();
            int lCount = LineRoadDesign.LineRoadDesginLst.Count;
            for (int i = 0; i < lCount; i++)
            {
                //�ߵ�ID,��ӦLineRoadDesignDetail�е�LineRoadDesignDetaiID
                string lid = LineRoadDesign.LineRoadDesginLst[i].LineRoadId;

                //�ߵ������Ļ�ͼ�
                string tmpItemNames = "";
                List<DrawItemName> drawItemlst = LineRoadDesign.LineRoadDesginLst[i].Drawingitems;
                if (LineRoadDesign.LineRoadDesginLst[i].LineRoadStyle.Equals(LineRoadStyle.JingShenLineRoad))
                    tmpItemNames = "����";
                else
                {
                    for (int j = 0; j < drawItemlst.Count; j++)
                    {
                        tmpItemNames = tmpItemNames + drawItemlst[j].DrawItemShowName + ";";
                    }
                }

                string lineRoadShowContent = lid + ":" + tmpItemNames;

                if(this.lb_LineRoadDesign.Items.Contains(lineRoadShowContent)== false)
                {
                    this.lb_LineRoadDesign.Items.Add(lineRoadShowContent);
                }
                
            }
            int iiui = 99;
            int count = this.lb_LineRoadDesign.Items.Count;
            if(count != 0&&this.lb_LineRoadDesign.Items[count - 1].ToString().Contains("����"))
            {
                var temp = this.lb_LineRoadDesign.Items[count - 1];
                this.lb_LineRoadDesign.Items[count - 1] = this.lb_LineRoadDesign.Items[0];
                this.lb_LineRoadDesign.Items[0] = temp;
                
            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("pan");
            //1,���û�ͼ�Ļdocument��
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
           
        }

        private static void HeaderTableFill()
        {
            //1,���ͼͷ����������ԣ�
            List<string> headerTableAttr = TuTouTableBuilder.GetHeaderTableAllAttri();
            //2,����ͼͷ�����ݽṹ��ΪDictionary��keyΪ�ֶ�����valueΪ��ȡ������ֵ��
            HTDataProviderFactory.getHTWorkDataProvider().BuildHtWorkData(headerTableAttr);
            //3,����ͼͷ����ͼͷ���������ԣ�������headerTableAttr�������ֶΣ������Ӧ���ֶ�ֵ��HeaderTableDataManage.HeaderTableData�У����и�ֵ��
            TuTouTableBuilder.BuildHeaderTable(headerTableAttr);
        }

        private void ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrameDesginForm frameDesignForm = new FrameDesginForm();
            frameDesignForm.ShowDialog();
        }

        private void ͼͷ��ѡ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void ͼͷ�����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            //OracTestForm frm = new OracTestForm();
            //frm.Show();
            //TestOracleDAL td = new TestOracleDAL();
            //DbDataReader db = td.GetDefaultFrameModel("");
            //if (db.HasRows)
            //{

            //    int i = db.FieldCount;
            //    MessageBox.Show(i.ToString());

            //}
            YXYGPictureForm tt = new YXYGPictureForm();
            tt.Show();

        }
        /**
        /if (cansee == false)
            {
                cansee = true;
                this.p_DrawItemDesign.Visible = cansee;
                List<DrawItemName> selectedItems=ItemOper.GetAllSelectedDrawItemName();
                CommonControlOper.FillDrawItemListBox(selectedItems,this.lb_SelectedItems);
                this.p_LineRoadDesign.Visible = !cansee;
                btn_Other.Text = "ѡ�����ģʽ                    ��   ";
            }
            else
            {
                cansee = false;
                this.p_DrawItemDesign.Visible = cansee;
                this.p_LineRoadDesign.Visible = !cansee;
                btn_Other.Text = "ѡ�����ģʽ                    ��   ";
            }

            SheDingMoBanForm sbmbf = new SheDingMoBanForm();
            sbmbf.cf = this.ActiveMdiChild as Childform;

            sbmbf.Show();
        **/

        private void btn_Other_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //����ߵ���
            Frm_LineRoad frm_lineroad = new Frm_LineRoad();
            if (frm_lineroad.ShowDialog() == DialogResult.OK)
            {
                LineRoadWinDesign();
            }



        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            Frm_LineRoad frm_lineRoad = new Frm_LineRoad();
            string tmpStr = this.lb_LineRoadDesign.SelectedItem.ToString();
            frm_lineRoad.CurrLineRoadId = GetLineRoadIdByDesignStr(tmpStr);
            if (frm_lineRoad.ShowDialog() == DialogResult.OK)
            {
                LineRoadWinDesign();
            }

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


        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            ComboUtil.ListitemsChange(this.lb_LineRoadDesign, true);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ComboUtil.ListitemsChange(this.lb_LineRoadDesign,false);
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            ShowYXPictureFrm();

            
        }

        private void ShowYXPictureFrm()
        {
            //��ʾ��мͼ��ģʽ��ѡ��������ʾ��мͼ��
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
            frm.vdScrollableControl1.BaseControl.ActiveDocument.Prompt("ѡ����ʾӫ��");
            vdFigure fig;
            gPoint userpt;
            //This command waits until thew user clicks an entity.
            StatusCode code = frm.vdScrollableControl1.BaseControl.ActiveDocument.ActionUtility.getUserEntity(out fig, out userpt);
            //       vdSelection selset = frm.vdScrollableControl1.BaseControl.ActiveDocument.ActionUtility.getUserSelection();
            frm.vdScrollableControl1.BaseControl.ActiveDocument.Prompt(null);
            // if (selset != null)
            if (code == StatusCode.Success)
            {
                // vdFigure tmp = selset[0];
                if (fig != null)
                {
                    fig.HighLight = true;
                    frm.vdScrollableControl1.BaseControl.ActiveDocument.Prompt("ӫ��鿴");


                    vdXProperty frmStyle = fig.XProperties.FindName(ImeItemAdditionProperties.formStyle);
                    if (null != frmStyle)
                    {
                        string frmstr = frmStyle.PropValue.ToString().Trim();
                        if (!StrUtil.StrCompareIngoreCase(frmstr, ImageFormStyle.YXPicture))//�Ƿ���мͼ��Ļ�ͼʵ��
                        {
                            MessageBox.Show("ѡ��ʵ��Ϊ��ӫ�����ѡ��ӫ���");
                            fig.HighLight = false;
                            frm.vdScrollableControl1.BaseControl.ActiveDocument.Prompt(null);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("ѡ��ʵ��Ϊ��ӫ�����ѡ��ӫ���");
                        fig.HighLight = false;
                        frm.vdScrollableControl1.BaseControl.ActiveDocument.Prompt(null);
                        return;
                    }


                    vdXProperty ygAddtionIme = fig.XProperties.FindName(ImeItemAdditionProperties.pathOneProName);
                    vdXProperty bgAddtionIme = fig.XProperties.FindName(ImeItemAdditionProperties.pathTwoProName);
                    vdXProperty jygAddtionIme = fig.XProperties.FindName(ImeItemAdditionProperties.pathThreeProName);
                    vdXProperty jbgAddtionIme = fig.XProperties.FindName(ImeItemAdditionProperties.pathFourProName);

                    if (null == ygAddtionIme && null == bgAddtionIme && null == jygAddtionIme && null == jbgAddtionIme)
                    {
                        MessageBox.Show("ѡ��ʵ��Ϊ��ӫ�����ѡ��ӫ���");
                        fig.HighLight = false;
                        frm.vdScrollableControl1.BaseControl.ActiveDocument.Prompt(null);
                        return;
                    }

                    YXYGPictureForm yxygfrm = new YXYGPictureForm();
                    if (null != ygAddtionIme)
                        yxygfrm.YingguangPath = ygAddtionIme.PropValue.ToString();
                    if (null != bgAddtionIme)
                        yxygfrm.BaiguangPath = bgAddtionIme.PropValue.ToString();
                    if (null != jygAddtionIme)
                        yxygfrm.JieyingguangPath = jygAddtionIme.PropValue.ToString();
                    if (null != jygAddtionIme)
                        yxygfrm.JiebaiguangPath = jbgAddtionIme.PropValue.ToString();
                    yxygfrm.Show();
                    fig.HighLight = false;
                    frm.vdScrollableControl1.BaseControl.ActiveDocument.Prompt(null);
                }


            }
            else
                MessageBox.Show("�û�ȡ����ӫ��ѡ��");
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (null != lb_LineRoadDesign.SelectedItem)
            {

                if (DialogResult.Yes == MessageBox.Show("ȷ��ɾ�����ߵ�ô��", "�ߵ�ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {


                    string tmpid = GetLineRoadIdByDesignStr(lb_LineRoadDesign.SelectedItem.ToString());
                    //1,���ߵ�����ṹ��ɾ�����ߵ����ݣ�
                    LineRoadDesign.DeleteLRDesignStrucByID(tmpid);
                    //2,���ߵ���ƿ���ɾ�����ߵ����ݣ�
                    this.lb_LineRoadDesign.Items.RemoveAt(lb_LineRoadDesign.SelectedIndex);


                }
            }
            else
            {
                MessageBox.Show("��ѡ��ɾ���ߵ�");
            }

        }

        private void lb_LineRoadDesign_Click(object sender, EventArgs e)
        {
           
        }

        private void lb_LineRoadDesign_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Frm_LineRoad frm_lineRoad = new Frm_LineRoad();
            if (null != this.lb_LineRoadDesign.SelectedItem)
            {
                string tmpStr = this.lb_LineRoadDesign.SelectedItem.ToString();
                frm_lineRoad.CurrLineRoadId = GetLineRoadIdByDesignStr(tmpStr);
                if (frm_lineRoad.ShowDialog() == DialogResult.OK)
                {
                    LineRoadWinDesign();
                }
            }

        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            YGXWPictureFrmShow();

            
        }

        private void YGXWPictureFrmShow()
        {
            //��ʾӫ����΢ͼ��ģʽ��ѡ��������ʾӫ����΢ͼ��
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
            frm.vdScrollableControl1.BaseControl.ActiveDocument.Prompt("ѡ����ʾ��ͼƬ");
            vdFigure fig;
            gPoint userpt;
            //This command waits until thew user clicks an entity.
            StatusCode code = frm.vdScrollableControl1.BaseControl.ActiveDocument.ActionUtility.getUserEntity(out fig, out userpt);
            //       vdSelection selset = frm.vdScrollableControl1.BaseControl.ActiveDocument.ActionUtility.getUserSelection();
            frm.vdScrollableControl1.BaseControl.ActiveDocument.Prompt(null);
            // if (selset != null)
            if (code == StatusCode.Success)
            {
                // vdFigure tmp = selset[0];
                if (null != fig)
                {
                    fig.HighLight = true;
                    frm.vdScrollableControl1.BaseControl.ActiveDocument.Prompt("ӫ��鿴");


                    vdXProperty frmStyle = fig.XProperties.FindName(ImeItemAdditionProperties.formStyle);
                    string frmstr = "";
                    if (null != frmStyle)
                    {
                        frmstr = frmStyle.PropValue.ToString().Trim();
                        if (!StrUtil.StrCompareIngoreCase(frmstr, ImageFormStyle.YGXWPicture))//�Ƿ�ӫ����΢ͼ��Ļ�ͼʵ��
                        {
                            MessageBox.Show("ѡ��ʵ��Ϊ��ӫ����΢ͼ��ʵ�壬��ѡ��ӫ����΢ͼ��ʵ�壡");
                            fig.HighLight = false;
                            frm.vdScrollableControl1.BaseControl.ActiveDocument.Prompt(null);
                            return;
                        }

                    }
                    else
                    {
                        MessageBox.Show("ѡ��ʵ��Ϊ��ӫ����΢ͼ��ʵ�壬��ѡ��ӫ����΢ͼ��ʵ�壡");
                        fig.HighLight = false;
                        frm.vdScrollableControl1.BaseControl.ActiveDocument.Prompt(null);
                        return;

                    }
                    vdXProperty oneAddtionIme = fig.XProperties.FindName(ImeItemAdditionProperties.pathOneProName);
                    vdXProperty twoAddtionIme = fig.XProperties.FindName(ImeItemAdditionProperties.pathTwoProName);
                    vdXProperty threeAddtionIme = fig.XProperties.FindName(ImeItemAdditionProperties.pathThreeProName);
                    vdXProperty fourAddtionIme = fig.XProperties.FindName(ImeItemAdditionProperties.pathFourProName);

                    vdXProperty fiveAddtionIme = fig.XProperties.FindName(ImeItemAdditionProperties.pathFiveProName);
                    vdXProperty sixAddtionIme = fig.XProperties.FindName(ImeItemAdditionProperties.pathSixProName);



                    YGXWPictureFrm yxygfrm = new YGXWPictureFrm();
                    if (null != oneAddtionIme)
                        yxygfrm.FiveBeiPicturePath = oneAddtionIme.PropValue.ToString();
                    if (null != twoAddtionIme)
                        yxygfrm.TenBeiPicturePath = twoAddtionIme.PropValue.ToString();
                    if (null != threeAddtionIme)
                        yxygfrm.TwentyBeiPicturePath = threeAddtionIme.PropValue.ToString();
                    if (null != threeAddtionIme)
                        yxygfrm.TwentyBeiChuLiPicturePath = fourAddtionIme.PropValue.ToString();
                    if (null != threeAddtionIme)
                        yxygfrm.PianGuangPicturePath = fiveAddtionIme.PropValue.ToString();
                    if (null != threeAddtionIme)
                        yxygfrm.KeepWordPicturePath = sixAddtionIme.PropValue.ToString();
                    yxygfrm.Show();
                    fig.HighLight = false;
                    frm.vdScrollableControl1.BaseControl.ActiveDocument.Prompt(null);
                }
            }
            else
                MessageBox.Show("�û�ȡ������΢ͼ��ѡ��");
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
          
            //We will ask the user to select an entity or to type Red,Green,Blue in order to select the entity.
          
            object ret = frm.vdScrollableControl1.BaseControl.ActiveDocument.ActionUtility.getUserPoint();
            if (ret is gPoint)
            {
                gPoint p1 = frm.vdScrollableControl1.BaseControl.ActiveDocument.World2PixelMatrix.Transform(ret as gPoint);
                Point location = new Point((int)p1.x, (int)p1.y);
                vdFigure fig = frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.GetEntityFromPoint(location, frm.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Render.GlobalProperties.PickSize, false);
                if (null == fig)
                    return;
                fig.HighLight = true;
                MessageBox.Show(fig.Handle.ToString());
            }
            else
                return;

        }

        private void MainForm_Click(object sender, EventArgs e)
        {

        }

        private void xML��ĿToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void ��мͼ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowYXPictureFrm();
        }

        private void ӫ����΢ͼ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YGXWPictureFrmShow();
        }

        private void ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            Childform frm = this.ActiveMdiChild as Childform;
            if (frm == null) return;
            frm.vdScrollableControl1.BaseControl.ActiveDocument.Prompt("ѡ��������ӵ�ʵ�壬����Ҽ�ȷ��");
            vdSelection selset = frm.vdScrollableControl1.BaseControl.ActiveDocument.ActionUtility.getUserSelection();
            frm.vdScrollableControl1.BaseControl.ActiveDocument.Prompt(null);
       
         
            if (null !=selset &&selset.Count>0)
            {
                vdFigure fig = selset[0];
                
                if (null != fig)
                {
                    fig.HighLight = true;
                   
                    HyperTxtLinkFrm linkFrm = new HyperTxtLinkFrm();
                    if (!string.IsNullOrEmpty(fig.URL))
                    {
                        linkFrm.linkURL = fig.URL;
                    }
                    if (DialogResult.OK == linkFrm.ShowDialog())
                    {
                        if (!string.IsNullOrEmpty(linkFrm.linkURL))
                        {
                            fig.URL = linkFrm.linkURL;
                        }
                    }
                    fig.HighLight = false;
                    frm.vdScrollableControl1.BaseControl.ActiveDocument.Prompt(null);
                }


            }
            else
                MessageBox.Show("�û�ȡ���˳����Ӳ���");
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {

        }

        private void lb_SelectedItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
             if (null != this.lb_SelectedItems.SelectedItem)
             {
                 string selectItem = this.lb_SelectedItems.SelectedItem.ToString();

             }
        }

        private void ģ��ѡ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HeaderTableModelSelect();
        }

        private void ͼͷ�����ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HeaderTableFill();
        }

        private void toolStripButton15_Click_1(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("line");
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("text");
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("Erase");
        }

        //��ͼ����������
        async private void toolStripButton18_Click(object sender, EventArgs e)
        {
            

            if(this.lb_LineRoadDesign.Items.Count == 0)
            {
                MessageBox.Show("��û��ѡ��ģ�壬����ѡ��ģ��");
                return;
            }

            SQLServerWorkDataProvider.invoke_method = "��ͼ";  //�û�ͼ�ķ�ʽ��ȡ���ݿ⣬��һ�ζ����������ݣ�����depth

            #region    �Ծ�̬����г�ʼ��
            DrawPointContainer.initiate();
            DrawCommonData.Initiate();
            pTableContainer.initiate();
            #endregion

            DrawCommonData.icount = 0;

            //��ͼ������ͼ��ܵ������Ϣд��ڰ�����
            SetFrameDesign();
            //����lb_LineRoadDesign���������ߵ����˳��
            SetLineRoadDesign();

            //�ߵ������ߵ���ܼ��������Ƶ����ݵ������Ϣд��ڰ�����
            FrameControlData.LineRoadControlLst = LineRoadOper.getLineRoadControlDataLst(LineRoadDesign.LineRoadDesginLst);
            WorkDataManage.WorkDataManage.BuildWorkDataHt();//��û�ͼ�������ݣ����������WorkDataManage���WorkDataHt�����У�

            Entities.ClearAll();


           
            if (TXTmsej.Text != "")
            {

                DrawCommonData.base_value = this.TXTmsej.Text;

            }

            this.toolStripButton2.Enabled = false;

            this.toolStripButton18.Enabled = false;
            this.toolStripButton20.Enabled = false;

            //��ͼ
            await Task.Run(() => { this.DrawLJJSPicture(); });
           
            //2 18 20

            this.toolStripButton2.Enabled = true;

            this.toolStripButton18.Enabled = true;
            this.toolStripButton20.Enabled = true;

            this.TXTmsej.Text = DrawCommonData.base_value;

            this.vdentitiescount = this.cf.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Entities.Count;
           
 
        }
        public int vdentitiescount;
        private List<string> _jdStrLst = new List<string>(); //�����Ǿ�������� ��"300-400"
        public void set_jdStrList(List<string> inputList)
        {
            this._jdStrLst = inputList;
        }
     //   public int i = 1;
     //   public int j = -1;

        private void timer1_Tick(object sender, EventArgs e)
        {
           // MsehAndMsevContainer.Initiate();
            //��ͼ������ͼ��ܵ������Ϣд��ڰ�����
            SetFrameDesign();
            //����lb_LineRoadDesign���������ߵ����˳��
            if (DrawCommonData.icount == 0)
            {
                SetLineRoadDesign();
            }
                //�ߵ������ߵ���ܼ��������Ƶ����ݵ������Ϣд��ڰ�����
                FrameControlData.LineRoadControlLst = LineRoadOper.getLineRoadControlDataLst(LineRoadDesign.LineRoadDesginLst);
           
            WorkDataManage.WorkDataManage.BuildWorkDataHt();//��û�ͼ�������ݣ����������WorkDataManage���WorkDataHt�����У�
            Entities.ClearAll();
            //��ͼ
            DrawLJJSPicture();

        
     
       
          
           
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void tb__JDStart_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb__JDEnd_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void lb_LineRoadDesign_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        int stepnum = 30;
        private void tb_JDSTEP_TextChanged(object sender, EventArgs e)
        {
           string step = tb_JDSTEP.Text.ToString();
           bool b = int.TryParse(step, out stepnum);
           if (step != "30")
           {
               if (b)
               {
                   if (stepnum < 0 || stepnum > 50)
                   {
                       MessageBox.Show("����Ƿ�������0-50֮�����");
                   }
                   else
                   {
                       //MessageBox.Show("����ȷ���ɹ���");
                   }
               }
               else 
               {
                   stepnum = 30;
               }
           }
           else { stepnum = 30; }
    
          
          

        }

        private void tb_STEPSURE_Click(object sender, EventArgs e)
        {
            MSEJjd = TXTmsejjd.Text.ToString().Trim();
           

        }
       
        private void toolStripButtonColor_Click(object sender, EventArgs e)
        {
            
            ColorSelectionForm csf = new ColorSelectionForm();
            csf.MdiParent = this;
            Thread th1 = new Thread(csf.Show);
            th1.Start();
            csf.Show();
            
        }
        public ColorEnum color = ColorEnum.ERROR;

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.color.ToString());
        }

        private void tb_JH_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
           
           
        }

        private void g_DrawDesign_Enter(object sender, EventArgs e)
        {

        }

        private void btnsetBILICHi_Click(object sender, EventArgs e)
        {
            BiLiChiChangeForm blc = new BiLiChiChangeForm();
            blc.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            change_Moban();
        }

        private void change_Moban()
        {
            if (this.cmbMoban.SelectedIndex < 0)
            {
                MessageBox.Show("��û��ѡ��ģ��");
                return;
            }




                string myLineRoadDesignName = this.cmbMoban.SelectedItem.ToString().Trim();

                this.initdrawdata(myLineRoadDesignName);
            

            
        }
        private void change_Moban(string input)
        {
            this.initdrawdata(input);

        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void p_DrawItemDesign_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void TXTmsej_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            vdEntities arr = this.cf.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Entities;
            MessageBox.Show("����" + arr.Count);
            foreach(vdFigure figure in arr)
            {
                
                //MessageBox.Show(figure._TypeName);
                /**
                if(figure is vdText)
                {
                    vdText text = figure as vdText;
                   MessageBox.Show(text.TextString);
                }**/
            }
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
            this.cf.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.CmdLine(null);
        }


        public int find_nearest_depth(List<vdText> depthList ,vdLine line )
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
            depthList.Sort(cmp);  //����ȵ����ִ�С��������

            for(int i =0;i<depthList.Count ;i++)
            {
                /**
                if(line.StartPoint.y<= depthList[i].InsertionPoint.y  && line.StartPoint.y>= depthList[i+1].InsertionPoint.y )
                {
                    return int.Parse(depthList[i + 1].TextString);
                }**/
                if(Math.Abs(line.StartPoint.y - depthList[i].InsertionPoint.y) <=5)
                {
                    return int.Parse(depthList[i].TextString);
                }
            }
            return -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            vdEntities arr = this.cf.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Entities;
            List<vdLine> list = new List<vdLine>() ;
            //arr[arr.Count - 2], arr[arr.Count - 1]
            vdLine line1 = (vdLine)arr[arr.Count - 2];
            vdLine line2 = (vdLine)arr[arr.Count - 1];
            if(line1.StartPoint.y >= line2.StartPoint.y)
            {
                //line1������
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

                
                if(figure is vdText)
                {
                    vdText text = figure as vdText;
                   try
                   {
                       int depth = int.Parse(text.TextString);
                       if (depth >= this.startDepth1 && depth <= this.startDepth2)
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

            int upper = this.find_nearest_depth(depthList, list[0]);  //����Ͻ�

            int lower = this.find_nearest_depth(depthList, list[1]);  //����½�

            MessageBox.Show("�Ͻ�:" + upper.ToString());
            MessageBox.Show("�½�:" + lower.ToString());

        }

        private void toolStripButton20_Click_1(object sender, EventArgs e)
        {
            
            if(this.vdentitiescount ==0&&this.cf.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Entities.Count == 0)
            {
                MessageBox.Show("���󣬻�û�л�ͼ");
                return;
            }
            BaseLineForm blf = new BaseLineForm();
            blf.mf = this;
            blf.mf.change_Moban("��������");
            //  this.initdrawdata("��������");
            new Frm_LineRoad().AddLineRoadDesign("LRD");
            blf.Show();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
      
       // int stepnum=30;
       
        /*public void tb_STEPSURE_Click(object sender, EventArgs e)
        {
            string step = tb_JDSTEP.Text.ToString();
            bool b = int.TryParse(step, out stepnum);
            if (step != "30")
            {
                //bool b = int.TryParse(step, out stepnum);
                if (b)
                {
                    if (stepnum < 0 || stepnum > 50)
                    {
                        MessageBox.Show("����Ƿ�������0-50֮�����");
                    }
                    else
                    {
                        MessageBox.Show("����ȷ���ɹ���");
                    }
                }
               else if (step == "")//����ת�����1��step�ַ���Ϊ��
                {
                    stepnum = 30;
                }
               else//����ת�����2��������ַ����Ƿ��Ҳ���ת��
                {
                    MessageBox.Show("����Ƿ�������0-50֮�����");
                }
            }
            else { stepnum = 30; }
    
            
        }  */

        

        

   





    }
}