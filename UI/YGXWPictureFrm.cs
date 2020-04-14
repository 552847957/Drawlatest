using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LJJSCAD.UI
{
    public partial class YGXWPictureFrm : Form
    {
        public string FiveBeiPicturePath { get; set; }	//五倍荧光图像路径
        public string TenBeiPicturePath { get; set; }	//十倍荧光图像路径
        public string TwentyBeiPicturePath { get; set; }	//二十部荧光图像路径
        public string TwentyBeiChuLiPicturePath { get; set; }	//二十倍处理荧光图像路径
        public string PianGuangPicturePath { get; set; }	//偏光荧光图像路径
        public string KeepWordPicturePath { get; set; }	//保留荧光图像路径

        public YGXWPictureFrm()
        {
            InitializeComponent();
        }

        private void YGXWPictureFrm_Load(object sender, EventArgs e)
        {
            this.pictureBox11.ImageLocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + "tb.JPG";
            if (!string.IsNullOrEmpty(FiveBeiPicturePath))
                this.pictureBox1.ImageLocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + FiveBeiPicturePath;
            if (!string.IsNullOrEmpty(TenBeiPicturePath))
                this.pictureBox2.ImageLocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + TenBeiPicturePath;
            if (!string.IsNullOrEmpty(TwentyBeiPicturePath))
                this.pictureBox3.ImageLocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + TwentyBeiPicturePath;
            if (!string.IsNullOrEmpty(TwentyBeiChuLiPicturePath))
                this.pictureBox4.ImageLocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + TwentyBeiChuLiPicturePath;
            if (!string.IsNullOrEmpty(PianGuangPicturePath))
                this.pictureBox5.ImageLocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + PianGuangPicturePath;
            if (!string.IsNullOrEmpty(KeepWordPicturePath))
                this.pictureBox6.ImageLocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + KeepWordPicturePath;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
