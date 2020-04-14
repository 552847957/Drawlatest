using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.DrawingElement;
using VectorDraw.Geometry;
using LJJSCAD.Util;
using LJJSCAD.CommonData;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdObjects;

namespace LJJSCAD.Drawing.Hatch
{
    class ImageHatch
    {
        public static ulong AddRectImageHatch(LJJSPoint leftBottomInsertPt,double rectHeigh,double rectWidth,string imagePath,double hatchScale,List<StrValueProperty> additionImageLst)
        {
            vdXProperties tmppro=new vdXProperties();
            gPoint leftBottomPt = new gPoint(leftBottomInsertPt.XValue, leftBottomInsertPt.YValue);
            gPoint leftTopPt=new gPoint(leftBottomInsertPt.XValue,leftBottomInsertPt.YValue+DrawCommonData.DirectionUp*rectHeigh);
            gPoint rightBottomPt=new gPoint(leftBottomInsertPt.XValue+rectWidth*DrawCommonData.DirectionRight,leftBottomInsertPt.YValue);
            gPoint rightTopPt=new gPoint(leftBottomInsertPt.XValue+rectWidth*DrawCommonData.DirectionRight,leftBottomInsertPt.YValue+DrawCommonData.DirectionUp*rectHeigh);

            Vertexes hatchRect=new Vertexes();
            hatchRect.Add(leftBottomPt);
            hatchRect.Add(leftTopPt);
            hatchRect.Add(rightTopPt);
            hatchRect.Add(rightBottomPt);
            if(null!=additionImageLst&&additionImageLst.Count>0)
            {               
                for(int i=0;i<additionImageLst.Count;i++)
                {
                    StrValueProperty tmp=additionImageLst[i];
                    if(!string.IsNullOrEmpty(tmp.PropertyName)&&!string.IsNullOrEmpty(tmp.PropertyValue))
                    {
                        vdXProperty tmpproperty = new vdXProperty();
                        tmpproperty.Name = tmp.PropertyName;
                        tmpproperty.PropValue = tmp.PropertyValue;
                        tmppro.AddItem(tmpproperty);
                    }
                }
            }
            return VectorDrawHelper.AddHatchImageToFigure(DrawCommonData.activeDocument, hatchRect, "", imagePath, hatchScale, tmppro);

            
        }
      
    }
}
