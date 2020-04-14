using System.Drawing;
using LJJSCAD.Util;
using LJJSCAD.DrawingOper;
using DesignEnum;

namespace LJJSCAD.Model.Drawing
{
    class TxtItemClass
    {
        #region<-----私有变量属性和封装字段----->

        private string _iD;
        private string _TxtItemFromTableName;//文本项相关位置等属性来自的表名。
        private string _txtItemName;//绘图项名称
        private string _TxtJDHeigh = "";//井段高度
        private string _TxtJDTop = "";//井段顶
        private string _TxtJDBottom = "";//井段底
        private DepthFieldStyle _depthstyle;
        private TxtItemOutFrame _TextOutFrame = TxtItemOutFrame.NoFrame;
        private string _TxtFont;
        private double _TxtSize = 1.5;
        private double _TxtHeaderStartheigh = 8;//绘图项起始位置的高度
        private string _TxtStrFont;

        private string _FromFieldName = "";//相关字段的名字；
        private int _TxtColor = Color.Black.ToArgb();
        private double _TxtItemTitleSpace = 0;
        private TxtArrangeStyle _TxtPaiLie = TxtArrangeStyle.TxtHorArrange;
        private AttachmentPoint _TxtPosition = AttachmentPoint.BottomCenter;
        private Txtdistribution _TxtDistribution = Txtdistribution.Txtfocus;
        private double _TxtOutFrameWidth;
        private VerDivPos _TxtOutFrameVerDivPos = VerDivPos.LeftPos;
        private double _TIOffset = 0;
        private TxtItemStyle _TiStyle = TxtItemStyle.NumberStyle;
        private ThinBZPosStyle _ThinBZPosStyle = ThinBZPosStyle.TopPos;
        private ItemTitlePos _txtItemTitlePos = ItemTitlePos.Mid;//默认头文本位置位于中间
        private string _TxtDiSubStyle;//绘图文本项的子类；

        public string ID
        {
            get { return _iD; }
            set { _iD = value; }
        }

        public string TxtItemFromTableName
        {
            get { return _TxtItemFromTableName; }
        }

        public string TxtItemName
        {
            get { return _txtItemName; }
        }

        public string TxtJDHeigh
        {
            get { return _TxtJDHeigh; }
        }
        public string TxtJDTop
        {
            get { return _TxtJDTop; }
        }
        public string TxtJDBottom
        {
            get { return _TxtJDBottom; }
        }
        public DepthFieldStyle Depthstyle
        {
            get { return _depthstyle; }
        }
        public TxtItemOutFrame TextOutFrame
        {
            get { return _TextOutFrame; }
        }
        public string TxtFont
        {
            get { return _TxtFont; }
        }
        public string TxtStrFont
        {
            get { return _TxtStrFont; }
        }

        public double TxtSize
        {
            get { return _TxtSize; }
        }
        public double TxtHeaderStartheigh
        {
            get { return _TxtHeaderStartheigh; }
        }
        public string FromFieldName
        {
            get { return _FromFieldName; }
        }

        public int TxtColor
        {
            get { return _TxtColor; }
        }
        public double TxtItemTitleSpace
        {
            get { return _TxtItemTitleSpace; }
        }
        public TxtArrangeStyle TxtPaiLie
        {
            get { return _TxtPaiLie; }
        }
        public AttachmentPoint TxtPosition
        {
            get { return _TxtPosition; }
        }
        public Txtdistribution TxtDistribution
        {
            get { return _TxtDistribution; }
        }
        public double TxtOutFrameWidth
        {
            get { return _TxtOutFrameWidth; }
        }
        public VerDivPos TxtOutFrameVerDivPos
        {
            get { return _TxtOutFrameVerDivPos; }
        }
        public double TIOffset
        {
            get { return _TIOffset; }
        }
        public TxtItemStyle TiStyle
        {
            get { return _TiStyle; }
        }
        public ThinBZPosStyle ThinBZPosStylePro
        {
            get { return _ThinBZPosStyle; }
        }
        public ItemTitlePos TxtItemTitlePos
        {
            get { return _txtItemTitlePos; }
        }
        public string TxtDiSubStyle
        {
            get { return _TxtDiSubStyle; }
        }

        #endregion

        public TxtItemClass(TextItemModel txtItemModel)
        {

            if (null!=txtItemModel)
            {

                _iD = txtItemModel.ID.Trim();
              
                _txtItemName = txtItemModel.TxtItemName.Trim();
                _FromFieldName = txtItemModel.TxtItemField.Trim();
                _TxtItemTitleSpace = StrUtil.StrToDouble(txtItemModel.TxtItemTitleSpace, 0, "文本项标题的纵向距离为非数值型"); //MyNormalOper.GetDoubleValue(ht["TxtItemTitleSpace"].ToString().Trim(), 0, "文本项标题的纵向具体为非数值型");
                _TxtPaiLie = EnumUtil.GetEnumByStr(txtItemModel.TxtPaiLie, TxtArrangeStyle.TxtHorArrange);
                _TxtPosition = EnumUtil.GetEnumByStr(txtItemModel.TxtPosition, AttachmentPoint.BottomCenter);// PositionUtil.GetTxtPositionForCAD(txtItemModel.TxtPosition);
                _TxtDistribution = EnumUtil.GetEnumByStr(txtItemModel.TxtLayOut.Trim(),Txtdistribution.Txtfocus);// ht["TxtLayOut"].ToString());//分散或集中；
                _TxtJDTop = txtItemModel.TxtJDTop.Trim();// ht["TxtJDTop"].ToString().Trim();
                _TxtJDHeigh = txtItemModel.TxtJDHeigh.Trim();// ht["TxtJDHeigh"].ToString().Trim();
                _TxtJDBottom = txtItemModel.TxtJDBottom.Trim();// ht["TxtJDBottom"].ToString().Trim();
                _TxtColor = StrUtil.StrToInt(txtItemModel.TxtColor, Color.Black.ToArgb(), "文本项颜色为非数值型");// AutoCADColor.FromColor(SysColor.FromArgb(MyNormalOper.GetIntValue(ht["TxtColor"].ToString(), 0, "文本项颜色为非数值型")));
                _TxtItemFromTableName = txtItemModel.TxtItemFromTbName.Trim();// ht["TxtItemFromTbName"].ToString().Trim();
                _TxtHeaderStartheigh = StrUtil.StrToDouble(txtItemModel.TxtHeaderStartheigh.Trim(), 8, "文本项的起始位置为非数值型");
                _TxtSize = StrUtil.StrToDouble(txtItemModel.TxtSize.Trim(), 1.5, "文本项的字高为非数值型");
                _TextOutFrame = EnumUtil.GetEnumByStr(txtItemModel.TextOutFrame, TxtItemOutFrame.NoFrame); //PositionUtil.GetTxtItemOutFrameStyle(txtItemModel.TextOutFrame.Trim());
                _TxtOutFrameWidth = StrUtil.StrToDouble(txtItemModel.TxtOutFrameWidth, -1, "文本项平行纵向外框的宽度为非数值型");
                _TxtOutFrameVerDivPos = EnumUtil.GetEnumByStr(txtItemModel.TxtOutFrameVerDivPos, VerDivPos.LeftPos);//PositionUtil.GetTxtItemOutFrameVerDivPos(txtItemModel.TxtOutFrameVerDivPos);
                _TIOffset = StrUtil.StrToDouble(txtItemModel.TIOffset, 0, "文本项的相对平移距离为非数值型");
                _TiStyle = EnumUtil.GetEnumByStr(txtItemModel.TxtItemStyle, TxtItemStyle.TxtStyle);//GetTxtStyle(txtItemModel.TxtItemStyle);
                _ThinBZPosStyle = EnumUtil.GetEnumByStr(txtItemModel.ThinBZpos, ThinBZPosStyle.TopPos); //PositionUtil.GetThinBZPosStyle(txtItemModel.ThinBZpos);
                _txtItemTitlePos = EnumUtil.GetEnumByStr(txtItemModel.TxtItemTitleHorPos,ItemTitlePos.Mid); //ItemOper.GetDrawingItemTitlePos(txtItemModel.TxtItemTitleHorPos);
                _depthstyle = ZuoBiaoOper.GetDepthFieldStyle(_TxtJDTop, _TxtJDBottom, _TxtJDHeigh);
                _TxtDiSubStyle = txtItemModel.TxtDiSubStyle.Trim();// ht["TxtDiSubStyle"].ToString().Trim();

                if (string.IsNullOrEmpty(txtItemModel.TxtFont.Trim()))
                    _TxtStrFont = txtItemModel.TxtFont.Trim();// ht["TxtFont"].ToString().Trim();
                else
                    _TxtStrFont = "宋体";

              

            }
        }
     
    }
}
