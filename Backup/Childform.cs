using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VectorDraw.Actions;
using VectorDraw.Geometry;


namespace SimpleMdiCAD
{
    public partial class Childform : Form
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
            this.vdScrollableControl1.BaseControl.MouseMoveAfter += new VectorDraw.Professional.Control.MouseMoveAfterEventHandler(BaseControl_MouseMoveAfter);
            
        }

        

        

        
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
        

        private void BaseControl_MouseMoveAfter(MouseEventArgs e)
        {
            MainForm parent = this.MdiParent as MainForm;
            VectorDraw.Geometry.gPoint ccspt = vdScrollableControl1.BaseControl.ActiveDocument.CCS_CursorPos();
            
            double x = ccspt.x;
            double y = ccspt.y;
            double z = ccspt.z;

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
            MainForm parent = this.MdiParent as MainForm;

            if (parent == null) return;
            parent.UpdateMenu(false);
        }
    }
}