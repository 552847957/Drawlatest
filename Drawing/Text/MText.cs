using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using LJJSCAD.Util;
using LJJSCAD.CommonData;
using VectorDraw.Geometry;
using DesignEnum;

namespace LJJSCAD.Drawing.Text
{
    class MText
    {
        public static ulong AddMText(string txtcontent, string txtStyle, LJJSPoint insPostion, AttachmentPoint txtlayoutstytle, string fontFile, double TxtAreaWidth, double lineSpaceFactor)
        {
            return 1;
        }
        /// <summary>
        /// 添加横排文字，集中式，不需要整行文字的宽度；
        /// </summary>
        /// <param name="txtcontent"></param>
        /// <param name="txtheight"></param>
        /// <param name="postion"></param>
        /// <param name="txtlayoutstytle"></param>
        /// <param name="txtstyle"></param>
        /// <returns></returns>
        public static ulong AddHengPaiMText(string txtcontent, double txtheight, LJJSPoint postion, AttachmentPoint txtlayoutstytle, string txtstyle)
        {

            return VectorDrawHelper.AddMText(DrawCommonData.activeDocument, txtheight, txtstyle, txtcontent, new gPoint(postion.XValue, postion.YValue), VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerBottom, VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter, "");
            
        }
        /// <summary>
        /// 添加横排文字，分散式，需要整行文字的所占的宽度；
        /// </summary>
        /// <param name="txtcontent"></param>
        /// <param name="txtheight"></param>
        /// <param name="postion"></param>
        /// <param name="txtlayoutstytle"></param>
        /// <param name="txtstyle"></param>
        /// <returns></returns>
        public static ulong AddHengPaiMTextHaveWidth(string txtcontent, double txtheight, LJJSPoint postion, AttachmentPoint txtlayoutstytle, string txtstyle,double horAreaWidth)
        {
            string textstring ="\\T2;"+txtcontent;
            return VectorDrawHelper.AddMText(DrawCommonData.activeDocument, txtheight, txtstyle, txtcontent, new gPoint(postion.XValue, postion.YValue), VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerBottom, VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter, "");
        
        }
        public static ulong AddZongPaiMText(string txtcontent, double txtheight, LJJSPoint postion, AttachmentPoint txtlayoutstytle, string txtstyle)
        {
            return VectorDrawHelper.AddZongPaiMText(DrawCommonData.activeDocument, txtheight, txtstyle, txtcontent, new gPoint(postion.XValue, postion.YValue), VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen, VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter, "",0);
        }
        public static ulong AddZongPaiMTextHaveHeigh(string txtcontent, double txtheight, LJJSPoint postion, AttachmentPoint txtlayoutstytle, string txtstyle,double verAreaHeight)
        {

            return VectorDrawHelper.AddZongPaiMText(DrawCommonData.activeDocument, txtheight, txtstyle, txtcontent, new gPoint(postion.XValue, postion.YValue), VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen, VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter, "", verAreaHeight);
        }

    }
}
