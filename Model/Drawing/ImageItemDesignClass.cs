using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Util;
using LJJSCAD.DrawingElement;
using LJJSCAD.DrawingOper;
using DesignEnum;

namespace LJJSCAD.Model.Drawing
{
    class ImageItemDesignClass
    {
        public string ID { set; get; }
        public string ItemName { set; get; }
        public string ImageFromField { set; get; }
        public string ImageJDTopField { set; get; }
        public string ImageJDBottomField { set; get; }
        public string ImageJDHeighField { set; get; }
        public DepthFieldStyle ImageJDStyle { set; get; }
        public string ItemFromTable { set; get; }
        public string ImageItemSubStyle { set; get; }
        public int ImageItemOrder { set; get; }
        public ItemTitlePos TitleHorPostion { set; get; }
        public double TitleTxtStartHeigh { set; get; }
        public string ImageAdditionOne { set; get; }//附加图片1路径
        public string ImageAdditionTwo { set; get; }//附加图片2路径
        public string ImageAdditionThree { set; get; }//附加图片3路径
        public string ImageAdditionFour { set; get; }//附加图片4路径
        public string ImageAdditionFive { set; get; }
        public string ImageAdditionSix { set; get; }
        public string FormStyle { set; get; }//附加图片的类型；

        public ImageItemDesignClass(ImageItemModel imageItemModel)
        {
            // TODO: Complete member initialization
            this.ID = imageItemModel.ID.Trim();
            this.ImageFromField = imageItemModel.ImageFromField.Trim();
            this.ImageItemOrder = StrUtil.StrToInt(imageItemModel.ImageItemOrder,0, "图片项序号为非整数型");
            this.ImageItemSubStyle =imageItemModel.ImageItemSubStyle.Trim();
            this.ImageJDBottomField = imageItemModel.ImageJDBottomField.Trim();
            this.ImageJDHeighField = imageItemModel.ImageJDHeighField.Trim();
            this.ImageJDTopField = imageItemModel.ImageJDTopField.Trim();
            this.ItemFromTable = imageItemModel.ItemFromTable.Trim();
            this.ItemName = imageItemModel.ItemName.Trim();
            this.ImageJDStyle = ZuoBiaoOper.GetDepthFieldStyle(imageItemModel.ImageJDStyle.Trim());
            this.TitleHorPostion = EnumUtil.GetEnumByStr(imageItemModel.TitleHorPostion, ItemTitlePos.Mid); //ItemOper.GetDrawingItemTitlePos(imageItemModel.TitleHorPostion.Trim());
            this.TitleTxtStartHeigh = StrUtil.StrToDouble(imageItemModel.TitleTxtStartHeigh.Trim(),4,"绘图项标题高度为非数值型");
            this.ImageAdditionOne = imageItemModel.ImageAdditionOne;
            this.ImageAdditionTwo = imageItemModel.ImageAdditionTwo;
            this.ImageAdditionThree = imageItemModel.ImageAdditionThree;
            this.ImageAdditionFour = imageItemModel.ImageAdditionFour;
            this.ImageAdditionFive = imageItemModel.ImageAdditionFive;
            this.ImageAdditionSix = imageItemModel.ImageAdditionSix;
            this.FormStyle = imageItemModel.FormStyle;        


        }

    }
    struct ImageItemStruc
    {
        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }
        private double _bottomJS; //左侧，底部井深值；

        public double BottomJS
        {
            get { return _bottomJS; }
            set { _bottomJS = value; }
        }
        private double _jsHeight;//图片高度;

        public double JsHeight
        {
            get { return _jsHeight; }
            set { _jsHeight = value; }
        }
        private List<StrValueProperty> _additionImeLst;

        public List<StrValueProperty> AdditionImeLst
        {
            get { return _additionImeLst; }
            set { _additionImeLst = value; }
        }

        public ImageItemStruc(string imagePath,double bottomJs,double jsHeight, List<StrValueProperty> additionImeLst)
        {
            _imagePath = imagePath;
            _bottomJS = bottomJs;
            _jsHeight = jsHeight;
            _additionImeLst = additionImeLst;
        }
    }

}
