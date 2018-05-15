using System;
using System.Collections.Generic;
using System.Text;

using TL.Model.City;

namespace TL.BLL.City
{
    public class Area
    {
        private TL.SQLServerDAL.City.Area dal;
        public Area() 
        {
            dal = new TL.SQLServerDAL.City.Area();
        }

        public IList<AreaInfo> GetList()
        {
            return dal.GetList();
        }

        public IList<AreaSubInfo> GetSubList(int AreaId)
        {
            return dal.GetSubList(AreaId);
        }

        public string GetSubListToJson(int AreaId)
        {
            return dal.GetSubListToJson(AreaId);
        }

        public string GetName(int Id)
        {
            return dal.GetName(Id);
        }

        public string GetSubName(int Id)
        {
            return dal.GetSubName(Id);
        }
    }
}
