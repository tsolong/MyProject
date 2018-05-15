using System;
using System.Collections.Generic;
using System.Text;

using TL.Model.City.Shop;
using TL.Model.City.Search;

namespace TL.BLL.City.Search
{
    public class Search
    {
        private TL.SQLServerDAL.City.Search.Search dal;
        public Search()
        {
            dal = new TL.SQLServerDAL.City.Search.Search();
        }

        public IList<ShopInfo> GetShopList(SearchInfo SearchInfo)
        {
            return dal.GetShopList(SearchInfo);
        }
    }
}
