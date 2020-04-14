using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LJJSCAD.Util
{
    class ListBoxUtil
    {
        /// <summary>
        /// 将一个listbox选择项传到另外一个listbox(不考虑其他因素)
        /// </summary>
        /// <param name="temp_lbfrom"></param>
        /// <param name="temp_lbtarget"></param>
        public static List<string> FromListboxToListBox(ListBox temp_lbfrom, ListBox temp_lbtarget)
        {
            List<string> movelst = new List<string>();
            if (temp_lbfrom.SelectedItems.Count != 0)
            {
                string[] ItemBoots = new string[temp_lbfrom.SelectedItems.Count];//设置承载所有选中项的数组
                for (int i = 0; i < ItemBoots.Length; i++)
                {
                    ItemBoots[i] = temp_lbfrom.SelectedItems[i].ToString().Trim();
                }
                for (int i = 0; i < ItemBoots.Length; i++)
                {
                    temp_lbtarget.Items.Add(ItemBoots[i]);//循环添加源listbox的item
                    temp_lbfrom.Items.Remove(ItemBoots[i]);//移除源listbox内的item
                    movelst.Add(ItemBoots[i]);
                }
            }
            return movelst;


        }
    }
}
