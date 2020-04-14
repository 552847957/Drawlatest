using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJJSCAD.Model;

namespace LJJSCAD.UI
{
    public partial class ColorSelectionForm : Form
    {
        public ColorSelectionForm()
        {
            InitializeComponent();
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.cmbColor.SelectedIndex < 0)
            {
                MessageBox.Show("还没选中颜色");
                
            }
            string colorname = this.cmbColor.SelectedItem.ToString().Trim();
            switch (colorname)
            {
                case "红色":
                    MainForm mf = this.MdiParent as MainForm;
                    mf.color = ColorEnum.Red;
                    
                    this.Close();
                    break;
                case "绿色":
                    MainForm mf1 = this.MdiParent as MainForm;
                    mf1.color = ColorEnum.Green;
                    this.Close();
                    break;
                case "紫色":
                    MainForm mf2 = this.MdiParent as MainForm;
                    mf2.color = ColorEnum.Purple;
                    this.Close();
                    break;
                case "黄色":
                    MainForm mf3 = this.MdiParent as MainForm;
                    mf3.color = ColorEnum.Yellow;
                    this.Close();
                    break;
                default:
                    this.Close();
                    break;
                    
            }
           
        }

        private void ColorSelectionForm_Load(object sender, EventArgs e)
        {

        }
    }
}
