using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.Model
{
  class LineRoadEnvironment
    {
        private double _lineRoadWidth;

        public double LineRoadWidth
        {
            get { return _lineRoadWidth; }
            set { _lineRoadWidth = value; }
        }
        private List<JDStruc> _jdDrawLst;

        public List<JDStruc> JdDrawLst
        {
            get { return _jdDrawLst; }
            set { _jdDrawLst = value; }
        }

    }
}
