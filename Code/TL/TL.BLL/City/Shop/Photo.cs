using System;
using System.Collections.Generic;
using System.Text;

using TL.Model.City.Shop;

namespace TL.BLL.City.Shop
{
    public class Photo
    {
        private TL.SQLServerDAL.City.Shop.Photo dal;
        public Photo()
        {
            dal = new TL.SQLServerDAL.City.Shop.Photo();
        }

        public int Add(PhotoInfo ShopPhoto)
        {
            return dal.Add(ShopPhoto);
        }

        public int Del(string Id, int UserId, out string PhotoUrls)
        {
            return dal.Del(Id, UserId, out PhotoUrls);
        }

        public int DelAll(int UserId, out string PhotoUrls)
        {
            return dal.DelAll(UserId, out PhotoUrls);
        }

        public int Save(PhotoInfo ShopPhoto)
        {
            return dal.Save(ShopPhoto);
        }

        public int GetTotalPhoto(int UserId)
        {
            return dal.GetTotalPhoto(UserId);
        }

        public IList<PhotoInfo> GetList(int UserId)
        {
            return dal.GetList(UserId);
        }

        public PhotoInfo GetByUserId(int UserId)
        {
            return dal.GetByUserId(UserId);
        }
    }
}
