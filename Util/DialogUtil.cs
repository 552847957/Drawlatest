using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace LJJSCAD.Util
{
    class DialogUtil
    {
        /// <summary>
        /// 获得文件路径，通过OpenDialog控件
        /// </summary>
        /// <param name="FilterName">需要选择的文件类型</param>
        /// <returns>选择的文件的所在路径</returns>
        public static string GetFilePathByOpenDialog(string FilterName)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = FilterName;
            string FileName = "";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FileName = dlg.FileName;
            }
            else
            {
                MessageBox.Show("没有选择文件！");
            }
            FileName = FileName.Trim();
            return FileName;

        }

        /// <summary>
        /// 方法:返回一个字体名称和一个字体高度
        /// </summary>
        /// <param name="txtname"></param>
        /// <param name="txtheigh"></param>
        public static void UpdateTextDesign(TextBox txtname, TextBox txtheigh)
        {
            Font font;

            font = ShowAndGetFont();
            if (font != null)
            {
                txtname.Text = font.Name;
                txtheigh.Text = font.Size.ToString();

            }
        }
        /// <summary>
        /// 私有方法获取Font(支持UpdateTxtSizeSet函数)
        /// </summary>
        /// <returns></returns>
        private static Font ShowAndGetFont()
        {
            Font font = null;
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() != DialogResult.Cancel)
            {
                font = fd.Font;
            }
            return font;
        }
    }
}
