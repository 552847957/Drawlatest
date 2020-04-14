using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using VectorDraw.Professional;

namespace SimpleMdiCAD
{ 
    public partial class MainForm : Form
    {
        internal bool mDisplayPolarCoord = false;
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            MessageBox.Show("This Sample's purpose is to demonstrate how you can easily combine vdScrollableControl,vdCommandLine,vdPropertyGrid components in order to build an MDI application.");
            commandLine.ExecuteCommand("new");
        }
        

        void createForm()
        {

            Childform form = new Childform();
            form.MdiParent = this;
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
            commandLine.ExecuteCommand("new");
        }

        private void open_Click(object sender, EventArgs e)
        {
            commandLine.ExecuteCommand("open");
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
            Childform frm = this.ActiveMdiChild  as Childform;
            if (frm == null) return;
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
            MessageBox.Show(this, "Vectordraw Developer Freamework CAD version 1.0 \n Copyright(C) VectorDraw 1998-2007 \n\n VDF Cad is an example for VDF component.Not to use for commercial purposes.", "About Vectordraw Developer Freamework CAD", MessageBoxButtons.OK);
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

        

        
        
        
    }
}