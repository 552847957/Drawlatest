using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.Model
{
    class JingShenModel
    {
        public string ID { set; get; }	//井深设计的ID
        public string MainKDLength { set; get; }	//主刻度线的绘制长度
        public string MainKDSpace { set; get; }	//主刻度线间距，即间隔多少毫米绘制一条主刻度线；
        public string BZTxtHeigh { set; get; }	//刻度标注的高度；
        public string ifDefault { set; get; }	//是否为默认的井深线道设置；
        public string BZTxtFont { set; get; }	//刻度标注的字体；
        public string isLeftMainKDShow { set; get; }	//左侧主刻度线是否显示
        public string isRightMainKDShow { set; get; }	//右侧主刻度线是否显示



    }
}
