using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using LJJSCAD.Model.Drawing;
using System.Windows.Forms;
using VectorDraw.Professional.vdObjects;
using LJJSCAD.CommonData;
using LJJSCAD.LJJSDrawing.Impl.DrawItem.SymbolItem;
using System.Drawing;
using LJJSCAD.Drawing.Hatch;
using System.Collections;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem
{

    
    public class AddLineHatchManager   ///js
    {

        public static void zuanshi_wrong_point_exterminate(List<LJJSPoint> drawptcol)
        {
            List<LJJSPoint> points_to_be_removed_list = new List<LJJSPoint>();
            foreach (LJJSPoint point in drawptcol)
            {
                if (point.YValue >= 0)
                {
                    points_to_be_removed_list.Add(point);
                   // drawptcol.Remove(point);
                }
            }
            foreach(LJJSPoint point in points_to_be_removed_list)
            {
                drawptcol.Remove(point);
            }
        }


        
        public static void remove_points_which_are_above_start_point(LJJSPoint startPoint, List<LJJSPoint> linelist)
        {
            //去除出现在起点之前的点
            List<LJJSPoint> wrong_point_list = new List<LJJSPoint>();
            foreach(LJJSPoint point in linelist)
            {
                if(point.YValue > startPoint.YValue)
                {
                    wrong_point_list.Add(point);
                }
            }

            foreach(LJJSPoint point in wrong_point_list)
            {
                linelist.Remove(point);
            }
        }

        


        public static bool is_all_zero_x_value_list(LJJSPoint original_point,List<LJJSPoint> linelist)
        {
            //如果这条线上所有点的x值都是 原点的x值 则返回true，否则返回false
            
            foreach(LJJSPoint point in linelist)
            {
                if(point.XValue != original_point.XValue)
                {
                    return false;
                }
            }
            return true ;
        }


        public static Color get_Color_By_CurveItemShowName(string CurveItemShowName)
        {
            if (CurveItemShowName.Equals("进给量")||CurveItemShowName.Equals("TG(%)") || CurveItemShowName.Equals("物性指数") || CurveItemShowName.Equals("钻时") ||  CurveItemShowName.Equals("孔隙度") ||CurveItemShowName.Equals("可钻性") )
            {
                //red
                return Color.FromArgb(255, 0, 0);
            }
            else if (CurveItemShowName.Equals("C1(%)") || CurveItemShowName.Equals("钻压"))
            {
                //purple
                return Color.FromArgb(128, 0, 128);
            }
            else if (CurveItemShowName.Equals("C2(%)") || CurveItemShowName.Equals("转速") || CurveItemShowName.Equals("硬度"))
            {
                //blue
                return Color.FromArgb(0, 0, 255);
            }
            else if (CurveItemShowName.Equals("垂向功") || CurveItemShowName.Equals("C3(%)") || CurveItemShowName.Equals("扭矩") ||  CurveItemShowName.Equals("脆性指数") )
            {
                //green
                return Color.FromArgb(0, 255, 0);
            }

            else if(CurveItemShowName.Equals("切向功") )
            {
                return Color.Brown;
            }
            else if( CurveItemShowName.Equals("物性指数交汇"))
            {
                return Color.Yellow;
            }
            else
            {
                //black
                return Color.FromArgb(0, 0, 0);
            }
        }

        public static Color get_Color_By_CurveItemShowName(LineItemStruct lineItemStruct)
        {
            //


            if (lineItemStruct.CurveItemShowName.Equals("进给量") || lineItemStruct.CurveItemShowName.Equals("TG(%)") || lineItemStruct.CurveItemShowName.Equals("物性指数") || lineItemStruct.CurveItemShowName.Equals("钻时") || lineItemStruct.CurveItemShowName.Equals("孔隙度") || lineItemStruct.CurveItemShowName.Equals("可钻性"))
            {
                //red
                return Color.FromArgb(255, 0, 0);
            }
            else if (lineItemStruct.CurveItemShowName.Equals("C1(%)") || lineItemStruct.CurveItemShowName.Equals("钻压"))
            {
                //purple
                return Color.FromArgb(128, 0, 128);
            }
            else if (lineItemStruct.CurveItemShowName.Equals("C2(%)") || lineItemStruct.CurveItemShowName.Equals("转速") || lineItemStruct.CurveItemShowName.Equals("硬度"))
            {
                //blue
                return Color.FromArgb(0, 0, 255);
            }
            else if (lineItemStruct.CurveItemShowName.Equals("垂向功") || lineItemStruct.CurveItemShowName.Equals("C3(%)") || lineItemStruct.CurveItemShowName.Equals("扭矩") || lineItemStruct.CurveItemShowName.Equals("脆性指数"))
            {
                //green
                return Color.FromArgb(0, 255, 0);
            }

            else if (lineItemStruct.CurveItemShowName.Equals("切向功"))
            {
                return Color.Brown;
            }
            else if (lineItemStruct.CurveItemShowName.Equals("物性指数交汇"))
            {
                return Color.Yellow;
            }
            else
            {
                //black
                return Color.FromArgb(0, 0, 0);
            }
        }

        public static void my_list_LJJSPOint_sort(List<LJJSPoint> list)
        {
            LJJSPoint[] arr = list.ToArray();
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - 1 - i; j++)
                {
                    if (arr[j].YValue > arr[j + 1].YValue)
                    {
                        LJJSPoint temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
            list = arr.ToList();
        }


        public static void Remove_Redundant_Point(LJJSPoint startPoint, List<LJJSPoint> list)
        {

            /**
            Comparison<LJJSPoint> Sort_Dele = (a, b) => 
            {

                if (a.YValue > b.YValue)
                {
                    return 1;
                }
                else if (a.YValue == b.YValue)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            };**/
            List<LJJSPoint> redund_list = MsehAndMsevContainer.same_YValue_PointsList(list);
            //redund_list.Sort(Sort_Dele);
            AddLineHatchManager.my_list_LJJSPOint_sort(redund_list);
            
            for (int ss = 0; ss < redund_list.Count - 1; ss++)
            {
                if (redund_list[ss].YValue == redund_list[ss + 1].YValue)
                {
                    list.Remove(redund_list[ss]);
                }
            }

            //list.Sort(Sort_Dele);
            AddLineHatchManager.my_list_LJJSPOint_sort(list);
        }

        public static void wrong_Point_exterminate(LJJSPoint startPoint, List<LJJSPoint> list)
        {
            List<LJJSPoint> Remove_wrong_point_list = new List<LJJSPoint>();

            



            for (int ss = 0; ss < list.Count; ss++)
            {
                if (list[ss].YValue > 0 || list[ss].XValue < startPoint.XValue || list[ss].XValue >= startPoint.XValue + 50)
                {
                    
                    //MessageBox.Show(string.Format("error,坐标:({0},{1})", drawptcol[ss].XValue.ToString(), drawptcol[ss].YValue.ToString()));
                    
                    //GAY
                    

                    if(list[ss].YValue > 0)
                    {
                        Remove_wrong_point_list.Add(list[ss]);

                        
                        continue;
                    }
                    if(list[ss].XValue < startPoint.XValue || list[ss].XValue >= startPoint.XValue + 50)
                    {
                        list[ss].XValue = startPoint.XValue;
                    }

                }



            }
            

            //GAY
            foreach (LJJSPoint sss in Remove_wrong_point_list)
            {
                list.Remove(sss);
            }


            AddLineHatchManager.Remove_Redundant_Point(startPoint, list); //去除具有同yvalue的点
             
        }


        /// <summary>
        /// Original is Green, else is red
        /// </summary>
        /// <param name="CollectionName"></param>
        /// <param name="color"></param>
        /// <param name="colorIndex"></param>
        public static  void selectColorByXValue_WithoutTransition(string CollectionName, out CurveColorEnum color, ref int colorIndex)
        {
            if (CollectionName.Equals("Original"))
            {
                color = CurveColorEnum.BlackToRed;
                colorIndex = Color.FromArgb(0, 255, 0).ToArgb();
                return;
            }
            else
            {
                color = CurveColorEnum.BlackToRed;
                colorIndex = Color.FromArgb(255, 73, 28).ToArgb();
                return;
            }
        }
        public static void selectColorByXValue(string a,LJJSPoint lrptstart, List<LJJSPoint> xValuesList, double xValue,  ref int colorIndex)
        {
            Color leftColor = Color.Yellow;
            Color rightColor = Color.Green;

            double MaxX = lrptstart.XValue + 50;
            double MinX = lrptstart.XValue;
            double length = MaxX - MinX;

            double proportion = (xValue - lrptstart.XValue) / length;
            
            int r = int.Parse(Math.Round((leftColor.R + (rightColor.R - leftColor.R) * proportion)).ToString());
            int g = int.Parse(Math.Round((leftColor.G + (rightColor.G - leftColor.G) * proportion)).ToString());
            int b = int.Parse(Math.Round((leftColor.B + (rightColor.B - leftColor.B) * proportion)).ToString());

            colorIndex = Color.FromArgb(r, g, b).ToArgb();

        }

        public static void selectColorByXValue(LJJSPoint lrptstart, List<LJJSPoint> xValuesList, double xValue,  ref int colorIndex)
        {
            //double MaxX = AddLineHatchManager.findMaxX(xValuesList);
            //double MinX = AddLineHatchManager.findMinX(xValuesList);
            #region
            /**
            double MaxX = lrptstart.XValue+50;
            double MinX = lrptstart.XValue;
            double length = MaxX-MinX;


            Color sourceColor = Color.Blue;
            Color destColor = Color.Red;

            int redSpace = destColor.R - sourceColor.R;
            int greenSpace = destColor.G - sourceColor.G;
            int blueSpace = destColor.B - sourceColor.B;


            double proportion = (xValue-lrptstart.XValue) / length;

            int r = int.Parse(Math.Round(sourceColor.R + proportion * redSpace).ToString());
            int g = int.Parse(Math.Round(sourceColor.G + proportion * greenSpace).ToString());
            int b = int.Parse(Math.Round(sourceColor.B + proportion * blueSpace).ToString());

            


            colorIndex = Color.FromArgb(r, g, b).ToArgb(); **/
            #endregion
            #region
            

            double MaxX = lrptstart.XValue+50;
            double MinX = lrptstart.XValue;
            double length = MaxX-MinX;

            double step = (MaxX - MinX) / 3;

            double ZoneOne = MinX + step;
            double ZoneTwo = MinX + 2 * step;

            CurveColorEnum curvecolor = CurveColorEnum.BlackToRed;

            if (xValue >= MinX && xValue < ZoneOne)   //白 -》 黄
            {
                curvecolor = CurveColorEnum.WhiteToYellow;
                AddLineHatchManager.selectLittleColorByXValue(xValue, ref curvecolor, MinX, ZoneOne, ref colorIndex);
            }
            else if (xValue >= ZoneOne && xValue < ZoneTwo)  //黄 -》 红
            {
                curvecolor = CurveColorEnum.YellowToRed;
                AddLineHatchManager.selectLittleColorByXValue(xValue, ref curvecolor, ZoneOne, ZoneTwo, ref colorIndex);
            }

            else if (xValue >= ZoneTwo && xValue <= MaxX)  //红 -》黑
            {
                curvecolor = CurveColorEnum.RedToBlack;
                AddLineHatchManager.selectLittleColorByXValue(xValue, ref curvecolor, ZoneTwo, MaxX, ref colorIndex);
            }
                
            else
            {
              //  curvecolor = CurveColorEnum.RedToBlack;
                //return;
              // AddLineHatchManager.selectLittleColorByXValue(xValue, ref curvecolor, ZoneTwo, MaxX, ref colorIndex,true);
              
               throw new Exception("???wxcs hatch");
                //return;
            }
            
            #endregion
        }
        public static  void selectLittleColorByXValue(double xValue, ref CurveColorEnum color, double xValueLeft, double xValueRight, ref int retColorIndex,bool special)
        {
             retColorIndex = Color.FromArgb(255, 255, 255).ToArgb();
                return;
        }

        public static  void selectLittleColorByXValue(double xValue, ref CurveColorEnum color, double xValueLeft, double xValueRight, ref int retColorIndex)
        {
           
            

            double interval = xValueRight - xValueLeft;

            double xValueLength = xValue - xValueLeft;

            double proportion = xValueLength/interval;  //0 -> 0.5 -> 1
            //???
            switch (color)
            {
                case CurveColorEnum.WhiteToYellow:
                    int r = int.Parse(Math.Round((255 * proportion)).ToString()); // r 代表xValueLength 占 interval 的比例

                    retColorIndex = Color.FromArgb(255, 255, 255-r).ToArgb();
                    break;
                case CurveColorEnum.YellowToRed:
                    int g = int.Parse(Math.Round((255 * proportion)).ToString());

                    retColorIndex = Color.FromArgb(255, 255-g, 0).ToArgb();

                    break;
                case CurveColorEnum.RedToBlack:
                    int b = int.Parse(Math.Round((255 * proportion)).ToString());

                    retColorIndex = Color.FromArgb(255 - b, 0, 0).ToArgb();
                    //MessageBox.Show(string.Format("case Green :(0,{0},{1})", descG.ToString(), ascB.ToString()));
                    break;
            }

        }


        private static double findMaxX(List<LJJSPoint> list)
        {
            double maxX = -1;
            for (int i = 0; i < list.Count - 1; i++)
            {
                double x1 = list[i].XValue;
                double x2 = list[i + 1].XValue;
                double avrx = (x1 + x2) / 2;
                if (avrx > maxX)
                {
                    maxX = avrx;
                }
            }
            return maxX;
        }
        private static double findMinX(List<LJJSPoint> list)
        {
            double minX = 999;
            for (int i = 0; i < list.Count; i++)
            {
                double nowx = list[i].XValue;
                if (nowx < minX)
                {
                    minX = nowx;
                }
            }
            return minX;
        }



        public static void Gei_Qu_Xian_Tu_Se(ArrayList al, JDStruc jdstruc, Hashtable drawht, List<LJJSPoint> line, LJJSPoint lrptstart) //给曲线部分涂色, lrptstart 是线道原点的坐标
        {

            //    vdDocument activeDOcu = DrawCommonData.activeDocument;
            for (int i = 0; i < line.Count - 1; i++)
            {
                double y1_value = (double)line[i].YValue;
                double x1_value = (double)line[i].XValue;

                double y2_value = (double)line[i + 1].YValue;
                double x2_value = (double)line[i + 1].XValue;
                /**double y1_value = (double)al[i];
                double x1_value = (double)drawht[y1_value];

                double y2_value = (double)al[i+1];
                double x2_value = (double)drawht[y2_value];
                if (this.drawptcol.Count == 1)
                {
                    y1_value += this.drawptcol[0].YValue;
                    x1_value += this.drawptcol[0].XValue;
                    y2_value += this.drawptcol[0].YValue;
                    x2_value += this.drawptcol[0].XValue;
                }
                else
                {
                    MessageBox.Show("error,this.drawptcol.Count is " + this.drawptcol.Count.ToString());
                    return;
                }**/
                //y2的值一定大于y1
                //TO BE DONE
                /**  涂左边
                LJJSPoint Oy1 = new LJJSPoint(lrptstart.XValue,y1_value);
                LJJSPoint x1y1 = new LJJSPoint(x1_value,y1_value);

                LJJSPoint Oy2 = new LJJSPoint(lrptstart.XValue,y2_value);
                LJJSPoint x2y2 = new LJJSPoint(x2_value,y2_value);
                //
                **/
                 
                /** 涂右边
                LJJSPoint Oy1 = new LJJSPoint(x1_value, y1_value);
                LJJSPoint x1y1 = new LJJSPoint(lrptstart.XValue + 50, y1_value);

                LJJSPoint Oy2 = new LJJSPoint(x2_value, y2_value);
                LJJSPoint x2y2 = new LJJSPoint(lrptstart.XValue + 50, y2_value);
                **/
                LJJSPoint Oy1 = new LJJSPoint(lrptstart.XValue, y1_value);
                LJJSPoint x1y1 = new LJJSPoint(lrptstart.XValue+50, y1_value);

                LJJSPoint Oy2 = new LJJSPoint(lrptstart.XValue, y2_value);
                LJJSPoint x2y2 = new LJJSPoint(lrptstart.XValue+50, y2_value);

                List<LJJSPoint> hatchPtLst = new List<LJJSPoint>() { Oy2, x2y2, x1y1, Oy1 };
                CurveColorEnum colorSelect;
                int colorIndex = Color.Red.ToArgb();  //颜色的argb值
               // AddLineHatchManager.selectColorByXValue(lrptstart,line, (x1_value + x2_value) / 2,  ref colorIndex);  //选择颜色
               
                
                try
                {
                    AddLineHatchManager.selectColorByXValue(lrptstart, line, x1_value, ref colorIndex);  //选择颜色
                    AreaHatch.AddStandardAreaHatch(DrawCommonData.activeDocument, hatchPtLst, colorIndex, colorIndex);
                }
                catch
                {
                    return;
                }


                
            }

        }




        //每次给两条线两次相交的交点之间涂色 , 两个交点之间line1在右边则为红色 line2在右边则为绿色
        public static void Gei_Qu_Xian_De_Jiao_Dian_Nei_Bu_Tu_Se_Quicker(Color line1_color, Color line2_color,List<LJJSPoint> line1, List<LJJSPoint> line2, LineItemStruct lineItemStruct,int index) //给曲线交点内部涂色
        {   
            if (lineItemStruct.LiSubClass.Equals("wxcs2") || lineItemStruct.LiSubClass.Equals("jgl"))
            {        //物性指数交汇和进给量的数据有问题，其中一个在y值相同时有两个x值，另外一个比另一个多4个点 需要进行处理
                if (line1.Count != line2.Count)
                {
                    List<LJJSPoint> list = MsehAndMsevContainer.findLostPoints(line1, line2);//寻找多出的点
                    if (DrawPointContainer.list[index].JinGeiLiangList != null)
                    {
                        bool res1 = MsehAndMsevContainer.check_Multi_Same_YValue(line1);
                        bool res2 = MsehAndMsevContainer.check_Multi_Same_YValue(line2);
                        //若为true，则代表具有此list中有两个点的y值相同，x值不同，需要去掉


                        List<LJJSPoint> same_Y_list;
                        if (res1 == true)
                        {
                            same_Y_list = MsehAndMsevContainer.same_YValue_PointsList(line1);
                            if (same_Y_list.Count > 0)
                            {
                                for (int i = 0; i < same_Y_list.Count - 1; i++)
                                {
                                    line1.Remove(same_Y_list[i]);
                                }
                            }
                        }

                        if (res2 == true)
                        {
                            same_Y_list = MsehAndMsevContainer.same_YValue_PointsList(line2);
                            if (same_Y_list.Count > 0)
                            {
                                for (int i = 0; i < same_Y_list.Count - 1; i++)
                                {
                                    line2.Remove(same_Y_list[i]);
                                }
                            }
                        }

                        int jingeiliang1 = line1.Count;
                        int wxcs1 = line2.Count;

                        foreach (LJJSPoint point in list)
                        {
                            line2.Remove(point);   //去掉多出的点
                        }


                        bool rrr = MsehAndMsevContainer.checkSameYValue_orNot(line1, line2); //检测两个list中所有的点的y值能不能对应上
                        if (rrr == false) //对应不上，则直接返回
                        {

                            MessageBox.Show("两个曲线上的点的个数不同，错误");
                            MessageBox.Show(line1.Count.ToString());
                            MessageBox.Show(line2.Count.ToString());
                            return;
                        }

                    }

                    //StringBuilder sb = new StringBuilder();
                    /**if (list != null)
                    {
                        foreach (LJJSPoint item in list)
                        {
                            sb.Append(string.Format("多出的点坐标：({0},{1})", item.XValue.ToString(), item.YValue.ToString()));
                            sb.Append("\r\n");
                        }
                    }
                    MessageBox.Show(sb.ToString());**/

                }
            }



            for (int i = 0; i < line1.Count; i++)
            {
                if (double.IsNaN(line1[i].XValue) == true || double.IsInfinity(line1[i].XValue) == true)
                {
                    MessageBox.Show(string.Format("值是NaN，第{0}个", i));
                }
                if (double.IsNaN(line2[i].XValue) == true || double.IsInfinity(line2[i].XValue) == true)
                {
                    MessageBox.Show(string.Format("值是NaN，第{0}个", i));
                }
                if (line1[i].YValue != line2[i].YValue)
                {
                    MessageBox.Show("两个曲线上的点的YValue不同，错误");
                    return;
                }

            }
            //MessageBox.Show("正确，可以涂色");

            vdDocument activeDOcu = DrawCommonData.activeDocument;

            //对两条线，每次在找到两条线的交点 后涂色
            int j = 0;
            LJJSPoint last_jiaoDian = null;
            LJJSPoint curr_jiaoDian = null;
            List<LJJSPoint> part_left_list = new List<LJJSPoint>();
            List<LJJSPoint> part_right_list = new List<LJJSPoint>();
            bool is_line1_bigger = false;
            bool curr_jiaodian_found = false;
            while(true)
            {
                part_left_list.Clear();
                part_right_list.Clear();

                //LJJSPoint jiaoDianPoint = null;
                if(j == line1.Count)
                {
                    break;
                }
                is_line1_bigger = false;
                curr_jiaodian_found = false;
                LJJSPoint line1_point = null;
                LJJSPoint line2_point = null;
                #region  

                //寻找两个曲线焦点之间的封闭区域
                for (; j < line1.Count; j++)
                {
                    line1_point = line1[j];
                    line2_point = line2[j];
                    if(line1_point.XValue > line2_point.XValue)
                    {
                        is_line1_bigger = true;

                        part_left_list.Add(line2_point);
                        part_right_list.Add(line1_point);

                    }
                    else if (line1_point.XValue < line2_point.XValue)
                    {
                        is_line1_bigger = false;
                        part_left_list.Add(line1_point);
                        part_right_list.Add(line2_point);
                    }
                    if (line1_point.XValue == line2_point.XValue && line1_point.YValue == line2_point.YValue)
                    {
                        //找到了焦点
                        curr_jiaoDian = line1_point;
                        curr_jiaodian_found = true;
                        j++;
                        break;
                    }

                    if(j+1 < line1.Count)
                    {
                        if((line1_point.XValue<line2_point.XValue&&line1[j+1].XValue>line2[j+1].XValue)||(line1_point.XValue>line2_point.XValue&&line1[j+1].XValue<line2[j+1].XValue))
                        {  //用函数粗略找到交点坐标
                            curr_jiaoDian = GetJiaoDIan.getjiaoDian(line1_point, line1[j + 1], line2_point, line2[j + 1]);
                            curr_jiaodian_found = true;
                            j++;
                            break;
                        }
                    }
                    //jiaodian_list.Add(line1_point);
                    //jiaodian_list.Add(line2_point);

                }
                #endregion

                Color fillColor = Color.Black;
                if(is_line1_bigger == true)
                {   //line1在右边，则填充色为 line1 线的颜色
                    fillColor = line1_color;
                }
                else
                {   //line1在左边，则填充色为 line2 线的颜色
                    fillColor = line2_color;
                }

                List<LJJSPoint> finalList = new List<LJJSPoint>();
                if (last_jiaoDian != null)
                {
                    finalList.Add(last_jiaoDian);
                }
                foreach(LJJSPoint point in part_left_list)
                {
                    finalList.Add(point);
                }
                if (curr_jiaoDian != null)
                {
                    finalList.Add(curr_jiaoDian);
                }

              

                if (curr_jiaodian_found == false)   //没有找到现在的焦点，构不成封闭区域
                {
                    double x = (line1[j - 1].XValue + line2[j - 1].XValue) / 2;
                    double y = line1[j - 1].YValue;
                    finalList.Add(new LJJSPoint(x,y ));
                }

                for(int cc = part_right_list.Count-1;cc >=0;cc--)
                {
                    finalList.Add(part_right_list[cc]);
                }
                string hatchColor = get_color_argb_by_color(fillColor).ToString();
                LJJSHatch ljjshatch = new LJJSHatch(hatchColor, "true");
                //GAY
                MyHatch.AddAreaHatch(activeDOcu, finalList, ljjshatch);

                last_jiaoDian = curr_jiaoDian;
                curr_jiaoDian = null;
            }
        }


        public static int get_color_argb_by_color(Color target_color)
        {
            return target_color.ToArgb();
        }








        public static void Gei_Qu_Xian_De_Jiao_Dian_Nei_Bu_Tu_Se(List<LJJSPoint> line1, List<LJJSPoint> line2, LineItemStruct lineItemStruct ,int index) //给曲线交点内部涂色
        {
            if (lineItemStruct.LiSubClass.Equals("wxcs2") || lineItemStruct.LiSubClass.Equals("jgl"))
            {        //物性指数交汇和进给量的数据有问题，其中一个在y值相同时有两个x值，另外一个比另一个多4个点 需要进行处理
                if (line1.Count != line2.Count)
                {
                    List<LJJSPoint> list = MsehAndMsevContainer.findLostPoints(line1, line2);//寻找多出的点
                    if (DrawPointContainer.list[index].JinGeiLiangList != null)
                    {
                        bool res1 = MsehAndMsevContainer.check_Multi_Same_YValue(line1);
                        bool res2 = MsehAndMsevContainer.check_Multi_Same_YValue(line2);
                        //若为true，则代表具有此list中有两个点的y值相同，x值不同，需要去掉


                        List<LJJSPoint> same_Y_list;
                        if (res1 == true)
                        {
                            same_Y_list = MsehAndMsevContainer.same_YValue_PointsList(line1);
                            if (same_Y_list.Count > 0)
                            {
                                for (int i = 0; i < same_Y_list.Count - 1; i++)
                                {
                                    line1.Remove(same_Y_list[i]);
                                }
                            }
                        }

                        if (res2 == true)
                        {
                            same_Y_list = MsehAndMsevContainer.same_YValue_PointsList(line2);
                            if (same_Y_list.Count > 0)
                            {
                                for (int i = 0; i < same_Y_list.Count - 1; i++)
                                {
                                    line2.Remove(same_Y_list[i]);
                                }
                            }
                        }

                        int jingeiliang1 = line1.Count;
                        int wxcs1 = line2.Count;

                        foreach (LJJSPoint point in list)
                        {
                            line2.Remove(point);   //去掉多出的点
                        }


                        bool rrr = MsehAndMsevContainer.checkSameYValue_orNot(line1, line2); //检测两个list中所有的点的y值能不能对应上
                        if (rrr == false) //对应不上，则直接返回
                        {

                            MessageBox.Show("两个曲线上的点的个数不同，错误");
                            MessageBox.Show(line1.Count.ToString());
                            MessageBox.Show(line2.Count.ToString());
                            return;
                        }

                    }

                    //StringBuilder sb = new StringBuilder();
                    /**if (list != null)
                    {
                        foreach (LJJSPoint item in list)
                        {
                            sb.Append(string.Format("多出的点坐标：({0},{1})", item.XValue.ToString(), item.YValue.ToString()));
                            sb.Append("\r\n");
                        }
                    }
                    MessageBox.Show(sb.ToString());**/

                }
            }



            for (int i = 0; i < line1.Count; i++)
            {
                if (double.IsNaN(line1[i].XValue) == true || double.IsInfinity(line1[i].XValue) == true)
                {
                    MessageBox.Show(string.Format("值是NaN，第{0}个", i));
                }
                if (double.IsNaN(line2[i].XValue) == true || double.IsInfinity(line2[i].XValue) == true)
                {
                    MessageBox.Show(string.Format("值是NaN，第{0}个", i));
                }
                if (line1[i].YValue != line2[i].YValue)
                {
                    MessageBox.Show("两个曲线上的点的YValue不同，错误");
                    return;
                }

            }
            //MessageBox.Show("正确，可以涂色");

            vdDocument activeDOcu = DrawCommonData.activeDocument;

            //对两条线，每次在每条线上分别取两个点，y值可以对应上（即可以连成两条平行直线）
            for (int i = 0; i < line1.Count - 1; i++)
            {
                LJJSPoint line1_x1y1 = line1[i];
                LJJSPoint line1_x2y2 = line1[i + 1];

                LJJSPoint line2_x1y1 = line2[i];
                LJJSPoint line2_x2y2 = line2[i + 1];
                List<LJJSPoint> list;

                if (line1_x1y1.YValue == line1_x2y2.YValue && line2_x1y1.YValue == line2_x2y2.YValue)
                {
                    continue;
                }


                //line1   is    this.drawptcol,   line2  is  this.randomPointList
                if (line2_x2y2.XValue < line1_x2y2.XValue)
                {
                    if (line2_x1y1.XValue < line1_x1y1.XValue)
                    {
                        list = new List<LJJSPoint>() { line2_x2y2, line1_x2y2, line1_x1y1, line2_x1y1 };


                        CurveColorEnum colorSelect;
                        int colorIndex = Color.Red.ToArgb();  //颜色的argb值

                        double xAvg = (line1_x1y1.XValue + line1_x2y2.XValue) / 2;
                        if (double.IsNaN(xAvg) == true || double.IsInfinity(xAvg) == true)
                        {
                            MessageBox.Show("xAvg是NaN");
                        }

                        AddLineHatchManager.selectColorByXValue_WithoutTransition("Original", out colorSelect, ref colorIndex);  //选择颜色


                        AreaHatch.AddStandardAreaHatch(activeDOcu, list, colorIndex, colorIndex);
                    }
                    else
                    {
                        LJJSPoint jiaodian = GetJiaoDIan.getjiaoDian(line1_x1y1, line1_x2y2, line2_x1y1, line2_x2y2);
                        //两个三角形涂色

                        //第一个三角形
                        list = new List<LJJSPoint>() { jiaodian, line2_x1y1, line1_x1y1 };
                        CurveColorEnum colorSelect;
                        int colorIndex = Color.Red.ToArgb();  //颜色的argb值

                        double xAvg = (jiaodian.XValue + line2_x1y1.XValue) / 2;
                        if (double.IsNaN(xAvg) == true || double.IsInfinity(xAvg) == true)
                        {
                            MessageBox.Show("xAvg是NaN");
                        }
                        AddLineHatchManager.selectColorByXValue_WithoutTransition("Red", out colorSelect, ref colorIndex);  //选择颜色
                        AreaHatch.AddStandardAreaHatch(activeDOcu, list, colorIndex, colorIndex);


                        //第二个三角形
                        list = new List<LJJSPoint>() { line2_x2y2, line1_x2y2, jiaodian };
                        colorIndex = Color.Red.ToArgb();  //颜色的argb值

                        xAvg = (jiaodian.XValue + line1_x2y2.XValue) / 2;
                        if (double.IsNaN(xAvg) == true || double.IsInfinity(xAvg) == true)
                        {
                            MessageBox.Show("xAvg是NaN");
                        }
                        AddLineHatchManager.selectColorByXValue_WithoutTransition("Original", out colorSelect, ref colorIndex);  //选择颜色
                        AreaHatch.AddStandardAreaHatch(activeDOcu, list, colorIndex, colorIndex);
                    }
                }
                else
                {
                    if (line2_x1y1.XValue > line1_x1y1.XValue)
                    {
                        list = new List<LJJSPoint>() { line1_x2y2, line2_x2y2, line2_x1y1, line1_x1y1 };

                        CurveColorEnum colorSelect;
                        int colorIndex = Color.Red.ToArgb();  //颜色的argb值

                        double xAvg = (line2_x1y1.XValue + line2_x2y2.XValue) / 2;
                        if (double.IsNaN(xAvg) == true || double.IsInfinity(xAvg) == true)
                        {
                            MessageBox.Show("xAvg是NaN");
                        }
                        AddLineHatchManager.selectColorByXValue_WithoutTransition("Red", out colorSelect, ref colorIndex);  //选择颜色

                        AreaHatch.AddStandardAreaHatch(activeDOcu, list, colorIndex, colorIndex);
                    }
                    else
                    {
                        LJJSPoint jiaodian = GetJiaoDIan.getjiaoDian(line1_x1y1, line1_x2y2, line2_x1y1, line2_x2y2);
                        //两个三角形涂色

                        //第一个三角形
                        list = new List<LJJSPoint>() { jiaodian, line1_x1y1, line2_x1y1 };

                        CurveColorEnum colorSelect;
                        int colorIndex = Color.Red.ToArgb();  //颜色的argb值
                        double xAvg = (jiaodian.XValue + line1_x1y1.XValue) / 2;
                        if (double.IsNaN(xAvg) == true || double.IsInfinity(xAvg) == true)
                        {
                            MessageBox.Show("xAvg是NaN");
                        }
                        AddLineHatchManager.selectColorByXValue_WithoutTransition("Original", out colorSelect, ref colorIndex);  //选择颜色

                        AreaHatch.AddStandardAreaHatch(activeDOcu, list, colorIndex, colorIndex);


                        //第二个三角形
                        list = new List<LJJSPoint>() { line1_x2y2, line2_x2y2, jiaodian };


                        colorIndex = Color.Red.ToArgb();  //颜色的argb值

                        xAvg = (jiaodian.XValue + line2_x2y2.XValue) / 2;
                        if (double.IsNaN(xAvg) == true || double.IsInfinity(xAvg) == true)
                        {
                            MessageBox.Show("xAvg是NaN");
                        }
                        AddLineHatchManager.selectColorByXValue_WithoutTransition("Red", out colorSelect, ref colorIndex);  //选择颜色

                        AreaHatch.AddStandardAreaHatch(activeDOcu, list, colorIndex, colorIndex);
                    }
                }




            }
        }
    }
}
