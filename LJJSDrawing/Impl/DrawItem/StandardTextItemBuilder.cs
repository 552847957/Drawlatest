using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.Model.Drawing;
using LJJSCAD.DrawingOper;
using System.Data;
using LJJSCAD.Util;
using LJJSCAD.BlackBoard.Legend;
using LJJSCAD.Drawing.Manage;
using LJJSCAD.DrawingElement;
using LJJSCAD.LJJSDrawing.Factory;
using LJJSCAD.ItemStyleOper;
using DesignEnum;
namespace LJJSCAD.LJJSDrawing.Impl.DrawItem
{
    class StandardTextItemBuilder : DrawItemBuilder
    {
        private TxtItemClass textItemStruct;
        public override void AddPerJDDrawItem(JDStruc jdStruc, int index)
        {
            double jdtop = jdStruc.JDtop;//井段头
            double jdbottom = jdStruc.JDBottom;//井段底
            string txtitemid =textItemStruct.TxtItemName.Replace("_", "");//绘图项名称
            txtitemid = txtitemid.Replace(" ", "");//去掉空格

            List<TextItemDrawStruc> txtitemdrawinglist = GetJoinAdjustTextItemDrawStrucList(jdtop, jdbottom);

            Layer.CreateAndSetCurrentLayer(txtitemid, textItemStruct.TxtColor);//创建一个层

           AddTextItem addTextItem= AddPerJDTextItemFactory.CreateAddTextItemToFigureInstance(textItemStruct.TiStyle);
           addTextItem.textItemStruct = textItemStruct;
           addTextItem.lineRoadWidth = lineRoadEnvironment.LineRoadWidth;
           addTextItem.AddTextItemToFigure(jdStruc, txtitemdrawinglist);

         
        }
        /// <summary>
        /// 方法：返回一个文本结构体的泛型
        /// </summary>
        /// <param name="jdTop">参数:井顶</param>
        /// <param name="jdBottom">参数:井底</param>
        /// <returns></returns>
        private List<TextItemDrawStruc> GetJoinAdjustTextItemDrawStrucList(double jdTop, double jdBottom)
        {

            List<TextItemDrawStruc> relist = new List<TextItemDrawStruc>();//将用于返回值
            double depthtop, depthbottom = jdBottom;

            string sqlTxt = GetJoinAdjustTxtItemSqlTxt(jdTop.ToString(), jdBottom.ToString());//通过井顶和井底参数返回一个sql语句用于查询            
            string tmpstr = "";
            if (sqlTxt == "")
                return relist;

            //查询获取所需的内容,此时已经将数据提取出来

            DataRow[] drs = ItemDataTable.Select(sqlTxt, textItemStruct.TxtJDTop + " ASC");

            string[] tifieldarr = textItemStruct.FromFieldName.Split(';');//拆解字段形成一个数组，该数组内的第0个内容非常特殊，保存的是所用数据的内容
            string fieldname = "";

            if (drs.Length < 1)//如果没有取到数据，退出
            {
                return relist;
            }
            //该部分算法说明:通过循环将上一个记录的井顶做为井底,然后将一个井段按照小段做成一个泛型
            //该部分获取的是一个井段内以某个高度段为基准进行的分段
            foreach (DataRow dr in drs)
            {

                depthtop = StrUtil.StrToDouble(dr[textItemStruct.TxtJDTop].ToString(), "绘图数据缺少顶部数据", "顶部数据为非数值型");
                if (depthtop < jdTop)
                {
                    depthtop = jdTop;
                }
                //对于depthtop的处理
                if (textItemStruct.Depthstyle == DepthFieldStyle.TopAndBottom)//顶底型处理
                {
                    depthbottom = StrUtil.StrToDouble(dr[textItemStruct.TxtJDBottom].ToString(), "绘图数据缺少底部数据", "底数据为非数值型");
                }
                else if (textItemStruct.Depthstyle == DepthFieldStyle.TopAndHeigh)//顶深型处理
                {
                    depthbottom = depthtop + StrUtil.StrToDouble(dr[textItemStruct.TxtJDHeigh].ToString(), "绘图数据缺少厚度数据", "厚度数据为非数值型");
                }
                else if (textItemStruct.Depthstyle == DepthFieldStyle.Top)//仅头型,直接赋值
                {
                    depthbottom = depthtop;
                }
                //当depthbottom超界进行赋值处理
                if (depthbottom > jdBottom)
                {
                    depthbottom = jdBottom;
                }
                if (tifieldarr.Length > 0)//字段集合长度大于0,也就是存在字段,则取出首字段赋值
                {
                    tmpstr = "";
                    fieldname = tifieldarr[0].Trim();
                    if (fieldname != "")
                    {
                        tmpstr = dr[fieldname].ToString().Trim();//该段内容的值
                    }
                }
                if (tmpstr != "00")
                {
                    relist.Add(new TextItemDrawStruc(depthtop, depthbottom, tmpstr));//添加新结构体
                    LegendManage.UpdateTxtLegendLst(fieldname, textItemStruct.FromFieldName, tmpstr);
                }
            }
            return relist;
        }

        /// <summary>
        /// 私有函数:通过井顶和井底数据返回一个sql语句,
        /// </summary>
        /// <param name="jTop"></param>
        /// <param name="jBottom"></param>
        /// <returns></returns>
        private string GetJoinAdjustTxtItemSqlTxt(string jTop, string jBottom)
        {

            string restr = "";

            if (textItemStruct.TxtJDTop != "")
            {
                if (textItemStruct.TxtJDBottom != "")//条件2:获取的井底和井底的要求
                {
                    restr = restr + textItemStruct.TxtJDBottom + ">" + jTop + " and " + textItemStruct.TxtJDTop + "<" + jBottom;
                }
                else if (textItemStruct.TxtJDHeigh != "")//条件3:井段顶+井段深度必须大于井段顶高度
                {
                    restr = restr + " and (" + textItemStruct.TxtJDTop + @"+" + textItemStruct.TxtJDHeigh + ")" + ">" + jTop + " and " + textItemStruct.TxtJDTop + "<" + jBottom;
                }
                else
                {
                    //条件4:井段头字段大于数值jTop,同时井段头字段还要小于数值jBottom
                    restr = restr + " and " + textItemStruct.TxtJDTop + ">" + jTop + " and " + textItemStruct.TxtJDTop + "<" + jBottom;
                }
                //最后按升序的井段头排序
                //  restr = restr + @" order by  " + textItemStruct.TxtJDTop + " ASC";
            }


            return restr;//返回sql字符串
        }
     
   

 

        public override void AddItemTitle()
        {
            LJJSCAD.DrawingOper.AddItemTitle.AddNormalItemTitleText(this.LineRoadStartPt, textItemStruct.TxtItemName, textItemStruct.TxtItemTitlePos, this.lineRoadEnvironment.LineRoadWidth, textItemStruct.TxtHeaderStartheigh, textItemStruct.TxtItemTitleSpace);
           
        }

        public override void SetItemStruct()
        {
            ItemDesignBlackBoardRead itemDesignBlackBoardRead = new ItemDesignBlackBoardRead(this.ID);
            textItemStruct = (TxtItemClass)itemDesignBlackBoardRead.ReturnItemInstance(DrawItemStyle.TextItem);
          //  textItemStruct = (TxtItemClass)ItemDesignBlackBoardRead.GetItemFromBlackBoard(this.ID, DrawItemStyle.TextItem);
        }

        public override void BuildItemDrawData(List<JDStruc> jdStruc)
        {

            ItemWorkDataTableRead itemWorkDataTableRead = new ItemWorkDataTableRead(this.ID);
            
           ItemDataTable=(DataTable)itemWorkDataTableRead.ReturnItemInstance(DrawItemStyle.TextItem);
           ItemDataTable.TableName = "g437";
           
           ItemDataTable.WriteXml("ceshi",XmlWriteMode.WriteSchema,true);
           DataTable dt = new DataTable();
           dt.TableName = "g437";
        //   dt.ReadXmlSchema("ceshi");
           dt.ReadXml("ceshi");
        }

        public override void InitOtherItemDesign()
        {
            
        }
       
    }
}
