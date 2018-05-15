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

        #region ���ע��

        private string UserName;
        private string Password;
        private string Email;

        /// <summary>
        /// ���ע��/����û�
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
                    msg="���ѳɹ�ע��Ϊ \\\"" + S.SiteName + "\\\" �ĵ����û�";
                    url="myshop/login.aspx";
                }
                else
                {
                    TL.Model.Core.Member.UserInfo NewMemberUser = new TL.Model.Core.Member.UserInfo();
                    NewMemberUser.UserName = UserName;
                    NewMemberUser.Password = Tools.MD5(Password);
                    NewMemberUser.Email = Email;
                    Result = new BLL.Core.Member.User().Add(NewMemberUser);
                    msg = "���ѳɹ�ע��Ϊ \\\"" + S.SiteName + "\\\" �Ļ�Ա�û�";
                    url = "login.aspx";
                }
                if (Result != 0)
                {
                    ShowWindow(3, "ע��ɹ�", msg, url, false);
                    TL.Common.ValidateImage.ClearCode();
                }
                else
                {
                    ShowWindow(4, "ϵͳ��ʾ", "ע��ʧ��", null, true);
                }
            }
        }

        /// <summary>
        /// ����û���
        /// </summary>
        /// <returns></returns>
        private bool CheckUserName()
        {
            UserName = Tools.GetForm("UserName").ToLower();
            if (UserName == string.Empty)
            {
                ShowWindow(4, "ϵͳ��ʾ", "����д�û���", null, true);
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
                        ShowWindow(4, "ϵͳ��ʾ", "���û����ѱ�ʹ�ã����������İ�", null, true);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    ShowWindow(4, "ϵͳ��ʾ", "�û�����ʽ����", null, true);
                    return false;
                }
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        private bool CheckPassword()
        {
            Password = Tools.GetForm("Password").ToLower();
            if (Password == string.Empty)
            {
                ShowWindow(4, "ϵͳ��ʾ", "����д����", null, true);
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
                    ShowWindow(4, "ϵͳ��ʾ", "�����ʽ����", null, true);
                    return false;
                }
            }
        }

        /// <summary>
        /// ���ȷ������
        /// </summary>
        /// <returns></returns>
        private bool CheckConfirmPassword()
        {
            if (Tools.GetForm("Password").ToLower() != Tools.GetForm("ConfirmPassword").ToLower())
            {
                ShowWindow(4, "ϵͳ��ʾ", "����������д��һ��", null, true);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        private bool CheckEmail()
        {
            Email = Tools.GetForm("Email").ToLower();
            if (Email == string.Empty)
            {
                ShowWindow(4, "ϵͳ��ʾ", "����д����", null, true);
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
                        ShowWindow(4, "ϵͳ��ʾ", "�������ѱ�ʹ�ã����������İ�", null, true);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    ShowWindow(4, "ϵͳ��ʾ", "�����ַ��Ч", null, true);
                    return false;
                }
            }
        }

        /// <summary>
        /// �����֤��
        /// </summary>
        /// <returns></returns>
        private bool CheckValidateCode()
        {
            string ValidateCode = Tools.GetForm("ValidateCode");
            if (ValidateCode == string.Empty)
            {
                ShowWindow(4, "ϵͳ��ʾ", "����д��֤��", null, true);
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
                        ShowWindow(4, "ϵͳ��ʾ", " ��֤����д����", null, true);
                        return false;
                    }
                }
                else
                {
                    ShowWindow(4, "ϵͳ��ʾ", "��֤��ֻ����4λ����", null, true);
                    return false;
                }
            }
        }

        #endregion

        #region ע��Ajax���ü��

        /// <summary>
        /// Ajax���ü���û����Ƿ��Ѵ���
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
        /// Ajax���ü�������Ƿ��Ѵ���
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
        /// Ajax���ü����֤���Ƿ���ȷ
        /// </summary>
        private void CheckValidateCodeIsOk()
        {
            Response.Write(TL.Common.ValidateImage.CheckCode(Tools.GetQueryString("code")).ToString().ToLower());
            Response.End();
        }

        #endregion
    }
}
