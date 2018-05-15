using System;
using System.Collections.Generic;
using System.Text;

using TL.Model.City;

namespace TL.BLL.City
{
    public class FoodSeries
    {
        private TL.SQLServerDAL.City.FoodSeries dal;
        public FoodSeries() 
        {
            dal = new TL.SQLServerDAL.City.FoodSeries();
        }

        public IList<FoodSeriesInfo> GetList()
        {
            return dal.GetList();
        }

        public IList<FoodSeriesSubInfo> GetSubList(int FoodSeriesId)
        {
            return dal.GetSubList(FoodSeriesId);
        }

        public string GetSubListToJson(int FoodSeriesId)
        {
            return dal.GetSubListToJson(FoodSeriesId);
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
