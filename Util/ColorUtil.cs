using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LJJSCAD.Util
{
    class ColorUtil
    {
        /// <summary>
        /// 方法:通过显示的ColorDialog返回一个颜色的RGB值
        /// </summary>
        /// <param name="CDialog"></param>
        /// <returns></returns>
        public static string ChangeColor(Button Btn)
        {
            string temp_colorRGB = "";
            ColorDialog CDialog = new ColorDialog();
            CDialog.ShowDialog();
            if (CDialog.Color != null)
            {
                temp_colorRGB = CDialog.Color.ToArgb().ToString();//获取颜色Rgb的值
                Btn.ForeColor = CDialog.Color;//令按钮颜色变换
            //    Btn.BackColor = CDialog.Color;
            }
            return temp_colorRGB;
        }
    }
}
