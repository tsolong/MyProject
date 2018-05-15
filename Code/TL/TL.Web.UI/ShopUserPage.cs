using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

using TL.Model.City.Shop;

namespace TL.Web.UI
{
    /// <summary>
    /// Web层->店铺用户公用类
    /// </summary>
    public class ShopUserPage : BasePage
    {
        /// <summary>
        /// 当前登录的店铺用户对象
        /// </summary>
        protected UserInfo Shop_User;
        public ShopUserPage()
        {
            this.Load += new EventHandler(ShopUserPage_Load);
        }

        /// <summary>
        /// 验证是否登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ShopUserPage_Load(object sender, EventArgs e)
        {
            if (Session["ShopUser"] != null)
                Shop_User = (UserInfo)(Session["ShopUser"]);
            else
                Response.Redirect("/myshop/overtime.aspx");
        }
    }
}
