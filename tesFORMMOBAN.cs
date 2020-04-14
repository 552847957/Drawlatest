using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LJJSCAD
{
    public partial class tesFORMMOBAN : Form
    {
        public tesFORMMOBAN()
        {
            InitializeComponent();
        }

        private void vdFramedControl1_Load(object sender, EventArgs e)
        {
            this.vdFramedControl1.BaseControl.ActiveDocument.ActiveLayOut.BkColorEx = Color.White;
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
            
            cl.SelectDocument(this.vdFramedControl1.BaseControl.ActiveDocument);

            this.vdFramedControl1.BaseControl.Focus();
            int i = 1;
            string fname = i.ToString() + ".vdml";
            while (IsNewFileNameExist(fname))
            {
                i++;
                fname = i.ToString() + ".vdml";
            }
            this.vdFramedControl1.BaseControl.ActiveDocument.FileName = fname;
            this.Text = fname;

        }
        private void tesFORMMOBAN_Load(object sender, EventArgs e)
        {
            
        }
        public void open_moban()
        {
            //this.vdFramedControl1.BaseControl.AfterNewDocument += new VectorDraw.Professional.Control.AfterNewDocumentEventHandler(BaseControl_AfterNewDocument);
           // string filename = VectorDraw.Professional.Utilities.vdGlobals.GetDirectoryName(Application.ExecutablePath) + "\\tesFORMMOBAN.vdml";
            string version = "";
            string filename  = this.vdFramedControl1.BaseControl.ActiveDocument.GetSaveFileNameDlg(this.Text, out version);
            this.vdFramedControl1.BaseControl.ActiveDocument.Open(filename);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.vdFramedControl1.BaseControl.ActiveDocument.IsModified)
            {
                
            }
            DialogResult res = MessageBox.Show("Save changes in Drawing \n" + this.Text + " ?", this.Text, MessageBoxButtons.YesNoCancel);
            if (res == DialogResult.Yes)
            {
                string version = "";
                string fname = this.vdFramedControl1.BaseControl.ActiveDocument.GetSaveFileNameDlg(this.Text, out version);
                if (fname != null)
                {
                    bool success = this.vdFramedControl1.BaseControl.ActiveDocument.SaveAs(fname, null, version);
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
            base.OnClosing(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.vdFramedControl1.BaseControl.ActiveDocument.CommandAction.CmdLine(null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.open_moban();
        }
    }
}
