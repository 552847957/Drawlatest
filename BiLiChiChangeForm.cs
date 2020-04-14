using LJJSCAD.DrawingDesign.Frame;
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
    public partial class BiLiChiChangeForm : Form
    {
        public BiLiChiChangeForm()
        {
            InitializeComponent();
        }

        private void btnBiLIChiOK_Click(object sender, EventArgs e)
        {
            if (this.tbXCOORD.Text.Length == 0 || this.tbYCOORD.Text.Length == 0)
            {
                MessageBox.Show("比例尺填写不完整");
                return;
            }
            double xcoord = 0;
            double ycoord = 0;
            try
            {
                xcoord = Convert.ToDouble(this.tbXCOORD.Text);
                ycoord = Convert.ToDouble(this.tbYCOORD.Text);
                bool res = FrameDesign.set_Bi_Li_Chi(xcoord, ycoord);
                if (res == true)
                {
                    MessageBox.Show("比例尺设定成功");
                }
                else
                {
                    MessageBox.Show("比例尺设定失败，检查输入");
                }
            }
            catch
            {
                MessageBox.Show("出错了");
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrameDesign.return_to_default_BiLiChi();
        }
    }
}
