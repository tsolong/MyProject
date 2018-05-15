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
                    ShowWindow(1, "ϵͳ��ʾ", "����д������", null, true);
                }
                else if (!Regex.IsMatch(Password, @"^[a-zA-Z0-9_]{4,16}$"))
                {
                    ShowWindow(4, "ϵͳ��ʾ", "�������ʽ����", null, true);
                }
                else if (Password != Tools.GetForm("ConfirmPassword").ToLower())
                {
                    ShowWindow(4, "ϵͳ��ʾ", "����������д��һ��", null, true);
                }
                else
                {
                    if (new BLL.City.Shop.User().ChangePassword(Shop_User.UserId, Tools.MD5(Password)) != 0)
                        ShowWindow(3, "ϵͳ��ʾ", "�����뱣��ɹ����´ε�¼��Ч��", "changepassword.aspx", false);
                    else
                        ShowWindow(4, "ϵͳ��ʾ", "�����뱣��ʧ��", null, true);
                }
            }
        }
    }
}
