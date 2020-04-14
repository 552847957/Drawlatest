using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Util;
using DesignEnum;

namespace LJJSCAD.Model.Drawing
{
    class JingShenDesignClass
    {
        public string ID { set; get; }	//井深设计的ID
        public double MainKDLength { set; get; }	//主刻度线的绘制长度
        public int MainKDSpace { set; get; }	//主刻度线间距，即间隔多少毫米绘制一条主刻度线；
        public double BZTxtHeigh { set; get; }	//刻度标注的高度；
        public string BZTxtFont { set; get; }	//刻度标注的字体；
        public bool isLeftMainKDShow { set; get; }	//左侧主刻度线是否显示
        public bool isRightMainKDShow { set; get; }	//右侧主刻度线是否显示
        public int BiaoZhuSpace { set; get; }//间隔多少主刻度标注一次；例如，每两个主刻度标注一次；

        public JingShenDesignClass(JingShenModel jingShenModel)
        {
            ID = jingShenModel.ID.Trim();
            MainKDLength = StrUtil.StrToDouble(jingShenModel.MainKDLength, 5, "井深的主刻度设计为非数值型");
            MainKDSpace = StrUtil.StrToInt(jingShenModel.MainKDSpace,5,"井深主刻度间隔设计为非数值型");
            BZTxtHeigh = StrUtil.StrToDouble(jingShenModel.BZTxtHeigh, 2, "井深刻度标注文字高度为非数值型");
            BZTxtFont = jingShenModel.BZTxtFont;
            isLeftMainKDShow =BoolUtil.GetBoolByBindID(jingShenModel.isLeftMainKDShow, true);// BoolUtil.GetBoolBySFStr(jingShenModel.isLeftMainKDShow,false);
            isRightMainKDShow = BoolUtil.GetBoolByBindID(jingShenModel.isRightMainKDShow, false);// BoolUtil.GetBoolBySFStr(jingShenModel.isRightMainKDShow,false);
            BiaoZhuSpace = 1;
            
        }

    }
}
