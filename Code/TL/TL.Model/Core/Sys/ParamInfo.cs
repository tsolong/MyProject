using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Model.Core.Sys
{
    /// <summary>
    /// 系统参数
    /// </summary>
    public class ParamInfo
    {
        private int _Id;
        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _SiteName;
        /// <summary>
        ///站点名称
        /// </summary>
        public string SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }

        private string _SiteSubName;
        /// <summary>
        /// 站点子标题
        /// </summary>
        public string SiteSubName
        {
            get { return _SiteSubName; }
            set { _SiteSubName = value; }
        }

        private string _SiteDomain;
        /// <summary>
        /// 站点域名
        /// </summary>
        public string SiteDomain
        {
            get { return _SiteDomain; }
            set { _SiteDomain = value; }
        }

        private string _SiteEmail;
        /// <summary>
        /// 站点邮箱
        /// </summary>
        public string SiteEmail
        {
            get { return _SiteEmail; }
            set { _SiteEmail = value; }
        }

        private string _Keywords;
        /// <summary>
        /// 关键词
        /// </summary>
        public string Keywords
        {
            get { return _Keywords; }
            set { _Keywords = value; }
        }

        private string _Description;
        /// <summary>
        /// 网页描述
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _Copyright;
        /// <summary>
        /// 版权信息
        /// </summary>
        public string Copyright
        {
            get { return _Copyright; }
            set { _Copyright = value; }
        }
    }
}
