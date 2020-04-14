using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.Util;

namespace LJJSCAD.UI
{
    public partial class FrameDesginForm : Form
    {

        public FrameDesginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //1,图幅设计；
            FrameDesign.JdSpace = StrUtil.StrToDouble(this.tb_JDSpace.Text, "缺少分段间距数据", "分段间距数据为非数值型"); //分段间距
            FrameDesign.PictureFrameLineWidth = StrUtil.StrToDouble(this.tb_PictureFrameLineWidth.Text, "缺少外框线宽", "外框线宽为非数值型");//外框线宽
            FrameDesign.LineRoadTitleBarHeigh = StrUtil.StrToDouble(this.tb_LineRoadTitleBarHeigh.Text, "缺少线道标题栏高度", "线道标题栏高度为非数值型");//线道标题栏高度
            //2,比例尺
            FrameDesign.XCoordinate = StrUtil.StrToDouble(this.tb_xBiLiChi.Text, 1, "比例尺X值为非数值型");
            FrameDesign.YCoordinate = StrUtil.StrToDouble(this.tb_yBiLiChi.Text, 100, "比例尺Y值为非数值型");

            FrameDesign.CorTxtFont = this.tb_BLCTxt_Font.Text;
            FrameDesign.CorTxtHeit = StrUtil.StrToDouble(this.tb_BLCTxt_Height.Text, 4, "比例尺为非数值型");
            //3,绘图项标题文字
            FrameDesign.PictureItemFont = this.tb_DrawItemTitle_Font.Text;
            FrameDesign.PictureItemTxtHeight = StrUtil.StrToDouble(this.tb_DrawItemTitle_Height.Text, 8, "绘图项标题为非数值型");
            //4,刻度尺文字
            FrameDesign.ScaleLabelTxtFont = this.tb_KDCTxt_Font.Text;
            FrameDesign.ScaleLabelTxtHeight = this.tb_KDCTxt_Height.Text;
            //5,图头文字设计
            FrameDesign.HeaderContent = this.tb_HeaderContent.Text;
            FrameDesign.PictureHeaderTXTStyle = this.tb_Header_Font.Text;
            FrameDesign.HeadBigTxtHeight = StrUtil.StrToDouble(this.tb_Header_Height.Text, 18, "图头大字为非数值型");
            this.DialogResult = DialogResult.OK;
        }

        private void FrameDesginForm_Shown(object sender, EventArgs e)
        {
            //1,图幅设计；
            this.tb_JDSpace.Text = FrameDesign.JdSpace.ToString();
            this.tb_LineRoadTitleBarHeigh.Text = FrameDesign.LineRoadTitleBarHeigh.ToString();
            this.tb_PictureFrameLineWidth.Text = FrameDesign.PictureFrameLineWidth.ToString();
            //2,比例尺
            this.tb_xBiLiChi.Text = FrameDesign.XCoordinate.ToString();
            this.tb_yBiLiChi.Text = FrameDesign.YCoordinate.ToString();
            this.tb_BLCTxt_Font.Text = FrameDesign.CorTxtFont;
            this.tb_BLCTxt_Height.Text = FrameDesign.CorTxtHeit.ToString();         
            if(!string.IsNullOrEmpty(FrameDesign.CorTxtColor))
            this.btn_BLCTxtColor.ForeColor=Color.FromArgb(StrUtil.StrToInt(FrameDesign.CorTxtColor,Color.Black.ToArgb(),"比例尺字体颜色为非数值型"));
           
            //3,绘图项标题文字
            this.tb_DrawItemTitle_Font.Text = FrameDesign.PictureItemFont;
            this.tb_DrawItemTitle_Height.Text = FrameDesign.PictureItemTxtHeight.ToString();
            
            //4,刻度尺文字
            this.tb_KDCTxt_Font.Text = FrameDesign.ScaleLabelTxtFont;
            this.tb_KDCTxt_Height.Text = FrameDesign.ScaleLabelTxtHeight.ToString();

            //5,图头文字设计
            this.tb_HeaderContent.Text = FrameDesign.HeaderContent;
            this.tb_Header_Font.Text = FrameDesign.PictureHeaderTXTStyle;
            this.tb_Header_Height.Text = FrameDesign.HeadBigTxtHeight.ToString();
          
            if (!string.IsNullOrEmpty(FrameDesign.HeaderTxtColor))
                this.btn_Header_Color.ForeColor = Color.FromArgb(StrUtil.StrToInt(FrameDesign.HeaderTxtColor, Color.Black.ToArgb(), "图头大字字体颜色为非数值型"));
           

        }

        private void btn_BLCTxtColor_Click(object sender, EventArgs e)
        {
          FrameDesign.CorTxtColor=ColorUtil.ChangeColor((Button)sender);
        }

        private void btn_Header_Color_Click(object sender, EventArgs e)
        {
          FrameDesign.HeaderTxtColor=ColorUtil.ChangeColor((Button)sender);
        }

        private void btn_BLCTxtFontStyle_Click(object sender, EventArgs e)
        {
            DialogUtil.UpdateTextDesign(this.tb_BLCTxt_Font, this.tb_BLCTxt_Height);
        }

        private void btn_DrawItemTitleTxtStyle_Click(object sender, EventArgs e)
        {
            DialogUtil.UpdateTextDesign(this.tb_DrawItemTitle_Font, this.tb_DrawItemTitle_Height);
        }

        private void btn_KDCTxtStyle_Click(object sender, EventArgs e)
        {
            DialogUtil.UpdateTextDesign(this.tb_KDCTxt_Font, this.tb_KDCTxt_Height);
        }

        private void btn_HeaderTxtStyle_Click(object sender, EventArgs e)
        {
            DialogUtil.UpdateTextDesign(this.tb_Header_Font, this.tb_Header_Height);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
