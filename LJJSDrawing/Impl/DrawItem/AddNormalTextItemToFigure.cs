using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface.DrawItemInterface;
using LJJSCAD.Util;
using LJJSCAD.DrawingElement;
using LJJSCAD.DrawingDesign.Frame;
using LJJSCAD.DrawingOper;
using LJJSCAD.Drawing.Text;
using DesignEnum;

namespace LJJSCAD.LJJSDrawing.Impl.DrawItem
{
    class AddNormalTextItemToFigure : AddTextItem
    {

        #region IAddTextItem 成员

        public override List<ulong> AddTextItemToFigure(JDStruc jdstruc, List<TextItemDrawStruc> textItemDrawStrucCol)
        {
            for (int i = 0; i < textItemDrawStrucCol.Count; i++)
            {
                TextItemDrawStruc currtidrawstruc = textItemDrawStrucCol[i];
                LJJSPoint titopZuoBiaoPt = ZuoBiaoOper.GetJSZuoBiaoPt(jdstruc.JDPtStart
                    , currtidrawstruc.depthtop, jdstruc.JDtop, FrameDesign.ValueCoordinate);//获取小段顶坐标

                LJJSPoint tibottomZuoBiaoPt = ZuoBiaoOper.GetJSZuoBiaoPt(jdstruc.JDPtStart
                    , currtidrawstruc.depthbottom, jdstruc.JDtop, FrameDesign.ValueCoordinate);//获取小段底坐标

                //如果是横向排列则用线道宽度,如果是纵向排列需要分析小井段厚度
                AddTxtItemDrawStrucToFigure(titopZuoBiaoPt, tibottomZuoBiaoPt
                    , currtidrawstruc, lineRoadWidth, Math.Abs(titopZuoBiaoPt.YValue - tibottomZuoBiaoPt.YValue), textItemStruct, lineRoadWidth);

                if (textItemStruct.TextOutFrame == TxtItemOutFrame.DoubleParallel)//如果类型为双平行线类型则绘制双平行线
                {
                    TextItemOper.AddTiOutFrameParelLine(titopZuoBiaoPt, tibottomZuoBiaoPt,textItemStruct.TIOffset,textItemStruct.TxtOutFrameWidth);
                }
            }
            return new List<ulong>();
        }

        #endregion
        /// <summary>
        /// 私有方法:隶属于AddNormalTxtItemToFigure,根据某一个小段的具体内容绘制数据
        /// </summary>
        /// <param name="topZbPt">参数:小段顶坐标</param>
        /// <param name="bottomZbPt">参数:小段底坐标</param>
        /// <param name="tiDrawstruc">参数:文本项信息结构体</param>
        /// <param name="HorSpace"></param>
        /// <param name="VerSpace"></param>
        private void AddTxtItemDrawStrucToFigure(LJJSPoint topZbPt, LJJSPoint bottomZbPt, TextItemDrawStruc tiDrawstruc, double HorSpace, double VerSpace, Model.Drawing.TxtItemClass textItemStruct,double lineRoadWidth)
        {
            LJJSPoint inserpos = GetTxtItemInsertPt(topZbPt, bottomZbPt, textItemStruct.TxtPosition, lineRoadWidth);//获取插入小段文字的位置
            double txtspace =textItemStruct.TxtSize * 1.2;//文字字体的格式
            string txtcontent = tiDrawstruc.textcontent.Trim();
            int txtcount = txtcontent.Length;//正常文字个数
            double verAreaHeight =Math.Abs(topZbPt.YValue - bottomZbPt.YValue);

            double txtspaceoffset = 0;

            //分析文字分散方式是集中还是分散
            if (textItemStruct.TxtDistribution == Txtdistribution.TxtSpread)//分散式
            {
                if (textItemStruct.TxtPaiLie == TxtArrangeStyle.TxtVerArrange)//横排排列
                {
                    txtspaceoffset = (VerSpace - txtcount * textItemStruct.TxtSize * 1.1) / (txtcount + 1);//横向距离处理
                }
                else//纵排排列
                {
                    txtspaceoffset = (HorSpace - txtcount * textItemStruct.TxtSize * 1.1) / (txtcount + 1);//纵向距离处理
                }
                if (txtspaceoffset > 0)
                {
                    txtspace = txtspace + txtspaceoffset;
                }
            }
            //分析文字排列方式是横排还是纵排
            if (textItemStruct.TxtPaiLie == TxtArrangeStyle.TxtHorArrange)
            {
                //绘制图形
                if (txtspaceoffset > 0)

                    MText.AddHengPaiMText(txtcontent, textItemStruct.TxtSize, inserpos, textItemStruct.TxtPosition, textItemStruct.TxtStrFont);
                else
                    MText.AddHengPaiMTextHaveWidth(txtcontent, textItemStruct.TxtSize, inserpos, textItemStruct.TxtPosition, textItemStruct.TxtStrFont, lineRoadWidth);
            }
            else
            {
                if (txtspaceoffset > 0)
                    MText.AddZongPaiMText(txtcontent, textItemStruct.TxtSize, inserpos, textItemStruct.TxtPosition, textItemStruct.TxtStrFont);
                else
                    MText.AddZongPaiMTextHaveHeigh(txtcontent, textItemStruct.TxtSize, inserpos, textItemStruct.TxtPosition, textItemStruct.TxtStrFont, verAreaHeight);

            }

        }
        /// <summary>
        /// 私有方法:通过传入的小段井顶和井底坐标返回插入文字项的位置坐标（Point3D）
        /// </summary>
        /// <param name="topZbPt">参数:小段井顶坐标</param>
        /// <param name="bottomZbPt">参数:小段井底坐标</param>
        /// <returns></returns>
        private LJJSPoint GetTxtItemInsertPt(LJJSPoint topZbPt, LJJSPoint bottomZbPt,AttachmentPoint txtAttachmentPoint,double lineRoadWidth)
        {
            LJJSPoint inserpos;
            double offset = 2;
            //根据不同的文字位置获取不同的坐标格式
            switch (txtAttachmentPoint)
            {
                case AttachmentPoint.TopLeft:
                    inserpos = new LJJSPoint(topZbPt.XValue + offset, topZbPt.YValue - offset);
                    break;
                case AttachmentPoint.TopRight:
                    inserpos = new LJJSPoint(topZbPt.XValue + lineRoadWidth - offset, topZbPt.YValue - offset);
                    break;
                case AttachmentPoint.TopCenter:
                    inserpos = new LJJSPoint(topZbPt.XValue + lineRoadWidth * 0.5, topZbPt.YValue - offset);
                    break;
                case AttachmentPoint.MiddleLeft:
                    inserpos = new LJJSPoint(topZbPt.XValue + offset, (topZbPt.YValue + bottomZbPt.YValue) * 0.5);
                    break;
                case AttachmentPoint.MiddleRight:
                    inserpos = new LJJSPoint(topZbPt.XValue + lineRoadWidth - offset, (topZbPt.YValue + bottomZbPt.YValue) * 0.5);
                    break;
                case AttachmentPoint.MiddleCenter:
                    inserpos = new LJJSPoint(topZbPt.XValue + lineRoadWidth * 0.5, (topZbPt.YValue + bottomZbPt.YValue) * 0.5);
                    break;
                case AttachmentPoint.BottomLeft:
                    inserpos = new LJJSPoint(bottomZbPt.XValue + offset, bottomZbPt.YValue + offset);
                    break;
                case AttachmentPoint.BottomCenter:
                    inserpos = new LJJSPoint(bottomZbPt.XValue + lineRoadWidth * 0.5, bottomZbPt.YValue + offset);
                    break;
                case AttachmentPoint.BottomRight:
                    inserpos = new LJJSPoint(bottomZbPt.XValue + lineRoadWidth - offset, bottomZbPt.YValue + offset);
                    break;
                default:
                    inserpos = new LJJSPoint(topZbPt.XValue + lineRoadWidth * 0.5, (topZbPt.YValue + bottomZbPt.YValue) * 0.5);
                    break;
            }
            return inserpos;

        }
    }
}
