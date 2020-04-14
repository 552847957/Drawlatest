using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJJSCAD.ItemStyleOper
{
    abstract class AllItemBuilder
    {
        protected abstract void CurveItemBuild();
        protected abstract void TextItemBuild();
        protected abstract void SymbolItemBuild();
        protected abstract void ImageItemBuild();
        protected abstract void HCGZItemBuild();
        protected abstract void MultiHatchCurveItemBuild();
        protected abstract void HatchRectItemBuild();
        protected abstract void NormalPuTuItemBuild();
        protected abstract void CurveHasHatchItemBuild();

      
        public void ExecAllItemBuilder()
        {
            CurveItemBuild();
            TextItemBuild();
            SymbolItemBuild();
            ImageItemBuild();
            HCGZItemBuild();
            MultiHatchCurveItemBuild();
            HatchRectItemBuild();
            NormalPuTuItemBuild();
            CurveHasHatchItemBuild();
        }
        
    }
}
