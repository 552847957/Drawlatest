using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LJJSCAD.Util
{
    class ComboUtil
    {
        /// <summary>
        /// 函数：通过字符串设置combobox的选择项,常规项
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="str"></param>
        public static void SetComboIndex(ComboBox cb, string str)
        {
            for (int i = 0; i < cb.Items.Count; i++)
            {
                string test = cb.Items[i].ToString();
                if (test == str)
                {
                    cb.SelectedIndex = i;
                }
            }
        }
        /// <summary>
        /// 方法:将listbox选中项向上或者向下移动,true为向上,false为向下
        /// </summary>
        /// <param name="temp_lb"></param>
        public static void ListitemsChange(ListBox temp_lb, bool porn)
        {
            if (temp_lb.SelectedItem != null)//判断是否为空
            {
                string ListItem_txt = "";
                if (porn)//通过porn控制是向上移动还是向下移动
                {
                    if (temp_lb.SelectedIndex > 0)
                    {
                        //可参考冒泡算法
                        ListItem_txt = temp_lb.Items[temp_lb.SelectedIndex - 1].ToString();//选中项上一条记录进行保存
                        int indexP = temp_lb.SelectedIndex - 1;//设置当前保存项
                        temp_lb.Items[indexP] = temp_lb.Items[indexP + 1].ToString();//数据交换
                        temp_lb.Items[indexP + 1] = ListItem_txt;//数据项赋值
                        temp_lb.SelectedIndex = indexP;//设置选中项
                    }
                    else
                    {
                        MessageBox.Show("该记录为首记录,不能继续向前", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else//向下移动的代码,和向上移动类似
                {
                    if (temp_lb.SelectedIndex < temp_lb.Items.Count-1)
                    {
                        ListItem_txt = temp_lb.Items[temp_lb.SelectedIndex + 1].ToString();
                        int indexP = temp_lb.SelectedIndex + 1;
                        temp_lb.Items[indexP] = temp_lb.Items[indexP - 1].ToString();
                        temp_lb.Items[indexP - 1] = ListItem_txt;
                        temp_lb.SelectedIndex = indexP;
                    }
                    else
                    {
                        MessageBox.Show("该记录为末记录,不能继续下移", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("不存在选中项", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
