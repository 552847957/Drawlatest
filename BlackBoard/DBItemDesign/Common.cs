using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.BlackBoard.DBItemDesign
{
    class BindDesign
    {
        public string ValueMember { set;get;}
        public string DisplayMember { set; get; }
        public BindDesign(string valueMember, string displayMember)
        {
            ValueMember = "";
             DisplayMember = "";
         
            if (!string.IsNullOrEmpty(valueMember))
                ValueMember = valueMember;       
                
            if (!string.IsNullOrEmpty(displayMember))
                DisplayMember = displayMember;
         
               
        }

    }
    class CommonDeignValue
    {
        public static readonly char FenJiValueSplitter=';';
        public static readonly char HatchSplitter = ';';
        public static readonly string nullValue = "none";
        public static readonly string boolBindIdField = "ID";
        public static readonly string boolBindDisplayField = "Text";

    }
}
