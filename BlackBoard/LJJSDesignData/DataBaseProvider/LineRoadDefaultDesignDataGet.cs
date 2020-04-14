using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LJJSCAD.BlackBoard.LJJSDesignData.Interface;
using System.Collections;
using System.Data.Common;
using LJJSCAD.DAL;
using LJJSCAD.BlackBoard.LJJSDesignData.ModelOper;
using LJJSCAD.Model;

namespace LJJSCAD.BlackBoard.LJJSDesignData.DataBaseProvider
{
    class LRDesignDataGetByDB : ILineRoadDesignDataGet
    {

        public List<LineRoadModel> GetDefaultLineRoadDesignModelLst(string myLineRoadDesignName)
        {
            //1,获得缺省线道模板所包含的所有线道设计数据；
            DbDataReader lineRoadDr = LineRoadDesignDAL.GetDefaultLineRoadDesignDR(myLineRoadDesignName);
            
            //2,转换成LineRoadModel；
            return LineRoadModelManage.GetLineRoadDesignModelLst(lineRoadDr);
            
        }
        public List<LineRoadModel> GetLineRoadDesignModelLst(string lineRoadDesignID)
        {
            DbDataReader lineRoadDr = LineRoadDesignDAL.GetLineRoadDesignDRByID(lineRoadDesignID);
            return LineRoadModelManage.GetLineRoadDesignModelLst(lineRoadDr);
           
        }


        public Model.JingShenModel GetDefaultJingShenDesign()
        {
            DbDataReader jsdr = LineRoadDesignDAL.GetDefaultJingShenDesignDR();
            return LineRoadModelManage.GetJingShenModel(jsdr);
        }
    }
}
