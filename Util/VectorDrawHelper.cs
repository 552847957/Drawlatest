using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VectorDraw.Geometry;
using LJJSCAD.CommonData;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.Constants;
using System.Drawing;
using System.Windows.Forms;
using VectorDraw.Professional.vdCollections;

namespace LJJSCAD.Util
{
    public class VectorDrawHelper
    {
        public static ulong InsertBolck(vdDocument activeDocument, string blockName, double XScale, double YScale, gPoint position)
        {
            if (activeDocument.Blocks.FindName(blockName) != null)
            {
                Layer_SetCurrent(activeDocument, "0");
                VectorDraw.Professional.vdFigures.vdInsert ins1 = new VectorDraw.Professional.vdFigures.vdInsert();
                ins1.SetUnRegisterDocument(activeDocument);
                ins1.setDocumentDefaults();
                ins1.Block = activeDocument.Blocks.FindName(blockName);
                ins1.Xscale = XScale;
                ins1.Yscale = YScale;
                ins1.InsertionPoint = position;
               
        //        ins1.PenColor.SystemColor = Color.FromArgb(Color.Red.ToArgb());
                activeDocument.ActiveLayOut.Entities.AddItem(ins1);
                return ins1.Handle.Value;
 
            }
            return 0;
        }
        public static ulong InsertBlock(vdDocument targetDoc, string sourceDoc, string blockname)
        {

            return 0;
        }
      private static  vdLineType GetLineTypeByName(vdDocument activeDocument,string lineTypeName)
        {
            for (int i = 0; i < activeDocument.LineTypes.Count; i++)
            {
                string lt = activeDocument.LineTypes[i].Name;
                if (lt.Equals(lineTypeName))
                {
                    return activeDocument.LineTypes[i];
                   
                }
            }
            return null;
        }
      public static ulong CreateLayer(vdDocument activeDocument, string layerName,int argbPenColor,string lineType)
      {
          try
          {
              vdLayer layer = activeDocument.Layers.FindName(layerName);
              if (null != layer)
              {
                  activeDocument.Layers.RemoveItem(layer);
                  
              }
         
                  vdLayer newlayer = new VectorDraw.Professional.vdPrimaries.vdLayer();
                  newlayer.Name = layerName;
                  newlayer.PenColor.SystemColor = Color.FromArgb(argbPenColor);
                  if (!string.IsNullOrEmpty(lineType))
                      newlayer.LineType = activeDocument.LineTypes.FindName(lineType);
                  //We set the document where the polyline is going to be added.This is important for the vdPolyline in order to obtain initial properties with setDocumentDefaults.
                  newlayer.SetUnRegisterDocument(activeDocument);
                  newlayer.setDocumentDefaults();
                  activeDocument.Layers.AddItem(newlayer);
                  return newlayer.Handle.Value;
                    

          }
          catch
          {
              return 0;
          }

  
      }
      public static void Layer_SetCurrent(vdDocument activerDocument,string layerName)
      {
          try
          {
              vdLayer layer= activerDocument.Layers.FindName(layerName);
              if (null != layer)
                  activerDocument.ActiveLayer = layer;
              else
                  return;

          }
          catch
          {
              return;
          }
      }
      public static ulong CommonLineByLayer(vdDocument activeDocument, gPoint pStart, gPoint pEnd, double pWidth,  string pToolTip,string lineTypeName)
      {
          VectorDraw.Professional.vdFigures.vdLine oneline = new VectorDraw.Professional.vdFigures.vdLine();
          //We set the document where the line is going to be added.This is important for the vdLine in order to obtain initial properties with setDocumentDefaults.
          // oneline.SetUnRegisterDocument(DrawCommonData.activeDocument);
          oneline.SetUnRegisterDocument(activeDocument);
          oneline.setDocumentDefaults();
          //The two previus steps are important if a vdFigure object is going to be added to a document.
          //Now we will change some properties of the line.
          oneline.StartPoint = pStart;
          oneline.EndPoint = pEnd;
          if (!string.IsNullOrEmpty(lineTypeName))
          {
              vdLineType linetype = GetLineTypeByName(activeDocument, lineTypeName);
              if (null != linetype)
                  oneline.LineType = GetLineTypeByName(activeDocument, lineTypeName);
          }
          //Pen width is depended from the zoom.See in the vdRect object about LineWeight.
          oneline.PenWidth = pWidth;
          oneline.ToolTip = pToolTip;
          //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
          DrawCommonData.activeDocument.ActiveLayOut.Entities.AddItem(oneline);
          return oneline.Handle.Value;
      
      }
        public static ulong CommonLine(vdDocument activeDocument,gPoint pStart, gPoint pEnd, double pWidth, int pColor,string lineTypeName, string pToolTip)
        {
            VectorDraw.Professional.vdFigures.vdLine oneline = new VectorDraw.Professional.vdFigures.vdLine();
            //We set the document where the line is going to be added.This is important for the vdLine in order to obtain initial properties with setDocumentDefaults.
           // oneline.SetUnRegisterDocument(DrawCommonData.activeDocument);
            oneline.SetUnRegisterDocument(activeDocument);
            oneline.setDocumentDefaults();
            //The two previus steps are important if a vdFigure object is going to be added to a document.
            //Now we will change some properties of the line.
            oneline.StartPoint = pStart;
            oneline.EndPoint = pEnd;
            if (!string.IsNullOrEmpty(lineTypeName))
            {
                vdLineType linetype = GetLineTypeByName(activeDocument, lineTypeName);
                if (null != linetype)
                    oneline.LineType = GetLineTypeByName(activeDocument, lineTypeName);
            }

            oneline.PenColor.SystemColor = Color.FromArgb(pColor);
            //Pen width is depended from the zoom.See in the vdRect object about LineWeight.
            oneline.PenWidth = pWidth;
            oneline.ToolTip = pToolTip;
            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            DrawCommonData.activeDocument.ActiveLayOut.Entities.AddItem(oneline);
            return oneline.Handle.Value;
        }
        public static ulong CommonPolyLine(vdDocument activeDocument, List<gPoint> polyLineLst, double penWidth ,Color color)
        {
            VectorDraw.Professional.vdFigures.vdPolyline onepoly = new VectorDraw.Professional.vdFigures.vdPolyline();
            onepoly.SetUnRegisterDocument(activeDocument);
            onepoly.setDocumentDefaults();
            onepoly.PenWidth = penWidth;
            vdColor LineColor = new vdColor(color);
            onepoly.PenColor = LineColor;
        
            if (polyLineLst != null)
            {
                for (int i = 0; i < polyLineLst.Count; i++)
                {
                    onepoly.VertexList.Add(new gPoint(polyLineLst[i].x, polyLineLst[i].y));
                }
                activeDocument.ActiveLayOut.Entities.AddItem(onepoly);
                return onepoly.Handle.Value;
            }
            else
                return 0;

        }
        public static ulong AddRect(vdDocument activeDocument,gPoint insertPoint, double rectHeight, double rectWidth, int rectColor, double penWidth, DrawDirection drawDirection)
        {

            //We will create a vdRect object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vdRect onerect = new VectorDraw.Professional.vdFigures.vdRect();
            //We set the document where the rect is going to be added.This is important for the vdRect in order to obtain initial properties with setDocumentDefaults.
            onerect.SetUnRegisterDocument(activeDocument);
            onerect.setDocumentDefaults();

            //The two previus steps are important if a vdFigure object is going to be added to a document.
            //Now we will change some properties of the rect.
            onerect.InsertionPoint = insertPoint;//Initial value for a gPoint is (0.0,0.0)
            onerect.Width = rectWidth * drawDirection.HorDirection;
            onerect.Height = rectHeight * drawDirection.VerDirection;
            onerect.PenColor.SystemColor = Color.FromArgb(rectColor);

            onerect.PenWidth = penWidth;
            ////LineWeight is indipended from the zoom.
            //onerect.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_140;

            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            DrawCommonData.activeDocument.ActiveLayOut.Entities.AddItem(onerect);
            return onerect.Handle.Value;
        }
        public static ulong BuildSpineWithSolidHatch(vdDocument activeDocument,Vertexes vertexs,VdConstPlineFlag pLineFlag,VdConstSplineFlag spLineFlag,int hatchColor,string toolTip)
        {
            VectorDraw.Professional.vdFigures.vdPolyline onepoly = new VectorDraw.Professional.vdFigures.vdPolyline();
            //We set the document where the polyline is going to be added.This is important for the vdPolyline in order to obtain initial properties with setDocumentDefaults.
            onepoly.SetUnRegisterDocument(activeDocument);
            onepoly.setDocumentDefaults();

            onepoly.VertexList = vertexs;
            onepoly.ToolTip = toolTip;
            onepoly.Flag = pLineFlag;
            onepoly.SPlineFlag = spLineFlag;
            onepoly.PenColor.SystemColor = Color.FromArgb(hatchColor);
            //Now we will change the hacth properties of this polyline.Hatch propertis have all curve figures(circle,arc,ellipse,rect,polyline).
            //Initialize the hatch properties object.
            onepoly.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties();
            //And change it's properties
            onepoly.HatchProperties.FillMode = VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid;
            onepoly.HatchProperties.FillColor.SystemColor = Color.FromArgb(hatchColor);

            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            activeDocument.ActiveLayOut.Entities.AddItem(onepoly);
            return onepoly.Handle.Value;
        }
        public static void CreateTextStyle(vdDocument activeDocument,string txtStyleName,double txtSize,double widthFactor,string fontFile,string bigFontFile)
        {
            vdTextstyle txt = new vdTextstyle();
            txt.Name = txtStyleName;
            txt.FontFile = fontFile;
            txt.BigFontFile=bigFontFile;
            txt.Height = txtSize;
            txt.WidthFactor = widthFactor;            
            activeDocument.TextStyles.AddItem(txt);
        }
        public static vdTextstyle GetTextStyleByName(vdDocument activeDocument,string txtStyleName)
        {
            vdTextstyle defaulttxt = new vdTextstyle();
            defaulttxt.FontFile = "Times New Roman";
            
            if (string.IsNullOrEmpty(txtStyleName))
                return defaulttxt;
            else
            {
               vdTextstyle txt = activeDocument.TextStyles.FindName(txtStyleName);
               if (null != txt)
                   return txt;
               else
                   return defaulttxt;
            }

 
        }
        public static ulong AddMultiLineText(vdDocument activeDocument, double txtSize, string txtStyle, string textContent, gPoint insertPoint, VdConstVerJust verJust, VdConstHorJust horJust, string fontFile, double verAreaHeight, double horAreaWidth)
        {
            //We will create a vdMText object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vdMText onemtext = new VectorDraw.Professional.vdFigures.vdMText();
            //We set the document where the Mtext is going to be added.This is important for the vdMText in order to obtain initial properties with setDocumentDefaults.
            onemtext.SetUnRegisterDocument(activeDocument);
            onemtext.setDocumentDefaults();

            //The two previus steps are important if a vdFigure object is going to be added to a document.
            //Now we will change some properties of the text.
            onemtext.InsertionPoint = insertPoint;
            onemtext.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
            onemtext.BoxWidth = 30;
            onemtext.Thickness = 10;
            onemtext.Style = activeDocument.TextStyles.FindName("zt");
            onemtext.lunits.FormatLength(10);
            onemtext.PenColor.SystemColor = Color.Red;
            // onemtext.TextString = "\\C1;\\H5.0;VectorDraw \\C2;\\H2x;Development \\C3;\\H2.5;Framework";
            string content = "姚二段,nihao,8888，你好啊，早上好";
            string tmp = "";
            string tmp2 = "";
            // content = "\\T2;" + content;
            char[] ss = content.ToCharArray();
            for (int i = 0; i < ss.Length; i++)
            {
                tmp = tmp + ss[i] + "\\P";
            }
            tmp = "\\H3" + tmp;

            onemtext.TextString = content;
            double width = onemtext.BoundingBox.Width;
            int count = content.Length;
            double wordwidth = width / count;
            int countPerLine = (int)(horAreaWidth / wordwidth);
            if (countPerLine > 0)
            {
                for (int j = 0; j < ss.Length; j++)
                {
                    int modcount = (j + 1) % countPerLine;
                    if (modcount.Equals(0))
                        tmp2 = tmp2 + ss[j] + "\\P";
                    else
                        tmp2 = tmp2 + ss[j];

                }
                tmp2 = "\\H3" + tmp2;
                onemtext.TextString = tmp2;
            }
            onemtext.Height = 50;



            //   MessageBox.Show(width.ToString());
            //  onemtext.TextString="\\H3.0;真正的\\P;\\T2;工程，确实的工程";

            //  onemtext.TextString = "\\H3.0;你好啊，我们一起回家妈妈，";


            /*
            \0...\o             Turns overline on and off 
            \L...\l               Turns underline on and off 
            \\                      Inserts a backslash 
            \{...\}                Inserts an opening and closing brace 
            \Cindex;        Changes to the  specified color 
            \Hvalue;       Changes to the text height specified in drawing units 
            \Hvaluex;     Changes the text height  to a multiple of the current text height 
            \Tvalue;       Adjusts the space between characters, from .75 to 4 times 
            \Qangle;      Changes obliquing angle 
            \Wvalue;     Changes width factor to produce wide text 
            \Ffile name; Changes to the specified font file  
            \A                   Sets the alignment value; valid values: 0, 1, 2 
            \P                   Ends paragraph 
            \S...^...;         Stacks the subsequent text at the \, #, or ^ symbol 
            */

            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            activeDocument.ActiveLayOut.Entities.AddItem(onemtext);

          
            return onemtext.Handle.Value;
        }
        public static ulong AddMText(vdDocument activeDocument, double txtSize, string txtStyle, string textContent, gPoint insertPoint, VdConstVerJust verJust, VdConstHorJust horJust, string fontFile)
        {

            //We will create a vdMText object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vdMText onemtext = new VectorDraw.Professional.vdFigures.vdMText();
            //We set the document where the Mtext is going to be added.This is important for the vdMText in order to obtain initial properties with setDocumentDefaults.
            onemtext.SetUnRegisterDocument(activeDocument);
            onemtext.setDocumentDefaults();

            //The two previus steps are important if a vdFigure object is going to be added to a document.
            //Now we will change some properties of the text.
            onemtext.InsertionPoint = insertPoint;
            onemtext.HorJustify = horJust;

            vdTextstyle txtStyleObj = GetTextStyleByName(activeDocument, txtStyle);
            if (!string.IsNullOrEmpty(fontFile))
                txtStyleObj.FontFile = fontFile;
            if (txtSize > 0.00001)
                txtStyleObj.Height = txtSize;
            onemtext.Style = txtStyleObj;
         //   onemtext.PenColor.SystemColor = Color.FromArgb(TextColor);
            onemtext.HorJustify = horJust;
            onemtext.VerJustify = verJust;
            onemtext.TextString = textContent;
 


            /*
            \0...\o             Turns overline on and off 
            \L...\l               Turns underline on and off 
            \\                      Inserts a backslash 
            \{...\}                Inserts an opening and closing brace 
            \Cindex;        Changes to the  specified color 
            \Hvalue;       Changes to the text height specified in drawing units 
            \Hvaluex;     Changes the text height  to a multiple of the current text height 
            \Tvalue;       Adjusts the space between characters, from .75 to 4 times 
            \Qangle;      Changes obliquing angle 
            \Wvalue;     Changes width factor to produce wide text 
            \Ffile name; Changes to the specified font file  
            \A                   Sets the alignment value; valid values: 0, 1, 2 
            \P                   Ends paragraph 
            \S...^...;         Stacks the subsequent text at the \, #, or ^ symbol 
            */

            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            activeDocument.ActiveLayOut.Entities.AddItem(onemtext);
            return onemtext.Handle.Value;
        }
        public static ulong AddZongPaiMText(vdDocument activeDocument, double txtSize, string txtStyle, string textContent, gPoint insertPoint, VdConstVerJust verJust, VdConstHorJust horJust, string fontFile,double verAreaHeight)
        {
            double txtsize = txtSize;

            //We will create a vdMText object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vdMText onemtext = new VectorDraw.Professional.vdFigures.vdMText();
            //We set the document where the Mtext is going to be added.This is important for the vdMText in order to obtain initial properties with setDocumentDefaults.
            onemtext.SetUnRegisterDocument(activeDocument);
            onemtext.setDocumentDefaults();

            //The two previus steps are important if a vdFigure object is going to be added to a document.
            //Now we will change some properties of the text.
            onemtext.InsertionPoint = insertPoint;
            onemtext.HorJustify = horJust;
            if (verAreaHeight>0.000001)
            onemtext.Height = verAreaHeight/2;
            

            vdTextstyle txtStyleObj = GetTextStyleByName(activeDocument, txtStyle);
            if (!string.IsNullOrEmpty(fontFile))
                txtStyleObj.FontFile = fontFile;
            if (txtSize > 0.00001)
                txtStyleObj.Height = txtSize;
            onemtext.Style = txtStyleObj;
         
            onemtext.HorJustify = horJust;
            onemtext.VerJustify = verJust;
            string tmp = "";         
            char[] ss = textContent.ToCharArray();
            for (int i = 0; i < ss.Length; i++)
            {
                tmp = tmp + ss[i] + "\\P";
            }
            if (txtsize>0.00001)
                tmp = "\\H"+txtSize.ToString() + tmp;

            onemtext.TextString = tmp;



            /*
            \0...\o             Turns overline on and off 
            \L...\l               Turns underline on and off 
            \\                      Inserts a backslash 
            \{...\}                Inserts an opening and closing brace 
            \Cindex;        Changes to the  specified color 
            \Hvalue;       Changes to the text height specified in drawing units 
            \Hvaluex;     Changes the text height  to a multiple of the current text height 
            \Tvalue;       Adjusts the space between characters, from .75 to 4 times 
            \Qangle;      Changes obliquing angle 
            \Wvalue;     Changes width factor to produce wide text 
            \Ffile name; Changes to the specified font file  
            \A                   Sets the alignment value; valid values: 0, 1, 2 
            \P                   Ends paragraph 
            \S...^...;         Stacks the subsequent text at the \, #, or ^ symbol 
            */

            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            activeDocument.ActiveLayOut.Entities.AddItem(onemtext);
            return onemtext.Handle.Value;
        }

        public static ulong AddText(vdDocument activeDocument, string textContent, gPoint insertPoint, VdConstVerJust verJust, VdConstHorJust horJust, int TextColor, string fontFile, double textHeight, double widthFactor, VectorDraw.Render.grTextStyleExtra.TextLineFlags textLineFlags, double textRotation, bool ifBold)
        {

            //We will create a vdText object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vdText onetext = new VectorDraw.Professional.vdFigures.vdText();
            //We set the document where the text is going to be added.This is important for the vdText in order to obtain initial properties with setDocumentDefaults.
            onetext.SetUnRegisterDocument(activeDocument);
            onetext.setDocumentDefaults();

            //The two previus steps are important if a vdFigure object is going to be added to a document.
            //Now we will change some properties of the text.
            onetext.PenColor.SystemColor = Color.FromArgb(TextColor);
            onetext.TextString = textContent;      
  
          
            onetext.InsertionPoint = insertPoint;
            onetext.VerJustify = verJust;
            onetext.HorJustify = horJust;
            onetext.TextLine = textLineFlags;
            onetext.Height = textHeight;
            onetext.WidthFactor = widthFactor;
            onetext.Rotation = textRotation;
            onetext.Bold = ifBold;
            onetext.Style.FontFile = fontFile;
            
            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            activeDocument.ActiveLayOut.Entities.AddItem(onetext);
            return onetext.Handle.Value;
        }

        public static ulong AddText(vdDocument activeDocument, string textContent, gPoint insertPoint, int textColor, VdConstVerJust verJust, VdConstHorJust horJust, string txtStyle)
        {

            //We will create a vdText object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vdText onetext = new VectorDraw.Professional.vdFigures.vdText();
            //We set the document where the text is going to be added.This is important for the vdText in order to obtain initial properties with setDocumentDefaults.
            onetext.SetUnRegisterDocument(activeDocument);
            onetext.setDocumentDefaults();

            //The two previus steps are important if a vdFigure object is going to be added to a document.
            //Now we will change some properties of the text.
            onetext.PenColor.SystemColor = Color.FromArgb(textColor);
            onetext.TextString = textContent;


            onetext.InsertionPoint = insertPoint;
            onetext.VerJustify = verJust;
            onetext.HorJustify = horJust;
           
            onetext.Style = GetTextStyleByName(activeDocument, txtStyle);

            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            activeDocument.ActiveLayOut.Entities.AddItem(onetext);
            return onetext.Handle.Value;
        }

        public static void ClearAllEntities(vdDocument activeDocument)
        {
            activeDocument.ClearLayoutEntities(activeDocument.ActiveLayOut);
        }


        public static ulong AddTextByLayer(vdDocument activeDocument, string textContent, gPoint insertPoint, VdConstVerJust verJust, VdConstHorJust horJust,string fontFile, double textHeight, double widthFactor, VectorDraw.Render.grTextStyleExtra.TextLineFlags textLineFlags, double textRotation, bool ifBold)
        {

            //We will create a vdText object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vdText onetext = new VectorDraw.Professional.vdFigures.vdText();
            //We set the document where the text is going to be added.This is important for the vdText in order to obtain initial properties with setDocumentDefaults.
            onetext.SetUnRegisterDocument(activeDocument);
            onetext.setDocumentDefaults();


            onetext.TextString = textContent;

            //vdText object with setDocumentDefaults has the STANDARD TextStyle.We will change the font of this textstyle to Verdana.
            //   activeDocument.TextStyles.Standard.FontFile = fontFile;
            //We set the insertion point depending the width of the Text from the vdFigure's BoundingBox

            onetext.InsertionPoint = insertPoint;
            onetext.VerJustify = verJust;
            onetext.HorJustify = horJust;
            onetext.TextLine = textLineFlags;
            onetext.Height = textHeight;
            onetext.WidthFactor = widthFactor;
            onetext.Rotation = textRotation;
            onetext.Bold = ifBold;
            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            activeDocument.ActiveLayOut.Entities.AddItem(onetext);
            return onetext.Handle.Value;
        }

        public static ulong AddHatchImageToFigure(vdDocument activeDocument, Vertexes vertexs, string toolTip, string imagePath, double hatchScale,vdXProperties xProperties)
        {
            //We will create a vdPolyline object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vdPolyline onepoly = new VectorDraw.Professional.vdFigures.vdPolyline();
            //We set the document where the polyline is going to be added.This is important for the vdPolyline in order to obtain initial properties with setDocumentDefaults.
            onepoly.SetUnRegisterDocument(activeDocument);
            onepoly.setDocumentDefaults();
            onepoly.ToolTip = toolTip;
            if (null != xProperties&&xProperties.Count>0)
            {
                for (int i = 0; i < xProperties.Count; i++)
                {
                    onepoly.XProperties.AddItem(xProperties[i]);
                }
            }
          

            onepoly.VertexList = vertexs;
            onepoly.Flag = VectorDraw.Professional.Constants.VdConstPlineFlag.PlFlagCLOSE;

            //Initialize the hatch properties object.
            onepoly.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties();
            //And change it's properties
            onepoly.HatchProperties.FillMode = VectorDraw.Professional.Constants.VdConstFill.VdFillModeImage;
            onepoly.HatchProperties.HatchScale = hatchScale;
            onepoly.HatchProperties.DrawBoundary = false;
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + imagePath;
            onepoly.HatchProperties.HatchImage = activeDocument.Images.Add(path);
            activeDocument.ActiveLayOut.Entities.AddItem(onepoly);
            return onepoly.Handle.Value;
        }

        public static ulong AddHatchToFigure(vdDocument activeDocument, Vertexes vertexs, int lineColor, int hatchColor, VdConstSplineFlag splineFlag)
        {
            if (null == vertexs || vertexs.Count < 3)
                return 0;

            //We will create a vdPolyline object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vdPolyline onepoly = new VectorDraw.Professional.vdFigures.vdPolyline();
            //We set the document where the polyline is going to be added.This is important for the vdPolyline in order to obtain initial properties with setDocumentDefaults.
            onepoly.SetUnRegisterDocument(activeDocument);
            onepoly.setDocumentDefaults();

            //The two previus steps are important if a vdFigure object is going to be added to a document.
            //Now we will change some properties of the polyline.
            onepoly.VertexList = vertexs;
          
       
            onepoly.Flag = VectorDraw.Professional.Constants.VdConstPlineFlag.PlFlagCLOSE;
            //  onepoly.SPlineFlag = VectorDraw.Professional.Constants.VdConstSplineFlag.SFlagQUADRATIC;
            onepoly.SPlineFlag = splineFlag;   //相当于上一句话

            onepoly.PenColor.SystemColor = Color.FromArgb(lineColor);
            //onepoly.PenColor.SystemColor = Color.Yellow;

            //Now we will change the hacth properties of this polyline.Hatch propertis have all curve figures(circle,arc,ellipse,rect,polyline).
            //Initialize the hatch properties object.
            onepoly.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties();
            //And change it's properties
            onepoly.HatchProperties.FillMode = VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid;
            onepoly.HatchProperties.FillColor.SystemColor =Color.FromArgb(hatchColor);
            //onepoly.HatchProperties.FillColor.ColorIndex = 122;

            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            activeDocument.ActiveLayOut.Entities.AddItem(onepoly);
            
        //    gPoint gp1 = new gPoint(vertexs[0].x, vertexs[0].y);
        //    gPoint gp2 = new gPoint(vertexs[2].x, vertexs[2].y);
       //     activeDocument.ActiveLayOut.ZoomWindow(gp1,gp2 );
            
            
            return onepoly.Handle.Value;
        }
    }
}
