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
    public partial class YXYGPictureForm : Form
    {
        private string yingguangPath;//荧光图片路径

        public string YingguangPath
        {
            get { return yingguangPath; }
            set { yingguangPath = value; }
        }
        private string baiguangPath;//白光图片路径

        public string BaiguangPath
        {
            get { return baiguangPath; }
            set { baiguangPath = value; }
        }
        private string jieyingguangPath;//截荧光图片路径

        public string JieyingguangPath
        {
            get { return jieyingguangPath; }
            set { jieyingguangPath = value; }
        }
        private string jiebaiguangPath;//截白光图片路径

        public string JiebaiguangPath
        {
            get { return jiebaiguangPath; }
            set { jiebaiguangPath = value; }
        }

        public YXYGPictureForm()
        {
            InitializeComponent();
        }

        private void YXYGPictureForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(yingguangPath))
                this.pictureBox1.ImageLocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\"+yingguangPath;
            if (!string.IsNullOrEmpty(baiguangPath))
                this.pictureBox2.ImageLocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\"+baiguangPath;
            if (!string.IsNullOrEmpty(jieyingguangPath))
                this.pictureBox3.ImageLocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\"+jieyingguangPath;
            if (!string.IsNullOrEmpty(jiebaiguangPath))
                this.pictureBox4.ImageLocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\"+jiebaiguangPath;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
