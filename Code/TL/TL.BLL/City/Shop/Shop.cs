using System;
using System.Collections.Generic;
using System.Text;

using TL.Model.City.Shop;

namespace TL.BLL.City.Shop
{
    public class Shop
    {
        private TL.SQLServerDAL.City.Shop.Shop dal;
        public Shop()
        {
            dal = new TL.SQLServerDAL.City.Shop.Shop();
        }

        public ShopInfo GetById(int UserId)
        {
            return dal.GetById(UserId);
        }

        public int Save(ShopInfo MyShop)
        {
            return dal.Save(MyShop);
        }
    }
}
