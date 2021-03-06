using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;

using TL.Model.City;
using TL.Model.Core.Sys;

namespace TL.Web.UI
{
    /// <summary>
    /// Web层->基础公用类
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        //系统参数信息对象
        protected ParamInfo S;

        //当前城市信息对象
        protected CityInfo CurrentCity;

        //资源地址
        protected string ResourcesUrl = TL.Config.SysConfig.GetConfigValue("ResourcesUrl");

        //分页条数
        protected int PageSize = Convert.ToInt32(TL.Config.SysConfig.GetConfigValue("PageSize"));

        public BasePage()
        {
            //获取系统参数
            S = new BLL.Core.Sys.Param().Get();
            //获取当前城市相关信息
            CurrentCity = TL.Config.SysConfig.GetCityByDomain(TL.Common.Tools.GetHost());
        }

        /// <summary>
        /// 页面对话框
        /// </summary>
        /// <param name="Type">1:提示  2:警告  3:正确或成功  4:错误</param>
        /// <param name="Title">对话框标题</param>
        /// <param name="Msg">提示信息</param>
        /// <param name="Url">跳转的Url History设为false时有效</param>
        /// <param name="History">跳转到前一页 false:跳转到所指定的Url,True:跳转到前一页</param>
        public void ShowWindow(int Type, string Title, string Msg, string Url, bool History)
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r");
            System.Web.HttpContext.Current.Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\" >\r<head>\r");
            System.Web.HttpContext.Current.Response.Write("\t<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r");
            System.Web.HttpContext.Current.Response.Write("\t<meta http-equiv=\"Content-Language\" content=\"zh-CN\"/>\r");
            System.Web.HttpContext.Current.Response.Write("\t<meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\" />\r");
            System.Web.HttpContext.Current.Response.Write("\t<title>" + Title +  " - " + S.SiteName + "</title>\r");
            System.Web.HttpContext.Current.Response.Write("\t<link type=\"text/css\" rel=\"stylesheet\" href=\"" + ResourcesUrl + "/Common/TL/TL-More.css\" />\r");
            System.Web.HttpContext.Current.Response.Write("\t<script type=\"text/javascript\" src=\"" + ResourcesUrl + "/Common/TL/TL-Core.js\"></script>\r");
            System.Web.HttpContext.Current.Response.Write("\t<script type=\"text/javascript\" src=\"" + ResourcesUrl + "/Common/TL/TL-More.js\"></script>\r");
            System.Web.HttpContext.Current.Response.Write("\t<script type=\"text/javascript\">\r");

            string str = History ? "\r\t\t\t\thistory.go(-1);" : "\r\t\t\t\tlocation.href = \"" + Url + "\";";
            System.Web.HttpContext.Current.Response.Write("\taddEvent(window, \"load\", function(){" +
                "\r\t\tnew win({" +
                    "\r\t\t\ttype: " + Type.ToString() + "," +
                    "\r\t\t\ttitle: \"" + Title + "\"," +
                    "\r\t\t\tmsg: \"" + Msg + "\"," +
                    "\r\t\t\tcloseEvent: function(){" + str +
                    "\r\t\t\t}," +
                    "\r\t\t\tisOverlay: false," +
                    "\r\t\t\tdragOpacity: 1" +
                "\r\t\t})" +
            "\r\t})\r\t");
            System.Web.HttpContext.Current.Response.Write("</script>\r");
            System.Web.HttpContext.Current.Response.Write("</head>\r<body>　\r</body>\r</html>");
            System.Web.HttpContext.Current.Response.End();
        }
    }
}
