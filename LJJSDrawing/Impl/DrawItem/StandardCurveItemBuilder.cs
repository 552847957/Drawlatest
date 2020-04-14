using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.Model.Drawing;
using LJJSCAD.DrawingOper;
using LJJSCAD.DrawingElement;
using System.Collections;
using System.Windows.Forms;
using LJJSCAD.DAL;
using LJJSCAD.Util;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.Drawing.Curve;
using System.Drawing;
using LJJSCAD.Drawing.Manage;
using LJJSCAD.Drawing.Figure;
using LJJSCAD.ItemStyleOper;
using System.Data;
using DesignEnum;
using LJJSCAD.LJJSDrawing.Impl.DrawItem.SymbolItem;
using LJJSCAD.Drawing.Hatch;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.Components;
using LJJSCAD.CommonData;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem
{
    public delegate void set_start_point_DELE(LJJSPoint startpoint);
    class StandardCurveItemBuilder : DrawItemBuilder
    {
       


        public string CallByDoubleLine = null;
        public static string depth = "depth";//默认的井深字段名;
        public LineItemStruct lineItemStruct;
        private List<KeDuChiItem> m_KDCList;
        public static string GetJSField(string jsField)
        {
            string reval=depth;
            if (!string.IsNullOrEmpty(jsField))
                reval = jsField.Trim();
            return reval;
        }

        

        public override void AddPerJDDrawItem(JDStruc jdStruc,int i)
        {

            double jdtop = jdStruc.JDtop;
            double jdbottom = jdStruc.JDBottom;

            Hashtable drawht;  //里面装有所有点的 x y坐标值 key是y值即depth  value是x值

            List<Hashtable> drawhtList = null; //如果是P，则需要画两条线 list【0】是本来的线，【1】是基质线
            
            if (jdtop < jdbottom)
            {
                /**
                if (lineItemStruct.LIFromFieldName.Trim().Equals("P") == false)
                {
                    drawht = CurveItemDAL.GetCurveItemPointHt(ItemDataTable, jdtop.ToString(), jdbottom.ToString(), lineItemStruct.LIFromFieldName.Trim(), lineItemStruct.JsField.Trim());//..GetJoinAdjustLineItemPointCol(LineItemDrawDt, sqltxt, m_liStruc.LIFromFieldName, depth);//保存了绘图的点集；key为Y值，value为x值；
                }
                else  //是P
                {
                    drawht = CurveItemDAL.GetCurveItemPointHt(ItemDataTable, jdtop.ToString(), jdbottom.ToString(), lineItemStruct.LIFromFieldName.Trim(), lineItemStruct.JsField.Trim());
                    drawhtList = CurveItemDAL.GetCurveItemPointHt(ItemDataTable, jdtop.ToString(), jdbottom.ToString(), lineItemStruct.JsField.Trim(), lineItemStruct.LIFromFieldName.Trim(), "MSEJ");
                }**/

                drawht = CurveItemDAL.GetCurveItemPointHt(ItemDataTable, jdtop.ToString(), jdbottom.ToString(), lineItemStruct.LIFromFieldName.Trim(), lineItemStruct.JsField.Trim());//..GetJoinAdjustLineItemPointCol(LineItemDrawDt, sqltxt, m_liStruc.LIFromFieldName, depth);//保存了绘图的点集；key为Y值，value为x值；
                if (drawht.Count > 0)
                {
                    
       
                    if (lineItemStruct.LineItemType.Equals(CJQXLineClass.StickLine))//棒图；
                    {
                        this.AddStickLineToFigure(jdStruc, drawht);
                    }

                    else //折线；
                    {
                       
                        this.AddContinuousLineToFigure(jdStruc, drawht, i,drawhtList);
                    }
                    


                }
            }

        }

        private void AddContinuousLineToFigure(JDStruc jdstruc, Hashtable drawht, int index, List<Hashtable> drawhtList)
        {
            
            //fff

            Color curr_color = AddLineHatchManager.get_Color_By_CurveItemShowName(this.lineItemStruct);

            if (null==m_KDCList||m_KDCList.Count == 0)
                return ;

            ArrayList al;//完成排序的绘图点;

            ArrayList al1 = null;

            if (m_KDCList[0].KStyle == KDCStyle.DuiShu)//对数项；
            {
                drawht = HashUtil.MoveHashTableZeroValue(drawht);//去掉数据中的0值；
                al = HashUtil.GetHastablePaiXuList(drawht);//完成排序的绘图点;
                if (al.Count < 2)
                    return ;
                AddDuiShuContinuousToFigure(m_KDCList[0], al, jdstruc, drawht, curr_color);//添加对数类型折线
                return ;
            }
            else//普通项
            {
             
                al = HashUtil.GetHastablePaiXuList(drawht);//完成排序的绘图点; 将所有y值即depth由小到大排序
                /**
                if (drawhtList != null)
                {
                    al1 = HashUtil.GetHastablePaiXuList(drawhtList[1]);
                }**/
                if (al.Count < 2)
                    return ;

                if (lineItemStruct.LiSubClass.Equals("msev") == false && lineItemStruct.LiSubClass.Equals("mseh") == false && lineItemStruct.LiSubClass.Equals("wxcs2") == false && lineItemStruct.LiSubClass.Equals("jgl") == false)
                {   //不是填充垂向功和切向功 或者 填充物性指数交汇和进给量的情况
                    this.AddNormalContinuousToFigure(al, jdstruc, drawht, curr_color , index);  //画边缘线

                }

                //填充物性指数交汇和进给量

                #region
                if (lineItemStruct.LiSubClass.Equals("wxcs2"))
                {

                    if (DrawPointContainer.list[index].WXCSJiaoHuiList == null)
                    {
                        this.AddNormalContinuousToFigure(al, jdstruc, drawht, curr_color, index); //Blue
                    }



                    if (null != DrawPointContainer.list[index].WXCSJiaoHuiList && null != DrawPointContainer.list[index].JinGeiLiangList)
                    {

                        //Gai

                        Color wxcs_jiaohui_color = AddLineHatchManager.get_Color_By_CurveItemShowName("物性指数交汇");
                        Color jgl_color = AddLineHatchManager.get_Color_By_CurveItemShowName("进给量");

                        AddLineHatchManager.Gei_Qu_Xian_De_Jiao_Dian_Nei_Bu_Tu_Se_Quicker(wxcs_jiaohui_color,jgl_color,DrawPointContainer.list[index].WXCSJiaoHuiList, DrawPointContainer.list[index].JinGeiLiangList, lineItemStruct, index);
                        
                    }
                   
                }

                if (lineItemStruct.LiSubClass.Equals("jgl"))
                {


                    if (DrawPointContainer.list[index].JinGeiLiangList == null)
                    {
                        this.AddNormalContinuousToFigure(al, jdstruc, drawht, curr_color, index); // Green
                    }






                    if (null != DrawPointContainer.list[index].WXCSJiaoHuiList && null != DrawPointContainer.list[index].JinGeiLiangList)
                    {
                        //Gai
                        Color wxcs_jiaohui_color = AddLineHatchManager.get_Color_By_CurveItemShowName("物性指数交汇");
                        Color jgl_color = AddLineHatchManager.get_Color_By_CurveItemShowName("进给量");
                        AddLineHatchManager.Gei_Qu_Xian_De_Jiao_Dian_Nei_Bu_Tu_Se_Quicker(wxcs_jiaohui_color,jgl_color,DrawPointContainer.list[index].WXCSJiaoHuiList, DrawPointContainer.list[index].JinGeiLiangList, lineItemStruct,  index);
                       
                    }
                   
                }
                #endregion
                //填充垂向功和切向功
                #region
               
                if (lineItemStruct.LiSubClass.Equals("msev"))
                {

                    if (DrawPointContainer.list[index].msevList == null)
                    {
                        this.AddNormalContinuousToFigure(al, jdstruc, drawht, curr_color, index);
                    }



                    if (null != DrawPointContainer.list[index].msevList && null != DrawPointContainer.list[index].msehList)
                    {
                        //Gai

                        Color msev_color = AddLineHatchManager.get_Color_By_CurveItemShowName("垂向功");
                        Color mseh_color = AddLineHatchManager.get_Color_By_CurveItemShowName("切向功");

                        AddLineHatchManager.Gei_Qu_Xian_De_Jiao_Dian_Nei_Bu_Tu_Se_Quicker(mseh_color,msev_color,DrawPointContainer.list[index].msehList, DrawPointContainer.list[index].msevList, lineItemStruct, index);
                        
                    }

                 
                }

                if (lineItemStruct.LiSubClass.Equals("mseh"))
                {


                    if (DrawPointContainer.list[index].msehList == null)
                    {
                        this.AddNormalContinuousToFigure(al, jdstruc, drawht, curr_color, index);
                    }





                    if (null != DrawPointContainer.list[index].msevList && null != DrawPointContainer.list[index].msehList)
                    {

                        //Gai

                        Color mseh_color = AddLineHatchManager.get_Color_By_CurveItemShowName("切向功");
                        Color msev_color = AddLineHatchManager.get_Color_By_CurveItemShowName("垂向功");
                        AddLineHatchManager.Gei_Qu_Xian_De_Jiao_Dian_Nei_Bu_Tu_Se_Quicker(mseh_color,msev_color,DrawPointContainer.list[index].msehList, DrawPointContainer.list[index].msevList, lineItemStruct,  index);
                        
                    }
                    
                }
                #endregion
                //TO BE DONE
                string sb = lineItemStruct.LiSubClass;
                if (lineItemStruct.LiSubClass == "wxcs" )   //物性指数涂色
                {
                   
                    if (this.lrptstartName.Equals("wxcs"))
                    {
                        if (DrawPointContainer.list[index].wxcsList == null)
                        {
                            DrawPointContainer.list[index].wxcsList = new List<LJJSPoint>();
                        }
                        //suizuan
                        AddLineHatchManager.Gei_Qu_Xian_Tu_Se(al, jdstruc, drawht, DrawPointContainer.list[index].wxcsList, this.lrptstart);    //在此给画好的曲线填充颜色
                        //1118 wxcs
                        this.AddNormalContinuousToFigure(al, jdstruc, drawht, Color.Black, index); //填充颜色后，边缘线消失，需要再画一次
                        /**
                        if (drawhtList != null)
                        {
                            this.AddNormalContinuousToFigure(al, jdstruc, drawhtList[1], Color.Blue, index,null);
                        }**/
                    }
                    else
                    {
                        MessageBox.Show("不知道线道起点坐标，无法显示涂色");
                    }
                    
                }     
              
            }
            
            
            
            
            
        }
        

        

        //single paint
        
        
        
        

       



        private KeDuChiItem PointIfChaoJie(double yval, Hashtable drawht)
        {
            KeDuChiItem drawkdc = null;

            double xval = (double)drawht[yval];
            for (int j = 0; j < m_KDCList.Count; j++)
            {
                KeDuChiItem tmpdrawkdc = m_KDCList[j];
                if (ZuoBiaoOper.IfInKeDuChi(xval, tmpdrawkdc))
                {
                    drawkdc = tmpdrawkdc;
                    break;
                }
            }
            return drawkdc;
        }
        //understand
        private LJJSPoint GetPtZBByKDC(KeDuChiItem drawkdc, LJJSPoint jdptstart, double xval, double yval, double jdtop)
        {
            ZuoBiaoOper zbopp = new ZuoBiaoOper(FrameDesign.ValueCoordinate);
            LJJSPoint lrptstart = ZuoBiaoOper.UpdateLRStartPt(drawkdc.KDir, jdptstart, LineRoadEnvironment.LineRoadWidth);
            return zbopp.GetDrawingZuoBiaoPt(lrptstart, drawkdc, xval, yval, jdtop, LineRoadEnvironment.LineRoadWidth);

        }



        public LJJSPoint lrptstart;

        public string lrptstartName;
        private void AddNormalContinuousToFigure(ArrayList al, JDStruc jdstruc, Hashtable drawht,  Color penColor,int index,LJJSPoint startPoint)
        {

            double jdtop = jdstruc.JDtop;
            string prekdcname = "No";
            double preyval = 0;
            double prexvalue = 0;
            string currkdcname = "MSEJ";
            LJJSPoint lrptstart;
            LJJSPoint currpt = new LJJSPoint(0, 0);
            List<LJJSPoint> drawptcol = new List<LJJSPoint>();
            List<LJJSPoint> outdrawcol = new List<LJJSPoint>();

            List<List<LJJSPoint>> outdrawcollist = new List<List<LJJSPoint>>();

            double jdbottom = jdstruc.JDBottom;
            ZuoBiaoOper zbopp = new ZuoBiaoOper(FrameDesign.ValueCoordinate);
            LJJSPoint prept = new LJJSPoint(0, 0);
            KeDuChiItem drawkdc = null;
            LJJSPoint chajieinterpt;//超界交点
            KeDuChiItem outdrawkdc = m_KDCList[m_KDCList.Count - 1];
            double lastxvalue = 0;


            #region
            for (int i = 0; i < al.Count; i++)
            {

                double yval = (double)al[i];
                double xvalue = (double)drawht[yval]; //获取 每个点x，y的值

                drawkdc = PointIfChaoJie(yval, drawht);

                if (drawkdc != null)//该点未超界;
                {
                    outdrawkdc = drawkdc;
                    lrptstart = ZuoBiaoOper.UpdateLRStartPt(drawkdc.KDir, jdstruc.JDPtStart, LineRoadEnvironment.LineRoadWidth);

                    this.lrptstart = lrptstart;
                    this.lrptstartName = lineItemStruct.LiSubClass;
                    //MessageBox.Show(string.Format("lrptstart:({0},{1})", lrptstart.XValue.ToString(), lrptstart.YValue.ToString()));

                    currkdcname = drawkdc.KName;
                    currpt = GetPtZBByKDC(drawkdc, jdstruc.JDPtStart, (double)drawht[yval], yval, jdstruc.JDtop);
                    prept = currpt;
                    if (StrUtil.IfTwoStrEqul(prekdcname, currkdcname))
                    {

                        drawptcol.Add(currpt);//和上一点为同一刻度尺；则加入绘图集合；
                    }
                    else//开始下一段曲线的操作；分为从超界曲线向非超界的跳变，也有可能是非超界之间的跳变;
                    {
                        if (prekdcname.Equals(""))//从超界曲线向非超界的跳变
                        {
                            if (lastxvalue < drawkdc.KMin)
                            {
                                chajieinterpt = ZuoBiaoOper.GetTiaoBianIntersectionPoint(currpt, prept, lrptstart.XValue);

                            }
                            else

                                chajieinterpt = ZuoBiaoOper.GetTiaoBianIntersectionPoint(currpt, prept, lrptstart.XValue + outdrawkdc.KDir * LineRoadEnvironment.LineRoadWidth);


                            outdrawcol.Add(chajieinterpt);
                            drawptcol.Clear();
                            drawptcol.Add(chajieinterpt);

                            if (outdrawcol.Count > 0)
                                outdrawcollist.Add(outdrawcol);
                        }
                        else//非超界之间的跳变;
                        {
                            if (drawptcol.Count > 1)
                            {
                                this.DrawContinuousLine(drawptcol, prekdcname, drawkdc.kLineWidth, penColor);//绘制上一段曲线；


                            }

                            drawptcol.Clear();

                        }
                        drawptcol.Add(currpt);
                    }
                    prekdcname = currkdcname;
                    prexvalue = (double)drawht[yval];
                    preyval = yval;
                }
                else//该点已超界  drawkdc == null
                {


                    if (m_KDCList.Count > 0)
                    {

                        currpt = GetPtZBByKDC(outdrawkdc, jdstruc.JDPtStart, (double)drawht[yval], yval, jdstruc.JDtop);
                        currkdcname = "";
                        if (lineItemStruct.LineItemChaoJie == CJQXChaoJie.BiaoZhu)//添加标注信息；
                        {
                            if (!StrUtil.IfTwoStrEqul(prekdcname, currkdcname))//跳变点
                            {
                                lrptstart = ZuoBiaoOper.UpdateLRStartPt(outdrawkdc.KDir, jdstruc.JDPtStart, lineRoadEnvironment.LineRoadWidth);
                                this.lrptstart = lrptstart;
                                this.lrptstartName = lineItemStruct.LiSubClass;
                                if (xvalue < outdrawkdc.KMin)
                                {
                                    chajieinterpt = ZuoBiaoOper.GetTiaoBianIntersectionPoint(currpt, prept, lrptstart.XValue);

                                }
                                else
                                {
                                    chajieinterpt = ZuoBiaoOper.GetTiaoBianIntersectionPoint(currpt, prept, lrptstart.XValue + outdrawkdc.KDir * lineRoadEnvironment.LineRoadWidth);
                                }
                                drawptcol.Add(chajieinterpt);
                                if (drawptcol.Count > 0)
                                {


                                    this.DrawContinuousLine(drawptcol, prekdcname, outdrawkdc.kLineWidth, penColor);//绘制上一段非超界曲线；


                                    drawptcol.Clear();
                                }
                            }
                            prept = currpt;
                            prekdcname = currkdcname;
                            prexvalue = (double)drawht[yval];
                            preyval = yval;

                            continue;
                        }


                        if (StrUtil.IfTwoStrEqul(prekdcname, currkdcname))
                        {
                            outdrawcol.Add(currpt);//和上一点为同一刻度尺；则加入绘图集合；
                        }
                        else//此时为从非超界向超界跳变；上一段非超界曲线结束;
                        {
                            outdrawcol = new List<LJJSPoint>();
                            lrptstart = ZuoBiaoOper.UpdateLRStartPt(outdrawkdc.KDir, jdstruc.JDPtStart, lineRoadEnvironment.LineRoadWidth);
                            this.lrptstart = lrptstart;
                            this.lrptstartName = lineItemStruct.LiSubClass;
                            if (xvalue < outdrawkdc.KMin)
                            {
                                chajieinterpt = ZuoBiaoOper.GetTiaoBianIntersectionPoint(currpt, prept, lrptstart.XValue);

                            }
                            else

                                chajieinterpt = ZuoBiaoOper.GetTiaoBianIntersectionPoint(currpt, prept, lrptstart.XValue + outdrawkdc.KDir * lineRoadEnvironment.LineRoadWidth);

                            drawptcol.Add(chajieinterpt);
                            if (drawptcol.Count > 1)
                            {
                                this.DrawContinuousLine(drawptcol, prekdcname, outdrawkdc.kLineWidth, penColor);//绘制上一段非超界曲线；

                                drawptcol.Clear();
                            }
                            if (outdrawcol.Count > 0)
                                outdrawcollist.Add(outdrawcol);
                            outdrawcol.Clear();
                            if (i != 0)
                                outdrawcol.Add(chajieinterpt);


                            outdrawcol.Add(currpt);
                            prekdcname = currkdcname;
                            prexvalue = (double)drawht[yval];
                            preyval = yval;
                        }
                    }
                }
                prept = currpt;
                lastxvalue = xvalue;
            }//遍历完所有的点;
            #endregion
            lrptstart = ZuoBiaoOper.UpdateLRStartPt(outdrawkdc.KDir, jdstruc.JDPtStart, lineRoadEnvironment.LineRoadWidth);


            
            this.lrptstart = lrptstart;
            this.lrptstartName = lineItemStruct.LiSubClass;
            if ((currkdcname == "") && (outdrawcol.Count > 1))
            {
                outdrawcollist.Add(outdrawcol);


                //add here?

            }



            //this.DrawContinuousLine(drawptcol, currkdcname, drawkdc.kLineWidth, penColor);//绘制上一段非超界曲线；在此画线
            if (drawptcol.Count > 1 && drawkdc != null)
            {

                

                this.DrawContinuousLine(drawptcol, currkdcname, drawkdc.kLineWidth, penColor);//绘制上一段非超界曲线；在此画线

            }

           
            
        }
        private void AddNormalContinuousToFigure(ArrayList al, JDStruc jdstruc, Hashtable drawht,  Color penColor,int index)
        {
            
            double jdtop = jdstruc.JDtop;
            string prekdcname = "No";
            double preyval = 0;
            double prexvalue = 0;
            string currkdcname = "";
            LJJSPoint lrptstart;
            LJJSPoint currpt=new LJJSPoint(0,0);
            List<LJJSPoint> drawptcol = new List<LJJSPoint>();
            List<LJJSPoint> outdrawcol = new List<LJJSPoint>();

            List<List<LJJSPoint>> outdrawcollist = new List<List<LJJSPoint>>();

            double jdbottom = jdstruc.JDBottom;
            ZuoBiaoOper zbopp = new ZuoBiaoOper(FrameDesign.ValueCoordinate);
            LJJSPoint prept=new LJJSPoint(0,0);
            KeDuChiItem drawkdc = null;
            LJJSPoint chajieinterpt;//超界交点
            KeDuChiItem outdrawkdc = m_KDCList[m_KDCList.Count - 1];
            double lastxvalue = 0;


            #region
            for (int i = 0; i < al.Count; i++)
            {

                double yval = (double)al[i];
                double xvalue = (double)drawht[yval]; //获取 每个点x，y的值

                drawkdc = PointIfChaoJie(yval, drawht);

                if (drawkdc != null)//该点未超界;
                {
                    outdrawkdc = drawkdc;
                    lrptstart = ZuoBiaoOper.UpdateLRStartPt(drawkdc.KDir, jdstruc.JDPtStart, LineRoadEnvironment.LineRoadWidth);

                    this.lrptstart = lrptstart;
                    this.lrptstartName = lineItemStruct.LiSubClass;
                    //MessageBox.Show(string.Format("lrptstart:({0},{1})", lrptstart.XValue.ToString(), lrptstart.YValue.ToString()));

                    currkdcname = drawkdc.KName;
                    currpt = GetPtZBByKDC(drawkdc, jdstruc.JDPtStart, (double)drawht[yval], yval, jdstruc.JDtop);
                    prept = currpt;
                    if (StrUtil.IfTwoStrEqul(prekdcname, currkdcname))
                    {
                        
                        drawptcol.Add(currpt);//和上一点为同一刻度尺；则加入绘图集合；
                    }
                    else//开始下一段曲线的操作；分为从超界曲线向非超界的跳变，也有可能是非超界之间的跳变;
                    {
                        if (prekdcname.Equals(""))//从超界曲线向非超界的跳变
                        {
                            if (lastxvalue < drawkdc.KMin)
                            {
                                chajieinterpt = ZuoBiaoOper.GetTiaoBianIntersectionPoint(currpt, prept, lrptstart.XValue);

                            }
                            else

                                chajieinterpt = ZuoBiaoOper.GetTiaoBianIntersectionPoint(currpt, prept, lrptstart.XValue + outdrawkdc.KDir * LineRoadEnvironment.LineRoadWidth);

                            
                            outdrawcol.Add(chajieinterpt);
                            drawptcol.Clear();
                            drawptcol.Add(chajieinterpt);
                            
                            if (outdrawcol.Count > 0)
                                outdrawcollist.Add(outdrawcol);
                        }
                        else//非超界之间的跳变;
                        {
                            if (drawptcol.Count > 1)
                            {
                                this.DrawContinuousLine(drawptcol, prekdcname, drawkdc.kLineWidth, penColor);//绘制上一段曲线；
                                

                            }
                                
                            drawptcol.Clear();

                        }
                        drawptcol.Add(currpt);
                    }
                    prekdcname = currkdcname;
                    prexvalue = (double)drawht[yval];
                    preyval = yval;
                }
                else//该点已超界  drawkdc == null
                {

                   
                    if (m_KDCList.Count > 0)
                    {

                        currpt = GetPtZBByKDC(outdrawkdc, jdstruc.JDPtStart, (double)drawht[yval], yval, jdstruc.JDtop);
                        currkdcname = "";
                        if (lineItemStruct.LineItemChaoJie == CJQXChaoJie.BiaoZhu)//添加标注信息；
                        {
                            if (!StrUtil.IfTwoStrEqul(prekdcname, currkdcname))//跳变点
                            {
                                lrptstart = ZuoBiaoOper.UpdateLRStartPt(outdrawkdc.KDir, jdstruc.JDPtStart, lineRoadEnvironment.LineRoadWidth);
                                this.lrptstart = lrptstart;
                                this.lrptstartName = lineItemStruct.LiSubClass;
                                if (xvalue < outdrawkdc.KMin)
                                {
                                    chajieinterpt = ZuoBiaoOper.GetTiaoBianIntersectionPoint(currpt, prept, lrptstart.XValue);

                                }
                                else
                                {
                                    chajieinterpt = ZuoBiaoOper.GetTiaoBianIntersectionPoint(currpt, prept, lrptstart.XValue + outdrawkdc.KDir * lineRoadEnvironment.LineRoadWidth);
                                }
                                drawptcol.Add(chajieinterpt);
                                if (drawptcol.Count > 0)
                                {
                                    
                                    
                                    this.DrawContinuousLine(drawptcol, prekdcname, outdrawkdc.kLineWidth, penColor);//绘制上一段非超界曲线；

                                    
                                    drawptcol.Clear();
                                }
                            }
                            prept = currpt;
                            prekdcname = currkdcname;
                            prexvalue = (double)drawht[yval];
                            preyval = yval;
                  
                            continue;
                        }


                        if (StrUtil.IfTwoStrEqul(prekdcname, currkdcname))
                        {
                            outdrawcol.Add(currpt);//和上一点为同一刻度尺；则加入绘图集合；
                        }
                        else//此时为从非超界向超界跳变；上一段非超界曲线结束;
                        {
                            outdrawcol = new List<LJJSPoint>();
                            lrptstart = ZuoBiaoOper.UpdateLRStartPt(outdrawkdc.KDir, jdstruc.JDPtStart, lineRoadEnvironment.LineRoadWidth);
                            this.lrptstart = lrptstart;
                            this.lrptstartName = lineItemStruct.LiSubClass;
                            if (xvalue < outdrawkdc.KMin)
                            {
                                chajieinterpt = ZuoBiaoOper.GetTiaoBianIntersectionPoint(currpt, prept, lrptstart.XValue);

                            }
                            else

                                chajieinterpt = ZuoBiaoOper.GetTiaoBianIntersectionPoint(currpt, prept, lrptstart.XValue + outdrawkdc.KDir * lineRoadEnvironment.LineRoadWidth);

                            drawptcol.Add(chajieinterpt);
                            if (drawptcol.Count > 1)
                            {
                                this.DrawContinuousLine(drawptcol, prekdcname, outdrawkdc.kLineWidth, penColor);//绘制上一段非超界曲线；

                                drawptcol.Clear();
                            }
                            if (outdrawcol.Count > 0)
                                outdrawcollist.Add(outdrawcol);
                            outdrawcol.Clear();
                            if (i != 0)
                                outdrawcol.Add(chajieinterpt);


                            outdrawcol.Add(currpt);
                            prekdcname = currkdcname;
                            prexvalue = (double)drawht[yval];
                            preyval = yval;
                        }
                    }
                }
                prept = currpt;
                lastxvalue = xvalue;
            }//遍历完所有的点;
            #endregion
            lrptstart = ZuoBiaoOper.UpdateLRStartPt(outdrawkdc.KDir, jdstruc.JDPtStart, lineRoadEnvironment.LineRoadWidth);


            this.set_start_point_dele = SuiZuanForm.set_startpoint;//这两行代码用于随钻界面画图后界面的放大，如果是绘图可无视
            this.set_start_point_dele(lrptstart);  //这两行代码用于随钻界面画图后界面的放大，如果是绘图可无视
            this.lrptstart = lrptstart;
            this.lrptstartName = lineItemStruct.LiSubClass;
            if ((currkdcname == "") && (outdrawcol.Count > 1))
            {
                outdrawcollist.Add(outdrawcol);

                
                //add here?
                
            }

            
            

            if (drawptcol.Count > 1 && drawkdc != null)  
            {                                            
                
                //在循环进行完毕，将drawptcol ，即画的线上的所有点的集合保存在一个静态类中
                if (lineItemStruct.LiSubClass == "msev" && DrawPointContainer.list[index].msevList == null)
                {
                    DrawPointContainer.list[index].msevList = drawptcol;
                    //MsehAndMsevContainer.msevList = drawptcol;
                }
                if (lineItemStruct.LiSubClass == "mseh" && DrawPointContainer.list[index].msehList == null)
                {
                    DrawPointContainer.list[index].msehList = drawptcol;
                }

                if (lineItemStruct.LiSubClass == "wxcs" && DrawPointContainer.list[index].wxcsList == null)
                {
                    DrawPointContainer.list[index].wxcsList = drawptcol;
                }

                if (lineItemStruct.LiSubClass == "wxcs2" && DrawPointContainer.list[index].WXCSJiaoHuiList == null)
                {
                    DrawPointContainer.list[index].WXCSJiaoHuiList = drawptcol;
                }

                if (lineItemStruct.LiSubClass == "jgl" && DrawPointContainer.list[index].JinGeiLiangList == null)
                {
                    DrawPointContainer.list[index].JinGeiLiangList = drawptcol;
                }

                
                this.DrawContinuousLine(drawptcol, currkdcname, drawkdc.kLineWidth, penColor);//绘制上一段非超界曲线；在此画线
                
            }
               
            if (outdrawcollist.Count > 0)
                AddChaoJieLine(outdrawcollist, lrptstart.XValue + outdrawkdc.KDir * lineRoadEnvironment.LineRoadWidth, outdrawkdc.KDir, outdrawkdc.KName, outdrawkdc.kLineWidth, penColor);
            
        }

        private void AddChaoJieLine(List<List<LJJSPoint>> outDrawcolList, double chaoJieXvalue, int KDir, string layername, double linewidth, Color penColor)
        {
            List<LJJSPoint> tmpptcol = new List<LJJSPoint>();
            int ptcount = 0;

            if (lineItemStruct.LineItemChaoJie == CJQXChaoJie.PingYi)
            {

                foreach (List<LJJSPoint> ptcol in outDrawcolList)
                {
                    ptcount = ptcol.Count;
                    if (ptcount > 1)
                    {
                        if (ptcol[1].XValue > lineRoadEnvironment.JdDrawLst[0].JDPtStart.XValue)
                            for (int j = 0; j < ptcount; j++)
                                tmpptcol.Add(new LJJSPoint(ptcol[j].XValue - lineRoadEnvironment.LineRoadWidth, ptcol[j].YValue));
                        else
                            for (int j = 0; j < ptcount; j++)
                                tmpptcol.Add(new LJJSPoint(ptcol[j].XValue + lineRoadEnvironment.LineRoadWidth, ptcol[j].YValue));
                        DrawContinuousLine(tmpptcol, layername, linewidth, penColor);
                        tmpptcol.Clear();
                    }
                }
            }
            else if (lineItemStruct.LineItemChaoJie == CJQXChaoJie.ZheHui)
            {
                foreach (List<LJJSPoint> ptcol in outDrawcolList)
                {
                    ptcount = ptcol.Count;
                    if (ptcount > 1)
                    {
                        for (int j = 0; j < ptcount; j++)
                            tmpptcol.Add(new LJJSPoint(2 * lineRoadEnvironment.LineRoadWidth - ptcol[j].XValue, ptcol[j].YValue));
                        DrawContinuousLine(tmpptcol, layername, linewidth, penColor);
                        tmpptcol.Clear();
                    }
                }
            }

        }
        private void AddChaoJieBZ(double xval, LJJSPoint jdStartPt, double BzJS, double jdStartJS, double blcvalue, KeDuChiItem drawkdc, double lRWidth)
        {
            Layer.Layer_SetToCurrent(drawkdc.KName);
            double chaojiepty = ZuoBiaoOper.GetJSZongZBValue(jdStartPt.YValue, BzJS, jdStartJS, blcvalue);

            LJJSPoint chaojiept = new LJJSPoint(jdStartPt.XValue + drawkdc.KDir * lRWidth, chaojiepty);
            ChaoJieBZ cjbz = new ChaoJieBZ(xval, chaojiept);
            cjbz.DrawChaoJieBiaoZhu(drawkdc.KDir);

        }

        public set_start_point_DELE set_start_point_dele;

        private void AddDuiShuContinuousToFigure(KeDuChiItem duiShuKDC, ArrayList al, JDStruc jdstruc, Hashtable drawht, Color penColor)
        {
            List<LJJSPoint> drawptcol = new List<LJJSPoint>();
            double yval = 0;
            LJJSPoint lrptstart = ZuoBiaoOper.UpdateLRStartPt(duiShuKDC.KDir, jdstruc.JDPtStart, LineRoadEnvironment.LineRoadWidth);//根据刻度尺方向获得起点；
            for (int i = 0; i < al.Count; i++)
            {
                yval = (double)al[i];
                drawptcol.Add(GetDuiShuPointZB(duiShuKDC, yval, drawht, jdstruc, lrptstart));
            }
            DrawContinuousLine(drawptcol, duiShuKDC.KName, duiShuKDC.kLineWidth, penColor);//绘制上一段曲线;
        }
        private void DrawContinuousLine(List<LJJSPoint> ptcol, string layerName, double lineWidth, Color penColor)
        {

            Layer.Layer_SetToCurrent(layerName);
            ulong objid = PolyLine.BuildCommonPolyLine(ptcol, lineWidth, penColor);     

        }
        private LJJSPoint GetDuiShuPointZB(KeDuChiItem duiShuKDC, double yvalue, Hashtable drawht, JDStruc jdstruc, LJJSPoint lrptstart)
        {
            double xvalue = (double)drawht[yvalue];

            double Xpt = DuiShuOper.XGetDSZuoBiaoValue(lrptstart.XValue, duiShuKDC.KDir, duiShuKDC.KParm, xvalue, duiShuKDC.KMin);
            double Ypt = ZuoBiaoOper.GetJSZongZBValue(jdstruc.JDPtStart.YValue, yvalue, jdstruc.JDtop, FrameDesign.ValueCoordinate);
            return new LJJSPoint(Xpt, Ypt);
        }
        private void AddStickLineToFigure(JDStruc jdstruc, Hashtable drawht)
        {
            if (m_KDCList.Count == 0)
                return;
            if (drawht.Count < 1)
                return;
            if (m_KDCList[0].KStyle == KDCStyle.DuiShu)
            {
                drawht = HashUtil.MoveHashTableZeroValue(drawht);
                if (drawht.Count < 2)
                    return;
                AddDuiShuStickLineToFigure(m_KDCList[0], jdstruc, drawht);
            }
            else
            {
                AddNormalStickLineToFigure(jdstruc, drawht);
            }
        }
        /// <summary>
        /// 添加普通类型的棒线，是与对数类型相区别；
        /// </summary>
        /// <param name="jdstruc">绘制棒线的井段范围</param>
        /// <param name="drawht">绘制棒线的数据，hashtable的key为y值也就是井深，X为棒线的横向取值</param>
        private void AddNormalStickLineToFigure(JDStruc jdstruc, Hashtable drawht)
        {
            bool ifinkdc = false;
            double jdtop = jdstruc.JDtop;
            double jdbottom = jdstruc.JDBottom;
            KeDuChiItem drawkdc;
            LJJSPoint lrptstart;
            int kdccount = m_KDCList.Count;

            foreach (DictionaryEntry de in drawht)
            {
                ifinkdc = false;
                double xval = (double)de.Value;
                for (int i = 0; i < m_KDCList.Count; i++)
                {
                    drawkdc = m_KDCList[i];
                    if (ZuoBiaoOper.IfInKeDuChi(xval, drawkdc))//假如棒线横向坐标值在刻度尺范围内则绘制棒线；
                    {
                        Layer.Layer_SetToCurrent(drawkdc.KName);
                        lrptstart = ZuoBiaoOper.UpdateLRStartPt(drawkdc.KDir, jdstruc.JDPtStart, lineRoadEnvironment.LineRoadWidth);
                        DrawStickLine(lrptstart, drawkdc, xval, (double)de.Key, jdtop);
                        ifinkdc = true;
                        break;
                    }

                }
                if (ifinkdc == false)//棒线横向坐标不再刻度吃范围内，添加超界标注；
                {
                    lrptstart = ZuoBiaoOper.UpdateLRStartPt(m_KDCList[kdccount - 1].KDir, jdstruc.JDPtStart, lineRoadEnvironment.LineRoadWidth);
                    if (lineItemStruct.LineItemChaoJie == CJQXChaoJie.BiaoZhu)//添加标注信息；
                    {
                        AddChaoJieBZ(xval, jdstruc.JDPtStart, (double)de.Key, jdstruc.JDtop, FrameDesign.ValueCoordinate, m_KDCList[kdccount - 1], lineRoadEnvironment.LineRoadWidth);
                    }
                }
            }
        }

        /// <summary>
        /// 绘制棒线
        /// </summary>
        /// <param name="kdc">棒线所属刻度尺</param>
        /// <param name="xVal">棒线终点对应的横向值（数据库中）</param>
        /// <param name="yVal">棒线终点对应的井深（数据库中）</param>
        private void DrawStickLine(LJJSPoint jdStartPt, KeDuChiItem kdc, double xVal, double yVal, double jdStartJS)
        {
            Layer.Layer_SetToCurrent(kdc.KName);
            ZuoBiaoOper zbopp = new ZuoBiaoOper(FrameDesign.ValueCoordinate);
            LJJSPoint ptend = zbopp.GetDrawingZuoBiaoPt(jdStartPt, kdc, xVal, yVal, jdStartJS, lineRoadEnvironment.LineRoadWidth);
            LJJSPoint ptstart = new LJJSPoint(jdStartPt.XValue, ptend.YValue);
            ulong objid = Line.BuildCommonLineByLayer(ptstart, ptend, kdc.kLineWidth);


        }
        private void AddDuiShuStickLineToFigure(KeDuChiItem duiShuKDC, JDStruc jdstruc, Hashtable drawht)
        {
            LJJSPoint lrptstart = ZuoBiaoOper.UpdateLRStartPt(duiShuKDC.KDir, jdstruc.JDPtStart, LineRoadEnvironment.LineRoadWidth);
            foreach (DictionaryEntry de in drawht)
            {
                double xval = (double)de.Value;
            
                DrawDuiShuStickLine(lrptstart, duiShuKDC, xval, (double)de.Key, jdstruc.JDtop);
            }

        }
        private void DrawDuiShuStickLine(LJJSPoint jdStartPt, KeDuChiItem kdc, double xVal, double yVal, double jdStartJS)
        {
         
            ZuoBiaoOper zbopp = new ZuoBiaoOper(FrameDesign.ValueCoordinate);
            double duishuX = DuiShuOper.XGetDSZuoBiaoValue(jdStartPt.XValue, kdc.KDir, kdc.KParm, xVal, kdc.KMin);
            double duishuY = ZuoBiaoOper.GetJSZongZBValue(jdStartPt.YValue, yVal, jdStartJS, FrameDesign.ValueCoordinate);
            LJJSPoint ptend = new LJJSPoint(duishuX, duishuY);
            LJJSPoint ptstart = new LJJSPoint(jdStartPt.XValue, ptend.YValue);
            ulong objid = Line.BuildCommonSoldLine(ptstart, ptend, Color.Black.ToArgb(), kdc.kLineWidth);
            //


        }
        
        public override void AddItemTitle()
        {
            
            CurveItemTitleClass title = new CurveItemTitleClass();
            title.showName = this.lineItemStruct.CurveItemShowName;
            title.isKDCShow = this.lineItemStruct.KDCIfShow;
            title.itemTitlePos = this.lineItemStruct.LineItemTitlePos;
            title.firstKDCStartHeigh = this.lineItemStruct.FirstKDCStartHeigh;
            title.curveItemUnit = this.lineItemStruct.LineItemUnit;
            title.curveUnitPos = this.lineItemStruct.UnitPosition;
            title.showNameVSKDCHeigh = this.lineItemStruct.LINameVSKDCHeigh;
            LJJSCAD.DrawingOper.AddItemTitle.AddLineItemTitleToFig(LineRoadStartPt, lineRoadEnvironment.LineRoadWidth, title);           
        }
     

        public override void SetItemStruct()
        {
            ItemDesignBlackBoardRead itemDesignBlackBoardRead = new ItemDesignBlackBoardRead(this.ID);
            lineItemStruct = (LineItemStruct)itemDesignBlackBoardRead.ReturnItemInstance(DrawItemStyle.LineItem);
        
        }
        //1210
        public override void BuildItemDrawData(List<JDStruc> jdStruc)
        {
            ItemWorkDataTableRead itemWorkDataTableRead = new ItemWorkDataTableRead(lineItemStruct.LineItemID);
            DataTable dt = (DataTable)itemWorkDataTableRead.ReturnItemInstance(DrawItemStyle.LineItem);

            DataColumn depthColumn = new DataColumn();

            if (lineItemStruct.LiSubClass == "jgl" || lineItemStruct.LiSubClass == "wxcs2" || lineItemStruct.LiSubClass == "wxcs" || lineItemStruct.LiSubClass == "msev" || lineItemStruct.LiSubClass == "mseh" || lineItemStruct.LiSubClass == "kxd" || lineItemStruct.LiSubClass == "kzx" || lineItemStruct.LiSubClass == "sxxs" || lineItemStruct.LiSubClass == "yd")
            {
                

                if (pTableContainer.itemDataTable == null)
                { 
                    GetPtable b = new GetPtable();
                    pTableContainer.itemDataTable = b.GetMseDesTable(dt);

                }
                /**
                else
                {
                    GetPtable b = new GetPtable();
                    pTableContainer.itemDataTable = b.GetMseDesTable(dt);
                }**/

                if (pTableContainer.itemDataTable != null && this.ItemDataTable == null)
                {
                    this.ItemDataTable = pTableContainer.itemDataTable;
                }
                



            }

            else
            {
                this.ItemDataTable = (DataTable)itemWorkDataTableRead.ReturnItemInstance(DrawItemStyle.LineItem);

            }
            int dtRowsCount = dt.Rows.Count;
            int idtRowCount = this.itemDataTable.Rows.Count;
            /**
            #region  //之前表中的depth没赋值， 给depth赋值

            for (int i = 0; i < this.itemDataTable.Rows.Count; i++)
            {
                this.itemDataTable.Rows[i][17] = dt.Rows[i][7].ToString();
            }
            **/
        }

        public override void InitOtherItemDesign()
        {
            m_KDCList = (List<KeDuChiItem>)KeDuChiManage.LineItemKDCHt[ItemName];
        }
    }
}
