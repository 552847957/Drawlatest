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
   
    public delegate void MainForm_InitDrawData_DELE(string myLineRoadDesignName);
    public partial class SheDingMoBanForm : Form
    {
        public Childform cf;


        public SheDingMoBanForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SheDingMoBanForm_Load(object sender, EventArgs e)
        {
            DataTable mobanTable = new DBHelper.SqlServerDAL("server=127.0.0.1;database=DQLREPORTDB;integrated security=true;").GetTable("use JXBNCAD select LineRoadDesignName from LineRoadDesign");
            //this.cmbMoBan.Items
            foreach (DataRow row in mobanTable.Rows)
            {
                this.cmbMOBAN.Items.Add(row[0].ToString());
            }

            
        }

        public MainForm_InitDrawData_DELE mainform_initdrawdata_dele;
        private void btnOK_Click(object sender, EventArgs e)
        {
            if(this.cmbMOBAN.SelectedIndex <0)
            {
                MessageBox.Show("还没有选中模板");
                return;
            }

            if(this.cf.MdiParent is MainForm)
            {
                MainForm mf = this.cf.MdiParent as MainForm;

                this.mainform_initdrawdata_dele = mf.initdrawdata;
                string myLineRoadDesignName = this.cmbMOBAN.SelectedItem.ToString().Trim();
                this.mainform_initdrawdata_dele(myLineRoadDesignName);
            }

            if (this.cf.MdiParent is SuiZuanForm)
            {
                SuiZuanForm szf = this.cf.MdiParent as SuiZuanForm;

                if(szf.zheng_zai_Sui_Zuan_Zhong == true)
                {
                    MessageBox.Show("正在随钻中，无法更改");
                    return;
                }

                this.mainform_initdrawdata_dele = szf.initdrawdata;
                string myLineRoadDesignName = this.cmbMOBAN.SelectedItem.ToString().Trim();
                this.mainform_initdrawdata_dele(myLineRoadDesignName);
            }

            

          
        }
    }
}
