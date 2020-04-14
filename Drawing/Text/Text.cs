using System;
using System.Collections.Generic;
using System.Text;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Geometry;
using LJJSCAD.DrawingElement;
using VectorDraw.Professional.Constants;
using LJJSCAD.Util;
using LJJSCAD.CommonData;
using DesignEnum;

namespace LJJSCAD.Drawing.Text
{
    class LJJSText
    {
        public static ulong AddHorCommonText(string textContent, LJJSPoint insertPt,int textColor, AttachmentPoint attachmentPoint, double textHeight, string fontFile)
        {
            AttachmentPointAdapter attptAdapter = GetAttachmentPointAdapter(attachmentPoint);

            return  VectorDrawHelper.AddText(DrawCommonData.activeDocument, textContent, new gPoint(insertPt.XValue,insertPt.YValue), attptAdapter.verJust, attptAdapter.HorJust, textColor, fontFile, textHeight, 1.0, VectorDraw.Render.grTextStyleExtra.TextLineFlags.None, 0, false);

        }
        public static ulong AddHorCommonText(string textContent, LJJSPoint insertPt, int textColor, AttachmentPoint attachmentPoint,  string txtStyle)
        {
            AttachmentPointAdapter attptAdapter = GetAttachmentPointAdapter(attachmentPoint);

            return VectorDrawHelper.AddText(DrawCommonData.activeDocument, textContent, new gPoint(insertPt.XValue, insertPt.YValue), textColor, attptAdapter.verJust, attptAdapter.HorJust, txtStyle);

        }
        public static ulong AddHorCommonTextByLayer(string textContent, LJJSPoint insertPt, AttachmentPoint attachmentPoint, double textHeight, string fontFile)
    {
         AttachmentPointAdapter attptAdapter = GetAttachmentPointAdapter(attachmentPoint);
         return VectorDrawHelper.AddTextByLayer(DrawCommonData.activeDocument, textContent, new gPoint(insertPt.XValue, insertPt.YValue), attptAdapter.verJust, attptAdapter.HorJust,  fontFile, textHeight, 1.0, VectorDraw.Render.grTextStyleExtra.TextLineFlags.None, 0, false);
    }
        private static AttachmentPointAdapter GetAttachmentPointAdapter(AttachmentPoint attachmentPoint)
        {
            AttachmentPointAdapter attachmentPointAdapter = new AttachmentPointAdapter();
            attachmentPointAdapter.HorJust = VdConstHorJust.VdTextHorCenter;
            attachmentPointAdapter.verJust = VdConstVerJust.VdTextVerBottom;
            if (attachmentPoint.Equals(AttachmentPoint.BottomCenter))
                return attachmentPointAdapter;
            else if (attachmentPoint.Equals(AttachmentPoint.BottomLeft))
            {
                attachmentPointAdapter.HorJust = VdConstHorJust.VdTextHorLeft;
                return attachmentPointAdapter;
            }
            else if (attachmentPoint.Equals(AttachmentPoint.MiddleCenter))
            {
                attachmentPointAdapter.HorJust = VdConstHorJust.VdTextHorCenter;
                attachmentPointAdapter.verJust = VdConstVerJust.VdTextVerCen;
                return attachmentPointAdapter;
            }
            else if (attachmentPoint.Equals(AttachmentPoint.MiddleLeft))
            {
                attachmentPointAdapter.HorJust = VdConstHorJust.VdTextHorLeft;
                attachmentPointAdapter.verJust = VdConstVerJust.VdTextVerCen;
                return attachmentPointAdapter;
            }
            else
            {
                attachmentPointAdapter.HorJust = VdConstHorJust.VdTextHorRight;
                return attachmentPointAdapter;
            }
        
        }
    }
}
