using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

using TL.Model.City.Shop;

namespace TL.Web.UI
{
    /// <summary>
    /// Web��->�����û�������
    /// </summary>
    public class ShopUserPage : BasePage
    {
        /// <summary>
        /// ��ǰ��¼�ĵ����û�����
        /// </summary>
        protected UserInfo Shop_User;
        public ShopUserPage()
        {
            this.Load += new EventHandler(ShopUserPage_Load);
        }

        /// <summary>
        /// ��֤�Ƿ��¼
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
