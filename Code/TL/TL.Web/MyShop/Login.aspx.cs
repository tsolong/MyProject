using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TL.Common;
using TL.Model;
using TL.Model.City.Shop;

namespace TL.Web.MyShop
{
    public partial class Login : TL.Web.UI.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断action并执行相应的操作
            switch (Tools.GetQueryString("action").ToLower())
            {
                case "checklogin":
                    CheckLogin();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 验证登录初始化
        /// </summary>
        private void CheckLogin()
        {
            string UserName = Tools.GetForm("UserName");
            string Password = Tools.GetForm("Password");
            if (UserName == string.Empty || Password == string.Empty)
            {
                Response.Write(Convert.ToInt32(LoginState.Err_Format).ToString());
            }
            else
            {
                UserInfo ShopUser = new UserInfo();
                ShopUser.UserName = UserName;
                ShopUser.Password = Tools.MD5(Password);
                ShopUser.LastLoginTime = DateTime.Now;
                ShopUser.LastLoginIP = Tools.GetIP();

                LoginState ls = new BLL.City.Shop.User().CheckLogin(ShopUser);
                if (ls == LoginState.Succeed)
                    Session.Add("ShopUser", ShopUser);
                Response.Write(Convert.ToInt32(ls).ToString());
            }
            Response.End();
        }
    }
}
