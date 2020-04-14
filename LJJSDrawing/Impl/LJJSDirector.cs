using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.LJJSDrawing.Interface;

namespace LJJSCAD.LJJSDrawing.Impl
{
    class LJJSDirector
    {
        private LJJSBuilder ljjsBuilder;
        public LJJSDirector(LJJSBuilder ljjsBuilder)
        {
            this.ljjsBuilder = ljjsBuilder;
        }
        public void BuildLJJS()
        {
            ljjsBuilder.BuildFrame();
            ljjsBuilder.BuildLineRoadArea();
         //   ljjsBuilder.BuildLegendArea();
        //    ljjsBuilder.BuildBiLiChi();
        //    ljjsBuilder.BuildHeader();
        }
    }
}
