using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJJSCAD.BlackBoard.PropertiesManage;
using LJJSCAD.Util;

namespace LJJSCAD.UI
{
    public partial class HyperTxtLinkFrm : Form
    {
        public string linkURL { set; get; }

        public HyperTxtLinkFrm()
        {
            InitializeComponent();
        }

        private void HyperTxtLinkFrm_Load(object sender, EventArgs e)
        {
            this.tb_CurrLink.Text = linkURL;
       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tb_NewLink.Text))
                this.linkURL = this.tb_NewLink.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          this.tb_NewLink.Text=DialogUtil.GetFilePathByOpenDialog("");
 
        }
    }
}
