using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DesignEnum;
using EnumManage;

namespace LJJSCAD.DesignEnumManage
{
    class ComboDicOper
    {
        public static void CreateBindSource(DicOper dic, string selectedValue,ComboBox comboBox)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dic.Dic;
          
            comboBox.DataSource = bs;
            comboBox.DisplayMember = "Value";
            comboBox.ValueMember = "Key";
            if(!string.IsNullOrWhiteSpace(selectedValue))
            comboBox.SelectedValue = selectedValue;
        }
        public static string GetDicValue(DicOper dic, string keyContent)
        {
            return dic.GetDicValueByKey(keyContent);

        }
    }
}
