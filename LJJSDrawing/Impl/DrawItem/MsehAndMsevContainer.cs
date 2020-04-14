using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using System.Windows.Forms;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem
{
    public class MsehAndMsevContainer  //js
    {
        public List<LJJSPoint> kxdList;


        public List<LJJSPoint> msevList;

        public List<LJJSPoint> msehList;

        public List<LJJSPoint> JinGeiLiangList;

        public List<LJJSPoint> WXCSJiaoHuiList;

        public  List<LJJSPoint> wxcsList;

        public LJJSPoint lrptstart;
        public void Initiate()
        {
            this.kxdList = new List<LJJSPoint>();
            this.lrptstart = null;
            this.msevList = null;
            this.msehList = null;
            this.JinGeiLiangList = null;
            this.WXCSJiaoHuiList = null;
            this.wxcsList = null;
        }

        public  void Part_Initiate()
        {
            this.kxdList = new List<LJJSPoint>();
            //MsehAndMsevContainer.lrptstart = null;
            //MsehAndMsevContainer.msevList = null;
           // MsehAndMsevContainer.msehList = null;
           // MsehAndMsevContainer.JinGeiLiangList = null;
           // MsehAndMsevContainer.WXCSJiaoHuiList = null;
            this.wxcsList = null;
        }
        public static List<LJJSPoint> same_YValue_PointsList(List<LJJSPoint> list) //返回List中所有具有相同yValue的点所构成的list
        {
            Dictionary<double, int> dict = new Dictionary<double, int>();
            foreach (LJJSPoint item in list)
            {

                if (dict.ContainsKey(item.YValue) == false)
                {
                    dict[item.YValue] = 1;
                }
                else
                {
                    dict[item.YValue]++;
                }
            }

            //字典中key是list中点的yValue，value是yValue出现的次数，也就是有几个具有相同yValue的点
            List<LJJSPoint> retList = new List<LJJSPoint>();
            foreach (double item in dict.Keys)
            {
                if (dict[item] > 1)
                {
                    //MessageBox.Show(string.Format("有{0}个点具有此yValue : {1}  ", dict[item], item));
                      //有重复的yValue

                    foreach(LJJSPoint point in list)
                    {
                        if (point.YValue == item)
                        {

                            retList.Add(point);
                        }
                    }
                }
            }
            return retList;
        }
        public static bool check_Multi_Same_YValue(List<LJJSPoint> list)//检测一个list中是否有多个相同的yValue
        {
            Dictionary<double, int> dict = new Dictionary<double, int>();
            foreach (LJJSPoint item in list)
            {

                if (dict.ContainsKey(item.YValue) == false)
                {
                    dict[item.YValue] = 1;
                }
                else
                {
                    dict[item.YValue]++;
                }
            }
            //
            foreach (double item in dict.Keys)
            {
                if (dict[item] > 1)
                {
                    //MessageBox.Show(string.Format("有{0}个点具有此yValue : {1}  ", dict[item], item));
                    return true;   //有重复的yValue 则返回true
                }
            }
            return false ;
        }

        public static bool checkSameYValue_orNot(List<LJJSPoint> list1, List<LJJSPoint> list2)
        {
            
            if (list1.Count != list2.Count)
            {
                return false;
            }
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].YValue != list2[i].YValue)
                {

                   // MessageBox.Show("检测到yValue不相同");
                    //MessageBox.Show(string.Format("进给量坐标:({0},{1})", list1[i].XValue,list1[i].YValue));
                   // MessageBox.Show(string.Format("物性指数坐标:({0},{1})", list2[i].XValue,list2[i].YValue));
                    return false;   //yvalue不相同，返回false；
                }
            }
            return true;
        }

        public static List<LJJSPoint> findPointList_ByYValue(double yValue, List<LJJSPoint> list)
        {
            List<LJJSPoint> retList = new List<LJJSPoint>();
            foreach (LJJSPoint item in list)
            {
                if (item.YValue == yValue)
                {
                    retList.Add(item);
                }
            }

            if (retList.Count > 1)
            {
               // MessageBox.Show(string.Format("多个点有相同的YValue:{0}", yValue.ToString()));
            }
            return retList;
        }

        public static bool removeDuoYuPoints(List<LJJSPoint> mainList, List<LJJSPoint> duoyuPointList)
        {
            foreach (LJJSPoint item in duoyuPointList)
            {
                bool res = mainList.Remove(item);
                if (res == false)
                {
                    return false;
                }
            }
            return true;
        }

        public static List<LJJSPoint> findLostPoints(List<LJJSPoint> list1, List<LJJSPoint> list2)
        {
            
            List<LJJSPoint> retList = new List<LJJSPoint>();

            if (list1.Count == list2.Count)
            {
                return null;
            }
            if (list1.Count > list2.Count)
            {
                //MessageBox.Show(string.Format("进给量List的点更多,为{0}个",list1.Count));
                for (int i = 0; i < list1.Count; i++)
                {

                    List<LJJSPoint> list = MsehAndMsevContainer.findPointList_ByYValue(list1[i].YValue, list2);
                    
                    if (list.Count == 0)
                    {
                        //MessageBox.Show("找到多余的点，" + list.Count.ToString());
                        retList.Add(list1[i]);
                    }
                }
            }

            else

            {
                //MessageBox.Show(string.Format("物性指数交汇List的点更多,为{0}个", list2.Count));
                for (int i = 0; i < list2.Count; i++)
                {

                    List<LJJSPoint> list = MsehAndMsevContainer.findPointList_ByYValue(list2[i].YValue, list1);
                    
                    if (list.Count == 0)
                    {
                        retList.Add(list2[i]);
                    }
                }
            
            }
            return retList;
        }
    }
}
