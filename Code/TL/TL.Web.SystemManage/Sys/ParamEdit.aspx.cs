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
using TL.Model.Core.Sys;

namespace TL.Web.SystemManage.Sys
{
    public partial class ParamEdit : TL.Web.UI.SysUserPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Tools.GetQueryString("action").ToLower() == "save")
            {
                string SiteName = Tools.GetForm("SiteName");
                string SiteSubName = Tools.GetForm("SiteSubName");
                string SiteDomain = Tools.GetForm("SiteDomain");
                string SiteEmail = Tools.GetForm("SiteEmail").ToLower();
                string Keywords = Tools.GetForm("Keywords");
                string Description = Tools.GetForm("Description");
                string Copyright = Tools.GetForm("Copyright");

                if (SiteName == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写网站名称", null, true);
                }
                else if (SiteSubName == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写网站子标题", null, true);
                }
                else if (SiteDomain == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写网站域名", null, true);
                }
                else if (SiteEmail == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写网站邮箱", null, true);
                }
                else if (!Regex.IsMatch(SiteEmail, @"^[\w\.-]+@[\w\.-]+\.\w+$"))
                {
                    ShowWindow(4, "系统提示", "邮箱地址无效", null, true);
                }
                else if (Keywords == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写关键词", null, true);
                }
                else if (Description == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写网页描述", null, true);
                }
                else if (Copyright == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写网站版权信息", null, true);
                }
                else
                {
                    ParamInfo SysParam = new ParamInfo();
                    SysParam.SiteName = SiteName;
                    SysParam.SiteSubName = SiteSubName;
                    SysParam.SiteDomain = SiteDomain;
                    SysParam.SiteEmail = SiteEmail;
                    SysParam.Keywords = Keywords;
                    SysParam.Description = Description;
                    SysParam.Copyright = Copyright;

                    if (new BLL.Core.Sys.Param().Save(SysParam) != 0)
                        ShowWindow(3, "系统提示", "保存存功,点击 \\\"确定\\\" 换钮返回", "paramedit.aspx", false);
                    else
                        ShowWindow(4, "系统提示", "保存失败", null, true);
                }
            }
        }
    }
}
