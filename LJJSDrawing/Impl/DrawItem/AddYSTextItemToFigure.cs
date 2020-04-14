using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.DrawingElement;
using LJJSCAD.Util;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.Drawing.Text;
using LJJSCAD.Drawing.Curve;
using LJJSCAD.CommonData;
using DesignEnum;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem
{
    class AddYSTextItemToFigure : AddTextItem
    {
        #region IAddTextItem 成员

        public override List<ulong> AddTextItemToFigure(JDStruc jdstruc, List<TextItemDrawStruc> textItemDrawStrucCol)
        {
            List<TextItemDrawStruc> yshebinglist = GetHeBingTiYSList(textItemDrawStrucCol);//获取新的泛型

            double lasthd = 10;
            LJJSPoint lastbottomzbpt = jdstruc.JDPtStart;//井段内的井段起始点
            AttachmentPoint bzpos = AttachmentPoint.MiddleCenter;//定义文本项位置
            LJJSPoint inserttxtpos;//插入文本项位置

            double txtoffset = 2;

            double txtwidth = lineRoadWidth - 2 * txtoffset;

            for (int i = 0; i < yshebinglist.Count; i++)
            {
                TextItemDrawStruc currtidrawstruc = yshebinglist[i];
                LJJSPoint titopZuoBiaoPt = ZuoBiaoOper.GetJSZuoBiaoPt(jdstruc.JDPtStart, currtidrawstruc.depthtop, jdstruc.JDtop, FrameDesign.ValueCoordinate);
                LJJSPoint tibottomZuoBiaoPt = ZuoBiaoOper.GetJSZuoBiaoPt(jdstruc.JDPtStart, currtidrawstruc.depthbottom, jdstruc.JDtop, FrameDesign.ValueCoordinate);


                if (Math.Abs(titopZuoBiaoPt.YValue - tibottomZuoBiaoPt.YValue) > textItemStruct.TxtSize * 1.2)
                {
                    bzpos = AttachmentPoint.MiddleCenter;

                    inserttxtpos = new LJJSPoint(titopZuoBiaoPt.XValue + lineRoadWidth * 0.5, (tibottomZuoBiaoPt.YValue+ titopZuoBiaoPt.YValue) * 0.5);
                }
                else
                {
                    if (bzpos == AttachmentPoint.MiddleLeft)
                    {
                        bzpos = AttachmentPoint.MiddleRight;
                        inserttxtpos = PointUtil.GetRightInsertTxtPt(titopZuoBiaoPt, tibottomZuoBiaoPt, 3, lineRoadWidth);
                    }
                    else
                    {
                        bzpos = AttachmentPoint.MiddleLeft;
                        inserttxtpos = PointUtil.GetLeftInsertTxtPt(titopZuoBiaoPt, tibottomZuoBiaoPt, 3);
                    }

                }
                MText.AddMText(currtidrawstruc.textcontent.Trim(), "", inserttxtpos, bzpos, textItemStruct.TxtFont, txtwidth + 0.5, 0.6);

               // MText.AddMText(currtidrawstruc.textcontent.Trim(), textItemStruct.TxtSize, inserttxtpos, bzpos, textItemStruct.TxtFont, txtwidth + 0.5, 0.6);
                if (Math.Abs(lastbottomzbpt.YValue - titopZuoBiaoPt.YValue) > 0.001)//是起头
                {
                    lasthd = 10;
                    if (lasthd < textItemStruct.TxtSize * 1.2)

                        BiaoZhuLineUtil.AddHorBZLine(lastbottomzbpt, txtoffset, lineRoadWidth, 0);//画上段剖面的终点的标注线;
                    else
                        Line.BuildCommonHorSolidLineByLayer(lastbottomzbpt, lineRoadWidth, 0, DrawCommonData.DirectionRight);

                }
                if (Math.Abs(titopZuoBiaoPt.YValue - tibottomZuoBiaoPt.YValue) < textItemStruct.TxtSize * 1.2 || lasthd < textItemStruct.TxtSize * 1.2)
                {
                    BiaoZhuLineUtil.AddHorBZLine(titopZuoBiaoPt, txtoffset, lineRoadWidth, 0);
                }
                else
                {
                    Line.BuildCommonHorSolidLineByLayer(titopZuoBiaoPt, lineRoadWidth, 0, DrawCommonData.DirectionRight);
                }
                lastbottomzbpt = tibottomZuoBiaoPt;
                lasthd = Math.Abs(titopZuoBiaoPt.YValue - tibottomZuoBiaoPt.YValue);
            }
            Line.BuildCommonHorSolidLineByLayer(lastbottomzbpt, lineRoadWidth, 0, DrawCommonData.DirectionRight);
            return new List<ulong>();
        }
        #endregion
        /// <summary>
        /// 获得合并颜色项
        /// </summary>
        /// <param name="textItemDrawStrucCol"></param>
        /// <returns></returns>
        private List<TextItemDrawStruc> GetHeBingTiYSList(List<TextItemDrawStruc> textItemDrawStrucCol)
        {


            List<TextItemDrawStruc> yshebinglist = new List<TextItemDrawStruc>();//定义新的泛型

            int ticount = textItemDrawStrucCol.Count;//获取原来小段泛型的个数

            for (int i = 0; i < ticount; i++)
            {
                TextItemDrawStruc tmptids = textItemDrawStrucCol[i];//小段泛型的第i个结构体
                string tidscontent = tmptids.textcontent.Trim();//该文本项绘图文字的内容

                //条件1:i+1小于ticount，条件2:第i个井底厚度和第i+1个井底厚度相差小于0.001时
                while ((i + 1) < ticount && (Math.Abs(textItemDrawStrucCol[i].depthbottom - textItemDrawStrucCol[i + 1].depthtop) < 0.001))
                {
                    if (tidscontent.Equals(textItemDrawStrucCol[i + 1].textcontent.Trim()))
                    {
                        tmptids.depthbottom = textItemDrawStrucCol[i + 1].depthbottom;
                        i = i + 1;
                    }
                    else
                    {
                        break;
                    }
                }

                yshebinglist.Add(tmptids);
            }
            return yshebinglist;
        }



    }
}
