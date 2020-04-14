using System.Drawing;
using System.Collections.Generic;
using LJJSCAD.DrawingElement;
using DesignEnum;
using EnumManage;
/// <summary>
/// 该文件主要保存，绘图过程中需要操作类型的结构体；例如井段
/// </summary>
 public struct JDStruc
{
    public LJJSPoint JDPtStart;//井段在绘图区域上的起点，以左边为准；
    public double JDHeight;//井段的绘图高度；
    public double JDtop;//井段顶部的深度；
    public double JDBottom;//井段底部的深度；
}
 struct JDTopAndBottom
 {
     public double JDTop;
     public double JDBottm;
 }
 struct AttachmentPointAdapter
 {
   public  VectorDraw.Professional.Constants.VdConstHorJust HorJust;
   public  VectorDraw.Professional.Constants.VdConstVerJust verJust;
 }

 struct DrawItemKey
 {
     public string DrawItemID;
     public DrawItemStyle ItemStyle;//该绘图项的分类类型
     public DrawItemKey(string drawItemID, DrawItemStyle drawItemStyle)
     {
         this.DrawItemID = drawItemID;
         this.ItemStyle = drawItemStyle;
     }

 }
struct DrawItemName
{
    public string DrawItemShowName;//该绘图项的名称
    public string DrawItemID;
    public DrawItemStyle ItemStyle;//该绘图项的分类类型
    public string ItemSubStyle;//绘图项分类的子类型;
    //构造函数
    public DrawItemName(string drawItemID, string drawItemShowName, DrawItemStyle itemStyle, string itemSubStyle)
    {
        DrawItemID = drawItemID.Trim();
        DrawItemShowName = drawItemShowName.Trim();
        ItemStyle = itemStyle;
        ItemSubStyle = itemSubStyle;
    }
}
struct LineRoadControlData
{
    public string LineRoadId;
    public List<JDStruc> LineRoadJDStructLst;
    public double LineRoadWidth;
}

/// <summary>
/// 结构体:用于存储层网信息的结构体
/// </summary>
struct LineRoadCeWang
{
    public bool ifAdd;//是否添加层网
    public int cewangsepnum;//份数；测网绘制几条线；
    public double cewangfixlen;//测网固长；
    public CeWangStyleEnum cewangstyle;// 等分;等差;对数正向，对数反向；
    public double duishuminvalue;//对数测网最小值；
    public double duishuParam;//对数测网系数，单位ｍｍ；
    public bool ifHeng;//是否画横向测网；
    public bool ifZong;//是否画纵向测网；

    /// <summary>
    /// 测网的构造函数，在初始时候定义是否添加层网
    /// </summary>
    /// <param name="ifadd"></param>
    public  LineRoadCeWang(bool ifadd)
    {
        ifAdd = ifadd;
        cewangfixlen = 0;
        cewangsepnum = 0;
        duishuminvalue = 1;
        duishuParam = 35;
        cewangstyle = CeWangStyleEnum.None;
        ifHeng = false;
        ifZong = false;
    }
    
}
public struct DrawDirection
{
    public int HorDirection;//水平方向;向右为1;向左-1
    /// <summary>
    /// 垂直方向;向上1;向下-1;
    /// </summary>
    public int VerDirection;
    public DrawDirection(int horDirect,int verDirect)
    {
        HorDirection = horDirect;
        VerDirection = verDirect;
    }
}
/// <summary>
/// 结构体:文本项结构体
/// </summary>
struct TextItemDrawStruc
{
    public double depthtop;//井顶
    public double depthbottom;//井底
    public string textcontent;//井段文字
    public TextItemDrawStruc(double depthTop, double depthBottom, string txtContent)
    {
        depthtop = depthTop;
        depthbottom = depthBottom;
        textcontent = txtContent;
    }
}