using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LJJSCAD.Util
{
    class CommonControlOper
    {
        /// <summary>
        /// 方法:将所有选中的索引项依次从listbox控件中删除.
        /// </summary>
        /// <param name="SourceListBox"></param>
        public static void DeleteFromList(ListBox SourceListBox)
        {
            for (int i = SourceListBox.SelectedIndices.Count - 1; i > -1; i--)
            {
                SourceListBox.Items.Remove(SourceListBox.SelectedItems[i]);
            }
        }
        public static void FillDrawItemListBox(List<DrawItemName> sourceLst, System.Windows.Forms.ListBox targetLB)
        {
            targetLB.Items.Clear();
            
            for (int i = 0; i < sourceLst.Count; i++)
            {
                DrawItemName tmp = sourceLst[i];
                string tmpDrawItemName = tmp.DrawItemID + ";" + tmp.ItemStyle.ToString() + ";" + tmp.DrawItemShowName;
                targetLB.Items.Add(tmpDrawItemName);
            }
        }
    }
}
