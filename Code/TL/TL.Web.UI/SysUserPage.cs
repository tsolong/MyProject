using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

using TL.Model.Core.Sys;

namespace TL.Web.UI
{
    /// <summary>
    /// Web层->系统用户公用类
    /// </summary>
    public class SysUserPage : BasePage
    {
        protected UserInfo Sys_User;
        public SysUserPage()
        {
            this.Load += new EventHandler(SysUserPage_Load);
        }

        /// <summary>
        /// 验证是否登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SysUserPage_Load(object sender, EventArgs e)
        {
            if (Session["SysUser"] != null)
                Sys_User = (UserInfo)(Session["SysUser"]);
            else
                Response.Redirect("/overtime.aspx");
        }
    }
}
