using System;
using System.Collections.Generic;
using System.Text;

using TL.Model.Core.Member;

namespace TL.BLL.Core.Member
{
    public class User
    {
        private TL.SQLServerDAL.Core.Member.User dal;
        public User()
        {
            dal = new TL.SQLServerDAL.Core.Member.User();
        }

        public bool CheckUserNameIsExist(string UserName)
        {
            return dal.CheckUserNameIsExist(UserName);
        }

        public bool CheckEmailIsExist(string Email)
        {
            return dal.CheckEmailIsExist(Email);
        }

        public int Add(UserInfo MemberUser)
        {
            return dal.Add(MemberUser);
        }
    }
}
