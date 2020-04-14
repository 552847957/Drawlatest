using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Model;
using LJJSCAD.DrawingDesign.Frame;
using System.Data;
using LJJSCAD.DrawingElement;
using DesignEnum;
using LJJSCAD.LJJSDrawing.Impl;
using LJJSCAD.LJJSDrawing.Impl.DrawItem;

namespace LJJSCAD.LJJSDrawing.Interface.DrawItemInterface
{
    abstract  class DrawItemBuilder
    {

        private string iD;

        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private string itemName;

        protected string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }
        private DrawItemStyle buildItemStyle;

        public DrawItemStyle BuildItemStyle
        {
            get { return buildItemStyle; }
            set { buildItemStyle = value; }
        }
        private string buildItemSubType;

        public string BuildItemSubType
        {
            get { return buildItemSubType; }
            set { buildItemSubType = value; }
        }
        private LJJSPoint lineRoadStartPt;

        internal LJJSPoint LineRoadStartPt
        {
            get { return lineRoadStartPt; }
            set { lineRoadStartPt = value; }
        }
        protected LineRoadEnvironment lineRoadEnvironment;

        public LineRoadEnvironment LineRoadEnvironment
        {
            get { return lineRoadEnvironment; }
            set { lineRoadEnvironment = value; }
        }
        private double BLCValue;
        private string JH;
        /// <summary>
        /// 绘图项所需要的绘图数据；
        /// </summary>
        protected DataTable itemDataTable;

        public DataTable ItemDataTable
        {
            get { return itemDataTable; }
            set { itemDataTable = value; }
        }

        public void SetLineRoadEnvironment(LineRoadEnvironment lineRoadEnvironment)
        {
            this.lineRoadEnvironment = lineRoadEnvironment;
            if (null != this.lineRoadEnvironment && this.lineRoadEnvironment.JdDrawLst.Count() > 0)
            {
                this.lineRoadStartPt = this.lineRoadEnvironment.JdDrawLst[0].JDPtStart;
            }
        }
        public void InitData(DrawItemName drawItemName)
        {
            BLCValue = FrameDesign.ValueCoordinate;
            JH = FrameDesign.JH;
            ID = drawItemName.DrawItemID;
            BuildItemSubType = drawItemName.ItemSubStyle;
            itemName = drawItemName.DrawItemShowName.Trim();

        }
       public abstract void SetItemStruct();
       public abstract void InitOtherItemDesign();
       //public int? JD_Count;
       //public DrawPointContainer dpc;
        public void DrawItemBody()
        {

            if (null != lineRoadEnvironment.JdDrawLst && lineRoadEnvironment.JdDrawLst.Count()>0)
            {
                BuildItemDrawData(lineRoadEnvironment.JdDrawLst);   //在StandardCurveItemBuilder里

                //this.itemDataTable 是datatable，有depth的值
                //sucker
                
                if(DrawPointContainer.list == null)
                {
                   
                    DrawPointContainer.set_list(lineRoadEnvironment.JdDrawLst.Count());
                    

                }
                for (int i = 0; i < lineRoadEnvironment.JdDrawLst.Count(); i++)
                {                   
                    //初始化itemtable  significant
                    //pTableContainer.initiate();
                    //DrawPointContainer.initiate();
                    AddPerJDDrawItem(lineRoadEnvironment.JdDrawLst[i],i);
                }
            }

        }
       public abstract  void AddPerJDDrawItem(JDStruc jdStruc,int index);
       public abstract void AddItemTitle();
        /// <summary>
        /// 构建绘图项绘图所需要的业务数据；
        /// </summary>
        /// <param name="jdStruc"></param>
       public abstract void BuildItemDrawData(List<JDStruc> jdStruc);
        
    }
}
