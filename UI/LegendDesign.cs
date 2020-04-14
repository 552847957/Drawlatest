using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJJSCAD.DrawingDesign.Frame;

namespace LJJSCAD.Util.UI
{
    public partial class LegendDesign : Form
    {
        public LegendDesign()
        {
            InitializeComponent();
        }

        private void LegendDesign_Shown(object sender, EventArgs e)
        {
            this.chk_ifAddLegend.Checked = FrameDesign.IfAddLegend;
         //   ComboUtil.SetComboIndex(this.cb_LegendPos, FrameDesign.LegendPos.ToString());



        }

        private void cb_ifAddLegend_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
