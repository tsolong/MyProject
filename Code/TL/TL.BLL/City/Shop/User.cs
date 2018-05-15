using System;
using System.Collections.Generic;
using System.Text;

using TL.Model;
using TL.Model.City.Shop;

namespace TL.BLL.City.Shop
{
    public class User
    {
        private TL.SQLServerDAL.City.Shop.User dal;
        public User()
        {
            dal = new TL.SQLServerDAL.City.Shop.User();
        }

        public LoginState CheckLogin(UserInfo ShopUser)
        {
            return dal.CheckLogin(ShopUser);
        }

        public bool CheckUserNameIsExist(string UserName)
        {
            return dal.CheckUserNameIsExist(UserName);
        }

        public bool CheckEmailIsExist(string Email)
        {
            return dal.CheckEmailIsExist(Email);
        }

        public int Add(UserInfo ShopUser)
        {
            return dal.Add(ShopUser);
        }

        public int ChangePassword(int UserId, string Password)
        {
            return dal.ChangePassword(UserId, Password);
        }
    }
}
