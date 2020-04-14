using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJJSCAD.BlackBoard;
using LJJSCAD.Model;
using LJJSCAD.BlackBoard.LJJSDesignData.ModelOper;
using LJJSCAD.Util;
using LJJSCAD.BlackBoard.LJJSDesignData.Impl;
using LJJSCAD.CommonData;
using DesignEnum;
using LJJSCAD.DesignEnumManage;
using EnumManage;
namespace LJJSCAD.UI
{
    public partial class Frm_LineRoad : Form
    {
        private MyFrameAEModel aeModel;
        private string currLineRoadId = "";

        public string CurrLineRoadId
        {
            get { return currLineRoadId; }
            set { currLineRoadId = value; }
        }
        public Frm_LineRoad()
        {
            InitializeComponent();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void LineRoad_Shown(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currLineRoadId))//编辑模式
            {
                aeModel = MyFrameAEModel.Edit;
                EditModelInitFrame();

            }
            else//添加模式
            {
                aeModel = MyFrameAEModel.Add;
                AddModelInitFrame();

            }
        }

        private void AddModelInitFrame()
        {
            this.Text = "添加线道信息";
            List<DrawItemName> noSelectDINameLst = GetNoSelectedDINames();
            FillDrawItemListBox(noSelectDINameLst, this.lb_WaitSelect);
            ComboDicOper.CreateBindSource(new LineRoadStyleDic(), LineRoadStyle.StandardLineRoad.ToString(), this.cb_LineRoadStyle);
            ComboDicOper.CreateBindSource(new CeWangStyleDic(), CeWangStyleEnum.None.ToString(), this.cb_CeWangStyle);
            ComboDicOper.CreateBindSource(new LineLeftKindDic(), LineLeftKind.enline.ToString(), this.cb_leftline);
        }

        private void EditModelInitFrame()
        {
            LineRoadDesignClass lrDesign = LineRoadDesign.GetLineRoadDesignStrucById(currLineRoadId);
          
            if (null == lrDesign)
                return;
            this.Text = "修改线道信息";
            ComboDicOper.CreateBindSource(new LineRoadStyleDic(), lrDesign.LineRoadStyle.ToString(), this.cb_LineRoadStyle);
            ComboDicOper.CreateBindSource(new CeWangStyleDic(), CeWangStyleEnum.None.ToString(), this.cb_CeWangStyle);
            //1,测网设计；
            if (lrDesign.Cewang.ifAdd)
            {
                LineRoadCeWang tmpcewang = lrDesign.Cewang;
                this.rb_CeWangYes.Checked = true;
                this.chb_cWHeng.Checked = tmpcewang.ifHeng;
                this.chb_cwZong.Checked = tmpcewang.ifZong;
                this.tb_CeWangFixLen.Text = tmpcewang.cewangfixlen.ToString();
                this.tb_CeWangSepNum.Text = tmpcewang.cewangsepnum.ToString();
                this.tb_CWDuiShuMin.Text = tmpcewang.duishuminvalue.ToString();
                this.tb_CWDuiShuParam.Text = tmpcewang.duishuParam.ToString();
                ComboDicOper.CreateBindSource(new CeWangStyleDic(), tmpcewang.cewangstyle.ToString(), this.cb_CeWangStyle);
            
            }
            else
                this.rb_CeWangNo.Checked = true;

            //2,线道样式
            this.tb_LineGroupWidth.Text = lrDesign.LineRoadWidth.ToString();
            this.tb_LineRoadName.Text = lrDesign.LineRoadName.ToString();
            this.chb_ifLeftSecondKD.Checked = lrDesign.IfLeftSecondKD;
            this.chb_ifRightSecondKD.Checked = lrDesign.IfRightSecondKD;
            this.chb_ifZhengMiLine.Checked = lrDesign.IfzhengMiLine;
            this.tb_titleStartHeight.Text = lrDesign.LineroadTitleHeight.ToString();
            

            //3,线道头左侧线

            this.chk_IfDrawTitleLeft.Checked = lrDesign.TitleLeftFrameLineChecked;
            ComboDicOper.CreateBindSource(new LineLeftKindDic(), lrDesign.LeftLineStyle.ToString(), this.cb_leftline);

           
            this.tb_leftlineLength.Text = lrDesign.LeftLineLength.ToString();

            //4,线道所包含的绘图项
            List<DrawItemName> noSelectDINameLst = GetNoSelectedDINames();
            FillDrawItemListBox(noSelectDINameLst, this.lb_WaitSelect);

            List<DrawItemName> lrDIS = lrDesign.Drawingitems;
            FillDrawItemListBox(lrDIS, this.lb_selected);
        }

        private List<DrawItemName> GetNoSelectedDINames()
        {
            List<DrawItemName> noSelectDINameLst = new List<DrawItemName>();
            List<DrawItemName> selectedDINameLst = new List<DrawItemName>();
            for (int i = 0; i < LineRoadDesign.LineRoadDesginLst.Count; i++)
            {
                LineRoadDesignClass tmp = LineRoadDesign.LineRoadDesginLst[i];
                if (null != tmp && null != tmp.Drawingitems && tmp.Drawingitems.Count > 0)
                    selectedDINameLst.AddRange(tmp.Drawingitems);

            }
            noSelectDINameLst = DrawItemNamesManage.GetNoSelectedDrawItemNamesList(selectedDINameLst);
            return noSelectDINameLst;
        }
        private void FillDrawItemListBox(List<DrawItemName> sourceLst, System.Windows.Forms.ListBox targetLB)
        {
            for (int i = 0; i < sourceLst.Count; i++)
            {
                DrawItemName tmp = sourceLst[i];
                string tmpDrawItemName = tmp.DrawItemID + ";" + tmp.ItemStyle.ToString() + ";" + tmp.DrawItemShowName;
                targetLB.Items.Add(tmpDrawItemName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (aeModel.Equals(MyFrameAEModel.Add))
                AddLineRoadDesign();
            else
                EditLineRoadDesign();
            this.DialogResult = DialogResult.OK;

        }
        private void EditLineRoadDesign()
        {
            LineRoadDesignClass lrDesign = LineRoadDesign.GetLineRoadDesignStrucById(currLineRoadId);
            if (null == lrDesign)
                return;
            LineRoadDesignClass tmplr = CreateLRDesignStrucByFrm(currLineRoadId);
            LineRoadDesign.UpdateLineRoadDesignStruc(tmplr);

        }

        private void AddLineRoadDesign()
        {
            string Lrmodelid = new IndexNums().createIndexNum(IDPreManage.LineRoadPreStr/**LRD**/);
            LineRoadDesignClass tmplr = CreateLRDesignStrucByFrm(Lrmodelid);
            LineRoadDesign.LineRoadDesginLst.Add(tmplr);
        }
        public void AddLineRoadDesign(string input)
        {
            string Lrmodelid = new IndexNums().createIndexNum("LRD");
            LineRoadDesignClass tmplr = CreateLRDesignStrucByFrm(Lrmodelid,true);
            LineRoadDesign.LineRoadDesginLst.Add(tmplr);
        }
        private LineRoadDesignClass CreateLRDesignStrucByFrm(string Lrmodelid,bool bydefault)
        {
            //"133;LineItem;基值线"
            LineRoadModel tmplrmodel = new LineRoadModel();
            tmplrmodel.LineRoadDesignDetailID = Lrmodelid;
            //1,测网设计；
            if (this.rb_CeWangYes.Checked)//绘制测网；
            {
                tmplrmodel.ifAddCeWang = true;
                tmplrmodel.IfCeWangHeng = this.chb_cWHeng.Checked;
                tmplrmodel.IfCeWangZong = this.chb_cwZong.Checked;
                if (null != this.cb_CeWangStyle.SelectedValue)
                    tmplrmodel.cewangstyle = this.cb_CeWangStyle.SelectedValue.ToString();
                tmplrmodel.cewangfixlen = this.tb_CeWangFixLen.Text.Trim();
                tmplrmodel.cewangsepnum = this.tb_CeWangSepNum.Text.Trim();
                tmplrmodel.duishuminvalue = this.tb_CWDuiShuMin.Text.Trim();
                tmplrmodel.duishuParam = this.tb_CWDuiShuParam.Text.Trim();

            }
            else
                tmplrmodel.ifAddCeWang = false;
            //2,线道样式
            tmplrmodel.IfLeftSecondKD = this.chb_ifLeftSecondKD.Checked;
            tmplrmodel.IfRightSecondKD = this.chb_ifRightSecondKD.Checked;
            tmplrmodel.IfzhengMiLine = this.chb_ifZhengMiLine.Checked;
            tmplrmodel.LineRoadWidth = this.tb_LineGroupWidth.Text.Trim();
            tmplrmodel.LineRoadName = this.tb_LineRoadName.Text.Trim();
            tmplrmodel.LineroadTitleHeight = this.tb_titleStartHeight.Text.Trim();
            if (null != this.cb_LineRoadStyle.SelectedValue)
                tmplrmodel.LineRoadStyle = this.cb_LineRoadStyle.SelectedValue.ToString();

            //3,线道头左侧线
            tmplrmodel.TitleLeftFrameLineChecked = this.chk_IfDrawTitleLeft.Checked;
            if (null != this.cb_leftline.SelectedValue)
                tmplrmodel.LeftLineStyle = this.cb_leftline.SelectedValue.ToString().Trim();
            tmplrmodel.LeftLineLength = this.tb_leftlineLength.Text;

            //4,线道所包含的绘图项      
            List<string> list = new List<string>();
            list.Add("133;LineItem;基值线");
            for (int i = 0; i < list.Count; i++)
            {
                string tmpItem =list[i].ToString();


                string[] tmpItemArr = tmpItem.Split(';');
                if (tmpItemArr.Length == 3)
                {
                    string id = tmpItemArr[0];
                    string style = tmpItemArr[1];
                    string name = tmpItemArr[2];
                    DrawItemStyle dis = EnumUtil.GetEnumByStr(style, DrawItemStyle.NoneItem);
                    if (dis.Equals(DrawItemStyle.LineItem))
                        tmplrmodel.CurveItemCollections = tmplrmodel.CurveItemCollections + id + ";";
                    else if (dis.Equals(DrawItemStyle.TextItem))
                        tmplrmodel.TextItemCollections = tmplrmodel.TextItemCollections + id + ";";
                    else if (dis.Equals(DrawItemStyle.HCGZItem))
                        tmplrmodel.HcgzItemCollections = tmplrmodel.HcgzItemCollections + id + ";";
                    else if (dis.Equals(DrawItemStyle.SymbolItem))
                        tmplrmodel.SymbolItemCollections = tmplrmodel.SymbolItemCollections + id + ";";
                    else if (dis.Equals(DrawItemStyle.ImageItem))
                        tmplrmodel.ImageItemCollections = tmplrmodel.ImageItemCollections + id + ";";
                    else if (dis.Equals(DrawItemStyle.MultiHatchCurveItem))
                        tmplrmodel.MultiHatchCurveItemCollections = tmplrmodel.MultiHatchCurveItemCollections + id + ";";
                    else if (dis.Equals(DrawItemStyle.HatchRectItem))
                        tmplrmodel.HatchRectItemCollections = tmplrmodel.HatchRectItemCollections + id + ";";
                    else if (dis.Equals(DrawItemStyle.NormalPuTuItem))
                        tmplrmodel.NormalPuTuItemCollections = tmplrmodel.NormalPuTuItemCollections + id + ";";
                    else if (dis.Equals(DrawItemStyle.CurveHasHatchItem))
                        tmplrmodel.CurveHasHatchItemCollections = tmplrmodel.CurveHasHatchItemCollections + id + ";";

                }
            }
            LineRoadDesignClass tmplr = new LineRoadDesignClass(tmplrmodel);
            return tmplr;
        }
        private LineRoadDesignClass CreateLRDesignStrucByFrm(string Lrmodelid)
        {
            LineRoadModel tmplrmodel = new LineRoadModel();
            tmplrmodel.LineRoadDesignDetailID = Lrmodelid;
            //1,测网设计；
            if (this.rb_CeWangYes.Checked)//绘制测网；
            {
                tmplrmodel.ifAddCeWang = true;
                tmplrmodel.IfCeWangHeng = this.chb_cWHeng.Checked;
                tmplrmodel.IfCeWangZong = this.chb_cwZong.Checked;
                if (null != this.cb_CeWangStyle.SelectedValue)
                    tmplrmodel.cewangstyle = this.cb_CeWangStyle.SelectedValue.ToString();
                tmplrmodel.cewangfixlen = this.tb_CeWangFixLen.Text.Trim();
                tmplrmodel.cewangsepnum = this.tb_CeWangSepNum.Text.Trim();
                tmplrmodel.duishuminvalue = this.tb_CWDuiShuMin.Text.Trim();
                tmplrmodel.duishuParam = this.tb_CWDuiShuParam.Text.Trim();

            }
            else
                tmplrmodel.ifAddCeWang = false;
            //2,线道样式
            tmplrmodel.IfLeftSecondKD = this.chb_ifLeftSecondKD.Checked;
            tmplrmodel.IfRightSecondKD = this.chb_ifRightSecondKD.Checked;       
            tmplrmodel.IfzhengMiLine = this.chb_ifZhengMiLine.Checked;
            tmplrmodel.LineRoadWidth = this.tb_LineGroupWidth.Text.Trim();
            tmplrmodel.LineRoadName = this.tb_LineRoadName.Text.Trim();
            tmplrmodel.LineroadTitleHeight = this.tb_titleStartHeight.Text.Trim();
            if (null != this.cb_LineRoadStyle.SelectedValue)
                tmplrmodel.LineRoadStyle = this.cb_LineRoadStyle.SelectedValue.ToString();

            //3,线道头左侧线
            tmplrmodel.TitleLeftFrameLineChecked = this.chk_IfDrawTitleLeft.Checked;
            if (null != this.cb_leftline.SelectedValue)
                tmplrmodel.LeftLineStyle = this.cb_leftline.SelectedValue.ToString().Trim();
            tmplrmodel.LeftLineLength = this.tb_leftlineLength.Text;

            //4,线道所包含的绘图项      
            for (int i = 0; i < this.lb_selected.Items.Count; i++)
            {
                string tmpItem = this.lb_selected.Items[i].ToString();


                string[] tmpItemArr = tmpItem.Split(';');
                if (tmpItemArr.Length == 3)
                {
                    string id = tmpItemArr[0];
                    string style = tmpItemArr[1];
                    string name = tmpItemArr[2];
                    DrawItemStyle dis = EnumUtil.GetEnumByStr(style, DrawItemStyle.NoneItem);
                    if (dis.Equals(DrawItemStyle.LineItem))
                        tmplrmodel.CurveItemCollections = tmplrmodel.CurveItemCollections + id + ";";
                    else if (dis.Equals(DrawItemStyle.TextItem))
                        tmplrmodel.TextItemCollections = tmplrmodel.TextItemCollections + id + ";";
                    else if (dis.Equals(DrawItemStyle.HCGZItem))
                        tmplrmodel.HcgzItemCollections = tmplrmodel.HcgzItemCollections + id + ";";
                    else if (dis.Equals(DrawItemStyle.SymbolItem))
                        tmplrmodel.SymbolItemCollections = tmplrmodel.SymbolItemCollections + id + ";";
                    else if (dis.Equals(DrawItemStyle.ImageItem))
                        tmplrmodel.ImageItemCollections = tmplrmodel.ImageItemCollections + id + ";";
                    else if (dis.Equals(DrawItemStyle.MultiHatchCurveItem))
                        tmplrmodel.MultiHatchCurveItemCollections = tmplrmodel.MultiHatchCurveItemCollections + id + ";";
                    else if (dis.Equals(DrawItemStyle.HatchRectItem))
                        tmplrmodel.HatchRectItemCollections = tmplrmodel.HatchRectItemCollections + id + ";";
                    else if (dis.Equals(DrawItemStyle.NormalPuTuItem))
                        tmplrmodel.NormalPuTuItemCollections = tmplrmodel.NormalPuTuItemCollections + id + ";";
                    else if (dis.Equals(DrawItemStyle.CurveHasHatchItem))
                        tmplrmodel.CurveHasHatchItemCollections = tmplrmodel.CurveHasHatchItemCollections + id + ";";

                }
            }
            LineRoadDesignClass tmplr = new LineRoadDesignClass(tmplrmodel);
            return tmplr;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ListBoxUtil.FromListboxToListBox(this.lb_WaitSelect, this.lb_selected);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListBoxUtil.FromListboxToListBox(this.lb_selected, this.lb_WaitSelect);

        }

        private void tb_LineGroupWidth_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lb_WaitSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
