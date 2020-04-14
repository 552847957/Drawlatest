using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJJSCAD.Util;
using LJJSCAD.Model.Drawing;
using LJJSCAD.LJJSDrawing.Impl.DrawItem;
using LJJSCAD.CommonData;
using LJJSCAD.DrawingElement;
using System.IO;

namespace LJJSCAD.UI
{
    public partial class TuTouTableOper : Form
    {
        public static string filePath;
        public static bool isSelectInsertPt=false;
        public static double xScale = 1;
        public static double yScale = 1;
        public static LJJSPoint insertPt = new LJJSPoint(1,1);
        public static TuTouTableModel tm;
        public TuTouTableOper()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filePath = DialogUtil.GetFilePathByOpenDialog("图头表设计文件(*.vdml)|*.vdml|dxf文件(*.dxf)|*.dxf|vdcl文件(*.vdcl)|*.vdcl");

            this.tb_ModelFilePath.Text = filePath;
          
        }

        private void button2_Click(object sender, EventArgs e)
        {

            isSelectInsertPt = this.checkBox1.Checked;
            tm = new TuTouTableModel(filePath, this.tb_xInsertPt.Text, this.tb_yInsertPt.Text, this.tb_xScale.Text, this.tb_yScale.Text, this.checkBox1.Checked);
            this.DialogResult = DialogResult.OK;
        }
    }
}
