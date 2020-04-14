using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.Model.Drawing;
using LJJSCAD.DrawingOper;
using LJJSCAD.DrawingElement;
using LJJSCAD.Util;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.Drawing.Hatch;
using LJJSCAD.CommonData;
using LJJSCAD.ItemStyleOper;
using System.Data;
using DesignEnum;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem
{
    class StandardImageItemBuilder : DrawItemBuilder
    {
        private ImageItemDesignClass imageItemDesginStruc;
        public override void SetItemStruct()
        {
            ItemDesignBlackBoardRead itemDesign = new ItemDesignBlackBoardRead(this.ID);
            imageItemDesginStruc = (ImageItemDesignClass)itemDesign.ReturnItemInstance(DrawItemStyle.ImageItem);
         
        }

        public override void InitOtherItemDesign()
        {
           
        }

        public override void AddPerJDDrawItem(JDStruc jdStruc, int index)
        {

            ImageItemBuilderOper imageItemBuilderOper = new ImageItemBuilderOper();
            imageItemBuilderOper.ImageItemDesignStruc = imageItemDesginStruc;
            List<ImageItemStruc> imagestruclist = imageItemBuilderOper.GetImageItemPerJDDrawData(jdStruc, ItemDataTable);
            AddStandardImageItemToFigure(jdStruc, imagestruclist);


        }

        private void AddStandardImageItemToFigure(JDStruc jdStruc, List<ImageItemStruc> imageStrucList)
        {
            for (int i = 0; i < imageStrucList.Count(); i++)
            {
                ImageItemStruc tmpImageItemStruc = imageStrucList[i];
                LJJSPoint imageBottomZuoBiaoPt = ZuoBiaoOper.GetJSZuoBiaoPt(jdStruc.JDPtStart, tmpImageItemStruc.BottomJS, jdStruc.JDtop, FrameDesign.ValueCoordinate);
                double imageDrawingHeigh = tmpImageItemStruc.JsHeight*1000 * FrameDesign.ValueCoordinate;
                ImageHatch.AddRectImageHatch(imageBottomZuoBiaoPt, imageDrawingHeigh, this.lineRoadEnvironment.LineRoadWidth, tmpImageItemStruc.ImagePath, DrawCommonData.HatchScale, tmpImageItemStruc.AdditionImeLst);

            }
            
        }

        public override void AddItemTitle()
        {
            LJJSCAD.DrawingOper.AddItemTitle.AddNormalItemTitleText(this.LineRoadStartPt, imageItemDesginStruc.ItemName, imageItemDesginStruc.TitleHorPostion, this.lineRoadEnvironment.LineRoadWidth, imageItemDesginStruc.TitleTxtStartHeigh, 5);
            
        }

        public override void BuildItemDrawData(List<JDStruc> jdStruc)
        {

            ItemWorkDataTableRead itemWorkDataTableRead = new ItemWorkDataTableRead(imageItemDesginStruc.ID);
            ItemDataTable = (DataTable)itemWorkDataTableRead.ReturnItemInstance(DrawItemStyle.ImageItem);
        }
    }
}
