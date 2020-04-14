using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using LJJSCAD.CommonData;
using LJJSCAD.Model;
using System.Data.Common;
using LJJSCAD.DAL;
using System.Reflection;

namespace LJJSCAD.BlackBoard.LJJSDesignData.Impl
{
    class KeDuChiDesignManage
    {
        private static Hashtable _ht_Keduchi = new Hashtable();
        /// <summary>
        /// 定义一个哈希表用来存储所有刻度尺表内容,注明:刻度尺仅仅为曲线项中所有
        /// </summary>
        public static Hashtable Ht_Keduchi
        {
            get { return _ht_Keduchi; }
            set { _ht_Keduchi = value; }
        }
        private static List<string> _lineitemKDC = new List<string>();
        /// <summary>
        /// 定义一个List链表用来存储所有包含刻度尺设计的曲线项的名字；
        /// </summary>
        public static List<string> KDCLineItemName
        {
            get { return _lineitemKDC; }
            set { _lineitemKDC = value; }
        }
        /// <summary>
        /// 方法:将刻度尺的数据导入到哈希表Ht_Keduchi中
        /// </summary>
        /// <param name="condition"></param>
        public static void SetDrawingKeduByDB(string condition)
        {
            _ht_Keduchi.Clear();
            _lineitemKDC.Clear();

            Object obj = typeof(string);//获取关于string类型的抽象类
            Type type;//反射接口
            string sqlGetKedu = "select * from " + MyTableManage.KeDuChiTbName + " " + condition;
            DbDataReader kedudr = null;
            DrawingKedu dk;
            KeDuChiDAL kdcDal=new KeDuChiDAL(MyTableManage.KeDuChiTbName);
            kedudr = kdcDal.GetKeDuChiDesignByDB(condition);
          //  kedudr = AccessCSHelper.ExecuteReader(MyTableManage.GetCJJSDrawingDBConnStr(), sqlGetKedu);//取出数据集加载到datareader中
            if (kedudr.HasRows)
            {
                while (kedudr.Read())
                {
                    string kdrawitem = kedudr["KDrawItem"].ToString().Trim();
                    if (!_lineitemKDC.Contains(kdrawitem))
                        _lineitemKDC.Add(kdrawitem);

                    dk = new DrawingKedu();
                    type = dk.GetType();//获取对象(dt)的类型
                    foreach (PropertyInfo p in type.GetProperties())//赋值过程
                    {
                        string proName = p.Name.Trim();//PropertyInfo的名字
                        string proValue = kedudr[proName].ToString().Trim();//将p进行赋值
                        type.GetProperty(proName).SetValue(dk, proValue, null);//关键,设置非静态对象的某个属性的值.
                    }
                    try
                    {
                        _ht_Keduchi.Add(dk.KName, dk);//将表中的主键作为key值，将对象赋予到哈希表中
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            kedudr.Close();
        }

        /// <summary>
        /// 根据绘图曲线项的名字获得曲线项包含的刻度尺设计链表；
        /// </summary>
        /// <param name="lineName"></param>
        /// <returns></returns>

        public static List<DrawingKedu> GetDrawKDList(string lineName)
        {
            DrawingKedu DKD = new DrawingKedu();
            List<DrawingKedu> relist = new List<DrawingKedu>();

            foreach (DictionaryEntry ent in _ht_Keduchi)//循环分析哈希表中每一个值
            {
                DKD = (DrawingKedu)ent.Value;
                if (DKD.KDrawItem == lineName)//比较,如果相同则sum自加1
                {
                    relist.Add(DKD);
                }
            }
            return relist;
        }
    }
}
