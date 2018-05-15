using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using TL.Common;
using TL.Model;

namespace TL.Web
{
    public partial class Register : TL.Web.UI.BasePage
    {
        public bool RegisterShop;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Tools.GetQueryString("type").ToLower() == "shop")
            {
                RegisterShop = true;
            }

            switch (Tools.GetQueryString("action").ToLower())
            {
                case "checkusernameisexist":
                    CheckUserNameIsExist();
                    break;
                case "checkemailisexist":
                    CheckEmailIsExist();
                    break;
                case "checkvalidatecode":
                    CheckValidateCodeIsOk();
                    break;
                case "add":
                    CheckRegister();
                    break;
                default:
                    break;
            }
        }

        #region 检查注册

        private string UserName;
        private string Password;
        private string Email;

        /// <summary>
        /// 检查注册/添加用户
        /// </summary>
        private void CheckRegister()
        {
            if (CheckUserName() && CheckPassword() && CheckConfirmPassword() && CheckEmail() && CheckValidateCode())
            {
                int Result = 0;
                string msg = "";
                string url = "";
                if (RegisterShop)
                {
                    TL.Model.City.Shop.UserInfo NewShopUser = new TL.Model.City.Shop.UserInfo();
                    NewShopUser.UserName = UserName;
                    NewShopUser.Password = Tools.MD5(Password);
                    NewShopUser.Email = Email;
                    Result = new BLL.City.Shop.User().Add(NewShopUser);
                    msg="您已成功注册为 \\\"" + S.SiteName + "\\\" 的店铺用户";
                    url="myshop/login.aspx";
                }
                else
                {
                    TL.Model.Core.Member.UserInfo NewMemberUser = new TL.Model.Core.Member.UserInfo();
                    NewMemberUser.UserName = UserName;
                    NewMemberUser.Password = Tools.MD5(Password);
                    NewMemberUser.Email = Email;
                    Result = new BLL.Core.Member.User().Add(NewMemberUser);
                    msg = "您已成功注册为 \\\"" + S.SiteName + "\\\" 的会员用户";
                    url = "login.aspx";
                }
                if (Result != 0)
                {
                    ShowWindow(3, "注册成功", msg, url, false);
                    TL.Common.ValidateImage.ClearCode();
                }
                else
                {
                    ShowWindow(4, "系统提示", "注册失败", null, true);
                }
            }
        }

        /// <summary>
        /// 检查用户名
        /// </summary>
        /// <returns></returns>
        private bool CheckUserName()
        {
            UserName = Tools.GetForm("UserName").ToLower();
            if (UserName == string.Empty)
            {
                ShowWindow(4, "系统提示", "请填写用户名", null, true);
                return false;
            }
            else
            {
                if (Regex.IsMatch(UserName, @"^[a-zA-Z0-9_]{4,16}$"))
                {
                    bool Result = false;
                    if (RegisterShop)
                    {
                        Result = new BLL.City.Shop.User().CheckUserNameIsExist(UserName);
                    }
                    else 
                    {
                        Result = new BLL.Core.Member.User().CheckUserNameIsExist(UserName);
                    }
                    if (Result)
                    {
                        ShowWindow(4, "系统提示", "此用户名已被使用，换个其它的吧", null, true);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    ShowWindow(4, "系统提示", "用户名格式错误", null, true);
                    return false;
                }
            }
        }

        /// <summary>
        /// 检查密码
        /// </summary>
        /// <returns></returns>
        private bool CheckPassword()
        {
            Password = Tools.GetForm("Password").ToLower();
            if (Password == string.Empty)
            {
                ShowWindow(4, "系统提示", "请填写密码", null, true);
                return false;
            }
            else
            {
                if (Regex.IsMatch(Password, @"^[a-zA-Z0-9_]{4,16}$"))
                {
                        return true;
                }
                else
                {
                    ShowWindow(4, "系统提示", "密码格式错误", null, true);
                    return false;
                }
            }
        }

        /// <summary>
        /// 检查确认密码
        /// </summary>
        /// <returns></returns>
        private bool CheckConfirmPassword()
        {
            if (Tools.GetForm("Password").ToLower() != Tools.GetForm("ConfirmPassword").ToLower())
            {
                ShowWindow(4, "系统提示", "两次密码填写不一致", null, true);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检查邮箱
        /// </summary>
        /// <returns></returns>
        private bool CheckEmail()
        {
            Email = Tools.GetForm("Email").ToLower();
            if (Email == string.Empty)
            {
                ShowWindow(4, "系统提示", "请填写邮箱", null, true);
                return false;
            }
            else
            {
                if (Regex.IsMatch(Email, @"^[\w\.-]+@[\w\.-]+\.\w+$"))
                {
                    bool Result = false;
                    if (RegisterShop)
                    {
                        Result = new BLL.City.Shop.User().CheckEmailIsExist(Email);
                    }
                    else
                    {
                        Result = new BLL.Core.Member.User().CheckEmailIsExist(Email);
                    }
                    if (Result)
                    {
                        ShowWindow(4, "系统提示", "此邮箱已被使用，换个其它的吧", null, true);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    ShowWindow(4, "系统提示", "邮箱地址无效", null, true);
                    return false;
                }
            }
        }

        /// <summary>
        /// 检查验证码
        /// </summary>
        /// <returns></returns>
        private bool CheckValidateCode()
        {
            string ValidateCode = Tools.GetForm("ValidateCode");
            if (ValidateCode == string.Empty)
            {
                ShowWindow(4, "系统提示", "请填写验证码", null, true);
                return false;
            }
            else
            {
                if (Regex.IsMatch(ValidateCode, @"^[0-9]{4,4}$"))
                {
                    if (TL.Common.ValidateImage.CheckCode(ValidateCode))
                    {
                        return true;
                    }
                    else
                    {
                        ShowWindow(4, "系统提示", " 验证码填写错误", null, true);
                        return false;
                    }
                }
                else
                {
                    ShowWindow(4, "系统提示", "验证码只能是4位数字", null, true);
                    return false;
                }
            }
        }

        #endregion

        #region 注册Ajax调用检查

        /// <summary>
        /// Ajax调用检查用户名是否已存在
        /// </summary>
        private void CheckUserNameIsExist()
        {
            string UserName = Tools.GetQueryString("username").ToLower();
            if (UserName != "")
            {
                if (RegisterShop)
                {
                    Response.Write(new BLL.City.Shop.User().CheckUserNameIsExist(UserName).ToString().ToLower());
                }
                else
                {
                    Response.Write(new BLL.Core.Member.User().CheckUserNameIsExist(UserName).ToString().ToLower());
                }
            }
            else
            {
                Response.Write("true");
            }
            Response.End();
        }

        /// <summary>
        /// Ajax调用检查邮箱是否已存在
        /// </summary>
        private void CheckEmailIsExist()
        {
            string Email = Tools.GetQueryString("email").ToLower();
            if (Email != "")
            {
                if (RegisterShop)
                {
                    Response.Write(new BLL.City.Shop.User().CheckEmailIsExist(Email).ToString().ToLower());
                }
                else
                {
                    Response.Write(new BLL.Core.Member.User().CheckEmailIsExist(Email).ToString().ToLower());
                }
            }
            else
            {
                Response.Write("true");
            }
            Response.End();
        }

        /// <summary>
        /// Ajax调用检查验证码是否正确
        /// </summary>
        private void CheckValidateCodeIsOk()
        {
            Response.Write(TL.Common.ValidateImage.CheckCode(Tools.GetQueryString("code")).ToString().ToLower());
            Response.End();
        }

        #endregion
    }
}
