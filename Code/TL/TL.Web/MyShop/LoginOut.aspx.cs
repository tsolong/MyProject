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

namespace TL.Web.MyShop
{
    public partial class LoginOut : TL.Web.UI.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ShopUser"] != null)
                Session.Remove("ShopUser");
        }
    }
}
