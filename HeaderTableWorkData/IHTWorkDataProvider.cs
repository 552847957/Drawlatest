using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.BlackBoard.DrawingHeaderTable;

namespace LJJSCAD.HeaderTableWorkData
{
    abstract class IHTWorkDataProvider
    {

        protected Dictionary<string,string> htWorkDataDic=new Dictionary<string,string>();
        public void BuildHtWorkData(List<string> allAttriLst)
        {
            InitHtWorkDataDic();//初始化具有字段和取值的图头表工作数据字典，key为字段名,字段全部为小写字母；value为字段取值；
            char bindtxt = HeaderTableDataManage.DrawingHeaderTbSplitter;
            HeaderTableDataManage.HeaderTableData.Clear();
            if (null == allAttriLst)
                return;
            for (int i = 0; i < allAttriLst.Count(); i++)
            {
                string tmp = allAttriLst[i];//获取图头表中属性的tag，为#Field格式；

                if (tmp.IndexOf(bindtxt) >= 0)
                {
                   string fieldname = tmp.Trim();
                   fieldname = fieldname.TrimStart(bindtxt);
                   fieldname = fieldname.ToLower();//全部转换成小写，因为htWorkDataDic中的字段全部为小写字母；
                   if (htWorkDataDic.ContainsKey(fieldname))
                   {
                       string fieldvalue = htWorkDataDic[fieldname];
                       HeaderTableDataManage.HeaderTableData.Add(tmp,fieldvalue);

                   }
                    
                }
 
            }
        }
        /// <summary>
        /// 从数据库或者文本文件中，获得图头表的工作数据，初始化htWorkDataDic字段，要求，key全部为小写字母；
        /// </summary>
        public abstract void InitHtWorkDataDic();

    }
}
