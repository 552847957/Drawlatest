using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.Model.Drawing;
using System.Data;
using LJJSCAD.DrawingElement;
using LJJSCAD.BlackBoard.PropertiesManage;
using DesignEnum;

namespace LJJSCAD.DrawingOper
{
    class ImageItemBuilderOper
    {
        private ImageItemDesignClass imageItemDesignStruc;

        internal ImageItemDesignClass ImageItemDesignStruc
        {
            get { return imageItemDesignStruc; }
            set { imageItemDesignStruc = value; }
        }
        public List<ImageItemStruc> GetImageItemPerJDDrawData(JDStruc jdStruc, DataTable imageItemDt)
        {
            double jdTop = jdStruc.JDtop;
            double jdBottom = jdStruc.JDBottom;
            List<ImageItemStruc> imagedrawinglist = new List<ImageItemStruc>();
            double depthHeigh, depthbottom = jdStruc.JDBottom;
            if (null== imageItemDt  || imageItemDt.Rows.Count < 1)
                return imagedrawinglist;
            string sqlTxt = GetJDImageItemDataSqlTxt(jdTop.ToString(), jdBottom.ToString());
            DataRow[] drs = imageItemDt.Select(sqlTxt, imageItemDesignStruc.ImageJDBottomField + " ASC");
            if (drs.Length < 1)
                return imagedrawinglist;

            foreach (DataRow dr in drs)
            {
                depthbottom =(double)dr[imageItemDesignStruc.ImageJDBottomField];

                double top = 0; 
                string imagePath = dr[imageItemDesignStruc.ImageFromField].ToString();
                if (imageItemDesignStruc.ImageJDStyle.Equals(DepthFieldStyle.BottomAndHeigh))
                {
                    depthHeigh = (double)dr[imageItemDesignStruc.ImageJDHeighField];
                    top = depthbottom - depthHeigh;
                }
                else if (imageItemDesignStruc.ImageJDStyle.Equals(DepthFieldStyle.TopAndBottom))
                {
                    top = (double)dr[imageItemDesignStruc.ImageJDTopField];
                    depthHeigh = Math.Abs(top - depthbottom);

                }
                else
                    depthHeigh = 0;

                if (top < jdStruc.JDtop)
                    depthHeigh = Math.Abs(jdStruc.JDtop - depthbottom);
                if (depthbottom > jdStruc.JDBottom)
                {
                    depthbottom = Math.Abs(jdStruc.JDBottom);
                    depthHeigh = Math.Abs(top - depthbottom);
                }
                List<StrValueProperty> tmpadditionImeLst = new List<StrValueProperty>();
                if (!string.IsNullOrEmpty(imageItemDesignStruc.ImageAdditionOne))
                {
                    string imeone = dr[imageItemDesignStruc.ImageAdditionOne].ToString();
                    if (!string.IsNullOrEmpty(imeone))
                        tmpadditionImeLst.Add(new StrValueProperty(ImeItemAdditionProperties.pathOneProName, imeone));
                }
                if (!string.IsNullOrEmpty(imageItemDesignStruc.ImageAdditionTwo))
                {
                    string imetwo = dr[imageItemDesignStruc.ImageAdditionTwo].ToString();
                    if (!string.IsNullOrEmpty(imetwo))
                        tmpadditionImeLst.Add(new StrValueProperty(ImeItemAdditionProperties.pathTwoProName, imetwo));
                }
                if (!string.IsNullOrEmpty(imageItemDesignStruc.ImageAdditionThree))
                {
                    string imethree = dr[imageItemDesignStruc.ImageAdditionThree].ToString();
                    if (!string.IsNullOrEmpty(imethree))
                        tmpadditionImeLst.Add(new StrValueProperty(ImeItemAdditionProperties.pathThreeProName, imethree));
                }
                if (!string.IsNullOrEmpty(imageItemDesignStruc.ImageAdditionFour))
                {
                    string imefour = dr[imageItemDesignStruc.ImageAdditionFour].ToString();
                    if (!string.IsNullOrEmpty(imefour))
                        tmpadditionImeLst.Add(new StrValueProperty(ImeItemAdditionProperties.pathFourProName, imefour));
                }
                if (!string.IsNullOrEmpty(imageItemDesignStruc.ImageAdditionFive))
                {
                    string imefive = dr[imageItemDesignStruc.ImageAdditionFive].ToString();
                    if (!string.IsNullOrEmpty(imefive))
                        tmpadditionImeLst.Add(new StrValueProperty(ImeItemAdditionProperties.pathFiveProName, imefive));
                }
                if (!string.IsNullOrEmpty(imageItemDesignStruc.ImageAdditionSix))
                {
                    string imesix = dr[imageItemDesignStruc.ImageAdditionSix].ToString();
                    if (!string.IsNullOrEmpty(imesix))
                        tmpadditionImeLst.Add(new StrValueProperty(ImeItemAdditionProperties.pathSixProName, imesix));
                }
                if (!string.IsNullOrEmpty(imageItemDesignStruc.FormStyle))
                {
                    string frmStyle = imageItemDesignStruc.FormStyle.ToString().Trim();
                    tmpadditionImeLst.Add(new StrValueProperty(ImeItemAdditionProperties.formStyle, frmStyle));
                }

                imagedrawinglist.Add(new ImageItemStruc(imagePath,depthbottom,depthHeigh,tmpadditionImeLst));
            }
            return imagedrawinglist;
        }

        private string GetJDImageItemDataSqlTxt(string jTop, string jBottom)
        {
            string restr = "";

            if (imageItemDesignStruc.ImageJDBottomField != "")
            {
                if (imageItemDesignStruc.ImageJDHeighField != "")
                {
                    restr = restr + "(" + imageItemDesignStruc.ImageJDBottomField + @"-" + imageItemDesignStruc.ImageJDHeighField + ")" + "<" + jBottom + " and " + imageItemDesignStruc.ImageJDBottomField + ">" + jTop;
                }
                else if(imageItemDesignStruc.ImageJDTopField!="")
                {
                    restr = restr + imageItemDesignStruc.ImageJDTopField + ">" + jTop + " and " + imageItemDesignStruc.ImageJDTopField + "<" + jBottom;
                } 
            }
            return restr;

        }
    }
}
