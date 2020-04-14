using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.DrawingElement
{
  public  class LJJSPoint
    {
        private double _xValue;

        public double XValue
        {
            get { return _xValue; }
            set { _xValue = value; }
        }
        private double _yValue;

        public double YValue
        {
            get { return _yValue; }
            set { _yValue = value; }
        }
        public  LJJSPoint(double xValue,double yValue)
        {
            this._xValue = xValue;
            this._yValue = yValue;
        }

        public LJJSPoint()
        {
            // TODO: Complete member initialization
        }

        public override bool  Equals(object obj)
        {
            LJJSPoint point = obj as LJJSPoint;
            if (this.XValue == point.XValue && this.YValue == point.YValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
