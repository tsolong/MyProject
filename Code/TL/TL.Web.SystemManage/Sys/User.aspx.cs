using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TL.Common;
using TL.Model.Core.Sys;

namespace TL.Web.SystemManage.Sys
{
    public partial class User : TL.Web.UI.SysUserPage
    {
        public int PageIndex;
        public int RecordTotal = 0;
        private PageBar MyPageBar;
        public string PageBarHtml = "";
        public IList<UserInfo> SysUserList;

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Tools.GetQueryString("action").ToLower())
            {
                case "del":
                    Del();
                    break;
                case "locked":
                    Locked();
                    break;
                case "unlocked":
                    UnLocked();
                    break;
                default:
                    GetList();
                    break;
            }
        }

        /// <summary>
        /// UserId���Ƿ������ǰ��¼�Ĺ���Ա
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        private bool IsCurrentUser(string UserId)
        {
            bool flag = false;
            string[] UserIdArr = UserId.Split(',');
            for (int i = 0; i < UserIdArr.Length; i++)
            {
                if (UserIdArr[i] == Sys_User.UserId.ToString())
                {
                    flag = true;
                }
            }
            return flag;
        }

        /// <summary>
        /// ɾ������Ա
        /// </summary>
        private void Del()
        {
            string UserId = Tools.GetQueryString("userid");
            if (UserId != string.Empty)
            {
                if (IsCurrentUser(UserId))
                {
                    ShowWindow(4, "ϵͳ��ʾ", "����ɾ���Լ�", null, true);
                }
                else
                {
                    if (new BLL.Core.Sys.User().Del(UserId) != 0)
                        ShowWindow(3, "ϵͳ��ʾ", "ɾ������Ա�ɹ�,��� \\\"ȷ��\\\" ��ť����", "user.aspx", false);
                    else
                        ShowWindow(4, "ϵͳ��ʾ", "ɾ������Աʧ��", null, true);
                }
            }
            else
            {
                ShowWindow(1, "ϵͳ��ʾ", "��ѡ��Ҫɾ���Ĺ���Ա", null, true);
            }
        }

        /// <summary>
        /// ��������Ա
        /// </summary>
        private void Locked()
        {
            string UserId = Tools.GetQueryString("userid");
            if (UserId != string.Empty)
            {
                if (IsCurrentUser(UserId))
                {
                    ShowWindow(4, "ϵͳ��ʾ", "���������Լ�", null, true);
                }
                else
                {
                    if (new BLL.Core.Sys.User().Locked(UserId) != 0)
                        ShowWindow(3, "ϵͳ��ʾ", "��������Ա�ɹ�,��� \\\"ȷ��\\\" ��ť����", "user.aspx", false);
                    else
                        ShowWindow(4, "ϵͳ��ʾ", "��������Աʧ��", null, true);
                }
            }
            else
            {
                ShowWindow(1, "ϵͳ��ʾ", "��ѡ��Ҫ�����Ĺ���Ա", null, true);
            }
        }

        /// <summary>
        /// ��������Ա
        /// </summary>
        private void UnLocked()
        {
            string UserId = Tools.GetQueryString("userid");
            if (UserId != string.Empty)
            {
                if (IsCurrentUser(UserId))
                {
                    ShowWindow(4, "ϵͳ��ʾ", "���ܽ����Լ�", null, true);
                }
                else
                {
                    if (new BLL.Core.Sys.User().UnLocked(UserId) != 0)
                        ShowWindow(3, "ϵͳ��ʾ", "��������Ա�ɹ�,��� \\\"ȷ��\\\" ��ť����", "user.aspx", false);
                    else
                        ShowWindow(4, "ϵͳ��ʾ", "��������Աʧ��", null, true);
                }
            }
            else
            {
                ShowWindow(1, "ϵͳ��ʾ", "��ѡ��Ҫ�����Ĺ���Ա", null, true);
            }
        }

        /// <summary>
        /// ��ȡ����Ա�û������б�
        /// </summary>
        private void GetList()
        {
            //���PageIndex����
            string tempIndex = Tools.GetQueryString("p");
            if (tempIndex == "") tempIndex = "1";
            if (Tools.IsPositiveInt(tempIndex))
            {
                PageIndex = Convert.ToInt32(tempIndex);

                SysUserList = new BLL.Core.Sys.User().GetList(PageIndex, PageSize, out RecordTotal);
                MyPageBar = new PageBar(PageIndex, PageSize, RecordTotal, "p");

                if (RecordTotal > 0)
                {
                    if (PageIndex > MyPageBar.PageTotal)
                        ShowWindow(4, "ϵͳ��ʾ", "��ҳ��������,��� \\\"ȷ��\\\" ��ť����", null, true);
                    else
                        PageBarHtml = MyPageBar.GetHTML();
                }
            }
            else
            {
                ShowWindow(4, "ϵͳ��ʾ", "��ҳ��������,��� \\\"ȷ��\\\" ��ť����", null, true);
            }
        }
    }
}
