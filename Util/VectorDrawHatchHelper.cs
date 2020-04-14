using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VectorDraw.Professional.vdObjects;
using LJJSCAD.Drawing.Hatch;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.Constants;
using LJJSCAD.DrawingElement;
using VectorDraw.Geometry;
using System.Drawing;

namespace LJJSCAD.Util
{
    class VectorDrawHatchHelper
    {
        public static VdConstFill ConvertToVDFillMode(LJJSFillMode fillMode)
        {
            if (fillMode.Equals(LJJSFillMode.ModeNone))
                return VdConstFill.VdFillModeNone;
            else if (fillMode.Equals(LJJSFillMode.ModeSolid))
                return VdConstFill.VdFillModeSolid;
            else if (fillMode.Equals(LJJSFillMode.ModeHatchPattern))
                return VdConstFill.VdFillModePattern;
            else if (fillMode.Equals(LJJSFillMode.ModeHatchBlock))
                return VdConstFill.VdFillModeHatchBlock;
            else if (fillMode.Equals(LJJSFillMode.ModeHatchCross))
                return VdConstFill.VdFillModeHatchCross;
            else if (fillMode.Equals(LJJSFillMode.ModeHatchImage))
                return VdConstFill.VdFillModeImage;
            else if (fillMode.Equals(LJJSFillMode.ModeHatchFDiagonal))
                return VdConstFill.VdFillModeHatchFDiagonal;
            else if (fillMode.Equals(LJJSFillMode.ModeDiagross))
                return VdConstFill.VdFillModeHatchDiagCross;
            else if (fillMode.Equals(LJJSFillMode.ModeHatchBdiagonal))
                return VdConstFill.VdFillModeHatchBDiagonal;
            else if (fillMode.Equals(LJJSFillMode.ModeHatchVertical))
                return VdConstFill.VdFillModeHatchVertical;
            else if (fillMode.Equals(LJJSFillMode.ModeHatchHorizontal))
                return VdConstFill.VdFillModeHatchHorizontal;

            else
                return VdConstFill.VdFillModeNone;

        }
        public static void AddPolyHatch(vdDocument activeDocument,LJJSHatch ljjsHatch,List<vdCurves> curvesLst)
        {
            //  buthatch.Enabled = false;
            //We will create a vdPolyHatch object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vdPolyhatch onehatch = new VectorDraw.Professional.vdFigures.vdPolyhatch();
            //We set the document where the hatch is going to be added.This is important for the vdPolyhatch in order to obtain initial properties with setDocumentDefaults.
            onehatch.SetUnRegisterDocument(activeDocument);
            onehatch.setDocumentDefaults();
            if (null != curvesLst && curvesLst.Count > 0)
            {
                for (int i = 0; i < curvesLst.Count; i++)
                {
                    if(null!=curvesLst[i]&&curvesLst[i].Count>2)
                    onehatch.PolyCurves.AddItem(curvesLst[i]);
                }
            }
            else
                return;
            if (onehatch.PolyCurves.Count < 1)
                return;
            onehatch.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties();
            onehatch.HatchProperties.FillMode =ConvertToVDFillMode(ljjsHatch.FillMode);
            if(ljjsHatch.FillMode.Equals(LJJSFillMode.ModeHatchPattern))
            {
                if(!string.IsNullOrEmpty(ljjsHatch.HatchPattern))
                {
                    onehatch.HatchProperties.HatchPattern=activeDocument.HatchPatterns.FindName(ljjsHatch.HatchPattern);
                }
            }
            if (ljjsHatch.FillMode.Equals(LJJSFillMode.ModeHatchBlock))
            {
                if (!string.IsNullOrEmpty(ljjsHatch.HatchBlk))
                {
                    onehatch.HatchProperties.HatchBlock = activeDocument.Blocks.FindName(ljjsHatch.HatchBlk); 
                }
            }
            onehatch.HatchProperties.FillColor.SystemColor =System.Drawing.Color.FromArgb(ljjsHatch.FillColor);
            onehatch.HatchProperties.FillBkColor.SystemColor = System.Drawing.Color.FromArgb(ljjsHatch.FillBKColor);            
            onehatch.HatchProperties.DrawBoundary = ljjsHatch.IsDrawBoundary;       
 
            onehatch.HatchProperties.HatchScale =ljjsHatch.HatchScale;
          
            onehatch.PenColor.SystemColor = System.Drawing.Color.FromArgb(ljjsHatch.PenColor);
            onehatch.PenWidth = ljjsHatch.PenWidth;


            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            activeDocument.ActiveLayOut.Entities.AddItem(onehatch); 
        }
        public static void AddAreaHatch(vdDocument activeDocument, LJJSHatch ljjsHatch, Vertexes  fillArea)
        {
            if (null == fillArea || fillArea.Count < 3)
                return;
            //We will create a vdPolyline object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vdPolyline onepoly = new VectorDraw.Professional.vdFigures.vdPolyline();
            //We set the document where the polyline is going to be added.This is important for the vdPolyline in order to obtain initial properties with setDocumentDefaults.
            onepoly.SetUnRegisterDocument(activeDocument);
            onepoly.setDocumentDefaults();

            //The two previus steps are important if a vdFigure object is going to be added to a document.
            //Now we will change some properties of the polyline.
            onepoly.VertexList = fillArea;


            onepoly.Flag = VectorDraw.Professional.Constants.VdConstPlineFlag.PlFlagCLOSE;
            //  onepoly.SPlineFlag = VectorDraw.Professional.Constants.VdConstSplineFlag.SFlagQUADRATIC;

            onepoly.PenColor.SystemColor = Color.FromArgb(ljjsHatch.PenColor);
            //Now we will change the hacth properties of this polyline.Hatch propertis have all curve figures(circle,arc,ellipse,rect,polyline).
            //Initialize the hatch properties object.
            onepoly.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties();
            //And change it's properties
            onepoly.HatchProperties.FillMode = ConvertToVDFillMode(ljjsHatch.FillMode);

            if (ljjsHatch.FillMode.Equals(LJJSFillMode.ModeHatchPattern))
            {
                if (!string.IsNullOrEmpty(ljjsHatch.HatchPattern))
                {
                    onepoly.HatchProperties.HatchPattern = activeDocument.HatchPatterns.FindName(ljjsHatch.HatchPattern);
                }
            }
            if (ljjsHatch.FillMode.Equals(LJJSFillMode.ModeHatchBlock))
            {
                if (!string.IsNullOrEmpty(ljjsHatch.HatchBlk))
                {
                    onepoly.HatchProperties.HatchBlock = activeDocument.Blocks.FindName(ljjsHatch.HatchBlk);
                }
            }

            onepoly.HatchProperties.FillColor.SystemColor = Color.FromArgb(ljjsHatch.FillColor);
            onepoly.HatchProperties.FillBkColor.SystemColor = Color.FromArgb(ljjsHatch.FillBKColor);
            onepoly.HatchProperties.DrawBoundary = ljjsHatch.IsDrawBoundary;
            onepoly.HatchProperties.HatchScale = ljjsHatch.HatchScale;
            onepoly.PenColor.SystemColor = Color.FromArgb(ljjsHatch.PenColor);
            onepoly.PenWidth = ljjsHatch.PenWidth;


            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            activeDocument.ActiveLayOut.Entities.AddItem(onepoly);
            //fc
            //gPoint g1 = new gPoint(0, 0);
            //gPoint g2 = new gPoint(0, 10);
            //activeDocument.ActiveLayOut.ZoomWindow(g1ff,g2 );
           


        }
    }
}
