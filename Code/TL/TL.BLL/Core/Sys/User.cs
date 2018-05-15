using System;
using System.Collections.Generic;
using System.Text;

using TL.Model;
using TL.Model.Core.Sys;

namespace TL.BLL.Core.Sys
{
    public class User
    {
        private TL.SQLServerDAL.Core.Sys.User dal;
        public User()
        {
            dal = new TL.SQLServerDAL.Core.Sys.User();
        }

        public LoginState CheckLogin(UserInfo SysUser)
        {
            return dal.CheckLogin(SysUser);
        }

        public bool CheckUserNameIsExist(string UserName)
        {
            return dal.CheckUserNameIsExist(UserName);
        }

        public int Add(UserInfo SysUser)
        {
            return dal.Add(SysUser);
        }

        public int Del(string UserId)
        {
            return dal.Del(UserId);
        }

        public int ChangePassword(int UserId, string Password)
        {
            return dal.ChangePassword(UserId, Password);
        }

        public int Locked(string UserId)
        {
            return dal.Locked(UserId);
        }

        public int UnLocked(string UserId)
        {
            return dal.UnLocked(UserId);
        }

        public UserInfo GetById(int UserId)
        {
            return dal.GetById(UserId);
        }

        public IList<UserInfo> GetList(int PageIndex, int PageSize, out int RecordTotal)
        {
            return dal.GetList(PageIndex, PageSize, out RecordTotal);
        }
    }
}
