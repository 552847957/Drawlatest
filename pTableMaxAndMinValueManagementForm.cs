using LJJSCAD.CommonData;
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
    public partial class pTableMaxAndMinValueManagementForm : Form
    {
        public pTableMaxAndMinValueManagementForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public SuiZuanForm mf;
        private void btnOK_Click(object sender, EventArgs e)
        {
            if(this.tbMinMseh.Text != "")
            {
                DrawCommonData.MsehMin = double.Parse(this.tbMinMseh.Text);
            }
            else
            {
                DrawCommonData.MsehMin = null;
            }

            if (this.tbMinMsev.Text != "")
            {
                DrawCommonData.MsevMin = double.Parse(this.tbMinMsev.Text);
            }
            else
            {
                DrawCommonData.MsevMin = null;
            }

            if (this.tbMinMseb.Text != "")
            {
                DrawCommonData.MsebMin = double.Parse(this.tbMinMseb.Text);
            }
            else
            {
                DrawCommonData.MsebMin = null;
            }

            if (this.tbMinJGL.Text != "")
            {
                DrawCommonData.JGLMin = double.Parse(this.tbMinJGL.Text);
            }
            else
            {
                DrawCommonData.JGLMin = null;
            }


            if (this.tbMaxMseh.Text != "")
            {
                DrawCommonData.MsehMax = double.Parse(this.tbMaxMseh.Text);
            }
            else
            {
                DrawCommonData.MsehMax = null;
            }

            if (this.tbMaxMsev.Text != "")
            {
                DrawCommonData.MsevMax = double.Parse(this.tbMaxMsev.Text);
            }
            else
            {
                DrawCommonData.MsevMax = null;
            }

            if (this.tbMaxMseb.Text != "")
            {
                DrawCommonData.MsebMax = double.Parse(this.tbMaxMseb.Text);
            }
            else
            {
                DrawCommonData.MsebMax = null;
            }

            if (this.tbMaxJGL.Text != "")
            {
                DrawCommonData.JGLMax = double.Parse(this.tbMaxJGL.Text);
            }
            else
            {
                DrawCommonData.JGLMax = null;
            }




        }

        private void pTableMaxAndMinValueManagementForm_Load(object sender, EventArgs e)
        {

        }
    }
}
