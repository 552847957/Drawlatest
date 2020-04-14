using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using LJJSCAD.DAL;
using LJJSCAD.DrawingDesign.Frame;


namespace LJJSCAD.UI
{
    public partial class SelectWell : Form
    {
        public SelectWell()
        {
            InitializeComponent();
        }

        private void SelectWell_Load(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.DataSource = SelectWellDAL.GetSelectWellDataTable();

            //dataGridView1.Columns[0].HeaderText = "系统编号";
            //dataGridView1.Columns[0].DataPropertyName = "ZZB";
            //dataGridView1.Columns[0].Visible = false;

            DataGridViewImageColumn btnImageEdit = new DataGridViewImageColumn(false);
            //Image imgEdit = new Bitmap("addplus.bmp");
           // btnImageEdit.Image = imgEdit;
            btnImageEdit.Width = 20;
            btnImageEdit.HeaderText = "选择";
            btnImageEdit.Name = "btnImageEdit";
            this.dataGridView1.Columns.Insert(0, btnImageEdit);

            //DataGridViewImageColumn btnImageDel = new DataGridViewImageColumn(false);
            ////Image imgDel = new Bitmap(Properties.Resources.Delete, new Size(16, 16));
            ////btnImageDel.Image = imgDel;
            //btnImageDel.Width = 50;
            //btnImageDel.HeaderText = "删除";
            //btnImageDel.Name = "btnImageEdit";
            //this.dataGridView1.Columns.Insert(1, btnImageDel);



        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int cIndex = e.ColumnIndex;
            if (cIndex == 0)
            {
                string tt = dataGridView1["井号", e.RowIndex].Value.ToString();
                if (!string.IsNullOrEmpty(tt))
                {
                    FrameDesign.JH = tt.Trim();
                    this.Close();
                }
            }
        }
    }
}
