using System;
using System.Collections.Generic;
using System.Text;

namespace DrawingModelDBStruc
{
    public class SymbolCodeStruct
    {
        private string _xh;
        /// <summary>
        /// 符号定义的序号
        /// </summary>
        public string xh
        {
            get { return _xh; }
            set { _xh = value; }
        }
        private string _ysmc;
        /// <summary>
        /// 符号定义的名称
        /// </summary>
        public string ysmc
        {
            get { return _ysmc; }
            set { _ysmc = value; }
        }
        private string _symbolcode;
        /// <summary>
        /// 符号定义的代码
        /// </summary>
        public string SymbolCode
        {
            get { return _symbolcode; }
            set { _symbolcode = value; }
        }
        private string _legendtype;
        /// <summary>
        /// 符号定义的图例类型
        /// </summary>
        public string LegendType
        {
            get { return _legendtype; }
            set { _legendtype = value; }
        }
        private string _symbolwidth;
        /// <summary>
        /// 符号的宽度
        /// </summary>
        public string SymbolWidth
        {
            get { return _symbolwidth; }
            set { _symbolwidth = value; }
        }
        private string _lengendlwidth;
        /// <summary>
        /// 符号定义时图例的宽度
        /// </summary>
        public string LegendlWidth
        {
            get { return _lengendlwidth; }
            set { _lengendlwidth = value; }
        }
        private string _ifzxenlarge;
        /// <summary>
        /// 符号是否纵向放大
        /// </summary>
        public string IfZXEnlarge
        {
            get { return _ifzxenlarge; }
            set { _ifzxenlarge = value; }
        }
        private string _iffill;
        /// <summary>
        /// 符号是否填充
        /// </summary>
        public string IfFill
        {
            get { return _iffill; }
            set { _iffill = value; }
        }
        private string _symbolheigh;
        /// <summary>
        /// 符号的高度
        /// </summary>
        public string SymbolHeigh
        {
            get { return _symbolheigh; }
            set { _symbolheigh = value; }
        }

    }
}
