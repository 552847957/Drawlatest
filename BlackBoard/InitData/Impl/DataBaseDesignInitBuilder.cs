using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.BlackBoard.InitData.Interface;
using LJJSCAD.BlackBoard.LJJSDesignData.Impl;
using System.Data.Common;
using LJJSCAD.DAL;
using LJJSCAD.CommonData;
using LJJSCAD.Model;
using System.Reflection;
using LJJSCAD.DrawingOper;
using LJJSCAD.BlackBoard.LJJSDesignData.Interface;
using LJJSCAD.BlackBoard.LJJSDesignData.DataBaseProvider;
using LJJSCAD.BlackBoard.LJJSDesignData.ModelOper;
using LJJSCAD.Model.Drawing;
using LJJSCAD.DrawingDesign.Frame;
using DesignEnum;
namespace LJJSCAD.BlackBoard.InitData.Impl
{
    class DataBaseDesignInitBuilder : DesignInitBuilder
    {

        public override void InitFrameDesign()
        {
            FrameModelDAL dal=new FrameModelDAL(MyTableManage.DrawingFrameTbName);
            DbDataReader dr=dal.GetDefaultFrameModel(@"where IfDefaultModel='True'");
            FrameModel fm=new FrameModel();
            if (dr.HasRows)
            {
                Type type = fm.GetType();
                while (dr.Read())
                {                   
                    foreach (PropertyInfo p in type.GetProperties())
                    {

                        string proName = p.Name.Trim();
                        string proValue = dr[proName].ToString().Trim();
                        type.GetProperty(proName).SetValue(fm, proValue, null);

                    }
                }
            }
            FrameDesignManage.SetFrameDesginByFrameModel(fm);
            
        }
        //将绘图项结构按照类别进行保存；

        public override void InitItemDesign()
        {
            //1,曲线项
            CurveItemDesignManage curItemManage = new CurveItemDesignManage();
            CurveItemDesignManage.CurveItemDesignHt = curItemManage.GetItemStrucHtByDB(DrawItemStyle.LineItem);
            
            //2,文本项
            TextItemDesignManage txtItemManage = new TextItemDesignManage();
            TextItemDesignManage.TextItemDesignManageHt = txtItemManage.GetItemStrucHtByDB(DrawItemStyle.TextItem);
     
            //3,符号项
            SymbolItemDesignManage symbolItemManage = new SymbolItemDesignManage();
            SymbolItemDesignManage.SymbolItemDesignManageHt = symbolItemManage.GetItemStrucHtByDB(DrawItemStyle.SymbolItem);

            //4,图片项
            ImageItemDesignManage imageItemDesignManage=new ImageItemDesignManage();
            ImageItemDesignManage.ImageItemDesignManageHt = imageItemDesignManage.GetItemStrucHtByDB(DrawItemStyle.ImageItem);

            //5,核磁共振项，带T2截止值得谱图；
            HCGZItemDesignManage hcgzItemDesignManage = new HCGZItemDesignManage();
            HCGZItemDesignManage.hcgzItemManageHt = hcgzItemDesignManage.GetItemStrucHtByDB(DrawItemStyle.HCGZItem);
            
            //6,多级曲线填充项
            MultiHatchCurveDesignManage mHCurveDesignManage = new MultiHatchCurveDesignManage();
            MultiHatchCurveDesignManage.MultiHatchCurveDesignManageHt = mHCurveDesignManage.GetItemStrucHtByDB(DrawItemStyle.MultiHatchCurveItem);
           
            //7,填充折线类
            CurveHasHatchDesignManage cHCurveDesignManage = new CurveHasHatchDesignManage();
            CurveHasHatchDesignManage.CurveHasHatchDesignManageHt = cHCurveDesignManage.GetItemStrucHtByDB(DrawItemStyle.CurveHasHatchItem);
            
            //8,普通谱图
            NormalPuTuDesignManage puTuDesignManage = new NormalPuTuDesignManage();
            NormalPuTuDesignManage.NormalPuTuDesignManageHt = puTuDesignManage.GetItemStrucHtByDB(DrawItemStyle.NormalPuTuItem);


            //9,填充直方图
            HatchRectDesignManage rectDesignManage = new HatchRectDesignManage();
            HatchRectDesignManage.HatchRectDesignManageHt = rectDesignManage.GetItemStrucHtByDB(DrawItemStyle.HatchRectItem);

        }

        public override void InitItemSymbolDesign()
        {
            //初始化符号定义管理SymbolCodeClassHt；定义每个符号的性质；
            FillSymbolCode.SetSymbolCodeClassHt();

        }
        //需要以其他条件做为基础的初始化内容，本初始化为最后执行的设计初始化；
        public override void InitOtherDesign()
        {
            DrawItemNamesManage.SetDrawItemNamesLstFromHt();
        }

        public override void InitKeDuChi()
        {
            //1,将刻度尺的数据导入到哈希表Ht_Keduchi中
            KeDuChiDesignManage.SetDrawingKeduByDB("");
            //2,根据刻度尺的List建立刻度尺的绘图管理结构
            KeDuChiManage.CreateKDCManageHt();//该方法必须在FillDrawingKeduchi.SetDrawingKeduByDB("")方法后调用；

        }

        public override void InitLineRoadDesgin(string myLineRoadDesignName)
        {
            //包含曲线项的线道设计；
            ILineRoadDesignDataGet lrdataGet=new LRDesignDataGetByDB();
            List<LineRoadModel> lrModelLst = lrdataGet.GetDefaultLineRoadDesignModelLst(myLineRoadDesignName);
            LineRoadDesign.LineRoadDesginLst = LineRoadModelManage.ConvertLRModelLstToLineRoadDesignLst(lrModelLst);
            //井深线道设计
            LineRoadDesign.JingShenDesign = new JingShenDesignClass(lrdataGet.GetDefaultJingShenDesign());

        }
    }
}
