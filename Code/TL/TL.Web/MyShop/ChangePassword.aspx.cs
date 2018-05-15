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
using System.Text.RegularExpressions;

using TL.Common;

namespace TL.Web.MyShop
{
    public partial class ChangePassword : TL.Web.UI.ShopUserPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Tools.GetQueryString("action").ToLower() == "save")
            {
                string Password = Tools.GetForm("Password").ToLower();
                if (Password == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写新密码", null, true);
                }
                else if (!Regex.IsMatch(Password, @"^[a-zA-Z0-9_]{4,16}$"))
                {
                    ShowWindow(4, "系统提示", "新密码格式错误", null, true);
                }
                else if (Password != Tools.GetForm("ConfirmPassword").ToLower())
                {
                    ShowWindow(4, "系统提示", "两次密码填写不一致", null, true);
                }
                else
                {
                    if (new BLL.City.Shop.User().ChangePassword(Shop_User.UserId, Tools.MD5(Password)) != 0)
                        ShowWindow(3, "系统提示", "新密码保存成功，下次登录生效。", "changepassword.aspx", false);
                    else
                        ShowWindow(4, "系统提示", "新密码保存失败", null, true);
                }
            }
        }
    }
}
