using System;
using System.Collections.Generic;
using System.Text;

using TL.Model.Core.Sys;

namespace TL.BLL.Core.Sys
{
    public class Param
    {
        private TL.SQLServerDAL.Core.Sys.Param dal;
        public Param() 
        {
            dal = new TL.SQLServerDAL.Core.Sys.Param();
        }

        public ParamInfo Get()
        {
            return dal.Get();
        }

        public int Save(ParamInfo SParam)
        {
            return dal.Save(SParam);
        }
    }
}
