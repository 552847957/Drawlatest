using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.BlackBoard.DrawingHeaderTable
{
    class HeaderTableDataManage
    {
        /// <summary>
        /// 图头表所在表名或者文件名,对于从数据库中获取则为数据库表名，；
        /// </summary>
        public static string DrawingHeaderTbName = "kf_table";
        /// <summary>
        /// 图头表字段标记，在字段前方标记；
        /// </summary>
        public static readonly char DrawingHeaderTbSplitter = '#';
        /// <summary>
        /// 图头表工作数据，key为字段名格式为#Field，value为具体的取值；由各种方式的IHTWorkDataProvider提供；
        /// </summary>
        private static Dictionary<string, string> headerTableData = new Dictionary<string, string>();

        public static Dictionary<string, string> HeaderTableData
        {
            get { return HeaderTableDataManage.headerTableData; }
            set { HeaderTableDataManage.headerTableData = value; }
        }


    }
}
