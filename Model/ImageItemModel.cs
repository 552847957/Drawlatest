using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.Model
{
    class ImageItemModel
    {
        public string ID { set; get; }
        public string ItemName { set; get; }
        public string ImageFromField { set; get; }
        public string ImageJDTopField { set; get; }
        public string ImageJDBottomField { set; get; }
        public string ImageJDHeighField { set; get; }
        public string ImageJDStyle { set; get; }
        public string ItemFromTable { set; get; }
        public string ImageItemSubStyle { set; get; }
        public string ImageItemOrder { set; get; }
        public string TitleHorPostion { set; get; }
        public string TitleTxtStartHeigh { set; get; }
        public string ImageAdditionOne { set; get; }//附加图片1路径
        public string ImageAdditionTwo { set; get; }//附加图片2路径
        public string ImageAdditionThree { set; get; }//附加图片3路径
        public string ImageAdditionFour { set; get; }//附加图片4路径
        public string ImageAdditionFive { set; get; }//附加图片3路径
        public string ImageAdditionSix { set; get; }//附加图片4路径

        public string FormStyle { set; get; }//附加图片的类型；


    }
}
